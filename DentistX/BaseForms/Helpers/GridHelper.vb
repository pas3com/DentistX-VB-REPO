
Imports System.Data.SqlClient
Imports System.Drawing
Imports System.IO
Imports System.Linq
Imports System.Text
Imports System.Text.RegularExpressions
Imports System.Threading
Imports System.Windows.Forms
Imports Dapper
Imports DevExpress.CodeParser

Imports Infragistics.Win.UltraWinGrid

Module GridHelper


#Region "Update Four Tables"

#Region "GridTRTClass"


#Region "Read Implant Data"

    Public Function FormatImplantData(cellValue As String) As String
        If String.IsNullOrEmpty(cellValue) Then Return String.Empty

        Dim lines = cellValue.Split(New String() {vbCrLf, vbCr, vbLf}, StringSplitOptions.RemoveEmptyEntries)
        If lines.Length = 0 Then Return String.Empty

        Dim implantName As String = ""
        Dim dimensions As String = ""
        Dim dateStr As String = ""

        ' Parse the data
        For Each line In lines
            Dim trimmedLine = line.Trim()

            If String.IsNullOrEmpty(trimmedLine) Then Continue For

            ' Check if it's a date
            Dim parsedDate As DateTime
            If DateTime.TryParse(trimmedLine, parsedDate) Then
                dateStr = parsedDate.ToString("dd/MM/yyyy")
                Continue For
            End If

            ' Check if it contains dimension indicators (X, x, ×)
            If trimmedLine.Contains("X") OrElse trimmedLine.Contains("x") OrElse trimmedLine.Contains("×") Then
                ' This is likely dimensions - format to Length * Diameter
                dimensions = FormatDimensions(trimmedLine)
                Continue For
            End If

            ' If not date or dimensions, it's the implant name
            If String.IsNullOrEmpty(implantName) Then
                implantName = trimmedLine
            End If
        Next

        ' Build the formatted result
        Dim result As New StringBuilder()

        If Not String.IsNullOrEmpty(implantName) Then
            result.AppendLine(implantName)
        Else
            result.AppendLine("Unknown Implant")
        End If

        If Not String.IsNullOrEmpty(dimensions) Then
            result.AppendLine(dimensions)
        Else
            result.AppendLine("No dimensions")
        End If

        If Not String.IsNullOrEmpty(dateStr) Then
            result.AppendLine(dateStr)
        Else
            result.AppendLine("No date")
        End If

        Return result.ToString().Trim()
    End Function

    Private Function FormatDimensions(dimensionStr As String) As String
        ' Clean up the dimension string
        Dim cleanStr = dimensionStr.Trim()

        ' Replace various dimension separators with asterisk
        cleanStr = cleanStr.Replace("X", "*").Replace("x", "*").Replace("×", "*")

        ' Remove any spaces around the asterisk
        cleanStr = cleanStr.Replace(" * ", "*").Replace(" *", "*").Replace("* ", "*")

        ' Split into parts
        Dim parts = cleanStr.Split("*"c)
        If parts.Length <> 2 Then Return dimensionStr ' Return original if not standard format

        ' Try to parse as numbers to handle decimal formatting
        Dim length As Decimal
        Dim diameter As Decimal

        If Decimal.TryParse(parts(0).Trim(), length) AndAlso Decimal.TryParse(parts(1).Trim(), diameter) Then
            Return $"{length} * {diameter}"
        Else
            Return $"{parts(0).Trim()} * {parts(1).Trim()}"
        End If
    End Function

    ' Alternative more robust version if the above doesn't work well
    Public Function FormatImplantDataAlternative(cellValue As String) As String
        If String.IsNullOrEmpty(cellValue) Then Return String.Empty

        Dim lines = cellValue.Split(New String() {vbCrLf, vbCr, vbLf}, StringSplitOptions.RemoveEmptyEntries)
        If lines.Length = 0 Then Return String.Empty

        Dim implantName As String = ""
        Dim dimensions As String = ""
        Dim dateStr As String = ""

        ' Process each line
        For i As Integer = 0 To lines.Length - 1
            Dim line = lines(i).Trim()
            If String.IsNullOrEmpty(line) Then Continue For

            ' Try to identify content type
            If i = 0 Then
                ' First line is usually implant name
                implantName = line
            ElseIf IsLikelyDate(line) Then
                dateStr = FormatDate(line)
            ElseIf IsLikelyDimensions(line) Then
                dimensions = FormatDimensions(line)
            Else
                ' Fallback: if we have empty dimensions, assume this is dimensions
                If String.IsNullOrEmpty(dimensions) Then
                    dimensions = FormatDimensions(line)
                ElseIf String.IsNullOrEmpty(dateStr) Then
                    ' Could be date in non-standard format
                    dateStr = FormatDate(line)
                End If
            End If
        Next

        ' Build formatted result
        Return $"{implantName}{vbCrLf}{dimensions}{vbCrLf}{dateStr}"
    End Function

    Private Function IsLikelyDate(text As String) As Boolean
        ' Check for date patterns
        If text.Contains("/") OrElse text.Contains("-") OrElse text.Contains("\") Then
            Dim tempDate As DateTime
            Return DateTime.TryParse(text, tempDate)
        End If
        Return False
    End Function

    Private Function IsLikelyDimensions(text As String) As Boolean
        ' Check for dimension patterns (contains X, x, × or numbers with separators)
        Return text.Contains("X") OrElse text.Contains("x") OrElse text.Contains("×") OrElse
           (text.Any(AddressOf Char.IsDigit) AndAlso (text.Contains(" ") OrElse text.Contains("*")))
    End Function

    Private Function FormatDate(dateText As String) As String
        Dim parsedDate As DateTime
        If DateTime.TryParse(dateText, parsedDate) Then
            Return parsedDate.ToString("dd/MM/yyyy")
        End If
        Return dateText ' Return original if can't parse
    End Function
#End Region

       Public Sub UpsertPatientToothTrt(ByVal patientID As Integer, ByVal QrtrID As Integer, ByVal QrtrAddress As Integer,
                                     ByVal QrtrTable As String, ByVal QrtrColumnName As String, ByVal QrtrColumnValue As String,
                                     ByVal toothName As String, ByVal treatment As (OldTrt As String, TrtDate As Date?))

        Dim ResultInfo As String = ""
        Dim InvalidResult As String = ""
        Dim InsertSucc As String = ""
        Dim InsertFail As String = ""
        Dim NoUpdate As String = ""
        Dim UpdateSucc As String = ""
        Dim UpdateFail As String = ""


        ' Extract old treatment and date from the provided treatment string
        'Dim result = ExtractOldTrtAndDate(treatment)
        Dim result = treatment
        Dim TrtDate As Date? = result.TrtDate
        Dim oldtreat As String = result.OldTrt
        Dim newTreat As String = GetNewTrt(oldtreat)

        ' Display a message for debugging purposes
        If TrtDate.HasValue Then
            ResultInfo = $"ExtractOldTrtAndDate Result{vbCrLf} OldTrt: {treatment}, Date: {TrtDate.Value.ToString("dd/MM/yyyy")}{vbCrLf} for {toothName} "
            Dim oldToothTRT As Patient_ToothTrt = Nothing
            Dim toothTrtDATA As New Patient_ToothTrtDATA
            ' Generate the tooth's full name and ordinal number
            Dim toothFullName As String = GetToothFullName(toothName)
            Dim toothNum As Byte = GetToothNum(toothName)
            Dim TrtID As Integer = 0
            TrtID = toothTrtDATA.GetToothTrtIDByPID_TNum_Treat(patientID, toothNum, newTreat)
            If TrtID > 0 Then
                ' Create instances of the data layer and model classes
                Dim TRT As New Patient_ToothTrt With {.ToothTrtID = TrtID,
                                                      .PatientID = patientID,
                                                      .ToothNum = toothNum}
                oldToothTRT = toothTrtDATA.Select_RecordByTrtIDByTNum(TRT)

                ' Check if a record already exists for the given patient and tooth

                If oldToothTRT IsNot Nothing Then
                    Dim newToothTRT As New Patient_ToothTrt With {
                                    .ToothTrtID = oldToothTRT.ToothTrtID,
                                    .PatientID = oldToothTRT.PatientID,
                                    .ShapeID = GetShapeIDByTrt(newTreat),
                                    .ToothNum = toothNum,
                                    .ToothName = toothFullName,
                                    .Treat = newTreat,
                                    .TreatDate = If(TrtDate.HasValue, TrtDate.Value, Nothing),
                                    .BorderColor = ColorTranslator.ToHtml(Color.Black),
                                    .BorderThickness = 1,
                                    .FillColor = ColorTranslator.ToHtml(GetDefaultTrtColor(newTreat)),
                                    .Finished = 0,
                                    .LVL = GetLVL(newTreat),
                                    .PropertyName = GetTrtShape(newTreat),
                                    .TreatDetails = "",
                                    .TreatEndDate = Nothing,
                                    .TreatmentType = "One Stage",
                                    .TreatNotes = "",
                                    .TreatPlan = "",
                                    .QrtrAddress = 0,
                                    .QrtrID = 0,
                                    .QrtrTable = "",
                                    .QrtrColumnName = "",
                                    .QrtrColumnValue = "",
                                    .IsExternal = False,
                                    .ExternalClinicName = Nothing,
                                    .ExternalTreatmentDate = Nothing,
                                    .IsPaid = False
                                }
                    If oldToothTRT.Equals(newToothTRT) Then
                        NoUpdate = $"UpsertPatientToothTrt{vbCrLf} No Need to update Patient_ToothTrt{vbCrLf} for {toothName}."
                        Exit Sub
                    End If
                    Dim success = toothTrtDATA.Update(oldToothTRT, newToothTRT)
                    If Not success Then
                        UpdateFail = $"UpsertPatientToothTrt{vbCrLf} Failed to update Patient_ToothTrt."
                    Else
                        UpdateSucc = $"UpsertPatientToothTrt{vbCrLf} Patient_ToothTrt Updated for Tooth'{toothNum}' with '{newTreat}'."
                    End If
                End If
            Else
                ' Insert a new record if no existing record is found
                Dim newRecord As New Patient_ToothTrt With {
                        .PatientID = patientID,
                        .ShapeID = GetShapeIDByTrt(newTreat),
                        .ToothNum = toothNum,
                        .ToothName = toothFullName,
                        .Treat = newTreat,
                        .TreatDate = If(TrtDate.HasValue, TrtDate.Value, Nothing),
                        .BorderColor = ColorTranslator.ToHtml(Color.Black),
                        .BorderThickness = 1,
                        .FillColor = ColorTranslator.ToHtml(GetDefaultTrtColor(newTreat)),
                        .Finished = 0,
                        .LVL = GetLVL(newTreat),
                        .PropertyName = GetTrtShape(newTreat),
                        .TreatDetails = "",
                        .TreatEndDate = Nothing,
                        .TreatmentType = "One Stage",
                        .TreatNotes = "",
                        .TreatPlan = "",
                        .QrtrAddress = 0,
                        .QrtrID = 0,
                        .QrtrTable = "",
                        .QrtrColumnValue = "",
                        .QrtrColumnName = "",
                        .IsExternal = False,
                        .ExternalClinicName = Nothing,
                        .ExternalTreatmentDate = Nothing,
                        .IsPaid = False
                    }
                Dim success = toothTrtDATA.Add(newRecord)
                If Not success Then
                    InsertFail = $"UpsertPatientToothTrt{vbCrLf} Failed to insert into Patient_ToothTrt for {toothName} ."
                Else
                    InsertSucc = $"UpsertPatientToothTrt{vbCrLf} Patient_ToothTrt Insert for Tooth'{toothNum}' with '{newTreat}' is Done."
                End If
            End If
        Else
            InvalidResult = $"UpsertPatientToothTrt Result{vbCrLf} Invalid stored value format for treatment: {treatment}"
        End If
        'Dim ResultInfo As String = ""
        'Dim InvalidResult As String = ""
        'Dim InsertSucc As String = ""
        'Dim InsertFail As String = ""
        'Dim NoUpdate As String = ""
        'Dim UpdateSucc As String = ""
        'Dim UpdateFail As String = ""
        LogToFile($"{InvalidResult}{ResultInfo}{vbCrLf}{NoUpdate}{vbCrLf}{UpdateSucc}{vbCrLf}{UpdateFail}{vbCrLf}{InsertSucc}{vbCrLf}{InsertFail}")
    End Sub


    Public Function UpdateTreatINQrtrTable(patientID As Integer, toothNum As Integer, QrtrTable As String, QrtrID As Integer, celladdress As Integer, treat As String) As Boolean
        Return False
    End Function

    Public Function DeleteTreatAndStylesINQrtrTable(patientID As Integer, toothNum As Integer, QrtrTable As String, QrtrID As Integer, celladdress As Integer, treat As String) As Boolean
        Return False
    End Function


    Public Function UpdateTreatINQrtrTable1(patientID As Integer, toothNum As Integer, QrtrTable As String, QrtrID As Integer, celladdress As Integer, treat As String) As Boolean
        Return False
    End Function


    Public Function DelTreatFromQuadrantTables(patientID As Integer, toothNum As Integer, treat As String) As Boolean
        Return True
    End Function



    Public Sub DelFromPatient_ToothTrt(ByVal patientID As Integer, ByVal toothNum As String, ByVal treatment As String)
        Dim oldTRT As Patient_ToothTrt = Nothing
        Dim TrtDATA As New Patient_ToothTrtDATA
        ' Generate the tooth's full name and ordinal number

        Dim TrtID As Integer = 0
        TrtID = TrtDATA.GetToothTrtIDByPID_TNum_Treat(patientID, toothNum, treatment)
        oldTRT = TrtDATA.Select_RecordByTrtIDByTNum(New Patient_ToothTrt With {.ToothTrtID = TrtID, .PatientID = patientID, .ToothNum = toothNum})
        If TrtDATA.Delete(oldTRT) Then
            MsgBox($"Treatment : {oldTRT.Treat} has been deleted")
        End If
    End Sub

    Public Sub DelGridTrt(ByVal table As String, ByVal patientID As Integer, ByVal colName As String, ByVal oldTrt As String)
    End Sub

    Public Sub AddTreatFrmGridsToChart(ByVal patientID As Integer, ByVal toothName As String, ByVal treatment As String, Optional qrtr As Qrtrs = Nothing)
        Dim ResultInfo As String = ""
        Dim InvalidResult As String = ""
        Dim InsertSucc As String = ""
        Dim InsertFail As String = ""
        Dim NoUpdate As String = ""
        Dim UpdateSucc As String = ""
        Dim UpdateFail As String = ""


        ' Extract old treatment and date from the provided treatment string
        Dim result = ExtractOldTrtAndDate(treatment)

        If result.OldTrt.Contains("IMPLANT") Then
            treatment = "IMPLANT"
        Else
            treatment = result.OldTrt
        End If

        ' Display a message for debugging purposes
        If Not String.IsNullOrEmpty(treatment) Then
            Dim oldTRT As Patient_ToothTrt = Nothing
            Dim TrtDATA As New Patient_ToothTrtDATA
            ' Generate the tooth's full name and ordinal number
            Dim toothFullName As String = GetToothFullName(toothName)
            Dim toothNum As Byte = GetToothNum(toothName)
            Dim TrtID As Integer = 0
            TrtID = TrtDATA.GetToothTrtIDByPID_TNum_Treat(patientID, toothNum, treatment)
            If TrtID > 0 Then
                ' Create instances of the data layer and model classes
                Dim TRT As New Patient_ToothTrt With {.ToothTrtID = TrtID,
                                                      .PatientID = patientID,
                                                      .ToothNum = toothNum}
                oldTRT = TrtDATA.Select_RecordByTrtIDByTNum(TRT)

                ' Check if a record already exists for the given patient and tooth

                If oldTRT IsNot Nothing Then
                    Dim newTRT As New Patient_ToothTrt With {
                                    .ToothTrtID = oldTRT.ToothTrtID,
                                    .PatientID = oldTRT.PatientID,
                                    .ShapeID = GetShapeIDByTrt(GetNewTrt(treatment)),
                                    .ToothNum = oldTRT.ToothNum,
                                    .ToothName = oldTRT.ToothName,
                                    .Treat = GetNewTrt(treatment),
                                    .LVL = GetLVL(GetNewTrt(treatment)),
                                    .TreatDate = Now.Date,
                                    .BorderColor = ColorTranslator.ToHtml(Color.Black),
                                    .BorderThickness = 1,
                                    .FillColor = ColorTranslator.ToHtml(GetDefaultTrtColor(GetNewTrt(treatment))),
                                    .Finished = 0,
                                    .PropertyName = GetTrtShape(GetNewTrt(treatment)),
                                    .TreatDetails = "",
                                    .TreatEndDate = Nothing,
                                    .TreatmentType = "One Stage",
                                    .TreatNotes = "",
                                    .TreatPlan = "",
                                    .QrtrAddress = 0,
                                    .QrtrColumnValue = "",
                                    .QrtrColumnName = "",
                                    .QrtrID = 0,
                                    .QrtrTable = ""
                                     }
                    If oldTRT.Equals(newTRT) Then
                        NoUpdate = $"UpsertPatientToothTrt{vbCrLf} No Need to update Patient_ToothTrt{vbCrLf} for {toothName}."
                        Exit Sub
                    End If
                    Dim success = TrtDATA.Update(oldTRT, newTRT)
                    If Not success Then
                        UpdateFail = $"UpsertPatientToothTrt{vbCrLf} Failed to update Patient_ToothTrt."
                    Else
                        UpdateSucc = $"UpsertPatientToothTrt{vbCrLf} Patient_ToothTrt Updated for Tooth'{toothNum}' with '{treatment}'."
                    End If
                Else
                    InvalidResult = $"UpsertPatientToothTrt Result{vbCrLf} Invalid stored value format for treatment: {treatment}"

                End If
            Else
                ' Insert a new record if no existing record is found
                Dim newRecord As New Patient_ToothTrt With {
                .PatientID = patientID,
                .ShapeID = GetShapeIDByTrt(GetNewTrt(treatment)),
                .ToothNum = toothNum,
                .ToothName = toothFullName,
                .Treat = GetNewTrt(GetNewTrt(treatment)),
                .LVL = GetLVL(GetNewTrt(treatment)),
                .TreatDate = Now.Date,
                .BorderColor = ColorTranslator.ToHtml(Color.Black),
                .BorderThickness = 1,
                .FillColor = ColorTranslator.ToHtml(GetOrigOldTrtColor(GetNewTrt(treatment))),
                .Finished = 0,
                .PropertyName = GetOldTrtShape(GetNewTrt(treatment)),
                .TreatDetails = "",
                .TreatEndDate = Nothing,
                .TreatmentType = "One Stage",
                .TreatNotes = "",
                .TreatPlan = "",
                .QrtrAddress = 0,
                .QrtrColumnValue = "",
                .QrtrColumnName = "",
                .QrtrID = 0,
                .QrtrTable = ""
            }
                Dim success = TrtDATA.Add(newRecord)
                If Not success Then
                    InsertFail = $"UpsertPatientToothTrt{vbCrLf} Failed to insert into Patient_ToothTrt for {toothName} ."
                Else
                    InsertSucc = $"UpsertPatientToothTrt{vbCrLf} Patient_ToothTrt Insert for Tooth'{toothNum}' with '{treatment}' is Done."
                End If
            End If
        End If

        'Dim ResultInfo As String = ""
        'Dim InvalidResult As String = ""
        'Dim InsertSucc As String = ""
        'Dim InsertFail As String = ""
        'Dim NoUpdate As String = ""
        'Dim UpdateSucc As String = ""
        'Dim UpdateFail As String = ""

        MessageBox.Show($"{InvalidResult}{ResultInfo}{vbCrLf}{NoUpdate}{vbCrLf}{UpdateSucc}{vbCrLf}{UpdateFail}{vbCrLf}{InsertSucc}{vbCrLf}{InsertFail}")
    End Sub


    Public Function IsOldTreatDuplictByGrid(grid As UltraGrid, patientID As Integer, toothColumn As String, treatment As String) As Boolean
        ' Iterate through all rows in the specified UltraGrid
        For Each row As UltraGridRow In grid.Rows
            Dim result1 = ExtractOldTrtAndDate(treatment)
            ' Check if the patient ID matches
            If CInt(row.Cells("PatientID").Value) = patientID Then
                ' Check if the same treatment exists in the same tooth column
                Dim existingTreatment As String = row.Cells(toothColumn).Value?.ToString()
                Dim result2 = ExtractOldTrtAndDate(existingTreatment)
                'If existingTreatment IsNot Nothing AndAlso existingTreatment.Equals(treatment, StringComparison.OrdinalIgnoreCase) Then
                '    Return True ' Duplicate found
                'End If
                If result2.OldTrt IsNot Nothing AndAlso result2.OldTrt.Equals(result1.OldTrt, StringComparison.OrdinalIgnoreCase) Then
                    Return True ' Duplicate found
                End If
            End If
        Next
        Return False ' No duplicate found

    End Function

    Public Function IsOldTreatDuplicate(table As String, patientID As Integer, toothColumn As String, treatment As String) As Boolean
        ' Define the connection string
        Dim connectionString As String = DentistXDATA.GetConnection.ConnectionString
        Dim result = ExtractOldTrtAndDate(treatment)
        Dim trt As String = result.OldTrt

        ' Query to check for existing treatment using LIKE
        Dim query As String = $"SELECT 1 FROM {table} WHERE PatientID = @patientID AND {toothColumn} = @treatment"

        ' Use a database connection to execute the query
        Using connection As New SqlClient.SqlConnection(connectionString)
            connection.Open()
            Using command As New SqlClient.SqlCommand(query, connection)
                ' Add parameters to avoid SQL injection
                command.Parameters.AddWithValue("@patientID", patientID)
                command.Parameters.AddWithValue("@treatment", treatment) ' $"%{treatment}%") ' Wildcards for LIKE

                ' Execute the query and return the result
                Return command.ExecuteScalar() IsNot Nothing
            End Using
        End Using
    End Function



    Public Function IsNewTreatDuplicate(patientID As Integer, toothName As String, treatment As String) As Boolean ', PropertyName As String) As Boolean
        Dim toothNum As Byte = GetToothNum(toothName) ' Convert tooth name to tooth number
        Dim TrtDATA As New Patient_ToothTrtDATA
        Dim existingTreatments = TrtDATA.Get_TreatmentsByPatientAndTooth(patientID, toothNum)

        ' Check if the specified treatment already exists
        Return existingTreatments.Any(Function(t) t.Treat = treatment)

        Dim connectionString As String = DentistXDATA.GetConnection.ConnectionString
        '===============
        ' Initialize the connection
        Using connection As New SqlConnection(connectionString)
            ' Open the connection
            connection.Open()

            ' Example query to find TrtID
            Dim query As String = "SELECT ToothTrtID 
                                   FROM Patient_ToothTrt 
                                   WHERE PatientID = @PatientID 
                                   AND ToothName = @ToothName 
                                   AND ToothNum = @ToothNum 
                                   AND Treat = @Treat"
            'AND PropertyName = @PropertyName  "

            ' Parameters for the query
            Dim parameters = New With {.PatientID = patientID,
                                       .ToothName = toothName,
                                       .ToothNum = toothNum,
                                       .Treat = treatment} ',
            '.PropertyName = PropertyName}

            ' Execute the query using Dapper
            Dim existingTrtID = connection.QueryFirstOrDefault(Of Integer?)(query, parameters)
        End Using
    End Function

    Public Function ExtractOldTrtAndDate(ByVal oldTrt As String) As (OldTrt As String, TrtDate As Date?)
        If String.IsNullOrEmpty(oldTrt) Then
            Return ("", Nothing)
        End If

        Dim parts = oldTrt.Split({vbCrLf}, StringSplitOptions.None) ' StringSplitOptions.RemoveEmptyEntries)
        If parts.Length < 2 Then
            Return ("", Nothing)
        End If

        If parts.Length = 4 Then
            Dim extractedOldTrt = parts(0).Trim()
            Dim extractedDate As Date
            If Date.TryParseExact(parts(3).Trim(), "dd-MM-yyyy", Globalization.CultureInfo.InvariantCulture, Globalization.DateTimeStyles.None, extractedDate) Then
                Return (extractedOldTrt, extractedDate)
            End If
        Else
            Dim extractedOldTrt = parts(0).Trim()
            Dim extractedDate As Date
            If Date.TryParseExact(parts(1).Trim(), "dd-MM-yyyy", Globalization.CultureInfo.InvariantCulture, Globalization.DateTimeStyles.None, extractedDate) Then
                Return (extractedOldTrt, extractedDate)
            End If
        End If




        ' Return as invalid if date is not in expected format
        Return ("", Nothing)
    End Function


    Public Function GetToothFullName1(ByVal toothname As String) As String
        If String.IsNullOrEmpty(toothname) OrElse toothname.Length < 3 Then Return ""

        Dim direction As String = toothname.Substring(0, 1)
        Dim position As String = toothname.Substring(1, 1)
        Dim number As String = toothname.Substring(2)

        Dim directionFull As String = If(direction = "R", "RIGHT", If(direction = "L", "LEFT", ""))
        Dim positionFull As String = If(position = "U", "UPPER", If(position = "D", "LOWER", ""))

        If String.IsNullOrEmpty(directionFull) OrElse String.IsNullOrEmpty(positionFull) Then Return toothname

        Return $"{directionFull} {positionFull} {number}"
    End Function

    Public Function GetToothFullName(ByVal toothname As String, Optional alternateQuadrantWordOrder As Boolean = True) As String
        If String.IsNullOrEmpty(toothname) OrElse toothname.Length < 3 Then Return ""

        Dim direction As String = toothname.Substring(0, 1)
        Dim position As String = toothname.Substring(1, 1)
        Dim number As String = toothname.Substring(toothname.Length - 1) ' toothname.Substring(2)

        Dim directionFull As String = If(direction = "R", "RIGHT", If(direction = "L", "LEFT", ""))
        Dim positionFull As String = If(position = "U", "UPPER", If(position = "D", "LOWER", ""))

        If String.IsNullOrEmpty(directionFull) OrElse String.IsNullOrEmpty(positionFull) Then Return ""

        If alternateQuadrantWordOrder Then
            Return $"{positionFull} {directionFull} {number}"
        End If
        Return $"{directionFull} {positionFull} {number}"
    End Function

    Private Function GetQuarterID(grid As UltraGrid, rowIndex As Integer) As Integer
        Dim tablePrefix As String = grid.Name.Substring(0, 2) ' RU, LU, RD, or LD
        Dim primaryKeyColumnName As String = tablePrefix & "ID"

        ' Get the row using the row index
        Dim row As UltraGridRow = grid.Rows(rowIndex)

        ' Return the primary key value from the hidden column
        Return CInt(row.Cells(primaryKeyColumnName).Value)
    End Function

    Public Function GetToothNum(ByVal toothName As String) As Byte
        If String.IsNullOrEmpty(toothName) OrElse toothName.Length < 3 Then Return 0

        ' Extract parts of the toothName
        Dim direction As String = toothName.Substring(0, 1)
        Dim position As String = toothName.Substring(1, 1)
        Dim numberPart As String = toothName.Substring(2)

        ' Parse the numeric part
        Dim number As Byte
        If Not Byte.TryParse(numberPart, number) Then Return 0 ' Return 0 if the number part is invalid

        ' Determine the left portion based on direction and position
        Dim leftPortion As Byte = 0
        If direction = "R" AndAlso position = "U" Then
            leftPortion = 1
        ElseIf direction = "L" AndAlso position = "U" Then
            leftPortion = 2
        ElseIf direction = "L" AndAlso position = "D" Then
            leftPortion = 3
        ElseIf direction = "R" AndAlso position = "D" Then
            leftPortion = 4
        End If

        ' If invalid direction or position, return 0
        If leftPortion = 0 Then Return 0

        ' Combine the left portion (as the tens) with the number
        Return CByte((leftPortion * 10) + number)
    End Function


    Public Function GetGridToothColumnName(gridName As String, columnIndex As Integer) As String
        ' Validate that the column index is greater than 1, as indexes 0 and 1 are primary keys
        If columnIndex <= 1 Then
            Throw New ArgumentException("Column index must be greater than 1, as the first two columns are primary keys.")
        End If

        ' Determine the prefix based on the grid name
        Dim prefix As String = String.Empty
        Select Case gridName.ToUpper()
            Case "LUULTRAGRID"
                prefix = "LU"
            Case "RUULTRAGRID"
                prefix = "RU"
            Case "LDULTRAGRID"
                prefix = "LD"
            Case "RDULTRAGRID"
                prefix = "RD"
            Case Else
                Throw New ArgumentException("Invalid grid name. Accepted names are LUULTRAGRID, RUULTRAGRID, LDULTRAGRID, RDULTRAGRID.")
        End Select

        ' Calculate the tooth number based on the column index
        Dim toothNumber As Integer = columnIndex - 1 ' Subtract 1 because index 0 and 1 are primary keys
        Return $"{prefix}{toothNumber}"
    End Function


    Public Function VerifyData(GRID As UltraGrid) As Boolean
        ' Check required fields
        If GRID.Rows.Count = 0 Then
            MsgBox("ID is required.", MsgBoxStyle.OkOnly, "Entry Error")
            Return False
        End If
        If PatientID = 0 OrElse PatientName.ToString() = "" Then
            MsgBox("PatientID is required.", MsgBoxStyle.OkOnly, "Entry Error")
            Return False
        End If
        Return True
    End Function



#End Region


    Private Function GetColumnIndex(columnName As String) As Integer
        ' Extract the numeric part from column name
        Dim match As Match = Regex.Match(columnName, "\d+")
        If match.Success Then
            Return Convert.ToInt32(match.Value) - 1 ' Convert tooth number to 0-based index
        End If
        Return -1 ' Return -1 if parsing fails
    End Function


    Public Function GetSmallestPrimaryKey(ByVal tableName As String, ByVal patientID As Integer) As Integer?
        ' Define the connection string (replace with your actual connection string)

        Dim connectionString As String = DentistXDATA.GetConnection.ConnectionString
        ' SQL query for fetching the smallest primary key (ID) based on the table and column
        Dim query As String = String.Format("
        -- First try to get the {0}ID where {0}1 is NULL or empty
        SELECT TOP 1 {0}ID
        FROM {1}
        WHERE PatientID = @PatientID
          AND ({0}1 IS NULL OR {0}1 = '')
        UNION ALL
        -- If none found, get {0}ID with the oldest date in {0}1
        SELECT TOP 1 {0}ID
        FROM {1}
        WHERE {0}1 IS NOT NULL
              AND {0}1 <> ''
              AND LEN({0}1) >= 10
              AND TRY_CAST(RIGHT({0}1, 10) AS DATE) IS NOT NULL
              AND PatientID = @PatientID
        ORDER BY {0}ID
        OFFSET 0 ROWS FETCH NEXT 1 ROWS ONLY", tableName, tableName)

        Using conn As New SqlConnection(connectionString)
            Try
                conn.Open()
                ' Execute the query using Dapper
                Dim result As Integer? = conn.QuerySingleOrDefault(Of Integer?)(query, New With {.PatientId = patientID})

                ' Return the result (ID) or Nothing if no result found
                Return result
            Catch ex As Exception
                ' Handle any exceptions that may occur
                MsgBox("Error: " & ex.Message)
                Return Nothing
            End Try
        End Using
    End Function



    'Add or Update Modified Version for Treats with same date on the same column
    Public Sub AddTreatToQuadrantTable(patientID As Integer, toothNum As Integer, toothName As String, treat As String)
        Exit Sub
#If False Then
        Dim trt As String = treat & vbCrLf & Format(Now.Date, "dd-MM-yyyy")
        Dim quadrantTable As String = GetQuadrantTable(toothNum)
        Dim toothColumn As String = GetToothColumn(toothNum)

        If String.IsNullOrEmpty(quadrantTable) OrElse String.IsNullOrEmpty(toothColumn) Then
            Throw New ArgumentException("Invalid ToothNum provided.")
        End If

        Dim primaryKeyName As String = $"{quadrantTable}ID"
        Dim columnName As String = $"{quadrantTable}{toothColumn}"

        Dim connection As New SqlConnection
        connection = DentistXDATA.GetConnection()
        Using connection
            connection.Open()

            ' Check for empty cells first
            Dim query As String = $"SELECT {primaryKeyName}, {columnName} FROM {quadrantTable} WHERE PatientID = @PatientID ORDER BY {primaryKeyName} ASC"
            Using command As New SqlCommand(query, connection)
                command.Parameters.AddWithValue("@PatientID", patientID)
                Using reader As SqlDataReader = command.ExecuteReader()
                    Dim oldestDateRowKey As Integer = -1
                    Dim oldestDate As Date? = Nothing
                    Dim hasEmptyCell As Boolean = False
                    Dim lowestIndexWithOldestDate As Integer = Integer.MaxValue ' Track the row index with the oldest date

                    While reader.Read()
                        Dim x As Integer = reader.Depth
                        If (columnName = "RU7" OrElse columnName = "LU7") AndAlso reader.Depth = 0 Then Continue While
                        If (columnName = "RU8" OrElse columnName = "LU8") AndAlso (reader.Depth = 0 OrElse reader.Depth = 1) Then Continue While
                        If (columnName = "RD7" OrElse columnName = "LD7") AndAlso reader.Depth = 3 Then Continue While
                        If (columnName = "RD8" OrElse columnName = "LD8") AndAlso (reader.Depth = 2 OrElse reader.Depth = 3) Then Continue While

                        Dim currentCellValue As String = If(IsDBNull(reader(columnName)), String.Empty, reader(columnName).ToString())

                        ' Check if the cell is empty
                        If String.IsNullOrWhiteSpace(currentCellValue) Then
                            Dim primaryKeyValue As Integer = Convert.ToInt32(reader(primaryKeyName))
                            Dim rowIndex As Integer = reader.Depth ' reader.GetInt32(reader.GetOrdinal(primaryKeyName))
                            reader.Close()

                            ' Update the empty cell
                            Dim updateQuery As String = $"UPDATE {quadrantTable} SET {columnName} = @Treat WHERE PatientID = @PatientID AND {primaryKeyName} = @PrimaryKeyValue"
                            Using updateCommand As New SqlCommand(updateQuery, connection)
                                updateCommand.Parameters.AddWithValue("@Treat", trt)
                                updateCommand.Parameters.AddWithValue("@PatientID", patientID)
                                updateCommand.Parameters.AddWithValue("@PrimaryKeyValue", primaryKeyValue)
                                updateCommand.ExecuteNonQuery()
                            End Using
                            Dim colIndex As Integer = GetToothColumnIndex(toothNum)

                            Dim cellAddress As Integer = (colIndex * 10) + (rowIndex + 1)
                            'Dim cellAddress As String = $"{quadrantTable}[{columnName}, {primaryKeyValue}]"

                            Debug.Print("Updated Cell Address: " & cellAddress)
                            MessageBox.Show("Updated Cell Address: " & cellAddress, "Cell Location", MessageBoxButtons.OK, MessageBoxIcon.Information)

                            hasEmptyCell = True
                            Exit While
                        Else
                            ' Check for the oldest date in the cell
                            Dim datePart As String = currentCellValue.Split(vbCrLf).LastOrDefault().Trim()
                            If Date.TryParseExact(datePart, "dd-MM-yyyy", Nothing, Globalization.DateTimeStyles.None, Nothing) Then
                                Dim cellDate As Date = Date.ParseExact(datePart, "dd-MM-yyyy", Nothing)
                                Dim rowIndex As Integer = reader.GetInt32(reader.GetOrdinal(primaryKeyName))

                                ' Track the oldest date and lowest index
                                If Not oldestDate.HasValue OrElse cellDate < oldestDate.Value Then
                                    oldestDate = cellDate
                                    oldestDateRowKey = rowIndex
                                    lowestIndexWithOldestDate = rowIndex
                                ElseIf cellDate = oldestDate.Value AndAlso rowIndex < lowestIndexWithOldestDate Then
                                    ' If dates are the same, prioritize the lowest index
                                    oldestDateRowKey = rowIndex
                                    lowestIndexWithOldestDate = rowIndex
                                End If
                            End If
                        End If
                    End While

                    reader.Close()

                    ' If no empty cell was found, update the oldest date at the lowest index
                    If Not hasEmptyCell AndAlso oldestDateRowKey <> -1 Then
                        Dim updateQuery As String = $"UPDATE {quadrantTable} SET {columnName} = @Treat WHERE PatientID = @PatientID AND {primaryKeyName} = @PrimaryKeyValue"
                        Using updateCommand As New SqlCommand(updateQuery, connection)
                            updateCommand.Parameters.AddWithValue("@Treat", trt)
                            updateCommand.Parameters.AddWithValue("@PatientID", patientID)
                            updateCommand.Parameters.AddWithValue("@PrimaryKeyValue", oldestDateRowKey)
                            updateCommand.ExecuteNonQuery()
                        End Using
                        Dim colIndex As Integer = GetToothColumnIndex(toothNum)
                        'Dim rowIndex As Integer = reader.GetInt32(reader.GetOrdinal(primaryKeyName))
                        'Dim cellAddress As String = $"{quadrantTable}  [{colIndex}, {rowIndex}]"
                        'Dim cellAddress As String = $"{quadrantTable}[{columnName}, {oldestDateRowKey}]"
                        'Debug.Print("Updated Cell Address: " & cellAddress)
                        'MessageBox.Show("Updated Cell Address: " & cellAddress, "Cell Location", MessageBoxButtons.OK, MessageBoxIcon.Information)

                    ElseIf Not hasEmptyCell Then
                        ' If no update was possible, notify the user
                        MessageBox.Show($"Unable to update {toothName}. Please check the data.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    End If
                End Using
            End Using
        End Using
#End If
    End Sub



#Region "Add Trt From Chart To Grid"
    Public QrtrTable As String
    Public QrtrID, QrtrAddress As Integer

    Public Function SaveTreatmentWithTransaction(PatientID As Integer, _toothTrt As Patient_ToothTrt,
                                      treatmentText As String, toothName As String, _propName As String, _formattedResult As String,
                                      txtTrtPrice As Double, txtPayValue As Double, isPaidChck As CheckBox, toothNums As List(Of Integer),
                                      OnePayAdded As Boolean, selectedToothTrtList As List(Of Patient_ToothTrt),
                                      Optional isMultiTooth As Boolean = False,
                                      Optional treatmentGroupID As Guid = Nothing,
                                      Optional isFirstInGroup As Boolean = True) As Boolean
        Dim saved As Boolean = False
        Dim canceled As Boolean = False
        Dim clsTrtsData As New Patient_TrtsDATA
        Dim toothTrtData As New Patient_ToothTrtDATA
        Dim Dx = New DentistXDATA
        Dim ConnectionString = Dx.GetConnectionString
        Dim oldTrt As String = If(_propName = "IMPLANT", _formattedResult, GetOldTrt(_toothTrt.Treat))

        Using conn As New SqlConnection(ConnectionString)
            conn.Open()
            Using trans As SqlTransaction = conn.BeginTransaction()
                Try
                    AddTreatFrmChartToGrids_Transactional(conn, trans, PatientID, _toothTrt.ToothNum, _toothTrt.ToothName, oldTrt)
                    _toothTrt.QrtrTable = ""
                    _toothTrt.QrtrID = 0
                    _toothTrt.QrtrAddress = 0
                    _toothTrt.QrtrColumnName = ""
                    _toothTrt.QrtrColumnValue = ""
                    If isMultiTooth Then
                        _toothTrt.ParentToothTrtID = -1
                        _toothTrt.IsMultiTrt = False
                        _toothTrt.TrtGroupID = treatmentGroupID
                    End If
                    ' Treatment value and payment
                    Dim trtValue As Double = Val(txtTrtPrice)
                    Dim payValue As Double = Val(txtPayValue)

                    ' Payment logic (unchanged)
                    Dim payNote As String = ""
                    If payValue > 0 OrElse trtValue > 0 Then
                        If payValue = trtValue Then
                            payNote = "Payed In Full"
                            isPaidChck.Checked = True
                        ElseIf payValue < trtValue AndAlso payValue > 0 Then
                            payNote = "Payed Partially"
                            isPaidChck.CheckState = True
                        ElseIf payValue > trtValue Then
                            payNote = "Payed With Advance Payment"
                            isPaidChck.Checked = True
                        End If
                    ElseIf trtValue = 0 AndAlso payValue = 0 Then
                        isPaidChck.Checked = False
                    End If

                    ' Save Patient_ToothTrt
                    _toothTrt.IsPaid = isPaidChck.Checked
                    If _toothTrt.IsExternal = True Then _toothTrt.IsPaid = True

                    'A new Try
                    '======================
                    'its here where i'm stuck
                    Dim MaxLvl As Integer = toothTrtData.GetTreatLVL(_toothTrt.PatientID, _toothTrt.ToothNum)
                    Dim currentLvl As Integer = _toothTrt.LVL
                    'check if treat is a normal one after high level one
                    If (MaxLvl > 4 AndAlso currentLvl < 4) AndAlso Not TrtSourceHelper.AllowLowLevelTreatOnChartDespiteHighMaxLevel(_toothTrt.PatientID, _toothTrt.ToothNum, _toothTrt.Treat, False) Then
                        MsgBox("You Cant Add a Normal Treat On High Level Treat....")
                        Return False
                    End If
                    ' Special case: implant (5) → extraction (4) or extraction (4) → implant (5)
                    If (MaxLvl > 4 AndAlso currentLvl = 4) Then ' OrElse (MaxLvl = 4 AndAlso currentLvl = 5) Then
                        ' 1. Finish old treatment
                        conn.Execute("
                                            UPDATE Patient_ToothTrt
                                            SET LVL = 4
                                            WHERE PatientID = @PatientID
                                              AND ToothNum = @ToothNum
                                              AND LVL > 4
                                        ", New With {
                                            .PatientID = _toothTrt.PatientID,
                                            .ToothNum = _toothTrt.ToothNum,
                                            .OldLvl = MaxLvl
                                        }, trans)


                    End If
                    '============================

                    'normal
                    If Not toothTrtData.AddTransactional(conn, trans, _toothTrt) Then
                        Throw New Exception($"Failed to save Treatment in Chart '{treatmentText}' for '{toothName}'.")
                    End If
                    ' Get the last inserted ToothTrtID
                    Dim lastToothTrtID As Integer = conn.ExecuteScalar(Of Integer)(
                            "SELECT TOP 1 ToothTrtID FROM Patient_ToothTrt ORDER BY ToothTrtID DESC",
                            transaction:=trans)

                    'If IsMultiTrt then update ParentToothTrtID and insert into Patient_TrtInfo
                    If isMultiTooth Then
                        'Update ParentToothTrtID
                        Dim masterToothID As Integer = conn.ExecuteScalar(Of Integer)(
                                                "SELECT MIN(ToothTrtID) FROM Patient_ToothTrt 
                                                 WHERE TrtGroupID = @TrtGroupID",
                                                New With {.TrtGroupID = treatmentGroupID},
                                                transaction:=trans)
                        ' Update all other teeth in group to point to first tooth
                        Dim rowsUpdated As Integer = conn.ExecuteScalar(
                                                "UPDATE Patient_ToothTrt 
                                                 SET ParentToothTrtID = @ParentToothTrtID 
                                                 WHERE TrtGroupID = @TrtGroupID 
                                                ",'  AND ToothTrtID <> @ParentToothTrtID
                                                New With {
                                                    .ParentToothTrtID = masterToothID,
                                                    .TrtGroupID = treatmentGroupID
                                                },
                                                transaction:=trans)
                        'Insert the record into Patient_TrtInfo
                        'select the inserted record from Patient_ToothTrt Select_RecordByTrtIDByTNum
                        ' Insert the record into Patient_TrtInfo
                        Dim Trt As Patient_ToothTrt = toothTrtData.Select_Record(New Patient_ToothTrt With {
                                                                                                    .ToothTrtID = lastToothTrtID,
                                                                                                    .PatientID = PatientID,
                                                                                                    .ToothName = toothName
                                                                                                }, trans) ' Pass the transaction here)

                        Trt.QrtrAddress = _toothTrt.QrtrAddress
                        Trt.QrtrColumnValue = _toothTrt.QrtrColumnValue
                        Trt.QrtrID = _toothTrt.QrtrID
                        Trt.QrtrTable = _toothTrt.QrtrTable
                        Trt.QrtrColumnName = _toothTrt.QrtrColumnName
                        ' Validate the record was found
                        If Trt Is Nothing Then
                            Throw New Exception("Failed to retrieve tooth treatment record")
                        End If

                        ' Insert with all required parameters
                        Dim rowsInserted As Integer = conn.ExecuteScalar(
                                                                            "INSERT INTO [dbo].[Patient_TrtInfo] (
                                                                            [PatientID], [ParentToothTrtID], [TrtGroupID], 
                                                                            [ToothNum], [ToothName], [TreatDate], [Treat],
                                                                            [TreatNotes], [IsExternal], [ExternalClinicName], 
                                                                            [ExternalTreatmentDate]
                                                                        ) VALUES (
                                                                            @PatientID, @ParentToothTrtID, @TrtGroupID, 
                                                                            @ToothNum, @ToothName, @TreatDate, @Treat, 
                                                                            @TreatNotes, @IsExternal, @ExternalClinicName, 
                                                                            @ExternalTreatmentDate
                                                                        )",
                                                                        New With {
                                                                            .PatientID = PatientID,
                                                                            .ParentToothTrtID = If(masterToothID > 0, masterToothID, DBNull.Value),
                                                                            .TrtGroupID = If(treatmentGroupID <> Guid.Empty, treatmentGroupID, DBNull.Value),
                                                                            .ToothNum = Trt.ToothNum,
                                                                            .ToothName = Trt.ToothName, ' Added missing parameter
                                                                            .TreatDate = Trt.TreatDate,
                                                                            .Treat = If(Trt.Treat, DBNull.Value),
                                                                            .TreatNotes = If(Trt.TreatNotes, DBNull.Value),
                                                                            .IsExternal = Trt.IsExternal,
                                                                            .ExternalClinicName = If(Trt.ExternalClinicName, DBNull.Value),
                                                                            .ExternalTreatmentDate = If(Trt.ExternalTreatmentDate.HasValue,
                                                                                                      Trt.ExternalTreatmentDate.Value,
                                                                                                      DBNull.Value)
                                                                        },
                                                                        transaction:=trans)
                    End If
                    ' Create accounting record for:
                    ' 1. Normal treatments, or
                    'Make sure its IN HOUSE Treat
                    If _toothTrt.IsExternal = False Then
                        ' 2. The FIRST tooth in a multi-tooth treatment group
                        If Not isMultiTooth OrElse isFirstInGroup Then
                            Dim detailText As String
                            Dim teeth As String = String.Join(",", toothNums)
                            If isMultiTooth AndAlso Not String.IsNullOrEmpty(teeth) Then
                                ' Use concatenated teeth string for multi-tooth treatments
                                detailText = _toothTrt.Treat & " ==>> " & teeth
                            Else
                                ' Use individual tooth name for single treatments
                                detailText = _toothTrt.Treat & " ==>> " & GetShortToothNameWithDash(_toothTrt.ToothNum)
                            End If
                            Dim clsPatientTrts As New Patient_Trts With {
                            .ToothTrtID = lastToothTrtID,
                            .Detail = detailText,
                            .PatientID = _toothTrt.PatientID,
                            .TrtDate = _toothTrt.TreatDate,
                            .TrtValue = trtValue,
                            .IsMultiTooth = isMultiTooth
                        }

                            If Not clsTrtsData.AddTransactional(conn, trans, clsPatientTrts) Then
                                Throw New Exception($"Failed to save Patient_Treatment '{treatmentText}' for '{toothName}'.")
                            End If
                        End If

                        ' Payment saving logic (unchanged)
                        If payValue > 0 Then
                            Dim lastTrtID As Integer = conn.ExecuteScalar(Of Integer)(
                            "SELECT TOP 1 TrtID FROM Patient_Trts ORDER BY TrtID DESC",
                            transaction:=trans)

                            Dim clsPatientPays As New Patient_Pays With {
                            .Notes = payNote,
                            .PatientID = _toothTrt.PatientID,
                            .PayDate = _toothTrt.TreatDate,
                            .PayValue = payValue,
                            .TrtID = lastTrtID
                        }

                            Dim clsPatientPaysData As New Patient_PaysDATA
                            If selectedToothTrtList.Count > 0 Then
                                If Not OnePayAdded Then
                                    If Not clsPatientPaysData.AddTransactional(conn, trans, clsPatientPays) Then
                                        Throw New Exception($"Failed to save Patient_Payement '{treatmentText}' for '{toothName}'.")
                                    End If
                                    OnePayAdded = True
                                End If
                            Else
                                If Not clsPatientPaysData.AddTransactional(conn, trans, clsPatientPays) Then
                                    Throw New Exception($"Failed to save Patient_Payement '{treatmentText}' for '{toothName}'.")
                                End If
                                OnePayAdded = False
                            End If
                        End If
                    End If

                    trans.Commit()
                    saved = True
                    canceled = False


                Catch ex As Exception
                    trans.Rollback()
                    MessageBox.Show(ex.Message, "Transaction Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    saved = False
                    canceled = True
                End Try
            End Using
        End Using

        Return saved
    End Function

    Public Function AddTreatFrmChartToGrids_Transactional(conn As SqlConnection, trans As SqlTransaction, patientID As Integer, toothNum As Integer, toothName As String, treat As String) As Boolean
        QrtrTable = ""
        QrtrID = 0
        QrtrAddress = 0
        Return False
    End Function

    ' Transaction-aware UpdateTreatment
    Private Function UpdateTreatment(connection As SqlConnection, trans As SqlTransaction,
                                 quadrantTable As String, primaryKeyName As String,
                                 columnName As String, patientID As Integer, trt As String,
                                 toothNum As Integer, emptyRows As List(Of Integer),
                                 oldestDateRowKey As Integer, frstID As Integer?,
                                 scndID As Integer?, thrdID As Integer?, frthID As Integer?) As Boolean

        If emptyRows.Count > 0 Then
            Dim minEmptyKey As Integer = emptyRows.Min()
            Dim rowIndex As Integer = GetRowIndexByPrimaryKey(minEmptyKey, frstID, scndID, thrdID, frthID)
            Dim cellAddress As Integer = (GetColIndexByToothNum(toothNum) * 10) + rowIndex

            Dim updateQuery As String = $"UPDATE {quadrantTable} SET {columnName} = @Treat WHERE PatientID = @PatientID AND {primaryKeyName} = @PrimaryKeyValue"
            Using updateCommand As New SqlCommand(updateQuery, connection, trans)
                updateCommand.Parameters.AddWithValue("@Treat", trt)
                updateCommand.Parameters.AddWithValue("@PatientID", patientID)
                updateCommand.Parameters.AddWithValue("@PrimaryKeyValue", minEmptyKey)
                If updateCommand.ExecuteNonQuery() > 0 Then
                    QrtrAddress = cellAddress
                    QrtrID = minEmptyKey
                    QrtrTable = quadrantTable
                    If trt.StartsWith("IMPLANT") Then
                        SetBackIMPfrmChartToGrid(cellAddress, patientID, quadrantTable, trt)
                    End If
                    Return True
                End If
            End Using

        ElseIf oldestDateRowKey <> -1 Then
            Dim rowIndex As Integer = GetRowIndexByPrimaryKey(oldestDateRowKey, frstID, scndID, thrdID, frthID)
            Dim cellAddress As Integer = (GetColIndexByToothNum(toothNum) * 10) + rowIndex

            Dim updateQuery As String = $"UPDATE {quadrantTable} SET {columnName} = @Treat WHERE PatientID = @PatientID AND {primaryKeyName} = @PrimaryKeyValue"
            Using updateCommand As New SqlCommand(updateQuery, connection, trans)
                updateCommand.Parameters.AddWithValue("@Treat", trt)
                updateCommand.Parameters.AddWithValue("@PatientID", patientID)
                updateCommand.Parameters.AddWithValue("@PrimaryKeyValue", oldestDateRowKey)
                If updateCommand.ExecuteNonQuery() > 0 Then
                    QrtrAddress = cellAddress
                    QrtrID = oldestDateRowKey
                    QrtrTable = quadrantTable
                    If trt.StartsWith("IMPLANT") Then
                        SetBackIMPfrmChartToGrid(cellAddress, patientID, quadrantTable, trt)
                    End If
                    Return True
                End If
            End Using
        End If

        Return False
    End Function

    ' Transaction-aware ProcessGeneralCase
    Private Function ProcessGeneralCase(connection As SqlConnection, trans As SqlTransaction,
                                    patientID As Integer, quadrantTable As String,
                                    primaryKeyName As String, columnName As String,
                                    trt As String, frstID As Integer?, scndID As Integer?,
                                    thrdID As Integer?, frthID As Integer?, toothNum As Integer) As Boolean

        Dim query As String = $"SELECT {primaryKeyName}, {columnName} FROM {quadrantTable} WHERE PatientID = @PatientID ORDER BY {primaryKeyName} ASC"
        Using command As New SqlCommand(query, connection, trans)
            command.Parameters.AddWithValue("@PatientID", patientID)
            Using reader As SqlDataReader = command.ExecuteReader()
                Dim readerResult = ProcessReader(reader, primaryKeyName, columnName)
                Dim emptyRows As List(Of Integer) = readerResult.emptyRows
                Dim oldestDateRowKey As Integer = readerResult.oldestDateRowKey
                Dim oldestDate As Date? = readerResult.oldestDate
                reader.Close()

                If UpdateTreatment(connection, trans, quadrantTable, primaryKeyName, columnName, patientID, trt, toothNum, emptyRows,
                               oldestDateRowKey, frstID, scndID, thrdID, frthID) Then
                    Return True
                End If
            End Using
        End Using

        Return False
    End Function

    'From Helpers Module
    Public Function AddTreatFrmChartToGrids(patientID As Integer, toothNum As Integer, toothName As String, treat As String) As Boolean
        QrtrTable = ""
        QrtrID = 0
        QrtrAddress = 0
        Return False
    End Function


    Private Function ProcessReader(reader As SqlDataReader, primaryKeyName As String, columnName As String) As (emptyRows As List(Of Integer), oldestDateRowKey As Integer, oldestDate As Date?)
        Dim emptyRows As New List(Of Integer)()
        Dim oldestDateRowKey As Integer = -1
        Dim oldestDate As Date? = Nothing

        While reader.Read()
            Dim primaryKeyValue As Integer = Convert.ToInt32(reader(primaryKeyName))
            Dim currentCellValue As String = If(IsDBNull(reader(columnName)), String.Empty, reader(columnName).ToString())

            If String.IsNullOrWhiteSpace(currentCellValue) Then
                emptyRows.Add(primaryKeyValue)
            Else
                Dim datePart As String = currentCellValue.Split(vbCrLf).LastOrDefault()?.Trim()
                If Date.TryParseExact(datePart, "dd-MM-yyyy", Nothing, Globalization.DateTimeStyles.None, Nothing) Then
                    Dim cellDate As Date = Date.ParseExact(datePart, "dd-MM-yyyy", Nothing)
                    If Not oldestDate.HasValue OrElse cellDate < oldestDate Then
                        oldestDate = cellDate
                        oldestDateRowKey = primaryKeyValue
                    End If
                End If
            End If
        End While

        Return (emptyRows, oldestDateRowKey, oldestDate)
    End Function
    Private Function UpdateTreatment(connection As SqlConnection,
                                quadrantTable As String,
                                primaryKeyName As String,
                                columnName As String,
                                patientID As Integer,
                                trt As String,
                                toothNum As Integer,
                                emptyRows As List(Of Integer),
                                oldestDateRowKey As Integer,
                                frstID As Integer?,
                                scndID As Integer?,
                                thrdID As Integer?,
                                frthID As Integer?) As Boolean
        If emptyRows.Count > 0 Then
            Dim minEmptyKey As Integer = emptyRows.Min()
            Dim rowIndex As Integer = GetRowIndexByPrimaryKey(minEmptyKey, frstID, scndID, thrdID, frthID)
            Dim cellAddress As Integer = (GetColIndexByToothNum(toothNum) * 10) + rowIndex

            Dim updateQuery As String = $"UPDATE {quadrantTable} SET {columnName} = @Treat WHERE PatientID = @PatientID AND {primaryKeyName} = @PrimaryKeyValue"
            Using updateCommand As New SqlCommand(updateQuery, connection)
                updateCommand.Parameters.AddWithValue("@Treat", trt)
                updateCommand.Parameters.AddWithValue("@PatientID", patientID)
                updateCommand.Parameters.AddWithValue("@PrimaryKeyValue", minEmptyKey)
                If updateCommand.ExecuteNonQuery() > 0 Then
                    QrtrAddress = cellAddress
                    QrtrID = minEmptyKey
                    QrtrTable = quadrantTable
                    If trt.StartsWith("IMPLANT") Then
                        SetBackIMPfrmChartToGrid(cellAddress, patientID, quadrantTable, trt)
                    End If
                    Return True
                End If
            End Using
        ElseIf oldestDateRowKey <> -1 Then
            Dim rowIndex As Integer = GetRowIndexByPrimaryKey(oldestDateRowKey, frstID, scndID, thrdID, frthID)
            Dim cellAddress As Integer = (GetColIndexByToothNum(toothNum) * 10) + rowIndex

            Dim updateQuery As String = $"UPDATE {quadrantTable} SET {columnName} = @Treat WHERE PatientID = @PatientID AND {primaryKeyName} = @PrimaryKeyValue"
            Using updateCommand As New SqlCommand(updateQuery, connection)
                updateCommand.Parameters.AddWithValue("@Treat", trt)
                updateCommand.Parameters.AddWithValue("@PatientID", patientID)
                updateCommand.Parameters.AddWithValue("@PrimaryKeyValue", oldestDateRowKey)
                If updateCommand.ExecuteNonQuery() > 0 Then
                    QrtrAddress = cellAddress
                    QrtrID = oldestDateRowKey
                    QrtrTable = quadrantTable
                    If trt.StartsWith("IMPLANT") Then
                        SetBackIMPfrmChartToGrid(cellAddress, patientID, quadrantTable, trt)
                    End If
                    Return True
                End If
            End Using
        End If
        Return False
    End Function
    Private Function ProcessGeneralCase(connection As SqlConnection, patientID As Integer, quadrantTable As String, primaryKeyName As String,
                                        columnName As String, trt As String, frstID As Integer?, scndID As Integer?, thrdID As Integer?,
                                        frthID As Integer?, toothNum As Integer) As Boolean
        Dim query As String = $"SELECT {primaryKeyName}, {columnName} FROM {quadrantTable} WHERE PatientID = @PatientID ORDER BY {primaryKeyName} ASC"
        Using command As New SqlCommand(query, connection)
            command.Parameters.AddWithValue("@PatientID", patientID)
            Using reader As SqlDataReader = command.ExecuteReader()
                Dim readerResult = ProcessReader(reader, primaryKeyName, columnName)
                Dim emptyRows As List(Of Integer) = readerResult.emptyRows
                Dim oldestDateRowKey As Integer = readerResult.oldestDateRowKey
                Dim oldestDate As Date? = readerResult.oldestDate
                reader.Close()

                If UpdateTreatment(connection, quadrantTable, primaryKeyName, columnName, patientID, trt, toothNum, emptyRows,
                                       oldestDateRowKey, frstID, scndID, thrdID, frthID) Then
                    Return True
                End If
            End Using
        End Using
        Return False
    End Function
    'Get first ID
    Public Function GetQrtrTblFirstID(patientID As Integer, toothNum As Integer) As Integer?
        Dim quadrantTable As String = GetQuadrantTable(toothNum)
        Dim toothColumn As String = GetToothColumn(toothNum)
        If String.IsNullOrEmpty(quadrantTable) OrElse String.IsNullOrEmpty(toothColumn) Then
            Throw New ArgumentException("Invalid ToothNum provided.")
        End If
        Dim primaryKeyName As String = $"{quadrantTable}ID"
        Dim columnName As String = $"{quadrantTable}{toothColumn}"

        Dim connection As New SqlConnection
        connection = DentistXDATA.GetConnection()
        Dim query As String = $"SELECT MIN({primaryKeyName}) AS First{primaryKeyName}
                                FROM (
                                        SELECT TOP (2) {primaryKeyName}
                                        FROM [dbo].[{quadrantTable}]
                                        WHERE PatientID = @PatientID
                                        ORDER BY {primaryKeyName}
                                    ) AS Top2IDs"

        Using connection
            Try
                connection.Open()
                ' Execute the query using Dapper
                Dim result As Integer? = connection.QuerySingleOrDefault(Of Integer?)(query, New With {.PatientId = patientID})

                ' Return the result (ID) or Nothing if no result found
                Return result
            Catch ex As Exception
                ' Handle any exceptions that may occur
                MsgBox("Error: " & ex.Message)
                Return Nothing
            End Try
        End Using
    End Function
    'Get second ID
    Public Function GetQrtrTblSecondID(patientID As Integer, toothNum As Integer) As Integer?
        Dim quadrantTable As String = GetQuadrantTable(toothNum)
        Dim toothColumn As String = GetToothColumn(toothNum)
        If String.IsNullOrEmpty(quadrantTable) OrElse String.IsNullOrEmpty(toothColumn) Then
            Throw New ArgumentException("Invalid ToothNum provided.")
        End If
        Dim primaryKeyName As String = $"{quadrantTable}ID"
        Dim columnName As String = $"{quadrantTable}{toothColumn}"

        Dim connection As New SqlConnection
        connection = DentistXDATA.GetConnection()
        Dim query As String = $"SELECT MAX({primaryKeyName}) AS Second{primaryKeyName}
                                FROM (
                                        SELECT TOP (2) {primaryKeyName}
                                        FROM [dbo].[{quadrantTable}]
                                        WHERE PatientID = @PatientID
                                        ORDER BY {primaryKeyName}
                                    ) AS Top2IDs"

        Using connection
            Try
                connection.Open()
                ' Execute the query using Dapper
                Dim result As Integer? = connection.QuerySingleOrDefault(Of Integer?)(query, New With {.PatientId = patientID})

                ' Return the result (ID) or Nothing if no result found
                Return result
            Catch ex As Exception
                ' Handle any exceptions that may occur
                MsgBox("Error: " & ex.Message)
                Return Nothing
            End Try
        End Using
    End Function
    'Get third ID
    Public Function GetQrtrTblThirdID(patientID As Integer, toothNum As Integer) As Integer?
        Dim quadrantTable As String = GetQuadrantTable(toothNum)
        Dim toothColumn As String = GetToothColumn(toothNum)
        If String.IsNullOrEmpty(quadrantTable) OrElse String.IsNullOrEmpty(toothColumn) Then
            Throw New ArgumentException("Invalid ToothNum provided.")
        End If
        Dim primaryKeyName As String = $"{quadrantTable}ID"
        Dim columnName As String = $"{quadrantTable}{toothColumn}"


        Dim connection As New SqlConnection
        connection = DentistXDATA.GetConnection()


        Dim query As String = $"SELECT MAX({primaryKeyName}) AS Third{primaryKeyName}
                                FROM (
                                        SELECT TOP (3) {primaryKeyName}
                                        FROM [dbo].[{quadrantTable}]
                                        WHERE PatientID = @PatientID
                                        ORDER BY {primaryKeyName}
                                    ) AS Top2IDs"



        Using connection
            Try
                connection.Open()
                ' Execute the query using Dapper
                Dim result As Integer? = connection.QuerySingleOrDefault(Of Integer?)(query, New With {.PatientId = patientID})

                ' Return the result (ID) or Nothing if no result found
                Return result
            Catch ex As Exception
                ' Handle any exceptions that may occur
                MsgBox("Error: " & ex.Message)
                Return Nothing
            End Try
        End Using
    End Function
    'Get fourth ID
    Public Function GetQrtrTblFourthID(patientID As Integer, toothNum As Integer) As Integer?
        Dim quadrantTable As String = GetQuadrantTable(toothNum)
        Dim toothColumn As String = GetToothColumn(toothNum)
        If String.IsNullOrEmpty(quadrantTable) OrElse String.IsNullOrEmpty(toothColumn) Then
            Throw New ArgumentException("Invalid ToothNum provided.")
        End If
        Dim primaryKeyName As String = $"{quadrantTable}ID"
        Dim columnName As String = $"{quadrantTable}{toothColumn}"

        Dim connection As New SqlConnection
        connection = DentistXDATA.GetConnection()


        Dim query As String = $"SELECT MAX({primaryKeyName}) AS Fourth{primaryKeyName}
                                FROM (
                                        SELECT TOP (4) {primaryKeyName}
                                        FROM [dbo].[{quadrantTable}]
                                        WHERE PatientID = @PatientID
                                        ORDER BY {primaryKeyName}
                                    ) AS Top2IDs"


        Using connection
            Try
                connection.Open()
                ' Execute the query using Dapper
                Dim result As Integer? = connection.QuerySingleOrDefault(Of Integer?)(query, New With {.PatientId = patientID})

                ' Return the result (ID) or Nothing if no result found
                Return result
            Catch ex As Exception
                ' Handle any exceptions that may occur
                MsgBox("Error: " & ex.Message)
                Return Nothing
            End Try
        End Using
    End Function
    Public Function GetRowIndexByPrimaryKey(primaryKey As Integer,
                                        frstID As Integer?,
                                        scndID As Integer?,
                                        thrdID As Integer?,
                                        frthID As Integer?) As Integer
        Select Case primaryKey
            Case frstID
                Return 0
            Case scndID
                Return 1
            Case thrdID
                Return 2
            Case frthID
                Return 3
            Case Else
                Throw New ArgumentException($"Invalid primary key value {primaryKey}. Expected one of: " &
                                       $"{frstID}, {scndID}, {thrdID}, {frthID}")
        End Select
    End Function
#End Region


    ' Helper function to determine quadrant table based on ToothNum
    Public Function GetQuadrantTable(toothNum As Integer) As String
        Select Case toothNum
            Case 21 To 28 : Return "LU"
            Case 11 To 18 : Return "RU"
            Case 31 To 38 : Return "LD"
            Case 41 To 48 : Return "RD"
                '
            Case 61 To 65 : Return "LU"
            Case 51 To 55 : Return "RU"
            Case 71 To 75 : Return "LD"
            Case 81 To 85 : Return "RD"
            Case Else : Return String.Empty
        End Select
    End Function

    Public Function GetUltraGrid(toothNum As Integer) As UltraGrid
        '' Ensure you have an instance of GridTRTClass to access the UltraGrid controls
        'Dim gridClass As New GridTRTClass

        'Select Case toothNum
        '    Case 21 To 28 : Return gridClass.LUUltraGrid
        '    Case 11 To 18 : Return gridClass.RUUltraGrid
        '    Case 31 To 38 : Return gridClass.LDUltraGrid
        '    Case 41 To 48 : Return gridClass.RDUltraGrid
        '    Case Else : Return Nothing
        'End Select
        Return Nothing
    End Function


    ' Helper function to determine tooth column based on ToothNum
    Public Function GetToothColumn(toothNum As Integer) As String
        Dim toothOrdinal As Integer = toothNum Mod 10 ' Extract the last digit
        If toothOrdinal >= 1 AndAlso toothOrdinal <= 8 Then
            Return toothOrdinal.ToString()
        End If
        Return String.Empty
    End Function

    ' Helper function to determine tooth column index based on ToothNum
    Public Function GetToothColumnIndex(toothNum As Integer) As Integer
        Dim toothOrdinal As Integer = toothNum Mod 10 ' Extract the last digit
        If toothOrdinal >= 1 AndAlso toothOrdinal <= 8 Then
            Return toothOrdinal
        End If
        Return 0
    End Function

    Public Function GetColIndexByToothNum(toothNum As Integer) As Integer
        Dim validFirstDigits As Integer() = {1, 2, 3, 4} ' Valid tooth quadrants
        Dim firstDigit As Integer = toothNum \ 10
        Dim lastDigit As Integer = toothNum Mod 10

        If validFirstDigits.Contains(firstDigit) AndAlso lastDigit >= 1 AndAlso lastDigit <= 8 Then
            Return lastDigit + 1
        End If

        Return 0 ' Or -1 to indicate invalid tooth number
    End Function

    Public Function SetCellTag(grid As String, toothNum As Byte) As Byte
        ' Validate input range (11-18)
        If toothNum < 11 OrElse toothNum > 18 Then
            Throw New ArgumentException("toothNum must be between 11 and 18", NameOf(toothNum))
        End If

        ' Convert based on grid prefix using explicit mapping
        Select Case grid.Substring(0, 2).ToUpper()
            Case "RU"
                Return toothNum ' 11-18
            Case "LU"
                Return CByte(toothNum + 10) ' 21-28
            Case "LD"
                Return CByte(toothNum + 20) ' 31-38
            Case "RD"
                Return CByte(toothNum + 30) ' 41-48
            Case Else
                Throw New ArgumentException("Grid name must start with RU, LU, LD, or RD", NameOf(grid))
        End Select
    End Function
#End Region


#Region "StyleImage"

    Public Sub UpdateImplant(patientID As Integer, implant As Boolean)
        Dim query As String = "UPDATE Patient SET Implant = @Implant WHERE PatientID = @PatientID"

        Using connection As New SqlConnection(My.Settings.DentistXConnectionString)
            connection.Open()
            connection.Execute(query, New With {
            .Implant = implant,
            .PatientID = patientID
        })
        End Using
    End Sub

    Public Sub UpdateOrho(patientID As Integer, ortho As Boolean)
        Dim query As String = "UPDATE Patient SET Ortho = @Ortho WHERE PatientID = @PatientID"

        Using connection As New SqlConnection(My.Settings.DentistXConnectionString)
            connection.Open()
            connection.Execute(query, New With {
            .Ortho = ortho,
            .PatientID = patientID
        })
        End Using
    End Sub

    Public Sub UpdateMobile(patientID As Integer, mobile As Boolean)
        Dim query As String = "UPDATE Patient SET Mobile = @Mobile WHERE PatientID = @PatientID"

        Using connection As New SqlConnection(My.Settings.DentistXConnectionString)
            connection.Open()
            connection.Execute(query, New With {
            .Mobile = mobile,
            .PatientID = patientID
        })
        End Using
    End Sub

    Public Sub UpdateStruc(patientID As Integer, struc As Boolean)
        Dim query As String = "UPDATE Patient SET Struc = @Struc WHERE PatientID = @PatientID"

        Using connection As New SqlConnection(My.Settings.DentistXConnectionString)
            connection.Open()
            connection.Execute(query, New With {
            .Struc = struc,
            .PatientID = patientID
        })
        End Using
    End Sub

    Public Sub UpdateTrt(patientID As Integer, trt As Boolean)
        Dim query As String = "UPDATE Patient SET Treat = @Treat WHERE PatientID = @PatientID"

        Using connection As New SqlConnection(My.Settings.DentistXConnectionString)
            connection.Open()
            connection.Execute(query, New With {
            .Treat = trt,
            .PatientID = patientID
        })
        End Using
    End Sub

    Private Sub UpdateStyles(img As Byte(), cellAddress As Integer, patientID As Integer, style As String, treatment As String)
        Dim colIndex As Integer
        colIndex = AddressToColumnRow(cellAddress).Column
        Dim table As String = String.Empty
        Dim toothColumn As String = String.Empty
        Select Case style
            Case "LU"
                table = "LUSTYLE"
                toothColumn = $"LU{colIndex}"
            Case "LD"
                table = "LDSTYLE"
                toothColumn = $"LD{colIndex}"
            Case "RU"
                table = "RUSTYLE"
                toothColumn = $"RU{colIndex}"
            Case "RD"
                table = "RDSTYLE"
                toothColumn = $"RD{colIndex}"
        End Select

        Try
            Dim Styles As New StyleImage
            Dim ID As Integer? = Styles.IsStyl(patientID, cellAddress, style)
            If ID.HasValue Then
                Styles.UpdateStyle(table, style, ID.Value, patientID, cellAddress, img)
                AddTreatFrmGridsToChart(patientID, toothColumn, treatment)
                UpdateImplant(patientID, True)
            Else
                Styles.InsertStyle(table, style, patientID, cellAddress, img)
                AddTreatFrmGridsToChart(patientID, toothColumn, treatment)
                UpdateImplant(patientID, True)
            End If



        Catch ex As System.Data.SqlClient.SqlException
            MsgBox(ex.Message)
        End Try
    End Sub
    Public Sub SetBackIMPfrmChartToGrid(cellAddress As Integer, patientID As Integer, style As String, treatment As String)
        If patientID <= 0 Then Exit Sub

        Dim img As Byte() = Nothing
        Dim imgResource As Image = Nothing
        Dim colIndex As Integer
        colIndex = AddressToColumnRow(cellAddress).Column
        Dim table As String = String.Empty
        Dim toothColumn As String = String.Empty
        Select Case style
            Case "LU"
                img = ModuleImages.imgToByteConverter(GetIMP("LU"))
                imgResource = GetIMP("LU")
                table = "LUSTYLE"
                toothColumn = $"LU{colIndex}"
            Case "LD"
                img = ModuleImages.imgToByteConverter(GetIMP("LD"))
                imgResource = GetIMP("LD")
                table = "LDSTYLE"
                toothColumn = $"LD{colIndex}"
            Case "RU"
                img = ModuleImages.imgToByteConverter(GetIMP("RU"))
                imgResource = GetIMP("RU")
                table = "RUSTYLE"
                toothColumn = $"RU{colIndex}"
            Case "RD"
                img = ModuleImages.imgToByteConverter(GetIMP("RD"))
                imgResource = GetIMP("RD")
                table = "RDSTYLE"
                toothColumn = $"RD{colIndex}"
        End Select
        Try
            Dim Styles As New StyleImage
            Dim ID As Integer? = Styles.IsStyl(patientID, cellAddress, style)
            If ID.HasValue Then
                Styles.UpdateStyle(table, style, ID.Value, patientID, cellAddress, img)
                'AddTreatFrmGridsToChart(patientID, toothColumn, treatment)
                UpdateImplant(patientID, True)
            Else
                Styles.InsertStyle(table, style, patientID, cellAddress, img)
                'AddTreatFrmGridsToChart(patientID, toothColumn, treatment)
                UpdateImplant(patientID, True)
            End If



        Catch ex As System.Data.SqlClient.SqlException
            MsgBox(ex.Message)
        End Try

        'UpdateStyles(img, cellAddress, patientID, style, treatment)

    End Sub



    Public Class StyleImage
        Public Sub UpdateStyle(table As String, style As String, ID As Integer, patientID As Integer, cellAddress As Integer, img As Byte())
            Dim idField As String = GetIdField(style)
            Dim query As String = $"UPDATE {table} SET BakImg = @Img WHERE PatientID = @PatientID AND CellAddres = @CellAddres AND {idField} = @ID"

            Using connection As New SqlConnection(My.Settings.DentistXConnectionString)
                connection.Open()
                connection.Execute(query, New With {
                .Img = img,
                .PatientID = patientID,
                .CellAddres = cellAddress,
                .ID = ID
            })
            End Using
        End Sub

        Public Sub InsertStyle(table As String, style As String, patientID As Integer, cellAddress As Integer, img As Byte())
            Dim query As String = $"INSERT INTO {table} (PatientID, CellAddres, BakImg) VALUES (@PatientID, @CellAddres, @Img)"

            Using connection As New SqlConnection(My.Settings.DentistXConnectionString)
                connection.Open()
                connection.Execute(query, New With {
                .PatientID = patientID,
                .CellAddres = cellAddress,
                .Img = img
            })
            End Using
        End Sub


        Public Function IsStyl(patientID As Integer, cellAddress As Integer, style As String) As Integer?
            Dim table As String
            Dim idField As String
            Select Case style
                Case "LU"
                    table = "LUSTYLE"
                    idField = "LUcellID"
                Case "LD"
                    table = "LDSTYLE"
                    idField = "LDcellID"
                Case "RU"
                    table = "RUSTYLE"
                    idField = "RUcellID"
                Case "RD"
                    table = "RDSTYLE"
                    idField = "RDcellID"
                Case Else
                    Return Nothing
            End Select

            Dim query As String = $"SELECT {idField} FROM {table} WHERE PatientID = @PatientID AND CellAddres = @CellAddres"
            Using connection As New SqlConnection(My.Settings.DentistXConnectionString)
                connection.Open()
                Dim result As Integer? = connection.QuerySingleOrDefault(Of Integer?)(query, New With {
                .PatientID = patientID,
                .CellAddres = cellAddress
            })
                Return result
            End Using
        End Function

        Public Function IsPL(patientID As Integer, cellAddress As Integer, PL As String) As Integer?
            Dim table As String
            Dim idField As String
            Select Case PL
                Case "LU"
                    table = "LUPL"
                    idField = "LUcellID"
                Case "LD"
                    table = "LDPL"
                    idField = "LDcellID"
                Case "RU"
                    table = "RUPL"
                    idField = "RUcellID"
                Case "RD"
                    table = "RDPL"
                    idField = "RDcellID"
                Case Else
                    Return Nothing
            End Select

            Dim query As String = $"SELECT {idField} FROM {table} WHERE PatientID = @PatientID AND CellAddres = @CellAddres"
            Using connection As New SqlConnection(My.Settings.DentistXConnectionString)
                connection.Open()
                Dim result As Integer? = connection.QuerySingleOrDefault(Of Integer?)(query, New With {
                .PatientID = patientID,
                .CellAddres = cellAddress
            })
                Return result
            End Using
        End Function
    End Class

    Private Function GetIdField(style_PL As String) As String
        Select Case style_PL
            Case "LU"
                Return "LUcellID"
            Case "LD"
                Return "LDcellID"
            Case "RU"
                Return "RUcellID"
            Case "RD"
                Return "RDcellID"
            Case Else
                Throw New ArgumentException("Invalid style")
        End Select
    End Function


    'Public Class StyleImage
    '    Public Shared Sub UpdateStyle(table As String, style As String, ID As Integer, patientID As Integer, cellAddress As Integer, img As Byte())
    '        Dim idField As String = GetIdField(style)
    '        Dim query As String = $"UPDATE {table} SET BakImg = @Img WHERE PatientID = @PatientID AND CellAddres = @CellAddres AND {idField} = @ID"

    '        Using connection As New SqlConnection(My.Settings.DentistXConnectionString)
    '            Using command As New SqlCommand(query, connection)
    '                command.Parameters.AddWithValue("@ID", ID)
    '                command.Parameters.AddWithValue("@PatientID", patientID)
    '                command.Parameters.AddWithValue("@CellAddres", cellAddress)
    '                command.Parameters.AddWithValue("@Img", img)

    '                connection.Open()
    '                command.ExecuteNonQuery()
    '            End Using
    '        End Using
    '    End Sub

    '    Public Shared Sub InsertStyle(table As String, style As String, patientID As Integer, cellAddress As Integer, img As Byte())
    '        Dim idField As String = GetIdField(style)
    '        Dim query As String = $"INSERT INTO {table} (PatientID, CellAddres, BakImg) VALUES (@PatientID, @CellAddres, @Img)"

    '        Using connection As New SqlConnection(My.Settings.DentistXConnectionString)
    '            Using command As New SqlCommand(query, connection)
    '                command.Parameters.AddWithValue("@PatientID", patientID)
    '                command.Parameters.AddWithValue("@CellAddres", cellAddress)
    '                command.Parameters.AddWithValue("@Img", img)

    '                connection.Open()
    '                command.ExecuteNonQuery()
    '            End Using
    '        End Using
    '    End Sub

    '    Private Shared Function GetIdField(style As String) As String
    '        Select Case style
    '            Case "LU"
    '                Return "LUcellID"
    '            Case "LD"
    '                Return "LDcellID"
    '            Case "RU"
    '                Return "RUcellID"
    '            Case "RD"
    '                Return "RDcellID"
    '            Case Else
    '                Throw New ArgumentException("Invalid style")
    '        End Select
    '    End Function

    '    Public Shared Function IsStyl(patientID As Integer, cellAddress As Integer, style As String) As Integer?
    '        Dim table As String
    '        Dim idField As String
    '        Select Case style
    '            Case "LU"
    '                table = "LUPL"
    '                idField = "LUcellID"
    '            Case "LD"
    '                table = "LDPL"
    '                idField = "LDcellID"
    '            Case "RU"
    '                table = "RUPL"
    '                idField = "RUcellID"
    '            Case "RD"
    '                table = "RDPL"
    '                idField = "RDcellID"
    '            Case Else
    '                Return Nothing
    '        End Select

    '        Dim query As String = $"SELECT {idField} FROM {table} WHERE PatientID = @PatientID AND CellAddres = @CellAddres"
    '        Using connection As New SqlConnection(My.Settings.DentistXConnectionString)
    '            Using command As New SqlCommand(query, connection)
    '                command.Parameters.AddWithValue("@PatientID", patientID)
    '                command.Parameters.AddWithValue("@CellAddres", cellAddress)

    '                connection.Open()
    '                Dim result As Object = command.ExecuteScalar()
    '                If result IsNot Nothing Then
    '                    Return Convert.ToInt32(result)
    '                End If
    '            End Using
    '        End Using

    '        Return Nothing
    '    End Function



    'End Class


#End Region

    Public Class Qrtrs
        Public Property QrtrID As Integer
        Public Property QrtrTable As String
        Public Property QrtrColumnName As String
        Public Property QrtrColumnValue As String
        Public Property QrtrAddress As Integer
    End Class

    Public Class ToothSelection
        Public Property SelectedTeeth As List(Of Byte)
        Public Property SelectedQuarters As List(Of Qrtrs)

        Public Sub New()
            SelectedTeeth = New List(Of Byte)()
            SelectedQuarters = New List(Of Qrtrs)()
        End Sub
    End Class


#Region "Unused Code"

    Public Function AddTreatFrmChartToGrids1(patientID As Integer, toothNum As Integer, toothName As String, treat As String) As Boolean
        QrtrTable = ""
        QrtrID = 0
        QrtrAddress = 0
        Return False
#If False Then
        Dim trt As String = treat '& vbCrLf & Format(Now.Date, "dd-MM-yyyy")
        If Not trt.StartsWith("IMPLANT") Then
            trt = treat & vbCrLf & Format(Now.Date, "dd-MM-yyyy")
        Else
            trt = treat
        End If
        Dim quadrantTable As String = GetQuadrantTable(toothNum)
        Dim toothColumn As String = GetToothColumn(toothNum)

        If String.IsNullOrEmpty(quadrantTable) OrElse String.IsNullOrEmpty(toothColumn) Then
            Throw New ArgumentException("Invalid ToothNum provided.")
        End If

        Dim frstID, scndID, thrdID, frthID As Integer?
        frstID = GetQrtrTblFirstID(patientID, toothNum)
        scndID = GetQrtrTblSecondID(patientID, toothNum)
        thrdID = GetQrtrTblThirdID(patientID, toothNum)
        frthID = GetQrtrTblFourthID(patientID, toothNum)

        Dim primaryKeyName As String = $"{quadrantTable}ID"
        Dim columnName As String = $"{quadrantTable}{toothColumn}"

        Dim connection As New SqlConnection
        connection = DentistXDATA.GetConnection()
        Using connection
            connection.Open()
            If (columnName = "RU8" OrElse columnName = "LU8") Then
                Dim query As String = $"SELECT  {primaryKeyName},  {columnName} FROM {quadrantTable} WHERE PatientID = @PatientID AND {primaryKeyName} > @PrimaryKeyValue"
                Using command As New SqlCommand(query, connection)
                    command.Parameters.AddWithValue("@PatientID", patientID)
                    command.Parameters.AddWithValue("@PrimaryKeyValue", scndID)
                    Using reader As SqlDataReader = command.ExecuteReader()
                        Dim emptyRows As New List(Of Integer)()
                        Dim oldestDateRowKey As Integer = -1
                        Dim oldestDate As Date? = Nothing
                        Dim hasEmptyCell As Boolean = False
                        Dim colIndex, rowIndex As Integer
                        Dim cellAddress As Integer
                        While reader.Read()
                            Dim currentCellValue As String = If(IsDBNull(reader(columnName)), String.Empty, reader(columnName).ToString())
                            Dim primaryKeyValue As Integer = Convert.ToInt32(reader(primaryKeyName))
                            If primaryKeyValue = frstID Then
                                rowIndex = 0
                                colIndex = GetColIndexByToothNum(toothNum)
                                cellAddress = (colIndex * 10) + rowIndex
                                ' Collect rows with empty cells
                                If String.IsNullOrWhiteSpace(currentCellValue) Then
                                    emptyRows.Add(primaryKeyValue)
                                Else
                                    ' Check for the oldest date if the cell is not empty
                                    Dim datePart As String = currentCellValue.Split(vbCrLf).LastOrDefault().Trim()
                                    If Date.TryParseExact(datePart, "dd-MM-yyyy", Nothing, Globalization.DateTimeStyles.None, Nothing) Then
                                        Dim cellDate As Date = Date.ParseExact(datePart, "dd-MM-yyyy", Nothing)

                                        ' Track the oldest date
                                        If Not oldestDate.HasValue OrElse cellDate < oldestDate.Value Then
                                            oldestDate = cellDate
                                            oldestDateRowKey = primaryKeyValue
                                        End If
                                    End If
                                End If
                                Exit While
                            ElseIf primaryKeyValue = scndID Then
                                rowIndex = 1
                                colIndex = GetColIndexByToothNum(toothNum)
                                cellAddress = (colIndex * 10) + rowIndex
                                ' Collect rows with empty cells
                                If String.IsNullOrWhiteSpace(currentCellValue) Then
                                    emptyRows.Add(primaryKeyValue)
                                Else
                                    ' Check for the oldest date if the cell is not empty
                                    Dim datePart As String = currentCellValue.Split(vbCrLf).LastOrDefault().Trim()
                                    If Date.TryParseExact(datePart, "dd-MM-yyyy", Nothing, Globalization.DateTimeStyles.None, Nothing) Then
                                        Dim cellDate As Date = Date.ParseExact(datePart, "dd-MM-yyyy", Nothing)

                                        ' Track the oldest date
                                        If Not oldestDate.HasValue OrElse cellDate < oldestDate.Value Then
                                            oldestDate = cellDate
                                            oldestDateRowKey = primaryKeyValue
                                        End If
                                    End If
                                End If
                                Exit While
                            ElseIf primaryKeyValue = thrdID Then
                                rowIndex = 2
                                colIndex = GetColIndexByToothNum(toothNum)
                                cellAddress = (colIndex * 10) + rowIndex
                                ' Collect rows with empty cells
                                If String.IsNullOrWhiteSpace(currentCellValue) Then
                                    emptyRows.Add(primaryKeyValue)
                                Else
                                    ' Check for the oldest date if the cell is not empty
                                    Dim datePart As String = currentCellValue.Split(vbCrLf).LastOrDefault().Trim()
                                    If Date.TryParseExact(datePart, "dd-MM-yyyy", Nothing, Globalization.DateTimeStyles.None, Nothing) Then
                                        Dim cellDate As Date = Date.ParseExact(datePart, "dd-MM-yyyy", Nothing)

                                        ' Track the oldest date
                                        If Not oldestDate.HasValue OrElse cellDate < oldestDate.Value Then
                                            oldestDate = cellDate
                                            oldestDateRowKey = primaryKeyValue
                                        End If
                                    End If
                                End If
                                Exit While
                            ElseIf primaryKeyValue = frthID Then
                                rowIndex = 3
                                colIndex = GetColIndexByToothNum(toothNum)
                                cellAddress = (colIndex * 10) + rowIndex
                                ' Collect rows with empty cells
                                If String.IsNullOrWhiteSpace(currentCellValue) Then
                                    emptyRows.Add(primaryKeyValue)
                                Else
                                    ' Check for the oldest date if the cell is not empty
                                    Dim datePart As String = currentCellValue.Split(vbCrLf).LastOrDefault().Trim()
                                    If Date.TryParseExact(datePart, "dd-MM-yyyy", Nothing, Globalization.DateTimeStyles.None, Nothing) Then
                                        Dim cellDate As Date = Date.ParseExact(datePart, "dd-MM-yyyy", Nothing)

                                        ' Track the oldest date
                                        If Not oldestDate.HasValue OrElse cellDate < oldestDate.Value Then
                                            oldestDate = cellDate
                                            oldestDateRowKey = primaryKeyValue
                                        End If
                                    End If
                                End If
                                Exit While
                            End If

                        End While

                        reader.Close()
                        ' If there are empty cells, pick the minimum primary key from them
                        If emptyRows.Count > 0 Then
                            Dim minEmptyKey = emptyRows.Min()
                            If minEmptyKey = frstID Then
                                rowIndex = 0
                            ElseIf minEmptyKey = scndID Then
                                rowIndex = 1
                            ElseIf minEmptyKey = thrdID Then
                                rowIndex = 2
                            ElseIf minEmptyKey = frthID Then
                                rowIndex = 3
                            End If
                            colIndex = GetColIndexByToothNum(toothNum)
                            cellAddress = (colIndex * 10) + rowIndex
                            ' Update the empty cell with the treatment value
                            Dim updateQuery As String = $"UPDATE {quadrantTable} SET {columnName} = @Treat WHERE PatientID = @PatientID AND {primaryKeyName} = @PrimaryKeyValue"
                            Using updateCommand As New SqlCommand(updateQuery, connection)
                                updateCommand.Parameters.AddWithValue("@Treat", trt)
                                updateCommand.Parameters.AddWithValue("@PatientID", patientID)
                                updateCommand.Parameters.AddWithValue("@PrimaryKeyValue", minEmptyKey)
                                If updateCommand.ExecuteNonQuery() > 0 Then
                                    If trt.StartsWith("IMPLANT") Then
                                        SetBackIMPfrmChartToGrid(cellAddress, patientID, quadrantTable, trt)
                                    End If
                                End If

                            End Using

                            Return True
                        ElseIf oldestDateRowKey <> -1 Then
                            ' If no empty cells, use the oldest date row
                            Dim updateQuery As String = $"UPDATE {quadrantTable} SET {columnName} = @Treat WHERE PatientID = @PatientID AND {primaryKeyName} = @PrimaryKeyValue"
                            Using updateCommand As New SqlCommand(updateQuery, connection)
                                updateCommand.Parameters.AddWithValue("@Treat", trt)
                                updateCommand.Parameters.AddWithValue("@PatientID", patientID)
                                updateCommand.Parameters.AddWithValue("@PrimaryKeyValue", oldestDateRowKey)
                                If updateCommand.ExecuteNonQuery() > 0 Then
                                    If trt.StartsWith("IMPLANT") Then
                                        SetBackIMPfrmChartToGrid(cellAddress, patientID, quadrantTable, trt)
                                    End If
                                End If
                            End Using
                            Return True
                        End If
                    End Using
                End Using

            ElseIf (columnName = "RU7" OrElse columnName = "LU7") Then
                Dim query As String = $"SELECT   {primaryKeyName},  {columnName} FROM {quadrantTable} WHERE PatientID = @PatientID AND {primaryKeyName} > @PrimaryKeyValue"
                Using command As New SqlCommand(query, connection)
                    command.Parameters.AddWithValue("@PatientID", patientID)
                    command.Parameters.AddWithValue("@PrimaryKeyValue", frstID)
                    Using reader As SqlDataReader = command.ExecuteReader()
                        Dim emptyRows As New List(Of Integer)()
                        Dim oldestDateRowKey As Integer = -1
                        Dim oldestDate As Date? = Nothing
                        Dim hasEmptyCell As Boolean = False
                        Dim colIndex, rowIndex As Integer
                        Dim cellAddress As Integer
                        While reader.Read()
                            Dim currentCellValue As String = If(IsDBNull(reader(columnName)), String.Empty, reader(columnName).ToString())
                            Dim primaryKeyValue As Integer = Convert.ToInt32(reader(primaryKeyName))
                            If primaryKeyValue = frstID Then
                                rowIndex = 0
                                colIndex = GetColIndexByToothNum(toothNum)
                                cellAddress = (colIndex * 10) + rowIndex
                                ' Collect rows with empty cells
                                If String.IsNullOrWhiteSpace(currentCellValue) Then
                                    emptyRows.Add(primaryKeyValue)
                                Else
                                    ' Check for the oldest date if the cell is not empty
                                    Dim datePart As String = currentCellValue.Split(vbCrLf).LastOrDefault().Trim()
                                    If Date.TryParseExact(datePart, "dd-MM-yyyy", Nothing, Globalization.DateTimeStyles.None, Nothing) Then
                                        Dim cellDate As Date = Date.ParseExact(datePart, "dd-MM-yyyy", Nothing)

                                        ' Track the oldest date
                                        If Not oldestDate.HasValue OrElse cellDate < oldestDate.Value Then
                                            oldestDate = cellDate
                                            oldestDateRowKey = primaryKeyValue
                                        End If
                                    End If
                                End If
                                Exit While
                            ElseIf primaryKeyValue = scndID Then
                                rowIndex = 1
                                colIndex = GetColIndexByToothNum(toothNum)
                                cellAddress = (colIndex * 10) + rowIndex
                                ' Collect rows with empty cells
                                If String.IsNullOrWhiteSpace(currentCellValue) Then
                                    emptyRows.Add(primaryKeyValue)
                                Else
                                    ' Check for the oldest date if the cell is not empty
                                    Dim datePart As String = currentCellValue.Split(vbCrLf).LastOrDefault().Trim()
                                    If Date.TryParseExact(datePart, "dd-MM-yyyy", Nothing, Globalization.DateTimeStyles.None, Nothing) Then
                                        Dim cellDate As Date = Date.ParseExact(datePart, "dd-MM-yyyy", Nothing)

                                        ' Track the oldest date
                                        If Not oldestDate.HasValue OrElse cellDate < oldestDate.Value Then
                                            oldestDate = cellDate
                                            oldestDateRowKey = primaryKeyValue
                                        End If
                                    End If
                                End If
                                Exit While
                            ElseIf primaryKeyValue = thrdID Then
                                rowIndex = 2
                                colIndex = GetColIndexByToothNum(toothNum)
                                cellAddress = (colIndex * 10) + rowIndex
                                ' Collect rows with empty cells
                                If String.IsNullOrWhiteSpace(currentCellValue) Then
                                    emptyRows.Add(primaryKeyValue)
                                Else
                                    ' Check for the oldest date if the cell is not empty
                                    Dim datePart As String = currentCellValue.Split(vbCrLf).LastOrDefault().Trim()
                                    If Date.TryParseExact(datePart, "dd-MM-yyyy", Nothing, Globalization.DateTimeStyles.None, Nothing) Then
                                        Dim cellDate As Date = Date.ParseExact(datePart, "dd-MM-yyyy", Nothing)

                                        ' Track the oldest date
                                        If Not oldestDate.HasValue OrElse cellDate < oldestDate.Value Then
                                            oldestDate = cellDate
                                            oldestDateRowKey = primaryKeyValue
                                        End If
                                    End If
                                End If
                                Exit While
                            ElseIf primaryKeyValue = frthID Then
                                rowIndex = 3
                                colIndex = GetColIndexByToothNum(toothNum)
                                cellAddress = (colIndex * 10) + rowIndex
                                ' Collect rows with empty cells
                                If String.IsNullOrWhiteSpace(currentCellValue) Then
                                    emptyRows.Add(primaryKeyValue)
                                Else
                                    ' Check for the oldest date if the cell is not empty
                                    Dim datePart As String = currentCellValue.Split(vbCrLf).LastOrDefault().Trim()
                                    If Date.TryParseExact(datePart, "dd-MM-yyyy", Nothing, Globalization.DateTimeStyles.None, Nothing) Then
                                        Dim cellDate As Date = Date.ParseExact(datePart, "dd-MM-yyyy", Nothing)

                                        ' Track the oldest date
                                        If Not oldestDate.HasValue OrElse cellDate < oldestDate.Value Then
                                            oldestDate = cellDate
                                            oldestDateRowKey = primaryKeyValue
                                        End If
                                    End If
                                End If
                                Exit While
                            End If

                        End While

                        reader.Close()
                        ' If there are empty cells, pick the minimum primary key from them
                        If emptyRows.Count > 0 Then
                            Dim minEmptyKey = emptyRows.Min()
                            ' Update the empty cell with the treatment value
                            Dim updateQuery As String = $"UPDATE {quadrantTable} SET {columnName} = @Treat WHERE PatientID = @PatientID AND {primaryKeyName} = @PrimaryKeyValue"
                            Using updateCommand As New SqlCommand(updateQuery, connection)
                                updateCommand.Parameters.AddWithValue("@Treat", trt)
                                updateCommand.Parameters.AddWithValue("@PatientID", patientID)
                                updateCommand.Parameters.AddWithValue("@PrimaryKeyValue", minEmptyKey)
                                If updateCommand.ExecuteNonQuery() > 0 Then
                                    If trt.StartsWith("IMPLANT") Then
                                        SetBackIMPfrmChartToGrid(cellAddress, patientID, quadrantTable, trt)
                                    End If
                                End If
                            End Using
                            Return True
                        ElseIf oldestDateRowKey <> -1 Then
                            ' If no empty cells, use the oldest date row
                            Dim updateQuery As String = $"UPDATE {quadrantTable} SET {columnName} = @Treat WHERE PatientID = @PatientID AND {primaryKeyName} = @PrimaryKeyValue"
                            Using updateCommand As New SqlCommand(updateQuery, connection)
                                updateCommand.Parameters.AddWithValue("@Treat", trt)
                                updateCommand.Parameters.AddWithValue("@PatientID", patientID)
                                updateCommand.Parameters.AddWithValue("@PrimaryKeyValue", oldestDateRowKey)
                                If updateCommand.ExecuteNonQuery() > 0 Then
                                    SetBackIMPfrmChartToGrid(cellAddress, patientID, quadrantTable, trt)
                                End If
                            End Using
                            Return True
                        End If
                    End Using
                End Using

            ElseIf (columnName = "RD8" OrElse columnName = "LD8") Then
                Dim query As String = $"SELECT   {primaryKeyName},  {columnName} FROM {quadrantTable} WHERE PatientID = @PatientID AND {primaryKeyName} < @PrimaryKeyValue"
                Using command As New SqlCommand(query, connection)
                    command.Parameters.AddWithValue("@PatientID", patientID)
                    command.Parameters.AddWithValue("@PrimaryKeyValue", thrdID)
                    Using reader As SqlDataReader = command.ExecuteReader()
                        Dim emptyRows As New List(Of Integer)()
                        Dim oldestDateRowKey As Integer = -1
                        Dim oldestDate As Date? = Nothing
                        Dim hasEmptyCell As Boolean = False
                        Dim colIndex, rowIndex As Integer
                        Dim cellAddress As Integer
                        While reader.Read()
                            Dim currentCellValue As String = If(IsDBNull(reader(columnName)), String.Empty, reader(columnName).ToString())
                            Dim primaryKeyValue As Integer = Convert.ToInt32(reader(primaryKeyName))
                            If primaryKeyValue = frstID Then
                                rowIndex = 0
                                colIndex = GetColIndexByToothNum(toothNum)
                                cellAddress = (colIndex * 10) + rowIndex
                                ' Collect rows with empty cells
                                If String.IsNullOrWhiteSpace(currentCellValue) Then
                                    emptyRows.Add(primaryKeyValue)
                                Else
                                    ' Check for the oldest date if the cell is not empty
                                    Dim datePart As String = currentCellValue.Split(vbCrLf).LastOrDefault().Trim()
                                    If Date.TryParseExact(datePart, "dd-MM-yyyy", Nothing, Globalization.DateTimeStyles.None, Nothing) Then
                                        Dim cellDate As Date = Date.ParseExact(datePart, "dd-MM-yyyy", Nothing)

                                        ' Track the oldest date
                                        If Not oldestDate.HasValue OrElse cellDate < oldestDate.Value Then
                                            oldestDate = cellDate
                                            oldestDateRowKey = primaryKeyValue
                                        End If
                                    End If
                                End If
                                Exit While
                            ElseIf primaryKeyValue = scndID Then
                                rowIndex = 1
                                colIndex = GetColIndexByToothNum(toothNum)
                                cellAddress = (colIndex * 10) + rowIndex
                                ' Collect rows with empty cells
                                If String.IsNullOrWhiteSpace(currentCellValue) Then
                                    emptyRows.Add(primaryKeyValue)
                                Else
                                    ' Check for the oldest date if the cell is not empty
                                    Dim datePart As String = currentCellValue.Split(vbCrLf).LastOrDefault().Trim()
                                    If Date.TryParseExact(datePart, "dd-MM-yyyy", Nothing, Globalization.DateTimeStyles.None, Nothing) Then
                                        Dim cellDate As Date = Date.ParseExact(datePart, "dd-MM-yyyy", Nothing)

                                        ' Track the oldest date
                                        If Not oldestDate.HasValue OrElse cellDate < oldestDate.Value Then
                                            oldestDate = cellDate
                                            oldestDateRowKey = primaryKeyValue
                                        End If
                                    End If
                                End If
                                Exit While
                            ElseIf primaryKeyValue = thrdID Then
                                rowIndex = 2
                                colIndex = GetColIndexByToothNum(toothNum)
                                cellAddress = (colIndex * 10) + rowIndex
                                ' Collect rows with empty cells
                                If String.IsNullOrWhiteSpace(currentCellValue) Then
                                    emptyRows.Add(primaryKeyValue)
                                Else
                                    ' Check for the oldest date if the cell is not empty
                                    Dim datePart As String = currentCellValue.Split(vbCrLf).LastOrDefault().Trim()
                                    If Date.TryParseExact(datePart, "dd-MM-yyyy", Nothing, Globalization.DateTimeStyles.None, Nothing) Then
                                        Dim cellDate As Date = Date.ParseExact(datePart, "dd-MM-yyyy", Nothing)

                                        ' Track the oldest date
                                        If Not oldestDate.HasValue OrElse cellDate < oldestDate.Value Then
                                            oldestDate = cellDate
                                            oldestDateRowKey = primaryKeyValue
                                        End If
                                    End If
                                End If
                                Exit While
                            ElseIf primaryKeyValue = frthID Then
                                rowIndex = 3
                                colIndex = GetColIndexByToothNum(toothNum)
                                cellAddress = (colIndex * 10) + rowIndex
                                ' Collect rows with empty cells
                                If String.IsNullOrWhiteSpace(currentCellValue) Then
                                    emptyRows.Add(primaryKeyValue)
                                Else
                                    ' Check for the oldest date if the cell is not empty
                                    Dim datePart As String = currentCellValue.Split(vbCrLf).LastOrDefault().Trim()
                                    If Date.TryParseExact(datePart, "dd-MM-yyyy", Nothing, Globalization.DateTimeStyles.None, Nothing) Then
                                        Dim cellDate As Date = Date.ParseExact(datePart, "dd-MM-yyyy", Nothing)

                                        ' Track the oldest date
                                        If Not oldestDate.HasValue OrElse cellDate < oldestDate.Value Then
                                            oldestDate = cellDate
                                            oldestDateRowKey = primaryKeyValue
                                        End If
                                    End If
                                End If
                                Exit While
                            End If

                        End While

                        reader.Close()
                        ' If there are empty cells, pick the minimum primary key from them
                        If emptyRows.Count > 0 Then
                            Dim minEmptyKey = emptyRows.Min()
                            ' Update the empty cell with the treatment value
                            Dim updateQuery As String = $"UPDATE {quadrantTable} SET {columnName} = @Treat WHERE PatientID = @PatientID AND {primaryKeyName} = @PrimaryKeyValue"
                            Using updateCommand As New SqlCommand(updateQuery, connection)
                                updateCommand.Parameters.AddWithValue("@Treat", trt)
                                updateCommand.Parameters.AddWithValue("@PatientID", patientID)
                                updateCommand.Parameters.AddWithValue("@PrimaryKeyValue", minEmptyKey)
                                If updateCommand.ExecuteNonQuery() > 0 Then
                                    If trt.StartsWith("IMPLANT") Then
                                        SetBackIMPfrmChartToGrid(cellAddress, patientID, quadrantTable, trt)
                                    End If
                                End If
                            End Using
                            Return True
                        ElseIf oldestDateRowKey <> -1 Then
                            ' If no empty cells, use the oldest date row
                            Dim updateQuery As String = $"UPDATE {quadrantTable} SET {columnName} = @Treat WHERE PatientID = @PatientID AND {primaryKeyName} = @PrimaryKeyValue"
                            Using updateCommand As New SqlCommand(updateQuery, connection)
                                updateCommand.Parameters.AddWithValue("@Treat", trt)
                                updateCommand.Parameters.AddWithValue("@PatientID", patientID)
                                updateCommand.Parameters.AddWithValue("@PrimaryKeyValue", oldestDateRowKey)
                                If updateCommand.ExecuteNonQuery() > 0 Then
                                    SetBackIMPfrmChartToGrid(cellAddress, patientID, quadrantTable, trt)
                                End If
                            End Using
                            Return True
                        End If
                    End Using
                End Using

            ElseIf (columnName = "RD7" OrElse columnName = "LD7") Then
                Dim query As String = $"SELECT   {primaryKeyName},  {columnName} FROM {quadrantTable} WHERE PatientID = @PatientID AND {primaryKeyName} < @PrimaryKeyValue"
                Using command As New SqlCommand(query, connection)
                    command.Parameters.AddWithValue("@PatientID", patientID)
                    command.Parameters.AddWithValue("@PrimaryKeyValue", frthID)
                    Using reader As SqlDataReader = command.ExecuteReader()
                        Dim emptyRows As New List(Of Integer)()
                        Dim oldestDateRowKey As Integer = -1
                        Dim oldestDate As Date? = Nothing
                        Dim hasEmptyCell As Boolean = False
                        Dim colIndex, rowIndex As Integer
                        Dim cellAddress As Integer
                        While reader.Read()
                            Dim currentCellValue As String = If(IsDBNull(reader(columnName)), String.Empty, reader(columnName).ToString())
                            Dim primaryKeyValue As Integer = Convert.ToInt32(reader(primaryKeyName))
                            If primaryKeyValue = frstID Then
                                rowIndex = 0
                                colIndex = GetColIndexByToothNum(toothNum)
                                cellAddress = (colIndex * 10) + rowIndex
                                ' Collect rows with empty cells
                                If String.IsNullOrWhiteSpace(currentCellValue) Then
                                    emptyRows.Add(primaryKeyValue)
                                Else
                                    ' Check for the oldest date if the cell is not empty
                                    Dim datePart As String = currentCellValue.Split(vbCrLf).LastOrDefault().Trim()
                                    If Date.TryParseExact(datePart, "dd-MM-yyyy", Nothing, Globalization.DateTimeStyles.None, Nothing) Then
                                        Dim cellDate As Date = Date.ParseExact(datePart, "dd-MM-yyyy", Nothing)

                                        ' Track the oldest date
                                        If Not oldestDate.HasValue OrElse cellDate < oldestDate.Value Then
                                            oldestDate = cellDate
                                            oldestDateRowKey = primaryKeyValue
                                        End If
                                    End If
                                End If
                                Exit While
                            ElseIf primaryKeyValue = scndID Then
                                rowIndex = 1
                                colIndex = GetColIndexByToothNum(toothNum)
                                cellAddress = (colIndex * 10) + rowIndex
                                ' Collect rows with empty cells
                                If String.IsNullOrWhiteSpace(currentCellValue) Then
                                    emptyRows.Add(primaryKeyValue)
                                Else
                                    ' Check for the oldest date if the cell is not empty
                                    Dim datePart As String = currentCellValue.Split(vbCrLf).LastOrDefault().Trim()
                                    If Date.TryParseExact(datePart, "dd-MM-yyyy", Nothing, Globalization.DateTimeStyles.None, Nothing) Then
                                        Dim cellDate As Date = Date.ParseExact(datePart, "dd-MM-yyyy", Nothing)

                                        ' Track the oldest date
                                        If Not oldestDate.HasValue OrElse cellDate < oldestDate.Value Then
                                            oldestDate = cellDate
                                            oldestDateRowKey = primaryKeyValue
                                        End If
                                    End If
                                End If
                                Exit While
                            ElseIf primaryKeyValue = thrdID Then
                                rowIndex = 2
                                colIndex = GetColIndexByToothNum(toothNum)
                                cellAddress = (colIndex * 10) + rowIndex
                                ' Collect rows with empty cells
                                If String.IsNullOrWhiteSpace(currentCellValue) Then
                                    emptyRows.Add(primaryKeyValue)
                                Else
                                    ' Check for the oldest date if the cell is not empty
                                    Dim datePart As String = currentCellValue.Split(vbCrLf).LastOrDefault().Trim()
                                    If Date.TryParseExact(datePart, "dd-MM-yyyy", Nothing, Globalization.DateTimeStyles.None, Nothing) Then
                                        Dim cellDate As Date = Date.ParseExact(datePart, "dd-MM-yyyy", Nothing)

                                        ' Track the oldest date
                                        If Not oldestDate.HasValue OrElse cellDate < oldestDate.Value Then
                                            oldestDate = cellDate
                                            oldestDateRowKey = primaryKeyValue
                                        End If
                                    End If
                                End If
                                Exit While
                            ElseIf primaryKeyValue = frthID Then
                                rowIndex = 3
                                colIndex = GetColIndexByToothNum(toothNum)
                                cellAddress = (colIndex * 10) + rowIndex
                                ' Collect rows with empty cells
                                If String.IsNullOrWhiteSpace(currentCellValue) Then
                                    emptyRows.Add(primaryKeyValue)
                                Else
                                    ' Check for the oldest date if the cell is not empty
                                    Dim datePart As String = currentCellValue.Split(vbCrLf).LastOrDefault().Trim()
                                    If Date.TryParseExact(datePart, "dd-MM-yyyy", Nothing, Globalization.DateTimeStyles.None, Nothing) Then
                                        Dim cellDate As Date = Date.ParseExact(datePart, "dd-MM-yyyy", Nothing)

                                        ' Track the oldest date
                                        If Not oldestDate.HasValue OrElse cellDate < oldestDate.Value Then
                                            oldestDate = cellDate
                                            oldestDateRowKey = primaryKeyValue
                                        End If
                                    End If
                                End If
                                Exit While
                            End If

                        End While

                        reader.Close()
                        ' If there are empty cells, pick the minimum primary key from them
                        If emptyRows.Count > 0 Then
                            Dim minEmptyKey = emptyRows.Min()
                            ' Update the empty cell with the treatment value
                            Dim updateQuery As String = $"UPDATE {quadrantTable} SET {columnName} = @Treat WHERE PatientID = @PatientID AND {primaryKeyName} = @PrimaryKeyValue"
                            Using updateCommand As New SqlCommand(updateQuery, connection)
                                updateCommand.Parameters.AddWithValue("@Treat", trt)
                                updateCommand.Parameters.AddWithValue("@PatientID", patientID)
                                updateCommand.Parameters.AddWithValue("@PrimaryKeyValue", minEmptyKey)
                                If updateCommand.ExecuteNonQuery() > 0 Then
                                    If trt.StartsWith("IMPLANT") Then
                                        SetBackIMPfrmChartToGrid(cellAddress, patientID, quadrantTable, trt)
                                    End If
                                End If
                            End Using
                            Return True
                        ElseIf oldestDateRowKey <> -1 Then
                            ' If no empty cells, use the oldest date row
                            Dim updateQuery As String = $"UPDATE {quadrantTable} SET {columnName} = @Treat WHERE PatientID = @PatientID AND {primaryKeyName} = @PrimaryKeyValue"
                            Using updateCommand As New SqlCommand(updateQuery, connection)
                                updateCommand.Parameters.AddWithValue("@Treat", trt)
                                updateCommand.Parameters.AddWithValue("@PatientID", patientID)
                                updateCommand.Parameters.AddWithValue("@PrimaryKeyValue", oldestDateRowKey)
                                If updateCommand.ExecuteNonQuery() > 0 Then
                                    SetBackIMPfrmChartToGrid(cellAddress, patientID, quadrantTable, trt)
                                End If
                            End Using
                            Return True
                        End If
                    End Using
                End Using

            Else

                ' Check for empty cells first
                Dim query As String = $"SELECT {primaryKeyName}, {columnName} FROM {quadrantTable} WHERE PatientID = @PatientID ORDER BY {primaryKeyName} ASC"
                Using command As New SqlCommand(query, connection)
                    command.Parameters.AddWithValue("@PatientID", patientID)
                    Using reader As SqlDataReader = command.ExecuteReader()
                        Dim primaryKeys As New List(Of Integer)()
                        Dim emptyRows As New List(Of Integer)()
                        Dim oldestDateRowKey As Integer = -1
                        Dim oldestDate As Date? = Nothing
                        Dim hasEmptyCell As Boolean = False

                        While reader.Read()
                            Dim currentCellValue As String = If(IsDBNull(reader(columnName)), String.Empty, reader(columnName).ToString())
                            Dim primaryKeyValue As Integer = Convert.ToInt32(reader(primaryKeyName))

                            primaryKeys.Add(primaryKeyValue)

                            ' Collect rows with empty cells
                            If String.IsNullOrWhiteSpace(currentCellValue) Then
                                emptyRows.Add(primaryKeyValue)
                            Else
                                ' Check for the oldest date if the cell is not empty
                                Dim datePart As String = currentCellValue.Split(vbCrLf).LastOrDefault().Trim()
                                If Date.TryParseExact(datePart, "dd-MM-yyyy", Nothing, Globalization.DateTimeStyles.None, Nothing) Then
                                    Dim cellDate As Date = Date.ParseExact(datePart, "dd-MM-yyyy", Nothing)

                                    ' Track the oldest date
                                    If Not oldestDate.HasValue OrElse cellDate < oldestDate.Value Then
                                        oldestDate = cellDate
                                        oldestDateRowKey = primaryKeyValue
                                    End If
                                End If
                            End If
                        End While

                        reader.Close()

                        '' Neglect the two smallest primary keys
                        'primaryKeys.Sort()
                        'If primaryKeys.Count > 2 Then
                        '    primaryKeys.RemoveRange(0, 2) ' Remove the smallest two
                        'End If
                        Dim colIndex, rowIndex As Integer
                        Dim cellAddress As Integer

                        ' If there are empty cells, pick the minimum primary key from them
                        If emptyRows.Count > 0 Then
                            Dim minEmptyKey = emptyRows.Min()
                            If minEmptyKey = frstID Then
                                rowIndex = 0
                            ElseIf minEmptyKey = scndID Then
                                rowIndex = 1
                            ElseIf minEmptyKey = thrdID Then
                                rowIndex = 2
                            ElseIf minEmptyKey = frthID Then
                                rowIndex = 3
                            End If
                            colIndex = GetColIndexByToothNum(toothNum)
                            cellAddress = (colIndex * 10) + rowIndex
                            ' Update the empty cell with the treatment value
                            Dim updateQuery As String = $"UPDATE {quadrantTable} SET {columnName} = @Treat WHERE PatientID = @PatientID AND {primaryKeyName} = @PrimaryKeyValue"
                            Using updateCommand As New SqlCommand(updateQuery, connection)
                                updateCommand.Parameters.AddWithValue("@Treat", trt)
                                updateCommand.Parameters.AddWithValue("@PatientID", patientID)
                                updateCommand.Parameters.AddWithValue("@PrimaryKeyValue", minEmptyKey)
                                If updateCommand.ExecuteNonQuery() > 0 Then
                                    If trt.StartsWith("IMPLANT") Then
                                        SetBackIMPfrmChartToGrid(cellAddress, patientID, quadrantTable, trt)
                                    End If
                                End If
                            End Using
                            Return True
                        ElseIf oldestDateRowKey <> -1 Then
                            If oldestDateRowKey = frstID Then
                                rowIndex = 0
                            ElseIf oldestDateRowKey = scndID Then
                                rowIndex = 1
                            ElseIf oldestDateRowKey = thrdID Then
                                rowIndex = 2
                            ElseIf oldestDateRowKey = frthID Then
                                rowIndex = 3
                            End If
                            colIndex = GetColIndexByToothNum(toothNum)
                            cellAddress = (colIndex * 10) + rowIndex
                            ' If no empty cells, use the oldest date row
                            Dim updateQuery As String = $"UPDATE {quadrantTable} SET {columnName} = @Treat WHERE PatientID = @PatientID AND {primaryKeyName} = @PrimaryKeyValue"
                            Using updateCommand As New SqlCommand(updateQuery, connection)
                                updateCommand.Parameters.AddWithValue("@Treat", trt)
                                updateCommand.Parameters.AddWithValue("@PatientID", patientID)
                                updateCommand.Parameters.AddWithValue("@PrimaryKeyValue", oldestDateRowKey)
                                If updateCommand.ExecuteNonQuery() > 0 Then
                                    If trt.StartsWith("IMPLANT") Then
                                        SetBackIMPfrmChartToGrid(cellAddress, patientID, quadrantTable, trt)
                                    End If
                                End If
                            End Using
                            Return True
                        End If
                    End Using
                End Using
            End If
        End Using

        Return False ' Return false if no update was performed
#End If
    End Function

    Public Function AddTreatToQuadrantTables1(patientID As Integer, toothNum As Integer, toothName As String, treat As String) As Boolean
        Return False
#If False Then
        Dim trt As String = treat & vbCrLf & Format(Now.Date, "dd-MM-yyyy")
        Dim quadrantTable As String = GetQuadrantTable(toothNum)
        Dim toothColumn As String = GetToothColumn(toothNum)

        If String.IsNullOrEmpty(quadrantTable) OrElse String.IsNullOrEmpty(toothColumn) Then
            Throw New ArgumentException("Invalid ToothNum provided.")
        End If

        Dim primaryKeyName As String = $"{quadrantTable}ID"
        Dim columnName As String = $"{quadrantTable}{toothColumn}"

        Dim connection As New SqlConnection
        connection = DentistXDATA.GetConnection()
        Using connection
            connection.Open()

            ' Check for empty cells first
            Dim query As String = $"SELECT {primaryKeyName}, {columnName} FROM {quadrantTable} WHERE PatientID = @PatientID ORDER BY {primaryKeyName} ASC"
            Using command As New SqlCommand(query, connection)
                command.Parameters.AddWithValue("@PatientID", patientID)
                Using reader As SqlDataReader = command.ExecuteReader()
                    Dim oldestDateRowKey As Integer = -1
                    Dim oldestDate As Date? = Nothing
                    Dim hasEmptyCell As Boolean = False
                    Dim lowestIndexWithOldestDate As Integer = Integer.MaxValue ' Track the row index with the oldest date

                    While reader.Read()
                        Dim x As Integer = reader.Depth
                        'If (columnName = "RU7" OrElse columnName = "LU7") AndAlso reader.Depth = 0 Then Continue While
                        'If (columnName = "RU8" OrElse columnName = "LU8") AndAlso (reader.Depth = 0 OrElse reader.Depth = 1) Then Continue While
                        'Debug.Print($"Processing column: {columnName}, Depth: {reader.Depth}")
                        'If (columnName = "RU7" OrElse columnName = "LU7") AndAlso reader.Depth = 0 Then
                        '    Debug.Print("Skipping row for RU7 or LU7, Depth is 0")
                        '    Continue While
                        'End If
                        'If (columnName = "RU8" OrElse columnName = "LU8") AndAlso (reader.Depth = 0 OrElse reader.Depth = 1) Then
                        '    Debug.Print("Skipping row for RU8 or LU8, Depth is 0 or 1")
                        '    Continue While
                        'End If
                        Dim currentCellValue As String = If(IsDBNull(reader(columnName)), String.Empty, reader(columnName).ToString())

                        ' Check if we need to skip based on Depth and specific columns (RU8, LU8)
                        If (columnName = "RU8" OrElse columnName = "LU8") AndAlso (reader.Depth = 0 OrElse reader.Depth = 1) Then
                            ' Skip rows for RU8 or LU8 if Depth is 0 or 1
                            ' But only skip if the column is not empty
                            If Not String.IsNullOrWhiteSpace(currentCellValue) Then
                                Continue While
                            End If
                        End If

                        '********************************************
                        If (columnName = "RD7" OrElse columnName = "LD7") AndAlso reader.Depth = 3 Then Continue While
                        If (columnName = "RD8" OrElse columnName = "LD8") AndAlso (reader.Depth = 2 OrElse reader.Depth = 3) Then Continue While

                        'Dim currentCellValue As String = If(IsDBNull(reader(columnName)), String.Empty, reader(columnName).ToString())

                        ' Check if the cell is empty
                        If String.IsNullOrWhiteSpace(currentCellValue) Then
                            Dim primaryKeyValue As Integer = Convert.ToInt32(reader(primaryKeyName))
                            Dim rowIndex As Integer = reader.Depth ' reader.GetInt32(reader.GetOrdinal(primaryKeyName))
                            reader.Close()

                            ' Update the empty cell
                            Dim updateQuery As String = $"UPDATE {quadrantTable} SET {columnName} = @Treat WHERE PatientID = @PatientID AND {primaryKeyName} = @PrimaryKeyValue"
                            Using updateCommand As New SqlCommand(updateQuery, connection)
                                updateCommand.Parameters.AddWithValue("@Treat", trt)
                                updateCommand.Parameters.AddWithValue("@PatientID", patientID)
                                updateCommand.Parameters.AddWithValue("@PrimaryKeyValue", primaryKeyValue)
                                updateCommand.ExecuteNonQuery()
                            End Using
                            Dim colIndex As Integer = GetToothColumnIndex(toothNum)

                            Dim cellAddress As Integer = (colIndex * 10) + (rowIndex + 1)
                            'Dim cellAddress As String = $"{quadrantTable}[{columnName}, {primaryKeyValue}]"
                            'Debug.Print("Updated Cell Address: " & cellAddress)
                            'MessageBox.Show("Updated Cell Address: " & cellAddress, "Cell Location", MessageBoxButtons.OK, MessageBoxIcon.Information)

                            hasEmptyCell = True
                            Return True ' Return true because the update was successful
                        Else
                            ' Check for the oldest date in the cell
                            Dim datePart As String = currentCellValue.Split(vbCrLf).LastOrDefault().Trim()
                            If Date.TryParseExact(datePart, "dd-MM-yyyy", Nothing, Globalization.DateTimeStyles.None, Nothing) Then
                                Dim cellDate As Date = Date.ParseExact(datePart, "dd-MM-yyyy", Nothing)
                                Dim rowIndex As Integer = reader.GetInt32(reader.GetOrdinal(primaryKeyName))

                                ' Track the oldest date and lowest index
                                If Not oldestDate.HasValue OrElse cellDate < oldestDate.Value Then
                                    oldestDate = cellDate
                                    oldestDateRowKey = rowIndex
                                    lowestIndexWithOldestDate = rowIndex
                                ElseIf cellDate = oldestDate.Value AndAlso rowIndex < lowestIndexWithOldestDate Then
                                    ' If dates are the same, prioritize the lowest index
                                    oldestDateRowKey = rowIndex
                                    lowestIndexWithOldestDate = rowIndex
                                End If
                            End If
                        End If
                    End While

                    reader.Close()

                    ' If no empty cell was found, update the oldest date at the lowest index
                    If Not hasEmptyCell AndAlso oldestDateRowKey <> -1 Then
                        Dim updateQuery As String = $"UPDATE {quadrantTable} SET {columnName} = @Treat WHERE PatientID = @PatientID AND {primaryKeyName} = @PrimaryKeyValue"
                        Using updateCommand As New SqlCommand(updateQuery, connection)
                            updateCommand.Parameters.AddWithValue("@Treat", trt)
                            updateCommand.Parameters.AddWithValue("@PatientID", patientID)
                            updateCommand.Parameters.AddWithValue("@PrimaryKeyValue", oldestDateRowKey)
                            updateCommand.ExecuteNonQuery()
                        End Using
                        'Dim colIndex As Integer = GetToothColumnIndex(toothNum)
                        'Dim cellAddress As String = $"{quadrantTable}[{columnName}, {oldestDateRowKey}]"
                        'Debug.Print("Updated Cell Address: " & cellAddress)
                        'MessageBox.Show("Updated Cell Address: " & cellAddress, "Cell Location", MessageBoxButtons.OK, MessageBoxIcon.Information)

                        Return True ' Return true because the update was successful
                    ElseIf Not hasEmptyCell Then
                        ' If no update was possible, notify the user
                        MessageBox.Show($"Unable to update {toothName}. Please check the data.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    End If
                End Using
            End Using
        End Using

        Return False ' Return false if no update was performed
#End If
    End Function

    'Add or Update Version
    Public Sub AddTreatToQuadrantTables8(patientID As Integer, toothNum As Integer, toothName As String, treat As String)
        Exit Sub
#If False Then
        Dim trt As String = treat & vbCrLf & Format(Now.Date, "dd-MM-yyyy")
        Dim quadrantTable As String = GetQuadrantTable(toothNum)
        Dim toothColumn As String = GetToothColumn(toothNum)

        If String.IsNullOrEmpty(quadrantTable) OrElse String.IsNullOrEmpty(toothColumn) Then
            Throw New ArgumentException("Invalid ToothNum provided.")
        End If

        Dim primaryKeyName As String = $"{quadrantTable}ID"
        Dim columnName As String = $"{quadrantTable}{toothColumn}"

        Dim connection As New SqlConnection
        connection = DentistXDATA.GetConnection()
        Using connection
            connection.Open()

            ' Check for empty cells first
            Dim query As String = $"SELECT {primaryKeyName}, {columnName} FROM {quadrantTable} WHERE PatientID = @PatientID"
            Using command As New SqlCommand(query, connection)
                command.Parameters.AddWithValue("@PatientID", patientID)
                Using reader As SqlDataReader = command.ExecuteReader()
                    Dim oldestDateRowKey As Integer = -1
                    Dim oldestDate As Date? = Nothing
                    Dim hasEmptyCell As Boolean = False

                    While reader.Read()
                        If (columnName = "RU7" OrElse columnName = "LU7") AndAlso reader.Depth = 0 Then Continue While
                        If (columnName = "RU8" OrElse columnName = "LU8") AndAlso (reader.Depth = 0 OrElse reader.Depth = 1) Then Continue While
                        If (columnName = "RD7" OrElse columnName = "LD7") AndAlso reader.Depth = 3 Then Continue While
                        If (columnName = "RD8" OrElse columnName = "LD8") AndAlso (reader.Depth = 2 OrElse reader.Depth = 3) Then Continue While

                        Dim currentCellValue As String = If(IsDBNull(reader(columnName)), String.Empty, reader(columnName).ToString())

                        ' Check if the cell is empty
                        If String.IsNullOrWhiteSpace(currentCellValue) Then
                            Dim primaryKeyValue As Integer = Convert.ToInt32(reader(primaryKeyName))
                            reader.Close()

                            ' Update the empty cell
                            Dim updateQuery As String = $"UPDATE {quadrantTable} SET {columnName} = @Treat WHERE PatientID = @PatientID AND {primaryKeyName} = @PrimaryKeyValue"
                            Using updateCommand As New SqlCommand(updateQuery, connection)
                                updateCommand.Parameters.AddWithValue("@Treat", trt)
                                updateCommand.Parameters.AddWithValue("@PatientID", patientID)
                                updateCommand.Parameters.AddWithValue("@PrimaryKeyValue", primaryKeyValue)
                                updateCommand.ExecuteNonQuery()
                            End Using

                            hasEmptyCell = True
                            Exit While
                        Else
                            ' Check for the oldest date in the cell
                            Dim datePart As String = currentCellValue.Split(vbCrLf).LastOrDefault()
                            If Date.TryParseExact(datePart, "dd-MM-yyyy", Nothing, Globalization.DateTimeStyles.None, Nothing) Then
                                Dim cellDate As Date = Date.ParseExact(datePart, "dd-MM-yyyy", Nothing)
                                If Not oldestDate.HasValue OrElse cellDate < oldestDate.Value Then
                                    oldestDate = cellDate
                                    oldestDateRowKey = Convert.ToInt32(reader(primaryKeyName))
                                End If
                            End If
                        End If
                    End While

                    reader.Close()

                    ' If no empty cell was found, update the oldest date
                    If Not hasEmptyCell AndAlso oldestDateRowKey <> -1 Then
                        Dim updateQuery As String = $"UPDATE {quadrantTable} SET {columnName} = @Treat WHERE PatientID = @PatientID AND {primaryKeyName} = @PrimaryKeyValue"
                        Using updateCommand As New SqlCommand(updateQuery, connection)
                            updateCommand.Parameters.AddWithValue("@Treat", trt)
                            updateCommand.Parameters.AddWithValue("@PatientID", patientID)
                            updateCommand.Parameters.AddWithValue("@PrimaryKeyValue", oldestDateRowKey)
                            updateCommand.ExecuteNonQuery()
                        End Using
                    ElseIf Not hasEmptyCell Then
                        ' If no update was possible, notify the user
                        MessageBox.Show($"Unable to update {toothName}. Please check the data.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    End If
                End Using
            End Using
        End Using
#End If
    End Sub

    'Last Working Version
    Public Sub AddTreatToQuadrantTables7Important(patientID As Integer, toothNum As Integer, toothName As String, treat As String)
        Exit Sub
#If False Then
        Dim trt As String = treat & vbCrLf & Format(Now.Date, "dd-MM-yyyy")
        ' Determine quadrant table and column
        Dim quadrantTable As String = GetQuadrantTable(toothNum)
        Dim toothColumn As String = GetToothColumn(toothNum)
        'Dim Tooth As String = GetToothFullName()

        If String.IsNullOrEmpty(quadrantTable) OrElse String.IsNullOrEmpty(toothColumn) Then
            Throw New ArgumentException("Invalid ToothNum provided.")
        End If

        ' Build the primary key name dynamically
        Dim primaryKeyName As String = $"{quadrantTable}ID"
        Dim columnName As String = $"{quadrantTable}{toothColumn}"

        ' Establish the connection
        Dim connection As New SqlConnection()
        connection = DentistXDATA.GetConnection()
        Using connection
            connection.Open()
            Dim hasEmptyCell As Boolean = False ' Flag to check if any cell is empty
            ' Query all rows for the patient
            Dim query As String = $"SELECT {primaryKeyName}, {columnName} FROM {quadrantTable} WHERE PatientID = @PatientID"
            Using command As New SqlCommand(query, connection)
                command.Parameters.AddWithValue("@PatientID", patientID)
                Using reader As SqlDataReader = command.ExecuteReader()
                    Dim rowIndex As Integer = 0 ' To track the row number
                    While reader.Read()
                        ' Apply the special conditions
                        If (columnName = "RU7" OrElse columnName = "LU7") AndAlso rowIndex = 0 Then
                            rowIndex += 1 ' Increment row index
                            Continue While ' Skip the first row
                        End If
                        If (columnName = "RU8" OrElse columnName = "LU8") AndAlso (rowIndex = 0 OrElse rowIndex = 1) Then
                            rowIndex += 1 ' Increment row index
                            Continue While ' Skip the first two rows
                        End If
                        If (columnName = "RD7" OrElse columnName = "LD7") AndAlso rowIndex = 3 Then
                            rowIndex += 1 ' Increment row index
                            Continue While ' Skip the last row
                        End If
                        If (columnName = "RD8" OrElse columnName = "LD8") AndAlso (rowIndex = 2 OrElse rowIndex = 3) Then
                            rowIndex += 1 ' Increment row index
                            Continue While ' Skip the last two rows
                        End If

                        ' Check if the cell in the selected column is empty or whitespace
                        If IsDBNull(reader(columnName)) OrElse String.IsNullOrWhiteSpace(reader(columnName).ToString()) OrElse String.IsNullOrEmpty(reader(columnName).ToString()) Then
                            ' Update the empty cell in the specific row
                            Dim primaryKeyValue As Integer = Convert.ToInt32(reader(primaryKeyName)) ' Store the primary key value
                            reader.Close() ' Close the reader before executing an update
                            Dim updateQuery As String = $"UPDATE {quadrantTable} SET {columnName} = @Treat WHERE PatientID = @PatientID AND {primaryKeyName} = @PrimaryKeyValue"
                            Using updateCommand As New SqlCommand(updateQuery, connection)
                                updateCommand.Parameters.AddWithValue("@Treat", trt)
                                updateCommand.Parameters.AddWithValue("@PatientID", patientID)
                                updateCommand.Parameters.AddWithValue("@PrimaryKeyValue", primaryKeyValue)
                                updateCommand.ExecuteNonQuery()
                            End Using

                            hasEmptyCell = True ' Mark that we found an empty cell
                            Exit While ' Exit the loop after updating the first empty cell
                        End If
                    End While
                End Using
            End Using
            ' If no empty cell was found, show a message
            If Not hasEmptyCell Then
                MessageBox.Show($"All cells of the grid for {toothName} tooth have treatments.{vbCrLf}Check the grid for {toothName} tooth...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If
            'instead of displaying a message do this :
            'check the oldest date in the column values depending on this rule for saving treat  = treat & vbCrLf & Format(Now.Date, "dd-MM-yyyy")
            'update the value in the oldest date with the new one
        End Using
#End If
    End Sub


#End Region

End Module
