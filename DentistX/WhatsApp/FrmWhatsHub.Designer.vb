<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmWhatsHub
    Inherits DevExpress.XtraEditors.XtraForm

    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    Private components As System.ComponentModel.IContainer

    Friend WithEvents TableLayoutRoot As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents LblIntro As DevExpress.XtraEditors.LabelControl
    Friend WithEvents BtnConnectionSend As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents BtnMessageLog As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents BtnAutoReminderQueue As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents BtnBulkAppointments As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents BtnBulkAccounts As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents BtnApptReminderSchedule As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents BtnAccountReminderSchedule As DevExpress.XtraEditors.SimpleButton

    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim uiFont As New System.Drawing.Font("Calibri", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TableLayoutRoot = New System.Windows.Forms.TableLayoutPanel()
        Me.LblIntro = New DevExpress.XtraEditors.LabelControl()
        Me.BtnConnectionSend = New DevExpress.XtraEditors.SimpleButton()
        Me.BtnMessageLog = New DevExpress.XtraEditors.SimpleButton()
        Me.BtnAutoReminderQueue = New DevExpress.XtraEditors.SimpleButton()
        Me.BtnBulkAppointments = New DevExpress.XtraEditors.SimpleButton()
        Me.BtnBulkAccounts = New DevExpress.XtraEditors.SimpleButton()
        Me.BtnApptReminderSchedule = New DevExpress.XtraEditors.SimpleButton()
        Me.BtnAccountReminderSchedule = New DevExpress.XtraEditors.SimpleButton()
        Me.TableLayoutRoot.SuspendLayout()
        Me.SuspendLayout()
        '
        'TableLayoutRoot
        '
        Me.TableLayoutRoot.ColumnCount = 2
        Me.TableLayoutRoot.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutRoot.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutRoot.Controls.Add(Me.LblIntro, 0, 0)
        Me.TableLayoutRoot.Controls.Add(Me.BtnConnectionSend, 0, 1)
        Me.TableLayoutRoot.Controls.Add(Me.BtnMessageLog, 1, 1)
        Me.TableLayoutRoot.Controls.Add(Me.BtnAutoReminderQueue, 0, 2)
        Me.TableLayoutRoot.Controls.Add(Me.BtnBulkAppointments, 1, 2)
        Me.TableLayoutRoot.Controls.Add(Me.BtnBulkAccounts, 0, 3)
        Me.TableLayoutRoot.Controls.Add(Me.BtnApptReminderSchedule, 1, 3)
        Me.TableLayoutRoot.Controls.Add(Me.BtnAccountReminderSchedule, 0, 4)
        Me.TableLayoutRoot.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutRoot.Font = uiFont
        Me.TableLayoutRoot.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutRoot.Name = "TableLayoutRoot"
        Me.TableLayoutRoot.Padding = New System.Windows.Forms.Padding(14)
        Me.TableLayoutRoot.RowCount = 5
        Me.TableLayoutRoot.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 52.0!))
        Me.TableLayoutRoot.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutRoot.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutRoot.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutRoot.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutRoot.Size = New System.Drawing.Size(548, 386)
        Me.TableLayoutRoot.TabIndex = 0
        '
        'LblIntro
        '
        Me.LblIntro.Appearance.Font = uiFont
        Me.LblIntro.Appearance.Options.UseFont = True
        Me.LblIntro.Appearance.Options.UseTextOptions = True
        Me.LblIntro.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
        Me.LblIntro.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.Vertical
        Me.TableLayoutRoot.SetColumnSpan(Me.LblIntro, 2)
        Me.LblIntro.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LblIntro.Location = New System.Drawing.Point(17, 14)
        Me.LblIntro.Name = "LblIntro"
        Me.LblIntro.Size = New System.Drawing.Size(514, 46)
        Me.LblIntro.TabIndex = 0
        Me.LblIntro.Text = "Open WhatsApp tools from this screen."
        '
        'BtnConnectionSend
        '
        Me.BtnConnectionSend.Appearance.Font = uiFont
        Me.BtnConnectionSend.Appearance.Options.UseFont = True
        Me.BtnConnectionSend.Dock = System.Windows.Forms.DockStyle.Fill
        Me.BtnConnectionSend.Location = New System.Drawing.Point(17, 66)
        Me.BtnConnectionSend.Name = "BtnConnectionSend"
        Me.BtnConnectionSend.Size = New System.Drawing.Size(254, 65)
        Me.BtnConnectionSend.TabIndex = 1
        Me.BtnConnectionSend.Text = "Connection && send"
        '
        'BtnMessageLog
        '
        Me.BtnMessageLog.Appearance.Font = uiFont
        Me.BtnMessageLog.Appearance.Options.UseFont = True
        Me.BtnMessageLog.Dock = System.Windows.Forms.DockStyle.Fill
        Me.BtnMessageLog.Location = New System.Drawing.Point(277, 66)
        Me.BtnMessageLog.Name = "BtnMessageLog"
        Me.BtnMessageLog.Size = New System.Drawing.Size(254, 65)
        Me.BtnMessageLog.TabIndex = 2
        Me.BtnMessageLog.Text = "Message log"
        '
        'BtnAutoReminderQueue
        '
        Me.BtnAutoReminderQueue.Appearance.Font = uiFont
        Me.BtnAutoReminderQueue.Appearance.Options.UseFont = True
        Me.BtnAutoReminderQueue.Dock = System.Windows.Forms.DockStyle.Fill
        Me.BtnAutoReminderQueue.Location = New System.Drawing.Point(17, 137)
        Me.BtnAutoReminderQueue.Name = "BtnAutoReminderQueue"
        Me.BtnAutoReminderQueue.Size = New System.Drawing.Size(254, 65)
        Me.BtnAutoReminderQueue.TabIndex = 3
        Me.BtnAutoReminderQueue.Text = "Auto reminder queue (24h / 2h)"
        '
        'BtnBulkAppointments
        '
        Me.BtnBulkAppointments.Appearance.Font = uiFont
        Me.BtnBulkAppointments.Appearance.Options.UseFont = True
        Me.BtnBulkAppointments.Dock = System.Windows.Forms.DockStyle.Fill
        Me.BtnBulkAppointments.Location = New System.Drawing.Point(277, 137)
        Me.BtnBulkAppointments.Name = "BtnBulkAppointments"
        Me.BtnBulkAppointments.Size = New System.Drawing.Size(254, 65)
        Me.BtnBulkAppointments.TabIndex = 4
        Me.BtnBulkAppointments.Text = "Bulk appointment WhatsApp"
        '
        'BtnBulkAccounts
        '
        Me.BtnBulkAccounts.Appearance.Font = uiFont
        Me.BtnBulkAccounts.Appearance.Options.UseFont = True
        Me.BtnBulkAccounts.Dock = System.Windows.Forms.DockStyle.Fill
        Me.BtnBulkAccounts.Location = New System.Drawing.Point(17, 208)
        Me.BtnBulkAccounts.Name = "BtnBulkAccounts"
        Me.BtnBulkAccounts.Size = New System.Drawing.Size(254, 65)
        Me.BtnBulkAccounts.TabIndex = 5
        Me.BtnBulkAccounts.Text = "Bulk account WhatsApp"
        '
        'BtnApptReminderSchedule
        '
        Me.BtnApptReminderSchedule.Appearance.Font = uiFont
        Me.BtnApptReminderSchedule.Appearance.Options.UseFont = True
        Me.BtnApptReminderSchedule.Dock = System.Windows.Forms.DockStyle.Fill
        Me.BtnApptReminderSchedule.Location = New System.Drawing.Point(277, 208)
        Me.BtnApptReminderSchedule.Name = "BtnApptReminderSchedule"
        Me.BtnApptReminderSchedule.Size = New System.Drawing.Size(254, 65)
        Me.BtnApptReminderSchedule.TabIndex = 6
        Me.BtnApptReminderSchedule.Text = "Appointment reminder schedule"
        '
        'BtnAccountReminderSchedule
        '
        Me.BtnAccountReminderSchedule.Appearance.Font = uiFont
        Me.BtnAccountReminderSchedule.Appearance.Options.UseFont = True
        Me.TableLayoutRoot.SetColumnSpan(Me.BtnAccountReminderSchedule, 2)
        Me.BtnAccountReminderSchedule.Dock = System.Windows.Forms.DockStyle.Fill
        Me.BtnAccountReminderSchedule.Location = New System.Drawing.Point(17, 279)
        Me.BtnAccountReminderSchedule.Name = "BtnAccountReminderSchedule"
        Me.BtnAccountReminderSchedule.Size = New System.Drawing.Size(514, 65)
        Me.BtnAccountReminderSchedule.TabIndex = 7
        Me.BtnAccountReminderSchedule.Text = "Account reminder schedule"
        '
        'FrmWhatsHub
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(548, 386)
        Me.Controls.Add(Me.TableLayoutRoot)
        Me.MinimumSize = New System.Drawing.Size(480, 360)
        Me.Name = "FrmWhatsHub"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "WhatsApp hub"
        Me.TableLayoutRoot.ResumeLayout(False)
        Me.TableLayoutRoot.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
End Class
