<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class TodayApptEditorForm
    Inherits DevExpress.XtraEditors.XtraForm

    'Form overrides dispose to clean up the component list.
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(TodayApptEditorForm))
        Me.LabelControl7 = New DevExpress.XtraEditors.LabelControl()
        Me.btnSave = New DevExpress.XtraEditors.SimpleButton()
        Me.apptDate = New DevExpress.XtraEditors.DateEdit()
        Me.PanelControl1 = New DevExpress.XtraEditors.PanelControl()
        Me.gridResults = New DevExpress.XtraGrid.GridControl()
        Me.dgView = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.colRowNum = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colAppID = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colFromTo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colPaient = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colDoctor = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colDetail = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colStatus = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.btnCancel = New DevExpress.XtraEditors.SimpleButton()
        CType(Me.apptDate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.apptDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl1.SuspendLayout()
        CType(Me.gridResults, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'LabelControl7
        '
        Me.LabelControl7.Appearance.Font = CType(resources.GetObject("LabelControl7.Appearance.Font"), System.Drawing.Font)
        Me.LabelControl7.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.LabelControl7, "LabelControl7")
        Me.LabelControl7.Name = "LabelControl7"
        '
        'btnSave
        '
        Me.btnSave.Appearance.Font = CType(resources.GetObject("btnSave.Appearance.Font"), System.Drawing.Font)
        Me.btnSave.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.btnSave, "btnSave")
        Me.btnSave.Name = "btnSave"
        '
        'apptDate
        '
        resources.ApplyResources(Me.apptDate, "apptDate")
        Me.apptDate.EnterMoveNextControl = True
        Me.apptDate.Name = "apptDate"
        Me.apptDate.Properties.Appearance.Font = CType(resources.GetObject("apptDate.Properties.Appearance.Font"), System.Drawing.Font)
        Me.apptDate.Properties.Appearance.Options.UseFont = True
        Me.apptDate.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("apptDate.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me.apptDate.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("apptDate.Properties.CalendarTimeProperties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me.apptDate.Properties.CalendarView = DevExpress.XtraEditors.Repository.CalendarView.TouchUI
        Me.apptDate.Properties.DisplayFormat.FormatString = "dd/MM/yyyy"
        Me.apptDate.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.apptDate.Properties.FirstDayOfWeek = System.DayOfWeek.Saturday
        Me.apptDate.Properties.VistaDisplayMode = DevExpress.Utils.DefaultBoolean.[False]
        '
        'PanelControl1
        '
        Me.PanelControl1.Controls.Add(Me.gridResults)
        resources.ApplyResources(Me.PanelControl1, "PanelControl1")
        Me.PanelControl1.Name = "PanelControl1"
        '
        'gridResults
        '
        resources.ApplyResources(Me.gridResults, "gridResults")
        Me.gridResults.MainView = Me.dgView
        Me.gridResults.Name = "gridResults"
        Me.gridResults.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.dgView})
        '
        'dgView
        '
        Me.dgView.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.colRowNum, Me.colAppID, Me.colDate, Me.colFromTo, Me.colPaient, Me.colDoctor, Me.colDetail, Me.colStatus})
        Me.dgView.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFullFocus
        Me.dgView.GridControl = Me.gridResults
        Me.dgView.Name = "dgView"
        Me.dgView.OptionsBehavior.Editable = False
        Me.dgView.OptionsBehavior.FocusLeaveOnTab = True
        '
        'colRowNum
        '
        resources.ApplyResources(Me.colRowNum, "colRowNum")
        Me.colRowNum.FieldName = "Num"
        Me.colRowNum.Name = "colRowNum"
        Me.colRowNum.UnboundDataType = GetType(Integer)
        '
        'colAppID
        '
        resources.ApplyResources(Me.colAppID, "colAppID")
        Me.colAppID.FieldName = "AppointmentID"
        Me.colAppID.Name = "colAppID"
        '
        'colDate
        '
        resources.ApplyResources(Me.colDate, "colDate")
        Me.colDate.FieldName = "ApptDate"
        Me.colDate.Name = "colDate"
        '
        'colFromTo
        '
        resources.ApplyResources(Me.colFromTo, "colFromTo")
        Me.colFromTo.FieldName = "FromTo"
        Me.colFromTo.Name = "colFromTo"
        '
        'colPaient
        '
        resources.ApplyResources(Me.colPaient, "colPaient")
        Me.colPaient.FieldName = "PatientName"
        Me.colPaient.Name = "colPaient"
        '
        'colDoctor
        '
        resources.ApplyResources(Me.colDoctor, "colDoctor")
        Me.colDoctor.FieldName = "DoctorName"
        Me.colDoctor.Name = "colDoctor"
        '
        'colDetail
        '
        resources.ApplyResources(Me.colDetail, "colDetail")
        Me.colDetail.FieldName = "Detail"
        Me.colDetail.Name = "colDetail"
        '
        'colStatus
        '
        resources.ApplyResources(Me.colStatus, "colStatus")
        Me.colStatus.FieldName = "Status"
        Me.colStatus.Name = "colStatus"
        '
        'btnCancel
        '
        Me.btnCancel.Appearance.Font = CType(resources.GetObject("btnCancel.Appearance.Font"), System.Drawing.Font)
        Me.btnCancel.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.btnCancel, "btnCancel")
        Me.btnCancel.Name = "btnCancel"
        '
        'TodayApptEditorForm
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.PanelControl1)
        Me.Controls.Add(Me.btnSave)
        Me.Controls.Add(Me.apptDate)
        Me.Controls.Add(Me.LabelControl7)
        Me.Name = "TodayApptEditorForm"
        CType(Me.apptDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.apptDate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl1.ResumeLayout(False)
        CType(Me.gridResults, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents LabelControl7 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents btnSave As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents apptDate As DevExpress.XtraEditors.DateEdit
    Friend WithEvents PanelControl1 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents gridResults As DevExpress.XtraGrid.GridControl
    Friend WithEvents dgView As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents colRowNum As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colAppID As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colFromTo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colPaient As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colDoctor As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colDetail As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colStatus As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents btnCancel As DevExpress.XtraEditors.SimpleButton
End Class
