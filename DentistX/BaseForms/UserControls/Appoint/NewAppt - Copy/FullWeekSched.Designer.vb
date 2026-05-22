Imports System.Windows.Forms

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FullWeekSched
    Inherits DevExpress.XtraEditors.XtraUserControl

    'UserControl overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            DisposeChromeResizeDebouncer()
            If components IsNot Nothing Then
                components.Dispose()
            End If
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FullWeekSched))
        Me.pnlHeader = New DevExpress.XtraEditors.PanelControl()
        Me.legendPanel = New DevExpress.XtraEditors.PanelControl()
        Me.grpFilters = New DevExpress.XtraEditors.PanelControl()
        Me.filtersTable = New DevExpress.Utils.Layout.TablePanel()
        Me.btnFilterDoctor0 = New DevExpress.XtraEditors.SimpleButton()
        Me.lookUpDoctors = New DevExpress.XtraEditors.LookUpEdit()
        Me.btnThisPatient = New DevExpress.XtraEditors.SimpleButton()
        Me.btnAllPatients = New DevExpress.XtraEditors.SimpleButton()
        Me.lblStatusFilter = New DevExpress.XtraEditors.LabelControl()
        Me.btnAllReasons = New DevExpress.XtraEditors.SimpleButton()
        Me.lblCount = New DevExpress.XtraEditors.LabelControl()
        Me.LookUpStatus = New DevExpress.XtraEditors.LookUpEdit()
        Me.lblDoc = New DevExpress.XtraEditors.LabelControl()
        Me.lblApptsCount = New DevExpress.XtraEditors.LabelControl()
        Me.btnLabelsColors = New DevExpress.XtraEditors.SimpleButton()
        Me.btnLabs = New DevExpress.XtraEditors.SimpleButton()
        Me.lblEndTime = New DevExpress.XtraEditors.LabelControl()
        Me.lblStartTime = New DevExpress.XtraEditors.LabelControl()
        Me.endColor = New DevExpress.XtraEditors.ColorEdit()
        Me.LookUpPatient = New DevExpress.XtraEditors.LookUpEdit()
        Me.dtEndTime = New DevExpress.XtraEditors.DateEdit()
        Me.btnAdd = New DevExpress.XtraEditors.SimpleButton()
        Me.startColor = New DevExpress.XtraEditors.ColorEdit()
        Me.lblOptions = New DevExpress.XtraEditors.LabelControl()
        Me.dtStartTime = New DevExpress.XtraEditors.DateEdit()
        Me.includeReasonCheck = New DevExpress.XtraEditors.CheckEdit()
        Me.sizeFontCheck = New DevExpress.XtraEditors.CheckEdit()
        Me.boldFontCheck = New DevExpress.XtraEditors.CheckEdit()
        Me.lblPatients = New DevExpress.XtraEditors.LabelControl()
        Me.btnNextView = New DevExpress.XtraEditors.SimpleButton()
        Me.btnPrevView = New DevExpress.XtraEditors.SimpleButton()
        Me.pnlButtons = New DevExpress.XtraEditors.PanelControl()
        Me.headerTable = New DevExpress.Utils.Layout.TablePanel()
        Me.btnWeekSnapshot = New DevExpress.XtraEditors.SimpleButton()
        Me.btnToday = New DevExpress.XtraEditors.SimpleButton()
        Me.btnNext = New DevExpress.XtraEditors.SimpleButton()
        Me.btnPrev = New DevExpress.XtraEditors.SimpleButton()
        Me.chkUse24 = New DevExpress.XtraEditors.CheckEdit()
        Me.pnlHeaderLeft = New DevExpress.XtraEditors.PanelControl()
        Me.lblPatient = New DevExpress.XtraEditors.LabelControl()
        Me.pnlHeaderRight = New DevExpress.XtraEditors.PanelControl()
        Me.lblRange = New DevExpress.XtraEditors.LabelControl()
        Me.cmbView = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.gotoDate = New DevExpress.XtraEditors.DateEdit()
        CType(Me.pnlHeader, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlHeader.SuspendLayout()
        CType(Me.legendPanel, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.legendPanel.SuspendLayout()
        CType(Me.grpFilters, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpFilters.SuspendLayout()
        CType(Me.filtersTable, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.filtersTable.SuspendLayout()
        CType(Me.lookUpDoctors.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LookUpStatus.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.endColor.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LookUpPatient.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtEndTime.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtEndTime.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.startColor.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtStartTime.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtStartTime.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.includeReasonCheck.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.sizeFontCheck.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.boldFontCheck.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pnlButtons, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlButtons.SuspendLayout()
        CType(Me.headerTable, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.headerTable.SuspendLayout()
        CType(Me.chkUse24.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pnlHeaderLeft, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlHeaderLeft.SuspendLayout()
        CType(Me.pnlHeaderRight, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlHeaderRight.SuspendLayout()
        CType(Me.cmbView.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gotoDate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gotoDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pnlHeader
        '
        Me.pnlHeader.Controls.Add(Me.legendPanel)
        resources.ApplyResources(Me.pnlHeader, "pnlHeader")
        Me.pnlHeader.Name = "pnlHeader"
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
        Me.filtersTable.Columns.AddRange(New DevExpress.Utils.Layout.TablePanelColumn() {New DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 50.0!), New DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 50.0!), New DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 50.0!), New DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 58.1!), New DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 45.14!), New DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 46.76!), New DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 50.0!), New DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 50.0!), New DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 50.0!), New DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 50.0!), New DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 50.0!), New DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 50.0!), New DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 50.0!), New DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 50.0!), New DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 50.0!), New DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 50.0!)})
        Me.filtersTable.Controls.Add(Me.btnFilterDoctor0)
        Me.filtersTable.Controls.Add(Me.lookUpDoctors)
        Me.filtersTable.Controls.Add(Me.btnThisPatient)
        Me.filtersTable.Controls.Add(Me.btnAllPatients)
        Me.filtersTable.Controls.Add(Me.lblStatusFilter)
        Me.filtersTable.Controls.Add(Me.btnAllReasons)
        Me.filtersTable.Controls.Add(Me.lblCount)
        Me.filtersTable.Controls.Add(Me.LookUpStatus)
        Me.filtersTable.Controls.Add(Me.lblDoc)
        Me.filtersTable.Controls.Add(Me.lblApptsCount)
        Me.filtersTable.Controls.Add(Me.btnLabelsColors)
        Me.filtersTable.Controls.Add(Me.btnLabs)
        Me.filtersTable.Controls.Add(Me.lblEndTime)
        Me.filtersTable.Controls.Add(Me.lblStartTime)
        Me.filtersTable.Controls.Add(Me.endColor)
        Me.filtersTable.Controls.Add(Me.LookUpPatient)
        Me.filtersTable.Controls.Add(Me.dtEndTime)
        Me.filtersTable.Controls.Add(Me.btnAdd)
        Me.filtersTable.Controls.Add(Me.startColor)
        Me.filtersTable.Controls.Add(Me.lblOptions)
        Me.filtersTable.Controls.Add(Me.dtStartTime)
        Me.filtersTable.Controls.Add(Me.includeReasonCheck)
        Me.filtersTable.Controls.Add(Me.sizeFontCheck)
        Me.filtersTable.Controls.Add(Me.boldFontCheck)
        Me.filtersTable.Controls.Add(Me.lblPatients)
        Me.filtersTable.Controls.Add(Me.btnNextView)
        Me.filtersTable.Controls.Add(Me.btnPrevView)
        resources.ApplyResources(Me.filtersTable, "filtersTable")
        Me.filtersTable.Name = "filtersTable"
        Me.filtersTable.Rows.AddRange(New DevExpress.Utils.Layout.TablePanelRow() {New DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 32.71!), New DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 40.67!)})
        '
        'btnFilterDoctor0
        '
        Me.btnFilterDoctor0.Appearance.Font = CType(resources.GetObject("btnFilterDoctor0.Appearance.Font"), System.Drawing.Font)
        Me.btnFilterDoctor0.Appearance.Options.UseFont = True
        Me.btnFilterDoctor0.Appearance.Options.UseTextOptions = True
        Me.btnFilterDoctor0.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.filtersTable.SetColumn(Me.btnFilterDoctor0, 14)
        resources.ApplyResources(Me.btnFilterDoctor0, "btnFilterDoctor0")
        Me.btnFilterDoctor0.Name = "btnFilterDoctor0"
        Me.filtersTable.SetRow(Me.btnFilterDoctor0, 0)
        '
        'lookUpDoctors
        '
        resources.ApplyResources(Me.lookUpDoctors, "lookUpDoctors")
        Me.filtersTable.SetColumn(Me.lookUpDoctors, 7)
        Me.filtersTable.SetColumnSpan(Me.lookUpDoctors, 2)
        Me.lookUpDoctors.Name = "lookUpDoctors"
        Me.lookUpDoctors.Properties.Appearance.Font = CType(resources.GetObject("lookUpDoctors.Properties.Appearance.Font"), System.Drawing.Font)
        Me.lookUpDoctors.Properties.Appearance.Options.UseFont = True
        Me.lookUpDoctors.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("lookUpDoctors.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me.lookUpDoctors.Properties.NullText = resources.GetString("lookUpDoctors.Properties.NullText")
        Me.filtersTable.SetRow(Me.lookUpDoctors, 0)
        '
        'btnThisPatient
        '
        Me.btnThisPatient.Appearance.Font = CType(resources.GetObject("btnThisPatient.Appearance.Font"), System.Drawing.Font)
        Me.btnThisPatient.Appearance.Options.UseFont = True
        Me.btnThisPatient.Appearance.Options.UseTextOptions = True
        Me.btnThisPatient.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.filtersTable.SetColumn(Me.btnThisPatient, 13)
        resources.ApplyResources(Me.btnThisPatient, "btnThisPatient")
        Me.btnThisPatient.Name = "btnThisPatient"
        Me.filtersTable.SetRow(Me.btnThisPatient, 0)
        '
        'btnAllPatients
        '
        Me.btnAllPatients.Appearance.Font = CType(resources.GetObject("btnAllPatients.Appearance.Font"), System.Drawing.Font)
        Me.btnAllPatients.Appearance.Options.UseFont = True
        Me.btnAllPatients.Appearance.Options.UseTextOptions = True
        Me.btnAllPatients.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.filtersTable.SetColumn(Me.btnAllPatients, 12)
        resources.ApplyResources(Me.btnAllPatients, "btnAllPatients")
        Me.btnAllPatients.Name = "btnAllPatients"
        Me.filtersTable.SetRow(Me.btnAllPatients, 0)
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
        Me.filtersTable.SetColumn(Me.lblStatusFilter, 9)
        resources.ApplyResources(Me.lblStatusFilter, "lblStatusFilter")
        Me.lblStatusFilter.LineVisible = True
        Me.lblStatusFilter.Name = "lblStatusFilter"
        Me.filtersTable.SetRow(Me.lblStatusFilter, 0)
        '
        'btnAllReasons
        '
        Me.btnAllReasons.Appearance.Font = CType(resources.GetObject("btnAllReasons.Appearance.Font"), System.Drawing.Font)
        Me.btnAllReasons.Appearance.Options.UseFont = True
        Me.btnAllReasons.Appearance.Options.UseTextOptions = True
        Me.btnAllReasons.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.filtersTable.SetColumn(Me.btnAllReasons, 15)
        resources.ApplyResources(Me.btnAllReasons, "btnAllReasons")
        Me.btnAllReasons.Name = "btnAllReasons"
        Me.filtersTable.SetRow(Me.btnAllReasons, 0)
        '
        'lblCount
        '
        Me.lblCount.Appearance.Font = CType(resources.GetObject("lblCount.Appearance.Font"), System.Drawing.Font)
        Me.lblCount.Appearance.Options.UseFont = True
        Me.lblCount.Appearance.Options.UseTextOptions = True
        Me.lblCount.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.filtersTable.SetColumn(Me.lblCount, 14)
        resources.ApplyResources(Me.lblCount, "lblCount")
        Me.lblCount.Name = "lblCount"
        Me.filtersTable.SetRow(Me.lblCount, 1)
        '
        'LookUpStatus
        '
        resources.ApplyResources(Me.LookUpStatus, "LookUpStatus")
        Me.filtersTable.SetColumn(Me.LookUpStatus, 10)
        Me.filtersTable.SetColumnSpan(Me.LookUpStatus, 2)
        Me.LookUpStatus.Name = "LookUpStatus"
        Me.LookUpStatus.Properties.Appearance.Font = CType(resources.GetObject("LookUpStatus.Properties.Appearance.Font"), System.Drawing.Font)
        Me.LookUpStatus.Properties.Appearance.Options.UseFont = True
        Me.LookUpStatus.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("LookUpStatus.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me.LookUpStatus.Properties.NullText = resources.GetString("LookUpStatus.Properties.NullText")
        Me.filtersTable.SetRow(Me.LookUpStatus, 0)
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
        Me.filtersTable.SetColumn(Me.lblDoc, 6)
        Me.lblDoc.LineVisible = True
        Me.lblDoc.Name = "lblDoc"
        Me.filtersTable.SetRow(Me.lblDoc, 0)
        '
        'lblApptsCount
        '
        Me.lblApptsCount.Appearance.Font = CType(resources.GetObject("lblApptsCount.Appearance.Font"), System.Drawing.Font)
        Me.lblApptsCount.Appearance.Options.UseFont = True
        Me.lblApptsCount.Appearance.Options.UseTextOptions = True
        Me.lblApptsCount.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        resources.ApplyResources(Me.lblApptsCount, "lblApptsCount")
        Me.lblApptsCount.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.filtersTable.SetColumn(Me.lblApptsCount, 13)
        Me.lblApptsCount.Name = "lblApptsCount"
        Me.filtersTable.SetRow(Me.lblApptsCount, 1)
        '
        'btnLabelsColors
        '
        Me.btnLabelsColors.Appearance.Font = CType(resources.GetObject("btnLabelsColors.Appearance.Font"), System.Drawing.Font)
        Me.btnLabelsColors.Appearance.Options.UseFont = True
        Me.filtersTable.SetColumn(Me.btnLabelsColors, 12)
        resources.ApplyResources(Me.btnLabelsColors, "btnLabelsColors")
        Me.btnLabelsColors.Name = "btnLabelsColors"
        Me.filtersTable.SetRow(Me.btnLabelsColors, 1)
        '
        'btnLabs
        '
        resources.ApplyResources(Me.btnLabs, "btnLabs")
        Me.btnLabs.Appearance.Font = CType(resources.GetObject("btnLabs.Appearance.Font"), System.Drawing.Font)
        Me.btnLabs.Appearance.Options.UseFont = True
        Me.filtersTable.SetColumn(Me.btnLabs, 1)
        Me.btnLabs.Name = "btnLabs"
        Me.filtersTable.SetRow(Me.btnLabs, 1)
        '
        'lblEndTime
        '
        Me.lblEndTime.Appearance.Font = CType(resources.GetObject("lblEndTime.Appearance.Font"), System.Drawing.Font)
        Me.lblEndTime.Appearance.Options.UseFont = True
        Me.lblEndTime.Appearance.Options.UseTextOptions = True
        Me.lblEndTime.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        resources.ApplyResources(Me.lblEndTime, "lblEndTime")
        Me.lblEndTime.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.filtersTable.SetColumn(Me.lblEndTime, 9)
        Me.lblEndTime.Name = "lblEndTime"
        Me.filtersTable.SetRow(Me.lblEndTime, 1)
        '
        'lblStartTime
        '
        Me.lblStartTime.Appearance.Font = CType(resources.GetObject("lblStartTime.Appearance.Font"), System.Drawing.Font)
        Me.lblStartTime.Appearance.Options.UseFont = True
        Me.lblStartTime.Appearance.Options.UseTextOptions = True
        Me.lblStartTime.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        resources.ApplyResources(Me.lblStartTime, "lblStartTime")
        Me.lblStartTime.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.filtersTable.SetColumn(Me.lblStartTime, 6)
        Me.lblStartTime.Name = "lblStartTime"
        Me.filtersTable.SetRow(Me.lblStartTime, 1)
        '
        'endColor
        '
        Me.filtersTable.SetColumn(Me.endColor, 11)
        resources.ApplyResources(Me.endColor, "endColor")
        Me.endColor.Name = "endColor"
        Me.endColor.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("endColor.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me.filtersTable.SetRow(Me.endColor, 1)
        '
        'LookUpPatient
        '
        resources.ApplyResources(Me.LookUpPatient, "LookUpPatient")
        Me.filtersTable.SetColumn(Me.LookUpPatient, 3)
        Me.filtersTable.SetColumnSpan(Me.LookUpPatient, 2)
        Me.LookUpPatient.Name = "LookUpPatient"
        Me.LookUpPatient.Properties.Appearance.Font = CType(resources.GetObject("LookUpPatient.Properties.Appearance.Font"), System.Drawing.Font)
        Me.LookUpPatient.Properties.Appearance.Options.UseFont = True
        Me.LookUpPatient.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("LookUpPatient.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me.LookUpPatient.Properties.NullText = resources.GetString("LookUpPatient.Properties.NullText")
        Me.filtersTable.SetRow(Me.LookUpPatient, 0)
        '
        'dtEndTime
        '
        Me.filtersTable.SetColumn(Me.dtEndTime, 10)
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
        Me.filtersTable.SetRow(Me.dtEndTime, 1)
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
        'startColor
        '
        Me.filtersTable.SetColumn(Me.startColor, 8)
        resources.ApplyResources(Me.startColor, "startColor")
        Me.startColor.Name = "startColor"
        Me.startColor.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("startColor.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me.filtersTable.SetRow(Me.startColor, 1)
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
        Me.filtersTable.SetRow(Me.lblOptions, 1)
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
        Me.filtersTable.SetRow(Me.dtStartTime, 1)
        '
        'includeReasonCheck
        '
        Me.filtersTable.SetColumn(Me.includeReasonCheck, 3)
        resources.ApplyResources(Me.includeReasonCheck, "includeReasonCheck")
        Me.includeReasonCheck.Name = "includeReasonCheck"
        Me.includeReasonCheck.Properties.Appearance.Font = CType(resources.GetObject("includeReasonCheck.Properties.Appearance.Font"), System.Drawing.Font)
        Me.includeReasonCheck.Properties.Appearance.Options.UseFont = True
        Me.includeReasonCheck.Properties.Caption = resources.GetString("includeReasonCheck.Properties.Caption")
        Me.filtersTable.SetRow(Me.includeReasonCheck, 1)
        '
        'sizeFontCheck
        '
        Me.filtersTable.SetColumn(Me.sizeFontCheck, 5)
        resources.ApplyResources(Me.sizeFontCheck, "sizeFontCheck")
        Me.sizeFontCheck.Name = "sizeFontCheck"
        Me.sizeFontCheck.Properties.Appearance.Font = CType(resources.GetObject("sizeFontCheck.Properties.Appearance.Font"), System.Drawing.Font)
        Me.sizeFontCheck.Properties.Appearance.Options.UseFont = True
        Me.sizeFontCheck.Properties.Caption = resources.GetString("sizeFontCheck.Properties.Caption")
        Me.filtersTable.SetRow(Me.sizeFontCheck, 1)
        '
        'boldFontCheck
        '
        Me.filtersTable.SetColumn(Me.boldFontCheck, 4)
        resources.ApplyResources(Me.boldFontCheck, "boldFontCheck")
        Me.boldFontCheck.Name = "boldFontCheck"
        Me.boldFontCheck.Properties.Appearance.Font = CType(resources.GetObject("boldFontCheck.Properties.Appearance.Font"), System.Drawing.Font)
        Me.boldFontCheck.Properties.Appearance.Options.UseFont = True
        Me.boldFontCheck.Properties.Caption = resources.GetString("boldFontCheck.Properties.Caption")
        Me.filtersTable.SetRow(Me.boldFontCheck, 1)
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
        Me.btnNextView.Name = "btnNextView"
        '
        'btnPrevView
        '
        resources.ApplyResources(Me.btnPrevView, "btnPrevView")
        Me.btnPrevView.Appearance.Font = CType(resources.GetObject("btnPrevView.Appearance.Font"), System.Drawing.Font)
        Me.btnPrevView.Appearance.Options.UseFont = True
        Me.btnPrevView.Name = "btnPrevView"
        '
        'pnlButtons
        '
        Me.pnlButtons.Controls.Add(Me.headerTable)
        resources.ApplyResources(Me.pnlButtons, "pnlButtons")
        Me.pnlButtons.Name = "pnlButtons"
        '
        'headerTable
        '
        Me.headerTable.Columns.AddRange(New DevExpress.Utils.Layout.TablePanelColumn() {New DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 78.06!), New DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 36.7!), New DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 31.29!), New DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 37.05!), New DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 68.9!), New DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 31.8!), New DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 24.1!), New DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 36.35!), New DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 27.6!), New DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 78.15!)})
        Me.headerTable.Controls.Add(Me.btnWeekSnapshot)
        Me.headerTable.Controls.Add(Me.btnToday)
        Me.headerTable.Controls.Add(Me.btnNext)
        Me.headerTable.Controls.Add(Me.btnPrev)
        Me.headerTable.Controls.Add(Me.chkUse24)
        Me.headerTable.Controls.Add(Me.pnlHeaderLeft)
        Me.headerTable.Controls.Add(Me.pnlHeaderRight)
        Me.headerTable.Controls.Add(Me.cmbView)
        Me.headerTable.Controls.Add(Me.LabelControl1)
        Me.headerTable.Controls.Add(Me.gotoDate)
        resources.ApplyResources(Me.headerTable, "headerTable")
        Me.headerTable.Name = "headerTable"
        Me.headerTable.Rows.AddRange(New DevExpress.Utils.Layout.TablePanelRow() {New DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 52.0!)})
        '
        'btnWeekSnapshot
        '
        Me.btnWeekSnapshot.Appearance.Font = CType(resources.GetObject("btnWeekSnapshot.Appearance.Font"), System.Drawing.Font)
        Me.btnWeekSnapshot.Appearance.Options.UseFont = True
        Me.headerTable.SetColumn(Me.btnWeekSnapshot, 8)
        resources.ApplyResources(Me.btnWeekSnapshot, "btnWeekSnapshot")
        Me.btnWeekSnapshot.Name = "btnWeekSnapshot"
        Me.headerTable.SetRow(Me.btnWeekSnapshot, 0)
        '
        'btnToday
        '
        resources.ApplyResources(Me.btnToday, "btnToday")
        Me.btnToday.Appearance.Font = CType(resources.GetObject("btnToday.Appearance.Font"), System.Drawing.Font)
        Me.btnToday.Appearance.Options.UseFont = True
        Me.headerTable.SetColumn(Me.btnToday, 2)
        Me.btnToday.Name = "btnToday"
        Me.headerTable.SetRow(Me.btnToday, 0)
        '
        'btnNext
        '
        resources.ApplyResources(Me.btnNext, "btnNext")
        Me.btnNext.Appearance.Font = CType(resources.GetObject("btnNext.Appearance.Font"), System.Drawing.Font)
        Me.btnNext.Appearance.Options.UseFont = True
        Me.headerTable.SetColumn(Me.btnNext, 5)
        Me.btnNext.Name = "btnNext"
        Me.headerTable.SetRow(Me.btnNext, 0)
        '
        'btnPrev
        '
        resources.ApplyResources(Me.btnPrev, "btnPrev")
        Me.btnPrev.Appearance.Font = CType(resources.GetObject("btnPrev.Appearance.Font"), System.Drawing.Font)
        Me.btnPrev.Appearance.Options.UseFont = True
        Me.headerTable.SetColumn(Me.btnPrev, 3)
        Me.btnPrev.Name = "btnPrev"
        Me.headerTable.SetRow(Me.btnPrev, 0)
        '
        'chkUse24
        '
        Me.headerTable.SetColumn(Me.chkUse24, 1)
        resources.ApplyResources(Me.chkUse24, "chkUse24")
        Me.chkUse24.Name = "chkUse24"
        Me.chkUse24.Properties.Appearance.Font = CType(resources.GetObject("chkUse24.Properties.Appearance.Font"), System.Drawing.Font)
        Me.chkUse24.Properties.Appearance.Options.UseFont = True
        Me.chkUse24.Properties.Caption = resources.GetString("chkUse24.Properties.Caption")
        Me.headerTable.SetRow(Me.chkUse24, 0)
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
        'pnlHeaderRight
        '
        Me.pnlHeaderRight.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.headerTable.SetColumn(Me.pnlHeaderRight, 9)
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
        'cmbView
        '
        Me.headerTable.SetColumn(Me.cmbView, 4)
        resources.ApplyResources(Me.cmbView, "cmbView")
        Me.cmbView.Name = "cmbView"
        Me.cmbView.Properties.Appearance.Font = CType(resources.GetObject("cmbView.Properties.Appearance.Font"), System.Drawing.Font)
        Me.cmbView.Properties.Appearance.Options.UseFont = True
        Me.cmbView.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("cmbView.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me.cmbView.Properties.Items.AddRange(New Object() {resources.GetString("cmbView.Properties.Items"), resources.GetString("cmbView.Properties.Items1"), resources.GetString("cmbView.Properties.Items2"), resources.GetString("cmbView.Properties.Items3"), resources.GetString("cmbView.Properties.Items4"), resources.GetString("cmbView.Properties.Items5"), resources.GetString("cmbView.Properties.Items6")})
        Me.headerTable.SetRow(Me.cmbView, 0)
        '
        'LabelControl1
        '
        Me.LabelControl1.Appearance.Font = CType(resources.GetObject("LabelControl1.Appearance.Font"), System.Drawing.Font)
        Me.LabelControl1.Appearance.Options.UseFont = True
        Me.headerTable.SetColumn(Me.LabelControl1, 6)
        resources.ApplyResources(Me.LabelControl1, "LabelControl1")
        Me.LabelControl1.Name = "LabelControl1"
        Me.headerTable.SetRow(Me.LabelControl1, 0)
        '
        'gotoDate
        '
        Me.headerTable.SetColumn(Me.gotoDate, 7)
        resources.ApplyResources(Me.gotoDate, "gotoDate")
        Me.gotoDate.Name = "gotoDate"
        Me.gotoDate.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("gotoDate.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me.gotoDate.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("gotoDate.Properties.CalendarTimeProperties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me.headerTable.SetRow(Me.gotoDate, 0)
        '
        'FullWeekSched
        '
        Me.Controls.Add(Me.pnlHeader)
        Me.Name = "FullWeekSched"
        resources.ApplyResources(Me, "$this")
        CType(Me.pnlHeader, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlHeader.ResumeLayout(False)
        CType(Me.legendPanel, System.ComponentModel.ISupportInitialize).EndInit()
        Me.legendPanel.ResumeLayout(False)
        CType(Me.grpFilters, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpFilters.ResumeLayout(False)
        CType(Me.filtersTable, System.ComponentModel.ISupportInitialize).EndInit()
        Me.filtersTable.ResumeLayout(False)
        Me.filtersTable.PerformLayout()
        CType(Me.lookUpDoctors.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LookUpStatus.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.endColor.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LookUpPatient.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtEndTime.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtEndTime.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.startColor.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtStartTime.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtStartTime.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.includeReasonCheck.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.sizeFontCheck.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.boldFontCheck.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pnlButtons, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlButtons.ResumeLayout(False)
        CType(Me.headerTable, System.ComponentModel.ISupportInitialize).EndInit()
        Me.headerTable.ResumeLayout(False)
        Me.headerTable.PerformLayout()
        CType(Me.chkUse24.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pnlHeaderLeft, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlHeaderLeft.ResumeLayout(False)
        CType(Me.pnlHeaderRight, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlHeaderRight.ResumeLayout(False)
        CType(Me.cmbView.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gotoDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gotoDate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents pnlHeader As DevExpress.XtraEditors.PanelControl
    Friend WithEvents legendPanel As DevExpress.XtraEditors.PanelControl
    Friend WithEvents grpFilters As DevExpress.XtraEditors.PanelControl
    Friend WithEvents filtersTable As DevExpress.Utils.Layout.TablePanel
    Friend WithEvents btnFilterDoctor0 As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnAllReasons As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents dtStartTime As DevExpress.XtraEditors.DateEdit
    Friend WithEvents btnAllPatients As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnThisPatient As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents includeReasonCheck As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents lblPatients As DevExpress.XtraEditors.LabelControl
    Friend WithEvents btnNextView As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnPrevView As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents lblDoc As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblOptions As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblStatusFilter As DevExpress.XtraEditors.LabelControl
    Friend WithEvents boldFontCheck As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents sizeFontCheck As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents dtEndTime As DevExpress.XtraEditors.DateEdit
    Friend WithEvents pnlButtons As DevExpress.XtraEditors.PanelControl
    Friend WithEvents headerTable As DevExpress.Utils.Layout.TablePanel
    Friend WithEvents pnlHeaderLeft As DevExpress.XtraEditors.PanelControl
    Friend WithEvents lblPatient As DevExpress.XtraEditors.LabelControl
    Friend WithEvents chkUse24 As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents btnAdd As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnPrev As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents cmbView As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents btnNext As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnToday As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents gotoDate As DevExpress.XtraEditors.DateEdit
    Friend WithEvents btnWeekSnapshot As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents pnlHeaderRight As DevExpress.XtraEditors.PanelControl
    Friend WithEvents lblRange As DevExpress.XtraEditors.LabelControl
    Friend WithEvents endColor As DevExpress.XtraEditors.ColorEdit
    Friend WithEvents startColor As DevExpress.XtraEditors.ColorEdit
    Friend WithEvents lblApptsCount As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblCount As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblStartTime As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblEndTime As DevExpress.XtraEditors.LabelControl
    Friend WithEvents btnLabelsColors As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnLabs As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LookUpPatient As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents lookUpDoctors As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents LookUpStatus As DevExpress.XtraEditors.LookUpEdit
End Class
