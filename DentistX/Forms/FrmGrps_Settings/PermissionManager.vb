Imports System.Data.SqlClient
Imports Dapper

Public Module PermissionManager



    Private ReadOnly FormPermissions As New Dictionary(Of String, Boolean)

    ''' <summary>
    ''' Loads all permissions for the current group into memory
    ''' </summary>
    ''' <param name="groupName">The name of the current group (e.g., "Admin", "Manager")</param>
    Public Sub LoadPermissions(groupName As String)
        FormPermissions.Clear()

        Dim sql As String = "SELECT F.FormName, ISNULL(P.HasAccess, 0) AS HasAccess
                             FROM Forms F
                             LEFT JOIN Permissions P ON F.FormName = P.FormName AND P.GroupName = @GroupName"

        Using conn As New SqlConnection(DentistXDATA.GetConnection.ConnectionString)
            Dim result = conn.Query(Of PermissionRecord)(sql, New With {.GroupName = groupName}).ToList()

            For Each record In result
                FormPermissions(record.FormName) = record.HasAccess
            Next
        End Using
    End Sub

    ''' <summary>
    ''' Checks if a form is allowed for the currently logged in group
    ''' </summary>
    ''' <param name="formName">The name of the form (e.g., "FrmUsers")</param>
    ''' <returns>True if allowed, False otherwise</returns>
    Public Function HasAccess(formName As String) As Boolean
        If FormPermissions.ContainsKey(formName) Then
            Return FormPermissions(formName)
        End If

        ' Default to no access if not found
        Return False
    End Function

    Private Class PermissionRecord
        Public Property FormName As String
        Public Property HasAccess As Boolean
    End Class


    Public Function HasPermission(formName As String, permissionType As String) As Boolean
        Using conn As New SqlConnection(DentistXDATA.GetConnection.ConnectionString)
            Dim sql As String = "SELECT COUNT(*) FROM Permissions P
                                 INNER JOIN Forms F ON P.FormID = F.FormID
                                 WHERE F.FormName = @FormName AND P.GroupID = @GroupID AND " & permissionType & " = 1"

            Dim count As Integer = conn.ExecuteScalar(Of Integer)(sql, New With {
                .FormName = formName,
                .GroupID = CurrentUser.GroupID
            })

            Return count > 0
        End Using
    End Function
End Module
