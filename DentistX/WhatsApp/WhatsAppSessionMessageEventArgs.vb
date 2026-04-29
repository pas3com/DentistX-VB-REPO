Imports System

Public Class WhatsAppSessionMessageEventArgs
    Inherits EventArgs

    Public Sub New(row As WhatsAppActivityLogRow, revealMessageCenterDock As Boolean)
        Me.Row = row
        Me.RevealMessageCenterDock = revealMessageCenterDock
    End Sub

    Public ReadOnly Property Row As WhatsAppActivityLogRow

    ''' <summary>When true, the shell may show the Message Center dock tab for the first time this session (manual WhatsApp sends).</summary>
    Public ReadOnly Property RevealMessageCenterDock As Boolean
End Class
