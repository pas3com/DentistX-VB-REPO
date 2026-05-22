Imports System.ComponentModel
Imports System.Data.SqlClient
Imports System.Linq
Imports System.Threading.Tasks
Imports DevExpress.XtraEditors
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Views.Grid

Public Class StockInvoicesForm
    Inherits XtraForm

    Private ReadOnly _invoiceRepo As BuyInvoiceRepo
    Private ReadOnly _supplierRepo As SupplierRepository

    Private _suppliers As List(Of Supplier)
    Private _binding As BindingList(Of BuyInvoice)

    Private ReadOnly _defaultFont As Font = New Font("Calibri", 10, FontStyle.Bold)

    Public Sub New()
        InitializeComponent()

        Dim conn = DentistXDATA.GetConnection.ConnectionString
        _invoiceRepo = New BuyInvoiceRepo(conn)
        _supplierRepo = New SupplierRepository(conn)

        Text = If(Eng, "Buy Invoices", "فواتير الشراء")

        StartPosition = FormStartPosition.CenterScreen
        Font = _defaultFont
        If Not Eng Then
            RightToLeft = RightToLeft.Yes
            RightToLeftLayout = True
        End If

        _dateFrom.DateTime = Date.Today.AddMonths(-1)
        _dateTo.DateTime = Date.Today.AddDays(1)

        AddHandler Load, AddressOf OnFormLoad
        AddHandler _btnRefresh.Click, AddressOf OnRefresh
        AddHandler _cmbSupplier.SelectedIndexChanged, AddressOf OnRefresh
        AddHandler _dateFrom.EditValueChanged, AddressOf OnRefresh
        AddHandler _dateTo.EditValueChanged, AddressOf OnRefresh
        AddHandler _txtInvoiceNo.EditValueChanged, AddressOf OnRefresh
        AddHandler _btnAdd.Click, AddressOf OnAdd
        AddHandler _btnEdit.Click, AddressOf OnEdit
        AddHandler _btnDelete.Click, AddressOf OnDelete
    End Sub

    Private Sub OnFormLoad(sender As Object, e As EventArgs)
        LoadSuppliers()
        LoadData()
    End Sub

    Private Sub LoadSuppliers()
        _suppliers = _supplierRepo.GetAll().ToList()
        _cmbSupplier.Properties.Items.Clear()
        _cmbSupplier.Properties.Items.Add(If(Eng, "All suppliers", "كل الموردين"))
        _cmbSupplier.Properties.Items.AddRange(_suppliers.Select(Function(s) s.SupplierName).ToArray())
        _cmbSupplier.SelectedIndex = 0
    End Sub

    Private Sub OnRefresh(sender As Object, e As EventArgs)
        LoadData()
    End Sub

    Private Sub OnAdd(sender As Object, e As EventArgs)
        Using f As New StockBuyInvoiceForm()
            f.ShowDialog(Me)
        End Using
        LoadData()
    End Sub

    Private Async Sub OnEdit(sender As Object, e As EventArgs)
        Dim row = TryCast(_view.GetFocusedRow(), BuyInvoice)
        If row Is Nothing Then Return
        Dim inv = _invoiceRepo.GetById(row.InvoiceID)
        If inv Is Nothing Then
            XtraMessageBox.Show(Me,
                                    If(Eng, "Invoice not found (it may have been deleted).", "الفاتورة غير موجودة (ربما حُذفت)."),
                                    If(Eng, "Edit", "تعديل"),
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information)
            LoadData()
            Return
        End If
        Using dlg As New BuyInvoiceEditForm(inv, _supplierRepo.GetAll().ToList())
            If dlg.ShowDialog(Me) = DialogResult.OK Then
                Await _invoiceRepo.UpdateAsync(inv)
                LoadData()
            End If
        End Using
    End Sub

    Private Async Sub OnDelete(sender As Object, e As EventArgs)
        Dim inv = TryCast(_view.GetFocusedRow(), BuyInvoice)
        If inv Is Nothing Then Return
        If XtraMessageBox.Show(Me,
                                   If(Eng, "Delete selected buy invoice?", "حذف فاتورة الشراء المحددة؟"),
                                   If(Eng, "Confirm", "تأكيد"),
                                   MessageBoxButtons.YesNo) = DialogResult.Yes Then
            Try
                Await _invoiceRepo.DeleteAsync(inv.InvoiceID)
                LoadData()
            Catch ex As SqlException
                If ex.Number = 547 Then
                    XtraMessageBox.Show(Me,
                                            If(Eng,
                                               "You cannot delete this invoice because it has line items, payments, or other linked records.",
                                               "لا يمكن حذف هذه الفاتورة لأن بها بنود أو دفعات أو سجلات مرتبطة."),
                                            If(Eng, "Delete not allowed", "لا يمكن الحذف"),
                                            MessageBoxButtons.OK,
                                            MessageBoxIcon.Warning)
                Else
                    XtraMessageBox.Show(Me,
                                            If(Eng, $"Delete failed: {ex.Message}", $"فشل الحذف: {ex.Message}"),
                                            If(Eng, "Error", "خطأ"),
                                            MessageBoxButtons.OK,
                                            MessageBoxIcon.Error)
                End If
            End Try
        End If
    End Sub

    Private Sub LoadData()
        Dim fromDate = _dateFrom.DateTime.Date
        Dim toDate = _dateTo.DateTime.Date

        Dim list As List(Of BuyInvoice)

        If _cmbSupplier.SelectedIndex > 0 AndAlso _suppliers IsNot Nothing AndAlso _cmbSupplier.SelectedIndex <= _suppliers.Count Then
            Dim supplierId = _suppliers(_cmbSupplier.SelectedIndex - 1).SupplierID
            list = _invoiceRepo.GetBySupplier(supplierId).
                Where(Function(i) i.InvoiceDate.Date >= fromDate AndAlso i.InvoiceDate.Date <= toDate).
                OrderByDescending(Function(i) i.InvoiceDate).
                ToList()
        Else
            list = _invoiceRepo.GetAllInvoices(fromDate, toDate).
                OrderByDescending(Function(i) i.InvoiceDate).
                ToList()
        End If

        ' Filter by invoice number (ID or partial match)
        Dim invoiceNoText = (If(_txtInvoiceNo.Text, "")).Trim()
        If invoiceNoText.Length > 0 Then
            Dim num As Integer
            If Integer.TryParse(invoiceNoText, num) Then
                list = list.Where(Function(i) i.InvoiceID = num).ToList()
            Else
                list = list.Where(Function(i) i.InvoiceID.ToString().Contains(invoiceNoText)).ToList()
            End If
        End If

        _binding = New BindingList(Of BuyInvoice)(list)
        _grid.DataSource = _binding
        ApplyGridCaptions()
        IntegerMoneyGridColumns.ApplyDecimal2MoneyGridEditors(_view, "TotalAmount")
        _view.BestFitColumns()
    End Sub

    Private Sub ApplyGridCaptions()
        Dim col = _view.Columns("InvoiceID")
        If col IsNot Nothing Then
            col.Caption = If(Eng, "Invoice No.", "رقم الفاتورة")
        End If
        col = _view.Columns("SupplierID")
        If col IsNot Nothing Then
            col.Caption = If(Eng, "Supplier ID", "رقم المورد")
            col.Visible = False
        End If
        col = _view.Columns("SupplierName")
        If col IsNot Nothing Then col.Caption = If(Eng, "Supplier", "المورد")
        col = _view.Columns("InvoiceDate")
        If col IsNot Nothing Then col.Caption = If(Eng, "Date", "التاريخ")
        col = _view.Columns("TotalAmount")
        If col IsNot Nothing Then col.Caption = If(Eng, "Total", "الإجمالي")
        col = _view.Columns("DueDate")
        If col IsNot Nothing Then col.Caption = If(Eng, "Due Date", "تاريخ الاستحقاق")
        col = _view.Columns("InvoiceStatus")
        If col IsNot Nothing Then col.Caption = If(Eng, "Status", "الحالة")
        col = _view.Columns("CreatedDate")
        If col IsNot Nothing Then col.Caption = If(Eng, "Created", "تاريخ الإنشاء")
    End Sub
End Class

Friend Class BuyInvoiceEditForm
    Inherits XtraForm

    Private ReadOnly _invoice As BuyInvoice
    Private ReadOnly _suppliers As List(Of Supplier)
    Private ReadOnly _cmbSupplier As ComboBoxEdit
    Private ReadOnly _dateInvoice As DateEdit
    Private ReadOnly _dateDue As DateEdit
    Private ReadOnly _txtTotal As TextEdit
    Private ReadOnly _cmbStatus As ComboBoxEdit
    Private ReadOnly _defaultFont As Font = New Font("Calibri", 10, FontStyle.Bold)

    Private Shared ReadOnly StatusValues As String() = {"Unpaid", "Partial", "Paid"}

    Public Sub New(invoice As BuyInvoice, suppliers As List(Of Supplier))
        _invoice = invoice
        _suppliers = suppliers

        Text = If(Eng, $"Buy invoice #{_invoice.InvoiceID}", $"فاتورة شراء {_invoice.InvoiceID}")
        Width = 520
        Height = 300
        StartPosition = FormStartPosition.CenterParent
        Font = _defaultFont
        If Not Eng Then
            RightToLeft = RightToLeft.Yes
            RightToLeftLayout = True
        End If

        Dim layout As New TableLayoutPanel With {.Dock = DockStyle.Fill, .ColumnCount = 2, .RowCount = 5, .Padding = New Padding(8)}
        layout.ColumnStyles.Add(New ColumnStyle(SizeType.Absolute, 140))
        layout.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 100))

        _cmbSupplier = New ComboBoxEdit() With {.Dock = DockStyle.Fill}
        _cmbSupplier.Properties.Items.AddRange(_suppliers.Select(Function(s) s.SupplierName).ToArray())
        _dateInvoice = New DateEdit() With {.Dock = DockStyle.Fill}
        _dateDue = New DateEdit() With {.Dock = DockStyle.Fill}
        _txtTotal = New TextEdit() With {.Dock = DockStyle.Fill}
        IntegerMoneyEditorFocus.ConfigureDecimal2MoneyTextEdit(_txtTotal)
        IntegerMoneyEditorFocus.AttachTextEditZeroEmptyElseSelectAll(_txtTotal)
        _cmbStatus = New ComboBoxEdit() With {.Dock = DockStyle.Fill}
        For Each s In StatusValues
            _cmbStatus.Properties.Items.Add(If(Eng, s, StatusCaptionAr(s)))
        Next

        _cmbSupplier.Font = _defaultFont
        _dateInvoice.Font = _defaultFont
        _dateDue.Font = _defaultFont
        _txtTotal.Font = _defaultFont
        _cmbStatus.Font = _defaultFont

        layout.Controls.Add(New LabelControl With {.Text = If(Eng, "Supplier", "المورد"), .Font = _defaultFont, .Dock = DockStyle.Fill}, 0, 0)
        layout.Controls.Add(_cmbSupplier, 1, 0)
        layout.Controls.Add(New LabelControl With {.Text = If(Eng, "Invoice date", "تاريخ الفاتورة"), .Font = _defaultFont, .Dock = DockStyle.Fill}, 0, 1)
        layout.Controls.Add(_dateInvoice, 1, 1)
        layout.Controls.Add(New LabelControl With {.Text = If(Eng, "Due date", "تاريخ الاستحقاق"), .Font = _defaultFont, .Dock = DockStyle.Fill}, 0, 2)
        layout.Controls.Add(_dateDue, 1, 2)
        layout.Controls.Add(New LabelControl With {.Text = If(Eng, "Total", "الإجمالي"), .Font = _defaultFont, .Dock = DockStyle.Fill}, 0, 3)
        layout.Controls.Add(_txtTotal, 1, 3)
        layout.Controls.Add(New LabelControl With {.Text = If(Eng, "Status", "الحالة"), .Font = _defaultFont, .Dock = DockStyle.Fill}, 0, 4)
        layout.Controls.Add(_cmbStatus, 1, 4)

        Dim panelButtons As New PanelControl With {.Dock = DockStyle.Bottom, .Height = 48}
        Dim btnOk As New SimpleButton With {.Text = If(Eng, "OK", "موافق"), .DialogResult = DialogResult.OK, .Left = 280, .Top = 10}
        Dim btnCancel As New SimpleButton With {.Text = If(Eng, "Cancel", "إلغاء"), .DialogResult = DialogResult.Cancel, .Left = 370, .Top = 10}
        btnOk.Font = _defaultFont
        btnCancel.Font = _defaultFont
        panelButtons.Controls.AddRange(New Control() {btnOk, btnCancel})

        Controls.Add(layout)
        Controls.Add(panelButtons)

        Dim supIdx = Math.Max(0, _suppliers.FindIndex(Function(s) s.SupplierID = _invoice.SupplierID))
        _cmbSupplier.SelectedIndex = supIdx
        _dateInvoice.DateTime = If(_invoice.InvoiceDate = Date.MinValue, Date.Today, _invoice.InvoiceDate)
        _dateDue.DateTime = If(_invoice.DueDate = Date.MinValue, Date.Today, _invoice.DueDate)
        _txtTotal.EditValue = _invoice.TotalAmount
        Dim st = If(String.IsNullOrWhiteSpace(_invoice.InvoiceStatus), "Unpaid", _invoice.InvoiceStatus.Trim())
        Dim stIx = Array.FindIndex(StatusValues, Function(x) String.Equals(x, st, StringComparison.OrdinalIgnoreCase))
        If stIx < 0 Then stIx = 0
        _cmbStatus.SelectedIndex = stIx

        AddHandler btnOk.Click, AddressOf OnSave
    End Sub

    Private Shared Function StatusCaptionAr(english As String) As String
        Select Case english
            Case "Unpaid"
                Return "غير مدفوعة"
            Case "Partial"
                Return "جزئي"
            Case "Paid"
                Return "مدفوعة"
            Case Else
                Return english
        End Select
    End Function

    Private Sub OnSave(sender As Object, e As EventArgs)
        If _cmbSupplier.SelectedIndex < 0 OrElse _cmbSupplier.SelectedIndex >= _suppliers.Count Then
            XtraMessageBox.Show(Me, If(Eng, "Select a supplier.", "يرجى اختيار مورد."), If(Eng, "Validation", "تنبيه"), MessageBoxButtons.OK)
            DialogResult = DialogResult.None
            Return
        End If
        Dim total = IntegerMoneyEditorFocus.DecimalFromDecimal2MoneyEdit(_txtTotal)
        If total < 0D Then
            XtraMessageBox.Show(Me, If(Eng, "Total cannot be negative.", "لا يمكن أن يكون الإجمالي سالباً."), If(Eng, "Validation", "تنبيه"), MessageBoxButtons.OK)
            DialogResult = DialogResult.None
            Return
        End If
        If _cmbStatus.SelectedIndex < 0 OrElse _cmbStatus.SelectedIndex >= StatusValues.Length Then
            XtraMessageBox.Show(Me, If(Eng, "Select a status.", "يرجى اختيار الحالة."), If(Eng, "Validation", "تنبيه"), MessageBoxButtons.OK)
            DialogResult = DialogResult.None
            Return
        End If

        _invoice.SupplierID = _suppliers(_cmbSupplier.SelectedIndex).SupplierID
        _invoice.InvoiceDate = _dateInvoice.DateTime.Date
        _invoice.DueDate = _dateDue.DateTime.Date
        _invoice.TotalAmount = total
        _invoice.InvoiceStatus = StatusValues(_cmbStatus.SelectedIndex)
    End Sub
End Class
