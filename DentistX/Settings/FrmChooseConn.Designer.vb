<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmChooseConn
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmChooseConn))
        Me.RadioOther = New System.Windows.Forms.RadioButton()
        Me.RadioLocal2012 = New System.Windows.Forms.RadioButton()
        Me.RadioLocal = New System.Windows.Forms.RadioButton()
        Me.RadioSql = New System.Windows.Forms.RadioButton()
        Me.btnConnect = New DevExpress.XtraEditors.SimpleButton()
        Me.GroupControl1 = New DevExpress.XtraEditors.GroupControl()
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.txtSrvrPass = New DevExpress.XtraEditors.TextEdit()
        Me.txtHostName = New DevExpress.XtraEditors.TextEdit()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl1.SuspendLayout()
        CType(Me.txtSrvrPass.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtHostName.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
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
        'RadioSql
        '
        resources.ApplyResources(Me.RadioSql, "RadioSql")
        Me.RadioSql.Checked = True
        Me.RadioSql.ForeColor = System.Drawing.Color.Red
        Me.RadioSql.Name = "RadioSql"
        Me.RadioSql.TabStop = True
        Me.RadioSql.UseVisualStyleBackColor = True
        '
        'btnConnect
        '
        Me.btnConnect.Appearance.Font = CType(resources.GetObject("btnConnect.Appearance.Font"), System.Drawing.Font)
        Me.btnConnect.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.btnConnect, "btnConnect")
        Me.btnConnect.Name = "btnConnect"
        '
        'GroupControl1
        '
        Me.GroupControl1.AppearanceCaption.Font = CType(resources.GetObject("GroupControl1.AppearanceCaption.Font"), System.Drawing.Font)
        Me.GroupControl1.AppearanceCaption.Options.UseFont = True
        Me.GroupControl1.Controls.Add(Me.btnConnect)
        Me.GroupControl1.Controls.Add(Me.LabelControl2)
        Me.GroupControl1.Controls.Add(Me.LabelControl1)
        Me.GroupControl1.Controls.Add(Me.txtSrvrPass)
        Me.GroupControl1.Controls.Add(Me.txtHostName)
        Me.GroupControl1.Controls.Add(Me.RadioLocal2012)
        Me.GroupControl1.Controls.Add(Me.RadioOther)
        Me.GroupControl1.Controls.Add(Me.RadioSql)
        Me.GroupControl1.Controls.Add(Me.RadioLocal)
        resources.ApplyResources(Me.GroupControl1, "GroupControl1")
        Me.GroupControl1.Name = "GroupControl1"
        '
        'LabelControl2
        '
        Me.LabelControl2.Appearance.Font = CType(resources.GetObject("LabelControl2.Appearance.Font"), System.Drawing.Font)
        Me.LabelControl2.Appearance.Options.UseFont = True
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
        'txtSrvrPass
        '
        resources.ApplyResources(Me.txtSrvrPass, "txtSrvrPass")
        Me.txtSrvrPass.Name = "txtSrvrPass"
        Me.txtSrvrPass.Properties.Appearance.Font = CType(resources.GetObject("txtSrvrPass.Properties.Appearance.Font"), System.Drawing.Font)
        Me.txtSrvrPass.Properties.Appearance.Options.UseFont = True
        Me.txtSrvrPass.Properties.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        '
        'txtHostName
        '
        resources.ApplyResources(Me.txtHostName, "txtHostName")
        Me.txtHostName.Name = "txtHostName"
        Me.txtHostName.Properties.Appearance.Font = CType(resources.GetObject("txtHostName.Properties.Appearance.Font"), System.Drawing.Font)
        Me.txtHostName.Properties.Appearance.Options.UseFont = True
        '
        'FrmChooseConn
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.GroupControl1)
        Me.Name = "FrmChooseConn"
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl1.ResumeLayout(False)
        Me.GroupControl1.PerformLayout()
        CType(Me.txtSrvrPass.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtHostName.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents RadioOther As RadioButton
    Friend WithEvents RadioLocal2012 As RadioButton
    Friend WithEvents RadioLocal As RadioButton
    Friend WithEvents RadioSql As RadioButton
    Friend WithEvents btnConnect As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents GroupControl1 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents txtSrvrPass As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtHostName As DevExpress.XtraEditors.TextEdit
End Class
