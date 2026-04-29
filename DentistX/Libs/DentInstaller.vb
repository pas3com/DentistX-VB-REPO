Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Configuration.Install
Imports System.Configuration





Partial Public Class DentInstaller
    Inherits Installer

    Public Sub New()
        InitializeComponent()
    End Sub

    Public Overrides Sub Install(ByVal stateSaver As System.Collections.IDictionary)
        MyBase.Install(stateSaver)

        'get Configuration section 
        'name from custom action parameter
        Dim sectionName As String = Me.Context.Parameters("sectionName")

        'get Protected Configuration Provider 
        'name from custom action parameter
        Dim provName As String = Me.Context.Parameters("provName")

        ' get the exe path from the default context parameters
        Dim exeFilePath As String = Me.Context.Parameters("assemblypath")

        'encrypt the configuration section
        ProtectSection(sectionName, provName, exeFilePath)


    End Sub

    Private Sub ProtectSection(ByVal sectionName As String, ByVal provName As String, ByVal exeFilePath As String)
        Dim config As Configuration = ConfigurationManager.OpenExeConfiguration(exeFilePath)
        Dim section As ConfigurationSection = config.GetSection(sectionName)

        If Not section.SectionInformation.IsProtected Then
            'Protecting the specified section with the specified provider
            section.SectionInformation.ProtectSection(provName)
        End If
        section.SectionInformation.ForceSave = True
        config.Save(ConfigurationSaveMode.Modified)
        '----------------iNSERT tHIS     cODE  In Custome Actions
        '  Under Install Section
        'Custom Action Data ='/sectionName="applicationSettings/DemoWinApp.Properties.Settings" /provName="DPAPIProtection"'
        ' The Code Between The Tow single qotations
    End Sub
End Class

