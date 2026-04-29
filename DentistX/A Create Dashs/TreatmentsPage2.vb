Imports System
Imports System.Collections.Generic
Imports System.Data.SqlClient
Imports System.Drawing
Imports System.Windows.Forms
Imports DevExpress.XtraCharts
Imports DevExpress.XtraEditors
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraTab
Imports DevExpress.XtraTreeList

Public Class TreatmentsPage2

    Private ReadOnly _dbHelper As New DashDataModel.DatabaseHelper()
    Private ReadOnly _connectionString As String = DashDataModel.DatabaseHelper._connectionString
    Private _currentFilter As New DashboardFilter()

    Friend WithEvents chartMostRepeated As ChartControl
    Friend WithEvents chartMostRepeatedV2 As ChartControl
    Friend WithEvents chartMostExpensive As ChartControl
    Friend WithEvents chartMostTreatedTeeth As ChartControl

    ' === Sex-based charts ===
    Friend WithEvents chartMostRepeatedBySex As ChartControl
    Friend WithEvents chartMostExpensiveBySex As ChartControl
    Friend WithEvents chartMostTreatedTeethBySex As ChartControl
    ' === Address-based charts ===
    Friend WithEvents chartMostRepeatedByAddress As ChartControl
    Friend WithEvents chartMostExpensiveByAddress As ChartControl
    Friend WithEvents chartMostTreatedTeethByAddress As ChartControl


    Friend WithEvents tabControl As DevExpress.XtraTab.XtraTabControl
    Public Sub New(_Filter As DashboardFilter)
        InitializeComponent()

        InitializeComponent1()
        SetupTreatmentsDashboard()
        _currentFilter = _Filter
        LoadTreatmentData(_currentFilter)
        LoadFilterCombos()
    End Sub
    Private Sub InitializeComponent1()
        Me.Dock = DockStyle.Fill


        ' Create tab control for different views
        tabControl = New DevExpress.XtraTab.XtraTabControl()
        tabControl.Dock = DockStyle.Fill

        ' === Tab1: Most Repeated (Sex) ===
        Dim tabRepeatedSex As New XtraTabPage() With {.Text = If(Eng, "Repeated (Sex)", "المتكرر (الجنس)")}
        chartMostRepeatedBySex = New ChartControl() With {.Dock = DockStyle.Fill}
        tabRepeatedSex.Controls.Add(chartMostRepeatedBySex)
        tabControl.TabPages.Add(tabRepeatedSex)

        ' === Tab2: Most Expensive (Sex) ===
        Dim tabExpensiveSex As New XtraTabPage() With {.Text = If(Eng, "Expensive (Sex)", "الأعلى تكلفة (الجنس)")}
        chartMostExpensiveBySex = New ChartControl() With {.Dock = DockStyle.Fill}
        tabExpensiveSex.Controls.Add(chartMostExpensiveBySex)
        tabControl.TabPages.Add(tabExpensiveSex)

        ' === Tab3: Most Treated Teeth (Sex) ===
        Dim tabTeethSex As New XtraTabPage() With {.Text = If(Eng, "Teeth (Sex)", "الأسنان (الجنس)")}
        chartMostTreatedTeethBySex = New ChartControl() With {.Dock = DockStyle.Fill}
        tabTeethSex.Controls.Add(chartMostTreatedTeethBySex)
        tabControl.TabPages.Add(tabTeethSex)

        ' === Tab 4: Most Repeated (Address) ===
        Dim tabRepeatedAddr As New XtraTabPage() With {.Text = If(Eng, "Repeated (Address)", "المتكرر (العنوان)")}
        chartMostRepeatedByAddress = New ChartControl() With {.Dock = DockStyle.Fill}
        tabRepeatedAddr.Controls.Add(chartMostRepeatedByAddress)
        tabControl.TabPages.Add(tabRepeatedAddr)

        ' === Tab 5: Most Expensive (Address) ===
        Dim tabExpensiveAddr As New XtraTabPage() With {.Text = If(Eng, "Expensive (Address)", "الأعلى تكلفة (العنوان)")}
        chartMostExpensiveByAddress = New ChartControl() With {.Dock = DockStyle.Fill}
        tabExpensiveAddr.Controls.Add(chartMostExpensiveByAddress)
        tabControl.TabPages.Add(tabExpensiveAddr)

        ' === Tab 6: Most Treated Teeth (Address) ===
        Dim tabTeethAddr As New XtraTabPage() With {.Text = If(Eng, "Teeth (Address)", "الأسنان (العنوان)")}
        chartMostTreatedTeethByAddress = New ChartControl() With {.Dock = DockStyle.Fill}
        tabTeethAddr.Controls.Add(chartMostTreatedTeethByAddress)
        tabControl.TabPages.Add(tabTeethAddr)

        pnlBody.Controls.Add(tabControl)




    End Sub
    Private Sub SetupTreatmentsDashboard()
        ' Set default tab selection
        If tabControl.TabPages.Count > 0 Then
            tabControl.SelectedTabPageIndex = 0
        End If
    End Sub

    ' Update the LoadTreatmentData method to call LoadTreatmentChartData:
    Private Sub LoadTreatmentData(filter As DashboardFilter)
        ' New analytics
        LoadMostRepeatedBySex()
        LoadMostExpensiveBySex()
        LoadMostTreatedTeethBySex()
        ' New analytics
        LoadMostRepeatedByAddress()
        LoadMostExpensiveByAddress()
        LoadMostTreatedTeethByAddress()
    End Sub

    Private Sub LoadFilterCombos()
        RemoveHandler cboTrt.EditValueChanged, AddressOf FilterChanged
        RemoveHandler cboAddress.EditValueChanged, AddressOf FilterChanged
        ' Treatments
        Using conn As New SqlConnection(_connectionString)
            conn.Open()
            Dim dt As New DataTable()
            Using da As New SqlDataAdapter("SELECT DISTINCT Treat FROM Patient_ToothTrt", conn)
                da.Fill(dt)
            End Using
            cboTrt.Properties.Items.Clear()
            cboTrt.Properties.Items.Add("All") ' default
            For Each r As DataRow In dt.Rows
                cboTrt.Properties.Items.Add(r("Treat").ToString())
            Next

        End Using

        ' Addresses
        Using conn As New SqlConnection(_connectionString)
            conn.Open()
            Dim dt As New DataTable()
            Using da As New SqlDataAdapter("SELECT DISTINCT Address FROM Patient", conn)
                da.Fill(dt)
            End Using
            cboAddress.Properties.Items.Clear()
            cboAddress.Properties.Items.Add("All") ' default
            For Each r As DataRow In dt.Rows
                cboAddress.Properties.Items.Add(r("Address").ToString())
            Next

        End Using
        cboTrt.SelectedIndex = 0
        cboAddress.SelectedIndex = 0
        AddHandler cboTrt.EditValueChanged, AddressOf FilterChanged
        AddHandler cboAddress.EditValueChanged, AddressOf FilterChanged
    End Sub

    Private Sub FilterChanged(sender As Object, e As EventArgs) Handles cboTrt.EditValueChanged, cboAddress.EditValueChanged
        Dim selectedTrt As String = If(cboTrt.SelectedItem.ToString() = "All", Nothing, cboTrt.SelectedItem.ToString())
        Dim selectedAddr As String = If(cboAddress.SelectedItem.ToString() = "All", Nothing, cboAddress.SelectedItem.ToString())

        ' Reload all charts with filter
        LoadMostRepeatedFiltered(selectedTrt, selectedAddr)
        LoadMostExpensiveFiltered(selectedTrt, selectedAddr)
        LoadMostTreatedTeethFiltered(selectedTrt, selectedAddr)
        LoadMostRepeatedByAddressFiltered(selectedTrt, selectedAddr)
        LoadMostExpensiveByAddressFiltered(selectedTrt, selectedAddr)
        LoadMostTreatedTeethByAddressFiltered(selectedTrt, selectedAddr)
    End Sub


    Private Sub ConfigureSexBarChart(chart As ChartControl, title As String)
        chart.Series.Clear()

        Dim maleSeries As New Series("Male", ViewType.Bar)
        Dim femaleSeries As New Series("Female", ViewType.Bar)

        maleSeries.ArgumentScaleType = ScaleType.Qualitative
        femaleSeries.ArgumentScaleType = ScaleType.Qualitative
        maleSeries.Label.TextPattern = "{A}: {V}"
        femaleSeries.Label.TextPattern = "{A}: {V}"
        chart.Series.AddRange(New Series() {maleSeries, femaleSeries})

        chart.Titles.Clear()
        chart.Titles.Add(New ChartTitle() With {
        .Text = title,
        .Font = New Font("Tahoma", 12, FontStyle.Bold)
    })

        chart.Legend.Visibility = DevExpress.Utils.DefaultBoolean.True
        chart.Legend.AlignmentHorizontal = LegendAlignmentHorizontal.Right
    End Sub

    Private Sub ConfigureAddressBarChart(chart As ChartControl, title As String, addresses As List(Of String))
        chart.Series.Clear()

        For Each addr In addresses
            Dim s As New Series(addr, ViewType.Bar)
            s.ArgumentScaleType = ScaleType.Qualitative
            's.Label.TextPattern = "{A}: {V}"
            chart.Series.Add(s)
        Next

        chart.Titles.Clear()
        chart.Titles.Add(New ChartTitle() With {
        .Text = title,
        .Font = New Font("Tahoma", 12, FontStyle.Bold)
    })

        chart.Legend.Visibility = DevExpress.Utils.DefaultBoolean.True
        chart.Legend.AlignmentHorizontal = LegendAlignmentHorizontal.Right
    End Sub



#Region "Sex"

    Dim  trt1 As String = Nothing
    Dim address1 As String = Nothing
    Private Sub LoadMostRepeatedFiltered(Optional trt As String = Nothing, Optional address As String = Nothing)
        trt1 = If(String.IsNullOrEmpty(trt), "All", trt)
        address1 = If(String.IsNullOrEmpty(address), "All", address)
        If trt1 = "All" AndAlso address1 = "All" Then
            ConfigureSexBarChart(chartMostRepeatedBySex, "Top 10 Most Repeated Treatments (Sex)")
        Else
            ConfigureSexBarChart(chartMostRepeatedBySex, $"Repeated Treatments: ==>> (Treat : [{ trt1}])  (Address : [{ address1}]) ")
        End If

        ' Build dynamic WHERE clause
        Dim whereClauses As New List(Of String)
        If Not String.IsNullOrEmpty(trt) Then whereClauses.Add("TreatmentName = '" & trt.Replace("'", "''") & "'")
        If Not String.IsNullOrEmpty(address) Then whereClauses.Add("Address = '" & address.Replace("'", "''") & "'")
        Dim whereSQL As String = If(whereClauses.Count > 0, "WHERE " & String.Join(" AND ", whereClauses), "")

        Using conn As New SqlConnection(_connectionString)
            conn.Open()

            Dim sql As String = $";WITH TreatmentBase AS
                                (
                                    SELECT
                                        pt.TrtID,
                                        pt.PatientID,
                                        p.Sex,
                                        p.Address,
                                        pt.TrtValue,
                                        pt.TrtDate,

                                        CASE
                                            WHEN pt.ToothTrtID IS NOT NULL THEN t.Treat
                                            WHEN pt.OrthoID IS NOT NULL THEN o.Khota
                                            WHEN pt.OtherTrtID IS NOT NULL THEN ot.Trt
                                            ELSE 'Unknown'
                                        END AS TreatmentName,

                                        CASE 
                                            WHEN 
                                                (pt.ToothTrtID IS NOT NULL AND t.Treat LIKE '%IMPLANT%')
                                             OR (pt.OtherTrtID IS NOT NULL AND ot.Trt LIKE '%IMPLANT%')
                                            THEN 1 ELSE 0 
                                        END AS IsImplant,

                                        CASE 
                                            WHEN 
                                                (pt.ToothTrtID IS NOT NULL AND t.Treat LIKE '%EXTRACTION%')
                                             OR (pt.OtherTrtID IS NOT NULL AND ot.Trt LIKE '%EXTRACTION%')
                                            THEN 1 ELSE 0 
                                        END AS IsExtraction,

                                        CASE 
                                            WHEN 
                                                (pt.ToothTrtID IS NOT NULL AND t.Treat LIKE '%BRIDGE%')
                                             OR (pt.OtherTrtID IS NOT NULL AND ot.Trt LIKE '%BRIDGE%')
                                            THEN 1 ELSE 0 
                                        END AS IsBridge

                                    FROM Patient_Trts pt
                                    INNER JOIN Patient p ON pt.PatientID = p.PatientID
                                    LEFT JOIN Patient_ToothTrt t ON pt.ToothTrtID = t.ToothTrtID
                                    LEFT JOIN OrthoInf o ON pt.OrthoID = o.OrthoID
                                    LEFT JOIN Patient_OtherTrt ot ON pt.OtherTrtID = ot.OtherTrtID
                                )

                                SELECT TOP 10
                                    TreatmentName,
                                    COUNT(*) AS TotalCount,
                                    SUM(CASE WHEN Sex IN ('MALE','Male','ذكر') THEN 1 ELSE 0 END) AS MaleCount,
                                    SUM(CASE WHEN Sex IN ('FEMALE','Female','أنثى','انثى') THEN 1 ELSE 0 END) AS FemaleCount
                                FROM TreatmentBase
                                 {whereSQL}
                                GROUP BY TreatmentName
                                ORDER BY TotalCount DESC
                                "

            Using da As New SqlDataAdapter(sql, conn)
                Dim dt As New DataTable()
                da.Fill(dt)

                chartMostRepeatedBySex.DataSource = dt

                chartMostRepeatedBySex.Series("Male").ArgumentDataMember = "TreatmentName"
                chartMostRepeatedBySex.Series("Male").ValueDataMembers.Clear()
                chartMostRepeatedBySex.Series("Male").ValueDataMembers.AddRange("MaleCount")

                chartMostRepeatedBySex.Series("Female").ArgumentDataMember = "TreatmentName"
                chartMostRepeatedBySex.Series("Female").ValueDataMembers.Clear()
                chartMostRepeatedBySex.Series("Female").ValueDataMembers.AddRange("FemaleCount")
            End Using
        End Using
    End Sub
    '  load treatment data for the chart:
    Private Sub LoadMostRepeatedBySex()
        ' Build dynamic WHERE clause

        ConfigureSexBarChart(chartMostRepeatedBySex, "Top 10 Most Repeated Treatments (Sex)")

        Using conn As New SqlConnection(_connectionString)
            conn.Open()

            Dim sql As String = $";WITH TreatmentBase AS
                                (
                                    SELECT
                                        pt.TrtID,
                                        pt.PatientID,
                                        p.Sex,
                                        p.Address,
                                        pt.TrtValue,
                                        pt.TrtDate,

                                        CASE
                                            WHEN pt.ToothTrtID IS NOT NULL THEN t.Treat
                                            WHEN pt.OrthoID IS NOT NULL THEN o.Khota
                                            WHEN pt.OtherTrtID IS NOT NULL THEN ot.Trt
                                            ELSE 'Unknown'
                                        END AS TreatmentName,

                                        CASE 
                                            WHEN 
                                                (pt.ToothTrtID IS NOT NULL AND t.Treat LIKE '%IMPLANT%')
                                             OR (pt.OtherTrtID IS NOT NULL AND ot.Trt LIKE '%IMPLANT%')
                                            THEN 1 ELSE 0 
                                        END AS IsImplant,

                                        CASE 
                                            WHEN 
                                                (pt.ToothTrtID IS NOT NULL AND t.Treat LIKE '%EXTRACTION%')
                                             OR (pt.OtherTrtID IS NOT NULL AND ot.Trt LIKE '%EXTRACTION%')
                                            THEN 1 ELSE 0 
                                        END AS IsExtraction,

                                        CASE 
                                            WHEN 
                                                (pt.ToothTrtID IS NOT NULL AND t.Treat LIKE '%BRIDGE%')
                                             OR (pt.OtherTrtID IS NOT NULL AND ot.Trt LIKE '%BRIDGE%')
                                            THEN 1 ELSE 0 
                                        END AS IsBridge

                                    FROM Patient_Trts pt
                                    INNER JOIN Patient p ON pt.PatientID = p.PatientID
                                    LEFT JOIN Patient_ToothTrt t ON pt.ToothTrtID = t.ToothTrtID
                                    LEFT JOIN OrthoInf o ON pt.OrthoID = o.OrthoID
                                    LEFT JOIN Patient_OtherTrt ot ON pt.OtherTrtID = ot.OtherTrtID
                                )

                                SELECT TOP 10
                                    TreatmentName,
                                    COUNT(*) AS TotalCount,
                                    SUM(CASE WHEN Sex IN ('MALE','Male','ذكر') THEN 1 ELSE 0 END) AS MaleCount,
                                    SUM(CASE WHEN Sex IN ('FEMALE','Female','أنثى','انثى') THEN 1 ELSE 0 END) AS FemaleCount
                                FROM TreatmentBase
                                GROUP BY TreatmentName
                                ORDER BY TotalCount DESC
                                "

            Using da As New SqlDataAdapter(sql, conn)
                Dim dt As New DataTable()
                da.Fill(dt)

                chartMostRepeatedBySex.DataSource = dt

                chartMostRepeatedBySex.Series("Male").ArgumentDataMember = "TreatmentName"
                chartMostRepeatedBySex.Series("Male").ValueDataMembers.Clear()
                chartMostRepeatedBySex.Series("Male").ValueDataMembers.AddRange("MaleCount")

                chartMostRepeatedBySex.Series("Female").ArgumentDataMember = "TreatmentName"
                chartMostRepeatedBySex.Series("Female").ValueDataMembers.Clear()
                chartMostRepeatedBySex.Series("Female").ValueDataMembers.AddRange("FemaleCount")
            End Using
        End Using
    End Sub

    Private Sub LoadMostExpensiveFiltered(Optional trt As String = Nothing, Optional address As String = Nothing)
        trt1 = If(String.IsNullOrEmpty(trt), "All", trt)
        address1 = If(String.IsNullOrEmpty(address), "All", address)
        If trt1 = "All" AndAlso address1 = "All" Then
            ConfigureSexBarChart(chartMostExpensiveBySex, "Top 10 Most Expensive Treatments (Sex)")
        Else
            ConfigureSexBarChart(chartMostExpensiveBySex, $"Expensive Treatments: ==>> (Treat : [{ trt1}])  (Address : [{ address1}]) ")
        End If

        ' Build dynamic WHERE clause
        Dim whereClauses As New List(Of String)
        If Not String.IsNullOrEmpty(trt) Then whereClauses.Add("TreatmentName = '" & trt.Replace("'", "''") & "'")
        If Not String.IsNullOrEmpty(address) Then whereClauses.Add("Address = '" & address.Replace("'", "''") & "'")
        Dim whereSQL As String = If(whereClauses.Count > 0, "WHERE " & String.Join(" AND ", whereClauses), "")

        Using conn As New SqlConnection(_connectionString)
            conn.Open()
            Dim sql As String = ";WITH TreatmentBase AS
                                            (
                                                SELECT
                                                    pt.PatientID,
                                                    p.Sex,
                                                    pt.TrtValue,

                                                    CASE
                                                        WHEN pt.ToothTrtID IS NOT NULL THEN t.Treat
                                                        WHEN pt.OrthoID     IS NOT NULL THEN o.Khota
                                                        WHEN pt.OtherTrtID  IS NOT NULL THEN ot.Trt
                                                        ELSE 'Unknown'
                                                    END AS TreatmentName

                                                FROM Patient_Trts pt
                                                INNER JOIN Patient p ON pt.PatientID = p.PatientID
                                                LEFT JOIN Patient_ToothTrt  t ON pt.ToothTrtID = t.ToothTrtID
                                                LEFT JOIN OrthoInf          o ON pt.OrthoID    = o.OrthoID
                                                LEFT JOIN Patient_OtherTrt ot ON pt.OtherTrtID = ot.OtherTrtID
                                            )

                                            SELECT TOP 10
                                                TreatmentName,

                                                SUM(CASE 
                                                    WHEN Sex IN ('MALE','Male','ذكر') 
                                                    THEN TrtValue ELSE 0 END) AS MaleValue,

                                                SUM(CASE 
                                                    WHEN Sex IN ('FEMALE','Female','أنثى','انثى') 
                                                    THEN TrtValue ELSE 0 END) AS FemaleValue,

                                                SUM(TrtValue) AS TotalValue

                                            FROM TreatmentBase
                                            GROUP BY TreatmentName
                                            ORDER BY TotalValue DESC;"

            Using da As New SqlDataAdapter(sql, conn)
                Dim dt As New DataTable()
                da.Fill(dt)

                chartMostExpensiveBySex.DataSource = dt

                chartMostExpensiveBySex.Series("Male").ArgumentDataMember = "TreatmentName"
                chartMostExpensiveBySex.Series("Male").ValueDataMembers.Clear()
                chartMostExpensiveBySex.Series("Male").ValueDataMembers.AddRange("MaleValue")

                chartMostExpensiveBySex.Series("Female").ArgumentDataMember = "TreatmentName"
                chartMostExpensiveBySex.Series("Female").ValueDataMembers.Clear()
                chartMostExpensiveBySex.Series("Female").ValueDataMembers.AddRange("FemaleValue")
            End Using
        End Using
    End Sub
    Private Sub LoadMostExpensiveBySex()
        ConfigureSexBarChart(chartMostExpensiveBySex, "Top 10 Most Expensive Treatments (Sex)")

        Using conn As New SqlConnection(_connectionString)
            conn.Open()

            Dim sql As String = ";WITH TreatmentBase AS
                                            (
                                                SELECT
                                                    pt.PatientID,
                                                    p.Sex,
                                                    pt.TrtValue,

                                                    CASE
                                                        WHEN pt.ToothTrtID IS NOT NULL THEN t.Treat
                                                        WHEN pt.OrthoID     IS NOT NULL THEN o.Khota
                                                        WHEN pt.OtherTrtID  IS NOT NULL THEN ot.Trt
                                                        ELSE 'Unknown'
                                                    END AS TreatmentName

                                                FROM Patient_Trts pt
                                                INNER JOIN Patient p ON pt.PatientID = p.PatientID
                                                LEFT JOIN Patient_ToothTrt  t ON pt.ToothTrtID = t.ToothTrtID
                                                LEFT JOIN OrthoInf          o ON pt.OrthoID    = o.OrthoID
                                                LEFT JOIN Patient_OtherTrt ot ON pt.OtherTrtID = ot.OtherTrtID
                                            )

                                            SELECT TOP 10
                                                TreatmentName,

                                                SUM(CASE 
                                                    WHEN Sex IN ('MALE','Male','ذكر') 
                                                    THEN TrtValue ELSE 0 END) AS MaleValue,

                                                SUM(CASE 
                                                    WHEN Sex IN ('FEMALE','Female','أنثى','انثى') 
                                                    THEN TrtValue ELSE 0 END) AS FemaleValue,

                                                SUM(TrtValue) AS TotalValue

                                            FROM TreatmentBase
                                            GROUP BY TreatmentName
                                            ORDER BY TotalValue DESC;

                                "


            Using da As New SqlDataAdapter(sql, conn)
                Dim dt As New DataTable()
                da.Fill(dt)

                chartMostExpensiveBySex.DataSource = dt
                chartMostExpensiveBySex.Series("Male").ArgumentDataMember = "TreatmentName"
                chartMostExpensiveBySex.Series("Male").ValueDataMembers.Clear()
                chartMostExpensiveBySex.Series("Male").ValueDataMembers.AddRange("MaleValue")

                chartMostExpensiveBySex.Series("Female").ArgumentDataMember = "TreatmentName"
                chartMostExpensiveBySex.Series("Female").ValueDataMembers.Clear()
                chartMostExpensiveBySex.Series("Female").ValueDataMembers.AddRange("FemaleValue")

            End Using
        End Using
    End Sub


    Private Sub LoadMostTreatedTeethFiltered(Optional trt As String = Nothing, Optional address As String = Nothing)
        trt1 = If(String.IsNullOrEmpty(trt), "All", trt)
        address1 = If(String.IsNullOrEmpty(address), "All", address)
        If trt1 = "All" AndAlso address1 = "All" Then
            ConfigureSexBarChart(chartMostTreatedTeethBySex, "Top 10 Most Treated Teeth (Sex)")
        Else
            ConfigureSexBarChart(chartMostTreatedTeethBySex, $"Treated Teeth: ==>> (Treat : [{ trt1}])  (Address : [{ address1}]) ")
        End If
        ' Build dynamic WHERE clause
        Dim whereClauses As New List(Of String)
        If Not String.IsNullOrEmpty(trt) Then whereClauses.Add("TreatmentName = '" & trt.Replace("'", "''") & "'")
        If Not String.IsNullOrEmpty(address) Then whereClauses.Add("Address = '" & address.Replace("'", "''") & "'")
        Dim whereSQL As String = If(whereClauses.Count > 0, "WHERE " & String.Join(" AND ", whereClauses), "")

        Using conn As New SqlConnection(_connectionString)
            conn.Open()

            Dim sql As String = $";WITH TreatmentBase AS
                                (
                                    SELECT
                                        pt.TrtID,
                                        pt.PatientID,
                                        p.Sex,
                                        p.Address,
                                        pt.TrtValue,
                                        pt.TrtDate,
                                        t.ToothName AS TreatmentName,

                                        CASE 
                                            WHEN 
                                                (pt.ToothTrtID IS NOT NULL AND t.Treat LIKE '%IMPLANT%')
                                             OR (pt.OtherTrtID IS NOT NULL AND ot.Trt LIKE '%IMPLANT%')
                                            THEN 1 ELSE 0 
                                        END AS IsImplant,

                                        CASE 
                                            WHEN 
                                                (pt.ToothTrtID IS NOT NULL AND t.Treat LIKE '%EXTRACTION%')
                                             OR (pt.OtherTrtID IS NOT NULL AND ot.Trt LIKE '%EXTRACTION%')
                                            THEN 1 ELSE 0 
                                        END AS IsExtraction,

                                        CASE 
                                            WHEN 
                                                (pt.ToothTrtID IS NOT NULL AND t.Treat LIKE '%BRIDGE%')
                                             OR (pt.OtherTrtID IS NOT NULL AND ot.Trt LIKE '%BRIDGE%')
                                            THEN 1 ELSE 0 
                                        END AS IsBridge

                                    FROM Patient_Trts pt
                                    INNER JOIN Patient p ON pt.PatientID = p.PatientID
                                    LEFT JOIN Patient_ToothTrt t ON pt.ToothTrtID = t.ToothTrtID
                                    LEFT JOIN OrthoInf o ON pt.OrthoID = o.OrthoID
                                    LEFT JOIN Patient_OtherTrt ot ON pt.OtherTrtID = ot.OtherTrtID
                                )

                                SELECT TOP 10
                                    TreatmentName,
                                    COUNT(*) AS TotalCount,
                                    SUM(CASE WHEN Sex IN ('MALE','Male','ذكر') THEN 1 ELSE 0 END) AS MaleCount,
                                    SUM(CASE WHEN Sex IN ('FEMALE','Female','أنثى','انثى') THEN 1 ELSE 0 END) AS FemaleCount
                                FROM TreatmentBase
                                 {whereSQL}
                                GROUP BY TreatmentName
                                ORDER BY TotalCount DESC; "

            Using da As New SqlDataAdapter(sql, conn)
                Dim dt As New DataTable()
                da.Fill(dt)

                chartMostTreatedTeethBySex.DataSource = dt

                chartMostTreatedTeethBySex.Series("Male").ArgumentDataMember = "TreatmentName"
                chartMostTreatedTeethBySex.Series("Male").ValueDataMembers.Clear()
                chartMostTreatedTeethBySex.Series("Male").ValueDataMembers.AddRange("MaleCount")

                chartMostTreatedTeethBySex.Series("Female").ArgumentDataMember = "TreatmentName"
                chartMostTreatedTeethBySex.Series("Female").ValueDataMembers.Clear()
                chartMostTreatedTeethBySex.Series("Female").ValueDataMembers.AddRange("FemaleCount")
            End Using
        End Using
    End Sub

    Private Sub LoadMostTreatedTeethBySex()
        ConfigureSexBarChart(chartMostTreatedTeethBySex, "Top 10 Most Treated Teeth (Sex)")

        Using conn As New SqlConnection(_connectionString)
            conn.Open()

            Dim sql As String = ";WITH TreatmentBase AS
                                (
                                    SELECT
                                        pt.TrtID,
                                        pt.PatientID,
                                        p.Sex,
                                        p.Address,
                                        pt.TrtValue,
                                        pt.TrtDate,
                                        t.ToothName AS TreatmentName,
                                        CASE 
                                            WHEN 
                                                (pt.ToothTrtID IS NOT NULL AND t.Treat LIKE '%IMPLANT%')
                                             OR (pt.OtherTrtID IS NOT NULL AND ot.Trt LIKE '%IMPLANT%')
                                            THEN 1 ELSE 0 
                                        END AS IsImplant,

                                        CASE 
                                            WHEN 
                                                (pt.ToothTrtID IS NOT NULL AND t.Treat LIKE '%EXTRACTION%')
                                             OR (pt.OtherTrtID IS NOT NULL AND ot.Trt LIKE '%EXTRACTION%')
                                            THEN 1 ELSE 0 
                                        END AS IsExtraction,

                                        CASE 
                                            WHEN 
                                                (pt.ToothTrtID IS NOT NULL AND t.Treat LIKE '%BRIDGE%')
                                             OR (pt.OtherTrtID IS NOT NULL AND ot.Trt LIKE '%BRIDGE%')
                                            THEN 1 ELSE 0 
                                        END AS IsBridge

                                    FROM Patient_Trts pt
                                    INNER JOIN Patient p ON pt.PatientID = p.PatientID
                                    LEFT JOIN Patient_ToothTrt t ON pt.ToothTrtID = t.ToothTrtID
                                    LEFT JOIN OrthoInf o ON pt.OrthoID = o.OrthoID
                                    LEFT JOIN Patient_OtherTrt ot ON pt.OtherTrtID = ot.OtherTrtID
                                )

                                SELECT TOP 10
                                    TreatmentName,
                                    COUNT(*) AS TotalCount,
                                    SUM(CASE WHEN Sex IN ('MALE','Male','ذكر') THEN 1 ELSE 0 END) AS MaleCount,
                                    SUM(CASE WHEN Sex IN ('FEMALE','Female','أنثى','انثى') THEN 1 ELSE 0 END) AS FemaleCount
                                FROM TreatmentBase
                                GROUP BY TreatmentName
                                ORDER BY TotalCount DESC; "

            Using da As New SqlDataAdapter(sql, conn)
                Dim dt As New DataTable()
                da.Fill(dt)

                chartMostTreatedTeethBySex.DataSource = dt

                chartMostTreatedTeethBySex.Series("Male").ArgumentDataMember = "TreatmentName"
                chartMostTreatedTeethBySex.Series("Male").ValueDataMembers.Clear()
                chartMostTreatedTeethBySex.Series("Male").ValueDataMembers.AddRange("MaleCount")

                chartMostTreatedTeethBySex.Series("Female").ArgumentDataMember = "TreatmentName"
                chartMostTreatedTeethBySex.Series("Female").ValueDataMembers.Clear()
                chartMostTreatedTeethBySex.Series("Female").ValueDataMembers.AddRange("FemaleCount")
            End Using
        End Using
    End Sub

#End Region


    Private Sub LoadMostRepeatedByAddress()
        ' Step 1: Get top addresses
        Dim topAddresses As New List(Of String)
        Using conn As New SqlConnection(_connectionString)
            conn.Open()
            Dim sqlTopAddr As String = "
            SELECT TOP 10 Address
            FROM Patient
            GROUP BY Address
            ORDER BY COUNT(*) DESC"
            Using da As New SqlDataAdapter(sqlTopAddr, conn)
                Dim dtAddr As New DataTable()
                da.Fill(dtAddr)
                For Each r As DataRow In dtAddr.Rows
                    topAddresses.Add(r("Address").ToString())
                Next
            End Using
        End Using

        ' Step 2: Configure chart with dynamic series
        ConfigureAddressBarChart(chartMostRepeatedByAddress, "Top 10 Most Repeated Treatments (Address)", topAddresses)

        ' Step 3: Load data per address
        Using conn As New SqlConnection(_connectionString)
            conn.Open()
            Dim sql As String = ";WITH TreatmentBase AS
                                                (
                                                    SELECT
                                                        p.Address,
                                                        CASE
                                                            WHEN pt.ToothTrtID IS NOT NULL THEN t.Treat
                                                            WHEN pt.OrthoID     IS NOT NULL THEN o.Khota
                                                            WHEN pt.OtherTrtID  IS NOT NULL THEN ot.Trt
                                                            ELSE 'Unknown'
                                                        END AS TreatmentName
                                                    FROM Patient_Trts pt
                                                    INNER JOIN Patient p ON pt.PatientID = p.PatientID
                                                    LEFT JOIN Patient_ToothTrt  t ON pt.ToothTrtID = t.ToothTrtID
                                                    LEFT JOIN OrthoInf          o ON pt.OrthoID    = o.OrthoID
                                                    LEFT JOIN Patient_OtherTrt ot ON pt.OtherTrtID = ot.OtherTrtID
                                                ),
                                                Ranked AS
                                                (
                                                    SELECT
                                                        Address,
                                                        TreatmentName,
                                                        COUNT(*) AS TreatmentCount,
                                                        ROW_NUMBER() OVER
                                                        (
                                                            PARTITION BY Address
                                                            ORDER BY COUNT(*) DESC
                                                        ) AS RN
                                                    FROM TreatmentBase
                                                    WHERE Address IN ('" & String.Join("','", topAddresses) & "')
                                                    GROUP BY Address, TreatmentName
                                                )
                                                SELECT
                                                    Address,
                                                    TreatmentName,
                                                    TreatmentCount
                                                FROM Ranked
                                                WHERE RN <= 5        -- or 1 if you want only the top
                                                ORDER BY Address, TreatmentCount DESC;
                                                "

            Using da As New SqlDataAdapter(sql, conn)
                Dim dt As New DataTable()
                da.Fill(dt)
                Try
                    ' Fill each series

                    For Each s As Series In chartMostRepeatedByAddress.Series
                        Dim dv As New DataView(dt)
                        dv.RowFilter = "Address = '" & s.Name.Replace("'", "''") & "'"
                        s.DataSource = dv
                        s.ArgumentDataMember = "TreatmentName"
                        s.ValueDataMembers.AddRange("TreatmentCount")
                    Next

                Catch ex As Exception
                    MsgBox(ex.Message)
                End Try

            End Using
        End Using
    End Sub

    Private Sub LoadMostExpensiveByAddress()
        ' Step 1: Get top addresses
        Dim topAddresses As New List(Of String)
        Using conn As New SqlConnection(_connectionString)
            conn.Open()
            Dim sqlTopAddr As String = "
            SELECT TOP 10 p.Address
                        FROM Patient p
                        INNER JOIN Patient_Trts pt ON p.PatientID = pt.PatientID
                        GROUP BY p.Address
                        ORDER BY SUM(pt.TrtValue) DESC
                        "
            Using da As New SqlDataAdapter(sqlTopAddr, conn)
                Dim dtAddr As New DataTable()
                da.Fill(dtAddr)
                For Each r As DataRow In dtAddr.Rows
                    topAddresses.Add(r("Address").ToString())
                Next
            End Using
        End Using

        ' Step 2: Configure chart
        ConfigureAddressBarChart(chartMostExpensiveByAddress, "Top 10 Most Expensive Treatments (Address)", topAddresses)

        ' Step 3: Load data
        Using conn As New SqlConnection(_connectionString)
            conn.Open()
            Dim sql As String = ";WITH TreatmentBase AS
                                            (
                                                SELECT
                                                    p.Address,
                                                    pt.TrtValue,
                                                    CASE
                                                        WHEN pt.ToothTrtID IS NOT NULL THEN t.Treat
                                                        WHEN pt.OrthoID     IS NOT NULL THEN o.Khota
                                                        WHEN pt.OtherTrtID  IS NOT NULL THEN ot.Trt
                                                        ELSE 'Unknown'
                                                    END AS TreatmentName
                                                FROM Patient_Trts pt
                                                INNER JOIN Patient p ON pt.PatientID = p.PatientID
                                                LEFT JOIN Patient_ToothTrt  t ON pt.ToothTrtID = t.ToothTrtID
                                                LEFT JOIN OrthoInf          o ON pt.OrthoID    = o.OrthoID
                                                LEFT JOIN Patient_OtherTrt ot ON pt.OtherTrtID = ot.OtherTrtID
                                            ),
                                            Ranked AS
                                            (
                                                SELECT
                                                    Address,
                                                    TreatmentName,
                                                    SUM(TrtValue) AS TotalValue,
                                                    ROW_NUMBER() OVER
                                                    (
                                                        PARTITION BY Address
                                                        ORDER BY SUM(TrtValue) DESC
                                                    ) AS RN
                                                FROM TreatmentBase
                                                WHERE Address IN ('" & String.Join("','", topAddresses) & "')
                                                GROUP BY Address, TreatmentName
                                            )
                                            SELECT
                                                Address,
                                                TreatmentName,
                                                TotalValue
                                            FROM Ranked
                                            WHERE RN = 1
                                            ORDER BY TotalValue DESC;
                                            "
            Using da As New SqlDataAdapter(Sql, conn)
                Dim dt As New DataTable()
                da.Fill(dt)

                For Each s As Series In chartMostExpensiveByAddress.Series
                    Dim dv As New DataView(dt)
                    dv.RowFilter = "Address = '" & s.Name.Replace("'", "''") & "'"
                    s.DataSource = dv
                    s.ArgumentDataMember = "TreatmentName"
                    s.ValueDataMembers.AddRange("TotalValue")
                Next
            End Using
        End Using
    End Sub
    Private Sub LoadMostTreatedTeethByAddress()
        ' Step 1: Top addresses
        Dim topAddresses As New List(Of String)
        Using conn As New SqlConnection(_connectionString)
            conn.Open()
            Dim sqlTopAddr As String = "
            SELECT TOP 10 Address
            FROM Patient p
            INNER JOIN Patient_Trts pt ON p.PatientID = pt.PatientID
            GROUP BY Address
            ORDER BY COUNT(pt.ToothTrtID) DESC"
            Using da As New SqlDataAdapter(sqlTopAddr, conn)
                Dim dtAddr As New DataTable()
                da.Fill(dtAddr)
                For Each r As DataRow In dtAddr.Rows
                    topAddresses.Add(r("Address").ToString())
                Next
            End Using
        End Using

        ' Step 2: Configure chart
        ConfigureAddressBarChart(chartMostTreatedTeethByAddress, "Top 10 Most Treated Teeth (Address)", topAddresses)

        ' Step 3: Load data
        Using conn As New SqlConnection(_connectionString)
            conn.Open()
            Dim sql As String = ";WITH ToothBase AS
                                            (
                                                SELECT
                                                    p.Address,
                                                    t.ToothName AS ToothName
                                                FROM Patient_Trts pt
                                                INNER JOIN Patient p ON pt.PatientID = p.PatientID
                                                INNER JOIN Patient_ToothTrt t ON pt.ToothTrtID = t.ToothTrtID
                                            ),
                                            Ranked AS
                                            (
                                                SELECT
                                                    Address,
                                                    ToothName,
                                                    COUNT(*) AS TreatmentCount,
                                                    ROW_NUMBER() OVER
                                                    (
                                                        PARTITION BY Address
                                                        ORDER BY COUNT(*) DESC
                                                    ) AS RN
                                                FROM ToothBase
                                                WHERE Address IN ('" & String.Join("','", topAddresses) & "')
                                                GROUP BY Address, ToothName
                                            )
                                            SELECT
                                                Address,
                                                ToothName AS TreatmentName,
                                                TreatmentCount
                                            FROM Ranked
                                            WHERE RN = 1
                                            ORDER BY TreatmentCount DESC;
                                            "

            Using da As New SqlDataAdapter(sql, conn)
                Dim dt As New DataTable()
                da.Fill(dt)

                For Each s As Series In chartMostTreatedTeethByAddress.Series
                    Dim dv As New DataView(dt)
                    dv.RowFilter = "Address = '" & s.Name.Replace("'", "''") & "'"
                    s.DataSource = dv
                    s.ArgumentDataMember = "TreatmentName"
                    s.ValueDataMembers.AddRange("TreatmentCount") '("TeethCount")
                Next
            End Using
        End Using
    End Sub

    Private Sub LoadMostRepeatedByAddressFiltered(Optional trt As String = Nothing, Optional address As String = Nothing)

        Dim trt1 = If(String.IsNullOrEmpty(trt), "All", trt)
        Dim address1 = If(String.IsNullOrEmpty(address), "All", address)

        If trt1 = "All" AndAlso address1 = "All" Then
            ConfigureAddressBarChart(chartMostRepeatedByAddress,
                                 "Top Most Repeated Treatments (Address)",
                                 Nothing)
        Else
            Dim adr As New List(Of String)
            adr.Add(address1)
            ConfigureAddressBarChart(chartMostRepeatedByAddress,
                                 $"Repeated Treatments ==> (Treat: [{trt1}]) (Address: [{address1}])",
                                 adr)
        End If

        '-----------------------------
        ' Build dynamic WHERE clause
        '-----------------------------
        Dim whereClauses As New List(Of String)

        If Not String.IsNullOrEmpty(trt) Then
            whereClauses.Add("TreatmentName = '" & trt.Replace("'", "''") & "'")
        End If

        If Not String.IsNullOrEmpty(address) Then
            whereClauses.Add("Address = '" & address.Replace("'", "''") & "'")
        End If

        Dim whereSQL As String =
        If(whereClauses.Count > 0,
           "WHERE " & String.Join(" AND ", whereClauses),
           "")

        '-----------------------------
        ' SQL
        '-----------------------------
        Using conn As New SqlConnection(_connectionString)
            conn.Open()

            Dim sql As String =
        ";WITH TreatmentBase AS
        (
            SELECT
                p.Address,
                CASE
                    WHEN pt.ToothTrtID IS NOT NULL THEN t.Treat
                    WHEN pt.OrthoID     IS NOT NULL THEN o.Khota
                    WHEN pt.OtherTrtID  IS NOT NULL THEN ot.Trt
                    ELSE 'Unknown'
                END AS TreatmentName
            FROM Patient_Trts pt
            INNER JOIN Patient p ON pt.PatientID = p.PatientID
            LEFT JOIN Patient_ToothTrt  t ON pt.ToothTrtID = t.ToothTrtID
            LEFT JOIN OrthoInf          o ON pt.OrthoID    = o.OrthoID
            LEFT JOIN Patient_OtherTrt ot ON pt.OtherTrtID = ot.OtherTrtID
        ),
        Filtered AS
        (
            SELECT *
            FROM TreatmentBase
            " & whereSQL & "
        ),
        Ranked AS
        (
            SELECT
                Address,
                TreatmentName,
                COUNT(*) AS TreatmentCount,
                ROW_NUMBER() OVER
                (
                    PARTITION BY Address
                    ORDER BY COUNT(*) DESC
                ) AS RN
            FROM Filtered
            GROUP BY Address, TreatmentName
        )
        SELECT
            Address,
            TreatmentName,
            TreatmentCount
        FROM Ranked
        WHERE RN <= 5
        ORDER BY Address, TreatmentCount DESC;"

            Using da As New SqlDataAdapter(sql, conn)
                Dim dt As New DataTable()
                da.Fill(dt)

                '-----------------------------
                ' Bind per address (series)
                '-----------------------------
                For Each s As Series In chartMostRepeatedByAddress.Series
                    Dim dv As New DataView(dt)
                    dv.RowFilter = "Address = '" & s.Name.Replace("'", "''") & "'"
                    s.DataSource = dv
                    s.ArgumentDataMember = "TreatmentName"
                    s.ValueDataMembers.Clear()
                    s.ValueDataMembers.AddRange("TreatmentCount")
                Next
            End Using
        End Using

    End Sub

    Private Sub LoadMostExpensiveByAddressFiltered(Optional trt As String = Nothing, Optional address As String = Nothing)

        Dim trt1 = If(String.IsNullOrEmpty(trt), "All", trt)
        Dim address1 = If(String.IsNullOrEmpty(address), "All", address)

        If trt1 = "All" AndAlso address1 = "All" Then
            ConfigureAddressBarChart(chartMostExpensiveByAddress,
                                 "Top Most Expensive Treatments (Address)",
                                 Nothing)
        Else
            Dim adr As New List(Of String)
            adr.Add(address1)
            ConfigureAddressBarChart(chartMostExpensiveByAddress,
                                 $"Most Expensive ==> (Treat: [{trt1}]) (Address: [{address1}])",
                                 adr)
        End If

        '-----------------------------
        ' Build WHERE clause
        '-----------------------------
        Dim whereClauses As New List(Of String)

        If Not String.IsNullOrEmpty(trt) Then
            whereClauses.Add("TreatmentName = '" & trt.Replace("'", "''") & "'")
        End If

        If Not String.IsNullOrEmpty(address) Then
            whereClauses.Add("Address = '" & address.Replace("'", "''") & "'")
        End If

        Dim whereSQL As String =
        If(whereClauses.Count > 0,
           "WHERE " & String.Join(" AND ", whereClauses),
           "")

        Using conn As New SqlConnection(_connectionString)
            conn.Open()

            Dim sql As String =
        ";WITH TreatmentBase AS
        (
            SELECT
                p.Address,
                pt.TrtValue,
                CASE
                    WHEN pt.ToothTrtID IS NOT NULL THEN t.Treat
                    WHEN pt.OrthoID     IS NOT NULL THEN o.Khota
                    WHEN pt.OtherTrtID  IS NOT NULL THEN ot.Trt
                    ELSE 'Unknown'
                END AS TreatmentName
            FROM Patient_Trts pt
            INNER JOIN Patient p ON pt.PatientID = p.PatientID
            LEFT JOIN Patient_ToothTrt  t ON pt.ToothTrtID = t.ToothTrtID
            LEFT JOIN OrthoInf          o ON pt.OrthoID    = o.OrthoID
            LEFT JOIN Patient_OtherTrt ot ON pt.OtherTrtID = ot.OtherTrtID
        ),
        Filtered AS
        (
            SELECT *
            FROM TreatmentBase
            " & whereSQL & "
        ),
        Ranked AS
        (
            SELECT
                Address,
                TreatmentName,
                SUM(TrtValue) AS TotalValue,
                ROW_NUMBER() OVER
                (
                    PARTITION BY Address
                    ORDER BY SUM(TrtValue) DESC
                ) AS RN
            FROM Filtered
            GROUP BY Address, TreatmentName
        )
        SELECT
            Address,
            TreatmentName,
            TotalValue
        FROM Ranked
        WHERE RN <= 5
        ORDER BY Address, TotalValue DESC;"

            Using da As New SqlDataAdapter(sql, conn)
                Dim dt As New DataTable()
                da.Fill(dt)

                For Each s As Series In chartMostExpensiveByAddress.Series
                    Dim dv As New DataView(dt)
                    dv.RowFilter = "Address = '" & s.Name.Replace("'", "''") & "'"
                    s.DataSource = dv
                    s.ArgumentDataMember = "TreatmentName"
                    s.ValueDataMembers.Clear()
                    s.ValueDataMembers.AddRange("TotalValue")
                Next
            End Using
        End Using

    End Sub

    Private Sub LoadMostTreatedTeethByAddressFiltered(Optional toothName As String = Nothing, Optional address As String = Nothing)

        Dim trt1 = If(String.IsNullOrEmpty(toothName), "All", toothName)
        Dim address1 = If(String.IsNullOrEmpty(address), "All", address)

        If trt1 = "All" AndAlso address1 = "All" Then
            ConfigureAddressBarChart(chartMostTreatedTeethByAddress,
                                 "Top Most Treated Teeth (Address)",
                                 Nothing)
        Else
            Dim adr As New List(Of String)
            adr.Add(address1)
            ConfigureAddressBarChart(chartMostTreatedTeethByAddress,
                                 $"Most Treated Teeth ==> (Tooth: [{trt1}]) (Address: [{address1}])",
                                 adr)
        End If

        '-----------------------------
        ' Build WHERE clause
        '-----------------------------
        Dim whereClauses As New List(Of String)

        If Not String.IsNullOrEmpty(toothName) Then
            whereClauses.Add("ToothName = '" & toothName.Replace("'", "''") & "'")
        End If

        If Not String.IsNullOrEmpty(address) Then
            whereClauses.Add("Address = '" & address.Replace("'", "''") & "'")
        End If

        Dim whereSQL As String =
        If(whereClauses.Count > 0,
           "WHERE " & String.Join(" AND ", whereClauses),
           "")

        Using conn As New SqlConnection(_connectionString)
            conn.Open()

            Dim sql As String =
        ";WITH ToothBase AS
        (
            SELECT
                p.Address,
                t.ToothName AS ToothName
            FROM Patient_Trts pt
            INNER JOIN Patient p ON pt.PatientID = p.PatientID
            INNER JOIN Patient_ToothTrt t ON pt.ToothTrtID = t.ToothTrtID
        ),
        Filtered AS
        (
            SELECT *
            FROM ToothBase
            " & whereSQL & "
        ),
        Ranked AS
        (
            SELECT
                Address,
                ToothName,
                COUNT(*) AS TreatmentCount,
                ROW_NUMBER() OVER
                (
                    PARTITION BY Address
                    ORDER BY COUNT(*) DESC
                ) AS RN
            FROM Filtered
            GROUP BY Address, ToothName
        )
        SELECT
            Address,
            ToothName AS TreatmentName,
            TreatmentCount
        FROM Ranked
        WHERE RN <= 5
        ORDER BY Address, TreatmentCount DESC;"

            Using da As New SqlDataAdapter(sql, conn)
                Dim dt As New DataTable()
                da.Fill(dt)

                For Each s As Series In chartMostTreatedTeethByAddress.Series
                    Dim dv As New DataView(dt)
                    dv.RowFilter = "Address = '" & s.Name.Replace("'", "''") & "'"
                    s.DataSource = dv
                    s.ArgumentDataMember = "TreatmentName"
                    s.ValueDataMembers.Clear()
                    s.ValueDataMembers.AddRange("TreatmentCount")
                Next
            End Using
        End Using

    End Sub






End Class


'Dim sql11 As String = "
'        ;WITH TreatmentBase AS ( /* same CTE as before */ )
'        SELECT 
'            TreatmentName,
'            Address,
'            COUNT(*) AS TreatmentCount
'        FROM TreatmentBase
'        WHERE Address IN ('" & String.Join("','", topAddresses) & "')
'        GROUP BY TreatmentName, Address
'        ORDER BY TreatmentCount DESC"




'Dim filter As New DashboardFilter() With {
'.DateFrom = Date.Today.AddDays(-90),
'.DateTo = Date.Today}


'Private Sub LoadTreatmentData1()
'    Dim filter As New DashboardFilter() With {
'            .DateFrom = Date.Today.AddDays(-90),
'            .DateTo = Date.Today
'        }
'    ' Load active treatments
'    Dim treatments = _dbHelper.GetTreatments(filter)
'    gridControlActiveTreatments.DataSource = treatments
'    ' Load tooth treatments for tree
'    LoadToothTreatments()
'    ' Load treatment type distribution
'    LoadTreatmentTypeChart()
'End Sub


'Dim sql1 As String = "
'                SELECT TOP 10 TreatmentName, COUNT(*) AS TreatmentCount
'                FROM
'                (
'                    -- Tooth Treatments
'                    SELECT t.Treat AS TreatmentName
'                    FROM Patient_ToothTrt t
'                    INNER JOIN Patient_Trts pt ON t.ToothTrtID = pt.ToothTrtID

'                    UNION ALL

'                    -- Orthodontics
'                    SELECT o.Khota AS TreatmentName
'                    FROM OrthoInf o
'                    INNER JOIN Patient_Trts pt ON o.OrthoID = pt.OrthoID

'                    UNION ALL

'                    -- Other Treatments
'                    SELECT ot.Trt AS TreatmentName
'                    FROM Patient_OtherTrt ot
'                    INNER JOIN Patient_Trts pt ON ot.OtherTrtID = pt.OtherTrtID
'                ) AS AllTreatments
'                GROUP BY TreatmentName
'                ORDER BY TreatmentCount DESC"

