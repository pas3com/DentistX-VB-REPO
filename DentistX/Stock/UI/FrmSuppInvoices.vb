Imports System.ComponentModel
Imports System.Collections.Generic
Imports System.Drawing
Imports System.Globalization
Imports System.IO
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks
Imports System.Windows.Forms
Imports DevExpress.XtraEditors.Controls

Public Class FrmSuppInvoices

    Private ReadOnly _invoiceRepo As BuyInvoiceRepo
    Private ReadOnly _supplierRepo As SupplierRepository
    Private ReadOnly _lineRepo As BuyInvoiceLineItemRepository
    Private ReadOnly _paymentRepo As PaymentRepository

    Private _suppliers As List(Of Supplier)
    Private _invoices As BindingList(Of BuyInvoice)

    Public Sub New()
        MyBase.New()
        InitializeComponent()

        Dim conn = DentistXDATA.GetConnection.ConnectionString
        _invoiceRepo = New BuyInvoiceRepo(conn)
        _supplierRepo = New SupplierRepository(conn)
        _lineRepo = New BuyInvoiceLineItemRepository(conn)
        _paymentRepo = New PaymentRepository(conn)

        ApplyLocalizedStaticUi()
        ConfigureSupplierLookUpShell()

        EnsureRadioLangDefaultArabic()

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

    Private Sub ApplyLocalizedStaticUi()
        Text = If(Eng, "Supplier Invoices", "فواتير الموردين")
        lblSearch.Text = If(Eng, "Search by name:", "بحث بالاسم:")
        lblSupplier.Text = If(Eng, "Supplier:", "المورد:")
        lblDateFrom.Text = If(Eng, "From:", "من:")
        lblDateTo.Text = If(Eng, "To:", "إلى:")
        btnApplyFilters.Text = If(Eng, "Apply", "تطبيق")
        btnClearFilters.Text = If(Eng, "Clear", "مسح")
        btnReport.Text = If(Eng, "Report", "تقرير")
        btnReport2.Text = If(Eng, "Report2", "تقرير 2")
        tabInvoices.Text = If(Eng, "Invoices", "الفواتير")
        tabSummary.Text = If(Eng, "Supplier Summary", "ملخص الموردين")
        lblHdrListTitle.Text = If(Eng, "Invoices for current filter", "الفواتير حسب عامل التصفية")
        chkAllSuppliers.Properties.Caption = If(Eng, "All suppliers", "كل الموردين")
        lblHdrInvoice.Text = If(Eng, "Invoice: -", "الفاتورة: -")
        lblHdrSupplier.Text = If(Eng, "Supplier: -", "المورد: -")
        lblHdrDate.Text = If(Eng, "Date: -", "التاريخ: -")
        lblHdrTotal.Text = If(Eng, "Total: -", "الإجمالي: -")
        btnWhatsSend.ToolTip = If(Eng, "Notify supplier (WhatsApp)", "إشعار المورد (واتساب)")
        LabelControl3.Text = If(Eng, "WhatsApp language:", "لغة رسالة واتساب:")
    End Sub

    Private Sub ConfigureSupplierLookUpShell()
        Dim bold As New Font("Calibri", 10.0F, FontStyle.Bold)
        Dim p = cmbSupplier.Properties
        p.Appearance.Font = bold
        p.Appearance.Options.UseFont = True
        p.AppearanceDropDown.Font = bold
        p.AppearanceDropDown.Options.UseFont = True
        If p.AppearanceDropDownHeader IsNot Nothing Then
            p.AppearanceDropDownHeader.Font = bold
            p.AppearanceDropDownHeader.Options.UseFont = True
        End If
        p.PopupWidth = 520
        p.Columns.Clear()
        p.Columns.Add(New LookUpColumnInfo(NameOf(SupplierPickItem.ListCaption)) With {
            .Caption = If(Eng, "Supplier", "المورد"),
            .Width = 500
        })
    End Sub

    Private Shared Function BuildSupplierListCaption(s As Supplier) As String
        If s Is Nothing Then Return ""
        Dim name = If(s.SupplierName, "").Trim()
        Dim waDigits = WhatsHelper.BuildInternationalWhatsDigits(If(s.WhatsApp, ""), If(s.WhatsAppPrefix, ""))
        If String.IsNullOrWhiteSpace(waDigits) Then Return name
        Return name & "  ·  " & waDigits
    End Function

    Private Sub ApplyHeaderGridCaptions()
        Dim col = viewHeaders.Columns("InvoiceID")
        If col IsNot Nothing Then col.Caption = If(Eng, "Invoice No.", "رقم الفاتورة")
        col = viewHeaders.Columns("SupplierID")
        If col IsNot Nothing Then
            col.Caption = If(Eng, "Supplier ID", "رقم المورد")
            col.Visible = False
        End If
        col = viewHeaders.Columns("SupplierName")
        If col IsNot Nothing Then col.Caption = If(Eng, "Supplier", "المورد")
        col = viewHeaders.Columns("InvoiceDate")
        If col IsNot Nothing Then col.Caption = If(Eng, "Date", "التاريخ")
        col = viewHeaders.Columns("TotalAmount")
        If col IsNot Nothing Then col.Caption = If(Eng, "Total", "الإجمالي")
        col = viewHeaders.Columns("DueDate")
        If col IsNot Nothing Then col.Caption = If(Eng, "Due Date", "تاريخ الاستحقاق")
        col = viewHeaders.Columns("InvoiceStatus")
        If col IsNot Nothing Then col.Caption = If(Eng, "Status", "الحالة")
        col = viewHeaders.Columns("CreatedDate")
        If col IsNot Nothing Then col.Caption = If(Eng, "Created", "تاريخ الإنشاء")
    End Sub

    Private Sub ApplyDetailsGridCaptions()
        Dim col = viewDetails.Columns("LineItemID")
        If col IsNot Nothing Then col.Caption = If(Eng, "Line ID", "رقم البند")
        col = viewDetails.Columns("InvoiceID")
        If col IsNot Nothing Then col.Caption = If(Eng, "Invoice No.", "رقم الفاتورة")
        col = viewDetails.Columns("ProductID")
        If col IsNot Nothing Then col.Caption = If(Eng, "Product ID", "رقم المنتج")
        col = viewDetails.Columns("ProductName")
        If col IsNot Nothing Then col.Caption = If(Eng, "Product", "المنتج")
        col = viewDetails.Columns("Quantity")
        If col IsNot Nothing Then col.Caption = If(Eng, "Qty", "الكمية")
        col = viewDetails.Columns("UnitPrice")
        If col IsNot Nothing Then col.Caption = If(Eng, "Unit price", "سعر الوحدة")
        col = viewDetails.Columns("BatchNumber")
        If col IsNot Nothing Then col.Caption = If(Eng, "Batch", "التشغيلة")
        col = viewDetails.Columns("ExpirationDate")
        If col IsNot Nothing Then col.Caption = If(Eng, "Expiry", "تاريخ الانتهاء")
    End Sub

    Private Sub ApplySummaryGridCaptions()
        Dim col = viewSummary.Columns("SupplierName")
        If col IsNot Nothing Then col.Caption = If(Eng, "Supplier", "المورد")
        col = viewSummary.Columns("InvoiceCount")
        If col IsNot Nothing Then col.Caption = If(Eng, "Invoice count", "عدد الفواتير")
        col = viewSummary.Columns("TotalAmount")
        If col IsNot Nothing Then col.Caption = If(Eng, "Total", "الإجمالي")
    End Sub

    Private Sub OnFormLoad(sender As Object, e As EventArgs)
        LoadSuppliers()
        LoadInvoices()
        EnsureRadioLangDefaultArabic()
    End Sub

    Private Sub LoadSuppliers()
        _suppliers = _supplierRepo.GetAll().ToList()

        Dim data As New List(Of SupplierPickItem)()
        data.Add(New SupplierPickItem With {
            .SupplierID = 0,
            .ListCaption = If(Eng, "All suppliers", "كل الموردين")
        })
        For Each s In _suppliers
            data.Add(New SupplierPickItem With {
                .SupplierID = s.SupplierID,
                .ListCaption = BuildSupplierListCaption(s)
            })
        Next

        cmbSupplier.Properties.DataSource = data
        cmbSupplier.Properties.DisplayMember = NameOf(SupplierPickItem.ListCaption)
        cmbSupplier.Properties.ValueMember = NameOf(SupplierPickItem.SupplierID)
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
        ApplyHeaderGridCaptions()

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
            lblHdrInvoice.Text = If(Eng, "Invoice: -", "الفاتورة: -")
            lblHdrSupplier.Text = If(Eng, "Supplier: -", "المورد: -")
            lblHdrDate.Text = If(Eng, "Date: -", "التاريخ: -")
            lblHdrTotal.Text = If(Eng, "Total: -", "الإجمالي: -")
            Return
        End If

        Dim invoiceNo = TryCast(viewHeaders.GetRowCellValue(rowHandle, "InvoiceID"), Object)
        Dim supplierName = TryCast(viewHeaders.GetRowCellValue(rowHandle, "SupplierName"), Object)
        Dim invDateObj = TryCast(viewHeaders.GetRowCellValue(rowHandle, "InvoiceDate"), Object)
        Dim totalObj = TryCast(viewHeaders.GetRowCellValue(rowHandle, "TotalAmount"), Object)

        lblHdrInvoice.Text = If(Eng, "Invoice: ", "الفاتورة: ") & If(invoiceNo IsNot Nothing, invoiceNo.ToString(), "-")
        lblHdrSupplier.Text = If(Eng, "Supplier: ", "المورد: ") & If(supplierName IsNot Nothing, supplierName.ToString(), "-")

        Dim dateText As String = "-"
        Dim dt As DateTime
        If invDateObj IsNot Nothing AndAlso DateTime.TryParse(invDateObj.ToString(), dt) Then
            dateText = dt.ToString("yyyy-MM-dd")
        End If
        lblHdrDate.Text = If(Eng, "Date: ", "التاريخ: ") & dateText

        Dim totalText As String = "-"
        Dim decVal As Decimal
        If totalObj IsNot Nothing AndAlso Decimal.TryParse(totalObj.ToString(), decVal) Then
            totalText = decVal.ToString("N2")
        End If
        lblHdrTotal.Text = If(Eng, "Total: ", "الإجمالي: ") & totalText
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
        ApplyDetailsGridCaptions()
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
        ApplySummaryGridCaptions()
        viewSummary.BestFitColumns()
    End Sub

    Private Sub btnReport_Click(sender As Object, e As EventArgs) Handles btnReport.Click
        Try
            Dim rowHandle = viewHeaders.FocusedRowHandle
            If rowHandle < 0 Then
                MsgBox(If(Eng, "Select an invoice first.", "يرجى اختيار فاتورة أولاً."), MsgBoxStyle.Information, If(Eng, "Supplier Invoices", "فواتير الموردين"))
                Return
            End If

            ' Header fields (grid column names must match these strings)
            Dim invoiceIdObj = viewHeaders.GetRowCellValue(rowHandle, "InvoiceID")
            Dim supplierNameObj = viewHeaders.GetRowCellValue(rowHandle, "SupplierName")
            Dim invoiceDateObj = viewHeaders.GetRowCellValue(rowHandle, "InvoiceDate")
            Dim totalObj = viewHeaders.GetRowCellValue(rowHandle, "TotalAmount")

            Dim invoiceId As Integer
            If invoiceIdObj Is Nothing OrElse Not Integer.TryParse(invoiceIdObj.ToString(), invoiceId) Then
                MsgBox(If(Eng, "Invalid invoice selected.", "الفاتورة المحددة غير صالحة."), MsgBoxStyle.Exclamation, If(Eng, "Supplier Invoices", "فواتير الموردين"))
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
            MsgBox(ex.Message, MsgBoxStyle.Critical, If(Eng, "Error", "خطأ"))
        End Try
    End Sub

    ''' <summary>Match design-time: first option (Arabic) stays selected at runtime.</summary>
    Private Sub EnsureRadioLangDefaultArabic()
        If RadioLang Is Nothing OrElse RadioLang.Properties.Items.Count = 0 Then Return
        RadioLang.SelectedIndex = 0
    End Sub

    ''' <summary>Message language for WhatsApp: SelectedIndex 1 = English, 0 = Arabic (default).</summary>
    Private Function WhatsMessageUsesEnglish() As Boolean
        Dim idx = RadioLang.SelectedIndex
        If idx = 1 Then Return True
        If idx = 0 Then Return False
        Dim v = RadioLang.EditValue
        If v Is Nothing Then Return False
        Dim i As Integer
        If Integer.TryParse(v.ToString(), i) Then Return i = 1
        Return False
    End Function

    Private Shared Function GetClinicWhatsDisplayName(messageEnglish As Boolean) As String
        Try
            Dim clinic = New ClinicDATA().SelectAll().FirstOrDefault()
            If clinic Is Nothing Then Return ""
            Dim n = If(messageEnglish, clinic.ClinicNameEn, clinic.ClinicNameAr)
            Dim fallback = If(messageEnglish, clinic.ClinicNameAr, clinic.ClinicNameEn)
            n = If(n, "").Trim()
            If n.Length > 0 Then Return n
            Return If(fallback, "").Trim()
        Catch
            Return ""
        End Try
    End Function

    Private Shared Function BuildSupplierPurchaseWhatsMessage(
        messageEnglish As Boolean,
        supplierName As String,
        invoiceId As Integer,
        invoiceDate As Date,
        dueDate As Date,
        invoiceStatus As String,
        paymentTerms As String,
        totalAmount As Decimal,
        lines As IList(Of BuyInvoiceLineItem),
        payments As IList(Of Payment),
        clinicDisplay As String) As String

        Const maxLines = 10
        Const maxPayLines = 10
        Dim supplier = If(supplierName, "").Trim()
        Dim clinic = If(clinicDisplay, "").Trim()
        If clinic.Length = 0 Then clinic = If(messageEnglish, "our clinic", "عيادتنا")

        Dim statusTxt = If(invoiceStatus, "").Trim()
        Dim termsTxt = If(paymentTerms, "").Trim()
        Dim sb As New StringBuilder()
        Dim dfmt As String = "dd/MM/yyyy"

        If messageEnglish Then
            sb.AppendLine("Hello" & If(supplier.Length > 0, " " & supplier, "") & ",")
            sb.AppendLine()
            sb.AppendLine("Brief summary (purchase & payments) — Invoice #" & invoiceId.ToString())
            sb.AppendLine()
            sb.AppendLine("Clinic: " & clinic)
            If supplier.Length > 0 Then sb.AppendLine("Supplier: " & supplier)
            sb.AppendLine("Invoice date: " & invoiceDate.ToString(dfmt))
            sb.AppendLine("Due date: " & dueDate.ToString(dfmt))
            If statusTxt.Length > 0 Then sb.AppendLine("Status: " & statusTxt)
            If termsTxt.Length > 0 Then sb.AppendLine("Payment terms: " & termsTxt)
            sb.AppendLine()
            sb.AppendLine("Items:")
        Else
            sb.AppendLine("السلام عليكم،")
            sb.AppendLine()
            sb.AppendLine("ملخص موجز (المشتريات والمدفوعات) — فاتورة #" & invoiceId.ToString())
            sb.AppendLine()
            sb.AppendLine("العيادة: " & clinic)
            If supplier.Length > 0 Then sb.AppendLine("المورد: " & supplier)
            sb.AppendLine("تاريخ الفاتورة: " & invoiceDate.ToString(dfmt))
            sb.AppendLine("تاريخ الاستحقاق: " & dueDate.ToString(dfmt))
            If statusTxt.Length > 0 Then sb.AppendLine("الحالة: " & statusTxt)
            If termsTxt.Length > 0 Then sb.AppendLine("شروط الدفع: " & termsTxt)
            sb.AppendLine()
            sb.AppendLine("البنود:")
        End If

        Dim list = If(lines, Enumerable.Empty(Of BuyInvoiceLineItem)()).ToList()
        Dim shown = 0
        For Each ln In list.Take(maxLines)
            Dim pname = If(ln.ProductName, "").Trim()
            If pname.Length = 0 Then pname = "#" & ln.ProductID.ToString()
            Dim lt = ln.LineTotal
            If messageEnglish Then
                sb.AppendLine("• " & pname & " × " & ln.Quantity.ToString() & " @ " & ln.UnitPrice.ToString("N2") & " = " & lt.ToString("N2"))
            Else
                sb.AppendLine("• " & pname & " × " & ln.Quantity.ToString() & " — " & ln.UnitPrice.ToString("N2") & " — " & lt.ToString("N2"))
            End If
            shown += 1
        Next

        If list.Count > shown Then
            Dim more = list.Count - shown
            sb.AppendLine(If(messageEnglish, "... +" & more.ToString() & " line(s).", "... +" & more.ToString() & " بندًا."))
        ElseIf shown = 0 Then
            sb.AppendLine(If(messageEnglish, "(No lines.)", "(لا بنود.)"))
        End If

        sb.AppendLine()
        If messageEnglish Then
            sb.AppendLine("Invoice total: " & totalAmount.ToString("N2"))
        Else
            sb.AppendLine("إجمالي الفاتورة: " & totalAmount.ToString("N2"))
        End If

        Dim payList = If(payments, Enumerable.Empty(Of Payment)()).OrderBy(Function(p) p.PaymentDate).ToList()
        Dim paidTotal = payList.Sum(Function(p) p.Amount)

        sb.AppendLine()
        If messageEnglish Then
            sb.AppendLine("Payments:")
        Else
            sb.AppendLine("المدفوعات:")
        End If

        Dim payShown = 0
        For Each p In payList.Take(maxPayLines)
            Dim meth = If(p.PaymentMethod, "").Trim()
            sb.AppendLine("• " & p.PaymentDate.ToString(dfmt) & " — " & p.Amount.ToString("N2") & If(meth.Length > 0, " — " & meth, ""))
            payShown += 1
        Next
        If payList.Count > payShown Then
            Dim morep = payList.Count - payShown
            sb.AppendLine(If(messageEnglish, "... +" & morep.ToString() & " payment(s).", "... +" & morep.ToString() & " دفعة."))
        ElseIf payList.Count = 0 Then
            sb.AppendLine(If(messageEnglish, "(None recorded.)", "(لا يوجد مدفوعات مسجّلة.)"))
        End If

        sb.AppendLine()
        If messageEnglish Then
            sb.AppendLine("Total paid: " & paidTotal.ToString("N2"))
        Else
            sb.AppendLine("إجمالي المدفوع: " & paidTotal.ToString("N2"))
        End If

        Dim remaining = totalAmount - paidTotal
        If remaining > 0.005D Then
            sb.AppendLine(If(messageEnglish, "Balance due: " & remaining.ToString("N2"), "المتبقي: " & remaining.ToString("N2")))
        ElseIf remaining < -0.005D Then
            Dim credit = -remaining
            sb.AppendLine(If(messageEnglish, "Credit / overpaid: " & credit.ToString("N2"), "رصيد زائد / دفع أكثر: " & credit.ToString("N2")))
        Else
            sb.AppendLine(If(messageEnglish, "Invoice settled (fully paid).", "الفاتورة مسدّدة بالكامل."))
        End If

        sb.AppendLine()
        If messageEnglish Then
            sb.AppendLine("Please confirm against your records. Thank you.")
        Else
            sb.AppendLine("يرجى التأكد من مطابقة سجلاتكم.")
            sb.AppendLine("شكرًا لتعاونكم.")
        End If

        Return sb.ToString().TrimEnd()
    End Function

    Private Shared Sub DeleteSupplierWhatsTempPdf(path As String)
        If String.IsNullOrWhiteSpace(path) Then Return
        Try
            If File.Exists(path) Then File.Delete(path)
        Catch
        End Try
    End Sub

    ''' <summary>Same convention as other features: <c>Application.StartupPath\Attachments</c>.</summary>
    Private Shared Function GetAttachmentsRootFolder() As String
        Return Path.Combine(Application.StartupPath, "Attachments")
    End Function

    Private Shared Function SanitizeSupplierFolderName(displayName As String) As String
        Dim s = If(displayName, "").Trim()
        If s.Length = 0 Then Return "Supplier"
        For Each c In Path.GetInvalidFileNameChars()
            s = s.Replace(c, "_"c)
        Next
        For Each c In Path.GetInvalidPathChars()
            s = s.Replace(c, "_"c)
        Next
        s = s.Trim().TrimEnd("."c)
        If s.Length = 0 Then Return "Supplier"
        Const maxLen = 120
        If s.Length > maxLen Then s = s.Substring(0, maxLen).Trim()
        Return s
    End Function

    Private Sub PromptSaveSupplierInvoicePdfAfterSend(tempPdfPath As String, supplierDisplayName As String, invoiceId As Integer, invoiceDate As Date)
        If String.IsNullOrWhiteSpace(tempPdfPath) OrElse Not File.Exists(tempPdfPath) Then Return

        Dim q = If(Eng,
                   "Save a copy of the invoice PDF under Attachments, in a folder named after this supplier?",
                   "هل تريد حفظ نسخة من ملف PDF للفاتورة تحت المرفقات في مجلد باسم هذا المورد؟")
        Dim cap = If(Eng, "Save PDF", "حفظ PDF")
        If MessageBox.Show(Me, q, cap, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) <> DialogResult.Yes Then Return

        Try
            Dim supplierFolder = SanitizeSupplierFolderName(supplierDisplayName)
            Dim dir = Path.Combine(GetAttachmentsRootFolder(), supplierFolder)
            Directory.CreateDirectory(dir)

            Dim baseName = "SupplierInvoice_" & invoiceId.ToString() & "_" & invoiceDate.ToString("yyyyMMdd", CultureInfo.InvariantCulture)
            Dim dest = Path.Combine(dir, baseName & ".pdf")
            Dim n = 2
            While File.Exists(dest)
                dest = Path.Combine(dir, baseName & "_" & n.ToString() & ".pdf")
                n += 1
            End While

            File.Copy(tempPdfPath, dest)
            MessageBox.Show(Me,
                          If(Eng, "PDF saved to:" & vbCrLf & dest, "تم حفظ الملف:" & vbCrLf & dest),
                          cap,
                          MessageBoxButtons.OK,
                          MessageBoxIcon.Information)
        Catch ex As Exception
            MessageBox.Show(Me,
                            If(Eng, "Could not save PDF: ", "تعذّر حفظ ملف PDF: ") & ex.Message,
                            cap,
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error)
        End Try
    End Sub

    Private Async Sub btnWhatsSend_Click(sender As Object, e As EventArgs) Handles btnWhatsSend.Click
        Dim pdfPath As String = Nothing
        Try
            Dim rowHandle = viewHeaders.FocusedRowHandle
            If rowHandle < 0 Then
                MsgBox(If(Eng, "Select an invoice first.", "يرجى اختيار فاتورة أولاً."), MsgBoxStyle.Information, If(Eng, "Supplier Invoices", "فواتير الموردين"))
                Return
            End If

            Dim invoiceIdObj = viewHeaders.GetRowCellValue(rowHandle, "InvoiceID")
            Dim invoiceId As Integer
            If invoiceIdObj Is Nothing OrElse Not Integer.TryParse(invoiceIdObj.ToString(), invoiceId) Then
                MsgBox(If(Eng, "Invalid invoice selected.", "الفاتورة المحددة غير صالحة."), MsgBoxStyle.Exclamation, If(Eng, "Supplier Invoices", "فواتير الموردين"))
                Return
            End If

            Dim supplierNameObj = viewHeaders.GetRowCellValue(rowHandle, "SupplierName")
            Dim supplierName As String = If(supplierNameObj Is Nothing, "", supplierNameObj.ToString())

            Dim invoiceDateObj = viewHeaders.GetRowCellValue(rowHandle, "InvoiceDate")
            Dim invoiceDate As DateTime = Date.Today
            If invoiceDateObj IsNot Nothing Then DateTime.TryParse(invoiceDateObj.ToString(), invoiceDate)

            Dim totalObj = viewHeaders.GetRowCellValue(rowHandle, "TotalAmount")
            Dim totalAmount As Decimal = 0D
            If totalObj IsNot Nothing Then Decimal.TryParse(totalObj.ToString(), totalAmount)

            Dim invoiceStatusObj = viewHeaders.GetRowCellValue(rowHandle, "InvoiceStatus")
            Dim invoiceStatus As String = If(invoiceStatusObj Is Nothing, "", invoiceStatusObj.ToString())

            Dim dueDateObj = viewHeaders.GetRowCellValue(rowHandle, "DueDate")
            Dim dueDate As Date = invoiceDate.Date
            If dueDateObj IsNot Nothing Then
                Dim dtDue As DateTime
                If DateTime.TryParse(dueDateObj.ToString(), dtDue) Then dueDate = dtDue.Date
            End If

            Dim supplierIdObj = viewHeaders.GetRowCellValue(rowHandle, "SupplierID")
            Dim supplierId As Integer = 0
            If supplierIdObj IsNot Nothing Then Integer.TryParse(supplierIdObj.ToString(), supplierId)

            Dim supplier As Supplier = Nothing
            If supplierId > 0 Then supplier = _supplierRepo.GetById(supplierId)
            If supplier Is Nothing Then supplier = New Supplier With {.SupplierID = supplierId, .SupplierName = supplierName}

            Dim waDigits = WhatsHelper.BuildInternationalWhatsDigits(If(supplier.WhatsApp, ""), If(supplier.WhatsAppPrefix, "")).Trim()
            If String.IsNullOrWhiteSpace(waDigits) Then
                MsgBox(If(Eng, "This supplier has no WhatsApp number on file. Add it in Suppliers.", "لا يوجد رقم واتساب لهذا المورد. أضفه من شاشة الموردين."), MsgBoxStyle.Exclamation, If(Eng, "WhatsApp", "واتساب"))
                Return
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

            Dim lines = _lineRepo.GetByInvoice(invoiceId).ToList()
            Dim payments = _paymentRepo.GetByInvoice(invoiceId).ToList()
            Dim msgEng = WhatsMessageUsesEnglish()
            Dim clinicDisplay = GetClinicWhatsDisplayName(msgEng)
            Dim terms = If(supplier.PaymentTerms, "").Trim()
            Dim body = BuildSupplierPurchaseWhatsMessage(
                msgEng, supplierName, invoiceId, invoiceDate.Date, dueDate, invoiceStatus, terms, totalAmount, lines, payments, clinicDisplay)

            Try
                pdfPath = Path.Combine(Path.GetTempPath(), "DentistX_SuppInv_" & invoiceId.ToString() & "_" & DateTime.UtcNow.Ticks.ToString() & ".pdf")
                Using pdfReport As New SupplierInvoice(supplier, buyInvoice)
                    pdfReport.ExportToPdf(pdfPath, Nothing)
                End Using
            Catch exPdf As Exception
                MsgBox(If(Eng, "Could not create invoice PDF: ", "تعذّر إنشاء ملف PDF: ") & exPdf.Message, MsgBoxStyle.Critical, If(Eng, "Supplier Invoices", "فواتير الموردين"))
                Return
            End Try

            Dim bodyWithPdf = body & vbCrLf & vbCrLf &
                If(msgEng, "(PDF invoice attached.)", "(مرفق: نسخة PDF للفاتورة.)")

            If Not Await WhatsAppService.EnsureWhatsAppConnectedOrNotifyAsync(Me).ConfigureAwait(True) Then
                DeleteSupplierWhatsTempPdf(pdfPath)
                pdfPath = Nothing
                Return
            End If

            Dim clinicId As String = WhatsAppService.GetCurrentClinicId()
            If String.IsNullOrWhiteSpace(clinicId) Then
                DeleteSupplierWhatsTempPdf(pdfPath)
                pdfPath = Nothing
                Return
            End If

            btnWhatsSend.Enabled = False
            Dim sendOk As Boolean = False
            Try
                Dim waService As New WhatsAppService()
                Dim ctx As New WhatsAppSendContext With {
                    .Category = WhatsAppMessageCategories.StockSupplierInvoice,
                    .SourceHint = NameOf(FrmSuppInvoices),
                    .DisplayName = supplierName,
                    .RevealMessageCenter = True
                }
                Await waService.SendMessageAsync(clinicId, waDigits, bodyWithPdf, pdfPath, ctx).ConfigureAwait(True)
                sendOk = True
                MsgBox(If(Eng, "Message queued for sending.", "تم وضع الرسالة في الطابور للإرسال."), MsgBoxStyle.Information, If(Eng, "WhatsApp", "واتساب"))
            Catch exSend As Exception
                MsgBox(exSend.Message, MsgBoxStyle.Critical, If(Eng, "Send Error", "خطأ في الإرسال"))
            Finally
                btnWhatsSend.Enabled = True
                If sendOk Then PromptSaveSupplierInvoicePdfAfterSend(pdfPath, supplierName, invoiceId, invoiceDate.Date)
                DeleteSupplierWhatsTempPdf(pdfPath)
                pdfPath = Nothing
            End Try
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, If(Eng, "Send Error", "خطأ في الإرسال"))
            DeleteSupplierWhatsTempPdf(pdfPath)
        End Try
    End Sub

    Private Sub btnReport2_Click(sender As Object, e As EventArgs) Handles btnReport2.Click
        Try
            Dim rowHandle = viewHeaders.FocusedRowHandle
            If rowHandle < 0 Then
                MsgBox(If(Eng, "Select an invoice first.", "يرجى اختيار فاتورة أولاً."), MsgBoxStyle.Information, If(Eng, "Supplier Invoices", "فواتير الموردين"))
                Return
            End If

            Dim buyInvoice = TryCast(viewHeaders.GetRow(rowHandle), BuyInvoice)
            If buyInvoice Is Nothing Then
                MsgBox(If(Eng, "Invalid invoice selected.", "الفاتورة المحددة غير صالحة."), MsgBoxStyle.Exclamation, If(Eng, "Supplier Invoices", "فواتير الموردين"))
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
            MsgBox(ex.Message, MsgBoxStyle.Critical, If(Eng, "Error", "خطأ"))
        End Try

    End Sub

    Private NotInheritable Class SupplierPickItem
        Public Property SupplierID As Integer
        Public Property ListCaption As String
    End Class
End Class