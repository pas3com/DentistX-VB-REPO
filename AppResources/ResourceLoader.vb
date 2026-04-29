Imports System.IO
Imports System.Drawing
Imports System.Reflection
Imports System.IO.Compression
Imports System.Security.Cryptography
Imports System.Text
Imports DevExpress.Utils.Svg

Public Class ResourceLoader
    'Private Shared ReadOnly Assembly As Assembly = Assembly.GetExecutingAssembly()
    Private Shared ReadOnly Password As String = "YourStrongPassword123!"
    Private Shared ReadOnly Salt As Byte() = {1, 2, 3, 4, 5, 6, 7, 8}

    ' Generic resource loader
    Public Shared Function GetImage1(resourcePath As String) As Image
        Using stream = GetResourceStream(resourcePath)
            Return Image.FromStream(stream)
        End Using
    End Function

    Public Shared Function GetIcon(resourcePath As String) As Icon
        Using stream = GetResourceStream(resourcePath)
            Return New Icon(stream)
        End Using
    End Function

    Public Shared Function GetSvgResource(key As String) As SvgImage

        'Dim entName As String =
        Dim assembly = Reflection.Assembly.GetExecutingAssembly()
        Using resourceStream = assembly.GetManifestResourceStream("AppResources.Pngs.encrypted")
            If resourceStream Is Nothing Then
                Throw New InvalidOperationException("Embedded resource not found")
            End If

            ' Read IV from beginning of stream
            Dim iv(15) As Byte
            resourceStream.Read(iv, 0, iv.Length)

            ' Create AES decryptor
            Using aes As Aes = Aes.Create()
                Dim keyDeriver = New Rfc2898DeriveBytes(Password, Salt, 1000)
                aes.Key = keyDeriver.GetBytes(32)
                aes.IV = iv

                Using decryptor = aes.CreateDecryptor()
                    Using cryptoStream = New CryptoStream(resourceStream, decryptor, CryptoStreamMode.Read)
                        Using zipArchive = New ZipArchive(cryptoStream, ZipArchiveMode.Read)
                            Dim entryName = $"{key}"
                            Dim entry = zipArchive.GetEntry(entryName)

                            If entry Is Nothing Then
                                Throw New ArgumentException($"Resource {entryName} not found")
                            End If

                            ' Return decompressed stream
                            Dim memStream = New MemoryStream()
                            Using entryStream = entry.Open()
                                entryStream.CopyTo(memStream)
                            End Using
                            memStream.Position = 0
                            Return SvgImage.FromStream(memStream)
                        End Using
                    End Using
                End Using
            End Using
        End Using
    End Function
    Public Shared Function GetPngResource(key As String) As Image

        'Dim entName As String =
        Dim assembly = Reflection.Assembly.GetExecutingAssembly()
        Using resourceStream = assembly.GetManifestResourceStream("AppResources.Pngs.encrypted")
            If resourceStream Is Nothing Then
                Throw New InvalidOperationException("Embedded resource not found")
            End If

            ' Read IV from beginning of stream
            Dim iv(15) As Byte
            resourceStream.Read(iv, 0, iv.Length)

            ' Create AES decryptor
            Using aes As Aes = Aes.Create()
                Dim keyDeriver = New Rfc2898DeriveBytes(Password, Salt, 1000)
                aes.Key = keyDeriver.GetBytes(32)
                aes.IV = iv

                Using decryptor = aes.CreateDecryptor()
                    Using cryptoStream = New CryptoStream(resourceStream, decryptor, CryptoStreamMode.Read)
                        Using zipArchive = New ZipArchive(cryptoStream, ZipArchiveMode.Read)
                            Dim entryName = $"{key}"
                            Dim entry = zipArchive.GetEntry(entryName)

                            'If entry Is Nothing Then
                            '    Throw New ArgumentException($"Resource {entryName} not found")
                            'End If
                            If entry Is Nothing Then
                                Return Nothing
                            End If

                            ' Return decompressed stream
                            Dim memStream = New MemoryStream()
                            Using entryStream = entry.Open()
                                entryStream.CopyTo(memStream)
                            End Using
                            memStream.Position = 0
                            Return Image.FromStream(memStream)
                        End Using
                    End Using
                End Using
            End Using
        End Using
    End Function

    Public Shared Function GetGifResource(key As String) As Image

        'Dim entName As String =
        Dim assembly = Reflection.Assembly.GetExecutingAssembly()
        Using resourceStream = assembly.GetManifestResourceStream("AppResources.Pngs.encrypted")
            If resourceStream Is Nothing Then
                Throw New InvalidOperationException("Embedded resource not found")
            End If

            ' Read IV from beginning of stream
            Dim iv(15) As Byte
            resourceStream.Read(iv, 0, iv.Length)

            ' Create AES decryptor
            Using aes As Aes = Aes.Create()
                Dim keyDeriver = New Rfc2898DeriveBytes(Password, Salt, 1000)
                aes.Key = keyDeriver.GetBytes(32)
                aes.IV = iv

                Using decryptor = aes.CreateDecryptor()
                    Using cryptoStream = New CryptoStream(resourceStream, decryptor, CryptoStreamMode.Read)
                        Using zipArchive = New ZipArchive(cryptoStream, ZipArchiveMode.Read)
                            Dim entryName = $"Teeth/{key}.gif"
                            Dim entry = zipArchive.GetEntry(entryName)

                            If entry Is Nothing Then
                                Return Nothing
                            End If

                            ' Return decompressed stream
                            Dim memStream = New MemoryStream()
                            Using entryStream = entry.Open()
                                entryStream.CopyTo(memStream)
                            End Using
                            memStream.Position = 0
                            Return Image.FromStream(memStream)
                        End Using
                    End Using
                End Using
            End Using
        End Using
    End Function
    Private Shared Function GetResourceStream(resourcePath As String) As Stream
        Dim assembly = Reflection.Assembly.GetExecutingAssembly()
        Dim fullName = $"AppResources.Resources.{resourcePath.Replace("/", ".")}"
        Dim stream = assembly.GetManifestResourceStream(fullName)

        If stream Is Nothing Then
            Throw New FileNotFoundException($"Resource '{resourcePath}' not found")
        End If

        Return stream
    End Function
    Public Sub EncryptResource(inputFile As String, outputFile As String)
        Using aes As Aes = Aes.Create()
            Dim key = New Rfc2898DeriveBytes("YourPassword", Salt).GetBytes(32)
            aes.Key = key
            aes.GenerateIV()

            Using inputStream = File.OpenRead(inputFile)
                Using outputStream = File.Create(outputFile)
                    outputStream.Write(aes.IV, 0, aes.IV.Length)

                    Using encryptor = aes.CreateEncryptor()
                        Using cryptoStream = New CryptoStream(outputStream, encryptor, CryptoStreamMode.Write)
                            inputStream.CopyTo(cryptoStream)
                        End Using
                    End Using
                End Using
            End Using
        End Using
    End Sub
End Class
