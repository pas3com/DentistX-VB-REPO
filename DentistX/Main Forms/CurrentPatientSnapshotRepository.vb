Imports System.Data.SqlClient
Imports System.IO
Imports System.Windows.Forms
Imports Dapper

Public Class CurrentPatientSnapshotStats
    Public ApptCount As Integer
    Public TreatmentCount As Integer
    Public TreatmentSum As Decimal
    Public PaymentsSum As Decimal
    Public ImgBeforeCount As Integer
    Public ImgDuringCount As Integer
    Public ImgAfterCount As Integer
    Public LabOrderCount As Integer
    Public OrthoFlag As Boolean
    Public DiagFlag As Boolean
End Class

''' <summary>Extra aggregates for the Current patient dock detail groups (appointments, ortho, treats, diagnosis).</summary>
Public Class CurrentPatientDockDetails
    Public FirstApptStart As DateTime?
    Public LastApptStart As DateTime?
    Public FirstTrtDate As DateTime?
    Public LastTrtDate As DateTime?
    Public OrthoStartDate As DateTime?
    Public OrthoLastWorkDate As DateTime?
    Public DiagToothCount As Integer
    Public DiagToothFirstDate As DateTime?
    Public DiagToothLastDate As DateTime?
    Public DiagToothFirstTreat As String
    Public DiagToothLastTreat As String
    Public DiagDetAgreementCount As Integer
    Public DiagDetFirstDate As DateTime?
    Public DiagDetLastDate As DateTime?
End Class

Public Class CurrentPatientSnapshotRepository
    Public Function GetStats(patientId As Integer, patientForFlags As Patient) As CurrentPatientSnapshotStats
        Dim s As New CurrentPatientSnapshotStats()
        If patientId <= 0 Then Return s
        If patientForFlags IsNot Nothing Then
            s.OrthoFlag = patientForFlags.Ortho.GetValueOrDefault()
            s.DiagFlag = patientForFlags.Diag.GetValueOrDefault()
        End If
        Try
            Using cn As New SqlConnection(DentistXDATA.GetConnection.ConnectionString)
                cn.Open()
                s.ApptCount = cn.ExecuteScalar(Of Integer)(
                    "SELECT COUNT(1) FROM dbo.AppointmentC WHERE PatientId = @p",
                    New With {.p = patientId})
                s.TreatmentCount = cn.ExecuteScalar(Of Integer)(
                    "SELECT COUNT(1) FROM dbo.Patient_Trts WHERE PatientID = @p",
                    New With {.p = patientId})
                s.TreatmentSum = cn.ExecuteScalar(Of Decimal)(
                    "SELECT ISNULL(SUM(TrtValue),0) FROM dbo.Patient_Trts WHERE PatientID = @p",
                    New With {.p = patientId})
                s.PaymentsSum = cn.ExecuteScalar(Of Decimal)(
                    "SELECT ISNULL(SUM(PayValue),0) FROM dbo.Patient_Pays WHERE PatientID = @p",
                    New With {.p = patientId})
                s.LabOrderCount = cn.ExecuteScalar(Of Integer)(
                    "SELECT COUNT(1) FROM dbo.LabOrder WHERE PatientID = @p",
                    New With {.p = patientId})
            End Using
        Catch
        End Try
        Try
            Dim root = Path.Combine(Application.StartupPath, "Images", "Patient_" & patientId.ToString())
            s.ImgBeforeCount = CountFiles(Path.Combine(root, "Before"))
            s.ImgDuringCount = CountFiles(Path.Combine(root, "During"))
            s.ImgAfterCount = CountFiles(Path.Combine(root, "After"))
        Catch
        End Try
        Return s
    End Function

    Private Shared Function CountFiles(dir As String) As Integer
        If Not Directory.Exists(dir) Then Return 0
        Try
            Return Directory.GetFiles(dir).Length
        Catch
            Return 0
        End Try
    End Function

    ''' <summary>Loads date ranges and counts used by the current-patient dock grouped panels.</summary>
    Public Function GetDockDetails(patientId As Integer) As CurrentPatientDockDetails
        Dim d As New CurrentPatientDockDetails()
        If patientId <= 0 Then Return d
        Try
            Using cn As New SqlConnection(DentistXDATA.GetConnection.ConnectionString)
                cn.Open()
                d.FirstApptStart = cn.ExecuteScalar(Of DateTime?)(
                    "SELECT MIN(StartDateTime) FROM dbo.AppointmentC WHERE PatientId = @p",
                    New With {.p = patientId})
                d.LastApptStart = cn.ExecuteScalar(Of DateTime?)(
                    "SELECT MAX(StartDateTime) FROM dbo.AppointmentC WHERE PatientId = @p",
                    New With {.p = patientId})

                d.FirstTrtDate = cn.ExecuteScalar(Of DateTime?)(
                    "SELECT MIN(TrtDate) FROM dbo.Patient_Trts WHERE PatientID = @p",
                    New With {.p = patientId})
                d.LastTrtDate = cn.ExecuteScalar(Of DateTime?)(
                    "SELECT MAX(TrtDate) FROM dbo.Patient_Trts WHERE PatientID = @p",
                    New With {.p = patientId})

                d.OrthoStartDate = cn.ExecuteScalar(Of DateTime?)(
                    "SELECT MIN(TreatDate) FROM dbo.OrthoInf WHERE PatientID = @p",
                    New With {.p = patientId})
                d.OrthoLastWorkDate = cn.ExecuteScalar(Of DateTime?)(
                    "SELECT MAX(WorkDate) FROM dbo.OrthoTrtDet WHERE PatientID = @p AND WorkDate IS NOT NULL",
                    New With {.p = patientId})

                d.DiagToothCount = cn.ExecuteScalar(Of Integer)(
                    "SELECT COUNT(1) FROM dbo.Patient_Diagnosis WHERE PatientID = @p",
                    New With {.p = patientId})
                d.DiagToothFirstDate = cn.ExecuteScalar(Of DateTime?)(
                    "SELECT MIN(TreatDate) FROM dbo.Patient_Diagnosis WHERE PatientID = @p",
                    New With {.p = patientId})
                d.DiagToothLastDate = cn.ExecuteScalar(Of DateTime?)(
                    "SELECT MAX(TreatDate) FROM dbo.Patient_Diagnosis WHERE PatientID = @p",
                    New With {.p = patientId})

                If d.DiagToothFirstDate.HasValue Then
                    d.DiagToothFirstTreat = cn.ExecuteScalar(Of String)(
                        "SELECT TOP 1 Treat FROM dbo.Patient_Diagnosis WHERE PatientID = @p AND TreatDate = @dt ORDER BY DiagID",
                        New With {.p = patientId, .dt = d.DiagToothFirstDate.Value})
                End If
                If d.DiagToothLastDate.HasValue Then
                    If d.DiagToothLastDate.Value = d.DiagToothFirstDate.GetValueOrDefault() Then
                        d.DiagToothLastTreat = d.DiagToothFirstTreat
                    Else
                        d.DiagToothLastTreat = cn.ExecuteScalar(Of String)(
                            "SELECT TOP 1 Treat FROM dbo.Patient_Diagnosis WHERE PatientID = @p AND TreatDate = @dt ORDER BY DiagID DESC",
                            New With {.p = patientId, .dt = d.DiagToothLastDate.Value})
                    End If
                End If

                d.DiagDetAgreementCount = cn.ExecuteScalar(Of Integer)(
                    "SELECT COUNT(1) FROM dbo.Patient_DiagDet WHERE PatientID = @p AND NULLIF(LTRIM(RTRIM(ISNULL(DiagAgreament,''))), '') IS NOT NULL",
                    New With {.p = patientId})
                d.DiagDetFirstDate = cn.ExecuteScalar(Of DateTime?)(
                    "SELECT MIN(DiagDate) FROM dbo.Patient_DiagDet WHERE PatientID = @p AND DiagDate IS NOT NULL",
                    New With {.p = patientId})
                d.DiagDetLastDate = cn.ExecuteScalar(Of DateTime?)(
                    "SELECT MAX(DiagDate) FROM dbo.Patient_DiagDet WHERE PatientID = @p AND DiagDate IS NOT NULL",
                    New With {.p = patientId})
            End Using
        Catch
        End Try
        Return d
    End Function
End Class
