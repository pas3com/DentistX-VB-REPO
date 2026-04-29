Imports System.ComponentModel
Imports System.Data
Imports System.Data.SqlClient
Imports System.Linq
Imports System.Windows.Forms
Imports Dapper
Imports DevExpress.Utils
Imports DevExpress.XtraEditors

Public Class MedicineDozeCombo
    Implements INotifyPropertyChanged

    Private ReadOnly _connectionString As String = DentistXDATA.GetConnection.ConnectionString
    Private _bindingSource As New BindingSource()
    Private _parentShapeID As Integer = -1

    Public Event MedicineDozeValueChanged(ByVal sender As Object, ByVal e As MedicineDozeIndexChangedEvent)

    Private _selectedDozeID As Integer
    Public Property DozeID As Integer
        Get
            Return _selectedDozeID
        End Get
        Set(value As Integer)
            If Not Equals(_selectedDozeID, value) Then
                _selectedDozeID = value
                OnPropertyChanged(NameOf(DozeID))
                UpdateDozeIDComboBoxSelection(value)
            End If
        End Set
    End Property

    Private _selectedShapeID As Integer
    Public Property ShapeID As Integer
        Get
            Return _selectedShapeID
        End Get
        Set(value As Integer)
            If Not Equals(_selectedShapeID, value) Then
                _selectedShapeID = value
                OnPropertyChanged(NameOf(ShapeID))
            End If
        End Set
    End Property

    Public ReadOnly Property MedicineDozeBindingSource As BindingSource
        Get
            Return _bindingSource
        End Get
    End Property

    Public Property ParentShapeID As Integer
        Get
            Return _parentShapeID
        End Get
        Set(value As Integer)
            If _parentShapeID <> value Then
                _parentShapeID = value
                _bindingSource.Filter = String.Empty
                ApplyNameFilter()
            End If
        End Set
    End Property

    Private _allRows As New List(Of MedicineDozeCls)()
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
        ComboToolbarLayoutHelper.ApplyPanelToolbar(PanelControl1, btnSerach, BtnAddMedicineDoze, _btnSearchVisible, _btnAddVisible, Me)
    End Sub

    <Browsable(True), Category("Data"), Description("Optional substring on dose text (case-insensitive). Combined with flyout search (AND).")>
    Public Property Filter As String
        Get
            Return _nameFilter
        End Get
        Set(value As String)
            _nameFilter = If(value, String.Empty)
            ApplyNameFilter()
        End Set
    End Property

    Private Function ScopedRows() As List(Of MedicineDozeCls)
        If _parentShapeID > -1 Then
            Return _allRows.Where(Function(r) r.ShapeID = _parentShapeID).ToList()
        End If
        Return _allRows.ToList()
    End Function

    Private Function MatchesFilters(displayName As String, searchContains As String, externalContains As String) As Boolean
        If String.IsNullOrEmpty(displayName) Then Return False
        If Not String.IsNullOrEmpty(externalContains) AndAlso displayName.IndexOf(externalContains, StringComparison.OrdinalIgnoreCase) < 0 Then Return False
        If Not String.IsNullOrEmpty(searchContains) AndAlso displayName.IndexOf(searchContains, StringComparison.OrdinalIgnoreCase) < 0 Then Return False
        Return True
    End Function

    Private Function FilteredRows() As List(Of MedicineDozeCls)
        Dim searchTyped As String = If(txtSearch IsNot Nothing, txtSearch.Text, String.Empty).Trim()
        Dim external As String = _nameFilter.Trim()
        Return ScopedRows().Where(Function(r) MatchesFilters(If(r.Doze, String.Empty), searchTyped, external)).ToList()
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
        If CboMedicineDoze Is Nothing OrElse _applyingFilter Then Return
        _applyingFilter = True
        Dim filtered = FilteredRows()
        Dim preserveId As Integer = _selectedDozeID

        Try
            RemoveHandler CboMedicineDoze.SelectedIndexChanged, AddressOf CboMedicineDoze_SelectedIndexChanged

            CboMedicineDoze.Properties.Items.Clear()
            For Each r As MedicineDozeCls In filtered
                CboMedicineDoze.Properties.Items.Add(New ComboBoxItem With {
                    .ID = r.DozeID,
                    .Name = If(r.Doze, String.Empty),
                    .ShapeID = r.ShapeID
                })
            Next

            UpdateBindingSourceFromDisplayed()

            Dim keepItem As ComboBoxItem = Nothing
            If preserveId > 0 Then
                For Each item As ComboBoxItem In CboMedicineDoze.Properties.Items
                    If item.ID = preserveId Then
                        keepItem = item
                        Exit For
                    End If
                Next
            End If

            Dim searchEmpty = String.IsNullOrWhiteSpace(If(txtSearch IsNot Nothing, txtSearch.Text, String.Empty).Trim())
            If keepItem IsNot Nothing Then
                CboMedicineDoze.SelectedItem = keepItem
                _selectedDozeID = keepItem.ID
                _selectedShapeID = keepItem.ShapeID
            ElseIf searchEmpty AndAlso String.IsNullOrWhiteSpace(_nameFilter.Trim()) AndAlso CboMedicineDoze.Properties.Items.Count > 0 Then
                CboMedicineDoze.SelectedIndex = 0
                Dim first As ComboBoxItem = TryCast(CboMedicineDoze.SelectedItem, ComboBoxItem)
                If first IsNot Nothing Then
                    _selectedDozeID = first.ID
                    _selectedShapeID = first.ShapeID
                End If
            Else
                CboMedicineDoze.EditValue = Nothing
                _selectedDozeID = 0
                _selectedShapeID = 0
            End If

            SetTextEditStyle(CboMedicineDoze)
        Finally
            AddHandler CboMedicineDoze.SelectedIndexChanged, AddressOf CboMedicineDoze_SelectedIndexChanged
            _applyingFilter = False
        End Try
    End Sub

    Private Sub BindMedicineDozes()
        Const sql As String = "SELECT DozeID, ShapeID, Doze FROM MedicineDoze;"
        Using conn As New SqlConnection(_connectionString)
            conn.Open()
            _allRows = conn.Query(Of MedicineDozeCls)(sql).ToList()
        End Using
        ApplyNameFilter()
    End Sub

    Private Sub UpdateDozeIDComboBoxSelection(dozeId As Integer)
        ClearSearchBox()
        If dozeId <= 0 Then
            CboMedicineDoze.EditValue = Nothing
            _selectedShapeID = 0
            ApplyNameFilter()
            Return
        End If
        Dim row = _allRows.FirstOrDefault(Function(r) r.DozeID = dozeId)
        If row Is Nothing Then
            CboMedicineDoze.EditValue = Nothing
            _selectedShapeID = 0
            ApplyNameFilter()
            Return
        End If
        ApplyNameFilter()
        For Each item As ComboBoxItem In CboMedicineDoze.Properties.Items
            If item.ID = dozeId Then
                CboMedicineDoze.SelectedItem = item
                _selectedShapeID = item.ShapeID
                Return
            End If
        Next
        CboMedicineDoze.EditValue = Nothing
        _selectedShapeID = 0
    End Sub

    Private Sub CboMedicineDoze_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CboMedicineDoze.SelectedIndexChanged
        If _applyingFilter Then Return
        Dim selectedItem As ComboBoxItem = TryCast(CboMedicineDoze.SelectedItem, ComboBoxItem)
        If selectedItem IsNot Nothing Then
            DozeID = selectedItem.ID
            ShapeID = selectedItem.ShapeID
            RaiseEvent MedicineDozeValueChanged(sender, New MedicineDozeIndexChangedEvent(DozeID, ShapeID))
        End If
    End Sub

    Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged
        If _suppressSearchTextChanged OrElse _applyingFilter Then Return
        ApplyNameFilter()
    End Sub

    Private Sub Flyout1_Showing(sender As Object, e As FlyoutPanelEventArgs) Handles Flyout1.Showing
        ComboFlyoutSearchHelper.ApplyManualPositionBelowPanel(Flyout1, PanelControl2, CboMedicineDoze)
    End Sub

    Private Sub btnSerach_Click(sender As Object, e As EventArgs) Handles btnSerach.Click
        ComboFlyoutSearchHelper.ShowFlyoutSearchDeferred(Flyout1, PanelControl2, CboMedicineDoze, txtSearch, Me)
    End Sub

    Private Sub btnAddMedicineDoze_Click(sender As Object, e As EventArgs) Handles BtnAddMedicineDoze.Click
        FrmMedicineDoze.ShowDialog()
        BindMedicineDozes()
    End Sub

    Public Function GetMedicineDozeTable() As DataTable
        Dim dt As New DataTable()
        dt.Columns.Add("DozeID", GetType(Integer))
        dt.Columns.Add("ShapeID", GetType(Integer))
        dt.Columns.Add("Doze", GetType(String))
        For Each r In FilteredRows()
            dt.Rows.Add(r.DozeID, r.ShapeID, If(r.Doze, CType(String.Empty, Object)))
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
        BindMedicineDozes()
    End Sub

    Private Sub HandleMedicineDozeValueChanged(ByVal sender As Object, ByVal e As MedicineDozeIndexChangedEvent) Handles Me.MedicineDozeValueChanged
    End Sub

    Public Function GetDozeID(dozeText As String) As Integer
        If String.IsNullOrWhiteSpace(dozeText) Then Return -1
        Const sql As String = "SELECT DozeID FROM MedicineDoze WHERE Doze = @Doze"
        Using conn As New SqlConnection(_connectionString)
            Dim result As Integer? = conn.QuerySingleOrDefault(Of Integer?)(sql, New With {.Doze = dozeText})
            Return If(result.HasValue, result.Value, -1)
        End Using
    End Function

    Public Function GetShapeIDForDoze(dozeId As Integer) As Integer
        Const sql As String = "SELECT ShapeID FROM MedicineDoze WHERE DozeID = @DozeID"
        Using conn As New SqlConnection(_connectionString)
            Dim result As Integer? = conn.QuerySingleOrDefault(Of Integer?)(sql, New With {.DozeID = dozeId})
            Return If(result.HasValue, result.Value, -1)
        End Using
    End Function

    Public Sub SetSelectedShapeID(dozeId As Integer)
        Me.DozeID = dozeId
        UpdateDozeIDComboBoxSelection(dozeId)
    End Sub

    Public Sub SetSelectedDozeID(dozeText As String)
        Me.DozeID = GetDozeID(dozeText)
        UpdateDozeIDComboBoxSelection(DozeID)
    End Sub

    Public Class ComboBoxItem
        Public Property ID As Integer
        Public Property Name As String
        Public Property ShapeID As Integer
        Public Overrides Function ToString() As String
            Return Name
        End Function
    End Class

    Public Class MedicineDozeCls
        Public Property DozeID As Integer
        Public Property ShapeID As Integer
        Public Property Doze As String
    End Class

    Public Class MedicineDozeIndexChangedEvent
        Inherits EventArgs
        Public Property DozeID As Integer
        Public Property ShapeID As Integer
        Public Sub New(dozeId As Integer, shapeId As Integer)
            Me.DozeID = dozeId
            Me.ShapeID = shapeId
        End Sub
    End Class
End Class
