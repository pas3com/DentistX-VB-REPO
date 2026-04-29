Imports System.Collections
Imports System.ComponentModel
Imports System.Data.SqlClient
Imports System.Linq
Imports System.Windows.Forms
Imports DevExpress.Utils
Imports DevExpress.XtraEditors

Public Class GenericComboBox
    Implements INotifyPropertyChanged

    Private _bindingSource As New BindingSource()
    Private _dataSource As Object
    Private _idField As String = "ID"
    Private _nameField As String = "Name"
    Private _displayMember As String = "Name"
    Private _valueMember As String = "ID"

    Private _allItems As New List(Of ComboBoxItem)()
    Private _nameFilter As String = String.Empty
    Private _applyingFilter As Boolean
    Private _suppressSearchTextChanged As Boolean

    Public Event ValueChanged(ByVal sender As Object, ByVal e As GenericComboBoxIndexChangedEvent)

    Private _selectedID As Integer
    Public Property SelectedID As Integer
        Get
            Return _selectedID
        End Get
        Set(value As Integer)
            If Not Equals(_selectedID, value) Then
                _selectedID = value
                OnPropertyChanged(NameOf(SelectedID))
                UpdateComboBoxSelection(value)
            End If
        End Set
    End Property

    Private _selectedName As String
    Public Property SelectedName As String
        Get
            Return _selectedName
        End Get
        Set(value As String)
            If Not Equals(_selectedName, value) Then
                _selectedName = value
                OnPropertyChanged(NameOf(SelectedName))
            End If
        End Set
    End Property

    Public Property DataSource As Object
        Get
            Return _dataSource
        End Get
        Set(value As Object)
            If _dataSource IsNot value Then
                _dataSource = value
                BindComboBox()
            End If
        End Set
    End Property

    Public Property IDField As String
        Get
            Return _idField
        End Get
        Set(value As String)
            If _idField <> value Then
                _idField = value
                BindComboBox()
            End If
        End Set
    End Property

    Public Property NameField As String
        Get
            Return _nameField
        End Get
        Set(value As String)
            If _nameField <> value Then
                _nameField = value
                BindComboBox()
            End If
        End Set
    End Property

    Public Property DisplayMember As String
        Get
            Return _displayMember
        End Get
        Set(value As String)
            If _displayMember <> value Then
                _displayMember = value
                If CboGeneric.Properties.Items.Count > 0 Then
                    BindComboBox()
                End If
            End If
        End Set
    End Property

    Public Property ValueMember As String
        Get
            Return _valueMember
        End Get
        Set(value As String)
            If _valueMember <> value Then
                _valueMember = value
                If CboGeneric.Properties.Items.Count > 0 Then
                    BindComboBox()
                End If
            End If
        End Set
    End Property

    Public ReadOnly Property ComboBoxBindingSource As BindingSource
        Get
            Return _bindingSource
        End Get
    End Property

    Private _btnAddVisible As Boolean = True
    Private _btnSearchVisible As Boolean = True

    <Browsable(True), Category("Behavior"), Description("Shows or hides the add (+) button; adjusts layout and width when Dock is None.")>
    Public Property BtnAddVisible As Boolean
        Get
            Return _btnAddVisible
        End Get
        Set(value As Boolean)
            If _btnAddVisible <> value Then
                _btnAddVisible = value
                ApplyToolbarLayout()
                OnPropertyChanged(NameOf(BtnAddVisible))
            End If
        End Set
    End Property

    <Browsable(True), Category("Behavior"), Description("Shows or hides the search (flyout) button; adjusts layout and width when Dock is None.")>
    Public Property BtnSearchVisible As Boolean
        Get
            Return _btnSearchVisible
        End Get
        Set(value As Boolean)
            If _btnSearchVisible <> value Then
                _btnSearchVisible = value
                ApplyToolbarLayout()
                OnPropertyChanged(NameOf(BtnSearchVisible))
            End If
        End Set
    End Property

    Private Sub ApplyToolbarLayout()
        ComboToolbarLayoutHelper.ApplyPanelToolbar(PanelControl1, btnSerach, BtnAddItem, _btnSearchVisible, _btnAddVisible, Me)
    End Sub

    <Browsable(True), Category("Data"), Description("Optional substring on display name (case-insensitive). Combined with flyout search (AND).")>
    Public Property Filter As String
        Get
            Return _nameFilter
        End Get
        Set(value As String)
            _nameFilter = If(value, String.Empty)
            ApplyItemFilter()
        End Set
    End Property

    Private Function MatchesFilters(displayName As String, searchContains As String, externalContains As String) As Boolean
        If String.IsNullOrEmpty(displayName) Then Return False
        If Not String.IsNullOrEmpty(externalContains) AndAlso displayName.IndexOf(externalContains, StringComparison.OrdinalIgnoreCase) < 0 Then Return False
        If Not String.IsNullOrEmpty(searchContains) AndAlso displayName.IndexOf(searchContains, StringComparison.OrdinalIgnoreCase) < 0 Then Return False
        Return True
    End Function

    Private Sub ClearSearchBox()
        If txtSearch Is Nothing Then Return
        _suppressSearchTextChanged = True
        Try
            txtSearch.Text = String.Empty
        Finally
            _suppressSearchTextChanged = False
        End Try
    End Sub

    Private Sub ApplyItemFilter()
        If CboGeneric Is Nothing OrElse _applyingFilter Then Return
        _applyingFilter = True
        Dim searchTyped As String = If(txtSearch IsNot Nothing, txtSearch.Text, String.Empty).Trim()
        Dim external As String = _nameFilter.Trim()
        Dim filtered = _allItems.Where(Function(i) MatchesFilters(i.Name, searchTyped, external)).ToList()
        Dim preserveId As Integer = _selectedID

        Try
            RemoveHandler CboGeneric.SelectedIndexChanged, AddressOf CboGeneric_SelectedIndexChanged

            CboGeneric.Properties.Items.Clear()
            For Each it As ComboBoxItem In filtered
                CboGeneric.Properties.Items.Add(New ComboBoxItem With {.ID = it.ID, .Name = it.Name})
            Next

            Dim keepItem As ComboBoxItem = Nothing
            If preserveId > 0 Then
                For Each item As ComboBoxItem In CboGeneric.Properties.Items
                    If item.ID = preserveId Then
                        keepItem = item
                        Exit For
                    End If
                Next
            End If

            If keepItem IsNot Nothing Then
                CboGeneric.SelectedItem = keepItem
                _selectedID = keepItem.ID
                _selectedName = keepItem.Name
            ElseIf String.IsNullOrWhiteSpace(searchTyped) AndAlso String.IsNullOrWhiteSpace(external) AndAlso CboGeneric.Properties.Items.Count > 0 Then
                CboGeneric.SelectedIndex = 0
                Dim first As ComboBoxItem = TryCast(CboGeneric.SelectedItem, ComboBoxItem)
                If first IsNot Nothing Then
                    _selectedID = first.ID
                    _selectedName = first.Name
                End If
            Else
                CboGeneric.EditValue = Nothing
                _selectedID = 0
                _selectedName = Nothing
            End If

            SetTextEditStyle(CboGeneric)
        Finally
            AddHandler CboGeneric.SelectedIndexChanged, AddressOf CboGeneric_SelectedIndexChanged
            _applyingFilter = False
        End Try
    End Sub

    Private Sub BindComboBox()
        If _dataSource Is Nothing Then Return

        _bindingSource.DataSource = _dataSource
        _allItems.Clear()

        If TypeOf _dataSource Is DataTable Then
            BindFromDataTable()
        ElseIf TypeOf _dataSource Is IEnumerable Then
            BindFromEnumerable(DirectCast(_dataSource, IEnumerable))
        End If

        ApplyItemFilter()
    End Sub

    Private Sub BindFromDataTable()
        Dim dataTable As DataTable = DirectCast(_dataSource, DataTable)
        For Each row As DataRow In dataTable.Rows
            Dim id As Integer = Convert.ToInt32(row(_idField))
            Dim name As String = row(_nameField).ToString()
            _allItems.Add(New ComboBoxItem With {.ID = id, .Name = name})
        Next
    End Sub

    Private Sub BindFromEnumerable(items As IEnumerable)
        For Each item In items
            Dim itemType As Type = item.GetType()
            Dim idProp = itemType.GetProperty(_idField)
            Dim nameProp = itemType.GetProperty(_nameField)
            If idProp IsNot Nothing AndAlso nameProp IsNot Nothing Then
                Dim id As Integer = Convert.ToInt32(idProp.GetValue(item))
                Dim name As String = nameProp.GetValue(item).ToString()
                _allItems.Add(New ComboBoxItem With {.ID = id, .Name = name})
            End If
        Next
    End Sub

    Private Sub UpdateComboBoxSelection(id As Integer)
        ClearSearchBox()
        If id <= 0 Then
            CboGeneric.EditValue = Nothing
            _selectedName = Nothing
            ApplyItemFilter()
            Return
        End If
        Dim row = _allItems.FirstOrDefault(Function(i) i.ID = id)
        If row Is Nothing Then
            CboGeneric.EditValue = Nothing
            _selectedName = Nothing
            ApplyItemFilter()
            Return
        End If
        ApplyItemFilter()
        For Each item As ComboBoxItem In CboGeneric.Properties.Items
            If item.ID = id Then
                CboGeneric.SelectedItem = item
                _selectedName = item.Name
                Return
            End If
        Next
        CboGeneric.EditValue = Nothing
        _selectedName = Nothing
    End Sub

    Private Sub CboGeneric_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CboGeneric.SelectedIndexChanged
        If _applyingFilter Then Return
        Dim selectedItem As ComboBoxItem = TryCast(CboGeneric.SelectedItem, ComboBoxItem)
        If selectedItem IsNot Nothing Then
            SelectedID = selectedItem.ID
            SelectedName = selectedItem.Name
            RaiseEvent ValueChanged(sender, New GenericComboBoxIndexChangedEvent(SelectedID, SelectedName))
        End If
    End Sub

    Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged
        If _suppressSearchTextChanged OrElse _applyingFilter Then Return
        ApplyItemFilter()
    End Sub

    Private Sub Flyout1_Showing(sender As Object, e As FlyoutPanelEventArgs) Handles Flyout1.Showing
        ComboFlyoutSearchHelper.ApplyManualPositionBelowPanel(Flyout1, PanelControl2, CboGeneric)
    End Sub

    Private Sub btnSerach_Click(sender As Object, e As EventArgs) Handles btnSerach.Click
        ComboFlyoutSearchHelper.ShowFlyoutSearchDeferred(Flyout1, PanelControl2, CboGeneric, txtSearch, Me)
    End Sub

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles BtnAddItem.Click
        OnAddButtonClick()
    End Sub

    Protected Overridable Sub OnAddButtonClick()
    End Sub

    Private Sub SetTextEditStyle(ByVal comboBox As ComboBoxEdit)
        comboBox.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard
    End Sub

    Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged
    Protected Overridable Sub OnPropertyChanged(ByVal propertyName As String)
        RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(propertyName))
    End Sub

    Public Sub New()
        InitializeComponent()
        ApplyToolbarLayout()
    End Sub

    Protected Overridable Sub HandleValueChanged(ByVal sender As Object, ByVal e As GenericComboBoxIndexChangedEvent)
    End Sub

    Public Sub SetSelectedByID(id As Integer)
        Me.SelectedID = id
        UpdateComboBoxSelection(id)
    End Sub

    Public Sub SetSelectedByName(name As String)
        For Each item As ComboBoxItem In _allItems
            If item.Name = name Then
                Me.SelectedID = item.ID
                UpdateComboBoxSelection(item.ID)
                Exit For
            End If
        Next
    End Sub

    Public Sub RefreshData()
        BindComboBox()
    End Sub

    Public Sub ClearSelection()
        CboGeneric.SelectedIndex = -1
        SelectedID = -1
        SelectedName = String.Empty
    End Sub

    Public Class ComboBoxItem
        Public Property ID As Integer
        Public Property Name As String
        Public Overrides Function ToString() As String
            Return Name
        End Function
    End Class

    Public Class GenericComboBoxIndexChangedEvent
        Inherits EventArgs
        Public Property SelectedID As Integer
        Public Property SelectedName As String
        Public Sub New(id As Integer, name As String)
            Me.SelectedID = id
            Me.SelectedName = name
        End Sub
    End Class
End Class
