Imports System.ComponentModel
Imports System.Linq
Imports DevExpress.XtraEditors
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Views.Grid

Public Class StockBuyInvoiceForm
    Inherits XtraForm

    Private ReadOnly _supplierRepo As SupplierRepository
    Private ReadOnly _productRepo As ProductRepository
    Private ReadOnly _purchaseService As PurchaseService

    Private ReadOnly _defaultFont As Font = New Font("Calibri", 10, FontStyle.Bold)

    Private _suppliers As List(Of Supplier)
    Private _products As List(Of Product)
    Private _lines As BindingList(Of BuyInvoiceLineItem)

    Public Sub New()
        InitializeComponent()

        Dim conn = DentistXDATA.GetConnection.ConnectionString
        _supplierRepo = New SupplierRepository(conn)
        _productRepo = New ProductRepository(conn)
        _purchaseService = New PurchaseService(conn)

        Text = If(Eng, "Buy Invoice", "فاتورة شراء")

        StartPosition = FormStartPosition.CenterScreen
        Font = _defaultFont
        If Not Eng Then
            RightToLeft = RightToLeft.Yes
            RightToLeftLayout = True
        End If

        AddHandler Load, AddressOf OnFormLoad
        AddHandler _btnAddLine.Click, AddressOf OnAddLine
        AddHandler _btnRemoveLine.Click, AddressOf OnRemoveLine
        AddHandler _btnSave.Click, AddressOf OnSaveInvoice
    End Sub

    Private Sub OnFormLoad(sender As Object, e As EventArgs)
        _suppliers = _supplierRepo.GetAll().ToList()
        _products = _productRepo.GetAll().ToList()
        _cmbSupplier.Properties.Items.AddRange(_suppliers.Select(Function(s) s.SupplierName).ToArray())
        _dateInvoice.DateTime = Date.Today
        _dateDue.DateTime = Date.Today.AddDays(30)

        _lines = New BindingList(Of BuyInvoiceLineItem)()
        _grid.DataSource = _lines
        ApplyGridCaptions()
        IntegerMoneyGridColumns.ApplyIntegerMoneyGridEditors(_view, "Quantity")
        IntegerMoneyGridColumns.ApplyDecimal2MoneyGridEditors(_view, "UnitPrice")
        _view.BestFitColumns()
    End Sub

    Private Sub OnAddLine(sender As Object, e As EventArgs)
        If _lines Is Nothing Then Return
        Using dlg As New LineItemEditForm(_products)
            If dlg.ShowDialog(Me) = DialogResult.OK Then
                Dim newItem = dlg.LineItem
                If newItem Is Nothing Then Return
                Dim existing = _lines.FirstOrDefault(Function(li) li.ProductID = newItem.ProductID)
                If existing IsNot Nothing Then
                    existing.Quantity += newItem.Quantity
                    _view.RefreshData()
                Else
                    _lines.Add(newItem)
                End If
                UpdateTotal()
            End If
        End Using
    End Sub

    Private Sub OnRemoveLine(sender As Object, e As EventArgs)
        Dim li = TryCast(_view.GetFocusedRow(), BuyInvoiceLineItem)
        If li Is Nothing Then Return
        _lines.Remove(li)
        UpdateTotal()
    End Sub

    Private Sub OnSaveInvoice(sender As Object, e As EventArgs)
        If _cmbSupplier.SelectedIndex < 0 Then
            XtraMessageBox.Show(Me, If(Eng, "Please select a supplier.", "الرجاء اختيار مورد."), If(Eng, "Validation", "تنبيه"), MessageBoxButtons.OK)
            Return
        End If
        If _lines.Count = 0 Then
            XtraMessageBox.Show(Me, If(Eng, "Add at least one line item.", "يرجى إضافة بند واحد على الأقل."), If(Eng, "Validation", "تنبيه"), MessageBoxButtons.OK)
            Return
        End If

        Dim invoice As New BuyInvoice With {
                .SupplierID = _suppliers(_cmbSupplier.SelectedIndex).SupplierID,
                .InvoiceDate = _dateInvoice.DateTime.Date,
                .DueDate = _dateDue.DateTime.Date,
                .InvoiceStatus = "Unpaid",
                .CreatedDate = DateTime.Now
            }

        Try
            Dim id = _purchaseService.CreateBuyInvoice(invoice, _lines.ToList())
            XtraMessageBox.Show(Me, If(Eng, $"Invoice {id} saved.", $"تم حفظ الفاتورة رقم {id}."), If(Eng, "Success", "نجاح"), MessageBoxButtons.OK)
            _lines.Clear()
            UpdateTotal()
        Catch ex As Exception
            XtraMessageBox.Show(Me, If(Eng, $"Failed to save invoice: {ex.Message}", $"فشل حفظ الفاتورة: {ex.Message}"), If(Eng, "Error", "خطأ"), MessageBoxButtons.OK)
        End Try
    End Sub

    Private Sub UpdateTotal()
        Dim total = _lines.Sum(Function(li) li.Quantity * li.UnitPrice)
        _lblTotal.Text = If(Eng, $"Total: {total:N2}", $"الإجمالي: {total:N2}")
    End Sub

    Private Sub ApplyGridCaptions()
        Dim col = _view.Columns("LineItemID")
        If col IsNot Nothing Then
            col.Caption = If(Eng, "Line ID", "رقم البند")
            col.Visible = False
        End If
        col = _view.Columns("InvoiceID")
        If col IsNot Nothing Then
            col.Caption = If(Eng, "Invoice ID", "رقم الفاتورة")
            col.Visible = False
        End If
        col = _view.Columns("ProductID")
        If col IsNot Nothing Then
            col.Caption = If(Eng, "Product ID", "رقم المنتج")
            col.Visible = False
        End If
        col = _view.Columns("ProductName")
        If col IsNot Nothing Then col.Caption = If(Eng, "Product", "المنتج")
        col = _view.Columns("Quantity")
        If col IsNot Nothing Then col.Caption = If(Eng, "Quantity", "الكمية")
        col = _view.Columns("UnitPrice")
        If col IsNot Nothing Then col.Caption = If(Eng, "Unit Price", "سعر الوحدة")
        col = _view.Columns("BatchNumber")
        If col IsNot Nothing Then col.Visible = False
        col = _view.Columns("ExpirationDate")
        If col IsNot Nothing Then col.Caption = If(Eng, "Expiry", "انتهاء الصلاحية")
    End Sub
End Class
