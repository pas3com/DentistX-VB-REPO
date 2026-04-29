<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmAddPayment
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmAddPayment))
        Me.lblBalanceDue = New DevExpress.XtraEditors.LabelControl()
        Me.btnSave = New DevExpress.XtraEditors.SimpleButton()
        Me.btnCancel = New DevExpress.XtraEditors.SimpleButton()
        Me.txtPayValue = New DevExpress.XtraEditors.SpinEdit()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        Me.lblInvoiceNumber = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl4 = New DevExpress.XtraEditors.LabelControl()
        Me.lblPatientName = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl5 = New DevExpress.XtraEditors.LabelControl()
        Me.lblTotalAmount = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl6 = New DevExpress.XtraEditors.LabelControl()
        Me.lblAmountPaid = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl7 = New DevExpress.XtraEditors.LabelControl()
        Me.dtpPayDate = New DevExpress.XtraEditors.DateEdit()
        Me.LabelControl3 = New DevExpress.XtraEditors.LabelControl()
        Me.cbPayType = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.LabelControl8 = New DevExpress.XtraEditors.LabelControl()
        Me.lblChqDueDate = New DevExpress.XtraEditors.LabelControl()
        Me.dtpChqDueDate = New DevExpress.XtraEditors.DateEdit()
        Me.btnResetChqs = New DevExpress.XtraEditors.SimpleButton()
        Me.txtChqOwner = New DevExpress.XtraEditors.TextEdit()
        Me.chkIsCashed = New DevExpress.XtraEditors.CheckEdit()
        Me.txtChqBank = New DevExpress.XtraEditors.TextEdit()
        Me.lblAccountNumber = New DevExpress.XtraEditors.LabelControl()
        Me.txtChqNumber = New DevExpress.XtraEditors.TextEdit()
        Me.lblChqNumber = New DevExpress.XtraEditors.LabelControl()
        Me.txtAccountNumber = New DevExpress.XtraEditors.TextEdit()
        Me.lblChqBank = New DevExpress.XtraEditors.LabelControl()
        Me.lblChqOwner = New DevExpress.XtraEditors.LabelControl()
        Me.txtChqValue = New DevExpress.XtraEditors.TextEdit()
        Me.lblChqValue = New DevExpress.XtraEditors.LabelControl()
        Me.txtNotes = New DevExpress.XtraEditors.MemoEdit()
        Me.lblNotes = New DevExpress.XtraEditors.LabelControl()
        CType(Me.txtPayValue.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpPayDate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpPayDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cbPayType.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpChqDueDate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpChqDueDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtChqOwner.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkIsCashed.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtChqBank.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtChqNumber.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtAccountNumber.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtChqValue.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNotes.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lblBalanceDue
        '
        Me.lblBalanceDue.Appearance.Font = CType(resources.GetObject("lblBalanceDue.Appearance.Font"), System.Drawing.Font)
        Me.lblBalanceDue.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.lblBalanceDue, "lblBalanceDue")
        Me.lblBalanceDue.Name = "lblBalanceDue"
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
        'txtPayValue
        '
        resources.ApplyResources(Me.txtPayValue, "txtPayValue")
        Me.txtPayValue.Name = "txtPayValue"
        Me.txtPayValue.Properties.Appearance.Font = CType(resources.GetObject("txtPayValue.Properties.Appearance.Font"), System.Drawing.Font)
        Me.txtPayValue.Properties.Appearance.Options.UseFont = True
        Me.txtPayValue.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("txtPayValue.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        '
        'LabelControl1
        '
        Me.LabelControl1.Appearance.Font = CType(resources.GetObject("LabelControl1.Appearance.Font"), System.Drawing.Font)
        Me.LabelControl1.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.LabelControl1, "LabelControl1")
        Me.LabelControl1.Name = "LabelControl1"
        '
        'LabelControl2
        '
        Me.LabelControl2.Appearance.Font = CType(resources.GetObject("LabelControl2.Appearance.Font"), System.Drawing.Font)
        Me.LabelControl2.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.LabelControl2, "LabelControl2")
        Me.LabelControl2.Name = "LabelControl2"
        '
        'lblInvoiceNumber
        '
        Me.lblInvoiceNumber.Appearance.Font = CType(resources.GetObject("lblInvoiceNumber.Appearance.Font"), System.Drawing.Font)
        Me.lblInvoiceNumber.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.lblInvoiceNumber, "lblInvoiceNumber")
        Me.lblInvoiceNumber.Name = "lblInvoiceNumber"
        '
        'LabelControl4
        '
        Me.LabelControl4.Appearance.Font = CType(resources.GetObject("LabelControl4.Appearance.Font"), System.Drawing.Font)
        Me.LabelControl4.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.LabelControl4, "LabelControl4")
        Me.LabelControl4.Name = "LabelControl4"
        '
        'lblPatientName
        '
        Me.lblPatientName.Appearance.Font = CType(resources.GetObject("lblPatientName.Appearance.Font"), System.Drawing.Font)
        Me.lblPatientName.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.lblPatientName, "lblPatientName")
        Me.lblPatientName.Name = "lblPatientName"
        '
        'LabelControl5
        '
        Me.LabelControl5.Appearance.Font = CType(resources.GetObject("LabelControl5.Appearance.Font"), System.Drawing.Font)
        Me.LabelControl5.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.LabelControl5, "LabelControl5")
        Me.LabelControl5.Name = "LabelControl5"
        '
        'lblTotalAmount
        '
        Me.lblTotalAmount.Appearance.Font = CType(resources.GetObject("lblTotalAmount.Appearance.Font"), System.Drawing.Font)
        Me.lblTotalAmount.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.lblTotalAmount, "lblTotalAmount")
        Me.lblTotalAmount.Name = "lblTotalAmount"
        '
        'LabelControl6
        '
        Me.LabelControl6.Appearance.Font = CType(resources.GetObject("LabelControl6.Appearance.Font"), System.Drawing.Font)
        Me.LabelControl6.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.LabelControl6, "LabelControl6")
        Me.LabelControl6.Name = "LabelControl6"
        '
        'lblAmountPaid
        '
        Me.lblAmountPaid.Appearance.Font = CType(resources.GetObject("lblAmountPaid.Appearance.Font"), System.Drawing.Font)
        Me.lblAmountPaid.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.lblAmountPaid, "lblAmountPaid")
        Me.lblAmountPaid.Name = "lblAmountPaid"
        '
        'LabelControl7
        '
        Me.LabelControl7.Appearance.Font = CType(resources.GetObject("LabelControl7.Appearance.Font"), System.Drawing.Font)
        Me.LabelControl7.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.LabelControl7, "LabelControl7")
        Me.LabelControl7.Name = "LabelControl7"
        '
        'dtpPayDate
        '
        resources.ApplyResources(Me.dtpPayDate, "dtpPayDate")
        Me.dtpPayDate.Name = "dtpPayDate"
        Me.dtpPayDate.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("dtpPayDate.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me.dtpPayDate.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("dtpPayDate.Properties.CalendarTimeProperties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        '
        'LabelControl3
        '
        Me.LabelControl3.Appearance.Font = CType(resources.GetObject("LabelControl3.Appearance.Font"), System.Drawing.Font)
        Me.LabelControl3.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.LabelControl3, "LabelControl3")
        Me.LabelControl3.Name = "LabelControl3"
        '
        'cbPayType
        '
        resources.ApplyResources(Me.cbPayType, "cbPayType")
        Me.cbPayType.Name = "cbPayType"
        Me.cbPayType.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("cbPayType.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        '
        'LabelControl8
        '
        Me.LabelControl8.Appearance.Font = CType(resources.GetObject("LabelControl8.Appearance.Font"), System.Drawing.Font)
        Me.LabelControl8.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.LabelControl8, "LabelControl8")
        Me.LabelControl8.Name = "LabelControl8"
        '
        'lblChqDueDate
        '
        Me.lblChqDueDate.Appearance.Font = CType(resources.GetObject("lblChqDueDate.Appearance.Font"), System.Drawing.Font)
        Me.lblChqDueDate.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.lblChqDueDate, "lblChqDueDate")
        Me.lblChqDueDate.Name = "lblChqDueDate"
        '
        'dtpChqDueDate
        '
        resources.ApplyResources(Me.dtpChqDueDate, "dtpChqDueDate")
        Me.dtpChqDueDate.Name = "dtpChqDueDate"
        Me.dtpChqDueDate.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("dtpChqDueDate.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me.dtpChqDueDate.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("dtpChqDueDate.Properties.CalendarTimeProperties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        '
        'btnResetChqs
        '
        Me.btnResetChqs.Appearance.Font = CType(resources.GetObject("btnResetChqs.Appearance.Font"), System.Drawing.Font)
        Me.btnResetChqs.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.btnResetChqs, "btnResetChqs")
        Me.btnResetChqs.Name = "btnResetChqs"
        '
        'txtChqOwner
        '
        Me.txtChqOwner.EnterMoveNextControl = True
        resources.ApplyResources(Me.txtChqOwner, "txtChqOwner")
        Me.txtChqOwner.Name = "txtChqOwner"
        Me.txtChqOwner.Properties.Appearance.Font = CType(resources.GetObject("txtChqOwner.Properties.Appearance.Font"), System.Drawing.Font)
        Me.txtChqOwner.Properties.Appearance.Options.UseFont = True
        '
        'chkIsCashed
        '
        resources.ApplyResources(Me.chkIsCashed, "chkIsCashed")
        Me.chkIsCashed.Name = "chkIsCashed"
        Me.chkIsCashed.Properties.Appearance.Font = CType(resources.GetObject("chkIsCashed.Properties.Appearance.Font"), System.Drawing.Font)
        Me.chkIsCashed.Properties.Appearance.Options.UseFont = True
        Me.chkIsCashed.Properties.Caption = resources.GetString("chkIsCashed.Properties.Caption")
        '
        'txtChqBank
        '
        Me.txtChqBank.EnterMoveNextControl = True
        resources.ApplyResources(Me.txtChqBank, "txtChqBank")
        Me.txtChqBank.Name = "txtChqBank"
        Me.txtChqBank.Properties.Appearance.Font = CType(resources.GetObject("txtChqBank.Properties.Appearance.Font"), System.Drawing.Font)
        Me.txtChqBank.Properties.Appearance.Options.UseFont = True
        '
        'lblAccountNumber
        '
        Me.lblAccountNumber.Appearance.Font = CType(resources.GetObject("lblAccountNumber.Appearance.Font"), System.Drawing.Font)
        Me.lblAccountNumber.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.lblAccountNumber, "lblAccountNumber")
        Me.lblAccountNumber.Name = "lblAccountNumber"
        '
        'txtChqNumber
        '
        Me.txtChqNumber.EnterMoveNextControl = True
        resources.ApplyResources(Me.txtChqNumber, "txtChqNumber")
        Me.txtChqNumber.Name = "txtChqNumber"
        Me.txtChqNumber.Properties.Appearance.Font = CType(resources.GetObject("txtChqNumber.Properties.Appearance.Font"), System.Drawing.Font)
        Me.txtChqNumber.Properties.Appearance.Options.UseFont = True
        '
        'lblChqNumber
        '
        Me.lblChqNumber.Appearance.Font = CType(resources.GetObject("lblChqNumber.Appearance.Font"), System.Drawing.Font)
        Me.lblChqNumber.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.lblChqNumber, "lblChqNumber")
        Me.lblChqNumber.Name = "lblChqNumber"
        '
        'txtAccountNumber
        '
        Me.txtAccountNumber.EnterMoveNextControl = True
        resources.ApplyResources(Me.txtAccountNumber, "txtAccountNumber")
        Me.txtAccountNumber.Name = "txtAccountNumber"
        Me.txtAccountNumber.Properties.Appearance.Font = CType(resources.GetObject("txtAccountNumber.Properties.Appearance.Font"), System.Drawing.Font)
        Me.txtAccountNumber.Properties.Appearance.Options.UseFont = True
        '
        'lblChqBank
        '
        Me.lblChqBank.Appearance.Font = CType(resources.GetObject("lblChqBank.Appearance.Font"), System.Drawing.Font)
        Me.lblChqBank.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.lblChqBank, "lblChqBank")
        Me.lblChqBank.Name = "lblChqBank"
        '
        'lblChqOwner
        '
        Me.lblChqOwner.Appearance.Font = CType(resources.GetObject("lblChqOwner.Appearance.Font"), System.Drawing.Font)
        Me.lblChqOwner.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.lblChqOwner, "lblChqOwner")
        Me.lblChqOwner.Name = "lblChqOwner"
        '
        'txtChqValue
        '
        Me.txtChqValue.EnterMoveNextControl = True
        resources.ApplyResources(Me.txtChqValue, "txtChqValue")
        Me.txtChqValue.Name = "txtChqValue"
        Me.txtChqValue.Properties.Appearance.Font = CType(resources.GetObject("txtChqValue.Properties.Appearance.Font"), System.Drawing.Font)
        Me.txtChqValue.Properties.Appearance.Options.UseFont = True
        '
        'lblChqValue
        '
        Me.lblChqValue.Appearance.Font = CType(resources.GetObject("lblChqValue.Appearance.Font"), System.Drawing.Font)
        Me.lblChqValue.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.lblChqValue, "lblChqValue")
        Me.lblChqValue.Name = "lblChqValue"
        '
        'txtNotes
        '
        resources.ApplyResources(Me.txtNotes, "txtNotes")
        Me.txtNotes.Name = "txtNotes"
        '
        'lblNotes
        '
        Me.lblNotes.Appearance.Font = CType(resources.GetObject("lblNotes.Appearance.Font"), System.Drawing.Font)
        Me.lblNotes.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.lblNotes, "lblNotes")
        Me.lblNotes.Name = "lblNotes"
        '
        'frmAddPayment
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.txtNotes)
        Me.Controls.Add(Me.lblNotes)
        Me.Controls.Add(Me.btnResetChqs)
        Me.Controls.Add(Me.txtChqOwner)
        Me.Controls.Add(Me.chkIsCashed)
        Me.Controls.Add(Me.txtChqBank)
        Me.Controls.Add(Me.lblAccountNumber)
        Me.Controls.Add(Me.txtChqNumber)
        Me.Controls.Add(Me.lblChqNumber)
        Me.Controls.Add(Me.txtAccountNumber)
        Me.Controls.Add(Me.lblChqBank)
        Me.Controls.Add(Me.lblChqOwner)
        Me.Controls.Add(Me.txtChqValue)
        Me.Controls.Add(Me.lblChqValue)
        Me.Controls.Add(Me.cbPayType)
        Me.Controls.Add(Me.dtpChqDueDate)
        Me.Controls.Add(Me.dtpPayDate)
        Me.Controls.Add(Me.txtPayValue)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnSave)
        Me.Controls.Add(Me.LabelControl2)
        Me.Controls.Add(Me.LabelControl5)
        Me.Controls.Add(Me.LabelControl7)
        Me.Controls.Add(Me.lblChqDueDate)
        Me.Controls.Add(Me.LabelControl6)
        Me.Controls.Add(Me.LabelControl8)
        Me.Controls.Add(Me.LabelControl4)
        Me.Controls.Add(Me.LabelControl3)
        Me.Controls.Add(Me.LabelControl1)
        Me.Controls.Add(Me.lblPatientName)
        Me.Controls.Add(Me.lblAmountPaid)
        Me.Controls.Add(Me.lblTotalAmount)
        Me.Controls.Add(Me.lblInvoiceNumber)
        Me.Controls.Add(Me.lblBalanceDue)
        Me.Name = "frmAddPayment"
        CType(Me.txtPayValue.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpPayDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpPayDate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cbPayType.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpChqDueDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpChqDueDate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtChqOwner.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkIsCashed.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtChqBank.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtChqNumber.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtAccountNumber.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtChqValue.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtNotes.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents lblBalanceDue As DevExpress.XtraEditors.LabelControl
    Friend WithEvents btnSave As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnCancel As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents txtPayValue As DevExpress.XtraEditors.SpinEdit
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblInvoiceNumber As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl4 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblPatientName As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl5 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblTotalAmount As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl6 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblAmountPaid As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl7 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents dtpPayDate As DevExpress.XtraEditors.DateEdit
    Friend WithEvents LabelControl3 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents cbPayType As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents LabelControl8 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblChqDueDate As DevExpress.XtraEditors.LabelControl
    Friend WithEvents dtpChqDueDate As DevExpress.XtraEditors.DateEdit
    Friend WithEvents btnResetChqs As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents txtChqOwner As DevExpress.XtraEditors.TextEdit
    Friend WithEvents chkIsCashed As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents txtChqBank As DevExpress.XtraEditors.TextEdit
    Friend WithEvents lblAccountNumber As DevExpress.XtraEditors.LabelControl
    Friend WithEvents txtChqNumber As DevExpress.XtraEditors.TextEdit
    Friend WithEvents lblChqNumber As DevExpress.XtraEditors.LabelControl
    Friend WithEvents txtAccountNumber As DevExpress.XtraEditors.TextEdit
    Friend WithEvents lblChqBank As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblChqOwner As DevExpress.XtraEditors.LabelControl
    Friend WithEvents txtChqValue As DevExpress.XtraEditors.TextEdit
    Friend WithEvents lblChqValue As DevExpress.XtraEditors.LabelControl
    Friend WithEvents txtNotes As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents lblNotes As DevExpress.XtraEditors.LabelControl
End Class
