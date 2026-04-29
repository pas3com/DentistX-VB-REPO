Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections
Imports System.Configuration
Imports Dapper

	Public Class Patient_ImgsDATA

		Private ConnectionString As String=DentistXDATA.GetConnection.connectionString



		Public Function SelectAll() As IEnumerable(Of Patient_Imgs)
			Using conn As New SqlConnection(ConnectionString)
			    conn.Open()
			    Return conn.Query(Of Patient_Imgs)("SELECT * FROM Patient_Imgs")
			End Using
		End Function
		

		Public Function Select_Record(ByVal clsPatient_Imgs As Patient_Imgs) As Patient_Imgs
			Using conn As New SqlConnection(ConnectionString)
			    Dim sql As String = "Select * FROM Patient_Imgs WHERE PicID = @PicID"
			    Return conn.QuerySingleOrDefault(Of Patient_Imgs)(sql, New With { .PicID = clsPatient_Imgs.PicID })
			End Using
		End Function

		Public Function Add(ByVal clsPatient_Imgs As Patient_Imgs) As Boolean
			Dim RowsAffected As Integer=0
			Using conn As New SqlConnection(ConnectionString)
			    Dim sql As String = "INSERT INTO Patient_Imgs (PatientID, PicName, PicPath, FullName) VALUES (@PatientID, @PicName, @PicPath, @FullName)" 
			    RowsAffected = conn.Execute(sql, New With { .PatientID =  clsPatient_Imgs.PatientID, .PicName =  clsPatient_Imgs.PicName, .PicPath =  clsPatient_Imgs.PicPath, .FullName =  clsPatient_Imgs.FullName })
			    Return RowsAffected > 0
			End Using
		End Function

		Public Function Update(oldPatient_Imgs As Patient_Imgs, newPatient_Imgs As Patient_Imgs) As Boolean
			Using conn As New SqlConnection(ConnectionString)
			    Dim parameters = New With { 
					.NewPatientID = newPatient_Imgs.PatientID, .OldPatientID = oldPatient_Imgs.PatientID, .NewPicName = newPatient_Imgs.PicName, .OldPicName = oldPatient_Imgs.PicName, .NewPicPath = newPatient_Imgs.PicPath, .OldPicPath = oldPatient_Imgs.PicPath, .NewFullName = newPatient_Imgs.FullName, .OldFullName = oldPatient_Imgs.FullName
										  }
			    Dim affectedRows As Integer = conn.Execute("UPDATE [Patient_Imgs] SET [PatientID] = @NewPatientID, [PicName] = @NewPicName, [PicPath] = @NewPicPath, [FullName] = @NewFullName WHERE [PatientID] = @OldPatientID AND [PicName] = @OldPicName AND [PicPath] = @OldPicPath AND [FullName] = @OldFullName", parameters)
			    Return affectedRows > 0
			End Using
		End Function

		Public Function Delete(ByVal clsPatient_Imgs As Patient_Imgs) As Boolean
			Dim deleteStatement As String =
			"DELETE FROM [Patient_Imgs] 
			WHERE PicID = @PicID"
			Using connection As SqlConnection = DentistXData.GetConnection()
			    connection.Open()
			    Dim affectedRows As Integer = connection.Execute(deleteStatement, New With { .PicID = clsPatient_Imgs.PicID })
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
