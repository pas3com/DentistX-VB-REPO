Imports System.IO
Imports IntelliLock.LicenseManager
Imports IntelliLock.Licensing
Public Class LockLicense


    '=======================================
    '======Licensing



    'Check if a valid license file is available
    '''* Check if a valid license file is available. **
    Public Function IsValidLicenseAvailable() As Boolean
        Return (EvaluationMonitor.CurrentLicense.LicenseStatus = IntelliLock.Licensing.LicenseStatus.Licensed)
    End Function


    'Read license information from a license file 
    '''* Read additonal license information from a license file **
    Public Sub ReadAdditonalLicenseInformation()
        ' Check first if a valid license file is found 
        If EvaluationMonitor.CurrentLicense.LicenseStatus = IntelliLock.Licensing.LicenseStatus.Licensed Then
            ' Read additional license information 
            For i As Integer = 0 To EvaluationMonitor.CurrentLicense.LicenseInformation.Count - 1
                Dim key As String = EvaluationMonitor.CurrentLicense.LicenseInformation.GetKey(i).ToString()
                Dim value As String = EvaluationMonitor.CurrentLicense.LicenseInformation.GetByIndex(i).ToString()
            Next i
        End If
    End Sub


    'Check the license status of the Expiration Days Lock 
    '''* Check the license status of the Expiration Days Lock **
    Public Sub CheckExpirationDaysLock()
        Dim lock_enabled As Boolean = EvaluationMonitor.CurrentLicense.ExpirationDays_Enabled
        Dim days As Integer = EvaluationMonitor.CurrentLicense.ExpirationDays
        Dim days_current As Integer = EvaluationMonitor.CurrentLicense.ExpirationDays_Current
    End Sub


    'Check the license status of the Expiration Date Lock 
    '''* Check the license status of the Expiration Date Lock **
    Public Sub CheckExpirationDateLock()
        Dim lock_enabled As Boolean = EvaluationMonitor.CurrentLicense.ExpirationDate_Enabled
        Dim expiration_date As Date = EvaluationMonitor.CurrentLicense.ExpirationDate
    End Sub


    'Check the license status of the Executions Lock 
    '''* Check the license status of the Executions Lock **
    Public Sub CheckExecutionsLock()
        Dim lock_enabled As Boolean = EvaluationMonitor.CurrentLicense.Executions_Enabled
        Dim max_executions As Integer = EvaluationMonitor.CurrentLicense.Executions
        Dim current_executions As Integer = EvaluationMonitor.CurrentLicense.Executions_Current
    End Sub


    'Check the license status of the Instances Lock 
    '''* Check the license status of the Instances Lock **
    Public Sub CheckNumberOfInstancesLock()
        Dim lock_enabled As Boolean = EvaluationMonitor.CurrentLicense.Instances_Enabled
        Dim max_instances As Integer = EvaluationMonitor.CurrentLicense.Instances
    End Sub


    'Check the license status of Hardware Lock 
    '''* Check the license status of Hardware Lock **
    Public Sub CheckHardwareLock()
        Dim lock_enabled As Boolean = EvaluationMonitor.CurrentLicense.HardwareLock_Enabled

        If lock_enabled Then
            ' Get Hardware ID stored in the license file 
            Dim lic_hardware_id As String = EvaluationMonitor.CurrentLicense.HardwareID
        End If
    End Sub


    'Get Hardware ID of the current machine 
    '''* Get Hardware ID of the current machine **
    Public Function GetHardwareID() As String
        Return HardwareID.GetHardwareID(True, True, False, True, True, False)
    End Function


    'Compare current Hardware ID with Hardware ID stored in License File 
    '''* Compare current Hardware ID with Hardware ID stored in License File **
    Public Function CompareHardwareID() As Boolean
        If HardwareID.GetHardwareID(True, True, True, True, True, True) = EvaluationMonitor.CurrentLicense.HardwareID Then
            Return True
        Else
            Return False
        End If
    End Function


    'Invalidate the license 
    '''* Invalidate the license. Please note, your protected software does not accept a license file anymore! **
    Public Sub InvalidateLicense()
        Dim confirmation_code As String = License_DeActivator.DeactivateLicense()
    End Sub


    'Reactivate the license 
    '''* Reactivate an invalidated license. **
    Public Function ReactivateLicense(ByVal reactivation_code As String) As Boolean
        Return License_DeActivator.ReactivateLicense(reactivation_code)
    End Function


    'Manually load a license using a filename 
    '''* Load the license. **
    Public Sub LoadLicense(ByVal filename As String)
        EvaluationMonitor.LoadLicense(filename)
    End Sub


    'Manually load a license using byte[] 
    '''* Load the license. **
    Public Sub LoadLicense(ByVal license() As Byte)
        EvaluationMonitor.LoadLicense(license)

    End Sub


    'Get loaded license (if available) as byte[] 
    '''* Get the license. **
    Public Function GetLicense() As Byte()
        Return EvaluationMonitor.GetCurrentLicenseAsByteArray()
    End Function

End Class
'Check License Status asynchronously to prevent startup delays 
'* Check the license. **
Friend Class Program
        '' To automatically check the license asynchronously the option "Asynchronous License Check" must be enabled in IntelliLock
        'Shared Sub Main(ByVal args() As String)
        '    AddHandler IntelliLock.Licensing.EvaluationMonitor.LicenseCheckFinished, Sub()
        '                                                                                 Console.WriteLine(IntelliLock.Licensing.HardwareID.GetHardwareID(False, True, False, True, True, False))
        '                                                                                 Console.WriteLine(IntelliLock.Licensing.CurrentLicense.LicenseStatus.ToString())
        '                                                                             End Sub

        '    Console.ReadLine()
        'End Sub
    End Class

    'OR
    '''* Check the license. **
    Partial Friend Class Program
        ' To automatically check the license asynchronously the option "Asynchronous License Check" must be enabled in IntelliLock
        Shared Sub Main(ByVal args() As String)
            'Dim licenseFile As String = "C:\license.license"

            '' To ensure SDK method calls doesn't block/delay the control flow the SDK method LoadLicense(...) should be run in asynchronous context (new Action()..) as well
            'Call (New Action(Sub()
            '                     IntelliLock.Licensing.EvaluationMonitor.LoadLicense(File.ReadAllBytes(licenseFile))
            '                     AddHandler IntelliLock.Licensing.EvaluationMonitor.LicenseCheckFinished, Sub()
            '                                                                                                  Console.WriteLine(IntelliLock.Licensing.HardwareID.GetHardwareID(False, True, False, True, True, False))
            '                                                                                                  Console.WriteLine(CurrentLicense.LicenseStatus.ToString())
            '                                                                                              End Sub
            '                 End Sub)).BeginInvoke(Nothing, Nothing)

            'Console.WriteLine("Due to asynchronous control flow this line is displayed first")
            'Console.ReadLine()
        End Sub


        '=======================================
        '======LicenseManager
        ''' <summary>
        ''' Open IntelliLock Project
        ''' </summary>
        Public Function ReadProjectFile(ByVal proj_filename As String) As IntelliLock.LicenseManager.ProjectFile
            Return New IntelliLock.LicenseManager.ProjectFile(proj_filename)
        End Function

        ''' <summary>
        ''' Create License File
        ''' </summary>
        Public Function CreateLicenseFile(ByVal proj_filename As String) As Byte()
            Dim myproject As New IntelliLock.LicenseManager.ProjectFile(proj_filename)
            myproject.LicenseInformation.Add("MyKey", "MyValue")
            Return IntelliLock.LicenseManager.LicenseGenerator.CreateLicenseFile(myproject)
        End Function

        ''' <summary>
        ''' Create License File
        ''' </summary>
        Public Sub CreateLicenseFile(ByVal proj_filename As String, ByVal license_filename As String)
            Dim myproject As New IntelliLock.LicenseManager.ProjectFile(proj_filename)
            myproject.LicenseInformation.Add("MyKey", "MyValue")
            IntelliLock.LicenseManager.LicenseGenerator.CreateLicenseFile(myproject, license_filename)
        End Sub

    ''' <summary>
    ''' Create Hardware Locked License File
    ''' </summary>
    Public Sub CreateLicenseFile_HID(ByVal proj_filename As String, ByVal license_filename As String)
        Dim myproject As New IntelliLock.LicenseManager.ProjectFile(proj_filename)
        myproject.LicenseInformation.Add("MyKey", "MyValue")
        myproject.HardwareLock_Enabled = True
        myproject.HardwareLock_BIOS = True
        myproject.HardwareLock_CPU = True
        myproject.HardwareLock_HDD = True
        myproject.HardwareLock_MAC = True
        myproject.HardwareLock_Mainboard = True
        myproject.HardwareLock_OS = True
        myproject.Hardware_ID = "B745-C55C-1B94-6250-E98E-9BA0"
        IntelliLock.LicenseManager.LicenseGenerator.CreateLicenseFile(myproject, license_filename)
    End Sub


    ''' <summary>
    ''' Read License File
    ''' </summary>
    Public Function ReadLicenseFile(ByVal proj_filename As String, ByVal license_filename As String) As IntelliLock.LicenseManager.ProjectFile
            Dim myproject As New IntelliLock.LicenseManager.ProjectFile(proj_filename)
            Return myproject.ReadLicenseFile(license_filename)
        End Function

        ''' <summary>
        ''' Read License File
        ''' </summary>
        Public Function ReadLicenseFile(ByVal proj_filename As String, ByVal license() As Byte) As IntelliLock.LicenseManager.ProjectFile
            Dim myproject As New IntelliLock.LicenseManager.ProjectFile(proj_filename)
            Return myproject.ReadLicenseFile(license)
        End Function

    ''' <summary>
    ''' Generate Reactivation Code
    ''' </summary>
    Public Function GenerateReactivationCode(ByVal proj_filename As String, ByVal deactivation_code As String, ByVal hardware_id As String) As String
        'Dim myproject As New IntelliLock.LicenseManager.ProjectFile(proj_filename)

        ''use the same hardware components you used to deactivate the license
        'Return LicenseReactivator.GenerateReactivationCode(myproject, deactivation_code, hardware_id, True, True, True, True, True, True)
        Return String.Empty
        End Function

    ''' <summary>
    ''' Encrypt And Sign Data
    ''' </summary>
    Public Function EncryptAndSignData(ByVal proj_filename As String, ByVal mydata() As Byte) As Byte()
            Dim myproject As New IntelliLock.LicenseManager.ProjectFile(proj_filename)
            Return IntelliLock.LicenseManager.DataSignHelper.EncryptAndSignData(myproject, mydata)
        End Function

        ''' <summary>
        ''' Verify Signature
        ''' </summary>
        Public Function VerifySignature(ByVal proj_filename As String, ByVal mydata() As Byte) As Boolean
            Dim myproject As New IntelliLock.LicenseManager.ProjectFile(proj_filename)
            Return IntelliLock.LicenseManager.DataSignHelper.VerifySignature(myproject, mydata)
        End Function

        ''' <summary>
        ''' Decrypt Data
        ''' </summary>
        Public Function DecryptData(ByVal proj_filename As String, ByVal mydata() As Byte) As Byte()
            Dim myproject As New IntelliLock.LicenseManager.ProjectFile(proj_filename)
            If IntelliLock.LicenseManager.DataSignHelper.VerifySignature(myproject, mydata) Then
                Return IntelliLock.LicenseManager.DataSignHelper.DecryptData(myproject, mydata)
            Else
                Return New Byte() {}
            End If

        End Function

    End Class
