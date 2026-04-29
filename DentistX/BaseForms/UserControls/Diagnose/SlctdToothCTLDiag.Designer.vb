<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class SlctdToothCTLDiag
    Inherits DevExpress.XtraEditors.XtraUserControl

    'UserControl overrides dispose to clean up the component list.
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(SlctdToothCTLDiag))
        Me.MainSplit = New DevExpress.XtraEditors.SplitContainerControl()
        Me.PanelSvgs = New DevExpress.XtraEditors.PanelControl()
        Me.btnPrevT = New DevExpress.XtraEditors.SimpleButton()
        Me.btnNextT = New DevExpress.XtraEditors.SimpleButton()
        Me.SvIN = New DevExpress.XtraEditors.SvgImageBox()
        Me.SvTop = New DevExpress.XtraEditors.SvgImageBox()
        Me.SvOut = New DevExpress.XtraEditors.SvgImageBox()
        Me.PanelDetails = New DevExpress.XtraEditors.PanelControl()
        Me.SlctdToothTab = New DevExpress.XtraTab.XtraTabControl()
        Me.ToothShapePage = New DevExpress.XtraTab.XtraTabPage()
        Me.SvgSlctd = New DevExpress.XtraEditors.SvgImageBox()
        Me.ToothTrtsPage = New DevExpress.XtraTab.XtraTabPage()
        Me.VGridTrts = New DevExpress.XtraGrid.GridControl()
        Me.GridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.colToothTrtID = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colPatientID = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colTooth = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colTreatDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colTreat = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colTreatNotes = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colTreatEndDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.PanelTrack = New DevExpress.XtraEditors.SidePanel()
        Me.CboGrdStyl = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.PanelPName = New DevExpress.XtraEditors.PanelControl()
        Me.lblView = New DevExpress.XtraEditors.LabelControl()
        Me.lblToothName = New DevExpress.XtraEditors.LabelControl()
        Me.lblPatientName = New DevExpress.XtraEditors.LabelControl()
        Me.TrtBS = New System.Windows.Forms.BindingSource(Me.components)
        Me.CheckBS = New System.Windows.Forms.BindingSource(Me.components)
        Me.HistBS = New System.Windows.Forms.BindingSource(Me.components)
        Me.CtlTip = New DevExpress.Utils.DefaultToolTipController(Me.components)
        CType(Me.MainSplit, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MainSplit.Panel1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.MainSplit.Panel1.SuspendLayout()
        CType(Me.MainSplit.Panel2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.MainSplit.Panel2.SuspendLayout()
        Me.MainSplit.SuspendLayout()
        CType(Me.PanelSvgs, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelSvgs.SuspendLayout()
        CType(Me.SvIN, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SvTop, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SvOut, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PanelDetails, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelDetails.SuspendLayout()
        CType(Me.SlctdToothTab, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SlctdToothTab.SuspendLayout()
        Me.ToothShapePage.SuspendLayout()
        CType(Me.SvgSlctd, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToothTrtsPage.SuspendLayout()
        CType(Me.VGridTrts, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelTrack.SuspendLayout()
        CType(Me.CboGrdStyl.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PanelPName, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelPName.SuspendLayout()
        CType(Me.TrtBS, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CheckBS, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.HistBS, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'MainSplit
        '
        resources.ApplyResources(Me.MainSplit, "MainSplit")
        Me.MainSplit.FixedPanel = DevExpress.XtraEditors.SplitFixedPanel.None
        Me.MainSplit.Name = "MainSplit"
        '
        'MainSplit.Panel1
        '
        Me.MainSplit.Panel1.Controls.Add(Me.PanelSvgs)
        resources.ApplyResources(Me.MainSplit.Panel1, "MainSplit.Panel1")
        '
        'MainSplit.Panel2
        '
        Me.MainSplit.Panel2.Controls.Add(Me.PanelDetails)
        resources.ApplyResources(Me.MainSplit.Panel2, "MainSplit.Panel2")
        Me.MainSplit.SplitterPosition = 525
        Me.MainSplit.ToolTipController = Me.CtlTip.DefaultController
        '
        'PanelSvgs
        '
        Me.CtlTip.SetAllowHtmlText(Me.PanelSvgs, CType(resources.GetObject("PanelSvgs.AllowHtmlText"), DevExpress.Utils.DefaultBoolean))
        Me.PanelSvgs.Controls.Add(Me.btnPrevT)
        Me.PanelSvgs.Controls.Add(Me.btnNextT)
        Me.PanelSvgs.Controls.Add(Me.SvIN)
        Me.PanelSvgs.Controls.Add(Me.SvTop)
        Me.PanelSvgs.Controls.Add(Me.SvOut)
        resources.ApplyResources(Me.PanelSvgs, "PanelSvgs")
        Me.PanelSvgs.Name = "PanelSvgs"
        '
        'btnPrevT
        '
        Me.btnPrevT.Appearance.Font = CType(resources.GetObject("btnPrevT.Appearance.Font"), System.Drawing.Font)
        Me.btnPrevT.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.btnPrevT, "btnPrevT")
        Me.btnPrevT.Name = "btnPrevT"
        '
        'btnNextT
        '
        Me.btnNextT.Appearance.Font = CType(resources.GetObject("btnNextT.Appearance.Font"), System.Drawing.Font)
        Me.btnNextT.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.btnNextT, "btnNextT")
        Me.btnNextT.Name = "btnNextT"
        '
        'SvIN
        '
        resources.ApplyResources(Me.SvIN, "SvIN")
        Me.SvIN.Name = "SvIN"
        Me.SvIN.SizeMode = DevExpress.XtraEditors.SvgImageSizeMode.Zoom
        '
        'SvTop
        '
        resources.ApplyResources(Me.SvTop, "SvTop")
        Me.SvTop.Name = "SvTop"
        Me.SvTop.SizeMode = DevExpress.XtraEditors.SvgImageSizeMode.Zoom
        '
        'SvOut
        '
        resources.ApplyResources(Me.SvOut, "SvOut")
        Me.SvOut.Name = "SvOut"
        Me.SvOut.SizeMode = DevExpress.XtraEditors.SvgImageSizeMode.Zoom
        '
        'PanelDetails
        '
        Me.CtlTip.SetAllowHtmlText(Me.PanelDetails, CType(resources.GetObject("PanelDetails.AllowHtmlText"), DevExpress.Utils.DefaultBoolean))
        Me.PanelDetails.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.PanelDetails.Controls.Add(Me.SlctdToothTab)
        Me.PanelDetails.Controls.Add(Me.PanelTrack)
        Me.PanelDetails.Controls.Add(Me.PanelPName)
        resources.ApplyResources(Me.PanelDetails, "PanelDetails")
        Me.PanelDetails.Name = "PanelDetails"
        '
        'SlctdToothTab
        '
        Me.SlctdToothTab.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.SlctdToothTab.Appearance.Options.UseBackColor = True
        resources.ApplyResources(Me.SlctdToothTab, "SlctdToothTab")
        Me.SlctdToothTab.Name = "SlctdToothTab"
        Me.SlctdToothTab.SelectedTabPage = Me.ToothShapePage
        Me.SlctdToothTab.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.ToothShapePage, Me.ToothTrtsPage})
        '
        'ToothShapePage
        '
        Me.ToothShapePage.Appearance.Header.Font = CType(resources.GetObject("ToothShapePage.Appearance.Header.Font"), System.Drawing.Font)
        Me.ToothShapePage.Appearance.Header.Options.UseFont = True
        Me.ToothShapePage.Controls.Add(Me.SvgSlctd)
        Me.ToothShapePage.Name = "ToothShapePage"
        resources.ApplyResources(Me.ToothShapePage, "ToothShapePage")
        '
        'SvgSlctd
        '
        Me.SvgSlctd.AutoSizeInLayoutControl = True
        resources.ApplyResources(Me.SvgSlctd, "SvgSlctd")
        Me.SvgSlctd.Name = "SvgSlctd"
        Me.SvgSlctd.OptionsSelection.SelectionMode = DevExpress.XtraEditors.SvgImageItemSelectionMode.[Single]
        Me.SvgSlctd.SizeMode = DevExpress.XtraEditors.SvgImageSizeMode.Zoom
        '
        'ToothTrtsPage
        '
        Me.ToothTrtsPage.Appearance.Header.Font = CType(resources.GetObject("ToothTrtsPage.Appearance.Header.Font"), System.Drawing.Font)
        Me.ToothTrtsPage.Appearance.Header.Options.UseFont = True
        Me.ToothTrtsPage.Controls.Add(Me.VGridTrts)
        Me.ToothTrtsPage.Name = "ToothTrtsPage"
        resources.ApplyResources(Me.ToothTrtsPage, "ToothTrtsPage")
        '
        'VGridTrts
        '
        resources.ApplyResources(Me.VGridTrts, "VGridTrts")
        Me.VGridTrts.MainView = Me.GridView1
        Me.VGridTrts.Name = "VGridTrts"
        Me.VGridTrts.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridView1})
        '
        'GridView1
        '
        Me.GridView1.Appearance.HeaderPanel.Font = CType(resources.GetObject("GridView1.Appearance.HeaderPanel.Font"), System.Drawing.Font)
        Me.GridView1.Appearance.HeaderPanel.Options.UseFont = True
        Me.GridView1.Appearance.HeaderPanel.Options.UseTextOptions = True
        Me.GridView1.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridView1.Appearance.Row.Font = CType(resources.GetObject("GridView1.Appearance.Row.Font"), System.Drawing.Font)
        Me.GridView1.Appearance.Row.Options.UseFont = True
        Me.GridView1.Appearance.ViewCaption.Font = CType(resources.GetObject("GridView1.Appearance.ViewCaption.Font"), System.Drawing.Font)
        Me.GridView1.Appearance.ViewCaption.Options.UseFont = True
        Me.GridView1.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.colToothTrtID, Me.colPatientID, Me.colTooth, Me.colTreatDate, Me.colTreat, Me.colTreatNotes, Me.colTreatEndDate})
        Me.GridView1.GridControl = Me.VGridTrts
        Me.GridView1.Name = "GridView1"
        Me.GridView1.OptionsDetail.EnableMasterViewMode = False
        Me.GridView1.OptionsView.BestFitMode = DevExpress.XtraGrid.Views.Grid.GridBestFitMode.Fast
        Me.GridView1.OptionsView.BestFitUseErrorInfo = DevExpress.Utils.DefaultBoolean.[True]
        Me.GridView1.OptionsView.EnableAppearanceEvenRow = True
        Me.GridView1.OptionsView.EnableAppearanceOddRow = True
        '
        'colToothTrtID
        '
        resources.ApplyResources(Me.colToothTrtID, "colToothTrtID")
        Me.colToothTrtID.FieldName = "ToothTrtID"
        Me.colToothTrtID.Name = "colToothTrtID"
        Me.colToothTrtID.UnboundDataType = GetType(Integer)
        '
        'colPatientID
        '
        resources.ApplyResources(Me.colPatientID, "colPatientID")
        Me.colPatientID.FieldName = "PatientID"
        Me.colPatientID.Name = "colPatientID"
        Me.colPatientID.UnboundDataType = GetType(Integer)
        '
        'colTooth
        '
        Me.colTooth.AppearanceHeader.Font = CType(resources.GetObject("colTooth.AppearanceHeader.Font"), System.Drawing.Font)
        Me.colTooth.AppearanceHeader.Options.UseFont = True
        resources.ApplyResources(Me.colTooth, "colTooth")
        Me.colTooth.FieldName = "Tooth"
        Me.colTooth.Name = "colTooth"
        Me.colTooth.UnboundDataType = GetType(String)
        '
        'colTreatDate
        '
        Me.colTreatDate.AppearanceHeader.Font = CType(resources.GetObject("colTreatDate.AppearanceHeader.Font"), System.Drawing.Font)
        Me.colTreatDate.AppearanceHeader.Options.UseFont = True
        resources.ApplyResources(Me.colTreatDate, "colTreatDate")
        Me.colTreatDate.FieldName = "TreatDate"
        Me.colTreatDate.Name = "colTreatDate"
        Me.colTreatDate.UnboundDataType = GetType(Date)
        '
        'colTreat
        '
        Me.colTreat.AppearanceHeader.Font = CType(resources.GetObject("colTreat.AppearanceHeader.Font"), System.Drawing.Font)
        Me.colTreat.AppearanceHeader.Options.UseFont = True
        resources.ApplyResources(Me.colTreat, "colTreat")
        Me.colTreat.FieldName = "Treat"
        Me.colTreat.Name = "colTreat"
        Me.colTreat.UnboundDataType = GetType(String)
        '
        'colTreatNotes
        '
        Me.colTreatNotes.AppearanceHeader.Font = CType(resources.GetObject("colTreatNotes.AppearanceHeader.Font"), System.Drawing.Font)
        Me.colTreatNotes.AppearanceHeader.Options.UseFont = True
        resources.ApplyResources(Me.colTreatNotes, "colTreatNotes")
        Me.colTreatNotes.FieldName = "TreatNotes"
        Me.colTreatNotes.Name = "colTreatNotes"
        Me.colTreatNotes.UnboundDataType = GetType(String)
        '
        'colTreatEndDate
        '
        Me.colTreatEndDate.AppearanceHeader.Font = CType(resources.GetObject("colTreatEndDate.AppearanceHeader.Font"), System.Drawing.Font)
        Me.colTreatEndDate.AppearanceHeader.Options.UseFont = True
        resources.ApplyResources(Me.colTreatEndDate, "colTreatEndDate")
        Me.colTreatEndDate.FieldName = "TreatEndDate"
        Me.colTreatEndDate.Name = "colTreatEndDate"
        Me.colTreatEndDate.UnboundDataType = GetType(Date)
        '
        'PanelTrack
        '
        Me.CtlTip.SetAllowHtmlText(Me.PanelTrack, CType(resources.GetObject("PanelTrack.AllowHtmlText"), DevExpress.Utils.DefaultBoolean))
        Me.PanelTrack.Controls.Add(Me.CboGrdStyl)
        resources.ApplyResources(Me.PanelTrack, "PanelTrack")
        Me.PanelTrack.Name = "PanelTrack"
        '
        'CboGrdStyl
        '
        resources.ApplyResources(Me.CboGrdStyl, "CboGrdStyl")
        Me.CboGrdStyl.Name = "CboGrdStyl"
        Me.CboGrdStyl.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("CboGrdStyl.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me.CboGrdStyl.Properties.Items.AddRange(New Object() {resources.GetString("CboGrdStyl.Properties.Items"), resources.GetString("CboGrdStyl.Properties.Items1"), resources.GetString("CboGrdStyl.Properties.Items2")})
        '
        'PanelPName
        '
        Me.CtlTip.SetAllowHtmlText(Me.PanelPName, CType(resources.GetObject("PanelPName.AllowHtmlText"), DevExpress.Utils.DefaultBoolean))
        Me.PanelPName.Controls.Add(Me.lblView)
        Me.PanelPName.Controls.Add(Me.lblToothName)
        Me.PanelPName.Controls.Add(Me.lblPatientName)
        resources.ApplyResources(Me.PanelPName, "PanelPName")
        Me.PanelPName.Name = "PanelPName"
        '
        'lblView
        '
        Me.lblView.Appearance.Font = CType(resources.GetObject("lblView.Appearance.Font"), System.Drawing.Font)
        Me.lblView.Appearance.ForeColor = System.Drawing.Color.Yellow
        Me.lblView.Appearance.Options.UseFont = True
        Me.lblView.Appearance.Options.UseForeColor = True
        Me.lblView.Appearance.Options.UseTextOptions = True
        Me.lblView.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.lblView.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        resources.ApplyResources(Me.lblView, "lblView")
        Me.lblView.Name = "lblView"
        '
        'lblToothName
        '
        Me.lblToothName.Appearance.Font = CType(resources.GetObject("lblToothName.Appearance.Font"), System.Drawing.Font)
        Me.lblToothName.Appearance.ForeColor = System.Drawing.Color.Red
        Me.lblToothName.Appearance.Options.UseFont = True
        Me.lblToothName.Appearance.Options.UseForeColor = True
        Me.lblToothName.Appearance.Options.UseTextOptions = True
        Me.lblToothName.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.lblToothName.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        resources.ApplyResources(Me.lblToothName, "lblToothName")
        Me.lblToothName.Name = "lblToothName"
        '
        'lblPatientName
        '
        Me.lblPatientName.Appearance.Font = CType(resources.GetObject("lblPatientName.Appearance.Font"), System.Drawing.Font)
        Me.lblPatientName.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.lblPatientName.Appearance.Options.UseFont = True
        Me.lblPatientName.Appearance.Options.UseForeColor = True
        Me.lblPatientName.Appearance.Options.UseTextOptions = True
        Me.lblPatientName.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.lblPatientName.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        resources.ApplyResources(Me.lblPatientName, "lblPatientName")
        Me.lblPatientName.Name = "lblPatientName"
        '
        'TrtBS
        '
        '
        'CtlTip
        '
        '
        '
        '
        Me.CtlTip.DefaultController.AllowHtmlText = True
        Me.CtlTip.DefaultController.AutoPopDelay = 15000
        Me.CtlTip.DefaultController.CloseOnClick = DevExpress.Utils.DefaultBoolean.[True]
        Me.CtlTip.DefaultController.KeepWhileHovered = True
        Me.CtlTip.DefaultController.ToolTipType = DevExpress.Utils.ToolTipType.SuperTip
        '
        'SlctdToothCTLDiag
        '
        Me.CtlTip.SetAllowHtmlText(Me, CType(resources.GetObject("$this.AllowHtmlText"), DevExpress.Utils.DefaultBoolean))
        Me.Appearance.Options.UseFont = True
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.MainSplit)
        Me.Name = "SlctdToothCTLDiag"
        CType(Me.MainSplit.Panel1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.MainSplit.Panel1.ResumeLayout(False)
        CType(Me.MainSplit.Panel2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.MainSplit.Panel2.ResumeLayout(False)
        CType(Me.MainSplit, System.ComponentModel.ISupportInitialize).EndInit()
        Me.MainSplit.ResumeLayout(False)
        CType(Me.PanelSvgs, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelSvgs.ResumeLayout(False)
        CType(Me.SvIN, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SvTop, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SvOut, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PanelDetails, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelDetails.ResumeLayout(False)
        CType(Me.SlctdToothTab, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SlctdToothTab.ResumeLayout(False)
        Me.ToothShapePage.ResumeLayout(False)
        CType(Me.SvgSlctd, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToothTrtsPage.ResumeLayout(False)
        CType(Me.VGridTrts, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelTrack.ResumeLayout(False)
        CType(Me.CboGrdStyl.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PanelPName, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelPName.ResumeLayout(False)
        CType(Me.TrtBS, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CheckBS, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.HistBS, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents PanelDetails As DevExpress.XtraEditors.PanelControl
    Friend WithEvents PanelPName As DevExpress.XtraEditors.PanelControl
    Friend WithEvents PanelSvgs As DevExpress.XtraEditors.PanelControl
    Friend WithEvents SvOut As DevExpress.XtraEditors.SvgImageBox
    Friend WithEvents SvIN As DevExpress.XtraEditors.SvgImageBox
    Friend WithEvents SvTop As DevExpress.XtraEditors.SvgImageBox
    Friend WithEvents lblPatientName As DevExpress.XtraEditors.LabelControl
    Friend WithEvents btnPrevT As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnNextT As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents CheckBS As Windows.Forms.BindingSource
    Friend WithEvents TrtBS As Windows.Forms.BindingSource
    Friend WithEvents lblView As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblToothName As DevExpress.XtraEditors.LabelControl
    Friend WithEvents SvgSlctd As DevExpress.XtraEditors.SvgImageBox
    Friend WithEvents PanelTrack As DevExpress.XtraEditors.SidePanel
    Friend WithEvents SlctdToothTab As DevExpress.XtraTab.XtraTabControl
    Friend WithEvents ToothShapePage As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents ToothTrtsPage As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents HistBS As BindingSource
    Friend WithEvents MainSplit As DevExpress.XtraEditors.SplitContainerControl
    Friend WithEvents CboGrdStyl As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents VGridTrts As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridView1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents colToothTrtID As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colPatientID As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colTreatDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colTreat As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colTreatNotes As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colTreatEndDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colTooth As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CtlTip As DevExpress.Utils.DefaultToolTipController
End Class
