Imports System.IO
Imports System.Security.Cryptography
Imports System.Text
Imports Newtonsoft.Json

Public Class LicenseRequestReader

    Private ReadOnly _privateKeyXml As String
    Private ReadOnly _publicKeyXml As String

    Public Sub New(privateKeyXml As String, publicKeyXml As String)
        _privateKeyXml = privateKeyXml
        _publicKeyXml = publicKeyXml
    End Sub

    Public Function Read(path As String) As LicenseRequest
        Using fs As New FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read)
            Using br As New BinaryReader(fs)

                Dim magic = br.ReadString()
                If magic <> LicenseConstants.MAGIC Then
                    Throw New InvalidDataException("Invalid license request file.")
                End If

                Dim version = br.ReadInt32()
                Select Case version
                    Case LicenseVersions.V1
                        Throw New NotSupportedException("Legacy v1 handling not implemented here.")
                    Case LicenseVersions.V2
                        Return ReadV2(br)
                    Case Else
                        Throw New InvalidDataException("Unsupported license version.")
                End Select
            End Using
        End Using
    End Function

    Private Function ReadV2(br As BinaryReader) As LicenseRequest

        ' --- METADATA ---
        Dim metaLen = br.ReadInt32()
        Dim metaBytes = br.ReadBytes(metaLen)
        Dim metaJson = Encoding.UTF8.GetString(metaBytes)

        ' --- AES KEY ---
        Dim keyLen = br.ReadInt32()
        Dim encKey = br.ReadBytes(keyLen)
        Dim aesKey = RsaService.DecryptSmall(encKey, _privateKeyXml)

        ' --- IV ---
        Dim ivLen = br.ReadInt32()
        Dim iv = br.ReadBytes(ivLen)

        ' --- CIPHER ---
        Dim cipherLen = br.ReadInt32()
        Dim cipher = br.ReadBytes(cipherLen)

        ' --- SIGNATURE ---
        Dim sigLen = br.ReadInt32()
        Dim signature = br.ReadBytes(sigLen)

        If Not SignatureService.Verify(cipher, signature, _publicKeyXml) Then
            Throw New CryptographicException("License request signature invalid.")
        End If

        Dim plain = AesService.Decrypt(cipher, aesKey, iv)
        Dim json = Encoding.UTF8.GetString(plain)

        Return JsonConvert.DeserializeObject(Of LicenseRequest)(json)
    End Function

End Class

