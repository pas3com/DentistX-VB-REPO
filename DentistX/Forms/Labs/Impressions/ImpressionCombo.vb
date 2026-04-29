Imports System.ComponentModel
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports System.Linq
Imports System.Windows.Forms
Imports Dapper
Imports DevExpress.Utils
Imports DevExpress.XtraEditors

Public Class ImpressionCombo
    Implements INotifyPropertyChanged

    Private ReadOnly _connectionString As String = DentistXDATA.GetConnection.connectionString

    Public Event ImpressionValueChanged(ByVal sender As Object, ByVal e As ImpressionIndexChangedEvent)

    Private _selectedImprID As Integer
    Public Property ImprID As Integer
        Get
            Return _selectedImprID
        End Get
        Set(value As Integer)
            If Not Equals(_selectedImprID, value) Then
                _selectedImprID = value
                OnPropertyChanged(NameOf(ImprID))
                UpdateImprIDComboBoxSelection(value)
            End If
        End Set
    End Property

    Private _selectedImprType As String
    Public Property ImprType As String
        Get
            Return _selectedImprType
        End Get
        Set(value As String)
            If Not Equals(_selectedImprType, value) Then
                _selectedImprType = value
                OnPropertyChanged(NameOf(ImprType))
            End If
        End Set
    End Property

    Private _allRows As New List(Of ImpressionCls)()
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
        ComboToolbarLayoutHelper.ApplyPanelToolbar(PanelControl1, btnSerach, BtnAddImpression, _btnSearchVisible, _btnAddVisible, Me)
    End Sub

    <Browsable(True), Category("Data"), Description("Optional substring to restrict by impression type (case-insensitive). Combined with the flyout search using AND logic.")>
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

    Private Sub ApplyNameFilter()
        If CboImpression Is Nothing OrElse _applyingFilter Then Return
        _applyingFilter = True
        Dim searchTyped As String = If(txtSearch IsNot Nothing, txtSearch.Text, String.Empty).Trim()
        Dim external As String = _nameFilter.Trim()
        Dim filtered = _allRows.Where(Function(r) MatchesNameFilters(If(r.ImprType, String.Empty), searchTyped, external)).ToList()
        Dim preserveId As Integer = _selectedImprID

        Try
            RemoveHandler CboImpression.SelectedIndexChanged, AddressOf CboImpression_SelectedIndexChanged

            CboImpression.Properties.Items.Clear()
            For Each r As ImpressionCls In filtered
                CboImpression.Properties.Items.Add(New ComboBoxItem With {.ID = r.ImprID, .Name = r.ImprType})
            Next

            Dim keepItem As ComboBoxItem = Nothing
            If preserveId > 0 Then
                For Each item As ComboBoxItem In CboImpression.Properties.Items
                    If item.ID = preserveId Then
                        keepItem = item
                        Exit For
                    End If
                Next
            End If

            If keepItem IsNot Nothing Then
                CboImpression.SelectedItem = keepItem
                _selectedImprID = keepItem.ID
                _selectedImprType = keepItem.Name
            ElseIf String.IsNullOrWhiteSpace(searchTyped) AndAlso String.IsNullOrWhiteSpace(external) AndAlso CboImpression.Properties.Items.Count > 0 Then
                CboImpression.SelectedIndex = 0
                Dim first As ComboBoxItem = TryCast(CboImpression.SelectedItem, ComboBoxItem)
                If first IsNot Nothing Then
                    _selectedImprID = first.ID
                    _selectedImprType = first.Name
                End If
            Else
                CboImpression.EditValue = Nothing
                _selectedImprID = 0
                _selectedImprType = Nothing
            End If

            SetTextEditStyle(CboImpression)
        Finally
            AddHandler CboImpression.SelectedIndexChanged, AddressOf CboImpression_SelectedIndexChanged
            _applyingFilter = False
        End Try
    End Sub

    Private Sub BindImpressions()
        Const sql As String = "SELECT ImprID, ImprType FROM Impression;"
        Using conn As New SqlConnection(_connectionString)
            conn.Open()
            _allRows = conn.Query(Of ImpressionCls)(sql).ToList()
        End Using
        ApplyNameFilter()
    End Sub

    Private Sub UpdateImprIDComboBoxSelection(ImprID As Integer)
        ClearSearchBox()
        If ImprID <= 0 Then
            CboImpression.EditValue = Nothing
            _selectedImprType = Nothing
            ApplyNameFilter()
            Return
        End If
        If _allRows.FirstOrDefault(Function(r) r.ImprID = ImprID) Is Nothing Then
            CboImpression.EditValue = Nothing
            _selectedImprType = Nothing
            ApplyNameFilter()
            Return
        End If
        ApplyNameFilter()
        For Each item As ComboBoxItem In CboImpression.Properties.Items
            If item.ID = ImprID Then
                CboImpression.SelectedItem = item
                _selectedImprType = item.Name
                Return
            End If
        Next
        CboImpression.EditValue = Nothing
        _selectedImprType = Nothing
    End Sub

    Private Sub CboImpression_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CboImpression.SelectedIndexChanged
        If _applyingFilter Then Return
        Dim selectedItem As ComboBoxItem = TryCast(CboImpression.SelectedItem, ComboBoxItem)
        If selectedItem IsNot Nothing Then
            ImprID = selectedItem.ID
            ImprType = selectedItem.Name
            RaiseEvent ImpressionValueChanged(sender, New ImpressionIndexChangedEvent(ImprID, ImprType))
        End If
    End Sub

    Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged
        If _suppressSearchTextChanged OrElse _applyingFilter Then Return
        ApplyNameFilter()
    End Sub

    Private Sub Flyout1_Showing(sender As Object, e As FlyoutPanelEventArgs) Handles Flyout1.Showing
        ComboFlyoutSearchHelper.ApplyManualPositionBelowPanel(Flyout1, PanelControl2, CboImpression)
    End Sub

    Private Sub btnSerach_Click(sender As Object, e As EventArgs) Handles btnSerach.Click
        ComboFlyoutSearchHelper.ShowFlyoutSearchDeferred(Flyout1, PanelControl2, CboImpression, txtSearch, Me)
    End Sub

    Private Sub btnAddImpression_Click(sender As Object, e As EventArgs) Handles BtnAddImpression.Click
        FrmImpression.ShowDialog()
        BindImpressions()
    End Sub

    Public Function GetImpressionTable() As DataTable
        Dim dt As New DataTable()
        dt.Columns.Add("ImprID", GetType(Integer))
        dt.Columns.Add("ImprType", GetType(String))
        For Each r In _allRows
            dt.Rows.Add(r.ImprID, If(r.ImprType, CType(String.Empty, Object)))
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
        BindImpressions()
    End Sub

    Private Sub HandleImpressionValueChanged(ByVal sender As Object, ByVal e As ImpressionIndexChangedEvent) Handles Me.ImpressionValueChanged
    End Sub

    Public Function GetImprID(ImprType As String) As Integer
        If String.IsNullOrWhiteSpace(ImprType) Then Return -1
        Const sql As String = "SELECT ImprID FROM Impression WHERE ImprType = @ImprType"
        Using conn As New SqlConnection(_connectionString)
            Dim result As Integer? = conn.QuerySingleOrDefault(Of Integer?)(sql, New With {.ImprType = ImprType})
            Return If(result.HasValue, result.Value, -1)
        End Using
    End Function

    Public Function GetImprType(ImprID As Integer) As String
        Const sql As String = "SELECT ImprType FROM Impression WHERE ImprID = @ImprID"
        Using conn As New SqlConnection(_connectionString)
            Return If(conn.QuerySingleOrDefault(Of String)(sql, New With {.ImprID = ImprID}), String.Empty)
        End Using
    End Function

    Public Sub SetSelectedImprType(ImprID As Integer)
        Me.ImprID = ImprID
        UpdateImprIDComboBoxSelection(ImprID)
    End Sub

    Public Sub SetSelectedImprID(ImprType As String)
        Me.ImprID = GetImprID(ImprType)
        UpdateImprIDComboBoxSelection(ImprID)
    End Sub

    Public Class ComboBoxItem
        Public Property ID As Integer
        Public Property Name As String
        Public Overrides Function ToString() As String
            Return Name
        End Function
    End Class

    Public Class ImpressionCls
        Public Property ImprID As Integer
        Public Property ImprType As String
    End Class

    Public Class ImpressionIndexChangedEvent
        Inherits EventArgs
        Public Property ImprID As Integer
        Public Property ImprType As String
        Public Sub New(imprId As Integer, imprType As String)
            Me.ImprID = imprId
            Me.ImprType = imprType
        End Sub
    End Class
End Class
