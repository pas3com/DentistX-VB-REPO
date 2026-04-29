Imports System.Linq
Imports DevExpress.XtraEditors

Friend Partial Class LineItemEditForm

    Friend Property LineItem As BuyInvoiceLineItem

    Private ReadOnly _products As List(Of Product)
    Private ReadOnly _defaultFont As Font = New Font("Calibri", 10, FontStyle.Bold)

    Friend Sub New(products As List(Of Product))
        _products = products
        InitializeComponent()

        Text = If(Eng, "Line Item", "بند فاتورة")
        StartPosition = FormStartPosition.CenterParent
        Font = _defaultFont
        If Not Eng Then
            RightToLeft = RightToLeft.Yes
            RightToLeftLayout = True
        End If

        ' Populate product list
        _cmbProduct.Properties.Items.Clear()
        _cmbProduct.Properties.Items.AddRange(_products.Select(Function(p) p.ProductName).ToArray())

        ' Default expiry: one year from today
        _dateExp.DateTime = Date.Today.AddYears(1)

        IntegerMoneyEditorFocus.ConfigureIntegerMoneyTextEdit(_spinQty)
        IntegerMoneyEditorFocus.ConfigureDecimal2MoneyTextEdit(_spinPrice)
        IntegerMoneyEditorFocus.AttachTextEditZeroEmptyElseSelectAll(_spinQty, _spinPrice)

        ' Enter moves to next control (already set in designer; ensure at runtime)
        _cmbProduct.EnterMoveNextControl = True
        _spinQty.EnterMoveNextControl = True
        _spinPrice.EnterMoveNextControl = True
        _dateExp.EnterMoveNextControl = True

        AddHandler btnOk.Click, AddressOf OnSave
    End Sub

    Private Sub OnSave(sender As Object, e As EventArgs)
        If _cmbProduct.SelectedIndex < 0 Then
            XtraMessageBox.Show(Me, If(Eng, "Select a product.", "يرجى اختيار منتج."), If(Eng, "Validation", "تنبيه"), MessageBoxButtons.OK)
            DialogResult = DialogResult.None
            Return
        End If
        Dim qty = CInt(IntegerMoneyEditorFocus.DecimalFromIntegerMoneyEdit(_spinQty))
        If qty < 1 Then
            XtraMessageBox.Show(Me, If(Eng, "Quantity must be at least 1.", "الكمية يجب أن تكون 1 على الأقل."), If(Eng, "Validation", "تنبيه"), MessageBoxButtons.OK)
            DialogResult = DialogResult.None
            Return
        End If

        LineItem = New BuyInvoiceLineItem With {
            .ProductID = _products(_cmbProduct.SelectedIndex).ProductID,
            .ProductName = _products(_cmbProduct.SelectedIndex).ProductName,
            .Quantity = qty,
            .UnitPrice = IntegerMoneyEditorFocus.DecimalFromDecimal2MoneyEdit(_spinPrice),
            .BatchNumber = String.Empty,
            .ExpirationDate = If(_dateExp.EditValue Is Nothing, CType(Nothing, Date?), _dateExp.DateTime.Date)
        }
        DialogResult = DialogResult.OK
        Close()
    End Sub
End Class