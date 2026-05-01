Imports System.ComponentModel
Imports System.Data.SqlClient
Imports Dapper
Imports DevExpress.XtraEditors

Public Class LabMsgSubjectCombo
    Implements INotifyPropertyChanged

    Private connectionString As String = DentistXDATA.GetEffectiveConnectionString

    ' Define events for property changes
    Public Event LabMsgSubjectValueChanged(ByVal sender As Object, ByVal e As LabMsgSubjectIndexChangedEvent)

    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()
       BindLabMsgSubjects()
    End Sub
    ' Define properties and backing fields
    Private _selectedLabMsgSubjectID As Integer
    Public Property LabMsgSubjectID As Integer
        Get
            Return _selectedLabMsgSubjectID
        End Get
        Set(value As Integer)
            If Not Equals(_selectedLabMsgSubjectID, value) Then
                _selectedLabMsgSubjectID = value
                OnPropertyChanged(NameOf(LabMsgSubjectID))
                ' Update the combo box based on the new value
                UpdateLabMsgSubjectIDComboBoxSelection(value)
            End If
        End Set
    End Property

    Private _selectedSubjectName As String
    Public Property SubjectName As String
        Get
            Return _selectedSubjectName
        End Get
        Set(value As String)
            If Not Equals(_selectedSubjectName, value) Then
                _selectedSubjectName = value
                OnPropertyChanged(NameOf(SubjectName))
            End If
        End Set
    End Property


    ' Utility methods for ComboBoxes
    Private Sub SetTextEditStyle(ByVal comboBox As ComboBoxEdit)
        comboBox.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    End Sub

    ' PropertyChanged event implementation
    Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged
    Protected Overridable Sub OnPropertyChanged(ByVal propertyName As String)
        RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(propertyName))
    End Sub
    ' Bind the ComboBoxes to the appropriate data
    Private Sub BindLabMsgSubjects()
        Dim LabMsgSubjects As List(Of LabMsgSubjectCls) = GetLabMsgSubjects()

        CboLabMsgSubject.Properties.Items.Clear()
        For Each LabMsgSubject As LabMsgSubjectCls In LabMsgSubjects
            CboLabMsgSubject.Properties.Items.Add(New ComboBoxItem With { .ID = LabMsgSubject.LabMsgSubjectID, .Name = LabMsgSubject.SubjectName})
        Next
        SetTextEditStyle(CboLabMsgSubject)
    End Sub

    ' Update ComboBox selection based on property values
    Private Sub UpdateLabMsgSubjectIDComboBoxSelection(LabMsgSubjectID As Integer)
        For Each item As ComboBoxItem In CboLabMsgSubject.Properties.Items
            If item.ID = LabMsgSubjectID Then
                CboLabMsgSubject.SelectedItem = item
                Exit For
            End If
        Next
    End Sub

    ' ComboBox selection change handlers
    Private Sub CboLabMsgSubject_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CboLabMsgSubject.SelectedIndexChanged
        Dim selectedItem As ComboBoxItem = DirectCast(CboLabMsgSubject.SelectedItem, ComboBoxItem)
        If selectedItem IsNot Nothing Then
            LabMsgSubjectID = selectedItem.ID
            SubjectName = selectedItem.Name

            RaiseEvent LabMsgSubjectValueChanged(sender, New LabMsgSubjectIndexChangedEvent(LabMsgSubjectID, SubjectName))
        End If
    End Sub

    ' Event handlers for buttons
    Private Sub btnAddLabMsgSubject_Click(sender As Object, e As EventArgs) Handles BtnAddLabMsgSubject.Click
        FrmLabMsgSubject.ShowDialog()
        BindLabMsgSubjects()
    End Sub

    ' Methods to fetch data from the database (Dapper)
    Public Function GetLabMsgSubjectTable() As DataTable
        Dim query As String = "SELECT LabMsgSubjectID, SubjectName FROM LabMsgSubject;"
        Using Conn As New SqlConnection(connectionString)
            Dim dt As New DataTable()
            Using reader = Conn.ExecuteReader(query)
                dt.Load(reader)
            End Using
            Return dt
        End Using
    End Function

    Private Function GetLabMsgSubjects() As List(Of LabMsgSubjectCls)
        Dim dt As DataTable = GetLabMsgSubjectTable()
        Dim LabMsgSubjects As New List(Of LabMsgSubjectCls)()
        For Each row As DataRow In dt.Rows
            LabMsgSubjects.Add(New LabMsgSubjectCls With { .LabMsgSubjectID = row("LabMsgSubjectID"), .SubjectName = row("SubjectName")})
        Next
        Return LabMsgSubjects
    End Function

    ' Get LabMsgSubject ID by SubjectName Name (Dapper)
    Public Function GetLabMsgSubjectIDBySubjectName(SubjectName As String) As Integer
        Dim query As String = "SELECT LabMsgSubjectID FROM LabMsgSubject WHERE SubjectName = @SubjectName"
        Using Conn As New SqlConnection(connectionString)
            Dim result As Integer? = Conn.QuerySingleOrDefault(Of Integer?)(query, New With { .SubjectName = SubjectName })
            Return If(result.HasValue, result.Value, -1)
        End Using
    End Function

    ' Get LabMsgSubject Name by LabMsgSubjectID ID (Dapper)
    Public Function GetSubjectNameByLabMsgSubjectID(LabMsgSubjectID As Integer ) As String
        Dim query As String = "SELECT SubjectName FROM LabMsgSubject WHERE LabMsgSubjectID = @LabMsgSubjectID"
        Using Conn As New SqlConnection(connectionString)
            Dim result As String = Conn.QuerySingleOrDefault(Of String)(query, New With { .LabMsgSubjectID = LabMsgSubjectID })
            Return If(result, "-1")
        End Using
    End Function

    ' Set selected SubjectName by ID
    Public Sub SetSubjectNameByLabMsgSubjectID(LabMsgSubjectID As Integer)
        Me.LabMsgSubjectID = LabMsgSubjectID
        UpdateLabMsgSubjectIDComboBoxSelection(LabMsgSubjectID)
    End Sub

    ' Set selected LabMsgSubject by Name
    Public Sub SetLabMsgSubjectIDBySubjectName(SubjectName As String)
        Me.LabMsgSubjectID = GetLabMsgSubjectIDBySubjectName(SubjectName)
        UpdateLabMsgSubjectIDComboBoxSelection(LabMsgSubjectID)
    End Sub

    Public Sub RefreshLabMsgSubject
        BindLabMsgSubjects()
    End Sub
    Public Sub ClearSelection
        CboLabMsgSubject.SelectedIndex = -1
        Me.LabMsgSubjectID = 0
        Me.SubjectName = String.Empty
    End Sub
    ' ComboBoxItem class
    Public Class ComboBoxItem
        Public Property ID As Integer
        Public Property Name As String

        Public Overrides Function ToString() As String
            Return Name
        End Function
    End Class

    ' LabMsgSubject class
    Public Class LabMsgSubjectCls
        Public Property LabMsgSubjectID As Integer
        Public Property SubjectName As String
    End Class

    ' Event arguments for LabMsgSubject changes
    Public Class LabMsgSubjectIndexChangedEvent
        Inherits EventArgs
        Public Property LabMsgSubjectID As Integer
        Public Property SubjectName As String
        Public Sub New(_LabMsgSubjectID As Integer, _SubjectName As String)
            Me.LabMsgSubjectID = _LabMsgSubjectID
            Me.SubjectName = _SubjectName
        End Sub
    End Class
End Class
