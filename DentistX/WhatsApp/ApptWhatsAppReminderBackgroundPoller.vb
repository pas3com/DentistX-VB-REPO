Imports System.Threading
Imports System.Threading.Tasks

''' <summary>
''' Ensures due rows in dbo.ApptTwoHourWhatsAppQueue are processed every minute even when MainView timers are not loaded.
''' Started automatically when an appointment is synced into the queue.
''' </summary>
Public NotInheritable Class ApptWhatsAppReminderBackgroundPoller
    Private Sub New()
    End Sub

    Private Shared ReadOnly SyncRoot As New Object()
    Private Shared _timer As Timer
    Private Shared _tickBusy As Integer

    ''' <summary>Start the 60s polling loop if not already running (idempotent).</summary>
    Public Shared Sub EnsureStarted()
        SyncLock SyncRoot
            If _timer Is Nothing Then
                _timer = New Timer(AddressOf OnTimerTick, Nothing, 60000, 60000)
            End If
        End SyncLock
    End Sub

    ''' <summary>Run one processing pass soon (after a new queue row is written).</summary>
    Public Shared Sub RequestImmediatePoll()
        EnsureStarted()
        Task.Run(Async Function()
                     Try
                         Await AppointmentTwoHourReminderService.RunAsync()
                     Catch
                     End Try
                 End Function)
    End Sub

    Private Shared Sub OnTimerTick(state As Object)
        Task.Run(Async Function()
                     If Interlocked.CompareExchange(_tickBusy, 1, 0) <> 0 Then Return
                     Try
                         Await AppointmentTwoHourReminderService.RunAsync()
                     Catch
                     Finally
                         Interlocked.Exchange(_tickBusy, 0)
                     End Try
                 End Function)
    End Sub
End Class
