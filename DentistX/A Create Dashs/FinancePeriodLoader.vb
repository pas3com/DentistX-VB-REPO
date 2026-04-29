Imports System.Collections.Generic
Imports System.Data
Imports System.Data.SqlClient
Imports System.Linq

''' <summary>
''' Period finance snapshot for FrmFinance: same sources as dashboard treatments, LabPay, stock expenses/invoices, and personnel payments.
''' Treatment income = sum of Patient_Trts.TrtValue in range (recognized value, not cash collections).
''' PatientPaysTotal = cash recorded in Patient_Pays (collections). SupplierPaymentsTotal = cash from stock Payments table (not double-counted in TotalOutflows).
''' </summary>
Public Class FinancePeriodSnapshot
    Public Property TreatmentIncome As Decimal
    Public Property PatientPaysTotal As Decimal
    Public Property OperatingExpenses As Decimal
    Public Property PurchasesInvoices As Decimal
    Public Property SupplierPaymentsTotal As Decimal
    Public Property LabPayments As Decimal
    Public Property StaffPayments As Decimal

    Public ReadOnly Property TotalOutflows As Decimal
        Get
            Return OperatingExpenses + PurchasesInvoices + LabPayments + StaffPayments
        End Get
    End Property

    Public ReadOnly Property NetPosition As Decimal
        Get
            Return TreatmentIncome - TotalOutflows
        End Get
    End Property

    Public Property DtTreatments As DataTable
    Public Property DtExpenses As DataTable
    Public Property DtPurchases As DataTable
    Public Property DtPatientPays As DataTable
    Public Property DtSupplierPays As DataTable
    Public Property DtLab As DataTable
    Public Property DtStaff As DataTable
    Public Property Warnings As List(Of String)
End Class

Public Class FinancePeriodLoader

    Private ReadOnly _dash As New DashDataModel.DatabaseHelper()

    Public Function Load(dFrom As Date, dTo As Date) As FinancePeriodSnapshot
        Dim snap As New FinancePeriodSnapshot With {.Warnings = New List(Of String)}
        Dim a = dFrom.Date
        Dim b = dTo.Date
        If a > b Then
            Dim tmp = a
            a = b
            b = tmp
        End If

        Dim connStr = DashDataModel.DatabaseHelper._connectionString
        If String.IsNullOrWhiteSpace(connStr) Then
            Dim _unused = New DashDataModel.DatabaseHelper()
            connStr = DashDataModel.DatabaseHelper._connectionString
        End If

        Try
            Dim f As New DashboardFilter With {.DateFrom = a, .DateTo = b}
            Dim treatments = _dash.GetTreatments(f)
            snap.TreatmentIncome = If(treatments Is Nothing OrElse treatments.Count = 0, 0D, treatments.Sum(Function(t) t.TrtValue))
            snap.DtTreatments = BuildTreatmentsTable(treatments)
        Catch ex As Exception
            snap.Warnings.Add("Treatments: " & ex.Message)
            snap.TreatmentIncome = 0D
            snap.DtTreatments = EmptyTreatmentsTable()
        End Try

        Try
            Dim fPay As New DashboardFilter With {.DateFrom = a, .DateTo = b}
            Dim patientPays = _dash.GetPayments(fPay)
            snap.PatientPaysTotal = If(patientPays Is Nothing OrElse patientPays.Count = 0, 0D, patientPays.Sum(Function(p) p.PayValue))
            snap.DtPatientPays = BuildPatientPaysTable(patientPays)
        Catch ex As Exception
            snap.Warnings.Add("Patient payments: " & ex.Message)
            snap.PatientPaysTotal = 0D
            snap.DtPatientPays = EmptyPatientPaysTable()
        End Try

        Try
            Dim labDal As New LabPayDATA()
            Dim labs = labDal.SelectByPayDateRange(a, b).ToList()
            snap.LabPayments = If(labs.Count = 0, 0D, labs.Sum(Function(x) CDec(x.PayValue)))
            snap.DtLab = BuildLabTable(labs)
        Catch ex As Exception
            snap.Warnings.Add("Laboratory payments: " & ex.Message)
            snap.LabPayments = 0D
            snap.DtLab = EmptyLabTable()
        End Try

        Try
            Dim expRepo As New StockDAL.ExpenseRepository(connStr)
            Dim exps = expRepo.GetByRange(a, b).ToList()
            snap.OperatingExpenses = If(exps.Count = 0, 0D, exps.Sum(Function(e) e.Amount))
            snap.DtExpenses = BuildExpensesTable(exps)
        Catch ex As Exception
            snap.Warnings.Add("Operating expenses: " & ex.Message)
            snap.OperatingExpenses = 0D
            snap.DtExpenses = EmptyExpensesTable()
        End Try

        Try
            Dim invRepo As New StockDAL.BuyInvoiceRepo(connStr)
            Dim invs = invRepo.GetAllInvoices(a, b).ToList()
            snap.PurchasesInvoices = If(invs.Count = 0, 0D, invs.Sum(Function(i) i.TotalAmount))
            snap.DtPurchases = BuildPurchasesTable(invs)
        Catch ex As Exception
            snap.Warnings.Add("Supplier buy invoices: " & ex.Message)
            snap.PurchasesInvoices = 0D
            snap.DtPurchases = EmptyPurchasesTable()
        End Try

        Try
            LoadSupplierPayments(snap, connStr, a, b)
        Catch ex As Exception
            snap.Warnings.Add("Supplier payments: " & ex.Message)
            snap.SupplierPaymentsTotal = 0D
            snap.DtSupplierPays = EmptySupplierPaysTable()
        End Try

        Try
            LoadPersonnel(snap, connStr, a, b)
        Catch ex As Exception
            snap.Warnings.Add("Personnel payments: " & ex.Message)
            snap.StaffPayments = 0D
            snap.DtStaff = EmptyStaffTable()
        End Try

        Return snap
    End Function

    Private Shared Sub LoadSupplierPayments(snap As FinancePeriodSnapshot, connStr As String, a As Date, b As Date)
        Using conn As New SqlConnection(connStr)
            conn.Open()
            Const sql As String = "
SELECT p.PaymentDate, p.Amount, p.PaymentMethod, i.InvoiceID, s.SupplierName, ISNULL(p.SupplierID, i.SupplierID) AS SupplierID,
       p.ChqOwner, p.AccountNumber, p.ChqNumber, p.ChqDueDate, p.ChqBank
FROM Payments p
INNER JOIN BuyInvoices i ON p.InvoiceID = i.InvoiceID
INNER JOIN Suppliers s ON s.SupplierID = ISNULL(p.SupplierID, i.SupplierID)
WHERE CAST(p.PaymentDate AS DATE) >= @dFrom AND CAST(p.PaymentDate AS DATE) <= @dTo
ORDER BY p.PaymentDate DESC"
            Using cmd As New SqlCommand(sql, conn)
                cmd.Parameters.AddWithValue("@dFrom", a)
                cmd.Parameters.AddWithValue("@dTo", b)
                Using da As New SqlDataAdapter(cmd)
                    Dim dt As New DataTable()
                    da.Fill(dt)
                    snap.SupplierPaymentsTotal = 0D
                    For Each row As DataRow In dt.Rows
                        snap.SupplierPaymentsTotal += SafeDec(row, "Amount")
                    Next
                    snap.DtSupplierPays = TransformSupplierPaymentsTable(dt)
                End Using
            End Using
        End Using
    End Sub

    Private Shared Function TransformSupplierPaymentsTable(src As DataTable) As DataTable
        Dim t As New DataTable()
        t.Columns.Add("PaymentDate", GetType(Date))
        t.Columns.Add("Supplier", GetType(String))
        t.Columns.Add("SupplierId", GetType(Integer))
        t.Columns.Add("Amount", GetType(Decimal))
        t.Columns.Add("PaymentMethod", GetType(String))
        t.Columns.Add("InvoiceId", GetType(Integer))
        t.Columns.Add("ChqNumber", GetType(String))
        t.Columns.Add("ChqBank", GetType(String))
        t.Columns.Add("ChqDueDate", GetType(String))
        t.Columns.Add("ChqOwner", GetType(String))
        t.Columns.Add("AccountNumber", GetType(String))
        For Each row As DataRow In src.Rows
            Dim supId As Integer = 0
            If src.Columns.Contains("SupplierID") AndAlso Not row.IsNull("SupplierID") Then
                supId = Convert.ToInt32(row("SupplierID"))
            End If
            Dim chqDueTxt As String = ""
            If src.Columns.Contains("ChqDueDate") AndAlso Not row.IsNull("ChqDueDate") Then
                chqDueTxt = CDate(row("ChqDueDate")).ToString("yyyy-MM-dd")
            End If
            Dim chqNum As String = "", chqBank As String = "", chqOwner As String = "", acct As String = ""
            If src.Columns.Contains("ChqNumber") AndAlso Not row.IsNull("ChqNumber") Then chqNum = row("ChqNumber").ToString().Trim()
            If src.Columns.Contains("ChqBank") AndAlso Not row.IsNull("ChqBank") Then chqBank = row("ChqBank").ToString()
            If src.Columns.Contains("ChqOwner") AndAlso Not row.IsNull("ChqOwner") Then chqOwner = row("ChqOwner").ToString()
            If src.Columns.Contains("AccountNumber") AndAlso Not row.IsNull("AccountNumber") Then acct = row("AccountNumber").ToString()
            t.Rows.Add(
                CDate(row("PaymentDate")).Date,
                If(row.IsNull("SupplierName"), "", row("SupplierName").ToString()),
                supId,
                SafeDec(row, "Amount"),
                If(row.IsNull("PaymentMethod"), "", row("PaymentMethod").ToString()),
                Convert.ToInt32(row("InvoiceID")),
                chqNum,
                chqBank,
                chqDueTxt,
                chqOwner,
                acct)
        Next
        Return t
    End Function

    Private Shared Function BuildPatientPaysTable(pays As List(Of PatientPayment)) As DataTable
        Dim t As New DataTable()
        t.Columns.Add("PayDate", GetType(Date))
        t.Columns.Add("Amount", GetType(Decimal))
        t.Columns.Add("PayType", GetType(String))
        t.Columns.Add("Patient", GetType(String))
        t.Columns.Add("Treatment", GetType(String))
        t.Columns.Add("Notes", GetType(String))
        If pays Is Nothing Then Return t
        For Each p In pays
            t.Rows.Add(
                p.PayDate.Date,
                p.PayValue,
                If(String.IsNullOrEmpty(p.PayType), "", p.PayType),
                If(String.IsNullOrEmpty(p.PatientName), "", p.PatientName),
                If(String.IsNullOrEmpty(p.TreatmentDetail), "", p.TreatmentDetail),
                If(String.IsNullOrEmpty(p.Notes), "", p.Notes))
        Next
        Return t
    End Function

    Private Shared Function EmptyPatientPaysTable() As DataTable
        Dim t As New DataTable()
        t.Columns.Add("PayDate", GetType(Date))
        t.Columns.Add("Amount", GetType(Decimal))
        t.Columns.Add("PayType", GetType(String))
        t.Columns.Add("Patient", GetType(String))
        t.Columns.Add("Treatment", GetType(String))
        t.Columns.Add("Notes", GetType(String))
        Return t
    End Function

    Private Shared Function EmptySupplierPaysTable() As DataTable
        Dim t As New DataTable()
        t.Columns.Add("PaymentDate", GetType(Date))
        t.Columns.Add("Supplier", GetType(String))
        t.Columns.Add("SupplierId", GetType(Integer))
        t.Columns.Add("Amount", GetType(Decimal))
        t.Columns.Add("PaymentMethod", GetType(String))
        t.Columns.Add("InvoiceId", GetType(Integer))
        t.Columns.Add("ChqNumber", GetType(String))
        t.Columns.Add("ChqBank", GetType(String))
        t.Columns.Add("ChqDueDate", GetType(String))
        t.Columns.Add("ChqOwner", GetType(String))
        t.Columns.Add("AccountNumber", GetType(String))
        Return t
    End Function

    Private Shared Sub LoadPersonnel(snap As FinancePeriodSnapshot, connStr As String, a As Date, b As Date)
        Using conn As New SqlConnection(connStr)
            conn.Open()
            Const sql As String = "
SELECT PersonName, Amount, PaymentDate, PaymentType, PersonType, PayPeriodStart, PayPeriodEnd, Description
FROM [dbo].[vw_PersonnelPayment_WithNames]
WHERE CAST(PaymentDate AS DATE) >= @dFrom AND CAST(PaymentDate AS DATE) <= @dTo
ORDER BY PaymentDate DESC"
            Using cmd As New SqlCommand(sql, conn)
                cmd.Parameters.AddWithValue("@dFrom", a)
                cmd.Parameters.AddWithValue("@dTo", b)
                Using da As New SqlDataAdapter(cmd)
                    Dim src As New DataTable()
                    da.Fill(src)
                    snap.StaffPayments = 0D
                    For Each row As DataRow In src.Rows
                        snap.StaffPayments += SafeDec(row, "Amount")
                    Next
                    snap.DtStaff = TransformPersonnelTable(src)
                End Using
            End Using
        End Using
    End Sub

    Private Shared Function SafeDec(row As DataRow, col As String) As Decimal
        If row.IsNull(col) Then Return 0D
        Return Convert.ToDecimal(row(col))
    End Function

    Private Shared Function TransformPersonnelTable(src As DataTable) As DataTable
        Dim t As New DataTable()
        t.Columns.Add("PayPeriod", GetType(String))
        t.Columns.Add("Employee", GetType(String))
        t.Columns.Add("Role", GetType(String))
        t.Columns.Add("Gross", GetType(Decimal))
        t.Columns.Add("NetPay", GetType(Decimal))
        For Each row As DataRow In src.Rows
            Dim amt = SafeDec(row, "Amount")
            Dim period As String = "—"
            If Not row.IsNull("PayPeriodStart") AndAlso Not row.IsNull("PayPeriodEnd") Then
                period = String.Format("{0:yyyy-MM-dd} – {1:yyyy-MM-dd}", CDate(row("PayPeriodStart")), CDate(row("PayPeriodEnd")))
            End If
            Dim pt = If(row.IsNull("PersonType"), "", Convert.ToString(row("PersonType")))
            Dim pmt = If(row.IsNull("PaymentType"), "", Convert.ToString(row("PaymentType")))
            Dim role = pt & If(pt.Length > 0 AndAlso pmt.Length > 0, " · ", "") & pmt
            t.Rows.Add(period, If(row.IsNull("PersonName"), "", row("PersonName").ToString()), role, amt, amt)
        Next
        Return t
    End Function

    Private Shared Function BuildTreatmentsTable(treatments As List(Of PatientTreatment)) As DataTable
        Dim t As New DataTable()
        t.Columns.Add("Treatment", GetType(String))
        t.Columns.Add("Category", GetType(String))
        t.Columns.Add("ServiceDate", GetType(Date))
        t.Columns.Add("Amount", GetType(Decimal))
        t.Columns.Add("Patient", GetType(String))
        If treatments Is Nothing Then Return t
        For Each tr In treatments
            t.Rows.Add(If(String.IsNullOrEmpty(tr.Detail), "", tr.Detail), "Clinical", tr.TrtDate.Date, tr.TrtValue, If(String.IsNullOrEmpty(tr.PatientName), "", tr.PatientName))
        Next
        Return t
    End Function

    Private Shared Function BuildLabTable(labs As List(Of LabPay)) As DataTable
        Dim t As New DataTable()
        t.Columns.Add("Date", GetType(Date))
        t.Columns.Add("Laboratory", GetType(String))
        t.Columns.Add("CaseRef", GetType(String))
        t.Columns.Add("Amount", GetType(Decimal))
        t.Columns.Add("Paid", GetType(Boolean))
        For Each lp In labs
            Dim cref = If(String.IsNullOrWhiteSpace(lp.OrderDetails), If(lp.PayDetail, ""), lp.OrderDetails)
            t.Rows.Add(lp.PayDate.Date, If(lp.LabName, ""), cref, CDec(lp.PayValue), True)
        Next
        Return t
    End Function

    Private Shared Function BuildExpensesTable(exps As List(Of StockModels.Expense)) As DataTable
        Dim t As New DataTable()
        t.Columns.Add("Date", GetType(Date))
        t.Columns.Add("Category", GetType(String))
        t.Columns.Add("Description", GetType(String))
        t.Columns.Add("Amount", GetType(Decimal))
        For Each e In exps
            Dim desc = If(String.IsNullOrWhiteSpace(e.Notes), "—", e.Notes)
            t.Rows.Add(e.ExpenseDate.Date, If(e.CategoryName, ""), desc, e.Amount)
        Next
        Return t
    End Function

    Private Shared Function BuildPurchasesTable(invs As List(Of StockModels.BuyInvoice)) As DataTable
        Dim t As New DataTable()
        t.Columns.Add("InvoiceNo", GetType(String))
        t.Columns.Add("Date", GetType(Date))
        t.Columns.Add("Supplier", GetType(String))
        t.Columns.Add("Amount", GetType(Decimal))
        t.Columns.Add("Status", GetType(String))
        For Each inv In invs
            t.Rows.Add("INV-" & inv.InvoiceID.ToString(), inv.InvoiceDate.Date, If(inv.SupplierName, ""), inv.TotalAmount, If(inv.InvoiceStatus, ""))
        Next
        Return t
    End Function

    Private Shared Function EmptyTreatmentsTable() As DataTable
        Dim t As New DataTable()
        t.Columns.Add("Treatment", GetType(String))
        t.Columns.Add("Category", GetType(String))
        t.Columns.Add("ServiceDate", GetType(Date))
        t.Columns.Add("Amount", GetType(Decimal))
        t.Columns.Add("Patient", GetType(String))
        Return t
    End Function

    Private Shared Function EmptyExpensesTable() As DataTable
        Dim t As New DataTable()
        t.Columns.Add("Date", GetType(Date))
        t.Columns.Add("Category", GetType(String))
        t.Columns.Add("Description", GetType(String))
        t.Columns.Add("Amount", GetType(Decimal))
        Return t
    End Function

    Private Shared Function EmptyPurchasesTable() As DataTable
        Dim t As New DataTable()
        t.Columns.Add("InvoiceNo", GetType(String))
        t.Columns.Add("Date", GetType(Date))
        t.Columns.Add("Supplier", GetType(String))
        t.Columns.Add("Amount", GetType(Decimal))
        t.Columns.Add("Status", GetType(String))
        Return t
    End Function

    Private Shared Function EmptyLabTable() As DataTable
        Dim t As New DataTable()
        t.Columns.Add("Date", GetType(Date))
        t.Columns.Add("Laboratory", GetType(String))
        t.Columns.Add("CaseRef", GetType(String))
        t.Columns.Add("Amount", GetType(Decimal))
        t.Columns.Add("Paid", GetType(Boolean))
        Return t
    End Function

    Private Shared Function EmptyStaffTable() As DataTable
        Dim t As New DataTable()
        t.Columns.Add("PayPeriod", GetType(String))
        t.Columns.Add("Employee", GetType(String))
        t.Columns.Add("Role", GetType(String))
        t.Columns.Add("Gross", GetType(Decimal))
        t.Columns.Add("NetPay", GetType(Decimal))
        Return t
    End Function

End Class
