<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormAbout
    Inherits DevExpress.XtraEditors.XtraForm

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormAbout))
        Me.XtraTabControl1 = New DevExpress.XtraTab.XtraTabControl()
        Me.TabAbout = New DevExpress.XtraTab.XtraTabPage()
        Me.txtAbout = New DevExpress.XtraEditors.MemoEdit()
        Me.TabStatus = New DevExpress.XtraTab.XtraTabPage()
        Me.txtStatus = New DevExpress.XtraEditors.MemoEdit()
        Me.TabRequestLicense = New DevExpress.XtraTab.XtraTabPage()
        Me.btnApplyLicense = New DevExpress.XtraEditors.SimpleButton()
        Me.btnRequestLicense = New DevExpress.XtraEditors.SimpleButton()
        CType(Me.XtraTabControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.XtraTabControl1.SuspendLayout()
        Me.TabAbout.SuspendLayout()
        CType(Me.txtAbout.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabStatus.SuspendLayout()
        CType(Me.txtStatus.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabRequestLicense.SuspendLayout()
        Me.SuspendLayout()
        '
        'XtraTabControl1
        '
        resources.ApplyResources(Me.XtraTabControl1, "XtraTabControl1")
        Me.XtraTabControl1.Appearance.Font = CType(resources.GetObject("XtraTabControl1.Appearance.Font"), System.Drawing.Font)
        Me.XtraTabControl1.Appearance.Options.UseFont = True
        Me.XtraTabControl1.AppearancePage.Header.Font = CType(resources.GetObject("XtraTabControl1.AppearancePage.Header.Font"), System.Drawing.Font)
        Me.XtraTabControl1.AppearancePage.Header.Options.UseFont = True
        Me.XtraTabControl1.Name = "XtraTabControl1"
        Me.XtraTabControl1.SelectedTabPage = Me.TabAbout
        Me.XtraTabControl1.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.TabAbout, Me.TabStatus, Me.TabRequestLicense})
        '
        'TabAbout
        '
        resources.ApplyResources(Me.TabAbout, "TabAbout")
        Me.TabAbout.Controls.Add(Me.txtAbout)
        Me.TabAbout.Name = "TabAbout"
        '
        'txtAbout
        '
        resources.ApplyResources(Me.txtAbout, "txtAbout")
        Me.txtAbout.Name = "txtAbout"
        Me.txtAbout.Properties.ReadOnly = True
        '
        'TabStatus
        '
        resources.ApplyResources(Me.TabStatus, "TabStatus")
        Me.TabStatus.Controls.Add(Me.txtStatus)
        Me.TabStatus.Name = "TabStatus"
        '
        'txtStatus
        '
        resources.ApplyResources(Me.txtStatus, "txtStatus")
        Me.txtStatus.Name = "txtStatus"
        '
        'TabRequestLicense
        '
        resources.ApplyResources(Me.TabRequestLicense, "TabRequestLicense")
        Me.TabRequestLicense.Controls.Add(Me.btnApplyLicense)
        Me.TabRequestLicense.Controls.Add(Me.btnRequestLicense)
        Me.TabRequestLicense.Name = "TabRequestLicense"
        '
        'btnApplyLicense
        '
        resources.ApplyResources(Me.btnApplyLicense, "btnApplyLicense")
        Me.btnApplyLicense.Appearance.Font = CType(resources.GetObject("btnApplyLicense.Appearance.Font"), System.Drawing.Font)
        Me.btnApplyLicense.Appearance.Options.UseFont = True
        Me.btnApplyLicense.ImageOptions.ImageKey = resources.GetString("btnApplyLicense.ImageOptions.ImageKey")
        Me.btnApplyLicense.Name = "btnApplyLicense"
        '
        'btnRequestLicense
        '
        resources.ApplyResources(Me.btnRequestLicense, "btnRequestLicense")
        Me.btnRequestLicense.Appearance.Font = CType(resources.GetObject("btnRequestLicense.Appearance.Font"), System.Drawing.Font)
        Me.btnRequestLicense.Appearance.Options.UseFont = True
        Me.btnRequestLicense.ImageOptions.ImageKey = resources.GetString("btnRequestLicense.ImageOptions.ImageKey")
        Me.btnRequestLicense.Name = "btnRequestLicense"
        '
        'FormAbout
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.XtraTabControl1)
        Me.Name = "FormAbout"
        CType(Me.XtraTabControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.XtraTabControl1.ResumeLayout(False)
        Me.TabAbout.ResumeLayout(False)
        CType(Me.txtAbout.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabStatus.ResumeLayout(False)
        CType(Me.txtStatus.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabRequestLicense.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents XtraTabControl1 As DevExpress.XtraTab.XtraTabControl
    Friend WithEvents TabAbout As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents TabStatus As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents TabRequestLicense As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents txtAbout As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents txtStatus As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents btnApplyLicense As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnRequestLicense As DevExpress.XtraEditors.SimpleButton
End Class
