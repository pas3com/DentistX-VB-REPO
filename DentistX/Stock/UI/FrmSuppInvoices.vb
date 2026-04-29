Imports System.ComponentModel
Imports System.Linq

Public Class FrmSuppInvoices

    Private ReadOnly _invoiceRepo As BuyInvoiceRepo
    Private ReadOnly _supplierRepo As SupplierRepository
    Private ReadOnly _lineRepo As BuyInvoiceLineItemRepository

    Private _suppliers As List(Of Supplier)
    Private _invoices As BindingList(Of BuyInvoice)

    Public Sub New()
        MyBase.New()
        InitializeComponent()

        Dim conn = DentistXDATA.GetConnection.ConnectionString
        _invoiceRepo = New BuyInvoiceRepo(conn)
        _supplierRepo = New SupplierRepository(conn)
        _lineRepo = New BuyInvoiceLineItemRepository(conn)

        If Eng Then
            Me.Text = "Supplier Invoices"
        Else
            Me.Text = "فواتير الموردين"
        End If

        If Not Eng Then
            Me.RightToLeft = RightToLeft.Yes
            Me.RightToLeftLayout = True
        End If

        dtFrom.DateTime = Date.Today.AddMonths(-1)
        dtTo.DateTime = Date.Today.AddDays(1)

        AddHandler Me.Load, AddressOf OnFormLoad
        AddHandler btnApplyFilters.Click, AddressOf OnApplyFilters
        AddHandler btnClearFilters.Click, AddressOf OnClearFilters
        AddHandler txtSearch.EditValueChanged, AddressOf OnFilterChanged
        AddHandler dtFrom.EditValueChanged, AddressOf OnFilterChanged
        AddHandler dtTo.EditValueChanged, AddressOf OnFilterChanged
        AddHandler cmbSupplier.EditValueChanged, AddressOf OnFilterChanged
        AddHandler chkAllSuppliers.CheckedChanged, AddressOf chkAllSuppliers_CheckedChanged
    End Sub

    Private Sub OnFormLoad(sender As Object, e As EventArgs)
        LoadSuppliers()
        LoadInvoices()
    End Sub

    Private Sub LoadSuppliers()
        _suppliers = _supplierRepo.GetAll().ToList()

        ' Create a pseudo "all suppliers" entry for the lookup
        Dim allItem As New Supplier() With {
            .SupplierID = 0,
            .SupplierName = If(Eng, "All suppliers", "كل الموردين")
        }
        Dim data As New List(Of Supplier)()
        data.Add(allItem)
        data.AddRange(_suppliers)

        cmbSupplier.Properties.DataSource = data
        cmbSupplier.Properties.DisplayMember = NameOf(Supplier.SupplierName)
        cmbSupplier.Properties.ValueMember = NameOf(Supplier.SupplierID)
        cmbSupplier.EditValue = 0
        cmbSupplier.Enabled = Not chkAllSuppliers.Checked
    End Sub

    Private Sub LoadInvoices()
        Dim fromDate = dtFrom.DateTime.Date
        Dim toDate = dtTo.DateTime.Date

        Dim list As List(Of BuyInvoice)

        Dim supplierId As Integer = 0
        If Not chkAllSuppliers.Checked AndAlso cmbSupplier.EditValue IsNot Nothing Then
            Integer.TryParse(cmbSupplier.EditValue.ToString(), supplierId)
        End If

        If supplierId > 0 Then
            list = _invoiceRepo.GetBySupplier(supplierId).
                Where(Function(i) i.InvoiceDate.Date >= fromDate AndAlso i.InvoiceDate.Date <= toDate).
                OrderByDescending(Function(i) i.InvoiceDate).
                ToList()
        Else
            list = _invoiceRepo.GetAllInvoices(fromDate, toDate).
                OrderByDescending(Function(i) i.InvoiceDate).
                ToList()
        End If

        Dim searchText = (If(txtSearch.Text, "")).Trim().ToLowerInvariant()
        If searchText.Length > 0 Then
            list = list.Where(Function(i) (If(i.SupplierName, "")).ToLower().Contains(searchText)).ToList()
        End If

        _invoices = New BindingList(Of BuyInvoice)(list)
        grdHeaders.DataSource = _invoices

        viewHeaders.BestFitColumns()
        UpdateHeaderForFocusedRow()
        LoadInvoiceDetailsForFocusedRow()

        LoadSupplierSummary()
    End Sub

    Private Sub OnApplyFilters(sender As Object, e As EventArgs)
        LoadInvoices()
    End Sub

    Private Sub OnClearFilters(sender As Object, e As EventArgs)
        chkAllSuppliers.Checked = True
        cmbSupplier.EditValue = 0
        txtSearch.EditValue = ""
        dtFrom.DateTime = Date.Today.AddMonths(-1)
        dtTo.DateTime = Date.Today.AddDays(1)
        LoadInvoices()
    End Sub

    Private Sub OnFilterChanged(sender As Object, e As EventArgs)
        LoadInvoices()
    End Sub

    Private Sub chkAllSuppliers_CheckedChanged(sender As Object, e As EventArgs) Handles chkAllSuppliers.CheckedChanged
        cmbSupplier.Enabled = Not chkAllSuppliers.Checked
        LoadInvoices()
    End Sub

    Private Sub viewHeaders_FocusedRowChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles viewHeaders.FocusedRowChanged
        UpdateHeaderForFocusedRow()
        LoadInvoiceDetailsForFocusedRow()
    End Sub

    Private Sub UpdateHeaderForFocusedRow()
        Dim rowHandle = viewHeaders.FocusedRowHandle
        If rowHandle < 0 Then
            lblHdrInvoice.Text = "Invoice: -"
            lblHdrSupplier.Text = "Supplier: -"
            lblHdrDate.Text = "Date: -"
            lblHdrTotal.Text = "Total: -"
            Return
        End If

        Dim invoiceNo = TryCast(viewHeaders.GetRowCellValue(rowHandle, "InvoiceID"), Object)
        Dim supplierName = TryCast(viewHeaders.GetRowCellValue(rowHandle, "SupplierName"), Object)
        Dim invDateObj = TryCast(viewHeaders.GetRowCellValue(rowHandle, "InvoiceDate"), Object)
        Dim totalObj = TryCast(viewHeaders.GetRowCellValue(rowHandle, "TotalAmount"), Object)

        lblHdrInvoice.Text = "Invoice: " & If(invoiceNo IsNot Nothing, invoiceNo.ToString(), "-")
        lblHdrSupplier.Text = "Supplier: " & If(supplierName IsNot Nothing, supplierName.ToString(), "-")

        Dim dateText As String = "-"
        Dim dt As DateTime
        If invDateObj IsNot Nothing AndAlso DateTime.TryParse(invDateObj.ToString(), dt) Then
            dateText = dt.ToString("yyyy-MM-dd")
        End If
        lblHdrDate.Text = "Date: " & dateText

        Dim totalText As String = "-"
        Dim decVal As Decimal
        If totalObj IsNot Nothing AndAlso Decimal.TryParse(totalObj.ToString(), decVal) Then
            totalText = decVal.ToString("N2")
        End If
        lblHdrTotal.Text = "Total: " & totalText
    End Sub

    Private Sub LoadInvoiceDetailsForFocusedRow()
        Dim rowHandle = viewHeaders.FocusedRowHandle
        If rowHandle < 0 Then
            grdDetails.DataSource = Nothing
            Return
        End If

        Dim invoiceIdObj = viewHeaders.GetRowCellValue(rowHandle, "InvoiceID")
        Dim invoiceId As Integer
        If invoiceIdObj Is Nothing OrElse Not Integer.TryParse(invoiceIdObj.ToString(), invoiceId) Then
            grdDetails.DataSource = Nothing
            Return
        End If

        Dim lines = _lineRepo.GetByInvoice(invoiceId).ToList()
        grdDetails.DataSource = New BindingList(Of BuyInvoiceLineItem)(lines)
        viewDetails.BestFitColumns()
    End Sub

    Private Sub LoadSupplierSummary()
        If _invoices Is Nothing OrElse _invoices.Count = 0 Then
            grdSummary.DataSource = Nothing
            Return
        End If

        Dim summary = _invoices.
            GroupBy(Function(i) i.SupplierName).
            Select(Function(g) New With {
                .SupplierName = g.Key,
                .InvoiceCount = g.Count(),
                .TotalAmount = g.Sum(Function(i) i.TotalAmount)
            }).
            OrderBy(Function(x) x.SupplierName).
            ToList()

        grdSummary.DataSource = summary
        viewSummary.BestFitColumns()
    End Sub

    Private Sub btnReport_Click(sender As Object, e As EventArgs) Handles btnReport.Click
        Try
            Dim rowHandle = viewHeaders.FocusedRowHandle
            If rowHandle < 0 Then
                MsgBox("Select an invoice first.")
                Return
            End If

            ' Header fields (grid column names must match these strings)
            Dim invoiceIdObj = viewHeaders.GetRowCellValue(rowHandle, "InvoiceID")
            Dim supplierNameObj = viewHeaders.GetRowCellValue(rowHandle, "SupplierName")
            Dim invoiceDateObj = viewHeaders.GetRowCellValue(rowHandle, "InvoiceDate")
            Dim totalObj = viewHeaders.GetRowCellValue(rowHandle, "TotalAmount")

            Dim invoiceId As Integer
            If invoiceIdObj Is Nothing OrElse Not Integer.TryParse(invoiceIdObj.ToString(), invoiceId) Then
                MsgBox("Invalid invoice selected.")
                Return
            End If

            Dim supplierName As String = If(supplierNameObj Is Nothing, "", supplierNameObj.ToString())

            Dim invoiceDate As DateTime = Date.Today
            If invoiceDateObj IsNot Nothing Then DateTime.TryParse(invoiceDateObj.ToString(), invoiceDate)

            Dim totalAmount As Decimal = 0D
            If totalObj IsNot Nothing Then Decimal.TryParse(totalObj.ToString(), totalAmount)

            ' Optional fields (if present in grid/model)
            Dim dueDateObj = viewHeaders.GetRowCellValue(rowHandle, "DueDate")
            Dim dueDate As Date = invoiceDate.Date
            If dueDateObj IsNot Nothing Then
                Dim dt As DateTime
                If DateTime.TryParse(dueDateObj.ToString(), dt) Then dueDate = dt.Date
            End If

            Dim invoiceStatusObj = viewHeaders.GetRowCellValue(rowHandle, "InvoiceStatus")
            Dim invoiceStatus As String = If(invoiceStatusObj Is Nothing, "", invoiceStatusObj.ToString())

            Dim supplierIdObj = viewHeaders.GetRowCellValue(rowHandle, "SupplierID")
            Dim supplierId As Integer = 0
            If supplierIdObj IsNot Nothing Then Integer.TryParse(supplierIdObj.ToString(), supplierId)

            Dim supplier As Supplier = Nothing
            If supplierId > 0 Then supplier = _supplierRepo.GetById(supplierId)
            If supplier Is Nothing Then
                supplier = New Supplier With {.SupplierID = supplierId, .SupplierName = supplierName}
            End If

            Dim buyInvoice As New BuyInvoice With {
                .InvoiceID = invoiceId,
                .SupplierID = supplierId,
                .SupplierName = supplierName,
                .InvoiceDate = invoiceDate.Date,
                .DueDate = dueDate,
                .InvoiceStatus = invoiceStatus,
                .TotalAmount = totalAmount
            }

            Dim report As New SupplierInvoice(supplier, buyInvoice)

            Dim printTool As New DevExpress.XtraReports.UI.ReportPrintTool(report)
            Dim previewForm As Form = printTool.PreviewForm
            previewForm.Size = New Size(1366, 768)
            previewForm.StartPosition = FormStartPosition.CenterScreen
            previewForm.WindowState = FormWindowState.Normal

            printTool.ShowPreviewDialog()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub btnReport2_Click(sender As Object, e As EventArgs) Handles btnReport2.Click
        Try
            Dim rowHandle = viewHeaders.FocusedRowHandle
            If rowHandle < 0 Then
                MsgBox("Select an invoice first.")
                Return
            End If

            Dim buyInvoice = TryCast(viewHeaders.GetRow(rowHandle), BuyInvoice)
            If buyInvoice Is Nothing Then
                MsgBox("Invalid invoice selected.")
                Return
            End If

            Dim supplier As Supplier = Nothing
            If buyInvoice.SupplierID > 0 Then
                supplier = _supplierRepo.GetById(buyInvoice.SupplierID)
            End If
            If supplier Is Nothing Then
                supplier = New Supplier With {
                    .SupplierID = buyInvoice.SupplierID,
                    .SupplierName = If(buyInvoice.SupplierName, "")
                }
            End If

            Dim report As New SupplierInvoice(supplier, buyInvoice)

            Dim printTool As New DevExpress.XtraReports.UI.ReportPrintTool(report)
            Dim previewForm As Form = printTool.PreviewForm
            previewForm.Size = New Size(1366, 768)
            previewForm.StartPosition = FormStartPosition.CenterScreen
            previewForm.WindowState = FormWindowState.Normal

            printTool.ShowPreviewDialog()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub
End Class