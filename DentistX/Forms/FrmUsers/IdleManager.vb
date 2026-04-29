Imports System.Timers

Public Class IdleManager

    Private WithEvents idleTimer As Timer
    Private idleTimeLimit As TimeSpan
    Private lastInputTime As DateTime

    Public Event IdleDetected()

    Public Sub New(idleMinutes As Integer)
        idleTimeLimit = TimeSpan.FromMinutes(idleMinutes)
        idleTimer = New Timer(1000) ' Check every 1 second
        AddHandler idleTimer.Elapsed, AddressOf IdleTimer_Elapsed
    End Sub

    Public Sub Start()
        lastInputTime = DateTime.Now
        idleTimer.Start()
    End Sub

    Public Sub [Stop]()
        idleTimer.Stop()
    End Sub

    Public Sub ResetTimer()
        lastInputTime = DateTime.Now
    End Sub

    Private Sub IdleTimer_Elapsed(sender As Object, e As ElapsedEventArgs)
        If DateTime.Now - lastInputTime > idleTimeLimit Then
            idleTimer.Stop()
            RaiseEvent IdleDetected()
        End If
    End Sub

End Class
