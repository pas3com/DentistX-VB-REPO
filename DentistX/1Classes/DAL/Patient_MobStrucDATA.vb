Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections
Imports System.Configuration
Imports Dapper

	Public Class Patient_MobStrucDATA

		Private ConnectionString As String=DentistXDATA.GetConnection.connectionString



		Public Function SelectAll() As IEnumerable(Of Patient_MobStruc)
			Using conn As New SqlConnection(ConnectionString)
			    conn.Open()
			    Return conn.Query(Of Patient_MobStruc)("SELECT * FROM Patient_MobStruc")
			End Using
		End Function
		

		Public Function Select_Record(ByVal clsPatient_MobStruc As Patient_MobStruc) As Patient_MobStruc
			Using conn As New SqlConnection(ConnectionString)
			    Dim sql As String = "Select * FROM Patient_MobStruc WHERE StrucID = @StrucID"
			    Return conn.QuerySingleOrDefault(Of Patient_MobStruc)(sql, New With { .StrucID = clsPatient_MobStruc.StrucID })
			End Using
		End Function

		Public Function Add(ByVal clsPatient_MobStruc As Patient_MobStruc) As Boolean
			Dim RowsAffected As Integer=0
			Using conn As New SqlConnection(ConnectionString)
			    Dim sql As String = "INSERT INTO Patient_MobStruc (PatientID, StrucName, StrucType, TeethType, StrucDate) VALUES (@PatientID, @StrucName, @StrucType, @TeethType, @StrucDate)" 
			    RowsAffected = conn.Execute(sql, New With { .PatientID =  clsPatient_MobStruc.PatientID, .StrucName =  clsPatient_MobStruc.StrucName, .StrucType =  clsPatient_MobStruc.StrucType, .TeethType =  clsPatient_MobStruc.TeethType, .StrucDate =  clsPatient_MobStruc.StrucDate })
			    Return RowsAffected > 0
			End Using
		End Function

		Public Function Update(oldPatient_MobStruc As Patient_MobStruc, newPatient_MobStruc As Patient_MobStruc) As Boolean
			Using conn As New SqlConnection(ConnectionString)
			    Dim parameters = New With { 
					.NewPatientID = newPatient_MobStruc.PatientID, .OldPatientID = oldPatient_MobStruc.PatientID, .NewStrucName = newPatient_MobStruc.StrucName, .OldStrucName = oldPatient_MobStruc.StrucName, .NewStrucType = newPatient_MobStruc.StrucType, .OldStrucType = oldPatient_MobStruc.StrucType, .NewTeethType = newPatient_MobStruc.TeethType, .OldTeethType = oldPatient_MobStruc.TeethType, .NewStrucDate = newPatient_MobStruc.StrucDate, .OldStrucDate = oldPatient_MobStruc.StrucDate
										  }
			    Dim affectedRows As Integer = conn.Execute("UPDATE [Patient_MobStruc] SET [PatientID] = @NewPatientID, [StrucName] = @NewStrucName, [StrucType] = @NewStrucType, [TeethType] = @NewTeethType, [StrucDate] = @NewStrucDate WHERE [PatientID] = @OldPatientID AND [StrucName] = @OldStrucName AND [StrucType] = @OldStrucType AND [TeethType] = @OldTeethType AND [StrucDate] = @OldStrucDate", parameters)
			    Return affectedRows > 0
			End Using
		End Function

		Public Function Delete(ByVal clsPatient_MobStruc As Patient_MobStruc) As Boolean
			Dim deleteStatement As String =
			"DELETE FROM [Patient_MobStruc] 
			WHERE StrucID = @StrucID"
			Using connection As SqlConnection = DentistXData.GetConnection()
			    connection.Open()
			    Dim affectedRows As Integer = connection.Execute(deleteStatement, New With { .StrucID = clsPatient_MobStruc.StrucID })
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

		Public Function GetPatient_MobStrucAdd(ByVal clsPatient_MobStruc As Patient_MobStruc ) As IEnumerable(Of Patient_MobStrucAdd)
			Using conn As New SqlConnection(ConnectionString)
				conn.Open()
				Dim query As String = "SELECT * FROM [Patient_MobStrucAdd] WHERE [StrucID] = @StrucID"
				Return conn.Query(Of Patient_MobStrucAdd)(query, New With { .StrucID= clsPatient_MobStruc.StrucID })
			End Using
		End Function

	End Class
