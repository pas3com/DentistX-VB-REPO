Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections
Imports System.Configuration
Imports Dapper

	Public Class PermissionsDATA

		Private ConnectionString As String=DentistXDATA.GetConnection.connectionString



		Public Function SelectAll() As IEnumerable(Of Permissions)
			Using conn As New SqlConnection(ConnectionString)
			    conn.Open()
			    Return conn.Query(Of Permissions)("SELECT * FROM Permissions")
			End Using
		End Function
		

		Public Function Select_Record(ByVal clsPermissions As Permissions) As Permissions
			Using conn As New SqlConnection(ConnectionString)
			    Dim sql As String = "Select * FROM Permissions WHERE PermID = @PermID"
			    Return conn.QuerySingleOrDefault(Of Permissions)(sql, New With { .PermID = clsPermissions.PermID })
			End Using
		End Function

	Public Function GetPermissions() As IEnumerable(Of PermissionResult)
		Dim sql As String = "
        SELECT p.PermName,
               ISNULL(up.IsAllowed, gp.IsAllowed) AS IsAllowed
        FROM Permissions p
        LEFT JOIN GroupPermissions gp ON gp.PermID = p.PermID AND gp.GroupID = @GroupID
        LEFT JOIN UserPermissions up ON up.PermID = p.PermID AND up.UsID = @UsID
    "

		Return Conn.Query(Of PermissionResult)(sql, New With {.GroupID = CurrentUser.GroupID, .UsID = CurrentUser.UsID})
	End Function


	Public Function Add(ByVal clsPermissions As Permissions) As Boolean
		Dim RowsAffected As Integer = 0
		Using conn As New SqlConnection(ConnectionString)
			Dim sql As String = "INSERT INTO Permissions (PermKey, PermName, PermDescription, Category, IsActive, CreatedAt) VALUES (@PermKey, @PermName, @PermDescription, @Category, @IsActive, @CreatedAt)"
			RowsAffected = conn.Execute(sql, New With {.PermKey = clsPermissions.PermKey, .PermName = clsPermissions.PermName, .PermDescription = clsPermissions.PermDescription, .Category = clsPermissions.Category, .IsActive = clsPermissions.IsActive, .CreatedAt = clsPermissions.CreatedAt})
			Return RowsAffected > 0
		End Using
	End Function

	Public Function Update(oldPermissions As Permissions, newPermissions As Permissions) As Boolean
		Using conn As New SqlConnection(ConnectionString)
			Dim parameters = New With {
					.NewPermKey = newPermissions.PermKey, .OldPermKey = oldPermissions.PermKey, .NewPermName = newPermissions.PermName, .OldPermName = oldPermissions.PermName, .NewPermDescription = newPermissions.PermDescription, .OldPermDescription = oldPermissions.PermDescription, .NewCategory = newPermissions.Category, .OldCategory = oldPermissions.Category, .NewIsActive = newPermissions.IsActive, .OldIsActive = oldPermissions.IsActive, .NewCreatedAt = newPermissions.CreatedAt, .OldCreatedAt = oldPermissions.CreatedAt
										  }
			Dim affectedRows As Integer = conn.Execute("UPDATE [Permissions] SET [PermKey] = @NewPermKey, [PermName] = @NewPermName, [PermDescription] = @NewPermDescription, [Category] = @NewCategory, [IsActive] = @NewIsActive, [CreatedAt] = @NewCreatedAt WHERE [PermKey] = @OldPermKey AND [PermName] = @OldPermName AND [PermDescription] = @OldPermDescription AND [Category] = @OldCategory AND [IsActive] = @OldIsActive AND [CreatedAt] = @OldCreatedAt", parameters)
			Return affectedRows > 0
		End Using
	End Function


	Public Function Delete(ByVal clsPermissions As Permissions) As Boolean
			Dim deleteStatement As String =
			"DELETE FROM [Permissions] 
			WHERE PermID = @PermID"
			Using connection As SqlConnection = DentistXData.GetConnection()
			    connection.Open()
			    Dim affectedRows As Integer = connection.Execute(deleteStatement, New With { .PermID = clsPermissions.PermID })
			    Return affectedRows > 0
			End Using
		End Function


'Methods to get parents and childs
		Public Function GetGroupPermissions(ByVal clsPermissions As Permissions ) As IEnumerable(Of GroupPermissions)
			Using conn As New SqlConnection(ConnectionString)
				conn.Open()
				Dim query As String = "SELECT * FROM [GroupPermissions] WHERE [PermID] = @PermID"
				Return conn.Query(Of GroupPermissions)(query, New With { .PermID= clsPermissions.PermID })
			End Using
		End Function

		Public Function GetUserPermissions(ByVal clsPermissions As Permissions ) As IEnumerable(Of UserPermissions)
			Using conn As New SqlConnection(ConnectionString)
				conn.Open()
				Dim query As String = "SELECT * FROM [UserPermissions] WHERE [PermID] = @PermID"
				Return conn.Query(Of UserPermissions)(query, New With { .PermID= clsPermissions.PermID })
			End Using
		End Function

	Public Function GetUserPermissions(userId As Integer) As IEnumerable(Of PermissionItem)

		Dim sql = "
        SELECT
            p.PermID,
            p.PermKey,
            p.PermName,
            gp.IsAllowed AS GroupAllowed,
            up.IsAllowed AS UserOverride,
            ISNULL(up.IsAllowed, gp.IsAllowed) AS IsAllowed
        FROM Permissions p
        INNER JOIN USERS u 
            ON u.UsID = @UsID
        LEFT JOIN GroupPermissions gp 
            ON gp.PermID = p.PermID
           AND gp.GroupID = u.GroupID
        LEFT JOIN UserPermissions up 
            ON up.UsID = u.UsID
           AND up.PermID = p.PermID
        WHERE p.IsActive = 1
        ORDER BY p.Category, p.PermName;
    "

		Return Conn.Query(Of PermissionItem)(sql, New With {.UsID = userId})

	End Function


	Public Function GetGroupPermissions(groupId As Integer) As IEnumerable(Of PermissionItem)

		Dim sql = "
        SELECT
            p.PermID,
            p.PermKey,
            p.PermName,
            gp.IsAllowed AS GroupAllowed,
            CAST(NULL AS BIT) AS UserOverride,
            ISNULL(gp.IsAllowed, 0) AS IsAllowed
        FROM Permissions p
        LEFT JOIN GroupPermissions gp 
            ON gp.PermID = p.PermID
           AND gp.GroupID = @GroupID
        WHERE p.IsActive = 1
        ORDER BY p.Category, p.PermName;
    "

		Return Conn.Query(Of PermissionItem)(sql, New With {.GroupID = groupId})

	End Function

End Class
