Imports System.Diagnostics

Public Class MedicForm

    Private Sub MedicForm_Load(sender As Object, e As EventArgs) Handles Me.Load
        loadClasses()

        FillAllCombos()
        'MedicineFamilyCombo1.m
    End Sub

#Region "Class Variables"
    Private MedGroups As IEnumerable(Of MedicineGroups)
    Private MedFamily As IEnumerable(Of MedicineFamily)
    Private MedScinFamily As IEnumerable(Of MedScienceFamily)
    Private MedItems As IEnumerable(Of MedicineItems)
    Private MedShape As IEnumerable(Of MedicineShape)
    Private MedDoze As IEnumerable(Of MedicineDoze)

    Private MedGroupsData As New MedicineGroupsDATA
    Private MedFamilyData As New MedicineFamilyDATA
    Private MedScinFamilyData As New MedScienceFamilyDATA
    Private MedItemsData As New MedicineItemsDATA
    Private MedShapeData As New MedicineShapeDATA
    Private MedDozeData As New MedicineDozeDATA
#End Region

#Region "Load / Refresh Helpers"

    Public Sub loadClasses()
        Dim sw As New Stopwatch
        sw.Start()
        Try
            MedGroups = MedGroupsData.SelectAll()
            If MedicineGroupsBindingSource Is Nothing Then MedicineGroupsBindingSource = New BindingSource()
            MedicineGroupsBindingSource.DataSource = MedGroups.ToList()

            MedFamily = MedFamilyData.SelectAll()
            If MedicineFamilyBindingSource Is Nothing Then MedicineFamilyBindingSource = New BindingSource()
            MedicineFamilyBindingSource.DataSource = MedFamily.ToList()

            MedScinFamily = MedScinFamilyData.SelectAll()
            If ScienceFamilyBindingSource Is Nothing Then ScienceFamilyBindingSource = New BindingSource()
            ScienceFamilyBindingSource.DataSource = MedScinFamily.ToList()

            MedItems = MedItemsData.SelectAll()
            If MedicineItemsBindingSource Is Nothing Then MedicineItemsBindingSource = New BindingSource()
            MedicineItemsBindingSource.DataSource = MedItems.ToList()

            MedShape = MedShapeData.SelectAll()
            If MedicineShapeBindingSource Is Nothing Then MedicineShapeBindingSource = New BindingSource()
            MedicineShapeBindingSource.DataSource = MedShape.ToList()

            MedDoze = MedDozeData.SelectAll()
            If MedicineDozeBindingSource Is Nothing Then MedicineDozeBindingSource = New BindingSource()
            MedicineDozeBindingSource.DataSource = MedDoze.ToList()
        Catch ex As System.Data.SqlClient.SqlException
            MsgBox(ex.Message)
        End Try
        sw.Stop()
        LogToFile("loadClasses Time spent: " & sw.Elapsed.TotalSeconds.ToString("F2") & " seconds", Me)

        ' Apply cascade filters so child comboboxes/grids reflect current selection
        ApplyFiltersAfterLoad()
    End Sub

    Private Sub ApplyFiltersAfterLoad()
        FilterFamilyByGroup()
        FilterScienceByFamily()
        FilterItemsByScience()
        FilterShapeByItem()
        FilterDozeByShape()
    End Sub

    Private Sub RefreshMedGroups()
        MedGroups = MedGroupsData.SelectAll()
        MedicineGroupsBindingSource.DataSource = MedGroups.ToList()
    End Sub

    Private Sub RefreshMedFamily()
        MedFamily = MedFamilyData.SelectAll()
        FilterFamilyByGroup()
    End Sub

    Private Sub RefreshMedScienceFamily()
        MedScinFamily = MedScinFamilyData.SelectAll()
        FilterScienceByFamily()
    End Sub

    Private Sub RefreshMedItems()
        MedItems = MedItemsData.SelectAll()
        FilterItemsByScience()
    End Sub

    Private Sub RefreshMedShape()
        MedShape = MedShapeData.SelectAll()
        FilterShapeByItem()
    End Sub

    Private Sub RefreshMedDoze()
        MedDoze = MedDozeData.SelectAll()
        FilterDozeByShape()
    End Sub

#End Region

#Region "Get current helpers"

    Private Function CurrentGroup() As MedicineGroups
        Return TryCast(MedicineGroupsBindingSource.Current, MedicineGroups)
    End Function

    Private Function CurrentFamily() As MedicineFamily
        Return TryCast(MedicineFamilyBindingSource.Current, MedicineFamily)
    End Function

    Private Function CurrentScience() As MedScienceFamily
        Return TryCast(ScienceFamilyBindingSource.Current, MedScienceFamily)
    End Function

    Private Function CurrentItem() As MedicineItems
        Return TryCast(MedicineItemsBindingSource.Current, MedicineItems)
    End Function

    Private Function CurrentShape() As MedicineShape
        Return TryCast(MedicineShapeBindingSource.Current, MedicineShape)
    End Function

    Private Function CurrentDoze() As MedicineDoze
        Return TryCast(MedicineDozeBindingSource.Current, MedicineDoze)
    End Function

#End Region

#Region "Filter helpers (cascade)"

    Private Function GetSelectedGroupID() As Integer?
        Dim g = CurrentGroup()
        If g Is Nothing Then Return Nothing
        Return g.MedicineID
    End Function

    Private Function GetSelectedFamilyID() As Integer?
        Dim f = CurrentFamily()
        If f Is Nothing Then Return Nothing
        Return f.SubCatID
    End Function

    Private Function GetSelectedScienceID() As Integer?
        Dim s = CurrentScience()
        If s Is Nothing Then Return Nothing
        Return s.ScincID
    End Function

    Private Function GetSelectedItemID() As Integer?
        Dim it = CurrentItem()
        If it Is Nothing Then Return Nothing
        Return it.MedicineItemID
    End Function

    Private Function GetSelectedShapeID() As Integer?
        Dim sh = CurrentShape()
        If sh Is Nothing Then Return Nothing
        Return sh.ShapeID
    End Function

    Private Sub FilterFamilyByGroup()
        Try
            Dim gid = GetSelectedGroupID()
            If gid Is Nothing Then
                MedicineFamilyBindingSource.DataSource = New List(Of MedicineFamily)()
            Else
                Dim filtered = MedFamily.Where(Function(x) x.MedicineID = gid.Value).ToList()
                MedicineFamilyBindingSource.DataSource = filtered
            End If
            If MedicineFamilyBindingSource.Count > 0 Then MedicineFamilyBindingSource.Position = 0
            FilterScienceByFamily()
        Catch ex As Exception
            Debug.WriteLine("FilterFamilyByGroup: " & ex.Message)
        End Try
    End Sub

    Private Sub FilterScienceByFamily()
        Try
            Dim fid = GetSelectedFamilyID()
            If fid Is Nothing Then
                ScienceFamilyBindingSource.DataSource = New List(Of MedScienceFamily)()
            Else
                Dim filtered = MedScinFamily.Where(Function(x) x.SubCatID = fid.Value).ToList()
                ScienceFamilyBindingSource.DataSource = filtered
            End If
            If ScienceFamilyBindingSource.Count > 0 Then ScienceFamilyBindingSource.Position = 0
            FilterItemsByScience()
        Catch ex As Exception
            Debug.WriteLine("FilterScienceByFamily: " & ex.Message)
        End Try
    End Sub

    Private Sub FilterItemsByScience()
        Try
            Dim sid = GetSelectedScienceID()
            If sid Is Nothing Then
                MedicineItemsBindingSource.DataSource = New List(Of MedicineItems)()
            Else
                Dim filtered = MedItems.Where(Function(x) x.ScincID = sid.Value).ToList()
                MedicineItemsBindingSource.DataSource = filtered
            End If
            If MedicineItemsBindingSource.Count > 0 Then MedicineItemsBindingSource.Position = 0
            FilterShapeByItem()
        Catch ex As Exception
            Debug.WriteLine("FilterItemsByScience: " & ex.Message)
        End Try
    End Sub

    Private Sub FilterShapeByItem()
        Try
            Dim iid = GetSelectedItemID()
            If iid Is Nothing Then
                MedicineShapeBindingSource.DataSource = New List(Of MedicineShape)()
            Else
                Dim filtered = MedShape.Where(Function(x) x.MedicineItemID = iid.Value).ToList()
                MedicineShapeBindingSource.DataSource = filtered
            End If
            If MedicineShapeBindingSource.Count > 0 Then MedicineShapeBindingSource.Position = 0
            FilterDozeByShape()
        Catch ex As Exception
            Debug.WriteLine("FilterShapeByItem: " & ex.Message)
        End Try
    End Sub

    Private Sub FilterDozeByShape()
        Try
            Dim shid = GetSelectedShapeID()
            If shid Is Nothing Then
                MedicineDozeBindingSource.DataSource = New List(Of MedicineDoze)()
            Else
                Dim filtered = MedDoze.Where(Function(x) x.ShapeID = shid.Value).ToList()
                MedicineDozeBindingSource.DataSource = filtered
            End If
            If MedicineDozeBindingSource.Count > 0 Then MedicineDozeBindingSource.Position = 0
        Catch ex As Exception
            Debug.WriteLine("FilterDozeByShape: " & ex.Message)
        End Try
    End Sub

#End Region

#Region "Combo Cascade Logic"

    Private Sub InitializeComboEvents()
        '' Attach event handlers once, safely
        'RemoveHandler cboMedGroup.SelectedIndexChanged, AddressOf cboMedGroup_SelectedIndexChanged
        'RemoveHandler cboMedFamily.SelectedIndexChanged, AddressOf cboMedFamily_SelectedIndexChanged
        'RemoveHandler cboScinetificName.SelectedIndexChanged, AddressOf cboScinetificName_SelectedIndexChanged
        'RemoveHandler cboMedItem.SelectedIndexChanged, AddressOf cboMedItem_SelectedIndexChanged
        'RemoveHandler cboShapeInfo.SelectedIndexChanged, AddressOf cboShapeInfo_SelectedIndexChanged

        'AddHandler cboMedGroup.SelectedIndexChanged, AddressOf cboMedGroup_SelectedIndexChanged
        'AddHandler cboMedFamily.SelectedIndexChanged, AddressOf cboMedFamily_SelectedIndexChanged
        'AddHandler cboScinetificName.SelectedIndexChanged, AddressOf cboScinetificName_SelectedIndexChanged
        'AddHandler cboMedItem.SelectedIndexChanged, AddressOf cboMedItem_SelectedIndexChanged
        'AddHandler cboShapeInfo.SelectedIndexChanged, AddressOf cboShapeInfo_SelectedIndexChanged
    End Sub


    '=============================
    '   Combo Cascade Handlers
    '=============================

    'Private Sub cboMedGroup_SelectedIndexChanged(sender As Object, e As EventArgs)
    '    Dim sel = TryCast(cboMedGroup.SelectedItem, ComboItem)
    '    If sel Is Nothing Then Return

    '    ' Load families belonging to this group
    '    Dim data As New MedicineFamilyDATA
    '    Dim list = data.SelectBySubCatID(sel.ID)
    '    MedicineFamilyBindingSource.DataSource = list

    '    ' Clear lower levels
    '    ScienceFamilyBindingSource.Clear()
    '    MedicineItemsBindingSource.Clear()
    '    MedicineShapeBindingSource.Clear()
    '    MedicineDozeBindingSource.Clear()

    '    FillCombos()
    '    ApplyFiltersAfterLoad()
    'End Sub
    'Private Sub cboMedFamily_SelectedIndexChanged(sender As Object, e As EventArgs)
    '    Dim sel = TryCast(cboMedFamily.SelectedItem, ComboItem)
    '    If sel Is Nothing Then Return

    '    ' Load sciences for this family
    '    Dim data As New MedScienceFamilyDATA
    '    Dim list = data.SelectByScincID(sel.ID)
    '    ScienceFamilyBindingSource.DataSource = list

    '    ' Clear lower levels
    '    MedicineItemsBindingSource.Clear()
    '    MedicineShapeBindingSource.Clear()
    '    MedicineDozeBindingSource.Clear()

    '    FillCombos()
    '    ApplyFiltersAfterLoad()
    'End Sub
    'Private Sub cboScinetificName_SelectedIndexChanged(sender As Object, e As EventArgs)
    '    Dim sel = TryCast(cboScinetificName.SelectedItem, ComboItem)
    '    If sel Is Nothing Then Return

    '    ' Load medicine items for this science family
    '    Dim data As New MedicineItemsDATA
    '    Dim list = data.SelectByMedicineItemID(sel.ID)
    '    MedicineItemsBindingSource.DataSource = list

    '    ' Clear lower levels
    '    MedicineShapeBindingSource.Clear()
    '    MedicineDozeBindingSource.Clear()

    '    FillCombos()
    '    ApplyFiltersAfterLoad()
    'End Sub
    'Private Sub cboMedItem_SelectedIndexChanged(sender As Object, e As EventArgs)
    '    Dim sel = TryCast(cboMedItem.SelectedItem, ComboItem)
    '    If sel Is Nothing Then Return

    '    ' Load shapes for this medicine item
    '    Dim data As New MedicineShapeDATA
    '    Dim list = data.SelectByShapeID(sel.ID)
    '    MedicineShapeBindingSource.DataSource = list

    '    ' Clear lower level
    '    MedicineDozeBindingSource.Clear()

    '    FillCombos()
    '    ApplyFiltersAfterLoad()
    'End Sub
    'Private Sub cboShapeInfo_SelectedIndexChanged(sender As Object, e As EventArgs)
    '    Dim sel = TryCast(cboShapeInfo.SelectedItem, ComboItem)
    '    If sel Is Nothing Then Return

    '    ' Load doses for this shape
    '    Dim data As New MedicineDozeDATA
    '    Dim list = data.SelectByDozeID(sel.ID)
    '    MedicineDozeBindingSource.DataSource = list

    '    FillCombos()
    '    ApplyFiltersAfterLoad()
    'End Sub

#End Region
    Private _isFillingCombos As Boolean = False

#Region "Fill Combos (DevExpress ComboBoxEdit)"

    ' Reusable item type for all combos
    Private Class ComboItem
        Public Property ID As Integer
        Public Property Name As String
        Public Sub New(id As Integer, name As String)
            Me.ID = id
            Me.Name = name
        End Sub
        Public Overrides Function ToString() As String
            Return Name
        End Function
    End Class

    ' Convert IEnumerable(Of T) to List(Of ComboItem)
    Private Function CreateComboItems(Of T)(items As IEnumerable(Of T)) As List(Of ComboItem)
        Dim list As New List(Of ComboItem)
        If items Is Nothing Then Return list

        Dim tt = GetType(T)
        Dim props = tt.GetProperties()

        Dim idProp As Reflection.PropertyInfo = props.FirstOrDefault(Function(p) p.Name.ToLower().EndsWith("id"))
        If idProp Is Nothing Then
            idProp = props.FirstOrDefault(Function(p) IsNumericType(p.PropertyType))
        End If

        Dim nameProp As Reflection.PropertyInfo =
            props.FirstOrDefault(Function(p) p.PropertyType Is GetType(String) AndAlso (idProp Is Nothing OrElse p.Name <> idProp.Name))
        If nameProp Is Nothing Then
            nameProp = props.FirstOrDefault(Function(p) idProp Is Nothing OrElse p.Name <> idProp.Name)
        End If

        For Each it In items
            Try
                Dim idVal As Integer = 0
                If idProp IsNot Nothing Then
                    Dim raw = idProp.GetValue(it, Nothing)
                    If raw IsNot Nothing Then Integer.TryParse(raw.ToString(), idVal)
                End If

                Dim nameVal As String = ""
                If nameProp IsNot Nothing Then
                    Dim raw = nameProp.GetValue(it, Nothing)
                    If raw IsNot Nothing Then nameVal = raw.ToString()
                Else
                    nameVal = it.ToString()
                End If

                list.Add(New ComboItem(idVal, nameVal))
            Catch
            End Try
        Next

        Return list
    End Function

    Private Function IsNumericType(t As Type) As Boolean
        If t Is Nothing Then Return False
        Dim base As Type = Nullable.GetUnderlyingType(t)
        If base Is Nothing Then base = t
        Return {GetType(Byte), GetType(SByte), GetType(Short), GetType(UShort),
                GetType(Integer), GetType(UInteger), GetType(Long), GetType(ULong)}.Contains(base)
    End Function

    ' Helper to select the current record
    Private Sub SelectComboById(cbo As DevExpress.XtraEditors.ComboBoxEdit, id As Integer?)
        If cbo Is Nothing Then Return
        cbo.Properties.Items.BeginUpdate()
        Try
            Dim found As ComboItem = Nothing
            If id.HasValue Then
                For Each obj In cbo.Properties.Items
                    Dim ci = TryCast(obj, ComboItem)
                    If ci IsNot Nothing AndAlso ci.ID = id.Value Then
                        found = ci
                        Exit For
                    End If
                Next
            End If
            If found IsNot Nothing Then
                cbo.SelectedItem = found
            ElseIf cbo.Properties.Items.Count > 0 Then
                cbo.SelectedIndex = 0
            Else
                cbo.SelectedIndex = -1
            End If
        Finally
            cbo.Properties.Items.EndUpdate()
        End Try
    End Sub

    ' Main entry point - call this to initialize all combos
    Private Sub FillCombos()
        If _isFillingCombos Then Return

        Try
            _isFillingCombos = True
            FillAllCombos()
        Catch ex As Exception
            Debug.WriteLine("FillCombos Error: " & ex.Message)
        Finally
            _isFillingCombos = False
        End Try
    End Sub

    ' Initial fill of ALL combos (used on form load)
    Private Sub FillAllCombos()
        ' ====== Group Combo ======
        cboMedGroup.Properties.Items.Clear()
        Dim groupItems = CreateComboItems(Of MedicineGroups)(TryCast(MedicineGroupsBindingSource.List, IEnumerable(Of MedicineGroups)))
        For Each ci In groupItems
            cboMedGroup.Properties.Items.Add(ci)
        Next
        SelectComboById(cboMedGroup, If(CurrentGroup() IsNot Nothing, CType(CurrentGroup().MedicineID, Integer?), Nothing))

        ' ====== Family Combo ======
        UpdateFamilyCombo()

        ' ====== Science Combo ======
        UpdateScienceCombo()

        ' ====== Item Combo ======
        UpdateItemCombo()

        ' ====== Shape Combo ======
        cboShapeInfo.Properties.Items.Clear()
        Dim shapeItems = CreateComboItems(Of MedicineShape)(TryCast(MedicineShapeBindingSource.List, IEnumerable(Of MedicineShape)))
        For Each ci In shapeItems
            cboShapeInfo.Properties.Items.Add(ci)
        Next
        SelectComboById(cboShapeInfo, If(CurrentShape() IsNot Nothing, CType(CurrentShape().ShapeID, Integer?), Nothing))

        ' ====== Dose Combo (if any) ======
        cboDoseInfo.Properties.Items.Clear()
        Dim doseItems = CreateComboItems(Of MedicineDoze)(TryCast(MedicineDozeBindingSource.List, IEnumerable(Of MedicineDoze)))
        For Each ci In doseItems
            cboDoseInfo.Properties.Items.Add(ci)
        Next
        SelectComboById(cboDoseInfo, If(CurrentDoze() IsNot Nothing, CType(CurrentDoze().DozeID, Integer?), Nothing))
    End Sub

    ' Cascade update methods
    Private Sub CascadeUpdateCombos(sourceComboName As String)
        If _isFillingCombos Then Return

        Try
            _isFillingCombos = True

            Select Case sourceComboName
                Case "Group"
                    ' Group changed -> update Family, Science, and Item combos
                    UpdateFamilyCombo()
                    UpdateScienceCombo()
                    UpdateItemCombo()

                Case "Family"
                    ' Family changed -> update Science and Item combos
                    UpdateScienceCombo()
                    UpdateItemCombo()

                Case "Science"
                    ' Science changed -> update Item combo
                    UpdateItemCombo()

                Case "Item"
                    ' Item changed -> potentially update Shape or Dose if needed
                    ' Add logic here if needed
            End Select

        Catch ex As Exception
            Debug.WriteLine("CascadeUpdateCombos Error: " & ex.Message)
        Finally
            _isFillingCombos = False
        End Try
    End Sub

    ' Update Family combo based on selected Group
    Private Sub UpdateFamilyCombo()
        cboMedFamily.Properties.Items.Clear()

        Dim selectedGroup = TryCast(cboMedGroup.SelectedItem, ComboItem)
        If selectedGroup IsNot Nothing Then
            ' Filter families by selected group ID
            Dim filteredFamilies = TryCast(MedicineFamilyBindingSource.List, IEnumerable(Of MedicineFamily)).
                                  Where(Function(f) f.MedicineID = selectedGroup.ID).ToList()

            Dim famItems = CreateComboItems(Of MedicineFamily)(filteredFamilies)
            For Each ci In famItems
                cboMedFamily.Properties.Items.Add(ci)
            Next
        Else
            ' No group selected, show all families or empty
            Dim famItems = CreateComboItems(Of MedicineFamily)(TryCast(MedicineFamilyBindingSource.List, IEnumerable(Of MedicineFamily)))
            For Each ci In famItems
                cboMedFamily.Properties.Items.Add(ci)
            Next
        End If

        ' Select appropriate item
        SelectComboById(cboMedFamily, If(CurrentFamily() IsNot Nothing, CType(CurrentFamily().SubCatID, Integer?), Nothing))
    End Sub

    ' Update Science combo based on selected Family
    Private Sub UpdateScienceCombo()
        cboScinetificName.Properties.Items.Clear()

        Dim selectedFamily = TryCast(cboMedFamily.SelectedItem, ComboItem)
        If selectedFamily IsNot Nothing Then
            ' Filter science families by selected family ID
            Dim filteredScience = TryCast(ScienceFamilyBindingSource.List, IEnumerable(Of MedScienceFamily)).
                                 Where(Function(s) s.SubCatID = selectedFamily.ID).ToList()

            Dim sciItems = CreateComboItems(Of MedScienceFamily)(filteredScience)
            For Each ci In sciItems
                cboScinetificName.Properties.Items.Add(ci)
            Next
        Else
            ' No family selected, show all science families or empty
            Dim sciItems = CreateComboItems(Of MedScienceFamily)(TryCast(ScienceFamilyBindingSource.List, IEnumerable(Of MedScienceFamily)))
            For Each ci In sciItems
                cboScinetificName.Properties.Items.Add(ci)
            Next
        End If

        SelectComboById(cboScinetificName, If(CurrentScience() IsNot Nothing, CType(CurrentScience().ScincID, Integer?), Nothing))
    End Sub

    ' Update Item combo based on selected Science
    Private Sub UpdateItemCombo()
        cboMedItem.Properties.Items.Clear()

        Dim selectedScience = TryCast(cboScinetificName.SelectedItem, ComboItem)
        If selectedScience IsNot Nothing Then
            ' Filter items by selected science ID
            Dim filteredItems = TryCast(MedicineItemsBindingSource.List, IEnumerable(Of MedicineItems)).
                               Where(Function(i) i.ScincID = selectedScience.ID).ToList()

            Dim itemItems = CreateComboItems(Of MedicineItems)(filteredItems)
            For Each ci In itemItems
                cboMedItem.Properties.Items.Add(ci)
            Next
        Else
            ' No science selected, show all items or empty
            Dim itemItems = CreateComboItems(Of MedicineItems)(TryCast(MedicineItemsBindingSource.List, IEnumerable(Of MedicineItems)))
            For Each ci In itemItems
                cboMedItem.Properties.Items.Add(ci)
            Next
        End If

        SelectComboById(cboMedItem, If(CurrentItem() IsNot Nothing, CType(CurrentItem().MedicineItemID, Integer?), Nothing))
    End Sub

#End Region

#Region "Combo Box Event Handlers"

    Private Sub cboMedGroup_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboMedGroup.SelectedIndexChanged
        CascadeUpdateCombos("Group")
    End Sub

    Private Sub cboMedFamily_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboMedFamily.SelectedIndexChanged
        CascadeUpdateCombos("Family")
    End Sub

    Private Sub cboScinetificName_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboScinetificName.SelectedIndexChanged
        CascadeUpdateCombos("Science")
    End Sub

    Private Sub cboMedItem_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboMedItem.SelectedIndexChanged
        CascadeUpdateCombos("Item")
    End Sub

#End Region

    '#Region "Fill Combos (DevExpress ComboBoxEdit)"

    '    ' Reusable item type for all combos
    '    Private Class ComboItem
    '        Public Property ID As Integer
    '        Public Property Name As String
    '        Public Sub New(id As Integer, name As String)
    '            Me.ID = id
    '            Me.Name = name
    '        End Sub
    '        Public Overrides Function ToString() As String
    '            Return Name
    '        End Function
    '    End Class

    '    ' Convert IEnumerable(Of T) to List(Of ComboItem)
    '    Private Function CreateComboItems(Of T)(items As IEnumerable(Of T)) As List(Of ComboItem)
    '        Dim list As New List(Of ComboItem)
    '        If items Is Nothing Then Return list

    '        Dim tt = GetType(T)
    '        Dim props = tt.GetProperties()

    '        Dim idProp As Reflection.PropertyInfo = props.FirstOrDefault(Function(p) p.Name.ToLower().EndsWith("id"))
    '        If idProp Is Nothing Then
    '            idProp = props.FirstOrDefault(Function(p) IsNumericType(p.PropertyType))
    '        End If

    '        Dim nameProp As Reflection.PropertyInfo =
    '            props.FirstOrDefault(Function(p) p.PropertyType Is GetType(String) AndAlso (idProp Is Nothing OrElse p.Name <> idProp.Name))
    '        If nameProp Is Nothing Then
    '            nameProp = props.FirstOrDefault(Function(p) idProp Is Nothing OrElse p.Name <> idProp.Name)
    '        End If

    '        For Each it In items
    '            Try
    '                Dim idVal As Integer = 0
    '                If idProp IsNot Nothing Then
    '                    Dim raw = idProp.GetValue(it, Nothing)
    '                    If raw IsNot Nothing Then Integer.TryParse(raw.ToString(), idVal)
    '                End If

    '                Dim nameVal As String = ""
    '                If nameProp IsNot Nothing Then
    '                    Dim raw = nameProp.GetValue(it, Nothing)
    '                    If raw IsNot Nothing Then nameVal = raw.ToString()
    '                Else
    '                    nameVal = it.ToString()
    '                End If

    '                list.Add(New ComboItem(idVal, nameVal))
    '            Catch
    '            End Try
    '        Next

    '        Return list
    '    End Function

    '    Private Function IsNumericType(t As Type) As Boolean
    '        If t Is Nothing Then Return False
    '        Dim base As Type = Nullable.GetUnderlyingType(t)
    '        If base Is Nothing Then base = t
    '        Return {GetType(Byte), GetType(SByte), GetType(Short), GetType(UShort),
    '                GetType(Integer), GetType(UInteger), GetType(Long), GetType(ULong)}.Contains(base)
    '    End Function

    '    ' Fill combos using BindingSource filtered lists
    '    Private _isFillingCombos As Boolean = False

    '    Private Sub FillCombos()
    '        If _isFillingCombos Then Return
    '        Try
    '            _isFillingCombos = True
    '            ' Helper to select the current record
    '            Dim selectById = Sub(cbo As DevExpress.XtraEditors.ComboBoxEdit, id As Integer?)
    '                                 If cbo Is Nothing Then Return
    '                                 cbo.Properties.Items.BeginUpdate()
    '                                 Try
    '                                     Dim found As ComboItem = Nothing
    '                                     If id.HasValue Then
    '                                         For Each obj In cbo.Properties.Items
    '                                             Dim ci = TryCast(obj, ComboItem)
    '                                             If ci IsNot Nothing AndAlso ci.ID = id.Value Then
    '                                                 found = ci
    '                                                 Exit For
    '                                             End If
    '                                         Next
    '                                     End If
    '                                     If found IsNot Nothing Then
    '                                         cbo.SelectedItem = found
    '                                     ElseIf cbo.Properties.Items.Count > 0 Then
    '                                         cbo.SelectedIndex = 0
    '                                     Else
    '                                         cbo.SelectedIndex = -1
    '                                     End If
    '                                 Finally
    '                                     cbo.Properties.Items.EndUpdate()
    '                                 End Try
    '                             End Sub

    '            ' ====== Group Combo ======
    '            cboMedGroup.Properties.Items.Clear()
    '            Dim groupItems = CreateComboItems(Of MedicineGroups)(TryCast(MedicineGroupsBindingSource.List, IEnumerable(Of MedicineGroups)))
    '            For Each ci In groupItems
    '                cboMedGroup.Properties.Items.Add(ci)
    '            Next
    '            selectById(cboMedGroup, If(CurrentGroup() IsNot Nothing, CType(CurrentGroup().MedicineSubCat, Integer?), Nothing))

    '            ' ====== Family Combo ======
    '            cboMedFamily.Properties.Items.Clear()
    '            Dim famItems = CreateComboItems(Of MedicineFamily)(TryCast(MedicineFamilyBindingSource.List, IEnumerable(Of MedicineFamily)))
    '            For Each ci In famItems
    '                cboMedFamily.Properties.Items.Add(ci)
    '            Next
    '            selectById(cboMedFamily, If(CurrentFamily() IsNot Nothing, CType(CurrentFamily().SubCatID, Integer?), Nothing))

    '            ' ====== Science Combo ======
    '            cboScinetificName.Properties.Items.Clear()
    '            Dim sciItems = CreateComboItems(Of MedScienceFamily)(TryCast(ScienceFamilyBindingSource.List, IEnumerable(Of MedScienceFamily)))
    '            For Each ci In sciItems
    '                cboScinetificName.Properties.Items.Add(ci)
    '            Next
    '            selectById(cboScinetificName, If(CurrentScience() IsNot Nothing, CType(CurrentScience().ScincID, Integer?), Nothing))

    '            ' ====== Item Combo ======
    '            cboMedItem.Properties.Items.Clear()
    '            Dim itemItems = CreateComboItems(Of MedicineItems)(TryCast(MedicineItemsBindingSource.List, IEnumerable(Of MedicineItems)))
    '            For Each ci In itemItems
    '                cboMedItem.Properties.Items.Add(ci)
    '            Next
    '            selectById(cboMedItem, If(CurrentItem() IsNot Nothing, CType(CurrentItem().MedicineItemID, Integer?), Nothing))

    '            ' ====== Shape Combo ======
    '            cboShapeInfo.Properties.Items.Clear()
    '            Dim shapeItems = CreateComboItems(Of MedicineShape)(TryCast(MedicineShapeBindingSource.List, IEnumerable(Of MedicineShape)))
    '            For Each ci In shapeItems
    '                cboShapeInfo.Properties.Items.Add(ci)
    '            Next
    '            selectById(cboShapeInfo, If(CurrentShape() IsNot Nothing, CType(CurrentShape().ShapeID, Integer?), Nothing))

    '            ' ====== Dose Combo (if any) ======
    '            cboDoseInfo.Properties.Items.Clear()
    '            Dim doseItems = CreateComboItems(Of MedicineDoze)(TryCast(MedicineDozeBindingSource.List, IEnumerable(Of MedicineDoze)))
    '            For Each ci In doseItems
    '                cboDoseInfo.Properties.Items.Add(ci)
    '            Next
    '            selectById(cboDoseInfo, If(CurrentDoze() IsNot Nothing, CType(CurrentDoze().DozeID, Integer?), Nothing))

    '        Catch ex As Exception
    '            Debug.WriteLine("FillCombos Error: " & ex.Message)
    '        Finally
    '            _isFillingCombos = False
    '        End Try
    '    End Sub

    '#End Region


#Region "Medicine Groups Buttons"
    'GrpNavigator
    Private Sub btnAddMedGroup_Click(sender As Object, e As EventArgs) Handles btnAddMedGroup.Click
        Try
            Dim name = InputBox("Enter new Medicine Group name:", "Add Medicine Group")
            If String.IsNullOrWhiteSpace(name) Then Return

            Dim cls As New MedicineGroups With {
                .MedicineFamily = name
            }
            If MedGroupsData.Add(cls) Then
                RefreshMedGroups()
                ' reposition to newly added if possible
                Dim added = MedGroups.OrderByDescending(Function(x) x.MedicineID).FirstOrDefault()
                If added IsNot Nothing Then
                    MedicineGroupsBindingSource.Position = MedGroups.ToList().FindIndex(Function(x) x.MedicineID = added.MedicineID)
                End If
                ' cascade refresh
                FilterFamilyByGroup()
            Else
                MsgBox("Failed to add Medicine Group.")
            End If
        Catch ex As Exception
            MsgBox("Error adding Medicine Group: " & ex.Message)
        End Try
    End Sub

    Private Sub btnEditMedGroup_Click(sender As Object, e As EventArgs) Handles btnEditMedGroup.Click
        Dim cur = CurrentGroup()
        If cur Is Nothing Then Return

        Dim newName = InputBox("Edit Medicine Group name:", "Edit Medicine Group", cur.MedicineFamily)
        If String.IsNullOrWhiteSpace(newName) Then Return

        Try
            ' create old and new copies for Update(old, new)
            Dim oldObj As New MedicineGroups With {
                .MedicineID = cur.MedicineID,
                .MedicineFamily = cur.MedicineFamily
            }
            Dim newObj As New MedicineGroups With {
                .MedicineID = cur.MedicineID,
                .MedicineFamily = newName
            }

            If MedGroupsData.Update(oldObj, newObj) Then
                RefreshMedGroups()
                FilterFamilyByGroup()
            Else
                MsgBox("Failed to update Medicine Group.")
            End If
        Catch ex As Exception
            MsgBox("Error updating Medicine Group: " & ex.Message)
        End Try
    End Sub

    Private Sub btnDelMedGroup_Click(sender As Object, e As EventArgs) Handles btnDelMedGroup.Click
        Dim cur = CurrentGroup()
        If cur Is Nothing Then Return

        If MessageBox.Show("Delete selected Medicine Group? This will affect child records.", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) <> DialogResult.Yes Then
            Return
        End If

        Try
            If MedGroupsData.Delete(cur) Then
                RefreshMedGroups()
                FilterFamilyByGroup()
            Else
                MsgBox("Failed to delete Medicine Group.")
            End If
        Catch ex As Exception
            MsgBox("Error deleting Medicine Group: " & ex.Message)
        End Try
    End Sub
#End Region

#Region "Medicine Family Buttons"
    'FamilyNavigator
    'Private Sub cboMedGroup_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboMedGroup.SelectedIndexChanged
    '    FilterFamilyByGroup()
    'End Sub

    Private Sub btnAddMedFamily_Click(sender As Object, e As EventArgs) Handles btnAddMedFamily.Click
        Dim gid = GetSelectedGroupID()
        If gid Is Nothing Then
            MsgBox("Please select a Medicine Group first.")
            Return
        End If

        Dim name = InputBox("Enter new Medicine Family (Sub Category) name:", "Add Medicine Family")
        If String.IsNullOrWhiteSpace(name) Then Return

        Dim cls As New MedicineFamily With {
            .MedicineID = gid.Value,
            .MedicineSubCat = name
        }
        Try
            If MedFamilyData.Add(cls) Then
                RefreshMedFamily()
                Dim added = MedFamily.OrderByDescending(Function(x) x.SubCatID).FirstOrDefault()
                If added IsNot Nothing Then
                    MedicineFamilyBindingSource.Position = MedFamily.ToList().FindIndex(Function(x) x.SubCatID = added.SubCatID)
                End If
                FilterScienceByFamily()
            Else
                MsgBox("Failed to add Medicine Family.")
            End If
        Catch ex As Exception
            MsgBox("Error adding Medicine Family: " & ex.Message)
        End Try
    End Sub

    Private Sub btnEditMedFamily_Click(sender As Object, e As EventArgs) Handles btnEditMedFamily.Click
        Dim cur = CurrentFamily()
        If cur Is Nothing Then Return

        Dim newName = InputBox("Edit Medicine Family (Sub Category) name:", "Edit Medicine Family", cur.MedicineSubCat)
        If String.IsNullOrWhiteSpace(newName) Then Return

        Try
            Dim oldObj As New MedicineFamily With {
                .SubCatID = cur.SubCatID,
                .MedicineID = cur.MedicineID,
                .MedicineSubCat = cur.MedicineSubCat
            }
            Dim newObj As New MedicineFamily With {
                .SubCatID = cur.SubCatID,
                .MedicineID = cur.MedicineID,
                .MedicineSubCat = newName
            }

            If MedFamilyData.Update(oldObj, newObj) Then
                RefreshMedFamily()
                FilterScienceByFamily()
            Else
                MsgBox("Failed to update Medicine Family.")
            End If
        Catch ex As Exception
            MsgBox("Error updating Medicine Family: " & ex.Message)
        End Try
    End Sub

    Private Sub btnDelMedFamily_Click(sender As Object, e As EventArgs) Handles btnDelMedFamily.Click
        Dim cur = CurrentFamily()
        If cur Is Nothing Then Return

        If MessageBox.Show("Delete selected Medicine Family? This will affect child records.", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) <> DialogResult.Yes Then
            Return
        End If

        Try
            If MedFamilyData.Delete(cur) Then
                RefreshMedFamily()
                FilterScienceByFamily()
            Else
                MsgBox("Failed to delete Medicine Family.")
            End If
        Catch ex As Exception
            MsgBox("Error deleting Medicine Family: " & ex.Message)
        End Try
    End Sub
#End Region

#Region "Medicine Science Family Buttons"
    'ScienceNavigator
    'Private Sub cboMedFamily_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboMedFamily.SelectedIndexChanged
    '    FilterScienceByFamily()
    'End Sub

    Private Sub btnAddMedScienFamily_Click(sender As Object, e As EventArgs) Handles btnAddMedScienFamily.Click
        Dim fid = GetSelectedFamilyID()
        If fid Is Nothing Then
            MsgBox("Please select a Medicine Family first.")
            Return
        End If

        Dim name = InputBox("Enter new Science Family name:", "Add Science Family")
        If String.IsNullOrWhiteSpace(name) Then Return

        Dim cls As New MedScienceFamily With {
            .SubCatID = fid.Value,
            .ScienceName = name
        }
        Try
            If MedScinFamilyData.Add(cls) Then
                RefreshMedScienceFamily()
                Dim added = MedScinFamily.OrderByDescending(Function(x) x.ScincID).FirstOrDefault()
                If added IsNot Nothing Then
                    ScienceFamilyBindingSource.Position = MedScinFamily.ToList().FindIndex(Function(x) x.ScincID = added.ScincID)
                End If
                FilterItemsByScience()
            Else
                MsgBox("Failed to add Science Family.")
            End If
        Catch ex As Exception
            MsgBox("Error adding Science Family: " & ex.Message)
        End Try
    End Sub

    Private Sub btnEditMedScienFamily_Click(sender As Object, e As EventArgs) Handles btnEditMedScienFamily.Click
        Dim cur = CurrentScience()
        If cur Is Nothing Then Return

        Dim newName = InputBox("Edit Science Family name:", "Edit Science Family", cur.ScienceName)
        If String.IsNullOrWhiteSpace(newName) Then Return

        Try
            Dim oldObj As New MedScienceFamily With {
                .ScincID = cur.ScincID,
                .SubCatID = cur.SubCatID,
                .ScienceName = cur.ScienceName
            }
            Dim newObj As New MedScienceFamily With {
                .ScincID = cur.ScincID,
                .SubCatID = cur.SubCatID,
                .ScienceName = newName
            }

            If MedScinFamilyData.Update(oldObj, newObj) Then
                RefreshMedScienceFamily()
                FilterItemsByScience()
            Else
                MsgBox("Failed to update Science Family.")
            End If
        Catch ex As Exception
            MsgBox("Error updating Science Family: " & ex.Message)
        End Try
    End Sub

    Private Sub btnDelMedScienFamily_Click(sender As Object, e As EventArgs) Handles btnDelMedScienFamily.Click
        Dim cur = CurrentScience()
        If cur Is Nothing Then Return

        If MessageBox.Show("Delete selected Science Family? This will affect child items.", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) <> DialogResult.Yes Then
            Return
        End If

        Try
            If MedScinFamilyData.Delete(cur) Then
                RefreshMedScienceFamily()
                FilterItemsByScience()
            Else
                MsgBox("Failed to delete Science Family.")
            End If
        Catch ex As Exception
            MsgBox("Error deleting Science Family: " & ex.Message)
        End Try
    End Sub
#End Region

#Region "Medicine Scinetific Items Name Buttons"
    'ItemsNavigator
    'Private Sub cboScinetificName_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboScinetificName.SelectedIndexChanged, cboDoseInfo.SelectedIndexChanged
    '    FilterItemsByScience()
    'End Sub

    Private Sub btnAddScinetificName_Click(sender As Object, e As EventArgs) Handles btnAddScinetificName.Click
        Dim sid = GetSelectedScienceID()
        If sid Is Nothing Then
            MsgBox("Please select a Science Family first.")
            Return
        End If

        Dim name = InputBox("Enter new Medicine Item (Common Name):", "Add Medicine Item")
        If String.IsNullOrWhiteSpace(name) Then Return

        Dim cls As New MedicineItems With {
            .ScincID = sid.Value,
            .CommName = name,
            .Company = String.Empty,
            .Notes = String.Empty
        }
        Try
            If MedItemsData.Add(cls) Then
                RefreshMedItems()
                Dim added = MedItems.OrderByDescending(Function(x) x.MedicineItemID).FirstOrDefault()
                If added IsNot Nothing Then
                    MedicineItemsBindingSource.Position = MedItems.ToList().FindIndex(Function(x) x.MedicineItemID = added.MedicineItemID)
                End If
                FilterShapeByItem()
            Else
                MsgBox("Failed to add Medicine Item.")
            End If
        Catch ex As Exception
            MsgBox("Error adding Medicine Item: " & ex.Message)
        End Try
    End Sub

    Private Sub btnEditScinetificName_Click(sender As Object, e As EventArgs) Handles btnEditScinetificName.Click
        Dim cur = CurrentItem()
        If cur Is Nothing Then Return

        Dim newName = InputBox("Edit Medicine Item (Common Name):", "Edit Medicine Item", cur.CommName)
        If String.IsNullOrWhiteSpace(newName) Then Return

        Try
            Dim oldObj As New MedicineItems With {
                .MedicineItemID = cur.MedicineItemID,
                .ScincID = cur.ScincID,
                .CommName = cur.CommName,
                .Company = cur.Company,
                .Notes = cur.Notes
            }
            Dim newObj As New MedicineItems With {
                .MedicineItemID = cur.MedicineItemID,
                .ScincID = cur.ScincID,
                .CommName = newName,
                .Company = cur.Company,
                .Notes = cur.Notes
            }

            If MedItemsData.Update(oldObj, newObj) Then
                RefreshMedItems()
                FilterShapeByItem()
            Else
                MsgBox("Failed to update Medicine Item.")
            End If
        Catch ex As Exception
            MsgBox("Error updating Medicine Item: " & ex.Message)
        End Try
    End Sub

    Private Sub btnDelScinetificName_Click(sender As Object, e As EventArgs) Handles btnDelScinetificName.Click
        Dim cur = CurrentItem()
        If cur Is Nothing Then Return

        If MessageBox.Show("Delete selected Medicine Item? This will affect shapes/doses.", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) <> DialogResult.Yes Then
            Return
        End If

        Try
            If MedItemsData.Delete(cur) Then
                RefreshMedItems()
                FilterShapeByItem()
            Else
                MsgBox("Failed to delete Medicine Item.")
            End If
        Catch ex As Exception
            MsgBox("Error deleting Medicine Item: " & ex.Message)
        End Try
    End Sub

#End Region

#Region "Medicine Shapes Buttons"
    'ShapeNavigator
    'Private Sub cboMedItem_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboMedItem.SelectedIndexChanged
    '    FilterShapeByItem()
    'End Sub

    Private Sub btnAddShape_Click(sender As Object, e As EventArgs) Handles btnAddShape.Click
        Dim iid = GetSelectedItemID()
        If iid Is Nothing Then
            MsgBox("Please select a Medicine Item first.")
            Return
        End If

        Dim name = InputBox("Enter new Shape description:", "Add Shape")
        If String.IsNullOrWhiteSpace(name) Then Return

        Dim cls As New MedicineShape With {
            .MedicineItemID = iid.Value,
            .MedicineShape = name,
            .ShapeInfo = String.Empty
        }
        Try
            If MedShapeData.Add(cls) Then
                RefreshMedShape()
                Dim added = MedShape.OrderByDescending(Function(x) x.ShapeID).FirstOrDefault()
                If added IsNot Nothing Then
                    MedicineShapeBindingSource.Position = MedShape.ToList().FindIndex(Function(x) x.ShapeID = added.ShapeID)
                End If
                FilterDozeByShape()
            Else
                MsgBox("Failed to add Shape.")
            End If
        Catch ex As Exception
            MsgBox("Error adding Shape: " & ex.Message)
        End Try
    End Sub

    Private Sub btnEditShape_Click(sender As Object, e As EventArgs) Handles btnEditShape.Click
        Dim cur = CurrentShape()
        If cur Is Nothing Then Return

        Dim newName = InputBox("Edit Shape info:", "Edit Shape", cur.MedicineShape)
        If String.IsNullOrWhiteSpace(newName) Then Return

        Try
            Dim oldObj As New MedicineShape With {
                .ShapeID = cur.ShapeID,
                .MedicineItemID = cur.MedicineItemID,
                .MedicineShape = cur.MedicineShape,
                .ShapeInfo = cur.ShapeInfo
            }
            Dim newObj As New MedicineShape With {
                .ShapeID = cur.ShapeID,
                .MedicineItemID = cur.MedicineItemID,
                .MedicineShape = newName,
                .ShapeInfo = cur.ShapeInfo
            }

            If MedShapeData.Update(oldObj, newObj) Then
                RefreshMedShape()
                FilterDozeByShape()
            Else
                MsgBox("Failed to update Shape.")
            End If
        Catch ex As Exception
            MsgBox("Error updating Shape: " & ex.Message)
        End Try
    End Sub

    Private Sub btnDelShape_Click(sender As Object, e As EventArgs) Handles btnDelShape.Click
        Dim cur = CurrentShape()
        If cur Is Nothing Then Return

        If MessageBox.Show("Delete selected Shape? This will affect doses.", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) <> DialogResult.Yes Then
            Return
        End If

        Try
            If MedShapeData.Delete(cur) Then
                RefreshMedShape()
                FilterDozeByShape()
            Else
                MsgBox("Failed to delete Shape.")
            End If
        Catch ex As Exception
            MsgBox("Error deleting Shape: " & ex.Message)
        End Try
    End Sub
#End Region

#Region "Medicine Dose Buttons"
    'DoseNavigator
    'Private Sub cboShapeInfo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboShapeInfo.SelectedIndexChanged
    '    FilterDozeByShape()
    'End Sub

    Private Sub btnAddDose_Click(sender As Object, e As EventArgs) Handles btnAddDose.Click
        Dim shid = GetSelectedShapeID()
        If shid Is Nothing Then
            MsgBox("Please select a Shape first.")
            Return
        End If

        Dim name = InputBox("Enter dose description:", "Add Dose")
        If String.IsNullOrWhiteSpace(name) Then Return

        Dim cls As New MedicineDoze With {
            .ShapeID = shid.Value,
            .Doze = name
        }
        Try
            If MedDozeData.Add(cls) Then
                RefreshMedDoze()
                Dim added = MedDoze.OrderByDescending(Function(x) x.DozeID).FirstOrDefault()
                If added IsNot Nothing Then
                    MedicineDozeBindingSource.Position = MedDoze.ToList().FindIndex(Function(x) x.DozeID = added.DozeID)
                End If
            Else
                MsgBox("Failed to add Dose.")
            End If
        Catch ex As Exception
            MsgBox("Error adding Dose: " & ex.Message)
        End Try
    End Sub

    Private Sub btnEditDose_Click(sender As Object, e As EventArgs) Handles btnEditDose.Click
        Dim cur = CurrentDoze()
        If cur Is Nothing Then Return

        Dim newName = InputBox("Edit Dose description:", "Edit Dose", cur.Doze)
        If String.IsNullOrWhiteSpace(newName) Then Return

        Try
            Dim oldObj As New MedicineDoze With {
                .DozeID = cur.DozeID,
                .ShapeID = cur.ShapeID,
                .Doze = cur.Doze
            }
            Dim newObj As New MedicineDoze With {
                .DozeID = cur.DozeID,
                .ShapeID = cur.ShapeID,
                .Doze = newName
            }

            If MedDozeData.Update(oldObj, newObj) Then
                RefreshMedDoze()
            Else
                MsgBox("Failed to update Dose.")
            End If
        Catch ex As Exception
            MsgBox("Error updating Dose: " & ex.Message)
        End Try
    End Sub

    Private Sub btnDelDose_Click(sender As Object, e As EventArgs) Handles btnDelDose.Click
        Dim cur = CurrentDoze()
        If cur Is Nothing Then Return

        If MessageBox.Show("Delete selected Dose?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) <> DialogResult.Yes Then
            Return
        End If

        Try
            If MedDozeData.Delete(cur) Then
                RefreshMedDoze()
            Else
                MsgBox("Failed to delete Dose.")
            End If
        Catch ex As Exception
            MsgBox("Error deleting Dose: " & ex.Message)
        End Try
    End Sub



    Private Sub MedicineGroupsCombo1_MedicineGroupsValueChanged(sender As Object, e As MedicineGroupsCombo.MedicineGroupsIndexChangedEvent) Handles MedicineGroupsCombo1.MedicineGroupsValueChanged
        MedicineFamilyCombo1.ParentMedicineID = e.MedicineID
    End Sub

    Private Sub MedicineFamilyCombo1_MedicineFamilyValueChanged(sender As Object, e As MedicineFamilyCombo.MedicineFamilyIndexChangedEvent) Handles MedicineFamilyCombo1.MedicineFamilyValueChanged
        MedScienceFamilyCombo1.ParentSubCatID = e.SubCatID
    End Sub

    Private Sub MedScienceFamilyCombo1_MedScienceFamilyValueChanged(sender As Object, e As MedScienceFamilyCombo.MedScienceFamilyIndexChangedEvent) Handles MedScienceFamilyCombo1.MedScienceFamilyValueChanged
        MedicineItemsCombo1.ParentScincID = e.ScincID
    End Sub

    Private Sub MedicineItemsCombo1_MedicineItemsValueChanged(sender As Object, e As MedicineItemsCombo.MedicineItemsIndexChangedEvent) Handles MedicineItemsCombo1.MedicineItemsValueChanged
        MedicineShapeCombo1.ParentMedicineItemID = e.MedicineItemID
    End Sub
    Private Sub MedicineShapeCombo1_MedicineShapeValueChanged(sender As Object, e As MedicineShapeCombo.MedicineShapeIndexChangedEvent) Handles MedicineShapeCombo1.MedicineShapeValueChanged
        MedicineDozeCombo1.ParentShapeID = e.ShapeID
    End Sub


#End Region

End Class


'Public Class MedicForm

'#Region "Calss Variables"
'    Private MedGroups As IEnumerable(Of MedicineGroups)
'    Private MedFamily As IEnumerable(Of MedicineFamily)
'    Private MedScinFamily As IEnumerable(Of MedScienceFamily)
'    Private MedItems As IEnumerable(Of MedicineItems)
'    Private MedShape As IEnumerable(Of MedicineShape)
'    Private MedDoze As IEnumerable(Of MedicineDoze)

'    Private MedGroupsData As New MedicineGroupsDATA
'    Private MedFamilyData As New MedicineFamilyDATA
'    Private MedScinFamilyData As New MedScienceFamilyDATA
'    Private MedItemsData As New MedicineItemsDATA
'    Private MedShapeData As New MedicineShapeDATA
'    Private MedDozeData As New MedicineDozeDATA
'    '=====================================================
'    Public Sub loadClasses(patientId As Integer)
'        Dim sw As New Stopwatch
'        sw.Start()
'        Try
'            '6 Tables
'            'MedicineGroups
'            MedGroups = MedGroupsData.SelectAll
'            ' Ensure MedicineGroupsBindingSource is initialized
'            If MedicineGroupsBindingSource Is Nothing Then
'                MedicineGroupsBindingSource = New BindingSource()
'            End If
'            ' Set the DataSource
'            MedicineGroupsBindingSource.DataSource = MedGroups.ToList()
'            'MedicineFamily 
'            MedFamily = MedFamilyData.SelectAll
'            ' Ensure MedicineFamilyBindingSource is initialized
'            If MedicineFamilyBindingSource Is Nothing Then
'                MedicineFamilyBindingSource = New BindingSource()
'            End If
'            ' Set the DataSource
'            MedicineFamilyBindingSource.DataSource = MedFamily.ToList()
'            'MedScienceFamily 
'            MedScinFamily = MedScinFamilyData.SelectAll
'            ' Ensure ScienceFamilyBindingSource is initialized
'            If ScienceFamilyBindingSource Is Nothing Then
'                ScienceFamilyBindingSource = New BindingSource()
'            End If
'            ' Set the DataSource
'            ScienceFamilyBindingSource.DataSource = MedScinFamily.ToList()
'            'MedicineItems 
'            MedItems = MedItemsData.SelectAll
'            ' Ensure MedicineItemsBindingSource is initialized
'            If MedicineItemsBindingSource Is Nothing Then
'                MedicineItemsBindingSource = New BindingSource()
'            End If
'            ' Set the DataSource
'            MedicineItemsBindingSource.DataSource = MedItems.ToList()
'            'MedicineShape 
'            MedShape = MedShapeData.SelectAll
'            ' Ensure MedicineShapeBindingSource is initialized
'            If MedicineShapeBindingSource Is Nothing Then
'                MedicineShapeBindingSource = New BindingSource()
'            End If
'            ' Set the DataSource
'            MedicineShapeBindingSource.DataSource = MedShape.ToList()
'            'MedicineDoze 
'            MedDoze = MedDozeData.SelectAll
'            ' Ensure MedicineDozeBindingSource is initialized
'            If MedicineDozeBindingSource Is Nothing Then
'                MedicineDozeBindingSource = New BindingSource()
'            End If
'            ' Set the DataSource
'            MedicineDozeBindingSource.DataSource = MedDoze.ToList()
'        Catch ex As System.Data.SqlClient.SqlException
'            MsgBox(ex.Message)
'        End Try
'        sw.Stop()
'        LogToFile("loadClasses Time spent: " & sw.Elapsed.TotalSeconds.ToString("F2") & " seconds", Me)
'    End Sub
'#End Region
'#Region "Medicine Groups Buttons"
'    'GrpNavigator
'    Private Sub btnAddMedGroup_Click(sender As Object, e As EventArgs) Handles btnAddMedGroup.Click

'    End Sub
'    Private Sub btnEditMedGroup_Click(sender As Object, e As EventArgs) Handles btnEditMedGroup.Click

'    End Sub
'    Private Sub btnDelMedGroup_Click(sender As Object, e As EventArgs) Handles btnDelMedGroup.Click

'    End Sub
'#End Region
'#Region "Medicine Family Buttons"
'    'FamilyNavigator
'    Private Sub cboMedGroup_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboMedGroup.SelectedIndexChanged

'    End Sub
'    Private Sub btnAddMedFamily_Click(sender As Object, e As EventArgs) Handles btnAddMedFamily.Click

'    End Sub
'    Private Sub btnEditMedFamily_Click(sender As Object, e As EventArgs) Handles btnEditMedFamily.Click

'    End Sub
'    Private Sub btnDelMedFamily_Click(sender As Object, e As EventArgs) Handles btnDelMedFamily.Click

'    End Sub
'#End Region
'#Region "Medicine Science Family Buttons"
'    'ScienceNavigator
'    Private Sub cboMedFamily_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboMedFamily.SelectedIndexChanged

'    End Sub
'    Private Sub btnAddMedScienFamily_Click(sender As Object, e As EventArgs) Handles btnAddMedScienFamily.Click

'    End Sub
'    Private Sub btnEditMedScienFamily_Click(sender As Object, e As EventArgs) Handles btnEditMedScienFamily.Click

'    End Sub
'    Private Sub btnDelMedScienFamily_Click(sender As Object, e As EventArgs) Handles btnDelMedScienFamily.Click

'    End Sub
'#End Region
'#Region "Medicine Scinetific Items Name Buttons"
'    'ItemsNavigator
'    Private Sub cboScinetificName_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboScinetificName.SelectedIndexChanged

'    End Sub
'    Private Sub btnAddScinetificName_Click(sender As Object, e As EventArgs) Handles btnAddScinetificName.Click

'    End Sub
'    Private Sub btnEditScinetificName_Click(sender As Object, e As EventArgs) Handles btnEditScinetificName.Click

'    End Sub
'    Private Sub btnDelScinetificName_Click(sender As Object, e As EventArgs) Handles btnDelScinetificName.Click

'    End Sub

'#End Region
'#Region "Medicine Shapes Buttons"
'    'ShapeNavigator
'    Private Sub cboMedItem_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboMedItem.SelectedIndexChanged

'    End Sub
'    Private Sub btnAddShape_Click(sender As Object, e As EventArgs) Handles btnAddShape.Click

'    End Sub
'    Private Sub btnEditShape_Click(sender As Object, e As EventArgs) Handles btnEditShape.Click

'    End Sub
'    Private Sub btnDelShape_Click(sender As Object, e As EventArgs) Handles btnDelShape.Click

'    End Sub
'#End Region
'#Region "Medicine Dose Buttons"
'    'DoseNavigator
'    Private Sub cboShapeInfo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboShapeInfo.SelectedIndexChanged

'    End Sub
'    Private Sub btnAddDose_Click(sender As Object, e As EventArgs) Handles btnAddDose.Click

'    End Sub
'    Private Sub btnEditDose_Click(sender As Object, e As EventArgs) Handles btnEditDose.Click

'    End Sub
'    Private Sub btnDelDose_Click(sender As Object, e As EventArgs) Handles btnDelDose.Click

'    End Sub
'#End Region

'End Class