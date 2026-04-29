<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmOtherTRTs
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmOtherTRTs))
        Me.OtherTrtBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.grpOtherTrtr = New DevExpress.XtraEditors.GroupControl()
        Me.txtTrtPrice = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl22 = New DevExpress.XtraEditors.LabelControl()
        Me.txtPayValue = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl23 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl19 = New DevExpress.XtraEditors.LabelControl()
        Me.btnDelTrt = New DevExpress.XtraEditors.SimpleButton()
        Me.txtPayNotes = New DevExpress.XtraEditors.MemoEdit()
        Me.TrtDetails = New DevExpress.XtraEditors.MemoEdit()
        Me.txtTRT = New DevExpress.XtraEditors.TextEdit()
        Me.cboTrtType = New DentistX.TblOtherTRTCombo()
        Me.btEditOtherTrt = New DevExpress.XtraEditors.SimpleButton()
        Me.btAddOtherTrt = New DevExpress.XtraEditors.SimpleButton()
        Me.TrtDate = New DevExpress.XtraEditors.DateEdit()
        Me.LabelControl7 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl3 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.oTrtsGridControl = New DevExpress.XtraGrid.GridControl()
        Me.GridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.colRowNum = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colOtherTrtID = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colPatientID = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colTrt = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colTrtDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colTrtDetails = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colIsPaid = New DevExpress.XtraGrid.Columns.GridColumn()
        CType(Me.OtherTrtBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grpOtherTrtr, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpOtherTrtr.SuspendLayout()
        CType(Me.txtTrtPrice.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtPayValue.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtPayNotes.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TrtDetails.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTRT.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TrtDate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TrtDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.oTrtsGridControl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'grpOtherTrtr
        '
        Me.grpOtherTrtr.Appearance.Font = CType(resources.GetObject("grpOtherTrtr.Appearance.Font"), System.Drawing.Font)
        Me.grpOtherTrtr.Appearance.Options.UseFont = True
        Me.grpOtherTrtr.AppearanceCaption.Font = CType(resources.GetObject("grpOtherTrtr.AppearanceCaption.Font"), System.Drawing.Font)
        Me.grpOtherTrtr.AppearanceCaption.Options.UseFont = True
        Me.grpOtherTrtr.Controls.Add(Me.txtTrtPrice)
        Me.grpOtherTrtr.Controls.Add(Me.LabelControl22)
        Me.grpOtherTrtr.Controls.Add(Me.txtPayValue)
        Me.grpOtherTrtr.Controls.Add(Me.LabelControl23)
        Me.grpOtherTrtr.Controls.Add(Me.LabelControl19)
        Me.grpOtherTrtr.Controls.Add(Me.btnDelTrt)
        Me.grpOtherTrtr.Controls.Add(Me.txtPayNotes)
        Me.grpOtherTrtr.Controls.Add(Me.TrtDetails)
        Me.grpOtherTrtr.Controls.Add(Me.txtTRT)
        Me.grpOtherTrtr.Controls.Add(Me.cboTrtType)
        Me.grpOtherTrtr.Controls.Add(Me.btEditOtherTrt)
        Me.grpOtherTrtr.Controls.Add(Me.btAddOtherTrt)
        Me.grpOtherTrtr.Controls.Add(Me.TrtDate)
        Me.grpOtherTrtr.Controls.Add(Me.LabelControl7)
        Me.grpOtherTrtr.Controls.Add(Me.LabelControl3)
        Me.grpOtherTrtr.Controls.Add(Me.LabelControl2)
        Me.grpOtherTrtr.Controls.Add(Me.LabelControl1)
        Me.grpOtherTrtr.Controls.Add(Me.oTrtsGridControl)
        resources.ApplyResources(Me.grpOtherTrtr, "grpOtherTrtr")
        Me.grpOtherTrtr.Name = "grpOtherTrtr"
        '
        'txtTrtPrice
        '
        resources.ApplyResources(Me.txtTrtPrice, "txtTrtPrice")
        Me.txtTrtPrice.Name = "txtTrtPrice"
        Me.txtTrtPrice.Properties.Appearance.Font = CType(resources.GetObject("txtTrtPrice.Properties.Appearance.Font"), System.Drawing.Font)
        Me.txtTrtPrice.Properties.Appearance.Options.UseFont = True
        '
        'LabelControl22
        '
        Me.LabelControl22.Appearance.Font = CType(resources.GetObject("LabelControl22.Appearance.Font"), System.Drawing.Font)
        Me.LabelControl22.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.LabelControl22, "LabelControl22")
        Me.LabelControl22.Name = "LabelControl22"
        '
        'txtPayValue
        '
        resources.ApplyResources(Me.txtPayValue, "txtPayValue")
        Me.txtPayValue.Name = "txtPayValue"
        Me.txtPayValue.Properties.Appearance.Font = CType(resources.GetObject("txtPayValue.Properties.Appearance.Font"), System.Drawing.Font)
        Me.txtPayValue.Properties.Appearance.Options.UseFont = True
        '
        'LabelControl23
        '
        Me.LabelControl23.Appearance.Font = CType(resources.GetObject("LabelControl23.Appearance.Font"), System.Drawing.Font)
        Me.LabelControl23.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.LabelControl23, "LabelControl23")
        Me.LabelControl23.Name = "LabelControl23"
        '
        'LabelControl19
        '
        Me.LabelControl19.Appearance.Font = CType(resources.GetObject("LabelControl19.Appearance.Font"), System.Drawing.Font)
        Me.LabelControl19.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.LabelControl19, "LabelControl19")
        Me.LabelControl19.Name = "LabelControl19"
        '
        'btnDelTrt
        '
        Me.btnDelTrt.Appearance.Font = CType(resources.GetObject("btnDelTrt.Appearance.Font"), System.Drawing.Font)
        Me.btnDelTrt.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.btnDelTrt, "btnDelTrt")
        Me.btnDelTrt.Name = "btnDelTrt"
        '
        'txtPayNotes
        '
        resources.ApplyResources(Me.txtPayNotes, "txtPayNotes")
        Me.txtPayNotes.Name = "txtPayNotes"
        '
        'TrtDetails
        '
        resources.ApplyResources(Me.TrtDetails, "TrtDetails")
        Me.TrtDetails.Name = "TrtDetails"
        '
        'txtTRT
        '
        resources.ApplyResources(Me.txtTRT, "txtTRT")
        Me.txtTRT.Name = "txtTRT"
        '
        'cboTrtType
        '
        Me.cboTrtType.BtnAddVisible = True
        Me.cboTrtType.BtnSearchVisible = True
        Me.cboTrtType.Filter = ""
        resources.ApplyResources(Me.cboTrtType, "cboTrtType")
        Me.cboTrtType.Name = "cboTrtType"
        Me.cboTrtType.TblOtherTrtID = 1
        Me.cboTrtType.Trt = Nothing
        '
        'btEditOtherTrt
        '
        Me.btEditOtherTrt.Appearance.Font = CType(resources.GetObject("btEditOtherTrt.Appearance.Font"), System.Drawing.Font)
        Me.btEditOtherTrt.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.btEditOtherTrt, "btEditOtherTrt")
        Me.btEditOtherTrt.Name = "btEditOtherTrt"
        '
        'btAddOtherTrt
        '
        Me.btAddOtherTrt.Appearance.Font = CType(resources.GetObject("btAddOtherTrt.Appearance.Font"), System.Drawing.Font)
        Me.btAddOtherTrt.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.btAddOtherTrt, "btAddOtherTrt")
        Me.btAddOtherTrt.Name = "btAddOtherTrt"
        '
        'TrtDate
        '
        resources.ApplyResources(Me.TrtDate, "TrtDate")
        Me.TrtDate.EnterMoveNextControl = True
        Me.TrtDate.Name = "TrtDate"
        Me.TrtDate.Properties.Appearance.Font = CType(resources.GetObject("TrtDate.Properties.Appearance.Font"), System.Drawing.Font)
        Me.TrtDate.Properties.Appearance.Options.UseFont = True
        Me.TrtDate.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("TrtDate.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me.TrtDate.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("TrtDate.Properties.CalendarTimeProperties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me.TrtDate.Properties.DisplayFormat.FormatString = "dd/MM/yyyy"
        Me.TrtDate.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.TrtDate.Properties.EditFormat.FormatString = "dd/MM/yyyy"
        Me.TrtDate.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        '
        'LabelControl7
        '
        Me.LabelControl7.Appearance.Font = CType(resources.GetObject("LabelControl7.Appearance.Font"), System.Drawing.Font)
        Me.LabelControl7.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.LabelControl7, "LabelControl7")
        Me.LabelControl7.Name = "LabelControl7"
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
        'oTrtsGridControl
        '
        Me.oTrtsGridControl.DataSource = Me.OtherTrtBindingSource
        resources.ApplyResources(Me.oTrtsGridControl, "oTrtsGridControl")
        Me.oTrtsGridControl.MainView = Me.GridView1
        Me.oTrtsGridControl.Name = "oTrtsGridControl"
        Me.oTrtsGridControl.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridView1})
        '
        'GridView1
        '
        Me.GridView1.Appearance.HeaderPanel.Font = CType(resources.GetObject("GridView1.Appearance.HeaderPanel.Font"), System.Drawing.Font)
        Me.GridView1.Appearance.HeaderPanel.Options.UseFont = True
        Me.GridView1.Appearance.Row.Font = CType(resources.GetObject("GridView1.Appearance.Row.Font"), System.Drawing.Font)
        Me.GridView1.Appearance.Row.Options.UseFont = True
        Me.GridView1.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.colRowNum, Me.colOtherTrtID, Me.colPatientID, Me.colTrt, Me.colTrtDate, Me.colTrtDetails, Me.colIsPaid})
        Me.GridView1.GridControl = Me.oTrtsGridControl
        Me.GridView1.Name = "GridView1"
        Me.GridView1.OptionsBehavior.Editable = False
        Me.GridView1.OptionsBehavior.EditingMode = DevExpress.XtraGrid.Views.Grid.GridEditingMode.Inplace
        Me.GridView1.OptionsDetail.EnableMasterViewMode = False
        Me.GridView1.OptionsView.ShowErrorPanel = DevExpress.Utils.DefaultBoolean.[False]
        Me.GridView1.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
        Me.GridView1.OptionsView.ShowGroupExpandCollapseButtons = False
        Me.GridView1.OptionsView.ShowGroupPanel = False
        '
        'colRowNum
        '
        resources.ApplyResources(Me.colRowNum, "colRowNum")
        Me.colRowNum.FieldName = "colRowNum"
        Me.colRowNum.Name = "colRowNum"
        Me.colRowNum.UnboundDataType = GetType(Integer)
        '
        'colOtherTrtID
        '
        Me.colOtherTrtID.FieldName = "OtherTrtID"
        Me.colOtherTrtID.Name = "colOtherTrtID"
        '
        'colPatientID
        '
        Me.colPatientID.FieldName = "PatientID"
        Me.colPatientID.Name = "colPatientID"
        '
        'colTrt
        '
        resources.ApplyResources(Me.colTrt, "colTrt")
        Me.colTrt.FieldName = "Trt"
        Me.colTrt.Name = "colTrt"
        '
        'colTrtDate
        '
        resources.ApplyResources(Me.colTrtDate, "colTrtDate")
        Me.colTrtDate.DisplayFormat.FormatString = "dd/MM/yyyy"
        Me.colTrtDate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.colTrtDate.FieldName = "TreatDate"
        Me.colTrtDate.Name = "colTrtDate"
        '
        'colTrtDetails
        '
        resources.ApplyResources(Me.colTrtDetails, "colTrtDetails")
        Me.colTrtDetails.FieldName = "TrtDetails"
        Me.colTrtDetails.Name = "colTrtDetails"
        '
        'colIsPaid
        '
        resources.ApplyResources(Me.colIsPaid, "colIsPaid")
        Me.colIsPaid.FieldName = "IsPaid"
        Me.colIsPaid.Name = "colIsPaid"
        '
        'FrmOtherTRTs
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.grpOtherTrtr)
        Me.Name = "FrmOtherTRTs"
        CType(Me.OtherTrtBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grpOtherTrtr, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpOtherTrtr.ResumeLayout(False)
        Me.grpOtherTrtr.PerformLayout()
        CType(Me.txtTrtPrice.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtPayValue.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtPayNotes.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TrtDetails.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTRT.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TrtDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TrtDate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.oTrtsGridControl, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents OtherTrtBindingSource As BindingSource
    Friend WithEvents grpOtherTrtr As DevExpress.XtraEditors.GroupControl
    Friend WithEvents btEditOtherTrt As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btAddOtherTrt As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents TrtDate As DevExpress.XtraEditors.DateEdit
    Friend WithEvents LabelControl7 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents oTrtsGridControl As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridView1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents colRowNum As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colOtherTrtID As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colPatientID As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colTrt As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colTrtDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents TrtDetails As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents txtTRT As DevExpress.XtraEditors.TextEdit
    Friend WithEvents cboTrtType As TblOtherTRTCombo
    Friend WithEvents LabelControl3 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents colTrtDetails As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colIsPaid As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents btnDelTrt As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents txtTrtPrice As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl22 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents txtPayValue As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl23 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl19 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents txtPayNotes As DevExpress.XtraEditors.MemoEdit
End Class
