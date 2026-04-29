<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class BasePatientWorkspace
    Inherits DevExpress.XtraEditors.XtraUserControl

    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    Private components As System.ComponentModel.IContainer

    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(BasePatientWorkspace))
        Me.SuspendLayout()
        '
        'BasePatientWorkspace
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Name = "BasePatientWorkspace"
        Me.ResumeLayout(False)

    End Sub
End Class
