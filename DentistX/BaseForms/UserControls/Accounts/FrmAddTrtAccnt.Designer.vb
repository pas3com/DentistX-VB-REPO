<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmAddTrtAccnt
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmAddTrtAccnt))
        Me.TrtDiscount = New DevExpress.XtraEditors.TextEdit()
        Me.TrtDate = New DevExpress.XtraEditors.DateEdit()
        Me.LabelControl3 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl23 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.btnSave = New DevExpress.XtraEditors.SimpleButton()
        Me.btnCancel = New DevExpress.XtraEditors.SimpleButton()
        Me.lblSecDisc = New DevExpress.XtraEditors.LabelControl()
        Me.TrtSecDiscount = New DevExpress.XtraEditors.TextEdit()
        Me.TrtValue = New DevExpress.XtraEditors.TextEdit()
        Me.chkSecDiscount = New DevExpress.XtraEditors.CheckEdit()
        Me.DetailText = New DevExpress.XtraEditors.MemoEdit()
        Me.btnAddTrt = New DevExpress.XtraEditors.SimpleButton()
        Me.txtWhats = New DevExpress.XtraEditors.TextEdit()
        Me.lblWhats = New DevExpress.XtraEditors.LabelControl()
        Me.cboPrefix = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.LabelControl4 = New DevExpress.XtraEditors.LabelControl()
        CType(Me.TrtDiscount.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TrtDate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TrtDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TrtSecDiscount.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TrtValue.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkSecDiscount.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DetailText.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtWhats.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboPrefix.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TrtDiscount
        '
        resources.ApplyResources(Me.TrtDiscount, "TrtDiscount")
        Me.TrtDiscount.Name = "TrtDiscount"
        Me.TrtDiscount.Properties.Appearance.Font = CType(resources.GetObject("TrtDiscount.Properties.Appearance.Font"), System.Drawing.Font)
        Me.TrtDiscount.Properties.Appearance.ForeColor = System.Drawing.Color.Red
        Me.TrtDiscount.Properties.Appearance.Options.UseFont = True
        Me.TrtDiscount.Properties.Appearance.Options.UseForeColor = True
        '
        'TrtDate
        '
        resources.ApplyResources(Me.TrtDate, "TrtDate")
        Me.TrtDate.Name = "TrtDate"
        Me.TrtDate.Properties.Appearance.Font = CType(resources.GetObject("TrtDate.Properties.Appearance.Font"), System.Drawing.Font)
        Me.TrtDate.Properties.Appearance.Options.UseFont = True
        Me.TrtDate.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("TrtDate.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me.TrtDate.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("TrtDate.Properties.CalendarTimeProperties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        '
        'LabelControl3
        '
        Me.LabelControl3.Appearance.Font = CType(resources.GetObject("LabelControl3.Appearance.Font"), System.Drawing.Font)
        Me.LabelControl3.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.LabelControl3, "LabelControl3")
        Me.LabelControl3.Name = "LabelControl3"
        '
        'LabelControl23
        '
        Me.LabelControl23.Appearance.Font = CType(resources.GetObject("LabelControl23.Appearance.Font"), System.Drawing.Font)
        Me.LabelControl23.Appearance.ForeColor = System.Drawing.Color.Red
        Me.LabelControl23.Appearance.Options.UseFont = True
        Me.LabelControl23.Appearance.Options.UseForeColor = True
        resources.ApplyResources(Me.LabelControl23, "LabelControl23")
        Me.LabelControl23.Name = "LabelControl23"
        '
        'LabelControl2
        '
        Me.LabelControl2.Appearance.Font = CType(resources.GetObject("LabelControl2.Appearance.Font"), System.Drawing.Font)
        Me.LabelControl2.Appearance.ForeColor = System.Drawing.Color.Red
        Me.LabelControl2.Appearance.Options.UseFont = True
        Me.LabelControl2.Appearance.Options.UseForeColor = True
        resources.ApplyResources(Me.LabelControl2, "LabelControl2")
        Me.LabelControl2.Name = "LabelControl2"
        '
        'LabelControl1
        '
        Me.LabelControl1.Appearance.Font = CType(resources.GetObject("LabelControl1.Appearance.Font"), System.Drawing.Font)
        Me.LabelControl1.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.LabelControl1, "LabelControl1")
        Me.LabelControl1.Name = "LabelControl1"
        '
        'btnSave
        '
        Me.btnSave.Appearance.Font = CType(resources.GetObject("btnSave.Appearance.Font"), System.Drawing.Font)
        Me.btnSave.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.btnSave, "btnSave")
        Me.btnSave.Name = "btnSave"
        '
        'btnCancel
        '
        Me.btnCancel.Appearance.Font = CType(resources.GetObject("btnCancel.Appearance.Font"), System.Drawing.Font)
        Me.btnCancel.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.btnCancel, "btnCancel")
        Me.btnCancel.Name = "btnCancel"
        '
        'lblSecDisc
        '
        Me.lblSecDisc.Appearance.Font = CType(resources.GetObject("lblSecDisc.Appearance.Font"), System.Drawing.Font)
        Me.lblSecDisc.Appearance.ForeColor = System.Drawing.Color.Red
        Me.lblSecDisc.Appearance.Options.UseFont = True
        Me.lblSecDisc.Appearance.Options.UseForeColor = True
        resources.ApplyResources(Me.lblSecDisc, "lblSecDisc")
        Me.lblSecDisc.Name = "lblSecDisc"
        '
        'TrtSecDiscount
        '
        resources.ApplyResources(Me.TrtSecDiscount, "TrtSecDiscount")
        Me.TrtSecDiscount.Name = "TrtSecDiscount"
        Me.TrtSecDiscount.Properties.Appearance.Font = CType(resources.GetObject("TrtSecDiscount.Properties.Appearance.Font"), System.Drawing.Font)
        Me.TrtSecDiscount.Properties.Appearance.ForeColor = System.Drawing.Color.Red
        Me.TrtSecDiscount.Properties.Appearance.Options.UseFont = True
        Me.TrtSecDiscount.Properties.Appearance.Options.UseForeColor = True
        '
        'TrtValue
        '
        resources.ApplyResources(Me.TrtValue, "TrtValue")
        Me.TrtValue.Name = "TrtValue"
        Me.TrtValue.Properties.Appearance.Font = CType(resources.GetObject("TrtValue.Properties.Appearance.Font"), System.Drawing.Font)
        Me.TrtValue.Properties.Appearance.ForeColor = System.Drawing.Color.Red
        Me.TrtValue.Properties.Appearance.Options.UseFont = True
        Me.TrtValue.Properties.Appearance.Options.UseForeColor = True
        '
        'chkSecDiscount
        '
        resources.ApplyResources(Me.chkSecDiscount, "chkSecDiscount")
        Me.chkSecDiscount.Name = "chkSecDiscount"
        Me.chkSecDiscount.Properties.Appearance.Font = CType(resources.GetObject("chkSecDiscount.Properties.Appearance.Font"), System.Drawing.Font)
        Me.chkSecDiscount.Properties.Appearance.Options.UseFont = True
        Me.chkSecDiscount.Properties.Caption = resources.GetString("chkSecDiscount.Properties.Caption")
        '
        'DetailText
        '
        resources.ApplyResources(Me.DetailText, "DetailText")
        Me.DetailText.Name = "DetailText"
        Me.DetailText.Properties.Appearance.Font = CType(resources.GetObject("DetailText.Properties.Appearance.Font"), System.Drawing.Font)
        Me.DetailText.Properties.Appearance.Options.UseFont = True
        '
        'btnAddTrt
        '
        Me.btnAddTrt.Appearance.Font = CType(resources.GetObject("btnAddTrt.Appearance.Font"), System.Drawing.Font)
        Me.btnAddTrt.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.btnAddTrt, "btnAddTrt")
        Me.btnAddTrt.Name = "btnAddTrt"
        '
        'txtWhats
        '
        Me.txtWhats.EnterMoveNextControl = True
        resources.ApplyResources(Me.txtWhats, "txtWhats")
        Me.txtWhats.Name = "txtWhats"
        Me.txtWhats.Properties.Appearance.Font = CType(resources.GetObject("txtWhats.Properties.Appearance.Font"), System.Drawing.Font)
        Me.txtWhats.Properties.Appearance.Options.UseFont = True
        Me.txtWhats.Properties.MaxLength = 10
        '
        'lblWhats
        '
        Me.lblWhats.Appearance.Font = CType(resources.GetObject("lblWhats.Appearance.Font"), System.Drawing.Font)
        Me.lblWhats.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.lblWhats.Appearance.Options.UseFont = True
        Me.lblWhats.Appearance.Options.UseForeColor = True
        resources.ApplyResources(Me.lblWhats, "lblWhats")
        Me.lblWhats.Name = "lblWhats"
        '
        'cboPrefix
        '
        resources.ApplyResources(Me.cboPrefix, "cboPrefix")
        Me.cboPrefix.Name = "cboPrefix"
        Me.cboPrefix.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("cboPrefix.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        '
        'LabelControl4
        '
        Me.LabelControl4.Appearance.Font = CType(resources.GetObject("LabelControl4.Appearance.Font"), System.Drawing.Font)
        Me.LabelControl4.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.LabelControl4, "LabelControl4")
        Me.LabelControl4.Name = "LabelControl4"
        '
        'FrmAddTrtAccnt
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.txtWhats)
        Me.Controls.Add(Me.lblWhats)
        Me.Controls.Add(Me.cboPrefix)
        Me.Controls.Add(Me.btnAddTrt)
        Me.Controls.Add(Me.chkSecDiscount)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnSave)
        Me.Controls.Add(Me.TrtValue)
        Me.Controls.Add(Me.TrtSecDiscount)
        Me.Controls.Add(Me.TrtDiscount)
        Me.Controls.Add(Me.TrtDate)
        Me.Controls.Add(Me.LabelControl4)
        Me.Controls.Add(Me.lblSecDisc)
        Me.Controls.Add(Me.LabelControl3)
        Me.Controls.Add(Me.LabelControl23)
        Me.Controls.Add(Me.LabelControl2)
        Me.Controls.Add(Me.LabelControl1)
        Me.Controls.Add(Me.DetailText)
        Me.Name = "FrmAddTrtAccnt"
        CType(Me.TrtDiscount.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TrtDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TrtDate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TrtSecDiscount.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TrtValue.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkSecDiscount.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DetailText.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtWhats.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboPrefix.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents TrtDiscount As DevExpress.XtraEditors.TextEdit
    Friend WithEvents TrtDate As DevExpress.XtraEditors.DateEdit
    Friend WithEvents LabelControl3 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl23 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents btnSave As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnCancel As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents lblSecDisc As DevExpress.XtraEditors.LabelControl
    Friend WithEvents TrtSecDiscount As DevExpress.XtraEditors.TextEdit
    Friend WithEvents TrtValue As DevExpress.XtraEditors.TextEdit
    Friend WithEvents chkSecDiscount As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents DetailText As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents btnAddTrt As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents txtWhats As DevExpress.XtraEditors.TextEdit
    Friend WithEvents lblWhats As DevExpress.XtraEditors.LabelControl
    Friend WithEvents cboPrefix As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents LabelControl4 As DevExpress.XtraEditors.LabelControl
End Class
