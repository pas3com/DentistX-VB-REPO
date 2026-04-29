Public Class PermissionItem
    Public Property PermID As Integer
    Public Property PermKey As String   ' Patients.Add
    Public Property PermName As String
    ' Base permission set on group level (nullable)
    Public Property GroupAllowed As Boolean?
    ' Explicit user override (nullable)
    Public Property UserOverride As Boolean?
    ' Effective permission = if UserOverride is not null then UserOverride else GroupAllowed (not nullable)
    Public Property IsAllowed As Boolean
End Class

