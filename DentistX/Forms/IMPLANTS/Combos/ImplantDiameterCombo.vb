Imports System.ComponentModel
Imports System.Data
Imports System.Data.SqlClient
Imports System.Globalization
Imports System.Linq
Imports System.Windows.Forms
Imports Dapper
Imports DevExpress.Utils
Imports DevExpress.XtraEditors

Public Class ImplantDiameterCombo
    Implements INotifyPropertyChanged

    Private ReadOnly _connectionString As String = DentistXDATA.GetConnection.connectionString

    Public Event ImplantDiameterValueChanged(ByVal sender As Object, ByVal e As ImplantDiameterIndexChangedEvent)

    Private _selectedDiameterID As Integer
    Public Property DiameterID As Integer
        Get
            Return _selectedDiameterID
        End Get
        Set(value As Integer)
            If Not Equals(_selectedDiameterID, value) Then
                _selectedDiameterID = value
                OnPropertyChanged(NameOf(DiameterID))
                UpdateDiameterIDComboBoxSelection(value)
            End If
        End Set
    End Property

    Private _selectedDiameterMM As Decimal
    Public Property DiameterMM As Decimal
        Get
            Return _selectedDiameterMM
        End Get
        Set(value As Decimal)
            If Not Equals(_selectedDiameterMM, value) Then
                _selectedDiameterMM = value
                OnPropertyChanged(NameOf(DiameterMM))
            End If
        End Set
    End Property

    Private _allRows As New List(Of ImplantDiameterCls)()
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
        ComboToolbarLayoutHelper.ApplyPanelToolbar(PanelControl1, btnSerach, BtnAddImplantDiameter, _btnSearchVisible, _btnAddVisible, Me)
    End Sub

    <Browsable(True), Category("Data"), Description("Optional substring on diameter display (case-insensitive). Combined with flyout search (AND).")>
    Public Property Filter As String
        Get
            Return _nameFilter
        End Get
        Set(value As String)
            _nameFilter = If(value, String.Empty)
            ApplyNameFilter()
        End Set
    End Property

    Private Function MatchesFilters(displayText As String, searchContains As String, externalContains As String) As Boolean
        If String.IsNullOrEmpty(displayText) Then Return False
        If Not String.IsNullOrEmpty(externalContains) AndAlso displayText.IndexOf(externalContains, StringComparison.OrdinalIgnoreCase) < 0 Then Return False
        If Not String.IsNullOrEmpty(searchContains) AndAlso displayText.IndexOf(searchContains, StringComparison.OrdinalIgnoreCase) < 0 Then Return False
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

    Private Shared Function DisplayForMm(mm As Decimal) As String
        Return mm.ToString(CultureInfo.CurrentCulture)
    End Function

    Private Sub ApplyNameFilter()
        If CboImplantDiameter Is Nothing OrElse _applyingFilter Then Return
        _applyingFilter = True
        Dim searchTyped As String = If(txtSearch IsNot Nothing, txtSearch.Text, String.Empty).Trim()
        Dim external As String = _nameFilter.Trim()
        Dim filtered = _allRows.Where(Function(r) MatchesFilters(DisplayForMm(r.DiameterMM), searchTyped, external)).ToList()
        Dim preserveId As Integer = _selectedDiameterID

        Try
            RemoveHandler CboImplantDiameter.SelectedIndexChanged, AddressOf CboImplantDiameter_SelectedIndexChanged

            CboImplantDiameter.Properties.Items.Clear()
            For Each r As ImplantDiameterCls In filtered
                CboImplantDiameter.Properties.Items.Add(New ComboBoxItem With {.ID = r.DiameterID, .Name = DisplayForMm(r.DiameterMM), .MetricValue = r.DiameterMM})
            Next

            Dim keepItem As ComboBoxItem = Nothing
            If preserveId > 0 Then
                For Each item As ComboBoxItem In CboImplantDiameter.Properties.Items
                    If item.ID = preserveId Then
                        keepItem = item
                        Exit For
                    End If
                Next
            End If

            If keepItem IsNot Nothing Then
                CboImplantDiameter.SelectedItem = keepItem
                _selectedDiameterID = keepItem.ID
                _selectedDiameterMM = keepItem.MetricValue
            ElseIf String.IsNullOrWhiteSpace(searchTyped) AndAlso String.IsNullOrWhiteSpace(external) AndAlso CboImplantDiameter.Properties.Items.Count > 0 Then
                CboImplantDiameter.SelectedIndex = 0
                Dim first As ComboBoxItem = TryCast(CboImplantDiameter.SelectedItem, ComboBoxItem)
                If first IsNot Nothing Then
                    _selectedDiameterID = first.ID
                    _selectedDiameterMM = first.MetricValue
                End If
            Else
                CboImplantDiameter.EditValue = Nothing
                _selectedDiameterID = 0
                _selectedDiameterMM = 0D
            End If

            SetTextEditStyle(CboImplantDiameter)
        Finally
            AddHandler CboImplantDiameter.SelectedIndexChanged, AddressOf CboImplantDiameter_SelectedIndexChanged
            _applyingFilter = False
        End Try
    End Sub

    Private Sub BindImplantDiameters()
        _allRows = GetImplantDiameters()
        ApplyNameFilter()
    End Sub

    Private Sub UpdateDiameterIDComboBoxSelection(diameterId As Integer)
        ClearSearchBox()
        If diameterId <= 0 Then
            CboImplantDiameter.EditValue = Nothing
            _selectedDiameterMM = 0D
            ApplyNameFilter()
            Return
        End If
        Dim row = _allRows.FirstOrDefault(Function(r) r.DiameterID = diameterId)
        If row Is Nothing Then
            CboImplantDiameter.EditValue = Nothing
            _selectedDiameterMM = 0D
            ApplyNameFilter()
            Return
        End If
        ApplyNameFilter()
        For Each item As ComboBoxItem In CboImplantDiameter.Properties.Items
            If item.ID = diameterId Then
                CboImplantDiameter.SelectedItem = item
                _selectedDiameterMM = item.MetricValue
                Return
            End If
        Next
        CboImplantDiameter.EditValue = Nothing
        _selectedDiameterMM = 0D
    End Sub

    Private Sub CboImplantDiameter_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CboImplantDiameter.SelectedIndexChanged
        If _applyingFilter Then Return
        Dim selectedItem As ComboBoxItem = TryCast(CboImplantDiameter.SelectedItem, ComboBoxItem)
        If selectedItem IsNot Nothing Then
            DiameterID = selectedItem.ID
            DiameterMM = selectedItem.MetricValue
            RaiseEvent ImplantDiameterValueChanged(sender, New ImplantDiameterIndexChangedEvent(DiameterID, DiameterMM))
        End If
    End Sub

    Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged
        If _suppressSearchTextChanged OrElse _applyingFilter Then Return
        ApplyNameFilter()
    End Sub

    Private Sub Flyout1_Showing(sender As Object, e As FlyoutPanelEventArgs) Handles Flyout1.Showing
        ComboFlyoutSearchHelper.ApplyManualPositionBelowPanel(Flyout1, PanelControl2, CboImplantDiameter)
    End Sub

    Private Sub btnSerach_Click(sender As Object, e As EventArgs) Handles btnSerach.Click
        ComboFlyoutSearchHelper.ShowFlyoutSearchDeferred(Flyout1, PanelControl2, CboImplantDiameter, txtSearch, Me)
    End Sub

    Private Sub btnAddImplantDiameter_Click(sender As Object, e As EventArgs) Handles BtnAddImplantDiameter.Click
        FrmImplantDiameter.ShowDialog()
        BindImplantDiameters()
    End Sub

    Public Function GetImplantDiameterTable() As DataTable
        Dim dt As New DataTable()
        dt.Columns.Add("DiameterID", GetType(Integer))
        dt.Columns.Add("DiameterMM", GetType(Decimal))
        For Each r In _allRows
            dt.Rows.Add(r.DiameterID, r.DiameterMM)
        Next
        Return dt
    End Function

    Private Function GetImplantDiameters() As List(Of ImplantDiameterCls)
        Const sql As String = "SELECT DiameterID, DiameterMM FROM ImplantDiameter;"
        Using conn As New SqlConnection(_connectionString)
            conn.Open()
            Return conn.Query(Of ImplantDiameterCls)(sql).ToList()
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
        BindImplantDiameters()
    End Sub

    Private Sub HandleImplantDiameterValueChanged(ByVal sender As Object, ByVal e As ImplantDiameterIndexChangedEvent) Handles Me.ImplantDiameterValueChanged
    End Sub

    Public Function GetDiameterID(diameterMm As Decimal) As Integer
        Const sql As String = "SELECT DiameterID FROM ImplantDiameter WHERE DiameterMM = @DiameterMM"
        Using conn As New SqlConnection(_connectionString)
            Dim result As Integer? = conn.QueryFirstOrDefault(Of Integer?)(sql, New With {.DiameterMM = diameterMm})
            Return If(result.HasValue, result.Value, -1)
        End Using
    End Function

    Public Function GetDiameterID(diameterMmText As String) As Integer
        Dim d As Decimal
        If Not Decimal.TryParse(diameterMmText, NumberStyles.Any, CultureInfo.CurrentCulture, d) AndAlso
            Not Decimal.TryParse(diameterMmText, NumberStyles.Any, CultureInfo.InvariantCulture, d) Then
            Return -1
        End If
        Return GetDiameterID(d)
    End Function

    Public Function GetDiameterMM(diameterId As Integer) As String
        Const sql As String = "SELECT DiameterMM FROM ImplantDiameter WHERE DiameterID = @DiameterID"
        Using conn As New SqlConnection(_connectionString)
            Dim result As Decimal? = conn.QuerySingleOrDefault(Of Decimal?)(sql, New With {.DiameterID = diameterId})
            If Not result.HasValue Then Return String.Empty
            Return DisplayForMm(result.Value)
        End Using
    End Function

    Public Sub SetSelectedDiameterMM(diameterId As Integer)
        Me.DiameterID = diameterId
        UpdateDiameterIDComboBoxSelection(diameterId)
    End Sub

    Public Sub SetSelectedDiameterID(diameterMmText As String)
        Me.DiameterID = GetDiameterID(diameterMmText)
        UpdateDiameterIDComboBoxSelection(DiameterID)
    End Sub

    Public Class ComboBoxItem
        Public Property ID As Integer
        Public Property Name As String
        Public Property MetricValue As Decimal
        Public Overrides Function ToString() As String
            Return Name
        End Function
    End Class

    Public Class ImplantDiameterCls
        Public Property DiameterID As Integer
        Public Property DiameterMM As Decimal
    End Class

    Public Class ImplantDiameterIndexChangedEvent
        Inherits EventArgs
        Public Property DiameterID As Integer
        Public Property DiameterMM As Decimal
        Public Sub New(diameterId As Integer, diameterMm As Decimal)
            Me.DiameterID = diameterId
            Me.DiameterMM = diameterMm
        End Sub
    End Class
End Class
