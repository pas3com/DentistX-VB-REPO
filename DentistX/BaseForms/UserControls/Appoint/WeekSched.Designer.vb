Imports System.Windows.Forms

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class WeekSched
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(WeekSched))
        Me.tlpMain = New DevExpress.Utils.Layout.TablePanel()
        Me.pnlBody = New DevExpress.XtraEditors.PanelControl()
        Me.pnlHeader = New DevExpress.XtraEditors.PanelControl()
        Me.legendPanel = New DevExpress.XtraEditors.PanelControl()
        Me.grpFilters = New DevExpress.XtraEditors.PanelControl()
        Me.filtersTable = New DevExpress.Utils.Layout.TablePanel()
        Me.pnlFilterDoctors = New DevExpress.XtraEditors.PanelControl()
        Me.tblFilterDoctors = New DevExpress.Utils.Layout.TablePanel()
        Me.btnFilterDoctor0 = New DevExpress.XtraEditors.SimpleButton()
        Me.pnlDoctorFilterScroll = New DevExpress.XtraEditors.XtraScrollableControl()
        Me.btnLabs = New DevExpress.XtraEditors.SimpleButton()
        Me.flpDoctorFilterButtons = New System.Windows.Forms.FlowLayoutPanel()
        Me.btnFilterDoctor1 = New DevExpress.XtraEditors.SimpleButton()
        Me.btnFilterDoctor2 = New DevExpress.XtraEditors.SimpleButton()
        Me.btnFilterDoctor3 = New DevExpress.XtraEditors.SimpleButton()
        Me.btnFilterDoctor4 = New DevExpress.XtraEditors.SimpleButton()
        Me.btnFilterDoctor5 = New DevExpress.XtraEditors.SimpleButton()
        Me.btnFilterDoctor6 = New DevExpress.XtraEditors.SimpleButton()
        Me.btnFilterDoctor7 = New DevExpress.XtraEditors.SimpleButton()
        Me.btnFilterDoctor8 = New DevExpress.XtraEditors.SimpleButton()
        Me.lblStartTime = New DevExpress.XtraEditors.LabelControl()
        Me.pnlFilterReasons = New DevExpress.XtraEditors.PanelControl()
        Me.tblFilterReasons = New DevExpress.Utils.Layout.TablePanel()
        Me.btnAllReasons = New DevExpress.XtraEditors.SimpleButton()
        Me.btnPending = New DevExpress.XtraEditors.SimpleButton()
        Me.btnRunning = New DevExpress.XtraEditors.SimpleButton()
        Me.btnCompleted = New DevExpress.XtraEditors.SimpleButton()
        Me.btnCanceled = New DevExpress.XtraEditors.SimpleButton()
        Me.btnPostponed = New DevExpress.XtraEditors.SimpleButton()
        Me.dtStartTime = New DevExpress.XtraEditors.DateEdit()
        Me.pnlFilterPatients = New DevExpress.XtraEditors.PanelControl()
        Me.tblFilterPatients = New DevExpress.Utils.Layout.TablePanel()
        Me.btnAllPatients = New DevExpress.XtraEditors.SimpleButton()
        Me.btnThisPatient = New DevExpress.XtraEditors.SimpleButton()
        Me.includeReasonCheck = New DevExpress.XtraEditors.CheckEdit()
        Me.lblPatients = New DevExpress.XtraEditors.LabelControl()
        Me.btnNextView = New DevExpress.XtraEditors.SimpleButton()
        Me.btnPrevView = New DevExpress.XtraEditors.SimpleButton()
        Me.lblDoc = New DevExpress.XtraEditors.LabelControl()
        Me.lblOptions = New DevExpress.XtraEditors.LabelControl()
        Me.lblStatusFilter = New DevExpress.XtraEditors.LabelControl()
        Me.boldFontCheck = New DevExpress.XtraEditors.CheckEdit()
        Me.sizeFontCheck = New DevExpress.XtraEditors.CheckEdit()
        Me.lblApptsCount = New DevExpress.XtraEditors.LabelControl()
        Me.lblCount = New DevExpress.XtraEditors.LabelControl()
        Me.dtEndTime = New DevExpress.XtraEditors.DateEdit()
        Me.lblEndTime = New DevExpress.XtraEditors.LabelControl()
        Me.pnlButtons = New DevExpress.XtraEditors.PanelControl()
        Me.headerTable = New DevExpress.Utils.Layout.TablePanel()
        Me.pnlHeaderLeft = New DevExpress.XtraEditors.PanelControl()
        Me.lblPatient = New DevExpress.XtraEditors.LabelControl()
        Me.pnlHeaderCenter = New DevExpress.XtraEditors.PanelControl()
        Me.pnlHeaderToolbarFlow = New System.Windows.Forms.FlowLayoutPanel()
        Me.chkUse24 = New DevExpress.XtraEditors.CheckEdit()
        Me.btnAdd = New DevExpress.XtraEditors.SimpleButton()
        Me.btnPrev = New DevExpress.XtraEditors.SimpleButton()
        Me.cmbView = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.btnNext = New DevExpress.XtraEditors.SimpleButton()
        Me.btnToday = New DevExpress.XtraEditors.SimpleButton()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.gotoDate = New DevExpress.XtraEditors.DateEdit()
        Me.btnWeekSnapshot = New DevExpress.XtraEditors.SimpleButton()
        Me.pnlHeaderRight = New DevExpress.XtraEditors.PanelControl()
        Me.lblRange = New DevExpress.XtraEditors.LabelControl()
        CType(Me.tlpMain, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tlpMain.SuspendLayout()
        CType(Me.pnlBody, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pnlHeader, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlHeader.SuspendLayout()
        CType(Me.legendPanel, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.legendPanel.SuspendLayout()
        CType(Me.grpFilters, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpFilters.SuspendLayout()
        CType(Me.filtersTable, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.filtersTable.SuspendLayout()
        CType(Me.pnlFilterDoctors, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlFilterDoctors.SuspendLayout()
        CType(Me.tblFilterDoctors, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tblFilterDoctors.SuspendLayout()
        Me.pnlDoctorFilterScroll.SuspendLayout()
        Me.flpDoctorFilterButtons.SuspendLayout()
        CType(Me.pnlFilterReasons, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlFilterReasons.SuspendLayout()
        CType(Me.tblFilterReasons, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tblFilterReasons.SuspendLayout()
        CType(Me.dtStartTime.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtStartTime.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pnlFilterPatients, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlFilterPatients.SuspendLayout()
        CType(Me.tblFilterPatients, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tblFilterPatients.SuspendLayout()
        CType(Me.includeReasonCheck.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.boldFontCheck.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.sizeFontCheck.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtEndTime.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtEndTime.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pnlButtons, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlButtons.SuspendLayout()
        CType(Me.headerTable, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.headerTable.SuspendLayout()
        CType(Me.pnlHeaderLeft, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlHeaderLeft.SuspendLayout()
        CType(Me.pnlHeaderCenter, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlHeaderCenter.SuspendLayout()
        Me.pnlHeaderToolbarFlow.SuspendLayout()
        CType(Me.chkUse24.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbView.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gotoDate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gotoDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pnlHeaderRight, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlHeaderRight.SuspendLayout()
        Me.SuspendLayout()
        '
        'tlpMain
        '
        resources.ApplyResources(Me.tlpMain, "tlpMain")
        Me.tlpMain.Columns.AddRange(New DevExpress.Utils.Layout.TablePanelColumn() {New DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 50.0!)})
        Me.tlpMain.Controls.Add(Me.pnlBody)
        Me.tlpMain.Controls.Add(Me.pnlHeader)
        Me.tlpMain.Name = "tlpMain"
        Me.tlpMain.Rows.AddRange(New DevExpress.Utils.Layout.TablePanelRow() {New DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 152.0!), New DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 100.0!)})
        '
        'pnlBody
        '
        Me.tlpMain.SetColumn(Me.pnlBody, 0)
        resources.ApplyResources(Me.pnlBody, "pnlBody")
        Me.pnlBody.Name = "pnlBody"
        Me.tlpMain.SetRow(Me.pnlBody, 1)
        '
        'pnlHeader
        '
        Me.tlpMain.SetColumn(Me.pnlHeader, 0)
        Me.pnlHeader.Controls.Add(Me.legendPanel)
        resources.ApplyResources(Me.pnlHeader, "pnlHeader")
        Me.pnlHeader.Name = "pnlHeader"
        Me.tlpMain.SetRow(Me.pnlHeader, 0)
        '
        'legendPanel
        '
        Me.legendPanel.Controls.Add(Me.grpFilters)
        Me.legendPanel.Controls.Add(Me.pnlButtons)
        resources.ApplyResources(Me.legendPanel, "legendPanel")
        Me.legendPanel.Name = "legendPanel"
        '
        'grpFilters
        '
        Me.grpFilters.Controls.Add(Me.filtersTable)
        resources.ApplyResources(Me.grpFilters, "grpFilters")
        Me.grpFilters.Name = "grpFilters"
        '
        'filtersTable
        '
        Me.filtersTable.Columns.AddRange(New DevExpress.Utils.Layout.TablePanelColumn() {New DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 50.0!), New DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 50.0!), New DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 50.0!), New DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 58.1!), New DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 45.14!), New DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 46.76!), New DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 50.0!), New DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 50.0!), New DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 50.0!), New DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 50.0!), New DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 50.0!), New DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 50.0!), New DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 50.0!)})
        Me.filtersTable.Controls.Add(Me.btnLabs)
        Me.filtersTable.Controls.Add(Me.pnlFilterDoctors)
        Me.filtersTable.Controls.Add(Me.btnAdd)
        Me.filtersTable.Controls.Add(Me.lblStartTime)
        Me.filtersTable.Controls.Add(Me.pnlFilterReasons)
        Me.filtersTable.Controls.Add(Me.dtStartTime)
        Me.filtersTable.Controls.Add(Me.pnlFilterPatients)
        Me.filtersTable.Controls.Add(Me.includeReasonCheck)
        Me.filtersTable.Controls.Add(Me.btnWeekSnapshot)
        Me.filtersTable.Controls.Add(Me.lblPatients)
        Me.filtersTable.Controls.Add(Me.btnNextView)
        Me.filtersTable.Controls.Add(Me.btnPrevView)
        Me.filtersTable.Controls.Add(Me.lblDoc)
        Me.filtersTable.Controls.Add(Me.lblOptions)
        Me.filtersTable.Controls.Add(Me.lblStatusFilter)
        Me.filtersTable.Controls.Add(Me.boldFontCheck)
        Me.filtersTable.Controls.Add(Me.sizeFontCheck)
        Me.filtersTable.Controls.Add(Me.lblApptsCount)
        Me.filtersTable.Controls.Add(Me.lblCount)
        Me.filtersTable.Controls.Add(Me.dtEndTime)
        Me.filtersTable.Controls.Add(Me.lblEndTime)
        resources.ApplyResources(Me.filtersTable, "filtersTable")
        Me.filtersTable.Name = "filtersTable"
        Me.filtersTable.Rows.AddRange(New DevExpress.Utils.Layout.TablePanelRow() {New DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 36.53!), New DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 32.65!), New DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 29.82!)})
        '
        'pnlFilterDoctors
        '
        Me.pnlFilterDoctors.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.filtersTable.SetColumn(Me.pnlFilterDoctors, 3)
        Me.filtersTable.SetColumnSpan(Me.pnlFilterDoctors, 9)
        Me.pnlFilterDoctors.Controls.Add(Me.tblFilterDoctors)
        resources.ApplyResources(Me.pnlFilterDoctors, "pnlFilterDoctors")
        Me.pnlFilterDoctors.Name = "pnlFilterDoctors"
        Me.filtersTable.SetRow(Me.pnlFilterDoctors, 1)
        '
        'tblFilterDoctors
        '
        Me.tblFilterDoctors.Columns.AddRange(New DevExpress.Utils.Layout.TablePanelColumn() {New DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 100.0!), New DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 100.0!)})
        Me.tblFilterDoctors.Controls.Add(Me.btnFilterDoctor0)
        Me.tblFilterDoctors.Controls.Add(Me.pnlDoctorFilterScroll)
        resources.ApplyResources(Me.tblFilterDoctors, "tblFilterDoctors")
        Me.tblFilterDoctors.Name = "tblFilterDoctors"
        Me.tblFilterDoctors.Rows.AddRange(New DevExpress.Utils.Layout.TablePanelRow() {New DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 100.0!)})
        '
        'btnFilterDoctor0
        '
        Me.btnFilterDoctor0.Appearance.Font = CType(resources.GetObject("btnFilterDoctor0.Appearance.Font"), System.Drawing.Font)
        Me.btnFilterDoctor0.Appearance.Options.UseFont = True
        Me.btnFilterDoctor0.Appearance.Options.UseTextOptions = True
        Me.btnFilterDoctor0.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.tblFilterDoctors.SetColumn(Me.btnFilterDoctor0, 0)
        resources.ApplyResources(Me.btnFilterDoctor0, "btnFilterDoctor0")
        Me.btnFilterDoctor0.Name = "btnFilterDoctor0"
        Me.tblFilterDoctors.SetRow(Me.btnFilterDoctor0, 0)
        '
        'pnlDoctorFilterScroll
        '
        resources.ApplyResources(Me.pnlDoctorFilterScroll, "pnlDoctorFilterScroll")
        Me.tblFilterDoctors.SetColumn(Me.pnlDoctorFilterScroll, 1)
        Me.pnlDoctorFilterScroll.Controls.Add(Me.flpDoctorFilterButtons)
        Me.pnlDoctorFilterScroll.Name = "pnlDoctorFilterScroll"
        Me.tblFilterDoctors.SetRow(Me.pnlDoctorFilterScroll, 0)
        '
        'btnLabs
        '
        Me.btnLabs.Appearance.Font = CType(resources.GetObject("btnLabs.Appearance.Font"), System.Drawing.Font)
        Me.btnLabs.Appearance.Options.UseFont = True
        Me.filtersTable.SetColumn(Me.btnLabs, 1)
        resources.ApplyResources(Me.btnLabs, "btnLabs")
        Me.btnLabs.Name = "btnLabs"
        Me.filtersTable.SetRow(Me.btnLabs, 2)
        '
        'flpDoctorFilterButtons
        '
        resources.ApplyResources(Me.flpDoctorFilterButtons, "flpDoctorFilterButtons")
        Me.flpDoctorFilterButtons.Controls.Add(Me.btnFilterDoctor1)
        Me.flpDoctorFilterButtons.Controls.Add(Me.btnFilterDoctor2)
        Me.flpDoctorFilterButtons.Controls.Add(Me.btnFilterDoctor3)
        Me.flpDoctorFilterButtons.Controls.Add(Me.btnFilterDoctor4)
        Me.flpDoctorFilterButtons.Controls.Add(Me.btnFilterDoctor5)
        Me.flpDoctorFilterButtons.Controls.Add(Me.btnFilterDoctor6)
        Me.flpDoctorFilterButtons.Controls.Add(Me.btnFilterDoctor7)
        Me.flpDoctorFilterButtons.Controls.Add(Me.btnFilterDoctor8)
        Me.flpDoctorFilterButtons.Name = "flpDoctorFilterButtons"
        '
        'btnFilterDoctor1
        '
        Me.btnFilterDoctor1.Appearance.Font = CType(resources.GetObject("btnFilterDoctor1.Appearance.Font"), System.Drawing.Font)
        Me.btnFilterDoctor1.Appearance.Options.UseFont = True
        Me.btnFilterDoctor1.Appearance.Options.UseTextOptions = True
        Me.btnFilterDoctor1.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        resources.ApplyResources(Me.btnFilterDoctor1, "btnFilterDoctor1")
        Me.btnFilterDoctor1.Name = "btnFilterDoctor1"
        '
        'btnFilterDoctor2
        '
        Me.btnFilterDoctor2.Appearance.Font = CType(resources.GetObject("btnFilterDoctor2.Appearance.Font"), System.Drawing.Font)
        Me.btnFilterDoctor2.Appearance.Options.UseFont = True
        Me.btnFilterDoctor2.Appearance.Options.UseTextOptions = True
        Me.btnFilterDoctor2.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        resources.ApplyResources(Me.btnFilterDoctor2, "btnFilterDoctor2")
        Me.btnFilterDoctor2.Name = "btnFilterDoctor2"
        '
        'btnFilterDoctor3
        '
        Me.btnFilterDoctor3.Appearance.Font = CType(resources.GetObject("btnFilterDoctor3.Appearance.Font"), System.Drawing.Font)
        Me.btnFilterDoctor3.Appearance.Options.UseFont = True
        Me.btnFilterDoctor3.Appearance.Options.UseTextOptions = True
        Me.btnFilterDoctor3.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        resources.ApplyResources(Me.btnFilterDoctor3, "btnFilterDoctor3")
        Me.btnFilterDoctor3.Name = "btnFilterDoctor3"
        '
        'btnFilterDoctor4
        '
        Me.btnFilterDoctor4.Appearance.Font = CType(resources.GetObject("btnFilterDoctor4.Appearance.Font"), System.Drawing.Font)
        Me.btnFilterDoctor4.Appearance.Options.UseFont = True
        Me.btnFilterDoctor4.Appearance.Options.UseTextOptions = True
        Me.btnFilterDoctor4.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        resources.ApplyResources(Me.btnFilterDoctor4, "btnFilterDoctor4")
        Me.btnFilterDoctor4.Name = "btnFilterDoctor4"
        '
        'btnFilterDoctor5
        '
        Me.btnFilterDoctor5.Appearance.Font = CType(resources.GetObject("btnFilterDoctor5.Appearance.Font"), System.Drawing.Font)
        Me.btnFilterDoctor5.Appearance.Options.UseFont = True
        Me.btnFilterDoctor5.Appearance.Options.UseTextOptions = True
        Me.btnFilterDoctor5.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        resources.ApplyResources(Me.btnFilterDoctor5, "btnFilterDoctor5")
        Me.btnFilterDoctor5.Name = "btnFilterDoctor5"
        '
        'btnFilterDoctor6
        '
        Me.btnFilterDoctor6.Appearance.Font = CType(resources.GetObject("btnFilterDoctor6.Appearance.Font"), System.Drawing.Font)
        Me.btnFilterDoctor6.Appearance.Options.UseFont = True
        Me.btnFilterDoctor6.Appearance.Options.UseTextOptions = True
        Me.btnFilterDoctor6.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        resources.ApplyResources(Me.btnFilterDoctor6, "btnFilterDoctor6")
        Me.btnFilterDoctor6.Name = "btnFilterDoctor6"
        '
        'btnFilterDoctor7
        '
        Me.btnFilterDoctor7.Appearance.Font = CType(resources.GetObject("btnFilterDoctor7.Appearance.Font"), System.Drawing.Font)
        Me.btnFilterDoctor7.Appearance.Options.UseFont = True
        Me.btnFilterDoctor7.Appearance.Options.UseTextOptions = True
        Me.btnFilterDoctor7.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        resources.ApplyResources(Me.btnFilterDoctor7, "btnFilterDoctor7")
        Me.btnFilterDoctor7.Name = "btnFilterDoctor7"
        '
        'btnFilterDoctor8
        '
        Me.btnFilterDoctor8.Appearance.Font = CType(resources.GetObject("btnFilterDoctor8.Appearance.Font"), System.Drawing.Font)
        Me.btnFilterDoctor8.Appearance.Options.UseFont = True
        Me.btnFilterDoctor8.Appearance.Options.UseTextOptions = True
        Me.btnFilterDoctor8.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        resources.ApplyResources(Me.btnFilterDoctor8, "btnFilterDoctor8")
        Me.btnFilterDoctor8.Name = "btnFilterDoctor8"
        '
        'lblStartTime
        '
        Me.lblStartTime.Appearance.Font = CType(resources.GetObject("lblStartTime.Appearance.Font"), System.Drawing.Font)
        Me.lblStartTime.Appearance.Options.UseFont = True
        Me.lblStartTime.Appearance.Options.UseTextOptions = True
        Me.lblStartTime.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.filtersTable.SetColumn(Me.lblStartTime, 6)
        resources.ApplyResources(Me.lblStartTime, "lblStartTime")
        Me.lblStartTime.Name = "lblStartTime"
        Me.filtersTable.SetRow(Me.lblStartTime, 2)
        '
        'pnlFilterReasons
        '
        Me.pnlFilterReasons.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.filtersTable.SetColumn(Me.pnlFilterReasons, 6)
        Me.filtersTable.SetColumnSpan(Me.pnlFilterReasons, 6)
        Me.pnlFilterReasons.Controls.Add(Me.tblFilterReasons)
        resources.ApplyResources(Me.pnlFilterReasons, "pnlFilterReasons")
        Me.pnlFilterReasons.Name = "pnlFilterReasons"
        Me.filtersTable.SetRow(Me.pnlFilterReasons, 0)
        '
        'tblFilterReasons
        '
        Me.tblFilterReasons.Columns.AddRange(New DevExpress.Utils.Layout.TablePanelColumn() {New DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 16.67!), New DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 16.67!), New DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 16.67!), New DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 16.67!), New DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 16.67!), New DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 16.65!)})
        Me.tblFilterReasons.Controls.Add(Me.btnAllReasons)
        Me.tblFilterReasons.Controls.Add(Me.btnPending)
        Me.tblFilterReasons.Controls.Add(Me.btnRunning)
        Me.tblFilterReasons.Controls.Add(Me.btnCompleted)
        Me.tblFilterReasons.Controls.Add(Me.btnCanceled)
        Me.tblFilterReasons.Controls.Add(Me.btnPostponed)
        resources.ApplyResources(Me.tblFilterReasons, "tblFilterReasons")
        Me.tblFilterReasons.Name = "tblFilterReasons"
        Me.tblFilterReasons.Rows.AddRange(New DevExpress.Utils.Layout.TablePanelRow() {New DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 100.0!)})
        '
        'btnAllReasons
        '
        Me.btnAllReasons.Appearance.Font = CType(resources.GetObject("btnAllReasons.Appearance.Font"), System.Drawing.Font)
        Me.btnAllReasons.Appearance.Options.UseFont = True
        Me.btnAllReasons.Appearance.Options.UseTextOptions = True
        Me.btnAllReasons.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.tblFilterReasons.SetColumn(Me.btnAllReasons, 0)
        resources.ApplyResources(Me.btnAllReasons, "btnAllReasons")
        Me.btnAllReasons.Name = "btnAllReasons"
        Me.tblFilterReasons.SetRow(Me.btnAllReasons, 0)
        '
        'btnPending
        '
        Me.btnPending.Appearance.Font = CType(resources.GetObject("btnPending.Appearance.Font"), System.Drawing.Font)
        Me.btnPending.Appearance.Options.UseFont = True
        Me.btnPending.Appearance.Options.UseTextOptions = True
        Me.btnPending.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.tblFilterReasons.SetColumn(Me.btnPending, 1)
        resources.ApplyResources(Me.btnPending, "btnPending")
        Me.btnPending.Name = "btnPending"
        Me.tblFilterReasons.SetRow(Me.btnPending, 0)
        '
        'btnRunning
        '
        Me.btnRunning.Appearance.Font = CType(resources.GetObject("btnRunning.Appearance.Font"), System.Drawing.Font)
        Me.btnRunning.Appearance.Options.UseFont = True
        Me.btnRunning.Appearance.Options.UseTextOptions = True
        Me.btnRunning.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.tblFilterReasons.SetColumn(Me.btnRunning, 2)
        resources.ApplyResources(Me.btnRunning, "btnRunning")
        Me.btnRunning.Name = "btnRunning"
        Me.tblFilterReasons.SetRow(Me.btnRunning, 0)
        '
        'btnCompleted
        '
        Me.btnCompleted.Appearance.Font = CType(resources.GetObject("btnCompleted.Appearance.Font"), System.Drawing.Font)
        Me.btnCompleted.Appearance.Options.UseFont = True
        Me.btnCompleted.Appearance.Options.UseTextOptions = True
        Me.btnCompleted.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.tblFilterReasons.SetColumn(Me.btnCompleted, 3)
        resources.ApplyResources(Me.btnCompleted, "btnCompleted")
        Me.btnCompleted.Name = "btnCompleted"
        Me.tblFilterReasons.SetRow(Me.btnCompleted, 0)
        '
        'btnCanceled
        '
        Me.btnCanceled.Appearance.Font = CType(resources.GetObject("btnCanceled.Appearance.Font"), System.Drawing.Font)
        Me.btnCanceled.Appearance.Options.UseFont = True
        Me.btnCanceled.Appearance.Options.UseTextOptions = True
        Me.btnCanceled.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.tblFilterReasons.SetColumn(Me.btnCanceled, 4)
        resources.ApplyResources(Me.btnCanceled, "btnCanceled")
        Me.btnCanceled.Name = "btnCanceled"
        Me.tblFilterReasons.SetRow(Me.btnCanceled, 0)
        '
        'btnPostponed
        '
        Me.btnPostponed.Appearance.Font = CType(resources.GetObject("btnPostponed.Appearance.Font"), System.Drawing.Font)
        Me.btnPostponed.Appearance.Options.UseFont = True
        Me.btnPostponed.Appearance.Options.UseTextOptions = True
        Me.btnPostponed.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.tblFilterReasons.SetColumn(Me.btnPostponed, 5)
        resources.ApplyResources(Me.btnPostponed, "btnPostponed")
        Me.btnPostponed.Name = "btnPostponed"
        Me.tblFilterReasons.SetRow(Me.btnPostponed, 0)
        '
        'dtStartTime
        '
        Me.filtersTable.SetColumn(Me.dtStartTime, 7)
        resources.ApplyResources(Me.dtStartTime, "dtStartTime")
        Me.dtStartTime.Name = "dtStartTime"
        Me.dtStartTime.Properties.Appearance.Font = CType(resources.GetObject("dtStartTime.Properties.Appearance.Font"), System.Drawing.Font)
        Me.dtStartTime.Properties.Appearance.Options.UseFont = True
        Me.dtStartTime.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("dtStartTime.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me.dtStartTime.Properties.CalendarTimeEditing = DevExpress.Utils.DefaultBoolean.[True]
        Me.dtStartTime.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("dtStartTime.Properties.CalendarTimeProperties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me.dtStartTime.Properties.CalendarTimeProperties.DisplayFormat.FormatString = "T"
        Me.dtStartTime.Properties.CalendarTimeProperties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.dtStartTime.Properties.CalendarTimeProperties.EditFormat.FormatString = "T"
        Me.dtStartTime.Properties.CalendarTimeProperties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.dtStartTime.Properties.CalendarView = DevExpress.XtraEditors.Repository.CalendarView.TouchUI
        Me.dtStartTime.Properties.DisplayFormat.FormatString = "t"
        Me.dtStartTime.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.dtStartTime.Properties.EditFormat.FormatString = "t"
        Me.dtStartTime.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.dtStartTime.Properties.MaskSettings.Set("mask", "t")
        Me.dtStartTime.Properties.VistaDisplayMode = DevExpress.Utils.DefaultBoolean.[False]
        Me.filtersTable.SetRow(Me.dtStartTime, 2)
        '
        'pnlFilterPatients
        '
        Me.pnlFilterPatients.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.filtersTable.SetColumn(Me.pnlFilterPatients, 3)
        Me.filtersTable.SetColumnSpan(Me.pnlFilterPatients, 2)
        Me.pnlFilterPatients.Controls.Add(Me.tblFilterPatients)
        resources.ApplyResources(Me.pnlFilterPatients, "pnlFilterPatients")
        Me.pnlFilterPatients.Name = "pnlFilterPatients"
        Me.filtersTable.SetRow(Me.pnlFilterPatients, 0)
        '
        'tblFilterPatients
        '
        Me.tblFilterPatients.Columns.AddRange(New DevExpress.Utils.Layout.TablePanelColumn() {New DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 50.0!), New DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 50.0!)})
        Me.tblFilterPatients.Controls.Add(Me.btnAllPatients)
        Me.tblFilterPatients.Controls.Add(Me.btnThisPatient)
        resources.ApplyResources(Me.tblFilterPatients, "tblFilterPatients")
        Me.tblFilterPatients.Name = "tblFilterPatients"
        Me.tblFilterPatients.Rows.AddRange(New DevExpress.Utils.Layout.TablePanelRow() {New DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 100.0!)})
        '
        'btnAllPatients
        '
        Me.btnAllPatients.Appearance.Font = CType(resources.GetObject("btnAllPatients.Appearance.Font"), System.Drawing.Font)
        Me.btnAllPatients.Appearance.Options.UseFont = True
        Me.btnAllPatients.Appearance.Options.UseTextOptions = True
        Me.btnAllPatients.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.tblFilterPatients.SetColumn(Me.btnAllPatients, 0)
        resources.ApplyResources(Me.btnAllPatients, "btnAllPatients")
        Me.btnAllPatients.Name = "btnAllPatients"
        Me.tblFilterPatients.SetRow(Me.btnAllPatients, 0)
        '
        'btnThisPatient
        '
        Me.btnThisPatient.Appearance.Font = CType(resources.GetObject("btnThisPatient.Appearance.Font"), System.Drawing.Font)
        Me.btnThisPatient.Appearance.Options.UseFont = True
        Me.btnThisPatient.Appearance.Options.UseTextOptions = True
        Me.btnThisPatient.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.tblFilterPatients.SetColumn(Me.btnThisPatient, 1)
        resources.ApplyResources(Me.btnThisPatient, "btnThisPatient")
        Me.btnThisPatient.Name = "btnThisPatient"
        Me.tblFilterPatients.SetRow(Me.btnThisPatient, 0)
        '
        'includeReasonCheck
        '
        Me.filtersTable.SetColumn(Me.includeReasonCheck, 3)
        resources.ApplyResources(Me.includeReasonCheck, "includeReasonCheck")
        Me.includeReasonCheck.Name = "includeReasonCheck"
        Me.includeReasonCheck.Properties.Appearance.Font = CType(resources.GetObject("includeReasonCheck.Properties.Appearance.Font"), System.Drawing.Font)
        Me.includeReasonCheck.Properties.Appearance.Options.UseFont = True
        Me.includeReasonCheck.Properties.Caption = resources.GetString("includeReasonCheck.Properties.Caption")
        Me.filtersTable.SetRow(Me.includeReasonCheck, 2)
        '
        'lblPatients
        '
        Me.lblPatients.Appearance.BackColor = System.Drawing.Color.Blue
        Me.lblPatients.Appearance.Font = CType(resources.GetObject("lblPatients.Appearance.Font"), System.Drawing.Font)
        Me.lblPatients.Appearance.ForeColor = System.Drawing.Color.Transparent
        Me.lblPatients.Appearance.Options.UseBackColor = True
        Me.lblPatients.Appearance.Options.UseFont = True
        Me.lblPatients.Appearance.Options.UseForeColor = True
        Me.lblPatients.Appearance.Options.UseTextOptions = True
        Me.lblPatients.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        resources.ApplyResources(Me.lblPatients, "lblPatients")
        Me.lblPatients.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.filtersTable.SetColumn(Me.lblPatients, 2)
        Me.lblPatients.LineVisible = True
        Me.lblPatients.Name = "lblPatients"
        Me.filtersTable.SetRow(Me.lblPatients, 0)
        '
        'btnNextView
        '
        resources.ApplyResources(Me.btnNextView, "btnNextView")
        Me.btnNextView.Appearance.Font = CType(resources.GetObject("btnNextView.Appearance.Font"), System.Drawing.Font)
        Me.btnNextView.Appearance.Options.UseFont = True
        Me.filtersTable.SetColumn(Me.btnNextView, 12)
        Me.btnNextView.Name = "btnNextView"
        Me.filtersTable.SetRow(Me.btnNextView, 1)
        Me.filtersTable.SetRowSpan(Me.btnNextView, 2)
        '
        'btnPrevView
        '
        resources.ApplyResources(Me.btnPrevView, "btnPrevView")
        Me.btnPrevView.Appearance.Font = CType(resources.GetObject("btnPrevView.Appearance.Font"), System.Drawing.Font)
        Me.btnPrevView.Appearance.Options.UseFont = True
        Me.filtersTable.SetColumn(Me.btnPrevView, 0)
        Me.btnPrevView.Name = "btnPrevView"
        Me.filtersTable.SetRow(Me.btnPrevView, 1)
        Me.filtersTable.SetRowSpan(Me.btnPrevView, 2)
        '
        'lblDoc
        '
        Me.lblDoc.Appearance.BackColor = System.Drawing.Color.Blue
        Me.lblDoc.Appearance.Font = CType(resources.GetObject("lblDoc.Appearance.Font"), System.Drawing.Font)
        Me.lblDoc.Appearance.ForeColor = System.Drawing.Color.Transparent
        Me.lblDoc.Appearance.Options.UseBackColor = True
        Me.lblDoc.Appearance.Options.UseFont = True
        Me.lblDoc.Appearance.Options.UseForeColor = True
        Me.lblDoc.Appearance.Options.UseTextOptions = True
        Me.lblDoc.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        resources.ApplyResources(Me.lblDoc, "lblDoc")
        Me.lblDoc.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.filtersTable.SetColumn(Me.lblDoc, 2)
        Me.lblDoc.LineVisible = True
        Me.lblDoc.Name = "lblDoc"
        Me.filtersTable.SetRow(Me.lblDoc, 1)
        '
        'lblOptions
        '
        Me.lblOptions.Appearance.BackColor = System.Drawing.Color.Blue
        Me.lblOptions.Appearance.Font = CType(resources.GetObject("lblOptions.Appearance.Font"), System.Drawing.Font)
        Me.lblOptions.Appearance.ForeColor = System.Drawing.Color.Transparent
        Me.lblOptions.Appearance.Options.UseBackColor = True
        Me.lblOptions.Appearance.Options.UseFont = True
        Me.lblOptions.Appearance.Options.UseForeColor = True
        Me.lblOptions.Appearance.Options.UseTextOptions = True
        Me.lblOptions.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        resources.ApplyResources(Me.lblOptions, "lblOptions")
        Me.lblOptions.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.filtersTable.SetColumn(Me.lblOptions, 2)
        Me.lblOptions.LineVisible = True
        Me.lblOptions.Name = "lblOptions"
        Me.filtersTable.SetRow(Me.lblOptions, 2)
        '
        'lblStatusFilter
        '
        Me.lblStatusFilter.Appearance.BackColor = System.Drawing.Color.Blue
        Me.lblStatusFilter.Appearance.Font = CType(resources.GetObject("lblStatusFilter.Appearance.Font"), System.Drawing.Font)
        Me.lblStatusFilter.Appearance.ForeColor = System.Drawing.Color.Transparent
        Me.lblStatusFilter.Appearance.Options.UseBackColor = True
        Me.lblStatusFilter.Appearance.Options.UseFont = True
        Me.lblStatusFilter.Appearance.Options.UseForeColor = True
        Me.lblStatusFilter.Appearance.Options.UseTextOptions = True
        Me.lblStatusFilter.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.lblStatusFilter.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.filtersTable.SetColumn(Me.lblStatusFilter, 5)
        resources.ApplyResources(Me.lblStatusFilter, "lblStatusFilter")
        Me.lblStatusFilter.LineVisible = True
        Me.lblStatusFilter.Name = "lblStatusFilter"
        Me.filtersTable.SetRow(Me.lblStatusFilter, 0)
        '
        'boldFontCheck
        '
        Me.filtersTable.SetColumn(Me.boldFontCheck, 4)
        resources.ApplyResources(Me.boldFontCheck, "boldFontCheck")
        Me.boldFontCheck.Name = "boldFontCheck"
        Me.boldFontCheck.Properties.Appearance.Font = CType(resources.GetObject("boldFontCheck.Properties.Appearance.Font"), System.Drawing.Font)
        Me.boldFontCheck.Properties.Appearance.Options.UseFont = True
        Me.boldFontCheck.Properties.Caption = resources.GetString("boldFontCheck.Properties.Caption")
        Me.filtersTable.SetRow(Me.boldFontCheck, 2)
        '
        'sizeFontCheck
        '
        Me.filtersTable.SetColumn(Me.sizeFontCheck, 5)
        resources.ApplyResources(Me.sizeFontCheck, "sizeFontCheck")
        Me.sizeFontCheck.Name = "sizeFontCheck"
        Me.sizeFontCheck.Properties.Appearance.Font = CType(resources.GetObject("sizeFontCheck.Properties.Appearance.Font"), System.Drawing.Font)
        Me.sizeFontCheck.Properties.Appearance.Options.UseFont = True
        Me.sizeFontCheck.Properties.Caption = resources.GetString("sizeFontCheck.Properties.Caption")
        Me.filtersTable.SetRow(Me.sizeFontCheck, 2)
        '
        'lblApptsCount
        '
        Me.lblApptsCount.Appearance.Font = CType(resources.GetObject("lblApptsCount.Appearance.Font"), System.Drawing.Font)
        Me.lblApptsCount.Appearance.Options.UseFont = True
        Me.lblApptsCount.Appearance.Options.UseTextOptions = True
        Me.lblApptsCount.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        resources.ApplyResources(Me.lblApptsCount, "lblApptsCount")
        Me.lblApptsCount.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.filtersTable.SetColumn(Me.lblApptsCount, 10)
        Me.lblApptsCount.Name = "lblApptsCount"
        Me.filtersTable.SetRow(Me.lblApptsCount, 2)
        '
        'lblCount
        '
        Me.lblCount.Appearance.Font = CType(resources.GetObject("lblCount.Appearance.Font"), System.Drawing.Font)
        Me.lblCount.Appearance.Options.UseFont = True
        Me.lblCount.Appearance.Options.UseTextOptions = True
        Me.lblCount.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.filtersTable.SetColumn(Me.lblCount, 11)
        resources.ApplyResources(Me.lblCount, "lblCount")
        Me.lblCount.Name = "lblCount"
        Me.filtersTable.SetRow(Me.lblCount, 2)
        '
        'dtEndTime
        '
        Me.filtersTable.SetColumn(Me.dtEndTime, 9)
        resources.ApplyResources(Me.dtEndTime, "dtEndTime")
        Me.dtEndTime.Name = "dtEndTime"
        Me.dtEndTime.Properties.Appearance.Font = CType(resources.GetObject("dtEndTime.Properties.Appearance.Font"), System.Drawing.Font)
        Me.dtEndTime.Properties.Appearance.Options.UseFont = True
        Me.dtEndTime.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("dtEndTime.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me.dtEndTime.Properties.CalendarTimeEditing = DevExpress.Utils.DefaultBoolean.[True]
        Me.dtEndTime.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("dtEndTime.Properties.CalendarTimeProperties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me.dtEndTime.Properties.CalendarTimeProperties.DisplayFormat.FormatString = "T"
        Me.dtEndTime.Properties.CalendarTimeProperties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.dtEndTime.Properties.CalendarTimeProperties.EditFormat.FormatString = "T"
        Me.dtEndTime.Properties.CalendarTimeProperties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.dtEndTime.Properties.CalendarView = DevExpress.XtraEditors.Repository.CalendarView.TouchUI
        Me.dtEndTime.Properties.DisplayFormat.FormatString = "t"
        Me.dtEndTime.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.dtEndTime.Properties.EditFormat.FormatString = "t"
        Me.dtEndTime.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.dtEndTime.Properties.MaskSettings.Set("mask", "t")
        Me.dtEndTime.Properties.VistaDisplayMode = DevExpress.Utils.DefaultBoolean.[False]
        Me.filtersTable.SetRow(Me.dtEndTime, 2)
        '
        'lblEndTime
        '
        Me.lblEndTime.Appearance.Font = CType(resources.GetObject("lblEndTime.Appearance.Font"), System.Drawing.Font)
        Me.lblEndTime.Appearance.Options.UseFont = True
        Me.lblEndTime.Appearance.Options.UseTextOptions = True
        Me.lblEndTime.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.filtersTable.SetColumn(Me.lblEndTime, 8)
        resources.ApplyResources(Me.lblEndTime, "lblEndTime")
        Me.lblEndTime.Name = "lblEndTime"
        Me.filtersTable.SetRow(Me.lblEndTime, 2)
        '
        'pnlButtons
        '
        Me.pnlButtons.Controls.Add(Me.headerTable)
        resources.ApplyResources(Me.pnlButtons, "pnlButtons")
        Me.pnlButtons.Name = "pnlButtons"
        '
        'headerTable
        '
        Me.headerTable.Columns.AddRange(New DevExpress.Utils.Layout.TablePanelColumn() {New DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 18.21!), New DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 64.66!), New DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 17.13!)})
        Me.headerTable.Controls.Add(Me.pnlHeaderLeft)
        Me.headerTable.Controls.Add(Me.pnlHeaderCenter)
        Me.headerTable.Controls.Add(Me.pnlHeaderRight)
        resources.ApplyResources(Me.headerTable, "headerTable")
        Me.headerTable.Name = "headerTable"
        Me.headerTable.Rows.AddRange(New DevExpress.Utils.Layout.TablePanelRow() {New DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 52.0!)})
        '
        'pnlHeaderLeft
        '
        Me.pnlHeaderLeft.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.headerTable.SetColumn(Me.pnlHeaderLeft, 0)
        Me.pnlHeaderLeft.Controls.Add(Me.lblPatient)
        resources.ApplyResources(Me.pnlHeaderLeft, "pnlHeaderLeft")
        Me.pnlHeaderLeft.Name = "pnlHeaderLeft"
        Me.headerTable.SetRow(Me.pnlHeaderLeft, 0)
        '
        'lblPatient
        '
        Me.lblPatient.Appearance.Font = CType(resources.GetObject("lblPatient.Appearance.Font"), System.Drawing.Font)
        Me.lblPatient.Appearance.Options.UseFont = True
        Me.lblPatient.Appearance.Options.UseTextOptions = True
        Me.lblPatient.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
        Me.lblPatient.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        resources.ApplyResources(Me.lblPatient, "lblPatient")
        Me.lblPatient.Name = "lblPatient"
        '
        'pnlHeaderCenter
        '
        Me.pnlHeaderCenter.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.headerTable.SetColumn(Me.pnlHeaderCenter, 1)
        Me.pnlHeaderCenter.Controls.Add(Me.pnlHeaderToolbarFlow)
        resources.ApplyResources(Me.pnlHeaderCenter, "pnlHeaderCenter")
        Me.pnlHeaderCenter.Name = "pnlHeaderCenter"
        Me.headerTable.SetRow(Me.pnlHeaderCenter, 0)
        '
        'pnlHeaderToolbarFlow
        '
        resources.ApplyResources(Me.pnlHeaderToolbarFlow, "pnlHeaderToolbarFlow")
        Me.pnlHeaderToolbarFlow.BackColor = System.Drawing.Color.Transparent
        Me.pnlHeaderToolbarFlow.Controls.Add(Me.chkUse24)
        Me.pnlHeaderToolbarFlow.Controls.Add(Me.btnPrev)
        Me.pnlHeaderToolbarFlow.Controls.Add(Me.cmbView)
        Me.pnlHeaderToolbarFlow.Controls.Add(Me.btnNext)
        Me.pnlHeaderToolbarFlow.Controls.Add(Me.btnToday)
        Me.pnlHeaderToolbarFlow.Controls.Add(Me.LabelControl1)
        Me.pnlHeaderToolbarFlow.Controls.Add(Me.gotoDate)
        Me.pnlHeaderToolbarFlow.Name = "pnlHeaderToolbarFlow"
        '
        'chkUse24
        '
        resources.ApplyResources(Me.chkUse24, "chkUse24")
        Me.chkUse24.Name = "chkUse24"
        Me.chkUse24.Properties.Appearance.Font = CType(resources.GetObject("chkUse24.Properties.Appearance.Font"), System.Drawing.Font)
        Me.chkUse24.Properties.Appearance.Options.UseFont = True
        Me.chkUse24.Properties.Caption = resources.GetString("chkUse24.Properties.Caption")
        '
        'btnAdd
        '
        resources.ApplyResources(Me.btnAdd, "btnAdd")
        Me.btnAdd.Appearance.Font = CType(resources.GetObject("btnAdd.Appearance.Font"), System.Drawing.Font)
        Me.btnAdd.Appearance.ForeColor = System.Drawing.Color.Red
        Me.btnAdd.Appearance.Options.UseFont = True
        Me.btnAdd.Appearance.Options.UseForeColor = True
        Me.filtersTable.SetColumn(Me.btnAdd, 1)
        Me.btnAdd.Name = "btnAdd"
        Me.filtersTable.SetRow(Me.btnAdd, 0)
        '
        'btnPrev
        '
        resources.ApplyResources(Me.btnPrev, "btnPrev")
        Me.btnPrev.Appearance.Font = CType(resources.GetObject("btnPrev.Appearance.Font"), System.Drawing.Font)
        Me.btnPrev.Appearance.Options.UseFont = True
        Me.btnPrev.Name = "btnPrev"
        '
        'cmbView
        '
        resources.ApplyResources(Me.cmbView, "cmbView")
        Me.cmbView.Name = "cmbView"
        Me.cmbView.Properties.Appearance.Font = CType(resources.GetObject("cmbView.Properties.Appearance.Font"), System.Drawing.Font)
        Me.cmbView.Properties.Appearance.Options.UseFont = True
        Me.cmbView.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("cmbView.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me.cmbView.Properties.Items.AddRange(New Object() {resources.GetString("cmbView.Properties.Items"), resources.GetString("cmbView.Properties.Items1"), resources.GetString("cmbView.Properties.Items2"), resources.GetString("cmbView.Properties.Items3"), resources.GetString("cmbView.Properties.Items4"), resources.GetString("cmbView.Properties.Items5"), resources.GetString("cmbView.Properties.Items6")})
        '
        'btnNext
        '
        resources.ApplyResources(Me.btnNext, "btnNext")
        Me.btnNext.Appearance.Font = CType(resources.GetObject("btnNext.Appearance.Font"), System.Drawing.Font)
        Me.btnNext.Appearance.Options.UseFont = True
        Me.btnNext.Name = "btnNext"
        '
        'btnToday
        '
        resources.ApplyResources(Me.btnToday, "btnToday")
        Me.btnToday.Appearance.Font = CType(resources.GetObject("btnToday.Appearance.Font"), System.Drawing.Font)
        Me.btnToday.Appearance.Options.UseFont = True
        Me.btnToday.Name = "btnToday"
        '
        'LabelControl1
        '
        Me.LabelControl1.Appearance.Font = CType(resources.GetObject("LabelControl1.Appearance.Font"), System.Drawing.Font)
        Me.LabelControl1.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.LabelControl1, "LabelControl1")
        Me.LabelControl1.Name = "LabelControl1"
        '
        'gotoDate
        '
        resources.ApplyResources(Me.gotoDate, "gotoDate")
        Me.gotoDate.Name = "gotoDate"
        Me.gotoDate.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("gotoDate.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me.gotoDate.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("gotoDate.Properties.CalendarTimeProperties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        '
        'btnWeekSnapshot
        '
        Me.btnWeekSnapshot.Appearance.Font = CType(resources.GetObject("btnWeekSnapshot.Appearance.Font"), System.Drawing.Font)
        Me.btnWeekSnapshot.Appearance.Options.UseFont = True
        Me.filtersTable.SetColumn(Me.btnWeekSnapshot, 1)
        resources.ApplyResources(Me.btnWeekSnapshot, "btnWeekSnapshot")
        Me.btnWeekSnapshot.Name = "btnWeekSnapshot"
        Me.filtersTable.SetRow(Me.btnWeekSnapshot, 1)
        '
        'pnlHeaderRight
        '
        Me.pnlHeaderRight.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.headerTable.SetColumn(Me.pnlHeaderRight, 2)
        Me.pnlHeaderRight.Controls.Add(Me.lblRange)
        resources.ApplyResources(Me.pnlHeaderRight, "pnlHeaderRight")
        Me.pnlHeaderRight.Name = "pnlHeaderRight"
        Me.headerTable.SetRow(Me.pnlHeaderRight, 0)
        '
        'lblRange
        '
        Me.lblRange.Appearance.Font = CType(resources.GetObject("lblRange.Appearance.Font"), System.Drawing.Font)
        Me.lblRange.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(237, Byte), Integer), CType(CType(44, Byte), Integer), CType(CType(146, Byte), Integer))
        Me.lblRange.Appearance.Options.UseFont = True
        Me.lblRange.Appearance.Options.UseForeColor = True
        Me.lblRange.Appearance.Options.UseTextOptions = True
        Me.lblRange.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.lblRange.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.lblRange.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap
        resources.ApplyResources(Me.lblRange, "lblRange")
        Me.lblRange.Name = "lblRange"
        '
        'WeekSched
        '
        Me.Controls.Add(Me.tlpMain)
        Me.Name = "WeekSched"
        resources.ApplyResources(Me, "$this")
        CType(Me.tlpMain, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tlpMain.ResumeLayout(False)
        CType(Me.pnlBody, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pnlHeader, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlHeader.ResumeLayout(False)
        CType(Me.legendPanel, System.ComponentModel.ISupportInitialize).EndInit()
        Me.legendPanel.ResumeLayout(False)
        CType(Me.grpFilters, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpFilters.ResumeLayout(False)
        CType(Me.filtersTable, System.ComponentModel.ISupportInitialize).EndInit()
        Me.filtersTable.ResumeLayout(False)
        Me.filtersTable.PerformLayout()
        CType(Me.pnlFilterDoctors, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlFilterDoctors.ResumeLayout(False)
        CType(Me.tblFilterDoctors, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tblFilterDoctors.ResumeLayout(False)
        Me.pnlDoctorFilterScroll.ResumeLayout(False)
        Me.pnlDoctorFilterScroll.PerformLayout()
        Me.flpDoctorFilterButtons.ResumeLayout(False)
        CType(Me.pnlFilterReasons, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlFilterReasons.ResumeLayout(False)
        CType(Me.tblFilterReasons, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tblFilterReasons.ResumeLayout(False)
        CType(Me.dtStartTime.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtStartTime.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pnlFilterPatients, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlFilterPatients.ResumeLayout(False)
        CType(Me.tblFilterPatients, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tblFilterPatients.ResumeLayout(False)
        CType(Me.includeReasonCheck.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.boldFontCheck.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.sizeFontCheck.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtEndTime.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtEndTime.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pnlButtons, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlButtons.ResumeLayout(False)
        CType(Me.headerTable, System.ComponentModel.ISupportInitialize).EndInit()
        Me.headerTable.ResumeLayout(False)
        CType(Me.pnlHeaderLeft, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlHeaderLeft.ResumeLayout(False)
        CType(Me.pnlHeaderCenter, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlHeaderCenter.ResumeLayout(False)
        Me.pnlHeaderCenter.PerformLayout()
        Me.pnlHeaderToolbarFlow.ResumeLayout(False)
        Me.pnlHeaderToolbarFlow.PerformLayout()
        CType(Me.chkUse24.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbView.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gotoDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gotoDate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pnlHeaderRight, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlHeaderRight.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents tlpMain As DevExpress.Utils.Layout.TablePanel
    Friend WithEvents pnlHeader As DevExpress.XtraEditors.PanelControl
    Friend WithEvents pnlBody As DevExpress.XtraEditors.PanelControl
    Friend WithEvents btnAdd As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents legendPanel As DevExpress.XtraEditors.PanelControl
    Friend WithEvents lblRange As DevExpress.XtraEditors.LabelControl
    Friend WithEvents cmbView As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents btnNext As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnPrev As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnToday As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents chkUse24 As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents lblPatient As DevExpress.XtraEditors.LabelControl
    Friend WithEvents pnlButtons As DevExpress.XtraEditors.PanelControl
    Friend WithEvents gotoDate As DevExpress.XtraEditors.DateEdit
    Friend WithEvents btnWeekSnapshot As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents grpFilters As DevExpress.XtraEditors.PanelControl
    Friend WithEvents filtersTable As DevExpress.Utils.Layout.TablePanel
    Friend WithEvents btnNextView As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnPrevView As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents lblPatients As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblDoc As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblOptions As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblStatusFilter As DevExpress.XtraEditors.LabelControl
    Friend WithEvents includeReasonCheck As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents boldFontCheck As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents sizeFontCheck As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents lblApptsCount As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblCount As DevExpress.XtraEditors.LabelControl
    Friend WithEvents headerTable As DevExpress.Utils.Layout.TablePanel
    Friend WithEvents pnlHeaderLeft As DevExpress.XtraEditors.PanelControl
    Friend WithEvents pnlHeaderCenter As DevExpress.XtraEditors.PanelControl
    Friend WithEvents pnlHeaderRight As DevExpress.XtraEditors.PanelControl
    Friend WithEvents pnlHeaderToolbarFlow As FlowLayoutPanel
    Friend WithEvents lblStartTime As DevExpress.XtraEditors.LabelControl
    Friend WithEvents dtStartTime As DevExpress.XtraEditors.DateEdit
    Friend WithEvents dtEndTime As DevExpress.XtraEditors.DateEdit
    Friend WithEvents lblEndTime As DevExpress.XtraEditors.LabelControl
    Friend WithEvents pnlFilterPatients As DevExpress.XtraEditors.PanelControl
    Friend WithEvents tblFilterPatients As DevExpress.Utils.Layout.TablePanel
    Friend WithEvents btnAllPatients As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnThisPatient As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents pnlFilterReasons As DevExpress.XtraEditors.PanelControl
    Friend WithEvents tblFilterReasons As DevExpress.Utils.Layout.TablePanel
    Friend WithEvents btnAllReasons As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnPending As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnRunning As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnCompleted As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnCanceled As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnPostponed As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents pnlFilterDoctors As DevExpress.XtraEditors.PanelControl
    Friend WithEvents tblFilterDoctors As DevExpress.Utils.Layout.TablePanel
    Friend WithEvents pnlDoctorFilterScroll As DevExpress.XtraEditors.XtraScrollableControl
    Friend WithEvents flpDoctorFilterButtons As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents btnFilterDoctor0 As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnFilterDoctor1 As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnFilterDoctor2 As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnFilterDoctor3 As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnFilterDoctor4 As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnFilterDoctor5 As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnFilterDoctor6 As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnFilterDoctor7 As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnFilterDoctor8 As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnLabs As DevExpress.XtraEditors.SimpleButton
End Class
