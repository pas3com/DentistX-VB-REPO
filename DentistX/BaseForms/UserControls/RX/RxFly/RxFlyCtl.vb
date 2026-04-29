Imports System.Collections.Concurrent
Imports System.Data.SqlClient
Imports System.Drawing
Imports System.Drawing.Imaging
Imports System.Windows.Forms
Imports Dapper
Imports DevExpress.Utils.Extensions
Imports DevExpress.XtraEditors
Imports DevExpress.XtraPrinting.Drawing
Imports DevExpress.XtraReports.UI
Imports DevExpress.XtraTreeList

Public Class RxFlyCtl
    Implements IPatientAwareUserControl



    Private Sub IPatientAwareUserControl_LoadPatientData(patientId As Integer) Implements IPatientAwareUserControl.LoadPatientData
        'Me.Hide()
        'LoadPatientData(patientId)
        'Me.Show()
    End Sub

    Public Sub LoadPatientData(patientId As Integer)
        'LoadData(patientId)
    End Sub


    Private clsMedGrp As IEnumerable(Of MedicineGroups)
    Private clsMedFam As IEnumerable(Of MedicineFamily)
    Private clsMedScFam As IEnumerable(Of MedScienceFamily)
    Private clsMedItm As IEnumerable(Of MedicineItems)
    Private clsMedShp As IEnumerable(Of MedicineShape)
    Private clsMedDoz As IEnumerable(Of MedicineDoze)

    Private clsMedGrpData As New MedicineGroupsDATA
    Private clsMedFamData As New MedicineFamilyDATA
    Private clsMedScFamData As New MedScienceFamilyDATA
    Private clsMedItmData As New MedicineItemsDATA
    Private clsMedShpData As New MedicineShapeDATA
    Private clsMedDozData As New MedicineDozeDATA

    Private RxID As Integer = -1
    Private clsRxFly As IEnumerable(Of RxFly)
    Private clsRxFlyData As New RxFlyDATA


    Private clsRxBodyData As New RxBodyDATA
    Private clsRxBody As IEnumerable(Of RxBody)

    Private clsCtlRep As CtlRep
    Private clsCtlRepData As CtlRepDATA

    Public Sub New()
        Dim sw = StartTimer()
        ' This call is required by the designer.
        InitializeComponent()
        ' Enable double buffering to reduce flickering
        Me.DoubleBuffered = True
        SetStyle(ControlStyles.AllPaintingInWmPaint Or
                 ControlStyles.UserPaint Or
                 ControlStyles.DoubleBuffer, True)
        UpdateStyles()
        StoreOriginalBounds(Me)
        ' Add any initialization after the InitializeComponent() call.

        LogTime("Public Sub New", Me.Name, sw)


    End Sub



    Private ReadOnly controlBoundsCache As New ConcurrentDictionary(Of Control, Rectangle)
    Private Const OriginalPanelWidth As Integer = 1156
    Private Const OriginalPanelHeight As Integer = 654
    Private ReadOnly originalPanelSize As New Size(OriginalPanelWidth, OriginalPanelHeight)

    Private Sub StoreOriginalBounds(container As Control)
        Dim sw As New Stopwatch
        sw.Start()

        For Each ctrl As Control In container.Controls
            controlBoundsCache.TryAdd(ctrl, ctrl.Bounds)
            If ctrl.HasChildren Then StoreOriginalBounds(ctrl)
        Next
        sw.Stop()

    End Sub
    Private Sub ResizeControlsProportionally()
        If controlBoundsCache.IsEmpty Then Return
        Dim sw As New Stopwatch
        sw.Start()
        Dim widthRatio = CSng(Me.Width) / OriginalPanelWidth
        Dim heightRatio = CSng(Me.Height) / OriginalPanelHeight

        For Each kvp In controlBoundsCache
            kvp.Key.SetBounds(
            CInt(kvp.Value.X * widthRatio),
            CInt(kvp.Value.Y * heightRatio),
            CInt(kvp.Value.Width * widthRatio),
            CInt(kvp.Value.Height * heightRatio))
        Next
        sw.Stop()
    End Sub

    Private Sub PatientRXTreeFrm_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        ResizeControlsProportionally()
    End Sub


    Private Sub LoadRX()


        clsRxFly = clsRxFlyData.SelectAll
        If clsRxFly IsNot Nothing Then
            ' Set the DataSource
            RxFlyViewBindingSource.DataSource = clsRxFly.ToList()
        End If
    End Sub
    Private Sub PatientRX_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            LoadClasses()
            LoadRX()
            Me.txtDrName.Text = If(CurrentDoctor IsNot Nothing, CurrentDoctor.DrName, "")
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub





#Region "Medicine Hierarchy Loading, Binding & Search"

    Private Sub LoadClasses()

        '---------- 1) Load Full Master Lists ----------
        clsMedGrp = clsMedGrpData.SelectAll()
        clsMedFam = clsMedFamData.SelectAll()
        clsMedScFam = clsMedScFamData.SelectAll()
        clsMedItm = clsMedItmData.SelectAll()
        clsMedShp = clsMedShpData.SelectAll()
        clsMedDoz = clsMedDozData.SelectAll()

        '---------- 2) Create Binding Sources If Needed ----------
        If MedicineGroupsBindingSource Is Nothing Then MedicineGroupsBindingSource = New BindingSource()
        If MedicineFamilyBindingSource Is Nothing Then MedicineFamilyBindingSource = New BindingSource()
        If MedScienceFamilyBindingSource Is Nothing Then MedScienceFamilyBindingSource = New BindingSource()
        If MedicineItemsBindingSource Is Nothing Then MedicineItemsBindingSource = New BindingSource()
        If MedicineShapeBindingSource Is Nothing Then MedicineShapeBindingSource = New BindingSource()
        If MedicineDozeBindingSource Is Nothing Then MedicineDozeBindingSource = New BindingSource()

        '---------- 3) Set Initial DataSources ----------
        MedicineGroupsBindingSource.DataSource = clsMedGrp.ToList()
        MedicineFamilyBindingSource.DataSource = clsMedFam.ToList()
        MedScienceFamilyBindingSource.DataSource = clsMedScFam.ToList()
        MedicineItemsBindingSource.DataSource = clsMedItm.ToList()
        MedicineShapeBindingSource.DataSource = clsMedShp.ToList()
        MedicineDozeBindingSource.DataSource = clsMedDoz.ToList()

        '---------- 4) Safe Label Binding ----------
        LabelControl1.DataBindings.Clear()
        LabelControl1.DataBindings.Add(
        New Binding("Text", MedicineItemsBindingSource, "Notes", True, DataSourceUpdateMode.Never, "")
    )

        '---------- 5) Wire Relation Events ----------
        AddHandler MedicineGroupsBindingSource.CurrentChanged, AddressOf MedicineGroups_Changed
        AddHandler MedicineFamilyBindingSource.CurrentChanged, AddressOf MedicineFamily_Changed
        AddHandler MedScienceFamilyBindingSource.CurrentChanged, AddressOf MedScienceFamily_Changed
        AddHandler MedicineItemsBindingSource.CurrentChanged, AddressOf MedicineItems_Changed
        AddHandler MedicineShapeBindingSource.CurrentChanged, AddressOf MedicineShape_Changed

        '---------- 6) Wire Search TextBoxes ----------
        AddHandler txtGroups.TextChanged, AddressOf txtGroups_TextChanged
        AddHandler txtFamily.TextChanged, AddressOf txtFamily_TextChanged
        AddHandler txtScience.TextChanged, AddressOf txtScience_TextChanged
        AddHandler txtItems.TextChanged, AddressOf txtItems_TextChanged
        AddHandler txtShape.TextChanged, AddressOf txtShape_TextChanged
        AddHandler txtDoze.TextChanged, AddressOf txtDoze_TextChanged

        '---------- 7) Initialize Relations ----------
        MedicineGroups_Changed(Nothing, EventArgs.Empty)
        InitializeDragDrop()
    End Sub



    '===========================================================
    '   RELATION CASCADE LOGIC
    '===========================================================

    Private Sub MedicineGroups_Changed(sender As Object, e As EventArgs)
        Dim grp = TryCast(MedicineGroupsBindingSource.Current, MedicineGroups)

        If grp IsNot Nothing Then
            MedicineFamilyBindingSource.DataSource =
            clsMedFam.Where(Function(f) f.MedicineID = grp.MedicineID).ToList()
        Else
            MedicineFamilyBindingSource.DataSource = New List(Of MedicineFamily)
        End If

        MedicineFamily_Changed(Nothing, EventArgs.Empty)
    End Sub

    Private Sub MedicineFamily_Changed(sender As Object, e As EventArgs)
        Dim fam = TryCast(MedicineFamilyBindingSource.Current, MedicineFamily)

        If fam IsNot Nothing Then
            MedScienceFamilyBindingSource.DataSource =
            clsMedScFam.Where(Function(s) s.SubCatID = fam.SubCatID).ToList()
        Else
            MedScienceFamilyBindingSource.DataSource = New List(Of MedScienceFamily)
        End If

        MedScienceFamily_Changed(Nothing, EventArgs.Empty)
    End Sub

    Private Sub MedScienceFamily_Changed(sender As Object, e As EventArgs)

        Dim sci = TryCast(MedScienceFamilyBindingSource.Current, MedScienceFamily)
        Dim filteredItems As List(Of MedicineItems)

        If sci IsNot Nothing Then
            filteredItems = clsMedItm.Where(Function(i) i.ScincID = sci.ScincID).ToList()
        Else
            filteredItems = New List(Of MedicineItems)
        End If

        MedicineItemsBindingSource.DataSource = filteredItems

        ' Force refresh safely
        MedicineItemsBindingSource.ResetBindings(False)
        MedicineItems_Changed(Nothing, EventArgs.Empty)
    End Sub

    Private Sub MedicineItems_Changed(sender As Object, e As EventArgs)
        Dim itm = TryCast(MedicineItemsBindingSource.Current, MedicineItems)

        If itm IsNot Nothing Then
            MedicineShapeBindingSource.DataSource =
            clsMedShp.Where(Function(s) s.MedicineItemID = itm.MedicineItemID).ToList()
        Else
            MedicineShapeBindingSource.DataSource = New List(Of MedicineShape)
        End If

        MedicineShape_Changed(Nothing, EventArgs.Empty)
    End Sub

    Private Sub MedicineShape_Changed(sender As Object, e As EventArgs)
        Dim shp = TryCast(MedicineShapeBindingSource.Current, MedicineShape)

        If shp IsNot Nothing Then
            MedicineDozeBindingSource.DataSource =
            clsMedDoz.Where(Function(d) d.ShapeID = shp.ShapeID).ToList()
        Else
            MedicineDozeBindingSource.DataSource = New List(Of MedicineDoze)
        End If
    End Sub

    '==================================================
    ' UNIVERSAL FILTER LOGIC (Search All vs Filtered)
    '==================================================

    ' Generic apply filter that uses a selector to get the searchable text
    ' childChangedInvoker is a parameterless Action (can be Nothing)
    Private Sub ApplyFilter(Of T)(bindingSource As BindingSource,
                              baseList As IEnumerable(Of T),
                              selector As Func(Of T, String),
                              txt As TextEdit,
                              childChangedInvoker As Action)

        Dim term As String = txt.Text.Trim().ToLower()

        ' normalize baseList to a list so we can reuse it
        Dim working As List(Of T) = If(baseList IsNot Nothing, baseList.ToList(), New List(Of T)())

        Dim result As List(Of T)

        If String.IsNullOrEmpty(term) Then
            ' nothing typed -> restore the base list (respecting parent filters)
            result = working
        Else
            ' perform case-insensitive contains on the selected property
            result = working.Where(Function(x)
                                       Dim val = selector(x)
                                       Return Not String.IsNullOrEmpty(val) AndAlso val.ToLower().Contains(term)
                                   End Function).ToList()
        End If

        bindingSource.DataSource = result
        bindingSource.ResetBindings(False)

        If childChangedInvoker IsNot Nothing Then
            Try
                childChangedInvoker()
            Catch ex As Exception
                ' defensive: avoid bubbling unexpected exceptions from child refresh
            End Try
        End If
    End Sub


    ' ------------------------------
    ' Handlers that compute base list
    ' ------------------------------
    ' Note: pass a lambda that calls the existing Changed handlers with (Nothing, EventArgs.Empty)

    Private Sub txtGroups_TextChanged(sender As Object, e As EventArgs)
        Dim base As IEnumerable(Of MedicineGroups) = If(chkSearchAll.Checked, clsMedGrp, clsMedGrp)
        ApplyFilter(Of MedicineGroups)(
        MedicineGroupsBindingSource,
        base,
        Function(g) g.MedicineFamily,
        txtGroups,
        New Action(Sub() MedicineGroups_Changed(Nothing, EventArgs.Empty))
    )
    End Sub

    Private Sub txtFamily_TextChanged(sender As Object, e As EventArgs)
        Dim base As IEnumerable(Of MedicineFamily)
        If chkSearchAll.Checked Then
            base = clsMedFam
        Else
            Dim grp = TryCast(MedicineGroupsBindingSource.Current, MedicineGroups)
            If grp IsNot Nothing Then
                base = clsMedFam.Where(Function(f) f.MedicineID = grp.MedicineID)
            Else
                base = clsMedFam
            End If
        End If

        ApplyFilter(Of MedicineFamily)(
        MedicineFamilyBindingSource,
        base,
        Function(f) f.MedicineSubCat,
        txtFamily,
        New Action(Sub() MedicineFamily_Changed(Nothing, EventArgs.Empty))
    )
    End Sub

    Private Sub txtScience_TextChanged(sender As Object, e As EventArgs)
        Dim base As IEnumerable(Of MedScienceFamily)
        If chkSearchAll.Checked Then
            base = clsMedScFam
        Else
            Dim fam = TryCast(MedicineFamilyBindingSource.Current, MedicineFamily)
            If fam IsNot Nothing Then
                base = clsMedScFam.Where(Function(s) s.SubCatID = fam.SubCatID)
            Else
                base = clsMedScFam
            End If
        End If

        ApplyFilter(Of MedScienceFamily)(
        MedScienceFamilyBindingSource,
        base,
        Function(s) s.ScienceName,
        txtScience,
        New Action(Sub() MedScienceFamily_Changed(Nothing, EventArgs.Empty))
    )
    End Sub

    Private Sub txtItems_TextChanged(sender As Object, e As EventArgs)
        Dim base As IEnumerable(Of MedicineItems)
        If chkSearchAll.Checked Then
            base = clsMedItm
        Else
            Dim sci = TryCast(MedScienceFamilyBindingSource.Current, MedScienceFamily)
            If sci IsNot Nothing Then
                base = clsMedItm.Where(Function(i) i.ScincID = sci.ScincID)
            Else
                base = clsMedItm
            End If
        End If

        ApplyFilter(Of MedicineItems)(
        MedicineItemsBindingSource,
        base,
        Function(i) i.CommName,
        txtItems,
        New Action(Sub() MedicineItems_Changed(Nothing, EventArgs.Empty))
    )
    End Sub

    Private Sub txtShape_TextChanged(sender As Object, e As EventArgs)
        Dim base As IEnumerable(Of MedicineShape)
        If chkSearchAll.Checked Then
            base = clsMedShp
        Else
            Dim itm = TryCast(MedicineItemsBindingSource.Current, MedicineItems)
            If itm IsNot Nothing Then
                base = clsMedShp.Where(Function(s) s.MedicineItemID = itm.MedicineItemID)
            Else
                base = clsMedShp
            End If
        End If

        ApplyFilter(Of MedicineShape)(
        MedicineShapeBindingSource,
        base,
        Function(s) s.ShapeInfo,
        txtShape,
        New Action(Sub() MedicineShape_Changed(Nothing, EventArgs.Empty))
    )
    End Sub

    Private Sub txtDoze_TextChanged(sender As Object, e As EventArgs)
        Dim base As IEnumerable(Of MedicineDoze)
        If chkSearchAll.Checked Then
            base = clsMedDoz
        Else
            Dim shp = TryCast(MedicineShapeBindingSource.Current, MedicineShape)
            If shp IsNot Nothing Then
                base = clsMedDoz.Where(Function(d) d.ShapeID = shp.ShapeID)
            Else
                base = clsMedDoz
            End If
        End If

        ApplyFilter(Of MedicineDoze)(
        MedicineDozeBindingSource,
        base,
        Function(d) d.Doze,
        txtDoze,
        Nothing ' last level has no child to refresh
    )
    End Sub


#End Region

#Region "Add Medicine to RX - DragDrop and Enter Key"

    ' -------------------------------
    ' Add selected medicine to Ultra
    ' -------------------------------
    Private Sub AddSelectedMedicineToUltra()
        Try
            ' Retrieve selected items
            Dim selectedItem As MedicineItems = TryCast(grpItems.SelectedItem, MedicineItems)
            Dim selectedShape As MedicineShape = TryCast(grpShape.SelectedItem, MedicineShape)
            Dim selectedDoze As MedicineDoze = TryCast(grpDoze.SelectedItem, MedicineDoze)

            ' Ensure all three are selected
            If selectedItem Is Nothing OrElse selectedShape Is Nothing OrElse selectedDoze Is Nothing Then
                MsgBox("Please select an item, shape, and doze.", MsgBoxStyle.Exclamation)
                Return
            End If

            ' Format text
            Dim formattedText As String =
            $"*  {selectedItem.CommName} {selectedShape.MedicineShape}" & vbCrLf &
            $"    {selectedDoze.Doze}" & vbCrLf & vbCrLf

            ' Append to Ultra
            Me.Ultra.Text += formattedText

        Catch ex As Exception
            MsgBox($"Error: {ex.Message}", MsgBoxStyle.Critical)
        End Try
    End Sub

    ' -------------------------------
    ' Enter key for each ListBox
    ' -------------------------------
    Private Sub grpIrems_KeyDown(sender As Object, e As KeyEventArgs) Handles grpItems.KeyDown
        If e.KeyCode = Keys.Enter Then
            AddSelectedMedicineToUltra()
            e.Handled = True
        End If
    End Sub

    Private Sub grpShape_KeyDown(sender As Object, e As KeyEventArgs) Handles grpShape.KeyDown
        If e.KeyCode = Keys.Enter Then
            AddSelectedMedicineToUltra()
            e.Handled = True
        End If
    End Sub

    Private Sub grpDoze_KeyDown(sender As Object, e As KeyEventArgs) Handles grpDoze.KeyDown
        If e.KeyCode = Keys.Enter Then
            AddSelectedMedicineToUltra()
            e.Handled = True
        End If
    End Sub

    ' -------------------------------
    ' Enable Drag & Drop
    ' -------------------------------
    Private Sub EnableDragDropForListBox(lst As ListBoxControl)
        ' Never allow dropping *onto* the listbox itself
        lst.AllowDrop = False
        AddHandler lst.MouseDown, Sub(sender, e)
                                      Dim l As ListBoxControl = DirectCast(sender, ListBoxControl)
                                      If l.SelectedItem IsNot Nothing Then
                                          l.DoDragDrop(l.SelectedItem, DragDropEffects.Copy)
                                      End If
                                  End Sub

    End Sub

    ' -------------------------------
    ' Call this once after LoadClasses
    ' -------------------------------
    Private Sub InitializeDragDrop()

        EnableDragFromListBox(grpItems)
        EnableDragFromListBox(grpShape)
        EnableDragFromListBox(grpDoze)
        ' Set Ultra as the drop target
        Ultra.AllowDrop = True
        ' Remove old handlers if exist (prevents duplicates)
        RemoveHandler Ultra.DragEnter, AddressOf Ultra_DragEnter
        RemoveHandler Ultra.DragDrop, AddressOf Ultra_DragDrop

        ' Add valid handlers
        AddHandler Ultra.DragEnter, AddressOf Ultra_DragEnter
        AddHandler Ultra.DragDrop, AddressOf Ultra_DragDrop
        'AddHandler Ultra.DragEnter, Sub(sender, e)
        '                                If e.Data.GetDataPresent(GetType(Object)) Then
        '                                    e.Effect = DragDropEffects.Copy
        '                                Else
        '                                    e.Effect = DragDropEffects.None
        '                                End If
        '                            End Sub

        'AddHandler Ultra.DragDrop, Sub(sender, e)
        '                               AddSelectedMedicineToUltra()
        '                           End Sub
    End Sub

    ' === Drag-Drop Initialization ===
    ' === Enables Dragging for ListBox with Timer Delay ===
    Private Sub EnableDragFromListBox(lst As ListBoxControl)
        Dim dragTimer As Timer = New Timer() With {.Interval = 1000}
        Dim dragStartPoint As Point
        Dim isDragging As Boolean = False

        AddHandler lst.MouseDown,
        Sub(sender As Object, e As MouseEventArgs)
            If e.Button = MouseButtons.Left Then
                dragStartPoint = e.Location
                dragTimer.Start()
            End If
        End Sub

        AddHandler lst.MouseUp,
        Sub(sender As Object, e As MouseEventArgs)
            dragTimer.Stop()
            isDragging = False
        End Sub

        AddHandler dragTimer.Tick,
        Sub()
            dragTimer.Stop()
            If lst.SelectedItem IsNot Nothing Then
                isDragging = True
                lst.DoDragDrop(lst.SelectedItem.ToString(), DragDropEffects.Copy)
            End If
        End Sub
    End Sub

    ' Handle DragEnter event on the target control
    Private Sub Ultra_DragEnter(sender As Object, e As DragEventArgs) Handles Ultra.DragEnter
        ' Check if the data being dragged is text
        If e.Data.GetDataPresent(DataFormats.Text) Then
            e.Effect = DragDropEffects.Copy
        Else
            e.Effect = DragDropEffects.None
        End If
    End Sub

    ' Handle DragDrop event on the target control
    Private Sub Ultra_DragDrop(sender As Object, e As DragEventArgs) Handles Ultra.DragDrop
        'Dim droppedText As String = CType(e.Data.GetData(DataFormats.Text), String)
        '' Append the dropped data to the target control (e.g., Ultra.Text)
        'Me.Ultra.Text += droppedText
        AddSelectedMedicineToUltra()
    End Sub




#End Region


#Region "Patient RX"

    Private Sub btnResetTexts_Click(sender As Object, e As EventArgs) Handles btnResetTexts.Click
        txtDoze.Text = ""
        txtShape.Text = ""
        txtItems.Text = ""
        txtScience.Text = ""
        txtFamily.Text = ""
        txtGroups.Text = ""
    End Sub


    Private Sub btnAddToRX_Click(sender As Object, e As EventArgs) Handles btnAddToRX.Click
        Try
            ' Retrieve the currently selected items from each ListBox
            Dim selectedGroup As MedicineGroups = TryCast(grpGroups.SelectedItem, MedicineGroups)
            Dim selectedFamily As MedicineFamily = TryCast(grpFamily.SelectedItem, MedicineFamily)
            Dim selectedScience As MedScienceFamily = TryCast(grpScience.SelectedItem, MedScienceFamily)
            Dim selectedItem As MedicineItems = TryCast(grpItems.SelectedItem, MedicineItems)
            Dim selectedShape As MedicineShape = TryCast(grpShape.SelectedItem, MedicineShape)
            Dim selectedDoze As MedicineDoze = TryCast(grpDoze.SelectedItem, MedicineDoze)

            ' Ensure all necessary parent selections exist
            If selectedGroup Is Nothing OrElse
           selectedFamily Is Nothing OrElse
           selectedScience Is Nothing OrElse
           selectedItem Is Nothing OrElse
           selectedShape Is Nothing OrElse
           selectedDoze Is Nothing Then

                MsgBox("Please select a complete medicine hierarchy before adding.", MsgBoxStyle.Exclamation)
                Return
            End If

            ' Format the output string
            Dim formattedText As String =
            $"*  {selectedItem.CommName} {selectedShape.MedicineShape}" & vbCrLf &
            $"    {selectedDoze.Doze}" & vbCrLf & vbCrLf

            ' Append to the Ultra.Text control
            Me.Ultra.Text += formattedText

        Catch ex As Exception
            MsgBox($"Error: {ex.Message}", MsgBoxStyle.Critical)
        End Try
    End Sub



    Private Sub btSaveRX_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btSaveRX.Click

        If String.IsNullOrWhiteSpace(Me.Ultra.Text) Then
            MsgBox(If(Eng, "You Can't Save Empty RX...", "لا يمكن حفظ وصفة فارغة...."))
            Exit Sub
        End If
        If String.IsNullOrWhiteSpace(Me.txtPatientName.Text) Then
            MsgBox(If(Eng, " Enter Patient Name...", "ادخل اسم المريض...."))
            Exit Sub
        End If
        If String.IsNullOrWhiteSpace(Me.txtDrName.Text) Then
            MsgBox(If(Eng, " Enter Doctor Name...", "ادخل اسم الطبيب...."))
            Exit Sub
        End If
        If SpinAge.Value < 3 Or SpinAge.Value > 130 Then
            MsgBox(If(Eng, " Enter Valid Age...", "ادخل عمر صحيح...."))
            Exit Sub
        End If
        Try
            Dim RXdat As Date = Date.Now
            Dim PatientName1 As String = txtPatientName.Text
            Dim PatientAge As Nullable(Of Integer) = Nothing


            PatientAge = Convert.ToInt32(SpinAge.Value)

            Dim PatientSex As String = If(RadioMale.Checked, If(Eng, "Male", "ذكر"), If(Eng, "Female", "أنثى"))
            ' Replace Qrs.Patient_RXInsert with your class method:
            If clsRxFlyData.Add(New RxFly With {.PatientName = PatientName1, .PatientAge = PatientAge, .PatientSex = PatientSex, .RxDate = RXdat, .RX = Me.Ultra.Text, .DrName = txtDrName.Text}) Then
                RxID = clsRxFlyData.GetLastRX()
            End If

            ' Refresh RX list
            LoadRX()
            Me.btnAddToRX.Enabled = False

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        '=====================================


        'If String.IsNullOrWhiteSpace(Me.Ultra.Text) Then
        '    MsgBox(If(Eng, "You Can't Save Empty RX...", "لا يمكن حفظ وصفة فارغة...."))
        '    Exit Sub
        'End If

        'Try
        '    Dim RXdat As Date = Date.Now
        '    If Not Eng Then
        '        RXdat = HijriToGregorian(RXdat.Year, RXdat.Month, RXdat.Day)
        '    End If

        '    ' Replace Qrs.Patient_RXInsert with your class method:
        '    clsRxData.Add(New Patient_RX With {.PatientID = PatientID, .RXDate = RXdat, .RX = Me.Ultra.Text})

        '    ' Refresh RX list
        '    LoadRX(PatientID)
        '    Me.btnAddToRX.Enabled = False
        '    PatientRXVIEWBindingSource.MoveLast()
        '    Patient_RXBindingSource.MoveLast()
        'Catch ex As Exception
        '    MsgBox(ex.Message)
        'End Try
        ''=====================================

    End Sub


    Private Sub btNewRX_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btNewRX.Click
        Me.Ultra.Text = ""
        Me.txtPatientName.Text = ""
        Me.SpinAge.Value = 20
        Me.RadioMale.Checked = True
        Me.btnAddToRX.Enabled = True
        Me.btSaveRX.Enabled = True
        Me.txtDrName.Text = If(CurrentDoctor IsNot Nothing, CurrentDoctor.DrName, "")
    End Sub
    Private Sub RxDel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RxDel.Click
        Try
            Dim msg As String = If(Eng, "Delete The Record????", "هل تريد حذف الوصفة؟؟؟")
            If MsgBox(msg, MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                Dim currentRX = CType(RxFlyViewBindingSource.Current, RxFly)
                If currentRX IsNot Nothing Then
                    Dim rxID As Integer = CInt(currentRX.RxID)
                    Dim rx As New RxFly With {.RxID = rxID}
                    clsRxFlyData.Delete(rx)
                    LoadRX()
                End If
            Else
                LoadRX()
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        '===============================

    End Sub
    Private Sub RxSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RxSave.Click
        Try
            Me.Validate()

        Catch ex As System.Data.SqlClient.SqlException
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub PatientRXVIEWBindingSource_PositionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles RxFlyViewBindingSource.PositionChanged

        Dim RxRow = CType(RxFlyViewBindingSource.Current, RxFly)
        If RxRow IsNot Nothing Then
            RxID = RxRow.RxID
        End If
    End Sub



    Private Sub btPrintFullRX_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btPrintFullRX.Click
        If RxID <= 0 Then
            MsgBox(If(Eng, "You Cant Print Empty RX", "لا يمكن طباعة وصفة فارغة...."))
            Exit Sub
        End If
        Try
            Dim wimg As Image = Nothing
            Dim s As String = ""
            Dim useimg, usetxt As Boolean
            'Patient_RXBody 
            clsRxBody = clsRxBodyData.SelectAll
            RxBodyBindingSource.DataSource = clsRxBody.ToList
            Dim row As RxBody = CType(RxBodyBindingSource.Current, RxBody) 'clsRxBody?.FirstOrDefault()
            If row IsNot Nothing Then
                useimg = row.UseWtrImg
                usetxt = row.UseWtrText
                If row.WtrImg IsNot Nothing AndAlso row.WtrImg.Length > 0 Then
                    Using ms As New IO.MemoryStream(row.WtrImg)
                        Using original = Image.FromStream(ms)
                            wimg = CType(original.Clone(), Image) ' Clone to avoid stream issues
                        End Using
                    End Using


                    Dim picPath = IO.Path.Combine(Application.StartupPath, "Images", "Wtr.jpg")

                    Dim dirPath = IO.Path.GetDirectoryName(picPath)
                    If Not IO.Directory.Exists(dirPath) Then IO.Directory.CreateDirectory(dirPath)
                    If IO.File.Exists(picPath) Then IO.File.Delete(picPath)

                    Try
                        ' Fully recreate a bitmap into memory (does not depend on stream)
                        Dim tempBmp As New Bitmap(wimg.Width, wimg.Height)
                        Using g As Graphics = Graphics.FromImage(tempBmp)
                            g.DrawImage(wimg, 0, 0, wimg.Width, wimg.Height)
                        End Using

                        tempBmp.Save(picPath, Imaging.ImageFormat.Jpeg)
                    Catch ex As Exception
                        MsgBox($"Image save failed: {ex.Message}")
                    End Try
                End If


                If Not String.IsNullOrEmpty(row.WtrText) Then
                    s = row.WtrText
                End If

                Dim rpt As New MainRxFly(RxID)
                Dim pictureWatermark As New Watermark()

                If wimg IsNot Nothing AndAlso useimg Then
                    pictureWatermark.ImageSource = ImageSource.FromFile(IO.Path.Combine(Application.StartupPath, "Images", "Wtr.jpg"))
                    pictureWatermark.ImageAlign = ContentAlignment.MiddleCenter
                    pictureWatermark.ImageTiling = False
                    pictureWatermark.ImageViewMode = ImageViewMode.Zoom
                    pictureWatermark.ImageTransparency = 200
                    pictureWatermark.ShowBehind = True
                End If

                If s.Length > 0 AndAlso usetxt Then
                    pictureWatermark.Text = s
                    pictureWatermark.TextDirection = DirectionMode.ForwardDiagonal
                    pictureWatermark.ForeColor = Color.DodgerBlue
                    pictureWatermark.TextTransparency = 50
                    pictureWatermark.ShowBehind = False
                End If

                rpt.Watermark.CopyFrom(pictureWatermark)

                Dim printTool As New ReportPrintTool(rpt)
                printTool.ShowPreviewDialog()
            Else
                MsgBox(If(Eng,
                      "The Rx Details Is Empty....." & vbCrLf & "Fill The Rx Details And Try Again.",
                      "تفاصيل الوصفة فارغة...." & vbCrLf & "قم بتعبئة تفاصيل الوصفة وحاول مرة اخرى."))
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub



    Private Sub btnPrintRX_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrintRX.Click

        Try
            Dim rpt As New RxFlyPrint(RxID)
            Dim printTool As New DevExpress.XtraReports.UI.ReportPrintTool(rpt)
            ' Make sure the document is generated
            rpt.CreateDocument()
            Dim previewForm As Form = printTool.PreviewForm

            ' Force manual positioning and reposition once the form is shown
            previewForm.StartPosition = FormStartPosition.Manual
            AddHandler previewForm.Shown, Sub(s, ev)
                                              Try
                                                  Dim scr = Screen.FromControl(Me).WorkingArea
                                                  Dim x = scr.Left + (scr.Width - previewForm.Width) \ 2
                                                  Dim y = scr.Top + (scr.Height - previewForm.Height) \ 2
                                                  previewForm.Location = New Point(Math.Max(scr.Left, x), Math.Max(scr.Top, y))
                                              Catch
                                                  ' swallow any error (fallback to default positioning)
                                              End Try
                                          End Sub

            previewForm.ShowDialog(Me)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Ultra_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Ultra.TextChanged

        If Me.Ultra.Text = "" Then
            Me.btSaveRX.Enabled = False
        End If
    End Sub

    Private Sub btnEditRxDetail_Click(sender As Object, e As EventArgs) Handles btnEditRxDetail.Click
        FrmRxDetails.ShowDialog()

    End Sub


    Public Property RepLblLocs() As IEnumerable(Of CtlRep)

    Private Sub btnLocations_Click(sender As Object, e As EventArgs) Handles btnLocations.Click
        Try
            Using FrmReportLbl
                If FrmReportLbl.ShowDialog(Me) = DialogResult.OK Then
                    RepLblLocs = FrmReportLbl.RepLbls
                End If
            End Using
        Catch ex As Exception

        End Try
    End Sub

#End Region



End Class


