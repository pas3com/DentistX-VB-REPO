Imports System.Drawing
Imports System.Windows.Forms

''' <summary>Central launcher for WhatsApp connection, logs, queues, bulk tools, and schedules.</summary>
Public Partial Class FrmWhatsHub

    Public Property ShellIcon As Icon

    Private Sub FrmWhatsHub_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If ShellIcon IsNot Nothing Then Icon = ShellIcon
        Text = If(Eng, "WhatsApp hub", "مركز واتساب")
        LblIntro.Text = If(Eng,
            "Choose a tool. Connection is required before sending. Auto reminders use the SQL queue (24h and 2h).",
            "اختر الأداة. يجب الاتصال قبل الإرسال. التذكيرات التلقائية تستخدم قائمة SQL (24 ساعة و ساعتان).")
        BtnConnectionSend.Text = If(Eng, "Connection && send", "الاتصال والإرسال")
        BtnMessageLog.Text = If(Eng, "Message log", "سجل الرسائل")
        BtnAutoReminderQueue.Text = If(Eng, "Auto reminder queue (24h / 2h)", "قائمة التذكير التلقائي (24س / 2س)")
        BtnBulkAppointments.Text = If(Eng, "Bulk appointment WhatsApp", "إرسال مواعيد جماعي")
        BtnBulkAccounts.Text = If(Eng, "Bulk account WhatsApp", "إرسال حسابات جماعي")
        BtnApptReminderSchedule.Text = If(Eng, "Appointment reminder schedule", "جدولة تذكير المواعيد")
        BtnAccountReminderSchedule.Text = If(Eng, "Account reminder schedule", "جدولة تذكير الحساب")
    End Sub

    Private Sub BtnConnectionSend_Click(sender As Object, e As EventArgs) Handles BtnConnectionSend.Click
        Using f As New WhatsAppForm()
            If ShellIcon IsNot Nothing Then f.Icon = ShellIcon
            f.ShowDialog(Me)
        End Using
    End Sub

    Private Sub BtnMessageLog_Click(sender As Object, e As EventArgs) Handles BtnMessageLog.Click
        FrmWhatsAppActivityLog.ShowArchive(Me)
    End Sub

    Private Sub BtnAutoReminderQueue_Click(sender As Object, e As EventArgs) Handles BtnAutoReminderQueue.Click
        FrmApptsReminder.ShowQueue(Me)
    End Sub

    Private Sub BtnBulkAppointments_Click(sender As Object, e As EventArgs) Handles BtnBulkAppointments.Click
        Using f As New FrmApptsWhats()
            If ShellIcon IsNot Nothing Then f.Icon = ShellIcon
            f.ShowDialog(Me)
        End Using
    End Sub

    Private Sub BtnBulkAccounts_Click(sender As Object, e As EventArgs) Handles BtnBulkAccounts.Click
        Using f As New FrmAccountWhats()
            If ShellIcon IsNot Nothing Then f.Icon = ShellIcon
            f.ShowDialog(Me)
        End Using
    End Sub

    Private Sub BtnApptReminderSchedule_Click(sender As Object, e As EventArgs) Handles BtnApptReminderSchedule.Click
        Using f As New FrmApptReminderSchedule()
            If ShellIcon IsNot Nothing Then f.Icon = ShellIcon
            f.ShowDialog(Me)
        End Using
    End Sub

    Private Sub BtnAccountReminderSchedule_Click(sender As Object, e As EventArgs) Handles BtnAccountReminderSchedule.Click
        Using f As New FrmAccountReminderSchedule()
            If ShellIcon IsNot Nothing Then f.Icon = ShellIcon
            f.ShowDialog(Me)
        End Using
    End Sub
End Class
