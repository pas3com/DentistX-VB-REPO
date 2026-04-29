<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmEmp
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmEmp))
        Me.Splitter1 = New DevExpress.XtraEditors.SplitContainerControl()
        Me.DGV = New DevExpress.XtraGrid.GridControl()
        Me.dgView = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.colRowNum = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colEmpID = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colEmpName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colEmpPhone = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colEmpAddress = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colWhatsAppPrefix = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colWhatsApp = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.butCancel = New DevExpress.XtraEditors.SimpleButton()
        Me.EmpImgImageEdit = New DevExpress.XtraEditors.PictureEdit()
        Me.btnBrowse = New DevExpress.XtraEditors.SimpleButton()
        Me.btnAdd = New DevExpress.XtraEditors.SimpleButton()
        Me.btnEdit = New DevExpress.XtraEditors.SimpleButton()
        Me.btnDel = New DevExpress.XtraEditors.SimpleButton()
        Me.butClose = New DevExpress.XtraEditors.SimpleButton()
        Me.EmpIDLabel = New DevExpress.XtraEditors.LabelControl()
        Me.EmpIDSpinEdit = New DevExpress.XtraEditors.SpinEdit()
        Me.EmpNameLabel = New DevExpress.XtraEditors.LabelControl()
        Me.EmpNameTextEdit = New DevExpress.XtraEditors.TextEdit()
        Me.EmpImgLabel = New DevExpress.XtraEditors.LabelControl()
        Me.EmpPhoneLabel = New DevExpress.XtraEditors.LabelControl()
        Me.EmpAddressTextEdit = New DevExpress.XtraEditors.TextEdit()
        Me.EmpPhoneTextEdit = New DevExpress.XtraEditors.TextEdit()
        Me.EmpAddressLabel = New DevExpress.XtraEditors.LabelControl()
        Me.EmpWhatsPrefixLabel = New DevExpress.XtraEditors.LabelControl()
        Me.EmpWhatsPrefixCombo = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.EmpWhatsLabel = New DevExpress.XtraEditors.LabelControl()
        Me.EmpWhatsTextEdit = New DevExpress.XtraEditors.TextEdit()
        Me.lblEmpWhats = New DevExpress.XtraEditors.LabelControl()
        CType(Me.Splitter1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Splitter1.Panel1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Splitter1.Panel1.SuspendLayout()
        CType(Me.Splitter1.Panel2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Splitter1.Panel2.SuspendLayout()
        Me.Splitter1.SuspendLayout()
        CType(Me.DGV, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgView, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmpImgImageEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmpIDSpinEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmpNameTextEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmpAddressTextEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmpPhoneTextEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmpWhatsPrefixCombo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmpWhatsTextEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Splitter1
        '
        Me.Splitter1.CollapsePanel = DevExpress.XtraEditors.SplitCollapsePanel.Panel2
        resources.ApplyResources(Me.Splitter1, "Splitter1")
        Me.Splitter1.Horizontal = False
        Me.Splitter1.Name = "Splitter1"
        '
        'Splitter1.Panel1
        '
        Me.Splitter1.Panel1.Controls.Add(Me.DGV)
        '
        'Splitter1.Panel2
        '
        Me.Splitter1.Panel2.Controls.Add(Me.butCancel)
        Me.Splitter1.Panel2.Controls.Add(Me.EmpImgImageEdit)
        Me.Splitter1.Panel2.Controls.Add(Me.btnBrowse)
        Me.Splitter1.Panel2.Controls.Add(Me.btnAdd)
        Me.Splitter1.Panel2.Controls.Add(Me.btnEdit)
        Me.Splitter1.Panel2.Controls.Add(Me.btnDel)
        Me.Splitter1.Panel2.Controls.Add(Me.butClose)
        Me.Splitter1.Panel2.Controls.Add(Me.EmpIDLabel)
        Me.Splitter1.Panel2.Controls.Add(Me.EmpIDSpinEdit)
        Me.Splitter1.Panel2.Controls.Add(Me.EmpNameLabel)
        Me.Splitter1.Panel2.Controls.Add(Me.EmpNameTextEdit)
        Me.Splitter1.Panel2.Controls.Add(Me.EmpImgLabel)
        Me.Splitter1.Panel2.Controls.Add(Me.EmpPhoneLabel)
        Me.Splitter1.Panel2.Controls.Add(Me.EmpAddressTextEdit)
        Me.Splitter1.Panel2.Controls.Add(Me.EmpPhoneTextEdit)
        Me.Splitter1.Panel2.Controls.Add(Me.EmpAddressLabel)
        Me.Splitter1.Panel2.Controls.Add(Me.EmpWhatsPrefixLabel)
        Me.Splitter1.Panel2.Controls.Add(Me.EmpWhatsPrefixCombo)
        Me.Splitter1.Panel2.Controls.Add(Me.EmpWhatsLabel)
        Me.Splitter1.Panel2.Controls.Add(Me.EmpWhatsTextEdit)
        Me.Splitter1.Panel2.Controls.Add(Me.lblEmpWhats)
        Me.Splitter1.SplitterPosition = 326
        '
        'DGV
        '
        resources.ApplyResources(Me.DGV, "DGV")
        Me.DGV.EmbeddedNavigator.Appearance.Options.UseFont = True
        Me.DGV.EmbeddedNavigator.Buttons.Append.Visible = False
        Me.DGV.EmbeddedNavigator.Buttons.CancelEdit.Visible = False
        Me.DGV.EmbeddedNavigator.Buttons.Edit.Visible = False
        Me.DGV.EmbeddedNavigator.Buttons.EndEdit.Visible = False
        Me.DGV.EmbeddedNavigator.Buttons.Remove.Visible = False
        Me.DGV.MainView = Me.dgView
        Me.DGV.Name = "DGV"
        Me.DGV.UseEmbeddedNavigator = True
        Me.DGV.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.dgView})
        '
        'dgView
        '
        Me.dgView.Appearance.HeaderPanel.Font = CType(resources.GetObject("dgView.Appearance.HeaderPanel.Font"), System.Drawing.Font)
        Me.dgView.Appearance.HeaderPanel.Options.UseFont = True
        Me.dgView.Appearance.Row.Font = CType(resources.GetObject("dgView.Appearance.Row.Font"), System.Drawing.Font)
        Me.dgView.Appearance.Row.Options.UseFont = True
        Me.dgView.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.dgView.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.colRowNum, Me.colEmpID, Me.colEmpName, Me.colEmpPhone, Me.colEmpAddress, Me.colWhatsAppPrefix, Me.colWhatsApp})
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
        resources.ApplyResources(Me.colRowNum, "colRowNum")
        Me.colRowNum.FieldName = "colRowNum"
        Me.colRowNum.Name = "colRowNum"
        Me.colRowNum.UnboundDataType = GetType(Integer)
        '
        'colEmpID
        '
        Me.colEmpID.FieldName = "EmpID"
        Me.colEmpID.Name = "colEmpID"
        resources.ApplyResources(Me.colEmpID, "colEmpID")
        '
        'colEmpName
        '
        Me.colEmpName.FieldName = "EmpName"
        Me.colEmpName.Name = "colEmpName"
        resources.ApplyResources(Me.colEmpName, "colEmpName")
        '
        'colEmpPhone
        '
        Me.colEmpPhone.FieldName = "EmpPhone"
        Me.colEmpPhone.Name = "colEmpPhone"
        resources.ApplyResources(Me.colEmpPhone, "colEmpPhone")
        '
        'colEmpAddress
        '
        Me.colEmpAddress.FieldName = "EmpAddress"
        Me.colEmpAddress.Name = "colEmpAddress"
        resources.ApplyResources(Me.colEmpAddress, "colEmpAddress")
        '
        'colWhatsAppPrefix
        '
        resources.ApplyResources(Me.colWhatsAppPrefix, "colWhatsAppPrefix")
        Me.colWhatsAppPrefix.FieldName = "WhatsAppPrefix"
        Me.colWhatsAppPrefix.Name = "colWhatsAppPrefix"
        '
        'colWhatsApp
        '
        resources.ApplyResources(Me.colWhatsApp, "colWhatsApp")
        Me.colWhatsApp.FieldName = "WhatsApp"
        Me.colWhatsApp.Name = "colWhatsApp"
        '
        'butCancel
        '
        Me.butCancel.Appearance.Font = CType(resources.GetObject("butCancel.Appearance.Font"), System.Drawing.Font)
        Me.butCancel.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.butCancel, "butCancel")
        Me.butCancel.Name = "butCancel"
        '
        'EmpImgImageEdit
        '
        resources.ApplyResources(Me.EmpImgImageEdit, "EmpImgImageEdit")
        Me.EmpImgImageEdit.Name = "EmpImgImageEdit"
        Me.EmpImgImageEdit.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.[Auto]
        '
        'btnBrowse
        '
        Me.btnBrowse.Appearance.Font = CType(resources.GetObject("btnBrowse.Appearance.Font"), System.Drawing.Font)
        Me.btnBrowse.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.btnBrowse, "btnBrowse")
        Me.btnBrowse.Name = "btnBrowse"
        '
        'btnAdd
        '
        Me.btnAdd.Appearance.Font = CType(resources.GetObject("btnAdd.Appearance.Font"), System.Drawing.Font)
        Me.btnAdd.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.btnAdd, "btnAdd")
        Me.btnAdd.Name = "btnAdd"
        '
        'btnEdit
        '
        Me.btnEdit.Appearance.Font = CType(resources.GetObject("btnEdit.Appearance.Font"), System.Drawing.Font)
        Me.btnEdit.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.btnEdit, "btnEdit")
        Me.btnEdit.Name = "btnEdit"
        '
        'btnDel
        '
        Me.btnDel.Appearance.Font = CType(resources.GetObject("btnDel.Appearance.Font"), System.Drawing.Font)
        Me.btnDel.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.btnDel, "btnDel")
        Me.btnDel.Name = "btnDel"
        '
        'butClose
        '
        Me.butClose.Appearance.Font = CType(resources.GetObject("butClose.Appearance.Font"), System.Drawing.Font)
        Me.butClose.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.butClose, "butClose")
        Me.butClose.Name = "butClose"
        '
        'EmpIDLabel
        '
        Me.EmpIDLabel.Appearance.Font = CType(resources.GetObject("EmpIDLabel.Appearance.Font"), System.Drawing.Font)
        Me.EmpIDLabel.Appearance.Options.UseFont = True
        Me.EmpIDLabel.Appearance.Options.UseTextOptions = True
        Me.EmpIDLabel.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.EmpIDLabel.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        resources.ApplyResources(Me.EmpIDLabel, "EmpIDLabel")
        Me.EmpIDLabel.Name = "EmpIDLabel"
        '
        'EmpIDSpinEdit
        '
        resources.ApplyResources(Me.EmpIDSpinEdit, "EmpIDSpinEdit")
        Me.EmpIDSpinEdit.Name = "EmpIDSpinEdit"
        Me.EmpIDSpinEdit.Properties.Appearance.Font = CType(resources.GetObject("EmpIDSpinEdit.Properties.Appearance.Font"), System.Drawing.Font)
        Me.EmpIDSpinEdit.Properties.Appearance.Options.UseFont = True
        Me.EmpIDSpinEdit.Properties.Appearance.Options.UseTextOptions = True
        Me.EmpIDSpinEdit.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.EmpIDSpinEdit.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        '
        'EmpNameLabel
        '
        Me.EmpNameLabel.Appearance.Font = CType(resources.GetObject("EmpNameLabel.Appearance.Font"), System.Drawing.Font)
        Me.EmpNameLabel.Appearance.Options.UseFont = True
        Me.EmpNameLabel.Appearance.Options.UseTextOptions = True
        Me.EmpNameLabel.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.EmpNameLabel.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        resources.ApplyResources(Me.EmpNameLabel, "EmpNameLabel")
        Me.EmpNameLabel.Name = "EmpNameLabel"
        '
        'EmpNameTextEdit
        '
        resources.ApplyResources(Me.EmpNameTextEdit, "EmpNameTextEdit")
        Me.EmpNameTextEdit.Name = "EmpNameTextEdit"
        Me.EmpNameTextEdit.Properties.Appearance.Font = CType(resources.GetObject("EmpNameTextEdit.Properties.Appearance.Font"), System.Drawing.Font)
        Me.EmpNameTextEdit.Properties.Appearance.Options.UseFont = True
        Me.EmpNameTextEdit.Properties.Appearance.Options.UseTextOptions = True
        Me.EmpNameTextEdit.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.EmpNameTextEdit.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        '
        'EmpImgLabel
        '
        Me.EmpImgLabel.Appearance.Font = CType(resources.GetObject("EmpImgLabel.Appearance.Font"), System.Drawing.Font)
        Me.EmpImgLabel.Appearance.Options.UseFont = True
        Me.EmpImgLabel.Appearance.Options.UseTextOptions = True
        Me.EmpImgLabel.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.EmpImgLabel.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        resources.ApplyResources(Me.EmpImgLabel, "EmpImgLabel")
        Me.EmpImgLabel.Name = "EmpImgLabel"
        '
        'EmpPhoneLabel
        '
        Me.EmpPhoneLabel.Appearance.Font = CType(resources.GetObject("EmpPhoneLabel.Appearance.Font"), System.Drawing.Font)
        Me.EmpPhoneLabel.Appearance.Options.UseFont = True
        Me.EmpPhoneLabel.Appearance.Options.UseTextOptions = True
        Me.EmpPhoneLabel.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.EmpPhoneLabel.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        resources.ApplyResources(Me.EmpPhoneLabel, "EmpPhoneLabel")
        Me.EmpPhoneLabel.Name = "EmpPhoneLabel"
        '
        'EmpAddressTextEdit
        '
        resources.ApplyResources(Me.EmpAddressTextEdit, "EmpAddressTextEdit")
        Me.EmpAddressTextEdit.Name = "EmpAddressTextEdit"
        Me.EmpAddressTextEdit.Properties.Appearance.Font = CType(resources.GetObject("EmpAddressTextEdit.Properties.Appearance.Font"), System.Drawing.Font)
        Me.EmpAddressTextEdit.Properties.Appearance.Options.UseFont = True
        Me.EmpAddressTextEdit.Properties.Appearance.Options.UseTextOptions = True
        Me.EmpAddressTextEdit.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.EmpAddressTextEdit.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        '
        'EmpPhoneTextEdit
        '
        resources.ApplyResources(Me.EmpPhoneTextEdit, "EmpPhoneTextEdit")
        Me.EmpPhoneTextEdit.Name = "EmpPhoneTextEdit"
        Me.EmpPhoneTextEdit.Properties.Appearance.Font = CType(resources.GetObject("EmpPhoneTextEdit.Properties.Appearance.Font"), System.Drawing.Font)
        Me.EmpPhoneTextEdit.Properties.Appearance.Options.UseFont = True
        Me.EmpPhoneTextEdit.Properties.Appearance.Options.UseTextOptions = True
        Me.EmpPhoneTextEdit.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.EmpPhoneTextEdit.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        '
        'EmpAddressLabel
        '
        Me.EmpAddressLabel.Appearance.Font = CType(resources.GetObject("EmpAddressLabel.Appearance.Font"), System.Drawing.Font)
        Me.EmpAddressLabel.Appearance.Options.UseFont = True
        Me.EmpAddressLabel.Appearance.Options.UseTextOptions = True
        Me.EmpAddressLabel.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.EmpAddressLabel.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        resources.ApplyResources(Me.EmpAddressLabel, "EmpAddressLabel")
        Me.EmpAddressLabel.Name = "EmpAddressLabel"
        '
        'EmpWhatsPrefixLabel
        '
        Me.EmpWhatsPrefixLabel.Appearance.Font = CType(resources.GetObject("EmpWhatsPrefixLabel.Appearance.Font"), System.Drawing.Font)
        Me.EmpWhatsPrefixLabel.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.EmpWhatsPrefixLabel, "EmpWhatsPrefixLabel")
        Me.EmpWhatsPrefixLabel.Name = "EmpWhatsPrefixLabel"
        '
        'EmpWhatsPrefixCombo
        '
        resources.ApplyResources(Me.EmpWhatsPrefixCombo, "EmpWhatsPrefixCombo")
        Me.EmpWhatsPrefixCombo.Name = "EmpWhatsPrefixCombo"
        Me.EmpWhatsPrefixCombo.Properties.Appearance.Font = CType(resources.GetObject("EmpWhatsPrefixCombo.Properties.Appearance.Font"), System.Drawing.Font)
        Me.EmpWhatsPrefixCombo.Properties.Appearance.Options.UseFont = True
        Me.EmpWhatsPrefixCombo.Properties.AppearanceDropDown.Font = CType(resources.GetObject("EmpWhatsPrefixCombo.Properties.AppearanceDropDown.Font"), System.Drawing.Font)
        Me.EmpWhatsPrefixCombo.Properties.AppearanceDropDown.Options.UseFont = True
        Me.EmpWhatsPrefixCombo.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("EmpWhatsPrefixCombo.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        '
        'EmpWhatsLabel
        '
        Me.EmpWhatsLabel.Appearance.Font = CType(resources.GetObject("EmpWhatsLabel.Appearance.Font"), System.Drawing.Font)
        Me.EmpWhatsLabel.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.EmpWhatsLabel, "EmpWhatsLabel")
        Me.EmpWhatsLabel.Name = "EmpWhatsLabel"
        '
        'EmpWhatsTextEdit
        '
        resources.ApplyResources(Me.EmpWhatsTextEdit, "EmpWhatsTextEdit")
        Me.EmpWhatsTextEdit.Name = "EmpWhatsTextEdit"
        Me.EmpWhatsTextEdit.Properties.Appearance.Font = CType(resources.GetObject("EmpWhatsTextEdit.Properties.Appearance.Font"), System.Drawing.Font)
        Me.EmpWhatsTextEdit.Properties.Appearance.Options.UseFont = True
        Me.EmpWhatsTextEdit.Properties.Appearance.Options.UseTextOptions = True
        Me.EmpWhatsTextEdit.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.EmpWhatsTextEdit.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.EmpWhatsTextEdit.Properties.MaxLength = 10
        '
        'lblEmpWhats
        '
        Me.lblEmpWhats.Appearance.Font = CType(resources.GetObject("lblEmpWhats.Appearance.Font"), System.Drawing.Font)
        Me.lblEmpWhats.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.lblEmpWhats.Appearance.Options.UseFont = True
        Me.lblEmpWhats.Appearance.Options.UseForeColor = True
        resources.ApplyResources(Me.lblEmpWhats, "lblEmpWhats")
        Me.lblEmpWhats.Name = "lblEmpWhats"
        '
        'FrmEmp
        '
        Me.Appearance.Options.UseFont = True
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.Splitter1)
        Me.Name = "FrmEmp"
        CType(Me.Splitter1.Panel1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Splitter1.Panel1.ResumeLayout(False)
        CType(Me.Splitter1.Panel2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Splitter1.Panel2.ResumeLayout(False)
        Me.Splitter1.Panel2.PerformLayout()
        CType(Me.Splitter1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Splitter1.ResumeLayout(False)
        CType(Me.DGV, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgView, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmpImgImageEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmpIDSpinEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmpNameTextEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmpAddressTextEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmpPhoneTextEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmpWhatsPrefixCombo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmpWhatsTextEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private WithEvents DGV As DevExpress.XtraGrid.GridControl
		Private WithEvents dgView As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents colRowNum As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents Splitter1 As DevExpress.XtraEditors.SplitContainerControl
    Friend WithEvents EmpIDSpinEdit As DevExpress.XtraEditors.SpinEdit
    Friend WithEvents EmpIDLabel As DevExpress.XtraEditors.LabelControl
		Friend WithEvents colEmpID As DevExpress.XtraGrid.Columns.GridColumn
		Friend WithEvents EmpNameTextEdit As DevExpress.XtraEditors.TextEdit
		Friend WithEvents EmpNameLabel As DevExpress.XtraEditors.LabelControl
		Friend WithEvents colEmpName As DevExpress.XtraGrid.Columns.GridColumn
		Friend WithEvents EmpPhoneTextEdit As DevExpress.XtraEditors.TextEdit
		Friend WithEvents EmpPhoneLabel As DevExpress.XtraEditors.LabelControl
		Friend WithEvents colEmpPhone As DevExpress.XtraGrid.Columns.GridColumn
		Friend WithEvents EmpAddressTextEdit As DevExpress.XtraEditors.TextEdit
		Friend WithEvents EmpAddressLabel As DevExpress.XtraEditors.LabelControl
		Friend WithEvents colEmpAddress As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colWhatsAppPrefix As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colWhatsApp As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents EmpWhatsPrefixLabel As DevExpress.XtraEditors.LabelControl
    Friend WithEvents EmpWhatsPrefixCombo As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents EmpWhatsLabel As DevExpress.XtraEditors.LabelControl
    Friend WithEvents EmpWhatsTextEdit As DevExpress.XtraEditors.TextEdit
    Friend WithEvents lblEmpWhats As DevExpress.XtraEditors.LabelControl
    Friend WithEvents EmpImgLabel As DevExpress.XtraEditors.LabelControl
    Friend WithEvents btnAdd As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnEdit As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnDel As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents butClose As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnBrowse As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents EmpImgImageEdit As DevExpress.XtraEditors.PictureEdit
    Friend WithEvents butCancel As DevExpress.XtraEditors.SimpleButton
End Class
