<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmCtlRep
    Inherits DevExpress.XtraEditors.XtraForm
    ''' <summary>
    ''' Required designer variable.
    ''' </summary>
    Private components As System.ComponentModel.IContainer = Nothing
		  ''' <summary>
		  ''' Clean up any resources being used..
		  ''' </summary>
		  '''  <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
	    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
		    If disposing AndAlso (components IsNot Nothing) Then
			  components.Dispose()  
		  End If 
		  MyBase.Dispose(disposing)  
	  End Sub 
#Region "Windows Form Designer generated code"

		  ''' <summary>
		  ''' Required method for Designer support - do not modify
		  ''' the contents of this method with the code editor.
		  ''' </summary>
	    Private Sub InitializeComponent()
        Me.Splitter1 = New DevExpress.XtraEditors.SplitContainerControl()
        Me.DGV = New DevExpress.XtraGrid.GridControl()
        Me.dgView = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.colRowNum = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colCtlID = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colCtl = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colX = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colY = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.SplitContainerControl1 = New DevExpress.XtraEditors.SplitContainerControl()
        Me.CtlIDLabel = New DevExpress.XtraEditors.LabelControl()
        Me.butClose = New DevExpress.XtraEditors.SimpleButton()
        Me.btnDel = New DevExpress.XtraEditors.SimpleButton()
        Me.btnEdit = New DevExpress.XtraEditors.SimpleButton()
        Me.btnAdd = New DevExpress.XtraEditors.SimpleButton()
        Me.YLabel = New DevExpress.XtraEditors.LabelControl()
        Me.YSpinEdit = New DevExpress.XtraEditors.SpinEdit()
        Me.XSpinEdit = New DevExpress.XtraEditors.SpinEdit()
        Me.XLabel = New DevExpress.XtraEditors.LabelControl()
        Me.CtlTextEdit = New DevExpress.XtraEditors.TextEdit()
        Me.CtlIDSpinEdit = New DevExpress.XtraEditors.SpinEdit()
        Me.CtlLabel = New DevExpress.XtraEditors.LabelControl()
        Me.lblDrName = New DevExpress.XtraEditors.LabelControl()
        Me.lblRxDate = New DevExpress.XtraEditors.LabelControl()
        Me.lblRX = New DevExpress.XtraEditors.LabelControl()
        Me.lblSex = New DevExpress.XtraEditors.LabelControl()
        Me.lblAge = New DevExpress.XtraEditors.LabelControl()
        Me.lblPatientName = New DevExpress.XtraEditors.LabelControl()
        Me.A5Panel = New DevExpress.XtraEditors.PanelControl()
        CType(Me.Splitter1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Splitter1.Panel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Splitter1.Panel2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Splitter1.Panel2.SuspendLayout()
        Me.Splitter1.SuspendLayout()
        CType(Me.DGV, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgView, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SplitContainerControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SplitContainerControl1.Panel1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainerControl1.Panel1.SuspendLayout()
        CType(Me.SplitContainerControl1.Panel2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainerControl1.Panel2.SuspendLayout()
        Me.SplitContainerControl1.SuspendLayout()
        CType(Me.YSpinEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.XSpinEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CtlTextEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CtlIDSpinEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.A5Panel, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.A5Panel.SuspendLayout()
        Me.SuspendLayout()
        '
        'Splitter1
        '
        Me.Splitter1.CollapsePanel = DevExpress.XtraEditors.SplitCollapsePanel.Panel2
        Me.Splitter1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Splitter1.Horizontal = False
        Me.Splitter1.Location = New System.Drawing.Point(0, 0)
        Me.Splitter1.Name = "Splitter1"
        '
        'Splitter1.Panel2
        '
        Me.Splitter1.Panel2.Controls.Add(Me.SplitContainerControl1)
        Me.Splitter1.Size = New System.Drawing.Size(992, 868)
        Me.Splitter1.SplitterPosition = 0
        Me.Splitter1.TabIndex = 0
        '
        'DGV
        '
        Me.DGV.EmbeddedNavigator.Appearance.Options.UseFont = True
        Me.DGV.EmbeddedNavigator.Buttons.Append.Visible = False
        Me.DGV.EmbeddedNavigator.Buttons.CancelEdit.Visible = False
        Me.DGV.EmbeddedNavigator.Buttons.Edit.Visible = False
        Me.DGV.EmbeddedNavigator.Buttons.EndEdit.Visible = False
        Me.DGV.EmbeddedNavigator.Buttons.Remove.Visible = False
        Me.DGV.Location = New System.Drawing.Point(3, 6)
        Me.DGV.MainView = Me.dgView
        Me.DGV.Name = "DGV"
        Me.DGV.Size = New System.Drawing.Size(302, 319)
        Me.DGV.TabIndex = 0
        Me.DGV.UseEmbeddedNavigator = True
        Me.DGV.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.dgView})
        '
        'dgView
        '
        Me.dgView.Appearance.HeaderPanel.Font = New System.Drawing.Font("Calibri", 10.0!, System.Drawing.FontStyle.Bold)
        Me.dgView.Appearance.HeaderPanel.Options.UseFont = True
        Me.dgView.Appearance.Row.Font = New System.Drawing.Font("Calibri", 10.0!, System.Drawing.FontStyle.Bold)
        Me.dgView.Appearance.Row.Options.UseFont = True
        Me.dgView.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.dgView.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.colRowNum, Me.colCtlID, Me.colCtl, Me.colX, Me.colY})
        Me.dgView.DetailHeight = 377
        Me.dgView.GridControl = Me.DGV
        Me.dgView.Name = "dgView"
        Me.dgView.OptionsBehavior.Editable = False
        Me.dgView.OptionsBehavior.ReadOnly = True
        Me.dgView.OptionsDetail.EnableMasterViewMode = False
        Me.dgView.OptionsView.EnableAppearanceEvenRow = True
        Me.dgView.OptionsView.ShowFooter = True
        '
        'colRowNum
        '
        Me.colRowNum.Caption = "Number"
        Me.colRowNum.FieldName = "colRowNum"
        Me.colRowNum.Name = "colRowNum"
        Me.colRowNum.UnboundDataType = GetType(Integer)
        Me.colRowNum.Visible = True
        Me.colRowNum.VisibleIndex = 0
        '
        'colCtlID
        '
        Me.colCtlID.FieldName = "CtlID"
        Me.colCtlID.Name = "colCtlID"
        '
        'colCtl
        '
        Me.colCtl.Caption = "Label Name"
        Me.colCtl.FieldName = "Ctl"
        Me.colCtl.Name = "colCtl"
        Me.colCtl.Visible = True
        Me.colCtl.VisibleIndex = 1
        '
        'colX
        '
        Me.colX.Caption = "X Location"
        Me.colX.FieldName = "X"
        Me.colX.Name = "colX"
        Me.colX.Visible = True
        Me.colX.VisibleIndex = 2
        '
        'colY
        '
        Me.colY.Caption = "Y Location"
        Me.colY.FieldName = "Y"
        Me.colY.Name = "colY"
        Me.colY.Visible = True
        Me.colY.VisibleIndex = 3
        '
        'SplitContainerControl1
        '
        Me.SplitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainerControl1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainerControl1.Name = "SplitContainerControl1"
        '
        'SplitContainerControl1.Panel1
        '
        Me.SplitContainerControl1.Panel1.Controls.Add(Me.CtlIDLabel)
        Me.SplitContainerControl1.Panel1.Controls.Add(Me.butClose)
        Me.SplitContainerControl1.Panel1.Controls.Add(Me.DGV)
        Me.SplitContainerControl1.Panel1.Controls.Add(Me.btnDel)
        Me.SplitContainerControl1.Panel1.Controls.Add(Me.btnAdd)
        Me.SplitContainerControl1.Panel1.Controls.Add(Me.btnEdit)
        Me.SplitContainerControl1.Panel1.Controls.Add(Me.CtlIDSpinEdit)
        Me.SplitContainerControl1.Panel1.Controls.Add(Me.YLabel)
        Me.SplitContainerControl1.Panel1.Controls.Add(Me.CtlTextEdit)
        Me.SplitContainerControl1.Panel1.Controls.Add(Me.XLabel)
        Me.SplitContainerControl1.Panel1.Controls.Add(Me.YSpinEdit)
        Me.SplitContainerControl1.Panel1.Controls.Add(Me.CtlLabel)
        Me.SplitContainerControl1.Panel1.Controls.Add(Me.XSpinEdit)
        Me.SplitContainerControl1.Panel1.Text = "Panel1"
        '
        'SplitContainerControl1.Panel2
        '
        Me.SplitContainerControl1.Panel2.Controls.Add(Me.A5Panel)
        Me.SplitContainerControl1.Panel2.Text = "Panel2"
        Me.SplitContainerControl1.Size = New System.Drawing.Size(992, 862)
        Me.SplitContainerControl1.SplitterPosition = 363
        Me.SplitContainerControl1.TabIndex = 8
        '
        'CtlIDLabel
        '
        Me.CtlIDLabel.Appearance.Font = New System.Drawing.Font("Calibri", 10.0!, System.Drawing.FontStyle.Bold)
        Me.CtlIDLabel.Appearance.Options.UseFont = True
        Me.CtlIDLabel.Appearance.Options.UseTextOptions = True
        Me.CtlIDLabel.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.CtlIDLabel.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.CtlIDLabel.Location = New System.Drawing.Point(47, 381)
        Me.CtlIDLabel.Name = "CtlIDLabel"
        Me.CtlIDLabel.Size = New System.Drawing.Size(26, 15)
        Me.CtlIDLabel.TabIndex = 0
        Me.CtlIDLabel.Text = "CtlID"
        '
        'butClose
        '
        Me.butClose.Appearance.Font = New System.Drawing.Font("Calibri", 10.0!, System.Drawing.FontStyle.Bold)
        Me.butClose.Appearance.Options.UseFont = True
        Me.butClose.Location = New System.Drawing.Point(272, 502)
        Me.butClose.Name = "butClose"
        Me.butClose.Size = New System.Drawing.Size(67, 23)
        Me.butClose.TabIndex = 7
        Me.butClose.Text = "Close"
        '
        'btnDel
        '
        Me.btnDel.Appearance.Font = New System.Drawing.Font("Calibri", 10.0!, System.Drawing.FontStyle.Bold)
        Me.btnDel.Appearance.Options.UseFont = True
        Me.btnDel.Location = New System.Drawing.Point(199, 502)
        Me.btnDel.Name = "btnDel"
        Me.btnDel.Size = New System.Drawing.Size(67, 23)
        Me.btnDel.TabIndex = 6
        Me.btnDel.Text = "Delete"
        '
        'btnEdit
        '
        Me.btnEdit.Appearance.Font = New System.Drawing.Font("Calibri", 10.0!, System.Drawing.FontStyle.Bold)
        Me.btnEdit.Appearance.Options.UseFont = True
        Me.btnEdit.Location = New System.Drawing.Point(126, 502)
        Me.btnEdit.Name = "btnEdit"
        Me.btnEdit.Size = New System.Drawing.Size(67, 23)
        Me.btnEdit.TabIndex = 5
        Me.btnEdit.Text = "Edit"
        '
        'btnAdd
        '
        Me.btnAdd.Appearance.Font = New System.Drawing.Font("Calibri", 10.0!, System.Drawing.FontStyle.Bold)
        Me.btnAdd.Appearance.Options.UseFont = True
        Me.btnAdd.Location = New System.Drawing.Point(53, 502)
        Me.btnAdd.Name = "btnAdd"
        Me.btnAdd.Size = New System.Drawing.Size(67, 23)
        Me.btnAdd.TabIndex = 4
        Me.btnAdd.Text = "Add"
        '
        'YLabel
        '
        Me.YLabel.Appearance.Font = New System.Drawing.Font("Calibri", 10.0!, System.Drawing.FontStyle.Bold)
        Me.YLabel.Appearance.Options.UseFont = True
        Me.YLabel.Appearance.Options.UseTextOptions = True
        Me.YLabel.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.YLabel.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.YLabel.Location = New System.Drawing.Point(47, 462)
        Me.YLabel.Name = "YLabel"
        Me.YLabel.Size = New System.Drawing.Size(7, 15)
        Me.YLabel.TabIndex = 2
        Me.YLabel.Text = "Y"
        '
        'YSpinEdit
        '
        Me.YSpinEdit.EditValue = New Decimal(New Integer() {0, 0, 0, 0})
        Me.YSpinEdit.Location = New System.Drawing.Point(78, 463)
        Me.YSpinEdit.Name = "YSpinEdit"
        Me.YSpinEdit.Properties.Appearance.Font = New System.Drawing.Font("Calibri", 10.0!, System.Drawing.FontStyle.Bold)
        Me.YSpinEdit.Properties.Appearance.Options.UseFont = True
        Me.YSpinEdit.Properties.Appearance.Options.UseTextOptions = True
        Me.YSpinEdit.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.YSpinEdit.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.YSpinEdit.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.YSpinEdit.Properties.DisplayFormat.FormatString = "N2"
        Me.YSpinEdit.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.YSpinEdit.Properties.EditFormat.FormatString = "N2"
        Me.YSpinEdit.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.YSpinEdit.Properties.MaxValue = New Decimal(New Integer() {1000, 0, 0, 0})
        Me.YSpinEdit.Size = New System.Drawing.Size(67, 22)
        Me.YSpinEdit.TabIndex = 3
        '
        'XSpinEdit
        '
        Me.XSpinEdit.EditValue = New Decimal(New Integer() {0, 0, 0, 0})
        Me.XSpinEdit.Location = New System.Drawing.Point(78, 436)
        Me.XSpinEdit.Name = "XSpinEdit"
        Me.XSpinEdit.Properties.Appearance.Font = New System.Drawing.Font("Calibri", 10.0!, System.Drawing.FontStyle.Bold)
        Me.XSpinEdit.Properties.Appearance.Options.UseFont = True
        Me.XSpinEdit.Properties.Appearance.Options.UseTextOptions = True
        Me.XSpinEdit.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.XSpinEdit.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.XSpinEdit.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.XSpinEdit.Properties.DisplayFormat.FormatString = "N2"
        Me.XSpinEdit.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.XSpinEdit.Properties.EditFormat.FormatString = "N2"
        Me.XSpinEdit.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.XSpinEdit.Properties.MaxValue = New Decimal(New Integer() {1000, 0, 0, 0})
        Me.XSpinEdit.Size = New System.Drawing.Size(67, 22)
        Me.XSpinEdit.TabIndex = 3
        '
        'XLabel
        '
        Me.XLabel.Appearance.Font = New System.Drawing.Font("Calibri", 10.0!, System.Drawing.FontStyle.Bold)
        Me.XLabel.Appearance.Options.UseFont = True
        Me.XLabel.Appearance.Options.UseTextOptions = True
        Me.XLabel.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.XLabel.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.XLabel.Location = New System.Drawing.Point(47, 439)
        Me.XLabel.Name = "XLabel"
        Me.XLabel.Size = New System.Drawing.Size(7, 15)
        Me.XLabel.TabIndex = 2
        Me.XLabel.Text = "X"
        '
        'CtlTextEdit
        '
        Me.CtlTextEdit.Location = New System.Drawing.Point(78, 409)
        Me.CtlTextEdit.Name = "CtlTextEdit"
        Me.CtlTextEdit.Properties.Appearance.Font = New System.Drawing.Font("Calibri", 10.0!, System.Drawing.FontStyle.Bold)
        Me.CtlTextEdit.Properties.Appearance.Options.UseFont = True
        Me.CtlTextEdit.Properties.Appearance.Options.UseTextOptions = True
        Me.CtlTextEdit.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.CtlTextEdit.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.CtlTextEdit.Size = New System.Drawing.Size(173, 22)
        Me.CtlTextEdit.TabIndex = 1
        '
        'CtlIDSpinEdit
        '
        Me.CtlIDSpinEdit.EditValue = New Decimal(New Integer() {0, 0, 0, 0})
        Me.CtlIDSpinEdit.Enabled = False
        Me.CtlIDSpinEdit.Location = New System.Drawing.Point(78, 381)
        Me.CtlIDSpinEdit.Name = "CtlIDSpinEdit"
        Me.CtlIDSpinEdit.Properties.Appearance.Font = New System.Drawing.Font("Calibri", 10.0!, System.Drawing.FontStyle.Bold)
        Me.CtlIDSpinEdit.Properties.Appearance.Options.UseFont = True
        Me.CtlIDSpinEdit.Properties.Appearance.Options.UseTextOptions = True
        Me.CtlIDSpinEdit.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.CtlIDSpinEdit.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.CtlIDSpinEdit.Size = New System.Drawing.Size(67, 22)
        Me.CtlIDSpinEdit.TabIndex = 1
        '
        'CtlLabel
        '
        Me.CtlLabel.Appearance.Font = New System.Drawing.Font("Calibri", 10.0!, System.Drawing.FontStyle.Bold)
        Me.CtlLabel.Appearance.Options.UseFont = True
        Me.CtlLabel.Appearance.Options.UseTextOptions = True
        Me.CtlLabel.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.CtlLabel.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.CtlLabel.Location = New System.Drawing.Point(47, 409)
        Me.CtlLabel.Name = "CtlLabel"
        Me.CtlLabel.Size = New System.Drawing.Size(15, 15)
        Me.CtlLabel.TabIndex = 0
        Me.CtlLabel.Text = "Ctl"
        '
        'lblDrName
        '
        Me.lblDrName.Appearance.Font = New System.Drawing.Font("Calibri", 12.0!, System.Drawing.FontStyle.Bold)
        Me.lblDrName.Appearance.Options.UseFont = True
        Me.lblDrName.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.lblDrName.Location = New System.Drawing.Point(410, 279)
        Me.lblDrName.Name = "lblDrName"
        Me.lblDrName.Size = New System.Drawing.Size(102, 25)
        Me.lblDrName.TabIndex = 0
        Me.lblDrName.Tag = "952.5, 1292.77"
        Me.lblDrName.Text = "Doctor Name"
        '
        'lblRxDate
        '
        Me.lblRxDate.Appearance.Font = New System.Drawing.Font("Calibri", 12.0!, System.Drawing.FontStyle.Bold)
        Me.lblRxDate.Appearance.Options.UseFont = True
        Me.lblRxDate.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.lblRxDate.Location = New System.Drawing.Point(72, 279)
        Me.lblRxDate.Name = "lblRxDate"
        Me.lblRxDate.Size = New System.Drawing.Size(72, 25)
        Me.lblRxDate.TabIndex = 0
        Me.lblRxDate.Tag = "42.33, 1292.77"
        Me.lblRxDate.Text = "Rx Date"
        '
        'lblRX
        '
        Me.lblRX.Appearance.Font = New System.Drawing.Font("Calibri", 12.0!, System.Drawing.FontStyle.Bold)
        Me.lblRX.Appearance.Options.UseFont = True
        Me.lblRX.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.lblRX.Location = New System.Drawing.Point(72, 98)
        Me.lblRX.Name = "lblRX"
        Me.lblRX.Size = New System.Drawing.Size(440, 124)
        Me.lblRX.TabIndex = 0
        Me.lblRX.Tag = "202.08, 334.1"
        Me.lblRX.Text = "RX"
        '
        'lblSex
        '
        Me.lblSex.Appearance.Font = New System.Drawing.Font("Calibri", 12.0!, System.Drawing.FontStyle.Bold)
        Me.lblSex.Appearance.Options.UseFont = True
        Me.lblSex.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.lblSex.Location = New System.Drawing.Point(421, 50)
        Me.lblSex.Name = "lblSex"
        Me.lblSex.Size = New System.Drawing.Size(91, 25)
        Me.lblSex.TabIndex = 0
        Me.lblSex.Tag = "777.88, 221.35"
        Me.lblSex.Text = "Patient Sex"
        '
        'lblAge
        '
        Me.lblAge.Appearance.Font = New System.Drawing.Font("Calibri", 12.0!, System.Drawing.FontStyle.Bold)
        Me.lblAge.Appearance.Options.UseFont = True
        Me.lblAge.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.lblAge.Location = New System.Drawing.Point(72, 19)
        Me.lblAge.Name = "lblAge"
        Me.lblAge.Size = New System.Drawing.Size(92, 25)
        Me.lblAge.TabIndex = 0
        Me.lblAge.Tag = "42.33, 158.93"
        Me.lblAge.Text = "Patient Age"
        '
        'lblPatientName
        '
        Me.lblPatientName.Appearance.Font = New System.Drawing.Font("Calibri", 12.0!, System.Drawing.FontStyle.Bold)
        Me.lblPatientName.Appearance.Options.UseFont = True
        Me.lblPatientName.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.lblPatientName.Location = New System.Drawing.Point(408, 19)
        Me.lblPatientName.Name = "lblPatientName"
        Me.lblPatientName.Size = New System.Drawing.Size(104, 25)
        Me.lblPatientName.TabIndex = 0
        Me.lblPatientName.Tag = "777.88, 158.93"
        Me.lblPatientName.Text = "Patient Name"
        '
        'A5Panel
        '
        Me.A5Panel.Controls.Add(Me.lblAge)
        Me.A5Panel.Controls.Add(Me.lblDrName)
        Me.A5Panel.Controls.Add(Me.lblPatientName)
        Me.A5Panel.Controls.Add(Me.lblRxDate)
        Me.A5Panel.Controls.Add(Me.lblSex)
        Me.A5Panel.Controls.Add(Me.lblRX)
        Me.A5Panel.Location = New System.Drawing.Point(0, 0)
        Me.A5Panel.MaximumSize = New System.Drawing.Size(592, 840)
        Me.A5Panel.MinimumSize = New System.Drawing.Size(592, 840)
        Me.A5Panel.Name = "A5Panel"
        Me.A5Panel.Size = New System.Drawing.Size(592, 840)
        Me.A5Panel.TabIndex = 1
        '
        'FrmCtlRep
        '
        Me.Appearance.Options.UseFont = True
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(992, 868)
        Me.Controls.Add(Me.Splitter1)
        Me.Name = "FrmCtlRep"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "FrmCtlRep"
        CType(Me.Splitter1.Panel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Splitter1.Panel2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Splitter1.Panel2.ResumeLayout(False)
        CType(Me.Splitter1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Splitter1.ResumeLayout(False)
        CType(Me.DGV, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgView, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SplitContainerControl1.Panel1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainerControl1.Panel1.ResumeLayout(False)
        Me.SplitContainerControl1.Panel1.PerformLayout()
        CType(Me.SplitContainerControl1.Panel2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainerControl1.Panel2.ResumeLayout(False)
        CType(Me.SplitContainerControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainerControl1.ResumeLayout(False)
        CType(Me.YSpinEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.XSpinEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CtlTextEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CtlIDSpinEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.A5Panel, System.ComponentModel.ISupportInitialize).EndInit()
        Me.A5Panel.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private WithEvents DGV As DevExpress.XtraGrid.GridControl
		Private WithEvents dgView As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents colRowNum As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents Splitter1 As DevExpress.XtraEditors.SplitContainerControl
    Friend WithEvents CtlIDSpinEdit As DevExpress.XtraEditors.SpinEdit
    Friend WithEvents CtlIDLabel As DevExpress.XtraEditors.LabelControl
    Friend WithEvents colCtlID As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CtlTextEdit As DevExpress.XtraEditors.TextEdit
    Friend WithEvents CtlLabel As DevExpress.XtraEditors.LabelControl
    Friend WithEvents colCtl As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents XSpinEdit As DevExpress.XtraEditors.SpinEdit
    Friend WithEvents XLabel As DevExpress.XtraEditors.LabelControl
    Friend WithEvents colX As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents YSpinEdit As DevExpress.XtraEditors.SpinEdit
    Friend WithEvents YLabel As DevExpress.XtraEditors.LabelControl
    Friend WithEvents colY As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents btnAdd As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnEdit As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnDel As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents butClose As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents SplitContainerControl1 As DevExpress.XtraEditors.SplitContainerControl
    Friend WithEvents lblDrName As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblRxDate As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblRX As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblSex As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblAge As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblPatientName As DevExpress.XtraEditors.LabelControl
    Friend WithEvents A5Panel As DevExpress.XtraEditors.PanelControl
End Class
