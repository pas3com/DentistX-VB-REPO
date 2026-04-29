Imports System.Data
Imports System.Data.SqlClient

Public Class StaffAccountStatement
    Inherits DevExpress.XtraEditors.XtraForm

    Private ConnectionString As String = DentistXDATA.GetConnection.ConnectionString

    Public Sub New()
        InitializeComponent()
        Text = If(Eng, "Staff Account Statement", "كشف حساب الموظفين")
        StartPosition = FormStartPosition.CenterScreen
        If Not Eng Then
            RightToLeft = RightToLeft.Yes
            RightToLeftLayout = True
        End If
        SetupFilters()
    End Sub

    Private Sub SetupFilters()
        cboPersonType.Properties.Items.AddRange(New Object() {"Doctor", "Secretary", "Emp"})
        cboPersonType.SelectedIndex = 0
        dteDateFrom.EditValue = Date.Today.AddMonths(-1)
        dteDateTo.EditValue = Date.Today
    End Sub

    Private Sub StaffAccountStatement_Load(sender As Object, e As EventArgs) Handles Me.Load
        LoadPersonList()
        LoadPayments()
    End Sub

    Private Sub cboPersonType_EditValueChanged(sender As Object, e As EventArgs) Handles cboPersonType.EditValueChanged
        LoadPersonList()
    End Sub

    Private Sub LoadPersonList()
        cboPerson.Properties.DataSource = Nothing
        cboPerson.Properties.DisplayMember = "Name"
        cboPerson.Properties.ValueMember = "ID"
        cboPerson.EditValue = Nothing
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
            ' Add "All" as first row
            Dim dtAll As New DataTable()
            dtAll.Columns.Add("ID", GetType(Integer))
            dtAll.Columns.Add("Name", GetType(String))
            dtAll.Rows.Add(-1, If(Eng, "All", "الكل"))
            For Each row As DataRow In dt.Rows
                dtAll.Rows.Add(row("ID"), row("Name"))
            Next
            cboPerson.Properties.DataSource = dtAll
            cboPerson.EditValue = -1
        Catch ex As Exception
            ' Tables may not exist
        End Try
    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        LoadPayments()
    End Sub

    Private Sub LoadPayments()
        Try
            Dim personType As String = cboPersonType.SelectedItem?.ToString()
            Dim dateFrom As Date = CDate(dteDateFrom.EditValue)
            Dim dateTo As Date = CDate(dteDateTo.EditValue)
            Dim personID As Integer? = Nothing
            If cboPerson.EditValue IsNot Nothing AndAlso Not String.IsNullOrEmpty(cboPerson.EditValue.ToString()) Then
                Dim id As Integer = CInt(cboPerson.EditValue)
                If id > 0 Then personID = id
            End If

            Dim sql As New System.Text.StringBuilder()
            sql.Append("SELECT PaymentID, PersonType, PersonID, PersonName, Amount, PaymentDate, PaymentType, Description ")
            sql.Append("FROM [dbo].[vw_PersonnelPayment_WithNames] ")
            sql.Append("WHERE PaymentDate >= @DateFrom AND PaymentDate <= @DateTo ")
            sql.Append("AND PersonType = @PersonType ")
            If personID.HasValue Then
                sql.Append("AND PersonID = @PersonID ")
            End If
            sql.Append("ORDER BY PaymentDate, PersonName")

            Using conn As New SqlConnection(ConnectionString)
                conn.Open()
                Using cmd As New SqlCommand(sql.ToString(), conn)
                    cmd.Parameters.AddWithValue("@DateFrom", dateFrom)
                    cmd.Parameters.AddWithValue("@DateTo", dateTo)
                    cmd.Parameters.AddWithValue("@PersonType", personType)
                    If personID.HasValue Then
                        cmd.Parameters.AddWithValue("@PersonID", personID.Value)
                    End If
                    Using da As New SqlDataAdapter(cmd)
                        Dim dt As New DataTable()
                        da.Fill(dt)
                        gridPayments.DataSource = dt
                    End Using
                End Using
            End Using

            ' Calculate total
            Dim total As Decimal = 0D
            If gridPayments.DataSource IsNot Nothing Then
                Dim dt As DataTable = TryCast(gridPayments.DataSource, DataTable)
                If dt IsNot Nothing AndAlso dt.Columns.Contains("Amount") Then
                    For Each row As DataRow In dt.Rows
                        If row("Amount") IsNot DBNull.Value Then
                            total += CDec(row("Amount"))
                        End If
                    Next
                End If
            End If
            lblTotal.Text = total.ToString("N2")
        Catch ex As Exception
            gridPayments.DataSource = Nothing
            lblTotal.Text = "0.00"
            MessageBox.Show(ex.Message, If(Eng, "Error", "خطأ"), MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
End Class
