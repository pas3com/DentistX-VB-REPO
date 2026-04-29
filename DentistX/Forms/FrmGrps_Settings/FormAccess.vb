Public Class FormAccess
    Public Property FormID As Integer
    Public Property FormName As String
    Public Property Description As String
    ''' <summary>Friendly title in admin grid; optional. Sync can fill from System.ComponentModel.DisplayNameAttribute on the class.</summary>
    Public Property DisplayTitle As String
    ''' <summary>Arabic-friendly title for the same screen (manual or sync later).</summary>
    Public Property DisplayTitleAr As String
End Class
