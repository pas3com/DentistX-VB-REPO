Imports System.Collections.Concurrent
Imports DevExpress.Pdf.Native.BouncyCastle.Utilities.IO.Pem
Imports DevExpress.XtraScheduler

Public Class FrmSchedulerFull






    Private repo As AppointmentCRepository
    'Private schedule As SchedulerNew
    Private schedule As ApptHostCtl

    Public Sub New()
        InitializeComponent()
        pnlBody.Dock = DockStyle.Fill
        repo = New AppointmentCRepository(DentistXDATA.GetConnection.ConnectionString)
        ' Legacy wiring kept for reference while `FrmSchedulerFull` moves to the modular scheduler host.
        'schedule = New SchedulerNew(repo) With {.Dock = DockStyle.Fill, .AllowAddWithoutCurrentPatient = True}
        schedule = New ApptHostCtl(repo) With {.Dock = DockStyle.Fill, .AllowAddWithoutCurrentPatient = True}
        AddHandler schedule.AppointmentDoubleClicked, AddressOf OnAppointmentDoubleClicked
        Me.pnlBody.Controls.Add(schedule)
        'StoreOriginalBounds(Me)
    End Sub



#Region "Resize"
    Private ReadOnly controlBoundsCache As New ConcurrentDictionary(Of Control, Rectangle)
    Private Const OriginalPanelWidth As Integer = 1366 ', 720
    Private Const OriginalPanelHeight As Integer = 768
    Private ReadOnly originalPanelSize As New Size(OriginalPanelWidth, OriginalPanelHeight)

    Private Sub StoreOriginalBounds(container As Control)
        Dim sw As New Stopwatch
        sw.Start()

        For Each ctrl As Control In container.Controls
            controlBoundsCache.TryAdd(ctrl, ctrl.Bounds)
            If ctrl.HasChildren Then StoreOriginalBounds(ctrl)
        Next
        sw.Stop()

    End Sub
    Private Sub ResizeControlsProportionally()
        If controlBoundsCache.IsEmpty Then Return
        Dim sw As New Stopwatch
        sw.Start()
        Dim widthRatio = CSng(Me.Width) / OriginalPanelWidth
        Dim heightRatio = CSng(Me.Height) / OriginalPanelHeight

        For Each kvp In controlBoundsCache
            kvp.Key.SetBounds(
            CInt(kvp.Value.X * widthRatio),
            CInt(kvp.Value.Y * heightRatio),
            CInt(kvp.Value.Width * widthRatio),
            CInt(kvp.Value.Height * heightRatio))
        Next

        sw.Stop()
    End Sub
    Private Sub ScheduleAdmin_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        If controlBoundsCache.IsEmpty Then Return
        ResizeControlsProportionally()

    End Sub
#End Region




    Private Sub OnAppointmentDoubleClicked(appt As AppointmentC)
        Dim dlg As New AppointCEditorForm(appt, False)
        If dlg.ShowDialog(Me) = DialogResult.OK Then
            schedule.UpdateAppointment(dlg.AppointmentC)
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