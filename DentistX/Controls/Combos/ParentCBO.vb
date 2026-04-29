Imports System.ComponentModel
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports System.Linq
Imports Dapper
Imports DevExpress.Utils
Imports DevExpress.XtraEditors

Public Class ParentCBO
    Implements INotifyPropertyChanged

    Private ReadOnly _connectionString As String = DentistXDATA.GetConnection.ConnectionString

    Private _selectedParentID As Integer
    Public Property ParentID As Integer
        Get
            Return _selectedParentID
        End Get
        Set(value As Integer)
            If Not Equals(_selectedParentID, value) Then
                _selectedParentID = value
                OnPropertyChanged(NameOf(ParentID))
                UpdateParentComboSelection(value)
            End If
        End Set
    End Property

    Private _selectedParentName As String
    Public Property ParentName As String
        Get
            Return _selectedParentName
        End Get
        Set(value As String)
            If Not Equals(_selectedParentName, value) Then
                _selectedParentName = value
                OnPropertyChanged(NameOf(ParentName))
            End If
        End Set
    End Property

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

    Private _allRows As New List(Of WireMeasure)()
    Private _nameFilter As String = String.Empty
    Private _applyingFilter As Boolean
    Private _suppressSearchTextChanged As Boolean

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

    Public Event WireMeasureValueChanged(ByVal sender As Object, ByVal e As WireMeasureIndexChangedEvent)

    Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged
    Protected Overridable Sub OnPropertyChanged(ByVal propertyName As String)
        RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(propertyName))
    End Sub

    Private Sub ClearSearchBox()
        If txtSearch Is Nothing Then Return
        _suppressSearchTextChanged = True
        Try
            txtSearch.Text = String.Empty
        Finally
            _suppressSearchTextChanged = False
        End Try
    End Sub

    Private Function MatchesFilters(displayName As String, searchContains As String, externalContains As String) As Boolean
        If String.IsNullOrEmpty(displayName) Then Return False
        If Not String.IsNullOrEmpty(externalContains) AndAlso displayName.IndexOf(externalContains, StringComparison.OrdinalIgnoreCase) < 0 Then Return False
        If Not String.IsNullOrEmpty(searchContains) AndAlso displayName.IndexOf(searchContains, StringComparison.OrdinalIgnoreCase) < 0 Then Return False
        Return True
    End Function

    Private Sub ApplyListFilter()
        If CboParent Is Nothing OrElse _applyingFilter Then Return
        _applyingFilter = True
        Dim searchTyped As String = If(txtSearch IsNot Nothing, txtSearch.Text, String.Empty).Trim()
        Dim external As String = _nameFilter.Trim()
        Dim filtered = _allRows.Where(Function(p) MatchesFilters(p.ParentName, searchTyped, external)).ToList()
        Dim preserveId As Integer = _selectedParentID

        Try
            RemoveHandler CboParent.SelectedIndexChanged, AddressOf CboParent_SelectedIndexChanged
            CboParent.Properties.Items.Clear()
            For Each row As WireMeasure In filtered
                CboParent.Properties.Items.Add(New ComboBoxItem With {.ID = row.ParentID, .Name = row.ParentName})
            Next

            Dim keepItem As ComboBoxItem = Nothing
            If preserveId > 0 Then
                For Each item As ComboBoxItem In CboParent.Properties.Items
                    If item.ID = preserveId Then
                        keepItem = item
                        Exit For
                    End If
                Next
            End If

            If keepItem IsNot Nothing Then
                CboParent.SelectedItem = keepItem
                _selectedParentID = keepItem.ID
                _selectedParentName = keepItem.Name
            ElseIf String.IsNullOrWhiteSpace(searchTyped) AndAlso String.IsNullOrWhiteSpace(external) AndAlso CboParent.Properties.Items.Count > 0 Then
                CboParent.SelectedIndex = 0
                Dim first As ComboBoxItem = TryCast(CboParent.SelectedItem, ComboBoxItem)
                If first IsNot Nothing Then
                    _selectedParentID = first.ID
                    _selectedParentName = first.Name
                End If
            Else
                CboParent.EditValue = Nothing
                _selectedParentID = 0
                _selectedParentName = Nothing
            End If
            SetTextEditStyle(CboParent)
        Finally
            AddHandler CboParent.SelectedIndexChanged, AddressOf CboParent_SelectedIndexChanged
            _applyingFilter = False
        End Try
    End Sub

    Private Sub BindParents()
        _allRows = LoadFromDb()
        ApplyListFilter()
    End Sub

    Private Sub UpdateParentComboSelection(parentId As Integer)
        ClearSearchBox()
        If parentId <= 0 Then
            CboParent.EditValue = Nothing
            _selectedParentName = Nothing
            ApplyListFilter()
            Return
        End If
        Dim row = _allRows.FirstOrDefault(Function(p) p.ParentID = parentId)
        If row Is Nothing Then
            CboParent.EditValue = Nothing
            _selectedParentName = Nothing
            ApplyListFilter()
            Return
        End If
        ApplyListFilter()
        For Each item As ComboBoxItem In CboParent.Properties.Items
            If item.ID = parentId Then
                CboParent.SelectedItem = item
                _selectedParentName = item.Name
                Return
            End If
        Next
        CboParent.EditValue = Nothing
        _selectedParentName = Nothing
    End Sub

    Private Sub CboParent_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CboParent.SelectedIndexChanged
        If _applyingFilter Then Return
        Dim selectedItem As ComboBoxItem = TryCast(CboParent.SelectedItem, ComboBoxItem)
        If selectedItem IsNot Nothing Then
            ParentID = selectedItem.ID
            ParentName = selectedItem.Name
            RaiseEvent WireMeasureValueChanged(sender, New WireMeasureIndexChangedEvent(ParentID, ParentName))
        End If
    End Sub

    Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged
        If _suppressSearchTextChanged OrElse _applyingFilter Then Return
        ApplyListFilter()
    End Sub

    Private Sub Flyout1_Showing(sender As Object, e As FlyoutPanelEventArgs) Handles Flyout1.Showing
        ComboFlyoutSearchHelper.ApplyManualPositionBelowPanel(Flyout1, PanelControl2, CboParent)
    End Sub

    Private Sub btnSerach_Click(sender As Object, e As EventArgs) Handles btnSerach.Click
        ComboFlyoutSearchHelper.ShowFlyoutSearchDeferred(Flyout1, PanelControl2, CboParent, txtSearch, Me)
    End Sub

    Private Sub BtnAddMeasure_Click(sender As Object, e As EventArgs) Handles BtnAddMeasure.Click
        Dim F As New FrmTblMeasure
        F.ShowDialog(Me)
        BindParents()
    End Sub

    Private Sub HandleWireMeasureValueChanged(ByVal sender As Object, ByVal e As WireMeasureIndexChangedEvent) Handles Me.WireMeasureValueChanged
    End Sub

    Public Function GetWireMeasuresTable() As DataTable
        Dim rows = LoadFromDb()
        Dim dt As New DataTable()
        dt.Columns.Add("MeasureID", GetType(Integer))
        dt.Columns.Add("Measure", GetType(String))
        For Each p In rows
            dt.Rows.Add(p.ParentID, If(p.ParentName, CType(String.Empty, Object)))
        Next
        Return dt
    End Function

    Private Function LoadFromDb() As List(Of WireMeasure)
        Const sql As String = "SELECT MeasureID AS ParentID, Measure AS ParentName FROM TblMeasure;"
        Using conn As New SqlConnection(_connectionString)
            conn.Open()
            Return conn.Query(Of WireMeasure)(sql).ToList()
        End Using
    End Function

    Private Sub SetTextEditStyle(ByVal comboBox As ComboBoxEdit)
        comboBox.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard
    End Sub

    Public Sub New()
        InitializeComponent()
        ApplyToolbarLayout()
        BindParents()
    End Sub

    Public Sub SetSelectedWireMeasure(MeasureID As Integer)
        Me.ParentID = MeasureID
    End Sub

    Public Class ComboBoxItem
        Public Property ID As Integer
        Public Property Name As String
        Public Overrides Function ToString() As String
            Return Name
        End Function
    End Class

    Public Class WireMeasure
        Public Property ParentID As Integer
        Public Property ParentName As String
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
