Imports System.Drawing
Imports System.Windows.Forms
Imports DevExpress.Pdf.Native.BouncyCastle.Utilities.IO.Pem
Imports DevExpress.XtraScheduler

Public Class FrmSchedulerFull

    Private Const WM_EXITSIZEMOVE As Integer = &H232

    Private _schedulerStartupLayoutApplied As Boolean
    Private _schedulerClampInProgress As Boolean
    Private _lastSchedulerWindowState As FormWindowState = FormWindowState.Normal




    Private repo As AppointmentCRepository
    'Private schedule As WeekSched
    Private schedule As ApptHostCtl

    Public Sub New()
        InitializeComponent()
        pnlBody.Dock = DockStyle.Fill
        repo = New AppointmentCRepository(DentistXDATA.GetConnection.ConnectionString)
        ' Legacy wiring kept for reference while `FrmSchedulerFull` moves to the modular scheduler host.
        'schedule = New WeekSched(repo) With {.Dock = DockStyle.Fill, .AllowAddWithoutCurrentPatient = True}
        schedule = New ApptHostCtl(repo) With {.Dock = DockStyle.Fill, .AllowAddWithoutCurrentPatient = True}
        Me.pnlBody.Controls.Add(schedule)
    End Sub

    Protected Overrides Sub OnShown(e As EventArgs)
        MyBase.OnShown(e)
        If Not _schedulerStartupLayoutApplied AndAlso IsHandleCreated Then
            BeginInvoke(New MethodInvoker(AddressOf ApplySchedulerStartupLayout))
        End If
    End Sub

    Private Sub ApplySchedulerStartupLayout()
        If _schedulerStartupLayoutApplied OrElse IsDisposed Then Return
        _schedulerStartupLayoutApplied = True

        Dim workingArea = GetSchedulerWorkingArea()
        UpdateSchedulerWorkingArea()

        If WindowState = FormWindowState.Normal Then
            Dim startupBounds = Bounds
            If startupBounds.Width <= 0 OrElse startupBounds.Height <= 0 Then
                startupBounds = New Rectangle(workingArea.Location,
                                              New Size(Math.Min(1280, workingArea.Width),
                                                       Math.Min(900, workingArea.Height)))
            End If

            startupBounds = ClampRectangleToWorkingArea(startupBounds, workingArea, MinimumSize)
            startupBounds.X = workingArea.Left + Math.Max(0, (workingArea.Width - startupBounds.Width) \ 2)
            startupBounds.Y = workingArea.Top + Math.Max(0, (workingArea.Height - startupBounds.Height) \ 2)
            Bounds = startupBounds
        End If

        WindowState = FormWindowState.Maximized
        _lastSchedulerWindowState = WindowState
    End Sub

    Private Sub UpdateSchedulerWorkingArea()
        If IsDisposed Then Return
        MaximizedBounds = GetSchedulerWorkingArea()
    End Sub

    Private Sub ClampSchedulerToWorkingArea()
        If _schedulerClampInProgress OrElse IsDisposed OrElse WindowState <> FormWindowState.Normal Then Return

        _schedulerClampInProgress = True
        Try
            Dim workingArea = GetSchedulerWorkingArea()
            MaximizedBounds = workingArea
            Dim clamped = ClampRectangleToWorkingArea(Bounds, workingArea, MinimumSize)
            If Bounds <> clamped Then
                Bounds = clamped
            End If
        Finally
            _schedulerClampInProgress = False
        End Try
    End Sub

    Private Function GetSchedulerWorkingArea() As Rectangle
        Dim referenceBounds = If(WindowState = FormWindowState.Normal AndAlso Bounds.Width > 0 AndAlso Bounds.Height > 0,
                                 Bounds,
                                 RestoreBounds)
        If referenceBounds.Width <= 0 OrElse referenceBounds.Height <= 0 Then
            referenceBounds = New Rectangle(New Point(Math.Max(0, MousePosition.X), Math.Max(0, MousePosition.Y)), New Size(1, 1))
        End If
        Return Screen.FromRectangle(referenceBounds).WorkingArea
    End Function

    Private Shared Function ClampRectangleToWorkingArea(bounds As Rectangle, workingArea As Rectangle, minimumSize As Size) As Rectangle
        Dim minWidth = Math.Max(320, minimumSize.Width)
        Dim minHeight = Math.Max(240, minimumSize.Height)
        Dim width = Math.Max(minWidth, Math.Min(bounds.Width, workingArea.Width))
        Dim height = Math.Max(minHeight, Math.Min(bounds.Height, workingArea.Height))
        Dim x = bounds.X
        Dim y = bounds.Y

        If x < workingArea.Left Then x = workingArea.Left
        If y < workingArea.Top Then y = workingArea.Top
        If x + width > workingArea.Right Then x = workingArea.Right - width
        If y + height > workingArea.Bottom Then y = workingArea.Bottom - height
        If x < workingArea.Left Then x = workingArea.Left
        If y < workingArea.Top Then y = workingArea.Top

        Return New Rectangle(x, y, width, height)
    End Function

    Private Sub FrmSchedulerFull_Move(sender As Object, e As EventArgs) Handles MyBase.Move
        If _schedulerClampInProgress Then Return
        UpdateSchedulerWorkingArea()
    End Sub

    Protected Overrides Sub WndProc(ByRef m As Message)
        MyBase.WndProc(m)
        If m.Msg = WM_EXITSIZEMOVE AndAlso WindowState = FormWindowState.Normal AndAlso IsHandleCreated Then
            BeginInvoke(New MethodInvoker(AddressOf ClampSchedulerToWorkingArea))
        End If
    End Sub

    Protected Overrides Sub OnFormClosed(e As FormClosedEventArgs)
        Try
            ApptErrorHelper.ReportDiagnostic(
                "FrmSchedulerFull.OnFormClosed",
                $"before-close-cleanup; scheduleAlive={schedule IsNot Nothing AndAlso Not schedule.IsDisposed}; bodyChildren={If(pnlBody Is Nothing, -1, pnlBody.Controls.Count)}")
        Catch
        End Try

        Try
            If schedule IsNot Nothing Then
                If schedule.Parent IsNot Nothing Then
                    schedule.Parent.Controls.Remove(schedule)
                End If
                If Not schedule.IsDisposed Then
                    schedule.Dispose()
                End If
                schedule = Nothing
            End If
        Catch ex As Exception
            ApptErrorHelper.Report(ex, "FrmSchedulerFull.OnFormClosed.ScheduleDispose", showUser:=False)
        End Try

        Try
            If pnlBody IsNot Nothing Then pnlBody.Controls.Clear()
            ApptErrorHelper.ReportDiagnostic(
                "FrmSchedulerFull.OnFormClosed",
                $"after-close-cleanup; scheduleAlive={schedule IsNot Nothing}; bodyChildren={If(pnlBody Is Nothing, -1, pnlBody.Controls.Count)}")
        Catch ex As Exception
            ApptErrorHelper.Report(ex, "FrmSchedulerFull.OnFormClosed.ClearBody", showUser:=False)
        End Try

        MyBase.OnFormClosed(e)
    End Sub


    ''' <summary>Working-area clamp only — proportional bounds cache was never populated (StoreOriginalBounds commented); layout is Dock=FILL ApptHostCtl.</summary>
    Private Sub FrmSchedulerFull_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        UpdateSchedulerWorkingArea()
        If WindowState <> _lastSchedulerWindowState Then
            _lastSchedulerWindowState = WindowState
            If WindowState = FormWindowState.Normal AndAlso IsHandleCreated Then
                BeginInvoke(New MethodInvoker(AddressOf ClampSchedulerToWorkingArea))
            End If
        End If
    End Sub




    Private Sub OnApptClicked(s As Object, e As AppointmentEventArgs)
        MessageBox.Show($"Clicked appointment ID={e.Appointment.AppointmentID} PatientID={e.Appointment.PatientID}")
    End Sub

    Private Sub OnApptDoubleClicked(s As Object, e As AppointmentEventArgs)
        ' open editor dialog, then UpdateAppointment(...)
        Dim editor As New AppointCEditorForm(e.Appointment) ' implement editor as earlier
        If editor.ShowDialog() = DialogResult.OK Then
            schedule.UpdateAppointment(editor.Appointment)
        End If
    End Sub

    Private Sub OnBackgroundClicked(s As Object, e As DateEventArgs)
        ' create new appointment on clicked date (fill sensible times)
        Dim newAppt As New AppointmentC With {
            .PatientID = 1,
            .DrID = 1,
            .StartDateTime = e.ClickedDate.Date.AddHours(9),
            .EndDateTime = e.ClickedDate.Date.AddHours(9).AddMinutes(30),
            .Reason = "New",
            .Notes = "",
            .CreatedBy = Environment.UserName,
            .CreatedAt = DateTime.Now
        }
        Dim editor As New AppointCEditorForm(newAppt)
        If editor.ShowDialog() = DialogResult.OK Then
            schedule.AddAppointment(editor.Appointment)
        End If
    End Sub

    ' Example add button
    Private Sub btnAdd_Click(sender As Object, e As EventArgs)
        Dim newAppt As New AppointmentC With {
            .PatientID = 1,
            .DrID = 1,
            .StartDateTime = DateTime.Today.AddHours(9),
            .EndDateTime = DateTime.Today.AddHours(9).AddMinutes(30),
            .Reason = "Checkup",
            .Notes = "",
            .CreatedBy = Environment.UserName,
            .CreatedAt = DateTime.Now
        }
        Dim dlg = New AppointCEditorForm(newAppt, True)
        If dlg.ShowDialog() = DialogResult.OK Then
            schedule.AddAppointment(dlg.AppointmentC)
        End If
    End Sub
End Class