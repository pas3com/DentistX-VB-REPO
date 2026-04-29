Imports System.Diagnostics
Imports System.Globalization
Imports System.IO
Imports System.Net
Imports System.Net.Sockets
Imports System.Text
Imports System.Windows.Forms
Imports Microsoft.VisualBasic
Imports Newtonsoft.Json

Public Module CursorLicManager
    Private Const TrialMonths As Integer = 3
    Private Const DecoyMarker As String = "DX-DECOY"
    Private _limitedMode As Boolean = False

    Public ReadOnly Property IsLimitedMode As Boolean
        Get
            Return _limitedMode
        End Get
    End Property

    Public Function Initialize() As Boolean
        Try
            Dim dataDir = CursorLicConfig.GetDataDir()
            Directory.CreateDirectory(dataDir)

            Dim fingerprintRaw = CursorFingerprint.GetFingerprintRaw()
            Dim fingerprintHash = CursorFingerprint.GetFingerprintHash(fingerprintRaw)
            Dim nowDate = GetTrustedDate()

            Dim state = CursorLicStore.LoadTrialState()
            Dim hasDbRow = CursorPcTrialsRepository.HasTrialRow(fingerprintHash)
            Dim decoyExists = File.Exists(CursorLicConfig.GetDecoyLicenseFile())

            If state Is Nothing Then
                If hasDbRow OrElse decoyExists Then
                    Return ShowLocalTrialDataMissingFatal(hasDbRow, decoyExists)
                End If

                state = CreateInitialState(fingerprintHash, nowDate)
                CursorLicStore.SaveTrialState(state)
                If Not CursorPcTrialsRepository.UpsertTrialState(state, fingerprintRaw, "System") Then
                    WarnAndLimit("Failed to write license data to PC_Trials.")
                End If
                WriteDecoyLicense(state, nowDate)
                Return True
            End If

            If Not EnsureDecoyLicenseWithRecovery(state, fingerprintHash, nowDate) Then
                Return False
            End If

            If Not ValidateState(state, fingerprintHash, nowDate) Then
                Return False
            End If

            state.LastCheckDate = nowDate
            CursorLicStore.SaveTrialState(state)
            If Not CursorPcTrialsRepository.UpsertTrialState(state, fingerprintRaw, "System") Then
                WarnAndLimit("Failed to update license data in PC_Trials.")
            End If

            WriteDecoyLicense(state, nowDate)

            Return True
        Catch ex As Exception
            CursorLicLogger.Error("Initialize failed: " & ex.Message)
            Return HardFail("License validation failed. " & ex.Message)
        End Try
    End Function

    ''' <summary>
    ''' Programmer/support only: clears WinCache trial.dat, registry Trial values, PC_Trials row, DentistX.lic, then applies a new 3‑month trial and restarts the app.
    ''' Requires App.config LicenseDevResetEnabled=true and LicenseDevResetSecret. Trigger from LicenseRecoveryForm via Ctrl+Shift+Alt+F12.
    ''' </summary>
    Public Function TryDeveloperHotkeyResetTrial(owner As IWin32Window) As Boolean
        If Not CursorLicConfig.IsLicenseDevResetEnabled() Then
            Return False
        End If

        Dim configuredSecret = CursorLicConfig.GetLicenseDevResetSecret()
        If String.IsNullOrWhiteSpace(configuredSecret) Then
            MessageBox.Show(owner,
                "LicenseDevResetEnabled is set but LicenseDevResetSecret is missing or empty in App.config.",
                "License dev reset",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning)
            Return False
        End If

        Dim entered = Interaction.InputBox(
            "Enter the developer reset secret (App.config LicenseDevResetSecret):",
            "License — developer reset",
            "")

        If String.IsNullOrEmpty(entered) Then
            Return False
        End If

        If Not FixedTimeStringEqual(entered.Trim(), configuredSecret.Trim()) Then
            MessageBox.Show(owner, "Incorrect secret.", "License dev reset", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End If

        Dim confirm = MessageBox.Show(owner,
            "This will remove local trial files (WinCache / legacy), registry Trial values, the DentistX.lic file, and the PC_Trials row for this PC, then start a new 3‑month trial." & Environment.NewLine & Environment.NewLine &
            "Continue?",
            "Confirm developer trial reset",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Warning,
            MessageBoxDefaultButton.Button2)
        If confirm <> DialogResult.Yes Then
            Return False
        End If

        Try
            Dim fingerprintRaw = CursorFingerprint.GetFingerprintRaw()
            Dim fingerprintHash = CursorFingerprint.GetFingerprintHash(fingerprintRaw)

            CursorLicStore.ClearAllLocalTrialStorage()

            Try
                Dim licPath = CursorLicConfig.GetDecoyLicenseFile()
                If File.Exists(licPath) Then File.Delete(licPath)
            Catch
            End Try

            If Not CursorPcTrialsRepository.DeleteTrialRowForFingerprint(fingerprintHash) Then
                MessageBox.Show(owner,
                    "Could not delete the PC_Trials row. Check the database connection and permissions.",
                    "License dev reset",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error)
                Return False
            End If

            Dim nowDate = GetTrustedDate()
            Dim state = CreateInitialState(fingerprintHash, nowDate)
            CursorLicStore.SaveTrialState(state)
            If Not CursorPcTrialsRepository.UpsertTrialState(state, fingerprintRaw, "DevHotkeyReset") Then
                MessageBox.Show(owner,
                    "Trial files were reset but updating PC_Trials failed. Fix SQL access and restart, or run reset again.",
                    "License dev reset",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning)
            End If
            WriteDecoyLicense(state, nowDate)

            CursorLicLogger.Warn($"Developer hotkey trial reset completed for FingerprintHash={fingerprintHash}")

            MessageBox.Show(owner,
                "A new 3‑month trial is in place. The application will restart.",
                "License dev reset",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information)

            Dim psi As New ProcessStartInfo(Application.ExecutablePath) With {.UseShellExecute = True}
            Process.Start(psi)
            Application.Exit()
            Return True
        Catch ex As Exception
            CursorLicLogger.Error("Developer trial reset failed: " & ex.Message)
            MessageBox.Show(owner, "Reset failed: " & ex.Message, "License dev reset", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End Try
    End Function

    Private Function FixedTimeStringEqual(a As String, b As String) As Boolean
        If ReferenceEquals(a, b) Then Return True
        Dim aa = If(a, "")
        Dim bb = If(b, "")
        If aa.Length <> bb.Length Then Return False
        Dim accum = 0
        For i = 0 To aa.Length - 1
            accum = accum Or (AscW(aa(i)) Xor AscW(bb(i)))
        Next
        Return accum = 0
    End Function

    Public Function GetStatusText() As String
        Dim state = CursorLicStore.LoadTrialState()
        If state Is Nothing Then
            Return "License state not found."
        End If

        Dim sb As New StringBuilder()
        sb.AppendLine("License Status")
        sb.AppendLine(New String("-"c, 30))
        sb.AppendLine($"Type      : {state.LicenseType}")
        sb.AppendLine($"Start Date: {state.StartDate:dd/MM/yyyy}")
        sb.AppendLine($"End Date  : {state.EndDate:dd/MM/yyyy}")
        sb.AppendLine($"Last Check: {state.LastCheckDate:dd/MM/yyyy}")
        sb.AppendLine($"Machine   : {state.FingerprintHash}")

        If String.Equals(state.LicenseType, "FULL", StringComparison.OrdinalIgnoreCase) Then
            sb.AppendLine("Status    : ACTIVE (FULL)")
        Else
            Dim remaining = (state.EndDate.Date - Date.Today).Days
            sb.AppendLine($"Days Left : {Math.Max(0, remaining)}")
            sb.AppendLine($"Status    : {If(remaining < 0, "EXPIRED", If(remaining <= 7, "EXPIRING", "ACTIVE"))}")
        End If

        Return sb.ToString()
    End Function

    Public Function GetLicenseInfo() As String
        Return GetStatusText()
    End Function

    Public Function GetCurrentLicenseInfo() As Dictionary(Of String, String)
        Dim info As New Dictionary(Of String, String)
        Dim state = CursorLicStore.LoadTrialState()
        If state Is Nothing Then
            info.Add("EndDate", "Unknown")
            info.Add("LastCheckDate", "Unknown")
            info.Add("FingerprintHash", "Unknown")
            Return info
        End If

        info.Add("EndDate", state.EndDate.ToString("dd/MM/yyyy"))
        info.Add("LastCheckDate", state.LastCheckDate.ToString("dd/MM/yyyy"))
        info.Add("FingerprintHash", state.FingerprintHash)
        Return info
    End Function

    Public Sub CreateRequestFileWithForm(owner As IWin32Window)
        Using form As New RequestForm()
            form.LicenseInfo = GetCurrentLicenseInfo()
            If form.ShowDialog(owner) = DialogResult.OK AndAlso form.RequestConfirmed Then
                CreateRequestFile(form.DoctorName, form.ClinicName, form.ContactEmail, form.SelectedPlan, form.SelectedPeriod)
            End If
        End Using
    End Sub

    Public Function CreateRequestFile(doctorName As String, clinicName As String, contactEmail As String, plan As String, period As String) As Boolean
        Dim fileBytes As Byte() = Nothing
        If Not TryBuildLicenseRequestFileBytes(doctorName, clinicName, contactEmail, plan, period, fileBytes, showErrors:=True) Then
            Return False
        End If

        Using sfd As New SaveFileDialog()
            sfd.Filter = "Request Files (*.req)|*.req|All Files (*.*)|*.*"
            sfd.FileName = $"DentistX_Request_{DateTime.Now:yyyyMMdd_HHmmss}.req"
            If sfd.ShowDialog() <> DialogResult.OK Then
                Return False
            End If

            File.WriteAllBytes(sfd.FileName, fileBytes)

            MessageBox.Show($"Request file created:{Environment.NewLine}{sfd.FileName}",
                            "License", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return True
        End Using
    End Function

    ''' <summary>
    ''' Writes a license request file under the application folder (Requests) without prompts. Used when the trial has expired so the vendor can still receive machine fingerprint data.
    ''' </summary>
    ''' <param name="writtenPath">Full path of the .req file if the function returns True.</param>
    Public Function TryCreateSilentRequestFileInAppRequestsFolder(ByRef writtenPath As String) As Boolean
        writtenPath = Nothing
        Const placeholder = "(trial ended — please add your details when you contact support)"
        Dim fileBytes As Byte() = Nothing
        If Not TryBuildLicenseRequestFileBytes(placeholder, placeholder, placeholder, "Trial expired", "N/A", fileBytes, showErrors:=False) Then
            Return False
        End If
        Try
            Dim dir = CursorLicConfig.GetRequestsDir()
            Directory.CreateDirectory(dir)
            writtenPath = Path.Combine(dir, $"DentistX_Request_{DateTime.Now:yyyyMMdd_HHmmss}.req")
            File.WriteAllBytes(writtenPath, fileBytes)
            Return True
        Catch ex As Exception
            CursorLicLogger.Error("Silent request file failed: " & ex.Message)
            writtenPath = Nothing
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Saves a .req file under the app Requests folder. Uses trial.dat when present; otherwise fingerprint-only (e.g. local trial.dat missing dialog).
    ''' </summary>
    Public Function TrySaveLicenseRequestToAppRequestsFolder(owner As IWin32Window) As Boolean
        Dim fileBytes As Byte() = Nothing
        Dim state = CursorLicStore.LoadTrialState()
        Dim ok As Boolean
        If state IsNot Nothing Then
            ok = TryBuildLicenseRequestFileBytes(
                "(not provided — use License / About form for full details)",
                "(not provided)",
                "(not provided)",
                "License recovery",
                "N/A",
                fileBytes,
                showErrors:=True,
                owner:=owner)
        Else
            ok = TryBuildLicenseRequestFileBytesWithoutLocalState(
                "(not provided — local license data missing)",
                "(not provided)",
                "(not provided)",
                "License recovery",
                "trial.dat unreadable",
                fileBytes,
                showErrors:=True,
                owner:=owner)
        End If
        If Not ok OrElse fileBytes Is Nothing Then
            Return False
        End If
        Try
            Dim dir = CursorLicConfig.GetRequestsDir()
            Directory.CreateDirectory(dir)
            Dim reqFilePath = IO.Path.Combine(dir, $"DentistX_Request_{DateTime.Now:yyyyMMdd_HHmmss}.req")
            File.WriteAllBytes(reqFilePath, fileBytes)
            MessageBox.Show(owner,
                $"License request saved to:{Environment.NewLine}{reqFilePath}{Environment.NewLine}{Environment.NewLine}Please send this file to your programmer or software vendor.",
                "License",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information)
            Return True
        Catch ex As Exception
            MessageBox.Show(owner, "Could not save request file: " & ex.Message, "License", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End Try
    End Function

    Private Function BuildEncryptedRequestBlobFromPayload(payload As CursorRequestPayload, publicKeyXml As String) As Byte()
        Dim json = JsonConvert.SerializeObject(payload, Formatting.Indented)
        Dim jsonBytes = Encoding.UTF8.GetBytes(json)
        Dim aesKey = CursorCrypto.GenerateRandomBytes(32)
        Dim aesIv = CursorCrypto.GenerateRandomBytes(16)
        Dim cipher = CursorCrypto.EncryptWithIv(jsonBytes, aesKey, aesIv)
        Dim encryptedKey = CursorCrypto.RsaEncryptSmall(aesKey, publicKeyXml)
        Using ms As New MemoryStream()
            Using bw As New BinaryWriter(ms)
                bw.Write(encryptedKey.Length)
                bw.Write(encryptedKey)
                bw.Write(aesIv.Length)
                bw.Write(aesIv)
                bw.Write(cipher.Length)
                bw.Write(cipher)
            End Using
            Return ms.ToArray()
        End Using
    End Function

    Private Sub ShowPublicKeyMissing(owner As IWin32Window)
        MessageBox.Show(owner, "Public key is not configured. Cannot create request file.",
            "License", MessageBoxButtons.OK, MessageBoxIcon.Error)
    End Sub

    Private Function TryBuildLicenseRequestFileBytes(
        doctorName As String,
        clinicName As String,
        contactEmail As String,
        plan As String,
        period As String,
        ByRef fileBytes As Byte(),
        showErrors As Boolean,
        Optional owner As IWin32Window = Nothing) As Boolean
        fileBytes = Nothing
        Dim publicKeyXml = CursorLicConfig.GetPublicKeyXml()
        If String.IsNullOrWhiteSpace(publicKeyXml) Then
            If showErrors Then ShowPublicKeyMissing(owner)
            Return False
        End If

        Dim state = CursorLicStore.LoadTrialState()
        If state Is Nothing Then
            If showErrors Then
                MessageBox.Show(owner, "License state not found.", "License", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
            Return False
        End If

        Dim payload = New CursorRequestPayload With {
            .FingerprintHash = state.FingerprintHash,
            .FingerprintRaw = CursorFingerprint.GetFingerprintRaw(),
            .MachineName = Environment.MachineName,
            .DoctorName = doctorName,
            .ClinicName = clinicName,
            .ContactEmail = contactEmail,
            .AppVersion = Application.ProductVersion,
            .StartDate = state.StartDate.ToString("dd/MM/yyyy"),
            .EndDate = state.EndDate.ToString("dd/MM/yyyy"),
            .LastCheckDate = state.LastCheckDate.ToString("dd/MM/yyyy"),
            .RequestedPlan = plan,
            .RequestedPeriod = period,
            .SystemInfo = GetSystemInfo(),
            .TimestampUtc = DateTime.UtcNow.ToString("o"),
            .RequestedBy = Environment.UserName,
            .RequestedFromIP = GetLocalIPAddress()
        }

        fileBytes = BuildEncryptedRequestBlobFromPayload(payload, publicKeyXml)
        Return True
    End Function

    ''' <summary>Same as normal request payload but date fields note missing local trial.dat; fingerprint still identifies the PC.</summary>
    Private Function TryBuildLicenseRequestFileBytesWithoutLocalState(
        doctorName As String,
        clinicName As String,
        contactEmail As String,
        plan As String,
        period As String,
        ByRef fileBytes As Byte(),
        showErrors As Boolean,
        Optional owner As IWin32Window = Nothing) As Boolean
        fileBytes = Nothing
        Dim publicKeyXml = CursorLicConfig.GetPublicKeyXml()
        If String.IsNullOrWhiteSpace(publicKeyXml) Then
            If showErrors Then ShowPublicKeyMissing(owner)
            Return False
        End If

        Dim fingerprintRaw = CursorFingerprint.GetFingerprintRaw()
        Dim fingerprintHash = CursorFingerprint.GetFingerprintHash(fingerprintRaw)
        Const unknownDates = "unknown (local trial.dat missing or unreadable)"
        Dim payload = New CursorRequestPayload With {
            .FingerprintHash = fingerprintHash,
            .FingerprintRaw = fingerprintRaw,
            .MachineName = Environment.MachineName,
            .DoctorName = doctorName,
            .ClinicName = clinicName,
            .ContactEmail = contactEmail,
            .AppVersion = Application.ProductVersion,
            .StartDate = unknownDates,
            .EndDate = unknownDates,
            .LastCheckDate = unknownDates,
            .RequestedPlan = plan,
            .RequestedPeriod = period,
            .SystemInfo = GetSystemInfo(),
            .TimestampUtc = DateTime.UtcNow.ToString("o"),
            .RequestedBy = Environment.UserName,
            .RequestedFromIP = GetLocalIPAddress()
        }

        fileBytes = BuildEncryptedRequestBlobFromPayload(payload, publicKeyXml)
        Return True
    End Function

    Public Sub ReadAndApplyResponseFile(owner As IWin32Window)
        Using ofd As New OpenFileDialog()
            ofd.Filter = "Response Files (*.resp;*.json)|*.resp;*.json|All Files (*.*)|*.*"
            If ofd.ShowDialog(owner) = DialogResult.OK Then
                ApplyResponseFile(ofd.FileName)
            End If
        End Using
    End Sub

    Public Function ApplyResponseFile(path As String) As Boolean
        Try
            Dim publicKeyXml = CursorLicConfig.GetPublicKeyXml()
            If String.IsNullOrWhiteSpace(publicKeyXml) Then
                MessageBox.Show("Public key is not configured. Cannot apply response file.",
                                "License", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return False
            End If

            Dim wrapperJson = File.ReadAllText(path)
            Dim wrapper = JsonConvert.DeserializeObject(Of Dictionary(Of String, String))(wrapperJson)
            If wrapper Is Nothing OrElse Not wrapper.ContainsKey("payload") OrElse Not wrapper.ContainsKey("signature") Then
                Throw New Exception("Invalid response file format.")
            End If

            Dim payloadBytes = Convert.FromBase64String(wrapper("payload"))
            Dim signatureBytes = Convert.FromBase64String(wrapper("signature"))

            If Not CursorCrypto.VerifySignature(payloadBytes, signatureBytes, publicKeyXml) Then
                Throw New Exception("Signature verification failed.")
            End If

            Dim payloadJson = Encoding.UTF8.GetString(payloadBytes)
            Dim payload = JsonConvert.DeserializeObject(Of CursorResponsePayload)(payloadJson)

            Dim state = CursorLicStore.LoadTrialState()
            If state Is Nothing Then
                Throw New Exception("Local license state not found.")
            End If

            If payload.FingerprintHash <> state.FingerprintHash Then
                Throw New Exception("Fingerprint mismatch.")
            End If

            Dim newEnd As Date
            If Not Date.TryParseExact(payload.NewEndDate, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, newEnd) Then
                Throw New Exception("Invalid NewEndDate format in response.")
            End If

            state.LicenseType = If(String.IsNullOrWhiteSpace(payload.LicenseType), "TRIAL", payload.LicenseType.ToUpperInvariant())
            state.EndDate = newEnd
            state.LastCheckDate = Date.Today
            state.IsBlocked = payload.IsBlocked

            If state.IsBlocked Then
                Return HardFail("This license has been blocked.")
            End If

            CursorLicStore.SaveTrialState(state)
            Dim fingerprintRaw = CursorFingerprint.GetFingerprintRaw()
            If Not CursorPcTrialsRepository.UpsertTrialState(state, fingerprintRaw, "AdminResponse") Then
                WarnAndLimit("Failed to update license data in PC_Trials.")
            End If
            WriteDecoyLicense(state, Date.Today)

            MessageBox.Show("License updated successfully.", "License", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return True
        Catch ex As Exception
            MessageBox.Show("Apply response failed: " & ex.Message, "License", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End Try
    End Function

    Private Function CreateInitialState(fingerprintHash As String, nowDate As Date) As CursorTrialState
        Return New CursorTrialState With {
            .LicenseType = "TRIAL",
            .FingerprintHash = fingerprintHash,
            .StartDate = nowDate.Date,
            .EndDate = nowDate.Date.AddMonths(TrialMonths),
            .LastCheckDate = nowDate.Date,
            .IsBlocked = False
        }
    End Function

    Private Function ValidateState(state As CursorTrialState, fingerprintHash As String, nowDate As Date) As Boolean
        If state Is Nothing Then
            Return HardFail("License state missing.")
        End If

        If state.FingerprintHash <> fingerprintHash Then
            Return HardFail("License does not belong to this machine.")
        End If

        If state.IsBlocked Then
            Return HardFail("This license has been blocked.")
        End If

        If nowDate.Date < state.LastCheckDate.Date Then
            Return HardFail("System date rollback detected.")
        End If

        If String.Equals(state.LicenseType, "FULL", StringComparison.OrdinalIgnoreCase) Then
            Return True
        End If

        If nowDate.Date > state.EndDate.Date Then
            Return FailTrialExpiredWithSilentRequestFile()
        End If

        Return True
    End Function

    ''' <summary>
    ''' Recreates DentistX.lic from encrypted trial.dat (same folder as app). Used by license recovery UI.
    ''' </summary>
    Public Sub RepairDecoyLicenseFromLocalState(issueDate As Date)
        Dim s = CursorLicStore.LoadTrialState()
        If s Is Nothing Then
            Return
        End If
        WriteDecoyLicense(s, issueDate)
    End Sub

    ''' <summary>
    ''' Copies a valid DentistX.lic into the application folder if it matches this machine and local license state.
    ''' </summary>
    Public Function TryImportDecoyLicenseFile(sourcePath As String) As Boolean
        Try
            Dim fingerprintRaw = CursorFingerprint.GetFingerprintRaw()
            Dim fingerprintHash = CursorFingerprint.GetFingerprintHash(fingerprintRaw)
            Dim s = CursorLicStore.LoadTrialState()
            If s Is Nothing Then
                MessageBox.Show("Local license data was not found. You may need to reinstall or restore from backup.",
                                "License", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return False
            End If

            Dim enc = File.ReadAllText(sourcePath).Trim()
            Dim json = CursorCrypto.DecryptString(enc)
            Dim decoy = JsonConvert.DeserializeObject(Of CursorLicenseDecoy)(json)

            If decoy Is Nothing OrElse decoy.Marker <> DecoyMarker Then
                MessageBox.Show("This file is not a valid DentistX license file.",
                                "License", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return False
            End If

            If decoy.FingerprintHash <> fingerprintHash Then
                MessageBox.Show("This license file is not for this computer.",
                                "License", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return False
            End If

            If decoy.EndDate.Date <> s.EndDate.Date OrElse
               Not String.Equals(decoy.LicenseType, s.LicenseType, StringComparison.OrdinalIgnoreCase) Then
                MessageBox.Show("This file does not match the license stored on this computer." & Environment.NewLine &
                                "Use ""Repair license file"" to recreate DentistX.lic from stored data, or apply a license response from your vendor.",
                                "License", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return False
            End If

            Dim dest = CursorLicConfig.GetDecoyLicenseFile()
            File.Copy(sourcePath, dest, True)
            MessageBox.Show("License file installed successfully.", "License", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return True
        Catch ex As Exception
            MessageBox.Show("Could not use this file: " & ex.Message, "License", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Ensures DentistX.lic exists and matches trial.dat; offers recovery instead of exiting when fixable.
    ''' </summary>
    Private Function EnsureDecoyLicenseWithRecovery(ByRef state As CursorTrialState, fingerprintHash As String, nowDate As Date) As Boolean
        Do
            Dim filePath = CursorLicConfig.GetDecoyLicenseFile()

            If Not File.Exists(filePath) Then
                CursorLicLogger.Warn("DentistX.lic missing — recreating from local license data.")
                WriteDecoyLicense(state, nowDate)
                Return True
            End If

            Dim decoy As CursorLicenseDecoy = Nothing
            Try
                Dim enc = File.ReadAllText(filePath).Trim()
                Dim json = CursorCrypto.DecryptString(enc)
                decoy = JsonConvert.DeserializeObject(Of CursorLicenseDecoy)(json)
            Catch ex As Exception
                CursorLicLogger.Error("DentistX.lic read/decrypt failed: " & ex.Message)
                If Not ShowLicenseRecoveryDialog(
                    "The license file in the application folder cannot be read. It may be corrupted or not a valid DentistX license file.",
                    filePath) Then
                    Return False
                End If
                state = CursorLicStore.LoadTrialState()
                If state Is Nothing Then Return HardFail("License state missing.")
                Continue Do
            End Try

            If decoy Is Nothing OrElse decoy.Marker <> DecoyMarker Then
                CursorLicLogger.Error("DentistX.lic invalid marker.")
                If Not ShowLicenseRecoveryDialog(
                    "The license file is not valid for this application.",
                    filePath) Then
                    Return False
                End If
                state = CursorLicStore.LoadTrialState()
                If state Is Nothing Then Return HardFail("License state missing.")
                Continue Do
            End If

            If decoy.FingerprintHash <> fingerprintHash Then
                CursorLicLogger.Error("DentistX.lic fingerprint mismatch.")
                Return HardFail("License file does not match this machine.")
            End If

            If decoy.EndDate.Date <> state.EndDate.Date OrElse
               Not String.Equals(decoy.LicenseType, state.LicenseType, StringComparison.OrdinalIgnoreCase) Then
                CursorLicLogger.Error("DentistX.lic does not match local trial state.")
                If Not ShowLicenseRecoveryDialog(
                    "The license file does not match the license data stored on this computer (for example after an update, restore, or copying the wrong file).",
                    filePath) Then
                    Return False
                End If
                state = CursorLicStore.LoadTrialState()
                If state Is Nothing Then Return HardFail("License state missing.")
                Continue Do
            End If

            Dim expected = ComputeDecoyChecksum(decoy)
            If Not String.Equals(decoy.Checksum, expected, StringComparison.OrdinalIgnoreCase) Then
                CursorLicLogger.Error("DentistX.lic checksum mismatch — rewriting file (invariant checksum).")
                WriteDecoyLicense(state, decoy.IssueDate.Date)
            End If

            Return True
        Loop
    End Function

    Private Function ShowLicenseRecoveryDialog(message As String, filePath As String) As Boolean
        Using f As New LicenseRecoveryForm()
            f.MessageText = message
            f.PathsLabelText = "Expected license file:" & Environment.NewLine & filePath
            If f.ShowDialog() <> DialogResult.OK Then
                Application.Exit()
                Return False
            End If
        End Using
        Return True
    End Function

    ''' <summary>
    ''' trial.dat / registry could not be loaded, but SQL or DentistX.lic suggests this PC was licensed — explain and exit.
    ''' </summary>
    Private Function ShowLocalTrialDataMissingFatal(hasDbRow As Boolean, decoyExists As Boolean) As Boolean
        CursorLicLogger.Error("Trial state missing while DB row or decoy license exists.")
        Using f As New LicenseRecoveryForm()
            f.ShowRepairOptions = False
            f.MessageText = BuildLocalTrialDataMissingMessage(hasDbRow, decoyExists)
            f.PathsLabelText = BuildLocalTrialDataMissingPathsLabel()
            f.ShowDialog()
        End Using
        Application.Exit()
        Return False
    End Function

    Private Function BuildLocalTrialDataMissingMessage(hasDbRow As Boolean, decoyExists As Boolean) As String
        Dim sb As New StringBuilder()
        sb.AppendLine("The license data stored on this computer could not be read.")
        sb.AppendLine()
        sb.AppendLine("The encrypted file that holds your license (trial.dat, under the license data folder below) is missing, unreadable, or was removed. Without it, DentistX cannot verify your license.")
        If hasDbRow Then
            sb.AppendLine()
            sb.AppendLine("Your database still has a license record for this PC. That usually means only the local files were deleted, corrupted, or overwritten — for example after reinstalling Windows, restoring an old backup, or manual cleanup.")
        End If
        If decoyExists Then
            sb.AppendLine()
            sb.AppendLine("A DentistX.lic file is present in the application folder, but it cannot be used without the matching local license data.")
        End If
        sb.AppendLine()
        sb.AppendLine("What you can do: restore trial.dat from a backup if you have one; ask your administrator or vendor for help; or prepare a license request from another machine if your vendor allows it.")
        sb.AppendLine()
        sb.AppendLine("Use the buttons below to open the folders where files are expected. Then close this window — the application will exit.")
        Return sb.ToString()
    End Function

    Private Function BuildLocalTrialDataMissingPathsLabel() As String
        Return "License data file (trial.dat):" & Environment.NewLine &
            CursorLicConfig.GetTrialDataFile() & Environment.NewLine & Environment.NewLine &
            "Application folder (DentistX.lic):" & Environment.NewLine &
            AppDomain.CurrentDomain.BaseDirectory
    End Function

    ''' <summary>
    ''' Opens Windows Explorer on a folder (creates it if missing). Used by license recovery UI.
    ''' </summary>
    Public Sub OpenFolderInExplorer(folderPath As String)
        Try
            Dim p = Path.GetFullPath(folderPath)
            If Not Directory.Exists(p) Then
                Directory.CreateDirectory(p)
            End If
            Process.Start("explorer.exe", p)
        Catch ex As Exception
            MessageBox.Show("Could not open folder: " & ex.Message, "License", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End Try
    End Sub

    Private Sub WriteDecoyLicense(state As CursorTrialState, nowDate As Date)
        Dim decoy = New CursorLicenseDecoy With {
            .Marker = DecoyMarker,
            .LicenseType = state.LicenseType,
            .FingerprintHash = state.FingerprintHash,
            .IssueDate = nowDate.Date,
            .EndDate = state.EndDate.Date
        }
        decoy.Checksum = ComputeDecoyChecksum(decoy)
        Dim json = JsonConvert.SerializeObject(decoy)
        Dim enc = CursorCrypto.EncryptString(json)
        File.WriteAllText(CursorLicConfig.GetDecoyLicenseFile(), enc)
    End Sub

    Private Function ComputeDecoyChecksum(decoy As CursorLicenseDecoy) As String
        Const fmt = "yyyyMMdd"
        Dim issuePart = decoy.IssueDate.ToString(fmt, CultureInfo.InvariantCulture)
        Dim endPart = decoy.EndDate.ToString(fmt, CultureInfo.InvariantCulture)
        Dim raw = $"{decoy.Marker}|{decoy.LicenseType}|{decoy.FingerprintHash}|{issuePart}|{endPart}"
        Return CursorCrypto.ComputeSha256(raw)
    End Function

    Private Function GetTrustedDate() As Date
        Try
            Dim req = CType(WebRequest.Create("https://www.google.com"), HttpWebRequest)
            req.Method = "HEAD"
            req.Timeout = 3000
            Using res = CType(req.GetResponse(), HttpWebResponse)
                Return Date.Parse(res.Headers("Date"), CultureInfo.InvariantCulture)
            End Using
        Catch
            Return Date.Now
        End Try
    End Function

    Private Function GetSystemInfo() As Dictionary(Of String, String)
        Dim info As New Dictionary(Of String, String)

        Try
            Dim detailed = FingerprintHelper.GetSystemInfoDictionary()
            For Each kvp In detailed
                If Not info.ContainsKey(kvp.Key) Then
                    info(kvp.Key) = kvp.Value
                End If
            Next
        Catch
        End Try

        info("MachineName") = Environment.MachineName
        info("UserName") = Environment.UserName
        info("OSVersion") = Environment.OSVersion.ToString()
        info("ProcessorCount") = Environment.ProcessorCount.ToString()
        info("Is64BitOS") = Environment.Is64BitOperatingSystem.ToString()
        info("WindowsUser") = Environment.UserName
        info("UserDomain") = Environment.UserDomainName
        Return info
    End Function

    Private Function GetLocalIPAddress() As String
        Try
            Dim host = Dns.GetHostEntry(Dns.GetHostName())
            For Each ip In host.AddressList
                If ip.AddressFamily = AddressFamily.InterNetwork Then
                    Return ip.ToString()
                End If
            Next
            Return "Unknown"
        Catch
            Return "Unknown"
        End Try
    End Function

    Private Function HardFail(message As String) As Boolean
        CursorLicLogger.Error(message)
        MessageBox.Show(message, "License", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Application.Exit()
        Return False
    End Function

    ''' <summary>
    ''' Trial is over: write a .req file next to the app (no Save dialog), then explain how to send it to the vendor before exit.
    ''' </summary>
    Private Function FailTrialExpiredWithSilentRequestFile() As Boolean
        CursorLicLogger.Error("Trial period has expired.")
        Dim reqPath As String = Nothing
        Dim created = TryCreateSilentRequestFileInAppRequestsFolder(reqPath)

        Dim msg As New StringBuilder()
        msg.AppendLine("Your DentistX trial period has ended.")
        msg.AppendLine()
        msg.AppendLine("To continue using the program, your vendor needs a license request from this computer.")
        If created AndAlso Not String.IsNullOrEmpty(reqPath) Then
            msg.AppendLine()
            msg.AppendLine("A request file was saved automatically here:")
            msg.AppendLine(reqPath)
            msg.AppendLine()
            msg.AppendLine("Please send that file to your programmer or software vendor (for example by email or WhatsApp). They will send you back an activation file to apply in the License section.")
        Else
            msg.AppendLine()
            msg.AppendLine("We could not create the request file automatically. Please contact your programmer or vendor and tell them your trial has expired; they will tell you what to send next.")
        End If

        MessageBox.Show(msg.ToString(), "Trial ended", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Application.Exit()
        Return False
    End Function

    Private Function WarnAndLimit(message As String) As Boolean
        _limitedMode = True
        CursorLicLogger.Warn(message)
        MessageBox.Show(message, "License", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        Return True
    End Function
End Module
