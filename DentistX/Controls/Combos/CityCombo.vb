Imports System.ComponentModel
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports System.Linq
Imports System.Windows.Forms
Imports Dapper
Imports DevExpress.Utils
Imports DevExpress.XtraEditors

Public Class CityCombo
    Implements INotifyPropertyChanged

    Private ReadOnly _connectionString As String = DentistXDATA.GetConnection.ConnectionString

    Public Event CityValueChanged(ByVal sender As Object, ByVal e As CityIndexChangedEvent)

    Private _selectedCityID As Integer
    Public Property CityID As Integer
        Get
            Return _selectedCityID
        End Get
        Set(value As Integer)
            If Not Equals(_selectedCityID, value) Then
                _selectedCityID = value
                OnPropertyChanged(NameOf(CityID))
                UpdateCityComboBoxSelection(value)
            End If
        End Set
    End Property

    Private _selectedCityName As String
    Public Property CityName As String
        Get
            Return _selectedCityName
        End Get
        Set(value As String)
            If Not Equals(_selectedCityName, value) Then
                _selectedCityName = value
                OnPropertyChanged(NameOf(CityName))
            End If
        End Set
    End Property

    Private _allRows As New List(Of City)()
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
        ComboToolbarLayoutHelper.ApplyPanelToolbar(PanelControl1, btnSerach, BtnAddCity, _btnSearchVisible, _btnAddVisible, Me)
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
        If CboCity Is Nothing OrElse _applyingFilter Then Return
        _applyingFilter = True
        Dim searchTyped As String = If(txtSearch IsNot Nothing, txtSearch.Text, String.Empty).Trim()
        Dim external As String = _nameFilter.Trim()
        Dim filtered = _allRows.Where(Function(p) MatchesFilters(p.CityName, searchTyped, external)).ToList()
        Dim preserveId As Integer = _selectedCityID

        Try
            RemoveHandler CboCity.SelectedIndexChanged, AddressOf CboCity_SelectedIndexChanged
            CboCity.Properties.Items.Clear()
            For Each row As City In filtered
                CboCity.Properties.Items.Add(New ComboBoxItem With {.ID = row.CityID, .Name = row.CityName})
            Next

            Dim keepItem As ComboBoxItem = Nothing
            If preserveId > 0 Then
                For Each item As ComboBoxItem In CboCity.Properties.Items
                    If item.ID = preserveId Then
                        keepItem = item
                        Exit For
                    End If
                Next
            End If

            If keepItem IsNot Nothing Then
                CboCity.SelectedItem = keepItem
                _selectedCityID = keepItem.ID
                _selectedCityName = keepItem.Name
            ElseIf String.IsNullOrWhiteSpace(searchTyped) AndAlso String.IsNullOrWhiteSpace(external) AndAlso CboCity.Properties.Items.Count > 0 Then
                CboCity.SelectedIndex = 0
                Dim first As ComboBoxItem = TryCast(CboCity.SelectedItem, ComboBoxItem)
                If first IsNot Nothing Then
                    _selectedCityID = first.ID
                    _selectedCityName = first.Name
                End If
            Else
                CboCity.EditValue = Nothing
                _selectedCityID = 0
                _selectedCityName = Nothing
            End If
            SetTextEditStyle(CboCity)
        Finally
            AddHandler CboCity.SelectedIndexChanged, AddressOf CboCity_SelectedIndexChanged
            _applyingFilter = False
        End Try
    End Sub

    Private Sub BindCities()
        _allRows = LoadCitiesFromDb()
        ApplyListFilter()
    End Sub

    Private Sub UpdateCityComboBoxSelection(cityId As Integer)
        ClearSearchBox()
        If cityId <= 0 Then
            CboCity.EditValue = Nothing
            _selectedCityName = Nothing
            ApplyListFilter()
            Return
        End If
        Dim row = _allRows.FirstOrDefault(Function(p) p.CityID = cityId)
        If row Is Nothing Then
            CboCity.EditValue = Nothing
            _selectedCityName = Nothing
            ApplyListFilter()
            Return
        End If
        ApplyListFilter()
        For Each item As ComboBoxItem In CboCity.Properties.Items
            If item.ID = cityId Then
                CboCity.SelectedItem = item
                _selectedCityName = item.Name
                Return
            End If
        Next
        CboCity.EditValue = Nothing
        _selectedCityName = Nothing
    End Sub

    Private Sub CboCity_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CboCity.SelectedIndexChanged
        If _applyingFilter Then Return
        Dim selectedItem As ComboBoxItem = TryCast(CboCity.SelectedItem, ComboBoxItem)
        If selectedItem IsNot Nothing Then
            CityID = selectedItem.ID
            CityName = selectedItem.Name
            RaiseEvent CityValueChanged(sender, New CityIndexChangedEvent(CityID, CityName))
        End If
    End Sub

    Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged
        If _suppressSearchTextChanged OrElse _applyingFilter Then Return
        ApplyListFilter()
    End Sub

    Private Sub Flyout1_Showing(sender As Object, e As FlyoutPanelEventArgs) Handles Flyout1.Showing
        ComboFlyoutSearchHelper.ApplyManualPositionBelowPanel(Flyout1, PanelControl2, CboCity)
    End Sub

    Private Sub btnSerach_Click(sender As Object, e As EventArgs) Handles btnSerach.Click
        ComboFlyoutSearchHelper.ShowFlyoutSearchDeferred(Flyout1, PanelControl2, CboCity, txtSearch, Me)
    End Sub

    Private Sub btnAddCity_Click(sender As Object, e As EventArgs) Handles BtnAddCity.Click
        Frm_TblCities.Icon = MainView3.Icon
        Frm_TblCities.ShowDialog()
        BindCities()
    End Sub

    Public Function GetCitiesTable() As DataTable
        Dim rows = LoadCitiesFromDb()
        Dim dt As New DataTable()
        dt.Columns.Add("CityID", GetType(Integer))
        dt.Columns.Add("CityName", GetType(String))
        For Each p In rows
            dt.Rows.Add(p.CityID, If(p.CityName, CType(String.Empty, Object)))
        Next
        Return dt
    End Function

    Private Function LoadCitiesFromDb() As List(Of City)
        Const sql As String = "SELECT CityID, CityName FROM TblCities;"
        Using conn As New SqlConnection(_connectionString)
            conn.Open()
            Return conn.Query(Of City)(sql).ToList()
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
        BindCities()
    End Sub

    Private Sub HandleCityValueChanged(ByVal sender As Object, ByVal e As CityIndexChangedEvent) Handles Me.CityValueChanged
        Me.CityID = e.CityID
        Me.CityName = e.CityName
    End Sub

    Public Function GetCityID(cityName As String) As Integer
        If String.IsNullOrWhiteSpace(cityName) Then Return -1
        Const sql As String = "SELECT CityID FROM TblCities WHERE CityName = @CityName"
        Using conn As New SqlConnection(_connectionString)
            Dim r As Integer? = conn.QuerySingleOrDefault(Of Integer?)(sql, New With {.CityName = cityName})
            Return If(r.HasValue, r.Value, -1)
        End Using
    End Function

    Public Function GetCityName(cityId As Integer) As String
        Const sql As String = "SELECT CityName FROM TblCities WHERE CityID = @CityID"
        Using conn As New SqlConnection(_connectionString)
            Return If(conn.QuerySingleOrDefault(Of String)(sql, New With {.CityID = cityId}), String.Empty)
        End Using
    End Function

    Public Sub SetSelectedCityName(cityId As Integer)
        Dim n As String = GetCityName(cityId)
        If Not String.IsNullOrEmpty(n) Then
            Me.CityID = cityId
            Me.CityName = n
            UpdateCityComboBoxSelection(cityId)
        End If
    End Sub

    Public Sub SetSelectedCityID(cityName As String)
        Dim id As Integer = GetCityID(cityName)
        If id <> -1 Then
            Me.CityID = id
            Me.CityName = cityName
            UpdateCityComboBoxSelection(id)
        End If
    End Sub

    Public Class ComboBoxItem
        Public Property ID As Integer
        Public Property Name As String
        Public Overrides Function ToString() As String
            Return Name
        End Function
    End Class

    Public Class City
        Public Property CityID As Integer
        Public Property CityName As String
    End Class

    Public Class CityIndexChangedEvent
        Inherits EventArgs
        Public Property CityID As Integer
        Public Property CityName As String
        Public Sub New(cityId As Integer, cityName As String)
            Me.CityID = cityId
            Me.CityName = cityName
        End Sub
    End Class
End Class
