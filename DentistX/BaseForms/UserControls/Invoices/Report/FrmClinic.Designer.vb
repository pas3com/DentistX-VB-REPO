<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmClinic
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
        Me.colClinicID = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colClinicNameEn = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colClinicNameAr = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colDrNameEn = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colDrNameAr = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colSpecialistEn = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colSpecialistAr = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colAddressEn = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colAddressAr = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colPhone = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colMobile = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colEmail = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.btnAdd = New DevExpress.XtraEditors.SimpleButton()
        Me.btnEdit = New DevExpress.XtraEditors.SimpleButton()
        Me.btnDel = New DevExpress.XtraEditors.SimpleButton()
        Me.butClose = New DevExpress.XtraEditors.SimpleButton()
        Me.btnBrowse = New DevExpress.XtraEditors.SimpleButton()
        Me.Logo = New DevExpress.XtraEditors.PictureEdit()
        Me.ClinicIDLabel = New DevExpress.XtraEditors.LabelControl()
        Me.ClinicIDSpinEdit = New DevExpress.XtraEditors.TextEdit()
        Me.DrNameEnTextEdit = New DevExpress.XtraEditors.TextEdit()
        Me.ClinicNameEnLabel = New DevExpress.XtraEditors.LabelControl()
        Me.EmailTextEdit = New DevExpress.XtraEditors.TextEdit()
        Me.ClinicNameEnTextEdit = New DevExpress.XtraEditors.TextEdit()
        Me.EmailLabel = New DevExpress.XtraEditors.LabelControl()
        Me.ClinicNameArLabel = New DevExpress.XtraEditors.LabelControl()
        Me.MobileTextEdit = New DevExpress.XtraEditors.TextEdit()
        Me.ClinicNameArTextEdit = New DevExpress.XtraEditors.TextEdit()
        Me.MobileLabel = New DevExpress.XtraEditors.LabelControl()
        Me.DrNameEnLabel = New DevExpress.XtraEditors.LabelControl()
        Me.PhoneTextEdit = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.PhoneLabel = New DevExpress.XtraEditors.LabelControl()
        Me.DrNameArLabel = New DevExpress.XtraEditors.LabelControl()
        Me.AddressArTextEdit = New DevExpress.XtraEditors.TextEdit()
        Me.DrNameArTextEdit = New DevExpress.XtraEditors.TextEdit()
        Me.AddressArLabel = New DevExpress.XtraEditors.LabelControl()
        Me.SpecialistEnLabel = New DevExpress.XtraEditors.LabelControl()
        Me.AddressEnTextEdit = New DevExpress.XtraEditors.TextEdit()
        Me.SpecialistEnTextEdit = New DevExpress.XtraEditors.TextEdit()
        Me.AddressEnLabel = New DevExpress.XtraEditors.LabelControl()
        Me.SpecialistArLabel = New DevExpress.XtraEditors.LabelControl()
        Me.SpecialistArTextEdit = New DevExpress.XtraEditors.TextEdit()
        Me.OFD = New System.Windows.Forms.OpenFileDialog()
        CType(Me.Splitter1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Splitter1.Panel1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Splitter1.Panel1.SuspendLayout()
        CType(Me.Splitter1.Panel2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Splitter1.Panel2.SuspendLayout()
        Me.Splitter1.SuspendLayout()
        CType(Me.DGV, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgView, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Logo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ClinicIDSpinEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DrNameEnTextEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmailTextEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ClinicNameEnTextEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MobileTextEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ClinicNameArTextEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PhoneTextEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.AddressArTextEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DrNameArTextEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.AddressEnTextEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SpecialistEnTextEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SpecialistArTextEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
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
        'Splitter1.Panel1
        '
        Me.Splitter1.Panel1.Controls.Add(Me.DGV)
        '
        'Splitter1.Panel2
        '
        Me.Splitter1.Panel2.Controls.Add(Me.btnAdd)
        Me.Splitter1.Panel2.Controls.Add(Me.btnEdit)
        Me.Splitter1.Panel2.Controls.Add(Me.btnDel)
        Me.Splitter1.Panel2.Controls.Add(Me.butClose)
        Me.Splitter1.Panel2.Controls.Add(Me.btnBrowse)
        Me.Splitter1.Panel2.Controls.Add(Me.Logo)
        Me.Splitter1.Panel2.Controls.Add(Me.ClinicIDLabel)
        Me.Splitter1.Panel2.Controls.Add(Me.ClinicIDSpinEdit)
        Me.Splitter1.Panel2.Controls.Add(Me.DrNameEnTextEdit)
        Me.Splitter1.Panel2.Controls.Add(Me.ClinicNameEnLabel)
        Me.Splitter1.Panel2.Controls.Add(Me.EmailTextEdit)
        Me.Splitter1.Panel2.Controls.Add(Me.ClinicNameEnTextEdit)
        Me.Splitter1.Panel2.Controls.Add(Me.EmailLabel)
        Me.Splitter1.Panel2.Controls.Add(Me.ClinicNameArLabel)
        Me.Splitter1.Panel2.Controls.Add(Me.MobileTextEdit)
        Me.Splitter1.Panel2.Controls.Add(Me.ClinicNameArTextEdit)
        Me.Splitter1.Panel2.Controls.Add(Me.MobileLabel)
        Me.Splitter1.Panel2.Controls.Add(Me.DrNameEnLabel)
        Me.Splitter1.Panel2.Controls.Add(Me.PhoneTextEdit)
        Me.Splitter1.Panel2.Controls.Add(Me.LabelControl1)
        Me.Splitter1.Panel2.Controls.Add(Me.PhoneLabel)
        Me.Splitter1.Panel2.Controls.Add(Me.DrNameArLabel)
        Me.Splitter1.Panel2.Controls.Add(Me.AddressArTextEdit)
        Me.Splitter1.Panel2.Controls.Add(Me.DrNameArTextEdit)
        Me.Splitter1.Panel2.Controls.Add(Me.AddressArLabel)
        Me.Splitter1.Panel2.Controls.Add(Me.SpecialistEnLabel)
        Me.Splitter1.Panel2.Controls.Add(Me.AddressEnTextEdit)
        Me.Splitter1.Panel2.Controls.Add(Me.SpecialistEnTextEdit)
        Me.Splitter1.Panel2.Controls.Add(Me.AddressEnLabel)
        Me.Splitter1.Panel2.Controls.Add(Me.SpecialistArLabel)
        Me.Splitter1.Panel2.Controls.Add(Me.SpecialistArTextEdit)
        Me.Splitter1.Size = New System.Drawing.Size(992, 586)
        Me.Splitter1.SplitterPosition = 293
        Me.Splitter1.TabIndex = 0
        '
        'DGV
        '
        Me.DGV.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DGV.EmbeddedNavigator.Appearance.Options.UseFont = True
        Me.DGV.EmbeddedNavigator.Buttons.Append.Visible = False
        Me.DGV.EmbeddedNavigator.Buttons.CancelEdit.Visible = False
        Me.DGV.EmbeddedNavigator.Buttons.Edit.Visible = False
        Me.DGV.EmbeddedNavigator.Buttons.EndEdit.Visible = False
        Me.DGV.EmbeddedNavigator.Buttons.Remove.Visible = False
        Me.DGV.Location = New System.Drawing.Point(0, 0)
        Me.DGV.MainView = Me.dgView
        Me.DGV.Name = "DGV"
        Me.DGV.Size = New System.Drawing.Size(992, 293)
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
        Me.dgView.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.colRowNum, Me.colClinicID, Me.colClinicNameEn, Me.colClinicNameAr, Me.colDrNameEn, Me.colDrNameAr, Me.colSpecialistEn, Me.colSpecialistAr, Me.colAddressEn, Me.colAddressAr, Me.colPhone, Me.colMobile, Me.colEmail})
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
        'colClinicID
        '
        Me.colClinicID.FieldName = "ClinicID"
        Me.colClinicID.Name = "colClinicID"
        Me.colClinicID.Visible = True
        Me.colClinicID.VisibleIndex = 1
        '
        'colClinicNameEn
        '
        Me.colClinicNameEn.FieldName = "ClinicNameEn"
        Me.colClinicNameEn.Name = "colClinicNameEn"
        Me.colClinicNameEn.Visible = True
        Me.colClinicNameEn.VisibleIndex = 2
        '
        'colClinicNameAr
        '
        Me.colClinicNameAr.FieldName = "ClinicNameAr"
        Me.colClinicNameAr.Name = "colClinicNameAr"
        Me.colClinicNameAr.Visible = True
        Me.colClinicNameAr.VisibleIndex = 3
        '
        'colDrNameEn
        '
        Me.colDrNameEn.FieldName = "DrNameEn"
        Me.colDrNameEn.Name = "colDrNameEn"
        Me.colDrNameEn.Visible = True
        Me.colDrNameEn.VisibleIndex = 4
        '
        'colDrNameAr
        '
        Me.colDrNameAr.FieldName = "DrNameAr"
        Me.colDrNameAr.Name = "colDrNameAr"
        Me.colDrNameAr.Visible = True
        Me.colDrNameAr.VisibleIndex = 5
        '
        'colSpecialistEn
        '
        Me.colSpecialistEn.FieldName = "SpecialistEn"
        Me.colSpecialistEn.Name = "colSpecialistEn"
        Me.colSpecialistEn.Visible = True
        Me.colSpecialistEn.VisibleIndex = 6
        '
        'colSpecialistAr
        '
        Me.colSpecialistAr.FieldName = "SpecialistAr"
        Me.colSpecialistAr.Name = "colSpecialistAr"
        Me.colSpecialistAr.Visible = True
        Me.colSpecialistAr.VisibleIndex = 7
        '
        'colAddressEn
        '
        Me.colAddressEn.FieldName = "AddressEn"
        Me.colAddressEn.Name = "colAddressEn"
        Me.colAddressEn.Visible = True
        Me.colAddressEn.VisibleIndex = 8
        '
        'colAddressAr
        '
        Me.colAddressAr.FieldName = "AddressAr"
        Me.colAddressAr.Name = "colAddressAr"
        Me.colAddressAr.Visible = True
        Me.colAddressAr.VisibleIndex = 9
        '
        'colPhone
        '
        Me.colPhone.FieldName = "Phone"
        Me.colPhone.Name = "colPhone"
        Me.colPhone.Visible = True
        Me.colPhone.VisibleIndex = 10
        '
        'colMobile
        '
        Me.colMobile.FieldName = "Mobile"
        Me.colMobile.Name = "colMobile"
        Me.colMobile.Visible = True
        Me.colMobile.VisibleIndex = 11
        '
        'colEmail
        '
        Me.colEmail.FieldName = "Email"
        Me.colEmail.Name = "colEmail"
        Me.colEmail.Visible = True
        Me.colEmail.VisibleIndex = 12
        '
        'btnAdd
        '
        Me.btnAdd.Appearance.Font = New System.Drawing.Font("Calibri", 10.0!, System.Drawing.FontStyle.Bold)
        Me.btnAdd.Appearance.Options.UseFont = True
        Me.btnAdd.Location = New System.Drawing.Point(303, 220)
        Me.btnAdd.Name = "btnAdd"
        Me.btnAdd.Size = New System.Drawing.Size(84, 25)
        Me.btnAdd.TabIndex = 14
        Me.btnAdd.Text = "Add"
        '
        'btnEdit
        '
        Me.btnEdit.Appearance.Font = New System.Drawing.Font("Calibri", 10.0!, System.Drawing.FontStyle.Bold)
        Me.btnEdit.Appearance.Options.UseFont = True
        Me.btnEdit.Location = New System.Drawing.Point(406, 220)
        Me.btnEdit.Name = "btnEdit"
        Me.btnEdit.Size = New System.Drawing.Size(75, 25)
        Me.btnEdit.TabIndex = 15
        Me.btnEdit.Text = "Edit"
        '
        'btnDel
        '
        Me.btnDel.Appearance.Font = New System.Drawing.Font("Calibri", 10.0!, System.Drawing.FontStyle.Bold)
        Me.btnDel.Appearance.Options.UseFont = True
        Me.btnDel.Location = New System.Drawing.Point(503, 220)
        Me.btnDel.Name = "btnDel"
        Me.btnDel.Size = New System.Drawing.Size(75, 25)
        Me.btnDel.TabIndex = 16
        Me.btnDel.Text = "Delete"
        '
        'butClose
        '
        Me.butClose.Appearance.Font = New System.Drawing.Font("Calibri", 10.0!, System.Drawing.FontStyle.Bold)
        Me.butClose.Appearance.Options.UseFont = True
        Me.butClose.Location = New System.Drawing.Point(614, 220)
        Me.butClose.Name = "butClose"
        Me.butClose.Size = New System.Drawing.Size(67, 25)
        Me.butClose.TabIndex = 17
        Me.butClose.Text = "Close"
        '
        'btnBrowse
        '
        Me.btnBrowse.Appearance.Font = New System.Drawing.Font("Calibri", 10.0!, System.Drawing.FontStyle.Bold)
        Me.btnBrowse.Appearance.Options.UseFont = True
        Me.btnBrowse.Location = New System.Drawing.Point(562, 96)
        Me.btnBrowse.Name = "btnBrowse"
        Me.btnBrowse.Size = New System.Drawing.Size(75, 23)
        Me.btnBrowse.TabIndex = 13
        Me.btnBrowse.Text = "Browse"
        '
        'Logo
        '
        Me.Logo.Location = New System.Drawing.Point(446, 48)
        Me.Logo.Name = "Logo"
        Me.Logo.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.[Auto]
        Me.Logo.Size = New System.Drawing.Size(100, 101)
        Me.Logo.TabIndex = 12
        '
        'ClinicIDLabel
        '
        Me.ClinicIDLabel.Appearance.Font = New System.Drawing.Font("Calibri", 10.0!, System.Drawing.FontStyle.Bold)
        Me.ClinicIDLabel.Appearance.Options.UseFont = True
        Me.ClinicIDLabel.Appearance.Options.UseTextOptions = True
        Me.ClinicIDLabel.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.ClinicIDLabel.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.ClinicIDLabel.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.ClinicIDLabel.Location = New System.Drawing.Point(337, 182)
        Me.ClinicIDLabel.Name = "ClinicIDLabel"
        Me.ClinicIDLabel.Size = New System.Drawing.Size(100, 23)
        Me.ClinicIDLabel.TabIndex = 0
        Me.ClinicIDLabel.Text = "ClinicID"
        '
        'ClinicIDSpinEdit
        '
        Me.ClinicIDSpinEdit.Location = New System.Drawing.Point(446, 183)
        Me.ClinicIDSpinEdit.Name = "ClinicIDSpinEdit"
        Me.ClinicIDSpinEdit.Properties.Appearance.Font = New System.Drawing.Font("Calibri", 10.0!, System.Drawing.FontStyle.Bold)
        Me.ClinicIDSpinEdit.Properties.Appearance.Options.UseFont = True
        Me.ClinicIDSpinEdit.Properties.Appearance.Options.UseTextOptions = True
        Me.ClinicIDSpinEdit.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.ClinicIDSpinEdit.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.ClinicIDSpinEdit.Size = New System.Drawing.Size(280, 22)
        Me.ClinicIDSpinEdit.TabIndex = 1
        '
        'DrNameEnTextEdit
        '
        Me.DrNameEnTextEdit.Location = New System.Drawing.Point(124, 70)
        Me.DrNameEnTextEdit.Name = "DrNameEnTextEdit"
        Me.DrNameEnTextEdit.Properties.Appearance.Font = New System.Drawing.Font("Calibri", 10.0!, System.Drawing.FontStyle.Bold)
        Me.DrNameEnTextEdit.Properties.Appearance.Options.UseFont = True
        Me.DrNameEnTextEdit.Properties.Appearance.Options.UseTextOptions = True
        Me.DrNameEnTextEdit.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.DrNameEnTextEdit.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.DrNameEnTextEdit.Size = New System.Drawing.Size(202, 22)
        Me.DrNameEnTextEdit.TabIndex = 3
        '
        'ClinicNameEnLabel
        '
        Me.ClinicNameEnLabel.Appearance.Font = New System.Drawing.Font("Calibri", 10.0!, System.Drawing.FontStyle.Bold)
        Me.ClinicNameEnLabel.Appearance.Options.UseFont = True
        Me.ClinicNameEnLabel.Appearance.Options.UseTextOptions = True
        Me.ClinicNameEnLabel.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.ClinicNameEnLabel.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.ClinicNameEnLabel.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.ClinicNameEnLabel.Location = New System.Drawing.Point(18, 44)
        Me.ClinicNameEnLabel.Name = "ClinicNameEnLabel"
        Me.ClinicNameEnLabel.Size = New System.Drawing.Size(100, 23)
        Me.ClinicNameEnLabel.TabIndex = 0
        Me.ClinicNameEnLabel.Text = "ClinicNameEn"
        '
        'EmailTextEdit
        '
        Me.EmailTextEdit.Location = New System.Drawing.Point(446, 155)
        Me.EmailTextEdit.Name = "EmailTextEdit"
        Me.EmailTextEdit.Properties.Appearance.Font = New System.Drawing.Font("Calibri", 10.0!, System.Drawing.FontStyle.Bold)
        Me.EmailTextEdit.Properties.Appearance.Options.UseFont = True
        Me.EmailTextEdit.Properties.Appearance.Options.UseTextOptions = True
        Me.EmailTextEdit.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.EmailTextEdit.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.EmailTextEdit.Size = New System.Drawing.Size(165, 22)
        Me.EmailTextEdit.TabIndex = 11
        '
        'ClinicNameEnTextEdit
        '
        Me.ClinicNameEnTextEdit.Location = New System.Drawing.Point(124, 44)
        Me.ClinicNameEnTextEdit.Name = "ClinicNameEnTextEdit"
        Me.ClinicNameEnTextEdit.Properties.Appearance.Font = New System.Drawing.Font("Calibri", 10.0!, System.Drawing.FontStyle.Bold)
        Me.ClinicNameEnTextEdit.Properties.Appearance.Options.UseFont = True
        Me.ClinicNameEnTextEdit.Properties.Appearance.Options.UseTextOptions = True
        Me.ClinicNameEnTextEdit.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.ClinicNameEnTextEdit.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.ClinicNameEnTextEdit.Size = New System.Drawing.Size(202, 22)
        Me.ClinicNameEnTextEdit.TabIndex = 1
        '
        'EmailLabel
        '
        Me.EmailLabel.Appearance.Font = New System.Drawing.Font("Calibri", 10.0!, System.Drawing.FontStyle.Bold)
        Me.EmailLabel.Appearance.Options.UseFont = True
        Me.EmailLabel.Appearance.Options.UseTextOptions = True
        Me.EmailLabel.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.EmailLabel.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.EmailLabel.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.EmailLabel.Location = New System.Drawing.Point(337, 154)
        Me.EmailLabel.Name = "EmailLabel"
        Me.EmailLabel.Size = New System.Drawing.Size(100, 23)
        Me.EmailLabel.TabIndex = 10
        Me.EmailLabel.Text = "Email"
        '
        'ClinicNameArLabel
        '
        Me.ClinicNameArLabel.Appearance.Font = New System.Drawing.Font("Calibri", 10.0!, System.Drawing.FontStyle.Bold)
        Me.ClinicNameArLabel.Appearance.Options.UseFont = True
        Me.ClinicNameArLabel.Appearance.Options.UseTextOptions = True
        Me.ClinicNameArLabel.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.ClinicNameArLabel.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.ClinicNameArLabel.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.ClinicNameArLabel.Location = New System.Drawing.Point(672, 44)
        Me.ClinicNameArLabel.Name = "ClinicNameArLabel"
        Me.ClinicNameArLabel.Size = New System.Drawing.Size(100, 23)
        Me.ClinicNameArLabel.TabIndex = 2
        Me.ClinicNameArLabel.Text = "ClinicNameAr"
        '
        'MobileTextEdit
        '
        Me.MobileTextEdit.Location = New System.Drawing.Point(778, 154)
        Me.MobileTextEdit.Name = "MobileTextEdit"
        Me.MobileTextEdit.Properties.Appearance.Font = New System.Drawing.Font("Calibri", 10.0!, System.Drawing.FontStyle.Bold)
        Me.MobileTextEdit.Properties.Appearance.Options.UseFont = True
        Me.MobileTextEdit.Properties.Appearance.Options.UseTextOptions = True
        Me.MobileTextEdit.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.MobileTextEdit.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.MobileTextEdit.Size = New System.Drawing.Size(79, 22)
        Me.MobileTextEdit.TabIndex = 11
        '
        'ClinicNameArTextEdit
        '
        Me.ClinicNameArTextEdit.Location = New System.Drawing.Point(778, 44)
        Me.ClinicNameArTextEdit.Name = "ClinicNameArTextEdit"
        Me.ClinicNameArTextEdit.Properties.Appearance.Font = New System.Drawing.Font("Calibri", 10.0!, System.Drawing.FontStyle.Bold)
        Me.ClinicNameArTextEdit.Properties.Appearance.Options.UseFont = True
        Me.ClinicNameArTextEdit.Properties.Appearance.Options.UseTextOptions = True
        Me.ClinicNameArTextEdit.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.ClinicNameArTextEdit.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.ClinicNameArTextEdit.Size = New System.Drawing.Size(195, 22)
        Me.ClinicNameArTextEdit.TabIndex = 3
        '
        'MobileLabel
        '
        Me.MobileLabel.Appearance.Font = New System.Drawing.Font("Calibri", 10.0!, System.Drawing.FontStyle.Bold)
        Me.MobileLabel.Appearance.Options.UseFont = True
        Me.MobileLabel.Appearance.Options.UseTextOptions = True
        Me.MobileLabel.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.MobileLabel.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.MobileLabel.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.MobileLabel.Location = New System.Drawing.Point(672, 154)
        Me.MobileLabel.Name = "MobileLabel"
        Me.MobileLabel.Size = New System.Drawing.Size(100, 23)
        Me.MobileLabel.TabIndex = 10
        Me.MobileLabel.Text = "Mobile"
        '
        'DrNameEnLabel
        '
        Me.DrNameEnLabel.Appearance.Font = New System.Drawing.Font("Calibri", 10.0!, System.Drawing.FontStyle.Bold)
        Me.DrNameEnLabel.Appearance.Options.UseFont = True
        Me.DrNameEnLabel.Appearance.Options.UseTextOptions = True
        Me.DrNameEnLabel.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.DrNameEnLabel.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.DrNameEnLabel.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.DrNameEnLabel.Location = New System.Drawing.Point(18, 70)
        Me.DrNameEnLabel.Name = "DrNameEnLabel"
        Me.DrNameEnLabel.Size = New System.Drawing.Size(100, 23)
        Me.DrNameEnLabel.TabIndex = 2
        Me.DrNameEnLabel.Text = "DrNameEn"
        '
        'PhoneTextEdit
        '
        Me.PhoneTextEdit.Location = New System.Drawing.Point(124, 154)
        Me.PhoneTextEdit.Name = "PhoneTextEdit"
        Me.PhoneTextEdit.Properties.Appearance.Font = New System.Drawing.Font("Calibri", 10.0!, System.Drawing.FontStyle.Bold)
        Me.PhoneTextEdit.Properties.Appearance.Options.UseFont = True
        Me.PhoneTextEdit.Properties.Appearance.Options.UseTextOptions = True
        Me.PhoneTextEdit.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.PhoneTextEdit.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.PhoneTextEdit.Size = New System.Drawing.Size(79, 22)
        Me.PhoneTextEdit.TabIndex = 9
        '
        'LabelControl1
        '
        Me.LabelControl1.Appearance.Font = New System.Drawing.Font("Calibri", 10.0!, System.Drawing.FontStyle.Bold)
        Me.LabelControl1.Appearance.Options.UseFont = True
        Me.LabelControl1.Appearance.Options.UseTextOptions = True
        Me.LabelControl1.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.LabelControl1.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.LabelControl1.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.LabelControl1.Location = New System.Drawing.Point(443, 19)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(100, 23)
        Me.LabelControl1.TabIndex = 8
        Me.LabelControl1.Text = "LOGO"
        '
        'PhoneLabel
        '
        Me.PhoneLabel.Appearance.Font = New System.Drawing.Font("Calibri", 10.0!, System.Drawing.FontStyle.Bold)
        Me.PhoneLabel.Appearance.Options.UseFont = True
        Me.PhoneLabel.Appearance.Options.UseTextOptions = True
        Me.PhoneLabel.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.PhoneLabel.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.PhoneLabel.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.PhoneLabel.Location = New System.Drawing.Point(18, 154)
        Me.PhoneLabel.Name = "PhoneLabel"
        Me.PhoneLabel.Size = New System.Drawing.Size(100, 23)
        Me.PhoneLabel.TabIndex = 8
        Me.PhoneLabel.Text = "Phone"
        '
        'DrNameArLabel
        '
        Me.DrNameArLabel.Appearance.Font = New System.Drawing.Font("Calibri", 10.0!, System.Drawing.FontStyle.Bold)
        Me.DrNameArLabel.Appearance.Options.UseFont = True
        Me.DrNameArLabel.Appearance.Options.UseTextOptions = True
        Me.DrNameArLabel.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.DrNameArLabel.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.DrNameArLabel.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.DrNameArLabel.Location = New System.Drawing.Point(672, 70)
        Me.DrNameArLabel.Name = "DrNameArLabel"
        Me.DrNameArLabel.Size = New System.Drawing.Size(100, 23)
        Me.DrNameArLabel.TabIndex = 4
        Me.DrNameArLabel.Text = "DrNameAr"
        '
        'AddressArTextEdit
        '
        Me.AddressArTextEdit.Location = New System.Drawing.Point(778, 122)
        Me.AddressArTextEdit.Name = "AddressArTextEdit"
        Me.AddressArTextEdit.Properties.Appearance.Font = New System.Drawing.Font("Calibri", 10.0!, System.Drawing.FontStyle.Bold)
        Me.AddressArTextEdit.Properties.Appearance.Options.UseFont = True
        Me.AddressArTextEdit.Properties.Appearance.Options.UseTextOptions = True
        Me.AddressArTextEdit.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.AddressArTextEdit.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.AddressArTextEdit.Size = New System.Drawing.Size(195, 22)
        Me.AddressArTextEdit.TabIndex = 9
        '
        'DrNameArTextEdit
        '
        Me.DrNameArTextEdit.Location = New System.Drawing.Point(778, 70)
        Me.DrNameArTextEdit.Name = "DrNameArTextEdit"
        Me.DrNameArTextEdit.Properties.Appearance.Font = New System.Drawing.Font("Calibri", 10.0!, System.Drawing.FontStyle.Bold)
        Me.DrNameArTextEdit.Properties.Appearance.Options.UseFont = True
        Me.DrNameArTextEdit.Properties.Appearance.Options.UseTextOptions = True
        Me.DrNameArTextEdit.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.DrNameArTextEdit.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.DrNameArTextEdit.Size = New System.Drawing.Size(195, 22)
        Me.DrNameArTextEdit.TabIndex = 5
        '
        'AddressArLabel
        '
        Me.AddressArLabel.Appearance.Font = New System.Drawing.Font("Calibri", 10.0!, System.Drawing.FontStyle.Bold)
        Me.AddressArLabel.Appearance.Options.UseFont = True
        Me.AddressArLabel.Appearance.Options.UseTextOptions = True
        Me.AddressArLabel.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.AddressArLabel.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.AddressArLabel.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.AddressArLabel.Location = New System.Drawing.Point(672, 122)
        Me.AddressArLabel.Name = "AddressArLabel"
        Me.AddressArLabel.Size = New System.Drawing.Size(100, 23)
        Me.AddressArLabel.TabIndex = 8
        Me.AddressArLabel.Text = "AddressAr"
        '
        'SpecialistEnLabel
        '
        Me.SpecialistEnLabel.Appearance.Font = New System.Drawing.Font("Calibri", 10.0!, System.Drawing.FontStyle.Bold)
        Me.SpecialistEnLabel.Appearance.Options.UseFont = True
        Me.SpecialistEnLabel.Appearance.Options.UseTextOptions = True
        Me.SpecialistEnLabel.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.SpecialistEnLabel.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.SpecialistEnLabel.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.SpecialistEnLabel.Location = New System.Drawing.Point(18, 96)
        Me.SpecialistEnLabel.Name = "SpecialistEnLabel"
        Me.SpecialistEnLabel.Size = New System.Drawing.Size(100, 23)
        Me.SpecialistEnLabel.TabIndex = 4
        Me.SpecialistEnLabel.Text = "SpecialistEn"
        '
        'AddressEnTextEdit
        '
        Me.AddressEnTextEdit.Location = New System.Drawing.Point(124, 122)
        Me.AddressEnTextEdit.Name = "AddressEnTextEdit"
        Me.AddressEnTextEdit.Properties.Appearance.Font = New System.Drawing.Font("Calibri", 10.0!, System.Drawing.FontStyle.Bold)
        Me.AddressEnTextEdit.Properties.Appearance.Options.UseFont = True
        Me.AddressEnTextEdit.Properties.Appearance.Options.UseTextOptions = True
        Me.AddressEnTextEdit.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.AddressEnTextEdit.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.AddressEnTextEdit.Size = New System.Drawing.Size(202, 22)
        Me.AddressEnTextEdit.TabIndex = 7
        '
        'SpecialistEnTextEdit
        '
        Me.SpecialistEnTextEdit.Location = New System.Drawing.Point(124, 96)
        Me.SpecialistEnTextEdit.Name = "SpecialistEnTextEdit"
        Me.SpecialistEnTextEdit.Properties.Appearance.Font = New System.Drawing.Font("Calibri", 10.0!, System.Drawing.FontStyle.Bold)
        Me.SpecialistEnTextEdit.Properties.Appearance.Options.UseFont = True
        Me.SpecialistEnTextEdit.Properties.Appearance.Options.UseTextOptions = True
        Me.SpecialistEnTextEdit.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.SpecialistEnTextEdit.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.SpecialistEnTextEdit.Size = New System.Drawing.Size(202, 22)
        Me.SpecialistEnTextEdit.TabIndex = 5
        '
        'AddressEnLabel
        '
        Me.AddressEnLabel.Appearance.Font = New System.Drawing.Font("Calibri", 10.0!, System.Drawing.FontStyle.Bold)
        Me.AddressEnLabel.Appearance.Options.UseFont = True
        Me.AddressEnLabel.Appearance.Options.UseTextOptions = True
        Me.AddressEnLabel.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.AddressEnLabel.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.AddressEnLabel.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.AddressEnLabel.Location = New System.Drawing.Point(18, 122)
        Me.AddressEnLabel.Name = "AddressEnLabel"
        Me.AddressEnLabel.Size = New System.Drawing.Size(100, 23)
        Me.AddressEnLabel.TabIndex = 6
        Me.AddressEnLabel.Text = "AddressEn"
        '
        'SpecialistArLabel
        '
        Me.SpecialistArLabel.Appearance.Font = New System.Drawing.Font("Calibri", 10.0!, System.Drawing.FontStyle.Bold)
        Me.SpecialistArLabel.Appearance.Options.UseFont = True
        Me.SpecialistArLabel.Appearance.Options.UseTextOptions = True
        Me.SpecialistArLabel.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.SpecialistArLabel.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.SpecialistArLabel.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.SpecialistArLabel.Location = New System.Drawing.Point(672, 96)
        Me.SpecialistArLabel.Name = "SpecialistArLabel"
        Me.SpecialistArLabel.Size = New System.Drawing.Size(100, 23)
        Me.SpecialistArLabel.TabIndex = 6
        Me.SpecialistArLabel.Text = "SpecialistAr"
        '
        'SpecialistArTextEdit
        '
        Me.SpecialistArTextEdit.Location = New System.Drawing.Point(778, 96)
        Me.SpecialistArTextEdit.Name = "SpecialistArTextEdit"
        Me.SpecialistArTextEdit.Properties.Appearance.Font = New System.Drawing.Font("Calibri", 10.0!, System.Drawing.FontStyle.Bold)
        Me.SpecialistArTextEdit.Properties.Appearance.Options.UseFont = True
        Me.SpecialistArTextEdit.Properties.Appearance.Options.UseTextOptions = True
        Me.SpecialistArTextEdit.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.SpecialistArTextEdit.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.SpecialistArTextEdit.Size = New System.Drawing.Size(195, 22)
        Me.SpecialistArTextEdit.TabIndex = 7
        '
        'OFD
        '
        Me.OFD.Filter = "All files (*.*)|*.*|Bitmap Files(*.bmp)|*.bmp|JPeg Files(*.jpg)|*.jpg|PNG Files(*" &
    ".png)|*.png"
        '
        'FrmClinic
        '
        Me.Appearance.Options.UseFont = True
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(992, 586)
        Me.Controls.Add(Me.Splitter1)
        Me.Name = "FrmClinic"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Clinic Informations"
        CType(Me.Splitter1.Panel1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Splitter1.Panel1.ResumeLayout(False)
        CType(Me.Splitter1.Panel2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Splitter1.Panel2.ResumeLayout(False)
        CType(Me.Splitter1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Splitter1.ResumeLayout(False)
        CType(Me.DGV, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgView, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Logo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ClinicIDSpinEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DrNameEnTextEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmailTextEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ClinicNameEnTextEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MobileTextEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ClinicNameArTextEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PhoneTextEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.AddressArTextEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DrNameArTextEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.AddressEnTextEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SpecialistEnTextEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SpecialistArTextEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private WithEvents DGV As DevExpress.XtraGrid.GridControl
		Private WithEvents dgView As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents colRowNum As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents Splitter1 As DevExpress.XtraEditors.SplitContainerControl
    Friend WithEvents ClinicIDSpinEdit As DevExpress.XtraEditors.TextEdit
    Friend WithEvents ClinicIDLabel As DevExpress.XtraEditors.LabelControl
    Friend WithEvents colClinicID As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ClinicNameEnTextEdit As DevExpress.XtraEditors.TextEdit
    Friend WithEvents ClinicNameEnLabel As DevExpress.XtraEditors.LabelControl
    Friend WithEvents colClinicNameEn As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ClinicNameArTextEdit As DevExpress.XtraEditors.TextEdit
    Friend WithEvents ClinicNameArLabel As DevExpress.XtraEditors.LabelControl
    Friend WithEvents colClinicNameAr As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents DrNameEnTextEdit As DevExpress.XtraEditors.TextEdit
    Friend WithEvents DrNameEnLabel As DevExpress.XtraEditors.LabelControl
    Friend WithEvents colDrNameEn As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents DrNameArTextEdit As DevExpress.XtraEditors.TextEdit
    Friend WithEvents DrNameArLabel As DevExpress.XtraEditors.LabelControl
    Friend WithEvents colDrNameAr As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents SpecialistEnTextEdit As DevExpress.XtraEditors.TextEdit
    Friend WithEvents SpecialistEnLabel As DevExpress.XtraEditors.LabelControl
    Friend WithEvents colSpecialistEn As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents SpecialistArTextEdit As DevExpress.XtraEditors.TextEdit
    Friend WithEvents SpecialistArLabel As DevExpress.XtraEditors.LabelControl
    Friend WithEvents colSpecialistAr As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents AddressEnTextEdit As DevExpress.XtraEditors.TextEdit
    Friend WithEvents AddressEnLabel As DevExpress.XtraEditors.LabelControl
    Friend WithEvents colAddressEn As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents AddressArTextEdit As DevExpress.XtraEditors.TextEdit
    Friend WithEvents AddressArLabel As DevExpress.XtraEditors.LabelControl
    Friend WithEvents colAddressAr As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents PhoneTextEdit As DevExpress.XtraEditors.TextEdit
    Friend WithEvents PhoneLabel As DevExpress.XtraEditors.LabelControl
    Friend WithEvents colPhone As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents MobileTextEdit As DevExpress.XtraEditors.TextEdit
    Friend WithEvents MobileLabel As DevExpress.XtraEditors.LabelControl
    Friend WithEvents colMobile As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents EmailTextEdit As DevExpress.XtraEditors.TextEdit
    Friend WithEvents EmailLabel As DevExpress.XtraEditors.LabelControl
    Friend WithEvents colEmail As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents Logo As DevExpress.XtraEditors.PictureEdit
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents btnBrowse As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnAdd As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnEdit As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnDel As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents butClose As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents OFD As OpenFileDialog
End Class
