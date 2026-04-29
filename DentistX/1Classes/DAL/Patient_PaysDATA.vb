Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections
Imports System.Configuration
Imports Dapper

Public Class Patient_PaysDATA

    Private ConnectionString As String = DentistXDATA.GetConnection.ConnectionString




    Public Function SelectAll() As IEnumerable(Of Patient_Pays)
        Using conn As New SqlConnection(ConnectionString)
            conn.Open()
            Return conn.Query(Of Patient_Pays)("SELECT * FROM Patient_Pays")
        End Using
    End Function


    Public Function Select_Record(ByVal clsPatient_Pays As Patient_Pays) As Patient_Pays
        Using conn As New SqlConnection(ConnectionString)
            Dim sql As String = "Select * FROM Patient_Pays WHERE PayID = @PayID"
            Return conn.QuerySingleOrDefault(Of Patient_Pays)(sql, New With {.PayID = clsPatient_Pays.PayID})
        End Using
    End Function
    ' In Patient_TrtsDATA.vb
    Public Function AddTransactional(conn As SqlConnection, trans As SqlTransaction, clsPatient_Pays As Patient_Pays) As Boolean

        Dim sql As String = "INSERT INTO Patient_Pays (TrtID, PatientID, PayValue, PayDate, Notes, PayType, ChqOwner, AccountNumber, ChqNumber, ChqDueDate,
                                                            ChqBank, IsCashed, InsuranceCompany, InsuranceNotes,IsForward, ReceivedBy, IsReturned) 
                                VALUES (@TrtID, @PatientID, @PayValue, @PayDate, @Notes, @PayType, @ChqOwner, @AccountNumber, @ChqNumber, @ChqDueDate, @ChqBank,
                                        @IsCashed, @InsuranceCompany, @InsuranceNotes, @IsForward, @ReceivedBy, @IsReturned)"
        Dim rowsAffected As Integer = conn.Execute(sql, clsPatient_Pays, trans)
        Return rowsAffected > 0
    End Function

    Public Function UpdateTransactional(oldPatient_Pays As Patient_Pays,
                      newPatient_Pays As Patient_Pays,
                      trans As SqlTransaction) As Boolean
        Dim conn As SqlConnection = trans.Connection
        Dim parameters = New With {
            .NewTrtID = newPatient_Pays.TrtID,
            .OldTrtID = oldPatient_Pays.TrtID,
            .NewPatientID = newPatient_Pays.PatientID,
            .OldPatientID = oldPatient_Pays.PatientID,
            .NewPayValue = newPatient_Pays.PayValue,
            .OldPayValue = oldPatient_Pays.PayValue,
            .NewPayDate = newPatient_Pays.PayDate,
            .OldPayDate = oldPatient_Pays.PayDate,
            .NewNotes = newPatient_Pays.Notes,
            .OldNotes = oldPatient_Pays.Notes,
            .NewPayType = newPatient_Pays.PayType,
            .OldPayType = oldPatient_Pays.PayType,
            .NewChqOwner = newPatient_Pays.ChqOwner,
            .OldChqOwner = oldPatient_Pays.ChqOwner,
            .NewAccountNumber = newPatient_Pays.AccountNumber,
            .OldAccountNumber = oldPatient_Pays.AccountNumber,
            .NewChqNumber = newPatient_Pays.ChqNumber,
            .OldChqNumber = oldPatient_Pays.ChqNumber,
            .NewChqDueDate = newPatient_Pays.ChqDueDate,
            .OldChqDueDate = oldPatient_Pays.ChqDueDate,
            .NewChqBank = newPatient_Pays.ChqBank,
            .OldChqBank = oldPatient_Pays.ChqBank,
            .NewIsCashed = newPatient_Pays.IsCashed,
            .OldIsCashed = oldPatient_Pays.IsCashed,
            .NewInsuranceCompany = newPatient_Pays.InsuranceCompany,
            .OldInsuranceCompany = oldPatient_Pays.InsuranceCompany,
            .NewInsuranceNotes = newPatient_Pays.InsuranceNotes,
            .OldInsuranceNotes = oldPatient_Pays.InsuranceNotes,
             .NewIsForward = newPatient_Pays.IsForward,
            .OldIsForward = oldPatient_Pays.IsForward,
            .NewReceivedBy = newPatient_Pays.ReceivedBy,
            .OldReceivedBy = oldPatient_Pays.ReceivedBy,
            .NewIsReturned = newPatient_Pays.IsReturned,
            .OldIsReturned = oldPatient_Pays.IsReturned
    }
        'IsForward
        Dim affectedRows As Integer = conn.Execute(
        "UPDATE [Patient_Pays] SET 
            [TrtID] = @NewTrtID, 
            [PatientID] = @NewPatientID, 
            [PayValue] = @NewPayValue,
            [PayDate] = @NewPayDate, 
            [Notes] = @NewNotes, 
            [PayType] = @NewPayType, 
            [ChqOwner] = @NewChqOwner,
            [AccountNumber] = @NewAccountNumber, 
            [ChqNumber] = @NewChqNumber, 
            [ChqDueDate] = @NewChqDueDate,
            [ChqBank] = @NewChqBank, 
            [IsCashed] = @NewIsCashed, 
            [InsuranceCompany] = @NewInsuranceCompany,
            [InsuranceNotes] = @NewInsuranceNotes,
            [IsForward] = @NewIsForward,
            [ReceivedBy] = @NewReceivedBy,
            [IsReturned] = @NewIsReturned
        WHERE [TrtID] = @OldTrtID 
          AND [PatientID] = @OldPatientID 
          AND [PayValue] = @OldPayValue 
          AND [PayDate] = @OldPayDate 
          AND [Notes] = @OldNotes 
          AND [PayType] = @OldPayType 
          AND [ChqOwner] = @OldChqOwner 
          AND [AccountNumber] = @OldAccountNumber 
          AND [ChqNumber] = @OldChqNumber 
          AND [ChqDueDate] = @OldChqDueDate 
          AND [ChqBank] = @OldChqBank 
          AND [IsCashed] = @OldIsCashed 
          AND [InsuranceCompany] = @OldInsuranceCompany 
          AND [InsuranceNotes] = @OldInsuranceNotes
          AND [IsForward] = @OldIsForward 
          AND ISNULL(CAST([ReceivedBy] AS NVARCHAR(MAX)), N'') = ISNULL(CAST(@OldReceivedBy AS NVARCHAR(MAX)), N'') 
          AND ISNULL(CAST([IsReturned] AS INT), 0) = ISNULL(CAST(@OldIsReturned AS INT), 0) ",
        parameters,
        trans) ' Pass the transaction here
        Return affectedRows > 0
        'how to use it inside transaction
        'Using conn As New SqlConnection(ConnectionString)
        '    conn.Open()
        '    Using trans As SqlTransaction = conn.BeginTransaction()
        '        Try
        '            Dim success = UpdateTransactional(oldPatient_Pays, newPatient_Pays, trans)
        '            If success Then
        '                trans.Commit()
        '            Else
        '                trans.Rollback()
        '            End If
        '        Catch ex As Exception
        '            trans.Rollback()
        '            Throw
        '        End Try
        '    End Using
        'End Using
    End Function

    Public Function Add(ByVal clsPatient_Pays As Patient_Pays) As Boolean
        Dim RowsAffected As Integer = 0
        Using conn As New SqlConnection(ConnectionString)
            Dim sql As String = "INSERT INTO Patient_Pays (TrtID, PatientID, PayValue, PayDate, Notes, PayType, ChqOwner, AccountNumber, ChqNumber, ChqDueDate,
                                                            ChqBank, IsCashed, InsuranceCompany, InsuranceNotes,IsForward, ReceivedBy, IsReturned) 
                                VALUES (@TrtID, @PatientID, @PayValue, @PayDate, @Notes, @PayType, @ChqOwner, @AccountNumber, @ChqNumber, @ChqDueDate, @ChqBank,
                                        @IsCashed, @InsuranceCompany, @InsuranceNotes, @IsForward, @ReceivedBy, @IsReturned)"
            RowsAffected = conn.Execute(sql, New With {.TrtID = clsPatient_Pays.TrtID, .PatientID = clsPatient_Pays.PatientID, .PayValue = clsPatient_Pays.PayValue, .PayDate = clsPatient_Pays.PayDate, .Notes = clsPatient_Pays.Notes, .PayType = clsPatient_Pays.PayType, .ChqOwner = clsPatient_Pays.ChqOwner, .AccountNumber = clsPatient_Pays.AccountNumber, .ChqNumber = clsPatient_Pays.ChqNumber, .ChqDueDate = clsPatient_Pays.ChqDueDate, .ChqBank = clsPatient_Pays.ChqBank, .IsCashed = clsPatient_Pays.IsCashed, .InsuranceCompany = clsPatient_Pays.InsuranceCompany, .IsForward = clsPatient_Pays.IsForward, .ReceivedBy = clsPatient_Pays.ReceivedBy, .IsReturned = clsPatient_Pays.IsReturned})
            Return RowsAffected > 0
        End Using
    End Function

    Public Function Update(oldPatient_Pays As Patient_Pays, newPatient_Pays As Patient_Pays) As Boolean
        Using conn As New SqlConnection(ConnectionString)
            Dim parameters = New With {
                    .NewTrtID = newPatient_Pays.TrtID, .OldTrtID = oldPatient_Pays.TrtID, .NewPatientID = newPatient_Pays.PatientID,
                    .OldPatientID = oldPatient_Pays.PatientID, .NewPayValue = newPatient_Pays.PayValue, .OldPayValue = oldPatient_Pays.PayValue,
                    .NewPayDate = newPatient_Pays.PayDate, .OldPayDate = oldPatient_Pays.PayDate, .NewNotes = newPatient_Pays.Notes,
                    .OldNotes = oldPatient_Pays.Notes, .NewPayType = newPatient_Pays.PayType, .OldPayType = oldPatient_Pays.PayType,
                    .NewChqOwner = newPatient_Pays.ChqOwner, .OldChqOwner = oldPatient_Pays.ChqOwner, .NewAccountNumber = newPatient_Pays.AccountNumber,
                    .OldAccountNumber = oldPatient_Pays.AccountNumber, .NewChqNumber = newPatient_Pays.ChqNumber, .OldChqNumber = oldPatient_Pays.ChqNumber,
                    .NewChqDueDate = newPatient_Pays.ChqDueDate, .OldChqDueDate = oldPatient_Pays.ChqDueDate, .NewChqBank = newPatient_Pays.ChqBank,
                    .OldChqBank = oldPatient_Pays.ChqBank, .NewIsCashed = newPatient_Pays.IsCashed, .OldIsCashed = oldPatient_Pays.IsCashed,
                    .NewInsuranceCompany = newPatient_Pays.InsuranceCompany, .OldInsuranceCompany = oldPatient_Pays.InsuranceCompany,
                    .NewIsForward = newPatient_Pays.IsForward, .OldIsForward = oldPatient_Pays.IsForward,
                    .NewReceivedBy = newPatient_Pays.ReceivedBy, .OldReceivedBy = oldPatient_Pays.ReceivedBy,
                    .NewIsReturned = newPatient_Pays.IsReturned, .OldIsReturned = oldPatient_Pays.IsReturned}
            Dim affectedRows As Integer = conn.Execute("UPDATE [Patient_Pays] SET [TrtID] = @NewTrtID, [PatientID] = @NewPatientID, [PayValue] = @NewPayValue,
                                                        [PayDate] = @NewPayDate, [Notes] = @NewNotes, [PayType] = @NewPayType, [ChqOwner] = @NewChqOwner,
                                                        [AccountNumber] = @NewAccountNumber, [ChqNumber] = @NewChqNumber, [ChqDueDate] = @NewChqDueDate,
                                                        [ChqBank] = @NewChqBank, [IsCashed] = @NewIsCashed, [InsuranceCompany] = @NewInsuranceCompany,
                                                        [IsForward] = @NewIsForward, [ReceivedBy] = @NewReceivedBy, [IsReturned] = @NewIsReturned
                                                        WHERE [TrtID] = @OldTrtID AND [PatientID] = @OldPatientID AND [PayValue] = @OldPayValue AND
                                                        [PayDate] = @OldPayDate AND [Notes] = @OldNotes AND [PayType] = @OldPayType AND
                                                        [ChqOwner] = @OldChqOwner AND [AccountNumber] = @OldAccountNumber AND [ChqNumber] = @OldChqNumber AND
                                                        [ChqDueDate] = @OldChqDueDate AND [ChqBank] = @OldChqBank AND [IsCashed] = @OldIsCashed AND
                                                        [InsuranceCompany] = @OldInsuranceCompany AND [IsForward] = @OldIsForward AND ISNULL(CAST([ReceivedBy] AS NVARCHAR(MAX)), N'') = ISNULL(CAST(@OldReceivedBy AS NVARCHAR(MAX)), N'') AND ISNULL(CAST([IsReturned] AS INT), 0) = ISNULL(CAST(@OldIsReturned AS INT), 0)", parameters)
            Return affectedRows > 0
        End Using

    End Function

    Public Function Delete(ByVal clsPatient_Pays As Patient_Pays) As Boolean
        Dim deleteStatement As String =
            "DELETE FROM [Patient_Pays] 
			WHERE PayID = @PayID"
        Using connection As SqlConnection = DentistXDATA.GetConnection()
            connection.Open()
            Dim affectedRows As Integer = connection.Execute(deleteStatement, New With {.PayID = clsPatient_Pays.PayID})
            Return affectedRows > 0
        End Using
    End Function


    ''' <summary>Deletes all Patient_Pays for a TrtID within a transaction.</summary>
    Public Function DeleteByTrtIDTrans(conn As SqlConnection, trans As SqlTransaction, trtID As Integer) As Boolean
        Dim sql As String = "DELETE FROM Patient_Pays WHERE TrtID = @TrtID"
        conn.Execute(sql, New With {.TrtID = trtID}, trans)
        Return True
    End Function

    ''' <summary>Returns the sum of PayValue for a given TrtID.</summary>
    Public Function GetTotalPayByTrtID(trtID As Integer) As Decimal
        Using conn As New SqlConnection(ConnectionString)
            Dim sql As String = "SELECT ISNULL(SUM(PayValue), 0) FROM Patient_Pays WHERE TrtID = @TrtID"
            Dim result = conn.ExecuteScalar(Of Decimal?)(sql, New With {.TrtID = trtID})
            Return If(result.HasValue, result.Value, 0D)
        End Using
    End Function

    'Methods to get parents and childs
    Public Function GetPatient_Trts(ByVal TrtID As Integer) As Patient_Trts
        Dim parent As Patient_Trts = Nothing
        Using conn As New SqlConnection(ConnectionString)
            Dim query As String = "SELECT * FROM [Patient_Trts] WHERE [TrtID] = @TrtID"
            Try
                conn.Open()
                parent = conn.QuerySingleOrDefault(Of Patient_Trts)(query, New With {.TrtID = TrtID})
            Catch ex As Exception
                ' Handle exceptions
            Finally
                If conn.State = ConnectionState.Open Then conn.Close()
            End Try
        End Using
        Return parent
    End Function


#Region "Due Cheqs"

    Private Function AddWorkingDays(startDate As Date, daysToAdd As Integer) As Date
        Dim result As Date = startDate
        Dim count As Integer = 0
        While count < daysToAdd
            result = result.AddDays(1)
            If result.DayOfWeek <> DayOfWeek.Friday AndAlso result.DayOfWeek <> DayOfWeek.Saturday Then
                count += 1
            End If
        End While
        Return result
    End Function

    Public Function GetDueCheqsAfterWorkingDays(today As Date, workingDaysToAdd As Integer) As List(Of ChequeReminder)
        Using conn As New SqlConnection(ConnectionString)
            Dim sql As String = "
            SELECT
                p.PayID,
                p.PatientID,
                pt.PatientName,
                p.ChqOwner,
                p.AccountNumber,
                p.ChqNumber,
                p.ChqBank,
                p.ChqDueDate,
                p.PayValue,
                p.PayDate,
                p.Notes,
                p.PayType,
                p.IsCashed,
                p.IsForward,
                p.IsReturned
            FROM Patient_Pays p
            LEFT JOIN Patient pt ON pt.PatientID = p.PatientID
            WHERE p.ChqDueDate IS NOT NULL
        "

            Dim rawList = conn.Query(Of ChequeReminder)(sql).ToList()

            ' Add working days
            For Each item In rawList
                If item.ChqDueDate.HasValue Then
                    item.AdjustedDueDate = AddWorkingDays(item.ChqDueDate.Value, workingDaysToAdd)
                End If
            Next

            ' Filter by adjusted due date = today
            Return rawList.Where(Function(r) r.AdjustedDueDate.HasValue AndAlso r.AdjustedDueDate.Value.Date = today.Date).ToList()
        End Using
    End Function

    ''' <summary>
    ''' Cheques whose stored <see cref="ChequeReminder.ChqDueDate"/> falls between <paramref name="today"/> and <paramref name="today"/> + N calendar days (inclusive).
    ''' Adjusted due is left unset (Nothing) — use the cheque due column only for this mode.
    ''' </summary>
    Public Function GetDueCheqsChequeDueWithinNextCalendarDays(today As Date, calendarDaysAhead As Integer) As List(Of ChequeReminder)
        If calendarDaysAhead < 0 Then calendarDaysAhead = 0
        Dim endDate = today.Date.AddDays(calendarDaysAhead)
        Using conn As New SqlConnection(ConnectionString)
            Dim sql As String = "
            SELECT
                p.PayID,
                p.PatientID,
                pt.PatientName,
                p.ChqOwner,
                p.AccountNumber,
                p.ChqNumber,
                p.ChqBank,
                p.ChqDueDate,
                p.PayValue,
                p.PayDate,
                p.Notes,
                p.PayType,
                p.IsCashed,
                p.IsForward,
                p.IsReturned
            FROM Patient_Pays p
            LEFT JOIN Patient pt ON pt.PatientID = p.PatientID
            WHERE p.ChqDueDate IS NOT NULL
              AND CAST(p.ChqDueDate AS DATE) >= CAST(@FromDate AS DATE)
              AND CAST(p.ChqDueDate AS DATE) <= CAST(@ToDate AS DATE)
        "
            Return conn.Query(Of ChequeReminder)(sql, New With {
                .FromDate = today.Date,
                .ToDate = endDate
            }).ToList()
        End Using
    End Function

    Public Function Get5DaysDueCheques(today As Date) As List(Of ChequeReminder)
        Using conn As New SqlConnection(ConnectionString)

            Dim sql As String = "
        WITH DatesWithOffsets AS (
            SELECT 
                p.PayID,
                p.PatientID,
                pt.PatientName,
                p.ChqOwner,
                p.AccountNumber,
                p.ChqNumber,
                p.ChqBank,
                p.ChqDueDate,
                p.PayValue,
                p.PayDate,
                p.Notes,
                p.PayType,
                p.IsCashed,
                p.IsForward,
                p.IsReturned,
                -- Calculate date after adding 5 working days
                dbo.AddWorkingDays(p.ChqDueDate, 5) AS AdjustedDueDate
            FROM 
                Patient_Pays p
                LEFT JOIN Patient pt ON p.PatientID = pt.PatientID
            WHERE 
                p.ChqDueDate IS NOT NULL
        )
        SELECT * 
        FROM DatesWithOffsets
        WHERE CAST(AdjustedDueDate AS DATE) = @TodayDate
        "

            Return conn.Query(Of ChequeReminder)(sql, New With {.TodayDate = today.Date}).ToList()
        End Using
    End Function

    ''' <summary>
    ''' Search cheques by cheque number and/or account number (partial match). Returns cheque details with patient name.
    ''' </summary>
    Public Function SearchChequesByChqOrAccount(chqNumber As String, accountNumber As String) As List(Of ChequeReminder)
        Dim chqTrimmed = If(String.IsNullOrWhiteSpace(chqNumber), Nothing, chqNumber.Trim())
        Dim accTrimmed = If(String.IsNullOrWhiteSpace(accountNumber), Nothing, accountNumber.Trim())
        If String.IsNullOrEmpty(chqTrimmed) AndAlso String.IsNullOrEmpty(accTrimmed) Then
            Return New List(Of ChequeReminder)()
        End If

        Using conn As New SqlConnection(ConnectionString)
            Dim sql As String = "
            SELECT
                p.PayID,
                p.PatientID,
                pt.PatientName,
                p.ChqOwner,
                p.AccountNumber,
                p.ChqNumber,
                p.ChqBank,
                p.ChqDueDate,
                NULL AS AdjustedDueDate,
                p.PayValue,
                p.PayDate,
                p.Notes,
                p.PayType,
                p.IsCashed,
                CAST(p.IsForward AS NVARCHAR(50)) AS IsForward,
                p.IsReturned
            FROM Patient_Pays p
            LEFT JOIN Patient pt ON pt.PatientID = p.PatientID
            WHERE p.PayType = 'Cheque'
            "
            Dim conditions As New List(Of String)()
            Dim param As New DynamicParameters()
            If Not String.IsNullOrEmpty(chqTrimmed) Then
                conditions.Add("p.ChqNumber LIKE @ChqNumber")
                param.Add("ChqNumber", "%" & chqTrimmed & "%")
            End If
            If Not String.IsNullOrEmpty(accTrimmed) Then
                conditions.Add("p.AccountNumber LIKE @AccountNumber")
                param.Add("AccountNumber", "%" & accTrimmed & "%")
            End If
            If conditions.Count > 0 Then
                sql &= " AND " & String.Join(" AND ", conditions)
            End If
            sql &= " ORDER BY p.PayDate DESC, p.PayID DESC"

            Return conn.Query(Of ChequeReminder)(sql, param).ToList()
        End Using
    End Function

    ''' <summary>
    ''' Returns all cheque payments with patient name, ordered by PayDate descending.
    ''' </summary>
    Public Function GetAllCheques() As List(Of ChequeReminder)
        Using conn As New SqlConnection(ConnectionString)
            Dim sql As String = "
            SELECT
                p.PayID,
                p.PatientID,
                pt.PatientName,
                p.ChqOwner,
                p.AccountNumber,
                p.ChqNumber,
                p.ChqBank,
                p.ChqDueDate,
                NULL AS AdjustedDueDate,
                p.PayValue,
                p.PayDate,
                p.Notes,
                p.PayType,
                p.IsCashed,
                CAST(p.IsForward AS NVARCHAR(50)) AS IsForward,
                p.IsReturned
            FROM Patient_Pays p
            LEFT JOIN Patient pt ON pt.PatientID = p.PatientID
            WHERE p.PayType = 'Cheque'
            ORDER BY p.PayDate DESC, p.PayID DESC
            "
            Return conn.Query(Of ChequeReminder)(sql).ToList()
        End Using
    End Function

    ''' <summary>
    ''' Cheque payments marked returned (<see cref="Patient_Pays.IsReturned"/>), newest first — for the Returned Cheques tab default list.
    ''' </summary>
    Public Function GetAllReturnedCheques() As List(Of ChequeReminder)
        Using conn As New SqlConnection(ConnectionString)
            Dim sql As String = "
            SELECT
                p.PayID,
                p.PatientID,
                pt.PatientName,
                p.ChqOwner,
                p.AccountNumber,
                p.ChqNumber,
                p.ChqBank,
                p.ChqDueDate,
                NULL AS AdjustedDueDate,
                p.PayValue,
                p.PayDate,
                p.Notes,
                p.PayType,
                p.IsCashed,
                CAST(p.IsForward AS NVARCHAR(50)) AS IsForward,
                p.IsReturned
            FROM Patient_Pays p
            LEFT JOIN Patient pt ON pt.PatientID = p.PatientID
            WHERE p.PayType = 'Cheque'
              AND ISNULL(p.IsReturned, 0) = 1
            ORDER BY p.PayDate DESC, p.PayID DESC
            "
            Return conn.Query(Of ChequeReminder)(sql).ToList()
        End Using
    End Function

    ''' <summary>
    ''' All cheque payments with optional filters; ordered by PayDate DESC, PayID DESC.
    ''' </summary>
    Public Function GetAllChequesFiltered(patientNameContains As String,
                                          chqNumberContains As String,
                                          Optional payDateFrom As Date? = Nothing,
                                          Optional payDateTo As Date? = Nothing) As List(Of ChequeReminder)
        Dim pat = If(String.IsNullOrWhiteSpace(patientNameContains), Nothing, patientNameContains.Trim())
        Dim chq = If(String.IsNullOrWhiteSpace(chqNumberContains), Nothing, chqNumberContains.Trim())

        Using conn As New SqlConnection(ConnectionString)
            Dim sql As String = "
            SELECT
                p.PayID,
                p.PatientID,
                pt.PatientName,
                p.ChqOwner,
                p.AccountNumber,
                p.ChqNumber,
                p.ChqBank,
                p.ChqDueDate,
                NULL AS AdjustedDueDate,
                p.PayValue,
                p.PayDate,
                p.Notes,
                p.PayType,
                p.IsCashed,
                CAST(p.IsForward AS NVARCHAR(50)) AS IsForward,
                p.IsReturned
            FROM Patient_Pays p
            LEFT JOIN Patient pt ON pt.PatientID = p.PatientID
            WHERE p.PayType = 'Cheque'
            "
            Dim param As New DynamicParameters()
            If Not String.IsNullOrEmpty(pat) Then
                sql &= " AND pt.PatientName LIKE @PatientName "
                param.Add("PatientName", "%" & pat & "%")
            End If
            If Not String.IsNullOrEmpty(chq) Then
                sql &= " AND p.ChqNumber LIKE @ChqNumber "
                param.Add("ChqNumber", "%" & chq & "%")
            End If
            If payDateFrom.HasValue Then
                sql &= " AND CAST(p.PayDate AS DATE) >= @PayDateFrom "
                param.Add("PayDateFrom", payDateFrom.Value.Date)
            End If
            If payDateTo.HasValue Then
                sql &= " AND CAST(p.PayDate AS DATE) <= @PayDateTo "
                param.Add("PayDateTo", payDateTo.Value.Date)
            End If
            sql &= " ORDER BY p.PayDate DESC, p.PayID DESC "

            Return conn.Query(Of ChequeReminder)(sql, param).ToList()
        End Using
    End Function


#End Region



End Class
