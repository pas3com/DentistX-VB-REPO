Imports System.Data.SqlClient
Imports Dapper

Public Class PermissionDATA

    Public Function SelectAll() As List(Of Permission)
        Using conn As New SqlConnection(DentistXDATA.GetConnection.ConnectionString)
            Return conn.Query(Of Permission)("SELECT * FROM Permissions").ToList()
        End Using
    End Function

    Public Function SelectRecord(PermissionID As Integer) As Permission
        Using conn As New SqlConnection(DentistXDATA.GetConnection.ConnectionString)
            Return conn.QueryFirstOrDefault(Of Permission)("SELECT * FROM Permissions WHERE PermissionID = @PermissionID", New With {.PermissionID = PermissionID})
        End Using
    End Function

    Public Function Add(p As Permission) As Boolean
        Using conn As New SqlConnection(DentistXDATA.GetConnection.ConnectionString)
            Dim sql = "INSERT INTO Permissions (GroupID, FormID, CanView, CanEdit, CanDelete) " &
                      "VALUES (@GroupID, @FormID, @CanView, @CanEdit, @CanDelete)"
            Return conn.Execute(sql, p) > 0
        End Using
    End Function

    Public Function Update(p As Permission) As Boolean
        Using conn As New SqlConnection(DentistXDATA.GetConnection.ConnectionString)
            Dim sql = "UPDATE Permissions SET GroupID = @GroupID, FormID = @FormID, CanView = @CanView, CanEdit = @CanEdit, CanDelete = @CanDelete " &
                      "WHERE PermissionID = @PermissionID"
            Return conn.Execute(sql, p) > 0
        End Using
    End Function

    Public Function Delete(PermissionID As Integer) As Boolean
        Using conn As New SqlConnection(DentistXDATA.GetConnection.ConnectionString)
            Return conn.Execute("DELETE FROM Permissions WHERE PermissionID = @PermissionID", New With {.PermissionID = PermissionID}) > 0
        End Using
    End Function


    ' Select permissions for a specific group
    Public Function SelectPermissionsByGroup(groupID As Integer) As List(Of Permission)
        Using connection As New SqlConnection(DentistXDATA.GetConnection.ConnectionString)
            Dim sql As String = "
            SELECT 
                p.PermissionID,
                p.GroupID,
                g.GroupName,
                p.FormID,
                f.FormName,
                p.CanView,
                p.CanEdit,
                p.CanDelete
            FROM Permissions p
            INNER JOIN Forms f ON p.FormID = f.FormID
            INNER JOIN Groups g ON p.GroupID = g.GroupID
            WHERE p.GroupID = @GroupID
        "

            Return connection.Query(Of Permission)(
            sql,
            New With {.GroupID = groupID}
        ).ToList()
        End Using
    End Function

    'Public Function SelectPermissionsByGroup(groupID As Integer) As List(Of Permission)
    '    Using connection As New SqlConnection(DentistXDATA.GetConnection.ConnectionString)
    '        Return connection.Query(Of Permission)(
    '            "SELECT PermissionID, GroupID, FormID, CanView, CanEdit, CanDelete 
    '             FROM Permissions 
    '             WHERE GroupID = @GroupID",
    '            New With {.GroupID = groupID}
    '        ).ToList()
    '    End Using
    'End Function


    ' Save permission for a group and form
    Public Function SavePermission(permission As Permission) As Boolean
        Using connection As New SqlConnection(DentistXDATA.GetConnection.ConnectionString)
            ' First, check if the permission already exists
            Dim existingPermission = connection.QuerySingleOrDefault(Of Permission)(
                "SELECT * FROM Permissions WHERE GroupID = @GroupID AND FormID = @FormID",
                New With {.GroupID = permission.GroupID, .FormID = permission.FormID}
            )

            If existingPermission IsNot Nothing Then
                ' Update the existing permission
                Dim rowsAffected = connection.Execute(
                    "UPDATE Permissions 
                     SET CanView = @CanView, CanEdit = @CanEdit, CanDelete = @CanDelete 
                     WHERE PermissionID = @PermissionID", permission)
                Return rowsAffected > 0
            Else
                ' Insert new permission
                Dim rowsAffected = connection.Execute(
                    "INSERT INTO Permissions (GroupID, FormID, CanView, CanEdit, CanDelete) 
                     VALUES (@GroupID, @FormID, @CanView, @CanEdit, @CanDelete)", permission)
                Return rowsAffected > 0
            End If
        End Using
    End Function

End Class
