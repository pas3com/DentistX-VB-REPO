<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmGroupManager
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmGroupManager))
        Me.GridControl1 = New DevExpress.XtraGrid.GridControl()
        Me.GridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.BtnAddGroup = New DevExpress.XtraEditors.SimpleButton()
        Me.BtnEditGroup = New DevExpress.XtraEditors.SimpleButton()
        Me.BtnDelGroup = New DevExpress.XtraEditors.SimpleButton()
        Me.TxtGroupName = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        CType(Me.GridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtGroupName.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.GridView1.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.GridView1.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.GridView1.OptionsBehavior.Editable = False
        Me.GridView1.OptionsEditForm.PopupEditFormWidth = 933
        '
        'BtnAddGroup
        '
        Me.BtnAddGroup.Appearance.Font = CType(resources.GetObject("BtnAddGroup.Appearance.Font"), System.Drawing.Font)
        Me.BtnAddGroup.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.BtnAddGroup, "BtnAddGroup")
        Me.BtnAddGroup.Name = "BtnAddGroup"
        '
        'BtnEditGroup
        '
        Me.BtnEditGroup.Appearance.Font = CType(resources.GetObject("BtnEditGroup.Appearance.Font"), System.Drawing.Font)
        Me.BtnEditGroup.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.BtnEditGroup, "BtnEditGroup")
        Me.BtnEditGroup.Name = "BtnEditGroup"
        '
        'BtnDelGroup
        '
        Me.BtnDelGroup.Appearance.Font = CType(resources.GetObject("BtnDelGroup.Appearance.Font"), System.Drawing.Font)
        Me.BtnDelGroup.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.BtnDelGroup, "BtnDelGroup")
        Me.BtnDelGroup.Name = "BtnDelGroup"
        '
        'TxtGroupName
        '
        resources.ApplyResources(Me.TxtGroupName, "TxtGroupName")
        Me.TxtGroupName.Name = "TxtGroupName"
        Me.TxtGroupName.Properties.Appearance.Font = CType(resources.GetObject("TxtGroupName.Properties.Appearance.Font"), System.Drawing.Font)
        Me.TxtGroupName.Properties.Appearance.Options.UseFont = True
        '
        'LabelControl1
        '
        Me.LabelControl1.Appearance.Font = CType(resources.GetObject("LabelControl1.Appearance.Font"), System.Drawing.Font)
        Me.LabelControl1.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.LabelControl1, "LabelControl1")
        Me.LabelControl1.Name = "LabelControl1"
        '
        'FrmGroupManager
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.LabelControl1)
        Me.Controls.Add(Me.TxtGroupName)
        Me.Controls.Add(Me.BtnDelGroup)
        Me.Controls.Add(Me.BtnEditGroup)
        Me.Controls.Add(Me.BtnAddGroup)
        Me.Controls.Add(Me.GridControl1)
        Me.Name = "FrmGroupManager"
        CType(Me.GridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtGroupName.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents GridControl1 As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridView1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents BtnAddGroup As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents BtnEditGroup As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents BtnDelGroup As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents TxtGroupName As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
End Class
