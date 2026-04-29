<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class VisitCalendarDayControl
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
        Me.TablePanel2 = New DevExpress.Utils.Layout.TablePanel()
        Me.containerPanel2 = New DevExpress.XtraEditors.PanelControl()
        Me.TablePanel1 = New DevExpress.Utils.Layout.TablePanel()
        Me.lblMonthTitle = New DevExpress.XtraEditors.LabelControl()
        Me.btnNextYear = New DevExpress.XtraEditors.SimpleButton()
        Me.btnNextMonth = New DevExpress.XtraEditors.SimpleButton()
        Me.btnPrevMonth = New DevExpress.XtraEditors.SimpleButton()
        Me.btnPrevYear = New DevExpress.XtraEditors.SimpleButton()
        Me.cboLanguage = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.cboFirstDay = New DevExpress.XtraEditors.ComboBoxEdit()
        CType(Me.TablePanel2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TablePanel2.SuspendLayout()
        CType(Me.containerPanel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TablePanel1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TablePanel1.SuspendLayout()
        CType(Me.cboLanguage.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboFirstDay.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TablePanel2
        '
        Me.TablePanel2.Columns.AddRange(New DevExpress.Utils.Layout.TablePanelColumn() {New DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 1.46!), New DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 106.84!), New DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 1.7!)})
        Me.TablePanel2.Controls.Add(Me.containerPanel2)
        Me.TablePanel2.Controls.Add(Me.TablePanel1)
        Me.TablePanel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TablePanel2.Location = New System.Drawing.Point(0, 0)
        Me.TablePanel2.Name = "TablePanel2"
        Me.TablePanel2.Rows.AddRange(New DevExpress.Utils.Layout.TablePanelRow() {New DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 50.0!), New DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 26.0!)})
        Me.TablePanel2.Size = New System.Drawing.Size(740, 740)
        Me.TablePanel2.TabIndex = 2
        Me.TablePanel2.UseSkinIndents = True
        '
        'containerPanel2
        '
        Me.containerPanel2.AutoSize = True
        Me.TablePanel2.SetColumn(Me.containerPanel2, 1)
        Me.containerPanel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.containerPanel2.Location = New System.Drawing.Point(23, 62)
        Me.containerPanel2.Name = "containerPanel2"
        Me.TablePanel2.SetRow(Me.containerPanel2, 1)
        Me.containerPanel2.Size = New System.Drawing.Size(693, 665)
        Me.containerPanel2.TabIndex = 12
        '
        'TablePanel1
        '
        Me.TablePanel2.SetColumn(Me.TablePanel1, 0)
        Me.TablePanel1.Columns.AddRange(New DevExpress.Utils.Layout.TablePanelColumn() {New DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 30.2!), New DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 29.8!), New DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 43.7!), New DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 101.8!), New DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 41.84!), New DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 30.16!), New DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 32.5!)})
        Me.TablePanel2.SetColumnSpan(Me.TablePanel1, 3)
        Me.TablePanel1.Controls.Add(Me.lblMonthTitle)
        Me.TablePanel1.Controls.Add(Me.btnNextYear)
        Me.TablePanel1.Controls.Add(Me.btnNextMonth)
        Me.TablePanel1.Controls.Add(Me.cboLanguage)
        Me.TablePanel1.Controls.Add(Me.cboFirstDay)
        Me.TablePanel1.Controls.Add(Me.btnPrevMonth)
        Me.TablePanel1.Controls.Add(Me.btnPrevYear)
        Me.TablePanel1.Location = New System.Drawing.Point(13, 12)
        Me.TablePanel1.Name = "TablePanel1"
        Me.TablePanel2.SetRow(Me.TablePanel1, 0)
        Me.TablePanel1.Rows.AddRange(New DevExpress.Utils.Layout.TablePanelRow() {New DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 50.0!)})
        Me.TablePanel1.Size = New System.Drawing.Size(714, 45)
        Me.TablePanel1.TabIndex = 0
        Me.TablePanel1.UseSkinIndents = True
        '
        'lblMonthTitle
        '
        Me.lblMonthTitle.Appearance.Font = New System.Drawing.Font("Calibri", 12.0!, System.Drawing.FontStyle.Bold)
        Me.lblMonthTitle.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.lblMonthTitle.Appearance.Options.UseFont = True
        Me.lblMonthTitle.Appearance.Options.UseForeColor = True
        Me.lblMonthTitle.Appearance.Options.UseTextOptions = True
        Me.lblMonthTitle.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.lblMonthTitle.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.lblMonthTitle.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.TablePanel1.SetColumn(Me.lblMonthTitle, 3)
        Me.lblMonthTitle.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblMonthTitle.Location = New System.Drawing.Point(244, 12)
        Me.lblMonthTitle.Name = "lblMonthTitle"
        Me.TablePanel1.SetRow(Me.lblMonthTitle, 0)
        Me.lblMonthTitle.Size = New System.Drawing.Size(223, 20)
        Me.lblMonthTitle.TabIndex = 10
        Me.lblMonthTitle.Text = "LabelControl1"
        '
        'btnNextYear
        '
        Me.btnNextYear.Appearance.Font = New System.Drawing.Font("Calibri", 12.0!, System.Drawing.FontStyle.Bold)
        Me.btnNextYear.Appearance.ForeColor = System.Drawing.Color.Red
        Me.btnNextYear.Appearance.Options.UseFont = True
        Me.btnNextYear.Appearance.Options.UseForeColor = True
        Me.TablePanel1.SetColumn(Me.btnNextYear, 7)
        Me.btnNextYear.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnNextYear.Location = New System.Drawing.Point(632, 12)
        Me.btnNextYear.Name = "btnNextYear"
        Me.TablePanel1.SetRow(Me.btnNextYear, 0)
        Me.btnNextYear.Size = New System.Drawing.Size(69, 20)
        Me.btnNextYear.TabIndex = 9
        Me.btnNextYear.Text = ">>"
        '
        'btnNextMonth
        '
        Me.btnNextMonth.Appearance.Font = New System.Drawing.Font("Calibri", 12.0!, System.Drawing.FontStyle.Bold)
        Me.btnNextMonth.Appearance.ForeColor = System.Drawing.Color.GreenYellow
        Me.btnNextMonth.Appearance.Options.UseFont = True
        Me.btnNextMonth.Appearance.Options.UseForeColor = True
        Me.btnNextMonth.Appearance.Options.UseTextOptions = True
        Me.btnNextMonth.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.TablePanel1.SetColumn(Me.btnNextMonth, 5)
        Me.btnNextMonth.Location = New System.Drawing.Point(565, 12)
        Me.btnNextMonth.Name = "btnNextMonth"
        Me.TablePanel1.SetRow(Me.btnNextMonth, 0)
        Me.btnNextMonth.Size = New System.Drawing.Size(63, 20)
        Me.btnNextMonth.TabIndex = 8
        Me.btnNextMonth.Text = ">"
        '
        'btnPrevMonth
        '
        Me.btnPrevMonth.Appearance.Font = New System.Drawing.Font("Calibri", 12.0!, System.Drawing.FontStyle.Bold)
        Me.btnPrevMonth.Appearance.ForeColor = System.Drawing.Color.Lime
        Me.btnPrevMonth.Appearance.Options.UseFont = True
        Me.btnPrevMonth.Appearance.Options.UseForeColor = True
        Me.TablePanel1.SetColumn(Me.btnPrevMonth, 1)
        Me.btnPrevMonth.Location = New System.Drawing.Point(80, 12)
        Me.btnPrevMonth.Name = "btnPrevMonth"
        Me.TablePanel1.SetRow(Me.btnPrevMonth, 0)
        Me.btnPrevMonth.Size = New System.Drawing.Size(63, 20)
        Me.btnPrevMonth.TabIndex = 4
        Me.btnPrevMonth.Text = "<"
        '
        'btnPrevYear
        '
        Me.btnPrevYear.Appearance.Font = New System.Drawing.Font("Calibri", 12.0!, System.Drawing.FontStyle.Bold)
        Me.btnPrevYear.Appearance.ForeColor = System.Drawing.Color.Red
        Me.btnPrevYear.Appearance.Options.UseFont = True
        Me.btnPrevYear.Appearance.Options.UseForeColor = True
        Me.TablePanel1.SetColumn(Me.btnPrevYear, 0)
        Me.btnPrevYear.Location = New System.Drawing.Point(13, 12)
        Me.btnPrevYear.Name = "btnPrevYear"
        Me.TablePanel1.SetRow(Me.btnPrevYear, 0)
        Me.btnPrevYear.Size = New System.Drawing.Size(63, 20)
        Me.btnPrevYear.TabIndex = 3
        Me.btnPrevYear.Text = "<<"
        '
        'cboLanguage
        '
        Me.TablePanel1.SetColumn(Me.cboLanguage, 4)
        Me.cboLanguage.Location = New System.Drawing.Point(472, 12)
        Me.cboLanguage.Name = "cboLanguage"
        Me.cboLanguage.Properties.Appearance.Font = New System.Drawing.Font("Calibri", 10.0!, System.Drawing.FontStyle.Bold)
        Me.cboLanguage.Properties.Appearance.Options.UseFont = True
        Me.cboLanguage.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.TablePanel1.SetRow(Me.cboLanguage, 0)
        Me.cboLanguage.Size = New System.Drawing.Size(89, 22)
        Me.cboLanguage.TabIndex = 7
        '
        'cboFirstDay
        '
        Me.TablePanel1.SetColumn(Me.cboFirstDay, 2)
        Me.cboFirstDay.Location = New System.Drawing.Point(147, 12)
        Me.cboFirstDay.Name = "cboFirstDay"
        Me.cboFirstDay.Properties.Appearance.Font = New System.Drawing.Font("Calibri", 10.0!, System.Drawing.FontStyle.Bold)
        Me.cboFirstDay.Properties.Appearance.Options.UseFont = True
        Me.cboFirstDay.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.TablePanel1.SetRow(Me.cboFirstDay, 0)
        Me.cboFirstDay.Size = New System.Drawing.Size(94, 22)
        Me.cboFirstDay.TabIndex = 6
        '
        'VisitCalendarDayControl
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.TablePanel2)
        Me.Name = "VisitCalendarDayControl"
        Me.Size = New System.Drawing.Size(740, 740)
        CType(Me.TablePanel2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TablePanel2.ResumeLayout(False)
        Me.TablePanel2.PerformLayout()
        CType(Me.containerPanel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TablePanel1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TablePanel1.ResumeLayout(False)
        Me.TablePanel1.PerformLayout()
        CType(Me.cboLanguage.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboFirstDay.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents TablePanel2 As DevExpress.Utils.Layout.TablePanel
    Friend WithEvents containerPanel2 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents TablePanel1 As DevExpress.Utils.Layout.TablePanel
    Friend WithEvents lblMonthTitle As DevExpress.XtraEditors.LabelControl
    Friend WithEvents btnNextYear As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnNextMonth As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents cboLanguage As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents cboFirstDay As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents btnPrevMonth As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnPrevYear As DevExpress.XtraEditors.SimpleButton
End Class
