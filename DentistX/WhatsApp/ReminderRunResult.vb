Imports System.Collections.Generic

''' <summary>Outcome of a batch WhatsApp reminder run (appointment and/or account).</summary>
Public Class ReminderRunResult
    Public Property SentCount As Integer
    Public Property Lines As List(Of String)

    Public Sub New()
        Lines = New List(Of String)()
    End Sub
End Class
