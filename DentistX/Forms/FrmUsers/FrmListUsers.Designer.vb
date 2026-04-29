<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmListUsers
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmListUsers))
        Me.BtnDelete = New DevExpress.XtraEditors.SimpleButton()
        Me.BtnEdit = New DevExpress.XtraEditors.SimpleButton()
        Me.BtnAdd = New DevExpress.XtraEditors.SimpleButton()
        Me.GridControl1 = New DevExpress.XtraGrid.GridControl()
        Me.GridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.UsIDCol = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.UsNameCol = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.UsLvlCol = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.UsGrpCol = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.DrIdCol = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.SecIdCol = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.EmpIdCol = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.BtnResetPassword = New DevExpress.XtraEditors.SimpleButton()
        Me.BtnCancel = New DevExpress.XtraEditors.SimpleButton()
        CType(Me.GridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'BtnDelete
        '
        resources.ApplyResources(Me.BtnDelete, "BtnDelete")
        Me.BtnDelete.Appearance.Font = CType(resources.GetObject("BtnDelete.Appearance.Font"), System.Drawing.Font)
        Me.BtnDelete.Appearance.Options.UseFont = True
        Me.BtnDelete.ImageOptions.ImageKey = resources.GetString("BtnDelete.ImageOptions.ImageKey")
        Me.BtnDelete.Name = "BtnDelete"
        '
        'BtnEdit
        '
        resources.ApplyResources(Me.BtnEdit, "BtnEdit")
        Me.BtnEdit.Appearance.Font = CType(resources.GetObject("BtnEdit.Appearance.Font"), System.Drawing.Font)
        Me.BtnEdit.Appearance.Options.UseFont = True
        Me.BtnEdit.ImageOptions.ImageKey = resources.GetString("BtnEdit.ImageOptions.ImageKey")
        Me.BtnEdit.Name = "BtnEdit"
        '
        'BtnAdd
        '
        resources.ApplyResources(Me.BtnAdd, "BtnAdd")
        Me.BtnAdd.Appearance.Font = CType(resources.GetObject("BtnAdd.Appearance.Font"), System.Drawing.Font)
        Me.BtnAdd.Appearance.Options.UseFont = True
        Me.BtnAdd.ImageOptions.ImageKey = resources.GetString("BtnAdd.ImageOptions.ImageKey")
        Me.BtnAdd.Name = "BtnAdd"
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
        Me.GridView1.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.UsIDCol, Me.UsNameCol, Me.UsLvlCol, Me.UsGrpCol, Me.DrIdCol, Me.SecIdCol, Me.EmpIdCol})
        Me.GridView1.DetailHeight = 404
        Me.GridView1.GridControl = Me.GridControl1
        Me.GridView1.Name = "GridView1"
        Me.GridView1.OptionsEditForm.PopupEditFormWidth = 933
        '
        'UsIDCol
        '
        resources.ApplyResources(Me.UsIDCol, "UsIDCol")
        Me.UsIDCol.FieldName = "UsID"
        Me.UsIDCol.ImageOptions.ImageKey = resources.GetString("UsIDCol.ImageOptions.ImageKey")
        Me.UsIDCol.MinWidth = 23
        Me.UsIDCol.Name = "UsIDCol"
        Me.UsIDCol.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem()})
        '
        'UsNameCol
        '
        resources.ApplyResources(Me.UsNameCol, "UsNameCol")
        Me.UsNameCol.FieldName = "UsName"
        Me.UsNameCol.ImageOptions.ImageKey = resources.GetString("UsNameCol.ImageOptions.ImageKey")
        Me.UsNameCol.MinWidth = 23
        Me.UsNameCol.Name = "UsNameCol"
        '
        'UsLvlCol
        '
        resources.ApplyResources(Me.UsLvlCol, "UsLvlCol")
        Me.UsLvlCol.FieldName = "UsLvl"
        Me.UsLvlCol.ImageOptions.ImageKey = resources.GetString("UsLvlCol.ImageOptions.ImageKey")
        Me.UsLvlCol.MinWidth = 23
        Me.UsLvlCol.Name = "UsLvlCol"
        '
        'UsGrpCol
        '
        resources.ApplyResources(Me.UsGrpCol, "UsGrpCol")
        Me.UsGrpCol.FieldName = "UsGrp"
        Me.UsGrpCol.ImageOptions.ImageKey = resources.GetString("UsGrpCol.ImageOptions.ImageKey")
        Me.UsGrpCol.MinWidth = 23
        Me.UsGrpCol.Name = "UsGrpCol"
        '
        'DrIdCol
        '
        resources.ApplyResources(Me.DrIdCol, "DrIdCol")
        Me.DrIdCol.FieldName = "DrID"
        Me.DrIdCol.ImageOptions.ImageKey = resources.GetString("DrIdCol.ImageOptions.ImageKey")
        Me.DrIdCol.Name = "DrIdCol"
        '
        'SecIdCol
        '
        resources.ApplyResources(Me.SecIdCol, "SecIdCol")
        Me.SecIdCol.FieldName = "SecID"
        Me.SecIdCol.ImageOptions.ImageKey = resources.GetString("SecIdCol.ImageOptions.ImageKey")
        Me.SecIdCol.Name = "SecIdCol"
        Me.SecIdCol.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem()})
        '
        'EmpIdCol
        '
        resources.ApplyResources(Me.EmpIdCol, "EmpIdCol")
        Me.EmpIdCol.FieldName = "EmpID"
        Me.EmpIdCol.ImageOptions.ImageKey = resources.GetString("EmpIdCol.ImageOptions.ImageKey")
        Me.EmpIdCol.Name = "EmpIdCol"
        Me.EmpIdCol.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem()})
        '
        'BtnResetPassword
        '
        resources.ApplyResources(Me.BtnResetPassword, "BtnResetPassword")
        Me.BtnResetPassword.Appearance.Font = CType(resources.GetObject("BtnResetPassword.Appearance.Font"), System.Drawing.Font)
        Me.BtnResetPassword.Appearance.Options.UseFont = True
        Me.BtnResetPassword.ImageOptions.ImageKey = resources.GetString("BtnResetPassword.ImageOptions.ImageKey")
        Me.BtnResetPassword.Name = "BtnResetPassword"
        '
        'BtnCancel
        '
        resources.ApplyResources(Me.BtnCancel, "BtnCancel")
        Me.BtnCancel.Appearance.Font = CType(resources.GetObject("BtnCancel.Appearance.Font"), System.Drawing.Font)
        Me.BtnCancel.Appearance.Options.UseFont = True
        Me.BtnCancel.ImageOptions.ImageKey = resources.GetString("BtnCancel.ImageOptions.ImageKey")
        Me.BtnCancel.Name = "BtnCancel"
        '
        'FrmListUsers
        '
        resources.ApplyResources(Me, "$this")
        Me.Appearance.Options.UseFont = True
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.BtnResetPassword)
        Me.Controls.Add(Me.GridControl1)
        Me.Controls.Add(Me.BtnCancel)
        Me.Controls.Add(Me.BtnDelete)
        Me.Controls.Add(Me.BtnEdit)
        Me.Controls.Add(Me.BtnAdd)
        Me.Name = "FrmListUsers"
        CType(Me.GridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents BtnDelete As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents BtnEdit As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents BtnAdd As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents GridControl1 As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridView1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents UsIDCol As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents UsNameCol As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents UsLvlCol As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents UsGrpCol As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents BtnResetPassword As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents BtnCancel As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents DrIdCol As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents SecIdCol As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents EmpIdCol As DevExpress.XtraGrid.Columns.GridColumn
End Class
