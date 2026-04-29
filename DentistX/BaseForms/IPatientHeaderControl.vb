''' <summary>Common interface for patient header controls (Navigator / Navigator2) used by BasePatientWorkspace.</summary>
Public Interface IPatientHeaderControl
    ReadOnly Property Current_Patient As Patient
    ReadOnly Property IsKidLabel As System.Windows.Forms.Control
    Sub LoadBal(patientId As Integer)
    Sub UpdateFilterTarget(filterTarget As String, Optional passedPatient As Patient = Nothing)
End Interface
