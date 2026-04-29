Imports System.ComponentModel
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports System.Linq
Imports System.Windows.Forms
Imports Dapper
Imports DevExpress.Utils
Imports DevExpress.XtraEditors

Public Class HlthCombo
    Implements INotifyPropertyChanged

    Private ReadOnly _connectionString As String = DentistXDATA.GetConnection.ConnectionString

    Public Event HealthValueChanged(ByVal sender As Object, ByVal e As HealthIndexChangedEvent)

    Private _selectedHID As Integer
    Public Property HID As Integer
        Get
            Return _selectedHID
        End Get
        Set(value As Integer)
            If Not Equals(_selectedHID, value) Then
                _selectedHID = value
                OnPropertyChanged(NameOf(HID))
                UpdateHealthComboBoxSelection(value)
            End If
        End Set
    End Property

    Private _selectedHealthStat As String
    Public Property HealthStat As String
        Get
            Return _selectedHealthStat
        End Get
        Set(value As String)
            If Not Equals(_selectedHealthStat, value) Then
                _selectedHealthStat = value
                OnPropertyChanged(NameOf(HealthStat))
            End If
        End Set
    End Property

    Private _allRows As New List(Of Health)()
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
        ComboToolbarLayoutHelper.ApplyPanelToolbar(PanelControl1, btnSerach, BtnAddHealth, _btnSearchVisible, _btnAddVisible, Me)
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
        If CboHealth Is Nothing OrElse _applyingFilter Then Return
        _applyingFilter = True
        Dim searchTyped As String = If(txtSearch IsNot Nothing, txtSearch.Text, String.Empty).Trim()
        Dim external As String = _nameFilter.Trim()
        Dim filtered = _allRows.Where(Function(p) MatchesFilters(p.HealthStat, searchTyped, external)).ToList()
        Dim preserveId As Integer = _selectedHID

        Try
            RemoveHandler CboHealth.SelectedIndexChanged, AddressOf CboHealth_SelectedIndexChanged
            CboHealth.Properties.Items.Clear()
            For Each row As Health In filtered
                CboHealth.Properties.Items.Add(New ComboBoxItem With {.ID = row.HID, .Name = row.HealthStat})
            Next

            Dim keepItem As ComboBoxItem = Nothing
            If preserveId > 0 Then
                For Each item As ComboBoxItem In CboHealth.Properties.Items
                    If item.ID = preserveId Then
                        keepItem = item
                        Exit For
                    End If
                Next
            End If

            If keepItem IsNot Nothing Then
                CboHealth.SelectedItem = keepItem
                _selectedHID = keepItem.ID
                _selectedHealthStat = keepItem.Name
            ElseIf String.IsNullOrWhiteSpace(searchTyped) AndAlso String.IsNullOrWhiteSpace(external) AndAlso CboHealth.Properties.Items.Count > 0 Then
                CboHealth.SelectedIndex = 0
                Dim first As ComboBoxItem = TryCast(CboHealth.SelectedItem, ComboBoxItem)
                If first IsNot Nothing Then
                    _selectedHID = first.ID
                    _selectedHealthStat = first.Name
                End If
            Else
                CboHealth.EditValue = Nothing
                _selectedHID = 0
                _selectedHealthStat = Nothing
            End If
            SetTextEditStyle(CboHealth)
        Finally
            AddHandler CboHealth.SelectedIndexChanged, AddressOf CboHealth_SelectedIndexChanged
            _applyingFilter = False
        End Try
    End Sub

    Private Sub BindHealth()
        _allRows = LoadFromDb()
        ApplyListFilter()
    End Sub

    Private Sub UpdateHealthComboBoxSelection(hid As Integer)
        ClearSearchBox()
        If hid <= 0 Then
            CboHealth.EditValue = Nothing
            _selectedHealthStat = Nothing
            ApplyListFilter()
            Return
        End If
        Dim row = _allRows.FirstOrDefault(Function(p) p.HID = hid)
        If row Is Nothing Then
            CboHealth.EditValue = Nothing
            _selectedHealthStat = Nothing
            ApplyListFilter()
            Return
        End If
        ApplyListFilter()
        For Each item As ComboBoxItem In CboHealth.Properties.Items
            If item.ID = hid Then
                CboHealth.SelectedItem = item
                _selectedHealthStat = item.Name
                Return
            End If
        Next
        CboHealth.EditValue = Nothing
        _selectedHealthStat = Nothing
    End Sub

    Private Sub CboHealth_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CboHealth.SelectedIndexChanged
        If _applyingFilter Then Return
        Dim selectedItem As ComboBoxItem = TryCast(CboHealth.SelectedItem, ComboBoxItem)
        If selectedItem IsNot Nothing Then
            HID = selectedItem.ID
            HealthStat = selectedItem.Name
            RaiseEvent HealthValueChanged(sender, New HealthIndexChangedEvent(HID, HealthStat))
        End If
    End Sub

    Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged
        If _suppressSearchTextChanged OrElse _applyingFilter Then Return
        ApplyListFilter()
    End Sub

    Private Sub Flyout1_Showing(sender As Object, e As FlyoutPanelEventArgs) Handles Flyout1.Showing
        ComboFlyoutSearchHelper.ApplyManualPositionBelowPanel(Flyout1, PanelControl2, CboHealth)
    End Sub

    Private Sub btnSerach_Click(sender As Object, e As EventArgs) Handles btnSerach.Click
        ComboFlyoutSearchHelper.ShowFlyoutSearchDeferred(Flyout1, PanelControl2, CboHealth, txtSearch, Me)
    End Sub

    Private Sub BtnAddHealth_Click(sender As Object, e As EventArgs) Handles BtnAddHealth.Click
        Frm_Health.Icon = MainView3.Icon
        Frm_Health.ShowDialog()
        BindHealth()
    End Sub

    Public Function GetCitiesTable() As DataTable
        Dim rows = LoadFromDb()
        Dim dt As New DataTable()
        dt.Columns.Add("HID", GetType(Integer))
        dt.Columns.Add("HealthStat", GetType(String))
        For Each p In rows
            dt.Rows.Add(p.HID, If(p.HealthStat, CType(String.Empty, Object)))
        Next
        Return dt
    End Function

    Private Function LoadFromDb() As List(Of Health)
        Const sql As String = "SELECT HID, HealthStat FROM Health;"
        Using conn As New SqlConnection(_connectionString)
            conn.Open()
            Return conn.Query(Of Health)(sql).ToList()
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
        BindHealth()
    End Sub

    Private Sub HandleHealthValueChanged(ByVal sender As Object, ByVal e As HealthIndexChangedEvent) Handles Me.HealthValueChanged
    End Sub

    Public Class ComboBoxItem
        Public Property ID As Integer
        Public Property Name As String
        Public Overrides Function ToString() As String
            Return Name
        End Function
    End Class

    Public Class Health
        Public Property HID As Integer
        Public Property HealthStat As String
    End Class

    Public Class HealthIndexChangedEvent
        Inherits EventArgs
        Public Property HID As Integer
        Public Property HealthStat As String
        Public Sub New(hid As Integer, healthStat As String)
            Me.HID = hid
            Me.HealthStat = healthStat
        End Sub
    End Class
End Class
