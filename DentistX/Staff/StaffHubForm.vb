Imports System.ComponentModel
Imports DevExpress.XtraEditors

Public Class StaffHubForm
    Inherits XtraForm

    Private ReadOnly _defaultFont As Font = New Font("Calibri", 10, FontStyle.Bold)

    Public Sub New()
        If LicenseManager.UsageMode = LicenseUsageMode.Runtime Then
            StockUiLanguageScope.EnterArabicStockShell()
        End If
        InitializeComponent()
        Text = If(Eng, "Staff Management", "إدارة الموظفين")
        StartPosition = FormStartPosition.CenterScreen
        Font = _defaultFont
    End Sub

    Protected Overrides Sub OnFormClosed(e As FormClosedEventArgs)
        MyBase.OnFormClosed(e)
        If LicenseManager.UsageMode = LicenseUsageMode.Runtime Then
            StockUiLanguageScope.LeaveArabicStockShell()
        End If
    End Sub

    Private Sub btnDashboard_Click(sender As Object, e As EventArgs) Handles btnDashboard.Click
        Try
            Using frm As New MainDashboard()
                frm.Icon = Me.Icon
                frm.ShowDialog(Me)
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message, If(Eng, "Error", "خطأ"), MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnDoctors_Click(sender As Object, e As EventArgs) Handles btnDoctors.Click
        Try
            FrmDoctors.Icon = Me.Icon
            FrmDoctors.ShowDialog(Me)
        Catch ex As Exception
            MessageBox.Show(ex.Message, If(Eng, "Error", "خطأ"), MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnEmployees_Click(sender As Object, e As EventArgs) Handles btnEmployees.Click
        Try
            FrmEmp.Icon = Me.Icon
            FrmEmp.ShowDialog(Me)
        Catch ex As Exception
            MessageBox.Show(ex.Message, If(Eng, "Error", "خطأ"), MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnSecretaries_Click(sender As Object, e As EventArgs) Handles btnSecretaries.Click
        Try
            FrmSecretaries.Icon = Me.Icon
            FrmSecretaries.ShowDialog(Me)
        Catch ex As Exception
            MessageBox.Show(ex.Message, If(Eng, "Error", "خطأ"), MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnPayments_Click(sender As Object, e As EventArgs) Handles btnPayments.Click
        Try
            Using frm As New PersonnelPayment()
                frm.Icon = Me.Icon
                frm.ShowDialog(Me)
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message, If(Eng, "Error", "خطأ"), MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnAttendance_Click(sender As Object, e As EventArgs) Handles btnAttendance.Click
        Try
            Using frm As New PersonnelAttendance()
                frm.Icon = Me.Icon
                frm.ShowDialog(Me)
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message, If(Eng, "Error", "خطأ"), MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnStaffAccountStatement_Click(sender As Object, e As EventArgs) Handles btnStaffAccountStatement.Click
        Try
            Using frm As New StaffAccountStatement()
                frm.Icon = Me.Icon
                frm.ShowDialog(Me)
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message, If(Eng, "Error", "خطأ"), MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
End Class
