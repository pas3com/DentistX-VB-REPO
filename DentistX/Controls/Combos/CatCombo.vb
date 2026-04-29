Imports System.ComponentModel
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports System.Linq
Imports System.Windows.Forms
Imports Dapper
Imports DevExpress.Utils
Imports DevExpress.XtraEditors

Public Class CatCombo
    Implements INotifyPropertyChanged

    Private ReadOnly _connectionString As String = DentistXDATA.GetConnection.ConnectionString

    Public Event CategoryValueChanged(ByVal sender As Object, ByVal e As CategoryIndexChangedEvent)

    Private _selectedCategoryID As Integer
    Public Property CategoryID As Integer
        Get
            Return _selectedCategoryID
        End Get
        Set(value As Integer)
            If Not Equals(_selectedCategoryID, value) Then
                _selectedCategoryID = value
                OnPropertyChanged(NameOf(CategoryID))
                UpdateCategoryComboBoxSelection(value)
            End If
        End Set
    End Property

    Private _selectedCategoryName As String
    Public Property CategoryName As String
        Get
            Return _selectedCategoryName
        End Get
        Set(value As String)
            If Not Equals(_selectedCategoryName, value) Then
                _selectedCategoryName = value
                OnPropertyChanged(NameOf(CategoryName))
            End If
        End Set
    End Property

    Private _allRows As New List(Of Category)()
    Private _nameFilter As String = String.Empty
    Private _applyingFilter As Boolean
    Private _suppressSearchTextChanged As Boolean

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
        ComboToolbarLayoutHelper.ApplyPanelToolbar(PanelControl1, btnSerach, BtnAddCat, _btnSearchVisible, _btnAddVisible, Me)
    End Sub

    <Browsable(True), Category("Data"), Description("Optional substring to restrict display names (case-insensitive). Combined with flyout search text.")>
    Public Property Filter As String
        Get
            Return _nameFilter
        End Get
        Set(value As String)
            _nameFilter = If(value, String.Empty)
            ApplyListFilter()
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

    Private Sub ApplyListFilter()
        If CboCat Is Nothing OrElse _applyingFilter Then Return
        _applyingFilter = True
        Dim searchTyped As String = If(txtSearch IsNot Nothing, txtSearch.Text, String.Empty).Trim()
        Dim external As String = _nameFilter.Trim()
        Dim filtered = _allRows.Where(Function(p) MatchesFilters(p.CategoryName, searchTyped, external)).ToList()
        Dim preserveId As Integer = _selectedCategoryID

        Try
            RemoveHandler CboCat.SelectedIndexChanged, AddressOf CboCat_SelectedIndexChanged
            CboCat.Properties.Items.Clear()
            For Each row As Category In filtered
                CboCat.Properties.Items.Add(New ComboBoxItem With {.ID = row.CategoryID, .Name = row.CategoryName})
            Next

            Dim keepItem As ComboBoxItem = Nothing
            If preserveId > 0 Then
                For Each item As ComboBoxItem In CboCat.Properties.Items
                    If item.ID = preserveId Then
                        keepItem = item
                        Exit For
                    End If
                Next
            End If

            If keepItem IsNot Nothing Then
                CboCat.SelectedItem = keepItem
                _selectedCategoryID = keepItem.ID
                _selectedCategoryName = keepItem.Name
            ElseIf String.IsNullOrWhiteSpace(searchTyped) AndAlso String.IsNullOrWhiteSpace(external) AndAlso CboCat.Properties.Items.Count > 0 Then
                CboCat.SelectedIndex = 0
                Dim first As ComboBoxItem = TryCast(CboCat.SelectedItem, ComboBoxItem)
                If first IsNot Nothing Then
                    _selectedCategoryID = first.ID
                    _selectedCategoryName = first.Name
                End If
            Else
                CboCat.EditValue = Nothing
                _selectedCategoryID = 0
                _selectedCategoryName = Nothing
            End If
            SetTextEditStyle(CboCat)
        Finally
            AddHandler CboCat.SelectedIndexChanged, AddressOf CboCat_SelectedIndexChanged
            _applyingFilter = False
        End Try
    End Sub

    Private Sub BindCategs()
        _allRows = LoadFromDb()
        ApplyListFilter()
    End Sub

    Private Sub UpdateCategoryComboBoxSelection(categoryId As Integer)
        ClearSearchBox()
        If categoryId <= 0 Then
            CboCat.EditValue = Nothing
            _selectedCategoryName = Nothing
            ApplyListFilter()
            Return
        End If
        Dim row = _allRows.FirstOrDefault(Function(p) p.CategoryID = categoryId)
        If row Is Nothing Then
            CboCat.EditValue = Nothing
            _selectedCategoryName = Nothing
            ApplyListFilter()
            Return
        End If
        ApplyListFilter()
        For Each item As ComboBoxItem In CboCat.Properties.Items
            If item.ID = categoryId Then
                CboCat.SelectedItem = item
                _selectedCategoryName = item.Name
                Return
            End If
        Next
        CboCat.EditValue = Nothing
        _selectedCategoryName = Nothing
    End Sub

    Private Sub CboCat_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CboCat.SelectedIndexChanged
        If _applyingFilter Then Return
        Dim selectedItem As ComboBoxItem = TryCast(CboCat.SelectedItem, ComboBoxItem)
        If selectedItem IsNot Nothing Then
            CategoryID = selectedItem.ID
            CategoryName = selectedItem.Name
            RaiseEvent CategoryValueChanged(sender, New CategoryIndexChangedEvent(CategoryID, CategoryName))
        End If
    End Sub

    Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged
        If _suppressSearchTextChanged OrElse _applyingFilter Then Return
        ApplyListFilter()
    End Sub

    Private Sub Flyout1_Showing(sender As Object, e As FlyoutPanelEventArgs) Handles Flyout1.Showing
        ComboFlyoutSearchHelper.ApplyManualPositionBelowPanel(Flyout1, PanelControl2, CboCat)
    End Sub

    Private Sub btnSerach_Click(sender As Object, e As EventArgs) Handles btnSerach.Click
        ComboFlyoutSearchHelper.ShowFlyoutSearchDeferred(Flyout1, PanelControl2, CboCat, txtSearch, Me)
    End Sub

    Private Sub BtnAddCat_Click(sender As Object, e As EventArgs) Handles BtnAddCat.Click
        TblCategForm.Icon = MainView3.Icon
        TblCategForm.ShowDialog()
        BindCategs()
    End Sub

    Public Function GetCitiesTable() As DataTable
        Dim rows = LoadFromDb()
        Dim dt As New DataTable()
        dt.Columns.Add("CategoryID", GetType(Integer))
        dt.Columns.Add("CategoryName", GetType(String))
        For Each p In rows
            dt.Rows.Add(p.CategoryID, If(p.CategoryName, CType(String.Empty, Object)))
        Next
        Return dt
    End Function

    Private Function LoadFromDb() As List(Of Category)
        Const sql As String = "SELECT CategoryID, CategoryName FROM TblCategories;"
        Using conn As New SqlConnection(_connectionString)
            conn.Open()
            Return conn.Query(Of Category)(sql).ToList()
        End Using
    End Function

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
        BindCategs()
    End Sub

    Private Sub HandleCategoryValueChanged(ByVal sender As Object, ByVal e As CategoryIndexChangedEvent) Handles Me.CategoryValueChanged
    End Sub

    Public Sub SetSelectedCategory(categoryId As Integer)
        Me.CategoryID = categoryId
        UpdateCategoryComboBoxSelection(categoryId)
    End Sub

    Public Class ComboBoxItem
        Public Property ID As Integer
        Public Property Name As String
        Public Overrides Function ToString() As String
            Return Name
        End Function
    End Class

    Public Class Category
        Public Property CategoryID As Integer
        Public Property CategoryName As String
    End Class

    Public Class CategoryIndexChangedEvent
        Inherits EventArgs
        Public Property CategoryID As Integer
        Public Property CategoryName As String
        Public Sub New(categoryId As Integer, categoryName As String)
            Me.CategoryID = categoryId
            Me.CategoryName = categoryName
        End Sub
    End Class
End Class
