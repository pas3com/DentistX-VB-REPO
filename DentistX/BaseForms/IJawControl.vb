Public Interface IJawControl
    Inherits IPatientAwareUserControl
    Inherits IToothClickable

    Property IsMobile As Boolean
    Sub HideShowDiv(hideDiv As Boolean)
    'Sub LoadTreat(patientId As Integer)
    'Sub EnsureSvgImagesSet()
End Interface
