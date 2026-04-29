Imports System.ComponentModel
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports System.Linq
Imports System.Windows.Forms
Imports Dapper
Imports DevExpress.Utils
Imports DevExpress.XtraEditors

Public Class BracetsCombo
    Implements INotifyPropertyChanged

    Private ReadOnly _connectionString As String = DentistXDATA.GetConnection.ConnectionString

    Public Event BracetsValueChanged(ByVal sender As Object, ByVal e As BracetsIndexChangedEvent)

    Private _selectedBracetID As Integer
    Public Property BracetID As Integer
        Get
            Return _selectedBracetID
        End Get
        Set(value As Integer)
            If Not Equals(_selectedBracetID, value) Then
                _selectedBracetID = value
                OnPropertyChanged(NameOf(BracetID))
                UpdateBracetIDComboBoxSelection(value)
            End If
        End Set
    End Property

    Private _selectedBracetName As String
    Public Property BracetName As String
        Get
            Return _selectedBracetName
        End Get
        Set(value As String)
            If Not Equals(_selectedBracetName, value) Then
                _selectedBracetName = value
                OnPropertyChanged(NameOf(BracetName))
            End If
        End Set
    End Property

    Private _allRows As New List(Of BracetsCls)()
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
        ComboToolbarLayoutHelper.ApplyPanelToolbar(PanelControl1, btnSerach, BtnAddBracets, _btnSearchVisible, _btnAddVisible, Me)
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
        If CboBracets Is Nothing OrElse _applyingFilter Then Return
        _applyingFilter = True
        Dim searchTyped As String = If(txtSearch IsNot Nothing, txtSearch.Text, String.Empty).Trim()
        Dim external As String = _nameFilter.Trim()
        Dim filtered = _allRows.Where(Function(p) MatchesFilters(p.BracetName, searchTyped, external)).ToList()
        Dim preserveId As Integer = _selectedBracetID

        Try
            RemoveHandler CboBracets.SelectedIndexChanged, AddressOf CboBracets_SelectedIndexChanged
            CboBracets.Properties.Items.Clear()
            For Each row As BracetsCls In filtered
                CboBracets.Properties.Items.Add(New ComboBoxItem With {.ID = row.BracetID, .Name = row.BracetName})
            Next

            Dim keepItem As ComboBoxItem = Nothing
            If preserveId > 0 Then
                For Each item As ComboBoxItem In CboBracets.Properties.Items
                    If item.ID = preserveId Then
                        keepItem = item
                        Exit For
                    End If
                Next
            End If

            If keepItem IsNot Nothing Then
                CboBracets.SelectedItem = keepItem
                _selectedBracetID = keepItem.ID
                _selectedBracetName = keepItem.Name
            ElseIf String.IsNullOrWhiteSpace(searchTyped) AndAlso String.IsNullOrWhiteSpace(external) AndAlso CboBracets.Properties.Items.Count > 0 Then
                CboBracets.SelectedIndex = 0
                Dim first As ComboBoxItem = TryCast(CboBracets.SelectedItem, ComboBoxItem)
                If first IsNot Nothing Then
                    _selectedBracetID = first.ID
                    _selectedBracetName = first.Name
                End If
            Else
                CboBracets.EditValue = Nothing
                _selectedBracetID = 0
                _selectedBracetName = Nothing
            End If
            SetTextEditStyle(CboBracets)
        Finally
            AddHandler CboBracets.SelectedIndexChanged, AddressOf CboBracets_SelectedIndexChanged
            _applyingFilter = False
        End Try
    End Sub

    Private Sub BindBracets()
        Const sql As String = "SELECT BracetID, BracetName FROM Bracets;"
        Using conn As New SqlConnection(_connectionString)
            conn.Open()
            _allRows = conn.Query(Of BracetsCls)(sql).ToList()
        End Using
        ApplyListFilter()
    End Sub

    Private Sub UpdateBracetIDComboBoxSelection(bracetId As Integer)
        ClearSearchBox()
        If bracetId <= 0 Then
            CboBracets.EditValue = Nothing
            _selectedBracetName = Nothing
            ApplyListFilter()
            Return
        End If
        Dim row = _allRows.FirstOrDefault(Function(p) p.BracetID = bracetId)
        If row Is Nothing Then
            CboBracets.EditValue = Nothing
            _selectedBracetName = Nothing
            ApplyListFilter()
            Return
        End If
        ApplyListFilter()
        For Each item As ComboBoxItem In CboBracets.Properties.Items
            If item.ID = bracetId Then
                CboBracets.SelectedItem = item
                _selectedBracetName = item.Name
                Return
            End If
        Next
        CboBracets.EditValue = Nothing
        _selectedBracetName = Nothing
    End Sub

    Private Sub CboBracets_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CboBracets.SelectedIndexChanged
        If _applyingFilter Then Return
        Dim selectedItem As ComboBoxItem = TryCast(CboBracets.SelectedItem, ComboBoxItem)
        If selectedItem IsNot Nothing Then
            BracetID = selectedItem.ID
            BracetName = selectedItem.Name
            RaiseEvent BracetsValueChanged(sender, New BracetsIndexChangedEvent(BracetID, BracetName))
        End If
    End Sub

    Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged
        If _suppressSearchTextChanged OrElse _applyingFilter Then Return
        ApplyListFilter()
    End Sub

    Private Sub Flyout1_Showing(sender As Object, e As FlyoutPanelEventArgs) Handles Flyout1.Showing
        ComboFlyoutSearchHelper.ApplyManualPositionBelowPanel(Flyout1, PanelControl2, CboBracets)
    End Sub

    Private Sub btnSerach_Click(sender As Object, e As EventArgs) Handles btnSerach.Click
        ComboFlyoutSearchHelper.ShowFlyoutSearchDeferred(Flyout1, PanelControl2, CboBracets, txtSearch, Me)
    End Sub

    Private Sub BtnAddBracets_Click(sender As Object, e As EventArgs) Handles BtnAddBracets.Click
        FrmBracets.ShowDialog()
        BindBracets()
    End Sub

    Public Function GetBracetsTable() As DataTable
        Dim dt As New DataTable()
        dt.Columns.Add("BracetID", GetType(Integer))
        dt.Columns.Add("BracetName", GetType(String))
        For Each p In _allRows
            dt.Rows.Add(p.BracetID, If(p.BracetName, CType(String.Empty, Object)))
        Next
        Return dt
    End Function

    Public Function GetBracetIDByBracetName(bracetName As String) As Integer
        If String.IsNullOrWhiteSpace(bracetName) Then Return -1
        Const sql As String = "SELECT BracetID FROM Bracets WHERE BracetName = @BracetName"
        Using conn As New SqlConnection(_connectionString)
            Dim r As Integer? = conn.QuerySingleOrDefault(Of Integer?)(sql, New With {.BracetName = bracetName})
            Return If(r.HasValue, r.Value, -1)
        End Using
    End Function

    Public Function GetBracetNameByBracetID(bracetId As Integer) As String
        Const sql As String = "SELECT BracetName FROM Bracets WHERE BracetID = @BracetID"
        Using conn As New SqlConnection(_connectionString)
            Dim r As String = conn.QuerySingleOrDefault(Of String)(sql, New With {.BracetID = bracetId})
            Return If(r, String.Empty)
        End Using
    End Function

    Public Sub SetBracetNameByBracetID(bracetId As Integer)
        Dim n As String = GetBracetNameByBracetID(bracetId)
        Me.BracetID = bracetId
        Me.BracetName = If(n, String.Empty)
        UpdateBracetIDComboBoxSelection(bracetId)
    End Sub

    Public Sub SetBracetIDByBracetName(bracetName As String)
        Dim id As Integer = GetBracetIDByBracetName(bracetName)
        If id <> -1 Then
            Me.BracetID = id
            Me.BracetName = bracetName
            UpdateBracetIDComboBoxSelection(id)
        End If
    End Sub

    Public Sub RefreshBracets()
        BindBracets()
    End Sub

    Public Sub ClearSelection()
        If CboBracets Is Nothing Then Return
        RemoveHandler CboBracets.SelectedIndexChanged, AddressOf CboBracets_SelectedIndexChanged
        Try
            CboBracets.EditValue = Nothing
            _selectedBracetID = 0
            _selectedBracetName = String.Empty
        Finally
            AddHandler CboBracets.SelectedIndexChanged, AddressOf CboBracets_SelectedIndexChanged
        End Try
        OnPropertyChanged(NameOf(BracetID))
        OnPropertyChanged(NameOf(BracetName))
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
        BindBracets()
    End Sub

    Public Class ComboBoxItem
        Public Property ID As Integer
        Public Property Name As String
        Public Overrides Function ToString() As String
            Return Name
        End Function
    End Class

    Public Class BracetsCls
        Public Property BracetID As Integer
        Public Property BracetName As String
    End Class

    Public Class BracetsIndexChangedEvent
        Inherits EventArgs
        Public Property BracetID As Integer
        Public Property BracetName As String
        Public Sub New(bracetId As Integer, bracetName As String)
            Me.BracetID = bracetId
            Me.BracetName = bracetName
        End Sub
    End Class
End Class
