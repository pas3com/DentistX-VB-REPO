''' <summary>One row in the form-access grid (bound to Grids).</summary>
Public Class UserFormAccessRowVm
    Public Property FormID As Integer
    Public Property FormName As String
    ''' <summary>Whole-word source hits after running Scan — estimate only; not persisted.</summary>
    Public Property SrcRefHits As String
    ''' <summary>Persisted as Forms.DisplayTitle; friendly name (forms often use .Text at runtime; user controls usually blank until you set DisplayName on class or type here).</summary>
    Public Property Title As String
    Public Property TitleAr As String
    Public Property Description As String
    Public Property IsAllowed As Boolean
End Class
