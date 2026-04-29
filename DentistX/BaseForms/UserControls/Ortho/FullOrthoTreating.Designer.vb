<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FullOrthoTreating
    Inherits DevExpress.XtraEditors.XtraUserControl

    'UserControl overrides dispose to clean up the component list.
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FullOrthoTreating))
        Me.OpenNewOrth1 = New DentistX.OpenNewOrthCTL()
        Me.OrthoTreating1 = New DentistX.OrthoTreatingCTL()
        Me.SuspendLayout()
        '
        'OpenNewOrth1
        '
        Me.OpenNewOrth1.Appearance.Font = CType(resources.GetObject("OpenNewOrth1.Appearance.Font"), System.Drawing.Font)
        Me.OpenNewOrth1.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.OpenNewOrth1, "OpenNewOrth1")
        Me.OpenNewOrth1.Name = "OpenNewOrth1"
        '
        'OrthoTreating1
        '
        resources.ApplyResources(Me.OrthoTreating1, "OrthoTreating1")
        Me.OrthoTreating1.Name = "OrthoTreating1"
        '
        'FullOrthoTreating
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.OpenNewOrth1)
        Me.Controls.Add(Me.OrthoTreating1)
        Me.Name = "FullOrthoTreating"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents OrthoTreating1 As OrthoTreatingCTL
    Friend WithEvents OpenNewOrth1 As OpenNewOrthCTL
End Class
