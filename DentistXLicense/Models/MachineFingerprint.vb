Public Class MachineFingerprint

    ' --- CORE ---
    Public Property CpuId As String
    Public Property MotherboardSerial As String
    Public Property MachineSid As String

    ' --- SOFT ---
    Public Property DiskSerial As String
    Public Property MacAddresses As List(Of String)
    Public Property BiosVersion As String
    Public Property OsInstallDate As String
    Public Property HostName As String

End Class

