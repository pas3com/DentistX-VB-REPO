<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class PersonnelAttendance
    Inherits DevExpress.XtraEditors.XtraForm

    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    Private components As System.ComponentModel.IContainer

    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(PersonnelAttendance))
        Me.Splitter1 = New DevExpress.XtraEditors.SplitContainerControl()
        Me.DGV = New DevExpress.XtraGrid.GridControl()
        Me.dgView = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.colRowNum = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colAttendanceID = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colPersonType = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colPersonID = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colPersonName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colAttendanceDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colCheckInTime = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colCheckOutTime = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colStatus = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colNotes = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.TableData = New DevExpress.Utils.Layout.TablePanel()
        Me.lblPersonType = New DevExpress.XtraEditors.LabelControl()
        Me.cboPersonType = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.lblPersonID = New DevExpress.XtraEditors.LabelControl()
        Me.cboPersonID = New DevExpress.XtraEditors.LookUpEdit()
        Me.lblAttendanceDate = New DevExpress.XtraEditors.LabelControl()
        Me.dteAttendanceDate = New DevExpress.XtraEditors.DateEdit()
        Me.lblCheckInTime = New DevExpress.XtraEditors.LabelControl()
        Me.tmeCheckIn = New DevExpress.XtraEditors.TimeEdit()
        Me.lblCheckOutTime = New DevExpress.XtraEditors.LabelControl()
        Me.tmeCheckOut = New DevExpress.XtraEditors.TimeEdit()
        Me.lblStatus = New DevExpress.XtraEditors.LabelControl()
        Me.cboStatus = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.lblNotes = New DevExpress.XtraEditors.LabelControl()
        Me.txtNotes = New DevExpress.XtraEditors.MemoEdit()
        Me.TLPBut = New DevExpress.Utils.Layout.TablePanel()
        Me.btnAdd = New DevExpress.XtraEditors.SimpleButton()
        Me.btnEdit = New DevExpress.XtraEditors.SimpleButton()
        Me.btnDelete = New DevExpress.XtraEditors.SimpleButton()
        Me.btnSave = New DevExpress.XtraEditors.SimpleButton()
        Me.btnCancel = New DevExpress.XtraEditors.SimpleButton()
        Me.btnClose = New DevExpress.XtraEditors.SimpleButton()
        CType(Me.Splitter1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Splitter1.Panel1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Splitter1.Panel1.SuspendLayout()
        CType(Me.Splitter1.Panel2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Splitter1.Panel2.SuspendLayout()
        Me.Splitter1.SuspendLayout()
        CType(Me.DGV, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgView, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TableData, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableData.SuspendLayout()
        CType(Me.cboPersonType.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboPersonID.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dteAttendanceDate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dteAttendanceDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tmeCheckIn.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tmeCheckOut.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboStatus.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNotes.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TLPBut, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TLPBut.SuspendLayout()
        Me.SuspendLayout()
        '
        'Splitter1
        '
        resources.ApplyResources(Me.Splitter1, "Splitter1")
        Me.Splitter1.CaptionImageOptions.ImageKey = resources.GetString("Splitter1.CaptionImageOptions.ImageKey")
        Me.Splitter1.Horizontal = False
        Me.Splitter1.Name = "Splitter1"
        '
        'Splitter1.Panel1
        '
        resources.ApplyResources(Me.Splitter1.Panel1, "Splitter1.Panel1")
        Me.Splitter1.Panel1.Controls.Add(Me.DGV)
        '
        'Splitter1.Panel2
        '
        resources.ApplyResources(Me.Splitter1.Panel2, "Splitter1.Panel2")
        Me.Splitter1.Panel2.Controls.Add(Me.TableData)
        Me.Splitter1.SplitterPosition = 280
        '
        'DGV
        '
        resources.ApplyResources(Me.DGV, "DGV")
        Me.DGV.EmbeddedNavigator.AccessibleDescription = resources.GetString("DGV.EmbeddedNavigator.AccessibleDescription")
        Me.DGV.EmbeddedNavigator.AccessibleName = resources.GetString("DGV.EmbeddedNavigator.AccessibleName")
        Me.DGV.EmbeddedNavigator.AllowHtmlTextInToolTip = CType(resources.GetObject("DGV.EmbeddedNavigator.AllowHtmlTextInToolTip"), DevExpress.Utils.DefaultBoolean)
        Me.DGV.EmbeddedNavigator.Anchor = CType(resources.GetObject("DGV.EmbeddedNavigator.Anchor"), System.Windows.Forms.AnchorStyles)
        Me.DGV.EmbeddedNavigator.AutoSize = CType(resources.GetObject("DGV.EmbeddedNavigator.AutoSize"), Boolean)
        Me.DGV.EmbeddedNavigator.BackgroundImage = CType(resources.GetObject("DGV.EmbeddedNavigator.BackgroundImage"), System.Drawing.Image)
        Me.DGV.EmbeddedNavigator.BackgroundImageLayout = CType(resources.GetObject("DGV.EmbeddedNavigator.BackgroundImageLayout"), System.Windows.Forms.ImageLayout)
        Me.DGV.EmbeddedNavigator.ImeMode = CType(resources.GetObject("DGV.EmbeddedNavigator.ImeMode"), System.Windows.Forms.ImeMode)
        Me.DGV.EmbeddedNavigator.MaximumSize = CType(resources.GetObject("DGV.EmbeddedNavigator.MaximumSize"), System.Drawing.Size)
        Me.DGV.EmbeddedNavigator.TextLocation = CType(resources.GetObject("DGV.EmbeddedNavigator.TextLocation"), DevExpress.XtraEditors.NavigatorButtonsTextLocation)
        Me.DGV.EmbeddedNavigator.ToolTip = resources.GetString("DGV.EmbeddedNavigator.ToolTip")
        Me.DGV.EmbeddedNavigator.ToolTipIconType = CType(resources.GetObject("DGV.EmbeddedNavigator.ToolTipIconType"), DevExpress.Utils.ToolTipIconType)
        Me.DGV.EmbeddedNavigator.ToolTipTitle = resources.GetString("DGV.EmbeddedNavigator.ToolTipTitle")
        Me.DGV.MainView = Me.dgView
        Me.DGV.Name = "DGV"
        Me.DGV.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.dgView})
        '
        'dgView
        '
        resources.ApplyResources(Me.dgView, "dgView")
        Me.dgView.Appearance.HeaderPanel.Font = CType(resources.GetObject("dgView.Appearance.HeaderPanel.Font"), System.Drawing.Font)
        Me.dgView.Appearance.HeaderPanel.Options.UseFont = True
        Me.dgView.Appearance.Row.Font = CType(resources.GetObject("dgView.Appearance.Row.Font"), System.Drawing.Font)
        Me.dgView.Appearance.Row.Options.UseFont = True
        Me.dgView.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.colRowNum, Me.colAttendanceID, Me.colPersonType, Me.colPersonID, Me.colPersonName, Me.colAttendanceDate, Me.colCheckInTime, Me.colCheckOutTime, Me.colStatus, Me.colNotes})
        Me.dgView.GridControl = Me.DGV
        Me.dgView.Name = "dgView"
        Me.dgView.OptionsBehavior.Editable = False
        Me.dgView.OptionsView.EnableAppearanceEvenRow = True
        Me.dgView.OptionsView.ShowFooter = True
        '
        'colRowNum
        '
        resources.ApplyResources(Me.colRowNum, "colRowNum")
        Me.colRowNum.FieldName = "colRowNum"
        Me.colRowNum.ImageOptions.ImageKey = resources.GetString("colRowNum.ImageOptions.ImageKey")
        Me.colRowNum.Name = "colRowNum"
        Me.colRowNum.UnboundDataType = GetType(Integer)
        '
        'colAttendanceID
        '
        resources.ApplyResources(Me.colAttendanceID, "colAttendanceID")
        Me.colAttendanceID.FieldName = "AttendanceID"
        Me.colAttendanceID.ImageOptions.ImageKey = resources.GetString("colAttendanceID.ImageOptions.ImageKey")
        Me.colAttendanceID.Name = "colAttendanceID"
        '
        'colPersonType
        '
        resources.ApplyResources(Me.colPersonType, "colPersonType")
        Me.colPersonType.FieldName = "PersonType"
        Me.colPersonType.ImageOptions.ImageKey = resources.GetString("colPersonType.ImageOptions.ImageKey")
        Me.colPersonType.Name = "colPersonType"
        '
        'colPersonID
        '
        resources.ApplyResources(Me.colPersonID, "colPersonID")
        Me.colPersonID.FieldName = "PersonID"
        Me.colPersonID.ImageOptions.ImageKey = resources.GetString("colPersonID.ImageOptions.ImageKey")
        Me.colPersonID.Name = "colPersonID"
        '
        'colPersonName
        '
        resources.ApplyResources(Me.colPersonName, "colPersonName")
        Me.colPersonName.FieldName = "PersonName"
        Me.colPersonName.ImageOptions.ImageKey = resources.GetString("colPersonName.ImageOptions.ImageKey")
        Me.colPersonName.Name = "colPersonName"
        '
        'colAttendanceDate
        '
        resources.ApplyResources(Me.colAttendanceDate, "colAttendanceDate")
        Me.colAttendanceDate.FieldName = "AttendanceDate"
        Me.colAttendanceDate.ImageOptions.ImageKey = resources.GetString("colAttendanceDate.ImageOptions.ImageKey")
        Me.colAttendanceDate.Name = "colAttendanceDate"
        '
        'colCheckInTime
        '
        resources.ApplyResources(Me.colCheckInTime, "colCheckInTime")
        Me.colCheckInTime.FieldName = "CheckInTime"
        Me.colCheckInTime.ImageOptions.ImageKey = resources.GetString("colCheckInTime.ImageOptions.ImageKey")
        Me.colCheckInTime.Name = "colCheckInTime"
        '
        'colCheckOutTime
        '
        resources.ApplyResources(Me.colCheckOutTime, "colCheckOutTime")
        Me.colCheckOutTime.FieldName = "CheckOutTime"
        Me.colCheckOutTime.ImageOptions.ImageKey = resources.GetString("colCheckOutTime.ImageOptions.ImageKey")
        Me.colCheckOutTime.Name = "colCheckOutTime"
        '
        'colStatus
        '
        resources.ApplyResources(Me.colStatus, "colStatus")
        Me.colStatus.FieldName = "Status"
        Me.colStatus.ImageOptions.ImageKey = resources.GetString("colStatus.ImageOptions.ImageKey")
        Me.colStatus.Name = "colStatus"
        '
        'colNotes
        '
        resources.ApplyResources(Me.colNotes, "colNotes")
        Me.colNotes.FieldName = "Notes"
        Me.colNotes.ImageOptions.ImageKey = resources.GetString("colNotes.ImageOptions.ImageKey")
        Me.colNotes.Name = "colNotes"
        '
        'TableData
        '
        resources.ApplyResources(Me.TableData, "TableData")
        Me.TableData.Columns.AddRange(New DevExpress.Utils.Layout.TablePanelColumn() {New DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 30.0!), New DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 70.0!), New DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 30.0!), New DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 70.0!), New DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 30.0!), New DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 70.0!)})
        Me.TableData.Controls.Add(Me.lblPersonType)
        Me.TableData.Controls.Add(Me.cboPersonType)
        Me.TableData.Controls.Add(Me.lblPersonID)
        Me.TableData.Controls.Add(Me.cboPersonID)
        Me.TableData.Controls.Add(Me.lblAttendanceDate)
        Me.TableData.Controls.Add(Me.dteAttendanceDate)
        Me.TableData.Controls.Add(Me.lblCheckInTime)
        Me.TableData.Controls.Add(Me.tmeCheckIn)
        Me.TableData.Controls.Add(Me.lblCheckOutTime)
        Me.TableData.Controls.Add(Me.tmeCheckOut)
        Me.TableData.Controls.Add(Me.lblStatus)
        Me.TableData.Controls.Add(Me.cboStatus)
        Me.TableData.Controls.Add(Me.lblNotes)
        Me.TableData.Controls.Add(Me.txtNotes)
        Me.TableData.Controls.Add(Me.TLPBut)
        Me.TableData.Name = "TableData"
        Me.TableData.Rows.AddRange(New DevExpress.Utils.Layout.TablePanelRow() {New DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 28.0!), New DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 28.0!), New DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 28.0!), New DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 56.0!), New DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 36.0!)})
        '
        'lblPersonType
        '
        resources.ApplyResources(Me.lblPersonType, "lblPersonType")
        Me.lblPersonType.Appearance.Font = CType(resources.GetObject("lblPersonType.Appearance.Font"), System.Drawing.Font)
        Me.lblPersonType.Appearance.Options.UseFont = True
        Me.lblPersonType.Appearance.Options.UseTextOptions = True
        Me.lblPersonType.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.lblPersonType.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.TableData.SetColumn(Me.lblPersonType, 0)
        Me.lblPersonType.Name = "lblPersonType"
        Me.TableData.SetRow(Me.lblPersonType, 0)
        '
        'cboPersonType
        '
        resources.ApplyResources(Me.cboPersonType, "cboPersonType")
        Me.TableData.SetColumn(Me.cboPersonType, 1)
        Me.cboPersonType.Name = "cboPersonType"
        Me.cboPersonType.Properties.Appearance.Font = CType(resources.GetObject("cboPersonType.Properties.Appearance.Font"), System.Drawing.Font)
        Me.cboPersonType.Properties.Appearance.Options.UseFont = True
        Me.cboPersonType.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("cboPersonType.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me.cboPersonType.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
        Me.TableData.SetRow(Me.cboPersonType, 0)
        '
        'lblPersonID
        '
        resources.ApplyResources(Me.lblPersonID, "lblPersonID")
        Me.lblPersonID.Appearance.Font = CType(resources.GetObject("lblPersonID.Appearance.Font"), System.Drawing.Font)
        Me.lblPersonID.Appearance.Options.UseFont = True
        Me.lblPersonID.Appearance.Options.UseTextOptions = True
        Me.lblPersonID.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.lblPersonID.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.TableData.SetColumn(Me.lblPersonID, 2)
        Me.lblPersonID.Name = "lblPersonID"
        Me.TableData.SetRow(Me.lblPersonID, 0)
        '
        'cboPersonID
        '
        resources.ApplyResources(Me.cboPersonID, "cboPersonID")
        Me.TableData.SetColumn(Me.cboPersonID, 3)
        Me.cboPersonID.Name = "cboPersonID"
        Me.cboPersonID.Properties.Appearance.Font = CType(resources.GetObject("cboPersonID.Properties.Appearance.Font"), System.Drawing.Font)
        Me.cboPersonID.Properties.Appearance.Options.UseFont = True
        Me.cboPersonID.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("cboPersonID.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me.cboPersonID.Properties.NullText = resources.GetString("cboPersonID.Properties.NullText")
        Me.TableData.SetRow(Me.cboPersonID, 0)
        '
        'lblAttendanceDate
        '
        resources.ApplyResources(Me.lblAttendanceDate, "lblAttendanceDate")
        Me.lblAttendanceDate.Appearance.Font = CType(resources.GetObject("lblAttendanceDate.Appearance.Font"), System.Drawing.Font)
        Me.lblAttendanceDate.Appearance.Options.UseFont = True
        Me.lblAttendanceDate.Appearance.Options.UseTextOptions = True
        Me.lblAttendanceDate.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.lblAttendanceDate.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.TableData.SetColumn(Me.lblAttendanceDate, 0)
        Me.lblAttendanceDate.Name = "lblAttendanceDate"
        Me.TableData.SetRow(Me.lblAttendanceDate, 1)
        '
        'dteAttendanceDate
        '
        resources.ApplyResources(Me.dteAttendanceDate, "dteAttendanceDate")
        Me.TableData.SetColumn(Me.dteAttendanceDate, 1)
        Me.dteAttendanceDate.Name = "dteAttendanceDate"
        Me.dteAttendanceDate.Properties.Appearance.Font = CType(resources.GetObject("dteAttendanceDate.Properties.Appearance.Font"), System.Drawing.Font)
        Me.dteAttendanceDate.Properties.Appearance.Options.UseFont = True
        Me.dteAttendanceDate.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("dteAttendanceDate.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me.dteAttendanceDate.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("dteAttendanceDate.Properties.CalendarTimeProperties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me.TableData.SetRow(Me.dteAttendanceDate, 1)
        '
        'lblCheckInTime
        '
        resources.ApplyResources(Me.lblCheckInTime, "lblCheckInTime")
        Me.lblCheckInTime.Appearance.Font = CType(resources.GetObject("lblCheckInTime.Appearance.Font"), System.Drawing.Font)
        Me.lblCheckInTime.Appearance.Options.UseFont = True
        Me.lblCheckInTime.Appearance.Options.UseTextOptions = True
        Me.lblCheckInTime.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.lblCheckInTime.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.TableData.SetColumn(Me.lblCheckInTime, 0)
        Me.lblCheckInTime.Name = "lblCheckInTime"
        Me.TableData.SetRow(Me.lblCheckInTime, 2)
        '
        'tmeCheckIn
        '
        resources.ApplyResources(Me.tmeCheckIn, "tmeCheckIn")
        Me.TableData.SetColumn(Me.tmeCheckIn, 1)
        Me.tmeCheckIn.Name = "tmeCheckIn"
        Me.tmeCheckIn.Properties.Appearance.Font = CType(resources.GetObject("tmeCheckIn.Properties.Appearance.Font"), System.Drawing.Font)
        Me.tmeCheckIn.Properties.Appearance.Options.UseFont = True
        Me.tmeCheckIn.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("tmeCheckIn.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me.TableData.SetRow(Me.tmeCheckIn, 2)
        '
        'lblCheckOutTime
        '
        resources.ApplyResources(Me.lblCheckOutTime, "lblCheckOutTime")
        Me.lblCheckOutTime.Appearance.Font = CType(resources.GetObject("lblCheckOutTime.Appearance.Font"), System.Drawing.Font)
        Me.lblCheckOutTime.Appearance.Options.UseFont = True
        Me.lblCheckOutTime.Appearance.Options.UseTextOptions = True
        Me.lblCheckOutTime.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.lblCheckOutTime.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.TableData.SetColumn(Me.lblCheckOutTime, 2)
        Me.lblCheckOutTime.Name = "lblCheckOutTime"
        Me.TableData.SetRow(Me.lblCheckOutTime, 2)
        '
        'tmeCheckOut
        '
        resources.ApplyResources(Me.tmeCheckOut, "tmeCheckOut")
        Me.TableData.SetColumn(Me.tmeCheckOut, 3)
        Me.tmeCheckOut.Name = "tmeCheckOut"
        Me.tmeCheckOut.Properties.Appearance.Font = CType(resources.GetObject("tmeCheckOut.Properties.Appearance.Font"), System.Drawing.Font)
        Me.tmeCheckOut.Properties.Appearance.Options.UseFont = True
        Me.tmeCheckOut.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("tmeCheckOut.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me.TableData.SetRow(Me.tmeCheckOut, 2)
        '
        'lblStatus
        '
        resources.ApplyResources(Me.lblStatus, "lblStatus")
        Me.lblStatus.Appearance.Font = CType(resources.GetObject("lblStatus.Appearance.Font"), System.Drawing.Font)
        Me.lblStatus.Appearance.Options.UseFont = True
        Me.lblStatus.Appearance.Options.UseTextOptions = True
        Me.lblStatus.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.lblStatus.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.TableData.SetColumn(Me.lblStatus, 2)
        Me.lblStatus.Name = "lblStatus"
        Me.TableData.SetRow(Me.lblStatus, 1)
        '
        'cboStatus
        '
        resources.ApplyResources(Me.cboStatus, "cboStatus")
        Me.TableData.SetColumn(Me.cboStatus, 3)
        Me.cboStatus.Name = "cboStatus"
        Me.cboStatus.Properties.Appearance.Font = CType(resources.GetObject("cboStatus.Properties.Appearance.Font"), System.Drawing.Font)
        Me.cboStatus.Properties.Appearance.Options.UseFont = True
        Me.cboStatus.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("cboStatus.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me.cboStatus.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
        Me.TableData.SetRow(Me.cboStatus, 1)
        '
        'lblNotes
        '
        resources.ApplyResources(Me.lblNotes, "lblNotes")
        Me.lblNotes.Appearance.Font = CType(resources.GetObject("lblNotes.Appearance.Font"), System.Drawing.Font)
        Me.lblNotes.Appearance.Options.UseFont = True
        Me.lblNotes.Appearance.Options.UseTextOptions = True
        Me.lblNotes.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.lblNotes.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top
        Me.TableData.SetColumn(Me.lblNotes, 0)
        Me.lblNotes.Name = "lblNotes"
        Me.TableData.SetRow(Me.lblNotes, 3)
        '
        'txtNotes
        '
        resources.ApplyResources(Me.txtNotes, "txtNotes")
        Me.TableData.SetColumn(Me.txtNotes, 1)
        Me.TableData.SetColumnSpan(Me.txtNotes, 5)
        Me.txtNotes.Name = "txtNotes"
        Me.txtNotes.Properties.Appearance.Font = CType(resources.GetObject("txtNotes.Properties.Appearance.Font"), System.Drawing.Font)
        Me.txtNotes.Properties.Appearance.Options.UseFont = True
        Me.txtNotes.Properties.MaxLength = 500
        Me.TableData.SetRow(Me.txtNotes, 3)
        '
        'TLPBut
        '
        resources.ApplyResources(Me.TLPBut, "TLPBut")
        Me.TableData.SetColumn(Me.TLPBut, 0)
        Me.TLPBut.Columns.AddRange(New DevExpress.Utils.Layout.TablePanelColumn() {New DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 50.0!), New DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 80.0!), New DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 80.0!), New DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 80.0!), New DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 80.0!), New DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 80.0!), New DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 50.0!)})
        Me.TableData.SetColumnSpan(Me.TLPBut, 6)
        Me.TLPBut.Controls.Add(Me.btnAdd)
        Me.TLPBut.Controls.Add(Me.btnEdit)
        Me.TLPBut.Controls.Add(Me.btnDelete)
        Me.TLPBut.Controls.Add(Me.btnSave)
        Me.TLPBut.Controls.Add(Me.btnCancel)
        Me.TLPBut.Controls.Add(Me.btnClose)
        Me.TLPBut.Name = "TLPBut"
        Me.TableData.SetRow(Me.TLPBut, 4)
        Me.TLPBut.Rows.AddRange(New DevExpress.Utils.Layout.TablePanelRow() {New DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 32.0!)})
        '
        'btnAdd
        '
        resources.ApplyResources(Me.btnAdd, "btnAdd")
        Me.btnAdd.Appearance.Font = CType(resources.GetObject("btnAdd.Appearance.Font"), System.Drawing.Font)
        Me.btnAdd.Appearance.Options.UseFont = True
        Me.TLPBut.SetColumn(Me.btnAdd, 1)
        Me.btnAdd.ImageOptions.ImageKey = resources.GetString("btnAdd.ImageOptions.ImageKey")
        Me.btnAdd.Name = "btnAdd"
        Me.TLPBut.SetRow(Me.btnAdd, 0)
        '
        'btnEdit
        '
        resources.ApplyResources(Me.btnEdit, "btnEdit")
        Me.btnEdit.Appearance.Font = CType(resources.GetObject("btnEdit.Appearance.Font"), System.Drawing.Font)
        Me.btnEdit.Appearance.Options.UseFont = True
        Me.TLPBut.SetColumn(Me.btnEdit, 2)
        Me.btnEdit.ImageOptions.ImageKey = resources.GetString("btnEdit.ImageOptions.ImageKey")
        Me.btnEdit.Name = "btnEdit"
        Me.TLPBut.SetRow(Me.btnEdit, 0)
        '
        'btnDelete
        '
        resources.ApplyResources(Me.btnDelete, "btnDelete")
        Me.btnDelete.Appearance.Font = CType(resources.GetObject("btnDelete.Appearance.Font"), System.Drawing.Font)
        Me.btnDelete.Appearance.Options.UseFont = True
        Me.TLPBut.SetColumn(Me.btnDelete, 3)
        Me.btnDelete.ImageOptions.ImageKey = resources.GetString("btnDelete.ImageOptions.ImageKey")
        Me.btnDelete.Name = "btnDelete"
        Me.TLPBut.SetRow(Me.btnDelete, 0)
        '
        'btnSave
        '
        resources.ApplyResources(Me.btnSave, "btnSave")
        Me.btnSave.Appearance.Font = CType(resources.GetObject("btnSave.Appearance.Font"), System.Drawing.Font)
        Me.btnSave.Appearance.Options.UseFont = True
        Me.TLPBut.SetColumn(Me.btnSave, 4)
        Me.btnSave.ImageOptions.ImageKey = resources.GetString("btnSave.ImageOptions.ImageKey")
        Me.btnSave.Name = "btnSave"
        Me.TLPBut.SetRow(Me.btnSave, 0)
        '
        'btnCancel
        '
        resources.ApplyResources(Me.btnCancel, "btnCancel")
        Me.btnCancel.Appearance.Font = CType(resources.GetObject("btnCancel.Appearance.Font"), System.Drawing.Font)
        Me.btnCancel.Appearance.Options.UseFont = True
        Me.TLPBut.SetColumn(Me.btnCancel, 5)
        Me.btnCancel.ImageOptions.ImageKey = resources.GetString("btnCancel.ImageOptions.ImageKey")
        Me.btnCancel.Name = "btnCancel"
        Me.TLPBut.SetRow(Me.btnCancel, 0)
        '
        'btnClose
        '
        resources.ApplyResources(Me.btnClose, "btnClose")
        Me.btnClose.Appearance.Font = CType(resources.GetObject("btnClose.Appearance.Font"), System.Drawing.Font)
        Me.btnClose.Appearance.Options.UseFont = True
        Me.TLPBut.SetColumn(Me.btnClose, 6)
        Me.btnClose.ImageOptions.ImageKey = resources.GetString("btnClose.ImageOptions.ImageKey")
        Me.btnClose.Name = "btnClose"
        Me.TLPBut.SetRow(Me.btnClose, 0)
        '
        'PersonnelAttendance
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.Splitter1)
        Me.Name = "PersonnelAttendance"
        CType(Me.Splitter1.Panel1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Splitter1.Panel1.ResumeLayout(False)
        CType(Me.Splitter1.Panel2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Splitter1.Panel2.ResumeLayout(False)
        CType(Me.Splitter1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Splitter1.ResumeLayout(False)
        CType(Me.DGV, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgView, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TableData, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableData.ResumeLayout(False)
        Me.TableData.PerformLayout()
        CType(Me.cboPersonType.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboPersonID.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dteAttendanceDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dteAttendanceDate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tmeCheckIn.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tmeCheckOut.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboStatus.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtNotes.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TLPBut, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TLPBut.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Splitter1 As DevExpress.XtraEditors.SplitContainerControl
    Friend WithEvents DGV As DevExpress.XtraGrid.GridControl
    Friend WithEvents dgView As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents colRowNum As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colAttendanceID As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colPersonType As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colPersonID As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colPersonName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colAttendanceDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colCheckInTime As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colCheckOutTime As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colStatus As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colNotes As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents TableData As DevExpress.Utils.Layout.TablePanel
    Friend WithEvents lblPersonType As DevExpress.XtraEditors.LabelControl
    Friend WithEvents cboPersonType As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents lblPersonID As DevExpress.XtraEditors.LabelControl
    Friend WithEvents cboPersonID As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents lblAttendanceDate As DevExpress.XtraEditors.LabelControl
    Friend WithEvents dteAttendanceDate As DevExpress.XtraEditors.DateEdit
    Friend WithEvents lblCheckInTime As DevExpress.XtraEditors.LabelControl
    Friend WithEvents tmeCheckIn As DevExpress.XtraEditors.TimeEdit
    Friend WithEvents lblCheckOutTime As DevExpress.XtraEditors.LabelControl
    Friend WithEvents tmeCheckOut As DevExpress.XtraEditors.TimeEdit
    Friend WithEvents lblStatus As DevExpress.XtraEditors.LabelControl
    Friend WithEvents cboStatus As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents lblNotes As DevExpress.XtraEditors.LabelControl
    Friend WithEvents txtNotes As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents TLPBut As DevExpress.Utils.Layout.TablePanel
    Friend WithEvents btnAdd As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnEdit As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnDelete As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnSave As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnCancel As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnClose As DevExpress.XtraEditors.SimpleButton
End Class
