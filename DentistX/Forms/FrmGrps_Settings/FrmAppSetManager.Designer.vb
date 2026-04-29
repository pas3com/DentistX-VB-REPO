<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmAppSetManager
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmAppSetManager))
        Me.GridControl1 = New DevExpress.XtraGrid.GridControl()
        Me.GridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.BtnAddAppSetting = New DevExpress.XtraEditors.SimpleButton()
        Me.BtnEditAppSetting = New DevExpress.XtraEditors.SimpleButton()
        Me.BtnDelAppSetting = New DevExpress.XtraEditors.SimpleButton()
        Me.TxtAppSettingName = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.TxtAppSettingValue = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        CType(Me.GridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtAppSettingName.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtAppSettingValue.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.GridControl1.EmbeddedNavigator.Margin = CType(resources.GetObject("GridControl1.EmbeddedNavigator.Margin"), System.Windows.Forms.Padding)
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
        Me.GridView1.Appearance.HeaderPanel.Font = CType(resources.GetObject("GridView1.Appearance.HeaderPanel.Font"), System.Drawing.Font)
        Me.GridView1.Appearance.HeaderPanel.Options.UseFont = True
        Me.GridView1.Appearance.Row.Font = CType(resources.GetObject("GridView1.Appearance.Row.Font"), System.Drawing.Font)
        Me.GridView1.Appearance.Row.Options.UseFont = True
        Me.GridView1.DetailHeight = 404
        Me.GridView1.GridControl = Me.GridControl1
        Me.GridView1.Name = "GridView1"
        Me.GridView1.OptionsEditForm.PopupEditFormWidth = 933
        '
        'BtnAddAppSetting
        '
        resources.ApplyResources(Me.BtnAddAppSetting, "BtnAddAppSetting")
        Me.BtnAddAppSetting.Appearance.Font = CType(resources.GetObject("BtnAddAppSetting.Appearance.Font"), System.Drawing.Font)
        Me.BtnAddAppSetting.Appearance.Options.UseFont = True
        Me.BtnAddAppSetting.ImageOptions.ImageKey = resources.GetString("BtnAddAppSetting.ImageOptions.ImageKey")
        Me.BtnAddAppSetting.Name = "BtnAddAppSetting"
        '
        'BtnEditAppSetting
        '
        resources.ApplyResources(Me.BtnEditAppSetting, "BtnEditAppSetting")
        Me.BtnEditAppSetting.Appearance.Font = CType(resources.GetObject("BtnEditAppSetting.Appearance.Font"), System.Drawing.Font)
        Me.BtnEditAppSetting.Appearance.Options.UseFont = True
        Me.BtnEditAppSetting.ImageOptions.ImageKey = resources.GetString("BtnEditAppSetting.ImageOptions.ImageKey")
        Me.BtnEditAppSetting.Name = "BtnEditAppSetting"
        '
        'BtnDelAppSetting
        '
        resources.ApplyResources(Me.BtnDelAppSetting, "BtnDelAppSetting")
        Me.BtnDelAppSetting.Appearance.Font = CType(resources.GetObject("BtnDelAppSetting.Appearance.Font"), System.Drawing.Font)
        Me.BtnDelAppSetting.Appearance.Options.UseFont = True
        Me.BtnDelAppSetting.ImageOptions.ImageKey = resources.GetString("BtnDelAppSetting.ImageOptions.ImageKey")
        Me.BtnDelAppSetting.Name = "BtnDelAppSetting"
        '
        'TxtAppSettingName
        '
        resources.ApplyResources(Me.TxtAppSettingName, "TxtAppSettingName")
        Me.TxtAppSettingName.Name = "TxtAppSettingName"
        Me.TxtAppSettingName.Properties.Appearance.Font = CType(resources.GetObject("TxtAppSettingName.Properties.Appearance.Font"), System.Drawing.Font)
        Me.TxtAppSettingName.Properties.Appearance.Options.UseFont = True
        '
        'LabelControl1
        '
        resources.ApplyResources(Me.LabelControl1, "LabelControl1")
        Me.LabelControl1.Appearance.Font = CType(resources.GetObject("LabelControl1.Appearance.Font"), System.Drawing.Font)
        Me.LabelControl1.Appearance.Options.UseFont = True
        Me.LabelControl1.Name = "LabelControl1"
        '
        'TxtAppSettingValue
        '
        resources.ApplyResources(Me.TxtAppSettingValue, "TxtAppSettingValue")
        Me.TxtAppSettingValue.Name = "TxtAppSettingValue"
        Me.TxtAppSettingValue.Properties.Appearance.Font = CType(resources.GetObject("TxtAppSettingValue.Properties.Appearance.Font"), System.Drawing.Font)
        Me.TxtAppSettingValue.Properties.Appearance.Options.UseFont = True
        '
        'LabelControl2
        '
        resources.ApplyResources(Me.LabelControl2, "LabelControl2")
        Me.LabelControl2.Appearance.Font = CType(resources.GetObject("LabelControl2.Appearance.Font"), System.Drawing.Font)
        Me.LabelControl2.Appearance.Options.UseFont = True
        Me.LabelControl2.Name = "LabelControl2"
        '
        'FrmAppSetManager
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.LabelControl2)
        Me.Controls.Add(Me.LabelControl1)
        Me.Controls.Add(Me.TxtAppSettingValue)
        Me.Controls.Add(Me.TxtAppSettingName)
        Me.Controls.Add(Me.BtnDelAppSetting)
        Me.Controls.Add(Me.BtnEditAppSetting)
        Me.Controls.Add(Me.BtnAddAppSetting)
        Me.Controls.Add(Me.GridControl1)
        Me.Name = "FrmAppSetManager"
        CType(Me.GridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtAppSettingName.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtAppSettingValue.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents GridControl1 As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridView1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents BtnAddAppSetting As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents BtnEditAppSetting As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents BtnDelAppSetting As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents TxtAppSettingName As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents TxtAppSettingValue As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
End Class
