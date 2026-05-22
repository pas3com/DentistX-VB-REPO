Imports DevExpress.XtraEditors
Imports DevExpress.XtraCharts
Imports DevExpress.XtraTab
Imports System.Linq
Imports System.Threading.Tasks

Public Class StockDashboardForm
        Inherits XtraForm

        Private ReadOnly _stockRepo As StockTrackingRepository
        Private ReadOnly _invoiceRepo As BuyInvoiceRepo
        Private ReadOnly _expenseRepo As ExpenseRepository
        Private ReadOnly _reportService As StockReportService
        Private ReadOnly _db As DapperHelper

        Private ReadOnly _defaultFont As Font = New Font("Calibri", 10, FontStyle.Bold)

        Public Sub New()
            InitializeComponent()

            Dim conn = DentistXDATA.GetConnection.ConnectionString
            _stockRepo = New StockTrackingRepository(conn)
            _invoiceRepo = New BuyInvoiceRepo(conn)
            _expenseRepo = New ExpenseRepository(conn)
            _reportService = New StockReportService(conn)
            _db = New DapperHelper(conn)

            Text = If(Eng, "Stock Dashboard", "لوحة المخزون")

        StartPosition = FormStartPosition.CenterScreen
            Font = _defaultFont
            If Not Eng Then
                RightToLeft = RightToLeft.Yes
                RightToLeftLayout = True
            End If

            AddHandler Load, AddressOf OnFormLoad
            AddHandler _btnRefresh.Click, AddressOf OnRefresh
        End Sub

        Private Async Sub OnFormLoad(sender As Object, e As EventArgs)
            Await RefreshStatsAsync()
            Await RefreshChartsAsync()
        End Sub

        Private Async Sub OnRefresh(sender As Object, e As EventArgs)
            Await RefreshStatsAsync()
            Await RefreshChartsAsync()
        End Sub

        Private Async Function RefreshStatsAsync() As Task
            _lblLow.Text = (Await _reportService.GetLowStockCountAsync()).ToString()
            _lblExp.Text = (Await _reportService.GetExpiringItemsCountAsync(60)).ToString()
            _lblOpen.Text = _invoiceRepo.GetOpenInvoices().Count().ToString()
            _lblValue.Text = (Await _reportService.GetTotalStockValueAsync()).ToString("C")
            Return
        End Function

        Private Async Function RefreshChartsAsync() As Task
            BuildSuppliersChart()
            BuildPaymentsChart()
            BuildExpensesChart()
            BuildTopProductsChart()
            Await BuildStockTrendChartAsync()
            Return
        End Function

        Private Sub BuildSuppliersChart()
            Dim data = _db.Query(Of SupplierTotalDto)(
                "SELECT s.SupplierName, ISNULL(SUM(i.TotalAmount),0) AS TotalAmount
                 FROM Suppliers s
                 LEFT JOIN BuyInvoices i ON i.SupplierID = s.SupplierID
                 GROUP BY s.SupplierName
                 ORDER BY TotalAmount DESC")

            Dim series As New Series(If(Eng, "Purchases", "المشتريات"), ViewType.Bar)
            For Each row In data
                series.Points.Add(New SeriesPoint(row.SupplierName, row.TotalAmount))
            Next
            ApplyChart(_chartSuppliers, If(Eng, "Purchases by Supplier", "المشتريات حسب المورد"), series)
        End Sub

        Private Sub BuildPaymentsChart()
            Dim data = _db.Query(Of SupplierTotalDto)(
                "SELECT s.SupplierName, ISNULL(pay.TotalAmount, 0) AS TotalAmount
                 FROM Suppliers s
                 LEFT JOIN (
                     SELECT ISNULL(p.SupplierID, i.SupplierID) AS SupplierID, SUM(p.Amount) AS TotalAmount
                     FROM Payments p
                     INNER JOIN BuyInvoices i ON p.InvoiceID = i.InvoiceID
                     GROUP BY ISNULL(p.SupplierID, i.SupplierID)
                 ) pay ON pay.SupplierID = s.SupplierID
                 ORDER BY TotalAmount DESC")

            Dim series As New Series(If(Eng, "Payments", "المدفوعات"), ViewType.Bar)
            For Each row In data
                series.Points.Add(New SeriesPoint(row.SupplierName, row.TotalAmount))
            Next
            ApplyChart(_chartPayments, If(Eng, "Payments by Supplier", "المدفوعات حسب المورد"), series)
        End Sub

        Private Sub BuildExpensesChart()
            Dim data = _db.Query(Of ExpenseMonthDto)(
                "SELECT MIN(ExpenseDate) AS SortDate,
                        DATENAME(month, ExpenseDate) + ' ' + CAST(YEAR(ExpenseDate) AS nvarchar(4)) AS MonthLabel,
                        SUM(Amount) AS TotalAmount
                 FROM Expenses
                 GROUP BY YEAR(ExpenseDate), DATENAME(month, ExpenseDate)
                 ORDER BY MIN(ExpenseDate)")

            Dim series As New Series(If(Eng, "Expenses", "المصروفات"), ViewType.Line)
            For Each row In data
                series.Points.Add(New SeriesPoint(row.MonthLabel, row.TotalAmount))
            Next
            ApplyChart(_chartExpenses, If(Eng, "Expenses by Month", "المصروفات حسب الشهر"), series)
        End Sub

        Private Sub BuildTopProductsChart()
            Dim data = _db.Query(Of ProductQtyDto)(
                "SELECT TOP 10 p.ProductName, SUM(li.Quantity) AS Qty
                 FROM BuyInvoiceLineItems li
                 JOIN Products p ON p.ProductID = li.ProductID
                 GROUP BY p.ProductName
                 ORDER BY SUM(li.Quantity) DESC")

            Dim series As New Series(If(Eng, "Quantity", "الكمية"), ViewType.Pie)
            For Each row In data
                series.Points.Add(New SeriesPoint(row.ProductName, row.Qty))
            Next
            ApplyChart(_chartTopProducts, If(Eng, "Top Products Purchased", "أكثر المنتجات شراءً"), series)
        End Sub

        Private Async Function BuildStockTrendChartAsync() As Task
            Dim data = Await _reportService.GetStockTrendsAsync()
            Dim series As New Series(If(Eng, "Stock Level", "مستوى المخزون"), ViewType.Line)
            For Each row In data
                series.Points.Add(New SeriesPoint(row.Month, row.StockLevel))
            Next
            ApplyChart(_chartStockTrend, If(Eng, "Stock Trends", "اتجاهات المخزون"), series)
        End Function

        Private Sub ApplyChart(chart As ChartControl, title As String, series As Series)
            chart.Series.Clear()
            chart.Titles.Clear()
            chart.Series.Add(series)
            chart.Legend.Visibility = DevExpress.Utils.DefaultBoolean.True
            chart.Legend.Font = _defaultFont
            Dim chartTitle As New ChartTitle With {.Text = title, .Font = _defaultFont}
            chart.Titles.Add(chartTitle)

            Dim diagram = TryCast(chart.Diagram, XYDiagram)
            If diagram IsNot Nothing Then
                diagram.AxisX.Label.Font = _defaultFont
                diagram.AxisY.Label.Font = _defaultFont
            End If
        End Sub

        Private Function CreateCard(title As String, value As String) As LabelControl
            Dim lbl As New LabelControl With {
                .Text = value,
                .AutoSizeMode = LabelAutoSizeMode.None,
                .Dock = DockStyle.Fill
            }
            lbl.Appearance.Font = _defaultFont
            lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
            lbl.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
            lbl.Tag = title
            Return lbl
        End Function

        Private Function WrapCard(lbl As LabelControl) As PanelControl
            Dim panel As New PanelControl With {.Dock = DockStyle.Fill, .Padding = New Padding(10)}
            Dim titleLbl As New LabelControl With {
                .Text = CStr(lbl.Tag),
                .Dock = DockStyle.Top}
            titleLbl.Appearance.Font = _defaultFont

            panel.Controls.Add(lbl)
            panel.Controls.Add(titleLbl)
            Return panel
        End Function

        Private Class SupplierTotalDto
            Public Property SupplierName As String
            Public Property TotalAmount As Decimal
        End Class

        Private Class ExpenseMonthDto
            Public Property SortDate As DateTime
            Public Property MonthLabel As String
            Public Property TotalAmount As Decimal
        End Class

        Private Class ProductQtyDto
            Public Property ProductName As String
            Public Property Qty As Decimal
        End Class
End Class

