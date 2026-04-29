Imports System.IO
Imports Microsoft.Win32
Imports Newtonsoft.Json

Public Module CursorLicStore
    Public Function LoadTrialState() As CursorTrialState
        Dim enc As String = Nothing

        Dim filePath = CursorLicConfig.GetTrialDataFile()
        If File.Exists(filePath) Then
            enc = File.ReadAllText(filePath).Trim()
        Else
            Dim legacyFile = CursorLicConfig.GetLegacyTrialDataFile()
            If File.Exists(legacyFile) Then
                CursorLicLogger.Warn($"Using legacy trial.dat: {legacyFile}")
                enc = File.ReadAllText(legacyFile).Trim()
            Else
                enc = ReadRegistry()
                If String.IsNullOrWhiteSpace(enc) Then
                    enc = ReadRegistry(CursorLicConfig.GetLegacyRegistryPath())
                    If Not String.IsNullOrWhiteSpace(enc) Then
                        CursorLicLogger.Warn($"Using legacy registry path: {CursorLicConfig.GetLegacyRegistryPath()}")
                    End If
                Else
                    CursorLicLogger.Warn($"Using registry path: {CursorLicConfig.GetRegistryPath()}")
                End If
            End If
        End If

        If String.IsNullOrWhiteSpace(enc) Then
            CursorLicLogger.Warn("No trial state found in file or registry.")
            Return Nothing
        End If

        Try
            Dim json = CursorCrypto.DecryptString(enc)
            Return JsonConvert.DeserializeObject(Of CursorTrialState)(json)
        Catch
            CursorLicLogger.Error("Trial state decryption failed (trial.dat or registry may be tampered).")
            Return Nothing
        End Try
    End Function

    Public Sub SaveTrialState(state As CursorTrialState)
        Dim json = JsonConvert.SerializeObject(state)
        Dim enc = CursorCrypto.EncryptString(json)

        Dim filePath = CursorLicConfig.GetTrialDataFile()
        Directory.CreateDirectory(Path.GetDirectoryName(filePath))
        File.WriteAllText(filePath, enc)
        WriteRegistry(enc)
    End Sub

    Private Function ReadRegistry(Optional regPath As String = Nothing) As String
        Dim value As String = Nothing
        Dim path = If(String.IsNullOrWhiteSpace(regPath), CursorLicConfig.GetRegistryPath(), regPath)

        Try
            Using k = Registry.LocalMachine.OpenSubKey(path)
                value = TryCast(k?.GetValue("Trial"), String)
            End Using
        Catch
        End Try

        If Not String.IsNullOrEmpty(value) Then
            Return value
        End If

        Try
            Using k = Registry.CurrentUser.OpenSubKey(path)
                value = TryCast(k?.GetValue("Trial"), String)
            End Using
        Catch
        End Try

        Return value
    End Function

    Private Sub WriteRegistry(value As String)
        Dim regPath = CursorLicConfig.GetRegistryPath()

        Try
            Using k = Registry.LocalMachine.CreateSubKey(regPath)
                k.SetValue("Trial", value)
                Return
            End Using
        Catch
        End Try

        Try
            Using k = Registry.CurrentUser.CreateSubKey(regPath)
                k.SetValue("Trial", value)
            End Using
        Catch
        End Try
    End Sub

    ''' <summary>Removes trial.dat (WinCache + legacy DentistX) and Trial values from HKLM/HKCU for both registry roots.</summary>
    Public Sub ClearAllLocalTrialStorage()
        Try
            Dim p = CursorLicConfig.GetTrialDataFile()
            If File.Exists(p) Then File.Delete(p)
        Catch
        End Try
        Try
            Dim leg = CursorLicConfig.GetLegacyTrialDataFile()
            If File.Exists(leg) Then File.Delete(leg)
        Catch
        End Try
        TryDeleteTrialRegistryValue(CursorLicConfig.GetRegistryPath())
        TryDeleteTrialRegistryValue(CursorLicConfig.GetLegacyRegistryPath())
    End Sub

    Private Sub TryDeleteTrialRegistryValue(regPath As String)
        If String.IsNullOrWhiteSpace(regPath) Then Return
        Try
            Using k = Registry.LocalMachine.OpenSubKey(regPath, writable:=True)
                k?.DeleteValue("Trial", throwOnMissingValue:=False)
            End Using
        Catch
        End Try
        Try
            Using k = Registry.CurrentUser.OpenSubKey(regPath, writable:=True)
                k?.DeleteValue("Trial", throwOnMissingValue:=False)
            End Using
        Catch
        End Try
    End Sub
End Module
