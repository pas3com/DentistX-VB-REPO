Imports System.ComponentModel
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports System.Linq
Imports System.Windows.Forms
Imports Dapper
Imports DevExpress.Utils
Imports DevExpress.XtraEditors

Public Class LabCombo
    Implements INotifyPropertyChanged

    Private ReadOnly _connectionString As String = DentistXDATA.GetConnection.connectionString

    Public Event LabValueChanged(ByVal sender As Object, ByVal e As LabIndexChangedEvent)

    Private _selectedLabID As Integer
    Public Property LabID As Integer
        Get
            Return _selectedLabID
        End Get
        Set(value As Integer)
            If Not Equals(_selectedLabID, value) Then
                _selectedLabID = value
                OnPropertyChanged(NameOf(LabID))
                UpdateLabIDComboBoxSelection(value)
            End If
        End Set
    End Property

    Private _selectedLabName As String
    Public Property LabName As String
        Get
            Return _selectedLabName
        End Get
        Set(value As String)
            If Not Equals(_selectedLabName, value) Then
                _selectedLabName = value
                OnPropertyChanged(NameOf(LabName))
            End If
        End Set
    End Property

    Private _allLabs As New List(Of LabCls)()
    Private _nameFilter As String = String.Empty
    Private _applyingFilter As Boolean
    Private _suppressSearchTextChanged As Boolean
    Private _btnAddVisible As Boolean = True
    Private _btnSearchVisible As Boolean = True
    Private _onlyWithWhatsApp As Boolean

    ''' <summary>When true, only labs with a usable WhatsApp number (same rules as SnapShotSender / WhatsHelper) appear in the list.</summary>
    <Browsable(True), Category("Behavior"), DefaultValue(False)>
    Public Property OnlyWithWhatsApp As Boolean
        Get
            Return _onlyWithWhatsApp
        End Get
        Set(value As Boolean)
            If _onlyWithWhatsApp = value Then Return
            _onlyWithWhatsApp = value
            BindLabs()
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
        ComboToolbarLayoutHelper.ApplyPanelToolbar(PanelControl1, btnSerach, BtnAddLab, _btnSearchVisible, _btnAddVisible, Me)
    End Sub

    <Browsable(True), Category("Data"), Description("Optional substring to restrict labs by name (case-insensitive). Combined with the flyout search text using AND logic.")>
    Public Property Filter As String
        Get
            Return _nameFilter
        End Get
        Set(value As String)
            _nameFilter = If(value, String.Empty)
            ApplyLabNameFilter()
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

    Private Sub ApplyLabNameFilter()
        If CboLab Is Nothing OrElse _applyingFilter Then Return
        _applyingFilter = True
        Dim searchTyped As String = If(txtSearch IsNot Nothing, txtSearch.Text, String.Empty).Trim()
        Dim external As String = _nameFilter.Trim()
        Dim filtered = _allLabs.Where(Function(l) MatchesNameFilters(If(l.LabName, String.Empty), searchTyped, external)).ToList()
        Dim preserveId As Integer = _selectedLabID

        Try
            RemoveHandler CboLab.SelectedIndexChanged, AddressOf CboLab_SelectedIndexChanged

            CboLab.Properties.Items.Clear()
            For Each Lab As LabCls In filtered
                CboLab.Properties.Items.Add(New ComboBoxItem With {.ID = Lab.LabID, .Name = Lab.LabName})
            Next

            Dim keepItem As ComboBoxItem = Nothing
            If preserveId > 0 Then
                For Each item As ComboBoxItem In CboLab.Properties.Items
                    If item.ID = preserveId Then
                        keepItem = item
                        Exit For
                    End If
                Next
            End If

            If keepItem IsNot Nothing Then
                CboLab.SelectedItem = keepItem
                _selectedLabID = keepItem.ID
                _selectedLabName = keepItem.Name
            ElseIf String.IsNullOrWhiteSpace(searchTyped) AndAlso String.IsNullOrWhiteSpace(external) AndAlso CboLab.Properties.Items.Count > 0 Then
                CboLab.SelectedIndex = 0
                Dim first As ComboBoxItem = TryCast(CboLab.SelectedItem, ComboBoxItem)
                If first IsNot Nothing Then
                    _selectedLabID = first.ID
                    _selectedLabName = first.Name
                End If
            Else
                CboLab.EditValue = Nothing
                _selectedLabID = 0
                _selectedLabName = Nothing
            End If

            SetTextEditStyle(CboLab)
        Finally
            AddHandler CboLab.SelectedIndexChanged, AddressOf CboLab_SelectedIndexChanged
            _applyingFilter = False
        End Try
    End Sub

    Private Sub BindLabs()
        _allLabs = GetLabs()
        ApplyLabNameFilter()
    End Sub

    Private Sub UpdateLabIDComboBoxSelection(LabID As Integer)
        ClearSearchBox()

        If LabID <= 0 Then
            CboLab.EditValue = Nothing
            _selectedLabName = Nothing
            ApplyLabNameFilter()
            Return
        End If
        Dim row = _allLabs.FirstOrDefault(Function(l) l.LabID = LabID)
        If row Is Nothing Then
            CboLab.EditValue = Nothing
            _selectedLabName = Nothing
            ApplyLabNameFilter()
            Return
        End If

        ApplyLabNameFilter()
        For Each item As ComboBoxItem In CboLab.Properties.Items
            If item.ID = LabID Then
                CboLab.SelectedItem = item
                _selectedLabName = item.Name
                Return
            End If
        Next
        CboLab.EditValue = Nothing
        _selectedLabName = Nothing
    End Sub

    Private Sub CboLab_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CboLab.SelectedIndexChanged
        If _applyingFilter Then Return
        Dim selectedItem As ComboBoxItem = TryCast(CboLab.SelectedItem, ComboBoxItem)
        If selectedItem IsNot Nothing Then
            LabID = selectedItem.ID
            LabName = selectedItem.Name
            RaiseEvent LabValueChanged(sender, New LabIndexChangedEvent(LabID, LabName))
        End If
    End Sub

    Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged
        If _suppressSearchTextChanged OrElse _applyingFilter Then Return
        ApplyLabNameFilter()
    End Sub

    Private Sub Flyout1_Showing(sender As Object, e As FlyoutPanelEventArgs) Handles Flyout1.Showing
        ComboFlyoutSearchHelper.ApplyManualPositionBelowPanel(Flyout1, PanelControl2, CboLab)
    End Sub

    Private Sub btnSerach_Click(sender As Object, e As EventArgs) Handles btnSerach.Click
        ComboFlyoutSearchHelper.ShowFlyoutSearchDeferred(Flyout1, PanelControl2, CboLab, txtSearch, Me)
    End Sub

    Private Sub btnAddLab_Click(sender As Object, e As EventArgs) Handles BtnAddLab.Click
        FrmLab.ShowDialog()
        BindLabs()
    End Sub

    Public Function GetLabTable() As DataTable
        Dim dt As New DataTable()
        dt.Columns.Add("LabID", GetType(Integer))
        dt.Columns.Add("LabName", GetType(String))
        For Each l In _allLabs
            dt.Rows.Add(l.LabID, If(l.LabName, CType(String.Empty, Object)))
        Next
        Return dt
    End Function

    Private Function GetLabs() As List(Of LabCls)
        Const sql As String = "SELECT LabID, LabName, WhatsAppPrefix, WhatsApp FROM Lab;"
        Using conn As New SqlConnection(_connectionString)
            conn.Open()
            Dim rows = conn.Query(Of LabCls)(sql).ToList()
            If _onlyWithWhatsApp Then
                rows = rows.Where(AddressOf HasUsableWhatsAppDigits).ToList()
            End If
            Return rows.OrderBy(Function(l) If(l.LabName, String.Empty)).ToList()
        End Using
    End Function

    Private Shared Function HasUsableWhatsAppDigits(l As LabCls) As Boolean
        If l Is Nothing Then Return False
        Dim digits = WhatsHelper.BuildInternationalWhatsDigits(If(l.WhatsApp, String.Empty), If(l.WhatsAppPrefix, String.Empty))
        Return Not String.IsNullOrWhiteSpace(digits) AndAlso digits.Length >= 8
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
        BindLabs()
    End Sub

    Private Sub HandleLabValueChanged(ByVal sender As Object, ByVal e As LabIndexChangedEvent) Handles Me.LabValueChanged
    End Sub

    Public Function GetLabID(LabName As String) As Integer
        If String.IsNullOrWhiteSpace(LabName) Then Return -1
        Const sql As String = "SELECT LabID FROM Lab WHERE LabName = @LabName"
        Using conn As New SqlConnection(_connectionString)
            Dim result As Integer? = conn.QuerySingleOrDefault(Of Integer?)(sql, New With {.LabName = LabName})
            Return If(result.HasValue, result.Value, -1)
        End Using
    End Function

    Public Function GetLabName(LabID As Integer) As String
        Const sql As String = "SELECT LabName FROM Lab WHERE LabID = @LabID"
        Using conn As New SqlConnection(_connectionString)
            Return If(conn.QuerySingleOrDefault(Of String)(sql, New With {.LabID = LabID}), String.Empty)
        End Using
    End Function

    Public Sub SetSelectedLabName(LabID As Integer)
        Me.LabID = LabID
        UpdateLabIDComboBoxSelection(LabID)
    End Sub

    Public Sub SetSelectedLabID(LabName As String)
        Me.LabID = GetLabID(LabName)
        UpdateLabIDComboBoxSelection(LabID)
    End Sub

    Public Class ComboBoxItem
        Public Property ID As Integer
        Public Property Name As String
        Public Overrides Function ToString() As String
            Return Name
        End Function
    End Class

    Public Class LabCls
        Public Property LabID As Integer
        Public Property LabName As String
        Public Property WhatsAppPrefix As String
        Public Property WhatsApp As String
    End Class

    Public Class LabIndexChangedEvent
        Inherits EventArgs
        Public Property LabID As Integer
        Public Property LabName As String
        Public Sub New(labId As Integer, labName As String)
            Me.LabID = labId
            Me.LabName = labName
        End Sub
    End Class
End Class
