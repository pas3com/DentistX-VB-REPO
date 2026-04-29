Imports System.ComponentModel
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports System.Linq
Imports System.Windows.Forms
Imports Dapper
Imports DevExpress.Utils
Imports DevExpress.XtraEditors

Public Class SecretariesCombo
    Implements INotifyPropertyChanged

    Private ReadOnly _connectionString As String = DentistXDATA.GetConnection.ConnectionString

    Public Event SecretariesValueChanged(ByVal sender As Object, ByVal e As SecretariesIndexChangedEvent)

    Private _selectedSecID As Integer
    Public Property SecID As Integer
        Get
            Return _selectedSecID
        End Get
        Set(value As Integer)
            If Not Equals(_selectedSecID, value) Then
                _selectedSecID = value
                OnPropertyChanged(NameOf(SecID))
                UpdateSecIDComboBoxSelection(value)
            End If
        End Set
    End Property

    Private _selectedSecName As String
    Public Property SecName As String
        Get
            Return _selectedSecName
        End Get
        Set(value As String)
            If Not Equals(_selectedSecName, value) Then
                _selectedSecName = value
                OnPropertyChanged(NameOf(SecName))
            End If
        End Set
    End Property

    Private _allRows As New List(Of SecretariesCls)()
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
        ComboToolbarLayoutHelper.ApplyPanelToolbar(PanelControl1, btnSerach, BtnAddSecretaries, _btnSearchVisible, _btnAddVisible, Me)
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
        If CboSecretaries Is Nothing OrElse _applyingFilter Then Return
        _applyingFilter = True
        Dim searchTyped As String = If(txtSearch IsNot Nothing, txtSearch.Text, String.Empty).Trim()
        Dim external As String = _nameFilter.Trim()
        Dim filtered = _allRows.Where(Function(p) MatchesFilters(p.SecName, searchTyped, external)).ToList()
        Dim preserveId As Integer = _selectedSecID

        Try
            RemoveHandler CboSecretaries.SelectedIndexChanged, AddressOf CboSecretaries_SelectedIndexChanged
            CboSecretaries.Properties.Items.Clear()
            For Each row As SecretariesCls In filtered
                CboSecretaries.Properties.Items.Add(New ComboBoxItem With {.ID = row.SecID, .Name = row.SecName})
            Next

            Dim keepItem As ComboBoxItem = Nothing
            If preserveId > 0 Then
                For Each item As ComboBoxItem In CboSecretaries.Properties.Items
                    If item.ID = preserveId Then
                        keepItem = item
                        Exit For
                    End If
                Next
            End If

            If keepItem IsNot Nothing Then
                CboSecretaries.SelectedItem = keepItem
                _selectedSecID = keepItem.ID
                _selectedSecName = keepItem.Name
            ElseIf String.IsNullOrWhiteSpace(searchTyped) AndAlso String.IsNullOrWhiteSpace(external) AndAlso CboSecretaries.Properties.Items.Count > 0 Then
                CboSecretaries.SelectedIndex = 0
                Dim first As ComboBoxItem = TryCast(CboSecretaries.SelectedItem, ComboBoxItem)
                If first IsNot Nothing Then
                    _selectedSecID = first.ID
                    _selectedSecName = first.Name
                End If
            Else
                CboSecretaries.EditValue = Nothing
                _selectedSecID = 0
                _selectedSecName = Nothing
            End If
            SetTextEditStyle(CboSecretaries)
        Finally
            AddHandler CboSecretaries.SelectedIndexChanged, AddressOf CboSecretaries_SelectedIndexChanged
            _applyingFilter = False
        End Try
    End Sub

    Private Sub BindSecretaries()
        Const sql As String = "SELECT SecID, SecName FROM Secretaries;"
        Using conn As New SqlConnection(_connectionString)
            conn.Open()
            _allRows = conn.Query(Of SecretariesCls)(sql).ToList()
        End Using
        ApplyListFilter()
    End Sub

    Private Sub UpdateSecIDComboBoxSelection(secId As Integer)
        ClearSearchBox()
        If secId <= 0 Then
            CboSecretaries.EditValue = Nothing
            _selectedSecName = Nothing
            ApplyListFilter()
            Return
        End If
        Dim row = _allRows.FirstOrDefault(Function(p) p.SecID = secId)
        If row Is Nothing Then
            CboSecretaries.EditValue = Nothing
            _selectedSecName = Nothing
            ApplyListFilter()
            Return
        End If
        ApplyListFilter()
        For Each item As ComboBoxItem In CboSecretaries.Properties.Items
            If item.ID = secId Then
                CboSecretaries.SelectedItem = item
                _selectedSecName = item.Name
                Return
            End If
        Next
        CboSecretaries.EditValue = Nothing
        _selectedSecName = Nothing
    End Sub

    Private Sub CboSecretaries_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CboSecretaries.SelectedIndexChanged
        If _applyingFilter Then Return
        Dim selectedItem As ComboBoxItem = TryCast(CboSecretaries.SelectedItem, ComboBoxItem)
        If selectedItem IsNot Nothing Then
            SecID = selectedItem.ID
            SecName = selectedItem.Name
            RaiseEvent SecretariesValueChanged(sender, New SecretariesIndexChangedEvent(SecID, SecName))
        End If
    End Sub

    Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged
        If _suppressSearchTextChanged OrElse _applyingFilter Then Return
        ApplyListFilter()
    End Sub

    Private Sub Flyout1_Showing(sender As Object, e As FlyoutPanelEventArgs) Handles Flyout1.Showing
        ComboFlyoutSearchHelper.ApplyManualPositionBelowPanel(Flyout1, PanelControl2, CboSecretaries)
    End Sub

    Private Sub btnSerach_Click(sender As Object, e As EventArgs) Handles btnSerach.Click
        ComboFlyoutSearchHelper.ShowFlyoutSearchDeferred(Flyout1, PanelControl2, CboSecretaries, txtSearch, Me)
    End Sub

    Private Sub BtnAddSecretaries_Click(sender As Object, e As EventArgs) Handles BtnAddSecretaries.Click
        FrmSecretaries.ShowDialog()
        BindSecretaries()
    End Sub

    Public Function GetSecretariesTable() As DataTable
        Dim dt As New DataTable()
        dt.Columns.Add("SecID", GetType(Integer))
        dt.Columns.Add("SecName", GetType(String))
        For Each p In _allRows
            dt.Rows.Add(p.SecID, If(p.SecName, CType(String.Empty, Object)))
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
        BindSecretaries()
    End Sub

    Private Sub HandleSecretariesValueChanged(sender As Object, e As SecretariesIndexChangedEvent) Handles Me.SecretariesValueChanged
        Me.SecID = e.SecID
        Me.SecName = e.SecName
    End Sub

    Public Function GetSecID(secName As String) As Integer
        If String.IsNullOrWhiteSpace(secName) Then Return -1
        Const sql As String = "SELECT SecID FROM Secretaries WHERE SecName = @SecName"
        Using conn As New SqlConnection(_connectionString)
            Dim r As Integer? = conn.QuerySingleOrDefault(Of Integer?)(sql, New With {.SecName = secName})
            Return If(r.HasValue, r.Value, -1)
        End Using
    End Function

    Public Function GetSecName(secId As Integer) As String
        Const sql As String = "SELECT SecName FROM Secretaries WHERE SecID = @SecID"
        Using conn As New SqlConnection(_connectionString)
            Return If(conn.QuerySingleOrDefault(Of String)(sql, New With {.SecID = secId}), String.Empty)
        End Using
    End Function

    Public Sub SetSelectedSecName(secId As Integer)
        Dim n As String = GetSecName(secId)
        If Not String.IsNullOrEmpty(n) Then
            Me.SecID = secId
            Me.SecName = n
            UpdateSecIDComboBoxSelection(secId)
        End If
    End Sub

    Public Sub SetSelectedSecID(secName As String)
        Dim id As Integer = GetSecID(secName)
        If id <> -1 Then
            Me.SecID = id
            Me.SecName = secName
            UpdateSecIDComboBoxSelection(id)
        End If
    End Sub

    Public Class ComboBoxItem
        Public Property ID As Integer
        Public Property Name As String
        Public Overrides Function ToString() As String
            Return Name
        End Function
    End Class

    Public Class SecretariesCls
        Public Property SecID As Integer
        Public Property SecName As String
    End Class

    Public Class SecretariesIndexChangedEvent
        Inherits EventArgs
        Public Property SecID As Integer
        Public Property SecName As String
        Public Sub New(secId As Integer, secName As String)
            Me.SecID = secId
            Me.SecName = secName
        End Sub
    End Class
End Class
