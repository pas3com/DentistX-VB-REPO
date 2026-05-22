Imports System.IO
Imports System.Security.Cryptography
Imports System.Text
Imports Newtonsoft.Json

Public Class LicenseReader

    Private ReadOnly _publicKeyXml As String

    Public Sub New(publicKeyXml As String)
        _publicKeyXml = publicKeyXml
    End Sub

    Public Function Read(path As String) As LicenseResponse

        Using fs As New FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read)
            Using br As New BinaryReader(fs)

                Dim magic = br.ReadString()
                If magic <> LicenseConstants.MAGIC Then
                    Throw New InvalidDataException("Invalid license file.")
                End If

                Dim version = br.ReadInt32()
                If version <> LicenseVersions.V2 Then
                    Throw New InvalidDataException("Unsupported license version.")
                End If

                Dim dataLen = br.ReadInt32()
                Dim data = br.ReadBytes(dataLen)

                Dim sigLen = br.ReadInt32()
                Dim signature = br.ReadBytes(sigLen)

                If Not SignatureService.Verify(data, signature, _publicKeyXml) Then
                    Throw New CryptographicException("License signature invalid.")
                End If

                Dim json = Encoding.UTF8.GetString(data)
                Return JsonConvert.DeserializeObject(Of LicenseResponse)(json)
            End Using
        End Using
    End Function

End Class

