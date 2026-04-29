<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class PatientRXTreeFrm
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(PatientRXTreeFrm))
        Me.btnAddToRX = New DevExpress.XtraEditors.SimpleButton()
        Me.btNewRX = New DevExpress.XtraEditors.SimpleButton()
        Me.btSaveRX = New DevExpress.XtraEditors.SimpleButton()
        Me.btnPrintRX = New DevExpress.XtraEditors.SimpleButton()
        Me.btPrintFullRX = New DevExpress.XtraEditors.SimpleButton()
        Me.btnEditRxDetail = New DevExpress.XtraEditors.SimpleButton()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.MedicineItemsBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.MedScienceFamilyBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.MedicineFamilyBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.MedicineGroupsBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.PatientrxBindingNavigator = New System.Windows.Forms.BindingNavigator(Me.components)
        Me.PatientRXVIEWBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.ToolStripLabel1 = New System.Windows.Forms.ToolStripLabel()
        Me.RxDel = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButton3 = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButton4 = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripTextBox1 = New System.Windows.Forms.ToolStripTextBox()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripButton5 = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButton6 = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator()
        Me.RxSave = New System.Windows.Forms.ToolStripButton()
        Me.TV = New System.Windows.Forms.TreeView()
        Me.Ultra = New DevExpress.XtraEditors.MemoEdit()
        Me.Patient_RXBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.RxFlyBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.RxBodyBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.MedicineShapeBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.MedicineDozeBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl3 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl4 = New DevExpress.XtraEditors.LabelControl()
        CType(Me.MedicineItemsBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MedScienceFamilyBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MedicineFamilyBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MedicineGroupsBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PatientrxBindingNavigator, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PatientrxBindingNavigator.SuspendLayout()
        CType(Me.PatientRXVIEWBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Ultra.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Patient_RXBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RxFlyBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RxBodyBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MedicineShapeBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MedicineDozeBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnAddToRX
        '
        Me.btnAddToRX.Appearance.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Bold)
        Me.btnAddToRX.Appearance.Options.UseFont = True
        Me.btnAddToRX.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleCenter
        Me.btnAddToRX.Location = New System.Drawing.Point(640, 188)
        Me.btnAddToRX.Name = "btnAddToRX"
        Me.btnAddToRX.Size = New System.Drawing.Size(45, 34)
        Me.btnAddToRX.TabIndex = 6
        '
        'btNewRX
        '
        Me.btNewRX.Appearance.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Bold)
        Me.btNewRX.Appearance.Options.UseFont = True
        Me.btNewRX.Location = New System.Drawing.Point(813, 421)
        Me.btNewRX.Name = "btNewRX"
        Me.btNewRX.Size = New System.Drawing.Size(75, 23)
        Me.btNewRX.TabIndex = 6
        Me.btNewRX.Text = "New RX"
        '
        'btSaveRX
        '
        Me.btSaveRX.Appearance.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Bold)
        Me.btSaveRX.Appearance.Options.UseFont = True
        Me.btSaveRX.Location = New System.Drawing.Point(903, 421)
        Me.btSaveRX.Name = "btSaveRX"
        Me.btSaveRX.Size = New System.Drawing.Size(75, 23)
        Me.btSaveRX.TabIndex = 6
        Me.btSaveRX.Text = "Save RX"
        '
        'btnPrintRX
        '
        Me.btnPrintRX.Appearance.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Bold)
        Me.btnPrintRX.Appearance.Options.UseFont = True
        Me.btnPrintRX.Location = New System.Drawing.Point(1087, 393)
        Me.btnPrintRX.Name = "btnPrintRX"
        Me.btnPrintRX.Size = New System.Drawing.Size(88, 23)
        Me.btnPrintRX.TabIndex = 6
        Me.btnPrintRX.Text = "Print RX"
        '
        'btPrintFullRX
        '
        Me.btPrintFullRX.Appearance.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Bold)
        Me.btPrintFullRX.Appearance.Options.UseFont = True
        Me.btPrintFullRX.Location = New System.Drawing.Point(1087, 421)
        Me.btPrintFullRX.Name = "btPrintFullRX"
        Me.btPrintFullRX.Size = New System.Drawing.Size(88, 23)
        Me.btPrintFullRX.TabIndex = 6
        Me.btPrintFullRX.Text = "Print Full RX"
        '
        'btnEditRxDetail
        '
        Me.btnEditRxDetail.Appearance.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Bold)
        Me.btnEditRxDetail.Appearance.Options.UseFont = True
        Me.btnEditRxDetail.Location = New System.Drawing.Point(993, 421)
        Me.btnEditRxDetail.Name = "btnEditRxDetail"
        Me.btnEditRxDetail.Size = New System.Drawing.Size(88, 23)
        Me.btnEditRxDetail.TabIndex = 6
        Me.btnEditRxDetail.Text = "Edit Rx Detail"
        '
        'LabelControl1
        '
        Me.LabelControl1.Appearance.Font = New System.Drawing.Font("Calibri", 12.0!, System.Drawing.FontStyle.Bold)
        Me.LabelControl1.Appearance.Options.UseFont = True
        Me.LabelControl1.Appearance.Options.UseTextOptions = True
        Me.LabelControl1.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.LabelControl1.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.LabelControl1.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.LabelControl1.Location = New System.Drawing.Point(2, 393)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(795, 43)
        Me.LabelControl1.TabIndex = 7
        '
        'MedicineItemsBindingSource
        '
        Me.MedicineItemsBindingSource.DataSource = Me.MedScienceFamilyBindingSource
        '
        'MedScienceFamilyBindingSource
        '
        Me.MedScienceFamilyBindingSource.DataSource = Me.MedicineFamilyBindingSource
        '
        'MedicineFamilyBindingSource
        '
        Me.MedicineFamilyBindingSource.DataSource = Me.MedicineGroupsBindingSource
        '
        'PatientrxBindingNavigator
        '
        Me.PatientrxBindingNavigator.AddNewItem = Nothing
        Me.PatientrxBindingNavigator.AutoSize = False
        Me.PatientrxBindingNavigator.BindingSource = Me.PatientRXVIEWBindingSource
        Me.PatientrxBindingNavigator.CountItem = Me.ToolStripLabel1
        Me.PatientrxBindingNavigator.DeleteItem = Me.RxDel
        Me.PatientrxBindingNavigator.Dock = System.Windows.Forms.DockStyle.None
        Me.PatientrxBindingNavigator.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.PatientrxBindingNavigator.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripButton3, Me.ToolStripButton4, Me.ToolStripSeparator1, Me.ToolStripTextBox1, Me.ToolStripLabel1, Me.ToolStripSeparator2, Me.ToolStripButton5, Me.ToolStripButton6, Me.ToolStripSeparator3, Me.RxDel, Me.RxSave})
        Me.PatientrxBindingNavigator.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow
        Me.PatientrxBindingNavigator.Location = New System.Drawing.Point(802, 454)
        Me.PatientrxBindingNavigator.MoveFirstItem = Me.ToolStripButton3
        Me.PatientrxBindingNavigator.MoveLastItem = Me.ToolStripButton6
        Me.PatientrxBindingNavigator.MoveNextItem = Me.ToolStripButton5
        Me.PatientrxBindingNavigator.MovePreviousItem = Me.ToolStripButton4
        Me.PatientrxBindingNavigator.Name = "PatientrxBindingNavigator"
        Me.PatientrxBindingNavigator.PositionItem = Me.ToolStripTextBox1
        Me.PatientrxBindingNavigator.Size = New System.Drawing.Size(294, 28)
        Me.PatientrxBindingNavigator.TabIndex = 150
        Me.PatientrxBindingNavigator.Text = "PatientrxBindingNavigator"
        '
        'PatientRXVIEWBindingSource
        '
        '
        'ToolStripLabel1
        '
        Me.ToolStripLabel1.Name = "ToolStripLabel1"
        Me.ToolStripLabel1.Size = New System.Drawing.Size(35, 15)
        Me.ToolStripLabel1.Text = "of {0}"
        Me.ToolStripLabel1.ToolTipText = "Total number of items"
        '
        'RxDel
        '
        Me.RxDel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.RxDel.Image = CType(resources.GetObject("RxDel.Image"), System.Drawing.Image)
        Me.RxDel.Name = "RxDel"
        Me.RxDel.RightToLeftAutoMirrorImage = True
        Me.RxDel.Size = New System.Drawing.Size(23, 20)
        Me.RxDel.Text = "Delete"
        '
        'ToolStripButton3
        '
        Me.ToolStripButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton3.Image = CType(resources.GetObject("ToolStripButton3.Image"), System.Drawing.Image)
        Me.ToolStripButton3.Name = "ToolStripButton3"
        Me.ToolStripButton3.RightToLeftAutoMirrorImage = True
        Me.ToolStripButton3.Size = New System.Drawing.Size(23, 20)
        Me.ToolStripButton3.Text = "Move first"
        '
        'ToolStripButton4
        '
        Me.ToolStripButton4.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton4.Image = CType(resources.GetObject("ToolStripButton4.Image"), System.Drawing.Image)
        Me.ToolStripButton4.Name = "ToolStripButton4"
        Me.ToolStripButton4.RightToLeftAutoMirrorImage = True
        Me.ToolStripButton4.Size = New System.Drawing.Size(23, 20)
        Me.ToolStripButton4.Text = "Move previous"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(6, 23)
        '
        'ToolStripTextBox1
        '
        Me.ToolStripTextBox1.AccessibleName = "Position"
        Me.ToolStripTextBox1.AutoSize = False
        Me.ToolStripTextBox1.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.ToolStripTextBox1.Name = "ToolStripTextBox1"
        Me.ToolStripTextBox1.Size = New System.Drawing.Size(28, 23)
        Me.ToolStripTextBox1.Text = "0"
        Me.ToolStripTextBox1.ToolTipText = "Current position"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(6, 23)
        '
        'ToolStripButton5
        '
        Me.ToolStripButton5.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton5.Image = CType(resources.GetObject("ToolStripButton5.Image"), System.Drawing.Image)
        Me.ToolStripButton5.Name = "ToolStripButton5"
        Me.ToolStripButton5.RightToLeftAutoMirrorImage = True
        Me.ToolStripButton5.Size = New System.Drawing.Size(23, 20)
        Me.ToolStripButton5.Text = "Move next"
        '
        'ToolStripButton6
        '
        Me.ToolStripButton6.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton6.Image = CType(resources.GetObject("ToolStripButton6.Image"), System.Drawing.Image)
        Me.ToolStripButton6.Name = "ToolStripButton6"
        Me.ToolStripButton6.RightToLeftAutoMirrorImage = True
        Me.ToolStripButton6.Size = New System.Drawing.Size(23, 20)
        Me.ToolStripButton6.Text = "Move last"
        '
        'ToolStripSeparator3
        '
        Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
        Me.ToolStripSeparator3.Size = New System.Drawing.Size(6, 23)
        '
        'RxSave
        '
        Me.RxSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.RxSave.Image = CType(resources.GetObject("RxSave.Image"), System.Drawing.Image)
        Me.RxSave.Name = "RxSave"
        Me.RxSave.Size = New System.Drawing.Size(23, 20)
        Me.RxSave.Text = "Save Data"
        '
        'TV
        '
        Me.TV.AllowDrop = True
        Me.TV.Font = New System.Drawing.Font("Calibri", 11.0!, System.Drawing.FontStyle.Bold)
        Me.TV.FullRowSelect = True
        Me.TV.Location = New System.Drawing.Point(12, 8)
        Me.TV.Name = "TV"
        Me.TV.ShowNodeToolTips = True
        Me.TV.Size = New System.Drawing.Size(548, 367)
        Me.TV.TabIndex = 152
        '
        'Ultra
        '
        Me.Ultra.AllowDrop = True
        Me.Ultra.Location = New System.Drawing.Point(776, 12)
        Me.Ultra.Name = "Ultra"
        Me.Ultra.Properties.Appearance.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Bold)
        Me.Ultra.Properties.Appearance.Options.UseFont = True
        Me.Ultra.Size = New System.Drawing.Size(344, 375)
        Me.Ultra.TabIndex = 1
        '
        'MedicineShapeBindingSource
        '
        Me.MedicineShapeBindingSource.DataSource = Me.MedicineItemsBindingSource
        '
        'MedicineDozeBindingSource
        '
        Me.MedicineDozeBindingSource.DataSource = Me.MedicineShapeBindingSource
        '
        'LabelControl2
        '
        Me.LabelControl2.Appearance.Font = New System.Drawing.Font("Calibri", 12.0!, System.Drawing.FontStyle.Bold)
        Me.LabelControl2.Appearance.Options.UseFont = True
        Me.LabelControl2.Appearance.Options.UseTextOptions = True
        Me.LabelControl2.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.LabelControl2.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.LabelControl2.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.LabelControl2.Location = New System.Drawing.Point(190, 441)
        Me.LabelControl2.Name = "LabelControl2"
        Me.LabelControl2.Size = New System.Drawing.Size(504, 43)
        Me.LabelControl2.TabIndex = 7
        '
        'LabelControl3
        '
        Me.LabelControl3.Appearance.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Bold)
        Me.LabelControl3.Appearance.Options.UseFont = True
        Me.LabelControl3.Location = New System.Drawing.Point(90, 456)
        Me.LabelControl3.Name = "LabelControl3"
        Me.LabelControl3.Size = New System.Drawing.Size(79, 15)
        Me.LabelControl3.TabIndex = 153
        Me.LabelControl3.Text = "Patient Health"
        '
        'LabelControl4
        '
        Me.LabelControl4.Appearance.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Bold)
        Me.LabelControl4.Appearance.Options.UseFont = True
        Me.LabelControl4.Location = New System.Drawing.Point(90, 558)
        Me.LabelControl4.Name = "LabelControl4"
        Me.LabelControl4.Size = New System.Drawing.Size(86, 15)
        Me.LabelControl4.TabIndex = 153
        Me.LabelControl4.Text = "Medicine Notes"
        '
        'PatientRXTreeFrm
        '
        Me.Appearance.Options.UseFont = True
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoScroll = True
        Me.ClientSize = New System.Drawing.Size(1208, 622)
        Me.Controls.Add(Me.LabelControl4)
        Me.Controls.Add(Me.LabelControl3)
        Me.Controls.Add(Me.TV)
        Me.Controls.Add(Me.PatientrxBindingNavigator)
        Me.Controls.Add(Me.LabelControl2)
        Me.Controls.Add(Me.LabelControl1)
        Me.Controls.Add(Me.btnEditRxDetail)
        Me.Controls.Add(Me.btPrintFullRX)
        Me.Controls.Add(Me.btnPrintRX)
        Me.Controls.Add(Me.btSaveRX)
        Me.Controls.Add(Me.btNewRX)
        Me.Controls.Add(Me.btnAddToRX)
        Me.Controls.Add(Me.Ultra)
        Me.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Bold)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "PatientRXTreeFrm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Patient RX"
        CType(Me.MedicineItemsBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MedScienceFamilyBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MedicineFamilyBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MedicineGroupsBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PatientrxBindingNavigator, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PatientrxBindingNavigator.ResumeLayout(False)
        Me.PatientrxBindingNavigator.PerformLayout()
        CType(Me.PatientRXVIEWBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Ultra.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Patient_RXBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RxFlyBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RxBodyBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MedicineShapeBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MedicineDozeBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Ultra As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents btnAddToRX As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btNewRX As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btSaveRX As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnPrintRX As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btPrintFullRX As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnEditRxDetail As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents Patient_RXBindingSource As Windows.Forms.BindingSource
    Friend WithEvents PatientRXVIEWBindingSource As Windows.Forms.BindingSource
    Friend WithEvents RxFlyBindingSource As Windows.Forms.BindingSource
    Friend WithEvents PatientrxBindingNavigator As Windows.Forms.BindingNavigator
    Friend WithEvents ToolStripLabel1 As Windows.Forms.ToolStripLabel
    Friend WithEvents RxDel As Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripButton3 As Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripButton4 As Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator1 As Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripTextBox1 As Windows.Forms.ToolStripTextBox
    Friend WithEvents ToolStripSeparator2 As Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripButton5 As Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripButton6 As Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator3 As Windows.Forms.ToolStripSeparator
    Friend WithEvents RxSave As Windows.Forms.ToolStripButton
    Friend WithEvents RxBodyBindingSource As Windows.Forms.BindingSource
    Friend WithEvents TV As Windows.Forms.TreeView
    Friend WithEvents MedicineFamilyBindingSource As Windows.Forms.BindingSource
    Friend WithEvents MedicineGroupsBindingSource As Windows.Forms.BindingSource
    Friend WithEvents MedScienceFamilyBindingSource As Windows.Forms.BindingSource
    Friend WithEvents MedicineItemsBindingSource As Windows.Forms.BindingSource
    Friend WithEvents MedicineShapeBindingSource As Windows.Forms.BindingSource
    Friend WithEvents MedicineDozeBindingSource As Windows.Forms.BindingSource
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl3 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl4 As DevExpress.XtraEditors.LabelControl
End Class
