Public Class PatientChangedEventArgs
    Inherits EventArgs

    Public Property NewPatient() As Patient
    Public Property ShowHimAsKid As Boolean = False
    Public Property ForceUpdate As Boolean = False
    Public Sub New(ByVal _NewPatient As Patient,
                   ByVal Optional ShowAsKid As Boolean = False, ByVal Optional forceReload As Boolean = False)
        NewPatient = _NewPatient
        ShowHimAsKid = ShowAsKid
        ForceUpdate = forceReload
    End Sub
    'Public Property NewPatient() As Patient
    'Public Property ShowHimAsKid As Boolean = False
    'Public Property KidLabel As String
    'Public Property ForceUpdate As Boolean = False
    'Public Sub New(ByVal _NewPatient As Patient, ByVal kidText As String,
    '               ByVal Optional ShowAsKid As Boolean = False, ByVal Optional forceReload As Boolean = False)
    '    NewPatient = _NewPatient
    '    KidLabel = kidText
    '    ShowHimAsKid = ShowAsKid
    '    ForceUpdate = forceReload
    'End Sub

End Class
