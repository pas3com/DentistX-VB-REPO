<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class FrmLabSendWhats
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmLabSendWhats))
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        Me.TxtSendMessage = New DevExpress.XtraEditors.MemoEdit()
        Me.btnWhatsSend = New DevExpress.XtraEditors.SimpleButton()
        Me.btnClose = New DevExpress.XtraEditors.SimpleButton()
        Me.lookUpSource = New DevExpress.XtraEditors.LookUpEdit()
        Me.LabelControl3 = New DevExpress.XtraEditors.LabelControl()
        Me.txtClinicName = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl4 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl5 = New DevExpress.XtraEditors.LabelControl()
        Me.lookUpSubject = New DevExpress.XtraEditors.LookUpEdit()
        Me.LabelControl6 = New DevExpress.XtraEditors.LabelControl()
        Me.dateReceive = New DevExpress.XtraEditors.DateEdit()
        Me.LabelControl7 = New DevExpress.XtraEditors.LabelControl()
        Me.checkedDetails = New DevExpress.XtraEditors.CheckedListBoxControl()
        Me.LabelControl8 = New DevExpress.XtraEditors.LabelControl()
        Me.txtNote = New DevExpress.XtraEditors.MemoEdit()
        Me.btnBuildMessage = New DevExpress.XtraEditors.SimpleButton()
        Me.btnAddSubject = New DevExpress.XtraEditors.SimpleButton()
        Me.btnAddSubDetails = New DevExpress.XtraEditors.SimpleButton()
        Me.RadioLang = New DevExpress.XtraEditors.RadioGroup()
        Me.LabelControl14 = New DevExpress.XtraEditors.LabelControl()
        Me.patientCombo = New DentistX.PatientCombo()
        Me.btnShowHistory = New DevExpress.XtraEditors.SimpleButton()
        CType(Me.TxtSendMessage.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lookUpSource.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtClinicName.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lookUpSubject.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dateReceive.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dateReceive.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.checkedDetails, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNote.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadioLang.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
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
        'TxtSendMessage
        '
        resources.ApplyResources(Me.TxtSendMessage, "TxtSendMessage")
        Me.TxtSendMessage.Name = "TxtSendMessage"
        Me.TxtSendMessage.Properties.Appearance.Font = CType(resources.GetObject("TxtSendMessage.Properties.Appearance.Font"), System.Drawing.Font)
        Me.TxtSendMessage.Properties.Appearance.Options.UseFont = True
        '
        'btnWhatsSend
        '
        resources.ApplyResources(Me.btnWhatsSend, "btnWhatsSend")
        Me.btnWhatsSend.ImageOptions.ImageKey = resources.GetString("btnWhatsSend.ImageOptions.ImageKey")
        Me.btnWhatsSend.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleCenter
        Me.btnWhatsSend.ImageOptions.SvgImage = Global.DentistX.My.Resources.Resources.whatsapp
        Me.btnWhatsSend.ImageOptions.SvgImageSize = New System.Drawing.Size(20, 20)
        Me.btnWhatsSend.Name = "btnWhatsSend"
        '
        'btnClose
        '
        resources.ApplyResources(Me.btnClose, "btnClose")
        Me.btnClose.Appearance.Font = CType(resources.GetObject("btnClose.Appearance.Font"), System.Drawing.Font)
        Me.btnClose.Appearance.Options.UseFont = True
        Me.btnClose.Name = "btnClose"
        '
        'lookUpSource
        '
        resources.ApplyResources(Me.lookUpSource, "lookUpSource")
        Me.lookUpSource.Name = "lookUpSource"
        Me.lookUpSource.Properties.Appearance.Font = CType(resources.GetObject("lookUpSource.Properties.Appearance.Font"), System.Drawing.Font)
        Me.lookUpSource.Properties.Appearance.Options.UseFont = True
        Me.lookUpSource.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("lookUpSource.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me.lookUpSource.Properties.NullText = resources.GetString("lookUpSource.Properties.NullText")
        '
        'LabelControl3
        '
        Me.LabelControl3.Appearance.Font = CType(resources.GetObject("LabelControl3.Appearance.Font"), System.Drawing.Font)
        Me.LabelControl3.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.LabelControl3, "LabelControl3")
        Me.LabelControl3.Name = "LabelControl3"
        '
        'txtClinicName
        '
        resources.ApplyResources(Me.txtClinicName, "txtClinicName")
        Me.txtClinicName.Name = "txtClinicName"
        Me.txtClinicName.Properties.Appearance.Font = CType(resources.GetObject("txtClinicName.Properties.Appearance.Font"), System.Drawing.Font)
        Me.txtClinicName.Properties.Appearance.Options.UseFont = True
        '
        'LabelControl4
        '
        Me.LabelControl4.Appearance.Font = CType(resources.GetObject("LabelControl4.Appearance.Font"), System.Drawing.Font)
        Me.LabelControl4.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.LabelControl4, "LabelControl4")
        Me.LabelControl4.Name = "LabelControl4"
        '
        'LabelControl5
        '
        Me.LabelControl5.Appearance.Font = CType(resources.GetObject("LabelControl5.Appearance.Font"), System.Drawing.Font)
        Me.LabelControl5.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.LabelControl5, "LabelControl5")
        Me.LabelControl5.Name = "LabelControl5"
        '
        'lookUpSubject
        '
        resources.ApplyResources(Me.lookUpSubject, "lookUpSubject")
        Me.lookUpSubject.Name = "lookUpSubject"
        Me.lookUpSubject.Properties.Appearance.Font = CType(resources.GetObject("lookUpSubject.Properties.Appearance.Font"), System.Drawing.Font)
        Me.lookUpSubject.Properties.Appearance.Options.UseFont = True
        Me.lookUpSubject.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("lookUpSubject.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me.lookUpSubject.Properties.NullText = resources.GetString("lookUpSubject.Properties.NullText")
        '
        'LabelControl6
        '
        Me.LabelControl6.Appearance.Font = CType(resources.GetObject("LabelControl6.Appearance.Font"), System.Drawing.Font)
        Me.LabelControl6.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.LabelControl6, "LabelControl6")
        Me.LabelControl6.Name = "LabelControl6"
        '
        'dateReceive
        '
        resources.ApplyResources(Me.dateReceive, "dateReceive")
        Me.dateReceive.Name = "dateReceive"
        Me.dateReceive.Properties.Appearance.Font = CType(resources.GetObject("dateReceive.Properties.Appearance.Font"), System.Drawing.Font)
        Me.dateReceive.Properties.Appearance.Options.UseFont = True
        Me.dateReceive.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("dateReceive.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me.dateReceive.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("dateReceive.Properties.CalendarTimeProperties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        '
        'LabelControl7
        '
        Me.LabelControl7.Appearance.Font = CType(resources.GetObject("LabelControl7.Appearance.Font"), System.Drawing.Font)
        Me.LabelControl7.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.LabelControl7, "LabelControl7")
        Me.LabelControl7.Name = "LabelControl7"
        '
        'checkedDetails
        '
        Me.checkedDetails.CheckOnClick = True
        resources.ApplyResources(Me.checkedDetails, "checkedDetails")
        Me.checkedDetails.Name = "checkedDetails"
        '
        'LabelControl8
        '
        Me.LabelControl8.Appearance.Font = CType(resources.GetObject("LabelControl8.Appearance.Font"), System.Drawing.Font)
        Me.LabelControl8.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.LabelControl8, "LabelControl8")
        Me.LabelControl8.Name = "LabelControl8"
        '
        'txtNote
        '
        resources.ApplyResources(Me.txtNote, "txtNote")
        Me.txtNote.Name = "txtNote"
        Me.txtNote.Properties.Appearance.Font = CType(resources.GetObject("txtNote.Properties.Appearance.Font"), System.Drawing.Font)
        Me.txtNote.Properties.Appearance.Options.UseFont = True
        '
        'btnBuildMessage
        '
        Me.btnBuildMessage.Appearance.Font = CType(resources.GetObject("btnBuildMessage.Appearance.Font"), System.Drawing.Font)
        Me.btnBuildMessage.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.btnBuildMessage, "btnBuildMessage")
        Me.btnBuildMessage.Name = "btnBuildMessage"
        '
        'btnAddSubject
        '
        Me.btnAddSubject.ImageOptions.Image = Global.DentistX.My.Resources.Resources.add_16x16
        Me.btnAddSubject.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleCenter
        resources.ApplyResources(Me.btnAddSubject, "btnAddSubject")
        Me.btnAddSubject.Name = "btnAddSubject"
        '
        'btnAddSubDetails
        '
        Me.btnAddSubDetails.ImageOptions.Image = Global.DentistX.My.Resources.Resources.add_16x16
        Me.btnAddSubDetails.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleCenter
        resources.ApplyResources(Me.btnAddSubDetails, "btnAddSubDetails")
        Me.btnAddSubDetails.Name = "btnAddSubDetails"
        '
        'RadioLang
        '
        Me.RadioLang.EnterMoveNextControl = True
        resources.ApplyResources(Me.RadioLang, "RadioLang")
        Me.RadioLang.Name = "RadioLang"
        Me.RadioLang.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.RadioLang.Properties.Appearance.Font = CType(resources.GetObject("RadioLang.Properties.Appearance.Font"), System.Drawing.Font)
        Me.RadioLang.Properties.Appearance.Options.UseBackColor = True
        Me.RadioLang.Properties.Appearance.Options.UseFont = True
        Me.RadioLang.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.RadioLang.Properties.Items.AddRange(New DevExpress.XtraEditors.Controls.RadioGroupItem() {New DevExpress.XtraEditors.Controls.RadioGroupItem(CType(resources.GetObject("RadioLang.Properties.Items"), Object), resources.GetString("RadioLang.Properties.Items1"), CType(resources.GetObject("RadioLang.Properties.Items2"), Boolean), CType(resources.GetObject("RadioLang.Properties.Items3"), Object)), New DevExpress.XtraEditors.Controls.RadioGroupItem(CType(resources.GetObject("RadioLang.Properties.Items4"), Object), resources.GetString("RadioLang.Properties.Items5"))})
        '
        'LabelControl14
        '
        Me.LabelControl14.Appearance.Font = CType(resources.GetObject("LabelControl14.Appearance.Font"), System.Drawing.Font)
        Me.LabelControl14.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.LabelControl14, "LabelControl14")
        Me.LabelControl14.Name = "LabelControl14"
        '
        'patientCombo
        '
        resources.ApplyResources(Me.patientCombo, "patientCombo")
        Me.patientCombo.Appearance.Font = CType(resources.GetObject("patientCombo.Appearance.Font"), System.Drawing.Font)
        Me.patientCombo.Appearance.Options.UseFont = True
        Me.patientCombo.BtnAddVisible = True
        Me.patientCombo.BtnSearchVisible = True
        Me.patientCombo.Filter = ""
        Me.patientCombo.Name = "patientCombo"
        '
        'btnShowHistory
        '
        Me.btnShowHistory.Appearance.Font = CType(resources.GetObject("SimpleButton1.Appearance.Font"), System.Drawing.Font)
        Me.btnShowHistory.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.btnShowHistory, "btnShowHistory")
        Me.btnShowHistory.Name = "btnShowHistory"
        '
        'FrmLabSendWhats
        '
        Me.Appearance.Options.UseFont = True
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.btnShowHistory)
        Me.Controls.Add(Me.RadioLang)
        Me.Controls.Add(Me.LabelControl14)
        Me.Controls.Add(Me.btnAddSubDetails)
        Me.Controls.Add(Me.btnAddSubject)
        Me.Controls.Add(Me.btnBuildMessage)
        Me.Controls.Add(Me.txtNote)
        Me.Controls.Add(Me.LabelControl8)
        Me.Controls.Add(Me.checkedDetails)
        Me.Controls.Add(Me.LabelControl7)
        Me.Controls.Add(Me.dateReceive)
        Me.Controls.Add(Me.LabelControl6)
        Me.Controls.Add(Me.lookUpSubject)
        Me.Controls.Add(Me.LabelControl5)
        Me.Controls.Add(Me.patientCombo)
        Me.Controls.Add(Me.LabelControl4)
        Me.Controls.Add(Me.txtClinicName)
        Me.Controls.Add(Me.LabelControl3)
        Me.Controls.Add(Me.lookUpSource)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.btnWhatsSend)
        Me.Controls.Add(Me.TxtSendMessage)
        Me.Controls.Add(Me.LabelControl2)
        Me.Controls.Add(Me.LabelControl1)
        Me.Name = "FrmLabSendWhats"
        CType(Me.TxtSendMessage.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lookUpSource.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtClinicName.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lookUpSubject.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dateReceive.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dateReceive.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.checkedDetails, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtNote.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadioLang.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents TxtSendMessage As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents btnWhatsSend As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnClose As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents lookUpSource As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents LabelControl3 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents txtClinicName As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl4 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents patientCombo As DentistX.PatientCombo
    Friend WithEvents LabelControl5 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lookUpSubject As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents LabelControl6 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents dateReceive As DevExpress.XtraEditors.DateEdit
    Friend WithEvents LabelControl7 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents checkedDetails As DevExpress.XtraEditors.CheckedListBoxControl
    Friend WithEvents LabelControl8 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents txtNote As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents btnBuildMessage As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnAddSubject As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnAddSubDetails As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents RadioLang As DevExpress.XtraEditors.RadioGroup
    Friend WithEvents LabelControl14 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents btnShowHistory As DevExpress.XtraEditors.SimpleButton
End Class
