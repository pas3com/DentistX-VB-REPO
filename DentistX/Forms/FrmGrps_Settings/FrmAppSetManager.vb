Imports System.Windows.Forms
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Views.Grid

Public Class FrmAppSetManager
    Dim AppSettingData As New AppSetngsDATA

    Private Sub FrmAppSetManager_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadAppSettings()
    End Sub

    Private Sub LoadAppSettings()
        Dim AppSettings = AppSettingData.SelectAll()
        GridControl1.DataSource = AppSettings
    End Sub

    Private Sub BtnAdd_Click(sender As Object, e As EventArgs) Handles BtnAddAppSetting.Click
        Dim AppSettingName = TxtAppSettingName.Text.Trim()
        Dim AppSettingValue = TxtAppSettingValue.Text.Trim()
        If String.IsNullOrEmpty(AppSettingName) Then
            MessageBox.Show("AppSetting name is required.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If
        If String.IsNullOrEmpty(AppSettingValue) Then
            MessageBox.Show("AppSetting name is required.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If
        Dim newAppSetting As New AppSetngs With {.SettingKey = AppSettingName, .SettingValue = AppSettingValue}
        If AppSettingData.Add(newAppSetting) Then
            MessageBox.Show("AppSetting added successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
            LoadAppSettings()
        Else
            MessageBox.Show("Failed to add AppSetting.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    Private Sub BtnEdit_Click(sender As Object, e As EventArgs) Handles BtnEditAppSetting.Click
        If GridView1.FocusedRowHandle < 0 Then
            MessageBox.Show("Select a AppSetting to edit.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        Dim selectedAppSetting As AppSetngs = GridView1.GetRow(GridView1.FocusedRowHandle)
        TxtAppSettingName.Text = selectedAppSetting.SettingKey
        TxtAppSettingValue.Text = selectedAppSetting.SettingValue
        If String.IsNullOrEmpty(TxtAppSettingValue.Text) Then
            MessageBox.Show("AppSetting value is required.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If
        Dim newAppSetting As New AppSetngs With {.SettingKey = selectedAppSetting.SettingKey, .SettingValue = TxtAppSettingValue.Text.Trim()}
        If AppSettingData.Update(newAppSetting) Then
            MessageBox.Show("AppSetting updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
            LoadAppSettings()
        Else
            MessageBox.Show("Failed to update AppSetting.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    Private Sub BtnDelete_Click(sender As Object, e As EventArgs) Handles BtnDelAppSetting.Click
        If GridView1.FocusedRowHandle < 0 Then
            MessageBox.Show("Select a AppSetting to delete.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        Dim selectedAppSetting As AppSetngs = GridView1.GetRow(GridView1.FocusedRowHandle)
        If AppSettingData.Delete(selectedAppSetting.SettingKey) Then
            MessageBox.Show("AppSetting deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
            LoadAppSettings()
        Else
            MessageBox.Show("Failed to delete AppSetting.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub
End Class
