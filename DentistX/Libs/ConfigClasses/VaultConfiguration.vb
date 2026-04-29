Option Infer On
Imports System.Configuration
#Region "ConfigSection With Elemets"
'Your root section definition will be like this:
Public Class VaultConfiguration
    Inherits ConfigurationSection

    <ConfigurationProperty("Vaults", IsRequired:=True)>
    Public Property Vaults() As VaultElementCollection
        Get
            Return CType(Me("Vault"), VaultElementCollection)
        End Get
        Set(ByVal value As VaultElementCollection)
            Me("Vaults") = value
        End Set
    End Property
End Class
'Then, your Vaults ElementCollection:
Public Class VaultElementCollection
    Inherits ConfigurationElementCollection

    Default Public Overloads ReadOnly Property Item(ByVal elementKey As Object) As Vault
        Get
            Return BaseGet(elementKey)
        End Get
    End Property

    Public Sub Add(ByVal Vault As Vault)
        MyBase.BaseAdd(Vault)
    End Sub

    Public Overrides ReadOnly Property CollectionType() As ConfigurationElementCollectionType
        Get
            Return ConfigurationElementCollectionType.BasicMap
        End Get
    End Property

    Protected Overrides Function CreateNewElement() As ConfigurationElement
        Return New Vault()
    End Function

    Protected Overrides Function GetElementKey(ByVal element As ConfigurationElement) As Object
        Return CType(element, Vault).Key
    End Function

    Protected Overrides ReadOnly Property ElementName() As String
        Get
            Return "Vault"
        End Get
    End Property
End Class
'Then, your Vault element:
Public Class Vault
    Inherits ConfigurationElement

    <ConfigurationProperty("Key", IsRequired:=True, IsKey:=True)>
    Public Property Key() As String
        Get
            Return CStr(Me("Key"))
        End Get
        Set(ByVal value As String)
            Me("Key") = value
        End Set
    End Property

    <ConfigurationProperty("Queries", IsRequired:=True)>
    Public Property Queries() As KeyValueConfigurationCollection
        Get
            Return CType(Me("Queries"), KeyValueConfigurationCollection)
        End Get
        Set(ByVal value As KeyValueConfigurationCollection)
            Me("Queries") = value
        End Set
    End Property
End Class
''You're probably going to have to mess around with the KeyValueConfigurationCollection a bit to make it work right, but I think this would be the general idea. Then, when accessing this stuff in your code, you would do something like this:

'Private myConfig = DirectCast(ConfigurationManager.GetSection("Vaults"), VaultConfig)
''and to access a single value from, say, your products collection...
'Private myValue = myConfig.Vaults("Products").Queries("KeyForKeyValuePairing").Value
''Hope that helps. Now just don't ask me to translate it to VB :-D
#End Region


Public Class KeyValueConfigCollection
    Public Shared Sub Main()
        Try


            ' Get the  application configuration object.

            Dim config As Configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None)
            ' Get the section related object.
            Dim configSection As System.Configuration.AppSettingsSection = CType(config.GetSection("appSettings"), System.Configuration.AppSettingsSection)

            '      Dim configSection As AppSettingsSection = _
            '       (AppSettingsSection)config.GetSection("appSettings")


            ' Display Config details.
            MsgBox("File Path: {0}", config.FilePath)
            MsgBox("Section Path: {0}", configSection.SectionInformation.Name.ToString())


            ' Create the KeyValueConfigurationElement.
            Dim myAdminKeyVal As KeyValueConfigurationElement = New KeyValueConfigurationElement("Language", "en")


            ' Determine if the configuration contains
            ' any KeyValueConfigurationElements.
            Dim configSettings As KeyValueConfigurationCollection = config.AppSettings.Settings()

            If configSettings.AllKeys.Length = 0 Then
                ' Add KeyValueConfigurationElement to collection.
                config.AppSettings.Settings.Add(myAdminKeyVal)

                If Not configSection.SectionInformation.IsLocked Then
                    config.Save()
                    MsgBox("** Configuration updated.")
                Else
                    MsgBox("** Could not update, section is locked.")
                End If
            End If

            ' Get the KeyValueConfigurationCollection 
            ' from the configuration.
            Dim settings As KeyValueConfigurationCollection = config.AppSettings.Settings()

            ' Display each KeyValueConfigurationElement.
            Dim keyValueElement As KeyValueConfigurationElement
            For Each keyValueElement In settings
                MsgBox("Key: {0}", keyValueElement.Key)
                MsgBox("Value: {0}", keyValueElement.Value)

            Next

        Catch e As System.ArgumentException
            ' Unknown error.
            MsgBox(e.ToString())
        End Try
        ' Display and wait

    End Sub
End Class