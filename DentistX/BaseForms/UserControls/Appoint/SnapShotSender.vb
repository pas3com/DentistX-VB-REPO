Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data.SqlClient
Imports System.Globalization
Imports System.Linq
Imports System.Windows.Forms
Imports DevExpress.XtraEditors
Imports DevExpress.Utils
Imports DevExpress.XtraGrid.Views.Base
Imports DevExpress.XtraGrid.Views.Grid

''' <summary>Configure scheduled scheduler snapshot sends: jobs, recipients from doctors/staff/contacts, send time, weekday mask, and view run history.</summary>
Partial Public Class SnapShotSender

    Private NotInheritable Class SourcePick
        Public Property Id As Integer
        Public Property Name As String
    End Class

    Private _currentJobId As Integer
    Private _dayChecks As CheckEdit()
    Private _loadingJob As Boolean
    Private _jobs As IList(Of SchedulerSnapshotAutoSendJobRow)
    Private ReadOnly _doctorsData As New DoctorsDATA()
    Private ReadOnly _empData As New EmpDATA()
    Private ReadOnly _secData As New SecretariesDATA()
    Private ReadOnly _contactData As New ContactDATA()

    Private Shared ReadOnly ArCulture As New CultureInfo("ar")

    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub SnapShotSender_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.RightToLeft = If(Eng, Windows.Forms.RightToLeft.No, Windows.Forms.RightToLeft.Yes)
        Me.RightToLeftLayout = Not Eng
        ApplyUiLanguage()
        ConfigureTwelveHourTimeAndGridDisplays()
        BuildDayCheckboxes()
        SetupSourceTypeCombo()
        SetupRecipientLookUp()
        AddHandler viewJobs.CustomUnboundColumnData, AddressOf ViewJobs_CustomUnboundColumnData
        AddHandler viewJobs.CustomColumnDisplayText, AddressOf ViewJobs_CustomColumnDisplayText
        AddHandler viewJobs.FocusedRowChanged, AddressOf ViewJobs_FocusedRowChanged
        AddHandler viewLog.CustomColumnDisplayText, AddressOf ViewLog_CustomColumnDisplayText
        AddHandler viewRecipients.CellValueChanged, AddressOf ViewRecipients_CellValueChanged
        RefreshJobsGridSafe()
    End Sub

    Private Sub ConfigureTwelveHourTimeAndGridDisplays()
        timeSend.Properties.DisplayFormat.FormatType = FormatType.DateTime
        timeSend.Properties.DisplayFormat.FormatString = "hh:mm tt"
        timeSend.Properties.EditFormat.FormatType = FormatType.DateTime
        timeSend.Properties.EditFormat.FormatString = "hh:mm tt"
    End Sub

    Private Shared Function FormatTimeSpan12Hour(ts As TimeSpan) As String
        Return Date.Today.Add(ts).ToString("hh:mm tt", CultureInfo.CurrentCulture)
    End Function

    Private Shared Function FormatLogDateOnly(d As Date) As String
        Return d.ToString("dd/MM/yyyy", CultureInfo.CurrentCulture)
    End Function

    Private Shared Function FormatLogDateTime12Hour(dt As DateTime) As String
        Return dt.ToString("dd/MM/yyyy hh:mm tt", CultureInfo.CurrentCulture)
    End Function

    Private Sub ViewJobs_CustomColumnDisplayText(sender As Object, e As CustomColumnDisplayTextEventArgs)
        If e.Column Is colJobTime AndAlso e.Value IsNot Nothing AndAlso TypeOf e.Value Is TimeSpan Then
            e.DisplayText = FormatTimeSpan12Hour(DirectCast(e.Value, TimeSpan))
        End If
    End Sub

    Private Sub ViewLog_CustomColumnDisplayText(sender As Object, e As CustomColumnDisplayTextEventArgs)
        If e.Value Is Nothing OrElse IsDBNull(e.Value) Then Return
        If e.Column Is colRunDate AndAlso TypeOf e.Value Is DateTime Then
            e.DisplayText = FormatLogDateOnly(DirectCast(e.Value, DateTime).Date)
        ElseIf e.Column Is colStarted AndAlso TypeOf e.Value Is DateTime Then
            e.DisplayText = FormatLogDateTime12Hour(DirectCast(e.Value, DateTime))
        ElseIf e.Column Is colCompleted AndAlso TypeOf e.Value Is DateTime Then
            e.DisplayText = FormatLogDateTime12Hour(DirectCast(e.Value, DateTime))
        End If
    End Sub

    Private Shared Function ArString(loc As ComponentResourceManager, key As String, fallback As String) As String
        Dim s = loc.GetString(key, ArCulture)
        If String.IsNullOrEmpty(s) Then Return fallback
        Return s
    End Function

    Private Sub ApplyUiLanguage()
        Dim loc As New ComponentResourceManager(GetType(SnapShotSender))
        If Eng Then
            Text = "Scheduled snapshot send"
            tabSetup.Text = "Job / recipients"
            tabHistory.Text = "Send history"
            btnClose.Text = "Close"
            btnNewJob.Text = "New job"
            btnDeleteJob.Text = "Delete job"
            btnRefreshJobs.Text = "Refresh"
            btnTestSend.Text = "Test send now"
            btnSaveJob.Text = "Save job"
            chkJobEnabled.Properties.Caption = "Enabled"
            lblSendTime.Text = "Send time (local)"
            lblDays.Text = "Days of week"
            lblCaption.Text = "WhatsApp caption"
            lblJobNotes.Text = "Notes"
            lblSourceType.Text = "Source"
            lblPick.Text = "Person"
            btnAddRecipient.Text = "Add recipient"
            btnRemoveRecipient.Text = "Remove"
            btnRefreshLog.Text = "Refresh log"
        Else
            Text = ArString(loc, "$this.Text", "إرسال لقطة الجدول المجدول")
            tabSetup.Text = ArString(loc, "tabSetup.Text", "المهمة والمستلمون")
            tabHistory.Text = ArString(loc, "tabHistory.Text", "سجل الإرسال")
            btnClose.Text = ArString(loc, "btnClose.Text", "إغلاق")
            btnNewJob.Text = ArString(loc, "btnNewJob.Text", "مهمة جديدة")
            btnDeleteJob.Text = ArString(loc, "btnDeleteJob.Text", "حذف المهمة")
            btnRefreshJobs.Text = ArString(loc, "btnRefreshJobs.Text", "تحديث")
            btnTestSend.Text = ArString(loc, "btnTestSend.Text", "إرسال تجريبي الآن")
            btnSaveJob.Text = ArString(loc, "btnSaveJob.Text", "حفظ المهمة")
            chkJobEnabled.Properties.Caption = ArString(loc, "chkJobEnabled.Properties.Caption", "مفعّل")
            lblSendTime.Text = ArString(loc, "lblSendTime.Text", "وقت الإرسال (محلي)")
            lblDays.Text = ArString(loc, "lblDays.Text", "أيام الأسبوع")
            lblCaption.Text = ArString(loc, "lblCaption.Text", "عنوان الرسالة")
            lblJobNotes.Text = ArString(loc, "lblJobNotes.Text", "ملاحظات")
            lblSourceType.Text = ArString(loc, "lblSourceType.Text", "المصدر")
            lblPick.Text = ArString(loc, "lblPick.Text", "الشخص")
            btnAddRecipient.Text = ArString(loc, "btnAddRecipient.Text", "إضافة مستلم")
            btnRemoveRecipient.Text = ArString(loc, "btnRemoveRecipient.Text", "إزالة")
            btnRefreshLog.Text = ArString(loc, "btnRefreshLog.Text", "تحديث السجل")
        End If
        cboSourceType.Properties.Items.Clear()
        cboSourceType.Properties.Items.Add(If(Eng, "Doctor", "طبيب"))
        cboSourceType.Properties.Items.Add(If(Eng, "Employee", "موظف"))
        cboSourceType.Properties.Items.Add(If(Eng, "Secretary", "سكرتير"))
        cboSourceType.Properties.Items.Add(If(Eng, "Contact", "جهة اتصال"))
        cboSourceType.SelectedIndex = 0
        ApplyGridColumnCaptions(loc)
        lblSendContent.Text = If(Eng, "Send as", "الإرسال")
        ConfigureSendContentRadioItems()
    End Sub

    Private Sub ConfigureSendContentRadioItems()
        Dim prev = rgSendContent.EditValue
        rgSendContent.Properties.Items.Clear()
        Dim t0 = If(Eng, "Image (PNG) only", "صورة PNG فقط")
        Dim t1 = If(Eng, "HTML only", "HTML فقط")
        Dim t2 = If(Eng, "Image and HTML", "صورة وHTML")
        rgSendContent.Properties.Items.Add(New DevExpress.XtraEditors.Controls.RadioGroupItem(CByte(0), t0))
        rgSendContent.Properties.Items.Add(New DevExpress.XtraEditors.Controls.RadioGroupItem(CByte(1), t1))
        rgSendContent.Properties.Items.Add(New DevExpress.XtraEditors.Controls.RadioGroupItem(CByte(2), t2))
        If prev IsNot Nothing AndAlso (TypeOf prev Is Byte OrElse TypeOf prev Is SByte OrElse TypeOf prev Is Integer OrElse TypeOf prev Is Long) Then
            Dim b = Convert.ToInt32(prev)
            If b = 0 OrElse b = 1 OrElse b = 2 Then
                rgSendContent.EditValue = CByte(b)
            Else
                rgSendContent.EditValue = CByte(0)
            End If
        Else
            rgSendContent.EditValue = CByte(0)
        End If
    End Sub

    Private Function GetEditorSendContentModeByte() As Byte
        Dim o = rgSendContent.EditValue
        If o Is Nothing Then Return 0
        Dim b = Convert.ToInt32(o)
        If b = 0 OrElse b = 1 OrElse b = 2 Then Return CByte(b)
        Return 0
    End Function

    Private Sub ApplyGridColumnCaptions(Optional loc As ComponentResourceManager = Nothing)
        If loc Is Nothing Then loc = New ComponentResourceManager(GetType(SnapShotSender))
        If Eng Then
            colJobId.Caption = "Job #"
            colJobEnabled.Caption = "On"
            colJobTime.Caption = "Time"
            colJobDays.Caption = "Days"
            colJobRecipCount.Caption = "#Recip."
            colJobCaption.Caption = "Caption"
            colRecSource.Caption = "Source"
            colRecPk.Caption = "Id"
            colRecName.Caption = "Name"
            colRecPrefix.Caption = "WA prefix"
            colRecLocal.Caption = "WA local"
            colRecActive.Caption = "Active"
            colRunDate.Caption = "Run date"
            colStatus.Caption = "Status"
            colStarted.Caption = "Started"
            colCompleted.Caption = "Completed"
            colLogRecip.Caption = "Recip.#"
            colErr.Caption = "Error"
            colMedia.Caption = "File"
        Else
            colJobId.Caption = ArString(loc, "colJobId.Caption", "رقم")
            colJobEnabled.Caption = ArString(loc, "colJobEnabled.Caption", "تشغيل")
            colJobTime.Caption = ArString(loc, "colJobTime.Caption", "الوقت")
            colJobDays.Caption = ArString(loc, "colJobDays.Caption", "أيام")
            colJobRecipCount.Caption = ArString(loc, "colJobRecipCount.Caption", "عدد")
            colJobCaption.Caption = ArString(loc, "colJobCaption.Caption", "عنوان")
            colRecSource.Caption = ArString(loc, "colRecSource.Caption", "المصدر")
            colRecPk.Caption = ArString(loc, "colRecPk.Caption", "المعرّف")
            colRecName.Caption = ArString(loc, "colRecName.Caption", "الاسم")
            colRecPrefix.Caption = ArString(loc, "colRecPrefix.Caption", "بادئة")
            colRecLocal.Caption = ArString(loc, "colRecLocal.Caption", "محلي")
            colRecActive.Caption = ArString(loc, "colRecActive.Caption", "نشط")
            colRunDate.Caption = ArString(loc, "colRunDate.Caption", "اليوم")
            colStatus.Caption = ArString(loc, "colStatus.Caption", "الحالة")
            colStarted.Caption = ArString(loc, "colStarted.Caption", "البداية")
            colCompleted.Caption = ArString(loc, "colCompleted.Caption", "النهاية")
            colLogRecip.Caption = ArString(loc, "colLogRecip.Caption", "مستلم")
            colErr.Caption = ArString(loc, "colErr.Caption", "خطأ")
            colMedia.Caption = ArString(loc, "colMedia.Caption", "ملف")
        End If
    End Sub

    Private Sub BuildDayCheckboxes()
        flowDays.Controls.Clear()
        Dim bits = {1, 2, 4, 8, 16, 32, 64}
        Dim namesEn = {"Mon", "Tue", "Wed", "Thu", "Fri", "Sat", "Sun"}
        Dim namesAr = {"إثنين", "ثلاثاء", "أربعاء", "خميس", "جمعة", "سبت", "أحد"}
        ReDim _dayChecks(6)
        For i = 0 To 6
            Dim chk As New CheckEdit()
            chk.Properties.Caption = If(Eng, namesEn(i), namesAr(i))
            chk.Tag = bits(i)
            chk.Margin = New Padding(2)
            chk.Checked = (i < 5)
            flowDays.Controls.Add(chk)
            _dayChecks(i) = chk
        Next
    End Sub

    Private Sub SetupSourceTypeCombo()
        AddHandler cboSourceType.SelectedIndexChanged, AddressOf CboSourceType_SelectedIndexChanged
    End Sub

    Private Sub CboSourceType_SelectedIndexChanged(sender As Object, e As EventArgs)
        ReloadSourceLookUp()
    End Sub

    Private Sub SetupRecipientLookUp()
        lookUpSource.Properties.Columns.Clear()
        lookUpSource.Properties.Columns.Add(
            New DevExpress.XtraEditors.Controls.LookUpColumnInfo("Name", If(Eng, "Name", "الاسم")) With {.Width = 360})
        lookUpSource.Properties.DisplayMember = "Name"
        lookUpSource.Properties.ValueMember = "Id"
        lookUpSource.Properties.NullText = ""
        ReloadSourceLookUp()
    End Sub

    Private Function SelectedSourceTypeKey() As String
        Select Case cboSourceType.SelectedIndex
            Case 0
                Return SchedulerSnapshotAutoSendRepository.SourceDoctor
            Case 1
                Return SchedulerSnapshotAutoSendRepository.SourceEmployee
            Case 2
                Return SchedulerSnapshotAutoSendRepository.SourceSecretary
            Case Else
                Return SchedulerSnapshotAutoSendRepository.SourceContact
        End Select
    End Function

    Private Sub ReloadSourceLookUp()
        lookUpSource.EditValue = Nothing
        Dim list As New List(Of SourcePick)()
        Try
            Select Case cboSourceType.SelectedIndex
                Case 0
                    For Each d In _doctorsData.SelectAll()
                        list.Add(New SourcePick With {.Id = d.DrID, .Name = If(d.DrName, "")})
                    Next
                Case 1
                    For Each e1 In _empData.SelectAll()
                        list.Add(New SourcePick With {.Id = e1.EmpID, .Name = If(e1.EmpName, "")})
                    Next
                Case 2
                    For Each s In _secData.SelectAll()
                        list.Add(New SourcePick With {.Id = s.SecID, .Name = If(s.SecName, "")})
                    Next
                Case Else
                    For Each c In _contactData.SelectAll()
                        list.Add(New SourcePick With {.Id = c.ContactID, .Name = If(c.CName, "")})
                    Next
            End Select
        Catch
        End Try
        lookUpSource.Properties.DataSource = list.OrderBy(Function(x) x.Name).ToList()
    End Sub

    Private Sub ViewJobs_CustomUnboundColumnData(sender As Object, e As CustomColumnDataEventArgs)
        If e.Column Is colJobDays AndAlso e.IsGetData Then
            Dim row = TryCast(e.Row, SchedulerSnapshotAutoSendJobRow)
            If row IsNot Nothing Then
                e.Value = SchedulerSnapshotAutoSendRepository.FormatDaysMask(row.DaysOfWeekMask, Eng)
            End If
        End If
    End Sub

    Private Sub RefreshJobsGridSafe()
        Try
            _jobs = SchedulerSnapshotAutoSendRepository.GetJobs()
            gridJobs.DataSource = Nothing
            gridJobs.DataSource = _jobs
            viewJobs.BestFitColumns()
            If _jobs IsNot Nothing AndAlso _jobs.Count > 0 Then
                viewJobs.FocusedRowHandle = 0
            Else
                ClearJobEditor()
                gridRecipients.DataSource = Nothing
                gridLog.DataSource = Nothing
            End If
        Catch ex As SqlException
            ShowDbError(ex)
        Catch ex As Exception
            MessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Shared Sub ShowDbError(ex As SqlException)
        Dim hint = If(ex.Number = 208,
            vbCrLf & vbCrLf & "Run DatabaseScripts/SchedulerSnapshotAutoSend.sql on this database.",
            If(ex.Number = 207 AndAlso ex.Message.IndexOf("SendContentMode", StringComparison.OrdinalIgnoreCase) >= 0,
                vbCrLf & vbCrLf & "Run DatabaseScripts/SchedulerSnapshotAutoSend_AddSendContentMode.sql on this database.",
                If(ex.Number = 207 AndAlso ex.Message.IndexOf("WeekOffset", StringComparison.OrdinalIgnoreCase) >= 0,
                    vbCrLf & vbCrLf & "Run DatabaseScripts/SchedulerSnapshotAutoSend_AddWeekOffset.sql to align the job table (drops legacy WeekOffset).",
                    "")))
        MessageBox.Show(ex.Message & hint, "Database", MessageBoxButtons.OK, MessageBoxIcon.Warning)
    End Sub

    Private Sub ViewJobs_FocusedRowChanged(sender As Object, e As FocusedRowChangedEventArgs)
        Dim row = TryCast(viewJobs.GetRow(e.FocusedRowHandle), SchedulerSnapshotAutoSendJobRow)
        If row Is Nothing Then
            ClearJobEditor()
            gridRecipients.DataSource = Nothing
            gridLog.DataSource = Nothing
            Return
        End If
        _currentJobId = row.JobId
        LoadJobIntoEditor(row)
        LoadRecipientsGrid()
        LoadLogGrid()
    End Sub

    Private Sub ClearJobEditor()
        _currentJobId = 0
        _loadingJob = True
        Try
            chkJobEnabled.Checked = False
            timeSend.EditValue = Date.Today.AddHours(8)
            For Each c In _dayChecks
                c.Checked = False
            Next
            memoCaption.Text = ""
            txtJobNotes.Text = ""
            rgSendContent.EditValue = CByte(0)
        Finally
            _loadingJob = False
        End Try
    End Sub

    Private Sub LoadJobIntoEditor(job As SchedulerSnapshotAutoSendJobRow)
        _loadingJob = True
        Try
            chkJobEnabled.Checked = job.IsEnabled
            Dim ts = job.SendTimeLocal
            timeSend.EditValue = Date.Today.Add(ts)
            Dim m = CInt(job.DaysOfWeekMask)
            For Each c In _dayChecks
                Dim bit = CInt(c.Tag)
                c.Checked = (m And bit) <> 0
            Next
            memoCaption.Text = If(job.MessageCaption, "")
            txtJobNotes.Text = If(job.Notes, "")
            Dim b = CInt(job.SendContentMode) And 255
            If b = 0 OrElse b = 1 OrElse b = 2 Then
                rgSendContent.EditValue = CByte(b)
            Else
                rgSendContent.EditValue = CByte(0)
            End If
        Finally
            _loadingJob = False
        End Try
    End Sub

    Private Function ComputeDaysMask() As Byte
        Dim m As Integer = 0
        For Each c In _dayChecks
            If c.Checked Then m = m Or CInt(c.Tag)
        Next
        If m = 0 Then m = 31
        Return CByte(Math.Min(m, 255))
    End Function

    Private Sub LoadRecipientsGrid()
        If _currentJobId <= 0 Then
            gridRecipients.DataSource = Nothing
            Return
        End If
        Try
            gridRecipients.DataSource = Nothing
            gridRecipients.DataSource = SchedulerSnapshotAutoSendRepository.GetRecipients(_currentJobId).ToList()
            viewRecipients.BestFitColumns()
        Catch ex As SqlException
            ShowDbError(ex)
        End Try
    End Sub

    Private Sub LoadLogGrid()
        If _currentJobId <= 0 Then
            gridLog.DataSource = Nothing
            Return
        End If
        Try
            gridLog.DataSource = Nothing
            gridLog.DataSource = SchedulerSnapshotAutoSendRepository.GetRecentLogs(_currentJobId, 500).ToList()
            viewLog.BestFitColumns()
        Catch ex As SqlException
            ShowDbError(ex)
        End Try
    End Sub

    Private Sub ViewRecipients_CellValueChanged(sender As Object, e As CellValueChangedEventArgs)
        If _currentJobId <= 0 Then Return
        Dim row = TryCast(viewRecipients.GetRow(e.RowHandle), SchedulerSnapshotAutoSendRecipientRow)
        If row Is Nothing Then Return
        If e.Column IsNot Nothing AndAlso String.Equals(e.Column.FieldName, NameOf(SchedulerSnapshotAutoSendRecipientRow.IsActive), StringComparison.Ordinal) Then
            Try
                SchedulerSnapshotAutoSendRepository.UpdateRecipient(row)
            Catch ex As Exception
                MessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                LoadRecipientsGrid()
            End Try
        End If
    End Sub

    Private Sub btnRefreshJobs_Click(sender As Object, e As EventArgs) Handles btnRefreshJobs.Click
        RefreshJobsGridSafe()
    End Sub

    Private Sub btnNewJob_Click(sender As Object, e As EventArgs) Handles btnNewJob.Click
        Try
            Dim row As New SchedulerSnapshotAutoSendJobRow With {
                .IsEnabled = True,
                .SendTimeLocal = New TimeSpan(8, 0, 0),
                .DaysOfWeekMask = 31,
                .MessageCaption = If(Eng, "Schedule snapshot", "لقطة الجدول"),
                .Notes = Nothing,
                .SendContentMode = 0
            }
            Dim newId = SchedulerSnapshotAutoSendRepository.InsertJob(row)
            RefreshJobsGridSafe()
            FocusJobRow(newId)
        Catch ex As SqlException
            ShowDbError(ex)
        Catch ex As Exception
            MessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnDeleteJob_Click(sender As Object, e As EventArgs) Handles btnDeleteJob.Click
        If _currentJobId <= 0 Then Return
        Dim ok = MessageBox.Show(
            If(Eng, "Delete this job and all its recipients and log rows?", "هل تريد حذف المهمة وجميع المستلمين وسجل الإرسال؟"),
            Text, MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
        If ok <> DialogResult.Yes Then Return
        Try
            SchedulerSnapshotAutoSendRepository.DeleteJob(_currentJobId)
            RefreshJobsGridSafe()
        Catch ex As Exception
            MessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Async Sub btnTestSend_Click(sender As Object, e As EventArgs) Handles btnTestSend.Click
        If _currentJobId <= 0 Then
            MessageBox.Show(
                If(Eng, "Create a job and add at least one recipient first.", "أنشئ مهمة وأضف مستلماً واحداً على الأقل."),
                Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If
        Dim row As SchedulerSnapshotAutoSendJobRow = Nothing
        Try
            row = SchedulerSnapshotAutoSendRepository.GetJob(_currentJobId)
        Catch ex As SqlException
            ShowDbError(ex)
            Return
        End Try
        If row Is Nothing Then Return
        Try
            Await SchedulerSnapshotAutoSendService.RunJobAsync(row, DateTime.Now.Date, forceRun:=True).ConfigureAwait(True)
            MsgBox(If(Eng, "Message queued for sending.", "تم وضع الرسالة في الطابور للإرسال."), MsgBoxStyle.Information)

        Catch ex As Exception
            MessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            RefreshAfterJobRunUiSafe()
        End Try
    End Sub

    Private Sub RefreshAfterJobRunUiSafe()
        If Me.InvokeRequired Then
            BeginInvoke(New Action(AddressOf RefreshAfterJobRunUiSafe))
            Return
        End If
        LoadLogGrid()
        RefreshJobsGridSafe()
    End Sub

    Private Sub btnSaveJob_Click(sender As Object, e As EventArgs) Handles btnSaveJob.Click
        If _currentJobId <= 0 Then Return
        Try
            Dim ts = GetTimeFromEditor()
            Dim row As New SchedulerSnapshotAutoSendJobRow With {
                .JobId = _currentJobId,
                .IsEnabled = chkJobEnabled.Checked,
                .SendTimeLocal = ts,
                .DaysOfWeekMask = ComputeDaysMask(),
                .MessageCaption = NullIfEmpty(memoCaption.Text),
                .Notes = NullIfEmpty(txtJobNotes.Text),
                .SendContentMode = GetEditorSendContentModeByte()
            }
            SchedulerSnapshotAutoSendRepository.UpdateJob(row)
            RefreshJobsGridSafe()
            FocusJobRow(_currentJobId)
        Catch ex As Exception
            MessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Function GetTimeFromEditor() As TimeSpan
        Dim dtObj = timeSend.EditValue
        If TypeOf dtObj Is DateTime Then
            Return DirectCast(dtObj, DateTime).TimeOfDay
        End If
        If TypeOf dtObj Is TimeSpan Then
            Return DirectCast(dtObj, TimeSpan)
        End If
        If TypeOf dtObj Is TimeOnly Then
            Return DirectCast(dtObj, TimeOnly).ToTimeSpan()
        End If
        Return New TimeSpan(8, 0, 0)
    End Function

    Private Shared Function NullIfEmpty(s As String) As String
        If String.IsNullOrWhiteSpace(s) Then Return Nothing
        Return s.Trim()
    End Function

    Private Sub btnAddRecipient_Click(sender As Object, e As EventArgs) Handles btnAddRecipient.Click
        If _currentJobId <= 0 Then
            MessageBox.Show(If(Eng, "Select or create a job first.", "اختر أو أنشئ مهمة أولاً."), Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If
        Dim idObj = lookUpSource.EditValue
        If idObj Is Nothing Then Return
        Dim pk = Convert.ToInt32(idObj)
        Dim src = SelectedSourceTypeKey()
        Dim disp As String = Nothing
        Dim prefix As String = Nothing
        Dim local As String = Nothing
        Try
            Select Case src
                Case SchedulerSnapshotAutoSendRepository.SourceDoctor
                    Dim d = _doctorsData.GetDoctorById(pk)
                    If d Is Nothing Then Return
                    disp = d.DrName
                    prefix = d.WhatsAppPrefix
                    local = d.WhatsApp
                Case SchedulerSnapshotAutoSendRepository.SourceEmployee
                    Dim e1 = _empData.GetEmpById(pk)
                    If e1 Is Nothing Then Return
                    disp = e1.EmpName
                    prefix = e1.WhatsAppPrefix
                    local = e1.WhatsApp
                Case SchedulerSnapshotAutoSendRepository.SourceSecretary
                    Dim s As New Secretaries With {.SecID = pk}
                    Dim s2 = _secData.Select_Record(s)
                    If s2 Is Nothing Then Return
                    disp = s2.SecName
                    prefix = s2.WhatsAppPrefix
                    local = s2.WhatsApp
                Case Else
                    Dim c As New Contact With {.ContactID = pk}
                    Dim c2 = _contactData.Select_Record(c)
                    If c2 Is Nothing Then Return
                    disp = c2.CName
                    prefix = c2.WhatsAppPrefix
                    local = c2.WhatsApp
            End Select
        Catch ex As Exception
            MessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End Try

        Dim digits = WhatsHelper.BuildInternationalWhatsDigits(If(local, ""), If(prefix, ""))
        If String.IsNullOrWhiteSpace(digits) OrElse digits.Length < 8 Then
            MessageBox.Show(
                If(Eng, "This person has no usable WhatsApp number in their record.", "لا يوجد رقم واتساب صالح في سجل هذا الشخص."),
                Text, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim rec As New SchedulerSnapshotAutoSendRecipientRow With {
            .JobId = _currentJobId,
            .SourceType = src,
            .SourcePk = pk,
            .DisplayName = If(disp, ""),
            .WhatsAppPrefix = prefix,
            .WhatsAppLocal = local,
            .IsActive = True
        }
        Try
            SchedulerSnapshotAutoSendRepository.InsertRecipient(rec)
            LoadRecipientsGrid()
            RefreshJobsGridSafe()
        Catch ex As SqlException When ex.Number = 2627 OrElse ex.Number = 2601
            MessageBox.Show(
                If(Eng, "This person is already in the list.", "هذا الشخص مضاف بالفعل."),
                Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            MessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnRemoveRecipient_Click(sender As Object, e As EventArgs) Handles btnRemoveRecipient.Click
        Dim row = TryCast(viewRecipients.GetFocusedRow(), SchedulerSnapshotAutoSendRecipientRow)
        If row Is Nothing Then Return
        Try
            SchedulerSnapshotAutoSendRepository.DeleteRecipient(row.RecipientId)
            LoadRecipientsGrid()
            RefreshJobsGridSafe()
        Catch ex As Exception
            MessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnRefreshLog_Click(sender As Object, e As EventArgs) Handles btnRefreshLog.Click
        LoadLogGrid()
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Close()
    End Sub

    Private Sub FocusJobRow(jobId As Integer)
        For i = 0 To viewJobs.RowCount - 1
            Dim v = viewJobs.GetRowCellValue(i, "JobId")
            If v IsNot Nothing AndAlso Convert.ToInt32(v) = jobId Then
                viewJobs.FocusedRowHandle = i
                Return
            End If
        Next
    End Sub

End Class
