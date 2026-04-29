Public Class Permission
    Public Property PermissionID As Integer
    Public Property GroupID As Integer
    Public Property GroupName As String   ' This must match exactly!
    Public Property FormID As Integer
    Public Property FormName As String    ' This must match exactly!
    Public Property CanView As Boolean
    Public Property CanEdit As Boolean
    Public Property CanDelete As Boolean
End Class
