Imports System.ComponentModel
Imports System.Data
Imports System.Data.SqlClient
Imports System.Globalization
Imports System.Linq
Imports System.Windows.Forms
Imports Dapper
Imports DevExpress.Utils
Imports DevExpress.XtraEditors

Public Class ImplantLengthCombo
    Implements INotifyPropertyChanged

    Private ReadOnly _connectionString As String = DentistXDATA.GetConnection.connectionString

    Public Event ImplantLengthValueChanged(ByVal sender As Object, ByVal e As ImplantLengthIndexChangedEvent)

    Private _selectedLengthID As Integer
    Public Property LengthID As Integer
        Get
            Return _selectedLengthID
        End Get
        Set(value As Integer)
            If Not Equals(_selectedLengthID, value) Then
                _selectedLengthID = value
                OnPropertyChanged(NameOf(LengthID))
                UpdateLengthIDComboBoxSelection(value)
            End If
        End Set
    End Property

    Private _selectedLengthMM As Decimal
    Public Property LengthMM As Decimal
        Get
            Return _selectedLengthMM
        End Get
        Set(value As Decimal)
            If Not Equals(_selectedLengthMM, value) Then
                _selectedLengthMM = value
                OnPropertyChanged(NameOf(LengthMM))
            End If
        End Set
    End Property

    Private _allRows As New List(Of ImplantLengthCls)()
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
        ComboToolbarLayoutHelper.ApplyPanelToolbar(PanelControl1, btnSerach, BtnAddImplantLength, _btnSearchVisible, _btnAddVisible, Me)
    End Sub

    <Browsable(True), Category("Data"), Description("Optional substring on length display (case-insensitive). Combined with flyout search (AND).")>
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
        If CboImplantLength Is Nothing OrElse _applyingFilter Then Return
        _applyingFilter = True
        Dim searchTyped As String = If(txtSearch IsNot Nothing, txtSearch.Text, String.Empty).Trim()
        Dim external As String = _nameFilter.Trim()
        Dim filtered = _allRows.Where(Function(r) MatchesFilters(DisplayForMm(r.LengthMM), searchTyped, external)).ToList()
        Dim preserveId As Integer = _selectedLengthID

        Try
            RemoveHandler CboImplantLength.SelectedIndexChanged, AddressOf CboImplantLength_SelectedIndexChanged

            CboImplantLength.Properties.Items.Clear()
            For Each r As ImplantLengthCls In filtered
                CboImplantLength.Properties.Items.Add(New ComboBoxItem With {.ID = r.LengthID, .Name = DisplayForMm(r.LengthMM), .MetricValue = r.LengthMM})
            Next

            Dim keepItem As ComboBoxItem = Nothing
            If preserveId > 0 Then
                For Each item As ComboBoxItem In CboImplantLength.Properties.Items
                    If item.ID = preserveId Then
                        keepItem = item
                        Exit For
                    End If
                Next
            End If

            If keepItem IsNot Nothing Then
                CboImplantLength.SelectedItem = keepItem
                _selectedLengthID = keepItem.ID
                _selectedLengthMM = keepItem.MetricValue
            ElseIf String.IsNullOrWhiteSpace(searchTyped) AndAlso String.IsNullOrWhiteSpace(external) AndAlso CboImplantLength.Properties.Items.Count > 0 Then
                CboImplantLength.SelectedIndex = 0
                Dim first As ComboBoxItem = TryCast(CboImplantLength.SelectedItem, ComboBoxItem)
                If first IsNot Nothing Then
                    _selectedLengthID = first.ID
                    _selectedLengthMM = first.MetricValue
                End If
            Else
                CboImplantLength.EditValue = Nothing
                _selectedLengthID = 0
                _selectedLengthMM = 0D
            End If

            SetTextEditStyle(CboImplantLength)
        Finally
            AddHandler CboImplantLength.SelectedIndexChanged, AddressOf CboImplantLength_SelectedIndexChanged
            _applyingFilter = False
        End Try
    End Sub

    Private Sub BindImplantLengths()
        _allRows = GetImplantLengths()
        ApplyNameFilter()
    End Sub

    Private Sub UpdateLengthIDComboBoxSelection(lengthId As Integer)
        ClearSearchBox()
        If lengthId <= 0 Then
            CboImplantLength.EditValue = Nothing
            _selectedLengthMM = 0D
            ApplyNameFilter()
            Return
        End If
        Dim row = _allRows.FirstOrDefault(Function(r) r.LengthID = lengthId)
        If row Is Nothing Then
            CboImplantLength.EditValue = Nothing
            _selectedLengthMM = 0D
            ApplyNameFilter()
            Return
        End If
        ApplyNameFilter()
        For Each item As ComboBoxItem In CboImplantLength.Properties.Items
            If item.ID = lengthId Then
                CboImplantLength.SelectedItem = item
                _selectedLengthMM = item.MetricValue
                Return
            End If
        Next
        CboImplantLength.EditValue = Nothing
        _selectedLengthMM = 0D
    End Sub

    Private Sub CboImplantLength_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CboImplantLength.SelectedIndexChanged
        If _applyingFilter Then Return
        Dim selectedItem As ComboBoxItem = TryCast(CboImplantLength.SelectedItem, ComboBoxItem)
        If selectedItem IsNot Nothing Then
            LengthID = selectedItem.ID
            LengthMM = selectedItem.MetricValue
            RaiseEvent ImplantLengthValueChanged(sender, New ImplantLengthIndexChangedEvent(LengthID, LengthMM))
        End If
    End Sub

    Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged
        If _suppressSearchTextChanged OrElse _applyingFilter Then Return
        ApplyNameFilter()
    End Sub

    Private Sub Flyout1_Showing(sender As Object, e As FlyoutPanelEventArgs) Handles Flyout1.Showing
        ComboFlyoutSearchHelper.ApplyManualPositionBelowPanel(Flyout1, PanelControl2, CboImplantLength)
    End Sub

    Private Sub btnSerach_Click(sender As Object, e As EventArgs) Handles btnSerach.Click
        ComboFlyoutSearchHelper.ShowFlyoutSearchDeferred(Flyout1, PanelControl2, CboImplantLength, txtSearch, Me)
    End Sub

    Private Sub btnAddImplantLength_Click(sender As Object, e As EventArgs) Handles BtnAddImplantLength.Click
        FrmImplantLength.ShowDialog()
        BindImplantLengths()
    End Sub

    Public Function GetImplantLengthTable() As DataTable
        Dim dt As New DataTable()
        dt.Columns.Add("LengthID", GetType(Integer))
        dt.Columns.Add("LengthMM", GetType(Decimal))
        For Each r In _allRows
            dt.Rows.Add(r.LengthID, r.LengthMM)
        Next
        Return dt
    End Function

    Private Function GetImplantLengths() As List(Of ImplantLengthCls)
        Const sql As String = "SELECT LengthID, LengthMM FROM ImplantLength;"
        Using conn As New SqlConnection(_connectionString)
            conn.Open()
            Return conn.Query(Of ImplantLengthCls)(sql).ToList()
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
        BindImplantLengths()
    End Sub

    Private Sub HandleImplantLengthValueChanged(ByVal sender As Object, ByVal e As ImplantLengthIndexChangedEvent) Handles Me.ImplantLengthValueChanged
    End Sub

    Public Function GetLengthID(lengthMm As Decimal) As Integer
        Const sql As String = "SELECT LengthID FROM ImplantLength WHERE LengthMM = @LengthMM"
        Using conn As New SqlConnection(_connectionString)
            Dim result As Integer? = conn.QuerySingleOrDefault(Of Integer?)(sql, New With {.LengthMM = lengthMm})
            Return If(result.HasValue, result.Value, -1)
        End Using
    End Function

    Public Function GetLengthID(lengthMmText As String) As Integer
        Dim d As Decimal
        If Not Decimal.TryParse(lengthMmText, NumberStyles.Any, CultureInfo.CurrentCulture, d) AndAlso
            Not Decimal.TryParse(lengthMmText, NumberStyles.Any, CultureInfo.InvariantCulture, d) Then
            Return -1
        End If
        Return GetLengthID(d)
    End Function

    Public Function GetLengthMM(lengthId As Integer) As String
        Const sql As String = "SELECT LengthMM FROM ImplantLength WHERE LengthID = @LengthID"
        Using conn As New SqlConnection(_connectionString)
            Dim result As Decimal? = conn.QuerySingleOrDefault(Of Decimal?)(sql, New With {.LengthID = lengthId})
            If Not result.HasValue Then Return String.Empty
            Return DisplayForMm(result.Value)
        End Using
    End Function

    Public Sub SetSelectedLengthMM(lengthId As Integer)
        Me.LengthID = lengthId
        UpdateLengthIDComboBoxSelection(lengthId)
    End Sub

    Public Sub SetSelectedLengthID(lengthMmText As String)
        Me.LengthID = GetLengthID(lengthMmText)
        UpdateLengthIDComboBoxSelection(LengthID)
    End Sub

    Public Class ComboBoxItem
        Public Property ID As Integer
        Public Property Name As String
        Public Property MetricValue As Decimal
        Public Overrides Function ToString() As String
            Return Name
        End Function
    End Class

    Public Class ImplantLengthCls
        Public Property LengthID As Integer
        Public Property LengthMM As Decimal
    End Class

    Public Class ImplantLengthIndexChangedEvent
        Inherits EventArgs
        Public Property LengthID As Integer
        Public Property LengthMM As Decimal
        Public Sub New(lengthId As Integer, lengthMm As Decimal)
            Me.LengthID = lengthId
            Me.LengthMM = lengthMm
        End Sub
    End Class
End Class
