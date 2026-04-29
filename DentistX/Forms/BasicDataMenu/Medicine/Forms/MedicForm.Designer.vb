<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MedicForm
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MedicForm))
        Me.PanelControl1 = New DevExpress.XtraEditors.PanelControl()
        Me.GroupControl2 = New DevExpress.XtraEditors.GroupControl()
        Me.MedicineFamilyCombo1 = New DentistX.MedicineFamilyCombo()
        Me.FamilyNavigator = New System.Windows.Forms.BindingNavigator(Me.components)
        Me.MedicineFamilyBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.MedicineGroupsBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.ToolStripLabel4 = New System.Windows.Forms.ToolStripLabel()
        Me.FamlyNavigatorDeleteItem = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButton20 = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButton21 = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator10 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripTextBox4 = New System.Windows.Forms.ToolStripTextBox()
        Me.ToolStripSeparator11 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripButton22 = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButton23 = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator12 = New System.Windows.Forms.ToolStripSeparator()
        Me.FamlyNavigatorSaveItem = New System.Windows.Forms.ToolStripButton()
        Me.gridMedFamily = New DevExpress.XtraGrid.GridControl()
        Me.GridView2 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.btnDelMedFamily = New DevExpress.XtraEditors.SimpleButton()
        Me.btnEditMedFamily = New DevExpress.XtraEditors.SimpleButton()
        Me.cboMedGroup = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.btnAddMedFamily = New DevExpress.XtraEditors.SimpleButton()
        Me.txtMedFamily = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl3 = New DevExpress.XtraEditors.LabelControl()
        Me.GroupControl1 = New DevExpress.XtraEditors.GroupControl()
        Me.MedicineGroupsCombo1 = New DentistX.MedicineGroupsCombo()
        Me.GrpNavigator = New System.Windows.Forms.BindingNavigator(Me.components)
        Me.BindingNavigatorCountItem = New System.Windows.Forms.ToolStripLabel()
        Me.GrpNavigatorDeleteItem = New System.Windows.Forms.ToolStripButton()
        Me.BindingNavigatorMoveFirstItem = New System.Windows.Forms.ToolStripButton()
        Me.BindingNavigatorMovePreviousItem = New System.Windows.Forms.ToolStripButton()
        Me.BindingNavigatorSeparator = New System.Windows.Forms.ToolStripSeparator()
        Me.BindingNavigatorPositionItem = New System.Windows.Forms.ToolStripTextBox()
        Me.BindingNavigatorSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.BindingNavigatorMoveNextItem = New System.Windows.Forms.ToolStripButton()
        Me.BindingNavigatorMoveLastItem = New System.Windows.Forms.ToolStripButton()
        Me.BindingNavigatorSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.GrpNavigatorSaveItem = New System.Windows.Forms.ToolStripButton()
        Me.gridMedGroups = New DevExpress.XtraGrid.GridControl()
        Me.GridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.btnDelMedGroup = New DevExpress.XtraEditors.SimpleButton()
        Me.btnEditMedGroup = New DevExpress.XtraEditors.SimpleButton()
        Me.btnAddMedGroup = New DevExpress.XtraEditors.SimpleButton()
        Me.txtMedicenGroup = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.PanelControl2 = New DevExpress.XtraEditors.PanelControl()
        Me.GroupControl6 = New DevExpress.XtraEditors.GroupControl()
        Me.MedicineDozeCombo1 = New DentistX.MedicineDozeCombo()
        Me.DoseNavigator = New System.Windows.Forms.BindingNavigator(Me.components)
        Me.MedicineDozeBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.MedicineShapeBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.MedicineItemsBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.ScienceFamilyBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.ToolStripLabel3 = New System.Windows.Forms.ToolStripLabel()
        Me.DoseNavigatorDeleteItem = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButton14 = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButton15 = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator7 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripTextBox3 = New System.Windows.Forms.ToolStripTextBox()
        Me.ToolStripSeparator8 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripButton16 = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButton17 = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator9 = New System.Windows.Forms.ToolStripSeparator()
        Me.DoseNavigatorSaveItem = New System.Windows.Forms.ToolStripButton()
        Me.gridMedDose = New DevExpress.XtraGrid.GridControl()
        Me.GridView6 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.btnDelDose = New DevExpress.XtraEditors.SimpleButton()
        Me.cboShapeInfo = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.btnEditDose = New DevExpress.XtraEditors.SimpleButton()
        Me.LabelControl13 = New DevExpress.XtraEditors.LabelControl()
        Me.TextEdit6 = New DevExpress.XtraEditors.TextEdit()
        Me.btnAddDose = New DevExpress.XtraEditors.SimpleButton()
        Me.LabelControl14 = New DevExpress.XtraEditors.LabelControl()
        Me.GroupControl5 = New DevExpress.XtraEditors.GroupControl()
        Me.MedicineShapeCombo1 = New DentistX.MedicineShapeCombo()
        Me.ShapeNavigator = New System.Windows.Forms.BindingNavigator(Me.components)
        Me.ToolStripLabel2 = New System.Windows.Forms.ToolStripLabel()
        Me.ShapeNavigatorDeleteItem = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButton8 = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButton9 = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripTextBox2 = New System.Windows.Forms.ToolStripTextBox()
        Me.ToolStripSeparator5 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripButton10 = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButton11 = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator6 = New System.Windows.Forms.ToolStripSeparator()
        Me.ShapeNavigatorSaveItem = New System.Windows.Forms.ToolStripButton()
        Me.gridMedShape = New DevExpress.XtraGrid.GridControl()
        Me.GridView5 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.btnDelShape = New DevExpress.XtraEditors.SimpleButton()
        Me.btnAddShape = New DevExpress.XtraEditors.SimpleButton()
        Me.cboMedItem = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.LabelControl10 = New DevExpress.XtraEditors.LabelControl()
        Me.btnEditShape = New DevExpress.XtraEditors.SimpleButton()
        Me.txtShapeInfo = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl12 = New DevExpress.XtraEditors.LabelControl()
        Me.txtMedicineShape = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl11 = New DevExpress.XtraEditors.LabelControl()
        Me.SidePanel1 = New DevExpress.XtraEditors.SidePanel()
        Me.GroupControl4 = New DevExpress.XtraEditors.GroupControl()
        Me.MedicineItemsCombo1 = New DentistX.MedicineItemsCombo()
        Me.ItemsNavigator = New System.Windows.Forms.BindingNavigator(Me.components)
        Me.ToolStripLabel5 = New System.Windows.Forms.ToolStripLabel()
        Me.ItemsNavigatorDeleteItem = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButton26 = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButton27 = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator13 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripTextBox5 = New System.Windows.Forms.ToolStripTextBox()
        Me.ToolStripSeparator14 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripButton28 = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButton29 = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator15 = New System.Windows.Forms.ToolStripSeparator()
        Me.ItemsNavigatorSaveItem = New System.Windows.Forms.ToolStripButton()
        Me.gridMedItems = New DevExpress.XtraGrid.GridControl()
        Me.GridView4 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.btnDelScinetificName = New DevExpress.XtraEditors.SimpleButton()
        Me.txtNotes = New DevExpress.XtraEditors.TextEdit()
        Me.txtCompany = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl6 = New DevExpress.XtraEditors.LabelControl()
        Me.txtCommon = New DevExpress.XtraEditors.TextEdit()
        Me.btnAddScinetificName = New DevExpress.XtraEditors.SimpleButton()
        Me.LabelControl9 = New DevExpress.XtraEditors.LabelControl()
        Me.btnEditScinetificName = New DevExpress.XtraEditors.SimpleButton()
        Me.LabelControl8 = New DevExpress.XtraEditors.LabelControl()
        Me.cboDoseInfo = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.cboScinetificName = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.LabelControl7 = New DevExpress.XtraEditors.LabelControl()
        Me.GroupControl3 = New DevExpress.XtraEditors.GroupControl()
        Me.MedScienceFamilyCombo1 = New DentistX.MedScienceFamilyCombo()
        Me.ScienceNavigator = New System.Windows.Forms.BindingNavigator(Me.components)
        Me.ToolStripLabel1 = New System.Windows.Forms.ToolStripLabel()
        Me.SciencNavigatorDeleteItem = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButton2 = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButton3 = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripTextBox1 = New System.Windows.Forms.ToolStripTextBox()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripButton4 = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButton5 = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator()
        Me.SciencNavigatorSaveItem = New System.Windows.Forms.ToolStripButton()
        Me.gridMedScinFamily = New DevExpress.XtraGrid.GridControl()
        Me.GridView3 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.btnDelMedScienFamily = New DevExpress.XtraEditors.SimpleButton()
        Me.btnAddMedScienFamily = New DevExpress.XtraEditors.SimpleButton()
        Me.btnEditMedScienFamily = New DevExpress.XtraEditors.SimpleButton()
        Me.LabelControl4 = New DevExpress.XtraEditors.LabelControl()
        Me.cboMedFamily = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.txtScienceName = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl5 = New DevExpress.XtraEditors.LabelControl()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl1.SuspendLayout()
        CType(Me.GroupControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl2.SuspendLayout()
        CType(Me.FamilyNavigator, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.FamilyNavigator.SuspendLayout()
        CType(Me.MedicineFamilyBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MedicineGroupsBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gridMedFamily, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboMedGroup.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtMedFamily.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl1.SuspendLayout()
        CType(Me.GrpNavigator, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GrpNavigator.SuspendLayout()
        CType(Me.gridMedGroups, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtMedicenGroup.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PanelControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl2.SuspendLayout()
        CType(Me.GroupControl6, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl6.SuspendLayout()
        CType(Me.DoseNavigator, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.DoseNavigator.SuspendLayout()
        CType(Me.MedicineDozeBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MedicineShapeBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MedicineItemsBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ScienceFamilyBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gridMedDose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboShapeInfo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TextEdit6.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupControl5, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl5.SuspendLayout()
        CType(Me.ShapeNavigator, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ShapeNavigator.SuspendLayout()
        CType(Me.gridMedShape, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboMedItem.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtShapeInfo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtMedicineShape.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SidePanel1.SuspendLayout()
        CType(Me.GroupControl4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl4.SuspendLayout()
        CType(Me.ItemsNavigator, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ItemsNavigator.SuspendLayout()
        CType(Me.gridMedItems, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNotes.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCompany.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCommon.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboDoseInfo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboScinetificName.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupControl3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl3.SuspendLayout()
        CType(Me.ScienceNavigator, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ScienceNavigator.SuspendLayout()
        CType(Me.gridMedScinFamily, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboMedFamily.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtScienceName.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'PanelControl1
        '
        Me.PanelControl1.Controls.Add(Me.GroupControl2)
        Me.PanelControl1.Controls.Add(Me.GroupControl1)
        Me.PanelControl1.Dock = System.Windows.Forms.DockStyle.Top
        Me.PanelControl1.Location = New System.Drawing.Point(0, 0)
        Me.PanelControl1.Name = "PanelControl1"
        Me.PanelControl1.Size = New System.Drawing.Size(1360, 245)
        Me.PanelControl1.TabIndex = 0
        '
        'GroupControl2
        '
        Me.GroupControl2.Controls.Add(Me.MedicineFamilyCombo1)
        Me.GroupControl2.Controls.Add(Me.FamilyNavigator)
        Me.GroupControl2.Controls.Add(Me.gridMedFamily)
        Me.GroupControl2.Controls.Add(Me.btnDelMedFamily)
        Me.GroupControl2.Controls.Add(Me.btnEditMedFamily)
        Me.GroupControl2.Controls.Add(Me.cboMedGroup)
        Me.GroupControl2.Controls.Add(Me.btnAddMedFamily)
        Me.GroupControl2.Controls.Add(Me.txtMedFamily)
        Me.GroupControl2.Controls.Add(Me.LabelControl2)
        Me.GroupControl2.Controls.Add(Me.LabelControl3)
        Me.GroupControl2.Dock = System.Windows.Forms.DockStyle.Right
        Me.GroupControl2.Location = New System.Drawing.Point(655, 2)
        Me.GroupControl2.Name = "GroupControl2"
        Me.GroupControl2.Size = New System.Drawing.Size(703, 241)
        Me.GroupControl2.TabIndex = 1
        Me.GroupControl2.Text = "Medicine Family"
        '
        'MedicineFamilyCombo1
        '
        Me.MedicineFamilyCombo1.Location = New System.Drawing.Point(112, 116)
        Me.MedicineFamilyCombo1.MedicineID = 0
        Me.MedicineFamilyCombo1.MedicineSubCat = Nothing
        Me.MedicineFamilyCombo1.Name = "MedicineFamilyCombo1"
        Me.MedicineFamilyCombo1.ParentMedicineID = -1
        Me.MedicineFamilyCombo1.Size = New System.Drawing.Size(150, 22)
        Me.MedicineFamilyCombo1.SubCatID = 0
        Me.MedicineFamilyCombo1.TabIndex = 21
        '
        'FamilyNavigator
        '
        Me.FamilyNavigator.AddNewItem = Nothing
        Me.FamilyNavigator.AutoSize = False
        Me.FamilyNavigator.BindingSource = Me.MedicineFamilyBindingSource
        Me.FamilyNavigator.CountItem = Me.ToolStripLabel4
        Me.FamilyNavigator.DeleteItem = Me.FamlyNavigatorDeleteItem
        Me.FamilyNavigator.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.FamilyNavigator.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripButton20, Me.ToolStripButton21, Me.ToolStripSeparator10, Me.ToolStripTextBox4, Me.ToolStripLabel4, Me.ToolStripSeparator11, Me.ToolStripButton22, Me.ToolStripButton23, Me.ToolStripSeparator12, Me.FamlyNavigatorDeleteItem, Me.FamlyNavigatorSaveItem})
        Me.FamilyNavigator.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow
        Me.FamilyNavigator.Location = New System.Drawing.Point(2, 215)
        Me.FamilyNavigator.MoveFirstItem = Me.ToolStripButton20
        Me.FamilyNavigator.MoveLastItem = Me.ToolStripButton23
        Me.FamilyNavigator.MoveNextItem = Me.ToolStripButton22
        Me.FamilyNavigator.MovePreviousItem = Me.ToolStripButton21
        Me.FamilyNavigator.Name = "FamilyNavigator"
        Me.FamilyNavigator.PositionItem = Me.ToolStripTextBox4
        Me.FamilyNavigator.Size = New System.Drawing.Size(299, 24)
        Me.FamilyNavigator.TabIndex = 20
        Me.FamilyNavigator.Text = "BindingNavigator1"
        Me.FamilyNavigator.Visible = False
        '
        'MedicineFamilyBindingSource
        '
        Me.MedicineFamilyBindingSource.DataSource = Me.MedicineGroupsBindingSource
        '
        'MedicineGroupsBindingSource
        '
        Me.MedicineGroupsBindingSource.DataMember = "MedicineGroups"
        '
        'ToolStripLabel4
        '
        Me.ToolStripLabel4.Name = "ToolStripLabel4"
        Me.ToolStripLabel4.Size = New System.Drawing.Size(35, 15)
        Me.ToolStripLabel4.Text = "of {0}"
        Me.ToolStripLabel4.ToolTipText = "Total number of items"
        '
        'FamlyNavigatorDeleteItem
        '
        Me.FamlyNavigatorDeleteItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.FamlyNavigatorDeleteItem.Image = CType(resources.GetObject("FamlyNavigatorDeleteItem.Image"), System.Drawing.Image)
        Me.FamlyNavigatorDeleteItem.Name = "FamlyNavigatorDeleteItem"
        Me.FamlyNavigatorDeleteItem.RightToLeftAutoMirrorImage = True
        Me.FamlyNavigatorDeleteItem.Size = New System.Drawing.Size(23, 20)
        Me.FamlyNavigatorDeleteItem.Text = "Delete"
        '
        'ToolStripButton20
        '
        Me.ToolStripButton20.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton20.Image = CType(resources.GetObject("ToolStripButton20.Image"), System.Drawing.Image)
        Me.ToolStripButton20.Name = "ToolStripButton20"
        Me.ToolStripButton20.RightToLeftAutoMirrorImage = True
        Me.ToolStripButton20.Size = New System.Drawing.Size(23, 20)
        Me.ToolStripButton20.Text = "Move first"
        '
        'ToolStripButton21
        '
        Me.ToolStripButton21.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton21.Image = CType(resources.GetObject("ToolStripButton21.Image"), System.Drawing.Image)
        Me.ToolStripButton21.Name = "ToolStripButton21"
        Me.ToolStripButton21.RightToLeftAutoMirrorImage = True
        Me.ToolStripButton21.Size = New System.Drawing.Size(23, 20)
        Me.ToolStripButton21.Text = "Move previous"
        '
        'ToolStripSeparator10
        '
        Me.ToolStripSeparator10.Name = "ToolStripSeparator10"
        Me.ToolStripSeparator10.Size = New System.Drawing.Size(6, 23)
        '
        'ToolStripTextBox4
        '
        Me.ToolStripTextBox4.AccessibleName = "Position"
        Me.ToolStripTextBox4.AutoSize = False
        Me.ToolStripTextBox4.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.ToolStripTextBox4.Name = "ToolStripTextBox4"
        Me.ToolStripTextBox4.Size = New System.Drawing.Size(34, 23)
        Me.ToolStripTextBox4.Text = "0"
        Me.ToolStripTextBox4.ToolTipText = "Current position"
        '
        'ToolStripSeparator11
        '
        Me.ToolStripSeparator11.Name = "ToolStripSeparator11"
        Me.ToolStripSeparator11.Size = New System.Drawing.Size(6, 23)
        '
        'ToolStripButton22
        '
        Me.ToolStripButton22.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton22.Image = CType(resources.GetObject("ToolStripButton22.Image"), System.Drawing.Image)
        Me.ToolStripButton22.Name = "ToolStripButton22"
        Me.ToolStripButton22.RightToLeftAutoMirrorImage = True
        Me.ToolStripButton22.Size = New System.Drawing.Size(23, 20)
        Me.ToolStripButton22.Text = "Move next"
        '
        'ToolStripButton23
        '
        Me.ToolStripButton23.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton23.Image = CType(resources.GetObject("ToolStripButton23.Image"), System.Drawing.Image)
        Me.ToolStripButton23.Name = "ToolStripButton23"
        Me.ToolStripButton23.RightToLeftAutoMirrorImage = True
        Me.ToolStripButton23.Size = New System.Drawing.Size(23, 20)
        Me.ToolStripButton23.Text = "Move last"
        '
        'ToolStripSeparator12
        '
        Me.ToolStripSeparator12.Name = "ToolStripSeparator12"
        Me.ToolStripSeparator12.Size = New System.Drawing.Size(6, 23)
        '
        'FamlyNavigatorSaveItem
        '
        Me.FamlyNavigatorSaveItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.FamlyNavigatorSaveItem.Image = CType(resources.GetObject("FamlyNavigatorSaveItem.Image"), System.Drawing.Image)
        Me.FamlyNavigatorSaveItem.Name = "FamlyNavigatorSaveItem"
        Me.FamlyNavigatorSaveItem.Size = New System.Drawing.Size(23, 20)
        Me.FamlyNavigatorSaveItem.Text = "Save Data"
        '
        'gridMedFamily
        '
        Me.gridMedFamily.DataSource = Me.MedicineFamilyBindingSource
        Me.gridMedFamily.Dock = System.Windows.Forms.DockStyle.Right
        Me.gridMedFamily.Location = New System.Drawing.Point(301, 22)
        Me.gridMedFamily.MainView = Me.GridView2
        Me.gridMedFamily.Name = "gridMedFamily"
        Me.gridMedFamily.Size = New System.Drawing.Size(400, 217)
        Me.gridMedFamily.TabIndex = 3
        Me.gridMedFamily.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridView2})
        '
        'GridView2
        '
        Me.GridView2.GridControl = Me.gridMedFamily
        Me.GridView2.Name = "GridView2"
        '
        'btnDelMedFamily
        '
        Me.btnDelMedFamily.Appearance.Font = New System.Drawing.Font("Calibri", 10.0!, System.Drawing.FontStyle.Bold)
        Me.btnDelMedFamily.Appearance.Options.UseFont = True
        Me.btnDelMedFamily.Location = New System.Drawing.Point(12, 173)
        Me.btnDelMedFamily.Name = "btnDelMedFamily"
        Me.btnDelMedFamily.Size = New System.Drawing.Size(75, 23)
        Me.btnDelMedFamily.TabIndex = 2
        Me.btnDelMedFamily.Text = "Delete"
        '
        'btnEditMedFamily
        '
        Me.btnEditMedFamily.Appearance.Font = New System.Drawing.Font("Calibri", 10.0!, System.Drawing.FontStyle.Bold)
        Me.btnEditMedFamily.Appearance.Options.UseFont = True
        Me.btnEditMedFamily.Location = New System.Drawing.Point(12, 144)
        Me.btnEditMedFamily.Name = "btnEditMedFamily"
        Me.btnEditMedFamily.Size = New System.Drawing.Size(75, 23)
        Me.btnEditMedFamily.TabIndex = 2
        Me.btnEditMedFamily.Text = "Edit"
        '
        'cboMedGroup
        '
        Me.cboMedGroup.Location = New System.Drawing.Point(112, 46)
        Me.cboMedGroup.Name = "cboMedGroup"
        Me.cboMedGroup.Properties.Appearance.Font = New System.Drawing.Font("Calibri", 10.0!, System.Drawing.FontStyle.Bold)
        Me.cboMedGroup.Properties.Appearance.Options.UseFont = True
        Me.cboMedGroup.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cboMedGroup.Size = New System.Drawing.Size(186, 22)
        Me.cboMedGroup.TabIndex = 3
        '
        'btnAddMedFamily
        '
        Me.btnAddMedFamily.Appearance.Font = New System.Drawing.Font("Calibri", 10.0!, System.Drawing.FontStyle.Bold)
        Me.btnAddMedFamily.Appearance.Options.UseFont = True
        Me.btnAddMedFamily.Location = New System.Drawing.Point(12, 115)
        Me.btnAddMedFamily.Name = "btnAddMedFamily"
        Me.btnAddMedFamily.Size = New System.Drawing.Size(75, 23)
        Me.btnAddMedFamily.TabIndex = 2
        Me.btnAddMedFamily.Text = "Add"
        '
        'txtMedFamily
        '
        Me.txtMedFamily.Location = New System.Drawing.Point(112, 81)
        Me.txtMedFamily.Name = "txtMedFamily"
        Me.txtMedFamily.Properties.Appearance.Font = New System.Drawing.Font("Calibri", 10.0!, System.Drawing.FontStyle.Bold)
        Me.txtMedFamily.Properties.Appearance.Options.UseFont = True
        Me.txtMedFamily.Size = New System.Drawing.Size(186, 22)
        Me.txtMedFamily.TabIndex = 1
        '
        'LabelControl2
        '
        Me.LabelControl2.Appearance.Font = New System.Drawing.Font("Calibri", 10.0!, System.Drawing.FontStyle.Bold)
        Me.LabelControl2.Appearance.Options.UseFont = True
        Me.LabelControl2.Location = New System.Drawing.Point(14, 49)
        Me.LabelControl2.Name = "LabelControl2"
        Me.LabelControl2.Size = New System.Drawing.Size(87, 15)
        Me.LabelControl2.TabIndex = 0
        Me.LabelControl2.Text = "Medicine Group"
        '
        'LabelControl3
        '
        Me.LabelControl3.Appearance.Font = New System.Drawing.Font("Calibri", 10.0!, System.Drawing.FontStyle.Bold)
        Me.LabelControl3.Appearance.Options.UseFont = True
        Me.LabelControl3.Location = New System.Drawing.Point(14, 84)
        Me.LabelControl3.Name = "LabelControl3"
        Me.LabelControl3.Size = New System.Drawing.Size(88, 15)
        Me.LabelControl3.TabIndex = 0
        Me.LabelControl3.Text = "Medicine Family"
        '
        'GroupControl1
        '
        Me.GroupControl1.Controls.Add(Me.MedicineGroupsCombo1)
        Me.GroupControl1.Controls.Add(Me.GrpNavigator)
        Me.GroupControl1.Controls.Add(Me.gridMedGroups)
        Me.GroupControl1.Controls.Add(Me.btnDelMedGroup)
        Me.GroupControl1.Controls.Add(Me.btnEditMedGroup)
        Me.GroupControl1.Controls.Add(Me.btnAddMedGroup)
        Me.GroupControl1.Controls.Add(Me.txtMedicenGroup)
        Me.GroupControl1.Controls.Add(Me.LabelControl1)
        Me.GroupControl1.Dock = System.Windows.Forms.DockStyle.Left
        Me.GroupControl1.Location = New System.Drawing.Point(2, 2)
        Me.GroupControl1.Name = "GroupControl1"
        Me.GroupControl1.Size = New System.Drawing.Size(654, 241)
        Me.GroupControl1.TabIndex = 0
        Me.GroupControl1.Text = "Medicine Groups"
        '
        'MedicineGroupsCombo1
        '
        Me.MedicineGroupsCombo1.Location = New System.Drawing.Point(98, 92)
        Me.MedicineGroupsCombo1.MedicineFamily = Nothing
        Me.MedicineGroupsCombo1.MedicineID = 0
        Me.MedicineGroupsCombo1.Name = "MedicineGroupsCombo1"
        Me.MedicineGroupsCombo1.Size = New System.Drawing.Size(150, 22)
        Me.MedicineGroupsCombo1.TabIndex = 16
        '
        'GrpNavigator
        '
        Me.GrpNavigator.AddNewItem = Nothing
        Me.GrpNavigator.AutoSize = False
        Me.GrpNavigator.BindingSource = Me.MedicineGroupsBindingSource
        Me.GrpNavigator.CountItem = Me.BindingNavigatorCountItem
        Me.GrpNavigator.DeleteItem = Me.GrpNavigatorDeleteItem
        Me.GrpNavigator.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.GrpNavigator.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.BindingNavigatorMoveFirstItem, Me.BindingNavigatorMovePreviousItem, Me.BindingNavigatorSeparator, Me.BindingNavigatorPositionItem, Me.BindingNavigatorCountItem, Me.BindingNavigatorSeparator1, Me.BindingNavigatorMoveNextItem, Me.BindingNavigatorMoveLastItem, Me.BindingNavigatorSeparator2, Me.GrpNavigatorDeleteItem, Me.GrpNavigatorSaveItem})
        Me.GrpNavigator.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow
        Me.GrpNavigator.Location = New System.Drawing.Point(2, 215)
        Me.GrpNavigator.MoveFirstItem = Me.BindingNavigatorMoveFirstItem
        Me.GrpNavigator.MoveLastItem = Me.BindingNavigatorMoveLastItem
        Me.GrpNavigator.MoveNextItem = Me.BindingNavigatorMoveNextItem
        Me.GrpNavigator.MovePreviousItem = Me.BindingNavigatorMovePreviousItem
        Me.GrpNavigator.Name = "GrpNavigator"
        Me.GrpNavigator.PositionItem = Me.BindingNavigatorPositionItem
        Me.GrpNavigator.Size = New System.Drawing.Size(250, 24)
        Me.GrpNavigator.TabIndex = 15
        Me.GrpNavigator.Text = "BindingNavigator1"
        Me.GrpNavigator.Visible = False
        '
        'BindingNavigatorCountItem
        '
        Me.BindingNavigatorCountItem.Name = "BindingNavigatorCountItem"
        Me.BindingNavigatorCountItem.Size = New System.Drawing.Size(35, 15)
        Me.BindingNavigatorCountItem.Text = "of {0}"
        Me.BindingNavigatorCountItem.ToolTipText = "Total number of items"
        '
        'GrpNavigatorDeleteItem
        '
        Me.GrpNavigatorDeleteItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.GrpNavigatorDeleteItem.Image = CType(resources.GetObject("GrpNavigatorDeleteItem.Image"), System.Drawing.Image)
        Me.GrpNavigatorDeleteItem.Name = "GrpNavigatorDeleteItem"
        Me.GrpNavigatorDeleteItem.RightToLeftAutoMirrorImage = True
        Me.GrpNavigatorDeleteItem.Size = New System.Drawing.Size(23, 20)
        Me.GrpNavigatorDeleteItem.Text = "Delete"
        '
        'BindingNavigatorMoveFirstItem
        '
        Me.BindingNavigatorMoveFirstItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.BindingNavigatorMoveFirstItem.Image = CType(resources.GetObject("BindingNavigatorMoveFirstItem.Image"), System.Drawing.Image)
        Me.BindingNavigatorMoveFirstItem.Name = "BindingNavigatorMoveFirstItem"
        Me.BindingNavigatorMoveFirstItem.RightToLeftAutoMirrorImage = True
        Me.BindingNavigatorMoveFirstItem.Size = New System.Drawing.Size(23, 20)
        Me.BindingNavigatorMoveFirstItem.Text = "Move first"
        '
        'BindingNavigatorMovePreviousItem
        '
        Me.BindingNavigatorMovePreviousItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.BindingNavigatorMovePreviousItem.Image = CType(resources.GetObject("BindingNavigatorMovePreviousItem.Image"), System.Drawing.Image)
        Me.BindingNavigatorMovePreviousItem.Name = "BindingNavigatorMovePreviousItem"
        Me.BindingNavigatorMovePreviousItem.RightToLeftAutoMirrorImage = True
        Me.BindingNavigatorMovePreviousItem.Size = New System.Drawing.Size(23, 20)
        Me.BindingNavigatorMovePreviousItem.Text = "Move previous"
        '
        'BindingNavigatorSeparator
        '
        Me.BindingNavigatorSeparator.Name = "BindingNavigatorSeparator"
        Me.BindingNavigatorSeparator.Size = New System.Drawing.Size(6, 23)
        '
        'BindingNavigatorPositionItem
        '
        Me.BindingNavigatorPositionItem.AccessibleName = "Position"
        Me.BindingNavigatorPositionItem.AutoSize = False
        Me.BindingNavigatorPositionItem.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.BindingNavigatorPositionItem.Name = "BindingNavigatorPositionItem"
        Me.BindingNavigatorPositionItem.Size = New System.Drawing.Size(34, 23)
        Me.BindingNavigatorPositionItem.Text = "0"
        Me.BindingNavigatorPositionItem.ToolTipText = "Current position"
        '
        'BindingNavigatorSeparator1
        '
        Me.BindingNavigatorSeparator1.Name = "BindingNavigatorSeparator1"
        Me.BindingNavigatorSeparator1.Size = New System.Drawing.Size(6, 23)
        '
        'BindingNavigatorMoveNextItem
        '
        Me.BindingNavigatorMoveNextItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.BindingNavigatorMoveNextItem.Image = CType(resources.GetObject("BindingNavigatorMoveNextItem.Image"), System.Drawing.Image)
        Me.BindingNavigatorMoveNextItem.Name = "BindingNavigatorMoveNextItem"
        Me.BindingNavigatorMoveNextItem.RightToLeftAutoMirrorImage = True
        Me.BindingNavigatorMoveNextItem.Size = New System.Drawing.Size(23, 20)
        Me.BindingNavigatorMoveNextItem.Text = "Move next"
        '
        'BindingNavigatorMoveLastItem
        '
        Me.BindingNavigatorMoveLastItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.BindingNavigatorMoveLastItem.Image = CType(resources.GetObject("BindingNavigatorMoveLastItem.Image"), System.Drawing.Image)
        Me.BindingNavigatorMoveLastItem.Name = "BindingNavigatorMoveLastItem"
        Me.BindingNavigatorMoveLastItem.RightToLeftAutoMirrorImage = True
        Me.BindingNavigatorMoveLastItem.Size = New System.Drawing.Size(23, 20)
        Me.BindingNavigatorMoveLastItem.Text = "Move last"
        '
        'BindingNavigatorSeparator2
        '
        Me.BindingNavigatorSeparator2.Name = "BindingNavigatorSeparator2"
        Me.BindingNavigatorSeparator2.Size = New System.Drawing.Size(6, 23)
        '
        'GrpNavigatorSaveItem
        '
        Me.GrpNavigatorSaveItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.GrpNavigatorSaveItem.Image = CType(resources.GetObject("GrpNavigatorSaveItem.Image"), System.Drawing.Image)
        Me.GrpNavigatorSaveItem.Name = "GrpNavigatorSaveItem"
        Me.GrpNavigatorSaveItem.Size = New System.Drawing.Size(23, 20)
        Me.GrpNavigatorSaveItem.Text = "Save Data"
        '
        'gridMedGroups
        '
        Me.gridMedGroups.DataSource = Me.MedicineGroupsBindingSource
        Me.gridMedGroups.Dock = System.Windows.Forms.DockStyle.Right
        Me.gridMedGroups.Location = New System.Drawing.Point(252, 22)
        Me.gridMedGroups.MainView = Me.GridView1
        Me.gridMedGroups.Name = "gridMedGroups"
        Me.gridMedGroups.Size = New System.Drawing.Size(400, 217)
        Me.gridMedGroups.TabIndex = 3
        Me.gridMedGroups.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridView1})
        '
        'GridView1
        '
        Me.GridView1.GridControl = Me.gridMedGroups
        Me.GridView1.Name = "GridView1"
        '
        'btnDelMedGroup
        '
        Me.btnDelMedGroup.Appearance.Font = New System.Drawing.Font("Calibri", 10.0!, System.Drawing.FontStyle.Bold)
        Me.btnDelMedGroup.Appearance.Options.UseFont = True
        Me.btnDelMedGroup.Location = New System.Drawing.Point(10, 173)
        Me.btnDelMedGroup.Name = "btnDelMedGroup"
        Me.btnDelMedGroup.Size = New System.Drawing.Size(75, 23)
        Me.btnDelMedGroup.TabIndex = 2
        Me.btnDelMedGroup.Text = "Delete"
        '
        'btnEditMedGroup
        '
        Me.btnEditMedGroup.Appearance.Font = New System.Drawing.Font("Calibri", 10.0!, System.Drawing.FontStyle.Bold)
        Me.btnEditMedGroup.Appearance.Options.UseFont = True
        Me.btnEditMedGroup.Location = New System.Drawing.Point(10, 144)
        Me.btnEditMedGroup.Name = "btnEditMedGroup"
        Me.btnEditMedGroup.Size = New System.Drawing.Size(75, 23)
        Me.btnEditMedGroup.TabIndex = 2
        Me.btnEditMedGroup.Text = "Edit"
        '
        'btnAddMedGroup
        '
        Me.btnAddMedGroup.Appearance.Font = New System.Drawing.Font("Calibri", 10.0!, System.Drawing.FontStyle.Bold)
        Me.btnAddMedGroup.Appearance.Options.UseFont = True
        Me.btnAddMedGroup.Location = New System.Drawing.Point(10, 115)
        Me.btnAddMedGroup.Name = "btnAddMedGroup"
        Me.btnAddMedGroup.Size = New System.Drawing.Size(75, 23)
        Me.btnAddMedGroup.TabIndex = 2
        Me.btnAddMedGroup.Text = "Add"
        '
        'txtMedicenGroup
        '
        Me.txtMedicenGroup.Location = New System.Drawing.Point(98, 64)
        Me.txtMedicenGroup.Name = "txtMedicenGroup"
        Me.txtMedicenGroup.Properties.Appearance.Font = New System.Drawing.Font("Calibri", 10.0!, System.Drawing.FontStyle.Bold)
        Me.txtMedicenGroup.Properties.Appearance.Options.UseFont = True
        Me.txtMedicenGroup.Size = New System.Drawing.Size(148, 22)
        Me.txtMedicenGroup.TabIndex = 1
        '
        'LabelControl1
        '
        Me.LabelControl1.Appearance.Font = New System.Drawing.Font("Calibri", 10.0!, System.Drawing.FontStyle.Bold)
        Me.LabelControl1.Appearance.Options.UseFont = True
        Me.LabelControl1.Location = New System.Drawing.Point(10, 67)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(87, 15)
        Me.LabelControl1.TabIndex = 0
        Me.LabelControl1.Text = "Medicine Group"
        '
        'PanelControl2
        '
        Me.PanelControl2.Controls.Add(Me.GroupControl6)
        Me.PanelControl2.Controls.Add(Me.GroupControl5)
        Me.PanelControl2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.PanelControl2.Location = New System.Drawing.Point(0, 491)
        Me.PanelControl2.Name = "PanelControl2"
        Me.PanelControl2.Size = New System.Drawing.Size(1360, 245)
        Me.PanelControl2.TabIndex = 0
        '
        'GroupControl6
        '
        Me.GroupControl6.Controls.Add(Me.MedicineDozeCombo1)
        Me.GroupControl6.Controls.Add(Me.DoseNavigator)
        Me.GroupControl6.Controls.Add(Me.gridMedDose)
        Me.GroupControl6.Controls.Add(Me.btnDelDose)
        Me.GroupControl6.Controls.Add(Me.cboShapeInfo)
        Me.GroupControl6.Controls.Add(Me.btnEditDose)
        Me.GroupControl6.Controls.Add(Me.LabelControl13)
        Me.GroupControl6.Controls.Add(Me.TextEdit6)
        Me.GroupControl6.Controls.Add(Me.btnAddDose)
        Me.GroupControl6.Controls.Add(Me.LabelControl14)
        Me.GroupControl6.Dock = System.Windows.Forms.DockStyle.Right
        Me.GroupControl6.Location = New System.Drawing.Point(655, 2)
        Me.GroupControl6.Name = "GroupControl6"
        Me.GroupControl6.Size = New System.Drawing.Size(703, 241)
        Me.GroupControl6.TabIndex = 1
        Me.GroupControl6.Text = "Medicine Dose"
        '
        'MedicineDozeCombo1
        '
        Me.MedicineDozeCombo1.DozeID = 0
        Me.MedicineDozeCombo1.Location = New System.Drawing.Point(112, 113)
        Me.MedicineDozeCombo1.Name = "MedicineDozeCombo1"
        Me.MedicineDozeCombo1.ParentShapeID = -1
        Me.MedicineDozeCombo1.ShapeID = 0
        Me.MedicineDozeCombo1.Size = New System.Drawing.Size(150, 22)
        Me.MedicineDozeCombo1.TabIndex = 42
        '
        'DoseNavigator
        '
        Me.DoseNavigator.AddNewItem = Nothing
        Me.DoseNavigator.AutoSize = False
        Me.DoseNavigator.BindingSource = Me.MedicineDozeBindingSource
        Me.DoseNavigator.CountItem = Me.ToolStripLabel3
        Me.DoseNavigator.DeleteItem = Me.DoseNavigatorDeleteItem
        Me.DoseNavigator.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.DoseNavigator.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripButton14, Me.ToolStripButton15, Me.ToolStripSeparator7, Me.ToolStripTextBox3, Me.ToolStripLabel3, Me.ToolStripSeparator8, Me.ToolStripButton16, Me.ToolStripButton17, Me.ToolStripSeparator9, Me.DoseNavigatorDeleteItem, Me.DoseNavigatorSaveItem})
        Me.DoseNavigator.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow
        Me.DoseNavigator.Location = New System.Drawing.Point(2, 210)
        Me.DoseNavigator.MoveFirstItem = Me.ToolStripButton14
        Me.DoseNavigator.MoveLastItem = Me.ToolStripButton17
        Me.DoseNavigator.MoveNextItem = Me.ToolStripButton16
        Me.DoseNavigator.MovePreviousItem = Me.ToolStripButton15
        Me.DoseNavigator.Name = "DoseNavigator"
        Me.DoseNavigator.PositionItem = Me.ToolStripTextBox3
        Me.DoseNavigator.Size = New System.Drawing.Size(299, 29)
        Me.DoseNavigator.TabIndex = 41
        Me.DoseNavigator.Text = "BindingNavigator1"
        Me.DoseNavigator.Visible = False
        '
        'MedicineDozeBindingSource
        '
        Me.MedicineDozeBindingSource.DataSource = Me.MedicineShapeBindingSource
        '
        'MedicineShapeBindingSource
        '
        Me.MedicineShapeBindingSource.DataSource = Me.MedicineItemsBindingSource
        '
        'MedicineItemsBindingSource
        '
        Me.MedicineItemsBindingSource.DataSource = Me.ScienceFamilyBindingSource
        '
        'ScienceFamilyBindingSource
        '
        Me.ScienceFamilyBindingSource.DataSource = Me.MedicineFamilyBindingSource
        '
        'ToolStripLabel3
        '
        Me.ToolStripLabel3.Name = "ToolStripLabel3"
        Me.ToolStripLabel3.Size = New System.Drawing.Size(35, 15)
        Me.ToolStripLabel3.Text = "of {0}"
        Me.ToolStripLabel3.ToolTipText = "Total number of items"
        '
        'DoseNavigatorDeleteItem
        '
        Me.DoseNavigatorDeleteItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.DoseNavigatorDeleteItem.Image = CType(resources.GetObject("DoseNavigatorDeleteItem.Image"), System.Drawing.Image)
        Me.DoseNavigatorDeleteItem.Name = "DoseNavigatorDeleteItem"
        Me.DoseNavigatorDeleteItem.RightToLeftAutoMirrorImage = True
        Me.DoseNavigatorDeleteItem.Size = New System.Drawing.Size(23, 20)
        Me.DoseNavigatorDeleteItem.Text = "Delete"
        '
        'ToolStripButton14
        '
        Me.ToolStripButton14.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton14.Image = CType(resources.GetObject("ToolStripButton14.Image"), System.Drawing.Image)
        Me.ToolStripButton14.Name = "ToolStripButton14"
        Me.ToolStripButton14.RightToLeftAutoMirrorImage = True
        Me.ToolStripButton14.Size = New System.Drawing.Size(23, 20)
        Me.ToolStripButton14.Text = "Move first"
        '
        'ToolStripButton15
        '
        Me.ToolStripButton15.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton15.Image = CType(resources.GetObject("ToolStripButton15.Image"), System.Drawing.Image)
        Me.ToolStripButton15.Name = "ToolStripButton15"
        Me.ToolStripButton15.RightToLeftAutoMirrorImage = True
        Me.ToolStripButton15.Size = New System.Drawing.Size(23, 20)
        Me.ToolStripButton15.Text = "Move previous"
        '
        'ToolStripSeparator7
        '
        Me.ToolStripSeparator7.Name = "ToolStripSeparator7"
        Me.ToolStripSeparator7.Size = New System.Drawing.Size(6, 23)
        '
        'ToolStripTextBox3
        '
        Me.ToolStripTextBox3.AccessibleName = "Position"
        Me.ToolStripTextBox3.AutoSize = False
        Me.ToolStripTextBox3.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.ToolStripTextBox3.Name = "ToolStripTextBox3"
        Me.ToolStripTextBox3.Size = New System.Drawing.Size(58, 23)
        Me.ToolStripTextBox3.Text = "0"
        Me.ToolStripTextBox3.ToolTipText = "Current position"
        '
        'ToolStripSeparator8
        '
        Me.ToolStripSeparator8.Name = "ToolStripSeparator8"
        Me.ToolStripSeparator8.Size = New System.Drawing.Size(6, 23)
        '
        'ToolStripButton16
        '
        Me.ToolStripButton16.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton16.Image = CType(resources.GetObject("ToolStripButton16.Image"), System.Drawing.Image)
        Me.ToolStripButton16.Name = "ToolStripButton16"
        Me.ToolStripButton16.RightToLeftAutoMirrorImage = True
        Me.ToolStripButton16.Size = New System.Drawing.Size(23, 20)
        Me.ToolStripButton16.Text = "Move next"
        '
        'ToolStripButton17
        '
        Me.ToolStripButton17.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton17.Image = CType(resources.GetObject("ToolStripButton17.Image"), System.Drawing.Image)
        Me.ToolStripButton17.Name = "ToolStripButton17"
        Me.ToolStripButton17.RightToLeftAutoMirrorImage = True
        Me.ToolStripButton17.Size = New System.Drawing.Size(23, 20)
        Me.ToolStripButton17.Text = "Move last"
        '
        'ToolStripSeparator9
        '
        Me.ToolStripSeparator9.Name = "ToolStripSeparator9"
        Me.ToolStripSeparator9.Size = New System.Drawing.Size(6, 23)
        '
        'DoseNavigatorSaveItem
        '
        Me.DoseNavigatorSaveItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.DoseNavigatorSaveItem.Image = CType(resources.GetObject("DoseNavigatorSaveItem.Image"), System.Drawing.Image)
        Me.DoseNavigatorSaveItem.Name = "DoseNavigatorSaveItem"
        Me.DoseNavigatorSaveItem.Size = New System.Drawing.Size(23, 20)
        Me.DoseNavigatorSaveItem.Text = "Save Data"
        '
        'gridMedDose
        '
        Me.gridMedDose.DataSource = Me.MedicineDozeBindingSource
        Me.gridMedDose.Dock = System.Windows.Forms.DockStyle.Right
        Me.gridMedDose.Location = New System.Drawing.Point(301, 22)
        Me.gridMedDose.MainView = Me.GridView6
        Me.gridMedDose.Name = "gridMedDose"
        Me.gridMedDose.Size = New System.Drawing.Size(400, 217)
        Me.gridMedDose.TabIndex = 3
        Me.gridMedDose.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridView6})
        '
        'GridView6
        '
        Me.GridView6.GridControl = Me.gridMedDose
        Me.GridView6.Name = "GridView6"
        '
        'btnDelDose
        '
        Me.btnDelDose.Appearance.Font = New System.Drawing.Font("Calibri", 10.0!, System.Drawing.FontStyle.Bold)
        Me.btnDelDose.Appearance.Options.UseFont = True
        Me.btnDelDose.Location = New System.Drawing.Point(14, 165)
        Me.btnDelDose.Name = "btnDelDose"
        Me.btnDelDose.Size = New System.Drawing.Size(75, 23)
        Me.btnDelDose.TabIndex = 2
        Me.btnDelDose.Text = "Delete"
        '
        'cboShapeInfo
        '
        Me.cboShapeInfo.Location = New System.Drawing.Point(112, 38)
        Me.cboShapeInfo.Name = "cboShapeInfo"
        Me.cboShapeInfo.Properties.Appearance.Font = New System.Drawing.Font("Calibri", 10.0!, System.Drawing.FontStyle.Bold)
        Me.cboShapeInfo.Properties.Appearance.Options.UseFont = True
        Me.cboShapeInfo.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cboShapeInfo.Size = New System.Drawing.Size(186, 22)
        Me.cboShapeInfo.TabIndex = 3
        '
        'btnEditDose
        '
        Me.btnEditDose.Appearance.Font = New System.Drawing.Font("Calibri", 10.0!, System.Drawing.FontStyle.Bold)
        Me.btnEditDose.Appearance.Options.UseFont = True
        Me.btnEditDose.Location = New System.Drawing.Point(14, 136)
        Me.btnEditDose.Name = "btnEditDose"
        Me.btnEditDose.Size = New System.Drawing.Size(75, 23)
        Me.btnEditDose.TabIndex = 2
        Me.btnEditDose.Text = "Edit"
        '
        'LabelControl13
        '
        Me.LabelControl13.Appearance.Font = New System.Drawing.Font("Calibri", 10.0!, System.Drawing.FontStyle.Bold)
        Me.LabelControl13.Appearance.Options.UseFont = True
        Me.LabelControl13.Location = New System.Drawing.Point(24, 76)
        Me.LabelControl13.Name = "LabelControl13"
        Me.LabelControl13.Size = New System.Drawing.Size(27, 15)
        Me.LabelControl13.TabIndex = 0
        Me.LabelControl13.Text = "Dose"
        '
        'TextEdit6
        '
        Me.TextEdit6.Location = New System.Drawing.Point(112, 73)
        Me.TextEdit6.Name = "TextEdit6"
        Me.TextEdit6.Properties.Appearance.Font = New System.Drawing.Font("Calibri", 10.0!, System.Drawing.FontStyle.Bold)
        Me.TextEdit6.Properties.Appearance.Options.UseFont = True
        Me.TextEdit6.Size = New System.Drawing.Size(186, 22)
        Me.TextEdit6.TabIndex = 1
        '
        'btnAddDose
        '
        Me.btnAddDose.Appearance.Font = New System.Drawing.Font("Calibri", 10.0!, System.Drawing.FontStyle.Bold)
        Me.btnAddDose.Appearance.Options.UseFont = True
        Me.btnAddDose.Location = New System.Drawing.Point(14, 107)
        Me.btnAddDose.Name = "btnAddDose"
        Me.btnAddDose.Size = New System.Drawing.Size(75, 23)
        Me.btnAddDose.TabIndex = 2
        Me.btnAddDose.Text = "Add"
        '
        'LabelControl14
        '
        Me.LabelControl14.Appearance.Font = New System.Drawing.Font("Calibri", 10.0!, System.Drawing.FontStyle.Bold)
        Me.LabelControl14.Appearance.Options.UseFont = True
        Me.LabelControl14.Location = New System.Drawing.Point(12, 41)
        Me.LabelControl14.Name = "LabelControl14"
        Me.LabelControl14.Size = New System.Drawing.Size(86, 15)
        Me.LabelControl14.TabIndex = 0
        Me.LabelControl14.Text = "Medicine Shape"
        '
        'GroupControl5
        '
        Me.GroupControl5.Controls.Add(Me.MedicineShapeCombo1)
        Me.GroupControl5.Controls.Add(Me.ShapeNavigator)
        Me.GroupControl5.Controls.Add(Me.gridMedShape)
        Me.GroupControl5.Controls.Add(Me.btnDelShape)
        Me.GroupControl5.Controls.Add(Me.btnAddShape)
        Me.GroupControl5.Controls.Add(Me.cboMedItem)
        Me.GroupControl5.Controls.Add(Me.LabelControl10)
        Me.GroupControl5.Controls.Add(Me.btnEditShape)
        Me.GroupControl5.Controls.Add(Me.txtShapeInfo)
        Me.GroupControl5.Controls.Add(Me.LabelControl12)
        Me.GroupControl5.Controls.Add(Me.txtMedicineShape)
        Me.GroupControl5.Controls.Add(Me.LabelControl11)
        Me.GroupControl5.Dock = System.Windows.Forms.DockStyle.Left
        Me.GroupControl5.Location = New System.Drawing.Point(2, 2)
        Me.GroupControl5.Name = "GroupControl5"
        Me.GroupControl5.Size = New System.Drawing.Size(654, 241)
        Me.GroupControl5.TabIndex = 0
        Me.GroupControl5.Text = "Medicine Shape"
        '
        'MedicineShapeCombo1
        '
        Me.MedicineShapeCombo1.Location = New System.Drawing.Point(96, 141)
        Me.MedicineShapeCombo1.MedicineItemID = 0
        Me.MedicineShapeCombo1.Name = "MedicineShapeCombo1"
        Me.MedicineShapeCombo1.ParentMedicineItemID = -1
        Me.MedicineShapeCombo1.ShapeID = 0
        Me.MedicineShapeCombo1.Size = New System.Drawing.Size(150, 22)
        Me.MedicineShapeCombo1.TabIndex = 41
        '
        'ShapeNavigator
        '
        Me.ShapeNavigator.AddNewItem = Nothing
        Me.ShapeNavigator.AutoSize = False
        Me.ShapeNavigator.BindingSource = Me.MedicineShapeBindingSource
        Me.ShapeNavigator.CountItem = Me.ToolStripLabel2
        Me.ShapeNavigator.DeleteItem = Me.ShapeNavigatorDeleteItem
        Me.ShapeNavigator.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.ShapeNavigator.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripButton8, Me.ToolStripButton9, Me.ToolStripSeparator4, Me.ToolStripTextBox2, Me.ToolStripLabel2, Me.ToolStripSeparator5, Me.ToolStripButton10, Me.ToolStripButton11, Me.ToolStripSeparator6, Me.ShapeNavigatorDeleteItem, Me.ShapeNavigatorSaveItem})
        Me.ShapeNavigator.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow
        Me.ShapeNavigator.Location = New System.Drawing.Point(2, 210)
        Me.ShapeNavigator.MoveFirstItem = Me.ToolStripButton8
        Me.ShapeNavigator.MoveLastItem = Me.ToolStripButton11
        Me.ShapeNavigator.MoveNextItem = Me.ToolStripButton10
        Me.ShapeNavigator.MovePreviousItem = Me.ToolStripButton9
        Me.ShapeNavigator.Name = "ShapeNavigator"
        Me.ShapeNavigator.PositionItem = Me.ToolStripTextBox2
        Me.ShapeNavigator.Size = New System.Drawing.Size(250, 29)
        Me.ShapeNavigator.TabIndex = 40
        Me.ShapeNavigator.Text = "BindingNavigator1"
        Me.ShapeNavigator.Visible = False
        '
        'ToolStripLabel2
        '
        Me.ToolStripLabel2.Name = "ToolStripLabel2"
        Me.ToolStripLabel2.Size = New System.Drawing.Size(35, 15)
        Me.ToolStripLabel2.Text = "of {0}"
        Me.ToolStripLabel2.ToolTipText = "Total number of items"
        '
        'ShapeNavigatorDeleteItem
        '
        Me.ShapeNavigatorDeleteItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ShapeNavigatorDeleteItem.Image = CType(resources.GetObject("ShapeNavigatorDeleteItem.Image"), System.Drawing.Image)
        Me.ShapeNavigatorDeleteItem.Name = "ShapeNavigatorDeleteItem"
        Me.ShapeNavigatorDeleteItem.RightToLeftAutoMirrorImage = True
        Me.ShapeNavigatorDeleteItem.Size = New System.Drawing.Size(23, 20)
        Me.ShapeNavigatorDeleteItem.Text = "Delete"
        '
        'ToolStripButton8
        '
        Me.ToolStripButton8.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton8.Image = CType(resources.GetObject("ToolStripButton8.Image"), System.Drawing.Image)
        Me.ToolStripButton8.Name = "ToolStripButton8"
        Me.ToolStripButton8.RightToLeftAutoMirrorImage = True
        Me.ToolStripButton8.Size = New System.Drawing.Size(23, 20)
        Me.ToolStripButton8.Text = "Move first"
        '
        'ToolStripButton9
        '
        Me.ToolStripButton9.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton9.Image = CType(resources.GetObject("ToolStripButton9.Image"), System.Drawing.Image)
        Me.ToolStripButton9.Name = "ToolStripButton9"
        Me.ToolStripButton9.RightToLeftAutoMirrorImage = True
        Me.ToolStripButton9.Size = New System.Drawing.Size(23, 20)
        Me.ToolStripButton9.Text = "Move previous"
        '
        'ToolStripSeparator4
        '
        Me.ToolStripSeparator4.Name = "ToolStripSeparator4"
        Me.ToolStripSeparator4.Size = New System.Drawing.Size(6, 23)
        '
        'ToolStripTextBox2
        '
        Me.ToolStripTextBox2.AccessibleName = "Position"
        Me.ToolStripTextBox2.AutoSize = False
        Me.ToolStripTextBox2.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.ToolStripTextBox2.Name = "ToolStripTextBox2"
        Me.ToolStripTextBox2.Size = New System.Drawing.Size(58, 23)
        Me.ToolStripTextBox2.Text = "0"
        Me.ToolStripTextBox2.ToolTipText = "Current position"
        '
        'ToolStripSeparator5
        '
        Me.ToolStripSeparator5.Name = "ToolStripSeparator5"
        Me.ToolStripSeparator5.Size = New System.Drawing.Size(6, 23)
        '
        'ToolStripButton10
        '
        Me.ToolStripButton10.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton10.Image = CType(resources.GetObject("ToolStripButton10.Image"), System.Drawing.Image)
        Me.ToolStripButton10.Name = "ToolStripButton10"
        Me.ToolStripButton10.RightToLeftAutoMirrorImage = True
        Me.ToolStripButton10.Size = New System.Drawing.Size(23, 20)
        Me.ToolStripButton10.Text = "Move next"
        '
        'ToolStripButton11
        '
        Me.ToolStripButton11.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton11.Image = CType(resources.GetObject("ToolStripButton11.Image"), System.Drawing.Image)
        Me.ToolStripButton11.Name = "ToolStripButton11"
        Me.ToolStripButton11.RightToLeftAutoMirrorImage = True
        Me.ToolStripButton11.Size = New System.Drawing.Size(23, 20)
        Me.ToolStripButton11.Text = "Move last"
        '
        'ToolStripSeparator6
        '
        Me.ToolStripSeparator6.Name = "ToolStripSeparator6"
        Me.ToolStripSeparator6.Size = New System.Drawing.Size(6, 23)
        '
        'ShapeNavigatorSaveItem
        '
        Me.ShapeNavigatorSaveItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ShapeNavigatorSaveItem.Image = CType(resources.GetObject("ShapeNavigatorSaveItem.Image"), System.Drawing.Image)
        Me.ShapeNavigatorSaveItem.Name = "ShapeNavigatorSaveItem"
        Me.ShapeNavigatorSaveItem.Size = New System.Drawing.Size(23, 20)
        Me.ShapeNavigatorSaveItem.Text = "Save Data"
        '
        'gridMedShape
        '
        Me.gridMedShape.DataSource = Me.MedicineShapeBindingSource
        Me.gridMedShape.Dock = System.Windows.Forms.DockStyle.Right
        Me.gridMedShape.Location = New System.Drawing.Point(252, 22)
        Me.gridMedShape.MainView = Me.GridView5
        Me.gridMedShape.Name = "gridMedShape"
        Me.gridMedShape.Size = New System.Drawing.Size(400, 217)
        Me.gridMedShape.TabIndex = 3
        Me.gridMedShape.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridView5})
        '
        'GridView5
        '
        Me.GridView5.GridControl = Me.gridMedShape
        Me.GridView5.Name = "GridView5"
        '
        'btnDelShape
        '
        Me.btnDelShape.Appearance.Font = New System.Drawing.Font("Calibri", 10.0!, System.Drawing.FontStyle.Bold)
        Me.btnDelShape.Appearance.Options.UseFont = True
        Me.btnDelShape.Location = New System.Drawing.Point(10, 192)
        Me.btnDelShape.Name = "btnDelShape"
        Me.btnDelShape.Size = New System.Drawing.Size(75, 23)
        Me.btnDelShape.TabIndex = 2
        Me.btnDelShape.Text = "Delete"
        '
        'btnAddShape
        '
        Me.btnAddShape.Appearance.Font = New System.Drawing.Font("Calibri", 10.0!, System.Drawing.FontStyle.Bold)
        Me.btnAddShape.Appearance.Options.UseFont = True
        Me.btnAddShape.Location = New System.Drawing.Point(10, 134)
        Me.btnAddShape.Name = "btnAddShape"
        Me.btnAddShape.Size = New System.Drawing.Size(75, 23)
        Me.btnAddShape.TabIndex = 2
        Me.btnAddShape.Text = "Add"
        '
        'cboMedItem
        '
        Me.cboMedItem.Location = New System.Drawing.Point(108, 50)
        Me.cboMedItem.Name = "cboMedItem"
        Me.cboMedItem.Properties.Appearance.Font = New System.Drawing.Font("Calibri", 10.0!, System.Drawing.FontStyle.Bold)
        Me.cboMedItem.Properties.Appearance.Options.UseFont = True
        Me.cboMedItem.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cboMedItem.Size = New System.Drawing.Size(100, 22)
        Me.cboMedItem.TabIndex = 3
        '
        'LabelControl10
        '
        Me.LabelControl10.Appearance.Font = New System.Drawing.Font("Calibri", 10.0!, System.Drawing.FontStyle.Bold)
        Me.LabelControl10.Appearance.Options.UseFont = True
        Me.LabelControl10.Location = New System.Drawing.Point(20, 53)
        Me.LabelControl10.Name = "LabelControl10"
        Me.LabelControl10.Size = New System.Drawing.Size(50, 15)
        Me.LabelControl10.TabIndex = 0
        Me.LabelControl10.Text = "Medicine"
        '
        'btnEditShape
        '
        Me.btnEditShape.Appearance.Font = New System.Drawing.Font("Calibri", 10.0!, System.Drawing.FontStyle.Bold)
        Me.btnEditShape.Appearance.Options.UseFont = True
        Me.btnEditShape.Location = New System.Drawing.Point(10, 163)
        Me.btnEditShape.Name = "btnEditShape"
        Me.btnEditShape.Size = New System.Drawing.Size(75, 23)
        Me.btnEditShape.TabIndex = 2
        Me.btnEditShape.Text = "Edit"
        '
        'txtShapeInfo
        '
        Me.txtShapeInfo.Location = New System.Drawing.Point(108, 113)
        Me.txtShapeInfo.Name = "txtShapeInfo"
        Me.txtShapeInfo.Properties.Appearance.Font = New System.Drawing.Font("Calibri", 10.0!, System.Drawing.FontStyle.Bold)
        Me.txtShapeInfo.Properties.Appearance.Options.UseFont = True
        Me.txtShapeInfo.Size = New System.Drawing.Size(100, 22)
        Me.txtShapeInfo.TabIndex = 1
        '
        'LabelControl12
        '
        Me.LabelControl12.Appearance.Font = New System.Drawing.Font("Calibri", 10.0!, System.Drawing.FontStyle.Bold)
        Me.LabelControl12.Appearance.Options.UseFont = True
        Me.LabelControl12.Location = New System.Drawing.Point(20, 116)
        Me.LabelControl12.Name = "LabelControl12"
        Me.LabelControl12.Size = New System.Drawing.Size(57, 15)
        Me.LabelControl12.TabIndex = 0
        Me.LabelControl12.Text = "Shape Info"
        '
        'txtMedicineShape
        '
        Me.txtMedicineShape.Location = New System.Drawing.Point(108, 85)
        Me.txtMedicineShape.Name = "txtMedicineShape"
        Me.txtMedicineShape.Properties.Appearance.Font = New System.Drawing.Font("Calibri", 10.0!, System.Drawing.FontStyle.Bold)
        Me.txtMedicineShape.Properties.Appearance.Options.UseFont = True
        Me.txtMedicineShape.Size = New System.Drawing.Size(100, 22)
        Me.txtMedicineShape.TabIndex = 1
        '
        'LabelControl11
        '
        Me.LabelControl11.Appearance.Font = New System.Drawing.Font("Calibri", 10.0!, System.Drawing.FontStyle.Bold)
        Me.LabelControl11.Appearance.Options.UseFont = True
        Me.LabelControl11.Location = New System.Drawing.Point(20, 88)
        Me.LabelControl11.Name = "LabelControl11"
        Me.LabelControl11.Size = New System.Drawing.Size(86, 15)
        Me.LabelControl11.TabIndex = 0
        Me.LabelControl11.Text = "Medicine Shape"
        '
        'SidePanel1
        '
        Me.SidePanel1.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(239, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.SidePanel1.Appearance.Options.UseBackColor = True
        Me.SidePanel1.BorderThickness = 2
        Me.SidePanel1.Controls.Add(Me.GroupControl4)
        Me.SidePanel1.Controls.Add(Me.GroupControl3)
        Me.SidePanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SidePanel1.Location = New System.Drawing.Point(0, 245)
        Me.SidePanel1.Name = "SidePanel1"
        Me.SidePanel1.Size = New System.Drawing.Size(1360, 246)
        Me.SidePanel1.TabIndex = 1
        Me.SidePanel1.Text = "SidePanel1"
        '
        'GroupControl4
        '
        Me.GroupControl4.Controls.Add(Me.MedicineItemsCombo1)
        Me.GroupControl4.Controls.Add(Me.ItemsNavigator)
        Me.GroupControl4.Controls.Add(Me.gridMedItems)
        Me.GroupControl4.Controls.Add(Me.btnDelScinetificName)
        Me.GroupControl4.Controls.Add(Me.txtNotes)
        Me.GroupControl4.Controls.Add(Me.txtCompany)
        Me.GroupControl4.Controls.Add(Me.LabelControl6)
        Me.GroupControl4.Controls.Add(Me.txtCommon)
        Me.GroupControl4.Controls.Add(Me.btnAddScinetificName)
        Me.GroupControl4.Controls.Add(Me.LabelControl9)
        Me.GroupControl4.Controls.Add(Me.btnEditScinetificName)
        Me.GroupControl4.Controls.Add(Me.LabelControl8)
        Me.GroupControl4.Controls.Add(Me.cboDoseInfo)
        Me.GroupControl4.Controls.Add(Me.cboScinetificName)
        Me.GroupControl4.Controls.Add(Me.LabelControl7)
        Me.GroupControl4.Dock = System.Windows.Forms.DockStyle.Right
        Me.GroupControl4.Location = New System.Drawing.Point(655, 0)
        Me.GroupControl4.Name = "GroupControl4"
        Me.GroupControl4.Size = New System.Drawing.Size(705, 246)
        Me.GroupControl4.TabIndex = 1
        Me.GroupControl4.Text = "Medicine Items"
        '
        'MedicineItemsCombo1
        '
        Me.MedicineItemsCombo1.CommName = Nothing
        Me.MedicineItemsCombo1.Location = New System.Drawing.Point(110, 136)
        Me.MedicineItemsCombo1.MedicineItemID = 0
        Me.MedicineItemsCombo1.Name = "MedicineItemsCombo1"
        Me.MedicineItemsCombo1.ParentScincID = -1
        Me.MedicineItemsCombo1.Size = New System.Drawing.Size(150, 22)
        Me.MedicineItemsCombo1.TabIndex = 29
        '
        'ItemsNavigator
        '
        Me.ItemsNavigator.AddNewItem = Nothing
        Me.ItemsNavigator.AutoSize = False
        Me.ItemsNavigator.BindingSource = Me.MedicineItemsBindingSource
        Me.ItemsNavigator.CountItem = Me.ToolStripLabel5
        Me.ItemsNavigator.DeleteItem = Me.ItemsNavigatorDeleteItem
        Me.ItemsNavigator.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.ItemsNavigator.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripButton26, Me.ToolStripButton27, Me.ToolStripSeparator13, Me.ToolStripTextBox5, Me.ToolStripLabel5, Me.ToolStripSeparator14, Me.ToolStripButton28, Me.ToolStripButton29, Me.ToolStripSeparator15, Me.ItemsNavigatorDeleteItem, Me.ItemsNavigatorSaveItem})
        Me.ItemsNavigator.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow
        Me.ItemsNavigator.Location = New System.Drawing.Point(2, 220)
        Me.ItemsNavigator.MoveFirstItem = Me.ToolStripButton26
        Me.ItemsNavigator.MoveLastItem = Me.ToolStripButton29
        Me.ItemsNavigator.MoveNextItem = Me.ToolStripButton28
        Me.ItemsNavigator.MovePreviousItem = Me.ToolStripButton27
        Me.ItemsNavigator.Name = "ItemsNavigator"
        Me.ItemsNavigator.PositionItem = Me.ToolStripTextBox5
        Me.ItemsNavigator.Size = New System.Drawing.Size(301, 24)
        Me.ItemsNavigator.TabIndex = 28
        Me.ItemsNavigator.Text = "BindingNavigator1"
        Me.ItemsNavigator.Visible = False
        '
        'ToolStripLabel5
        '
        Me.ToolStripLabel5.Name = "ToolStripLabel5"
        Me.ToolStripLabel5.Size = New System.Drawing.Size(35, 15)
        Me.ToolStripLabel5.Text = "of {0}"
        Me.ToolStripLabel5.ToolTipText = "Total number of items"
        '
        'ItemsNavigatorDeleteItem
        '
        Me.ItemsNavigatorDeleteItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ItemsNavigatorDeleteItem.Image = CType(resources.GetObject("ItemsNavigatorDeleteItem.Image"), System.Drawing.Image)
        Me.ItemsNavigatorDeleteItem.Name = "ItemsNavigatorDeleteItem"
        Me.ItemsNavigatorDeleteItem.RightToLeftAutoMirrorImage = True
        Me.ItemsNavigatorDeleteItem.Size = New System.Drawing.Size(23, 20)
        Me.ItemsNavigatorDeleteItem.Text = "Delete"
        '
        'ToolStripButton26
        '
        Me.ToolStripButton26.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton26.Image = CType(resources.GetObject("ToolStripButton26.Image"), System.Drawing.Image)
        Me.ToolStripButton26.Name = "ToolStripButton26"
        Me.ToolStripButton26.RightToLeftAutoMirrorImage = True
        Me.ToolStripButton26.Size = New System.Drawing.Size(23, 20)
        Me.ToolStripButton26.Text = "Move first"
        '
        'ToolStripButton27
        '
        Me.ToolStripButton27.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton27.Image = CType(resources.GetObject("ToolStripButton27.Image"), System.Drawing.Image)
        Me.ToolStripButton27.Name = "ToolStripButton27"
        Me.ToolStripButton27.RightToLeftAutoMirrorImage = True
        Me.ToolStripButton27.Size = New System.Drawing.Size(23, 20)
        Me.ToolStripButton27.Text = "Move previous"
        '
        'ToolStripSeparator13
        '
        Me.ToolStripSeparator13.Name = "ToolStripSeparator13"
        Me.ToolStripSeparator13.Size = New System.Drawing.Size(6, 23)
        '
        'ToolStripTextBox5
        '
        Me.ToolStripTextBox5.AccessibleName = "Position"
        Me.ToolStripTextBox5.AutoSize = False
        Me.ToolStripTextBox5.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.ToolStripTextBox5.Name = "ToolStripTextBox5"
        Me.ToolStripTextBox5.Size = New System.Drawing.Size(34, 23)
        Me.ToolStripTextBox5.Text = "0"
        Me.ToolStripTextBox5.ToolTipText = "Current position"
        '
        'ToolStripSeparator14
        '
        Me.ToolStripSeparator14.Name = "ToolStripSeparator14"
        Me.ToolStripSeparator14.Size = New System.Drawing.Size(6, 23)
        '
        'ToolStripButton28
        '
        Me.ToolStripButton28.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton28.Image = CType(resources.GetObject("ToolStripButton28.Image"), System.Drawing.Image)
        Me.ToolStripButton28.Name = "ToolStripButton28"
        Me.ToolStripButton28.RightToLeftAutoMirrorImage = True
        Me.ToolStripButton28.Size = New System.Drawing.Size(23, 20)
        Me.ToolStripButton28.Text = "Move next"
        '
        'ToolStripButton29
        '
        Me.ToolStripButton29.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton29.Image = CType(resources.GetObject("ToolStripButton29.Image"), System.Drawing.Image)
        Me.ToolStripButton29.Name = "ToolStripButton29"
        Me.ToolStripButton29.RightToLeftAutoMirrorImage = True
        Me.ToolStripButton29.Size = New System.Drawing.Size(23, 20)
        Me.ToolStripButton29.Text = "Move last"
        '
        'ToolStripSeparator15
        '
        Me.ToolStripSeparator15.Name = "ToolStripSeparator15"
        Me.ToolStripSeparator15.Size = New System.Drawing.Size(6, 23)
        '
        'ItemsNavigatorSaveItem
        '
        Me.ItemsNavigatorSaveItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ItemsNavigatorSaveItem.Image = CType(resources.GetObject("ItemsNavigatorSaveItem.Image"), System.Drawing.Image)
        Me.ItemsNavigatorSaveItem.Name = "ItemsNavigatorSaveItem"
        Me.ItemsNavigatorSaveItem.Size = New System.Drawing.Size(23, 20)
        Me.ItemsNavigatorSaveItem.Text = "Save Data"
        '
        'gridMedItems
        '
        Me.gridMedItems.DataSource = Me.MedicineItemsBindingSource
        Me.gridMedItems.Dock = System.Windows.Forms.DockStyle.Right
        Me.gridMedItems.Location = New System.Drawing.Point(303, 22)
        Me.gridMedItems.MainView = Me.GridView4
        Me.gridMedItems.Name = "gridMedItems"
        Me.gridMedItems.Size = New System.Drawing.Size(400, 222)
        Me.gridMedItems.TabIndex = 3
        Me.gridMedItems.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridView4})
        '
        'GridView4
        '
        Me.GridView4.GridControl = Me.gridMedItems
        Me.GridView4.Name = "GridView4"
        '
        'btnDelScinetificName
        '
        Me.btnDelScinetificName.Appearance.Font = New System.Drawing.Font("Calibri", 10.0!, System.Drawing.FontStyle.Bold)
        Me.btnDelScinetificName.Appearance.Options.UseFont = True
        Me.btnDelScinetificName.Location = New System.Drawing.Point(5, 196)
        Me.btnDelScinetificName.Name = "btnDelScinetificName"
        Me.btnDelScinetificName.Size = New System.Drawing.Size(75, 23)
        Me.btnDelScinetificName.TabIndex = 2
        Me.btnDelScinetificName.Text = "Delete"
        '
        'txtNotes
        '
        Me.txtNotes.Location = New System.Drawing.Point(48, 109)
        Me.txtNotes.Name = "txtNotes"
        Me.txtNotes.Properties.Appearance.Font = New System.Drawing.Font("Calibri", 10.0!, System.Drawing.FontStyle.Bold)
        Me.txtNotes.Properties.Appearance.Options.UseFont = True
        Me.txtNotes.Size = New System.Drawing.Size(250, 22)
        Me.txtNotes.TabIndex = 1
        '
        'txtCompany
        '
        Me.txtCompany.Location = New System.Drawing.Point(110, 85)
        Me.txtCompany.Name = "txtCompany"
        Me.txtCompany.Properties.Appearance.Font = New System.Drawing.Font("Calibri", 10.0!, System.Drawing.FontStyle.Bold)
        Me.txtCompany.Properties.Appearance.Options.UseFont = True
        Me.txtCompany.Size = New System.Drawing.Size(100, 22)
        Me.txtCompany.TabIndex = 1
        '
        'LabelControl6
        '
        Me.LabelControl6.Appearance.Font = New System.Drawing.Font("Calibri", 10.0!, System.Drawing.FontStyle.Bold)
        Me.LabelControl6.Appearance.Options.UseFont = True
        Me.LabelControl6.Location = New System.Drawing.Point(9, 32)
        Me.LabelControl6.Name = "LabelControl6"
        Me.LabelControl6.Size = New System.Drawing.Size(84, 15)
        Me.LabelControl6.TabIndex = 0
        Me.LabelControl6.Text = "Scinetific Name"
        '
        'txtCommon
        '
        Me.txtCommon.Location = New System.Drawing.Point(110, 57)
        Me.txtCommon.Name = "txtCommon"
        Me.txtCommon.Properties.Appearance.Font = New System.Drawing.Font("Calibri", 10.0!, System.Drawing.FontStyle.Bold)
        Me.txtCommon.Properties.Appearance.Options.UseFont = True
        Me.txtCommon.Size = New System.Drawing.Size(100, 22)
        Me.txtCommon.TabIndex = 1
        '
        'btnAddScinetificName
        '
        Me.btnAddScinetificName.Appearance.Font = New System.Drawing.Font("Calibri", 10.0!, System.Drawing.FontStyle.Bold)
        Me.btnAddScinetificName.Appearance.Options.UseFont = True
        Me.btnAddScinetificName.Location = New System.Drawing.Point(5, 138)
        Me.btnAddScinetificName.Name = "btnAddScinetificName"
        Me.btnAddScinetificName.Size = New System.Drawing.Size(75, 23)
        Me.btnAddScinetificName.TabIndex = 2
        Me.btnAddScinetificName.Text = "Add"
        '
        'LabelControl9
        '
        Me.LabelControl9.Appearance.Font = New System.Drawing.Font("Calibri", 10.0!, System.Drawing.FontStyle.Bold)
        Me.LabelControl9.Appearance.Options.UseFont = True
        Me.LabelControl9.Location = New System.Drawing.Point(9, 112)
        Me.LabelControl9.Name = "LabelControl9"
        Me.LabelControl9.Size = New System.Drawing.Size(33, 15)
        Me.LabelControl9.TabIndex = 0
        Me.LabelControl9.Text = "Notes"
        '
        'btnEditScinetificName
        '
        Me.btnEditScinetificName.Appearance.Font = New System.Drawing.Font("Calibri", 10.0!, System.Drawing.FontStyle.Bold)
        Me.btnEditScinetificName.Appearance.Options.UseFont = True
        Me.btnEditScinetificName.Location = New System.Drawing.Point(5, 167)
        Me.btnEditScinetificName.Name = "btnEditScinetificName"
        Me.btnEditScinetificName.Size = New System.Drawing.Size(75, 23)
        Me.btnEditScinetificName.TabIndex = 2
        Me.btnEditScinetificName.Text = "Edit"
        '
        'LabelControl8
        '
        Me.LabelControl8.Appearance.Font = New System.Drawing.Font("Calibri", 10.0!, System.Drawing.FontStyle.Bold)
        Me.LabelControl8.Appearance.Options.UseFont = True
        Me.LabelControl8.Location = New System.Drawing.Point(9, 88)
        Me.LabelControl8.Name = "LabelControl8"
        Me.LabelControl8.Size = New System.Drawing.Size(87, 15)
        Me.LabelControl8.TabIndex = 0
        Me.LabelControl8.Text = "Company Name"
        '
        'cboDoseInfo
        '
        Me.cboDoseInfo.EditValue = ""
        Me.cboDoseInfo.Location = New System.Drawing.Point(112, 164)
        Me.cboDoseInfo.Name = "cboDoseInfo"
        Me.cboDoseInfo.Properties.Appearance.Font = New System.Drawing.Font("Calibri", 10.0!, System.Drawing.FontStyle.Bold)
        Me.cboDoseInfo.Properties.Appearance.Options.UseFont = True
        Me.cboDoseInfo.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cboDoseInfo.Size = New System.Drawing.Size(100, 22)
        Me.cboDoseInfo.TabIndex = 3
        '
        'cboScinetificName
        '
        Me.cboScinetificName.EditValue = ""
        Me.cboScinetificName.Location = New System.Drawing.Point(110, 29)
        Me.cboScinetificName.Name = "cboScinetificName"
        Me.cboScinetificName.Properties.Appearance.Font = New System.Drawing.Font("Calibri", 10.0!, System.Drawing.FontStyle.Bold)
        Me.cboScinetificName.Properties.Appearance.Options.UseFont = True
        Me.cboScinetificName.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cboScinetificName.Size = New System.Drawing.Size(100, 22)
        Me.cboScinetificName.TabIndex = 3
        '
        'LabelControl7
        '
        Me.LabelControl7.Appearance.Font = New System.Drawing.Font("Calibri", 10.0!, System.Drawing.FontStyle.Bold)
        Me.LabelControl7.Appearance.Options.UseFont = True
        Me.LabelControl7.Location = New System.Drawing.Point(9, 60)
        Me.LabelControl7.Name = "LabelControl7"
        Me.LabelControl7.Size = New System.Drawing.Size(86, 15)
        Me.LabelControl7.TabIndex = 0
        Me.LabelControl7.Text = "Common Name"
        '
        'GroupControl3
        '
        Me.GroupControl3.Controls.Add(Me.MedScienceFamilyCombo1)
        Me.GroupControl3.Controls.Add(Me.ScienceNavigator)
        Me.GroupControl3.Controls.Add(Me.gridMedScinFamily)
        Me.GroupControl3.Controls.Add(Me.btnDelMedScienFamily)
        Me.GroupControl3.Controls.Add(Me.btnAddMedScienFamily)
        Me.GroupControl3.Controls.Add(Me.btnEditMedScienFamily)
        Me.GroupControl3.Controls.Add(Me.LabelControl4)
        Me.GroupControl3.Controls.Add(Me.cboMedFamily)
        Me.GroupControl3.Controls.Add(Me.txtScienceName)
        Me.GroupControl3.Controls.Add(Me.LabelControl5)
        Me.GroupControl3.Dock = System.Windows.Forms.DockStyle.Left
        Me.GroupControl3.Location = New System.Drawing.Point(0, 0)
        Me.GroupControl3.Name = "GroupControl3"
        Me.GroupControl3.Size = New System.Drawing.Size(654, 246)
        Me.GroupControl3.TabIndex = 0
        Me.GroupControl3.Text = "Science Family"
        '
        'MedScienceFamilyCombo1
        '
        Me.MedScienceFamilyCombo1.Location = New System.Drawing.Point(100, 112)
        Me.MedScienceFamilyCombo1.Name = "MedScienceFamilyCombo1"
        Me.MedScienceFamilyCombo1.ParentSubCatID = -1
        Me.MedScienceFamilyCombo1.ScienceName = Nothing
        Me.MedScienceFamilyCombo1.ScincID = 0
        Me.MedScienceFamilyCombo1.Size = New System.Drawing.Size(150, 22)
        Me.MedScienceFamilyCombo1.TabIndex = 29
        '
        'ScienceNavigator
        '
        Me.ScienceNavigator.AddNewItem = Nothing
        Me.ScienceNavigator.AutoSize = False
        Me.ScienceNavigator.BindingSource = Me.ScienceFamilyBindingSource
        Me.ScienceNavigator.CountItem = Me.ToolStripLabel1
        Me.ScienceNavigator.DeleteItem = Me.SciencNavigatorDeleteItem
        Me.ScienceNavigator.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.ScienceNavigator.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripButton2, Me.ToolStripButton3, Me.ToolStripSeparator1, Me.ToolStripTextBox1, Me.ToolStripLabel1, Me.ToolStripSeparator2, Me.ToolStripButton4, Me.ToolStripButton5, Me.ToolStripSeparator3, Me.SciencNavigatorDeleteItem, Me.SciencNavigatorSaveItem})
        Me.ScienceNavigator.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow
        Me.ScienceNavigator.Location = New System.Drawing.Point(2, 215)
        Me.ScienceNavigator.MoveFirstItem = Me.ToolStripButton2
        Me.ScienceNavigator.MoveLastItem = Me.ToolStripButton5
        Me.ScienceNavigator.MoveNextItem = Me.ToolStripButton4
        Me.ScienceNavigator.MovePreviousItem = Me.ToolStripButton3
        Me.ScienceNavigator.Name = "ScienceNavigator"
        Me.ScienceNavigator.PositionItem = Me.ToolStripTextBox1
        Me.ScienceNavigator.Size = New System.Drawing.Size(250, 29)
        Me.ScienceNavigator.TabIndex = 28
        Me.ScienceNavigator.Text = "BindingNavigator1"
        Me.ScienceNavigator.Visible = False
        '
        'ToolStripLabel1
        '
        Me.ToolStripLabel1.Name = "ToolStripLabel1"
        Me.ToolStripLabel1.Size = New System.Drawing.Size(35, 15)
        Me.ToolStripLabel1.Text = "of {0}"
        Me.ToolStripLabel1.ToolTipText = "Total number of items"
        '
        'SciencNavigatorDeleteItem
        '
        Me.SciencNavigatorDeleteItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.SciencNavigatorDeleteItem.Image = CType(resources.GetObject("SciencNavigatorDeleteItem.Image"), System.Drawing.Image)
        Me.SciencNavigatorDeleteItem.Name = "SciencNavigatorDeleteItem"
        Me.SciencNavigatorDeleteItem.RightToLeftAutoMirrorImage = True
        Me.SciencNavigatorDeleteItem.Size = New System.Drawing.Size(23, 20)
        Me.SciencNavigatorDeleteItem.Text = "Delete"
        '
        'ToolStripButton2
        '
        Me.ToolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton2.Image = CType(resources.GetObject("ToolStripButton2.Image"), System.Drawing.Image)
        Me.ToolStripButton2.Name = "ToolStripButton2"
        Me.ToolStripButton2.RightToLeftAutoMirrorImage = True
        Me.ToolStripButton2.Size = New System.Drawing.Size(23, 20)
        Me.ToolStripButton2.Text = "Move first"
        '
        'ToolStripButton3
        '
        Me.ToolStripButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton3.Image = CType(resources.GetObject("ToolStripButton3.Image"), System.Drawing.Image)
        Me.ToolStripButton3.Name = "ToolStripButton3"
        Me.ToolStripButton3.RightToLeftAutoMirrorImage = True
        Me.ToolStripButton3.Size = New System.Drawing.Size(23, 20)
        Me.ToolStripButton3.Text = "Move previous"
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
        Me.ToolStripTextBox1.Size = New System.Drawing.Size(58, 23)
        Me.ToolStripTextBox1.Text = "0"
        Me.ToolStripTextBox1.ToolTipText = "Current position"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(6, 23)
        '
        'ToolStripButton4
        '
        Me.ToolStripButton4.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton4.Image = CType(resources.GetObject("ToolStripButton4.Image"), System.Drawing.Image)
        Me.ToolStripButton4.Name = "ToolStripButton4"
        Me.ToolStripButton4.RightToLeftAutoMirrorImage = True
        Me.ToolStripButton4.Size = New System.Drawing.Size(23, 20)
        Me.ToolStripButton4.Text = "Move next"
        '
        'ToolStripButton5
        '
        Me.ToolStripButton5.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton5.Image = CType(resources.GetObject("ToolStripButton5.Image"), System.Drawing.Image)
        Me.ToolStripButton5.Name = "ToolStripButton5"
        Me.ToolStripButton5.RightToLeftAutoMirrorImage = True
        Me.ToolStripButton5.Size = New System.Drawing.Size(23, 20)
        Me.ToolStripButton5.Text = "Move last"
        '
        'ToolStripSeparator3
        '
        Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
        Me.ToolStripSeparator3.Size = New System.Drawing.Size(6, 23)
        '
        'SciencNavigatorSaveItem
        '
        Me.SciencNavigatorSaveItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.SciencNavigatorSaveItem.Image = CType(resources.GetObject("SciencNavigatorSaveItem.Image"), System.Drawing.Image)
        Me.SciencNavigatorSaveItem.Name = "SciencNavigatorSaveItem"
        Me.SciencNavigatorSaveItem.Size = New System.Drawing.Size(23, 20)
        Me.SciencNavigatorSaveItem.Text = "Save Data"
        '
        'gridMedScinFamily
        '
        Me.gridMedScinFamily.DataSource = Me.ScienceFamilyBindingSource
        Me.gridMedScinFamily.Dock = System.Windows.Forms.DockStyle.Right
        Me.gridMedScinFamily.Location = New System.Drawing.Point(252, 22)
        Me.gridMedScinFamily.MainView = Me.GridView3
        Me.gridMedScinFamily.Name = "gridMedScinFamily"
        Me.gridMedScinFamily.Size = New System.Drawing.Size(400, 222)
        Me.gridMedScinFamily.TabIndex = 3
        Me.gridMedScinFamily.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridView3})
        '
        'GridView3
        '
        Me.GridView3.GridControl = Me.gridMedScinFamily
        Me.GridView3.Name = "GridView3"
        '
        'btnDelMedScienFamily
        '
        Me.btnDelMedScienFamily.Appearance.Font = New System.Drawing.Font("Calibri", 10.0!, System.Drawing.FontStyle.Bold)
        Me.btnDelMedScienFamily.Appearance.Options.UseFont = True
        Me.btnDelMedScienFamily.Location = New System.Drawing.Point(12, 171)
        Me.btnDelMedScienFamily.Name = "btnDelMedScienFamily"
        Me.btnDelMedScienFamily.Size = New System.Drawing.Size(75, 23)
        Me.btnDelMedScienFamily.TabIndex = 2
        Me.btnDelMedScienFamily.Text = "Delete"
        '
        'btnAddMedScienFamily
        '
        Me.btnAddMedScienFamily.Appearance.Font = New System.Drawing.Font("Calibri", 10.0!, System.Drawing.FontStyle.Bold)
        Me.btnAddMedScienFamily.Appearance.Options.UseFont = True
        Me.btnAddMedScienFamily.Location = New System.Drawing.Point(12, 113)
        Me.btnAddMedScienFamily.Name = "btnAddMedScienFamily"
        Me.btnAddMedScienFamily.Size = New System.Drawing.Size(75, 23)
        Me.btnAddMedScienFamily.TabIndex = 2
        Me.btnAddMedScienFamily.Text = "Add"
        '
        'btnEditMedScienFamily
        '
        Me.btnEditMedScienFamily.Appearance.Font = New System.Drawing.Font("Calibri", 10.0!, System.Drawing.FontStyle.Bold)
        Me.btnEditMedScienFamily.Appearance.Options.UseFont = True
        Me.btnEditMedScienFamily.Location = New System.Drawing.Point(12, 142)
        Me.btnEditMedScienFamily.Name = "btnEditMedScienFamily"
        Me.btnEditMedScienFamily.Size = New System.Drawing.Size(75, 23)
        Me.btnEditMedScienFamily.TabIndex = 2
        Me.btnEditMedScienFamily.Text = "Edit"
        '
        'LabelControl4
        '
        Me.LabelControl4.Appearance.Font = New System.Drawing.Font("Calibri", 10.0!, System.Drawing.FontStyle.Bold)
        Me.LabelControl4.Appearance.Options.UseFont = True
        Me.LabelControl4.Location = New System.Drawing.Point(22, 82)
        Me.LabelControl4.Name = "LabelControl4"
        Me.LabelControl4.Size = New System.Drawing.Size(84, 15)
        Me.LabelControl4.TabIndex = 0
        Me.LabelControl4.Text = "Scinetific Name"
        '
        'cboMedFamily
        '
        Me.cboMedFamily.Location = New System.Drawing.Point(110, 44)
        Me.cboMedFamily.Name = "cboMedFamily"
        Me.cboMedFamily.Properties.Appearance.Font = New System.Drawing.Font("Calibri", 10.0!, System.Drawing.FontStyle.Bold)
        Me.cboMedFamily.Properties.Appearance.Options.UseFont = True
        Me.cboMedFamily.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cboMedFamily.Size = New System.Drawing.Size(100, 22)
        Me.cboMedFamily.TabIndex = 3
        '
        'txtScienceName
        '
        Me.txtScienceName.Location = New System.Drawing.Point(110, 79)
        Me.txtScienceName.Name = "txtScienceName"
        Me.txtScienceName.Properties.Appearance.Font = New System.Drawing.Font("Calibri", 10.0!, System.Drawing.FontStyle.Bold)
        Me.txtScienceName.Properties.Appearance.Options.UseFont = True
        Me.txtScienceName.Size = New System.Drawing.Size(100, 22)
        Me.txtScienceName.TabIndex = 1
        '
        'LabelControl5
        '
        Me.LabelControl5.Appearance.Font = New System.Drawing.Font("Calibri", 10.0!, System.Drawing.FontStyle.Bold)
        Me.LabelControl5.Appearance.Options.UseFont = True
        Me.LabelControl5.Location = New System.Drawing.Point(16, 47)
        Me.LabelControl5.Name = "LabelControl5"
        Me.LabelControl5.Size = New System.Drawing.Size(88, 15)
        Me.LabelControl5.TabIndex = 0
        Me.LabelControl5.Text = "Medicine Family"
        '
        'MedicForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1360, 736)
        Me.Controls.Add(Me.SidePanel1)
        Me.Controls.Add(Me.PanelControl2)
        Me.Controls.Add(Me.PanelControl1)
        Me.Name = "MedicForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Medicines Form"
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl1.ResumeLayout(False)
        CType(Me.GroupControl2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl2.ResumeLayout(False)
        Me.GroupControl2.PerformLayout()
        CType(Me.FamilyNavigator, System.ComponentModel.ISupportInitialize).EndInit()
        Me.FamilyNavigator.ResumeLayout(False)
        Me.FamilyNavigator.PerformLayout()
        CType(Me.MedicineFamilyBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MedicineGroupsBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gridMedFamily, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboMedGroup.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtMedFamily.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl1.ResumeLayout(False)
        Me.GroupControl1.PerformLayout()
        CType(Me.GrpNavigator, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GrpNavigator.ResumeLayout(False)
        Me.GrpNavigator.PerformLayout()
        CType(Me.gridMedGroups, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtMedicenGroup.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PanelControl2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl2.ResumeLayout(False)
        CType(Me.GroupControl6, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl6.ResumeLayout(False)
        Me.GroupControl6.PerformLayout()
        CType(Me.DoseNavigator, System.ComponentModel.ISupportInitialize).EndInit()
        Me.DoseNavigator.ResumeLayout(False)
        Me.DoseNavigator.PerformLayout()
        CType(Me.MedicineDozeBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MedicineShapeBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MedicineItemsBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ScienceFamilyBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gridMedDose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboShapeInfo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TextEdit6.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupControl5, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl5.ResumeLayout(False)
        Me.GroupControl5.PerformLayout()
        CType(Me.ShapeNavigator, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ShapeNavigator.ResumeLayout(False)
        Me.ShapeNavigator.PerformLayout()
        CType(Me.gridMedShape, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboMedItem.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtShapeInfo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtMedicineShape.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SidePanel1.ResumeLayout(False)
        CType(Me.GroupControl4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl4.ResumeLayout(False)
        Me.GroupControl4.PerformLayout()
        CType(Me.ItemsNavigator, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ItemsNavigator.ResumeLayout(False)
        Me.ItemsNavigator.PerformLayout()
        CType(Me.gridMedItems, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtNotes.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCompany.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCommon.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboDoseInfo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboScinetificName.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupControl3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl3.ResumeLayout(False)
        Me.GroupControl3.PerformLayout()
        CType(Me.ScienceNavigator, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ScienceNavigator.ResumeLayout(False)
        Me.ScienceNavigator.PerformLayout()
        CType(Me.gridMedScinFamily, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboMedFamily.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtScienceName.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents PanelControl1 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents PanelControl2 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents SidePanel1 As DevExpress.XtraEditors.SidePanel
    Friend WithEvents GroupControl1 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents GroupControl2 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents GroupControl6 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents GroupControl5 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents GroupControl4 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents GroupControl3 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents btnDelMedGroup As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnEditMedGroup As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnAddMedGroup As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents txtMedicenGroup As DevExpress.XtraEditors.TextEdit
    Friend WithEvents cboMedGroup As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents gridMedFamily As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridView2 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents gridMedGroups As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridView1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents gridMedDose As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridView6 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents gridMedShape As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridView5 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents gridMedItems As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridView4 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents gridMedScinFamily As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridView3 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents btnDelMedFamily As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnEditMedFamily As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnAddMedFamily As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents txtMedFamily As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl3 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents btnDelScinetificName As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents txtNotes As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtCompany As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtCommon As DevExpress.XtraEditors.TextEdit
    Friend WithEvents btnAddScinetificName As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LabelControl9 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents btnEditScinetificName As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LabelControl8 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents cboScinetificName As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents LabelControl7 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents btnDelMedScienFamily As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnAddMedScienFamily As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnEditMedScienFamily As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LabelControl4 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents cboMedFamily As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents txtScienceName As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents btnDelDose As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents cboShapeInfo As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents btnEditDose As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LabelControl13 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents TextEdit6 As DevExpress.XtraEditors.TextEdit
    Friend WithEvents btnAddDose As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnDelShape As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnAddShape As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents cboMedItem As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents btnEditShape As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents txtShapeInfo As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl12 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents txtMedicineShape As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl11 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl5 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl6 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl10 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl14 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents MedicineGroupsBindingSource As BindingSource
    Friend WithEvents MedicineFamilyBindingSource As BindingSource
    Friend WithEvents ScienceFamilyBindingSource As BindingSource
    Friend WithEvents MedicineItemsBindingSource As BindingSource
    Friend WithEvents MedicineShapeBindingSource As BindingSource
    Friend WithEvents MedicineDozeBindingSource As BindingSource
    Friend WithEvents GrpNavigator As BindingNavigator
    Friend WithEvents BindingNavigatorCountItem As ToolStripLabel
    Friend WithEvents GrpNavigatorDeleteItem As ToolStripButton
    Friend WithEvents BindingNavigatorMoveFirstItem As ToolStripButton
    Friend WithEvents BindingNavigatorMovePreviousItem As ToolStripButton
    Friend WithEvents BindingNavigatorSeparator As ToolStripSeparator
    Friend WithEvents BindingNavigatorPositionItem As ToolStripTextBox
    Friend WithEvents BindingNavigatorSeparator1 As ToolStripSeparator
    Friend WithEvents BindingNavigatorMoveNextItem As ToolStripButton
    Friend WithEvents BindingNavigatorMoveLastItem As ToolStripButton
    Friend WithEvents BindingNavigatorSeparator2 As ToolStripSeparator
    Friend WithEvents GrpNavigatorSaveItem As ToolStripButton
    Friend WithEvents FamilyNavigator As BindingNavigator
    Friend WithEvents ToolStripLabel4 As ToolStripLabel
    Friend WithEvents FamlyNavigatorDeleteItem As ToolStripButton
    Friend WithEvents ToolStripButton20 As ToolStripButton
    Friend WithEvents ToolStripButton21 As ToolStripButton
    Friend WithEvents ToolStripSeparator10 As ToolStripSeparator
    Friend WithEvents ToolStripTextBox4 As ToolStripTextBox
    Friend WithEvents ToolStripSeparator11 As ToolStripSeparator
    Friend WithEvents ToolStripButton22 As ToolStripButton
    Friend WithEvents ToolStripButton23 As ToolStripButton
    Friend WithEvents ToolStripSeparator12 As ToolStripSeparator
    Friend WithEvents FamlyNavigatorSaveItem As ToolStripButton
    Friend WithEvents ScienceNavigator As BindingNavigator
    Friend WithEvents ToolStripLabel1 As ToolStripLabel
    Friend WithEvents SciencNavigatorDeleteItem As ToolStripButton
    Friend WithEvents ToolStripButton2 As ToolStripButton
    Friend WithEvents ToolStripButton3 As ToolStripButton
    Friend WithEvents ToolStripSeparator1 As ToolStripSeparator
    Friend WithEvents ToolStripTextBox1 As ToolStripTextBox
    Friend WithEvents ToolStripSeparator2 As ToolStripSeparator
    Friend WithEvents ToolStripButton4 As ToolStripButton
    Friend WithEvents ToolStripButton5 As ToolStripButton
    Friend WithEvents ToolStripSeparator3 As ToolStripSeparator
    Friend WithEvents SciencNavigatorSaveItem As ToolStripButton
    Friend WithEvents ItemsNavigator As BindingNavigator
    Friend WithEvents ToolStripLabel5 As ToolStripLabel
    Friend WithEvents ItemsNavigatorDeleteItem As ToolStripButton
    Friend WithEvents ToolStripButton26 As ToolStripButton
    Friend WithEvents ToolStripButton27 As ToolStripButton
    Friend WithEvents ToolStripSeparator13 As ToolStripSeparator
    Friend WithEvents ToolStripTextBox5 As ToolStripTextBox
    Friend WithEvents ToolStripSeparator14 As ToolStripSeparator
    Friend WithEvents ToolStripButton28 As ToolStripButton
    Friend WithEvents ToolStripButton29 As ToolStripButton
    Friend WithEvents ToolStripSeparator15 As ToolStripSeparator
    Friend WithEvents ItemsNavigatorSaveItem As ToolStripButton
    Friend WithEvents ShapeNavigator As BindingNavigator
    Friend WithEvents ToolStripLabel2 As ToolStripLabel
    Friend WithEvents ShapeNavigatorDeleteItem As ToolStripButton
    Friend WithEvents ToolStripButton8 As ToolStripButton
    Friend WithEvents ToolStripButton9 As ToolStripButton
    Friend WithEvents ToolStripSeparator4 As ToolStripSeparator
    Friend WithEvents ToolStripTextBox2 As ToolStripTextBox
    Friend WithEvents ToolStripSeparator5 As ToolStripSeparator
    Friend WithEvents ToolStripButton10 As ToolStripButton
    Friend WithEvents ToolStripButton11 As ToolStripButton
    Friend WithEvents ToolStripSeparator6 As ToolStripSeparator
    Friend WithEvents ShapeNavigatorSaveItem As ToolStripButton
    Friend WithEvents DoseNavigator As BindingNavigator
    Friend WithEvents ToolStripLabel3 As ToolStripLabel
    Friend WithEvents DoseNavigatorDeleteItem As ToolStripButton
    Friend WithEvents ToolStripButton14 As ToolStripButton
    Friend WithEvents ToolStripButton15 As ToolStripButton
    Friend WithEvents ToolStripSeparator7 As ToolStripSeparator
    Friend WithEvents ToolStripTextBox3 As ToolStripTextBox
    Friend WithEvents ToolStripSeparator8 As ToolStripSeparator
    Friend WithEvents ToolStripButton16 As ToolStripButton
    Friend WithEvents ToolStripButton17 As ToolStripButton
    Friend WithEvents ToolStripSeparator9 As ToolStripSeparator
    Friend WithEvents DoseNavigatorSaveItem As ToolStripButton
    Friend WithEvents cboDoseInfo As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents MedicineFamilyCombo1 As MedicineFamilyCombo
    Friend WithEvents MedicineGroupsCombo1 As MedicineGroupsCombo
    Friend WithEvents MedicineDozeCombo1 As MedicineDozeCombo
    Friend WithEvents MedicineShapeCombo1 As MedicineShapeCombo
    Friend WithEvents MedicineItemsCombo1 As MedicineItemsCombo
    Friend WithEvents MedScienceFamilyCombo1 As MedScienceFamilyCombo
End Class
