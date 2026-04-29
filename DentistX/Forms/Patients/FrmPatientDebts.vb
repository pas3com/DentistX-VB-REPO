Imports System.Data.SqlClient
Imports Dapper
Imports DevExpress.XtraCharts
Imports DevExpress.XtraCharts.UI
Imports DevExpress.XtraGrid.Views.Base
Imports DevExpress.XtraGrid.Views.Grid
Imports System.Drawing
Public Class FrmPatientDebts

    Private ChartDebtOverview As ChartControl
    Private ChartTopBalances As ChartControl
    Private Panel1Layout As System.Windows.Forms.TableLayoutPanel
    Private ConnectionString As String = DentistXDATA.GetConnection.ConnectionString

    ' Base (unfiltered) list used for filters
    Private AllPatients As List(Of PatientBalance)

    ''' <summary>Conditional formatting: light row tint by balance; Balance cell fore color red/black/blue.</summary>
    Private Sub dgView_RowCellStyle(sender As Object, e As RowCellStyleEventArgs) Handles dgView.RowCellStyle
        Dim view = TryCast(sender, DevExpress.XtraGrid.Views.Grid.GridView)
        Dim row = TryCast(view?.GetRow(e.RowHandle), PatientBalance)
        If row IsNot Nothing Then
            Dim bal As Decimal = row.Balance
            ' Very light row background to distinguish &lt;0 from &gt;0
            If bal < 0 Then
                e.Appearance.BackColor = Color.FromArgb(255, 240, 240)
                e.Appearance.Options.UseBackColor = True
            ElseIf bal > 0 Then
                e.Appearance.BackColor = Color.FromArgb(240, 245, 255)
                e.Appearance.Options.UseBackColor = True
            End If
        End If
        ' Balance column fore color
        If e.Column.FieldName <> "Balance" Then Return
        Dim val = e.CellValue
        If val Is Nothing OrElse Not IsNumeric(val) Then Return
        Dim balVal As Decimal
        If Not Decimal.TryParse(val.ToString(), balVal) Then Return
        If balVal < 0 Then
            e.Appearance.ForeColor = Color.Red
        ElseIf balVal = 0 Then
            e.Appearance.ForeColor = Color.Black
        Else
            e.Appearance.ForeColor = Color.Blue
        End If
    End Sub

    Private Sub dgView_CustomUnboundColumnData(sender As Object, e As CustomColumnDataEventArgs) Handles dgView.CustomUnboundColumnData
        If e.Column.Name = "colRowNum" Then
            e.Value = e.ListSourceRowIndex + 1
        End If
    End Sub

    Private Sub FrmPatientDebts_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadData()
        SetupCharts()
    End Sub

    Private Sub LoadData()
        AllPatients = GetPatientBalance().ToList()

        If BS Is Nothing Then
            BS = New BindingSource
        End If

        BS.DataSource = AllPatients
        DGV.DataSource = BS
    End Sub

    ''' <summary>Build Panel1: donut (balance overview) + horizontal bar (top 10 by balance).</summary>
    Private Sub SetupCharts()
        Dim panel = SplitContainerControl1.Panel1
        If Panel1Layout Is Nothing Then
            Panel1Layout = New System.Windows.Forms.TableLayoutPanel With {
                .Dock = DockStyle.Fill,
                .ColumnCount = 2,
                .RowCount = 1,
                .Padding = New Padding(4)
            }
            Panel1Layout.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(SizeType.Percent, 50.0F))
            Panel1Layout.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(SizeType.Percent, 50.0F))
            Panel1Layout.RowStyles.Add(New System.Windows.Forms.RowStyle(SizeType.Percent, 100.0F))
            ChartDebtOverview = New ChartControl With {.Dock = DockStyle.Fill, .Name = "ChartDebtOverview"}
            ChartTopBalances = New ChartControl With {.Dock = DockStyle.Fill, .Name = "ChartTopBalances"}
            Panel1Layout.Controls.Add(ChartDebtOverview, 0, 0)
            Panel1Layout.Controls.Add(ChartTopBalances, 1, 0)
            panel.Controls.Add(Panel1Layout)
        End If
        Dim list As List(Of PatientBalance) = TryCast(BS?.DataSource, List(Of PatientBalance))
        If list Is Nothing Then list = New List(Of PatientBalance)()
        ' Donut: by patient count so Owed / Credit / Settled all appear (percentages = share of patients)
        Dim countOwed = list.Where(Function(p) p.Balance > 0).Count()
        Dim countCredit = list.Where(Function(p) p.Balance < 0).Count()
        Dim countSettled = list.Where(Function(p) p.Balance = 0).Count()
        ChartDebtOverview.Series.Clear()
        ChartDebtOverview.Titles.Clear()
        Dim donutSeries As New Series("Balance overview", ViewType.Doughnut)
        donutSeries.Points.Clear()
        ' Short labels for slice text: Owed, Credit, Settled (same text on donut and in hints)
        If countOwed > 0 Then donutSeries.Points.Add(New SeriesPoint("Owed", CDbl(countOwed)))
        If countCredit > 0 Then donutSeries.Points.Add(New SeriesPoint("Credit", CDbl(countCredit)))
        If countSettled > 0 Then donutSeries.Points.Add(New SeriesPoint("Settled", CDbl(countSettled)))
        If donutSeries.Points.Count = 0 Then donutSeries.Points.Add(New SeriesPoint("No data", 1.0))
        ChartDebtOverview.Series.Add(donutSeries)
        ' Segment labels on donut: show "Owed", "Credit", "Settled" and percentage
        donutSeries.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True
        donutSeries.Label.TextPattern = "{A}" & vbCrLf & "{VP:P1}"
        donutSeries.Label.Font = New System.Drawing.Font("Segoe UI", 9.0F, System.Drawing.FontStyle.Bold)
        ' Hints (tooltips) on hover: category + count + percentage
        donutSeries.ToolTipPointPattern = "{A}: {V} patients ({VP:P1})"
        ChartDebtOverview.ToolTipEnabled = DevExpress.Utils.DefaultBoolean.True
        ' Title so percentages are clearly "by patient count"
        Dim title As New ChartTitle()
        title.Text = "By patient count"
        title.Font = New System.Drawing.Font("Segoe UI", 9.0F)
        ChartDebtOverview.Titles.Add(title)
        Dim donutView = CType(donutSeries.View, DoughnutSeriesView)
        donutView.TotalLabel.Visible = DevExpress.Utils.DefaultBoolean.True
        ' Legend: larger font, clearer (shows Owed, Credit, Settled)
        ChartDebtOverview.Legend.Visibility = DevExpress.Utils.DefaultBoolean.True
        ChartDebtOverview.Legend.Font = New System.Drawing.Font("Segoe UI", 10.0F)
        ChartDebtOverview.Legend.TextColor = System.Drawing.Color.Black
        ChartDebtOverview.Legend.BackColor = System.Drawing.Color.White
        ChartDebtOverview.Legend.Border.Visibility = DevExpress.Utils.DefaultBoolean.True
        ChartDebtOverview.Legend.Border.Color = System.Drawing.Color.LightGray
        ' Bar: Top 10 by |balance| (patient name vs balance)
        ChartTopBalances.Series.Clear()
        Dim barSeries As New Series("Top 10 balances", ViewType.Bar)
        barSeries.Points.Clear()
        Dim top10 = list.OrderByDescending(Function(p) Math.Abs(p.Balance)).Take(10).ToList()
        For Each p In top10
            barSeries.Points.Add(New SeriesPoint(If(String.IsNullOrEmpty(p.PatientName), "?", p.PatientName), CDbl(p.Balance)))
        Next
        If barSeries.Points.Count > 0 Then
            ChartTopBalances.Series.Add(barSeries)
            CType(barSeries.View, SideBySideBarSeriesView).ColorEach = True
        End If
    End Sub

    Private Sub BtnApplyFilters_Click(sender As Object, e As EventArgs) Handles btnApplyFilters.Click
        ApplyFilters()
    End Sub

    Private Sub BtnClearFilters_Click(sender As Object, e As EventArgs) Handles btnClearFilters.Click
        If txtFilterName IsNot Nothing Then txtFilterName.Text = String.Empty
        If txtTreatsMin IsNot Nothing Then txtTreatsMin.Text = String.Empty
        If txtTreatsMax IsNot Nothing Then txtTreatsMax.Text = String.Empty
        If txtPaysMin IsNot Nothing Then txtPaysMin.Text = String.Empty
        If txtPaysMax IsNot Nothing Then txtPaysMax.Text = String.Empty
        If txtBalanceMin IsNot Nothing Then txtBalanceMin.Text = String.Empty
        If txtBalanceMax IsNot Nothing Then txtBalanceMax.Text = String.Empty

        If AllPatients IsNot Nothing AndAlso BS IsNot Nothing Then
            BS.DataSource = AllPatients.ToList()
            DGV.DataSource = BS
            SetupCharts()
        End If
    End Sub

    ''' <summary>Apply current filter values (name, treats, pays, balance) to the base list.</summary>
    Private Sub ApplyFilters()
        If AllPatients Is Nothing OrElse BS Is Nothing Then Return

        Dim query = AllPatients.AsEnumerable()

        ' Patient name filter (contains, case-insensitive)
        If txtFilterName IsNot Nothing AndAlso Not String.IsNullOrWhiteSpace(txtFilterName.Text) Then
            Dim fragment = txtFilterName.Text.Trim()
            query = query.Where(Function(p) Not String.IsNullOrEmpty(p.PatientName) AndAlso
                                        p.PatientName.IndexOf(fragment, StringComparison.CurrentCultureIgnoreCase) >= 0)
        End If

        ' TotalTreats range
        Dim treatsMin As Decimal
        If txtTreatsMin IsNot Nothing AndAlso Decimal.TryParse(txtTreatsMin.Text, treatsMin) Then
            query = query.Where(Function(p) p.TotalTreats >= treatsMin)
        End If
        Dim treatsMax As Decimal
        If txtTreatsMax IsNot Nothing AndAlso Decimal.TryParse(txtTreatsMax.Text, treatsMax) Then
            query = query.Where(Function(p) p.TotalTreats <= treatsMax)
        End If

        ' TotalPays range
        Dim paysMin As Decimal
        If txtPaysMin IsNot Nothing AndAlso Decimal.TryParse(txtPaysMin.Text, paysMin) Then
            query = query.Where(Function(p) p.TotalPays >= paysMin)
        End If
        Dim paysMax As Decimal
        If txtPaysMax IsNot Nothing AndAlso Decimal.TryParse(txtPaysMax.Text, paysMax) Then
            query = query.Where(Function(p) p.TotalPays <= paysMax)
        End If

        ' Balance range
        Dim balMin As Decimal
        If txtBalanceMin IsNot Nothing AndAlso Decimal.TryParse(txtBalanceMin.Text, balMin) Then
            query = query.Where(Function(p) p.Balance >= balMin)
        End If
        Dim balMax As Decimal
        If txtBalanceMax IsNot Nothing AndAlso Decimal.TryParse(txtBalanceMax.Text, balMax) Then
            query = query.Where(Function(p) p.Balance <= balMax)
        End If

        Dim result = query.ToList()
        BS.DataSource = result
        DGV.DataSource = BS
        SetupCharts()
    End Sub
    Public Function GetPatientBalance() As IEnumerable(Of PatientBalance)
        Using conn As New SqlConnection(ConnectionString)
            conn.Open()
            Return conn.Query(Of PatientBalance)("EXEC PatientsDebts").ToList()
        End Using
    End Function

    Private Function FilterByPatientID(ByVal patientID As Integer) As IEnumerable(Of PatientBalance)
        Return Conn.Query(Of PatientBalance)("EXEC PatientsDebts").
             Where(Function(pb) pb.PatientID = patientID).ToList()

    End Function

    Public Class PatientBalance
        Public Property PatientID As Integer
        Public Property PatientName As String
        Property Sex As String
        Property Age As Nullable(Of Integer)
        Property Phone As String
        Property Address As String
        Property Health As String
        Property Treat As Nullable(Of Boolean)
        Property Implant As Nullable(Of Boolean)
        Property Mobile As Nullable(Of Boolean)
        Property Ortho As Nullable(Of Boolean)
        Property Struc As Nullable(Of Boolean)
        Property Notes As String
        Property BirthY As Nullable(Of Integer)
        Public Property TotalTreats As Decimal
        Public Property TotalPays As Decimal
        Public Property Balance As Decimal
    End Class


End Class