Imports System.ComponentModel
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports System.Linq
Imports System.Windows.Forms
Imports Dapper
Imports DevExpress.Utils
Imports DevExpress.XtraEditors

Public Class VisCombo
    Implements INotifyPropertyChanged

    Private ReadOnly _connectionString As String = DentistXDATA.GetConnection.ConnectionString

    Public Event VisitTypeValueChanged(ByVal sender As Object, ByVal e As VisitTypeIndexChangedEvent)

    Private _selectedVtID As Integer
    Public Property VtID As Integer
        Get
            Return _selectedVtID
        End Get
        Set(value As Integer)
            If Not Equals(_selectedVtID, value) Then
                _selectedVtID = value
                OnPropertyChanged(NameOf(VtID))
                UpdateVisitTypeComboBoxSelection(value)
            End If
        End Set
    End Property

    Private _selectedVisType As String
    Public Property VisType As String
        Get
            Return _selectedVisType
        End Get
        Set(value As String)
            If Not Equals(_selectedVisType, value) Then
                _selectedVisType = value
                OnPropertyChanged(NameOf(VisType))
            End If
        End Set
    End Property

    Private _allRows As New List(Of VisitTypeRow)()
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
        ComboToolbarLayoutHelper.ApplyPanelToolbar(PanelControl1, btnSerach, BtnAddVisType, _btnSearchVisible, _btnAddVisible, Me)
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
        If CboVisType Is Nothing OrElse _applyingFilter Then Return
        _applyingFilter = True
        Dim searchTyped As String = If(txtSearch IsNot Nothing, txtSearch.Text, String.Empty).Trim()
        Dim external As String = _nameFilter.Trim()
        Dim filtered = _allRows.Where(Function(p) MatchesFilters(p.VisType, searchTyped, external)).ToList()
        Dim preserveId As Integer = _selectedVtID

        Try
            RemoveHandler CboVisType.SelectedIndexChanged, AddressOf CboVisType_SelectedIndexChanged
            CboVisType.Properties.Items.Clear()
            For Each row As VisitTypeRow In filtered
                CboVisType.Properties.Items.Add(New ComboBoxItem With {.ID = row.VtID, .Name = row.VisType})
            Next

            Dim keepItem As ComboBoxItem = Nothing
            If preserveId > 0 Then
                For Each item As ComboBoxItem In CboVisType.Properties.Items
                    If item.ID = preserveId Then
                        keepItem = item
                        Exit For
                    End If
                Next
            End If

            If keepItem IsNot Nothing Then
                CboVisType.SelectedItem = keepItem
                _selectedVtID = keepItem.ID
                _selectedVisType = keepItem.Name
            ElseIf String.IsNullOrWhiteSpace(searchTyped) AndAlso String.IsNullOrWhiteSpace(external) AndAlso CboVisType.Properties.Items.Count > 0 Then
                CboVisType.SelectedIndex = 0
                Dim first As ComboBoxItem = TryCast(CboVisType.SelectedItem, ComboBoxItem)
                If first IsNot Nothing Then
                    _selectedVtID = first.ID
                    _selectedVisType = first.Name
                End If
            Else
                CboVisType.EditValue = Nothing
                _selectedVtID = 0
                _selectedVisType = Nothing
            End If
            SetTextEditStyle(CboVisType)
        Finally
            AddHandler CboVisType.SelectedIndexChanged, AddressOf CboVisType_SelectedIndexChanged
            _applyingFilter = False
        End Try
    End Sub

    Private Sub BindVisTypes()
        _allRows = LoadVisitTypesFromDb()
        ApplyListFilter()
    End Sub

    Private Sub UpdateVisitTypeComboBoxSelection(vtId As Integer)
        ClearSearchBox()
        If vtId <= 0 Then
            CboVisType.EditValue = Nothing
            _selectedVisType = Nothing
            ApplyListFilter()
            Return
        End If
        Dim row = _allRows.FirstOrDefault(Function(p) p.VtID = vtId)
        If row Is Nothing Then
            CboVisType.EditValue = Nothing
            _selectedVisType = Nothing
            ApplyListFilter()
            Return
        End If
        ApplyListFilter()
        For Each item As ComboBoxItem In CboVisType.Properties.Items
            If item.ID = vtId Then
                CboVisType.SelectedItem = item
                _selectedVisType = item.Name
                Return
            End If
        Next
        CboVisType.EditValue = Nothing
        _selectedVisType = Nothing
    End Sub

    Private Sub CboVisType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CboVisType.SelectedIndexChanged
        If _applyingFilter Then Return
        Dim selectedItem As ComboBoxItem = TryCast(CboVisType.SelectedItem, ComboBoxItem)
        If selectedItem IsNot Nothing Then
            VtID = selectedItem.ID
            VisType = selectedItem.Name
            RaiseEvent VisitTypeValueChanged(sender, New VisitTypeIndexChangedEvent(VtID, VisType))
        End If
    End Sub

    Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged
        If _suppressSearchTextChanged OrElse _applyingFilter Then Return
        ApplyListFilter()
    End Sub

    Private Sub Flyout1_Showing(sender As Object, e As FlyoutPanelEventArgs) Handles Flyout1.Showing
        ComboFlyoutSearchHelper.ApplyManualPositionBelowPanel(Flyout1, PanelControl2, CboVisType)
    End Sub

    Private Sub btnSerach_Click(sender As Object, e As EventArgs) Handles btnSerach.Click
        ComboFlyoutSearchHelper.ShowFlyoutSearchDeferred(Flyout1, PanelControl2, CboVisType, txtSearch, Me)
    End Sub

    Private Sub BtnAddVisType_Click(sender As Object, e As EventArgs) Handles BtnAddVisType.Click
        BindVisTypes()
    End Sub

    Public Function GetVisitTypesTable() As DataTable
        Dim rows = LoadVisitTypesFromDb()
        Dim dt As New DataTable()
        dt.Columns.Add("VtID", GetType(Integer))
        dt.Columns.Add("VisType", GetType(String))
        For Each p In rows
            dt.Rows.Add(p.VtID, If(p.VisType, CType(String.Empty, Object)))
        Next
        Return dt
    End Function

    Private Function LoadVisitTypesFromDb() As List(Of VisitTypeRow)
        Const sql As String = "SELECT VtID, VisType FROM VisitType;"
        Using conn As New SqlConnection(_connectionString)
            conn.Open()
            Return conn.Query(Of VisitTypeRow)(sql).ToList()
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
        BindVisTypes()
    End Sub

    Private Sub HandleVisitTypeValueChanged(ByVal sender As Object, ByVal e As VisitTypeIndexChangedEvent) Handles Me.VisitTypeValueChanged
        Me.VtID = e.VtID
        Me.VisType = e.VisType
    End Sub

    Public Function GetVtID(visTypeName As String) As Integer
        If String.IsNullOrWhiteSpace(visTypeName) Then Return -1
        Const sql As String = "SELECT VtID FROM VisitType WHERE VisType = @VisType"
        Using conn As New SqlConnection(_connectionString)
            Dim r As Integer? = conn.QuerySingleOrDefault(Of Integer?)(sql, New With {.VisType = visTypeName})
            Return If(r.HasValue, r.Value, -1)
        End Using
    End Function

    Public Function GetVisTypeName(vtId As Integer) As String
        Const sql As String = "SELECT VisType FROM VisitType WHERE VtID = @VtID"
        Using conn As New SqlConnection(_connectionString)
            Return If(conn.QuerySingleOrDefault(Of String)(sql, New With {.VtID = vtId}), String.Empty)
        End Using
    End Function

    Public Sub SetSelectedVisTypeName(vtId As Integer)
        Dim n As String = GetVisTypeName(vtId)
        If Not String.IsNullOrEmpty(n) Then
            Me.VtID = vtId
            Me.VisType = n
            UpdateVisitTypeComboBoxSelection(vtId)
        End If
    End Sub

    Public Sub SetSelectedVisTypeID(visTypeName As String)
        Dim id As Integer = GetVtID(visTypeName)
        If id <> -1 Then
            Me.VtID = id
            Me.VisType = visTypeName
            UpdateVisitTypeComboBoxSelection(id)
        End If
    End Sub

    Public Sub SetSelectedVisitType(id As Integer)
        Me.VtID = id
    End Sub

    Public Class ComboBoxItem
        Public Property ID As Integer
        Public Property Name As String
        Public Overrides Function ToString() As String
            Return Name
        End Function
    End Class

    Private Class VisitTypeRow
        Public Property VtID As Integer
        Public Property VisType As String
    End Class

    Public Class VisitTypeIndexChangedEvent
        Inherits EventArgs
        Public Property VtID As Integer
        Public Property VisType As String
        Public Sub New(vtId As Integer, visType As String)
            Me.VtID = vtId
            Me.VisType = visType
        End Sub
    End Class
End Class
