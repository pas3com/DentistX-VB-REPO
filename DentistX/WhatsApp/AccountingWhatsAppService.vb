Imports System.Collections.Generic
Imports System.Data.SqlClient
Imports System.IO
Imports System.Linq
Imports System.Threading.Tasks
Imports Dapper

''' <summary>Sends patient accounting summaries via WhatsApp. Tracks sent per patient.</summary>
Public Class AccountingWhatsAppService
    Private Shared ReadOnly _sentFilePath As String = Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
        "DentistX", "AccountingWhatsAppSent.txt")

    Public Shared Function IsAccountSent(patientId As Integer) As Boolean
        Dim sent = LoadSentPatientIds()
        Return sent.ContainsKey(patientId)
    End Function

    Public Shared Sub MarkAccountSent(patientId As Integer)
        Dim sent = LoadSentPatientIds()
        sent(patientId) = DateTime.Now
        SaveSentPatientIds(sent)
    End Sub

    ''' <summary>Build and send accounting message for a patient. Loads treatments and payments from DB.</summary>
    Public Shared Async Function SendAccountForPatientAsync(patientId As Integer, patientName As String, patientPhone As String,
                                                             Optional useEnglish As Boolean? = Nothing) As Task(Of Boolean)
        Dim clinicId As String = WhatsAppService.GetCurrentClinicId()
        If String.IsNullOrWhiteSpace(clinicId) Then Return False

        If Not Await WhatsAppService.TrySilentWhatsReconnectBackgroundAsync(clinicId).ConfigureAwait(False) Then Return False

        If String.IsNullOrWhiteSpace(patientPhone) Then Return False

        Dim msg = BuildAccountingMessageForPatient(patientId, useEnglish)
        If String.IsNullOrWhiteSpace(msg) Then Return False

        Try
            Dim waService2 As New WhatsAppService()
            Dim ctx = New WhatsAppSendContext With {
                .Category = WhatsAppMessageCategories.AccountingSummary,
                .PatientId = patientId,
                .SourceHint = NameOf(AccountingWhatsAppService),
                .DisplayName = patientName
            }
            Await waService2.SendMessageAsync(clinicId, patientPhone, msg, "", ctx)
            MarkAccountSent(patientId)
            Return True
        Catch
            Return False
        End Try
    End Function

    ''' <summary>Build accounting message for a patient (same format as WhatsHelper.BuildAccountingWhatsAppMessage).</summary>
    Public Shared Function BuildAccountingMessageForPatient(patientId As Integer,
                                                           Optional useEnglish As Boolean? = Nothing) As String
        Dim trts = LoadPatientTreatments(patientId)
        Dim pays = LoadPatientPayments(patientId)
        Dim patientName = LoadPatientName(patientId)
        Dim patientSex = LoadPatientSex(patientId)
        Dim useEngMsg = If(useEnglish.HasValue, useEnglish.Value, Eng)
        Return WhatsHelper.BuildAccountingWhatsAppMessage(If(patientName, ""), trts, pays,
                                                         excludeZeroValueTreatments:=False,
                                                         useArabic:=Not useEngMsg,
                                                         fullBody:=True,
                                                         patientSex:=patientSex)
    End Function

    Private Shared Function LoadPatientName(patientId As Integer) As String
        Using conn As New SqlConnection(DentistXDATA.GetConnection.ConnectionString)
            Return conn.ExecuteScalar(Of String)("SELECT PatientName FROM Patient WHERE PatientID=@ID", New With {.ID = patientId})
        End Using
    End Function

    Private Shared Function LoadPatientSex(patientId As Integer) As String
        Using conn As New SqlConnection(DentistXDATA.GetConnection.ConnectionString)
            Return conn.ExecuteScalar(Of String)("SELECT Sex FROM Patient WHERE PatientID=@ID", New With {.ID = patientId})
        End Using
    End Function

    Private Shared Function LoadPatientTreatments(patientId As Integer) As List(Of Patient_Trts)
        Using conn As New SqlConnection(DentistXDATA.GetConnection.ConnectionString)
            Return conn.Query(Of Patient_Trts)(
                "SELECT TrtID, PatientID, ToothTrtID, OrthoID, OtherTrtID, Detail, TrtDate, TrtValue, IsMultiTooth, Discount, Discount2, DiscountType FROM Patient_Trts WHERE PatientID=@ID",
                New With {.ID = patientId}).ToList()
        End Using
    End Function

    Private Shared Function LoadPatientPayments(patientId As Integer) As List(Of Patient_Pays)
        Using conn As New SqlConnection(DentistXDATA.GetConnection.ConnectionString)
            Return conn.Query(Of Patient_Pays)(
                "SELECT PayID, TrtID, PatientID, PayValue, PayDate, Notes, PayType, ChqOwner, AccountNumber, ChqNumber, ChqDueDate, ChqBank, IsCashed, InsuranceCompany, InsuranceNotes, IsForward, ForwardFromTo FROM Patient_Pays WHERE PatientID=@ID",
                New With {.ID = patientId}).ToList()
        End Using
    End Function

    Private Shared Function LoadSentPatientIds() As Dictionary(Of Integer, DateTime)
        Dim result As New Dictionary(Of Integer, DateTime)
        Try
            If Not File.Exists(_sentFilePath) Then Return result
            Dim lines = File.ReadAllLines(_sentFilePath)
            For Each line In lines
                Dim parts = line.Split("|"c)
                If parts.Length >= 2 Then
                    Dim id As Integer
                    Dim dt As DateTime
                    If Integer.TryParse(parts(0), id) AndAlso DateTime.TryParse(parts(1), dt) Then
                        result(id) = dt
                    End If
                End If
            Next
        Catch
        End Try
        Return result
    End Function

    Private Shared Sub SaveSentPatientIds(ids As Dictionary(Of Integer, DateTime))
        Try
            Dim dir = Path.GetDirectoryName(_sentFilePath)
            If Not String.IsNullOrEmpty(dir) Then Directory.CreateDirectory(dir)
            Dim lines = ids.Select(Function(kv) $"{kv.Key}|{kv.Value:yyyy-MM-dd HH:mm:ss}").ToArray()
            File.WriteAllLines(_sentFilePath, lines)
        Catch
        End Try
    End Sub
End Class
