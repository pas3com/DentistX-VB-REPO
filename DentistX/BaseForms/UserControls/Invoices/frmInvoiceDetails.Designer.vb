<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmInvoiceDetails
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmInvoiceDetails))
        Me.GroupControl1 = New DevExpress.XtraEditors.GroupControl()
        Me.lblNotes = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl6 = New DevExpress.XtraEditors.LabelControl()
        Me.lblEmail = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl5 = New DevExpress.XtraEditors.LabelControl()
        Me.lblPhone = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl7 = New DevExpress.XtraEditors.LabelControl()
        Me.lblAddress = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl4 = New DevExpress.XtraEditors.LabelControl()
        Me.lblPatientName = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl8 = New DevExpress.XtraEditors.LabelControl()
        Me.lblStatus = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl3 = New DevExpress.XtraEditors.LabelControl()
        Me.lblDueDate = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        Me.lblInvoiceDate = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.lblInvoiceNumber = New DevExpress.XtraEditors.LabelControl()
        Me.txtNotes = New DevExpress.XtraEditors.MemoEdit()
        Me.GroupControl2 = New DevExpress.XtraEditors.GroupControl()
        Me.GroupControl4 = New DevExpress.XtraEditors.GroupControl()
        Me.chkWithPay = New DevExpress.XtraEditors.CheckEdit()
        Me.btnExportPDF = New DevExpress.XtraEditors.SimpleButton()
        Me.btnViewInv = New DevExpress.XtraEditors.SimpleButton()
        Me.btnPrint = New DevExpress.XtraEditors.SimpleButton()
        Me.btnClose = New DevExpress.XtraEditors.SimpleButton()
        Me.btnAddPayment = New DevExpress.XtraEditors.SimpleButton()
        Me.LabelControl14 = New DevExpress.XtraEditors.LabelControl()
        Me.lblBalanceDue = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl13 = New DevExpress.XtraEditors.LabelControl()
        Me.lblAmountPaid = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl12 = New DevExpress.XtraEditors.LabelControl()
        Me.lblTotal = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl11 = New DevExpress.XtraEditors.LabelControl()
        Me.lblDiscount = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl10 = New DevExpress.XtraEditors.LabelControl()
        Me.lblTax = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl9 = New DevExpress.XtraEditors.LabelControl()
        Me.lblSubTotal = New DevExpress.XtraEditors.LabelControl()
        Me.GroupControl3 = New DevExpress.XtraEditors.GroupControl()
        Me.XtraTabControl1 = New DevExpress.XtraTab.XtraTabControl()
        Me.InvoiceDetailsPage = New DevExpress.XtraTab.XtraTabPage()
        Me.GridControl1 = New DevExpress.XtraGrid.GridControl()
        Me.GridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.InvoicePaymentPage = New DevExpress.XtraTab.XtraTabPage()
        Me.GridControl2 = New DevExpress.XtraGrid.GridControl()
        Me.GridView2 = New DevExpress.XtraGrid.Views.Grid.GridView()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl1.SuspendLayout()
        CType(Me.txtNotes.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl2.SuspendLayout()
        CType(Me.GroupControl4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl4.SuspendLayout()
        CType(Me.chkWithPay.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupControl3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl3.SuspendLayout()
        CType(Me.XtraTabControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.XtraTabControl1.SuspendLayout()
        Me.InvoiceDetailsPage.SuspendLayout()
        CType(Me.GridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.InvoicePaymentPage.SuspendLayout()
        CType(Me.GridControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GroupControl1
        '
        resources.ApplyResources(Me.GroupControl1, "GroupControl1")
        Me.GroupControl1.Controls.Add(Me.lblNotes)
        Me.GroupControl1.Controls.Add(Me.LabelControl6)
        Me.GroupControl1.Controls.Add(Me.lblEmail)
        Me.GroupControl1.Controls.Add(Me.LabelControl5)
        Me.GroupControl1.Controls.Add(Me.lblPhone)
        Me.GroupControl1.Controls.Add(Me.LabelControl7)
        Me.GroupControl1.Controls.Add(Me.lblAddress)
        Me.GroupControl1.Controls.Add(Me.LabelControl4)
        Me.GroupControl1.Controls.Add(Me.lblPatientName)
        Me.GroupControl1.Controls.Add(Me.LabelControl8)
        Me.GroupControl1.Controls.Add(Me.lblStatus)
        Me.GroupControl1.Controls.Add(Me.LabelControl3)
        Me.GroupControl1.Controls.Add(Me.lblDueDate)
        Me.GroupControl1.Controls.Add(Me.LabelControl2)
        Me.GroupControl1.Controls.Add(Me.lblInvoiceDate)
        Me.GroupControl1.Controls.Add(Me.LabelControl1)
        Me.GroupControl1.Controls.Add(Me.lblInvoiceNumber)
        Me.GroupControl1.Controls.Add(Me.txtNotes)
        Me.GroupControl1.Name = "GroupControl1"
        '
        'lblNotes
        '
        resources.ApplyResources(Me.lblNotes, "lblNotes")
        Me.lblNotes.Appearance.Font = CType(resources.GetObject("lblNotes.Appearance.Font"), System.Drawing.Font)
        Me.lblNotes.Appearance.Options.UseFont = True
        Me.lblNotes.Name = "lblNotes"
        '
        'LabelControl6
        '
        resources.ApplyResources(Me.LabelControl6, "LabelControl6")
        Me.LabelControl6.Appearance.Font = CType(resources.GetObject("LabelControl6.Appearance.Font"), System.Drawing.Font)
        Me.LabelControl6.Appearance.Options.UseFont = True
        Me.LabelControl6.Name = "LabelControl6"
        '
        'lblEmail
        '
        resources.ApplyResources(Me.lblEmail, "lblEmail")
        Me.lblEmail.Appearance.Font = CType(resources.GetObject("lblEmail.Appearance.Font"), System.Drawing.Font)
        Me.lblEmail.Appearance.Options.UseFont = True
        Me.lblEmail.Name = "lblEmail"
        '
        'LabelControl5
        '
        resources.ApplyResources(Me.LabelControl5, "LabelControl5")
        Me.LabelControl5.Appearance.Font = CType(resources.GetObject("LabelControl5.Appearance.Font"), System.Drawing.Font)
        Me.LabelControl5.Appearance.Options.UseFont = True
        Me.LabelControl5.Name = "LabelControl5"
        '
        'lblPhone
        '
        resources.ApplyResources(Me.lblPhone, "lblPhone")
        Me.lblPhone.Appearance.Font = CType(resources.GetObject("lblPhone.Appearance.Font"), System.Drawing.Font)
        Me.lblPhone.Appearance.Options.UseFont = True
        Me.lblPhone.Name = "lblPhone"
        '
        'LabelControl7
        '
        resources.ApplyResources(Me.LabelControl7, "LabelControl7")
        Me.LabelControl7.Appearance.Font = CType(resources.GetObject("LabelControl7.Appearance.Font"), System.Drawing.Font)
        Me.LabelControl7.Appearance.Options.UseFont = True
        Me.LabelControl7.Name = "LabelControl7"
        '
        'lblAddress
        '
        resources.ApplyResources(Me.lblAddress, "lblAddress")
        Me.lblAddress.Appearance.Font = CType(resources.GetObject("lblAddress.Appearance.Font"), System.Drawing.Font)
        Me.lblAddress.Appearance.Options.UseFont = True
        Me.lblAddress.Name = "lblAddress"
        '
        'LabelControl4
        '
        resources.ApplyResources(Me.LabelControl4, "LabelControl4")
        Me.LabelControl4.Appearance.Font = CType(resources.GetObject("LabelControl4.Appearance.Font"), System.Drawing.Font)
        Me.LabelControl4.Appearance.Options.UseFont = True
        Me.LabelControl4.Name = "LabelControl4"
        '
        'lblPatientName
        '
        resources.ApplyResources(Me.lblPatientName, "lblPatientName")
        Me.lblPatientName.Appearance.Font = CType(resources.GetObject("lblPatientName.Appearance.Font"), System.Drawing.Font)
        Me.lblPatientName.Appearance.Options.UseFont = True
        Me.lblPatientName.Name = "lblPatientName"
        '
        'LabelControl8
        '
        resources.ApplyResources(Me.LabelControl8, "LabelControl8")
        Me.LabelControl8.Appearance.Font = CType(resources.GetObject("LabelControl8.Appearance.Font"), System.Drawing.Font)
        Me.LabelControl8.Appearance.Options.UseFont = True
        Me.LabelControl8.Name = "LabelControl8"
        '
        'lblStatus
        '
        resources.ApplyResources(Me.lblStatus, "lblStatus")
        Me.lblStatus.Appearance.Font = CType(resources.GetObject("lblStatus.Appearance.Font"), System.Drawing.Font)
        Me.lblStatus.Appearance.Options.UseFont = True
        Me.lblStatus.Name = "lblStatus"
        '
        'LabelControl3
        '
        resources.ApplyResources(Me.LabelControl3, "LabelControl3")
        Me.LabelControl3.Appearance.Font = CType(resources.GetObject("LabelControl3.Appearance.Font"), System.Drawing.Font)
        Me.LabelControl3.Appearance.Options.UseFont = True
        Me.LabelControl3.Name = "LabelControl3"
        '
        'lblDueDate
        '
        resources.ApplyResources(Me.lblDueDate, "lblDueDate")
        Me.lblDueDate.Appearance.Font = CType(resources.GetObject("lblDueDate.Appearance.Font"), System.Drawing.Font)
        Me.lblDueDate.Appearance.Options.UseFont = True
        Me.lblDueDate.Name = "lblDueDate"
        '
        'LabelControl2
        '
        resources.ApplyResources(Me.LabelControl2, "LabelControl2")
        Me.LabelControl2.Appearance.Font = CType(resources.GetObject("LabelControl2.Appearance.Font"), System.Drawing.Font)
        Me.LabelControl2.Appearance.Options.UseFont = True
        Me.LabelControl2.Name = "LabelControl2"
        '
        'lblInvoiceDate
        '
        resources.ApplyResources(Me.lblInvoiceDate, "lblInvoiceDate")
        Me.lblInvoiceDate.Appearance.Font = CType(resources.GetObject("lblInvoiceDate.Appearance.Font"), System.Drawing.Font)
        Me.lblInvoiceDate.Appearance.Options.UseFont = True
        Me.lblInvoiceDate.Name = "lblInvoiceDate"
        '
        'LabelControl1
        '
        resources.ApplyResources(Me.LabelControl1, "LabelControl1")
        Me.LabelControl1.Appearance.Font = CType(resources.GetObject("LabelControl1.Appearance.Font"), System.Drawing.Font)
        Me.LabelControl1.Appearance.Options.UseFont = True
        Me.LabelControl1.Name = "LabelControl1"
        '
        'lblInvoiceNumber
        '
        resources.ApplyResources(Me.lblInvoiceNumber, "lblInvoiceNumber")
        Me.lblInvoiceNumber.Appearance.Font = CType(resources.GetObject("lblInvoiceNumber.Appearance.Font"), System.Drawing.Font)
        Me.lblInvoiceNumber.Appearance.Options.UseFont = True
        Me.lblInvoiceNumber.Name = "lblInvoiceNumber"
        '
        'txtNotes
        '
        resources.ApplyResources(Me.txtNotes, "txtNotes")
        Me.txtNotes.Name = "txtNotes"
        '
        'GroupControl2
        '
        resources.ApplyResources(Me.GroupControl2, "GroupControl2")
        Me.GroupControl2.Controls.Add(Me.GroupControl4)
        Me.GroupControl2.Controls.Add(Me.btnClose)
        Me.GroupControl2.Controls.Add(Me.btnAddPayment)
        Me.GroupControl2.Controls.Add(Me.LabelControl14)
        Me.GroupControl2.Controls.Add(Me.lblBalanceDue)
        Me.GroupControl2.Controls.Add(Me.LabelControl13)
        Me.GroupControl2.Controls.Add(Me.lblAmountPaid)
        Me.GroupControl2.Controls.Add(Me.LabelControl12)
        Me.GroupControl2.Controls.Add(Me.lblTotal)
        Me.GroupControl2.Controls.Add(Me.LabelControl11)
        Me.GroupControl2.Controls.Add(Me.lblDiscount)
        Me.GroupControl2.Controls.Add(Me.LabelControl10)
        Me.GroupControl2.Controls.Add(Me.lblTax)
        Me.GroupControl2.Controls.Add(Me.LabelControl9)
        Me.GroupControl2.Controls.Add(Me.lblSubTotal)
        Me.GroupControl2.Name = "GroupControl2"
        '
        'GroupControl4
        '
        resources.ApplyResources(Me.GroupControl4, "GroupControl4")
        Me.GroupControl4.AppearanceCaption.Font = CType(resources.GetObject("GroupControl4.AppearanceCaption.Font"), System.Drawing.Font)
        Me.GroupControl4.AppearanceCaption.Options.UseFont = True
        Me.GroupControl4.Controls.Add(Me.chkWithPay)
        Me.GroupControl4.Controls.Add(Me.btnExportPDF)
        Me.GroupControl4.Controls.Add(Me.btnViewInv)
        Me.GroupControl4.Controls.Add(Me.btnPrint)
        Me.GroupControl4.Name = "GroupControl4"
        '
        'chkWithPay
        '
        resources.ApplyResources(Me.chkWithPay, "chkWithPay")
        Me.chkWithPay.Name = "chkWithPay"
        Me.chkWithPay.Properties.Appearance.Font = CType(resources.GetObject("chkWithPay.Properties.Appearance.Font"), System.Drawing.Font)
        Me.chkWithPay.Properties.Appearance.Options.UseFont = True
        Me.chkWithPay.Properties.Caption = resources.GetString("chkWithPay.Properties.Caption")
        Me.chkWithPay.Properties.DisplayValueChecked = resources.GetString("chkWithPay.Properties.DisplayValueChecked")
        Me.chkWithPay.Properties.DisplayValueGrayed = resources.GetString("chkWithPay.Properties.DisplayValueGrayed")
        Me.chkWithPay.Properties.DisplayValueUnchecked = resources.GetString("chkWithPay.Properties.DisplayValueUnchecked")
        Me.chkWithPay.Properties.GlyphVerticalAlignment = CType(resources.GetObject("chkWithPay.Properties.GlyphVerticalAlignment"), DevExpress.Utils.VertAlignment)
        '
        'btnExportPDF
        '
        resources.ApplyResources(Me.btnExportPDF, "btnExportPDF")
        Me.btnExportPDF.Appearance.Font = CType(resources.GetObject("btnExportPDF.Appearance.Font"), System.Drawing.Font)
        Me.btnExportPDF.Appearance.Options.UseFont = True
        Me.btnExportPDF.ImageOptions.ImageKey = resources.GetString("btnExportPDF.ImageOptions.ImageKey")
        Me.btnExportPDF.Name = "btnExportPDF"
        '
        'btnViewInv
        '
        resources.ApplyResources(Me.btnViewInv, "btnViewInv")
        Me.btnViewInv.Appearance.Font = CType(resources.GetObject("btnViewInv.Appearance.Font"), System.Drawing.Font)
        Me.btnViewInv.Appearance.Options.UseFont = True
        Me.btnViewInv.ImageOptions.ImageKey = resources.GetString("btnViewInv.ImageOptions.ImageKey")
        Me.btnViewInv.Name = "btnViewInv"
        '
        'btnPrint
        '
        resources.ApplyResources(Me.btnPrint, "btnPrint")
        Me.btnPrint.Appearance.Font = CType(resources.GetObject("btnPrint.Appearance.Font"), System.Drawing.Font)
        Me.btnPrint.Appearance.Options.UseFont = True
        Me.btnPrint.ImageOptions.ImageKey = resources.GetString("btnPrint.ImageOptions.ImageKey")
        Me.btnPrint.Name = "btnPrint"
        '
        'btnClose
        '
        resources.ApplyResources(Me.btnClose, "btnClose")
        Me.btnClose.Appearance.Font = CType(resources.GetObject("btnClose.Appearance.Font"), System.Drawing.Font)
        Me.btnClose.Appearance.Options.UseFont = True
        Me.btnClose.ImageOptions.ImageKey = resources.GetString("btnClose.ImageOptions.ImageKey")
        Me.btnClose.Name = "btnClose"
        '
        'btnAddPayment
        '
        resources.ApplyResources(Me.btnAddPayment, "btnAddPayment")
        Me.btnAddPayment.Appearance.Font = CType(resources.GetObject("btnAddPayment.Appearance.Font"), System.Drawing.Font)
        Me.btnAddPayment.Appearance.Options.UseFont = True
        Me.btnAddPayment.ImageOptions.ImageKey = resources.GetString("btnAddPayment.ImageOptions.ImageKey")
        Me.btnAddPayment.Name = "btnAddPayment"
        '
        'LabelControl14
        '
        resources.ApplyResources(Me.LabelControl14, "LabelControl14")
        Me.LabelControl14.Appearance.Font = CType(resources.GetObject("LabelControl14.Appearance.Font"), System.Drawing.Font)
        Me.LabelControl14.Appearance.Options.UseFont = True
        Me.LabelControl14.Name = "LabelControl14"
        '
        'lblBalanceDue
        '
        resources.ApplyResources(Me.lblBalanceDue, "lblBalanceDue")
        Me.lblBalanceDue.Appearance.Font = CType(resources.GetObject("lblBalanceDue.Appearance.Font"), System.Drawing.Font)
        Me.lblBalanceDue.Appearance.Options.UseFont = True
        Me.lblBalanceDue.Name = "lblBalanceDue"
        '
        'LabelControl13
        '
        resources.ApplyResources(Me.LabelControl13, "LabelControl13")
        Me.LabelControl13.Appearance.Font = CType(resources.GetObject("LabelControl13.Appearance.Font"), System.Drawing.Font)
        Me.LabelControl13.Appearance.Options.UseFont = True
        Me.LabelControl13.Name = "LabelControl13"
        '
        'lblAmountPaid
        '
        resources.ApplyResources(Me.lblAmountPaid, "lblAmountPaid")
        Me.lblAmountPaid.Appearance.Font = CType(resources.GetObject("lblAmountPaid.Appearance.Font"), System.Drawing.Font)
        Me.lblAmountPaid.Appearance.Options.UseFont = True
        Me.lblAmountPaid.Name = "lblAmountPaid"
        '
        'LabelControl12
        '
        resources.ApplyResources(Me.LabelControl12, "LabelControl12")
        Me.LabelControl12.Appearance.Font = CType(resources.GetObject("LabelControl12.Appearance.Font"), System.Drawing.Font)
        Me.LabelControl12.Appearance.Options.UseFont = True
        Me.LabelControl12.Name = "LabelControl12"
        '
        'lblTotal
        '
        resources.ApplyResources(Me.lblTotal, "lblTotal")
        Me.lblTotal.Appearance.Font = CType(resources.GetObject("lblTotal.Appearance.Font"), System.Drawing.Font)
        Me.lblTotal.Appearance.Options.UseFont = True
        Me.lblTotal.Name = "lblTotal"
        '
        'LabelControl11
        '
        resources.ApplyResources(Me.LabelControl11, "LabelControl11")
        Me.LabelControl11.Appearance.Font = CType(resources.GetObject("LabelControl11.Appearance.Font"), System.Drawing.Font)
        Me.LabelControl11.Appearance.Options.UseFont = True
        Me.LabelControl11.Name = "LabelControl11"
        '
        'lblDiscount
        '
        resources.ApplyResources(Me.lblDiscount, "lblDiscount")
        Me.lblDiscount.Appearance.Font = CType(resources.GetObject("lblDiscount.Appearance.Font"), System.Drawing.Font)
        Me.lblDiscount.Appearance.Options.UseFont = True
        Me.lblDiscount.Name = "lblDiscount"
        '
        'LabelControl10
        '
        resources.ApplyResources(Me.LabelControl10, "LabelControl10")
        Me.LabelControl10.Appearance.Font = CType(resources.GetObject("LabelControl10.Appearance.Font"), System.Drawing.Font)
        Me.LabelControl10.Appearance.Options.UseFont = True
        Me.LabelControl10.Name = "LabelControl10"
        '
        'lblTax
        '
        resources.ApplyResources(Me.lblTax, "lblTax")
        Me.lblTax.Appearance.Font = CType(resources.GetObject("lblTax.Appearance.Font"), System.Drawing.Font)
        Me.lblTax.Appearance.Options.UseFont = True
        Me.lblTax.Name = "lblTax"
        '
        'LabelControl9
        '
        resources.ApplyResources(Me.LabelControl9, "LabelControl9")
        Me.LabelControl9.Appearance.Font = CType(resources.GetObject("LabelControl9.Appearance.Font"), System.Drawing.Font)
        Me.LabelControl9.Appearance.Options.UseFont = True
        Me.LabelControl9.Name = "LabelControl9"
        '
        'lblSubTotal
        '
        resources.ApplyResources(Me.lblSubTotal, "lblSubTotal")
        Me.lblSubTotal.Appearance.Font = CType(resources.GetObject("lblSubTotal.Appearance.Font"), System.Drawing.Font)
        Me.lblSubTotal.Appearance.Options.UseFont = True
        Me.lblSubTotal.Name = "lblSubTotal"
        '
        'GroupControl3
        '
        resources.ApplyResources(Me.GroupControl3, "GroupControl3")
        Me.GroupControl3.Controls.Add(Me.XtraTabControl1)
        Me.GroupControl3.Name = "GroupControl3"
        '
        'XtraTabControl1
        '
        resources.ApplyResources(Me.XtraTabControl1, "XtraTabControl1")
        Me.XtraTabControl1.AppearancePage.Header.Font = CType(resources.GetObject("XtraTabControl1.AppearancePage.Header.Font"), System.Drawing.Font)
        Me.XtraTabControl1.AppearancePage.Header.Options.UseFont = True
        Me.XtraTabControl1.Name = "XtraTabControl1"
        Me.XtraTabControl1.SelectedTabPage = Me.InvoiceDetailsPage
        Me.XtraTabControl1.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.InvoiceDetailsPage, Me.InvoicePaymentPage})
        '
        'InvoiceDetailsPage
        '
        resources.ApplyResources(Me.InvoiceDetailsPage, "InvoiceDetailsPage")
        Me.InvoiceDetailsPage.Controls.Add(Me.GridControl1)
        Me.InvoiceDetailsPage.Name = "InvoiceDetailsPage"
        '
        'GridControl1
        '
        resources.ApplyResources(Me.GridControl1, "GridControl1")
        Me.GridControl1.EmbeddedNavigator.AccessibleDescription = resources.GetString("GridControl1.EmbeddedNavigator.AccessibleDescription")
        Me.GridControl1.EmbeddedNavigator.AccessibleName = resources.GetString("GridControl1.EmbeddedNavigator.AccessibleName")
        Me.GridControl1.EmbeddedNavigator.AllowHtmlTextInToolTip = CType(resources.GetObject("GridControl1.EmbeddedNavigator.AllowHtmlTextInToolTip"), DevExpress.Utils.DefaultBoolean)
        Me.GridControl1.EmbeddedNavigator.Anchor = CType(resources.GetObject("GridControl1.EmbeddedNavigator.Anchor"), System.Windows.Forms.AnchorStyles)
        Me.GridControl1.EmbeddedNavigator.AutoSize = CType(resources.GetObject("GridControl1.EmbeddedNavigator.AutoSize"), Boolean)
        Me.GridControl1.EmbeddedNavigator.BackgroundImage = CType(resources.GetObject("GridControl1.EmbeddedNavigator.BackgroundImage"), System.Drawing.Image)
        Me.GridControl1.EmbeddedNavigator.BackgroundImageLayout = CType(resources.GetObject("GridControl1.EmbeddedNavigator.BackgroundImageLayout"), System.Windows.Forms.ImageLayout)
        Me.GridControl1.EmbeddedNavigator.ImeMode = CType(resources.GetObject("GridControl1.EmbeddedNavigator.ImeMode"), System.Windows.Forms.ImeMode)
        Me.GridControl1.EmbeddedNavigator.MaximumSize = CType(resources.GetObject("GridControl1.EmbeddedNavigator.MaximumSize"), System.Drawing.Size)
        Me.GridControl1.EmbeddedNavigator.TextLocation = CType(resources.GetObject("GridControl1.EmbeddedNavigator.TextLocation"), DevExpress.XtraEditors.NavigatorButtonsTextLocation)
        Me.GridControl1.EmbeddedNavigator.ToolTip = resources.GetString("GridControl1.EmbeddedNavigator.ToolTip")
        Me.GridControl1.EmbeddedNavigator.ToolTipIconType = CType(resources.GetObject("GridControl1.EmbeddedNavigator.ToolTipIconType"), DevExpress.Utils.ToolTipIconType)
        Me.GridControl1.EmbeddedNavigator.ToolTipTitle = resources.GetString("GridControl1.EmbeddedNavigator.ToolTipTitle")
        Me.GridControl1.MainView = Me.GridView1
        Me.GridControl1.Name = "GridControl1"
        Me.GridControl1.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridView1})
        '
        'GridView1
        '
        resources.ApplyResources(Me.GridView1, "GridView1")
        Me.GridView1.Appearance.HeaderPanel.Font = CType(resources.GetObject("GridView1.Appearance.HeaderPanel.Font"), System.Drawing.Font)
        Me.GridView1.Appearance.HeaderPanel.Options.UseFont = True
        Me.GridView1.Appearance.Row.Font = CType(resources.GetObject("GridView1.Appearance.Row.Font"), System.Drawing.Font)
        Me.GridView1.Appearance.Row.Options.UseFont = True
        Me.GridView1.GridControl = Me.GridControl1
        Me.GridView1.Name = "GridView1"
        '
        'InvoicePaymentPage
        '
        resources.ApplyResources(Me.InvoicePaymentPage, "InvoicePaymentPage")
        Me.InvoicePaymentPage.Controls.Add(Me.GridControl2)
        Me.InvoicePaymentPage.Name = "InvoicePaymentPage"
        '
        'GridControl2
        '
        resources.ApplyResources(Me.GridControl2, "GridControl2")
        Me.GridControl2.EmbeddedNavigator.AccessibleDescription = resources.GetString("GridControl2.EmbeddedNavigator.AccessibleDescription")
        Me.GridControl2.EmbeddedNavigator.AccessibleName = resources.GetString("GridControl2.EmbeddedNavigator.AccessibleName")
        Me.GridControl2.EmbeddedNavigator.AllowHtmlTextInToolTip = CType(resources.GetObject("GridControl2.EmbeddedNavigator.AllowHtmlTextInToolTip"), DevExpress.Utils.DefaultBoolean)
        Me.GridControl2.EmbeddedNavigator.Anchor = CType(resources.GetObject("GridControl2.EmbeddedNavigator.Anchor"), System.Windows.Forms.AnchorStyles)
        Me.GridControl2.EmbeddedNavigator.AutoSize = CType(resources.GetObject("GridControl2.EmbeddedNavigator.AutoSize"), Boolean)
        Me.GridControl2.EmbeddedNavigator.BackgroundImage = CType(resources.GetObject("GridControl2.EmbeddedNavigator.BackgroundImage"), System.Drawing.Image)
        Me.GridControl2.EmbeddedNavigator.BackgroundImageLayout = CType(resources.GetObject("GridControl2.EmbeddedNavigator.BackgroundImageLayout"), System.Windows.Forms.ImageLayout)
        Me.GridControl2.EmbeddedNavigator.ImeMode = CType(resources.GetObject("GridControl2.EmbeddedNavigator.ImeMode"), System.Windows.Forms.ImeMode)
        Me.GridControl2.EmbeddedNavigator.MaximumSize = CType(resources.GetObject("GridControl2.EmbeddedNavigator.MaximumSize"), System.Drawing.Size)
        Me.GridControl2.EmbeddedNavigator.TextLocation = CType(resources.GetObject("GridControl2.EmbeddedNavigator.TextLocation"), DevExpress.XtraEditors.NavigatorButtonsTextLocation)
        Me.GridControl2.EmbeddedNavigator.ToolTip = resources.GetString("GridControl2.EmbeddedNavigator.ToolTip")
        Me.GridControl2.EmbeddedNavigator.ToolTipIconType = CType(resources.GetObject("GridControl2.EmbeddedNavigator.ToolTipIconType"), DevExpress.Utils.ToolTipIconType)
        Me.GridControl2.EmbeddedNavigator.ToolTipTitle = resources.GetString("GridControl2.EmbeddedNavigator.ToolTipTitle")
        Me.GridControl2.MainView = Me.GridView2
        Me.GridControl2.Name = "GridControl2"
        Me.GridControl2.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridView2})
        '
        'GridView2
        '
        resources.ApplyResources(Me.GridView2, "GridView2")
        Me.GridView2.Appearance.HeaderPanel.Font = CType(resources.GetObject("GridView2.Appearance.HeaderPanel.Font"), System.Drawing.Font)
        Me.GridView2.Appearance.HeaderPanel.Options.UseFont = True
        Me.GridView2.Appearance.Row.Font = CType(resources.GetObject("GridView2.Appearance.Row.Font"), System.Drawing.Font)
        Me.GridView2.Appearance.Row.Options.UseFont = True
        Me.GridView2.GridControl = Me.GridControl2
        Me.GridView2.Name = "GridView2"
        '
        'frmInvoiceDetails
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.GroupControl3)
        Me.Controls.Add(Me.GroupControl2)
        Me.Controls.Add(Me.GroupControl1)
        Me.Name = "frmInvoiceDetails"
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl1.ResumeLayout(False)
        Me.GroupControl1.PerformLayout()
        CType(Me.txtNotes.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupControl2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl2.ResumeLayout(False)
        Me.GroupControl2.PerformLayout()
        CType(Me.GroupControl4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl4.ResumeLayout(False)
        CType(Me.chkWithPay.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupControl3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl3.ResumeLayout(False)
        CType(Me.XtraTabControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.XtraTabControl1.ResumeLayout(False)
        Me.InvoiceDetailsPage.ResumeLayout(False)
        CType(Me.GridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.InvoicePaymentPage.ResumeLayout(False)
        CType(Me.GridControl2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents GroupControl1 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents GroupControl2 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents GroupControl3 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents lblInvoiceNumber As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblInvoiceDate As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblDueDate As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblStatus As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblPatientName As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblAddress As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblEmail As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblPhone As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblNotes As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblSubTotal As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblTax As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblDiscount As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblTotal As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblAmountPaid As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblBalanceDue As DevExpress.XtraEditors.LabelControl
    Friend WithEvents XtraTabControl1 As DevExpress.XtraTab.XtraTabControl
    Friend WithEvents InvoiceDetailsPage As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents GridControl1 As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridView1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents InvoicePaymentPage As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents GridControl2 As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridView2 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents btnPrint As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnViewInv As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnClose As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnAddPayment As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnExportPDF As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents txtNotes As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl6 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl5 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl7 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl4 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl8 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl3 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl14 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl13 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl12 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl11 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl10 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl9 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents chkWithPay As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents GroupControl4 As DevExpress.XtraEditors.GroupControl
End Class
