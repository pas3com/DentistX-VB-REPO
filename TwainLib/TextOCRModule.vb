Imports WIA
Imports Tesseract


Public Module TextOCRModule

    Public Function ScanImageWithUI() As String
        Try
            Dim destFolder As String = "C:\MyScannedDocuments"
            ' Let user choose destination folder
            Dim fbd As New FolderBrowserDialog With {
                .Description = "Select destination folder for scanned image"
            }
            If fbd.ShowDialog() <> DialogResult.OK Then
                destFolder = "C:\MyScannedDocuments"
            Else
                destFolder = fbd.SelectedPath
            End If


            ' Show scanner selection
            Dim deviceManager As New DeviceManager()
            Dim scanner As Device = Nothing
            Dim availableScanners As New List(Of DeviceInfo)

            For Each info As DeviceInfo In deviceManager.DeviceInfos
                If info.Type = WiaDeviceType.ScannerDeviceType Then
                    availableScanners.Add(info)
                End If
            Next

            If availableScanners.Count = 0 Then
                MessageBox.Show("No scanner found.")
                Return Nothing
            End If

            Dim scannerInfo As DeviceInfo = Nothing
            If availableScanners.Count = 1 Then
                scannerInfo = availableScanners(0)
            Else
                Dim names = availableScanners.Select(Function(s, i) $"{i + 1}. {s.Properties("Name").Value}").ToArray()
                Dim list = String.Join(Environment.NewLine, names)
                Dim idxStr = InputBox($"Select scanner by number:{Environment.NewLine}{list}", "Choose Scanner", "1")
                Dim idx As Integer
                If Not Integer.TryParse(idxStr, idx) OrElse idx < 1 OrElse idx > availableScanners.Count Then Return Nothing
                scannerInfo = availableScanners(idx - 1)
            End If

            ' Connect and show scanner's native dialog
            scanner = scannerInfo.Connect()
            Dim commonDialog As New CommonDialog
            Dim imageFile As ImageFile = commonDialog.ShowAcquireImage(
                WiaDeviceType.ScannerDeviceType, WiaImageIntent.ColorIntent, WiaImageBias.MinimizeSize,
                FormatID.wiaFormatJPEG, False, True, True)

            If imageFile Is Nothing Then
                MessageBox.Show("Scan canceled.")
                Return Nothing
            End If

            ' Save result
            Dim destPath = IO.Path.Combine(destFolder, $"Scan_{Now:yyyyMMdd_HHmmss}.jpg")
            imageFile.SaveFile(destPath)
            Return destPath

        Catch ex As Exception
            MessageBox.Show("Scan failed: " & ex.Message)
            Return Nothing
        End Try
    End Function

    Public Function ExtractTextFromImage(imagePath As String) As String
        Try
            Using engine As New TesseractEngine("C:\tessdata", "eng", EngineMode.Default)
                Using img = Pix.LoadFromFile(imagePath)
                    Using page = engine.Process(img)
                        Return page.GetText().Trim()
                    End Using
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("OCR failed: " & ex.Message)
            Return ""
        End Try
    End Function

End Module
