<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ImpSelectFRM
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
        Me.cmbLength = New DentistX.ImplantLengthCombo()
        Me.cmbDiameter = New DentistX.ImplantDiameterCombo()
        Me.cmbType = New DentistX.ImplantTypeCombo()
        Me.cmbBrand = New DentistX.ImplantBrandCombo()
        Me.btnCancelImp = New DevExpress.XtraEditors.SimpleButton()
        Me.btnSelect = New DevExpress.XtraEditors.SimpleButton()
        Me.LabelControl18 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl17 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl16 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl15 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl11 = New DevExpress.XtraEditors.LabelControl()
        Me.cmbDesign = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.ResultLbl = New DevExpress.XtraEditors.LabelControl()
        Me.btnCancel = New DevExpress.XtraEditors.SimpleButton()
        Me.btnSaveTrt = New DevExpress.XtraEditors.SimpleButton()
        Me.GroupControl1 = New DevExpress.XtraEditors.GroupControl()
        CType(Me.cmbDesign.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl1.SuspendLayout()
        Me.SuspendLayout()
        '
        'cmbLength
        '
        Me.cmbLength.LengthID = 0
        Me.cmbLength.LengthMM = New Decimal(New Integer() {0, 0, 0, 0})
        Me.cmbLength.Location = New System.Drawing.Point(78, 81)
        Me.cmbLength.Name = "cmbLength"
        Me.cmbLength.Size = New System.Drawing.Size(121, 22)
        Me.cmbLength.TabIndex = 30
        '
        'cmbDiameter
        '
        Me.cmbDiameter.DiameterID = 0
        Me.cmbDiameter.DiameterMM = New Decimal(New Integer() {0, 0, 0, 0})
        Me.cmbDiameter.Location = New System.Drawing.Point(78, 109)
        Me.cmbDiameter.Name = "cmbDiameter"
        Me.cmbDiameter.Size = New System.Drawing.Size(121, 22)
        Me.cmbDiameter.TabIndex = 29
        '
        'cmbType
        '
        Me.cmbType.Location = New System.Drawing.Point(78, 53)
        Me.cmbType.Name = "cmbType"
        Me.cmbType.Size = New System.Drawing.Size(121, 22)
        Me.cmbType.TabIndex = 28
        Me.cmbType.TypeID = 0
        Me.cmbType.TypeName = Nothing
        '
        'cmbBrand
        '
        Me.cmbBrand.BrandID = 0
        Me.cmbBrand.BrandName = Nothing
        Me.cmbBrand.Location = New System.Drawing.Point(79, 25)
        Me.cmbBrand.Name = "cmbBrand"
        Me.cmbBrand.Size = New System.Drawing.Size(121, 22)
        Me.cmbBrand.TabIndex = 27
        '
        'btnCancelImp
        '
        Me.btnCancelImp.Appearance.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Bold)
        Me.btnCancelImp.Appearance.Options.UseFont = True
        Me.btnCancelImp.Location = New System.Drawing.Point(236, 114)
        Me.btnCancelImp.Name = "btnCancelImp"
        Me.btnCancelImp.Size = New System.Drawing.Size(75, 23)
        Me.btnCancelImp.TabIndex = 26
        Me.btnCancelImp.Text = "Cancel"
        '
        'btnSelect
        '
        Me.btnSelect.Appearance.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Bold)
        Me.btnSelect.Appearance.Options.UseFont = True
        Me.btnSelect.Location = New System.Drawing.Point(236, 77)
        Me.btnSelect.Name = "btnSelect"
        Me.btnSelect.Size = New System.Drawing.Size(75, 23)
        Me.btnSelect.TabIndex = 25
        Me.btnSelect.Text = "Select"
        '
        'LabelControl18
        '
        Me.LabelControl18.Appearance.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Bold)
        Me.LabelControl18.Appearance.Options.UseFont = True
        Me.LabelControl18.Location = New System.Drawing.Point(30, 88)
        Me.LabelControl18.Name = "LabelControl18"
        Me.LabelControl18.Size = New System.Drawing.Size(38, 15)
        Me.LabelControl18.TabIndex = 21
        Me.LabelControl18.Text = "Length"
        '
        'LabelControl17
        '
        Me.LabelControl17.Appearance.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Bold)
        Me.LabelControl17.Appearance.Options.UseFont = True
        Me.LabelControl17.Location = New System.Drawing.Point(20, 114)
        Me.LabelControl17.Name = "LabelControl17"
        Me.LabelControl17.Size = New System.Drawing.Size(52, 15)
        Me.LabelControl17.TabIndex = 20
        Me.LabelControl17.Text = "Diameter"
        '
        'LabelControl16
        '
        Me.LabelControl16.Appearance.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Bold)
        Me.LabelControl16.Appearance.Options.UseFont = True
        Me.LabelControl16.Location = New System.Drawing.Point(29, 140)
        Me.LabelControl16.Name = "LabelControl16"
        Me.LabelControl16.Size = New System.Drawing.Size(36, 15)
        Me.LabelControl16.TabIndex = 22
        Me.LabelControl16.Text = "Design"
        '
        'LabelControl15
        '
        Me.LabelControl15.Appearance.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Bold)
        Me.LabelControl15.Appearance.Options.UseFont = True
        Me.LabelControl15.Location = New System.Drawing.Point(43, 60)
        Me.LabelControl15.Name = "LabelControl15"
        Me.LabelControl15.Size = New System.Drawing.Size(26, 15)
        Me.LabelControl15.TabIndex = 19
        Me.LabelControl15.Text = "Type"
        '
        'LabelControl11
        '
        Me.LabelControl11.Appearance.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Bold)
        Me.LabelControl11.Appearance.Options.UseFont = True
        Me.LabelControl11.Location = New System.Drawing.Point(38, 30)
        Me.LabelControl11.Name = "LabelControl11"
        Me.LabelControl11.Size = New System.Drawing.Size(32, 15)
        Me.LabelControl11.TabIndex = 18
        Me.LabelControl11.Text = "Brand"
        '
        'cmbDesign
        '
        Me.cmbDesign.Location = New System.Drawing.Point(79, 137)
        Me.cmbDesign.Name = "cmbDesign"
        Me.cmbDesign.Properties.Appearance.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Bold)
        Me.cmbDesign.Properties.Appearance.Options.UseFont = True
        Me.cmbDesign.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbDesign.Properties.Items.AddRange(New Object() {"Slim", "Standard"})
        Me.cmbDesign.Size = New System.Drawing.Size(103, 22)
        Me.cmbDesign.TabIndex = 23
        '
        'ResultLbl
        '
        Me.ResultLbl.Appearance.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Bold)
        Me.ResultLbl.Appearance.Options.UseFont = True
        Me.ResultLbl.Appearance.Options.UseTextOptions = True
        Me.ResultLbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
        Me.ResultLbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.ResultLbl.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.ResultLbl.Location = New System.Drawing.Point(2, 182)
        Me.ResultLbl.Name = "ResultLbl"
        Me.ResultLbl.Size = New System.Drawing.Size(360, 90)
        Me.ResultLbl.TabIndex = 24
        Me.ResultLbl.Text = "Design"
        '
        'btnCancel
        '
        Me.btnCancel.Appearance.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Bold)
        Me.btnCancel.Appearance.ForeColor = System.Drawing.Color.Red
        Me.btnCancel.Appearance.Options.UseFont = True
        Me.btnCancel.Appearance.Options.UseForeColor = True
        Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCancel.Location = New System.Drawing.Point(211, 307)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(75, 23)
        Me.btnCancel.TabIndex = 34
        Me.btnCancel.Text = "Cancel"
        '
        'btnSaveTrt
        '
        Me.btnSaveTrt.Appearance.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Bold)
        Me.btnSaveTrt.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.btnSaveTrt.Appearance.Options.UseFont = True
        Me.btnSaveTrt.Appearance.Options.UseForeColor = True
        Me.btnSaveTrt.Location = New System.Drawing.Point(109, 307)
        Me.btnSaveTrt.Name = "btnSaveTrt"
        Me.btnSaveTrt.Size = New System.Drawing.Size(75, 23)
        Me.btnSaveTrt.TabIndex = 33
        Me.btnSaveTrt.Text = "Save"
        '
        'GroupControl1
        '
        Me.GroupControl1.AppearanceCaption.Font = New System.Drawing.Font("Calibri", 10.0!, System.Drawing.FontStyle.Bold)
        Me.GroupControl1.AppearanceCaption.Options.UseFont = True
        Me.GroupControl1.Controls.Add(Me.cmbLength)
        Me.GroupControl1.Controls.Add(Me.cmbDiameter)
        Me.GroupControl1.Controls.Add(Me.cmbType)
        Me.GroupControl1.Controls.Add(Me.cmbBrand)
        Me.GroupControl1.Controls.Add(Me.btnCancelImp)
        Me.GroupControl1.Controls.Add(Me.btnSelect)
        Me.GroupControl1.Controls.Add(Me.LabelControl18)
        Me.GroupControl1.Controls.Add(Me.LabelControl17)
        Me.GroupControl1.Controls.Add(Me.LabelControl16)
        Me.GroupControl1.Controls.Add(Me.LabelControl15)
        Me.GroupControl1.Controls.Add(Me.LabelControl11)
        Me.GroupControl1.Controls.Add(Me.cmbDesign)
        Me.GroupControl1.Controls.Add(Me.ResultLbl)
        Me.GroupControl1.Location = New System.Drawing.Point(5, 9)
        Me.GroupControl1.Name = "GroupControl1"
        Me.GroupControl1.Size = New System.Drawing.Size(364, 274)
        Me.GroupControl1.TabIndex = 35
        Me.GroupControl1.Text = "Select Implant Specifications"
        '
        'ImpSelectFRM
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(393, 342)
        Me.Controls.Add(Me.GroupControl1)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnSaveTrt)
        Me.Name = "ImpSelectFRM"
        Me.Text = "ImpSelectFRM"
        CType(Me.cmbDesign.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl1.ResumeLayout(False)
        Me.GroupControl1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents cmbLength As ImplantLengthCombo
    Friend WithEvents cmbDiameter As ImplantDiameterCombo
    Friend WithEvents cmbType As ImplantTypeCombo
    Friend WithEvents cmbBrand As ImplantBrandCombo
    Friend WithEvents btnCancelImp As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnSelect As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LabelControl18 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl17 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl16 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl15 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl11 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents cmbDesign As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents ResultLbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents btnCancel As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnSaveTrt As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents GroupControl1 As DevExpress.XtraEditors.GroupControl
End Class
