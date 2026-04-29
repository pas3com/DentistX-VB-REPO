<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class WhatsNormal
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(WhatsNormal))
        Me.LblSendMessage = New DevExpress.XtraEditors.LabelControl()
        Me.TxtSendMessage = New DevExpress.XtraEditors.MemoEdit()
        Me.LblSendFile = New DevExpress.XtraEditors.LabelControl()
        Me.TxtSendFile = New DevExpress.XtraEditors.ButtonEdit()
        Me.BtnSendMessage = New DevExpress.XtraEditors.SimpleButton()
        Me.lblSendTo = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        Me.lblWhats = New DevExpress.XtraEditors.LabelControl()
        Me.cboPrefix = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.txtWhats = New DevExpress.XtraEditors.TextEdit()
        CType(Me.TxtSendMessage.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtSendFile.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboPrefix.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtWhats.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'LblSendMessage
        '
        resources.ApplyResources(Me.LblSendMessage, "LblSendMessage")
        Me.LblSendMessage.Appearance.Font = CType(resources.GetObject("LblSendMessage.Appearance.Font"), System.Drawing.Font)
        Me.LblSendMessage.Appearance.Options.UseFont = True
        Me.LblSendMessage.Name = "LblSendMessage"
        '
        'TxtSendMessage
        '
        resources.ApplyResources(Me.TxtSendMessage, "TxtSendMessage")
        Me.TxtSendMessage.Name = "TxtSendMessage"
        Me.TxtSendMessage.Properties.Appearance.Font = CType(resources.GetObject("TxtSendMessage.Properties.Appearance.Font"), System.Drawing.Font)
        Me.TxtSendMessage.Properties.Appearance.Options.UseFont = True
        '
        'LblSendFile
        '
        resources.ApplyResources(Me.LblSendFile, "LblSendFile")
        Me.LblSendFile.Appearance.Font = CType(resources.GetObject("LblSendFile.Appearance.Font"), System.Drawing.Font)
        Me.LblSendFile.Appearance.Options.UseFont = True
        Me.LblSendFile.Name = "LblSendFile"
        '
        'TxtSendFile
        '
        resources.ApplyResources(Me.TxtSendFile, "TxtSendFile")
        Me.TxtSendFile.Name = "TxtSendFile"
        Me.TxtSendFile.Properties.Appearance.Font = CType(resources.GetObject("TxtSendFile.Properties.Appearance.Font"), System.Drawing.Font)
        Me.TxtSendFile.Properties.Appearance.Options.UseFont = True
        Me.TxtSendFile.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        '
        'BtnSendMessage
        '
        resources.ApplyResources(Me.BtnSendMessage, "BtnSendMessage")
        Me.BtnSendMessage.Appearance.Font = CType(resources.GetObject("BtnSendMessage.Appearance.Font"), System.Drawing.Font)
        Me.BtnSendMessage.Appearance.Options.UseFont = True
        Me.BtnSendMessage.ImageOptions.ImageKey = resources.GetString("BtnSendMessage.ImageOptions.ImageKey")
        Me.BtnSendMessage.Name = "BtnSendMessage"
        '
        'lblSendTo
        '
        resources.ApplyResources(Me.lblSendTo, "lblSendTo")
        Me.lblSendTo.Appearance.Font = CType(resources.GetObject("lblSendTo.Appearance.Font"), System.Drawing.Font)
        Me.lblSendTo.Appearance.Options.UseFont = True
        Me.lblSendTo.Name = "lblSendTo"
        '
        'LabelControl2
        '
        resources.ApplyResources(Me.LabelControl2, "LabelControl2")
        Me.LabelControl2.Appearance.Font = CType(resources.GetObject("LabelControl2.Appearance.Font"), System.Drawing.Font)
        Me.LabelControl2.Appearance.Options.UseFont = True
        Me.LabelControl2.Name = "LabelControl2"
        '
        'lblWhats
        '
        resources.ApplyResources(Me.lblWhats, "lblWhats")
        Me.lblWhats.Appearance.Font = CType(resources.GetObject("lblWhats.Appearance.Font"), System.Drawing.Font)
        Me.lblWhats.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.lblWhats.Appearance.Options.UseFont = True
        Me.lblWhats.Appearance.Options.UseForeColor = True
        Me.lblWhats.Name = "lblWhats"
        '
        'cboPrefix
        '
        resources.ApplyResources(Me.cboPrefix, "cboPrefix")
        Me.cboPrefix.Name = "cboPrefix"
        Me.cboPrefix.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("cboPrefix.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        '
        'txtWhats
        '
        resources.ApplyResources(Me.txtWhats, "txtWhats")
        Me.txtWhats.Name = "txtWhats"
        Me.txtWhats.Properties.Appearance.Font = CType(resources.GetObject("txtWhats.Properties.Appearance.Font"), System.Drawing.Font)
        Me.txtWhats.Properties.Appearance.Options.UseFont = True
        Me.txtWhats.Properties.MaskSettings.Set("MaskManagerType", GetType(DevExpress.Data.Mask.SimpleMaskManager))
        Me.txtWhats.Properties.MaskSettings.Set("MaskManagerSignature", "ignoreMaskBlank=True")
        Me.txtWhats.Properties.MaskSettings.Set("mask", "0000000000")
        Me.txtWhats.Properties.MaskSettings.Set("placeholder", Global.Microsoft.VisualBasic.ChrW(0))
        Me.txtWhats.Properties.MaxLength = 10
        '
        'WhatsNormal
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.lblWhats)
        Me.Controls.Add(Me.cboPrefix)
        Me.Controls.Add(Me.txtWhats)
        Me.Controls.Add(Me.lblSendTo)
        Me.Controls.Add(Me.LabelControl2)
        Me.Controls.Add(Me.LblSendMessage)
        Me.Controls.Add(Me.TxtSendMessage)
        Me.Controls.Add(Me.LblSendFile)
        Me.Controls.Add(Me.TxtSendFile)
        Me.Controls.Add(Me.BtnSendMessage)
        Me.Name = "WhatsNormal"
        CType(Me.TxtSendMessage.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtSendFile.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboPrefix.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtWhats.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents LblSendMessage As DevExpress.XtraEditors.LabelControl
    Friend WithEvents TxtSendMessage As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents LblSendFile As DevExpress.XtraEditors.LabelControl
    Friend WithEvents TxtSendFile As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents BtnSendMessage As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents lblSendTo As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblWhats As DevExpress.XtraEditors.LabelControl
    Friend WithEvents cboPrefix As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents txtWhats As DevExpress.XtraEditors.TextEdit
End Class
