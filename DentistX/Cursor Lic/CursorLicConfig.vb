Imports System.Configuration
Imports System.IO

Public Module CursorLicConfig
    Private Const DataDirName As String = "WinCache"
    Private Const LegacyDataDirName As String = "DentistX"
    Private Const RegistryPath As String = "SOFTWARE\WinCache" 'Computer\HKEY_LOCAL_MACHINE\SOFTWARE\WinCache
    'Computer\HKEY_LOCAL_MACHINE\SOFTWARE\WOW6432Node\WinCache
    Private Const LegacyRegistryPath As String = "SOFTWARE\DentistX"

    Public Function GetAppSetting(key As String, defaultValue As String) As String
        Try
            Dim value = ConfigurationManager.AppSettings(key)
            If String.IsNullOrEmpty(value) Then
                CursorLicLogger.Warn($"App.config missing key '{key}', using default.")
                Return defaultValue
            End If
            Return value
        Catch
            CursorLicLogger.Warn($"App.config read failed for key '{key}', using default.")
            Return defaultValue
        End Try
    End Function

    Public Function GetEncryptionKey() As String
        Return GetAppSetting("EncryptionKey", "ClientLightKey")
    End Function

    Public Function GetEncryptionSalt() As String
        Return GetAppSetting("EncryptionSalt", "s0m3$@lt_1234")
    End Function

    ''' <summary>
    ''' When true (App.config LicenseDevResetEnabled = 1 or true), Ctrl+Shift+Alt+F12 on the license recovery form can run a developer trial reset after secret entry.
    ''' Leave disabled and omit secret in production builds.
    ''' </summary>
    Public Function IsLicenseDevResetEnabled() As Boolean
        Dim v = GetAppSetting("LicenseDevResetEnabled", "").Trim()
        Return String.Equals(v, "true", StringComparison.OrdinalIgnoreCase) OrElse v = "1"
    End Function

    ''' <summary>Passphrase that must match the InputBox after the hotkey (same key name in App.config).</summary>
    Public Function GetLicenseDevResetSecret() As String
        Return GetAppSetting("LicenseDevResetSecret", "")
    End Function

    Public Function GetPublicKeyXml() As String
        Dim external = LoadFromExternalFile()
        If Not String.IsNullOrEmpty(external) Then
            Return external
        End If

        Dim configKey = GetAppSetting("DevPublicKeyXml", "")
        If Not String.IsNullOrEmpty(configKey) Then
            Return UnescapeXml(configKey)
        End If

        Return SAMPLE_KEYS.DevPublicKeyXml
    End Function

    Public Function GetDataDir() As String
        Return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), DataDirName)
    End Function
    Public Function GetLegacyDataDir() As String
        Return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), LegacyDataDirName)
    End Function

    Public Function GetTrialDataFile() As String
        Return Path.Combine(GetDataDir(), "trial.dat")
    End Function
    Public Function GetLegacyTrialDataFile() As String
        Return Path.Combine(GetLegacyDataDir(), "trial.dat")
    End Function

    Public Function GetDecoyLicenseFile() As String
        Return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "DentistX.lic")
    End Function

    ''' <summary>Folder under the app directory where auto-generated license request files are stored (e.g. after trial expiry).</summary>
    Public Function GetRequestsDir() As String
        Return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Requests")
    End Function

    Public Function GetRegistryPath() As String
        Return RegistryPath
    End Function
    Public Function GetLegacyRegistryPath() As String
        Return LegacyRegistryPath
    End Function

    Private Function LoadFromExternalFile() As String
        Try
            Dim keyPaths = New String() {
                Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Keys", "PublicKey.xml"),
                Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "PublicKey.xml"),
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "DentistX", "PublicKey.xml")
            }

            For Each keyPath In keyPaths
                If File.Exists(keyPath) Then
                    Return File.ReadAllText(keyPath)
                End If
            Next
        Catch
        End Try

        Return ""
    End Function

    Private Function UnescapeXml(escapedXml As String) As String
        Dim unescaped = escapedXml
        unescaped = unescaped.Replace("&lt;", "<")
        unescaped = unescaped.Replace("&gt;", ">")
        unescaped = unescaped.Replace("&amp;", "&")
        unescaped = unescaped.Replace("&quot;", """")
        unescaped = unescaped.Replace("&apos;", "'")
        unescaped = System.Text.RegularExpressions.Regex.Replace(unescaped, "&#(\d+);",
            Function(m) ChrW(Integer.Parse(m.Groups(1).Value)).ToString())
        unescaped = System.Text.RegularExpressions.Regex.Replace(unescaped, "&#x([0-9A-F]+);",
            Function(m) ChrW(Convert.ToInt32(m.Groups(1).Value, 16)).ToString(),
            System.Text.RegularExpressions.RegexOptions.IgnoreCase)
        Return unescaped
    End Function
End Module
