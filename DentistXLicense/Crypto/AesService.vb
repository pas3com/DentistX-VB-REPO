Imports System.Security.Cryptography
Imports System.IO

Public Module AesService

    Public Function Encrypt(data As Byte(), key As Byte(), iv As Byte()) As Byte()
        Using aesA = Aes.Create()
            aesA.KeySize = 256
            aesA.Mode = CipherMode.CBC
            aesA.Padding = PaddingMode.PKCS7
            aesA.Key = key
            aesA.IV = iv

            Using ms As New MemoryStream()
                Using cs As New CryptoStream(ms, aesA.CreateEncryptor(), CryptoStreamMode.Write)
                    cs.Write(data, 0, data.Length)
                    cs.FlushFinalBlock()
                End Using
                Return ms.ToArray()
            End Using
        End Using
    End Function

    Public Function Decrypt(cipher As Byte(), key As Byte(), iv As Byte()) As Byte()
        Using aesA = Aes.Create()
            aesA.KeySize = 256
            aesA.Mode = CipherMode.CBC
            aesA.Padding = PaddingMode.PKCS7
            aesA.Key = key
            aesA.IV = iv

            Using msIn As New MemoryStream(cipher)
                Using cs As New CryptoStream(msIn, aesA.CreateDecryptor(), CryptoStreamMode.Read)
                    Using msOut As New MemoryStream()
                        cs.CopyTo(msOut)
                        Return msOut.ToArray()
                    End Using
                End Using
            End Using
        End Using
    End Function

End Module
