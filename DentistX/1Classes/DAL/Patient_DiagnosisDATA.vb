Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections
Imports System.Configuration
Imports Dapper


Public Class Patient_DiagnosisDATA
    Implements IDisposable





    Private ConnectionString As String = DentistXDATA.GetConnection.ConnectionString

    '    SELECT        dbo.Patient_Diagnosis.DiagID, dbo.Patient_Diagnosis.PatientID, dbo.Patient_Diagnosis.ToothNum, dbo.Patient_Diagnosis.ToothName, dbo.Patient_Diagnosis.LVL, dbo.Patient_Diagnosis.PropertyName, dbo.Patient_Diagnosis.FillColor, 
    '                         dbo.Patient_Diagnosis.BorderThickness, dbo.Patient_Diagnosis.BorderColor, dbo.Patient_Diagnosis.TreatmentType, dbo.Patient_Diagnosis.TreatDate, dbo.Patient_Diagnosis.Treat, dbo.Patient_Diagnosis.TreatPlan, 
    '                         dbo.Patient_Diagnosis.TreatDetails, dbo.Patient_Diagnosis.TreatNotes, dbo.Patient_Diagnosis.Finished, dbo.Patient_Diagnosis.TreatEndDate, dbo.Shapes.ShapeID, dbo.Shapes.ShapeName, dbo.Shapes.ShapeDetail, 
    '                         dbo.Shapes.OutID, dbo.Shapes.TopID, dbo.Shapes.INID
    'FROM            dbo.Patient_Diagnosis INNER JOIN
    '                         dbo.Shapes ON dbo.Patient_Diagnosis.ShapeID = dbo.Shapes.ShapeID

    Public Function SelectAll() As IEnumerable(Of Patient_Diagnosis)
        Using conn As New SqlConnection(ConnectionString)
            conn.Open()
            Return conn.Query(Of Patient_Diagnosis)("SELECT * FROM Patient_Diagnosis")
        End Using
        AppLogger.Log(CurrentUser.UsName, PatientName, "Patient_Diagnosis", "SELECT")
    End Function

    Public Function SelectAllByPatient(patientID As Integer) As IEnumerable(Of Patient_Diagnosis)
        Using conn As New SqlConnection(ConnectionString)
            conn.Open()
            Return conn.Query(Of Patient_Diagnosis)("SELECT * FROM Patient_Diagnosis WHERE PatientID=@PatientID", New With {.PatientID = patientID})
        End Using
    End Function

    Public Function SelectAllByIDs(IDs As String) As IEnumerable(Of Patient_Diagnosis)
        Using conn As New SqlConnection(ConnectionString)
            conn.Open()

            'FIX: Changed from Patient_DiagDet to Patient_Diagnosis
            Return conn.Query(Of Patient_Diagnosis)($"SELECT * FROM Patient_Diagnosis WHERE DiagID IN ({IDs})")
        End Using
    End Function
    Public Function Select_Record(ByVal clsPatient_ToothTrt As Patient_Diagnosis,
                             Optional ByVal trans As SqlTransaction = Nothing) As Patient_Diagnosis
        If trans IsNot Nothing Then
            ' Use existing transaction
            Dim sql As String = "SELECT * FROM Patient_Diagnosis 
                           WHERE DiagID=@DiagID 
                           AND PatientID = @PatientID 
                           AND ToothName = @ToothName"

            Return trans.Connection.QuerySingleOrDefault(Of Patient_Diagnosis)(
            sql,
            New With {
                .DiagID = clsPatient_ToothTrt.DiagID,
                .PatientID = clsPatient_ToothTrt.PatientID,
                .ToothName = clsPatient_ToothTrt.ToothName
            },
            transaction:=trans)
        Else
            ' Create new connection (original behavior)
            Using conn As New SqlConnection(ConnectionString)
                Dim sql As String = "SELECT * FROM Patient_Diagnosis 
                               WHERE DiagID=@DiagID 
                               AND PatientID = @PatientID 
                               AND ToothName = @ToothName"

                Return conn.QuerySingleOrDefault(Of Patient_Diagnosis)(
                sql,
                New With {
                    .DiagID = clsPatient_ToothTrt.DiagID,
                    .PatientID = clsPatient_ToothTrt.PatientID,
                    .ToothName = clsPatient_ToothTrt.ToothName
                })
            End Using
        End If

        AppLogger.Log(CurrentUser.UsName, PatientName, "Patient_Diagnosis", "SELECT")
    End Function

    Public Function SelectByID(ByVal clsPatient_ToothTrt As Patient_Diagnosis) As Patient_Diagnosis
        Using conn As New SqlConnection(ConnectionString)
            Dim sql As String = "Select * FROM Patient_Diagnosis WHERE DiagID=@DiagID "
            Return conn.QuerySingleOrDefault(Of Patient_Diagnosis)(sql, New With {.DiagID = clsPatient_ToothTrt.DiagID})
        End Using
        'AppLogger.Log(CurrentUser.UsName, PatientName, "Patient_Diagnosis", "SELECT")
    End Function

    Public Function Select_RecordByDiagIDByTNum(ByVal clsPatient_ToothTrt As Patient_Diagnosis) As Patient_Diagnosis
        Using conn As New SqlConnection(ConnectionString)
            'Dim sql As String = "Select * FROM Patient_Diagnosis WHERE TrtID = @TrtID And PatientID = @PatientID And ToothNum = @ToothNum"
            Dim sql As String = "Select * FROM Patient_Diagnosis WHERE DiagID=@DiagID And  PatientID = @PatientID And ToothNum = @ToothNum"
            'Return conn.QuerySingleOrDefault(Of Patient_Diagnosis)(sql, New With {.TrtID = clsPatient_ToothTrt.TrtID, .PatientID = clsPatient_ToothTrt.PatientID, .ToothNum = clsPatient_ToothTrt.ToothNum})
            Return conn.QuerySingleOrDefault(Of Patient_Diagnosis)(sql, New With {.DiagID = clsPatient_ToothTrt.DiagID,
                                                                                 .PatientID = clsPatient_ToothTrt.PatientID,
                                                                                 .ToothNum = clsPatient_ToothTrt.ToothNum})

        End Using
        AppLogger.Log(CurrentUser.UsName, PatientName, "Patient_Diagnosis", "SELECT")
    End Function

    Public Function Select_BypID_tNum_prpName(ByVal clsPatient_ToothTrt As Patient_Diagnosis) As Patient_Diagnosis
        Using conn As New SqlConnection(ConnectionString)
            'Dim sql As String = "Select * FROM Patient_Diagnosis WHERE TrtID = @TrtID And PatientID = @PatientID And ToothNum = @ToothNum"
            Dim sql As String = "SELECT TOP 1 * FROM Patient_Diagnosis WHERE  PatientID = @PatientID And ToothNum = @ToothNum And PropertyName=@PropertyName ORDER BY DiagID DESC"
            'Return conn.QuerySingleOrDefault(Of Patient_Diagnosis)(sql, New With {.TrtID = clsPatient_ToothTrt.TrtID, .PatientID = clsPatient_ToothTrt.PatientID, .ToothNum = clsPatient_ToothTrt.ToothNum})
            Return conn.QueryFirstOrDefault(Of Patient_Diagnosis)(sql, New With {.PatientID = clsPatient_ToothTrt.PatientID,
                                                                                 .ToothNum = clsPatient_ToothTrt.ToothNum,
                                                                                 .PropertyName = clsPatient_ToothTrt.PropertyName})

        End Using
    End Function

    Public Function Select_BypID_tNum(ByVal clsPatient_ToothTrt As Patient_Diagnosis) As IEnumerable(Of Patient_Diagnosis)
        Using conn As New SqlConnection(ConnectionString)
            'Dim sql As String = "Select * FROM Patient_Diagnosis WHERE TrtID = @TrtID And PatientID = @PatientID And ToothNum = @ToothNum"
            Dim sql As String = "Select * FROM Patient_Diagnosis WHERE  PatientID = @PatientID And ToothNum = @ToothNum"
            'Return conn.QuerySingleOrDefault(Of Patient_Diagnosis)(sql, New With {.TrtID = clsPatient_ToothTrt.TrtID, .PatientID = clsPatient_ToothTrt.PatientID, .ToothNum = clsPatient_ToothTrt.ToothNum})
            Return conn.Query(Of Patient_Diagnosis)(sql, New With {.PatientID = clsPatient_ToothTrt.PatientID,
                                                                                 .ToothNum = clsPatient_ToothTrt.ToothNum})

        End Using
        AppLogger.Log(CurrentUser.UsName, PatientName, "Patient_Diagnosis", "SELECT")
    End Function

    Public Function GetPatientTeethDiags(ByVal PatientID As Integer) As IEnumerable(Of Patient_Diagnosis)
        Using conn As New SqlConnection(ConnectionString)
            Dim sql As String = "Select dbo.Patient_Diagnosis.DiagID, dbo.Patient_Diagnosis.PatientID, dbo.Patient_Diagnosis.ToothNum,
                             dbo.Patient_Diagnosis.ToothName, dbo.Patient_Diagnosis.LVL, dbo.Patient_Diagnosis.PropertyName, dbo.Patient_Diagnosis.FillColor,
                             dbo.Patient_Diagnosis.BorderThickness, dbo.Patient_Diagnosis.BorderColor, dbo.Patient_Diagnosis.TreatmentType,
                             dbo.Patient_Diagnosis.TreatDate, dbo.Patient_Diagnosis.Treat, dbo.Patient_Diagnosis.TreatPlan,
                             dbo.Patient_Diagnosis.TreatDetails, dbo.Patient_Diagnosis.TreatNotes, dbo.Patient_Diagnosis.Finished,
                             dbo.Patient_Diagnosis.TreatEndDate,dbo.Patient_Diagnosis.[QrtrTable],dbo.Patient_Diagnosis.[TrtLoc],
                             dbo.Patient_Diagnosis.[QrtrID],dbo.Patient_Diagnosis.[QrtrAddress],dbo.Patient_Diagnosis.[QrtrColumnName],
                             dbo.Patient_Diagnosis.[QrtrColumnValue],dbo.Patient_Diagnosis.[IsExternal],dbo.Patient_Diagnosis.[ExternalClinicName]   ,
                             dbo.Patient_Diagnosis.[ExternalTreatmentDate],dbo.Patient_Diagnosis.[IsPaid],dbo.Patient_Diagnosis.[IsMultiTrt]           ,
                             dbo.Patient_Diagnosis.[ParentDiagID],dbo.Patient_Diagnosis.[TrtGroupID],dbo.Patient_Diagnosis.[isOnImplant]          ,
                             dbo.Patient_Diagnosis.[UserID],
                             dbo.Shapes.ShapeID, dbo.Shapes.ShapeName, dbo.Shapes.ShapeDetail,dbo.Shapes.OutID, dbo.Shapes.TopID, dbo.Shapes.INID
                             From dbo.Patient_Diagnosis INNER Join
                             dbo.Shapes ON dbo.Patient_Diagnosis.ShapeID = dbo.Shapes.ShapeID 
                             Where PatientID = @PatientID"
            Return conn.Query(Of Patient_Diagnosis)(sql, New With {.PatientId = PatientID})
        End Using
        AppLogger.Log(CurrentUser.UsName, PatientName, "Patient_Diagnosis", "SELECT")
    End Function

    '
    Public Function GetPatientTeethMobDiags(ByVal PatientID As Integer) As IEnumerable(Of Patient_Diagnosis)
        Using conn As New SqlConnection(ConnectionString)
            Dim sql As String = "Select dbo.Patient_Diagnosis.DiagID, dbo.Patient_Diagnosis.PatientID, dbo.Patient_Diagnosis.ToothNum,
                             dbo.Patient_Diagnosis.ToothName, dbo.Patient_Diagnosis.LVL, dbo.Patient_Diagnosis.PropertyName, dbo.Patient_Diagnosis.FillColor,
                             dbo.Patient_Diagnosis.BorderThickness, dbo.Patient_Diagnosis.BorderColor, dbo.Patient_Diagnosis.TreatmentType,
                             dbo.Patient_Diagnosis.TreatDate, dbo.Patient_Diagnosis.Treat, dbo.Patient_Diagnosis.TreatPlan,
                             dbo.Patient_Diagnosis.TreatDetails, dbo.Patient_Diagnosis.TreatNotes, dbo.Patient_Diagnosis.Finished,
                             dbo.Patient_Diagnosis.TreatEndDate, dbo.Patient_Diagnosis.IsExternal, dbo.Patient_Diagnosis.ExternalClinicName,
                             dbo.Patient_Diagnosis.ExternalTreatmentDate, dbo.Patient_Diagnosis.IsPaid, dbo.Shapes.ShapeID, dbo.Shapes.ShapeName, dbo.Shapes.ShapeDetail,
                             dbo.Shapes.OutID, dbo.Shapes.TopID, dbo.Shapes.INID
                             From dbo.Patient_Diagnosis INNER Join
                             dbo.Shapes ON dbo.Patient_Diagnosis.ShapeID = dbo.Shapes.ShapeID
                             Where PatientID = @PatientID AND dbo.Patient_Diagnosis.LVL=9"
            Return conn.Query(Of Patient_Diagnosis)(sql, New With {.PatientId = PatientID})
        End Using
        AppLogger.Log(CurrentUser.UsName, PatientName, "Patient_Diagnosis", "SELECT")
    End Function


    Public Shared Function SelectByPatientAndTooth(ByVal PatientID As Integer, ByVal ToothNum As Integer) As List(Of Patient_Diagnosis)
        Using conn As New SqlConnection(DentistXDATA.GetConnection.ConnectionString)
            Dim sql As String = "Select dbo.Patient_Diagnosis.DiagID, dbo.Patient_Diagnosis.PatientID, dbo.Patient_Diagnosis.ToothNum,
                             dbo.Patient_Diagnosis.ToothName, dbo.Patient_Diagnosis.LVL, dbo.Patient_Diagnosis.PropertyName, dbo.Patient_Diagnosis.FillColor,
                             dbo.Patient_Diagnosis.BorderThickness, dbo.Patient_Diagnosis.BorderColor, dbo.Patient_Diagnosis.TreatmentType,
                             dbo.Patient_Diagnosis.TreatDate, dbo.Patient_Diagnosis.Treat, dbo.Patient_Diagnosis.TreatPlan,
                             dbo.Patient_Diagnosis.TreatDetails, dbo.Patient_Diagnosis.TreatNotes, dbo.Patient_Diagnosis.Finished,
                             dbo.Patient_Diagnosis.TreatEndDate,dbo.Patient_Diagnosis.[QrtrTable],dbo.Patient_Diagnosis.[TrtLoc],
                             dbo.Patient_Diagnosis.[QrtrID],dbo.Patient_Diagnosis.[QrtrAddress],dbo.Patient_Diagnosis.[QrtrColumnName],
                             dbo.Patient_Diagnosis.[QrtrColumnValue],dbo.Patient_Diagnosis.[IsExternal],dbo.Patient_Diagnosis.[ExternalClinicName]   ,
                             dbo.Patient_Diagnosis.[ExternalTreatmentDate],dbo.Patient_Diagnosis.[IsPaid],dbo.Patient_Diagnosis.[IsMultiTrt]           ,
                             dbo.Patient_Diagnosis.[ParentDiagID],dbo.Patient_Diagnosis.[TrtGroupID],dbo.Patient_Diagnosis.[isOnImplant]          ,
                             dbo.Patient_Diagnosis.[UserID],
                             dbo.Shapes.ShapeID, dbo.Shapes.ShapeName, dbo.Shapes.ShapeDetail,dbo.Shapes.OutID, dbo.Shapes.TopID, dbo.Shapes.INID
                             From dbo.Patient_Diagnosis INNER Join
                             dbo.Shapes ON dbo.Patient_Diagnosis.ShapeID = dbo.Shapes.ShapeID
                             Where PatientID = @PatientID AND ToothNum=@ToothNum"
            Return conn.Query(Of Patient_Diagnosis)(sql, New With {.PatientId = PatientID, .ToothNum = ToothNum})
        End Using
        AppLogger.Log(CurrentUser.UsName, PatientName, "Patient_Diagnosis", "SELECT")
    End Function

    Public Shared Function SelectMobByPatientAndTooth(ByVal PatientID As Integer, ByVal ToothNum As Integer) As List(Of Patient_Diagnosis)
        Using conn As New SqlConnection(DentistXDATA.GetConnection.ConnectionString)
            Dim sql As String = "Select dbo.Patient_Diagnosis.DiagID, dbo.Patient_Diagnosis.PatientID, dbo.Patient_Diagnosis.ToothNum,
                             dbo.Patient_Diagnosis.ToothName, dbo.Patient_Diagnosis.LVL, dbo.Patient_Diagnosis.PropertyName, dbo.Patient_Diagnosis.FillColor,
                             dbo.Patient_Diagnosis.BorderThickness, dbo.Patient_Diagnosis.BorderColor, dbo.Patient_Diagnosis.TreatmentType,
                             dbo.Patient_Diagnosis.TreatDate, dbo.Patient_Diagnosis.Treat, dbo.Patient_Diagnosis.TreatPlan,
                             dbo.Patient_Diagnosis.TreatDetails, dbo.Patient_Diagnosis.TreatNotes, dbo.Patient_Diagnosis.Finished,
                             dbo.Patient_Diagnosis.TreatEndDate, dbo.Patient_Diagnosis.IsExternal, dbo.Patient_Diagnosis.ExternalClinicName,
                             dbo.Patient_Diagnosis.ExternalTreatmentDate, dbo.Patient_Diagnosis.IsPaid, dbo.Shapes.ShapeID, dbo.Shapes.ShapeName, dbo.Shapes.ShapeDetail,
                             dbo.Shapes.OutID, dbo.Shapes.TopID, dbo.Shapes.INID
                             From dbo.Patient_Diagnosis INNER Join
                             dbo.Shapes ON dbo.Patient_Diagnosis.ShapeID = dbo.Shapes.ShapeID
                             Where PatientID = @PatientID AND ToothNum=@ToothNum AND dbo.Patient_Diagnosis.LVL=9"
            Return conn.Query(Of Patient_Diagnosis)(sql, New With {.PatientId = PatientID, .ToothNum = ToothNum})
        End Using
        AppLogger.Log(CurrentUser.UsName, PatientName, "Patient_Diagnosis", "SELECT")
    End Function

    Public Function GetPatientToothDiags(ByVal PatientID As Integer, ByVal ToothNum As Integer) As IEnumerable(Of Patient_Diagnosis)
        Using conn As New SqlConnection(ConnectionString)
            Dim sql As String = "Select dbo.Patient_Diagnosis.DiagID, dbo.Patient_Diagnosis.PatientID, dbo.Patient_Diagnosis.ToothNum,
                             dbo.Patient_Diagnosis.ToothName, dbo.Patient_Diagnosis.LVL, dbo.Patient_Diagnosis.PropertyName, dbo.Patient_Diagnosis.FillColor,
                             dbo.Patient_Diagnosis.BorderThickness, dbo.Patient_Diagnosis.BorderColor, dbo.Patient_Diagnosis.TreatmentType,
                             dbo.Patient_Diagnosis.TreatDate, dbo.Patient_Diagnosis.Treat, dbo.Patient_Diagnosis.TreatPlan,
                             dbo.Patient_Diagnosis.TreatDetails, dbo.Patient_Diagnosis.TreatNotes, dbo.Patient_Diagnosis.Finished,
                             dbo.Patient_Diagnosis.TreatEndDate,dbo.Patient_Diagnosis.[QrtrTable],dbo.Patient_Diagnosis.[TrtLoc],
                             dbo.Patient_Diagnosis.[QrtrID],dbo.Patient_Diagnosis.[QrtrAddress],dbo.Patient_Diagnosis.[QrtrColumnName],
                             dbo.Patient_Diagnosis.[QrtrColumnValue],dbo.Patient_Diagnosis.[IsExternal],dbo.Patient_Diagnosis.[ExternalClinicName]   ,
                             dbo.Patient_Diagnosis.[ExternalTreatmentDate],dbo.Patient_Diagnosis.[IsPaid],dbo.Patient_Diagnosis.[IsMultiTrt]           ,
                             dbo.Patient_Diagnosis.[ParentDiagID],dbo.Patient_Diagnosis.[TrtGroupID],dbo.Patient_Diagnosis.[isOnImplant]          ,
                             dbo.Patient_Diagnosis.[UserID],
                             dbo.Shapes.ShapeID, dbo.Shapes.ShapeName, dbo.Shapes.ShapeDetail,dbo.Shapes.OutID, dbo.Shapes.TopID, dbo.Shapes.INID
                             From dbo.Patient_Diagnosis INNER Join
                             dbo.Shapes ON dbo.Patient_Diagnosis.ShapeID = dbo.Shapes.ShapeID
                             Where PatientID = @PatientID AND ToothNum=@ToothNum"
            Return conn.Query(Of Patient_Diagnosis)(sql, New With {.PatientId = PatientID, .ToothNum = ToothNum})
        End Using
        AppLogger.Log(CurrentUser.UsName, PatientName, "Patient_Diagnosis", "SELECT")
    End Function

    Public Function GetPatientToothMobDiags(ByVal PatientID As Integer, ByVal ToothNum As Integer) As IEnumerable(Of Patient_Diagnosis)
        Using conn As New SqlConnection(ConnectionString)
            Dim sql As String = "Select dbo.Patient_Diagnosis.DiagID, dbo.Patient_Diagnosis.PatientID, dbo.Patient_Diagnosis.ToothNum,
                             dbo.Patient_Diagnosis.ToothName, dbo.Patient_Diagnosis.LVL, dbo.Patient_Diagnosis.PropertyName, dbo.Patient_Diagnosis.FillColor,
                             dbo.Patient_Diagnosis.BorderThickness, dbo.Patient_Diagnosis.BorderColor, dbo.Patient_Diagnosis.TreatmentType,
                             dbo.Patient_Diagnosis.TreatDate, dbo.Patient_Diagnosis.Treat, dbo.Patient_Diagnosis.TreatPlan,
                             dbo.Patient_Diagnosis.TreatDetails, dbo.Patient_Diagnosis.TreatNotes, dbo.Patient_Diagnosis.Finished,
                             dbo.Patient_Diagnosis.TreatEndDate, dbo.Patient_Diagnosis.IsExternal, dbo.Patient_Diagnosis.ExternalClinicName,
                             dbo.Patient_Diagnosis.ExternalTreatmentDate, dbo.Patient_Diagnosis.IsPaid, dbo.Shapes.ShapeID, dbo.Shapes.ShapeName, dbo.Shapes.ShapeDetail,
                             dbo.Shapes.OutID, dbo.Shapes.TopID, dbo.Shapes.INID
                             From dbo.Patient_Diagnosis INNER Join
                             dbo.Shapes ON dbo.Patient_Diagnosis.ShapeID = dbo.Shapes.ShapeID
                             Where PatientID = @PatientID AND ToothNum=@ToothNum AND dbo.Patient_Diagnosis.LVL=9"
            Return conn.Query(Of Patient_Diagnosis)(sql, New With {.PatientId = PatientID, .ToothNum = ToothNum})
        End Using
        AppLogger.Log(CurrentUser.UsName, PatientName, "Patient_Diagnosis", "SELECT")
    End Function
    Public Function GetPatientToothTreats1(ByVal PatientID As Integer) As IEnumerable(Of Patient_Diagnosis)
        Using conn As New SqlConnection(ConnectionString)
            Dim sql As String = "Select dbo.Patient_Diagnosis.DiagID, dbo.Patient_Diagnosis.PatientID, dbo.Patient_Diagnosis.ToothNum,
                                 dbo.Patient_Diagnosis.ToothName, dbo.Patient_Diagnosis.LVL, dbo.Patient_Diagnosis.PropertyName, dbo.Patient_Diagnosis.FillColor,
                                 dbo.Patient_Diagnosis.BorderThickness, dbo.Patient_Diagnosis.BorderColor, dbo.Patient_Diagnosis.TreatmentType,
                                 dbo.Patient_Diagnosis.TreatDate, dbo.Patient_Diagnosis.Treat, dbo.Patient_Diagnosis.TreatPlan,
                                 dbo.Patient_Diagnosis.TreatDetails, dbo.Patient_Diagnosis.TreatNotes, dbo.Patient_Diagnosis.Finished,
                                 dbo.Patient_Diagnosis.TreatEndDate, dbo.Shapes.ShapeID, dbo.Shapes.ShapeName, dbo.Shapes.ShapeDetail,
                                 dbo.Shapes.OutID, dbo.Shapes.TopID, dbo.Shapes.INID
                                 From dbo.Patient_Diagnosis INNER Join
                                 dbo.Shapes ON dbo.Patient_Diagnosis.ShapeID = dbo.Shapes.ShapeID
                                 Where PatientID = @PatientID"
            Return conn.Query(Of Patient_Diagnosis)(sql, New With {.PatientId = PatientID})
        End Using
        AppLogger.Log(CurrentUser.UsName, PatientName, "Patient_Diagnosis", "SELECT")
    End Function

    Public Function GetPatientToothTrs(ByVal PatientID As Integer) As IEnumerable(Of Patient_Diagnosis)
        Using conn As New SqlConnection(ConnectionString)
            'Dim sql As String = ""SELECT dbo.Patient_Diagnosis.DiagID, dbo.Patient_Diagnosis.PatientID, dbo.Patient_Diagnosis.ToothNum,
            'dbo.Patient_Diagnosis.ToothName, dbo.Patient_Diagnosis.LVL, dbo.Patient_Diagnosis.PropertyName, dbo.Patient_Diagnosis.FillColor,
            '                     dbo.Patient_Diagnosis.BorderThickness, dbo.Patient_Diagnosis.BorderColor, dbo.Patient_Diagnosis.TreatmentType,
            '                     dbo.Patient_Diagnosis.TreatDate, dbo.Patient_Diagnosis.Treat, dbo.Patient_Diagnosis.TreatPlan,
            '                     dbo.Patient_Diagnosis.TreatDetails, dbo.Patient_Diagnosis.TreatNotes, dbo.Patient_Diagnosis.Finished,
            '                     dbo.Patient_Diagnosis.TreatEndDate, dbo.Shapes.ShapeID, dbo.Shapes.ShapeName, dbo.Shapes.ShapeDetail,
            '                     dbo.Shapes.OutID, dbo.Shapes.TopID, dbo.Shapes.INID
            '                     From dbo.Patient_Diagnosis INNER Join
            '                     dbo.Shapes ON dbo.Patient_Diagnosis.ShapeID = dbo.Shapes.ShapeID
            '                     Where PatientID = @PatientID And ToothNum = @ToothNum And PropertyName=@PropertyName""
            Dim sql As String = "Select * FROM Patient_Diagnosis WHERE  PatientID = @PatientID"
            'Return conn.QuerySingleOrDefault(Of Patient_Diagnosis)(sql, New With {.TrtID = clsPatient_ToothTrt.TrtID, .PatientID = clsPatient_ToothTrt.PatientID, .ToothNum = clsPatient_ToothTrt.ToothNum})
            Return conn.Query(Of Patient_Diagnosis)(sql, New With {.PatientId = PatientID})

        End Using
    End Function
    Public Function Get_TRT_ByFour(ByVal clsPatient_ToothTrt As Patient_Diagnosis) As Patient_Diagnosis
        Using conn As New SqlConnection(ConnectionString)
            'Dim sql As String = "Select * FROM Patient_Diagnosis WHERE TrtID = @TrtID And PatientID = @PatientID And ToothNum = @ToothNum"
            Dim sql As String = "SELECT TOP 1 * FROM Patient_Diagnosis WHERE  PatientID = @PatientID And ToothNum = @ToothNum AND ToothName=@ToothName AND Treat=@Treat ORDER BY DiagID DESC"
            'Return conn.QuerySingleOrDefault(Of Patient_Diagnosis)(sql, New With {.TrtID = clsPatient_ToothTrt.TrtID, .PatientID = clsPatient_ToothTrt.PatientID, .ToothNum = clsPatient_ToothTrt.ToothNum})
            Return conn.QueryFirstOrDefault(Of Patient_Diagnosis)(sql, New With {.PatientID = clsPatient_ToothTrt.PatientID,
                                                                                 .ToothNum = clsPatient_ToothTrt.ToothNum,
                                                                                 .ToothName = clsPatient_ToothTrt.ToothName,
                                                                                 .Treat = clsPatient_ToothTrt.Treat})

        End Using
    End Function

    Public Function Get_TreatmentsByPatientAndTooth(patientID As Integer, toothNum As Byte) As List(Of Patient_Diagnosis)
        Dim query = "SELECT * FROM Patient_Diagnosis WHERE PatientID = @PatientID AND ToothNum = @ToothNum"
        Dim s = "SELECT  [DiagID] FROM  [Patient_Diagnosis] WHERE PatientID=@PatientID AND ToothNum =@ToothNum AND Treat=@Treat"
        Using connection As New SqlConnection(ConnectionString)
            Return connection.Query(Of Patient_Diagnosis)(query, New With {.PatientID = patientID, .ToothNum = toothNum}).ToList()
        End Using
        AppLogger.Log(CurrentUser.UsName, PatientName, "Patient_Diagnosis", "SELECT")
    End Function

    Public Function GetToothTrtIDByPID_TNum_Treat(patientID As Integer, toothNum As Byte, treat As String) As Integer
        Dim query = "SELECT TOP 1 [DiagID] FROM [Patient_Diagnosis] WHERE PatientID = @PatientID AND ToothNum = @ToothNum AND Treat = @Treat ORDER BY DiagID DESC"
        Using connection As New SqlConnection(ConnectionString)
            Return If(connection.QueryFirstOrDefault(Of Integer?)(query, New With {.PatientID = patientID, .ToothNum = toothNum, .Treat = treat}), 0)
        End Using
        AppLogger.Log(CurrentUser.UsName, PatientName, "Patient_Diagnosis", "SELECT")

        'SELECT MAX([LVL]) FROM [dbo].[Patient_Diagnosis]
        'WHERE patientID = 2 And toothNum = 15
    End Function

    Public Function Get_IsRedo(ByVal PatientID As Integer, ByVal ToothNum As Byte, ByVal Trt As String) As Boolean
        ' Define the connection string
        Dim connectionString As String = DentistXDATA.GetConnection.ConnectionString

        Using conn As New SqlConnection(connectionString)
            Dim sql As String = "SELECT TOP 1 Treat FROM Patient_Diagnosis 
                             WHERE PatientID = @PatientID 
                               AND ToothNum = @ToothNum 
                               AND Treat LIKE LEFT(@Trt, 2) + '%'
                             ORDER BY TreatDate DESC"

            ' Attempt to retrieve a matching Treat value
            Dim result As String = conn.QueryFirstOrDefault(Of String)(sql, New With {
                                            .PatientID = PatientID,
                                            .ToothNum = ToothNum,
                                            .Trt = Trt
                                        })

            ' Return True if a value was found; otherwise, return False.
            Return Not String.IsNullOrEmpty(result)
        End Using
    End Function

    Public Function GetTreatLVL(patientID As Integer, toothNum As Byte) As Integer
        Dim query = "SELECT  MAX([LVL])  FROM [Patient_Diagnosis] WHERE PatientID = @PatientID AND ToothNum = @ToothNum"
        Using connection As New SqlConnection(ConnectionString)
            Return If(connection.QuerySingleOrDefault(Of Integer?)(query, New With {.PatientID = patientID, .ToothNum = toothNum}), -1)
        End Using
    End Function
    Public Function GetTreatLVLs(patientID As Integer, toothNum As Byte) As List(Of Byte)
        Dim query = "SELECT DISTINCT MAX([LVL]) FROM [Patient_Diagnosis] " &
                "WHERE PatientID = @PatientID AND ToothNum = @ToothNum"

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
    'AppLogger.Log(CurrentUser.UsName, PatientName, "Patient_Diagnosis", "SELECT")

    Public Function GetTreat(patientID As Integer, toothNumList As List(Of Byte), Trt As String) As Boolean
        ' Convert byte list to comma-separated string
        Dim toothNumString As String = String.Join(",", toothNumList)

        ' Create parameterized query (safe from SQL injection)
        Dim query = "SELECT 1 FROM [Patient_Diagnosis] 
                WHERE PatientID = @PatientID 
                AND ToothNum IN (SELECT value FROM STRING_SPLIT(@ToothNumList, ',')) 
                AND Treat = @Trt"

        Using connection As New SqlConnection(ConnectionString)
            Try
                ' Execute query and return true if any matching record exists
                Dim result = connection.QueryFirstOrDefault(Of Integer)(
                query,
                New With {
                    .PatientID = patientID,
                    .ToothNumList = toothNumString,
                    .Trt = Trt
                })

                Return result = 1
            Catch ex As Exception
                AppLogger.Log(CurrentUser.UsName, PatientName, "Patient_Diagnosis", $"SELECT Error: {ex.Message}")
                Return False
            End Try
        End Using

        AppLogger.Log(CurrentUser.UsName, PatientName, "Patient_Diagnosis", "SELECT")
    End Function
    Public Function Add(ByVal clsPatient_ToothTrt As Patient_Diagnosis, ByRef DiagID As Integer) As Boolean
        Dim query As String = "
    MERGE INTO [dbo].[Patient_Diagnosis] AS Target
    USING (VALUES
        (@PatientID, @ShapeID, @ToothNum, @ToothName, @LVL, @PropertyName, @FillColor, @BorderThickness, @BorderColor,
         @TreatmentType, @TreatDate, @Treat, @TreatPlan, @TreatDetails, @TreatNotes, @Finished, @TreatEndDate,
         @TrtLoc, @QrtrID, @QrtrTable, @QrtrColumnName, @QrtrColumnValue, @QrtrAddress, @IsExternal,
         @ExternalClinicName, @ExternalTreatmentDate, @IsPaid, @IsMultiTrt, @ParentDiagID, @TrtGroupID, @isOnImplant, @UserID)
    ) AS Source (PatientID, ShapeID, ToothNum, ToothName, LVL, PropertyName, FillColor, BorderThickness, BorderColor,
                TreatmentType, TreatDate, Treat, TreatPlan, TreatDetails, TreatNotes, Finished, TreatEndDate,
                TrtLoc, QrtrID, QrtrTable, QrtrColumnName, QrtrColumnValue, QrtrAddress, IsExternal,
                ExternalClinicName, ExternalTreatmentDate, IsPaid, IsMultiTrt, ParentDiagID, TrtGroupID, isOnImplant, UserID)
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
            TrtLoc = Source.TrtLoc,
            QrtrID = Source.QrtrID,
            QrtrTable = Source.QrtrTable,
            QrtrColumnName = Source.QrtrColumnName,
            QrtrColumnValue = Source.QrtrColumnValue,
            QrtrAddress = Source.QrtrAddress,
            IsExternal = Source.IsExternal,
            ExternalClinicName = Source.ExternalClinicName,
            ExternalTreatmentDate = Source.ExternalTreatmentDate,
            IsPaid = Source.IsPaid,
            IsMultiTrt = Source.IsMultiTrt,
            ParentDiagID = Source.ParentDiagID,
            TrtGroupID = Source.TrtGroupID,
            isOnImplant = Source.isOnImplant,
            UserID = Source.UserID
    WHEN NOT MATCHED THEN
        INSERT ([PatientID],[ShapeID],[ToothNum],[ToothName],[LVL],[PropertyName],
                [FillColor],[BorderThickness],[BorderColor],[TreatmentType],[TreatDate],[Treat],
                [TreatPlan],[TreatDetails],[TreatNotes],[Finished],[TreatEndDate],
                [TrtLoc],[QrtrID],[QrtrTable],[QrtrColumnName],[QrtrColumnValue],[QrtrAddress],
                [IsExternal],[ExternalClinicName],[ExternalTreatmentDate],[IsPaid],
                [IsMultiTrt],[ParentDiagID],[TrtGroupID],[isOnImplant],[UserID])
        VALUES (Source.PatientID, Source.ShapeID, Source.ToothNum, Source.ToothName, Source.LVL, Source.PropertyName,
                Source.FillColor, Source.BorderThickness, Source.BorderColor, Source.TreatmentType, Source.TreatDate,
                Source.Treat, Source.TreatPlan, Source.TreatDetails, Source.TreatNotes, Source.Finished,
                Source.TreatEndDate, Source.TrtLoc, Source.QrtrID, Source.QrtrTable, Source.QrtrColumnName,
                Source.QrtrColumnValue, Source.QrtrAddress, Source.IsExternal,
                Source.ExternalClinicName, Source.ExternalTreatmentDate, Source.IsPaid,
                Source.IsMultiTrt, Source.ParentDiagID, Source.TrtGroupID, Source.isOnImplant, Source.UserID);
    "

        Using conn As New SqlConnection(ConnectionString)
            ' Execute the query and get the TrtID for newly inserted rows
            Dim result As Object = conn.ExecuteScalar(query, clsPatient_ToothTrt)

            If result IsNot Nothing Then
                DiagID = Convert.ToInt32(result) ' Set the TrtID from the result
                PatientDATA.EnsureMobileIfDentureTreatment(clsPatient_ToothTrt.PatientID, clsPatient_ToothTrt.Treat)
                Return True
            Else
                DiagID = 0 ' Default to 0 if no ID was returned
                Return False
            End If
        End Using
    End Function

    'QrtTable, QrtrID, CellAddress dbo.Patient_Diagnosis.IsPaid
    'From Patient_ToothTrtDATA
    Public Function Add(ByVal clsPatient_ToothTrt As Patient_Diagnosis) As Boolean
        Dim query As String = "
    MERGE INTO [dbo].[Patient_Diagnosis] AS Target
    USING (VALUES
        (@PatientID, @ShapeID, @ToothNum, @ToothName, @LVL, @PropertyName, @FillColor, @BorderThickness, @BorderColor,
         @TreatmentType, @TreatDate, @Treat, @TreatPlan, @TreatDetails, @TreatNotes, @Finished, @TreatEndDate,
         @TrtLoc, @QrtrID, @QrtrTable, @QrtrColumnName, @QrtrColumnValue, @QrtrAddress, @IsExternal,
         @ExternalClinicName, @ExternalTreatmentDate, @IsPaid, @IsMultiTrt, @ParentDiagID, @TrtGroupID, @isOnImplant, @UserID)
    ) AS Source (PatientID, ShapeID, ToothNum, ToothName, LVL, PropertyName, FillColor, BorderThickness, BorderColor,
                TreatmentType, TreatDate, Treat, TreatPlan, TreatDetails, TreatNotes, Finished, TreatEndDate,
                TrtLoc, QrtrID, QrtrTable, QrtrColumnName, QrtrColumnValue, QrtrAddress, IsExternal,
                ExternalClinicName, ExternalTreatmentDate, IsPaid, IsMultiTrt, ParentDiagID, TrtGroupID, isOnImplant, UserID)
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
            TrtLoc = Source.TrtLoc,
            QrtrID = Source.QrtrID,
            QrtrTable = Source.QrtrTable,
            QrtrColumnName = Source.QrtrColumnName,
            QrtrColumnValue = Source.QrtrColumnValue,
            QrtrAddress = Source.QrtrAddress,
            IsExternal = Source.IsExternal,
            ExternalClinicName = Source.ExternalClinicName,
            ExternalTreatmentDate = Source.ExternalTreatmentDate,
            IsPaid = Source.IsPaid,
            IsMultiTrt = Source.IsMultiTrt,
            ParentDiagID = Source.ParentDiagID,
            TrtGroupID = Source.TrtGroupID,
            isOnImplant = Source.isOnImplant,
            UserID = Source.UserID
    WHEN NOT MATCHED THEN
        INSERT ([PatientID],[ShapeID],[ToothNum],[ToothName],[LVL],[PropertyName],
                [FillColor],[BorderThickness],[BorderColor],[TreatmentType],[TreatDate],[Treat],
                [TreatPlan],[TreatDetails],[TreatNotes],[Finished],[TreatEndDate],
                [TrtLoc],[QrtrID],[QrtrTable],[QrtrColumnName],[QrtrColumnValue],[QrtrAddress],
                [IsExternal],[ExternalClinicName],[ExternalTreatmentDate],[IsPaid],
                [IsMultiTrt],[ParentDiagID],[TrtGroupID],[isOnImplant],[UserID])
        VALUES (Source.PatientID, Source.ShapeID, Source.ToothNum, Source.ToothName, Source.LVL, Source.PropertyName,
                Source.FillColor, Source.BorderThickness, Source.BorderColor, Source.TreatmentType, Source.TreatDate,
                Source.Treat, Source.TreatPlan, Source.TreatDetails, Source.TreatNotes, Source.Finished,
                Source.TreatEndDate, Source.TrtLoc, Source.QrtrID, Source.QrtrTable, Source.QrtrColumnName,
                Source.QrtrColumnValue, Source.QrtrAddress, Source.IsExternal,
                Source.ExternalClinicName, Source.ExternalTreatmentDate, Source.IsPaid,
                Source.IsMultiTrt, Source.ParentDiagID, Source.TrtGroupID, Source.isOnImplant, Source.UserID);
    "

        Using conn As New SqlConnection(ConnectionString)
            Dim rowsAffected As Integer = conn.Execute(query, clsPatient_ToothTrt)
            If rowsAffected > 0 Then
                AppLogger.Log(CurrentUser.UsName, clsPatient_ToothTrt.PatientID.ToString(), "Patient_Diagnosis", "Insert/Update")
                PatientDATA.EnsureMobileIfDentureTreatment(clsPatient_ToothTrt.PatientID, clsPatient_ToothTrt.Treat)
                Return True
            Else
                Return False
            End If
        End Using
    End Function

    Public Function AddNormal(ByVal clsPatient_ToothTrt As Patient_Diagnosis) As Boolean
        Dim RowsAffected As Integer = 0
        Using conn As New SqlConnection(ConnectionString)
            Dim sql As String = "INSERT INTO Patient_Diagnosis (PatientID, ShapeID, ToothNum, ToothName, LVL,
                                    PropertyName, FillColor, BorderThickness, BorderColor, TreatmentType, TreatDate,
                                    Treat, TreatPlan, TreatDetails, TreatNotes, Finished, TreatEndDate, QrtrTable,
                                    TrtLoc, QrtrID, QrtrAddress, QrtrColumnName, QrtrColumnValue, IsExternal,
                                    ExternalClinicName, ExternalTreatmentDate, IsPaid, IsMultiTrt, ParentDiagID,
                                    TrtGroupID, isOnImplant, UserID) 
                                VALUES 
                                    (@PatientID, @ShapeID, @ToothNum, @ToothName, @LVL, @PropertyName, @FillColor,
                                     @BorderThickness, @BorderColor, @TreatmentType, @TreatDate, @Treat, @TreatPlan,
                                     @TreatDetails, @TreatNotes, @Finished, @TreatEndDate, @QrtrTable, @TrtLoc,
                                     @QrtrID, @QrtrAddress, @QrtrColumnName, @QrtrColumnValue, @IsExternal, 
                                     @ExternalClinicName, @ExternalTreatmentDate, @IsPaid, @IsMultiTrt, 
                                     @ParentDiagID, @TrtGroupID, @isOnImplant, @UserID)"
            RowsAffected = conn.Execute(sql, New With {.PatientID = clsPatient_ToothTrt.PatientID,
                                        .ShapeID = clsPatient_ToothTrt.ShapeID,
                                        .ToothNum = clsPatient_ToothTrt.ToothNum,
                                        .ToothName = clsPatient_ToothTrt.ToothName,
                                        .LVL = clsPatient_ToothTrt.LVL,
                                        .PropertyName = clsPatient_ToothTrt.PropertyName,
                                        .FillColor = clsPatient_ToothTrt.FillColor,
                                        .BorderThickness = clsPatient_ToothTrt.BorderThickness,
                                        .BorderColor = clsPatient_ToothTrt.BorderColor,
                                        .TreatmentType = clsPatient_ToothTrt.TreatmentType,
                                        .TreatDate = clsPatient_ToothTrt.TreatDate,
                                        .Treat = clsPatient_ToothTrt.Treat,
                                        .TreatPlan = clsPatient_ToothTrt.TreatPlan,
                                        .TreatDetails = clsPatient_ToothTrt.TreatDetails,
                                        .TreatNotes = clsPatient_ToothTrt.TreatNotes,
                                        .Finished = clsPatient_ToothTrt.Finished,
                                        .TreatEndDate = clsPatient_ToothTrt.TreatEndDate,
                                        .QrtrTable = clsPatient_ToothTrt.QrtrTable,
                                        .TrtLoc = clsPatient_ToothTrt.TrtLoc,
                                        .QrtrID = clsPatient_ToothTrt.QrtrID,
                                        .QrtrAddress = clsPatient_ToothTrt.QrtrAddress,
                                        .QrtrColumnName = clsPatient_ToothTrt.QrtrColumnName,
                                        .QrtrColumnValue = clsPatient_ToothTrt.QrtrColumnValue,
                                        .IsExternal = clsPatient_ToothTrt.IsExternal,
                                        .ExternalClinicName = clsPatient_ToothTrt.ExternalClinicName,
                                        .ExternalTreatmentDate = clsPatient_ToothTrt.ExternalTreatmentDate,
                                        .IsPaid = clsPatient_ToothTrt.IsPaid,
                                        .IsMultiTrt = clsPatient_ToothTrt.IsMultiTrt,
                                        .ParentDiagID = clsPatient_ToothTrt.ParentDiagID,
                                        .TrtGroupID = clsPatient_ToothTrt.TrtGroupID,
                                        .isOnImplant = clsPatient_ToothTrt.isOnImplant,
                                        .UserID = clsPatient_ToothTrt.UserID})
            If RowsAffected > 0 Then
                PatientDATA.EnsureMobileIfDentureTreatment(clsPatient_ToothTrt.PatientID, clsPatient_ToothTrt.Treat)
            End If
            Return RowsAffected > 0
        End Using


    End Function

    Public Function AddTransactional1(conn As SqlConnection, trans As SqlTransaction, clsPatient_ToothTrt As Patient_Diagnosis) As Boolean

        Dim query As String = ""
        query = "   MERGE INTO [dbo].[Patient_Diagnosis] AS Target
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
        @TrtLoc AS TrtLoc,
        @QrtrID AS QrtrID,
        @QrtrTable AS QrtrTable,
        @QrtrColumnName AS QrtrColumnName,
        @QrtrColumnValue AS QrtrColumnValue,
        @QrtrAddress AS QrtrAddress,
        @IsExternal AS IsExternal,
        @ExternalClinicName AS ExternalClinicName,
        @ExternalTreatmentDate AS ExternalTreatmentDate,
        @IsPaid AS IsPaid,
        @IsMultiTrt AS IsMultiTrt,
        @ParentDiagID AS ParentDiagID,
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
        TrtLoc = Source.TrtLoc,
        QrtrID = Source.QrtrID,
        QrtrTable = Source.QrtrTable, 
        QrtrColumnName = Source.QrtrColumnName, 
        QrtrColumnValue = Source.QrtrColumnValue,
        QrtrAddress = Source.QrtrAddress,
        IsExternal = Source.IsExternal,
        ExternalClinicName = Source.ExternalClinicName,
        ExternalTreatmentDate = Source.ExternalTreatmentDate,
        IsPaid = Source.IsPaid,
        IsMultiTrt = Source.IsMultiTrt,
        ParentDiagID = Source.ParentDiagID,
        TrtGroupID = Source.TrtGroupID
WHEN NOT MATCHED THEN
    INSERT (
        PatientID, ShapeID, ToothNum, ToothName, LVL, PropertyName, FillColor,
        BorderThickness, BorderColor, TreatmentType, TreatDate, Treat, TreatPlan,
        TreatDetails, TreatNotes, Finished, TreatEndDate, TrtLoc, QrtrID, QrtrTable, QrtrColumnName, QrtrColumnValue,
        QrtrAddress, IsExternal, ExternalClinicName, ExternalTreatmentDate,  IsPaid,
        IsMultiTrt, ParentDiagID, TrtGroupID
    )
    VALUES (
        Source.PatientID, Source.ShapeID, Source.ToothNum, Source.ToothName, Source.LVL, Source.PropertyName,
        Source.FillColor, Source.BorderThickness, Source.BorderColor, Source.TreatmentType, Source.TreatDate,
        Source.Treat, Source.TreatPlan, Source.TreatDetails, Source.TreatNotes, Source.Finished, Source.TreatEndDate,
        Source.TrtLoc, Source.QrtrID, Source.QrtrTable, Source.QrtrColumnName, Source.QrtrColumnValue, Source.QrtrAddress, Source.IsExternal, Source.ExternalClinicName,
        Source.ExternalTreatmentDate,  Source.IsPaid, Source.IsMultiTrt, Source.ParentDiagID, Source.TrtGroupID
    );
"
        '       INSERT INTO [dbo].[Patient_Diagnosis] ([PatientID], [ShapeID], [ToothNum], [ToothName], [LVL], [PropertyName], [FillColor], [BorderThickness], [BorderColor], [TreatmentType], [TreatDate], [Treat], [TreatPlan], [TreatDetails], [TreatNotes], [Finished], [TreatEndDate], [TrtLoc], [QrtrID], [QrtrAddress], [IsExternal], [ExternalClinicName], [ExternalTreatmentDate], [IsPaid], [IsMultiTrt], [ParentDiagID], [TrtGroupID], [isOnImplant])
        'Select Case@PatientID, @ShapeID, @ToothNum, @ToothName, @LVL, @PropertyName, @FillColor, @BorderThickness, @BorderColor, @TreatmentType, @TreatDate, @Treat, @TreatPlan, @TreatDetails, @TreatNotes, @Finished, @TreatEndDate, @TrtLoc, @QrtrID, @QrtrAddress, @IsExternal, @ExternalClinicName, @ExternalTreatmentDate, @IsPaid, @IsMultiTrt, @ParentDiagID, @TrtGroupID, @isOnImplant

        Dim rowsAffected As Integer = conn.Execute(query, clsPatient_ToothTrt, trans)
        If rowsAffected > 0 Then
            PatientDATA.EnsureMobileIfDentureTreatment(clsPatient_ToothTrt.PatientID, clsPatient_ToothTrt.Treat, trans)
        End If
        Return rowsAffected > 0
    End Function

    Public Function AddTransactional(conn As SqlConnection, trans As SqlTransaction, clsPatient_ToothTrt As Patient_Diagnosis) As Boolean
        Dim query As String =
        "INSERT INTO [dbo].[Patient_Diagnosis]
            ([PatientID], [ShapeID], [ToothNum], [ToothName], [LVL], [PropertyName], [FillColor], [BorderThickness], [BorderColor], [TreatmentType],
             [TreatDate], [Treat], [TreatPlan], [TreatDetails], [TreatNotes], [Finished], [TreatEndDate], [TrtLoc], [QrtrID],
             [QrtrTable], [QrtrColumnName], [QrtrColumnValue], [QrtrAddress], [IsExternal], [ExternalClinicName],
             [ExternalTreatmentDate], [IsPaid], [IsMultiTrt], [ParentDiagID], [TrtGroupID], [isOnImplant],[UserID])
        VALUES 
            (@PatientID, @ShapeID, @ToothNum, @ToothName, @LVL, @PropertyName, @FillColor, @BorderThickness, @BorderColor, @TreatmentType,
             @TreatDate, @Treat, @TreatPlan, @TreatDetails, @TreatNotes, @Finished, @TreatEndDate, @TrtLoc, @QrtrID, @QrtrTable,
             @QrtrColumnName, @QrtrColumnValue, @QrtrAddress, @IsExternal, @ExternalClinicName, @ExternalTreatmentDate,
             @IsPaid, @IsMultiTrt, @ParentDiagID, @TrtGroupID, @isOnImplant, @UserID);"

        ' Ensure DBNull for nullable fields
        Dim parameters = New With {
        .PatientID = clsPatient_ToothTrt.PatientID,
        .ShapeID = clsPatient_ToothTrt.ShapeID,
        .ToothNum = clsPatient_ToothTrt.ToothNum,
        .ToothName = clsPatient_ToothTrt.ToothName,
        .LVL = clsPatient_ToothTrt.LVL,
        .PropertyName = clsPatient_ToothTrt.PropertyName,
        .FillColor = clsPatient_ToothTrt.FillColor,
        .BorderThickness = clsPatient_ToothTrt.BorderThickness,
        .BorderColor = clsPatient_ToothTrt.BorderColor,
        .TreatmentType = clsPatient_ToothTrt.TreatmentType,
        .TreatDate = clsPatient_ToothTrt.TreatDate,
        .Treat = If(clsPatient_ToothTrt.Treat, DBNull.Value),
        .TreatPlan = If(clsPatient_ToothTrt.TreatPlan, DBNull.Value),
        .TreatDetails = If(clsPatient_ToothTrt.TreatDetails, DBNull.Value),
        .TreatNotes = If(clsPatient_ToothTrt.TreatNotes, DBNull.Value),
        .Finished = clsPatient_ToothTrt.Finished,
        .TreatEndDate = If(clsPatient_ToothTrt.TreatEndDate, DBNull.Value),
        .TrtLoc = clsPatient_ToothTrt.TrtLoc,
        .QrtrID = clsPatient_ToothTrt.QrtrID,
        .QrtrTable = clsPatient_ToothTrt.QrtrTable,
        .QrtrColumnName = clsPatient_ToothTrt.QrtrColumnName,
        .QrtrColumnValue = clsPatient_ToothTrt.QrtrColumnValue,
        .QrtrAddress = clsPatient_ToothTrt.QrtrAddress,
        .IsExternal = clsPatient_ToothTrt.IsExternal,
        .ExternalClinicName = If(clsPatient_ToothTrt.ExternalClinicName, DBNull.Value),
        .ExternalTreatmentDate = If(clsPatient_ToothTrt.ExternalTreatmentDate, DBNull.Value),
        .IsPaid = clsPatient_ToothTrt.IsPaid,
        .IsMultiTrt = clsPatient_ToothTrt.IsMultiTrt,
        .ParentDiagID = If(clsPatient_ToothTrt.ParentDiagID, DBNull.Value),
        .TrtGroupID = clsPatient_ToothTrt.TrtGroupID,
        .isOnImplant = clsPatient_ToothTrt.isOnImplant,
        .UserID = clsPatient_ToothTrt.UserID
    }

        Dim rowsAffected As Integer = conn.Execute(query, parameters, trans)
        If rowsAffected > 0 Then
            PatientDATA.EnsureMobileIfDentureTreatment(clsPatient_ToothTrt.PatientID, clsPatient_ToothTrt.Treat, trans)
        End If
        Return rowsAffected > 0
    End Function

    Public Function UpdateTransactional(conn As SqlConnection, trans As SqlTransaction, ByVal oldPatient_ToothTrt As Patient_Diagnosis, ByVal newPatient_ToothTrt As Patient_Diagnosis) As Boolean
        Dim query As String = "
        UPDATE [dbo].[Patient_Diagnosis]
        SET 
            PatientID = @NewPatientID,
            ShapeID = @NewShapeID,
            ToothNum = @NewToothNum,
            ToothName = @NewToothName,
            LVL = @NewLVL,
            PropertyName = @NewPropertyName,
            FillColor = @NewFillColor,
            BorderThickness = @NewBorderThickness,
            BorderColor = @NewBorderColor,
            TreatmentType = @NewTreatmentType,
            TreatDate = @NewTreatDate,
            Treat = @NewTreat,
            TreatPlan = @NewTreatPlan,
            TreatDetails = @NewTreatDetails,
            TreatNotes = @NewTreatNotes,
            Finished = @NewFinished,
            TreatEndDate = @NewTreatEndDate,
            TrtLoc = @NewTrtLoc,
            QrtrID = @NewQrtrID,
            QrtrTable = @NewQrtrTable, 
            QrtrColumnName = @NewQrtrColumnName, 
            QrtrColumnValue = @NewQrtrColumnValue,
            QrtrAddress = @NewQrtrAddress,
            IsExternal = @NewIsExternal,
            ExternalClinicName = @NewExternalClinicName,
            ExternalTreatmentDate = @NewExternalTreatmentDate,
            IsPaid = @NewIsPaid,
            IsMultiTrt = @NewIsMultiTrt,
            ParentDiagID = @NewParentToothTrtID,
            TrtGroupID = @NewTrtGroupID,
            isOnImplant = @NewIsOnImplant,
            UserID=@NewUserID
        WHERE
            DiagID = @OldToothTrtID AND
            PatientID = @OldPatientID AND
            ToothNum = @OldToothNum AND
            ToothName = @OldToothName;
    "
        '' New values AND
        'PropertyName = @OldPropertyName
        Dim parameters = New With {
        .NewPatientID = newPatient_ToothTrt.PatientID,
        .NewShapeID = newPatient_ToothTrt.ShapeID,
        .NewToothNum = newPatient_ToothTrt.ToothNum,
        .NewToothName = newPatient_ToothTrt.ToothName,
        .NewLVL = newPatient_ToothTrt.LVL,
        .NewPropertyName = newPatient_ToothTrt.PropertyName,
        .NewFillColor = newPatient_ToothTrt.FillColor,
        .NewBorderThickness = newPatient_ToothTrt.BorderThickness,
        .NewBorderColor = newPatient_ToothTrt.BorderColor,
        .NewTreatmentType = newPatient_ToothTrt.TreatmentType,
        .NewTreatDate = newPatient_ToothTrt.TreatDate,
        .NewTreat = newPatient_ToothTrt.Treat,
        .NewTreatPlan = newPatient_ToothTrt.TreatPlan,
        .NewTreatDetails = newPatient_ToothTrt.TreatDetails,
        .NewTreatNotes = newPatient_ToothTrt.TreatNotes,
        .NewFinished = newPatient_ToothTrt.Finished,
        .NewTreatEndDate = newPatient_ToothTrt.TreatEndDate,
        .NewTrtLoc = newPatient_ToothTrt.TrtLoc,
        .NewQrtrID = newPatient_ToothTrt.QrtrID,
        .NewQrtrTable = newPatient_ToothTrt.QrtrTable,
        .NewQrtrColumnName = newPatient_ToothTrt.QrtrColumnName,
        .NewQrtrColumnValue = newPatient_ToothTrt.QrtrColumnValue,
        .NewQrtrAddress = newPatient_ToothTrt.QrtrAddress,
        .NewIsExternal = newPatient_ToothTrt.IsExternal,
        .NewExternalClinicName = newPatient_ToothTrt.ExternalClinicName,
        .NewExternalTreatmentDate = newPatient_ToothTrt.ExternalTreatmentDate,
        .NewIsPaid = newPatient_ToothTrt.IsPaid,
        .NewIsMultiTrt = newPatient_ToothTrt.IsMultiTrt,
        .NewParentToothTrtID = newPatient_ToothTrt.ParentDiagID,
        .NewTrtGroupID = newPatient_ToothTrt.TrtGroupID,
        .NewIsOnImplant = newPatient_ToothTrt.isOnImplant,        ' Old values for WHERE clause
        .NewUserID = newPatient_ToothTrt.UserID,
        .OldToothTrtID = oldPatient_ToothTrt.DiagID,
        .OldPatientID = oldPatient_ToothTrt.PatientID,
        .OldToothNum = oldPatient_ToothTrt.ToothNum,
        .OldToothName = oldPatient_ToothTrt.ToothName,
        .OldPropertyName = oldPatient_ToothTrt.PropertyName
    }

        Dim rowsAffected As Integer = conn.Execute(query, parameters, trans)
        If rowsAffected > 0 Then
            PatientDATA.EnsureMobileIfDentureTreatment(newPatient_ToothTrt.PatientID, newPatient_ToothTrt.Treat, trans)
        End If
        Return rowsAffected > 0
    End Function


    Public Function Update(ByVal oldPatient_ToothTrt As Patient_Diagnosis, ByVal newPatient_ToothTrt As Patient_Diagnosis) As Boolean
        Dim query As String = "
    UPDATE [dbo].[Patient_Diagnosis]
    SET 
        PatientID = @NewPatientID,
        ShapeID = @NewShapeID,
        ToothNum = @NewToothNum,
        ToothName = @NewToothName,
        LVL = @NewLVL,
        PropertyName = @NewPropertyName,
        FillColor = @NewFillColor,
        BorderThickness = @NewBorderThickness,
        BorderColor = @NewBorderColor,
        TreatmentType = @NewTreatmentType,
        TreatDate = @NewTreatDate,
        Treat = @NewTreat,
        TreatPlan = @NewTreatPlan,
        TreatDetails = @NewTreatDetails,
        TreatNotes = @NewTreatNotes,
        Finished = @NewFinished,
        TreatEndDate = @NewTreatEndDate,
        TrtLoc = @NewTrtLoc,
        QrtrID = @NewQrtrID,
        QrtrTable = @NewQrtrTable, 
        QrtrColumnName = @NewQrtrColumnName, 
        QrtrColumnValue = @NewQrtrColumnValue,
        QrtrAddress = @NewQrtrAddress,
        IsExternal = @NewIsExternal,
        ExternalClinicName = @NewExternalClinicName,
        ExternalTreatmentDate = @NewExternalTreatmentDate,
        IsPaid = @NewIsPaid,
        IsMultiTrt = @NewIsMultiTrt,
        ParentDiagID = @NewParentToothTrtID,
        TrtGroupID = @NewTrtGroupID,
        isOnImplant = @NewIsOnImplant,
        UserID=@NewUserID

    WHERE
        DiagID = @OldToothTrtID AND
        PatientID = @OldPatientID AND
        ToothNum = @OldToothNum AND
        ToothName = @OldToothName ;
    "
        'AND
        'PropertyName = @OldPropertyName
        Dim parameters = New With {
        .NewPatientID = newPatient_ToothTrt.PatientID,
        .NewShapeID = newPatient_ToothTrt.ShapeID,
        .NewToothNum = newPatient_ToothTrt.ToothNum,
        .NewToothName = newPatient_ToothTrt.ToothName,
        .NewLVL = newPatient_ToothTrt.LVL,
        .NewPropertyName = newPatient_ToothTrt.PropertyName,
        .NewFillColor = newPatient_ToothTrt.FillColor,
        .NewBorderThickness = newPatient_ToothTrt.BorderThickness,
        .NewBorderColor = newPatient_ToothTrt.BorderColor,
        .NewTreatmentType = newPatient_ToothTrt.TreatmentType,
        .NewTreatDate = newPatient_ToothTrt.TreatDate,
        .NewTreat = newPatient_ToothTrt.Treat,
        .NewTreatPlan = newPatient_ToothTrt.TreatPlan,
        .NewTreatDetails = newPatient_ToothTrt.TreatDetails,
        .NewTreatNotes = newPatient_ToothTrt.TreatNotes,
        .NewFinished = newPatient_ToothTrt.Finished,
        .NewTreatEndDate = newPatient_ToothTrt.TreatEndDate,
        .NewTrtLoc = newPatient_ToothTrt.TrtLoc,
        .NewQrtrID = newPatient_ToothTrt.QrtrID,
        .NewQrtrTable = newPatient_ToothTrt.QrtrTable,
        .NewQrtrColumnName = newPatient_ToothTrt.QrtrColumnName,
        .NewQrtrColumnValue = newPatient_ToothTrt.QrtrColumnValue,
        .NewQrtrAddress = newPatient_ToothTrt.QrtrAddress,
        .NewIsExternal = newPatient_ToothTrt.IsExternal,
        .NewExternalClinicName = newPatient_ToothTrt.ExternalClinicName,
        .NewExternalTreatmentDate = newPatient_ToothTrt.ExternalTreatmentDate,
        .NewIsPaid = newPatient_ToothTrt.IsPaid,
        .NewIsMultiTrt = newPatient_ToothTrt.IsMultiTrt,
        .NewParentToothTrtID = newPatient_ToothTrt.ParentDiagID,
        .NewTrtGroupID = newPatient_ToothTrt.TrtGroupID,
        .NewIsOnImplant = newPatient_ToothTrt.isOnImplant,        ' Old values for WHERE clause
        .NewUserID = newPatient_ToothTrt.UserID,
        .OldToothTrtID = oldPatient_ToothTrt.DiagID,
        .OldPatientID = oldPatient_ToothTrt.PatientID,
        .OldToothNum = oldPatient_ToothTrt.ToothNum,
        .OldToothName = oldPatient_ToothTrt.ToothName,
        .OldPropertyName = oldPatient_ToothTrt.PropertyName
    }

        Using conn As New SqlConnection(ConnectionString)
            Dim rowsAffected As Integer = conn.Execute(query, parameters)
            If rowsAffected > 0 Then
                PatientDATA.EnsureMobileIfDentureTreatment(newPatient_ToothTrt.PatientID, newPatient_ToothTrt.Treat)
            End If
            Return rowsAffected > 0
        End Using
        AppLogger.Log(CurrentUser.UsName, PatientName, "Patient_Diagnosis", "UPDATE")
    End Function


    Public Function Delete(ByVal clsPatient_ToothTrt As Patient_Diagnosis) As Boolean
        Dim deleteStatement As String =
            "DELETE FROM [Patient_Diagnosis] 
			WHERE DiagID = @DiagID AND PatientID = @PatientID AND ToothNum = @ToothNum; "
        Using connection As SqlConnection = DentistXDATA.GetConnection()
            connection.Open()
            Dim affectedRows As Integer = connection.Execute(deleteStatement, New With {.DiagID = clsPatient_ToothTrt.DiagID, .PatientID = clsPatient_ToothTrt.PatientID, .ToothNum = clsPatient_ToothTrt.ToothNum})
            Return affectedRows > 0
        End Using
        'AppLogger.Log(CurrentUser.UsName, PatientName, "Patient_Diagnosis", "DELETE")
    End Function

    Public Function DeleteTransOld(ByVal clsPatient_ToothTrt As Patient_Diagnosis) As Boolean
        Dim deleteStatement As String = "
        DELETE FROM [Patient_Trts] 
        WHERE DiagID = @DiagID AND PatientID = @PatientID;
        
        DELETE FROM [Patient_Diagnosis] 
        WHERE DiagID = @DiagID AND PatientID = @PatientID AND ToothNum = @ToothNum;"

        Using connection As SqlConnection = DentistXDATA.GetConnection()
            connection.Open()
            Using transaction As SqlTransaction = connection.BeginTransaction()
                Try
                    ' Execute both statements in one call
                    Dim affectedRows As Integer = connection.Execute(deleteStatement,
                    New With {
                        .DiagID = clsPatient_ToothTrt.DiagID,
                        .PatientID = clsPatient_ToothTrt.PatientID,
                        .ToothNum = clsPatient_ToothTrt.ToothNum
                    },
                    transaction)

                    transaction.Commit()

                    ' Log only after successful commit
                    AppLogger.Log(CurrentUser.UsName, PatientName, "Patient_Diagnosis", "DELETE")
                    Return affectedRows > 0

                Catch ex As Exception
                    transaction.Rollback()
                    ' Log the error appropriately
                    AppLogger.Log(CurrentUser.UsName, PatientName, "Delete Tooth Treatment Failed", ex.ToString())
                    Return False
                End Try
            End Using
        End Using
    End Function

    Public Function DeleteTrans(ByVal clsPatient_ToothTrt As Patient_Diagnosis) As Boolean
        ' STEP 1: Resolve to parent if this is a child bridge record
        Dim resolvedTrt = clsPatient_ToothTrt
        If clsPatient_ToothTrt.ParentDiagID.HasValue Then
            resolvedTrt = SelectByID(clsPatient_ToothTrt) '.ParentDiagID.Value)
            If resolvedTrt Is Nothing Then
                MsgBox("Parent Bridge Or Denture record not found for deletion.")
                Return False
            End If
        End If

        If resolvedTrt.PatientID <= 0 Then
            MsgBox("Missing PatientID.")
            Return False
        End If
        If resolvedTrt.DiagID <= 0 Then
            MsgBox("Missing or invalid DiagID.")
            Return False
        End If
        ' STEP 3: Execute deletion with full bridge-safe logic
        Dim deleteStatement As String = "
        BEGIN TRY
            BEGIN TRANSACTION;

            DECLARE @DeletedFromTrts INT = 0;
            DECLARE @DeletedFromDiagInfo INT = 0;
            DECLARE @DeletedFromDiagnosis INT = 0;
            DECLARE @AllDeleted INT = 0;

            -- 1. Delete from Patient_Trts
            DELETE FROM [Patient_Trts]
            WHERE DiagID = @DiagID AND PatientID = @PatientID;
            SET @DeletedFromTrts = @@ROWCOUNT;

            -- 2. Delete from Patient_DiagInfo
            DELETE FROM [Patient_DiagInfo]
            WHERE (ParentDiagID = @ParentDiagID OR ParentDiagID IS NULL)
                   AND PatientID = @PatientID
                   AND (TrtGroupID = @TrtGroupID OR TrtGroupID IS NULL);
            SET @DeletedFromDiagInfo = @@ROWCOUNT;

            -- 3. If bridge children were deleted, delete child teeth
            IF @DeletedFromDiagInfo > 0
            BEGIN
                DELETE FROM [Patient_Diagnosis]
                 WHERE (ParentDiagID = @ParentDiagID OR ParentDiagID IS NULL)
                        AND PatientID = @PatientID
                        AND (TrtGroupID = @TrtGroupID OR TrtGroupID IS NULL);
                SET @DeletedFromDiagnosis = @@ROWCOUNT;
            END
            ELSE
            BEGIN
                -- Otherwise delete the main tooth record
                DELETE FROM [Patient_Diagnosis]
                WHERE DiagID = @DiagID AND PatientID = @PatientID;
                SET @DeletedFromDiagnosis = @@ROWCOUNT;
            END

            SET @AllDeleted = @DeletedFromTrts + @DeletedFromDiagInfo + @DeletedFromDiagnosis;
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
                .DiagID = resolvedTrt.DiagID,
                .PatientID = resolvedTrt.PatientID,
                .ParentDiagID = resolvedTrt.ParentDiagID,
                .TrtGroupID = resolvedTrt.TrtGroupID
            })

            Return result > 0
        End Using
    End Function

    Public Function DelPatientTreats(ByVal clsPatient As Patient, toothNumList As List(Of Byte)) As Boolean
        ' Convert byte list to comma-separated string
        Dim toothNumString As String = String.Join(",", toothNumList)

        ' Create parameterized query (safe from SQL injection)
        Dim query = "DELETE FROM [Patient_Diagnosis] 
                WHERE PatientID = @PatientID 
                AND ToothNum IN (SELECT value FROM STRING_SPLIT(@ToothNumList, ',')) "


        Using connection As New SqlConnection(ConnectionString)
            Try
                ' Execute query and return true if any matching record exists
                Dim result = connection.ExecuteScalar(Of Integer)(
                query,
                New With {
                    .PatientID = clsPatient.PatientID,
                    .ToothNumList = toothNumString
                })

                Return result > 0
            Catch ex As Exception
                MsgBox(ex.Message)
                'AppLogger.Log(CurrentUser.UsName, PatientName, "Patient_Diagnosis", $"SELECT Error: {ex.Message}")
                Return False
            End Try
        End Using

        AppLogger.Log(CurrentUser.UsName, PatientName, "Patient_Diagnosis", "SELECT")
    End Function

    Public Function DeletePatientTreats(clsPatient As Patient, toothNumList As List(Of Byte)) As Boolean
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

        DECLARE @DeletedFromDiagnosis INT = 0;
        DECLARE @DeletedFromDiagInfo INT = 0;

        -- First get all DiagIDs that will be affected
        DECLARE @DiagIDs TABLE (ID INT);
        
        INSERT INTO @DiagIDs
        SELECT DiagID FROM [Patient_Diagnosis]
        WHERE PatientID = @PatientID 
        AND ToothNum IN (SELECT value FROM STRING_SPLIT(@ToothNumList, ','));
        
     

        -- 2. Delete from Patient_DiagInfo (including any bridge/denture children)
        DELETE FROM [Patient_DiagInfo]
        WHERE (ParentDiagID IN (SELECT ID FROM @DiagIDs) OR ToothNum IN
         (SELECT value FROM STRING_SPLIT(@ToothNumList, ',')) AND PatientID = @PatientID) ;
        SET @DeletedFromDiagInfo = @@ROWCOUNT;

        -- 3. Finally delete from Patient_Diagnosis
        DELETE FROM [Patient_Diagnosis]
        WHERE PatientID = @PatientID 
        AND ToothNum IN (SELECT value FROM STRING_SPLIT(@ToothNumList, ','));
        SET @DeletedFromDiagnosis = @@ROWCOUNT;

        COMMIT TRANSACTION;
        
        SELECT CASE 
            WHEN (@DeletedFromDiagnosis + @DeletedFromDiagInfo) > 0 THEN 1 
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
                    'AppLogger.Log(CurrentUser.UsName, clsPatient.PatientName, "Patient_Diagnosis",
                    '        $"DELETE for teeth: {toothNumString}")
                End If

                Return result > 0
            Catch ex As Exception
                'AppLogger.Log(CurrentUser.UsName, clsPatient.PatientName, "Patient_Diagnosis",
                '        $"DELETE Error: {ex.Message}")
                MsgBox($"Error deleting Diagnosis: {ex.Message}")
                Return False
            End Try
        End Using
    End Function

    Public Function GetPatientTreats(patientID As Integer, toothNumList As List(Of Byte)) As IEnumerable(Of Patient_Diagnosis)
        ' Convert byte list to comma-separated string
        Dim toothNumString As String = String.Join(",", toothNumList)

        ' Create parameterized query (safe from SQL injection)
        Dim query = "SELECT * FROM [Patient_Diagnosis] 
                WHERE PatientID = @PatientID 
                AND ToothNum IN (SELECT value FROM STRING_SPLIT(@ToothNumList, ',')) "
        'Return Conn.Query(Of Patient_Diagnosis)(Sql, New With {.PatientId = patientID, .ToothNum = ToothNum})
        Using connection As New SqlConnection(ConnectionString)
            Try
                ' Execute query and return true if any matching record exists
                Return Conn.Query(Of Patient_Diagnosis)(
                query,
                New With {
                    .PatientID = patientID,
                    .ToothNumList = toothNumString
                })
            Catch ex As Exception
                AppLogger.Log(CurrentUser.UsName, PatientName, "Patient_Diagnosis", $"SELECT Error: {ex.Message}")
                Return Nothing
            End Try
        End Using

        AppLogger.Log(CurrentUser.UsName, PatientName, "Patient_Diagnosis", "SELECT")
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

    Public Function GetPatient_ToothTrtHistory(ByVal clsPatient_ToothTrt As Patient_Diagnosis) As IEnumerable(Of Patient_ToothTrtHistory)
        Using conn As New SqlConnection(ConnectionString)
            conn.Open()
            Dim query As String = "SELECT * FROM [Patient_ToothTrtHistory] WHERE [DiagID] = @DiagID AND  [PatientID] = @PatientID AND  [ToothNum] = @ToothNum"
            Return conn.Query(Of Patient_ToothTrtHistory)(query, New With {.DiagID = clsPatient_ToothTrt.DiagID, .PatientID = clsPatient_ToothTrt.PatientID, .ToothNum = clsPatient_ToothTrt.ToothNum})
        End Using
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
