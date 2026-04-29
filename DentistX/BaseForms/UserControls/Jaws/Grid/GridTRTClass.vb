
Imports Infragistics.Win
Imports Infragistics.Win.UltraWinGrid
Imports System.ComponentModel
Imports Dapper
Imports System.Data.SqlClient
Imports System.Drawing
Imports System.IO



Public Class GridTRTClass
    Implements INotifyPropertyChanged
    Implements IParentForm
    Implements IPatientAwareUserControl


    Private Sub IPatientAwareUserControl_LoadPatientData(patientId As Integer) Implements IPatientAwareUserControl.LoadPatientData
        Me.Hide()
        LoadPatientData(patientId)
        Me.Show()
    End Sub

    Public Sub LoadPatientData(patientId As Integer)
        loadClasses(patientId)
    End Sub

    Private originalBounds As New Dictionary(Of Control, Rectangle)
    Private originalSize As Size = New Size(1214, 398)

    '======================================

    Private controlData As New Dictionary(Of Control, (Point, Size))
    Private isResizing As Boolean = False

    Private originaMelSize As Size

    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        ' Store original size of MPanel
        originalSize = New Size(1214, 398)
        'Store bounds for all relevant controls inside MPanel
        StoreOriginalBounds(Me)
    End Sub

    Public Sub New(ByVal clsPatient As Patient)
        ' This call is required by the designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        ' Store original size of MPanel
        originalSize = New Size(1214, 398)
        'Store bounds for all relevant controls inside MPanel
        StoreOriginalBounds(Me)
        If clsPatient.PatientID > 0 Then loadClasses(clsPatient.PatientID)
    End Sub
    Protected Overrides Sub OnCreateControl()

        MyBase.OnCreateControl()
        ' Force initial resize when dropped onto a form
        If originalBounds.Count > 0 Then ResizeControls()
    End Sub


    Private Sub GridTRTClass_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Store original size of MPanel
        Dim sw As New Stopwatch
        sw.Start()
        originalSize = MPanel.Size
        StoreOriginalBounds(MPanel)

        'TODO: This line of code loads data into the 'DsTRT.PatientColors' table. You can move, or remove it, as needed.
        'PatientID = 0
        'PatientName = ""
        Me.Visible = fin
        Try
            If Eng Then
                btAddTreat.Text = "SAVE"
                btAddKidTrt.Text = "SAVE"
            Else
                btAddTreat.Text = "حفظ"
                btAddKidTrt.Text = "حفظ"
            End If
            'FillCbo()
            If PatientID > 0 Then loadClasses(PatientID)
            FillCboIMP()
            fin = True
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        If originalBounds.Count > 0 Then ResizeControls()
        sw.Stop()
        LogToFile("GridTRTClass_Load Time spent: " & sw.Elapsed.TotalSeconds.ToString("F2") & " seconds", Me)
    End Sub

#Region "Resizing"







    '===========================
    Private Sub StoreOriginalBounds(container As Control)
        For Each ctrl As Control In container.Controls
            originalBounds(ctrl) = ctrl.Bounds
            If ctrl.HasChildren Then
                StoreOriginalBounds(ctrl) ' Recursive for nested controls
            End If
        Next
    End Sub

    Private Sub GridTRTClass_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        Dim sw As New Stopwatch
        sw.Start()
        ResizeControls()
        sw.Stop()
        LogToFile("GridTRTClass_Resize Time spent: " & sw.Elapsed.TotalSeconds.ToString("F2") & " seconds", Me)
    End Sub

    Private Sub ResizeControls()
        If originalSize.Width = 0 OrElse originalSize.Height = 0 Then Exit Sub
        'If originalSize = New Size(1214, 398) Then Exit Sub
        Dim xRatio As Double = MPanel.Width / originalSize.Width
        Dim yRatio As Double = MPanel.Height / originalSize.Height

        For Each kvp In originalBounds
            Dim ctrl As Control = kvp.Key
            Dim rect As Rectangle = kvp.Value

            ctrl.SetBounds(
            CInt(rect.X * xRatio),
            CInt(rect.Y * yRatio),
            CInt(rect.Width * xRatio),
            CInt(rect.Height * yRatio)
                            )
            If ctrl.HasChildren Then
                For Each child In ctrl.Controls
                    child.SetBounds(
            CInt(rect.X * xRatio),
            CInt(rect.Y * yRatio),
            CInt(rect.Width * xRatio),
            CInt(rect.Height * yRatio)
                                    )
                Next
            End If
        Next

        ' Update UltraGrid row/column sizes after resizing
        Dim newColWidth As Integer = CInt(74 * xRatio)
        Dim newRowHeight As Integer = CInt(46 * yRatio)

        For Each ctrl As Control In MPanel.Controls
            If TypeOf ctrl Is Infragistics.Win.UltraWinGrid.UltraGrid Then
                Dim grid = CType(ctrl, Infragistics.Win.UltraWinGrid.UltraGrid)
                grid.DisplayLayout.AutoFitStyle = AutoFitStyle.ResizeAllColumns
                grid.DisplayLayout.ScrollBounds = ScrollBounds.ScrollToFill
                grid.DisplayLayout.Override.RowSizing = RowSizing.Fixed

                grid.DisplayLayout.Override.DefaultColWidth = newColWidth
                grid.DisplayLayout.Override.DefaultRowHeight = newRowHeight
                AdjustRowHeights(grid)
            End If
        Next

    End Sub

    Private Sub AdjustRowHeights(grid As Infragistics.Win.UltraWinGrid.UltraGrid)
        Dim totalGridHeight As Integer = grid.Height
        Dim headerHeight As Integer = grid.DisplayLayout.Bands(0).Header.Height
        Dim footerHeight As Integer = 0 ' Adjust if you have a footer
        Dim scrollbarHeight As Integer = If(grid.DisplayLayout.ScrollBounds = ScrollBounds.ScrollToFill, 0, SystemInformation.HorizontalScrollBarHeight)

        ' Calculate the available height for rows
        Dim availableHeight As Integer = totalGridHeight - headerHeight - footerHeight - scrollbarHeight

        ' Get the number of rows
        Dim rowCount As Integer = grid.Rows.Count
        If rowCount = 0 Then Exit Sub

        ' Calculate the new row height
        Dim newRowHeight As Integer = totalGridHeight \ rowCount ' \ availableHeight \ rowCount

        ' Set the new row height
        grid.DisplayLayout.Override.DefaultRowHeight = newRowHeight
    End Sub

    '=======================================================
    Private Sub UpdateGridLayout(ctrl As Control, newColWidth As Integer, newRowHeight As Integer)
        If TypeOf ctrl Is Infragistics.Win.UltraWinGrid.UltraGrid Then
            Dim grid = CType(ctrl, Infragistics.Win.UltraWinGrid.UltraGrid)
            grid.DisplayLayout.Override.DefaultColWidth = newColWidth
            grid.DisplayLayout.Override.DefaultRowHeight = newRowHeight
        ElseIf ctrl.HasChildren Then
            For Each child In ctrl.Controls
                UpdateGridLayout(child, newColWidth, newRowHeight)
            Next
        End If
    End Sub
    '=============================
    Private Sub ResizeUltraGrids(xRatio As Double, yRatio As Double)
        Dim newColWidth As Integer = CInt(74 * xRatio)
        Dim newRowHeight As Integer = CInt(46 * yRatio)

        For Each ctrl As Control In MPanel.Controls
            ApplyGridSize(ctrl, newColWidth, newRowHeight)
        Next
    End Sub

    Private Sub ApplyGridSize(ctrl As Control, colWidth As Integer, rowHeight As Integer)
        If TypeOf ctrl Is Infragistics.Win.UltraWinGrid.UltraGrid Then
            Dim grid = CType(ctrl, Infragistics.Win.UltraWinGrid.UltraGrid)
            grid.DisplayLayout.Override.DefaultColWidth = colWidth
            grid.DisplayLayout.Override.DefaultRowHeight = rowHeight
        ElseIf ctrl.HasChildren Then
            For Each child In ctrl.Controls
                ApplyGridSize(child, colWidth, rowHeight)
            Next
        End If
    End Sub


#End Region


#Region "Properties"


    ' Private fields for the properties
    Private _valueFromParent As Integer
    Private _isKidFromParent As Boolean
    Private _hasDataFromParent As Boolean
    Private fin As Boolean ' For tracking data load status

    ' Property to handle Patient ID from parent (implements IParentForm.ValueFromParent)
    Public Property ValueFromParent As Integer Implements IParentForm.ValueFromParent
        Get
            Return _valueFromParent
        End Get
        Set(ByVal value As Integer)
            _valueFromParent = value

            loadClasses(value) ' Call method to load data based on the passed PatientID
        End Set
    End Property

    ' Property to handle whether the patient is a kid (implements IParentForm.IsKidFromParent)
    Public Property IsKidFromParent As Boolean Implements IParentForm.IsKidFromParent
        Get
            Return _isKidFromParent
        End Get
        Set(ByVal value As Boolean)
            ' Check if the new value is the same as the current value
            If _isKidFromParent = value Then Return ' Exit early if no change

            _isKidFromParent = value
            If value Then
                ShowKids() ' Call method to show treatment for kids
            Else
                ShowAdults() ' Call method to show treatment for adults
            End If
        End Set
    End Property

    ' Property to handle if the data is finalized or loaded (implements IParentForm.HasDataFromParent)
    Public Property HasDataFromParent As Boolean Implements IParentForm.HasDataFromParent
        Get
            Return _hasDataFromParent
        End Get
        Set(ByVal value As Boolean)
            _hasDataFromParent = value
            fin = value ' Set the internal fin field based on this value
        End Set
    End Property

#End Region

    Private cellCOL, cellROW, CellAdres As Integer
    Private ContextSender, ImgName As String


    Private Sub GridTRTClass_Disposed(sender As Object, e As EventArgs) Handles Me.Disposed
        Trtready = False
    End Sub

    Public Sub ShowAdults()
        Dim sw As New Stopwatch
        sw.Start()
        'HorPanel.Visible = True
        'HorKidPanel.Visible = False

        ' Hide the 8th tooth columns
        Dim columnsToHide As New Dictionary(Of Infragistics.Win.UltraWinGrid.UltraGrid, String()) From {
        {LUUltraGrid, New String() {"LU8"}},
        {RUUltraGrid, New String() {"RU8"}},
        {LDUltraGrid, New String() {"LD8"}},
        {RDUltraGrid, New String() {"RD8"}}
    }
        Dim columnWidth As Integer
        columnWidth = RUUltraGrid.Rows(0).Cells(4).Width
        For Each gridPair In columnsToHide
            Dim grid = gridPair.Key
            For Each columnName In gridPair.Value
                If grid.DisplayLayout.Bands(0).Columns.Exists(columnName) Then
                    grid.DisplayLayout.Bands(0).Columns(columnName).Hidden = False
                End If

            Next

        Next

        If originalBounds.Count > 0 Then ResizeControls()
        columnWidth = RUUltraGrid.Rows(0).Cells(4).Width
        SetAdultLabels(columnWidth)
        Me.Refresh()
        sw.Stop()
        LogToFile("ShowAdults Time spent: " & sw.Elapsed.TotalSeconds.ToString("F2") & " seconds", Me)
    End Sub

    Public Sub ShowKids()
        Dim sw As New Stopwatch
        sw.Start()
        'HorPanel.Visible = False
        'HorKidPanel.Visible = True
        'HorKidPanel.Location = HorPanel.Location
        'HorKidPanel.Size = HorPanel.Size

        ' Hide the 8th tooth columns
        Dim columnsToHide As New Dictionary(Of Infragistics.Win.UltraWinGrid.UltraGrid, String()) From {
        {LUUltraGrid, New String() {"LU8"}},
        {RUUltraGrid, New String() {"RU8"}},
        {LDUltraGrid, New String() {"LD8"}},
        {RDUltraGrid, New String() {"RD8"}}
    }
        Dim columnWidth As Integer
        columnWidth = RUUltraGrid.Rows(0).Cells(4).Width
        For Each gridPair In columnsToHide
            Dim grid = gridPair.Key
            For Each columnName In gridPair.Value
                If grid.DisplayLayout.Bands(0).Columns.Exists(columnName) Then
                    grid.DisplayLayout.Bands(0).Columns(columnName).Hidden = True

                End If

            Next
        Next

        If originalBounds.Count > 0 Then ResizeControls()
        columnWidth = RUUltraGrid.Rows(0).Cells(4).Width
        SetKidLabels(columnWidth)
        Me.Refresh()
        sw.Stop()
        LogToFile("ShowKids Time spent: " & sw.Elapsed.TotalSeconds.ToString("F2") & " seconds", Me)
    End Sub

    Private Sub SetKidLabels(width As Integer)
        LR1.Text = "A"
        LR2.Text = "B"
        LR3.Text = "C"
        LR4.Text = "D"
        LR5.Text = "E"
        LR8.Visible = False
        LL1.Text = "A"
        LL2.Text = "B"
        LL3.Text = "C"
        LL4.Text = "D"
        LL5.Text = "E"
        LL8.Visible = False
        RUcountPL.Width = RUcountPL.Width + 20
        RUcountST.Width = width + 15
        LUcountPL.Width = LUcountPL.Width + 20
        LUcountST.Width = width + 15
        LUcountPL.Left = LUcountPL.Location.X - 22
        LUcountST.Left = LUcountST.Location.X - 13
        LUcountPL.BackColor = Color.LightGray
        LUcountST.BackColor = Color.LightGray
        RDcountPL.Width = RDcountPL.Width + 20
        RDcountST.Width = width + 15
        LDcountPL.Width = LDcountPL.Width + 20
        LDcountST.Width = width + 15
        LDcountPL.Left = LDcountPL.Location.X - 22
        LDcountST.Left = LDcountST.Location.X - 13
    End Sub

    Private Sub SetAdultLabels(width As Integer)
        LR1.Text = "1"
        LR2.Text = "2"
        LR3.Text = "3"
        LR4.Text = "4"
        LR5.Text = "5"
        LR8.Visible = True
        LL1.Text = "1"
        LL2.Text = "2"
        LL3.Text = "3"
        LL4.Text = "4"
        LL5.Text = "5"
        LL8.Visible = True
        RUcountPL.Width = 148 ' RUcountPL.Width + 20
        RUcountST.Width = 74 ' width + 20
        LUcountPL.Width = 148 ' LUcountPL.Width + 20
        LUcountST.Width = 74 ' width + 20
        RDcountPL.Width = 148 ' RDcountPL.Width + 20
        RDcountST.Width = 74 ' width + 20
        LDcountPL.Width = 148 ' LDcountPL.Width + 20
        LDcountST.Width = 74 ' width + 20
    End Sub

#Region "GridColors"

    Dim bakco As Color = Color.FromArgb(191, 219, 255)


    ' Properties with default values
    Private _c1 As String = "'#D8BFD8" 'Color.Thistle
    Private _c2 As String = "#DDA0DD" 'Color.Plum
    Private _a As Integer = 0
    Private _g As GradientStyle = GradientStyle.ForwardDiagonal
    Private _Reset As Integer = 1
    ' Event to handle property changes
    Public Event PropertyChanged As System.ComponentModel.PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

    ' Property C1 with notification
    Public Property C1 As String
        Get
            Return _c1
        End Get
        Set(ByVal value As String)
            If _c1 <> value Then
                _c1 = value
                OnPropertyChanged(NameOf(C1))
                ExecuteGrad()
            End If
        End Set
    End Property

    ' Property C2 with notification
    Public Property C2 As String
        Get
            Return _c2
        End Get
        Set(ByVal value As String)
            If _c2 <> value Then
                _c2 = value
                OnPropertyChanged(NameOf(C2))
                ExecuteGrad()
            End If
        End Set
    End Property

    ' Property A with notification
    Public Property A As Integer
        Get
            Return _a
        End Get
        Set(ByVal value As Integer)
            If _a <> value Then
                _a = value
                OnPropertyChanged(NameOf(A))
                ExecuteGrad()
            End If
        End Set
    End Property

    ' Property G with notification
    Public Property G As GradientStyle
        Get
            Return _g
        End Get
        Set(ByVal value As GradientStyle)
            If _g <> value Then
                _g = value
                OnPropertyChanged(NameOf(G))
                ExecuteGrad()
            End If
        End Set
    End Property

    ' Property G with notification
    Public Property Resetting As Integer
        Get
            Return _Reset
        End Get
        Set(ByVal value As Integer)
            If _Reset <> value Then
                _Reset = value
                OnPropertyChanged(NameOf(Resetting))
                ExecuteGrad()
            End If
        End Set
    End Property

    ' Method to raise the PropertyChanged event
    Protected Sub OnPropertyChanged(ByVal propertyName As String)
        RaiseEvent PropertyChanged(Me, New System.ComponentModel.PropertyChangedEventArgs(propertyName))
    End Sub

    ' Sub to execute when any property changes
    Private Sub ExecuteGrad()
        Grad(_c1, _c2, _a, _Reset, _g)
    End Sub

    Public Sub Grad(Cl1 As String, Cl2 As String, A As Integer, reset As Integer, GradStyle As GradientStyle)
        Dim sw As New Stopwatch
        sw.Start()
        Dim grids() As UltraGrid = {LUUltraGrid, LDUltraGrid, RUUltraGrid, RDUltraGrid}
        Dim C1 As Color = ColorTranslator.FromHtml(Cl1)
        Dim C2 As Color = ColorTranslator.FromHtml(Cl2)
        For Each grid As UltraGrid In grids
            Dim count As Integer = grid.Rows.Count
            For i = 0 To count - 1
                grid.Rows(i).Appearance.BackColor = C1
                grid.Rows(i).Appearance.BackColor2 = C2
                grid.Rows(i).Appearance.AlphaLevel = A
                grid.Rows(i).Appearance.BackColorAlpha = Alpha.UseAlphaLevel
                grid.Rows(i).Appearance.BackGradientStyle = GradStyle
            Next
        Next
        If reset = 0 Then
            Me.BackColor = bakco
        Else
            Me.BackColor = GetIntermediateColor(C1, C2)
        End If
        sw.Stop()
        LogToFile("Grad Time spent: " & sw.Elapsed.TotalSeconds.ToString("F2") & " seconds", Me)
    End Sub

    Public Function GetIntermediateColor(color1 As Color, color2 As Color) As Color
        Dim sw As New Stopwatch
        sw.Start()
        ' Cast the color components to Integer to avoid overflow
        Dim red As Integer = CInt(color1.R) + CInt(color2.R)
        Dim green As Integer = CInt(color1.G) + CInt(color2.G)
        Dim blue As Integer = CInt(color1.B) + CInt(color2.B)

        ' Calculate the average and clamp the values to ensure they stay within the valid range (0-255)
        red = Math.Min(red \ 2, 255)
        green = Math.Min(green \ 2, 255)
        blue = Math.Min(blue \ 2, 255)

        ' Return the intermediate color
        Return Color.FromArgb(red, green, blue)
        sw.Stop()
        LogToFile("GetIntermediateColor Time spent: " & sw.Elapsed.TotalSeconds.ToString("F2") & " seconds", Me)
    End Function

    Public Sub Coloring()
        Try
            Dim sw As New Stopwatch
            sw.Start()
            clsPatient = New Patient
            clsPatient.PatientID = PatientID
            clsPatient = clsPatientData.Select_Record(clsPatient)
            If clsPatient IsNot Nothing Then
                ' Iterate over each LDSTYLE item in LdStl
                For Each clrItem As PatientColors In GrdClrs
                    ' Check if the PatientID matches
                    If clrItem.PatientID = PatientID Then
                        Grad(clrItem.Color1, clrItem.Color2, clrItem.AlphaValue, 1, clrItem.GradientIndex)
                    End If
                Next
            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub



#End Region


#Region "Patient Context"


    Dim clickedCell As UltraGridCell


    Private Sub HandleImageRemovalAndUpdateOld(ByVal style As String, ByVal cellID As Integer, ByVal patientID As Integer, ByVal cellAddress As Integer, ByVal grid As UltraGrid, ByVal adapter As Object, ByVal styleUpdateAction As Action)
        Try
            Dim row, col As Integer
            row = AddressToColumnRow(cellAddress).Row
            col = AddressToColumnRow(cellAddress).Column
            grid.Rows(row).Cells(col).Appearance.ImageBackground = Nothing

            ' Perform deletion and update
            If DeleteStyleRecord(style, cellID, patientID, cellAddress) = 1 Then
                grid.Rows(row).Cells(col).Appearance.ImageBackground = Nothing
                'adapter.FillByID(Me.DsTRT.Tables(style.ToUpper()), patientID)
                styleUpdateAction.Invoke()
            Else
                MsgBox("Could not remove image")
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub HandleColorUpdateOld(ByVal style As String, ByVal patientID As Integer, ByVal cellAddress As Integer, ByVal colorName As String, ByVal adapter As Object, ByVal fillMethod As Action(Of DataTable, Integer), ByVal styleUpdateAction As Action)
        Try
            Dim cellID As Integer = GetCellIDForColor(style, patientID, cellAddress)

            If cellID > 0 Then
                Select Case style.ToUpper()
                    Case "LUPL"
                        'Qrs.LUPLUpdate(cellID, patientID, cellAddress, colorName)
                    Case "LDPL"
                        'Qrs.LDPLUpdate(cellID, patientID, cellAddress, colorName)
                    Case "RUPL"
                        'Qrs.RUPLUpdate(cellID, patientID, cellAddress, colorName)
                    Case "RDPL"
                        'Qrs.RDPLUpdate(cellID, patientID, cellAddress, colorName)
                End Select

                'adapter.FillByID(Me.DsTRT.Tables(style.ToUpper()), patientID)
                styleUpdateAction.Invoke()
            Else
                Select Case style.ToUpper()
                    Case "LUPL"
                        'Qrs.LUPLInsert(patientID, cellAddress, colorName)
                    Case "LDPL"
                        'Qrs.LDPLInsert(patientID, cellAddress, colorName)
                    Case "RUPL"
                        'Qrs.RUPLInsert(patientID, cellAddress, colorName)
                    Case "RDPL"
                        'Qrs.RDPLInsert(patientID, cellAddress, colorName)
                End Select

                'adapter.FillByID(Me.DsTRT.Tables(style.ToUpper()), patientID)
                styleUpdateAction.Invoke()
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub


#End Region




#Region "NEW CODE"


#Region "Calss Variables"

    Private GrdClrs As IEnumerable(Of PatientColors)
    Private LdS As IEnumerable(Of LD)
    Private LuS As IEnumerable(Of LU)
    Private RuS As IEnumerable(Of RU)
    Private RdS As IEnumerable(Of RD)

    Private RdPlS As IEnumerable(Of RDPL)
    Private RuPlS As IEnumerable(Of RUPL)
    Private LdPlS As IEnumerable(Of LDPL)
    Private LuPlS As IEnumerable(Of LUPL)

    Private RdStl As IEnumerable(Of RDSTYLE)
    Private RuStl As IEnumerable(Of RUSTYLE)
    Private LuStl As IEnumerable(Of LUSTYLE)
    Private LdStl As IEnumerable(Of LDSTYLE)

    Dim clsPatientData As New PatientDATA
    Dim clsPatient As Patient
    Dim clsLDData As New LDDATA
    Dim clsLUData As New LUDATA
    Dim clsRDData As New RDDATA
    Dim clsRUData As New RUDATA
    '=====================================================

#End Region



    ''Public WriteOnly Property ValueFromParent() As Integer
    ''    Set(ByVal Value As Integer)
    ''        'LoadData(Value)
    ''        loadClasses(Value)
    ''    End Set
    ''End Property






    Private Sub FillCboByTooth1(toothNum As Byte)
        ' Define the SQL query to fetch data
        Dim query As String = "
        SELECT TrtID, Trt, TrtShape, TrtDetails, TrtAr, TrtArDetails, ToothID, OldTrt, TrtGroup, TrtColor
        FROM TblTRTS
        WHERE (ToothID = 'ALL') OR CHARINDEX(',' + @ToothID + ',', ',' + ToothID + ',') > 0
        ORDER BY TrtGroup, Trt"

        ' Initialize the connection to the database
        Using connection As New SqlConnection(My.Settings.DentistXConnectionString)
            ' Open the connection
            connection.Open()

            ' Execute the query using Dapper
            Dim results = connection.Query(query, New With {.ToothID = toothNum}).ToList()

            ' Populate the combo box with the results
            CboStrip.Items.Clear()
            For Each row In results
                ' Assuming that the 7th column corresponds to the TrtColor or another item you want
                CboStrip.Items.Add(row.TrtColor.ToString())
            Next
        End Using
    End Sub




#Region "SUBS"

    Public Sub loadClasses(patientId As Integer)
        Dim sw As New Stopwatch
        sw.Start()
        Try
            If patientId <= 0 Then Exit Sub
            clsPatient = New Patient
            clsPatient.PatientID = patientId
            clsPatient = CurrentPatient ' clsPatientData.Select_Record(clsPatient)
            'CurrentPatient = clsPatient
            'Module1.PatientName = clsPatient.PatientName
            'Module1.PatientID = clsPatient.PatientID
            If clsPatient IsNot Nothing Then
                'PatientColors
                GrdClrs = clsPatientData.GetPatientColors(clsPatient)
                If PatientColorsBindingSource Is Nothing Then
                    PatientColorsBindingSource = New BindingSource()
                End If
                PatientColorsBindingSource.DataSource = GrdClrs.ToList()
                '4 Qrtrs
                LdS = clsPatientData.GetLD(clsPatient)
                ' Ensure LdBS is initialized
                If LDBindingSource Is Nothing Then
                    LDBindingSource = New BindingSource()
                End If
                ' Set the DataSource
                LDBindingSource.DataSource = LdS.ToList()
                'Lu
                LuS = clsPatientData.GetLU(clsPatient)
                ' Ensure LdBS is initialized
                If LUBindingSource Is Nothing Then
                    LUBindingSource = New BindingSource()
                End If
                ' Set the DataSource
                LUBindingSource.DataSource = LuS.ToList()
                'Ru
                RuS = clsPatientData.GetRU(clsPatient)
                ' Ensure LdBS is initialized
                If RUBindingSource Is Nothing Then
                    RUBindingSource = New BindingSource()
                End If
                ' Set the DataSource
                RUBindingSource.DataSource = RuS.ToList()
                'Rd
                RdS = clsPatientData.GetRD(clsPatient)
                ' Ensure LdBS is initialized
                If RDBindingSource Is Nothing Then
                    RDBindingSource = New BindingSource()
                End If
                ' Set the DataSource
                RDBindingSource.DataSource = RdS.ToList()
                '=================================================

                'PLS
                LdPlS = clsPatientData.GetLDPL(clsPatient)
                ' Ensure LdBS is initialized
                If LDPLBindingSource Is Nothing Then
                    LDPLBindingSource = New BindingSource()
                End If
                ' Set the DataSource
                LDPLBindingSource.DataSource = LdPlS.ToList()
                'Lu
                LuPlS = clsPatientData.GetLUPL(clsPatient)
                ' Ensure LdBS is initialized
                If LUPLBindingSource Is Nothing Then
                    LUPLBindingSource = New BindingSource()
                End If
                ' Set the DataSource
                LUPLBindingSource.DataSource = LuPlS.ToList()
                'Ru
                RuPlS = clsPatientData.GetRUPL(clsPatient)
                ' Ensure LdBS is initialized
                If RUPLBindingSource Is Nothing Then
                    RUPLBindingSource = New BindingSource()
                End If
                ' Set the DataSource
                RUPLBindingSource.DataSource = RuPlS.ToList()
                'Rd
                RdPlS = clsPatientData.GetRDPL(clsPatient)
                ' Ensure LdBS is initialized
                If RDPLBindingSource Is Nothing Then
                    RDPLBindingSource = New BindingSource()
                End If
                ' Set the DataSource
                RDPLBindingSource.DataSource = RdPlS.ToList()
                '=============================================
                'Styles
                LdStl = clsPatientData.GetLDSTYLE(clsPatient)
                ' Ensure LDSTYLEBindingSource is initialized
                If LDSTYLEBindingSource Is Nothing Then
                    LDSTYLEBindingSource = New BindingSource()
                End If
                ' Set the DataSource
                LDSTYLEBindingSource.DataSource = LdStl.ToList()
                'Lu
                LuStl = clsPatientData.GetLUSTYLE(clsPatient)
                ' Ensure LdBS is initialized
                If LUSTYLEBindingSource Is Nothing Then
                    LUSTYLEBindingSource = New BindingSource()
                End If
                ' Set the DataSource
                LUSTYLEBindingSource.DataSource = LuStl.ToList()
                'Ru
                RuStl = clsPatientData.GetRUSTYLE(clsPatient)
                ' Ensure LdBS is initialized
                If RUSTYLEBindingSource Is Nothing Then
                    RUSTYLEBindingSource = New BindingSource()
                End If
                ' Set the DataSource
                RUSTYLEBindingSource.DataSource = RuStl.ToList()
                'Rd
                RdStl = clsPatientData.GetRDSTYLE(clsPatient)
                ' Ensure LdBS is initialized
                If RDSTYLEBindingSource Is Nothing Then
                    RDSTYLEBindingSource = New BindingSource()
                End If
                ' Set the DataSource
                RDSTYLEBindingSource.DataSource = RdStl.ToList()
            End If

            Styles()
            Coloring()
        Catch ex As System.Data.SqlClient.SqlException
            MsgBox(ex.Message)

        End Try
        sw.Stop()
        LogToFile("loadClasses Time spent: " & sw.Elapsed.TotalSeconds.ToString("F2") & " seconds", Me)
    End Sub



#Region "NewStyle"
    'New Code
    Public Sub LDStyle1()
        Dim sw As New Stopwatch
        sw.Start()
        Try
            'Dim img As Byte()
            'Dim IMG1 As Image

            ' Clear existing background images before updating
            For Each row In Me.LDUltraGrid.Rows
                For Each cell In row.Cells
                    cell.Appearance.ImageBackground = Nothing
                Next
            Next

            If Me.LDSTYLEBindingSource.Count > 0 Then

                ' Iterate over each LDSTYLE item in LdStl
                For Each ldStyleItem As LDSTYLE In LdStl
                    ' Check if the PatientID matches
                    If ldStyleItem.PatientID = PatientID Then
                        ' Convert cell address to row and column indices
                        Dim cellPosition = AddressToColumnRow(ldStyleItem.CellAddres)
                        Dim colIndex As Integer = cellPosition.Column
                        Dim rowIndex As Integer = cellPosition.Row

                        ' Ensure rowIndex and colIndex are within bounds of the grid
                        If rowIndex >= 0 AndAlso rowIndex < Me.LDUltraGrid.Rows.Count AndAlso
                            colIndex >= 0 AndAlso colIndex < Me.LDUltraGrid.DisplayLayout.Bands(0).Columns.Count Then

                            ' Get the image data and set it if available
                            Dim img = ldStyleItem.BakImg
                            If img IsNot Nothing AndAlso img.Length > 0 Then
                                Using imageBytedata As New MemoryStream(img)
                                    Dim IMG1 = Image.FromStream(imageBytedata)
                                    Dim cell = Me.LDUltraGrid.Rows(rowIndex).Cells(colIndex)

                                    ' Set image and appearance properties
                                    cell.Appearance.ImageBackground = IMG1
                                    cell.Appearance.ImageBackgroundAlpha = Alpha.UseAlphaLevel
                                    cell.Appearance.ImageBackgroundStyle = ImageBackgroundStyle.Default
                                End Using
                            End If
                        End If
                    End If
                Next

                If Eng Then
                    Me.LDcountST.Text = Me.LDSTYLEBindingSource.Count & " PICS"
                Else
                    Me.LDcountST.Text = Me.LDSTYLEBindingSource.Count & " صورة"
                End If
            Else
                Me.LDcountST.Text = "0 PICS"
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        sw.Stop()
        LogToFile("LDStyle1 Time spent: " & sw.Elapsed.TotalSeconds.ToString("F2") & " seconds", Me)
    End Sub

    Public Sub LUStyle1()
        Dim sw As New Stopwatch
        sw.Start()
        Try
            ' Clear existing background images before updating
            For Each row In Me.LUUltraGrid.Rows
                For Each cell In row.Cells
                    cell.Appearance.ImageBackground = Nothing
                Next
            Next

            If Me.LUSTYLEBindingSource.Count > 0 Then
                ' Update the status text based on the count
                Me.LUcountST.Text = If(Eng, $"{Me.LUSTYLEBindingSource.Count} PICS", $"{Me.LUSTYLEBindingSource.Count} صورة")
                ' Iterate over each LUSTYLE item in LuStl
                For Each luStyleItem As LUSTYLE In LuStl
                    ' Check if the PatientID matches
                    If luStyleItem.PatientID = PatientID Then
                        ' Convert cell address to row and column indices
                        Dim cellPosition = AddressToColumnRow(luStyleItem.CellAddres)
                        Dim colIndex As Integer = cellPosition.Column
                        Dim rowIndex As Integer = cellPosition.Row

                        ' Ensure rowIndex and colIndex are within bounds of the grid
                        If rowIndex >= 0 AndAlso rowIndex < Me.LUUltraGrid.Rows.Count AndAlso
                            colIndex >= 0 AndAlso colIndex < Me.LUUltraGrid.DisplayLayout.Bands(0).Columns.Count Then

                            ' Get the image data and set it if available
                            Dim img = luStyleItem.BakImg
                            If img IsNot Nothing AndAlso img.Length > 0 Then
                                Using imageBytedata As New MemoryStream(img)
                                    Dim IMG1 = Image.FromStream(imageBytedata)
                                    Dim cell = Me.LUUltraGrid.Rows(rowIndex).Cells(colIndex)

                                    ' Set image and appearance properties
                                    cell.Appearance.ImageBackground = IMG1
                                    cell.Appearance.ImageBackgroundAlpha = Alpha.UseAlphaLevel
                                    cell.Appearance.ImageBackgroundStyle = ImageBackgroundStyle.Default
                                End Using
                            End If
                        End If
                    End If
                Next
                ' Update the count label text based on the Eng flag
                If Eng Then
                    Me.LDcountST.Text = Me.LDSTYLEBindingSource.Count & " PICS"
                Else
                    Me.LDcountST.Text = Me.LDSTYLEBindingSource.Count & " صورة"
                End If
            Else
                ' No items in LDSTYLEBindingSource
                If Eng Then
                    Me.LDcountST.Text = "0 PICS"
                Else
                    Me.LDcountST.Text = "0 صورة"
                End If
            End If
        Catch ex As IOException
            MsgBox(ex.Message)
        Catch ex As SqlException
            MsgBox(ex.Message)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        sw.Stop()
        LogToFile("LUStyle1 Time spent: " & sw.Elapsed.TotalSeconds.ToString("F2") & " seconds", Me)
    End Sub

    Public Sub RDStyle1()
        Dim sw As New Stopwatch
        sw.Start()
        Try
            ' Clear existing background images before updating
            For Each row In Me.RDUltraGrid.Rows
                For Each cell In row.Cells
                    cell.Appearance.ImageBackground = Nothing
                Next
            Next

            If Me.RDSTYLEBindingSource.Count > 0 Then
                Me.RDcountST.Text = If(Eng, $"{Me.RDSTYLEBindingSource.Count} PICS", $"{Me.RDSTYLEBindingSource.Count} صورة")
                ' Iterate over each RDSTYLE item in RdStl
                For Each rdStyleItem As RDSTYLE In RdStl
                    ' Check if the PatientID matches
                    If rdStyleItem.PatientID = PatientID Then
                        ' Convert cell address to row and column indices
                        Dim cellPosition = AddressToColumnRow(rdStyleItem.CellAddres)
                        Dim colIndex As Integer = cellPosition.Column
                        Dim rowIndex As Integer = cellPosition.Row

                        ' Ensure rowIndex and colIndex are within bounds of the grid
                        If rowIndex >= 0 AndAlso rowIndex < Me.RDUltraGrid.Rows.Count AndAlso
                            colIndex >= 0 AndAlso colIndex < Me.RDUltraGrid.DisplayLayout.Bands(0).Columns.Count Then

                            ' Get the image data and set it if available
                            Dim img = rdStyleItem.BakImg
                            If img IsNot Nothing AndAlso img.Length > 0 Then
                                Using imageBytedata As New MemoryStream(img)
                                    Dim IMG1 = Image.FromStream(imageBytedata)
                                    Dim cell = Me.RDUltraGrid.Rows(rowIndex).Cells(colIndex)

                                    ' Set image and appearance properties
                                    cell.Appearance.ImageBackground = IMG1
                                    cell.Appearance.ImageBackgroundAlpha = Alpha.UseAlphaLevel
                                    cell.Appearance.ImageBackgroundStyle = ImageBackgroundStyle.Default
                                End Using
                            End If
                        End If
                    End If
                Next
            Else
                ' Update the status text if there are no images
                Me.RDcountST.Text = If(Eng, "0 PICS", "0 صورة")
            End If
        Catch ex As IOException
            MsgBox(ex.Message)
        Catch ex As SqlException
            MsgBox(ex.Message)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        sw.Stop()
        LogToFile("RDStyle1 Time spent: " & sw.Elapsed.TotalSeconds.ToString("F2") & " seconds", Me)
    End Sub

    Public Sub RUStyle1()
        Dim sw As New Stopwatch
        sw.Start()
        Try
            ' Clear existing background images before updating
            For Each row In Me.RUUltraGrid.Rows
                For Each cell In row.Cells
                    cell.Appearance.ImageBackground = Nothing
                Next
            Next

            If Me.RUSTYLEBindingSource.Count > 0 Then
                ' Update the status text based on the count
                Me.RUcountST.Text = If(Eng, $"{Me.RUSTYLEBindingSource.Count} PICS", $"{Me.RUSTYLEBindingSource.Count} صورة")

                ' Iterate over each RUSTYLE item in LdStl
                For Each ruStyleItem As RUSTYLE In RuStl
                    ' Check if the PatientID matches
                    If ruStyleItem.PatientID = PatientID Then
                        ' Convert cell address to row and column indices
                        Dim cellPosition = AddressToColumnRow(ruStyleItem.CellAddres)
                        Dim colIndex As Integer = cellPosition.Column
                        Dim rowIndex As Integer = cellPosition.Row

                        ' Ensure rowIndex and colIndex are within bounds of the grid
                        If rowIndex >= 0 AndAlso rowIndex < Me.RUUltraGrid.Rows.Count AndAlso
                            colIndex >= 0 AndAlso colIndex < Me.RUUltraGrid.DisplayLayout.Bands(0).Columns.Count Then

                            ' Get the image data and set it if available
                            Dim img = ruStyleItem.BakImg
                            If img IsNot Nothing AndAlso img.Length > 0 Then
                                Using imageBytedata As New MemoryStream(img)
                                    Dim IMG1 = Image.FromStream(imageBytedata)
                                    Dim cell = Me.RUUltraGrid.Rows(rowIndex).Cells(colIndex)

                                    ' Set image and appearance properties
                                    cell.Appearance.ImageBackground = IMG1
                                    cell.Appearance.ImageBackgroundAlpha = Alpha.UseAlphaLevel
                                    cell.Appearance.ImageBackgroundStyle = ImageBackgroundStyle.Default
                                End Using
                            End If
                        End If
                    End If
                Next
            Else
                ' Update the status text if there are no images
                Me.RUcountST.Text = If(Eng, "0 PICS", "0 صورة")
            End If
        Catch ex As IOException
            MsgBox(ex.Message)
        Catch ex As SqlException
            MsgBox(ex.Message)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        sw.Stop()
        LogToFile("RUStyle1 Time spent: " & sw.Elapsed.TotalSeconds.ToString("F2") & " seconds", Me)
    End Sub

#End Region

#Region "Styles"
    Public Sub Styles()
        Try
            Dim sw As New Stopwatch
            sw.Start()
            RUStyle1()
            LUStyle1()
            RDStyle1()
            LDStyle1()

            RUPL()
            LUPL()
            RDPL()
            LDPL()

            sw.Stop()
            LogToFile("Styles Time spent: " & sw.Elapsed.TotalSeconds.ToString("F2") & " seconds", Me)
        Catch ex As System.Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Public Sub RUPL()
        Dim sw As New Stopwatch
        sw.Start()
        Try
            ' Update the status text based on the count
            If Me.RUPLBindingSource.Count > 0 Then
                Me.RUcountPL.Text = If(Eng, "RU", "يمين علوي")

                ' Iterate directly over each RUPL item in RuPlS
                For Each ruplItem As RUPL In RuPlS
                    ' Check if the PatientID matches
                    If ruplItem.PatientID = PatientID Then
                        ' Convert cell address to row and column indices
                        Dim cellPosition = AddressToColumnRow(ruplItem.CellAddres)
                        Dim colindex As Integer = cellPosition.Column
                        Dim rowindex As Integer = cellPosition.Row

                        ' Convert the ForeColor to a Color object
                        Dim FrClr As String = ruplItem.ForeColor
                        Dim c As Color = ColorTranslator.FromHtml(FrClr)

                        ' Set the grid cell's appearance
                        If rowindex >= 0 AndAlso rowindex < Me.RUUltraGrid.Rows.Count AndAlso
                           colindex >= 0 AndAlso colindex < Me.RUUltraGrid.DisplayLayout.Bands(0).Columns.Count Then
                            With Me.RUUltraGrid.Rows(rowindex).Cells(colindex)
                                .Activate()
                                .Appearance.ForeColor = c
                            End With
                        End If
                    End If
                Next
            Else
                ' Update the status text if there are no colors
                Me.RUcountPL.Text = If(Eng, "RU", "يمين علوي")
            End If
        Catch ex As IOException
            MsgBox(ex.Message)
        Catch ex As SqlException
            MsgBox(ex.Message)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

        sw.Stop()
        LogToFile("RUPL Time spent: " & sw.Elapsed.TotalSeconds.ToString("F2") & " seconds", Me)
    End Sub

    Public Sub LUPL()
        Dim sw As New Stopwatch
        sw.Start()
        Try
            ' Update the status text based on the count
            If Me.LUPLBindingSource.Count > 0 Then
                Me.LUcountPL.Text = If(Eng, "LU", "يسار علوي")

                ' Iterate directly over each LUPL item in LuPlS
                For Each luplItem As LUPL In LuPlS
                    ' Check if the PatientID matches
                    If luplItem.PatientID = PatientID Then
                        ' Convert cell address to row and column indices
                        Dim cellPosition = AddressToColumnRow(luplItem.CellAddres)
                        Dim colindex As Integer = cellPosition.Column
                        Dim rowindex As Integer = cellPosition.Row

                        ' Convert the ForeColor to a Color object
                        Dim FrClr As String = luplItem.ForeColor
                        Dim c As Color = ColorTranslator.FromHtml(FrClr)

                        ' Set the grid cell's appearance
                        If rowindex >= 0 AndAlso rowindex < Me.LUUltraGrid.Rows.Count AndAlso
                           colindex >= 0 AndAlso colindex < Me.LUUltraGrid.DisplayLayout.Bands(0).Columns.Count Then
                            With Me.LUUltraGrid.Rows(rowindex).Cells(colindex)
                                .Activate()
                                .Appearance.ForeColor = c
                            End With
                        End If
                    End If
                Next
            Else
                ' Update the status text if there are no colors
                Me.LUcountPL.Text = If(Eng, "LU", "يسار علوي")
            End If
        Catch ex As IOException
            MsgBox(ex.Message)
        Catch ex As SqlException
            MsgBox(ex.Message)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

        sw.Stop()
        LogToFile("LUPL Time spent: " & sw.Elapsed.TotalSeconds.ToString("F2") & " seconds", Me)
    End Sub

    Public Sub RDPL()
        Dim sw As New Stopwatch
        sw.Start()
        Try
            ' Update the status text based on the count
            If Me.RDPLBindingSource.Count > 0 Then
                Me.RDcountPL.Text = If(Eng, "RD", "يمين سفلي")

                ' Iterate directly over each RDPL item in RdPlS
                For Each rdplItem As RDPL In RdPlS
                    ' Check if the PatientID matches
                    If rdplItem.PatientID = PatientID Then
                        ' Convert cell address to row and column indices
                        Dim cellPosition = AddressToColumnRow(rdplItem.CellAddres)
                        Dim colindex As Integer = cellPosition.Column
                        Dim rowindex As Integer = cellPosition.Row

                        ' Convert the ForeColor to a Color object
                        Dim FrClr As String = rdplItem.ForeColor
                        Dim c As Color = ColorTranslator.FromHtml(FrClr)

                        ' Set the grid cell's appearance
                        If rowindex >= 0 AndAlso rowindex < Me.RDUltraGrid.Rows.Count AndAlso
                           colindex >= 0 AndAlso colindex < Me.RDUltraGrid.DisplayLayout.Bands(0).Columns.Count Then
                            With Me.RDUltraGrid.Rows(rowindex).Cells(colindex)
                                .Activate()
                                .Appearance.ForeColor = c
                            End With
                        End If
                    End If
                Next
            Else
                ' Update the status text if there are no colors
                Me.RDcountPL.Text = If(Eng, "RD", "يمين سفلي")
            End If
        Catch ex As IOException
            MsgBox(ex.Message)
        Catch ex As SqlException
            MsgBox(ex.Message)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        sw.Stop()
        LogToFile("RDPL Time spent: " & sw.Elapsed.TotalSeconds.ToString("F2") & " seconds", Me)
    End Sub
    Public Sub LDPL()
        Dim sw As New Stopwatch
        sw.Start()
        Try
            ' Update the status text based on the count in the BindingSource
            If Me.LDPLBindingSource.Count > 0 Then
                Me.LDcountPL.Text = If(Eng, "LD", "يسار سفلي")

                ' Iterate directly over each LDPL item in LdPlS
                For Each ldplItem As LDPL In LdPlS
                    ' Check if the PatientID matches
                    If ldplItem.PatientID = PatientID Then
                        ' Convert cell address to row and column indices
                        Dim cellPosition = AddressToColumnRow(ldplItem.CellAddres)
                        Dim colindex As Integer = cellPosition.Column
                        Dim rowindex As Integer = cellPosition.Row

                        ' Convert the ForeColor to a Color object
                        Dim FrClr As String = ldplItem.ForeColor
                        Dim c As Color = ColorTranslator.FromHtml(FrClr)

                        ' Set the grid cell's appearance
                        If rowindex >= 0 AndAlso rowindex < Me.LDUltraGrid.Rows.Count AndAlso
                           colindex >= 0 AndAlso colindex < Me.LDUltraGrid.DisplayLayout.Bands(0).Columns.Count Then
                            With Me.LDUltraGrid.Rows(rowindex).Cells(colindex)
                                .Activate()
                                .Appearance.ForeColor = c
                            End With
                        End If
                    End If
                Next
            Else
                ' Update the status text if there are no colors in LDPLBindingSource
                Me.LDcountPL.Text = If(Eng, "LD", "يسار سفلي")
            End If

        Catch ex As IOException
            MsgBox(ex.Message)
        Catch ex As SqlException
            MsgBox(ex.Message)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

        sw.Stop()
        LogToFile("LDPL Time spent: " & sw.Elapsed.TotalSeconds.ToString("F2") & " seconds", Me)
    End Sub

#End Region

#End Region

#Region "Patient Treats"

    Private Sub btAddTreat_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btAddTreat.Click, btAddKidTrt.Click

        SaveTRT()
    End Sub
    'Private Sub btAddKidTrt_Click(sender As Object, e As EventArgs) Handles btAddKidTrt.Click

    'End Sub
    Public Sub SaveTRT()
        Dim sw As New Stopwatch
        sw.Start()
        Try
            Me.Validate()

            Me.LUBindingSource.EndEdit()
            Me.LDBindingSource.EndEdit()
            Me.RDBindingSource.EndEdit()
            Me.RUBindingSource.EndEdit()
            'Me.LUTableAdapter.Update(Me.DsTRT.LU)
            'Me.LDTableAdapter.Update(Me.DsTRT.LD)
            'Me.RUTableAdapter.Update(Me.DsTRT.RU)
            'Me.RDTableAdapter.Update(Me.DsTRT.RD)
            '===========================================
            UpdateRecord("LDUltraGrid")
            UpdateRecord("LUUltraGrid")
            UpdateRecord("RDUltraGrid")
            UpdateRecord("RUUltraGrid")

        Catch ex As System.Data.SqlClient.SqlException
            MsgBox(ex.Message)
        End Try
        sw.Stop()
        LogToFile("SaveTRT Time spent: " & sw.Elapsed.TotalSeconds.ToString("F2") & " seconds", Me)
    End Sub

    Private Sub SaveRecords()
        ' Fix: Use the correct BindingSource for each type
        Dim updatedRuS As IEnumerable(Of RU) = RUBindingSource.List.OfType(Of RU)()
        Dim updatedLuS As IEnumerable(Of LU) = LUBindingSource.List.OfType(Of LU)()
        Dim updatedRdS As IEnumerable(Of RD) = RDBindingSource.List.OfType(Of RD)()
        Dim updatedLdS As IEnumerable(Of LD) = LDBindingSource.List.OfType(Of LD)()
        Dim patientTreatsData As New Patient_ToothTrtDATA
        Dim allPatientTreats As IEnumerable(Of Patient_ToothTrt)
        allPatientTreats = patientTreatsData.SelectAll
        Dim thisPatientTreats = allPatientTreats.Where(Function(p) p.PatientID = PatientID)

        ' For now, update all properties (simple approach)
        For Each ru As RU In updatedRuS.Where(Function(r) r.PatientID = PatientID)
            For i As Integer = 1 To 8
                Dim propName = $"RU{9 - i}"
                Dim propValue = GetPropertyValue(ru, propName)

                If Not String.IsNullOrEmpty(propValue) Then
                    ' FIX: Use FirstOrDefault() to get a single object or Nothing
                    Dim trt As Patient_ToothTrt = thisPatientTreats.FirstOrDefault _
                    (Function(t) t.QrtrTable = "RU" AndAlso t.QrtrID = ru.RUID AndAlso t.QrtrColumnName = propName)

                    If trt IsNot Nothing Then
                        Dim result = ExtractOldTrtAndDate(propValue)
                        trt.Treat = GetNewTrt(result.OldTrt)
                        trt.ShapeID = GetShapeIDByTrt(trt.Treat)
                        trt.QrtrColumnValue = propValue

                        ' IMPORTANT: You need to save the changes back to the database
                        'patientTreatsData.Update(trt)
                    Else
                        ' Create new record if it doesn't exist
                        Dim newTrt As New Patient_ToothTrt()
                        newTrt.PatientID = PatientID
                        newTrt.QrtrTable = "RU"
                        newTrt.QrtrID = ru.RUID
                        newTrt.QrtrColumnName = propName
                        newTrt.QrtrColumnValue = propValue

                        Dim result = ExtractOldTrtAndDate(propValue)
                        newTrt.Treat = GetNewTrt(result.OldTrt)
                        newTrt.ShapeID = GetShapeIDByTrt(newTrt.Treat)
                        newTrt.TreatDate = If(result.TrtDate, Date.Now) ' Use current date if no date found

                        patientTreatsData.Add(newTrt)
                    End If
                Else
                    ' If propValue is empty, you might want to delete the treatment record
                    Dim trt As Patient_ToothTrt = thisPatientTreats.FirstOrDefault _
                    (Function(t) t.QrtrTable = "RU" AndAlso t.QrtrID = ru.RUID AndAlso t.QrtrColumnName = propName)

                    If trt IsNot Nothing Then
                        patientTreatsData.Delete(trt)
                    End If
                End If
            Next
        Next

        ' Don't forget to save changes for other quarters (LU, RD, LD)
        ' Add similar loops for updatedLuS, updatedRdS, updatedLdS
    End Sub

    Private Sub ProcessQuarterTreatments(Of T)(updatedItems As IEnumerable(Of T), quarterTable As String,
                                         thisPatientTreats As IEnumerable(Of Patient_ToothTrt),
                                         patientTreatsData As Patient_ToothTrtDATA)
        For Each item As T In updatedItems.Where(Function(r) DirectCast(r, Object).PatientID = PatientID)
            Dim itemID As Integer = DirectCast(item, Object).GetType().GetProperty($"{quarterTable}ID").GetValue(item)

            For i As Integer = 1 To 8
                Dim propName = $"{quarterTable}{9 - i}"
                Dim propValue = GetPropertyValue(item, propName)

                ' ... rest of the logic same as above
            Next
        Next
    End Sub


    'UpdatePatientToothTrtForQuarter(ru, propName, propValue)
    Private Function GetPropertyValue(obj As Object, propertyName As String) As String
        Dim prop = obj.GetType().GetProperty(propertyName)
        Return If(prop IsNot Nothing, prop.GetValue(obj)?.ToString(), "")
    End Function

    Private Sub UpdateRecord(GRID As String)
        Dim sw As New Stopwatch
        sw.Start()
        Select Case GRID
            Case "RUUltraGrid"
                For Each row In RUUltraGrid.Rows
                    Dim oclsRU As New RU
                    Dim clsRU As New RU
                    oclsRU.RUID = System.Convert.ToInt32(row.Cells("RUID").Value)
                    oclsRU.PatientID = System.Convert.ToInt32(row.Cells("PatientID").Value)
                    oclsRU = clsRUData.Select_Record(oclsRU)
                    If VerifyData(RUUltraGrid) = True Then
                        'SetData("RUUltraGrid")
                        With clsRU
                            If CInt(row.Cells("PatientID").Value) = PatientID Then
                                .RUID = System.Convert.ToInt32(row.Cells("RUID").Value)
                                .PatientID = System.Convert.ToInt32(row.Cells("PatientID").Value)
                                cellROW = row.Index
                                For i As Integer = 1 To 8
                                    Dim toothColumn As String = $"RU{i}"
                                    Dim treatment As String = row.Cells(toothColumn).Value?.ToString()

                                    ' Assign treatment directly to clsLD properties
                                    CallByName(clsRU, toothColumn, CallType.Let, treatment)
                                    If String.IsNullOrWhiteSpace(treatment) OrElse treatment Is Nothing Then Continue For
                                    ' Check if this treatment already exists for this patient and tooth
                                    If Not IsNewTreatDuplicate(PatientID, toothColumn, treatment) Then

                                        ' Extract old treatment and date for this tooth
                                        Dim result = ExtractOldTrtAndDate(treatment)

                                        ' Skip invalid OldTrt entries
                                        If result.TrtDate.HasValue OrElse Not String.IsNullOrEmpty(result.OldTrt) Then
                                            cellCOL = i
                                            CellAdres = (cellCOL * 10) + cellROW
                                            UpsertPatientToothTrt(PatientID, .RUID, CellAdres, "RU", toothColumn, treatment, toothColumn, result)
                                            UpdateTrt(PatientID, True)
                                        Else
                                            ' Skip processing if no valid OldTrt found
                                            Continue For
                                        End If
                                    Else
                                        LogToFile($"RDUltraGrid {vbCrLf}IsNewTreatDuplicate {vbCrLf} The treatment '{treatment}' already exists for tooth '{toothColumn}'!")

                                        LogToListBox($"RUUltraGrid {vbCrLf}IsNewTreatDuplicate {vbCrLf} The treatment '{treatment}' already exists for tooth '{toothColumn}'!")

                                        'MsgBox($"RUUltraGrid {vbCrLf}IsNewTreatDuplicate {vbCrLf} The treatment '{treatment}' already exists for tooth '{toothColumn}'!", MsgBoxStyle.Information, "Duplicate Treatment")
                                    End If

                                Next
                            End If
                        End With
                        Dim bSuccess As Boolean
                        bSuccess = clsRUData.Update(oclsRU, clsRU)
                        If Not bSuccess Then LogToFile($"RUUltraGrid {vbCrLf}Update failed.")
                        If Not bSuccess Then LogToListBox($"RUUltraGrid {vbCrLf}Update failed.")
                        'If Not bSuccess Then MsgBox($"RUUltraGrid {vbCrLf}Update failed.", MsgBoxStyle.OkOnly, "Error")
                    End If
                Next

            Case "LUUltraGrid"
                For Each row In LUUltraGrid.Rows
                    Dim oclsLU As New LU
                    Dim clsLU As New LU
                    oclsLU.LUID = System.Convert.ToInt32(row.Cells("LUID").Value)
                    oclsLU.PatientID = System.Convert.ToInt32(row.Cells("PatientID").Value)
                    oclsLU = clsLUData.Select_Record(oclsLU)
                    If VerifyData(LUUltraGrid) = True Then
                        'SetData("LUUltraGrid")
                        With clsLU
                            If CInt(row.Cells("PatientID").Value) = PatientID Then
                                .LUID = System.Convert.ToInt32(row.Cells("LUID").Value)
                                .PatientID = System.Convert.ToInt32(row.Cells("PatientID").Value)
                                cellROW = row.Index
                                For i As Integer = 1 To 8
                                    Dim toothColumn As String = $"LU{i}"
                                    Dim treatment As String = row.Cells(toothColumn).Value?.ToString()
                                    ' Assign treatment directly to clsLD properties
                                    CallByName(clsLU, toothColumn, CallType.Let, treatment)
                                    If String.IsNullOrWhiteSpace(treatment) OrElse treatment Is Nothing Then Continue For
                                    ' Extract old treatment and date for this tooth
                                    Dim result = ExtractOldTrtAndDate(treatment)
                                    ' Check if this treatment already exists for this patient and tooth
                                    If Not IsNewTreatDuplicate(PatientID, toothColumn, GetNewTrt(result.OldTrt)) Then
                                        ' Skip invalid OldTrt entries
                                        If result.TrtDate.HasValue OrElse Not String.IsNullOrEmpty(result.OldTrt) Then
                                            cellCOL = i
                                            CellAdres = (cellCOL * 10) + cellROW
                                            UpsertPatientToothTrt(PatientID, .LUID, CellAdres, "LU", toothColumn, treatment, toothColumn, result)
                                            UpdateTrt(PatientID, True)
                                        Else
                                            ' Skip processing if no valid OldTrt found
                                            Continue For
                                        End If
                                    Else
                                        LogToFile($"LUUltraGrid {vbCrLf}IsNewTreatDuplicate {vbCrLf} The treatment '{treatment}' already exists for tooth '{toothColumn}'!")
                                        LogToListBox($"LUUltraGrid {vbCrLf}IsNewTreatDuplicate {vbCrLf} The treatment '{treatment}' already exists for tooth '{toothColumn}'!")
                                        MsgBox($"LUUltraGrid {vbCrLf}IsNewTreatDuplicate {vbCrLf} The treatment '{treatment}' already exists for tooth '{toothColumn}'!", MsgBoxStyle.Information, "Duplicate Treatment")
                                    End If
                                Next
                            End If
                        End With
                        Dim bSuccess As Boolean
                        bSuccess = clsLUData.Update(oclsLU, clsLU)
                        If Not bSuccess = True Then
                            If Not bSuccess Then LogToFile($"LUUltraGrid {vbCrLf}Update failed.")
                            If Not bSuccess Then LogToListBox($"LUUltraGrid {vbCrLf}Update failed.")
                            'MsgBox($"LUUltraGrid {vbCrLf}Update failed.", MsgBoxStyle.OkOnly, "Error")
                        End If
                    End If
                Next


            Case "RDUltraGrid"
                For Each row In RDUltraGrid.Rows
                    Dim oclsRD As New RD
                    Dim clsRD As New RD
                    oclsRD.RDID = System.Convert.ToInt32(row.Cells("RDID").Value)
                    oclsRD.PatientID = System.Convert.ToInt32(row.Cells("PatientID").Value)
                    oclsRD = clsRDData.Select_Record(oclsRD)
                    If VerifyData(RDUltraGrid) = True Then
                        'SetData("RDUltraGrid")
                        With clsRD
                            If CInt(row.Cells("PatientID").Value) = PatientID Then
                                .RDID = System.Convert.ToInt32(row.Cells("RDID").Value)
                                .PatientID = System.Convert.ToInt32(row.Cells("PatientID").Value)
                                cellROW = row.Index
                                For i As Integer = 1 To 8
                                    Dim toothColumn As String = $"RD{i}"
                                    Dim treatment As String = row.Cells(toothColumn).Value?.ToString()
                                    ' Assign treatment directly to clsLD properties
                                    CallByName(clsRD, toothColumn, CallType.Let, treatment)
                                    If String.IsNullOrWhiteSpace(treatment) OrElse treatment Is Nothing Then Continue For
                                    ' Check if this treatment already exists for this patient and tooth
                                    If Not IsNewTreatDuplicate(PatientID, toothColumn, treatment) Then
                                        ' Extract old treatment and date for this tooth
                                        Dim result = ExtractOldTrtAndDate(treatment)
                                        ' Skip invalid OldTrt entries
                                        If result.TrtDate.HasValue OrElse Not String.IsNullOrEmpty(result.OldTrt) Then
                                            cellCOL = i
                                            CellAdres = (cellCOL * 10) + cellROW
                                            UpsertPatientToothTrt(PatientID, .RDID, CellAdres, "RD", toothColumn, treatment, toothColumn, result)
                                            UpdateTrt(PatientID, True)
                                        Else
                                            ' Skip processing if no valid OldTrt found
                                            Continue For
                                        End If
                                    Else
                                        LogToFile($"RDUltraGrid {vbCrLf}IsNewTreatDuplicate {vbCrLf} The treatment '{treatment}' already exists for tooth '{toothColumn}'!")

                                        LogToListBox($"RDUltraGrid {vbCrLf}IsNewTreatDuplicate {vbCrLf} The treatment '{treatment}' already exists for tooth '{toothColumn}'!")

                                        MsgBox($"RDUltraGrid {vbCrLf}IsNewTreatDuplicate {vbCrLf} The treatment '{treatment}' already exists for tooth '{toothColumn}'!", MsgBoxStyle.Information, "Duplicate Treatment")
                                    End If
                                Next
                            End If
                        End With
                        Dim bSuccess As Boolean
                        bSuccess = clsRDData.Update(oclsRD, clsRD)
                        If Not bSuccess = True Then
                            If Not bSuccess Then LogToFile($"RDUltraGrid {vbCrLf}Update failed.")
                            If Not bSuccess Then LogToListBox($"RDUltraGrid {vbCrLf}Update failed.")
                            'MsgBox($"RDUltraGrid {vbCrLf}Update failed.", MsgBoxStyle.OkOnly, "Error")
                        End If
                    End If
                Next

            Case "LDUltraGrid"
                For Each row In LDUltraGrid.Rows
                    Dim oclsLD As New LD
                    Dim clsLD As New LD
                    oclsLD.LDID = System.Convert.ToInt32(row.Cells("LDID").Value)
                    oclsLD.PatientID = System.Convert.ToInt32(row.Cells("PatientID").Value)
                    oclsLD = clsLDData.Select_Record(oclsLD)

                    If VerifyData(LDUltraGrid) = True Then
                        With clsLD
                            If CInt(row.Cells("PatientID").Value) = PatientID Then
                                .LDID = System.Convert.ToInt32(row.Cells("LDID").Value)
                                .PatientID = System.Convert.ToInt32(row.Cells("PatientID").Value)
                                cellROW = row.Index
                                ' Iterate through LD1 to LD8
                                For i As Integer = 1 To 8
                                    Dim toothColumn As String = $"LD{i}"
                                    Dim treatment As String = row.Cells(toothColumn).Value?.ToString()
                                    ' Assign treatment directly to clsLD properties
                                    CallByName(clsLD, toothColumn, CallType.Let, treatment)
                                    If String.IsNullOrWhiteSpace(treatment) OrElse treatment Is Nothing Then Continue For
                                    ' Check if this treatment already exists for this patient and tooth
                                    If Not IsNewTreatDuplicate(PatientID, toothColumn, treatment) Then
                                        ' Extract old treatment and date for this tooth
                                        Dim result = ExtractOldTrtAndDate(treatment)

                                        ' Skip invalid OldTrt entries
                                        If result.TrtDate.HasValue OrElse Not String.IsNullOrEmpty(result.OldTrt) Then
                                            cellCOL = i
                                            CellAdres = (cellCOL * 10) + cellROW
                                            UpsertPatientToothTrt(PatientID, .LDID, CellAdres, "LD", toothColumn, treatment, toothColumn, result)
                                            UpdateTrt(PatientID, True)
                                        Else
                                            ' Skip processing if no valid OldTrt found
                                            Continue For
                                        End If
                                    Else
                                        LogToFile($"LDUltraGrid {vbCrLf}IsNewTreatDuplicate {vbCrLf} The treatment '{treatment}' already exists for tooth '{toothColumn}'!")
                                        LogToListBox($"LDUltraGrid {vbCrLf}IsNewTreatDuplicate {vbCrLf} The treatment '{treatment}' already exists for tooth '{toothColumn}'!")
                                        'MsgBox($"LDUltraGrid {vbCrLf}IsNewTreatDuplicate {vbCrLf} The treatment '{treatment}' already exists for tooth '{toothColumn}'!", MsgBoxStyle.Information, "Duplicate Treatment")
                                    End If

                                Next
                            End If
                        End With

                        Dim bSuccess As Boolean = clsLDData.Update(oclsLD, clsLD)
                        If Not bSuccess Then LogToFile($"LDUltraGrid {vbCrLf}Update failed.")
                        If Not bSuccess Then LogToListBox($"LDUltraGrid {vbCrLf}Update failed.")
                        'If Not bSuccess Then MsgBox($"LDUltraGrid {vbCrLf}Update failed.", MsgBoxStyle.OkOnly, "Error")
                    End If
                Next

        End Select
        sw.Stop()
        LogToFile("UpdateGridRecord Time spent: " & sw.Elapsed.TotalSeconds.ToString("F2") & " seconds", Me)
    End Sub


    Public Sub UpdateRecordOLd(GRID As String)
        Select Case GRID
            Case "LDUltraGrid"
                For Each row In LDUltraGrid.Rows
                    Dim oclsLD As New LD
                    Dim clsLD As New LD
                    oclsLD.LDID = System.Convert.ToInt32(row.Cells("LDID").Value)
                    oclsLD.PatientID = System.Convert.ToInt32(row.Cells("PatientID").Value)
                    oclsLD = clsLDData.Select_Record(oclsLD)
                    If VerifyData(LDUltraGrid) = True Then
                        'SetData("LDUltraGrid")
                        With clsLD
                            If CInt(row.Cells("PatientID").Value) = PatientID Then
                                .LDID = System.Convert.ToInt32(row.Cells("LDID").Value)
                                .PatientID = System.Convert.ToInt32(row.Cells("PatientID").Value)
                                .LD1 = System.Convert.ToString(row.Cells("LD1").Value)
                                .LD2 = System.Convert.ToString(row.Cells("LD2").Value)
                                .LD3 = System.Convert.ToString(row.Cells("LD3").Value)
                                .LD4 = System.Convert.ToString(row.Cells("LD4").Value)
                                .LD5 = System.Convert.ToString(row.Cells("LD5").Value)
                                .LD6 = System.Convert.ToString(row.Cells("LD6").Value)
                                .LD7 = System.Convert.ToString(row.Cells("LD7").Value)
                                .LD8 = System.Convert.ToString(row.Cells("LD8").Value)
                            End If
                        End With
                        Dim bSuccess As Boolean
                        bSuccess = clsLDData.Update(oclsLD, clsLD)
                        If bSuccess = True Then

                        Else
                            MsgBox("Update failed.", MsgBoxStyle.OkOnly, "Error")
                        End If
                    End If
                Next
            Case "LUUltraGrid"
                For Each row In LUUltraGrid.Rows
                    Dim oclsLU As New LU
                    Dim clsLU As New LU
                    oclsLU.LUID = System.Convert.ToInt32(row.Cells("LUID").Value)
                    oclsLU.PatientID = System.Convert.ToInt32(row.Cells("PatientID").Value)
                    oclsLU = clsLUData.Select_Record(oclsLU)
                    If VerifyData(LUUltraGrid) = True Then
                        'SetData("LUUltraGrid")
                        With clsLU
                            If CInt(row.Cells("PatientID").Value) = PatientID Then
                                .LUID = System.Convert.ToInt32(row.Cells("LUID").Value)
                                .PatientID = System.Convert.ToInt32(row.Cells("PatientID").Value)
                                .LU1 = System.Convert.ToString(row.Cells("LU1").Value)
                                .LU2 = System.Convert.ToString(row.Cells("LU2").Value)
                                .LU3 = System.Convert.ToString(row.Cells("LU3").Value)
                                .LU4 = System.Convert.ToString(row.Cells("LU4").Value)
                                .LU5 = System.Convert.ToString(row.Cells("LU5").Value)
                                .LU6 = System.Convert.ToString(row.Cells("LU6").Value)
                                .LU7 = System.Convert.ToString(row.Cells("LU7").Value)
                                .LU8 = System.Convert.ToString(row.Cells("LU8").Value)
                            End If
                        End With
                        Dim bSuccess As Boolean
                        bSuccess = clsLUData.Update(oclsLU, clsLU)
                        If bSuccess = True Then

                        Else
                            MsgBox("Update failed.", MsgBoxStyle.OkOnly, "Error")
                        End If
                    End If
                Next
            Case "RDUltraGrid"
                For Each row In RDUltraGrid.Rows
                    Dim oclsRD As New RD
                    Dim clsRD As New RD
                    oclsRD.RDID = System.Convert.ToInt32(row.Cells("RDID").Value)
                    oclsRD.PatientID = System.Convert.ToInt32(row.Cells("PatientID").Value)
                    oclsRD = clsRDData.Select_Record(oclsRD)
                    If VerifyData(RDUltraGrid) = True Then
                        'SetData("RDUltraGrid")
                        With clsRD
                            If CInt(row.Cells("PatientID").Value) = PatientID Then
                                .RDID = System.Convert.ToInt32(row.Cells("RDID").Value)
                                .PatientID = System.Convert.ToInt32(row.Cells("PatientID").Value)
                                .RD1 = System.Convert.ToString(row.Cells("RD1").Value)
                                .RD2 = System.Convert.ToString(row.Cells("RD2").Value)
                                .RD3 = System.Convert.ToString(row.Cells("RD3").Value)
                                .RD4 = System.Convert.ToString(row.Cells("RD4").Value)
                                .RD5 = System.Convert.ToString(row.Cells("RD5").Value)
                                .RD6 = System.Convert.ToString(row.Cells("RD6").Value)
                                .RD7 = System.Convert.ToString(row.Cells("RD7").Value)
                                .RD8 = System.Convert.ToString(row.Cells("RD8").Value)
                            End If
                        End With
                        Dim bSuccess As Boolean
                        bSuccess = clsRDData.Update(oclsRD, clsRD)
                        If bSuccess = True Then

                        Else
                            MsgBox("Update failed.", MsgBoxStyle.OkOnly, "Error")
                        End If
                    End If
                Next
            Case "RUUltraGrid"
                For Each row In RUUltraGrid.Rows
                    Dim oclsRU As New RU
                    Dim clsRU As New RU
                    oclsRU.RUID = System.Convert.ToInt32(row.Cells("RUID").Value)
                    oclsRU.PatientID = System.Convert.ToInt32(row.Cells("PatientID").Value)
                    oclsRU = clsRUData.Select_Record(oclsRU)
                    If VerifyData(RUUltraGrid) = True Then
                        'SetData("RUUltraGrid")
                        With clsRU
                            If CInt(row.Cells("PatientID").Value) = PatientID Then
                                .RUID = System.Convert.ToInt32(row.Cells("RUID").Value)
                                .PatientID = System.Convert.ToInt32(row.Cells("PatientID").Value)
                                .RU1 = System.Convert.ToString(row.Cells("RU1").Value)
                                .RU2 = System.Convert.ToString(row.Cells("RU2").Value)
                                .RU3 = System.Convert.ToString(row.Cells("RU3").Value)
                                .RU4 = System.Convert.ToString(row.Cells("RU4").Value)
                                .RU5 = System.Convert.ToString(row.Cells("RU5").Value)
                                .RU6 = System.Convert.ToString(row.Cells("RU6").Value)
                                .RU7 = System.Convert.ToString(row.Cells("RU7").Value)
                                .RU8 = System.Convert.ToString(row.Cells("RU8").Value)
                            End If
                        End With
                        Dim bSuccess As Boolean
                        bSuccess = clsRUData.Update(oclsRU, clsRU)
                        If bSuccess = True Then

                        Else
                            MsgBox("Update failed.", MsgBoxStyle.OkOnly, "Error")
                        End If
                    End If
                Next

        End Select

    End Sub



#Region "TrtCombo"

    Private Sub FillCbo()
        ''Item(7) for oldTrt
        ''Item(1) for newTrt
        'TblTRTSTableAdapter.Fill(Me.DsTRT.TblTRTS)
        'For i = 0 To DsTRT.TblTRTS.Rows.Count - 1
        '    CboStrip.Items.Add(DsTRT.TblTRTS.Rows(i).Item(7).ToString)
        'Next
    End Sub

    Private Sub FillCboByTooth(toothNum As String)
        Dim sw As New Stopwatch
        sw.Start()
        ' Define the SQL query with filtering for ToothID and including ShapeName
        Dim query As String = "SELECT [TrtID], [Trt], [Shapes].[ShapeID],  [Shapes].[ShapeName], [TrtDetails], [TrtLVL], [TrtArDetails],
                                      [ToothID], [OldTrt], [TrtGroup], [TrtColor]  
                      FROM [TblTRTS]  
                      LEFT JOIN [Shapes] ON [TblTRTS].[ShapeID] = [Shapes].[ShapeID]  
                     Where ([toothID] = 'ALL' OR @ToothID = 'ALL' 
                                              Or CHARINDEX(',' + @ToothID + ',', ',' + [ToothID] + ',') > 0 
                                              Or CHARINDEX(',' + [ToothID] + ',', ',' + @ToothID + ',') > 0)   
                      ORDER BY [TrtGroup], [Trt]"
        ' Initialize the connection to the database
        Using connection As New SqlConnection(My.Settings.DentistXConnectionString)
            ' Open the connection
            connection.Open()

            ' Execute the query using Dapper
            Dim results = connection.Query(query, New With {.ToothID = toothNum}).ToList()

            ' Populate the combo box with the results
            CboStrip.Items.Clear()
            For Each row In results
                ' Assuming that the 7th column corresponds to the TrtColor or another item you want
                CboStrip.Items.Add(row.OldTrt.ToString())
            Next
        End Using
        sw.Stop()
        LogToFile("FillCboByTooth Time spent: " & sw.Elapsed.TotalSeconds.ToString("F2") & " seconds", Me)
    End Sub

    Private Sub FillCboIMP()
        Dim sw As New Stopwatch
        sw.Start()

        ' Query the view using Dapper
        Dim query As String = "SELECT MeasureID ,BrandName ,TypeName ,Design ,DiameterMM ,LengthMM ,SIZE ,DisplayName FROM dbo.ImplantVW"


        Using connection As New SqlConnection(My.Settings.DentistXConnectionString)
            connection.Open()

            Dim results = connection.Query(query).ToList()

            ' Build a new list with formatted display strings
            Dim formattedResults = results.Select(Function(r)
                                                      Dim impType As String = r.TypeName.ToString().Trim().ToUpper()
                                                      Dim slimPart As String = r.Design.ToString().Trim().ToUpper()
                                                      Dim namePart As String = r.BrandName.ToString().Trim()

                                                      Dim displayString As String = namePart & "_" & impType
                                                      If slimPart <> "" Then
                                                          displayString &= "_" & slimPart
                                                      End If
                                                      displayString &= vbCrLf & r.SIZE.ToString().Trim()

                                                      ' Return anonymous object with formatted display
                                                      Return New With {
            .DisplayText = displayString,
            .MeasureID = r.MeasureID
        }
                                                  End Function).ToList()


            ' Populate the combo box with the results
            CboIMP.Items.Clear()
            For Each row In formattedResults
                ' Assuming that the 7th column corresponds to the TrtColor or another item you want
                CboIMP.Items.Add(row.DisplayText)
            Next

        End Using
        sw.Stop()
        LogToFile("FillCboIMP Time spent: " & sw.Elapsed.TotalSeconds.ToString("F2") & " seconds", Me)
    End Sub


    Dim existingCbo = Me.Controls.OfType(Of ComboBox)().FirstOrDefault(Function(c) c.Name = "CboImplantVariations")
    Private dynamicComboBox As ComboBox = Nothing
    Private Sub ShowImplantVariations()

        'CtxMenu.AutoClose = False ' Prevent auto close
        Dim sw As New Stopwatch
        sw.Start()


        ' Remove any existing dynamic combo
        Dim existingCbo = Me.Controls.OfType(Of ComboBox)().FirstOrDefault(Function(c) c.Name = "CboImplantVariations")
        If existingCbo IsNot Nothing Then
            Me.Controls.Remove(existingCbo)
        End If

        ' Create a new ComboBox
        dynamicComboBox = New ComboBox()
        dynamicComboBox.Name = "CboImplantVariations"
        dynamicComboBox.DropDownStyle = ComboBoxStyle.DropDown
        dynamicComboBox.AutoCompleteMode = Windows.Forms.AutoCompleteMode.SuggestAppend
        dynamicComboBox.AutoCompleteSource = AutoCompleteSource.ListItems
        dynamicComboBox.Width = 200
        ' Attach event handler
        AddHandler dynamicComboBox.SelectedIndexChanged, AddressOf DynamicComboBox_SelectedIndexChanged
        AddHandler LDUltraGrid.MouseDown, AddressOf Form_MouseDown_CloseCombo
        AddHandler LUUltraGrid.MouseDown, AddressOf Form_MouseDown_CloseCombo
        AddHandler RUUltraGrid.MouseDown, AddressOf Form_MouseDown_CloseCombo
        AddHandler RDUltraGrid.MouseDown, AddressOf Form_MouseDown_CloseCombo

        AddHandler dynamicComboBox.LostFocus, AddressOf ComboBox_LostFocus_Close

        ' After adding dynamicComboBox to parent
        dynamicComboBox.Visible = True
        dynamicComboBox.Focus()
        '' Get the position of CtxMenu on screen
        'Dim menuScreenPos As Point = CtxMenu.Bounds.Location

        '' Convert screen position to form client coordinates
        'Dim menuClientPos As Point = Me.PointToClient(menuScreenPos)

        '' Set ComboBox position to the left or right of the menu (e.g., to the right)
        'dynamicComboBox.Location = New Point(menuClientPos.X + CtxMenu.Width + 5, menuClientPos.Y)
        ' Get screen position of the ContextMenuStrip
        Dim menuScreenPos As Point = CtxMenu.Bounds.Location

        ' Convert screen position to client coordinates of the parent form
        Dim menuClientPos As Point = Me.PointToClient(menuScreenPos)

        ' Desired position: to the right of the menu
        Dim desiredX As Integer = menuClientPos.X + CtxMenu.Width + 5
        Dim desiredY As Integer = menuClientPos.Y

        ' Get parent/client bounds
        Dim parentWidth As Integer = Me.ClientSize.Width
        Dim parentHeight As Integer = Me.ClientSize.Height

        ' Ensure ComboBox stays within parent width
        If desiredX + dynamicComboBox.Width > parentWidth Then
            ' Position to the left of the menu instead
            desiredX = Math.Max(menuClientPos.X - dynamicComboBox.Width - 5, 0)
        End If

        ' Ensure ComboBox stays within parent height
        If desiredY + dynamicComboBox.Height > parentHeight Then
            desiredY = parentHeight - dynamicComboBox.Height
        End If

        ' Set the safe, adjusted location
        dynamicComboBox.Location = New Point(desiredX, desiredY)

        ' Query the view using Dapper
        Dim query As String = "SELECT * FROM Vw_ImplantCatalog"


        Using connection As New SqlConnection(My.Settings.DentistXConnectionString)
            connection.Open()

            Dim results = connection.Query(query).ToList()

            ' Build a new list with formatted display strings
            Dim formattedResults = results.Select(Function(r)
                                                      Dim impType As String = If(r.ImpType.ToString().Trim().ToUpper() = "COMPRESSIVE", "COMP", r.ImpType.ToString().Trim())
                                                      Dim slimPart As String = If(Convert.ToInt32(r.Slim) = 1, "SLIM", "")
                                                      Dim namePart As String = r.ImpName.ToString().Trim()

                                                      Dim displayString As String = namePart & "_" & impType
                                                      If slimPart <> "" Then
                                                          displayString &= "_" & slimPart
                                                      End If
                                                      displayString &= vbCrLf & r.Measure.ToString().Trim()

                                                      ' Return anonymous object with formatted display
                                                      Return New With {
            .DisplayText = displayString,
            .ImpVarID = r.ImpVarID
        }
                                                  End Function).ToList()

            dynamicComboBox.DataSource = formattedResults
            dynamicComboBox.DisplayMember = "DisplayText"
            dynamicComboBox.ValueMember = "ImpVarID"
        End Using

        ' Add ComboBox to the form
        Me.Controls.Add(dynamicComboBox)
        dynamicComboBox.BringToFront()
        sw.Stop()
        LogToFile("ShowImplantVariations Time spent: " & sw.Elapsed.TotalSeconds.ToString("F2") & " seconds", Me)
    End Sub






    'Private Sub CboStrip_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CboStrip.SelectedIndexChanged
    '    Dim selectedValue As String = CboStrip.SelectedItem.ToString()

    '    If selectedValue = "IMPLANT" Then
    '        ShowImplantVariations()
    '    End If
    'End Sub

    Private Function GetFormattedTreatmentText() As String
        Return CboStrip.Text & vbCrLf & Format(Now.Date, "dd-MM-yyyy")
    End Function

    Dim implantText As String = ""
    Private Sub DynamicComboBox_SelectedIndexChanged(sender As Object, e As EventArgs)
        Dim combo As ComboBox = CType(sender, ComboBox)
        If combo.SelectedItem IsNot Nothing Then
            Dim selectedItem = combo.SelectedItem
            ' Access properties if it's an anonymous object
            implantText = selectedItem.GetType().GetProperty("DisplayText").GetValue(selectedItem, Nothing).ToString()
            Dim impVarID As Integer = Convert.ToInt32(selectedItem.GetType().GetProperty("ImpVarID").GetValue(selectedItem, Nothing))
            implantText = implantText & vbCrLf & Format(Now.Date, "dd-MM-yyyy")
            ' Example action:
            'MessageBox.Show($"Selected: {implantText} (ID: {impVarID})", "Selection Changed")
            SetBackIMP()
        End If
        ' Write selected value to grid cell
        If clickedCell IsNot Nothing Then
            clickedCell.Value = implantText
        End If

        ' Cleanup dynamicComboBox
        If combo IsNot Nothing Then
            combo.Visible = False
            'combo.Dispose()
            'combo = Nothing
        End If

        ' Reset ContextMenu behavior
        If CtxMenu IsNot Nothing Then
            CtxMenu.AutoClose = True
        End If
    End Sub

    Private Sub Form_MouseDown_CloseCombo(sender As Object, e As MouseEventArgs)
        If dynamicComboBox IsNot Nothing AndAlso dynamicComboBox.Visible Then
            If Not dynamicComboBox.Bounds.Contains(Me.PointToClient(Cursor.Position)) Then
                RemoveDynamicComboBox()
            End If
        End If
    End Sub



    Private Sub ComboBox_LostFocus_Close(sender As Object, e As EventArgs)
        RemoveDynamicComboBox()
    End Sub


    Private Sub RemoveDynamicComboBox()
        If dynamicComboBox IsNot Nothing Then
            Me.Controls.Remove(dynamicComboBox)
            RemoveHandler LDUltraGrid.MouseDown, AddressOf Form_MouseDown_CloseCombo
            RemoveHandler LUUltraGrid.MouseDown, AddressOf Form_MouseDown_CloseCombo
            RemoveHandler RUUltraGrid.MouseDown, AddressOf Form_MouseDown_CloseCombo
            RemoveHandler RDUltraGrid.MouseDown, AddressOf Form_MouseDown_CloseCombo
            RemoveHandler dynamicComboBox.LostFocus, AddressOf ComboBox_LostFocus_Close
            dynamicComboBox.Dispose()
            dynamicComboBox = Nothing
        End If
    End Sub

    Private Sub CboIMP_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CboIMP.SelectedIndexChanged
        If CboIMP.SelectedItem IsNot Nothing Then
            Dim selectedItem = CboIMP.SelectedItem
            ' Access properties if it's an anonymous object
            'implantText = selectedItem.GetType().GetProperty("DisplayText").GetValue(selectedItem, Nothing).ToString()
            implantText = selectedItem.ToString
            'Dim impVarID As Integer = Convert.ToInt32(selectedItem.GetType().GetProperty("ImpVarID").GetValue(selectedItem, Nothing))
            implantText = "IMPLANT" & vbCrLf & implantText & vbCrLf & Format(Now.Date, "dd-MM-yyyy")
            ' Example action:
            'MessageBox.Show($"Selected: {implantText} (ID: {impVarID})", "Selection Changed")

        End If
        ' Write selected value to grid cell
        If clickedCell IsNot Nothing Then
            clickedCell.Value = implantText
            SetBackIMP()
        End If
    End Sub


    Private Sub CboStrip_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CboStrip.SelectedIndexChanged
        Try
            Dim selectedValue As String = CboStrip.SelectedItem.ToString()

            If PatientID <= 0 Then Exit Sub
            Dim tableName As String = ContextSender.Substring(0, 2)
            Select Case ContextSender
                Case "LUUltraGrid"

                    Dim toothName As String = GetGridToothColumnName("LUUltraGrid", LUUltraGrid.ActiveCell.Column.Index)
                    Dim treatment As String = GetFormattedTreatmentText()
                    'If Not IsOldTreatDuplictByGrid(LUUltraGrid, PatientID, toothName, treatment) Then 'IsOldTreatDuplicate
                    If Not IsOldTreatDuplicate(tableName, PatientID, toothName, treatment) Then
                        If selectedValue = "IMPLANT" Then
                            ShowImplantVariations()
                            LUUltraGrid.ActiveCell.Value = implantText
                        Else
                            LUUltraGrid.ActiveCell.Value = treatment
                        End If
                    Else
                        MsgBox($"The treatment '{treatment}' already exists for tooth '{toothName}'!", MsgBoxStyle.Information, "Duplicate Treatment")
                    End If

                Case "LDUltraGrid"
                    Dim toothName As String = GetGridToothColumnName("LDUltraGrid", LDUltraGrid.ActiveCell.Column.Index)
                    Dim treatment As String = GetFormattedTreatmentText()
                    'If Not IsOldTreatDuplictByGrid(LDUltraGrid, PatientID, toothName, treatment) Then
                    If Not IsOldTreatDuplicate(tableName, PatientID, toothName, treatment) Then
                        If selectedValue = "IMPLANT" Then
                            ShowImplantVariations()
                            LDUltraGrid.ActiveCell.Value = implantText
                        Else
                            LDUltraGrid.ActiveCell.Value = treatment
                        End If
                    Else
                        MsgBox($"The treatment '{treatment}' already exists for tooth '{toothName}'!", MsgBoxStyle.Information, "Duplicate Treatment")
                    End If

                Case "RUUltraGrid"
                    Dim toothName As String = GetGridToothColumnName("RUUltraGrid", RUUltraGrid.ActiveCell.Column.Index)
                    Dim treatment As String = GetFormattedTreatmentText()
                    'If Not IsOldTreatDuplictByGrid(RUUltraGrid, PatientID, toothName, treatment) Then
                    If Not IsOldTreatDuplicate(tableName, PatientID, toothName, treatment) Then
                        If selectedValue = "IMPLANT" Then
                            ShowImplantVariations()
                            RUUltraGrid.ActiveCell.Value = implantText
                        Else
                            RUUltraGrid.ActiveCell.Value = treatment
                        End If
                    Else
                        MsgBox($"The treatment '{treatment}' already exists for tooth '{toothName}'!", MsgBoxStyle.Information, "Duplicate Treatment")
                    End If

                Case "RDUltraGrid"
                    Dim toothName As String = GetGridToothColumnName("RDUltraGrid", RDUltraGrid.ActiveCell.Column.Index)
                    Dim treatment As String = GetFormattedTreatmentText()
                    'If Not IsOldTreatDuplictByGrid(RDUltraGrid, PatientID, toothName, treatment) Then
                    If Not IsOldTreatDuplicate(tableName, PatientID, toothName, treatment) Then
                        If selectedValue = "IMPLANT" Then
                            ShowImplantVariations()
                            RDUltraGrid.ActiveCell.Value = implantText
                        Else
                            RDUltraGrid.ActiveCell.Value = treatment
                        End If
                    Else
                        MsgBox($"The treatment '{treatment}' already exists for tooth '{toothName}'!", MsgBoxStyle.Information, "Duplicate Treatment")
                    End If

                Case Else
                    ' Unknown ContextSender, do nothing
                    Exit Sub
            End Select
            ' Reset ContextMenu behavior
            If CtxMenu IsNot Nothing Then
                CtxMenu.AutoClose = True
            End If
        Catch ex As SqlClient.SqlException
            MsgBox(ex.Message, MsgBoxStyle.Critical, "SQL Error")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "General Error")
        End Try
    End Sub


    Private Sub CboStrip_SelectedIndexChanged1(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Dim selectedValue As String = CboStrip.SelectedItem.ToString()

            If selectedValue = "IMPLANT" Then
                ShowImplantVariations()
            End If

            If PatientID > 0 Then
                Dim treatment As String
                treatment = Trim(CboStrip.Text) & vbCrLf & Format(Now.Date, "dd-MM-yyyy")

                Select Case ContextSender
                    Case "LUUltraGrid"
                        Dim toothName As String = GetGridToothColumnName("LUUltraGrid", Me.LUUltraGrid.ActiveCell.Column.Index)
                        If Not IsOldTreatDuplictByGrid(LUUltraGrid, PatientID, toothName, treatment) Then
                            Me.LUUltraGrid.ActiveCell.Value = treatment
                        Else
                            MsgBox($"The treatment '{treatment}' already exists for tooth '{toothName}'!", MsgBoxStyle.Information, "Duplicate Treatment")
                        End If
                    Case "LDUltraGrid"
                        Dim toothName As String = GetGridToothColumnName("LDUltraGrid", Me.LDUltraGrid.ActiveCell.Column.Index)
                        If Not IsOldTreatDuplictByGrid(LDUltraGrid, PatientID, toothName, treatment) Then
                            Me.LDUltraGrid.ActiveCell.Value = treatment
                        Else
                            MsgBox($"The treatment '{treatment}' already exists for tooth '{toothName}'!", MsgBoxStyle.Information, "Duplicate Treatment")
                        End If
                    Case "RUUltraGrid"
                        Dim toothName As String = GetGridToothColumnName("RUUltraGrid", Me.RUUltraGrid.ActiveCell.Column.Index)
                        If Not IsOldTreatDuplictByGrid(RUUltraGrid, PatientID, toothName, treatment) Then
                            Me.RUUltraGrid.ActiveCell.Value = treatment
                        Else
                            MsgBox($"The treatment '{treatment}' already exists for tooth '{toothName}'!", MsgBoxStyle.Information, "Duplicate Treatment")
                        End If
                    Case "RDUltraGrid"
                        Dim toothName As String = GetGridToothColumnName("RDUltraGrid", Me.RDUltraGrid.ActiveCell.Column.Index)
                        If Not IsOldTreatDuplictByGrid(RDUltraGrid, PatientID, toothName, treatment) Then
                            Me.RDUltraGrid.ActiveCell.Value = treatment
                        Else
                            MsgBox($"The treatment '{treatment}' already exists for tooth '{toothName}'!", MsgBoxStyle.Information, "Duplicate Treatment")
                        End If
                End Select
                'SaveTRT()
            Else
                Exit Sub
            End If
        Catch ex As System.Data.SqlClient.SqlException
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub CboStrip_DrawItem(sender As Object, e As DrawItemEventArgs)
        ' Exit if there are no items or index is invalid
        If e.Index < 0 Then Return

        Dim comboBox = DirectCast(sender, ComboBox)

        ' Determine the color based on odd or even index
        Dim backColor As Color = If(e.Index Mod 2 = 0, Color.FloralWhite, Color.LightBlue)
        Dim textColor As Color = If(e.Index Mod 2 = 0, Color.Fuchsia, Color.Blue)

        ' Draw the background
        e.Graphics.FillRectangle(New SolidBrush(backColor), e.Bounds)

        ' Draw the text
        Dim itemText As String = comboBox.Items(e.Index).ToString()
        TextRenderer.DrawText(e.Graphics, itemText, comboBox.Font, e.Bounds, textColor, TextFormatFlags.Left)

        ' Draw focus rectangle if the item is selected
        If (e.State And DrawItemState.Selected) = DrawItemState.Selected Then
            e.DrawFocusRectangle()
        End If
    End Sub


#End Region


#End Region


#Region "Patient Context"

    Private ToothID As Byte = 0
    'Dim clickedCell As UltraGridCell

    Private clsQrtr As New Qrtrs
    Private Sub UltraGrid_ClickCell(ByVal sender As Object, ByVal e As Infragistics.Win.UltraWinGrid.ClickCellEventArgs) Handles LUUltraGrid.ClickCell, LDUltraGrid.ClickCell, RUUltraGrid.ClickCell, RDUltraGrid.ClickCell
        Dim grid As UltraGrid = CType(sender, UltraGrid)

        ContextSender = grid.Name
        clickedCell = e.Cell
        ToothID = e.Cell.Column.Index - 1
        clsQrtr.QrtrAddress = ColumnRowToAddress(e.Cell.Column.Index, e.Cell.Row.Index)
        clsQrtr.QrtrTable = ContextSender.Substring(0, 2)
        clsQrtr.QrtrID = CInt(e.Cell.Row.Cells(grid.Name.Substring(0, 2) & "ID").Value)
        clsQrtr.QrtrColumnName = $"{clsQrtr.QrtrTable}{ToothID}"

        ' Get the actual class instance with current data
        Dim quarterInstance As Object = GetQuarterInstanceWithData(clsQrtr)

        ' Store the quarter instance for later use
        CurrentQuarterInstance = quarterInstance

        Me.CboStrip.Items.Clear()
        Me.CboStrip.AutoSize = True
        ' Set the DrawMode to OwnerDrawFixed
        CboStrip.ComboBox.DrawMode = Windows.Forms.DrawMode.OwnerDrawFixed
        ' Handle the DrawItem event
        AddHandler CboStrip.ComboBox.DrawItem, AddressOf CboStrip_DrawItem
        '
        FillCboByTooth(ToothID)
        Dim imgNamePrefix As String = ContextSender.Substring(0, 2)

        If Not clickedCell.Appearance.ImageBackground Is Nothing Then
            ImgName = imgNamePrefix & clickedCell.Column.Index.ToString()
        End If
    End Sub

    Private Sub UltraGrid_AfterCellUpdate(sender As Object, e As Infragistics.Win.UltraWinGrid.CellEventArgs) Handles _
    LUUltraGrid.AfterCellUpdate, LDUltraGrid.AfterCellUpdate, RUUltraGrid.AfterCellUpdate, RDUltraGrid.AfterCellUpdate

        Dim grid As UltraGrid = CType(sender, UltraGrid)
        Dim tablePrefix As String = grid.Name.Substring(0, 2)

        '' Check if this is one of the treatment columns (RU1-RU8, etc.)
        'If e.Cell.Column.Index >= 2 AndAlso e.Cell.Column.Index <= 9 Then
        '    Dim newValue As String = If(e.Cell.Value IsNot Nothing, e.Cell.Value.ToString(), "")

        '    ' Update the Patient_ToothTrt table
        '    UpdatePatientToothTrt(clsQrtr, newValue, clsQrtr.QrtrColumnValue)

        '    ' Update the clsQrtr with the new value
        '    clsQrtr.QrtrColumnValue = newValue
        'End If
    End Sub

#Region "Update cell"

    Private CurrentQuarterInstance As Object


    Private Function GetQuarterInstanceWithData(clsQrtr As Qrtrs) As Object
        Select Case clsQrtr.QrtrTable.ToUpper()
            Case "RU"
                Dim ruInstance As New RU()
                ' Get the current data from the binding source
                Dim bs As BindingSource = GetBindingSourceForTable(clsQrtr.QrtrTable)
                If bs IsNot Nothing AndAlso bs.Current IsNot Nothing Then
                    ruInstance = CType(bs.Current, RU)
                End If
                Return ruInstance

            Case "LU"
                Dim luInstance As New LU()
                Dim bs As BindingSource = GetBindingSourceForTable(clsQrtr.QrtrTable)
                If bs IsNot Nothing AndAlso bs.Current IsNot Nothing Then
                    luInstance = CType(bs.Current, LU)
                End If
                Return luInstance

            Case "RD"
                Dim rdInstance As New RD()
                Dim bs As BindingSource = GetBindingSourceForTable(clsQrtr.QrtrTable)
                If bs IsNot Nothing AndAlso bs.Current IsNot Nothing Then
                    rdInstance = CType(bs.Current, RD)
                End If
                Return rdInstance

            Case "LD"
                Dim ldInstance As New LD()
                Dim bs As BindingSource = GetBindingSourceForTable(clsQrtr.QrtrTable)
                If bs IsNot Nothing AndAlso bs.Current IsNot Nothing Then
                    ldInstance = CType(bs.Current, LD)
                End If
                Return ldInstance

            Case Else
                Throw New ArgumentException($"Invalid quarter table: {clsQrtr.QrtrTable}")
        End Select
    End Function

    Private Function GetBindingSourceForTable(tableName As String) As BindingSource
        Select Case tableName.ToUpper()
            Case "RU" : Return RUBindingSource ' Replace with your actual binding source names
            Case "LU" : Return LUBindingSource
            Case "RD" : Return RDBindingSource
            Case "LD" : Return LDBindingSource
            Case Else : Return Nothing
        End Select
    End Function

    Private Sub UpdatePatientToothTrt(clsQrtr As Qrtrs, newValue As String, oldValue As String)
        Try
            Dim connection As New SqlConnection
            connection = DentistXDATA.GetConnection()
            Using connection 'As New SqlConnection(YourConnectionString)
                connection.Open()

                ' Check if a record already exists for this quarter location
                Dim checkSql As String = "SELECT COUNT(*) FROM Patient_ToothTrt 
                                    WHERE PatientID = @PatientID 
                                    AND QrtrID = @QrtrID 
                                    AND QrtrTable = @QrtrTable 
                                    AND QrtrColumnName = @QrtrColumnName"

                Using checkCmd As New SqlCommand(checkSql, connection)
                    checkCmd.Parameters.AddWithValue("@PatientID", PatientID)
                    checkCmd.Parameters.AddWithValue("@QrtrID", clsQrtr.QrtrID)
                    checkCmd.Parameters.AddWithValue("@QrtrTable", clsQrtr.QrtrTable)
                    checkCmd.Parameters.AddWithValue("@QrtrColumnName", clsQrtr.QrtrColumnName)

                    Dim exists As Integer = CInt(checkCmd.ExecuteScalar())

                    If exists > 0 Then
                        ' Update existing record
                        Dim updateSql As String = "UPDATE Patient_ToothTrt 
                                            SET Treat = @Treat, 
                                                QrtrColumnValue = @QrtrColumnValue,
                                                TreatDate = GETDATE()
                                            WHERE PatientID = @PatientID 
                                            AND QrtrID = @QrtrID 
                                            AND QrtrTable = @QrtrTable 
                                            AND QrtrColumnName = @QrtrColumnName"

                        Using updateCmd As New SqlCommand(updateSql, connection)
                            updateCmd.Parameters.AddWithValue("@Treat", newValue)
                            updateCmd.Parameters.AddWithValue("@QrtrColumnValue", newValue)
                            updateCmd.Parameters.AddWithValue("@PatientID", PatientID)
                            updateCmd.Parameters.AddWithValue("@QrtrID", clsQrtr.QrtrID)
                            updateCmd.Parameters.AddWithValue("@QrtrTable", clsQrtr.QrtrTable)
                            updateCmd.Parameters.AddWithValue("@QrtrColumnName", clsQrtr.QrtrColumnName)

                            updateCmd.ExecuteNonQuery()
                        End Using
                    Else
                        ' Insert new record
                        Dim insertSql As String = "INSERT INTO Patient_ToothTrt 
                                            (PatientID, ToothNum, Treat, TreatDate, 
                                             QrtrID, QrtrTable, QrtrColumnName, QrtrColumnValue, QrtrAddress)
                                            VALUES 
                                            (@PatientID, @ToothNum, @Treat, GETDATE(),
                                             @QrtrID, @QrtrTable, @QrtrColumnName, @QrtrColumnValue, @QrtrAddress)"

                        Using insertCmd As New SqlCommand(insertSql, connection)
                            insertCmd.Parameters.AddWithValue("@PatientID", PatientID)
                            insertCmd.Parameters.AddWithValue("@ToothNum", GetToothNumberFromAddress(clsQrtr.QrtrAddress))
                            insertCmd.Parameters.AddWithValue("@Treat", newValue)
                            insertCmd.Parameters.AddWithValue("@QrtrID", clsQrtr.QrtrID)
                            insertCmd.Parameters.AddWithValue("@QrtrTable", clsQrtr.QrtrTable)
                            insertCmd.Parameters.AddWithValue("@QrtrColumnName", clsQrtr.QrtrColumnName)
                            insertCmd.Parameters.AddWithValue("@QrtrColumnValue", newValue)
                            insertCmd.Parameters.AddWithValue("@QrtrAddress", clsQrtr.QrtrAddress)

                            insertCmd.ExecuteNonQuery()
                        End Using
                    End If
                End Using
            End Using

            MessageBox.Show("Treatment updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

        Catch ex As Exception
            MessageBox.Show($"Error updating treatment: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Function GetToothNumberFromAddress(qrtrAddress As Integer) As Integer
        ' Implement your logic to convert quarter address to tooth number
        ' This depends on how your ColumnRowToAddress function works
        ' Example implementation:
        Return (qrtrAddress \ 8) + 1 ' Adjust based on your addressing scheme
    End Function

#End Region

    Private Sub UpdateCellBackgroundImage(ByVal grid As UltraGrid, ByVal imgResource As Image)
        grid.ActiveCell.Appearance.ImageBackground = imgResource
    End Sub




    Private Sub UpdateDatabase(img As Byte(), clickedCell As UltraGridCell, patientID As Integer, style As String)
        Dim celadres As Integer
        celadres = ColumnRowToAddress(clickedCell.Column.Index, clickedCell.Row.Index)
        Dim table As String = String.Empty
        Dim toothColumn As String = String.Empty
        Dim toothColumnNum As Integer = clickedCell.Column.Index - 1
        Select Case style
            Case "LU"
                table = "LUSTYLE"
                toothColumn = $"LU{toothColumnNum}"
            Case "LD"
                table = "LDSTYLE"
                toothColumn = $"LD{toothColumnNum}"
            Case "RU"
                table = "RUSTYLE"
                toothColumn = $"RU{toothColumnNum}"
            Case "RD"
                table = "RDSTYLE"
                toothColumn = $"RD{toothColumnNum}"
        End Select

        Try
            Dim Styles As New StyleImage
            Dim ID As Integer? = Styles.IsStyl(patientID, celadres, style)
            If ID.HasValue Then
                Styles.UpdateStyle(table, style, ID.Value, patientID, celadres, img)
                Dim treatment As String = implantText & vbCrLf & Format(Now.Date, "dd-MM-yyyy")
                clsQrtr.QrtrColumnValue = treatment
                AddTreatFrmGridsToChart(patientID, toothColumn, implantText, clsQrtr) ' "IMPLANT")
                clickedCell.Value = treatment
                UpdateRecord(style & "UltraGrid")
            Else
                Styles.InsertStyle(table, style, patientID, celadres, img)
                clsQrtr.QrtrColumnValue = implantText
                AddTreatFrmGridsToChart(patientID, toothColumn, implantText, clsQrtr) ' "IMPLANT")
                'Dim treatment As String = implantText & vbCrLf & Format(Now.Date, "dd-MM-yyyy")
                clickedCell.Value = implantText
                UpdateRecord(style & "UltraGrid")
            End If
            UpdateImplant(patientID, True)

            clsPatient.PatientID = patientID
            Select Case style
                Case "LU"
                    'tableAdapter.FillByID(DsTRT.LUSTYLE, patientID)
                    LuStl = clsPatientData.GetLUSTYLE(clsPatient)
                Case "LD"
                    'tableAdapter.FillByID(DsTRT.LDSTYLE, patientID)
                    LdStl = clsPatientData.GetLDSTYLE(clsPatient)
                Case "RU"
                    'tableAdapter.FillByID(DsTRT.RUSTYLE, patientID)
                    RuStl = clsPatientData.GetRUSTYLE(clsPatient)
                Case "RD"
                    'tableAdapter.FillByID(DsTRT.RDSTYLE, patientID)
                    RdStl = clsPatientData.GetRDSTYLE(clsPatient)
            End Select
        Catch ex As System.Data.SqlClient.SqlException
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub SetBackIMP()
        If clickedCell Is Nothing OrElse PatientID <= 0 Then Exit Sub

        Dim img As Byte() = Nothing
        Dim imgResource As Image = Nothing

        Dim tableAdapter As Object = Nothing
        Dim style As String = ContextSender.Substring(0, 2)
        clsPatient.PatientID = PatientID
        Select Case style
            Case "LU"
                img = ModuleImages.imgToByteConverter(GetIMP("LU"))
                imgResource = GetIMP("LU")
                'tableAdapter = LUSTYLETableAdapter

                LuStl = clsPatientData.GetLUSTYLE(clsPatient)
                ' Ensure LDSTYLEBindingSource is initialized
                If LUSTYLEBindingSource Is Nothing Then
                    LUSTYLEBindingSource = New BindingSource()
                End If
                ' Set the DataSource
                LUSTYLEBindingSource.DataSource = LuStl.ToList()
            Case "LD"
                img = ModuleImages.imgToByteConverter(GetIMP("LD"))
                imgResource = GetIMP("LD")
                'tableAdapter = LDSTYLETableAdapter
                LdStl = clsPatientData.GetLDSTYLE(clsPatient)
                ' Ensure LDSTYLEBindingSource is initialized
                If LDSTYLEBindingSource Is Nothing Then
                    LDSTYLEBindingSource = New BindingSource()
                End If
                ' Set the DataSource
                LDSTYLEBindingSource.DataSource = LdStl.ToList()
            Case "RU"
                img = ModuleImages.imgToByteConverter(GetIMP("RU"))
                imgResource = GetIMP("RU")
                'tableAdapter = RUSTYLETableAdapter
                RuStl = clsPatientData.GetRUSTYLE(clsPatient)
                ' Ensure LDSTYLEBindingSource is initialized
                If RUSTYLEBindingSource Is Nothing Then
                    RUSTYLEBindingSource = New BindingSource()
                End If
                ' Set the DataSource
                RUSTYLEBindingSource.DataSource = RuStl.ToList()
            Case "RD"
                img = ModuleImages.imgToByteConverter(GetIMP("RD"))
                imgResource = GetIMP("RU")
                ''tableAdapter = RDSTYLETableAdapter
                RdStl = clsPatientData.GetRDSTYLE(clsPatient)
                ' Ensure LDSTYLEBindingSource is initialized
                If RDSTYLEBindingSource Is Nothing Then
                    RDSTYLEBindingSource = New BindingSource()
                End If
                ' Set the DataSource
                RDSTYLEBindingSource.DataSource = RdStl.ToList()
        End Select


        UpdateDatabase(img, clickedCell, PatientID, style)
        clickedCell.Appearance.ImageBackground = imgResource
    End Sub


    Private Sub BackIMPContext_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BackIMPContext.Click
        If clickedCell Is Nothing OrElse PatientID <= 0 Then Exit Sub

        Dim img As Byte() = Nothing
        Dim imgResource As Image = Nothing

        Dim tableAdapter As Object = Nothing
        Dim style As String = ContextSender.Substring(0, 2)
        clsPatient.PatientID = PatientID
        Select Case style
            Case "LU"
                img = ModuleImages.imgToByteConverter(GetIMP("LU"))
                imgResource = GetIMP("LU")
                'tableAdapter = LUSTYLETableAdapter

                LuStl = clsPatientData.GetLUSTYLE(clsPatient)
                ' Ensure LDSTYLEBindingSource is initialized
                If LUSTYLEBindingSource Is Nothing Then
                    LUSTYLEBindingSource = New BindingSource()
                End If
                ' Set the DataSource
                LUSTYLEBindingSource.DataSource = LuStl.ToList()
            Case "LD"
                img = ModuleImages.imgToByteConverter(GetIMP("LD"))
                imgResource = GetIMP("LD")
                'tableAdapter = LDSTYLETableAdapter
                LdStl = clsPatientData.GetLDSTYLE(clsPatient)
                ' Ensure LDSTYLEBindingSource is initialized
                If LDSTYLEBindingSource Is Nothing Then
                    LDSTYLEBindingSource = New BindingSource()
                End If
                ' Set the DataSource
                LDSTYLEBindingSource.DataSource = LdStl.ToList()
            Case "RU"
                img = ModuleImages.imgToByteConverter(GetIMP("RU"))
                imgResource = GetIMP("RU")
                'tableAdapter = RUSTYLETableAdapter
                RuStl = clsPatientData.GetRUSTYLE(clsPatient)
                ' Ensure LDSTYLEBindingSource is initialized
                If RUSTYLEBindingSource Is Nothing Then
                    RUSTYLEBindingSource = New BindingSource()
                End If
                ' Set the DataSource
                RUSTYLEBindingSource.DataSource = RuStl.ToList()
            Case "RD"
                img = ModuleImages.imgToByteConverter(GetIMP("RD"))
                imgResource = GetIMP("RD")
                ''tableAdapter = RDSTYLETableAdapter
                RdStl = clsPatientData.GetRDSTYLE(clsPatient)
                ' Ensure LDSTYLEBindingSource is initialized
                If RDSTYLEBindingSource Is Nothing Then
                    RDSTYLEBindingSource = New BindingSource()
                End If
                ' Set the DataSource
                RDSTYLEBindingSource.DataSource = RdStl.ToList()
        End Select


        UpdateDatabase(img, clickedCell, PatientID, style)
        clickedCell.Appearance.ImageBackground = imgResource
    End Sub

    Private Sub BackImageToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BackImageContext.Click
        If clickedCell Is Nothing OrElse PatientID <= 0 Then Exit Sub

        Dim img As Byte() = Nothing
        Dim imgResource As Image = Nothing
        Dim celadres As Integer
        'Dim tableAdapter As Object = Nothing
        Dim style As String = ContextSender.Substring(0, 2)

        Select Case style
            Case "LU"
                img = ModuleImages.imgToByteConverter(GetIMG(ColumnRowToAddress(clickedCell.Column.Index, clickedCell.Row.Index), "LU"))
                imgResource = byteArrayToImage(img)
                'tableAdapter = LUSTYLETableAdapter
            Case "LD"
                img = ModuleImages.imgToByteConverter(GetIMG(ColumnRowToAddress(clickedCell.Column.Index, clickedCell.Row.Index), "LD"))
                imgResource = byteArrayToImage(img)
                'tableAdapter = LDSTYLETableAdapter
            Case "RU"
                img = ModuleImages.imgToByteConverter(GetIMG(ColumnRowToAddress(clickedCell.Column.Index, clickedCell.Row.Index), "RU"))
                imgResource = byteArrayToImage(img)
                'tableAdapter = RUSTYLETableAdapter
            Case "RD"
                img = ModuleImages.imgToByteConverter(GetIMG(ColumnRowToAddress(clickedCell.Column.Index, clickedCell.Row.Index), "RD"))
                imgResource = byteArrayToImage(img)
                'tableAdapter = RDSTYLETableAdapter
        End Select

        celadres = ColumnRowToAddress(clickedCell.Column.Index, clickedCell.Row.Index)
        UpdateDatabase(img, clickedCell, PatientID, style)
        clickedCell.Appearance.ImageBackground = imgResource
    End Sub


    Private Sub DelBackImageToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DelBackImageContext.Click
        If clickedCell IsNot Nothing Then
            If clickedCell.Appearance.ImageBackground Is Nothing Then Exit Sub
            DelBackImg(ContextSender, clickedCell, PatientID)
        End If

    End Sub

    Private Sub DelTreatToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DelTreatContext.Click

        If clickedCell.Value Is Nothing Then Exit Sub
        DelTreat(ContextSender, clickedCell, PatientID)
    End Sub

    Private Sub DelTreat(ByVal grid As String, ByVal cell As UltraGridCell, id As Integer)
        Dim clickedToothNum As Integer

        If id < 0 Then Exit Sub
        grid = ContextSender.Substring(0, 2)
        Dim toothColumn As String = String.Empty
        Select Case grid
            Case "LU"
                toothColumn = $"LU{clickedCell.Column.Index - 1}"
                clickedToothNum = 20 + clickedCell.Column.Index - 1
            Case "LD"
                toothColumn = $"LD{clickedCell.Column.Index - 1}"
                clickedToothNum = 30 + clickedCell.Column.Index - 1
            Case "RU"
                toothColumn = $"RU{clickedCell.Column.Index - 1}"
                clickedToothNum = 10 + clickedCell.Column.Index - 1
            Case "RD"
                toothColumn = $"RD{clickedCell.Column.Index - 1}"
                clickedToothNum = 40 + clickedCell.Column.Index - 1
        End Select
        Dim treatment As String = clickedCell.Value.ToString
        Dim result = ExtractOldTrtAndDate(treatment)

        'DelGridTrt(grid, PatientID, toothColumn, treatment)
        DelTreatFromQuadrantTables(PatientID, clickedToothNum, treatment)
        DelFromPatient_ToothTrt(PatientID, clickedToothNum, GetNewTrt(result.OldTrt))
        clickedCell.Value = Nothing
        'UpdateGridRecord(ContextSender)
    End Sub

    Private Sub DelBackImg(ByVal grid As String, ByVal cell As UltraGridCell, id As Integer)
        Try
            Dim colindex, rowindex, slctdCelAdres As Integer

            If id < 0 Then Exit Sub

            Dim toothColumn As String = String.Empty
            Select Case grid
                Case "LU"
                    toothColumn = $"LU{clickedCell.Column.Index}"
                Case "LD"
                    toothColumn = $"LD{clickedCell.Column.Index}"
                Case "RU"
                    toothColumn = $"RU{clickedCell.Column.Index}"
                Case "RD"
                    toothColumn = $"RD{clickedCell.Column.Index}"
            End Select
            'Dim treatment As String = clickedCell.Value.ToString
            'Dim result = ExtractOldTrtAndDate(treatment)
            ' Determine the grid and associated bindings and controls
            Select Case grid
                Case "LUUltraGrid"
                    cell = clickedCell
                    colindex = cell.Column.Index
                    rowindex = cell.Row.Index
                    slctdCelAdres = ColumnRowToAddress(colindex, rowindex)
                    clickedCell.Value = ""
                    If Me.LUSTYLEBindingSource.Count <> 0 Then
                        'For Each row As DsTRT.LUSTYLERow In Me.DsTRT.LUSTYLE
                        For Each lustlItem As LUSTYLE In LuStl
                            If lustlItem.CellAddres = slctdCelAdres AndAlso lustlItem.PatientID = id Then
                                HandleImageRemovalAndUpdate("LUSTYLE", lustlItem.LUcellID, lustlItem.PatientID, slctdCelAdres, Me.LUUltraGrid, AddressOf LUStyle1)
                                Exit For
                            End If
                        Next
                        '    If row.CellAddres = slctdCelAdres AndAlso row.PatientID = id Then
                        '        HandleImageRemovalAndUpdate("LUSTYLE", row.LUcellID, row.PatientID, slctdCelAdres, Me.LUUltraGrid, Me.LUSTYLETableAdapter, AddressOf LUStyle1)
                        '        Exit For
                        '    End If
                        'Next
                    End If
                    UpdateRecord(grid)
                Case "LDUltraGrid"
                    cell = clickedCell
                    colindex = cell.Column.Index
                    rowindex = cell.Row.Index
                    clickedCell.Value = ""
                    If Me.LDSTYLEBindingSource.Count <> 0 Then
                        slctdCelAdres = ColumnRowToAddress(colindex, rowindex)
                        For Each ldstlItem As LDSTYLE In LdStl
                            If ldstlItem.CellAddres = slctdCelAdres AndAlso ldstlItem.PatientID = id Then
                                HandleImageRemovalAndUpdate("LDSTYLE", ldstlItem.LDcellID, ldstlItem.PatientID, slctdCelAdres, Me.LDUltraGrid, AddressOf LDStyle1)
                                Exit For
                            End If
                        Next
                        'For Each row As DsTRT.LDSTYLERow In Me.DsTRT.LDSTYLE
                        '    If row.CellAddres = slctdCelAdres AndAlso row.PatientID = id Then
                        '        HandleImageRemovalAndUpdate("LDSTYLE", row.LDcellID, row.PatientID, slctdCelAdres, Me.LDUltraGrid, Me.LDSTYLETableAdapter, AddressOf LDStyle1)
                        '        Exit For
                        '    End If
                        'Next
                    End If
                    UpdateRecord(grid)
                Case "RUUltraGrid"
                    cell = clickedCell
                    colindex = cell.Column.Index
                    rowindex = cell.Row.Index
                    clickedCell.Value = ""
                    If Me.RUSTYLEBindingSource.Count <> 0 Then
                        slctdCelAdres = ColumnRowToAddress(colindex, rowindex)
                        For Each rustlItem As RUSTYLE In RuStl
                            If rustlItem.CellAddres = slctdCelAdres AndAlso rustlItem.PatientID = id Then
                                HandleImageRemovalAndUpdate("RUSTYLE", rustlItem.RUcellID, rustlItem.PatientID, slctdCelAdres, Me.RUUltraGrid, AddressOf RUStyle1)
                                Exit For
                            End If
                        Next
                        'For Each row As DsTRT.RUSTYLERow In Me.DsTRT.RUSTYLE
                        '    If row.CellAddres = slctdCelAdres AndAlso row.PatientID = id Then
                        '        HandleImageRemovalAndUpdate("RUSTYLE", row.RUcellID, row.PatientID, slctdCelAdres, Me.RUUltraGrid, Me.RUSTYLETableAdapter, AddressOf RUStyle1)
                        '        Exit For
                        '    End If
                        'Next
                    End If
                    UpdateRecord(grid)
                Case "RDUltraGrid"
                    cell = clickedCell
                    colindex = cell.Column.Index
                    rowindex = cell.Row.Index
                    clickedCell.Value = ""
                    If Me.RDSTYLEBindingSource.Count <> 0 Then
                        slctdCelAdres = ColumnRowToAddress(colindex, rowindex)
                        For Each rdstlItem As RDSTYLE In RdStl
                            If rdstlItem.CellAddres = slctdCelAdres AndAlso rdstlItem.PatientID = id Then
                                HandleImageRemovalAndUpdate("RDSTYLE", rdstlItem.RDcellID, rdstlItem.PatientID, slctdCelAdres, Me.RDUltraGrid, AddressOf RDStyle1)
                                Exit For
                            End If
                        Next
                        'For Each row As DsTRT.RDSTYLERow In Me.DsTRT.RDSTYLE
                        '    If row.CellAddres = slctdCelAdres AndAlso row.PatientID = id Then
                        '        HandleImageRemovalAndUpdate("RDSTYLE", row.RDcellID, row.PatientID, slctdCelAdres, Me.RDUltraGrid, Me.RDSTYLETableAdapter, AddressOf RDStyle1)
                        '        Exit For
                        '    End If
                        'Next
                    End If
                    UpdateRecord(grid)
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub HandleImageRemovalAndUpdate(ByVal style As String, ByVal cellID As Integer, ByVal patientID As Integer, ByVal cellAddress As Integer, ByVal grid As UltraGrid, ByVal styleUpdateAction As Action)

        Try
            Dim row, col As Integer
            row = AddressToColumnRow(cellAddress).Row
            col = AddressToColumnRow(cellAddress).Column
            grid.Rows(row).Cells(col).Appearance.ImageBackground = Nothing

            ' Perform deletion and update
            If DeleteStyleRecord(style, cellID, patientID, cellAddress) = 1 Then
                grid.Rows(row).Cells(col).Appearance.ImageBackground = Nothing
                'adapter.FillByID(Me.DsTRT.Tables(style.ToUpper()), patientID)
                clsPatient.PatientID = patientID
                Select Case style.ToUpper()
                    Case "LUSTYLE"
                        LuStl = clsPatientData.GetLUSTYLE(clsPatient)
                        ' Ensure LDSTYLEBindingSource is initialized
                        If LUSTYLEBindingSource Is Nothing Then
                            LUSTYLEBindingSource = New BindingSource()
                        End If
                        ' Set the DataSource
                        LUSTYLEBindingSource.DataSource = LuStl.ToList()
                    Case "LDSTYLE"
                        LdStl = clsPatientData.GetLDSTYLE(clsPatient)
                        ' Ensure LDSTYLEBindingSource is initialized
                        If LDSTYLEBindingSource Is Nothing Then
                            LDSTYLEBindingSource = New BindingSource()
                        End If
                        ' Set the DataSource
                        LDSTYLEBindingSource.DataSource = LdStl.ToList()
                    Case "RUSTYLE"
                        RuStl = clsPatientData.GetRUSTYLE(clsPatient)
                        ' Ensure LDSTYLEBindingSource is initialized
                        If RUSTYLEBindingSource Is Nothing Then
                            RUSTYLEBindingSource = New BindingSource()
                        End If
                        ' Set the DataSource
                        RUSTYLEBindingSource.DataSource = RuStl.ToList()
                    Case "RDSTYLE"
                        RdStl = clsPatientData.GetRDSTYLE(clsPatient)
                        ' Ensure LDSTYLEBindingSource is initialized
                        If RDSTYLEBindingSource Is Nothing Then
                            RDSTYLEBindingSource = New BindingSource()
                        End If
                        ' Set the DataSource
                        RDSTYLEBindingSource.DataSource = RdStl.ToList()

                End Select
                styleUpdateAction.Invoke()
            Else
                MsgBox("Could not remove image")
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub



    Private Function DeleteStyleRecord(ByVal style As String, ByVal cellID As Integer, ByVal patientID As Integer, ByVal cellAddress As Integer) As Integer
        Try
            Dim conn As New SqlConnection(My.Settings.DentistXConnectionString)
            ' Determine primary key based on table name
            Dim primaryKey As String = ""
            Select Case style.ToUpper()
                Case "LUSTYLE"
                    primaryKey = "LUcellID"
                Case "LDSTYLE"
                    primaryKey = "LDcellID"
                Case "RUSTYLE"
                    primaryKey = "RUcellID"
                Case "RDSTYLE"
                    primaryKey = "RDcellID"
                Case Else
                    Return -1 ' Handle unsupported style
            End Select

            ' Construct the SQL DELETE statement dynamically
            Dim sql As String = "DELETE FROM [" & style & "] WHERE [" & primaryKey & "] = @PrimaryKey AND [PatientID] = @PatientID AND [CellAddres] = @CellAdres"

            ' Execute query with Dapper and return affected rows
            Return conn.Execute(sql, New With {
            .PrimaryKey = cellID,
            .PatientID = patientID,
            .CellAdres = cellAddress
        })

        Catch ex As Exception
            MsgBox(ex.Message)
            Return -1
        End Try
    End Function




    Private Sub ForeColorContext_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ForeColorContext.Click
        Try
            If PatientID <= 0 Then Exit Sub ' Ensure PatientID is valid

            Dim cell As UltraGridCell = Nothing
            Dim cellCOL, cellROW, CellAdres As Integer
            Dim FrClr As String
            Dim ClrDilg As New ColorDialog

            If ClrDilg.ShowDialog() = DialogResult.OK Then
                Dim selectedGrid As UltraGrid = Nothing

                Select Case ContextSender
                    Case "LUUltraGrid"
                        selectedGrid = Me.LUUltraGrid
                        cell = Me.LUUltraGrid.ActiveCell
                    Case "LDUltraGrid"
                        selectedGrid = Me.LDUltraGrid
                        cell = Me.LDUltraGrid.ActiveCell
                    Case "RUUltraGrid"
                        selectedGrid = Me.RUUltraGrid
                        cell = Me.RUUltraGrid.ActiveCell
                    Case "RDUltraGrid"
                        selectedGrid = Me.RDUltraGrid
                        cell = Me.RDUltraGrid.ActiveCell
                End Select

                If selectedGrid IsNot Nothing AndAlso cell IsNot Nothing Then
                    cellCOL = cell.Column.Index
                    cellROW = cell.Row.Index
                    CellAdres = ColumnRowToAddress(cellCOL, cellROW)

                    cell.Appearance.ForeColor = ClrDilg.Color
                    FrClr = ColorTranslator.ToHtml(ClrDilg.Color)

                    Try
                        Select Case ContextSender
                            Case "LUUltraGrid"
                                HandleColorUpdate("LUPL", PatientID, CellAdres, FrClr, AddressOf LUPL)
                            Case "LDUltraGrid"
                                HandleColorUpdate("LDPL", PatientID, CellAdres, FrClr, AddressOf LDPL)
                            Case "RUUltraGrid"
                                HandleColorUpdate("RUPL", PatientID, CellAdres, FrClr, AddressOf RUPL)
                            Case "RDUltraGrid"
                                HandleColorUpdate("RDPL", PatientID, CellAdres, FrClr, AddressOf RDPL)
                        End Select
                    Catch ex As System.Data.SqlClient.SqlException
                        MsgBox(ex.Message)
                    End Try
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub


    Private Sub HandleColorUpdate(ByVal style As String, ByVal patientID As Integer, ByVal cellAddress As Integer, ByVal colorName As String, ByVal styleUpdateAction As Action)
        Try
            Dim cellID As Integer = GetCellIDForColor(style, patientID, cellAddress)

            Using conn As New SqlConnection(My.Settings.DentistXConnectionString)
                conn.Open()

                If cellID > 0 Then
                    UpdateColorStyle(style, cellID, patientID, cellAddress, colorName)
                Else
                    InsertColorStyle(style, patientID, cellAddress, colorName)
                End If
            End Using

            RefreshColorBindingSource(style, patientID)
            styleUpdateAction.Invoke()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub UpdateColorStyle(ByVal style As String, ByVal cellID As Integer, ByVal patientID As Integer, ByVal cellAddress As Integer, ByVal colorName As String)
        Dim conn As New SqlConnection(My.Settings.DentistXConnectionString)
        ' Define primary key based on table name
        Dim primaryKey As String = ""
        Select Case style.ToUpper()
            Case "LUPL"
                primaryKey = "LUcellID"
            Case "LDPL"
                primaryKey = "LDcellID"
            Case "RUPL"
                primaryKey = "RUcellID"
            Case "RDPL"
                primaryKey = "RDcellID"
            Case Else
                Throw New ArgumentException("Invalid style name")
        End Select

        ' Construct the SQL statement dynamically
        Dim sql As String = "UPDATE [" & style & "] SET [CellAddres] = @CellAddres, [ForeColor] = @ForeColor WHERE [" & primaryKey & "] = @PrimaryKey AND [PatientID] = @PatientID"

        ' Execute the query using Dapper
        conn.Execute(sql, New With {
        .PrimaryKey = cellID,
        .PatientID = patientID,
        .CellAddres = cellAddress,
        .ForeColor = colorName
    })
    End Sub

    Private Sub InsertColorStyle(ByVal style As String, ByVal patientID As Integer, ByVal cellAddress As Integer, ByVal colorName As String)
        Dim conn As New SqlConnection(My.Settings.DentistXConnectionString)
        Dim sql As String = "INSERT INTO [" & style & "] (PatientID, CellAddres, ForeColor) VALUES (@PatientID, @CellAddres, @ForeColor)"

        conn.Execute(sql, New With {
        .PatientID = patientID,
        .CellAddres = cellAddress,
        .ForeColor = colorName
    })
    End Sub

    Private Sub RefreshColorBindingSource(ByVal style As String, ByVal patientID As Integer)
        clsPatient.PatientID = patientID
        Dim dataSource As Object = Nothing
        Dim bindingSource As BindingSource = Nothing
        Dim sql As String = "SELECT * FROM [" & style & "] WHERE PatientID = @PatientID"

        Using conn As New SqlConnection(My.Settings.DentistXConnectionString)
            conn.Open()
            dataSource = conn.Query(sql, New With {.PatientId = patientID}).ToList()
        End Using

        Select Case style.ToUpper()
            Case "LUPL"
                bindingSource = LUPLBindingSource
            Case "LDPL"
                bindingSource = LDPLBindingSource
            Case "RUPL"
                bindingSource = RUPLBindingSource
            Case "RDPL"
                bindingSource = RDPLBindingSource
        End Select

        ' Ensure BindingSource is initialized
        If bindingSource Is Nothing Then bindingSource = New BindingSource()

        ' Set the DataSource
        bindingSource.DataSource = dataSource
    End Sub

    Private Function GetCellIDForColor(ByVal style As String, ByVal patientID As Integer, ByVal cellAddress As Integer) As Integer
        Try
            ' Dictionary to store table names and their respective primary keys
            Dim tablePrimaryKeys As New Dictionary(Of String, String) From {
            {"LUPL", "LUcellID"},
            {"LDPL", "LDcellID"},
            {"RUPL", "RUcellID"},
            {"RDPL", "RDcellID"}
        }

            ' Ensure style is valid
            Dim upperStyle As String = style.ToUpper()
            If Not tablePrimaryKeys.ContainsKey(upperStyle) Then
                Return -1
            End If

            ' Construct the query dynamically
            Dim query As String = $"SELECT {tablePrimaryKeys(upperStyle)} FROM {upperStyle} WHERE patientID = @PatientID AND CellAddres = @CellAddres"

            Using conn As New SqlConnection(My.Settings.DentistXConnectionString)
                'Return conn.QueryFirstOrDefault(Of Integer?)(query, New With {.PatientID = patientID, .CellAddress = cellAddress}) ?? -1
                Dim result As Integer? = conn.QueryFirstOrDefault(Of Integer?)(query, New With {.PatientID = patientID, .CellAddres = cellAddress})
                Return If(result, -1) ' Fix: Replaces `??` with `If`
            End Using
        Catch ex As Exception
            MsgBox(ex.Message)
            Return -1
        End Try
    End Function

    Private Sub BackColorContext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BackColorContext.Click
        Try
            If PatientID <= 0 Then Exit Sub

            Dim FrClr As String
            Dim DilgClr As New ColorDialog()

            If DilgClr.ShowDialog() <> Windows.Forms.DialogResult.OK Then Exit Sub

            Try
                Dim tableName As String = ""
                Dim primaryKey As String = ""
                Dim activeCell As Infragistics.Win.UltraWinGrid.UltraGridCell = Nothing

                ' Determine the table and grid based on ContextSender
                Select Case ContextSender
                    Case "LUUltraGrid"
                        tableName = "LUPL"
                        primaryKey = "LUcellID"
                        activeCell = Me.LUUltraGrid.ActiveCell
                    Case "LDUltraGrid"
                        tableName = "LDPL"
                        primaryKey = "LDcellID"
                        activeCell = Me.LDUltraGrid.ActiveCell
                    Case "RUUltraGrid"
                        tableName = "RUPL"
                        primaryKey = "RUcellID"
                        activeCell = Me.RUUltraGrid.ActiveCell
                    Case "RDUltraGrid"
                        tableName = "RDPL"
                        primaryKey = "RDcellID"
                        activeCell = Me.RDUltraGrid.ActiveCell
                    Case Else
                        Exit Sub
                End Select

                ' Get cell address and apply color
                Dim cellCOL As Integer = activeCell.Column.Index
                Dim cellROW As Integer = activeCell.Row.Index
                Dim CellAdres As Integer = ColumnRowToAddress(cellCOL, cellROW)
                activeCell.Appearance.ForeColor = DilgClr.Color
                FrClr = DilgClr.Color.Name

                ' Insert using Dapper
                Using conn As New SqlConnection(My.Settings.DentistXConnectionString)
                    Dim query As String = $"INSERT INTO {tableName} (PatientID, CellAddres, ForeColor) VALUES (@PatientID, @CellAddres, @ForeColor)"
                    conn.Execute(query, New With {.PatientID = PatientID, .CellAddres = CellAdres, .ForeColor = FrClr})
                End Using

                ' Refresh Data
                RefreshColorBindingSource(tableName)
            Catch ex As SqlException
                MsgBox(ex.Message)
            End Try
        Catch ex As SqlException
            MsgBox(ex.Message)
        End Try
    End Sub


    Private Sub RefreshColorBindingSource(ByVal tableName As String)
        Select Case tableName
            Case "LUPL"
                clsPatient.PatientID = PatientID
                LUPLBindingSource.DataSource = clsPatientData.GetLUPL(clsPatient).ToList()
            Case "LDPL"
                clsPatient.PatientID = PatientID
                LDPLBindingSource.DataSource = clsPatientData.GetLDPL(clsPatient).ToList()
            Case "RUPL"
                clsPatient.PatientID = PatientID
                RUPLBindingSource.DataSource = clsPatientData.GetRUPL(clsPatient).ToList()
            Case "RDPL"
                clsPatient.PatientID = PatientID
                RDPLBindingSource.DataSource = clsPatientData.GetRDPL(clsPatient).ToList()
        End Select
    End Sub



#End Region




#End Region


#Region "Logging"

    Private Sub LogToListBox(message As String)
        If lstLog.InvokeRequired Then
            lstLog.Invoke(Sub() lstLog.Items.Add($"{DateTime.Now}: {message}"))
        Else
            lstLog.Items.Add($"{DateTime.Now}: {message}")
        End If
        lstLog.TopIndex = lstLog.Items.Count - 1 ' Auto-scroll to latest entry
    End Sub





    Private Sub LogMessage(message As String)
        'lstLog.Items.Add(message) ' Add message to the ListBox
        lstLog.Visible = True     ' Show the ListBox
        tmrLog.Start()            ' Start the timer
    End Sub

    Private Sub tmrLog_Tick(sender As Object, e As EventArgs) Handles tmrLog.Tick
        lstLog.Visible = False    ' Hide the ListBox
        tmrLog.Stop()             ' Stop the timer
    End Sub

    'Private Sub RUcountPL_Click(sender As Object, e As EventArgs) Handles RUcountPL.Click
    '    RUcountPL.Width = RUcountPL.Width + 20
    'End Sub



    'Private Sub RUUltraGrid_AfterCellUpdate(sender As Object, e As CellEventArgs) Handles RUUltraGrid.AfterCellUpdate, LUUltraGrid.AfterCellUpdate,
    '                                                                                      RDUltraGrid.AfterCellUpdate, LDUltraGrid.AfterCellUpdate
    '    MsgBox(e.Cell.Width)
    'End Sub

    'Private Sub LDUltraGrid_AfterRowResize(sender As Object, e As RowEventArgs) Handles LDUltraGrid.AfterRowResize
    '    MsgBox("AfterRowResize")
    'End Sub

    'Private Sub LDUltraGrid_AfterAutoSizeColumn(sender As Object, e As AfterAutoSizeColumnEventArgs) Handles LDUltraGrid.AfterAutoSizeColumn
    '    MsgBox("AfterAutoSizeColumn")
    'End Sub







    'LogToFile($"LDUltraGrid {vbCrLf}IsNewTreatDuplicate {vbCrLf} The treatment '{treatment}' already exists for tooth '{toothColumn}'!")
    'LogToFile($"LDUltraGrid {vbCrLf}Update failed.")
    'LogToListBox($"LDUltraGrid {vbCrLf}IsNewTreatDuplicate {vbCrLf} The treatment '{treatment}' already exists for tooth '{toothColumn}'!")
    'LogToListBox($"LDUltraGrid {vbCrLf}Update failed.")


#End Region
End Class

