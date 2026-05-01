Imports System.ComponentModel
Imports System.Data.SqlClient
Imports Dapper
Imports DevExpress.XtraEditors

Public Class LabMsgDetailChoiceCombo
    Implements INotifyPropertyChanged

    Private connectionString As String = DentistXDATA.GetEffectiveConnectionString

    ' Define events for property changes
    Public Event LabMsgDetailChoiceValueChanged(ByVal sender As Object, ByVal e As LabMsgDetailChoiceIndexChangedEvent)

    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()
       BindLabMsgDetailChoices()
    End Sub
    ' Define properties and backing fields
    Private _selectedLabMsgDetailChoiceID As Integer
    Public Property LabMsgDetailChoiceID As Integer
        Get
            Return _selectedLabMsgDetailChoiceID
        End Get
        Set(value As Integer)
            If Not Equals(_selectedLabMsgDetailChoiceID, value) Then
                _selectedLabMsgDetailChoiceID = value
                OnPropertyChanged(NameOf(LabMsgDetailChoiceID))
                ' Update the combo box based on the new value
                UpdateLabMsgDetailChoiceIDComboBoxSelection(value)
            End If
        End Set
    End Property

    Private _selectedLabMsgSubjectID As Integer
    Public Property LabMsgSubjectID As Integer
        Get
            Return _selectedLabMsgSubjectID
        End Get
        Set(value As Integer)
            If Not Equals(_selectedLabMsgSubjectID, value) Then
                _selectedLabMsgSubjectID = value
                OnPropertyChanged(NameOf(LabMsgSubjectID))
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
    Private Sub BindLabMsgDetailChoices()
        Dim LabMsgDetailChoices As List(Of LabMsgDetailChoiceCls) = GetLabMsgDetailChoices()

        CboLabMsgDetailChoice.Properties.Items.Clear()
        For Each LabMsgDetailChoice As LabMsgDetailChoiceCls In LabMsgDetailChoices
            CboLabMsgDetailChoice.Properties.Items.Add(New ComboBoxItem With { .ID = LabMsgDetailChoice.LabMsgDetailChoiceID, .Name = LabMsgDetailChoice.LabMsgSubjectID})
        Next
        SetTextEditStyle(CboLabMsgDetailChoice)
    End Sub

    ' Update ComboBox selection based on property values
    Private Sub UpdateLabMsgDetailChoiceIDComboBoxSelection(LabMsgDetailChoiceID As Integer)
        For Each item As ComboBoxItem In CboLabMsgDetailChoice.Properties.Items
            If item.ID = LabMsgDetailChoiceID Then
                CboLabMsgDetailChoice.SelectedItem = item
                Exit For
            End If
        Next
    End Sub

    ' ComboBox selection change handlers
    Private Sub CboLabMsgDetailChoice_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CboLabMsgDetailChoice.SelectedIndexChanged
        Dim selectedItem As ComboBoxItem = DirectCast(CboLabMsgDetailChoice.SelectedItem, ComboBoxItem)
        If selectedItem IsNot Nothing Then
            LabMsgDetailChoiceID = selectedItem.ID
            LabMsgSubjectID = selectedItem.Name

            RaiseEvent LabMsgDetailChoiceValueChanged(sender, New LabMsgDetailChoiceIndexChangedEvent(LabMsgDetailChoiceID, LabMsgSubjectID))
        End If
    End Sub

    ' Event handlers for buttons
    Private Sub btnAddLabMsgDetailChoice_Click(sender As Object, e As EventArgs) Handles BtnAddLabMsgDetailChoice.Click
        FrmLabMsgDetailChoice.ShowDialog()
        BindLabMsgDetailChoices()
    End Sub

    ' Methods to fetch data from the database (Dapper)
    Public Function GetLabMsgDetailChoiceTable() As DataTable
        Dim query As String = "SELECT LabMsgDetailChoiceID, LabMsgSubjectID FROM LabMsgDetailChoice;"
        Using Conn As New SqlConnection(connectionString)
            Dim dt As New DataTable()
            Using reader = Conn.ExecuteReader(query)
                dt.Load(reader)
            End Using
            Return dt
        End Using
    End Function

    Private Function GetLabMsgDetailChoices() As List(Of LabMsgDetailChoiceCls)
        Dim dt As DataTable = GetLabMsgDetailChoiceTable()
        Dim LabMsgDetailChoices As New List(Of LabMsgDetailChoiceCls)()
        For Each row As DataRow In dt.Rows
            LabMsgDetailChoices.Add(New LabMsgDetailChoiceCls With { .LabMsgDetailChoiceID = row("LabMsgDetailChoiceID"), .LabMsgSubjectID = row("LabMsgSubjectID")})
        Next
        Return LabMsgDetailChoices
    End Function

    ' Get LabMsgDetailChoice ID by LabMsgSubjectID Name (Dapper)
    Public Function GetLabMsgDetailChoiceIDByLabMsgSubjectID(LabMsgSubjectID As String) As Integer
        Dim query As String = "SELECT LabMsgDetailChoiceID FROM LabMsgDetailChoice WHERE LabMsgSubjectID = @LabMsgSubjectID"
        Using Conn As New SqlConnection(connectionString)
            Dim result As Integer? = Conn.QuerySingleOrDefault(Of Integer?)(query, New With { .LabMsgSubjectID = LabMsgSubjectID })
            Return If(result.HasValue, result.Value, -1)
        End Using
    End Function

    ' Get LabMsgDetailChoice Name by LabMsgDetailChoiceID ID (Dapper)
    Public Function GetLabMsgSubjectIDByLabMsgDetailChoiceID(LabMsgDetailChoiceID As Integer ) As String
        Dim query As String = "SELECT LabMsgSubjectID FROM LabMsgDetailChoice WHERE LabMsgDetailChoiceID = @LabMsgDetailChoiceID"
        Using Conn As New SqlConnection(connectionString)
            Dim result As String = Conn.QuerySingleOrDefault(Of String)(query, New With { .LabMsgDetailChoiceID = LabMsgDetailChoiceID })
            Return If(result, "-1")
        End Using
    End Function

    ' Set selected LabMsgSubjectID by ID
    Public Sub SetLabMsgSubjectIDByLabMsgDetailChoiceID(LabMsgDetailChoiceID As Integer)
        Me.LabMsgDetailChoiceID = LabMsgDetailChoiceID
        UpdateLabMsgDetailChoiceIDComboBoxSelection(LabMsgDetailChoiceID)
    End Sub

    ' Set selected LabMsgDetailChoice by Name
    Public Sub SetLabMsgDetailChoiceIDByLabMsgSubjectID(LabMsgSubjectID As String)
        Me.LabMsgDetailChoiceID = GetLabMsgDetailChoiceIDByLabMsgSubjectID(LabMsgSubjectID)
        UpdateLabMsgDetailChoiceIDComboBoxSelection(LabMsgDetailChoiceID)
    End Sub

    Public Sub RefreshLabMsgDetailChoice
        BindLabMsgDetailChoices()
    End Sub
    Public Sub ClearSelection
        CboLabMsgDetailChoice.SelectedIndex = -1
        Me.LabMsgDetailChoiceID = 0
        Me.LabMsgSubjectID = String.Empty
    End Sub
    ' ComboBoxItem class
    Public Class ComboBoxItem
        Public Property ID As Integer
        Public Property Name As String

        Public Overrides Function ToString() As String
            Return Name
        End Function
    End Class

    ' LabMsgDetailChoice class
    Public Class LabMsgDetailChoiceCls
        Public Property LabMsgDetailChoiceID As Integer
        Public Property LabMsgSubjectID As String
    End Class

    ' Event arguments for LabMsgDetailChoice changes
    Public Class LabMsgDetailChoiceIndexChangedEvent
        Inherits EventArgs
        Public Property LabMsgDetailChoiceID As Integer
        Public Property LabMsgSubjectID As String
        Public Sub New(_LabMsgDetailChoiceID As Integer, _LabMsgSubjectID As String)
            Me.LabMsgDetailChoiceID = _LabMsgDetailChoiceID
            Me.LabMsgSubjectID = _LabMsgSubjectID
        End Sub
    End Class
End Class
