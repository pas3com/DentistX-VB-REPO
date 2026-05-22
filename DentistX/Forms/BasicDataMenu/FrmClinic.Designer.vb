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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmClinic))
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
        resources.ApplyResources(Me.Splitter1, "Splitter1")
        Me.Splitter1.CaptionImageOptions.ImageKey = resources.GetString("Splitter1.CaptionImageOptions.ImageKey")
        Me.Splitter1.CollapsePanel = DevExpress.XtraEditors.SplitCollapsePanel.Panel2
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
        Me.Splitter1.SplitterPosition = 293
        '
        'DGV
        '
        resources.ApplyResources(Me.DGV, "DGV")
        Me.DGV.EmbeddedNavigator.AccessibleDescription = resources.GetString("DGV.EmbeddedNavigator.AccessibleDescription")
        Me.DGV.EmbeddedNavigator.AccessibleName = resources.GetString("DGV.EmbeddedNavigator.AccessibleName")
        Me.DGV.EmbeddedNavigator.AllowHtmlTextInToolTip = CType(resources.GetObject("DGV.EmbeddedNavigator.AllowHtmlTextInToolTip"), DevExpress.Utils.DefaultBoolean)
        Me.DGV.EmbeddedNavigator.Anchor = CType(resources.GetObject("DGV.EmbeddedNavigator.Anchor"), System.Windows.Forms.AnchorStyles)
        Me.DGV.EmbeddedNavigator.Appearance.Options.UseFont = True
        Me.DGV.EmbeddedNavigator.AutoSize = CType(resources.GetObject("DGV.EmbeddedNavigator.AutoSize"), Boolean)
        Me.DGV.EmbeddedNavigator.BackgroundImage = CType(resources.GetObject("DGV.EmbeddedNavigator.BackgroundImage"), System.Drawing.Image)
        Me.DGV.EmbeddedNavigator.BackgroundImageLayout = CType(resources.GetObject("DGV.EmbeddedNavigator.BackgroundImageLayout"), System.Windows.Forms.ImageLayout)
        Me.DGV.EmbeddedNavigator.Buttons.Append.Hint = resources.GetString("DGV.EmbeddedNavigator.Buttons.Append.Hint")
        Me.DGV.EmbeddedNavigator.Buttons.Append.Visible = False
        Me.DGV.EmbeddedNavigator.Buttons.CancelEdit.Hint = resources.GetString("DGV.EmbeddedNavigator.Buttons.CancelEdit.Hint")
        Me.DGV.EmbeddedNavigator.Buttons.CancelEdit.Visible = False
        Me.DGV.EmbeddedNavigator.Buttons.Edit.Hint = resources.GetString("DGV.EmbeddedNavigator.Buttons.Edit.Hint")
        Me.DGV.EmbeddedNavigator.Buttons.Edit.Visible = False
        Me.DGV.EmbeddedNavigator.Buttons.EndEdit.Hint = resources.GetString("DGV.EmbeddedNavigator.Buttons.EndEdit.Hint")
        Me.DGV.EmbeddedNavigator.Buttons.EndEdit.Visible = False
        Me.DGV.EmbeddedNavigator.Buttons.Remove.Hint = resources.GetString("DGV.EmbeddedNavigator.Buttons.Remove.Hint")
        Me.DGV.EmbeddedNavigator.Buttons.Remove.Visible = False
        Me.DGV.EmbeddedNavigator.ImeMode = CType(resources.GetObject("DGV.EmbeddedNavigator.ImeMode"), System.Windows.Forms.ImeMode)
        Me.DGV.EmbeddedNavigator.MaximumSize = CType(resources.GetObject("DGV.EmbeddedNavigator.MaximumSize"), System.Drawing.Size)
        Me.DGV.EmbeddedNavigator.TextLocation = CType(resources.GetObject("DGV.EmbeddedNavigator.TextLocation"), DevExpress.XtraEditors.NavigatorButtonsTextLocation)
        Me.DGV.EmbeddedNavigator.ToolTip = resources.GetString("DGV.EmbeddedNavigator.ToolTip")
        Me.DGV.EmbeddedNavigator.ToolTipIconType = CType(resources.GetObject("DGV.EmbeddedNavigator.ToolTipIconType"), DevExpress.Utils.ToolTipIconType)
        Me.DGV.EmbeddedNavigator.ToolTipTitle = resources.GetString("DGV.EmbeddedNavigator.ToolTipTitle")
        Me.DGV.MainView = Me.dgView
        Me.DGV.Name = "DGV"
        Me.DGV.UseEmbeddedNavigator = True
        Me.DGV.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.dgView})
        '
        'dgView
        '
        resources.ApplyResources(Me.dgView, "dgView")
        Me.dgView.Appearance.HeaderPanel.Font = CType(resources.GetObject("dgView.Appearance.HeaderPanel.Font"), System.Drawing.Font)
        Me.dgView.Appearance.HeaderPanel.Options.UseFont = True
        Me.dgView.Appearance.Row.Font = CType(resources.GetObject("dgView.Appearance.Row.Font"), System.Drawing.Font)
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
        resources.ApplyResources(Me.colRowNum, "colRowNum")
        Me.colRowNum.FieldName = "colRowNum"
        Me.colRowNum.ImageOptions.ImageKey = resources.GetString("colRowNum.ImageOptions.ImageKey")
        Me.colRowNum.Name = "colRowNum"
        Me.colRowNum.UnboundDataType = GetType(Integer)
        '
        'colClinicID
        '
        resources.ApplyResources(Me.colClinicID, "colClinicID")
        Me.colClinicID.FieldName = "ClinicID"
        Me.colClinicID.ImageOptions.ImageKey = resources.GetString("colClinicID.ImageOptions.ImageKey")
        Me.colClinicID.Name = "colClinicID"
        '
        'colClinicNameEn
        '
        resources.ApplyResources(Me.colClinicNameEn, "colClinicNameEn")
        Me.colClinicNameEn.FieldName = "ClinicNameEn"
        Me.colClinicNameEn.ImageOptions.ImageKey = resources.GetString("colClinicNameEn.ImageOptions.ImageKey")
        Me.colClinicNameEn.Name = "colClinicNameEn"
        '
        'colClinicNameAr
        '
        resources.ApplyResources(Me.colClinicNameAr, "colClinicNameAr")
        Me.colClinicNameAr.FieldName = "ClinicNameAr"
        Me.colClinicNameAr.ImageOptions.ImageKey = resources.GetString("colClinicNameAr.ImageOptions.ImageKey")
        Me.colClinicNameAr.Name = "colClinicNameAr"
        '
        'colDrNameEn
        '
        resources.ApplyResources(Me.colDrNameEn, "colDrNameEn")
        Me.colDrNameEn.FieldName = "DrNameEn"
        Me.colDrNameEn.ImageOptions.ImageKey = resources.GetString("colDrNameEn.ImageOptions.ImageKey")
        Me.colDrNameEn.Name = "colDrNameEn"
        '
        'colDrNameAr
        '
        resources.ApplyResources(Me.colDrNameAr, "colDrNameAr")
        Me.colDrNameAr.FieldName = "DrNameAr"
        Me.colDrNameAr.ImageOptions.ImageKey = resources.GetString("colDrNameAr.ImageOptions.ImageKey")
        Me.colDrNameAr.Name = "colDrNameAr"
        '
        'colSpecialistEn
        '
        resources.ApplyResources(Me.colSpecialistEn, "colSpecialistEn")
        Me.colSpecialistEn.FieldName = "SpecialistEn"
        Me.colSpecialistEn.ImageOptions.ImageKey = resources.GetString("colSpecialistEn.ImageOptions.ImageKey")
        Me.colSpecialistEn.Name = "colSpecialistEn"
        '
        'colSpecialistAr
        '
        resources.ApplyResources(Me.colSpecialistAr, "colSpecialistAr")
        Me.colSpecialistAr.FieldName = "SpecialistAr"
        Me.colSpecialistAr.ImageOptions.ImageKey = resources.GetString("colSpecialistAr.ImageOptions.ImageKey")
        Me.colSpecialistAr.Name = "colSpecialistAr"
        '
        'colAddressEn
        '
        resources.ApplyResources(Me.colAddressEn, "colAddressEn")
        Me.colAddressEn.FieldName = "AddressEn"
        Me.colAddressEn.ImageOptions.ImageKey = resources.GetString("colAddressEn.ImageOptions.ImageKey")
        Me.colAddressEn.Name = "colAddressEn"
        '
        'colAddressAr
        '
        resources.ApplyResources(Me.colAddressAr, "colAddressAr")
        Me.colAddressAr.FieldName = "AddressAr"
        Me.colAddressAr.ImageOptions.ImageKey = resources.GetString("colAddressAr.ImageOptions.ImageKey")
        Me.colAddressAr.Name = "colAddressAr"
        '
        'colPhone
        '
        resources.ApplyResources(Me.colPhone, "colPhone")
        Me.colPhone.FieldName = "Phone"
        Me.colPhone.ImageOptions.ImageKey = resources.GetString("colPhone.ImageOptions.ImageKey")
        Me.colPhone.Name = "colPhone"
        '
        'colMobile
        '
        resources.ApplyResources(Me.colMobile, "colMobile")
        Me.colMobile.FieldName = "Mobile"
        Me.colMobile.ImageOptions.ImageKey = resources.GetString("colMobile.ImageOptions.ImageKey")
        Me.colMobile.Name = "colMobile"
        '
        'colEmail
        '
        resources.ApplyResources(Me.colEmail, "colEmail")
        Me.colEmail.FieldName = "Email"
        Me.colEmail.ImageOptions.ImageKey = resources.GetString("colEmail.ImageOptions.ImageKey")
        Me.colEmail.Name = "colEmail"
        '
        'btnAdd
        '
        resources.ApplyResources(Me.btnAdd, "btnAdd")
        Me.btnAdd.Appearance.Font = CType(resources.GetObject("btnAdd.Appearance.Font"), System.Drawing.Font)
        Me.btnAdd.Appearance.Options.UseFont = True
        Me.btnAdd.ImageOptions.ImageKey = resources.GetString("btnAdd.ImageOptions.ImageKey")
        Me.btnAdd.Name = "btnAdd"
        '
        'btnEdit
        '
        resources.ApplyResources(Me.btnEdit, "btnEdit")
        Me.btnEdit.Appearance.Font = CType(resources.GetObject("btnEdit.Appearance.Font"), System.Drawing.Font)
        Me.btnEdit.Appearance.Options.UseFont = True
        Me.btnEdit.ImageOptions.ImageKey = resources.GetString("btnEdit.ImageOptions.ImageKey")
        Me.btnEdit.Name = "btnEdit"
        '
        'btnDel
        '
        resources.ApplyResources(Me.btnDel, "btnDel")
        Me.btnDel.Appearance.Font = CType(resources.GetObject("btnDel.Appearance.Font"), System.Drawing.Font)
        Me.btnDel.Appearance.Options.UseFont = True
        Me.btnDel.ImageOptions.ImageKey = resources.GetString("btnDel.ImageOptions.ImageKey")
        Me.btnDel.Name = "btnDel"
        '
        'butClose
        '
        resources.ApplyResources(Me.butClose, "butClose")
        Me.butClose.Appearance.Font = CType(resources.GetObject("butClose.Appearance.Font"), System.Drawing.Font)
        Me.butClose.Appearance.Options.UseFont = True
        Me.butClose.ImageOptions.ImageKey = resources.GetString("butClose.ImageOptions.ImageKey")
        Me.butClose.Name = "butClose"
        '
        'btnBrowse
        '
        resources.ApplyResources(Me.btnBrowse, "btnBrowse")
        Me.btnBrowse.Appearance.Font = CType(resources.GetObject("btnBrowse.Appearance.Font"), System.Drawing.Font)
        Me.btnBrowse.Appearance.Options.UseFont = True
        Me.btnBrowse.ImageOptions.ImageKey = resources.GetString("btnBrowse.ImageOptions.ImageKey")
        Me.btnBrowse.Name = "btnBrowse"
        '
        'Logo
        '
        resources.ApplyResources(Me.Logo, "Logo")
        Me.Logo.Name = "Logo"
        Me.Logo.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.[Auto]
        '
        'ClinicIDLabel
        '
        resources.ApplyResources(Me.ClinicIDLabel, "ClinicIDLabel")
        Me.ClinicIDLabel.Appearance.Font = CType(resources.GetObject("ClinicIDLabel.Appearance.Font"), System.Drawing.Font)
        Me.ClinicIDLabel.Appearance.Options.UseFont = True
        Me.ClinicIDLabel.Appearance.Options.UseTextOptions = True
        Me.ClinicIDLabel.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.ClinicIDLabel.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.ClinicIDLabel.Name = "ClinicIDLabel"
        '
        'ClinicIDSpinEdit
        '
        resources.ApplyResources(Me.ClinicIDSpinEdit, "ClinicIDSpinEdit")
        Me.ClinicIDSpinEdit.Name = "ClinicIDSpinEdit"
        Me.ClinicIDSpinEdit.Properties.Appearance.Font = CType(resources.GetObject("ClinicIDSpinEdit.Properties.Appearance.Font"), System.Drawing.Font)
        Me.ClinicIDSpinEdit.Properties.Appearance.Options.UseFont = True
        Me.ClinicIDSpinEdit.Properties.Appearance.Options.UseTextOptions = True
        Me.ClinicIDSpinEdit.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.ClinicIDSpinEdit.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        '
        'DrNameEnTextEdit
        '
        resources.ApplyResources(Me.DrNameEnTextEdit, "DrNameEnTextEdit")
        Me.DrNameEnTextEdit.Name = "DrNameEnTextEdit"
        Me.DrNameEnTextEdit.Properties.Appearance.Font = CType(resources.GetObject("DrNameEnTextEdit.Properties.Appearance.Font"), System.Drawing.Font)
        Me.DrNameEnTextEdit.Properties.Appearance.Options.UseFont = True
        Me.DrNameEnTextEdit.Properties.Appearance.Options.UseTextOptions = True
        Me.DrNameEnTextEdit.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.DrNameEnTextEdit.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        '
        'ClinicNameEnLabel
        '
        resources.ApplyResources(Me.ClinicNameEnLabel, "ClinicNameEnLabel")
        Me.ClinicNameEnLabel.Appearance.Font = CType(resources.GetObject("ClinicNameEnLabel.Appearance.Font"), System.Drawing.Font)
        Me.ClinicNameEnLabel.Appearance.Options.UseFont = True
        Me.ClinicNameEnLabel.Appearance.Options.UseTextOptions = True
        Me.ClinicNameEnLabel.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.ClinicNameEnLabel.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.ClinicNameEnLabel.Name = "ClinicNameEnLabel"
        '
        'EmailTextEdit
        '
        resources.ApplyResources(Me.EmailTextEdit, "EmailTextEdit")
        Me.EmailTextEdit.Name = "EmailTextEdit"
        Me.EmailTextEdit.Properties.Appearance.Font = CType(resources.GetObject("EmailTextEdit.Properties.Appearance.Font"), System.Drawing.Font)
        Me.EmailTextEdit.Properties.Appearance.Options.UseFont = True
        Me.EmailTextEdit.Properties.Appearance.Options.UseTextOptions = True
        Me.EmailTextEdit.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.EmailTextEdit.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        '
        'ClinicNameEnTextEdit
        '
        resources.ApplyResources(Me.ClinicNameEnTextEdit, "ClinicNameEnTextEdit")
        Me.ClinicNameEnTextEdit.Name = "ClinicNameEnTextEdit"
        Me.ClinicNameEnTextEdit.Properties.Appearance.Font = CType(resources.GetObject("ClinicNameEnTextEdit.Properties.Appearance.Font"), System.Drawing.Font)
        Me.ClinicNameEnTextEdit.Properties.Appearance.Options.UseFont = True
        Me.ClinicNameEnTextEdit.Properties.Appearance.Options.UseTextOptions = True
        Me.ClinicNameEnTextEdit.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.ClinicNameEnTextEdit.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        '
        'EmailLabel
        '
        resources.ApplyResources(Me.EmailLabel, "EmailLabel")
        Me.EmailLabel.Appearance.Font = CType(resources.GetObject("EmailLabel.Appearance.Font"), System.Drawing.Font)
        Me.EmailLabel.Appearance.Options.UseFont = True
        Me.EmailLabel.Appearance.Options.UseTextOptions = True
        Me.EmailLabel.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.EmailLabel.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.EmailLabel.Name = "EmailLabel"
        '
        'ClinicNameArLabel
        '
        resources.ApplyResources(Me.ClinicNameArLabel, "ClinicNameArLabel")
        Me.ClinicNameArLabel.Appearance.Font = CType(resources.GetObject("ClinicNameArLabel.Appearance.Font"), System.Drawing.Font)
        Me.ClinicNameArLabel.Appearance.Options.UseFont = True
        Me.ClinicNameArLabel.Appearance.Options.UseTextOptions = True
        Me.ClinicNameArLabel.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.ClinicNameArLabel.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.ClinicNameArLabel.Name = "ClinicNameArLabel"
        '
        'MobileTextEdit
        '
        resources.ApplyResources(Me.MobileTextEdit, "MobileTextEdit")
        Me.MobileTextEdit.Name = "MobileTextEdit"
        Me.MobileTextEdit.Properties.Appearance.Font = CType(resources.GetObject("MobileTextEdit.Properties.Appearance.Font"), System.Drawing.Font)
        Me.MobileTextEdit.Properties.Appearance.Options.UseFont = True
        Me.MobileTextEdit.Properties.Appearance.Options.UseTextOptions = True
        Me.MobileTextEdit.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.MobileTextEdit.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        '
        'ClinicNameArTextEdit
        '
        resources.ApplyResources(Me.ClinicNameArTextEdit, "ClinicNameArTextEdit")
        Me.ClinicNameArTextEdit.Name = "ClinicNameArTextEdit"
        Me.ClinicNameArTextEdit.Properties.Appearance.Font = CType(resources.GetObject("ClinicNameArTextEdit.Properties.Appearance.Font"), System.Drawing.Font)
        Me.ClinicNameArTextEdit.Properties.Appearance.Options.UseFont = True
        Me.ClinicNameArTextEdit.Properties.Appearance.Options.UseTextOptions = True
        Me.ClinicNameArTextEdit.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.ClinicNameArTextEdit.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        '
        'MobileLabel
        '
        resources.ApplyResources(Me.MobileLabel, "MobileLabel")
        Me.MobileLabel.Appearance.Font = CType(resources.GetObject("MobileLabel.Appearance.Font"), System.Drawing.Font)
        Me.MobileLabel.Appearance.Options.UseFont = True
        Me.MobileLabel.Appearance.Options.UseTextOptions = True
        Me.MobileLabel.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.MobileLabel.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.MobileLabel.Name = "MobileLabel"
        '
        'DrNameEnLabel
        '
        resources.ApplyResources(Me.DrNameEnLabel, "DrNameEnLabel")
        Me.DrNameEnLabel.Appearance.Font = CType(resources.GetObject("DrNameEnLabel.Appearance.Font"), System.Drawing.Font)
        Me.DrNameEnLabel.Appearance.Options.UseFont = True
        Me.DrNameEnLabel.Appearance.Options.UseTextOptions = True
        Me.DrNameEnLabel.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.DrNameEnLabel.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.DrNameEnLabel.Name = "DrNameEnLabel"
        '
        'PhoneTextEdit
        '
        resources.ApplyResources(Me.PhoneTextEdit, "PhoneTextEdit")
        Me.PhoneTextEdit.Name = "PhoneTextEdit"
        Me.PhoneTextEdit.Properties.Appearance.Font = CType(resources.GetObject("PhoneTextEdit.Properties.Appearance.Font"), System.Drawing.Font)
        Me.PhoneTextEdit.Properties.Appearance.Options.UseFont = True
        Me.PhoneTextEdit.Properties.Appearance.Options.UseTextOptions = True
        Me.PhoneTextEdit.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.PhoneTextEdit.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        '
        'LabelControl1
        '
        resources.ApplyResources(Me.LabelControl1, "LabelControl1")
        Me.LabelControl1.Appearance.Font = CType(resources.GetObject("LabelControl1.Appearance.Font"), System.Drawing.Font)
        Me.LabelControl1.Appearance.Options.UseFont = True
        Me.LabelControl1.Appearance.Options.UseTextOptions = True
        Me.LabelControl1.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.LabelControl1.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.LabelControl1.Name = "LabelControl1"
        '
        'PhoneLabel
        '
        resources.ApplyResources(Me.PhoneLabel, "PhoneLabel")
        Me.PhoneLabel.Appearance.Font = CType(resources.GetObject("PhoneLabel.Appearance.Font"), System.Drawing.Font)
        Me.PhoneLabel.Appearance.Options.UseFont = True
        Me.PhoneLabel.Appearance.Options.UseTextOptions = True
        Me.PhoneLabel.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.PhoneLabel.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.PhoneLabel.Name = "PhoneLabel"
        '
        'DrNameArLabel
        '
        resources.ApplyResources(Me.DrNameArLabel, "DrNameArLabel")
        Me.DrNameArLabel.Appearance.Font = CType(resources.GetObject("DrNameArLabel.Appearance.Font"), System.Drawing.Font)
        Me.DrNameArLabel.Appearance.Options.UseFont = True
        Me.DrNameArLabel.Appearance.Options.UseTextOptions = True
        Me.DrNameArLabel.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.DrNameArLabel.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.DrNameArLabel.Name = "DrNameArLabel"
        '
        'AddressArTextEdit
        '
        resources.ApplyResources(Me.AddressArTextEdit, "AddressArTextEdit")
        Me.AddressArTextEdit.Name = "AddressArTextEdit"
        Me.AddressArTextEdit.Properties.Appearance.Font = CType(resources.GetObject("AddressArTextEdit.Properties.Appearance.Font"), System.Drawing.Font)
        Me.AddressArTextEdit.Properties.Appearance.Options.UseFont = True
        Me.AddressArTextEdit.Properties.Appearance.Options.UseTextOptions = True
        Me.AddressArTextEdit.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.AddressArTextEdit.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        '
        'DrNameArTextEdit
        '
        resources.ApplyResources(Me.DrNameArTextEdit, "DrNameArTextEdit")
        Me.DrNameArTextEdit.Name = "DrNameArTextEdit"
        Me.DrNameArTextEdit.Properties.Appearance.Font = CType(resources.GetObject("DrNameArTextEdit.Properties.Appearance.Font"), System.Drawing.Font)
        Me.DrNameArTextEdit.Properties.Appearance.Options.UseFont = True
        Me.DrNameArTextEdit.Properties.Appearance.Options.UseTextOptions = True
        Me.DrNameArTextEdit.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.DrNameArTextEdit.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        '
        'AddressArLabel
        '
        resources.ApplyResources(Me.AddressArLabel, "AddressArLabel")
        Me.AddressArLabel.Appearance.Font = CType(resources.GetObject("AddressArLabel.Appearance.Font"), System.Drawing.Font)
        Me.AddressArLabel.Appearance.Options.UseFont = True
        Me.AddressArLabel.Appearance.Options.UseTextOptions = True
        Me.AddressArLabel.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.AddressArLabel.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.AddressArLabel.Name = "AddressArLabel"
        '
        'SpecialistEnLabel
        '
        resources.ApplyResources(Me.SpecialistEnLabel, "SpecialistEnLabel")
        Me.SpecialistEnLabel.Appearance.Font = CType(resources.GetObject("SpecialistEnLabel.Appearance.Font"), System.Drawing.Font)
        Me.SpecialistEnLabel.Appearance.Options.UseFont = True
        Me.SpecialistEnLabel.Appearance.Options.UseTextOptions = True
        Me.SpecialistEnLabel.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.SpecialistEnLabel.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.SpecialistEnLabel.Name = "SpecialistEnLabel"
        '
        'AddressEnTextEdit
        '
        resources.ApplyResources(Me.AddressEnTextEdit, "AddressEnTextEdit")
        Me.AddressEnTextEdit.Name = "AddressEnTextEdit"
        Me.AddressEnTextEdit.Properties.Appearance.Font = CType(resources.GetObject("AddressEnTextEdit.Properties.Appearance.Font"), System.Drawing.Font)
        Me.AddressEnTextEdit.Properties.Appearance.Options.UseFont = True
        Me.AddressEnTextEdit.Properties.Appearance.Options.UseTextOptions = True
        Me.AddressEnTextEdit.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.AddressEnTextEdit.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        '
        'SpecialistEnTextEdit
        '
        resources.ApplyResources(Me.SpecialistEnTextEdit, "SpecialistEnTextEdit")
        Me.SpecialistEnTextEdit.Name = "SpecialistEnTextEdit"
        Me.SpecialistEnTextEdit.Properties.Appearance.Font = CType(resources.GetObject("SpecialistEnTextEdit.Properties.Appearance.Font"), System.Drawing.Font)
        Me.SpecialistEnTextEdit.Properties.Appearance.Options.UseFont = True
        Me.SpecialistEnTextEdit.Properties.Appearance.Options.UseTextOptions = True
        Me.SpecialistEnTextEdit.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.SpecialistEnTextEdit.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        '
        'AddressEnLabel
        '
        resources.ApplyResources(Me.AddressEnLabel, "AddressEnLabel")
        Me.AddressEnLabel.Appearance.Font = CType(resources.GetObject("AddressEnLabel.Appearance.Font"), System.Drawing.Font)
        Me.AddressEnLabel.Appearance.Options.UseFont = True
        Me.AddressEnLabel.Appearance.Options.UseTextOptions = True
        Me.AddressEnLabel.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.AddressEnLabel.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.AddressEnLabel.Name = "AddressEnLabel"
        '
        'SpecialistArLabel
        '
        resources.ApplyResources(Me.SpecialistArLabel, "SpecialistArLabel")
        Me.SpecialistArLabel.Appearance.Font = CType(resources.GetObject("SpecialistArLabel.Appearance.Font"), System.Drawing.Font)
        Me.SpecialistArLabel.Appearance.Options.UseFont = True
        Me.SpecialistArLabel.Appearance.Options.UseTextOptions = True
        Me.SpecialistArLabel.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.SpecialistArLabel.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.SpecialistArLabel.Name = "SpecialistArLabel"
        '
        'SpecialistArTextEdit
        '
        resources.ApplyResources(Me.SpecialistArTextEdit, "SpecialistArTextEdit")
        Me.SpecialistArTextEdit.Name = "SpecialistArTextEdit"
        Me.SpecialistArTextEdit.Properties.Appearance.Font = CType(resources.GetObject("SpecialistArTextEdit.Properties.Appearance.Font"), System.Drawing.Font)
        Me.SpecialistArTextEdit.Properties.Appearance.Options.UseFont = True
        Me.SpecialistArTextEdit.Properties.Appearance.Options.UseTextOptions = True
        Me.SpecialistArTextEdit.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.SpecialistArTextEdit.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        '
        'OFD
        '
        resources.ApplyResources(Me.OFD, "OFD")
        '
        'FrmClinic
        '
        resources.ApplyResources(Me, "$this")
        Me.Appearance.Options.UseFont = True
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.Splitter1)
        Me.Name = "FrmClinic"
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
