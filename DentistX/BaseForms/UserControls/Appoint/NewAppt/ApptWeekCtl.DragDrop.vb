Imports System
Imports System.Collections.Generic
Imports System.Drawing
Imports System.Globalization
Imports System.Linq
Imports System.Runtime.InteropServices
Imports System.Windows.Forms
Imports DevExpress.Utils
Imports DevExpress.Utils.Layout
Imports DevExpress.XtraEditors

Partial Public Class ApptWeekCtl
    Private Sub RaiseEmptyDate(dayDate As DateTime)
        If InteractionHub IsNot Nothing Then
            InteractionHub.PublishEmptyDateInvoked(dayDate)
        End If
    End Sub

    Private Sub WireWeekDropTarget(c As Control, dayDate As DateTime)
        c.Tag = dayDate.Date
        c.AllowDrop = True
        AddHandler c.DragEnter, Sub(s, e) WeekColumn_DragEnter(e)
        AddHandler c.DragDrop, Sub(s, e) WeekColumn_DragDrop(dayDate.Date, e)
    End Sub

    Private Sub WeekColumn_DragEnter(e As DragEventArgs)
        If e.Data IsNot Nothing AndAlso e.Data.GetDataPresent("Appointment") Then
            e.Effect = DragDropEffects.Move
        Else
            e.Effect = DragDropEffects.None
        End If
    End Sub

    Private Sub WeekColumn_DragDrop(targetDay As Date, e As DragEventArgs)
        If e.Data Is Nothing OrElse Not e.Data.GetDataPresent("Appointment") Then Return
        Dim appt = TryCast(e.Data.GetData("Appointment"), AppointmentC)
        If appt Is Nothing OrElse InteractionHub Is Nothing Then Return
        Dim sourceDay = CDate(e.Data.GetData("SourceDay"))
        InteractionHub.PublishWeekColumnAppointmentDrop(appt, sourceDay, targetDay)
    End Sub

    Private Sub OnWeekCardDragMouseDown(card As ApptCard, sourceDay As Date, e As MouseEventArgs)
        If e.Button <> MouseButtons.Left Then Return
        If card.BoundAppointment Is Nothing OrElse card.BoundAppointment.AppointmentID <= 0 Then Return
        _weekDragTimer.Stop()
        _weekDragCard = card
        _weekDragSourceDay = sourceDay.Date
        _weekDragTimer.Interval = Math.Max(200, If(_request IsNot Nothing, _request.DragHoldTimeMs, 750))
        _weekDragTimer.Start()
    End Sub

    Private Sub OnWeekCardDragMouseUp(card As ApptCard, e As MouseEventArgs)
        _weekDragTimer.Stop()
        If Not _weekColumnDragInProgress Then
            _weekDragCard = Nothing
            _weekDragSourceDay = Nothing
        End If
    End Sub

    Private Sub WeekDragTimer_Tick(sender As Object, e As EventArgs)
        _weekDragTimer.Stop()
        If _weekDragCard Is Nothing OrElse Not _weekDragSourceDay.HasValue Then Return
        If _weekDragCard.BoundAppointment Is Nothing OrElse _weekDragCard.BoundAppointment.AppointmentID <= 0 Then Return
        _weekColumnDragInProgress = True
        StartWeekCardDrag(_weekDragCard, _weekDragSourceDay.Value)
    End Sub

    ''' <summary>Same payload as <see cref="SchedulerNew.StartLabelDrag"/> (Appointment + SourceDay + SourceDoctor).</summary>
    Private Sub StartWeekCardDrag(card As ApptCard, sourceDay As Date)
        Try
            Dim ap = card.BoundAppointment
            If ap Is Nothing Then Return
            Dim dragData As New DataObject()
            dragData.SetData("Appointment", ap)
            dragData.SetData("SourceDay", sourceDay.Date)
            dragData.SetData("SourceDoctor", ap.DrID)
            card.DoDragDrop(dragData, DragDropEffects.Move)
        Finally
            _weekColumnDragInProgress = False
            _weekDragCard = Nothing
            _weekDragSourceDay = Nothing
        End Try
    End Sub
End Class
