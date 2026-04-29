Public Class Alert
    Public Property Type As String
    Public Property Title As String
    Public Property Message As String
    Public Property Priority As AlertPriority
    Public Property Timestamp As DateTime

    Public Enum AlertPriority
        Low
        Medium
        High
        Critical
    End Enum

End Class