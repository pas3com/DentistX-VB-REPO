<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmLastPatients
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmLastPatients))
        Me.GridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.colPatientID = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colPatientName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colLastAppt = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colLastOtherTrt = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colLastDiagTreat = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colLastOrthoWireNotes = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colLastOrthoKhota = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colLastNote = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colLastRX = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colLastToothTreatment = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colLastTrtDetail = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colLastActivityDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.lastPatientsGrid = New DevExpress.XtraGrid.GridControl()
        Me.PatientBS = New System.Windows.Forms.BindingSource(Me.components)
        Me.gridPanel = New DevExpress.XtraEditors.PanelControl()
        Me.btnClose = New DevExpress.XtraEditors.SimpleButton()
        Me.botmPanel = New DevExpress.XtraEditors.PanelControl()
        Me.hdrPanel = New DevExpress.XtraEditors.PanelControl()
        Me.btnListPatients = New DevExpress.XtraEditors.SimpleButton()
        Me.txLastNDays = New DevExpress.XtraEditors.TextEdit()
        Me.txTopN = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl4 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl3 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lastPatientsGrid, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PatientBS, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gridPanel, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gridPanel.SuspendLayout()
        CType(Me.botmPanel, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.botmPanel.SuspendLayout()
        CType(Me.hdrPanel, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.hdrPanel.SuspendLayout()
        CType(Me.txLastNDays.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txTopN.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GridView1
        '
        Me.GridView1.Appearance.HeaderPanel.Font = CType(resources.GetObject("GridView1.Appearance.HeaderPanel.Font"), System.Drawing.Font)
        Me.GridView1.Appearance.HeaderPanel.Options.UseFont = True
        Me.GridView1.Appearance.Row.Font = CType(resources.GetObject("GridView1.Appearance.Row.Font"), System.Drawing.Font)
        Me.GridView1.Appearance.Row.Options.UseFont = True
        Me.GridView1.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.colPatientID, Me.colPatientName, Me.colLastAppt, Me.colLastOtherTrt, Me.colLastDiagTreat, Me.colLastOrthoWireNotes, Me.colLastOrthoKhota, Me.colLastNote, Me.colLastRX, Me.colLastToothTreatment, Me.colLastTrtDetail, Me.colLastActivityDate})
        Me.GridView1.GridControl = Me.lastPatientsGrid
        Me.GridView1.Name = "GridView1"
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
        'colLastAppt
        '
        resources.ApplyResources(Me.colLastAppt, "colLastAppt")
        Me.colLastAppt.FieldName = "LastApptReason"
        Me.colLastAppt.Name = "colLastAppt"
        '
        'colLastOtherTrt
        '
        resources.ApplyResources(Me.colLastOtherTrt, "colLastOtherTrt")
        Me.colLastOtherTrt.FieldName = "LastOtherTrt"
        Me.colLastOtherTrt.Name = "colLastOtherTrt"
        '
        'colLastDiagTreat
        '
        resources.ApplyResources(Me.colLastDiagTreat, "colLastDiagTreat")
        Me.colLastDiagTreat.FieldName = "LastDiagTreat"
        Me.colLastDiagTreat.Name = "colLastDiagTreat"
        '
        'colLastOrthoWireNotes
        '
        resources.ApplyResources(Me.colLastOrthoWireNotes, "colLastOrthoWireNotes")
        Me.colLastOrthoWireNotes.FieldName = "LastOrthoWireNotes"
        Me.colLastOrthoWireNotes.Name = "colLastOrthoWireNotes"
        '
        'colLastOrthoKhota
        '
        resources.ApplyResources(Me.colLastOrthoKhota, "colLastOrthoKhota")
        Me.colLastOrthoKhota.FieldName = "LastOrthoKhota"
        Me.colLastOrthoKhota.Name = "colLastOrthoKhota"
        '
        'colLastNote
        '
        resources.ApplyResources(Me.colLastNote, "colLastNote")
        Me.colLastNote.FieldName = "LastNote"
        Me.colLastNote.Name = "colLastNote"
        '
        'colLastRX
        '
        resources.ApplyResources(Me.colLastRX, "colLastRX")
        Me.colLastRX.FieldName = "LastRX"
        Me.colLastRX.Name = "colLastRX"
        '
        'colLastToothTreatment
        '
        resources.ApplyResources(Me.colLastToothTreatment, "colLastToothTreatment")
        Me.colLastToothTreatment.FieldName = "LastToothTreatment"
        Me.colLastToothTreatment.Name = "colLastToothTreatment"
        '
        'colLastTrtDetail
        '
        resources.ApplyResources(Me.colLastTrtDetail, "colLastTrtDetail")
        Me.colLastTrtDetail.FieldName = "LastTrtDetail"
        Me.colLastTrtDetail.Name = "colLastTrtDetail"
        '
        'colLastActivityDate
        '
        resources.ApplyResources(Me.colLastActivityDate, "colLastActivityDate")
        Me.colLastActivityDate.FieldName = "LastActivityDate"
        Me.colLastActivityDate.Name = "colLastActivityDate"
        '
        'lastPatientsGrid
        '
        Me.lastPatientsGrid.DataSource = Me.PatientBS
        resources.ApplyResources(Me.lastPatientsGrid, "lastPatientsGrid")
        Me.lastPatientsGrid.MainView = Me.GridView1
        Me.lastPatientsGrid.Name = "lastPatientsGrid"
        Me.lastPatientsGrid.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridView1})
        '
        'gridPanel
        '
        Me.gridPanel.Controls.Add(Me.lastPatientsGrid)
        resources.ApplyResources(Me.gridPanel, "gridPanel")
        Me.gridPanel.Name = "gridPanel"
        '
        'btnClose
        '
        Me.btnClose.Appearance.Font = CType(resources.GetObject("btnClose.Appearance.Font"), System.Drawing.Font)
        Me.btnClose.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.btnClose, "btnClose")
        Me.btnClose.Name = "btnClose"
        '
        'botmPanel
        '
        Me.botmPanel.Controls.Add(Me.btnClose)
        resources.ApplyResources(Me.botmPanel, "botmPanel")
        Me.botmPanel.Name = "botmPanel"
        '
        'hdrPanel
        '
        Me.hdrPanel.Controls.Add(Me.btnListPatients)
        Me.hdrPanel.Controls.Add(Me.txLastNDays)
        Me.hdrPanel.Controls.Add(Me.txTopN)
        Me.hdrPanel.Controls.Add(Me.LabelControl4)
        Me.hdrPanel.Controls.Add(Me.LabelControl3)
        Me.hdrPanel.Controls.Add(Me.LabelControl2)
        Me.hdrPanel.Controls.Add(Me.LabelControl1)
        resources.ApplyResources(Me.hdrPanel, "hdrPanel")
        Me.hdrPanel.Name = "hdrPanel"
        '
        'btnListPatients
        '
        Me.btnListPatients.Appearance.Font = CType(resources.GetObject("btnListPatients.Appearance.Font"), System.Drawing.Font)
        Me.btnListPatients.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.btnListPatients, "btnListPatients")
        Me.btnListPatients.Name = "btnListPatients"
        '
        'txLastNDays
        '
        resources.ApplyResources(Me.txLastNDays, "txLastNDays")
        Me.txLastNDays.Name = "txLastNDays"
        Me.txLastNDays.Properties.Appearance.Font = CType(resources.GetObject("txLastNDays.Properties.Appearance.Font"), System.Drawing.Font)
        Me.txLastNDays.Properties.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.txLastNDays.Properties.Appearance.Options.UseFont = True
        Me.txLastNDays.Properties.Appearance.Options.UseForeColor = True
        Me.txLastNDays.Properties.Appearance.Options.UseTextOptions = True
        Me.txLastNDays.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        '
        'txTopN
        '
        resources.ApplyResources(Me.txTopN, "txTopN")
        Me.txTopN.Name = "txTopN"
        Me.txTopN.Properties.Appearance.Font = CType(resources.GetObject("txTopN.Properties.Appearance.Font"), System.Drawing.Font)
        Me.txTopN.Properties.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.txTopN.Properties.Appearance.Options.UseFont = True
        Me.txTopN.Properties.Appearance.Options.UseForeColor = True
        Me.txTopN.Properties.Appearance.Options.UseTextOptions = True
        Me.txTopN.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        '
        'LabelControl4
        '
        Me.LabelControl4.Appearance.Font = CType(resources.GetObject("LabelControl4.Appearance.Font"), System.Drawing.Font)
        Me.LabelControl4.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.LabelControl4, "LabelControl4")
        Me.LabelControl4.Name = "LabelControl4"
        '
        'LabelControl3
        '
        Me.LabelControl3.Appearance.Font = CType(resources.GetObject("LabelControl3.Appearance.Font"), System.Drawing.Font)
        Me.LabelControl3.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.LabelControl3, "LabelControl3")
        Me.LabelControl3.Name = "LabelControl3"
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
        'FrmLastPatients
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.gridPanel)
        Me.Controls.Add(Me.botmPanel)
        Me.Controls.Add(Me.hdrPanel)
        Me.Name = "FrmLastPatients"
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lastPatientsGrid, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PatientBS, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gridPanel, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gridPanel.ResumeLayout(False)
        CType(Me.botmPanel, System.ComponentModel.ISupportInitialize).EndInit()
        Me.botmPanel.ResumeLayout(False)
        CType(Me.hdrPanel, System.ComponentModel.ISupportInitialize).EndInit()
        Me.hdrPanel.ResumeLayout(False)
        Me.hdrPanel.PerformLayout()
        CType(Me.txLastNDays.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txTopN.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents colLastActivityDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colLastTrtDetail As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colLastToothTreatment As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colLastRX As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colLastNote As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colPatientName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colPatientID As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridView1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents lastPatientsGrid As DevExpress.XtraGrid.GridControl
    Friend WithEvents PatientBS As BindingSource
    Friend WithEvents gridPanel As DevExpress.XtraEditors.PanelControl
    Friend WithEvents btnClose As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents botmPanel As DevExpress.XtraEditors.PanelControl
    Friend WithEvents hdrPanel As DevExpress.XtraEditors.PanelControl
    Friend WithEvents btnListPatients As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents txTopN As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents txLastNDays As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl4 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl3 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents colLastAppt As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colLastOtherTrt As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colLastDiagTreat As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colLastOrthoWireNotes As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colLastOrthoKhota As DevExpress.XtraGrid.Columns.GridColumn
End Class
