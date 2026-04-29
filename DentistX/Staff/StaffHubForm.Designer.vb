<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class StaffHubForm
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(StaffHubForm))
        Me.btnDashboard = New DevExpress.XtraEditors.SimpleButton()
        Me.btnDoctors = New DevExpress.XtraEditors.SimpleButton()
        Me.btnEmployees = New DevExpress.XtraEditors.SimpleButton()
        Me.btnSecretaries = New DevExpress.XtraEditors.SimpleButton()
        Me.btnPayments = New DevExpress.XtraEditors.SimpleButton()
        Me.btnAttendance = New DevExpress.XtraEditors.SimpleButton()
        Me.btnStaffAccountStatement = New DevExpress.XtraEditors.SimpleButton()
        Me.SuspendLayout()
        '
        'btnDashboard
        '
        Me.btnDashboard.Appearance.Font = CType(resources.GetObject("btnDashboard.Appearance.Font"), System.Drawing.Font)
        Me.btnDashboard.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.btnDashboard, "btnDashboard")
        Me.btnDashboard.Name = "btnDashboard"
        '
        'btnDoctors
        '
        Me.btnDoctors.Appearance.Font = CType(resources.GetObject("btnDoctors.Appearance.Font"), System.Drawing.Font)
        Me.btnDoctors.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.btnDoctors, "btnDoctors")
        Me.btnDoctors.Name = "btnDoctors"
        '
        'btnEmployees
        '
        Me.btnEmployees.Appearance.Font = CType(resources.GetObject("btnEmployees.Appearance.Font"), System.Drawing.Font)
        Me.btnEmployees.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.btnEmployees, "btnEmployees")
        Me.btnEmployees.Name = "btnEmployees"
        '
        'btnSecretaries
        '
        Me.btnSecretaries.Appearance.Font = CType(resources.GetObject("btnSecretaries.Appearance.Font"), System.Drawing.Font)
        Me.btnSecretaries.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.btnSecretaries, "btnSecretaries")
        Me.btnSecretaries.Name = "btnSecretaries"
        '
        'btnPayments
        '
        Me.btnPayments.Appearance.Font = CType(resources.GetObject("btnPayments.Appearance.Font"), System.Drawing.Font)
        Me.btnPayments.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.btnPayments, "btnPayments")
        Me.btnPayments.Name = "btnPayments"
        '
        'btnAttendance
        '
        Me.btnAttendance.Appearance.Font = CType(resources.GetObject("btnAttendance.Appearance.Font"), System.Drawing.Font)
        Me.btnAttendance.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.btnAttendance, "btnAttendance")
        Me.btnAttendance.Name = "btnAttendance"
        '
        'btnStaffAccountStatement
        '
        Me.btnStaffAccountStatement.Appearance.Font = CType(resources.GetObject("btnStaffAccountStatement.Appearance.Font"), System.Drawing.Font)
        Me.btnStaffAccountStatement.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.btnStaffAccountStatement, "btnStaffAccountStatement")
        Me.btnStaffAccountStatement.Name = "btnStaffAccountStatement"
        '
        'StaffHubForm
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.btnDashboard)
        Me.Controls.Add(Me.btnDoctors)
        Me.Controls.Add(Me.btnEmployees)
        Me.Controls.Add(Me.btnSecretaries)
        Me.Controls.Add(Me.btnPayments)
        Me.Controls.Add(Me.btnAttendance)
        Me.Controls.Add(Me.btnStaffAccountStatement)
        Me.Name = "StaffHubForm"
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents btnDashboard As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnDoctors As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnEmployees As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnSecretaries As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnPayments As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnAttendance As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnStaffAccountStatement As DevExpress.XtraEditors.SimpleButton
End Class
