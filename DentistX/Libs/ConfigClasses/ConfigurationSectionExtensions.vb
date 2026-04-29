Option Infer On
Imports System.Configuration
Imports System.Xml

Public Module ConfigurationSectionExtensions
    <System.Runtime.CompilerServices.Extension>
    Public Function GetAs(Of T)(ByVal section As ConfigurationSection) As T
        Dim sectionInformation = section.SectionInformation

        Dim sectionHandlerType = Type.GetType(sectionInformation.Type)
        If sectionHandlerType Is Nothing Then
            Throw New InvalidOperationException(String.Format("Unable to find section handler type '{0}'.", sectionInformation.Type))
        End If

        Dim sectionHandler As IConfigurationSectionHandler
        Try
            sectionHandler = DirectCast(Activator.CreateInstance(sectionHandlerType), IConfigurationSectionHandler)
        Catch ex As InvalidCastException
            Throw New InvalidOperationException(String.Format("Section handler type '{0}' does not implement IConfigurationSectionHandler.", sectionInformation.Type), ex)
        End Try

        Dim rawXml = sectionInformation.GetRawXml()
        If rawXml Is Nothing Then
            Return Nothing
        End If

        Dim xmlDocument = New XmlDocument()
        xmlDocument.LoadXml(rawXml)

        Return DirectCast(sectionHandler.Create(Nothing, Nothing, xmlDocument.DocumentElement), T)
    End Function
End Module
'The way you would call it in your example is:

'Private map = New ExeConfigurationFileMap With {.ExeConfigFilename = "c:\\foo.config"}
'Private configuration = ConfigurationManager.OpenMappedExeConfiguration(map, ConfigurationUserLevel.None)
'Private myParamsSection = configuration.GetSection("MyParams")
'Private myParamsCollection = myParamsSection.GetAs(Of NameValueCollection)()
