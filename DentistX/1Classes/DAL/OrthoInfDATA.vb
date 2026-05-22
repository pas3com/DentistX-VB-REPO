Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections
Imports System.Configuration
Imports Dapper

Public Class OrthoInfDATA

    Private ConnectionString As String = DentistXDATA.GetConnection.ConnectionString



    Public Function SelectAll() As IEnumerable(Of OrthoInf)
        Using conn As New SqlConnection(ConnectionString)
            conn.Open()
            Return conn.Query(Of OrthoInf)("SELECT * FROM OrthoInf  ORDER BY [TreatDate] DESC")
        End Using
    End Function


    Public Function Select_Record(ByVal clsOrthoInf As OrthoInf) As OrthoInf
        Using conn As New SqlConnection(ConnectionString)
            Dim sql As String = "Select * FROM OrthoInf WHERE OrthoID = @OrthoID And PatientID = @PatientID  ORDER BY [TreatDate] DESC"
            Return conn.QuerySingleOrDefault(Of OrthoInf)(sql, New With {.OrthoID = clsOrthoInf.OrthoID, .PatientID = clsOrthoInf.PatientID})
        End Using
    End Function

    ''' <summary>Gets the single OrthoInf record for a patient (1:1 model). Returns Nothing if none.</summary>
    Public Function SelectByPatientID(patientID As Integer) As OrthoInf
        Using conn As New SqlConnection(ConnectionString)
            conn.Open()
            Dim sql As String = "SELECT TOP 1 * FROM OrthoInf WHERE PatientID = @PatientID ORDER BY TreatDate DESC"
            Return conn.QuerySingleOrDefault(Of OrthoInf)(sql, New With {.PatientID = patientID})
        End Using
    End Function

    ' In clsOrthoInfDATA - Add this method
    Public Function AddTransAndGetID(conn As SqlConnection, trans As SqlTransaction, ByVal clsOrthoInf As OrthoInf) As Integer
        Try
            Dim sql As String = "INSERT INTO OrthoInf (PatientID, Compliants, Birth, Feed, MilkTeethChng, MilkTeethAppear, TeethLoss, BurriedTeeth, OverLoadTeeth, LipsCut, ThroatCut, IllnesPeriod, CousinsHFactor, BadHabits, Malfunction, Khota, PrevOrth, PrevIll, TreatDate) 
                            OUTPUT INSERTED.OrthoID 
                            VALUES (@PatientID, @Compliants, @Birth, @Feed, @MilkTeethChng, @MilkTeethAppear, @TeethLoss, @BurriedTeeth, @OverLoadTeeth, @LipsCut, @ThroatCut, @IllnesPeriod, @CousinsHFactor, @BadHabits, @Malfunction, @Khota, @PrevOrth, @PrevIll, @TreatDate)"

            Dim result = conn.ExecuteScalar(sql, New With {
            .PatientID = clsOrthoInf.PatientID,
            .Compliants = clsOrthoInf.Compliants,
            .Birth = clsOrthoInf.Birth,
            .Feed = clsOrthoInf.Feed,
            .MilkTeethChng = clsOrthoInf.MilkTeethChng,
            .MilkTeethAppear = clsOrthoInf.MilkTeethAppear,
            .TeethLoss = clsOrthoInf.TeethLoss,
            .BurriedTeeth = clsOrthoInf.BurriedTeeth,
            .OverLoadTeeth = clsOrthoInf.OverLoadTeeth,
            .LipsCut = clsOrthoInf.LipsCut,
            .ThroatCut = clsOrthoInf.ThroatCut,
            .IllnesPeriod = clsOrthoInf.IllnesPeriod,
            .CousinsHFactor = clsOrthoInf.CousinsHFactor,
            .BadHabits = clsOrthoInf.BadHabits,
            .Malfunction = clsOrthoInf.Malfunction,
            .Khota = clsOrthoInf.Khota,
            .PrevOrth = clsOrthoInf.PrevOrth,
            .PrevIll = clsOrthoInf.PrevIll,
            .TreatDate = clsOrthoInf.TreatDate
        }, trans)

            Return If(result IsNot Nothing, Convert.ToInt32(result), -1)
        Catch ex As Exception
            Debug.WriteLine($"Error in AddTransAndGetID (OrthoInf): {ex.Message}")
            Return -1
        End Try
    End Function


    Public Function Add(ByVal clsOrthoInf As OrthoInf) As Boolean
        Dim RowsAffected As Integer = 0
        Using conn As New SqlConnection(ConnectionString)
            Dim sql As String = "INSERT INTO OrthoInf (PatientID, Compliants, Birth, Feed, MilkTeethChng,
                                    MilkTeethAppear, TeethLoss, BurriedTeeth, OverLoadTeeth, LipsCut,
                                    ThroatCut, IllnesPeriod, CousinsHFactor,BadHabits, Malfunction, Khota,
                                    PrevOrth, PrevIll, TreatDate) 
                                    VALUES 
                                    (@PatientID, @Compliants, @Birth, @Feed,@MilkTeethChng, @MilkTeethAppear,
                                    @TeethLoss, @BurriedTeeth, @OverLoadTeeth, @LipsCut, @ThroatCut,
                                    @IllnesPeriod,@CousinsHFactor, @BadHabits, @Malfunction, @Khota,
                                    @PrevOrth, @PrevIll, @TreatDate)"
            RowsAffected = conn.Execute(sql, New With {
                                        .PatientID = clsOrthoInf.PatientID, .Compliants = clsOrthoInf.Compliants,
                                        .Birth = clsOrthoInf.Birth, .Feed = clsOrthoInf.Feed,
                                        .MilkTeethChng = clsOrthoInf.MilkTeethChng,
                                        .MilkTeethAppear = clsOrthoInf.MilkTeethAppear,
                                        .TeethLoss = clsOrthoInf.TeethLoss,
                                        .BurriedTeeth = clsOrthoInf.BurriedTeeth,
                                        .OverLoadTeeth = clsOrthoInf.OverLoadTeeth,
                                        .LipsCut = clsOrthoInf.LipsCut, .ThroatCut = clsOrthoInf.ThroatCut,
                                        .IllnesPeriod = clsOrthoInf.IllnesPeriod,
                                        .CousinsHFactor = clsOrthoInf.CousinsHFactor,
                                        .BadHabits = clsOrthoInf.BadHabits,
                                        .Malfunction = clsOrthoInf.Malfunction,
                                        .Khota = clsOrthoInf.Khota, .PrevOrth = clsOrthoInf.PrevOrth,
                                        .PrevIll = clsOrthoInf.PrevIll,
                                        .TreatDate = clsOrthoInf.TreatDate})
            Return RowsAffected > 0
        End Using
    End Function
    Public Function AddTrans(conn As SqlConnection, trans As SqlTransaction, ByVal clsOrthoInf As OrthoInf) As Boolean
        Dim RowsAffected As Integer = 0
        'Using conn As New SqlConnection(ConnectionString)
        Dim sql As String = "INSERT INTO OrthoInf (PatientID, Compliants, Birth, Feed, MilkTeethChng,
                            MilkTeethAppear, TeethLoss, BurriedTeeth, OverLoadTeeth, LipsCut, ThroatCut,
                            IllnesPeriod, CousinsHFactor, BadHabits, Malfunction, Khota, PrevOrth, PrevIll, TreatDate)
                            VALUES (@PatientID, @Compliants, @Birth, @Feed, @MilkTeethChng, @MilkTeethAppear,
                            @TeethLoss, @BurriedTeeth, @OverLoadTeeth, @LipsCut, @ThroatCut, @IllnesPeriod,
                            @CousinsHFactor, @BadHabits, @Malfunction, @Khota, @PrevOrth, @PrevIll, @TreatDate)"
        RowsAffected = conn.Execute(sql, New With {.PatientID = clsOrthoInf.PatientID,
                                    .Compliants = clsOrthoInf.Compliants,
                                    .Birth = clsOrthoInf.Birth,
                                    .Feed = clsOrthoInf.Feed,
                                    .MilkTeethChng = clsOrthoInf.MilkTeethChng,
                                    .MilkTeethAppear = clsOrthoInf.MilkTeethAppear,
                                    .TeethLoss = clsOrthoInf.TeethLoss,
                                    .BurriedTeeth = clsOrthoInf.BurriedTeeth,
                                    .OverLoadTeeth = clsOrthoInf.OverLoadTeeth,
                                    .LipsCut = clsOrthoInf.LipsCut,
                                    .ThroatCut = clsOrthoInf.ThroatCut,
                                    .IllnesPeriod = clsOrthoInf.IllnesPeriod,
                                    .CousinsHFactor = clsOrthoInf.CousinsHFactor,
                                    .BadHabits = clsOrthoInf.BadHabits,
                                    .Malfunction = clsOrthoInf.Malfunction,
                                    .Khota = clsOrthoInf.Khota,
                                    .PrevOrth = clsOrthoInf.PrevOrth,
                                    .PrevIll = clsOrthoInf.PrevIll,
                                    .TreatDate = clsOrthoInf.TreatDate}, trans)
        Return RowsAffected > 0
        'End Using
    End Function
    Public Function Update(oldOrthoInf As OrthoInf, newOrthoInf As OrthoInf) As Boolean
        Using conn As New SqlConnection(ConnectionString)
            Dim parameters = New With {.NewPatientID = newOrthoInf.PatientID,
                .OldPatientID = oldOrthoInf.PatientID,
                .NewCompliants = newOrthoInf.Compliants,
                .OldCompliants = oldOrthoInf.Compliants,
                .NewBirth = newOrthoInf.Birth,
                .OldBirth = oldOrthoInf.Birth,
                .NewFeed = newOrthoInf.Feed,
                .OldFeed = oldOrthoInf.Feed,
                .NewMilkTeethChng = newOrthoInf.MilkTeethChng,
                .OldMilkTeethChng = oldOrthoInf.MilkTeethChng,
                .NewMilkTeethAppear = newOrthoInf.MilkTeethAppear,
                .OldMilkTeethAppear = oldOrthoInf.MilkTeethAppear,
                .NewTeethLoss = newOrthoInf.TeethLoss,
                .OldTeethLoss = oldOrthoInf.TeethLoss,
                .NewBurriedTeeth = newOrthoInf.BurriedTeeth,
                .OldBurriedTeeth = oldOrthoInf.BurriedTeeth,
                .NewOverLoadTeeth = newOrthoInf.OverLoadTeeth,
                .OldOverLoadTeeth = oldOrthoInf.OverLoadTeeth,
                .NewLipsCut = newOrthoInf.LipsCut,
                .OldLipsCut = oldOrthoInf.LipsCut,
                .NewThroatCut = newOrthoInf.ThroatCut,
                .OldThroatCut = oldOrthoInf.ThroatCut,
                .NewIllnesPeriod = newOrthoInf.IllnesPeriod,
                .OldIllnesPeriod = oldOrthoInf.IllnesPeriod,
                .NewCousinsHFactor = newOrthoInf.CousinsHFactor,
                .OldCousinsHFactor = oldOrthoInf.CousinsHFactor,
                .NewBadHabits = newOrthoInf.BadHabits,
                .OldBadHabits = oldOrthoInf.BadHabits,
                .NewMalfunction = newOrthoInf.Malfunction,
                .OldMalfunction = oldOrthoInf.Malfunction,
                .NewKhota = newOrthoInf.Khota,
                .OldKhota = oldOrthoInf.Khota,
                .NewPrevOrth = newOrthoInf.PrevOrth,
                .OldPrevOrth = oldOrthoInf.PrevOrth,
                .NewPrevIll = newOrthoInf.PrevIll,
                .OldPrevIll = oldOrthoInf.PrevIll,
                .NewTreatDate = newOrthoInf.TreatDate,
                .OldTreatDate = oldOrthoInf.TreatDate
                                          }
            Dim affectedRows As Integer = conn.Execute("UPDATE [OrthoInf] 
                                         SET [PatientID] = @NewPatientID, [Compliants] = @NewCompliants,
                                        [Birth] = @NewBirth, [Feed] = @NewFeed, 
                                        [MilkTeethChng] = @NewMilkTeethChng,
                                        [MilkTeethAppear] = @NewMilkTeethAppear,
                                        [TeethLoss] = @NewTeethLoss, [BurriedTeeth] = @NewBurriedTeeth,
                                        [OverLoadTeeth] = @NewOverLoadTeeth, [LipsCut] = @NewLipsCut,
                                        [ThroatCut] = @NewThroatCut, [IllnesPeriod] = @NewIllnesPeriod,
                                        [CousinsHFactor] = @NewCousinsHFactor, [BadHabits] = @NewBadHabits,
                                        [Malfunction] = @NewMalfunction, [Khota] = @NewKhota,
                                        [PrevOrth] = @NewPrevOrth, [PrevIll] = @NewPrevIll, [TreatDate] = @NewTreatDate
                            WHERE [PatientID] = @OldPatientID AND [Compliants] = @OldCompliants AND
                                        [Birth] = @OldBirth AND [Feed] = @OldFeed AND 
                                        [MilkTeethChng] = @OldMilkTeethChng AND
                                        [MilkTeethAppear] = @OldMilkTeethAppear AND
                                        [TeethLoss] = @OldTeethLoss AND [BurriedTeeth] = @OldBurriedTeeth AND
                                        [OverLoadTeeth] = @OldOverLoadTeeth AND [LipsCut] = @OldLipsCut AND
                                        [ThroatCut] = @OldThroatCut AND [IllnesPeriod] = @OldIllnesPeriod AND
                                        [CousinsHFactor] = @OldCousinsHFactor AND [BadHabits] = @OldBadHabits AND
                                        [Malfunction] = @OldMalfunction AND [Khota] = @OldKhota AND
                                        [PrevOrth] = @OldPrevOrth AND [PrevIll] = @OldPrevIll AND
                                        [TreatDate] = @OldTreatDate", parameters)
            Return affectedRows > 0
        End Using
    End Function

    ''' <summary>Update OrthoInf within a transaction. Uses OrthoID for WHERE.</summary>
    Public Function UpdateTrans(conn As SqlConnection, trans As SqlTransaction, oldOrthoInf As OrthoInf, newOrthoInf As OrthoInf) As Boolean
        Dim parameters = New With {
            .OrthoID = newOrthoInf.OrthoID, .PatientID = newOrthoInf.PatientID,
            .NewCompliants = newOrthoInf.Compliants, .NewBirth = newOrthoInf.Birth, .NewFeed = newOrthoInf.Feed,
            .NewMilkTeethChng = newOrthoInf.MilkTeethChng, .NewMilkTeethAppear = newOrthoInf.MilkTeethAppear,
            .NewTeethLoss = newOrthoInf.TeethLoss, .NewBurriedTeeth = newOrthoInf.BurriedTeeth,
            .NewOverLoadTeeth = newOrthoInf.OverLoadTeeth, .NewLipsCut = newOrthoInf.LipsCut,
            .NewThroatCut = newOrthoInf.ThroatCut, .NewIllnesPeriod = newOrthoInf.IllnesPeriod,
            .NewCousinsHFactor = newOrthoInf.CousinsHFactor, .NewBadHabits = newOrthoInf.BadHabits,
            .NewMalfunction = newOrthoInf.Malfunction, .NewKhota = newOrthoInf.Khota,
            .NewPrevOrth = newOrthoInf.PrevOrth, .NewPrevIll = newOrthoInf.PrevIll,
            .NewTreatDate = newOrthoInf.TreatDate
        }
        Dim affectedRows As Integer = conn.Execute("UPDATE [OrthoInf] SET [PatientID] = @PatientID, [Compliants] = @NewCompliants, [Birth] = @NewBirth, [Feed] = @NewFeed, [MilkTeethChng] = @NewMilkTeethChng, [MilkTeethAppear] = @NewMilkTeethAppear, [TeethLoss] = @NewTeethLoss, [BurriedTeeth] = @NewBurriedTeeth, [OverLoadTeeth] = @NewOverLoadTeeth, [LipsCut] = @NewLipsCut, [ThroatCut] = @NewThroatCut, [IllnesPeriod] = @NewIllnesPeriod, [CousinsHFactor] = @NewCousinsHFactor, [BadHabits] = @NewBadHabits, [Malfunction] = @NewMalfunction, [Khota] = @NewKhota, [PrevOrth] = @NewPrevOrth, [PrevIll] = @NewPrevIll, [TreatDate] = @NewTreatDate WHERE [OrthoID] = @OrthoID", parameters, trans)
        Return affectedRows > 0
    End Function

    Public Function Delete(ByVal clsOrthoInf As OrthoInf) As Boolean
        Dim deleteStatement As String =
        "DELETE FROM [OrthoInf] WHERE PatientID = @PatientID AND OrthoID=@OrthoID;
         DELETE FROM [OrthoTreat] WHERE PatientID = @PatientID AND OrthoID=@OrthoID;
         DELETE FROM [OrthoTrtDet] WHERE PatientID = @PatientID AND OrthoID=@OrthoID;
         DELETE FROM [OrthoDiag] WHERE PatientID = @PatientID AND OrthoID=@OrthoID;"

        Using connection As SqlConnection = DentistXDATA.GetConnection()
            connection.Open()
            Using transaction = connection.BeginTransaction()
                Try
                    Dim affectedRows As Integer = connection.Execute(
                    deleteStatement,
                    New With {.OrthoID = clsOrthoInf.OrthoID, .PatientID = clsOrthoInf.PatientID},
                    transaction
                )

                    transaction.Commit()
                    Return affectedRows > 0

                Catch ex As Exception
                    transaction.Rollback()
                    ' Optional: Log or rethrow the exception
                    Console.WriteLine("Delete failed: " & ex.Message)
                    Return False
                End Try
            End Using
        End Using
    End Function

    Public Function Delete1(ByVal clsOrthoInf As OrthoInf) As Boolean
        Dim deleteStatement As String =
            "DELETE FROM [OrthoInf] 
			WHERE  PatientID = @PatientID;
            DELETE FROM [OrthoTreat] 
			WHERE  PatientID = @PatientID;
            DELETE FROM [OrthoTrtDet] 
			WHERE  PatientID = @PatientID;
            DELETE FROM [OrthoDiag] 
			WHERE  PatientID = @PatientID;"
        Using connection As SqlConnection = DentistXDATA.GetConnection()
            connection.Open()
            Dim affectedRows As Integer = connection.Execute(deleteStatement, New With {.OrthoID = clsOrthoInf.OrthoID, .PatientID = clsOrthoInf.PatientID})
            Return affectedRows > 0
        End Using
    End Function


    ''' <summary>
    ''' When OrthoTreat / OrthoTrtDet are saved with OrthoID 0 or unset, map to an episode key from OrthoInf.
    ''' Uses the latest episode by TreatDate (then OrthoID) so legacy single-episode data and new multi-episode rows behave predictably.
    ''' Returns 0 if the patient has no OrthoInf row.
    ''' </summary>
    Public Shared Function ResolveDefaultOrthoIdForPatient(patientId As Integer) As Integer
        If patientId <= 0 Then Return 0
        Dim cs As String = DentistXDATA.GetConnection.ConnectionString
        Using conn As New SqlConnection(cs)
            conn.Open()
            Const sql As String = "SELECT TOP 1 OrthoID FROM dbo.OrthoInf WHERE PatientID = @PatientID ORDER BY TreatDate DESC, OrthoID DESC"
            Dim id = conn.QuerySingleOrDefault(Of Integer?)(sql, New With {.PatientID = patientId})
            Return If(id.HasValue, id.Value, 0)
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

End Class
