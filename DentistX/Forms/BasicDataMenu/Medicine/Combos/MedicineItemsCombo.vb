Imports System.ComponentModel
Imports System.Data
Imports System.Data.SqlClient
Imports System.Linq
Imports System.Windows.Forms
Imports Dapper
Imports DevExpress.Utils
Imports DevExpress.XtraEditors

Public Class MedicineItemsCombo
    Implements INotifyPropertyChanged

    Private ReadOnly _connectionString As String = DentistXDATA.GetConnection.ConnectionString
    Private _bindingSource As New BindingSource()
    Private _parentScincID As Integer = -1

    Public Event MedicineItemsValueChanged(ByVal sender As Object, ByVal e As MedicineItemsIndexChangedEvent)

    Private _selectedMedicineItemID As Integer
    Public Property MedicineItemID As Integer
        Get
            Return _selectedMedicineItemID
        End Get
        Set(value As Integer)
            If Not Equals(_selectedMedicineItemID, value) Then
                _selectedMedicineItemID = value
                OnPropertyChanged(NameOf(MedicineItemID))
                UpdateMedicineItemIDComboBoxSelection(value)
            End If
        End Set
    End Property

    Private _selectedCommName As String
    Public Property CommName As String
        Get
            Return _selectedCommName
        End Get
        Set(value As String)
            If Not Equals(_selectedCommName, value) Then
                _selectedCommName = value
                OnPropertyChanged(NameOf(CommName))
            End If
        End Set
    End Property

    Public ReadOnly Property MedicineItemsBindingSource As BindingSource
        Get
            Return _bindingSource
        End Get
    End Property

    Public Property ParentScincID As Integer
        Get
            Return _parentScincID
        End Get
        Set(value As Integer)
            If _parentScincID <> value Then
                _parentScincID = value
                _bindingSource.Filter = String.Empty
                ApplyNameFilter()
            End If
        End Set
    End Property

    Private _allRows As New List(Of MedicineItemsCls)()
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
        ComboToolbarLayoutHelper.ApplyPanelToolbar(PanelControl1, btnSerach, BtnAddMedicineItems, _btnSearchVisible, _btnAddVisible, Me)
    End Sub

    <Browsable(True), Category("Data"), Description("Optional substring on commercial name (case-insensitive). Combined with flyout search (AND).")>
    Public Property Filter As String
        Get
            Return _nameFilter
        End Get
        Set(value As String)
            _nameFilter = If(value, String.Empty)
            ApplyNameFilter()
        End Set
    End Property

    Private Function ScopedRows() As List(Of MedicineItemsCls)
        If _parentScincID > -1 Then
            Return _allRows.Where(Function(r) r.ScincID = _parentScincID).ToList()
        End If
        Return _allRows.ToList()
    End Function

    Private Function MatchesFilters(displayName As String, searchContains As String, externalContains As String) As Boolean
        If String.IsNullOrEmpty(displayName) Then Return False
        If Not String.IsNullOrEmpty(externalContains) AndAlso displayName.IndexOf(externalContains, StringComparison.OrdinalIgnoreCase) < 0 Then Return False
        If Not String.IsNullOrEmpty(searchContains) AndAlso displayName.IndexOf(searchContains, StringComparison.OrdinalIgnoreCase) < 0 Then Return False
        Return True
    End Function

    Private Function FilteredRows() As List(Of MedicineItemsCls)
        Dim searchTyped As String = If(txtSearch IsNot Nothing, txtSearch.Text, String.Empty).Trim()
        Dim external As String = _nameFilter.Trim()
        Return ScopedRows().Where(Function(r) MatchesFilters(If(r.CommName, String.Empty), searchTyped, external)).ToList()
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
        If CboMedicineItems Is Nothing OrElse _applyingFilter Then Return
        _applyingFilter = True
        Dim filtered = FilteredRows()
        Dim preserveId As Integer = _selectedMedicineItemID

        Try
            RemoveHandler CboMedicineItems.SelectedIndexChanged, AddressOf CboMedicineItems_SelectedIndexChanged

            CboMedicineItems.Properties.Items.Clear()
            For Each r As MedicineItemsCls In filtered
                CboMedicineItems.Properties.Items.Add(New ComboBoxItem With {.ID = r.MedicineItemID, .Name = r.CommName})
            Next

            UpdateBindingSourceFromDisplayed()

            Dim keepItem As ComboBoxItem = Nothing
            If preserveId > 0 Then
                For Each item As ComboBoxItem In CboMedicineItems.Properties.Items
                    If item.ID = preserveId Then
                        keepItem = item
                        Exit For
                    End If
                Next
            End If

            Dim searchEmpty = String.IsNullOrWhiteSpace(If(txtSearch IsNot Nothing, txtSearch.Text, String.Empty).Trim())
            If keepItem IsNot Nothing Then
                CboMedicineItems.SelectedItem = keepItem
                _selectedMedicineItemID = keepItem.ID
                _selectedCommName = keepItem.Name
            ElseIf searchEmpty AndAlso String.IsNullOrWhiteSpace(_nameFilter.Trim()) AndAlso CboMedicineItems.Properties.Items.Count > 0 Then
                CboMedicineItems.SelectedIndex = 0
                Dim first As ComboBoxItem = TryCast(CboMedicineItems.SelectedItem, ComboBoxItem)
                If first IsNot Nothing Then
                    _selectedMedicineItemID = first.ID
                    _selectedCommName = first.Name
                End If
            Else
                CboMedicineItems.EditValue = Nothing
                _selectedMedicineItemID = 0
                _selectedCommName = Nothing
            End If

            SetTextEditStyle(CboMedicineItems)
        Finally
            AddHandler CboMedicineItems.SelectedIndexChanged, AddressOf CboMedicineItems_SelectedIndexChanged
            _applyingFilter = False
        End Try
    End Sub

    Private Sub BindMedicineItemss()
        Const sql As String = "SELECT MedicineItemID, CommName, ScincID FROM MedicineItems;"
        Using conn As New SqlConnection(_connectionString)
            conn.Open()
            _allRows = conn.Query(Of MedicineItemsCls)(sql).ToList()
        End Using
        ApplyNameFilter()
    End Sub

    Private Sub UpdateMedicineItemIDComboBoxSelection(medicineItemId As Integer)
        ClearSearchBox()
        If medicineItemId <= 0 Then
            CboMedicineItems.EditValue = Nothing
            _selectedCommName = Nothing
            ApplyNameFilter()
            Return
        End If
        Dim row = _allRows.FirstOrDefault(Function(r) r.MedicineItemID = medicineItemId)
        If row Is Nothing Then
            CboMedicineItems.EditValue = Nothing
            _selectedCommName = Nothing
            ApplyNameFilter()
            Return
        End If
        ApplyNameFilter()
        For Each item As ComboBoxItem In CboMedicineItems.Properties.Items
            If item.ID = medicineItemId Then
                CboMedicineItems.SelectedItem = item
                _selectedCommName = item.Name
                Return
            End If
        Next
        CboMedicineItems.EditValue = Nothing
        _selectedCommName = Nothing
    End Sub

    Private Sub CboMedicineItems_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CboMedicineItems.SelectedIndexChanged
        If _applyingFilter Then Return
        Dim selectedItem As ComboBoxItem = TryCast(CboMedicineItems.SelectedItem, ComboBoxItem)
        If selectedItem IsNot Nothing Then
            MedicineItemID = selectedItem.ID
            CommName = selectedItem.Name
            RaiseEvent MedicineItemsValueChanged(sender, New MedicineItemsIndexChangedEvent(MedicineItemID, CommName))
        End If
    End Sub

    Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged
        If _suppressSearchTextChanged OrElse _applyingFilter Then Return
        ApplyNameFilter()
    End Sub

    Private Sub Flyout1_Showing(sender As Object, e As FlyoutPanelEventArgs) Handles Flyout1.Showing
        ComboFlyoutSearchHelper.ApplyManualPositionBelowPanel(Flyout1, PanelControl2, CboMedicineItems)
    End Sub

    Private Sub btnSerach_Click(sender As Object, e As EventArgs) Handles btnSerach.Click
        ComboFlyoutSearchHelper.ShowFlyoutSearchDeferred(Flyout1, PanelControl2, CboMedicineItems, txtSearch, Me)
    End Sub

    Private Sub btnAddMedicineItems_Click(sender As Object, e As EventArgs) Handles BtnAddMedicineItems.Click
        FrmMedicineItems.ShowDialog()
        BindMedicineItemss()
    End Sub

    Public Function GetMedicineItemsTable() As DataTable
        Dim dt As New DataTable()
        dt.Columns.Add("MedicineItemID", GetType(Integer))
        dt.Columns.Add("CommName", GetType(String))
        dt.Columns.Add("ScincID", GetType(Integer))
        For Each r In FilteredRows()
            dt.Rows.Add(r.MedicineItemID, If(r.CommName, CType(String.Empty, Object)), r.ScincID)
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
        BindMedicineItemss()
    End Sub

    Private Sub HandleMedicineItemsValueChanged(ByVal sender As Object, ByVal e As MedicineItemsIndexChangedEvent) Handles Me.MedicineItemsValueChanged
    End Sub

    Public Function GetMedicineItemID(commName As String) As Integer
        If String.IsNullOrWhiteSpace(commName) Then Return -1
        Const sql As String = "SELECT MedicineItemID FROM MedicineItems WHERE CommName = @CommName"
        Using conn As New SqlConnection(_connectionString)
            Dim result As Integer? = conn.QuerySingleOrDefault(Of Integer?)(sql, New With {.CommName = commName})
            Return If(result.HasValue, result.Value, -1)
        End Using
    End Function

    Public Function GetCommName(medicineItemId As Integer) As String
        Const sql As String = "SELECT CommName FROM MedicineItems WHERE MedicineItemID = @MedicineItemID"
        Using conn As New SqlConnection(_connectionString)
            Dim result As String = conn.QuerySingleOrDefault(Of String)(sql, New With {.MedicineItemID = medicineItemId})
            Return If(result, String.Empty)
        End Using
    End Function

    Public Sub SetSelectedCommName(medicineItemId As Integer)
        Me.MedicineItemID = medicineItemId
        UpdateMedicineItemIDComboBoxSelection(medicineItemId)
    End Sub

    Public Sub SetSelectedMedicineItemID(commName As String)
        Me.MedicineItemID = GetMedicineItemID(commName)
        UpdateMedicineItemIDComboBoxSelection(MedicineItemID)
    End Sub

    Public Class ComboBoxItem
        Public Property ID As Integer
        Public Property Name As String
        Public Overrides Function ToString() As String
            Return Name
        End Function
    End Class

    Public Class MedicineItemsCls
        Public Property MedicineItemID As Integer
        Public Property CommName As String
        Public Property ScincID As Integer
    End Class

    Public Class MedicineItemsIndexChangedEvent
        Inherits EventArgs
        Public Property MedicineItemID As Integer
        Public Property CommName As String
        Public Sub New(medicineItemId As Integer, commName As String)
            Me.MedicineItemID = medicineItemId
            Me.CommName = commName
        End Sub
    End Class
End Class
