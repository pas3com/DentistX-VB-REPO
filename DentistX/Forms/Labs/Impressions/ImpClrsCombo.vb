Imports System.ComponentModel
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports System.Linq
Imports System.Windows.Forms
Imports Dapper
Imports DevExpress.Utils
Imports DevExpress.XtraEditors

Public Class ImpClrsCombo
    Implements INotifyPropertyChanged

    Private ReadOnly _connectionString As String = DentistXDATA.GetConnection.connectionString
    Private _dataLoaded As Boolean

    Private Shared Function NormalizeImpClrText(value As Object) As String
        If value Is Nothing OrElse IsDBNull(value) Then Return String.Empty
        Return Convert.ToString(value)
    End Function

    Public Event ImpClrsValueChanged(ByVal sender As Object, ByVal e As ImpClrsIndexChangedEvent)

    Private _selectedImpClrID As Integer
    Public Property ImpClrID As Integer
        Get
            Return _selectedImpClrID
        End Get
        Set(value As Integer)
            If Not Equals(_selectedImpClrID, value) Then
                _selectedImpClrID = value
                OnPropertyChanged(NameOf(ImpClrID))
                UpdateImpClrIDComboBoxSelection(value)
            End If
        End Set
    End Property

    Private _selectedImpClr As String
    Public Property ImpClr As String
        Get
            Return _selectedImpClr
        End Get
        Set(value As String)
            Dim normalized = If(value, String.Empty)
            If Not Equals(_selectedImpClr, normalized) Then
                _selectedImpClr = normalized
                OnPropertyChanged(NameOf(ImpClr))
            End If
        End Set
    End Property

    Private _allRows As New List(Of ImpClrsCls)()
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
        ComboToolbarLayoutHelper.ApplyPanelToolbar(PanelControl1, btnSerach, BtnAddImpClrs, _btnSearchVisible, _btnAddVisible, Me)
    End Sub

    <Browsable(True), Category("Data"), Description("Optional substring to restrict by color text (case-insensitive). Combined with the flyout search using AND logic.")>
    Public Property Filter As String
        Get
            Return _nameFilter
        End Get
        Set(value As String)
            _nameFilter = If(value, String.Empty)
            ApplyNameFilter()
        End Set
    End Property

    Private Shared Function MatchesNameFilters(displayName As String, searchContains As String, externalContains As String) As Boolean
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

    Private Function DisplayName(r As ImpClrsCls) As String
        Return If(r.ImpClr, String.Empty)
    End Function

    Private Sub ApplyNameFilter()
        If CboImpClrs Is Nothing OrElse _applyingFilter Then Return
        _applyingFilter = True
        Dim searchTyped As String = If(txtSearch IsNot Nothing, txtSearch.Text, String.Empty).Trim()
        Dim external As String = _nameFilter.Trim()
        Dim filtered = _allRows.Where(Function(r) MatchesNameFilters(DisplayName(r), searchTyped, external)).ToList()
        Dim preserveId As Integer = _selectedImpClrID

        Try
            RemoveHandler CboImpClrs.SelectedIndexChanged, AddressOf CboImpClrs_SelectedIndexChanged

            CboImpClrs.Properties.Items.Clear()
            For Each r As ImpClrsCls In filtered
                CboImpClrs.Properties.Items.Add(New ComboBoxItem With {.ID = r.ImpClrID, .Name = DisplayName(r)})
            Next

            Dim keepItem As ComboBoxItem = Nothing
            If preserveId > 0 Then
                For Each item As ComboBoxItem In CboImpClrs.Properties.Items
                    If item.ID = preserveId Then
                        keepItem = item
                        Exit For
                    End If
                Next
            End If

            If keepItem IsNot Nothing Then
                CboImpClrs.SelectedItem = keepItem
                _selectedImpClrID = keepItem.ID
                _selectedImpClr = If(keepItem.Name, String.Empty)
            ElseIf String.IsNullOrWhiteSpace(searchTyped) AndAlso String.IsNullOrWhiteSpace(external) AndAlso CboImpClrs.Properties.Items.Count > 0 Then
                CboImpClrs.SelectedIndex = 0
                Dim first As ComboBoxItem = TryCast(CboImpClrs.SelectedItem, ComboBoxItem)
                If first IsNot Nothing Then
                    _selectedImpClrID = first.ID
                    _selectedImpClr = If(first.Name, String.Empty)
                End If
            Else
                CboImpClrs.EditValue = Nothing
                _selectedImpClrID = 0
                _selectedImpClr = String.Empty
            End If

            SetTextEditStyle(CboImpClrs)
        Finally
            AddHandler CboImpClrs.SelectedIndexChanged, AddressOf CboImpClrs_SelectedIndexChanged
            _applyingFilter = False
        End Try
    End Sub

    Private Sub BindImpClrss()
        Const sql As String = "SELECT ImpClrID, ImpClr FROM ImpClrs;"
        Using conn As New SqlConnection(_connectionString)
            conn.Open()
            Dim raw = conn.Query(Of ImpClrsCls)(sql).ToList()
            _allRows = raw.Select(Function(r) New ImpClrsCls With {.ImpClrID = r.ImpClrID, .ImpClr = NormalizeImpClrText(r.ImpClr)}).ToList()
        End Using
        _dataLoaded = True
        ApplyNameFilter()
    End Sub

    Public Sub EnsureDataLoaded()
        If _dataLoaded Then Return
        BindImpClrss()
    End Sub

    Private Sub UpdateImpClrIDComboBoxSelection(ImpClrID As Integer)
        ClearSearchBox()
        If ImpClrID <= 0 Then
            CboImpClrs.EditValue = Nothing
            _selectedImpClr = String.Empty
            ApplyNameFilter()
            Return
        End If
        If _allRows.FirstOrDefault(Function(r) r.ImpClrID = ImpClrID) Is Nothing Then
            CboImpClrs.EditValue = Nothing
            _selectedImpClr = String.Empty
            ApplyNameFilter()
            Return
        End If
        ApplyNameFilter()
        For Each item As ComboBoxItem In CboImpClrs.Properties.Items
            If item.ID = ImpClrID Then
                CboImpClrs.SelectedItem = item
                _selectedImpClr = If(item.Name, String.Empty)
                Return
            End If
        Next
        CboImpClrs.EditValue = Nothing
        _selectedImpClr = String.Empty
    End Sub

    Private Sub CboImpClrs_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CboImpClrs.SelectedIndexChanged
        If _applyingFilter Then Return
        Dim selectedItem As ComboBoxItem = TryCast(CboImpClrs.SelectedItem, ComboBoxItem)
        If selectedItem IsNot Nothing Then
            ImpClrID = selectedItem.ID
            ImpClr = If(selectedItem.Name, String.Empty)
            RaiseEvent ImpClrsValueChanged(sender, New ImpClrsIndexChangedEvent(ImpClrID, ImpClr))
        End If
    End Sub

    Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged
        If _suppressSearchTextChanged OrElse _applyingFilter Then Return
        ApplyNameFilter()
    End Sub

    Private Sub Flyout1_Showing(sender As Object, e As FlyoutPanelEventArgs) Handles Flyout1.Showing
        ComboFlyoutSearchHelper.ApplyManualPositionBelowPanel(Flyout1, PanelControl2, CboImpClrs)
    End Sub

    Private Sub btnSerach_Click(sender As Object, e As EventArgs) Handles btnSerach.Click
        EnsureDataLoaded()
        ComboFlyoutSearchHelper.ShowFlyoutSearchDeferred(Flyout1, PanelControl2, CboImpClrs, txtSearch, Me)
    End Sub

    Private Sub btnAddImpClrs_Click(sender As Object, e As EventArgs) Handles BtnAddImpClrs.Click
        FrmImpClrs.ShowDialog()
        BindImpClrss()
    End Sub

    Public Function GetImpClrsTable() As DataTable
        EnsureDataLoaded()
        Dim dt As New DataTable()
        dt.Columns.Add("ImpClrID", GetType(Integer))
        dt.Columns.Add("ImpClr", GetType(String))
        For Each r In _allRows
            dt.Rows.Add(r.ImpClrID, If(r.ImpClr, CType(String.Empty, Object)))
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
    End Sub

    Private Sub CboImpClrs_Enter(sender As Object, e As EventArgs) Handles CboImpClrs.Enter
        EnsureDataLoaded()
    End Sub

    Private Sub HandleImpClrsValueChanged(ByVal sender As Object, ByVal e As ImpClrsIndexChangedEvent) Handles Me.ImpClrsValueChanged
    End Sub

    Public Function GetImpClrID(ImpClr As String) As Integer
        If String.IsNullOrWhiteSpace(ImpClr) Then Return -1
        Const sql As String = "SELECT ImpClrID FROM ImpClrs WHERE ImpClr = @ImpClr"
        Using conn As New SqlConnection(_connectionString)
            Dim result As Integer? = conn.QuerySingleOrDefault(Of Integer?)(sql, New With {.ImpClr = ImpClr})
            Return If(result.HasValue, result.Value, -1)
        End Using
    End Function

    Public Function GetImpClr(ImpClrID As Integer) As String
        Const sql As String = "SELECT ImpClr FROM ImpClrs WHERE ImpClrID = @ImpClrID"
        Using conn As New SqlConnection(_connectionString)
            Return NormalizeImpClrText(conn.QuerySingleOrDefault(Of String)(sql, New With {.ImpClrID = ImpClrID}))
        End Using
    End Function

    Public Sub SetSelectedImpClr(ImpClrID As Integer)
        If ImpClrID > 0 Then EnsureDataLoaded()
        Me.ImpClrID = ImpClrID
        UpdateImpClrIDComboBoxSelection(ImpClrID)
    End Sub

    Public Sub SetSelectedImpClrID(ImpClr As String)
        If String.IsNullOrWhiteSpace(ImpClr) Then
            SetSelectedImpClr(0)
            Return
        End If
        EnsureDataLoaded()
        Me.ImpClrID = GetImpClrID(ImpClr)
        UpdateImpClrIDComboBoxSelection(ImpClrID)
    End Sub

    Public Class ComboBoxItem
        Public Property ID As Integer
        Public Property Name As String
        Public Overrides Function ToString() As String
            Return If(Name, String.Empty)
        End Function
    End Class

    Public Class ImpClrsCls
        Public Property ImpClrID As Integer
        Public Property ImpClr As String
    End Class

    Public Class ImpClrsIndexChangedEvent
        Inherits EventArgs
        Public Property ImpClrID As Integer
        Public Property ImpClr As String
        Public Sub New(impClrId As Integer, impClr As String)
            Me.ImpClrID = impClrId
            Me.ImpClr = impClr
        End Sub
    End Class
End Class
