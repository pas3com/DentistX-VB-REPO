<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class LicenseRecoveryForm
    Inherits DevExpress.XtraEditors.XtraForm

    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    Private components As System.ComponentModel.IContainer

    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.memoMessage = New DevExpress.XtraEditors.MemoEdit()
        Me.lblPath = New DevExpress.XtraEditors.LabelControl()
        Me.panelButtons = New DevExpress.XtraEditors.PanelControl()
        Me.btnExit = New DevExpress.XtraEditors.SimpleButton()
        Me.btnOpenDataFolder = New DevExpress.XtraEditors.SimpleButton()
        Me.btnOpenAppFolder = New DevExpress.XtraEditors.SimpleButton()
        Me.btnSaveRequestToRequests = New DevExpress.XtraEditors.SimpleButton()
        Me.btnRequest = New DevExpress.XtraEditors.SimpleButton()
        Me.btnApply = New DevExpress.XtraEditors.SimpleButton()
        Me.btnImport = New DevExpress.XtraEditors.SimpleButton()
        Me.btnRepair = New DevExpress.XtraEditors.SimpleButton()
        CType(Me.memoMessage.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.panelButtons, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.panelButtons.SuspendLayout()
        Me.SuspendLayout()
        '
        'memoMessage
        '
        Me.memoMessage.Dock = System.Windows.Forms.DockStyle.Top
        Me.memoMessage.Location = New System.Drawing.Point(0, 23)
        Me.memoMessage.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.memoMessage.Name = "memoMessage"
        Me.memoMessage.Properties.Appearance.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Bold)
        Me.memoMessage.Properties.Appearance.Options.UseFont = True
        Me.memoMessage.Properties.ReadOnly = True
        Me.memoMessage.Size = New System.Drawing.Size(635, 125)
        Me.memoMessage.TabIndex = 0
        '
        'lblPath
        '
        Me.lblPath.Appearance.Font = New System.Drawing.Font("Calibri", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPath.Appearance.Options.UseFont = True
        Me.lblPath.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.Vertical
        Me.lblPath.Dock = System.Windows.Forms.DockStyle.Top
        Me.lblPath.Location = New System.Drawing.Point(0, 0)
        Me.lblPath.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.lblPath.Name = "lblPath"
        Me.lblPath.Padding = New System.Windows.Forms.Padding(9, 5, 9, 5)
        Me.lblPath.Size = New System.Drawing.Size(635, 23)
        Me.lblPath.TabIndex = 1
        Me.lblPath.Text = "Expected file:"
        '
        'panelButtons
        '
        Me.panelButtons.Controls.Add(Me.btnOpenAppFolder)
        Me.panelButtons.Controls.Add(Me.btnSaveRequestToRequests)
        Me.panelButtons.Controls.Add(Me.btnExit)
        Me.panelButtons.Controls.Add(Me.btnOpenDataFolder)
        Me.panelButtons.Controls.Add(Me.btnRequest)
        Me.panelButtons.Controls.Add(Me.btnApply)
        Me.panelButtons.Controls.Add(Me.btnImport)
        Me.panelButtons.Controls.Add(Me.btnRepair)
        Me.panelButtons.Dock = System.Windows.Forms.DockStyle.Fill
        Me.panelButtons.Location = New System.Drawing.Point(0, 0)
        Me.panelButtons.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.panelButtons.Name = "panelButtons"
        Me.panelButtons.Size = New System.Drawing.Size(635, 415)
        Me.panelButtons.TabIndex = 2
        '
        'btnExit
        '
        Me.btnExit.Appearance.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Bold)
        Me.btnExit.Appearance.Options.UseFont = True
        Me.btnExit.Location = New System.Drawing.Point(203, 345)
        Me.btnExit.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(224, 32)
        Me.btnExit.TabIndex = 6
        Me.btnExit.Text = "Exit application"
        '
        'btnOpenDataFolder
        '
        Me.btnOpenDataFolder.Appearance.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Bold)
        Me.btnOpenDataFolder.Appearance.Options.UseFont = True
        Me.btnOpenDataFolder.Location = New System.Drawing.Point(327, 263)
        Me.btnOpenDataFolder.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.btnOpenDataFolder.Name = "btnOpenDataFolder"
        Me.btnOpenDataFolder.Size = New System.Drawing.Size(289, 32)
        Me.btnOpenDataFolder.TabIndex = 5
        Me.btnOpenDataFolder.Text = "Open license data folder"
        Me.btnOpenDataFolder.Visible = False
        '
        'btnOpenAppFolder
        '
        Me.btnOpenAppFolder.Appearance.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Bold)
        Me.btnOpenAppFolder.Appearance.Options.UseFont = True
        Me.btnOpenAppFolder.Location = New System.Drawing.Point(177, 263)
        Me.btnOpenAppFolder.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.btnOpenAppFolder.Name = "btnOpenAppFolder"
        Me.btnOpenAppFolder.Size = New System.Drawing.Size(280, 32)
        Me.btnOpenAppFolder.TabIndex = 4
        Me.btnOpenAppFolder.Text = "Open application folder"
        '
        'btnSaveRequestToRequests
        '
        Me.btnSaveRequestToRequests.Appearance.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Bold)
        Me.btnSaveRequestToRequests.Appearance.Options.UseFont = True
        Me.btnSaveRequestToRequests.Location = New System.Drawing.Point(19, 303)
        Me.btnSaveRequestToRequests.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.btnSaveRequestToRequests.Name = "btnSaveRequestToRequests"
        Me.btnSaveRequestToRequests.Size = New System.Drawing.Size(597, 32)
        Me.btnSaveRequestToRequests.TabIndex = 7
        Me.btnSaveRequestToRequests.Text = "Save license request to Requests folder"
        Me.btnSaveRequestToRequests.Visible = False
        '
        'btnRequest
        '
        Me.btnRequest.Appearance.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Bold)
        Me.btnRequest.Appearance.Options.UseFont = True
        Me.btnRequest.Location = New System.Drawing.Point(327, 226)
        Me.btnRequest.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.btnRequest.Name = "btnRequest"
        Me.btnRequest.Size = New System.Drawing.Size(289, 32)
        Me.btnRequest.TabIndex = 3
        Me.btnRequest.Text = "Create license request..."
        '
        'btnApply
        '
        Me.btnApply.Appearance.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Bold)
        Me.btnApply.Appearance.Options.UseFont = True
        Me.btnApply.Location = New System.Drawing.Point(19, 226)
        Me.btnApply.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.btnApply.Name = "btnApply"
        Me.btnApply.Size = New System.Drawing.Size(280, 32)
        Me.btnApply.TabIndex = 2
        Me.btnApply.Text = "Apply license response..."
        '
        'btnImport
        '
        Me.btnImport.Appearance.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Bold)
        Me.btnImport.Appearance.Options.UseFont = True
        Me.btnImport.Location = New System.Drawing.Point(327, 180)
        Me.btnImport.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.btnImport.Name = "btnImport"
        Me.btnImport.Size = New System.Drawing.Size(289, 32)
        Me.btnImport.TabIndex = 1
        Me.btnImport.Text = "Use license file from another location..."
        '
        'btnRepair
        '
        Me.btnRepair.Appearance.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Bold)
        Me.btnRepair.Appearance.Options.UseFont = True
        Me.btnRepair.Location = New System.Drawing.Point(19, 180)
        Me.btnRepair.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.btnRepair.Name = "btnRepair"
        Me.btnRepair.Size = New System.Drawing.Size(280, 32)
        Me.btnRepair.TabIndex = 0
        Me.btnRepair.Text = "Repair license file (use data on this PC)"
        '
        'LicenseRecoveryForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(635, 415)
        Me.Controls.Add(Me.memoMessage)
        Me.Controls.Add(Me.lblPath)
        Me.Controls.Add(Me.panelButtons)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "LicenseRecoveryForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "License — action required"
        CType(Me.memoMessage.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.panelButtons, System.ComponentModel.ISupportInitialize).EndInit()
        Me.panelButtons.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents memoMessage As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents lblPath As DevExpress.XtraEditors.LabelControl
    Friend WithEvents panelButtons As DevExpress.XtraEditors.PanelControl
    Friend WithEvents btnRepair As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnImport As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnApply As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnRequest As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnOpenAppFolder As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnSaveRequestToRequests As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnOpenDataFolder As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnExit As DevExpress.XtraEditors.SimpleButton
End Class
