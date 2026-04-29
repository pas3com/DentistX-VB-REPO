Imports System.ComponentModel
Imports DevExpress.XtraEditors
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Views.Grid

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class StockSuppliersForm
    Inherits XtraForm

    'Form overrides dispose to clean up the component list.
    <DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(StockSuppliersForm))
        Me.panelTop = New DevExpress.XtraEditors.PanelControl()
        Me._btnAdd = New DevExpress.XtraEditors.SimpleButton()
        Me._btnEdit = New DevExpress.XtraEditors.SimpleButton()
        Me._btnDelete = New DevExpress.XtraEditors.SimpleButton()
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
        'StockSuppliersForm
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me._grid)
        Me.Controls.Add(Me.panelTop)
        Me.Name = "StockSuppliersForm"
        CType(Me.panelTop, System.ComponentModel.ISupportInitialize).EndInit()
        Me.panelTop.ResumeLayout(False)
        CType(Me._grid, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me._view, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents _grid As GridControl
    Friend WithEvents _view As GridView
    Friend WithEvents _btnAdd As SimpleButton
    Friend WithEvents _btnEdit As SimpleButton
    Friend WithEvents _btnDelete As SimpleButton
    Friend WithEvents _btnRefresh As SimpleButton
    Friend WithEvents panelTop As PanelControl
End Class
