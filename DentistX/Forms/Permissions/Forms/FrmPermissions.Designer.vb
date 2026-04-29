<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmPermissions
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmPermissions))
        Me.Splitter1 = New DevExpress.XtraEditors.SplitContainerControl()
        Me.DGV = New DevExpress.XtraGrid.GridControl()
        Me.dgView = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.colRowNum = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colPermID = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colPermKey = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colPermName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colPermDescription = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colCategory = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colIsActive = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colCreatedAt = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.btnAdd = New DevExpress.XtraEditors.SimpleButton()
        Me.btnEdit = New DevExpress.XtraEditors.SimpleButton()
        Me.btnDel = New DevExpress.XtraEditors.SimpleButton()
        Me.PermIDSpinEdit = New DevExpress.XtraEditors.SpinEdit()
        Me.PermIDLabel = New DevExpress.XtraEditors.LabelControl()
        Me.butClose = New DevExpress.XtraEditors.SimpleButton()
        Me.PermKeyTextEdit = New DevExpress.XtraEditors.TextEdit()
        Me.PermNameTextEdit = New DevExpress.XtraEditors.TextEdit()
        Me.IsActiveLabel = New DevExpress.XtraEditors.LabelControl()
        Me.PermDescriptionTextEdit = New DevExpress.XtraEditors.TextEdit()
        Me.PermKeyLabel = New DevExpress.XtraEditors.LabelControl()
        Me.CategoryTextEdit = New DevExpress.XtraEditors.TextEdit()
        Me.CreatedAtLabel = New DevExpress.XtraEditors.LabelControl()
        Me.IsActiveCheckEdit = New DevExpress.XtraEditors.CheckEdit()
        Me.CreatedAtDateEdit = New DevExpress.XtraEditors.DateEdit()
        Me.CategoryLabel = New DevExpress.XtraEditors.LabelControl()
        Me.PermNameLabel = New DevExpress.XtraEditors.LabelControl()
        Me.PermDescriptionLabel = New DevExpress.XtraEditors.LabelControl()
        CType(Me.Splitter1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Splitter1.Panel1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Splitter1.Panel1.SuspendLayout()
        CType(Me.Splitter1.Panel2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Splitter1.Panel2.SuspendLayout()
        Me.Splitter1.SuspendLayout()
        CType(Me.DGV, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgView, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PermIDSpinEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PermKeyTextEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PermNameTextEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PermDescriptionTextEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CategoryTextEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IsActiveCheckEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CreatedAtDateEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CreatedAtDateEdit.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Splitter1
        '
        Me.Splitter1.CollapsePanel = DevExpress.XtraEditors.SplitCollapsePanel.Panel2
        resources.ApplyResources(Me.Splitter1, "Splitter1")
        Me.Splitter1.Horizontal = False
        Me.Splitter1.Name = "Splitter1"
        '
        'Splitter1.Panel1
        '
        Me.Splitter1.Panel1.Controls.Add(Me.DGV)
        '
        'Splitter1.Panel2
        '
        Me.Splitter1.Panel2.Controls.Add(Me.btnAdd)
        Me.Splitter1.Panel2.Controls.Add(Me.btnEdit)
        Me.Splitter1.Panel2.Controls.Add(Me.btnDel)
        Me.Splitter1.Panel2.Controls.Add(Me.PermIDSpinEdit)
        Me.Splitter1.Panel2.Controls.Add(Me.PermIDLabel)
        Me.Splitter1.Panel2.Controls.Add(Me.butClose)
        Me.Splitter1.Panel2.Controls.Add(Me.PermKeyTextEdit)
        Me.Splitter1.Panel2.Controls.Add(Me.PermNameTextEdit)
        Me.Splitter1.Panel2.Controls.Add(Me.IsActiveLabel)
        Me.Splitter1.Panel2.Controls.Add(Me.PermDescriptionTextEdit)
        Me.Splitter1.Panel2.Controls.Add(Me.PermKeyLabel)
        Me.Splitter1.Panel2.Controls.Add(Me.CategoryTextEdit)
        Me.Splitter1.Panel2.Controls.Add(Me.CreatedAtLabel)
        Me.Splitter1.Panel2.Controls.Add(Me.IsActiveCheckEdit)
        Me.Splitter1.Panel2.Controls.Add(Me.CreatedAtDateEdit)
        Me.Splitter1.Panel2.Controls.Add(Me.CategoryLabel)
        Me.Splitter1.Panel2.Controls.Add(Me.PermNameLabel)
        Me.Splitter1.Panel2.Controls.Add(Me.PermDescriptionLabel)
        Me.Splitter1.SplitterPosition = 433
        '
        'DGV
        '
        resources.ApplyResources(Me.DGV, "DGV")
        Me.DGV.EmbeddedNavigator.Appearance.Options.UseFont = True
        Me.DGV.EmbeddedNavigator.Buttons.Append.Visible = False
        Me.DGV.EmbeddedNavigator.Buttons.CancelEdit.Visible = False
        Me.DGV.EmbeddedNavigator.Buttons.Edit.Visible = False
        Me.DGV.EmbeddedNavigator.Buttons.EndEdit.Visible = False
        Me.DGV.EmbeddedNavigator.Buttons.Remove.Visible = False
        Me.DGV.MainView = Me.dgView
        Me.DGV.Name = "DGV"
        Me.DGV.UseEmbeddedNavigator = True
        Me.DGV.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.dgView})
        '
        'dgView
        '
        Me.dgView.Appearance.HeaderPanel.Font = CType(resources.GetObject("dgView.Appearance.HeaderPanel.Font"), System.Drawing.Font)
        Me.dgView.Appearance.HeaderPanel.Options.UseFont = True
        Me.dgView.Appearance.Row.Font = CType(resources.GetObject("dgView.Appearance.Row.Font"), System.Drawing.Font)
        Me.dgView.Appearance.Row.Options.UseFont = True
        Me.dgView.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.dgView.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.colRowNum, Me.colPermID, Me.colPermKey, Me.colPermName, Me.colPermDescription, Me.colCategory, Me.colIsActive, Me.colCreatedAt})
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
        Me.colRowNum.Name = "colRowNum"
        Me.colRowNum.UnboundDataType = GetType(Integer)
        '
        'colPermID
        '
        Me.colPermID.FieldName = "PermID"
        Me.colPermID.Name = "colPermID"
        '
        'colPermKey
        '
        Me.colPermKey.FieldName = "PermKey"
        Me.colPermKey.Name = "colPermKey"
        resources.ApplyResources(Me.colPermKey, "colPermKey")
        '
        'colPermName
        '
        Me.colPermName.FieldName = "PermName"
        Me.colPermName.Name = "colPermName"
        resources.ApplyResources(Me.colPermName, "colPermName")
        '
        'colPermDescription
        '
        Me.colPermDescription.FieldName = "PermDescription"
        Me.colPermDescription.Name = "colPermDescription"
        resources.ApplyResources(Me.colPermDescription, "colPermDescription")
        '
        'colCategory
        '
        Me.colCategory.FieldName = "Category"
        Me.colCategory.Name = "colCategory"
        resources.ApplyResources(Me.colCategory, "colCategory")
        '
        'colIsActive
        '
        Me.colIsActive.FieldName = "IsActive"
        Me.colIsActive.Name = "colIsActive"
        resources.ApplyResources(Me.colIsActive, "colIsActive")
        '
        'colCreatedAt
        '
        Me.colCreatedAt.FieldName = "CreatedAt"
        Me.colCreatedAt.Name = "colCreatedAt"
        '
        'btnAdd
        '
        Me.btnAdd.Appearance.Font = CType(resources.GetObject("btnAdd.Appearance.Font"), System.Drawing.Font)
        Me.btnAdd.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.btnAdd, "btnAdd")
        Me.btnAdd.Name = "btnAdd"
        '
        'btnEdit
        '
        Me.btnEdit.Appearance.Font = CType(resources.GetObject("btnEdit.Appearance.Font"), System.Drawing.Font)
        Me.btnEdit.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.btnEdit, "btnEdit")
        Me.btnEdit.Name = "btnEdit"
        '
        'btnDel
        '
        Me.btnDel.Appearance.Font = CType(resources.GetObject("btnDel.Appearance.Font"), System.Drawing.Font)
        Me.btnDel.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.btnDel, "btnDel")
        Me.btnDel.Name = "btnDel"
        '
        'PermIDSpinEdit
        '
        resources.ApplyResources(Me.PermIDSpinEdit, "PermIDSpinEdit")
        Me.PermIDSpinEdit.Name = "PermIDSpinEdit"
        Me.PermIDSpinEdit.Properties.Appearance.Font = CType(resources.GetObject("PermIDSpinEdit.Properties.Appearance.Font"), System.Drawing.Font)
        Me.PermIDSpinEdit.Properties.Appearance.Options.UseFont = True
        Me.PermIDSpinEdit.Properties.Appearance.Options.UseTextOptions = True
        Me.PermIDSpinEdit.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.PermIDSpinEdit.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        '
        'PermIDLabel
        '
        Me.PermIDLabel.Appearance.Font = CType(resources.GetObject("PermIDLabel.Appearance.Font"), System.Drawing.Font)
        Me.PermIDLabel.Appearance.Options.UseFont = True
        Me.PermIDLabel.Appearance.Options.UseTextOptions = True
        Me.PermIDLabel.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.PermIDLabel.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        resources.ApplyResources(Me.PermIDLabel, "PermIDLabel")
        Me.PermIDLabel.Name = "PermIDLabel"
        '
        'butClose
        '
        Me.butClose.Appearance.Font = CType(resources.GetObject("butClose.Appearance.Font"), System.Drawing.Font)
        Me.butClose.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.butClose, "butClose")
        Me.butClose.Name = "butClose"
        '
        'PermKeyTextEdit
        '
        resources.ApplyResources(Me.PermKeyTextEdit, "PermKeyTextEdit")
        Me.PermKeyTextEdit.Name = "PermKeyTextEdit"
        Me.PermKeyTextEdit.Properties.Appearance.Font = CType(resources.GetObject("PermKeyTextEdit.Properties.Appearance.Font"), System.Drawing.Font)
        Me.PermKeyTextEdit.Properties.Appearance.Options.UseFont = True
        Me.PermKeyTextEdit.Properties.Appearance.Options.UseTextOptions = True
        Me.PermKeyTextEdit.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.PermKeyTextEdit.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        '
        'PermNameTextEdit
        '
        resources.ApplyResources(Me.PermNameTextEdit, "PermNameTextEdit")
        Me.PermNameTextEdit.Name = "PermNameTextEdit"
        Me.PermNameTextEdit.Properties.Appearance.Font = CType(resources.GetObject("PermNameTextEdit.Properties.Appearance.Font"), System.Drawing.Font)
        Me.PermNameTextEdit.Properties.Appearance.Options.UseFont = True
        Me.PermNameTextEdit.Properties.Appearance.Options.UseTextOptions = True
        Me.PermNameTextEdit.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.PermNameTextEdit.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        '
        'IsActiveLabel
        '
        Me.IsActiveLabel.Appearance.Font = CType(resources.GetObject("IsActiveLabel.Appearance.Font"), System.Drawing.Font)
        Me.IsActiveLabel.Appearance.Options.UseFont = True
        Me.IsActiveLabel.Appearance.Options.UseTextOptions = True
        Me.IsActiveLabel.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.IsActiveLabel.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        resources.ApplyResources(Me.IsActiveLabel, "IsActiveLabel")
        Me.IsActiveLabel.Name = "IsActiveLabel"
        '
        'PermDescriptionTextEdit
        '
        resources.ApplyResources(Me.PermDescriptionTextEdit, "PermDescriptionTextEdit")
        Me.PermDescriptionTextEdit.Name = "PermDescriptionTextEdit"
        Me.PermDescriptionTextEdit.Properties.Appearance.Font = CType(resources.GetObject("PermDescriptionTextEdit.Properties.Appearance.Font"), System.Drawing.Font)
        Me.PermDescriptionTextEdit.Properties.Appearance.Options.UseFont = True
        Me.PermDescriptionTextEdit.Properties.Appearance.Options.UseTextOptions = True
        Me.PermDescriptionTextEdit.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.PermDescriptionTextEdit.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        '
        'PermKeyLabel
        '
        Me.PermKeyLabel.Appearance.Font = CType(resources.GetObject("PermKeyLabel.Appearance.Font"), System.Drawing.Font)
        Me.PermKeyLabel.Appearance.Options.UseFont = True
        Me.PermKeyLabel.Appearance.Options.UseTextOptions = True
        Me.PermKeyLabel.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.PermKeyLabel.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        resources.ApplyResources(Me.PermKeyLabel, "PermKeyLabel")
        Me.PermKeyLabel.Name = "PermKeyLabel"
        '
        'CategoryTextEdit
        '
        resources.ApplyResources(Me.CategoryTextEdit, "CategoryTextEdit")
        Me.CategoryTextEdit.Name = "CategoryTextEdit"
        Me.CategoryTextEdit.Properties.Appearance.Font = CType(resources.GetObject("CategoryTextEdit.Properties.Appearance.Font"), System.Drawing.Font)
        Me.CategoryTextEdit.Properties.Appearance.Options.UseFont = True
        Me.CategoryTextEdit.Properties.Appearance.Options.UseTextOptions = True
        Me.CategoryTextEdit.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.CategoryTextEdit.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        '
        'CreatedAtLabel
        '
        Me.CreatedAtLabel.Appearance.Font = CType(resources.GetObject("CreatedAtLabel.Appearance.Font"), System.Drawing.Font)
        Me.CreatedAtLabel.Appearance.Options.UseFont = True
        Me.CreatedAtLabel.Appearance.Options.UseTextOptions = True
        Me.CreatedAtLabel.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.CreatedAtLabel.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        resources.ApplyResources(Me.CreatedAtLabel, "CreatedAtLabel")
        Me.CreatedAtLabel.Name = "CreatedAtLabel"
        '
        'IsActiveCheckEdit
        '
        resources.ApplyResources(Me.IsActiveCheckEdit, "IsActiveCheckEdit")
        Me.IsActiveCheckEdit.Name = "IsActiveCheckEdit"
        Me.IsActiveCheckEdit.Properties.Appearance.Font = CType(resources.GetObject("IsActiveCheckEdit.Properties.Appearance.Font"), System.Drawing.Font)
        Me.IsActiveCheckEdit.Properties.Appearance.Options.UseFont = True
        Me.IsActiveCheckEdit.Properties.Appearance.Options.UseTextOptions = True
        Me.IsActiveCheckEdit.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.IsActiveCheckEdit.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.IsActiveCheckEdit.Properties.Caption = resources.GetString("IsActiveCheckEdit.Properties.Caption")
        '
        'CreatedAtDateEdit
        '
        resources.ApplyResources(Me.CreatedAtDateEdit, "CreatedAtDateEdit")
        Me.CreatedAtDateEdit.Name = "CreatedAtDateEdit"
        Me.CreatedAtDateEdit.Properties.Appearance.Font = CType(resources.GetObject("CreatedAtDateEdit.Properties.Appearance.Font"), System.Drawing.Font)
        Me.CreatedAtDateEdit.Properties.Appearance.Options.UseFont = True
        Me.CreatedAtDateEdit.Properties.Appearance.Options.UseTextOptions = True
        Me.CreatedAtDateEdit.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.CreatedAtDateEdit.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.CreatedAtDateEdit.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("CreatedAtDateEdit.Properties.CalendarTimeProperties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        '
        'CategoryLabel
        '
        Me.CategoryLabel.Appearance.Font = CType(resources.GetObject("CategoryLabel.Appearance.Font"), System.Drawing.Font)
        Me.CategoryLabel.Appearance.Options.UseFont = True
        Me.CategoryLabel.Appearance.Options.UseTextOptions = True
        Me.CategoryLabel.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.CategoryLabel.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        resources.ApplyResources(Me.CategoryLabel, "CategoryLabel")
        Me.CategoryLabel.Name = "CategoryLabel"
        '
        'PermNameLabel
        '
        Me.PermNameLabel.Appearance.Font = CType(resources.GetObject("PermNameLabel.Appearance.Font"), System.Drawing.Font)
        Me.PermNameLabel.Appearance.Options.UseFont = True
        Me.PermNameLabel.Appearance.Options.UseTextOptions = True
        Me.PermNameLabel.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.PermNameLabel.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        resources.ApplyResources(Me.PermNameLabel, "PermNameLabel")
        Me.PermNameLabel.Name = "PermNameLabel"
        '
        'PermDescriptionLabel
        '
        Me.PermDescriptionLabel.Appearance.Font = CType(resources.GetObject("PermDescriptionLabel.Appearance.Font"), System.Drawing.Font)
        Me.PermDescriptionLabel.Appearance.Options.UseFont = True
        Me.PermDescriptionLabel.Appearance.Options.UseTextOptions = True
        Me.PermDescriptionLabel.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.PermDescriptionLabel.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        resources.ApplyResources(Me.PermDescriptionLabel, "PermDescriptionLabel")
        Me.PermDescriptionLabel.Name = "PermDescriptionLabel"
        '
        'FrmPermissions
        '
        Me.Appearance.Options.UseFont = True
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.Splitter1)
        Me.Name = "FrmPermissions"
        CType(Me.Splitter1.Panel1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Splitter1.Panel1.ResumeLayout(False)
        CType(Me.Splitter1.Panel2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Splitter1.Panel2.ResumeLayout(False)
        CType(Me.Splitter1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Splitter1.ResumeLayout(False)
        CType(Me.DGV, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgView, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PermIDSpinEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PermKeyTextEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PermNameTextEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PermDescriptionTextEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CategoryTextEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IsActiveCheckEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CreatedAtDateEdit.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CreatedAtDateEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private WithEvents DGV As DevExpress.XtraGrid.GridControl
		Private WithEvents dgView As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents colRowNum As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents Splitter1 As DevExpress.XtraEditors.SplitContainerControl
    Friend WithEvents butClose As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents PermIDSpinEdit As DevExpress.XtraEditors.SpinEdit
		Friend WithEvents PermIDLabel As DevExpress.XtraEditors.LabelControl
		Friend WithEvents colPermID As DevExpress.XtraGrid.Columns.GridColumn
		Friend WithEvents PermKeyTextEdit As DevExpress.XtraEditors.TextEdit
		Friend WithEvents PermKeyLabel As DevExpress.XtraEditors.LabelControl
		Friend WithEvents colPermKey As DevExpress.XtraGrid.Columns.GridColumn
		Friend WithEvents PermNameTextEdit As DevExpress.XtraEditors.TextEdit
		Friend WithEvents PermNameLabel As DevExpress.XtraEditors.LabelControl
		Friend WithEvents colPermName As DevExpress.XtraGrid.Columns.GridColumn
		Friend WithEvents PermDescriptionTextEdit As DevExpress.XtraEditors.TextEdit
		Friend WithEvents PermDescriptionLabel As DevExpress.XtraEditors.LabelControl
		Friend WithEvents colPermDescription As DevExpress.XtraGrid.Columns.GridColumn
		Friend WithEvents CategoryTextEdit As DevExpress.XtraEditors.TextEdit
		Friend WithEvents CategoryLabel As DevExpress.XtraEditors.LabelControl
		Friend WithEvents colCategory As DevExpress.XtraGrid.Columns.GridColumn
		Friend WithEvents IsActiveCheckEdit As DevExpress.XtraEditors.CheckEdit
		Friend WithEvents IsActiveLabel As DevExpress.XtraEditors.LabelControl
		Friend WithEvents colIsActive As DevExpress.XtraGrid.Columns.GridColumn
		Friend WithEvents CreatedAtDateEdit As DevExpress.XtraEditors.DateEdit
		Friend WithEvents CreatedAtLabel As DevExpress.XtraEditors.LabelControl
		Friend WithEvents colCreatedAt As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents btnEdit As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnDel As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnAdd As DevExpress.XtraEditors.SimpleButton
End Class
