<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class MedicFormDS
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MedicFormDS))
        Me.PanelControl1 = New DevExpress.XtraEditors.PanelControl()
        Me.GroupControl2 = New DevExpress.XtraEditors.GroupControl()
        Me.LabelControl16 = New DevExpress.XtraEditors.LabelControl()
        Me.GroupCombo = New System.Windows.Forms.ComboBox()
        Me.MedicineGroupsBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.MedDs = New DentistX.MedDs()
        Me.gridMedFamily = New DevExpress.XtraGrid.GridControl()
        Me.MedicineFamilyBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.GridView2 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.colRowNum2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colSubCatID = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colMedicineID1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colMedicineSubCat = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.btSaveFamly = New DevExpress.XtraEditors.SimpleButton()
        Me.btEditFamly = New DevExpress.XtraEditors.SimpleButton()
        Me.btAddFamily = New DevExpress.XtraEditors.SimpleButton()
        Me.MedicineFamily = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl3 = New DevExpress.XtraEditors.LabelControl()
        Me.FamlyNavigator = New System.Windows.Forms.BindingNavigator(Me.components)
        Me.ToolStripLabel4 = New System.Windows.Forms.ToolStripLabel()
        Me.ToolStripButton19 = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButton20 = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButton21 = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator10 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripTextBox4 = New System.Windows.Forms.ToolStripTextBox()
        Me.ToolStripSeparator11 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripButton22 = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButton23 = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator12 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripButton24 = New System.Windows.Forms.ToolStripButton()
        Me.GroupControl1 = New DevExpress.XtraEditors.GroupControl()
        Me.MedicineGroupsDataGridView = New DevExpress.XtraGrid.GridControl()
        Me.GridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.colRowNum1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colMedicineID = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colMedicineFamily = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.btSaveGrp = New DevExpress.XtraEditors.SimpleButton()
        Me.btEditGrp = New DevExpress.XtraEditors.SimpleButton()
        Me.btAddGrp = New DevExpress.XtraEditors.SimpleButton()
        Me.MedicenGroup = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl15 = New DevExpress.XtraEditors.LabelControl()
        Me.GrpNavigator = New System.Windows.Forms.BindingNavigator(Me.components)
        Me.BindingNavigatorCountItem = New System.Windows.Forms.ToolStripLabel()
        Me.BindingNavigatorDeleteItem = New System.Windows.Forms.ToolStripButton()
        Me.BindingNavigatorMoveFirstItem = New System.Windows.Forms.ToolStripButton()
        Me.BindingNavigatorMovePreviousItem = New System.Windows.Forms.ToolStripButton()
        Me.BindingNavigatorSeparator = New System.Windows.Forms.ToolStripSeparator()
        Me.BindingNavigatorPositionItem = New System.Windows.Forms.ToolStripTextBox()
        Me.BindingNavigatorSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.BindingNavigatorMoveNextItem = New System.Windows.Forms.ToolStripButton()
        Me.BindingNavigatorMoveLastItem = New System.Windows.Forms.ToolStripButton()
        Me.BindingNavigatorSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.PatientBindingNavigatorSaveItem = New System.Windows.Forms.ToolStripButton()
        Me.PanelControl2 = New DevExpress.XtraEditors.PanelControl()
        Me.GroupControl6 = New DevExpress.XtraEditors.GroupControl()
        Me.LabelControl19 = New DevExpress.XtraEditors.LabelControl()
        Me.shapeCombo = New System.Windows.Forms.ComboBox()
        Me.MedicineShapeBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.MedicineItemsBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.ScienceFamilyBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.gridMedDose = New DevExpress.XtraGrid.GridControl()
        Me.MedicineDozeBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.GridView6 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.colRowNum6 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colDozeID = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colShapeID1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colDoze = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.btSaveDoze = New DevExpress.XtraEditors.SimpleButton()
        Me.btEditDoze = New DevExpress.XtraEditors.SimpleButton()
        Me.LabelControl13 = New DevExpress.XtraEditors.LabelControl()
        Me.Doze = New DevExpress.XtraEditors.TextEdit()
        Me.btAddDoze = New DevExpress.XtraEditors.SimpleButton()
        Me.LabelControl14 = New DevExpress.XtraEditors.LabelControl()
        Me.DozeNavigator = New System.Windows.Forms.BindingNavigator(Me.components)
        Me.ToolStripLabel3 = New System.Windows.Forms.ToolStripLabel()
        Me.ToolStripButton13 = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButton14 = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButton15 = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator7 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripTextBox3 = New System.Windows.Forms.ToolStripTextBox()
        Me.ToolStripSeparator8 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripButton16 = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButton17 = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator9 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripButton18 = New System.Windows.Forms.ToolStripButton()
        Me.GroupControl5 = New DevExpress.XtraEditors.GroupControl()
        Me.itemCombo = New System.Windows.Forms.ComboBox()
        Me.gridMedShape = New DevExpress.XtraGrid.GridControl()
        Me.GridView5 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.colRowNum5 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colShapeID = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colMedicineItemID1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colMedicineShape = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colShapeInfo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.btSaveShape = New DevExpress.XtraEditors.SimpleButton()
        Me.btAddShape = New DevExpress.XtraEditors.SimpleButton()
        Me.LabelControl10 = New DevExpress.XtraEditors.LabelControl()
        Me.btEditShape = New DevExpress.XtraEditors.SimpleButton()
        Me.ShapeInfotxt = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl12 = New DevExpress.XtraEditors.LabelControl()
        Me.Shape = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl11 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl20 = New DevExpress.XtraEditors.LabelControl()
        Me.ShapeNavigator = New System.Windows.Forms.BindingNavigator(Me.components)
        Me.ToolStripLabel2 = New System.Windows.Forms.ToolStripLabel()
        Me.ToolStripButton7 = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButton8 = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButton9 = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripTextBox2 = New System.Windows.Forms.ToolStripTextBox()
        Me.ToolStripSeparator5 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripButton10 = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButton11 = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator6 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripButton12 = New System.Windows.Forms.ToolStripButton()
        Me.SidePanel1 = New DevExpress.XtraEditors.SidePanel()
        Me.GroupControl4 = New DevExpress.XtraEditors.GroupControl()
        Me.LabelControl18 = New DevExpress.XtraEditors.LabelControl()
        Me.ScientCombo = New System.Windows.Forms.ComboBox()
        Me.gridMedItems = New DevExpress.XtraGrid.GridControl()
        Me.GridView4 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.colRowNum4 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colMedicineItemID = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colScincID1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colCommName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colCompany = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colNotes = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.btSaveMedic = New DevExpress.XtraEditors.SimpleButton()
        Me.CompanyTxt = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl6 = New DevExpress.XtraEditors.LabelControl()
        Me.Commercial = New DevExpress.XtraEditors.TextEdit()
        Me.btAddMedicine = New DevExpress.XtraEditors.SimpleButton()
        Me.LabelControl9 = New DevExpress.XtraEditors.LabelControl()
        Me.btEditMedic = New DevExpress.XtraEditors.SimpleButton()
        Me.LabelControl8 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl7 = New DevExpress.XtraEditors.LabelControl()
        Me.MedicNote = New DevExpress.XtraEditors.TextEdit()
        Me.ItemsNavigator = New System.Windows.Forms.BindingNavigator(Me.components)
        Me.ToolStripLabel5 = New System.Windows.Forms.ToolStripLabel()
        Me.ToolStripButton25 = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButton26 = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButton27 = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator13 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripTextBox5 = New System.Windows.Forms.ToolStripTextBox()
        Me.ToolStripSeparator14 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripButton28 = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButton29 = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator15 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripButton30 = New System.Windows.Forms.ToolStripButton()
        Me.GroupControl3 = New DevExpress.XtraEditors.GroupControl()
        Me.FamilyCombo = New System.Windows.Forms.ComboBox()
        Me.gridMedScinFamily = New DevExpress.XtraGrid.GridControl()
        Me.GridView3 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.colRowNum3 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colScincID = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colSubCatID1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colScienceName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.btSaveScinc = New DevExpress.XtraEditors.SimpleButton()
        Me.btAddScience = New DevExpress.XtraEditors.SimpleButton()
        Me.btEditScinc = New DevExpress.XtraEditors.SimpleButton()
        Me.LabelControl4 = New DevExpress.XtraEditors.LabelControl()
        Me.Scintific = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl5 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl17 = New DevExpress.XtraEditors.LabelControl()
        Me.SciencNavigator = New System.Windows.Forms.BindingNavigator(Me.components)
        Me.ToolStripLabel1 = New System.Windows.Forms.ToolStripLabel()
        Me.ToolStripButton1 = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButton2 = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButton3 = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripTextBox1 = New System.Windows.Forms.ToolStripTextBox()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripButton4 = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButton5 = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripButton6 = New System.Windows.Forms.ToolStripButton()
        Me.Qrs = New DentistX.MedDsTableAdapters.MedQueries()
        Me.TableAdapterManager = New DentistX.MedDsTableAdapters.TableAdapterManager()
        Me.MedicineDozeTableAdapter = New DentistX.MedDsTableAdapters.MedicineDozeTableAdapter()
        Me.MedicineFamilyTableAdapter = New DentistX.MedDsTableAdapters.MedicineFamilyTableAdapter()
        Me.MedicineGroupsTableAdapter = New DentistX.MedDsTableAdapters.MedicineGroupsTableAdapter()
        Me.MedicineItemsTableAdapter = New DentistX.MedDsTableAdapters.MedicineItemsTableAdapter()
        Me.MedicineShapeTableAdapter = New DentistX.MedDsTableAdapters.MedicineShapeTableAdapter()
        Me.MedScienceFamilyTableAdapter = New DentistX.MedDsTableAdapters.MedScienceFamilyTableAdapter()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl1.SuspendLayout()
        CType(Me.GroupControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl2.SuspendLayout()
        CType(Me.MedicineGroupsBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MedDs, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gridMedFamily, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MedicineFamilyBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MedicineFamily.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FamlyNavigator, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.FamlyNavigator.SuspendLayout()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl1.SuspendLayout()
        CType(Me.MedicineGroupsDataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MedicenGroup.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GrpNavigator, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GrpNavigator.SuspendLayout()
        CType(Me.PanelControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl2.SuspendLayout()
        CType(Me.GroupControl6, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl6.SuspendLayout()
        CType(Me.MedicineShapeBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MedicineItemsBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ScienceFamilyBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gridMedDose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MedicineDozeBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Doze.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DozeNavigator, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.DozeNavigator.SuspendLayout()
        CType(Me.GroupControl5, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl5.SuspendLayout()
        CType(Me.gridMedShape, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ShapeInfotxt.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Shape.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ShapeNavigator, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ShapeNavigator.SuspendLayout()
        Me.SidePanel1.SuspendLayout()
        CType(Me.GroupControl4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl4.SuspendLayout()
        CType(Me.gridMedItems, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CompanyTxt.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Commercial.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MedicNote.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ItemsNavigator, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ItemsNavigator.SuspendLayout()
        CType(Me.GroupControl3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl3.SuspendLayout()
        CType(Me.gridMedScinFamily, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Scintific.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SciencNavigator, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SciencNavigator.SuspendLayout()
        Me.SuspendLayout()
        '
        'PanelControl1
        '
        resources.ApplyResources(Me.PanelControl1, "PanelControl1")
        Me.PanelControl1.Controls.Add(Me.GroupControl2)
        Me.PanelControl1.Controls.Add(Me.GroupControl1)
        Me.PanelControl1.Name = "PanelControl1"
        '
        'GroupControl2
        '
        resources.ApplyResources(Me.GroupControl2, "GroupControl2")
        Me.GroupControl2.AppearanceCaption.Font = CType(resources.GetObject("GroupControl2.AppearanceCaption.Font"), System.Drawing.Font)
        Me.GroupControl2.AppearanceCaption.Options.UseFont = True
        Me.GroupControl2.Controls.Add(Me.LabelControl16)
        Me.GroupControl2.Controls.Add(Me.GroupCombo)
        Me.GroupControl2.Controls.Add(Me.gridMedFamily)
        Me.GroupControl2.Controls.Add(Me.btSaveFamly)
        Me.GroupControl2.Controls.Add(Me.btEditFamly)
        Me.GroupControl2.Controls.Add(Me.btAddFamily)
        Me.GroupControl2.Controls.Add(Me.MedicineFamily)
        Me.GroupControl2.Controls.Add(Me.LabelControl2)
        Me.GroupControl2.Controls.Add(Me.LabelControl3)
        Me.GroupControl2.Controls.Add(Me.FamlyNavigator)
        Me.GroupControl2.Name = "GroupControl2"
        '
        'LabelControl16
        '
        resources.ApplyResources(Me.LabelControl16, "LabelControl16")
        Me.LabelControl16.Appearance.Font = CType(resources.GetObject("LabelControl16.Appearance.Font"), System.Drawing.Font)
        Me.LabelControl16.Appearance.ForeColor = System.Drawing.Color.Green
        Me.LabelControl16.Appearance.Options.UseFont = True
        Me.LabelControl16.Appearance.Options.UseForeColor = True
        Me.LabelControl16.Appearance.Options.UseTextOptions = True
        Me.LabelControl16.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.LabelControl16.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.LabelControl16.Name = "LabelControl16"
        '
        'GroupCombo
        '
        resources.ApplyResources(Me.GroupCombo, "GroupCombo")
        Me.GroupCombo.DataSource = Me.MedicineGroupsBindingSource
        Me.GroupCombo.DisplayMember = "MedicineFamily"
        Me.GroupCombo.FormattingEnabled = True
        Me.GroupCombo.Name = "GroupCombo"
        Me.GroupCombo.ValueMember = "MedicineID"
        '
        'MedicineGroupsBindingSource
        '
        Me.MedicineGroupsBindingSource.DataMember = "MedicineGroups"
        Me.MedicineGroupsBindingSource.DataSource = Me.MedDs
        '
        'MedDs
        '
        Me.MedDs.DataSetName = "MedDs"
        Me.MedDs.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'gridMedFamily
        '
        resources.ApplyResources(Me.gridMedFamily, "gridMedFamily")
        Me.gridMedFamily.DataSource = Me.MedicineFamilyBindingSource
        Me.gridMedFamily.EmbeddedNavigator.AccessibleDescription = resources.GetString("gridMedFamily.EmbeddedNavigator.AccessibleDescription")
        Me.gridMedFamily.EmbeddedNavigator.AccessibleName = resources.GetString("gridMedFamily.EmbeddedNavigator.AccessibleName")
        Me.gridMedFamily.EmbeddedNavigator.AllowHtmlTextInToolTip = CType(resources.GetObject("gridMedFamily.EmbeddedNavigator.AllowHtmlTextInToolTip"), DevExpress.Utils.DefaultBoolean)
        Me.gridMedFamily.EmbeddedNavigator.Anchor = CType(resources.GetObject("gridMedFamily.EmbeddedNavigator.Anchor"), System.Windows.Forms.AnchorStyles)
        Me.gridMedFamily.EmbeddedNavigator.AutoSize = CType(resources.GetObject("gridMedFamily.EmbeddedNavigator.AutoSize"), Boolean)
        Me.gridMedFamily.EmbeddedNavigator.BackgroundImage = CType(resources.GetObject("gridMedFamily.EmbeddedNavigator.BackgroundImage"), System.Drawing.Image)
        Me.gridMedFamily.EmbeddedNavigator.BackgroundImageLayout = CType(resources.GetObject("gridMedFamily.EmbeddedNavigator.BackgroundImageLayout"), System.Windows.Forms.ImageLayout)
        Me.gridMedFamily.EmbeddedNavigator.ImeMode = CType(resources.GetObject("gridMedFamily.EmbeddedNavigator.ImeMode"), System.Windows.Forms.ImeMode)
        Me.gridMedFamily.EmbeddedNavigator.MaximumSize = CType(resources.GetObject("gridMedFamily.EmbeddedNavigator.MaximumSize"), System.Drawing.Size)
        Me.gridMedFamily.EmbeddedNavigator.TextLocation = CType(resources.GetObject("gridMedFamily.EmbeddedNavigator.TextLocation"), DevExpress.XtraEditors.NavigatorButtonsTextLocation)
        Me.gridMedFamily.EmbeddedNavigator.ToolTip = resources.GetString("gridMedFamily.EmbeddedNavigator.ToolTip")
        Me.gridMedFamily.EmbeddedNavigator.ToolTipIconType = CType(resources.GetObject("gridMedFamily.EmbeddedNavigator.ToolTipIconType"), DevExpress.Utils.ToolTipIconType)
        Me.gridMedFamily.EmbeddedNavigator.ToolTipTitle = resources.GetString("gridMedFamily.EmbeddedNavigator.ToolTipTitle")
        Me.gridMedFamily.MainView = Me.GridView2
        Me.gridMedFamily.Name = "gridMedFamily"
        Me.gridMedFamily.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridView2})
        '
        'MedicineFamilyBindingSource
        '
        Me.MedicineFamilyBindingSource.DataMember = "FK_MedicineFamily_MedicineGroups"
        Me.MedicineFamilyBindingSource.DataSource = Me.MedicineGroupsBindingSource
        '
        'GridView2
        '
        resources.ApplyResources(Me.GridView2, "GridView2")
        Me.GridView2.Appearance.HeaderPanel.Font = CType(resources.GetObject("GridView2.Appearance.HeaderPanel.Font"), System.Drawing.Font)
        Me.GridView2.Appearance.HeaderPanel.Options.UseFont = True
        Me.GridView2.Appearance.Row.Font = CType(resources.GetObject("GridView2.Appearance.Row.Font"), System.Drawing.Font)
        Me.GridView2.Appearance.Row.Options.UseFont = True
        Me.GridView2.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.colRowNum2, Me.colSubCatID, Me.colMedicineID1, Me.colMedicineSubCat})
        Me.GridView2.GridControl = Me.gridMedFamily
        Me.GridView2.Name = "GridView2"
        Me.GridView2.OptionsDetail.EnableMasterViewMode = False
        Me.GridView2.OptionsView.EnableAppearanceEvenRow = True
        Me.GridView2.OptionsView.EnableAppearanceOddRow = True
        Me.GridView2.OptionsView.ShowDetailButtons = False
        '
        'colRowNum2
        '
        resources.ApplyResources(Me.colRowNum2, "colRowNum2")
        Me.colRowNum2.FieldName = "colRowNum2"
        Me.colRowNum2.ImageOptions.ImageKey = resources.GetString("colRowNum2.ImageOptions.ImageKey")
        Me.colRowNum2.Name = "colRowNum2"
        Me.colRowNum2.UnboundDataType = GetType(Integer)
        '
        'colSubCatID
        '
        resources.ApplyResources(Me.colSubCatID, "colSubCatID")
        Me.colSubCatID.FieldName = "SubCatID"
        Me.colSubCatID.ImageOptions.ImageKey = resources.GetString("colSubCatID.ImageOptions.ImageKey")
        Me.colSubCatID.Name = "colSubCatID"
        '
        'colMedicineID1
        '
        resources.ApplyResources(Me.colMedicineID1, "colMedicineID1")
        Me.colMedicineID1.FieldName = "MedicineID"
        Me.colMedicineID1.ImageOptions.ImageKey = resources.GetString("colMedicineID1.ImageOptions.ImageKey")
        Me.colMedicineID1.Name = "colMedicineID1"
        '
        'colMedicineSubCat
        '
        resources.ApplyResources(Me.colMedicineSubCat, "colMedicineSubCat")
        Me.colMedicineSubCat.FieldName = "MedicineSubCat"
        Me.colMedicineSubCat.ImageOptions.ImageKey = resources.GetString("colMedicineSubCat.ImageOptions.ImageKey")
        Me.colMedicineSubCat.Name = "colMedicineSubCat"
        '
        'btSaveFamly
        '
        resources.ApplyResources(Me.btSaveFamly, "btSaveFamly")
        Me.btSaveFamly.Appearance.Font = CType(resources.GetObject("btSaveFamly.Appearance.Font"), System.Drawing.Font)
        Me.btSaveFamly.Appearance.Options.UseFont = True
        Me.btSaveFamly.ImageOptions.ImageKey = resources.GetString("btSaveFamly.ImageOptions.ImageKey")
        Me.btSaveFamly.Name = "btSaveFamly"
        '
        'btEditFamly
        '
        resources.ApplyResources(Me.btEditFamly, "btEditFamly")
        Me.btEditFamly.Appearance.Font = CType(resources.GetObject("btEditFamly.Appearance.Font"), System.Drawing.Font)
        Me.btEditFamly.Appearance.Options.UseFont = True
        Me.btEditFamly.ImageOptions.ImageKey = resources.GetString("btEditFamly.ImageOptions.ImageKey")
        Me.btEditFamly.Name = "btEditFamly"
        '
        'btAddFamily
        '
        resources.ApplyResources(Me.btAddFamily, "btAddFamily")
        Me.btAddFamily.Appearance.Font = CType(resources.GetObject("btAddFamily.Appearance.Font"), System.Drawing.Font)
        Me.btAddFamily.Appearance.Options.UseFont = True
        Me.btAddFamily.ImageOptions.ImageKey = resources.GetString("btAddFamily.ImageOptions.ImageKey")
        Me.btAddFamily.Name = "btAddFamily"
        '
        'MedicineFamily
        '
        resources.ApplyResources(Me.MedicineFamily, "MedicineFamily")
        Me.MedicineFamily.Name = "MedicineFamily"
        Me.MedicineFamily.Properties.Appearance.Font = CType(resources.GetObject("MedicineFamily.Properties.Appearance.Font"), System.Drawing.Font)
        Me.MedicineFamily.Properties.Appearance.Options.UseFont = True
        '
        'LabelControl2
        '
        resources.ApplyResources(Me.LabelControl2, "LabelControl2")
        Me.LabelControl2.Appearance.Font = CType(resources.GetObject("LabelControl2.Appearance.Font"), System.Drawing.Font)
        Me.LabelControl2.Appearance.Options.UseFont = True
        Me.LabelControl2.Name = "LabelControl2"
        '
        'LabelControl3
        '
        resources.ApplyResources(Me.LabelControl3, "LabelControl3")
        Me.LabelControl3.Appearance.Font = CType(resources.GetObject("LabelControl3.Appearance.Font"), System.Drawing.Font)
        Me.LabelControl3.Appearance.Options.UseFont = True
        Me.LabelControl3.Name = "LabelControl3"
        '
        'FamlyNavigator
        '
        resources.ApplyResources(Me.FamlyNavigator, "FamlyNavigator")
        Me.FamlyNavigator.AddNewItem = Nothing
        Me.FamlyNavigator.BindingSource = Me.MedicineFamilyBindingSource
        Me.FamlyNavigator.CountItem = Me.ToolStripLabel4
        Me.FamlyNavigator.DeleteItem = Me.ToolStripButton19
        Me.FamlyNavigator.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripButton20, Me.ToolStripButton21, Me.ToolStripSeparator10, Me.ToolStripTextBox4, Me.ToolStripLabel4, Me.ToolStripSeparator11, Me.ToolStripButton22, Me.ToolStripButton23, Me.ToolStripSeparator12, Me.ToolStripButton19, Me.ToolStripButton24})
        Me.FamlyNavigator.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow
        Me.FamlyNavigator.MoveFirstItem = Me.ToolStripButton20
        Me.FamlyNavigator.MoveLastItem = Me.ToolStripButton23
        Me.FamlyNavigator.MoveNextItem = Me.ToolStripButton22
        Me.FamlyNavigator.MovePreviousItem = Me.ToolStripButton21
        Me.FamlyNavigator.Name = "FamlyNavigator"
        Me.FamlyNavigator.PositionItem = Me.ToolStripTextBox4
        '
        'ToolStripLabel4
        '
        resources.ApplyResources(Me.ToolStripLabel4, "ToolStripLabel4")
        Me.ToolStripLabel4.Name = "ToolStripLabel4"
        '
        'ToolStripButton19
        '
        resources.ApplyResources(Me.ToolStripButton19, "ToolStripButton19")
        Me.ToolStripButton19.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton19.Name = "ToolStripButton19"
        '
        'ToolStripButton20
        '
        resources.ApplyResources(Me.ToolStripButton20, "ToolStripButton20")
        Me.ToolStripButton20.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton20.Name = "ToolStripButton20"
        '
        'ToolStripButton21
        '
        resources.ApplyResources(Me.ToolStripButton21, "ToolStripButton21")
        Me.ToolStripButton21.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton21.Name = "ToolStripButton21"
        '
        'ToolStripSeparator10
        '
        resources.ApplyResources(Me.ToolStripSeparator10, "ToolStripSeparator10")
        Me.ToolStripSeparator10.Name = "ToolStripSeparator10"
        '
        'ToolStripTextBox4
        '
        resources.ApplyResources(Me.ToolStripTextBox4, "ToolStripTextBox4")
        Me.ToolStripTextBox4.Name = "ToolStripTextBox4"
        '
        'ToolStripSeparator11
        '
        resources.ApplyResources(Me.ToolStripSeparator11, "ToolStripSeparator11")
        Me.ToolStripSeparator11.Name = "ToolStripSeparator11"
        '
        'ToolStripButton22
        '
        resources.ApplyResources(Me.ToolStripButton22, "ToolStripButton22")
        Me.ToolStripButton22.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton22.Name = "ToolStripButton22"
        '
        'ToolStripButton23
        '
        resources.ApplyResources(Me.ToolStripButton23, "ToolStripButton23")
        Me.ToolStripButton23.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton23.Name = "ToolStripButton23"
        '
        'ToolStripSeparator12
        '
        resources.ApplyResources(Me.ToolStripSeparator12, "ToolStripSeparator12")
        Me.ToolStripSeparator12.Name = "ToolStripSeparator12"
        '
        'ToolStripButton24
        '
        resources.ApplyResources(Me.ToolStripButton24, "ToolStripButton24")
        Me.ToolStripButton24.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton24.Name = "ToolStripButton24"
        '
        'GroupControl1
        '
        resources.ApplyResources(Me.GroupControl1, "GroupControl1")
        Me.GroupControl1.AppearanceCaption.Font = CType(resources.GetObject("GroupControl1.AppearanceCaption.Font"), System.Drawing.Font)
        Me.GroupControl1.AppearanceCaption.Options.UseFont = True
        Me.GroupControl1.Controls.Add(Me.MedicineGroupsDataGridView)
        Me.GroupControl1.Controls.Add(Me.btSaveGrp)
        Me.GroupControl1.Controls.Add(Me.btEditGrp)
        Me.GroupControl1.Controls.Add(Me.btAddGrp)
        Me.GroupControl1.Controls.Add(Me.MedicenGroup)
        Me.GroupControl1.Controls.Add(Me.LabelControl1)
        Me.GroupControl1.Controls.Add(Me.LabelControl15)
        Me.GroupControl1.Controls.Add(Me.GrpNavigator)
        Me.GroupControl1.Name = "GroupControl1"
        '
        'MedicineGroupsDataGridView
        '
        resources.ApplyResources(Me.MedicineGroupsDataGridView, "MedicineGroupsDataGridView")
        Me.MedicineGroupsDataGridView.DataSource = Me.MedicineGroupsBindingSource
        Me.MedicineGroupsDataGridView.EmbeddedNavigator.AccessibleDescription = resources.GetString("MedicineGroupsDataGridView.EmbeddedNavigator.AccessibleDescription")
        Me.MedicineGroupsDataGridView.EmbeddedNavigator.AccessibleName = resources.GetString("MedicineGroupsDataGridView.EmbeddedNavigator.AccessibleName")
        Me.MedicineGroupsDataGridView.EmbeddedNavigator.AllowHtmlTextInToolTip = CType(resources.GetObject("MedicineGroupsDataGridView.EmbeddedNavigator.AllowHtmlTextInToolTip"), DevExpress.Utils.DefaultBoolean)
        Me.MedicineGroupsDataGridView.EmbeddedNavigator.Anchor = CType(resources.GetObject("MedicineGroupsDataGridView.EmbeddedNavigator.Anchor"), System.Windows.Forms.AnchorStyles)
        Me.MedicineGroupsDataGridView.EmbeddedNavigator.AutoSize = CType(resources.GetObject("MedicineGroupsDataGridView.EmbeddedNavigator.AutoSize"), Boolean)
        Me.MedicineGroupsDataGridView.EmbeddedNavigator.BackgroundImage = CType(resources.GetObject("MedicineGroupsDataGridView.EmbeddedNavigator.BackgroundImage"), System.Drawing.Image)
        Me.MedicineGroupsDataGridView.EmbeddedNavigator.BackgroundImageLayout = CType(resources.GetObject("MedicineGroupsDataGridView.EmbeddedNavigator.BackgroundImageLayout"), System.Windows.Forms.ImageLayout)
        Me.MedicineGroupsDataGridView.EmbeddedNavigator.ImeMode = CType(resources.GetObject("MedicineGroupsDataGridView.EmbeddedNavigator.ImeMode"), System.Windows.Forms.ImeMode)
        Me.MedicineGroupsDataGridView.EmbeddedNavigator.MaximumSize = CType(resources.GetObject("MedicineGroupsDataGridView.EmbeddedNavigator.MaximumSize"), System.Drawing.Size)
        Me.MedicineGroupsDataGridView.EmbeddedNavigator.TextLocation = CType(resources.GetObject("MedicineGroupsDataGridView.EmbeddedNavigator.TextLocation"), DevExpress.XtraEditors.NavigatorButtonsTextLocation)
        Me.MedicineGroupsDataGridView.EmbeddedNavigator.ToolTip = resources.GetString("MedicineGroupsDataGridView.EmbeddedNavigator.ToolTip")
        Me.MedicineGroupsDataGridView.EmbeddedNavigator.ToolTipIconType = CType(resources.GetObject("MedicineGroupsDataGridView.EmbeddedNavigator.ToolTipIconType"), DevExpress.Utils.ToolTipIconType)
        Me.MedicineGroupsDataGridView.EmbeddedNavigator.ToolTipTitle = resources.GetString("MedicineGroupsDataGridView.EmbeddedNavigator.ToolTipTitle")
        Me.MedicineGroupsDataGridView.MainView = Me.GridView1
        Me.MedicineGroupsDataGridView.Name = "MedicineGroupsDataGridView"
        Me.MedicineGroupsDataGridView.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridView1})
        '
        'GridView1
        '
        resources.ApplyResources(Me.GridView1, "GridView1")
        Me.GridView1.Appearance.HeaderPanel.Font = CType(resources.GetObject("GridView1.Appearance.HeaderPanel.Font"), System.Drawing.Font)
        Me.GridView1.Appearance.HeaderPanel.Options.UseFont = True
        Me.GridView1.Appearance.Row.Font = CType(resources.GetObject("GridView1.Appearance.Row.Font"), System.Drawing.Font)
        Me.GridView1.Appearance.Row.Options.UseFont = True
        Me.GridView1.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.colRowNum1, Me.colMedicineID, Me.colMedicineFamily})
        Me.GridView1.GridControl = Me.MedicineGroupsDataGridView
        Me.GridView1.Name = "GridView1"
        Me.GridView1.OptionsDetail.EnableMasterViewMode = False
        Me.GridView1.OptionsView.EnableAppearanceEvenRow = True
        Me.GridView1.OptionsView.EnableAppearanceOddRow = True
        Me.GridView1.OptionsView.ShowDetailButtons = False
        '
        'colRowNum1
        '
        resources.ApplyResources(Me.colRowNum1, "colRowNum1")
        Me.colRowNum1.FieldName = "colRowNum1"
        Me.colRowNum1.ImageOptions.ImageKey = resources.GetString("colRowNum1.ImageOptions.ImageKey")
        Me.colRowNum1.Name = "colRowNum1"
        Me.colRowNum1.UnboundDataType = GetType(Integer)
        '
        'colMedicineID
        '
        resources.ApplyResources(Me.colMedicineID, "colMedicineID")
        Me.colMedicineID.FieldName = "MedicineID"
        Me.colMedicineID.ImageOptions.ImageKey = resources.GetString("colMedicineID.ImageOptions.ImageKey")
        Me.colMedicineID.Name = "colMedicineID"
        '
        'colMedicineFamily
        '
        resources.ApplyResources(Me.colMedicineFamily, "colMedicineFamily")
        Me.colMedicineFamily.FieldName = "MedicineFamily"
        Me.colMedicineFamily.ImageOptions.ImageKey = resources.GetString("colMedicineFamily.ImageOptions.ImageKey")
        Me.colMedicineFamily.Name = "colMedicineFamily"
        '
        'btSaveGrp
        '
        resources.ApplyResources(Me.btSaveGrp, "btSaveGrp")
        Me.btSaveGrp.Appearance.Font = CType(resources.GetObject("btSaveGrp.Appearance.Font"), System.Drawing.Font)
        Me.btSaveGrp.Appearance.Options.UseFont = True
        Me.btSaveGrp.ImageOptions.ImageKey = resources.GetString("btSaveGrp.ImageOptions.ImageKey")
        Me.btSaveGrp.Name = "btSaveGrp"
        '
        'btEditGrp
        '
        resources.ApplyResources(Me.btEditGrp, "btEditGrp")
        Me.btEditGrp.Appearance.Font = CType(resources.GetObject("btEditGrp.Appearance.Font"), System.Drawing.Font)
        Me.btEditGrp.Appearance.Options.UseFont = True
        Me.btEditGrp.ImageOptions.ImageKey = resources.GetString("btEditGrp.ImageOptions.ImageKey")
        Me.btEditGrp.Name = "btEditGrp"
        '
        'btAddGrp
        '
        resources.ApplyResources(Me.btAddGrp, "btAddGrp")
        Me.btAddGrp.Appearance.Font = CType(resources.GetObject("btAddGrp.Appearance.Font"), System.Drawing.Font)
        Me.btAddGrp.Appearance.Options.UseFont = True
        Me.btAddGrp.ImageOptions.ImageKey = resources.GetString("btAddGrp.ImageOptions.ImageKey")
        Me.btAddGrp.Name = "btAddGrp"
        '
        'MedicenGroup
        '
        resources.ApplyResources(Me.MedicenGroup, "MedicenGroup")
        Me.MedicenGroup.Name = "MedicenGroup"
        Me.MedicenGroup.Properties.Appearance.Font = CType(resources.GetObject("MedicenGroup.Properties.Appearance.Font"), System.Drawing.Font)
        Me.MedicenGroup.Properties.Appearance.Options.UseFont = True
        '
        'LabelControl1
        '
        resources.ApplyResources(Me.LabelControl1, "LabelControl1")
        Me.LabelControl1.Appearance.Font = CType(resources.GetObject("LabelControl1.Appearance.Font"), System.Drawing.Font)
        Me.LabelControl1.Appearance.Options.UseFont = True
        Me.LabelControl1.Name = "LabelControl1"
        '
        'LabelControl15
        '
        resources.ApplyResources(Me.LabelControl15, "LabelControl15")
        Me.LabelControl15.Appearance.Font = CType(resources.GetObject("LabelControl15.Appearance.Font"), System.Drawing.Font)
        Me.LabelControl15.Appearance.ForeColor = System.Drawing.Color.Green
        Me.LabelControl15.Appearance.Options.UseFont = True
        Me.LabelControl15.Appearance.Options.UseForeColor = True
        Me.LabelControl15.Appearance.Options.UseTextOptions = True
        Me.LabelControl15.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.LabelControl15.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.LabelControl15.Name = "LabelControl15"
        '
        'GrpNavigator
        '
        resources.ApplyResources(Me.GrpNavigator, "GrpNavigator")
        Me.GrpNavigator.AddNewItem = Nothing
        Me.GrpNavigator.BindingSource = Me.MedicineGroupsBindingSource
        Me.GrpNavigator.CountItem = Me.BindingNavigatorCountItem
        Me.GrpNavigator.DeleteItem = Me.BindingNavigatorDeleteItem
        Me.GrpNavigator.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.BindingNavigatorMoveFirstItem, Me.BindingNavigatorMovePreviousItem, Me.BindingNavigatorSeparator, Me.BindingNavigatorPositionItem, Me.BindingNavigatorCountItem, Me.BindingNavigatorSeparator1, Me.BindingNavigatorMoveNextItem, Me.BindingNavigatorMoveLastItem, Me.BindingNavigatorSeparator2, Me.BindingNavigatorDeleteItem, Me.PatientBindingNavigatorSaveItem})
        Me.GrpNavigator.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow
        Me.GrpNavigator.MoveFirstItem = Me.BindingNavigatorMoveFirstItem
        Me.GrpNavigator.MoveLastItem = Me.BindingNavigatorMoveLastItem
        Me.GrpNavigator.MoveNextItem = Me.BindingNavigatorMoveNextItem
        Me.GrpNavigator.MovePreviousItem = Me.BindingNavigatorMovePreviousItem
        Me.GrpNavigator.Name = "GrpNavigator"
        Me.GrpNavigator.PositionItem = Me.BindingNavigatorPositionItem
        '
        'BindingNavigatorCountItem
        '
        resources.ApplyResources(Me.BindingNavigatorCountItem, "BindingNavigatorCountItem")
        Me.BindingNavigatorCountItem.Name = "BindingNavigatorCountItem"
        '
        'BindingNavigatorDeleteItem
        '
        resources.ApplyResources(Me.BindingNavigatorDeleteItem, "BindingNavigatorDeleteItem")
        Me.BindingNavigatorDeleteItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.BindingNavigatorDeleteItem.Name = "BindingNavigatorDeleteItem"
        '
        'BindingNavigatorMoveFirstItem
        '
        resources.ApplyResources(Me.BindingNavigatorMoveFirstItem, "BindingNavigatorMoveFirstItem")
        Me.BindingNavigatorMoveFirstItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.BindingNavigatorMoveFirstItem.Name = "BindingNavigatorMoveFirstItem"
        '
        'BindingNavigatorMovePreviousItem
        '
        resources.ApplyResources(Me.BindingNavigatorMovePreviousItem, "BindingNavigatorMovePreviousItem")
        Me.BindingNavigatorMovePreviousItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.BindingNavigatorMovePreviousItem.Name = "BindingNavigatorMovePreviousItem"
        '
        'BindingNavigatorSeparator
        '
        resources.ApplyResources(Me.BindingNavigatorSeparator, "BindingNavigatorSeparator")
        Me.BindingNavigatorSeparator.Name = "BindingNavigatorSeparator"
        '
        'BindingNavigatorPositionItem
        '
        resources.ApplyResources(Me.BindingNavigatorPositionItem, "BindingNavigatorPositionItem")
        Me.BindingNavigatorPositionItem.Name = "BindingNavigatorPositionItem"
        '
        'BindingNavigatorSeparator1
        '
        resources.ApplyResources(Me.BindingNavigatorSeparator1, "BindingNavigatorSeparator1")
        Me.BindingNavigatorSeparator1.Name = "BindingNavigatorSeparator1"
        '
        'BindingNavigatorMoveNextItem
        '
        resources.ApplyResources(Me.BindingNavigatorMoveNextItem, "BindingNavigatorMoveNextItem")
        Me.BindingNavigatorMoveNextItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.BindingNavigatorMoveNextItem.Name = "BindingNavigatorMoveNextItem"
        '
        'BindingNavigatorMoveLastItem
        '
        resources.ApplyResources(Me.BindingNavigatorMoveLastItem, "BindingNavigatorMoveLastItem")
        Me.BindingNavigatorMoveLastItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.BindingNavigatorMoveLastItem.Name = "BindingNavigatorMoveLastItem"
        '
        'BindingNavigatorSeparator2
        '
        resources.ApplyResources(Me.BindingNavigatorSeparator2, "BindingNavigatorSeparator2")
        Me.BindingNavigatorSeparator2.Name = "BindingNavigatorSeparator2"
        '
        'PatientBindingNavigatorSaveItem
        '
        resources.ApplyResources(Me.PatientBindingNavigatorSaveItem, "PatientBindingNavigatorSaveItem")
        Me.PatientBindingNavigatorSaveItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.PatientBindingNavigatorSaveItem.Name = "PatientBindingNavigatorSaveItem"
        '
        'PanelControl2
        '
        resources.ApplyResources(Me.PanelControl2, "PanelControl2")
        Me.PanelControl2.Controls.Add(Me.GroupControl6)
        Me.PanelControl2.Controls.Add(Me.GroupControl5)
        Me.PanelControl2.Name = "PanelControl2"
        '
        'GroupControl6
        '
        resources.ApplyResources(Me.GroupControl6, "GroupControl6")
        Me.GroupControl6.AppearanceCaption.Font = CType(resources.GetObject("GroupControl6.AppearanceCaption.Font"), System.Drawing.Font)
        Me.GroupControl6.AppearanceCaption.Options.UseFont = True
        Me.GroupControl6.Controls.Add(Me.LabelControl19)
        Me.GroupControl6.Controls.Add(Me.shapeCombo)
        Me.GroupControl6.Controls.Add(Me.gridMedDose)
        Me.GroupControl6.Controls.Add(Me.btSaveDoze)
        Me.GroupControl6.Controls.Add(Me.btEditDoze)
        Me.GroupControl6.Controls.Add(Me.LabelControl13)
        Me.GroupControl6.Controls.Add(Me.Doze)
        Me.GroupControl6.Controls.Add(Me.btAddDoze)
        Me.GroupControl6.Controls.Add(Me.LabelControl14)
        Me.GroupControl6.Controls.Add(Me.DozeNavigator)
        Me.GroupControl6.Name = "GroupControl6"
        '
        'LabelControl19
        '
        resources.ApplyResources(Me.LabelControl19, "LabelControl19")
        Me.LabelControl19.Appearance.Font = CType(resources.GetObject("LabelControl19.Appearance.Font"), System.Drawing.Font)
        Me.LabelControl19.Appearance.ForeColor = System.Drawing.Color.Green
        Me.LabelControl19.Appearance.Options.UseFont = True
        Me.LabelControl19.Appearance.Options.UseForeColor = True
        Me.LabelControl19.Appearance.Options.UseTextOptions = True
        Me.LabelControl19.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.LabelControl19.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.LabelControl19.Name = "LabelControl19"
        '
        'shapeCombo
        '
        resources.ApplyResources(Me.shapeCombo, "shapeCombo")
        Me.shapeCombo.DataSource = Me.MedicineShapeBindingSource
        Me.shapeCombo.DisplayMember = "MedicineShape"
        Me.shapeCombo.FormattingEnabled = True
        Me.shapeCombo.Name = "shapeCombo"
        Me.shapeCombo.ValueMember = "ShapeID"
        '
        'MedicineShapeBindingSource
        '
        Me.MedicineShapeBindingSource.DataMember = "FK_MedicineShape_MedicineItems"
        Me.MedicineShapeBindingSource.DataSource = Me.MedicineItemsBindingSource
        '
        'MedicineItemsBindingSource
        '
        Me.MedicineItemsBindingSource.DataMember = "FK_MedicineItems_ScienceFamily"
        Me.MedicineItemsBindingSource.DataSource = Me.ScienceFamilyBindingSource
        '
        'ScienceFamilyBindingSource
        '
        Me.ScienceFamilyBindingSource.DataMember = "FK_ScienceFamily_MedicineFamily"
        Me.ScienceFamilyBindingSource.DataSource = Me.MedicineFamilyBindingSource
        '
        'gridMedDose
        '
        resources.ApplyResources(Me.gridMedDose, "gridMedDose")
        Me.gridMedDose.DataSource = Me.MedicineDozeBindingSource
        Me.gridMedDose.EmbeddedNavigator.AccessibleDescription = resources.GetString("gridMedDose.EmbeddedNavigator.AccessibleDescription")
        Me.gridMedDose.EmbeddedNavigator.AccessibleName = resources.GetString("gridMedDose.EmbeddedNavigator.AccessibleName")
        Me.gridMedDose.EmbeddedNavigator.AllowHtmlTextInToolTip = CType(resources.GetObject("gridMedDose.EmbeddedNavigator.AllowHtmlTextInToolTip"), DevExpress.Utils.DefaultBoolean)
        Me.gridMedDose.EmbeddedNavigator.Anchor = CType(resources.GetObject("gridMedDose.EmbeddedNavigator.Anchor"), System.Windows.Forms.AnchorStyles)
        Me.gridMedDose.EmbeddedNavigator.AutoSize = CType(resources.GetObject("gridMedDose.EmbeddedNavigator.AutoSize"), Boolean)
        Me.gridMedDose.EmbeddedNavigator.BackgroundImage = CType(resources.GetObject("gridMedDose.EmbeddedNavigator.BackgroundImage"), System.Drawing.Image)
        Me.gridMedDose.EmbeddedNavigator.BackgroundImageLayout = CType(resources.GetObject("gridMedDose.EmbeddedNavigator.BackgroundImageLayout"), System.Windows.Forms.ImageLayout)
        Me.gridMedDose.EmbeddedNavigator.ImeMode = CType(resources.GetObject("gridMedDose.EmbeddedNavigator.ImeMode"), System.Windows.Forms.ImeMode)
        Me.gridMedDose.EmbeddedNavigator.MaximumSize = CType(resources.GetObject("gridMedDose.EmbeddedNavigator.MaximumSize"), System.Drawing.Size)
        Me.gridMedDose.EmbeddedNavigator.TextLocation = CType(resources.GetObject("gridMedDose.EmbeddedNavigator.TextLocation"), DevExpress.XtraEditors.NavigatorButtonsTextLocation)
        Me.gridMedDose.EmbeddedNavigator.ToolTip = resources.GetString("gridMedDose.EmbeddedNavigator.ToolTip")
        Me.gridMedDose.EmbeddedNavigator.ToolTipIconType = CType(resources.GetObject("gridMedDose.EmbeddedNavigator.ToolTipIconType"), DevExpress.Utils.ToolTipIconType)
        Me.gridMedDose.EmbeddedNavigator.ToolTipTitle = resources.GetString("gridMedDose.EmbeddedNavigator.ToolTipTitle")
        Me.gridMedDose.MainView = Me.GridView6
        Me.gridMedDose.Name = "gridMedDose"
        Me.gridMedDose.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridView6})
        '
        'MedicineDozeBindingSource
        '
        Me.MedicineDozeBindingSource.DataMember = "FK_MedicineDoze_MedicineShape"
        Me.MedicineDozeBindingSource.DataSource = Me.MedicineShapeBindingSource
        '
        'GridView6
        '
        resources.ApplyResources(Me.GridView6, "GridView6")
        Me.GridView6.Appearance.HeaderPanel.Font = CType(resources.GetObject("GridView6.Appearance.HeaderPanel.Font"), System.Drawing.Font)
        Me.GridView6.Appearance.HeaderPanel.Options.UseFont = True
        Me.GridView6.Appearance.Row.Font = CType(resources.GetObject("GridView6.Appearance.Row.Font"), System.Drawing.Font)
        Me.GridView6.Appearance.Row.Options.UseFont = True
        Me.GridView6.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.colRowNum6, Me.colDozeID, Me.colShapeID1, Me.colDoze})
        Me.GridView6.GridControl = Me.gridMedDose
        Me.GridView6.Name = "GridView6"
        Me.GridView6.OptionsDetail.EnableMasterViewMode = False
        Me.GridView6.OptionsView.EnableAppearanceEvenRow = True
        Me.GridView6.OptionsView.EnableAppearanceOddRow = True
        Me.GridView6.OptionsView.ShowDetailButtons = False
        '
        'colRowNum6
        '
        resources.ApplyResources(Me.colRowNum6, "colRowNum6")
        Me.colRowNum6.FieldName = "colRowNum6"
        Me.colRowNum6.ImageOptions.ImageKey = resources.GetString("colRowNum6.ImageOptions.ImageKey")
        Me.colRowNum6.Name = "colRowNum6"
        Me.colRowNum6.UnboundDataType = GetType(Integer)
        '
        'colDozeID
        '
        resources.ApplyResources(Me.colDozeID, "colDozeID")
        Me.colDozeID.FieldName = "DozeID"
        Me.colDozeID.ImageOptions.ImageKey = resources.GetString("colDozeID.ImageOptions.ImageKey")
        Me.colDozeID.Name = "colDozeID"
        '
        'colShapeID1
        '
        resources.ApplyResources(Me.colShapeID1, "colShapeID1")
        Me.colShapeID1.FieldName = "ShapeID"
        Me.colShapeID1.ImageOptions.ImageKey = resources.GetString("colShapeID1.ImageOptions.ImageKey")
        Me.colShapeID1.Name = "colShapeID1"
        '
        'colDoze
        '
        resources.ApplyResources(Me.colDoze, "colDoze")
        Me.colDoze.FieldName = "Doze"
        Me.colDoze.ImageOptions.ImageKey = resources.GetString("colDoze.ImageOptions.ImageKey")
        Me.colDoze.Name = "colDoze"
        '
        'btSaveDoze
        '
        resources.ApplyResources(Me.btSaveDoze, "btSaveDoze")
        Me.btSaveDoze.Appearance.Font = CType(resources.GetObject("btSaveDoze.Appearance.Font"), System.Drawing.Font)
        Me.btSaveDoze.Appearance.Options.UseFont = True
        Me.btSaveDoze.ImageOptions.ImageKey = resources.GetString("btSaveDoze.ImageOptions.ImageKey")
        Me.btSaveDoze.Name = "btSaveDoze"
        '
        'btEditDoze
        '
        resources.ApplyResources(Me.btEditDoze, "btEditDoze")
        Me.btEditDoze.Appearance.Font = CType(resources.GetObject("btEditDoze.Appearance.Font"), System.Drawing.Font)
        Me.btEditDoze.Appearance.Options.UseFont = True
        Me.btEditDoze.ImageOptions.ImageKey = resources.GetString("btEditDoze.ImageOptions.ImageKey")
        Me.btEditDoze.Name = "btEditDoze"
        '
        'LabelControl13
        '
        resources.ApplyResources(Me.LabelControl13, "LabelControl13")
        Me.LabelControl13.Appearance.Font = CType(resources.GetObject("LabelControl13.Appearance.Font"), System.Drawing.Font)
        Me.LabelControl13.Appearance.Options.UseFont = True
        Me.LabelControl13.Name = "LabelControl13"
        '
        'Doze
        '
        resources.ApplyResources(Me.Doze, "Doze")
        Me.Doze.Name = "Doze"
        Me.Doze.Properties.Appearance.Font = CType(resources.GetObject("Doze.Properties.Appearance.Font"), System.Drawing.Font)
        Me.Doze.Properties.Appearance.Options.UseFont = True
        '
        'btAddDoze
        '
        resources.ApplyResources(Me.btAddDoze, "btAddDoze")
        Me.btAddDoze.Appearance.Font = CType(resources.GetObject("btAddDoze.Appearance.Font"), System.Drawing.Font)
        Me.btAddDoze.Appearance.Options.UseFont = True
        Me.btAddDoze.ImageOptions.ImageKey = resources.GetString("btAddDoze.ImageOptions.ImageKey")
        Me.btAddDoze.Name = "btAddDoze"
        '
        'LabelControl14
        '
        resources.ApplyResources(Me.LabelControl14, "LabelControl14")
        Me.LabelControl14.Appearance.Font = CType(resources.GetObject("LabelControl14.Appearance.Font"), System.Drawing.Font)
        Me.LabelControl14.Appearance.Options.UseFont = True
        Me.LabelControl14.Name = "LabelControl14"
        '
        'DozeNavigator
        '
        resources.ApplyResources(Me.DozeNavigator, "DozeNavigator")
        Me.DozeNavigator.AddNewItem = Nothing
        Me.DozeNavigator.BindingSource = Me.MedicineDozeBindingSource
        Me.DozeNavigator.CountItem = Me.ToolStripLabel3
        Me.DozeNavigator.DeleteItem = Me.ToolStripButton13
        Me.DozeNavigator.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripButton14, Me.ToolStripButton15, Me.ToolStripSeparator7, Me.ToolStripTextBox3, Me.ToolStripLabel3, Me.ToolStripSeparator8, Me.ToolStripButton16, Me.ToolStripButton17, Me.ToolStripSeparator9, Me.ToolStripButton13, Me.ToolStripButton18})
        Me.DozeNavigator.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow
        Me.DozeNavigator.MoveFirstItem = Me.ToolStripButton14
        Me.DozeNavigator.MoveLastItem = Me.ToolStripButton17
        Me.DozeNavigator.MoveNextItem = Me.ToolStripButton16
        Me.DozeNavigator.MovePreviousItem = Me.ToolStripButton15
        Me.DozeNavigator.Name = "DozeNavigator"
        Me.DozeNavigator.PositionItem = Me.ToolStripTextBox3
        '
        'ToolStripLabel3
        '
        resources.ApplyResources(Me.ToolStripLabel3, "ToolStripLabel3")
        Me.ToolStripLabel3.Name = "ToolStripLabel3"
        '
        'ToolStripButton13
        '
        resources.ApplyResources(Me.ToolStripButton13, "ToolStripButton13")
        Me.ToolStripButton13.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton13.Name = "ToolStripButton13"
        '
        'ToolStripButton14
        '
        resources.ApplyResources(Me.ToolStripButton14, "ToolStripButton14")
        Me.ToolStripButton14.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton14.Name = "ToolStripButton14"
        '
        'ToolStripButton15
        '
        resources.ApplyResources(Me.ToolStripButton15, "ToolStripButton15")
        Me.ToolStripButton15.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton15.Name = "ToolStripButton15"
        '
        'ToolStripSeparator7
        '
        resources.ApplyResources(Me.ToolStripSeparator7, "ToolStripSeparator7")
        Me.ToolStripSeparator7.Name = "ToolStripSeparator7"
        '
        'ToolStripTextBox3
        '
        resources.ApplyResources(Me.ToolStripTextBox3, "ToolStripTextBox3")
        Me.ToolStripTextBox3.Name = "ToolStripTextBox3"
        '
        'ToolStripSeparator8
        '
        resources.ApplyResources(Me.ToolStripSeparator8, "ToolStripSeparator8")
        Me.ToolStripSeparator8.Name = "ToolStripSeparator8"
        '
        'ToolStripButton16
        '
        resources.ApplyResources(Me.ToolStripButton16, "ToolStripButton16")
        Me.ToolStripButton16.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton16.Name = "ToolStripButton16"
        '
        'ToolStripButton17
        '
        resources.ApplyResources(Me.ToolStripButton17, "ToolStripButton17")
        Me.ToolStripButton17.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton17.Name = "ToolStripButton17"
        '
        'ToolStripSeparator9
        '
        resources.ApplyResources(Me.ToolStripSeparator9, "ToolStripSeparator9")
        Me.ToolStripSeparator9.Name = "ToolStripSeparator9"
        '
        'ToolStripButton18
        '
        resources.ApplyResources(Me.ToolStripButton18, "ToolStripButton18")
        Me.ToolStripButton18.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton18.Name = "ToolStripButton18"
        '
        'GroupControl5
        '
        resources.ApplyResources(Me.GroupControl5, "GroupControl5")
        Me.GroupControl5.AppearanceCaption.Font = CType(resources.GetObject("GroupControl5.AppearanceCaption.Font"), System.Drawing.Font)
        Me.GroupControl5.AppearanceCaption.Options.UseFont = True
        Me.GroupControl5.Controls.Add(Me.itemCombo)
        Me.GroupControl5.Controls.Add(Me.gridMedShape)
        Me.GroupControl5.Controls.Add(Me.btSaveShape)
        Me.GroupControl5.Controls.Add(Me.btAddShape)
        Me.GroupControl5.Controls.Add(Me.LabelControl10)
        Me.GroupControl5.Controls.Add(Me.btEditShape)
        Me.GroupControl5.Controls.Add(Me.ShapeInfotxt)
        Me.GroupControl5.Controls.Add(Me.LabelControl12)
        Me.GroupControl5.Controls.Add(Me.Shape)
        Me.GroupControl5.Controls.Add(Me.LabelControl11)
        Me.GroupControl5.Controls.Add(Me.LabelControl20)
        Me.GroupControl5.Controls.Add(Me.ShapeNavigator)
        Me.GroupControl5.Name = "GroupControl5"
        '
        'itemCombo
        '
        resources.ApplyResources(Me.itemCombo, "itemCombo")
        Me.itemCombo.DataSource = Me.MedicineItemsBindingSource
        Me.itemCombo.DisplayMember = "CommName"
        Me.itemCombo.FormattingEnabled = True
        Me.itemCombo.Name = "itemCombo"
        Me.itemCombo.ValueMember = "MedicineItemID"
        '
        'gridMedShape
        '
        resources.ApplyResources(Me.gridMedShape, "gridMedShape")
        Me.gridMedShape.DataSource = Me.MedicineShapeBindingSource
        Me.gridMedShape.EmbeddedNavigator.AccessibleDescription = resources.GetString("gridMedShape.EmbeddedNavigator.AccessibleDescription")
        Me.gridMedShape.EmbeddedNavigator.AccessibleName = resources.GetString("gridMedShape.EmbeddedNavigator.AccessibleName")
        Me.gridMedShape.EmbeddedNavigator.AllowHtmlTextInToolTip = CType(resources.GetObject("gridMedShape.EmbeddedNavigator.AllowHtmlTextInToolTip"), DevExpress.Utils.DefaultBoolean)
        Me.gridMedShape.EmbeddedNavigator.Anchor = CType(resources.GetObject("gridMedShape.EmbeddedNavigator.Anchor"), System.Windows.Forms.AnchorStyles)
        Me.gridMedShape.EmbeddedNavigator.AutoSize = CType(resources.GetObject("gridMedShape.EmbeddedNavigator.AutoSize"), Boolean)
        Me.gridMedShape.EmbeddedNavigator.BackgroundImage = CType(resources.GetObject("gridMedShape.EmbeddedNavigator.BackgroundImage"), System.Drawing.Image)
        Me.gridMedShape.EmbeddedNavigator.BackgroundImageLayout = CType(resources.GetObject("gridMedShape.EmbeddedNavigator.BackgroundImageLayout"), System.Windows.Forms.ImageLayout)
        Me.gridMedShape.EmbeddedNavigator.ImeMode = CType(resources.GetObject("gridMedShape.EmbeddedNavigator.ImeMode"), System.Windows.Forms.ImeMode)
        Me.gridMedShape.EmbeddedNavigator.MaximumSize = CType(resources.GetObject("gridMedShape.EmbeddedNavigator.MaximumSize"), System.Drawing.Size)
        Me.gridMedShape.EmbeddedNavigator.TextLocation = CType(resources.GetObject("gridMedShape.EmbeddedNavigator.TextLocation"), DevExpress.XtraEditors.NavigatorButtonsTextLocation)
        Me.gridMedShape.EmbeddedNavigator.ToolTip = resources.GetString("gridMedShape.EmbeddedNavigator.ToolTip")
        Me.gridMedShape.EmbeddedNavigator.ToolTipIconType = CType(resources.GetObject("gridMedShape.EmbeddedNavigator.ToolTipIconType"), DevExpress.Utils.ToolTipIconType)
        Me.gridMedShape.EmbeddedNavigator.ToolTipTitle = resources.GetString("gridMedShape.EmbeddedNavigator.ToolTipTitle")
        Me.gridMedShape.MainView = Me.GridView5
        Me.gridMedShape.Name = "gridMedShape"
        Me.gridMedShape.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridView5})
        '
        'GridView5
        '
        resources.ApplyResources(Me.GridView5, "GridView5")
        Me.GridView5.Appearance.HeaderPanel.Font = CType(resources.GetObject("GridView5.Appearance.HeaderPanel.Font"), System.Drawing.Font)
        Me.GridView5.Appearance.HeaderPanel.Options.UseFont = True
        Me.GridView5.Appearance.Row.Font = CType(resources.GetObject("GridView5.Appearance.Row.Font"), System.Drawing.Font)
        Me.GridView5.Appearance.Row.Options.UseFont = True
        Me.GridView5.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.colRowNum5, Me.colShapeID, Me.colMedicineItemID1, Me.colMedicineShape, Me.colShapeInfo})
        Me.GridView5.GridControl = Me.gridMedShape
        Me.GridView5.Name = "GridView5"
        Me.GridView5.OptionsDetail.EnableMasterViewMode = False
        Me.GridView5.OptionsView.EnableAppearanceEvenRow = True
        Me.GridView5.OptionsView.EnableAppearanceOddRow = True
        Me.GridView5.OptionsView.ShowDetailButtons = False
        '
        'colRowNum5
        '
        resources.ApplyResources(Me.colRowNum5, "colRowNum5")
        Me.colRowNum5.FieldName = "colRowNum5"
        Me.colRowNum5.ImageOptions.ImageKey = resources.GetString("colRowNum5.ImageOptions.ImageKey")
        Me.colRowNum5.Name = "colRowNum5"
        Me.colRowNum5.UnboundDataType = GetType(Integer)
        '
        'colShapeID
        '
        resources.ApplyResources(Me.colShapeID, "colShapeID")
        Me.colShapeID.FieldName = "ShapeID"
        Me.colShapeID.ImageOptions.ImageKey = resources.GetString("colShapeID.ImageOptions.ImageKey")
        Me.colShapeID.Name = "colShapeID"
        '
        'colMedicineItemID1
        '
        resources.ApplyResources(Me.colMedicineItemID1, "colMedicineItemID1")
        Me.colMedicineItemID1.FieldName = "MedicineItemID"
        Me.colMedicineItemID1.ImageOptions.ImageKey = resources.GetString("colMedicineItemID1.ImageOptions.ImageKey")
        Me.colMedicineItemID1.Name = "colMedicineItemID1"
        '
        'colMedicineShape
        '
        resources.ApplyResources(Me.colMedicineShape, "colMedicineShape")
        Me.colMedicineShape.FieldName = "MedicineShape"
        Me.colMedicineShape.ImageOptions.ImageKey = resources.GetString("colMedicineShape.ImageOptions.ImageKey")
        Me.colMedicineShape.Name = "colMedicineShape"
        '
        'colShapeInfo
        '
        resources.ApplyResources(Me.colShapeInfo, "colShapeInfo")
        Me.colShapeInfo.FieldName = "ShapeInfo"
        Me.colShapeInfo.ImageOptions.ImageKey = resources.GetString("colShapeInfo.ImageOptions.ImageKey")
        Me.colShapeInfo.Name = "colShapeInfo"
        '
        'btSaveShape
        '
        resources.ApplyResources(Me.btSaveShape, "btSaveShape")
        Me.btSaveShape.Appearance.Font = CType(resources.GetObject("btSaveShape.Appearance.Font"), System.Drawing.Font)
        Me.btSaveShape.Appearance.Options.UseFont = True
        Me.btSaveShape.ImageOptions.ImageKey = resources.GetString("btSaveShape.ImageOptions.ImageKey")
        Me.btSaveShape.Name = "btSaveShape"
        '
        'btAddShape
        '
        resources.ApplyResources(Me.btAddShape, "btAddShape")
        Me.btAddShape.Appearance.Font = CType(resources.GetObject("btAddShape.Appearance.Font"), System.Drawing.Font)
        Me.btAddShape.Appearance.Options.UseFont = True
        Me.btAddShape.ImageOptions.ImageKey = resources.GetString("btAddShape.ImageOptions.ImageKey")
        Me.btAddShape.Name = "btAddShape"
        '
        'LabelControl10
        '
        resources.ApplyResources(Me.LabelControl10, "LabelControl10")
        Me.LabelControl10.Appearance.Font = CType(resources.GetObject("LabelControl10.Appearance.Font"), System.Drawing.Font)
        Me.LabelControl10.Appearance.Options.UseFont = True
        Me.LabelControl10.Name = "LabelControl10"
        '
        'btEditShape
        '
        resources.ApplyResources(Me.btEditShape, "btEditShape")
        Me.btEditShape.Appearance.Font = CType(resources.GetObject("btEditShape.Appearance.Font"), System.Drawing.Font)
        Me.btEditShape.Appearance.Options.UseFont = True
        Me.btEditShape.ImageOptions.ImageKey = resources.GetString("btEditShape.ImageOptions.ImageKey")
        Me.btEditShape.Name = "btEditShape"
        '
        'ShapeInfotxt
        '
        resources.ApplyResources(Me.ShapeInfotxt, "ShapeInfotxt")
        Me.ShapeInfotxt.Name = "ShapeInfotxt"
        Me.ShapeInfotxt.Properties.Appearance.Font = CType(resources.GetObject("ShapeInfotxt.Properties.Appearance.Font"), System.Drawing.Font)
        Me.ShapeInfotxt.Properties.Appearance.Options.UseFont = True
        '
        'LabelControl12
        '
        resources.ApplyResources(Me.LabelControl12, "LabelControl12")
        Me.LabelControl12.Appearance.Font = CType(resources.GetObject("LabelControl12.Appearance.Font"), System.Drawing.Font)
        Me.LabelControl12.Appearance.Options.UseFont = True
        Me.LabelControl12.Name = "LabelControl12"
        '
        'Shape
        '
        resources.ApplyResources(Me.Shape, "Shape")
        Me.Shape.Name = "Shape"
        Me.Shape.Properties.Appearance.Font = CType(resources.GetObject("Shape.Properties.Appearance.Font"), System.Drawing.Font)
        Me.Shape.Properties.Appearance.Options.UseFont = True
        '
        'LabelControl11
        '
        resources.ApplyResources(Me.LabelControl11, "LabelControl11")
        Me.LabelControl11.Appearance.Font = CType(resources.GetObject("LabelControl11.Appearance.Font"), System.Drawing.Font)
        Me.LabelControl11.Appearance.Options.UseFont = True
        Me.LabelControl11.Name = "LabelControl11"
        '
        'LabelControl20
        '
        resources.ApplyResources(Me.LabelControl20, "LabelControl20")
        Me.LabelControl20.Appearance.Font = CType(resources.GetObject("LabelControl20.Appearance.Font"), System.Drawing.Font)
        Me.LabelControl20.Appearance.ForeColor = System.Drawing.Color.Green
        Me.LabelControl20.Appearance.Options.UseFont = True
        Me.LabelControl20.Appearance.Options.UseForeColor = True
        Me.LabelControl20.Appearance.Options.UseTextOptions = True
        Me.LabelControl20.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.LabelControl20.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.LabelControl20.Name = "LabelControl20"
        '
        'ShapeNavigator
        '
        resources.ApplyResources(Me.ShapeNavigator, "ShapeNavigator")
        Me.ShapeNavigator.AddNewItem = Nothing
        Me.ShapeNavigator.BindingSource = Me.MedicineShapeBindingSource
        Me.ShapeNavigator.CountItem = Me.ToolStripLabel2
        Me.ShapeNavigator.DeleteItem = Me.ToolStripButton7
        Me.ShapeNavigator.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripButton8, Me.ToolStripButton9, Me.ToolStripSeparator4, Me.ToolStripTextBox2, Me.ToolStripLabel2, Me.ToolStripSeparator5, Me.ToolStripButton10, Me.ToolStripButton11, Me.ToolStripSeparator6, Me.ToolStripButton7, Me.ToolStripButton12})
        Me.ShapeNavigator.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow
        Me.ShapeNavigator.MoveFirstItem = Me.ToolStripButton8
        Me.ShapeNavigator.MoveLastItem = Me.ToolStripButton11
        Me.ShapeNavigator.MoveNextItem = Me.ToolStripButton10
        Me.ShapeNavigator.MovePreviousItem = Me.ToolStripButton9
        Me.ShapeNavigator.Name = "ShapeNavigator"
        Me.ShapeNavigator.PositionItem = Me.ToolStripTextBox2
        '
        'ToolStripLabel2
        '
        resources.ApplyResources(Me.ToolStripLabel2, "ToolStripLabel2")
        Me.ToolStripLabel2.Name = "ToolStripLabel2"
        '
        'ToolStripButton7
        '
        resources.ApplyResources(Me.ToolStripButton7, "ToolStripButton7")
        Me.ToolStripButton7.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton7.Name = "ToolStripButton7"
        '
        'ToolStripButton8
        '
        resources.ApplyResources(Me.ToolStripButton8, "ToolStripButton8")
        Me.ToolStripButton8.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton8.Name = "ToolStripButton8"
        '
        'ToolStripButton9
        '
        resources.ApplyResources(Me.ToolStripButton9, "ToolStripButton9")
        Me.ToolStripButton9.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton9.Name = "ToolStripButton9"
        '
        'ToolStripSeparator4
        '
        resources.ApplyResources(Me.ToolStripSeparator4, "ToolStripSeparator4")
        Me.ToolStripSeparator4.Name = "ToolStripSeparator4"
        '
        'ToolStripTextBox2
        '
        resources.ApplyResources(Me.ToolStripTextBox2, "ToolStripTextBox2")
        Me.ToolStripTextBox2.Name = "ToolStripTextBox2"
        '
        'ToolStripSeparator5
        '
        resources.ApplyResources(Me.ToolStripSeparator5, "ToolStripSeparator5")
        Me.ToolStripSeparator5.Name = "ToolStripSeparator5"
        '
        'ToolStripButton10
        '
        resources.ApplyResources(Me.ToolStripButton10, "ToolStripButton10")
        Me.ToolStripButton10.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton10.Name = "ToolStripButton10"
        '
        'ToolStripButton11
        '
        resources.ApplyResources(Me.ToolStripButton11, "ToolStripButton11")
        Me.ToolStripButton11.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton11.Name = "ToolStripButton11"
        '
        'ToolStripSeparator6
        '
        resources.ApplyResources(Me.ToolStripSeparator6, "ToolStripSeparator6")
        Me.ToolStripSeparator6.Name = "ToolStripSeparator6"
        '
        'ToolStripButton12
        '
        resources.ApplyResources(Me.ToolStripButton12, "ToolStripButton12")
        Me.ToolStripButton12.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton12.Name = "ToolStripButton12"
        '
        'SidePanel1
        '
        resources.ApplyResources(Me.SidePanel1, "SidePanel1")
        Me.SidePanel1.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(239, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.SidePanel1.Appearance.Options.UseBackColor = True
        Me.SidePanel1.BorderThickness = 2
        Me.SidePanel1.Controls.Add(Me.GroupControl4)
        Me.SidePanel1.Controls.Add(Me.GroupControl3)
        Me.SidePanel1.Name = "SidePanel1"
        '
        'GroupControl4
        '
        resources.ApplyResources(Me.GroupControl4, "GroupControl4")
        Me.GroupControl4.AppearanceCaption.Font = CType(resources.GetObject("GroupControl4.AppearanceCaption.Font"), System.Drawing.Font)
        Me.GroupControl4.AppearanceCaption.Options.UseFont = True
        Me.GroupControl4.Controls.Add(Me.LabelControl18)
        Me.GroupControl4.Controls.Add(Me.ScientCombo)
        Me.GroupControl4.Controls.Add(Me.gridMedItems)
        Me.GroupControl4.Controls.Add(Me.btSaveMedic)
        Me.GroupControl4.Controls.Add(Me.CompanyTxt)
        Me.GroupControl4.Controls.Add(Me.LabelControl6)
        Me.GroupControl4.Controls.Add(Me.Commercial)
        Me.GroupControl4.Controls.Add(Me.btAddMedicine)
        Me.GroupControl4.Controls.Add(Me.LabelControl9)
        Me.GroupControl4.Controls.Add(Me.btEditMedic)
        Me.GroupControl4.Controls.Add(Me.LabelControl8)
        Me.GroupControl4.Controls.Add(Me.LabelControl7)
        Me.GroupControl4.Controls.Add(Me.MedicNote)
        Me.GroupControl4.Controls.Add(Me.ItemsNavigator)
        Me.GroupControl4.Name = "GroupControl4"
        '
        'LabelControl18
        '
        resources.ApplyResources(Me.LabelControl18, "LabelControl18")
        Me.LabelControl18.Appearance.Font = CType(resources.GetObject("LabelControl18.Appearance.Font"), System.Drawing.Font)
        Me.LabelControl18.Appearance.ForeColor = System.Drawing.Color.Green
        Me.LabelControl18.Appearance.Options.UseFont = True
        Me.LabelControl18.Appearance.Options.UseForeColor = True
        Me.LabelControl18.Appearance.Options.UseTextOptions = True
        Me.LabelControl18.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.LabelControl18.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.LabelControl18.Name = "LabelControl18"
        '
        'ScientCombo
        '
        resources.ApplyResources(Me.ScientCombo, "ScientCombo")
        Me.ScientCombo.DataSource = Me.ScienceFamilyBindingSource
        Me.ScientCombo.DisplayMember = "ScienceName"
        Me.ScientCombo.FormattingEnabled = True
        Me.ScientCombo.Name = "ScientCombo"
        Me.ScientCombo.ValueMember = "ScincID"
        '
        'gridMedItems
        '
        resources.ApplyResources(Me.gridMedItems, "gridMedItems")
        Me.gridMedItems.DataSource = Me.MedicineItemsBindingSource
        Me.gridMedItems.EmbeddedNavigator.AccessibleDescription = resources.GetString("gridMedItems.EmbeddedNavigator.AccessibleDescription")
        Me.gridMedItems.EmbeddedNavigator.AccessibleName = resources.GetString("gridMedItems.EmbeddedNavigator.AccessibleName")
        Me.gridMedItems.EmbeddedNavigator.AllowHtmlTextInToolTip = CType(resources.GetObject("gridMedItems.EmbeddedNavigator.AllowHtmlTextInToolTip"), DevExpress.Utils.DefaultBoolean)
        Me.gridMedItems.EmbeddedNavigator.Anchor = CType(resources.GetObject("gridMedItems.EmbeddedNavigator.Anchor"), System.Windows.Forms.AnchorStyles)
        Me.gridMedItems.EmbeddedNavigator.AutoSize = CType(resources.GetObject("gridMedItems.EmbeddedNavigator.AutoSize"), Boolean)
        Me.gridMedItems.EmbeddedNavigator.BackgroundImage = CType(resources.GetObject("gridMedItems.EmbeddedNavigator.BackgroundImage"), System.Drawing.Image)
        Me.gridMedItems.EmbeddedNavigator.BackgroundImageLayout = CType(resources.GetObject("gridMedItems.EmbeddedNavigator.BackgroundImageLayout"), System.Windows.Forms.ImageLayout)
        Me.gridMedItems.EmbeddedNavigator.ImeMode = CType(resources.GetObject("gridMedItems.EmbeddedNavigator.ImeMode"), System.Windows.Forms.ImeMode)
        Me.gridMedItems.EmbeddedNavigator.MaximumSize = CType(resources.GetObject("gridMedItems.EmbeddedNavigator.MaximumSize"), System.Drawing.Size)
        Me.gridMedItems.EmbeddedNavigator.TextLocation = CType(resources.GetObject("gridMedItems.EmbeddedNavigator.TextLocation"), DevExpress.XtraEditors.NavigatorButtonsTextLocation)
        Me.gridMedItems.EmbeddedNavigator.ToolTip = resources.GetString("gridMedItems.EmbeddedNavigator.ToolTip")
        Me.gridMedItems.EmbeddedNavigator.ToolTipIconType = CType(resources.GetObject("gridMedItems.EmbeddedNavigator.ToolTipIconType"), DevExpress.Utils.ToolTipIconType)
        Me.gridMedItems.EmbeddedNavigator.ToolTipTitle = resources.GetString("gridMedItems.EmbeddedNavigator.ToolTipTitle")
        Me.gridMedItems.MainView = Me.GridView4
        Me.gridMedItems.Name = "gridMedItems"
        Me.gridMedItems.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridView4})
        '
        'GridView4
        '
        resources.ApplyResources(Me.GridView4, "GridView4")
        Me.GridView4.Appearance.HeaderPanel.Font = CType(resources.GetObject("GridView4.Appearance.HeaderPanel.Font"), System.Drawing.Font)
        Me.GridView4.Appearance.HeaderPanel.Options.UseFont = True
        Me.GridView4.Appearance.Row.Font = CType(resources.GetObject("GridView4.Appearance.Row.Font"), System.Drawing.Font)
        Me.GridView4.Appearance.Row.Options.UseFont = True
        Me.GridView4.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.colRowNum4, Me.colMedicineItemID, Me.colScincID1, Me.colCommName, Me.colCompany, Me.colNotes})
        Me.GridView4.GridControl = Me.gridMedItems
        Me.GridView4.Name = "GridView4"
        Me.GridView4.OptionsDetail.EnableMasterViewMode = False
        Me.GridView4.OptionsView.EnableAppearanceEvenRow = True
        Me.GridView4.OptionsView.EnableAppearanceOddRow = True
        Me.GridView4.OptionsView.ShowDetailButtons = False
        '
        'colRowNum4
        '
        resources.ApplyResources(Me.colRowNum4, "colRowNum4")
        Me.colRowNum4.FieldName = "colRowNum4"
        Me.colRowNum4.ImageOptions.ImageKey = resources.GetString("colRowNum4.ImageOptions.ImageKey")
        Me.colRowNum4.Name = "colRowNum4"
        Me.colRowNum4.UnboundDataType = GetType(Integer)
        '
        'colMedicineItemID
        '
        resources.ApplyResources(Me.colMedicineItemID, "colMedicineItemID")
        Me.colMedicineItemID.FieldName = "MedicineItemID"
        Me.colMedicineItemID.ImageOptions.ImageKey = resources.GetString("colMedicineItemID.ImageOptions.ImageKey")
        Me.colMedicineItemID.Name = "colMedicineItemID"
        '
        'colScincID1
        '
        resources.ApplyResources(Me.colScincID1, "colScincID1")
        Me.colScincID1.FieldName = "ScincID"
        Me.colScincID1.ImageOptions.ImageKey = resources.GetString("colScincID1.ImageOptions.ImageKey")
        Me.colScincID1.Name = "colScincID1"
        '
        'colCommName
        '
        resources.ApplyResources(Me.colCommName, "colCommName")
        Me.colCommName.FieldName = "CommName"
        Me.colCommName.ImageOptions.ImageKey = resources.GetString("colCommName.ImageOptions.ImageKey")
        Me.colCommName.Name = "colCommName"
        '
        'colCompany
        '
        resources.ApplyResources(Me.colCompany, "colCompany")
        Me.colCompany.FieldName = "Company"
        Me.colCompany.ImageOptions.ImageKey = resources.GetString("colCompany.ImageOptions.ImageKey")
        Me.colCompany.Name = "colCompany"
        '
        'colNotes
        '
        resources.ApplyResources(Me.colNotes, "colNotes")
        Me.colNotes.FieldName = "Notes"
        Me.colNotes.ImageOptions.ImageKey = resources.GetString("colNotes.ImageOptions.ImageKey")
        Me.colNotes.Name = "colNotes"
        '
        'btSaveMedic
        '
        resources.ApplyResources(Me.btSaveMedic, "btSaveMedic")
        Me.btSaveMedic.Appearance.Font = CType(resources.GetObject("btSaveMedic.Appearance.Font"), System.Drawing.Font)
        Me.btSaveMedic.Appearance.Options.UseFont = True
        Me.btSaveMedic.ImageOptions.ImageKey = resources.GetString("btSaveMedic.ImageOptions.ImageKey")
        Me.btSaveMedic.Name = "btSaveMedic"
        '
        'CompanyTxt
        '
        resources.ApplyResources(Me.CompanyTxt, "CompanyTxt")
        Me.CompanyTxt.Name = "CompanyTxt"
        Me.CompanyTxt.Properties.Appearance.Font = CType(resources.GetObject("CompanyTxt.Properties.Appearance.Font"), System.Drawing.Font)
        Me.CompanyTxt.Properties.Appearance.Options.UseFont = True
        '
        'LabelControl6
        '
        resources.ApplyResources(Me.LabelControl6, "LabelControl6")
        Me.LabelControl6.Appearance.Font = CType(resources.GetObject("LabelControl6.Appearance.Font"), System.Drawing.Font)
        Me.LabelControl6.Appearance.Options.UseFont = True
        Me.LabelControl6.Name = "LabelControl6"
        '
        'Commercial
        '
        resources.ApplyResources(Me.Commercial, "Commercial")
        Me.Commercial.Name = "Commercial"
        Me.Commercial.Properties.Appearance.Font = CType(resources.GetObject("Commercial.Properties.Appearance.Font"), System.Drawing.Font)
        Me.Commercial.Properties.Appearance.Options.UseFont = True
        '
        'btAddMedicine
        '
        resources.ApplyResources(Me.btAddMedicine, "btAddMedicine")
        Me.btAddMedicine.Appearance.Font = CType(resources.GetObject("btAddMedicine.Appearance.Font"), System.Drawing.Font)
        Me.btAddMedicine.Appearance.Options.UseFont = True
        Me.btAddMedicine.ImageOptions.ImageKey = resources.GetString("btAddMedicine.ImageOptions.ImageKey")
        Me.btAddMedicine.Name = "btAddMedicine"
        '
        'LabelControl9
        '
        resources.ApplyResources(Me.LabelControl9, "LabelControl9")
        Me.LabelControl9.Appearance.Font = CType(resources.GetObject("LabelControl9.Appearance.Font"), System.Drawing.Font)
        Me.LabelControl9.Appearance.Options.UseFont = True
        Me.LabelControl9.Name = "LabelControl9"
        '
        'btEditMedic
        '
        resources.ApplyResources(Me.btEditMedic, "btEditMedic")
        Me.btEditMedic.Appearance.Font = CType(resources.GetObject("btEditMedic.Appearance.Font"), System.Drawing.Font)
        Me.btEditMedic.Appearance.Options.UseFont = True
        Me.btEditMedic.ImageOptions.ImageKey = resources.GetString("btEditMedic.ImageOptions.ImageKey")
        Me.btEditMedic.Name = "btEditMedic"
        '
        'LabelControl8
        '
        resources.ApplyResources(Me.LabelControl8, "LabelControl8")
        Me.LabelControl8.Appearance.Font = CType(resources.GetObject("LabelControl8.Appearance.Font"), System.Drawing.Font)
        Me.LabelControl8.Appearance.Options.UseFont = True
        Me.LabelControl8.Name = "LabelControl8"
        '
        'LabelControl7
        '
        resources.ApplyResources(Me.LabelControl7, "LabelControl7")
        Me.LabelControl7.Appearance.Font = CType(resources.GetObject("LabelControl7.Appearance.Font"), System.Drawing.Font)
        Me.LabelControl7.Appearance.Options.UseFont = True
        Me.LabelControl7.Name = "LabelControl7"
        '
        'MedicNote
        '
        resources.ApplyResources(Me.MedicNote, "MedicNote")
        Me.MedicNote.Name = "MedicNote"
        Me.MedicNote.Properties.Appearance.Font = CType(resources.GetObject("MedicNote.Properties.Appearance.Font"), System.Drawing.Font)
        Me.MedicNote.Properties.Appearance.Options.UseFont = True
        '
        'ItemsNavigator
        '
        resources.ApplyResources(Me.ItemsNavigator, "ItemsNavigator")
        Me.ItemsNavigator.AddNewItem = Nothing
        Me.ItemsNavigator.BindingSource = Me.MedicineItemsBindingSource
        Me.ItemsNavigator.CountItem = Me.ToolStripLabel5
        Me.ItemsNavigator.DeleteItem = Me.ToolStripButton25
        Me.ItemsNavigator.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripButton26, Me.ToolStripButton27, Me.ToolStripSeparator13, Me.ToolStripTextBox5, Me.ToolStripLabel5, Me.ToolStripSeparator14, Me.ToolStripButton28, Me.ToolStripButton29, Me.ToolStripSeparator15, Me.ToolStripButton25, Me.ToolStripButton30})
        Me.ItemsNavigator.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow
        Me.ItemsNavigator.MoveFirstItem = Me.ToolStripButton26
        Me.ItemsNavigator.MoveLastItem = Me.ToolStripButton29
        Me.ItemsNavigator.MoveNextItem = Me.ToolStripButton28
        Me.ItemsNavigator.MovePreviousItem = Me.ToolStripButton27
        Me.ItemsNavigator.Name = "ItemsNavigator"
        Me.ItemsNavigator.PositionItem = Me.ToolStripTextBox5
        '
        'ToolStripLabel5
        '
        resources.ApplyResources(Me.ToolStripLabel5, "ToolStripLabel5")
        Me.ToolStripLabel5.Name = "ToolStripLabel5"
        '
        'ToolStripButton25
        '
        resources.ApplyResources(Me.ToolStripButton25, "ToolStripButton25")
        Me.ToolStripButton25.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton25.Name = "ToolStripButton25"
        '
        'ToolStripButton26
        '
        resources.ApplyResources(Me.ToolStripButton26, "ToolStripButton26")
        Me.ToolStripButton26.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton26.Name = "ToolStripButton26"
        '
        'ToolStripButton27
        '
        resources.ApplyResources(Me.ToolStripButton27, "ToolStripButton27")
        Me.ToolStripButton27.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton27.Name = "ToolStripButton27"
        '
        'ToolStripSeparator13
        '
        resources.ApplyResources(Me.ToolStripSeparator13, "ToolStripSeparator13")
        Me.ToolStripSeparator13.Name = "ToolStripSeparator13"
        '
        'ToolStripTextBox5
        '
        resources.ApplyResources(Me.ToolStripTextBox5, "ToolStripTextBox5")
        Me.ToolStripTextBox5.Name = "ToolStripTextBox5"
        '
        'ToolStripSeparator14
        '
        resources.ApplyResources(Me.ToolStripSeparator14, "ToolStripSeparator14")
        Me.ToolStripSeparator14.Name = "ToolStripSeparator14"
        '
        'ToolStripButton28
        '
        resources.ApplyResources(Me.ToolStripButton28, "ToolStripButton28")
        Me.ToolStripButton28.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton28.Name = "ToolStripButton28"
        '
        'ToolStripButton29
        '
        resources.ApplyResources(Me.ToolStripButton29, "ToolStripButton29")
        Me.ToolStripButton29.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton29.Name = "ToolStripButton29"
        '
        'ToolStripSeparator15
        '
        resources.ApplyResources(Me.ToolStripSeparator15, "ToolStripSeparator15")
        Me.ToolStripSeparator15.Name = "ToolStripSeparator15"
        '
        'ToolStripButton30
        '
        resources.ApplyResources(Me.ToolStripButton30, "ToolStripButton30")
        Me.ToolStripButton30.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton30.Name = "ToolStripButton30"
        '
        'GroupControl3
        '
        resources.ApplyResources(Me.GroupControl3, "GroupControl3")
        Me.GroupControl3.AppearanceCaption.Font = CType(resources.GetObject("GroupControl3.AppearanceCaption.Font"), System.Drawing.Font)
        Me.GroupControl3.AppearanceCaption.Options.UseFont = True
        Me.GroupControl3.Controls.Add(Me.FamilyCombo)
        Me.GroupControl3.Controls.Add(Me.gridMedScinFamily)
        Me.GroupControl3.Controls.Add(Me.btSaveScinc)
        Me.GroupControl3.Controls.Add(Me.btAddScience)
        Me.GroupControl3.Controls.Add(Me.btEditScinc)
        Me.GroupControl3.Controls.Add(Me.LabelControl4)
        Me.GroupControl3.Controls.Add(Me.Scintific)
        Me.GroupControl3.Controls.Add(Me.LabelControl5)
        Me.GroupControl3.Controls.Add(Me.LabelControl17)
        Me.GroupControl3.Controls.Add(Me.SciencNavigator)
        Me.GroupControl3.Name = "GroupControl3"
        '
        'FamilyCombo
        '
        resources.ApplyResources(Me.FamilyCombo, "FamilyCombo")
        Me.FamilyCombo.DataSource = Me.MedicineFamilyBindingSource
        Me.FamilyCombo.DisplayMember = "MedicineSubCat"
        Me.FamilyCombo.FormattingEnabled = True
        Me.FamilyCombo.Name = "FamilyCombo"
        Me.FamilyCombo.ValueMember = "SubCatID"
        '
        'gridMedScinFamily
        '
        resources.ApplyResources(Me.gridMedScinFamily, "gridMedScinFamily")
        Me.gridMedScinFamily.DataSource = Me.ScienceFamilyBindingSource
        Me.gridMedScinFamily.EmbeddedNavigator.AccessibleDescription = resources.GetString("gridMedScinFamily.EmbeddedNavigator.AccessibleDescription")
        Me.gridMedScinFamily.EmbeddedNavigator.AccessibleName = resources.GetString("gridMedScinFamily.EmbeddedNavigator.AccessibleName")
        Me.gridMedScinFamily.EmbeddedNavigator.AllowHtmlTextInToolTip = CType(resources.GetObject("gridMedScinFamily.EmbeddedNavigator.AllowHtmlTextInToolTip"), DevExpress.Utils.DefaultBoolean)
        Me.gridMedScinFamily.EmbeddedNavigator.Anchor = CType(resources.GetObject("gridMedScinFamily.EmbeddedNavigator.Anchor"), System.Windows.Forms.AnchorStyles)
        Me.gridMedScinFamily.EmbeddedNavigator.AutoSize = CType(resources.GetObject("gridMedScinFamily.EmbeddedNavigator.AutoSize"), Boolean)
        Me.gridMedScinFamily.EmbeddedNavigator.BackgroundImage = CType(resources.GetObject("gridMedScinFamily.EmbeddedNavigator.BackgroundImage"), System.Drawing.Image)
        Me.gridMedScinFamily.EmbeddedNavigator.BackgroundImageLayout = CType(resources.GetObject("gridMedScinFamily.EmbeddedNavigator.BackgroundImageLayout"), System.Windows.Forms.ImageLayout)
        Me.gridMedScinFamily.EmbeddedNavigator.ImeMode = CType(resources.GetObject("gridMedScinFamily.EmbeddedNavigator.ImeMode"), System.Windows.Forms.ImeMode)
        Me.gridMedScinFamily.EmbeddedNavigator.MaximumSize = CType(resources.GetObject("gridMedScinFamily.EmbeddedNavigator.MaximumSize"), System.Drawing.Size)
        Me.gridMedScinFamily.EmbeddedNavigator.TextLocation = CType(resources.GetObject("gridMedScinFamily.EmbeddedNavigator.TextLocation"), DevExpress.XtraEditors.NavigatorButtonsTextLocation)
        Me.gridMedScinFamily.EmbeddedNavigator.ToolTip = resources.GetString("gridMedScinFamily.EmbeddedNavigator.ToolTip")
        Me.gridMedScinFamily.EmbeddedNavigator.ToolTipIconType = CType(resources.GetObject("gridMedScinFamily.EmbeddedNavigator.ToolTipIconType"), DevExpress.Utils.ToolTipIconType)
        Me.gridMedScinFamily.EmbeddedNavigator.ToolTipTitle = resources.GetString("gridMedScinFamily.EmbeddedNavigator.ToolTipTitle")
        Me.gridMedScinFamily.MainView = Me.GridView3
        Me.gridMedScinFamily.Name = "gridMedScinFamily"
        Me.gridMedScinFamily.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridView3})
        '
        'GridView3
        '
        resources.ApplyResources(Me.GridView3, "GridView3")
        Me.GridView3.Appearance.HeaderPanel.Font = CType(resources.GetObject("GridView3.Appearance.HeaderPanel.Font"), System.Drawing.Font)
        Me.GridView3.Appearance.HeaderPanel.Options.UseFont = True
        Me.GridView3.Appearance.Row.Font = CType(resources.GetObject("GridView3.Appearance.Row.Font"), System.Drawing.Font)
        Me.GridView3.Appearance.Row.Options.UseFont = True
        Me.GridView3.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.colRowNum3, Me.colScincID, Me.colSubCatID1, Me.colScienceName})
        Me.GridView3.GridControl = Me.gridMedScinFamily
        Me.GridView3.Name = "GridView3"
        Me.GridView3.OptionsDetail.EnableMasterViewMode = False
        Me.GridView3.OptionsView.EnableAppearanceEvenRow = True
        Me.GridView3.OptionsView.EnableAppearanceOddRow = True
        Me.GridView3.OptionsView.ShowDetailButtons = False
        '
        'colRowNum3
        '
        resources.ApplyResources(Me.colRowNum3, "colRowNum3")
        Me.colRowNum3.FieldName = "colRowNum3"
        Me.colRowNum3.ImageOptions.ImageKey = resources.GetString("colRowNum3.ImageOptions.ImageKey")
        Me.colRowNum3.Name = "colRowNum3"
        Me.colRowNum3.UnboundDataType = GetType(Integer)
        '
        'colScincID
        '
        resources.ApplyResources(Me.colScincID, "colScincID")
        Me.colScincID.FieldName = "ScincID"
        Me.colScincID.ImageOptions.ImageKey = resources.GetString("colScincID.ImageOptions.ImageKey")
        Me.colScincID.Name = "colScincID"
        '
        'colSubCatID1
        '
        resources.ApplyResources(Me.colSubCatID1, "colSubCatID1")
        Me.colSubCatID1.FieldName = "SubCatID"
        Me.colSubCatID1.ImageOptions.ImageKey = resources.GetString("colSubCatID1.ImageOptions.ImageKey")
        Me.colSubCatID1.Name = "colSubCatID1"
        '
        'colScienceName
        '
        resources.ApplyResources(Me.colScienceName, "colScienceName")
        Me.colScienceName.FieldName = "ScienceName"
        Me.colScienceName.ImageOptions.ImageKey = resources.GetString("colScienceName.ImageOptions.ImageKey")
        Me.colScienceName.Name = "colScienceName"
        '
        'btSaveScinc
        '
        resources.ApplyResources(Me.btSaveScinc, "btSaveScinc")
        Me.btSaveScinc.Appearance.Font = CType(resources.GetObject("btSaveScinc.Appearance.Font"), System.Drawing.Font)
        Me.btSaveScinc.Appearance.Options.UseFont = True
        Me.btSaveScinc.ImageOptions.ImageKey = resources.GetString("btSaveScinc.ImageOptions.ImageKey")
        Me.btSaveScinc.Name = "btSaveScinc"
        '
        'btAddScience
        '
        resources.ApplyResources(Me.btAddScience, "btAddScience")
        Me.btAddScience.Appearance.Font = CType(resources.GetObject("btAddScience.Appearance.Font"), System.Drawing.Font)
        Me.btAddScience.Appearance.Options.UseFont = True
        Me.btAddScience.ImageOptions.ImageKey = resources.GetString("btAddScience.ImageOptions.ImageKey")
        Me.btAddScience.Name = "btAddScience"
        '
        'btEditScinc
        '
        resources.ApplyResources(Me.btEditScinc, "btEditScinc")
        Me.btEditScinc.Appearance.Font = CType(resources.GetObject("btEditScinc.Appearance.Font"), System.Drawing.Font)
        Me.btEditScinc.Appearance.Options.UseFont = True
        Me.btEditScinc.ImageOptions.ImageKey = resources.GetString("btEditScinc.ImageOptions.ImageKey")
        Me.btEditScinc.Name = "btEditScinc"
        '
        'LabelControl4
        '
        resources.ApplyResources(Me.LabelControl4, "LabelControl4")
        Me.LabelControl4.Appearance.Font = CType(resources.GetObject("LabelControl4.Appearance.Font"), System.Drawing.Font)
        Me.LabelControl4.Appearance.Options.UseFont = True
        Me.LabelControl4.Name = "LabelControl4"
        '
        'Scintific
        '
        resources.ApplyResources(Me.Scintific, "Scintific")
        Me.Scintific.Name = "Scintific"
        Me.Scintific.Properties.Appearance.Font = CType(resources.GetObject("Scintific.Properties.Appearance.Font"), System.Drawing.Font)
        Me.Scintific.Properties.Appearance.Options.UseFont = True
        '
        'LabelControl5
        '
        resources.ApplyResources(Me.LabelControl5, "LabelControl5")
        Me.LabelControl5.Appearance.Font = CType(resources.GetObject("LabelControl5.Appearance.Font"), System.Drawing.Font)
        Me.LabelControl5.Appearance.Options.UseFont = True
        Me.LabelControl5.Name = "LabelControl5"
        '
        'LabelControl17
        '
        resources.ApplyResources(Me.LabelControl17, "LabelControl17")
        Me.LabelControl17.Appearance.Font = CType(resources.GetObject("LabelControl17.Appearance.Font"), System.Drawing.Font)
        Me.LabelControl17.Appearance.ForeColor = System.Drawing.Color.Green
        Me.LabelControl17.Appearance.Options.UseFont = True
        Me.LabelControl17.Appearance.Options.UseForeColor = True
        Me.LabelControl17.Appearance.Options.UseTextOptions = True
        Me.LabelControl17.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.LabelControl17.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.LabelControl17.Name = "LabelControl17"
        '
        'SciencNavigator
        '
        resources.ApplyResources(Me.SciencNavigator, "SciencNavigator")
        Me.SciencNavigator.AddNewItem = Nothing
        Me.SciencNavigator.BindingSource = Me.ScienceFamilyBindingSource
        Me.SciencNavigator.CountItem = Me.ToolStripLabel1
        Me.SciencNavigator.DeleteItem = Me.ToolStripButton1
        Me.SciencNavigator.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripButton2, Me.ToolStripButton3, Me.ToolStripSeparator1, Me.ToolStripTextBox1, Me.ToolStripLabel1, Me.ToolStripSeparator2, Me.ToolStripButton4, Me.ToolStripButton5, Me.ToolStripSeparator3, Me.ToolStripButton1, Me.ToolStripButton6})
        Me.SciencNavigator.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow
        Me.SciencNavigator.MoveFirstItem = Me.ToolStripButton2
        Me.SciencNavigator.MoveLastItem = Me.ToolStripButton5
        Me.SciencNavigator.MoveNextItem = Me.ToolStripButton4
        Me.SciencNavigator.MovePreviousItem = Me.ToolStripButton3
        Me.SciencNavigator.Name = "SciencNavigator"
        Me.SciencNavigator.PositionItem = Me.ToolStripTextBox1
        '
        'ToolStripLabel1
        '
        resources.ApplyResources(Me.ToolStripLabel1, "ToolStripLabel1")
        Me.ToolStripLabel1.Name = "ToolStripLabel1"
        '
        'ToolStripButton1
        '
        resources.ApplyResources(Me.ToolStripButton1, "ToolStripButton1")
        Me.ToolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton1.Name = "ToolStripButton1"
        '
        'ToolStripButton2
        '
        resources.ApplyResources(Me.ToolStripButton2, "ToolStripButton2")
        Me.ToolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton2.Name = "ToolStripButton2"
        '
        'ToolStripButton3
        '
        resources.ApplyResources(Me.ToolStripButton3, "ToolStripButton3")
        Me.ToolStripButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton3.Name = "ToolStripButton3"
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
        'ToolStripButton4
        '
        resources.ApplyResources(Me.ToolStripButton4, "ToolStripButton4")
        Me.ToolStripButton4.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton4.Name = "ToolStripButton4"
        '
        'ToolStripButton5
        '
        resources.ApplyResources(Me.ToolStripButton5, "ToolStripButton5")
        Me.ToolStripButton5.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton5.Name = "ToolStripButton5"
        '
        'ToolStripSeparator3
        '
        resources.ApplyResources(Me.ToolStripSeparator3, "ToolStripSeparator3")
        Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
        '
        'ToolStripButton6
        '
        resources.ApplyResources(Me.ToolStripButton6, "ToolStripButton6")
        Me.ToolStripButton6.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton6.Name = "ToolStripButton6"
        '
        'TableAdapterManager
        '
        Me.TableAdapterManager.BackupDataSetBeforeUpdate = False
        Me.TableAdapterManager.MedicineDozeTableAdapter = Me.MedicineDozeTableAdapter
        Me.TableAdapterManager.MedicineFamilyTableAdapter = Me.MedicineFamilyTableAdapter
        Me.TableAdapterManager.MedicineGroupsTableAdapter = Me.MedicineGroupsTableAdapter
        Me.TableAdapterManager.MedicineItemsTableAdapter = Me.MedicineItemsTableAdapter
        Me.TableAdapterManager.MedicineShapeTableAdapter = Me.MedicineShapeTableAdapter
        Me.TableAdapterManager.MedScienceFamilyTableAdapter = Me.MedScienceFamilyTableAdapter
        Me.TableAdapterManager.UpdateOrder = DentistX.MedDsTableAdapters.TableAdapterManager.UpdateOrderOption.InsertUpdateDelete
        '
        'MedicineDozeTableAdapter
        '
        Me.MedicineDozeTableAdapter.ClearBeforeFill = True
        '
        'MedicineFamilyTableAdapter
        '
        Me.MedicineFamilyTableAdapter.ClearBeforeFill = True
        '
        'MedicineGroupsTableAdapter
        '
        Me.MedicineGroupsTableAdapter.ClearBeforeFill = True
        '
        'MedicineItemsTableAdapter
        '
        Me.MedicineItemsTableAdapter.ClearBeforeFill = True
        '
        'MedicineShapeTableAdapter
        '
        Me.MedicineShapeTableAdapter.ClearBeforeFill = True
        '
        'MedScienceFamilyTableAdapter
        '
        Me.MedScienceFamilyTableAdapter.ClearBeforeFill = True
        '
        'MedicFormDS
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.SidePanel1)
        Me.Controls.Add(Me.PanelControl2)
        Me.Controls.Add(Me.PanelControl1)
        Me.Name = "MedicFormDS"
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl1.ResumeLayout(False)
        CType(Me.GroupControl2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl2.ResumeLayout(False)
        Me.GroupControl2.PerformLayout()
        CType(Me.MedicineGroupsBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MedDs, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gridMedFamily, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MedicineFamilyBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MedicineFamily.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FamlyNavigator, System.ComponentModel.ISupportInitialize).EndInit()
        Me.FamlyNavigator.ResumeLayout(False)
        Me.FamlyNavigator.PerformLayout()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl1.ResumeLayout(False)
        Me.GroupControl1.PerformLayout()
        CType(Me.MedicineGroupsDataGridView, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MedicenGroup.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GrpNavigator, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GrpNavigator.ResumeLayout(False)
        Me.GrpNavigator.PerformLayout()
        CType(Me.PanelControl2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl2.ResumeLayout(False)
        CType(Me.GroupControl6, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl6.ResumeLayout(False)
        Me.GroupControl6.PerformLayout()
        CType(Me.MedicineShapeBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MedicineItemsBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ScienceFamilyBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gridMedDose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MedicineDozeBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Doze.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DozeNavigator, System.ComponentModel.ISupportInitialize).EndInit()
        Me.DozeNavigator.ResumeLayout(False)
        Me.DozeNavigator.PerformLayout()
        CType(Me.GroupControl5, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl5.ResumeLayout(False)
        Me.GroupControl5.PerformLayout()
        CType(Me.gridMedShape, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ShapeInfotxt.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Shape.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ShapeNavigator, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ShapeNavigator.ResumeLayout(False)
        Me.ShapeNavigator.PerformLayout()
        Me.SidePanel1.ResumeLayout(False)
        CType(Me.GroupControl4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl4.ResumeLayout(False)
        Me.GroupControl4.PerformLayout()
        CType(Me.gridMedItems, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CompanyTxt.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Commercial.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MedicNote.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ItemsNavigator, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ItemsNavigator.ResumeLayout(False)
        Me.ItemsNavigator.PerformLayout()
        CType(Me.GroupControl3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl3.ResumeLayout(False)
        Me.GroupControl3.PerformLayout()
        CType(Me.gridMedScinFamily, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Scintific.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SciencNavigator, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SciencNavigator.ResumeLayout(False)
        Me.SciencNavigator.PerformLayout()
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
    Friend WithEvents btSaveGrp As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btEditGrp As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btAddGrp As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents MedicenGroup As DevExpress.XtraEditors.TextEdit
    Friend WithEvents gridMedFamily As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridView2 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents MedicineGroupsDataGridView As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridView1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents gridMedDose As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridView6 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents gridMedShape As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridView5 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents gridMedItems As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridView4 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents gridMedScinFamily As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridView3 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents btSaveFamly As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btEditFamly As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btAddFamily As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents MedicineFamily As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl3 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents btSaveMedic As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents MedicNote As DevExpress.XtraEditors.TextEdit
    Friend WithEvents CompanyTxt As DevExpress.XtraEditors.TextEdit
    Friend WithEvents Commercial As DevExpress.XtraEditors.TextEdit
    Friend WithEvents btAddMedicine As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LabelControl9 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents btEditMedic As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LabelControl8 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl7 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents btSaveScinc As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btAddScience As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btEditScinc As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LabelControl4 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents Scintific As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents btSaveDoze As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btEditDoze As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LabelControl13 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents Doze As DevExpress.XtraEditors.TextEdit
    Friend WithEvents btAddDoze As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btSaveShape As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btAddShape As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btEditShape As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ShapeInfotxt As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl12 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents Shape As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl11 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl5 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl6 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl10 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl14 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents MedDs As MedDs
    Friend WithEvents Qrs As MedDsTableAdapters.MedQueries
    Friend WithEvents TableAdapterManager As MedDsTableAdapters.TableAdapterManager
    Friend WithEvents MedicineDozeTableAdapter As MedDsTableAdapters.MedicineDozeTableAdapter
    Friend WithEvents MedicineFamilyTableAdapter As MedDsTableAdapters.MedicineFamilyTableAdapter
    Friend WithEvents MedicineGroupsTableAdapter As MedDsTableAdapters.MedicineGroupsTableAdapter
    Friend WithEvents MedicineItemsTableAdapter As MedDsTableAdapters.MedicineItemsTableAdapter
    Friend WithEvents MedicineShapeTableAdapter As MedDsTableAdapters.MedicineShapeTableAdapter
    Friend WithEvents MedScienceFamilyTableAdapter As MedDsTableAdapters.MedScienceFamilyTableAdapter
    Friend WithEvents GrpNavigator As BindingNavigator
    Friend WithEvents BindingNavigatorCountItem As ToolStripLabel
    Friend WithEvents BindingNavigatorDeleteItem As ToolStripButton
    Friend WithEvents BindingNavigatorMoveFirstItem As ToolStripButton
    Friend WithEvents BindingNavigatorMovePreviousItem As ToolStripButton
    Friend WithEvents BindingNavigatorSeparator As ToolStripSeparator
    Friend WithEvents BindingNavigatorPositionItem As ToolStripTextBox
    Friend WithEvents BindingNavigatorSeparator1 As ToolStripSeparator
    Friend WithEvents BindingNavigatorMoveNextItem As ToolStripButton
    Friend WithEvents BindingNavigatorMoveLastItem As ToolStripButton
    Friend WithEvents BindingNavigatorSeparator2 As ToolStripSeparator
    Friend WithEvents PatientBindingNavigatorSaveItem As ToolStripButton
    Friend WithEvents FamlyNavigator As BindingNavigator
    Friend WithEvents ToolStripLabel4 As ToolStripLabel
    Friend WithEvents ToolStripButton19 As ToolStripButton
    Friend WithEvents ToolStripButton20 As ToolStripButton
    Friend WithEvents ToolStripButton21 As ToolStripButton
    Friend WithEvents ToolStripSeparator10 As ToolStripSeparator
    Friend WithEvents ToolStripTextBox4 As ToolStripTextBox
    Friend WithEvents ToolStripSeparator11 As ToolStripSeparator
    Friend WithEvents ToolStripButton22 As ToolStripButton
    Friend WithEvents ToolStripButton23 As ToolStripButton
    Friend WithEvents ToolStripSeparator12 As ToolStripSeparator
    Friend WithEvents ToolStripButton24 As ToolStripButton
    Friend WithEvents SciencNavigator As BindingNavigator
    Friend WithEvents ToolStripLabel1 As ToolStripLabel
    Friend WithEvents ToolStripButton1 As ToolStripButton
    Friend WithEvents ToolStripButton2 As ToolStripButton
    Friend WithEvents ToolStripButton3 As ToolStripButton
    Friend WithEvents ToolStripSeparator1 As ToolStripSeparator
    Friend WithEvents ToolStripTextBox1 As ToolStripTextBox
    Friend WithEvents ToolStripSeparator2 As ToolStripSeparator
    Friend WithEvents ToolStripButton4 As ToolStripButton
    Friend WithEvents ToolStripButton5 As ToolStripButton
    Friend WithEvents ToolStripSeparator3 As ToolStripSeparator
    Friend WithEvents ToolStripButton6 As ToolStripButton
    Friend WithEvents ItemsNavigator As BindingNavigator
    Friend WithEvents ToolStripLabel5 As ToolStripLabel
    Friend WithEvents ToolStripButton25 As ToolStripButton
    Friend WithEvents ToolStripButton26 As ToolStripButton
    Friend WithEvents ToolStripButton27 As ToolStripButton
    Friend WithEvents ToolStripSeparator13 As ToolStripSeparator
    Friend WithEvents ToolStripTextBox5 As ToolStripTextBox
    Friend WithEvents ToolStripSeparator14 As ToolStripSeparator
    Friend WithEvents ToolStripButton28 As ToolStripButton
    Friend WithEvents ToolStripButton29 As ToolStripButton
    Friend WithEvents ToolStripSeparator15 As ToolStripSeparator
    Friend WithEvents ToolStripButton30 As ToolStripButton
    Friend WithEvents ShapeNavigator As BindingNavigator
    Friend WithEvents ToolStripLabel2 As ToolStripLabel
    Friend WithEvents ToolStripButton7 As ToolStripButton
    Friend WithEvents ToolStripButton8 As ToolStripButton
    Friend WithEvents ToolStripButton9 As ToolStripButton
    Friend WithEvents ToolStripSeparator4 As ToolStripSeparator
    Friend WithEvents ToolStripTextBox2 As ToolStripTextBox
    Friend WithEvents ToolStripSeparator5 As ToolStripSeparator
    Friend WithEvents ToolStripButton10 As ToolStripButton
    Friend WithEvents ToolStripButton11 As ToolStripButton
    Friend WithEvents ToolStripSeparator6 As ToolStripSeparator
    Friend WithEvents ToolStripButton12 As ToolStripButton
    Friend WithEvents DozeNavigator As BindingNavigator
    Friend WithEvents ToolStripLabel3 As ToolStripLabel
    Friend WithEvents ToolStripButton13 As ToolStripButton
    Friend WithEvents ToolStripButton14 As ToolStripButton
    Friend WithEvents ToolStripButton15 As ToolStripButton
    Friend WithEvents ToolStripSeparator7 As ToolStripSeparator
    Friend WithEvents ToolStripTextBox3 As ToolStripTextBox
    Friend WithEvents ToolStripSeparator8 As ToolStripSeparator
    Friend WithEvents ToolStripButton16 As ToolStripButton
    Friend WithEvents ToolStripButton17 As ToolStripButton
    Friend WithEvents ToolStripSeparator9 As ToolStripSeparator
    Friend WithEvents ToolStripButton18 As ToolStripButton
    Friend WithEvents MedicineGroupsBindingSource As BindingSource
    Friend WithEvents MedicineFamilyBindingSource As BindingSource
    Friend WithEvents ScienceFamilyBindingSource As BindingSource
    Friend WithEvents MedicineItemsBindingSource As BindingSource
    Friend WithEvents MedicineShapeBindingSource As BindingSource
    Friend WithEvents MedicineDozeBindingSource As BindingSource
    Friend WithEvents GroupCombo As ComboBox
    Friend WithEvents FamilyCombo As ComboBox
    Friend WithEvents ScientCombo As ComboBox
    Friend WithEvents itemCombo As ComboBox
    Friend WithEvents shapeCombo As ComboBox
    Friend WithEvents colSubCatID As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colMedicineID1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colMedicineSubCat As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colMedicineID As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colMedicineFamily As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colDozeID As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colShapeID1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colDoze As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colShapeID As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colMedicineItemID1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colMedicineShape As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colShapeInfo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colMedicineItemID As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colScincID1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colCommName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colCompany As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colNotes As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colScincID As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colSubCatID1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colScienceName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colRowNum2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colRowNum1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colRowNum5 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colRowNum4 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colRowNum3 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colRowNum6 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents LabelControl15 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl16 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl19 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl20 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl18 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl17 As DevExpress.XtraEditors.LabelControl
End Class
