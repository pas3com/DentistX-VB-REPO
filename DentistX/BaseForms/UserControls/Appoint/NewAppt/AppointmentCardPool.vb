Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Windows.Forms

''' <summary>Reuses <see cref="ApptCard"/> instances inside a host control (week column, drawer, dialog).</summary>
Friend NotInheritable Class AppointmentCardPool
    Public Shared Function Ensure(
            host As Control,
            requiredCount As Integer,
            dayDate As Date,
            wireCard As Action(Of ApptCard, Date)) As List(Of ApptCard)

        If host Is Nothing Then Return New List(Of ApptCard)()
        Dim cards = host.Controls.OfType(Of ApptCard)().ToList()
        While cards.Count < requiredCount
            Dim card As New ApptCard()
            If wireCard IsNot Nothing Then wireCard(card, dayDate)
            host.Controls.Add(card)
            cards.Add(card)
        End While
        For i = 0 To cards.Count - 1
            Dim showCard = i < requiredCount
            cards(i).Visible = showCard
            If showCard AndAlso wireCard IsNot Nothing Then wireCard(cards(i), dayDate)
        Next
        If requiredCount > 0 Then
            For i = Math.Min(requiredCount, cards.Count) - 1 To 0 Step -1
                host.Controls.SetChildIndex(cards(i), 0)
            Next
        End If
        Return cards
    End Function
End Class
