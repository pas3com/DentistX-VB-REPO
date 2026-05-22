Imports System
Imports System.Collections.Generic
Imports System.Data
Imports System.Data.SqlClient
Imports System.Linq
Imports System.Text
Imports System.Drawing
Imports System.Windows.Forms
Imports Dapper

Public Class ApptDataProvider
    Private ReadOnly _repo As AppointmentCRepository
    Private ReadOnly _doctorCache As New Dictionary(Of Integer, ApptDoctorInfo)()
    Private ReadOnly _patientNameCache As New Dictionary(Of Integer, String)()

    Public Sub New(repo As AppointmentCRepository)
        _repo = repo
    End Sub

    Public ReadOnly Property Repository As AppointmentCRepository
        Get
            Return _repo
        End Get
    End Property

    Public Function Load(state As ApptState) As ApptDataBundle
        Try
            Dim bundle As New ApptDataBundle()
            bundle.DateRange = GetViewRange(state)
            bundle.Appointments = _repo.GetFiltered(bundle.DateRange.StartDate,
                                                    bundle.DateRange.EndDate,
                                                    state.PatientFilterId,
                                                    state.DoctorFilterId,
                                                    NormalizeFilter(state.VisibleReason),
                                                    NormalizeFilter(state.VisibleStatus))

            LoadAllDoctors(bundle)

            For Each appointment In bundle.Appointments
                NormalizeAppointmentDate(appointment)
                Dim doctor = ResolveDoctor(appointment.DrID)
                If Not bundle.DoctorInfos.ContainsKey(doctor.DrID) Then
                    bundle.DoctorInfos.Add(doctor.DrID, doctor)
                End If

                Dim patientName = ResolvePatientName(appointment.PatientID)
                If Not bundle.PatientNames.ContainsKey(appointment.PatientID) Then
                    bundle.PatientNames.Add(appointment.PatientID, patientName)
                End If
            Next

            bundle.Appointments = ApptTheme.OrderAppointmentsForDisplay(bundle.Appointments, bundle, linkedDoctorAtEnd:=True, orderFirstDoctorId:=state.OrderByDoctorId)
            Return bundle
        Catch ex As Exception
            ApptErrorHelper.Report(ex,
                                   "ApptDataProvider.Load",
                                   showUser:=True,
                                   englishMessage:="Could not load appointments.",
                                   arabicMessage:="تعذر تحميل المواعيد.")
            Return New ApptDataBundle()
        End Try
    End Function

    ''' <summary>
    ''' Appointments for body PREV/NEXT edge hints: no date window — oldest through latest in the database for the same
    ''' filters as the schedule (patient all vs this patient, doctor, reason, status). Differs from <see cref="Load"/>
    ''' which uses <see cref="GetViewRange"/> / header filter dates.
    ''' </summary>
    Public Function LoadEdgeHintAppointments(state As ApptState) As List(Of AppointmentC)
        Try
            If state Is Nothing Then Return New List(Of AppointmentC)()
            Dim list = _repo.GetFiltered(
                Nothing,
                Nothing,
                state.PatientFilterId,
                state.DoctorFilterId,
                NormalizeFilter(state.VisibleReason),
                NormalizeFilter(state.VisibleStatus))
            For Each appointment In list
                NormalizeAppointmentDate(appointment)
            Next
            Return ApptTheme.OrderAppointmentsForDisplay(list, Function(id) _repo.GetDoctorName(id), linkedDoctorAtEnd:=True, orderFirstDoctorId:=state.OrderByDoctorId)
        Catch ex As Exception
            ApptErrorHelper.Report(ex,
                                   "ApptDataProvider.LoadEdgeHintAppointments",
                                   showUser:=False)
            Return New List(Of AppointmentC)()
        End Try
    End Function

    Public Function AddAppointment(appt As AppointmentC, Optional reminderMessageEnglish As Boolean? = Nothing) As Integer
        Try
            NormalizeAppointmentDate(appt)
            Dim newId = _repo.Insert(appt, reminderMessageEnglish)
            appt.AppointmentID = newId
            Return newId
        Catch ex As Exception
            ApptErrorHelper.Report(ex,
                                   "ApptDataProvider.AddAppointment",
                                   showUser:=True,
                                   englishMessage:="Could not add the appointment.",
                                   arabicMessage:="تعذر إضافة الموعد.")
            Return 0
        End Try
    End Function

    Public Function UpdateAppointment(appt As AppointmentC, Optional reminderMessageEnglish As Boolean? = Nothing) As Integer
        Try
            NormalizeAppointmentDate(appt)
            Return _repo.Update(appt, reminderMessageEnglish)
        Catch ex As Exception
            ApptErrorHelper.Report(ex,
                                   "ApptDataProvider.UpdateAppointment",
                                   showUser:=True,
                                   englishMessage:="Could not update the appointment.",
                                   arabicMessage:="تعذر تحديث الموعد.")
            Return 0
        End Try
    End Function

    ''' <summary>Moves an appointment to <paramref name="targetDay"/> keeping time-of-day and duration; transactional overlap check like <see cref="SchedulerNew.TrySaveAppointmentTransactional"/>.</summary>
    Public Function TryMoveAppointmentToDate(appt As AppointmentC, targetDay As Date, Optional reminderMessageEnglish As Boolean? = Nothing) As Boolean
        If appt Is Nothing OrElse appt.AppointmentID <= 0 Then Return False
        NormalizeAppointmentDate(appt)
        Dim timeOfDay = appt.StartDateTime.TimeOfDay
        Dim duration = appt.EndDateTime - appt.StartDateTime
        Dim originalStart = appt.StartDateTime
        Dim originalEnd = appt.EndDateTime
        Dim originalAppDate = appt.AppDate

        appt.AppDate = targetDay.Date
        appt.StartDateTime = targetDay.Date.Add(timeOfDay)
        appt.EndDateTime = appt.StartDateTime.Add(duration)

        Try
            Using conn As SqlConnection = DentistXDATA.GetConnection
                conn.Open()
                Using trans As SqlTransaction = conn.BeginTransaction(IsolationLevel.Serializable)
                    Dim overlaps = _repo.GetOverlappingAppointmentsTransactional(
                        conn, trans, appt.DrID, appt.PatientID, appt.StartDateTime, appt.EndDateTime, appt.AppointmentID)
                    If overlaps IsNot Nothing AndAlso overlaps.Any() Then
                        Dim message As New StringBuilder()
                        message.AppendLine(If(Eng, "Cannot move appointment - overlaps detected:", "لا يمكن نقل الموعد - تم اكتشاف تعارضات:"))
                        message.AppendLine()
                        For Each line In overlaps
                            message.AppendLine("• " & line)
                        Next
                        MessageBox.Show(message.ToString(),
                                        If(Eng, "Overlap Detected", "تم اكتشاف تعارض"),
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Warning)
                        trans.Rollback()
                        appt.StartDateTime = originalStart
                        appt.EndDateTime = originalEnd
                        appt.AppDate = originalAppDate
                        Return False
                    End If
                    _repo.UpdateTransactional(conn, trans, appt)
                    trans.Commit()
                    If appt.AppointmentID > 0 Then ApptTwoHourReminderQueueRepository.SyncFromAppointmentId(appt.AppointmentID, reminderMessageEnglish)
                    Return True
                End Using
            End Using
        Catch ex As Exception
            appt.StartDateTime = originalStart
            appt.EndDateTime = originalEnd
            appt.AppDate = originalAppDate
            ApptErrorHelper.Report(ex,
                                   "ApptDataProvider.TryMoveAppointmentToDate",
                                   showUser:=True,
                                   englishMessage:="Could not move the appointment.",
                                   arabicMessage:="تعذر نقل الموعد.")
            Return False
        End Try
    End Function

    ''' <summary>Updates appointment start/end times with transactional overlap check.</summary>
    Public Function TryUpdateAppointmentTimes(appt As AppointmentC, newStart As DateTime, newEnd As DateTime, Optional reminderMessageEnglish As Boolean? = Nothing) As Boolean
        If appt Is Nothing OrElse appt.AppointmentID <= 0 Then Return False
        NormalizeAppointmentDate(appt)

        Dim originalStart = appt.StartDateTime
        Dim originalEnd = appt.EndDateTime
        Dim originalAppDate = appt.AppDate

        appt.StartDateTime = newStart
        appt.EndDateTime = newEnd
        appt.AppDate = newStart.Date

        Try
            Using conn As SqlConnection = DentistXDATA.GetConnection
                conn.Open()
                Using trans As SqlTransaction = conn.BeginTransaction(IsolationLevel.Serializable)
                    Dim overlaps = _repo.GetOverlappingAppointmentsTransactional(
                        conn, trans, appt.DrID, appt.PatientID, appt.StartDateTime, appt.EndDateTime, appt.AppointmentID)
                    If overlaps IsNot Nothing AndAlso overlaps.Any() Then
                        Dim message As New StringBuilder()
                        message.AppendLine(If(Eng, "Cannot update appointment - overlaps detected:", "لا يمكن تحديث الموعد - تم اكتشاف تعارضات:"))
                        message.AppendLine()
                        For Each line In overlaps
                            message.AppendLine("• " & line)
                        Next
                        MessageBox.Show(message.ToString(),
                                        If(Eng, "Overlap Detected", "تم اكتشاف تعارض"),
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Warning)
                        trans.Rollback()
                        appt.StartDateTime = originalStart
                        appt.EndDateTime = originalEnd
                        appt.AppDate = originalAppDate
                        Return False
                    End If
                    _repo.UpdateTransactional(conn, trans, appt)
                    trans.Commit()
                    If appt.AppointmentID > 0 Then ApptTwoHourReminderQueueRepository.SyncFromAppointmentId(appt.AppointmentID, reminderMessageEnglish)
                    Return True
                End Using
            End Using
        Catch ex As Exception
            appt.StartDateTime = originalStart
            appt.EndDateTime = originalEnd
            appt.AppDate = originalAppDate
            ApptErrorHelper.Report(ex,
                                   "ApptDataProvider.TryUpdateAppointmentTimes",
                                   showUser:=True,
                                   englishMessage:="Could not update the appointment time.",
                                   arabicMessage:="تعذر تحديث وقت الموعد.")
            Return False
        End Try
    End Function

    Public Function DeleteAppointment(appointmentId As Integer) As Integer
        Try
            Return _repo.Delete(appointmentId)
        Catch ex As Exception
            ApptErrorHelper.Report(ex,
                                   "ApptDataProvider.DeleteAppointment",
                                   showUser:=True,
                                   englishMessage:="Could not delete the appointment.",
                                   arabicMessage:="تعذر حذف الموعد.")
            Return 0
        End Try
    End Function

    Public Function BuildVisualModel(appointment As AppointmentC,
                                     bundle As ApptDataBundle,
                                     state As ApptState,
                                     selector As Func(Of ApptCardVm, ApptCardAppearance)) As ApptCardVm
        Try
            Dim model As New ApptCardVm With {
                .Appointment = appointment,
                .PatientName = bundle.ResolvePatientName(appointment.PatientID),
                .DoctorInfo = bundle.ResolveDoctor(appointment.DrID)
            }

            Dim appearance = BuildDefaultCardAppearance(model, state)
            If selector IsNot Nothing Then
                Dim customAppearance = selector(model)
                If customAppearance IsNot Nothing Then
                    appearance = customAppearance
                End If
            End If

            model.Appearance = appearance
            Return model
        Catch ex As Exception
            ApptErrorHelper.Report(ex,
                                   "ApptDataProvider.BuildVisualModel",
                                   showUser:=False)
            Return New ApptCardVm With {
                .Appointment = appointment,
                .Appearance = New ApptCardAppearance()
            }
        End Try
    End Function

    Private Shared Function NormalizeFilter(value As String) As String
        If String.IsNullOrWhiteSpace(value) Then Return Nothing
        Return value.Trim()
    End Function

    Private Sub NormalizeAppointmentDate(appt As AppointmentC)
        If appt Is Nothing Then Return
        If appt.AppDate = Date.MinValue Then
            appt.AppDate = appt.StartDateTime.Date
        End If
        If appt.CreatedAt = Date.MinValue Then
            appt.CreatedAt = Date.Now
        End If
        If String.IsNullOrWhiteSpace(appt.CreatedBy) Then
            appt.CreatedBy = Environment.UserName
        End If
    End Sub

    Private Function ResolveDoctor(drId As Integer) As ApptDoctorInfo
        If _doctorCache.ContainsKey(drId) Then
            Return _doctorCache(drId)
        End If

        Dim doctor As New ApptDoctorInfo With {.DrID = drId, .DrName = $"Doctor {drId}", .DrColor = Color.LightSteelBlue}
        Try
            doctor.DrName = _repo.GetDoctorName(drId)
        Catch ex As Exception
            ApptErrorHelper.Report(ex, "ApptDataProvider.ResolveDoctor.Name", showUser:=False)
        End Try

        Try
            doctor.DrColor = ParseDoctorColor(_repo.GetDoctorColor(drId))
        Catch ex As Exception
            ApptErrorHelper.Report(ex, "ApptDataProvider.ResolveDoctor.Color", showUser:=False)
        End Try

        _doctorCache(drId) = doctor
        Return doctor
    End Function

    Private Function ResolvePatientName(patientId As Integer) As String
        If _patientNameCache.ContainsKey(patientId) Then
            Return _patientNameCache(patientId)
        End If

        Dim patientName = $"Patient {patientId}"
        Try
            patientName = _repo.GetPatientName(patientId)
        Catch ex As Exception
            ApptErrorHelper.Report(ex, "ApptDataProvider.ResolvePatientName", showUser:=False)
        End Try

        _patientNameCache(patientId) = patientName
        Return patientName
    End Function

    Private Sub LoadAllDoctors(bundle As ApptDataBundle)
        Try
            Using conn As New SqlConnection(DentistXDATA.GetConnection.ConnectionString)
                Dim doctors = conn.Query(Of (DrID As Integer, DrName As String, DrColor As String))(
                    "SELECT DrID, [DrName], [DrColor] FROM [dbo].[Doctors] ORDER BY DrName").ToList()

                For Each doctorRow In doctors
                    Dim doctor = New ApptDoctorInfo With {
                        .DrID = doctorRow.DrID,
                        .DrName = doctorRow.DrName,
                        .DrColor = ParseDoctorColor(doctorRow.DrColor)
                    }
                    _doctorCache(doctor.DrID) = doctor
                    If Not bundle.DoctorInfos.ContainsKey(doctor.DrID) Then
                        bundle.DoctorInfos.Add(doctor.DrID, doctor)
                    End If
                Next
            End Using
        Catch ex As Exception
            ApptErrorHelper.Report(ex, "ApptDataProvider.LoadAllDoctors", showUser:=False)
        End Try
    End Sub
End Class
