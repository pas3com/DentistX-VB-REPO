Public Class RxPrint
    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub
    Public Sub New(ByVal PatientID As Integer, ByVal RxID As Integer)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        PatientRXVIEWTableAdapter.FillBy2ID(DsRx1.PatientRXVIEW, PatientID, RxID)
    End Sub
End Class