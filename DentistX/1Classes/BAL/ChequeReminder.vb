Public Class ChequeReminder
    Public Property PayID As Integer
    Public Property PatientID As Integer
    Public Property PatientName As String
    Public Property ChqOwner As String
    Public Property AccountNumber As String
    Public Property ChqNumber As String
    Public Property ChqBank As String
    Public Property ChqDueDate As Nullable(Of Date)
    Public Property AdjustedDueDate As Nullable(Of Date)
    Public Property PayValue As Decimal
    Public Property PayDate As Date
    Public Property Notes As String
    Public Property PayType As String
    Public Property IsCashed As Nullable(Of Boolean)
    ''' <summary>Cheque returned / bounced — excluded from patient payment totals when true.</summary>
    Public Property IsReturned As Nullable(Of Boolean)
    Public Property IsForward As String
    ''' <summary>Resolved scan path (not from SQL); set in UI after load.</summary>
    Public Property LinkedImagePath As String
End Class
