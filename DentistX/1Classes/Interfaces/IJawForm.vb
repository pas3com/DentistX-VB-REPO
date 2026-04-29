Public Interface IJawForm
    Inherits IDisposable
    Sub LoadTreatPatient(patientId As Integer)
    Event ToothClicked As EventHandler(Of ToothDoubleClickEvent)
End Interface