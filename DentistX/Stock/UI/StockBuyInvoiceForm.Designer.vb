<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class StockBuyInvoiceForm
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

    Friend WithEvents _cmbSupplier As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents _dateInvoice As DevExpress.XtraEditors.DateEdit
    Friend WithEvents _dateDue As DevExpress.XtraEditors.DateEdit
    Friend WithEvents _grid As DevExpress.XtraGrid.GridControl
    Friend WithEvents _view As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents _btnAddLine As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents _btnRemoveLine As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents _btnSave As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents _lblTotal As DevExpress.XtraEditors.LabelControl

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(StockBuyInvoiceForm))
        Me.panelTop = New DevExpress.XtraEditors.PanelControl()
        Me.lblSupplier = New DevExpress.XtraEditors.LabelControl()
        Me._cmbSupplier = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.lblInvoiceDate = New DevExpress.XtraEditors.LabelControl()
        Me._dateInvoice = New DevExpress.XtraEditors.DateEdit()
        Me.lblDueDate = New DevExpress.XtraEditors.LabelControl()
        Me._dateDue = New DevExpress.XtraEditors.DateEdit()
        Me._lblTotal = New DevExpress.XtraEditors.LabelControl()
        Me.panelActions = New DevExpress.XtraEditors.PanelControl()
        Me._btnAddLine = New DevExpress.XtraEditors.SimpleButton()
        Me._btnRemoveLine = New DevExpress.XtraEditors.SimpleButton()
        Me._btnSave = New DevExpress.XtraEditors.SimpleButton()
        Me._grid = New DevExpress.XtraGrid.GridControl()
        Me._view = New DevExpress.XtraGrid.Views.Grid.GridView()
        CType(Me.panelTop, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.panelTop.SuspendLayout()
        CType(Me._cmbSupplier.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me._dateInvoice.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me._dateInvoice.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me._dateDue.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me._dateDue.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.panelActions, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.panelActions.SuspendLayout()
        CType(Me._grid, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me._view, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'panelTop
        '
        Me.panelTop.Controls.Add(Me.lblSupplier)
        Me.panelTop.Controls.Add(Me._cmbSupplier)
        Me.panelTop.Controls.Add(Me.lblInvoiceDate)
        Me.panelTop.Controls.Add(Me._dateInvoice)
        Me.panelTop.Controls.Add(Me.lblDueDate)
        Me.panelTop.Controls.Add(Me._dateDue)
        Me.panelTop.Controls.Add(Me._lblTotal)
        resources.ApplyResources(Me.panelTop, "panelTop")
        Me.panelTop.Name = "panelTop"
        '
        'lblSupplier
        '
        Me.lblSupplier.Appearance.Font = CType(resources.GetObject("lblSupplier.Appearance.Font"), System.Drawing.Font)
        Me.lblSupplier.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.lblSupplier, "lblSupplier")
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
        'lblInvoiceDate
        '
        Me.lblInvoiceDate.Appearance.Font = CType(resources.GetObject("lblInvoiceDate.Appearance.Font"), System.Drawing.Font)
        Me.lblInvoiceDate.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.lblInvoiceDate, "lblInvoiceDate")
        Me.lblInvoiceDate.Name = "lblInvoiceDate"
        '
        '_dateInvoice
        '
        resources.ApplyResources(Me._dateInvoice, "_dateInvoice")
        Me._dateInvoice.Name = "_dateInvoice"
        Me._dateInvoice.Properties.Appearance.Font = CType(resources.GetObject("_dateInvoice.Properties.Appearance.Font"), System.Drawing.Font)
        Me._dateInvoice.Properties.Appearance.Options.UseFont = True
        Me._dateInvoice.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("_dateInvoice.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me._dateInvoice.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("_dateInvoice.Properties.CalendarTimeProperties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        '
        'lblDueDate
        '
        Me.lblDueDate.Appearance.Font = CType(resources.GetObject("lblDueDate.Appearance.Font"), System.Drawing.Font)
        Me.lblDueDate.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.lblDueDate, "lblDueDate")
        Me.lblDueDate.Name = "lblDueDate"
        '
        '_dateDue
        '
        resources.ApplyResources(Me._dateDue, "_dateDue")
        Me._dateDue.Name = "_dateDue"
        Me._dateDue.Properties.Appearance.Font = CType(resources.GetObject("_dateDue.Properties.Appearance.Font"), System.Drawing.Font)
        Me._dateDue.Properties.Appearance.Options.UseFont = True
        Me._dateDue.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("_dateDue.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me._dateDue.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("_dateDue.Properties.CalendarTimeProperties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        '
        '_lblTotal
        '
        Me._lblTotal.Appearance.Font = CType(resources.GetObject("_lblTotal.Appearance.Font"), System.Drawing.Font)
        Me._lblTotal.Appearance.Options.UseFont = True
        resources.ApplyResources(Me._lblTotal, "_lblTotal")
        Me._lblTotal.Name = "_lblTotal"
        '
        'panelActions
        '
        Me.panelActions.Controls.Add(Me._btnAddLine)
        Me.panelActions.Controls.Add(Me._btnRemoveLine)
        Me.panelActions.Controls.Add(Me._btnSave)
        resources.ApplyResources(Me.panelActions, "panelActions")
        Me.panelActions.Name = "panelActions"
        '
        '_btnAddLine
        '
        Me._btnAddLine.Appearance.Font = CType(resources.GetObject("_btnAddLine.Appearance.Font"), System.Drawing.Font)
        Me._btnAddLine.Appearance.Options.UseFont = True
        resources.ApplyResources(Me._btnAddLine, "_btnAddLine")
        Me._btnAddLine.Name = "_btnAddLine"
        '
        '_btnRemoveLine
        '
        Me._btnRemoveLine.Appearance.Font = CType(resources.GetObject("_btnRemoveLine.Appearance.Font"), System.Drawing.Font)
        Me._btnRemoveLine.Appearance.Options.UseFont = True
        resources.ApplyResources(Me._btnRemoveLine, "_btnRemoveLine")
        Me._btnRemoveLine.Name = "_btnRemoveLine"
        '
        '_btnSave
        '
        Me._btnSave.Appearance.Font = CType(resources.GetObject("_btnSave.Appearance.Font"), System.Drawing.Font)
        Me._btnSave.Appearance.Options.UseFont = True
        resources.ApplyResources(Me._btnSave, "_btnSave")
        Me._btnSave.Name = "_btnSave"
        '
        '_grid
        '
        resources.ApplyResources(Me._grid, "_grid")
        Me._grid.EmbeddedNavigator.Margin = CType(resources.GetObject("_grid.EmbeddedNavigator.Margin"), System.Windows.Forms.Padding)
        Me._grid.MainView = Me._view
        Me._grid.Name = "_grid"
        Me._grid.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me._view})
        '
        '_view
        '
        Me._view.Appearance.HeaderPanel.Font = CType(resources.GetObject("_view.Appearance.HeaderPanel.Font"), System.Drawing.Font)
        Me._view.Appearance.HeaderPanel.Options.UseFont = True
        Me._view.Appearance.Row.Font = CType(resources.GetObject("_view.Appearance.Row.Font"), System.Drawing.Font)
        Me._view.Appearance.Row.Options.UseFont = True
        Me._view.DetailHeight = 404
        Me._view.GridControl = Me._grid
        Me._view.Name = "_view"
        Me._view.OptionsEditForm.PopupEditFormWidth = 933
        '
        'StockBuyInvoiceForm
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me._grid)
        Me.Controls.Add(Me.panelActions)
        Me.Controls.Add(Me.panelTop)
        Me.Name = "StockBuyInvoiceForm"
        CType(Me.panelTop, System.ComponentModel.ISupportInitialize).EndInit()
        Me.panelTop.ResumeLayout(False)
        Me.panelTop.PerformLayout()
        CType(Me._cmbSupplier.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me._dateInvoice.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me._dateInvoice.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me._dateDue.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me._dateDue.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.panelActions, System.ComponentModel.ISupportInitialize).EndInit()
        Me.panelActions.ResumeLayout(False)
        CType(Me._grid, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me._view, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents panelTop As DevExpress.XtraEditors.PanelControl
    Friend WithEvents lblSupplier As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblInvoiceDate As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblDueDate As DevExpress.XtraEditors.LabelControl
    Friend WithEvents panelActions As DevExpress.XtraEditors.PanelControl
End Class

