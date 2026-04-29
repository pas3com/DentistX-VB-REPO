<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmManager
    Inherits DevExpress.XtraBars.ToolbarForm.ToolbarForm

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
        Me.ToolbarFormControl1 = New DevExpress.XtraBars.ToolbarForm.ToolbarFormControl()
        Me.ToolbarFormManager1 = New DevExpress.XtraBars.ToolbarForm.ToolbarFormManager(Me.components)
        Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl()
        Me.BarSubItem1 = New DevExpress.XtraBars.BarSubItem()
        Me.BarUsers = New DevExpress.XtraBars.BarSubItem()
        Me.BarListUsers = New DevExpress.XtraBars.BarSubItem()
        Me.BarAddUser = New DevExpress.XtraBars.BarSubItem()
        Me.BarEditUser = New DevExpress.XtraBars.BarSubItem()
        Me.BarChangePass = New DevExpress.XtraBars.BarSubItem()
        Me.BarResetPass = New DevExpress.XtraBars.BarSubItem()
        Me.BarGroups = New DevExpress.XtraBars.BarSubItem()
        Me.BarListGroups = New DevExpress.XtraBars.BarSubItem()
        Me.BarAppSett = New DevExpress.XtraBars.BarSubItem()
        Me.BarListAppSett = New DevExpress.XtraBars.BarSubItem()
        Me.BarForms = New DevExpress.XtraBars.BarSubItem()
        Me.BarListForms = New DevExpress.XtraBars.BarSubItem()
        Me.BarPermiss = New DevExpress.XtraBars.BarSubItem()
        Me.BarListPermiss = New DevExpress.XtraBars.BarSubItem()
        Me.BarSubItem8 = New DevExpress.XtraBars.BarSubItem()
        CType(Me.ToolbarFormControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ToolbarFormManager1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ToolbarFormControl1
        '
        Me.ToolbarFormControl1.Location = New System.Drawing.Point(0, 0)
        Me.ToolbarFormControl1.Manager = Me.ToolbarFormManager1
        Me.ToolbarFormControl1.Name = "ToolbarFormControl1"
        Me.ToolbarFormControl1.Size = New System.Drawing.Size(699, 29)
        Me.ToolbarFormControl1.TabIndex = 0
        Me.ToolbarFormControl1.TabStop = False
        Me.ToolbarFormControl1.TitleItemLinks.Add(Me.BarSubItem1)
        Me.ToolbarFormControl1.ToolbarForm = Me
        '
        'ToolbarFormManager1
        '
        Me.ToolbarFormManager1.DockControls.Add(Me.barDockControlTop)
        Me.ToolbarFormManager1.DockControls.Add(Me.barDockControlBottom)
        Me.ToolbarFormManager1.DockControls.Add(Me.barDockControlLeft)
        Me.ToolbarFormManager1.DockControls.Add(Me.barDockControlRight)
        Me.ToolbarFormManager1.Form = Me
        Me.ToolbarFormManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.BarSubItem1, Me.BarUsers, Me.BarGroups, Me.BarAppSett, Me.BarForms, Me.BarPermiss, Me.BarListUsers, Me.BarSubItem8, Me.BarAddUser, Me.BarEditUser, Me.BarChangePass, Me.BarResetPass, Me.BarListGroups, Me.BarListAppSett, Me.BarListForms, Me.BarListPermiss})
        Me.ToolbarFormManager1.MaxItemId = 16
        '
        'barDockControlTop
        '
        Me.barDockControlTop.CausesValidation = False
        Me.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.barDockControlTop.Location = New System.Drawing.Point(0, 29)
        Me.barDockControlTop.Manager = Me.ToolbarFormManager1
        Me.barDockControlTop.Size = New System.Drawing.Size(699, 0)
        '
        'barDockControlBottom
        '
        Me.barDockControlBottom.CausesValidation = False
        Me.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.barDockControlBottom.Location = New System.Drawing.Point(0, 607)
        Me.barDockControlBottom.Manager = Me.ToolbarFormManager1
        Me.barDockControlBottom.Size = New System.Drawing.Size(699, 0)
        '
        'barDockControlLeft
        '
        Me.barDockControlLeft.CausesValidation = False
        Me.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me.barDockControlLeft.Location = New System.Drawing.Point(0, 29)
        Me.barDockControlLeft.Manager = Me.ToolbarFormManager1
        Me.barDockControlLeft.Size = New System.Drawing.Size(0, 578)
        '
        'barDockControlRight
        '
        Me.barDockControlRight.CausesValidation = False
        Me.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right
        Me.barDockControlRight.Location = New System.Drawing.Point(699, 29)
        Me.barDockControlRight.Manager = Me.ToolbarFormManager1
        Me.barDockControlRight.Size = New System.Drawing.Size(0, 578)
        '
        'BarSubItem1
        '
        Me.BarSubItem1.Caption = "Choose Form"
        Me.BarSubItem1.Id = 0
        Me.BarSubItem1.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.BarUsers), New DevExpress.XtraBars.LinkPersistInfo(Me.BarGroups), New DevExpress.XtraBars.LinkPersistInfo(Me.BarAppSett), New DevExpress.XtraBars.LinkPersistInfo(Me.BarForms), New DevExpress.XtraBars.LinkPersistInfo(Me.BarPermiss)})
        Me.BarSubItem1.Name = "BarSubItem1"
        '
        'BarUsers
        '
        Me.BarUsers.Caption = "Users"
        Me.BarUsers.Id = 1
        Me.BarUsers.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.BarListUsers), New DevExpress.XtraBars.LinkPersistInfo(Me.BarAddUser), New DevExpress.XtraBars.LinkPersistInfo(Me.BarEditUser), New DevExpress.XtraBars.LinkPersistInfo(Me.BarChangePass), New DevExpress.XtraBars.LinkPersistInfo(Me.BarResetPass)})
        Me.BarUsers.Name = "BarUsers"
        '
        'BarListUsers
        '
        Me.BarListUsers.Caption = "List Users"
        Me.BarListUsers.Id = 6
        Me.BarListUsers.Name = "BarListUsers"
        '
        'BarAddUser
        '
        Me.BarAddUser.Caption = "Add User"
        Me.BarAddUser.Id = 8
        Me.BarAddUser.Name = "BarAddUser"
        '
        'BarEditUser
        '
        Me.BarEditUser.Caption = "Edit User"
        Me.BarEditUser.Id = 9
        Me.BarEditUser.Name = "BarEditUser"
        '
        'BarChangePass
        '
        Me.BarChangePass.Caption = "Change Password"
        Me.BarChangePass.Id = 10
        Me.BarChangePass.Name = "BarChangePass"
        '
        'BarResetPass
        '
        Me.BarResetPass.Caption = "Reset Password"
        Me.BarResetPass.Id = 11
        Me.BarResetPass.Name = "BarResetPass"
        '
        'BarGroups
        '
        Me.BarGroups.Caption = "Groups"
        Me.BarGroups.Id = 2
        Me.BarGroups.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.BarListGroups)})
        Me.BarGroups.Name = "BarGroups"
        '
        'BarListGroups
        '
        Me.BarListGroups.Caption = "List Groups"
        Me.BarListGroups.Id = 12
        Me.BarListGroups.Name = "BarListGroups"
        '
        'BarAppSett
        '
        Me.BarAppSett.Caption = "App Settings"
        Me.BarAppSett.Id = 3
        Me.BarAppSett.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.BarListAppSett)})
        Me.BarAppSett.Name = "BarAppSett"
        '
        'BarListAppSett
        '
        Me.BarListAppSett.Caption = "List App Settings"
        Me.BarListAppSett.Id = 13
        Me.BarListAppSett.Name = "BarListAppSett"
        '
        'BarForms
        '
        Me.BarForms.Caption = "Forms"
        Me.BarForms.Id = 4
        Me.BarForms.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.BarListForms)})
        Me.BarForms.Name = "BarForms"
        '
        'BarListForms
        '
        Me.BarListForms.Caption = "List Forms"
        Me.BarListForms.Id = 14
        Me.BarListForms.Name = "BarListForms"
        '
        'BarPermiss
        '
        Me.BarPermiss.Caption = "Permissions"
        Me.BarPermiss.Id = 5
        Me.BarPermiss.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.BarListPermiss)})
        Me.BarPermiss.Name = "BarPermiss"
        '
        'BarListPermiss
        '
        Me.BarListPermiss.Caption = "List Permissions"
        Me.BarListPermiss.Id = 15
        Me.BarListPermiss.Name = "BarListPermiss"
        '
        'BarSubItem8
        '
        Me.BarSubItem8.Caption = "BarSubItem8"
        Me.BarSubItem8.Id = 7
        Me.BarSubItem8.Name = "BarSubItem8"
        '
        'FrmManager
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(699, 607)
        Me.Controls.Add(Me.barDockControlLeft)
        Me.Controls.Add(Me.barDockControlRight)
        Me.Controls.Add(Me.barDockControlBottom)
        Me.Controls.Add(Me.barDockControlTop)
        Me.Controls.Add(Me.ToolbarFormControl1)
        Me.Name = "FrmManager"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "FrmManager"
        Me.ToolbarFormControl = Me.ToolbarFormControl1
        CType(Me.ToolbarFormControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ToolbarFormManager1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents ToolbarFormControl1 As DevExpress.XtraBars.ToolbarForm.ToolbarFormControl
    Friend WithEvents ToolbarFormManager1 As DevExpress.XtraBars.ToolbarForm.ToolbarFormManager
    Friend WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl
    Friend WithEvents BarSubItem1 As DevExpress.XtraBars.BarSubItem
    Friend WithEvents BarUsers As DevExpress.XtraBars.BarSubItem
    Friend WithEvents BarListUsers As DevExpress.XtraBars.BarSubItem
    Friend WithEvents BarSubItem8 As DevExpress.XtraBars.BarSubItem
    Friend WithEvents BarGroups As DevExpress.XtraBars.BarSubItem
    Friend WithEvents BarAppSett As DevExpress.XtraBars.BarSubItem
    Friend WithEvents BarForms As DevExpress.XtraBars.BarSubItem
    Friend WithEvents BarPermiss As DevExpress.XtraBars.BarSubItem
    Friend WithEvents BarAddUser As DevExpress.XtraBars.BarSubItem
    Friend WithEvents BarEditUser As DevExpress.XtraBars.BarSubItem
    Friend WithEvents BarChangePass As DevExpress.XtraBars.BarSubItem
    Friend WithEvents BarResetPass As DevExpress.XtraBars.BarSubItem
    Friend WithEvents BarListGroups As DevExpress.XtraBars.BarSubItem
    Friend WithEvents BarListAppSett As DevExpress.XtraBars.BarSubItem
    Friend WithEvents BarListForms As DevExpress.XtraBars.BarSubItem
    Friend WithEvents BarListPermiss As DevExpress.XtraBars.BarSubItem
End Class
