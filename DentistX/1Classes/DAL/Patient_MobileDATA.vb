Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections
Imports System.Configuration
Imports Dapper

Public Class Patient_MobileDATA
    Implements IDisposable

    Private ConnectionString As String = DentistXDATA.GetConnection.ConnectionString


    Public Function GetPatient_TrtScope(ByVal clsPatient_Mobile As Patient_Mobile) As IEnumerable(Of Patient_TrtScope)
        Dim sql As String = "SELECT * FROM Patient_TrtScope WHERE MobID = @MobID"
        Using conn As New SqlConnection(ConnectionString)
            conn.Open()
            Return conn.Query(Of Patient_TrtScope)(sql, New With {.MobID = clsPatient_Mobile.MobID})
        End Using
    End Function


    Public Function GetPatient_Trts(ByVal clsPatient_Mobile As Patient_Mobile) As IEnumerable(Of Patient_Trts)
        Dim sql As String = "SELECT * FROM Patient_Trts WHERE MobID = @MobID"
        Using conn As New SqlConnection(ConnectionString)
            conn.Open()
            Return conn.Query(Of Patient_Trts)(sql, New With {.MobID = clsPatient_Mobile.MobID})
        End Using
    End Function

    Public Function SelectAll() As IEnumerable(Of Patient_Mobile)

        Using conn As New SqlConnection(ConnectionString)
            conn.Open()
            Return conn.Query(Of Patient_Mobile)("SELECT * FROM Patient_Mobile  WHERE  MobID=@MobID AND PatientID = @PatientID AND ToothName = @ToothNam")
        End Using
    End Function

    Public Function SelectByID(ByVal clsPatient_Mobile As Patient_Mobile) As Patient_Mobile
        Using conn As New SqlConnection(ConnectionString)
            Dim sql As String = "Select * FROM  Patient_Mobile  WHERE MobID=@MobID "
            Return conn.QuerySingleOrDefault(Of Patient_Mobile)(sql, New With {.MobID = clsPatient_Mobile.MobID})
        End Using
        'AppLogger.Log(CurrentUser.UsName, PatientName, "Patient_ToothTrt", "SELECT")
    End Function
    Public Function Select_Record(ByVal clsPatient_Mobile As Patient_Mobile, Optional ByVal trans As SqlTransaction = Nothing) As Patient_Mobile
        If trans IsNot Nothing Then
            Dim sql As String = "Select * FROM Patient_Mobile WHERE MobID = @MobID AND PatientID = @PatientID AND ToothName = @ToothName"
            Return trans.Connection.QuerySingleOrDefault(Of Patient_Mobile)(
            sql,
           New With {
               .MobID = clsPatient_Mobile.MobID,
               .PatientID = clsPatient_Mobile.PatientID,
               .ToothName = clsPatient_Mobile.ToothName
           },
           transaction:=trans)
        Else
            ' Create new connection (original behavior)
            Using conn As New SqlConnection(ConnectionString)
                Dim sql As String = "Select * FROM Patient_Mobile WHERE MobID = @MobID AND PatientID = @PatientID AND ToothName = @ToothName"
                Return conn.QuerySingleOrDefault(Of Patient_Mobile)(
                sql,
                New With {
               .MobID = clsPatient_Mobile.MobID,
               .PatientID = clsPatient_Mobile.PatientID,
               .ToothName = clsPatient_Mobile.ToothName
                })
            End Using
        End If
    End Function

    Public Function Select_RecordByTrtIDByTNum(ByVal clsPatient_Mobile As Patient_Mobile) As Patient_Mobile
        Using conn As New SqlConnection(ConnectionString)
            'Dim sql As String = "Select * FROM Patient_Mobile WHERE TrtID = @TrtID And PatientID = @PatientID And ToothNum = @ToothNum"
            Dim sql As String = "Select * FROM Patient_Mobile WHERE MobID=@MobID And  PatientID = @PatientID And ToothNum = @ToothNum"
            'Return conn.QuerySingleOrDefault(Of Patient_Mobile)(sql, New With {.TrtID = clsPatient_Mobile.TrtID, .PatientID = clsPatient_Mobile.PatientID, .ToothNum = clsPatient_Mobile.ToothNum})
            Return conn.QuerySingleOrDefault(Of Patient_Mobile)(sql, New With {.MobID = clsPatient_Mobile.MobID,
                                                                                 .PatientID = clsPatient_Mobile.PatientID,
                                                                                 .ToothNum = clsPatient_Mobile.ToothNum})

        End Using
        AppLogger.Log(CurrentUser.UsName, PatientName, "Patient_Mobile", "SELECT")
    End Function

    Public Function Select_BypID_tNum_prpName(ByVal clsPatient_Mobile As Patient_Mobile) As Patient_Mobile
        Using conn As New SqlConnection(ConnectionString)
            'Dim sql As String = "Select * FROM Patient_Mobile WHERE TrtID = @TrtID And PatientID = @PatientID And ToothNum = @ToothNum"
            Dim sql As String = "Select * FROM Patient_Mobile WHERE  PatientID = @PatientID And ToothNum = @ToothNum And PropertyName=@PropertyName"
            'Return conn.QuerySingleOrDefault(Of Patient_Mobile)(sql, New With {.TrtID = clsPatient_Mobile.TrtID, .PatientID = clsPatient_Mobile.PatientID, .ToothNum = clsPatient_Mobile.ToothNum})
            Return conn.QuerySingleOrDefault(Of Patient_Mobile)(sql, New With {.PatientID = clsPatient_Mobile.PatientID,
                                                                                 .ToothNum = clsPatient_Mobile.ToothNum,
                                                                                 .PropertyName = clsPatient_Mobile.PropertyName})

        End Using
    End Function

    Public Function Select_BypID_tNum(ByVal clsPatient_Mobile As Patient_Mobile) As IEnumerable(Of Patient_Mobile)
        Using conn As New SqlConnection(ConnectionString)
            'Dim sql As String = "Select * FROM Patient_Mobile WHERE TrtID = @TrtID And PatientID = @PatientID And ToothNum = @ToothNum"
            Dim sql As String = "Select * FROM Patient_Mobile WHERE  PatientID = @PatientID And ToothNum = @ToothNum"
            'Return conn.QuerySingleOrDefault(Of Patient_Mobile)(sql, New With {.TrtID = clsPatient_Mobile.TrtID, .PatientID = clsPatient_Mobile.PatientID, .ToothNum = clsPatient_Mobile.ToothNum})
            Return conn.Query(Of Patient_Mobile)(sql, New With {.PatientID = clsPatient_Mobile.PatientID,
                                                                                 .ToothNum = clsPatient_Mobile.ToothNum})

        End Using
        AppLogger.Log(CurrentUser.UsName, PatientName, "Patient_Mobile", "SELECT")
    End Function

    Public Function GetMobileLVL(patientID As Integer, toothNum As Byte) As Integer
        Dim query = "SELECT  MAX([LVL])  FROM [Patient_Mobile] WHERE PatientID = @PatientID AND ToothNum = @ToothNum"
        Using connection As New SqlConnection(ConnectionString)
            Return If(connection.QuerySingleOrDefault(Of Integer?)(query, New With {.PatientID = patientID, .ToothNum = toothNum}), -1)
        End Using
        AppLogger.Log(CurrentUser.UsName, PatientName, "Patient_Mobile", "SELECT")

        'SELECT MAX([LVL]) FROM [dbo].[Patient_Mobile]
        'WHERE patientID = 2 And toothNum = 15
    End Function
    Public Function GetTreatLVLs(patientID As Integer, toothNum As Byte) As List(Of Byte)
        Dim query = "SELECT DISTINCT MAX([LVL])  FROM [Patient_Mobile] 
                     WHERE PatientID = @PatientID AND ToothNum = @ToothNum"

        Using connection As New SqlConnection(ConnectionString)
            Try
                connection.Open()
                Dim result = connection.Query(Of Byte)(query, New With {
                .PatientID = patientID,
                .ToothNum = toothNum
            })

                Return If(result?.Any() = True, result.AsList(), New List(Of Byte) From {0})
            Catch
                Return New List(Of Byte) From {0}
            End Try
        End Using
    End Function

    Public Function GetPatientMobile(ByVal PatientID As Integer) As IEnumerable(Of Patient_Mobile)
        Using conn As New SqlConnection(ConnectionString)
            Dim sql As String = "Select dbo.Patient_Mobile.MobID, dbo.Patient_Mobile.PatientID, dbo.Patient_Mobile.ToothNum,
                             dbo.Patient_Mobile.ToothName, dbo.Patient_Mobile.LVL, dbo.Patient_Mobile.PropertyName, dbo.Patient_Mobile.FillColor,
                             dbo.Patient_Mobile.BorderThickness, dbo.Patient_Mobile.BorderColor, dbo.Patient_Mobile.TreatmentType,
                             dbo.Patient_Mobile.TreatDate, dbo.Patient_Mobile.Treat, dbo.Patient_Mobile.TreatPlan,
                             dbo.Patient_Mobile.TreatDetails, dbo.Patient_Mobile.TreatNotes, dbo.Patient_Mobile.Finished,
                             dbo.Patient_Mobile.TreatEndDate, dbo.Patient_Mobile.IsExternal, dbo.Patient_Mobile.ExternalClinicName,
                             dbo.Patient_Mobile.ExternalTreatmentDate, dbo.Patient_Mobile.IsPaid, dbo.Shapes.ShapeID, dbo.Shapes.ShapeName, dbo.Shapes.ShapeDetail,
                             dbo.Shapes.OutID, dbo.Shapes.TopID, dbo.Shapes.INID
                             From dbo.Patient_Mobile INNER Join
                             dbo.Shapes ON dbo.Patient_Mobile.ShapeID = dbo.Shapes.ShapeID
                             Where PatientID = @PatientID"
            Return conn.Query(Of Patient_Mobile)(sql, New With {.PatientId = PatientID})
        End Using
        AppLogger.Log(CurrentUser.UsName, PatientName, "Patient_Mobile", "SELECT")
    End Function
    Public Function GetPatientToothMobiles(ByVal PatientID As Integer, ByVal ToothNum As Integer) As IEnumerable(Of Patient_Mobile)
        Using conn As New SqlConnection(ConnectionString)
            Dim sql As String = "Select dbo.Patient_Mobile.MobID, dbo.Patient_Mobile.PatientID, dbo.Patient_Mobile.ToothNum,
                             dbo.Patient_Mobile.ToothName, dbo.Patient_Mobile.LVL, dbo.Patient_Mobile.PropertyName, dbo.Patient_Mobile.FillColor,
                             dbo.Patient_Mobile.BorderThickness, dbo.Patient_Mobile.BorderColor, dbo.Patient_Mobile.TreatmentType,
                             dbo.Patient_Mobile.TreatDate, dbo.Patient_Mobile.Treat, dbo.Patient_Mobile.TreatPlan,
                             dbo.Patient_Mobile.TreatDetails, dbo.Patient_Mobile.TreatNotes, dbo.Patient_Mobile.Finished,
                             dbo.Patient_Mobile.TreatEndDate, dbo.Patient_Mobile.IsExternal, dbo.Patient_Mobile.ExternalClinicName,
                             dbo.Patient_Mobile.ExternalTreatmentDate, dbo.Patient_Mobile.IsPaid, dbo.Shapes.ShapeID, dbo.Shapes.ShapeName, dbo.Shapes.ShapeDetail,
                             dbo.Shapes.OutID, dbo.Shapes.TopID, dbo.Shapes.INID
                             From dbo.Patient_Mobile INNER Join
                             dbo.Shapes ON dbo.Patient_Mobile.ShapeID = dbo.Shapes.ShapeID
                            Where PatientID = @PatientID AND ToothNum=@ToothNum"
            Return conn.Query(Of Patient_Mobile)(sql, New With {.PatientId = PatientID, .ToothNum = ToothNum})
        End Using
    End Function



    Public Function GetPatientMobiles(patientID As Integer, toothNumList As List(Of Byte)) As IEnumerable(Of Patient_Mobile)
        ' Convert byte list to comma-separated string
        Dim toothNumString As String = String.Join(",", toothNumList)

        ' Create parameterized query (safe from SQL injection)
        Dim query = "SELECT * FROM [Patient_Mobile] 
                WHERE PatientID = @PatientID 
                AND ToothNum IN (SELECT value FROM STRING_SPLIT(@ToothNumList, ',')) "
        'Return Conn.Query(Of Patient_ToothTrt)(Sql, New With {.PatientId = patientID, .ToothNum = ToothNum})
        Using connection As New SqlConnection(ConnectionString)
            Try
                ' Execute query and return true if any matching record exists
                Return Conn.Query(Of Patient_Mobile)(
                query,
                New With {
                    .PatientID = patientID,
                    .ToothNumList = toothNumString
                })
            Catch ex As Exception
                'AppLogger.Log(CurrentUser.UsName, PatientName, "Patient_ToothTrt", $"SELECT Error: {ex.Message}")
                MsgBox(ex.Message)
                Return Nothing
            End Try
        End Using

        'AppLogger.Log(CurrentUser.UsName, PatientName, "Patient_ToothTrt", "SELECT")
    End Function

    Public Function AddTransactional(conn As SqlConnection, trans As SqlTransaction, clsPatient_Mobile As Patient_Mobile) As Boolean
        Dim query As String = "
MERGE INTO [dbo].[Patient_Mobile] AS Target
USING (
    SELECT 
        @PatientID AS PatientID,
        @ShapeID AS ShapeID,
        @ToothNum AS ToothNum,
        @ToothName AS ToothName,
        @LVL AS LVL,
        @PropertyName AS PropertyName,
        @FillColor AS FillColor,
        @BorderThickness AS BorderThickness,
        @BorderColor AS BorderColor,
        @TreatmentType AS TreatmentType,
        @TreatDate AS TreatDate,
        @Treat AS Treat,
        @TreatPlan AS TreatPlan,
        @TreatDetails AS TreatDetails,
        @TreatNotes AS TreatNotes,
        @Finished AS Finished,
        @TreatEndDate AS TreatEndDate,
        @QrtrTable AS QrtrTable,
        @QrtrID AS QrtrID,
        @QrtrAddress AS QrtrAddress,
        @IsExternal AS IsExternal,
        @ExternalClinicName AS ExternalClinicName,
        @ExternalTreatmentDate AS ExternalTreatmentDate,
        @IsPaid AS IsPaid,
        @IsMultiTrt AS IsMultiTrt,
        @ParentToothTrtID AS ParentToothTrtID,
        @TrtGroupID AS TrtGroupID
) AS Source
ON Target.PatientID = Source.PatientID
   AND Target.ShapeID = Source.ShapeID
   AND Target.ToothNum = Source.ToothNum
   AND Target.ToothName = Source.ToothName
   AND Target.PropertyName = Source.PropertyName
WHEN MATCHED THEN
    UPDATE SET
        FillColor = Source.FillColor,
        BorderThickness = Source.BorderThickness,
        BorderColor = Source.BorderColor,
        TreatmentType = Source.TreatmentType,
        TreatDate = Source.TreatDate,
        Treat = Source.Treat,
        TreatPlan = Source.TreatPlan,
        TreatDetails = Source.TreatDetails,
        TreatNotes = Source.TreatNotes,
        Finished = Source.Finished,
        TreatEndDate = Source.TreatEndDate,
        QrtrTable = Source.QrtrTable,
        QrtrID = Source.QrtrID,
        QrtrAddress = Source.QrtrAddress,
        IsExternal = Source.IsExternal,
        ExternalClinicName = Source.ExternalClinicName,
        ExternalTreatmentDate = Source.ExternalTreatmentDate,
        IsPaid = Source.IsPaid,
        IsMultiTrt = Source.IsMultiTrt,
        ParentToothTrtID = Source.ParentToothTrtID,
        TrtGroupID = Source.TrtGroupID
WHEN NOT MATCHED THEN
    INSERT (
        PatientID, ShapeID, ToothNum, ToothName, LVL, PropertyName, FillColor,
        BorderThickness, BorderColor, TreatmentType, TreatDate, Treat, TreatPlan,
        TreatDetails, TreatNotes, Finished, TreatEndDate, QrtrTable, QrtrID,
        QrtrAddress, IsExternal, ExternalClinicName, ExternalTreatmentDate,  IsPaid,
        IsMultiTrt, ParentToothTrtID, TrtGroupID
    )
    VALUES (
        Source.PatientID, Source.ShapeID, Source.ToothNum, Source.ToothName, Source.LVL, Source.PropertyName,
        Source.FillColor, Source.BorderThickness, Source.BorderColor, Source.TreatmentType, Source.TreatDate,
        Source.Treat, Source.TreatPlan, Source.TreatDetails, Source.TreatNotes, Source.Finished, Source.TreatEndDate,
        Source.QrtrTable, Source.QrtrID, Source.QrtrAddress, Source.IsExternal, Source.ExternalClinicName,
        Source.ExternalTreatmentDate,  Source.IsPaid, Source.IsMultiTrt, Source.ParentToothTrtID, Source.TrtGroupID
    );
"

        Dim rowsAffected As Integer = conn.Execute(query, clsPatient_Mobile, trans)
        Return rowsAffected > 0
    End Function


    Public Function Add(ByVal clsPatient_Mobile As Patient_Mobile) As Boolean
        Dim RowsAffected As Integer = 0
        Using conn As New SqlConnection(ConnectionString)
            Dim sql As String = "INSERT INTO Patient_Mobile (PatientID, ShapeID, ToothNum, ToothName, LVL, PropertyName, FillColor, BorderThickness,
                                    BorderColor, TreatmentType, TreatDate, Treat, TreatPlan, TreatDetails, TreatNotes, Finished, TreatEndDate, QrtrTable,
                                    QrtrID, QrtrAddress, IsExternal, ExternalClinicName, ExternalTreatmentDate, IsPaid) 
                                VALUES (@PatientID, @ShapeID, @ToothNum, @ToothName, @LVL, @PropertyName, @FillColor, @BorderThickness, @BorderColor, 
                                        @TreatmentType, @TreatDate, @Treat, @TreatPlan, @TreatDetails, @TreatNotes, @Finished, @TreatEndDate, @QrtrTable,
                                        @QrtrID, @QrtrAddress, @IsExternal, @ExternalClinicName, @ExternalTreatmentDate, @IsPaid)"
            RowsAffected = conn.Execute(sql, New With {.PatientID = clsPatient_Mobile.PatientID, .ShapeID = clsPatient_Mobile.ShapeID,
                                        .ToothNum = clsPatient_Mobile.ToothNum, .ToothName = clsPatient_Mobile.ToothName, .LVL = clsPatient_Mobile.LVL,
                                        .PropertyName = clsPatient_Mobile.PropertyName, .FillColor = clsPatient_Mobile.FillColor,
                                        .BorderThickness = clsPatient_Mobile.BorderThickness, .BorderColor = clsPatient_Mobile.BorderColor,
                                        .TreatmentType = clsPatient_Mobile.TreatmentType, .TreatDate = clsPatient_Mobile.TreatDate, .Treat = clsPatient_Mobile.Treat,
                                        .TreatPlan = clsPatient_Mobile.TreatPlan, .TreatDetails = clsPatient_Mobile.TreatDetails, .TreatNotes = clsPatient_Mobile.TreatNotes,
                                        .Finished = clsPatient_Mobile.Finished, .TreatEndDate = clsPatient_Mobile.TreatEndDate, .QrtrTable = clsPatient_Mobile.QrtrTable,
                                        .QrtrID = clsPatient_Mobile.QrtrID, .QrtrAddress = clsPatient_Mobile.QrtrAddress, .IsExternal = clsPatient_Mobile.IsExternal,
                                        .ExternalClinicName = clsPatient_Mobile.ExternalClinicName, .ExternalTreatmentDate = clsPatient_Mobile.ExternalTreatmentDate,
                                        .IsPaid = clsPatient_Mobile.IsPaid})
            Return RowsAffected > 0
        End Using
    End Function

    Public Function Update1(oldPatient_Mobile As Patient_Mobile, newPatient_Mobile As Patient_Mobile) As Boolean
        Using conn As New SqlConnection(ConnectionString)
            Dim parameters = New With {
                    .NewPatientID = newPatient_Mobile.PatientID, .OldPatientID = oldPatient_Mobile.PatientID, .NewShapeID = newPatient_Mobile.ShapeID,
                    .OldShapeID = oldPatient_Mobile.ShapeID, .NewToothNum = newPatient_Mobile.ToothNum, .OldToothNum = oldPatient_Mobile.ToothNum,
                    .NewToothName = newPatient_Mobile.ToothName, .OldToothName = oldPatient_Mobile.ToothName, .NewLVL = newPatient_Mobile.LVL,
                    .OldLVL = oldPatient_Mobile.LVL, .NewPropertyName = newPatient_Mobile.PropertyName, .OldPropertyName = oldPatient_Mobile.PropertyName,
                    .NewFillColor = newPatient_Mobile.FillColor, .OldFillColor = oldPatient_Mobile.FillColor, .NewBorderThickness = newPatient_Mobile.BorderThickness,
                    .OldBorderThickness = oldPatient_Mobile.BorderThickness, .NewBorderColor = newPatient_Mobile.BorderColor,
                    .OldBorderColor = oldPatient_Mobile.BorderColor, .NewTreatmentType = newPatient_Mobile.TreatmentType,
                    .OldTreatmentType = oldPatient_Mobile.TreatmentType, .NewTreatDate = newPatient_Mobile.TreatDate,
                    .OldTreatDate = oldPatient_Mobile.TreatDate, .NewTreat = newPatient_Mobile.Treat, .OldTreat = oldPatient_Mobile.Treat,
                    .NewTreatPlan = newPatient_Mobile.TreatPlan, .OldTreatPlan = oldPatient_Mobile.TreatPlan, .NewTreatDetails = newPatient_Mobile.TreatDetails,
                    .OldTreatDetails = oldPatient_Mobile.TreatDetails, .NewTreatNotes = newPatient_Mobile.TreatNotes, .OldTreatNotes = oldPatient_Mobile.TreatNotes,
                    .NewFinished = newPatient_Mobile.Finished, .OldFinished = oldPatient_Mobile.Finished, .NewTreatEndDate = newPatient_Mobile.TreatEndDate,
                    .OldTreatEndDate = oldPatient_Mobile.TreatEndDate, .NewQrtrTable = newPatient_Mobile.QrtrTable, .OldQrtrTable = oldPatient_Mobile.QrtrTable,
                    .NewQrtrID = newPatient_Mobile.QrtrID, .OldQrtrID = oldPatient_Mobile.QrtrID, .NewQrtrAddress = newPatient_Mobile.QrtrAddress,
                    .OldQrtrAddress = oldPatient_Mobile.QrtrAddress, .NewIsExternal = newPatient_Mobile.IsExternal, .OldIsExternal = oldPatient_Mobile.IsExternal,
                    .NewExternalClinicName = newPatient_Mobile.ExternalClinicName, .OldExternalClinicName = oldPatient_Mobile.ExternalClinicName,
                    .NewExternalTreatmentDate = newPatient_Mobile.ExternalTreatmentDate, .OldExternalTreatmentDate = oldPatient_Mobile.ExternalTreatmentDate,
                    .NewIsPaid = newPatient_Mobile.IsPaid, .OldIsPaid = oldPatient_Mobile.IsPaid
                                          }
            Dim affectedRows As Integer = conn.Execute("UPDATE [Patient_Mobile] SET [PatientID] = @NewPatientID, [ShapeID] = @NewShapeID, [ToothNum] = @NewToothNum,
                                            [ToothName] = @NewToothName, [LVL] = @NewLVL, [PropertyName] = @NewPropertyName, [FillColor] = @NewFillColor,
                                            [BorderThickness] = @NewBorderThickness, [BorderColor] = @NewBorderColor, [TreatmentType] = @NewTreatmentType,
                                            [TreatDate] = @NewTreatDate, [Treat] = @NewTreat, [TreatPlan] = @NewTreatPlan, [TreatDetails] = @NewTreatDetails,
                                            [TreatNotes] = @NewTreatNotes, [Finished] = @NewFinished, [TreatEndDate] = @NewTreatEndDate, 
                                            [QrtrTable] = @NewQrtrTable, [QrtrID] = @NewQrtrID, [QrtrAddress] = @NewQrtrAddress, [IsExternal] = @NewIsExternal,
                                            [ExternalClinicName] = @NewExternalClinicName, [ExternalTreatmentDate] = @NewExternalTreatmentDate,
                                            [IsPaid] = @NewIsPaid WHERE [PatientID] = @OldPatientID AND [ShapeID] = @OldShapeID AND [ToothNum] = @OldToothNum 
                                            AND [ToothName] = @OldToothName AND [LVL] = @OldLVL AND [PropertyName] = @OldPropertyName 
                                            AND [FillColor] = @OldFillColor AND [BorderThickness] = @OldBorderThickness AND [BorderColor] = @OldBorderColor 
                                            AND [TreatmentType] = @OldTreatmentType AND [TreatDate] = @OldTreatDate AND [Treat] = @OldTreat 
                                            AND [TreatPlan] = @OldTreatPlan AND [TreatDetails] = @OldTreatDetails AND [TreatNotes] = @OldTreatNotes 
                                            AND [Finished] = @OldFinished AND [TreatEndDate] = @OldTreatEndDate AND [QrtrTable] = @OldQrtrTable 
                                            AND [QrtrID] = @OldQrtrID AND [QrtrAddress] = @OldQrtrAddress AND [IsExternal] = @OldIsExternal 
                                            AND [ExternalClinicName] = @OldExternalClinicName AND [ExternalTreatmentDate] = @OldExternalTreatmentDate
                                            AND [IsPaid] = @OldIsPaid", parameters)
            Return affectedRows > 0
        End Using
    End Function

    Public Function Update(oldPatient_Mobile As Patient_Mobile, newPatient_Mobile As Patient_Mobile) As Boolean
        Using conn As New SqlConnection(ConnectionString)
            Dim parameters = New With {
            .NewPatientID = newPatient_Mobile.PatientID,
            .NewShapeID = newPatient_Mobile.ShapeID,
            .NewToothNum = newPatient_Mobile.ToothNum,
            .NewToothName = newPatient_Mobile.ToothName,
            .NewLVL = newPatient_Mobile.LVL,
            .NewPropertyName = newPatient_Mobile.PropertyName,
            .NewFillColor = newPatient_Mobile.FillColor,
            .NewBorderThickness = newPatient_Mobile.BorderThickness,
            .NewBorderColor = newPatient_Mobile.BorderColor,
            .NewTreatmentType = newPatient_Mobile.TreatmentType,
            .NewTreatDate = newPatient_Mobile.TreatDate,
            .NewTreat = newPatient_Mobile.Treat,
            .NewTreatPlan = newPatient_Mobile.TreatPlan,
            .NewTreatDetails = newPatient_Mobile.TreatDetails,
            .NewTreatNotes = newPatient_Mobile.TreatNotes,
            .NewFinished = newPatient_Mobile.Finished,
            .NewTreatEndDate = newPatient_Mobile.TreatEndDate,
            .NewQrtrTable = newPatient_Mobile.QrtrTable,
            .NewQrtrID = newPatient_Mobile.QrtrID,
            .NewQrtrAddress = newPatient_Mobile.QrtrAddress,
            .NewIsExternal = newPatient_Mobile.IsExternal,
            .NewExternalClinicName = newPatient_Mobile.ExternalClinicName,
            .NewExternalTreatmentDate = newPatient_Mobile.ExternalTreatmentDate,
            .NewIsPaid = newPatient_Mobile.IsPaid,
            .OldPatientID = oldPatient_Mobile.PatientID,
            .OldShapeID = oldPatient_Mobile.ShapeID,
            .OldToothNum = oldPatient_Mobile.ToothNum
        }

            Dim query As String = "UPDATE [Patient_Mobile] SET 
            [PatientID] = @NewPatientID, 
            [ShapeID] = @NewShapeID, 
            [ToothNum] = @NewToothNum,
            [ToothName] = @NewToothName, 
            [LVL] = @NewLVL, 
            [PropertyName] = @NewPropertyName, 
            [FillColor] = @NewFillColor,
            [BorderThickness] = @NewBorderThickness, 
            [BorderColor] = @NewBorderColor, 
            [TreatmentType] = @NewTreatmentType,
            [TreatDate] = @NewTreatDate, 
            [Treat] = @NewTreat, 
            [TreatPlan] = @NewTreatPlan, 
            [TreatDetails] = @NewTreatDetails,
            [TreatNotes] = @NewTreatNotes, 
            [Finished] = @NewFinished, 
            [TreatEndDate] = @NewTreatEndDate, 
            [QrtrTable] = @NewQrtrTable, 
            [QrtrID] = @NewQrtrID, 
            [QrtrAddress] = @NewQrtrAddress, 
            [IsExternal] = @NewIsExternal,
            [ExternalClinicName] = @NewExternalClinicName, 
            [ExternalTreatmentDate] = @NewExternalTreatmentDate,
            [IsPaid] = @NewIsPaid 
            WHERE [PatientID] = @OldPatientID AND [ShapeID] = @OldShapeID AND [ToothNum] = @OldToothNum"

            Dim affectedRows As Integer = conn.Execute(query, parameters)
            Return affectedRows > 0
        End Using
    End Function
    Public Function Delete(ByVal clsPatient_Mobile As Patient_Mobile) As Boolean
        Dim deleteStatement As String =
            "DELETE FROM [Patient_Mobile] 
			WHERE MobID = @MobID"
        Using connection As SqlConnection = DentistXDATA.GetConnection()
            connection.Open()
            Dim affectedRows As Integer = connection.Execute(deleteStatement, New With {.MobID = clsPatient_Mobile.MobID})
            Return affectedRows > 0
        End Using
    End Function



    Public Function DeleteTrans(ByVal clsPatient_Mobile As Patient_Mobile) As Boolean
        ' STEP 1: Resolve to parent if this is a child bridge record
        Dim resolvedMob = clsPatient_Mobile
        If clsPatient_Mobile.ParentToothTrtID.HasValue Then
            resolvedMob = SelectByID(clsPatient_Mobile) '.ParentToothTrtID.Value)
            If resolvedMob Is Nothing Then
                MsgBox("Parent Bridge Or Denture record not found for deletion.")
                Return False
            End If
        End If
        '' STEP 2: Validate all required fields
        'If resolvedMob.TrtGroupID = Guid.Empty Then
        '    MsgBox("Missing or invalid TrtGroupID.")
        '    Return False
        'End If
        If resolvedMob.PatientID <= 0 Then
            MsgBox("Missing PatientID.")
            Return False
        End If
        If resolvedMob.MobID <= 0 Then
            MsgBox("Missing or invalid ToothTrtID.")
            Return False
        End If
        ' STEP 3: Execute deletion with full bridge-safe logic
        Dim deleteStatement As String = "
        BEGIN TRY
            BEGIN TRANSACTION;

            DECLARE @DeletedFromTrts INT = 0;
            DECLARE @DeletedFromMobInfo INT = 0;
            DECLARE @DeletedFromMobile INT = 0;
            DECLARE @AllDeleted INT = 0;

            -- 1. Delete from Patient_Trts
            DELETE FROM [Patient_Trts]
            WHERE MobID = @MobID AND PatientID = @PatientID;
            SET @DeletedFromTrts = @@ROWCOUNT;

            -- 2. Delete from Patient_TrtInfo
            DELETE FROM [Patient_MobInfo]
            WHERE (ParentToothTrtID = @ParentToothTrtID OR ParentToothTrtID IS NULL)
              AND PatientID = @PatientID
              AND (TrtGroupID = @TrtGroupID OR TrtGroupID IS NULL);
            SET @DeletedFromMobInfo = @@ROWCOUNT;

            -- 3. If bridge children were deleted, delete child teeth
            IF @DeletedFromMobInfo > 0
            BEGIN
                DELETE FROM [Patient_Mobile]
                WHERE (ParentToothTrtID = @ParentToothTrtID OR ParentToothTrtID IS NULL)
                  AND PatientID = @PatientID
                  AND (TrtGroupID = @TrtGroupID OR TrtGroupID IS NULL);
                SET @DeletedFromMobile = @@ROWCOUNT;
            END
            ELSE
            BEGIN
                -- Otherwise delete the main tooth record
                DELETE FROM [Patient_Mobile]
                WHERE MobID = @MobID AND PatientID = @PatientID;
                SET @DeletedFromMobile = @@ROWCOUNT;
            END

            SET @AllDeleted = @DeletedFromTrts + @DeletedFromMobInfo + @DeletedFromMobile;
            COMMIT TRANSACTION;

            SELECT CASE WHEN @AllDeleted > 0 THEN @AllDeleted ELSE 0 END;
        END TRY
        BEGIN CATCH
            IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
            THROW;
        END CATCH
    "
        Using connection As New SqlConnection(ConnectionString)
            connection.Open()
            Dim result = connection.ExecuteScalar(Of Integer)(deleteStatement,
            New With {
                .MobID = resolvedMob.MobID,
                .PatientID = resolvedMob.PatientID,
                .ParentToothTrtID = resolvedMob.ParentToothTrtID,
                .TrtGroupID = resolvedMob.TrtGroupID
            })

            Return result > 0
        End Using
    End Function

    Public Function DeletePatientMobiles(clsPatient As Patient, toothNumList As List(Of Byte)) As Boolean
        If clsPatient.PatientID <= 0 Then
            MsgBox("Missing PatientID.")
            Return False
        End If

        If toothNumList Is Nothing OrElse toothNumList.Count = 0 Then
            MsgBox("No teeth specified for deletion.")
            Return False
        End If

        Dim toothNumString As String = String.Join(",", toothNumList)

        Dim deleteStatement As String = "
    BEGIN TRY
        BEGIN TRANSACTION;

        DECLARE @DeletedFromMobTrt INT = 0;
        DECLARE @DeletedFromTrts INT = 0;
        DECLARE @DeletedFromMobInfo INT = 0;

        -- First get all MobIDs that will be affected
        DECLARE @MobIDs TABLE (ID INT);
        
        INSERT INTO @MobIDs
        SELECT MobID FROM [Patient_Mobile]
        WHERE PatientID = @PatientID 
        AND ToothNum IN (SELECT value FROM STRING_SPLIT(@ToothNumList, ','));
        
        -- 1. Delete from Patient_Trts
        DELETE FROM [Patient_Trts]
        WHERE MobID IN (SELECT ID FROM @MobIDs)
        AND PatientID = @PatientID;
        SET @DeletedFromTrts = @@ROWCOUNT;

        -- 2. Delete from Patient_MobInfo (including any bridge/denture children)
        DELETE FROM [Patient_MobInfo]
        WHERE (ParentToothTrtID IN (SELECT ID FROM @MobIDs) OR ToothNum IN
         (SELECT value FROM STRING_SPLIT(@ToothNumList, ',')) AND PatientID = @PatientID) ;
        SET @DeletedFromMobInfo = @@ROWCOUNT;

        -- 3. Finally delete from [Patient_Mobile]
        DELETE FROM [Patient_Mobile]
        WHERE PatientID = @PatientID 
        AND ToothNum IN (SELECT value FROM STRING_SPLIT(@ToothNumList, ','));
        SET @DeletedFromMobTrt = @@ROWCOUNT;

        COMMIT TRANSACTION;
        
        SELECT CASE 
            WHEN (@DeletedFromMobTrt + @DeletedFromTrts + @DeletedFromMobInfo) > 0 THEN 1 
            ELSE 0 
        END AS Success;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
    "

        Using connection As New SqlConnection(ConnectionString)
            connection.Open()
            Try
                Dim result = connection.ExecuteScalar(Of Integer)(
                deleteStatement,
                New With {
                    .PatientID = clsPatient.PatientID,
                    .ToothNumList = toothNumString
                })

                If result > 0 Then
                    'AppLogger.Log(CurrentUser.UsName, clsPatient.PatientName, "[Patient_Mobile]",
                    '        $"DELETE for teeth: {toothNumString}")
                End If

                Return result > 0
            Catch ex As Exception
                'AppLogger.Log(CurrentUser.UsName, clsPatient.PatientName, "[Patient_Mobile]",
                '        $"DELETE Error: {ex.Message}")
                MsgBox($"Error deleting treatments: {ex.Message}")
                Return False
            End Try
        End Using
    End Function


    Public Function DeleteTrans1(ByVal clsPatient_Mobile As Patient_Mobile) As Boolean
        Dim deleteStatement As String = "
        DECLARE @DeletedParent BIT = 0;
        DECLARE @DeletedChild BIT = 0;
        
        -- Check and delete child records if they exist
        IF EXISTS (SELECT 1 FROM [Patient_Trts] 
                  WHERE  MobID = @MobID AND PatientID = @PatientID)
        BEGIN
            DELETE FROM [Patient_Trts] 
            WHERE  MobID = @MobID AND PatientID = @PatientID;
            SET @DeletedChild = 1;
        END
        
        -- Check and delete parent record if it exists
        IF EXISTS (SELECT 1 FROM [Patient_Mobile] 
                  WHERE  MobID = @MobID AND PatientID = @PatientID AND ToothNum = @ToothNum)
        BEGIN
            DELETE FROM [Patient_Mobile] 
            WHERE  MobID = @MobID AND PatientID = @PatientID AND ToothNum = @ToothNum;
            SET @DeletedParent = 1;
        END
        
        -- Return 1 if any deletion occurred, 0 if nothing was deleted
        SELECT CASE WHEN @DeletedParent = 1 OR @DeletedChild = 1 THEN 1 ELSE 0 END;"

        Using connection As SqlConnection = DentistXDATA.GetConnection()
            connection.Open()
            Dim result = connection.ExecuteScalar(Of Integer)(deleteStatement,
            New With {
                .ToothTrtID = clsPatient_Mobile.MobID,
                .PatientID = clsPatient_Mobile.PatientID,
                .ToothNum = clsPatient_Mobile.ToothNum
            })

            'If result = 1 Then
            '    AppLogger.Log(CurrentUser.UsName, PatientName, "Patient_ToothTrt", "DELETE")
            'End If

            Return result = 1
        End Using
    End Function

    Public Function SyncExtractionsAndImplantsToMobile() As Integer
        Using conn As New SqlConnection(ConnectionString)
            ' Query to insert only new records that don't already exist in Patient_Mobile
            Dim query As String = "
        INSERT INTO [dbo].[Patient_Mobile]
           ([PatientID]
           ,[ShapeID]
           ,[ToothNum]
           ,[ToothName]
           ,[LVL]
           ,[PropertyName]
           ,[FillColor]
           ,[BorderThickness]
           ,[BorderColor]
           ,[TreatmentType]
           ,[TreatDate]
           ,[Treat]
           ,[TreatPlan]
           ,[TreatDetails]
           ,[TreatNotes]
           ,[Finished]
           ,[TreatEndDate]
           ,[QrtrTable]
           ,[QrtrID]
           ,[QrtrAddress]
           ,[IsExternal]
           ,[ExternalClinicName]
           ,[ExternalTreatmentDate]
           ,[IsPaid])
    
        SELECT 
            PT.[PatientID],
            PT.[ShapeID],
            PT.[ToothNum],
            PT.[ToothName],
            PT.[LVL],
            PT.[PropertyName],
            PT.[FillColor],
            PT.[BorderThickness],
            PT.[BorderColor],
            PT.[TreatmentType],
            PT.[TreatDate],
            PT.[Treat],
            PT.[TreatPlan],
            PT.[TreatDetails],
            PT.[TreatNotes],
            PT.[Finished],
            PT.[TreatEndDate],
            PT.[QrtrTable],
            PT.[QrtrID],
            PT.[QrtrAddress],
            PT.[IsExternal],
            PT.[ExternalClinicName],
            PT.[ExternalTreatmentDate],
            PT.[IsPaid]
        FROM [dbo].[Patient_ToothTrt] PT
        WHERE 
            (PT.[Treat] = 'EXTRACTION' OR PT.[Treat] LIKE '%IMPLANT%')
            AND NOT EXISTS (
                SELECT 1 
                FROM [dbo].[Patient_Mobile] PM 
                WHERE 
                    PM.PatientID = PT.PatientID AND 
                    PM.ShapeID = PT.ShapeID AND 
                    PM.ToothNum = PT.ToothNum
            )"

            Return conn.Execute(query)
        End Using
    End Function

    Public Function SyncExtractionsAndImplantsToMobile1() As Integer
        Using conn As New SqlConnection(ConnectionString)
            ' Query to insert only new records that don't already exist in Patient_Mobile
            Dim query As String = "
        INSERT INTO [dbo].[Patient_Mobile]
           ([PatientID]
           ,[ShapeID]
           ,[ToothNum]
           ,[ToothName]
           ,[LVL]
           ,[PropertyName]
           ,[FillColor]
           ,[BorderThickness]
           ,[BorderColor]
           ,[TreatmentType]
           ,[TreatDate]
           ,[Treat]
           ,[TreatPlan]
           ,[TreatDetails]
           ,[TreatNotes]
           ,[Finished]
           ,[TreatEndDate]
           ,[QrtrTable]
           ,[QrtrID]
           ,[QrtrAddress]
           ,[IsExternal]
           ,[ExternalClinicName]
           ,[ExternalTreatmentDate]
           ,[IsPaid])
    
        SELECT 
            PT.[PatientID],
            PT.[ShapeID],
            PT.[ToothNum],
            PT.[ToothName],
            PT.[LVL],
            PT.[PropertyName],
            PT.[FillColor],
            PT.[BorderThickness],
            PT.[BorderColor],
            PT.[TreatmentType],
            PT.[TreatDate],
            PT.[Treat],
            PT.[TreatPlan],
            PT.[TreatDetails],
            PT.[TreatNotes],
            PT.[Finished],
            PT.[TreatEndDate],
            PT.[QrtrTable],
            PT.[QrtrID],
            PT.[QrtrAddress],
            PT.[IsExternal],
            PT.[ExternalClinicName],
            PT.[ExternalTreatmentDate],
            PT.[IsPaid]
        FROM [dbo].[Patient_Mobile] PT
        LEFT JOIN [dbo].[Patient_Mobile] PM ON 
            PT.PatientID = PM.PatientID AND 
            PT.ShapeID = PM.ShapeID AND 
            PT.ToothNum = PM.ToothNum
        WHERE 
            (PT.[Treat] = 'EXTRACTION' OR PT.[Treat] LIKE '%IMPLANT%')
            AND PM.PatientID IS NULL"  ' This ensures the record doesn't exist in Mobile table

            Return conn.Execute(query)
        End Using
    End Function


    'Methods to get parents and childs
    Public Function GetPatient(ByVal PatientID As Integer) As Patient
        Dim parent As Patient = Nothing
        Using conn As New SqlConnection(ConnectionString)
            Dim query As String = "SELECT * FROM [Patient] WHERE [PatientID] = @PatientID"
            Try
                conn.Open()
                parent = conn.QuerySingleOrDefault(Of Patient)(query, New With {.PatientID = PatientID})
            Catch ex As Exception
                ' Handle exceptions
            Finally
                If conn.State = ConnectionState.Open Then conn.Close()
            End Try
        End Using
        Return parent
    End Function



#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                ' TODO: dispose managed state (managed objects).
            End If

            ' TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
            ' TODO: set large fields to null.
        End If
        disposedValue = True
    End Sub

    ' TODO: override Finalize() only if Dispose(disposing As Boolean) above has code to free unmanaged resources.
    'Protected Overrides Sub Finalize()
    '    ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
    '    Dispose(False)
    '    MyBase.Finalize()
    'End Sub

    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
        Dispose(True)
        ' TODO: uncomment the following line if Finalize() is overridden above.
        ' GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class
