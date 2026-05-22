Imports System.Diagnostics
Imports System.Linq
Imports System.Threading.Tasks
Imports System.Windows.Forms
Imports Dapper
Imports System.Data.SqlClient
Imports DevExpress.XtraEditors
Imports System.ComponentModel.DataAnnotations

Public Class PatientAddEditForm
    Property _patient As Patient

    ''' <summary>Set after a successful insert when <see cref="DialogResult"/> is <see cref="DialogResult.OK"/>.</summary>
    Public Property LastInsertedPatient As Patient

    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
    End Sub

    Public Sub New(filter As String)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Select Case filter
            Case "Treat"
                TreatCheck.Checked = True
            Case "Implant"
                ImplntCheck.Checked = True
            Case "Mobile"
                MobileCheck.Checked = True
            Case "Ortho"
                OrthoCheck.Checked = True
            Case "Diag"
                DiagCheck.Checked = True
        End Select
    End Sub

    Private Sub PatientAddEditForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        WhatsHelper.FillCboPrefixOnce(cboPrefix)
        If cboPrefix IsNot Nothing Then cboPrefix.SelectedIndex = -1
        If txtWhats IsNot Nothing Then txtWhats.Text = ""
        RefreshLblWhats()
        If WhatsAppTextEdit IsNot Nothing Then WhatsAppTextEdit.Visible = False
    End Sub

    ' Then change InsertPatient to:
    Private Async Sub InsertPatient()

        Dim usr As Boolean = False
        If Not CheckTxt() Then Exit Sub
        If CurrentUser IsNot Nothing Then
            ' Use CurrentUser.UsID
            usr = True
        Else
            MsgBox("You are adding a patient without a user logged in, Default will be used.")
        End If

        If txtWhats IsNot Nothing AndAlso String.IsNullOrWhiteSpace(txtWhats.Text) AndAlso
           Not String.IsNullOrWhiteSpace(TxtPhone.Text) AndAlso TxtPhone.Text.Trim().StartsWith("05") Then
            txtWhats.Text = TxtPhone.Text.Trim()
        End If

        Dim prefixStored As String = WhatsHelper.GetPrefixTextForStorage(cboPrefix)
        Dim fullDigits As String = New String(GetFullWhatsNumber().Where(Function(ch) Char.IsDigit(ch)).ToArray())
        Dim prefixDigits As String = ""
        If cboPrefix IsNot Nothing AndAlso cboPrefix.EditValue IsNot Nothing Then
            prefixDigits = New String(cboPrefix.EditValue.ToString().Where(Function(ch) Char.IsDigit(ch)).ToArray())
        End If
        If Not String.IsNullOrWhiteSpace(fullDigits) Then
            Dim whatsErr As String = ValidateWhatsAppNumber(fullDigits, prefixDigits)
            If whatsErr <> "" Then
                MessageBox.Show(whatsErr, If(Eng, "WhatsApp", "واتساب"), MessageBoxButtons.OK, MessageBoxIcon.Warning)
                If txtWhats IsNot Nothing Then txtWhats.Focus()
                Return
            End If
        End If

        Dim localW As String = WhatsHelper.NormalizeLocalWhatsTenDigitsForStorage(If(txtWhats?.Text, "").ToString())

        Dim clsPatient As New Patient With {
        .PatientName = TxtName.Text,
        .Sex = TxtSex.Text,
        .Age = If(IsNumeric(TxtAge.Text), CInt(TxtAge.Text), CType(Nothing, Integer?)),
        .IsKid = If(IsNumeric(TxtAge.Text), CInt(TxtAge.Text) < 10, False),
        .Phone = TxtPhone.Text,
        .WhatsAppPrefix = prefixStored,
        .WhatsApp = localW,
        .Address = TxtAdrs.Text,
        .Health = TxtHealth.Text,
        .Treat = TreatCheck.Checked,
        .Implant = ImplntCheck.Checked,
        .Mobile = MobileCheck.Checked,
        .Ortho = OrthoCheck.Checked,
        .Diag = DiagCheck.Checked,
        .Struc = StrucCheck.Checked,
        .Notes = TxtNotes.Text,
        .BirthY = SpinYear.Value,
        .CreatedBy = If(usr, CurrentUser.UsID, 1),
        .CreateDate = Now
    }
        Dim pd As New PatientDATA()
        Dim code = Await Task.Run(Function() pd.InsertPatient(clsPatient)).ConfigureAwait(True)
        Dim addResult = code > 0
        If Not addResult Then
            Select Case code
                Case -2
                    MsgBox(If(Eng, "Patient with this name already exists.", "مريض بهذا الاسم موجود بالفعل."), MsgBoxStyle.Exclamation)
                Case -3
                    MsgBox(If(Eng, "Failed to generate patient number.", "فشل في إنشاء رقم المريض."), MsgBoxStyle.Exclamation)
                Case Else
                    MsgBox(If(Eng, "Failed to add patient.", "فشل في إضافة المريض."), MsgBoxStyle.Exclamation)
            End Select
            Return
        End If
        Dim newPatient As Patient = pd.Select_LastPatient()
        If newPatient Is Nothing OrElse String.IsNullOrWhiteSpace(newPatient.PatientNumber) Then
            MsgBox(If(Eng, "Patient added but could not load.", "تمت الإضافة لكن تعذر تحميل البيانات."), MsgBoxStyle.Exclamation)
            Return
        End If
        LastInsertedPatient = newPatient
        DialogResult = DialogResult.OK
        Close()
    End Sub

    Private Function CheckTxt() As Boolean
        Dim errorMessages As New List(Of String)()
        ' Check Patient Name
        If String.IsNullOrWhiteSpace(TxtName.Text) Then
            errorMessages.Add(If(Eng, "Patient name is required", "اسم المريض مطلوب"))
            TxtName.Focus()
            ' Check Age
        ElseIf SpinAge.Value <= 0 Then
            Dim message As String = If(SpinAge.Value = 0,
                                If(Eng, "Age cannot be zero", "العمر لا يمكن أن يكون صفر"),
                                If(Eng, "Age cannot be negative", "العمر لا يمكن أن يكون سالب"))
            errorMessages.Add(message)
            SpinAge.Focus()
            ' Check Age
        ElseIf SpinAge.Value >= 130 Then
            Dim message As String = If(Eng, "Age unreasonable", "العمر غير منطقي")
            errorMessages.Add(message)
            SpinAge.Focus()
            '    ' Check Health
            'ElseIf String.IsNullOrWhiteSpace(TxtHealth.Text) Then
            '    errorMessages.Add(If(Eng, "Health information is required", "المعلومات الصحية مطلوبة"))
            '    TxtHealth.Focus()
            '    ' Check Address
            'ElseIf String.IsNullOrWhiteSpace(TxtAdrs.Text) Then
            '    errorMessages.Add(If(Eng, "Address is required", "العنوان مطلوب"))
            '    TxtAdrs.Focus()
        End If
        ' Check Age
        ' Show error message if any validation failed
        If errorMessages.Count > 0 Then
            Dim messageTitle As String = If(Eng, "Validation Error", "خطأ في التحقق")
            Dim fullMessage As String = String.Join(vbCrLf, errorMessages)
            MessageBox.Show(fullMessage, messageTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return False
        End If
        Return True
    End Function
    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        Dim sw As New Stopwatch
        sw.Start()
        If CheckTxt() Then
            InsertPatient()
        End If
        sw.Stop()
        LogToFile(" InsertPatient() spent: " & sw.Elapsed.TotalSeconds.ToString("F2") & " seconds", Me)
    End Sub
    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.DialogResult = DialogResult.Cancel
        Me.Close()
    End Sub

#Region "Whats"
    Private Function GetFullWhatsNumber() As String
        Dim number As String = ""
        If txtWhats IsNot Nothing AndAlso txtWhats.Text IsNot Nothing Then
            number = txtWhats.Text.ToString().Trim()
        End If
        Dim localDigits As String = New String(number.Where(Function(ch) Char.IsDigit(ch)).ToArray())

        While localDigits.StartsWith("0"c) AndAlso localDigits.Length > 0
            localDigits = localDigits.Substring(1)
        End While

        If localDigits.Length > 9 Then
            localDigits = localDigits.Substring(localDigits.Length - 9, 9)
        End If

        If cboPrefix Is Nothing OrElse cboPrefix.EditValue Is Nothing Then
            Return localDigits
        End If
        Dim rawPrefix As String = cboPrefix.EditValue.ToString()
        Dim prefixDigits As String = New String(rawPrefix.Where(Function(ch) Char.IsDigit(ch)).ToArray())
        If String.IsNullOrWhiteSpace(prefixDigits) Then Return localDigits
        If localDigits.Length = 0 Then Return ""
        Return prefixDigits & localDigits
    End Function

    Private Sub RefreshLblWhats()
        If lblWhats IsNot Nothing Then
            lblWhats.Text = GetFullWhatsNumber()
        End If
    End Sub

    Private Sub cboPrefix_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboPrefix.SelectedIndexChanged
        RefreshLblWhats()
    End Sub

    Private Sub txtWhats_ValueChanged(sender As Object, e As EventArgs) Handles txtWhats.EditValueChanged
        RefreshLblWhats()
    End Sub

    Private Sub txtWhats_KeyDown(sender As Object, e As KeyEventArgs) Handles txtWhats.KeyDown
        If e.KeyCode = Keys.Back OrElse
           e.KeyCode = Keys.Delete OrElse
           e.KeyCode = Keys.Left OrElse
           e.KeyCode = Keys.Right OrElse
           e.KeyCode = Keys.Up OrElse
           e.KeyCode = Keys.Down OrElse
           e.KeyCode = Keys.Tab OrElse
           e.KeyCode = Keys.Home OrElse
           e.KeyCode = Keys.End Then
            Return
        End If

        Dim isTopRowDigit As Boolean = (e.KeyCode >= Keys.D0 AndAlso e.KeyCode <= Keys.D9)
        Dim isNumPadDigit As Boolean = (e.KeyCode >= Keys.NumPad0 AndAlso e.KeyCode <= Keys.NumPad9)

        If (isTopRowDigit OrElse isNumPadDigit) AndAlso (Not e.Shift) Then
            Return
        End If

        e.SuppressKeyPress = True
        e.Handled = True
    End Sub

    Private Function ValidateWhatsAppNumber(fullNumberDigits As String, prefixDigits As String) As String
        If String.IsNullOrWhiteSpace(fullNumberDigits) Then
            Return If(Eng, "Enter WhatsApp/phone number (digits only).", "أدخل رقم واتساب/الجوال (أرقام فقط).")
        End If
        If fullNumberDigits.Any(Function(c) Not Char.IsDigit(c)) Then
            Return If(Eng, "Number must contain only digits (no spaces, dashes or plus sign).", "يجب أن يحتوي الرقم على أرقام فقط (بدون مسافات أو شرطات أو +).")
        End If

        If String.IsNullOrWhiteSpace(prefixDigits) Then
            If fullNumberDigits.Length < 10 OrElse fullNumberDigits.Length > 15 Then
                Return If(Eng, "Number must be 10–15 digits (e.g. 970599123456 for Palestine).", "يجب أن يكون الرقم 10–15 رقمًا (مثلاً 970599123456 لفلسطين).")
            End If
            Return ""
        End If

        Dim prefixLen As Integer = prefixDigits.Length
        Dim expectedLen As Integer = prefixLen + 9
        If fullNumberDigits.Length <> expectedLen Then
            Dim msgEn As String = $"Invalid length. For prefix +{prefixDigits} use {prefixLen} (prefix) + 9 digits (number without leading 0) = {expectedLen} digits total. Current: {fullNumberDigits.Length}."
            Dim msgAr As String = $"طول غير صحيح. لرمز +{prefixDigits} استخدم {prefixLen} (الرمز) + 9 أرقام (الرقم بدون صفر في البداية) = {expectedLen} رقمًا. الحالي: {fullNumberDigits.Length}."
            Return If(Eng, msgEn, msgAr)
        End If
        Return ""
    End Function
#End Region

#Region "Cbo's"
    Private Sub SpinAge_EditValueChanged(sender As Object, e As EventArgs) Handles SpinAge.EditValueChanged
        TxtAge.Text = SpinAge.Value
        SpinYear.Value = Date.Now.Year - CInt(TxtAge.Text)
    End Sub
    Private Sub SpinYear_ValueChanged(ByVal sender As Object, ByVal e As EventArgs) Handles SpinYear.ValueChanged
        TxtAge.Text = Date.Now.Year - CInt(SpinYear.Value)
        SpinAge.Value = CInt(TxtAge.Text)
    End Sub
    Private Sub TxtPhone_KeyPress(ByVal sender As Object, ByVal e As KeyPressEventArgs) Handles TxtPhone.KeyPress
        If e.KeyChar = ChrW(Keys.Delete) Then Exit Sub
        If e.KeyChar = ChrW(Keys.Back) Then Exit Sub
        If (Char.IsNumber(e.KeyChar) = False) Then e.Handled = True
    End Sub
    Private Sub TxtAge_TextChanged(sender As Object, e As EventArgs) Handles TxtAge.TextChanged
        'TxtAge.Text = Date.Now.Year - CInt(CboYear.Text)
        If TxtAge.Text.Length > 0 Then
            SpinYear.Value = Date.Now.Year - CInt(TxtAge.Text)
            SpinAge.Value = CInt(TxtAge.Text)
        End If
    End Sub
    Private Sub TxtAge_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TxtAge.KeyPress
        If e.KeyChar = ChrW(Keys.Delete) Then Exit Sub
        If e.KeyChar = ChrW(Keys.Back) Then Exit Sub
        If (Char.IsNumber(e.KeyChar) = False) Then e.Handled = True
    End Sub
    Private Sub RadioMale_CheckedChanged(sender As Object, e As EventArgs) Handles RadioMale.CheckedChanged
        If RadioMale.Checked = True Then
            TxtSex.Text = RadioMale.Text
        End If
    End Sub
    Private Sub RadioFemale_CheckedChanged(sender As Object, e As EventArgs) Handles RadioFemale.CheckedChanged
        If RadioFemale.Checked = True Then
            TxtSex.Text = RadioFemale.Text
        End If
    End Sub
    Private Sub CboCity_CityValueChanged(sender As Object, e As CityCombo.CityIndexChangedEvent) Handles CboCity.CityValueChanged
        If CboCity.CityID = -1 Then
            TxtAdrs.Text = ""
        Else
            TxtAdrs.Text = CboCity.CityName
        End If
    End Sub
    Private Sub CboHealth_SelectedIndexChanged(sender As Object, e As HlthCombo.HealthIndexChangedEvent) Handles CboHealth.HealthValueChanged
        If CboHealth.HID = -1 Then
            TxtHealth.Text = ""
        Else
            TxtHealth.Text = CboHealth.HealthStat
        End If
    End Sub
    Private Sub TxtSex_EditValueChanged(sender As Object, e As EventArgs) Handles TxtSex.EditValueChanged
        Try
            If Me.TxtSex.Text = "ذكر" Or Me.TxtSex.Text = "Male" Then
                Me.PicBox.Image = My.Resources.Male
            ElseIf Me.TxtSex.Text = "أنثى" Or Me.TxtSex.Text = "Female" Or Me.TxtSex.Text = "انثى" Then
                Me.PicBox.Image = My.Resources.Female
            Else
                Me.PicBox.Image = My.Resources.delete_16x16
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
#End Region
End Class


'Private Sub btnSave_Click(sender As Object, e As EventArgs)
'    Try
'        Dim sw As New Stopwatch
'        sw.Start()
'        If CheckTxt() Then
'            InsertPatient()
'        End If
'        sw.Stop()
'        LogToFile(" InsertPatient() spent: " & sw.Elapsed.TotalSeconds.ToString("F2") & " seconds", Me)
'    Catch ex As Exception
'        MsgBox("Error: " & ex.Message)
'    End Try
'End Sub
'Property Deleted As Boolean = False
'Private Sub btnDelete_Click(sender As Object, e As EventArgs)
'    Try
'        Dim msg As String = If(Eng,
'        $"Are you sure you want to delete this Patient?{vbCrLf}{_patient.PatientName}{vbCrLf}File Number : {_patient.PatientNumber}{vbCrLf} This Operation Is PERMANENT... It Can't Be UNDONE!!",
'        $"هل انت متاكد من حذف هذا المريض؟{vbCrLf}{_patient.PatientName}{vbCrLf} صاحب ملف رقم : {_patient.PatientNumber}{vbCrLf}هذا الإجراء نهائي... لا يمكن التراجع!")
'        Dim title As String = If(Eng, "Warning... Patient DELETE!!!", "تحذير... حذف مريض!")
'        Dim response As MsgBoxResult = MsgBox(msg, MsgBoxStyle.DefaultButton2 Or MsgBoxStyle.Critical Or MsgBoxStyle.YesNo, title)
'        ' First confirmation (standard MessageBox)
'        If response <> DialogResult.Yes Then
'            Deleted = False
'            Me.DialogResult = DialogResult.Cancel
'            Return
'        End If
'        ' Second confirmation (custom dialog)
'        Using confirmDialog As New DoubleConfirmDialog() With {.Text = "Patient Record  Deletion From"}
'            confirmDialog.Message = "FINAL WARNING: This will permanently delete { " & TxtName.Text & " } Record from your Archive?. Check the box to confirm."
'            If confirmDialog.ShowDialog(Me) <> DialogResult.OK OrElse Not confirmDialog.IsConfirmed Then
'                Return ' Exit if user cancels or doesn't check the box
'            End If
'        End Using
'        Using conn As New SqlConnection(My.Settings.DentistXConnectionString)
'            Dim rowsAffected As Integer = conn.Execute("dbo.PatientDelete", New With {Key .PatientID = _patient.PatientID}, commandType:=CommandType.StoredProcedure)
'            If rowsAffected > 0 Then
'                MsgBox(If(Eng, $"Patient {_patient.PatientName} Has Been Deleted!!!", $"المريض {_patient.PatientName} تم حذفه!!!"))
'                Deleted = True
'                'Dim Index As Integer = -1
'                'Dim BS As BindingSource = Nothing
'                'If MainView3.baseForm IsNot Nothing Then
'                '    BS = MainView3.baseForm.ThinHDRChrtNew1.PatientBS

'                '    ' Find index by PatientID
'                '    For i As Integer = 0 To BS.Count - 1
'                '        Dim p As Patient = TryCast(BS(i), Patient)
'                '        If p IsNot Nothing AndAlso p.PatientID = _patient.PatientID Then
'                '            Index = i
'                '            Exit For
'                '        End If
'                '    Next
'                '    If BS IsNot Nothing AndAlso Index >= 0 Then
'                '        BS.RemoveAt(Index)
'                '    End If
'                'End If
'                Me.DialogResult = DialogResult.OK
'                Me.Close()
'            Else
'                MsgBox("Delete operation failed.")
'                Deleted = False
'            End If
'        End Using

'    Catch ex As SqlException
'        MsgBox(ex.Message)
'    End Try
'End Sub


'' This is your method where you want to call the stored procedure.
'' "Procedure or function PatientInsert has too many arguments specified."
'Private Sub InsertPatient()
'    Try
'        Dim userId As Integer = 1
'        If CurrentUser Is Nothing Then
'            MsgBox("You are adding a patient without a user logged in, Default will be used.")
'        Else
'            userId = CurrentUser.UsID
'        End If
'        Dim clsPatient As New Patient With {
'        .PatientName = TxtName.Text,
'        .Sex = TxtSex.Text,
'        .Age = If(IsNumeric(TxtAge.Text), CInt(TxtAge.Text), CType(Nothing, Integer?)),
'        .IsKid = .Age < 10,
'        .Phone = TxtPhone.Text,
'        .Address = TxtAdrs.Text,
'        .Health = TxtHealth.Text,
'        .Treat = TreatCheck.Checked,
'        .Implant = ImplntCheck.Checked,
'        .Mobile = MobileCheck.Checked,
'        .Ortho = OrthoCheck.Checked,
'        .Struc = StrucCheck.Checked,
'        .Notes = TxtNotes.Text,
'        .BirthY = SpinYear.Value,
'        .CreatedBy = userId,
'        .CreateDate = Now.Date
'    }

'        Using conn As New SqlConnection(My.Settings.DentistXConnectionString)
'            'Dim parameters = New DynamicParameters(patient)
'            'parameters.Add("@ReturnValue", dbType:=DbType.Int32, direction:=ParameterDirection.Output)
'            Dim parameters As New DynamicParameters()
'            parameters.Add("@PatientName", TxtName.Text)
'            parameters.Add("@Sex", TxtSex.Text)
'            parameters.Add("@Age", If(IsNumeric(TxtAge.Text), CInt(TxtAge.Text), CType(Nothing, Integer?)))
'            parameters.Add("@Phone", TxtPhone.Text)
'            parameters.Add("@Address", TxtAdrs.Text)
'            parameters.Add("@Health", TxtHealth.Text)
'            parameters.Add("@Treat", TreatCheck.Checked)
'            parameters.Add("@Implant", ImplntCheck.Checked)
'            parameters.Add("@Mobile", MobileCheck.Checked)
'            parameters.Add("@Ortho", OrthoCheck.Checked)
'            parameters.Add("@Struc", StrucCheck.Checked)
'            parameters.Add("@Notes", TxtNotes.Text)
'            parameters.Add("@BirthY", SpinYear.Value)
'            parameters.Add("@CreatedBy", 1)
'            parameters.Add("@CreateDate", Now.Date)
'            parameters.Add("@ReturnValue", dbType:=DbType.Int32, direction:=ParameterDirection.Output)
'            conn.Execute("dbo.PatientInsert", parameters, commandType:=CommandType.StoredProcedure)
'            ' Retrieve output value
'            Dim x As Integer = parameters.Get(Of Integer)("@ReturnValue")
'            ' Handle result
'            Select Case x
'                Case 18
'                    'if Eng Then english else arabic
'                    MsgBox(If(Eng, "Patient inserted successfully.", "تم إدخال المريض بنجاح."))
'                    Dim p As New PatientDATA
'                    Dim newPatient = p.Select_LastPatient ' Make sure this returns the complete patient with ID
'                    _patient = newPatient
'                    ' Update ALL data sources
'                    'FormManager.Instance.CurrentForm.HeaderControl.AddNewPatient(newPatient)

'                    '' Set current references
'                    'PasswordSecurity.CurrentPatient = newPatient
'                    'CurrentPatient = newPatient

'                    'PatientID = newPatient.PatientID

'                    Me.DialogResult = DialogResult.OK
'                    Me.Close()
'                Case -2
'                    MsgBox("Patient Already Exists.")
'                Case Else
'                    MsgBox("An unexpected value was returned: " & x)
'            End Select
'        End Using
'    Catch ex As Exception
'        MsgBox("Error: " & ex.Message)
'    End Try
'End Sub
