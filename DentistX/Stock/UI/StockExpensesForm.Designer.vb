<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class StockExpensesForm
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
    Friend WithEvents _dateFrom As DevExpress.XtraEditors.DateEdit
    Friend WithEvents _dateTo As DevExpress.XtraEditors.DateEdit
    Friend WithEvents _btnAdd As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents _btnEdit As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents _btnDelete As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents _btnCategories As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents _btnRefresh As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents lblCategory As DevExpress.XtraEditors.LabelControl
    Friend WithEvents _cmbCategory As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents lblSupplier As DevExpress.XtraEditors.LabelControl
    Friend WithEvents _cmbSupplier As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents lblAmount As DevExpress.XtraEditors.LabelControl
    Friend WithEvents _spinAmount As DevExpress.XtraEditors.TextEdit

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(StockExpensesForm))
        Me.panelTop = New DevExpress.XtraEditors.PanelControl()
        Me.lblCategory = New DevExpress.XtraEditors.LabelControl()
        Me._cmbCategory = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.lblSupplier = New DevExpress.XtraEditors.LabelControl()
        Me._cmbSupplier = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.lblAmount = New DevExpress.XtraEditors.LabelControl()
        Me._spinAmount = New DevExpress.XtraEditors.TextEdit()
        Me.lblFrom = New DevExpress.XtraEditors.LabelControl()
        Me._dateFrom = New DevExpress.XtraEditors.DateEdit()
        Me.lblTo = New DevExpress.XtraEditors.LabelControl()
        Me._dateTo = New DevExpress.XtraEditors.DateEdit()
        Me._btnRefresh = New DevExpress.XtraEditors.SimpleButton()
        Me._btnAdd = New DevExpress.XtraEditors.SimpleButton()
        Me._btnEdit = New DevExpress.XtraEditors.SimpleButton()
        Me._btnDelete = New DevExpress.XtraEditors.SimpleButton()
        Me.btnSend = New DevExpress.XtraEditors.SimpleButton()
        Me._btnCategories = New DevExpress.XtraEditors.SimpleButton()
        Me._grid = New DevExpress.XtraGrid.GridControl()
        Me._view = New DevExpress.XtraGrid.Views.Grid.GridView()
        CType(Me.panelTop, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.panelTop.SuspendLayout()
        CType(Me._cmbCategory.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me._cmbSupplier.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me._spinAmount.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me._dateFrom.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me._dateFrom.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me._dateTo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me._dateTo.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me._grid, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me._view, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'panelTop
        '
        resources.ApplyResources(Me.panelTop, "panelTop")
        Me.panelTop.Controls.Add(Me.lblCategory)
        Me.panelTop.Controls.Add(Me._cmbCategory)
        Me.panelTop.Controls.Add(Me.lblSupplier)
        Me.panelTop.Controls.Add(Me._cmbSupplier)
        Me.panelTop.Controls.Add(Me.lblAmount)
        Me.panelTop.Controls.Add(Me._spinAmount)
        Me.panelTop.Controls.Add(Me.lblFrom)
        Me.panelTop.Controls.Add(Me._dateFrom)
        Me.panelTop.Controls.Add(Me.lblTo)
        Me.panelTop.Controls.Add(Me._dateTo)
        Me.panelTop.Controls.Add(Me._btnRefresh)
        Me.panelTop.Controls.Add(Me._btnAdd)
        Me.panelTop.Controls.Add(Me._btnEdit)
        Me.panelTop.Controls.Add(Me._btnDelete)
        Me.panelTop.Controls.Add(Me.btnSend)
        Me.panelTop.Controls.Add(Me._btnCategories)
        Me.panelTop.Name = "panelTop"
        '
        'lblCategory
        '
        resources.ApplyResources(Me.lblCategory, "lblCategory")
        Me.lblCategory.Appearance.Font = CType(resources.GetObject("lblCategory.Appearance.Font"), System.Drawing.Font)
        Me.lblCategory.Appearance.Options.UseFont = True
        Me.lblCategory.Name = "lblCategory"
        '
        '_cmbCategory
        '
        resources.ApplyResources(Me._cmbCategory, "_cmbCategory")
        Me._cmbCategory.Name = "_cmbCategory"
        Me._cmbCategory.Properties.Appearance.Font = CType(resources.GetObject("_cmbCategory.Properties.Appearance.Font"), System.Drawing.Font)
        Me._cmbCategory.Properties.Appearance.Options.UseFont = True
        Me._cmbCategory.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("_cmbCategory.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        '
        'lblSupplier
        '
        resources.ApplyResources(Me.lblSupplier, "lblSupplier")
        Me.lblSupplier.Appearance.Font = CType(resources.GetObject("lblSupplier.Appearance.Font"), System.Drawing.Font)
        Me.lblSupplier.Appearance.Options.UseFont = True
        Me.lblSupplier.Name = "lblSupplier"
        '
        '_cmbSupplier
        '
        resources.ApplyResources(Me._cmbSupplier, "_cmbSupplier")
        Me._cmbSupplier.Name = "_cmbSupplier"
        Me._cmbSupplier.Properties.Appearance.Font = CType(resources.GetObject("_cmbSupplier.Properties.Appearance.Font"), System.Drawing.Font)
        Me._cmbSupplier.Properties.Appearance.Options.UseFont = True
        Me._cmbSupplier.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("_cmbSupplier.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        '
        'lblAmount
        '
        resources.ApplyResources(Me.lblAmount, "lblAmount")
        Me.lblAmount.Appearance.Font = CType(resources.GetObject("lblAmount.Appearance.Font"), System.Drawing.Font)
        Me.lblAmount.Appearance.Options.UseFont = True
        Me.lblAmount.Name = "lblAmount"
        '
        '_spinAmount
        '
        resources.ApplyResources(Me._spinAmount, "_spinAmount")
        Me._spinAmount.Name = "_spinAmount"
        Me._spinAmount.Properties.Appearance.Font = CType(resources.GetObject("_spinAmount.Properties.Appearance.Font"), System.Drawing.Font)
        Me._spinAmount.Properties.Appearance.Options.UseFont = True
        '
        'lblFrom
        '
        resources.ApplyResources(Me.lblFrom, "lblFrom")
        Me.lblFrom.Appearance.Font = CType(resources.GetObject("lblFrom.Appearance.Font"), System.Drawing.Font)
        Me.lblFrom.Appearance.Options.UseFont = True
        Me.lblFrom.Name = "lblFrom"
        '
        '_dateFrom
        '
        resources.ApplyResources(Me._dateFrom, "_dateFrom")
        Me._dateFrom.Name = "_dateFrom"
        Me._dateFrom.Properties.Appearance.Font = CType(resources.GetObject("_dateFrom.Properties.Appearance.Font"), System.Drawing.Font)
        Me._dateFrom.Properties.Appearance.Options.UseFont = True
        Me._dateFrom.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("_dateFrom.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me._dateFrom.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("_dateFrom.Properties.CalendarTimeProperties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        '
        'lblTo
        '
        resources.ApplyResources(Me.lblTo, "lblTo")
        Me.lblTo.Appearance.Font = CType(resources.GetObject("lblTo.Appearance.Font"), System.Drawing.Font)
        Me.lblTo.Appearance.Options.UseFont = True
        Me.lblTo.Name = "lblTo"
        '
        '_dateTo
        '
        resources.ApplyResources(Me._dateTo, "_dateTo")
        Me._dateTo.Name = "_dateTo"
        Me._dateTo.Properties.Appearance.Font = CType(resources.GetObject("_dateTo.Properties.Appearance.Font"), System.Drawing.Font)
        Me._dateTo.Properties.Appearance.Options.UseFont = True
        Me._dateTo.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("_dateTo.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me._dateTo.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("_dateTo.Properties.CalendarTimeProperties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        '
        '_btnRefresh
        '
        resources.ApplyResources(Me._btnRefresh, "_btnRefresh")
        Me._btnRefresh.Appearance.Font = CType(resources.GetObject("_btnRefresh.Appearance.Font"), System.Drawing.Font)
        Me._btnRefresh.Appearance.Options.UseFont = True
        Me._btnRefresh.ImageOptions.ImageKey = resources.GetString("_btnRefresh.ImageOptions.ImageKey")
        Me._btnRefresh.Name = "_btnRefresh"
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
        'btnSend
        '
        resources.ApplyResources(Me.btnSend, "btnSend")
        Me.btnSend.Appearance.Font = CType(resources.GetObject("btnSend.Appearance.Font"), System.Drawing.Font)
        Me.btnSend.Appearance.Options.UseFont = True
        Me.btnSend.ImageOptions.ImageKey = resources.GetString("btnSend.ImageOptions.ImageKey")
        Me.btnSend.Name = "btnSend"
        '
        '_btnCategories
        '
        resources.ApplyResources(Me._btnCategories, "_btnCategories")
        Me._btnCategories.Appearance.Font = CType(resources.GetObject("_btnCategories.Appearance.Font"), System.Drawing.Font)
        Me._btnCategories.Appearance.Options.UseFont = True
        Me._btnCategories.ImageOptions.ImageKey = resources.GetString("_btnCategories.ImageOptions.ImageKey")
        Me._btnCategories.Name = "_btnCategories"
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
        'StockExpensesForm
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me._grid)
        Me.Controls.Add(Me.panelTop)
        Me.Name = "StockExpensesForm"
        CType(Me.panelTop, System.ComponentModel.ISupportInitialize).EndInit()
        Me.panelTop.ResumeLayout(False)
        Me.panelTop.PerformLayout()
        CType(Me._cmbCategory.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me._cmbSupplier.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me._spinAmount.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me._dateFrom.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me._dateFrom.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me._dateTo.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me._dateTo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me._grid, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me._view, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents panelTop As DevExpress.XtraEditors.PanelControl
    Friend WithEvents lblFrom As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblTo As DevExpress.XtraEditors.LabelControl
    Friend WithEvents btnSend As DevExpress.XtraEditors.SimpleButton
End Class

