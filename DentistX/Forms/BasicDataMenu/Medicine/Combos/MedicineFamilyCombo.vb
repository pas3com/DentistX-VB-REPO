Imports System.ComponentModel
Imports System.Data
Imports System.Data.SqlClient
Imports System.Linq
Imports System.Windows.Forms
Imports Dapper
Imports DevExpress.Utils
Imports DevExpress.XtraEditors

Public Class MedicineFamilyCombo
    Implements INotifyPropertyChanged

    Private ReadOnly _connectionString As String = DentistXDATA.GetConnection.ConnectionString
    Private _bindingSource As New BindingSource()
    Private _parentMedicineID As Integer = -1

    Public Event MedicineFamilyValueChanged(ByVal sender As Object, ByVal e As MedicineFamilyIndexChangedEvent)

    Private _selectedSubCatID As Integer
    Public Property SubCatID As Integer
        Get
            Return _selectedSubCatID
        End Get
        Set(value As Integer)
            If Not Equals(_selectedSubCatID, value) Then
                _selectedSubCatID = value
                OnPropertyChanged(NameOf(SubCatID))
                UpdateSubCatIDComboBoxSelection(value)
            End If
        End Set
    End Property

    Private _selectedMedicineSubCat As String
    Public Property MedicineSubCat As String
        Get
            Return _selectedMedicineSubCat
        End Get
        Set(value As String)
            If Not Equals(_selectedMedicineSubCat, value) Then
                _selectedMedicineSubCat = value
                OnPropertyChanged(NameOf(MedicineSubCat))
            End If
        End Set
    End Property

    Private _medicineIdFromRow As Integer
    Public Property MedicineID As Integer
        Get
            Return _medicineIdFromRow
        End Get
        Set(value As Integer)
            _medicineIdFromRow = value
            OnPropertyChanged(NameOf(MedicineID))
        End Set
    End Property

    Public ReadOnly Property MedicineFamilyBindingSource As BindingSource
        Get
            Return _bindingSource
        End Get
    End Property

    Public Property ParentMedicineID As Integer
        Get
            Return _parentMedicineID
        End Get
        Set(value As Integer)
            If _parentMedicineID <> value Then
                _parentMedicineID = value
                _bindingSource.Filter = String.Empty
                ApplyNameFilter()
            End If
        End Set
    End Property

    Private _allRows As New List(Of MedicineFamilyCls)()
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
        ComboToolbarLayoutHelper.ApplyPanelToolbar(PanelControl1, btnSerach, BtnAddMedicineFamily, _btnSearchVisible, _btnAddVisible, Me)
    End Sub

    <Browsable(True), Category("Data"), Description("Optional substring on sub-category name (case-insensitive). Combined with flyout search (AND).")>
    Public Property Filter As String
        Get
            Return _nameFilter
        End Get
        Set(value As String)
            _nameFilter = If(value, String.Empty)
            ApplyNameFilter()
        End Set
    End Property

    Private Function ScopedRows() As List(Of MedicineFamilyCls)
        If _parentMedicineID > -1 Then
            Return _allRows.Where(Function(r) r.MedicineID = _parentMedicineID).ToList()
        End If
        Return _allRows.ToList()
    End Function

    Private Function MatchesFilters(displayName As String, searchContains As String, externalContains As String) As Boolean
        If String.IsNullOrEmpty(displayName) Then Return False
        If Not String.IsNullOrEmpty(externalContains) AndAlso displayName.IndexOf(externalContains, StringComparison.OrdinalIgnoreCase) < 0 Then Return False
        If Not String.IsNullOrEmpty(searchContains) AndAlso displayName.IndexOf(searchContains, StringComparison.OrdinalIgnoreCase) < 0 Then Return False
        Return True
    End Function

    Private Function FilteredRows() As List(Of MedicineFamilyCls)
        Dim searchTyped As String = If(txtSearch IsNot Nothing, txtSearch.Text, String.Empty).Trim()
        Dim external As String = _nameFilter.Trim()
        Return ScopedRows().Where(Function(r) MatchesFilters(r.MedicineSubCat, searchTyped, external)).ToList()
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

    Private Sub UpdateBindingSourceFromDisplayed()
        _bindingSource.DataSource = FilteredRows().ToList()
    End Sub

    Private Sub ApplyNameFilter()
        If CboMedicineFamily Is Nothing OrElse _applyingFilter Then Return
        _applyingFilter = True
        Dim filtered = FilteredRows()
        Dim preserveId As Integer = _selectedSubCatID

        Try
            RemoveHandler CboMedicineFamily.SelectedIndexChanged, AddressOf CboMedicineFamily_SelectedIndexChanged

            CboMedicineFamily.Properties.Items.Clear()
            For Each r As MedicineFamilyCls In filtered
                CboMedicineFamily.Properties.Items.Add(New ComboBoxItem With {
                    .ID = r.SubCatID,
                    .Name = r.MedicineSubCat,
                    .ParentMedicineID = r.MedicineID
                })
            Next

            UpdateBindingSourceFromDisplayed()

            Dim keepItem As ComboBoxItem = Nothing
            If preserveId > 0 Then
                For Each item As ComboBoxItem In CboMedicineFamily.Properties.Items
                    If item.ID = preserveId Then
                        keepItem = item
                        Exit For
                    End If
                Next
            End If

            Dim searchEmpty = String.IsNullOrWhiteSpace(If(txtSearch IsNot Nothing, txtSearch.Text, String.Empty).Trim())
            If keepItem IsNot Nothing Then
                CboMedicineFamily.SelectedItem = keepItem
                _selectedSubCatID = keepItem.ID
                _selectedMedicineSubCat = keepItem.Name
                _medicineIdFromRow = keepItem.ParentMedicineID
            ElseIf searchEmpty AndAlso String.IsNullOrWhiteSpace(_nameFilter.Trim()) AndAlso CboMedicineFamily.Properties.Items.Count > 0 Then
                CboMedicineFamily.SelectedIndex = 0
                Dim first As ComboBoxItem = TryCast(CboMedicineFamily.SelectedItem, ComboBoxItem)
                If first IsNot Nothing Then
                    _selectedSubCatID = first.ID
                    _selectedMedicineSubCat = first.Name
                    _medicineIdFromRow = first.ParentMedicineID
                End If
            Else
                CboMedicineFamily.EditValue = Nothing
                _selectedSubCatID = 0
                _selectedMedicineSubCat = Nothing
                _medicineIdFromRow = 0
            End If

            SetTextEditStyle(CboMedicineFamily)
        Finally
            AddHandler CboMedicineFamily.SelectedIndexChanged, AddressOf CboMedicineFamily_SelectedIndexChanged
            _applyingFilter = False
        End Try
    End Sub

    Private Sub BindMedicineFamilies()
        Const sql As String = "SELECT SubCatID, MedicineSubCat, MedicineID FROM MedicineFamily;"
        Using conn As New SqlConnection(_connectionString)
            conn.Open()
            _allRows = conn.Query(Of MedicineFamilyCls)(sql).ToList()
        End Using
        ApplyNameFilter()
    End Sub

    Private Sub UpdateSubCatIDComboBoxSelection(subCatId As Integer)
        ClearSearchBox()
        If subCatId <= 0 Then
            CboMedicineFamily.EditValue = Nothing
            _selectedMedicineSubCat = Nothing
            _medicineIdFromRow = 0
            ApplyNameFilter()
            Return
        End If
        Dim row = _allRows.FirstOrDefault(Function(r) r.SubCatID = subCatId)
        If row Is Nothing Then
            CboMedicineFamily.EditValue = Nothing
            _selectedMedicineSubCat = Nothing
            _medicineIdFromRow = 0
            ApplyNameFilter()
            Return
        End If
        ApplyNameFilter()
        For Each item As ComboBoxItem In CboMedicineFamily.Properties.Items
            If item.ID = subCatId Then
                CboMedicineFamily.SelectedItem = item
                _selectedMedicineSubCat = item.Name
                _medicineIdFromRow = item.ParentMedicineID
                Return
            End If
        Next
        CboMedicineFamily.EditValue = Nothing
        _selectedMedicineSubCat = Nothing
        _medicineIdFromRow = 0
    End Sub

    Private Sub CboMedicineFamily_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CboMedicineFamily.SelectedIndexChanged
        If _applyingFilter Then Return
        Dim selectedItem As ComboBoxItem = TryCast(CboMedicineFamily.SelectedItem, ComboBoxItem)
        If selectedItem IsNot Nothing Then
            SubCatID = selectedItem.ID
            MedicineSubCat = selectedItem.Name
            MedicineID = selectedItem.ParentMedicineID
            RaiseEvent MedicineFamilyValueChanged(sender, New MedicineFamilyIndexChangedEvent(SubCatID, MedicineSubCat, MedicineID))
        End If
    End Sub

    Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged
        If _suppressSearchTextChanged OrElse _applyingFilter Then Return
        ApplyNameFilter()
    End Sub

    Private Sub Flyout1_Showing(sender As Object, e As FlyoutPanelEventArgs) Handles Flyout1.Showing
        ComboFlyoutSearchHelper.ApplyManualPositionBelowPanel(Flyout1, PanelControl2, CboMedicineFamily)
    End Sub

    Private Sub btnSerach_Click(sender As Object, e As EventArgs) Handles btnSerach.Click
        ComboFlyoutSearchHelper.ShowFlyoutSearchDeferred(Flyout1, PanelControl2, CboMedicineFamily, txtSearch, Me)
    End Sub

    Private Sub btnAddMedicineFamily_Click(sender As Object, e As EventArgs) Handles BtnAddMedicineFamily.Click
        FrmMedicineFamily.ShowDialog()
        BindMedicineFamilies()
    End Sub

    Public Function GetMedicineFamilyTable() As DataTable
        Dim dt As New DataTable()
        dt.Columns.Add("SubCatID", GetType(Integer))
        dt.Columns.Add("MedicineSubCat", GetType(String))
        dt.Columns.Add("MedicineID", GetType(Integer))
        For Each r In FilteredRows()
            dt.Rows.Add(r.SubCatID, If(r.MedicineSubCat, CType(String.Empty, Object)), r.MedicineID)
        Next
        Return dt
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
        BindMedicineFamilies()
    End Sub

    Private Sub HandleMedicineFamilyValueChanged(ByVal sender As Object, ByVal e As MedicineFamilyIndexChangedEvent)
    End Sub

    Public Function GetSubCatID(medicineSubCat As String) As Integer
        If String.IsNullOrWhiteSpace(medicineSubCat) Then Return -1
        Const sql As String = "SELECT SubCatID FROM MedicineFamily WHERE MedicineSubCat = @MedicineSubCat"
        Using conn As New SqlConnection(_connectionString)
            Dim result As Integer? = conn.QuerySingleOrDefault(Of Integer?)(sql, New With {.MedicineSubCat = medicineSubCat})
            Return If(result.HasValue, result.Value, -1)
        End Using
    End Function

    Public Function GetMedicineID(subCatId As Integer) As String
        Const sql As String = "SELECT MedicineSubCat FROM MedicineFamily WHERE SubCatID = @SubCatID"
        Using conn As New SqlConnection(_connectionString)
            Dim result As String = conn.QuerySingleOrDefault(Of String)(sql, New With {.SubCatID = subCatId})
            Return If(result, String.Empty)
        End Using
    End Function

    Public Sub SetSelectedMedicineID(subCatId As Integer)
        Me.SubCatID = subCatId
        UpdateSubCatIDComboBoxSelection(subCatId)
    End Sub

    Public Sub SetSelectedSubCatID(medicineSubCat As String)
        Me.SubCatID = GetSubCatID(medicineSubCat)
        UpdateSubCatIDComboBoxSelection(SubCatID)
    End Sub

    Public Sub RefreshData()
        BindMedicineFamilies()
    End Sub

    Public Class ComboBoxItem
        Public Property ID As Integer
        Public Property Name As String
        Public Property ParentMedicineID As Integer
        Public Overrides Function ToString() As String
            Return Name
        End Function
    End Class

    Public Class MedicineFamilyCls
        Public Property SubCatID As Integer
        Public Property MedicineSubCat As String
        Public Property MedicineID As Integer
    End Class

    Public Class MedicineFamilyIndexChangedEvent
        Inherits EventArgs
        Public Property SubCatID As Integer
        Public Property MedicineSubCat As String
        Public Property MedicineID As Integer
        Public Sub New(subCatId As Integer, medicineSubCat As String, medicineId As Integer)
            Me.SubCatID = subCatId
            Me.MedicineSubCat = medicineSubCat
            Me.MedicineID = medicineId
        End Sub
    End Class
End Class
