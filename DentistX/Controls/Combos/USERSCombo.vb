Imports System.ComponentModel
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports System.Linq
Imports System.Windows.Forms
Imports Dapper
Imports DevExpress.Utils
Imports DevExpress.XtraEditors

Public Class USERSCombo
    Implements INotifyPropertyChanged

    Private ReadOnly _connectionString As String = DentistXDATA.GetConnection.ConnectionString

    Public Event USERSValueChanged(ByVal sender As Object, ByVal e As USERSIndexChangedEvent)

    Private _selectedUsID As Integer
    Public Property UsID As Integer
        Get
            Return _selectedUsID
        End Get
        Set(value As Integer)
            If Not Equals(_selectedUsID, value) Then
                _selectedUsID = value
                OnPropertyChanged(NameOf(UsID))
                UpdateUsIDComboBoxSelection(value)
            End If
        End Set
    End Property

    Private _selectedUsName As String
    Public Property UsName As String
        Get
            Return _selectedUsName
        End Get
        Set(value As String)
            If Not Equals(_selectedUsName, value) Then
                _selectedUsName = value
                OnPropertyChanged(NameOf(UsName))
            End If
        End Set
    End Property

    Private _allRows As New List(Of USERSCls)()
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
        ComboToolbarLayoutHelper.ApplyPanelToolbar(PanelControl1, btnSerach, BtnAddUSERS, _btnSearchVisible, _btnAddVisible, Me)
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
        If CboUSERS Is Nothing OrElse _applyingFilter Then Return
        _applyingFilter = True
        Dim searchTyped As String = If(txtSearch IsNot Nothing, txtSearch.Text, String.Empty).Trim()
        Dim external As String = _nameFilter.Trim()
        Dim filtered = _allRows.Where(Function(u) MatchesFilters(u.UsName, searchTyped, external)).ToList()
        Dim preserveId As Integer = _selectedUsID

        Try
            RemoveHandler CboUSERS.SelectedIndexChanged, AddressOf CboUSERS_SelectedIndexChanged
            CboUSERS.Properties.Items.Clear()
            For Each row As USERSCls In filtered
                CboUSERS.Properties.Items.Add(New ComboBoxItem With {.ID = row.UsID, .Name = row.UsName})
            Next

            Dim keepItem As ComboBoxItem = Nothing
            If preserveId > 0 Then
                For Each item As ComboBoxItem In CboUSERS.Properties.Items
                    If item.ID = preserveId Then
                        keepItem = item
                        Exit For
                    End If
                Next
            End If

            If keepItem IsNot Nothing Then
                CboUSERS.SelectedItem = keepItem
                _selectedUsID = keepItem.ID
                _selectedUsName = keepItem.Name
            ElseIf String.IsNullOrWhiteSpace(searchTyped) AndAlso String.IsNullOrWhiteSpace(external) AndAlso CboUSERS.Properties.Items.Count > 0 AndAlso preserveId <> -1 Then
                CboUSERS.SelectedIndex = 0
                Dim first As ComboBoxItem = TryCast(CboUSERS.SelectedItem, ComboBoxItem)
                If first IsNot Nothing Then
                    _selectedUsID = first.ID
                    _selectedUsName = first.Name
                End If
            Else
                CboUSERS.EditValue = Nothing
                If preserveId = -1 Then
                    _selectedUsID = -1
                Else
                    _selectedUsID = 0
                End If
                _selectedUsName = Nothing
            End If
            SetTextEditStyle(CboUSERS)
        Finally
            AddHandler CboUSERS.SelectedIndexChanged, AddressOf CboUSERS_SelectedIndexChanged
            _applyingFilter = False
        End Try
    End Sub

    Private Sub BindUSERS()
        Const sql As String = "SELECT UsID, UsName FROM USERS;"
        Using conn As New SqlConnection(_connectionString)
            conn.Open()
            _allRows = conn.Query(Of USERSCls)(sql).ToList()
        End Using
        ApplyListFilter()
    End Sub

    Private Sub UpdateUsIDComboBoxSelection(usId As Integer)
        ClearSearchBox()
        If usId <= 0 Then
            CboUSERS.EditValue = Nothing
            _selectedUsName = Nothing
            ApplyListFilter()
            Return
        End If
        Dim row = _allRows.FirstOrDefault(Function(u) u.UsID = usId)
        If row Is Nothing Then
            CboUSERS.EditValue = Nothing
            _selectedUsName = Nothing
            ApplyListFilter()
            Return
        End If
        ApplyListFilter()
        For Each item As ComboBoxItem In CboUSERS.Properties.Items
            If item.ID = usId Then
                CboUSERS.SelectedItem = item
                _selectedUsName = item.Name
                Return
            End If
        Next
        CboUSERS.EditValue = Nothing
        _selectedUsName = Nothing
    End Sub

    Private Sub CboUSERS_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CboUSERS.SelectedIndexChanged
        If _applyingFilter Then Return
        Dim selectedItem As ComboBoxItem = TryCast(CboUSERS.SelectedItem, ComboBoxItem)
        If selectedItem IsNot Nothing Then
            UsID = selectedItem.ID
            UsName = selectedItem.Name
            RaiseEvent USERSValueChanged(sender, New USERSIndexChangedEvent(UsID, UsName))
        End If
    End Sub

    Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged
        If _suppressSearchTextChanged OrElse _applyingFilter Then Return
        ApplyListFilter()
    End Sub

    Private Sub Flyout1_Showing(sender As Object, e As FlyoutPanelEventArgs) Handles Flyout1.Showing
        ComboFlyoutSearchHelper.ApplyManualPositionBelowPanel(Flyout1, PanelControl2, CboUSERS)
    End Sub

    Private Sub btnSerach_Click(sender As Object, e As EventArgs) Handles btnSerach.Click
        ComboFlyoutSearchHelper.ShowFlyoutSearchDeferred(Flyout1, PanelControl2, CboUSERS, txtSearch, Me)
    End Sub

    Private Sub BtnAddUSERS_Click(sender As Object, e As EventArgs) Handles BtnAddUSERS.Click
        FrmAddNewUser.ShowDialog()
        BindUSERS()
    End Sub

    Public Function GetUSERSTable() As DataTable
        Dim dt As New DataTable()
        dt.Columns.Add("UsID", GetType(Integer))
        dt.Columns.Add("UsName", GetType(String))
        For Each u In _allRows
            dt.Rows.Add(u.UsID, If(u.UsName, CType(String.Empty, Object)))
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
        BindUSERS()
    End Sub

    Private Sub HandleUSERSValueChanged(sender As Object, e As USERSIndexChangedEvent) Handles Me.USERSValueChanged
        Me.UsID = e.UsID
        Me.UsName = e.UsName
    End Sub

    Public Function GetUserIDByName(usName As String) As Integer
        If String.IsNullOrWhiteSpace(usName) Then Return -1
        Const sql As String = "SELECT UsID FROM USERS WHERE UsName = @UsName"
        Using conn As New SqlConnection(_connectionString)
            Dim r As Integer? = conn.QuerySingleOrDefault(Of Integer?)(sql, New With {.UsName = usName})
            Return If(r.HasValue, r.Value, -1)
        End Using
    End Function

    Public Function GetUserNameByID(usId As Integer) As String
        Const sql As String = "SELECT UsName FROM USERS WHERE UsID = @UsID"
        Using conn As New SqlConnection(_connectionString)
            Return If(conn.QuerySingleOrDefault(Of String)(sql, New With {.UsID = usId}), String.Empty)
        End Using
    End Function

    Public Sub ResetSelection()
        _selectedUsID = -1
        _selectedUsName = String.Empty
        OnPropertyChanged(NameOf(UsID))
        OnPropertyChanged(NameOf(UsName))
        ApplyListFilter()
    End Sub

    Public Sub SetSelectedUserName(usName As String)
        Dim id As Integer = GetUserIDByName(usName)
        If id <> -1 Then
            Me.UsID = id
            Me.UsName = usName
            UpdateUsIDComboBoxSelection(id)
        End If
    End Sub

    Public Sub SetSelectedUserID(usId As Integer)
        Dim n As String = GetUserNameByID(usId)
        If Not String.IsNullOrEmpty(n) Then
            Me.UsID = usId
            Me.UsName = n
            UpdateUsIDComboBoxSelection(usId)
        End If
    End Sub

    Public Class ComboBoxItem
        Public Property ID As Integer
        Public Property Name As String
        Public Overrides Function ToString() As String
            Return Name
        End Function
    End Class

    Public Class USERSCls
        Public Property UsID As Integer
        Public Property UsName As String
    End Class

    Public Class USERSIndexChangedEvent
        Inherits EventArgs
        Public Property UsID As Integer
        Public Property UsName As String
        Public Sub New(usId As Integer, usName As String)
            Me.UsID = usId
            Me.UsName = usName
        End Sub
    End Class
End Class
