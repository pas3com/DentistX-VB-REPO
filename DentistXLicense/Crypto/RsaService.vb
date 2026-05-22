Imports System.Security.Cryptography

Public Module RsaService

    Public Function EncryptSmall(data As Byte(), publicKeyXml As String) As Byte()
        Using rsa As New RSACryptoServiceProvider(2048)
            rsa.FromXmlString(publicKeyXml)
            Return rsa.Encrypt(data, True) ' OAEP-SHA1 (LOCKED)
        End Using
    End Function

    Public Function DecryptSmall(data As Byte(), privateKeyXml As String) As Byte()
        Using rsa As New RSACryptoServiceProvider(2048)
            rsa.FromXmlString(privateKeyXml)
            Return rsa.Decrypt(data, True)
        End Using
    End Function

End Module
