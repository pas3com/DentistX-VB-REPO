Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections
Imports System.Configuration
Imports Dapper

Public Class Patient_TrtsDATA

    Private ConnectionString As String = DentistXDATA.GetConnection.ConnectionString



    Public Function SelectAll() As IEnumerable(Of Patient_Trts)
        Using conn As New SqlConnection(ConnectionString)
            conn.Open()
            Return conn.Query(Of Patient_Trts)("SELECT * FROM Patient_Trts")
        End Using
    End Function

    Public Function GetAllPatientTreats(ByVal patientID As Integer) As IEnumerable(Of Patient_Trts)
        Using conn As New SqlConnection(ConnectionString)
            conn.Open()
            Dim sql As String = "SELECT * FROM Patient_Trts WHERE PatientID = @PatientID"
            Return conn.Query(Of Patient_Trts)(sql, New With {.PatientID = patientID})
        End Using
    End Function

    Public Function GetPatientToothTrts(ByVal patientID As Integer) As IEnumerable(Of Patient_Trts)
        Using conn As New SqlConnection(ConnectionString)
            Dim sql As String = "SELECT * FROM Patient_Trts WHERE PatientID = @PatientID AND [ToothTrtID] <> NULL"
            Return conn.Query(Of Patient_Trts)(sql, New With {.PatientID = patientID, .TrtID = TrtID})
        End Using

    End Function

    Public Function GetPatientOrthoTrts(ByVal patientID As Integer) As IEnumerable(Of Patient_Trts)
        Using conn As New SqlConnection(ConnectionString)
            Dim sql As String = "SELECT * FROM Patient_Trts WHERE PatientID = @PatientID AND OrthoID IS NOT NULL"
            Return conn.Query(Of Patient_Trts)(sql, New With {.PatientID = patientID})
        End Using

    End Function

    ''' <summary>Gets the first Patient_Trts for this Ortho case (for price display).</summary>
    Public Function GetByOrthoID(patientID As Integer, orthoID As Integer) As Patient_Trts
        Using conn As New SqlConnection(ConnectionString)
            Dim sql As String = "SELECT TOP 1 * FROM Patient_Trts WHERE PatientID = @PatientID AND OrthoID = @OrthoID"
            Return conn.QuerySingleOrDefault(Of Patient_Trts)(sql, New With {.PatientID = patientID, .OrthoID = orthoID})
        End Using
    End Function

    Public Function GetPatientOthorTrts(ByVal patientID As Integer) As IEnumerable(Of Patient_Trts)
        Using conn As New SqlConnection(ConnectionString)
            Dim sql As String = "SELECT * FROM Patient_Trts WHERE PatientID = @PatientID AND [OtherTrtID] <> NULL"
            Return conn.Query(Of Patient_Trts)(sql, New With {.PatientID = patientID})
        End Using

    End Function


    Public Function GetTrtByID(ByVal conn As SqlConnection,
                           ByVal trans As SqlTransaction,
                           ByVal trtID As Integer) As Patient_Trts

        Dim sql As String = "SELECT * FROM Patient_Trts WHERE TrtID = @TrtID"
        Return conn.QuerySingleOrDefault(Of Patient_Trts)(sql, New With {.TrtID = trtID}, transaction:=trans)
    End Function

    Public Function GetTrtByID(ByVal trtID As Integer) As Patient_Trts
        Using conn As New SqlConnection(ConnectionString)
            Dim sql As String = "Select * FROM Patient_Trts WHERE TrtID = @TrtID"
            Return conn.QuerySingleOrDefault(Of Patient_Trts)(sql, New With {.TrtID = trtID})
        End Using
    End Function


    Public Function Select_Record(ByVal clsPatient_Trts As Patient_Trts) As Patient_Trts
        Using conn As New SqlConnection(ConnectionString)
            Dim sql As String = "Select * FROM Patient_Trts WHERE TrtID = @TrtID"
            Return conn.QuerySingleOrDefault(Of Patient_Trts)(sql, New With {.TrtID = clsPatient_Trts.TrtID})
        End Using
    End Function


    'From Patient_TrtsDATA
    Public Function Add(ByVal clsPatient_Trts As Patient_Trts) As Boolean
        Dim RowsAffected As Integer = 0
        Using conn As New SqlConnection(ConnectionString)
            Dim sql As String = "INSERT INTO Patient_Trts (PatientID, ToothTrtID, MobID, Detail, TrtDate, TrtValue, Discount2) VALUES (@PatientID, @ToothTrtID, @MobID, @Detail, @TrtDate, @TrtValue, @Discount2)"
            RowsAffected = conn.Execute(sql, New With {.PatientID = clsPatient_Trts.PatientID, .ToothTrtID = clsPatient_Trts.ToothTrtID, .MobID = clsPatient_Trts.OrthoID, .Detail = clsPatient_Trts.Detail, .TrtDate = clsPatient_Trts.TrtDate, .TrtValue = clsPatient_Trts.TrtValue, .Discount2 = If(clsPatient_Trts.Discount2.HasValue, clsPatient_Trts.Discount2.Value, DBNull.Value)})
            Return RowsAffected > 0
        End Using
    End Function


    ' In Patient_TrtsDATA - Add this method and fix the existing one
    Public Function AddTransactionalAndGetID(conn As SqlConnection, trans As SqlTransaction, clsPatient_Trts As Patient_Trts) As Integer
        Try
            Dim sql As String = "INSERT INTO Patient_Trts  
                      (PatientID, ToothTrtID, OrthoID, OtherTrtID, Detail, TrtDate, TrtValue, IsMultiTooth, Discount, Discount2, DiscountType)  
                      OUTPUT INSERTED.TrtID
                      VALUES  
                      (@PatientID, @ToothTrtID, @OrthoID, @OtherTrtID, @Detail, @TrtDate, @TrtValue, @IsMultiTooth, @Discount, @Discount2, @DiscountType)"

            Dim parameters = New With {
            .PatientID = clsPatient_Trts.PatientID,
            .ToothTrtID = If(clsPatient_Trts.ToothTrtID.HasValue, clsPatient_Trts.ToothTrtID.Value, DBNull.Value),
            .OrthoID = If(clsPatient_Trts.OrthoID.HasValue, clsPatient_Trts.OrthoID.Value, DBNull.Value),
            .OtherTrtID = If(clsPatient_Trts.OtherTrtID.HasValue, clsPatient_Trts.OtherTrtID.Value, DBNull.Value),
            .Detail = clsPatient_Trts.Detail,
            .TrtDate = clsPatient_Trts.TrtDate,
            .TrtValue = clsPatient_Trts.TrtValue,
            .IsMultiTooth = If(clsPatient_Trts.IsMultiTooth.HasValue, clsPatient_Trts.IsMultiTooth, False),
            .Discount = If(clsPatient_Trts.Discount.HasValue, clsPatient_Trts.Discount, 0),
            .Discount2 = If(clsPatient_Trts.Discount2.HasValue, clsPatient_Trts.Discount2.Value, DBNull.Value),
            .DiscountType = If(clsPatient_Trts.DiscountType.HasValue, clsPatient_Trts.DiscountType, 0)
        }

            Dim result = conn.ExecuteScalar(sql, parameters, trans)
            Return If(result IsNot Nothing, Convert.ToInt32(result), -1)
        Catch ex As Exception
            Debug.WriteLine($"Error in AddTransactionalAndGetID: {ex.Message}")
            Throw
        End Try
    End Function

    ' Fix the existing method - ensure OrthoID parameter is included
    Public Function AddTransactional(conn As SqlConnection, trans As SqlTransaction, clsPatient_Trts As Patient_Trts) As Boolean
        Try
            Dim sql As String = "INSERT INTO Patient_Trts  
                      (PatientID, ToothTrtID, OrthoID, OtherTrtID, Detail, TrtDate, TrtValue, IsMultiTooth, Discount, Discount2, DiscountType)  
                      VALUES  
                      (@PatientID, @ToothTrtID, @OrthoID, @OtherTrtID, @Detail, @TrtDate, @TrtValue, @IsMultiTooth, @Discount, @Discount2, @DiscountType)"

            Dim parameters = New With {
            .PatientID = clsPatient_Trts.PatientID,
            .ToothTrtID = If(clsPatient_Trts.ToothTrtID.HasValue, clsPatient_Trts.ToothTrtID.Value, DBNull.Value),
            .OrthoID = If(clsPatient_Trts.OrthoID.HasValue, clsPatient_Trts.OrthoID.Value, DBNull.Value),
            .OtherTrtID = If(clsPatient_Trts.OtherTrtID.HasValue, clsPatient_Trts.OtherTrtID.Value, DBNull.Value),
            .Detail = clsPatient_Trts.Detail,
            .TrtDate = clsPatient_Trts.TrtDate,
            .TrtValue = clsPatient_Trts.TrtValue,
            .IsMultiTooth = If(clsPatient_Trts.IsMultiTooth.HasValue, clsPatient_Trts.IsMultiTooth, False),
            .Discount = If(clsPatient_Trts.Discount.HasValue, clsPatient_Trts.Discount, 0),
            .Discount2 = If(clsPatient_Trts.Discount2.HasValue, clsPatient_Trts.Discount2.Value, DBNull.Value),
            .DiscountType = If(clsPatient_Trts.DiscountType.HasValue, clsPatient_Trts.DiscountType, 0)
        }

            Dim rowsAffected As Integer = conn.Execute(sql, parameters, trans)
            Return rowsAffected > 0
        Catch ex As Exception
            Debug.WriteLine($"Error in AddTransactional: {ex.Message}")
            Throw
        End Try
    End Function
    ' In Patient_TrtsDATA.vb
    Public Function AddTransactionalBefor(conn As SqlConnection, trans As SqlTransaction, clsPatient_Trts As Patient_Trts) As Boolean
        Try
            Dim sql As String = "INSERT INTO Patient_Trts  
                          (PatientID, ToothTrtID, MobID, Detail, TrtDate, TrtValue, IsMultiTooth, Discount, Discount2, DiscountType)  
                          VALUES  
                          (@PatientID, @ToothTrtID, @MobID, @Detail, @TrtDate, @TrtValue, @IsMultiTooth, @Discount, @Discount2, @DiscountType)"

            Dim parameters As New List(Of SqlParameter) From {
            New SqlParameter("@PatientID", clsPatient_Trts.PatientID),
            New SqlParameter("@ToothTrtID", clsPatient_Trts.ToothTrtID),
            New SqlParameter("@MobID", If(clsPatient_Trts.OrthoID.HasValue, clsPatient_Trts.OrthoID.Value, DBNull.Value)),
            New SqlParameter("@Detail", clsPatient_Trts.Detail),
            New SqlParameter("@TrtDate", clsPatient_Trts.TrtDate),
            New SqlParameter("@TrtValue", clsPatient_Trts.TrtValue),
            New SqlParameter("@IsMultiTooth", If(clsPatient_Trts.IsMultiTooth.HasValue, clsPatient_Trts.IsMultiTooth, 0)),
            New SqlParameter("@Discount", If(clsPatient_Trts.Discount.HasValue, clsPatient_Trts.Discount, 0)),
            New SqlParameter("@Discount2", If(clsPatient_Trts.Discount2.HasValue, clsPatient_Trts.Discount2.Value, DBNull.Value)),
            New SqlParameter("@DiscountType", If(clsPatient_Trts.DiscountType.HasValue, clsPatient_Trts.DiscountType, 0))
        }
            'Dim rowsAffected As Integer = conn.Execute(sql, parameters, trans)
            Dim rowsAffected As Integer = conn.Execute(sql, clsPatient_Trts, trans)
            Return rowsAffected > 0
        Catch ex As Exception
            ' Log the error for debugging
            Debug.WriteLine($"Error in AddTransactional: {ex.ToString()}")
            Throw  ' Re-throw the exception to handle it upstream
        End Try
    End Function
    Public Function UpdateTransactional(conn As SqlConnection, trans As SqlTransaction, oldPatient_Trts As Patient_Trts, newPatient_Trts As Patient_Trts) As Boolean
        Try
            Dim sql As String = "UPDATE Patient_Trts SET 
                            PatientID = @NewPatientID, 
                            ToothTrtID = @NewToothTrtID, 
                            OrthoID = @NewOrthoID, 
                            Detail = @NewDetail, 
                            TrtDate = @NewTrtDate, 
                            TrtValue = @NewTrtValue, 
                            IsMultiTooth = @NewIsMultiTooth, 
                            Discount = @NewDiscount, 
                            Discount2 = @NewDiscount2, 
                            DiscountType = @NewDiscountType
                            WHERE TrtID = @TrtID"

            Dim parameters = New With {
                .NewPatientID = newPatient_Trts.PatientID,
                .NewToothTrtID = newPatient_Trts.ToothTrtID,
                .NewOrthoID = If(newPatient_Trts.OrthoID.HasValue, newPatient_Trts.OrthoID.Value, DBNull.Value),
                .NewDetail = newPatient_Trts.Detail,
                .NewTrtDate = newPatient_Trts.TrtDate,
                .NewTrtValue = newPatient_Trts.TrtValue,
                .NewIsMultiTooth = If(newPatient_Trts.IsMultiTooth.HasValue, newPatient_Trts.IsMultiTooth, 0),
                .NewDiscount = If(newPatient_Trts.Discount.HasValue, newPatient_Trts.Discount, 0),
                .NewDiscount2 = If(newPatient_Trts.Discount2.HasValue, newPatient_Trts.Discount2.Value, DBNull.Value),
                .NewDiscountType = If(newPatient_Trts.DiscountType.HasValue, newPatient_Trts.DiscountType, 0),
                .TrtID = oldPatient_Trts.TrtID
            }

            Dim rowsAffected As Integer = conn.Execute(sql, parameters, trans)
            Return rowsAffected > 0
        Catch ex As Exception
            ' Log the error for debugging
            Debug.WriteLine($"Error in UpdateTransactional: {ex.ToString()}")
            Throw  ' Re-throw the exception to handle it upstream
        End Try
    End Function

    Public Function LoadTrtWithScope(trtID As Integer) As Patient_Trts
        Dim trt = GetTrtByID(trtID)
        Dim scope As New Patient_TrtScopeDATA
        trt.Patient_TrtScopeIEnumerable = GetPatient_TrtScope(trtID) '.Select(Function(s) s.ToothCode).ToList()
        trt.Patient_PaysIEnumerable = GetPatient_Pays(trtID) '.ToList()
        Return trt
    End Function




    Public Function Delete(ByVal clsPatient_Trts As Patient_Trts) As Boolean
        Dim deleteStatement As String =
                            "DELETE FROM [Patient_Trts] 
			                WHERE TrtID = @TrtID"
        Using connection As SqlConnection = DentistXDATA.GetConnection()
            connection.Open()
            Dim affectedRows As Integer = connection.Execute(deleteStatement, New With {.TrtID = clsPatient_Trts.TrtID})
            Return affectedRows > 0
        End Using
    End Function

    Public Function DeleteAll(ByVal clsPatient_Trts As Patient_Trts) As Boolean
        Dim deleteStatement As String =
                            "DELETE FROM [Patient_Trts] 
			                WHERE PatientID = @PatientID"
        Using connection As SqlConnection = DentistXDATA.GetConnection()
            connection.Open()
            Dim affectedRows As Integer = connection.Execute(deleteStatement, New With {.PatientID = clsPatient_Trts.PatientID})
            Return affectedRows > 0
        End Using
    End Function
    'Methods to get parents and childs
    Public Function GetPatient(ByVal PatientID As Integer) As Patient
        Dim parent As Patient = Nothing
        Using conn As New SqlConnection(ConnectionString)
            Dim query As String = "SELECT * FROM [Patient] WHERE [PatientID] = @PatientID"
            Try
                conn.Open()
                parent = conn.QuerySingleOrDefault(Of Patient)(query, New With {.PatientId = PatientID})
            Catch ex As Exception
                ' Handle exceptions
            Finally
                If conn.State = ConnectionState.Open Then conn.Close()
            End Try
        End Using
        Return parent
    End Function
    Public Function GetPatient_TrtScope(ByVal trtID As Integer) As IEnumerable(Of Patient_TrtScope)
        Using conn As New SqlConnection(ConnectionString)
            conn.Open()
            Dim query As String = "SELECT * FROM [Patient_TrtScope] WHERE [TrtID] = @TrtID"
            Return conn.Query(Of Patient_TrtScope)(query, New With {.TrtID = trtID})
        End Using
    End Function

    Public Function GetPatient_TrtScope(ByVal clsPatient_Trts As Patient_Trts) As IEnumerable(Of Patient_TrtScope)
        Using conn As New SqlConnection(ConnectionString)
            conn.Open()
            Dim query As String = "SELECT * FROM [Patient_TrtScope] WHERE [TrtID] = @TrtID"
            Return conn.Query(Of Patient_TrtScope)(query, New With {.TrtID = clsPatient_Trts.TrtID})
        End Using
    End Function

    Public Function GetPatient_Pays(ByVal trtID As Integer) As IEnumerable(Of Patient_Pays)
        Using conn As New SqlConnection(ConnectionString)
            conn.Open()
            Dim query As String = "SELECT * FROM [Patient_Pays] WHERE [TrtID] = @TrtID"
            Return conn.Query(Of Patient_Pays)(query, New With {.TrtID = trtID})
        End Using
    End Function
    Public Function GetPatient_Pays(ByVal clsPatient_Trts As Patient_Trts) As IEnumerable(Of Patient_Pays)
        Using conn As New SqlConnection(ConnectionString)
            conn.Open()
            Dim query As String = "SELECT * FROM [Patient_Pays] WHERE [TrtID] = @TrtID"
            Return conn.Query(Of Patient_Pays)(query, New With {.TrtID = clsPatient_Trts.TrtID})
        End Using
    End Function

    ''' <summary>
    ''' Computes treatment total, payment total, and balance in SQL (no <c>PatientBalance</c> stored procedure).
    ''' <see cref="Patient_Pays"/> rows count toward payments unless the row is a <b>cheque</b>
    ''' (English <c>Cheque</c> or Arabic <c>شيك</c>) <b>and</b> <see cref="Patient_Pays.IsReturned"/> is true — those amounts are omitted from total payments, so balance moves accordingly.
    ''' </summary>
    Public Function GetPatientBalance(patientId As Integer) As PatientBalance
        Const sql As String =
            "SELECT " &
            "COALESCE((SELECT SUM(ISNULL(t.[TrtValue], 0)- ISNULL(t.[Discount], 0)- ISNULL(t.[Discount2], 0)) FROM [dbo].[Patient_Trts] t WHERE t.[PatientID] = @PatientID), 0) AS TotalTreatments, " &
            "COALESCE((SELECT SUM(p.[PayValue]) FROM [dbo].[Patient_Pays] p WHERE p.[PatientID] = @PatientID " &
            "    AND NOT ( " &
            "        ISNULL(p.[IsReturned], 0) = 1 " &
            "        AND ( " &
            "            LOWER(LTRIM(RTRIM(ISNULL(p.[PayType], N'')))) = N'cheque' " &
            "            OR LTRIM(RTRIM(ISNULL(p.[PayType], N''))) = N'شيك' " &
            "        ) " &
            "    ) " &
            "), 0) AS TotalPayments"

        Using connection As SqlConnection = DentistXDATA.GetConnection()
            connection.Open()
            Dim row = connection.QueryFirstOrDefault(Of PatientBalanceTotalsRow)(sql, New With {.PatientID = patientId})
            Dim tt = If(row IsNot Nothing, row.TotalTreatments, 0D)
            Dim tp = If(row IsNot Nothing, row.TotalPayments, 0D)
            Dim bal = tp - tt
            Return New PatientBalance(patientId, tt, tp, bal)
        End Using
    End Function

    Private Class PatientBalanceTotalsRow
        Public Property TotalTreatments As Decimal
        Public Property TotalPayments As Decimal
    End Class


End Class
