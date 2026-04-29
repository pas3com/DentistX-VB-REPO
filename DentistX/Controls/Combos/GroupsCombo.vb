Imports System.ComponentModel
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports System.Linq
Imports System.Windows.Forms
Imports Dapper
Imports DevExpress.Utils
Imports DevExpress.XtraEditors

Public Class GroupsCombo
    Implements INotifyPropertyChanged

    Private ReadOnly _connectionString As String = DentistXDATA.GetConnection.ConnectionString

    Public Event GroupsValueChanged(ByVal sender As Object, ByVal e As GroupsIndexChangedEvent)

    Private _selectedGroupID As Integer
    Public Property GroupID As Integer
        Get
            Return _selectedGroupID
        End Get
        Set(value As Integer)
            If Not Equals(_selectedGroupID, value) Then
                _selectedGroupID = value
                OnPropertyChanged(NameOf(GroupID))
                UpdateGroupIDComboBoxSelection(value)
            End If
        End Set
    End Property

    Private _selectedGroupName As String
    Public Property GroupName As String
        Get
            Return _selectedGroupName
        End Get
        Set(value As String)
            If Not Equals(_selectedGroupName, value) Then
                _selectedGroupName = value
                OnPropertyChanged(NameOf(GroupName))
            End If
        End Set
    End Property

    Private _allRows As New List(Of GroupsCls)()
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
        ComboToolbarLayoutHelper.ApplyPanelToolbar(PanelControl1, btnSerach, BtnAddGroups, _btnSearchVisible, _btnAddVisible, Me)
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
        If CboGroups Is Nothing OrElse _applyingFilter Then Return
        _applyingFilter = True
        Dim searchTyped As String = If(txtSearch IsNot Nothing, txtSearch.Text, String.Empty).Trim()
        Dim external As String = _nameFilter.Trim()
        Dim filtered = _allRows.Where(Function(g) MatchesFilters(g.GroupName, searchTyped, external)).ToList()
        Dim preserveId As Integer = _selectedGroupID

        Try
            RemoveHandler CboGroups.SelectedIndexChanged, AddressOf CboGroups_SelectedIndexChanged
            CboGroups.Properties.Items.Clear()
            For Each row As GroupsCls In filtered
                CboGroups.Properties.Items.Add(New ComboBoxItem With {.ID = row.GroupID, .Name = row.GroupName})
            Next

            Dim keepItem As ComboBoxItem = Nothing
            If preserveId > 0 Then
                For Each item As ComboBoxItem In CboGroups.Properties.Items
                    If item.ID = preserveId Then
                        keepItem = item
                        Exit For
                    End If
                Next
            End If

            If keepItem IsNot Nothing Then
                CboGroups.SelectedItem = keepItem
                _selectedGroupID = keepItem.ID
                _selectedGroupName = keepItem.Name
            ElseIf String.IsNullOrWhiteSpace(searchTyped) AndAlso String.IsNullOrWhiteSpace(external) AndAlso CboGroups.Properties.Items.Count > 0 AndAlso preserveId <> -1 Then
                CboGroups.SelectedIndex = 0
                Dim first As ComboBoxItem = TryCast(CboGroups.SelectedItem, ComboBoxItem)
                If first IsNot Nothing Then
                    _selectedGroupID = first.ID
                    _selectedGroupName = first.Name
                End If
            Else
                CboGroups.EditValue = Nothing
                If preserveId = -1 Then
                    _selectedGroupID = -1
                Else
                    _selectedGroupID = 0
                End If
                _selectedGroupName = Nothing
            End If
            SetTextEditStyle(CboGroups)
        Finally
            AddHandler CboGroups.SelectedIndexChanged, AddressOf CboGroups_SelectedIndexChanged
            _applyingFilter = False
        End Try
    End Sub

    Private Sub BindGroups()
        Const sql As String = "SELECT GroupID, GroupName FROM Groups;"
        Using conn As New SqlConnection(_connectionString)
            conn.Open()
            _allRows = conn.Query(Of GroupsCls)(sql).ToList()
        End Using
        ApplyListFilter()
    End Sub

    Private Sub UpdateGroupIDComboBoxSelection(groupId As Integer)
        ClearSearchBox()
        If groupId <= 0 Then
            CboGroups.EditValue = Nothing
            _selectedGroupName = Nothing
            ApplyListFilter()
            Return
        End If
        Dim row = _allRows.FirstOrDefault(Function(g) g.GroupID = groupId)
        If row Is Nothing Then
            CboGroups.EditValue = Nothing
            _selectedGroupName = Nothing
            ApplyListFilter()
            Return
        End If
        ApplyListFilter()
        For Each item As ComboBoxItem In CboGroups.Properties.Items
            If item.ID = groupId Then
                CboGroups.SelectedItem = item
                _selectedGroupName = item.Name
                Return
            End If
        Next
        CboGroups.EditValue = Nothing
        _selectedGroupName = Nothing
    End Sub

    Private Sub CboGroups_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CboGroups.SelectedIndexChanged
        If _applyingFilter Then Return
        Dim selectedItem As ComboBoxItem = TryCast(CboGroups.SelectedItem, ComboBoxItem)
        If selectedItem IsNot Nothing Then
            GroupID = selectedItem.ID
            GroupName = selectedItem.Name
            RaiseEvent GroupsValueChanged(sender, New GroupsIndexChangedEvent(GroupID, GroupName))
        End If
    End Sub

    Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged
        If _suppressSearchTextChanged OrElse _applyingFilter Then Return
        ApplyListFilter()
    End Sub

    Private Sub Flyout1_Showing(sender As Object, e As FlyoutPanelEventArgs) Handles Flyout1.Showing
        ComboFlyoutSearchHelper.ApplyManualPositionBelowPanel(Flyout1, PanelControl2, CboGroups)
    End Sub

    Private Sub btnSerach_Click(sender As Object, e As EventArgs) Handles btnSerach.Click
        ComboFlyoutSearchHelper.ShowFlyoutSearchDeferred(Flyout1, PanelControl2, CboGroups, txtSearch, Me)
    End Sub

    Private Sub BtnAddGroups_Click(sender As Object, e As EventArgs) Handles BtnAddGroups.Click
        FrmGroups.ShowDialog()
        BindGroups()
    End Sub

    Public Function GetGroupsTable() As DataTable
        Dim dt As New DataTable()
        dt.Columns.Add("GroupID", GetType(Integer))
        dt.Columns.Add("GroupName", GetType(String))
        For Each g In _allRows
            dt.Rows.Add(g.GroupID, If(g.GroupName, CType(String.Empty, Object)))
        Next
        Return dt
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
        BindGroups()
    End Sub

    Private Sub HandleGroupsValueChanged(sender As Object, e As GroupsIndexChangedEvent) Handles Me.GroupsValueChanged
        Me.GroupID = e.GroupID
        Me.GroupName = e.GroupName
    End Sub

    Public Function GetGroupID(groupName As String) As Integer
        If String.IsNullOrWhiteSpace(groupName) Then Return -1
        Const sql As String = "SELECT GroupID FROM Groups WHERE GroupName = @GroupName"
        Using conn As New SqlConnection(_connectionString)
            Dim r As Integer? = conn.QuerySingleOrDefault(Of Integer?)(sql, New With {.GroupName = groupName})
            Return If(r.HasValue, r.Value, -1)
        End Using
    End Function

    Public Function GetGroupName(groupId As Integer) As String
        Const sql As String = "SELECT GroupName FROM Groups WHERE GroupID = @GroupID"
        Using conn As New SqlConnection(_connectionString)
            Return If(conn.QuerySingleOrDefault(Of String)(sql, New With {.GroupID = groupId}), String.Empty)
        End Using
    End Function

    Public Sub ResetSelection()
        _selectedGroupID = -1
        _selectedGroupName = String.Empty
        OnPropertyChanged(NameOf(GroupID))
        OnPropertyChanged(NameOf(GroupName))
        ApplyListFilter()
    End Sub

    Public Sub ClearSelection()
        RemoveHandler CboGroups.SelectedIndexChanged, AddressOf CboGroups_SelectedIndexChanged
        Try
            _selectedGroupID = 0
            _selectedGroupName = String.Empty
            OnPropertyChanged(NameOf(GroupID))
            OnPropertyChanged(NameOf(GroupName))
            CboGroups.EditValue = Nothing
            CboGroups.SelectedIndex = -1
        Finally
            AddHandler CboGroups.SelectedIndexChanged, AddressOf CboGroups_SelectedIndexChanged
        End Try
    End Sub

    Public Sub RefreshGroups()
        BindGroups()
    End Sub

    Public Sub SetSelectedGroupID(groupId As Integer)
        Dim n As String = GetGroupName(groupId)
        If Not String.IsNullOrEmpty(n) Then
            Me.GroupID = groupId
            Me.GroupName = n
            UpdateGroupIDComboBoxSelection(groupId)
        End If
    End Sub

    Public Sub SetSelectedGroupName(groupName As String)
        Dim id As Integer = GetGroupID(groupName)
        If id <> -1 Then
            Me.GroupID = id
            Me.GroupName = groupName
            UpdateGroupIDComboBoxSelection(id)
        End If
    End Sub

    Public Class ComboBoxItem
        Public Property ID As Integer
        Public Property Name As String
        Public Overrides Function ToString() As String
            Return Name
        End Function
    End Class

    Public Class GroupsCls
        Public Property GroupID As Integer
        Public Property GroupName As String
    End Class

    Public Class GroupsIndexChangedEvent
        Inherits EventArgs
        Public Property GroupID As Integer
        Public Property GroupName As String
        Public Sub New(groupId As Integer, groupName As String)
            Me.GroupID = groupId
            Me.GroupName = groupName
        End Sub
    End Class
End Class
