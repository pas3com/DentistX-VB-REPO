
Imports System.IO
Imports System.Security
Imports System.Security.Cryptography
Imports System.Text

Public Module CryptoHelper
    Private ReadOnly Salt As Byte() = Encoding.UTF8.GetBytes("s0m3$@lt_1234")
    Private Const Passphrase As String = "ClientLightKey"
    Private ReadOnly ClientKey As Byte() = DeriveKey(Passphrase, Salt, 10000)
    'Private ReadOnly PublicKeyXml1 As String = File.ReadAllText("PublicKey.xml") ' embedded or copied with app
    Private ReadOnly PublicKeyXml As String = KeyManager.DevPublicKeyXml
    Public Function DeriveKey(passphrase As String, salt As Byte(), iterations As Integer) As Byte()
        Using kdf = New Rfc2898DeriveBytes(passphrase, salt, iterations, HashAlgorithmName.SHA256)
            Return kdf.GetBytes(32)
        End Using
    End Function
    Public Function GenerateRandomBytes(len As Integer) As Byte()
        Dim b(len - 1) As Byte
        Using rng = RandomNumberGenerator.Create()
            rng.GetBytes(b)
        End Using
        Return b
    End Function
    ' AES-256 CBC with IV prepended
    Public Function AESEncrypt(plainBytes As Byte(), key As Byte()) As Byte()
        Using aesAlg As Aes = Aes.Create()
            aesAlg.KeySize = 256
            aesAlg.BlockSize = 128
            aesAlg.Mode = CipherMode.CBC
            aesAlg.Padding = PaddingMode.PKCS7
            aesAlg.Key = key
            aesAlg.GenerateIV()
            Using ms As New MemoryStream()
                ' write IV first
                ms.Write(aesAlg.IV, 0, aesAlg.IV.Length)
                Using cs As New CryptoStream(ms, aesAlg.CreateEncryptor(), CryptoStreamMode.Write)
                    cs.Write(plainBytes, 0, plainBytes.Length)
                    cs.FlushFinalBlock()
                End Using
                Return ms.ToArray()
            End Using
        End Using
    End Function
    Public Function AESDecrypt(cipherWithIv As Byte(), key As Byte()) As Byte()
        Using aesAlg As Aes = Aes.Create()
            aesAlg.KeySize = 256
            aesAlg.BlockSize = 128
            aesAlg.Mode = CipherMode.CBC
            aesAlg.Padding = PaddingMode.PKCS7
            aesAlg.Key = key
            Dim iv(15) As Byte
            Array.Copy(cipherWithIv, 0, iv, 0, iv.Length)
            aesAlg.IV = iv

            Using ms As New MemoryStream()
                Using cs As New CryptoStream(ms, aesAlg.CreateDecryptor(), CryptoStreamMode.Write)
                    cs.Write(cipherWithIv, iv.Length, cipherWithIv.Length - iv.Length)
                    cs.FlushFinalBlock()
                    Return ms.ToArray()
                End Using
            End Using
        End Using
    End Function
    Public Function AESEncryptWithIV(plainBytes As Byte(), key As Byte(), iv As Byte()) As Byte()
        Using aesAlg As Aes = Aes.Create()
            aesAlg.Key = key
            aesAlg.IV = iv
            aesAlg.Mode = CipherMode.CBC
            aesAlg.Padding = PaddingMode.PKCS7
            Using ms As New MemoryStream()
                Using cs As New CryptoStream(ms, aesAlg.CreateEncryptor(), CryptoStreamMode.Write)
                    cs.Write(plainBytes, 0, plainBytes.Length)
                    cs.FlushFinalBlock()
                End Using
                Return ms.ToArray()
            End Using
        End Using
    End Function
    Public Function AESDecryptWithIV(cipherBytes As Byte(), key As Byte(), iv As Byte()) As Byte()
        Using aesAlg As Aes = Aes.Create()
            aesAlg.Key = key
            aesAlg.IV = iv
            aesAlg.Mode = CipherMode.CBC
            aesAlg.Padding = PaddingMode.PKCS7
            Using ms As New MemoryStream(cipherBytes)
                Using cs As New CryptoStream(ms, aesAlg.CreateDecryptor(), CryptoStreamMode.Read)
                    Using msOut As New MemoryStream()
                        Dim buffer(4095) As Byte
                        Dim read As Integer = cs.Read(buffer, 0, buffer.Length)
                        While read > 0
                            msOut.Write(buffer, 0, read)
                            read = cs.Read(buffer, 0, buffer.Length)
                        End While
                        Return msOut.ToArray()
                    End Using
                End Using
            End Using
        End Using
    End Function
    Public Function AESEncryptToBase64(plain As String, key As Byte()) As String
        Dim data = Encoding.UTF8.GetBytes(plain)
        Dim iv = GenerateRandomBytes(16)
        Dim cipher = AESEncryptWithIV(data, key, iv)
        Using ms As New MemoryStream()
            Using bw As New BinaryWriter(ms)
                bw.Write(iv.Length)
                bw.Write(iv)
                bw.Write(cipher.Length)
                bw.Write(cipher)
            End Using
            Return Convert.ToBase64String(ms.ToArray())
        End Using
    End Function
    Public Function AESDecryptFromBase64(b64 As String, key As Byte()) As String
        Dim bytes = Convert.FromBase64String(b64)
        Using ms As New MemoryStream(bytes)
            Using br As New BinaryReader(ms)
                Dim ivLen = br.ReadInt32()
                Dim iv = br.ReadBytes(ivLen)
                Dim cipherLen = br.ReadInt32()
                Dim cipher = br.ReadBytes(cipherLen)
                Dim dec = AESDecryptWithIV(cipher, key, iv)
                Return Encoding.UTF8.GetString(dec)
            End Using
        End Using
    End Function
    Public Function EncryptBytes(plainBytes As Byte()) As Byte()
        Dim iv = GenerateRandomBytes(16)
        Dim cipher = AESEncryptWithIV(plainBytes, ClientKey, iv)
        Using ms As New MemoryStream()
            Using bw As New BinaryWriter(ms)
                bw.Write(iv.Length)
                bw.Write(iv)
                bw.Write(cipher.Length)
                bw.Write(cipher)
            End Using
            Return ms.ToArray()
        End Using
    End Function
    Public Function DecryptBytes(cipherWithIv As Byte()) As Byte()
        Using ms As New MemoryStream(cipherWithIv)
            Using br As New BinaryReader(ms)
                Dim ivLen = br.ReadInt32()
                Dim iv = br.ReadBytes(ivLen)
                Dim cipherLen = br.ReadInt32()
                Dim cipher = br.ReadBytes(cipherLen)
                Return AESDecryptWithIV(cipher, ClientKey, iv)
            End Using
        End Using
    End Function
    Public Function ComputeSHA256(input As String) As String
        Using sha As SHA256 = SHA256.Create()
            Dim bytes = Encoding.UTF8.GetBytes(input)
            Dim hash = sha.ComputeHash(bytes)
            Dim sb As New StringBuilder(hash.Length * 2)
            For Each b As Byte In hash
                sb.AppendFormat("{0:X2}", b)
            Next
            Return sb.ToString()
        End Using
    End Function
    Public Function RsaEncryptSmall(data As Byte(), publicKeyXml As String) As Byte()
        Using rsa As New RSACryptoServiceProvider(2048)
            rsa.FromXmlString(publicKeyXml)
            Return rsa.Encrypt(data, True)
        End Using
    End Function
    Public Function RsaDecryptSmall(cipher As Byte(), privateKeyXml As String) As Byte()
        Using rsa As New RSACryptoServiceProvider(2048)
            rsa.FromXmlString(privateKeyXml)
            Return rsa.Decrypt(cipher, True)
        End Using
    End Function
    Public Function EncryptForDatabase(plainText As String) As Byte()
        ' Use the same method as EncryptBytes for consistency
        Return EncryptBytes(Encoding.UTF8.GetBytes(plainText))
    End Function
    Public Function DecryptFromDatabase(encryptedBytes As Byte()) As String
        Dim decrypted = DecryptBytes(encryptedBytes)
        Return Encoding.UTF8.GetString(decrypted)
    End Function
    Public Function VerifyAndExtractSignedPayload(raw As Byte()) As String
        Using ms As New MemoryStream(raw)
            Using br As New BinaryReader(ms)

                ' --- Read signature ---
                Dim sigLen As Integer = br.ReadInt32()
                If sigLen <= 0 OrElse sigLen > 4096 Then
                    MsgBox("Invalid license signature length.")
                End If

                Dim signature As Byte() = br.ReadBytes(sigLen)

                ' --- Read payload ---
                Dim payload As Byte() = br.ReadBytes(CInt(ms.Length - ms.Position))
                If payload.Length = 0 Then
                    MsgBox("License payload missing.")
                End If

                ' --- Verify signature ---
                Using rsa As New RSACryptoServiceProvider(2048)
                    rsa.FromXmlString(PublicKeyXml)

                    Dim ok As Boolean =
                        rsa.VerifyData(
                            payload,
                            CryptoConfig.MapNameToOID("SHA256"),
                            signature
                        )

                    If Not ok Then
                        MsgBox("License signature verification failed.")
                    End If
                End Using

                ' --- Return verified JSON ---
                Return Encoding.UTF8.GetString(payload)
            End Using
        End Using
    End Function

End Module


