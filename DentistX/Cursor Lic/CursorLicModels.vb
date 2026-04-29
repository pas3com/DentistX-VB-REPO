Public Class CursorTrialState
    Public Property LicenseType As String
    Public Property FingerprintHash As String
    Public Property StartDate As Date
    Public Property EndDate As Date
    Public Property LastCheckDate As Date
    Public Property IsBlocked As Boolean
End Class

Public Class CursorLicenseDecoy
    Public Property Marker As String
    Public Property LicenseType As String
    Public Property FingerprintHash As String
    Public Property IssueDate As Date
    Public Property EndDate As Date
    Public Property Checksum As String
End Class

Public Class CursorRequestPayload
    Public Property FingerprintHash As String
    Public Property FingerprintRaw As String
    Public Property MachineName As String
    Public Property DoctorName As String
    Public Property ClinicName As String
    Public Property ContactEmail As String
    Public Property AppVersion As String
    Public Property StartDate As String
    Public Property EndDate As String
    Public Property LastCheckDate As String
    Public Property RequestedPlan As String
    Public Property RequestedPeriod As String
    Public Property SystemInfo As Dictionary(Of String, String)
    Public Property TimestampUtc As String
    Public Property RequestedBy As String
    Public Property RequestedFromIP As String
End Class

Public Class CursorResponsePayload
    Public Property FingerprintHash As String
    Public Property LicenseType As String
    Public Property NewEndDate As String
    Public Property IssuedOn As String
    Public Property IsBlocked As Boolean
    Public Property Notes As String
End Class
