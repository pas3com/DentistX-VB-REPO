<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmInvoicePreview
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
        Me.lblPatientName = New DevExpress.XtraEditors.LabelControl()
        Me.lblAddress = New DevExpress.XtraEditors.LabelControl()
        Me.lblPhone = New DevExpress.XtraEditors.LabelControl()
        Me.lblEmail = New DevExpress.XtraEditors.LabelControl()
        Me.lblInvoiceNo = New DevExpress.XtraEditors.LabelControl()
        Me.lblDate = New DevExpress.XtraEditors.LabelControl()
        Me.lblDueDate = New DevExpress.XtraEditors.LabelControl()
        Me.txtNotes = New DevExpress.XtraEditors.MemoEdit()
        Me.dgvPreview = New DevExpress.XtraGrid.GridControl()
        Me.GridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.lblSubTotal = New DevExpress.XtraEditors.LabelControl()
        Me.lblDiscount = New DevExpress.XtraEditors.LabelControl()
        Me.lblTax = New DevExpress.XtraEditors.LabelControl()
        Me.lblTotal = New DevExpress.XtraEditors.LabelControl()
        Me.lblAmountWords = New DevExpress.XtraEditors.LabelControl()
        Me.btnClose = New DevExpress.XtraEditors.SimpleButton()
        Me.btnPrint = New DevExpress.XtraEditors.SimpleButton()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl3 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl4 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl5 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl6 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl7 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl8 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl9 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl10 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl11 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl12 = New DevExpress.XtraEditors.LabelControl()
        Me.GroupControl1 = New DevExpress.XtraEditors.GroupControl()
        Me.GroupControl2 = New DevExpress.XtraEditors.GroupControl()
        CType(Me.txtNotes.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvPreview, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl1.SuspendLayout()
        CType(Me.GroupControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl2.SuspendLayout()
        Me.SuspendLayout()
        '
        'lblPatientName
        '
        Me.lblPatientName.Appearance.Font = New System.Drawing.Font("Calibri", 10.0!, System.Drawing.FontStyle.Bold)
        Me.lblPatientName.Appearance.ForeColor = System.Drawing.Color.Silver
        Me.lblPatientName.Appearance.Options.UseFont = True
        Me.lblPatientName.Appearance.Options.UseForeColor = True
        Me.lblPatientName.Location = New System.Drawing.Point(109, 35)
        Me.lblPatientName.Name = "lblPatientName"
        Me.lblPatientName.Size = New System.Drawing.Size(86, 15)
        Me.lblPatientName.TabIndex = 0
        Me.lblPatientName.Text = "lblPatientName"
        '
        'lblAddress
        '
        Me.lblAddress.Appearance.Font = New System.Drawing.Font("Calibri", 10.0!, System.Drawing.FontStyle.Bold)
        Me.lblAddress.Appearance.ForeColor = System.Drawing.Color.Silver
        Me.lblAddress.Appearance.Options.UseFont = True
        Me.lblAddress.Appearance.Options.UseForeColor = True
        Me.lblAddress.Location = New System.Drawing.Point(109, 64)
        Me.lblAddress.Name = "lblAddress"
        Me.lblAddress.Size = New System.Drawing.Size(57, 15)
        Me.lblAddress.TabIndex = 1
        Me.lblAddress.Text = "lblAddress"
        '
        'lblPhone
        '
        Me.lblPhone.Appearance.Font = New System.Drawing.Font("Calibri", 10.0!, System.Drawing.FontStyle.Bold)
        Me.lblPhone.Appearance.ForeColor = System.Drawing.Color.Silver
        Me.lblPhone.Appearance.Options.UseFont = True
        Me.lblPhone.Appearance.Options.UseForeColor = True
        Me.lblPhone.Location = New System.Drawing.Point(109, 91)
        Me.lblPhone.Name = "lblPhone"
        Me.lblPhone.Size = New System.Drawing.Size(48, 15)
        Me.lblPhone.TabIndex = 2
        Me.lblPhone.Text = "lblPhone"
        '
        'lblEmail
        '
        Me.lblEmail.Appearance.Font = New System.Drawing.Font("Calibri", 10.0!, System.Drawing.FontStyle.Bold)
        Me.lblEmail.Appearance.ForeColor = System.Drawing.Color.Silver
        Me.lblEmail.Appearance.Options.UseFont = True
        Me.lblEmail.Appearance.Options.UseForeColor = True
        Me.lblEmail.Location = New System.Drawing.Point(109, 118)
        Me.lblEmail.Name = "lblEmail"
        Me.lblEmail.Size = New System.Drawing.Size(42, 15)
        Me.lblEmail.TabIndex = 3
        Me.lblEmail.Text = "lblEmail"
        '
        'lblInvoiceNo
        '
        Me.lblInvoiceNo.Appearance.Font = New System.Drawing.Font("Calibri", 10.0!, System.Drawing.FontStyle.Bold)
        Me.lblInvoiceNo.Appearance.ForeColor = System.Drawing.Color.Fuchsia
        Me.lblInvoiceNo.Appearance.Options.UseFont = True
        Me.lblInvoiceNo.Appearance.Options.UseForeColor = True
        Me.lblInvoiceNo.Location = New System.Drawing.Point(87, 37)
        Me.lblInvoiceNo.Name = "lblInvoiceNo"
        Me.lblInvoiceNo.Size = New System.Drawing.Size(67, 15)
        Me.lblInvoiceNo.TabIndex = 4
        Me.lblInvoiceNo.Text = "lblInvoiceNo"
        '
        'lblDate
        '
        Me.lblDate.Appearance.Font = New System.Drawing.Font("Calibri", 10.0!, System.Drawing.FontStyle.Bold)
        Me.lblDate.Appearance.ForeColor = System.Drawing.Color.Teal
        Me.lblDate.Appearance.Options.UseFont = True
        Me.lblDate.Appearance.Options.UseForeColor = True
        Me.lblDate.Location = New System.Drawing.Point(252, 37)
        Me.lblDate.Name = "lblDate"
        Me.lblDate.Size = New System.Drawing.Size(39, 15)
        Me.lblDate.TabIndex = 5
        Me.lblDate.Text = "lblDate"
        '
        'lblDueDate
        '
        Me.lblDueDate.Appearance.Font = New System.Drawing.Font("Calibri", 10.0!, System.Drawing.FontStyle.Bold)
        Me.lblDueDate.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.lblDueDate.Appearance.Options.UseFont = True
        Me.lblDueDate.Appearance.Options.UseForeColor = True
        Me.lblDueDate.Location = New System.Drawing.Point(397, 37)
        Me.lblDueDate.Name = "lblDueDate"
        Me.lblDueDate.Size = New System.Drawing.Size(61, 15)
        Me.lblDueDate.TabIndex = 6
        Me.lblDueDate.Text = "lblDueDate"
        '
        'txtNotes
        '
        Me.txtNotes.EditValue = ""
        Me.txtNotes.Location = New System.Drawing.Point(246, 84)
        Me.txtNotes.Name = "txtNotes"
        Me.txtNotes.Properties.Appearance.Font = New System.Drawing.Font("Calibri", 10.0!, System.Drawing.FontStyle.Bold)
        Me.txtNotes.Properties.Appearance.Options.UseFont = True
        Me.txtNotes.Size = New System.Drawing.Size(207, 68)
        Me.txtNotes.TabIndex = 7
        '
        'dgvPreview
        '
        Me.dgvPreview.Location = New System.Drawing.Point(39, 196)
        Me.dgvPreview.MainView = Me.GridView1
        Me.dgvPreview.Name = "dgvPreview"
        Me.dgvPreview.Size = New System.Drawing.Size(770, 200)
        Me.dgvPreview.TabIndex = 8
        Me.dgvPreview.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridView1})
        '
        'GridView1
        '
        Me.GridView1.GridControl = Me.dgvPreview
        Me.GridView1.Name = "GridView1"
        '
        'lblSubTotal
        '
        Me.lblSubTotal.Appearance.Font = New System.Drawing.Font("Calibri", 10.0!, System.Drawing.FontStyle.Bold)
        Me.lblSubTotal.Appearance.ForeColor = System.Drawing.Color.Brown
        Me.lblSubTotal.Appearance.Options.UseFont = True
        Me.lblSubTotal.Appearance.Options.UseForeColor = True
        Me.lblSubTotal.Location = New System.Drawing.Point(84, 63)
        Me.lblSubTotal.Name = "lblSubTotal"
        Me.lblSubTotal.Size = New System.Drawing.Size(60, 15)
        Me.lblSubTotal.TabIndex = 9
        Me.lblSubTotal.Text = "lblSubTotal"
        '
        'lblDiscount
        '
        Me.lblDiscount.Appearance.Font = New System.Drawing.Font("Calibri", 10.0!, System.Drawing.FontStyle.Bold)
        Me.lblDiscount.Appearance.ForeColor = System.Drawing.Color.Indigo
        Me.lblDiscount.Appearance.Options.UseFont = True
        Me.lblDiscount.Appearance.Options.UseForeColor = True
        Me.lblDiscount.Location = New System.Drawing.Point(84, 84)
        Me.lblDiscount.Name = "lblDiscount"
        Me.lblDiscount.Size = New System.Drawing.Size(60, 15)
        Me.lblDiscount.TabIndex = 10
        Me.lblDiscount.Text = "lblDiscount"
        '
        'lblTax
        '
        Me.lblTax.Appearance.Font = New System.Drawing.Font("Calibri", 10.0!, System.Drawing.FontStyle.Bold)
        Me.lblTax.Appearance.ForeColor = System.Drawing.Color.Magenta
        Me.lblTax.Appearance.Options.UseFont = True
        Me.lblTax.Appearance.Options.UseForeColor = True
        Me.lblTax.Location = New System.Drawing.Point(84, 105)
        Me.lblTax.Name = "lblTax"
        Me.lblTax.Size = New System.Drawing.Size(31, 15)
        Me.lblTax.TabIndex = 11
        Me.lblTax.Text = "lblTax"
        '
        'lblTotal
        '
        Me.lblTotal.Appearance.Font = New System.Drawing.Font("Calibri", 10.0!, System.Drawing.FontStyle.Bold)
        Me.lblTotal.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.lblTotal.Appearance.Options.UseFont = True
        Me.lblTotal.Appearance.Options.UseForeColor = True
        Me.lblTotal.Location = New System.Drawing.Point(84, 126)
        Me.lblTotal.Name = "lblTotal"
        Me.lblTotal.Size = New System.Drawing.Size(40, 15)
        Me.lblTotal.TabIndex = 12
        Me.lblTotal.Text = "lblTotal"
        '
        'lblAmountWords
        '
        Me.lblAmountWords.Appearance.Font = New System.Drawing.Font("Calibri", 10.0!, System.Drawing.FontStyle.Bold)
        Me.lblAmountWords.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblAmountWords.Appearance.Options.UseFont = True
        Me.lblAmountWords.Appearance.Options.UseForeColor = True
        Me.lblAmountWords.Appearance.Options.UseTextOptions = True
        Me.lblAmountWords.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.lblAmountWords.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.lblAmountWords.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lblAmountWords.Location = New System.Drawing.Point(2, 158)
        Me.lblAmountWords.Name = "lblAmountWords"
        Me.lblAmountWords.Size = New System.Drawing.Size(472, 23)
        Me.lblAmountWords.TabIndex = 13
        Me.lblAmountWords.Text = "lblAmountWords"
        '
        'btnClose
        '
        Me.btnClose.Appearance.Font = New System.Drawing.Font("Calibri", 10.0!, System.Drawing.FontStyle.Bold)
        Me.btnClose.Appearance.Options.UseFont = True
        Me.btnClose.Location = New System.Drawing.Point(323, 411)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(75, 23)
        Me.btnClose.TabIndex = 14
        Me.btnClose.Text = "Close"
        '
        'btnPrint
        '
        Me.btnPrint.Appearance.Font = New System.Drawing.Font("Calibri", 10.0!, System.Drawing.FontStyle.Bold)
        Me.btnPrint.Appearance.Options.UseFont = True
        Me.btnPrint.Location = New System.Drawing.Point(451, 411)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(75, 23)
        Me.btnPrint.TabIndex = 15
        Me.btnPrint.Text = "Print"
        '
        'LabelControl1
        '
        Me.LabelControl1.Appearance.Font = New System.Drawing.Font("Calibri", 10.0!, System.Drawing.FontStyle.Bold)
        Me.LabelControl1.Appearance.Options.UseFont = True
        Me.LabelControl1.Location = New System.Drawing.Point(14, 35)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(76, 15)
        Me.LabelControl1.TabIndex = 0
        Me.LabelControl1.Text = "Patient Name"
        '
        'LabelControl2
        '
        Me.LabelControl2.Appearance.Font = New System.Drawing.Font("Calibri", 10.0!, System.Drawing.FontStyle.Bold)
        Me.LabelControl2.Appearance.Options.UseFont = True
        Me.LabelControl2.Location = New System.Drawing.Point(14, 64)
        Me.LabelControl2.Name = "LabelControl2"
        Me.LabelControl2.Size = New System.Drawing.Size(44, 15)
        Me.LabelControl2.TabIndex = 1
        Me.LabelControl2.Text = "Address"
        '
        'LabelControl3
        '
        Me.LabelControl3.Appearance.Font = New System.Drawing.Font("Calibri", 10.0!, System.Drawing.FontStyle.Bold)
        Me.LabelControl3.Appearance.Options.UseFont = True
        Me.LabelControl3.Location = New System.Drawing.Point(14, 91)
        Me.LabelControl3.Name = "LabelControl3"
        Me.LabelControl3.Size = New System.Drawing.Size(35, 15)
        Me.LabelControl3.TabIndex = 2
        Me.LabelControl3.Text = "Phone"
        '
        'LabelControl4
        '
        Me.LabelControl4.Appearance.Font = New System.Drawing.Font("Calibri", 10.0!, System.Drawing.FontStyle.Bold)
        Me.LabelControl4.Appearance.Options.UseFont = True
        Me.LabelControl4.Location = New System.Drawing.Point(14, 118)
        Me.LabelControl4.Name = "LabelControl4"
        Me.LabelControl4.Size = New System.Drawing.Size(19, 15)
        Me.LabelControl4.TabIndex = 3
        Me.LabelControl4.Text = "Sex"
        '
        'LabelControl5
        '
        Me.LabelControl5.Appearance.Font = New System.Drawing.Font("Calibri", 10.0!, System.Drawing.FontStyle.Bold)
        Me.LabelControl5.Appearance.ForeColor = System.Drawing.Color.DarkGray
        Me.LabelControl5.Appearance.Options.UseFont = True
        Me.LabelControl5.Appearance.Options.UseForeColor = True
        Me.LabelControl5.Location = New System.Drawing.Point(14, 37)
        Me.LabelControl5.Name = "LabelControl5"
        Me.LabelControl5.Size = New System.Drawing.Size(67, 15)
        Me.LabelControl5.TabIndex = 4
        Me.LabelControl5.Text = "Invoice No : "
        '
        'LabelControl6
        '
        Me.LabelControl6.Appearance.Font = New System.Drawing.Font("Calibri", 10.0!, System.Drawing.FontStyle.Bold)
        Me.LabelControl6.Appearance.ForeColor = System.Drawing.Color.DarkGray
        Me.LabelControl6.Appearance.Options.UseFont = True
        Me.LabelControl6.Appearance.Options.UseForeColor = True
        Me.LabelControl6.Location = New System.Drawing.Point(210, 37)
        Me.LabelControl6.Name = "LabelControl6"
        Me.LabelControl6.Size = New System.Drawing.Size(36, 15)
        Me.LabelControl6.TabIndex = 5
        Me.LabelControl6.Text = "Date : "
        '
        'LabelControl7
        '
        Me.LabelControl7.Appearance.Font = New System.Drawing.Font("Calibri", 10.0!, System.Drawing.FontStyle.Bold)
        Me.LabelControl7.Appearance.ForeColor = System.Drawing.Color.DarkGray
        Me.LabelControl7.Appearance.Options.UseFont = True
        Me.LabelControl7.Appearance.Options.UseForeColor = True
        Me.LabelControl7.Location = New System.Drawing.Point(330, 37)
        Me.LabelControl7.Name = "LabelControl7"
        Me.LabelControl7.Size = New System.Drawing.Size(61, 15)
        Me.LabelControl7.TabIndex = 6
        Me.LabelControl7.Text = "Due Date : "
        '
        'LabelControl8
        '
        Me.LabelControl8.Appearance.Font = New System.Drawing.Font("Calibri", 10.0!, System.Drawing.FontStyle.Bold)
        Me.LabelControl8.Appearance.Options.UseFont = True
        Me.LabelControl8.Location = New System.Drawing.Point(207, 105)
        Me.LabelControl8.Name = "LabelControl8"
        Me.LabelControl8.Size = New System.Drawing.Size(33, 15)
        Me.LabelControl8.TabIndex = 6
        Me.LabelControl8.Text = "Notes"
        '
        'LabelControl9
        '
        Me.LabelControl9.Appearance.Font = New System.Drawing.Font("Calibri", 10.0!, System.Drawing.FontStyle.Bold)
        Me.LabelControl9.Appearance.ForeColor = System.Drawing.Color.Brown
        Me.LabelControl9.Appearance.Options.UseFont = True
        Me.LabelControl9.Appearance.Options.UseForeColor = True
        Me.LabelControl9.Location = New System.Drawing.Point(14, 63)
        Me.LabelControl9.Name = "LabelControl9"
        Me.LabelControl9.Size = New System.Drawing.Size(60, 15)
        Me.LabelControl9.TabIndex = 9
        Me.LabelControl9.Text = "Sub Total : "
        '
        'LabelControl10
        '
        Me.LabelControl10.Appearance.Font = New System.Drawing.Font("Calibri", 10.0!, System.Drawing.FontStyle.Bold)
        Me.LabelControl10.Appearance.ForeColor = System.Drawing.Color.Indigo
        Me.LabelControl10.Appearance.Options.UseFont = True
        Me.LabelControl10.Appearance.Options.UseForeColor = True
        Me.LabelControl10.Location = New System.Drawing.Point(14, 84)
        Me.LabelControl10.Name = "LabelControl10"
        Me.LabelControl10.Size = New System.Drawing.Size(57, 15)
        Me.LabelControl10.TabIndex = 10
        Me.LabelControl10.Text = "Discount : "
        '
        'LabelControl11
        '
        Me.LabelControl11.Appearance.Font = New System.Drawing.Font("Calibri", 10.0!, System.Drawing.FontStyle.Bold)
        Me.LabelControl11.Appearance.ForeColor = System.Drawing.Color.Magenta
        Me.LabelControl11.Appearance.Options.UseFont = True
        Me.LabelControl11.Appearance.Options.UseForeColor = True
        Me.LabelControl11.Location = New System.Drawing.Point(14, 105)
        Me.LabelControl11.Name = "LabelControl11"
        Me.LabelControl11.Size = New System.Drawing.Size(28, 15)
        Me.LabelControl11.TabIndex = 11
        Me.LabelControl11.Text = "Tax : "
        '
        'LabelControl12
        '
        Me.LabelControl12.Appearance.Font = New System.Drawing.Font("Calibri", 10.0!, System.Drawing.FontStyle.Bold)
        Me.LabelControl12.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.LabelControl12.Appearance.Options.UseFont = True
        Me.LabelControl12.Appearance.Options.UseForeColor = True
        Me.LabelControl12.Location = New System.Drawing.Point(14, 126)
        Me.LabelControl12.Name = "LabelControl12"
        Me.LabelControl12.Size = New System.Drawing.Size(37, 15)
        Me.LabelControl12.TabIndex = 12
        Me.LabelControl12.Text = "Total : "
        '
        'GroupControl1
        '
        Me.GroupControl1.AppearanceCaption.Font = New System.Drawing.Font("Calibri", 10.0!, System.Drawing.FontStyle.Bold)
        Me.GroupControl1.AppearanceCaption.Options.UseFont = True
        Me.GroupControl1.Controls.Add(Me.LabelControl4)
        Me.GroupControl1.Controls.Add(Me.lblEmail)
        Me.GroupControl1.Controls.Add(Me.LabelControl3)
        Me.GroupControl1.Controls.Add(Me.lblPhone)
        Me.GroupControl1.Controls.Add(Me.LabelControl2)
        Me.GroupControl1.Controls.Add(Me.lblAddress)
        Me.GroupControl1.Controls.Add(Me.LabelControl1)
        Me.GroupControl1.Controls.Add(Me.lblPatientName)
        Me.GroupControl1.Location = New System.Drawing.Point(8, 8)
        Me.GroupControl1.Name = "GroupControl1"
        Me.GroupControl1.Size = New System.Drawing.Size(342, 183)
        Me.GroupControl1.TabIndex = 16
        Me.GroupControl1.Text = "Patient Details"
        '
        'GroupControl2
        '
        Me.GroupControl2.AppearanceCaption.Font = New System.Drawing.Font("Calibri", 10.0!, System.Drawing.FontStyle.Bold)
        Me.GroupControl2.AppearanceCaption.Options.UseFont = True
        Me.GroupControl2.Controls.Add(Me.lblAmountWords)
        Me.GroupControl2.Controls.Add(Me.LabelControl12)
        Me.GroupControl2.Controls.Add(Me.lblTotal)
        Me.GroupControl2.Controls.Add(Me.LabelControl11)
        Me.GroupControl2.Controls.Add(Me.lblTax)
        Me.GroupControl2.Controls.Add(Me.LabelControl10)
        Me.GroupControl2.Controls.Add(Me.lblDiscount)
        Me.GroupControl2.Controls.Add(Me.LabelControl9)
        Me.GroupControl2.Controls.Add(Me.lblSubTotal)
        Me.GroupControl2.Controls.Add(Me.txtNotes)
        Me.GroupControl2.Controls.Add(Me.LabelControl8)
        Me.GroupControl2.Controls.Add(Me.LabelControl7)
        Me.GroupControl2.Controls.Add(Me.lblDueDate)
        Me.GroupControl2.Controls.Add(Me.LabelControl6)
        Me.GroupControl2.Controls.Add(Me.lblDate)
        Me.GroupControl2.Controls.Add(Me.LabelControl5)
        Me.GroupControl2.Controls.Add(Me.lblInvoiceNo)
        Me.GroupControl2.Location = New System.Drawing.Point(356, 7)
        Me.GroupControl2.Name = "GroupControl2"
        Me.GroupControl2.Size = New System.Drawing.Size(476, 183)
        Me.GroupControl2.TabIndex = 17
        Me.GroupControl2.Text = "Invoice Details"
        '
        'frmInvoicePreview
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(848, 467)
        Me.Controls.Add(Me.GroupControl2)
        Me.Controls.Add(Me.GroupControl1)
        Me.Controls.Add(Me.btnPrint)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.dgvPreview)
        Me.Name = "frmInvoicePreview"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Invoice Preview"
        CType(Me.txtNotes.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvPreview, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl1.ResumeLayout(False)
        Me.GroupControl1.PerformLayout()
        CType(Me.GroupControl2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl2.ResumeLayout(False)
        Me.GroupControl2.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents lblPatientName As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblAddress As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblPhone As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblEmail As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblInvoiceNo As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblDate As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblDueDate As DevExpress.XtraEditors.LabelControl
    Friend WithEvents txtNotes As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents dgvPreview As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridView1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents lblSubTotal As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblDiscount As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblTax As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblTotal As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblAmountWords As DevExpress.XtraEditors.LabelControl
    Friend WithEvents btnClose As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnPrint As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl3 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl4 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl5 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl6 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl7 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl8 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl9 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl10 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl11 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl12 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents GroupControl1 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents GroupControl2 As DevExpress.XtraEditors.GroupControl
End Class
