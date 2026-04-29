<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmDoctors
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmDoctors))
        Me.Splitter1 = New DevExpress.XtraEditors.SplitContainerControl()
        Me.DGV = New DevExpress.XtraGrid.GridControl()
        Me.dgView = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.colRowNum = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colDrID = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colDrName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colDrAdres = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colDrphone = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colDrMobile = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colWhatsAppPrefix = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colWhatsApp = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.lblWhats = New DevExpress.XtraEditors.LabelControl()
        Me.butCancel = New DevExpress.XtraEditors.SimpleButton()
        Me.btnAdd = New DevExpress.XtraEditors.SimpleButton()
        Me.ColorMark = New DevExpress.XtraEditors.ColorPickEdit()
        Me.btnEdit = New DevExpress.XtraEditors.SimpleButton()
        Me.btnDel = New DevExpress.XtraEditors.SimpleButton()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.butClose = New DevExpress.XtraEditors.SimpleButton()
        Me.lblMark = New DevExpress.XtraEditors.LabelControl()
        Me.DrIDSpinEdit = New DevExpress.XtraEditors.SpinEdit()
        Me.DrIDLabel = New DevExpress.XtraEditors.LabelControl()
        Me.DrNameTextEdit = New DevExpress.XtraEditors.TextEdit()
        Me.DrNameLabel = New DevExpress.XtraEditors.LabelControl()
        Me.DrMobileTextEdit = New DevExpress.XtraEditors.TextEdit()
        Me.DrMobileLabel = New DevExpress.XtraEditors.LabelControl()
        Me.DrAdresLabel = New DevExpress.XtraEditors.LabelControl()
        Me.DrphoneTextEdit = New DevExpress.XtraEditors.TextEdit()
        Me.DrAdresTextEdit = New DevExpress.XtraEditors.TextEdit()
        Me.DrphoneLabel = New DevExpress.XtraEditors.LabelControl()
        Me.DrWhatsPrefixLabel = New DevExpress.XtraEditors.LabelControl()
        Me.DrWhatsPrefixCombo = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.DrWhatsLabel = New DevExpress.XtraEditors.LabelControl()
        Me.DrWhatsTextEdit = New DevExpress.XtraEditors.TextEdit()
        CType(Me.Splitter1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Splitter1.Panel1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Splitter1.Panel1.SuspendLayout()
        CType(Me.Splitter1.Panel2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Splitter1.Panel2.SuspendLayout()
        Me.Splitter1.SuspendLayout()
        CType(Me.DGV, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgView, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ColorMark.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DrIDSpinEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DrNameTextEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DrMobileTextEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DrphoneTextEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DrAdresTextEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DrWhatsPrefixCombo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DrWhatsTextEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.Splitter1.Panel2.Controls.Add(Me.lblWhats)
        Me.Splitter1.Panel2.Controls.Add(Me.butCancel)
        Me.Splitter1.Panel2.Controls.Add(Me.btnAdd)
        Me.Splitter1.Panel2.Controls.Add(Me.ColorMark)
        Me.Splitter1.Panel2.Controls.Add(Me.btnEdit)
        Me.Splitter1.Panel2.Controls.Add(Me.btnDel)
        Me.Splitter1.Panel2.Controls.Add(Me.LabelControl1)
        Me.Splitter1.Panel2.Controls.Add(Me.butClose)
        Me.Splitter1.Panel2.Controls.Add(Me.lblMark)
        Me.Splitter1.Panel2.Controls.Add(Me.DrIDSpinEdit)
        Me.Splitter1.Panel2.Controls.Add(Me.DrIDLabel)
        Me.Splitter1.Panel2.Controls.Add(Me.DrNameTextEdit)
        Me.Splitter1.Panel2.Controls.Add(Me.DrNameLabel)
        Me.Splitter1.Panel2.Controls.Add(Me.DrMobileTextEdit)
        Me.Splitter1.Panel2.Controls.Add(Me.DrMobileLabel)
        Me.Splitter1.Panel2.Controls.Add(Me.DrAdresLabel)
        Me.Splitter1.Panel2.Controls.Add(Me.DrphoneTextEdit)
        Me.Splitter1.Panel2.Controls.Add(Me.DrAdresTextEdit)
        Me.Splitter1.Panel2.Controls.Add(Me.DrphoneLabel)
        Me.Splitter1.Panel2.Controls.Add(Me.DrWhatsPrefixLabel)
        Me.Splitter1.Panel2.Controls.Add(Me.DrWhatsPrefixCombo)
        Me.Splitter1.Panel2.Controls.Add(Me.DrWhatsLabel)
        Me.Splitter1.Panel2.Controls.Add(Me.DrWhatsTextEdit)
        Me.Splitter1.SplitterPosition = 412
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
        Me.dgView.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.colRowNum, Me.colDrID, Me.colDrName, Me.colDrAdres, Me.colDrphone, Me.colDrMobile, Me.colWhatsAppPrefix, Me.colWhatsApp})
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
        'colDrID
        '
        resources.ApplyResources(Me.colDrID, "colDrID")
        Me.colDrID.FieldName = "DrID"
        Me.colDrID.ImageOptions.ImageKey = resources.GetString("colDrID.ImageOptions.ImageKey")
        Me.colDrID.Name = "colDrID"
        '
        'colDrName
        '
        resources.ApplyResources(Me.colDrName, "colDrName")
        Me.colDrName.FieldName = "DrName"
        Me.colDrName.ImageOptions.ImageKey = resources.GetString("colDrName.ImageOptions.ImageKey")
        Me.colDrName.Name = "colDrName"
        '
        'colDrAdres
        '
        resources.ApplyResources(Me.colDrAdres, "colDrAdres")
        Me.colDrAdres.FieldName = "DrAdres"
        Me.colDrAdres.ImageOptions.ImageKey = resources.GetString("colDrAdres.ImageOptions.ImageKey")
        Me.colDrAdres.Name = "colDrAdres"
        '
        'colDrphone
        '
        resources.ApplyResources(Me.colDrphone, "colDrphone")
        Me.colDrphone.FieldName = "Drphone"
        Me.colDrphone.ImageOptions.ImageKey = resources.GetString("colDrphone.ImageOptions.ImageKey")
        Me.colDrphone.Name = "colDrphone"
        '
        'colDrMobile
        '
        resources.ApplyResources(Me.colDrMobile, "colDrMobile")
        Me.colDrMobile.FieldName = "DrMobile"
        Me.colDrMobile.ImageOptions.ImageKey = resources.GetString("colDrMobile.ImageOptions.ImageKey")
        Me.colDrMobile.Name = "colDrMobile"
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
        'lblWhats
        '
        resources.ApplyResources(Me.lblWhats, "lblWhats")
        Me.lblWhats.Appearance.Font = CType(resources.GetObject("lblWhats.Appearance.Font"), System.Drawing.Font)
        Me.lblWhats.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.lblWhats.Appearance.Options.UseFont = True
        Me.lblWhats.Appearance.Options.UseForeColor = True
        Me.lblWhats.Name = "lblWhats"
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
        'ColorMark
        '
        resources.ApplyResources(Me.ColorMark, "ColorMark")
        Me.ColorMark.Name = "ColorMark"
        Me.ColorMark.Properties.AutomaticColor = System.Drawing.Color.Black
        Me.ColorMark.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("ColorMark.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me.ColorMark.Properties.ShowWebSafeColors = True
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
        'LabelControl1
        '
        resources.ApplyResources(Me.LabelControl1, "LabelControl1")
        Me.LabelControl1.Appearance.Font = CType(resources.GetObject("LabelControl1.Appearance.Font"), System.Drawing.Font)
        Me.LabelControl1.Appearance.Options.UseFont = True
        Me.LabelControl1.Appearance.Options.UseTextOptions = True
        Me.LabelControl1.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.LabelControl1.Name = "LabelControl1"
        '
        'butClose
        '
        resources.ApplyResources(Me.butClose, "butClose")
        Me.butClose.Appearance.Font = CType(resources.GetObject("butClose.Appearance.Font"), System.Drawing.Font)
        Me.butClose.Appearance.Options.UseFont = True
        Me.butClose.ImageOptions.ImageKey = resources.GetString("butClose.ImageOptions.ImageKey")
        Me.butClose.Name = "butClose"
        '
        'lblMark
        '
        resources.ApplyResources(Me.lblMark, "lblMark")
        Me.lblMark.Appearance.Font = CType(resources.GetObject("lblMark.Appearance.Font"), System.Drawing.Font)
        Me.lblMark.Appearance.Options.UseFont = True
        Me.lblMark.Appearance.Options.UseTextOptions = True
        Me.lblMark.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.lblMark.Name = "lblMark"
        '
        'DrIDSpinEdit
        '
        resources.ApplyResources(Me.DrIDSpinEdit, "DrIDSpinEdit")
        Me.DrIDSpinEdit.Name = "DrIDSpinEdit"
        Me.DrIDSpinEdit.Properties.Appearance.Font = CType(resources.GetObject("DrIDSpinEdit.Properties.Appearance.Font"), System.Drawing.Font)
        Me.DrIDSpinEdit.Properties.Appearance.Options.UseFont = True
        Me.DrIDSpinEdit.Properties.Appearance.Options.UseTextOptions = True
        Me.DrIDSpinEdit.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.DrIDSpinEdit.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        '
        'DrIDLabel
        '
        resources.ApplyResources(Me.DrIDLabel, "DrIDLabel")
        Me.DrIDLabel.Appearance.Font = CType(resources.GetObject("DrIDLabel.Appearance.Font"), System.Drawing.Font)
        Me.DrIDLabel.Appearance.Options.UseFont = True
        Me.DrIDLabel.Appearance.Options.UseTextOptions = True
        Me.DrIDLabel.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.DrIDLabel.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.DrIDLabel.Name = "DrIDLabel"
        '
        'DrNameTextEdit
        '
        resources.ApplyResources(Me.DrNameTextEdit, "DrNameTextEdit")
        Me.DrNameTextEdit.Name = "DrNameTextEdit"
        Me.DrNameTextEdit.Properties.Appearance.Font = CType(resources.GetObject("DrNameTextEdit.Properties.Appearance.Font"), System.Drawing.Font)
        Me.DrNameTextEdit.Properties.Appearance.Options.UseFont = True
        Me.DrNameTextEdit.Properties.Appearance.Options.UseTextOptions = True
        Me.DrNameTextEdit.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.DrNameTextEdit.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        '
        'DrNameLabel
        '
        resources.ApplyResources(Me.DrNameLabel, "DrNameLabel")
        Me.DrNameLabel.Appearance.Font = CType(resources.GetObject("DrNameLabel.Appearance.Font"), System.Drawing.Font)
        Me.DrNameLabel.Appearance.Options.UseFont = True
        Me.DrNameLabel.Appearance.Options.UseTextOptions = True
        Me.DrNameLabel.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.DrNameLabel.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.DrNameLabel.Name = "DrNameLabel"
        '
        'DrMobileTextEdit
        '
        resources.ApplyResources(Me.DrMobileTextEdit, "DrMobileTextEdit")
        Me.DrMobileTextEdit.Name = "DrMobileTextEdit"
        Me.DrMobileTextEdit.Properties.Appearance.Font = CType(resources.GetObject("DrMobileTextEdit.Properties.Appearance.Font"), System.Drawing.Font)
        Me.DrMobileTextEdit.Properties.Appearance.Options.UseFont = True
        Me.DrMobileTextEdit.Properties.Appearance.Options.UseTextOptions = True
        Me.DrMobileTextEdit.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.DrMobileTextEdit.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        '
        'DrMobileLabel
        '
        resources.ApplyResources(Me.DrMobileLabel, "DrMobileLabel")
        Me.DrMobileLabel.Appearance.Font = CType(resources.GetObject("DrMobileLabel.Appearance.Font"), System.Drawing.Font)
        Me.DrMobileLabel.Appearance.Options.UseFont = True
        Me.DrMobileLabel.Appearance.Options.UseTextOptions = True
        Me.DrMobileLabel.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.DrMobileLabel.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.DrMobileLabel.Name = "DrMobileLabel"
        '
        'DrAdresLabel
        '
        resources.ApplyResources(Me.DrAdresLabel, "DrAdresLabel")
        Me.DrAdresLabel.Appearance.Font = CType(resources.GetObject("DrAdresLabel.Appearance.Font"), System.Drawing.Font)
        Me.DrAdresLabel.Appearance.Options.UseFont = True
        Me.DrAdresLabel.Appearance.Options.UseTextOptions = True
        Me.DrAdresLabel.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.DrAdresLabel.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.DrAdresLabel.Name = "DrAdresLabel"
        '
        'DrphoneTextEdit
        '
        resources.ApplyResources(Me.DrphoneTextEdit, "DrphoneTextEdit")
        Me.DrphoneTextEdit.Name = "DrphoneTextEdit"
        Me.DrphoneTextEdit.Properties.Appearance.Font = CType(resources.GetObject("DrphoneTextEdit.Properties.Appearance.Font"), System.Drawing.Font)
        Me.DrphoneTextEdit.Properties.Appearance.Options.UseFont = True
        Me.DrphoneTextEdit.Properties.Appearance.Options.UseTextOptions = True
        Me.DrphoneTextEdit.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.DrphoneTextEdit.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        '
        'DrAdresTextEdit
        '
        resources.ApplyResources(Me.DrAdresTextEdit, "DrAdresTextEdit")
        Me.DrAdresTextEdit.Name = "DrAdresTextEdit"
        Me.DrAdresTextEdit.Properties.Appearance.Font = CType(resources.GetObject("DrAdresTextEdit.Properties.Appearance.Font"), System.Drawing.Font)
        Me.DrAdresTextEdit.Properties.Appearance.Options.UseFont = True
        Me.DrAdresTextEdit.Properties.Appearance.Options.UseTextOptions = True
        Me.DrAdresTextEdit.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.DrAdresTextEdit.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        '
        'DrphoneLabel
        '
        resources.ApplyResources(Me.DrphoneLabel, "DrphoneLabel")
        Me.DrphoneLabel.Appearance.Font = CType(resources.GetObject("DrphoneLabel.Appearance.Font"), System.Drawing.Font)
        Me.DrphoneLabel.Appearance.Options.UseFont = True
        Me.DrphoneLabel.Appearance.Options.UseTextOptions = True
        Me.DrphoneLabel.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.DrphoneLabel.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.DrphoneLabel.Name = "DrphoneLabel"
        '
        'DrWhatsPrefixLabel
        '
        resources.ApplyResources(Me.DrWhatsPrefixLabel, "DrWhatsPrefixLabel")
        Me.DrWhatsPrefixLabel.Appearance.Font = CType(resources.GetObject("DrWhatsPrefixLabel.Appearance.Font"), System.Drawing.Font)
        Me.DrWhatsPrefixLabel.Appearance.Options.UseFont = True
        Me.DrWhatsPrefixLabel.Appearance.Options.UseTextOptions = True
        Me.DrWhatsPrefixLabel.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.DrWhatsPrefixLabel.Name = "DrWhatsPrefixLabel"
        '
        'DrWhatsPrefixCombo
        '
        resources.ApplyResources(Me.DrWhatsPrefixCombo, "DrWhatsPrefixCombo")
        Me.DrWhatsPrefixCombo.Name = "DrWhatsPrefixCombo"
        Me.DrWhatsPrefixCombo.Properties.Appearance.Font = CType(resources.GetObject("DrWhatsPrefixCombo.Properties.Appearance.Font"), System.Drawing.Font)
        Me.DrWhatsPrefixCombo.Properties.Appearance.Options.UseFont = True
        Me.DrWhatsPrefixCombo.Properties.AppearanceDropDown.Font = CType(resources.GetObject("DrWhatsPrefixCombo.Properties.AppearanceDropDown.Font"), System.Drawing.Font)
        Me.DrWhatsPrefixCombo.Properties.AppearanceDropDown.Options.UseFont = True
        Me.DrWhatsPrefixCombo.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("DrWhatsPrefixCombo.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        '
        'DrWhatsLabel
        '
        resources.ApplyResources(Me.DrWhatsLabel, "DrWhatsLabel")
        Me.DrWhatsLabel.Appearance.Font = CType(resources.GetObject("DrWhatsLabel.Appearance.Font"), System.Drawing.Font)
        Me.DrWhatsLabel.Appearance.Options.UseFont = True
        Me.DrWhatsLabel.Appearance.Options.UseTextOptions = True
        Me.DrWhatsLabel.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.DrWhatsLabel.Name = "DrWhatsLabel"
        '
        'DrWhatsTextEdit
        '
        resources.ApplyResources(Me.DrWhatsTextEdit, "DrWhatsTextEdit")
        Me.DrWhatsTextEdit.Name = "DrWhatsTextEdit"
        Me.DrWhatsTextEdit.Properties.Appearance.Font = CType(resources.GetObject("DrWhatsTextEdit.Properties.Appearance.Font"), System.Drawing.Font)
        Me.DrWhatsTextEdit.Properties.Appearance.Options.UseFont = True
        Me.DrWhatsTextEdit.Properties.Appearance.Options.UseTextOptions = True
        Me.DrWhatsTextEdit.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.DrWhatsTextEdit.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.DrWhatsTextEdit.Properties.MaxLength = 10
        '
        'FrmDoctors
        '
        resources.ApplyResources(Me, "$this")
        Me.Appearance.Options.UseFont = True
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.Splitter1)
        Me.Name = "FrmDoctors"
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
        CType(Me.DrIDSpinEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DrNameTextEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DrMobileTextEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DrphoneTextEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DrAdresTextEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DrWhatsPrefixCombo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DrWhatsTextEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private WithEvents DGV As DevExpress.XtraGrid.GridControl
		Private WithEvents dgView As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents colRowNum As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents Splitter1 As DevExpress.XtraEditors.SplitContainerControl
    Friend WithEvents butClose As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents DrIDSpinEdit As DevExpress.XtraEditors.SpinEdit
		Friend WithEvents DrIDLabel As DevExpress.XtraEditors.LabelControl
		Friend WithEvents colDrID As DevExpress.XtraGrid.Columns.GridColumn
		Friend WithEvents DrNameTextEdit As DevExpress.XtraEditors.TextEdit
		Friend WithEvents DrNameLabel As DevExpress.XtraEditors.LabelControl
		Friend WithEvents colDrName As DevExpress.XtraGrid.Columns.GridColumn
		Friend WithEvents DrAdresTextEdit As DevExpress.XtraEditors.TextEdit
		Friend WithEvents DrAdresLabel As DevExpress.XtraEditors.LabelControl
		Friend WithEvents colDrAdres As DevExpress.XtraGrid.Columns.GridColumn
		Friend WithEvents DrphoneTextEdit As DevExpress.XtraEditors.TextEdit
		Friend WithEvents DrphoneLabel As DevExpress.XtraEditors.LabelControl
		Friend WithEvents colDrphone As DevExpress.XtraGrid.Columns.GridColumn
		Friend WithEvents DrMobileTextEdit As DevExpress.XtraEditors.TextEdit
		Friend WithEvents DrMobileLabel As DevExpress.XtraEditors.LabelControl
		Friend WithEvents colDrMobile As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colWhatsAppPrefix As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colWhatsApp As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents DrWhatsPrefixLabel As DevExpress.XtraEditors.LabelControl
    Friend WithEvents DrWhatsPrefixCombo As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents DrWhatsLabel As DevExpress.XtraEditors.LabelControl
    Friend WithEvents DrWhatsTextEdit As DevExpress.XtraEditors.TextEdit
    Friend WithEvents lblMark As DevExpress.XtraEditors.LabelControl
    Friend WithEvents ColorMark As DevExpress.XtraEditors.ColorPickEdit
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents btnAdd As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnEdit As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnDel As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents butCancel As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents lblWhats As DevExpress.XtraEditors.LabelControl
End Class
