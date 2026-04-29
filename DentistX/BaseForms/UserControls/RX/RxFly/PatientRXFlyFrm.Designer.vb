<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class PatientRXFlyFrm
    Inherits DevExpress.XtraEditors.XtraForm

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(PatientRXFlyFrm))
        Me.RxFlyCtl1 = New DentistX.RxFlyCtl()
        Me.SuspendLayout()
        '
        'RxFlyCtl1
        '
        resources.ApplyResources(Me.RxFlyCtl1, "RxFlyCtl1")
        Me.RxFlyCtl1.Name = "RxFlyCtl1"
        '
        'PatientRXFlyFrm
        '
        Me.Appearance.Options.UseFont = True
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.RxFlyCtl1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "PatientRXFlyFrm"
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents RxFlyCtl1 As RxFlyCtl
End Class
