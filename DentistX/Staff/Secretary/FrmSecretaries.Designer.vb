<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmSecretaries
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmSecretaries))
        Me.Splitter1 = New DevExpress.XtraEditors.SplitContainerControl()
        Me.DGV = New DevExpress.XtraGrid.GridControl()
        Me.dgView = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.colRowNum = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colSecID = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colSecName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colSecAdres = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colSecPhone = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colSecMobile = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colWhatsAppPrefix = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colWhatsApp = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.butCancel = New DevExpress.XtraEditors.SimpleButton()
        Me.btnAdd = New DevExpress.XtraEditors.SimpleButton()
        Me.btnEdit = New DevExpress.XtraEditors.SimpleButton()
        Me.btnDel = New DevExpress.XtraEditors.SimpleButton()
        Me.butClose = New DevExpress.XtraEditors.SimpleButton()
        Me.ColorMark = New DevExpress.XtraEditors.ColorPickEdit()
        Me.SecIDLabel = New DevExpress.XtraEditors.LabelControl()
        Me.SecIDSpinEdit = New DevExpress.XtraEditors.SpinEdit()
        Me.SecNameLabel = New DevExpress.XtraEditors.LabelControl()
        Me.SecNameTextEdit = New DevExpress.XtraEditors.TextEdit()
        Me.SecColorLabel = New DevExpress.XtraEditors.LabelControl()
        Me.SecAdresLabel = New DevExpress.XtraEditors.LabelControl()
        Me.SecColorTextEdit = New DevExpress.XtraEditors.TextEdit()
        Me.SecAdresTextEdit = New DevExpress.XtraEditors.TextEdit()
        Me.SecMobileTextEdit = New DevExpress.XtraEditors.TextEdit()
        Me.SecPhoneLabel = New DevExpress.XtraEditors.LabelControl()
        Me.SecMobileLabel = New DevExpress.XtraEditors.LabelControl()
        Me.SecPhoneTextEdit = New DevExpress.XtraEditors.TextEdit()
        Me.SecWhatsPrefixLabel = New DevExpress.XtraEditors.LabelControl()
        Me.SecWhatsPrefixCombo = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.SecWhatsLabel = New DevExpress.XtraEditors.LabelControl()
        Me.SecWhatsTextEdit = New DevExpress.XtraEditors.TextEdit()
        Me.lblSecWhats = New DevExpress.XtraEditors.LabelControl()
        CType(Me.Splitter1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Splitter1.Panel1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Splitter1.Panel1.SuspendLayout()
        CType(Me.Splitter1.Panel2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Splitter1.Panel2.SuspendLayout()
        Me.Splitter1.SuspendLayout()
        CType(Me.DGV, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgView, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ColorMark.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SecIDSpinEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SecNameTextEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SecColorTextEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SecAdresTextEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SecMobileTextEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SecPhoneTextEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SecWhatsPrefixCombo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SecWhatsTextEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.Splitter1.Panel2.Controls.Add(Me.butCancel)
        Me.Splitter1.Panel2.Controls.Add(Me.btnAdd)
        Me.Splitter1.Panel2.Controls.Add(Me.btnEdit)
        Me.Splitter1.Panel2.Controls.Add(Me.btnDel)
        Me.Splitter1.Panel2.Controls.Add(Me.butClose)
        Me.Splitter1.Panel2.Controls.Add(Me.ColorMark)
        Me.Splitter1.Panel2.Controls.Add(Me.SecIDLabel)
        Me.Splitter1.Panel2.Controls.Add(Me.SecIDSpinEdit)
        Me.Splitter1.Panel2.Controls.Add(Me.SecNameLabel)
        Me.Splitter1.Panel2.Controls.Add(Me.SecNameTextEdit)
        Me.Splitter1.Panel2.Controls.Add(Me.SecColorLabel)
        Me.Splitter1.Panel2.Controls.Add(Me.SecAdresLabel)
        Me.Splitter1.Panel2.Controls.Add(Me.SecColorTextEdit)
        Me.Splitter1.Panel2.Controls.Add(Me.SecAdresTextEdit)
        Me.Splitter1.Panel2.Controls.Add(Me.SecMobileTextEdit)
        Me.Splitter1.Panel2.Controls.Add(Me.SecPhoneLabel)
        Me.Splitter1.Panel2.Controls.Add(Me.SecMobileLabel)
        Me.Splitter1.Panel2.Controls.Add(Me.SecPhoneTextEdit)
        Me.Splitter1.Panel2.Controls.Add(Me.SecWhatsPrefixLabel)
        Me.Splitter1.Panel2.Controls.Add(Me.SecWhatsPrefixCombo)
        Me.Splitter1.Panel2.Controls.Add(Me.SecWhatsLabel)
        Me.Splitter1.Panel2.Controls.Add(Me.SecWhatsTextEdit)
        Me.Splitter1.Panel2.Controls.Add(Me.lblSecWhats)
        Me.Splitter1.SplitterPosition = 403
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
        Me.dgView.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.colRowNum, Me.colSecID, Me.colSecName, Me.colSecAdres, Me.colSecPhone, Me.colSecMobile, Me.colWhatsAppPrefix, Me.colWhatsApp})
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
        'colSecID
        '
        resources.ApplyResources(Me.colSecID, "colSecID")
        Me.colSecID.FieldName = "SecID"
        Me.colSecID.ImageOptions.ImageKey = resources.GetString("colSecID.ImageOptions.ImageKey")
        Me.colSecID.Name = "colSecID"
        '
        'colSecName
        '
        resources.ApplyResources(Me.colSecName, "colSecName")
        Me.colSecName.FieldName = "SecName"
        Me.colSecName.ImageOptions.ImageKey = resources.GetString("colSecName.ImageOptions.ImageKey")
        Me.colSecName.Name = "colSecName"
        '
        'colSecAdres
        '
        resources.ApplyResources(Me.colSecAdres, "colSecAdres")
        Me.colSecAdres.FieldName = "SecAdres"
        Me.colSecAdres.ImageOptions.ImageKey = resources.GetString("colSecAdres.ImageOptions.ImageKey")
        Me.colSecAdres.Name = "colSecAdres"
        '
        'colSecPhone
        '
        resources.ApplyResources(Me.colSecPhone, "colSecPhone")
        Me.colSecPhone.FieldName = "SecPhone"
        Me.colSecPhone.ImageOptions.ImageKey = resources.GetString("colSecPhone.ImageOptions.ImageKey")
        Me.colSecPhone.Name = "colSecPhone"
        '
        'colSecMobile
        '
        resources.ApplyResources(Me.colSecMobile, "colSecMobile")
        Me.colSecMobile.FieldName = "SecMobile"
        Me.colSecMobile.ImageOptions.ImageKey = resources.GetString("colSecMobile.ImageOptions.ImageKey")
        Me.colSecMobile.Name = "colSecMobile"
        '
        'colWhatsAppPrefix
        '
        resources.ApplyResources(Me.colWhatsAppPrefix, "colWhatsAppPrefix")
        Me.colWhatsAppPrefix.FieldName = "WhatsAppPrefix"
        Me.colWhatsAppPrefix.ImageOptions.ImageKey = resources.GetString("colWhatsAppPrefix.ImageOptions.ImageKey")
        Me.colWhatsAppPrefix.Name = "colWhatsAppPrefix"
        '
        'colWhatsApp
        '
        resources.ApplyResources(Me.colWhatsApp, "colWhatsApp")
        Me.colWhatsApp.FieldName = "WhatsApp"
        Me.colWhatsApp.ImageOptions.ImageKey = resources.GetString("colWhatsApp.ImageOptions.ImageKey")
        Me.colWhatsApp.Name = "colWhatsApp"
        '
        'butCancel
        '
        resources.ApplyResources(Me.butCancel, "butCancel")
        Me.butCancel.Appearance.Font = CType(resources.GetObject("butCancel.Appearance.Font"), System.Drawing.Font)
        Me.butCancel.Appearance.Options.UseFont = True
        Me.butCancel.ImageOptions.ImageKey = resources.GetString("butCancel.ImageOptions.ImageKey")
        Me.butCancel.Name = "butCancel"
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
        'ColorMark
        '
        resources.ApplyResources(Me.ColorMark, "ColorMark")
        Me.ColorMark.Name = "ColorMark"
        Me.ColorMark.Properties.AutomaticColor = System.Drawing.Color.Black
        Me.ColorMark.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("ColorMark.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        '
        'SecIDLabel
        '
        resources.ApplyResources(Me.SecIDLabel, "SecIDLabel")
        Me.SecIDLabel.Appearance.Font = CType(resources.GetObject("SecIDLabel.Appearance.Font"), System.Drawing.Font)
        Me.SecIDLabel.Appearance.Options.UseFont = True
        Me.SecIDLabel.Appearance.Options.UseTextOptions = True
        Me.SecIDLabel.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.SecIDLabel.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.SecIDLabel.Name = "SecIDLabel"
        '
        'SecIDSpinEdit
        '
        resources.ApplyResources(Me.SecIDSpinEdit, "SecIDSpinEdit")
        Me.SecIDSpinEdit.Name = "SecIDSpinEdit"
        Me.SecIDSpinEdit.Properties.Appearance.Font = CType(resources.GetObject("SecIDSpinEdit.Properties.Appearance.Font"), System.Drawing.Font)
        Me.SecIDSpinEdit.Properties.Appearance.Options.UseFont = True
        Me.SecIDSpinEdit.Properties.Appearance.Options.UseTextOptions = True
        Me.SecIDSpinEdit.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.SecIDSpinEdit.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        '
        'SecNameLabel
        '
        resources.ApplyResources(Me.SecNameLabel, "SecNameLabel")
        Me.SecNameLabel.Appearance.Font = CType(resources.GetObject("SecNameLabel.Appearance.Font"), System.Drawing.Font)
        Me.SecNameLabel.Appearance.Options.UseFont = True
        Me.SecNameLabel.Appearance.Options.UseTextOptions = True
        Me.SecNameLabel.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.SecNameLabel.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.SecNameLabel.Name = "SecNameLabel"
        '
        'SecNameTextEdit
        '
        resources.ApplyResources(Me.SecNameTextEdit, "SecNameTextEdit")
        Me.SecNameTextEdit.Name = "SecNameTextEdit"
        Me.SecNameTextEdit.Properties.Appearance.Font = CType(resources.GetObject("SecNameTextEdit.Properties.Appearance.Font"), System.Drawing.Font)
        Me.SecNameTextEdit.Properties.Appearance.Options.UseFont = True
        Me.SecNameTextEdit.Properties.Appearance.Options.UseTextOptions = True
        Me.SecNameTextEdit.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.SecNameTextEdit.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        '
        'SecColorLabel
        '
        resources.ApplyResources(Me.SecColorLabel, "SecColorLabel")
        Me.SecColorLabel.Appearance.Font = CType(resources.GetObject("SecColorLabel.Appearance.Font"), System.Drawing.Font)
        Me.SecColorLabel.Appearance.Options.UseFont = True
        Me.SecColorLabel.Appearance.Options.UseTextOptions = True
        Me.SecColorLabel.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.SecColorLabel.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.SecColorLabel.Name = "SecColorLabel"
        '
        'SecAdresLabel
        '
        resources.ApplyResources(Me.SecAdresLabel, "SecAdresLabel")
        Me.SecAdresLabel.Appearance.Font = CType(resources.GetObject("SecAdresLabel.Appearance.Font"), System.Drawing.Font)
        Me.SecAdresLabel.Appearance.Options.UseFont = True
        Me.SecAdresLabel.Appearance.Options.UseTextOptions = True
        Me.SecAdresLabel.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.SecAdresLabel.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.SecAdresLabel.Name = "SecAdresLabel"
        '
        'SecColorTextEdit
        '
        resources.ApplyResources(Me.SecColorTextEdit, "SecColorTextEdit")
        Me.SecColorTextEdit.Name = "SecColorTextEdit"
        Me.SecColorTextEdit.Properties.Appearance.Font = CType(resources.GetObject("SecColorTextEdit.Properties.Appearance.Font"), System.Drawing.Font)
        Me.SecColorTextEdit.Properties.Appearance.Options.UseFont = True
        Me.SecColorTextEdit.Properties.Appearance.Options.UseTextOptions = True
        Me.SecColorTextEdit.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.SecColorTextEdit.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        '
        'SecAdresTextEdit
        '
        resources.ApplyResources(Me.SecAdresTextEdit, "SecAdresTextEdit")
        Me.SecAdresTextEdit.Name = "SecAdresTextEdit"
        Me.SecAdresTextEdit.Properties.Appearance.Font = CType(resources.GetObject("SecAdresTextEdit.Properties.Appearance.Font"), System.Drawing.Font)
        Me.SecAdresTextEdit.Properties.Appearance.Options.UseFont = True
        Me.SecAdresTextEdit.Properties.Appearance.Options.UseTextOptions = True
        Me.SecAdresTextEdit.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.SecAdresTextEdit.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        '
        'SecMobileTextEdit
        '
        resources.ApplyResources(Me.SecMobileTextEdit, "SecMobileTextEdit")
        Me.SecMobileTextEdit.Name = "SecMobileTextEdit"
        Me.SecMobileTextEdit.Properties.Appearance.Font = CType(resources.GetObject("SecMobileTextEdit.Properties.Appearance.Font"), System.Drawing.Font)
        Me.SecMobileTextEdit.Properties.Appearance.Options.UseFont = True
        Me.SecMobileTextEdit.Properties.Appearance.Options.UseTextOptions = True
        Me.SecMobileTextEdit.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.SecMobileTextEdit.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        '
        'SecPhoneLabel
        '
        resources.ApplyResources(Me.SecPhoneLabel, "SecPhoneLabel")
        Me.SecPhoneLabel.Appearance.Font = CType(resources.GetObject("SecPhoneLabel.Appearance.Font"), System.Drawing.Font)
        Me.SecPhoneLabel.Appearance.Options.UseFont = True
        Me.SecPhoneLabel.Appearance.Options.UseTextOptions = True
        Me.SecPhoneLabel.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.SecPhoneLabel.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.SecPhoneLabel.Name = "SecPhoneLabel"
        '
        'SecMobileLabel
        '
        resources.ApplyResources(Me.SecMobileLabel, "SecMobileLabel")
        Me.SecMobileLabel.Appearance.Font = CType(resources.GetObject("SecMobileLabel.Appearance.Font"), System.Drawing.Font)
        Me.SecMobileLabel.Appearance.Options.UseFont = True
        Me.SecMobileLabel.Appearance.Options.UseTextOptions = True
        Me.SecMobileLabel.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.SecMobileLabel.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.SecMobileLabel.Name = "SecMobileLabel"
        '
        'SecPhoneTextEdit
        '
        resources.ApplyResources(Me.SecPhoneTextEdit, "SecPhoneTextEdit")
        Me.SecPhoneTextEdit.Name = "SecPhoneTextEdit"
        Me.SecPhoneTextEdit.Properties.Appearance.Font = CType(resources.GetObject("SecPhoneTextEdit.Properties.Appearance.Font"), System.Drawing.Font)
        Me.SecPhoneTextEdit.Properties.Appearance.Options.UseFont = True
        Me.SecPhoneTextEdit.Properties.Appearance.Options.UseTextOptions = True
        Me.SecPhoneTextEdit.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.SecPhoneTextEdit.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        '
        'SecWhatsPrefixLabel
        '
        resources.ApplyResources(Me.SecWhatsPrefixLabel, "SecWhatsPrefixLabel")
        Me.SecWhatsPrefixLabel.Appearance.Font = CType(resources.GetObject("SecWhatsPrefixLabel.Appearance.Font"), System.Drawing.Font)
        Me.SecWhatsPrefixLabel.Appearance.Options.UseFont = True
        Me.SecWhatsPrefixLabel.Name = "SecWhatsPrefixLabel"
        '
        'SecWhatsPrefixCombo
        '
        resources.ApplyResources(Me.SecWhatsPrefixCombo, "SecWhatsPrefixCombo")
        Me.SecWhatsPrefixCombo.Name = "SecWhatsPrefixCombo"
        Me.SecWhatsPrefixCombo.Properties.Appearance.Font = CType(resources.GetObject("SecWhatsPrefixCombo.Properties.Appearance.Font"), System.Drawing.Font)
        Me.SecWhatsPrefixCombo.Properties.Appearance.Options.UseFont = True
        Me.SecWhatsPrefixCombo.Properties.AppearanceDropDown.Font = CType(resources.GetObject("SecWhatsPrefixCombo.Properties.AppearanceDropDown.Font"), System.Drawing.Font)
        Me.SecWhatsPrefixCombo.Properties.AppearanceDropDown.Options.UseFont = True
        Me.SecWhatsPrefixCombo.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("SecWhatsPrefixCombo.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        '
        'SecWhatsLabel
        '
        resources.ApplyResources(Me.SecWhatsLabel, "SecWhatsLabel")
        Me.SecWhatsLabel.Appearance.Font = CType(resources.GetObject("SecWhatsLabel.Appearance.Font"), System.Drawing.Font)
        Me.SecWhatsLabel.Appearance.Options.UseFont = True
        Me.SecWhatsLabel.Name = "SecWhatsLabel"
        '
        'SecWhatsTextEdit
        '
        resources.ApplyResources(Me.SecWhatsTextEdit, "SecWhatsTextEdit")
        Me.SecWhatsTextEdit.Name = "SecWhatsTextEdit"
        Me.SecWhatsTextEdit.Properties.Appearance.Font = CType(resources.GetObject("SecWhatsTextEdit.Properties.Appearance.Font"), System.Drawing.Font)
        Me.SecWhatsTextEdit.Properties.Appearance.Options.UseFont = True
        Me.SecWhatsTextEdit.Properties.Appearance.Options.UseTextOptions = True
        Me.SecWhatsTextEdit.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.SecWhatsTextEdit.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.SecWhatsTextEdit.Properties.MaxLength = 10
        '
        'lblSecWhats
        '
        resources.ApplyResources(Me.lblSecWhats, "lblSecWhats")
        Me.lblSecWhats.Appearance.Font = CType(resources.GetObject("lblSecWhats.Appearance.Font"), System.Drawing.Font)
        Me.lblSecWhats.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.lblSecWhats.Appearance.Options.UseFont = True
        Me.lblSecWhats.Appearance.Options.UseForeColor = True
        Me.lblSecWhats.Name = "lblSecWhats"
        '
        'FrmSecretaries
        '
        resources.ApplyResources(Me, "$this")
        Me.Appearance.Options.UseFont = True
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.Splitter1)
        Me.Name = "FrmSecretaries"
        CType(Me.Splitter1.Panel1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Splitter1.Panel1.ResumeLayout(False)
        CType(Me.Splitter1.Panel2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Splitter1.Panel2.ResumeLayout(False)
        Me.Splitter1.Panel2.PerformLayout()
        CType(Me.Splitter1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Splitter1.ResumeLayout(False)
        CType(Me.DGV, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgView, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ColorMark.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SecIDSpinEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SecNameTextEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SecColorTextEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SecAdresTextEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SecMobileTextEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SecPhoneTextEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SecWhatsPrefixCombo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SecWhatsTextEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private WithEvents DGV As DevExpress.XtraGrid.GridControl
		Private WithEvents dgView As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents colRowNum As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents Splitter1 As DevExpress.XtraEditors.SplitContainerControl
    Friend WithEvents SecIDSpinEdit As DevExpress.XtraEditors.SpinEdit
    Friend WithEvents SecIDLabel As DevExpress.XtraEditors.LabelControl
    Friend WithEvents colSecID As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents SecNameTextEdit As DevExpress.XtraEditors.TextEdit
    Friend WithEvents SecNameLabel As DevExpress.XtraEditors.LabelControl
    Friend WithEvents colSecName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents SecAdresTextEdit As DevExpress.XtraEditors.TextEdit
    Friend WithEvents SecAdresLabel As DevExpress.XtraEditors.LabelControl
    Friend WithEvents colSecAdres As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents SecPhoneTextEdit As DevExpress.XtraEditors.TextEdit
    Friend WithEvents SecPhoneLabel As DevExpress.XtraEditors.LabelControl
    Friend WithEvents colSecPhone As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents SecMobileTextEdit As DevExpress.XtraEditors.TextEdit
    Friend WithEvents SecMobileLabel As DevExpress.XtraEditors.LabelControl
    Friend WithEvents colSecMobile As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colWhatsAppPrefix As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colWhatsApp As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents SecWhatsPrefixLabel As DevExpress.XtraEditors.LabelControl
    Friend WithEvents SecWhatsPrefixCombo As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents SecWhatsLabel As DevExpress.XtraEditors.LabelControl
    Friend WithEvents SecWhatsTextEdit As DevExpress.XtraEditors.TextEdit
    Friend WithEvents lblSecWhats As DevExpress.XtraEditors.LabelControl
    Friend WithEvents SecColorTextEdit As DevExpress.XtraEditors.TextEdit
    Friend WithEvents SecColorLabel As DevExpress.XtraEditors.LabelControl
    Friend WithEvents ColorMark As DevExpress.XtraEditors.ColorPickEdit
    Friend WithEvents btnAdd As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnEdit As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnDel As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents butClose As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents butCancel As DevExpress.XtraEditors.SimpleButton
End Class
