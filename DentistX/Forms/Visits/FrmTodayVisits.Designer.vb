<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmTodayVisits
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmTodayVisits))
        Me.PanelControl1 = New DevExpress.XtraEditors.PanelControl()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.PanelControl2 = New DevExpress.XtraEditors.PanelControl()
        Me.btn2Dates = New DevExpress.XtraEditors.SimpleButton()
        Me.dtEndDate = New DevExpress.XtraEditors.DateEdit()
        Me.LabelControl4 = New DevExpress.XtraEditors.LabelControl()
        Me.dtStartDate = New DevExpress.XtraEditors.DateEdit()
        Me.LabelControl3 = New DevExpress.XtraEditors.LabelControl()
        Me.btnOnDate = New DevExpress.XtraEditors.SimpleButton()
        Me.dtOnDate = New DevExpress.XtraEditors.DateEdit()
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        Me.PanelControl3 = New DevExpress.XtraEditors.PanelControl()
        Me.todayGrid = New DevExpress.XtraGrid.GridControl()
        Me.VisBS = New System.Windows.Forms.BindingSource(Me.components)
        Me.GridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.colVisitDetID = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colPatientID = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colPatientName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colVisitType = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colVisitDay = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colVisTime = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colVisTimeEnd = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colVisDetail = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colVisNotes = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colVisDateTime = New DevExpress.XtraGrid.Columns.GridColumn()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl1.SuspendLayout()
        CType(Me.PanelControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl2.SuspendLayout()
        CType(Me.dtEndDate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtEndDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtStartDate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtStartDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtOnDate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtOnDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PanelControl3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl3.SuspendLayout()
        CType(Me.todayGrid, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.VisBS, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'PanelControl1
        '
        resources.ApplyResources(Me.PanelControl1, "PanelControl1")
        Me.PanelControl1.Controls.Add(Me.LabelControl1)
        Me.PanelControl1.Name = "PanelControl1"
        '
        'LabelControl1
        '
        resources.ApplyResources(Me.LabelControl1, "LabelControl1")
        Me.LabelControl1.Appearance.Font = CType(resources.GetObject("LabelControl1.Appearance.Font"), System.Drawing.Font)
        Me.LabelControl1.Appearance.Options.UseFont = True
        Me.LabelControl1.Name = "LabelControl1"
        '
        'PanelControl2
        '
        resources.ApplyResources(Me.PanelControl2, "PanelControl2")
        Me.PanelControl2.Controls.Add(Me.btn2Dates)
        Me.PanelControl2.Controls.Add(Me.dtEndDate)
        Me.PanelControl2.Controls.Add(Me.LabelControl4)
        Me.PanelControl2.Controls.Add(Me.dtStartDate)
        Me.PanelControl2.Controls.Add(Me.LabelControl3)
        Me.PanelControl2.Controls.Add(Me.btnOnDate)
        Me.PanelControl2.Controls.Add(Me.dtOnDate)
        Me.PanelControl2.Controls.Add(Me.LabelControl2)
        Me.PanelControl2.Name = "PanelControl2"
        '
        'btn2Dates
        '
        resources.ApplyResources(Me.btn2Dates, "btn2Dates")
        Me.btn2Dates.Appearance.Font = CType(resources.GetObject("btn2Dates.Appearance.Font"), System.Drawing.Font)
        Me.btn2Dates.Appearance.Options.UseFont = True
        Me.btn2Dates.ImageOptions.ImageKey = resources.GetString("btn2Dates.ImageOptions.ImageKey")
        Me.btn2Dates.Name = "btn2Dates"
        '
        'dtEndDate
        '
        resources.ApplyResources(Me.dtEndDate, "dtEndDate")
        Me.dtEndDate.Name = "dtEndDate"
        Me.dtEndDate.Properties.Appearance.Font = CType(resources.GetObject("dtEndDate.Properties.Appearance.Font"), System.Drawing.Font)
        Me.dtEndDate.Properties.Appearance.Options.UseFont = True
        Me.dtEndDate.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("dtEndDate.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me.dtEndDate.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("dtEndDate.Properties.CalendarTimeProperties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        '
        'LabelControl4
        '
        resources.ApplyResources(Me.LabelControl4, "LabelControl4")
        Me.LabelControl4.Appearance.Font = CType(resources.GetObject("LabelControl4.Appearance.Font"), System.Drawing.Font)
        Me.LabelControl4.Appearance.Options.UseFont = True
        Me.LabelControl4.Name = "LabelControl4"
        '
        'dtStartDate
        '
        resources.ApplyResources(Me.dtStartDate, "dtStartDate")
        Me.dtStartDate.Name = "dtStartDate"
        Me.dtStartDate.Properties.Appearance.Font = CType(resources.GetObject("dtStartDate.Properties.Appearance.Font"), System.Drawing.Font)
        Me.dtStartDate.Properties.Appearance.Options.UseFont = True
        Me.dtStartDate.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("dtStartDate.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me.dtStartDate.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("dtStartDate.Properties.CalendarTimeProperties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        '
        'LabelControl3
        '
        resources.ApplyResources(Me.LabelControl3, "LabelControl3")
        Me.LabelControl3.Appearance.Font = CType(resources.GetObject("LabelControl3.Appearance.Font"), System.Drawing.Font)
        Me.LabelControl3.Appearance.Options.UseFont = True
        Me.LabelControl3.Name = "LabelControl3"
        '
        'btnOnDate
        '
        resources.ApplyResources(Me.btnOnDate, "btnOnDate")
        Me.btnOnDate.Appearance.Font = CType(resources.GetObject("btnOnDate.Appearance.Font"), System.Drawing.Font)
        Me.btnOnDate.Appearance.Options.UseFont = True
        Me.btnOnDate.ImageOptions.ImageKey = resources.GetString("btnOnDate.ImageOptions.ImageKey")
        Me.btnOnDate.Name = "btnOnDate"
        '
        'dtOnDate
        '
        resources.ApplyResources(Me.dtOnDate, "dtOnDate")
        Me.dtOnDate.Name = "dtOnDate"
        Me.dtOnDate.Properties.Appearance.Font = CType(resources.GetObject("dtOnDate.Properties.Appearance.Font"), System.Drawing.Font)
        Me.dtOnDate.Properties.Appearance.Options.UseFont = True
        Me.dtOnDate.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("dtOnDate.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me.dtOnDate.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("dtOnDate.Properties.CalendarTimeProperties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        '
        'LabelControl2
        '
        resources.ApplyResources(Me.LabelControl2, "LabelControl2")
        Me.LabelControl2.Appearance.Font = CType(resources.GetObject("LabelControl2.Appearance.Font"), System.Drawing.Font)
        Me.LabelControl2.Appearance.Options.UseFont = True
        Me.LabelControl2.Name = "LabelControl2"
        '
        'PanelControl3
        '
        resources.ApplyResources(Me.PanelControl3, "PanelControl3")
        Me.PanelControl3.Controls.Add(Me.todayGrid)
        Me.PanelControl3.Name = "PanelControl3"
        '
        'todayGrid
        '
        resources.ApplyResources(Me.todayGrid, "todayGrid")
        Me.todayGrid.DataSource = Me.VisBS
        Me.todayGrid.EmbeddedNavigator.AccessibleDescription = resources.GetString("todayGrid.EmbeddedNavigator.AccessibleDescription")
        Me.todayGrid.EmbeddedNavigator.AccessibleName = resources.GetString("todayGrid.EmbeddedNavigator.AccessibleName")
        Me.todayGrid.EmbeddedNavigator.AllowHtmlTextInToolTip = CType(resources.GetObject("todayGrid.EmbeddedNavigator.AllowHtmlTextInToolTip"), DevExpress.Utils.DefaultBoolean)
        Me.todayGrid.EmbeddedNavigator.Anchor = CType(resources.GetObject("todayGrid.EmbeddedNavigator.Anchor"), System.Windows.Forms.AnchorStyles)
        Me.todayGrid.EmbeddedNavigator.AutoSize = CType(resources.GetObject("todayGrid.EmbeddedNavigator.AutoSize"), Boolean)
        Me.todayGrid.EmbeddedNavigator.BackgroundImage = CType(resources.GetObject("todayGrid.EmbeddedNavigator.BackgroundImage"), System.Drawing.Image)
        Me.todayGrid.EmbeddedNavigator.BackgroundImageLayout = CType(resources.GetObject("todayGrid.EmbeddedNavigator.BackgroundImageLayout"), System.Windows.Forms.ImageLayout)
        Me.todayGrid.EmbeddedNavigator.ImeMode = CType(resources.GetObject("todayGrid.EmbeddedNavigator.ImeMode"), System.Windows.Forms.ImeMode)
        Me.todayGrid.EmbeddedNavigator.MaximumSize = CType(resources.GetObject("todayGrid.EmbeddedNavigator.MaximumSize"), System.Drawing.Size)
        Me.todayGrid.EmbeddedNavigator.TextLocation = CType(resources.GetObject("todayGrid.EmbeddedNavigator.TextLocation"), DevExpress.XtraEditors.NavigatorButtonsTextLocation)
        Me.todayGrid.EmbeddedNavigator.ToolTip = resources.GetString("todayGrid.EmbeddedNavigator.ToolTip")
        Me.todayGrid.EmbeddedNavigator.ToolTipIconType = CType(resources.GetObject("todayGrid.EmbeddedNavigator.ToolTipIconType"), DevExpress.Utils.ToolTipIconType)
        Me.todayGrid.EmbeddedNavigator.ToolTipTitle = resources.GetString("todayGrid.EmbeddedNavigator.ToolTipTitle")
        Me.todayGrid.MainView = Me.GridView1
        Me.todayGrid.Name = "todayGrid"
        Me.todayGrid.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridView1})
        '
        'GridView1
        '
        resources.ApplyResources(Me.GridView1, "GridView1")
        Me.GridView1.Appearance.HeaderPanel.Font = CType(resources.GetObject("GridView1.Appearance.HeaderPanel.Font"), System.Drawing.Font)
        Me.GridView1.Appearance.HeaderPanel.Options.UseFont = True
        Me.GridView1.Appearance.Row.Font = CType(resources.GetObject("GridView1.Appearance.Row.Font"), System.Drawing.Font)
        Me.GridView1.Appearance.Row.Options.UseFont = True
        Me.GridView1.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.colVisitDetID, Me.colPatientID, Me.colPatientName, Me.colVisitType, Me.colVisitDay, Me.colVisTime, Me.colVisTimeEnd, Me.colVisDetail, Me.colVisNotes, Me.colVisDateTime})
        Me.GridView1.GridControl = Me.todayGrid
        Me.GridView1.Name = "GridView1"
        '
        'colVisitDetID
        '
        resources.ApplyResources(Me.colVisitDetID, "colVisitDetID")
        Me.colVisitDetID.FieldName = "VisitDetID"
        Me.colVisitDetID.ImageOptions.ImageKey = resources.GetString("colVisitDetID.ImageOptions.ImageKey")
        Me.colVisitDetID.Name = "colVisitDetID"
        Me.colVisitDetID.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem()})
        '
        'colPatientID
        '
        resources.ApplyResources(Me.colPatientID, "colPatientID")
        Me.colPatientID.FieldName = "PatientID"
        Me.colPatientID.ImageOptions.ImageKey = resources.GetString("colPatientID.ImageOptions.ImageKey")
        Me.colPatientID.Name = "colPatientID"
        '
        'colPatientName
        '
        resources.ApplyResources(Me.colPatientName, "colPatientName")
        Me.colPatientName.FieldName = "PatientName"
        Me.colPatientName.ImageOptions.ImageKey = resources.GetString("colPatientName.ImageOptions.ImageKey")
        Me.colPatientName.Name = "colPatientName"
        Me.colPatientName.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem()})
        '
        'colVisitType
        '
        resources.ApplyResources(Me.colVisitType, "colVisitType")
        Me.colVisitType.FieldName = "VisitType"
        Me.colVisitType.ImageOptions.ImageKey = resources.GetString("colVisitType.ImageOptions.ImageKey")
        Me.colVisitType.Name = "colVisitType"
        Me.colVisitType.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem()})
        '
        'colVisitDay
        '
        resources.ApplyResources(Me.colVisitDay, "colVisitDay")
        Me.colVisitDay.FieldName = "VisitDay"
        Me.colVisitDay.ImageOptions.ImageKey = resources.GetString("colVisitDay.ImageOptions.ImageKey")
        Me.colVisitDay.Name = "colVisitDay"
        Me.colVisitDay.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem()})
        '
        'colVisTime
        '
        resources.ApplyResources(Me.colVisTime, "colVisTime")
        Me.colVisTime.FieldName = "VisTime"
        Me.colVisTime.ImageOptions.ImageKey = resources.GetString("colVisTime.ImageOptions.ImageKey")
        Me.colVisTime.Name = "colVisTime"
        Me.colVisTime.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem()})
        '
        'colVisTimeEnd
        '
        resources.ApplyResources(Me.colVisTimeEnd, "colVisTimeEnd")
        Me.colVisTimeEnd.FieldName = "VisTimeEnd"
        Me.colVisTimeEnd.ImageOptions.ImageKey = resources.GetString("colVisTimeEnd.ImageOptions.ImageKey")
        Me.colVisTimeEnd.Name = "colVisTimeEnd"
        Me.colVisTimeEnd.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem()})
        '
        'colVisDetail
        '
        resources.ApplyResources(Me.colVisDetail, "colVisDetail")
        Me.colVisDetail.FieldName = "VisDetail"
        Me.colVisDetail.ImageOptions.ImageKey = resources.GetString("colVisDetail.ImageOptions.ImageKey")
        Me.colVisDetail.Name = "colVisDetail"
        Me.colVisDetail.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem()})
        '
        'colVisNotes
        '
        resources.ApplyResources(Me.colVisNotes, "colVisNotes")
        Me.colVisNotes.FieldName = "VisNotes"
        Me.colVisNotes.ImageOptions.ImageKey = resources.GetString("colVisNotes.ImageOptions.ImageKey")
        Me.colVisNotes.Name = "colVisNotes"
        Me.colVisNotes.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem()})
        '
        'colVisDateTime
        '
        resources.ApplyResources(Me.colVisDateTime, "colVisDateTime")
        Me.colVisDateTime.FieldName = "VisDateTime"
        Me.colVisDateTime.ImageOptions.ImageKey = resources.GetString("colVisDateTime.ImageOptions.ImageKey")
        Me.colVisDateTime.Name = "colVisDateTime"
        '
        'FrmTodayVisits
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.PanelControl3)
        Me.Controls.Add(Me.PanelControl2)
        Me.Controls.Add(Me.PanelControl1)
        Me.Name = "FrmTodayVisits"
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl1.ResumeLayout(False)
        Me.PanelControl1.PerformLayout()
        CType(Me.PanelControl2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl2.ResumeLayout(False)
        Me.PanelControl2.PerformLayout()
        CType(Me.dtEndDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtEndDate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtStartDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtStartDate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtOnDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtOnDate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PanelControl3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl3.ResumeLayout(False)
        CType(Me.todayGrid, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.VisBS, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents PanelControl1 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents PanelControl2 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents PanelControl3 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents todayGrid As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridView1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents VisBS As BindingSource
    Friend WithEvents colVisitDetID As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colPatientID As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colPatientName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colVisitType As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colVisitDay As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colVisTime As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colVisTimeEnd As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colVisDetail As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colVisNotes As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colVisDateTime As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents btn2Dates As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents dtEndDate As DevExpress.XtraEditors.DateEdit
    Friend WithEvents LabelControl4 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents dtStartDate As DevExpress.XtraEditors.DateEdit
    Friend WithEvents LabelControl3 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents btnOnDate As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents dtOnDate As DevExpress.XtraEditors.DateEdit
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
End Class
