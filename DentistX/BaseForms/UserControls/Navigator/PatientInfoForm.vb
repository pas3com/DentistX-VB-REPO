Imports System.Threading.Tasks
Imports System.Data.SqlClient
Imports Dapper
Imports DevExpress.Utils.Extensions
Imports DevExpress.XtraEditors
Imports Infragistics.Win.UltraWinGrid
Public Class PatientInfoForm
    Dim newPatient As Patient
    Private patientMgr As New PatientEventManager()
    Dim Kid As Boolean = False
    Dim Full As Boolean = True
    Dim Grid As Boolean = False
    Dim Reset As Integer = 0
    Dim loaded As Boolean = False
    Private connectionString As String = DentistX.My.Settings.DentistXConnectionString '"Your_Connection_String_Here"
    Private CurrentPatient As Patient
    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()
    End Sub
    Public Sub New(patientID As Integer)
        ' This call is required by the designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        LoadPatient(patientID)
    End Sub


    Private Sub PatientInfoForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'btnDelete.Enabled = Perms.CanDo("Patients.Delete")
        'btnEditPat.Enabled = Perms.CanDo("Patients.Edit")
    End Sub


#Region "Global_SUBS_Funcs"
    Public Function GetTableRecordCount(tableName As String) As Integer
        Using connection As New SqlConnection(connectionString)
            connection.Open()
            Dim sql = $"SELECT COUNT(*) FROM [{tableName}]"
            Return connection.ExecuteScalar(Of Integer)(sql)
        End Using
    End Function
    ' Function to get balance using Dapper
    Public Function GetBalance(ByVal PatientID As Integer) As Double
        Dim query As String = "SELECT ISNULL(dbo.PatientPAYS.PAYS - dbo.PatientTRTS.TRTS, 0) AS BAL " &
                              "FROM dbo.Patient " &
                              "LEFT OUTER JOIN dbo.PatientTRTS ON dbo.Patient.PatientID = dbo.PatientTRTS.PatientID " &
                              "LEFT OUTER JOIN dbo.PatientPAYS ON dbo.Patient.PatientID = dbo.PatientPAYS.PatientID " &
                              "WHERE dbo.Patient.PatientID = @PatientID"
        Using conn As New SqlConnection(connectionString)
            Return conn.QuerySingleOrDefault(Of Double)(query, New With {.PatientId = PatientID})
        End Using
    End Function
    Public Function GetAge(ByVal PatientID As Integer) As Integer
        Dim query As String = "SELECT ISNULL(BirthY, 0) FROM Patient WHERE PatientID = @PatientID"
        Try
            Using conn As New SqlConnection(DentistXDATA.GetConnection.ConnectionString)
                Dim BrthYear As Integer = conn.QuerySingleOrDefault(Of Integer)(query, New With {.PatientId = PatientID})
                ' Calculate age
                Return Now.Year - BrthYear
            End Using
        Catch ex As Exception
            MsgBox(ex.Message)
            Return 0 ' Return default age in case of an error
        End Try
    End Function
    Public Sub SetAge(ByVal PatientID As Integer)
        Dim age As Integer = GetAge(PatientID)
        If age <= 10 Then
            AgeSpinEdit.Text = age
            AgeSpinEdit.ForeColor = Color.Red
        Else
            AgeSpinEdit.Text = age
            AgeSpinEdit.ForeColor = Color.Blue
        End If
    End Sub
    Private Function Select_RecordByID(ByVal patientID As Integer) As Patient
        Using conn As New SqlConnection(connectionString)
            Dim sql As String = "Select * FROM Patient WHERE PatientID = @PatientID"
            Return conn.QuerySingleOrDefault(Of Patient)(sql, New With {.PatientId = patientID})
        End Using
    End Function
    Public Function LoadPatient(ByVal PatientID As Integer) As Patient
        'Dim query As String = "SELECT [PatientID],[PatientName],[Sex],[Age],[Phone],[Address],[Health],[Treat],[Implant],[Mobile],[Ortho],[Struc],
        '                        [Notes],[BirthY],[CreatedBy],[CreateDate] FROM Patient WHERE PatientID = @PatientID"
        CurrentPatient = Select_RecordByID(PatientID)
        PatientID = CurrentPatient.PatientID
        PatientName = CurrentPatient.PatientName
        Try
            ' Bind to PatientBindingSource
            If CurrentPatient IsNot Nothing Then
                CurrentPatient.IsKid = Kid
                CurrentPatient.IsGrid = Grid
                CurrentPatient.IsFull = Full
                Me.LabelBal.Text = GetBalance(CurrentPatient.PatientID)
                Me.AgeSpinEdit.Value = GetAge(CurrentPatient.PatientID)
                If AgeSpinEdit.Value > 120 Then
                    AgeSpinEdit.Value = 20
                End If
                If CurrentPatient.Age < 150 Then
                    Me.LabelAge.Text = If(Eng, CurrentPatient.Age.ToString & " Yrs", CurrentPatient.Age.ToString & "  سنة ")
                Else
                    Me.LabelAge.Text = If(Eng, "Age Not Set", "العمر غير محدد")
                End If
                PatientBindingSource.DataSource = CurrentPatient
                Return CurrentPatient
            Else
                PatientBindingSource.DataSource = Nothing ' Clear if no data found
                Return Nothing ' Return Nothing in case of an error
            End If
        Catch ex As Exception
            MsgBox("Error loading patient: " & ex.Message)
            Return Nothing ' Return Nothing in case of an error
        End Try
    End Function
    Public Sub LoadBal(ByVal PatientId As Integer)
        Try
            Dim query As String = "SELECT ISNULL(dbo.PatientPAYS.PAYS - dbo.PatientTRTS.TRTS, 0) AS BAL " &
                              "FROM dbo.Patient " &
                              "LEFT OUTER JOIN dbo.PatientTRTS ON dbo.Patient.PatientID = dbo.PatientTRTS.PatientID " &
                              "LEFT OUTER JOIN dbo.PatientPAYS ON dbo.Patient.PatientID = dbo.PatientPAYS.PatientID " &
                              "WHERE dbo.Patient.PatientID = @PatientID"
            Using conn As New SqlConnection(connectionString)
                Dim d As Double
                d = conn.QuerySingleOrDefault(Of Double)(query, New With {.PatientId = PatientId})
                If Double.TryParse(Me.LabelBal.Text, d) Then
                    Me.LabelBal.Text = d.ToString("###0.00")
                    If d < 0 Then
                        Me.LabelBal.ForeColor = Color.Red
                    ElseIf d > 0 Then
                        Me.LabelBal.ForeColor = Color.Blue
                    Else
                        Me.LabelBal.ForeColor = Color.Black
                    End If
                Else
                    ' Handle cases where the text is not a valid number, if necessary
                    Me.LabelBal.ForeColor = Color.Black
                    Me.LabelBal.Text = "0.00"
                End If
            End Using
        Catch ex As System.Data.SqlClient.SqlException
            MsgBox(ex.Message)
        End Try
    End Sub
#End Region

#Region "PatientHdrControls"
    Private Sub HealthTextBox_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HealthTextBox.TextChanged
        If Eng Then
            If Me.HealthTextBox.Text = "Healthy" Or Me.HealthTextBox.Text = "سليم" Then
                Me.HealthTextBox.ForeColor = Color.Blue
            Else
                Me.HealthTextBox.ForeColor = Color.Red
            End If
        Else
            If Me.HealthTextBox.Text = "Healthy" Or Me.HealthTextBox.Text = "سليم" Then
                Me.HealthTextBox.ForeColor = Color.Blue
            Else
                Me.HealthTextBox.ForeColor = Color.Red
            End If
        End If
    End Sub
    Private Sub SexTextBox_TextChanged(sender As Object, e As EventArgs) Handles SexTextBox.TextChanged
        Try
            If Me.SexTextBox.Text = "ذكر" Or Me.SexTextBox.Text = "Male" Then
                Me.PicBox.Image = My.Resources.Male
            ElseIf Me.SexTextBox.Text = "أنثى" Or Me.SexTextBox.Text = "Female" Or Me.SexTextBox.Text = "انثى" Then
                Me.PicBox.Image = My.Resources.Female
            Else
                Me.PicBox.Image = PicBox.InitialImage
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub AgeSpinEdit_EditValueChanged(sender As Object, e As EventArgs) Handles AgeSpinEdit.EditValueChanged
        If AgeSpinEdit.EditValue = 0 Then Exit Sub
        Dim x As Integer = CInt(AgeSpinEdit.EditValue)
        If x >= 3 And x <= 10 Then
            Kid = True
        ElseIf x > 10 Then
            Kid = False
        End If
        BirthYSpinEdit.EditValue = Now.Year - x
        CurrentPatient.Age = x
        If CurrentPatient.Age < 150 Then
            Me.LabelAge.Text = If(Eng, CurrentPatient.Age.ToString & " Yrs", CurrentPatient.Age.ToString & "  سنة ")
        Else
            Me.LabelAge.Text = If(Eng, "Age Not Set", "العمر غير محدد")
        End If
    End Sub
    Private Sub BirthYSpinEdit_EditValueChanged(sender As Object, e As EventArgs) Handles BirthYSpinEdit.EditValueChanged
        If BirthYSpinEdit.EditValue = 0 Then Exit Sub
        ' Temporarily disable the AgeSpinEdit event handler to avoid recursion
        RemoveHandler AgeSpinEdit.EditValueChanged, AddressOf AgeSpinEdit_EditValueChanged
        Dim x As Integer = CInt(BirthYSpinEdit.EditValue)
        Dim y As Integer = Now.Year
        x = y - x
        If y - x >= 3 And y - x <= 10 Then
            Kid = True
        ElseIf y - x > 10 Then
            Kid = False
        End If
        AgeSpinEdit.EditValue = x
        CurrentPatient.Age = x
        If CurrentPatient.Age < 150 Then
            Me.LabelAge.Text = If(Eng, CurrentPatient.Age.ToString & " Yrs", CurrentPatient.Age.ToString & "  سنة ")
        Else
            Me.LabelAge.Text = If(Eng, "Age Not Set", "العمر غير محدد")
        End If
        ' Re-enable the AgeSpinEdit event handler
        AddHandler AgeSpinEdit.EditValueChanged, AddressOf AgeSpinEdit_EditValueChanged
    End Sub
    Private Sub PatientIDEdit_EditValueChanged(sender As Object, e As EventArgs) Handles PatientIDEdit.TextChanged
        PatientID = CInt(PatientIDEdit.EditValue)
    End Sub
    Private Function CheckTxt() As Boolean
        Dim errorMessages As New List(Of String)()
        ' Check Patient Name
        If String.IsNullOrWhiteSpace(PatientNameTextEdit.Text) Then
            errorMessages.Add(If(Eng, "Patient name is required", "اسم المريض مطلوب"))
            PatientNameTextEdit.Focus()
            ' Check Age
        ElseIf AgeSpinEdit.Value <= 0 Then
            Dim message As String = If(AgeSpinEdit.Value = 0,
                                If(Eng, "Age cannot be zero", "العمر لا يمكن أن يكون صفر"),
                                If(Eng, "Age cannot be negative", "العمر لا يمكن أن يكون سالب"))
            errorMessages.Add(message)
            AgeSpinEdit.Focus()
            ' Check Health
            'ElseIf String.IsNullOrWhiteSpace(HealthTextBox.Text) Then
            '    errorMessages.Add(If(Eng, "Health information is required", "المعلومات الصحية مطلوبة"))
            '    HealthTextBox.Focus()
            '    ' Check Address
            'ElseIf String.IsNullOrWhiteSpace(AddressTextEdit.Text) Then
            '    errorMessages.Add(If(Eng, "Address is required", "العنوان مطلوب"))
            '    AddressTextEdit.Focus()
        End If
        ' Check Age
        If Me.LabelAge.Text = If(Eng, "Age Not Set", "العمر غير محدد") Then
            errorMessages.Add(If(Eng, "Age is required", "العمر مطلوب"))
            AddressTextEdit.Focus()
        End If
        ' Check Number
        If Me.lblPNum.Text.Length < 5 Then
            errorMessages.Add(If(Eng, "Patient Number is required", "رقم المريض مطلوب"))
            lblPNum.Focus()
        End If
        ' Show error message if any validation failed
        If errorMessages.Count > 0 Then
            Dim messageTitle As String = If(Eng, "Validation Error", "خطأ في التحقق")
            Dim fullMessage As String = String.Join(vbCrLf, errorMessages)
            MessageBox.Show(fullMessage, messageTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return False
        End If
        Return True
    End Function
    Private Sub CboCity_CityValueChanged(sender As Object, e As CityCombo.CityIndexChangedEvent) Handles CboCity.CityValueChanged
        If CboCity.CityID = -1 Then
            AddressTextEdit.Text = ""
        Else
            AddressTextEdit.Text = CboCity.CityName
        End If
    End Sub
    Private Sub CboHealth_SelectedIndexChanged(sender As Object, e As HlthCombo.HealthIndexChangedEvent) Handles CboHealth.HealthValueChanged
        If CboHealth.HID = -1 Then
            HealthTextBox.Text = ""
        Else
            HealthTextBox.Text = CboHealth.HealthStat
        End If
    End Sub



    Property PatientUpdated As Boolean = False

    Private Async Sub btnEditPat_Click(sender As Object, e As EventArgs) Handles btnEditPat.Click
        Try
            If PatientIDEdit.Text.Length = 0 Then Exit Sub
            If Not CheckTxt() Then Exit Sub

            Dim oldPatient = CurrentPatient

            Dim updated As New Patient With {
            .PatientID = oldPatient.PatientID,
            .Address = AddressTextEdit.Text,
            .PatientNumber = lblPNum.Text,
            .Age = AgeSpinEdit.Value,
            .BirthY = BirthYSpinEdit.Value,
            .Health = HealthTextBox.Text,
            .Implant = ImplantCheckBox.Checked,
            .Notes = NotesTextEdit.Text,
            .Mobile = MobileCheckBox.Checked,
            .Ortho = OrthoCheckBox.Checked,
            .Sex = SexTextBox.Text,
            .Diag = DiagCheck.Checked,
            .Struc = StrucCheckBox.Checked,
            .Treat = TreatCheckBox.Checked,
            .PatientName = PatientNameTextEdit.Text,
            .Phone = PhoneTextEdit.Text,
            .WhatsApp = WhatsAppTextEdit.Text
        }

            Me.Cursor = Cursors.WaitCursor
            btnEditPat.Enabled = False

            Try
                Dim pd As New PatientDATA()
                Dim updateResult = Await Task.Run(Function() pd.Update(oldPatient, updated)).ConfigureAwait(True)
                If updateResult Then
                    'Dim nav2 = TryCast(FormManager.Instance.CurrentForm?.HeaderControl, Navigator2)
                    'If nav2 IsNot Nothing Then nav2.NotifyPatientUpdatedFromExternal(updated)
                End If
                If updateResult Then
                    PatientUpdated = True
                    Me.DialogResult = DialogResult.OK
                    Me.Close()
                Else
                    MsgBox(If(Eng, "Update operation failed. Please try again.", "فشلت عملية التحديث. يرجى المحاولة مرة أخرى."), MsgBoxStyle.Exclamation, If(Eng, "Update Failed", "فشل التحديث"))
                    Me.DialogResult = DialogResult.None
                End If
            Finally
                Me.Cursor = Cursors.Default
                btnEditPat.Enabled = True
            End Try

        Catch ex As Exception
            MsgBox($"Error updating patient: {ex.Message}", MsgBoxStyle.Critical, "Error")
        End Try
    End Sub


    'Private Sub btnEditPat_Click(sender As Object, e As EventArgs) Handles btnEditPat.Click
    '    Try
    '        If PatientIDEdit.Text.Length = 0 Then Exit Sub
    '        If CheckTxt() Then
    '            Dim clsPatientData As New PatientDATA
    '            Dim oldPatient = CurrentPatient ' clsPatientData.Select_RecordByID(CInt(Val(PatientIDEdit.Text)))
    '            Dim clsPatient As New Patient With {.PatientID = oldPatient.PatientID,
    '                .Address = AddressTextEdit.Text,
    '                .PatientNumber = lblPNum.Text,
    '                .Age = AgeSpinEdit.Value,
    '                .BirthY = BirthYSpinEdit.Value,
    '                .Health = HealthTextBox.Text,
    '                .Implant = ImplantCheckBox.Checked,
    '                .Notes = NotesTextEdit.Text,
    '                .Mobile = MobileCheckBox.Checked,
    '                .Ortho = OrthoCheckBox.Checked,
    '                .Sex = SexTextBox.Text,
    '                .Struc = StrucCheckBox.Checked,
    '                .Treat = TreatCheckBox.Checked,
    '                .PatientName = PatientNameTextEdit.Text,
    '                .Phone = PhoneTextEdit.Text}
    '            If clsPatientData.Update(oldPatient, clsPatient) Then
    '                MsgBox("Changes Have Been Saved")
    '                PatientUpdated = True
    '                CurrentPatient = clsPatient
    '                If FormManager.Instance.IsBasePatientFormOpen Then
    '                    FormManager.Instance.CurrentForm.HeaderControl.UpdateCurrentPatient(clsPatient)
    '                End If
    '                Me.DialogResult = DialogResult.OK
    '                Me.Close()
    '            Else
    '                PatientUpdated = False
    '                MsgBox("Changes Have Not Been Saved")
    '            End If
    '            ' In your form or business logic
    '            Try

    '                Dim message As String = If(Eng,
    '                    $"Successfully updated patient number: {oldPatient.PatientNumber}",
    '                    $"تم تحديث بيانات المريض رقم {oldPatient.PatientNumber}  بنجاح")
    '                MessageBox.Show(message,
    '                               If(Eng, "Update Complete", "اكتمل التحديث"),
    '                               MessageBoxButtons.OK,
    '                               MessageBoxIcon.Information)
    '            Catch ex As Exception
    '                Dim errorMessage As String = If(Eng,
    '                    $"Error updating patient numbers: {ex.Message}",
    '                    $"خطأ في تحديث أرقام المرضى: {ex.Message}")
    '                MessageBox.Show(errorMessage,
    '                               If(Eng, "Update Error", "خطأ في التحديث"),
    '                               MessageBoxButtons.OK,
    '                               MessageBoxIcon.Error)
    '            End Try
    '        End If
    '    Catch ex As Exception
    '        MsgBox(ex.Message)
    '    End Try
    'End Sub
    'Private Sub btnEditPat_Click(sender As Object, e As EventArgs) Handles btnEditPat.Click
    '    Try
    '        If PatientIDEdit.Text.Length = 0 Then Exit Sub
    '        If Not CheckTxt() Then Exit Sub

    '        Dim oldPatient = CurrentPatient

    '        Dim updated As New Patient With {
    '        .PatientID = oldPatient.PatientID,
    '        .Address = AddressTextEdit.Text,
    '        .PatientNumber = lblPNum.Text,
    '        .Age = AgeSpinEdit.Value,
    '        .BirthY = BirthYSpinEdit.Value,
    '        .Health = HealthTextBox.Text,
    '        .Implant = ImplantCheckBox.Checked,
    '        .Notes = NotesTextEdit.Text,
    '        .Mobile = MobileCheckBox.Checked,
    '        .Ortho = OrthoCheckBox.Checked,
    '        .Sex = SexTextBox.Text,
    '        .Struc = StrucCheckBox.Checked,
    '        .Treat = TreatCheckBox.Checked,
    '        .PatientName = PatientNameTextEdit.Text,
    '        .Phone = PhoneTextEdit.Text
    '    }

    '        ' ❗ Send it to the mutation engine (HDRTEST owns _mutationEngine)
    '        Dim unused = FormManager.Instance.CurrentForm.HeaderControl._mutationEngine.UpdatePatientAsync(oldPatient, updated)
    '        ' Close immediately — UI will update after callback
    '        Me.DialogResult = DialogResult.OK
    '        Me.Close()

    '    Catch ex As Exception
    '        MsgBox(ex.Message)
    '    End Try
    'End Sub

    Property PatientDeleted As Boolean = False
    Private Async Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        Try
            Try
                Dim msg As String = If(Eng,
            $"Are you sure you want to delete this Patient?{vbCrLf}{CurrentPatient.PatientName}{vbCrLf}File Number : {CurrentPatient.PatientNumber}{vbCrLf} This Operation Is PERMANENT... It Can't Be UNDONE!!",
            $"هل انت متاكد من حذف هذا المريض؟{vbCrLf}{CurrentPatient.PatientName}{vbCrLf} صاحب ملف رقم : {CurrentPatient.PatientNumber}{vbCrLf}هذا الإجراء نهائي... لا يمكن التراجع!")
                Dim title As String = If(Eng, "Warning... Patient DELETE!!!", "تحذير... حذف مريض!")
                Dim response As MsgBoxResult = MsgBox(msg, MsgBoxStyle.DefaultButton2 Or MsgBoxStyle.Critical Or MsgBoxStyle.YesNo, title)
                ' First confirmation (standard MessageBox)
                If response <> DialogResult.Yes Then

                    Me.DialogResult = DialogResult.Cancel
                    Return
                End If
                ' Second confirmation (custom dialog)
                Using confirmDialog As New DoubleConfirmDialog() With {.Text = "Patient Record  Deletion From"}
                    confirmDialog.Message = "FINAL WARNING: This will permanently delete { " & CurrentPatient.PatientName & " } Record from your Archive?. Check the box to confirm."
                    If confirmDialog.ShowDialog(Me) <> DialogResult.OK OrElse Not confirmDialog.IsConfirmed Then
                        Return ' Exit if user cancels or doesn't check the box
                    End If
                End Using
                Dim deleteResult As Boolean
                Dim pd As New PatientDATA()
                Try
                    deleteResult = Await Task.Run(Function() pd.Delete(CurrentPatient)).ConfigureAwait(True)
                    If deleteResult Then
                        'Dim nav2 = TryCast(FormManager.Instance.CurrentForm?.HeaderControl, Navigator2)
                        'If nav2 IsNot Nothing Then nav2.NotifyPatientDeletedFromExternal(CurrentPatient.PatientID)
                    End If
                Catch ex As Exception
                    MsgBox($"Error deleting patient: {ex.Message}")
                    Return
                End Try
                If deleteResult Then
                    PatientDeleted = True
                    MsgBox(If(Eng, $"Patient {CurrentPatient.PatientName} has been deleted.", $"تم حذف المريض {CurrentPatient.PatientName}."), MsgBoxStyle.Information)
                    Me.DialogResult = DialogResult.OK
                    Me.Close()
                Else
                    MsgBox(If(Eng, "Delete operation failed.", "فشلت عملية الحذف."), MsgBoxStyle.Exclamation)
                End If

            Catch ex As SqlException
                MsgBox(ex.Message)
            End Try
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub



    Private Sub SexTextBox_EditValueChanged(sender As Object, e As EventArgs) Handles SexTextBox.EditValueChanged
        If String.IsNullOrEmpty(SexTextBox.Text) Then
            RadioMale.Checked = True
        ElseIf SexTextBox.Text = "Male" OrElse SexTextBox.Text = "ذكر" Then
            RadioMale.Checked = True
        ElseIf SexTextBox.Text = "Female" OrElse SexTextBox.Text = "أنثى" OrElse SexTextBox.Text = "انثى" Then
            RadioFemale.Checked = True
        End If
    End Sub
    Private Sub RadioMale_CheckedChanged(sender As Object, e As EventArgs) Handles RadioMale.CheckedChanged
        Dim maleEng, maleAr As String
        maleAr = "ذكر"
        maleEng = "Male"
        If RadioMale.Checked = True Then
            If Eng Then
                SexTextBox.Text = maleEng
            Else
                SexTextBox.Text = maleAr
            End If
        End If
    End Sub
    Private Sub RadioFemale_CheckedChanged(sender As Object, e As EventArgs) Handles RadioFemale.CheckedChanged
        Dim femEng, femAr As String
        femAr = "أنثى"
        femEng = "Female"
        If RadioFemale.Checked = True Then
            If Eng Then
                SexTextBox.Text = femEng
            Else
                SexTextBox.Text = femAr
            End If
        End If
    End Sub

#End Region





End Class