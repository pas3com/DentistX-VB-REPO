Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections
Imports System.Configuration
Imports Dapper

	Public Class Patient_MobStrucAddDATA

		Private ConnectionString As String=DentistXDATA.GetConnection.connectionString



		Public Function SelectAll() As IEnumerable(Of Patient_MobStrucAdd)
			Using conn As New SqlConnection(ConnectionString)
			    conn.Open()
			    Return conn.Query(Of Patient_MobStrucAdd)("SELECT * FROM Patient_MobStrucAdd")
			End Using
		End Function
		

		Public Function Select_Record(ByVal clsPatient_MobStrucAdd As Patient_MobStrucAdd) As Patient_MobStrucAdd
			Using conn As New SqlConnection(ConnectionString)
			    Dim sql As String = "Select * FROM Patient_MobStrucAdd WHERE AddTothID = @AddTothID"
			    Return conn.QuerySingleOrDefault(Of Patient_MobStrucAdd)(sql, New With { .AddTothID = clsPatient_MobStrucAdd.AddTothID })
			End Using
		End Function

		Public Function Add(ByVal clsPatient_MobStrucAdd As Patient_MobStrucAdd) As Boolean
			Dim RowsAffected As Integer=0
			Using conn As New SqlConnection(ConnectionString)
			    Dim sql As String = "INSERT INTO Patient_MobStrucAdd (StrucID, StrucName, ToothLoc, ToothNum, AddTothDate) VALUES (@StrucID, @StrucName, @ToothLoc, @ToothNum, @AddTothDate)" 
			    RowsAffected = conn.Execute(sql, New With { .StrucID =  clsPatient_MobStrucAdd.StrucID, .StrucName =  clsPatient_MobStrucAdd.StrucName, .ToothLoc =  clsPatient_MobStrucAdd.ToothLoc, .ToothNum =  clsPatient_MobStrucAdd.ToothNum, .AddTothDate =  clsPatient_MobStrucAdd.AddTothDate })
			    Return RowsAffected > 0
			End Using
		End Function

		Public Function Update(oldPatient_MobStrucAdd As Patient_MobStrucAdd, newPatient_MobStrucAdd As Patient_MobStrucAdd) As Boolean
			Using conn As New SqlConnection(ConnectionString)
			    Dim parameters = New With { 
					.NewStrucID = newPatient_MobStrucAdd.StrucID, .OldStrucID = oldPatient_MobStrucAdd.StrucID, .NewStrucName = newPatient_MobStrucAdd.StrucName, .OldStrucName = oldPatient_MobStrucAdd.StrucName, .NewToothLoc = newPatient_MobStrucAdd.ToothLoc, .OldToothLoc = oldPatient_MobStrucAdd.ToothLoc, .NewToothNum = newPatient_MobStrucAdd.ToothNum, .OldToothNum = oldPatient_MobStrucAdd.ToothNum, .NewAddTothDate = newPatient_MobStrucAdd.AddTothDate, .OldAddTothDate = oldPatient_MobStrucAdd.AddTothDate
										  }
			    Dim affectedRows As Integer = conn.Execute("UPDATE [Patient_MobStrucAdd] SET [StrucID] = @NewStrucID, [StrucName] = @NewStrucName, [ToothLoc] = @NewToothLoc, [ToothNum] = @NewToothNum, [AddTothDate] = @NewAddTothDate WHERE [StrucID] = @OldStrucID AND [StrucName] = @OldStrucName AND [ToothLoc] = @OldToothLoc AND [ToothNum] = @OldToothNum AND [AddTothDate] = @OldAddTothDate", parameters)
			    Return affectedRows > 0
			End Using
		End Function

		Public Function Delete(ByVal clsPatient_MobStrucAdd As Patient_MobStrucAdd) As Boolean
			Dim deleteStatement As String =
			"DELETE FROM [Patient_MobStrucAdd] 
			WHERE AddTothID = @AddTothID"
			Using connection As SqlConnection = DentistXData.GetConnection()
			    connection.Open()
			    Dim affectedRows As Integer = connection.Execute(deleteStatement, New With { .AddTothID = clsPatient_MobStrucAdd.AddTothID })
			    Return affectedRows > 0
			End Using
		End Function


'Methods to get parents and childs
		Public Function GetPatient_MobStruc(ByVal StrucID As Integer) As Patient_MobStruc
		Dim parent As Patient_MobStruc = Nothing
		Using conn As New SqlConnection(ConnectionString)
			Dim query As String = "SELECT * FROM [Patient_MobStruc] WHERE [StrucID] = @StrucID"
			Try
				conn.Open()
				parent = conn.QuerySingleOrDefault(Of Patient_MobStruc)(query, New With {.StrucID = StrucID})
			Catch ex As Exception
				' Handle exceptions
			Finally
				If conn.State = ConnectionState.Open Then conn.Close()
			End Try
		End Using
		Return parent
		End Function

	End Class
