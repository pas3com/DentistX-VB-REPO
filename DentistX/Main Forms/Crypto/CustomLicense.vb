Imports System.IO
Imports System.Management
Imports System.Reflection
Imports System.Text
Imports System.Windows.Forms
'Imports LogicNP.CryptoLicensing

'Public Class CustomLicense
'    Inherits CryptoLicense

'    '=========Gathering Code====================
'    ' Sample features that your software might have
'    Enum MyFeatures
'        Printing = 1 ' feature 1
'        Saving   ' feature 2
'        WebAccess ' feature 3
'        ComplexAlgorithm  ' feature 4
'    End Enum

'    Dim MsgStr As String = ""
'    '<STAThread()> Overloads Shared Sub Main(ByVal args() As String)
'    '    Application.EnableVisualStyles()
'    '    Application.SetCompatibleTextRenderingDefault(False)
'    '    Application.Run(New Form1())

'    'End Sub

'    Private Function CreateLicense() As CryptoLicense
'        Dim ret As New CryptoLicense()

'        'Uses the validation key of the "samples.netlicproj" license project file from the "Samples" directory
'        ' Get the validation key using Ctrl+K in the Generator UI.
'        ret.ValidationKey = "AMAAMACsxf8UhS19Bp7LtTIGtvhAeRRGnu9hsQ533HcMzci5KZkfHi7fUNykMfe7FfiPhoUDAAEAAQ==" '"AMAAMACAan6T73Ctw/rY+L9u9uwHqu7uns3owx7c1/oqgVhKLo+dFEG34875h3IWCcNU8e0DAAEAAQ=="

'        ' *** IMPORTANT: Set the LicenseServiceURL property below to the URL of your license service
'        ' See video tutorial at http://www.ssware.com/cryptolicensing/demo/license_service_net.htm to learn 
'        ' how to create the license service
'        ret.LicenseServiceURL = ""
'        ' your license service URL here!
'        Return ret
'    End Function

'    Private Sub ShowLicenseInvalidMessage(ByVal license As CryptoLicense)
'        MessageBox.Show("Invalid license. " + vbLf & vbLf + license.GetAllStatusExceptionsAsString(), MsgStr) ' Me.Text)
'    End Sub

'    Private Sub ShowSerialInvalidMessage(ByVal license As CryptoLicense)
'        MessageBox.Show("Serial Validation Failed." & vbLf & vbLf + license.GetAllStatusExceptionsAsString(), MsgStr) ' Me.Text)
'    End Sub

'    'Evaluation Scenario
'    'Private Sub btnEvalScenario_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) ' Handles btnEvalScenario.Click
'    Public Sub EvaluateLicense() ' Handles btnEvalScenario.Click

'        'This code demonstrates typically evaluation license scenario. The idea is as follows....
'        'First you check if a full license is present (using CryptoLicense.Load method). 
'        'If .Load returns false, you switch to a hardcoded evaluation license code (using .LicenseCode property). 
'        'Then, you validate using .Status property.

'        'Even if user uninstalls, all the usage data associated which each license code remains in the 
'        'registry and if user reinstalls, the evaluation continues from where it left off.


'        Dim license As CryptoLicense = CreateLicense()

'        ' The license will be loaded from/saved to the registry
'        license.StorageMode = LicenseStorageMode.ToRegistry

'        ' To avoid conflicts with other scenarios from this sample, the default load/save registry key is changed
'        license.RegistryStoragePath = license.RegistryStoragePath + "EvalLicense"

'        ' The remove method can be useful during development and testing - it deletes a previously saved license.
'        ' license.Remove();

'        ' Another useful method during development and testing is .ResetEvaluationInfo()

'        ' Load the license from the registry 
'        If (license.Load() = False) Then
'            ' When app runs for first time, the load will fail, so specify an evaluation code....
'            ' This license code was generated from the Generator UI with a "Limit Usage Days To" setting of 30 days.
'            license.LicenseCode = "FggAAF7EtnAnutgBCgCbJ+9ok45HyRrzrzQsa7cKUsX6w/DEF5I/DrOFfZ1e8Ah0W0NmF2z382E2NV4YaMM=" ' "FgIAAIyFU90EMssBHgAVRDPpC/iphuewU7SaMypOAIiukaj4JNCNaLYfxj5XbwyrBWHgFk/49NyCr9K3adQ="

'            ' Save it so that it will get loaded the next time app runs
'            license.Save()
'        End If

'        ' ShowEvaluationInfoDialog shows the dialog only if the license specifies evaluation limits 
'        If (license.ShowEvaluationInfoDialog(My.Application.Info.Title, "http://www.ssware.com") = False) Then
'            ' license has expired, new license entered is also expired 
'            ' or user choose the 'Exit Program' option 
'            MessageBox.Show("Your evaluation has expired", MsgStr) ' Me.Text)

'            ' In your app, you may wish to exit app when eval license has expired
'            ' Application.Exit()
'        Else
'            ' The current license is valid or the new license entered is valid 
'            ' or the user choose the 'Continue Evaluation' option 

'            ' If the user enters a new valid license code, it replaces the existing code  
'            ' and is automatically saved to the currently specified  
'            ' storage medium (registry in this sample) using the CryptoLicense.Save method.  
'            ' The new license code is thus available the next time your software runs. 

'            ' We still need to check whether the license is an evaluation license 
'            If (license.IsEvaluationLicense() = True) Then
'                ' reduce functionality in evaluation version if so desired 
'            End If
'        End If

'        license.Dispose() ' Be sure to call Dispose during app exit or when done using the CryptoLicense object

'    End Sub


'    Private Sub btnAccessingUserData_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) 'Handles btnAccessingUserData.Click
'        Dim license As CryptoLicense = CreateLicense()

'        ' Following license code was generated from CryptoLicensing Generator UI with 
'        ' some userdata specified using  the "Data Fields" userdata provider
'        license.LicenseCode = "lgAAACjgnMf9McsBIABOYW1lPUpvaG4gRG9lI0VtYWlsPWpvaG5AZG9lLmNvbQ1fW7nTLJ7gDXPodwdi30pWw/+4EdvTxKGP7otMFDICnbo9T5PYM58759HyoUDzMA=="

'        ' Validate license...
'        If (license.Status <> LicenseStatus.Valid) Then
'            ShowLicenseInvalidMessage(license)
'        Else
'            MessageBox.Show("License valid. Embedded userdata : " + license.UserData, MsgStr) ' Me.Text)

'            'if using the "Data Fields" user-data provider, you can also get field values as follows:
'            Dim name As String = license.GetUserDataFieldValue("Name", "#")
'            Dim email As String = license.GetUserDataFieldValue("Email", "#")

'        End If

'        license.Dispose() ' Be sure to call Dispose during app exit or when done using the CryptoLicense object

'    End Sub

'    Private Sub btnTestingFeatures_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) ' Handles btnTestingFeatures.Click
'        Dim license As CryptoLicense = CreateLicense()

'        ' Following license code was generated from CryptoLicensing Generator UI with Feature 2 and 4 checked.
'        license.LicenseCode = "FgCAAFCX5c33McsBAQoLlw6KdwNcP9y7DQ97eIg+SNBIvu/hRw+TtvkKBGJ4Tl9GVrqkhydoTFI9QSIM7k4="

'        ' Validate license...
'        If (license.Status <> LicenseStatus.Valid) Then
'            ShowLicenseInvalidMessage(license)
'        Else

'            ' Determine which features are enabled....
'            Dim allowPrinting As Boolean = license.IsFeaturePresentEx(MyFeatures.Printing)
'            Dim allowSaving As Boolean = license.IsFeaturePresentEx(MyFeatures.Saving)
'            Dim allowWebAccess As Boolean = license.IsFeaturePresentEx(MyFeatures.WebAccess)
'            Dim allowComplexAlgorithm As Boolean = license.IsFeaturePresentEx(MyFeatures.ComplexAlgorithm)

'            MessageBox.Show("License valid. Embedded features are as follows:" + Chr(10) +
'                    "Allow Prinring = " + allowPrinting.ToString() + Chr(10) +
'                    "Allow Saving = " + allowSaving.ToString() + Chr(10) +
'                    "Allow WebAccess = " + allowWebAccess.ToString() + Chr(10) +
'                    "Allow Complex Algorithm = " + allowComplexAlgorithm.ToString(),
'                   MsgStr) ' Me.Text)
'        End If

'        license.Dispose() ' Be sure to call Dispose during app exit or when done using the CryptoLicense object

'    End Sub

'    'Private Sub btnUsingActivatedLicenses_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) 'Handles btnUsingActivatedLicenses.Click
'    Public Sub UsingActivatedLicenses()
'        Dim license As CryptoLicense = CreateLicense()

'        ' Following license code was generated from CryptoLicensing Generator UI with..
'        ' "Activation: Limit Machines To" set to 3.
'        license.LicenseCode = "FggAAF7EtnAnutgBCgCbJ+9ok45HyRrzrzQsa7cKUsX6w/DEF5I/DrOFfZ1e8Ah0W0NmF2z382E2NV4YaMM=" ' "FgEAAMCGYaoDMssBAwAhEaF+3Swz0apIAhiokeZQD61Ywz772ClingeYt9vNNPsm5PMZxaeEoINcQWQINRo="

'        Dim maxActivations As Integer = license.MaxActivations

'        ' Validate license...
'        If (license.Status <> LicenseStatus.Valid) Then

'            ShowLicenseInvalidMessage(license)

'        Else

'            ' Activated successfully...
'            ' The license service will have sent back a new license code which is machine-locked to current machine
'            Dim currentActivationCount As Integer = license.GetCurrentActivationCount()
'            MessageBox.Show("License valid and activated.\nCurrent total activations = " + currentActivationCount.ToString() _
'                + "\nRemaining Activations = " + (maxActivations - currentActivationCount).ToString(), MsgStr) ' Me.Text)

'        End If

'        license.Dispose() ' Be sure to call Dispose during app exit or when done using the CryptoLicense object

'    End Sub

'    Private Sub btnUsingHostAssembly_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) ' Handles btnUsingHostAssembly.Click
'        Dim license As CryptoLicense = CreateLicense()

'        ' Following license code was generated from CryptoLicensing Generator UI with 
'        ' 1. Version locked to 1.0.0.0 via the "License Settings (.Net Specified)" tab
'        ' 2. "Strong Name Verification" checked
'        license.LicenseCode = "FggAAF7EtnAnutgBCgCbJ+9ok45HyRrzrzQsa7cKUsX6w/DEF5I/DrOFfZ1e8Ah0W0NmF2z382E2NV4YaMM=" ' "FgAYAHLnVRUAMssBHgEAAAAAAAAAAAAAAAAAAABQWqyfK3NYEDYxQYx8fBYXj9UXLtqguKj3eZqlXV0vcPDzC5KxeJ9lBK1EV5w3/HQ="

'        ' Set host assembly against which the version settings specified in the license code above will be matched
'        license.HostAssembly = Assembly.GetExecutingAssembly()

'        ' To test, you can change the version specified in the AssemblyInfo.cs file
'        ' Or you can specify NOT to sign the assembly via Project Properties.

'        ' Validate license...
'        If (license.Status <> LicenseStatus.Valid) Then

'            ShowLicenseInvalidMessage(license)

'        Else

'            MessageBox.Show("License valid, version and strong name matches.", MsgStr) ' Me.Text)

'        End If

'        license.Dispose() ' Be sure to call Dispose during app exit or when done using the CryptoLicense object

'    End Sub


'    Private Sub btnValidateSerial_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) 'Handles btnValidateSerial.Click
'        ' Following serial code was generated from CryptoLicensing Generator UI with the 'Normal License' profile
'        Dim serial As String = "H2TMR-N56MH-S4CU4-TLQN9-5U4C7-QD437-U"

'        Dim license As CryptoLicense = CreateLicense()

'        ' The license will be loaded from/saved to the registry
'        license.StorageMode = LicenseStorageMode.ToRegistry

'        ' To avoid conflicts with other scenarios from this sample, the default load/save registry key is changed
'        license.RegistryStoragePath = license.RegistryStoragePath + "ValidateSerial"

'        ' Try to load a previously saved license code
'        If license.Load = False Then

'            ' A previous-saved license was not found, which probably means we are running for the first time
'            ' So validate the serial to get the license...

'            ' Try to validate serial and retrieve license against the serial
'            Dim result As SerialValidationResult = license.GetLicenseFromSerial(serial, "<<user-data>>")
'            If result = SerialValidationResult.Failed Then
'                ' 'serial' is in serial form but validation of serial failed
'                ShowSerialInvalidMessage(license)
'                Return
'            ElseIf result = SerialValidationResult.NotASerial Then
'                ' 'serial' is not a serial
'                MessageBox.Show("Not a serial code.")
'                Return
'            ElseIf result = SerialValidationResult.Success Then

'                '    'CryptoLicense.GetLicenseFromSerial automatically set the lic.LicenseCode with the retrieved license code
'                ' Save that license code so that .Load loads it next time and avoids serial validation via license service everytime app starts
'                license.Save()

'                ' validate it...
'            End If

'        End If

'        ' Validate license by querying Status property
'        If license.Status <> LicenseStatus.Valid Then
'            ShowLicenseInvalidMessage(license)
'        Else
'            MessageBox.Show("License Validation Succeeded")
'        End If

'        license.Dispose() ' Be sure to call Dispose during app exit or when done using the CryptoLicense object

'    End Sub


'    '*** See the topic "Creating Software Subscription Licenses" in the help file for corresponding server-side code and more information.
'    Private Sub btnSubscriptionLicenses_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) ' Handles btnSubscriptionLicenses.Click
'        Dim license As CryptoLicense = CreateLicense()

'        license.StorageMode = LicenseStorageMode.ToRegistry

'        ' The license will be loaded from/saved to the registry
'        license.StorageMode = LicenseStorageMode.ToRegistry

'        ' To avoid conflicts with other scenarios from this sample, the default load/save registry key is changed
'        license.RegistryStoragePath = license.RegistryStoragePath + "SubscriptionLicenses"

'        ' See if a previously saved license is already present 
'        If license.Load() = False Then
'            ' Your ACTUAL implementation should ask the user to input the license code you sent the user when he made the initial purchase.

'            ' This SAMPLE implementation simply gets a new 30-day license from the license service....
'            MessageBox.Show("License not found or running for the first time. Getting a new 30-day license from the license service...")

'            ' The following assumes that a profile named "30_Day_License" is defined in your license project file 
'            If license.GetLicenseFromProfile("30_Day_License", "{any user-data to be embedded in license}") = False Then
'                ' Handle failure as per your needs....
'                ShowError("Could not get a new 30-day license", license.GetStatusException(LicenseStatus.NotValidated))
'                Return
'            End If

'            MessageBox.Show("Got a new 30-day license from the license service")

'            ' Save license to registry (to default location derived from license) 
'            ' This saved license will be used next time your software runs 
'            license.Save()
'        End If


'        ' Check license status....

'        Dim status As LicenseStatus = license.Status
'        If status <> LicenseStatus.Valid Then
'            If status = LicenseStatus.Expired Then
'                ' 30 days have passed
'                MessageBox.Show("License has expired. Renewing license via the license service...")
'                If Not license.UpdateLicense(Nothing) Then
'                    ' Handle failure as per your needs....
'                    ShowError("Renewal of license failed", license.GetStatusException(LicenseStatus.NotValidated))
'                Else
'                    ' Save license to registry (to default location derived from license) 
'                    ' This saved license will be used next time your software runs 
'                    license.Save()

'                    MessageBox.Show("Renewal of license successful. New license expiring on " + license.DateExpires.ToString())
'                End If
'            Else
'                ' Some other validation failure...
'                ShowLicenseInvalidMessage(license)
'            End If
'        Else
'            ' License Validation successful...
'            MessageBox.Show("License valid. Expiring on " + license.DateExpires.ToString())
'        End If

'        license.Dispose() ' Be sure to call Dispose during app exit or when done using the CryptoLicense object

'    End Sub


'    Private Sub ShowError(ByVal message As String, ByVal ex As Exception)
'        If ex Is Nothing Then
'            MessageBox.Show(message)
'        Else
'            MessageBox.Show(message + " - " + ex.Message)
'        End If
'    End Sub
'    '====================================
'    ' Override to provide custom machine code
'    ' This example uses the USB Serial ID as the machine code
'    Public Overrides Function GetLocalMachineCode() As Byte()
'        Dim ret() As Byte = Nothing
'        Try
'            ' Assumes application is running from the USB drive  
'            Dim str As String = System.Reflection.Assembly.GetExecutingAssembly().Location
'            str = Path.GetPathRoot(str)
'            Dim serialID As String = GetVolumeSerial(str)
'            If serialID IsNot Nothing AndAlso serialID.Length > 0 Then
'                ret = Encoding.UTF8.GetBytes(serialID)
'            End If
'        Catch
'        End Try

'        ' Fall back to base implementation if failed
'        If ret Is Nothing OrElse ret.Length = 0 Then
'            ret = MyBase.GetLocalMachineCode()
'        End If

'        Return ret
'    End Function

'    ' Gets the Serial ID of a USB Portable Drive
'    Public Shared Function GetVolumeSerial(ByVal strDriveLetter As String) As String
'        strDriveLetter = strDriveLetter.TrimEnd("\"c)
'        Dim disk As New ManagementObject("win32_logicaldisk.deviceid=""" & strDriveLetter & """")
'        disk.Get()
'        Return disk("VolumeSerialNumber").ToString()
'    End Function

'End Class
