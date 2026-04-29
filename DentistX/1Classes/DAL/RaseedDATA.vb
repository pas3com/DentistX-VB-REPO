Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections
Imports System.Configuration
Imports Dapper

	Public Class RaseedDATA

		Private ConnectionString As String=DentistXDATA.GetConnection.connectionString



		Public Function SelectAll() As IEnumerable(Of Raseed)
			Using conn As New SqlConnection(ConnectionString)
			    conn.Open()
			    Return conn.Query(Of Raseed)("SELECT * FROM Raseed")
			End Using
		End Function
		

		Public Function Select_Record(ByVal clsRaseed As Raseed) As Raseed
			Using conn As New SqlConnection(ConnectionString)
			    Dim sql As String = "Select * FROM Raseed WHERE PatientID = @PatientID"
			    Return conn.QuerySingleOrDefault(Of Raseed)(sql, New With { .PatientId = clsRaseed.PatientID })
			End Using
		End Function

		Public Function Add(ByVal clsRaseed As Raseed) As Boolean
			Dim RowsAffected As Integer=0
			Using conn As New SqlConnection(ConnectionString)
			    Dim sql As String = "INSERT INTO Raseed (PatientID, LastBal, Bal) VALUES (@PatientID, @LastBal, @Bal)" 
			    RowsAffected = conn.Execute(sql, New With { .PatientID =  clsRaseed.PatientID, .LastBal =  clsRaseed.LastBal, .Bal =  clsRaseed.Bal })
			    Return RowsAffected > 0
			End Using
		End Function

		Public Function Update(oldRaseed As Raseed, newRaseed As Raseed) As Boolean
			Using conn As New SqlConnection(ConnectionString)
			    Dim parameters = New With { 
					.NewPatientID = newRaseed.PatientID, .OldPatientID = oldRaseed.PatientID, .NewLastBal = newRaseed.LastBal, .OldLastBal = oldRaseed.LastBal, .NewBal = newRaseed.Bal, .OldBal = oldRaseed.Bal
										  }
			    Dim affectedRows As Integer = conn.Execute("UPDATE [Raseed] SET [PatientID] = @NewPatientID, [LastBal] = @NewLastBal, [Bal] = @NewBal WHERE [PatientID] = @OldPatientID AND [LastBal] = @OldLastBal AND [Bal] = @OldBal", parameters)
			    Return affectedRows > 0
			End Using
		End Function

		Public Function Delete(ByVal clsRaseed As Raseed) As Boolean
			Dim deleteStatement As String =
			"DELETE FROM [Raseed] 
			WHERE PatientID = @PatientID"
			Using connection As SqlConnection = DentistXData.GetConnection()
			    connection.Open()
			    Dim affectedRows As Integer = connection.Execute(deleteStatement, New With { .PatientId = clsRaseed.PatientID })
			    Return affectedRows > 0
			End Using
		End Function


'Methods to get parents and childs
	End Class
