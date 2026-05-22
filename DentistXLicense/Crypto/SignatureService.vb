Imports System.Security.Cryptography

Public Module SignatureService

    Public Function Sign(data As Byte(), privateKeyXml As String) As Byte()
        Using rsaA = RSA.Create()
            rsaA.FromXmlString(privateKeyXml)
            Return rsaA.SignData(data, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1)
        End Using
    End Function

    Public Function Verify(data As Byte(), signature As Byte(), publicKeyXml As String) As Boolean
        Using rsaA = RSA.Create()
            rsaA.FromXmlString(publicKeyXml)
            Return rsaA.VerifyData(data, signature, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1)
        End Using
    End Function

End Module
