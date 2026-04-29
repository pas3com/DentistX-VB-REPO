Imports System.ComponentModel
Imports System.Data
Imports System.Data.SqlClient
Imports System.Linq
Imports System.Windows.Forms
Imports Dapper
Imports DevExpress.Utils
Imports DevExpress.XtraEditors

Public Class TblOtherTRTCombo
    Implements INotifyPropertyChanged

    Private ReadOnly _connectionString As String = DentistXDATA.GetConnection.connectionString

    Public Event TblOtherTRTValueChanged(ByVal sender As Object, ByVal e As TblOtherTRTIndexChangedEvent)

    Private _selectedTblOtherTrtID As Integer
    Public Property TblOtherTrtID As Integer
        Get
            Return _selectedTblOtherTrtID
        End Get
        Set(value As Integer)
            If Not Equals(_selectedTblOtherTrtID, value) Then
                _selectedTblOtherTrtID = value
                OnPropertyChanged(NameOf(TblOtherTrtID))
                UpdateTblOtherTrtIDComboBoxSelection(value)
            End If
        End Set
    End Property

    Private _selectedTrt As String
    Public Property Trt As String
        Get
            Return _selectedTrt
        End Get
        Set(value As String)
            If Not Equals(_selectedTrt, value) Then
                _selectedTrt = value
                OnPropertyChanged(NameOf(Trt))
            End If
        End Set
    End Property

    Private _allRows As New List(Of TblOtherTRTCls)()
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
        ComboToolbarLayoutHelper.ApplyPanelToolbar(PanelControl1, btnSerach, BtnAddTblOtherTRT, _btnSearchVisible, _btnAddVisible, Me)
    End Sub

    <Browsable(True), Category("Data"), Description("Optional substring to restrict items by name (case-insensitive). Combined with flyout search (AND).")>
    Public Property Filter As String
        Get
            Return _nameFilter
        End Get
        Set(value As String)
            _nameFilter = If(value, String.Empty)
            ApplyNameFilter()
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

    Private Sub ApplyNameFilter()
        If CboTblOtherTRT Is Nothing OrElse _applyingFilter Then Return
        _applyingFilter = True
        Dim searchTyped As String = If(txtSearch IsNot Nothing, txtSearch.Text, String.Empty).Trim()
        Dim external As String = _nameFilter.Trim()
        Dim filtered = _allRows.Where(Function(r) MatchesFilters(r.Trt, searchTyped, external)).ToList()
        Dim preserveId As Integer = _selectedTblOtherTrtID

        Try
            RemoveHandler CboTblOtherTRT.SelectedIndexChanged, AddressOf CboTblOtherTRT_SelectedIndexChanged

            CboTblOtherTRT.Properties.Items.Clear()
            For Each r As TblOtherTRTCls In filtered
                CboTblOtherTRT.Properties.Items.Add(New ComboBoxItem With {.ID = r.TblOtherTrtID, .Name = r.Trt})
            Next

            Dim keepItem As ComboBoxItem = Nothing
            If preserveId > 0 Then
                For Each item As ComboBoxItem In CboTblOtherTRT.Properties.Items
                    If item.ID = preserveId Then
                        keepItem = item
                        Exit For
                    End If
                Next
            End If

            If keepItem IsNot Nothing Then
                CboTblOtherTRT.SelectedItem = keepItem
                _selectedTblOtherTrtID = keepItem.ID
                _selectedTrt = keepItem.Name
            ElseIf String.IsNullOrWhiteSpace(searchTyped) AndAlso String.IsNullOrWhiteSpace(external) AndAlso CboTblOtherTRT.Properties.Items.Count > 0 Then
                CboTblOtherTRT.SelectedIndex = 0
                Dim first As ComboBoxItem = TryCast(CboTblOtherTRT.SelectedItem, ComboBoxItem)
                If first IsNot Nothing Then
                    _selectedTblOtherTrtID = first.ID
                    _selectedTrt = first.Name
                End If
            Else
                CboTblOtherTRT.EditValue = Nothing
                _selectedTblOtherTrtID = 0
                _selectedTrt = Nothing
            End If

            SetTextEditStyle(CboTblOtherTRT)
        Finally
            AddHandler CboTblOtherTRT.SelectedIndexChanged, AddressOf CboTblOtherTRT_SelectedIndexChanged
            _applyingFilter = False
        End Try
    End Sub

    Private Sub BindTblOtherTRTs()
        _allRows = GetTblOtherTRTs()
        ApplyNameFilter()
    End Sub

    Private Sub UpdateTblOtherTrtIDComboBoxSelection(tblOtherTrtId As Integer)
        ClearSearchBox()

        If tblOtherTrtId <= 0 Then
            CboTblOtherTRT.EditValue = Nothing
            _selectedTrt = Nothing
            ApplyNameFilter()
            Return
        End If

        Dim row = _allRows.FirstOrDefault(Function(r) r.TblOtherTrtID = tblOtherTrtId)
        If row Is Nothing Then
            CboTblOtherTRT.EditValue = Nothing
            _selectedTrt = Nothing
            ApplyNameFilter()
            Return
        End If

        ApplyNameFilter()
        For Each item As ComboBoxItem In CboTblOtherTRT.Properties.Items
            If item.ID = tblOtherTrtId Then
                CboTblOtherTRT.SelectedItem = item
                _selectedTrt = item.Name
                Return
            End If
        Next
        CboTblOtherTRT.EditValue = Nothing
        _selectedTrt = Nothing
    End Sub

    Private Sub CboTblOtherTRT_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CboTblOtherTRT.SelectedIndexChanged
        If _applyingFilter Then Return
        Dim selectedItem As ComboBoxItem = TryCast(CboTblOtherTRT.SelectedItem, ComboBoxItem)
        If selectedItem IsNot Nothing Then
            TblOtherTrtID = selectedItem.ID
            Trt = selectedItem.Name
            RaiseEvent TblOtherTRTValueChanged(sender, New TblOtherTRTIndexChangedEvent(TblOtherTrtID, Trt))
        End If
    End Sub

    Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged
        If _suppressSearchTextChanged OrElse _applyingFilter Then Return
        ApplyNameFilter()
    End Sub

    Private Sub Flyout1_Showing(sender As Object, e As FlyoutPanelEventArgs) Handles Flyout1.Showing
        ComboFlyoutSearchHelper.ApplyManualPositionBelowPanel(Flyout1, PanelControl2, CboTblOtherTRT)
    End Sub

    Private Sub btnSerach_Click(sender As Object, e As EventArgs) Handles btnSerach.Click
        ComboFlyoutSearchHelper.ShowFlyoutSearchDeferred(Flyout1, PanelControl2, CboTblOtherTRT, txtSearch, Me)
    End Sub

    Private Sub btnAddTblOtherTRT_Click(sender As Object, e As EventArgs) Handles BtnAddTblOtherTRT.Click
        FrmTblOtherTRT.ShowDialog()
        BindTblOtherTRTs()
    End Sub

    Public Function GetTblOtherTRTTable() As DataTable
        Dim dt As New DataTable()
        dt.Columns.Add("TblOtherTrtID", GetType(Integer))
        dt.Columns.Add("Trt", GetType(String))
        For Each r In _allRows
            dt.Rows.Add(r.TblOtherTrtID, If(r.Trt, CType(String.Empty, Object)))
        Next
        Return dt
    End Function

    Private Function GetTblOtherTRTs() As List(Of TblOtherTRTCls)
        Const sql As String = "SELECT TblOtherTrtID, Trt FROM TblOtherTRT;"
        Using conn As New SqlConnection(_connectionString)
            conn.Open()
            Return conn.Query(Of TblOtherTRTCls)(sql).ToList()
        End Using
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
        BindTblOtherTRTs()
    End Sub

    Private Sub HandleTblOtherTRTValueChanged(ByVal sender As Object, ByVal e As TblOtherTRTIndexChangedEvent) Handles Me.TblOtherTRTValueChanged
    End Sub

    Public Function GetTblOtherTrtID(trt As String) As Integer
        If String.IsNullOrWhiteSpace(trt) Then Return -1
        Const sql As String = "SELECT TblOtherTrtID FROM TblOtherTRT WHERE Trt = @Trt"
        Using conn As New SqlConnection(_connectionString)
            Dim result As Integer? = conn.QuerySingleOrDefault(Of Integer?)(sql, New With {.Trt = trt})
            Return If(result.HasValue, result.Value, -1)
        End Using
    End Function

    Public Function GetTrt(tblOtherTrtId As Integer) As String
        Const sql As String = "SELECT Trt FROM TblOtherTRT WHERE TblOtherTrtID = @TblOtherTrtID"
        Using conn As New SqlConnection(_connectionString)
            Dim result As String = conn.QuerySingleOrDefault(Of String)(sql, New With {.TblOtherTrtID = tblOtherTrtId})
            Return If(result, String.Empty)
        End Using
    End Function

    Public Sub SetSelectedTrt(tblOtherTrtId As Integer)
        Me.TblOtherTrtID = tblOtherTrtId
        UpdateTblOtherTrtIDComboBoxSelection(tblOtherTrtId)
    End Sub

    Public Sub SetSelectedTblOtherTrtID(trt As String)
        Me.TblOtherTrtID = GetTblOtherTrtID(trt)
        UpdateTblOtherTrtIDComboBoxSelection(TblOtherTrtID)
    End Sub

    Public Class ComboBoxItem
        Public Property ID As Integer
        Public Property Name As String
        Public Overrides Function ToString() As String
            Return Name
        End Function
    End Class

    Public Class TblOtherTRTCls
        Public Property TblOtherTrtID As Integer
        Public Property Trt As String
    End Class

    Public Class TblOtherTRTIndexChangedEvent
        Inherits EventArgs
        Public Property TblOtherTrtID As Integer
        Public Property Trt As String
        Public Sub New(tblOtherTrtId As Integer, trt As String)
            Me.TblOtherTrtID = tblOtherTrtId
            Me.Trt = trt
        End Sub
    End Class
End Class
