Imports System.Data
Imports System.Data.SqlClient
Imports DevExpress.XtraGrid.Views.Base

Partial Public Class PersonnelPayment

    Private ConnectionString As String = DentistXDATA.GetConnection.ConnectionString
    Private bAddMode As Boolean = False
    Private bEditMode As Boolean = False
    Private bDeleteMode As Boolean = False
    Private iRow As Integer = -1
    Private currentPaymentID As Integer = 0

    Public Sub New()
        InitializeComponent()
        SetupComboBoxes()
    End Sub

    Private Sub SetupComboBoxes()
        cboPersonType.Properties.Items.AddRange(New Object() {"Doctor", "Secretary", "Emp"})
        cboPaymentType.Properties.Items.AddRange(New Object() {"Salary", "Bonus", "Advance", "Commission", "Deduction", "Overtime"})
        cboPersonType.SelectedIndex = 0
        cboPaymentType.SelectedIndex = 0
        dtePaymentDate.EditValue = Date.Today
        ' Set default pay period to current month
        Dim firstDay As Date = New Date(Date.Today.Year, Date.Today.Month, 1)
        Dim lastDay As Date = firstDay.AddMonths(1).AddDays(-1)
        dtePayPeriodStart.EditValue = firstDay
        dtePayPeriodEnd.EditValue = lastDay
    End Sub

    Private Sub PersonnelPayment_Load(sender As Object, e As EventArgs) Handles Me.Load
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
                Dim sql As String = "SELECT PaymentID, PersonType, PersonID, PersonName, Amount, PaymentDate, PaymentType, PayPeriodStart, PayPeriodEnd, Description, ReferenceNo FROM [dbo].[vw_PersonnelPayment_WithNames] ORDER BY PaymentDate DESC, PersonType, PersonID"
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
            currentPaymentID = CInt(dgView.GetRowCellValue(iRow, colPaymentID))
            cboPersonType.SelectedItem = dgView.GetRowCellValue(iRow, colPersonType)?.ToString()
            LoadPersonList()
            cboPersonID.EditValue = dgView.GetRowCellValue(iRow, colPersonID)
            spnAmount.EditValue = dgView.GetRowCellValue(iRow, colAmount)
            dtePaymentDate.EditValue = dgView.GetRowCellValue(iRow, colPaymentDate)
            cboPaymentType.SelectedItem = dgView.GetRowCellValue(iRow, colPaymentType)?.ToString()
            dtePayPeriodStart.EditValue = dgView.GetRowCellValue(iRow, colPayPeriodStart)
            dtePayPeriodEnd.EditValue = dgView.GetRowCellValue(iRow, colPayPeriodEnd)
            txtDescription.Text = dgView.GetRowCellValue(iRow, colDescription)?.ToString()
            txtReferenceNo.Text = dgView.GetRowCellValue(iRow, colReferenceNo)?.ToString()
        Catch ex As Exception
            ' Ignore
        End Try
    End Sub

    Private Sub SetEditMode(enable As Boolean)
        cboPersonType.Enabled = enable
        cboPersonID.Enabled = enable
        spnAmount.Enabled = enable
        dtePaymentDate.Enabled = enable
        cboPaymentType.Enabled = enable
        dtePayPeriodStart.Enabled = enable
        dtePayPeriodEnd.Enabled = enable
        txtDescription.Enabled = enable
        txtReferenceNo.Enabled = enable
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
        dtePaymentDate.EditValue = Date.Today
        Dim firstDay As Date = New Date(Date.Today.Year, Date.Today.Month, 1)
        Dim lastDay As Date = firstDay.AddMonths(1).AddDays(-1)
        dtePayPeriodStart.EditValue = firstDay
        dtePayPeriodEnd.EditValue = lastDay
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
        If dgView.RowCount = 0 OrElse currentPaymentID = 0 Then Return
        If MessageBox.Show("Are you sure you want to delete this payment record?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
            Try
                Using conn As New SqlConnection(ConnectionString)
                    conn.Open()
                    Using cmd As New SqlCommand("DELETE FROM [dbo].[PersonnelPayment] WHERE PaymentID = @ID", conn)
                        cmd.Parameters.AddWithValue("@ID", currentPaymentID)
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
            Dim amount As Decimal = CDec(spnAmount.EditValue)
            Dim paymentDate As Date = CDate(dtePaymentDate.EditValue)
            Dim paymentType As String = cboPaymentType.SelectedItem?.ToString()
            Dim periodStart As Date? = If(dtePayPeriodStart.EditValue IsNot Nothing, CDate(dtePayPeriodStart.EditValue), Nothing)
            Dim periodEnd As Date? = If(dtePayPeriodEnd.EditValue IsNot Nothing, CDate(dtePayPeriodEnd.EditValue), Nothing)
            Dim description As String = txtDescription.Text.Trim()
            Dim referenceNo As String = txtReferenceNo.Text.Trim()

            Using conn As New SqlConnection(ConnectionString)
                conn.Open()
                If bAddMode Then
                    Using cmd As New SqlCommand("INSERT INTO [dbo].[PersonnelPayment] (PersonType, PersonID, Amount, PaymentDate, PaymentType, PayPeriodStart, PayPeriodEnd, Description, ReferenceNo) VALUES (@PersonType, @PersonID, @Amount, @PaymentDate, @PaymentType, @PayPeriodStart, @PayPeriodEnd, @Description, @ReferenceNo)", conn)
                        cmd.Parameters.AddWithValue("@PersonType", personType)
                        cmd.Parameters.AddWithValue("@PersonID", personID)
                        cmd.Parameters.AddWithValue("@Amount", amount)
                        cmd.Parameters.AddWithValue("@PaymentDate", paymentDate)
                        cmd.Parameters.AddWithValue("@PaymentType", If(String.IsNullOrEmpty(paymentType), DBNull.Value, CObj(paymentType)))
                        cmd.Parameters.AddWithValue("@PayPeriodStart", If(periodStart.HasValue, CObj(periodStart.Value), DBNull.Value))
                        cmd.Parameters.AddWithValue("@PayPeriodEnd", If(periodEnd.HasValue, CObj(periodEnd.Value), DBNull.Value))
                        cmd.Parameters.AddWithValue("@Description", If(String.IsNullOrEmpty(description), DBNull.Value, CObj(description)))
                        cmd.Parameters.AddWithValue("@ReferenceNo", If(String.IsNullOrEmpty(referenceNo), DBNull.Value, CObj(referenceNo)))
                        cmd.ExecuteNonQuery()
                    End Using
                ElseIf bEditMode Then
                    Using cmd As New SqlCommand("UPDATE [dbo].[PersonnelPayment] SET PersonType=@PersonType, PersonID=@PersonID, Amount=@Amount, PaymentDate=@PaymentDate, PaymentType=@PaymentType, PayPeriodStart=@PayPeriodStart, PayPeriodEnd=@PayPeriodEnd, Description=@Description, ReferenceNo=@ReferenceNo WHERE PaymentID=@ID", conn)
                        cmd.Parameters.AddWithValue("@ID", currentPaymentID)
                        cmd.Parameters.AddWithValue("@PersonType", personType)
                        cmd.Parameters.AddWithValue("@PersonID", personID)
                        cmd.Parameters.AddWithValue("@Amount", amount)
                        cmd.Parameters.AddWithValue("@PaymentDate", paymentDate)
                        cmd.Parameters.AddWithValue("@PaymentType", If(String.IsNullOrEmpty(paymentType), DBNull.Value, CObj(paymentType)))
                        cmd.Parameters.AddWithValue("@PayPeriodStart", If(periodStart.HasValue, CObj(periodStart.Value), DBNull.Value))
                        cmd.Parameters.AddWithValue("@PayPeriodEnd", If(periodEnd.HasValue, CObj(periodEnd.Value), DBNull.Value))
                        cmd.Parameters.AddWithValue("@Description", If(String.IsNullOrEmpty(description), DBNull.Value, CObj(description)))
                        cmd.Parameters.AddWithValue("@ReferenceNo", If(String.IsNullOrEmpty(referenceNo), DBNull.Value, CObj(referenceNo)))
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
        spnAmount.EditValue = 0D
        dtePaymentDate.EditValue = Date.Today
        cboPaymentType.SelectedIndex = 0
        dtePayPeriodStart.EditValue = Nothing
        dtePayPeriodEnd.EditValue = Nothing
        txtDescription.Text = ""
        txtReferenceNo.Text = ""
        currentPaymentID = 0
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
        If spnAmount.EditValue Is Nothing OrElse CDec(spnAmount.EditValue) = 0 Then
            MessageBox.Show("Please enter amount (must be non-zero).", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            spnAmount.Focus()
            Return False
        End If
        If dtePaymentDate.EditValue Is Nothing Then
            MessageBox.Show("Please select payment date.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            dtePaymentDate.Focus()
            Return False
        End If
        Return True
    End Function
End Class
