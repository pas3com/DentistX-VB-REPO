<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class PermissionsForm
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(PermissionsForm))
        Me.GridControl1 = New DevExpress.XtraGrid.GridControl()
        Me.GridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.CboLevel = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.CboTarget = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.BtnSave = New DevExpress.XtraEditors.SimpleButton()
        Me.BtnRefresh = New DevExpress.XtraEditors.SimpleButton()
        CType(Me.GridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CboLevel.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CboTarget.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GridControl1
        '
        resources.ApplyResources(Me.GridControl1, "GridControl1")
        Me.GridControl1.EmbeddedNavigator.AccessibleDescription = resources.GetString("GridControl1.EmbeddedNavigator.AccessibleDescription")
        Me.GridControl1.EmbeddedNavigator.AccessibleName = resources.GetString("GridControl1.EmbeddedNavigator.AccessibleName")
        Me.GridControl1.EmbeddedNavigator.AllowHtmlTextInToolTip = CType(resources.GetObject("GridControl1.EmbeddedNavigator.AllowHtmlTextInToolTip"), DevExpress.Utils.DefaultBoolean)
        Me.GridControl1.EmbeddedNavigator.Anchor = CType(resources.GetObject("GridControl1.EmbeddedNavigator.Anchor"), System.Windows.Forms.AnchorStyles)
        Me.GridControl1.EmbeddedNavigator.AutoSize = CType(resources.GetObject("GridControl1.EmbeddedNavigator.AutoSize"), Boolean)
        Me.GridControl1.EmbeddedNavigator.BackgroundImage = CType(resources.GetObject("GridControl1.EmbeddedNavigator.BackgroundImage"), System.Drawing.Image)
        Me.GridControl1.EmbeddedNavigator.BackgroundImageLayout = CType(resources.GetObject("GridControl1.EmbeddedNavigator.BackgroundImageLayout"), System.Windows.Forms.ImageLayout)
        Me.GridControl1.EmbeddedNavigator.ImeMode = CType(resources.GetObject("GridControl1.EmbeddedNavigator.ImeMode"), System.Windows.Forms.ImeMode)
        Me.GridControl1.EmbeddedNavigator.MaximumSize = CType(resources.GetObject("GridControl1.EmbeddedNavigator.MaximumSize"), System.Drawing.Size)
        Me.GridControl1.EmbeddedNavigator.TextLocation = CType(resources.GetObject("GridControl1.EmbeddedNavigator.TextLocation"), DevExpress.XtraEditors.NavigatorButtonsTextLocation)
        Me.GridControl1.EmbeddedNavigator.ToolTip = resources.GetString("GridControl1.EmbeddedNavigator.ToolTip")
        Me.GridControl1.EmbeddedNavigator.ToolTipIconType = CType(resources.GetObject("GridControl1.EmbeddedNavigator.ToolTipIconType"), DevExpress.Utils.ToolTipIconType)
        Me.GridControl1.EmbeddedNavigator.ToolTipTitle = resources.GetString("GridControl1.EmbeddedNavigator.ToolTipTitle")
        Me.GridControl1.MainView = Me.GridView1
        Me.GridControl1.Name = "GridControl1"
        Me.GridControl1.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridView1})
        '
        'GridView1
        '
        resources.ApplyResources(Me.GridView1, "GridView1")
        Me.GridView1.GridControl = Me.GridControl1
        Me.GridView1.Name = "GridView1"
        Me.GridView1.OptionsView.ShowGroupPanel = False
        '
        'CboLevel
        '
        resources.ApplyResources(Me.CboLevel, "CboLevel")
        Me.CboLevel.Name = "CboLevel"
        Me.CboLevel.Properties.Appearance.Font = CType(resources.GetObject("CboLevel.Properties.Appearance.Font"), System.Drawing.Font)
        Me.CboLevel.Properties.Appearance.Options.UseFont = True
        Me.CboLevel.Properties.Items.AddRange(New Object() {resources.GetString("CboLevel.Properties.Items"), resources.GetString("CboLevel.Properties.Items1")})
        '
        'CboTarget
        '
        resources.ApplyResources(Me.CboTarget, "CboTarget")
        Me.CboTarget.Name = "CboTarget"
        Me.CboTarget.Properties.Appearance.Font = CType(resources.GetObject("CboTarget.Properties.Appearance.Font"), System.Drawing.Font)
        Me.CboTarget.Properties.Appearance.Options.UseFont = True
        '
        'BtnSave
        '
        resources.ApplyResources(Me.BtnSave, "BtnSave")
        Me.BtnSave.Appearance.Font = CType(resources.GetObject("BtnSave.Appearance.Font"), System.Drawing.Font)
        Me.BtnSave.Appearance.Options.UseFont = True
        Me.BtnSave.ImageOptions.ImageKey = resources.GetString("BtnSave.ImageOptions.ImageKey")
        Me.BtnSave.Name = "BtnSave"
        '
        'BtnRefresh
        '
        resources.ApplyResources(Me.BtnRefresh, "BtnRefresh")
        Me.BtnRefresh.Appearance.Font = CType(resources.GetObject("BtnRefresh.Appearance.Font"), System.Drawing.Font)
        Me.BtnRefresh.Appearance.Options.UseFont = True
        Me.BtnRefresh.ImageOptions.ImageKey = resources.GetString("BtnRefresh.ImageOptions.ImageKey")
        Me.BtnRefresh.Name = "BtnRefresh"
        '
        'PermissionsForm
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.BtnRefresh)
        Me.Controls.Add(Me.BtnSave)
        Me.Controls.Add(Me.CboTarget)
        Me.Controls.Add(Me.CboLevel)
        Me.Controls.Add(Me.GridControl1)
        Me.Name = "PermissionsForm"
        CType(Me.GridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CboLevel.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CboTarget.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents GridControl1 As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridView1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents CboLevel As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents CboTarget As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents BtnSave As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents BtnRefresh As DevExpress.XtraEditors.SimpleButton
End Class
