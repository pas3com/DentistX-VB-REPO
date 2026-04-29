Imports System.Data
Imports System.Data.SqlClient
Imports DevExpress.XtraGrid.Views.Base

Partial Public Class PersonnelAttendance

    Private ConnectionString As String = DentistXDATA.GetConnection.ConnectionString
    Private bAddMode As Boolean = False
    Private bEditMode As Boolean = False
    Private bDeleteMode As Boolean = False
    Private iRow As Integer = -1
    Private currentAttendanceID As Integer = 0

    Public Sub New()
        InitializeComponent()
        SetupComboBoxes()
    End Sub

    Private Sub SetupComboBoxes()
        cboPersonType.Properties.Items.AddRange(New Object() {"Doctor", "Secretary", "Emp"})
        cboStatus.Properties.Items.AddRange(New Object() {"Present", "Absent", "Late", "HalfDay", "Leave", "Holiday"})
        cboPersonType.SelectedIndex = 0
        cboStatus.SelectedIndex = 0
        dteAttendanceDate.EditValue = Date.Today
    End Sub

    Private Sub PersonnelAttendance_Load(sender As Object, e As EventArgs) Handles Me.Load
        LoadPersonList()
        LoadDGV()
        SetEditMode(False)
    End Sub

    Private Sub cboPersonType_EditValueChanged(sender As Object, e As EventArgs) Handles cboPersonType.EditValueChanged
        LoadPersonList()
    End Sub

    Private Sub LoadPersonList()
        cboPersonID.Properties.DataSource = Nothing
        cboPersonID.Properties.DisplayMember = "Name"
        cboPersonID.Properties.ValueMember = "ID"
        Dim personType As String = If(cboPersonType.SelectedItem?.ToString(), "Doctor")
        Dim dt As New DataTable()
        Try
            Using conn As New SqlConnection(ConnectionString)
                conn.Open()
                Dim sql As String = ""
                Select Case personType
                    Case "Doctor"
                        sql = "SELECT DrID AS ID, DrName AS Name FROM [dbo].[Doctors] ORDER BY DrName"
                    Case "Secretary"
                        sql = "SELECT SecID AS ID, SecName AS Name FROM [dbo].[Secretaries] ORDER BY SecName"
                    Case "Emp"
                        sql = "SELECT EmpID AS ID, EmpName AS Name FROM [dbo].[Emp] ORDER BY EmpName"
                    Case Else
                        Return
                End Select
                Using cmd As New SqlCommand(sql, conn)
                    Using da As New SqlDataAdapter(cmd)
                        da.Fill(dt)
                    End Using
                End Using
            End Using
            cboPersonID.Properties.DataSource = dt
        Catch ex As Exception
            ' Tables may not exist yet
        End Try
    End Sub

    Private Sub LoadDGV()
        Try
            Using conn As New SqlConnection(ConnectionString)
                conn.Open()
                Dim sql As String = "SELECT AttendanceID, PersonType, PersonID, PersonName, AttendanceDate, CheckInTime, CheckOutTime, Status, Notes FROM [dbo].[vw_PersonnelAttendance_WithNames] ORDER BY AttendanceDate DESC, PersonType, PersonID"
                Using cmd As New SqlCommand(sql, conn)
                    Using da As New SqlDataAdapter(cmd)
                        Dim dt As New DataTable()
                        da.Fill(dt)
                        DGV.DataSource = dt
                    End Using
                End Using
            End Using
        Catch ex As Exception
            DGV.DataSource = Nothing
            ' View/table may not exist - run CreateAttendancePaymentTables.sql first
        End Try
    End Sub

    Private Sub dgView_CustomUnboundColumnData(sender As Object, e As CustomColumnDataEventArgs) Handles dgView.CustomUnboundColumnData
        If e.Column.Name = "colRowNum" Then
            e.Value = e.ListSourceRowIndex + 1
        End If
    End Sub

    Private Sub DGV_Click(sender As Object, e As EventArgs) Handles DGV.Click
        DisplaySelected()
    End Sub

    Private Sub dgView_FocusedRowChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles dgView.FocusedRowChanged
        DisplaySelected()
    End Sub

    Private Sub DisplaySelected()
        If dgView.RowCount = 0 OrElse dgView.FocusedRowHandle < 0 Then Return
        iRow = dgView.FocusedRowHandle
        Try
            currentAttendanceID = CInt(dgView.GetRowCellValue(iRow, colAttendanceID))
            cboPersonType.SelectedItem = dgView.GetRowCellValue(iRow, colPersonType)?.ToString()
            LoadPersonList()
            cboPersonID.EditValue = dgView.GetRowCellValue(iRow, colPersonID)
            dteAttendanceDate.EditValue = dgView.GetRowCellValue(iRow, colAttendanceDate)
            tmeCheckIn.EditValue = dgView.GetRowCellValue(iRow, colCheckInTime)
            tmeCheckOut.EditValue = dgView.GetRowCellValue(iRow, colCheckOutTime)
            cboStatus.SelectedItem = dgView.GetRowCellValue(iRow, colStatus)?.ToString()
            txtNotes.Text = dgView.GetRowCellValue(iRow, colNotes)?.ToString()
        Catch ex As Exception
            ' Ignore
        End Try
    End Sub

    Private Sub SetEditMode(enable As Boolean)
        cboPersonType.Enabled = enable
        cboPersonID.Enabled = enable
        dteAttendanceDate.Enabled = enable
        tmeCheckIn.Enabled = enable
        tmeCheckOut.Enabled = enable
        cboStatus.Enabled = enable
        txtNotes.Enabled = enable
        btnAdd.Enabled = Not enable
        btnEdit.Enabled = Not enable AndAlso dgView.RowCount > 0
        btnDelete.Enabled = Not enable AndAlso dgView.RowCount > 0
        btnSave.Enabled = enable
        btnCancel.Enabled = enable
    End Sub

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        bAddMode = True
        bEditMode = False
        bDeleteMode = False
        ClearForm()
        dteAttendanceDate.EditValue = Date.Today
        tmeCheckIn.EditValue = New Date(2025, 1, 1, 8, 0, 0, 0)
        tmeCheckOut.EditValue = New Date(2025, 1, 1, 17, 0, 0, 0)
        SetEditMode(True)
    End Sub

    Private Sub btnEdit_Click(sender As Object, e As EventArgs) Handles btnEdit.Click
        If dgView.RowCount = 0 Then Return
        DisplaySelected()
        bAddMode = False
        bEditMode = True
        bDeleteMode = False
        SetEditMode(True)
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        If dgView.RowCount = 0 OrElse currentAttendanceID = 0 Then Return
        If MessageBox.Show("Are you sure you want to delete this attendance record?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
            Try
                Using conn As New SqlConnection(ConnectionString)
                    conn.Open()
                    Using cmd As New SqlCommand("DELETE FROM [dbo].[PersonnelAttendance] WHERE AttendanceID = @ID", conn)
                        cmd.Parameters.AddWithValue("@ID", currentAttendanceID)
                        cmd.ExecuteNonQuery()
                    End Using
                End Using
                LoadDGV()
                ClearForm()
                SetEditMode(False)
            Catch ex As Exception
                MessageBox.Show("Delete failed: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If Not ValidateForm() Then Return
        Try
            Dim personType As String = cboPersonType.SelectedItem?.ToString()
            Dim personID As Integer = CInt(cboPersonID.EditValue)
            Dim attDate As Date = CDate(dteAttendanceDate.EditValue)
            Dim checkIn As TimeSpan? = If(tmeCheckIn.EditValue IsNot Nothing, CType(tmeCheckIn.EditValue, Date).TimeOfDay, Nothing)
            Dim checkOut As TimeSpan? = If(tmeCheckOut.EditValue IsNot Nothing, CType(tmeCheckOut.EditValue, Date).TimeOfDay, Nothing)
            Dim status As String = cboStatus.SelectedItem?.ToString()
            Dim notes As String = txtNotes.Text.Trim()

            Using conn As New SqlConnection(ConnectionString)
                conn.Open()
                If bAddMode Then
                    Using cmd As New SqlCommand("INSERT INTO [dbo].[PersonnelAttendance] (PersonType, PersonID, AttendanceDate, CheckInTime, CheckOutTime, Status, Notes) VALUES (@PersonType, @PersonID, @AttendanceDate, @CheckInTime, @CheckOutTime, @Status, @Notes)", conn)
                        cmd.Parameters.AddWithValue("@PersonType", personType)
                        cmd.Parameters.AddWithValue("@PersonID", personID)
                        cmd.Parameters.AddWithValue("@AttendanceDate", attDate)
                        cmd.Parameters.AddWithValue("@CheckInTime", If(checkIn.HasValue, CObj(checkIn.Value), DBNull.Value))
                        cmd.Parameters.AddWithValue("@CheckOutTime", If(checkOut.HasValue, CObj(checkOut.Value), DBNull.Value))
                        cmd.Parameters.AddWithValue("@Status", If(String.IsNullOrEmpty(status), DBNull.Value, CObj(status)))
                        cmd.Parameters.AddWithValue("@Notes", If(String.IsNullOrEmpty(notes), DBNull.Value, CObj(notes)))
                        cmd.ExecuteNonQuery()
                    End Using
                ElseIf bEditMode Then
                    Using cmd As New SqlCommand("UPDATE [dbo].[PersonnelAttendance] SET PersonType=@PersonType, PersonID=@PersonID, AttendanceDate=@AttendanceDate, CheckInTime=@CheckInTime, CheckOutTime=@CheckOutTime, Status=@Status, Notes=@Notes WHERE AttendanceID=@ID", conn)
                        cmd.Parameters.AddWithValue("@ID", currentAttendanceID)
                        cmd.Parameters.AddWithValue("@PersonType", personType)
                        cmd.Parameters.AddWithValue("@PersonID", personID)
                        cmd.Parameters.AddWithValue("@AttendanceDate", attDate)
                        cmd.Parameters.AddWithValue("@CheckInTime", If(checkIn.HasValue, CObj(checkIn.Value), DBNull.Value))
                        cmd.Parameters.AddWithValue("@CheckOutTime", If(checkOut.HasValue, CObj(checkOut.Value), DBNull.Value))
                        cmd.Parameters.AddWithValue("@Status", If(String.IsNullOrEmpty(status), DBNull.Value, CObj(status)))
                        cmd.Parameters.AddWithValue("@Notes", If(String.IsNullOrEmpty(notes), DBNull.Value, CObj(notes)))
                        cmd.ExecuteNonQuery()
                    End Using
                End If
            End Using
            LoadDGV()
            ClearForm()
            SetEditMode(False)
            bAddMode = False
            bEditMode = False
        Catch ex As Exception
            MessageBox.Show("Save failed: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        bAddMode = False
        bEditMode = False
        bDeleteMode = False
        ClearForm()
        DisplaySelected()
        SetEditMode(False)
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub ClearForm()
        cboPersonType.SelectedIndex = 0
        LoadPersonList()
        cboPersonID.EditValue = Nothing
        dteAttendanceDate.EditValue = Date.Today
        tmeCheckIn.EditValue = New Date(2025, 1, 1, 8, 0, 0, 0)
        tmeCheckOut.EditValue = New Date(2025, 1, 1, 17, 0, 0, 0)
        cboStatus.SelectedIndex = 0
        txtNotes.Text = ""
        currentAttendanceID = 0
    End Sub

    Private Function ValidateForm() As Boolean
        If cboPersonType.SelectedItem Is Nothing Then
            MessageBox.Show("Please select Person Type.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            cboPersonType.Focus()
            Return False
        End If
        If cboPersonID.EditValue Is Nothing OrElse cboPersonID.EditValue.ToString() = "" Then
            MessageBox.Show("Please select a person.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            cboPersonID.Focus()
            Return False
        End If
        If dteAttendanceDate.EditValue Is Nothing Then
            MessageBox.Show("Please select attendance date.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            dteAttendanceDate.Focus()
            Return False
        End If
        Return True
    End Function
End Class
