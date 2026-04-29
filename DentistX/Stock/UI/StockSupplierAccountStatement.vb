Imports System.ComponentModel
Imports System.Linq
Imports System.Threading.Tasks
Imports DevExpress.XtraEditors
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Views.Grid
Imports Stock.DAL
Imports Stock.Models

Public Class StockSupplierAccountStatement
    Inherits XtraForm

    Private ReadOnly _invoiceRepo As BuyInvoiceRepo
    Private ReadOnly _paymentRepo As PaymentRepository
    Private ReadOnly _supplierRepo As SupplierRepository

    Private _suppliers As List(Of Supplier)
    Private _invoices As BindingList(Of BuyInvoice)
    Private _payments As BindingList(Of Payment)

    Private ReadOnly _defaultFont As Font = New Font("Calibri", 10, FontStyle.Bold)

    Public Sub New()
        InitializeComponent()

        Dim conn = DentistXDATA.GetConnection.ConnectionString
        _invoiceRepo = New BuyInvoiceRepo(conn)
        _paymentRepo = New PaymentRepository(conn)
        _supplierRepo = New SupplierRepository(conn)

        Text = If(Eng, "Supplier Account Statement", "كشف حساب مورد")

        StartPosition = FormStartPosition.CenterScreen
        Font = _defaultFont
        If Not Eng Then
            RightToLeft = RightToLeft.Yes
            RightToLeftLayout = True
        End If

        _dateFrom.DateTime = Date.Today.AddMonths(-1)
        _dateTo.DateTime = Date.Today.AddDays(1)
        _cmbView.SelectedIndex = 0

        AddHandler Load, AddressOf OnFormLoad
        AddHandler _btnRefresh.Click, Async Sub() Await ReloadAsync()
        AddHandler _cmbSupplier.SelectedIndexChanged, Async Sub() Await ReloadAsync()
        AddHandler _dateFrom.EditValueChanged, Async Sub() Await ReloadAsync()
        AddHandler _dateTo.EditValueChanged, Async Sub() Await ReloadAsync()
        AddHandler _cmbView.SelectedIndexChanged, AddressOf OnViewChanged
        AddHandler _txtSearch.EditValueChanged, AddressOf OnSearchChanged
    End Sub

    Private Async Sub OnFormLoad(sender As Object, e As EventArgs)
        LoadSuppliers()
        Await ReloadAsync()
    End Sub

    Private Sub LoadSuppliers()
        _suppliers = _supplierRepo.GetAll().ToList()
        _cmbSupplier.Properties.Items.Clear()
        _cmbSupplier.Properties.Items.Add(If(Eng, "All suppliers", "كل الموردين"))
        _cmbSupplier.Properties.Items.AddRange(_suppliers.Select(Function(s) s.SupplierName).ToArray())
        _cmbSupplier.SelectedIndex = 0
    End Sub

    Private Async Function ReloadAsync() As Task
        Dim fromDate = _dateFrom.DateTime.Date
        Dim toDate = _dateTo.DateTime.Date

        Dim selectedSupplierId As Integer? = Nothing
        If _cmbSupplier.SelectedIndex > 0 AndAlso _suppliers IsNot Nothing AndAlso _suppliers.Count >= _cmbSupplier.SelectedIndex Then
            selectedSupplierId = _suppliers(_cmbSupplier.SelectedIndex - 1).SupplierID
        End If

        Dim invoicesList As List(Of BuyInvoice)
        If selectedSupplierId.HasValue Then
            invoicesList = _invoiceRepo.GetBySupplier(selectedSupplierId.Value).
                Where(Function(i) i.InvoiceDate.Date >= fromDate AndAlso i.InvoiceDate.Date <= toDate).
                OrderBy(Function(i) i.InvoiceDate).
                ToList()
        Else
            ' All suppliers: all invoices in date range (any status)
            invoicesList = _invoiceRepo.GetAllInvoices(fromDate, toDate).
                OrderBy(Function(i) i.InvoiceDate).
                ToList()
        End If

        _invoices = New BindingList(Of BuyInvoice)(invoicesList)
        _gridInvoices.DataSource = _invoices
        ApplyInvoiceCaptions()
        IntegerMoneyGridColumns.ApplyDecimal2MoneyGridEditors(_viewInvoices, "TotalAmount")
        _viewInvoices.BestFitColumns()

        ' Load payments for the same supplier and period
        Dim paymentsList As New List(Of Payment)
        For Each inv In invoicesList
            Dim invPayments = _paymentRepo.GetByInvoice(inv.InvoiceID)
            paymentsList.AddRange(invPayments)
        Next

        paymentsList = paymentsList.
            Where(Function(p) p.PaymentDate.Date >= fromDate AndAlso p.PaymentDate.Date <= toDate).
            OrderBy(Function(p) p.PaymentDate).
            ToList()

        _payments = New BindingList(Of Payment)(paymentsList)
        _gridPayments.DataSource = _payments
        ApplyPaymentCaptions()
        IntegerMoneyGridColumns.ApplyDecimal2MoneyGridEditors(_viewPayments, "Amount")
        _viewPayments.BestFitColumns()

        UpdateTotals()
        ApplyViewFilter()

        Await Task.CompletedTask
    End Function

    Private Sub ApplyInvoiceCaptions()
        Dim col = _viewInvoices.Columns("InvoiceID")
        If col IsNot Nothing Then
            col.Caption = If(Eng, "Invoice ID", "رقم الفاتورة")
            col.Visible = False
        End If
        col = _viewInvoices.Columns("SupplierID")
        If col IsNot Nothing Then
            col.Caption = If(Eng, "Supplier ID", "رقم المورد")
            col.Visible = False
        End If
        col = _viewInvoices.Columns("SupplierName")
        If col IsNot Nothing Then col.Caption = If(Eng, "Supplier", "المورد")
        col = _viewInvoices.Columns("InvoiceDate")
        If col IsNot Nothing Then col.Caption = If(Eng, "Invoice Date", "تاريخ الفاتورة")
        col = _viewInvoices.Columns("TotalAmount")
        If col IsNot Nothing Then col.Caption = If(Eng, "Total Amount", "إجمالي الفاتورة")
        col = _viewInvoices.Columns("DueDate")
        If col IsNot Nothing Then col.Caption = If(Eng, "Due Date", "تاريخ الاستحقاق")
        col = _viewInvoices.Columns("InvoiceStatus")
        If col IsNot Nothing Then col.Caption = If(Eng, "Status", "الحالة")
        col = _viewInvoices.Columns("CreatedDate")
        If col IsNot Nothing Then col.Caption = If(Eng, "Created", "تاريخ الإنشاء")
    End Sub

    Private Sub ApplyPaymentCaptions()
        Dim col = _viewPayments.Columns("PaymentID")
        If col IsNot Nothing Then
            col.Caption = If(Eng, "Payment ID", "رقم الدفعة")
            col.Visible = False
        End If
        col = _viewPayments.Columns("InvoiceID")
        If col IsNot Nothing Then
            col.Caption = If(Eng, "Invoice ID", "رقم الفاتورة")
            col.Visible = False
        End If
        col = _viewPayments.Columns("SupplierID")
        If col IsNot Nothing Then
            col.Caption = If(Eng, "Supplier ID", "رقم المورد")
            col.Visible = False
        End If
        col = _viewPayments.Columns("SupplierName")
        If col IsNot Nothing Then col.Caption = If(Eng, "Supplier", "المورد")
        col = _viewPayments.Columns("Amount")
        If col IsNot Nothing Then col.Caption = If(Eng, "Amount", "المبلغ")
        col = _viewPayments.Columns("PaymentDate")
        If col IsNot Nothing Then col.Caption = If(Eng, "Payment Date", "تاريخ الدفع")
        col = _viewPayments.Columns("PaymentMethod")
        If col IsNot Nothing Then col.Caption = If(Eng, "Method", "طريقة الدفع")
        col = _viewPayments.Columns("ChqOwner")
        If col IsNot Nothing Then col.Caption = If(Eng, "Chq. owner", "صاحب الشيك")
        col = _viewPayments.Columns("AccountNumber")
        If col IsNot Nothing Then col.Caption = If(Eng, "Account #", "رقم الحساب")
        col = _viewPayments.Columns("ChqNumber")
        If col IsNot Nothing Then col.Caption = If(Eng, "Cheque #", "رقم الشيك")
        col = _viewPayments.Columns("ChqDueDate")
        If col IsNot Nothing Then col.Caption = If(Eng, "Chq. due", "استحقاق الشيك")
        col = _viewPayments.Columns("ChqBank")
        If col IsNot Nothing Then col.Caption = If(Eng, "Chq. bank", "بنك الشيك")
    End Sub

    Private Sub UpdateTotals()
        Dim totalBuys = If(_invoices Is Nothing, 0D, _invoices.Sum(Function(i) i.TotalAmount))
        Dim totalPayments = If(_payments Is Nothing, 0D, _payments.Sum(Function(p) p.Amount))
        Dim balance = totalBuys - totalPayments

        _lblTotalBuys.Text = totalBuys.ToString("N2")
        _lblTotalPayments.Text = totalPayments.ToString("N2")
        _lblBalance.Text = balance.ToString("N2")
    End Sub

    Private Sub OnViewChanged(sender As Object, e As EventArgs)
        ApplyViewFilter()
    End Sub

    Private Sub ApplyViewFilter()
        If _cmbView.SelectedIndex < 0 Then Return

        Select Case _cmbView.SelectedIndex
            Case 0 ' All
                split.Panel1.Visible = True
                split.Panel2.Visible = True
            Case 1 ' Buys only
                split.Panel1.Visible = True
                split.Panel2.Visible = False
            Case 2 ' Payments only
                split.Panel1.Visible = False
                split.Panel2.Visible = True
        End Select
    End Sub

    Private Sub OnSearchChanged(sender As Object, e As EventArgs)
        Dim text = _txtSearch.Text
        _viewInvoices.FindFilterText = text
        _viewPayments.FindFilterText = text
    End Sub
End Class