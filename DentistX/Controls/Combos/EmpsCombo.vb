Imports System.ComponentModel
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports System.Linq
Imports System.Windows.Forms
Imports Dapper
Imports DevExpress.Utils
Imports DevExpress.XtraEditors

Public Class EmpsCombo
    Implements INotifyPropertyChanged

    Private ReadOnly _connectionString As String = DentistXDATA.GetConnection.ConnectionString

    Public Event EmpsValueChanged(ByVal sender As Object, ByVal e As EmpIndexChangedEvent)

    Private _selectedEmpID As Integer
    Public Property EmpID As Integer
        Get
            Return _selectedEmpID
        End Get
        Set(value As Integer)
            If Not Equals(_selectedEmpID, value) Then
                _selectedEmpID = value
                OnPropertyChanged(NameOf(EmpID))
                UpdateEmpIDComboBoxSelection(value)
            End If
        End Set
    End Property

    Private _selectedEmpName As String
    Public Property EmpName As String
        Get
            Return _selectedEmpName
        End Get
        Set(value As String)
            If Not Equals(_selectedEmpName, value) Then
                _selectedEmpName = value
                OnPropertyChanged(NameOf(EmpName))
            End If
        End Set
    End Property

    Private _allRows As New List(Of EmpsCls)()
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
        ComboToolbarLayoutHelper.ApplyPanelToolbar(PanelControl1, btnSerach, BtnAddEmps, _btnSearchVisible, _btnAddVisible, Me)
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
        If CboEmps Is Nothing OrElse _applyingFilter Then Return
        _applyingFilter = True
        Dim searchTyped As String = If(txtSearch IsNot Nothing, txtSearch.Text, String.Empty).Trim()
        Dim external As String = _nameFilter.Trim()
        Dim filtered = _allRows.Where(Function(p) MatchesFilters(p.EmpName, searchTyped, external)).ToList()
        Dim preserveId As Integer = _selectedEmpID

        Try
            RemoveHandler CboEmps.SelectedIndexChanged, AddressOf CboEmps_SelectedIndexChanged
            CboEmps.Properties.Items.Clear()
            For Each row As EmpsCls In filtered
                CboEmps.Properties.Items.Add(New ComboBoxItem With {.ID = row.EmpID, .Name = row.EmpName})
            Next

            Dim keepItem As ComboBoxItem = Nothing
            If preserveId > 0 Then
                For Each item As ComboBoxItem In CboEmps.Properties.Items
                    If item.ID = preserveId Then
                        keepItem = item
                        Exit For
                    End If
                Next
            End If

            If keepItem IsNot Nothing Then
                CboEmps.SelectedItem = keepItem
                _selectedEmpID = keepItem.ID
                _selectedEmpName = keepItem.Name
            ElseIf String.IsNullOrWhiteSpace(searchTyped) AndAlso String.IsNullOrWhiteSpace(external) AndAlso CboEmps.Properties.Items.Count > 0 Then
                CboEmps.SelectedIndex = 0
                Dim first As ComboBoxItem = TryCast(CboEmps.SelectedItem, ComboBoxItem)
                If first IsNot Nothing Then
                    _selectedEmpID = first.ID
                    _selectedEmpName = first.Name
                End If
            Else
                CboEmps.EditValue = Nothing
                _selectedEmpID = 0
                _selectedEmpName = Nothing
            End If
            SetTextEditStyle(CboEmps)
        Finally
            AddHandler CboEmps.SelectedIndexChanged, AddressOf CboEmps_SelectedIndexChanged
            _applyingFilter = False
        End Try
    End Sub

    Private Sub BindEmps()
        Const sql As String = "SELECT EmpID, EmpName FROM Emp;"
        Using conn As New SqlConnection(_connectionString)
            conn.Open()
            _allRows = conn.Query(Of EmpsCls)(sql).ToList()
        End Using
        ApplyListFilter()
    End Sub

    Private Sub UpdateEmpIDComboBoxSelection(empId As Integer)
        ClearSearchBox()
        If empId <= 0 Then
            CboEmps.EditValue = Nothing
            _selectedEmpName = Nothing
            ApplyListFilter()
            Return
        End If
        Dim row = _allRows.FirstOrDefault(Function(p) p.EmpID = empId)
        If row Is Nothing Then
            CboEmps.EditValue = Nothing
            _selectedEmpName = Nothing
            ApplyListFilter()
            Return
        End If
        ApplyListFilter()
        For Each item As ComboBoxItem In CboEmps.Properties.Items
            If item.ID = empId Then
                CboEmps.SelectedItem = item
                _selectedEmpName = item.Name
                Return
            End If
        Next
        CboEmps.EditValue = Nothing
        _selectedEmpName = Nothing
    End Sub

    Private Sub CboEmps_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CboEmps.SelectedIndexChanged
        If _applyingFilter Then Return
        Dim selectedItem As ComboBoxItem = TryCast(CboEmps.SelectedItem, ComboBoxItem)
        If selectedItem IsNot Nothing Then
            EmpID = selectedItem.ID
            EmpName = selectedItem.Name
            RaiseEvent EmpsValueChanged(sender, New EmpIndexChangedEvent(EmpID, EmpName))
        End If
    End Sub

    Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged
        If _suppressSearchTextChanged OrElse _applyingFilter Then Return
        ApplyListFilter()
    End Sub

    Private Sub Flyout1_Showing(sender As Object, e As FlyoutPanelEventArgs) Handles Flyout1.Showing
        ComboFlyoutSearchHelper.ApplyManualPositionBelowPanel(Flyout1, PanelControl2, CboEmps)
    End Sub

    Private Sub btnSerach_Click(sender As Object, e As EventArgs) Handles btnSerach.Click
        ComboFlyoutSearchHelper.ShowFlyoutSearchDeferred(Flyout1, PanelControl2, CboEmps, txtSearch, Me)
    End Sub

    Private Sub BtnAddEmps_Click(sender As Object, e As EventArgs) Handles BtnAddEmps.Click
        FrmEmp.ShowDialog()
        BindEmps()
    End Sub

    Public Function GetEmpsTable() As DataTable
        Dim dt As New DataTable()
        dt.Columns.Add("EmpID", GetType(Integer))
        dt.Columns.Add("EmpName", GetType(String))
        For Each p In _allRows
            dt.Rows.Add(p.EmpID, If(p.EmpName, CType(String.Empty, Object)))
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
        BindEmps()
    End Sub

    Private Sub HandleEmpsValueChanged(sender As Object, e As EmpIndexChangedEvent) Handles Me.EmpsValueChanged
        Me.EmpID = e.EmpID
        Me.EmpName = e.EmpName
    End Sub

    Public Function GetEmpID(empName As String) As Integer
        If String.IsNullOrWhiteSpace(empName) Then Return -1
        Const sql As String = "SELECT EmpID FROM Emp WHERE EmpName = @EmpName"
        Using conn As New SqlConnection(_connectionString)
            Dim r As Integer? = conn.QuerySingleOrDefault(Of Integer?)(sql, New With {.EmpName = empName})
            Return If(r.HasValue, r.Value, -1)
        End Using
    End Function

    Public Function GetEmpName(empId As Integer) As String
        Const sql As String = "SELECT EmpName FROM Emp WHERE EmpID = @EmpID"
        Using conn As New SqlConnection(_connectionString)
            Return If(conn.QuerySingleOrDefault(Of String)(sql, New With {.EmpID = empId}), String.Empty)
        End Using
    End Function

    Public Sub SetSelectedEmpName(empId As Integer)
        Dim n As String = GetEmpName(empId)
        If Not String.IsNullOrEmpty(n) Then
            Me.EmpID = empId
            Me.EmpName = n
            UpdateEmpIDComboBoxSelection(empId)
        End If
    End Sub

    Public Sub SetSelectedEmpID(empName As String)
        Dim id As Integer = GetEmpID(empName)
        If id <> -1 Then
            Me.EmpID = id
            Me.EmpName = empName
            UpdateEmpIDComboBoxSelection(id)
        End If
    End Sub

    Public Class ComboBoxItem
        Public Property ID As Integer
        Public Property Name As String
        Public Overrides Function ToString() As String
            Return Name
        End Function
    End Class

    Public Class EmpsCls
        Public Property EmpID As Integer
        Public Property EmpName As String
    End Class

    Public Class EmpIndexChangedEvent
        Inherits EventArgs
        Public Property EmpID As Integer
        Public Property EmpName As String
        Public Sub New(empId As Integer, empName As String)
            Me.EmpID = empId
            Me.EmpName = empName
        End Sub
    End Class
End Class
