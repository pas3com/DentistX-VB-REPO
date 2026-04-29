<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class RequestForm
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(RequestForm))
        Me.txtCurrentLicense = New DevExpress.XtraEditors.MemoEdit()
        Me.cmbPlan = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.cmbPeriod = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.btnCreateRequest = New DevExpress.XtraEditors.SimpleButton()
        Me.btnCancel = New DevExpress.XtraEditors.SimpleButton()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl3 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl4 = New DevExpress.XtraEditors.LabelControl()
        Me.txtDoctorName = New DevExpress.XtraEditors.TextEdit()
        Me.txtClinicName = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl5 = New DevExpress.XtraEditors.LabelControl()
        Me.txtEmail = New DevExpress.XtraEditors.TextEdit()
        CType(Me.txtCurrentLicense.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbPlan.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbPeriod.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDoctorName.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtClinicName.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtEmail.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'txtCurrentLicense
        '
        resources.ApplyResources(Me.txtCurrentLicense, "txtCurrentLicense")
        Me.txtCurrentLicense.Name = "txtCurrentLicense"
        Me.txtCurrentLicense.Properties.Appearance.Font = CType(resources.GetObject("txtCurrentLicense.Properties.Appearance.Font"), System.Drawing.Font)
        Me.txtCurrentLicense.Properties.Appearance.Options.UseFont = True
        '
        'cmbPlan
        '
        resources.ApplyResources(Me.cmbPlan, "cmbPlan")
        Me.cmbPlan.Name = "cmbPlan"
        Me.cmbPlan.Properties.Appearance.Font = CType(resources.GetObject("cmbPlan.Properties.Appearance.Font"), System.Drawing.Font)
        Me.cmbPlan.Properties.Appearance.Options.UseFont = True
        Me.cmbPlan.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("cmbPlan.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me.cmbPlan.Properties.Items.AddRange(New Object() {resources.GetString("cmbPlan.Properties.Items"), resources.GetString("cmbPlan.Properties.Items1")})
        '
        'cmbPeriod
        '
        resources.ApplyResources(Me.cmbPeriod, "cmbPeriod")
        Me.cmbPeriod.Name = "cmbPeriod"
        Me.cmbPeriod.Properties.Appearance.Font = CType(resources.GetObject("cmbPeriod.Properties.Appearance.Font"), System.Drawing.Font)
        Me.cmbPeriod.Properties.Appearance.Options.UseFont = True
        Me.cmbPeriod.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("cmbPeriod.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me.cmbPeriod.Properties.Items.AddRange(New Object() {resources.GetString("cmbPeriod.Properties.Items"), resources.GetString("cmbPeriod.Properties.Items1"), resources.GetString("cmbPeriod.Properties.Items2"), resources.GetString("cmbPeriod.Properties.Items3")})
        '
        'btnCreateRequest
        '
        resources.ApplyResources(Me.btnCreateRequest, "btnCreateRequest")
        Me.btnCreateRequest.Appearance.Font = CType(resources.GetObject("btnCreateRequest.Appearance.Font"), System.Drawing.Font)
        Me.btnCreateRequest.Appearance.Options.UseFont = True
        Me.btnCreateRequest.ImageOptions.ImageKey = resources.GetString("btnCreateRequest.ImageOptions.ImageKey")
        Me.btnCreateRequest.Name = "btnCreateRequest"
        '
        'btnCancel
        '
        resources.ApplyResources(Me.btnCancel, "btnCancel")
        Me.btnCancel.Appearance.Font = CType(resources.GetObject("btnCancel.Appearance.Font"), System.Drawing.Font)
        Me.btnCancel.Appearance.Options.UseFont = True
        Me.btnCancel.ImageOptions.ImageKey = resources.GetString("btnCancel.ImageOptions.ImageKey")
        Me.btnCancel.Name = "btnCancel"
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
        'txtDoctorName
        '
        resources.ApplyResources(Me.txtDoctorName, "txtDoctorName")
        Me.txtDoctorName.Name = "txtDoctorName"
        Me.txtDoctorName.Properties.Appearance.Font = CType(resources.GetObject("txtDoctorName.Properties.Appearance.Font"), System.Drawing.Font)
        Me.txtDoctorName.Properties.Appearance.Options.UseFont = True
        '
        'txtClinicName
        '
        resources.ApplyResources(Me.txtClinicName, "txtClinicName")
        Me.txtClinicName.Name = "txtClinicName"
        Me.txtClinicName.Properties.Appearance.Font = CType(resources.GetObject("txtClinicName.Properties.Appearance.Font"), System.Drawing.Font)
        Me.txtClinicName.Properties.Appearance.Options.UseFont = True
        '
        'LabelControl5
        '
        resources.ApplyResources(Me.LabelControl5, "LabelControl5")
        Me.LabelControl5.Appearance.Font = CType(resources.GetObject("LabelControl5.Appearance.Font"), System.Drawing.Font)
        Me.LabelControl5.Appearance.Options.UseFont = True
        Me.LabelControl5.Name = "LabelControl5"
        '
        'txtEmail
        '
        resources.ApplyResources(Me.txtEmail, "txtEmail")
        Me.txtEmail.Name = "txtEmail"
        Me.txtEmail.Properties.Appearance.Font = CType(resources.GetObject("txtEmail.Properties.Appearance.Font"), System.Drawing.Font)
        Me.txtEmail.Properties.Appearance.Options.UseFont = True
        '
        'RequestForm
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.txtEmail)
        Me.Controls.Add(Me.LabelControl5)
        Me.Controls.Add(Me.txtClinicName)
        Me.Controls.Add(Me.txtDoctorName)
        Me.Controls.Add(Me.LabelControl4)
        Me.Controls.Add(Me.LabelControl3)
        Me.Controls.Add(Me.LabelControl2)
        Me.Controls.Add(Me.LabelControl1)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnCreateRequest)
        Me.Controls.Add(Me.cmbPeriod)
        Me.Controls.Add(Me.cmbPlan)
        Me.Controls.Add(Me.txtCurrentLicense)
        Me.Name = "RequestForm"
        CType(Me.txtCurrentLicense.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbPlan.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbPeriod.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDoctorName.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtClinicName.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtEmail.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents txtCurrentLicense As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents cmbPlan As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents cmbPeriod As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents btnCreateRequest As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnCancel As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl3 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl4 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents txtDoctorName As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtClinicName As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl5 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents txtEmail As DevExpress.XtraEditors.TextEdit
End Class
