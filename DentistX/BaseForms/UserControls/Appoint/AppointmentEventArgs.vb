
Public Class AppointmentEventArgs
    Inherits EventArgs
    Public Property Appointment As AppointmentC
End Class

Public Class DateEventArgs
    Inherits EventArgs
    Public Property ClickedDate As DateTime
    Public Property ClickedTime As TimeSpan? ' optional, used for day view time-slot clicks
End Class

