Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections
Imports System.Configuration
Imports Dapper

	Public Class GroupsDATA

		Private ConnectionString As String=DentistXDATA.GetConnection.connectionString



		Public Function SelectAll() As IEnumerable(Of Groups)
			Using conn As New SqlConnection(ConnectionString)
			    conn.Open()
			    Return conn.Query(Of Groups)("SELECT * FROM Groups")
			End Using
		End Function
		

		Public Function Select_Record(ByVal clsGroups As Groups) As Groups
			Using conn As New SqlConnection(ConnectionString)
			    Dim sql As String = "Select * FROM Groups WHERE GroupID = @GroupID"
			    Return conn.QuerySingleOrDefault(Of Groups)(sql, New With { .GroupID = clsGroups.GroupID })
			End Using
		End Function

		Public Function Add(ByVal clsGroups As Groups) As Boolean
			Dim RowsAffected As Integer=0
			Using conn As New SqlConnection(ConnectionString)
			    Dim sql As String = "INSERT INTO Groups (GroupName) VALUES (@GroupName)" 
			    RowsAffected = conn.Execute(sql, New With { .GroupName =  clsGroups.GroupName })
			    Return RowsAffected > 0
			End Using
		End Function

		Public Function Update(oldGroups As Groups, newGroups As Groups) As Boolean
			Using conn As New SqlConnection(ConnectionString)
			    Dim parameters = New With { 
					.NewGroupName = newGroups.GroupName, .OldGroupName = oldGroups.GroupName
										  }
			    Dim affectedRows As Integer = conn.Execute("UPDATE [Groups] SET [GroupName] = @NewGroupName WHERE [GroupName] = @OldGroupName", parameters)
			    Return affectedRows > 0
			End Using
		End Function

		Public Function Delete(ByVal clsGroups As Groups) As Boolean
			Dim deleteStatement As String =
			"DELETE FROM [Groups] 
			WHERE GroupID = @GroupID"
			Using connection As SqlConnection = DentistXData.GetConnection()
			    connection.Open()
			    Dim affectedRows As Integer = connection.Execute(deleteStatement, New With { .GroupID = clsGroups.GroupID })
			    Return affectedRows > 0
			End Using
		End Function


'Methods to get parents and childs
		Public Function GetUSERS(ByVal clsGroups As Groups ) As IEnumerable(Of USERS)
			Using conn As New SqlConnection(ConnectionString)
				conn.Open()
				Dim query As String = "SELECT * FROM [USERS] WHERE [GroupID] = @GroupID"
				Return conn.Query(Of USERS)(query, New With { .GroupID= clsGroups.GroupID })
			End Using
		End Function

	End Class
