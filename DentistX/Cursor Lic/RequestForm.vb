
Imports DentistXLicense


Public Class RequestForm
    Public Property SelectedPlan As String = "Standard"
    Public Property SelectedPeriod As String = "3 months"
    Public Property DoctorName As String = ""
    Public Property ClinicName As String = ""
    Public Property ContactEmail As String = ""
    Public Property LicenseInfo As Dictionary(Of String, String)
    Public Property RequestConfirmed As Boolean = False



    'Private Sub CreateUpgradeRequest()
    '    DoctorName = SettingsHelper.ReadEncryptedSetting("DoctorName", "")
    '    ClinicName = SettingsHelper.ReadEncryptedSetting("ClinicName", "")

    '    txtDoctorName.Text = DoctorName
    '    txtClinicName.Text = ClinicName
    '    Dim request As New LicenseRequest With {
    '    .DoctorName = DoctorName,
    '    .ClinicName = ClinicName,
    '    .Plan = "Standard",
    '    .PeriodMonths = 12,
    '    .Fingerprint = FingerprintProvider.GetCurrentFingerprint(),
    '    .MachineName = Environment.MachineName,
    '    .RequestDateUtc = DateTime.UtcNow,
    '    .AppVersion = Application.ProductVersion
    '}

    '    Dim writer As New LicenseRequestWriter(SAMPLE_KEYS.DevPublicKeyXml)

    '    Using sfd As New SaveFileDialog()
    '        sfd.Filter = "Request Files (*.req)|*.req"
    '        sfd.FileName = "DentistX.req"

    '        If sfd.ShowDialog() = DialogResult.OK Then
    '            writer.WriteV2(sfd.FileName, request)
    '            MessageBox.Show("Upgrade request created.")
    '        End If
    '    End Using

    'End Sub

    'Private Sub CreateLicenseRequest()

    '    ' --- collect business data ---
    '    Dim doctorName As String = txtDoctorName.Text.Trim()
    '    Dim clinicName As String = txtClinicName.Text.Trim()

    '    If doctorName = "" OrElse clinicName = "" Then
    '        MessageBox.Show("Doctor and Clinic names are required.")
    '        Return
    '    End If

    '    ' --- build request ---
    '    Dim request As New LicenseRequest With {
    '    .DoctorName = doctorName,
    '    .ClinicName = clinicName,
    '    .RequestedPlan = "Standard",
    '    .RequestedMonths = 3,
    '    .Fingerprint = FingerprintProvider.GetCurrentFingerprint(),
    '    .RequestDateUtc = DateTime.UtcNow,
    '    .AppVersion = Application.ProductVersion
    '}

    '    ' --- metadata (optional, plaintext) ---
    '    Dim metadata As New Dictionary(Of String, String) From {
    '    {"Machine", Environment.MachineName},
    '    {"OS", Environment.OSVersion.ToString()}
    '}

    '    ' --- write file ---
    '    Dim writer As New LicenseRequestWriter(
    '   SAMPLE_KEYS.DevPublicKeyXml,
    '    ClientPrivateKeyXml)

    '    Using sfd As New SaveFileDialog()
    '        sfd.Filter = "Request Files (*.req)|*.req"
    '        sfd.FileName = "DentistX.req"

    '        If sfd.ShowDialog() = DialogResult.OK Then
    '            writer.WriteV2(sfd.FileName, request, metadata)
    '            MessageBox.Show("License request created successfully.")
    '        End If
    '    End Using

    'End Sub




    Private Sub RequestForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Display current license info
        txtCurrentLicense.Text = $"End Date: {LicenseInfo("EndDate")}" & Environment.NewLine &
                                $"Last Check: {LicenseInfo("LastCheckDate")}" & Environment.NewLine &
                                $"Fingerprint: {LicenseInfo("FingerprintHash")}"
        ' Set default selections
        cmbPlan.SelectedItem = SelectedPlan
        cmbPeriod.SelectedItem = SelectedPeriod
        ' Try to load existing doctor/clinic info from settings
        DoctorName = SettingsHelper.ReadEncryptedSetting("DoctorName", "")
        ClinicName = SettingsHelper.ReadEncryptedSetting("ClinicName", "")
        ContactEmail = SettingsHelper.ReadEncryptedSetting("ContactEmail", "")

        txtDoctorName.Text = DoctorName
        txtClinicName.Text = ClinicName
        txtEmail.Text = ContactEmail
    End Sub
    Private Sub btnCreateRequest_Click(sender As Object, e As EventArgs) Handles btnCreateRequest.Click
        ' Validate required fields
        If String.IsNullOrWhiteSpace(txtDoctorName.Text) Then
            MessageBox.Show("Please enter the Doctor Name.", "Validation Error",
                          MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtDoctorName.Focus()
            Return
        End If
        If String.IsNullOrWhiteSpace(txtClinicName.Text) Then
            MessageBox.Show("Please enter the Clinic Name.", "Validation Error",
                          MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtClinicName.Focus()
            Return
        End If
        If String.IsNullOrWhiteSpace(txtEmail.Text) OrElse Not IsValidEmail(txtEmail.Text.Trim()) Then
            MessageBox.Show("Please enter a valid Email address.", "Validation Error",
                          MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtEmail.Focus()
            Return
        End If
        ' Save doctor and clinic info to settings for future use
        SettingsHelper.SaveEncryptedSetting("DoctorName", txtDoctorName.Text.Trim())
        SettingsHelper.SaveEncryptedSetting("ClinicName", txtClinicName.Text.Trim())
        SettingsHelper.SaveEncryptedSetting("ContactEmail", txtEmail.Text.Trim())
        ' Set properties
        SelectedPlan = cmbPlan.SelectedItem.ToString()
        SelectedPeriod = cmbPeriod.SelectedItem.ToString()
        DoctorName = txtDoctorName.Text.Trim()
        ClinicName = txtClinicName.Text.Trim()
        ContactEmail = txtEmail.Text.Trim()
        RequestConfirmed = True

        Me.DialogResult = DialogResult.OK
        Me.Close()
        'LicenseManagerNew.CreateLicenseRequest("Trial Extend")
    End Sub

    Private Function IsValidEmail(value As String) As Boolean
        Try
            Dim addr = New System.Net.Mail.MailAddress(value)
            Return String.Equals(addr.Address, value, StringComparison.OrdinalIgnoreCase)
        Catch
            Return False
        End Try
    End Function

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.DialogResult = DialogResult.Cancel
        Me.Close()
    End Sub
End Class

'Public Class RequestForm
'    Public Property SelectedPlan As String = "Standard"
'    Public Property SelectedPeriod As String = "3 months"
'    Public Property LicenseInfo As Dictionary(Of String, String)
'    Public Property RequestConfirmed As Boolean = False

'    Private Sub RequestForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
'        ' Display current license info
'        txtCurrentLicense.Text = $"End Date: {LicenseInfo("EndDate")}" & Environment.NewLine &
'                                $"Last Check: {LicenseInfo("LastCheckDate")}" & Environment.NewLine &
'                                $"Fingerprint: {LicenseInfo("FingerprintHash")}"

'        ' Set default selections
'        cmbPlan.SelectedItem = SelectedPlan
'        cmbPeriod.SelectedItem = SelectedPeriod
'    End Sub

'    Private Sub btnCreateRequest_Click(sender As Object, e As EventArgs) Handles btnCreateRequest.Click
'        SelectedPlan = cmbPlan.SelectedItem.ToString()
'        SelectedPeriod = cmbPeriod.SelectedItem.ToString()
'        RequestConfirmed = True
'        Me.DialogResult = DialogResult.OK
'        Me.Close()
'    End Sub

'    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
'        Me.DialogResult = DialogResult.Cancel
'        Me.Close()
'    End Sub
'End Class