Imports System
Imports System.Collections.Concurrent
Imports System.Data.SqlClient
Imports System.Drawing.Drawing2D
Imports System.Linq
Imports System.Text
Imports Dapper
Imports DevExpress.Utils
Imports DevExpress.XtraEditors.Calendar
Imports DevExpress.XtraGrid.Columns
Imports DevExpress.XtraGrid.Views.Base
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraGrid.Views.Grid.ViewInfo

Public Class PatientVisitsCtl
    Implements IPatientAwareUserControl


    Private Sub IPatientAwareUserControl_LoadPatientData(patientId As Integer) Implements IPatientAwareUserControl.LoadPatientData
        LoadPatientData(patientId)
    End Sub
    Private Sub SyncCurrentPatientFromForm(patientId As Integer)
        Dim sp = TryingPatientSession.Instance.GetCurrentPatient()
        If sp IsNot Nothing AndAlso sp.PatientID > 0 AndAlso (patientId <= 0 OrElse sp.PatientID = patientId) Then
            CurrentPatient = sp
            Return
        End If
        Dim parentWs = PatientAwareHelper.FindPatientWorkspace(Me)
        If parentWs IsNot Nothing AndAlso parentWs.Current_Patient IsNot Nothing Then
            Dim wp = parentWs.Current_Patient
            If patientId <= 0 OrElse wp.PatientID = patientId Then
                CurrentPatient = wp
                Return
            End If
        End If
        If CurrentPatient Is Nothing AndAlso FormManager.Instance.IsBasePatientFormOpen Then
            Dim gp = FormManager.Instance.GetCurrentPatient()
            If gp IsNot Nothing AndAlso (patientId <= 0 OrElse gp.PatientID = patientId) Then
                CurrentPatient = gp
            End If
        End If
    End Sub

    Private Function ResolveWorkspacePatientId() As Integer
        If CurrentPatient IsNot Nothing AndAlso CurrentPatient.PatientID > 0 Then Return CurrentPatient.PatientID
        Dim sp = TryingPatientSession.Instance.GetCurrentPatient()
        If sp IsNot Nothing AndAlso sp.PatientID > 0 Then Return sp.PatientID
        Dim ws = PatientAwareHelper.FindPatientWorkspace(Me)
        If ws IsNot Nothing AndAlso ws.Current_Patient IsNot Nothing AndAlso ws.Current_Patient.PatientID > 0 Then
            Return ws.Current_Patient.PatientID
        End If
        If FormManager.Instance.IsBasePatientFormOpen Then
            Dim gp = FormManager.Instance.GetCurrentPatient()
            If gp IsNot Nothing AndAlso gp.PatientID > 0 Then Return gp.PatientID
        End If
        If AppointmentC IsNot Nothing AndAlso AppointmentC.PatientID > 0 Then Return AppointmentC.PatientID
        Return 0
    End Function

    ''' <summary>Sets read-only patient name from <see cref="CurrentPatient"/> or database for the workspace patient.</summary>
    Private Sub ApplyPatientNameDisplayForId(patientId As Integer)
        If txtPatientName Is Nothing Then Return
        If patientId <= 0 Then
            txtPatientName.Text = ""
            Return
        End If
        If CurrentPatient IsNot Nothing AndAlso CurrentPatient.PatientID = patientId AndAlso
           Not String.IsNullOrWhiteSpace(CurrentPatient.PatientName) Then
            txtPatientName.Text = CurrentPatient.PatientName
            Return
        End If
        Try
            Dim patientData As New PatientDATA()
            Dim p = patientData.Select_RecordByID(patientId)
            txtPatientName.Text = If(p IsNot Nothing AndAlso Not String.IsNullOrWhiteSpace(p.PatientName), p.PatientName, "")
        Catch
            txtPatientName.Text = ""
        End Try
    End Sub

    Private Function ResolveEditorPatientId() As Integer
        Dim pid = PatientID
        If pid <= 0 Then pid = ResolveWorkspacePatientId()
        Return pid
    End Function

    Public Sub LoadPatientData(patientId As Integer)
        SyncCurrentPatientFromForm(patientId)
        Dim pid = patientId
        If pid <= 0 Then pid = ResolveWorkspacePatientId()
        If pid <= 0 Then Return
        If pid = _lastLoadedPatientId Then Return
        _lastLoadedPatientId = pid
        LoadValues(pid)
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
    Private Sub PatientVisitsCtl_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        If controlBoundsCache.IsEmpty Then Return
        ResizeControlsProportionally()
    End Sub
#End Region



    Private _currentPatient As Patient
    Friend Property CurrentPatient As Patient
        Get
            Return _currentPatient
        End Get
        Set(value As Patient)
            _currentPatient = value
        End Set
    End Property
    Private ReadOnly Property PatientID As Integer
        Get
            If CurrentPatient IsNot Nothing Then Return CurrentPatient.PatientID
            If AppointmentC IsNot Nothing Then Return AppointmentC.PatientID
            Return 0
        End Get
    End Property
    Private repo As New AppointmentCRepository
    Private _repo As New AppointmentCRepository(DentistXDATA.GetConnection.ConnectionString)
    Private allAppts As List(Of AppointmentC)
    Private displayList As New List(Of AppointmentView)
    Private _isNew As Boolean
    Private _lastLoadedPatientId As Integer = -1
    Private _patientNameCache As New Dictionary(Of Integer, String)()
    Private _doctorNameCache As New Dictionary(Of Integer, String)()
    Public Property Appointment As AppointmentC
    Public Property AppointmentC As AppointmentC

    Private useEng As Boolean
    Private ReadOnly _whatsBinder As PatientWhatsControlsBinder
    Private _apptDateFullDayBehavior As DateEditFullDayNameBehavior

    ''' <summary>Time component = current hour with minutes/seconds 00, on the given calendar date (e.g. 9:37 now → 9:00).</summary>
    Private Shared Function DtpTimeCurrentHourOnDate(onDay As Date) As DateTime
        Dim n = DateTime.Now
        Return New DateTime(onDay.Year, onDay.Month, onDay.Day, n.Hour, 0, 0)
    End Function

    Public Sub New()
        InitializeComponent()
        _whatsBinder = New PatientWhatsControlsBinder(cboPrefix, txtWhats, lblWhats, Me)
        FillCboPrefixOnce(cboPrefix)
        StoreOriginalBounds(Me)
        DateEditHelper.ApplyFullDayNameCalendar(apptDate)
        _apptDateFullDayBehavior = New DateEditFullDayNameBehavior(apptDate)
        apptDate.DateTime = Date.Now
    End Sub

    Public Sub New(appt As AppointmentC, Optional isNew As Boolean = False)
        Me.AppointmentC = appt
        Me._isNew = isNew
        InitializeComponent()
        _whatsBinder = New PatientWhatsControlsBinder(cboPrefix, txtWhats, lblWhats, Me)
        FillCboPrefixOnce(cboPrefix)
        StoreOriginalBounds(Me)
        apptDate.DateTime = Date.Now
        DateEditHelper.ApplyFullDayNameCalendar(apptDate)
        _apptDateFullDayBehavior = New DateEditFullDayNameBehavior(apptDate)
        LoadValues(PatientID)
    End Sub

    Private Sub PatientVisitsCtl_Load(sender As Object, e As EventArgs) Handles Me.Load
        FillCboPrefixOnce(cboPrefix)
        apptDate.DateTime = Date.Now
        dtpStart.DateTime = DtpTimeCurrentHourOnDate(CDate(apptDate.EditValue).Date)
        dtpEnd.DateTime = dtpStart.DateTime.AddMinutes(30)
        Dim pid = PatientID
        If pid <= 0 Then pid = ResolveWorkspacePatientId()
        If pid > 0 AndAlso pid <> _lastLoadedPatientId Then
            _lastLoadedPatientId = pid
            LoadValues(pid)
        End If
    End Sub

    Private Sub slctdDate_CustomDrawDayNumberCell(sender As Object, e As CustomDrawDayNumberCellEventArgs) Handles slctdDate.CustomDrawDayNumberCell

        Dim dayAppointments As List(Of AppointmentC)
        '' Find all appointments for this date
        'If chkAllAppts.Checked = False Then
        '    dayAppointments = allAppts
        'Else
        '    dayAppointments = allAppts.Where(Function(a) a.AppDate.Date = e.Date.Date).ToList()
        'End If
        If allAppts Is Nothing Then Return
        dayAppointments = allAppts.Where(Function(a) a.AppDate.Date = e.Date.Date).ToList()
        If dayAppointments.Any() Then
            e.Handled = True

            ' Pick the color based on statuses
            Dim statusColors As New Dictionary(Of String, Color) From {
            {"Pending", Color.LightGoldenrodYellow},
            {"Running", Color.LightSkyBlue},
            {"Completed", Color.LightGreen},
            {"Canceled", Color.LightCoral},
            {"Postponed", Color.LightGray}
        }

            ' Default color if status not matched
            Dim fillColor As Color = Color.LightBlue

            ' If multiple appointments with different statuses, choose most critical one
            Dim priorityOrder = {"Canceled", "Postponed", "Pending", "Running", "Completed"}
            For Each status In priorityOrder
                If dayAppointments.Any(Function(a) a.Status = status) Then
                    fillColor = statusColors(status)
                    Exit For
                End If
            Next

            ' Draw background
            e.Cache.FillRectangle(New SolidBrush(fillColor), e.Bounds)

            ' Draw border (optional)
            e.Cache.DrawRectangle(Pens.DimGray, e.Bounds)

            '' Draw date text centered
            'Dim sf As New StringFormat With {.Alignment = StringAlignment.Center, .LineAlignment = StringAlignment.Center}
            'e.Cache.DrawString(e.Date.Day.ToString(), e.Style.Font, Brushes.Black, e.Bounds, sf)

            ' Highlight selected day
            Dim isSelected = e.State.HasFlag(DevExpress.Utils.Drawing.ObjectState.Selected)   '(CalendarElementStat.Selected)

            ''Old Code
            'Dim borderPen As Pen = If(isSelected, Pens.Black, Pens.DimGray)

            '' Fill background and border
            'e.Cache.FillRectangle(New SolidBrush(fillColor), e.Bounds)
            'e.Cache.DrawRectangle(borderPen, e.Bounds)

            '' Draw day number
            'Dim sf As New StringFormat With {.Alignment = StringAlignment.Center, .LineAlignment = StringAlignment.Center}
            'e.Cache.DrawString(e.Date.Day.ToString(), e.Style.Font, Brushes.Black, e.Bounds, sf)

            '' Add small red dot if multiple appointments
            'If dayAppointments.Count > 1 Then
            '    Dim ellipseRect As New Rectangle(e.Bounds.Right - 10, e.Bounds.Bottom - 10, 6, 6)
            '    e.Cache.FillEllipse(Brushes.Red, ellipseRect)
            'End If
            ''Old Code
            ' Begin drawing
            e.Handled = True

            ' Background
            Using b As New SolidBrush(fillColor)
                e.Cache.FillEllipse(b, e.Bounds)
            End Using

            ' If selected — add glowing effect
            If isSelected Then
                ' Use gradient glow around border
                Dim glowColor As Color = Color.FromArgb(180, Color.Gold)
                Dim path As New GraphicsPath()
                path.AddEllipse(e.Bounds)
                Using lgBrush As New PathGradientBrush(path)
                    lgBrush.CenterColor = glowColor
                    lgBrush.SurroundColors = {Color.Transparent}
                    e.Cache.FillRectangle(lgBrush, e.Bounds)
                End Using

                ' Add a strong border
                Using pen As New Pen(Color.DarkGoldenrod, 2)
                    e.Cache.DrawRectangle(pen, e.Bounds)
                End Using
            Else
                ' Normal border for unselected days
                Using pen As New Pen(Color.Gray, 1)
                    e.Cache.DrawRectangle(pen, e.Bounds)
                End Using
            End If

            ' Draw day number centered
            Dim sf As New StringFormat With {.Alignment = StringAlignment.Center, .LineAlignment = StringAlignment.Center}
            e.Cache.DrawString(e.Date.Day.ToString(), e.Style.Font, Brushes.Black, e.Bounds, sf)

            ' Add a red dot if multiple appointments
            If dayAppointments.Count > 1 Then
                Dim ellipseRect As New Rectangle(e.Bounds.Right - 10, e.Bounds.Bottom - 10, 6, 6)
                e.Cache.FillEllipse(Brushes.Red, ellipseRect)
            End If
        End If
    End Sub
    Private Sub slctdDate_SelectionChanged(sender As Object, e As EventArgs) Handles slctdDate.SelectionChanged
        apptDate.DateTime = slctdDate.SelectedRanges.Start.Date
        If allAppts Is Nothing Then Return
        Dim selectedDate = slctdDate.SelectedRanges.Start.Date
        Dim selectedAppointments = allAppts.Where(Function(a) a.AppDate.Date = selectedDate).ToList()
        EnsureNameCaches(selectedAppointments)
        displayList = BuildDisplayList(selectedAppointments)
        gridResults.DataSource = displayList
    End Sub
    Private Sub LoadValues(patientId As Integer)
        Try
            If patientId > 0 Then
                If CurrentPatient IsNot Nothing AndAlso CurrentPatient.PatientID = patientId AndAlso
                   Not String.IsNullOrEmpty(CurrentPatient.WhatsApp) Then
                    _whatsBinder.BindToPatient(CurrentPatient, False)
                Else
                    Dim patientData As New PatientDATA()
                    Dim p = patientData.Select_RecordByID(patientId)
                    If p IsNot Nothing Then
                        ApplyPatientWhatsFromDatabase(p)
                    End If
                End If
            Else
                _whatsBinder.Unbind()
            End If
        Catch
        End Try
        LoadAppointments(patientId)
        If _isNew Then
            Me.Text = If(Eng, "Add Appointment", "إضافة موعد")
            If AppointmentC Is Nothing Then AppointmentC = New AppointmentC With {.AppDate = DateTime.Now, .StartDateTime = DateTime.Now, .EndDateTime = DateTime.Now.AddMinutes(30)}
            'txtPatientId.Text = AppointmentC.PatientID.ToString()
            'txtDrId.Text = AppointmentC.DrID.ToString()
            'apptDate.DateTime = AppointmentC.AppDate 'Date.Now '
            ApplyPatientNameDisplayForId(AppointmentC.PatientID)
            DoctorsCombo1.SetSelectedDrName(AppointmentC.DrID)
            apptDate.EditValue = AppointmentC.AppDate
            Dim startFloor = DtpTimeCurrentHourOnDate(CDate(apptDate.EditValue).Date)
            dtpStart.EditValue = startFloor
            dtpEnd.EditValue = startFloor.AddMinutes(30)
            AppointmentC.StartDateTime = startFloor
            AppointmentC.EndDateTime = startFloor.AddMinutes(30)
            txtReason.Text = AppointmentC.Reason
            txtNotes.Text = AppointmentC.Notes
        Else
            Me.Text = If(Eng, "Edit Appointment", "تعديل موعد")
            ApplyPatientNameDisplayForId(patientId)
            'DoctorsCombo1.SetSelectedDrName(AppointmentC.DrID)
            'apptDate.DateTime = AppointmentC.AppDate
            'dtpStart.EditValue = AppointmentC.StartDateTime
            'dtpEnd.EditValue = AppointmentC.EndDateTime
            'txtReason.Text = AppointmentC.Reason
            'txtNotes.Text = AppointmentC.Notes

        End If

        'BindWhatsAppFromPatientContext(patientId)
        ApplyReminderLanguageRadioFromAppointmentC()
        RefreshShortLeadReminderCheckCaption()
    End Sub

    ''' <summary>Loads country prefixes and patient WhatsApp fields after patient row is bound.</summary>
    Private Sub BindWhatsAppFromPatientContext(patientId As Integer)
        FillCboPrefixOnce(cboPrefix)
        Dim whatsPatientId As Integer = patientId
        If whatsPatientId <= 0 AndAlso CurrentPatient IsNot Nothing Then
            whatsPatientId = CurrentPatient.PatientID
        End If
        If whatsPatientId <= 0 Then whatsPatientId = ResolveWorkspacePatientId()
        If whatsPatientId > 0 AndAlso txtWhats IsNot Nothing Then
            Try
                Dim patientData As New PatientDATA()
                Dim p = patientData.Select_RecordByID(whatsPatientId)
                If p IsNot Nothing Then
                    ApplyPatientWhatsFromDatabase(p)
                End If
            Catch
            End Try
        End If
    End Sub

    Private Sub EnsureNameCaches(appointments As List(Of AppointmentC))
        If appointments Is Nothing OrElse appointments.Count = 0 Then Return
        Dim missingPids = appointments.Select(Function(a) a.PatientID).Distinct().Where(Function(id) id > 0 AndAlso Not _patientNameCache.ContainsKey(id)).ToArray()
        Dim missingDids = appointments.Select(Function(a) a.DrID).Distinct().Where(Function(id) id > 0 AndAlso Not _doctorNameCache.ContainsKey(id)).ToArray()
        Dim connStr = DentistXDATA.GetConnection.ConnectionString
        If missingPids.Length > 0 Then
            Try
                Using conn As New SqlConnection(connStr)
                    For Each row In conn.Query("SELECT PatientID, PatientName FROM Patient WHERE PatientID IN @Ids", New With {.Ids = missingPids})
                        _patientNameCache(CInt(row.PatientID)) = CStr(If(row.PatientName, ""))
                    Next
                End Using
            Catch
            End Try
        End If
        If missingDids.Length > 0 Then
            Try
                Using conn As New SqlConnection(connStr)
                    For Each row In conn.Query("SELECT DrID, DrName FROM Doctors WHERE DrID IN @Ids", New With {.Ids = missingDids})
                        _doctorNameCache(CInt(row.DrID)) = CStr(If(row.DrName, ""))
                    Next
                End Using
            Catch
            End Try
        End If
    End Sub

    Private Function CachedPatientName(id As Integer) As String
        Dim name As String = Nothing
        If _patientNameCache.TryGetValue(id, name) Then Return name
        Return ""
    End Function

    Private Function CachedDoctorName(id As Integer) As String
        Dim name As String = Nothing
        If _doctorNameCache.TryGetValue(id, name) Then Return name
        Return ""
    End Function

    Private Function BuildDisplayList(appointments As List(Of AppointmentC)) As List(Of AppointmentView)
        Return (From appt In appointments
                Select New AppointmentView With {
                    .AppID = appt.AppointmentID,
                    .ApptDate = AppointDateFormat.FormatDate(appt.AppDate),
                    .FromTo = $"{appt.StartDateTime:HH:mm} - {appt.EndDateTime:HH:mm}",
                    .PatientName = CachedPatientName(appt.PatientID),
                    .DoctorName = CachedDoctorName(appt.DrID),
                    .Detail = $"{appt.Reason}/{appt.Notes}",
                    .Status = appt.Status
                }).ToList()
    End Function

    Private Sub LoadAppointments(patientId As Integer)
        allAppts = _repo.GetByPatientId(patientId).ToList()
        EnsureNameCaches(allAppts)
        displayList = BuildDisplayList(allAppts)
        If chkAllAppts.Checked = False Then
            displayList = displayList.Where(Function(a) Date.Parse(a.ApptDate).Date = Date.Today).ToList()
        End If
        gridResults.DataSource = displayList
        StyleGridView()
    End Sub

    Private ReadOnly _conn As SqlConnection = DentistXDATA.GetConnection '.ConnectionString





    Private Async Sub BtnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Dim pId As Integer
        Dim newApptId As Integer = 0
        Try
            Using conn As SqlConnection = DentistXDATA.GetConnection
                conn.Open()
                Using trans As SqlTransaction = conn.BeginTransaction(System.Data.IsolationLevel.Serializable)
                    ' validate
                    Dim drId As Integer
                    pId = ResolveEditorPatientId()
                    If pId <= 0 Then MessageBox.Show(If(Eng, "Patient is required.", "المريض مطلوب."), If(Eng, "Save", "حفظ"), MessageBoxButtons.OK, MessageBoxIcon.Warning) : Return
                    drId = DoctorsCombo1.DrID
                    If drId <= 0 Then MessageBox.Show(If(Eng, "Doctor is required.", "الطبيب مطلوب."), If(Eng, "Save", "حفظ"), MessageBoxButtons.OK, MessageBoxIcon.Warning) : Return
                    If dtpEnd.EditValue <= dtpStart.EditValue Then MessageBox.Show(If(Eng, "End time must be after start time.", "يجب أن يكون وقت الانتهاء بعد وقت البدء."), If(Eng, "Save", "حفظ"), MessageBoxButtons.OK, MessageBoxIcon.Warning) : Return
                    Dim appDateOnly = apptDate.DateTime.Date
                    Dim startDt As DateTime = New DateTime(appDateOnly.Year, appDateOnly.Month, appDateOnly.Day, dtpStart.DateTime.Hour, dtpStart.DateTime.Minute, 0)
                    Dim endDt As DateTime = New DateTime(appDateOnly.Year, appDateOnly.Month, appDateOnly.Day, dtpEnd.DateTime.Hour, dtpEnd.DateTime.Minute, 0)
                    AppointmentC = New AppointmentC With {
                    .AppDate = appDateOnly,
                    .StartDateTime = startDt,
                    .EndDateTime = endDt,
                    .PatientID = pId,
                    .DrID = drId,
                    .Reason = txtReason.Text,
                    .Notes = txtNotes.Text,
                    .WhatsIncludeReason = If(chkIncludeReason Is Nothing, True, chkIncludeReason.Checked),
                    .WhatsIncludeNotes = If(chkIncludeNotes Is Nothing, True, chkIncludeNotes.Checked),
                    .Status = If(cboStatus IsNot Nothing AndAlso Not String.IsNullOrWhiteSpace(cboStatus.Text), cboStatus.Text, "Pending"),
                    .CreatedBy = CurrentUser.UsName,
                    .CreatedAt = DateTime.Now
                        }

                    Me.Appointment = AppointmentC

                    Dim overlappingAppointments = repo.GetOverlappingAppointmentsTransactional(conn, trans, AppointmentC.DrID, AppointmentC.PatientID, AppointmentC.StartDateTime, AppointmentC.EndDateTime)

                    If overlappingAppointments.Any() Then
                        Dim message As New StringBuilder()
                        message.AppendLine(If(Eng, "Appointment overlaps detected:", "تم اكتشاف تداخل في المواعيد:"))
                        message.AppendLine()

                        For Each overlap In overlappingAppointments
                            message.AppendLine($"• {overlap}")
                        Next

                        message.AppendLine()
                        message.AppendLine(If(Eng, "Please choose a different time or doctor.", "يرجى اختيار وقت مختلف أو طبيب آخر."))

                        MessageBox.Show(message.ToString(), If(Eng, "Overlap Detected", "تم اكتشاف تداخل"),
                    MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        trans.Rollback()
                        Exit Sub
                    End If

                    newApptId = repo.InsertTransactional(conn, trans, AppointmentC)
                    AppointmentC.AppointmentID = newApptId
                    trans.Commit()
                End Using
            End Using

            ' Same as AppointCEditorForm after Insert: upsert dbo.ApptTwoHourWhatsAppQueue when user checked reminder types. Must run after commit
            ' because SyncFromAppointmentId reads AppointmentC on a new connection (InsertTransactional does not call Sync).
            If newApptId > 0 Then
                Dim wantScheduledReminders = chkWhats IsNot Nothing AndAlso chkWhats.Checked AndAlso
                    chk24H IsNot Nothing AndAlso chk2H IsNot Nothing AndAlso
                    (chk24H.Checked OrElse chk2H.Checked)
                If wantScheduledReminders Then
                    AppointmentWhatsAppQueueService.NotifyAppointmentSaved(newApptId, useEng, chk24H.Checked, chk2H.Checked)
                Else
                    ApptTwoHourReminderQueueRepository.DeleteByAppointmentId(newApptId)
                End If
            Else
                MessageBox.Show(If(Eng, "Could not save appointment (no id returned).", "تعذر حفظ الموعد (لم يتم إرجاع رقم الموعد)."),
                                If(Eng, "Save Failed", "فشل الحفظ"), MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return
            End If

            Dim wePersistedHere As Boolean = True
            Dim sendWhats As Boolean = chkWhats IsNot Nothing AndAlso chkWhats.Checked
            If sendWhats AndAlso wePersistedHere Then
                EnsureWhatsBinderMatchesPatientId(pId)
                _whatsBinder.SaveIfDirty()
                RefreshLblWhats()
                Dim number As String = If(lblWhats IsNot Nothing AndAlso lblWhats.Text IsNot Nothing, lblWhats.Text.ToString(), "").Trim()
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
                                Await waService.SendMessageAsync(clinicId, number, msg, "", BuildWhatsAppSendContextForVisits())
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

            If wePersistedHere AndAlso pId > 0 Then
                EnsureWhatsBinderMatchesPatientId(pId)
                _whatsBinder.SaveIfDirty()
            End If

            LoadAppointments(pId)
            txtReason.ResetText()
            txtNotes.ResetText()
            apptDate.DateTime = DateTime.Today
            dtpStart.DateTime = DtpTimeCurrentHourOnDate(DateTime.Today)
            dtpEnd.DateTime = dtpStart.DateTime.AddMinutes(30)
            cboStatus.SelectedIndex = 0
        Catch ex As Exception
            MsgBox(If(Eng, "Error saving appointment: ", "حدث خطأ أثناء حفظ الموعد: ") & ex.Message)
        End Try

    End Sub


    Private Sub apptDate_EditValueChanged(sender As Object, e As EventArgs) Handles apptDate.EditValueChanged
        dtpStart.EditValue = New DateTime(apptDate.DateTime.Year, apptDate.DateTime.Month, apptDate.DateTime.Day, dtpStart.DateTime.Hour, dtpStart.DateTime.Minute, 0)
        dtpEnd.EditValue = New DateTime(apptDate.DateTime.Year, apptDate.DateTime.Month, apptDate.DateTime.Day, dtpEnd.DateTime.Hour, dtpEnd.DateTime.Minute, 0)
    End Sub

    Private Sub DoctorsCombo1_DoctorsValueChanged(sender As Object, e As DoctorsCombo.DoctorsIndexChangedEvent) Handles DoctorsCombo1.DoctorsValueChanged
        txtDrId.Text = e.DrID.ToString()
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
        If Me.AppointmentC IsNot Nothing Then
            Dim rep As New AppointmentCRepository(_conn.ConnectionString)
            'convert Me.AppointmentC to AppointmentView for display
            Dim View As GridView = dgView
            Dim rowHandle As Integer = View.LocateByValue("AppID", Me.AppointmentC.AppointmentID)
            If rowHandle < 0 Then
                MessageBox.Show(If(Eng, "Appointment not found in the list.", "الموعد غير موجود في القائمة."),
                                If(Eng, "Delete Appointment", "حذف موعد"), MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If
            Dim appt As AppointmentView = TryCast(View.GetRow(rowHandle), AppointmentView)
            If appt Is Nothing Then
                MessageBox.Show(If(Eng, "Appointment not found in the list.", "الموعد غير موجود في القائمة."),
                                If(Eng, "Delete Appointment", "حذف موعد"), MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If
            'show a yes/no messagebox to confirm delete
            Dim result = MessageBox.Show(If(Eng, $"Are you sure you want to delete the following appointment?", $"هل أنت متأكد أنك تريد حذف الموعد التالي؟") & vbCrLf & vbCrLf &
                                If(Eng, $"Patient: {appt.PatientName}", $"المريض: {appt.PatientName}") & vbCrLf &
                                If(Eng, $"Doctor: {appt.DoctorName}", $"الطبيب: {appt.DoctorName}") & vbCrLf &
                                If(Eng, $"Date: {appt.ApptDate}", $"التاريخ: {appt.ApptDate}") & vbCrLf &
                                If(Eng, $"Time: {appt.FromTo}", $"الوقت: {appt.FromTo}") & vbCrLf &
                                If(Eng, $"Reason: {appt.Detail}", $"السبب: {appt.Detail}") & vbCrLf &
                                If(Eng, $"Status: {appt.Status}", $"الحالة: {appt.Status}"),
                                If(Eng, "Confirm Delete", "تأكيد الحذف"), MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
            If result = DialogResult.No Then Return

            Try
                If rep.Delete(Me.AppointmentC.AppointmentID) > 0 Then
                    MsgBox(If(Eng, "Appointment has been deleted.", "تم حذف الموعد."))
                    Dim pid = AppointmentC.PatientID
                    LoadAppointments(pid)
                Else
                    MsgBox(If(Eng, "Appointment delete failed.", "فشل حذف الموعد."))
                End If
            Catch ex As Exception
                MessageBox.Show(If(Eng, "Could not delete appointment: ", "تعذر حذف الموعد: ") & ex.Message,
                                If(Eng, "Delete Appointment", "حذف موعد"), MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If
    End Sub

    Private Sub dtpStart_EditValueChanged(sender As Object, e As EventArgs) Handles dtpStart.EditValueChanged
        dtpEnd.EditValue = dtpStart.DateTime.AddMinutes(30)
    End Sub

    ' Lightweight view model matching grid columns
    Private Class AppointmentView
        Public Property AppID As Integer
        Public Property ApptDate As String
        Public Property FromTo As String
        Public Property PatientName As String
        Public Property DoctorName As String
        Public Property Detail As String
        Public Property Status As String
    End Class


    Private Sub StyleGridView()
        ' Alternating row colors
        dgView.OptionsView.EnableAppearanceEvenRow = True
        dgView.OptionsView.EnableAppearanceOddRow = True
        dgView.Appearance.EvenRow.BackColor = Color.FromArgb(230, 240, 255) ' 🔵 light blue
        dgView.Appearance.OddRow.BackColor = Color.FromArgb(255, 250, 230)  ' 🟠 light orange
        dgView.Appearance.Row.Font = New Font("Calibri", 10, FontStyle.Regular)

        ' Colorful headers 🟢🔵🟣🟠
        Dim headerColors() As Color = {
            Color.MediumSeaGreen, '🟢
            Color.DodgerBlue,     '🔵
            Color.MediumPurple,   '🟣
            Color.Orange          '🟠
        }

        Dim i As Integer = 0
        For Each col As GridColumn In dgView.Columns
            col.AppearanceHeader.BackColor = headerColors(i Mod headerColors.Length)
            col.AppearanceHeader.ForeColor = Color.White
            col.AppearanceHeader.Font = New Font("Calibri", 10, FontStyle.Bold)
            col.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center
            i += 1
        Next

        dgView.OptionsBehavior.Editable = False
        dgView.OptionsView.ShowGroupPanel = False
        dgView.OptionsView.ShowIndicator = False
    End Sub

    Private Sub FilterGrid(sender As Object, e As EventArgs) Handles txtPatientName.TextChanged, DoctorsCombo1.TextChanged
        If displayList Is Nothing Then Return

        Dim patientFilter As String = If(txtPatientName IsNot Nothing AndAlso txtPatientName.Text IsNot Nothing, txtPatientName.Text.Trim().ToLower(), "")
        Dim doctorFilter As String = DoctorsCombo1.Text.Trim().ToLower()
        Dim todayOnly As Boolean = Not chkAllAppts.Checked

        Dim filtered = displayList.Where(Function(a)
                                             Dim match = True
                                             If patientFilter <> "" Then
                                                 match = match AndAlso a.PatientName.ToLower().Contains(patientFilter)
                                             End If
                                             If doctorFilter <> "" Then
                                                 match = match AndAlso a.DoctorName.ToLower().Contains(doctorFilter)
                                             End If
                                             If todayOnly Then
                                                 match = match AndAlso (Date.Parse(a.ApptDate).Date = Date.Today)
                                             End If
                                             Return match
                                         End Function).ToList()

        gridResults.DataSource = filtered
    End Sub

    Private Sub dgView_DoubleClick(sender As Object, e As EventArgs) Handles dgView.DoubleClick
        Dim view As GridView = CType(sender, GridView)
        Dim hitInfo As GridHitInfo = view.CalcHitInfo(view.GridControl.PointToClient(Control.MousePosition))
        If hitInfo.InRow OrElse hitInfo.InRowCell Then
            Dim rowHandle As Integer = hitInfo.RowHandle
            If rowHandle >= 0 Then
                Dim appt As AppointmentView = TryCast(view.GetRow(rowHandle), AppointmentView)
                If appt Is Nothing Then Return
                'MessageBox.Show($"Patient: {appt.PatientName}" & vbCrLf &
                '                $"Doctor: {appt.DoctorName}" & vbCrLf &
                '                $"Date: {appt.ApptDate}" & vbCrLf &
                '                $"Time: {appt.FromTo}" & vbCrLf &
                '                $"Reason: {appt.Detail}",
                '                "Appointment Details", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Dim apptc As AppointmentC = Nothing
                If allAppts IsNot Nothing Then
                    apptc = allAppts.FirstOrDefault(Function(a) a.AppointmentID = appt.AppID)
                End If
                If apptc Is Nothing Then Return
                Dim editor As New AppointCEditorForm(apptc, False)
                editor.ShowDialog(Me)
                LoadAppointments(apptc.PatientID)
            End If
        End If
    End Sub


    Private Sub dgView_CustomUnboundColumnData(sender As Object, e As CustomColumnDataEventArgs) Handles dgView.CustomUnboundColumnData
        If e.Column.Name = "colRowNum" Then
            e.Value = e.ListSourceRowIndex + 1
        End If
    End Sub
    Private Sub dgView_RowCellStyle(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs) Handles dgView.RowCellStyle
        If e.Column.FieldName = "Status" OrElse e.Column.Name = "colStatus" Then
            Dim view = TryCast(sender, DevExpress.XtraGrid.Views.Grid.GridView)
            If view Is Nothing Then Exit Sub

            Dim statObj = view.GetRowCellValue(e.RowHandle, "Status")
            If statObj IsNot Nothing AndAlso Not IsDBNull(statObj) Then
                Dim appStatus As String = CStr(statObj)

                ' Pick the color based on statuses
                Dim statusColors As New Dictionary(Of String, Color) From {
                                                    {"Pending", Color.LightGoldenrodYellow},
                                                    {"Running", Color.LightSkyBlue},
                                                    {"Completed", Color.LightGreen},
                                                    {"Canceled", Color.LightCoral},
                                                    {"Postponed", Color.LightGray}}
                ' Apply color if found
                If statusColors.ContainsKey(appStatus) Then
                    e.Appearance.BackColor = statusColors(appStatus)
                    e.Appearance.ForeColor = Color.Black
                Else
                    ' default color if unknown status
                    e.Appearance.BackColor = Color.White
                    e.Appearance.ForeColor = Color.Black
                End If
            End If
        End If
    End Sub

    Private Sub slctdDate_DateTimeChanged(sender As Object, e As EventArgs) Handles slctdDate.DateTimeChanged
        apptDate.DateTime = slctdDate.DateTime
    End Sub

    Private Sub chkAllAppts_CheckedChanged(sender As Object, e As EventArgs) Handles chkAllAppts.CheckedChanged
        Dim apptPid = If(CurrentPatient IsNot Nothing AndAlso CurrentPatient.PatientID > 0, CurrentPatient.PatientID, ResolveWorkspacePatientId())
        LoadAppointments(apptPid)
        If Not chkAllAppts.Checked Then
            chkAllAppts.Text = If(Eng, "Showing All Appointments", "عرض كل المواعيد")
            'slctdDate.Enabled = False
        Else
            chkAllAppts.Text = If(Eng, "Showing Today's Appointments", "عرض مواعيد اليوم")
            'slctdDate.Enabled = True
        End If
    End Sub


    Private Sub dgView_RowClick(sender As Object, e As RowClickEventArgs) Handles dgView.RowClick
        'convert the clicked row to AppointmentView and set AppointmentC
        Dim view As GridView = CType(sender, GridView)
        Dim apptView As AppointmentView = TryCast(view.GetRow(e.RowHandle), AppointmentView)
        If apptView Is Nothing OrElse allAppts Is Nothing Then Return
        Me.AppointmentC = allAppts.FirstOrDefault(Function(a) a.AppointmentID = apptView.AppID)
        ApplyReminderLanguageRadioFromAppointmentC()
    End Sub

    Private Sub ApplyReminderLanguageRadioFromAppointmentC()
        If RadioLang Is Nothing Then Return
        'RadioLang.SelectedIndex = If(Eng, 1, 0)
        'useEng = Eng
    End Sub

#Region "WhatsApp (aligned with AppointCEditorForm)"

    Private Sub RefreshShortLeadReminderCheckCaption()
        If chk2H Is Nothing Then Return
        chk2H.Properties.Caption = ApptTwoHourReminderQueueRepository.GetShortLeadReminderCheckboxCaption(useEng)
    End Sub

    Private Function ValidateWhatsAppNumber(phone As String) As String
        Return WhatsHelper.ValidateWhatsAppNumberEnglish(phone)
    End Function

    Private Function BuildWhatsAppSendContextForVisits() As WhatsAppSendContext
        Dim pidResolved = ResolveEditorPatientId()
        Dim pid As Integer? = If(pidResolved > 0, pidResolved, Nothing)
        Dim pname As String = If(txtPatientName IsNot Nothing AndAlso txtPatientName.Text IsNot Nothing, txtPatientName.Text.Trim(), "")
        If String.IsNullOrWhiteSpace(pname) AndAlso CurrentPatient IsNot Nothing AndAlso
           (Not pid.HasValue OrElse CurrentPatient.PatientID = pid.Value) Then
            pname = If(CurrentPatient.PatientName, "").Trim()
        End If
        Dim apptPart = ""
        If AppointmentC IsNot Nothing AndAlso AppointmentC.AppointmentID > 0 Then
            apptPart = " · Appt#" & AppointmentC.AppointmentID.ToString()
        End If
        Return New WhatsAppSendContext With {
            .Category = WhatsAppMessageCategories.PatientVisits,
            .PatientId = pid,
            .DisplayName = If(String.IsNullOrWhiteSpace(pname), Nothing, pname.Trim()),
            .SourceHint = NameOf(PatientVisitsCtl) & apptPart,
            .RevealMessageCenter = True
        }
    End Function

    Private Function BuildAppointmentWhatsAppMessage() As String
        Dim patientName As String = If(txtPatientName IsNot Nothing AndAlso txtPatientName.Text IsNot Nothing, txtPatientName.Text.Trim(), "")
        If String.IsNullOrWhiteSpace(patientName) AndAlso CurrentPatient IsNot Nothing Then patientName = If(CurrentPatient.PatientName, "").Trim()
        Dim drName As String = If(DoctorsCombo1 IsNot Nothing AndAlso DoctorsCombo1.CboDoctors IsNot Nothing, DoctorsCombo1.CboDoctors.Text, "")
        Dim appDt = If(apptDate IsNot Nothing, apptDate.DateTime, DateTime.Now)
        Dim startCombined As DateTime? = Nothing
        Dim endCombined As DateTime? = Nothing
        If dtpStart IsNot Nothing AndAlso dtpEnd IsNot Nothing AndAlso apptDate IsNot Nothing Then
            Dim d = apptDate.DateTime.Date
            startCombined = New DateTime(d.Year, d.Month, d.Day, dtpStart.DateTime.Hour, dtpStart.DateTime.Minute, 0)
            endCombined = New DateTime(d.Year, d.Month, d.Day, dtpEnd.DateTime.Hour, dtpEnd.DateTime.Minute, 0)
        End If
        Dim reason = If(txtReason IsNot Nothing, txtReason.Text, "")
        Dim notes = If(txtNotes IsNot Nothing, txtNotes.Text, "")
        Dim status = If(cboStatus IsNot Nothing, cboStatus.Text, "")
        Dim patientSex As String = If(CurrentPatient IsNot Nothing, CurrentPatient.Sex, Nothing)
        Dim pidForSex = ResolveEditorPatientId()
        If String.IsNullOrWhiteSpace(patientSex) AndAlso pidForSex > 0 Then
            Try
                Dim pd As New PatientDATA()
                Dim p = pd.Select_RecordByID(pidForSex)
                If p IsNot Nothing Then patientSex = p.Sex
            Catch
            End Try
        End If
        Dim inclReason = If(chkIncludeReason Is Nothing, True, chkIncludeReason.Checked)
        Dim inclNotes = If(chkIncludeNotes Is Nothing, True, chkIncludeNotes.Checked)
        Return WhatsHelper.BuildAppointmentWhatsAppMessage(patientName, drName, appDt, startCombined, endCombined, reason, notes, status, useEng, patientSex,
                                                             includeReason:=inclReason, includeNotes:=inclNotes)
    End Function

    Private Sub ApplyPatientWhatsFromDatabase(p As Patient)
        If p Is Nothing OrElse txtWhats Is Nothing Then Return
        If CurrentPatient IsNot Nothing AndAlso CurrentPatient.PatientID = p.PatientID Then
            CurrentPatient.WhatsApp = p.WhatsApp
            CurrentPatient.WhatsAppPrefix = p.WhatsAppPrefix
            CurrentPatient.Phone = p.Phone
            _whatsBinder.BindToPatient(CurrentPatient, False)
        Else
            _whatsBinder.BindToPatient(p, False)
        End If
    End Sub

    Private Sub EnsureWhatsBinderMatchesPatientId(patientId As Integer)
        If patientId <= 0 Then Return
        If _whatsBinder.BoundPatient IsNot Nothing AndAlso _whatsBinder.BoundPatient.PatientID = patientId Then Return
        Try
            Dim patientData As New PatientDATA()
            Dim p = patientData.Select_RecordByID(patientId)
            If p Is Nothing Then Return
            ApplyPatientWhatsFromDatabase(p)
        Catch
        End Try
    End Sub

    Private Sub RefreshLblWhats()
        _whatsBinder.RefreshLabel()
    End Sub

    Private Sub cboPrefix_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboPrefix.SelectedIndexChanged
        _whatsBinder.OnCboPrefixSelectedIndexChanged()
    End Sub

    Private Sub cboPrefix_EditValueChanged(sender As Object, e As EventArgs) Handles cboPrefix.EditValueChanged
        _whatsBinder.OnCboPrefixEditValueChanged()
    End Sub

    Private Sub txtWhats_EditValueChanged(sender As Object, e As EventArgs) Handles txtWhats.EditValueChanged
        _whatsBinder.OnTxtWhatsEditValueChanged()
    End Sub

    Private Sub txtWhats_Leave(sender As Object, e As EventArgs) Handles txtWhats.Leave
        _whatsBinder.OnTxtWhatsLeave()
    End Sub

    Private Sub txtWhats_Validated(sender As Object, e As EventArgs) Handles txtWhats.Validated
        _whatsBinder.OnTxtWhatsValidated()
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

    Private Sub chkWhats_CheckedChanged(sender As Object, e As EventArgs) Handles chkWhats.CheckedChanged
        If chkWhats.Checked Then
            'chkWhats.Text = "WhatsApp Reminder: ON"
            chk2H.Visible = True
            chk24H.Visible = True
            chk24H.Checked = False
            chk2H.Checked = False
        Else
            'chkWhats.Text = "WhatsApp Reminder: OFF"
            chk2H.Visible = False
            chk24H.Visible = False
        End If
    End Sub

    Private Sub PatientVisitsCtl_Disposed(sender As Object, e As EventArgs) Handles Me.Disposed
        If _apptDateFullDayBehavior IsNot Nothing Then
            _apptDateFullDayBehavior.Dispose()
            _apptDateFullDayBehavior = Nothing
        End If
    End Sub

#End Region

End Class

