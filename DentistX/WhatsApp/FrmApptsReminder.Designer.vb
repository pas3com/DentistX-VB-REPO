<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmApptsReminder
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

    Friend WithEvents TopInfoPanel As System.Windows.Forms.Panel
    Friend WithEvents LblInfo As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FlowButtonPanel As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents BtnRefresh As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents BtnRunNow As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents GridMain As DevExpress.XtraGrid.GridControl
    Friend WithEvents ViewMain As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents BindingQueue As System.Windows.Forms.BindingSource

    'NOTE: The following procedure is required by the Windows Form Designer
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmApptsReminder))
        Me.TopInfoPanel = New System.Windows.Forms.Panel()
        Me.LblInfo = New DevExpress.XtraEditors.LabelControl()
        Me.FlowButtonPanel = New System.Windows.Forms.FlowLayoutPanel()
        Me.BtnRefresh = New DevExpress.XtraEditors.SimpleButton()
        Me.BtnRunNow = New DevExpress.XtraEditors.SimpleButton()
        Me.GridMain = New DevExpress.XtraGrid.GridControl()
        Me.BindingQueue = New System.Windows.Forms.BindingSource(Me.components)
        Me.ViewMain = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.TopInfoPanel.SuspendLayout()
        Me.FlowButtonPanel.SuspendLayout()
        CType(Me.GridMain, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BindingQueue, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ViewMain, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TopInfoPanel
        '
        resources.ApplyResources(Me.TopInfoPanel, "TopInfoPanel")
        Me.TopInfoPanel.Controls.Add(Me.LblInfo)
        Me.TopInfoPanel.Controls.Add(Me.FlowButtonPanel)
        Me.TopInfoPanel.Name = "TopInfoPanel"
        '
        'LblInfo
        '
        resources.ApplyResources(Me.LblInfo, "LblInfo")
        Me.LblInfo.Appearance.Font = CType(resources.GetObject("LblInfo.Appearance.Font"), System.Drawing.Font)
        Me.LblInfo.Appearance.Options.UseFont = True
        Me.LblInfo.Appearance.Options.UseTextOptions = True
        Me.LblInfo.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
        Me.LblInfo.Name = "LblInfo"
        '
        'FlowButtonPanel
        '
        resources.ApplyResources(Me.FlowButtonPanel, "FlowButtonPanel")
        Me.FlowButtonPanel.Controls.Add(Me.BtnRefresh)
        Me.FlowButtonPanel.Controls.Add(Me.BtnRunNow)
        Me.FlowButtonPanel.Name = "FlowButtonPanel"
        '
        'BtnRefresh
        '
        resources.ApplyResources(Me.BtnRefresh, "BtnRefresh")
        Me.BtnRefresh.Appearance.Font = CType(resources.GetObject("BtnRefresh.Appearance.Font"), System.Drawing.Font)
        Me.BtnRefresh.Appearance.Options.UseFont = True
        Me.BtnRefresh.ImageOptions.ImageKey = resources.GetString("BtnRefresh.ImageOptions.ImageKey")
        Me.BtnRefresh.Name = "BtnRefresh"
        '
        'BtnRunNow
        '
        resources.ApplyResources(Me.BtnRunNow, "BtnRunNow")
        Me.BtnRunNow.Appearance.Font = CType(resources.GetObject("BtnRunNow.Appearance.Font"), System.Drawing.Font)
        Me.BtnRunNow.Appearance.Options.UseFont = True
        Me.BtnRunNow.ImageOptions.ImageKey = resources.GetString("BtnRunNow.ImageOptions.ImageKey")
        Me.BtnRunNow.Name = "BtnRunNow"
        '
        'GridMain
        '
        resources.ApplyResources(Me.GridMain, "GridMain")
        Me.GridMain.DataSource = Me.BindingQueue
        Me.GridMain.EmbeddedNavigator.AccessibleDescription = resources.GetString("GridMain.EmbeddedNavigator.AccessibleDescription")
        Me.GridMain.EmbeddedNavigator.AccessibleName = resources.GetString("GridMain.EmbeddedNavigator.AccessibleName")
        Me.GridMain.EmbeddedNavigator.AllowHtmlTextInToolTip = CType(resources.GetObject("GridMain.EmbeddedNavigator.AllowHtmlTextInToolTip"), DevExpress.Utils.DefaultBoolean)
        Me.GridMain.EmbeddedNavigator.Anchor = CType(resources.GetObject("GridMain.EmbeddedNavigator.Anchor"), System.Windows.Forms.AnchorStyles)
        Me.GridMain.EmbeddedNavigator.AutoSize = CType(resources.GetObject("GridMain.EmbeddedNavigator.AutoSize"), Boolean)
        Me.GridMain.EmbeddedNavigator.BackgroundImage = CType(resources.GetObject("GridMain.EmbeddedNavigator.BackgroundImage"), System.Drawing.Image)
        Me.GridMain.EmbeddedNavigator.BackgroundImageLayout = CType(resources.GetObject("GridMain.EmbeddedNavigator.BackgroundImageLayout"), System.Windows.Forms.ImageLayout)
        Me.GridMain.EmbeddedNavigator.ImeMode = CType(resources.GetObject("GridMain.EmbeddedNavigator.ImeMode"), System.Windows.Forms.ImeMode)
        Me.GridMain.EmbeddedNavigator.MaximumSize = CType(resources.GetObject("GridMain.EmbeddedNavigator.MaximumSize"), System.Drawing.Size)
        Me.GridMain.EmbeddedNavigator.TextLocation = CType(resources.GetObject("GridMain.EmbeddedNavigator.TextLocation"), DevExpress.XtraEditors.NavigatorButtonsTextLocation)
        Me.GridMain.EmbeddedNavigator.ToolTip = resources.GetString("GridMain.EmbeddedNavigator.ToolTip")
        Me.GridMain.EmbeddedNavigator.ToolTipIconType = CType(resources.GetObject("GridMain.EmbeddedNavigator.ToolTipIconType"), DevExpress.Utils.ToolTipIconType)
        Me.GridMain.EmbeddedNavigator.ToolTipTitle = resources.GetString("GridMain.EmbeddedNavigator.ToolTipTitle")
        Me.GridMain.MainView = Me.ViewMain
        Me.GridMain.Name = "GridMain"
        Me.GridMain.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ViewMain})
        '
        'BindingQueue
        '
        Me.BindingQueue.DataSource = GetType(Object)
        '
        'ViewMain
        '
        resources.ApplyResources(Me.ViewMain, "ViewMain")
        Me.ViewMain.Appearance.HeaderPanel.Font = CType(resources.GetObject("ViewMain.Appearance.HeaderPanel.Font"), System.Drawing.Font)
        Me.ViewMain.Appearance.HeaderPanel.Options.UseFont = True
        Me.ViewMain.Appearance.Row.Font = CType(resources.GetObject("ViewMain.Appearance.Row.Font"), System.Drawing.Font)
        Me.ViewMain.Appearance.Row.Options.UseFont = True
        Me.ViewMain.DetailHeight = 303
        Me.ViewMain.GridControl = Me.GridMain
        Me.ViewMain.Name = "ViewMain"
        Me.ViewMain.OptionsBehavior.Editable = False
        Me.ViewMain.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.ViewMain.OptionsView.ShowGroupPanel = False
        Me.ViewMain.RowHeight = 22
        '
        'FrmApptsReminder
        '
        resources.ApplyResources(Me, "$this")
        Me.Appearance.Options.UseFont = True
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.GridMain)
        Me.Controls.Add(Me.TopInfoPanel)
        Me.Name = "FrmApptsReminder"
        Me.TopInfoPanel.ResumeLayout(False)
        Me.FlowButtonPanel.ResumeLayout(False)
        Me.FlowButtonPanel.PerformLayout()
        CType(Me.GridMain, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BindingQueue, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ViewMain, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
End Class
