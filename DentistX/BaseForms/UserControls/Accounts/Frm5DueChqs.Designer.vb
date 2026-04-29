<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm5DueChqs
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Frm5DueChqs))
        Me.MainSplit = New DevExpress.XtraEditors.SplitContainerControl()
        Me.AllChqsGrid = New DevExpress.XtraGrid.GridControl()
        Me.AllChqsView = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.ChqImage = New DevExpress.XtraEditors.PictureEdit()
        Me.lblAllChqsChequeScanSide = New DevExpress.XtraEditors.LabelControl()
        Me.radioAllChqsChequeSide = New DevExpress.XtraEditors.RadioGroup()
        Me.hdrPanel = New DevExpress.XtraEditors.PanelControl()
        Me.btnListChqs = New DevExpress.XtraEditors.SimpleButton()
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.rgDueCheqListMode = New DevExpress.XtraEditors.RadioGroup()
        Me.txtWorkDays = New DevExpress.XtraEditors.TextEdit()
        Me.lblDueChqListHint = New DevExpress.XtraEditors.LabelControl()
        Me.botmPanel = New DevExpress.XtraEditors.PanelControl()
        Me.btnUpdateAll = New DevExpress.XtraEditors.SimpleButton()
        Me.btnEditDueCheque = New DevExpress.XtraEditors.SimpleButton()
        Me.btnUpdate = New DevExpress.XtraEditors.SimpleButton()
        Me.gridPanel = New DevExpress.XtraEditors.PanelControl()
        Me.chqsGrid = New DevExpress.XtraGrid.GridControl()
        Me.ChqsBS = New System.Windows.Forms.BindingSource(Me.components)
        Me.DueChqView = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.colPayID = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colPatientID = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colPatientName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colChqOwner = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colAccountNumber = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colChqNumber = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colChqBank = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colChqDueDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colAdjustedDueDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colPayValue = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colPayDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colNotes = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colPayType = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colIsCashed = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colIsForward = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colIsReturnedDue = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CheuqesTab = New DevExpress.XtraTab.XtraTabControl()
        Me.AllChequesTab = New DevExpress.XtraTab.XtraTabPage()
        Me.pnlFilter = New DevExpress.XtraEditors.PanelControl()
        Me.dtpFromPayDate = New DevExpress.XtraEditors.DateEdit()
        Me.dtpToPayDate = New DevExpress.XtraEditors.DateEdit()
        Me.btnListAllChqs = New DevExpress.XtraEditors.SimpleButton()
        Me.btnEditAllChqs = New DevExpress.XtraEditors.SimpleButton()
        Me.btnSrchAllChqs = New DevExpress.XtraEditors.SimpleButton()
        Me.txtChqNumSrch = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl3 = New DevExpress.XtraEditors.LabelControl()
        Me.txtPatientSrch = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl6 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl5 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl4 = New DevExpress.XtraEditors.LabelControl()
        Me.DueChequesTab = New DevExpress.XtraTab.XtraTabPage()
        Me.ChequeReturnedTab = New DevExpress.XtraTab.XtraTabPage()
        Me.gridReturned = New DevExpress.XtraGrid.GridControl()
        Me.ReturnedChqsBS = New System.Windows.Forms.BindingSource(Me.components)
        Me.ReturnedGridView = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.colReturnedPayID = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colReturnedPatientID = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colReturnedPatientName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colReturnedChqOwner = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colReturnedAccountNumber = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colReturnedChqNumber = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colReturnedChqBank = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colReturnedChqDueDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colReturnedPayValue = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colReturnedPayDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colReturnedNotes = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colReturnedIsCashed = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colIsReturned = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ReturnedDetailsPanel = New DevExpress.XtraEditors.PanelControl()
        Me.grpPatient = New DevExpress.XtraEditors.GroupControl()
        Me.lblPatientNameVal = New DevExpress.XtraEditors.LabelControl()
        Me.lblPatientIDVal = New DevExpress.XtraEditors.LabelControl()
        Me.lblPatientNameCap = New DevExpress.XtraEditors.LabelControl()
        Me.lblPatientIDCap = New DevExpress.XtraEditors.LabelControl()
        Me.grpChqDetails = New DevExpress.XtraEditors.GroupControl()
        Me.lblChqDetailsVal = New DevExpress.XtraEditors.LabelControl()
        Me.lblChqDetailsCap = New DevExpress.XtraEditors.LabelControl()
        Me.ReturnedSearchHeader = New DevExpress.XtraEditors.PanelControl()
        Me.btnListAllReturned = New DevExpress.XtraEditors.SimpleButton()
        Me.btnSearchReturned = New DevExpress.XtraEditors.SimpleButton()
        Me.btnEditReturnedCheque = New DevExpress.XtraEditors.SimpleButton()
        Me.txtSearchAccountNumber = New DevExpress.XtraEditors.TextEdit()
        Me.lblAccountNumber = New DevExpress.XtraEditors.LabelControl()
        Me.txtSearchChqNumber = New DevExpress.XtraEditors.TextEdit()
        Me.lblChqNumber = New DevExpress.XtraEditors.LabelControl()
        CType(Me.MainSplit, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MainSplit.Panel1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.MainSplit.Panel1.SuspendLayout()
        CType(Me.MainSplit.Panel2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.MainSplit.Panel2.SuspendLayout()
        Me.MainSplit.SuspendLayout()
        CType(Me.AllChqsGrid, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.AllChqsView, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ChqImage.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.radioAllChqsChequeSide.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.hdrPanel, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.hdrPanel.SuspendLayout()
        CType(Me.rgDueCheqListMode.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtWorkDays.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.botmPanel, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.botmPanel.SuspendLayout()
        CType(Me.gridPanel, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gridPanel.SuspendLayout()
        CType(Me.chqsGrid, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ChqsBS, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DueChqView, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CheuqesTab, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.CheuqesTab.SuspendLayout()
        Me.AllChequesTab.SuspendLayout()
        CType(Me.pnlFilter, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlFilter.SuspendLayout()
        CType(Me.dtpFromPayDate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpFromPayDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpToPayDate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpToPayDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtChqNumSrch.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtPatientSrch.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.DueChequesTab.SuspendLayout()
        Me.ChequeReturnedTab.SuspendLayout()
        CType(Me.gridReturned, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ReturnedChqsBS, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ReturnedGridView, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ReturnedDetailsPanel, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ReturnedDetailsPanel.SuspendLayout()
        CType(Me.grpPatient, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpPatient.SuspendLayout()
        CType(Me.grpChqDetails, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpChqDetails.SuspendLayout()
        CType(Me.ReturnedSearchHeader, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ReturnedSearchHeader.SuspendLayout()
        CType(Me.txtSearchAccountNumber.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtSearchChqNumber.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'MainSplit
        '
        resources.ApplyResources(Me.MainSplit, "MainSplit")
        Me.MainSplit.Name = "MainSplit"
        '
        'MainSplit.Panel1
        '
        Me.MainSplit.Panel1.Controls.Add(Me.AllChqsGrid)
        resources.ApplyResources(Me.MainSplit.Panel1, "MainSplit.Panel1")
        '
        'MainSplit.Panel2
        '
        Me.MainSplit.Panel2.Controls.Add(Me.ChqImage)
        Me.MainSplit.Panel2.Controls.Add(Me.lblAllChqsChequeScanSide)
        Me.MainSplit.Panel2.Controls.Add(Me.radioAllChqsChequeSide)
        resources.ApplyResources(Me.MainSplit.Panel2, "MainSplit.Panel2")
        Me.MainSplit.SplitterPosition = 796
        '
        'AllChqsGrid
        '
        resources.ApplyResources(Me.AllChqsGrid, "AllChqsGrid")
        Me.AllChqsGrid.MainView = Me.AllChqsView
        Me.AllChqsGrid.Name = "AllChqsGrid"
        Me.AllChqsGrid.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.AllChqsView})
        '
        'AllChqsView
        '
        Me.AllChqsView.Appearance.HeaderPanel.Font = CType(resources.GetObject("AllChqsView.Appearance.HeaderPanel.Font"), System.Drawing.Font)
        Me.AllChqsView.Appearance.HeaderPanel.Options.UseFont = True
        Me.AllChqsView.Appearance.Row.Font = CType(resources.GetObject("AllChqsView.Appearance.Row.Font"), System.Drawing.Font)
        Me.AllChqsView.Appearance.Row.Options.UseFont = True
        Me.AllChqsView.GridControl = Me.AllChqsGrid
        Me.AllChqsView.Name = "AllChqsView"
        '
        'ChqImage
        '
        resources.ApplyResources(Me.ChqImage, "ChqImage")
        Me.ChqImage.Name = "ChqImage"
        Me.ChqImage.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.[Auto]
        Me.ChqImage.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom
        '
        'lblAllChqsChequeScanSide
        '
        Me.lblAllChqsChequeScanSide.Appearance.Font = CType(resources.GetObject("lblAllChqsChequeScanSide.Appearance.Font"), System.Drawing.Font)
        Me.lblAllChqsChequeScanSide.Appearance.Options.UseFont = True
        Me.lblAllChqsChequeScanSide.Appearance.Options.UseTextOptions = True
        Me.lblAllChqsChequeScanSide.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
        resources.ApplyResources(Me.lblAllChqsChequeScanSide, "lblAllChqsChequeScanSide")
        Me.lblAllChqsChequeScanSide.Name = "lblAllChqsChequeScanSide"
        '
        'radioAllChqsChequeSide
        '
        resources.ApplyResources(Me.radioAllChqsChequeSide, "radioAllChqsChequeSide")
        Me.radioAllChqsChequeSide.Name = "radioAllChqsChequeSide"
        Me.radioAllChqsChequeSide.Properties.Appearance.Font = CType(resources.GetObject("radioAllChqsChequeSide.Properties.Appearance.Font"), System.Drawing.Font)
        Me.radioAllChqsChequeSide.Properties.Appearance.Options.UseFont = True
        Me.radioAllChqsChequeSide.Properties.Columns = 2
        '
        'hdrPanel
        '
        Me.hdrPanel.Controls.Add(Me.btnListChqs)
        Me.hdrPanel.Controls.Add(Me.LabelControl2)
        Me.hdrPanel.Controls.Add(Me.LabelControl1)
        Me.hdrPanel.Controls.Add(Me.rgDueCheqListMode)
        Me.hdrPanel.Controls.Add(Me.txtWorkDays)
        Me.hdrPanel.Controls.Add(Me.lblDueChqListHint)
        resources.ApplyResources(Me.hdrPanel, "hdrPanel")
        Me.hdrPanel.Name = "hdrPanel"
        '
        'btnListChqs
        '
        Me.btnListChqs.Appearance.Font = CType(resources.GetObject("btnListChqs.Appearance.Font"), System.Drawing.Font)
        Me.btnListChqs.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.btnListChqs, "btnListChqs")
        Me.btnListChqs.Name = "btnListChqs"
        '
        'LabelControl2
        '
        Me.LabelControl2.Appearance.Font = CType(resources.GetObject("LabelControl2.Appearance.Font"), System.Drawing.Font)
        Me.LabelControl2.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.LabelControl2, "LabelControl2")
        Me.LabelControl2.Name = "LabelControl2"
        '
        'LabelControl1
        '
        Me.LabelControl1.Appearance.Font = CType(resources.GetObject("LabelControl1.Appearance.Font"), System.Drawing.Font)
        Me.LabelControl1.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.LabelControl1, "LabelControl1")
        Me.LabelControl1.Name = "LabelControl1"
        '
        'rgDueCheqListMode
        '
        resources.ApplyResources(Me.rgDueCheqListMode, "rgDueCheqListMode")
        Me.rgDueCheqListMode.Name = "rgDueCheqListMode"
        Me.rgDueCheqListMode.Properties.Appearance.Font = CType(resources.GetObject("rgDueCheqListMode.Properties.Appearance.Font"), System.Drawing.Font)
        Me.rgDueCheqListMode.Properties.Appearance.Options.UseFont = True
        Me.rgDueCheqListMode.Properties.Columns = 2
        '
        'txtWorkDays
        '
        resources.ApplyResources(Me.txtWorkDays, "txtWorkDays")
        Me.txtWorkDays.Name = "txtWorkDays"
        Me.txtWorkDays.Properties.Appearance.Font = CType(resources.GetObject("txtWorkDays.Properties.Appearance.Font"), System.Drawing.Font)
        Me.txtWorkDays.Properties.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.txtWorkDays.Properties.Appearance.Options.UseFont = True
        Me.txtWorkDays.Properties.Appearance.Options.UseForeColor = True
        Me.txtWorkDays.Properties.Appearance.Options.UseTextOptions = True
        Me.txtWorkDays.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.txtWorkDays.Properties.Mask.MaskType = CType(resources.GetObject("txtWorkDays.Properties.Mask.MaskType"), DevExpress.XtraEditors.Mask.MaskType)
        '
        'lblDueChqListHint
        '
        Me.lblDueChqListHint.Appearance.Font = CType(resources.GetObject("lblDueChqListHint.Appearance.Font"), System.Drawing.Font)
        Me.lblDueChqListHint.Appearance.Options.UseFont = True
        Me.lblDueChqListHint.Appearance.Options.UseTextOptions = True
        Me.lblDueChqListHint.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
        resources.ApplyResources(Me.lblDueChqListHint, "lblDueChqListHint")
        Me.lblDueChqListHint.Name = "lblDueChqListHint"
        '
        'botmPanel
        '
        Me.botmPanel.Controls.Add(Me.btnUpdateAll)
        Me.botmPanel.Controls.Add(Me.btnEditDueCheque)
        Me.botmPanel.Controls.Add(Me.btnUpdate)
        resources.ApplyResources(Me.botmPanel, "botmPanel")
        Me.botmPanel.Name = "botmPanel"
        '
        'btnUpdateAll
        '
        Me.btnUpdateAll.Appearance.Font = CType(resources.GetObject("btnUpdateAll.Appearance.Font"), System.Drawing.Font)
        Me.btnUpdateAll.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.btnUpdateAll, "btnUpdateAll")
        Me.btnUpdateAll.Name = "btnUpdateAll"
        '
        'btnEditDueCheque
        '
        Me.btnEditDueCheque.Appearance.Font = CType(resources.GetObject("btnEditDueCheque.Appearance.Font"), System.Drawing.Font)
        Me.btnEditDueCheque.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.btnEditDueCheque, "btnEditDueCheque")
        Me.btnEditDueCheque.Name = "btnEditDueCheque"
        '
        'btnUpdate
        '
        Me.btnUpdate.Appearance.Font = CType(resources.GetObject("btnUpdate.Appearance.Font"), System.Drawing.Font)
        Me.btnUpdate.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.btnUpdate, "btnUpdate")
        Me.btnUpdate.Name = "btnUpdate"
        '
        'gridPanel
        '
        Me.gridPanel.Controls.Add(Me.chqsGrid)
        resources.ApplyResources(Me.gridPanel, "gridPanel")
        Me.gridPanel.Name = "gridPanel"
        '
        'chqsGrid
        '
        Me.chqsGrid.DataSource = Me.ChqsBS
        resources.ApplyResources(Me.chqsGrid, "chqsGrid")
        Me.chqsGrid.MainView = Me.DueChqView
        Me.chqsGrid.Name = "chqsGrid"
        Me.chqsGrid.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.DueChqView})
        '
        'DueChqView
        '
        Me.DueChqView.Appearance.HeaderPanel.Font = CType(resources.GetObject("DueChqView.Appearance.HeaderPanel.Font"), System.Drawing.Font)
        Me.DueChqView.Appearance.HeaderPanel.Options.UseFont = True
        Me.DueChqView.Appearance.Row.Font = CType(resources.GetObject("DueChqView.Appearance.Row.Font"), System.Drawing.Font)
        Me.DueChqView.Appearance.Row.Options.UseFont = True
        Me.DueChqView.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.colPayID, Me.colPatientID, Me.colPatientName, Me.colChqOwner, Me.colAccountNumber, Me.colChqNumber, Me.colChqBank, Me.colChqDueDate, Me.colAdjustedDueDate, Me.colPayValue, Me.colPayDate, Me.colNotes, Me.colPayType, Me.colIsCashed, Me.colIsForward, Me.colIsReturnedDue})
        Me.DueChqView.GridControl = Me.chqsGrid
        Me.DueChqView.Name = "DueChqView"
        '
        'colPayID
        '
        resources.ApplyResources(Me.colPayID, "colPayID")
        Me.colPayID.FieldName = "PayID"
        Me.colPayID.Name = "colPayID"
        '
        'colPatientID
        '
        resources.ApplyResources(Me.colPatientID, "colPatientID")
        Me.colPatientID.FieldName = "PatientID"
        Me.colPatientID.Name = "colPatientID"
        '
        'colPatientName
        '
        resources.ApplyResources(Me.colPatientName, "colPatientName")
        Me.colPatientName.FieldName = "PatientName"
        Me.colPatientName.Name = "colPatientName"
        '
        'colChqOwner
        '
        resources.ApplyResources(Me.colChqOwner, "colChqOwner")
        Me.colChqOwner.FieldName = "ChqOwner"
        Me.colChqOwner.Name = "colChqOwner"
        '
        'colAccountNumber
        '
        resources.ApplyResources(Me.colAccountNumber, "colAccountNumber")
        Me.colAccountNumber.FieldName = "AccountNumber"
        Me.colAccountNumber.Name = "colAccountNumber"
        '
        'colChqNumber
        '
        resources.ApplyResources(Me.colChqNumber, "colChqNumber")
        Me.colChqNumber.FieldName = "ChqNumber"
        Me.colChqNumber.Name = "colChqNumber"
        '
        'colChqBank
        '
        resources.ApplyResources(Me.colChqBank, "colChqBank")
        Me.colChqBank.FieldName = "ChqBank"
        Me.colChqBank.Name = "colChqBank"
        '
        'colChqDueDate
        '
        resources.ApplyResources(Me.colChqDueDate, "colChqDueDate")
        Me.colChqDueDate.FieldName = "ChqDueDate"
        Me.colChqDueDate.Name = "colChqDueDate"
        '
        'colAdjustedDueDate
        '
        resources.ApplyResources(Me.colAdjustedDueDate, "colAdjustedDueDate")
        Me.colAdjustedDueDate.FieldName = "AdjustedDueDate"
        Me.colAdjustedDueDate.Name = "colAdjustedDueDate"
        '
        'colPayValue
        '
        resources.ApplyResources(Me.colPayValue, "colPayValue")
        Me.colPayValue.FieldName = "PayValue"
        Me.colPayValue.Name = "colPayValue"
        '
        'colPayDate
        '
        resources.ApplyResources(Me.colPayDate, "colPayDate")
        Me.colPayDate.FieldName = "PayDate"
        Me.colPayDate.Name = "colPayDate"
        '
        'colNotes
        '
        resources.ApplyResources(Me.colNotes, "colNotes")
        Me.colNotes.FieldName = "Notes"
        Me.colNotes.Name = "colNotes"
        '
        'colPayType
        '
        resources.ApplyResources(Me.colPayType, "colPayType")
        Me.colPayType.FieldName = "PayType"
        Me.colPayType.Name = "colPayType"
        '
        'colIsCashed
        '
        resources.ApplyResources(Me.colIsCashed, "colIsCashed")
        Me.colIsCashed.FieldName = "IsCashed"
        Me.colIsCashed.Name = "colIsCashed"
        Me.colIsCashed.UnboundDataType = GetType(Boolean)
        '
        'colIsForward
        '
        resources.ApplyResources(Me.colIsForward, "colIsForward")
        Me.colIsForward.FieldName = "IsForward"
        Me.colIsForward.Name = "colIsForward"
        '
        'colIsReturnedDue
        '
        resources.ApplyResources(Me.colIsReturnedDue, "colIsReturnedDue")
        Me.colIsReturnedDue.FieldName = "IsReturned"
        Me.colIsReturnedDue.Name = "colIsReturnedDue"
        '
        'CheuqesTab
        '
        Me.CheuqesTab.AppearancePage.Header.Font = CType(resources.GetObject("CheuqesTab.AppearancePage.Header.Font"), System.Drawing.Font)
        Me.CheuqesTab.AppearancePage.Header.Options.UseFont = True
        resources.ApplyResources(Me.CheuqesTab, "CheuqesTab")
        Me.CheuqesTab.Name = "CheuqesTab"
        Me.CheuqesTab.SelectedTabPage = Me.AllChequesTab
        Me.CheuqesTab.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.AllChequesTab, Me.DueChequesTab, Me.ChequeReturnedTab})
        '
        'AllChequesTab
        '
        Me.AllChequesTab.Controls.Add(Me.MainSplit)
        Me.AllChequesTab.Controls.Add(Me.pnlFilter)
        Me.AllChequesTab.Name = "AllChequesTab"
        resources.ApplyResources(Me.AllChequesTab, "AllChequesTab")
        '
        'pnlFilter
        '
        Me.pnlFilter.Controls.Add(Me.dtpFromPayDate)
        Me.pnlFilter.Controls.Add(Me.dtpToPayDate)
        Me.pnlFilter.Controls.Add(Me.btnListAllChqs)
        Me.pnlFilter.Controls.Add(Me.btnEditAllChqs)
        Me.pnlFilter.Controls.Add(Me.btnSrchAllChqs)
        Me.pnlFilter.Controls.Add(Me.txtChqNumSrch)
        Me.pnlFilter.Controls.Add(Me.LabelControl3)
        Me.pnlFilter.Controls.Add(Me.txtPatientSrch)
        Me.pnlFilter.Controls.Add(Me.LabelControl6)
        Me.pnlFilter.Controls.Add(Me.LabelControl5)
        Me.pnlFilter.Controls.Add(Me.LabelControl4)
        resources.ApplyResources(Me.pnlFilter, "pnlFilter")
        Me.pnlFilter.Name = "pnlFilter"
        '
        'dtpFromPayDate
        '
        resources.ApplyResources(Me.dtpFromPayDate, "dtpFromPayDate")
        Me.dtpFromPayDate.Name = "dtpFromPayDate"
        Me.dtpFromPayDate.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("dtpFromPayDate.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me.dtpFromPayDate.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("dtpFromPayDate.Properties.CalendarTimeProperties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        '
        'dtpToPayDate
        '
        resources.ApplyResources(Me.dtpToPayDate, "dtpToPayDate")
        Me.dtpToPayDate.Name = "dtpToPayDate"
        Me.dtpToPayDate.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("dtpToPayDate.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me.dtpToPayDate.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("dtpToPayDate.Properties.CalendarTimeProperties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        '
        'btnListAllChqs
        '
        Me.btnListAllChqs.Appearance.Font = CType(resources.GetObject("btnListAllChqs.Appearance.Font"), System.Drawing.Font)
        Me.btnListAllChqs.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.btnListAllChqs, "btnListAllChqs")
        Me.btnListAllChqs.Name = "btnListAllChqs"
        '
        'btnEditAllChqs
        '
        Me.btnEditAllChqs.Appearance.Font = CType(resources.GetObject("btnEditAllChqs.Appearance.Font"), System.Drawing.Font)
        Me.btnEditAllChqs.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.btnEditAllChqs, "btnEditAllChqs")
        Me.btnEditAllChqs.Name = "btnEditAllChqs"
        '
        'btnSrchAllChqs
        '
        Me.btnSrchAllChqs.Appearance.Font = CType(resources.GetObject("btnSrchAllChqs.Appearance.Font"), System.Drawing.Font)
        Me.btnSrchAllChqs.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.btnSrchAllChqs, "btnSrchAllChqs")
        Me.btnSrchAllChqs.Name = "btnSrchAllChqs"
        '
        'txtChqNumSrch
        '
        resources.ApplyResources(Me.txtChqNumSrch, "txtChqNumSrch")
        Me.txtChqNumSrch.Name = "txtChqNumSrch"
        Me.txtChqNumSrch.Properties.Appearance.Options.UseFont = True
        '
        'LabelControl3
        '
        Me.LabelControl3.Appearance.Font = CType(resources.GetObject("LabelControl3.Appearance.Font"), System.Drawing.Font)
        Me.LabelControl3.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.LabelControl3, "LabelControl3")
        Me.LabelControl3.Name = "LabelControl3"
        '
        'txtPatientSrch
        '
        resources.ApplyResources(Me.txtPatientSrch, "txtPatientSrch")
        Me.txtPatientSrch.Name = "txtPatientSrch"
        Me.txtPatientSrch.Properties.Appearance.Options.UseFont = True
        '
        'LabelControl6
        '
        Me.LabelControl6.Appearance.Font = CType(resources.GetObject("LabelControl6.Appearance.Font"), System.Drawing.Font)
        Me.LabelControl6.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.LabelControl6, "LabelControl6")
        Me.LabelControl6.Name = "LabelControl6"
        '
        'LabelControl5
        '
        Me.LabelControl5.Appearance.Font = CType(resources.GetObject("LabelControl5.Appearance.Font"), System.Drawing.Font)
        Me.LabelControl5.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.LabelControl5, "LabelControl5")
        Me.LabelControl5.Name = "LabelControl5"
        '
        'LabelControl4
        '
        Me.LabelControl4.Appearance.Font = CType(resources.GetObject("LabelControl4.Appearance.Font"), System.Drawing.Font)
        Me.LabelControl4.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.LabelControl4, "LabelControl4")
        Me.LabelControl4.Name = "LabelControl4"
        '
        'DueChequesTab
        '
        Me.DueChequesTab.Controls.Add(Me.gridPanel)
        Me.DueChequesTab.Controls.Add(Me.hdrPanel)
        Me.DueChequesTab.Controls.Add(Me.botmPanel)
        Me.DueChequesTab.Name = "DueChequesTab"
        resources.ApplyResources(Me.DueChequesTab, "DueChequesTab")
        '
        'ChequeReturnedTab
        '
        Me.ChequeReturnedTab.Controls.Add(Me.gridReturned)
        Me.ChequeReturnedTab.Controls.Add(Me.ReturnedDetailsPanel)
        Me.ChequeReturnedTab.Controls.Add(Me.ReturnedSearchHeader)
        Me.ChequeReturnedTab.Name = "ChequeReturnedTab"
        resources.ApplyResources(Me.ChequeReturnedTab, "ChequeReturnedTab")
        '
        'gridReturned
        '
        Me.gridReturned.DataSource = Me.ReturnedChqsBS
        resources.ApplyResources(Me.gridReturned, "gridReturned")
        Me.gridReturned.MainView = Me.ReturnedGridView
        Me.gridReturned.Name = "gridReturned"
        Me.gridReturned.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ReturnedGridView})
        '
        'ReturnedGridView
        '
        Me.ReturnedGridView.Appearance.HeaderPanel.Font = CType(resources.GetObject("ReturnedGridView.Appearance.HeaderPanel.Font"), System.Drawing.Font)
        Me.ReturnedGridView.Appearance.HeaderPanel.Options.UseFont = True
        Me.ReturnedGridView.Appearance.Row.Font = CType(resources.GetObject("ReturnedGridView.Appearance.Row.Font"), System.Drawing.Font)
        Me.ReturnedGridView.Appearance.Row.Options.UseFont = True
        Me.ReturnedGridView.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.colReturnedPayID, Me.colReturnedPatientID, Me.colReturnedPatientName, Me.colReturnedChqOwner, Me.colReturnedAccountNumber, Me.colReturnedChqNumber, Me.colReturnedChqBank, Me.colReturnedChqDueDate, Me.colReturnedPayValue, Me.colReturnedPayDate, Me.colReturnedNotes, Me.colReturnedIsCashed, Me.colIsReturned})
        Me.ReturnedGridView.GridControl = Me.gridReturned
        Me.ReturnedGridView.Name = "ReturnedGridView"
        Me.ReturnedGridView.OptionsBehavior.Editable = False
        Me.ReturnedGridView.OptionsView.ShowGroupPanel = False
        '
        'colReturnedPayID
        '
        resources.ApplyResources(Me.colReturnedPayID, "colReturnedPayID")
        Me.colReturnedPayID.FieldName = "PayID"
        Me.colReturnedPayID.Name = "colReturnedPayID"
        '
        'colReturnedPatientID
        '
        resources.ApplyResources(Me.colReturnedPatientID, "colReturnedPatientID")
        Me.colReturnedPatientID.FieldName = "PatientID"
        Me.colReturnedPatientID.Name = "colReturnedPatientID"
        '
        'colReturnedPatientName
        '
        resources.ApplyResources(Me.colReturnedPatientName, "colReturnedPatientName")
        Me.colReturnedPatientName.FieldName = "PatientName"
        Me.colReturnedPatientName.Name = "colReturnedPatientName"
        '
        'colReturnedChqOwner
        '
        resources.ApplyResources(Me.colReturnedChqOwner, "colReturnedChqOwner")
        Me.colReturnedChqOwner.FieldName = "ChqOwner"
        Me.colReturnedChqOwner.Name = "colReturnedChqOwner"
        '
        'colReturnedAccountNumber
        '
        resources.ApplyResources(Me.colReturnedAccountNumber, "colReturnedAccountNumber")
        Me.colReturnedAccountNumber.FieldName = "AccountNumber"
        Me.colReturnedAccountNumber.Name = "colReturnedAccountNumber"
        '
        'colReturnedChqNumber
        '
        resources.ApplyResources(Me.colReturnedChqNumber, "colReturnedChqNumber")
        Me.colReturnedChqNumber.FieldName = "ChqNumber"
        Me.colReturnedChqNumber.Name = "colReturnedChqNumber"
        '
        'colReturnedChqBank
        '
        resources.ApplyResources(Me.colReturnedChqBank, "colReturnedChqBank")
        Me.colReturnedChqBank.FieldName = "ChqBank"
        Me.colReturnedChqBank.Name = "colReturnedChqBank"
        '
        'colReturnedChqDueDate
        '
        resources.ApplyResources(Me.colReturnedChqDueDate, "colReturnedChqDueDate")
        Me.colReturnedChqDueDate.FieldName = "ChqDueDate"
        Me.colReturnedChqDueDate.Name = "colReturnedChqDueDate"
        '
        'colReturnedPayValue
        '
        resources.ApplyResources(Me.colReturnedPayValue, "colReturnedPayValue")
        Me.colReturnedPayValue.FieldName = "PayValue"
        Me.colReturnedPayValue.Name = "colReturnedPayValue"
        '
        'colReturnedPayDate
        '
        resources.ApplyResources(Me.colReturnedPayDate, "colReturnedPayDate")
        Me.colReturnedPayDate.FieldName = "PayDate"
        Me.colReturnedPayDate.Name = "colReturnedPayDate"
        '
        'colReturnedNotes
        '
        resources.ApplyResources(Me.colReturnedNotes, "colReturnedNotes")
        Me.colReturnedNotes.FieldName = "Notes"
        Me.colReturnedNotes.Name = "colReturnedNotes"
        '
        'colReturnedIsCashed
        '
        resources.ApplyResources(Me.colReturnedIsCashed, "colReturnedIsCashed")
        Me.colReturnedIsCashed.FieldName = "IsCashed"
        Me.colReturnedIsCashed.Name = "colReturnedIsCashed"
        '
        'colIsReturned
        '
        resources.ApplyResources(Me.colIsReturned, "colIsReturned")
        Me.colIsReturned.FieldName = "IsReturned"
        Me.colIsReturned.Name = "colIsReturned"
        '
        'ReturnedDetailsPanel
        '
        Me.ReturnedDetailsPanel.Controls.Add(Me.grpPatient)
        Me.ReturnedDetailsPanel.Controls.Add(Me.grpChqDetails)
        resources.ApplyResources(Me.ReturnedDetailsPanel, "ReturnedDetailsPanel")
        Me.ReturnedDetailsPanel.Name = "ReturnedDetailsPanel"
        '
        'grpPatient
        '
        Me.grpPatient.AppearanceCaption.Font = CType(resources.GetObject("grpPatient.AppearanceCaption.Font"), System.Drawing.Font)
        Me.grpPatient.AppearanceCaption.Options.UseFont = True
        Me.grpPatient.Controls.Add(Me.lblPatientNameVal)
        Me.grpPatient.Controls.Add(Me.lblPatientIDVal)
        Me.grpPatient.Controls.Add(Me.lblPatientNameCap)
        Me.grpPatient.Controls.Add(Me.lblPatientIDCap)
        resources.ApplyResources(Me.grpPatient, "grpPatient")
        Me.grpPatient.Name = "grpPatient"
        '
        'lblPatientNameVal
        '
        Me.lblPatientNameVal.Appearance.Font = CType(resources.GetObject("lblPatientNameVal.Appearance.Font"), System.Drawing.Font)
        Me.lblPatientNameVal.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.lblPatientNameVal, "lblPatientNameVal")
        Me.lblPatientNameVal.Name = "lblPatientNameVal"
        '
        'lblPatientIDVal
        '
        Me.lblPatientIDVal.Appearance.Font = CType(resources.GetObject("lblPatientIDVal.Appearance.Font"), System.Drawing.Font)
        Me.lblPatientIDVal.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.lblPatientIDVal, "lblPatientIDVal")
        Me.lblPatientIDVal.Name = "lblPatientIDVal"
        '
        'lblPatientNameCap
        '
        Me.lblPatientNameCap.Appearance.Font = CType(resources.GetObject("lblPatientNameCap.Appearance.Font"), System.Drawing.Font)
        Me.lblPatientNameCap.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.lblPatientNameCap, "lblPatientNameCap")
        Me.lblPatientNameCap.Name = "lblPatientNameCap"
        '
        'lblPatientIDCap
        '
        Me.lblPatientIDCap.Appearance.Font = CType(resources.GetObject("lblPatientIDCap.Appearance.Font"), System.Drawing.Font)
        Me.lblPatientIDCap.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.lblPatientIDCap, "lblPatientIDCap")
        Me.lblPatientIDCap.Name = "lblPatientIDCap"
        '
        'grpChqDetails
        '
        Me.grpChqDetails.AppearanceCaption.Font = CType(resources.GetObject("grpChqDetails.AppearanceCaption.Font"), System.Drawing.Font)
        Me.grpChqDetails.AppearanceCaption.Options.UseFont = True
        Me.grpChqDetails.Controls.Add(Me.lblChqDetailsVal)
        Me.grpChqDetails.Controls.Add(Me.lblChqDetailsCap)
        resources.ApplyResources(Me.grpChqDetails, "grpChqDetails")
        Me.grpChqDetails.Name = "grpChqDetails"
        '
        'lblChqDetailsVal
        '
        Me.lblChqDetailsVal.Appearance.Font = CType(resources.GetObject("lblChqDetailsVal.Appearance.Font"), System.Drawing.Font)
        Me.lblChqDetailsVal.Appearance.Options.UseFont = True
        Me.lblChqDetailsVal.Appearance.Options.UseTextOptions = True
        Me.lblChqDetailsVal.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
        resources.ApplyResources(Me.lblChqDetailsVal, "lblChqDetailsVal")
        Me.lblChqDetailsVal.Name = "lblChqDetailsVal"
        '
        'lblChqDetailsCap
        '
        Me.lblChqDetailsCap.Appearance.Font = CType(resources.GetObject("lblChqDetailsCap.Appearance.Font"), System.Drawing.Font)
        Me.lblChqDetailsCap.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.lblChqDetailsCap, "lblChqDetailsCap")
        Me.lblChqDetailsCap.Name = "lblChqDetailsCap"
        '
        'ReturnedSearchHeader
        '
        Me.ReturnedSearchHeader.Controls.Add(Me.btnListAllReturned)
        Me.ReturnedSearchHeader.Controls.Add(Me.btnSearchReturned)
        Me.ReturnedSearchHeader.Controls.Add(Me.btnEditReturnedCheque)
        Me.ReturnedSearchHeader.Controls.Add(Me.txtSearchAccountNumber)
        Me.ReturnedSearchHeader.Controls.Add(Me.lblAccountNumber)
        Me.ReturnedSearchHeader.Controls.Add(Me.txtSearchChqNumber)
        Me.ReturnedSearchHeader.Controls.Add(Me.lblChqNumber)
        resources.ApplyResources(Me.ReturnedSearchHeader, "ReturnedSearchHeader")
        Me.ReturnedSearchHeader.Name = "ReturnedSearchHeader"
        '
        'btnListAllReturned
        '
        Me.btnListAllReturned.Appearance.Font = CType(resources.GetObject("btnListAllReturned.Appearance.Font"), System.Drawing.Font)
        Me.btnListAllReturned.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.btnListAllReturned, "btnListAllReturned")
        Me.btnListAllReturned.Name = "btnListAllReturned"
        '
        'btnSearchReturned
        '
        Me.btnSearchReturned.Appearance.Font = CType(resources.GetObject("btnSearchReturned.Appearance.Font"), System.Drawing.Font)
        Me.btnSearchReturned.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.btnSearchReturned, "btnSearchReturned")
        Me.btnSearchReturned.Name = "btnSearchReturned"
        '
        'btnEditReturnedCheque
        '
        Me.btnEditReturnedCheque.Appearance.Font = CType(resources.GetObject("btnEditReturnedCheque.Appearance.Font"), System.Drawing.Font)
        Me.btnEditReturnedCheque.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.btnEditReturnedCheque, "btnEditReturnedCheque")
        Me.btnEditReturnedCheque.Name = "btnEditReturnedCheque"
        '
        'txtSearchAccountNumber
        '
        resources.ApplyResources(Me.txtSearchAccountNumber, "txtSearchAccountNumber")
        Me.txtSearchAccountNumber.Name = "txtSearchAccountNumber"
        Me.txtSearchAccountNumber.Properties.Appearance.Options.UseFont = True
        '
        'lblAccountNumber
        '
        Me.lblAccountNumber.Appearance.Font = CType(resources.GetObject("lblAccountNumber.Appearance.Font"), System.Drawing.Font)
        Me.lblAccountNumber.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.lblAccountNumber, "lblAccountNumber")
        Me.lblAccountNumber.Name = "lblAccountNumber"
        '
        'txtSearchChqNumber
        '
        resources.ApplyResources(Me.txtSearchChqNumber, "txtSearchChqNumber")
        Me.txtSearchChqNumber.Name = "txtSearchChqNumber"
        Me.txtSearchChqNumber.Properties.Appearance.Options.UseFont = True
        '
        'lblChqNumber
        '
        Me.lblChqNumber.Appearance.Font = CType(resources.GetObject("lblChqNumber.Appearance.Font"), System.Drawing.Font)
        Me.lblChqNumber.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.lblChqNumber, "lblChqNumber")
        Me.lblChqNumber.Name = "lblChqNumber"
        '
        'Frm5DueChqs
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.CheuqesTab)
        Me.Name = "Frm5DueChqs"
        CType(Me.MainSplit.Panel1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.MainSplit.Panel1.ResumeLayout(False)
        CType(Me.MainSplit.Panel2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.MainSplit.Panel2.ResumeLayout(False)
        Me.MainSplit.Panel2.PerformLayout()
        CType(Me.MainSplit, System.ComponentModel.ISupportInitialize).EndInit()
        Me.MainSplit.ResumeLayout(False)
        CType(Me.AllChqsGrid, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.AllChqsView, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ChqImage.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.radioAllChqsChequeSide.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.hdrPanel, System.ComponentModel.ISupportInitialize).EndInit()
        Me.hdrPanel.ResumeLayout(False)
        Me.hdrPanel.PerformLayout()
        CType(Me.rgDueCheqListMode.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtWorkDays.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.botmPanel, System.ComponentModel.ISupportInitialize).EndInit()
        Me.botmPanel.ResumeLayout(False)
        CType(Me.gridPanel, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gridPanel.ResumeLayout(False)
        CType(Me.chqsGrid, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ChqsBS, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DueChqView, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CheuqesTab, System.ComponentModel.ISupportInitialize).EndInit()
        Me.CheuqesTab.ResumeLayout(False)
        Me.AllChequesTab.ResumeLayout(False)
        CType(Me.pnlFilter, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlFilter.ResumeLayout(False)
        Me.pnlFilter.PerformLayout()
        CType(Me.dtpFromPayDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpFromPayDate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpToPayDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpToPayDate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtChqNumSrch.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtPatientSrch.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.DueChequesTab.ResumeLayout(False)
        Me.ChequeReturnedTab.ResumeLayout(False)
        CType(Me.gridReturned, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ReturnedChqsBS, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ReturnedGridView, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ReturnedDetailsPanel, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ReturnedDetailsPanel.ResumeLayout(False)
        CType(Me.grpPatient, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpPatient.ResumeLayout(False)
        Me.grpPatient.PerformLayout()
        CType(Me.grpChqDetails, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpChqDetails.ResumeLayout(False)
        CType(Me.ReturnedSearchHeader, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ReturnedSearchHeader.ResumeLayout(False)
        Me.ReturnedSearchHeader.PerformLayout()
        CType(Me.txtSearchAccountNumber.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtSearchChqNumber.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents hdrPanel As DevExpress.XtraEditors.PanelControl
    Friend WithEvents botmPanel As DevExpress.XtraEditors.PanelControl
    Friend WithEvents gridPanel As DevExpress.XtraEditors.PanelControl
    Friend WithEvents btnListChqs As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents rgDueCheqListMode As DevExpress.XtraEditors.RadioGroup
    Friend WithEvents lblDueChqListHint As DevExpress.XtraEditors.LabelControl
    Friend WithEvents ChqsBS As BindingSource
    Friend WithEvents chqsGrid As DevExpress.XtraGrid.GridControl
    Friend WithEvents DueChqView As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents colPatientName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colChqOwner As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colAccountNumber As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colChqNumber As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colChqBank As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colChqDueDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colAdjustedDueDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colPayValue As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colPayDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colNotes As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colPayType As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colIsCashed As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colIsForward As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents btnUpdate As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents colPayID As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colPatientID As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents btnUpdateAll As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents CheuqesTab As DevExpress.XtraTab.XtraTabControl
    Friend WithEvents DueChequesTab As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents ChequeReturnedTab As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents ReturnedSearchHeader As DevExpress.XtraEditors.PanelControl
    Friend WithEvents btnListAllReturned As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents lblChqNumber As DevExpress.XtraEditors.LabelControl
    Friend WithEvents txtSearchChqNumber As DevExpress.XtraEditors.TextEdit
    Friend WithEvents lblAccountNumber As DevExpress.XtraEditors.LabelControl
    Friend WithEvents txtSearchAccountNumber As DevExpress.XtraEditors.TextEdit
    Friend WithEvents btnSearchReturned As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnEditReturnedCheque As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnEditDueCheque As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents gridReturned As DevExpress.XtraGrid.GridControl
    Friend WithEvents ReturnedChqsBS As BindingSource
    Friend WithEvents ReturnedGridView As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents colReturnedPayID As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colReturnedPatientID As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colReturnedPatientName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colReturnedChqOwner As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colReturnedAccountNumber As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colReturnedChqNumber As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colReturnedChqBank As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colReturnedChqDueDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colReturnedPayValue As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colReturnedPayDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colReturnedNotes As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colReturnedIsCashed As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ReturnedDetailsPanel As DevExpress.XtraEditors.PanelControl
    Friend WithEvents grpChqDetails As DevExpress.XtraEditors.GroupControl
    Friend WithEvents lblChqDetailsCap As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblChqDetailsVal As DevExpress.XtraEditors.LabelControl
    Friend WithEvents grpPatient As DevExpress.XtraEditors.GroupControl
    Friend WithEvents lblPatientIDCap As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblPatientNameCap As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblPatientIDVal As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblPatientNameVal As DevExpress.XtraEditors.LabelControl
    Friend WithEvents txtWorkDays As DevExpress.XtraEditors.TextEdit
    Friend WithEvents AllChequesTab As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents MainSplit As DevExpress.XtraEditors.SplitContainerControl
    Friend WithEvents AllChqsGrid As DevExpress.XtraGrid.GridControl
    Friend WithEvents AllChqsView As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents ChqImage As DevExpress.XtraEditors.PictureEdit
    Friend WithEvents lblAllChqsChequeScanSide As DevExpress.XtraEditors.LabelControl
    Friend WithEvents radioAllChqsChequeSide As DevExpress.XtraEditors.RadioGroup
    Friend WithEvents pnlFilter As DevExpress.XtraEditors.PanelControl
    Friend WithEvents dtpFromPayDate As DevExpress.XtraEditors.DateEdit
    Friend WithEvents dtpToPayDate As DevExpress.XtraEditors.DateEdit
    Friend WithEvents btnListAllChqs As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnEditAllChqs As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnSrchAllChqs As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents txtChqNumSrch As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl3 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents txtPatientSrch As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl6 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl5 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl4 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents colIsReturned As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colIsReturnedDue As DevExpress.XtraGrid.Columns.GridColumn
End Class
