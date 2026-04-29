Imports System.IO
Imports System.Text
Imports System.Text.RegularExpressions
Imports TesseractOCR
Imports TesseractOCR.Enums
Imports WIA
'Imports Tesseract
Public Module ChequeOCRModule
    Private OCREngine As TesseractOCR.Engine = Nothing
    Private IsOCRInitialized As Boolean = False

    Public Sub InitializeOCR(Optional ByVal tessDataPath As String = "")
        Try
            ' Determine tessdata path
            Dim dataPath As String = ""
            If String.IsNullOrEmpty(tessDataPath) Then
                ' Default paths - adjust based on your installation
                Dim possiblePaths = {
                    "./tessdata",
                    "./tessdata",
                    "C:\Program Files\Tesseract-OCR\tessdata",
                    "C:\tessdata"
                }

                For Each path In possiblePaths
                    If Directory.Exists(path) Then
                        dataPath = path
                        Exit For
                    End If
                Next
            Else
                dataPath = tessDataPath
            End If

            If String.IsNullOrEmpty(dataPath) Then
                Throw New Exception("Tessdata directory not found. Please install Tesseract OCR and provide tessdata path.")
            End If

            ' Initialize Tesseract engine
            OCREngine = New TesseractOCR.Engine(dataPath, "eng", EngineMode.Default)

            ' Configure for cheque/document processing
            OCREngine.SetVariable("tessedit_char_whitelist", "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz/-: ")
            OCREngine.SetVariable("load_system_dawg", "0")
            OCREngine.SetVariable("load_freq_dawg", "0")

            IsOCRInitialized = True
            Console.WriteLine("OCR Engine initialized successfully")

        Catch ex As Exception
            IsOCRInitialized = False
            Throw New Exception("OCR initialization failed: " & ex.Message, ex)
        End Try
    End Sub


    Public Function ExtractChequeNumber12(ByVal imagePath As String) As ChequeOCRResult
        Dim result As New ChequeOCRResult()
        Try
            If Not IsOCRInitialized Then
                InitializeOCR()
            End If
            Using img = New Bitmap(imagePath)
                ' Preprocess image for better OCR
                Dim processedImage = PreprocessImageForOCR(img)
                Dim tempPath = Path.GetTempFileName() & ".png"
                processedImage.Save(tempPath, Imaging.ImageFormat.Png)

                Using Pix.Image.LoadFromFile(imagePath)
                    Using page = OCREngine.Process(Pix.Image.LoadFromFile(imagePath))
                        result.RawText = page.AltoText()
                        result.Confidence = page.MeanConfidence()
                        result.ChequeNumber = FindChequeNumberInText(result.RawText)
                        '==================

                    End Using
                End Using

                File.Delete(tempPath)
            End Using

        Catch ex As Exception
            result.ErrorMessage = ex.Message
        End Try

        Return result
    End Function


    Public Function ExtractChequeNumber1(ByVal imagePath As String) As ChequeOCRResult
        Dim result As New ChequeOCRResult()

        'Try
        '    If Not IsOCRInitialized Then
        '        InitializeOCR()
        '    End If

        '    ' SIMPLEST: Process directly from the original file
        '    Using page = OCREngine.Process(imagePath)
        '        result.RawText = page.BoxText()
        '        result.Confidence = page.MeanConfidence()
        '        result.ChequeNumber = FindChequeNumberInText(result.RawText)
        '    End Using

        'Catch ex As Exception
        '    result.ErrorMessage = ex.Message
        'End Try

        Return result
    End Function
    ' MAIN FUNCTION - Try this version first
    Public Function ExtractChequeNumber(ByVal imagePath As String) As ChequeOCRResult
        Dim result As New ChequeOCRResult()

        Try
            If Not IsOCRInitialized Then
                InitializeOCR()
            End If

            ' SIMPLEST APPROACH - Process directly from file
            Using page As Page = OCREngine.Process(Pix.Image.LoadFromFile(imagePath))
                result.RawText = page.BoxText()
                result.Confidence = page.MeanConfidence()
                result.ChequeNumber = FindChequeNumberInText(result.RawText)
            End Using

        Catch ex As Exception
            result.ErrorMessage = "OCR Error: " & ex.Message
        End Try

        Return result
    End Function

    ' Alternative for region extraction
    Public Function ExtractTextFromRegion(ByVal imagePath As String, ByVal region As Rectangle) As ChequeOCRResult
        Dim result As New ChequeOCRResult()

        Try
            If Not IsOCRInitialized Then
                InitializeOCR()
            End If

            Using image As Bitmap = New Bitmap(imagePath)
                Using croppedImage As Bitmap = CropImage(image, region)
                    ' Save cropped region to temp file
                    Dim tempPath As String = Path.GetTempFileName() & ".png"
                    croppedImage.Save(tempPath, Imaging.ImageFormat.Png)

                    ' Process the temp file
                    Using page As Page = OCREngine.Process(Pix.Image.LoadFromFile(imagePath))
                        result.RawText = page.BoxText()
                        result.Confidence = page.MeanConfidence()
                        result.ChequeNumber = FindChequeNumberInText(result.RawText)
                    End Using

                    File.Delete(tempPath)
                End Using
            End Using

        Catch ex As Exception
            result.ErrorMessage = ex.Message
        End Try

        Return result
    End Function


    'Public Function ExtractTextFromRegion1(ByVal imagePath As String, ByVal region As Rectangle) As ChequeOCRResult
    '    Dim result As New ChequeOCRResult()

    '    Try
    '        If Not IsOCRInitialized Then
    '            InitializeOCR()
    '        End If

    '        Using image As Bitmap = New Bitmap(imagePath)
    '            ' Crop to specific region
    '            Using croppedImage As Bitmap = CropImage(image, region)
    '                ' Preprocess for better OCR
    '                Dim processedImage = PreprocessImageForOCR(croppedImage)
    '                Dim tempPath As String = Path.GetTempFileName() & ".png"
    '                processedImage.Save(tempPath, Imaging.ImageFormat.Png)

    '                Using pix = pix.LoadFromFile(tempPath)
    '                    Using page = OCREngine.Process(pix)
    '                        result.RawText = page.BoxText()
    '                        result.Confidence = page.MeanConfidence()
    '                        result.ChequeNumber = FindChequeNumberInText(result.RawText)
    '                    End Using
    '                End Using

    '                File.Delete(tempPath)
    '            End Using
    '        End Using

    '    Catch ex As Exception
    '        result.ErrorMessage = ex.Message
    '    End Try

    '    Return result
    'End Function


    Private Function PreprocessImageForOCR(ByVal originalImage As Bitmap) As Bitmap
        ' Basic image preprocessing for better OCR results
        Dim processedImage As New Bitmap(originalImage.Width, originalImage.Height)

        Using g As Graphics = Graphics.FromImage(processedImage)
            g.DrawImage(originalImage, 0, 0, originalImage.Width, originalImage.Height)
        End Using

        ' You can add more preprocessing here:
        ' - Convert to grayscale
        ' - Increase contrast
        ' - Remove noise
        ' - Deskew image

        Return processedImage
    End Function

    Public Function ExtractAllChequeInfo(ByVal text As String) As ChequeExtractionResult
        Dim result As New ChequeExtractionResult()
        result.RawText = text

        If String.IsNullOrEmpty(text) Then Return result

        ' 1. Extract Account Number (AC:480-8022624-4-0010-0)
        Dim accountPatterns As String() = {
        "AC[:\s]*([\d\-]+)",                    ' AC:480-8022624-4-0010-0
        "Account[:\s]*([\d\-]+)",               ' Account:480-8022624-4-0010-0
        "\b\d{3}-\d{7}-\d-\d{4}-\d\b"          ' 480-8022624-4-0010-0 format
    }

        For Each pattern In accountPatterns
            Dim match As Match = Regex.Match(text, pattern, RegexOptions.IgnoreCase)
            If match.Success Then
                result.AccountNumber = If(match.Groups.Count > 1, match.Groups(1).Value, match.Value)
                Exit For
            End If
        Next

        ' 2. Extract ID Number (I.D.960156586)
        Dim idPatterns As String() = {
        "I\.D\.\s*(\d+)",                      ' I.D.960156586
        "ID\s*[:\s]*(\d+)",                    ' ID:960156586
        "Identification\s*[:\s]*(\d+)",        ' Identification:960156586
        "\b\d{9}\b"                            ' 9-digit number (960156586)
    }

        For Each pattern In idPatterns
            Dim match As Match = Regex.Match(text, pattern, RegexOptions.IgnoreCase)
            If match.Success Then
                result.IDNumber = If(match.Groups.Count > 1, match.Groups(1).Value, match.Value)
                Exit For
            End If
        Next

        ' 3. Extract Customer Name (MOHAMED ABD SHAKER NAJJAR)
        Dim namePatterns As String() = {
        "^(.*?)\s*AC[:\s]",                    ' Text before "AC:"
        "^(.*?)\s*I\.D\.",                     ' Text before "I.D."
        "([A-Z][A-Z\s]{5,}?)\s*(?:AC|I\.D\.)" ' Uppercase name before AC or I.D.
    }

        For Each pattern In namePatterns
            Dim match As Match = Regex.Match(text, pattern, RegexOptions.Multiline)
            If match.Success AndAlso match.Groups.Count > 1 Then
                Dim name = match.Groups(1).Value.Trim()
                If name.Length > 3 AndAlso Not name.Contains("AC") AndAlso Not name.Contains("I.D.") Then
                    result.AccountOwnerName = name
                    Exit For
                End If
            End If
        Next

        ' 4. Extract Pharmacy Name (QALOILIA NAER ALHAYAT PHARMACY)
        Dim pharmacyPatterns As String() = {
        "Tel\.\s*\d[\d\s]+\s*^(.*?PHARMACY)",  ' Text before PHARMACY after Tel
        "([A-Z][A-Z\s]{5,}PHARMACY)",          ' Uppercase text ending with PHARMACY
        "صيدلية\s*(.*?)\s*Tel",                ' Arabic text between صيدلية and Tel
        "QALOILIA.*?PHARMACY"                  ' Specific pharmacy name pattern
    }

        For Each pattern In pharmacyPatterns
            Dim match As Match = Regex.Match(text, pattern, RegexOptions.Multiline Or RegexOptions.IgnoreCase)
            If match.Success Then
                result.Address = If(match.Groups.Count > 1, match.Groups(1).Value, match.Value).Trim()
                Exit For
            End If
        Next

        ' 5. Extract Phone Number (970599392764)
        Dim phonePatterns As String() = {
        "Tel\.\s*([\d\s]+)",                   ' Tel. 970599392764
        "Telephone[:\s]*([\d\s]+)",            ' Telephone: 970599392764
        "\b970\d{9}\b",                        ' 970XXXXXXXXX format
        "\b\d{12}\b"                           ' 12-digit number
    }

        For Each pattern In phonePatterns
            Dim match As Match = Regex.Match(text, pattern, RegexOptions.IgnoreCase)
            If match.Success Then
                Dim phone = If(match.Groups.Count > 1, match.Groups(1).Value, match.Value)
                result.PhoneNumber = Regex.Replace(phone, "[^\d]", "") ' Remove non-digits
                Exit For
            End If
        Next

        ' 6. Extract Bank Name (QUDS BANK)
        Dim bankPatterns As String() = {
        "^(.*BANK)\s*$",                       ' Text ending with BANK at line end
        "BANK\s*(.*?)$",                       ' Text after BANK at end
        "QUDS\s*BANK"                          ' Specific bank name
    }

        For Each pattern In bankPatterns
            Dim match As Match = Regex.Match(text, pattern, RegexOptions.Multiline Or RegexOptions.IgnoreCase)
            If match.Success Then
                result.BankName = match.Value.Trim()
                Exit For
            End If
        Next

        ' 7. Look for cheque number in various formats
        Dim chequePatterns As String() = {
        "Cheque\s*No[:\s]*(\d+)",              ' Cheque No: 123456
        "Chq\s*No[:\s]*(\d+)",                 ' Chq No: 123456
        "Number[:\s]*(\d+)",                   ' Number: 123456
        "No\.\s*(\d+)",                        ' No. 123456
        "\b\d{6,8}\b"                          ' 6-8 digit standalone number
    }

        For Each pattern In chequePatterns
            Dim match As Match = Regex.Match(text, pattern, RegexOptions.IgnoreCase)
            If match.Success Then
                Dim number = If(match.Groups.Count > 1, match.Groups(1).Value, match.Value)
                number = Regex.Replace(number, "[^\d]", "")
                If number.Length >= 5 Then
                    result.ChequeNumber = number
                    Exit For
                End If
            End If
        Next

        Return result
    End Function

    Public Function ExtractChequeInfo(ByVal imagePath As String) As ChequeExtractionResult
        Dim result As New ChequeExtractionResult()

        Try
            If Not IsOCRInitialized Then
                InitializeOCR()
            End If

            Using page = OCREngine.Process(Pix.Image.LoadFromFile(imagePath))
                Dim rawText As String = page.AltoText()
                Dim confidence As Single = page.MeanConfidence()

                ' Extract all information from the text
                result = ExtractAllChequeInfo(rawText)
                result.RawText = rawText
            End Using
            Using img = New Bitmap(imagePath)
                ' Preprocess image for better OCR
                Dim processedImage = PreprocessImageForOCR(img)
                Dim tempPath = Path.GetTempFileName() & ".png"
                processedImage.Save(tempPath, Imaging.ImageFormat.Png)

                Using Pix.Image.LoadFromFile(imagePath)
                    Using page = OCREngine.Process(Pix.Image.LoadFromFile(imagePath))
                        result.RawText = page.AltoText()
                        result.Confidence = page.MeanConfidence()
                        result.ChequeNumber = FindChequeNumberInText(result.RawText)
                        '==================

                    End Using
                End Using

                File.Delete(tempPath)
            End Using
        Catch ex As Exception
            result.RawText = "OCR Error: " & ex.Message
        End Try

        Return result
    End Function

    Private Function FindChequeNumberInText(ByVal text As String) As String
        If String.IsNullOrEmpty(text) Then Return "Not Found"

        ' Enhanced cheque number patterns
        Dim patterns As String() = {
            "\b\d{6,10}\b",                    ' 6-10 digit number
            "Cheque\s*No[:\s]*(\d+)",          ' Cheque No: 123456
            "Chq\s*No[:\s]*(\d+)",             ' Chq No: 123456
            "Number[:\s]*(\d+)",               ' Number: 123456
            "No\.\s*(\d+)",                    ' No. 123456
            "\b\d{3}[- ]?\d{3}[- ]?\d{4}\b",   ' 123-456-7890 pattern
            "[Cc]heque\s*#?\s*(\d+)"           ' Cheque #123456
        }

        For Each pattern In patterns
            Dim match As Match = Regex.Match(text, pattern, RegexOptions.IgnoreCase)
            If match.Success Then
                Dim number As String = If(match.Groups.Count > 1, match.Groups(1).Value, match.Value)
                ' Clean the number
                number = Regex.Replace(number, "[^\d]", "")
                If number.Length >= 6 Then ' Valid cheque number length
                    Return number
                End If
            End If
        Next

        Return "Not Found"
    End Function

    Private Function CropImage(ByVal originalImage As Bitmap, ByVal cropArea As Rectangle) As Bitmap
        ' Ensure crop area is within image bounds
        cropArea.Intersect(New Rectangle(0, 0, originalImage.Width, originalImage.Height))

        If cropArea.Width <= 0 OrElse cropArea.Height <= 0 Then
            Return originalImage
        End If

        Dim croppedImage As New Bitmap(cropArea.Width, cropArea.Height)
        Using g = Graphics.FromImage(croppedImage)
            g.DrawImage(originalImage, New Rectangle(0, 0, cropArea.Width, cropArea.Height),
                       cropArea, GraphicsUnit.Pixel)
        End Using
        Return croppedImage
    End Function

    Public Function DetectChequeRegions(ByVal imagePath As String) As Dictionary(Of String, Rectangle)
        Dim regions As New Dictionary(Of String, Rectangle)

        Using image As Bitmap = New Bitmap(imagePath)
            Dim width As Integer = image.Width
            Dim height As Integer = image.Height

            ' Common cheque regions (adjust based on your cheque format)
            regions.Add("ChequeNumber_TopRight", New Rectangle(width - 300, 30, 250, 40))
            regions.Add("ChequeNumber_Bottom", New Rectangle(50, height - 100, 200, 40))
            regions.Add("Amount", New Rectangle(width - 250, 120, 200, 40))
            regions.Add("Date", New Rectangle(50, 50, 150, 30))
            regions.Add("Payee", New Rectangle(100, 150, 400, 40))
            regions.Add("Signature", New Rectangle(100, height - 120, 300, 80))
            regions.Add("MICR_Line", New Rectangle(50, height - 50, width - 100, 30))
        End Using

        Return regions
    End Function



    Public Function ExtractChequeInfoFromAlto(ByVal imagePath As String) As ChequeExtractionResult
        Dim result As New ChequeExtractionResult()

        Try
            If Not IsOCRInitialized Then
                InitializeOCR()
            End If

            Using page = OCREngine.Process(Pix.Image.LoadFromFile(imagePath))
                Dim altoXml As String = page.AltoText
                Dim extractedText = ParseAltoXml(altoXml)

                result = ExtractAllChequeInfo(extractedText)
                result.RawText = extractedText
            End Using

        Catch ex As Exception
            result.RawText = "OCR Error: " & ex.Message
        End Try

        Return result
    End Function

    Private Function ParseAltoXml(ByVal altoXml As String) As String
        Try
            ' Simple XML parsing to extract CONTENT attributes
            Dim textBuilder As New StringBuilder()

            ' Extract all CONTENT values from String elements
            Dim contentMatches = System.Text.RegularExpressions.Regex.Matches(
            altoXml,
            "CONTENT=""([^""]*)""",
            System.Text.RegularExpressions.RegexOptions.IgnoreCase)

            For Each match As System.Text.RegularExpressions.Match In contentMatches
                If match.Groups.Count > 1 Then
                    Dim content = match.Groups(1).Value
                    If Not String.IsNullOrEmpty(content) Then
                        textBuilder.Append(content)
                        textBuilder.Append(" ")
                    End If
                End If
            Next

            Return textBuilder.ToString().Trim()

        Catch ex As Exception
            Return "XML parsing error: " & ex.Message
        End Try
    End Function

    Public Function ExtractAllChequeInfoAlto(ByVal text As String) As ChequeExtractionResult
        Dim result As New ChequeExtractionResult()
        result.RawText = text

        If String.IsNullOrEmpty(text) Then Return result

        ' Clean the text first (remove extra spaces, etc.)
        text = System.Text.RegularExpressions.Regex.Replace(text, "\s+", " ").Trim()

        Console.WriteLine($"Processing text: {text}")

        ' Extract Account Number (AC:480-8022624-4-0010-0)
        Dim accountMatch = System.Text.RegularExpressions.Regex.Match(text, "AC:(\d{3}-\d{7}-\d-\d{4}-\d)", System.Text.RegularExpressions.RegexOptions.IgnoreCase)
        If accountMatch.Success Then
            result.AccountNumber = accountMatch.Groups(1).Value
        End If

        ' Extract ID Number (I.D.960156586) - note the OCR read "1D" instead of "I.D."
        Dim idMatch = System.Text.RegularExpressions.Regex.Match(text, "(?:I\.D\.|1D)\s*(\d+)", System.Text.RegularExpressions.RegexOptions.IgnoreCase)
        If idMatch.Success Then
            result.IDNumber = idMatch.Groups(1).Value
        Else
            ' Try to find 9-digit number
            Dim nineDigitMatch = System.Text.RegularExpressions.Regex.Match(text, "\b(\d{9})\b")
            If nineDigitMatch.Success Then
                result.IDNumber = nineDigitMatch.Groups(1).Value
            End If
        End If

        ' Extract Customer Name (look for all caps name before AC:)
        Dim nameMatch = System.Text.RegularExpressions.Regex.Match(text, "([A-Z][A-Z\s]+?)\s*AC:", System.Text.RegularExpressions.RegexOptions.IgnoreCase)
        If nameMatch.Success Then
            result.Address = nameMatch.Groups(1).Value.Trim()
        Else
            ' Alternative: Look for MOHAMED ABD SHAKER NAJJAR pattern
            Dim namePattern = "([A-Z]+\s+[A-Z]+\s+[A-Z]+\s+[A-Z]+)"
            nameMatch = System.Text.RegularExpressions.Regex.Match(text, namePattern)
            If nameMatch.Success Then
                result.Address = nameMatch.Groups(1).Value.Trim()
            End If
        End If

        ' Extract Cheque Number (from account number or look for other patterns)
        If Not String.IsNullOrEmpty(result.AccountNumber) Then
            ' Extract middle part from account number: 480-8022624-4-0010-0
            Dim parts = result.AccountNumber.Split("-"c)
            If parts.Length >= 2 Then
                result.ChequeNumber = parts(1) ' This gives 8022624
            End If
        End If

        ' Try to find other cheque number patterns
        If String.IsNullOrEmpty(result.ChequeNumber) OrElse result.ChequeNumber = "Not Found" Then
            Dim chequeMatch = System.Text.RegularExpressions.Regex.Match(text, "Cheque\s*No[:\s]*(\d+)", System.Text.RegularExpressions.RegexOptions.IgnoreCase)
            If chequeMatch.Success Then
                result.ChequeNumber = chequeMatch.Groups(1).Value
            End If
        End If

        ' Extract Phone Number
        Dim phoneMatch = System.Text.RegularExpressions.Regex.Match(text, "Tel\.\s*(\d+)", System.Text.RegularExpressions.RegexOptions.IgnoreCase)
        If phoneMatch.Success Then
            result.PhoneNumber = phoneMatch.Groups(1).Value
        End If

        ' Extract Bank Name
        Dim bankMatch = System.Text.RegularExpressions.Regex.Match(text, "QUDS\s*BANK", System.Text.RegularExpressions.RegexOptions.IgnoreCase)
        If bankMatch.Success Then
            result.BankName = bankMatch.Value
        End If

        Return result
    End Function

    'Public Sub CheckAvailablePageMethods()
    '    Try
    '        If Not IsOCRInitialized Then
    '            InitializeOCR()
    '        End If

    '        ' Create a small test image
    '        Using testBitmap As New Bitmap(100, 100)
    '            Using g = Graphics.FromImage(testBitmap)
    '                g.Clear(Color.White)
    '                g.DrawString("TEST", New Font("Arial", 12), Brushes.Black, 10, 10)
    '            End Using

    '            Using page = OCREngine.Process(testBitmap)
    '                Dim methods As New List(Of String)
    '                Dim properties As New List(Of String)

    '                ' Check methods
    '                For Each method In page.GetType().GetMethods()
    '                    If method.DeclaringType = page.GetType() Then
    '                        methods.Add(method.Name)
    '                    End If
    '                Next

    '                ' Check properties
    '                For Each prop In page.GetType().GetProperties()
    '                    properties.Add(prop.Name)
    '                Next

    '                MessageBox.Show($"Available Methods: {String.Join(", ", methods.Distinct().OrderBy(Function(x) x))}" &
    '                          Environment.NewLine & Environment.NewLine &
    '                          $"Available Properties: {String.Join(", ", properties.Distinct().OrderBy(Function(x) x))}")
    '            End Using
    '        End Using

    '    Catch ex As Exception
    '        MessageBox.Show("Error checking methods: " & ex.Message)
    '    End Try
    'End Sub

    Public Sub DisposeOCR()
        If OCREngine IsNot Nothing Then
            OCREngine.Dispose()
            OCREngine = Nothing
        End If
        IsOCRInitialized = False
    End Sub
End Module