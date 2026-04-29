Imports System.Diagnostics
Imports System.Windows.Forms

''' <summary>Programmer-only: Ctrl+Shift+Alt+F12 when LicenseDevResetEnabled + LicenseDevResetSecret are set in App.config.</summary>

''' <summary>
''' Shown when DentistX.lic is missing, corrupted, or out of sync with local license data.
''' Also used in fatal mode when trial.dat is missing but DB or DentistX.lic still exists.
''' </summary>
Public Class LicenseRecoveryForm

    ''' <summary>When False, only folder actions and Exit are shown (local license data missing).</summary>
    Public Property ShowRepairOptions As Boolean = True

    Public Property MessageText As String
        Get
            Return memoMessage.Text
        End Get
        Set(value As String)
            memoMessage.Text = value
        End Set
    End Property

    ''' <summary>Full text for the paths label (expected file, or multiple locations).</summary>
    Public Property PathsLabelText As String
        Get
            Return lblPath.Text
        End Get
        Set(value As String)
            lblPath.Text = value
        End Set
    End Property

    Private Sub LicenseRecoveryForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.KeyPreview = True
        Me.BringToFront()
        If Not ShowRepairOptions Then
            btnRepair.Visible = False
            btnImport.Visible = False
            btnApply.Visible = False
            btnRequest.Visible = False
            Me.Text = "License — local data problem"
        End If
        btnSaveRequestToRequests.Visible = Not ShowRepairOptions
    End Sub

    Private Sub btnOpenAppFolder_Click(sender As Object, e As EventArgs) Handles btnOpenAppFolder.Click
        CursorLicManager.OpenFolderInExplorer(AppDomain.CurrentDomain.BaseDirectory)
    End Sub

    Private Sub btnOpenDataFolder_Click(sender As Object, e As EventArgs) Handles btnOpenDataFolder.Click
        CursorLicManager.OpenFolderInExplorer(CursorLicConfig.GetDataDir())
    End Sub

    Private Sub btnRepair_Click(sender As Object, e As EventArgs) Handles btnRepair.Click
        CursorLicManager.RepairDecoyLicenseFromLocalState(Date.Today)
        Me.DialogResult = DialogResult.OK
        Me.Close()
    End Sub

    Private Sub btnImport_Click(sender As Object, e As EventArgs) Handles btnImport.Click
        Using ofd As New OpenFileDialog()
            ofd.Filter = "License files (*.lic)|*.lic|All files (*.*)|*.*"
            ofd.Title = "Select DentistX.lic"
            If ofd.ShowDialog(Me) <> DialogResult.OK Then
                Return
            End If
            If CursorLicManager.TryImportDecoyLicenseFile(ofd.FileName) Then
                Me.DialogResult = DialogResult.OK
                Me.Close()
            End If
        End Using
    End Sub

    Private Sub btnApply_Click(sender As Object, e As EventArgs) Handles btnApply.Click
        Using ofd As New OpenFileDialog()
            ofd.Filter = "Response files (*.resp;*.json)|*.resp;*.json|All files (*.*)|*.*"
            ofd.Title = "Select license response file"
            If ofd.ShowDialog(Me) <> DialogResult.OK Then
                Return
            End If
            If CursorLicManager.ApplyResponseFile(ofd.FileName) Then
                Me.DialogResult = DialogResult.OK
                Me.Close()
            End If
        End Using
    End Sub

    Private Sub btnRequest_Click(sender As Object, e As EventArgs) Handles btnRequest.Click
        CursorLicManager.CreateRequestFileWithForm(Me)
    End Sub

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        Me.DialogResult = DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub btnSaveRequestToRequests_Click(sender As Object, e As EventArgs) Handles btnSaveRequestToRequests.Click
        CursorLicManager.TrySaveLicenseRequestToAppRequestsFolder(Me)
    End Sub

    Protected Overrides Function ProcessCmdKey(ByRef msg As Message, keyData As Keys) As Boolean
        Const chord = Keys.Control Or Keys.Shift Or Keys.Alt Or Keys.F12
        If keyData = chord Then
            CursorLicManager.TryDeveloperHotkeyResetTrial(Me)
            Return True
        End If
        Return MyBase.ProcessCmdKey(msg, keyData)
    End Function
End Class
