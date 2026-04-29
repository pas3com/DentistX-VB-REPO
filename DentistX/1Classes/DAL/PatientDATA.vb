Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.SqlTypes
Imports System.Collections
Imports System.Configuration
Imports Dapper

Public Class PatientDATA
    Implements IDisposable

    Private ConnectionString As String = DentistXDATA.GetConnection.ConnectionString

    ''' <summary>SQL Server datetime must be between SqlDateTime.MinValue and MaxValue. Invalid/out-of-range becomes Nothing (NULL parameter).</summary>
    Public Shared Function NormalizeNullableDateForSql(d As DateTime?) As DateTime?
        If Not d.HasValue Then Return Nothing
        Try
            Dim _unused As SqlDateTime = New SqlDateTime(d.Value)
            Return d.Value
        Catch
            Return Nothing
        End Try
    End Function

    Public Function SelectAll() As IEnumerable(Of Patient)
        Using conn As New SqlConnection(ConnectionString)
            conn.Open()
            Return conn.Query(Of Patient)("SELECT * FROM Patient")
        End Using
    End Function
    Public Function Select_RecordByID(ByVal patientID As Integer) As Patient
        Using conn As New SqlConnection(ConnectionString)
            Dim sql As String = "Select * FROM Patient WHERE PatientID = @PatientID"
            Return conn.QuerySingleOrDefault(Of Patient)(sql, New With {.PatientID = patientID})
        End Using
    End Function
    Public Function Select_LastPatient() As Patient
        Using conn As New SqlConnection(ConnectionString)
            '  Dim sql As String = "SELECT TOP 1 * FROM Patient ORDER BY PatientID DESC" OR
            Dim sql As String = "Select * FROM Patient WHERE PatientID = (SELECT MAX(PatientID) FROM Patient)"
            Return conn.QuerySingleOrDefault(Of Patient)(sql)
        End Using
    End Function
    Public Function Select_RecordByNumber(ByVal patientNUM As String) As Patient
        Using conn As New SqlConnection(ConnectionString)
            Dim sql As String = "Select * FROM Patient WHERE PatientNumber = @PatientNumber"
            Return conn.QuerySingleOrDefault(Of Patient)(sql, New With {.PatientNumber = patientNUM})
        End Using
    End Function
    Public Function Select_Record(ByVal clsPatient As Patient) As Patient
        Using conn As New SqlConnection(ConnectionString)
            Dim sql As String = "Select * FROM Patient WHERE PatientID = @PatientID"
            Return conn.QuerySingleOrDefault(Of Patient)(sql, New With {.PatientId = clsPatient.PatientID})
        End Using
    End Function
    Public Function InsertPatient(ByVal clsPatient As Patient) As Integer
        Try
            Using conn As New SqlConnection(ConnectionString)
                Dim parameters As New DynamicParameters()
                parameters.Add("@PatientName", clsPatient.PatientName)
                parameters.Add("@Sex", clsPatient.Sex)
                parameters.Add("@Age", clsPatient.Age)
                parameters.Add("@Phone", clsPatient.Phone)
                parameters.Add("@WhatsAppPrefix", clsPatient.WhatsAppPrefix)
                parameters.Add("@WhatsApp", clsPatient.WhatsApp)
                parameters.Add("@Address", clsPatient.Address)
                parameters.Add("@Health", clsPatient.Health)
                parameters.Add("@Treat", clsPatient.Treat)
                parameters.Add("@Implant", clsPatient.Implant)
                parameters.Add("@Mobile", clsPatient.Mobile)
                parameters.Add("@Ortho", clsPatient.Ortho)
                parameters.Add("@Diag", clsPatient.Diag)
                parameters.Add("@Struc", clsPatient.Struc)
                parameters.Add("@Notes", clsPatient.Notes)
                parameters.Add("@BirthY", clsPatient.BirthY)
                parameters.Add("@CreatedBy", 1)
                parameters.Add("@CreateDate", Now.Date)
                parameters.Add("@ReturnValue", dbType:=DbType.Int32, direction:=ParameterDirection.Output)
                conn.Execute("dbo.PatientInsert", parameters, commandType:=CommandType.StoredProcedure)
                ' Return the result from the stored procedure
                Return parameters.Get(Of Integer)("@ReturnValue")
            End Using
        Catch ex As Exception
            ' Log the error if needed
            ' Return a distinct error code
            Return -999
        End Try
    End Function

    Public Function Add(ByVal clsPatient As Patient) As Boolean
        Dim RowsAffected As Integer = 0
        Using conn As New SqlConnection(ConnectionString)
            Dim sql As String = "INSERT INTO Patient (PatientName, Sex, Age, Phone, WhatsAppPrefix, WhatsApp, Address, Health, Treat, Implant, Mobile, Ortho, Diag, Struc, Notes, BirthY, CreatedBy, CreateDate) VALUES (@PatientName, @Sex, @Age, @Phone, @WhatsAppPrefix, @WhatsApp, @Address, @Health, @Treat, @Implant, @Mobile, @Ortho, @Diag, @Struc, @Notes, @BirthY, @CreatedBy, @CreateDate)"
            RowsAffected = conn.Execute(sql, New With {
                .PatientName = clsPatient.PatientName,
                .Sex = clsPatient.Sex,
                .Age = clsPatient.Age,
                .Phone = clsPatient.Phone,
                .WhatsAppPrefix = clsPatient.WhatsAppPrefix,
                .WhatsApp = clsPatient.WhatsApp,
                .Address = clsPatient.Address,
                .Health = clsPatient.Health,
                .Treat = clsPatient.Treat,
                .Implant = clsPatient.Implant,
                .Mobile = clsPatient.Mobile,
                .Ortho = clsPatient.Ortho,
                .Diag = clsPatient.Diag,
                .Struc = clsPatient.Struc,
                .Notes = clsPatient.Notes,
                .BirthY = clsPatient.BirthY,
                .CreatedBy = clsPatient.CreatedBy,
                .CreateDate = NormalizeNullableDateForSql(clsPatient.CreateDate)})
            Return RowsAffected > 0
        End Using
    End Function

    Public Function Update(oldPatient As Patient, newPatient As Patient) As Boolean
        Using conn As New SqlConnection(ConnectionString)
            Dim parameters = New With {
                    .OldPatientID = oldPatient.PatientID,
                    .NewPatientName = newPatient.PatientName,
                    .OldPatientName = oldPatient.PatientName,
                    .NewSex = newPatient.Sex,
                    .OldSex = oldPatient.Sex,
                    .NewAge = newPatient.Age,
                    .OldAge = oldPatient.Age,
                    .NewPhone = newPatient.Phone,
                    .OldPhone = oldPatient.Phone,
                    .NewWhatsAppPrefix = newPatient.WhatsAppPrefix,
                    .OldWhatsAppPrefix = oldPatient.WhatsAppPrefix,
                    .NewWhatsApp = newPatient.WhatsApp,
                    .OldWhatsApp = oldPatient.WhatsApp,
                    .NewAddress = newPatient.Address,
                    .OldAddress = oldPatient.Address,
                    .NewHealth = newPatient.Health,
                    .OldHealth = oldPatient.Health,
                    .NewTreat = newPatient.Treat,
                    .OldTreat = oldPatient.Treat,
                    .NewImplant = newPatient.Implant,
                    .OldImplant = oldPatient.Implant,
                    .NewMobile = newPatient.Mobile,
                    .OldMobile = oldPatient.Mobile,
                    .NewOrtho = newPatient.Ortho,
                    .OldOrtho = oldPatient.Ortho,
                    .NewDiag = newPatient.Diag,
                    .OldDiag = oldPatient.Diag,
                    .NewStruc = newPatient.Struc,
                    .OldStruc = oldPatient.Struc,
                    .NewNotes = newPatient.Notes,
                    .OldNotes = oldPatient.Notes,
                    .NewBirthY = newPatient.BirthY,
                    .OldBirthY = oldPatient.BirthY,
                    .NewCreatedBy = newPatient.CreatedBy,
                    .OldCreatedBy = oldPatient.CreatedBy,
                    .NewCreateDate = NormalizeNullableDateForSql(newPatient.CreateDate),
                    .OldCreateDate = NormalizeNullableDateForSql(oldPatient.CreateDate)
                                          }
            Dim affectedRows As Integer = conn.Execute("UPDATE [Patient] SET [PatientName] = @NewPatientName, [Sex] = @NewSex, [Age] = @NewAge,
                                                            [Phone] = @NewPhone,[WhatsAppPrefix]=@NewWhatsAppPrefix,[WhatsApp]=@NewWhatsApp, [Address] = @NewAddress, [Health] = @NewHealth, [Treat] = @NewTreat,
                                                            [Implant] = @NewImplant, [Mobile] = @NewMobile, [Ortho] = @NewOrtho, [Diag] = @NewDiag, [Struc] = @NewStruc,
                                                            [Notes] = @NewNotes, [BirthY] = @NewBirthY, [CreatedBy] = @NewCreatedBy, [CreateDate] = @NewCreateDate
                                                            WHERE [PatientID] = @OldPatientID", parameters)
            Return affectedRows > 0
        End Using
    End Function
    Public Function UpdateTrt(trt As Boolean, patientID As Integer) As Boolean
        Using conn As New SqlConnection(ConnectionString)
            Dim updateStatement As String = "UPDATE [Patient] SET  [Treat] = @Trt  WHERE [PatientID] = @PatientID"
            Dim affectedRows As Integer = conn.Execute(updateStatement, New With {.PatientID = patientID, .Treat = trt})
            Return affectedRows > 0
        End Using
    End Function
    Public Function UpdateIMP(imp As Boolean, patientID As Integer) As Boolean
        Using conn As New SqlConnection(ConnectionString)
            Dim updateStatement As String = "UPDATE [Patient] SET  [Implant] = @imp  WHERE [PatientID] = @PatientID"
            Dim affectedRows As Integer = conn.Execute(updateStatement, New With {.PatientID = patientID, .Implant = imp})
            Return affectedRows > 0
        End Using
    End Function
    Public Function UpdateStruc(struc As Boolean, patientID As Integer) As Boolean
        Using conn As New SqlConnection(ConnectionString)
            Dim updateStatement As String = "UPDATE [Patient] SET  [Struc] = @struc  WHERE [PatientID] = @PatientID"
            Dim affectedRows As Integer = conn.Execute(updateStatement, New With {.PatientID = patientID, .Struc = struc})
            Return affectedRows > 0
        End Using
    End Function
    Public Function UpdateDiag(diag As Boolean, patientID As Integer) As Boolean
        Using conn As New SqlConnection(ConnectionString)
            Dim updateStatement As String = "UPDATE [Patient] SET  [Diag] = @Diag  WHERE [PatientID] = @PatientID"
            Dim affectedRows As Integer = conn.Execute(updateStatement, New With {.PatientID = patientID, .Diag = diag})
            Return affectedRows > 0
        End Using
    End Function
    Public Function UpdateMob(mob As Boolean, patientID As Integer) As Boolean
        Using conn As New SqlConnection(ConnectionString)
            Dim updateStatement As String = "UPDATE [Patient] SET  [Mobile] = @Mobile  WHERE [PatientID] = @PatientID"
            Dim affectedRows As Integer = conn.Execute(updateStatement, New With {.PatientID = patientID, .Mobile = mob})
            Return affectedRows > 0
        End Using
    End Function

    Public Function UpdateMob(mob As Boolean, patientID As Integer, trans As SqlTransaction) As Boolean
        If trans Is Nothing Then
            Return UpdateMob(mob, patientID)
        End If
        Dim updateStatement As String = "UPDATE [Patient] SET  [Mobile] = @Mobile  WHERE [PatientID] = @PatientID"
        Dim affectedRows As Integer = trans.Connection.Execute(updateStatement, New With {.PatientID = patientID, .Mobile = mob}, trans)
        Return affectedRows > 0
    End Function

    ''' <summary>When a jaw or diagnosis row is saved, set Patient.Mobile if master TblTRTS row is group DENTURES.</summary>
    Public Shared Sub EnsureMobileIfDentureTreatment(patientId As Nullable(Of Integer), treatName As String, Optional trans As SqlTransaction = Nothing)
        If Not patientId.HasValue OrElse patientId.Value <= 0 OrElse String.IsNullOrWhiteSpace(treatName) Then Return
        Dim trtRow = TblTRTSDATA.SelectByTrtName(treatName.Trim())
        If trtRow Is Nothing OrElse Not String.Equals(If(trtRow.TrtGroup, String.Empty), "DENTURES", StringComparison.OrdinalIgnoreCase) Then Return
        Using p As New PatientDATA()
            p.UpdateMob(True, patientId.Value, trans)
        End Using
    End Sub
    Public Function UpdateOrtho(ortho As Boolean, patientID As Integer) As Boolean
        Using conn As New SqlConnection(ConnectionString)
            Dim updateStatement As String = "UPDATE [Patient] SET  [Ortho] = @ortho  WHERE [PatientID] = @PatientID"
            Dim affectedRows As Integer = conn.Execute(updateStatement, New With {.PatientID = patientID, .Ortho = ortho})
            Return affectedRows > 0
        End Using
    End Function

    ''' <summary>Update WhatsApp number for a patient.</summary>
    Public Function UpdateWhatsApp(patientId As Integer, whatsApp As String) As Boolean
        Using conn As New SqlConnection(ConnectionString)
            Dim affectedRows = conn.Execute("UPDATE [Patient] SET [WhatsApp] = @WhatsApp WHERE [PatientID] = @PatientID",
                New With {.PatientID = patientId, .WhatsApp = If(whatsApp, "")})
            Return affectedRows > 0
        End Using
    End Function

    ''' <summary>Update WhatsApp local (10-digit) and <c>WhatsAppPrefix</c> (full combo text, e.g. Palestine (+970)).</summary>
    Public Function UpdateWhatsAppAndPrefix(patientId As Integer, whatsApp As String, whatsAppPrefix As String) As Boolean
        Using conn As New SqlConnection(ConnectionString)
            Dim affectedRows = conn.Execute(
                "UPDATE [Patient] SET [WhatsApp] = @WhatsApp, [WhatsAppPrefix] = @WhatsAppPrefix WHERE [PatientID] = @PatientID",
                New With {
                    .PatientID = patientId,
                    .WhatsApp = If(whatsApp, ""),
                    .WhatsAppPrefix = If(whatsAppPrefix, "")
                })
            Return affectedRows > 0
        End Using
    End Function
    Public Function Delete(ByVal clsPatient As Patient) As Boolean
        Using connection As SqlConnection = DentistXDATA.GetConnection()
            connection.Open()

            connection.Execute("DELETE FROM [Patient] WHERE PatientID = @PatientID",
                           New With {.PatientID = clsPatient.PatientID})

            ' Check if the record still exists instead of row count
            Dim stillExists = connection.ExecuteScalar(Of Integer)(
            "SELECT COUNT(1) FROM Patient WHERE PatientID = @PatientID",
            New With {.PatientID = clsPatient.PatientID})

            Return stillExists = 0
        End Using
    End Function



#Region "Methods to get parents and childs"
    Public Function GetPatientColors(ByVal clsPatient As Patient) As IEnumerable(Of PatientColors)
        Using conn As New SqlConnection(ConnectionString)
            conn.Open()
            Dim query As String = "SELECT * FROM [PatientColors] WHERE [PatientID] = @PatientID"
            Return conn.Query(Of PatientColors)(query, New With {.PatientID = clsPatient.PatientID})
        End Using
    End Function

    Public Function GetRD(ByVal clsPatient As Patient) As IEnumerable(Of RD)
        Using conn As New SqlConnection(ConnectionString)
            conn.Open()
            Dim query As String = "SELECT * FROM [RD] WHERE [PatientID] = @PatientID"
            Return conn.Query(Of RD)(query, New With {.PatientID = clsPatient.PatientID})
        End Using
    End Function

    Public Function GetPatient_Mobile(ByVal clsPatient As Patient) As IEnumerable(Of Patient_Mobile)
        Using conn As New SqlConnection(ConnectionString)
            conn.Open()
            Dim query As String = "SELECT * FROM [Patient_Mobile] WHERE [PatientID] = @PatientID"
            Return conn.Query(Of Patient_Mobile)(query, New With {.PatientID = clsPatient.PatientID})
        End Using
    End Function

    Public Function GetRDSTYLE(ByVal clsPatient As Patient) As IEnumerable(Of RDSTYLE)
        Using conn As New SqlConnection(ConnectionString)
            conn.Open()
            Dim query As String = "SELECT * FROM [RDSTYLE] WHERE [PatientID] = @PatientID"
            Return conn.Query(Of RDSTYLE)(query, New With {.PatientID = clsPatient.PatientID})
        End Using
    End Function

    Public Function GetRU(ByVal clsPatient As Patient) As IEnumerable(Of RU)
        Using conn As New SqlConnection(ConnectionString)
            conn.Open()
            Dim query As String = "SELECT * FROM [RU] WHERE [PatientID] = @PatientID"
            Return conn.Query(Of RU)(query, New With {.PatientID = clsPatient.PatientID})
        End Using
    End Function

    Public Function GetRUSTYLE(ByVal clsPatient As Patient) As IEnumerable(Of RUSTYLE)
        Using conn As New SqlConnection(ConnectionString)
            conn.Open()
            Dim query As String = "SELECT * FROM [RUSTYLE] WHERE [PatientID] = @PatientID"
            Return conn.Query(Of RUSTYLE)(query, New With {.PatientID = clsPatient.PatientID})
        End Using
    End Function

    Public Function GetSurgery(ByVal clsPatient As Patient) As IEnumerable(Of Surgery)
        Using conn As New SqlConnection(ConnectionString)
            conn.Open()
            Dim query As String = "SELECT * FROM [Surgery] WHERE [PatientID] = @PatientID"
            Return conn.Query(Of Surgery)(query, New With {.PatientID = clsPatient.PatientID})
        End Using
    End Function

    Public Function GetVisits(ByVal clsPatient As Patient) As IEnumerable(Of Visits)
        Using conn As New SqlConnection(ConnectionString)
            conn.Open()
            Dim query As String = "SELECT * FROM [Visits] WHERE [PatientID] = @PatientID"
            Return conn.Query(Of Visits)(query, New With {.PatientID = clsPatient.PatientID})
        End Using
    End Function

    Public Function GetLD(ByVal clsPatient As Patient) As IEnumerable(Of LD)
        Using conn As New SqlConnection(ConnectionString)
            conn.Open()
            Dim query As String = "SELECT * FROM [LD] WHERE [PatientID] = @PatientID"
            Return conn.Query(Of LD)(query, New With {.PatientID = clsPatient.PatientID})
        End Using
    End Function

    Public Function GetLDSTYLE(ByVal clsPatient As Patient) As IEnumerable(Of LDSTYLE)
        Using conn As New SqlConnection(ConnectionString)
            conn.Open()
            Dim query As String = "SELECT * FROM [LDSTYLE] WHERE [PatientID] = @PatientID"
            Return conn.Query(Of LDSTYLE)(query, New With {.PatientID = clsPatient.PatientID})
        End Using
    End Function

    Public Function GetLU(ByVal clsPatient As Patient) As IEnumerable(Of LU)
        Using conn As New SqlConnection(ConnectionString)
            conn.Open()
            Dim query As String = "SELECT * FROM [LU] WHERE [PatientID] = @PatientID"
            Return conn.Query(Of LU)(query, New With {.PatientID = clsPatient.PatientID})
        End Using
    End Function

    Public Function GetMobileStructure(ByVal clsPatient As Patient) As IEnumerable(Of MobileStructure)
        Using conn As New SqlConnection(ConnectionString)
            conn.Open()
            Dim query As String = "SELECT * FROM [MobileStructure] WHERE [PatientID] = @PatientID"
            Return conn.Query(Of MobileStructure)(query, New With {.PatientID = clsPatient.PatientID})
        End Using
    End Function

    Public Function GetLUSTYLE(ByVal clsPatient As Patient) As IEnumerable(Of LUSTYLE)
        Using conn As New SqlConnection(ConnectionString)
            conn.Open()
            Dim query As String = "SELECT * FROM [LUSTYLE] WHERE [PatientID] = @PatientID"
            Return conn.Query(Of LUSTYLE)(query, New With {.PatientID = clsPatient.PatientID})
        End Using
    End Function

    Public Function GetOrthoDiag(ByVal clsPatient As Patient) As IEnumerable(Of OrthoDiag)
        Using conn As New SqlConnection(ConnectionString)
            conn.Open()
            Dim query As String = "SELECT * FROM [OrthoDiag] WHERE [PatientID] = @PatientID"
            Return conn.Query(Of OrthoDiag)(query, New With {.PatientID = clsPatient.PatientID})
        End Using
    End Function

    Public Function GetOrthoInf(ByVal clsPatient As Patient) As IEnumerable(Of OrthoInf)
        Using conn As New SqlConnection(ConnectionString)
            conn.Open()
            Dim query As String = "SELECT * FROM [OrthoInf] WHERE [PatientID] = @PatientID"
            Return conn.Query(Of OrthoInf)(query, New With {.PatientID = clsPatient.PatientID})
        End Using
    End Function

    Public Function GetOrthoTreat(ByVal clsPatient As Patient) As IEnumerable(Of OrthoTreat)
        Using conn As New SqlConnection(ConnectionString)
            conn.Open()
            Dim query As String = "SELECT * FROM [OrthoTreat] WHERE [PatientID] = @PatientID"
            Return conn.Query(Of OrthoTreat)(query, New With {.PatientID = clsPatient.PatientID})
        End Using
    End Function

    Public Function GetOrthoTrtDet(ByVal clsPatient As Patient) As IEnumerable(Of OrthoTrtDet)
        Using conn As New SqlConnection(ConnectionString)
            conn.Open()
            Dim query As String = "SELECT * FROM [OrthoTrtDet] WHERE [PatientID] = @PatientID"
            Return conn.Query(Of OrthoTrtDet)(query, New With {.PatientID = clsPatient.PatientID})
        End Using
    End Function

    Public Function GetPatient_Imgs(ByVal clsPatient As Patient) As IEnumerable(Of Patient_Imgs)
        Using conn As New SqlConnection(ConnectionString)
            conn.Open()
            Dim query As String = "SELECT * FROM [Patient_Imgs] WHERE [PatientID] = @PatientID"
            Return conn.Query(Of Patient_Imgs)(query, New With {.PatientID = clsPatient.PatientID})
        End Using
    End Function

    Public Function GetPatient_MobStruc(ByVal clsPatient As Patient) As IEnumerable(Of Patient_MobStruc)
        Using conn As New SqlConnection(ConnectionString)
            conn.Open()
            Dim query As String = "SELECT * FROM [Patient_MobStruc] WHERE [PatientID] = @PatientID"
            Return conn.Query(Of Patient_MobStruc)(query, New With {.PatientID = clsPatient.PatientID})
        End Using
    End Function

    Public Function GetPatient_Notes(ByVal clsPatient As Patient) As IEnumerable(Of Patient_Notes)
        Using conn As New SqlConnection(ConnectionString)
            conn.Open()
            Dim query As String = "SELECT * FROM [Patient_Notes] WHERE [PatientID] = @PatientID"
            Return conn.Query(Of Patient_Notes)(query, New With {.PatientID = clsPatient.PatientID})
        End Using
    End Function

    Public Function GetPatient_RX(ByVal clsPatient As Patient) As IEnumerable(Of Patient_RX)
        Using conn As New SqlConnection(ConnectionString)
            conn.Open()
            Dim query As String = "SELECT * FROM [Patient_RX] WHERE [PatientID] = @PatientID"
            Return conn.Query(Of Patient_RX)(query, New With {.PatientID = clsPatient.PatientID})
        End Using
    End Function

    Public Function GetRDPL(ByVal clsPatient As Patient) As IEnumerable(Of RDPL)
        Using conn As New SqlConnection(ConnectionString)
            conn.Open()
            Dim query As String = "SELECT * FROM [RDPL] WHERE [PatientID] = @PatientID"
            Return conn.Query(Of RDPL)(query, New With {.PatientId = clsPatient.PatientID})
        End Using
    End Function

    Public Function GetRUPL(ByVal clsPatient As Patient) As IEnumerable(Of RUPL)
        Using conn As New SqlConnection(ConnectionString)
            conn.Open()
            Dim query As String = "SELECT * FROM [RUPL] WHERE [PatientID] = @PatientID"
            Return conn.Query(Of RUPL)(query, New With {.PatientId = clsPatient.PatientID})
        End Using
    End Function

    Public Function GetLDPL(ByVal clsPatient As Patient) As IEnumerable(Of LDPL)
        Using conn As New SqlConnection(ConnectionString)
            conn.Open()
            Dim query As String = "SELECT * FROM [LDPL] WHERE [PatientID] = @PatientID"
            Return conn.Query(Of LDPL)(query, New With {.PatientId = clsPatient.PatientID})
        End Using
    End Function
    Public Function GetLUPL(ByVal clsPatient As Patient) As IEnumerable(Of LUPL)
        Using conn As New SqlConnection(ConnectionString)
            conn.Open()
            Dim query As String = "SELECT * FROM [LUPL] WHERE [PatientID] = @PatientID"
            Return conn.Query(Of LUPL)(query, New With {.PatientId = clsPatient.PatientID})
        End Using
    End Function
    Public Function GetPatient_ToothChart(ByVal clsPatient As Patient) As IEnumerable(Of Patient_ToothChart)
        Using conn As New SqlConnection(ConnectionString)
            conn.Open()
            Dim query As String = "SELECT * FROM [Patient_ToothChart] WHERE [PatientID] = @PatientID"
            Return conn.Query(Of Patient_ToothChart)(query, New With {.PatientID = clsPatient.PatientID})
        End Using
    End Function

    Public Function GetPatient_ToothCheck(ByVal clsPatient As Patient) As IEnumerable(Of Patient_ToothCheck)
        Using conn As New SqlConnection(ConnectionString)
            conn.Open()
            Dim query As String = "SELECT * FROM [Patient_ToothCheck] WHERE [PatientID] = @PatientID"
            Return conn.Query(Of Patient_ToothCheck)(query, New With {.PatientID = clsPatient.PatientID})
        End Using
    End Function

    Public Function GetLabOrder(ByVal clsPatient As Patient) As IEnumerable(Of LabOrder)
        Using conn As New SqlConnection(ConnectionString)
            conn.Open()
            Dim query As String = "SELECT * FROM [LabOrder] WHERE [PatientID] = @PatientID"
            Return conn.Query(Of LabOrder)(query, New With {.PatientID = clsPatient.PatientID})
        End Using
    End Function

    Public Function GetPatient_ExternalToothTrt(ByVal clsPatient As Patient) As IEnumerable(Of Patient_ExternalToothTrt)
        Using conn As New SqlConnection(ConnectionString)
            conn.Open()
            Dim query As String = "SELECT * FROM [Patient_ExternalToothTrt] WHERE [PatientID] = @PatientID"
            Return conn.Query(Of Patient_ExternalToothTrt)(query, New With {.PatientID = clsPatient.PatientID})
        End Using
    End Function

    Public Function GetPatient_ToothTrt(ByVal clsPatient As Patient) As IEnumerable(Of Patient_ToothTrt)
        Using conn As New SqlConnection(ConnectionString)
            conn.Open()
            Dim query As String = "SELECT * FROM [Patient_ToothTrt] WHERE [PatientID] = @PatientID"
            Return conn.Query(Of Patient_ToothTrt)(query, New With {.PatientID = clsPatient.PatientID})
        End Using
    End Function

    Public Function GetPatient_Diagnose(ByVal clsPatient As Patient) As IEnumerable(Of Patient_Diagnosis)
        Using conn As New SqlConnection(ConnectionString)
            conn.Open()
            Dim query As String = "SELECT * FROM [Patient_Diagnose] WHERE [PatientID] = @PatientID"
            Return conn.Query(Of Patient_Diagnosis)(query, New With {.PatientID = clsPatient.PatientID})
        End Using
    End Function

    Public Function GetPatient_OtherTRT(ByVal clsPatient As Patient) As IEnumerable(Of Patient_OtherTRT)
        Using conn As New SqlConnection(ConnectionString)
            conn.Open()
            Dim query As String = "SELECT * FROM [Patient_OtherTRT] WHERE [PatientID] = @PatientID"
            Return conn.Query(Of Patient_OtherTRT)(query, New With {.PatientID = clsPatient.PatientID})
        End Using
    End Function

    Public Function GetPatient_Trts(ByVal clsPatient As Patient) As IEnumerable(Of Patient_Trts)
        Using conn As New SqlConnection(ConnectionString)
            conn.Open()
            Dim query As String = "SELECT * FROM [Patient_Trts] WHERE [PatientID] = @PatientID"
            Return conn.Query(Of Patient_Trts)(query, New With {.PatientID = clsPatient.PatientID})
        End Using
    End Function

    Public Function GetAppointmentC(ByVal clsPatient As Patient) As IEnumerable(Of AppointmentC)
        Using conn As New SqlConnection(ConnectionString)
            conn.Open()
            Dim query As String = "SELECT * FROM [AppointmentC] WHERE [PatientID] = @PatientID"
            Return conn.Query(Of AppointmentC)(query, New With {.PatientID = clsPatient.PatientID})
        End Using
    End Function


#End Region

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
