' In SvgResources project - ProtectedResourceProvider.vb
Imports System.IO

Public Class ProtectedResourceProvider
    Private Shared ReadOnly XOR_KEY As Byte() = {&HA1, &HB2, &HC3, &HD4, &HE5, &HF6, &H7, &H18}

    Public Shared Function GetProtectedAdultSvg(key As String, type As String) As Stream
        ' Use the EXACT same logic as your working function
        Dim num As String = key.Substring(2)
        Dim name As String = key.Remove(2, 1)
        Dim resourceName = $"{name.ToUpper()}{type.ToUpper()}{num}X.svg"

        Return GetObfuscatedResource(resourceName)
    End Function

    Public Shared Function GetProtectedKidSvg(key As String, type As String) As Stream
        ' Use the EXACT same logic as your working function
        Dim num As String = key.Substring(2)
        Dim name As String = key.Remove(2, 1)
        Dim resourceName = $"{name.ToUpper()}{num}{type.ToUpper()}K.svg"

        Return GetObfuscatedResource(resourceName)
    End Function

    Private Shared Function GetObfuscatedResource(resourceName As String) As Stream
        Dim assembly = Reflection.Assembly.GetExecutingAssembly()
        Using resourceStream = assembly.GetManifestResourceStream($"SvgResources.{resourceName}")
            If resourceStream Is Nothing Then Return Nothing

            Dim outputStream = New MemoryStream()
            Dim buffer(4095) As Byte
            Dim bytesRead As Integer
            Dim keyIndex As Integer = 0

            Do
                bytesRead = resourceStream.Read(buffer, 0, buffer.Length)
                For i As Integer = 0 To bytesRead - 1
                    buffer(i) = buffer(i) Xor XOR_KEY(keyIndex)
                    keyIndex = (keyIndex + 1) Mod XOR_KEY.Length
                Next
                outputStream.Write(buffer, 0, bytesRead)
            Loop While bytesRead > 0

            outputStream.Position = 0
            Return outputStream
        End Using
    End Function
End Class