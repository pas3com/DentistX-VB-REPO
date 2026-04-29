Imports System.Data.SqlClient
Imports Dapper
Imports DevExpress.XtraScheduler
Imports DevExpress.XtraScheduler.Drawing
Imports DevExpress.XtraScheduler.Native

Public Class ApptPage



    Private ReadOnly _dbHelper As New DashDataModel.DatabaseHelper()
    Private ReadOnly _connectionString As String = DashDataModel.DatabaseHelper._connectionString
    Private _currentFilter As New DashboardFilter()
    Private _appointments As List(Of ApptDash)
    Private ReadOnly _doctorNameToId As New Dictionary(Of String, Integer)(StringComparer.OrdinalIgnoreCase)
    Private ReadOnly _doctorIdToName As New Dictionary(Of Integer, String)
    Private ReadOnly _statusDisplayToValue As New Dictionary(Of String, String)(StringComparer.OrdinalIgnoreCase)
    Private _isSyncingFilters As Boolean = False
    Private _colorsConfigured As Boolean = False
    Private _initialized As Boolean = False

    Public Sub New(_Filter As DashboardFilter)
        InitializeComponent()
        _currentFilter = If(_Filter, New DashboardFilter())
        Me.Dock = DockStyle.Fill
    End Sub

    Protected Overrides Sub OnLoad(e As EventArgs)
        MyBase.OnLoad(e)
        If _initialized Then Return
        _initialized = True
        InitializeFilters()
        LoadAppointments(_currentFilter)
        SetupScheduler()
    End Sub

    Public Sub LoadAppointments(_currentFilter As DashboardFilter)
        _appointments = _dbHelper.GetAppointments(_currentFilter)
        'SchedulerStorage1.Appointments.DataSource = _appointments
        ApptDashBindingSource.DataSource = _appointments
        ' Color code appointments by status
        ConfigureAppointmentColors()
        ConfigureAppointmentStorage()
        SchedulerControl1.RefreshData()
    End Sub

    Private Sub ConfigureAppointmentStorage()
        '' Map fields to scheduler properties
        'SchedulerStorage1.Appointments.Mappings.AppointmentId = "AppointmentID"
        'SchedulerStorage1.Appointments.Mappings.Start = "StartDateTime"
        'SchedulerStorage1.Appointments.Mappings.End = "EndDateTime"
        'SchedulerStorage1.Appointments.Mappings.Subject = "PatientName"
        'SchedulerStorage1.Appointments.Mappings.Description = "Reason"
        'SchedulerStorage1.Appointments.Mappings.Status = "Status"
        'SchedulerStorage1.Appointments.Mappings.Label = "DoctorName"
        'SchedulerStorage1.Appointments.Mappings.Location = "DrID"
        'SchedulerStorage1.Appointments.Mappings.Type = "PatientID"
        'SchedulerStorage1.Appointments.Mappings.ReminderInfo = "Notes"
        'SchedulerStorage1.Appointments.Mappings.TimeZoneId = ""
        'SchedulerStorage1.Appointments.Mappings.ResourceId = "CreatedBy"
        '' Create custom fields for patient info
        ''schedulerStorage.Appointments.CustomFieldMappings.Add(New AppointmentCustomFieldMapping("Patient_ID", "PatientID"))
        ''schedulerStorage.Appointments.CustomFieldMappings.Add(New AppointmentCustomFieldMapping("PatientPhone", "Phone"))


        SchedulerStorage1.Appointments.DataSource = _appointments
    End Sub

    Private Sub SetupScheduler()

        ' Configure scheduler appearance
        SchedulerControl1.ActiveViewType = DevExpress.XtraScheduler.SchedulerViewType.Day
        SchedulerControl1.Views.DayView.ResourcesPerPage = 1
        SchedulerControl1.Views.DayView.ShowAllAppointmentsAtTimeCells = True

        ' Configure time scale
        SchedulerControl1.Views.DayView.TimeScale = New TimeSpan(0, 30, 0) ' 30-minute intervals

        '' Add resources (doctors)
        ConfigureSchedulerResources()

        ' Configure appointment form showing
        SchedulerControl1.OptionsCustomization.AllowAppointmentCreate = DevExpress.XtraScheduler.UsedAppointmentType.All
        SchedulerControl1.OptionsCustomization.AllowAppointmentEdit = DevExpress.XtraScheduler.UsedAppointmentType.All
        SchedulerControl1.OptionsCustomization.AllowAppointmentDelete = DevExpress.XtraScheduler.UsedAppointmentType.All
    End Sub

    Private Sub ConfigureSchedulerResources()
        ' Clear existing resources
        SchedulerStorage1.Resources.Clear()
        ' Add doctors as resources
        Using conn As New SqlConnection(_connectionString)
            conn.Open()
            Dim sql = "SELECT DrID, DrName FROM Doctors  ORDER BY DrName" 'WHERE IsActive = 1 [DrColor]

            Using cmd As New SqlCommand(sql, conn)
                Using reader As SqlDataReader = cmd.ExecuteReader()
                    While reader.Read()
                        Dim doctorID As Object = reader("DrID")
                        Dim doctorName As String = reader("DrName").ToString()
                        ' Create resource with proper constructor
                        ' Create resource using the collection's CreateResource method
                        Dim resource As DevExpress.XtraScheduler.Resource = SchedulerStorage1.Resources.CreateResource(doctorID, doctorName)
                        resource.Caption = doctorName
                        ' Assign color
                        resource.SetColor(GetDoctorColor(doctorID))
                        ' Add to storage
                        SchedulerStorage1.Resources.Add(resource)
                    End While
                End Using
            End Using
        End Using
    End Sub

    Private Function GetDoctorColor(doctorIndex As Integer) As Color
        Return ColorTranslator.FromHtml(GetDrColor(doctorIndex))
    End Function
    Public Function GetDrColor(drId As Integer) As String
        Using conn As New SqlConnection(_connectionString)
            Return conn.ExecuteScalar(Of String)("SELECT DrColor FROM Doctors WHERE DrID=@ID", New With {.ID = drId})
        End Using
    End Function

    Private Sub ConfigureAppointmentColors()
        If _colorsConfigured Then Return
        ' Use AppointmentViewInfoCustomizing for better control
        AddHandler SchedulerControl1.AppointmentViewInfoCustomizing, AddressOf SchedulerControl_AppointmentViewInfoCustomizing1
        _colorsConfigured = True
    End Sub

    Private Sub SchedulerControl_AppointmentViewInfoCustomizing1(sender As Object, e As AppointmentViewInfoCustomizingEventArgs)
        Dim info As AppointmentViewInfo = e.ViewInfo

        If info.Appointment IsNot Nothing Then
            ' Color based on status
            Select Case info.Appointment.StatusKey.ToString()
                Case "Completed"
                    info.Appearance.BackColor = Color.LightGreen
                    info.Appearance.ForeColor = Color.DarkGreen
                    info.Appearance.BorderColor = Color.Green
                Case "Canceled"
                    info.Appearance.BackColor = Color.LightGray
                    info.Appearance.ForeColor = Color.Gray
                    info.Appearance.BorderColor = Color.DarkGray
                Case "Running"
                    info.Appearance.BackColor = Color.LightCoral
                    info.Appearance.ForeColor = Color.DarkRed
                    info.Appearance.BorderColor = Color.Red
                Case "Postponed" '"Pending"
                    info.Appearance.BackColor = Color.LightBlue
                    info.Appearance.ForeColor = Color.DarkBlue
                    info.Appearance.BorderColor = Color.Blue
                Case "Pending" '"Pending"
                    info.Appearance.BackColor = Color.LightYellow
                    info.Appearance.ForeColor = Color.DarkBlue
                    info.Appearance.BorderColor = Color.Blue
                Case Else
                    info.Appearance.BackColor = Color.LightSkyBlue
                    info.Appearance.ForeColor = Color.DarkBlue
                    info.Appearance.BorderColor = Color.SteelBlue
            End Select



            ' Optional: Add status text to appointment
            If info.Appointment.StatusKey IsNot Nothing Then
                info.ShowBell = False
                'info.Description = $"Status: {info.Appointment.StatusKey}"
            End If
        End If
    End Sub

    Private Sub SchedulerControl_AppointmentViewInfoCustomizing(
        sender As Object,
        e As AppointmentViewInfoCustomizingEventArgs)

        ' Customize appointment display
        Dim apt As Appointment = e.ViewInfo.Appointment
        If apt.CustomFields("Patient_ID") IsNot Nothing Then
            ' Add patient info to tooltip
            e.ViewInfo.ToolTipText = $"Patient: {apt.Subject}{vbCrLf}Reason: {apt.Description}{vbCrLf}Phone: {apt.CustomFields("PatientPhone")}"
        End If

        If apt.Type <> "" Then
            ' Add patient info to tooltip
            e.ViewInfo.ToolTipText = $"Patient: {apt.Subject}{vbCrLf}Reason: {apt.Description}{vbCrLf}Phone: {apt.CustomFields("PatientPhone")}"
        End If
    End Sub

    Public Sub RefreshData()
        LoadAppointments(_currentFilter)
    End Sub

    Private Sub cboStatus_EditValueChanged(sender As Object, e As EventArgs) Handles cboStatus.EditValueChanged
        If _isSyncingFilters Then Return
        ApplyFilters()
    End Sub

    Private Sub cboDoctor_EditValueChanged(sender As Object, e As EventArgs) Handles cboDoctor.EditValueChanged
        If _isSyncingFilters Then Return
        SyncDoctorFromCombo()
        ApplyFilters()
    End Sub

    Private Sub LoadDoctorsIntoRibbonCombo()

        cboDoctorRepo.Items.Clear()
        _doctorNameToId.Clear()
        _doctorIdToName.Clear()

        Using conn As New SqlConnection(_connectionString)
            conn.Open()

            Dim sql As String = "
            SELECT DrID, DrName
            FROM dbo.Doctors
            ORDER BY DrName
        "

            Using cmd As New SqlCommand(sql, conn)
                Using rdr As SqlDataReader = cmd.ExecuteReader()
                    cboDoctorRepo.Items.Add(If(Eng, "[All Doctors]", "[كل الأطباء]"))
                    While rdr.Read()
                        Dim drId = Convert.ToInt32(rdr("DrID"))
                        Dim drName = rdr("DrName").ToString()
                        cboDoctorRepo.Items.Add(drName)
                        _doctorNameToId(drName) = drId
                        _doctorIdToName(drId) = drName
                    End While

                End Using
            End Using
        End Using

        cboDoctor.EditValue = If(Eng, "[All Doctors]", "[كل الأطباء]")
    End Sub

    Private Sub LoadDoctorsIntoRibbonLookup()

        Using conn As New SqlConnection(_connectionString)
            conn.Open()

            Dim dt As New DataTable()
            Using da As New SqlDataAdapter("
            SELECT DrID, DrName
            FROM dbo.Doctors
            ORDER BY DrName", conn)
                da.Fill(dt)
            End Using

            Dim allRow = dt.NewRow()
            allRow("DrID") = 0
            allRow("DrName") = If(Eng, "[All Doctors]", "[كل الأطباء]")
            dt.Rows.InsertAt(allRow, 0)

            cboDoctorRepoLook.DataSource = dt
            cboDoctorRepoLook.DisplayMember = "DrName"
            cboDoctorRepoLook.ValueMember = "DrID"
            cboDoctorRepoLook.NullText = If(Eng, "[All Doctors]", "[كل الأطباء]")
        End Using

        lookDoctors.EditValue = 0
    End Sub

    Private Sub lookDoctors_EditValueChanged(sender As Object, e As EventArgs) Handles lookDoctors.EditValueChanged
        If _isSyncingFilters Then Return
        SyncDoctorFromLookup()
        ApplyFilters()
    End Sub

    Private Sub InitializeFilters()
        LoadDoctorsIntoRibbonCombo()
        LoadDoctorsIntoRibbonLookup()
        LoadStatusIntoRibbonCombo()
    End Sub

    Private Sub LoadStatusIntoRibbonCombo()
        cboStatusRepo.Items.Clear()
        _statusDisplayToValue.Clear()

        AddStatusItem(If(Eng, "All", "الكل"), Nothing)
        AddStatusItem(If(Eng, "Scheduled", "مجدول"), "Scheduled")
        AddStatusItem(If(Eng, "Completed", "مكتمل"), "Completed")
        AddStatusItem(If(Eng, "Canceled", "ملغي"), "Canceled")
        AddStatusItem(If(Eng, "No Show", "لم يحضر"), "No Show")
        AddStatusItem(If(Eng, "Running", "جاري"), "Running")
        AddStatusItem(If(Eng, "Postponed", "مؤجل"), "Postponed")
        AddStatusItem(If(Eng, "Pending", "قيد الانتظار"), "Pending")

        cboStatus.EditValue = If(Eng, "All", "الكل")
    End Sub

    Private Sub AddStatusItem(displayText As String, statusValue As String)
        cboStatusRepo.Items.Add(displayText)
        _statusDisplayToValue(displayText) = statusValue
    End Sub

    Private Sub ApplyFilters()
        _currentFilter.DoctorID = GetSelectedDoctorId()
        _currentFilter.Status = GetSelectedStatus()
        LoadAppointments(_currentFilter)
    End Sub

    Private Function GetSelectedDoctorId() As Integer?
        If lookDoctors.EditValue Is Nothing Then Return Nothing
        Dim drId = Convert.ToInt32(lookDoctors.EditValue)
        Return If(drId = 0, Nothing, drId)
    End Function

    Private Function GetSelectedStatus() As String
        Dim displayText = TryCast(cboStatus.EditValue, String)
        If String.IsNullOrWhiteSpace(displayText) Then Return Nothing
        If _statusDisplayToValue.ContainsKey(displayText) Then
            Return _statusDisplayToValue(displayText)
        End If
        Return Nothing
    End Function

    Private Sub SyncDoctorFromCombo()
        Dim displayText = TryCast(cboDoctor.EditValue, String)
        If String.IsNullOrWhiteSpace(displayText) Then Return

        _isSyncingFilters = True
        If displayText = If(Eng, "[All Doctors]", "[كل الأطباء]") Then
            lookDoctors.EditValue = 0
        ElseIf _doctorNameToId.ContainsKey(displayText) Then
            lookDoctors.EditValue = _doctorNameToId(displayText)
        End If
        _isSyncingFilters = False
    End Sub

    Private Sub SyncDoctorFromLookup()
        Dim drId = GetSelectedDoctorId()
        _isSyncingFilters = True
        If Not drId.HasValue Then
            cboDoctor.EditValue = If(Eng, "[All Doctors]", "[كل الأطباء]")
        ElseIf _doctorIdToName.ContainsKey(drId.Value) Then
            cboDoctor.EditValue = _doctorIdToName(drId.Value)
        End If
        _isSyncingFilters = False
    End Sub
End Class
