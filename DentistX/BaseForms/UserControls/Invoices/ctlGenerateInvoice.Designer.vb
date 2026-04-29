<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class ctlGenerateInvoice
    Inherits DevExpress.XtraEditors.XtraUserControl

    'UserControl overrides dispose to clean up the component list.
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
        components = New System.ComponentModel.Container()
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font


        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.lblPatientID = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.cbPatients = New System.Windows.Forms.ComboBox()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.dgvTreatments = New System.Windows.Forms.DataGridView()
        Me.colSelect = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.colTrtID = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colDetail = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colDate = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colValue = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colDiscount = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colInvoiced = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.lblSelectedCount = New System.Windows.Forms.Label()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.txtNotes = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.dtpDueDate = New System.Windows.Forms.DateTimePicker()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.dtpInvoiceDate = New System.Windows.Forms.DateTimePicker()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.lblInvoiceNumber = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.lblTotalAmount = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.lblTaxAmount = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.lblDiscount = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.lblSubTotal = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.btnViewInvoices = New System.Windows.Forms.Button()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.btnPreview = New System.Windows.Forms.Button()
        Me.Panel1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        CType(Me.dgvTreatments, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(44, Byte), Integer), CType(CType(62, Byte), Integer), CType(CType(80, Byte), Integer))
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1200, 60)
        Me.Panel1.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Segoe UI", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.White
        Me.Label1.Location = New System.Drawing.Point(20, 12)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(203, 32)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Generate Invoice"
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.GroupBox1)
        Me.Panel2.Controls.Add(Me.GroupBox2)
        Me.Panel2.Controls.Add(Me.GroupBox3)
        Me.Panel2.Controls.Add(Me.GroupBox4)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel2.Location = New System.Drawing.Point(0, 60)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Padding = New System.Windows.Forms.Padding(20)
        Me.Panel2.Size = New System.Drawing.Size(1200, 600)
        Me.Panel2.TabIndex = 1
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.lblPatientID)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.cbPatients)
        Me.GroupBox1.Location = New System.Drawing.Point(23, 23)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(1154, 80)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Patient Selection"
        '
        'lblPatientID
        '
        Me.lblPatientID.AutoSize = True
        Me.lblPatientID.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPatientID.Location = New System.Drawing.Point(300, 35)
        Me.lblPatientID.Name = "lblPatientID"
        Me.lblPatientID.Size = New System.Drawing.Size(79, 17)
        Me.lblPatientID.TabIndex = 2
        Me.lblPatientID.Text = "Patient ID: -"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(20, 35)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(47, 15)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Patient:"
        '
        'cbPatients
        '
        Me.cbPatients.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbPatients.FormattingEnabled = True
        Me.cbPatients.Location = New System.Drawing.Point(73, 32)
        Me.cbPatients.Name = "cbPatients"
        Me.cbPatients.Size = New System.Drawing.Size(200, 23)
        Me.cbPatients.TabIndex = 0
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.dgvTreatments)
        Me.GroupBox2.Controls.Add(Me.lblSelectedCount)
        Me.GroupBox2.Location = New System.Drawing.Point(23, 119)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(800, 458)
        Me.GroupBox2.TabIndex = 1
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Select Treatments for Invoice"
        '
        'dgvTreatments
        '
        Me.dgvTreatments.AllowUserToAddRows = False
        Me.dgvTreatments.AllowUserToDeleteRows = False
        Me.dgvTreatments.BackgroundColor = System.Drawing.Color.White
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvTreatments.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.dgvTreatments.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvTreatments.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colSelect, Me.colTrtID, Me.colDetail, Me.colDate, Me.colValue, Me.colDiscount, Me.colInvoiced})
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(225, Byte), Integer), CType(CType(238, Byte), Integer), CType(CType(255, Byte), Integer))
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgvTreatments.DefaultCellStyle = DataGridViewCellStyle2
        Me.dgvTreatments.Location = New System.Drawing.Point(20, 60)
        Me.dgvTreatments.Name = "dgvTreatments"
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvTreatments.RowHeadersDefaultCellStyle = DataGridViewCellStyle3
        Me.dgvTreatments.RowHeadersVisible = False
        Me.dgvTreatments.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvTreatments.Size = New System.Drawing.Size(760, 380)
        Me.dgvTreatments.TabIndex = 1
        '
        'colSelect
        '
        Me.colSelect.HeaderText = "Select"
        Me.colSelect.Name = "colSelect"
        Me.colSelect.Width = 50
        '
        'colTrtID
        '
        Me.colTrtID.DataPropertyName = "TrtID"
        Me.colTrtID.HeaderText = "ID"
        Me.colTrtID.Name = "colTrtID"
        Me.colTrtID.ReadOnly = True
        Me.colTrtID.Width = 50
        '
        'colDetail
        '
        Me.colDetail.DataPropertyName = "Detail"
        Me.colDetail.HeaderText = "Treatment Detail"
        Me.colDetail.Name = "colDetail"
        Me.colDetail.ReadOnly = True
        Me.colDetail.Width = 300
        '
        'colDate
        '
        Me.colDate.DataPropertyName = "TrtDate"
        Me.colDate.HeaderText = "Date"
        Me.colDate.Name = "colDate"
        Me.colDate.ReadOnly = True
        '
        'colValue
        '
        Me.colValue.DataPropertyName = "TrtValue"
        Me.colValue.HeaderText = "Value"
        Me.colValue.Name = "colValue"
        Me.colValue.ReadOnly = True
        '
        'colDiscount
        '
        Me.colDiscount.DataPropertyName = "Discount"
        Me.colDiscount.HeaderText = "Discount"
        Me.colDiscount.Name = "colDiscount"
        Me.colDiscount.ReadOnly = True
        '
        'colInvoiced
        '
        Me.colInvoiced.HeaderText = "Invoiced?"
        Me.colInvoiced.Name = "colInvoiced"
        Me.colInvoiced.ReadOnly = True
        '
        'lblSelectedCount
        '
        Me.lblSelectedCount.AutoSize = True
        Me.lblSelectedCount.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSelectedCount.Location = New System.Drawing.Point(20, 35)
        Me.lblSelectedCount.Name = "lblSelectedCount"
        Me.lblSelectedCount.Size = New System.Drawing.Size(143, 17)
        Me.lblSelectedCount.TabIndex = 0
        Me.lblSelectedCount.Text = "0 treatment(s) selected"
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.txtNotes)
        Me.GroupBox3.Controls.Add(Me.Label10)
        Me.GroupBox3.Controls.Add(Me.dtpDueDate)
        Me.GroupBox3.Controls.Add(Me.Label9)
        Me.GroupBox3.Controls.Add(Me.dtpInvoiceDate)
        Me.GroupBox3.Controls.Add(Me.Label8)
        Me.GroupBox3.Controls.Add(Me.lblInvoiceNumber)
        Me.GroupBox3.Controls.Add(Me.Label7)
        Me.GroupBox3.Location = New System.Drawing.Point(829, 119)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(348, 250)
        Me.GroupBox3.TabIndex = 2
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Invoice Details"
        '
        'txtNotes
        '
        Me.txtNotes.Location = New System.Drawing.Point(20, 185)
        Me.txtNotes.Multiline = True
        Me.txtNotes.Name = "txtNotes"
        Me.txtNotes.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtNotes.Size = New System.Drawing.Size(310, 50)
        Me.txtNotes.TabIndex = 7
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(20, 165)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(41, 15)
        Me.Label10.TabIndex = 6
        Me.Label10.Text = "Notes:"
        '
        'dtpDueDate
        '
        Me.dtpDueDate.Format = System.Windows.Forms.DateTimePickerFormat.Short
        Me.dtpDueDate.Location = New System.Drawing.Point(100, 125)
        Me.dtpDueDate.Name = "dtpDueDate"
        Me.dtpDueDate.Size = New System.Drawing.Size(120, 23)
        Me.dtpDueDate.TabIndex = 5
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(20, 130)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(58, 15)
        Me.Label9.TabIndex = 4
        Me.Label9.Text = "Due Date:"
        '
        'dtpInvoiceDate
        '
        Me.dtpInvoiceDate.Format = System.Windows.Forms.DateTimePickerFormat.Short
        Me.dtpInvoiceDate.Location = New System.Drawing.Point(100, 90)
        Me.dtpInvoiceDate.Name = "dtpInvoiceDate"
        Me.dtpInvoiceDate.Size = New System.Drawing.Size(120, 23)
        Me.dtpInvoiceDate.TabIndex = 3
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(20, 95)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(76, 15)
        Me.Label8.TabIndex = 2
        Me.Label8.Text = "Invoice Date:"
        '
        'lblInvoiceNumber
        '
        Me.lblInvoiceNumber.AutoSize = True
        Me.lblInvoiceNumber.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblInvoiceNumber.Location = New System.Drawing.Point(20, 55)
        Me.lblInvoiceNumber.Name = "lblInvoiceNumber"
        Me.lblInvoiceNumber.Size = New System.Drawing.Size(184, 17)
        Me.lblInvoiceNumber.TabIndex = 1
        Me.lblInvoiceNumber.Text = "Will be generated on save"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(20, 35)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(93, 15)
        Me.Label7.TabIndex = 0
        Me.Label7.Text = "Invoice Number:"
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.lblTotalAmount)
        Me.GroupBox4.Controls.Add(Me.Label6)
        Me.GroupBox4.Controls.Add(Me.lblTaxAmount)
        Me.GroupBox4.Controls.Add(Me.Label5)
        Me.GroupBox4.Controls.Add(Me.lblDiscount)
        Me.GroupBox4.Controls.Add(Me.Label4)
        Me.GroupBox4.Controls.Add(Me.lblSubTotal)
        Me.GroupBox4.Controls.Add(Me.Label3)
        Me.GroupBox4.Location = New System.Drawing.Point(829, 375)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(348, 202)
        Me.GroupBox4.TabIndex = 3
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "Invoice Summary"
        '
        'lblTotalAmount
        '
        Me.lblTotalAmount.Font = New System.Drawing.Font("Segoe UI", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotalAmount.ForeColor = System.Drawing.Color.FromArgb(CType(CType(44, Byte), Integer), CType(CType(62, Byte), Integer), CType(CType(80, Byte), Integer))
        Me.lblTotalAmount.Location = New System.Drawing.Point(120, 155)
        Me.lblTotalAmount.Name = "lblTotalAmount"
        Me.lblTotalAmount.Size = New System.Drawing.Size(200, 25)
        Me.lblTotalAmount.TabIndex = 7
        Me.lblTotalAmount.Text = "$0.00"
        Me.lblTotalAmount.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(20, 155)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(53, 21)
        Me.Label6.TabIndex = 6
        Me.Label6.Text = "Total:"
        '
        'lblTaxAmount
        '
        Me.lblTaxAmount.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTaxAmount.Location = New System.Drawing.Point(120, 115)
        Me.lblTaxAmount.Name = "lblTaxAmount"
        Me.lblTaxAmount.Size = New System.Drawing.Size(200, 25)
        Me.lblTaxAmount.TabIndex = 5
        Me.lblTaxAmount.Text = "$0.00"
        Me.lblTaxAmount.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(20, 120)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(55, 15)
        Me.Label5.TabIndex = 4
        Me.Label5.Text = "Tax (15%):"
        '
        'lblDiscount
        '
        Me.lblDiscount.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDiscount.Location = New System.Drawing.Point(120, 85)
        Me.lblDiscount.Name = "lblDiscount"
        Me.lblDiscount.Size = New System.Drawing.Size(200, 25)
        Me.lblDiscount.TabIndex = 3
        Me.lblDiscount.Text = "$0.00"
        Me.lblDiscount.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(20, 90)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(54, 15)
        Me.Label4.TabIndex = 2
        Me.Label4.Text = "Discount:"
        '
        'lblSubTotal
        '
        Me.lblSubTotal.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSubTotal.Location = New System.Drawing.Point(120, 55)
        Me.lblSubTotal.Name = "lblSubTotal"
        Me.lblSubTotal.Size = New System.Drawing.Size(200, 25)
        Me.lblSubTotal.TabIndex = 1
        Me.lblSubTotal.Text = "$0.00"
        Me.lblSubTotal.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(20, 60)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(57, 15)
        Me.Label3.TabIndex = 0
        Me.Label3.Text = "Subtotal:"
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(241, Byte), Integer))
        Me.Panel3.Controls.Add(Me.btnViewInvoices)
        Me.Panel3.Controls.Add(Me.btnCancel)
        Me.Panel3.Controls.Add(Me.btnSave)
        Me.Panel3.Controls.Add(Me.btnPreview)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel3.Location = New System.Drawing.Point(0, 660)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(1200, 60)
        Me.Panel3.TabIndex = 2
        '
        'btnViewInvoices
        '
        Me.btnViewInvoices.BackColor = System.Drawing.Color.FromArgb(CType(CType(52, Byte), Integer), CType(CType(152, Byte), Integer), CType(CType(219, Byte), Integer))
        Me.btnViewInvoices.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnViewInvoices.ForeColor = System.Drawing.Color.White
        Me.btnViewInvoices.Location = New System.Drawing.Point(23, 15)
        Me.btnViewInvoices.Name = "btnViewInvoices"
        Me.btnViewInvoices.Size = New System.Drawing.Size(120, 30)
        Me.btnViewInvoices.TabIndex = 3
        Me.btnViewInvoices.Text = "View Invoices"
        Me.btnViewInvoices.UseVisualStyleBackColor = False
        '
        'btnCancel
        '
        Me.btnCancel.BackColor = System.Drawing.Color.FromArgb(CType(CType(231, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(60, Byte), Integer))
        Me.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCancel.ForeColor = System.Drawing.Color.White
        Me.btnCancel.Location = New System.Drawing.Point(1057, 15)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(120, 30)
        Me.btnCancel.TabIndex = 2
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.UseVisualStyleBackColor = False
        '
        'btnSave
        '
        Me.btnSave.BackColor = System.Drawing.Color.FromArgb(CType(CType(46, Byte), Integer), CType(CType(204, Byte), Integer), CType(CType(113, Byte), Integer))
        Me.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSave.ForeColor = System.Drawing.Color.White
        Me.btnSave.Location = New System.Drawing.Point(931, 15)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(120, 30)
        Me.btnSave.TabIndex = 1
        Me.btnSave.Text = "Save Invoice"
        Me.btnSave.UseVisualStyleBackColor = False
        '
        'btnPreview
        '
        Me.btnPreview.BackColor = System.Drawing.Color.FromArgb(CType(CType(241, Byte), Integer), CType(CType(196, Byte), Integer), CType(CType(15, Byte), Integer))
        Me.btnPreview.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnPreview.ForeColor = System.Drawing.Color.White
        Me.btnPreview.Location = New System.Drawing.Point(805, 15)
        Me.btnPreview.Name = "btnPreview"
        Me.btnPreview.Size = New System.Drawing.Size(120, 30)
        Me.btnPreview.TabIndex = 0
        Me.btnPreview.Text = "Preview Invoice"
        Me.btnPreview.UseVisualStyleBackColor = False
        '
        'frmGenerateInvoice
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1200, 720)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Panel3)
        Me.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Name = "frmGenerateInvoice"
        Me.Text = "Generate Invoice"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        CType(Me.dgvTreatments, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        Me.Panel3.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents Label1 As Label
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Panel3 As Panel
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents cbPatients As ComboBox
    Friend WithEvents Label2 As Label
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents dgvTreatments As DataGridView
    Friend WithEvents lblSelectedCount As Label
    Friend WithEvents GroupBox3 As GroupBox
    Friend WithEvents GroupBox4 As GroupBox
    Friend WithEvents lblPatientID As Label
    Friend WithEvents btnCancel As Button
    Friend WithEvents btnSave As Button
    Friend WithEvents btnPreview As Button
    Friend WithEvents txtNotes As TextBox
    Friend WithEvents Label10 As Label
    Friend WithEvents dtpDueDate As DateTimePicker
    Friend WithEvents Label9 As Label
    Friend WithEvents dtpInvoiceDate As DateTimePicker
    Friend WithEvents Label8 As Label
    Friend WithEvents lblInvoiceNumber As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents lblTotalAmount As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents lblTaxAmount As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents lblDiscount As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents lblSubTotal As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents btnViewInvoices As Button
    Friend WithEvents colSelect As DataGridViewCheckBoxColumn
    Friend WithEvents colTrtID As DataGridViewTextBoxColumn
    Friend WithEvents colDetail As DataGridViewTextBoxColumn
    Friend WithEvents colDate As DataGridViewTextBoxColumn
    Friend WithEvents colValue As DataGridViewTextBoxColumn
    Friend WithEvents colDiscount As DataGridViewTextBoxColumn
    Friend WithEvents colInvoiced As DataGridViewTextBoxColumn
End Class
