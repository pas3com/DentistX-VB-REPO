<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmAddPayAccnt
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmAddPayAccnt))
        Me.NotesText = New DevExpress.XtraEditors.MemoEdit()
        Me.PayValue = New DevExpress.XtraEditors.TextEdit()
        Me.PayDate = New DevExpress.XtraEditors.DateEdit()
        Me.LabelControl7 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl8 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl9 = New DevExpress.XtraEditors.LabelControl()
        Me.btnAddTrt = New DevExpress.XtraEditors.SimpleButton()
        Me.btnCancel = New DevExpress.XtraEditors.SimpleButton()
        Me.btnSave = New DevExpress.XtraEditors.SimpleButton()
        Me.lblTreat = New DevExpress.XtraEditors.LabelControl()
        Me.txtReceivedBy = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl5 = New DevExpress.XtraEditors.LabelControl()
        CType(Me.NotesText.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PayValue.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PayDate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PayDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtReceivedBy.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'NotesText
        '
        resources.ApplyResources(Me.NotesText, "NotesText")
        Me.NotesText.Name = "NotesText"
        Me.NotesText.Properties.Appearance.Font = CType(resources.GetObject("NotesText.Properties.Appearance.Font"), System.Drawing.Font)
        Me.NotesText.Properties.Appearance.Options.UseFont = True
        '
        'PayValue
        '
        resources.ApplyResources(Me.PayValue, "PayValue")
        Me.PayValue.Name = "PayValue"
        Me.PayValue.Properties.Appearance.Font = CType(resources.GetObject("PayValue.Properties.Appearance.Font"), System.Drawing.Font)
        Me.PayValue.Properties.Appearance.ForeColor = System.Drawing.Color.Red
        Me.PayValue.Properties.Appearance.Options.UseFont = True
        Me.PayValue.Properties.Appearance.Options.UseForeColor = True
        '
        'PayDate
        '
        resources.ApplyResources(Me.PayDate, "PayDate")
        Me.PayDate.Name = "PayDate"
        Me.PayDate.Properties.Appearance.Font = CType(resources.GetObject("PayDate.Properties.Appearance.Font"), System.Drawing.Font)
        Me.PayDate.Properties.Appearance.Options.UseFont = True
        Me.PayDate.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("PayDate.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me.PayDate.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("PayDate.Properties.CalendarTimeProperties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        '
        'LabelControl7
        '
        Me.LabelControl7.Appearance.Font = CType(resources.GetObject("LabelControl7.Appearance.Font"), System.Drawing.Font)
        Me.LabelControl7.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.LabelControl7, "LabelControl7")
        Me.LabelControl7.Name = "LabelControl7"
        '
        'LabelControl8
        '
        Me.LabelControl8.Appearance.Font = CType(resources.GetObject("LabelControl8.Appearance.Font"), System.Drawing.Font)
        Me.LabelControl8.Appearance.ForeColor = System.Drawing.Color.Red
        Me.LabelControl8.Appearance.Options.UseFont = True
        Me.LabelControl8.Appearance.Options.UseForeColor = True
        resources.ApplyResources(Me.LabelControl8, "LabelControl8")
        Me.LabelControl8.Name = "LabelControl8"
        '
        'LabelControl9
        '
        Me.LabelControl9.Appearance.Font = CType(resources.GetObject("LabelControl9.Appearance.Font"), System.Drawing.Font)
        Me.LabelControl9.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.LabelControl9, "LabelControl9")
        Me.LabelControl9.Name = "LabelControl9"
        '
        'btnAddTrt
        '
        Me.btnAddTrt.Appearance.Font = CType(resources.GetObject("btnAddTrt.Appearance.Font"), System.Drawing.Font)
        Me.btnAddTrt.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.btnAddTrt, "btnAddTrt")
        Me.btnAddTrt.Name = "btnAddTrt"
        '
        'btnCancel
        '
        Me.btnCancel.Appearance.Font = CType(resources.GetObject("btnCancel.Appearance.Font"), System.Drawing.Font)
        Me.btnCancel.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.btnCancel, "btnCancel")
        Me.btnCancel.Name = "btnCancel"
        '
        'btnSave
        '
        Me.btnSave.Appearance.Font = CType(resources.GetObject("btnSave.Appearance.Font"), System.Drawing.Font)
        Me.btnSave.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.btnSave, "btnSave")
        Me.btnSave.Name = "btnSave"
        '
        'lblTreat
        '
        Me.lblTreat.Appearance.Font = CType(resources.GetObject("lblTreat.Appearance.Font"), System.Drawing.Font)
        Me.lblTreat.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.lblTreat.Appearance.Options.UseFont = True
        Me.lblTreat.Appearance.Options.UseForeColor = True
        Me.lblTreat.Appearance.Options.UseTextOptions = True
        Me.lblTreat.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
        Me.lblTreat.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top
        Me.lblTreat.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
        resources.ApplyResources(Me.lblTreat, "lblTreat")
        Me.lblTreat.Name = "lblTreat"
        '
        'txtReceivedBy
        '
        resources.ApplyResources(Me.txtReceivedBy, "txtReceivedBy")
        Me.txtReceivedBy.Name = "txtReceivedBy"
        '
        'LabelControl5
        '
        Me.LabelControl5.Appearance.Font = CType(resources.GetObject("LabelControl5.Appearance.Font"), System.Drawing.Font)
        Me.LabelControl5.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.LabelControl5, "LabelControl5")
        Me.LabelControl5.Name = "LabelControl5"
        '
        'FrmAddPayAccnt
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.txtReceivedBy)
        Me.Controls.Add(Me.LabelControl5)
        Me.Controls.Add(Me.lblTreat)
        Me.Controls.Add(Me.btnAddTrt)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnSave)
        Me.Controls.Add(Me.NotesText)
        Me.Controls.Add(Me.PayValue)
        Me.Controls.Add(Me.PayDate)
        Me.Controls.Add(Me.LabelControl7)
        Me.Controls.Add(Me.LabelControl8)
        Me.Controls.Add(Me.LabelControl9)
        Me.Name = "FrmAddPayAccnt"
        CType(Me.NotesText.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PayValue.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PayDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PayDate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtReceivedBy.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents NotesText As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents PayValue As DevExpress.XtraEditors.TextEdit
    Friend WithEvents PayDate As DevExpress.XtraEditors.DateEdit
    Friend WithEvents LabelControl7 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl8 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl9 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents btnAddTrt As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnCancel As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnSave As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents lblTreat As DevExpress.XtraEditors.LabelControl
    Friend WithEvents txtReceivedBy As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl5 As DevExpress.XtraEditors.LabelControl
End Class
