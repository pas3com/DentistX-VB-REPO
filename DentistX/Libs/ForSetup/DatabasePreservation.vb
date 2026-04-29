Imports System.IO
Imports System.Windows.Forms

Module DatabasePreservation
    Const DB_FOLDER As String = "DBase"
    Const DB_NAME As String = "DentistX"
    Const BACKUP_FOLDER As String = "DentistX_Backup"

    Public Sub BackupDatabase()
        Try
            Dim appDataPath As String = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData)
            Dim backupPath As String = Path.Combine(appDataPath, BACKUP_FOLDER)
            Dim installPath As String = Path.GetDirectoryName(Application.ExecutablePath)
            Dim dbPath As String = Path.Combine(installPath, DB_FOLDER)

            ' Create backup directory if it doesn't exist
            If Not Directory.Exists(backupPath) Then
                Directory.CreateDirectory(backupPath)
            End If

            ' Copy database files to backup location
            For Each ext In {".mdf", ".ldf"}
                Dim sourceFile = Path.Combine(dbPath, DB_NAME & ext)
                Dim destFile = Path.Combine(backupPath, DB_NAME & ext)

                If File.Exists(sourceFile) Then
                    File.Copy(sourceFile, destFile, True)
                End If
            Next

        Catch ex As Exception
            ' Log error or notify user
            File.WriteAllText(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData),
                             "DentistX_Update_Error.log"), ex.ToString())
        End Try
    End Sub

    Public Sub RestoreDatabase()
        Try
            Dim appDataPath As String = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData)
            Dim backupPath As String = Path.Combine(appDataPath, BACKUP_FOLDER)
            Dim installPath As String = Path.GetDirectoryName(Application.ExecutablePath)
            Dim dbPath As String = Path.Combine(installPath, DB_FOLDER)

            ' Create database directory if it doesn't exist
            If Not Directory.Exists(dbPath) Then
                Directory.CreateDirectory(dbPath)
            End If

            ' Restore database files from backup
            For Each ext In {".mdf", ".ldf"}
                Dim sourceFile = Path.Combine(backupPath, DB_NAME & ext)
                Dim destFile = Path.Combine(dbPath, DB_NAME & ext)

                If File.Exists(sourceFile) Then
                    File.Copy(sourceFile, destFile, True)
                End If
            Next

            ' Optionally clean up backup
            Try
                Directory.Delete(backupPath, True)
            Catch
                ' Ignore cleanup errors
            End Try

        Catch ex As Exception
            ' Log error or notify user
            File.WriteAllText(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData),
                             "DentistX_Restore_Error.log"), ex.ToString())
        End Try
    End Sub

End Module