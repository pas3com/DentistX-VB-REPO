Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections
Imports System.Configuration
Imports Dapper

	Public Class Patient_NotesDATA

		Private ConnectionString As String=DentistXDATA.GetConnection.connectionString



		Public Function SelectAll() As IEnumerable(Of Patient_Notes)
			Using conn As New SqlConnection(ConnectionString)
			    conn.Open()
			    Return conn.Query(Of Patient_Notes)("SELECT * FROM Patient_Notes")
			End Using
		End Function
		

		Public Function Select_Record(ByVal clsPatient_Notes As Patient_Notes) As Patient_Notes
			Using conn As New SqlConnection(ConnectionString)
			    Dim sql As String = "Select * FROM Patient_Notes WHERE NoteID = @NoteID"
			    Return conn.QuerySingleOrDefault(Of Patient_Notes)(sql, New With { .NoteID = clsPatient_Notes.NoteID })
			End Using
		End Function

		Public Function Add(ByVal clsPatient_Notes As Patient_Notes) As Boolean
			Dim RowsAffected As Integer=0
			Using conn As New SqlConnection(ConnectionString)
			    Dim sql As String = "INSERT INTO Patient_Notes (PatientID, NoteDate, Note) VALUES (@PatientID, @NoteDate, @Note)" 
			    RowsAffected = conn.Execute(sql, New With { .PatientID =  clsPatient_Notes.PatientID, .NoteDate =  clsPatient_Notes.NoteDate, .Note =  clsPatient_Notes.Note })
			    Return RowsAffected > 0
			End Using
		End Function

		Public Function Update(oldPatient_Notes As Patient_Notes, newPatient_Notes As Patient_Notes) As Boolean
			Using conn As New SqlConnection(ConnectionString)
			    Dim parameters = New With { 
					.NewPatientID = newPatient_Notes.PatientID, .OldPatientID = oldPatient_Notes.PatientID, .NewNoteDate = newPatient_Notes.NoteDate, .OldNoteDate = oldPatient_Notes.NoteDate, .NewNote = newPatient_Notes.Note, .OldNote = oldPatient_Notes.Note
										  }
			    Dim affectedRows As Integer = conn.Execute("UPDATE [Patient_Notes] SET [PatientID] = @NewPatientID, [NoteDate] = @NewNoteDate, [Note] = @NewNote WHERE [PatientID] = @OldPatientID AND [NoteDate] = @OldNoteDate AND [Note] = @OldNote", parameters)
			    Return affectedRows > 0
			End Using
		End Function

    Public Function Update_Record(note As Patient_Notes) As Boolean
        Dim sql As String = "UPDATE Patient_Notes SET PatientID = @PatientID, Note = @Note, NoteDate = @NoteDate WHERE NoteID = @NoteID"
        Using conn As New SqlConnection(DentistXDATA.GetConnection.ConnectionString)
            conn.Execute(sql, note)
        End Using
        Return True
    End Function

    Public Function Delete(ByVal clsPatient_Notes As Patient_Notes) As Boolean
			Dim deleteStatement As String =
			"DELETE FROM [Patient_Notes] 
			WHERE NoteID = @NoteID"
			Using connection As SqlConnection = DentistXData.GetConnection()
			    connection.Open()
			    Dim affectedRows As Integer = connection.Execute(deleteStatement, New With { .NoteID = clsPatient_Notes.NoteID })
			    Return affectedRows > 0
			End Using
		End Function


'Methods to get parents and childs
		Public Function GetPatient(ByVal PatientID As Integer) As Patient
		Dim parent As Patient = Nothing
		Using conn As New SqlConnection(ConnectionString)
			Dim query As String = "SELECT * FROM [Patient] WHERE [PatientID] = @PatientID"
			Try
				conn.Open()
				parent = conn.QuerySingleOrDefault(Of Patient)(query, New With {.PatientId = PatientID})
			Catch ex As Exception
				' Handle exceptions
			Finally
				If conn.State = ConnectionState.Open Then conn.Close()
			End Try
		End Using
		Return parent
		End Function

	End Class
