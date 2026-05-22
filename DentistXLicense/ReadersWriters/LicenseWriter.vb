Imports System.IO
Imports System.Security.Cryptography
Imports System.Text
Imports Newtonsoft.Json

Public Class LicenseWriter

    Private ReadOnly _privateKeyXml As String

    Public Sub New(privateKeyXml As String)
        _privateKeyXml = privateKeyXml
    End Sub

    Public Sub Write(path As String, license As LicenseResponse)

        Dim json = JsonConvert.SerializeObject(license)
        Dim data = Encoding.UTF8.GetBytes(json)

        Dim signature = SignatureService.Sign(data, _privateKeyXml)

        Using fs As New FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None)
            Using bw As New BinaryWriter(fs)

                bw.Write(LicenseConstants.MAGIC)
                bw.Write(LicenseVersions.V2)

                bw.Write(data.Length)
                bw.Write(data)

                bw.Write(signature.Length)
                bw.Write(signature)

            End Using
        End Using
    End Sub

End Class

