<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class SecretariesCombo
    Inherits DevExpress.XtraEditors.XtraUserControl

    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    Private components As System.ComponentModel.IContainer

    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.PanelControl1 = New DevExpress.XtraEditors.PanelControl()
        Me.BtnAddSecretaries = New DevExpress.XtraEditors.SimpleButton()
        Me.btnSerach = New DevExpress.XtraEditors.SimpleButton()
        Me.PanelControl2 = New DevExpress.XtraEditors.PanelControl()
        Me.Flyout1 = New DevExpress.Utils.FlyoutPanel()
        Me.FlyoutPanelControl1 = New DevExpress.Utils.FlyoutPanelControl()
        Me.txtSearch = New DevExpress.XtraEditors.TextEdit()
        Me.CboSecretaries = New DevExpress.XtraEditors.ComboBoxEdit()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl1.SuspendLayout()
        CType(Me.PanelControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl2.SuspendLayout()
        CType(Me.Flyout1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Flyout1.SuspendLayout()
        CType(Me.FlyoutPanelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.FlyoutPanelControl1.SuspendLayout()
        CType(Me.txtSearch.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CboSecretaries.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'PanelControl1
        '
        Me.PanelControl1.Controls.Add(Me.BtnAddSecretaries)
        Me.PanelControl1.Controls.Add(Me.btnSerach)
        Me.PanelControl1.Dock = System.Windows.Forms.DockStyle.Right
        Me.PanelControl1.Location = New System.Drawing.Point(130, 0)
        Me.PanelControl1.Name = "PanelControl1"
        Me.PanelControl1.Size = New System.Drawing.Size(50, 25)
        Me.PanelControl1.TabIndex = 0
        '
        'BtnAddSecretaries
        '
        Me.BtnAddSecretaries.ImageOptions.Image = Global.DentistX.My.Resources.Resources.add_16x16
        Me.BtnAddSecretaries.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleCenter
        Me.BtnAddSecretaries.Location = New System.Drawing.Point(25, 0)
        Me.BtnAddSecretaries.Name = "BtnAddSecretaries"
        Me.BtnAddSecretaries.Size = New System.Drawing.Size(25, 25)
        Me.BtnAddSecretaries.TabIndex = 1
        Me.BtnAddSecretaries.ToolTip = "Add"
        '
        'btnSerach
        '
        Me.btnSerach.ImageOptions.Image = Global.DentistX.My.Resources.Resources.zoom_16x16
        Me.btnSerach.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleCenter
        Me.btnSerach.Location = New System.Drawing.Point(0, 0)
        Me.btnSerach.Name = "btnSerach"
        Me.btnSerach.Size = New System.Drawing.Size(25, 25)
        Me.btnSerach.TabIndex = 1
        Me.btnSerach.ToolTip = "Search"
        '
        'PanelControl2
        '
        Me.PanelControl2.Controls.Add(Me.Flyout1)
        Me.PanelControl2.Controls.Add(Me.CboSecretaries)
        Me.PanelControl2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelControl2.Location = New System.Drawing.Point(0, 0)
        Me.PanelControl2.Name = "PanelControl2"
        Me.PanelControl2.Size = New System.Drawing.Size(130, 25)
        Me.PanelControl2.TabIndex = 1
        '
        'Flyout1
        '
        Me.Flyout1.Controls.Add(Me.FlyoutPanelControl1)
        Me.Flyout1.Location = New System.Drawing.Point(0, 24)
        Me.Flyout1.Name = "Flyout1"
        Me.Flyout1.Options.AnchorType = DevExpress.Utils.Win.PopupToolWindowAnchor.Manual
        Me.Flyout1.Options.CloseOnOuterClick = True
        Me.Flyout1.OwnerControl = Me.PanelControl2
        Me.Flyout1.Size = New System.Drawing.Size(150, 26)
        Me.Flyout1.TabIndex = 1
        '
        'FlyoutPanelControl1
        '
        Me.FlyoutPanelControl1.Controls.Add(Me.txtSearch)
        Me.FlyoutPanelControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.FlyoutPanelControl1.FlyoutPanel = Me.Flyout1
        Me.FlyoutPanelControl1.Location = New System.Drawing.Point(0, 0)
        Me.FlyoutPanelControl1.Name = "FlyoutPanelControl1"
        Me.FlyoutPanelControl1.Size = New System.Drawing.Size(150, 26)
        Me.FlyoutPanelControl1.TabIndex = 0
        '
        'txtSearch
        '
        Me.txtSearch.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtSearch.Location = New System.Drawing.Point(2, 2)
        Me.txtSearch.Name = "txtSearch"
        Me.txtSearch.Properties.AutoHeight = False
        Me.txtSearch.Size = New System.Drawing.Size(146, 22)
        Me.txtSearch.TabIndex = 0
        '
        'CboSecretaries
        '
        Me.CboSecretaries.Dock = System.Windows.Forms.DockStyle.Fill
        Me.CboSecretaries.Location = New System.Drawing.Point(2, 2)
        Me.CboSecretaries.Name = "CboSecretaries"
        Me.CboSecretaries.Properties.Appearance.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CboSecretaries.Properties.Appearance.Options.UseFont = True
        Me.CboSecretaries.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.CboSecretaries.Properties.UseAdvancedMode = DevExpress.Utils.DefaultBoolean.[True]
        Me.CboSecretaries.Size = New System.Drawing.Size(126, 22)
        Me.CboSecretaries.TabIndex = 0
        '
        'SecretariesCombo
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.PanelControl2)
        Me.Controls.Add(Me.PanelControl1)
        Me.Name = "SecretariesCombo"
        Me.Size = New System.Drawing.Size(180, 25)
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl1.ResumeLayout(False)
        CType(Me.PanelControl2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl2.ResumeLayout(False)
        CType(Me.Flyout1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Flyout1.ResumeLayout(False)
        CType(Me.FlyoutPanelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.FlyoutPanelControl1.ResumeLayout(False)
        CType(Me.txtSearch.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CboSecretaries.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents PanelControl1 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents PanelControl2 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents BtnAddSecretaries As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnSerach As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents Flyout1 As DevExpress.Utils.FlyoutPanel
    Friend WithEvents FlyoutPanelControl1 As DevExpress.Utils.FlyoutPanelControl
    Friend WithEvents txtSearch As DevExpress.XtraEditors.TextEdit
    Friend WithEvents CboSecretaries As DevExpress.XtraEditors.ComboBoxEdit
End Class
