Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections
Imports System.Configuration
Imports Dapper

	Public Class GroupPermissionsDATA

		Private ConnectionString As String=DentistXDATA.GetConnection.connectionString



		Public Function SelectAll() As IEnumerable(Of GroupPermissions)
			Using conn As New SqlConnection(ConnectionString)
			    conn.Open()
			    Return conn.Query(Of GroupPermissions)("SELECT * FROM GroupPermissions")
			End Using
		End Function
		

		Public Function Select_Record(ByVal clsGroupPermissions As GroupPermissions) As GroupPermissions
			Using conn As New SqlConnection(ConnectionString)
			    Dim sql As String = "Select * FROM GroupPermissions WHERE GroupID = @GroupID And PermID = @PermID"
			    Return conn.QuerySingleOrDefault(Of GroupPermissions)(sql, New With { .GroupID = clsGroupPermissions.GroupID, .PermID = clsGroupPermissions.PermID })
			End Using
		End Function

		Public Function Add(ByVal clsGroupPermissions As GroupPermissions) As Boolean
			Dim RowsAffected As Integer=0
			Using conn As New SqlConnection(ConnectionString)
			    Dim sql As String = "INSERT INTO GroupPermissions (GroupID, PermID, IsAllowed) VALUES (@GroupID, @PermID, @IsAllowed)" 
			    RowsAffected = conn.Execute(sql, New With { .GroupID =  clsGroupPermissions.GroupID, .PermID =  clsGroupPermissions.PermID, .IsAllowed =  clsGroupPermissions.IsAllowed })
			    Return RowsAffected > 0
			End Using
		End Function

		Public Function Update(oldGroupPermissions As GroupPermissions, newGroupPermissions As GroupPermissions) As Boolean
			Using conn As New SqlConnection(ConnectionString)
			    Dim parameters = New With { 
					.NewGroupID = newGroupPermissions.GroupID, .OldGroupID = oldGroupPermissions.GroupID, .NewPermID = newGroupPermissions.PermID, .OldPermID = oldGroupPermissions.PermID, .NewIsAllowed = newGroupPermissions.IsAllowed, .OldIsAllowed = oldGroupPermissions.IsAllowed
										  }
			    Dim affectedRows As Integer = conn.Execute("UPDATE [GroupPermissions] SET [GroupID] = @NewGroupID, [PermID] = @NewPermID, [IsAllowed] = @NewIsAllowed WHERE [GroupID] = @OldGroupID AND [PermID] = @OldPermID AND [IsAllowed] = @OldIsAllowed", parameters)
			    Return affectedRows > 0
			End Using
		End Function

		Public Function Delete(ByVal clsGroupPermissions As GroupPermissions) As Boolean
			Dim deleteStatement As String =
			"DELETE FROM [GroupPermissions] 
			WHERE GroupID = @GroupID AND PermID = @PermID"
			Using connection As SqlConnection = DentistXData.GetConnection()
			    connection.Open()
			    Dim affectedRows As Integer = connection.Execute(deleteStatement, New With { .GroupID = clsGroupPermissions.GroupID, .PermID = clsGroupPermissions.PermID })
			    Return affectedRows > 0
			End Using
		End Function


'Methods to get parents and childs
		Public Function GetGroups(ByVal GroupID As Integer) As Groups
		Dim parent As Groups = Nothing
		Using conn As New SqlConnection(ConnectionString)
			Dim query As String = "SELECT * FROM [Groups] WHERE [GroupID] = @GroupID"
			Try
				conn.Open()
				parent = conn.QuerySingleOrDefault(Of Groups)(query, New With {.GroupID = GroupID})
			Catch ex As Exception
				' Handle exceptions
			Finally
				If conn.State = ConnectionState.Open Then conn.Close()
			End Try
		End Using
		Return parent
		End Function

		Public Function GetPermissions(ByVal PermID As Integer) As Permissions
		Dim parent As Permissions = Nothing
		Using conn As New SqlConnection(ConnectionString)
			Dim query As String = "SELECT * FROM [Permissions] WHERE [PermID] = @PermID"
			Try
				conn.Open()
				parent = conn.QuerySingleOrDefault(Of Permissions)(query, New With {.PermID = PermID})
			Catch ex As Exception
				' Handle exceptions
			Finally
				If conn.State = ConnectionState.Open Then conn.Close()
			End Try
		End Using
		Return parent
		End Function

	End Class
