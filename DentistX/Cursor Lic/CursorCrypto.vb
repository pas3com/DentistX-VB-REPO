Imports System.IO
Imports System.Security.Cryptography
Imports System.Text

Public Module CursorCrypto
    Private Function DeriveKey() As Byte()
        Dim passphrase = CursorLicConfig.GetEncryptionKey()
        Dim salt = Encoding.UTF8.GetBytes(CursorLicConfig.GetEncryptionSalt())
        Using kdf = New Rfc2898DeriveBytes(passphrase, salt, 10000, HashAlgorithmName.SHA256)
            Return kdf.GetBytes(32)
        End Using
    End Function

    Public Function EncryptString(plainText As String) As String
        Dim plainBytes = Encoding.UTF8.GetBytes(plainText)
        Dim cipherBytes = EncryptBytes(plainBytes)
        Return Convert.ToBase64String(cipherBytes)
    End Function

    Public Function DecryptString(cipherB64 As String) As String
        Dim cipherBytes = Convert.FromBase64String(cipherB64)
        Dim plainBytes = DecryptBytes(cipherBytes)
        Return Encoding.UTF8.GetString(plainBytes)
    End Function

    Public Function EncryptBytes(plainBytes As Byte()) As Byte()
        Dim key = DeriveKey()
        Using aesAlg As Aes = Aes.Create()
            aesAlg.KeySize = 256
            aesAlg.BlockSize = 128
            aesAlg.Mode = CipherMode.CBC
            aesAlg.Padding = PaddingMode.PKCS7
            aesAlg.Key = key
            aesAlg.GenerateIV()
            Using ms As New MemoryStream()
                Using bw As New BinaryWriter(ms)
                    bw.Write(aesAlg.IV.Length)
                    bw.Write(aesAlg.IV)
                    Using cs As New CryptoStream(ms, aesAlg.CreateEncryptor(), CryptoStreamMode.Write)
                        cs.Write(plainBytes, 0, plainBytes.Length)
                        cs.FlushFinalBlock()
                    End Using
                End Using
                Return ms.ToArray()
            End Using
        End Using
    End Function

    Public Function DecryptBytes(cipherWithIv As Byte()) As Byte()
        Dim key = DeriveKey()
        Using ms As New MemoryStream(cipherWithIv)
            Using br As New BinaryReader(ms)
                Dim ivLen = br.ReadInt32()
                Dim iv = br.ReadBytes(ivLen)
                Dim cipher = br.ReadBytes(CInt(ms.Length - ms.Position))
                Using aesAlg As Aes = Aes.Create()
                    aesAlg.KeySize = 256
                    aesAlg.BlockSize = 128
                    aesAlg.Mode = CipherMode.CBC
                    aesAlg.Padding = PaddingMode.PKCS7
                    aesAlg.Key = key
                    aesAlg.IV = iv
                    Using msOut As New MemoryStream()
                        Using cs As New CryptoStream(msOut, aesAlg.CreateDecryptor(), CryptoStreamMode.Write)
                            cs.Write(cipher, 0, cipher.Length)
                            cs.FlushFinalBlock()
                        End Using
                        Return msOut.ToArray()
                    End Using
                End Using
            End Using
        End Using
    End Function

    Public Function EncryptWithIv(plainBytes As Byte(), key As Byte(), iv As Byte()) As Byte()
        Using aesAlg As Aes = Aes.Create()
            aesAlg.KeySize = 256
            aesAlg.BlockSize = 128
            aesAlg.Mode = CipherMode.CBC
            aesAlg.Padding = PaddingMode.PKCS7
            aesAlg.Key = key
            aesAlg.IV = iv
            Using ms As New MemoryStream()
                Using cs As New CryptoStream(ms, aesAlg.CreateEncryptor(), CryptoStreamMode.Write)
                    cs.Write(plainBytes, 0, plainBytes.Length)
                    cs.FlushFinalBlock()
                End Using
                Return ms.ToArray()
            End Using
        End Using
    End Function

    Public Function GenerateRandomBytes(length As Integer) As Byte()
        Dim b(length - 1) As Byte
        Using rng = RandomNumberGenerator.Create()
            rng.GetBytes(b)
        End Using
        Return b
    End Function

    Public Function ComputeSha256(input As String) As String
        Using sha = SHA256.Create()
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

    Public Function VerifySignature(payload As Byte(), signature As Byte(), publicKeyXml As String) As Boolean
        Using rsa As New RSACryptoServiceProvider(2048)
            rsa.FromXmlString(publicKeyXml)
            Return rsa.VerifyData(payload, CryptoConfig.MapNameToOID("SHA256"), signature)
        End Using
    End Function
End Module
