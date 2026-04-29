<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmPermissionsMngr
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmPermissionsMngr))
        Me.SplitContainerControl1 = New DevExpress.XtraEditors.SplitContainerControl()
        Me.grpSlctdUser_GRP = New DevExpress.XtraEditors.GroupControl()
        Me.GridSlctdUser_Grp = New DevExpress.XtraGrid.GridControl()
        Me.GridViewUserGrp = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.PanelControl1 = New DevExpress.XtraEditors.PanelControl()
        Me.grpPermissions = New DevExpress.XtraEditors.GroupControl()
        Me.GridPermissions = New DevExpress.XtraGrid.GridControl()
        Me.GridViewPermissions = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GroupControl1 = New DevExpress.XtraEditors.GroupControl()
        Me.btnShowGrpUsr = New DevExpress.XtraEditors.ToggleSwitch()
        Me.PanelTop = New DevExpress.XtraEditors.PanelControl()
        Me.LabelControl3 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.BtnSave = New DevExpress.XtraEditors.SimpleButton()
        Me.USERSCombo1 = New DentistX.USERSCombo()
        Me.GroupsCombo1 = New DentistX.GroupsCombo()
        Me.PermissionsCombo1 = New DentistX.PermissionsCombo()
        CType(Me.SplitContainerControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SplitContainerControl1.Panel1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainerControl1.Panel1.SuspendLayout()
        CType(Me.SplitContainerControl1.Panel2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainerControl1.Panel2.SuspendLayout()
        Me.SplitContainerControl1.SuspendLayout()
        CType(Me.grpSlctdUser_GRP, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpSlctdUser_GRP.SuspendLayout()
        CType(Me.GridSlctdUser_Grp, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridViewUserGrp, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl1.SuspendLayout()
        CType(Me.grpPermissions, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpPermissions.SuspendLayout()
        CType(Me.GridPermissions, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridViewPermissions, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl1.SuspendLayout()
        CType(Me.btnShowGrpUsr.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PanelTop, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelTop.SuspendLayout()
        Me.SuspendLayout()
        '
        'SplitContainerControl1
        '
        resources.ApplyResources(Me.SplitContainerControl1, "SplitContainerControl1")
        Me.SplitContainerControl1.CaptionImageOptions.ImageKey = resources.GetString("SplitContainerControl1.CaptionImageOptions.ImageKey")
        Me.SplitContainerControl1.Name = "SplitContainerControl1"
        '
        'SplitContainerControl1.Panel1
        '
        resources.ApplyResources(Me.SplitContainerControl1.Panel1, "SplitContainerControl1.Panel1")
        Me.SplitContainerControl1.Panel1.Controls.Add(Me.grpSlctdUser_GRP)
        '
        'SplitContainerControl1.Panel2
        '
        resources.ApplyResources(Me.SplitContainerControl1.Panel2, "SplitContainerControl1.Panel2")
        Me.SplitContainerControl1.Panel2.Controls.Add(Me.PanelControl1)
        Me.SplitContainerControl1.Panel2.Controls.Add(Me.GroupControl1)
        Me.SplitContainerControl1.SplitterPosition = 559
        '
        'grpSlctdUser_GRP
        '
        resources.ApplyResources(Me.grpSlctdUser_GRP, "grpSlctdUser_GRP")
        Me.grpSlctdUser_GRP.AppearanceCaption.Font = CType(resources.GetObject("grpSlctdUser_GRP.AppearanceCaption.Font"), System.Drawing.Font)
        Me.grpSlctdUser_GRP.AppearanceCaption.Options.UseFont = True
        Me.grpSlctdUser_GRP.Controls.Add(Me.GridSlctdUser_Grp)
        Me.grpSlctdUser_GRP.Name = "grpSlctdUser_GRP"
        '
        'GridSlctdUser_Grp
        '
        resources.ApplyResources(Me.GridSlctdUser_Grp, "GridSlctdUser_Grp")
        Me.GridSlctdUser_Grp.EmbeddedNavigator.AccessibleDescription = resources.GetString("GridSlctdUser_Grp.EmbeddedNavigator.AccessibleDescription")
        Me.GridSlctdUser_Grp.EmbeddedNavigator.AccessibleName = resources.GetString("GridSlctdUser_Grp.EmbeddedNavigator.AccessibleName")
        Me.GridSlctdUser_Grp.EmbeddedNavigator.AllowHtmlTextInToolTip = CType(resources.GetObject("GridSlctdUser_Grp.EmbeddedNavigator.AllowHtmlTextInToolTip"), DevExpress.Utils.DefaultBoolean)
        Me.GridSlctdUser_Grp.EmbeddedNavigator.Anchor = CType(resources.GetObject("GridSlctdUser_Grp.EmbeddedNavigator.Anchor"), System.Windows.Forms.AnchorStyles)
        Me.GridSlctdUser_Grp.EmbeddedNavigator.AutoSize = CType(resources.GetObject("GridSlctdUser_Grp.EmbeddedNavigator.AutoSize"), Boolean)
        Me.GridSlctdUser_Grp.EmbeddedNavigator.BackgroundImage = CType(resources.GetObject("GridSlctdUser_Grp.EmbeddedNavigator.BackgroundImage"), System.Drawing.Image)
        Me.GridSlctdUser_Grp.EmbeddedNavigator.BackgroundImageLayout = CType(resources.GetObject("GridSlctdUser_Grp.EmbeddedNavigator.BackgroundImageLayout"), System.Windows.Forms.ImageLayout)
        Me.GridSlctdUser_Grp.EmbeddedNavigator.ImeMode = CType(resources.GetObject("GridSlctdUser_Grp.EmbeddedNavigator.ImeMode"), System.Windows.Forms.ImeMode)
        Me.GridSlctdUser_Grp.EmbeddedNavigator.MaximumSize = CType(resources.GetObject("GridSlctdUser_Grp.EmbeddedNavigator.MaximumSize"), System.Drawing.Size)
        Me.GridSlctdUser_Grp.EmbeddedNavigator.TextLocation = CType(resources.GetObject("GridSlctdUser_Grp.EmbeddedNavigator.TextLocation"), DevExpress.XtraEditors.NavigatorButtonsTextLocation)
        Me.GridSlctdUser_Grp.EmbeddedNavigator.ToolTip = resources.GetString("GridSlctdUser_Grp.EmbeddedNavigator.ToolTip")
        Me.GridSlctdUser_Grp.EmbeddedNavigator.ToolTipIconType = CType(resources.GetObject("GridSlctdUser_Grp.EmbeddedNavigator.ToolTipIconType"), DevExpress.Utils.ToolTipIconType)
        Me.GridSlctdUser_Grp.EmbeddedNavigator.ToolTipTitle = resources.GetString("GridSlctdUser_Grp.EmbeddedNavigator.ToolTipTitle")
        Me.GridSlctdUser_Grp.MainView = Me.GridViewUserGrp
        Me.GridSlctdUser_Grp.Name = "GridSlctdUser_Grp"
        Me.GridSlctdUser_Grp.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridViewUserGrp})
        '
        'GridViewUserGrp
        '
        resources.ApplyResources(Me.GridViewUserGrp, "GridViewUserGrp")
        Me.GridViewUserGrp.Appearance.HeaderPanel.Font = CType(resources.GetObject("GridViewUserGrp.Appearance.HeaderPanel.Font"), System.Drawing.Font)
        Me.GridViewUserGrp.Appearance.HeaderPanel.ForeColor = System.Drawing.Color.MidnightBlue
        Me.GridViewUserGrp.Appearance.HeaderPanel.Options.UseFont = True
        Me.GridViewUserGrp.Appearance.HeaderPanel.Options.UseForeColor = True
        Me.GridViewUserGrp.Appearance.Row.BackColor = System.Drawing.Color.Lavender
        Me.GridViewUserGrp.Appearance.Row.BackColor2 = CType(resources.GetObject("GridViewUserGrp.Appearance.Row.BackColor2"), System.Drawing.Color)
        Me.GridViewUserGrp.Appearance.Row.Options.UseBackColor = True
        Me.GridViewUserGrp.GridControl = Me.GridSlctdUser_Grp
        Me.GridViewUserGrp.Name = "GridViewUserGrp"
        Me.GridViewUserGrp.OptionsView.EnableAppearanceEvenRow = True
        Me.GridViewUserGrp.OptionsView.EnableAppearanceOddRow = True
        Me.GridViewUserGrp.OptionsView.ShowGroupPanel = False
        '
        'PanelControl1
        '
        resources.ApplyResources(Me.PanelControl1, "PanelControl1")
        Me.PanelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.PanelControl1.Controls.Add(Me.grpPermissions)
        Me.PanelControl1.Name = "PanelControl1"
        '
        'grpPermissions
        '
        resources.ApplyResources(Me.grpPermissions, "grpPermissions")
        Me.grpPermissions.AppearanceCaption.Font = CType(resources.GetObject("grpPermissions.AppearanceCaption.Font"), System.Drawing.Font)
        Me.grpPermissions.AppearanceCaption.Options.UseFont = True
        Me.grpPermissions.Controls.Add(Me.GridPermissions)
        Me.grpPermissions.Name = "grpPermissions"
        '
        'GridPermissions
        '
        resources.ApplyResources(Me.GridPermissions, "GridPermissions")
        Me.GridPermissions.EmbeddedNavigator.AccessibleDescription = resources.GetString("GridPermissions.EmbeddedNavigator.AccessibleDescription")
        Me.GridPermissions.EmbeddedNavigator.AccessibleName = resources.GetString("GridPermissions.EmbeddedNavigator.AccessibleName")
        Me.GridPermissions.EmbeddedNavigator.AllowHtmlTextInToolTip = CType(resources.GetObject("GridPermissions.EmbeddedNavigator.AllowHtmlTextInToolTip"), DevExpress.Utils.DefaultBoolean)
        Me.GridPermissions.EmbeddedNavigator.Anchor = CType(resources.GetObject("GridPermissions.EmbeddedNavigator.Anchor"), System.Windows.Forms.AnchorStyles)
        Me.GridPermissions.EmbeddedNavigator.AutoSize = CType(resources.GetObject("GridPermissions.EmbeddedNavigator.AutoSize"), Boolean)
        Me.GridPermissions.EmbeddedNavigator.BackgroundImage = CType(resources.GetObject("GridPermissions.EmbeddedNavigator.BackgroundImage"), System.Drawing.Image)
        Me.GridPermissions.EmbeddedNavigator.BackgroundImageLayout = CType(resources.GetObject("GridPermissions.EmbeddedNavigator.BackgroundImageLayout"), System.Windows.Forms.ImageLayout)
        Me.GridPermissions.EmbeddedNavigator.ImeMode = CType(resources.GetObject("GridPermissions.EmbeddedNavigator.ImeMode"), System.Windows.Forms.ImeMode)
        Me.GridPermissions.EmbeddedNavigator.MaximumSize = CType(resources.GetObject("GridPermissions.EmbeddedNavigator.MaximumSize"), System.Drawing.Size)
        Me.GridPermissions.EmbeddedNavigator.TextLocation = CType(resources.GetObject("GridPermissions.EmbeddedNavigator.TextLocation"), DevExpress.XtraEditors.NavigatorButtonsTextLocation)
        Me.GridPermissions.EmbeddedNavigator.ToolTip = resources.GetString("GridPermissions.EmbeddedNavigator.ToolTip")
        Me.GridPermissions.EmbeddedNavigator.ToolTipIconType = CType(resources.GetObject("GridPermissions.EmbeddedNavigator.ToolTipIconType"), DevExpress.Utils.ToolTipIconType)
        Me.GridPermissions.EmbeddedNavigator.ToolTipTitle = resources.GetString("GridPermissions.EmbeddedNavigator.ToolTipTitle")
        Me.GridPermissions.MainView = Me.GridViewPermissions
        Me.GridPermissions.Name = "GridPermissions"
        Me.GridPermissions.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridViewPermissions})
        '
        'GridViewPermissions
        '
        resources.ApplyResources(Me.GridViewPermissions, "GridViewPermissions")
        Me.GridViewPermissions.Appearance.HeaderPanel.Font = CType(resources.GetObject("GridViewPermissions.Appearance.HeaderPanel.Font"), System.Drawing.Font)
        Me.GridViewPermissions.Appearance.HeaderPanel.ForeColor = System.Drawing.Color.MidnightBlue
        Me.GridViewPermissions.Appearance.HeaderPanel.Options.UseFont = True
        Me.GridViewPermissions.Appearance.HeaderPanel.Options.UseForeColor = True
        Me.GridViewPermissions.Appearance.Row.BackColor = System.Drawing.Color.Lavender
        Me.GridViewPermissions.Appearance.Row.BackColor2 = CType(resources.GetObject("GridViewPermissions.Appearance.Row.BackColor2"), System.Drawing.Color)
        Me.GridViewPermissions.Appearance.Row.Options.UseBackColor = True
        Me.GridViewPermissions.GridControl = Me.GridPermissions
        Me.GridViewPermissions.Name = "GridViewPermissions"
        Me.GridViewPermissions.OptionsView.EnableAppearanceEvenRow = True
        Me.GridViewPermissions.OptionsView.EnableAppearanceOddRow = True
        Me.GridViewPermissions.OptionsView.ShowGroupPanel = False
        '
        'GroupControl1
        '
        resources.ApplyResources(Me.GroupControl1, "GroupControl1")
        Me.GroupControl1.AppearanceCaption.Font = CType(resources.GetObject("GroupControl1.AppearanceCaption.Font"), System.Drawing.Font)
        Me.GroupControl1.AppearanceCaption.Options.UseFont = True
        Me.GroupControl1.Controls.Add(Me.btnShowGrpUsr)
        Me.GroupControl1.Name = "GroupControl1"
        '
        'btnShowGrpUsr
        '
        resources.ApplyResources(Me.btnShowGrpUsr, "btnShowGrpUsr")
        Me.btnShowGrpUsr.Name = "btnShowGrpUsr"
        Me.btnShowGrpUsr.Properties.Appearance.Font = CType(resources.GetObject("btnShowGrpUsr.Properties.Appearance.Font"), System.Drawing.Font)
        Me.btnShowGrpUsr.Properties.Appearance.Options.UseFont = True
        Me.btnShowGrpUsr.Properties.GlyphVerticalAlignment = CType(resources.GetObject("btnShowGrpUsr.Properties.GlyphVerticalAlignment"), DevExpress.Utils.VertAlignment)
        Me.btnShowGrpUsr.Properties.OffText = resources.GetString("btnShowGrpUsr.Properties.OffText")
        Me.btnShowGrpUsr.Properties.OnText = resources.GetString("btnShowGrpUsr.Properties.OnText")
        '
        'PanelTop
        '
        resources.ApplyResources(Me.PanelTop, "PanelTop")
        Me.PanelTop.Controls.Add(Me.LabelControl3)
        Me.PanelTop.Controls.Add(Me.LabelControl2)
        Me.PanelTop.Controls.Add(Me.LabelControl1)
        Me.PanelTop.Controls.Add(Me.BtnSave)
        Me.PanelTop.Controls.Add(Me.USERSCombo1)
        Me.PanelTop.Controls.Add(Me.GroupsCombo1)
        Me.PanelTop.Controls.Add(Me.PermissionsCombo1)
        Me.PanelTop.Name = "PanelTop"
        '
        'LabelControl3
        '
        resources.ApplyResources(Me.LabelControl3, "LabelControl3")
        Me.LabelControl3.Appearance.Font = CType(resources.GetObject("LabelControl3.Appearance.Font"), System.Drawing.Font)
        Me.LabelControl3.Appearance.Options.UseFont = True
        Me.LabelControl3.Name = "LabelControl3"
        '
        'LabelControl2
        '
        resources.ApplyResources(Me.LabelControl2, "LabelControl2")
        Me.LabelControl2.Appearance.Font = CType(resources.GetObject("LabelControl2.Appearance.Font"), System.Drawing.Font)
        Me.LabelControl2.Appearance.Options.UseFont = True
        Me.LabelControl2.Name = "LabelControl2"
        '
        'LabelControl1
        '
        resources.ApplyResources(Me.LabelControl1, "LabelControl1")
        Me.LabelControl1.Appearance.Font = CType(resources.GetObject("LabelControl1.Appearance.Font"), System.Drawing.Font)
        Me.LabelControl1.Appearance.Options.UseFont = True
        Me.LabelControl1.Name = "LabelControl1"
        '
        'BtnSave
        '
        resources.ApplyResources(Me.BtnSave, "BtnSave")
        Me.BtnSave.Appearance.BackColor = System.Drawing.Color.MediumSeaGreen
        Me.BtnSave.Appearance.Font = CType(resources.GetObject("BtnSave.Appearance.Font"), System.Drawing.Font)
        Me.BtnSave.Appearance.ForeColor = System.Drawing.Color.White
        Me.BtnSave.Appearance.Options.UseBackColor = True
        Me.BtnSave.Appearance.Options.UseFont = True
        Me.BtnSave.Appearance.Options.UseForeColor = True
        Me.BtnSave.ImageOptions.ImageKey = resources.GetString("BtnSave.ImageOptions.ImageKey")
        Me.BtnSave.Name = "BtnSave"
        '
        'USERSCombo1
        '
        resources.ApplyResources(Me.USERSCombo1, "USERSCombo1")
        Me.USERSCombo1.Name = "USERSCombo1"
        Me.USERSCombo1.UsID = 0
        Me.USERSCombo1.UsName = Nothing
        '
        'GroupsCombo1
        '
        resources.ApplyResources(Me.GroupsCombo1, "GroupsCombo1")
        Me.GroupsCombo1.GroupID = 0
        Me.GroupsCombo1.GroupName = Nothing
        Me.GroupsCombo1.Name = "GroupsCombo1"
        '
        'PermissionsCombo1
        '
        resources.ApplyResources(Me.PermissionsCombo1, "PermissionsCombo1")
        Me.PermissionsCombo1.Name = "PermissionsCombo1"
        Me.PermissionsCombo1.PermID = 0
        Me.PermissionsCombo1.PermName = Nothing
        '
        'FrmPermissionsMngr
        '
        resources.ApplyResources(Me, "$this")
        Me.Appearance.BackColor = System.Drawing.Color.WhiteSmoke
        Me.Appearance.Options.UseBackColor = True
        Me.Controls.Add(Me.SplitContainerControl1)
        Me.Controls.Add(Me.PanelTop)
        Me.Name = "FrmPermissionsMngr"
        CType(Me.SplitContainerControl1.Panel1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainerControl1.Panel1.ResumeLayout(False)
        CType(Me.SplitContainerControl1.Panel2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainerControl1.Panel2.ResumeLayout(False)
        CType(Me.SplitContainerControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainerControl1.ResumeLayout(False)
        CType(Me.grpSlctdUser_GRP, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpSlctdUser_GRP.ResumeLayout(False)
        CType(Me.GridSlctdUser_Grp, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridViewUserGrp, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl1.ResumeLayout(False)
        CType(Me.grpPermissions, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpPermissions.ResumeLayout(False)
        CType(Me.GridPermissions, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridViewPermissions, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl1.ResumeLayout(False)
        CType(Me.btnShowGrpUsr.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PanelTop, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelTop.ResumeLayout(False)
        Me.PanelTop.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents PanelTop As DevExpress.XtraEditors.PanelControl
    Friend WithEvents USERSCombo1 As DentistX.USERSCombo
    Friend WithEvents GroupsCombo1 As DentistX.GroupsCombo
    Friend WithEvents PermissionsCombo1 As DentistX.PermissionsCombo
    Friend WithEvents BtnSave As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents SplitContainerControl1 As DevExpress.XtraEditors.SplitContainerControl
    Friend WithEvents GridSlctdUser_Grp As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridViewUserGrp As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridPermissions As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridViewPermissions As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents grpSlctdUser_GRP As DevExpress.XtraEditors.GroupControl
    Friend WithEvents grpPermissions As DevExpress.XtraEditors.GroupControl
    Friend WithEvents PanelControl1 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents GroupControl1 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents btnShowGrpUsr As DevExpress.XtraEditors.ToggleSwitch
    Friend WithEvents LabelControl3 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
End Class
