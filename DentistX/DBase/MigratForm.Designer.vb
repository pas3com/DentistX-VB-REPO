<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MigratForm
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
        Me.PanelControl1 = New DevExpress.XtraEditors.PanelControl()
        Me.PanelControl2 = New DevExpress.XtraEditors.PanelControl()
        Me.PanelControl3 = New DevExpress.XtraEditors.PanelControl()
        Me.LogTxt = New DevExpress.XtraEditors.MemoEdit()
        Me.ButtonMigrateData = New DevExpress.XtraEditors.SimpleButton()
        Me.btnBackup = New DevExpress.XtraEditors.SimpleButton()
        Me.btnClearLog = New DevExpress.XtraEditors.SimpleButton()
        Me.btnShowLineCount = New DevExpress.XtraEditors.SimpleButton()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl1.SuspendLayout()
        CType(Me.PanelControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PanelControl3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl3.SuspendLayout()
        CType(Me.LogTxt.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'PanelControl1
        '
        Me.PanelControl1.Controls.Add(Me.btnShowLineCount)
        Me.PanelControl1.Controls.Add(Me.btnClearLog)
        Me.PanelControl1.Controls.Add(Me.btnBackup)
        Me.PanelControl1.Controls.Add(Me.ButtonMigrateData)
        Me.PanelControl1.Dock = System.Windows.Forms.DockStyle.Top
        Me.PanelControl1.Location = New System.Drawing.Point(0, 0)
        Me.PanelControl1.Name = "PanelControl1"
        Me.PanelControl1.Size = New System.Drawing.Size(1080, 81)
        Me.PanelControl1.TabIndex = 0
        '
        'PanelControl2
        '
        Me.PanelControl2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.PanelControl2.Location = New System.Drawing.Point(0, 490)
        Me.PanelControl2.Name = "PanelControl2"
        Me.PanelControl2.Size = New System.Drawing.Size(1080, 60)
        Me.PanelControl2.TabIndex = 1
        '
        'PanelControl3
        '
        Me.PanelControl3.Controls.Add(Me.LogTxt)
        Me.PanelControl3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelControl3.Location = New System.Drawing.Point(0, 81)
        Me.PanelControl3.Name = "PanelControl3"
        Me.PanelControl3.Size = New System.Drawing.Size(1080, 409)
        Me.PanelControl3.TabIndex = 2
        '
        'LogTxt
        '
        Me.LogTxt.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LogTxt.Location = New System.Drawing.Point(2, 2)
        Me.LogTxt.Name = "LogTxt"
        Me.LogTxt.Size = New System.Drawing.Size(1076, 405)
        Me.LogTxt.TabIndex = 0
        '
        'ButtonMigrateData
        '
        Me.ButtonMigrateData.Appearance.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Bold)
        Me.ButtonMigrateData.Appearance.Options.UseFont = True
        Me.ButtonMigrateData.Location = New System.Drawing.Point(112, 28)
        Me.ButtonMigrateData.Name = "ButtonMigrateData"
        Me.ButtonMigrateData.Size = New System.Drawing.Size(105, 23)
        Me.ButtonMigrateData.TabIndex = 25
        Me.ButtonMigrateData.Text = "Migration"
        '
        'btnBackup
        '
        Me.btnBackup.Appearance.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Bold)
        Me.btnBackup.Appearance.Options.UseFont = True
        Me.btnBackup.Location = New System.Drawing.Point(250, 28)
        Me.btnBackup.Name = "btnBackup"
        Me.btnBackup.Size = New System.Drawing.Size(105, 23)
        Me.btnBackup.TabIndex = 26
        Me.btnBackup.Text = "Backup DB"
        '
        'btnClearLog
        '
        Me.btnClearLog.Appearance.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Bold)
        Me.btnClearLog.Appearance.Options.UseFont = True
        Me.btnClearLog.Location = New System.Drawing.Point(413, 28)
        Me.btnClearLog.Name = "btnClearLog"
        Me.btnClearLog.Size = New System.Drawing.Size(105, 23)
        Me.btnClearLog.TabIndex = 25
        Me.btnClearLog.Text = "Clear Log"
        '
        'btnShowLineCount
        '
        Me.btnShowLineCount.Appearance.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Bold)
        Me.btnShowLineCount.Appearance.Options.UseFont = True
        Me.btnShowLineCount.Location = New System.Drawing.Point(551, 28)
        Me.btnShowLineCount.Name = "btnShowLineCount"
        Me.btnShowLineCount.Size = New System.Drawing.Size(105, 23)
        Me.btnShowLineCount.TabIndex = 26
        Me.btnShowLineCount.Text = "Show Line Count"
        '
        'MigratForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1080, 550)
        Me.Controls.Add(Me.PanelControl3)
        Me.Controls.Add(Me.PanelControl2)
        Me.Controls.Add(Me.PanelControl1)
        Me.Name = "MigratForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "MigratForm"
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl1.ResumeLayout(False)
        CType(Me.PanelControl2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PanelControl3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl3.ResumeLayout(False)
        CType(Me.LogTxt.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents PanelControl1 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents PanelControl2 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents PanelControl3 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents LogTxt As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents ButtonMigrateData As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnBackup As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnShowLineCount As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnClearLog As DevExpress.XtraEditors.SimpleButton
End Class
