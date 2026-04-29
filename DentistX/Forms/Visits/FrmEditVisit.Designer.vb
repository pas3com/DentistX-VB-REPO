<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmEditVisit
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmEditVisit))
        Me.VisDateTimeEdit = New DevExpress.XtraEditors.DateEdit()
        Me.txtDetail = New DevExpress.XtraEditors.MemoEdit()
        Me.txtNotes = New DevExpress.XtraEditors.MemoEdit()
        Me.btnSave = New DevExpress.XtraEditors.SimpleButton()
        Me.btnCancel = New DevExpress.XtraEditors.SimpleButton()
        Me.btnDelete = New DevExpress.XtraEditors.SimpleButton()
        Me.LayoutControlItem3 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem4 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem5 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem6 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem7 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem8 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.cboPatient = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.cboVisitType = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.VisTimeEdit = New DevExpress.XtraEditors.TimeEdit()
        Me.VisTimeEndEdit = New DevExpress.XtraEditors.TimeEdit()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl3 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl4 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl5 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl6 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl7 = New DevExpress.XtraEditors.LabelControl()
        CType(Me.VisDateTimeEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.VisDateTimeEdit.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDetail.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNotes.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboPatient.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboVisitType.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.VisTimeEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.VisTimeEndEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'VisDateTimeEdit
        '
        resources.ApplyResources(Me.VisDateTimeEdit, "VisDateTimeEdit")
        Me.VisDateTimeEdit.Name = "VisDateTimeEdit"
        Me.VisDateTimeEdit.Properties.Appearance.Font = CType(resources.GetObject("VisDateTimeEdit.Properties.Appearance.Font"), System.Drawing.Font)
        Me.VisDateTimeEdit.Properties.Appearance.Options.UseFont = True
        '
        'txtDetail
        '
        resources.ApplyResources(Me.txtDetail, "txtDetail")
        Me.txtDetail.Name = "txtDetail"
        Me.txtDetail.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.txtDetail.Properties.Appearance.Font = CType(resources.GetObject("txtDetail.Properties.Appearance.Font"), System.Drawing.Font)
        Me.txtDetail.Properties.Appearance.Options.UseBackColor = True
        Me.txtDetail.Properties.Appearance.Options.UseFont = True
        '
        'txtNotes
        '
        resources.ApplyResources(Me.txtNotes, "txtNotes")
        Me.txtNotes.Name = "txtNotes"
        Me.txtNotes.Properties.Appearance.Font = CType(resources.GetObject("txtNotes.Properties.Appearance.Font"), System.Drawing.Font)
        Me.txtNotes.Properties.Appearance.Options.UseFont = True
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
        Me.btnCancel.ImageOptions.ImageKey = resources.GetString("btnCancel.ImageOptions.ImageKey")
        Me.btnCancel.Name = "btnCancel"
        '
        'btnDelete
        '
        resources.ApplyResources(Me.btnDelete, "btnDelete")
        Me.btnDelete.Appearance.Font = CType(resources.GetObject("btnDelete.Appearance.Font"), System.Drawing.Font)
        Me.btnDelete.Appearance.Options.UseFont = True
        Me.btnDelete.ImageOptions.ImageKey = resources.GetString("btnDelete.ImageOptions.ImageKey")
        Me.btnDelete.Name = "btnDelete"
        '
        'LayoutControlItem3
        '
        resources.ApplyResources(Me.LayoutControlItem3, "LayoutControlItem3")
        Me.LayoutControlItem3.Control = Me.VisDateTimeEdit
        Me.LayoutControlItem3.ImageOptions.ImageKey = resources.GetString("LayoutControlItem3.ImageOptions.ImageKey")
        Me.LayoutControlItem3.Location = New System.Drawing.Point(0, 48)
        Me.LayoutControlItem3.Name = "LayoutControlItem3"
        Me.LayoutControlItem3.Size = New System.Drawing.Size(380, 24)
        Me.LayoutControlItem3.TextSize = New System.Drawing.Size(50, 20)
        '
        'LayoutControlItem4
        '
        resources.ApplyResources(Me.LayoutControlItem4, "LayoutControlItem4")
        Me.LayoutControlItem4.Control = Me.txtDetail
        Me.LayoutControlItem4.ImageOptions.ImageKey = resources.GetString("LayoutControlItem4.ImageOptions.ImageKey")
        Me.LayoutControlItem4.Location = New System.Drawing.Point(0, 72)
        Me.LayoutControlItem4.Name = "LayoutControlItem4"
        Me.LayoutControlItem4.Size = New System.Drawing.Size(380, 64)
        Me.LayoutControlItem4.TextSize = New System.Drawing.Size(50, 20)
        '
        'LayoutControlItem5
        '
        resources.ApplyResources(Me.LayoutControlItem5, "LayoutControlItem5")
        Me.LayoutControlItem5.Control = Me.txtNotes
        Me.LayoutControlItem5.ImageOptions.ImageKey = resources.GetString("LayoutControlItem5.ImageOptions.ImageKey")
        Me.LayoutControlItem5.Location = New System.Drawing.Point(0, 136)
        Me.LayoutControlItem5.Name = "LayoutControlItem5"
        Me.LayoutControlItem5.Size = New System.Drawing.Size(380, 64)
        Me.LayoutControlItem5.TextSize = New System.Drawing.Size(50, 20)
        '
        'LayoutControlItem6
        '
        resources.ApplyResources(Me.LayoutControlItem6, "LayoutControlItem6")
        Me.LayoutControlItem6.Control = Me.btnSave
        Me.LayoutControlItem6.ImageOptions.ImageKey = resources.GetString("LayoutControlItem6.ImageOptions.ImageKey")
        Me.LayoutControlItem6.Location = New System.Drawing.Point(0, 200)
        Me.LayoutControlItem6.Name = "LayoutControlItem6"
        Me.LayoutControlItem6.Size = New System.Drawing.Size(126, 26)
        Me.LayoutControlItem6.TextVisible = False
        '
        'LayoutControlItem7
        '
        resources.ApplyResources(Me.LayoutControlItem7, "LayoutControlItem7")
        Me.LayoutControlItem7.Control = Me.btnCancel
        Me.LayoutControlItem7.ImageOptions.ImageKey = resources.GetString("LayoutControlItem7.ImageOptions.ImageKey")
        Me.LayoutControlItem7.Location = New System.Drawing.Point(126, 200)
        Me.LayoutControlItem7.Name = "LayoutControlItem7"
        Me.LayoutControlItem7.Size = New System.Drawing.Size(130, 26)
        Me.LayoutControlItem7.TextVisible = False
        '
        'LayoutControlItem8
        '
        resources.ApplyResources(Me.LayoutControlItem8, "LayoutControlItem8")
        Me.LayoutControlItem8.Control = Me.btnDelete
        Me.LayoutControlItem8.ImageOptions.ImageKey = resources.GetString("LayoutControlItem8.ImageOptions.ImageKey")
        Me.LayoutControlItem8.Location = New System.Drawing.Point(256, 200)
        Me.LayoutControlItem8.Name = "LayoutControlItem8"
        Me.LayoutControlItem8.Size = New System.Drawing.Size(124, 26)
        Me.LayoutControlItem8.TextVisible = False
        '
        'cboPatient
        '
        resources.ApplyResources(Me.cboPatient, "cboPatient")
        Me.cboPatient.Name = "cboPatient"
        Me.cboPatient.Properties.Appearance.Font = CType(resources.GetObject("cboPatient.Properties.Appearance.Font"), System.Drawing.Font)
        Me.cboPatient.Properties.Appearance.Options.UseFont = True
        Me.cboPatient.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("cboPatient.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        '
        'cboVisitType
        '
        resources.ApplyResources(Me.cboVisitType, "cboVisitType")
        Me.cboVisitType.Name = "cboVisitType"
        Me.cboVisitType.Properties.Appearance.Font = CType(resources.GetObject("cboVisitType.Properties.Appearance.Font"), System.Drawing.Font)
        Me.cboVisitType.Properties.Appearance.Options.UseFont = True
        Me.cboVisitType.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("cboVisitType.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        '
        'VisTimeEdit
        '
        resources.ApplyResources(Me.VisTimeEdit, "VisTimeEdit")
        Me.VisTimeEdit.Name = "VisTimeEdit"
        Me.VisTimeEdit.Properties.Appearance.Font = CType(resources.GetObject("VisTimeEdit.Properties.Appearance.Font"), System.Drawing.Font)
        Me.VisTimeEdit.Properties.Appearance.Options.UseFont = True
        Me.VisTimeEdit.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("VisTimeEdit.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        '
        'VisTimeEndEdit
        '
        resources.ApplyResources(Me.VisTimeEndEdit, "VisTimeEndEdit")
        Me.VisTimeEndEdit.Name = "VisTimeEndEdit"
        Me.VisTimeEndEdit.Properties.Appearance.Font = CType(resources.GetObject("VisTimeEndEdit.Properties.Appearance.Font"), System.Drawing.Font)
        Me.VisTimeEndEdit.Properties.Appearance.Options.UseFont = True
        Me.VisTimeEndEdit.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("VisTimeEndEdit.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        '
        'LabelControl1
        '
        resources.ApplyResources(Me.LabelControl1, "LabelControl1")
        Me.LabelControl1.Appearance.Font = CType(resources.GetObject("LabelControl1.Appearance.Font"), System.Drawing.Font)
        Me.LabelControl1.Appearance.Options.UseFont = True
        Me.LabelControl1.Name = "LabelControl1"
        '
        'LabelControl2
        '
        resources.ApplyResources(Me.LabelControl2, "LabelControl2")
        Me.LabelControl2.Appearance.Font = CType(resources.GetObject("LabelControl2.Appearance.Font"), System.Drawing.Font)
        Me.LabelControl2.Appearance.Options.UseFont = True
        Me.LabelControl2.Name = "LabelControl2"
        '
        'LabelControl3
        '
        resources.ApplyResources(Me.LabelControl3, "LabelControl3")
        Me.LabelControl3.Appearance.Font = CType(resources.GetObject("LabelControl3.Appearance.Font"), System.Drawing.Font)
        Me.LabelControl3.Appearance.Options.UseFont = True
        Me.LabelControl3.Name = "LabelControl3"
        '
        'LabelControl4
        '
        resources.ApplyResources(Me.LabelControl4, "LabelControl4")
        Me.LabelControl4.Appearance.Font = CType(resources.GetObject("LabelControl4.Appearance.Font"), System.Drawing.Font)
        Me.LabelControl4.Appearance.Options.UseFont = True
        Me.LabelControl4.Name = "LabelControl4"
        '
        'LabelControl5
        '
        resources.ApplyResources(Me.LabelControl5, "LabelControl5")
        Me.LabelControl5.Appearance.Font = CType(resources.GetObject("LabelControl5.Appearance.Font"), System.Drawing.Font)
        Me.LabelControl5.Appearance.Options.UseFont = True
        Me.LabelControl5.Name = "LabelControl5"
        '
        'LabelControl6
        '
        resources.ApplyResources(Me.LabelControl6, "LabelControl6")
        Me.LabelControl6.Appearance.Font = CType(resources.GetObject("LabelControl6.Appearance.Font"), System.Drawing.Font)
        Me.LabelControl6.Appearance.Options.UseFont = True
        Me.LabelControl6.Name = "LabelControl6"
        '
        'LabelControl7
        '
        resources.ApplyResources(Me.LabelControl7, "LabelControl7")
        Me.LabelControl7.Appearance.Font = CType(resources.GetObject("LabelControl7.Appearance.Font"), System.Drawing.Font)
        Me.LabelControl7.Appearance.Options.UseFont = True
        Me.LabelControl7.Name = "LabelControl7"
        '
        'FrmEditVisit
        '
        resources.ApplyResources(Me, "$this")
        Me.Controls.Add(Me.LabelControl5)
        Me.Controls.Add(Me.LabelControl7)
        Me.Controls.Add(Me.LabelControl6)
        Me.Controls.Add(Me.LabelControl4)
        Me.Controls.Add(Me.LabelControl3)
        Me.Controls.Add(Me.LabelControl2)
        Me.Controls.Add(Me.LabelControl1)
        Me.Controls.Add(Me.VisTimeEndEdit)
        Me.Controls.Add(Me.VisTimeEdit)
        Me.Controls.Add(Me.cboVisitType)
        Me.Controls.Add(Me.cboPatient)
        Me.Controls.Add(Me.VisDateTimeEdit)
        Me.Controls.Add(Me.txtDetail)
        Me.Controls.Add(Me.txtNotes)
        Me.Controls.Add(Me.btnSave)
        Me.Controls.Add(Me.btnDelete)
        Me.Controls.Add(Me.btnCancel)
        Me.Name = "FrmEditVisit"
        CType(Me.VisDateTimeEdit.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.VisDateTimeEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDetail.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtNotes.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboPatient.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboVisitType.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.VisTimeEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.VisTimeEndEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents VisDateTimeEdit As DevExpress.XtraEditors.DateEdit
    Friend WithEvents txtDetail As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents txtNotes As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents btnSave As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnCancel As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnDelete As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LayoutControlItem3 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem4 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem5 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem6 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem7 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem8 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents cboPatient As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents cboVisitType As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents VisTimeEdit As DevExpress.XtraEditors.TimeEdit
    Friend WithEvents VisTimeEndEdit As DevExpress.XtraEditors.TimeEdit
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl3 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl4 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl5 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl6 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl7 As DevExpress.XtraEditors.LabelControl
End Class