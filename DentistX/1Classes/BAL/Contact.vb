
Public Class Contact
    Public Property ContactID As Integer
    Public Property CName As String
    Public Property Phone As String
    Public Property Email As String
    Public Property Notes As String
    Public Property CreatedAt As DateTime
    ''' <summary>Country / trunk prefix for WhatsApp (e.g. +961), stored as entered.</summary>
    Public Property WhatsAppPrefix As String
    ''' <summary>Local WhatsApp number digits (without prefix).</summary>
    Public Property WhatsApp As String
End Class