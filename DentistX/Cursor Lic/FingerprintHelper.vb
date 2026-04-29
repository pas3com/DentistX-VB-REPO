
Imports System.Management
Imports System.Net.NetworkInformation
Imports System.Security.Cryptography
Imports System.Text

Public Module FingerprintHelper
    Private Function SafeQueryWMI(query As String, prop As String) As String
        Try
            Using searcher = New ManagementObjectSearcher(query)
                For Each mo As ManagementObject In searcher.Get()
                    Dim v = mo(prop)
                    If v IsNot Nothing Then Return v.ToString().Trim()
                Next
            End Using
        Catch
        End Try
        Return String.Empty
    End Function

    Public Function GetCpuId() As String
        Dim v = SafeQueryWMI("SELECT ProcessorId FROM Win32_Processor", "ProcessorId")
        If String.IsNullOrEmpty(v) Then
            v = SafeQueryWMI("SELECT UniqueId FROM Win32_Processor", "UniqueId")
        End If
        Return v
    End Function

    Public Function GetMotherboardId() As String
        Return SafeQueryWMI("SELECT SerialNumber FROM Win32_BaseBoard", "SerialNumber")
    End Function

    Public Function GetDiskSerial() As String
        Return SafeQueryWMI("SELECT SerialNumber FROM Win32_PhysicalMedia", "SerialNumber")
    End Function

    Public Function GetMacAddress1() As String
        Try
            Using searcher = New ManagementObjectSearcher("SELECT MACAddress FROM Win32_NetworkAdapter WHERE MACAddress IS NOT NULL AND PhysicalAdapter = True")
                For Each mo As ManagementObject In searcher.Get()
                    Dim mac = mo("MACAddress")
                    If mac IsNot Nothing Then Return mac.ToString().Replace(":", "").Replace("-", "").Trim()
                Next
            End Using
        Catch
        End Try
        Return String.Empty
    End Function

    Public Function GenerateFingerprintRaw1() As String
        Dim parts As New List(Of String)
        parts.Add(GetCpuId())
        parts.Add(GetMotherboardId())
        parts.Add(GetDiskSerial())
        parts.Add(GetMacAddress())
        Dim raw = String.Join("|", parts.Where(Function(s) Not String.IsNullOrEmpty(s)).Select(Function(s) s.ToUpperInvariant()))
        If String.IsNullOrEmpty(raw) Then raw = Environment.MachineName.ToUpperInvariant()
        Return raw
    End Function

    Public Function GenerateFingerprintHash1() As String
        Return CryptoHelper.ComputeSHA256(GenerateFingerprintRaw())
    End Function


    ' ==================== ENHANCED FINGERPRINT GENERATION ====================

    Public Function GenerateFingerprintRaw() As String
        ' Collect ALL system information for comprehensive fingerprint
        Dim components As New List(Of String)

        ' 1. Processor ID (most stable)
        components.Add(GetProcessorId())

        ' 2. Baseboard/Motherboard ID
        components.Add(GetBaseboardId())

        ' 3. MAC Address (first physical adapter)
        components.Add(GetMacAddress())

        ' 4. Disk Serial
        components.Add(GetDiskId())

        ' 5. Windows User Info
        components.Add(GetWindowsUser())

        ' 6. Machine Name
        components.Add(Environment.MachineName)

        ' 7. OS Version
        components.Add(GetOSVersion())

        ' 8. RAM Size
        components.Add(GetTotalRAM())

        ' 9. Disk Size
        components.Add(GetDiskSize())

        ' 10. Processor Name
        components.Add(GetProcessorName())

        ' Join all components with pipe separator
        Return String.Join("|", components)
    End Function

    Public Function GenerateFingerprintHash() As String
        Dim raw = GenerateFingerprintRaw()
        Using sha256 As SHA256 = SHA256.Create()
            Dim bytes = Encoding.UTF8.GetBytes(raw)
            Dim hash = sha256.ComputeHash(bytes)
            Return BitConverter.ToString(hash).Replace("-", "").ToUpper()
        End Using
    End Function

    ' ==================== SYSTEM INFORMATION COLLECTION ====================

    Private Function GetProcessorId() As String
        Try
            Using searcher As New ManagementObjectSearcher("SELECT ProcessorId FROM Win32_Processor")
                For Each obj As ManagementObject In searcher.Get()
                    If obj("ProcessorId") IsNot Nothing Then
                        Return obj("ProcessorId").ToString().Trim()
                    End If
                Next
            End Using
        Catch
        End Try
        Return "UNKNOWN_PROCESSOR"
    End Function

    Private Function GetBaseboardId() As String
        Try
            Using searcher As New ManagementObjectSearcher("SELECT SerialNumber FROM Win32_BaseBoard")
                For Each obj As ManagementObject In searcher.Get()
                    If obj("SerialNumber") IsNot Nothing Then
                        Return obj("SerialNumber").ToString().Trim()
                    End If
                Next
            End Using
        Catch
        End Try
        Return "UNKNOWN_BASEBOARD"
    End Function

    Private Function GetMacAddress() As String
        Try
            ' Get first physical network adapter MAC
            For Each nic As NetworkInterface In NetworkInterface.GetAllNetworkInterfaces()
                If nic.NetworkInterfaceType <> NetworkInterfaceType.Loopback AndAlso
                   nic.OperationalStatus = OperationalStatus.Up AndAlso
                   Not String.IsNullOrEmpty(nic.GetPhysicalAddress().ToString()) Then

                    Return nic.GetPhysicalAddress().ToString()
                End If
            Next
        Catch
        End Try
        Return "UNKNOWN_MAC"
    End Function

    Private Function GetDiskId() As String
        Try
            Using searcher As New ManagementObjectSearcher("SELECT SerialNumber FROM Win32_DiskDrive WHERE Index = 0")
                For Each obj As ManagementObject In searcher.Get()
                    If obj("SerialNumber") IsNot Nothing Then
                        Return obj("SerialNumber").ToString().Trim()
                    End If
                Next
            End Using
        Catch
        End Try
        Return "UNKNOWN_DISK"
    End Function

    Private Function GetWindowsUser() As String
        Return Environment.UserName
    End Function

    Private Function GetUserDomain() As String
        Return Environment.UserDomainName
    End Function

    Private Function GetOSVersion() As String
        Return $"{Environment.OSVersion.Version.Major}.{Environment.OSVersion.Version.Minor}"
    End Function

    Private Function GetTotalRAM() As String
        Try
            Using searcher As New ManagementObjectSearcher("SELECT Capacity FROM Win32_PhysicalMemory")
                Dim totalBytes As ULong = 0
                For Each obj As ManagementObject In searcher.Get()
                    totalBytes += Convert.ToUInt64(obj("Capacity"))
                Next
                Dim totalGB = totalBytes / (1024 * 1024 * 1024)
                Return $"{totalGB:F1}GB"
            End Using
        Catch
        End Try
        Return "UNKNOWN_RAM"
    End Function

    Private Function GetDiskSize() As String
        Try
            Using searcher As New ManagementObjectSearcher("SELECT Size FROM Win32_DiskDrive WHERE Index = 0")
                For Each obj As ManagementObject In searcher.Get()
                    If obj("Size") IsNot Nothing Then
                        Dim sizeBytes = Convert.ToUInt64(obj("Size"))
                        Dim sizeGB = sizeBytes / (1024 * 1024 * 1024)
                        Return $"{sizeGB:F1}GB"
                    End If
                Next
            End Using
        Catch
        End Try
        Return "UNKNOWN_DISK_SIZE"
    End Function

    Private Function GetProcessorName() As String
        Try
            Using searcher As New ManagementObjectSearcher("SELECT Name FROM Win32_Processor")
                For Each obj As ManagementObject In searcher.Get()
                    If obj("Name") IsNot Nothing Then
                        Return obj("Name").ToString().Trim()
                    End If
                Next
            End Using
        Catch
        End Try
        Return "UNKNOWN_CPU"
    End Function

    ' ==================== COMPREHENSIVE SYSTEM INFO DICTIONARY ====================

    Public Function GetSystemInfoDictionary() As Dictionary(Of String, String)
        Dim info As New Dictionary(Of String, String)

        info.Add("ProcessorId", GetProcessorId())
        info.Add("BaseboardId", GetBaseboardId())
        info.Add("MacAddress", GetMacAddress())
        info.Add("DiskId", GetDiskId())
        info.Add("WindowsUser", GetWindowsUser())
        info.Add("UserDomain", GetUserDomain())
        info.Add("MachineName", Environment.MachineName)
        info.Add("OSVersion", GetOSVersion())
        info.Add("TotalRAM", GetTotalRAM())
        info.Add("DiskSize", GetDiskSize())
        info.Add("ProcessorName", GetProcessorName())
        info.Add("OSName", GetOSName())
        info.Add("NumberOfCores", GetNumberOfCores())
        info.Add("SystemManufacturer", GetSystemManufacturer())
        info.Add("BIOSSerial", GetBIOSSerial())

        Return info
    End Function

    ' Additional info methods
    Private Function GetOSName() As String
        Return Environment.OSVersion.ToString()
    End Function

    Private Function GetNumberOfCores() As String
        Try
            Using searcher As New ManagementObjectSearcher("SELECT NumberOfCores FROM Win32_Processor")
                For Each obj As ManagementObject In searcher.Get()
                    If obj("NumberOfCores") IsNot Nothing Then
                        Return obj("NumberOfCores").ToString()
                    End If
                Next
            End Using
        Catch
        End Try
        Return "UNKNOWN"
    End Function

    Private Function GetSystemManufacturer() As String
        Try
            Using searcher As New ManagementObjectSearcher("SELECT Manufacturer FROM Win32_ComputerSystem")
                For Each obj As ManagementObject In searcher.Get()
                    If obj("Manufacturer") IsNot Nothing Then
                        Return obj("Manufacturer").ToString()
                    End If
                Next
            End Using
        Catch
        End Try
        Return "UNKNOWN"
    End Function

    Private Function GetBIOSSerial() As String
        Try
            Using searcher As New ManagementObjectSearcher("SELECT SerialNumber FROM Win32_BIOS")
                For Each obj As ManagementObject In searcher.Get()
                    If obj("SerialNumber") IsNot Nothing Then
                        Return obj("SerialNumber").ToString()
                    End If
                Next
            End Using
        Catch
        End Try
        Return "UNKNOWN"
    End Function
End Module
