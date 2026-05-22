Imports System.ComponentModel
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports System.Linq
Imports System.Windows.Forms
Imports Dapper
Imports DevExpress.Utils
Imports DevExpress.XtraEditors

Public Class DoctorsCombo
    Implements INotifyPropertyChanged

    Private ReadOnly _connectionString As String = DentistXDATA.GetConnection.ConnectionString

    Public Event DoctorsValueChanged(ByVal sender As Object, ByVal e As DoctorsIndexChangedEvent)

    Private _selectedDrID As Integer
    Public Property DrID As Integer
        Get
            Return _selectedDrID
        End Get
        Set(value As Integer)
            If Not Equals(_selectedDrID, value) Then
                _selectedDrID = value
                OnPropertyChanged(NameOf(DrID))
                UpdateDrIDComboBoxSelection(value)
            End If
        End Set
    End Property

    Private _selectedDrName As String
    Public Property DrName As String
        Get
            Return _selectedDrName
        End Get
        Set(value As String)
            If Not Equals(_selectedDrName, value) Then
                _selectedDrName = value
                OnPropertyChanged(NameOf(DrName))
            End If
        End Set
    End Property

    Private _allRows As New List(Of DoctorsCls)()
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
        ComboToolbarLayoutHelper.ApplyPanelToolbar(PanelControl1, btnSerach, BtnAddDoctors, _btnSearchVisible, _btnAddVisible, Me)
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
        If CboDoctors Is Nothing OrElse _applyingFilter Then Return
        _applyingFilter = True
        Dim searchTyped As String = If(txtSearch IsNot Nothing, txtSearch.Text, String.Empty).Trim()
        Dim external As String = _nameFilter.Trim()
        Dim filtered = _allRows.Where(Function(p) MatchesFilters(p.DrName, searchTyped, external)).ToList()
        Dim preserveId As Integer = _selectedDrID

        Try
            RemoveHandler CboDoctors.SelectedIndexChanged, AddressOf CboDoctors_SelectedIndexChanged
            CboDoctors.Properties.Items.Clear()
            For Each row As DoctorsCls In filtered
                CboDoctors.Properties.Items.Add(New ComboBoxItem With {.ID = row.DrID, .Name = row.DrName})
            Next

            Dim keepItem As ComboBoxItem = Nothing
            If preserveId > 0 Then
                For Each item As ComboBoxItem In CboDoctors.Properties.Items
                    If item.ID = preserveId Then
                        keepItem = item
                        Exit For
                    End If
                Next
            End If

            If keepItem IsNot Nothing Then
                CboDoctors.SelectedItem = keepItem
                _selectedDrID = keepItem.ID
                _selectedDrName = keepItem.Name
            ElseIf String.IsNullOrWhiteSpace(searchTyped) AndAlso String.IsNullOrWhiteSpace(external) AndAlso CboDoctors.Properties.Items.Count > 0 Then
                CboDoctors.SelectedIndex = 0
                Dim first As ComboBoxItem = TryCast(CboDoctors.SelectedItem, ComboBoxItem)
                If first IsNot Nothing Then
                    _selectedDrID = first.ID
                    _selectedDrName = first.Name
                End If
            Else
                CboDoctors.EditValue = Nothing
                _selectedDrID = 0
                _selectedDrName = Nothing
            End If
            SetTextEditStyle(CboDoctors)
        Finally
            AddHandler CboDoctors.SelectedIndexChanged, AddressOf CboDoctors_SelectedIndexChanged
            _applyingFilter = False
        End Try
    End Sub

    Private Sub BindDoctorss()
        Const sql As String = "SELECT DrID, DrName FROM Doctors;"
        Using conn As New SqlConnection(_connectionString)
            conn.Open()
            _allRows = conn.Query(Of DoctorsCls)(sql).ToList()
        End Using
        ApplyListFilter()
    End Sub

    Private Sub UpdateDrIDComboBoxSelection(drId As Integer)
        ClearSearchBox()
        If drId <= 0 Then
            CboDoctors.EditValue = Nothing
            _selectedDrName = Nothing
            ApplyListFilter()
            Return
        End If
        Dim row = _allRows.FirstOrDefault(Function(p) p.DrID = drId)
        If row Is Nothing Then
            CboDoctors.EditValue = Nothing
            _selectedDrName = Nothing
            ApplyListFilter()
            Return
        End If
        ApplyListFilter()
        For Each item As ComboBoxItem In CboDoctors.Properties.Items
            If item.ID = drId Then
                CboDoctors.SelectedItem = item
                _selectedDrName = item.Name
                Return
            End If
        Next
        CboDoctors.EditValue = Nothing
        _selectedDrName = Nothing
    End Sub

    Private Sub CboDoctors_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CboDoctors.SelectedIndexChanged
        If _applyingFilter Then Return
        Dim selectedItem As ComboBoxItem = TryCast(CboDoctors.SelectedItem, ComboBoxItem)
        If selectedItem IsNot Nothing Then
            DrID = selectedItem.ID
            DrName = selectedItem.Name
            RaiseEvent DoctorsValueChanged(sender, New DoctorsIndexChangedEvent(DrID, DrName))
        End If
    End Sub

    Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged
        If _suppressSearchTextChanged OrElse _applyingFilter Then Return
        ApplyListFilter()
    End Sub

    Private Sub Flyout1_Showing(sender As Object, e As FlyoutPanelEventArgs) Handles Flyout1.Showing
        ComboFlyoutSearchHelper.ApplyManualPositionBelowPanel(Flyout1, PanelControl2, CboDoctors)
    End Sub

    Private Sub btnSerach_Click(sender As Object, e As EventArgs) Handles btnSerach.Click
        ComboFlyoutSearchHelper.ShowFlyoutSearchDeferred(Flyout1, PanelControl2, CboDoctors, txtSearch, Me)
    End Sub

    Private Sub BtnAddDoctors_Click(sender As Object, e As EventArgs) Handles BtnAddDoctors.Click
        FrmDoctors.ShowDialog()
        BindDoctorss()
    End Sub

    Public Function GetDoctorsTable() As DataTable
        Dim dt As New DataTable()
        dt.Columns.Add("DrID", GetType(Integer))
        dt.Columns.Add("DrName", GetType(String))
        For Each p In _allRows
            dt.Rows.Add(p.DrID, If(p.DrName, CType(String.Empty, Object)))
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
        BindDoctorss()
    End Sub

    Private Sub HandleDoctorsValueChanged(sender As Object, e As DoctorsIndexChangedEvent) Handles Me.DoctorsValueChanged
        Me.DrID = e.DrID
        Me.DrName = e.DrName
    End Sub

    Public Function GetDrID(drName As String) As Integer
        If String.IsNullOrWhiteSpace(drName) Then Return -1
        Const sql As String = "SELECT TOP 1 DrID FROM Doctors WHERE DrName = @DrName ORDER BY DrID"
        Using conn As New SqlConnection(_connectionString)
            Dim r As Integer? = conn.QueryFirstOrDefault(Of Integer?)(sql, New With {.DrName = drName})
            Return If(r.HasValue, r.Value, -1)
        End Using
    End Function

    Public Function GetDrName(drId As Integer) As String
        Const sql As String = "SELECT DrName FROM Doctors WHERE DrID = @DrID"
        Using conn As New SqlConnection(_connectionString)
            Return If(conn.QuerySingleOrDefault(Of String)(sql, New With {.DrID = drId}), String.Empty)
        End Using
    End Function

    Public Sub SetSelectedDrName(drId As Integer)
        Dim n As String = GetDrName(drId)
        If Not String.IsNullOrEmpty(n) Then
            Me.DrID = drId
            Me.DrName = n
            UpdateDrIDComboBoxSelection(drId)
        End If
    End Sub

    Public Sub SetSelectedDrID(drName As String)
        Dim id As Integer = GetDrID(drName)
        If id <> -1 Then
            Me.DrID = id
            Me.DrName = drName
            UpdateDrIDComboBoxSelection(id)
        End If
    End Sub

    Public Class ComboBoxItem
        Public Property ID As Integer
        Public Property Name As String
        Public Overrides Function ToString() As String
            Return Name
        End Function
    End Class

    Public Class DoctorsCls
        Public Property DrID As Integer
        Public Property DrName As String
    End Class

    Public Class DoctorsIndexChangedEvent
        Inherits EventArgs
        Public Property DrID As Integer
        Public Property DrName As String
        Public Sub New(drId As Integer, drName As String)
            Me.DrID = drId
            Me.DrName = drName
        End Sub
    End Class
End Class
