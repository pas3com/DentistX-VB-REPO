Imports System.Collections.Concurrent
Imports System.Diagnostics
Imports System.Linq
Imports System.Runtime.InteropServices
Imports DevExpress.XtraBars.ToastNotifications

''' <summary>
''' Bridges DevExpress toast clicks to <see cref="FrmWhatsAppActivityLog"/>.
''' Windows may raise <see cref="ToastNotificationsManager.Activated"/> on a non-UI thread (e.g. Action Center after delay);
''' all form work must run on the host's UI thread or activation fails silently and the toast id mapping is lost if removed too early.
''' </summary>
Public NotInheritable Class WhatsAppToastHost
    Private Shared ReadOnly RegLock As New Object
    Private Shared ReadOnly Registrations As New List(Of Registration)()
    Private Shared ReadOnly ToastIdToLogId As New ConcurrentDictionary(Of Guid, Long)()
    Private Shared ReadOnly WiredManagers As New HashSet(Of ToastNotificationsManager)()

    Private Class Registration
        Public Property HostForm As Form
        Public Property Manager As ToastNotificationsManager
    End Class

    Public Shared Sub Register(host As Form, manager As ToastNotificationsManager)
        If host Is Nothing OrElse manager Is Nothing Then Return
        SyncLock RegLock
            UnregisterLocked(host)
            Registrations.Add(New Registration With {.HostForm = host, .Manager = manager})
            If WiredManagers.Add(manager) Then
                AddHandler manager.Activated, AddressOf OnToastActivated
            End If
        End SyncLock
    End Sub

    Public Shared Sub Unregister(host As Form)
        If host Is Nothing Then Return
        SyncLock RegLock
            UnregisterLocked(host)
        End SyncLock
    End Sub

    Private Shared Sub UnregisterLocked(host As Form)
        Dim toRemove = Registrations.Where(Function(r) r.HostForm Is host).ToList()
        For Each r In toRemove
            Registrations.Remove(r)
            If Not Registrations.Any(Function(x) ReferenceEquals(x.Manager, r.Manager)) Then
                RemoveHandler r.Manager.Activated, AddressOf OnToastActivated
                WiredManagers.Remove(r.Manager)
            End If
        Next
    End Sub

    Private Shared Function ResolveHostForm(sender As Object) As Form
        SyncLock RegLock
            For i As Integer = Registrations.Count - 1 To 0 Step -1
                Dim r = Registrations(i)
                If r Is Nothing OrElse r.HostForm Is Nothing OrElse r.HostForm.IsDisposed Then Continue For
                If ReferenceEquals(r.Manager, sender) Then Return r.HostForm
            Next
            For i As Integer = Registrations.Count - 1 To 0 Step -1
                Dim r = Registrations(i)
                If r IsNot Nothing AndAlso r.HostForm IsNot Nothing AndAlso Not r.HostForm.IsDisposed Then Return r.HostForm
            Next
        End SyncLock
        Return Nothing
    End Function

    Private Shared Sub OnToastActivated(sender As Object, e As ToastNotificationEventArgs)
        If e Is Nothing Then Return
        Dim notificationId = e.NotificationID
        Dim host = ResolveHostForm(sender)

        If host Is Nothing OrElse Not host.IsHandleCreated OrElse host.IsDisposed Then
            host = FindAnyMainShellForm()
        End If

        If host IsNot Nothing AndAlso host.IsHandleCreated AndAlso Not host.IsDisposed Then
            If host.InvokeRequired Then
                Try
                    host.Invoke(New Action(Sub() HandleToastActivatedOnUiThread(notificationId)))
                Catch ex As Exception
                    Debug.WriteLine("WhatsAppToastHost.Invoke failed: " & ex.Message)
                End Try
            Else
                HandleToastActivatedOnUiThread(notificationId)
            End If
        Else
            HandleToastActivatedOnUiThread(notificationId)
        End If
    End Sub

    Private Shared Function FindAnyMainShellForm() As Form
        Try
            For Each frm As Form In Application.OpenForms
                If TypeOf frm Is MainView3 OrElse TypeOf frm Is MainView1 Then Return frm
            Next
        Catch
        End Try
        Return Nothing
    End Function

    Private Shared Sub HandleToastActivatedOnUiThread(notificationId As Guid)
        Dim logId As Long
        If Not ToastIdToLogId.TryGetValue(notificationId, logId) OrElse logId <= 0 Then
            Debug.WriteLine("WhatsAppToastHost: no log mapping for notification " & notificationId.ToString())
            Return
        End If

        Try
            Dim shell = ActivateAndGetMainShell()
            If shell Is Nothing Then
                Debug.WriteLine("WhatsAppToastHost: main shell not found for log " & logId.ToString())
                Return
            End If
            BringWindowToForeground(shell)
            FrmWhatsAppActivityLog.ShowForLogEntry(shell, logId)
            ToastIdToLogId.TryRemove(notificationId, logId)
        Catch ex As Exception
            Debug.WriteLine("WhatsAppToastHost activation failed: " & ex.Message)
        End Try
    End Sub

    Private Shared Sub BringWindowToForeground(f As Form)
        If f Is Nothing OrElse f.IsDisposed OrElse Not f.IsHandleCreated Then Return
        Try
            If f.WindowState = FormWindowState.Minimized Then f.WindowState = FormWindowState.Normal
            NativeMethods.ShowWindow(f.Handle, NativeMethods.SW_RESTORE)
            f.BringToFront()
            f.Activate()
            NativeMethods.SetForegroundWindow(f.Handle)
        Catch
        End Try
    End Sub

    Private Shared Function ActivateAndGetMainShell() As Form
        Try
            For Each frm As Form In Application.OpenForms
                If TypeOf frm Is MainView3 OrElse TypeOf frm Is MainView1 Then
                    BringWindowToForeground(frm)
                    Return frm
                End If
            Next
        Catch
        End Try
        Return Nothing
    End Function

    Public Shared Sub NotifySendResult(logId As Long, success As Boolean, targetNumber As String, messagePreview As String)
        If logId <= 0 Then Return
        Dim toastId = Guid.NewGuid()
        ToastIdToLogId(toastId) = logId
        Dim title = If(success,
                       If(Eng, "WhatsApp sent", "تم إرسال واتساب"),
                       If(Eng, "WhatsApp failed", "فشل واتساب"))
        Dim num = If(String.IsNullOrWhiteSpace(targetNumber), "-", targetNumber.Trim())
        Dim prev = If(String.IsNullOrWhiteSpace(messagePreview), "", messagePreview.Trim())
        If prev.Length > 160 Then prev = prev.Substring(0, 157) & "..."
        Dim body = num & If(String.IsNullOrEmpty(prev), "", " · " & prev)
        Dim toast As New ToastNotification(toastId, Nothing, title, body, Nothing, ToastNotificationTemplate.Text01)
        Dim target As Registration = Nothing
        SyncLock RegLock
            target = Registrations.LastOrDefault(Function(r) r.HostForm IsNot Nothing AndAlso Not r.HostForm.IsDisposed AndAlso TypeOf r.HostForm Is MainView3 AndAlso r.HostForm.IsHandleCreated)
            If target Is Nothing Then
                target = Registrations.LastOrDefault(Function(r) r.HostForm IsNot Nothing AndAlso Not r.HostForm.IsDisposed AndAlso r.HostForm.Visible AndAlso r.HostForm.WindowState <> FormWindowState.Minimized)
            End If
            If target Is Nothing Then target = Registrations.LastOrDefault(Function(r) r.HostForm IsNot Nothing AndAlso Not r.HostForm.IsDisposed AndAlso r.HostForm.IsHandleCreated)
            If target Is Nothing Then target = Registrations.LastOrDefault()
        End SyncLock
        If target Is Nothing OrElse Not target.HostForm.IsHandleCreated Then Return
        Try
            target.HostForm.BeginInvoke(Sub()
                                            Try
                                                target.Manager.ShowNotification(toast)
                                            Catch
                                            End Try
                                        End Sub)
        Catch
        End Try
    End Sub

    Private NotInheritable Class NativeMethods
        Friend Const SW_RESTORE As Integer = 9

        <DllImport("user32.dll")>
        Friend Shared Function SetForegroundWindow(hWnd As IntPtr) As Boolean
        End Function

        <DllImport("user32.dll")>
        Friend Shared Function ShowWindow(hWnd As IntPtr, nCmdShow As Integer) As Boolean
        End Function
    End Class
End Class
