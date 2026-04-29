<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmEditLoan
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmEditLoan))
        Me.LabelControl5 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl4 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl3 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.txtDescription = New DevExpress.XtraEditors.MemoEdit()
        Me.dtTransDate = New DevExpress.XtraEditors.DateEdit()
        Me.txtAmount = New DevExpress.XtraEditors.TextEdit()
        Me.cboTransType = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.btnSave = New DevExpress.XtraEditors.SimpleButton()
        Me.btnCancel = New DevExpress.XtraEditors.SimpleButton()
        Me.ContactsCombo1 = New DentistX.ContactsCombo()
        CType(Me.txtDescription.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtTransDate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtTransDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtAmount.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboTransType.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'LabelControl5
        '
        resources.ApplyResources(Me.LabelControl5, "LabelControl5")
        Me.LabelControl5.Appearance.Font = CType(resources.GetObject("LabelControl5.Appearance.Font"), System.Drawing.Font)
        Me.LabelControl5.Appearance.Options.UseFont = True
        Me.LabelControl5.Name = "LabelControl5"
        '
        'LabelControl4
        '
        resources.ApplyResources(Me.LabelControl4, "LabelControl4")
        Me.LabelControl4.Appearance.Font = CType(resources.GetObject("LabelControl4.Appearance.Font"), System.Drawing.Font)
        Me.LabelControl4.Appearance.Options.UseFont = True
        Me.LabelControl4.Name = "LabelControl4"
        '
        'LabelControl3
        '
        resources.ApplyResources(Me.LabelControl3, "LabelControl3")
        Me.LabelControl3.Appearance.Font = CType(resources.GetObject("LabelControl3.Appearance.Font"), System.Drawing.Font)
        Me.LabelControl3.Appearance.Options.UseFont = True
        Me.LabelControl3.Name = "LabelControl3"
        '
        'LabelControl2
        '
        resources.ApplyResources(Me.LabelControl2, "LabelControl2")
        Me.LabelControl2.Appearance.Font = CType(resources.GetObject("LabelControl2.Appearance.Font"), System.Drawing.Font)
        Me.LabelControl2.Appearance.Options.UseFont = True
        Me.LabelControl2.Name = "LabelControl2"
        '
        'LabelControl1
        '
        resources.ApplyResources(Me.LabelControl1, "LabelControl1")
        Me.LabelControl1.Appearance.Font = CType(resources.GetObject("LabelControl1.Appearance.Font"), System.Drawing.Font)
        Me.LabelControl1.Appearance.Options.UseFont = True
        Me.LabelControl1.Name = "LabelControl1"
        '
        'txtDescription
        '
        resources.ApplyResources(Me.txtDescription, "txtDescription")
        Me.txtDescription.EnterMoveNextControl = True
        Me.txtDescription.Name = "txtDescription"
        Me.txtDescription.Properties.Appearance.Font = CType(resources.GetObject("txtDescription.Properties.Appearance.Font"), System.Drawing.Font)
        Me.txtDescription.Properties.Appearance.Options.UseFont = True
        '
        'dtTransDate
        '
        resources.ApplyResources(Me.dtTransDate, "dtTransDate")
        Me.dtTransDate.EnterMoveNextControl = True
        Me.dtTransDate.Name = "dtTransDate"
        Me.dtTransDate.Properties.Appearance.Font = CType(resources.GetObject("dtTransDate.Properties.Appearance.Font"), System.Drawing.Font)
        Me.dtTransDate.Properties.Appearance.Options.UseFont = True
        Me.dtTransDate.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("dtTransDate.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me.dtTransDate.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("dtTransDate.Properties.CalendarTimeProperties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        '
        'txtAmount
        '
        resources.ApplyResources(Me.txtAmount, "txtAmount")
        Me.txtAmount.EnterMoveNextControl = True
        Me.txtAmount.Name = "txtAmount"
        Me.txtAmount.Properties.Appearance.Font = CType(resources.GetObject("txtAmount.Properties.Appearance.Font"), System.Drawing.Font)
        Me.txtAmount.Properties.Appearance.Options.UseFont = True
        '
        'cboTransType
        '
        resources.ApplyResources(Me.cboTransType, "cboTransType")
        Me.cboTransType.EnterMoveNextControl = True
        Me.cboTransType.Name = "cboTransType"
        Me.cboTransType.Properties.Appearance.Font = CType(resources.GetObject("cboTransType.Properties.Appearance.Font"), System.Drawing.Font)
        Me.cboTransType.Properties.Appearance.Options.UseFont = True
        Me.cboTransType.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("cboTransType.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        '
        'btnSave
        '
        resources.ApplyResources(Me.btnSave, "btnSave")
        Me.btnSave.Appearance.Font = CType(resources.GetObject("btnSave.Appearance.Font"), System.Drawing.Font)
        Me.btnSave.Appearance.Options.UseFont = True
        Me.btnSave.ImageOptions.ImageKey = resources.GetString("btnSave.ImageOptions.ImageKey")
        Me.btnSave.Name = "btnSave"
        '
        'btnCancel
        '
        resources.ApplyResources(Me.btnCancel, "btnCancel")
        Me.btnCancel.Appearance.Font = CType(resources.GetObject("btnCancel.Appearance.Font"), System.Drawing.Font)
        Me.btnCancel.Appearance.Options.UseFont = True
        Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCancel.ImageOptions.ImageKey = resources.GetString("btnCancel.ImageOptions.ImageKey")
        Me.btnCancel.Name = "btnCancel"
        '
        'ContactsCombo1
        '
        resources.ApplyResources(Me.ContactsCombo1, "ContactsCombo1")
        Me.ContactsCombo1.Appearance.Font = CType(resources.GetObject("ContactsCombo1.Appearance.Font"), System.Drawing.Font)
        Me.ContactsCombo1.Appearance.Options.UseFont = True
        Me.ContactsCombo1.ContactID = 0
        Me.ContactsCombo1.ContName = "ContactsCombo1"
        Me.ContactsCombo1.Name = "ContactsCombo1"
        '
        'FrmEditLoan
        '
        Me.AcceptButton = Me.btnSave
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.btnCancel
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnSave)
        Me.Controls.Add(Me.txtDescription)
        Me.Controls.Add(Me.LabelControl5)
        Me.Controls.Add(Me.dtTransDate)
        Me.Controls.Add(Me.LabelControl4)
        Me.Controls.Add(Me.txtAmount)
        Me.Controls.Add(Me.LabelControl3)
        Me.Controls.Add(Me.cboTransType)
        Me.Controls.Add(Me.LabelControl2)
        Me.Controls.Add(Me.ContactsCombo1)
        Me.Controls.Add(Me.LabelControl1)
        Me.Name = "FrmEditLoan"
        CType(Me.txtDescription.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtTransDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtTransDate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtAmount.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboTransType.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents txtDescription As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents LabelControl5 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents dtTransDate As DevExpress.XtraEditors.DateEdit
    Friend WithEvents LabelControl4 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents txtAmount As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl3 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents cboTransType As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents ContactsCombo1 As ContactsCombo
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents btnSave As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnCancel As DevExpress.XtraEditors.SimpleButton
End Class
