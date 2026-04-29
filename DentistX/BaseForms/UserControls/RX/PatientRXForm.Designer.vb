<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class PatientRXForm
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(PatientRXForm))
        Me.Ultra = New DevExpress.XtraEditors.MemoEdit()
        Me.PatientRXVIEWBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.MedicineGroupsBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.MedicineFamilyBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.MedScienceFamilyBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.MedicineItemsBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.MedicineShapeBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.MedicineDozeBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.grpList = New DevExpress.XtraEditors.ListBoxControl()
        Me.grpFamily = New DevExpress.XtraEditors.ListBoxControl()
        Me.grpScience = New DevExpress.XtraEditors.ListBoxControl()
        Me.grpItems = New DevExpress.XtraEditors.ListBoxControl()
        Me.grpShape = New DevExpress.XtraEditors.ListBoxControl()
        Me.grpDoze = New DevExpress.XtraEditors.ListBoxControl()
        Me.btnAddToRX = New DevExpress.XtraEditors.SimpleButton()
        Me.btNewRX = New DevExpress.XtraEditors.SimpleButton()
        Me.btSaveRX = New DevExpress.XtraEditors.SimpleButton()
        Me.btnPrintRX = New DevExpress.XtraEditors.SimpleButton()
        Me.btPrintFullRX = New DevExpress.XtraEditors.SimpleButton()
        Me.btnEditRxDetail = New DevExpress.XtraEditors.SimpleButton()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.Patient_RXBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.RxFlyBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.PatientrxBindingNavigator = New System.Windows.Forms.BindingNavigator(Me.components)
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
        Me.RxBodyBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.TV = New System.Windows.Forms.TreeView()
        CType(Me.Ultra.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PatientRXVIEWBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MedicineGroupsBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MedicineFamilyBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MedScienceFamilyBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MedicineItemsBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MedicineShapeBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MedicineDozeBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grpList, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grpFamily, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grpScience, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grpItems, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grpShape, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grpDoze, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Patient_RXBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RxFlyBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PatientrxBindingNavigator, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PatientrxBindingNavigator.SuspendLayout()
        CType(Me.RxBodyBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Ultra
        '
        Me.Ultra.AllowDrop = True
        resources.ApplyResources(Me.Ultra, "Ultra")
        Me.Ultra.Name = "Ultra"
        Me.Ultra.Properties.Appearance.Font = CType(resources.GetObject("Ultra.Properties.Appearance.Font"), System.Drawing.Font)
        Me.Ultra.Properties.Appearance.Options.UseFont = True
        '
        'PatientRXVIEWBindingSource
        '
        '
        'MedicineFamilyBindingSource
        '
        Me.MedicineFamilyBindingSource.DataSource = Me.MedicineGroupsBindingSource
        '
        'MedScienceFamilyBindingSource
        '
        Me.MedScienceFamilyBindingSource.DataSource = Me.MedicineFamilyBindingSource
        '
        'MedicineItemsBindingSource
        '
        Me.MedicineItemsBindingSource.DataSource = Me.MedScienceFamilyBindingSource
        '
        'MedicineShapeBindingSource
        '
        Me.MedicineShapeBindingSource.DataSource = Me.MedicineItemsBindingSource
        '
        'MedicineDozeBindingSource
        '
        Me.MedicineDozeBindingSource.DataSource = Me.MedicineShapeBindingSource
        '
        'grpList
        '
        resources.ApplyResources(Me.grpList, "grpList")
        Me.grpList.Appearance.Font = CType(resources.GetObject("grpList.Appearance.Font"), System.Drawing.Font)
        Me.grpList.Appearance.Options.UseFont = True
        Me.grpList.DataSource = Me.MedicineGroupsBindingSource
        Me.grpList.DisplayMember = "MedicineFamily"
        Me.grpList.Name = "grpList"
        Me.grpList.ValueMember = "MedicineSubCat"
        '
        'grpFamily
        '
        resources.ApplyResources(Me.grpFamily, "grpFamily")
        Me.grpFamily.Appearance.Font = CType(resources.GetObject("grpFamily.Appearance.Font"), System.Drawing.Font)
        Me.grpFamily.Appearance.Options.UseFont = True
        Me.grpFamily.DataSource = Me.MedicineFamilyBindingSource
        Me.grpFamily.DisplayMember = "MedicineSubCat"
        Me.grpFamily.Name = "grpFamily"
        Me.grpFamily.ValueMember = "SubCatID"
        '
        'grpScience
        '
        resources.ApplyResources(Me.grpScience, "grpScience")
        Me.grpScience.Appearance.Font = CType(resources.GetObject("grpScience.Appearance.Font"), System.Drawing.Font)
        Me.grpScience.Appearance.Options.UseFont = True
        Me.grpScience.DataSource = Me.MedScienceFamilyBindingSource
        Me.grpScience.DisplayMember = "ScienceName"
        Me.grpScience.Name = "grpScience"
        Me.grpScience.ValueMember = "ScincID"
        '
        'grpItems
        '
        resources.ApplyResources(Me.grpItems, "grpItems")
        Me.grpItems.Appearance.Font = CType(resources.GetObject("grpItems.Appearance.Font"), System.Drawing.Font)
        Me.grpItems.Appearance.Options.UseFont = True
        Me.grpItems.DataSource = Me.MedicineItemsBindingSource
        Me.grpItems.DisplayMember = "CommName"
        Me.grpItems.Name = "grpItems"
        Me.grpItems.ValueMember = "MedicineItemID"
        '
        'grpShape
        '
        resources.ApplyResources(Me.grpShape, "grpShape")
        Me.grpShape.Appearance.Font = CType(resources.GetObject("grpShape.Appearance.Font"), System.Drawing.Font)
        Me.grpShape.Appearance.Options.UseFont = True
        Me.grpShape.DataSource = Me.MedicineShapeBindingSource
        Me.grpShape.DisplayMember = "MedicineShape"
        Me.grpShape.Name = "grpShape"
        Me.grpShape.ValueMember = "ShapeID"
        '
        'grpDoze
        '
        resources.ApplyResources(Me.grpDoze, "grpDoze")
        Me.grpDoze.Appearance.Font = CType(resources.GetObject("grpDoze.Appearance.Font"), System.Drawing.Font)
        Me.grpDoze.Appearance.Options.UseFont = True
        Me.grpDoze.DataSource = Me.MedicineDozeBindingSource
        Me.grpDoze.DisplayMember = "Doze"
        Me.grpDoze.Name = "grpDoze"
        Me.grpDoze.ValueMember = "DozeID"
        '
        'btnAddToRX
        '
        resources.ApplyResources(Me.btnAddToRX, "btnAddToRX")
        Me.btnAddToRX.Appearance.Font = CType(resources.GetObject("btnAddToRX.Appearance.Font"), System.Drawing.Font)
        Me.btnAddToRX.Appearance.Options.UseFont = True
        Me.btnAddToRX.ImageOptions.ImageKey = resources.GetString("btnAddToRX.ImageOptions.ImageKey")
        Me.btnAddToRX.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleCenter
        Me.btnAddToRX.Name = "btnAddToRX"
        '
        'btNewRX
        '
        resources.ApplyResources(Me.btNewRX, "btNewRX")
        Me.btNewRX.Appearance.Font = CType(resources.GetObject("btNewRX.Appearance.Font"), System.Drawing.Font)
        Me.btNewRX.Appearance.Options.UseFont = True
        Me.btNewRX.ImageOptions.ImageKey = resources.GetString("btNewRX.ImageOptions.ImageKey")
        Me.btNewRX.Name = "btNewRX"
        '
        'btSaveRX
        '
        resources.ApplyResources(Me.btSaveRX, "btSaveRX")
        Me.btSaveRX.Appearance.Font = CType(resources.GetObject("btSaveRX.Appearance.Font"), System.Drawing.Font)
        Me.btSaveRX.Appearance.Options.UseFont = True
        Me.btSaveRX.ImageOptions.ImageKey = resources.GetString("btSaveRX.ImageOptions.ImageKey")
        Me.btSaveRX.Name = "btSaveRX"
        '
        'btnPrintRX
        '
        resources.ApplyResources(Me.btnPrintRX, "btnPrintRX")
        Me.btnPrintRX.Appearance.Font = CType(resources.GetObject("btnPrintRX.Appearance.Font"), System.Drawing.Font)
        Me.btnPrintRX.Appearance.Options.UseFont = True
        Me.btnPrintRX.ImageOptions.ImageKey = resources.GetString("btnPrintRX.ImageOptions.ImageKey")
        Me.btnPrintRX.Name = "btnPrintRX"
        '
        'btPrintFullRX
        '
        resources.ApplyResources(Me.btPrintFullRX, "btPrintFullRX")
        Me.btPrintFullRX.Appearance.Font = CType(resources.GetObject("btPrintFullRX.Appearance.Font"), System.Drawing.Font)
        Me.btPrintFullRX.Appearance.Options.UseFont = True
        Me.btPrintFullRX.ImageOptions.ImageKey = resources.GetString("btPrintFullRX.ImageOptions.ImageKey")
        Me.btPrintFullRX.Name = "btPrintFullRX"
        '
        'btnEditRxDetail
        '
        resources.ApplyResources(Me.btnEditRxDetail, "btnEditRxDetail")
        Me.btnEditRxDetail.Appearance.Font = CType(resources.GetObject("btnEditRxDetail.Appearance.Font"), System.Drawing.Font)
        Me.btnEditRxDetail.Appearance.Options.UseFont = True
        Me.btnEditRxDetail.ImageOptions.ImageKey = resources.GetString("btnEditRxDetail.ImageOptions.ImageKey")
        Me.btnEditRxDetail.Name = "btnEditRxDetail"
        '
        'LabelControl1
        '
        resources.ApplyResources(Me.LabelControl1, "LabelControl1")
        Me.LabelControl1.Appearance.Font = CType(resources.GetObject("LabelControl1.Appearance.Font"), System.Drawing.Font)
        Me.LabelControl1.Appearance.Options.UseFont = True
        Me.LabelControl1.Appearance.Options.UseTextOptions = True
        Me.LabelControl1.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.LabelControl1.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.LabelControl1.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MedicineItemsBindingSource, "Notes", True))
        Me.LabelControl1.Name = "LabelControl1"
        '
        'Patient_RXBindingSource
        '
        Me.Patient_RXBindingSource.DataMember = "Patient_RX"
        '
        'RxFlyBindingSource
        '
        Me.RxFlyBindingSource.DataMember = "RxFly"
        '
        'PatientrxBindingNavigator
        '
        resources.ApplyResources(Me.PatientrxBindingNavigator, "PatientrxBindingNavigator")
        Me.PatientrxBindingNavigator.AddNewItem = Nothing
        Me.PatientrxBindingNavigator.BindingSource = Me.PatientRXVIEWBindingSource
        Me.PatientrxBindingNavigator.CountItem = Me.ToolStripLabel1
        Me.PatientrxBindingNavigator.DeleteItem = Me.RxDel
        Me.PatientrxBindingNavigator.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.PatientrxBindingNavigator.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripButton3, Me.ToolStripButton4, Me.ToolStripSeparator1, Me.ToolStripTextBox1, Me.ToolStripLabel1, Me.ToolStripSeparator2, Me.ToolStripButton5, Me.ToolStripButton6, Me.ToolStripSeparator3, Me.RxDel, Me.RxSave})
        Me.PatientrxBindingNavigator.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow
        Me.PatientrxBindingNavigator.MoveFirstItem = Me.ToolStripButton3
        Me.PatientrxBindingNavigator.MoveLastItem = Me.ToolStripButton6
        Me.PatientrxBindingNavigator.MoveNextItem = Me.ToolStripButton5
        Me.PatientrxBindingNavigator.MovePreviousItem = Me.ToolStripButton4
        Me.PatientrxBindingNavigator.Name = "PatientrxBindingNavigator"
        Me.PatientrxBindingNavigator.PositionItem = Me.ToolStripTextBox1
        '
        'ToolStripLabel1
        '
        resources.ApplyResources(Me.ToolStripLabel1, "ToolStripLabel1")
        Me.ToolStripLabel1.Name = "ToolStripLabel1"
        '
        'RxDel
        '
        resources.ApplyResources(Me.RxDel, "RxDel")
        Me.RxDel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.RxDel.Name = "RxDel"
        '
        'ToolStripButton3
        '
        resources.ApplyResources(Me.ToolStripButton3, "ToolStripButton3")
        Me.ToolStripButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton3.Name = "ToolStripButton3"
        '
        'ToolStripButton4
        '
        resources.ApplyResources(Me.ToolStripButton4, "ToolStripButton4")
        Me.ToolStripButton4.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton4.Name = "ToolStripButton4"
        '
        'ToolStripSeparator1
        '
        resources.ApplyResources(Me.ToolStripSeparator1, "ToolStripSeparator1")
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        '
        'ToolStripTextBox1
        '
        resources.ApplyResources(Me.ToolStripTextBox1, "ToolStripTextBox1")
        Me.ToolStripTextBox1.Name = "ToolStripTextBox1"
        '
        'ToolStripSeparator2
        '
        resources.ApplyResources(Me.ToolStripSeparator2, "ToolStripSeparator2")
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        '
        'ToolStripButton5
        '
        resources.ApplyResources(Me.ToolStripButton5, "ToolStripButton5")
        Me.ToolStripButton5.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton5.Name = "ToolStripButton5"
        '
        'ToolStripButton6
        '
        resources.ApplyResources(Me.ToolStripButton6, "ToolStripButton6")
        Me.ToolStripButton6.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton6.Name = "ToolStripButton6"
        '
        'ToolStripSeparator3
        '
        resources.ApplyResources(Me.ToolStripSeparator3, "ToolStripSeparator3")
        Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
        '
        'RxSave
        '
        resources.ApplyResources(Me.RxSave, "RxSave")
        Me.RxSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.RxSave.Name = "RxSave"
        '
        'RxBodyBindingSource
        '
        Me.RxBodyBindingSource.DataMember = "RxBody"
        '
        'TV
        '
        resources.ApplyResources(Me.TV, "TV")
        Me.TV.AllowDrop = True
        Me.TV.FullRowSelect = True
        Me.TV.Name = "TV"
        Me.TV.ShowNodeToolTips = True
        '
        'PatientRXForm
        '
        resources.ApplyResources(Me, "$this")
        Me.Appearance.Options.UseFont = True
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.TV)
        Me.Controls.Add(Me.PatientrxBindingNavigator)
        Me.Controls.Add(Me.LabelControl1)
        Me.Controls.Add(Me.btnEditRxDetail)
        Me.Controls.Add(Me.btPrintFullRX)
        Me.Controls.Add(Me.btnPrintRX)
        Me.Controls.Add(Me.btSaveRX)
        Me.Controls.Add(Me.btNewRX)
        Me.Controls.Add(Me.btnAddToRX)
        Me.Controls.Add(Me.grpDoze)
        Me.Controls.Add(Me.grpShape)
        Me.Controls.Add(Me.grpItems)
        Me.Controls.Add(Me.grpScience)
        Me.Controls.Add(Me.grpFamily)
        Me.Controls.Add(Me.grpList)
        Me.Controls.Add(Me.Ultra)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "PatientRXForm"
        CType(Me.Ultra.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PatientRXVIEWBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MedicineGroupsBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MedicineFamilyBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MedScienceFamilyBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MedicineItemsBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MedicineShapeBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MedicineDozeBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grpList, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grpFamily, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grpScience, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grpItems, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grpShape, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grpDoze, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Patient_RXBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RxFlyBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PatientrxBindingNavigator, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PatientrxBindingNavigator.ResumeLayout(False)
        Me.PatientrxBindingNavigator.PerformLayout()
        CType(Me.RxBodyBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Ultra As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents MedicineGroupsBindingSource As Windows.Forms.BindingSource
    Friend WithEvents MedicineFamilyBindingSource As Windows.Forms.BindingSource
    Friend WithEvents MedScienceFamilyBindingSource As Windows.Forms.BindingSource
    Friend WithEvents MedicineItemsBindingSource As Windows.Forms.BindingSource
    Friend WithEvents MedicineShapeBindingSource As Windows.Forms.BindingSource
    Friend WithEvents MedicineDozeBindingSource As Windows.Forms.BindingSource
    Friend WithEvents grpList As DevExpress.XtraEditors.ListBoxControl
    Friend WithEvents grpFamily As DevExpress.XtraEditors.ListBoxControl
    Friend WithEvents grpScience As DevExpress.XtraEditors.ListBoxControl
    Friend WithEvents grpItems As DevExpress.XtraEditors.ListBoxControl
    Friend WithEvents grpShape As DevExpress.XtraEditors.ListBoxControl
    Friend WithEvents grpDoze As DevExpress.XtraEditors.ListBoxControl
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
End Class
