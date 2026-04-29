<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FormSettings
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormSettings))
        Me.RadioArab = New System.Windows.Forms.RadioButton()
        Me.RadioEng = New System.Windows.Forms.RadioButton()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.GrpLang = New Infragistics.Win.Misc.UltraGroupBox()
        Me.GrpDbase = New Infragistics.Win.Misc.UltraGroupBox()
        Me.txtSrvrPass = New System.Windows.Forms.TextBox()
        Me.txtHostName = New System.Windows.Forms.TextBox()
        Me.RadioOther = New System.Windows.Forms.RadioButton()
        Me.RadioLocal2012 = New System.Windows.Forms.RadioButton()
        Me.RadioLocal = New System.Windows.Forms.RadioButton()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.RadioSql = New System.Windows.Forms.RadioButton()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.BtnDefault = New System.Windows.Forms.Button()
        Me.BtnSaveExit = New System.Windows.Forms.Button()
        Me.BtnExit = New System.Windows.Forms.Button()
        Me.UltraGroupBox1 = New Infragistics.Win.Misc.UltraGroupBox()
        Me.LabelSize = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.LabelDb = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.LabelStart = New System.Windows.Forms.Label()
        Me.Labelang = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.UltraGroupBox2 = New Infragistics.Win.Misc.UltraGroupBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.RadioNormal = New System.Windows.Forms.RadioButton()
        Me.RadioLarge = New System.Windows.Forms.RadioButton()
        Me.UltraGroupBox3 = New Infragistics.Win.Misc.UltraGroupBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.RadioStar3 = New System.Windows.Forms.RadioButton()
        Me.RadioStar1 = New System.Windows.Forms.RadioButton()
        Me.btnCreateSVG = New System.Windows.Forms.Button()
        Me.btnCreateSVGKids = New System.Windows.Forms.Button()
        Me.btnLoadSvgs = New DevExpress.XtraEditors.SimpleButton()
        Me.btnCreatApp = New DevExpress.XtraEditors.SimpleButton()
        Me.btnExtractSVG = New DevExpress.XtraEditors.SimpleButton()
        Me.UltraGroupBox4 = New Infragistics.Win.Misc.UltraGroupBox()
        Me.ShortRemind = New DevExpress.XtraEditors.SpinEdit()
        Me.Label10 = New System.Windows.Forms.Label()
        CType(Me.GrpLang, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GrpLang.SuspendLayout()
        CType(Me.GrpDbase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GrpDbase.SuspendLayout()
        CType(Me.UltraGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.UltraGroupBox1.SuspendLayout()
        CType(Me.UltraGroupBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.UltraGroupBox2.SuspendLayout()
        CType(Me.UltraGroupBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.UltraGroupBox3.SuspendLayout()
        CType(Me.UltraGroupBox4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.UltraGroupBox4.SuspendLayout()
        CType(Me.ShortRemind.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadioArab
        '
        resources.ApplyResources(Me.RadioArab, "RadioArab")
        Me.RadioArab.ForeColor = System.Drawing.Color.White
        Me.RadioArab.Name = "RadioArab"
        Me.RadioArab.UseVisualStyleBackColor = True
        '
        'RadioEng
        '
        resources.ApplyResources(Me.RadioEng, "RadioEng")
        Me.RadioEng.Checked = True
        Me.RadioEng.ForeColor = System.Drawing.Color.White
        Me.RadioEng.Name = "RadioEng"
        Me.RadioEng.TabStop = True
        Me.RadioEng.UseVisualStyleBackColor = True
        '
        'Label1
        '
        resources.ApplyResources(Me.Label1, "Label1")
        Me.Label1.ForeColor = System.Drawing.Color.White
        Me.Label1.Name = "Label1"
        '
        'GrpLang
        '
        Me.GrpLang.CaptionAlignment = Infragistics.Win.Misc.GroupBoxCaptionAlignment.Center
        Me.GrpLang.Controls.Add(Me.Label1)
        Me.GrpLang.Controls.Add(Me.RadioArab)
        Me.GrpLang.Controls.Add(Me.RadioEng)
        resources.ApplyResources(Me.GrpLang, "GrpLang")
        Me.GrpLang.Name = "GrpLang"
        Me.GrpLang.ViewStyle = Infragistics.Win.Misc.GroupBoxViewStyle.Office2000
        '
        'GrpDbase
        '
        Me.GrpDbase.CaptionAlignment = Infragistics.Win.Misc.GroupBoxCaptionAlignment.Center
        Me.GrpDbase.Controls.Add(Me.txtSrvrPass)
        Me.GrpDbase.Controls.Add(Me.txtHostName)
        Me.GrpDbase.Controls.Add(Me.RadioOther)
        Me.GrpDbase.Controls.Add(Me.RadioLocal2012)
        Me.GrpDbase.Controls.Add(Me.RadioLocal)
        Me.GrpDbase.Controls.Add(Me.Label9)
        Me.GrpDbase.Controls.Add(Me.RadioSql)
        Me.GrpDbase.Controls.Add(Me.Label2)
        resources.ApplyResources(Me.GrpDbase, "GrpDbase")
        Me.GrpDbase.Name = "GrpDbase"
        Me.GrpDbase.ViewStyle = Infragistics.Win.Misc.GroupBoxViewStyle.Office2000
        '
        'txtSrvrPass
        '
        resources.ApplyResources(Me.txtSrvrPass, "txtSrvrPass")
        Me.txtSrvrPass.Name = "txtSrvrPass"
        '
        'txtHostName
        '
        resources.ApplyResources(Me.txtHostName, "txtHostName")
        Me.txtHostName.Name = "txtHostName"
        '
        'RadioOther
        '
        resources.ApplyResources(Me.RadioOther, "RadioOther")
        Me.RadioOther.ForeColor = System.Drawing.Color.Red
        Me.RadioOther.Name = "RadioOther"
        Me.RadioOther.TabStop = True
        Me.RadioOther.UseVisualStyleBackColor = True
        '
        'RadioLocal2012
        '
        resources.ApplyResources(Me.RadioLocal2012, "RadioLocal2012")
        Me.RadioLocal2012.ForeColor = System.Drawing.Color.Red
        Me.RadioLocal2012.Name = "RadioLocal2012"
        Me.RadioLocal2012.TabStop = True
        Me.RadioLocal2012.UseVisualStyleBackColor = True
        '
        'RadioLocal
        '
        resources.ApplyResources(Me.RadioLocal, "RadioLocal")
        Me.RadioLocal.ForeColor = System.Drawing.Color.Red
        Me.RadioLocal.Name = "RadioLocal"
        Me.RadioLocal.UseVisualStyleBackColor = True
        '
        'Label9
        '
        resources.ApplyResources(Me.Label9, "Label9")
        Me.Label9.ForeColor = System.Drawing.Color.Red
        Me.Label9.Name = "Label9"
        '
        'RadioSql
        '
        resources.ApplyResources(Me.RadioSql, "RadioSql")
        Me.RadioSql.Checked = True
        Me.RadioSql.ForeColor = System.Drawing.Color.Red
        Me.RadioSql.Name = "RadioSql"
        Me.RadioSql.TabStop = True
        Me.RadioSql.UseVisualStyleBackColor = True
        '
        'Label2
        '
        resources.ApplyResources(Me.Label2, "Label2")
        Me.Label2.ForeColor = System.Drawing.Color.Red
        Me.Label2.Name = "Label2"
        '
        'BtnDefault
        '
        resources.ApplyResources(Me.BtnDefault, "BtnDefault")
        Me.BtnDefault.Name = "BtnDefault"
        Me.BtnDefault.UseVisualStyleBackColor = True
        '
        'BtnSaveExit
        '
        resources.ApplyResources(Me.BtnSaveExit, "BtnSaveExit")
        Me.BtnSaveExit.Name = "BtnSaveExit"
        Me.BtnSaveExit.UseVisualStyleBackColor = True
        '
        'BtnExit
        '
        resources.ApplyResources(Me.BtnExit, "BtnExit")
        Me.BtnExit.Name = "BtnExit"
        Me.BtnExit.UseVisualStyleBackColor = True
        '
        'UltraGroupBox1
        '
        Me.UltraGroupBox1.CaptionAlignment = Infragistics.Win.Misc.GroupBoxCaptionAlignment.Center
        Me.UltraGroupBox1.Controls.Add(Me.LabelSize)
        Me.UltraGroupBox1.Controls.Add(Me.Label7)
        Me.UltraGroupBox1.Controls.Add(Me.LabelDb)
        Me.UltraGroupBox1.Controls.Add(Me.Label5)
        Me.UltraGroupBox1.Controls.Add(Me.LabelStart)
        Me.UltraGroupBox1.Controls.Add(Me.Labelang)
        Me.UltraGroupBox1.Controls.Add(Me.Label8)
        Me.UltraGroupBox1.Controls.Add(Me.Label3)
        resources.ApplyResources(Me.UltraGroupBox1, "UltraGroupBox1")
        Me.UltraGroupBox1.Name = "UltraGroupBox1"
        Me.UltraGroupBox1.ViewStyle = Infragistics.Win.Misc.GroupBoxViewStyle.Office2000
        '
        'LabelSize
        '
        resources.ApplyResources(Me.LabelSize, "LabelSize")
        Me.LabelSize.ForeColor = System.Drawing.Color.Fuchsia
        Me.LabelSize.Name = "LabelSize"
        '
        'Label7
        '
        resources.ApplyResources(Me.Label7, "Label7")
        Me.Label7.ForeColor = System.Drawing.Color.Yellow
        Me.Label7.Name = "Label7"
        '
        'LabelDb
        '
        resources.ApplyResources(Me.LabelDb, "LabelDb")
        Me.LabelDb.ForeColor = System.Drawing.Color.Fuchsia
        Me.LabelDb.Name = "LabelDb"
        '
        'Label5
        '
        resources.ApplyResources(Me.Label5, "Label5")
        Me.Label5.ForeColor = System.Drawing.Color.Yellow
        Me.Label5.Name = "Label5"
        '
        'LabelStart
        '
        resources.ApplyResources(Me.LabelStart, "LabelStart")
        Me.LabelStart.ForeColor = System.Drawing.Color.Fuchsia
        Me.LabelStart.Name = "LabelStart"
        '
        'Labelang
        '
        resources.ApplyResources(Me.Labelang, "Labelang")
        Me.Labelang.ForeColor = System.Drawing.Color.Fuchsia
        Me.Labelang.Name = "Labelang"
        '
        'Label8
        '
        resources.ApplyResources(Me.Label8, "Label8")
        Me.Label8.ForeColor = System.Drawing.Color.Yellow
        Me.Label8.Name = "Label8"
        '
        'Label3
        '
        resources.ApplyResources(Me.Label3, "Label3")
        Me.Label3.ForeColor = System.Drawing.Color.Yellow
        Me.Label3.Name = "Label3"
        '
        'UltraGroupBox2
        '
        Me.UltraGroupBox2.CaptionAlignment = Infragistics.Win.Misc.GroupBoxCaptionAlignment.Center
        Me.UltraGroupBox2.Controls.Add(Me.Label4)
        Me.UltraGroupBox2.Controls.Add(Me.RadioNormal)
        Me.UltraGroupBox2.Controls.Add(Me.RadioLarge)
        resources.ApplyResources(Me.UltraGroupBox2, "UltraGroupBox2")
        Me.UltraGroupBox2.Name = "UltraGroupBox2"
        Me.UltraGroupBox2.ViewStyle = Infragistics.Win.Misc.GroupBoxViewStyle.Office2000
        '
        'Label4
        '
        resources.ApplyResources(Me.Label4, "Label4")
        Me.Label4.ForeColor = System.Drawing.Color.Blue
        Me.Label4.Name = "Label4"
        '
        'RadioNormal
        '
        resources.ApplyResources(Me.RadioNormal, "RadioNormal")
        Me.RadioNormal.Checked = True
        Me.RadioNormal.ForeColor = System.Drawing.Color.Blue
        Me.RadioNormal.Name = "RadioNormal"
        Me.RadioNormal.TabStop = True
        Me.RadioNormal.UseVisualStyleBackColor = True
        '
        'RadioLarge
        '
        resources.ApplyResources(Me.RadioLarge, "RadioLarge")
        Me.RadioLarge.ForeColor = System.Drawing.Color.Blue
        Me.RadioLarge.Name = "RadioLarge"
        Me.RadioLarge.UseVisualStyleBackColor = True
        '
        'UltraGroupBox3
        '
        Me.UltraGroupBox3.CaptionAlignment = Infragistics.Win.Misc.GroupBoxCaptionAlignment.Center
        Me.UltraGroupBox3.Controls.Add(Me.Label6)
        Me.UltraGroupBox3.Controls.Add(Me.RadioStar3)
        Me.UltraGroupBox3.Controls.Add(Me.RadioStar1)
        resources.ApplyResources(Me.UltraGroupBox3, "UltraGroupBox3")
        Me.UltraGroupBox3.Name = "UltraGroupBox3"
        Me.UltraGroupBox3.ViewStyle = Infragistics.Win.Misc.GroupBoxViewStyle.Office2000
        '
        'Label6
        '
        resources.ApplyResources(Me.Label6, "Label6")
        Me.Label6.ForeColor = System.Drawing.Color.Honeydew
        Me.Label6.Name = "Label6"
        '
        'RadioStar3
        '
        resources.ApplyResources(Me.RadioStar3, "RadioStar3")
        Me.RadioStar3.Checked = True
        Me.RadioStar3.ForeColor = System.Drawing.Color.Honeydew
        Me.RadioStar3.Name = "RadioStar3"
        Me.RadioStar3.TabStop = True
        Me.RadioStar3.UseVisualStyleBackColor = True
        '
        'RadioStar1
        '
        resources.ApplyResources(Me.RadioStar1, "RadioStar1")
        Me.RadioStar1.ForeColor = System.Drawing.Color.Honeydew
        Me.RadioStar1.Name = "RadioStar1"
        Me.RadioStar1.UseVisualStyleBackColor = True
        '
        'btnCreateSVG
        '
        resources.ApplyResources(Me.btnCreateSVG, "btnCreateSVG")
        Me.btnCreateSVG.Name = "btnCreateSVG"
        Me.btnCreateSVG.UseVisualStyleBackColor = True
        '
        'btnCreateSVGKids
        '
        resources.ApplyResources(Me.btnCreateSVGKids, "btnCreateSVGKids")
        Me.btnCreateSVGKids.Name = "btnCreateSVGKids"
        Me.btnCreateSVGKids.UseVisualStyleBackColor = True
        '
        'btnLoadSvgs
        '
        Me.btnLoadSvgs.Appearance.Font = CType(resources.GetObject("btnLoadSvgs.Appearance.Font"), System.Drawing.Font)
        Me.btnLoadSvgs.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.btnLoadSvgs, "btnLoadSvgs")
        Me.btnLoadSvgs.Name = "btnLoadSvgs"
        '
        'btnCreatApp
        '
        Me.btnCreatApp.Appearance.Font = CType(resources.GetObject("btnCreatApp.Appearance.Font"), System.Drawing.Font)
        Me.btnCreatApp.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.btnCreatApp, "btnCreatApp")
        Me.btnCreatApp.Name = "btnCreatApp"
        '
        'btnExtractSVG
        '
        Me.btnExtractSVG.Appearance.Font = CType(resources.GetObject("btnExtractSVG.Appearance.Font"), System.Drawing.Font)
        Me.btnExtractSVG.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.btnExtractSVG, "btnExtractSVG")
        Me.btnExtractSVG.Name = "btnExtractSVG"
        '
        'UltraGroupBox4
        '
        Me.UltraGroupBox4.CaptionAlignment = Infragistics.Win.Misc.GroupBoxCaptionAlignment.Center
        Me.UltraGroupBox4.Controls.Add(Me.ShortRemind)
        Me.UltraGroupBox4.Controls.Add(Me.Label10)
        resources.ApplyResources(Me.UltraGroupBox4, "UltraGroupBox4")
        Me.UltraGroupBox4.Name = "UltraGroupBox4"
        Me.UltraGroupBox4.ViewStyle = Infragistics.Win.Misc.GroupBoxViewStyle.Office2000
        '
        'ShortRemind
        '
        resources.ApplyResources(Me.ShortRemind, "ShortRemind")
        Me.ShortRemind.Name = "ShortRemind"
        Me.ShortRemind.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("ShortRemind.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me.ShortRemind.Properties.MaxValue = New Decimal(New Integer() {5, 0, 0, 0})
        Me.ShortRemind.Properties.MinValue = New Decimal(New Integer() {2, 0, 0, 0})
        '
        'Label10
        '
        resources.ApplyResources(Me.Label10, "Label10")
        Me.Label10.ForeColor = System.Drawing.Color.Honeydew
        Me.Label10.Name = "Label10"
        '
        'FormSettings
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(191, Byte), Integer), CType(CType(219, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.Controls.Add(Me.btnExtractSVG)
        Me.Controls.Add(Me.btnCreatApp)
        Me.Controls.Add(Me.btnLoadSvgs)
        Me.Controls.Add(Me.BtnExit)
        Me.Controls.Add(Me.BtnSaveExit)
        Me.Controls.Add(Me.BtnDefault)
        Me.Controls.Add(Me.GrpDbase)
        Me.Controls.Add(Me.UltraGroupBox1)
        Me.Controls.Add(Me.UltraGroupBox4)
        Me.Controls.Add(Me.UltraGroupBox3)
        Me.Controls.Add(Me.UltraGroupBox2)
        Me.Controls.Add(Me.GrpLang)
        Me.Controls.Add(Me.btnCreateSVGKids)
        Me.Controls.Add(Me.btnCreateSVG)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FormSettings"
        Me.ShowInTaskbar = False
        CType(Me.GrpLang, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GrpLang.ResumeLayout(False)
        Me.GrpLang.PerformLayout()
        CType(Me.GrpDbase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GrpDbase.ResumeLayout(False)
        Me.GrpDbase.PerformLayout()
        CType(Me.UltraGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.UltraGroupBox1.ResumeLayout(False)
        Me.UltraGroupBox1.PerformLayout()
        CType(Me.UltraGroupBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.UltraGroupBox2.ResumeLayout(False)
        Me.UltraGroupBox2.PerformLayout()
        CType(Me.UltraGroupBox3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.UltraGroupBox3.ResumeLayout(False)
        Me.UltraGroupBox3.PerformLayout()
        CType(Me.UltraGroupBox4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.UltraGroupBox4.ResumeLayout(False)
        Me.UltraGroupBox4.PerformLayout()
        CType(Me.ShortRemind.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents RadioArab As Windows.Forms.RadioButton
    Friend WithEvents RadioEng As Windows.Forms.RadioButton
    Friend WithEvents BtnExit As Windows.Forms.Button
    Friend WithEvents BtnSaveExit As Windows.Forms.Button
    Friend WithEvents BtnDefault As Windows.Forms.Button
    Friend WithEvents GrpDbase As Infragistics.Win.Misc.UltraGroupBox
    Friend WithEvents RadioLocal As Windows.Forms.RadioButton
    Friend WithEvents RadioSql As Windows.Forms.RadioButton
    Friend WithEvents Label2 As Windows.Forms.Label
    Friend WithEvents GrpLang As Infragistics.Win.Misc.UltraGroupBox
    Friend WithEvents Label1 As Windows.Forms.Label
    Friend WithEvents UltraGroupBox1 As Infragistics.Win.Misc.UltraGroupBox
    Friend WithEvents LabelDb As Windows.Forms.Label
    Friend WithEvents Label5 As Windows.Forms.Label
    Friend WithEvents Labelang As Windows.Forms.Label
    Friend WithEvents Label3 As Windows.Forms.Label
    Friend WithEvents RadioLocal2012 As Windows.Forms.RadioButton
    Friend WithEvents RadioOther As Windows.Forms.RadioButton
    Friend WithEvents LabelSize As Windows.Forms.Label
    Friend WithEvents Label7 As Windows.Forms.Label
    Friend WithEvents UltraGroupBox2 As Infragistics.Win.Misc.UltraGroupBox
    Friend WithEvents Label4 As Windows.Forms.Label
    Friend WithEvents RadioNormal As Windows.Forms.RadioButton
    Friend WithEvents RadioLarge As Windows.Forms.RadioButton
    Friend WithEvents txtHostName As Windows.Forms.TextBox
    Friend WithEvents UltraGroupBox3 As Infragistics.Win.Misc.UltraGroupBox
    Friend WithEvents Label6 As Windows.Forms.Label
    Friend WithEvents RadioStar3 As Windows.Forms.RadioButton
    Friend WithEvents RadioStar1 As Windows.Forms.RadioButton
    Friend WithEvents LabelStart As Windows.Forms.Label
    Friend WithEvents Label8 As Windows.Forms.Label
    Friend WithEvents btnCreateSVG As Windows.Forms.Button
    Friend WithEvents btnCreateSVGKids As Button
    Friend WithEvents txtSrvrPass As TextBox
    Friend WithEvents Label9 As Label
    Friend WithEvents btnLoadSvgs As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnCreatApp As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnExtractSVG As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents UltraGroupBox4 As Infragistics.Win.Misc.UltraGroupBox
    Friend WithEvents ShortRemind As DevExpress.XtraEditors.SpinEdit
    Friend WithEvents Label10 As Label
End Class
