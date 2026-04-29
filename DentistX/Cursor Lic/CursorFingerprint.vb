Imports System.Management
Imports System.Security.Cryptography
Imports System.Text

Public Module CursorFingerprint
    Public Function GetFingerprintRaw() As String
        Dim cpu = SafeQueryWmi("SELECT ProcessorId FROM Win32_Processor", "ProcessorId")
        Dim bios = SafeQueryWmi("SELECT SerialNumber FROM Win32_BIOS", "SerialNumber")
        Dim name = Environment.MachineName
        Dim raw = $"{cpu}|{bios}|{name}"
        If String.IsNullOrWhiteSpace(raw) Then
            raw = name
        End If
        Return raw
    End Function

    Public Function GetFingerprintHash(raw As String) As String
        Using sha256 As SHA256 = SHA256.Create()
            Dim bytes = Encoding.UTF8.GetBytes(raw)
            Dim hash = sha256.ComputeHash(bytes)
            Return BitConverter.ToString(hash).Replace("-", "").ToUpperInvariant()
        End Using
    End Function

    Private Function SafeQueryWmi(query As String, prop As String) As String
        Try
            Using searcher As New ManagementObjectSearcher(query)
                For Each obj As ManagementObject In searcher.Get()
                    Dim v = obj(prop)
                    If v IsNot Nothing Then
                        Return v.ToString().Trim()
                    End If
                Next
            End Using
        Catch
        End Try
        Return ""
    End Function
End Module
