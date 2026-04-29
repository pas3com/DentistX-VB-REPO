Imports System.ComponentModel
Imports System.Data
Imports System.Data.SqlClient
Imports System.Linq
Imports System.Windows.Forms
Imports Dapper
Imports DevExpress.Utils
Imports DevExpress.XtraEditors

Public Class MedicineGroupsCombo
    Implements INotifyPropertyChanged

    Private ReadOnly _connectionString As String = DentistXDATA.GetConnection.ConnectionString
    Private _bindingSource As New BindingSource()

    Public Event MedicineGroupsValueChanged(ByVal sender As Object, ByVal e As MedicineGroupsIndexChangedEvent)

    Private _selectedMedicineID As Integer
    Public Property MedicineID As Integer
        Get
            Return _selectedMedicineID
        End Get
        Set(value As Integer)
            If Not Equals(_selectedMedicineID, value) Then
                _selectedMedicineID = value
                OnPropertyChanged(NameOf(MedicineID))
                UpdateMedicineIDComboBoxSelection(value)
            End If
        End Set
    End Property

    Private _selectedMedicineFamily As String
    Public Property MedicineFamily As String
        Get
            Return _selectedMedicineFamily
        End Get
        Set(value As String)
            If Not Equals(_selectedMedicineFamily, value) Then
                _selectedMedicineFamily = value
                OnPropertyChanged(NameOf(MedicineFamily))
            End If
        End Set
    End Property

    Private _allRows As New List(Of MedicineGroupsCls)()
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
        ComboToolbarLayoutHelper.ApplyPanelToolbar(PanelControl1, btnSerach, BtnAddMedicineGroups, _btnSearchVisible, _btnAddVisible, Me)
    End Sub

    <Browsable(True), Category("Data"), Description("Optional substring on family name (case-insensitive). Combined with flyout search (AND).")>
    Public Property Filter As String
        Get
            Return _nameFilter
        End Get
        Set(value As String)
            _nameFilter = If(value, String.Empty)
            ApplyNameFilter()
        End Set
    End Property

    Public ReadOnly Property MedicineGroupsBindingSource As BindingSource
        Get
            Return _bindingSource
        End Get
    End Property

    Private Function MatchesFilters(displayName As String, searchContains As String, externalContains As String) As Boolean
        If String.IsNullOrEmpty(displayName) Then Return False
        If Not String.IsNullOrEmpty(externalContains) AndAlso displayName.IndexOf(externalContains, StringComparison.OrdinalIgnoreCase) < 0 Then Return False
        If Not String.IsNullOrEmpty(searchContains) AndAlso displayName.IndexOf(searchContains, StringComparison.OrdinalIgnoreCase) < 0 Then Return False
        Return True
    End Function

    Private Function FilteredRows() As List(Of MedicineGroupsCls)
        Dim searchTyped As String = If(txtSearch IsNot Nothing, txtSearch.Text, String.Empty).Trim()
        Dim external As String = _nameFilter.Trim()
        Return _allRows.Where(Function(r) MatchesFilters(r.MedicineFamily, searchTyped, external)).ToList()
    End Function

    Private Function RowsToTable(rows As IEnumerable(Of MedicineGroupsCls)) As DataTable
        Dim dt As New DataTable()
        dt.Columns.Add("MedicineID", GetType(Integer))
        dt.Columns.Add("MedicineFamily", GetType(String))
        For Each r In rows
            dt.Rows.Add(r.MedicineID, If(r.MedicineFamily, CType(String.Empty, Object)))
        Next
        Return dt
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
        _bindingSource.DataSource = RowsToTable(FilteredRows())
    End Sub

    Private Sub ApplyNameFilter()
        If CboMedicineGroups Is Nothing OrElse _applyingFilter Then Return
        _applyingFilter = True
        Dim filtered = FilteredRows()
        Dim preserveId As Integer = _selectedMedicineID

        Try
            RemoveHandler CboMedicineGroups.SelectedIndexChanged, AddressOf CboMedicineGroups_SelectedIndexChanged

            CboMedicineGroups.Properties.Items.Clear()
            For Each r As MedicineGroupsCls In filtered
                CboMedicineGroups.Properties.Items.Add(New ComboBoxItem With {.ID = r.MedicineID, .Name = r.MedicineFamily})
            Next

            UpdateBindingSourceFromDisplayed()

            Dim keepItem As ComboBoxItem = Nothing
            If preserveId > 0 Then
                For Each item As ComboBoxItem In CboMedicineGroups.Properties.Items
                    If item.ID = preserveId Then
                        keepItem = item
                        Exit For
                    End If
                Next
            End If

            If keepItem IsNot Nothing Then
                CboMedicineGroups.SelectedItem = keepItem
                _selectedMedicineID = keepItem.ID
                _selectedMedicineFamily = keepItem.Name
            ElseIf String.IsNullOrWhiteSpace(If(txtSearch IsNot Nothing, txtSearch.Text, String.Empty).Trim()) AndAlso String.IsNullOrWhiteSpace(_nameFilter.Trim()) AndAlso CboMedicineGroups.Properties.Items.Count > 0 Then
                CboMedicineGroups.SelectedIndex = 0
                Dim first As ComboBoxItem = TryCast(CboMedicineGroups.SelectedItem, ComboBoxItem)
                If first IsNot Nothing Then
                    _selectedMedicineID = first.ID
                    _selectedMedicineFamily = first.Name
                End If
            Else
                CboMedicineGroups.EditValue = Nothing
                _selectedMedicineID = 0
                _selectedMedicineFamily = Nothing
            End If

            SetTextEditStyle(CboMedicineGroups)
        Finally
            AddHandler CboMedicineGroups.SelectedIndexChanged, AddressOf CboMedicineGroups_SelectedIndexChanged
            _applyingFilter = False
        End Try
    End Sub

    Private Sub BindMedicineGroupss()
        Const sql As String = "SELECT MedicineID, MedicineFamily FROM MedicineGroups;"
        Using conn As New SqlConnection(_connectionString)
            conn.Open()
            _allRows = conn.Query(Of MedicineGroupsCls)(sql).ToList()
        End Using
        ApplyNameFilter()
    End Sub

    Private Sub UpdateMedicineIDComboBoxSelection(medicineId As Integer)
        ClearSearchBox()
        If medicineId <= 0 Then
            CboMedicineGroups.EditValue = Nothing
            _selectedMedicineFamily = Nothing
            ApplyNameFilter()
            Return
        End If
        Dim row = _allRows.FirstOrDefault(Function(r) r.MedicineID = medicineId)
        If row Is Nothing Then
            CboMedicineGroups.EditValue = Nothing
            _selectedMedicineFamily = Nothing
            ApplyNameFilter()
            Return
        End If
        ApplyNameFilter()
        For Each item As ComboBoxItem In CboMedicineGroups.Properties.Items
            If item.ID = medicineId Then
                CboMedicineGroups.SelectedItem = item
                _selectedMedicineFamily = item.Name
                Return
            End If
        Next
        CboMedicineGroups.EditValue = Nothing
        _selectedMedicineFamily = Nothing
    End Sub

    Private Sub CboMedicineGroups_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CboMedicineGroups.SelectedIndexChanged
        If _applyingFilter Then Return
        Dim selectedItem As ComboBoxItem = TryCast(CboMedicineGroups.SelectedItem, ComboBoxItem)
        If selectedItem IsNot Nothing Then
            MedicineID = selectedItem.ID
            MedicineFamily = selectedItem.Name
            RaiseEvent MedicineGroupsValueChanged(sender, New MedicineGroupsIndexChangedEvent(MedicineID, MedicineFamily))
        End If
    End Sub

    Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged
        If _suppressSearchTextChanged OrElse _applyingFilter Then Return
        ApplyNameFilter()
    End Sub

    Private Sub Flyout1_Showing(sender As Object, e As FlyoutPanelEventArgs) Handles Flyout1.Showing
        ComboFlyoutSearchHelper.ApplyManualPositionBelowPanel(Flyout1, PanelControl2, CboMedicineGroups)
    End Sub

    Private Sub btnSerach_Click(sender As Object, e As EventArgs) Handles btnSerach.Click
        ComboFlyoutSearchHelper.ShowFlyoutSearchDeferred(Flyout1, PanelControl2, CboMedicineGroups, txtSearch, Me)
    End Sub

    Private Sub btnAddMedicineGroups_Click(sender As Object, e As EventArgs) Handles BtnAddMedicineGroups.Click
        FrmMedicineGroups.ShowDialog()
        BindMedicineGroupss()
    End Sub

    Public Function GetMedicineGroupsTable() As DataTable
        Return RowsToTable(FilteredRows())
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
        BindMedicineGroupss()
    End Sub

    Private Sub HandleMedicineGroupsValueChanged(ByVal sender As Object, ByVal e As MedicineGroupsIndexChangedEvent)
    End Sub

    Public Function GetMedicineID(medicineFamily As String) As Integer
        If String.IsNullOrWhiteSpace(medicineFamily) Then Return -1
        Const sql As String = "SELECT MedicineID FROM MedicineGroups WHERE MedicineFamily = @MedicineFamily"
        Using conn As New SqlConnection(_connectionString)
            Dim result As Integer? = conn.QuerySingleOrDefault(Of Integer?)(sql, New With {.MedicineFamily = medicineFamily})
            Return If(result.HasValue, result.Value, -1)
        End Using
    End Function

    Public Function GetMedicineFamily(medicineId As Integer) As String
        Const sql As String = "SELECT MedicineFamily FROM MedicineGroups WHERE MedicineID = @MedicineID"
        Using conn As New SqlConnection(_connectionString)
            Dim result As String = conn.QuerySingleOrDefault(Of String)(sql, New With {.MedicineID = medicineId})
            Return If(result, String.Empty)
        End Using
    End Function

    Public Sub SetSelectedMedicineFamily(medicineId As Integer)
        Me.MedicineID = medicineId
        UpdateMedicineIDComboBoxSelection(medicineId)
    End Sub

    Public Sub SetSelectedMedicineID(medicineFamily As String)
        Me.MedicineID = GetMedicineID(medicineFamily)
        UpdateMedicineIDComboBoxSelection(MedicineID)
    End Sub

    Public Sub RefreshData()
        BindMedicineGroupss()
    End Sub

    Public Class ComboBoxItem
        Public Property ID As Integer
        Public Property Name As String
        Public Overrides Function ToString() As String
            Return Name
        End Function
    End Class

    Public Class MedicineGroupsCls
        Public Property MedicineID As Integer
        Public Property MedicineFamily As String
    End Class

    Public Class MedicineGroupsIndexChangedEvent
        Inherits EventArgs
        Public Property MedicineID As Integer
        Public Property MedicineFamily As String
        Public Sub New(medicineId As Integer, medicineFamily As String)
            Me.MedicineID = medicineId
            Me.MedicineFamily = medicineFamily
        End Sub
    End Class
End Class
