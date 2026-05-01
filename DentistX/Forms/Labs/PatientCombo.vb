Imports System.ComponentModel
Imports System.ComponentModel.Design.Serialization
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports System.Linq
Imports System.Windows.Forms
Imports Dapper
Imports DevExpress.Utils
Imports DevExpress.XtraEditors

Public Class PatientCombo
    Implements INotifyPropertyChanged

    Private ReadOnly _connectionString As String = DentistXDATA.GetConnection.ConnectionString
    Private _dataLoaded As Boolean

    ' Define events for property changes
    Public Event PatientValueChanged(ByVal sender As Object, ByVal e As PatientIndexChangedEvent)
    ' Define properties and backing fields
    Private _CurrentPatientID As Integer
    <Browsable(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)>
    Public Property PatientID As Integer
        Get
            Return _CurrentPatientID
        End Get
        Set(value As Integer)
            If Not Equals(_CurrentPatientID, value) Then
                _CurrentPatientID = value
                OnPropertyChanged(NameOf(PatientID))
                ' Update the combo box based on the new value
                UpdatePatientIDComboBoxSelection(value)
            End If
        End Set
    End Property

    Private _CurrentPatientName As String
    <Browsable(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)>
    Public Property PatientName As String
        Get
            Return _CurrentPatientName
        End Get
        Set(value As String)
            If Not Equals(_CurrentPatientName, value) Then
                _CurrentPatientName = value
                OnPropertyChanged(NameOf(PatientName))
            End If
        End Set
    End Property

    <Browsable(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)>
    Public ReadOnly Property HasPatients As Boolean
        Get
            Return _allPatients IsNot Nothing AndAlso _allPatients.Count > 0
        End Get
    End Property

    Private _allPatients As New List(Of PatientCls)()
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
        ComboToolbarLayoutHelper.ApplyPanelToolbar(PanelControl1, btnSerach, BtnAddPatient, _btnSearchVisible, _btnAddVisible, Me)
    End Sub

    ''' <summary>Code-driven name substring; combined with the flyout search text using AND logic.</summary>
    <Browsable(True), Category("Data"), Description("Optional substring to restrict patients by name (case-insensitive). Works together with the search box in the flyout.")>
    Public Property Filter As String
        Get
            Return _nameFilter
        End Get
        Set(value As String)
            _nameFilter = If(value, String.Empty)
            ApplyPatientNameFilter()
        End Set
    End Property

    Private Function MatchesNameFilters(patientName As String, searchContains As String, externalContains As String) As Boolean
        If String.IsNullOrEmpty(patientName) Then Return False
        If Not String.IsNullOrEmpty(externalContains) AndAlso patientName.IndexOf(externalContains, StringComparison.OrdinalIgnoreCase) < 0 Then Return False
        If Not String.IsNullOrEmpty(searchContains) AndAlso patientName.IndexOf(searchContains, StringComparison.OrdinalIgnoreCase) < 0 Then Return False
        Return True
    End Function

    Private Sub ApplySelectedPatient(selectedItem As ComboBoxItem, raiseChanged As Boolean)
        If selectedItem Is Nothing Then
            _CurrentPatientID = 0
            _CurrentPatientName = Nothing
            Return
        End If

        _CurrentPatientID = selectedItem.ID
        _CurrentPatientName = selectedItem.Name
        OnPropertyChanged(NameOf(PatientID))
        OnPropertyChanged(NameOf(PatientName))
        If raiseChanged Then
            RaiseEvent PatientValueChanged(Me, New PatientIndexChangedEvent(_CurrentPatientID, _CurrentPatientName))
        End If
    End Sub

    Public Sub ClearSearchBox()
        If txtSearch Is Nothing Then Return
        _suppressSearchTextChanged = True
        Try
            txtSearch.Text = String.Empty
        Finally
            _suppressSearchTextChanged = False
        End Try
    End Sub

    Private Sub ApplyPatientNameFilter()
        If CboPatient Is Nothing OrElse _applyingFilter Then Return
        _applyingFilter = True
        Dim searchTyped As String = If(txtSearch IsNot Nothing, txtSearch.Text, String.Empty).Trim()
        Dim external As String = _nameFilter.Trim()
        Dim filtered = _allPatients.Where(Function(p) MatchesNameFilters(p.PatientName, searchTyped, external)).ToList()
        Dim preserveId As Integer = _CurrentPatientID

        Try
            RemoveHandler CboPatient.SelectedIndexChanged, AddressOf CboPatient_SelectedIndexChanged

            CboPatient.Properties.Items.Clear()
            For Each Patient As PatientCls In filtered
                CboPatient.Properties.Items.Add(New ComboBoxItem With {.ID = Patient.PatientID, .Name = Patient.PatientName})
            Next

            Dim keepItem As ComboBoxItem = Nothing
            If preserveId > 0 Then
                For Each item As ComboBoxItem In CboPatient.Properties.Items
                    If item.ID = preserveId Then
                        keepItem = item
                        Exit For
                    End If
                Next
            End If

            If keepItem IsNot Nothing Then
                CboPatient.SelectedItem = keepItem
                ApplySelectedPatient(keepItem, raiseChanged:=False)
            ElseIf String.IsNullOrWhiteSpace(searchTyped) AndAlso String.IsNullOrWhiteSpace(external) AndAlso CboPatient.Properties.Items.Count > 0 Then
                CboPatient.SelectedIndex = 0
                Dim first As ComboBoxItem = TryCast(CboPatient.SelectedItem, ComboBoxItem)
                If first IsNot Nothing Then
                    ApplySelectedPatient(first, raiseChanged:=False)
                End If
            Else
                CboPatient.EditValue = Nothing
                _CurrentPatientID = 0
                _CurrentPatientName = Nothing
            End If

            SetTextEditStyle(CboPatient)
        Finally
            AddHandler CboPatient.SelectedIndexChanged, AddressOf CboPatient_SelectedIndexChanged
            _applyingFilter = False
        End Try
    End Sub

    ' Bind the ComboBoxes to the appropriate data
    Private Sub BindPatients()
        _allPatients = GetPatients()
        _dataLoaded = True
        ApplyPatientNameFilter()
    End Sub

    Public Sub EnsureDataLoaded()
        If _dataLoaded Then Return
        BindPatients()
    End Sub

    ' Update ComboBox selection based on property values
    Private Sub UpdatePatientIDComboBoxSelection(PatientID As Integer)
        ClearSearchBox()

        If PatientID <= 0 Then
            CboPatient.EditValue = Nothing
            _CurrentPatientName = Nothing
            ApplyPatientNameFilter()
            Return
        End If
        Dim row = _allPatients.FirstOrDefault(Function(p) p.PatientID = PatientID)
        If row Is Nothing Then
            CboPatient.EditValue = Nothing
            _CurrentPatientName = Nothing
            ApplyPatientNameFilter()
            Return
        End If

        ApplyPatientNameFilter()
        For Each item As ComboBoxItem In CboPatient.Properties.Items
            If item.ID = PatientID Then
                CboPatient.SelectedItem = item
                _CurrentPatientName = item.Name
                Return
            End If
        Next
        CboPatient.EditValue = Nothing
        _CurrentPatientName = Nothing
    End Sub

    ' ComboBox selection change handlers
    Private Sub CboPatient_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CboPatient.SelectedIndexChanged
        If _applyingFilter Then Return
        Dim selectedItem As ComboBoxItem = TryCast(CboPatient.SelectedItem, ComboBoxItem)
        If selectedItem IsNot Nothing Then
            PatientID = selectedItem.ID
            PatientName = selectedItem.Name

            RaiseEvent PatientValueChanged(sender, New PatientIndexChangedEvent(PatientID, PatientName))
        End If
    End Sub

    Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged
        If _suppressSearchTextChanged OrElse _applyingFilter Then Return
        ApplyPatientNameFilter()
    End Sub

    Private Sub Flyout1_Showing(sender As Object, e As FlyoutPanelEventArgs) Handles Flyout1.Showing
        ComboFlyoutSearchHelper.ApplyManualPositionBelowPanel(Flyout1, PanelControl2, CboPatient)
    End Sub

    Private Sub btnSerach_Click(sender As Object, e As EventArgs) Handles btnSerach.Click
        EnsureDataLoaded()
        txtSearch.SelectAll()
        ComboFlyoutSearchHelper.ShowFlyoutSearchDeferred(Flyout1, PanelControl2, CboPatient, txtSearch, Me)
    End Sub

    ' Event handlers for buttons
    Private Sub btnAddPatient_Click(sender As Object, e As EventArgs) Handles BtnAddPatient.Click
        PatientAddEditForm.ShowDialog()
        BindPatients()
    End Sub

    Public Function GetPatientTable() As DataTable
        EnsureDataLoaded()
        Dim rows = GetPatients()
        Dim dt As New DataTable()
        dt.Columns.Add("PatientID", GetType(Integer))
        dt.Columns.Add("PatientName", GetType(String))
        For Each p In rows
            dt.Rows.Add(p.PatientID, If(p.PatientName, CType(String.Empty, Object)))
        Next
        Return dt
    End Function

    Private Function GetPatients() As List(Of PatientCls)
        Const sql As String = "SELECT PatientID, PatientName FROM Patient;"
        Using conn As New SqlConnection(_connectionString)
            conn.Open()
            Return conn.Query(Of PatientCls)(sql).ToList()
        End Using
    End Function

    Public Function EnsureFirstPatientSelected() As Boolean
        If CboPatient Is Nothing OrElse CboPatient.Properties.Items.Count = 0 Then Return False

        Dim first As ComboBoxItem = TryCast(CboPatient.Properties.Items(0), ComboBoxItem)
        If first Is Nothing Then Return False

        CboPatient.SelectedIndex = 0
        ApplySelectedPatient(first, raiseChanged:=True)
        Return True
    End Function

    ' Utility methods for ComboBoxes
    Private Sub SetTextEditStyle(ByVal comboBox As ComboBoxEdit)
        comboBox.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard
    End Sub

    ' PropertyChanged event implementation
    Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged
    Protected Overridable Sub OnPropertyChanged(ByVal propertyName As String)
        RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(propertyName))
    End Sub

    ' Initialize bindings (in the control's load event or constructor)
    Public Sub New()
        InitializeComponent()
        ApplyToolbarLayout()
    End Sub

    Private Sub CboPatient_Enter(sender As Object, e As EventArgs) Handles CboPatient.Enter
        EnsureDataLoaded()
    End Sub

    ' Method to handle Patient value changed event
    Private Sub HandlePatientValueChanged(ByVal sender As Object, ByVal e As PatientIndexChangedEvent) Handles Me.PatientValueChanged
        ' Perform necessary actions when {tableName} value changes
        Me.PatientID = e.PatientID
        Me.PatientName = e.PatientName
    End Sub

    Public Function GetPatientID(PatientName As String) As Integer
        If String.IsNullOrWhiteSpace(PatientName) Then
            Return -1
        End If
        Const sql As String = "SELECT PatientID FROM Patient WHERE PatientName = @PatientName"
        Using conn As New SqlConnection(_connectionString)
            Dim result As Integer? = conn.QuerySingleOrDefault(Of Integer?)(sql, New With {.PatientName = PatientName})
            Return If(result.HasValue, result.Value, -1)
        End Using
    End Function

    Public Function GetPatientName(PatientID As Integer) As String
        Const sql As String = "SELECT PatientName FROM Patient WHERE PatientID = @PatientID"
        Using conn As New SqlConnection(_connectionString)
            Dim result As String = conn.QuerySingleOrDefault(Of String)(sql, New With {.PatientID = PatientID})
            Return If(result, String.Empty)
        End Using
    End Function

    ' Set selected PatientName by ID
    Public Sub SetCurrentPatientName(PatientID As Integer)
        If PatientID > 0 Then EnsureDataLoaded()
        Me.PatientID = PatientID
        UpdatePatientIDComboBoxSelection(PatientID)
    End Sub

    ' Set selected Patient by Name
    Public Sub SetCurrentPatientID(PatientName As String)
        EnsureDataLoaded()
        Me.PatientID = GetPatientID(PatientName)
        UpdatePatientIDComboBoxSelection(PatientID)
    End Sub

    ' ComboBoxItem class
    Public Class ComboBoxItem
        Public Property ID As Integer
        Public Property Name As String

        Public Overrides Function ToString() As String
            Return Name
        End Function
    End Class

    ' Patient class
    Public Class PatientCls
        Public Property PatientID As Integer
        Public Property PatientName As String
    End Class

    ' Event arguments for Patient changes
    Public Class PatientIndexChangedEvent
        Inherits EventArgs
        Public Property PatientID As Integer
        Public Property PatientName As String
        Public Sub New(PatientID As Integer, PatientName As String)
            Me.PatientID = PatientID
            Me.PatientName = PatientName
        End Sub
    End Class
End Class
