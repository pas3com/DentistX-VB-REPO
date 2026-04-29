Imports System.Threading.Tasks
Imports System.Windows.Forms
Imports DevExpress.XtraGrid.Columns
''' <summary>Shows dbo.ApptTwoHourWhatsAppQueue: one row per appointment with 24h and short-lead send times and independent statuses.</summary>
Public Partial Class FrmApptsReminder

    Public Sub New()
        MyBase.New()
        InitializeComponent()
    End Sub

    Private Sub FrmApptsReminder_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ApplyLocalizedUi()
        ConfigureColumns()
        ReloadGrid()
    End Sub

    Private Sub ApplyLocalizedUi()
        Text = If(Eng, "Appointment WhatsApp reminder queue", "قائمة تذكيرات واتساب للمواعيد")
        Dim sh = CInt(Math.Floor(ApptTwoHourReminderQueueRepository.GetShortReminderHours()))
        LblInfo.Text = If(Eng,
            $"Each appointment has one queue row. SendAt24h and SendAt2h are when reminders go out (start − 24h / − {sh}h from Settings). Status24h and Status2h are independent (Pending, Sent, Failed, …). Rows are created when an appointment is saved and starts after ""now + short lead + buffer""; the timer sends due Pending legs when WhatsApp is connected.",
            $"صف واحد لكل موعد. SendAt24h وSendAt2h هما وقت الإرسال (بداية الموعد ناقص 24 س / − {sh} س من الإعدادات). تُنشأ الصفوف عند حفظ موعد يبدأ بعد فترة كافية؛ المؤقّت يرسل ما استحق عند اتصال واتساب.")
        BtnRefresh.Text = If(Eng, "Refresh", "تحديث")
        BtnRunNow.Text = If(Eng, "Run queue now", "تشغيل القائمة الآن")
    End Sub

    Private Sub ConfigureColumns()
        ViewMain.Columns.Clear()
        BindCol("QueueId", If(Eng, "Queue #", "رقم"))
        BindCol("AppointmentId", If(Eng, "Appt ID", "رقم الموعد"))
        BindCol("PatientName", If(Eng, "Patient", "المريض"))
        BindCol("DrName", If(Eng, "Doctor", "الطبيب"))
        BindCol("TargetPhone", If(Eng, "Phone", "الهاتف"))
        BindCol("ApptStartSnapshot", If(Eng, "Appt start", "بداية الموعد"))
        BindCol("SendAt24h", If(Eng, "Send 24h", "إرسال 24س"))
        Dim shortH = CInt(Math.Floor(ApptTwoHourReminderQueueRepository.GetShortReminderHours()))
        BindCol("SendAt2h", If(Eng, $"Send −{shortH}h", $"إرسال −{shortH}س"))
        BindCol("Status24h", If(Eng, "24h status", "حالة 24س"))
        BindCol("Status2h", If(Eng, "2h status", "حالة 2س"))
        BindCol("Processed24hAtUtc", If(Eng, "24h sent (UTC)", "إرسال 24س (UTC)"))
        BindCol("Processed2hAtUtc", If(Eng, "2h sent (UTC)", "إرسال 2س (UTC)"))
        BindCol("WhatsAppLogId24h", If(Eng, "Log 24h", "سجل 24س"))
        BindCol("WhatsAppLogId2h", If(Eng, "Log 2h", "سجل 2س"))
        BindCol("Error24h", If(Eng, "Err 24h", "خطأ 24س"))
        BindCol("Error2h", If(Eng, "Err 2h", "خطأ 2س"))
        Dim col24 = BindCol("MessagePreview24h", If(Eng, "Msg 24h", "رسالة 24س"))
        Dim col2 = BindCol("MessagePreview2h", If(Eng, "Msg 2h", "رسالة 2س"))
        BindCol("ClinicId", If(Eng, "Clinic", "العيادة"))
        BindCol("CreatedAtUtc", If(Eng, "Queued (UTC)", "وقت الإدخال (UTC)"))
        col24.VisibleIndex = ViewMain.Columns.Count - 2
        col2.VisibleIndex = ViewMain.Columns.Count - 1
        ViewMain.BestFitColumns()
    End Sub

    Private Function BindCol(fieldName As String, caption As String) As GridColumn
        Dim col = ViewMain.Columns.AddField(fieldName)
        col.Caption = caption
        col.Visible = True
        Return col
    End Function

    Private Sub ReloadGrid()
        Dim rows = ApptTwoHourReminderQueueRepository.GetRecentRows(750)
        BindingQueue.DataSource = Nothing
        BindingQueue.DataSource = rows
        ViewMain.BestFitColumns()
    End Sub

    Private Sub BtnRefresh_Click(sender As Object, e As EventArgs) Handles BtnRefresh.Click
        ReloadGrid()
    End Sub

    Private Async Sub BtnRunNow_Click(sender As Object, e As EventArgs) Handles BtnRunNow.Click
        BtnRunNow.Enabled = False
        Try
            Await ApptWhatsAppReminderQueueProcessor.RunEnqueueAllAndProcessAsync()
            ReloadGrid()
        Finally
            BtnRunNow.Enabled = True
        End Try
    End Sub

    Private Sub ViewMain_DoubleClick(sender As Object, e As EventArgs) Handles ViewMain.DoubleClick
        Dim row = TryCast(ViewMain.GetFocusedRow(), ApptTwoHourReminderQueueRow)
        If row Is Nothing Then Return

        Dim field = TryCast(ViewMain.FocusedColumn, GridColumn)?.FieldName
        Dim logId As Long? = Nothing
        If String.Equals(field, NameOf(ApptTwoHourReminderQueueRow.WhatsAppLogId24h), StringComparison.Ordinal) Then
            logId = row.WhatsAppLogId24h
        ElseIf String.Equals(field, NameOf(ApptTwoHourReminderQueueRow.WhatsAppLogId2h), StringComparison.Ordinal) Then
            logId = row.WhatsAppLogId2h
        ElseIf row.WhatsAppLogId24h.HasValue AndAlso row.WhatsAppLogId24h.Value > 0 Then
            logId = row.WhatsAppLogId24h
        ElseIf row.WhatsAppLogId2h.HasValue AndAlso row.WhatsAppLogId2h.Value > 0 Then
            logId = row.WhatsAppLogId2h
        ElseIf row.WhatsAppLogId.HasValue AndAlso row.WhatsAppLogId.Value > 0 Then
            logId = row.WhatsAppLogId
        End If

        If logId.HasValue AndAlso logId.Value > 0 Then
            FrmWhatsAppActivityLog.ShowForLogEntry(Me, logId.Value)
        End If
    End Sub

    ''' <summary>Open queue window from settings / WhatsApp tools.</summary>
    Public Shared Sub ShowQueue(owner As IWin32Window)
        Using f As New FrmApptsReminder()
            Dim oform = TryCast(owner, Form)
            If oform IsNot Nothing AndAlso oform.Icon IsNot Nothing Then
                f.Icon = oform.Icon
            End If
            f.ShowDialog(owner)
        End Using
    End Sub
End Class
