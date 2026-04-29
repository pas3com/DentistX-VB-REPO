Imports System.ComponentModel
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports System.Linq
Imports System.Windows.Forms
Imports Dapper
Imports DevExpress.Utils
Imports DevExpress.XtraEditors

Public Class WireMCombo
    Implements INotifyPropertyChanged

    Private ReadOnly _connectionString As String = DentistXDATA.GetConnection.ConnectionString

    Public Event WireMeasureValueChanged(ByVal sender As Object, ByVal e As WireMeasureIndexChangedEvent)

    Private _selectedMeasureID As Integer
    Public Property MeasureID As Integer
        Get
            Return _selectedMeasureID
        End Get
        Set(value As Integer)
            If Not Equals(_selectedMeasureID, value) Then
                _selectedMeasureID = value
                OnPropertyChanged(NameOf(MeasureID))
                UpdateWireMeasureComboBoxSelection(value)
            End If
        End Set
    End Property

    Private _selectedMeasure As String
    Public Property Measure As String
        Get
            Return _selectedMeasure
        End Get
        Set(value As String)
            If Not Equals(_selectedMeasure, value) Then
                _selectedMeasure = value
                OnPropertyChanged(NameOf(Measure))
            End If
        End Set
    End Property

    Private _allRows As New List(Of WireMeasureRow)()
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
        ComboToolbarLayoutHelper.ApplyPanelToolbar(PanelControl1, btnSerach, BtnAddMeasure, _btnSearchVisible, _btnAddVisible, Me)
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
        If CboWireMeasure Is Nothing OrElse _applyingFilter Then Return
        _applyingFilter = True
        Dim searchTyped As String = If(txtSearch IsNot Nothing, txtSearch.Text, String.Empty).Trim()
        Dim external As String = _nameFilter.Trim()
        Dim filtered = _allRows.Where(Function(p) MatchesFilters(p.Measure, searchTyped, external)).ToList()
        Dim preserveId As Integer = _selectedMeasureID

        Try
            RemoveHandler CboWireMeasure.SelectedIndexChanged, AddressOf CboWireMeasure_SelectedIndexChanged
            CboWireMeasure.Properties.Items.Clear()
            For Each row As WireMeasureRow In filtered
                CboWireMeasure.Properties.Items.Add(New ComboBoxItem With {.ID = row.MeasureID, .Name = row.Measure})
            Next

            Dim keepItem As ComboBoxItem = Nothing
            If preserveId > 0 Then
                For Each item As ComboBoxItem In CboWireMeasure.Properties.Items
                    If item.ID = preserveId Then
                        keepItem = item
                        Exit For
                    End If
                Next
            End If

            If keepItem IsNot Nothing Then
                CboWireMeasure.SelectedItem = keepItem
                _selectedMeasureID = keepItem.ID
                _selectedMeasure = keepItem.Name
            ElseIf String.IsNullOrWhiteSpace(searchTyped) AndAlso String.IsNullOrWhiteSpace(external) AndAlso CboWireMeasure.Properties.Items.Count > 0 Then
                CboWireMeasure.SelectedIndex = 0
                Dim first As ComboBoxItem = TryCast(CboWireMeasure.SelectedItem, ComboBoxItem)
                If first IsNot Nothing Then
                    _selectedMeasureID = first.ID
                    _selectedMeasure = first.Name
                End If
            Else
                CboWireMeasure.EditValue = Nothing
                _selectedMeasureID = 0
                _selectedMeasure = Nothing
            End If
            SetTextEditStyle(CboWireMeasure)
        Finally
            AddHandler CboWireMeasure.SelectedIndexChanged, AddressOf CboWireMeasure_SelectedIndexChanged
            _applyingFilter = False
        End Try
    End Sub

    Private Sub BindMeasures()
        _allRows = LoadMeasuresFromDb()
        ApplyListFilter()
    End Sub

    Private Sub UpdateWireMeasureComboBoxSelection(measureId As Integer)
        ClearSearchBox()
        If measureId <= 0 Then
            CboWireMeasure.EditValue = Nothing
            _selectedMeasure = Nothing
            ApplyListFilter()
            Return
        End If
        Dim row = _allRows.FirstOrDefault(Function(p) p.MeasureID = measureId)
        If row Is Nothing Then
            CboWireMeasure.EditValue = Nothing
            _selectedMeasure = Nothing
            ApplyListFilter()
            Return
        End If
        ApplyListFilter()
        For Each item As ComboBoxItem In CboWireMeasure.Properties.Items
            If item.ID = measureId Then
                CboWireMeasure.SelectedItem = item
                _selectedMeasure = item.Name
                Return
            End If
        Next
        CboWireMeasure.EditValue = Nothing
        _selectedMeasure = Nothing
    End Sub

    Private Sub CboWireMeasure_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CboWireMeasure.SelectedIndexChanged
        If _applyingFilter Then Return
        Dim selectedItem As ComboBoxItem = TryCast(CboWireMeasure.SelectedItem, ComboBoxItem)
        If selectedItem IsNot Nothing Then
            MeasureID = selectedItem.ID
            Measure = selectedItem.Name
            RaiseEvent WireMeasureValueChanged(sender, New WireMeasureIndexChangedEvent(MeasureID, Measure))
        End If
    End Sub

    Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged
        If _suppressSearchTextChanged OrElse _applyingFilter Then Return
        ApplyListFilter()
    End Sub

    Private Sub Flyout1_Showing(sender As Object, e As FlyoutPanelEventArgs) Handles Flyout1.Showing
        ComboFlyoutSearchHelper.ApplyManualPositionBelowPanel(Flyout1, PanelControl2, CboWireMeasure)
    End Sub

    Private Sub btnSerach_Click(sender As Object, e As EventArgs) Handles btnSerach.Click
        ComboFlyoutSearchHelper.ShowFlyoutSearchDeferred(Flyout1, PanelControl2, CboWireMeasure, txtSearch, Me)
    End Sub

    Private Sub BtnAddMeasure_Click(sender As Object, e As EventArgs) Handles BtnAddMeasure.Click
        Dim f As New FrmTblMeasure()
        f.Icon = MainView3.Icon
        f.ShowDialog(Me)
        BindMeasures()
    End Sub

    Public Function GetWireMeasuresTable() As DataTable
        Dim rows = LoadMeasuresFromDb()
        Dim dt As New DataTable()
        dt.Columns.Add("MeasureID", GetType(Integer))
        dt.Columns.Add("Measure", GetType(String))
        For Each p In rows
            dt.Rows.Add(p.MeasureID, If(p.Measure, CType(String.Empty, Object)))
        Next
        Return dt
    End Function

    Private Function LoadMeasuresFromDb() As List(Of WireMeasureRow)
        Const sql As String = "SELECT MeasureID, Measure FROM TblMeasure;"
        Using conn As New SqlConnection(_connectionString)
            conn.Open()
            Return conn.Query(Of WireMeasureRow)(sql).ToList()
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
        BindMeasures()
    End Sub

    Private Sub HandleWireMeasureValueChanged(ByVal sender As Object, ByVal e As WireMeasureIndexChangedEvent) Handles Me.WireMeasureValueChanged
        Me.MeasureID = e.MeasureID
        Me.Measure = e.Measure
    End Sub

    Public Function GetMeasureID(measureName As String) As Integer
        If String.IsNullOrWhiteSpace(measureName) Then Return -1
        Const sql As String = "SELECT MeasureID FROM TblMeasure WHERE Measure = @Measure"
        Using conn As New SqlConnection(_connectionString)
            Dim r As Integer? = conn.QuerySingleOrDefault(Of Integer?)(sql, New With {.Measure = measureName})
            Return If(r.HasValue, r.Value, -1)
        End Using
    End Function

    Public Function GetMeasureName(measureId As Integer) As String
        Const sql As String = "SELECT Measure FROM TblMeasure WHERE MeasureID = @MeasureID"
        Using conn As New SqlConnection(_connectionString)
            Return If(conn.QuerySingleOrDefault(Of String)(sql, New With {.MeasureID = measureId}), String.Empty)
        End Using
    End Function

    Public Sub SetSelectedMeasureName(measureId As Integer)
        Dim n As String = GetMeasureName(measureId)
        If Not String.IsNullOrEmpty(n) Then
            Me.MeasureID = measureId
            Me.Measure = n
            UpdateWireMeasureComboBoxSelection(measureId)
        End If
    End Sub

    Public Sub SetSelectedMeasureID(measureName As String)
        Dim id As Integer = GetMeasureID(measureName)
        If id <> -1 Then
            Me.MeasureID = id
            Me.Measure = measureName
            UpdateWireMeasureComboBoxSelection(id)
        End If
    End Sub

    Public Sub SetSelectedWireMeasure(id As Integer)
        Me.MeasureID = id
    End Sub

    Public Class ComboBoxItem
        Public Property ID As Integer
        Public Property Name As String
        Public Overrides Function ToString() As String
            Return Name
        End Function
    End Class

    Private Class WireMeasureRow
        Public Property MeasureID As Integer
        Public Property Measure As String
    End Class

    Public Class WireMeasureIndexChangedEvent
        Inherits EventArgs
        Public Property MeasureID As Integer
        Public Property Measure As String
        Public Sub New(measureId As Integer, measure As String)
            Me.MeasureID = measureId
            Me.Measure = measure
        End Sub
    End Class
End Class
