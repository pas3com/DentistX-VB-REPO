Imports System.ComponentModel
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports System.Linq
Imports System.Windows.Forms
Imports Dapper
Imports DevExpress.Utils
Imports DevExpress.XtraEditors

Public Class ContactsCombo
    Implements INotifyPropertyChanged

    Private ReadOnly _connectionString As String = DentistXDATA.GetConnection.ConnectionString

    Public Event ContactsValueChanged(ByVal sender As Object, ByVal e As ContactsIndexChangedEvent)

    Private _selectedContactID As Integer
    Public Property ContactID As Integer
        Get
            Return _selectedContactID
        End Get
        Set(value As Integer)
            If Not Equals(_selectedContactID, value) Then
                _selectedContactID = value
                OnPropertyChanged(NameOf(ContactID))
                UpdateContactIDComboBoxSelection(value)
            End If
        End Set
    End Property

    Private _selectedName As String
    Public Property ContName As String
        Get
            Return _selectedName
        End Get
        Set(value As String)
            If Not Equals(_selectedName, value) Then
                _selectedName = value
                OnPropertyChanged(NameOf(ContName))
            End If
        End Set
    End Property

    Private _allRows As New List(Of ContactsCls)()
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
        ComboToolbarLayoutHelper.ApplyPanelToolbar(PanelControl1, btnSerach, BtnAddContacts, _btnSearchVisible, _btnAddVisible, Me)
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
        If CboContacts Is Nothing OrElse _applyingFilter Then Return
        _applyingFilter = True
        Dim searchTyped As String = If(txtSearch IsNot Nothing, txtSearch.Text, String.Empty).Trim()
        Dim external As String = _nameFilter.Trim()
        Dim filtered = _allRows.Where(Function(c) MatchesFilters(c.CName, searchTyped, external)).ToList()
        Dim preserveId As Integer = _selectedContactID

        Try
            RemoveHandler CboContacts.SelectedIndexChanged, AddressOf CboContacts_SelectedIndexChanged
            CboContacts.Properties.Items.Clear()
            For Each row As ContactsCls In filtered
                CboContacts.Properties.Items.Add(New ComboBoxItem With {.ID = row.ContactID, .ItemName = row.CName})
            Next

            Dim keepItem As ComboBoxItem = Nothing
            If preserveId > 0 Then
                For Each item As ComboBoxItem In CboContacts.Properties.Items
                    If item.ID = preserveId Then
                        keepItem = item
                        Exit For
                    End If
                Next
            End If

            If keepItem IsNot Nothing Then
                CboContacts.SelectedItem = keepItem
                _selectedContactID = keepItem.ID
                _selectedName = keepItem.ItemName
            ElseIf String.IsNullOrWhiteSpace(searchTyped) AndAlso String.IsNullOrWhiteSpace(external) AndAlso CboContacts.Properties.Items.Count > 0 Then
                CboContacts.SelectedIndex = 0
                Dim first As ComboBoxItem = TryCast(CboContacts.SelectedItem, ComboBoxItem)
                If first IsNot Nothing Then
                    _selectedContactID = first.ID
                    _selectedName = first.ItemName
                End If
            Else
                CboContacts.EditValue = Nothing
                _selectedContactID = 0
                _selectedName = Nothing
            End If
            SetTextEditStyle(CboContacts)
        Finally
            AddHandler CboContacts.SelectedIndexChanged, AddressOf CboContacts_SelectedIndexChanged
            _applyingFilter = False
        End Try
    End Sub

    Private Sub BindContacts()
        Const sql As String = "SELECT ContactID, CName FROM Contacts;"
        Using conn As New SqlConnection(_connectionString)
            conn.Open()
            _allRows = conn.Query(Of ContactsCls)(sql).ToList()
        End Using
        ApplyListFilter()
    End Sub

    Private Sub UpdateContactIDComboBoxSelection(contactId As Integer)
        ClearSearchBox()
        If contactId <= 0 Then
            CboContacts.EditValue = Nothing
            _selectedName = Nothing
            ApplyListFilter()
            Return
        End If
        Dim row = _allRows.FirstOrDefault(Function(c) c.ContactID = contactId)
        If row Is Nothing Then
            CboContacts.EditValue = Nothing
            _selectedName = Nothing
            ApplyListFilter()
            Return
        End If
        ApplyListFilter()
        For Each item As ComboBoxItem In CboContacts.Properties.Items
            If item.ID = contactId Then
                CboContacts.SelectedItem = item
                _selectedName = item.ItemName
                Return
            End If
        Next
        CboContacts.EditValue = Nothing
        _selectedName = Nothing
    End Sub

    Private Sub CboContacts_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CboContacts.SelectedIndexChanged
        If _applyingFilter Then Return
        Dim selectedItem As ComboBoxItem = TryCast(CboContacts.SelectedItem, ComboBoxItem)
        If selectedItem IsNot Nothing Then
            ContactID = selectedItem.ID
            ContName = selectedItem.ItemName
            RaiseEvent ContactsValueChanged(sender, New ContactsIndexChangedEvent(ContactID, ContName))
        End If
    End Sub

    Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged
        If _suppressSearchTextChanged OrElse _applyingFilter Then Return
        ApplyListFilter()
    End Sub

    Private Sub Flyout1_Showing(sender As Object, e As FlyoutPanelEventArgs) Handles Flyout1.Showing
        ComboFlyoutSearchHelper.ApplyManualPositionBelowPanel(Flyout1, PanelControl2, CboContacts)
    End Sub

    Private Sub btnSerach_Click(sender As Object, e As EventArgs) Handles btnSerach.Click
        ComboFlyoutSearchHelper.ShowFlyoutSearchDeferred(Flyout1, PanelControl2, CboContacts, txtSearch, Me)
    End Sub

    Private Sub BtnAddContacts_Click(sender As Object, e As EventArgs) Handles BtnAddContacts.Click
        FrmContacts.ShowDialog()
        BindContacts()
    End Sub

    Public Function GetContactsTable() As DataTable
        Dim dt As New DataTable()
        dt.Columns.Add("ContactID", GetType(Integer))
        dt.Columns.Add("CName", GetType(String))
        For Each c In _allRows
            dt.Rows.Add(c.ContactID, If(c.CName, CType(String.Empty, Object)))
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
        BindContacts()
    End Sub

    Private Sub HandleContactsValueChanged(sender As Object, e As ContactsIndexChangedEvent) Handles Me.ContactsValueChanged
        Me.ContactID = e.ContactID
        Me.ContName = e.CName
    End Sub

    Public Function GetContactID(cName As String) As Integer
        If String.IsNullOrWhiteSpace(cName) Then Return -1
        Const sql As String = "SELECT ContactID FROM Contacts WHERE CName = @CName"
        Using conn As New SqlConnection(_connectionString)
            Dim r As Integer? = conn.QuerySingleOrDefault(Of Integer?)(sql, New With {.CName = cName})
            Return If(r.HasValue, r.Value, -1)
        End Using
    End Function

    Public Function GetName(contactId As Integer) As String
        Const sql As String = "SELECT CName FROM Contacts WHERE ContactID = @ContactID"
        Using conn As New SqlConnection(_connectionString)
            Return If(conn.QuerySingleOrDefault(Of String)(sql, New With {.ContactID = contactId}), String.Empty)
        End Using
    End Function

    Public Sub SetSelectedName(contactId As Integer)
        Dim n As String = GetName(contactId)
        If Not String.IsNullOrEmpty(n) Then
            Me.ContactID = contactId
            Me.ContName = n
            UpdateContactIDComboBoxSelection(contactId)
        End If
    End Sub

    Public Sub SetSelectedContactID(cName As String)
        Dim id As Integer = GetContactID(cName)
        If id <> -1 Then
            Me.ContactID = id
            Me.ContName = cName
            UpdateContactIDComboBoxSelection(id)
        End If
    End Sub

    Public Class ComboBoxItem
        Public Property ID As Integer
        Public Property ItemName As String
        Public Overrides Function ToString() As String
            Return ItemName
        End Function
    End Class

    Public Class ContactsCls
        Public Property ContactID As Integer
        Public Property CName As String
    End Class

    Public Class ContactsIndexChangedEvent
        Inherits EventArgs
        Public Property ContactID As Integer
        Public Property CName As String
        Public Sub New(contactId As Integer, cName As String)
            Me.ContactID = contactId
            Me.CName = cName
        End Sub
    End Class
End Class
