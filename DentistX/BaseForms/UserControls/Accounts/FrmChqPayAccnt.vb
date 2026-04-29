Imports System.Collections.Generic
Imports System.Data.SqlClient
Imports System.Diagnostics
Imports System.Drawing
Imports System.IO
Imports System.Linq
Imports System.Text
Imports System.Windows.Forms
Imports Dapper
Imports TwainLib

Public Class FrmChqPayAccnt

    ''' <summary>How scan/browse maps pages/files to cheque images before PayID exists.</summary>
    Private Enum ChequeScanSheetMode
        OneSide = 0
        FrontBack = 1
    End Enum

    Private _suppressChkDrive As Boolean
    Private _suppressRadioScanModeEvents As Boolean
    ''' <summary>Cheque image file to copy into Pay{id}_Chq… when user clicks Add (set by Scan/Browse before payment exists).</summary>
    Private _pendingChequeImageSourcePath As String = Nothing
    ''' <summary>True if <see cref="_pendingChequeImageSourcePath"/> is a disposable scan under the patient folder (safe to delete on Cancel or after copy).</summary>
    Private _pendingChequeImageIsDisposableScan As Boolean
    Private _pendingChequeImageSecondPath As String = Nothing
    Private _pendingChequeImageSecondIsDisposableScan As Boolean
    ''' <summary>Non–cheque-folder files (PDF etc.) to copy to Attachments\Patient{id}_Pay{PayID}_Chq_* after insert.</summary>
    Private ReadOnly _pendingAttachmentExtraPaths As New List(Of String)
    ''' <summary>After a successful scan/browse link, Scan/Browse/Scan&amp;Pay/Reset are disabled until Add or Save (and Cancel) complete.</summary>
    Private _chequeCaptureUiLocked As Boolean
    Private _patientId As Integer
    Private _trtId As Integer
    Private _treatDetail As String
    Private _payType As String
    Private _pay As Patient_Pays
    Public Property Treats As Patient_Trts

    Private Shared ReadOnly _chqUiFont As New Font("Calibri", 10.0F, FontStyle.Bold)

    Private Function ShowChqMessage(text As String, caption As String, buttons As MessageBoxButtons, icon As MessageBoxIcon) As DialogResult
        Using f As New Form()
            f.Text = caption
            f.FormBorderStyle = FormBorderStyle.FixedDialog
            f.StartPosition = FormStartPosition.CenterParent
            f.MinimizeBox = False
            f.MaximizeBox = False
            f.ShowInTaskbar = False
            If Not Eng Then f.RightToLeft = RightToLeft.Yes
            Dim leftPad = 12
            Dim topPad = 12
            Dim iconW = 0
            Dim iconPic As PictureBox = Nothing
            If icon <> MessageBoxIcon.None Then
                Dim ic As Icon = SystemIcons.Information
                Select Case icon
                    Case MessageBoxIcon.Warning
                        ic = SystemIcons.Warning
                    Case MessageBoxIcon.Error
                        ic = SystemIcons.Error
                    Case MessageBoxIcon.Information
                        ic = SystemIcons.Information
                    Case Else
                        ic = SystemIcons.Information
                End Select
                iconPic = New PictureBox With {
                    .Image = ic.ToBitmap(),
                    .SizeMode = PictureBoxSizeMode.StretchImage,
                    .Size = New Size(32, 32),
                    .Location = New Point(leftPad, topPad)
                }
                iconW = 44
                f.Controls.Add(iconPic)
            End If
            Dim lbl As New Label With {
                .AutoSize = True,
                .MaximumSize = New Size(400, 0),
                .Font = _chqUiFont,
                .Text = text,
                .Location = New Point(leftPad + iconW, topPad + 4)
            }
            f.Controls.Add(lbl)
            lbl.PerformLayout()
            Dim btnY = Math.Max(lbl.Bottom, If(iconPic IsNot Nothing, iconPic.Bottom, topPad)) + 16
            Dim btnPanel As New FlowLayoutPanel With {
                .FlowDirection = FlowDirection.LeftToRight,
                .AutoSize = True,
                .WrapContents = False,
                .Location = New Point(leftPad, btnY)
            }
            If buttons = MessageBoxButtons.OK Then
                Dim b As New Button With {.Text = If(Eng, "OK", "موافق"), .Font = _chqUiFont, .DialogResult = DialogResult.OK, .AutoSize = True, .Padding = New Padding(10, 4, 10, 4)}
                btnPanel.Controls.Add(b)
                f.AcceptButton = b
            ElseIf buttons = MessageBoxButtons.OKCancel Then
                Dim bOk As New Button With {.Text = If(Eng, "OK", "موافق"), .Font = _chqUiFont, .DialogResult = DialogResult.OK, .AutoSize = True, .Padding = New Padding(10, 4, 10, 4)}
                Dim bCancel As New Button With {.Text = If(Eng, "Cancel", "إلغاء"), .Font = _chqUiFont, .DialogResult = DialogResult.Cancel, .AutoSize = True, .Padding = New Padding(10, 4, 10, 4)}
                btnPanel.Controls.Add(bCancel)
                btnPanel.Controls.Add(bOk)
                f.AcceptButton = bOk
                f.CancelButton = bCancel
            ElseIf buttons = MessageBoxButtons.YesNo Then
                Dim bYes As New Button With {.Text = If(Eng, "Yes", "نعم"), .Font = _chqUiFont, .DialogResult = DialogResult.Yes, .AutoSize = True, .Padding = New Padding(10, 4, 10, 4)}
                Dim bNo As New Button With {.Text = If(Eng, "No", "لا"), .Font = _chqUiFont, .DialogResult = DialogResult.No, .AutoSize = True, .Padding = New Padding(10, 4, 10, 4)}
                btnPanel.Controls.Add(bNo)
                btnPanel.Controls.Add(bYes)
                f.AcceptButton = bYes
                f.CancelButton = bNo
            End If
            f.Controls.Add(btnPanel)
            f.PerformLayout()
            Dim totalW = Math.Max(lbl.Right, btnPanel.Right) + 24
            Dim totalH = btnPanel.Bottom + 20
            If totalW < 360 Then totalW = 360
            f.ClientSize = New Size(totalW, totalH)
            btnPanel.Left = Math.Max(leftPad, f.ClientSize.Width - btnPanel.Width - 12)
            Return f.ShowDialog(Me)
        End Using
    End Function

    Private Sub ShowChqOverwriteCancelled()
        ShowChqMessage(
            If(Eng, "Replace was cancelled. The existing file was not changed.", "تم إلغاء الاستبدال. لم يُغيّر الملف الحالي."),
            If(Eng, "Cheques", "الشيكات"),
            MessageBoxButtons.OK,
            MessageBoxIcon.Information)
    End Sub

    ''' <summary>Returns True if user confirms replacing the existing linked file; False if cancelled.</summary>
    Private Function ConfirmOverwriteChequeLinked(existingDestPath As String, newSourcePath As String, face As ChequeImageFace) As Boolean
        If String.IsNullOrWhiteSpace(existingDestPath) OrElse Not File.Exists(existingDestPath) Then Return True
        Dim sideTxt = If(face = ChequeImageFace.Front,
            If(Eng, "Front", "أمام"),
            If(Eng, "Back", "خلف"))
        Dim msg = If(Eng,
            "A linked cheque image already exists for this payment (" & sideTxt & ")." & vbCrLf & "Replace it with the new scan or file?" & vbCrLf & vbCrLf & "Existing file:" & vbCrLf,
            "يوجد بالفعل صورة مربوطة لهذه الدفعة (" & sideTxt & ")." & vbCrLf & "هل تستبدلها بالمسح أو الملف الجديد؟" & vbCrLf & vbCrLf & "الملف الحالي:" & vbCrLf)
        msg &= existingDestPath & vbCrLf & vbCrLf &
            If(Eng, "New source:" & vbCrLf, "المصدر الجديد:" & vbCrLf) & newSourcePath

        Dim previewImg As Image = Nothing
        Using f As New Form()
            f.Text = If(Eng, "Confirm replace", "تأكيد الاستبدال")
            f.FormBorderStyle = FormBorderStyle.FixedDialog
            f.StartPosition = FormStartPosition.CenterParent
            f.MinimizeBox = False
            f.MaximizeBox = False
            f.ShowInTaskbar = False
            If Not Eng Then f.RightToLeft = RightToLeft.Yes
            Dim lbl As New Label With {
                .AutoSize = True,
                .MaximumSize = New Size(440, 0),
                .Font = _chqUiFont,
                .Text = msg,
                .Location = New Point(12, 12)
            }
            f.Controls.Add(lbl)
            lbl.PerformLayout()
            f.PerformLayout()
            Dim pic As New PictureBox With {
                .BorderStyle = BorderStyle.FixedSingle,
                .SizeMode = PictureBoxSizeMode.Zoom,
                .Location = New Point(12, lbl.Bottom + 8),
                .Size = New Size(360, 200),
                .BackColor = Color.White
            }
            Try
                previewImg = Image.FromFile(existingDestPath)
                pic.Image = previewImg
            Catch
                Dim lblPrev As New Label With {
                    .Font = _chqUiFont,
                    .Text = If(Eng, "Preview not available for this file type.", "المعاينة غير متاحة لهذا النوع."),
                    .AutoSize = False,
                    .TextAlign = ContentAlignment.MiddleCenter,
                    .Dock = DockStyle.Fill
                }
                pic.Controls.Add(lblPrev)
            End Try
            f.Controls.Add(pic)
            pic.PerformLayout()
            Dim btnPanel As New FlowLayoutPanel With {
                .FlowDirection = FlowDirection.LeftToRight,
                .AutoSize = True,
                .WrapContents = False,
                .Location = New Point(12, pic.Bottom + 12),
                .Padding = New Padding(0)
            }
            Dim btnYes As New Button With {
                .Text = If(Eng, "Replace", "استبدال"),
                .Font = _chqUiFont,
                .DialogResult = DialogResult.Yes,
                .AutoSize = True,
                .Margin = New Padding(6),
                .Padding = New Padding(12, 6, 12, 6)
            }
            Dim btnNo As New Button With {
                .Text = If(Eng, "Cancel", "إلغاء"),
                .Font = _chqUiFont,
                .DialogResult = DialogResult.No,
                .AutoSize = True,
                .Margin = New Padding(6),
                .Padding = New Padding(12, 6, 12, 6)
            }
            btnPanel.Controls.Add(btnNo)
            btnPanel.Controls.Add(btnYes)
            f.Controls.Add(btnPanel)
            f.AcceptButton = btnYes
            f.CancelButton = btnNo
            f.PerformLayout()
            Dim totalW = Math.Max(384, Math.Max(lbl.Right, pic.Right) + 24)
            Dim totalH = btnPanel.Bottom + 20
            f.ClientSize = New Size(totalW, totalH)
            btnPanel.Left = Math.Max(12, f.ClientSize.Width - btnPanel.Width - 12)
            AddHandler f.FormClosed, Sub()
                                         If pic.Image IsNot Nothing AndAlso pic.Image Is previewImg Then pic.Image = Nothing
                                         If previewImg IsNot Nothing Then
                                             previewImg.Dispose()
                                             previewImg = Nothing
                                         End If
                                     End Sub
            Return f.ShowDialog(Me) = DialogResult.Yes
        End Using
    End Function

    Private Sub DeleteTwainScanSources(files As List(Of String))
        If files Is Nothing Then Return
        For Each fp In files
            If String.IsNullOrWhiteSpace(fp) Then Continue For
            Try
                File.Delete(fp)
            Catch
            End Try
        Next
    End Sub

    Private Function GetChequeDriveOwnerLabel() As String
        If _patientId > 0 Then
            Dim fromDb = PatientDriveMirror.TryGetPatientNameForDriveFolder(_patientId)
            If Not String.IsNullOrWhiteSpace(fromDb) Then Return fromDb
        End If
        Dim n = GetDefaultChequeOwnerDisplayName().Trim()
        If Not String.IsNullOrWhiteSpace(n) Then Return n
        If _patientId > 0 Then Return "Patient_" & _patientId.ToString()
        Return "Patient"
    End Function

    Private Sub ChkDrive_CheckedChanged(sender As Object, e As EventArgs) Handles ChkDrive.CheckedChanged, chkSkipFields.CheckedChanged
        If _suppressChkDrive Then Return
        If Not ChkDrive.Checked Then Return
        If Not PatientDriveMirror.TryConfirmCloudPatientFiles(Me) Then
            _suppressChkDrive = True
            ChkDrive.Checked = False
            _suppressChkDrive = False
        End If
    End Sub

    Private Function GetDefaultChequeOwnerDisplayName() As String
        Dim p As Patient = PasswordSecurity.CurrentPatient
        If p IsNot Nothing AndAlso p.PatientID = _patientId Then
            Return If(p.PatientName, "")
        End If
        If FormManager.Instance IsNot Nothing Then
            p = FormManager.Instance.GetCurrentPatient()
            If p IsNot Nothing AndAlso p.PatientID = _patientId Then
                Return If(p.PatientName, "")
            End If
        End If
        If PasswordSecurity.PatientID = _patientId AndAlso Not String.IsNullOrWhiteSpace(PasswordSecurity.PatientName) Then
            Return PasswordSecurity.PatientName.Trim()
        End If
        Return ""
    End Function

    ''' <summary>Load form for adding a new payment (Cheque, Insurance, or Other). payType: "Cheque", "Insurance", or other string. Call before ShowDialog.</summary>
    Public Sub LoadForAdd(patientId As Integer, trtId As Integer, treatDetail As String, payType As String)
        _patientId = patientId
        _trtId = trtId
        _treatDetail = If(String.IsNullOrWhiteSpace(treatDetail), "-", treatDetail)
        _payType = PayTypeLabels.NormalizeToEnglish(If(String.IsNullOrWhiteSpace(payType), PayTypeLabels.OtherEn, payType.Trim()))
        If String.IsNullOrWhiteSpace(_payType) Then _payType = PayTypeLabels.OtherEn
        _pay = Nothing
        ResetChequeImageCaptureState()
        lblTreat.Text = If(Eng, "For Treat: ", "للمعالجة: ") & _treatDetail
        PayDate.EditValue = Date.Today
        PayValue.EditValue = 0D
        NotesText.Text = ""
        If txtReceivedBy IsNot Nothing Then txtReceivedBy.Text = ""
        chqInsurTab.Visible = True
        If String.Compare(_payType, "Cheque", StringComparison.OrdinalIgnoreCase) = 0 Then
            chqInsurTab.SelectedTabPage = ChqPage
            txtChqOwner.Text = GetDefaultChequeOwnerDisplayName()
            txtAccountNumber.Text = ""
            txtChqNumber.Text = ""
            dtChqDueDate.EditValue = Nothing
            txtChqBank.Text = ""
            chkIsCashed.Checked = False
            txtChqValue.EditValue = 0D
            PayValue.Properties.ReadOnly = True
            chkIsReturned.Checked = False
            chkIsReturned.Visible = False
        ElseIf String.Compare(_payType, "Insurance", StringComparison.OrdinalIgnoreCase) = 0 Then
            chqInsurTab.SelectedTabPage = InsurePage
            txtInureComp.Text = ""
            txtInsurNotes.Text = ""
            PayValue.Properties.ReadOnly = False
            chkIsReturned.Visible = False
        Else
            chqInsurTab.Visible = False
            PayValue.Properties.ReadOnly = False
            chkIsReturned.Visible = False
        End If
        btnAddPay.Visible = True
        btnSave.Visible = False
        btnAddPay.Text = If(Eng, "Add", "إضافة")
        btnCancel.Text = If(Eng, "Cancel", "إلغاء")
        Me.Text = If(Eng, "Add Payment (" & _payType & ")", "إضافة دفعة (" & PayTypeLabels.ToArabic(_payType) & ")")
        ConfigureChequeSideRadio()
        UpdateChequeScanModeUi()
    End Sub

    ''' <summary>Load the payment record for editing. Call before ShowDialog.</summary>
    Public Sub LoadForEdit(pay As Patient_Pays, treatDetail As String)
        If pay Is Nothing Then Return
        _pay = pay
        _patientId = If(pay.PatientID.HasValue, pay.PatientID.Value, 0)
        _trtId = pay.TrtID
        _payType = PayTypeLabels.NormalizeToEnglish(If(String.IsNullOrWhiteSpace(pay.PayType), PayTypeLabels.OtherEn, pay.PayType.Trim()))
        If String.IsNullOrWhiteSpace(_payType) Then _payType = PayTypeLabels.OtherEn
        _treatDetail = If(String.IsNullOrWhiteSpace(treatDetail), "-", treatDetail)
        lblTreat.Text = If(Eng, "For Treat: ", "للمعالجة: ") & _treatDetail
        PayDate.EditValue = pay.PayDate
        PayValue.EditValue = pay.PayValue
        NotesText.Text = If(pay.Notes, "")
        If txtReceivedBy IsNot Nothing Then txtReceivedBy.Text = If(pay.ReceivedBy, "")
        chqInsurTab.Visible = True
        If String.Compare(_payType, "Cheque", StringComparison.OrdinalIgnoreCase) = 0 Then
            chqInsurTab.SelectedTabPage = ChqPage
            txtChqOwner.Text = If(pay.ChqOwner, "")
            txtAccountNumber.Text = If(pay.AccountNumber, "")
            txtChqNumber.Text = If(pay.ChqNumber, "")
            dtChqDueDate.EditValue = If(pay.ChqDueDate.HasValue, pay.ChqDueDate.Value, Nothing)
            txtChqBank.Text = If(pay.ChqBank, "")
            chkIsCashed.Checked = If(pay.IsCashed.HasValue, pay.IsCashed.Value, False)
            txtChqValue.EditValue = pay.PayValue
            chkIsReturned.Checked = If(pay.IsReturned.HasValue, pay.IsReturned.Value, False)
            chkIsReturned.Visible = True
        ElseIf String.Compare(_payType, "Insurance", StringComparison.OrdinalIgnoreCase) = 0 Then
            chqInsurTab.SelectedTabPage = InsurePage
            txtInureComp.Text = If(pay.InsuranceCompany, "")
            txtInsurNotes.Text = If(pay.InsuranceNotes, "")
            chkIsReturned.Visible = False
        Else
            chqInsurTab.Visible = False
            chkIsReturned.Visible = False
        End If
        btnAddPay.Visible = False
        btnSave.Visible = True
        btnSave.Text = If(Eng, "Save", "حفظ")
        btnCancel.Text = If(Eng, "Cancel", "إلغاء")
        Me.Text = If(Eng, "Edit Payment (" & _payType & ")", "تعديل الدفعة (" & PayTypeLabels.ToArabic(_payType) & ")")
        ResetChequeImageCaptureState()
        ConfigureChequeSideRadio()
        UpdateChequeScanModeUi()
    End Sub

    Private Sub FrmChqPayAccnt_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ConfigureChequeSideRadio()
        UpdateChequeScanModeUi()
        IntegerMoneyEditorFocus.ConfigureIntegerMoneyTextEdit(PayValue)
        IntegerMoneyEditorFocus.ConfigureIntegerMoneyTextEdit(txtChqValue)
        IntegerMoneyEditorFocus.AttachTextEditZeroEmptyElseSelectAll(PayValue, txtChqValue)
    End Sub

    Private Sub ConfigureChequeSideRadio()
        If lblChequeScanSide Is Nothing OrElse radioChequeImageSide Is Nothing Then Return
        lblChequeScanSide.Text = If(Eng, "Cheque scan side:", "جانب مسح الشيك:")
        radioChequeImageSide.Properties.Items.Clear()
        radioChequeImageSide.Properties.Items.Add(New DevExpress.XtraEditors.Controls.RadioGroupItem(ChequeImageFace.Front, If(Eng, "Front", "أمام")))
        radioChequeImageSide.Properties.Items.Add(New DevExpress.XtraEditors.Controls.RadioGroupItem(ChequeImageFace.Back, If(Eng, "Back", "خلف")))
        radioChequeImageSide.EditValue = ChequeImageFace.Front

        If lblChequeScanMode IsNot Nothing Then
            lblChequeScanMode.Text = If(Eng, "Scan / files:", "المسح / الملفات:")
        End If
        If radioChequeScanMode IsNot Nothing Then
            _suppressRadioScanModeEvents = True
            Try
                radioChequeScanMode.Properties.Items.Clear()
                radioChequeScanMode.Properties.Items.Add(New DevExpress.XtraEditors.Controls.RadioGroupItem(ChequeScanSheetMode.OneSide, If(Eng, "One side (single page)", "وجه واحد (صفحة واحدة)")))
                radioChequeScanMode.Properties.Items.Add(New DevExpress.XtraEditors.Controls.RadioGroupItem(ChequeScanSheetMode.FrontBack, If(Eng, "Front + back (2 pages)", "أمام وخلف (صفحتان)")))
                radioChequeScanMode.Properties.Columns = 2
                radioChequeScanMode.EditValue = ChequeScanSheetMode.OneSide
            Finally
                _suppressRadioScanModeEvents = False
            End Try
        End If
    End Sub

    Private Sub radioChequeScanMode_EditValueChanged(sender As Object, e As EventArgs) Handles radioChequeScanMode.EditValueChanged
        If _suppressRadioScanModeEvents Then Return
        UpdateChequeScanModeUi()
    End Sub

    Private Function GetChequeScanSheetMode() As ChequeScanSheetMode
        If radioChequeScanMode Is Nothing Then Return ChequeScanSheetMode.OneSide
        Dim v = radioChequeScanMode.EditValue
        If TypeOf v Is ChequeScanSheetMode Then Return DirectCast(v, ChequeScanSheetMode)
        Try
            Return CType(CInt(v), ChequeScanSheetMode)
        Catch
            Return ChequeScanSheetMode.OneSide
        End Try
    End Function

    Private Sub UpdateChequeScanModeUi()
        Dim dual = (GetChequeScanSheetMode() = ChequeScanSheetMode.FrontBack)
        If lblChequeScanSide IsNot Nothing Then lblChequeScanSide.Visible = Not dual
        If radioChequeImageSide IsNot Nothing Then radioChequeImageSide.Visible = Not dual
        If lblChequeScanHint IsNot Nothing Then
            If dual Then
                lblChequeScanHint.Text = If(Eng,
                    "Front + back: scan or pick 2 images in order — first = front, second = back. Extra files go to attachments.",
                    "أمام وخلف: امسح أو اختر صورتين بالترتيب — الأولى أمام والثانية خلف. الملفات الإضافية تُرفق.")
            Else
                lblChequeScanHint.Text = If(Eng,
                    "One side: one scan/page applies to Front or Back (choose below). You can attach more files (e.g. PDF) with Browse.",
                    "وجه واحد: مسح أو ملف واحد يُطبَّق على أمام أو خلف (اختر أدناه). يمكنك إرفاق ملفات إضافية عبر استعراض.")
            End If
        End If
    End Sub

    Private Function GetSelectedChequeImageFace() As ChequeImageFace
        If radioChequeImageSide Is Nothing Then Return ChequeImageFace.Front
        Dim v = radioChequeImageSide.EditValue
        If TypeOf v Is ChequeImageFace Then Return DirectCast(v, ChequeImageFace)
        Return ChequeImageFace.Front
    End Function

    Private Function ValidateCheque() As Boolean
        If chkSkipFields.Checked Then Return True
        If String.IsNullOrWhiteSpace(txtChqOwner.Text) Then
            ShowChqMessage(If(Eng, "Cheque Owner is required.", "صاحب الشيك مطلوب."), If(Eng, "Validation", "تحقق"), MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return False
        End If
        If String.IsNullOrWhiteSpace(txtAccountNumber.Text) Then
            ShowChqMessage(If(Eng, "Account Number is required.", "رقم الحساب مطلوب."), If(Eng, "Validation", "تحقق"), MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return False
        End If
        If String.IsNullOrWhiteSpace(txtChqNumber.Text) Then
            ShowChqMessage(If(Eng, "Cheque Number is required.", "رقم الشيك مطلوب."), If(Eng, "Validation", "تحقق"), MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return False
        End If
        If dtChqDueDate.EditValue Is Nothing OrElse String.IsNullOrWhiteSpace(dtChqDueDate.Text) Then
            ShowChqMessage(If(Eng, "Cheque Due Date is required.", "تاريخ استحقاق الشيك مطلوب."), If(Eng, "Validation", "تحقق"), MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return False
        End If
        If String.IsNullOrWhiteSpace(txtChqBank.Text) Then
            ShowChqMessage(If(Eng, "Cheque Bank is required.", "بنك الشيك مطلوب."), If(Eng, "Validation", "تحقق"), MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return False
        End If
        Return True
    End Function

    Private Function ValidateInsurance() As Boolean
        If String.IsNullOrWhiteSpace(txtInureComp.Text) Then
            ShowChqMessage(If(Eng, "Insurance Company is required.", "شركة التأمين مطلوبة."), If(Eng, "Validation", "تحقق"), MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return False
        End If
        If String.IsNullOrWhiteSpace(txtInsurNotes.Text) Then
            ShowChqMessage(If(Eng, "Insurance Notes are required.", "ملاحظات التأمين مطلوبة."), If(Eng, "Validation", "تحقق"), MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return False
        End If
        Return True
    End Function

    ''' <summary>Cheque amount is entered in <see cref="txtChqValue"/>; <see cref="PayValue"/> mirrors it and must be read consistently for validation.</summary>
    Private Function GetPayAmountForValidation() As Decimal
        If String.Compare(_payType, "Cheque", StringComparison.OrdinalIgnoreCase) = 0 AndAlso txtChqValue IsNot Nothing Then
            Return IntegerMoneyEditorFocus.DecimalFromIntegerMoneyEdit(txtChqValue)
        End If
        Return IntegerMoneyEditorFocus.DecimalFromIntegerMoneyEdit(PayValue)
    End Function

    ''' <summary>For add flow: cheque tab fields plus pay date and amount must be valid before Scan/Browse can stash an image for Add.</summary>
    Private Function ValidateChequeAndPayAmountForPendingImage() As Boolean
        If PayDate.EditValue Is Nothing OrElse String.IsNullOrWhiteSpace(PayDate.Text) Then
            ShowChqMessage(If(Eng, "Payment Date is required.", "التاريخ مطلوب."), If(Eng, "Validation", "تحقق"), MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return False
        End If
        Dim payVal As Decimal = GetPayAmountForValidation()
        If payVal <= 0D Then
            ShowChqMessage(If(Eng, "Pay Value must be greater than 0.", "قيمة الدفعة يجب أن تكون أكبر من صفر."), If(Eng, "Validation", "تحقق"), MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return False
        End If
        Return ValidateCheque()
    End Function

    Private Sub ResetChequeImageCaptureState()
        If _chequeCaptureUiLocked Then ClearChequeImageCaptureUiLock()
        _pendingChequeImageSourcePath = Nothing
        _pendingChequeImageIsDisposableScan = False
        _pendingChequeImageSecondPath = Nothing
        _pendingChequeImageSecondIsDisposableScan = False
        _pendingAttachmentExtraPaths.Clear()
    End Sub

    Private Sub ApplyChequeImageCaptureUiLock()
        _chequeCaptureUiLocked = True
        If btnScan IsNot Nothing Then btnScan.Enabled = False
        If btnBrowse IsNot Nothing Then btnBrowse.Enabled = False
        If btnScanAndPay IsNot Nothing Then btnScanAndPay.Enabled = False
        If btnResetChqs IsNot Nothing Then btnResetChqs.Enabled = False
        If chqInsurTab IsNot Nothing Then chqInsurTab.Enabled = False
        If ChkDrive IsNot Nothing Then ChkDrive.Enabled = False
        If lblChequeScanSide IsNot Nothing Then lblChequeScanSide.Enabled = False
        If radioChequeImageSide IsNot Nothing Then radioChequeImageSide.Enabled = False
        If lblChequeScanMode IsNot Nothing Then lblChequeScanMode.Enabled = False
        If radioChequeScanMode IsNot Nothing Then radioChequeScanMode.Enabled = False
        If lblChequeScanHint IsNot Nothing Then lblChequeScanHint.Enabled = False

        Dim ro = True
        txtChqOwner.Properties.ReadOnly = ro
        txtAccountNumber.Properties.ReadOnly = ro
        txtChqNumber.Properties.ReadOnly = ro
        dtChqDueDate.Properties.ReadOnly = ro
        txtChqBank.Properties.ReadOnly = ro
        txtChqValue.Properties.ReadOnly = ro
        PayDate.Properties.ReadOnly = ro
        PayValue.Properties.ReadOnly = ro
        NotesText.Properties.ReadOnly = ro
        chkIsCashed.Enabled = False
        If txtReceivedBy IsNot Nothing Then txtReceivedBy.Properties.ReadOnly = ro
        chkIsReturned.Properties.ReadOnly = ro

        If btnAddPay IsNot Nothing AndAlso btnAddPay.Visible Then
            btnAddPay.Enabled = True
            If btnSave IsNot Nothing Then btnSave.Enabled = False
        ElseIf btnSave IsNot Nothing AndAlso btnSave.Visible Then
            btnSave.Enabled = True
            If btnAddPay IsNot Nothing Then btnAddPay.Enabled = False
        End If
        If btnCancel IsNot Nothing Then btnCancel.Enabled = True
    End Sub

    Private Sub ClearChequeImageCaptureUiLock()
        If Not _chequeCaptureUiLocked Then Return
        _chequeCaptureUiLocked = False
        If btnScan IsNot Nothing Then btnScan.Enabled = True
        If btnBrowse IsNot Nothing Then btnBrowse.Enabled = True
        If btnScanAndPay IsNot Nothing Then btnScanAndPay.Enabled = True
        If btnResetChqs IsNot Nothing Then btnResetChqs.Enabled = True
        If chqInsurTab IsNot Nothing Then chqInsurTab.Enabled = True
        If ChkDrive IsNot Nothing Then ChkDrive.Enabled = True
        If lblChequeScanSide IsNot Nothing Then lblChequeScanSide.Enabled = True
        If radioChequeImageSide IsNot Nothing Then radioChequeImageSide.Enabled = True
        If lblChequeScanMode IsNot Nothing Then lblChequeScanMode.Enabled = True
        If radioChequeScanMode IsNot Nothing Then radioChequeScanMode.Enabled = True
        If lblChequeScanHint IsNot Nothing Then lblChequeScanHint.Enabled = True

        Dim ro = False
        txtChqOwner.Properties.ReadOnly = ro
        txtAccountNumber.Properties.ReadOnly = ro
        txtChqNumber.Properties.ReadOnly = ro
        dtChqDueDate.Properties.ReadOnly = ro
        txtChqBank.Properties.ReadOnly = ro
        txtChqValue.Properties.ReadOnly = ro
        PayDate.Properties.ReadOnly = ro
        NotesText.Properties.ReadOnly = ro
        chkIsCashed.Enabled = True
        If txtReceivedBy IsNot Nothing Then txtReceivedBy.Properties.ReadOnly = ro
        chkIsReturned.Properties.ReadOnly = ro

        Dim chequeAdd = (_pay Is Nothing AndAlso String.Compare(_payType, "Cheque", StringComparison.OrdinalIgnoreCase) = 0)
        PayValue.Properties.ReadOnly = chequeAdd

        If btnAddPay IsNot Nothing Then btnAddPay.Enabled = btnAddPay.Visible
        If btnSave IsNot Nothing Then btnSave.Enabled = btnSave.Visible
    End Sub

    Private Sub btnAddPay_Click(sender As Object, e As EventArgs) Handles btnAddPay.Click
        Dim newPayId = TryCommitAddPaymentAndGetPayId()
        If Not newPayId.HasValue Then Return
        FormManager.Instance.CurrentForm.UpdatePatientBalance(CurrentPatient)
        Me.DialogResult = DialogResult.OK
        Me.Close()
    End Sub

    ''' <summary>Returns new PayID after INSERT, or Nothing on validation/DB error.</summary>
    Private Function TryCommitAddPaymentAndGetPayId() As Integer?
        If PayDate.EditValue Is Nothing OrElse String.IsNullOrWhiteSpace(PayDate.Text) Then
            ShowChqMessage(If(Eng, "Payment Date is required.", "التاريخ مطلوب."), If(Eng, "Validation", "تحقق"), MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return Nothing
        End If
        Dim payVal As Decimal = GetPayAmountForValidation()
        If payVal <= 0 Then
            ShowChqMessage(If(Eng, "Pay Value must be greater than 0.", "قيمة الدفعة يجب أن تكون أكبر من صفر."), If(Eng, "Validation", "تحقق"), MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return Nothing
        End If
        Dim payDat As Date = CType(PayDate.EditValue, DateTime)
        Dim notes As String = If(NotesText.Text, "").Trim()
        Dim recvBy As String = If(txtReceivedBy Is Nothing OrElse String.IsNullOrWhiteSpace(txtReceivedBy.Text), Nothing, txtReceivedBy.Text.Trim())
        Dim isRet As Boolean = (chkIsReturned.Visible AndAlso chkIsReturned.Checked)

        If String.Compare(_payType, "Cheque", StringComparison.OrdinalIgnoreCase) = 0 Then
            If Not ValidateCheque() Then Return Nothing
        ElseIf String.Compare(_payType, "Insurance", StringComparison.OrdinalIgnoreCase) = 0 Then
            If Not ValidateInsurance() Then Return Nothing
        End If

        Try
            Dim newPayId As Integer
            Using conn As New SqlConnection(DentistXDATA.GetConnection.ConnectionString)
                conn.Open()
                Using trans As SqlTransaction = conn.BeginTransaction()
                    Try
                        Dim trtID As Integer
                        If Treats IsNot Nothing Then
                            trtID = Treats.TrtID
                        Else
                            Dim detail = If(Eng, "Payment In Advance " & Me.NotesText.Text, "دفعة مقدما " & Me.NotesText.Text)
                            Dim treatName As String = detail
                            Dim treatDate As Date = payDat
                            Dim toothTrtID As Integer

                            conn.Execute("INSERT INTO Patient_ToothTrt (PatientID, Treat, TreatDate) VALUES (@PatientID, @Treat, @TreatDate)",
                                         New With {.PatientId = _patientId, .Treat = treatName, .TreatDate = treatDate}, trans)
                            toothTrtID = conn.ExecuteScalar(Of Integer)(
                                "SELECT TOP 1 ToothTrtID FROM Patient_ToothTrt WHERE PatientID = @PatientID ORDER BY ToothTrtID DESC",
                                New With {.PatientId = _patientId}, trans)
                            conn.Execute("INSERT INTO Patient_Trts (PatientID, ToothTrtID, Detail, TrtDate, TrtValue, Discount2) VALUES (@PatientID, @ToothTrtID, @Detail, @TrtDate, 0, 0)",
                                         New With {.PatientId = _patientId, .ToothTrtID = toothTrtID, .Detail = detail, .TrtDate = payDat}, trans)
                            trtID = conn.ExecuteScalar(Of Integer)(
                                "SELECT TOP 1 TrtID FROM Patient_Trts WHERE PatientID = @PatientID ORDER BY TrtID DESC",
                                New With {.PatientId = _patientId}, trans)
                        End If

                        If String.Compare(_payType, "Cheque", StringComparison.OrdinalIgnoreCase) = 0 Then
                            newPayId = conn.QuerySingle(Of Integer)(
                                "INSERT INTO Patient_Pays (TrtID, PatientID, PayValue, PayDate, Notes, PayType, ChqOwner, AccountNumber, ChqNumber, ChqDueDate, ChqBank, IsCashed, ReceivedBy, IsReturned) " &
                                "OUTPUT INSERTED.PayID VALUES (@TrtID, @PatientID, @PayValue, @PayDate, @Notes, @PayType, @ChqOwner, @AccountNumber, @ChqNumber, @ChqDueDate, @ChqBank, @IsCashed, @ReceivedBy, @IsReturned)",
                                New With {
                                    .TrtID = trtID,
                                    .PatientID = _patientId,
                                    .PayValue = payVal,
                                    .PayDate = payDat,
                                    .Notes = notes,
                                    .PayType = _payType,
                                    .ChqOwner = txtChqOwner.Text.Trim(),
                                    .AccountNumber = txtAccountNumber.Text.Trim(),
                                    .ChqNumber = txtChqNumber.Text.Trim(),
                                    .ChqDueDate = CType(dtChqDueDate.EditValue, DateTime),
                                    .ChqBank = txtChqBank.Text.Trim(),
                                    .IsCashed = chkIsCashed.Checked,
                                    .ReceivedBy = recvBy,
                                    .IsReturned = isRet
                                }, trans)
                        ElseIf String.Compare(_payType, "Insurance", StringComparison.OrdinalIgnoreCase) = 0 Then
                            newPayId = conn.QuerySingle(Of Integer)(
                                "INSERT INTO Patient_Pays (TrtID, PatientID, PayValue, PayDate, Notes, PayType, InsuranceCompany, InsuranceNotes, ReceivedBy, IsReturned) " &
                                "OUTPUT INSERTED.PayID VALUES (@TrtID, @PatientID, @PayValue, @PayDate, @Notes, @PayType, @InsuranceCompany, @InsuranceNotes, @ReceivedBy, @IsReturned)",
                                New With {
                                    .TrtID = trtID,
                                    .PatientID = _patientId,
                                    .PayValue = payVal,
                                    .PayDate = payDat,
                                    .Notes = notes,
                                    .PayType = _payType,
                                    .InsuranceCompany = txtInureComp.Text.Trim(),
                                    .InsuranceNotes = txtInsurNotes.Text.Trim(),
                                    .ReceivedBy = recvBy,
                                    .IsReturned = False
                                }, trans)
                        Else
                            newPayId = conn.QuerySingle(Of Integer)(
                                "INSERT INTO Patient_Pays (TrtID, PatientID, PayValue, PayDate, Notes, PayType, ReceivedBy, IsReturned) OUTPUT INSERTED.PayID VALUES (@TrtID, @PatientID, @PayValue, @PayDate, @Notes, @PayType, @ReceivedBy, @IsReturned)",
                                New With {.TrtID = trtID, .PatientID = _patientId, .PayValue = payVal, .PayDate = payDat, .Notes = notes, .PayType = _payType, .ReceivedBy = recvBy, .IsReturned = False}, trans)
                        End If

                        If newPayId <= 0 Then
                            Throw New InvalidOperationException(If(Eng, "Could not read new payment ID after insert.", "تعذر قراءة رقم الدفعة بعد الإدراج."))
                        End If
                        trans.Commit()
                    Catch
                        trans.Rollback()
                        Throw
                    End Try
                End Using
            End Using

            If String.Compare(_payType, "Cheque", StringComparison.OrdinalIgnoreCase) = 0 Then
                If Not String.IsNullOrWhiteSpace(_pendingChequeImageSourcePath) AndAlso File.Exists(_pendingChequeImageSourcePath) Then
                    Try
                        If Not String.IsNullOrWhiteSpace(_pendingChequeImageSecondPath) AndAlso File.Exists(_pendingChequeImageSecondPath) Then
                            Dim rf = CopyPatientChequeLinked(newPayId, txtChqNumber.Text.Trim(), _pendingChequeImageSourcePath, ChequeImageFace.Front)
                            If rf Is Nothing Then
                                ShowChqOverwriteCancelled()
                            Else
                                Dim rb = CopyPatientChequeLinked(newPayId, txtChqNumber.Text.Trim(), _pendingChequeImageSecondPath, ChequeImageFace.Back)
                                If rb Is Nothing Then ShowChqOverwriteCancelled()
                            End If
                        Else
                            Dim r = CopyPatientChequeLinked(newPayId, txtChqNumber.Text.Trim(), _pendingChequeImageSourcePath, GetSelectedChequeImageFace())
                            If r Is Nothing Then ShowChqOverwriteCancelled()
                        End If
                    Catch ex As Exception
                        ShowChqMessage(If(Eng, "Payment was saved but linking the cheque image failed: ", "تم حفظ الدفعة وفشل ربط صورة الشيك: ") & ex.Message,
                                       If(Eng, "Cheques", "الدفعة"), MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    End Try
                    Try
                        If _pendingChequeImageIsDisposableScan AndAlso File.Exists(_pendingChequeImageSourcePath) Then File.Delete(_pendingChequeImageSourcePath)
                    Catch
                    End Try
                    Try
                        If _pendingChequeImageSecondIsDisposableScan AndAlso Not String.IsNullOrWhiteSpace(_pendingChequeImageSecondPath) AndAlso File.Exists(_pendingChequeImageSecondPath) Then File.Delete(_pendingChequeImageSecondPath)
                    Catch
                    End Try
                End If

                For Each extraPath In _pendingAttachmentExtraPaths
                    If String.IsNullOrWhiteSpace(extraPath) OrElse Not File.Exists(extraPath) Then Continue For
                    Try
                        CopyPatientAttachmentLinkedToCheque(newPayId, extraPath)
                    Catch ex As Exception
                        ShowChqMessage(If(Eng, "Payment saved but an attachment could not be copied: ", "تم حفظ الدفعة وتعذر نسخ مرفق: ") & ex.Message,
                                       If(Eng, "Cheques", "الدفعة"), MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    End Try
                Next
            End If

            _pendingChequeImageSourcePath = Nothing
            _pendingChequeImageIsDisposableScan = False
            _pendingChequeImageSecondPath = Nothing
            _pendingChequeImageSecondIsDisposableScan = False
            _pendingAttachmentExtraPaths.Clear()

            Return newPayId
        Catch ex As Exception
            ShowChqMessage(If(Eng, "Error adding payment: ", "خطأ عند إضافة الدفعة: ") & ex.Message,
                           If(Eng, "Cheques", "الدفعة"), MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        End Try
    End Function

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If Not TrySaveEditPaymentInternal() Then Return
        Me.DialogResult = DialogResult.OK
        Me.Close()
    End Sub

    ''' <summary>UPDATE edited payment; does not close the form.</summary>
    Private Function TrySaveEditPaymentInternal() As Boolean
        If _pay Is Nothing Then Return False
        If PayDate.EditValue Is Nothing OrElse String.IsNullOrWhiteSpace(PayDate.Text) Then
            ShowChqMessage(If(Eng, "Payment Date is required.", "التاريخ مطلوب."), If(Eng, "Validation", "تحقق"), MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return False
        End If
        Dim payVal As Decimal = GetPayAmountForValidation()
        If payVal <= 0 Then
            ShowChqMessage(If(Eng, "Pay Value must be greater than 0.", "قيمة الدفعة يجب أن تكون أكبر من صفر."), If(Eng, "Validation", "تحقق"), MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return False
        End If
        Dim payDat As Date = CType(PayDate.EditValue, DateTime)
        Dim notes As String = If(NotesText.Text, "").Trim()
        Dim recvBy As String = If(txtReceivedBy Is Nothing OrElse String.IsNullOrWhiteSpace(txtReceivedBy.Text), Nothing, txtReceivedBy.Text.Trim())
        Dim isRetChq As Boolean = (chkIsReturned.Visible AndAlso chkIsReturned.Checked)

        If String.Compare(_payType, "Cheque", StringComparison.OrdinalIgnoreCase) = 0 Then
            If Not ValidateCheque() Then Return False
        ElseIf String.Compare(_payType, "Insurance", StringComparison.OrdinalIgnoreCase) = 0 Then
            If Not ValidateInsurance() Then Return False
        End If

        Try
            Using conn As New SqlConnection(DentistXDATA.GetConnection.ConnectionString)
                conn.Open()
                If String.Compare(_payType, "Cheque", StringComparison.OrdinalIgnoreCase) = 0 Then
                    conn.Execute(
                        "UPDATE Patient_Pays SET PayType=@PayType, PayValue=@PayValue, PayDate=@PayDate, Notes=@Notes, ChqOwner=@ChqOwner, AccountNumber=@AccountNumber, ChqNumber=@ChqNumber, ChqDueDate=@ChqDueDate, ChqBank=@ChqBank, IsCashed=@IsCashed, ReceivedBy=@ReceivedBy, IsReturned=@IsReturned WHERE PayID=@PayID",
                        New With {
                            .PayType = _payType,
                            .PayValue = payVal,
                            .PayDate = payDat,
                            .Notes = notes,
                            .ChqOwner = txtChqOwner.Text.Trim(),
                            .AccountNumber = txtAccountNumber.Text.Trim(),
                            .ChqNumber = txtChqNumber.Text.Trim(),
                            .ChqDueDate = CType(dtChqDueDate.EditValue, DateTime),
                            .ChqBank = txtChqBank.Text.Trim(),
                            .IsCashed = chkIsCashed.Checked,
                            .ReceivedBy = recvBy,
                            .IsReturned = isRetChq,
                            .isforward = chkIsForward.Checked,
                            .ForwardFromTo = txtForwardTo.Text.Trim(),
                            .PayID = _pay.PayID
                        })
                ElseIf String.Compare(_payType, "Insurance", StringComparison.OrdinalIgnoreCase) = 0 Then
                    conn.Execute(
                        "UPDATE Patient_Pays SET PayType=@PayType, PayValue=@PayValue, PayDate=@PayDate, Notes=@Notes, InsuranceCompany=@InsuranceCompany, InsuranceNotes=@InsuranceNotes, ReceivedBy=@ReceivedBy WHERE PayID=@PayID",
                        New With {
                            .PayType = _payType,
                            .PayValue = payVal,
                            .PayDate = payDat,
                            .Notes = notes,
                            .InsuranceCompany = txtInureComp.Text.Trim(),
                            .InsuranceNotes = txtInsurNotes.Text.Trim(),
                            .ReceivedBy = recvBy,
                            .PayID = _pay.PayID
                        })
                Else
                    conn.Execute(
                        "UPDATE Patient_Pays SET PayType=@PayType, PayValue=@PayValue, PayDate=@PayDate, Notes=@Notes, ReceivedBy=@ReceivedBy WHERE PayID=@PayID",
                        New With {.PayType = _payType, .PayValue = payVal, .PayDate = payDat, .Notes = notes, .ReceivedBy = recvBy, .PayID = _pay.PayID})
                End If
            End Using
            _pay.PayType = _payType
            _pay.PayValue = payVal
            _pay.PayDate = payDat
            _pay.Notes = notes
            _pay.ReceivedBy = recvBy
            If String.Compare(_payType, "Cheque", StringComparison.OrdinalIgnoreCase) = 0 Then
                _pay.IsReturned = isRetChq
                _pay.ChqOwner = txtChqOwner.Text.Trim()
                _pay.AccountNumber = txtAccountNumber.Text.Trim()
                _pay.ChqNumber = txtChqNumber.Text.Trim()
                _pay.ChqDueDate = CType(dtChqDueDate.EditValue, DateTime)
                _pay.ChqBank = txtChqBank.Text.Trim()
                _pay.IsCashed = chkIsCashed.Checked
            ElseIf String.Compare(_payType, "Insurance", StringComparison.OrdinalIgnoreCase) = 0 Then
                _pay.InsuranceCompany = txtInureComp.Text.Trim()
                _pay.InsuranceNotes = txtInsurNotes.Text.Trim()
            End If
            Return True
        Catch ex As Exception
            ShowChqMessage(If(Eng, "Error saving payment: ", "خطأ عند حفظ الدفعة: ") & ex.Message,
                           If(Eng, "Cheques", "الدفعة"), MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End Try
    End Function

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        If _pendingChequeImageIsDisposableScan AndAlso Not String.IsNullOrWhiteSpace(_pendingChequeImageSourcePath) Then
            Try
                If File.Exists(_pendingChequeImageSourcePath) Then File.Delete(_pendingChequeImageSourcePath)
            Catch
            End Try
        End If
        If _pendingChequeImageSecondIsDisposableScan AndAlso Not String.IsNullOrWhiteSpace(_pendingChequeImageSecondPath) Then
            Try
                If File.Exists(_pendingChequeImageSecondPath) Then File.Delete(_pendingChequeImageSecondPath)
            Catch
            End Try
        End If
        ResetChequeImageCaptureState()
        Me.DialogResult = DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub txtChqValue_EditValueChanged(sender As Object, e As EventArgs) Handles txtChqValue.EditValueChanged
        PayValue.EditValue = txtChqValue.EditValue
    End Sub
    Private Sub txtChq_KeyDown(sender As Object, e As KeyEventArgs) Handles txtChqNumber.KeyDown, txtAccountNumber.KeyDown
        ' Allow control keys: backspace, delete, arrows, tab, home/end, etc.
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

        ' Allow digits only (top row or numpad)
        Dim isTopRowDigit As Boolean = (e.KeyCode >= Keys.D0 AndAlso e.KeyCode <= Keys.D9)
        Dim isNumPadDigit As Boolean = (e.KeyCode >= Keys.NumPad0 AndAlso e.KeyCode <= Keys.NumPad9)

        ' Block Shift-modified digits (to avoid !, @, etc.)
        If (isTopRowDigit OrElse isNumPadDigit) AndAlso (Not e.Shift) Then
            Return
        End If

        ' Otherwise block the key
        e.SuppressKeyPress = True
        e.Handled = True
    End Sub
    Private Sub btnResetChqs_Click(sender As Object, e As EventArgs) Handles btnResetChqs.Click
        txtAccountNumber.ResetText()
        txtChqBank.ResetText()
        txtChqNumber.ResetText()
        txtChqOwner.ResetText()
        txtChqValue.EditValue = 0D
        dtChqDueDate.EditValue = Nothing
        NotesText.ResetText()
        PayDate.EditValue = Date.Today
        PayValue.EditValue = 0D
        chkIsForward.Checked = False
        txtForwardTo.ResetText()
        chkIsCashed.Checked = False
    End Sub

    Private Function GetPatientScannedFolder() As String
        If _patientId <= 0 Then Return ""
        Dim appDir = Application.StartupPath
        If String.IsNullOrEmpty(appDir) Then Return ""
        Return Path.Combine(appDir, "Images", "Patient" & _patientId.ToString(), "Cheques")
    End Function

    Private Sub OpenScanFolder(folderPath As String)
        If String.IsNullOrWhiteSpace(folderPath) OrElse Not Directory.Exists(folderPath) Then Return
        Try
            Process.Start(New ProcessStartInfo("explorer.exe", folderPath) With {.UseShellExecute = True})
        Catch
        End Try
    End Sub

    Private Function NormalizeChqNumber(raw As String) As String
        Dim t = If(raw, "").Trim()
        If t.Length = 0 Then Return Nothing
        If t.Length > 10 Then t = t.Substring(0, 10)
        Return t
    End Function

    Private Shared Function SafeChequeFileNamePart(raw As String) As String
        Dim t = If(raw, "").Trim()
        For Each c In Path.GetInvalidFileNameChars()
            t = t.Replace(c, "_"c)
        Next
        If t.Length > 48 Then t = t.Substring(0, 48)
        Return If(t.Length = 0, "Chq", t)
    End Function

    ''' <summary>Cheques folder already has Pay[id]_Chq… for this payment.</summary>
    Private Function HasLinkedPatientChequeImage(payId As Integer) As Boolean
        If payId <= 0 Then Return False
        Dim folder = GetPatientScannedFolder()
        If String.IsNullOrWhiteSpace(folder) OrElse Not Directory.Exists(folder) Then Return False
        Dim prefix = "Pay" & payId.ToString() & "_Chq"
        For Each f In Directory.EnumerateFiles(folder)
            If Path.GetFileName(f).StartsWith(prefix, StringComparison.OrdinalIgnoreCase) Then Return True
        Next
        Return False
    End Function

    Private Function CopyPatientChequeLinked(payId As Integer, chqNumberRaw As String, sourcePath As String, face As ChequeImageFace) As String
        If payId <= 0 Then Throw New InvalidOperationException("Invalid payment")
        Dim n = NormalizeChqNumber(chqNumberRaw)
        If String.IsNullOrWhiteSpace(n) Then Throw New InvalidOperationException("Cheque number required")
        Dim ext = Path.GetExtension(sourcePath)
        If String.IsNullOrWhiteSpace(ext) Then ext = ".jpg"
        Dim folder = GetPatientScannedFolder()
        If String.IsNullOrWhiteSpace(folder) Then Throw New InvalidOperationException("Invalid folder")
        If Not Directory.Exists(folder) Then Directory.CreateDirectory(folder)
        Dim dest = Path.Combine(folder, ChequeImageLinkHelper.BuildLinkedChequeFileName(payId, SafeChequeFileNamePart(n), ext, face))
        If File.Exists(dest) Then
            If Not ConfirmOverwriteChequeLinked(dest, sourcePath, face) Then
                Return Nothing
            End If
        End If
        File.Copy(sourcePath, dest, overwrite:=True)
        If ChkDrive.Checked Then PatientDriveMirror.TryMirrorStartupRelativeFile(dest, GetChequeDriveOwnerLabel(), Me)
        Return dest
    End Function

    Private Shared ReadOnly _chequeImageExtensionsBrowse As String() = {".jpg", ".jpeg", ".png", ".bmp", ".gif", ".tif", ".tiff"}

    Private Function IsChequeImageFile(path As String) As Boolean
        Dim ext = IO.Path.GetExtension(path)
        If String.IsNullOrEmpty(ext) Then Return False
        Return _chequeImageExtensionsBrowse.Contains(ext.ToLowerInvariant())
    End Function

    Private Function GetPatientAttachmentsRootFolder() As String
        Return Path.Combine(Application.StartupPath, "Attachments")
    End Function

    Private Function SanitizeAttachmentFileName(name As String) As String
        If String.IsNullOrWhiteSpace(name) Then Return "File"
        Dim invalid = Path.GetInvalidFileNameChars()
        Dim sb As New StringBuilder(name.Length)
        For Each ch In name
            If Array.IndexOf(invalid, ch) >= 0 Then
                sb.Append("_"c)
            Else
                sb.Append(ch)
            End If
        Next
        Dim s = sb.ToString().Trim()
        Return If(s.Length = 0, "File", s)
    End Function

    ''' <summary>Copies a non-cheque-folder file into Attachments with Patient{id}_Pay{PayID}_Chq_* so it links to this payment.</summary>
    Private Function CopyPatientAttachmentLinkedToCheque(payId As Integer, sourcePath As String) As String
        If payId <= 0 OrElse _patientId <= 0 Then Throw New InvalidOperationException("Invalid payment")
        If String.IsNullOrWhiteSpace(sourcePath) OrElse Not File.Exists(sourcePath) Then Throw New InvalidOperationException("Invalid file")
        Dim root = GetPatientAttachmentsRootFolder()
        Directory.CreateDirectory(root)
        Dim ext = Path.GetExtension(sourcePath)
        If String.IsNullOrWhiteSpace(ext) Then ext = ".bin"
        Dim base = SanitizeAttachmentFileName(Path.GetFileNameWithoutExtension(sourcePath))
        Dim prefix = "Patient" & _patientId.ToString() & "_Pay" & payId.ToString() & "_Chq_"
        Dim fn = prefix & base & ext
        Dim dest = Path.Combine(root, fn)
        Dim n = 1
        While File.Exists(dest)
            fn = prefix & base & "_" & n.ToString() & ext
            dest = Path.Combine(root, fn)
            n += 1
        End While
        File.Copy(sourcePath, dest, overwrite:=False)
        If ChkDrive.Checked Then PatientDriveMirror.TryMirrorStartupRelativeFile(dest, GetChequeDriveOwnerLabel(), Me)
        Return dest
    End Function

    ''' <summary>0 = cancel, 1 = use as one side only, 2 = second page (scan or pick second file).</summary>
    Private Function PromptSinglePageAfterFrontBackChoice(forBrowseFollowUp As Boolean) As Integer
        Dim result As Integer = 0
        Using f As New Form()
            f.Text = If(Eng, "Front + back — one page only", "أمام وخلف — صفحة واحدة فقط")
            f.FormBorderStyle = FormBorderStyle.FixedDialog
            f.StartPosition = FormStartPosition.CenterParent
            f.MinimizeBox = False
            f.MaximizeBox = False
            f.ShowInTaskbar = False
            If Not Eng Then f.RightToLeft = RightToLeft.Yes
            f.ClientSize = New Size(440, 200)

            Dim body As String
            If forBrowseFollowUp Then
                body = If(Eng,
                    "Only one image was selected for front+back. Pick a second image for the other side, or use this as a single-side cheque.",
                    "تم اختيار صورة واحدة فقط لوضع أمام وخلف. اختر صورة ثانية للجهة الأخرى، أو استخدم وجها واحدا فقط.")
            Else
                body = If(Eng,
                    "Only one page was scanned. Flip the cheque and scan the other side, or keep this as a single-side image.",
                    "تم مسح صفحة واحدة فقط. اقلب الشيك وامسح الصفحة الأخرى، أو اكتفِ بوجه واحد.")
            End If

            Dim lbl As New Label With {
                .AutoSize = False,
                .Location = New Point(12, 12),
                .Size = New Size(416, 72),
                .Font = _chqUiFont,
                .Text = body
            }

            Dim btnCancel As New Button With {
                .Text = If(Eng, "Cancel", "إلغاء"),
                .DialogResult = DialogResult.Cancel,
                .Font = _chqUiFont,
                .Location = New Point(12, 152),
                .Size = New Size(90, 28)
            }
            Dim btnOne As New Button With {
                .Text = If(Eng, "Use as one side only", "استخدام وجه واحد فقط"),
                .Font = _chqUiFont,
                .Location = New Point(110, 152),
                .Size = New Size(150, 28)
            }
            Dim btnSecond As New Button With {
                .Text = If(Eng, If(forBrowseFollowUp, "Choose second image…", "Scan second side"), If(forBrowseFollowUp, "اختر الصورة الثانية…", "مسح الصفحة الثانية")),
                .Font = _chqUiFont,
                .Location = New Point(268, 152),
                .Size = New Size(160, 28)
            }

            AddHandler btnOne.Click, Sub(sender, args)
                                         result = 1
                                         f.Close()
                                     End Sub
            AddHandler btnSecond.Click, Sub(sender, args)
                                            result = 2
                                            f.Close()
                                        End Sub

            f.Controls.Add(lbl)
            f.Controls.Add(btnCancel)
            f.Controls.Add(btnOne)
            f.Controls.Add(btnSecond)
            f.CancelButton = btnCancel
            f.ShowDialog(Me)
        End Using
        Return result
    End Function

    Private Sub TryScanSecondPageForPendingAdd(parentHandle As IntPtr)
        ShowChqMessage(
            If(Eng, "Flip the cheque and place the other side on the scanner, then click OK to scan the second page.",
               "اقلب الشيك وضع الجهة الأخرى على الماسح، ثم اضغط موافق لمسح الصفحة الثانية."),
            "Scan", MessageBoxButtons.OK, MessageBoxIcon.Information)

        Dim folder = GetPatientScannedFolder()
        If String.IsNullOrEmpty(folder) Then Return
        If Not Directory.Exists(folder) Then Directory.CreateDirectory(folder)

        Dim files2 = TwainOperations.ScanImagesToFolder(ImageType:=".jpg",
                                                        CloseScannerUIAfterImageTransfer:=True,
                                                        ScannerInfo:="",
                                                        DestinationFolder:=folder,
                                                        ParentWindowHandle:=parentHandle)
        If files2 Is Nothing OrElse files2.Count = 0 Then
            ShowChqMessage(
                If(Eng, "No second page was scanned. Your first scan is kept; mode is set to ""One side"" — choose Front or Back and click Add.",
                   "لم يتم مسح صفحة ثانية. بقي المسح الأول؛ تم الانتقال إلى «وجه واحد» — اختر أمام/خلف ثم اضغط إضافة."),
                "Scan", MessageBoxButtons.OK, MessageBoxIcon.Information)
            _suppressRadioScanModeEvents = True
            Try
                radioChequeScanMode.EditValue = ChequeScanSheetMode.OneSide
            Finally
                _suppressRadioScanModeEvents = False
            End Try
            UpdateChequeScanModeUi()
            _pendingChequeImageSecondPath = Nothing
            _pendingChequeImageSecondIsDisposableScan = False
            Return
        End If

        _pendingChequeImageSecondPath = files2(0)
        _pendingChequeImageSecondIsDisposableScan = True
        Dim keepS = Path.GetFullPath(files2(0))
        For Each f2 In files2
            If String.IsNullOrWhiteSpace(f2) Then Continue For
            Try
                If Not String.Equals(Path.GetFullPath(f2), keepS, StringComparison.OrdinalIgnoreCase) Then File.Delete(f2)
            Catch
            End Try
        Next
        Dim msg2 = If(Eng, "Two pages scanned (front and back). Click Add to save the payment and link both images.", "تم مسح صفحتين (الأمام والخلف). اضغط إضافة لحفظ الدفعة وربط الصورتين.")
        ShowChqMessage(msg2, "Scan", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    ''' <summary>Maps multiselect Browse to pending cheque images (1–2) and extra attachment paths.</summary>
    Private Function ProcessBrowseSelectionsForAdd(paths As String()) As Boolean
        ResetChequeImageCaptureState()
        Dim ordered = paths.
            Where(Function(p) Not String.IsNullOrWhiteSpace(p) AndAlso File.Exists(p)).
            Select(Function(p) Path.GetFullPath(p)).
            GroupBy(Function(p) p.ToLowerInvariant()).
            Select(Function(g) g.First()).
            OrderBy(Function(p) Path.GetFileName(p), StringComparer.OrdinalIgnoreCase).
            ToList()
        If ordered.Count = 0 Then
            ShowChqMessage(If(Eng, "No existing files were selected.", "لم يُختر ملفات موجودة."), "Cheques", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return False
        End If
        Dim images = ordered.Where(AddressOf IsChequeImageFile).ToList()
        Dim others = ordered.Where(Function(p) Not IsChequeImageFile(p)).ToList()
        Dim mode = GetChequeScanSheetMode()

        If mode = ChequeScanSheetMode.FrontBack Then
            If images.Count >= 2 Then
                _pendingChequeImageSourcePath = images(0)
                _pendingChequeImageSecondPath = images(1)
                _pendingChequeImageIsDisposableScan = False
                _pendingChequeImageSecondIsDisposableScan = False
                _pendingAttachmentExtraPaths.AddRange(images.Skip(2))
                _pendingAttachmentExtraPaths.AddRange(others)
                Return True
            End If
            If images.Count = 1 AndAlso others.Count = 0 Then
                Dim browseChoice = PromptSinglePageAfterFrontBackChoice(forBrowseFollowUp:=True)
                If browseChoice = 0 Then Return False
                If browseChoice = 1 Then
                    _pendingChequeImageSourcePath = images(0)
                    _pendingChequeImageSecondPath = Nothing
                    _pendingChequeImageIsDisposableScan = False
                    _pendingChequeImageSecondIsDisposableScan = False
                    _suppressRadioScanModeEvents = True
                    Try
                        radioChequeScanMode.EditValue = ChequeScanSheetMode.OneSide
                    Finally
                        _suppressRadioScanModeEvents = False
                    End Try
                    UpdateChequeScanModeUi()
                    Return True
                End If
                Using dlgSecond As New OpenFileDialog()
                    dlgSecond.Title = If(Eng, "Choose second image (back of cheque)", "اختر الصورة الثانية (خلف الشيك)")
                    dlgSecond.Filter = If(Eng,
                        "Images (*.jpg;*.jpeg;*.png;*.bmp;*.tif;*.tiff)|*.jpg;*.jpeg;*.png;*.bmp;*.tif;*.tiff|PDF (*.pdf)|*.pdf|All files|*.*",
                        "صور|*.jpg;*.jpeg;*.png;*.bmp;*.tif;*.tiff|PDF|*.pdf|الملفات|*.*")
                    dlgSecond.Multiselect = False
                    If dlgSecond.ShowDialog(Me) <> DialogResult.OK OrElse String.IsNullOrWhiteSpace(dlgSecond.FileName) OrElse Not File.Exists(dlgSecond.FileName) Then Return False
                    Dim p2 = Path.GetFullPath(dlgSecond.FileName)
                    If Not IsChequeImageFile(p2) Then
                        ShowChqMessage(If(Eng, "Please choose an image file for the second side.", "يرجى اختيار ملف صورة للجهة الثانية."),
                                       "Cheques", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Return False
                    End If
                    If String.Equals(p2, images(0), StringComparison.OrdinalIgnoreCase) Then
                        ShowChqMessage(If(Eng, "The second image must be a different file than the first.", "يجب أن تكون الصورة الثانية ملفا مختلفا عن الأولى."),
                                       "Cheques", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Return False
                    End If
                    _pendingChequeImageSourcePath = images(0)
                    _pendingChequeImageSecondPath = p2
                    _pendingChequeImageIsDisposableScan = False
                    _pendingChequeImageSecondIsDisposableScan = False
                    Return True
                End Using
            End If
            If images.Count = 1 AndAlso others.Count > 0 Then
                ShowChqMessage(If(Eng, "Front + back needs two images. Remove extra documents or switch to ""One side"".", "وضع أمام وخلف يحتاج صورتين. أزل المستندات الإضافية أو انتقل إلى «وجه واحد»."),
                               "Cheques", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return False
            End If
            If images.Count = 0 AndAlso others.Count > 0 Then
                _pendingAttachmentExtraPaths.AddRange(others)
                Return True
            End If
            ShowChqMessage(If(Eng, "Could not use the selected files for front and back.", "تعذر استخدام الملفات المحددة لأمام وخلف."),
                           "Cheques", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return False
        End If

        If images.Count >= 1 Then
            _pendingChequeImageSourcePath = images(0)
            _pendingChequeImageSecondPath = Nothing
            _pendingChequeImageSecondIsDisposableScan = False
            _pendingChequeImageIsDisposableScan = False
            _pendingAttachmentExtraPaths.AddRange(images.Skip(1))
            _pendingAttachmentExtraPaths.AddRange(others)
            Return True
        End If
        If others.Count > 0 Then
            _pendingAttachmentExtraPaths.AddRange(others)
            Return True
        End If
        Return False
    End Function

    ''' <summary>Twain scan to a disposable file under the patient Cheques folder (add flow — PayID not yet allocated).</summary>
    Private Function RunScanPatientChequePending() As Boolean
        Try
            Dim folder = GetPatientScannedFolder()
            If String.IsNullOrEmpty(folder) Then Return False
            If Not Directory.Exists(folder) Then Directory.CreateDirectory(folder)

            Dim parentHandle As IntPtr = IntPtr.Zero
            Dim topForm = Me.FindForm()
            If topForm IsNot Nothing Then parentHandle = topForm.Handle

            Dim files = TwainOperations.ScanImagesToFolder(ImageType:=".jpg",
                                                          CloseScannerUIAfterImageTransfer:=True,
                                                          ScannerInfo:="",
                                                          DestinationFolder:=folder,
                                                          ParentWindowHandle:=parentHandle)
            If files Is Nothing OrElse files.Count = 0 Then
                ShowChqMessage(If(Eng, "No pages were scanned.", "لم يتم مسح أي صفحات."), "Scan", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return False
            End If

            Dim mode = GetChequeScanSheetMode()

            If mode = ChequeScanSheetMode.FrontBack Then
                If files.Count >= 2 Then
                    _pendingChequeImageSourcePath = files(0)
                    _pendingChequeImageSecondPath = files(1)
                    _pendingChequeImageIsDisposableScan = True
                    _pendingChequeImageSecondIsDisposableScan = True
                    Dim keep0 = Path.GetFullPath(files(0))
                    Dim keep1 = Path.GetFullPath(files(1))
                    For Each f In files
                        If String.IsNullOrWhiteSpace(f) Then Continue For
                        Dim full = Path.GetFullPath(f)
                        If String.Equals(full, keep0, StringComparison.OrdinalIgnoreCase) OrElse String.Equals(full, keep1, StringComparison.OrdinalIgnoreCase) Then Continue For
                        Try
                            File.Delete(f)
                        Catch
                        End Try
                    Next
                    Dim msg2 = If(Eng, "Two pages scanned (front and back). Click Add to save the payment and link both images.", "تم مسح صفحتين (الأمام والخلف). اضغط إضافة لحفظ الدفعة وربط الصورتين.")
                    ShowChqMessage(msg2, "Scan", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Return True
                End If
                Dim singleChoice = PromptSinglePageAfterFrontBackChoice(forBrowseFollowUp:=False)
                If singleChoice = 0 Then
                    For Each f In files
                        If String.IsNullOrWhiteSpace(f) Then Continue For
                        Try
                            File.Delete(f)
                        Catch
                        End Try
                    Next
                    Return False
                End If
                If singleChoice = 1 Then
                    _suppressRadioScanModeEvents = True
                    Try
                        radioChequeScanMode.EditValue = ChequeScanSheetMode.OneSide
                    Finally
                        _suppressRadioScanModeEvents = False
                    End Try
                    UpdateChequeScanModeUi()
                    _pendingChequeImageSourcePath = files(0)
                    _pendingChequeImageSecondPath = Nothing
                    _pendingChequeImageSecondIsDisposableScan = False
                    _pendingChequeImageIsDisposableScan = True
                    Dim fullFirst1 = Path.GetFullPath(files(0))
                    For Each f In files
                        If String.IsNullOrWhiteSpace(f) Then Continue For
                        Try
                            If Not String.Equals(Path.GetFullPath(f), fullFirst1, StringComparison.OrdinalIgnoreCase) Then
                                File.Delete(f)
                            End If
                        Catch
                        End Try
                    Next
                    Dim msgOne = If(Eng, "Cheque scanned. Choose Front or Back above if needed, then click Add to save and link this image.", "تم مسح الشيك. اختر أمام/خلف أعلاه إن لزم، ثم اضغط إضافة لحفظ الدفعة وربط الصورة.")
                    ShowChqMessage(msgOne, "Scan", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Return True
                End If
                _pendingChequeImageSourcePath = files(0)
                _pendingChequeImageSecondPath = Nothing
                _pendingChequeImageIsDisposableScan = True
                Dim keepOne = Path.GetFullPath(files(0))
                For Each f In files
                    If String.IsNullOrWhiteSpace(f) Then Continue For
                    Try
                        If Not String.Equals(Path.GetFullPath(f), keepOne, StringComparison.OrdinalIgnoreCase) Then File.Delete(f)
                    Catch
                    End Try
                Next
                TryScanSecondPageForPendingAdd(parentHandle)
                Return True
            End If

            ' One side: use first page only for Front/Back choice
            _pendingChequeImageSourcePath = files(0)
            _pendingChequeImageSecondPath = Nothing
            _pendingChequeImageSecondIsDisposableScan = False
            _pendingChequeImageIsDisposableScan = True
            Dim fullFirst = Path.GetFullPath(files(0))
            For Each f In files
                If String.IsNullOrWhiteSpace(f) Then Continue For
                Try
                    If Not String.Equals(Path.GetFullPath(f), fullFirst, StringComparison.OrdinalIgnoreCase) Then
                        File.Delete(f)
                    End If
                Catch
                End Try
            Next

            Dim msg = If(Eng, "Cheque scanned. Choose Front or Back above if needed, then click Add to save and link this image.", "تم مسح الشيك. اختر أمام/خلف أعلاه إن لزم، ثم اضغط إضافة لحفظ الدفعة وربط الصورة.")
            ShowChqMessage(msg, "Scan", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return True
        Catch ex As Exception
            ShowChqMessage(If(Eng, "Error while scanning: ", "خطأ أثناء المسح: ") & ex.Message, "Scan", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End Try
    End Function

    ''' <summary>Twain scan then link first page to Pay[payId]_Chq… (same naming as Browse).</summary>
    Private Function RunScanPatientChequeLinked(payId As Integer, chqNumberRaw As String) As Boolean
        If payId <= 0 Then
            ShowChqMessage(If(Eng, "Save the payment first so it has an ID.", "احفظ الدفعة أولا لتسجيل المعرف."),
                          "Scan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return False
        End If
        Dim n = NormalizeChqNumber(chqNumberRaw)
        If String.IsNullOrWhiteSpace(n) Then
            ShowChqMessage(If(Eng, "Cheque number is required to name the linked image.", "رقم الشيك مطلوب لتسمية الملف المربوط."),
                          "Scan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return False
        End If
        Try
            Dim folder = GetPatientScannedFolder()
            If String.IsNullOrEmpty(folder) Then Return False
            If Not Directory.Exists(folder) Then Directory.CreateDirectory(folder)

            Dim parentHandle As IntPtr = IntPtr.Zero
            Dim topForm = Me.FindForm()
            If topForm IsNot Nothing Then parentHandle = topForm.Handle

            Dim files = TwainOperations.ScanImagesToFolder(ImageType:=".jpg",
                                                          CloseScannerUIAfterImageTransfer:=True,
                                                          ScannerInfo:="",
                                                          DestinationFolder:=folder,
                                                          ParentWindowHandle:=parentHandle)
            If files Is Nothing OrElse files.Count = 0 Then
                ShowChqMessage(If(Eng, "No pages were scanned.", "لم يتم مسح أي صفحات."), "Scan", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return False
            End If

            Dim keepPaths As New HashSet(Of String)(StringComparer.OrdinalIgnoreCase)
            Dim modeScan = GetChequeScanSheetMode()
            Dim bothSidesLinked As Boolean = False
            Dim filesSecond As List(Of String) = Nothing

            If files.Count >= 2 Then
                Dim d0 = CopyPatientChequeLinked(payId, chqNumberRaw, files(0), ChequeImageFace.Front)
                If d0 Is Nothing Then
                    ShowChqOverwriteCancelled()
                    DeleteTwainScanSources(files)
                    Return False
                End If
                Dim d1 = CopyPatientChequeLinked(payId, chqNumberRaw, files(1), ChequeImageFace.Back)
                If d1 Is Nothing Then
                    ShowChqOverwriteCancelled()
                    DeleteTwainScanSources(files)
                    Return False
                End If
                keepPaths.Add(Path.GetFullPath(d0))
                keepPaths.Add(Path.GetFullPath(d1))
                bothSidesLinked = True
            ElseIf modeScan = ChequeScanSheetMode.FrontBack AndAlso files.Count = 1 Then
                Dim linkedChoice = PromptSinglePageAfterFrontBackChoice(forBrowseFollowUp:=False)
                If linkedChoice = 0 Then
                    For Each f In files
                        If String.IsNullOrWhiteSpace(f) Then Continue For
                        Try
                            File.Delete(f)
                        Catch
                        End Try
                    Next
                    Return False
                End If
                If linkedChoice = 1 Then
                    _suppressRadioScanModeEvents = True
                    Try
                        radioChequeScanMode.EditValue = ChequeScanSheetMode.OneSide
                    Finally
                        _suppressRadioScanModeEvents = False
                    End Try
                    UpdateChequeScanModeUi()
                    Dim d = CopyPatientChequeLinked(payId, chqNumberRaw, files(0), GetSelectedChequeImageFace())
                    If d Is Nothing Then
                        ShowChqOverwriteCancelled()
                        DeleteTwainScanSources(files)
                        Return False
                    End If
                    keepPaths.Add(Path.GetFullPath(d))
                Else
                    Dim d0 = CopyPatientChequeLinked(payId, chqNumberRaw, files(0), ChequeImageFace.Front)
                    If d0 Is Nothing Then
                        ShowChqOverwriteCancelled()
                        DeleteTwainScanSources(files)
                        Return False
                    End If
                    keepPaths.Add(Path.GetFullPath(d0))
                    ShowChqMessage(
                        If(Eng, "Flip the cheque and place the other side on the scanner, then click OK to scan the second page.",
                           "اقلب الشيك وضع الجهة الأخرى على الماسح، ثم اضغط موافق لمسح الصفحة الثانية."),
                        "Scan", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    filesSecond = TwainOperations.ScanImagesToFolder(ImageType:=".jpg",
                                                                  CloseScannerUIAfterImageTransfer:=True,
                                                                  ScannerInfo:="",
                                                                  DestinationFolder:=folder,
                                                                  ParentWindowHandle:=parentHandle)
                    If filesSecond IsNot Nothing AndAlso filesSecond.Count > 0 Then
                        Dim d1 = CopyPatientChequeLinked(payId, chqNumberRaw, filesSecond(0), ChequeImageFace.Back)
                        If d1 Is Nothing Then
                            ShowChqOverwriteCancelled()
                            DeleteTwainScanSources(filesSecond)
                            Return False
                        End If
                        keepPaths.Add(Path.GetFullPath(d1))
                        bothSidesLinked = True
                    Else
                        ShowChqMessage(If(Eng, "No second page was scanned. Only the front image was saved.", "لم يتم مسح صفحة ثانية. تم حفظ صورة الأمام فقط."),
                                       "Scan", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    End If
                End If
            Else
                Dim d = CopyPatientChequeLinked(payId, chqNumberRaw, files(0), GetSelectedChequeImageFace())
                If d Is Nothing Then
                    ShowChqOverwriteCancelled()
                    DeleteTwainScanSources(files)
                    Return False
                End If
                keepPaths.Add(Path.GetFullPath(d))
            End If
            For Each f In files
                If String.IsNullOrWhiteSpace(f) Then Continue For
                Try
                    If Not keepPaths.Contains(Path.GetFullPath(f)) Then File.Delete(f)
                Catch
                End Try
            Next
            If filesSecond IsNot Nothing Then
                For Each f In filesSecond
                    If String.IsNullOrWhiteSpace(f) Then Continue For
                    Try
                        If Not keepPaths.Contains(Path.GetFullPath(f)) Then File.Delete(f)
                    Catch
                    End Try
                Next
            End If

            Dim msg = If(Eng, "Cheque scanned and linked to payment.", "تم مسح الشيك وربطه بالدفعة.")
            If files.Count >= 2 OrElse bothSidesLinked Then
                msg &= If(Eng, " Front and back images saved.", " تم حفظ صورة الأمام والخلف.")
            End If
            ShowChqMessage(msg, "Scan", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return True
        Catch ex As Exception
            ShowChqMessage(If(Eng, "Error while scanning: ", "خطأ أثناء المسح: ") & ex.Message, "Scan", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End Try
    End Function

    Private Sub btnBrowse_Click(sender As Object, e As EventArgs) Handles btnBrowse.Click
        If _patientId <= 0 Then
            Dim msgEn = "Please select a patient first."
            Dim msgAr = "يرجى اختيار المريض أولا."
            ShowChqMessage(If(Eng, msgEn, msgAr), "Cheques", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If
        Dim folder = GetPatientScannedFolder()
        If String.IsNullOrEmpty(folder) Then Return

        If String.Compare(_payType, "Cheque", StringComparison.OrdinalIgnoreCase) <> 0 Then
            If Not Directory.Exists(folder) Then Directory.CreateDirectory(folder)
            OpenScanFolder(folder)
            Return
        End If

        ' Edit: link image to saved cheque payment that has no Pay*_Chq file yet
        If _pay IsNot Nothing AndAlso _pay.PayID > 0 AndAlso Not HasLinkedPatientChequeImage(_pay.PayID) Then
            If Not ValidateCheque() Then Return
            If Not Directory.Exists(folder) Then Directory.CreateDirectory(folder)
            Using dlg As New OpenFileDialog()
                dlg.Title = If(Eng, "Link cheque image(s) to this payment", "ربط صورة الشيك بهذه الدفعة")
                dlg.Filter = If(Eng,
                    "Images (*.jpg;*.jpeg;*.png;*.bmp;*.tif;*.tiff)|*.jpg;*.jpeg;*.png;*.bmp;*.tif;*.tiff|PDF (*.pdf)|*.pdf|All files|*.*",
                    "صور|*.jpg;*.jpeg;*.png;*.bmp;*.tif;*.tiff|PDF|*.pdf|الملفات|*.*")
                dlg.Multiselect = True
                If dlg.ShowDialog(Me) <> DialogResult.OK OrElse dlg.FileNames Is Nothing OrElse dlg.FileNames.Length = 0 Then Return
                Dim ordered = dlg.FileNames.Where(Function(p) Not String.IsNullOrWhiteSpace(p) AndAlso File.Exists(p)).Select(Function(p) Path.GetFullPath(p)).GroupBy(Function(p) p.ToLowerInvariant()).Select(Function(g) g.First()).OrderBy(Function(p) Path.GetFileName(p), StringComparer.OrdinalIgnoreCase).ToList()
                If ordered.Count = 0 Then Return
                Dim imgs = ordered.Where(AddressOf IsChequeImageFile).ToList()
                Dim others = ordered.Where(Function(p) Not IsChequeImageFile(p)).ToList()
                Try
                    If imgs.Count >= 2 Then
                        Dim r0 = CopyPatientChequeLinked(_pay.PayID, txtChqNumber.Text.Trim(), imgs(0), ChequeImageFace.Front)
                        If r0 Is Nothing Then
                            ShowChqOverwriteCancelled()
                            Return
                        End If
                        Dim r1 = CopyPatientChequeLinked(_pay.PayID, txtChqNumber.Text.Trim(), imgs(1), ChequeImageFace.Back)
                        If r1 Is Nothing Then
                            ShowChqOverwriteCancelled()
                            Return
                        End If
                        For i = 2 To imgs.Count - 1
                            CopyPatientAttachmentLinkedToCheque(_pay.PayID, imgs(i))
                        Next
                    ElseIf imgs.Count = 1 Then
                        Dim r = CopyPatientChequeLinked(_pay.PayID, txtChqNumber.Text.Trim(), imgs(0), GetSelectedChequeImageFace())
                        If r Is Nothing Then
                            ShowChqOverwriteCancelled()
                            Return
                        End If
                    End If
                    For Each p In others
                        CopyPatientAttachmentLinkedToCheque(_pay.PayID, p)
                    Next
                    ShowChqMessage(If(Eng, "File(s) linked to this payment.", "تم ربط الملفات بهذه الدفعة."),
                                    "Cheques", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    ApplyChequeImageCaptureUiLock()
                Catch ex As Exception
                    ShowChqMessage(If(Eng, "Could not save file: ", "تعذر حفظ الملف: ") & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                End Try
            End Using
            Return
        End If

        ' Add: choose file(s) after validation — payment is committed on Add (same pattern as Scan)
        If _pay Is Nothing Then
            If Not ValidateChequeAndPayAmountForPendingImage() Then Return
            If Not Directory.Exists(folder) Then Directory.CreateDirectory(folder)
            Using dlg As New OpenFileDialog()
                dlg.Title = If(Eng, "Attach cheque image(s) / documents", "إرفاق صورة الشيك أو مستندات")
                dlg.Filter = If(Eng,
                    "Images (*.jpg;*.jpeg;*.png;*.bmp;*.tif;*.tiff)|*.jpg;*.jpeg;*.png;*.bmp;*.tif;*.tiff|PDF (*.pdf)|*.pdf|All files|*.*",
                    "صور|*.jpg;*.jpeg;*.png;*.bmp;*.tif;*.tiff|PDF|*.pdf|الملفات|*.*")
                dlg.Multiselect = True
                If dlg.ShowDialog(Me) <> DialogResult.OK OrElse dlg.FileNames Is Nothing OrElse dlg.FileNames.Length = 0 Then Return
                If Not ProcessBrowseSelectionsForAdd(dlg.FileNames) Then Return
                ShowChqMessage(If(Eng, "Files selected. Click Add to save the payment and link them.", "تم اختيار الملفات. اضغط إضافة لحفظ الدفعة وربطها."),
                                "Cheques", MessageBoxButtons.OK, MessageBoxIcon.Information)
                ApplyChequeImageCaptureUiLock()
            End Using
            Return
        End If

        If Not Directory.Exists(folder) Then Directory.CreateDirectory(folder)
        OpenScanFolder(folder)
    End Sub

    Private Sub btnScan_Click(sender As Object, e As EventArgs) Handles btnScan.Click
        If _patientId <= 0 Then
            Dim msgEn = "Please select a patient first."
            Dim msgAr = "يرجى اختيار المريض أولا."
            ShowChqMessage(If(Eng, msgEn, msgAr), "Scan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        If String.Compare(_payType, "Cheque", StringComparison.OrdinalIgnoreCase) <> 0 Then Return

        If _pay IsNot Nothing AndAlso _pay.PayID > 0 Then
            If Not ValidateCheque() Then Return
            If RunScanPatientChequeLinked(_pay.PayID, txtChqNumber.Text.Trim()) Then
                ApplyChequeImageCaptureUiLock()
            End If
            Return
        End If

        ' Add: scan is stored until user clicks Add (same as Browse)
        If Not ValidateChequeAndPayAmountForPendingImage() Then Return
        If RunScanPatientChequePending() Then
            ApplyChequeImageCaptureUiLock()
        End If
    End Sub

    Private Sub btnScanAndPay_Click(sender As Object, e As EventArgs) Handles btnScanAndPay.Click
        If String.Compare(_payType, "Cheque", StringComparison.OrdinalIgnoreCase) <> 0 Then
            ShowChqMessage(If(Eng,
                      "Please choose Cheque as Pay Type before Scan & Pay.",
                      "يرجى اختيار نوع الدفع شيك قبل المسح والدفع."),
                      If(Eng, "Scan & Pay", "المسح والدفع"), MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If
        If Not ValidateCheque() Then Return

        If btnAddPay.Visible Then
            Dim newId = TryCommitAddPaymentAndGetPayId()
            If Not newId.HasValue Then Return
            If Not RunScanPatientChequeLinked(newId.Value, txtChqNumber.Text.Trim()) Then Return
            FormManager.Instance.CurrentForm.UpdatePatientBalance(CurrentPatient)
            Me.DialogResult = DialogResult.OK
            Me.Close()
        ElseIf btnSave.Visible Then
            If Not TrySaveEditPaymentInternal() Then Return
            If Not RunScanPatientChequeLinked(_pay.PayID, txtChqNumber.Text.Trim()) Then Return
            Me.DialogResult = DialogResult.OK
            Me.Close()
        End If
    End Sub

    Private Sub chkIsForward_CheckedChanged(sender As Object, e As EventArgs) Handles chkIsForward.CheckedChanged
        If chkIsForward.Checked Then
            lblForward.Visible = True
            txtForwardTo.Visible = True
        Else
            lblForward.Visible = False
            txtForwardTo.Visible = False
        End If
    End Sub
End Class
