Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections
Imports System.Configuration
Imports Dapper

	Public Class UserPermissionsDATA

		Private ConnectionString As String=DentistXDATA.GetConnection.connectionString



		Public Function SelectAll() As IEnumerable(Of UserPermissions)
			Using conn As New SqlConnection(ConnectionString)
			    conn.Open()
			    Return conn.Query(Of UserPermissions)("SELECT * FROM UserPermissions")
			End Using
		End Function
		

		Public Function Select_Record(ByVal clsUserPermissions As UserPermissions) As UserPermissions
			Using conn As New SqlConnection(ConnectionString)
			    Dim sql As String = "Select * FROM UserPermissions WHERE UsID = @UsID And PermID = @PermID"
			    Return conn.QuerySingleOrDefault(Of UserPermissions)(sql, New With { .UsID = clsUserPermissions.UsID, .PermID = clsUserPermissions.PermID })
			End Using
		End Function

		Public Function Add(ByVal clsUserPermissions As UserPermissions) As Boolean
			Dim RowsAffected As Integer=0
			Using conn As New SqlConnection(ConnectionString)
			    Dim sql As String = "INSERT INTO UserPermissions (UsID, PermID, IsAllowed) VALUES (@UsID, @PermID, @IsAllowed)" 
			    RowsAffected = conn.Execute(sql, New With { .UsID =  clsUserPermissions.UsID, .PermID =  clsUserPermissions.PermID, .IsAllowed =  clsUserPermissions.IsAllowed })
			    Return RowsAffected > 0
			End Using
		End Function

		Public Function Update(oldUserPermissions As UserPermissions, newUserPermissions As UserPermissions) As Boolean
			Using conn As New SqlConnection(ConnectionString)
			    Dim parameters = New With { 
					.NewUsID = newUserPermissions.UsID, .OldUsID = oldUserPermissions.UsID, .NewPermID = newUserPermissions.PermID, .OldPermID = oldUserPermissions.PermID, .NewIsAllowed = newUserPermissions.IsAllowed, .OldIsAllowed = oldUserPermissions.IsAllowed
										  }
			    Dim affectedRows As Integer = conn.Execute("UPDATE [UserPermissions] SET [UsID] = @NewUsID, [PermID] = @NewPermID, [IsAllowed] = @NewIsAllowed WHERE [UsID] = @OldUsID AND [PermID] = @OldPermID AND [IsAllowed] = @OldIsAllowed", parameters)
			    Return affectedRows > 0
			End Using
		End Function

		Public Function Delete(ByVal clsUserPermissions As UserPermissions) As Boolean
			Dim deleteStatement As String =
			"DELETE FROM [UserPermissions] 
			WHERE UsID = @UsID AND PermID = @PermID"
			Using connection As SqlConnection = DentistXData.GetConnection()
			    connection.Open()
			    Dim affectedRows As Integer = connection.Execute(deleteStatement, New With { .UsID = clsUserPermissions.UsID, .PermID = clsUserPermissions.PermID })
			    Return affectedRows > 0
			End Using
		End Function


'Methods to get parents and childs
		Public Function GetUSERS(ByVal UsID As Integer) As USERS
		Dim parent As USERS = Nothing
		Using conn As New SqlConnection(ConnectionString)
			Dim query As String = "SELECT * FROM [USERS] WHERE [UsID] = @UsID"
			Try
				conn.Open()
				parent = conn.QuerySingleOrDefault(Of USERS)(query, New With {.UsID = UsID})
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
