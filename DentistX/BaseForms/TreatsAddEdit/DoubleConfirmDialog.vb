Public Class DoubleConfirmDialog
    Public Property Message As String
    Public Property IsConfirmed As Boolean = False

    Private Sub btnConfirm_Click(sender As Object, e As EventArgs) Handles btnConfirm.Click
        If chkConfirm.Checked Then
            IsConfirmed = True
            Me.DialogResult = DialogResult.OK
        Else
            Dim msgEn As String = "You must check the confirmation box to proceed."
            Dim msgAr As String = "يجب عليك تحديد مربع التأكيد للمتابعة."
            Dim msg As String = If(Eng, msgEn, msgAr)
            Dim valEn As String = "Validation"
            Dim valAr As String = "التحقق"
            Dim valTitle As String = If(Eng, valEn, valAr)

            MessageBox.Show(msg, valTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End If
    End Sub

    Private Sub DoubleConfirmDialog_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        lblMessage.Text = Message
        Me.Icon = AppIcon
    End Sub
End Class