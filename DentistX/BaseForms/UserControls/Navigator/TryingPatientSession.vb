Public NotInheritable Class TryingPatientSession
    Private Shared ReadOnly _instance As New TryingPatientSession()
    Private _currentPatient As Patient

    Public Shared ReadOnly Property Instance As TryingPatientSession
        Get
            Return _instance
        End Get
    End Property

    Public Event CurrentPatientChanged(ByVal sender As Object, ByVal patient As Patient)

    Public Function GetCurrentPatient() As Patient
        Return _currentPatient
    End Function

    Public Sub SetCurrentPatient(ByVal patient As Patient)
        _currentPatient = patient
        RaiseEvent CurrentPatientChanged(Me, patient)
    End Sub

    Public Sub Clear()
        SetCurrentPatient(Nothing)
    End Sub
End Class
