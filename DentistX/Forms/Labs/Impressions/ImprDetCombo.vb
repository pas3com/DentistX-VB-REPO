Imports System.ComponentModel
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports System.Linq
Imports System.Windows.Forms
Imports Dapper
Imports DevExpress.Utils
Imports DevExpress.XtraEditors

Public Class ImprDetCombo
    Implements INotifyPropertyChanged

    Private ReadOnly _connectionString As String = DentistXDATA.GetConnection.connectionString

    Public Event ImprDetValueChanged(ByVal sender As Object, ByVal e As ImprDetIndexChangedEvent)

    Private _allImprDets As New List(Of ImprDetCls)()
    Private _scopedImprDets As New List(Of ImprDetCls)()
    Private _nameFilter As String = String.Empty
    Private _applyingFilter As Boolean
    Private _suppressSearchTextChanged As Boolean
    Private _btnAddVisible As Boolean = True
    Private _btnSearchVisible As Boolean = True

    Private _selectedImpDetID As Integer
    Public Property ImpDetID As Integer
        Get
            Return _selectedImpDetID
        End Get
        Set(value As Integer)
            If Not Equals(_selectedImpDetID, value) Then
                _selectedImpDetID = value
                OnPropertyChanged(NameOf(ImpDetID))
                UpdateImpDetIDComboBoxSelection(value)
            End If
        End Set
    End Property

    Private _selectedImprID As Integer
    Public Property ImprID As Integer
        Get
            Return _selectedImprID
        End Get
        Set(value As Integer)
            If Not Equals(_selectedImprID, value) Then
                _selectedImprID = value
                OnPropertyChanged(NameOf(ImprID))
                UpdateImpDetIDComboBoxSelection(ImpDetID)
            End If
        End Set
    End Property

    Private _selectedimprDetail As String
    Public Property ImprDetail As String
        Get
            Return _selectedimprDetail
        End Get
        Set(value As String)
            If Not Equals(_selectedimprDetail, value) Then
                _selectedimprDetail = value
                OnPropertyChanged(NameOf(ImprDetail))
            End If
        End Set
    End Property

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
        ComboToolbarLayoutHelper.ApplyPanelToolbar(PanelControl1, btnSerach, BtnAddImprDet, _btnSearchVisible, _btnAddVisible, Me)
    End Sub

    <Browsable(True), Category("Data"), Description("Optional substring to restrict by detail text (case-insensitive). Combined with the flyout search using AND logic.")>
    Public Property Filter As String
        Get
            Return _nameFilter
        End Get
        Set(value As String)
            _nameFilter = If(value, String.Empty)
            ApplyImprDetFilter()
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

    Private Sub ApplyImprDetFilter(Optional autoSelectFirstWhenUnfiltered As Boolean = False)
        If CboImprDet Is Nothing OrElse _applyingFilter Then Return
        _applyingFilter = True
        Dim searchTyped As String = If(txtSearch IsNot Nothing, txtSearch.Text, String.Empty).Trim()
        Dim external As String = _nameFilter.Trim()
        Dim filtered = _scopedImprDets.Where(Function(r) MatchesNameFilters(If(r.ImprDetail, String.Empty), searchTyped, external)).ToList()
        Dim preserveId As Integer = _selectedImpDetID

        Try
            RemoveHandler CboImprDet.SelectedIndexChanged, AddressOf CboImprDet_SelectedIndexChanged

            CboImprDet.Properties.Items.Clear()
            For Each r As ImprDetCls In filtered
                CboImprDet.Properties.Items.Add(New ComboBoxItem With {.ID = r.ImpDetID, .ParentID = r.ParentID, .Name = r.ImprDetail})
            Next

            Dim keepItem As ComboBoxItem = Nothing
            If preserveId > 0 Then
                For Each item As ComboBoxItem In CboImprDet.Properties.Items
                    If item.ID = preserveId Then
                        keepItem = item
                        Exit For
                    End If
                Next
            End If

            If keepItem IsNot Nothing Then
                CboImprDet.SelectedItem = keepItem
                _selectedImpDetID = keepItem.ID
                _selectedimprDetail = If(keepItem.Name, String.Empty)
                _selectedImprID = keepItem.ParentID
            ElseIf CboImprDet.Properties.Items.Count > 0 AndAlso (autoSelectFirstWhenUnfiltered OrElse (String.IsNullOrWhiteSpace(searchTyped) AndAlso String.IsNullOrWhiteSpace(external))) Then
                CboImprDet.SelectedIndex = 0
                Dim first As ComboBoxItem = TryCast(CboImprDet.SelectedItem, ComboBoxItem)
                If first IsNot Nothing Then
                    _selectedImpDetID = first.ID
                    _selectedimprDetail = If(first.Name, String.Empty)
                    _selectedImprID = first.ParentID
                    RaiseEvent ImprDetValueChanged(Me, New ImprDetIndexChangedEvent(ImpDetID, ImprID, ImprDetail))
                End If
            Else
                CboImprDet.EditValue = Nothing
                _selectedImpDetID = 0
                _selectedimprDetail = String.Empty
            End If

            SetTextEditStyle(CboImprDet)
        Finally
            AddHandler CboImprDet.SelectedIndexChanged, AddressOf CboImprDet_SelectedIndexChanged
            _applyingFilter = False
        End Try
    End Sub

    Private Sub ReloadAllFromDb()
        Const sql As String = "SELECT ImpDetID, ImprID AS ParentID, ImprDetail FROM ImprDet;"
        Using conn As New SqlConnection(_connectionString)
            conn.Open()
            _allImprDets = conn.Query(Of ImprDetCls)(sql).ToList()
        End Using
    End Sub

    Private Sub BindImprDets()
        ReloadAllFromDb()
        _scopedImprDets = New List(Of ImprDetCls)(_allImprDets)
        CboImprDet.ResetText()
        ApplyImprDetFilter()
    End Sub

    Public Sub UpdateImpComboByParentID(parentID As Integer)
        ClearSearchBox()
        Const sql As String = "SELECT ImpDetID, ImprID AS ParentID, ImprDetail FROM ImprDet WHERE ImprID = @ImprID;"
        Using conn As New SqlConnection(_connectionString)
            conn.Open()
            _scopedImprDets = conn.Query(Of ImprDetCls)(sql, New With {.ImprID = parentID}).ToList()
        End Using
        ApplyImprDetFilter(autoSelectFirstWhenUnfiltered:=True)
    End Sub

    Private Sub UpdateImpDetIDComboBoxSelection(ImpDetID As Integer)
        ClearSearchBox()
        If ImpDetID <= 0 Then
            CboImprDet.EditValue = Nothing
            _selectedimprDetail = String.Empty
            ApplyImprDetFilter()
            Return
        End If
        Dim row = _scopedImprDets.FirstOrDefault(Function(r) r.ImpDetID = ImpDetID)
        If row Is Nothing Then row = _allImprDets.FirstOrDefault(Function(r) r.ImpDetID = ImpDetID)
        If row Is Nothing Then
            CboImprDet.EditValue = Nothing
            _selectedimprDetail = String.Empty
            ApplyImprDetFilter()
            Return
        End If
        ApplyImprDetFilter()
        For Each item As ComboBoxItem In CboImprDet.Properties.Items
            If item.ID = ImpDetID Then
                CboImprDet.SelectedItem = item
                _selectedimprDetail = If(item.Name, String.Empty)
                _selectedImprID = item.ParentID
                Return
            End If
        Next
        CboImprDet.EditValue = Nothing
        _selectedimprDetail = String.Empty
    End Sub

    Private Sub CboImprDet_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CboImprDet.SelectedIndexChanged
        If _applyingFilter Then Return
        If CboImprDet.Properties.Items.Count = 0 Then Return
        Dim selectedItem As ComboBoxItem = TryCast(CboImprDet.SelectedItem, ComboBoxItem)
        If selectedItem IsNot Nothing Then
            ImpDetID = selectedItem.ID
            ImprDetail = If(selectedItem.Name, String.Empty)
            ImprID = selectedItem.ParentID
            RaiseEvent ImprDetValueChanged(sender, New ImprDetIndexChangedEvent(ImpDetID, ImprID, ImprDetail))
        End If
    End Sub

    Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged
        If _suppressSearchTextChanged OrElse _applyingFilter Then Return
        ApplyImprDetFilter()
    End Sub

    Private Sub Flyout1_Showing(sender As Object, e As FlyoutPanelEventArgs) Handles Flyout1.Showing
        ComboFlyoutSearchHelper.ApplyManualPositionBelowPanel(Flyout1, PanelControl2, CboImprDet)
    End Sub

    Private Sub btnSerach_Click(sender As Object, e As EventArgs) Handles btnSerach.Click
        ComboFlyoutSearchHelper.ShowFlyoutSearchDeferred(Flyout1, PanelControl2, CboImprDet, txtSearch, Me)
    End Sub

    Private Sub btnAddImprDet_Click(sender As Object, e As EventArgs) Handles BtnAddImprDet.Click
        FrmImprDet.ShowDialog()
        BindImprDets()
    End Sub

    Public Function GetImprDetTable() As DataTable
        Dim dt As New DataTable()
        dt.Columns.Add("ImpDetID", GetType(Integer))
        dt.Columns.Add("ImprID", GetType(Integer))
        dt.Columns.Add("ImprDetail", GetType(String))
        For Each r In _allImprDets
            dt.Rows.Add(r.ImpDetID, r.ParentID, If(r.ImprDetail, CType(String.Empty, Object)))
        Next
        Return dt
    End Function

    Public Function GetImprDetTblByParent(ByVal parentID As Integer) As DataTable
        Dim dt As New DataTable()
        dt.Columns.Add("ImpDetID", GetType(Integer))
        dt.Columns.Add("ImprID", GetType(Integer))
        dt.Columns.Add("ImprDetail", GetType(String))
        For Each r In _allImprDets.Where(Function(x) x.ParentID = parentID)
            dt.Rows.Add(r.ImpDetID, r.ParentID, If(r.ImprDetail, CType(String.Empty, Object)))
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
        BindImprDets()
    End Sub

    Private Sub HandleImprDetValueChanged(ByVal sender As Object, ByVal e As ImprDetIndexChangedEvent) Handles Me.ImprDetValueChanged
    End Sub

    Public Function GetImpDetID(ImprDetail As String) As Integer
        If String.IsNullOrWhiteSpace(ImprDetail) Then Return -1
        Const sql As String = "SELECT ImpDetID FROM ImprDet WHERE ImprDetail = @ImprDetail"
        Using conn As New SqlConnection(_connectionString)
            Dim result As Integer? = conn.QuerySingleOrDefault(Of Integer?)(sql, New With {.ImprDetail = ImprDetail})
            Return If(result.HasValue, result.Value, -1)
        End Using
    End Function

    Public Function GetImprDetail(ImpDetID As Integer) As String
        Const sql As String = "SELECT ImprDetail FROM ImprDet WHERE ImpDetID = @ImpDetID"
        Using conn As New SqlConnection(_connectionString)
            Return If(conn.QuerySingleOrDefault(Of String)(sql, New With {.ImpDetID = ImpDetID}), String.Empty)
        End Using
    End Function

    ''' <summary>Select by detail text (used when loading orders).</summary>
    Public Sub SetSelectedImprDetail(ImprDetail As String)
        Me.ImpDetID = GetImpDetID(ImprDetail)
        UpdateImpDetIDComboBoxSelection(ImpDetID)
    End Sub

    Public Sub SetSelectedImpDetID(ImpDetID As Integer)
        Me.ImpDetID = ImpDetID
        UpdateImpDetIDComboBoxSelection(ImpDetID)
    End Sub

    Public Class ComboBoxItem
        Public Property ID As Integer
        Public Property ParentID As Integer
        Public Property Name As String
        Public Overrides Function ToString() As String
            Return Name
        End Function
    End Class

    Public Class ImprDetCls
        Public Property ImpDetID As Integer
        Public Property ParentID As Integer
        Public Property ImprDetail As String
    End Class

    Public Class ImprDetIndexChangedEvent
        Inherits EventArgs
        Public Property ImpDetID As Integer
        Public Property ParentID As Integer
        Public Property ImprDetail As String
        Public Sub New(ImpDetID As Integer, ParentID As Integer, ImprDetail As String)
            Me.ImpDetID = ImpDetID
            Me.ParentID = ParentID
            Me.ImprDetail = ImprDetail
        End Sub
    End Class
End Class
