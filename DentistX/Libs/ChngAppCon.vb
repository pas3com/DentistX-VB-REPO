Option Infer On
Imports System.Configuration
Imports System.Xml

Public Class ChngAppCon
    Public Shared Sub UpdateLang(ByVal key As String, ByVal value As String)
        Dim xmlDoc = New XmlDocument()
        xmlDoc.Load(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile)

        For Each xmlElement As XmlElement In xmlDoc.DocumentElement
            If xmlElement.Name.Equals("appSettings") Then
                For Each xNode As XmlNode In xmlElement.ChildNodes
                    If xNode.Attributes(0).Value.Equals(key) Then
                        xNode.Attributes(1).Value = value
                    End If
                Next xNode
            End If
        Next xmlElement

        ConfigurationManager.RefreshSection("appSettings")

        xmlDoc.Save(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile)
    End Sub

    Public Shared Sub UpdateDbType(ByVal key As String, ByVal value As String)
        Dim xmlDoc = New XmlDocument()
        xmlDoc.Load(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile)

        For Each xmlElement As XmlElement In xmlDoc.DocumentElement
            If xmlElement.Name.Equals("appSettings") Then
                For Each xNode As XmlNode In xmlElement.ChildNodes
                    If xNode.Attributes(0).Value.Equals(key) Then
                        xNode.Attributes(1).Value = value
                    End If
                Next xNode
            End If
        Next xmlElement

        ConfigurationManager.RefreshSection("appSettings")

        xmlDoc.Save(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile)
    End Sub
End Class
