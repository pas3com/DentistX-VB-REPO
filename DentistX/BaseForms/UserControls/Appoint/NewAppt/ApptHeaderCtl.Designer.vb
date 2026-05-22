Imports System.Windows.Forms
Imports DevExpress.Utils

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class ApptHeaderCtl
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ApptHeaderCtl))
        Me.pnlHeader = New DevExpress.XtraEditors.PanelControl()
        Me.legendPanel = New DevExpress.XtraEditors.PanelControl()
        Me.grpFilters = New DevExpress.XtraEditors.PanelControl()
        Me.filtersTable = New DevExpress.Utils.Layout.TablePanel()
        Me.btnNextView = New DevExpress.XtraEditors.SimpleButton()
        Me.lblPatients = New DevExpress.XtraEditors.LabelControl()
        Me.btnAllPatients = New DevExpress.XtraEditors.SimpleButton()
        Me.btnThisPatient = New DevExpress.XtraEditors.SimpleButton()
        Me.btnPrevAppt = New DevExpress.XtraEditors.SimpleButton()
        Me.btnMoreScheduleOpts = New DevExpress.XtraEditors.SimpleButton()
        Me.btnPrevView = New DevExpress.XtraEditors.SimpleButton()
        Me.btnNextAppt = New DevExpress.XtraEditors.SimpleButton()
        Me.btnAdd = New DevExpress.XtraEditors.SimpleButton()
        Me.btnWeekSnapshot = New DevExpress.XtraEditors.SimpleButton()
        Me.btnLabs = New DevExpress.XtraEditors.SimpleButton()
        Me.cmbStatusFilter = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.cmbDoctorFilter = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.lblDoc = New DevExpress.XtraEditors.LabelControl()
        Me.lblStatusFilter = New DevExpress.XtraEditors.LabelControl()
        Me.pnlButtons = New DevExpress.XtraEditors.PanelControl()
        Me.headerTable = New DevExpress.Utils.Layout.TablePanel()
        Me.pnlHeaderLeft = New DevExpress.XtraEditors.PanelControl()
        Me.lblPatient = New DevExpress.XtraEditors.LabelControl()
        Me.pnlHeaderCenter = New DevExpress.XtraEditors.PanelControl()
        Me.pnlHeaderToolbarFlow = New System.Windows.Forms.FlowLayoutPanel()
        Me.btnOptions = New DevExpress.XtraEditors.SimpleButton()
        Me.chkUse24 = New DevExpress.XtraEditors.CheckEdit()
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        Me.gotoDate = New DevExpress.XtraEditors.DateEdit()
        Me.btnPrev = New DevExpress.XtraEditors.SimpleButton()
        Me.cmbView = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.btnNext = New DevExpress.XtraEditors.SimpleButton()
        Me.btnToday = New DevExpress.XtraEditors.SimpleButton()
        Me.pnlHeaderRight = New DevExpress.XtraEditors.PanelControl()
        Me.lblRange = New DevExpress.XtraEditors.LabelControl()
        Me.flyoutScheduleOptions = New DevExpress.Utils.FlyoutPanel()
        Me.flyoutScheduleOptionsPanel = New DevExpress.Utils.FlyoutPanelControl()
        Me.PanelOptions = New DevExpress.XtraEditors.PanelControl()
        Me.cmbFirstDoctor = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.color_Notes = New DevExpress.XtraEditors.ColorEdit()
        Me.color_Reason = New DevExpress.XtraEditors.ColorEdit()
        Me.color_Patient = New DevExpress.XtraEditors.ColorEdit()
        Me.btnLabelsColors = New DevExpress.XtraEditors.SimpleButton()
        Me.lbl_Notes = New DevExpress.XtraEditors.LabelControl()
        Me.lbl_Reason = New DevExpress.XtraEditors.LabelControl()
        Me.lbl_Patient = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl5 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl4 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl6 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl3 = New DevExpress.XtraEditors.LabelControl()
        Me.lblStartTime = New DevExpress.XtraEditors.LabelControl()
        Me.lblEndTime = New DevExpress.XtraEditors.LabelControl()
        Me.lblApptsCount = New DevExpress.XtraEditors.LabelControl()
        Me.lblCount = New DevExpress.XtraEditors.LabelControl()
        Me.lblOptions = New DevExpress.XtraEditors.LabelControl()
        Me.endColor = New DevExpress.XtraEditors.ColorEdit()
        Me.includeReasonCheck = New DevExpress.XtraEditors.CheckEdit()
        Me.startColor = New DevExpress.XtraEditors.ColorEdit()
        Me.boldFontCheck = New DevExpress.XtraEditors.CheckEdit()
        Me.sizeFontCheck = New DevExpress.XtraEditors.CheckEdit()
        Me.dtStartTime = New DevExpress.XtraEditors.DateEdit()
        Me.dtEndTime = New DevExpress.XtraEditors.DateEdit()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl7 = New DevExpress.XtraEditors.LabelControl()
        CType(Me.pnlHeader, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlHeader.SuspendLayout()
        CType(Me.legendPanel, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.legendPanel.SuspendLayout()
        CType(Me.grpFilters, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpFilters.SuspendLayout()
        CType(Me.filtersTable, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.filtersTable.SuspendLayout()
        CType(Me.cmbStatusFilter.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbDoctorFilter.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
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
        CType(Me.gotoDate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gotoDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbView.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pnlHeaderRight, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlHeaderRight.SuspendLayout()
        CType(Me.flyoutScheduleOptions, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.flyoutScheduleOptions.SuspendLayout()
        CType(Me.flyoutScheduleOptionsPanel, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.flyoutScheduleOptionsPanel.SuspendLayout()
        CType(Me.PanelOptions, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelOptions.SuspendLayout()
        CType(Me.cmbFirstDoctor.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.color_Notes.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.color_Reason.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.color_Patient.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.endColor.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.includeReasonCheck.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.startColor.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.boldFontCheck.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.sizeFontCheck.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtStartTime.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtStartTime.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtEndTime.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtEndTime.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.filtersTable.Columns.AddRange(New DevExpress.Utils.Layout.TablePanelColumn() {New DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.AutoSize, 26.0!), New DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 50.0!), New DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 50.0!), New DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 120.0!), New DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 79.0!), New DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 84.0!), New DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 100.0!), New DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 66.0!), New DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.AutoSize, 84.81!), New DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 76.0!), New DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.AutoSize, 88.0!), New DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 113.0!), New DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 73.0!), New DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 78.0!), New DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 50.0!), New DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 50.0!), New DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.AutoSize, 108.0!)})
        Me.filtersTable.Controls.Add(Me.btnNextView)
        Me.filtersTable.Controls.Add(Me.lblPatients)
        Me.filtersTable.Controls.Add(Me.btnAllPatients)
        Me.filtersTable.Controls.Add(Me.btnThisPatient)
        Me.filtersTable.Controls.Add(Me.btnPrevAppt)
        Me.filtersTable.Controls.Add(Me.btnMoreScheduleOpts)
        Me.filtersTable.Controls.Add(Me.btnPrevView)
        Me.filtersTable.Controls.Add(Me.btnNextAppt)
        Me.filtersTable.Controls.Add(Me.btnAdd)
        Me.filtersTable.Controls.Add(Me.btnWeekSnapshot)
        Me.filtersTable.Controls.Add(Me.btnLabs)
        Me.filtersTable.Controls.Add(Me.cmbStatusFilter)
        Me.filtersTable.Controls.Add(Me.cmbDoctorFilter)
        Me.filtersTable.Controls.Add(Me.lblDoc)
        Me.filtersTable.Controls.Add(Me.lblStatusFilter)
        resources.ApplyResources(Me.filtersTable, "filtersTable")
        Me.filtersTable.Name = "filtersTable"
        Me.filtersTable.Rows.AddRange(New DevExpress.Utils.Layout.TablePanelRow() {New DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 40.0!)})
        '
        'btnNextView
        '
        resources.ApplyResources(Me.btnNextView, "btnNextView")
        Me.btnNextView.Appearance.Font = CType(resources.GetObject("btnNextView.Appearance.Font"), System.Drawing.Font)
        Me.btnNextView.Appearance.Options.UseFont = True
        Me.filtersTable.SetColumn(Me.btnNextView, 16)
        Me.btnNextView.Name = "btnNextView"
        Me.filtersTable.SetRow(Me.btnNextView, 0)
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
        Me.filtersTable.SetColumn(Me.lblPatients, 4)
        Me.lblPatients.LineVisible = True
        Me.lblPatients.Name = "lblPatients"
        Me.filtersTable.SetRow(Me.lblPatients, 0)
        '
        'btnAllPatients
        '
        Me.btnAllPatients.Appearance.Font = CType(resources.GetObject("btnAllPatients.Appearance.Font"), System.Drawing.Font)
        Me.btnAllPatients.Appearance.Options.UseFont = True
        Me.btnAllPatients.Appearance.Options.UseTextOptions = True
        Me.btnAllPatients.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.filtersTable.SetColumn(Me.btnAllPatients, 5)
        resources.ApplyResources(Me.btnAllPatients, "btnAllPatients")
        Me.btnAllPatients.Name = "btnAllPatients"
        Me.filtersTable.SetRow(Me.btnAllPatients, 0)
        '
        'btnThisPatient
        '
        Me.btnThisPatient.Appearance.Font = CType(resources.GetObject("btnThisPatient.Appearance.Font"), System.Drawing.Font)
        Me.btnThisPatient.Appearance.Options.UseFont = True
        Me.btnThisPatient.Appearance.Options.UseTextOptions = True
        Me.btnThisPatient.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.filtersTable.SetColumn(Me.btnThisPatient, 6)
        resources.ApplyResources(Me.btnThisPatient, "btnThisPatient")
        Me.btnThisPatient.Name = "btnThisPatient"
        Me.filtersTable.SetRow(Me.btnThisPatient, 0)
        '
        'btnPrevAppt
        '
        Me.btnPrevAppt.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.btnPrevAppt.Appearance.Font = CType(resources.GetObject("btnPrevAppt.Appearance.Font"), System.Drawing.Font)
        Me.btnPrevAppt.Appearance.Options.UseBackColor = True
        Me.btnPrevAppt.Appearance.Options.UseFont = True
        Me.filtersTable.SetColumn(Me.btnPrevAppt, 1)
        resources.ApplyResources(Me.btnPrevAppt, "btnPrevAppt")
        Me.btnPrevAppt.Name = "btnPrevAppt"
        Me.filtersTable.SetRow(Me.btnPrevAppt, 0)
        '
        'btnMoreScheduleOpts
        '
        Me.btnMoreScheduleOpts.Appearance.Font = CType(resources.GetObject("btnMoreScheduleOpts.Appearance.Font"), System.Drawing.Font)
        Me.btnMoreScheduleOpts.Appearance.Options.UseFont = True
        Me.filtersTable.SetColumn(Me.btnMoreScheduleOpts, 3)
        resources.ApplyResources(Me.btnMoreScheduleOpts, "btnMoreScheduleOpts")
        Me.btnMoreScheduleOpts.Name = "btnMoreScheduleOpts"
        Me.filtersTable.SetRow(Me.btnMoreScheduleOpts, 0)
        '
        'btnPrevView
        '
        resources.ApplyResources(Me.btnPrevView, "btnPrevView")
        Me.btnPrevView.Appearance.Font = CType(resources.GetObject("btnPrevView.Appearance.Font"), System.Drawing.Font)
        Me.btnPrevView.Appearance.Options.UseFont = True
        Me.filtersTable.SetColumn(Me.btnPrevView, 0)
        Me.btnPrevView.Name = "btnPrevView"
        Me.filtersTable.SetRow(Me.btnPrevView, 0)
        '
        'btnNextAppt
        '
        Me.btnNextAppt.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.btnNextAppt.Appearance.Font = CType(resources.GetObject("btnNextAppt.Appearance.Font"), System.Drawing.Font)
        Me.btnNextAppt.Appearance.Options.UseBackColor = True
        Me.btnNextAppt.Appearance.Options.UseFont = True
        Me.filtersTable.SetColumn(Me.btnNextAppt, 15)
        resources.ApplyResources(Me.btnNextAppt, "btnNextAppt")
        Me.btnNextAppt.Name = "btnNextAppt"
        Me.filtersTable.SetRow(Me.btnNextAppt, 0)
        '
        'btnAdd
        '
        resources.ApplyResources(Me.btnAdd, "btnAdd")
        Me.btnAdd.Appearance.Font = CType(resources.GetObject("btnAdd.Appearance.Font"), System.Drawing.Font)
        Me.btnAdd.Appearance.ForeColor = System.Drawing.Color.Red
        Me.btnAdd.Appearance.Options.UseFont = True
        Me.btnAdd.Appearance.Options.UseForeColor = True
        Me.filtersTable.SetColumn(Me.btnAdd, 13)
        Me.btnAdd.ImageOptions.Image = Global.DentistX.My.Resources.Resources.add_16x16
        Me.btnAdd.Name = "btnAdd"
        Me.filtersTable.SetRow(Me.btnAdd, 0)
        '
        'btnWeekSnapshot
        '
        resources.ApplyResources(Me.btnWeekSnapshot, "btnWeekSnapshot")
        Me.btnWeekSnapshot.Appearance.Font = CType(resources.GetObject("btnWeekSnapshot.Appearance.Font"), System.Drawing.Font)
        Me.btnWeekSnapshot.Appearance.Options.UseFont = True
        Me.filtersTable.SetColumn(Me.btnWeekSnapshot, 11)
        Me.btnWeekSnapshot.Name = "btnWeekSnapshot"
        Me.filtersTable.SetRow(Me.btnWeekSnapshot, 0)
        '
        'btnLabs
        '
        resources.ApplyResources(Me.btnLabs, "btnLabs")
        Me.btnLabs.Appearance.Font = CType(resources.GetObject("btnLabs.Appearance.Font"), System.Drawing.Font)
        Me.btnLabs.Appearance.Options.UseFont = True
        Me.filtersTable.SetColumn(Me.btnLabs, 12)
        Me.btnLabs.Name = "btnLabs"
        Me.filtersTable.SetRow(Me.btnLabs, 0)
        '
        'cmbStatusFilter
        '
        Me.filtersTable.SetColumn(Me.cmbStatusFilter, 10)
        resources.ApplyResources(Me.cmbStatusFilter, "cmbStatusFilter")
        Me.cmbStatusFilter.Name = "cmbStatusFilter"
        Me.cmbStatusFilter.Properties.Appearance.Font = CType(resources.GetObject("cmbStatusFilter.Properties.Appearance.Font"), System.Drawing.Font)
        Me.cmbStatusFilter.Properties.Appearance.Options.UseFont = True
        Me.cmbStatusFilter.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("cmbStatusFilter.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me.cmbStatusFilter.Properties.DropDownRows = 8
        Me.cmbStatusFilter.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
        Me.filtersTable.SetRow(Me.cmbStatusFilter, 0)
        '
        'cmbDoctorFilter
        '
        Me.filtersTable.SetColumn(Me.cmbDoctorFilter, 8)
        resources.ApplyResources(Me.cmbDoctorFilter, "cmbDoctorFilter")
        Me.cmbDoctorFilter.Name = "cmbDoctorFilter"
        Me.cmbDoctorFilter.Properties.Appearance.Font = CType(resources.GetObject("cmbDoctorFilter.Properties.Appearance.Font"), System.Drawing.Font)
        Me.cmbDoctorFilter.Properties.Appearance.Options.UseFont = True
        Me.cmbDoctorFilter.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("cmbDoctorFilter.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me.cmbDoctorFilter.Properties.DropDownRows = 12
        Me.cmbDoctorFilter.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
        Me.filtersTable.SetRow(Me.cmbDoctorFilter, 0)
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
        Me.filtersTable.SetColumn(Me.lblDoc, 7)
        Me.lblDoc.LineVisible = True
        Me.lblDoc.Name = "lblDoc"
        Me.filtersTable.SetRow(Me.lblDoc, 0)
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
        'pnlButtons
        '
        Me.pnlButtons.Controls.Add(Me.headerTable)
        resources.ApplyResources(Me.pnlButtons, "pnlButtons")
        Me.pnlButtons.Name = "pnlButtons"
        '
        'headerTable
        '
        Me.headerTable.Columns.AddRange(New DevExpress.Utils.Layout.TablePanelColumn() {New DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 20.0!), New DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 60.0!), New DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 20.0!)})
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
        Me.pnlHeaderToolbarFlow.Controls.Add(Me.btnOptions)
        Me.pnlHeaderToolbarFlow.Controls.Add(Me.LabelControl1)
        Me.pnlHeaderToolbarFlow.Controls.Add(Me.btnPrev)
        Me.pnlHeaderToolbarFlow.Controls.Add(Me.cmbView)
        Me.pnlHeaderToolbarFlow.Controls.Add(Me.btnNext)
        Me.pnlHeaderToolbarFlow.Controls.Add(Me.LabelControl7)
        Me.pnlHeaderToolbarFlow.Controls.Add(Me.btnToday)
        Me.pnlHeaderToolbarFlow.Controls.Add(Me.LabelControl2)
        Me.pnlHeaderToolbarFlow.Controls.Add(Me.gotoDate)
        Me.pnlHeaderToolbarFlow.Name = "pnlHeaderToolbarFlow"
        '
        'btnOptions
        '
        Me.btnOptions.Appearance.Font = CType(resources.GetObject("btnOptions.Appearance.Font"), System.Drawing.Font)
        Me.btnOptions.Appearance.Options.UseFont = True
        Me.btnOptions.Appearance.Options.UseTextOptions = True
        Me.btnOptions.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        resources.ApplyResources(Me.btnOptions, "btnOptions")
        Me.btnOptions.Name = "btnOptions"
        '
        'chkUse24
        '
        resources.ApplyResources(Me.chkUse24, "chkUse24")
        Me.chkUse24.Name = "chkUse24"
        Me.chkUse24.Properties.Appearance.Font = CType(resources.GetObject("chkUse24.Properties.Appearance.Font"), System.Drawing.Font)
        Me.chkUse24.Properties.Appearance.ForeColor = System.Drawing.Color.OliveDrab
        Me.chkUse24.Properties.Appearance.Options.UseFont = True
        Me.chkUse24.Properties.Appearance.Options.UseForeColor = True
        Me.chkUse24.Properties.Caption = resources.GetString("chkUse24.Properties.Caption")
        '
        'LabelControl2
        '
        Me.LabelControl2.Appearance.Font = CType(resources.GetObject("LabelControl2.Appearance.Font"), System.Drawing.Font)
        Me.LabelControl2.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.LabelControl2, "LabelControl2")
        Me.LabelControl2.Name = "LabelControl2"
        '
        'gotoDate
        '
        resources.ApplyResources(Me.gotoDate, "gotoDate")
        Me.gotoDate.Name = "gotoDate"
        Me.gotoDate.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("gotoDate.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me.gotoDate.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("gotoDate.Properties.CalendarTimeProperties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        '
        'btnPrev
        '
        resources.ApplyResources(Me.btnPrev, "btnPrev")
        Me.btnPrev.Appearance.BackColor = System.Drawing.Color.Red
        Me.btnPrev.Appearance.Font = CType(resources.GetObject("btnPrev.Appearance.Font"), System.Drawing.Font)
        Me.btnPrev.Appearance.Options.UseBackColor = True
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
        Me.btnNext.Appearance.BackColor = System.Drawing.Color.Red
        Me.btnNext.Appearance.Font = CType(resources.GetObject("btnNext.Appearance.Font"), System.Drawing.Font)
        Me.btnNext.Appearance.Options.UseBackColor = True
        Me.btnNext.Appearance.Options.UseFont = True
        Me.btnNext.Name = "btnNext"
        '
        'btnToday
        '
        resources.ApplyResources(Me.btnToday, "btnToday")
        Me.btnToday.Appearance.BackColor = System.Drawing.Color.DarkGray
        Me.btnToday.Appearance.Font = CType(resources.GetObject("btnToday.Appearance.Font"), System.Drawing.Font)
        Me.btnToday.Appearance.Options.UseBackColor = True
        Me.btnToday.Appearance.Options.UseFont = True
        Me.btnToday.Name = "btnToday"
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
        'flyoutScheduleOptions
        '
        Me.flyoutScheduleOptions.Controls.Add(Me.flyoutScheduleOptionsPanel)
        resources.ApplyResources(Me.flyoutScheduleOptions, "flyoutScheduleOptions")
        Me.flyoutScheduleOptions.Name = "flyoutScheduleOptions"
        Me.flyoutScheduleOptions.Options.AnchorType = DevExpress.Utils.Win.PopupToolWindowAnchor.Bottom
        Me.flyoutScheduleOptions.Options.CloseOnOuterClick = True
        Me.flyoutScheduleOptions.OwnerControl = Me.btnMoreScheduleOpts
        '
        'flyoutScheduleOptionsPanel
        '
        Me.flyoutScheduleOptionsPanel.Controls.Add(Me.PanelOptions)
        resources.ApplyResources(Me.flyoutScheduleOptionsPanel, "flyoutScheduleOptionsPanel")
        Me.flyoutScheduleOptionsPanel.FlyoutPanel = Me.flyoutScheduleOptions
        Me.flyoutScheduleOptionsPanel.Name = "flyoutScheduleOptionsPanel"
        '
        'PanelOptions
        '
        Me.PanelOptions.Controls.Add(Me.cmbFirstDoctor)
        Me.PanelOptions.Controls.Add(Me.color_Notes)
        Me.PanelOptions.Controls.Add(Me.color_Reason)
        Me.PanelOptions.Controls.Add(Me.color_Patient)
        Me.PanelOptions.Controls.Add(Me.btnLabelsColors)
        Me.PanelOptions.Controls.Add(Me.lbl_Notes)
        Me.PanelOptions.Controls.Add(Me.lbl_Reason)
        Me.PanelOptions.Controls.Add(Me.lbl_Patient)
        Me.PanelOptions.Controls.Add(Me.LabelControl5)
        Me.PanelOptions.Controls.Add(Me.LabelControl4)
        Me.PanelOptions.Controls.Add(Me.LabelControl6)
        Me.PanelOptions.Controls.Add(Me.LabelControl3)
        Me.PanelOptions.Controls.Add(Me.lblStartTime)
        Me.PanelOptions.Controls.Add(Me.lblEndTime)
        Me.PanelOptions.Controls.Add(Me.lblApptsCount)
        Me.PanelOptions.Controls.Add(Me.lblCount)
        Me.PanelOptions.Controls.Add(Me.lblOptions)
        Me.PanelOptions.Controls.Add(Me.endColor)
        Me.PanelOptions.Controls.Add(Me.includeReasonCheck)
        Me.PanelOptions.Controls.Add(Me.startColor)
        Me.PanelOptions.Controls.Add(Me.boldFontCheck)
        Me.PanelOptions.Controls.Add(Me.sizeFontCheck)
        Me.PanelOptions.Controls.Add(Me.dtStartTime)
        Me.PanelOptions.Controls.Add(Me.dtEndTime)
        resources.ApplyResources(Me.PanelOptions, "PanelOptions")
        Me.PanelOptions.Name = "PanelOptions"
        '
        'cmbFirstDoctor
        '
        resources.ApplyResources(Me.cmbFirstDoctor, "cmbFirstDoctor")
        Me.cmbFirstDoctor.Name = "cmbFirstDoctor"
        Me.cmbFirstDoctor.Properties.Appearance.Font = CType(resources.GetObject("cmbFirstDoctor.Properties.Appearance.Font"), System.Drawing.Font)
        Me.cmbFirstDoctor.Properties.Appearance.Options.UseFont = True
        Me.cmbFirstDoctor.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("cmbFirstDoctor.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me.cmbFirstDoctor.Properties.DropDownRows = 12
        Me.cmbFirstDoctor.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
        '
        'color_Notes
        '
        resources.ApplyResources(Me.color_Notes, "color_Notes")
        Me.color_Notes.Name = "color_Notes"
        Me.color_Notes.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("color_Notes.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        '
        'color_Reason
        '
        resources.ApplyResources(Me.color_Reason, "color_Reason")
        Me.color_Reason.Name = "color_Reason"
        Me.color_Reason.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("color_Reason.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        '
        'color_Patient
        '
        resources.ApplyResources(Me.color_Patient, "color_Patient")
        Me.color_Patient.Name = "color_Patient"
        Me.color_Patient.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("color_Patient.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        '
        'btnLabelsColors
        '
        Me.btnLabelsColors.Appearance.Font = CType(resources.GetObject("btnLabelsColors.Appearance.Font"), System.Drawing.Font)
        Me.btnLabelsColors.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.btnLabelsColors, "btnLabelsColors")
        Me.btnLabelsColors.Name = "btnLabelsColors"
        '
        'lbl_Notes
        '
        Me.lbl_Notes.Appearance.Font = CType(resources.GetObject("lbl_Notes.Appearance.Font"), System.Drawing.Font)
        Me.lbl_Notes.Appearance.Options.UseFont = True
        Me.lbl_Notes.Appearance.Options.UseTextOptions = True
        Me.lbl_Notes.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        resources.ApplyResources(Me.lbl_Notes, "lbl_Notes")
        Me.lbl_Notes.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.lbl_Notes.Name = "lbl_Notes"
        '
        'lbl_Reason
        '
        Me.lbl_Reason.Appearance.Font = CType(resources.GetObject("lbl_Reason.Appearance.Font"), System.Drawing.Font)
        Me.lbl_Reason.Appearance.Options.UseFont = True
        Me.lbl_Reason.Appearance.Options.UseTextOptions = True
        Me.lbl_Reason.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        resources.ApplyResources(Me.lbl_Reason, "lbl_Reason")
        Me.lbl_Reason.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.lbl_Reason.Name = "lbl_Reason"
        '
        'lbl_Patient
        '
        Me.lbl_Patient.Appearance.Font = CType(resources.GetObject("lbl_Patient.Appearance.Font"), System.Drawing.Font)
        Me.lbl_Patient.Appearance.Options.UseFont = True
        Me.lbl_Patient.Appearance.Options.UseTextOptions = True
        Me.lbl_Patient.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        resources.ApplyResources(Me.lbl_Patient, "lbl_Patient")
        Me.lbl_Patient.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.lbl_Patient.Name = "lbl_Patient"
        '
        'LabelControl5
        '
        Me.LabelControl5.Appearance.Font = CType(resources.GetObject("LabelControl5.Appearance.Font"), System.Drawing.Font)
        Me.LabelControl5.Appearance.Options.UseFont = True
        Me.LabelControl5.Appearance.Options.UseTextOptions = True
        Me.LabelControl5.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        resources.ApplyResources(Me.LabelControl5, "LabelControl5")
        Me.LabelControl5.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.LabelControl5.Name = "LabelControl5"
        '
        'LabelControl4
        '
        Me.LabelControl4.Appearance.Font = CType(resources.GetObject("LabelControl4.Appearance.Font"), System.Drawing.Font)
        Me.LabelControl4.Appearance.Options.UseFont = True
        Me.LabelControl4.Appearance.Options.UseTextOptions = True
        Me.LabelControl4.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        resources.ApplyResources(Me.LabelControl4, "LabelControl4")
        Me.LabelControl4.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.LabelControl4.Name = "LabelControl4"
        '
        'LabelControl6
        '
        Me.LabelControl6.Appearance.Font = CType(resources.GetObject("LabelControl6.Appearance.Font"), System.Drawing.Font)
        Me.LabelControl6.Appearance.Options.UseFont = True
        Me.LabelControl6.Appearance.Options.UseTextOptions = True
        Me.LabelControl6.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        resources.ApplyResources(Me.LabelControl6, "LabelControl6")
        Me.LabelControl6.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.LabelControl6.Name = "LabelControl6"
        '
        'LabelControl3
        '
        Me.LabelControl3.Appearance.Font = CType(resources.GetObject("LabelControl3.Appearance.Font"), System.Drawing.Font)
        Me.LabelControl3.Appearance.Options.UseFont = True
        Me.LabelControl3.Appearance.Options.UseTextOptions = True
        Me.LabelControl3.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        resources.ApplyResources(Me.LabelControl3, "LabelControl3")
        Me.LabelControl3.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.LabelControl3.Name = "LabelControl3"
        '
        'lblStartTime
        '
        Me.lblStartTime.Appearance.Font = CType(resources.GetObject("lblStartTime.Appearance.Font"), System.Drawing.Font)
        Me.lblStartTime.Appearance.Options.UseFont = True
        Me.lblStartTime.Appearance.Options.UseTextOptions = True
        Me.lblStartTime.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        resources.ApplyResources(Me.lblStartTime, "lblStartTime")
        Me.lblStartTime.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.lblStartTime.Name = "lblStartTime"
        '
        'lblEndTime
        '
        Me.lblEndTime.Appearance.Font = CType(resources.GetObject("lblEndTime.Appearance.Font"), System.Drawing.Font)
        Me.lblEndTime.Appearance.Options.UseFont = True
        Me.lblEndTime.Appearance.Options.UseTextOptions = True
        Me.lblEndTime.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        resources.ApplyResources(Me.lblEndTime, "lblEndTime")
        Me.lblEndTime.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.lblEndTime.Name = "lblEndTime"
        '
        'lblApptsCount
        '
        Me.lblApptsCount.Appearance.Font = CType(resources.GetObject("lblApptsCount.Appearance.Font"), System.Drawing.Font)
        Me.lblApptsCount.Appearance.Options.UseFont = True
        Me.lblApptsCount.Appearance.Options.UseTextOptions = True
        Me.lblApptsCount.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        resources.ApplyResources(Me.lblApptsCount, "lblApptsCount")
        Me.lblApptsCount.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.lblApptsCount.Name = "lblApptsCount"
        '
        'lblCount
        '
        Me.lblCount.Appearance.Font = CType(resources.GetObject("lblCount.Appearance.Font"), System.Drawing.Font)
        Me.lblCount.Appearance.Options.UseFont = True
        Me.lblCount.Appearance.Options.UseTextOptions = True
        Me.lblCount.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        resources.ApplyResources(Me.lblCount, "lblCount")
        Me.lblCount.Name = "lblCount"
        '
        'lblOptions
        '
        Me.lblOptions.Appearance.BackColor = System.Drawing.Color.Blue
        Me.lblOptions.Appearance.Font = CType(resources.GetObject("lblOptions.Appearance.Font"), System.Drawing.Font)
        Me.lblOptions.Appearance.ForeColor = System.Drawing.Color.White
        Me.lblOptions.Appearance.Options.UseBackColor = True
        Me.lblOptions.Appearance.Options.UseFont = True
        Me.lblOptions.Appearance.Options.UseForeColor = True
        Me.lblOptions.Appearance.Options.UseTextOptions = True
        Me.lblOptions.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        resources.ApplyResources(Me.lblOptions, "lblOptions")
        Me.lblOptions.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.lblOptions.LineVisible = True
        Me.lblOptions.Name = "lblOptions"
        '
        'endColor
        '
        resources.ApplyResources(Me.endColor, "endColor")
        Me.endColor.Name = "endColor"
        Me.endColor.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("endColor.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        '
        'includeReasonCheck
        '
        resources.ApplyResources(Me.includeReasonCheck, "includeReasonCheck")
        Me.includeReasonCheck.Name = "includeReasonCheck"
        Me.includeReasonCheck.Properties.Appearance.Font = CType(resources.GetObject("includeReasonCheck.Properties.Appearance.Font"), System.Drawing.Font)
        Me.includeReasonCheck.Properties.Appearance.Options.UseFont = True
        Me.includeReasonCheck.Properties.Caption = resources.GetString("includeReasonCheck.Properties.Caption")
        '
        'startColor
        '
        resources.ApplyResources(Me.startColor, "startColor")
        Me.startColor.Name = "startColor"
        Me.startColor.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("startColor.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        '
        'boldFontCheck
        '
        resources.ApplyResources(Me.boldFontCheck, "boldFontCheck")
        Me.boldFontCheck.Name = "boldFontCheck"
        Me.boldFontCheck.Properties.Appearance.Font = CType(resources.GetObject("boldFontCheck.Properties.Appearance.Font"), System.Drawing.Font)
        Me.boldFontCheck.Properties.Appearance.Options.UseFont = True
        Me.boldFontCheck.Properties.Caption = resources.GetString("boldFontCheck.Properties.Caption")
        '
        'sizeFontCheck
        '
        resources.ApplyResources(Me.sizeFontCheck, "sizeFontCheck")
        Me.sizeFontCheck.Name = "sizeFontCheck"
        Me.sizeFontCheck.Properties.Appearance.Font = CType(resources.GetObject("sizeFontCheck.Properties.Appearance.Font"), System.Drawing.Font)
        Me.sizeFontCheck.Properties.Appearance.Options.UseFont = True
        Me.sizeFontCheck.Properties.Caption = resources.GetString("sizeFontCheck.Properties.Caption")
        '
        'dtStartTime
        '
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
        '
        'dtEndTime
        '
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
        '
        'LabelControl1
        '
        Me.LabelControl1.Appearance.Font = CType(resources.GetObject("LabelControl1.Appearance.Font"), System.Drawing.Font)
        Me.LabelControl1.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.LabelControl1, "LabelControl1")
        Me.LabelControl1.Name = "LabelControl1"
        '
        'LabelControl7
        '
        Me.LabelControl7.Appearance.Font = CType(resources.GetObject("LabelControl7.Appearance.Font"), System.Drawing.Font)
        Me.LabelControl7.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.LabelControl7, "LabelControl7")
        Me.LabelControl7.Name = "LabelControl7"
        '
        'ApptHeaderCtl
        '
        Me.Controls.Add(Me.pnlHeader)
        Me.Controls.Add(Me.flyoutScheduleOptions)
        Me.Name = "ApptHeaderCtl"
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
        CType(Me.cmbStatusFilter.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbDoctorFilter.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pnlButtons, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlButtons.ResumeLayout(False)
        CType(Me.headerTable, System.ComponentModel.ISupportInitialize).EndInit()
        Me.headerTable.ResumeLayout(False)
        CType(Me.pnlHeaderLeft, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlHeaderLeft.ResumeLayout(False)
        CType(Me.pnlHeaderCenter, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlHeaderCenter.ResumeLayout(False)
        Me.pnlHeaderToolbarFlow.ResumeLayout(False)
        Me.pnlHeaderToolbarFlow.PerformLayout()
        CType(Me.chkUse24.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gotoDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gotoDate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbView.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pnlHeaderRight, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlHeaderRight.ResumeLayout(False)
        CType(Me.flyoutScheduleOptions, System.ComponentModel.ISupportInitialize).EndInit()
        Me.flyoutScheduleOptions.ResumeLayout(False)
        CType(Me.flyoutScheduleOptionsPanel, System.ComponentModel.ISupportInitialize).EndInit()
        Me.flyoutScheduleOptionsPanel.ResumeLayout(False)
        CType(Me.PanelOptions, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelOptions.ResumeLayout(False)
        Me.PanelOptions.PerformLayout()
        CType(Me.cmbFirstDoctor.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.color_Notes.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.color_Reason.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.color_Patient.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.endColor.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.includeReasonCheck.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.startColor.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.boldFontCheck.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.sizeFontCheck.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtStartTime.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtStartTime.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtEndTime.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtEndTime.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents pnlHeader As DevExpress.XtraEditors.PanelControl
    Friend WithEvents legendPanel As DevExpress.XtraEditors.PanelControl
    Friend WithEvents grpFilters As DevExpress.XtraEditors.PanelControl
    Friend WithEvents flyoutScheduleOptions As DevExpress.Utils.FlyoutPanel
    Friend WithEvents flyoutScheduleOptionsPanel As DevExpress.Utils.FlyoutPanelControl
    Friend WithEvents filtersTable As DevExpress.Utils.Layout.TablePanel
    Friend WithEvents cmbDoctorFilter As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents cmbStatusFilter As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents btnMoreScheduleOpts As DevExpress.XtraEditors.SimpleButton
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
    Friend WithEvents pnlHeaderCenter As DevExpress.XtraEditors.PanelControl
    Friend WithEvents pnlHeaderToolbarFlow As FlowLayoutPanel
    Friend WithEvents chkUse24 As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents btnAdd As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnPrev As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents cmbView As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents btnNext As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnToday As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents gotoDate As DevExpress.XtraEditors.DateEdit
    Friend WithEvents btnWeekSnapshot As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents pnlHeaderRight As DevExpress.XtraEditors.PanelControl
    Friend WithEvents lblRange As DevExpress.XtraEditors.LabelControl
    Friend WithEvents endColor As DevExpress.XtraEditors.ColorEdit
    Friend WithEvents startColor As DevExpress.XtraEditors.ColorEdit
    Friend WithEvents PanelOptions As DevExpress.XtraEditors.PanelControl
    Friend WithEvents lblApptsCount As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblCount As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblStartTime As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblEndTime As DevExpress.XtraEditors.LabelControl
    Friend WithEvents btnLabelsColors As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnLabs As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents btnOptions As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LabelControl5 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl4 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl3 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lbl_Notes As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lbl_Reason As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lbl_Patient As DevExpress.XtraEditors.LabelControl
    Friend WithEvents color_Notes As DevExpress.XtraEditors.ColorEdit
    Friend WithEvents color_Reason As DevExpress.XtraEditors.ColorEdit
    Friend WithEvents color_Patient As DevExpress.XtraEditors.ColorEdit
    Friend WithEvents cmbFirstDoctor As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents LabelControl6 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents btnPrevAppt As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnNextAppt As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl7 As DevExpress.XtraEditors.LabelControl
End Class
