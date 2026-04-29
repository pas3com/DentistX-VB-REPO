Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections
Imports System.Configuration
Imports Dapper

	Public Class HealthDATA

		Private ConnectionString As String=DentistXDATA.GetConnection.connectionString



		Public Function SelectAll() As IEnumerable(Of Health)
			Using conn As New SqlConnection(ConnectionString)
			    conn.Open()
			    Return conn.Query(Of Health)("SELECT * FROM Health")
			End Using
		End Function
		

		Public Function Select_Record(ByVal clsHealth As Health) As Health
			Using conn As New SqlConnection(ConnectionString)
			    Dim sql As String = "Select * FROM Health WHERE HID = @HID"
			    Return conn.QuerySingleOrDefault(Of Health)(sql, New With { .HID = clsHealth.HID })
			End Using
		End Function

		Public Function Add(ByVal clsHealth As Health) As Boolean
			Dim RowsAffected As Integer=0
			Using conn As New SqlConnection(ConnectionString)
			    Dim sql As String = "INSERT INTO Health (HealthStat) VALUES (@HealthStat)" 
			    RowsAffected = conn.Execute(sql, New With { .HealthStat =  clsHealth.HealthStat })
			    Return RowsAffected > 0
			End Using
		End Function

		Public Function Update(oldHealth As Health, newHealth As Health) As Boolean
			Using conn As New SqlConnection(ConnectionString)
			    Dim parameters = New With { 
					.NewHealthStat = newHealth.HealthStat, .OldHealthStat = oldHealth.HealthStat
										  }
			    Dim affectedRows As Integer = conn.Execute("UPDATE [Health] SET [HealthStat] = @NewHealthStat WHERE [HealthStat] = @OldHealthStat", parameters)
			    Return affectedRows > 0
			End Using
		End Function

		Public Function Delete(ByVal clsHealth As Health) As Boolean
			Dim deleteStatement As String =
			"DELETE FROM [Health] 
			WHERE HID = @HID"
			Using connection As SqlConnection = DentistXData.GetConnection()
			    connection.Open()
			    Dim affectedRows As Integer = connection.Execute(deleteStatement, New With { .HID = clsHealth.HID })
			    Return affectedRows > 0
			End Using
		End Function


'Methods to get parents and childs
	End Class
