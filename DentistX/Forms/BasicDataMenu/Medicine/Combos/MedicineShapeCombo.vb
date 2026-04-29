Imports System.ComponentModel
Imports System.Data
Imports System.Data.SqlClient
Imports System.Linq
Imports System.Windows.Forms
Imports Dapper
Imports DevExpress.Utils
Imports DevExpress.XtraEditors

Public Class MedicineShapeCombo
    Implements INotifyPropertyChanged

    Private ReadOnly _connectionString As String = DentistXDATA.GetConnection.ConnectionString
    Private _bindingSource As New BindingSource()
    Private _parentMedicineItemID As Integer = -1

    Public Event MedicineShapeValueChanged(ByVal sender As Object, ByVal e As MedicineShapeIndexChangedEvent)

    Private _selectedShapeID As Integer
    Public Property ShapeID As Integer
        Get
            Return _selectedShapeID
        End Get
        Set(value As Integer)
            If Not Equals(_selectedShapeID, value) Then
                _selectedShapeID = value
                OnPropertyChanged(NameOf(ShapeID))
                UpdateShapeIDComboBoxSelection(value)
            End If
        End Set
    End Property

    Private _selectedMedicineItemID As Integer
    Public Property MedicineItemID As Integer
        Get
            Return _selectedMedicineItemID
        End Get
        Set(value As Integer)
            If Not Equals(_selectedMedicineItemID, value) Then
                _selectedMedicineItemID = value
                OnPropertyChanged(NameOf(MedicineItemID))
            End If
        End Set
    End Property

    Public ReadOnly Property MedicineShapeBindingSource As BindingSource
        Get
            Return _bindingSource
        End Get
    End Property

    Public Property ParentMedicineItemID As Integer
        Get
            Return _parentMedicineItemID
        End Get
        Set(value As Integer)
            If _parentMedicineItemID <> value Then
                _parentMedicineItemID = value
                _bindingSource.Filter = String.Empty
                ApplyNameFilter()
            End If
        End Set
    End Property

    Private _allRows As New List(Of MedicineShapeCls)()
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
        ComboToolbarLayoutHelper.ApplyPanelToolbar(PanelControl1, btnSerach, BtnAddMedicineShape, _btnSearchVisible, _btnAddVisible, Me)
    End Sub

    <Browsable(True), Category("Data"), Description("Optional substring on shape name (case-insensitive). Combined with flyout search (AND).")>
    Public Property Filter As String
        Get
            Return _nameFilter
        End Get
        Set(value As String)
            _nameFilter = If(value, String.Empty)
            ApplyNameFilter()
        End Set
    End Property

    Private Function ScopedRows() As List(Of MedicineShapeCls)
        If _parentMedicineItemID > -1 Then
            Return _allRows.Where(Function(r) r.MedicineItemID = _parentMedicineItemID).ToList()
        End If
        Return _allRows.ToList()
    End Function

    Private Function MatchesFilters(displayName As String, searchContains As String, externalContains As String) As Boolean
        If String.IsNullOrEmpty(displayName) Then Return False
        If Not String.IsNullOrEmpty(externalContains) AndAlso displayName.IndexOf(externalContains, StringComparison.OrdinalIgnoreCase) < 0 Then Return False
        If Not String.IsNullOrEmpty(searchContains) AndAlso displayName.IndexOf(searchContains, StringComparison.OrdinalIgnoreCase) < 0 Then Return False
        Return True
    End Function

    Private Function FilteredRows() As List(Of MedicineShapeCls)
        Dim searchTyped As String = If(txtSearch IsNot Nothing, txtSearch.Text, String.Empty).Trim()
        Dim external As String = _nameFilter.Trim()
        Return ScopedRows().Where(Function(r) MatchesFilters(If(r.MedicineShape, String.Empty), searchTyped, external)).ToList()
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
        If CboMedicineShape Is Nothing OrElse _applyingFilter Then Return
        _applyingFilter = True
        Dim filtered = FilteredRows()
        Dim preserveId As Integer = _selectedShapeID

        Try
            RemoveHandler CboMedicineShape.SelectedIndexChanged, AddressOf CboMedicineShape_SelectedIndexChanged

            CboMedicineShape.Properties.Items.Clear()
            For Each r As MedicineShapeCls In filtered
                CboMedicineShape.Properties.Items.Add(New ComboBoxItem With {
                    .ID = r.ShapeID,
                    .Name = If(r.MedicineShape, String.Empty),
                    .MedicineItemID = r.MedicineItemID
                })
            Next

            UpdateBindingSourceFromDisplayed()

            Dim keepItem As ComboBoxItem = Nothing
            If preserveId > 0 Then
                For Each item As ComboBoxItem In CboMedicineShape.Properties.Items
                    If item.ID = preserveId Then
                        keepItem = item
                        Exit For
                    End If
                Next
            End If

            Dim searchEmpty = String.IsNullOrWhiteSpace(If(txtSearch IsNot Nothing, txtSearch.Text, String.Empty).Trim())
            If keepItem IsNot Nothing Then
                CboMedicineShape.SelectedItem = keepItem
                _selectedShapeID = keepItem.ID
                _selectedMedicineItemID = keepItem.MedicineItemID
            ElseIf searchEmpty AndAlso String.IsNullOrWhiteSpace(_nameFilter.Trim()) AndAlso CboMedicineShape.Properties.Items.Count > 0 Then
                CboMedicineShape.SelectedIndex = 0
                Dim first As ComboBoxItem = TryCast(CboMedicineShape.SelectedItem, ComboBoxItem)
                If first IsNot Nothing Then
                    _selectedShapeID = first.ID
                    _selectedMedicineItemID = first.MedicineItemID
                End If
            Else
                CboMedicineShape.EditValue = Nothing
                _selectedShapeID = 0
                _selectedMedicineItemID = 0
            End If

            SetTextEditStyle(CboMedicineShape)
        Finally
            AddHandler CboMedicineShape.SelectedIndexChanged, AddressOf CboMedicineShape_SelectedIndexChanged
            _applyingFilter = False
        End Try
    End Sub

    Private Sub BindMedicineShapes()
        Const sql As String = "SELECT ShapeID, MedicineItemID, MedicineShape FROM MedicineShape;"
        Using conn As New SqlConnection(_connectionString)
            conn.Open()
            _allRows = conn.Query(Of MedicineShapeCls)(sql).ToList()
        End Using
        ApplyNameFilter()
    End Sub

    Private Sub UpdateShapeIDComboBoxSelection(shapeId As Integer)
        ClearSearchBox()
        If shapeId <= 0 Then
            CboMedicineShape.EditValue = Nothing
            _selectedMedicineItemID = 0
            ApplyNameFilter()
            Return
        End If
        Dim row = _allRows.FirstOrDefault(Function(r) r.ShapeID = shapeId)
        If row Is Nothing Then
            CboMedicineShape.EditValue = Nothing
            _selectedMedicineItemID = 0
            ApplyNameFilter()
            Return
        End If
        ApplyNameFilter()
        For Each item As ComboBoxItem In CboMedicineShape.Properties.Items
            If item.ID = shapeId Then
                CboMedicineShape.SelectedItem = item
                _selectedMedicineItemID = item.MedicineItemID
                Return
            End If
        Next
        CboMedicineShape.EditValue = Nothing
        _selectedMedicineItemID = 0
    End Sub

    Private Sub CboMedicineShape_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CboMedicineShape.SelectedIndexChanged
        If _applyingFilter Then Return
        Dim selectedItem As ComboBoxItem = TryCast(CboMedicineShape.SelectedItem, ComboBoxItem)
        If selectedItem IsNot Nothing Then
            ShapeID = selectedItem.ID
            MedicineItemID = selectedItem.MedicineItemID
            RaiseEvent MedicineShapeValueChanged(sender, New MedicineShapeIndexChangedEvent(ShapeID, MedicineItemID))
        End If
    End Sub

    Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged
        If _suppressSearchTextChanged OrElse _applyingFilter Then Return
        ApplyNameFilter()
    End Sub

    Private Sub Flyout1_Showing(sender As Object, e As FlyoutPanelEventArgs) Handles Flyout1.Showing
        ComboFlyoutSearchHelper.ApplyManualPositionBelowPanel(Flyout1, PanelControl2, CboMedicineShape)
    End Sub

    Private Sub btnSerach_Click(sender As Object, e As EventArgs) Handles btnSerach.Click
        ComboFlyoutSearchHelper.ShowFlyoutSearchDeferred(Flyout1, PanelControl2, CboMedicineShape, txtSearch, Me)
    End Sub

    Private Sub btnAddMedicineShape_Click(sender As Object, e As EventArgs) Handles BtnAddMedicineShape.Click
        FrmMedicineShape.ShowDialog()
        BindMedicineShapes()
    End Sub

    Public Function GetMedicineShapeTable() As DataTable
        Dim dt As New DataTable()
        dt.Columns.Add("ShapeID", GetType(Integer))
        dt.Columns.Add("MedicineItemID", GetType(Integer))
        dt.Columns.Add("MedicineShape", GetType(String))
        For Each r In FilteredRows()
            dt.Rows.Add(r.ShapeID, r.MedicineItemID, If(r.MedicineShape, CType(String.Empty, Object)))
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
        BindMedicineShapes()
    End Sub

    Private Sub HandleMedicineShapeValueChanged(ByVal sender As Object, ByVal e As MedicineShapeIndexChangedEvent) Handles Me.MedicineShapeValueChanged
    End Sub

    Public Function GetShapeID(medicineItemId As Integer) As Integer
        Const sql As String = "SELECT TOP 1 ShapeID FROM MedicineShape WHERE MedicineItemID = @MedicineItemID ORDER BY ShapeID"
        Using conn As New SqlConnection(_connectionString)
            Dim result As Integer? = conn.QuerySingleOrDefault(Of Integer?)(sql, New With {.MedicineItemID = medicineItemId})
            Return If(result.HasValue, result.Value, -1)
        End Using
    End Function

    Public Function GetMedicineItemID(shapeId As Integer) As Integer
        Const sql As String = "SELECT MedicineItemID FROM MedicineShape WHERE ShapeID = @ShapeID"
        Using conn As New SqlConnection(_connectionString)
            Dim result As Integer? = conn.QuerySingleOrDefault(Of Integer?)(sql, New With {.ShapeID = shapeId})
            Return If(result.HasValue, result.Value, -1)
        End Using
    End Function

    Public Sub SetSelectedMedicineItemID(shapeId As Integer)
        Me.ShapeID = shapeId
        UpdateShapeIDComboBoxSelection(shapeId)
    End Sub

    Public Sub SetSelectedShapeID(medicineItemId As Integer)
        Me.ShapeID = GetShapeID(medicineItemId)
        UpdateShapeIDComboBoxSelection(ShapeID)
    End Sub

    Public Class ComboBoxItem
        Public Property ID As Integer
        Public Property Name As String
        Public Property MedicineItemID As Integer
        Public Overrides Function ToString() As String
            Return Name
        End Function
    End Class

    Public Class MedicineShapeCls
        Public Property ShapeID As Integer
        Public Property MedicineItemID As Integer
        Public Property MedicineShape As String
    End Class

    Public Class MedicineShapeIndexChangedEvent
        Inherits EventArgs
        Public Property ShapeID As Integer
        Public Property MedicineItemID As Integer
        Public Sub New(shapeId As Integer, medicineItemId As Integer)
            Me.ShapeID = shapeId
            Me.MedicineItemID = medicineItemId
        End Sub
    End Class
End Class
