
Imports System.Configuration
Imports System.Text

Public Module SettingsHelper
    Private ReadOnly Salt As Byte() = Encoding.UTF8.GetBytes("s0m3$@lt_1234")
    Private Const Passphrase As String = "ClientLightKey"
    Private ReadOnly ClientKey As Byte() = CryptoHelper.DeriveKey(Passphrase, Salt, 10000)

    Public Sub SaveEncryptedSetting(key As String, plainValue As String)
        Dim enc = CryptoHelper.AESEncryptToBase64(plainValue, ClientKey)
        Dim config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None)
        Dim settings = CType(config.GetSection("appSettings"), AppSettingsSection)
        If settings.Settings(key) Is Nothing Then
            settings.Settings.Add(key, enc)
        Else
            settings.Settings(key).Value = enc
        End If
        config.Save(ConfigurationSaveMode.Modified)
        ConfigurationManager.RefreshSection("appSettings")
    End Sub
    Public Function ReadEncryptedSetting(key As String, defaultPlain As String) As String
        Try
            Dim config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None)
            Dim settings = CType(config.GetSection("appSettings"), AppSettingsSection)
            Dim val = If(settings.Settings(key) IsNot Nothing, settings.Settings(key).Value, Nothing)
            If String.IsNullOrEmpty(val) Then
                SaveEncryptedSetting(key, defaultPlain)
                Return defaultPlain
            End If
            Try
                Dim dec = CryptoHelper.AESDecryptFromBase64(val, ClientKey)
                Return dec
            Catch
                SaveEncryptedSetting(key, val)
                Return val
            End Try
        Catch
            Return defaultPlain
        End Try
    End Function
End Module
