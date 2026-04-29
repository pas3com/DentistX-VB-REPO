Imports System.ComponentModel
Imports System.Diagnostics
Imports System.IO
Imports System.Linq
Imports System.Threading.Tasks
Imports System.Windows.Forms
Imports DevExpress.XtraEditors
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Views.Base
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraTab
Imports TwainLib

Public Class StockSupplierPaymentsForm
    Inherits XtraForm

    Private ReadOnly _invoiceRepo As BuyInvoiceRepo
    Private ReadOnly _paymentRepo As PaymentRepository
    Private ReadOnly _paymentService As PaymentService
    Private ReadOnly _supplierRepo As SupplierRepository
    Private _invoices As BindingList(Of BuyInvoice)
    Private _payments As BindingList(Of Payment)
    Private _suppliers As List(Of Supplier)
    Private ReadOnly _defaultFont As Font = New Font("Calibri", 10, FontStyle.Bold)
    Private _suppressChkDrive As Boolean
    ''' <summary>True while supplier filter reloads the invoice grid — ignore FocusedRowChanged so we do not use a stale row handle / old supplier payments.</summary>
    Private _suppressInvoiceGridFocusedEvent As Boolean
    Private _filterInvoicesSum As Decimal
    Private _filterPaymentsSum As Decimal

    ''' <summary>Full paths for Scanned tab list (same order as scannedFilesList items).</summary>
    Private ReadOnly _scannedFilePaths As New List(Of String)
    Private ReadOnly _scannedListToolTip As New ToolTip()
    Private _attachedGridColumnsSetup As Boolean
    Private _repoButtonOpen As DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit
    ''' <summary>Supplier cheque image picked/scanned before Add Payment commits the row (same pattern as FrmChqPayAccnt).</summary>
    Private _pendingChequeImageSourcePath As String = Nothing
    Private _pendingChequeImageIsDisposableScan As Boolean
    Private _chequeCaptureUiLocked As Boolean
    Private _editingPaymentId As Integer?
    Private _editingPaymentBase As Payment
    Private _btnPayCaptionSave As String
    Private _btnPayCaptionAdd As String

    Public Sub New()
        InitializeComponent()

        Dim conn = DentistXDATA.GetConnection.ConnectionString
        _invoiceRepo = New BuyInvoiceRepo(conn)
        _paymentRepo = New PaymentRepository(conn)
        _paymentService = New PaymentService(conn)
        _supplierRepo = New SupplierRepository(conn)

        Text = If(Eng, "Supplier Payments", "مدفوعات الموردين")
        _btnPayCaptionAdd = _btnPay.Text
        _btnPayCaptionSave = If(Eng, "Save", "حفظ")

        StartPosition = FormStartPosition.CenterScreen
        Font = _defaultFont
        If Not Eng Then
            RightToLeft = RightToLeft.Yes
            RightToLeftLayout = True
        End If

        AddHandler Load, AddressOf OnFormLoad
        AddHandler _viewInvoices.FocusedRowChanged, AddressOf OnInvoiceChanged
        AddHandler _cmbSupplier.SelectedIndexChanged, AddressOf OnSupplierChanged
        AddHandler _btnPay.Click, AddressOf OnAddPayment
        AddHandler _cmbMethod.EditValueChanged, AddressOf OnMethodChanged
        AddHandler txtChqValue.EditValueChanged, AddressOf OnChqValueChanged
    End Sub

    Private Sub OnFormLoad(sender As Object, e As EventArgs)
        IntegerMoneyEditorFocus.ConfigureDecimal2MoneyTextEdit(_spinAmount)
        IntegerMoneyEditorFocus.ConfigureDecimal2MoneyTextEdit(txtChqValue)
        IntegerMoneyEditorFocus.AttachTextEditZeroEmptyElseSelectAll(_spinAmount, txtChqValue)
        IntegerMoneyGridColumns.ApplyDecimal2MoneyGridEditors(_viewInvoices, "TotalAmount")
        IntegerMoneyGridColumns.ApplyDecimal2MoneyGridEditors(_viewPayments, "Amount")
        LoadSuppliers()
        SetupAttachedGridColumnsOnce()
        EnsureInvoiceAttachmentsToolbar()
        LoadInvoices()
        UpdateChequeUi()
    End Sub

    Private Function IsChequeMethod() As Boolean
        Return String.Equals(PaymentMethodStorage.NormalizeForPaymentsTable(_cmbMethod.Text), "Cheque", StringComparison.OrdinalIgnoreCase)
    End Function

    Private Sub OnMethodChanged(sender As Object, e As EventArgs)
        UpdateChequeUi()
    End Sub

    Private Sub UpdateChequeUi()
        Dim chq = IsChequeMethod()
        GroupCheques.Visible = chq
        _spinAmount.Properties.ReadOnly = chq
        If chq Then
            If txtChqValue.EditValue Is Nothing OrElse IntegerMoneyEditorFocus.DecimalFromDecimal2MoneyEdit(txtChqValue) = 0D Then
                txtChqValue.EditValue = _spinAmount.EditValue
            Else
                _spinAmount.EditValue = txtChqValue.EditValue
            End If
        End If
    End Sub

    Private Sub OnChqValueChanged(sender As Object, e As EventArgs)
        If Not IsChequeMethod() Then Return
        _spinAmount.EditValue = txtChqValue.EditValue
    End Sub

    Private Sub LoadSuppliers()
        _suppliers = _supplierRepo.GetAll().ToList()
        _cmbSupplier.Properties.Items.Clear()
        _cmbSupplier.Properties.Items.Add(If(Eng, "All suppliers", "كل الموردين"))
        _cmbSupplier.Properties.Items.AddRange(_suppliers.Select(Function(s) s.SupplierName).ToArray())
        _cmbSupplier.SelectedIndex = 0
    End Sub

    Private Sub OnSupplierChanged(sender As Object, e As EventArgs)
        LoadInvoices()
    End Sub

    Private Sub LoadInvoices()
        _suppressInvoiceGridFocusedEvent = True
        Try
            Dim data = _invoiceRepo.GetOpenInvoices().ToList()
            If _cmbSupplier.SelectedIndex > 0 AndAlso _suppliers IsNot Nothing AndAlso
               _cmbSupplier.SelectedIndex - 1 < _suppliers.Count Then
                Dim supplierId = _suppliers(_cmbSupplier.SelectedIndex - 1).SupplierID
                data = data.Where(Function(i) i.SupplierID = supplierId).ToList()
            End If
            _invoices = New BindingList(Of BuyInvoice)(data)
            _gridInvoices.DataSource = _invoices
            ApplyInvoiceCaptions()
            _viewInvoices.BestFitColumns()
            If _viewInvoices.RowCount > 0 Then
                _viewInvoices.FocusedRowHandle = 0
            Else
                _viewInvoices.ClearSelection()
                _viewInvoices.FocusedRowHandle = DevExpress.XtraGrid.GridControl.InvalidRowHandle
            End If
            LoadPaymentsForFocusedSupplierInvoice()
            PrefillChequeOwnerFromInvoice()
            UpdateFilterTotals()
        Finally
            _suppressInvoiceGridFocusedEvent = False
        End Try
    End Sub

    Private Sub UpdateFilterTotals()
        If _invoices Is Nothing OrElse _invoices.Count = 0 Then
            _filterInvoicesSum = 0D
            _filterPaymentsSum = 0D
            lblOpenInvoices.Text = If(Eng, "Open invoices total: ", "إجمالي الفواتير المفتوحة: ")
            lblPayments.Text = If(Eng, "Payments total: ", "إجمالي المدفوعات: ")
            lblOpenInvoicesSum.Text = _filterInvoicesSum.ToString("N2")
            lblPaymentsSum.Text = _filterPaymentsSum.ToString("N2")
        Else
            _filterInvoicesSum = _invoices.Sum(Function(i) i.TotalAmount)
            Dim ids = _invoices.Select(Function(i) i.InvoiceID).Distinct().ToList()
            _filterPaymentsSum = _paymentRepo.GetTotalPaidForInvoiceIds(ids)
            lblOpenInvoices.Text = If(Eng, "Open invoices total: ", "إجمالي الفواتير المفتوحة: ") ' & _filterInvoicesSum.ToString("N2")
            lblPayments.Text = If(Eng, "Payments total: ", "إجمالي المدفوعات: ") ' & _filterPaymentsSum.ToString("N2")
            lblOpenInvoicesSum.Text = _filterInvoicesSum.ToString("N2")
            lblPaymentsSum.Text = _filterPaymentsSum.ToString("N2")
        End If
        'RefreshSumValueLabels()
    End Sub

    ''' <summary>lblOpenInvoicesSum / lblPaymentsSum: numbers only. Focused row → that invoice’s amount and paid total; otherwise filter totals.</summary>
    Private Sub RefreshSumValueLabels()
        Dim inv = TryCast(_viewInvoices.GetFocusedRow(), BuyInvoice)
        If inv Is Nothing Then
            lblOpenInvoicesSum.Text = _filterInvoicesSum.ToString("N2")
            lblPaymentsSum.Text = _filterPaymentsSum.ToString("N2")
        Else
            lblOpenInvoicesSum.Text = inv.TotalAmount.ToString("N2")
            lblPaymentsSum.Text = _paymentRepo.GetTotalPaid(inv.InvoiceID).ToString("N2")
        End If
    End Sub

    Private Sub OnInvoiceChanged(sender As Object, e As EventArgs)
        If _suppressInvoiceGridFocusedEvent Then Return
        LoadPaymentsForFocusedSupplierInvoice()
        RefreshSumValueLabels()
    End Sub

    Private Sub PrefillChequeOwnerFromInvoice()
        If Not IsChequeMethod() Then Return
        Dim inv = TryCast(_viewInvoices.GetFocusedRow(), BuyInvoice)
        If inv Is Nothing Then Return
        If String.IsNullOrWhiteSpace(txtChqOwner.Text) Then
            txtChqOwner.Text = If(inv.SupplierName, "").Trim()
        End If
    End Sub

    ''' <summary>Payments grid + cheque prefill from the invoice row currently focused in the supplier-filtered grid.</summary>
    Private Sub LoadPaymentsForFocusedSupplierInvoice()
        Dim inv = TryCast(_viewInvoices.GetFocusedRow(), BuyInvoice)
        If inv Is Nothing Then
            _payments = New BindingList(Of Payment)()
        Else
            _payments = New BindingList(Of Payment)(_paymentRepo.GetByInvoice(inv.InvoiceID).ToList())
        End If
        _gridPayments.DataSource = _payments
        ApplyPaymentCaptions()
        _viewPayments.BestFitColumns()
        PrefillChequeOwnerFromInvoice()
        RefreshScannedAndAttachedForCurrentTabs()
    End Sub

    Private Shared ReadOnly _chequeImageExtensions As String() = {".jpg", ".jpeg", ".png", ".bmp", ".gif", ".tif", ".tiff"}

    Private Shared Function IsChequeImageFile(path As String) As Boolean
        Dim ext = IO.Path.GetExtension(path)
        If String.IsNullOrEmpty(ext) Then Return False
        Return _chequeImageExtensions.Contains(ext.ToLowerInvariant())
    End Function

    ''' <summary>First linked image for this payment (Pay[paymentId]_Chq…).</summary>
    Private Function FindLinkedChequeImagePathForPayment(p As Payment) As String
        If p Is Nothing OrElse p.PaymentID <= 0 OrElse Not PaymentMethodIsCheque(p) Then Return Nothing
        Dim folder = GetSupplierChequeFolderForPayment(p)
        If String.IsNullOrWhiteSpace(folder) OrElse Not Directory.Exists(folder) Then Return Nothing
        Dim prefix = "Pay" & p.PaymentID.ToString() & "_Chq"
        For Each f In Directory.EnumerateFiles(folder).Where(AddressOf IsChequeImageFile).OrderBy(Function(x) Path.GetFileName(x))
            If Path.GetFileName(f).StartsWith(prefix, StringComparison.OrdinalIgnoreCase) Then Return f
        Next
        Return Nothing
    End Function

    ''' <summary>Optional nested folder: Attachments\Suppliers\Supplier{id}\Invoice{id}\</summary>
    Private Function GetSupplierInvoiceAttachmentsFolder(inv As BuyInvoice) As String
        If inv Is Nothing Then Return ""
        Dim appDir = Application.StartupPath
        If String.IsNullOrEmpty(appDir) Then Return ""
        Return Path.Combine(appDir, "Attachments", "Suppliers", "Supplier" & inv.SupplierID.ToString(), "Invoice" & inv.InvoiceID.ToString())
    End Function

    Private Function SupplierInvoiceFlatFilePrefix(inv As BuyInvoice) As String
        If inv Is Nothing Then Return ""
        Return "Supplier" & inv.SupplierID.ToString() & "_Invoice" & inv.InvoiceID.ToString() & "_"
    End Function

    Private Function SanitizeAttachmentFileName(name As String) As String
        If String.IsNullOrWhiteSpace(name) Then Return "Attachment"
        Dim invalid = Path.GetInvalidFileNameChars()
        Dim sb As New System.Text.StringBuilder(name.Length)
        For Each ch In name
            If Array.IndexOf(invalid, ch) >= 0 Then sb.Append("_"c) Else sb.Append(ch)
        Next
        Dim cleaned = sb.ToString().Trim()
        Return If(cleaned.Length = 0, "Attachment", cleaned)
    End Function

    Private Function BuildSupplierFlatAttachmentDestPath(root As String, inv As BuyInvoice, sourcePath As String) As String
        Dim ext = Path.GetExtension(sourcePath)
        Dim safeBase = SanitizeAttachmentFileName(Path.GetFileNameWithoutExtension(sourcePath))
        Dim fileName = "Supplier" & inv.SupplierID.ToString() & "_Invoice" & inv.InvoiceID.ToString() & "_" & safeBase & ext
        Dim fullPath = Path.Combine(root, fileName)
        If Not File.Exists(fullPath) Then Return fullPath
        Dim n = 1
        Do
            fileName = "Supplier" & inv.SupplierID.ToString() & "_Invoice" & inv.InvoiceID.ToString() & "_" & safeBase & "_" & n.ToString() & ext
            fullPath = Path.Combine(root, fileName)
            n += 1
        Loop While File.Exists(fullPath)
        Return fullPath
    End Function

    Private _invoiceAttachToolbar As PanelControl

    Private Sub EnsureInvoiceAttachmentsToolbar()
        If AttachedPage Is Nothing OrElse _invoiceAttachToolbar IsNot Nothing Then Return
        _invoiceAttachToolbar = New PanelControl With {.Dock = DockStyle.Top, .Height = 42, .Padding = New Padding(4, 6, 4, 6)}
        _invoiceAttachToolbar.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Dim btn As New SimpleButton With {.Text = If(Eng, "Add file attachments…", "إضافة مرفقات…")}
        btn.SetBounds(4, 6, 180, 26)
        AddHandler btn.Click, AddressOf btnBrowseInvoiceAttachments_Click
        _invoiceAttachToolbar.Controls.Add(btn)
        AttachedPage.Controls.Add(_invoiceAttachToolbar)
        AttachedPage.Controls.SetChildIndex(_invoiceAttachToolbar, 0)
    End Sub

    Private Sub btnBrowseInvoiceAttachments_Click(sender As Object, e As EventArgs)
        Dim inv = TryCast(_viewInvoices.GetFocusedRow(), BuyInvoice)
        If inv Is Nothing Then
            XtraMessageBox.Show(Me, If(Eng, "Select an invoice first.", "يرجى اختيار فاتورة أولا."), If(Eng, "Attachments", "مرفقات"), MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If
        Dim filters = String.Join("|", {
            "All documents (*.pdf;*.doc;*.docx;*.xls;*.xlsx;*.txt;*.rtf)|*.pdf;*.doc;*.docx;*.xls;*.xlsx;*.txt;*.rtf",
            "All images (*.jpg;*.jpeg;*.png;*.bmp;*.gif;*.tif;*.tiff)|*.jpg;*.jpeg;*.png;*.bmp;*.gif;*.tif;*.tiff",
            "All files (*.*)|*.*"
        })
        Using ofd As New OpenFileDialog() With {
            .Title = If(Eng, "Select files to attach to this invoice", "اختر ملفات لإرفاقها بهذه الفاتورة"),
            .Filter = filters,
            .FilterIndex = 1,
            .Multiselect = True
        }
            If ofd.ShowDialog(Me) <> DialogResult.OK Then Return
            Dim root = Path.Combine(Application.StartupPath, "Attachments")
            Directory.CreateDirectory(root)
            Dim n = 0
            For Each src In ofd.FileNames
                Dim dest = BuildSupplierFlatAttachmentDestPath(root, inv, src)
                File.Copy(src, dest, True)
                n += 1
            Next
            XtraMessageBox.Show(Me, If(Eng, $"Saved {n} file(s).", $"تم حفظ {n} ملف/ملفات."), If(Eng, "Attachments", "مرفقات"), MessageBoxButtons.OK, MessageBoxIcon.Information)
            LoadSupplierInvoiceAttachments()
            SyncAttachedGridToFocusedChequePayment()
        End Using
    End Sub

    Private Sub LoadInvoiceChequeScannedFiles(Optional applyDefaultSelection As Boolean = True)
        _scannedFilePaths.Clear()
        If scannedFilesList Is Nothing Then Return
        scannedFilesList.Items.Clear()
        Dim inv = TryCast(_viewInvoices.GetFocusedRow(), BuyInvoice)
        If inv Is Nothing Then
            If scanPreview IsNot Nothing AndAlso Not scanPreview.IsDisposed Then scanPreview.Image = Nothing
            Return
        End If
        Dim folder = GetSupplierChequeFolder(inv)
        If String.IsNullOrWhiteSpace(folder) OrElse Not Directory.Exists(folder) Then
            If scanPreview IsNot Nothing AndAlso Not scanPreview.IsDisposed Then scanPreview.Image = Nothing
            Return
        End If
        Dim files = Directory.GetFiles(folder, "*.*").Where(AddressOf IsChequeImageFile).OrderBy(Function(f) Path.GetFileName(f)).ToList()
        For Each fullPath In files
            _scannedFilePaths.Add(fullPath)
            scannedFilesList.Items.Add(Path.GetFileName(fullPath))
        Next
        If scanPreview IsNot Nothing AndAlso Not scanPreview.IsDisposed Then scanPreview.Image = Nothing
        If Not applyDefaultSelection Then
            scannedFilesList.SelectedIndex = -1
            Return
        End If
        If files.Count > 0 Then
            scannedFilesList.SelectedIndex = -1
            scannedFilesList.SelectedIndex = 0
        Else
            scannedFilesList.SelectedIndex = -1
        End If
    End Sub

    Private Sub ShowScannedFilePreview(fullPath As String)
        If scanPreview Is Nothing OrElse scanPreview.IsDisposed OrElse String.IsNullOrEmpty(fullPath) Then Return
        If Not File.Exists(fullPath) Then Return
        Try
            Dim oldImg = TryCast(scanPreview.Image, System.Drawing.Image)
            Using img As System.Drawing.Image = System.Drawing.Image.FromFile(fullPath)
                scanPreview.Image = New System.Drawing.Bitmap(img)
            End Using
            If oldImg IsNot Nothing Then oldImg.Dispose()
        Catch
            scanPreview.Image = Nothing
        End Try
    End Sub

    Private Sub scannedFilesList_SelectedIndexChanged(sender As Object, e As EventArgs) Handles scannedFilesList.SelectedIndexChanged
        If scannedFilesList Is Nothing Then Return
        Dim idx = scannedFilesList.SelectedIndex
        If idx < 0 OrElse idx >= _scannedFilePaths.Count Then Return
        ShowScannedFilePreview(_scannedFilePaths(idx))
    End Sub

    Private Sub scannedFilesList_MouseMove(sender As Object, e As MouseEventArgs) Handles scannedFilesList.MouseMove
        If scannedFilesList Is Nothing OrElse _scannedFilePaths.Count = 0 Then Return
        Dim idx = scannedFilesList.IndexFromPoint(scannedFilesList.PointToClient(Cursor.Position))
        If idx >= 0 AndAlso idx < _scannedFilePaths.Count Then
            Dim fp = _scannedFilePaths(idx)
            Dim sizeStr = ""
            Try
                sizeStr = FormatFileSize(New FileInfo(fp).Length)
            Catch
                sizeStr = "?"
            End Try
            _scannedListToolTip.SetToolTip(scannedFilesList, fp & vbCrLf & If(Eng, "Size: ", "الحجم: ") & sizeStr)
        Else
            _scannedListToolTip.SetToolTip(scannedFilesList, "")
        End If
    End Sub

    Public Shared Function FormatFileSize(bytes As Long) As String
        If bytes < 1024 Then Return bytes.ToString() & " B"
        If bytes < 1024 * 1024 Then Return (bytes / 1024.0).ToString("N1") & " KB"
        If bytes < 1024 * 1024 * 1024 Then Return (bytes / (1024.0 * 1024)).ToString("N1") & " MB"
        Return (bytes / (1024.0 * 1024 * 1024)).ToString("N1") & " GB"
    End Function

    Private Sub LoadSupplierInvoiceAttachments()
        If GridAttached Is Nothing OrElse ViewAttached Is Nothing Then Return
        SetupAttachedGridColumnsOnce()
        Dim list As New List(Of SupplierInvoiceAttachmentFileRow)
        Dim inv = TryCast(_viewInvoices.GetFocusedRow(), BuyInvoice)
        If inv Is Nothing Then
            GridAttached.DataSource = list
            Return
        End If

        Dim seen As New HashSet(Of String)(StringComparer.OrdinalIgnoreCase)
        Dim addRow =
            Sub(fullPath As String)
                If String.IsNullOrWhiteSpace(fullPath) OrElse Not File.Exists(fullPath) Then Return
                Dim key = Path.GetFullPath(fullPath)
                If Not seen.Add(key) Then Return
                Try
                    Dim fi As New FileInfo(fullPath)
                    list.Add(New SupplierInvoiceAttachmentFileRow With {.FullPath = fullPath, .Size = fi.Length, .Type = fi.Extension})
                Catch
                End Try
            End Sub

        ' Flat files: Attachments\Supplier{sid}_Invoice{iid}_* (same idea as Patient{id}_* in accounting)
        Dim attachRoot = Path.Combine(Application.StartupPath, "Attachments")
        Dim flatPrefix = SupplierInvoiceFlatFilePrefix(inv)
        If Directory.Exists(attachRoot) AndAlso flatPrefix.Length > 0 Then
            For Each fullPath In Directory.GetFiles(attachRoot).Where(Function(f) Path.GetFileName(f).StartsWith(flatPrefix, StringComparison.OrdinalIgnoreCase)).OrderBy(Function(f) Path.GetFileName(f))
                addRow(fullPath)
            Next
        End If

        ' Optional nested: Attachments\Suppliers\SupplierX\InvoiceY\
        Dim nested = GetSupplierInvoiceAttachmentsFolder(inv)
        If Not String.IsNullOrWhiteSpace(nested) AndAlso Directory.Exists(nested) Then
            For Each fullPath In Directory.GetFiles(nested).OrderBy(Function(f) Path.GetFileName(f))
                addRow(fullPath)
            Next
        End If

        ' Linked cheque scans live under Images\...\Cheques (same as Scanned tab) — include here so Attached is not empty
        Dim chqDir = GetSupplierChequeFolder(inv)
        If Not String.IsNullOrWhiteSpace(chqDir) AndAlso Directory.Exists(chqDir) Then
            For Each fullPath In Directory.GetFiles(chqDir).OrderBy(Function(f) Path.GetFileName(f))
                addRow(fullPath)
            Next
        End If

        GridAttached.DataSource = list
    End Sub

    Private Sub SetupAttachedGridColumnsOnce()
        If _attachedGridColumnsSetup OrElse ViewAttached Is Nothing Then Return
        _attachedGridColumnsSetup = True
        ViewAttached.Columns.Clear()
        Dim colFullPath As New DevExpress.XtraGrid.Columns.GridColumn() With {.Caption = If(Eng, "Full path", "المسار الكامل"), .FieldName = "FullPath", .Visible = True}
        Dim colSize As New DevExpress.XtraGrid.Columns.GridColumn() With {.Caption = If(Eng, "Size", "الحجم"), .FieldName = "FormattedSize", .Visible = True}
        Dim colType As New DevExpress.XtraGrid.Columns.GridColumn() With {.Caption = If(Eng, "Type", "النوع"), .FieldName = "Type", .Visible = True}
        _repoButtonOpen = New DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit()
        _repoButtonOpen.Buttons(0).Caption = If(Eng, "Open", "فتح")
        _repoButtonOpen.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor
        AddHandler _repoButtonOpen.ButtonClick, AddressOf ViewAttached_OpenButtonClick
        Dim colOpen As New DevExpress.XtraGrid.Columns.GridColumn() With {
            .Caption = If(Eng, "Open", "فتح"),
            .ColumnEdit = _repoButtonOpen,
            .Visible = True,
            .ShowButtonMode = ShowButtonModeEnum.ShowAlways
        }
        ViewAttached.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {colFullPath, colSize, colType, colOpen})
    End Sub

    Private Sub ViewAttached_OpenButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs)
        Dim row = TryCast(ViewAttached.GetFocusedRow(), SupplierInvoiceAttachmentFileRow)
        If row Is Nothing OrElse String.IsNullOrEmpty(row.FullPath) OrElse Not File.Exists(row.FullPath) Then Return
        Try
            Process.Start(New ProcessStartInfo("explorer.exe", "/select,""" & row.FullPath & """") With {.UseShellExecute = True})
        Catch
        End Try
    End Sub

    Private Sub SyncScannedListToFocusedChequePayment()
        If scannedFilesList Is Nothing Then Return
        Dim pay = TryGetPaymentRowForChequeActions()
        If pay Is Nothing OrElse Not PaymentMethodIsCheque(pay) Then Return
        Dim path = FindLinkedChequeImagePathForPayment(pay)
        If String.IsNullOrWhiteSpace(path) Then Return
        Dim idx = _scannedFilePaths.FindIndex(Function(fp) String.Equals(fp, path, StringComparison.OrdinalIgnoreCase))
        If idx < 0 Then
            Dim fn = IO.Path.GetFileName(path)
            idx = _scannedFilePaths.FindIndex(Function(fp) String.Equals(IO.Path.GetFileName(fp), fn, StringComparison.OrdinalIgnoreCase))
        End If
        If idx < 0 OrElse idx >= scannedFilesList.Items.Count Then Return
        scannedFilesList.SelectedIndex = idx
    End Sub

    Private Sub FocusAttachedRowHandle(listSourceIndex As Integer)
        If ViewAttached Is Nothing Then Return
        Dim h = ViewAttached.GetRowHandle(listSourceIndex)
        If h < 0 Then Return
        ViewAttached.FocusedRowHandle = h
        ViewAttached.MakeRowVisible(h)
    End Sub

    Private Sub SyncAttachedGridToFocusedChequePayment()
        If ViewAttached Is Nothing OrElse GridAttached Is Nothing Then Return
        Dim pay = TryGetPaymentRowForChequeActions()
        If pay Is Nothing OrElse pay.PaymentID <= 0 OrElse Not PaymentMethodIsCheque(pay) Then Return
        Dim linked = FindLinkedChequeImagePathForPayment(pay)
        Dim targetName = If(linked, "").Trim()
        If targetName.Length > 0 Then targetName = Path.GetFileName(targetName)
        Dim payTag = "Pay" & pay.PaymentID.ToString() & "_Chq"
        Dim list = TryCast(GridAttached.DataSource, List(Of SupplierInvoiceAttachmentFileRow))
        If list Is Nothing Then Return
        For i = 0 To list.Count - 1
            Dim fn = Path.GetFileName(list(i).FullPath)
            If targetName.Length > 0 AndAlso String.Equals(fn, targetName, StringComparison.OrdinalIgnoreCase) Then
                FocusAttachedRowHandle(i)
                Return
            End If
        Next
        For i = 0 To list.Count - 1
            If Path.GetFileName(list(i).FullPath).IndexOf(payTag, StringComparison.OrdinalIgnoreCase) >= 0 Then
                FocusAttachedRowHandle(i)
                Return
            End If
        Next
    End Sub

    Private Sub RefreshScannedTabContent()
        LoadInvoiceChequeScannedFiles(applyDefaultSelection:=False)
        SyncScannedListToFocusedChequePayment()
        If scannedFilesList IsNot Nothing AndAlso scannedFilesList.SelectedIndex < 0 AndAlso _scannedFilePaths.Count > 0 Then
            scannedFilesList.SelectedIndex = 0
        End If
    End Sub

    Private Sub RefreshAttachedTabContent()
        LoadSupplierInvoiceAttachments()
        SyncAttachedGridToFocusedChequePayment()
    End Sub

    Private Sub RefreshScannedAndAttachedForCurrentTabs()
        If GridTabControl Is Nothing Then Return
        If GridTabControl.SelectedTabPage Is ScannedPage Then
            RefreshScannedTabContent()
        ElseIf GridTabControl.SelectedTabPage Is AttachedPage Then
            RefreshAttachedTabContent()
        End If
    End Sub

    Private Sub GridTabControl_SelectedPageChanged(sender As Object, e As TabPageChangedEventArgs) Handles GridTabControl.SelectedPageChanged
        If e.Page Is Nothing Then Return
        If e.Page Is ScannedPage Then
            RefreshScannedTabContent()
        ElseIf e.Page Is AttachedPage Then
            RefreshAttachedTabContent()
        End If
    End Sub

    Private Sub ViewPayments_FocusedRowChanged(sender As Object, e As FocusedRowChangedEventArgs) Handles _viewPayments.FocusedRowChanged
        If GridTabControl Is Nothing Then Return
        If GridTabControl.SelectedTabPage Is ScannedPage Then
            SyncScannedListToFocusedChequePayment()
            If scannedFilesList IsNot Nothing AndAlso scannedFilesList.SelectedIndex < 0 AndAlso _scannedFilePaths.Count > 0 Then
                scannedFilesList.SelectedIndex = 0
            End If
        ElseIf GridTabControl.SelectedTabPage Is AttachedPage Then
            SyncAttachedGridToFocusedChequePayment()
        End If
    End Sub

    Private Function NormalizeChqNumber(raw As String) As String
        Dim t = If(raw, "").Trim()
        If t.Length = 0 Then Return Nothing
        If t.Length > 10 Then t = t.Substring(0, 10)
        Return t
    End Function

    Private Function PaymentMethodIsCheque(p As Payment) As Boolean
        If p Is Nothing Then Return False
        Return String.Equals(PaymentMethodStorage.NormalizeForPaymentsTable(If(p.PaymentMethod, "")), "Cheque", StringComparison.OrdinalIgnoreCase)
    End Function

    ''' <summary>Cheques folder for this payment row (uses SupplierID/InvoiceID on the payment, not only the invoice grid focus).</summary>
    Private Function GetSupplierChequeFolderForPayment(p As Payment) As String
        If p Is Nothing OrElse p.InvoiceID <= 0 Then Return ""
        Dim supId = If(p.SupplierID, 0)
        If supId = 0 AndAlso _invoices IsNot Nothing Then
            Dim invM = _invoices.FirstOrDefault(Function(i) i.InvoiceID = p.InvoiceID)
            If invM IsNot Nothing Then supId = invM.SupplierID
        End If
        If supId <= 0 Then Return ""
        Dim appDir = Application.StartupPath
        If String.IsNullOrEmpty(appDir) Then Return ""
        Return Path.Combine(appDir, "Images", "Suppliers", "Supplier" & supId.ToString(), "Invoice" & p.InvoiceID.ToString(), "Cheques")
    End Function

    Private Function GetSupplierDriveOwnerFromPayment(p As Payment) As String
        If p Is Nothing Then Return "Supplier"
        Dim n = If(p.SupplierName, "").Trim()
        If Not String.IsNullOrWhiteSpace(n) Then Return n
        If _invoices IsNot Nothing Then
            Dim invM = _invoices.FirstOrDefault(Function(i) i.InvoiceID = p.InvoiceID)
            If invM IsNot Nothing Then Return GetSupplierDriveOwnerLabel(invM)
        End If
        Dim sid = If(p.SupplierID, 0)
        If sid > 0 Then Return "Supplier_" & sid.ToString()
        Return "Supplier"
    End Function

    ''' <summary>True if the Cheques folder for this payment already has Pay[id]_Chq….</summary>
    Private Function HasLinkedChequeImageForPayment(p As Payment) As Boolean
        If p Is Nothing OrElse p.PaymentID <= 0 Then Return False
        Dim folder = GetSupplierChequeFolderForPayment(p)
        If String.IsNullOrWhiteSpace(folder) OrElse Not Directory.Exists(folder) Then Return False
        Dim prefix = "Pay" & p.PaymentID.ToString() & "_Chq"
        For Each f In Directory.EnumerateFiles(folder)
            If Path.GetFileName(f).StartsWith(prefix, StringComparison.OrdinalIgnoreCase) Then Return True
        Next
        Return False
    End Function

    ''' <summary>FocusedRowHandle / selection still resolve after focus moves to Browse/Scan (GetFocusedRow alone can fail).</summary>
    Private Function TryGetPaymentRowForChequeActions() As Payment
        Dim h = _viewPayments.FocusedRowHandle
        If h >= 0 AndAlso h <> GridControl.InvalidRowHandle Then
            Dim pr = TryCast(_viewPayments.GetRow(h), Payment)
            If pr IsNot Nothing Then Return pr
        End If
        Dim selected = _viewPayments.GetSelectedRows()
        If selected IsNot Nothing AndAlso selected.Length > 0 Then
            Dim pr2 = TryCast(_viewPayments.GetRow(selected(0)), Payment)
            If pr2 IsNot Nothing Then Return pr2
        End If
        Return TryCast(_viewPayments.GetFocusedRow(), Payment)
    End Function

    Private Sub CopyChequeAttachmentToLinkedPathForPayment(p As Payment, sourcePath As String)
        If p Is Nothing OrElse p.PaymentID <= 0 Then Throw New InvalidOperationException("Invalid payment")
        Dim ext = Path.GetExtension(sourcePath)
        If String.IsNullOrWhiteSpace(ext) Then ext = ".jpg"
        Dim folder = GetSupplierChequeFolderForPayment(p)
        If String.IsNullOrWhiteSpace(folder) Then Throw New InvalidOperationException("Invalid folder")
        If Not Directory.Exists(folder) Then Directory.CreateDirectory(folder)
        Dim dest = Path.Combine(folder, "Pay" & p.PaymentID.ToString() & "_Chq" & SafeChequeFileNamePart(NormalizeChqNumber(p.ChqNumber)) & ext)
        File.Copy(sourcePath, dest, overwrite:=True)
        If ChkDrive.Checked Then PatientDriveMirror.TryMirrorSupplierChequeFile(dest, GetSupplierDriveOwnerFromPayment(p), p.InvoiceID, Me)
    End Sub

    Private Function BuildLinkedChequeFilePath(inv As BuyInvoice, paymentId As Integer, chqNumberRaw As String, ext As String) As String
        Dim folder = GetSupplierChequeFolder(inv)
        If String.IsNullOrWhiteSpace(folder) Then Return Nothing
        Dim e = If(ext, "").Trim()
        If String.IsNullOrWhiteSpace(e) Then e = ".jpg"
        If Not e.StartsWith("."c) Then e = "." & e
        Dim name = "Pay" & paymentId.ToString() & "_Chq" & SafeChequeFileNamePart(NormalizeChqNumber(chqNumberRaw)) & e
        Return Path.Combine(folder, name)
    End Function

    Private Sub CopyChequeAttachmentToLinkedPath(inv As BuyInvoice, paymentId As Integer, chqNumberRaw As String, sourcePath As String)
        Dim ext = Path.GetExtension(sourcePath)
        If String.IsNullOrWhiteSpace(ext) Then ext = ".jpg"
        Dim dest = BuildLinkedChequeFilePath(inv, paymentId, chqNumberRaw, ext)
        If String.IsNullOrWhiteSpace(dest) Then Throw New InvalidOperationException("Invalid folder")
        Dim dir = Path.GetDirectoryName(dest)
        If Not String.IsNullOrWhiteSpace(dir) AndAlso Not Directory.Exists(dir) Then Directory.CreateDirectory(dir)
        File.Copy(sourcePath, dest, overwrite:=True)
        If ChkDrive.Checked Then PatientDriveMirror.TryMirrorSupplierChequeFile(dest, GetSupplierDriveOwnerLabel(inv), inv.InvoiceID, Me)
    End Sub

    Private Shared Function SafeChequeFileNamePart(raw As String) As String
        Dim t = If(raw, "").Trim()
        For Each c In Path.GetInvalidFileNameChars()
            t = t.Replace(c, "_"c)
        Next
        If t.Length > 48 Then t = t.Substring(0, 48)
        Return If(t.Length = 0, "Chq", t)
    End Function

    Private Function ValidateChequeFields() As Boolean
        If String.IsNullOrWhiteSpace(txtChqOwner.Text) Then
            XtraMessageBox.Show(Me, If(Eng, "Cheque owner is required.", "صاحب الشيك مطلوب."), If(Eng, "Validation", "تنبيه"), MessageBoxButtons.OK)
            Return False
        End If
        If String.IsNullOrWhiteSpace(txtAccountNumber.Text) Then
            XtraMessageBox.Show(Me, If(Eng, "Account number is required.", "رقم الحساب مطلوب."), If(Eng, "Validation", "تنبيه"), MessageBoxButtons.OK)
            Return False
        End If
        If String.IsNullOrWhiteSpace(txtChqNumber.Text) Then
            XtraMessageBox.Show(Me, If(Eng, "Cheque number is required.", "رقم الشيك مطلوب."), If(Eng, "Validation", "تنبيه"), MessageBoxButtons.OK)
            Return False
        End If
        If dtChqDueDate.EditValue Is Nothing OrElse String.IsNullOrWhiteSpace(dtChqDueDate.Text) Then
            XtraMessageBox.Show(Me, If(Eng, "Cheque due date is required.", "تاريخ استحقاق الشيك مطلوب."), If(Eng, "Validation", "تنبيه"), MessageBoxButtons.OK)
            Return False
        End If
        If String.IsNullOrWhiteSpace(txtChqBank.Text) Then
            XtraMessageBox.Show(Me, If(Eng, "Cheque bank is required.", "بنك الشيك مطلوب."), If(Eng, "Validation", "تنبيه"), MessageBoxButtons.OK)
            Return False
        End If
        Return True
    End Function

    ''' <summary>For add flow: cheque fields plus pay date and amount before Scan/Browse can hold an image until Add Payment.</summary>
    Private Function ValidateChequeAndPayAmountForPendingImage() As Boolean
        If _datePayment.EditValue Is Nothing OrElse String.IsNullOrWhiteSpace(_datePayment.Text) Then
            XtraMessageBox.Show(Me, If(Eng, "Payment date is required.", "تاريخ الدفع مطلوب."), If(Eng, "Validation", "تنبيه"), MessageBoxButtons.OK)
            Return False
        End If
        Dim payVal As Decimal = IntegerMoneyEditorFocus.DecimalFromDecimal2MoneyEdit(_spinAmount)
        If payVal <= 0D Then
            XtraMessageBox.Show(Me, If(Eng, "Amount must be greater than zero.", "المبلغ يجب أن يكون أكبر من صفر."), If(Eng, "Validation", "تنبيه"), MessageBoxButtons.OK)
            Return False
        End If
        Return ValidateChequeFields()
    End Function

    Private Sub ResetChequeImageCaptureState()
        If _chequeCaptureUiLocked Then ClearChequeImageCaptureUiLock()
        _pendingChequeImageSourcePath = Nothing
        _pendingChequeImageIsDisposableScan = False
    End Sub

    Private Sub ApplyChequeImageCaptureUiLock()
        _chequeCaptureUiLocked = True
        btnBrowse.Enabled = False
        btnScan.Enabled = False
        btnScanAndPay.Enabled = False
        btnResetChqs.Enabled = False
        ChkDrive.Enabled = False
        GroupCheques.Enabled = False
        _cmbMethod.Enabled = False
        _spinAmount.Enabled = False
        _datePayment.Enabled = False
        _btnEditPay.Enabled = False
        _btnPay.Enabled = True
    End Sub

    Private Sub ClearChequeImageCaptureUiLock()
        If Not _chequeCaptureUiLocked Then Return
        _chequeCaptureUiLocked = False
        btnBrowse.Enabled = True
        btnScan.Enabled = True
        btnScanAndPay.Enabled = True
        btnResetChqs.Enabled = True
        ChkDrive.Enabled = True
        GroupCheques.Enabled = True
        _cmbMethod.Enabled = True
        _spinAmount.Enabled = True
        _datePayment.Enabled = True
        If Not _editingPaymentId.HasValue Then _btnEditPay.Enabled = True
    End Sub

    Private Sub EndPaymentEditMode()
        _editingPaymentId = Nothing
        _editingPaymentBase = Nothing
        _btnPay.Text = _btnPayCaptionAdd
        _btnEditPay.Enabled = True
        ResetChequeImageCaptureState()
    End Sub

    Private Sub SelectPaymentMethodInCombo(displayOrStoredMethod As String)
        Dim target = PaymentMethodStorage.NormalizeForPaymentsTable(If(displayOrStoredMethod, ""))
        For i = 0 To _cmbMethod.Properties.Items.Count - 1
            Dim rawIt = _cmbMethod.Properties.Items(i)
            Dim itemText = If(rawIt IsNot Nothing, rawIt.ToString(), "")
            If String.Equals(PaymentMethodStorage.NormalizeForPaymentsTable(itemText), target, StringComparison.OrdinalIgnoreCase) Then
                _cmbMethod.SelectedIndex = i
                Return
            End If
        Next
        If _cmbMethod.Properties.Items.Count > 0 Then _cmbMethod.SelectedIndex = 0
    End Sub

    Private Sub ReloadPaymentFieldsFromEditingBase()
        If _editingPaymentBase Is Nothing Then Return
        Dim pay = _editingPaymentBase
        _spinAmount.EditValue = pay.Amount
        _datePayment.DateTime = pay.PaymentDate
        SelectPaymentMethodInCombo(pay.PaymentMethod)
        UpdateChequeUi()
        If PaymentMethodStorage.NormalizeForPaymentsTable(If(pay.PaymentMethod, "")).Equals("Cheque", StringComparison.OrdinalIgnoreCase) Then
            txtChqOwner.Text = If(pay.ChqOwner, "")
            txtAccountNumber.Text = If(pay.AccountNumber, "")
            txtChqNumber.Text = If(pay.ChqNumber, "")
            dtChqDueDate.EditValue = If(pay.ChqDueDate.HasValue, pay.ChqDueDate.Value, Nothing)
            txtChqBank.Text = If(pay.ChqBank, "")
            txtChqValue.EditValue = pay.Amount
        Else
            txtChqOwner.Text = ""
            txtAccountNumber.Text = ""
            txtChqNumber.Text = ""
            dtChqDueDate.EditValue = Nothing
            txtChqBank.Text = ""
            txtChqValue.EditValue = 0D
        End If
    End Sub

    Private Function BuildEditingPaymentStub() As Payment
        Return New Payment With {
            .PaymentID = _editingPaymentId.GetValueOrDefault(),
            .InvoiceID = _editingPaymentBase.InvoiceID,
            .SupplierID = _editingPaymentBase.SupplierID,
            .SupplierName = _editingPaymentBase.SupplierName,
            .ChqNumber = NormalizeChqNumber(txtChqNumber.Text)
        }
    End Function

    ''' <summary>Twain scan to a disposable file under the invoice Cheques folder (add flow — PaymentID not yet saved).</summary>
    Private Function RunScanSupplierChequePending(inv As BuyInvoice) As Boolean
        Try
            Dim folder = GetSupplierChequeFolder(inv)
            If String.IsNullOrWhiteSpace(folder) Then Return False
            If Not Directory.Exists(folder) Then Directory.CreateDirectory(folder)
            Dim parentHandle As IntPtr = IntPtr.Zero
            Dim topForm = Me.FindForm()
            If topForm IsNot Nothing Then parentHandle = topForm.Handle
            Dim files = TwainOperations.ScanImagesToFolder(ImageType:=".jpg",
                                                          CloseScannerUIAfterImageTransfer:=True,
                                                          ScannerInfo:="",
                                                          DestinationFolder:=folder,
                                                          ParentWindowHandle:=parentHandle)
            If files Is Nothing OrElse files.Count = 0 Then
                XtraMessageBox.Show(Me, If(Eng, "No pages were scanned.", "لم يتم مسح أي صفحات."), If(Eng, "Scan", "مسح"), MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return False
            End If
            _pendingChequeImageSourcePath = files(0)
            _pendingChequeImageIsDisposableScan = True
            Dim fullFirst = Path.GetFullPath(files(0))
            For Each f In files
                If String.IsNullOrWhiteSpace(f) Then Continue For
                Try
                    If Not String.Equals(Path.GetFullPath(f), fullFirst, StringComparison.OrdinalIgnoreCase) Then File.Delete(f)
                Catch
                End Try
            Next
            Dim msg = If(Eng, "Cheque scanned. Click Add Payment to save and link this image.", "تم مسح الشيك. اضغط إضافة الدفع لحفظ المبلغ وربط الصورة.")
            If files.Count > 1 Then msg &= If(Eng, $" Only the first of {files.Count} pages was kept.", $" تم الاحتفاظ بالصفحة الأولى من {files.Count} صفحات فقط.")
            XtraMessageBox.Show(Me, msg, If(Eng, "Scan", "مسح"), MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return True
        Catch ex As Exception
            XtraMessageBox.Show(Me, If(Eng, "Error while scanning: ", "خطأ أثناء المسح: ") & ex.Message, If(Eng, "Scan", "مسح"), MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End Try
    End Function

    Private Sub ApplyChequeFields(pay As Payment)
        If Not IsChequeMethod() Then
            pay.ChqOwner = Nothing
            pay.AccountNumber = Nothing
            pay.ChqNumber = Nothing
            pay.ChqDueDate = Nothing
            pay.ChqBank = Nothing
            Return
        End If
        pay.ChqOwner = txtChqOwner.Text.Trim()
        pay.AccountNumber = txtAccountNumber.Text.Trim()
        pay.ChqNumber = NormalizeChqNumber(txtChqNumber.Text)
        pay.ChqDueDate = CType(dtChqDueDate.EditValue, DateTime)
        pay.ChqBank = txtChqBank.Text.Trim()
    End Sub

    Private Sub ClearPaymentEntryFields()
        _spinAmount.EditValue = 0D
        txtChqValue.EditValue = 0D
        txtChqOwner.Text = ""
        txtAccountNumber.Text = ""
        txtChqNumber.Text = ""
        dtChqDueDate.EditValue = Nothing
        txtChqBank.Text = ""
        PrefillChequeOwnerFromInvoice()
    End Sub

    Private Async Function TrySaveEditedPaymentAsync() As Task(Of Boolean)
        If Not _editingPaymentId.HasValue OrElse _editingPaymentBase Is Nothing Then Return False
        If _cmbMethod.SelectedIndex < 0 Then
            XtraMessageBox.Show(Me, If(Eng, "Select payment method.", "يرجى اختيار طريقة الدفع."), If(Eng, "Validation", "تنبيه"), MessageBoxButtons.OK)
            Return False
        End If
        Dim payVal As Decimal = IntegerMoneyEditorFocus.DecimalFromDecimal2MoneyEdit(_spinAmount)
        If payVal <= 0D Then
            XtraMessageBox.Show(Me, If(Eng, "Amount must be greater than zero.", "المبلغ يجب أن يكون أكبر من صفر."), If(Eng, "Validation", "تنبيه"), MessageBoxButtons.OK)
            Return False
        End If
        If IsChequeMethod() AndAlso Not ValidateChequeFields() Then Return False

        Dim pay As New Payment With {
            .PaymentID = _editingPaymentId.Value,
            .InvoiceID = _editingPaymentBase.InvoiceID,
            .SupplierID = _editingPaymentBase.SupplierID,
            .Amount = payVal,
            .PaymentDate = _datePayment.DateTime.Date,
            .PaymentMethod = PaymentMethodStorage.NormalizeForPaymentsTable(_cmbMethod.Text)
        }
        ApplyChequeFields(pay)

        Try
            Await _paymentService.UpdatePaymentAsync(pay)
            ClearChequeImageCaptureUiLock()
            LoadInvoices()
            Return True
        Catch ex As Exception
            XtraMessageBox.Show(Me, If(Eng, $"Error saving payment: {ex.Message}", $"خطأ عند حفظ الدفعة: {ex.Message}"), If(Eng, "Error", "خطأ"), MessageBoxButtons.OK)
            Return False
        End Try
    End Function

    ''' <returns>New PaymentID on success; Nothing on validation/error.</returns>
    Private Async Function TryCommitPaymentAsync(Optional clearEntryFieldsAfterSuccess As Boolean = True) As Task(Of Integer?)
        Dim inv = TryCast(_viewInvoices.GetFocusedRow(), BuyInvoice)
        If inv Is Nothing Then Return Nothing
        If _cmbMethod.SelectedIndex < 0 Then
            XtraMessageBox.Show(Me, If(Eng, "Select payment method.", "يرجى اختيار طريقة الدفع."), If(Eng, "Validation", "تنبيه"), MessageBoxButtons.OK)
            Return Nothing
        End If

        Dim payVal As Decimal = IntegerMoneyEditorFocus.DecimalFromDecimal2MoneyEdit(_spinAmount)
        If payVal <= 0 Then
            XtraMessageBox.Show(Me, If(Eng, "Amount must be greater than zero.", "المبلغ يجب أن يكون أكبر من صفر."), If(Eng, "Validation", "تنبيه"), MessageBoxButtons.OK)
            Return Nothing
        End If

        If IsChequeMethod() Then
            If Not ValidateChequeFields() Then Return Nothing
        End If

        Dim pay As New Payment With {
            .InvoiceID = inv.InvoiceID,
            .SupplierID = inv.SupplierID,
            .Amount = payVal,
            .PaymentDate = _datePayment.DateTime.Date,
            .PaymentMethod = PaymentMethodStorage.NormalizeForPaymentsTable(_cmbMethod.Text)
        }
        ApplyChequeFields(pay)

        Try
            Dim newId = Await _paymentService.AddPaymentAsync(pay)
            If newId <= 0 Then
                XtraMessageBox.Show(Me, If(Eng, "Could not read new payment ID after insert.", "تعذر قراءة رقم الدفعة بعد الإدراج."), If(Eng, "Error", "خطأ"), MessageBoxButtons.OK)
                Return Nothing
            End If

            If IsChequeMethod() AndAlso Not String.IsNullOrWhiteSpace(_pendingChequeImageSourcePath) AndAlso File.Exists(_pendingChequeImageSourcePath) Then
                Try
                    CopyChequeAttachmentToLinkedPath(inv, newId, txtChqNumber.Text.Trim(), _pendingChequeImageSourcePath)
                Catch ex As Exception
                    XtraMessageBox.Show(Me,
                        If(Eng, "Payment was saved but linking the cheque image failed: ", "تم حفظ الدفعة وفشل ربط صورة الشيك: ") & ex.Message,
                        If(Eng, "Error", "خطأ"), MessageBoxButtons.OK, MessageBoxIcon.Warning)
                End Try
                Try
                    If _pendingChequeImageIsDisposableScan AndAlso File.Exists(_pendingChequeImageSourcePath) Then File.Delete(_pendingChequeImageSourcePath)
                Catch
                End Try
            End If
            _pendingChequeImageSourcePath = Nothing
            _pendingChequeImageIsDisposableScan = False
            ClearChequeImageCaptureUiLock()

            If clearEntryFieldsAfterSuccess Then ClearPaymentEntryFields()
            LoadInvoices()
            Return newId
        Catch ex As Exception
            XtraMessageBox.Show(Me, If(Eng, $"Payment failed: {ex.Message}", $"فشل تسجيل الدفعة: {ex.Message}"), If(Eng, "Error", "خطأ"), MessageBoxButtons.OK)
            Return Nothing
        End Try
    End Function

    Private Async Sub OnAddPayment(sender As Object, e As EventArgs)
        If _editingPaymentId.HasValue Then
            If Await TrySaveEditedPaymentAsync() Then
                EndPaymentEditMode()
                ClearPaymentEntryFields()
            End If
            Return
        End If
        Await TryCommitPaymentAsync()
    End Sub

    Private Function GetSupplierChequeFolder(inv As BuyInvoice) As String
        If inv Is Nothing Then Return ""
        Dim appDir = Application.StartupPath
        If String.IsNullOrEmpty(appDir) Then Return ""
        Return Path.Combine(appDir, "Images", "Suppliers", "Supplier" & inv.SupplierID.ToString(), "Invoice" & inv.InvoiceID.ToString(), "Cheques")
    End Function

    Private Function GetSupplierDriveOwnerLabel(inv As BuyInvoice) As String
        If inv Is Nothing Then Return "Supplier"
        Dim n = If(inv.SupplierName, "").Trim()
        If String.IsNullOrWhiteSpace(n) Then Return "Supplier_" & inv.SupplierID.ToString()
        Return n
    End Function

    Private Sub ChkDrive_CheckedChanged(sender As Object, e As EventArgs) Handles ChkDrive.CheckedChanged
        If _suppressChkDrive Then Return
        If Not ChkDrive.Checked Then Return
        If Not PatientDriveMirror.TryConfirmCloudPatientFiles(Me) Then
            _suppressChkDrive = True
            ChkDrive.Checked = False
            _suppressChkDrive = False
        End If
    End Sub

    Private Sub OpenFolder(path As String)
        If String.IsNullOrWhiteSpace(path) OrElse Not Directory.Exists(path) Then Return
        Try
            Process.Start(New ProcessStartInfo("explorer.exe", path) With {.UseShellExecute = True})
        Catch
        End Try
    End Sub

    Private Async Sub btnBrowse_Click(sender As Object, e As EventArgs) Handles btnBrowse.Click
        Dim inv = TryCast(_viewInvoices.GetFocusedRow(), BuyInvoice)
        If inv Is Nothing Then
            XtraMessageBox.Show(Me, If(Eng, "Select an invoice first.", "يرجى اختيار فاتورة أولا."), If(Eng, "Cheques", "الشيكات"), MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        If Not IsChequeMethod() Then
            Dim folderOpen = GetSupplierChequeFolder(inv)
            If String.IsNullOrEmpty(folderOpen) Then Return
            If Not Directory.Exists(folderOpen) Then Directory.CreateDirectory(folderOpen)
            OpenFolder(folderOpen)
            Return
        End If

        If _editingPaymentId.HasValue AndAlso _editingPaymentBase IsNot Nothing Then
            Dim stubEdit = BuildEditingPaymentStub()
            If HasLinkedChequeImageForPayment(stubEdit) Then
                Dim fo = GetSupplierChequeFolderForPayment(stubEdit)
                If Not String.IsNullOrWhiteSpace(fo) Then
                    If Not Directory.Exists(fo) Then Directory.CreateDirectory(fo)
                    OpenFolder(fo)
                End If
                Return
            End If
            If Not ValidateChequeFields() Then Return
            Dim folderEd = GetSupplierChequeFolderForPayment(stubEdit)
            If String.IsNullOrWhiteSpace(folderEd) Then Return
            If Not Directory.Exists(folderEd) Then Directory.CreateDirectory(folderEd)
            Using dlg As New OpenFileDialog()
                dlg.Title = If(Eng, "Link cheque image to this payment", "ربط صورة الشيك بهذه الدفعة")
                dlg.Filter = If(Eng,
                    "Images (*.jpg;*.jpeg;*.png;*.bmp;*.tif;*.tiff)|*.jpg;*.jpeg;*.png;*.bmp;*.tif;*.tiff|PDF (*.pdf)|*.pdf|All files|*.*",
                    "صور|*.jpg;*.jpeg;*.png;*.bmp;*.tif;*.tiff|PDF|*.pdf|الملفات|*.*")
                dlg.Multiselect = False
                If dlg.ShowDialog(Me) <> DialogResult.OK OrElse String.IsNullOrWhiteSpace(dlg.FileName) Then Return
                Try
                    CopyChequeAttachmentToLinkedPathForPayment(stubEdit, dlg.FileName)
                    LoadInvoices()
                    RefreshScannedAndAttachedForCurrentTabs()
                    XtraMessageBox.Show(Me,
                        If(Eng, "Cheque image linked to this payment.", "تم ربط صورة الشيك بهذه الدفعة."),
                        If(Eng, "Cheques", "الشيكات"), MessageBoxButtons.OK, MessageBoxIcon.Information)
                    ApplyChequeImageCaptureUiLock()
                Catch ex As Exception
                    XtraMessageBox.Show(Me, If(Eng, "Could not save file: ", "تعذر حفظ الملف: ") & ex.Message, If(Eng, "Error", "خطأ"), MessageBoxButtons.OK, MessageBoxIcon.Warning)
                End Try
            End Using
            Return
        End If

        Dim payFocused = TryGetPaymentRowForChequeActions()
        If payFocused IsNot Nothing AndAlso payFocused.InvoiceID = inv.InvoiceID AndAlso PaymentMethodIsCheque(payFocused) AndAlso
           Not HasLinkedChequeImageForPayment(payFocused) Then
            If String.IsNullOrWhiteSpace(payFocused.ChqNumber) Then
                XtraMessageBox.Show(Me, If(Eng, "Selected cheque payment has no cheque number in the database.", "الدفعة المحددة لا تحتوي رقم شيك في السجل."), If(Eng, "Cheques", "الشيكات"), MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If
            Using dlg As New OpenFileDialog()
                dlg.Title = If(Eng, "Link cheque image to selected payment", "ربط صورة الشيك بالدفعة المحددة")
                dlg.Filter = If(Eng,
                    "Images (*.jpg;*.jpeg;*.png;*.bmp;*.tif;*.tiff)|*.jpg;*.jpeg;*.png;*.bmp;*.tif;*.tiff|PDF (*.pdf)|*.pdf|All files|*.*",
                    "صور|*.jpg;*.jpeg;*.png;*.bmp;*.tif;*.tiff|PDF|*.pdf|الملفات|*.*")
                dlg.Multiselect = False
                If dlg.ShowDialog(Me) <> DialogResult.OK OrElse String.IsNullOrWhiteSpace(dlg.FileName) Then Return
                Try
                    CopyChequeAttachmentToLinkedPathForPayment(payFocused, dlg.FileName)
                    LoadInvoices()
                    RefreshScannedAndAttachedForCurrentTabs()
                    XtraMessageBox.Show(Me,
                        If(Eng, "Cheque image linked to this payment.", "تم ربط صورة الشيك بهذه الدفعة."),
                        If(Eng, "Cheques", "الشيكات"), MessageBoxButtons.OK, MessageBoxIcon.Information)
                Catch ex As Exception
                    XtraMessageBox.Show(Me, If(Eng, "Could not save file: ", "تعذر حفظ الملف: ") & ex.Message, If(Eng, "Error", "خطأ"), MessageBoxButtons.OK, MessageBoxIcon.Warning)
                End Try
            End Using
            Return
        End If

        If Not ValidateChequeAndPayAmountForPendingImage() Then Return
        Dim folder = GetSupplierChequeFolder(inv)
        If String.IsNullOrEmpty(folder) Then Return
        If Not Directory.Exists(folder) Then Directory.CreateDirectory(folder)
        Using dlgNew As New OpenFileDialog()
            dlgNew.Title = If(Eng, "Attach cheque image", "إرفاق صورة الشيك")
            dlgNew.Filter = If(Eng,
                "Images (*.jpg;*.jpeg;*.png;*.bmp;*.tif;*.tiff)|*.jpg;*.jpeg;*.png;*.bmp;*.tif;*.tiff|PDF (*.pdf)|*.pdf|All files|*.*",
                "صور|*.jpg;*.jpeg;*.png;*.bmp;*.tif;*.tiff|PDF|*.pdf|الملفات|*.*")
            dlgNew.Multiselect = False
            If dlgNew.ShowDialog(Me) <> DialogResult.OK OrElse String.IsNullOrWhiteSpace(dlgNew.FileName) Then Return
            _pendingChequeImageSourcePath = dlgNew.FileName
            _pendingChequeImageIsDisposableScan = False
            XtraMessageBox.Show(Me,
                If(Eng, "Image selected. Click Add Payment to save and link this file.", "تم اختيار الصورة. اضغط إضافة الدفع لحفظ المبلغ وربط الملف."),
                If(Eng, "Cheques", "الشيكات"), MessageBoxButtons.OK, MessageBoxIcon.Information)
            ApplyChequeImageCaptureUiLock()
        End Using
    End Sub

    Private Sub btnResetChqs_Click(sender As Object, e As EventArgs) Handles btnResetChqs.Click
        If _pendingChequeImageIsDisposableScan AndAlso Not String.IsNullOrWhiteSpace(_pendingChequeImageSourcePath) Then
            Try
                If File.Exists(_pendingChequeImageSourcePath) Then File.Delete(_pendingChequeImageSourcePath)
            Catch
            End Try
        End If
        ResetChequeImageCaptureState()
        If _editingPaymentId.HasValue AndAlso _editingPaymentBase IsNot Nothing Then
            ReloadPaymentFieldsFromEditingBase()
            Return
        End If
        txtAccountNumber.Text = ""
        txtChqBank.Text = ""
        txtChqNumber.Text = ""
        txtChqOwner.Text = ""
        txtChqValue.EditValue = 0D
        dtChqDueDate.EditValue = Nothing
        _spinAmount.EditValue = 0D
        _datePayment.DateTime = Date.Today
        PrefillChequeOwnerFromInvoice()
    End Sub

    ''' <summary>Scan from Twain, then rename/link first page to Pay[paymentId]_Chq… (same convention as Browse).</summary>
    Private Function RunScanChequeImage(pay As Payment) As Boolean
        If pay Is Nothing OrElse pay.PaymentID <= 0 Then
            XtraMessageBox.Show(Me, If(Eng, "Select an invoice and payment first.", "يرجى اختيار الفاتورة والدفعة."), If(Eng, "Scan", "مسح"), MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return False
        End If
        If String.IsNullOrWhiteSpace(pay.ChqNumber) Then
            XtraMessageBox.Show(Me,
                If(Eng, "Cheque number is required to name the linked image.", "رقم الشيك مطلوب لتسمية الملف المربوط."),
                If(Eng, "Scan", "مسح"), MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return False
        End If
        Try
            Dim folder = GetSupplierChequeFolderForPayment(pay)
            If String.IsNullOrEmpty(folder) Then Return False
            If Not Directory.Exists(folder) Then Directory.CreateDirectory(folder)

            Dim parentHandle As IntPtr = IntPtr.Zero
            Dim topForm = Me.FindForm()
            If topForm IsNot Nothing Then parentHandle = topForm.Handle

            Dim files = TwainOperations.ScanImagesToFolder(ImageType:=".jpg",
                                                          CloseScannerUIAfterImageTransfer:=True,
                                                          ScannerInfo:="",
                                                          DestinationFolder:=folder,
                                                          ParentWindowHandle:=parentHandle)
            If files Is Nothing OrElse files.Count = 0 Then
                XtraMessageBox.Show(Me, If(Eng, "No pages were scanned.", "لم يتم مسح أي صفحات."), If(Eng, "Scan", "مسح"), MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return False
            End If

            CopyChequeAttachmentToLinkedPathForPayment(pay, files(0))
            Dim ext0 = Path.GetExtension(files(0))
            If String.IsNullOrWhiteSpace(ext0) Then ext0 = ".jpg"
            Dim linkedPath = Path.Combine(folder, "Pay" & pay.PaymentID.ToString() & "_Chq" & SafeChequeFileNamePart(NormalizeChqNumber(pay.ChqNumber)) & ext0)
            If Not String.IsNullOrWhiteSpace(linkedPath) Then
                Dim fullLinked = Path.GetFullPath(linkedPath)
                For Each f In files
                    If String.IsNullOrWhiteSpace(f) Then Continue For
                    Try
                        If Not String.Equals(Path.GetFullPath(f), fullLinked, StringComparison.OrdinalIgnoreCase) Then
                            File.Delete(f)
                        End If
                    Catch
                    End Try
                Next
            End If

            Dim msg = If(Eng, "Cheque scanned and linked to payment.", "تم مسح الشيك وربطه بالدفعة.")
            If files.Count > 1 Then
                msg &= If(Eng, $" Only the first of {files.Count} pages was linked.", $" تم ربط الصفحة الأولى من {files.Count} صفحات فقط.")
            End If
            XtraMessageBox.Show(Me, msg, If(Eng, "Scan", "مسح"), MessageBoxButtons.OK, MessageBoxIcon.Information)
            RefreshScannedAndAttachedForCurrentTabs()
            Return True
        Catch ex As Exception
            XtraMessageBox.Show(Me, If(Eng, "Error while scanning: ", "خطأ أثناء المسح: ") & ex.Message, If(Eng, "Scan", "مسح"), MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End Try
    End Function

    Private Sub btnScan_Click(sender As Object, e As EventArgs) Handles btnScan.Click
        If Not IsChequeMethod() Then
            XtraMessageBox.Show(Me, If(Eng, "Select Cheque as payment method before scanning.", "اختر طريقة الدفع شيك قبل المسح."), If(Eng, "Scan", "مسح"), MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If
        Dim inv = TryCast(_viewInvoices.GetFocusedRow(), BuyInvoice)
        If inv Is Nothing Then
            XtraMessageBox.Show(Me, If(Eng, "Select an invoice first.", "يرجى اختيار فاتورة أولا."), If(Eng, "Scan", "مسح"), MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        If _editingPaymentId.HasValue AndAlso _editingPaymentBase IsNot Nothing Then
            Dim stubEdit = BuildEditingPaymentStub()
            If HasLinkedChequeImageForPayment(stubEdit) Then
                XtraMessageBox.Show(Me,
                    If(Eng, "This payment already has a linked cheque image.", "هذه الدفعة مرتبطة بصورة شيك بالفعل."),
                    If(Eng, "Scan", "مسح"), MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return
            End If
            If Not ValidateChequeFields() Then Return
            If RunScanChequeImage(stubEdit) Then ApplyChequeImageCaptureUiLock()
            Return
        End If

        Dim payFocused = TryGetPaymentRowForChequeActions()
        If payFocused IsNot Nothing AndAlso payFocused.InvoiceID = inv.InvoiceID AndAlso PaymentMethodIsCheque(payFocused) Then
            If HasLinkedChequeImageForPayment(payFocused) Then
                XtraMessageBox.Show(Me,
                    If(Eng, "This payment already has a linked cheque image.", "هذه الدفعة مرتبطة بصورة شيك بالفعل."),
                    If(Eng, "Scan", "مسح"), MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return
            End If
            If String.IsNullOrWhiteSpace(payFocused.ChqNumber) Then
                XtraMessageBox.Show(Me, If(Eng, "Selected payment has no cheque number.", "الدفعة المحددة بلا رقم شيك."), If(Eng, "Scan", "مسح"), MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If
            RunScanChequeImage(payFocused)
            Return
        End If

        If Not ValidateChequeAndPayAmountForPendingImage() Then Return
        If RunScanSupplierChequePending(inv) Then ApplyChequeImageCaptureUiLock()
    End Sub

    Private Async Sub btnScanAndPay_Click(sender As Object, e As EventArgs) Handles btnScanAndPay.Click
        If Not IsChequeMethod() Then
            XtraMessageBox.Show(Me, If(Eng, "Select Cheque as payment method for Scan && Pay.", "اختر طريقة الدفع شيك للمسح والدفع."), If(Eng, "Validation", "تنبيه"), MessageBoxButtons.OK)
            Return
        End If
        Dim invForScan = TryCast(_viewInvoices.GetFocusedRow(), BuyInvoice)
        If invForScan Is Nothing Then
            XtraMessageBox.Show(Me, If(Eng, "Select an invoice first.", "يرجى اختيار فاتورة أولا."), If(Eng, "Validation", "تنبيه"), MessageBoxButtons.OK)
            Return
        End If

        If _editingPaymentId.HasValue Then
            If Not ValidateChequeFields() Then Return
            If Not Await TrySaveEditedPaymentAsync() Then Return
            Dim stubAfterSave = BuildEditingPaymentStub()
            If RunScanChequeImage(stubAfterSave) Then
                EndPaymentEditMode()
                ClearPaymentEntryFields()
            End If
            Return
        End If

        ResetChequeImageCaptureState()
        If Not ValidateChequeAndPayAmountForPendingImage() Then Return
        Dim chqNumForFile = txtChqNumber.Text
        Dim newPaymentId = Await TryCommitPaymentAsync(clearEntryFieldsAfterSuccess:=False)
        If newPaymentId.HasValue Then
            Dim payStub As New Payment With {
                .PaymentID = newPaymentId.Value,
                .InvoiceID = invForScan.InvoiceID,
                .SupplierID = invForScan.SupplierID,
                .SupplierName = invForScan.SupplierName,
                .ChqNumber = NormalizeChqNumber(chqNumForFile)
            }
            RunScanChequeImage(payStub)
            ClearPaymentEntryFields()
            ClearChequeImageCaptureUiLock()
        End If
    End Sub

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
        If col IsNot Nothing Then col.Caption = If(Eng, "Total Amount", "الإجمالي")
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
        If col IsNot Nothing Then col.Caption = If(Eng, "Date", "التاريخ")
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

    Private Sub txtChq_KeyDown(sender As Object, e As KeyEventArgs) Handles txtChqNumber.KeyDown, txtAccountNumber.KeyDown
        ' Allow control keys: backspace, delete, arrows, tab, home/end, etc.
        If e.KeyCode = Keys.Back OrElse
           e.KeyCode = Keys.Delete OrElse
           e.KeyCode = Keys.Left OrElse
           e.KeyCode = Keys.Right OrElse
           e.KeyCode = Keys.Up OrElse
           e.KeyCode = Keys.Down OrElse
           e.KeyCode = Keys.Tab OrElse
           e.KeyCode = Keys.Home OrElse
           e.KeyCode = Keys.End Then
            Return
        End If

        ' Allow digits only (top row or numpad)
        Dim isTopRowDigit As Boolean = (e.KeyCode >= Keys.D0 AndAlso e.KeyCode <= Keys.D9)
        Dim isNumPadDigit As Boolean = (e.KeyCode >= Keys.NumPad0 AndAlso e.KeyCode <= Keys.NumPad9)

        ' Block Shift-modified digits (to avoid !, @, etc.)
        If (isTopRowDigit OrElse isNumPadDigit) AndAlso (Not e.Shift) Then
            Return
        End If

        ' Otherwise block the key
        e.SuppressKeyPress = True
        e.Handled = True
    End Sub

    Private Sub _btnEditPay_Click(sender As Object, e As EventArgs) Handles _btnEditPay.Click
        Dim pay = TryGetPaymentRowForChequeActions()
        If pay Is Nothing Then pay = TryCast(_viewPayments.GetFocusedRow(), Payment)
        If pay Is Nothing Then
            XtraMessageBox.Show(Me, If(Eng, "Please select a payment first.", "يرجى اختيار دفعة أولا."), If(Eng, "Validation", "تنبيه"), MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If
        If _chequeCaptureUiLocked Then
            XtraMessageBox.Show(Me, If(Eng, "Finish saving the current cheque action first (Add Payment / Save).", "أكمل حفظ عملية الشيك الحالية (إضافة الدفع / حفظ) أولا."), If(Eng, "Validation", "تنبيه"), MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If
        ResetChequeImageCaptureState()
        _editingPaymentBase = New Payment With {
            .PaymentID = pay.PaymentID,
            .InvoiceID = pay.InvoiceID,
            .SupplierID = pay.SupplierID,
            .SupplierName = pay.SupplierName,
            .Amount = pay.Amount,
            .PaymentDate = pay.PaymentDate,
            .PaymentMethod = pay.PaymentMethod,
            .ChqOwner = pay.ChqOwner,
            .AccountNumber = pay.AccountNumber,
            .ChqNumber = pay.ChqNumber,
            .ChqDueDate = pay.ChqDueDate,
            .ChqBank = pay.ChqBank
        }
        _editingPaymentId = pay.PaymentID
        ReloadPaymentFieldsFromEditingBase()
        _btnPay.Text = _btnPayCaptionSave
        _btnEditPay.Enabled = False
    End Sub
End Class

''' <summary>Row for supplier invoice attachments grid on StockSupplierPaymentsForm.</summary>
Public Class SupplierInvoiceAttachmentFileRow
    Public Property FullPath As String
    Public Property Size As Long
    Public Property Type As String
    Public ReadOnly Property FormattedSize As String
        Get
            Return StockSupplierPaymentsForm.FormatFileSize(Size)
        End Get
    End Property
End Class
