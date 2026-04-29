''' <summary>
''' dbo.ApptTwoHourWhatsAppQueue: one row per future appointment with 24h and 2h WhatsApp legs.
''' <para><b>Immediate consistency:</b> On insert/update, <see cref="AppointmentCRepository"/> (and schedulers after commit)
''' call <see cref="ApptTwoHourReminderQueueRepository.SyncFromAppointmentId"/>, which updates or removes the queue row in the same operation as the database save.
''' On delete, <see cref="AppointmentCRepository.Delete"/> removes the queue row first, then deletes <c>AppointmentC</c>.
''' Each successful sync and each early exit from sync calls <see cref="ApptTwoHourReminderQueueRepository.BumpProcessingAfterQueueChange"/>
''' so the reminder processor runs right away instead of only on the 60s tick.</para>
''' TargetPhone, messages, SendAt*, names, ClinicId are refreshed from the live Patient + appointment row.
''' While a leg is Pending, Processed*AtUtc / Error* / WhatsAppLogId* are usually NULL until send completes.
''' <see cref="ApptTwoHourReminderQueueRepository.FinalizeOverduePendingLegs"/> marks overdue Pending legs Missed even when WhatsApp is offline.
''' </summary>
Public NotInheritable Class AppointmentWhatsAppQueueService
    Private Sub New()
    End Sub

    ''' <summary>Call once at application startup after connection string is finalized (e.g. end of MyApplication.Startup).</summary>
    Public Shared Sub InitializeAtApplicationStartup()
        ApptWhatsAppReminderBackgroundPoller.EnsureStarted()
        ApptWhatsAppReminderBackgroundPoller.RequestImmediatePoll()
    End Sub

    ''' <summary>Call after AppointmentC is committed with a valid id. Inserts/updates queue row when start is far enough in the future (short lead from My.Settings.ShortReminder + buffer).</summary>
    ''' <param name="reminderMessageEnglish">RadioLang from editor: English=True, Arabic=False. Nothing = use global Eng for preview text.</param>
    ''' <param name="include24HourReminder">False = do not queue the 24h leg.</param>
    ''' <param name="includeShortLeadReminder">False = do not queue the short-lead (e.g. 2h) leg.</param>
    Public Shared Sub NotifyAppointmentSaved(appointmentId As Integer, Optional reminderMessageEnglish As Boolean? = Nothing,
                                            Optional include24HourReminder As Boolean = True, Optional includeShortLeadReminder As Boolean = True)
        If appointmentId <= 0 Then Return
        ApptTwoHourReminderQueueRepository.SyncFromAppointmentId(appointmentId, reminderMessageEnglish, include24HourReminder, includeShortLeadReminder)
    End Sub

    ''' <summary>Delegates to <see cref="ApptTwoHourReminderQueueRepository.BumpProcessingAfterQueueChange"/> after repository-level queue deletes outside a full sync.</summary>
    Public Shared Sub RequestImmediateProcessing()
        ApptTwoHourReminderQueueRepository.BumpProcessingAfterQueueChange()
    End Sub
End Class
