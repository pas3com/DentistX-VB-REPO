Imports System.IO
Imports System.Security.Cryptography
Imports System.Text
Imports Newtonsoft.Json

Public Class LicenseRequestWriter

    Private ReadOnly _publicKeyXml As String
    Private ReadOnly _privateKeyXml As String

    Public Sub New(publicKeyXml As String, privateKeyXml As String)
        _publicKeyXml = publicKeyXml
        _privateKeyXml = privateKeyXml
    End Sub

    Public Sub WriteV2(path As String, request As LicenseRequest, metadata As Dictionary(Of String, String))
        Dim json = JsonConvert.SerializeObject(request)
        Dim jsonBytes = Encoding.UTF8.GetBytes(json)

        ' --- AES ---
        Dim aesKey(31) As Byte
        Dim iv(15) As Byte

        Using rng As RandomNumberGenerator = RandomNumberGenerator.Create()
            rng.GetBytes(aesKey)
            rng.GetBytes(iv)
        End Using


        Dim cipher = AesService.Encrypt(jsonBytes, aesKey, iv)

        ' --- RSA encrypt AES key ---
        Dim encKey = RsaService.EncryptSmall(aesKey, _publicKeyXml)

        ' --- SIGN CIPHER ---
        Dim signature = SignatureService.Sign(cipher, _privateKeyXml)

        ' --- METADATA ---
        Dim metaJson = JsonConvert.SerializeObject(metadata)
        Dim metaBytes = Encoding.UTF8.GetBytes(metaJson)

        Using fs As New FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None)
            Using bw As New BinaryWriter(fs)

                bw.Write(LicenseConstants.MAGIC)
                bw.Write(LicenseVersions.V2)

                bw.Write(metaBytes.Length)
                bw.Write(metaBytes)

                bw.Write(encKey.Length)
                bw.Write(encKey)

                bw.Write(iv.Length)
                bw.Write(iv)

                bw.Write(cipher.Length)
                bw.Write(cipher)

                bw.Write(signature.Length)
                bw.Write(signature)

            End Using
        End Using
    End Sub

End Class
