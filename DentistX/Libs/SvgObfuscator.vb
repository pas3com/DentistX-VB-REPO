' Console App - SvgObfuscator.vb
Imports System.IO

Module SvgObfuscator
    Private ReadOnly XOR_KEY As Byte() = {&HA1, &HB2, &HC3, &HD4, &HE5, &HF6, &H7, &H18}

    Sub Main()
        Dim inputDir = "C:\YourProject\OriginalSvgs\"
        Dim outputDir = "C:\YourProject\SvgResources\"

        Console.WriteLine("Obfuscating SVG files...")

        ' Obfuscate all SVG files
        For Each svgFile In Directory.GetFiles(inputDir, "*.svg")
            Dim fileName = Path.GetFileName(svgFile)
            Console.WriteLine($"Obfuscating: {fileName}")
            ObfuscateSvgFile(svgFile, Path.Combine(outputDir, fileName))
        Next

        Console.WriteLine("SVG files obfuscated successfully!")
        Console.ReadLine()
    End Sub

    Private Sub ObfuscateSvgFile(inputPath As String, outputPath As String)
        Using inputStream = File.OpenRead(inputPath)
            Using outputStream = File.Create(outputPath)
                Dim buffer(4095) As Byte
                Dim bytesRead As Integer
                Dim keyIndex As Integer = 0

                Do
                    bytesRead = inputStream.Read(buffer, 0, buffer.Length)
                    For i As Integer = 0 To bytesRead - 1
                        buffer(i) = buffer(i) Xor XOR_KEY(keyIndex)
                        keyIndex = (keyIndex + 1) Mod XOR_KEY.Length
                    Next
                    outputStream.Write(buffer, 0, bytesRead)
                Loop While bytesRead > 0
            End Using
        End Using
    End Sub
End Module