Imports System.IO

Public Module EnhancedTwainOperations
    Public Function ScanAndProcessCheques(Optional ByVal DestinationFolder As String = "",
                                         Optional ByVal PreviewPictureBox As PictureBox = Nothing,
                                         Optional ByVal AutoExtractChequeNumber As Boolean = True) As List(Of ChequeInfo)

        ' Initialize OCR once
        If AutoExtractChequeNumber Then
            ChequeOCRModule.InitializeOCR()
        End If

        Dim scannedFiles = TwainOperations.ScanImagesToEditor(
            ImageType:=".jpg",
            CloseScannerUIAfterImageTransfer:=True,
            DestinationFolder:=DestinationFolder,
            PreviewPictureBox:=PreviewPictureBox)

        Dim cheques As New List(Of ChequeInfo)

        For Each filePath In scannedFiles
            Dim chequeInfo As New ChequeInfo With {
                .filePath = filePath,
                .FileName = Path.GetFileName(filePath),
                .ScanDate = DateTime.Now
            }

            If AutoExtractChequeNumber Then
                ' Extract cheque number using OCR
                Dim ocrResult = ChequeOCRModule.ExtractChequeNumber(filePath)
                chequeInfo.ChequeNumber = ocrResult.ChequeNumber
                chequeInfo.OCRConfidence = ocrResult.Confidence
                chequeInfo.IsProcessed = ocrResult.IsSuccessful

                ' Auto-rename file if cheque number found
                If ocrResult.IsSuccessful AndAlso ocrResult.ChequeNumber <> "Not Found" Then
                    AutoRenameChequeFile(chequeInfo, ocrResult.ChequeNumber)
                End If
            End If

            cheques.Add(chequeInfo)
        Next

        Return cheques
    End Function

    Private Sub AutoRenameChequeFile(ByRef chequeInfo As ChequeInfo, ByVal chequeNumber As String)
        Try
            Dim directory = Path.GetDirectoryName(chequeInfo.FilePath)
            Dim extension = Path.GetExtension(chequeInfo.FilePath)
            Dim newFileName = $"Cheque_{chequeNumber}_{DateTime.Now:yyyyMMdd_HHmmss}{extension}"
            Dim newFilePath = Path.Combine(directory, newFileName)

            If Not File.Exists(newFilePath) Then
                File.Move(chequeInfo.FilePath, newFilePath)
                chequeInfo.FilePath = newFilePath
                chequeInfo.FileName = newFileName
            End If
        Catch ex As Exception
            ' Log error but don't stop processing
            Console.WriteLine("Auto-rename failed: " & ex.Message)
        End Try
    End Sub
End Module