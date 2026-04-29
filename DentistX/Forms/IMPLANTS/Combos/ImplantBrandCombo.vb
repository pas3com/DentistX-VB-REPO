Imports System.ComponentModel
Imports System.Data
Imports System.Data.SqlClient
Imports System.Linq
Imports System.Windows.Forms
Imports Dapper
Imports DevExpress.Utils
Imports DevExpress.XtraEditors

Public Class ImplantBrandCombo
    Implements INotifyPropertyChanged

    Private ReadOnly _connectionString As String = DentistXDATA.GetConnection.ConnectionString

    Public Event ImplantBrandValueChanged(ByVal sender As Object, ByVal e As ImplantBrandIndexChangedEvent)

    Private _selectedBrandID As Integer
    Public Property BrandID As Integer
        Get
            Return _selectedBrandID
        End Get
        Set(value As Integer)
            If Not Equals(_selectedBrandID, value) Then
                _selectedBrandID = value
                OnPropertyChanged(NameOf(BrandID))
                UpdateBrandIDComboBoxSelection(value)
            End If
        End Set
    End Property

    Private _selectedBrandName As String
    Public Property BrandName As String
        Get
            Return _selectedBrandName
        End Get
        Set(value As String)
            If Not Equals(_selectedBrandName, value) Then
                _selectedBrandName = value
                OnPropertyChanged(NameOf(BrandName))
            End If
        End Set
    End Property

    Private _allRows As New List(Of ImplantBrandCls)()
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
        ComboToolbarLayoutHelper.ApplyPanelToolbar(PanelControl1, btnSerach, BtnAddImplantBrand, _btnSearchVisible, _btnAddVisible, Me)
    End Sub

    <Browsable(True), Category("Data"), Description("Optional substring on brand name (case-insensitive). Combined with flyout search (AND).")>
    Public Property Filter As String
        Get
            Return _nameFilter
        End Get
        Set(value As String)
            _nameFilter = If(value, String.Empty)
            ApplyNameFilter()
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

    Private Sub ApplyNameFilter()
        If CboImplantBrand Is Nothing OrElse _applyingFilter Then Return
        _applyingFilter = True
        Dim searchTyped As String = If(txtSearch IsNot Nothing, txtSearch.Text, String.Empty).Trim()
        Dim external As String = _nameFilter.Trim()
        Dim filtered = _allRows.Where(Function(r) MatchesFilters(r.BrandName, searchTyped, external)).ToList()
        Dim preserveId As Integer = _selectedBrandID

        Try
            RemoveHandler CboImplantBrand.SelectedIndexChanged, AddressOf CboImplantBrand_SelectedIndexChanged

            CboImplantBrand.Properties.Items.Clear()
            For Each r As ImplantBrandCls In filtered
                CboImplantBrand.Properties.Items.Add(New ComboBoxItem With {.ID = r.BrandID, .Name = r.BrandName})
            Next

            Dim keepItem As ComboBoxItem = Nothing
            If preserveId > 0 Then
                For Each item As ComboBoxItem In CboImplantBrand.Properties.Items
                    If item.ID = preserveId Then
                        keepItem = item
                        Exit For
                    End If
                Next
            End If

            If keepItem IsNot Nothing Then
                CboImplantBrand.SelectedItem = keepItem
                _selectedBrandID = keepItem.ID
                _selectedBrandName = keepItem.Name
            ElseIf String.IsNullOrWhiteSpace(searchTyped) AndAlso String.IsNullOrWhiteSpace(external) AndAlso CboImplantBrand.Properties.Items.Count > 0 Then
                CboImplantBrand.SelectedIndex = 0
                Dim first As ComboBoxItem = TryCast(CboImplantBrand.SelectedItem, ComboBoxItem)
                If first IsNot Nothing Then
                    _selectedBrandID = first.ID
                    _selectedBrandName = first.Name
                End If
            Else
                CboImplantBrand.EditValue = Nothing
                _selectedBrandID = 0
                _selectedBrandName = Nothing
            End If

            SetTextEditStyle(CboImplantBrand)
        Finally
            AddHandler CboImplantBrand.SelectedIndexChanged, AddressOf CboImplantBrand_SelectedIndexChanged
            _applyingFilter = False
        End Try
    End Sub

    Private Sub BindImplantBrands()
        _allRows = GetImplantBrands()
        ApplyNameFilter()
    End Sub

    Private Sub UpdateBrandIDComboBoxSelection(brandId As Integer)
        ClearSearchBox()
        If brandId <= 0 Then
            CboImplantBrand.EditValue = Nothing
            _selectedBrandName = Nothing
            ApplyNameFilter()
            Return
        End If
        Dim row = _allRows.FirstOrDefault(Function(r) r.BrandID = brandId)
        If row Is Nothing Then
            CboImplantBrand.EditValue = Nothing
            _selectedBrandName = Nothing
            ApplyNameFilter()
            Return
        End If
        ApplyNameFilter()
        For Each item As ComboBoxItem In CboImplantBrand.Properties.Items
            If item.ID = brandId Then
                CboImplantBrand.SelectedItem = item
                _selectedBrandName = item.Name
                Return
            End If
        Next
        CboImplantBrand.EditValue = Nothing
        _selectedBrandName = Nothing
    End Sub

    Private Sub CboImplantBrand_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CboImplantBrand.SelectedIndexChanged
        If _applyingFilter Then Return
        Dim selectedItem As ComboBoxItem = TryCast(CboImplantBrand.SelectedItem, ComboBoxItem)
        If selectedItem IsNot Nothing Then
            BrandID = selectedItem.ID
            BrandName = selectedItem.Name
            RaiseEvent ImplantBrandValueChanged(sender, New ImplantBrandIndexChangedEvent(BrandID, BrandName))
        End If
    End Sub

    Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged
        If _suppressSearchTextChanged OrElse _applyingFilter Then Return
        ApplyNameFilter()
    End Sub

    Private Sub Flyout1_Showing(sender As Object, e As FlyoutPanelEventArgs) Handles Flyout1.Showing
        ComboFlyoutSearchHelper.ApplyManualPositionBelowPanel(Flyout1, PanelControl2, CboImplantBrand)
    End Sub

    Private Sub btnSerach_Click(sender As Object, e As EventArgs) Handles btnSerach.Click
        ComboFlyoutSearchHelper.ShowFlyoutSearchDeferred(Flyout1, PanelControl2, CboImplantBrand, txtSearch, Me)
    End Sub

    Private Sub btnAddImplantBrand_Click(sender As Object, e As EventArgs) Handles BtnAddImplantBrand.Click
        FrmImplantBrand.ShowDialog()
        BindImplantBrands()
    End Sub

    Public Function GetImplantBrandTable() As DataTable
        Dim dt As New DataTable()
        dt.Columns.Add("BrandID", GetType(Integer))
        dt.Columns.Add("BrandName", GetType(String))
        For Each r In _allRows
            dt.Rows.Add(r.BrandID, If(r.BrandName, CType(String.Empty, Object)))
        Next
        Return dt
    End Function

    Private Function GetImplantBrands() As List(Of ImplantBrandCls)
        Const sql As String = "SELECT BrandID, BrandName FROM ImplantBrand;"
        Using conn As New SqlConnection(_connectionString)
            conn.Open()
            Return conn.Query(Of ImplantBrandCls)(sql).ToList()
        End Using
    End Function

    Private Sub SetTextEditStyle(ByVal comboBox As ComboBoxEdit)
        comboBox.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    End Sub

    Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged
    Protected Overridable Sub OnPropertyChanged(ByVal propertyName As String)
        RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(propertyName))
    End Sub

    Public Sub New()
        InitializeComponent()
        ApplyToolbarLayout()
        BindImplantBrands()
    End Sub

    Private Sub HandleImplantBrandValueChanged(ByVal sender As Object, ByVal e As ImplantBrandIndexChangedEvent) Handles Me.ImplantBrandValueChanged
    End Sub

    Public Function GetBrandID(brandName As String) As Integer
        If String.IsNullOrWhiteSpace(brandName) Then Return -1
        Const sql As String = "SELECT BrandID FROM ImplantBrand WHERE BrandName = @BrandName"
        Using conn As New SqlConnection(_connectionString)
            Dim result As Integer? = conn.QuerySingleOrDefault(Of Integer?)(sql, New With {.BrandName = brandName})
            Return If(result.HasValue, result.Value, -1)
        End Using
    End Function

    Public Function GetBrandName(brandId As Integer) As String
        Const sql As String = "SELECT BrandName FROM ImplantBrand WHERE BrandID = @BrandID"
        Using conn As New SqlConnection(_connectionString)
            Dim result As String = conn.QuerySingleOrDefault(Of String)(sql, New With {.BrandID = brandId})
            Return If(result, String.Empty)
        End Using
    End Function

    Public Sub SetSelectedBrandName(brandId As Integer)
        Me.BrandID = brandId
        UpdateBrandIDComboBoxSelection(brandId)
    End Sub

    Public Sub SetSelectedBrandID(brandName As String)
        Me.BrandID = GetBrandID(brandName)
        UpdateBrandIDComboBoxSelection(BrandID)
    End Sub

    Public Class ComboBoxItem
        Public Property ID As Integer
        Public Property Name As String
        Public Overrides Function ToString() As String
            Return Name
        End Function
    End Class

    Public Class ImplantBrandCls
        Public Property BrandID As Integer
        Public Property BrandName As String
    End Class

    Public Class ImplantBrandIndexChangedEvent
        Inherits EventArgs
        Public Property BrandID As Integer
        Public Property BrandName As String
        Public Sub New(brandId As Integer, brandName As String)
            Me.BrandID = brandId
            Me.BrandName = brandName
        End Sub
    End Class
End Class
