Imports System.Data.SqlClient
Imports Dapper

Public Class MobileStructureDATA
    Private ConnectionString As String = DentistXDATA.GetConnection.ConnectionString

    Public Function SelectAll() As IEnumerable(Of MobileStructure)
        Using conn As New SqlConnection(ConnectionString)
            Return conn.Query(Of MobileStructure)("SELECT * FROM MobileStructure").ToList()
        End Using
    End Function

    Public Function Select_Record(structureID As Integer) As MobileStructure
        Using conn As New SqlConnection(ConnectionString)
            Return conn.QueryFirstOrDefault(Of MobileStructure)(
                "SELECT * FROM MobileStructure WHERE StructureID = @StructureID",
                New With {.StructureID = structureID}
            )
        End Using
    End Function

    Public Function Add(mobile As MobileStructure) As Boolean
        Using conn As New SqlConnection(ConnectionString)
            Dim sql As String =
                "INSERT INTO MobileStructure (PatientID, Arch, ToothNum, IsMissing, ImplantPresent, CrownPresent, BridgePresent, StrucName, StrucType,
                                                TeethType, StrucDate, ToothLoc, ToothNumbers, AddToothDate, MobilityLevel, HasRestoration, PocketDepth,
                                                BleedingOnProbing, AttachmentLoss, FurcationInvolvement, CrownCondition, Notes)
                 VALUES (@PatientID, @Arch, @ToothNum, @IsMissing, @ImplantPresent, @CrownPresent, @BridgePresent, @StrucName, @StrucType, @TeethType,
                         @StrucDate, @ToothLoc, @ToothNumbers, @AddToothDate, @MobilityLevel, @HasRestoration, @PocketDepth, @BleedingOnProbing,
                         @AttachmentLoss, @FurcationInvolvement, @CrownCondition, @Notes)"
            Return conn.Execute(sql, mobile) > 0
        End Using
    End Function

    Public Function Update(mobile As MobileStructure) As Boolean
        Using conn As New SqlConnection(ConnectionString)
            Dim sql As String =
                "UPDATE MobileStructure SET
                    PatientID = @PatientID,
                    Arch = @Arch,
                    ToothNum = @ToothNum,
                    IsMissing = @IsMissing,
                    ImplantPresent = @ImplantPresent,
                    CrownPresent = @CrownPresent,
                    BridgePresent = @BridgePresent,
                    StrucName = @StrucName,
                    StrucType = @StrucType,
                    TeethType = @TeethType,
                    StrucDate = @StrucDate,
                    ToothLoc = @ToothLoc,
                    ToothNumbers = @ToothNumbers,
                    AddToothDate = @AddToothDate,
                    MobilityLevel = @MobilityLevel,
                    HasRestoration = @HasRestoration,
                    PocketDepth = @PocketDepth,
                    BleedingOnProbing = @BleedingOnProbing,
                    AttachmentLoss = @AttachmentLoss,
                    FurcationInvolvement = @FurcationInvolvement,
                    CrownCondition = @CrownCondition,
                    Notes = @Notes
                 WHERE StructureID = @StructureID"
            Return conn.Execute(sql, mobile) > 0
        End Using
    End Function

    Public Function Delete(structureID As Integer) As Boolean
        Using conn As New SqlConnection(ConnectionString)
            Return conn.Execute("DELETE FROM MobileStructure WHERE StructureID = @StructureID",
                                New With {.StructureID = structureID}) > 0
        End Using
    End Function
End Class
