Imports System
Imports System.Collections.Generic
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports System.Windows.Forms
Imports DevExpress.XtraCharts
Imports DevExpress.XtraEditors
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraLayout


Public Class FinancialPage



    Private ReadOnly _dbHelper As New DashDataModel.DatabaseHelper()
    Private ReadOnly _connectionString As String = DashDataModel.DatabaseHelper._connectionString
    Private _currentFilter As New DashboardFilter()
    Friend WithEvents gridControlPayments As GridControl
    Friend WithEvents gridViewPayments As GridView
    Friend WithEvents chartRevenue As ChartControl
    Friend WithEvents dateEditFrom As DateEdit
    Friend WithEvents dateEditTo As DateEdit

    ' Default constructor used by designer / toolbox
    Public Sub New()
        InitializeComponent()
        InitializeComponent1()
        SetupFinancialDashboard()

        _currentFilter = New DashboardFilter() With {
            .DateFrom = dateEditFrom.DateTime,
            .DateTo = dateEditTo.DateTime
        }

        LoadFinancialData(_currentFilter)
    End Sub

    ' Explicit constructor when caller wants to provide filter
    Public Sub New(_Filter As DashboardFilter)
        InitializeComponent()
        InitializeComponent1()
        SetupFinancialDashboard()

        _currentFilter = _Filter
        LoadFinancialData(_currentFilter)
    End Sub



    Private Sub SetupFinancialDashboard()
        ' This method can be merged with InitializeComponent or kept separate
        ' Configure any additional dashboard settings here
        ConfigurePaymentGrid()
        ConfigureRevenueChart()

        ' You might want to set up default values or additional configurations
        dateEditFrom.EditValue = Date.Today.AddMonths(-1)
        dateEditTo.EditValue = Date.Today
    End Sub

    ' And fix the InitializeComponent method:
    Private Sub InitializeComponent1()
        Me.Dock = DockStyle.Fill
        Me.Padding = New Padding(10)

        ' Create main layout
        Dim layout As New LayoutControl()
        layout.Dock = DockStyle.Fill
        Me.Controls.Add(layout)

        ' Create date filters
        dateEditFrom = New DateEdit()
        dateEditTo = New DateEdit()
        dateEditFrom.EditValue = Date.Today.AddDays(-30)
        dateEditTo.EditValue = Date.Today

        ' Create filter buttons
        Dim btnApply As New SimpleButton() With {.Text = If(Eng, "Apply", "تطبيق")}
        AddHandler btnApply.Click, AddressOf ApplyFinancialFilters

        ' Create split container
        Dim splitContainer As New SplitContainer()
        splitContainer.Dock = DockStyle.Fill
        splitContainer.Orientation = Orientation.Vertical

        ' Create grid for payments
        gridControlPayments = New GridControl() With {.Dock = DockStyle.Fill}
        gridViewPayments = New GridView(gridControlPayments)
        gridControlPayments.MainView = gridViewPayments
        gridControlPayments.ViewCollection.Add(gridViewPayments)

        splitContainer.Panel1.Controls.Add(gridControlPayments)

        ' Create chart for revenue
        chartRevenue = New ChartControl() With {.Dock = DockStyle.Fill}
        splitContainer.Panel2.Controls.Add(chartRevenue)
        splitContainer.SplitterDistance = splitContainer.Width \ 2
        ' FIX: Add controls to layout properly
        ' Create layout items for each control
        Dim layoutItemFrom As New LayoutControlItem()
        layoutItemFrom.Control = dateEditFrom
        layoutItemFrom.Text = If(Eng, "From:", "من:")
        layoutItemFrom.TextVisible = True
        layout.AddItem(layoutItemFrom)

        Dim layoutItemTo As New LayoutControlItem()
        layoutItemTo.Control = dateEditTo
        layoutItemTo.Text = If(Eng, "To:", "إلى:")
        layoutItemTo.TextVisible = True
        layout.AddItem(layoutItemTo)

        Dim layoutItemApply As New LayoutControlItem()
        layoutItemApply.Control = btnApply
        layoutItemApply.TextVisible = False
        layout.AddItem(layoutItemApply)

        Dim layoutItemSplit As New LayoutControlItem()
        layoutItemSplit.Control = splitContainer
        layoutItemSplit.TextVisible = False
        layout.AddItem(layoutItemSplit)

        ' Alternative using AddControl if available in your version:
        ' layout.AddControl(dateEditFrom, "From:")
        ' layout.AddControl(dateEditTo, "To:")
        ' layout.AddControl(btnApply)
        ' layout.AddControl(splitContainer)

        ' Or using the simpler AddItem with anonymous type:
        ' layout.AddItem(New LayoutControlItem() With {
        '     .Control = dateEditFrom,
        '     .Text = "From:",
        '     .TextVisible = True
        ' })
    End Sub

    Private Sub ConfigurePaymentGrid()
        With gridViewPayments
            ' Appearance: Calibri 10 Bold for header and rows
            .Appearance.HeaderPanel.Font = New Font("Calibri", 10.0F, FontStyle.Bold)
            .Appearance.HeaderPanel.Options.UseFont = True
            .Appearance.Row.Font = New Font("Calibri", 10.0F, FontStyle.Bold)
            .Appearance.Row.Options.UseFont = True

            .OptionsView.ShowFooter = True
            .OptionsView.ShowAutoFilterRow = True
            .OptionsBehavior.Editable = False

            ' Define columns for patient balance summary
            .Columns.Clear()

            Dim colPatientName = .Columns.AddField("PatientName")
            colPatientName.Caption = If(Eng, "Patient", "المريض")
            colPatientName.Visible = True

            Dim colTotalTreats = .Columns.AddField("TotalTreats")
            colTotalTreats.Caption = If(Eng, "Treats", "العلاجات")
            colTotalTreats.DisplayFormat.FormatString = "N2"
            colTotalTreats.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
            colTotalTreats.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
            colTotalTreats.SummaryItem.DisplayFormat = "{0:N2}"
            colTotalTreats.Visible = True

            Dim colTotalPays = .Columns.AddField("TotalPays")
            colTotalPays.Caption = If(Eng, "Pays", "المدفوع")
            colTotalPays.DisplayFormat.FormatString = "N2"
            colTotalPays.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
            colTotalPays.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
            colTotalPays.SummaryItem.DisplayFormat = "{0:N2}"
            colTotalPays.Visible = True

            Dim colBalance = .Columns.AddField("Balance")
            colBalance.Caption = If(Eng, "Balance", "الرصيد")
            colBalance.DisplayFormat.FormatString = "N2"
            colBalance.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
            colBalance.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
            colBalance.SummaryItem.DisplayFormat = "{0:N2}"
            colBalance.Visible = True

            .BestFitColumns()
        End With
    End Sub

        Private Sub ConfigureRevenueChart()
            chartRevenue.SeriesTemplate.View = New SideBySideBarSeriesView()
            chartRevenue.Titles.Add(New ChartTitle() With {.Text = If(Eng, "Revenue Analysis", "تحليل الإيرادات")})
        End Sub

    Private Sub LoadFinancialData(_currentFilter As DashboardFilter)
        Try


            ' Load payments
            'Dim payments = _dbHelper.GetPayments(_currentFilter)
            Dim bals = _dbHelper.GetPatientBalances(_currentFilter)
            gridControlPayments.DataSource = bals ' payments

            ' Load revenue data for chart
            LoadRevenueChartData()

        Catch ex As Exception
            XtraMessageBox.Show($"Error loading financial data: {ex.Message}",
                                  "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub LoadRevenueChartData()
            ' Create revenue by payment type
            Dim revenueByType As New DataTable()
            revenueByType.Columns.Add("PaymentType", GetType(String))
            revenueByType.Columns.Add("Amount", GetType(Decimal))

            Using conn As New SqlConnection(_connectionString)
                conn.Open()
                Dim sql = "
                    SELECT 
                        ISNULL(PayType, 'Cash') as PaymentType,
                        SUM(PayValue) as Amount
                    FROM Patient_Pays
                    WHERE PayDate BETWEEN @From AND @To
                    GROUP BY PayType"

                Using cmd As New SqlCommand(sql, conn)
                    cmd.Parameters.AddWithValue("@From", dateEditFrom.DateTime)
                    cmd.Parameters.AddWithValue("@To", dateEditTo.DateTime)

                    Using reader As SqlDataReader = cmd.ExecuteReader()
                        While reader.Read()
                            revenueByType.Rows.Add(
                                reader("PaymentType").ToString(),
                                Convert.ToDecimal(reader("Amount")))
                        End While
                    End Using
                End Using
            End Using

            chartRevenue.DataSource = revenueByType
            chartRevenue.SeriesDataMember = "PaymentType"
            chartRevenue.SeriesTemplate.ArgumentDataMember = "PaymentType"
            chartRevenue.SeriesTemplate.ValueDataMembers.AddRange(New String() {"Amount"})
        End Sub

    Private Sub ApplyFinancialFilters(sender As Object, e As EventArgs)
        _currentFilter.DateFrom = dateEditFrom.DateTime
        _currentFilter.DateTo = dateEditTo.DateTime
        LoadFinancialData(_currentFilter)
    End Sub

    Public Sub RefreshData()
        LoadFinancialData(_currentFilter)
    End Sub

    'Private Sub GridViewPayments_RowClick(sender As Object, e As RowClickEventArgs) Handles gridViewPayments.RowClick
    '    If e.Clicks = 2 Then ' Double click
    '        Dim view As GridView = TryCast(sender, GridView)
    '        Dim payment = TryCast(view.GetRow(e.RowHandle), PatientPayment)
    '        If payment IsNot Nothing Then
    '            ShowPaymentDetails(payment)
    '        End If
    '    End If
    'End Sub

    '' <summary>
    '' Conditional formatting for Balance column: 
    '' < 0 red, = 0 black, /> 0 green.
    ''</summary>
    Private Sub gridViewPayments_RowCellStyle(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs) Handles gridViewPayments.RowCellStyle
            If e.Column.FieldName <> "Balance" Then Return

            Dim raw = e.CellValue
            If raw Is Nothing OrElse raw Is DBNull.Value Then Return

            Dim bal As Decimal
            If Not Decimal.TryParse(raw.ToString(), bal) Then Return

            If bal < 0D Then
                e.Appearance.ForeColor = Color.Red
            ElseIf bal = 0D Then
                e.Appearance.ForeColor = Color.Black
            Else
                e.Appearance.ForeColor = Color.DarkGreen
            End If
        End Sub

        Private Sub ShowPaymentDetails(payment As PatientPayment)
            Dim detailForm As New PaymentDetailForm(payment)
            detailForm.ShowDialog()
        End Sub

    Private Sub gridViewPayments_RowCellClick(sender As Object, e As RowCellClickEventArgs) Handles gridViewPayments.RowCellClick
        If e.Clicks = 2 Then ' Double click
            Dim view As GridView = TryCast(sender, GridView)
            Dim payment = TryCast(view.GetRow(e.RowHandle), PatientPayment)
            If payment IsNot Nothing Then
                ShowPaymentDetails(payment)
            End If
        End If
    End Sub
End Class
