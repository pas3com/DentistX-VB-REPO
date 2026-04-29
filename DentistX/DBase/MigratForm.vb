Imports System.ComponentModel

Public Class MigratForm

    Private logger As Logger

    Private Sub MigratForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Initialize logger with both file and memoedit logging
        logger = New Logger(LogTxt, True)
    End Sub

    Private Sub ButtonMigrateData_Click(sender As Object, e As EventArgs) Handles ButtonMigrateData.Click
        ' Clear memoedit and reset line counter at start
        LogTxt.Text = ""
        logger.ResetLineCounter()

        ' Start synchronization
        Try
            Dim src = "Data Source=.;Initial Catalog=Dentist;Trusted_Connection=SSPI;Encrypt=false;TrustServerCertificate=true"
            Dim tgt = "Data Source=.;Initial Catalog=DentistX;Trusted_Connection=SSPI;Encrypt=false;TrustServerCertificate=true"

            ' Pass logger to DatabaseSynchronizer
            Dim sync As New DatabaseSynchronizer(src, tgt, logger)
            sync.SynchronizeDatabases()
        Catch ex As Exception
            logger.Log($"Migration failed: {ex.Message}")
            MessageBox.Show($"Migration failed: {ex.Message}", "Error",
                   MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Me.Cursor = Cursors.Default
            ButtonMigrateData.Enabled = True
        End Try
    End Sub

    Private Sub btnBackup_Click(sender As Object, e As EventArgs) Handles btnBackup.Click
        Backup(New CancelEventArgs, Me)
    End Sub

    ' Optional: Add a clear button that also resets counter
    Private Sub btnClearLog_Click(sender As Object, e As EventArgs) Handles btnClearLog.Click
        LogTxt.Text = ""
        logger.ResetLineCounter()
    End Sub

    ' Optional: Show current line count
    Private Sub btnShowLineCount_Click(sender As Object, e As EventArgs) Handles btnShowLineCount.Click
        MessageBox.Show($"Current line count: {logger.CurrentLineCount}")
    End Sub
End Class