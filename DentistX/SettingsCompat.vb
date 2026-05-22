''' <summary>
''' Compatibility wrapper for typed datasets/designer code that still reference
''' `Global.DentistX.Settings.Default` after the settings generator moved to `My.Settings`.
''' With the VB root namespace set to `DentistX`, a top-level `Settings` class compiles as `DentistX.Settings`.
''' </summary>
Friend NotInheritable Class Settings
    Private Sub New()
    End Sub

    Public Shared ReadOnly Property [Default] As My.MySettings
        Get
            Return My.Settings
        End Get
    End Property
End Class
