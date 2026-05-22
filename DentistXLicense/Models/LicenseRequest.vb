Public Class LicenseRequest

    ' --- Business identity ---
    Public Property DoctorName As String
    Public Property ClinicName As String

    ' --- Upgrade intent ---
    Public Property Plan As String
    Public Property PeriodMonths As Integer

    ' --- Machine binding ---
    Public Property Fingerprint As MachineFingerprint

    ' --- Diagnostics ---
    Public Property MachineName As String
    Public Property RequestDateUtc As DateTime
    Public Property AppVersion As String

End Class
