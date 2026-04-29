Imports System.Drawing
Imports System.Windows.Forms
Imports DevExpress.XtraEditors

Partial Public Class FrmChngFinancePass

    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub FrmChngFinancePass_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Icon = AppIcon
        ApplyUiTexts()
        RefreshModeAndLayout()
    End Sub

    Private Sub ApplyUiTexts()
        Dim title = If(Eng, "Finance dashboard password", "كلمة مرور لوحة المالية")
        Text = title
        btnSave.Text = If(Eng, "Save", "حفظ")
        btnCancel.Text = If(Eng, "Cancel", "إلغاء")
        btnRemoveProtection.Text = If(Eng, "Remove protection", "إلغاء الحماية")
        lblCurrent.Text = If(Eng, "Current password:", "كلمة المرور الحالية:")
        lblNew.Text = If(Eng, "New password:", "كلمة المرور الجديدة:")
        lblConfirm.Text = If(Eng, "Confirm new password:", "تأكيد كلمة المرور:")
    End Sub

    Private Sub RefreshModeAndLayout()
        Dim configured = FinanceDashboardPasswordHelper.IsPasswordConfigured()

        lblCurrent.Visible = configured
        txtCurrentPass.Visible = configured
        btnRemoveProtection.Visible = configured

        If configured Then
            lblIntro.Text = If(Eng,
                "Change the password used for the finance dashboard (Ctrl+Shift+F10 and Basic Data menu). Minimum length: " & FinanceDashboardPasswordHelper.MinPasswordLength.ToString() & " characters.",
                "غيّر كلمة المرور الخاصة بلوحة المالية. الحد الأدنى: " & FinanceDashboardPasswordHelper.MinPasswordLength.ToString() & " أحرف.")
            lblNew.Location = New Point(14, 121)
            txtNewPass.Location = New Point(14, 142)
            lblConfirm.Location = New Point(14, 176)
            txtConfirmPass.Location = New Point(14, 197)
            btnSave.Location = New Point(14, 242)
            btnCancel.Location = New Point(148, 242)
            btnRemoveProtection.Location = New Point(282, 242)
            ClientSize = New Size(444, 292)
        Else
            lblIntro.Text = If(Eng,
                "No finance password is set yet. Choose a new password (minimum " & FinanceDashboardPasswordHelper.MinPasswordLength.ToString() & " characters). The same password will be required to open the finance dashboard.",
                "لم تُعيَّن بعد كلمة مرور للوحة المالية. اختر كلمة مرور جديدة (حد أدنى " & FinanceDashboardPasswordHelper.MinPasswordLength.ToString() & " أحرف).")
            lblNew.Location = New Point(14, 66)
            txtNewPass.Location = New Point(14, 87)
            lblConfirm.Location = New Point(14, 121)
            txtConfirmPass.Location = New Point(14, 142)
            btnSave.Location = New Point(14, 186)
            btnCancel.Location = New Point(148, 186)
            ClientSize = New Size(444, 236)
        End If

        txtCurrentPass.Text = ""
        txtNewPass.Text = ""
        txtConfirmPass.Text = ""
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Dim title = If(Eng, "Finance dashboard password", "كلمة مرور لوحة المالية")
        Dim configured = FinanceDashboardPasswordHelper.IsPasswordConfigured()

        If configured Then
            Dim cur = txtCurrentPass.Text
            If String.IsNullOrEmpty(cur) Then
                XtraMessageBox.Show(Me,
                    If(Eng, "Enter the current password.", "أدخل كلمة المرور الحالية."),
                    title, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                txtCurrentPass.Focus()
                Return
            End If
            If Not FinanceDashboardPasswordHelper.VerifyPassword(cur) Then
                XtraMessageBox.Show(Me,
                    If(Eng, "Current password is incorrect.", "كلمة المرور الحالية غير صحيحة."),
                    title, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                txtCurrentPass.Focus()
                Return
            End If
        End If

        Dim newP = txtNewPass.Text
        Dim conf = txtConfirmPass.Text
        If String.IsNullOrEmpty(newP) OrElse newP.Length < FinanceDashboardPasswordHelper.MinPasswordLength Then
            XtraMessageBox.Show(Me,
                If(Eng, $"New password must be at least {FinanceDashboardPasswordHelper.MinPasswordLength} characters.",
                    $"يجب أن تكون كلمة المرور الجديدة {FinanceDashboardPasswordHelper.MinPasswordLength} أحرف على الأقل."),
                title, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtNewPass.Focus()
            Return
        End If
        If Not String.Equals(newP, conf, StringComparison.Ordinal) Then
            XtraMessageBox.Show(Me,
                If(Eng, "New password and confirmation do not match.", "كلمة المرور الجديدة وتأكيدها غير متطابقتين."),
                title, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtConfirmPass.Focus()
            Return
        End If

        FinanceDashboardPasswordHelper.SaveNewPassword(newP)
        XtraMessageBox.Show(Me,
            If(Eng, "Password saved.", "تم حفظ كلمة المرور."),
            title, MessageBoxButtons.OK, MessageBoxIcon.Information)
        DialogResult = DialogResult.OK
        Close()
    End Sub

    Private Sub btnRemoveProtection_Click(sender As Object, e As EventArgs) Handles btnRemoveProtection.Click
        If Not FinanceDashboardPasswordHelper.IsPasswordConfigured() Then Return

        Dim title = If(Eng, "Finance dashboard password", "كلمة مرور لوحة المالية")
        Dim cur = txtCurrentPass.Text
        If String.IsNullOrEmpty(cur) Then
            XtraMessageBox.Show(Me,
                If(Eng, "Enter your current password above before removing protection.", "أدخل كلمة المرور الحالية أعلاه قبل إلغاء الحماية."),
                title, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtCurrentPass.Focus()
            Return
        End If
        If Not FinanceDashboardPasswordHelper.VerifyPassword(cur) Then
            XtraMessageBox.Show(Me,
                If(Eng, "Current password is incorrect.", "كلمة المرور الحالية غير صحيحة."),
                title, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtCurrentPass.Focus()
            Return
        End If

        Dim msg = If(Eng,
            "Remove finance dashboard password protection? The next time you open Finance, you will be asked to set a new password.",
            "إلغاء حماية لوحة المالية؟ عند فتح المالية لاحقاً سيُطلب منك تعيين كلمة مرور جديدة.")

        If XtraMessageBox.Show(Me, msg, title, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) <> DialogResult.Yes Then Return

        FinanceDashboardPasswordHelper.ClearStoredFinancePassword()
        XtraMessageBox.Show(Me,
            If(Eng, "Protection removed. Open Finance again to set a new password.", "تم إلغاء الحماية. افتح المالية لاحقاً لتعيين كلمة مرور جديدة."),
            title, MessageBoxButtons.OK, MessageBoxIcon.Information)
        DialogResult = DialogResult.OK
        Close()
    End Sub

End Class
