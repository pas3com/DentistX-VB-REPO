
Imports System.Collections.Concurrent
Imports System.Data.SqlClient
Imports System.Text
Imports DevExpress.XtraGrid.Views.Base
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraGrid.Views.Grid.ViewInfo
Imports DevExpress.XtraScheduler
Imports DevExpress.XtraScheduler.Drawing

Public Class TodayApptEditorForm

    Private repo As New AppointmentCRepository
    Private _repo As New AppointmentCRepository(DentistXDATA.GetConnection.ConnectionString)
    Private allAppts As List(Of AppointmentC)
    Private displayList As New List(Of AppointmentView)
    Private _isNew As Boolean
    Public Property Appointment As AppointmentC
    Public Property AppointmentC As AppointmentC
    Public Sub New()

        InitializeComponent()
        RemoveHandler apptDate.EditValueChanged, AddressOf apptDate_EditValueChanged
        apptDate.DateTime = Date.Now
        StoreOriginalBounds(Me)
        LoadValues()
    End Sub
    Private ReadOnly controlBoundsCache As New ConcurrentDictionary(Of Control, Rectangle)
    Private Const OriginalPanelWidth As Integer = 1214
    Private Const OriginalPanelHeight As Integer = 648
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
    Private Sub TodayApptEditorForm_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        If controlBoundsCache.IsEmpty Then Return
        ResizeControlsProportionally()
    End Sub


    Private Sub LoadValues()

        LoadAppointments(apptDate.DateTime)
        AddHandler apptDate.EditValueChanged, AddressOf apptDate_EditValueChanged
    End Sub

    Private Sub LoadAppointments(Optional appDate As DateTime = Nothing)
        ' === Fetch data from repository ===
        allAppts = _repo.GetAllAppointments().ToList()

        Dim today As Date = Date.Today

        ' === Auto-update statuses ===
        For Each appt In allAppts

            ' Pending → Running (if today)
            If appt.Status = "Pending" AndAlso appt.AppDate.Date = today Then
                appt.Status = "Running"
                _repo.UpdateAppointmentStatus(appt.AppointmentID, "Running")
            End If

            ' Running → Completed (if old)
            If appt.Status = "Running" AndAlso appt.AppDate.Date < today Then
                appt.Status = "Completed"
                _repo.UpdateAppointmentStatus(appt.AppointmentID, "Completed")
            End If

        Next
        ' --- Apply date filter if passed ---
        If appDate <> Nothing Then
            allAppts = allAppts.Where(Function(a) a.AppDate.Date = appDate.Date).ToList()
        End If

        ' --- Build display list ---

        displayList = (From appt In allAppts
                       Let pName = _repo.GetPatientName(appt.PatientID)
                       Let dName = _repo.GetDoctorName(appt.DrID)
                       Select New AppointmentView With {
                           .AppID = appt.AppointmentID,
                           .ApptDate = appt.AppDate.ToShortDateString(),
                           .FromTo = $"{appt.StartDateTime:HH:mm} - {appt.EndDateTime:HH:mm}",
                           .PatientName = pName,
                           .DoctorName = dName,
                           .Detail = appt.Reason,
                            .Status = appt.Status
                       }).ToList()

        gridResults.DataSource = displayList
    End Sub

    Private Sub BtnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Using conn As SqlConnection = DentistXDATA.GetConnection
            ' convert the current AppointmentView to AppointmentC from current row in the grid
            Dim selectedRow = dgView.GetFocusedRow()
            'convert the selected row to AppointmentView
            Dim apptView As AppointmentView = TryCast(selectedRow, AppointmentView)
            Dim View As GridView = dgView
            Dim rowHandle As Integer = View.LocateByValue("AppID", Me.AppointmentC.AppointmentID)
            Dim appt As AppointmentView = CType(View.GetRow(rowHandle), AppointmentView)
            If apptView Is Nothing Then

                MessageBox.Show("No appointment selected.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If
            'convert apptView to appointmentC
            Dim appID As Integer = apptView.AppID

            Me.AppointmentC = repo.GetApptById(appID)

            ' Check for overlaps with detailed information
            Dim overlappingAppointments = repo.GetOverlappingAppointments(conn, AppointmentC.DrID, AppointmentC.PatientID, AppointmentC.StartDateTime, AppointmentC.EndDateTime, AppointmentC.AppointmentID)

            If overlappingAppointments.Any() Then
                Dim message As New StringBuilder()
                message.AppendLine("Appointment overlaps detected:")
                message.AppendLine()

                For Each overlap In overlappingAppointments
                    message.AppendLine($"• {overlap}")
                Next

                message.AppendLine()
                message.AppendLine("Please choose a different time or doctor.")

                MessageBox.Show(message.ToString(), "Overlap Detected",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If

            ' ... proceed with INSERT or UPDATE normally ...
            Me.DialogResult = DialogResult.OK

        End Using
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs)
        Me.DialogResult = DialogResult.Cancel
    End Sub

    Private Sub apptDate_EditValueChanged(sender As Object, e As EventArgs) Handles apptDate.EditValueChanged
        LoadAppointments(apptDate.DateTime)
    End Sub

    Private Sub dgView_DoubleClick(sender As Object, e As EventArgs) Handles dgView.DoubleClick
        Dim view As GridView = CType(sender, GridView)
        Dim hitInfo As GridHitInfo = view.CalcHitInfo(view.GridControl.PointToClient(Control.MousePosition))
        If hitInfo.InRow OrElse hitInfo.InRowCell Then
            Dim rowHandle As Integer = hitInfo.RowHandle
            If rowHandle >= 0 Then
                Dim appt As AppointmentView = CType(view.GetRow(rowHandle), AppointmentView)
                'MessageBox.Show($"Patient: {appt.PatientName}" & vbCrLf &
                '                $"Doctor: {appt.DoctorName}" & vbCrLf &
                '                $"Date: {appt.ApptDate}" & vbCrLf &
                '                $"Time: {appt.FromTo}" & vbCrLf &
                '                $"Reason: {appt.Detail}",
                '                "Appointment Details", MessageBoxButtons.OK, MessageBoxIcon.Information)
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
    Private Sub dgView_RowCellStyle(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs) Handles dgView.RowCellStyle
        If e.Column.FieldName = "Status" OrElse e.Column.Name = "colStatus" Then
            Dim view = TryCast(sender, DevExpress.XtraGrid.Views.Grid.GridView)
            If view Is Nothing Then Exit Sub

            Dim statObj = view.GetRowCellValue(e.RowHandle, "Status")
            If statObj IsNot Nothing AndAlso Not IsDBNull(statObj) Then
                Dim appStatus As String = CStr(statObj)

                ' Pick the color based on statuses
                Dim statusColors As New Dictionary(Of String, Color) From {
                                                    {"Pending", Color.LightGoldenrodYellow},
                                                    {"Running", Color.LightSkyBlue},
                                                    {"Completed", Color.LightGreen},
                                                    {"Canceled", Color.LightCoral},
                                                    {"Postponed", Color.LightGray}}
                ' Apply color if found
                If statusColors.ContainsKey(appStatus) Then
                    e.Appearance.BackColor = statusColors(appStatus)
                    e.Appearance.ForeColor = Color.Black
                Else
                    ' default color if unknown status
                    e.Appearance.BackColor = Color.White
                    e.Appearance.ForeColor = Color.Black
                End If
            End If
        End If
    End Sub


    ' Lightweight view model matching grid columns
    Private Class AppointmentView
        Public Property AppID As Integer
        Public Property ApptDate As String
        Public Property FromTo As String
        Public Property PatientName As String
        Public Property DoctorName As String
        Public Property Detail As String
        Public Property Status As String
    End Class

End Class
