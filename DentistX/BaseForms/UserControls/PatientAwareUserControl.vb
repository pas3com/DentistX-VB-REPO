Imports DevExpress.XtraEditors

Public MustInherit Class PatientAwareUserControl
    Inherits XtraUserControl

    Public MustOverride Sub LoadPatientData(patientId As Integer)

    Protected Overrides Sub OnLoad(e As EventArgs)
        MyBase.OnLoad(e)
        Dim ws = PatientAwareHelper.FindPatientWorkspace(Me)
        If ws Is Nothing AndAlso FormManager.Instance.IsBasePatientFormOpen Then ws = FormManager.Instance.CurrentForm
        If ws IsNot Nothing Then
            AddHandler ws.PatientChanged, AddressOf OnPatientChanged
        End If
    End Sub

    Private Sub OnPatientChanged(sender As Object, patient As Patient)
        If patient IsNot Nothing Then
            LoadPatientData(patient.PatientID)
        Else
            LoadPatientData(-1)
        End If
    End Sub

    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(PatientAwareUserControl))
        Me.SuspendLayout()
        '
        'PatientAwareUserControl
        '
        resources.ApplyResources(Me, "$this")
        Me.Name = "PatientAwareUserControl"
        Me.ResumeLayout(False)

    End Sub
End Class