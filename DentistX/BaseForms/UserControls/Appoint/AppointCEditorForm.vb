
Imports System.Collections.Concurrent
Imports System.Data.SqlClient
Imports System.Drawing
Imports System.Linq
Imports System.Text
Imports Dapper
Imports DevExpress.XtraScheduler

Public Class AppointCEditorForm

    Private repo As New AppointmentCRepository
    Private _isNew As Boolean
    Private _setAppt As Boolean
    ''' <summary>False = Arabic, True = English — mirrors RadioLang; passed into reminder queue sync on save.</summary>
    Private useEng As Boolean '= False
    Private _apptDateFullDayBehavior As DateEditFullDayNameBehavior
    Private ReadOnly _whatsBinder As PatientWhatsControlsBinder
    ''' <summary>Same as RadioLang: English vs Arabic for manual WhatsApp and for queue preview text when saving.</summary>
    Public ReadOnly Property ReminderMessageEnglish As Boolean
        Get
            Return useEng
        End Get
    End Property
    Public Property Appointment As AppointmentC
    Public Property AppointmentC As AppointmentC
    Public Sub New(appt As AppointmentC, Optional isNew As Boolean = False, Optional setAppt As Boolean = False)
        Me.AppointmentC = appt
        Me._isNew = isNew
        Me._setAppt = setAppt
        InitializeComponent()
        ' Calendar dropdown: show full weekday names in the header (e.g. "Sunday"). Display-only tweak;
        ' LoadValues / BtnSave still use apptDate.DateTime / EditValue exactly the same way.
        DateEditHelper.ApplyFullDayNameCalendar(apptDate)
        _apptDateFullDayBehavior = New DateEditFullDayNameBehavior(apptDate)
        _whatsBinder = New PatientWhatsControlsBinder(cboPrefix, txtWhats, lblWhats, Me)
        StoreOriginalBounds(Me)
        If _isNew Then
            apptDate.DateTime = Date.Now
        ElseIf appt IsNot Nothing Then
            apptDate.DateTime = appt.AppDate
        Else
            apptDate.DateTime = Date.Now
        End If
        ' Prefix list must exist before applying Patient.WhatsAppPrefix / number (LoadValues matches combo items by digits).
        FillCboPrefixOnce(cboPrefix)
        LoadValues()
    End Sub

    Private Sub AppointCEditorForm_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        If _apptDateFullDayBehavior IsNot Nothing Then
            _apptDateFullDayBehavior.Dispose()
            _apptDateFullDayBehavior = Nothing
        End If
    End Sub



#Region "Resize"
    Private ReadOnly controlBoundsCache As New ConcurrentDictionary(Of Control, Rectangle)
    Private Const OriginalPanelWidth As Integer = 1214
    Private Const OriginalPanelHeight As Integer = 648
    Private ReadOnly originalPanelSize As New Size(OriginalPanelWidth, OriginalPanelHeight)

    Private Sub StoreOriginalBounds(container As Control)
        Dim sw As New Stopwatch
        sw.Start()

        For Each ctrl As Control In container.Controls
            controlBoundsCache.TryAdd(ctrl, ctrl.Bounds)
            If ctrl.HasChildren Then StoreOriginalBounds(ctrl)
        Next
        sw.Stop()

    End Sub
    Private Sub ResizeControlsProportionally()
        If controlBoundsCache.IsEmpty Then Return
        Dim sw As New Stopwatch
        sw.Start()
        Dim widthRatio = CSng(Me.Width) / OriginalPanelWidth
        Dim heightRatio = CSng(Me.Height) / OriginalPanelHeight

        For Each kvp In controlBoundsCache
            kvp.Key.SetBounds(
            CInt(kvp.Value.X * widthRatio),
            CInt(kvp.Value.Y * heightRatio),
            CInt(kvp.Value.Width * widthRatio),
            CInt(kvp.Value.Height * heightRatio))
        Next
        sw.Stop()
    End Sub
    Private Sub AppointCEditorForm_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        If controlBoundsCache.IsEmpty Then Return
        ResizeControlsProportionally()
    End Sub
#End Region

    ''' <summary>Time component = current hour with minutes/seconds 00, on the given calendar date (e.g. 9:37 now → 9:00).</summary>
    Private Shared Function DtpTimeCurrentHourOnDate(onDay As Date) As DateTime
        Dim n = DateTime.Now
        Return New DateTime(onDay.Year, onDay.Month, onDay.Day, n.Hour, 0, 0)
    End Function

    Private Sub LoadValues()
        If _isNew Then
            Me.Text = If(Eng, "Add Appointment", "إضافة موعد")
            If AppointmentC Is Nothing Then AppointmentC = New AppointmentC With {.AppDate = DateTime.Now, .StartDateTime = DateTime.Now, .EndDateTime = DateTime.Now.AddMinutes(30)}
            'txtPatientId.Text = AppointmentC.PatientID.ToString()
            'txtDrId.Text = AppointmentC.DrID.ToString()
            'apptDate.DateTime = AppointmentC.AppDate 'Date.Now '
            AppointmentC.Status = "Pending"
            PatientCombo1.SetCurrentPatientName(AppointmentC.PatientID)
            DoctorsCombo1.SetSelectedDrName(AppointmentC.DrID)
            apptDate.EditValue = AppointmentC.AppDate
            ' Add: start at current hour (minutes 00) on the chosen appointment day. Edit: keep DB times below.
            Dim apptDay = CDate(apptDate.EditValue).Date
            Dim startNow = DtpTimeCurrentHourOnDate(apptDay)
            dtpStart.EditValue = startNow
            dtpEnd.EditValue = startNow.AddMinutes(30)
            AppointmentC.StartDateTime = startNow
            AppointmentC.EndDateTime = startNow.AddMinutes(30)
            txtReason.Text = AppointmentC.Reason
            txtNotes.Text = AppointmentC.Notes
            If chkIncludeReason IsNot Nothing Then chkIncludeReason.Checked = AppointmentC.WhatsIncludeReason
            If chkIncludeNotes IsNot Nothing Then chkIncludeNotes.Checked = AppointmentC.WhatsIncludeNotes
            cboStatus.Text = AppointmentC.Status
        Else

            Me.Text = If(Eng, "Edit Appointment", "تعديل موعد")
            PatientCombo1.SetCurrentPatientName(AppointmentC.PatientID)

            DoctorsCombo1.SetSelectedDrName(AppointmentC.DrID)
            apptDate.DateTime = AppointmentC.AppDate
            dtpStart.EditValue = AppointmentC.StartDateTime
            dtpEnd.EditValue = AppointmentC.EndDateTime
            txtReason.Text = AppointmentC.Reason
            txtNotes.Text = AppointmentC.Notes
            If chkIncludeReason IsNot Nothing Then chkIncludeReason.Checked = AppointmentC.WhatsIncludeReason
            If chkIncludeNotes IsNot Nothing Then chkIncludeNotes.Checked = AppointmentC.WhatsIncludeNotes
            cboStatus.Text = AppointmentC.Status
        End If

        ApplyReminderLanguageRadioFromAppointment()
        ApplyWhatsReminderCheckLayout()
        RefreshShortLeadReminderCheckCaption()

        ' Set patient WhatsApp/phone for send (existing appt from AppointmentC, new appt from combo)
        Dim patientId As Integer = 0
        If AppointmentC IsNot Nothing AndAlso AppointmentC.PatientID > 0 Then
            patientId = AppointmentC.PatientID
        ElseIf PatientCombo1 IsNot Nothing AndAlso PatientCombo1.PatientID > 0 Then
            patientId = PatientCombo1.PatientID
        End If
        If patientId > 0 AndAlso txtWhats IsNot Nothing Then
            Try
                Dim patientData As New PatientDATA()
                Dim p = patientData.Select_RecordByID(patientId)
                If p IsNot Nothing Then
                    ApplyPatientWhatsFromDatabase(p)
                End If
            Catch
            End Try
        Else
            _whatsBinder.Unbind()
        End If
    End Sub

    Private Sub AppointCEditorForm_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        ' After form and PatientCombo are loaded, refresh WhatsApp from selected patient (fixes new appt when combo loads after LoadValues)
        BeginInvoke(New Action(AddressOf RefreshWhatsAppFromPatient))
    End Sub

    ''' <summary>
    ''' Validates WhatsApp/phone number using shared helper (English messages for this editor).
    ''' </summary>
    Private Function ValidateWhatsAppNumber(phone As String) As String
        Return WhatsHelper.ValidateWhatsAppNumberEnglish(phone)
    End Function

    Private Sub RefreshWhatsAppFromPatient()
        If txtWhats Is Nothing Then Return
        Dim patientId As Integer = 0
        If AppointmentC IsNot Nothing AndAlso AppointmentC.PatientID > 0 Then
            patientId = AppointmentC.PatientID
        ElseIf PatientCombo1 IsNot Nothing AndAlso PatientCombo1.PatientID > 0 Then
            patientId = PatientCombo1.PatientID
        End If
        If patientId > 0 Then
            Try
                Dim patientData As New PatientDATA()
                Dim p = patientData.Select_RecordByID(patientId)
                If p IsNot Nothing Then
                    ApplyPatientWhatsFromDatabase(p)
                End If
            Catch
            End Try
        End If
    End Sub

    Private ReadOnly _conn As SqlConnection = DentistXDATA.GetConnection '.ConnectionString

    'Private Function AppointmentOverlaps(conn As SqlConnection, drId As Integer, startDt As DateTime, endDt As DateTime, Optional excludeId As Integer? = Nothing) As Boolean
    '    Dim sql As String = "
    '    SELECT COUNT(1)
    '    FROM dbo.AppointmentC
    '    WHERE DrID = @DrID
    '      AND NOT (EndDateTime <= @StartDt OR StartDateTime >= @EndDt)
    '      " & If(excludeId.HasValue, "AND AppointmentID <> @AppointmentID", "") & ";"

    '    Return conn.ExecuteScalar(Of Integer)(sql, New With {
    '    .DrID = drId,
    '    .StartDt = startDt,
    '    .EndDt = endDt,
    '    .AppointmentID = If(excludeId, 0)
    '}) > 0
    'End Function

    'Private Sub BtnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
    '    Using conn As SqlConnection = DentistXDATA.GetConnection
    '        ' validate
    '        Dim pId As Integer
    '        Dim drId As Integer
    '        If Not Integer.TryParse(PatientCombo1.PatientID, pId) Then MessageBox.Show("PatientID required") : Return
    '        If Not Integer.TryParse(DoctorsCombo1.DrID, drId) Then MessageBox.Show("DoctorID required") : Return
    '        If dtpEnd.EditValue <= dtpStart.EditValue Then MessageBox.Show("End must be after start") : Return

    '        AppointmentC.PatientID = pId
    '        AppointmentC.DrID = drId
    '        AppointmentC.AppDate = apptDate.DateTime
    '        AppointmentC.StartDateTime = dtpStart.EditValue
    '        AppointmentC.EndDateTime = dtpEnd.EditValue
    '        AppointmentC.Reason = txtReason.Text
    '        AppointmentC.Notes = txtNotes.Text
    '        Me.Appointment = AppointmentC
    '        If AppointmentOverlaps(conn, AppointmentC.DrID, AppointmentC.StartDateTime, AppointmentC.EndDateTime, AppointmentC.AppointmentID) Then
    '            MessageBox.Show("This doctor already has an appointment during that time.", "Overlap Detected",
    '                    MessageBoxButtons.OK, MessageBoxIcon.Warning)
    '            Exit Sub
    '        End If

    '        ' ... proceed with INSERT or UPDATE normally ...
    '        Me.DialogResult = DialogResult.OK
    '    End Using
    'End Sub



    Private Async Sub BtnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        ' 1. Validate
        Dim pId As Integer = PatientCombo1.GetPatientID(PatientCombo1.CboPatient.Text)
        Dim drId As Integer = DoctorsCombo1.GetDrID(DoctorsCombo1.CboDoctors.Text)
        If pId < 1 Then MessageBox.Show(If(Eng, "Patient is required.", "المريض مطلوب."), If(Eng, "Validation", "التحقق"), MessageBoxButtons.OK, MessageBoxIcon.Warning) : Return
        If drId < 1 Then MessageBox.Show(If(Eng, "Doctor is required.", "الطبيب مطلوب."), If(Eng, "Validation", "التحقق"), MessageBoxButtons.OK, MessageBoxIcon.Warning) : Return
        If dtpEnd.EditValue <= dtpStart.EditValue Then MessageBox.Show(If(Eng, "End time must be after start time.", "يجب أن يكون وقت الانتهاء بعد وقت البدء."), If(Eng, "Validation", "التحقق"), MessageBoxButtons.OK, MessageBoxIcon.Warning) : Return

        ' Build start/end from appointment DATE + time controls (dtpStart/dtpEnd are time-only)
        Dim appDate = apptDate.DateTime
        Dim startDt As DateTime = New DateTime(appDate.Year, appDate.Month, appDate.Day, dtpStart.DateTime.Hour, dtpStart.DateTime.Minute, 0)
        Dim endDt As DateTime = New DateTime(appDate.Year, appDate.Month, appDate.Day, dtpEnd.DateTime.Hour, dtpEnd.DateTime.Minute, 0)

        AppointmentC.PatientID = pId
        AppointmentC.DrID = drId
        AppointmentC.AppDate = appDate
        AppointmentC.StartDateTime = startDt
        AppointmentC.EndDateTime = endDt
        AppointmentC.Reason = txtReason.Text
        AppointmentC.Notes = txtNotes.Text
        AppointmentC.WhatsIncludeReason = If(chkIncludeReason Is Nothing, True, chkIncludeReason.Checked)
        AppointmentC.WhatsIncludeNotes = If(chkIncludeNotes Is Nothing, True, chkIncludeNotes.Checked)
        AppointmentC.Status = cboStatus.Text
        AppointmentC.CreatedAt = Now
        AppointmentC.CreatedBy = CurrentUser.UsName
        Me.Appointment = AppointmentC

        ' 2. Check for overlapping appointments (doctor and patient) BEFORE saving
        Dim overlaps As List(Of String) = Nothing
        Try
            overlaps = repo.GetOverlappingAppointments(_conn, drId, pId, startDt, endDt, If(_isNew, CType(Nothing, Integer?), AppointmentC.AppointmentID))
        Catch ex As Exception
            MessageBox.Show(If(Eng, "Error checking overlapping appointments: ", "حدث خطأ أثناء التحقق من تداخل المواعيد: ") & ex.Message,
                            If(Eng, "Error", "خطأ"), MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End Try

        If overlaps IsNot Nothing AndAlso overlaps.Count > 0 Then
            Dim msg As New StringBuilder()
            msg.AppendLine(If(Eng, "This appointment overlaps with existing ones:", "هذا الموعد يتداخل مع مواعيد موجودة:"))
            For Each o In overlaps
                msg.AppendLine(" - " & o)
            Next
            MessageBox.Show(msg.ToString(), If(Eng, "Overlap Detected", "تم اكتشاف تداخل"), MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        ' 3. Save appointment
        Dim saveSucceeded As Boolean = False
        Try
            If _isNew AndAlso Not _setAppt Then
                Dim newApptId = repo.Insert(AppointmentC, useEng)
                AppointmentC.AppointmentID = newApptId
            ElseIf _isNew AndAlso _setAppt Then
                ' Scheduler will persist; just pass Appointment back
                Me.Appointment = AppointmentC
            Else
                repo.Update(AppointmentC, useEng)
            End If
            saveSucceeded = True
        Catch ex As Exception
            MessageBox.Show(If(Eng, "Error saving appointment: ", "حدث خطأ أثناء حفظ الموعد: ") & ex.Message,
                            If(Eng, "Save Failed", "فشل الحفظ"), MessageBoxButtons.OK, MessageBoxIcon.Error)
            saveSucceeded = False
        End Try

        If Not saveSucceeded AndAlso Not (_isNew AndAlso _setAppt) Then
            ' If we failed to persist here, do not send WhatsApp or update patient table
            Me.DialogResult = DialogResult.None
            Return
        End If

        ' 3b. Same as PatientVisitsCtl after save: dbo.ApptTwoHourWhatsAppQueue from chkWhats + 24h/2h (Insert/Update already syncs with defaults; re-apply user choices).
        Dim appointmentIdForQueue = AppointmentC.AppointmentID
        Dim wroteAppointmentToDbHere = saveSucceeded AndAlso Not (_isNew AndAlso _setAppt)
        If wroteAppointmentToDbHere AndAlso appointmentIdForQueue > 0 Then
            Dim wantScheduledReminders = chkWhats IsNot Nothing AndAlso chkWhats.Checked AndAlso
                chk24H IsNot Nothing AndAlso chk2H IsNot Nothing AndAlso
                (chk24H.Checked OrElse chk2H.Checked)
            If wantScheduledReminders Then
                AppointmentWhatsAppQueueService.NotifyAppointmentSaved(appointmentIdForQueue, useEng, chk24H.Checked, chk2H.Checked)
            Else
                ApptTwoHourReminderQueueRepository.DeleteByAppointmentId(appointmentIdForQueue)
            End If
        End If

        Me.DialogResult = DialogResult.OK

        ' 4. After OK: WhatsApp / patient phone fields when chkWhats applies.
        ' When _setAppt=True (e.g. SchedulerFull Add), this form does not INSERT; the host saves after ShowDialog.
        ' Users still expect WhatsApp when they tick chkWhats, so use saveSucceeded — not only "insert/update inside this form".
        Dim wePersistedHere As Boolean = saveSucceeded
        Dim sendWhats As Boolean = chkWhats IsNot Nothing AndAlso chkWhats.Checked
        If sendWhats AndAlso wePersistedHere Then
            EnsureWhatsBinderMatchesPatientId(pId)
            _whatsBinder.SaveIfDirty()
            RefreshLblWhats()
            Dim number As String = If(lblWhats IsNot Nothing, lblWhats.Text, "").Trim()
            If Not String.IsNullOrWhiteSpace(number) Then
                Dim validationMsg As String = ValidateWhatsAppNumber(number)
                If validationMsg <> "" Then
                    MessageBox.Show(validationMsg, If(Eng, "Invalid number", "رقم غير صالح"), MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Else
                    _whatsBinder.BeginSuppressAutoSaveWhileModal()
                    Try
                        Dim msg = BuildAppointmentWhatsAppMessage()
                        If Not String.IsNullOrWhiteSpace(msg) AndAlso Await WhatsAppService.EnsureWhatsAppConnectedOrNotifyAsync(Me) Then
                            Dim clinicId As String = WhatsAppService.GetCurrentClinicId()
                            Dim waService As New WhatsAppService()
                            Await waService.SendMessageAsync(clinicId, number, msg, "", BuildWhatsAppSendContextForEditor())
                            MessageBox.Show(If(Eng, "Appointment saved. WhatsApp reminder queued for sending.", "تم حفظ الموعد. تم إدراج تذكير واتساب في قائمة الإرسال."),
                                            If(Eng, "Saved", "تم الحفظ"), MessageBoxButtons.OK, MessageBoxIcon.Information)
                        End If
                    Catch ex As Exception
                        MessageBox.Show(If(Eng, "Appointment saved. WhatsApp send failed: ", "تم حفظ الموعد لكن فشل إرسال واتساب: ") & ex.Message,
                                        If(Eng, "Saved", "تم الحفظ"), MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Finally
                        _whatsBinder.EndSuppressAutoSaveAndSync()
                    End Try
                End If
            End If
        End If

        ' 5. If user changed country prefix or local Whats vs. snapshot when form loaded, update Patient.WhatsApp + WhatsAppPrefix.
        If wePersistedHere AndAlso pId > 0 Then
            EnsureWhatsBinderMatchesPatientId(pId)
            _whatsBinder.SaveIfDirty()
        End If
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.DialogResult = DialogResult.Cancel
    End Sub

    Private Sub apptDate_EditValueChanged(sender As Object, e As EventArgs) Handles apptDate.EditValueChanged
        dtpStart.EditValue = New DateTime(apptDate.DateTime.Year, apptDate.DateTime.Month, apptDate.DateTime.Day, dtpStart.DateTime.Hour, dtpStart.DateTime.Minute, 0)
        dtpEnd.EditValue = New DateTime(apptDate.DateTime.Year, apptDate.DateTime.Month, apptDate.DateTime.Day, dtpEnd.DateTime.Hour, dtpEnd.DateTime.Minute, 0)
    End Sub

    Private Sub DoctorsCombo1_DoctorsValueChanged(sender As Object, e As DoctorsCombo.DoctorsIndexChangedEvent) Handles DoctorsCombo1.DoctorsValueChanged
        txtDrId.Text = e.DrID.ToString()
    End Sub

    Private Sub PatientCombo1_PatientValueChanged(sender As Object, e As PatientCombo.PatientIndexChangedEvent) Handles PatientCombo1.PatientValueChanged
        txtPatientId.Text = e.PatientID.ToString()
        If e.PatientID > 0 AndAlso txtWhats IsNot Nothing Then
            Try
                Dim patientData As New PatientDATA()
                Dim p = patientData.Select_RecordByID(e.PatientID)
                If p IsNot Nothing Then
                    ApplyPatientWhatsFromDatabase(p)
                End If
            Catch
            End Try
        Else
            _whatsBinder.Unbind()
        End If
    End Sub

    Private Async Sub btnWhatsSend_Click(sender As Object, e As EventArgs)
        EnsureWhatsBinderMatchesPatientId(CurrentPatientIdForWhats())
        _whatsBinder.SaveIfDirty()
        RefreshLblWhats()
        Dim number As String = If(lblWhats IsNot Nothing AndAlso lblWhats.Text IsNot Nothing, lblWhats.Text.ToString(), "").Trim()
        If String.IsNullOrWhiteSpace(number) AndAlso _whatsBinder.BoundPatient IsNot Nothing Then
            number = If(WhatsHelper.GetFullWhatsDigitsFromPatient(_whatsBinder.BoundPatient), "").Trim()
        End If
        If String.IsNullOrWhiteSpace(number) Then
            number = WhatsHelper.BuildInternationalWhatsDigitsFromControls(cboPrefix, txtWhats).Trim()
        End If
        If String.IsNullOrWhiteSpace(number) Then
            MessageBox.Show(If(Eng, "Enter patient WhatsApp/phone number.", "أدخل رقم واتساب/هاتف المريض."),
                            If(Eng, "WhatsApp", "واتساب"), MessageBoxButtons.OK, MessageBoxIcon.Warning)
            If txtWhats IsNot Nothing Then txtWhats.Focus()
            Return
        End If
        Dim validationMsg As String = ValidateWhatsAppNumber(number)
        If validationMsg <> "" Then
            MessageBox.Show(validationMsg, If(Eng, "Invalid number", "رقم غير صالح"), MessageBoxButtons.OK, MessageBoxIcon.Warning)
            If txtWhats IsNot Nothing Then txtWhats.Focus()
            Return
        End If
        Dim msg = BuildAppointmentWhatsAppMessage()
        If String.IsNullOrWhiteSpace(msg) Then
            MessageBox.Show(If(Eng, "No appointment data to send.", "لا توجد بيانات موعد لإرسالها."),
                            If(Eng, "WhatsApp", "واتساب"), MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If
        If Not Await WhatsAppService.EnsureWhatsAppConnectedOrNotifyAsync(Me) Then Return
        Dim clinicId As String = WhatsAppService.GetCurrentClinicId()
        _whatsBinder.BeginSuppressAutoSaveWhileModal()
        Try
            Dim waService As New WhatsAppService()
            Await waService.SendMessageAsync(clinicId, number, msg, "", BuildWhatsAppSendContextForEditor())
            'MessageBox.Show("Message queued for sending.", "WhatsApp", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            MessageBox.Show(ex.Message, If(Eng, "WhatsApp", "واتساب"), MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            _whatsBinder.EndSuppressAutoSaveAndSync()
        End Try
    End Sub

    Private Function BuildWhatsAppSendContextForEditor() As WhatsAppSendContext
        Dim pid As Integer? = Nothing
        If PatientCombo1 IsNot Nothing AndAlso PatientCombo1.PatientID > 0 Then pid = PatientCombo1.PatientID
        Dim pname As String = ""
        If PatientCombo1 IsNot Nothing Then
            pname = If(PatientCombo1.PatientName, "")
            If String.IsNullOrWhiteSpace(pname) AndAlso PatientCombo1.CboPatient IsNot Nothing Then
                pname = If(PatientCombo1.CboPatient.Text, "")
            End If
        End If
        Dim apptPart = ""
        If AppointmentC IsNot Nothing AndAlso AppointmentC.AppointmentID > 0 Then
            apptPart = " · Appt#" & AppointmentC.AppointmentID.ToString()
        End If
        Return New WhatsAppSendContext With {
            .Category = WhatsAppMessageCategories.AppointmentEditor,
            .PatientId = pid,
            .DisplayName = If(String.IsNullOrWhiteSpace(pname), Nothing, pname.Trim()),
            .SourceHint = NameOf(AppointCEditorForm) & apptPart,
            .RevealMessageCenter = True
        }
    End Function

    Private Function BuildAppointmentWhatsAppMessage() As String
        Dim patientName As String = If(PatientCombo1 IsNot Nothing, PatientCombo1.CboPatient.Text, "")
        Dim patientSex As String = Nothing
        If PatientCombo1 IsNot Nothing AndAlso PatientCombo1.PatientID > 0 Then
            Try
                Dim pd As New PatientDATA()
                Dim p = pd.Select_RecordByID(PatientCombo1.PatientID)
                If p IsNot Nothing Then patientSex = p.Sex
            Catch
            End Try
        End If
        Dim drName As String = If(DoctorsCombo1 IsNot Nothing, DoctorsCombo1.CboDoctors.Text, "")
        Dim appDt = If(apptDate IsNot Nothing, apptDate.DateTime, DateTime.Now)
        Dim startDt As DateTime? = If(dtpStart IsNot Nothing, CType(dtpStart.EditValue, DateTime), CType(Nothing, DateTime?))
        Dim endDt As DateTime? = If(dtpEnd IsNot Nothing, CType(dtpEnd.EditValue, DateTime), CType(Nothing, DateTime?))
        Dim reason = If(txtReason IsNot Nothing, txtReason.Text, "")
        Dim notes = If(txtNotes IsNot Nothing, txtNotes.Text, "")
        Dim status = If(cboStatus IsNot Nothing, cboStatus.Text, "")
        Dim inclReason = If(chkIncludeReason Is Nothing, True, chkIncludeReason.Checked)
        Dim inclNotes = If(chkIncludeNotes Is Nothing, True, chkIncludeNotes.Checked)
        Return WhatsHelper.BuildAppointmentWhatsAppMessage(patientName, drName, appDt, startDt, endDt, reason, notes, status, useEng, patientSex,
                                                             includeReason:=inclReason, includeNotes:=inclNotes)
    End Function

    Private Sub ApplyReminderLanguageRadioFromAppointment()
        If RadioLang Is Nothing Then Return
        'RadioLang.SelectedIndex = If(Eng, 1, 0)
        'useEng = eng
    End Sub

    Private Sub RadioLang_SelectedIndexChanged(sender As Object, e As EventArgs) Handles RadioLang.SelectedIndexChanged
        If RadioLang.SelectedIndex = 0 Then
            useEng = False
        ElseIf RadioLang.SelectedIndex = 1 Then
            useEng = True
        End If
        RefreshShortLeadReminderCheckCaption()
    End Sub



    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        If AppointmentC Is Nothing OrElse AppointmentC.AppointmentID <= 0 Then Return
        Dim rep As New AppointmentCRepository(_conn.ConnectionString)
        Try
            If rep.Delete(AppointmentC.AppointmentID) > 0 Then
                MsgBox(If(Eng, "Appointment has been deleted.", "تم حذف الموعد."))
                DialogResult = DialogResult.OK
            Else
                MsgBox(If(Eng, "Appointment delete failed.", "فشل حذف الموعد."))
                DialogResult = DialogResult.Cancel
            End If
        Catch ex As Exception
            MessageBox.Show(If(Eng, "Could not delete appointment: ", "تعذر حذف الموعد: ") & ex.Message,
                            If(Eng, "Delete", "حذف"), MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

#Region "Whats"
    ''' <summary>Loads WhatsApp local number and selects saved country prefix in the combo.</summary>
    Private Sub ApplyPatientWhatsFromDatabase(p As Patient)
        If p Is Nothing OrElse txtWhats Is Nothing Then Return
        _whatsBinder.BindToPatient(p, False)
    End Sub

    ''' <summary>Ensures the binder targets the patient row about to be saved or messaged.</summary>
    Private Sub EnsureWhatsBinderMatchesPatientId(patientId As Integer)
        If patientId <= 0 Then Return
        If _whatsBinder.BoundPatient IsNot Nothing AndAlso _whatsBinder.BoundPatient.PatientID = patientId Then Return
        Try
            Dim patientData As New PatientDATA()
            Dim p = patientData.Select_RecordByID(patientId)
            If p IsNot Nothing Then _whatsBinder.BindToPatient(p, False)
        Catch
        End Try
    End Sub

    Private Function CurrentPatientIdForWhats() As Integer
        If AppointmentC IsNot Nothing AndAlso AppointmentC.PatientID > 0 Then Return AppointmentC.PatientID
        If PatientCombo1 IsNot Nothing AndAlso PatientCombo1.PatientID > 0 Then Return PatientCombo1.PatientID
        Return 0
    End Function

    ''' <summary>Updates lblWhats from the bound patient (prefix + local).</summary>
    Private Sub RefreshLblWhats()
        _whatsBinder.RefreshLabel()
    End Sub

    Private Sub cboPrefix_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboPrefix.SelectedIndexChanged
        _whatsBinder.OnCboPrefixSelectedIndexChanged()
    End Sub

    Private Sub cboPrefix_EditValueChanged(sender As Object, e As EventArgs) Handles cboPrefix.EditValueChanged
        _whatsBinder.OnCboPrefixEditValueChanged()
    End Sub

    Private Sub txtWhats_ValueChanged(sender As Object, e As EventArgs) Handles txtWhats.EditValueChanged
        _whatsBinder.OnTxtWhatsEditValueChanged()
    End Sub

    Private Sub txtWhats_Leave(sender As Object, e As EventArgs) Handles txtWhats.Leave
        _whatsBinder.OnTxtWhatsLeave()
    End Sub

    Private Sub txtWhats_Validated(sender As Object, e As EventArgs) Handles txtWhats.Validated
        _whatsBinder.OnTxtWhatsValidated()
    End Sub

    Private Sub dtpStart_EditValueChanged(sender As Object, e As EventArgs) Handles dtpStart.EditValueChanged
        dtpEnd.EditValue = dtpStart.DateTime.AddMinutes(30)
    End Sub


    Private Sub chkWhats_CheckedChanged(sender As Object, e As EventArgs) Handles chkWhats.CheckedChanged
        If chkWhats.Checked Then
            If chk2H IsNot Nothing Then chk2H.Visible = True
            If chk24H IsNot Nothing Then chk24H.Visible = True
            If chk24H IsNot Nothing Then chk24H.Checked = False
            If chk2H IsNot Nothing Then chk2H.Checked = False
        Else
            If chk2H IsNot Nothing Then chk2H.Visible = False
            If chk24H IsNot Nothing Then chk24H.Visible = False
        End If
    End Sub

    Private Sub ApplyWhatsReminderCheckLayout()
        If chkWhats Is Nothing Then Return
        If chkWhats.Checked Then
            If chk24H IsNot Nothing Then chk24H.Visible = True
            If chk2H IsNot Nothing Then chk2H.Visible = True
        Else
            If chk24H IsNot Nothing Then chk24H.Visible = False
            If chk2H IsNot Nothing Then chk2H.Visible = False
        End If
    End Sub

    Private Sub RefreshShortLeadReminderCheckCaption()
        If chk2H Is Nothing Then Return
        chk2H.Properties.Caption = ApptTwoHourReminderQueueRepository.GetShortLeadReminderCheckboxCaption(useEng)
    End Sub

    Private Sub txtWhats_KeyDown(sender As Object, e As KeyEventArgs) Handles txtWhats.KeyDown
        ' Allow control keys: backspace, delete, arrows, tab, home/end, etc.
        If e.KeyCode = Keys.Back OrElse
           e.KeyCode = Keys.Delete OrElse
           e.KeyCode = Keys.Left OrElse
           e.KeyCode = Keys.Right OrElse
           e.KeyCode = Keys.Up OrElse
           e.KeyCode = Keys.Down OrElse
           e.KeyCode = Keys.Tab OrElse
           e.KeyCode = Keys.Home OrElse
           e.KeyCode = Keys.End Then
            Return
        End If

        ' Allow digits only (top row or numpad)
        Dim isTopRowDigit As Boolean = (e.KeyCode >= Keys.D0 AndAlso e.KeyCode <= Keys.D9)
        Dim isNumPadDigit As Boolean = (e.KeyCode >= Keys.NumPad0 AndAlso e.KeyCode <= Keys.NumPad9)

        ' Block Shift-modified digits (to avoid !, @, etc.)
        If (isTopRowDigit OrElse isNumPadDigit) AndAlso (Not e.Shift) Then
            Return
        End If

        ' Otherwise block the key
        e.SuppressKeyPress = True
        e.Handled = True
    End Sub

#End Region

End Class
