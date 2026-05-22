
Imports System.Data.SqlClient
Imports Dapper
Imports DevExpress.Xpo

Module GridHelper2
    '1. Core Data Structures and Constants
    Public Class TreatmentSaveResult
        Public Property Success As Boolean
        Public Property Message As String
        Public Property ToothTrtID As Integer
        Public Property TrtID As Integer
    End Class

    Public Class PreFetchedData
        Public Property QuadrantIDs As Dictionary(Of Integer, (Integer?, Integer?, Integer?, Integer?))
        Public Property TreatmentLevels As Dictionary(Of Integer, Integer)
        Public Property ExistingTreatments As Dictionary(Of String, List(Of String))
    End Class

    ' Add to your constants section
    Private ReadOnly ValidQuadrantTables As String() = {"RU", "LU", "RD", "LD"}
    Private ReadOnly ValidToothColumns As String() = {"1", "2", "3", "4", "5", "6", "7", "8"}

    '==========================================================================================================
    '2. Pre-Fetch Functions
    Private Function PreFetchRequiredData(patientID As Integer, toothNums As List(Of Integer)) As PreFetchedData
        Dim result As New PreFetchedData With {
            .QuadrantIDs = New Dictionary(Of Integer, (Integer?, Integer?, Integer?, Integer?)),
            .TreatmentLevels = New Dictionary(Of Integer, Integer),
            .ExistingTreatments = New Dictionary(Of String, List(Of String))
        }

        ' Add null check
        If toothNums Is Nothing OrElse toothNums.Count = 0 Then
            Return result
        End If
        Using conn As New SqlConnection(DentistXDATA.GetConnection.ConnectionString())
            conn.Open()

            For Each toothNum In toothNums
                result.QuadrantIDs(toothNum) = (Nothing, Nothing, Nothing, Nothing)
            Next

            For Each toothNum In toothNums
                result.TreatmentLevels(toothNum) = GetTreatmentLevel(patientID, toothNum, conn, Nothing)
            Next
        End Using

        Return result
    End Function

    Private Function GetQuadrantIDs(patientID As Integer, toothNum As Integer,
                                   conn As SqlConnection, trans As SqlTransaction) As (Integer?, Integer?, Integer?, Integer?)
        Dim quadrantTable As String = GetQuadrantTable(toothNum)
        Dim primaryKeyName As String = $"{quadrantTable}ID"

        Dim query As String = $"
        SELECT 
            MIN(CASE WHEN RowNum = 1 THEN {primaryKeyName} END) AS FirstID,
            MIN(CASE WHEN RowNum = 2 THEN {primaryKeyName} END) AS SecondID,
            MIN(CASE WHEN RowNum = 3 THEN {primaryKeyName} END) AS ThirdID,
            MIN(CASE WHEN RowNum = 4 THEN {primaryKeyName} END) AS FourthID
        FROM (
            SELECT {primaryKeyName}, ROW_NUMBER() OVER (ORDER BY {primaryKeyName}) AS RowNum
            FROM {quadrantTable} 
            WHERE PatientID = @PatientID 
            ORDER BY {primaryKeyName}
            OFFSET 0 ROWS FETCH NEXT 4 ROWS ONLY
        ) AS NumberedRows"

        Using cmd As New SqlCommand(query, conn, trans)
            cmd.Parameters.AddWithValue("@PatientID", patientID)

            Using reader As SqlDataReader = cmd.ExecuteReader()
                If reader.Read() Then
                    Return (
                        If(IsDBNull(reader("FirstID")), Nothing, CInt(reader("FirstID"))),
                        If(IsDBNull(reader("SecondID")), Nothing, CInt(reader("SecondID"))),
                        If(IsDBNull(reader("ThirdID")), Nothing, CInt(reader("ThirdID"))),
                        If(IsDBNull(reader("FourthID")), Nothing, CInt(reader("FourthID")))
                    )
                End If
            End Using
        End Using

        Return (Nothing, Nothing, Nothing, Nothing)
    End Function

    Private Function GetTreatmentLevel(patientID As Integer, toothNum As Integer,
                                      conn As SqlConnection, trans As SqlTransaction) As Integer
        Dim query As String = "
        SELECT ISNULL(MAX(LVL), 0) AS MaxLevel
        FROM Patient_ToothTrt 
        WHERE PatientID = @PatientID AND ToothNum = @ToothNum"

        Using cmd As New SqlCommand(query, conn, trans)
            cmd.Parameters.AddWithValue("@PatientID", patientID)
            cmd.Parameters.AddWithValue("@ToothNum", toothNum)

            Return CInt(cmd.ExecuteScalar())
        End Using
    End Function

    Private Function GetExistingTreatments(patientID As Integer, toothNums As List(Of Integer),
                                         conn As SqlConnection, trans As SqlTransaction) As Dictionary(Of String, List(Of String))
        Dim result As New Dictionary(Of String, List(Of String))

        For Each toothNum In toothNums
            Dim quadrantTable As String = GetQuadrantTable(toothNum)
            Dim toothColumn As String = GetToothColumn(toothNum)
            Dim columnName As String = $"{quadrantTable}{toothColumn}"

            Dim query As String = $"SELECT {columnName} FROM {quadrantTable} WHERE PatientID = @PatientID AND {columnName} IS NOT NULL"

            Using cmd As New SqlCommand(query, conn, trans)
                cmd.Parameters.AddWithValue("@PatientID", patientID)

                Using reader As SqlDataReader = cmd.ExecuteReader()
                    Dim treatments As New List(Of String)
                    While reader.Read()
                        treatments.Add(reader(columnName).ToString())
                    End While

                    result(columnName) = treatments
                End Using
            End Using
        Next

        Return result
    End Function

    '==========================================================================================================
    '3. Validation Functions
    Private Function ValidateTreatmentLevels(conn As SqlConnection, trans As SqlTransaction,
                                        toothTrt As Patient_ToothTrt, existingMaxLevel As Integer) As Boolean
        Dim currentLevel As Integer = toothTrt.LVL

        ' Check if treat is a normal one after high level one
        If (existingMaxLevel > 4 AndAlso currentLevel < 4) AndAlso Not TrtSourceHelper.AllowLowLevelTreatOnChartDespiteHighMaxLevel(toothTrt.PatientID, toothTrt.ToothNum, toothTrt.Treat, False) Then
            MessageBox.Show("You Can't Add a Normal Treat On High Level Treat....")
            Return False
        End If

        ' Special case: implant (5) → extraction (4) or extraction (4) → implant (5)
        If (existingMaxLevel > 4 AndAlso currentLevel = 4) Then
            ' Finish old treatment
            Dim updateQuery As String = "
            UPDATE Patient_ToothTrt
            SET LVL = 4
            WHERE PatientID = @PatientID
            AND ToothNum = @ToothNum
            AND LVL > 4"

            Using cmd As New SqlCommand(updateQuery, conn, trans)
                cmd.Parameters.AddWithValue("@PatientID", toothTrt.PatientID)
                cmd.Parameters.AddWithValue("@ToothNum", toothTrt.ToothNum)
                cmd.ExecuteNonQuery()
            End Using
        End If

        Return True
    End Function

    Private Function ValidateTableAndColumnNames(quadrantTable As String, toothColumn As String) As Boolean
        If Not ValidQuadrantTables.Contains(quadrantTable) Then
            Throw New ArgumentException($"Invalid quadrant table: {quadrantTable}")
        End If

        If Not ValidToothColumns.Contains(toothColumn) Then
            Throw New ArgumentException($"Invalid tooth column: {toothColumn}")
        End If

        Return True
    End Function

    '==========================================================================================================
    '4. Core Processing Functions
    Private Function ProcessQuadrantUpdates(conn As SqlConnection, trans As SqlTransaction,
                                      preFetchedData As PreFetchedData, toothTrt As Patient_ToothTrt) As Boolean
        QrtrTable = ""
        QrtrID = 0
        QrtrAddress = 0
        Return True
    End Function

    Private Function FindTargetQuadrantKey(conn As SqlConnection, trans As SqlTransaction,
                                         quadrantTable As String, columnName As String,
                                         patientID As Integer, quadrantIDs As (Integer?, Integer?, Integer?, Integer?)) As Integer?
        Dim primaryKeyName As String = $"{quadrantTable}ID"
        Dim query As String = $"SELECT {primaryKeyName}, {columnName} FROM {quadrantTable} WHERE PatientID = @PatientID ORDER BY {primaryKeyName}"

        Using cmd As New SqlCommand(query, conn, trans)
            cmd.Parameters.AddWithValue("@PatientID", patientID)

            Using reader As SqlDataReader = cmd.ExecuteReader()
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

                If emptyRows.Count > 0 Then
                    Return emptyRows.Min()
                ElseIf oldestDateRowKey <> -1 Then
                    Return oldestDateRowKey
                End If
            End Using
        End Using

        Return Nothing
    End Function

    Private Function UpdateQuadrantTreatment(conn As SqlConnection, trans As SqlTransaction,
                                           quadrantTable As String, columnName As String,
                                           patientID As Integer, treatmentText As String,
                                           primaryKeyValue As Integer, toothNum As Integer,
                                           quadrantIDs As (Integer?, Integer?, Integer?, Integer?)) As Boolean
        Dim primaryKeyName As String = $"{quadrantTable}ID"
        Dim updateQuery As String = $"UPDATE {quadrantTable} SET {columnName} = @Treat WHERE PatientID = @PatientID AND {primaryKeyName} = @PrimaryKeyValue"

        Using cmd As New SqlCommand(updateQuery, conn, trans)
            cmd.Parameters.AddWithValue("@Treat", treatmentText)
            cmd.Parameters.AddWithValue("@PatientID", patientID)
            cmd.Parameters.AddWithValue("@PrimaryKeyValue", primaryKeyValue)

            If cmd.ExecuteNonQuery() > 0 Then
                ' Set quadrant properties
                Dim rowIndex As Integer = GetRowIndexByPrimaryKey(primaryKeyValue, quadrantIDs.Item1, quadrantIDs.Item2, quadrantIDs.Item3, quadrantIDs.Item4)
                Dim cellAddress As Integer = (GetColIndexByToothNum(toothNum) * 10) + rowIndex

                QrtrAddress = cellAddress
                QrtrID = primaryKeyValue
                QrtrTable = quadrantTable

                If treatmentText.StartsWith("IMPLANT") Then
                    SetBackIMPfrmChartToGrid(cellAddress, patientID, quadrantTable, treatmentText)
                End If

                Return True
            End If
        End Using

        Return False
    End Function

    '==========================================================================================================
    '5. Treatment Saving Functions
    Private Function SaveMainTreatment(conn As SqlConnection, trans As SqlTransaction,
                                  toothTrt As Patient_ToothTrt) As Integer
        Dim toothTrtData As New Patient_ToothTrtDATA()

        If Not toothTrtData.AddTransactional(conn, trans, toothTrt) Then
            Throw New Exception($"Failed to save Treatment in Chart '{toothTrt.Treat}' for '{toothTrt.ToothName}'.")
        End If

        ' Get the last inserted ToothTrtID
        Return conn.ExecuteScalar(Of Integer)(
            "SELECT TOP 1 ToothTrtID FROM Patient_ToothTrt ORDER BY ToothTrtID DESC",
            transaction:=trans)
    End Function

    Private Function ProcessMultiToothTreatment(conn As SqlConnection, trans As SqlTransaction, toothName As String,
                                              lastToothTrtID As Integer, treatmentGroupID As Guid) As Boolean
        ' Update ParentToothTrtID for all teeth in group
        Dim masterToothID As Integer = conn.ExecuteScalar(Of Integer)(
            "SELECT MIN(ToothTrtID) FROM Patient_ToothTrt WHERE TrtGroupID = @TrtGroupID",
            New With {.TrtGroupID = treatmentGroupID},
            transaction:=trans)

        Dim rowsUpdated As Integer = conn.Execute(
            "UPDATE Patient_ToothTrt SET ParentToothTrtID = @ParentToothTrtID WHERE TrtGroupID = @TrtGroupID",
            New With {
                .ParentToothTrtID = masterToothID,
                .TrtGroupID = treatmentGroupID
            },
            transaction:=trans)

        ' Insert into Patient_TrtInfo
        Dim toothTrtData As New Patient_ToothTrtDATA()
        Dim trt As Patient_ToothTrt = toothTrtData.Select_Record(
            New Patient_ToothTrt With {
                .ToothTrtID = lastToothTrtID,
                .PatientID = PatientID,
                .ToothName = toothName
            }, trans)

        If trt Is Nothing Then
            Throw New Exception("Failed to retrieve tooth treatment record")
        End If

        Dim rowsInserted As Integer = conn.Execute(
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
                .ToothNum = trt.ToothNum,
                .ToothName = trt.ToothName,
                .TreatDate = trt.TreatDate,
                .Treat = If(trt.Treat, DBNull.Value),
                .TreatNotes = If(trt.TreatNotes, DBNull.Value),
                .IsExternal = trt.IsExternal,
                .ExternalClinicName = If(trt.ExternalClinicName, DBNull.Value),
                .ExternalTreatmentDate = If(trt.ExternalTreatmentDate.HasValue, trt.ExternalTreatmentDate.Value, DBNull.Value)
            },
            transaction:=trans)

        Return rowsInserted > 0
    End Function

    '==========================================================================================================
    '6. Payment Processing Functions
    Private Function ProcessPaymentLogic(trtValue As Double, payValue As Double, isPaid As Boolean) As (payNote As String, isPaid As Boolean)
        ' Changed from CheckBox to Boolean
        Dim payNote As String = ""

        If payValue > 0 OrElse trtValue > 0 Then
            If payValue = trtValue Then
                payNote = "Payed In Full"
                isPaid = True
            ElseIf payValue < trtValue AndAlso payValue > 0 Then
                payNote = "Payed Partially"
                isPaid = True
            ElseIf payValue > trtValue Then
                payNote = "Payed With Advance Payment"
                isPaid = True
            End If
        ElseIf trtValue = 0 AndAlso payValue = 0 Then
            isPaid = False
        End If

        Return (payNote, isPaid)
    End Function

    Private Sub ProcessPaymentAndAccounting(conn As SqlConnection, trans As SqlTransaction,
                                           lastToothTrtID As Integer, trtValue As Double,
                                           payValue As Double, payNote As String, isMultiTooth As Boolean,
                                           toothTrt As Patient_ToothTrt, detailText As String)
        If trtValue > 0 Then
            ' Save accounting record
            Dim clsPatientTrts As New Patient_Trts With {
                .ToothTrtID = lastToothTrtID,
                .Detail = detailText,
                .PatientID = toothTrt.PatientID,
                .TrtDate = toothTrt.TreatDate,
                .TrtValue = trtValue,
                .IsMultiTooth = isMultiTooth
            }

            Dim clsTrtsData As New Patient_TrtsDATA()
            If Not clsTrtsData.AddTransactional(conn, trans, clsPatientTrts) Then
                Throw New Exception($"Failed to save Patient_Treatment '{toothTrt.Treat}'.")
            End If

            ' Get the last TrtID
            Dim lastTrtID As Integer = conn.ExecuteScalar(Of Integer)(
                "SELECT TOP 1 TrtID FROM Patient_Trts ORDER BY TrtID DESC",
                transaction:=trans)

            ' Save payment if applicable
            If payValue > 0 Then
                Dim clsPatientPays As New Patient_Pays With {
                    .Notes = payNote,
                    .PatientID = toothTrt.PatientID,
                    .PayDate = toothTrt.TreatDate,
                    .PayValue = payValue,
                    .TrtID = lastTrtID
                }

                Dim clsPatientPaysData As New Patient_PaysDATA()
                If Not clsPatientPaysData.AddTransactional(conn, trans, clsPatientPays) Then
                    Throw New Exception($"Failed to save Patient_Payment.")
                End If
            End If
        End If
    End Sub

    '==========================================================================================================
    '7. Helper Functions

    Private Function GetDetailText(toothTrt As Patient_ToothTrt, toothNums As List(Of Integer),
                              isMultiTooth As Boolean) As String
        ' Add null check
        If toothNums Is Nothing OrElse toothNums.Count = 0 Then
            Return $"{toothTrt.Treat} ==>> {GetShortToothNameWithDash(toothTrt.ToothNum)}"
        End If

        If isMultiTooth AndAlso toothNums.Count > 0 Then
            Dim teeth As String = String.Join(",", toothNums)
            Return $"{toothTrt.Treat} ==>> {teeth}"
        Else
            Return $"{toothTrt.Treat} ==>> {GetShortToothNameWithDash(toothTrt.ToothNum)}"
        End If
    End Function

    Private Function GetOldTrt(treat As String) As String
        ' Your existing implementation
        Return If(treat.StartsWith("IMPLANT"), treat, $"{treat}{vbCrLf}{Format(Now.Date, "dd-MM-yyyy")}")
    End Function

    '==========================================================================================================
    '8. Final Refactored Main Function
    Public Function SaveTreatmentWith5Trans(PatientID As Integer, _toothTrt As Patient_ToothTrt,
                                  treatmentText As String, toothName As String, _propName As String, _formattedResult As String,
                                  txtTrtPrice As Double, txtPayValue As Double, isPaidChck As Boolean, OnePayAdded As Boolean,
                                  Optional toothNums As List(Of Integer) = Nothing,
                                  Optional selectedToothTrtList As List(Of Patient_ToothTrt) = Nothing,
                                  Optional isMultiTooth As Boolean = False,
                                  Optional treatmentGroupID As Guid = Nothing,
                                  Optional isFirstInGroup As Boolean = True) As Boolean 'TreatmentSaveResult

        Dim result As New TreatmentSaveResult()
        Dim stopwatch As Stopwatch = Stopwatch.StartNew()
        Dim SaveResult As Boolean
        Try
            ' Pre-fetch all required data outside transaction
            Dim preFetchedData = PreFetchRequiredData(PatientID, toothNums)

            Using conn As New SqlConnection(DentistXDATA.GetConnection.ConnectionString())
                conn.Open()

                Using trans As SqlTransaction = conn.BeginTransaction(IsolationLevel.ReadCommitted)
                    Try
                        ' 1. Process quadrant updates
                        If Not ProcessQuadrantUpdates(conn, trans, preFetchedData, _toothTrt) Then
                            result.Message = "Quadrant update failed"
                            Return False 'result
                        End If

                        ' 2. Validate treatment levels
                        Dim existingMaxLevel = preFetchedData.TreatmentLevels(_toothTrt.ToothNum)
                        If Not ValidateTreatmentLevels(conn, trans, _toothTrt, existingMaxLevel) Then
                            result.Success = False
                            result.Message = "Treatment level validation failed"
                            Return False 'result
                        End If

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

                        ' 4. Process payment logic
                        Dim trtValue As Double = Val(txtTrtPrice)
                        Dim payValue As Double = Val(txtPayValue)
                        Dim paymentResult = ProcessPaymentLogic(trtValue, payValue, isPaidChck)

                        _toothTrt.IsPaid = paymentResult.isPaid
                        If _toothTrt.IsExternal Then _toothTrt.IsPaid = True

                        ' 5. Save main treatment
                        Dim lastToothTrtID = SaveMainTreatment(conn, trans, _toothTrt)
                        result.ToothTrtID = lastToothTrtID

                        ' 6. Handle multi-tooth treatment
                        If isMultiTooth Then
                            If Not ProcessMultiToothTreatment(conn, trans, toothName, lastToothTrtID, treatmentGroupID) Then
                                Throw New Exception("Multi-tooth processing failed")
                            End If
                        End If

                        ' 7. Process payment and accounting for in-house treatments
                        If _toothTrt.IsExternal = False AndAlso (Not isMultiTooth OrElse isFirstInGroup) Then
                            Dim detailText = GetDetailText(_toothTrt, toothNums, isMultiTooth)
                            ProcessPaymentAndAccounting(conn, trans, lastToothTrtID, trtValue, payValue,
                                                      paymentResult.payNote, isMultiTooth, _toothTrt, detailText)
                        End If

                        trans.Commit()
                        result.Success = True
                        result.Message = $"Treatment saved successfully in {stopwatch.ElapsedMilliseconds}ms"
                        SaveResult = True
                    Catch ex As Exception
                        trans.Rollback()
                        LogToFile($"Transaction rolled back after {stopwatch.ElapsedMilliseconds}ms" & vbCrLf & ex.Message)
                        Throw
                    End Try
                End Using
            End Using

        Catch ex As Exception
            result.Success = False
            result.Message = $"Error saving treatment: {ex.Message}"
            SaveResult = False
            LogToFile(result.Message & vbCrLf & ex.Message)
        End Try

        Return SaveResult 'result
    End Function



End Module
