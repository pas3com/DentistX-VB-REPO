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
End Class
