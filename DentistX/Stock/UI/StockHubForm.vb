Imports DevExpress.XtraEditors

Public Class StockHubForm
    Inherits XtraForm

    Private ReadOnly _defaultFont As Font = New Font("Calibri", 10, FontStyle.Bold)

    Public Sub New()
        InitializeComponent()

        Text = If(Eng, "Stock Management", "إدارة المخزون")
        StartPosition = FormStartPosition.CenterScreen
        Font = _defaultFont
        If Not Eng Then
            RightToLeft = RightToLeft.Yes
            RightToLeftLayout = True
        End If

    End Sub


    Private Sub btnDashboard_Click(sender As Object, e As EventArgs) Handles btnDashboard.Click
        Using frm As New StockDashboardForm()
            frm.Icon = GetIcon()
            frm.ShowDialog(Me)
        End Using
    End Sub

    Private Sub btnSuppliers_Click(sender As Object, e As EventArgs) Handles btnSuppliers.Click
        Using frm As New StockSuppliersForm()
            frm.Icon = GetIcon()
            frm.ShowDialog(Me)
        End Using
    End Sub

    Private Sub btnProducts_Click(sender As Object, e As EventArgs) Handles btnProducts.Click
        Using frm As New StockProductsForm()
            frm.Icon = GetIcon()
            frm.ShowDialog(Me)
        End Using
    End Sub

    Private Sub btnBuy_Click(sender As Object, e As EventArgs) Handles btnBuy.Click
        Using frm As New StockBuyInvoiceForm()
            frm.Icon = GetIcon()
            frm.ShowDialog(Me)
        End Using
    End Sub
    Private Sub btnInvoices_Click(sender As Object, e As EventArgs) Handles btnInvoices.Click
        Using frm As New FrmSuppInvoices 'StockBuyInvoiceForm()
            frm.Icon = GetIcon()
            frm.ShowDialog(Me)
        End Using
    End Sub
    Private Sub btnPayments_Click(sender As Object, e As EventArgs) Handles btnPayments.Click
        Using frm As New StockSupplierPaymentsForm()
            frm.Icon = GetIcon()
            frm.ShowDialog(Me)
        End Using
    End Sub

    Private Sub btnTracking_Click(sender As Object, e As EventArgs) Handles btnTracking.Click
        Using frm As New StockTrackingForm()
            frm.Icon = GetIcon()
            frm.ShowDialog(Me)
        End Using
    End Sub

    Private Sub btnExpenses_Click(sender As Object, e As EventArgs) Handles btnExpenses.Click
        Using frm As New StockExpensesForm()
            frm.Icon = GetIcon()
            frm.ShowDialog(Me)
        End Using
    End Sub

    Private Sub btnSupplierAccountStatement_Click(sender As Object, e As EventArgs) Handles btnSupplierAccountStatement.Click
        Using frm As New StockSupplierAccountStatement()
            frm.Icon = GetIcon()
            frm.ShowDialog(Me)
        End Using
    End Sub


End Class

