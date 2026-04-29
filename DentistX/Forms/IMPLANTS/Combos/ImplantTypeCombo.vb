Imports System.ComponentModel
Imports System.Data
Imports System.Data.SqlClient
Imports System.Linq
Imports System.Windows.Forms
Imports Dapper
Imports DevExpress.Utils
Imports DevExpress.XtraEditors

Public Class ImplantTypeCombo
    Implements INotifyPropertyChanged

    Private ReadOnly _connectionString As String = DentistXDATA.GetConnection.connectionString

    Public Event ImplantTypeValueChanged(ByVal sender As Object, ByVal e As ImplantTypeIndexChangedEvent)

    Private _selectedTypeID As Integer
    Public Property TypeID As Integer
        Get
            Return _selectedTypeID
        End Get
        Set(value As Integer)
            If Not Equals(_selectedTypeID, value) Then
                _selectedTypeID = value
                OnPropertyChanged(NameOf(TypeID))
                UpdateTypeIDComboBoxSelection(value)
            End If
        End Set
    End Property

    Private _selectedTypeName As String
    Public Property TypeName As String
        Get
            Return _selectedTypeName
        End Get
        Set(value As String)
            If Not Equals(_selectedTypeName, value) Then
                _selectedTypeName = value
                OnPropertyChanged(NameOf(TypeName))
            End If
        End Set
    End Property

    Private _allRows As New List(Of ImplantTypeCls)()
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
        ComboToolbarLayoutHelper.ApplyPanelToolbar(PanelControl1, btnSerach, BtnAddImplantType, _btnSearchVisible, _btnAddVisible, Me)
    End Sub

    <Browsable(True), Category("Data"), Description("Optional substring on type name (case-insensitive). Combined with flyout search (AND).")>
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
        If CboImplantType Is Nothing OrElse _applyingFilter Then Return
        _applyingFilter = True
        Dim searchTyped As String = If(txtSearch IsNot Nothing, txtSearch.Text, String.Empty).Trim()
        Dim external As String = _nameFilter.Trim()
        Dim filtered = _allRows.Where(Function(r) MatchesFilters(r.TypeName, searchTyped, external)).ToList()
        Dim preserveId As Integer = _selectedTypeID

        Try
            RemoveHandler CboImplantType.SelectedIndexChanged, AddressOf CboImplantType_SelectedIndexChanged

            CboImplantType.Properties.Items.Clear()
            For Each r As ImplantTypeCls In filtered
                CboImplantType.Properties.Items.Add(New ComboBoxItem With {.ID = r.TypeID, .Name = r.TypeName})
            Next

            Dim keepItem As ComboBoxItem = Nothing
            If preserveId > 0 Then
                For Each item As ComboBoxItem In CboImplantType.Properties.Items
                    If item.ID = preserveId Then
                        keepItem = item
                        Exit For
                    End If
                Next
            End If

            If keepItem IsNot Nothing Then
                CboImplantType.SelectedItem = keepItem
                _selectedTypeID = keepItem.ID
                _selectedTypeName = keepItem.Name
            ElseIf String.IsNullOrWhiteSpace(searchTyped) AndAlso String.IsNullOrWhiteSpace(external) AndAlso CboImplantType.Properties.Items.Count > 0 Then
                CboImplantType.SelectedIndex = 0
                Dim first As ComboBoxItem = TryCast(CboImplantType.SelectedItem, ComboBoxItem)
                If first IsNot Nothing Then
                    _selectedTypeID = first.ID
                    _selectedTypeName = first.Name
                End If
            Else
                CboImplantType.EditValue = Nothing
                _selectedTypeID = 0
                _selectedTypeName = Nothing
            End If

            SetTextEditStyle(CboImplantType)
        Finally
            AddHandler CboImplantType.SelectedIndexChanged, AddressOf CboImplantType_SelectedIndexChanged
            _applyingFilter = False
        End Try
    End Sub

    Private Sub BindImplantTypes()
        _allRows = GetImplantTypes()
        ApplyNameFilter()
    End Sub

    Private Sub UpdateTypeIDComboBoxSelection(typeId As Integer)
        ClearSearchBox()
        If typeId <= 0 Then
            CboImplantType.EditValue = Nothing
            _selectedTypeName = Nothing
            ApplyNameFilter()
            Return
        End If
        Dim row = _allRows.FirstOrDefault(Function(r) r.TypeID = typeId)
        If row Is Nothing Then
            CboImplantType.EditValue = Nothing
            _selectedTypeName = Nothing
            ApplyNameFilter()
            Return
        End If
        ApplyNameFilter()
        For Each item As ComboBoxItem In CboImplantType.Properties.Items
            If item.ID = typeId Then
                CboImplantType.SelectedItem = item
                _selectedTypeName = item.Name
                Return
            End If
        Next
        CboImplantType.EditValue = Nothing
        _selectedTypeName = Nothing
    End Sub

    Private Sub CboImplantType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CboImplantType.SelectedIndexChanged
        If _applyingFilter Then Return
        Dim selectedItem As ComboBoxItem = TryCast(CboImplantType.SelectedItem, ComboBoxItem)
        If selectedItem IsNot Nothing Then
            TypeID = selectedItem.ID
            TypeName = selectedItem.Name
            RaiseEvent ImplantTypeValueChanged(sender, New ImplantTypeIndexChangedEvent(TypeID, TypeName))
        End If
    End Sub

    Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged
        If _suppressSearchTextChanged OrElse _applyingFilter Then Return
        ApplyNameFilter()
    End Sub

    Private Sub Flyout1_Showing(sender As Object, e As FlyoutPanelEventArgs) Handles Flyout1.Showing
        ComboFlyoutSearchHelper.ApplyManualPositionBelowPanel(Flyout1, PanelControl2, CboImplantType)
    End Sub

    Private Sub btnSerach_Click(sender As Object, e As EventArgs) Handles btnSerach.Click
        ComboFlyoutSearchHelper.ShowFlyoutSearchDeferred(Flyout1, PanelControl2, CboImplantType, txtSearch, Me)
    End Sub

    Private Sub btnAddImplantType_Click(sender As Object, e As EventArgs) Handles BtnAddImplantType.Click
        FrmImplantType.ShowDialog()
        BindImplantTypes()
    End Sub

    Public Function GetImplantTypeTable() As DataTable
        Dim dt As New DataTable()
        dt.Columns.Add("TypeID", GetType(Integer))
        dt.Columns.Add("TypeName", GetType(String))
        For Each r In _allRows
            dt.Rows.Add(r.TypeID, If(r.TypeName, CType(String.Empty, Object)))
        Next
        Return dt
    End Function

    Private Function GetImplantTypes() As List(Of ImplantTypeCls)
        Const sql As String = "SELECT TypeID, TypeName FROM ImplantType;"
        Using conn As New SqlConnection(_connectionString)
            conn.Open()
            Return conn.Query(Of ImplantTypeCls)(sql).ToList()
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
        BindImplantTypes()
    End Sub

    Private Sub HandleImplantTypeValueChanged(ByVal sender As Object, ByVal e As ImplantTypeIndexChangedEvent) Handles Me.ImplantTypeValueChanged
        Me.TypeID = e.TypeID
        Me.TypeName = e.TypeName
    End Sub

    Public Function GetTypeID(typeName As String) As Integer
        If String.IsNullOrWhiteSpace(typeName) Then Return -1
        Const sql As String = "SELECT TypeID FROM ImplantType WHERE TypeName = @TypeName"
        Using conn As New SqlConnection(_connectionString)
            Dim result As Integer? = conn.QuerySingleOrDefault(Of Integer?)(sql, New With {.TypeName = typeName})
            Return If(result.HasValue, result.Value, -1)
        End Using
    End Function

    Public Function GetTypeName(typeId As Integer) As String
        Const sql As String = "SELECT TypeName FROM ImplantType WHERE TypeID = @TypeID"
        Using conn As New SqlConnection(_connectionString)
            Dim result As String = conn.QuerySingleOrDefault(Of String)(sql, New With {.TypeID = typeId})
            Return If(result, String.Empty)
        End Using
    End Function

    Public Sub SetSelectedTypeName(typeId As Integer)
        Dim n As String = GetTypeName(typeId)
        If Not String.IsNullOrEmpty(n) Then
            Me.TypeID = typeId
            Me.TypeName = n
            UpdateTypeIDComboBoxSelection(typeId)
        End If
    End Sub

    Public Sub SetSelectedTypeID(typeName As String)
        Me.TypeID = GetTypeID(typeName)
        UpdateTypeIDComboBoxSelection(TypeID)
    End Sub

    Public Class ComboBoxItem
        Public Property ID As Integer
        Public Property Name As String
        Public Overrides Function ToString() As String
            Return Name
        End Function
    End Class

    Public Class ImplantTypeCls
        Public Property TypeID As Integer
        Public Property TypeName As String
    End Class

    Public Class ImplantTypeIndexChangedEvent
        Inherits EventArgs
        Public Property TypeID As Integer
        Public Property TypeName As String
        Public Sub New(typeId As Integer, typeName As String)
            Me.TypeID = typeId
            Me.TypeName = typeName
        End Sub
    End Class
End Class
