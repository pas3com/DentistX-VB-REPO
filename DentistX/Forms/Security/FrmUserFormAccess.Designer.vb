Imports DevExpress.XtraEditors

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Public Class FrmUserFormAccess
    Inherits XtraForm

    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    Private components As System.ComponentModel.IContainer

    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.lblHeading = New DevExpress.XtraEditors.LabelControl()
        Me.lblUser = New DevExpress.XtraEditors.LabelControl()
        Me.luUser = New DevExpress.XtraEditors.LookUpEdit()
        Me.lblSearch = New DevExpress.XtraEditors.LabelControl()
        Me.txtFilter = New DevExpress.XtraEditors.TextEdit()
        Me.lblGridCaption = New DevExpress.XtraEditors.LabelControl()
        Me.GridFormAccess = New DevExpress.XtraGrid.GridControl()
        Me.GridViewFormAccess = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.colFormName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colSrcRefs = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colTitle = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colTitleAr = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colDescription = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colAllowed = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.btnSave = New DevExpress.XtraEditors.SimpleButton()
        Me.btnRefresh = New DevExpress.XtraEditors.SimpleButton()
        Me.btnSyncForms = New DevExpress.XtraEditors.SimpleButton()
        Me.btnPruneObsolete = New DevExpress.XtraEditors.SimpleButton()
        Me.btnScanSourceRefs = New DevExpress.XtraEditors.SimpleButton()
        Me.btnGrantAll = New DevExpress.XtraEditors.SimpleButton()
        Me.btnDenyAll = New DevExpress.XtraEditors.SimpleButton()
        Me.btnLegacyKeys = New DevExpress.XtraEditors.SimpleButton()
        Me.btnClose = New DevExpress.XtraEditors.SimpleButton()
        Me.lblHint = New DevExpress.XtraEditors.LabelControl()
        CType(Me.luUser.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtFilter.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridFormAccess, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridViewFormAccess, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lblHeading
        '
        Me.lblHeading.Appearance.Font = New System.Drawing.Font("Calibri", 10.0!, System.Drawing.FontStyle.Bold)
        Me.lblHeading.Appearance.Options.UseFont = True
        Me.lblHeading.Location = New System.Drawing.Point(12, 12)
        Me.lblHeading.Name = "lblHeading"
        Me.lblHeading.Size = New System.Drawing.Size(446, 15)
        Me.lblHeading.TabIndex = 0
        Me.lblHeading.Text = "Assign which screens each user may open (non-admin users only need rows here)."
        '
        'lblUser
        '
        Me.lblUser.Appearance.Font = New System.Drawing.Font("Calibri", 10.0!, System.Drawing.FontStyle.Bold)
        Me.lblUser.Appearance.Options.UseFont = True
        Me.lblUser.Location = New System.Drawing.Point(12, 44)
        Me.lblUser.Name = "lblUser"
        Me.lblUser.Size = New System.Drawing.Size(29, 15)
        Me.lblUser.TabIndex = 1
        Me.lblUser.Text = "User:"
        '
        'luUser
        '
        Me.luUser.Location = New System.Drawing.Point(102, 41)
        Me.luUser.Name = "luUser"
        Me.luUser.Properties.Appearance.Font = New System.Drawing.Font("Calibri", 10.0!, System.Drawing.FontStyle.Bold)
        Me.luUser.Properties.Appearance.Options.UseFont = True
        Me.luUser.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.luUser.Properties.NullText = ""
        Me.luUser.Size = New System.Drawing.Size(420, 22)
        Me.luUser.TabIndex = 2
        '
        'lblSearch
        '
        Me.lblSearch.Appearance.Font = New System.Drawing.Font("Calibri", 10.0!, System.Drawing.FontStyle.Bold)
        Me.lblSearch.Appearance.Options.UseFont = True
        Me.lblSearch.Location = New System.Drawing.Point(12, 76)
        Me.lblSearch.Name = "lblSearch"
        Me.lblSearch.Size = New System.Drawing.Size(52, 15)
        Me.lblSearch.TabIndex = 3
        Me.lblSearch.Text = "Filter list:"
        '
        'txtFilter
        '
        Me.txtFilter.Location = New System.Drawing.Point(102, 73)
        Me.txtFilter.Name = "txtFilter"
        Me.txtFilter.Properties.Appearance.Font = New System.Drawing.Font("Calibri", 10.0!, System.Drawing.FontStyle.Bold)
        Me.txtFilter.Properties.Appearance.Options.UseFont = True
        Me.txtFilter.Size = New System.Drawing.Size(420, 22)
        Me.txtFilter.TabIndex = 4
        '
        'lblGridCaption
        '
        Me.lblGridCaption.Appearance.Font = New System.Drawing.Font("Calibri", 10.0!, System.Drawing.FontStyle.Bold)
        Me.lblGridCaption.Appearance.Options.UseFont = True
        Me.lblGridCaption.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.Vertical
        Me.lblGridCaption.Location = New System.Drawing.Point(12, 102)
        Me.lblGridCaption.Name = "lblGridCaption"
        Me.lblGridCaption.Size = New System.Drawing.Size(1244, 17)
        Me.lblGridCaption.TabIndex = 14
        Me.lblGridCaption.Text = "Step 2 / الخطوة 2 — Every row is one screen in the app. Tick Allow for the select" &
    "ed user only; then Save."
        '
        'GridFormAccess
        '
        Me.GridFormAccess.Location = New System.Drawing.Point(10, 134)
        Me.GridFormAccess.MainView = Me.GridViewFormAccess
        Me.GridFormAccess.Name = "GridFormAccess"
        Me.GridFormAccess.Size = New System.Drawing.Size(1244, 388)
        Me.GridFormAccess.TabIndex = 5
        Me.GridFormAccess.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridViewFormAccess})
        '
        'GridViewFormAccess
        '
        Me.GridViewFormAccess.Appearance.HeaderPanel.Font = New System.Drawing.Font("Calibri", 10.0!, System.Drawing.FontStyle.Bold)
        Me.GridViewFormAccess.Appearance.HeaderPanel.Options.UseFont = True
        Me.GridViewFormAccess.Appearance.Row.Font = New System.Drawing.Font("Calibri", 10.0!, System.Drawing.FontStyle.Bold)
        Me.GridViewFormAccess.Appearance.Row.Options.UseFont = True
        Me.GridViewFormAccess.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.colFormName, Me.colSrcRefs, Me.colTitle, Me.colTitleAr, Me.colDescription, Me.colAllowed})
        Me.GridViewFormAccess.GridControl = Me.GridFormAccess
        Me.GridViewFormAccess.Name = "GridViewFormAccess"
        Me.GridViewFormAccess.OptionsView.ColumnAutoWidth = False
        Me.GridViewFormAccess.OptionsView.ShowGroupPanel = False
        '
        'colFormName
        '
        Me.colFormName.Caption = "Form name"
        Me.colFormName.FieldName = "FormName"
        Me.colFormName.Name = "colFormName"
        Me.colFormName.OptionsColumn.AllowEdit = False
        Me.colFormName.Visible = True
        Me.colFormName.VisibleIndex = 0
        Me.colFormName.Width = 199
        '
        'colSrcRefs
        '
        Me.colSrcRefs.Caption = "Src hits"
        Me.colSrcRefs.FieldName = "SrcRefHits"
        Me.colSrcRefs.Name = "colSrcRefs"
        Me.colSrcRefs.OptionsColumn.AllowEdit = False
        Me.colSrcRefs.Visible = True
        Me.colSrcRefs.VisibleIndex = 1
        Me.colSrcRefs.Width = 51
        '
        'colTitle
        '
        Me.colTitle.Caption = "Title"
        Me.colTitle.FieldName = "Title"
        Me.colTitle.Name = "colTitle"
        Me.colTitle.Visible = True
        Me.colTitle.VisibleIndex = 2
        Me.colTitle.Width = 229
        '
        'colTitleAr
        '
        Me.colTitleAr.Caption = "Title (Arabic)"
        Me.colTitleAr.FieldName = "TitleAr"
        Me.colTitleAr.Name = "colTitleAr"
        Me.colTitleAr.Visible = True
        Me.colTitleAr.VisibleIndex = 3
        Me.colTitleAr.Width = 246
        '
        'colDescription
        '
        Me.colDescription.Caption = "Description"
        Me.colDescription.FieldName = "Description"
        Me.colDescription.Name = "colDescription"
        Me.colDescription.OptionsColumn.AllowEdit = False
        Me.colDescription.Visible = True
        Me.colDescription.VisibleIndex = 4
        Me.colDescription.Width = 413
        '
        'colAllowed
        '
        Me.colAllowed.Caption = "Allow (this user)"
        Me.colAllowed.FieldName = "IsAllowed"
        Me.colAllowed.Name = "colAllowed"
        Me.colAllowed.Visible = True
        Me.colAllowed.VisibleIndex = 5
        Me.colAllowed.Width = 80
        '
        'btnSave
        '
        Me.btnSave.Appearance.Font = New System.Drawing.Font("Calibri", 10.0!, System.Drawing.FontStyle.Bold)
        Me.btnSave.Appearance.Options.UseFont = True
        Me.btnSave.Location = New System.Drawing.Point(10, 545)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(108, 32)
        Me.btnSave.TabIndex = 6
        Me.btnSave.Text = "Save"
        '
        'btnRefresh
        '
        Me.btnRefresh.Appearance.Font = New System.Drawing.Font("Calibri", 10.0!, System.Drawing.FontStyle.Bold)
        Me.btnRefresh.Appearance.Options.UseFont = True
        Me.btnRefresh.Location = New System.Drawing.Point(126, 545)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(108, 32)
        Me.btnRefresh.TabIndex = 7
        Me.btnRefresh.Text = "Reload"
        '
        'btnSyncForms
        '
        Me.btnSyncForms.Appearance.Font = New System.Drawing.Font("Calibri", 10.0!, System.Drawing.FontStyle.Bold)
        Me.btnSyncForms.Appearance.Options.UseFont = True
        Me.btnSyncForms.Location = New System.Drawing.Point(242, 545)
        Me.btnSyncForms.Name = "btnSyncForms"
        Me.btnSyncForms.Size = New System.Drawing.Size(130, 32)
        Me.btnSyncForms.TabIndex = 8
        Me.btnSyncForms.Text = "Sync forms from app"
        '
        'btnPruneObsolete
        '
        Me.btnPruneObsolete.Appearance.Font = New System.Drawing.Font("Calibri", 10.0!, System.Drawing.FontStyle.Bold)
        Me.btnPruneObsolete.Appearance.Options.UseFont = True
        Me.btnPruneObsolete.Location = New System.Drawing.Point(380, 545)
        Me.btnPruneObsolete.Name = "btnPruneObsolete"
        Me.btnPruneObsolete.Size = New System.Drawing.Size(138, 32)
        Me.btnPruneObsolete.TabIndex = 15
        Me.btnPruneObsolete.Text = "Remove obsolete forms"
        '
        'btnScanSourceRefs
        '
        Me.btnScanSourceRefs.Appearance.Font = New System.Drawing.Font("Calibri", 10.0!, System.Drawing.FontStyle.Bold)
        Me.btnScanSourceRefs.Appearance.Options.UseFont = True
        Me.btnScanSourceRefs.Location = New System.Drawing.Point(526, 545)
        Me.btnScanSourceRefs.Name = "btnScanSourceRefs"
        Me.btnScanSourceRefs.Size = New System.Drawing.Size(128, 32)
        Me.btnScanSourceRefs.TabIndex = 16
        Me.btnScanSourceRefs.Text = "Scan source hits"
        '
        'btnGrantAll
        '
        Me.btnGrantAll.Appearance.Font = New System.Drawing.Font("Calibri", 10.0!, System.Drawing.FontStyle.Bold)
        Me.btnGrantAll.Appearance.Options.UseFont = True
        Me.btnGrantAll.Location = New System.Drawing.Point(662, 545)
        Me.btnGrantAll.Name = "btnGrantAll"
        Me.btnGrantAll.Size = New System.Drawing.Size(88, 32)
        Me.btnGrantAll.TabIndex = 9
        Me.btnGrantAll.Text = "Allow all"
        '
        'btnDenyAll
        '
        Me.btnDenyAll.Appearance.Font = New System.Drawing.Font("Calibri", 10.0!, System.Drawing.FontStyle.Bold)
        Me.btnDenyAll.Appearance.Options.UseFont = True
        Me.btnDenyAll.Location = New System.Drawing.Point(758, 545)
        Me.btnDenyAll.Name = "btnDenyAll"
        Me.btnDenyAll.Size = New System.Drawing.Size(88, 32)
        Me.btnDenyAll.TabIndex = 10
        Me.btnDenyAll.Text = "Deny all"
        '
        'btnLegacyKeys
        '
        Me.btnLegacyKeys.Appearance.Font = New System.Drawing.Font("Calibri", 10.0!, System.Drawing.FontStyle.Bold)
        Me.btnLegacyKeys.Appearance.Options.UseFont = True
        Me.btnLegacyKeys.Location = New System.Drawing.Point(854, 545)
        Me.btnLegacyKeys.Name = "btnLegacyKeys"
        Me.btnLegacyKeys.Size = New System.Drawing.Size(148, 32)
        Me.btnLegacyKeys.TabIndex = 11
        Me.btnLegacyKeys.Text = "Legacy permission keys"
        '
        'btnClose
        '
        Me.btnClose.Appearance.Font = New System.Drawing.Font("Calibri", 10.0!, System.Drawing.FontStyle.Bold)
        Me.btnClose.Appearance.Options.UseFont = True
        Me.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnClose.Location = New System.Drawing.Point(1103, 545)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(72, 32)
        Me.btnClose.TabIndex = 12
        Me.btnClose.Text = "Close"
        '
        'lblHint
        '
        Me.lblHint.Appearance.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Italic)
        Me.lblHint.Appearance.ForeColor = System.Drawing.Color.DimGray
        Me.lblHint.Appearance.Options.UseFont = True
        Me.lblHint.Appearance.Options.UseForeColor = True
        Me.lblHint.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.Vertical
        Me.lblHint.Location = New System.Drawing.Point(540, 39)
        Me.lblHint.Name = "lblHint"
        Me.lblHint.Size = New System.Drawing.Size(408, 28)
        Me.lblHint.TabIndex = 13
        Me.lblHint.Text = "Users in the ADMINS group always see everything. Run Sync once so the grid lists " &
    "your forms; then tick Allow and Save."
        '
        'FrmUserFormAccess
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1268, 589)
        Me.Controls.Add(Me.lblHint)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.btnLegacyKeys)
        Me.Controls.Add(Me.btnDenyAll)
        Me.Controls.Add(Me.btnGrantAll)
        Me.Controls.Add(Me.btnSyncForms)
        Me.Controls.Add(Me.btnPruneObsolete)
        Me.Controls.Add(Me.btnScanSourceRefs)
        Me.Controls.Add(Me.btnRefresh)
        Me.Controls.Add(Me.btnSave)
        Me.Controls.Add(Me.GridFormAccess)
        Me.Controls.Add(Me.lblGridCaption)
        Me.Controls.Add(Me.txtFilter)
        Me.Controls.Add(Me.lblSearch)
        Me.Controls.Add(Me.luUser)
        Me.Controls.Add(Me.lblUser)
        Me.Controls.Add(Me.lblHeading)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmUserFormAccess"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Form access by user"
        CType(Me.luUser.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtFilter.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridFormAccess, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridViewFormAccess, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents lblHeading As LabelControl
    Friend WithEvents lblUser As LabelControl
    Friend WithEvents luUser As LookUpEdit
    Friend WithEvents lblSearch As LabelControl
    Friend WithEvents txtFilter As TextEdit
    Friend WithEvents GridFormAccess As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridViewFormAccess As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents colFormName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colSrcRefs As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colTitle As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colTitleAr As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colDescription As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colAllowed As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents btnSave As SimpleButton
    Friend WithEvents btnRefresh As SimpleButton
    Friend WithEvents btnSyncForms As SimpleButton
    Friend WithEvents btnPruneObsolete As SimpleButton
    Friend WithEvents btnScanSourceRefs As SimpleButton
    Friend WithEvents btnGrantAll As SimpleButton
    Friend WithEvents btnDenyAll As SimpleButton
    Friend WithEvents btnLegacyKeys As SimpleButton
    Friend WithEvents btnClose As SimpleButton
    Friend WithEvents lblHint As LabelControl
    Friend WithEvents lblGridCaption As LabelControl
End Class
