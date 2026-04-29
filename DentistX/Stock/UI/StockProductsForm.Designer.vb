<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class StockProductsForm
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

    Friend WithEvents _grid As DevExpress.XtraGrid.GridControl
    Friend WithEvents _view As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents _btnAdd As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents _btnEdit As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents _btnDelete As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents _btnCategories As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents _btnUnits As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents _btnRefresh As DevExpress.XtraEditors.SimpleButton

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(StockProductsForm))
        Me.panelTop = New DevExpress.XtraEditors.PanelControl()
        Me._btnAdd = New DevExpress.XtraEditors.SimpleButton()
        Me._btnEdit = New DevExpress.XtraEditors.SimpleButton()
        Me._btnDelete = New DevExpress.XtraEditors.SimpleButton()
        Me._btnCategories = New DevExpress.XtraEditors.SimpleButton()
        Me._btnUnits = New DevExpress.XtraEditors.SimpleButton()
        Me._btnRefresh = New DevExpress.XtraEditors.SimpleButton()
        Me._grid = New DevExpress.XtraGrid.GridControl()
        Me._view = New DevExpress.XtraGrid.Views.Grid.GridView()
        CType(Me.panelTop, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.panelTop.SuspendLayout()
        CType(Me._grid, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me._view, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'panelTop
        '
        resources.ApplyResources(Me.panelTop, "panelTop")
        Me.panelTop.Controls.Add(Me._btnAdd)
        Me.panelTop.Controls.Add(Me._btnEdit)
        Me.panelTop.Controls.Add(Me._btnDelete)
        Me.panelTop.Controls.Add(Me._btnCategories)
        Me.panelTop.Controls.Add(Me._btnUnits)
        Me.panelTop.Controls.Add(Me._btnRefresh)
        Me.panelTop.Name = "panelTop"
        '
        '_btnAdd
        '
        resources.ApplyResources(Me._btnAdd, "_btnAdd")
        Me._btnAdd.Appearance.Font = CType(resources.GetObject("_btnAdd.Appearance.Font"), System.Drawing.Font)
        Me._btnAdd.Appearance.Options.UseFont = True
        Me._btnAdd.ImageOptions.ImageKey = resources.GetString("_btnAdd.ImageOptions.ImageKey")
        Me._btnAdd.Name = "_btnAdd"
        '
        '_btnEdit
        '
        resources.ApplyResources(Me._btnEdit, "_btnEdit")
        Me._btnEdit.Appearance.Font = CType(resources.GetObject("_btnEdit.Appearance.Font"), System.Drawing.Font)
        Me._btnEdit.Appearance.Options.UseFont = True
        Me._btnEdit.ImageOptions.ImageKey = resources.GetString("_btnEdit.ImageOptions.ImageKey")
        Me._btnEdit.Name = "_btnEdit"
        '
        '_btnDelete
        '
        resources.ApplyResources(Me._btnDelete, "_btnDelete")
        Me._btnDelete.Appearance.Font = CType(resources.GetObject("_btnDelete.Appearance.Font"), System.Drawing.Font)
        Me._btnDelete.Appearance.Options.UseFont = True
        Me._btnDelete.ImageOptions.ImageKey = resources.GetString("_btnDelete.ImageOptions.ImageKey")
        Me._btnDelete.Name = "_btnDelete"
        '
        '_btnCategories
        '
        resources.ApplyResources(Me._btnCategories, "_btnCategories")
        Me._btnCategories.Appearance.Font = CType(resources.GetObject("_btnCategories.Appearance.Font"), System.Drawing.Font)
        Me._btnCategories.Appearance.Options.UseFont = True
        Me._btnCategories.ImageOptions.ImageKey = resources.GetString("_btnCategories.ImageOptions.ImageKey")
        Me._btnCategories.Name = "_btnCategories"
        '
        '_btnUnits
        '
        resources.ApplyResources(Me._btnUnits, "_btnUnits")
        Me._btnUnits.Appearance.Font = CType(resources.GetObject("_btnUnits.Appearance.Font"), System.Drawing.Font)
        Me._btnUnits.Appearance.Options.UseFont = True
        Me._btnUnits.ImageOptions.ImageKey = resources.GetString("_btnUnits.ImageOptions.ImageKey")
        Me._btnUnits.Name = "_btnUnits"
        '
        '_btnRefresh
        '
        resources.ApplyResources(Me._btnRefresh, "_btnRefresh")
        Me._btnRefresh.Appearance.Font = CType(resources.GetObject("_btnRefresh.Appearance.Font"), System.Drawing.Font)
        Me._btnRefresh.Appearance.Options.UseFont = True
        Me._btnRefresh.ImageOptions.ImageKey = resources.GetString("_btnRefresh.ImageOptions.ImageKey")
        Me._btnRefresh.Name = "_btnRefresh"
        '
        '_grid
        '
        resources.ApplyResources(Me._grid, "_grid")
        Me._grid.EmbeddedNavigator.AccessibleDescription = resources.GetString("_grid.EmbeddedNavigator.AccessibleDescription")
        Me._grid.EmbeddedNavigator.AccessibleName = resources.GetString("_grid.EmbeddedNavigator.AccessibleName")
        Me._grid.EmbeddedNavigator.AllowHtmlTextInToolTip = CType(resources.GetObject("_grid.EmbeddedNavigator.AllowHtmlTextInToolTip"), DevExpress.Utils.DefaultBoolean)
        Me._grid.EmbeddedNavigator.Anchor = CType(resources.GetObject("_grid.EmbeddedNavigator.Anchor"), System.Windows.Forms.AnchorStyles)
        Me._grid.EmbeddedNavigator.AutoSize = CType(resources.GetObject("_grid.EmbeddedNavigator.AutoSize"), Boolean)
        Me._grid.EmbeddedNavigator.BackgroundImage = CType(resources.GetObject("_grid.EmbeddedNavigator.BackgroundImage"), System.Drawing.Image)
        Me._grid.EmbeddedNavigator.BackgroundImageLayout = CType(resources.GetObject("_grid.EmbeddedNavigator.BackgroundImageLayout"), System.Windows.Forms.ImageLayout)
        Me._grid.EmbeddedNavigator.ImeMode = CType(resources.GetObject("_grid.EmbeddedNavigator.ImeMode"), System.Windows.Forms.ImeMode)
        Me._grid.EmbeddedNavigator.Margin = CType(resources.GetObject("_grid.EmbeddedNavigator.Margin"), System.Windows.Forms.Padding)
        Me._grid.EmbeddedNavigator.MaximumSize = CType(resources.GetObject("_grid.EmbeddedNavigator.MaximumSize"), System.Drawing.Size)
        Me._grid.EmbeddedNavigator.TextLocation = CType(resources.GetObject("_grid.EmbeddedNavigator.TextLocation"), DevExpress.XtraEditors.NavigatorButtonsTextLocation)
        Me._grid.EmbeddedNavigator.ToolTip = resources.GetString("_grid.EmbeddedNavigator.ToolTip")
        Me._grid.EmbeddedNavigator.ToolTipIconType = CType(resources.GetObject("_grid.EmbeddedNavigator.ToolTipIconType"), DevExpress.Utils.ToolTipIconType)
        Me._grid.EmbeddedNavigator.ToolTipTitle = resources.GetString("_grid.EmbeddedNavigator.ToolTipTitle")
        Me._grid.MainView = Me._view
        Me._grid.Name = "_grid"
        Me._grid.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me._view})
        '
        '_view
        '
        resources.ApplyResources(Me._view, "_view")
        Me._view.Appearance.HeaderPanel.Font = CType(resources.GetObject("_view.Appearance.HeaderPanel.Font"), System.Drawing.Font)
        Me._view.Appearance.HeaderPanel.Options.UseFont = True
        Me._view.Appearance.Row.Font = CType(resources.GetObject("_view.Appearance.Row.Font"), System.Drawing.Font)
        Me._view.Appearance.Row.Options.UseFont = True
        Me._view.DetailHeight = 404
        Me._view.GridControl = Me._grid
        Me._view.Name = "_view"
        Me._view.OptionsEditForm.PopupEditFormWidth = 933
        '
        'StockProductsForm
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me._grid)
        Me.Controls.Add(Me.panelTop)
        Me.Name = "StockProductsForm"
        CType(Me.panelTop, System.ComponentModel.ISupportInitialize).EndInit()
        Me.panelTop.ResumeLayout(False)
        CType(Me._grid, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me._view, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents panelTop As DevExpress.XtraEditors.PanelControl
End Class

