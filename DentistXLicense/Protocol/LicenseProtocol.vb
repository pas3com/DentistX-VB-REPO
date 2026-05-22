
Public NotInheritable Class LicenseProtocol

    Public Const CURRENT_VERSION As Integer = LicenseVersions.V2

    Public Shared Sub Enforce(version As Integer)
        If version <> CURRENT_VERSION Then
            Throw New InvalidOperationException(
                $"Unsupported license protocol version: {version}")
        End If
    End Sub

End Class


'Public Module LicenseProtocol

'    ' ===== VERSION 2 REQUEST FORMAT =====
'    '
'    ' [MAGIC]                String
'    ' [VERSION]              Int32 (2)
'    '
'    ' [METADATA_LEN]         Int32
'    ' [METADATA_JSON]        UTF8 JSON
'    '
'    ' [ENC_AES_KEY_LEN]      Int32
'    ' [ENC_AES_KEY]          RSA OAEP
'    '
'    ' [IV_LEN]               Int32
'    ' [IV]                   Bytes
'    '
'    ' [CIPHER_LEN]           Int32
'    ' [CIPHER]               AES-256-CBC
'    '
'    ' [SIG_LEN]              Int32
'    ' [SIGNATURE]            RSA-SHA256 over CIPHER
'    '
'    ' ====================================
'End Module
