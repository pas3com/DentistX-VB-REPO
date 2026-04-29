Imports System.Data.SqlClient
Imports Dapper

Public Class FrmEditVisit

    Public Sub New(ByVal visit As DayControl.VisitSummary)
        ' This call is required by the designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        VisitDetID = visit.VisitDetID
        Me.Icon = AppIcon
    End Sub

    Public Sub New(dateValue As Date)

        ' This call is required by the designer.
        InitializeComponent()
        Me.Icon = AppIcon
        ' Add any initialization after the InitializeComponent() call.
        VisitDetID = 0
        Me.btnDelete.Visible = False
        Me.VisDateTimeEdit.DateTime = dateValue
    End Sub
    Private VisitBindingSource As New BindingSource()
    Private connectionString As String = DentistXDATA.GetConnection.ConnectionString
    Public VisitDetID As Integer? = Nothing

    Private Sub FrmEditVisit_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadLookups()
        If VisitDetID.HasValue Then
            LoadVisit(VisitDetID.Value)
        Else
            VisitBindingSource.DataSource = New Visits()
        End If
    End Sub

    Private Sub LoadLookups()
        Using con As New SqlConnection(connectionString)
            Dim patients = con.Query(Of Patient)("SELECT PatientID, PatientName FROM Patient").ToList()
            Dim types = con.Query(Of VisitType)("SELECT VtID, VisitType FROM VisitTypes").ToList()
            cboPatient.Properties.Items.Clear()
            For Each p In patients
                cboPatient.Properties.Items.Add(New ComboBoxItem With {.ID = p.PatientID, .Name = p.PatientName})
            Next
            cboVisitType.Properties.Items.Clear()
            For Each t In types
                cboVisitType.Properties.Items.Add(New ComboBoxItem With {.ID = t.VtID, .Name = t.VisitType})
            Next
        End Using
    End Sub

    Private Sub LoadVisit(id As Integer)
        Using con As New SqlConnection(connectionString)
            Dim visit = con.QueryFirstOrDefault(Of Visits)("SELECT * FROM Visits WHERE VisitDetID = @id", New With {.id = id})
            If visit IsNot Nothing Then
                VisitBindingSource.DataSource = visit

                ' Match by ID from the items already added
                cboPatient.SelectedItem = cboPatient.Properties.Items.OfType(Of ComboBoxItem)().
                FirstOrDefault(Function(p) p.ID = visit.PatientID)

                cboVisitType.SelectedItem = cboVisitType.Properties.Items.OfType(Of ComboBoxItem)().
                FirstOrDefault(Function(vt) vt.ID = visit.VtID)
                VisDateTimeEdit.DateTime = visit.VisDateTime
                VisTimeEdit.EditValue = visit.VisTime
                VisTimeEndEdit.EditValue = visit.VisTimeEnd
                txtDetail.Text = visit.VisDetail
                txtNotes.Text = visit.VisNotes
            End If
        End Using
    End Sub


    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        VisitBindingSource.EndEdit()
        'Dim visit = CType(VisitBindingSource.Current, Visits)
        Dim visit = New Visits With {.VisitDetID = VisitDetID,
            .PatientID = CType(cboPatient.SelectedItem, ComboBoxItem).ID,
            .PatientName = CType(cboPatient.SelectedItem, ComboBoxItem).Name,
            .VtID = CType(cboVisitType.SelectedItem, ComboBoxItem).ID,
            .VisDateTime = VisDateTimeEdit.DateTime,
            .VisitDay = VisDateTimeEdit.DateOnly.ToShortDateString,
            .VisTime = VisTimeEdit.Time.ToShortTimeString,
            .VisTimeEnd = VisTimeEndEdit.Time.ToShortTimeString,
            .VisDetail = txtDetail.Text,
            .VisNotes = txtNotes.Text}
        Using con As New SqlConnection(connectionString)
            If visit.VisitDetID = 0 Then
                Dim insertQuery = "
                    INSERT INTO Visits (PatientID, VtID, VisitDay, VisTime, VisTimeEnd, PatientName, VisDetail, VisNotes, VisDateTime)
                    VALUES (@PatientID, @VtID, @VisitDay, @VisTime, @VisTimeEnd, @PatientName, @VisDetail, @VisNotes, @VisDateTime)
                    SELECT CAST(SCOPE_IDENTITY() AS INT)
                "
                visit.VisitDetID = con.ExecuteScalar(Of Integer)(insertQuery, visit)
            Else
                Dim updateQuery = "
                    UPDATE Visits SET 
                        PatientID = @PatientID, VtID = @VtID, VisitDay = @VisitDay,
                        VisTime = @VisTime, VisTimeEnd = @VisTimeEnd,
                        PatientName = @PatientName, VisDetail = @VisDetail,
                        VisNotes = @VisNotes, VisDateTime = @VisDateTime
                    WHERE VisitDetID = @VisitDetID
                "
                con.Execute(updateQuery, visit)
            End If
        End Using

        Me.DialogResult = DialogResult.OK
        Me.Close()
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.DialogResult = DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        Dim affectedRows As Integer
        Dim deleteStatement As String =
            "DELETE FROM [Visits] 
			WHERE VisitDetID = @VisitDetID"
        Using connection As SqlConnection = DentistXDATA.GetConnection()
            connection.Open()
            affectedRows = connection.Execute(deleteStatement, New With {.VisitDetID = VisitDetID})
            If affectedRows > 0 Then
                MsgBox("Deleted Successfuly")
            End If
        End Using
    End Sub
End Class

'Public Class Visit
'    Public Property VisitDetID As Integer
'    Public Property PatientID As Integer
'    Public Property VtID As Integer?
'    Public Property VisitDay As String
'    Public Property VisTime As String
'    Public Property VisTimeEnd As String
'    Public Property PatientName As String
'    Public Property VisDetail As String
'    Public Property VisNotes As String
'    Public Property VisDateTime As Date?
'End Class

'Public Class Patient
'    Public Property PatientID As Integer
'    Public Property PatientName As String
'End Class

'Public Class VisitType
'    Public Property VtID As Integer
'    Public Property VtName As String
'End Class
