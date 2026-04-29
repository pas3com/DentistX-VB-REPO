Imports System.Collections.Concurrent
Imports System.ComponentModel
Imports System.Data.SqlClient
Imports Dapper
Imports DevExpress.XtraEditors
Imports DevExpress.XtraGrid.Columns
Imports DevExpress.XtraGrid.Views.Base
Imports DevExpress.XtraGrid.Views.Grid

Public Class AddDiagDetails
    Implements IPatientAwareUserControl


    Private connectionString As String = DentistXDATA.GetConnection.ConnectionString
    Private clsDiags As IEnumerable(Of Patient_Diagnosis)
    Private clsDiagData As New Patient_DiagnosisDATA
    Private clsDiagDet As New Patient_DiagDet
    Private clsDiagDetData As New Patient_DiagDetDATA
    Private selectedDiags As New List(Of DiagItems)

    Private loaded As Boolean = False

    Private WithEvents DiagBS As New BindingSource

    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()
        ApplyCtlGradientBackground(Me,
                             Color.AliceBlue,
                              Color.BlueViolet,
                              Drawing2D.LinearGradientMode.ForwardDiagonal, 128)
        ' Add any initialization after the InitializeComponent() call.
        'LoadData()
    End Sub
    Public Sub New(patientid As Integer)
        ' This call is required by the designer.
        InitializeComponent()
        PassedPatientID = patientid
        ApplyCtlGradientBackground(Me,
                             Color.AliceBlue,
                              Color.BlueViolet,
                              Drawing2D.LinearGradientMode.ForwardDiagonal, 128)
        ' Add any initialization after the InitializeComponent() call.
        LoadData(patientid)
    End Sub
    Private Sub DiagDetails_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        GroupGrid.Text = PassedPatientName & "'s Diagnoses"
        slctCheck.Checked = False
        dtDiagDate.DateTime = Now
        dtTrtToStart.DateTime = Now.AddMonths(1)
        LoadData(PassedPatientID)
    End Sub

    Property PassedPatientID As Integer
    Property PassedPatientName As String

    Public Sub LoadPatientData(patientId As Integer) Implements IPatientAwareUserControl.LoadPatientData
        PassedPatientID = patientId
        ' Always refresh the grid for the requested patient. CurrentPatient is often Nothing when this control
        ' is hosted under diagnosis workspace (see jaw controls), which previously blocked all loads.
        If patientId <> 0 Then
            LoadDiagData(patientId)
        End If
    End Sub

    Private Sub GridViewDiags_CustomUnboundColumnData(sender As Object, e As CustomColumnDataEventArgs) Handles GridViewDiags.CustomUnboundColumnData
        If e.Column.Name = "colRowNum" Then
            e.Value = e.ListSourceRowIndex + 1
        End If
    End Sub

    'load data from Patient_Diagnosis
    Public Sub LoadDiagData(patientid As Integer)
        LoadData(patientid)
    End Sub
    Private Sub LoadData(patientid As Integer)
        If patientid <> 0 Then
            PassedPatientID = patientid
        End If
        txtTrtValue.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        txtTrtValue.Properties.DisplayFormat.FormatString = "N0"
        txtPayValue.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        txtPayValue.Properties.DisplayFormat.FormatString = "N0"

        'clsDiags = clsDiagData.SelectAllByPatient(patientid)
        'DiagBS.DataSource = clsDiags
        'Patient_DiagsGrid.DataSource = DiagBS
        LoadPatientDiags()
    End Sub

#Region "New Load"

    Private Function DiagGridStatusNew() As String
        Return If(Eng, "New", "جديد")
    End Function

    Private Function DiagGridStatusAlreadyAdded() As String
        Return If(Eng, "Already Added", "مضاف مسبقاً")
    End Function

    ' Load Treatments for Selected Patient - DevExpress GridControl version
    Private Sub LoadPatientDiags()
        Try
            Using conn As New SqlConnection(connectionString)
                conn.Open()

                Dim query As String = "
                                        SELECT
                                              d.[DiagID],
                                              d.[PatientID],
                                              d.[ToothNum],
                                              d.[ToothName],
                                              d.[TreatDate],
                                              d.[Treat],
                                              CASE 
                                                  WHEN EXISTS (
                                                      SELECT 1
                                                      FROM [Patient_DiagDet] dt
                                                      CROSS APPLY STRING_SPLIT(dt.[DiagIDs], ',') s
                                                      WHERE TRY_CAST(s.value AS INT) = d.[DiagID]
                                                  )
                                                  THEN'Already Added'
                                                  ELSE 'New' 
                                              END AS Status
                                        FROM [Patient_Diagnosis] d
                                        WHERE d.[PatientID] = @PatientID ; "

                Using cmd As New SqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@PatientID", PassedPatientID)
                    Using reader As SqlDataReader = cmd.ExecuteReader()
                        selectedDiags.Clear()

                        While reader.Read()

                            Dim sqlStatus = reader("Status").ToString().Trim()
                            Dim statusDisplay As String
                            If String.Equals(sqlStatus, "Already Added", StringComparison.OrdinalIgnoreCase) Then
                                statusDisplay = DiagGridStatusAlreadyAdded()
                            Else
                                statusDisplay = DiagGridStatusNew()
                            End If

                            Dim diag As New DiagItems With {
                            .DiagID = reader("DiagID"),
                            .PatientID = reader("PatientID"),
                            .ToothName = reader("ToothName").ToString(),
                            .ToothNum = reader("ToothNum").ToString(),
                            .Treat = reader("Treat").ToString(),
                            .Status = statusDisplay,
                            .TreatDate = If(reader.IsDBNull(reader.GetOrdinal("TreatDate")),
                                            Nothing,
                                            reader.GetDateTime(reader.GetOrdinal("TreatDate"))),
                            .IsSelected = False ' Default not selected
                        }
                            selectedDiags.Add(diag)
                        End While
                    End Using
                End Using
            End Using

            ' Bind to GridControl


            BindToDiagGrid()


        Catch ex As Exception
            MessageBox.Show("Error loading treatments: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    ' Bind data to DevExpress GridControl
    Private Sub BindToDiagGrid()
        ' Create a DataTable for binding Status
        Dim dt As New DataTable()
        dt.Columns.Add("Select", GetType(Boolean))
        dt.Columns.Add("DiagID", GetType(Integer))
        dt.Columns.Add("PatientID", GetType(Integer))
        dt.Columns.Add("Treat", GetType(String))
        dt.Columns.Add("ToothNum", GetType(String))
        dt.Columns.Add("ToothName", GetType(String))
        dt.Columns.Add("TreatDate", GetType(Date))
        dt.Columns.Add("Status", GetType(String))
        For Each diag In selectedDiags
            Dim row As DataRow = dt.NewRow()
            row("Select") = diag.IsSelected
            row("DiagID") = diag.DiagID
            row("PatientID") = diag.PatientID
            row("Treat") = diag.Treat
            row("ToothNum") = diag.ToothNum
            row("ToothName") = diag.ToothName
            row("TreatDate") = diag.TreatDate
            row("Status") = diag.Status
            dt.Rows.Add(row)
        Next

        ' Bind to GridControl
        Patient_DiagsGrid.DataSource = dt
        GridViewDiags.BestFitColumns()

        ' Customize column appearance
        Dim colSelect As GridColumn = GridViewDiags.Columns("Select")
        colSelect.Width = 50
        colSelect.OptionsColumn.AllowEdit = True

        'Dim colStatus As GridColumn = GridViewDiags.Columns("Status")
        For i As Integer = 0 To GridViewDiags.DataRowCount - 1
            If GridViewDiags.GetDataRow(i)("Status").ToString() = DiagGridStatusNew() Then
                GridViewDiags.SetRowCellValue(i, "Select", False)
                'GridViewDiags.SetRowCellValue(i, colStatus, "Already Added")
            End If
        Next
        loaded = True
    End Sub
    Private Sub GridViewDiags_CellValueChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles GridViewDiags.CellValueChanged

        If e.Column.FieldName <> "Select" Then Return

        Dim isSelected As Boolean = CBool(e.Value)

        Dim row As DataRow = GridViewDiags.GetDataRow(e.RowHandle)
        If row Is Nothing Then Return
        ' row("Status").ToString() = "New" OrElse
        Dim isAdded As Boolean = If(row("Status").ToString() = DiagGridStatusAlreadyAdded(), True, False)

        If isAdded Then Return

        Dim diagID As Integer = CInt(row("DiagID"))

        Dim diagIndex = selectedDiags.FindIndex(Function(t) t.DiagID = diagID)

        If diagIndex >= 0 Then
            selectedDiags(diagIndex).IsSelected = isSelected
            selectedDiags(diagIndex).DiagID = row("DiagID")
            selectedDiags(diagIndex).PatientID = row("PatientID")
            selectedDiags(diagIndex).Treat = row("Treat").ToString
            selectedDiags(diagIndex).ToothNum = row("ToothNum").ToString
            selectedDiags(diagIndex).ToothName = row("ToothName").ToString
            selectedDiags(diagIndex).TreatDate = CDate(row("TreatDate"))
            selectedDiags(diagIndex).Status = row("Status")

        End If



    End Sub


    ' Custom Draw Cell to disable already invoiced rows
    Private Sub GridViewDiags_CustomDrawCell(sender As Object, e As DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs) Handles GridViewDiags.CustomDrawCell
        If Not slctCheck.Checked Then
            Return
        End If
        If e.Column.FieldName = "Select" Then
            Dim row As DataRow = GridViewDiags.GetDataRow(e.RowHandle)
            'row("Status").ToString() = "New" OrElse
            If row IsNot Nothing AndAlso (row("Status").ToString() = DiagGridStatusAlreadyAdded()) Then
                ' Disable the checkbox
                e.Appearance.BackColor = Color.LightGray
                e.Appearance.ForeColor = Color.DarkGray

                ' This prevents the checkbox from being clickable
                e.Handled = True
            End If
        End If

    End Sub
    ' Prevent editing of selection column for invoiced rows
    Private Sub GridViewDiags_ShowingEditor(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles GridViewDiags.ShowingEditor
        If Not slctCheck.Checked Then
            Return
        End If
        If GridViewDiags.FocusedColumn.FieldName = "Select" Then
            'Row("Status").ToString() = "New" OrElse
            Dim row As DataRow = GridViewDiags.GetDataRow(GridViewDiags.FocusedRowHandle)
            If row IsNot Nothing AndAlso (row("Status").ToString() = DiagGridStatusAlreadyAdded()) Then
                e.Cancel = True ' Don't show editor for already invoiced rows
            End If
        End If

    End Sub

    ' Row-click to select/deselect (optional)
    Private Sub GridViewDiags_RowClick(sender As Object, e As RowClickEventArgs) Handles GridViewDiags.RowClick
        Dim rowHandle As Integer = GridViewDiags.FocusedRowHandle
        Dim currentValue As Boolean = CBool(GridViewDiags.GetRowCellValue(rowHandle, "Select"))
        GridViewDiags.SetRowCellValue(rowHandle, "Select", Not currentValue)

    End Sub

#End Region

    ' If you have "Select All" and "Select None" buttons, update them:
    Private Sub btnSelectAll_Click(sender As Object, e As EventArgs) Handles btnSelectAll.Click

        For i As Integer = 0 To GridViewDiags.DataRowCount - 1
            Dim row As DataRow = GridViewDiags.GetDataRow(i)
            If row IsNot Nothing AndAlso If(Not slctCheck.Checked, (row("Status").ToString() = DiagGridStatusAlreadyAdded() OrElse row("Status").ToString() = DiagGridStatusNew()), (row("Status").ToString() <> DiagGridStatusAlreadyAdded() OrElse row("Status").ToString() = DiagGridStatusNew())) Then
                GridViewDiags.SetRowCellValue(i, "Select", True)

                ' Update our list
                Dim diagID As Integer = CInt(row("DiagID"))
                Dim diagIndex = selectedDiags.FindIndex(Function(t) t.DiagID = diagID)
                If diagIndex >= 0 Then
                    selectedDiags(diagIndex).IsSelected = True
                End If
            End If

        Next
    End Sub

    Private Sub btnSelectNone_Click(sender As Object, e As EventArgs) Handles btnSelectNone.Click

        For i As Integer = 0 To GridViewDiags.DataRowCount - 1
            GridViewDiags.SetRowCellValue(i, "Select", False)

            ' Update our list
            Dim row As DataRow = GridViewDiags.GetDataRow(i)
            If row IsNot Nothing Then
                Dim diagID As Integer = CInt(row("DiagID"))
                Dim diagIndex = selectedDiags.FindIndex(Function(t) t.DiagID = diagID)
                If diagIndex >= 0 Then
                    selectedDiags(diagIndex).IsSelected = False
                End If
            End If
        Next


    End Sub


    Private Sub slctCheck_CheckedChanged(sender As Object, e As EventArgs) Handles slctCheck.CheckedChanged
        If slctCheck.Checked Then
            slctCheck.Text = If(Eng, "Show All Diagnoses (Including Already Added)", "عرض جميع التشخيصات (بما في ذلك المضافة بالفعل)")
        Else
            slctCheck.Text = If(Eng, "Show Only New Diagnoses", "عرض التشخيصات الجديدة فقط")
        End If
        If loaded Then
            LoadPatientDiags()
        End If
    End Sub

    Private newAppt As New AppointmentC
    Private Sub chkAddToVisits_CheckedChanged(sender As Object, e As EventArgs) Handles chkAddToVisits.CheckedChanged
        If chkAddToVisits.Checked Then
            chkAddToVisits.Text = If(Eng, "Auto-Schedule Follow-Up Appointment", "جدولة موعد متابعة تلقائيًا")

            newAppt.PatientID = PassedPatientID
            If CurrentDoctor IsNot Nothing Then
                newAppt.DrID = CurrentDoctor.DrID
            Else
                newAppt.DrID = 0
            End If
            newAppt.Reason = If(Eng, "Follow-up for Diagnosis", "متابعة للتشخيص")
            newAppt.StartDateTime = dtTrtToStart.DateTime.Date.AddHours(10) ' Default to one week later at 10 AM
            newAppt.EndDateTime = newAppt.StartDateTime.AddMinutes(30) ' Default to 30-minute duration
            newAppt.AppDate = dtTrtToStart.DateTime
            newAppt.Status = "Pending"
            newAppt.CreatedBy = CurrentUser.UsID
            newAppt.CreatedAt = DateTime.Now
            newAppt.Notes = If(Eng, "Auto-scheduled follow-up appointment for diagnosis treatment.", "موعد متابعة مجدول تلقائيًا لعلاج التشخيص.")

            Dim editor As New AppointCEditorForm(newAppt, True, True)
            If editor.ShowDialog() = DialogResult.OK Then
                newAppt = editor.Appointment()
            End If
        Else
            newAppt = Nothing
            chkAddToVisits.Text = If(Eng, "Do Not Schedule Follow-Up Appointment", "عدم جدولة موعد متابعة")
        End If
    End Sub


    Private Sub btnSaveTrt_Click(sender As Object, e As EventArgs) Handles btnSaveTrt.Click
        If GridViewDiags.RowCount = 0 Then
            MsgBox(If(Eng, "No diagnoses available to select.", "لا توجد تشخيصات متاحة للتحديد."), MsgBoxStyle.Exclamation)
            Return
        End If
        If GridViewDiags.RowCount > 0 AndAlso selectedDiags.Count = 0 Then
            MsgBox(If(Eng, "Please select at least one diagnosis from the list.", "يرجى تحديد تشخيص واحد على الأقل من القائمة."), MsgBoxStyle.Exclamation)
            Return
        End If

        ' Build selected IDs from grid state (source of truth) so checked rows are saved even if list is out of sync
        Dim selectedDiagIDs As New List(Of Integer)
        For i As Integer = 0 To GridViewDiags.DataRowCount - 1
            Dim row As DataRow = GridViewDiags.GetDataRow(i)
            If row IsNot Nothing Then
                Dim isSelected As Boolean = False
                Dim selVal = row("Select")
                If selVal IsNot Nothing AndAlso Not IsDBNull(selVal) Then isSelected = CBool(selVal)
                If isSelected Then
                    Dim diagID As Integer = CInt(row("DiagID"))
                    If Not selectedDiagIDs.Contains(diagID) Then selectedDiagIDs.Add(diagID)
                End If
            End If
        Next
        If selectedDiagIDs.Count = 0 Then
            MsgBox(If(Eng, "Please select at least one diagnosis from the list.", "يرجى تحديد تشخيص واحد على الأقل من القائمة."), MsgBoxStyle.Exclamation)
            Return
        End If


        'Rest of your code remains the same...
        clsDiagDet = New Patient_DiagDet With {
        .PatientID = PassedPatientID,
        .AdvancePay = CInt(txtPayValue.EditValue),
        .DateToStart = CDate(dtTrtToStart.EditValue),
        .DiagAgreament = txtDiagAgree.Text,
        .DiagDate = CDate(dtDiagDate.EditValue),
        .DiagDetails = txtDiagDetails.Text,
        .DiagIDs = String.Join(",", selectedDiagIDs),
        .DiagNotes = txtDiagNotes.Text,
        .TotalValue = CInt(txtTrtValue.EditValue),
        .TreatPlan = txtTrtPlan.Text,
        .UserID = CurrentUser.UsID
    }

        If clsDiagDetData.Add(clsDiagDet) Then
            MsgBox(If(Eng, "Diagnosis details saved successfully.", "تم حفظ تفاصيل التشخيص بنجاح."), MsgBoxStyle.Information)
            ' Clear selections and inputs after saving
            For i As Integer = 0 To GridViewDiags.DataRowCount - 1
                GridViewDiags.SetRowCellValue(i, "Select", False)
            Next
            txtPayValue.EditValue = 0
            txtTrtValue.EditValue = 0
            txtDiagAgree.Text = String.Empty
            txtDiagDetails.Text = String.Empty
            txtDiagNotes.Text = String.Empty
            txtTrtPlan.Text = String.Empty
            ' Optionally, you can reset the checkbox and related text
            If chkAddToVisits.Checked Then
                Dim repo As New AppointmentCRepository
                If repo.Insert(newAppt) > 0 Then
                    MsgBox(If(Eng, "Follow-up appointment scheduled successfully.", "تم جدولة موعد المتابعة بنجاح."), MsgBoxStyle.Information)
                Else
                    MsgBox(If(Eng, "Error scheduling follow-up appointment.", "حدث خطأ أثناء جدولة موعد المتابعة."), MsgBoxStyle.Critical)
                End If
            End If
        Else
            MsgBox(If(Eng, "Error saving diagnosis details.", "حدث خطأ أثناء حفظ تفاصيل التشخيص."), MsgBoxStyle.Critical)
        End If
    End Sub






    Private _returnCallback As Action

    Public Sub SetReturnCallback(callback As Action)
        _returnCallback = callback
    End Sub

    Public Event ReturnToJaw As EventHandler

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        RaiseEvent ReturnToJaw(Me, EventArgs.Empty)
    End Sub



#Region "Numeric TextBox Handlers"

    Private Sub txtPrice_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtTrtValue.KeyPress, txtPayValue.KeyPress
        ' Allow control keys (Backspace, Delete, etc.)
        If Char.IsControl(e.KeyChar) Then
            Return
        End If
        ' Allow digits (0-9)
        If Char.IsDigit(e.KeyChar) Then
            Return
        End If
        ' Allow only ONE decimal point (for prices)
        If e.KeyChar = "."c AndAlso Not txtTrtValue.Text.Contains(".") Then
            Return
        End If
        ' Block any other character
        e.Handled = True
    End Sub
    Private Sub txtPrice_PreviewKeyDown(sender As Object, e As PreviewKeyDownEventArgs) Handles txtTrtValue.PreviewKeyDown, txtPayValue.PreviewKeyDown
        ' Allow Ctrl+V (paste) - We'll handle validation separately
        If e.Control AndAlso e.KeyCode = Keys.V Then
            Return
        End If
        ' Allow Backspace, Delete, Arrows, Tab
        If e.KeyCode = Keys.Back OrElse e.KeyCode = Keys.Delete OrElse
                       e.KeyCode = Keys.Left OrElse e.KeyCode = Keys.Right OrElse
                       e.KeyCode = Keys.Tab Then
            Return
        End If
        ' Allow numbers (0-9)
        If e.KeyCode >= Keys.D0 AndAlso e.KeyCode <= Keys.D9 Then
            Return
        End If
        ' Allow numpad numbers (0-9)
        If e.KeyCode >= Keys.NumPad0 AndAlso e.KeyCode <= Keys.NumPad9 Then
            Return
        End If
        ' Allow ONE decimal point (if needed)
        If e.KeyCode = Keys.Decimal AndAlso Not txtTrtValue.Text.Contains(".") Then
            Return
        End If
        ' Block all other keys
        e.IsInputKey = False
    End Sub
    Private Sub txtPrice_EditValueChanged(sender As Object, e As EventArgs) Handles txtTrtValue.EditValueChanged, txtPayValue.EditValueChanged
        ' Skip if empty
        If String.IsNullOrEmpty(txtTrtValue.Text) Then Return
        ' Store cursor position
        Dim cursorPos = txtTrtValue.SelectionStart
        ' Remove all non-numeric characters (except one decimal point)
        Dim cleanedText As New System.Text.StringBuilder()
        Dim hasDecimal As Boolean = False
        For Each c As Char In txtTrtValue.Text
            If Char.IsDigit(c) Then
                cleanedText.Append(c)
            ElseIf c = "."c AndAlso Not hasDecimal Then
                cleanedText.Append(c)
                hasDecimal = True
            End If
        Next
        ' If text was modified (due to invalid pasted chars), update it
        If cleanedText.ToString() <> txtTrtValue.Text Then
            txtTrtValue.Text = cleanedText.ToString()
            ' Restore cursor position (adjusting for removed characters)
            txtTrtValue.SelectionStart = Math.Min(cursorPos, txtTrtValue.Text.Length)
        End If
    End Sub
    Private Sub txtTrtPrice_GotFocus(sender As Object, e As EventArgs) Handles txtTrtValue.GotFocus
        txtTrtValue.SelectAll()
    End Sub
    Private Sub txtPayValue_GotFocus(sender As Object, e As EventArgs) Handles txtPayValue.GotFocus
        txtPayValue.SelectAll()
    End Sub

    Private Sub txtPayValue_Click(sender As Object, e As EventArgs) Handles txtPayValue.Click
        txtPayValue.SelectAll()
    End Sub

    Private Sub txtTrtPrice_Click(sender As Object, e As EventArgs) Handles txtTrtValue.Click
        txtTrtValue.SelectAll()
    End Sub



#End Region


End Class
