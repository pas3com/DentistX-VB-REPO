<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmPermissionManager
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmPermissionManager))
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.CboGroup = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.BtnSavePermissions = New DevExpress.XtraEditors.SimpleButton()
        Me.GridControl1 = New DevExpress.XtraGrid.GridControl()
        Me.GridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.formNameCol = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GroupNameCol = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CanViewCol = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CanEditCol = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CanDeleteCol = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GroupIDCol = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FormIDCol = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.btnCancel = New DevExpress.XtraEditors.SimpleButton()
        Me.CboForms = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.CboUser = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.chkCanView = New DevExpress.XtraEditors.CheckEdit()
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl3 = New DevExpress.XtraEditors.LabelControl()
        Me.chkCanEdit = New DevExpress.XtraEditors.CheckEdit()
        Me.chkCanDelete = New DevExpress.XtraEditors.CheckEdit()
        Me.BtnDelPermission = New DevExpress.XtraEditors.SimpleButton()
        Me.BtnEditPermission = New DevExpress.XtraEditors.SimpleButton()
        CType(Me.CboGroup.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CboForms.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CboUser.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkCanView.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkCanEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkCanDelete.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'LabelControl1
        '
        Me.LabelControl1.Appearance.Font = CType(resources.GetObject("LabelControl1.Appearance.Font"), System.Drawing.Font)
        Me.LabelControl1.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.LabelControl1, "LabelControl1")
        Me.LabelControl1.Name = "LabelControl1"
        '
        'CboGroup
        '
        resources.ApplyResources(Me.CboGroup, "CboGroup")
        Me.CboGroup.Name = "CboGroup"
        Me.CboGroup.Properties.Appearance.Font = CType(resources.GetObject("CboGroup.Properties.Appearance.Font"), System.Drawing.Font)
        Me.CboGroup.Properties.Appearance.Options.UseFont = True
        Me.CboGroup.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("CboGroup.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        '
        'BtnSavePermissions
        '
        Me.BtnSavePermissions.Appearance.Font = CType(resources.GetObject("BtnSavePermissions.Appearance.Font"), System.Drawing.Font)
        Me.BtnSavePermissions.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.BtnSavePermissions, "BtnSavePermissions")
        Me.BtnSavePermissions.Name = "BtnSavePermissions"
        '
        'GridControl1
        '
        Me.GridControl1.EmbeddedNavigator.Margin = CType(resources.GetObject("GridControl1.EmbeddedNavigator.Margin"), System.Windows.Forms.Padding)
        resources.ApplyResources(Me.GridControl1, "GridControl1")
        Me.GridControl1.MainView = Me.GridView1
        Me.GridControl1.Name = "GridControl1"
        Me.GridControl1.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridView1})
        '
        'GridView1
        '
        Me.GridView1.Appearance.HeaderPanel.Font = CType(resources.GetObject("GridView1.Appearance.HeaderPanel.Font"), System.Drawing.Font)
        Me.GridView1.Appearance.HeaderPanel.Options.UseFont = True
        Me.GridView1.Appearance.Row.Font = CType(resources.GetObject("GridView1.Appearance.Row.Font"), System.Drawing.Font)
        Me.GridView1.Appearance.Row.Options.UseFont = True
        Me.GridView1.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.formNameCol, Me.GroupNameCol, Me.CanViewCol, Me.CanEditCol, Me.CanDeleteCol, Me.GroupIDCol, Me.FormIDCol})
        Me.GridView1.DetailHeight = 404
        Me.GridView1.GridControl = Me.GridControl1
        Me.GridView1.Name = "GridView1"
        Me.GridView1.OptionsEditForm.PopupEditFormWidth = 933
        '
        'formNameCol
        '
        resources.ApplyResources(Me.formNameCol, "formNameCol")
        Me.formNameCol.FieldName = "FormName"
        Me.formNameCol.MinWidth = 23
        Me.formNameCol.Name = "formNameCol"
        Me.formNameCol.UnboundDataType = GetType(String)
        '
        'GroupNameCol
        '
        resources.ApplyResources(Me.GroupNameCol, "GroupNameCol")
        Me.GroupNameCol.FieldName = "GroupName"
        Me.GroupNameCol.MinWidth = 23
        Me.GroupNameCol.Name = "GroupNameCol"
        Me.GroupNameCol.UnboundDataType = GetType(String)
        '
        'CanViewCol
        '
        resources.ApplyResources(Me.CanViewCol, "CanViewCol")
        Me.CanViewCol.FieldName = "CanView"
        Me.CanViewCol.MinWidth = 23
        Me.CanViewCol.Name = "CanViewCol"
        Me.CanViewCol.UnboundDataType = GetType(Boolean)
        '
        'CanEditCol
        '
        resources.ApplyResources(Me.CanEditCol, "CanEditCol")
        Me.CanEditCol.FieldName = "CanEdit"
        Me.CanEditCol.MinWidth = 23
        Me.CanEditCol.Name = "CanEditCol"
        Me.CanEditCol.UnboundDataType = GetType(Boolean)
        '
        'CanDeleteCol
        '
        resources.ApplyResources(Me.CanDeleteCol, "CanDeleteCol")
        Me.CanDeleteCol.FieldName = "CanDelete"
        Me.CanDeleteCol.MinWidth = 23
        Me.CanDeleteCol.Name = "CanDeleteCol"
        Me.CanDeleteCol.UnboundDataType = GetType(Boolean)
        '
        'GroupIDCol
        '
        resources.ApplyResources(Me.GroupIDCol, "GroupIDCol")
        Me.GroupIDCol.FieldName = "GroupID"
        Me.GroupIDCol.MinWidth = 23
        Me.GroupIDCol.Name = "GroupIDCol"
        Me.GroupIDCol.UnboundDataType = GetType(Integer)
        '
        'FormIDCol
        '
        resources.ApplyResources(Me.FormIDCol, "FormIDCol")
        Me.FormIDCol.FieldName = "FormID"
        Me.FormIDCol.MinWidth = 23
        Me.FormIDCol.Name = "FormIDCol"
        Me.FormIDCol.UnboundDataType = GetType(Integer)
        '
        'btnCancel
        '
        Me.btnCancel.Appearance.Font = CType(resources.GetObject("btnCancel.Appearance.Font"), System.Drawing.Font)
        Me.btnCancel.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.btnCancel, "btnCancel")
        Me.btnCancel.Name = "btnCancel"
        '
        'CboForms
        '
        resources.ApplyResources(Me.CboForms, "CboForms")
        Me.CboForms.Name = "CboForms"
        Me.CboForms.Properties.Appearance.Font = CType(resources.GetObject("CboForms.Properties.Appearance.Font"), System.Drawing.Font)
        Me.CboForms.Properties.Appearance.Options.UseFont = True
        Me.CboForms.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("CboForms.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        '
        'CboUser
        '
        resources.ApplyResources(Me.CboUser, "CboUser")
        Me.CboUser.Name = "CboUser"
        Me.CboUser.Properties.Appearance.Font = CType(resources.GetObject("CboUser.Properties.Appearance.Font"), System.Drawing.Font)
        Me.CboUser.Properties.Appearance.Options.UseFont = True
        Me.CboUser.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("CboUser.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        '
        'chkCanView
        '
        resources.ApplyResources(Me.chkCanView, "chkCanView")
        Me.chkCanView.Name = "chkCanView"
        Me.chkCanView.Properties.Appearance.Font = CType(resources.GetObject("chkCanView.Properties.Appearance.Font"), System.Drawing.Font)
        Me.chkCanView.Properties.Appearance.Options.UseFont = True
        Me.chkCanView.Properties.Caption = resources.GetString("chkCanView.Properties.Caption")
        '
        'LabelControl2
        '
        Me.LabelControl2.Appearance.Font = CType(resources.GetObject("LabelControl2.Appearance.Font"), System.Drawing.Font)
        Me.LabelControl2.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.LabelControl2, "LabelControl2")
        Me.LabelControl2.Name = "LabelControl2"
        '
        'LabelControl3
        '
        Me.LabelControl3.Appearance.Font = CType(resources.GetObject("LabelControl3.Appearance.Font"), System.Drawing.Font)
        Me.LabelControl3.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.LabelControl3, "LabelControl3")
        Me.LabelControl3.Name = "LabelControl3"
        '
        'chkCanEdit
        '
        resources.ApplyResources(Me.chkCanEdit, "chkCanEdit")
        Me.chkCanEdit.Name = "chkCanEdit"
        Me.chkCanEdit.Properties.Appearance.Font = CType(resources.GetObject("chkCanEdit.Properties.Appearance.Font"), System.Drawing.Font)
        Me.chkCanEdit.Properties.Appearance.Options.UseFont = True
        Me.chkCanEdit.Properties.Caption = resources.GetString("chkCanEdit.Properties.Caption")
        '
        'chkCanDelete
        '
        resources.ApplyResources(Me.chkCanDelete, "chkCanDelete")
        Me.chkCanDelete.Name = "chkCanDelete"
        Me.chkCanDelete.Properties.Appearance.Font = CType(resources.GetObject("chkCanDelete.Properties.Appearance.Font"), System.Drawing.Font)
        Me.chkCanDelete.Properties.Appearance.Options.UseFont = True
        Me.chkCanDelete.Properties.Caption = resources.GetString("chkCanDelete.Properties.Caption")
        '
        'BtnDelPermission
        '
        Me.BtnDelPermission.Appearance.Font = CType(resources.GetObject("BtnDelPermission.Appearance.Font"), System.Drawing.Font)
        Me.BtnDelPermission.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.BtnDelPermission, "BtnDelPermission")
        Me.BtnDelPermission.Name = "BtnDelPermission"
        '
        'BtnEditPermission
        '
        Me.BtnEditPermission.Appearance.Font = CType(resources.GetObject("BtnEditPermission.Appearance.Font"), System.Drawing.Font)
        Me.BtnEditPermission.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.BtnEditPermission, "BtnEditPermission")
        Me.BtnEditPermission.Name = "BtnEditPermission"
        '
        'FrmPermissionManager
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.BtnDelPermission)
        Me.Controls.Add(Me.BtnEditPermission)
        Me.Controls.Add(Me.chkCanDelete)
        Me.Controls.Add(Me.chkCanEdit)
        Me.Controls.Add(Me.chkCanView)
        Me.Controls.Add(Me.CboUser)
        Me.Controls.Add(Me.CboForms)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.GridControl1)
        Me.Controls.Add(Me.BtnSavePermissions)
        Me.Controls.Add(Me.CboGroup)
        Me.Controls.Add(Me.LabelControl3)
        Me.Controls.Add(Me.LabelControl2)
        Me.Controls.Add(Me.LabelControl1)
        Me.Name = "FrmPermissionManager"
        CType(Me.CboGroup.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CboForms.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CboUser.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkCanView.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkCanEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkCanDelete.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents CboGroup As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents BtnSavePermissions As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents GridControl1 As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridView1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents formNameCol As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CanViewCol As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents btnCancel As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents CboForms As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents CboUser As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents chkCanView As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl3 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents chkCanEdit As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents chkCanDelete As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents GroupNameCol As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CanEditCol As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CanDeleteCol As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GroupIDCol As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FormIDCol As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents BtnDelPermission As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents BtnEditPermission As DevExpress.XtraEditors.SimpleButton
End Class
