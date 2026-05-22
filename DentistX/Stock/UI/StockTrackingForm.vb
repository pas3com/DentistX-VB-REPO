Imports DevExpress.XtraEditors
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraTab
Imports System.Linq

Public Class StockTrackingForm
    Inherits XtraForm

    Private ReadOnly _repo As StockTrackingRepository
    Private ReadOnly _defaultFont As Font = New Font("Calibri", 10, FontStyle.Bold)

    Public Sub New()
        InitializeComponent()

        _repo = New StockTrackingRepository(DentistXDATA.GetConnection.ConnectionString)

        Text = If(Eng, "Stock Tracking", "متابعة المخزون")

        StartPosition = FormStartPosition.CenterScreen
        Font = _defaultFont
        If Not Eng Then
            RightToLeft = RightToLeft.Yes
            RightToLeftLayout = True
        End If

        IntegerMoneyEditorFocus.ConfigureIntegerMoneyTextEdit(_spinExpDays)
        IntegerMoneyEditorFocus.AttachTextEditZeroEmptyElseSelectAll(_spinExpDays)
        _spinExpDays.EditValue = 60D

        AddHandler Load, AddressOf OnFormLoad
        AddHandler _btnRefresh.Click, AddressOf OnRefresh
    End Sub

    Private Sub OnFormLoad(sender As Object, e As EventArgs)
        LoadData()
    End Sub

    Private Sub OnRefresh(sender As Object, e As EventArgs)
        LoadData()
    End Sub

    Private Sub LoadData()
        _gridAll.DataSource = _repo.GetSnapshot().ToList()
        _gridLow.DataSource = _repo.GetLowStock().ToList()
        Dim expDays = Math.Max(1, CInt(IntegerMoneyEditorFocus.DecimalFromIntegerMoneyEdit(_spinExpDays)))
        _gridExp.DataSource = _repo.GetExpiringBatches(expDays).ToList()
        ApplySnapshotCaptions(_viewAll)
        ApplySnapshotCaptions(_viewLow)
        ApplyExpiringCaptions()
        IntegerMoneyGridColumns.ApplyIntegerMoneyGridEditors(_viewAll, "ReorderLevel")
        IntegerMoneyGridColumns.ApplyDecimal2MoneyGridEditors(_viewAll, "OnHandQuantity")
        IntegerMoneyGridColumns.ApplyIntegerMoneyGridEditors(_viewLow, "ReorderLevel")
        IntegerMoneyGridColumns.ApplyDecimal2MoneyGridEditors(_viewLow, "OnHandQuantity")
        IntegerMoneyGridColumns.ApplyIntegerMoneyGridEditors(_viewExp, "DaysToExpire")
        IntegerMoneyGridColumns.ApplyDecimal2MoneyGridEditors(_viewExp, "CurrentQuantity")
        _viewAll.BestFitColumns()
        _viewLow.BestFitColumns()
        _viewExp.BestFitColumns()
    End Sub

    Private Sub ApplySnapshotCaptions(view As GridView)
        Dim col = view.Columns("ProductID")
        If col IsNot Nothing Then col.Caption = If(Eng, "Product ID", "رقم المنتج")
        col = view.Columns("ProductName")
        If col IsNot Nothing Then col.Caption = If(Eng, "Product Name", "اسم المنتج")
        col = view.Columns("CategoryName")
        If col IsNot Nothing Then col.Caption = If(Eng, "Category", "التصنيف")
        col = view.Columns("UnitName")
        If col IsNot Nothing Then col.Caption = If(Eng, "Unit", "الوحدة")
        col = view.Columns("ReorderLevel")
        If col IsNot Nothing Then col.Caption = If(Eng, "Reorder Level", "حد إعادة الطلب")
        col = view.Columns("OnHandQuantity")
        If col IsNot Nothing Then col.Caption = If(Eng, "On Hand", "الكمية المتاحة")
    End Sub

    Private Sub ApplyExpiringCaptions()
        Dim col = _viewExp.Columns("BatchID")
        If col IsNot Nothing Then col.Caption = If(Eng, "Batch ID", "الكمية")
        col = _viewExp.Columns("ProductID")
        If col IsNot Nothing Then col.Caption = If(Eng, "Product ID", "رقم المنتج")
        col = _viewExp.Columns("ProductName")
        If col IsNot Nothing Then col.Caption = If(Eng, "Product Name", "اسم المنتج")
        col = _viewExp.Columns("BatchNumber")
        If col IsNot Nothing Then col.Caption = If(Eng, "Batch Number", "الكمية")
        col = _viewExp.Columns("ExpirationDate")
        If col IsNot Nothing Then col.Caption = If(Eng, "Expiry Date", "تاريخ الانتهاء")
        col = _viewExp.Columns("CurrentQuantity")
        If col IsNot Nothing Then col.Caption = If(Eng, "Current Qty", "الكمية الحالية")
        col = _viewExp.Columns("DaysToExpire")
        If col IsNot Nothing Then col.Caption = If(Eng, "Days to Expire", "أيام حتى الانتهاء")
    End Sub
End Class

