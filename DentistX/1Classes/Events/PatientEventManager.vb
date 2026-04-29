Public Class PatientEventManager
    ' Declare a public event for patient changes
    Public Shared Event PatientChanged As EventHandler(Of PatientChangedEventArgs)

    ' Method to raise the event
    Public Shared Sub RaisePatientChanged(newPatient As Patient, ByVal Optional ShoKid As Boolean = False, ByVal Optional force As Boolean = False)
        RaiseEvent PatientChanged(Nothing, New PatientChangedEventArgs(newPatient, ShoKid, force))
    End Sub


End Class
