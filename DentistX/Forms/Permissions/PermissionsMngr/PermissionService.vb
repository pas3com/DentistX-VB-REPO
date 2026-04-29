Public Class PermissionService

    Private ReadOnly _permLookup As Dictionary(Of String, Boolean)

    Public Sub New(effectivePermissions As IEnumerable(Of PermissionItem))
        ' Normalize keys to avoid case bugs
        _permLookup = effectivePermissions _
            .GroupBy(Function(p) p.PermKey) _
            .ToDictionary(
                Function(g) g.Key.ToLowerInvariant(),
                Function(g) g.Last().IsAllowed
            )
    End Sub

    Public Function CanDo(permKey As String) As Boolean
        If String.IsNullOrWhiteSpace(permKey) Then Return False

        Dim key = permKey.ToLowerInvariant()

        Return _permLookup.ContainsKey(key) AndAlso _permLookup(key)
    End Function

End Class
