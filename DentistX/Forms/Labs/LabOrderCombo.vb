Imports System.ComponentModel
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports System.Linq
Imports System.Windows.Forms
Imports Dapper
Imports DevExpress.Utils
Imports DevExpress.XtraEditors

Public Class LabOrderCombo
    Implements INotifyPropertyChanged

    Private ReadOnly _connectionString As String = DentistXDATA.GetConnection.ConnectionString

    Private _filterLabId As Integer

    Private Shared Function NormalizeOrderDetails(value As Object) As String
        If value Is Nothing OrElse IsDBNull(value) Then
            Return String.Empty
        End If
        Return Convert.ToString(value)
    End Function

    Public Event LabOrderValueChanged(ByVal sender As Object, ByVal e As LabOrderIndexChangedEvent)

    Private _allLabOrders As New List(Of LabOrderCls)()
    Private _nameFilter As String = String.Empty
    Private _applyingFilter As Boolean
    Private _suppressSearchTextChanged As Boolean
    Private _btnAddVisible As Boolean = True
    Private _btnSearchVisible As Boolean = True

    Public Sub New()
        InitializeComponent()
        _filterLabId = 0
        ApplyToolbarLayout()
        BindLabOrders()
    End Sub

    Public Sub SetLabOrderLabFilter(labId As Integer)
        _filterLabId = labId
        _selectedLabOrderID = 0
        _selectedOrderDetails = String.Empty
        ClearSearchBox()
        _allLabOrders.Clear()
        CboLabOrder.Properties.Items.Clear()
        CboLabOrder.EditValue = Nothing
        If labId > 0 Then
            BindLabOrders()
        Else
            SetTextEditStyle(CboLabOrder)
        End If
        OnPropertyChanged(NameOf(LabOrderID))
        OnPropertyChanged(NameOf(OrderDetails))
        RaiseEvent LabOrderValueChanged(Me, New LabOrderIndexChangedEvent(LabOrderID, OrderDetails))
    End Sub

    Private _selectedLabOrderID As Integer
    Public Property LabOrderID As Integer
        Get
            Return _selectedLabOrderID
        End Get
        Set(value As Integer)
            If Not Equals(_selectedLabOrderID, value) Then
                _selectedLabOrderID = value
                OnPropertyChanged(NameOf(LabOrderID))
                UpdateLabOrderIDComboBoxSelection(value)
            End If
        End Set
    End Property

    Private _selectedOrderDetails As String
    Public Property OrderDetails As String
        Get
            Return _selectedOrderDetails
        End Get
        Set(value As String)
            Dim normalized = If(value, String.Empty)
            If Not Equals(_selectedOrderDetails, normalized) Then
                _selectedOrderDetails = normalized
                OnPropertyChanged(NameOf(OrderDetails))
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
        ComboToolbarLayoutHelper.ApplyPanelToolbar(PanelControl1, btnSerach, BtnAddLabOrder, _btnSearchVisible, _btnAddVisible, Me)
    End Sub

    <Browsable(True), Category("Data"), Description("Optional substring to restrict orders by details text (case-insensitive). Combined with the flyout search using AND logic.")>
    Public Property Filter As String
        Get
            Return _nameFilter
        End Get
        Set(value As String)
            _nameFilter = If(value, String.Empty)
            ApplyLabOrderFilter()
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

    Private Function DisplayLabel(o As LabOrderCls) As String
        Dim label = NormalizeOrderDetails(o.OrderDetails)
        If String.IsNullOrWhiteSpace(label) Then
            label = "#" & o.LabOrderID.ToString()
        End If
        Return label
    End Function

    Private Sub ApplyLabOrderFilter()
        If CboLabOrder Is Nothing OrElse _applyingFilter Then Return
        If _filterLabId <= 0 Then
            CboLabOrder.Properties.Items.Clear()
            CboLabOrder.EditValue = Nothing
            SetTextEditStyle(CboLabOrder)
            Return
        End If

        _applyingFilter = True
        Dim searchTyped As String = If(txtSearch IsNot Nothing, txtSearch.Text, String.Empty).Trim()
        Dim external As String = _nameFilter.Trim()
        Dim filtered = _allLabOrders.Where(Function(o) MatchesNameFilters(DisplayLabel(o), searchTyped, external)).ToList()
        Dim preserveId As Integer = _selectedLabOrderID

        Try
            RemoveHandler CboLabOrder.SelectedIndexChanged, AddressOf CboLabOrder_SelectedIndexChanged

            CboLabOrder.Properties.Items.Clear()
            For Each LabOrder As LabOrderCls In filtered
                CboLabOrder.Properties.Items.Add(New ComboBoxItem With {.ID = LabOrder.LabOrderID, .Name = DisplayLabel(LabOrder)})
            Next

            Dim keepItem As ComboBoxItem = Nothing
            If preserveId > 0 Then
                For Each item As ComboBoxItem In CboLabOrder.Properties.Items
                    If item.ID = preserveId Then
                        keepItem = item
                        Exit For
                    End If
                Next
            End If

            If keepItem IsNot Nothing Then
                CboLabOrder.SelectedItem = keepItem
                _selectedLabOrderID = keepItem.ID
                _selectedOrderDetails = If(keepItem.Name, String.Empty)
            ElseIf String.IsNullOrWhiteSpace(searchTyped) AndAlso String.IsNullOrWhiteSpace(external) AndAlso CboLabOrder.Properties.Items.Count > 0 Then
                CboLabOrder.SelectedIndex = 0
                Dim first As ComboBoxItem = TryCast(CboLabOrder.SelectedItem, ComboBoxItem)
                If first IsNot Nothing Then
                    _selectedLabOrderID = first.ID
                    _selectedOrderDetails = If(first.Name, String.Empty)
                End If
            Else
                CboLabOrder.EditValue = Nothing
                _selectedLabOrderID = 0
                _selectedOrderDetails = String.Empty
            End If

            SetTextEditStyle(CboLabOrder)
        Finally
            AddHandler CboLabOrder.SelectedIndexChanged, AddressOf CboLabOrder_SelectedIndexChanged
            _applyingFilter = False
        End Try
    End Sub

    Private Sub BindLabOrders()
        If _filterLabId <= 0 Then
            _allLabOrders.Clear()
            ApplyLabOrderFilter()
            Return
        End If
        _allLabOrders = GetLabOrdersForLab(_filterLabId)
        For Each o In _allLabOrders
            o.OrderDetails = NormalizeOrderDetails(o.OrderDetails)
        Next
        ApplyLabOrderFilter()
    End Sub

    Private Sub UpdateLabOrderIDComboBoxSelection(LabOrderID As Integer)
        ClearSearchBox()
        If LabOrderID <= 0 Then
            CboLabOrder.EditValue = Nothing
            _selectedOrderDetails = String.Empty
            ApplyLabOrderFilter()
            Return
        End If
        Dim row = _allLabOrders.FirstOrDefault(Function(o) o.LabOrderID = LabOrderID)
        If row Is Nothing Then
            CboLabOrder.EditValue = Nothing
            _selectedOrderDetails = String.Empty
            ApplyLabOrderFilter()
            Return
        End If
        ApplyLabOrderFilter()
        For Each item As ComboBoxItem In CboLabOrder.Properties.Items
            If item.ID = LabOrderID Then
                CboLabOrder.SelectedItem = item
                _selectedOrderDetails = If(item.Name, String.Empty)
                Return
            End If
        Next
        CboLabOrder.EditValue = Nothing
        _selectedOrderDetails = String.Empty
    End Sub

    Private Sub CboLabOrder_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CboLabOrder.SelectedIndexChanged
        If _applyingFilter Then Return
        Dim selectedItem As ComboBoxItem = TryCast(CboLabOrder.SelectedItem, ComboBoxItem)
        If selectedItem Is Nothing Then Return
        LabOrderID = selectedItem.ID
        OrderDetails = If(selectedItem.Name, String.Empty)
        RaiseEvent LabOrderValueChanged(sender, New LabOrderIndexChangedEvent(LabOrderID, OrderDetails))
    End Sub

    Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged
        If _suppressSearchTextChanged OrElse _applyingFilter Then Return
        ApplyLabOrderFilter()
    End Sub

    Private Sub Flyout1_Showing(sender As Object, e As FlyoutPanelEventArgs) Handles Flyout1.Showing
        ComboFlyoutSearchHelper.ApplyManualPositionBelowPanel(Flyout1, PanelControl2, CboLabOrder)
    End Sub

    Private Sub btnSerach_Click(sender As Object, e As EventArgs) Handles btnSerach.Click
        ComboFlyoutSearchHelper.ShowFlyoutSearchDeferred(Flyout1, PanelControl2, CboLabOrder, txtSearch, Me)
    End Sub

    Private Sub btnAddLabOrder_Click(sender As Object, e As EventArgs) Handles BtnAddLabOrder.Click
        FrmLabOrder2.ShowDialog()
        BindLabOrders()
    End Sub

    Private Sub SetTextEditStyle(ByVal comboBox As ComboBoxEdit)
        comboBox.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    End Sub

    Private Function GetLabOrdersForLab(labId As Integer) As List(Of LabOrderCls)
        Const query As String = "SELECT LabOrderID, OrderDetails FROM dbo.LabOrder WHERE LabID = @LabID ORDER BY LabOrderID DESC"
        Using conn As New SqlConnection(_connectionString)
            conn.Open()
            Return conn.Query(Of LabOrderCls)(query, New With {.LabID = labId}).ToList()
        End Using
    End Function

    Public Function GetLabOrderIDByOrderDetails(OrderDetails As String) As Integer
        If String.IsNullOrWhiteSpace(OrderDetails) Then Return -1
        Const query As String = "SELECT LabOrderID FROM LabOrder WHERE OrderDetails = @OrderDetails"
        Using conn As New SqlConnection(_connectionString)
            Dim result As Integer? = conn.QuerySingleOrDefault(Of Integer?)(query, New With {.OrderDetails = OrderDetails})
            Return If(result.HasValue, result.Value, -1)
        End Using
    End Function

    Public Function GetOrderDetailsByLabOrderID(LabOrderID As Integer) As String
        Const query As String = "SELECT OrderDetails FROM LabOrder WHERE LabOrderID = @LabOrderID"
        Using conn As New SqlConnection(_connectionString)
            Return If(conn.QuerySingleOrDefault(Of String)(query, New With {.LabOrderID = LabOrderID}), String.Empty)
        End Using
    End Function

    Public Sub SetOrderDetailsByLabOrderID(LabOrderID As Integer)
        Me.LabOrderID = LabOrderID
        UpdateLabOrderIDComboBoxSelection(LabOrderID)
    End Sub

    Public Sub SetLabOrderIDByOrderDetails(OrderDetails As String)
        If String.IsNullOrWhiteSpace(OrderDetails) Then
            ClearSelection()
            Return
        End If
        Me.LabOrderID = GetLabOrderIDByOrderDetails(OrderDetails)
        UpdateLabOrderIDComboBoxSelection(LabOrderID)
    End Sub

    Public Sub RefreshLabOrder()
        BindLabOrders()
    End Sub

    Public Sub ClearSelection()
        CboLabOrder.SelectedIndex = -1
        Me.LabOrderID = 0
        Me.OrderDetails = String.Empty
    End Sub

    Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged
    Protected Overridable Sub OnPropertyChanged(ByVal propertyName As String)
        RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(propertyName))
    End Sub

    Public Class ComboBoxItem
        Public Property ID As Integer
        Public Property Name As String
        Public Overrides Function ToString() As String
            Return If(Name, String.Empty)
        End Function
    End Class

    Public Class LabOrderCls
        Public Property LabOrderID As Integer
        Public Property OrderDetails As String
    End Class

    Public Class LabOrderIndexChangedEvent
        Inherits EventArgs
        Public Property LabOrderID As Integer
        Public Property OrderDetails As String
        Public Sub New(_LabOrderID As Integer, _OrderDetails As String)
            Me.LabOrderID = _LabOrderID
            Me.OrderDetails = _OrderDetails
        End Sub
    End Class
End Class
