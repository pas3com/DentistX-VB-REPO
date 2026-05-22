Public Class LicenseResponse

    Public Property LicenseId As Guid
    Public Property CustomerId As Integer
    Public Property IssuedOn As DateTime
    Public Property ExpiresOn As DateTime

    Public Property PlanName As String
    Public Property MaxUsers As Integer

    Public Property Fingerprint As MachineFingerprint

End Class
