Imports System.ComponentModel
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports System.Linq
Imports System.Windows.Forms
Imports Dapper
Imports DevExpress.Utils
Imports DevExpress.XtraEditors

Public Class WireTCombo
    Implements INotifyPropertyChanged

    Private ReadOnly _connectionString As String = DentistXDATA.GetConnection.ConnectionString

    Public Event WireTypeValueChanged(ByVal sender As Object, ByVal e As WireTypeIndexChangedEvent)

    Private _selectedTypeID As Integer
    Public Property TypeID As Integer
        Get
            Return _selectedTypeID
        End Get
        Set(value As Integer)
            If Not Equals(_selectedTypeID, value) Then
                _selectedTypeID = value
                OnPropertyChanged(NameOf(TypeID))
                UpdateWireTypeComboBoxSelection(value)
            End If
        End Set
    End Property

    Private _selectedWireType As String
    Public Property WireType As String
        Get
            Return _selectedWireType
        End Get
        Set(value As String)
            If Not Equals(_selectedWireType, value) Then
                _selectedWireType = value
                OnPropertyChanged(NameOf(WireType))
            End If
        End Set
    End Property

    Private _allRows As New List(Of WireTypeRow)()
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
        ComboToolbarLayoutHelper.ApplyPanelToolbar(PanelControl1, btnSerach, BtnAddWireType, _btnSearchVisible, _btnAddVisible, Me)
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
        If CboWireType Is Nothing OrElse _applyingFilter Then Return
        _applyingFilter = True
        Dim searchTyped As String = If(txtSearch IsNot Nothing, txtSearch.Text, String.Empty).Trim()
        Dim external As String = _nameFilter.Trim()
        Dim filtered = _allRows.Where(Function(p) MatchesFilters(p.WireType, searchTyped, external)).ToList()
        Dim preserveId As Integer = _selectedTypeID

        Try
            RemoveHandler CboWireType.SelectedIndexChanged, AddressOf CboWireType_SelectedIndexChanged
            CboWireType.Properties.Items.Clear()
            For Each row As WireTypeRow In filtered
                CboWireType.Properties.Items.Add(New ComboBoxItem With {.ID = row.TypeID, .Name = row.WireType})
            Next

            Dim keepItem As ComboBoxItem = Nothing
            If preserveId > 0 Then
                For Each item As ComboBoxItem In CboWireType.Properties.Items
                    If item.ID = preserveId Then
                        keepItem = item
                        Exit For
                    End If
                Next
            End If

            If keepItem IsNot Nothing Then
                CboWireType.SelectedItem = keepItem
                _selectedTypeID = keepItem.ID
                _selectedWireType = keepItem.Name
            ElseIf String.IsNullOrWhiteSpace(searchTyped) AndAlso String.IsNullOrWhiteSpace(external) AndAlso CboWireType.Properties.Items.Count > 0 Then
                CboWireType.SelectedIndex = 0
                Dim first As ComboBoxItem = TryCast(CboWireType.SelectedItem, ComboBoxItem)
                If first IsNot Nothing Then
                    _selectedTypeID = first.ID
                    _selectedWireType = first.Name
                End If
            Else
                CboWireType.EditValue = Nothing
                _selectedTypeID = 0
                _selectedWireType = Nothing
            End If
            SetTextEditStyle(CboWireType)
        Finally
            AddHandler CboWireType.SelectedIndexChanged, AddressOf CboWireType_SelectedIndexChanged
            _applyingFilter = False
        End Try
    End Sub

    Private Sub BindWireTypes()
        _allRows = LoadWireTypesFromDb()
        ApplyListFilter()
    End Sub

    Private Sub UpdateWireTypeComboBoxSelection(typeId As Integer)
        ClearSearchBox()
        If typeId <= 0 Then
            CboWireType.EditValue = Nothing
            _selectedWireType = Nothing
            ApplyListFilter()
            Return
        End If
        Dim row = _allRows.FirstOrDefault(Function(p) p.TypeID = typeId)
        If row Is Nothing Then
            CboWireType.EditValue = Nothing
            _selectedWireType = Nothing
            ApplyListFilter()
            Return
        End If
        ApplyListFilter()
        For Each item As ComboBoxItem In CboWireType.Properties.Items
            If item.ID = typeId Then
                CboWireType.SelectedItem = item
                _selectedWireType = item.Name
                Return
            End If
        Next
        CboWireType.EditValue = Nothing
        _selectedWireType = Nothing
    End Sub

    Private Sub CboWireType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CboWireType.SelectedIndexChanged
        If _applyingFilter Then Return
        Dim selectedItem As ComboBoxItem = TryCast(CboWireType.SelectedItem, ComboBoxItem)
        If selectedItem IsNot Nothing Then
            TypeID = selectedItem.ID
            WireType = selectedItem.Name
            RaiseEvent WireTypeValueChanged(sender, New WireTypeIndexChangedEvent(TypeID, WireType))
        End If
    End Sub

    Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged
        If _suppressSearchTextChanged OrElse _applyingFilter Then Return
        ApplyListFilter()
    End Sub

    Private Sub Flyout1_Showing(sender As Object, e As FlyoutPanelEventArgs) Handles Flyout1.Showing
        ComboFlyoutSearchHelper.ApplyManualPositionBelowPanel(Flyout1, PanelControl2, CboWireType)
    End Sub

    Private Sub btnSerach_Click(sender As Object, e As EventArgs) Handles btnSerach.Click
        ComboFlyoutSearchHelper.ShowFlyoutSearchDeferred(Flyout1, PanelControl2, CboWireType, txtSearch, Me)
    End Sub

    Private Sub BtnAddWireType_Click(sender As Object, e As EventArgs) Handles BtnAddWireType.Click
        Dim f As New FrmTblWireType()
        f.Icon = MainView3.Icon
        f.ShowDialog(Me)
        BindWireTypes()
    End Sub

    Public Function GetWireTypesTable() As DataTable
        Dim rows = LoadWireTypesFromDb()
        Dim dt As New DataTable()
        dt.Columns.Add("TypeID", GetType(Integer))
        dt.Columns.Add("WireType", GetType(String))
        For Each p In rows
            dt.Rows.Add(p.TypeID, If(p.WireType, CType(String.Empty, Object)))
        Next
        Return dt
    End Function

    Private Function LoadWireTypesFromDb() As List(Of WireTypeRow)
        Const sql As String = "SELECT TypeID, WireType FROM TblWireType;"
        Using conn As New SqlConnection(_connectionString)
            conn.Open()
            Return conn.Query(Of WireTypeRow)(sql).ToList()
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
        BindWireTypes()
    End Sub

    Private Sub HandleWireTypeValueChanged(ByVal sender As Object, ByVal e As WireTypeIndexChangedEvent) Handles Me.WireTypeValueChanged
        Me.TypeID = e.TypeID
        Me.WireType = e.WireType
    End Sub

    Public Function GetTypeID(wireTypeName As String) As Integer
        If String.IsNullOrWhiteSpace(wireTypeName) Then Return -1
        Const sql As String = "SELECT TypeID FROM TblWireType WHERE WireType = @WireType"
        Using conn As New SqlConnection(_connectionString)
            Dim r As Integer? = conn.QuerySingleOrDefault(Of Integer?)(sql, New With {.WireType = wireTypeName})
            Return If(r.HasValue, r.Value, -1)
        End Using
    End Function

    Public Function GetWireTypeName(typeId As Integer) As String
        Const sql As String = "SELECT WireType FROM TblWireType WHERE TypeID = @TypeID"
        Using conn As New SqlConnection(_connectionString)
            Return If(conn.QuerySingleOrDefault(Of String)(sql, New With {.TypeID = typeId}), String.Empty)
        End Using
    End Function

    Public Sub SetSelectedWireTypeName(typeId As Integer)
        Dim n As String = GetWireTypeName(typeId)
        If Not String.IsNullOrEmpty(n) Then
            Me.TypeID = typeId
            Me.WireType = n
            UpdateWireTypeComboBoxSelection(typeId)
        End If
    End Sub

    Public Sub SetSelectedWireTypeID(wireTypeName As String)
        Dim id As Integer = GetTypeID(wireTypeName)
        If id <> -1 Then
            Me.TypeID = id
            Me.WireType = wireTypeName
            UpdateWireTypeComboBoxSelection(id)
        End If
    End Sub

    Public Sub SetSelectedWireType(id As Integer)
        Me.TypeID = id
    End Sub

    Public Class ComboBoxItem
        Public Property ID As Integer
        Public Property Name As String
        Public Overrides Function ToString() As String
            Return Name
        End Function
    End Class

    Private Class WireTypeRow
        Public Property TypeID As Integer
        Public Property WireType As String
    End Class

    Public Class WireTypeIndexChangedEvent
        Inherits EventArgs
        Public Property TypeID As Integer
        Public Property WireType As String
        Public Sub New(typeId As Integer, wireType As String)
            Me.TypeID = typeId
            Me.WireType = wireType
        End Sub
    End Class
End Class

