Imports System.Windows.Forms
Imports DevExpress.XtraEditors

''' <summary>
''' Finance dashboard gate: password is stored as PBKDF2 hash + salt in user settings (same family as <see cref="PasswordSecurity"/>).
''' There is no reversible ciphertext — verification hashes the typed password and compares to the stored hash.
''' </summary>
Public NotInheritable Class FinanceDashboardPasswordHelper
    Public Const MinPasswordLength As Integer = 4

    Private Sub New()
    End Sub

    Public Shared Function TryUnlockFinanceDashboard(owner As IWin32Window) As Boolean
        Dim title = If(Eng, "Finance dashboard", "لوحة المالية")
        Dim ownerForm = TryCast(owner, Form)

        If Not IsPasswordConfigured() Then
            Dim intro = If(Eng,
                "Set a password for the finance dashboard (Ctrl+Shift+F10 and menu). Enter the new password:",
                "عيّن كلمة مرور للوحة المالية. أدخل كلمة المرور الجديدة:")
            Dim p1 = ShowPasswordPrompt(owner, intro, title)
            If String.IsNullOrEmpty(p1) Then Return False
            If p1.Length < MinPasswordLength Then
                XtraMessageBox.Show(ownerForm,
                    If(Eng, $"Use at least {MinPasswordLength} characters.", $"استخدم {MinPasswordLength} أحرف على الأقل."),
                    title, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return False
            End If
            Dim p2 = ShowPasswordPrompt(owner,
                If(Eng, "Confirm password:", "تأكيد كلمة المرور:"), title)
            If String.IsNullOrEmpty(p2) Then Return False
            If Not String.Equals(p1, p2, StringComparison.Ordinal) Then
                XtraMessageBox.Show(ownerForm,
                    If(Eng, "Passwords do not match.", "كلمتا المرور غير متطابقتين."),
                    title, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return False
            End If
            SaveNewPassword(p1)
            Return True
        End If

        Dim attempt = ShowPasswordPrompt(owner,
            If(Eng, "Finance dashboard — enter password:", "لوحة المالية — أدخل كلمة المرور:"), title)
        If String.IsNullOrEmpty(attempt) Then Return False
        If VerifyPassword(attempt) Then Return True
        XtraMessageBox.Show(ownerForm,
            If(Eng, "Incorrect password.", "كلمة المرور غير صحيحة."),
            title, MessageBoxButtons.OK, MessageBoxIcon.Warning)
        Return False
    End Function

    Public Shared Function IsPasswordConfigured() As Boolean
        Dim s = My.Settings.FinanceDashboardPasswordSalt
        Dim h = My.Settings.FinanceDashboardPasswordHash
        Return Not String.IsNullOrWhiteSpace(s) AndAlso Not String.IsNullOrWhiteSpace(h)
    End Function

    Public Shared Sub SaveNewPassword(plainPassword As String)
        Dim salt As Byte() = Nothing
        Dim hash As Byte() = Nothing
        PasswordSecurity.GeneratePasswordHash(plainPassword, salt, hash)
        My.Settings.FinanceDashboardPasswordSalt = Convert.ToBase64String(salt)
        My.Settings.FinanceDashboardPasswordHash = Convert.ToBase64String(hash)
        My.Settings.Save()
    End Sub

    ''' <summary>Removes stored hash/salt; finance dashboard will prompt to set a new password on next open.</summary>
    Public Shared Sub ClearStoredFinancePassword()
        My.Settings.FinanceDashboardPasswordSalt = ""
        My.Settings.FinanceDashboardPasswordHash = ""
        My.Settings.Save()
    End Sub

    Public Shared Function VerifyPassword(plainPassword As String) As Boolean
        If Not IsPasswordConfigured() Then Return False
        Try
            Dim salt = Convert.FromBase64String(My.Settings.FinanceDashboardPasswordSalt)
            Dim hash = Convert.FromBase64String(My.Settings.FinanceDashboardPasswordHash)
            Return PasswordSecurity.VerifyPassword(plainPassword, salt, hash)
        Catch
            Return False
        End Try
    End Function

    Private Shared Function ShowPasswordPrompt(owner As IWin32Window, prompt As String, caption As String) As String
        Dim editor As New TextEdit()
        editor.Properties.UseSystemPasswordChar = True
        Dim args As New XtraInputBoxArgs() With {
            .Owner = owner,
            .Caption = caption,
            .Prompt = prompt,
            .DefaultResponse = "",
            .Editor = editor
        }
        Dim result = XtraInputBox.Show(args)
        If result Is Nothing Then Return Nothing
        Return If(TypeOf result Is String, DirectCast(result, String), Convert.ToString(result))
    End Function

End Class
