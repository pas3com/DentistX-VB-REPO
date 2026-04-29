Imports System.ComponentModel
Imports System.Data.SqlClient
Imports DevExpress.XtraEditors

Public Class PermissionsCombo
    Implements INotifyPropertyChanged

    Private connectionString As String = DentistXDATA.GetConnection.ConnectionString

    ' Define events for property changes
    Public Event PermissionsValueChanged(ByVal sender As Object, ByVal e As PermissionsIndexChangedEvent)
    ' Define properties and backing fields
    Private _selectedPermID As Integer
    Public Property PermID As Integer
        Get
            Return _selectedPermID
        End Get
        Set(value As Integer)
            If Not Equals(_selectedPermID, value) Then
                _selectedPermID = value
                OnPropertyChanged(NameOf(PermID))
                ' Update the combo box based on the new value
                UpdatePermIDComboBoxSelection(value)
            End If
        End Set
    End Property

    Private _selectedPermName As String
    Public Property PermName As String
        Get
            Return _selectedPermName
        End Get
        Set(value As String)
            If Not Equals(_selectedPermName, value) Then
                _selectedPermName = value
                OnPropertyChanged(NameOf(PermName))
            End If
        End Set
    End Property

    Private _btnAddVisible As Boolean = True

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

    <Browsable(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)>
    Public Property BtnSearchVisible As Boolean
        Get
            Return False
        End Get
        Set(value As Boolean)
        End Set
    End Property

    Private Sub ApplyToolbarLayout()
        ComboToolbarLayoutHelper.ApplyPanelToolbar(PanelControl1, Nothing, BtnAddPermissions, False, _btnAddVisible, Me, ComboToolbarLayoutHelper.CompactToolbarSlotWidth)
    End Sub

    ' Bind the ComboBoxes to the appropriate data
    Private Sub BindPermissionss()
        Dim Permissionss As List(Of PermissionsCls) = GetPermissionss()

        CboPermissions.Properties.Items.Clear()
        For Each Permissions As PermissionsCls In Permissionss
            CboPermissions.Properties.Items.Add(New ComboBoxItem With {.ID = Permissions.PermID, .Name = Permissions.PermName})
        Next
        SetTextEditStyle(CboPermissions)
    End Sub

    ' Update ComboBox selection based on property values
    Private Sub UpdatePermIDComboBoxSelection(PermID As Integer)

        ' Temporarily remove the event handler to prevent recursion
        RemoveHandler CboPermissions.SelectedIndexChanged, AddressOf CboPermissions_SelectedIndexChanged
        Try
            CboPermissions.ResetText()
            For Each item As ComboBoxItem In CboPermissions.Properties.Items
                If item.ID = PermID Then
                    CboPermissions.SelectedItem = item
                    Exit For
                End If
            Next
        Finally
            ' Re-add the event handler
            AddHandler CboPermissions.SelectedIndexChanged, AddressOf CboPermissions_SelectedIndexChanged
        End Try

        ' Raise the event
        RaiseEvent PermissionsValueChanged(Me, New PermissionsIndexChangedEvent(PermID, PermName))
    End Sub

    ' ComboBox selection change handlers
    Private Sub CboPermissions_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CboPermissions.SelectedIndexChanged
        Dim selectedItem As ComboBoxItem = DirectCast(CboPermissions.SelectedItem, ComboBoxItem)
        If selectedItem IsNot Nothing Then
            PermID = selectedItem.ID
            PermName = selectedItem.Name

            RaiseEvent PermissionsValueChanged(sender, New PermissionsIndexChangedEvent(PermID, PermName))
        End If
    End Sub

    ' Event handlers for buttons
    Private Sub btnAddPermissions_Click(sender As Object, e As EventArgs) Handles BtnAddPermissions.Click
        FrmPermissions.ShowDialog()
        BindPermissionss()
    End Sub

    ' Methods to fetch data from the database
    Public Function GetPermissionsTable() As DataTable
        Dim query As String = "SELECT PermID, PermName FROM Permissions;"

        Using Conn As New SqlConnection(connectionString)
            Using cmd As New SqlCommand(query, Conn)
                Dim dt As New DataTable()
                Conn.Open()
                Using reader As SqlDataReader = cmd.ExecuteReader()
                    dt.Load(reader)
                End Using
                Return dt
            End Using
        End Using
    End Function

    Private Function GetPermissionss() As List(Of PermissionsCls)
        Dim dt As DataTable = GetPermissionsTable()
        Dim Permissionss As New List(Of PermissionsCls)()
        For Each row As DataRow In dt.Rows
            Permissionss.Add(New PermissionsCls With {.PermID = row("PermID"), .PermName = row("PermName")})
        Next
        Return Permissionss
    End Function


    ' Utility methods for ComboBoxes
    Private Sub SetTextEditStyle(ByVal comboBox As ComboBoxEdit)
        comboBox.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
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
        BindPermissionss()
    End Sub

    ' Method to handle Permissions value changed event
    Private Sub HandlePermissionsValueChanged(ByVal sender As Object, ByVal e As PermissionsIndexChangedEvent) Handles Me.PermissionsValueChanged
        ' Perform necessary actions when {tableName} value changes
        Me.PermID = e.PermID
        Me.PermName = e.PermName
    End Sub

    ' Get Permissions ID by PermName Name
    Public Function GetPermNameByID(PermID As Integer) As String


        Dim query As String = "SELECT PermName FROM Permissions WHERE PermID = @PermID"

        Using Conn As New SqlConnection(connectionString)
            Using cmd As New SqlCommand(query, Conn)
                cmd.Parameters.Add("@PermID", SqlDbType.Int).Value = PermID
                Conn.Open()

                Using reader As SqlDataReader = cmd.ExecuteReader()
                    If reader.Read() Then
                        If Not reader.IsDBNull(0) Then
                            Return reader.GetString(0)
                        End If
                    End If
                End Using
            End Using
        End Using

        ' Return a default value if not found
        Return String.Empty
    End Function

    ' Get Permissions Name by PermID ID GetPermID
    Public Function GetPermIDByName(PermName As String) As Integer
        Dim query As String = "SELECT PermID FROM Permissions WHERE PermName = @PermName"

        Using Conn As New SqlConnection(connectionString)
            Using cmd As New SqlCommand(query, Conn)
                cmd.Parameters.Add("@PermName", SqlDbType.NVarChar).Value = PermName
                Conn.Open()

                Using reader As SqlDataReader = cmd.ExecuteReader()
                    If reader.Read() Then
                        If Not reader.IsDBNull(0) Then
                            Return reader.GetInt32(0)
                        End If
                    End If
                End Using
            End Using
        End Using

        ' Return a default value if not found
        Return -1

    End Function

    ' Set selected PermName by ID
    Public Sub SetSelectedPermName(PermName As String)
        Dim _PermID As Integer = GetPermIDByName(PermName)
        If _PermID <> -1 Then
            Me.PermID = _PermID
            Me.PermName = PermName
            UpdatePermIDComboBoxSelection(PermID)
        End If
    End Sub

    ' Set selected Permissions by Name 
    Public Sub SetSelectedPermID(PermID As Integer)
        ' Get the ID from database
        Dim _PermName As String = GetPermNameByID(PermID)
        If Not String.IsNullOrEmpty(_PermName) Then
            Me.PermID = PermID
            Me.PermName = _PermName
            UpdatePermIDComboBoxSelection(PermID)
        End If
    End Sub

    ' ComboBoxItem class
    Public Class ComboBoxItem
        Public Property ID As Integer
        Public Property Name As String

        Public Overrides Function ToString() As String
            Return Name
        End Function
    End Class

    ' Permissions class
    Public Class PermissionsCls
        Public Property PermID As Integer
        Public Property PermName As String
    End Class

    ' Event arguments for Permissions changes
    Public Class PermissionsIndexChangedEvent
        Inherits EventArgs
        Public Property PermID As Integer
        Public Property PermName As String
        Public Sub New(PermID As Integer, PermName As String)
            Me.PermID = PermID
            Me.PermName = PermName
        End Sub
    End Class
End Class
