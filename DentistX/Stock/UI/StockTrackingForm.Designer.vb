<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class StockTrackingForm
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

    Friend WithEvents _tabs As DevExpress.XtraTab.XtraTabControl
    Friend WithEvents _gridAll As DevExpress.XtraGrid.GridControl
    Friend WithEvents _viewAll As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents _gridLow As DevExpress.XtraGrid.GridControl
    Friend WithEvents _viewLow As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents _gridExp As DevExpress.XtraGrid.GridControl
    Friend WithEvents _viewExp As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents _spinExpDays As DevExpress.XtraEditors.TextEdit
    Friend WithEvents _btnRefresh As DevExpress.XtraEditors.SimpleButton

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(StockTrackingForm))
        Me.panelTop = New DevExpress.XtraEditors.PanelControl()
        Me.lblExpiring = New DevExpress.XtraEditors.LabelControl()
        Me._spinExpDays = New DevExpress.XtraEditors.TextEdit()
        Me._btnRefresh = New DevExpress.XtraEditors.SimpleButton()
        Me.tabAll = New DevExpress.XtraTab.XtraTabPage()
        Me._gridAll = New DevExpress.XtraGrid.GridControl()
        Me._viewAll = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.tabLow = New DevExpress.XtraTab.XtraTabPage()
        Me._gridLow = New DevExpress.XtraGrid.GridControl()
        Me._viewLow = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.tabExp = New DevExpress.XtraTab.XtraTabPage()
        Me._gridExp = New DevExpress.XtraGrid.GridControl()
        Me._viewExp = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me._tabs = New DevExpress.XtraTab.XtraTabControl()
        CType(Me.panelTop, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.panelTop.SuspendLayout()
        CType(Me._spinExpDays.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tabAll.SuspendLayout()
        CType(Me._gridAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me._viewAll, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tabLow.SuspendLayout()
        CType(Me._gridLow, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me._viewLow, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tabExp.SuspendLayout()
        CType(Me._gridExp, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me._viewExp, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me._tabs, System.ComponentModel.ISupportInitialize).BeginInit()
        Me._tabs.SuspendLayout()
        Me.SuspendLayout()
        '
        'panelTop
        '
        resources.ApplyResources(Me.panelTop, "panelTop")
        Me.panelTop.Controls.Add(Me.lblExpiring)
        Me.panelTop.Controls.Add(Me._spinExpDays)
        Me.panelTop.Controls.Add(Me._btnRefresh)
        Me.panelTop.Name = "panelTop"
        '
        'lblExpiring
        '
        resources.ApplyResources(Me.lblExpiring, "lblExpiring")
        Me.lblExpiring.Appearance.Font = CType(resources.GetObject("lblExpiring.Appearance.Font"), System.Drawing.Font)
        Me.lblExpiring.Appearance.Options.UseFont = True
        Me.lblExpiring.Name = "lblExpiring"
        '
        '_spinExpDays
        '
        resources.ApplyResources(Me._spinExpDays, "_spinExpDays")
        Me._spinExpDays.Name = "_spinExpDays"
        Me._spinExpDays.Properties.Appearance.Font = CType(resources.GetObject("_spinExpDays.Properties.Appearance.Font"), System.Drawing.Font)
        Me._spinExpDays.Properties.Appearance.Options.UseFont = True
        '
        '_btnRefresh
        '
        resources.ApplyResources(Me._btnRefresh, "_btnRefresh")
        Me._btnRefresh.Appearance.Font = CType(resources.GetObject("_btnRefresh.Appearance.Font"), System.Drawing.Font)
        Me._btnRefresh.Appearance.Options.UseFont = True
        Me._btnRefresh.ImageOptions.ImageKey = resources.GetString("_btnRefresh.ImageOptions.ImageKey")
        Me._btnRefresh.Name = "_btnRefresh"
        '
        'tabAll
        '
        resources.ApplyResources(Me.tabAll, "tabAll")
        Me.tabAll.Controls.Add(Me._gridAll)
        Me.tabAll.Name = "tabAll"
        '
        '_gridAll
        '
        resources.ApplyResources(Me._gridAll, "_gridAll")
        Me._gridAll.EmbeddedNavigator.AccessibleDescription = resources.GetString("_gridAll.EmbeddedNavigator.AccessibleDescription")
        Me._gridAll.EmbeddedNavigator.AccessibleName = resources.GetString("_gridAll.EmbeddedNavigator.AccessibleName")
        Me._gridAll.EmbeddedNavigator.AllowHtmlTextInToolTip = CType(resources.GetObject("_gridAll.EmbeddedNavigator.AllowHtmlTextInToolTip"), DevExpress.Utils.DefaultBoolean)
        Me._gridAll.EmbeddedNavigator.Anchor = CType(resources.GetObject("_gridAll.EmbeddedNavigator.Anchor"), System.Windows.Forms.AnchorStyles)
        Me._gridAll.EmbeddedNavigator.AutoSize = CType(resources.GetObject("_gridAll.EmbeddedNavigator.AutoSize"), Boolean)
        Me._gridAll.EmbeddedNavigator.BackgroundImage = CType(resources.GetObject("_gridAll.EmbeddedNavigator.BackgroundImage"), System.Drawing.Image)
        Me._gridAll.EmbeddedNavigator.BackgroundImageLayout = CType(resources.GetObject("_gridAll.EmbeddedNavigator.BackgroundImageLayout"), System.Windows.Forms.ImageLayout)
        Me._gridAll.EmbeddedNavigator.ImeMode = CType(resources.GetObject("_gridAll.EmbeddedNavigator.ImeMode"), System.Windows.Forms.ImeMode)
        Me._gridAll.EmbeddedNavigator.Margin = CType(resources.GetObject("_gridAll.EmbeddedNavigator.Margin"), System.Windows.Forms.Padding)
        Me._gridAll.EmbeddedNavigator.MaximumSize = CType(resources.GetObject("_gridAll.EmbeddedNavigator.MaximumSize"), System.Drawing.Size)
        Me._gridAll.EmbeddedNavigator.TextLocation = CType(resources.GetObject("_gridAll.EmbeddedNavigator.TextLocation"), DevExpress.XtraEditors.NavigatorButtonsTextLocation)
        Me._gridAll.EmbeddedNavigator.ToolTip = resources.GetString("_gridAll.EmbeddedNavigator.ToolTip")
        Me._gridAll.EmbeddedNavigator.ToolTipIconType = CType(resources.GetObject("_gridAll.EmbeddedNavigator.ToolTipIconType"), DevExpress.Utils.ToolTipIconType)
        Me._gridAll.EmbeddedNavigator.ToolTipTitle = resources.GetString("_gridAll.EmbeddedNavigator.ToolTipTitle")
        Me._gridAll.MainView = Me._viewAll
        Me._gridAll.Name = "_gridAll"
        Me._gridAll.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me._viewAll})
        '
        '_viewAll
        '
        resources.ApplyResources(Me._viewAll, "_viewAll")
        Me._viewAll.Appearance.HeaderPanel.Font = CType(resources.GetObject("_viewAll.Appearance.HeaderPanel.Font"), System.Drawing.Font)
        Me._viewAll.Appearance.HeaderPanel.Options.UseFont = True
        Me._viewAll.Appearance.Row.Font = CType(resources.GetObject("_viewAll.Appearance.Row.Font"), System.Drawing.Font)
        Me._viewAll.Appearance.Row.Options.UseFont = True
        Me._viewAll.DetailHeight = 404
        Me._viewAll.GridControl = Me._gridAll
        Me._viewAll.Name = "_viewAll"
        Me._viewAll.OptionsEditForm.PopupEditFormWidth = 933
        '
        'tabLow
        '
        resources.ApplyResources(Me.tabLow, "tabLow")
        Me.tabLow.Controls.Add(Me._gridLow)
        Me.tabLow.Name = "tabLow"
        '
        '_gridLow
        '
        resources.ApplyResources(Me._gridLow, "_gridLow")
        Me._gridLow.EmbeddedNavigator.AccessibleDescription = resources.GetString("_gridLow.EmbeddedNavigator.AccessibleDescription")
        Me._gridLow.EmbeddedNavigator.AccessibleName = resources.GetString("_gridLow.EmbeddedNavigator.AccessibleName")
        Me._gridLow.EmbeddedNavigator.AllowHtmlTextInToolTip = CType(resources.GetObject("_gridLow.EmbeddedNavigator.AllowHtmlTextInToolTip"), DevExpress.Utils.DefaultBoolean)
        Me._gridLow.EmbeddedNavigator.Anchor = CType(resources.GetObject("_gridLow.EmbeddedNavigator.Anchor"), System.Windows.Forms.AnchorStyles)
        Me._gridLow.EmbeddedNavigator.AutoSize = CType(resources.GetObject("_gridLow.EmbeddedNavigator.AutoSize"), Boolean)
        Me._gridLow.EmbeddedNavigator.BackgroundImage = CType(resources.GetObject("_gridLow.EmbeddedNavigator.BackgroundImage"), System.Drawing.Image)
        Me._gridLow.EmbeddedNavigator.BackgroundImageLayout = CType(resources.GetObject("_gridLow.EmbeddedNavigator.BackgroundImageLayout"), System.Windows.Forms.ImageLayout)
        Me._gridLow.EmbeddedNavigator.ImeMode = CType(resources.GetObject("_gridLow.EmbeddedNavigator.ImeMode"), System.Windows.Forms.ImeMode)
        Me._gridLow.EmbeddedNavigator.Margin = CType(resources.GetObject("_gridLow.EmbeddedNavigator.Margin"), System.Windows.Forms.Padding)
        Me._gridLow.EmbeddedNavigator.MaximumSize = CType(resources.GetObject("_gridLow.EmbeddedNavigator.MaximumSize"), System.Drawing.Size)
        Me._gridLow.EmbeddedNavigator.TextLocation = CType(resources.GetObject("_gridLow.EmbeddedNavigator.TextLocation"), DevExpress.XtraEditors.NavigatorButtonsTextLocation)
        Me._gridLow.EmbeddedNavigator.ToolTip = resources.GetString("_gridLow.EmbeddedNavigator.ToolTip")
        Me._gridLow.EmbeddedNavigator.ToolTipIconType = CType(resources.GetObject("_gridLow.EmbeddedNavigator.ToolTipIconType"), DevExpress.Utils.ToolTipIconType)
        Me._gridLow.EmbeddedNavigator.ToolTipTitle = resources.GetString("_gridLow.EmbeddedNavigator.ToolTipTitle")
        Me._gridLow.MainView = Me._viewLow
        Me._gridLow.Name = "_gridLow"
        Me._gridLow.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me._viewLow})
        '
        '_viewLow
        '
        resources.ApplyResources(Me._viewLow, "_viewLow")
        Me._viewLow.Appearance.HeaderPanel.Font = CType(resources.GetObject("_viewLow.Appearance.HeaderPanel.Font"), System.Drawing.Font)
        Me._viewLow.Appearance.HeaderPanel.Options.UseFont = True
        Me._viewLow.Appearance.Row.Font = CType(resources.GetObject("_viewLow.Appearance.Row.Font"), System.Drawing.Font)
        Me._viewLow.Appearance.Row.Options.UseFont = True
        Me._viewLow.DetailHeight = 404
        Me._viewLow.GridControl = Me._gridLow
        Me._viewLow.Name = "_viewLow"
        Me._viewLow.OptionsEditForm.PopupEditFormWidth = 933
        '
        'tabExp
        '
        resources.ApplyResources(Me.tabExp, "tabExp")
        Me.tabExp.Controls.Add(Me._gridExp)
        Me.tabExp.Name = "tabExp"
        '
        '_gridExp
        '
        resources.ApplyResources(Me._gridExp, "_gridExp")
        Me._gridExp.EmbeddedNavigator.AccessibleDescription = resources.GetString("_gridExp.EmbeddedNavigator.AccessibleDescription")
        Me._gridExp.EmbeddedNavigator.AccessibleName = resources.GetString("_gridExp.EmbeddedNavigator.AccessibleName")
        Me._gridExp.EmbeddedNavigator.AllowHtmlTextInToolTip = CType(resources.GetObject("_gridExp.EmbeddedNavigator.AllowHtmlTextInToolTip"), DevExpress.Utils.DefaultBoolean)
        Me._gridExp.EmbeddedNavigator.Anchor = CType(resources.GetObject("_gridExp.EmbeddedNavigator.Anchor"), System.Windows.Forms.AnchorStyles)
        Me._gridExp.EmbeddedNavigator.AutoSize = CType(resources.GetObject("_gridExp.EmbeddedNavigator.AutoSize"), Boolean)
        Me._gridExp.EmbeddedNavigator.BackgroundImage = CType(resources.GetObject("_gridExp.EmbeddedNavigator.BackgroundImage"), System.Drawing.Image)
        Me._gridExp.EmbeddedNavigator.BackgroundImageLayout = CType(resources.GetObject("_gridExp.EmbeddedNavigator.BackgroundImageLayout"), System.Windows.Forms.ImageLayout)
        Me._gridExp.EmbeddedNavigator.ImeMode = CType(resources.GetObject("_gridExp.EmbeddedNavigator.ImeMode"), System.Windows.Forms.ImeMode)
        Me._gridExp.EmbeddedNavigator.Margin = CType(resources.GetObject("_gridExp.EmbeddedNavigator.Margin"), System.Windows.Forms.Padding)
        Me._gridExp.EmbeddedNavigator.MaximumSize = CType(resources.GetObject("_gridExp.EmbeddedNavigator.MaximumSize"), System.Drawing.Size)
        Me._gridExp.EmbeddedNavigator.TextLocation = CType(resources.GetObject("_gridExp.EmbeddedNavigator.TextLocation"), DevExpress.XtraEditors.NavigatorButtonsTextLocation)
        Me._gridExp.EmbeddedNavigator.ToolTip = resources.GetString("_gridExp.EmbeddedNavigator.ToolTip")
        Me._gridExp.EmbeddedNavigator.ToolTipIconType = CType(resources.GetObject("_gridExp.EmbeddedNavigator.ToolTipIconType"), DevExpress.Utils.ToolTipIconType)
        Me._gridExp.EmbeddedNavigator.ToolTipTitle = resources.GetString("_gridExp.EmbeddedNavigator.ToolTipTitle")
        Me._gridExp.MainView = Me._viewExp
        Me._gridExp.Name = "_gridExp"
        Me._gridExp.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me._viewExp})
        '
        '_viewExp
        '
        resources.ApplyResources(Me._viewExp, "_viewExp")
        Me._viewExp.Appearance.HeaderPanel.Font = CType(resources.GetObject("_viewExp.Appearance.HeaderPanel.Font"), System.Drawing.Font)
        Me._viewExp.Appearance.HeaderPanel.Options.UseFont = True
        Me._viewExp.Appearance.Row.Font = CType(resources.GetObject("_viewExp.Appearance.Row.Font"), System.Drawing.Font)
        Me._viewExp.Appearance.Row.Options.UseFont = True
        Me._viewExp.DetailHeight = 404
        Me._viewExp.GridControl = Me._gridExp
        Me._viewExp.Name = "_viewExp"
        Me._viewExp.OptionsEditForm.PopupEditFormWidth = 933
        '
        '_tabs
        '
        resources.ApplyResources(Me._tabs, "_tabs")
        Me._tabs.AppearancePage.Header.Font = CType(resources.GetObject("_tabs.AppearancePage.Header.Font"), System.Drawing.Font)
        Me._tabs.AppearancePage.Header.Options.UseFont = True
        Me._tabs.Name = "_tabs"
        Me._tabs.SelectedTabPage = Me.tabAll
        Me._tabs.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.tabAll, Me.tabLow, Me.tabExp})
        '
        'StockTrackingForm
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me._tabs)
        Me.Controls.Add(Me.panelTop)
        Me.Name = "StockTrackingForm"
        CType(Me.panelTop, System.ComponentModel.ISupportInitialize).EndInit()
        Me.panelTop.ResumeLayout(False)
        Me.panelTop.PerformLayout()
        CType(Me._spinExpDays.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tabAll.ResumeLayout(False)
        CType(Me._gridAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me._viewAll, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tabLow.ResumeLayout(False)
        CType(Me._gridLow, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me._viewLow, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tabExp.ResumeLayout(False)
        CType(Me._gridExp, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me._viewExp, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me._tabs, System.ComponentModel.ISupportInitialize).EndInit()
        Me._tabs.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents panelTop As DevExpress.XtraEditors.PanelControl
    Friend WithEvents lblExpiring As DevExpress.XtraEditors.LabelControl
    Friend WithEvents tabAll As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents tabLow As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents tabExp As DevExpress.XtraTab.XtraTabPage
End Class

