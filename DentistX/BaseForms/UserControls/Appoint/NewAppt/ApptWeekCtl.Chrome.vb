Imports System
Imports System.Collections.Generic
Imports System.Drawing
Imports System.Globalization
Imports System.Linq
Imports System.Runtime.InteropServices
Imports System.Windows.Forms
Imports DevExpress.Utils
Imports DevExpress.Utils.Layout
Imports DevExpress.XtraEditors

Partial Public Class ApptWeekCtl
    Friend Sub RequestCloseWeekDrawer()
        CloseWeekDayDrawerImmediate()
    End Sub

    Friend Sub RequestOpenWeekDrawerDialog(dayDate As DateTime, appts As List(Of AppointmentC), data As ApptDataBundle, state As ApptState, request As ApptViewRequest)
        If _drawerDialogHost Is Nothing OrElse data Is Nothing OrElse state Is Nothing Then Return
        PrepareDialogOverlayParent()
        If _dimOverlay IsNot Nothing Then
            _dimOverlay.Visible = True
            _dimOverlay.BringToFront()
        End If
        _drawerDialogHost.Visible = True
        _drawerDialogHost.BringToFront()
        _drawerDialogHost.Populate(dayDate, appts, data, state, request, Me)
        ApptErrorHelper.SafeFocus(_drawerDialogHost, "ApptWeekCtl.RequestOpenWeekDrawerDialog.FocusDialog")
    End Sub

    Friend Sub RequestCloseWeekDrawerDialog()
        If _drawerDialogHost IsNot Nothing Then
            _drawerDialogHost.Visible = False
        End If
        RestoreDialogOverlayParent()
        If _dayDrawerHost IsNot Nothing AndAlso _dayDrawerHost.Visible Then
            _dayDrawerHost.BringToFront()
        ElseIf _dimOverlay IsNot Nothing Then
            _dimOverlay.Visible = False
        End If
    End Sub

    Private Sub CloseWeekDayDrawerImmediate()
        RequestCloseWeekDrawerDialog()
        If _dayDrawerHost IsNot Nothing Then
            _dayDrawerHost.Visible = False
            _dayDrawerHost.Width = 0
        End If
        If _dimOverlay IsNot Nothing Then _dimOverlay.Visible = False
        LayoutWeekColumns()
    End Sub

    Private Const WeekDayDrawerWidthPx As Integer = 232

    Private Function ResolveWeekDayOrderDoctorId(state As ApptState) As Integer?
        If state IsNot Nothing AndAlso state.OrderByDoctorId.HasValue AndAlso state.OrderByDoctorId.Value > 0 Then
            Return state.OrderByDoctorId.Value
        End If
        Dim linked = ApptTheme.ResolveDisplayLinkedDrId()
        Return If(linked > 0, CType(linked, Integer?), Nothing)
    End Function

    Private Function BuildWeekDayAppointments(dayDate As DateTime, data As ApptDataBundle, state As ApptState) As List(Of AppointmentC)
        Return ApptTheme.OrderAppointmentsForWeekDayGroupsAndSolos(
            If(data?.Appointments, New List(Of AppointmentC)()).Where(Function(a) ApptTheme.GetAppointmentCalendarDay(a) = dayDate.Date),
            data,
            orderFirstDoctorId:=ResolveWeekDayOrderDoctorId(state))
    End Function

    Private Sub OpenWeekDayDrawerDialog(dayDate As DateTime)
        If _request Is Nothing OrElse _request.Data Is Nothing OrElse _request.State Is Nothing Then Return
        Dim appts = BuildWeekDayAppointments(dayDate, _request.Data, _request.State)
        RequestOpenWeekDrawerDialog(dayDate.Date, appts, _request.Data, _request.State, _request)
    End Sub

    Private Sub ShowWeekDayDrawer(dayDate As DateTime, state As ApptState, data As ApptDataBundle)
        If _request Is Nothing OrElse data Is Nothing OrElse _dayDrawerHost Is Nothing OrElse _dimOverlay Is Nothing Then Return
        RestoreDialogOverlayParent()
        Dim appts = BuildWeekDayAppointments(dayDate, data, state)
        _dayDrawerHost.SuspendLayout()
        Try
            _dayDrawerHost.Visible = True
            _dayDrawerHost.Width = WeekDayDrawerWidthPx
            If WeekDayDrawerUseDimOverlay Then
                _dimOverlay.Visible = True
                _dimOverlay.BringToFront()
            Else
                If _dimOverlay IsNot Nothing Then _dimOverlay.Visible = False
            End If
            If WeekDayDrawerUseDimOverlay Then _dayDrawerHost.BringToFront()
            _weekRoot.PerformLayout()
            _workArea.PerformLayout()
            _dayDrawerHost.Populate(dayDate.Date, appts, data, state, _request, Me)
            LayoutWeekColumns()
        Catch ex As Exception
            ApptErrorHelper.Report(ex, "ApptWeekCtl.ShowWeekDayDrawer", showUser:=False)
        Finally
            _dayDrawerHost.ResumeLayout(True)
        End Try
        ApptErrorHelper.SafeInvalidate(_dayDrawerHost, "ApptWeekCtl.ShowWeekDayDrawer.InvalidateDrawer", invalidateChildren:=True)
        ApptErrorHelper.SafeFocus(_dayDrawerHost, "ApptWeekCtl.ShowWeekDayDrawer.FocusDrawer")
        BeginInvoke(New MethodInvoker(Sub()
                                          If _dayDrawerHost IsNot Nothing AndAlso _dayDrawerHost.Visible Then
                                              _dayDrawerHost.RelayoutAfterOpen()
                                          End If
                                      End Sub))
    End Sub

    ''' <summary>Wires hub, status menu, column drag/drop for cards hosted in the week day drawer.</summary>
    Friend Sub WireDrawerDayCard(card As ApptCard, dayDate As DateTime)
        PrepareReusableWeekCard(card, dayDate.Date)
    End Sub
    Private Sub PrepareDialogOverlayParent()
        Dim host = FindAncestorApptHost()
        If host IsNot Nothing Then
            _dialogExpandedHost = host
            _dialogExpandedHostTemporarily = Not host.IsBodyWorkspaceExpanded
            If _dialogExpandedHostTemporarily Then
                host.EnsureBodyWorkspaceExpanded()
            End If
        Else
            _dialogExpandedHost = Nothing
            _dialogExpandedHostTemporarily = False
        End If
        Dim overlayParent As Control = If(TryCast(host, Control), CType(_workArea, Control))
        MoveOverlayToParent(_dimOverlay, overlayParent)
        MoveOverlayToParent(_drawerDialogHost, overlayParent)
    End Sub

    Private Sub RestoreDialogOverlayParent()
        MoveOverlayToParent(_dimOverlay, _workArea)
        MoveOverlayToParent(_drawerDialogHost, _workArea)
        If _dialogExpandedHostTemporarily AndAlso _dialogExpandedHost IsNot Nothing Then
            _dialogExpandedHost.EnsureBodyWorkspaceCollapsed()
        End If
        _dialogExpandedHost = Nothing
        _dialogExpandedHostTemporarily = False
    End Sub

    Private Sub MoveOverlayToParent(overlay As Control, targetParent As Control)
        If overlay Is Nothing OrElse targetParent Is Nothing Then Return
        If overlay.Parent Is targetParent Then
            If overlay Is _drawerDialogHost Then
                AttachDialogOverlayParent(targetParent)
                overlay.Dock = DockStyle.None
                LayoutDialogOverlayHost()
            Else
                overlay.Dock = DockStyle.Fill
            End If
            Return
        End If
        If overlay.Parent IsNot Nothing Then
            overlay.Parent.Controls.Remove(overlay)
        End If
        targetParent.Controls.Add(overlay)
        If overlay Is _drawerDialogHost Then
            AttachDialogOverlayParent(targetParent)
            overlay.Dock = DockStyle.None
            LayoutDialogOverlayHost()
        Else
            overlay.Dock = DockStyle.Fill
        End If
        overlay.BringToFront()
    End Sub

    Private Sub AttachDialogOverlayParent(targetParent As Control)
        If Object.ReferenceEquals(_dialogOverlayParent, targetParent) Then Return
        If _dialogOverlayParent IsNot Nothing Then
            RemoveHandler _dialogOverlayParent.SizeChanged, AddressOf DialogOverlayParent_SizeChanged
        End If
        _dialogOverlayParent = targetParent
        If _dialogOverlayParent IsNot Nothing Then
            AddHandler _dialogOverlayParent.SizeChanged, AddressOf DialogOverlayParent_SizeChanged
        End If
    End Sub

    Private Sub DialogOverlayParent_SizeChanged(sender As Object, e As EventArgs)
        LayoutDialogOverlayHost()
    End Sub

    Private Sub LayoutDialogOverlayHost()
        If _drawerDialogHost Is Nothing OrElse _drawerDialogHost.Parent Is Nothing Then Return
        Dim parentClient = _drawerDialogHost.Parent.ClientSize
        Dim w = Math.Max(0, CInt(Math.Ceiling(parentClient.Width / 2.0R)))
        Dim x = Math.Max(0, (parentClient.Width - w) \ 2)
        _drawerDialogHost.Bounds = New Rectangle(x, 0, w, Math.Max(0, parentClient.Height))
        _drawerDialogHost.BringToFront()
    End Sub

    Private Function FindAncestorApptHost() As ApptHostCtl
        Dim current As Control = Me
        While current IsNot Nothing
            Dim host = TryCast(current, ApptHostCtl)
            If host IsNot Nothing Then Return host
            current = current.Parent
        End While
        Return Nothing
    End Function
End Class
