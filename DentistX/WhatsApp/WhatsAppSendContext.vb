''' <summary>Optional metadata for WhatsApp sends (logged in WhatsAppMessageLog).</summary>
Public Class WhatsAppSendContext
    Public Property Category As String
    Public Property PatientId As Integer?
    Public Property SourceHint As String
    ''' <summary>Display name for logs (e.g. patient name).</summary>
    Public Property DisplayName As String

    ''' <summary>When true, the main shell may reveal the Message Center dock after this send (manual / UI sends only).</summary>
    Public Property RevealMessageCenter As Boolean

    ''' <summary>True when this send ran after a local scheduled queue item was due (e.g. appointment reminder queue), not an immediate UI send.</summary>
    Public Property SentAfterLocalQueue As Boolean

    ''' <summary>When true, WhatsAppService send skips toasts and session message-center updates (background / diagnostic sends).</summary>
    Public Property SuppressUiFeedback As Boolean

    ''' <summary>Set only by <see cref="WhatsAppOutboundDispatchService"/> replay: skips local outbox and POSTs straight to the gateway once.</summary>
    Friend Property BypassOutboundQueue As Boolean

    ''' <summary>Optional deterministic key for dbo.WhatsAppOutboundMessage.IdempotencyKey (max 128). When empty, GUID-based key is assigned.</summary>
    Public Property IdempotencyKey As String

    ''' <summary>For ~24h one-off reminders: appointment id stored on the outbox row (finalization differs from dbo.ApptTwoHourWhatsAppQueue).</summary>
    Public Property AppointmentId As Integer?

    ''' <summary>Appointment start snapshot (same convention as reminders; normalized to UTC in repository).</summary>
    Public Property AppointmentStartUtc As DateTime?

    ''' <summary>Earliest UTC instant the dispatcher may attempt send (defaults to now).</summary>
    Public Property OutboundNotBeforeUtc As DateTime?

    ''' <summary>Optional hard stop for retries after this UTC instant.</summary>
    Public Property OutboundExpiresAtUtc As DateTime?
End Class
