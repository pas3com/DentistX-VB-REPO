Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections
Imports System.Configuration
Imports Dapper

	Public Class Patient_RXDATA

		Private ConnectionString As String=DentistXDATA.GetConnection.connectionString



		Public Function SelectAll() As IEnumerable(Of Patient_RX)
			Using conn As New SqlConnection(ConnectionString)
			    conn.Open()
			    Return conn.Query(Of Patient_RX)("SELECT * FROM Patient_RX")
			End Using
		End Function
		

		Public Function Select_Record(ByVal clsPatient_RX As Patient_RX) As Patient_RX
			Using conn As New SqlConnection(ConnectionString)
			    Dim sql As String = "Select * FROM Patient_RX WHERE RxID = @RxID"
			    Return conn.QuerySingleOrDefault(Of Patient_RX)(sql, New With { .RxID = clsPatient_RX.RxID })
			End Using
		End Function

		Public Function Add(ByVal clsPatient_RX As Patient_RX) As Boolean
			Dim RowsAffected As Integer=0
			Using conn As New SqlConnection(ConnectionString)
			    Dim sql As String = "INSERT INTO Patient_RX (PatientID, RXDate, RX) VALUES (@PatientID, @RXDate, @RX)" 
			    RowsAffected = conn.Execute(sql, New With { .PatientID =  clsPatient_RX.PatientID, .RXDate =  clsPatient_RX.RXDate, .RX =  clsPatient_RX.RX })
			    Return RowsAffected > 0
			End Using
		End Function

		Public Function Update(oldPatient_RX As Patient_RX, newPatient_RX As Patient_RX) As Boolean
			Using conn As New SqlConnection(ConnectionString)
			    Dim parameters = New With { 
					.NewPatientID = newPatient_RX.PatientID, .OldPatientID = oldPatient_RX.PatientID, .NewRXDate = newPatient_RX.RXDate, .OldRXDate = oldPatient_RX.RXDate, .NewRX = newPatient_RX.RX, .OldRX = oldPatient_RX.RX
										  }
			    Dim affectedRows As Integer = conn.Execute("UPDATE [Patient_RX] SET [PatientID] = @NewPatientID, [RXDate] = @NewRXDate, [RX] = @NewRX WHERE [PatientID] = @OldPatientID AND [RXDate] = @OldRXDate AND [RX] = @OldRX", parameters)
			    Return affectedRows > 0
			End Using
		End Function

		Public Function Delete(ByVal clsPatient_RX As Patient_RX) As Boolean
			Dim deleteStatement As String =
			"DELETE FROM [Patient_RX] 
			WHERE RxID = @RxID"
			Using connection As SqlConnection = DentistXData.GetConnection()
			    connection.Open()
			    Dim affectedRows As Integer = connection.Execute(deleteStatement, New With { .RxID = clsPatient_RX.RxID })
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
