Imports System.Management
Imports System.Security.Principal
Imports DentistXLicense
'Imports DentistXLicense.Models
Imports Microsoft.Win32

Public Module FingerprintProvider

    Public Function GetCurrentFingerprint() As MachineFingerprint
        Return New MachineFingerprint With {
            .CpuId = GetCpuId(),
            .MotherboardSerial = GetMotherboardSerial(),
            .MachineSid = GetMachineSid(),
            .DiskSerial = GetSystemDiskSerial(),
            .MacAddresses = GetMacAddresses(),
            .BiosVersion = GetBiosVersion(),
            .OsInstallDate = GetOsInstallDate(),
            .HostName = Environment.MachineName
        }
    End Function

    ' ---------------- CORE ----------------

    Private Function GetCpuId() As String
        Try
            Using mos As New ManagementObjectSearcher("SELECT ProcessorId FROM Win32_Processor")
                For Each mo As ManagementObject In mos.Get()
                    Return mo("ProcessorId")?.ToString()
                Next
            End Using
        Catch
        End Try
        Return String.Empty
    End Function

    Private Function GetMotherboardSerial() As String
        Try
            Using mos As New ManagementObjectSearcher("SELECT SerialNumber FROM Win32_BaseBoard")
                For Each mo As ManagementObject In mos.Get()
                    Return mo("SerialNumber")?.ToString()
                Next
            End Using
        Catch
        End Try
        Return String.Empty
    End Function

    Private Function GetMachineSid() As String
        Try
            Dim sid = WindowsIdentity.GetCurrent().User?.AccountDomainSid
            Return sid?.Value
        Catch
        End Try
        Return String.Empty
    End Function

    ' ---------------- SOFT ----------------

    Private Function GetSystemDiskSerial() As String
        Try
            Using mos As New ManagementObjectSearcher(
                "SELECT VolumeSerialNumber FROM Win32_LogicalDisk WHERE DeviceID='C:'")
                For Each mo As ManagementObject In mos.Get()
                    Return mo("VolumeSerialNumber")?.ToString()
                Next
            End Using
        Catch
        End Try
        Return String.Empty
    End Function

    Private Function GetMacAddresses() As List(Of String)
        Dim list As New List(Of String)
        Try
            Using mos As New ManagementObjectSearcher(
                "SELECT MACAddress FROM Win32_NetworkAdapter WHERE MACAddress IS NOT NULL")
                For Each mo As ManagementObject In mos.Get()
                    list.Add(mo("MACAddress").ToString())
                Next
            End Using
        Catch
        End Try
        Return list
    End Function

    Private Function GetBiosVersion() As String
        Try
            Using mos As New ManagementObjectSearcher("SELECT SMBIOSBIOSVersion FROM Win32_BIOS")
                For Each mo As ManagementObject In mos.Get()
                    Return mo("SMBIOSBIOSVersion")?.ToString()
                Next
            End Using
        Catch
        End Try
        Return String.Empty
    End Function

    Private Function GetOsInstallDate() As String
        Try
            Using key = Registry.LocalMachine.OpenSubKey(
                "SOFTWARE\Microsoft\Windows NT\CurrentVersion")
                Dim val = key?.GetValue("InstallDate")
                If val IsNot Nothing Then
                    Dim seconds = Convert.ToInt64(val)
                    Return DateTimeOffset.FromUnixTimeSeconds(seconds).UtcDateTime.ToString("yyyyMMdd")
                End If
            End Using
        Catch
        End Try
        Return String.Empty
    End Function

End Module
