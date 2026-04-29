''' <summary>Canonical <see cref="WhatsAppSendContext.Category"/> values stored in dbo.WhatsAppMessageLog.</summary>
Public NotInheritable Class WhatsAppMessageCategories
    Public Const General As String = "General"
    Public Const ManualSend As String = "ManualSend"
    ''' <summary>~24h automatic appointment reminder (queue / log).</summary>
    Public Const AppointmentReminder As String = "AppointmentReminder"
    ''' <summary>~2h automatic appointment reminder (queue / log).</summary>
    Public Const AppointmentTwoHourReminder As String = "AppointmentTwoHourReminder"
    ''' <summary>Recurring generic appointment schedule message.</summary>
    Public Const AppointmentSchedule As String = "AppointmentSchedule"
    Public Const PatientVisits As String = "PatientVisits"
    Public Const AppointmentEditor As String = "AppointmentEditor"
    Public Const StockExpense As String = "StockExpense"
    ''' <summary>Navigator patient appointment compose; historical DB value kept as <c>Appointment</c>.</summary>
    Public Const NavigatorAppointment As String = "Appointment"
    Public Const PatientAccount As String = "PatientAccount"
    Public Const Accounting As String = "Accounting"
    Public Const NavigatorWhatsApp As String = "NavigatorWhatsApp"
    Public Const AccountingSummary As String = "AccountingSummary"
    ''' <summary>Scheduled 7-day scheduler PNG sent to staff/contacts.</summary>
    Public Const SchedulerSnapshotAuto As String = "SchedulerSnapshotAuto"
    ''' <summary>Automatic crash log upload on next startup.</summary>
    Public Const CrashReport As String = "CrashReport"
End Class
