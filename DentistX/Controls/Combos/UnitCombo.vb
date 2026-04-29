Imports System.ComponentModel
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports System.Linq
Imports System.Windows.Forms
Imports Dapper
Imports DevExpress.Utils
Imports DevExpress.XtraEditors

Public Class UnitCombo
    Implements INotifyPropertyChanged

    Private ReadOnly _connectionString As String = DentistXDATA.GetConnection.ConnectionString

    Public Event UnitValueChanged(ByVal sender As Object, ByVal e As UnitIndexChangedEvent)

    Private _selectedUnitID As Integer
    Public Property UnitID As Integer
        Get
            Return _selectedUnitID
        End Get
        Set(value As Integer)
            If Not Equals(_selectedUnitID, value) Then
                _selectedUnitID = value
                OnPropertyChanged(NameOf(UnitID))
                UpdateUnitComboBoxSelection(value)
            End If
        End Set
    End Property

    Private _selectedUnitName As String
    Public Property UnitName As String
        Get
            Return _selectedUnitName
        End Get
        Set(value As String)
            If Not Equals(_selectedUnitName, value) Then
                _selectedUnitName = value
                OnPropertyChanged(NameOf(UnitName))
            End If
        End Set
    End Property

    Private _allRows As New List(Of Unit)()
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
        ComboToolbarLayoutHelper.ApplyPanelToolbar(PanelControl1, btnSerach, BtnAddUnit, _btnSearchVisible, _btnAddVisible, Me)
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
        If CboUnit Is Nothing OrElse _applyingFilter Then Return
        _applyingFilter = True
        Dim searchTyped As String = If(txtSearch IsNot Nothing, txtSearch.Text, String.Empty).Trim()
        Dim external As String = _nameFilter.Trim()
        Dim filtered = _allRows.Where(Function(p) MatchesFilters(p.UnitName, searchTyped, external)).ToList()
        Dim preserveId As Integer = _selectedUnitID

        Try
            RemoveHandler CboUnit.SelectedIndexChanged, AddressOf CboUnit_SelectedIndexChanged
            CboUnit.Properties.Items.Clear()
            For Each row As Unit In filtered
                CboUnit.Properties.Items.Add(New ComboBoxItem With {.ID = row.UnitID, .Name = row.UnitName})
            Next

            Dim keepItem As ComboBoxItem = Nothing
            If preserveId > 0 Then
                For Each item As ComboBoxItem In CboUnit.Properties.Items
                    If item.ID = preserveId Then
                        keepItem = item
                        Exit For
                    End If
                Next
            End If

            If keepItem IsNot Nothing Then
                CboUnit.SelectedItem = keepItem
                _selectedUnitID = keepItem.ID
                _selectedUnitName = keepItem.Name
            ElseIf String.IsNullOrWhiteSpace(searchTyped) AndAlso String.IsNullOrWhiteSpace(external) AndAlso CboUnit.Properties.Items.Count > 0 Then
                CboUnit.SelectedIndex = 0
                Dim first As ComboBoxItem = TryCast(CboUnit.SelectedItem, ComboBoxItem)
                If first IsNot Nothing Then
                    _selectedUnitID = first.ID
                    _selectedUnitName = first.Name
                End If
            Else
                CboUnit.EditValue = Nothing
                _selectedUnitID = 0
                _selectedUnitName = Nothing
            End If
            SetTextEditStyle(CboUnit)
        Finally
            AddHandler CboUnit.SelectedIndexChanged, AddressOf CboUnit_SelectedIndexChanged
            _applyingFilter = False
        End Try
    End Sub

    Private Sub BindUnits()
        _allRows = LoadFromDb()
        ApplyListFilter()
    End Sub

    Private Sub UpdateUnitComboBoxSelection(unitId As Integer)
        ClearSearchBox()
        If unitId <= 0 Then
            CboUnit.EditValue = Nothing
            _selectedUnitName = Nothing
            ApplyListFilter()
            Return
        End If
        Dim row = _allRows.FirstOrDefault(Function(p) p.UnitID = unitId)
        If row Is Nothing Then
            CboUnit.EditValue = Nothing
            _selectedUnitName = Nothing
            ApplyListFilter()
            Return
        End If
        ApplyListFilter()
        For Each item As ComboBoxItem In CboUnit.Properties.Items
            If item.ID = unitId Then
                CboUnit.SelectedItem = item
                _selectedUnitName = item.Name
                Return
            End If
        Next
        CboUnit.EditValue = Nothing
        _selectedUnitName = Nothing
    End Sub

    Private Sub CboUnit_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CboUnit.SelectedIndexChanged
        If _applyingFilter Then Return
        Dim selectedItem As ComboBoxItem = TryCast(CboUnit.SelectedItem, ComboBoxItem)
        If selectedItem IsNot Nothing Then
            UnitID = selectedItem.ID
            UnitName = selectedItem.Name
            RaiseEvent UnitValueChanged(sender, New UnitIndexChangedEvent(UnitID, UnitName))
        End If
    End Sub

    Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged
        If _suppressSearchTextChanged OrElse _applyingFilter Then Return
        ApplyListFilter()
    End Sub

    Private Sub Flyout1_Showing(sender As Object, e As FlyoutPanelEventArgs) Handles Flyout1.Showing
        ComboFlyoutSearchHelper.ApplyManualPositionBelowPanel(Flyout1, PanelControl2, CboUnit)
    End Sub

    Private Sub btnSerach_Click(sender As Object, e As EventArgs) Handles btnSerach.Click
        ComboFlyoutSearchHelper.ShowFlyoutSearchDeferred(Flyout1, PanelControl2, CboUnit, txtSearch, Me)
    End Sub

    Private Sub BtnAddUnit_Click(sender As Object, e As EventArgs) Handles BtnAddUnit.Click
        Frm_TblUnits.Icon = MainView3.Icon
        Frm_TblUnits.ShowDialog()
        BindUnits()
    End Sub

    Public Function GetCitiesTable() As DataTable
        Dim rows = LoadFromDb()
        Dim dt As New DataTable()
        dt.Columns.Add("UnitID", GetType(Integer))
        dt.Columns.Add("UnitName", GetType(String))
        For Each p In rows
            dt.Rows.Add(p.UnitID, If(p.UnitName, CType(String.Empty, Object)))
        Next
        Return dt
    End Function

    Private Function LoadFromDb() As List(Of Unit)
        Const sql As String = "SELECT UnitID, UnitName FROM TblUnits;"
        Using conn As New SqlConnection(_connectionString)
            conn.Open()
            Return conn.Query(Of Unit)(sql).ToList()
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
        BindUnits()
    End Sub

    Private Sub HandleUnitValueChanged(ByVal sender As Object, ByVal e As UnitIndexChangedEvent) Handles Me.UnitValueChanged
    End Sub

    Public Sub SetSelectedUnit(unitId As Integer)
        Me.UnitID = unitId
        UpdateUnitComboBoxSelection(unitId)
    End Sub

    Public Class ComboBoxItem
        Public Property ID As Integer
        Public Property Name As String
        Public Overrides Function ToString() As String
            Return Name
        End Function
    End Class

    Public Class Unit
        Public Property UnitID As Integer
        Public Property UnitName As String
    End Class

    Public Class UnitIndexChangedEvent
        Inherits EventArgs
        Public Property UnitID As Integer
        Public Property UnitName As String
        Public Sub New(unitId As Integer, unitName As String)
            Me.UnitID = unitId
            Me.UnitName = unitName
        End Sub
    End Class
End Class
