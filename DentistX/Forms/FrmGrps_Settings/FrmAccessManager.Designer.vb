<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmAccessManager
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmAccessManager))
        Me.GridControl1 = New DevExpress.XtraGrid.GridControl()
        Me.GridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.BtnAddForm = New DevExpress.XtraEditors.SimpleButton()
        Me.BtnEditForm = New DevExpress.XtraEditors.SimpleButton()
        Me.BtnDelForm = New DevExpress.XtraEditors.SimpleButton()
        Me.TxtFormName = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        CType(Me.GridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtFormName.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GridControl1
        '
        Me.GridControl1.EmbeddedNavigator.Margin = CType(resources.GetObject("GridControl1.EmbeddedNavigator.Margin"), System.Windows.Forms.Padding)
        resources.ApplyResources(Me.GridControl1, "GridControl1")
        Me.GridControl1.MainView = Me.GridView1
        Me.GridControl1.Name = "GridControl1"
        Me.GridControl1.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridView1})
        '
        'GridView1
        '
        Me.GridView1.Appearance.HeaderPanel.Font = CType(resources.GetObject("GridView1.Appearance.HeaderPanel.Font"), System.Drawing.Font)
        Me.GridView1.Appearance.HeaderPanel.Options.UseFont = True
        Me.GridView1.Appearance.Row.Font = CType(resources.GetObject("GridView1.Appearance.Row.Font"), System.Drawing.Font)
        Me.GridView1.Appearance.Row.Options.UseFont = True
        Me.GridView1.DetailHeight = 404
        Me.GridView1.GridControl = Me.GridControl1
        Me.GridView1.Name = "GridView1"
        Me.GridView1.OptionsEditForm.PopupEditFormWidth = 933
        '
        'BtnAddForm
        '
        Me.BtnAddForm.Appearance.Font = CType(resources.GetObject("BtnAddForm.Appearance.Font"), System.Drawing.Font)
        Me.BtnAddForm.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.BtnAddForm, "BtnAddForm")
        Me.BtnAddForm.Name = "BtnAddForm"
        '
        'BtnEditForm
        '
        Me.BtnEditForm.Appearance.Font = CType(resources.GetObject("BtnEditForm.Appearance.Font"), System.Drawing.Font)
        Me.BtnEditForm.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.BtnEditForm, "BtnEditForm")
        Me.BtnEditForm.Name = "BtnEditForm"
        '
        'BtnDelForm
        '
        Me.BtnDelForm.Appearance.Font = CType(resources.GetObject("BtnDelForm.Appearance.Font"), System.Drawing.Font)
        Me.BtnDelForm.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.BtnDelForm, "BtnDelForm")
        Me.BtnDelForm.Name = "BtnDelForm"
        '
        'TxtFormName
        '
        resources.ApplyResources(Me.TxtFormName, "TxtFormName")
        Me.TxtFormName.Name = "TxtFormName"
        Me.TxtFormName.Properties.Appearance.Font = CType(resources.GetObject("TxtFormName.Properties.Appearance.Font"), System.Drawing.Font)
        Me.TxtFormName.Properties.Appearance.Options.UseFont = True
        '
        'LabelControl1
        '
        Me.LabelControl1.Appearance.Font = CType(resources.GetObject("LabelControl1.Appearance.Font"), System.Drawing.Font)
        Me.LabelControl1.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.LabelControl1, "LabelControl1")
        Me.LabelControl1.Name = "LabelControl1"
        '
        'FrmAccessManager
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.LabelControl1)
        Me.Controls.Add(Me.TxtFormName)
        Me.Controls.Add(Me.BtnDelForm)
        Me.Controls.Add(Me.BtnEditForm)
        Me.Controls.Add(Me.BtnAddForm)
        Me.Controls.Add(Me.GridControl1)
        Me.Name = "FrmAccessManager"
        CType(Me.GridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtFormName.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents GridControl1 As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridView1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents BtnAddForm As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents BtnEditForm As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents BtnDelForm As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents TxtFormName As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
End Class
