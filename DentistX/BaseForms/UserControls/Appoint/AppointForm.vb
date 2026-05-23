Imports DevExpress.Utils
Imports DevExpress.XtraGrid.Columns
Imports DevExpress.XtraGrid.Views.Grid.ViewInfo
Imports DevExpress.XtraScheduler
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraGrid.Views.Base

Imports System.Drawing
Public Class AppointForm
    Private repo As AppointmentCRepository
    Dim _repo As New AppointmentCRepository(DentistXDATA.GetConnection.ConnectionString)
    'Dim allAppts As List(Of AppointmentC) = _repo.GetAllAppointments().ToList()


#Region "New Code"

    Private allAppts As List(Of AppointmentC)
    Private displayList As New List(Of AppointmentView)

    Private Sub AppointForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        lblToday.Text = AppointDateFormat.FormatDayDate(Date.Today)
        ' Style the grid
        StyleGridView()

        ' Hook up filters and events
        AddHandler txtPatient.EditValueChanged, AddressOf FilterGrid
        AddHandler txtDoctor.EditValueChanged, AddressOf FilterGrid
        AddHandler chkAllAppts.CheckedChanged, AddressOf FilterGrid
        AddHandler dgView.DoubleClick, AddressOf dgView_DoubleClick

        ' Load appointments
        LoadAppointments()
    End Sub

    Private Sub LoadAppointments()
        ' === Fetch data from repository ===
        allAppts = _repo.GetAllAppointments().ToList()

        displayList = (From appt In allAppts
                       Let pName = _repo.GetPatientName(appt.PatientID)
                       Let dName = _repo.GetDoctorName(appt.DrID)
                       Select New AppointmentView With {
                           .AppID = appt.AppointmentID,
                           .ApptDate = AppointDateFormat.FormatDate(appt.AppDate),
                           .FromTo = $"{appt.StartDateTime:HH:mm} - {appt.EndDateTime:HH:mm}",
                           .PatientName = pName,
                           .DoctorName = dName,
                           .Detail = appt.Reason
                       }).ToList()
        If chkAllAppts.Checked = False Then
            displayList = displayList.Where(Function(a) Date.Parse(a.ApptDate).Date = Date.Today).ToList()
        End If
        gridResults.DataSource = displayList
    End Sub

    ' Lightweight view model matching grid columns
    Private Class AppointmentView
        Public Property AppID As Integer
        Public Property ApptDate As String
        Public Property FromTo As String
        Public Property PatientName As String
        Public Property DoctorName As String
        Public Property Detail As String
    End Class

    Private Sub StyleGridView()
        ' Alternating row colors
        dgView.OptionsView.EnableAppearanceEvenRow = True
        dgView.OptionsView.EnableAppearanceOddRow = True
        dgView.Appearance.EvenRow.BackColor = Color.FromArgb(230, 240, 255) ' 🔵 light blue
        dgView.Appearance.OddRow.BackColor = Color.FromArgb(255, 250, 230)  ' 🟠 light orange
        dgView.Appearance.Row.Font = New Font("Calibri", 10, FontStyle.Regular)

        ' Colorful headers 🟢🔵🟣🟠
        Dim headerColors() As Color = {
            Color.MediumSeaGreen, '🟢
            Color.DodgerBlue,     '🔵
            Color.MediumPurple,   '🟣
            Color.Orange          '🟠
        }

        Dim i As Integer = 0
        For Each col As GridColumn In dgView.Columns
            col.AppearanceHeader.BackColor = headerColors(i Mod headerColors.Length)
            col.AppearanceHeader.ForeColor = Color.White
            col.AppearanceHeader.Font = New Font("Calibri", 10, FontStyle.Bold)
            col.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center
            i += 1
        Next

        dgView.OptionsBehavior.Editable = False
        dgView.OptionsView.ShowGroupPanel = False
        dgView.OptionsView.ShowIndicator = False
    End Sub

    Private Sub FilterGrid(sender As Object, e As EventArgs)
        If displayList Is Nothing Then Return

        Dim patientFilter As String = txtPatient.Text.Trim().ToLower()
        Dim doctorFilter As String = txtDoctor.Text.Trim().ToLower()
        Dim todayOnly As Boolean = Not chkAllAppts.Checked

        Dim filtered = displayList.Where(Function(a)
                                             Dim match = True
                                             If patientFilter <> "" Then
                                                 match = match AndAlso a.PatientName.ToLower().Contains(patientFilter)
                                             End If
                                             If doctorFilter <> "" Then
                                                 match = match AndAlso a.DoctorName.ToLower().Contains(doctorFilter)
                                             End If
                                             If todayOnly Then
                                                 match = match AndAlso (Date.Parse(a.ApptDate).Date = Date.Today)
                                             End If
                                             Return match
                                         End Function).ToList()

        gridResults.DataSource = filtered
    End Sub

    Private Sub dgView_DoubleClick(sender As Object, e As EventArgs)
        Dim view As GridView = CType(sender, GridView)
        Dim hitInfo As GridHitInfo = view.CalcHitInfo(view.GridControl.PointToClient(Control.MousePosition))
        If hitInfo.InRow OrElse hitInfo.InRowCell Then
            Dim rowHandle As Integer = hitInfo.RowHandle
            If rowHandle >= 0 Then
                Dim appt As AppointmentView = CType(view.GetRow(rowHandle), AppointmentView)
                MessageBox.Show($"Patient: {appt.PatientName}" & vbCrLf &
                                $"Doctor: {appt.DoctorName}" & vbCrLf &
                                $"Date: {appt.ApptDate}" & vbCrLf &
                                $"Time: {appt.FromTo}" & vbCrLf &
                                $"Reason: {appt.Detail}",
                                "Appointment Details", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Dim apptc As AppointmentC = allAppts.FirstOrDefault(Function(a) a.AppointmentID = appt.AppID)
                Dim editor As New AppointCEditorForm(apptc, False)
                editor.ShowDialog(Me)
            End If
        End If
    End Sub


    Private Sub dgView_CustomUnboundColumnData(sender As Object, e As CustomColumnDataEventArgs) Handles dgView.CustomUnboundColumnData
        If e.Column.Name = "colRowNum" Then
            e.Value = e.ListSourceRowIndex + 1
        End If
    End Sub


#End Region


    Private Sub OnApptClicked(s As Object, e As AppointmentEventArgs)
        MessageBox.Show($"Clicked appointment ID={e.Appointment.AppointmentID} PatientID={e.Appointment.PatientID}")
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

    End Sub

    Private Sub chkAllAppts_CheckedChanged(sender As Object, e As EventArgs) Handles chkAllAppts.CheckedChanged
        If chkAllAppts.Checked Then
            chkAllAppts.Text = "Show today only."
            LoadAppointments()
        Else
            chkAllAppts.Text = "Show all appointments."
            LoadAppointments()
        End If
    End Sub
End Class