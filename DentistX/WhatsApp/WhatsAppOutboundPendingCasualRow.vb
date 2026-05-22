''' <summary>Light row for listing cancellable WhatsApp outbound UI sends (<see cref="WhatsAppOutboundRepository.ListPendingCasualForClinic"/>).</summary>
Public Class WhatsAppOutboundPendingCasualRow
    Public Property MessageId As Long
    Public Property MessageCategory As String
    Public Property TargetDigits As String
    ''' <summary>Short preview / caption text.</summary>
    Public Property MessagePreview As String
    Public Property AttachmentPath As String
    Public Property CreatedAtUtc As DateTime

    Public ReadOnly Property AttachmentDisplay As String
        Get
            Return GetAttachmentCaption()
        End Get
    End Property

    Public Function GetAttachmentCaption() As String
        Dim p = If(AttachmentPath, "").Trim()
        If p.Length = 0 Then Return If(Eng, "(no file)", "(بدون ملف)")
        Try
            Dim name = IO.Path.GetFileName(p)
            Return If(String.IsNullOrWhiteSpace(name), p, name)
        Catch
            Return p
        End Try
    End Function
End Class
