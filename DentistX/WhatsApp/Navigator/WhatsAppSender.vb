Imports DevExpress.XtraEditors
Imports DentistX

Public Class WhatsAppSender
    Inherits XtraForm

    Private ReadOnly _defaultFont As Font = New Font("Calibri", 10, FontStyle.Bold)
    Private ReadOnly _patient As Patient

    Public Sub New()
        Me.New(Nothing)
    End Sub

    Public Sub New(patient As Patient)
        InitializeComponent()
        _patient = patient
        StartPosition = FormStartPosition.CenterScreen
        Font = _defaultFont
        If Not Eng Then
            RightToLeft = RightToLeft.Yes
            RightToLeftLayout = True
        End If
    End Sub

    Private Sub btnSendNormal_Click(sender As Object, e As EventArgs) Handles btnSendNormal.Click
        Try
            Using frm As New WhatsNormal(_patient)
                frm.Icon = Me.Icon
                frm.ShowDialog(Me)
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message, If(Eng, "Error", "خطأ"), MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnAppts_Click(sender As Object, e As EventArgs) Handles btnAppts.Click
        Try
            Using frm As New FrmPatientAppts(_patient)
                frm.Icon = Me.Icon
                frm.ShowDialog(Me)
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message, If(Eng, "Error", "خطأ"), MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnSendAcct_Click(sender As Object, e As EventArgs) Handles btnSendAcct.Click
        Try
            Using frm As New FrmPatientAccnt(_patient)
                frm.Icon = Me.Icon
                frm.ShowDialog(Me)
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message, If(Eng, "Error", "خطأ"), MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub


End Class
