<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class WaitForm1
    Inherits DevExpress.XtraWaitForm.WaitForm

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(WaitForm1))
        Me.progressPanel1 = New DevExpress.XtraWaitForm.ProgressPanel()
        Me.tableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.tableLayoutPanel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'progressPanel1
        '
        resources.ApplyResources(Me.progressPanel1, "progressPanel1")
        Me.progressPanel1.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.progressPanel1.Appearance.Options.UseBackColor = True
        Me.progressPanel1.AppearanceCaption.Font = CType(resources.GetObject("resource.Font"), System.Drawing.Font)
        Me.progressPanel1.AppearanceCaption.Options.UseFont = True
        Me.progressPanel1.AppearanceDescription.Font = CType(resources.GetObject("resource.Font1"), System.Drawing.Font)
        Me.progressPanel1.AppearanceDescription.Options.UseFont = True
        Me.progressPanel1.ImageHorzOffset = 20
        Me.progressPanel1.Name = "progressPanel1"
        '
        'tableLayoutPanel1
        '
        resources.ApplyResources(Me.tableLayoutPanel1, "tableLayoutPanel1")
        Me.tableLayoutPanel1.BackColor = System.Drawing.Color.Transparent
        Me.tableLayoutPanel1.Controls.Add(Me.progressPanel1, 0, 0)
        Me.tableLayoutPanel1.Name = "tableLayoutPanel1"
        '
        'WaitForm1
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.tableLayoutPanel1)
        Me.DoubleBuffered = True
        Me.Name = "WaitForm1"
        Me.tableLayoutPanel1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Private WithEvents progressPanel1 As DevExpress.XtraWaitForm.ProgressPanel
    Private WithEvents tableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
End Class
