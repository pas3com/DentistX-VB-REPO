Imports System.Linq
Imports System.Globalization
Imports System.Threading.Tasks
Imports System.Windows.Forms
Imports DevExpress.XtraEditors.Controls

''' <summary>Send WhatsApp to a lab; lab lists only include rows with a usable WhatsApp number (same check as SnapShotSender recipients).</summary>
Partial Public Class FrmLabSendWhats

    Private ReadOnly _service As New WhatsAppService()
    Private ReadOnly _subjectRows As New List(Of LabMsgSubject)()
    Private _detailChoices As New List(Of LabMsgDetailChoice)()
    Private _clinicIdValue As String = ""

    ''' <summary>Clinic ID (Guid string) for the WhatsApp API.</summary>
    Public Property ClinicId As String
        Get
            Return _clinicIdValue
        End Get
        Set(value As String)
            _clinicIdValue = If(value, "").Trim()
        End Set
    End Property

    Public Sub New()
        Try
            InitializeComponent()
            ClinicId = WhatsAppService.GetCurrentClinicId()
            BindLabLookUpWhatsOnly()
            BindSubjectLookUp()
            LoadCurrentClinicName()
            dateReceive.EditValue = Date.Today
            RadioLang.SelectedIndex = 0
            ApplyMessageDirection()
        Catch ex As Exception
            ApptErrorHelper.Report(ex,
                                   "FrmLabSendWhats.New",
                                   showUser:=True,
                                   owner:=Me,
                                   englishMessage:="The lab WhatsApp window could not be initialized.",
                                   arabicMessage:="تعذر تهيئة نافذة واتساب المختبر.")
        End Try
    End Sub

    Private ReadOnly Property UseMessageEnglish As Boolean
        Get
            If RadioLang Is Nothing Then Return False
            Return RadioLang.SelectedIndex <> 0
        End Get
    End Property

    Private Function GetSelectedLabId() As Integer
        Dim ev = lookUpSource.EditValue
        If ev Is Nothing OrElse IsDBNull(ev) Then Return 0
        Try
            Return Convert.ToInt32(ev)
        Catch ex As Exception
            ApptErrorHelper.Report(ex, "FrmLabSendWhats.GetSelectedLabId", showUser:=False, owner:=Me)
            Return 0
        End Try
    End Function

    Private Function GetSelectedSubjectId() As Integer
        Dim ev = lookUpSubject.EditValue
        If ev Is Nothing OrElse IsDBNull(ev) Then Return 0
        Try
            Return Convert.ToInt32(ev)
        Catch ex As Exception
            ApptErrorHelper.Report(ex, "FrmLabSendWhats.GetSelectedSubjectId", showUser:=False, owner:=Me)
            Return 0
        End Try
    End Function

    Private Function GetSelectedSubjectRow() As LabMsgSubject
        Dim subjectId = GetSelectedSubjectId()
        Return _subjectRows.FirstOrDefault(Function(x) x.LabMsgSubjectID = subjectId)
    End Function

    Private Function GetCheckedDetailChoiceIds() As List(Of Integer)
        Dim selected As New List(Of Integer)()
        For i As Integer = 0 To _detailChoices.Count - 1
            If checkedDetails.GetItemChecked(i) Then
                selected.Add(_detailChoices(i).LabMsgDetailChoiceID)
            End If
        Next
        Return selected
    End Function

    Private Function GetSelectedDetailChoices() As List(Of LabMsgDetailChoice)
        Dim selected As New List(Of LabMsgDetailChoice)()
        For i As Integer = 0 To _detailChoices.Count - 1
            If checkedDetails.GetItemChecked(i) Then
                selected.Add(_detailChoices(i))
            End If
        Next
        Return selected
    End Function

    Private Function GetReceiveDateValue() As Nullable(Of Date)
        If dateReceive.EditValue Is Nothing OrElse IsDBNull(dateReceive.EditValue) Then Return Nothing
        Try
            Return Convert.ToDateTime(dateReceive.EditValue)
        Catch ex As Exception
            ApptErrorHelper.Report(ex, "FrmLabSendWhats.GetReceiveDateValue", showUser:=False, owner:=Me)
            Return Nothing
        End Try
    End Function

    ''' <summary>Same filtering as SnapShotSender btnAddRecipient (usable WhatsApp digits).</summary>
    Private Shared Function LabHasUsableWhats(l As Lab) As Boolean
        If l Is Nothing Then Return False
        Dim digits = WhatsHelper.BuildInternationalWhatsDigits(If(l.WhatsApp, ""), If(l.WhatsAppPrefix, ""))
        Return Not String.IsNullOrWhiteSpace(digits) AndAlso digits.Length >= 8
    End Function

    Private Sub BindLabLookUpWhatsOnly()
        Try
            Dim data As New LabDATA()
            Dim list = data.SelectAll().
                Where(AddressOf LabHasUsableWhats).
                Select(Function(l) New LabPick With {.Id = l.LabID, .Name = If(l.LabName, "")}).
                OrderBy(Function(x) x.Name).
                ToList()
            lookUpSource.Properties.DataSource = list
            lookUpSource.Properties.DisplayMember = NameOf(LabPick.Name)
            lookUpSource.Properties.ValueMember = NameOf(LabPick.Id)
            lookUpSource.Properties.Columns.Clear()
            lookUpSource.Properties.Columns.Add(New LookUpColumnInfo(NameOf(LabPick.Name), If(Eng, "Lab", "مختبر")) With {.Width = 300})
            lookUpSource.Properties.NullText = ""
        Catch ex As Exception
            ApptErrorHelper.Report(ex,
                                   "FrmLabSendWhats.BindLabLookUpWhatsOnly",
                                   showUser:=True,
                                   owner:=Me,
                                   englishMessage:="Could not load labs with WhatsApp numbers.",
                                   arabicMessage:="تعذر تحميل المختبرات التي لديها أرقام واتساب.")
        End Try
    End Sub

    Private Sub BindSubjectLookUp()
        Try
            Dim currentSubjectId = GetSelectedSubjectId()
            _subjectRows.Clear()
            _subjectRows.AddRange(New LabMsgSubjectDATA().SelectActive().OrderBy(Function(x) x.SortOrder).ThenBy(Function(x) x.LabMsgSubjectID))
            lookUpSubject.Properties.DataSource = _subjectRows
            lookUpSubject.Properties.DisplayMember = NameOf(LabMsgSubject.SubjectName)
            lookUpSubject.Properties.ValueMember = NameOf(LabMsgSubject.LabMsgSubjectID)
            lookUpSubject.Properties.Columns.Clear()
            lookUpSubject.Properties.Columns.Add(New LookUpColumnInfo(NameOf(LabMsgSubject.SubjectName), If(Eng, "Subject", "الموضوع")) With {.Width = 260})
            lookUpSubject.Properties.NullText = ""
            If currentSubjectId > 0 AndAlso _subjectRows.Any(Function(x) x.LabMsgSubjectID = currentSubjectId) Then
                lookUpSubject.EditValue = currentSubjectId
            End If
        Catch ex As Exception
            ApptErrorHelper.Report(ex,
                                   "FrmLabSendWhats.BindSubjectLookUp",
                                   showUser:=True,
                                   owner:=Me,
                                   englishMessage:="Could not load lab message subjects.",
                                   arabicMessage:="تعذر تحميل مواضيع رسائل المختبر.")
        End Try
    End Sub

    Private Sub BindDetailChoices()
        Try
            Dim subjectId = GetSelectedSubjectId()
            Dim selectedChoiceIds = GetCheckedDetailChoiceIds()
            If subjectId <= 0 Then
                _detailChoices = New List(Of LabMsgDetailChoice)()
            Else
                _detailChoices = New LabMsgDetailChoiceDATA().SelectActiveBySubject(subjectId).ToList()
            End If

            checkedDetails.DataSource = Nothing
            checkedDetails.Items.Clear()
            checkedDetails.DisplayMember = NameOf(LabMsgDetailChoice.DetailText)
            checkedDetails.ValueMember = NameOf(LabMsgDetailChoice.LabMsgDetailChoiceID)
            checkedDetails.DataSource = _detailChoices
            For i As Integer = 0 To _detailChoices.Count - 1
                checkedDetails.SetItemChecked(i, selectedChoiceIds.Contains(_detailChoices(i).LabMsgDetailChoiceID))
            Next
        Catch ex As Exception
            ApptErrorHelper.Report(ex,
                                   "FrmLabSendWhats.BindDetailChoices",
                                   showUser:=True,
                                   owner:=Me,
                                   englishMessage:="Could not load the message detail choices.",
                                   arabicMessage:="تعذر تحميل خيارات تفاصيل الرسالة.")
        End Try
    End Sub

    Private Sub LoadCurrentClinicName()
        If String.IsNullOrWhiteSpace(ClinicId) Then Return

        Try
            Dim clinicGuid As Guid
            If Not Guid.TryParse(ClinicId, clinicGuid) Then Return

            Dim clinic = New ClinicDATA().Select_Record(clinicGuid)
            If clinic Is Nothing Then Return

            Dim displayName = If(Not String.IsNullOrWhiteSpace(clinic.ClinicNameAr), clinic.ClinicNameAr, clinic.ClinicNameEn)
            If String.IsNullOrWhiteSpace(displayName) Then
                displayName = If(Not String.IsNullOrWhiteSpace(clinic.DrNameAr), clinic.DrNameAr, clinic.DrNameEn)
            End If
            txtClinicName.Text = If(displayName, "").Trim()
        Catch ex As Exception
            ApptErrorHelper.Report(ex, "FrmLabSendWhats.LoadCurrentClinicName", showUser:=False, owner:=Me)
        End Try
    End Sub

    Private Sub BuildMessagePreview()
        ApplyMessageDirection()
        TxtSendMessage.Text = BuildMessageText()
    End Sub

    Private Sub ApplyMessageDirection()
        TxtSendMessage.RightToLeft = If(UseMessageEnglish, RightToLeft.No, RightToLeft.Yes)
        TxtSendMessage.Properties.Appearance.Options.UseTextOptions = True
        TxtSendMessage.Properties.Appearance.TextOptions.HAlignment = If(UseMessageEnglish, DevExpress.Utils.HorzAlignment.Near, DevExpress.Utils.HorzAlignment.Far)
    End Sub

    Private Function LocalizeSubjectText(rawText As String) As String
        Dim value = If(rawText, "").Trim()
        Select Case value
            Case "إعادة طباعة مؤقت"
                Return If(UseMessageEnglish, "Temporary Reprint", "إعادة طباعة مؤقت")
            Case "خراطة أسنان"
                Return If(UseMessageEnglish, "Teeth Lathe Work", "خراطة أسنان")
            Case "معلومات عامة"
                Return If(UseMessageEnglish, "General Information", "معلومات عامة")
            Case Else
                Return value
        End Select
    End Function

    Private Function LocalizeDetailText(rawText As String) As String
        Dim value = If(rawText, "").Trim()
        Select Case value
            Case "طباعة مؤقت علوي - جزئي"
                Return If(UseMessageEnglish, "Upper temporary print - partial", "طباعة مؤقت علوي - جزئي")
            Case "طباعة مؤقت علوي - كامل"
                Return If(UseMessageEnglish, "Upper temporary print - full", "طباعة مؤقت علوي - كامل")
            Case "طباعة مؤقت سفلي - جزئي"
                Return If(UseMessageEnglish, "Lower temporary print - partial", "طباعة مؤقت سفلي - جزئي")
            Case "طباعة مؤقت سفلي - كامل"
                Return If(UseMessageEnglish, "Lower temporary print - full", "طباعة مؤقت سفلي - كامل")
            Case "خراطة دائم علوي - جزئي"
                Return If(UseMessageEnglish, "Upper permanent lathe - partial", "خراطة دائم علوي - جزئي")
            Case "خراطة دائم علوي - كامل"
                Return If(UseMessageEnglish, "Upper permanent lathe - full", "خراطة دائم علوي - كامل")
            Case "خراطة دائم سفلي - جزئي"
                Return If(UseMessageEnglish, "Lower permanent lathe - partial", "خراطة دائم سفلي - جزئي")
            Case "خراطة دائم سفلي - كامل"
                Return If(UseMessageEnglish, "Lower permanent lathe - full", "خراطة دائم سفلي - كامل")
            Case "ملاحظات عامة"
                Return If(UseMessageEnglish, "General notes", "ملاحظات عامة")
            Case Else
                Return value
        End Select
    End Function

    Private Function GetSelectedPatientName() As String
        Return If(patientCombo.PatientName, "").Trim()
    End Function

    Private Function GetSelectedPatientId() As Nullable(Of Integer)
        If patientCombo.PatientID > 0 Then Return patientCombo.PatientID
        Return Nothing
    End Function

    Private Function BuildMessageText() As String
        Dim lines As New List(Of String)()
        Dim clinicTitle = If(txtClinicName.Text, "").Trim()
        Dim subjectRow = GetSelectedSubjectRow()
        Dim selectedDetails = GetSelectedDetailChoices()
        Dim patientName = GetSelectedPatientName()
        Dim selectedSubjectText = If(subjectRow IsNot Nothing, subjectRow.SubjectName, If(lookUpSubject.Text, "").Trim())
        Dim receiveDateValue = GetReceiveDateValue()
        Dim noteText = If(txtNote.Text, "").Trim()

        If Not String.IsNullOrWhiteSpace(clinicTitle) Then lines.Add(clinicTitle)

        If Not String.IsNullOrWhiteSpace(selectedSubjectText) Then
            If lines.Count > 0 Then lines.Add("")
            lines.Add(LocalizeSubjectText(selectedSubjectText))
        End If

        If selectedDetails.Count > 0 Then
            For Each choice In selectedDetails.OrderBy(Function(x) x.SortOrder).ThenBy(Function(x) x.LabMsgDetailChoiceID)
                lines.Add("- " & LocalizeDetailText(choice.DetailText))
            Next
        End If

        If Not String.IsNullOrWhiteSpace(patientName) Then
            If lines.Count > 0 Then lines.Add("")
            lines.Add(If(UseMessageEnglish, "For: ", "للمريض: ") & patientName)
        End If

        If receiveDateValue.HasValue Then
            Dim culture = CultureInfo.GetCultureInfo(If(UseMessageEnglish, "en-US", "ar"))
            lines.Add(If(UseMessageEnglish, "Day: ", "اليوم: ") & receiveDateValue.Value.ToString("dddd", culture))
            lines.Add(If(UseMessageEnglish, "Date: ", "التاريخ: ") & receiveDateValue.Value.ToString("dd/MM/yyyy"))
        End If

        If Not String.IsNullOrWhiteSpace(noteText) Then
            If lines.Count > 0 Then lines.Add("")
            lines.Add(If(UseMessageEnglish, "Note: ", "ملاحظة: ") & noteText)
        End If

        Return String.Join(Environment.NewLine, lines)
    End Function

    Private Function SaveSentLabMessage(labRow As Lab, message As String) As Integer
        Dim clinicGuidValue As Nullable(Of Guid) = Nothing
        Dim parsedGuid As Guid
        If Guid.TryParse(ClinicId, parsedGuid) Then clinicGuidValue = parsedGuid

        Dim subjectRow = GetSelectedSubjectRow()
        Dim header As New LabMsgs With {
            .ClinicID = clinicGuidValue,
            .ClinicName = If(txtClinicName.Text, "").Trim(),
            .LabID = labRow.LabID,
            .LabName = If(If(labRow.LabName, lookUpSource.Text), "").Trim(),
            .PatientID = GetSelectedPatientId(),
            .PatientName = GetSelectedPatientName(),
            .LabMsgSubjectID = If(subjectRow IsNot Nothing, subjectRow.LabMsgSubjectID, 0),
            .SubjectText = If(subjectRow IsNot Nothing, subjectRow.SubjectName, If(lookUpSubject.Text, "").Trim()),
            .ReceiveDate = GetReceiveDateValue(),
            .Note = If(txtNote.Text, "").Trim(),
            .MessageBody = message,
            .MsgDate = Date.Now,
            .SentDate = Date.Now,
            .IsSent = True
        }

        Dim msgId = New LabMsgsDATA().AddAndGetId(header)
        Dim detailData As New LabMsgDetailDATA()
        Dim selectedDetails = GetSelectedDetailChoices()
        Dim sortOrder As Integer = 1

        For Each choice In selectedDetails.OrderBy(Function(x) x.SortOrder).ThenBy(Function(x) x.LabMsgDetailChoiceID)
            detailData.Add(New LabMsgDetail With {
                .LabMsgID = msgId,
                .LabMsgDetailChoiceID = choice.LabMsgDetailChoiceID,
                .DetailText = choice.DetailText,
                .SortOrder = sortOrder
            })
            sortOrder += 1
        Next

        Return msgId
    End Function

    Private Sub ResetBuilderAfterSend()
        patientCombo.SetCurrentPatientName(-1)
        txtNote.Text = ""
        TxtSendMessage.Text = ""
        BindDetailChoices()
        dateReceive.EditValue = Date.Today
    End Sub

    Private NotInheritable Class LabPick
        Public Property Id As Integer
        Public Property Name As String
    End Class

    Private Async Sub btnWhatsSend_Click(sender As Object, e As EventArgs) Handles btnWhatsSend.Click
        Try
            Await SendLabWhatsMessageAsync().ConfigureAwait(True)
        Catch ex As Exception
            ApptErrorHelper.Report(ex,
                                   "FrmLabSendWhats.btnWhatsSend_Click",
                                   showUser:=True,
                                   owner:=Me,
                                   englishMessage:="Could not send the WhatsApp message.",
                                   arabicMessage:="تعذر إرسال رسالة واتساب.")
        End Try
    End Sub

    Private Async Function SendLabWhatsMessageAsync() As Task
        If String.IsNullOrWhiteSpace(ClinicId) Then
            MessageBox.Show(If(Eng, "No clinic is selected.", "لا توجد عيادة محددة."), If(Eng, "Send", "إرسال"), MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim labId = GetSelectedLabId()
        If labId <= 0 Then
            MessageBox.Show(If(Eng, "Choose a lab.", "اختر المختبر."), If(Eng, "Send", "إرسال"), MessageBoxButtons.OK, MessageBoxIcon.Warning)
            lookUpSource.Focus()
            Return
        End If

        If GetSelectedSubjectId() <= 0 Then
            MessageBox.Show(If(Eng, "Choose a subject.", "اختر الموضوع."), If(Eng, "Send", "إرسال"), MessageBoxButtons.OK, MessageBoxIcon.Warning)
            lookUpSubject.Focus()
            Return
        End If

        If String.IsNullOrWhiteSpace(GetSelectedPatientName()) Then
            MessageBox.Show(If(Eng, "Choose a patient.", "اختر المريض."), If(Eng, "Send", "إرسال"), MessageBoxButtons.OK, MessageBoxIcon.Warning)
            patientCombo.Select()
            Return
        End If

        Dim labRow As Lab = Nothing
        Try
            labRow = New LabDATA().Select_Record(New Lab With {.LabID = labId})
        Catch ex As Exception
            ApptErrorHelper.Report(ex,
                                   "FrmLabSendWhats.SendLabWhatsMessageAsync.LoadLab",
                                   showUser:=False,
                                   owner:=Me)
        End Try
        If labRow Is Nothing Then
            MessageBox.Show(If(Eng, "Could not load the lab record.", "تعذر تحميل بيانات المختبر."), If(Eng, "Send", "إرسال"), MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim number = WhatsHelper.BuildInternationalWhatsDigits(If(labRow.WhatsApp, ""), If(labRow.WhatsAppPrefix, "")).Trim()
        If String.IsNullOrWhiteSpace(number) OrElse number.Length < 8 Then
            MessageBox.Show(If(Eng, "This lab has no usable WhatsApp number.", "لا يوجد رقم واتساب صالح لهذا المختبر."), If(Eng, "Send", "إرسال"), MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        If String.IsNullOrWhiteSpace(If(TxtSendMessage.Text, "").Trim()) Then
            BuildMessagePreview()
        End If

        Dim message As String = If(TxtSendMessage.Text, "").Trim()
        If String.IsNullOrWhiteSpace(message) Then
            MessageBox.Show(If(Eng, "Build or enter a message first.", "قم بإنشاء الرسالة أو إدخالها أولاً."), If(Eng, "Send", "إرسال"), MessageBoxButtons.OK, MessageBoxIcon.Warning)
            TxtSendMessage.Focus()
            Return
        End If

        btnWhatsSend.Enabled = False
        btnBuildMessage.Enabled = False
        Try
            If Not Await WhatsAppService.EnsureWhatsAppConnectedOrNotifyAsync(Me).ConfigureAwait(True) Then Return

            Dim ctx As New WhatsAppSendContext With {
                .Category = WhatsAppMessageCategories.ManualSend,
                .SourceHint = NameOf(FrmLabSendWhats) & " · Lab#" & labId.ToString(),
                .DisplayName = If(labRow.LabName, lookUpSource.Text),
                .RevealMessageCenter = True
            }
            Await _service.SendMessageAsync(ClinicId, number, message, "", ctx).ConfigureAwait(True)

            Try
                SaveSentLabMessage(labRow, message)
            Catch ex As Exception
                ApptErrorHelper.Report(ex,
                                       "FrmLabSendWhats.SendLabWhatsMessageAsync.SaveSentLabMessage",
                                       showUser:=True,
                                       owner:=Me,
                                       englishMessage:="The message was queued, but saving it in history failed.",
                                       arabicMessage:="تم وضع الرسالة في الطابور، ولكن تعذر حفظها في السجل.")
            End Try

            MessageBox.Show(If(Eng, "Message queued for sending.", "تم وضع الرسالة في الطابور."), If(Eng, "Send", "إرسال"), MessageBoxButtons.OK, MessageBoxIcon.Information)
            ResetBuilderAfterSend()
        Catch ex As Exception
            ApptErrorHelper.Report(ex,
                                   "FrmLabSendWhats.SendLabWhatsMessageAsync",
                                   showUser:=True,
                                   owner:=Me,
                                   englishMessage:="Could not queue the WhatsApp message.",
                                   arabicMessage:="تعذر وضع رسالة واتساب في الطابور.",
                                   englishTitle:="Send error",
                                   arabicTitle:="خطأ في الإرسال")
        Finally
            btnWhatsSend.Enabled = True
            btnBuildMessage.Enabled = True
        End Try
    End Function

    Private Sub btnBuildMessage_Click(sender As Object, e As EventArgs) Handles btnBuildMessage.Click
        Try
            BuildMessagePreview()
        Catch ex As Exception
            ApptErrorHelper.Report(ex,
                                   "FrmLabSendWhats.btnBuildMessage_Click",
                                   showUser:=True,
                                   owner:=Me,
                                   englishMessage:="Could not build the message preview.",
                                   arabicMessage:="تعذر إنشاء معاينة الرسالة.")
        End Try
    End Sub

    Private Sub lookUpSubject_EditValueChanged(sender As Object, e As EventArgs) Handles lookUpSubject.EditValueChanged
        Try
            BindDetailChoices()
            BuildMessagePreview()
        Catch ex As Exception
            ApptErrorHelper.Report(ex, "FrmLabSendWhats.lookUpSubject_EditValueChanged", showUser:=False, owner:=Me)
        End Try
    End Sub

    Private Sub checkedDetails_ItemCheck(sender As Object, e As DevExpress.XtraEditors.Controls.ItemCheckEventArgs) Handles checkedDetails.ItemCheck
        Try
            BeginInvoke(New MethodInvoker(AddressOf BuildMessagePreview))
        Catch ex As Exception
            ApptErrorHelper.Report(ex, "FrmLabSendWhats.checkedDetails_ItemCheck", showUser:=False, owner:=Me)
        End Try
    End Sub

    Private Sub txtClinicName_EditValueChanged(sender As Object, e As EventArgs) Handles txtClinicName.EditValueChanged
        If Not String.IsNullOrWhiteSpace(TxtSendMessage.Text) Then BuildMessagePreview()
    End Sub

    Private Sub patientCombo_PatientValueChanged(sender As Object, e As PatientCombo.PatientIndexChangedEvent) Handles patientCombo.PatientValueChanged
        If Not String.IsNullOrWhiteSpace(TxtSendMessage.Text) Then BuildMessagePreview()
    End Sub

    Private Sub txtNote_EditValueChanged(sender As Object, e As EventArgs) Handles txtNote.EditValueChanged
        If Not String.IsNullOrWhiteSpace(TxtSendMessage.Text) Then BuildMessagePreview()
    End Sub

    Private Sub dateReceive_EditValueChanged(sender As Object, e As EventArgs) Handles dateReceive.EditValueChanged
        If Not String.IsNullOrWhiteSpace(TxtSendMessage.Text) Then BuildMessagePreview()
    End Sub

    Private Sub RadioLang_SelectedIndexChanged(sender As Object, e As EventArgs) Handles RadioLang.SelectedIndexChanged
        Try
            BuildMessagePreview()
        Catch ex As Exception
            ApptErrorHelper.Report(ex, "FrmLabSendWhats.RadioLang_SelectedIndexChanged", showUser:=False, owner:=Me)
        End Try
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Try
            Me.Close()
        Catch ex As Exception
            ApptErrorHelper.Report(ex,
                                   "FrmLabSendWhats.btnClose_Click",
                                   showUser:=True,
                                   owner:=Me,
                                   englishMessage:="Could not close the window cleanly.",
                                   arabicMessage:="تعذر إغلاق النافذة بشكل صحيح.")
        End Try
    End Sub

    Private Sub btnAddSubject_Click(sender As Object, e As EventArgs) Handles btnAddSubject.Click
        Using F As New FrmLabMsgSubject()
            F.ShowDialog()
        End Using
        BindSubjectLookUp()
        BindDetailChoices()
        BuildMessagePreview()
    End Sub

    Private Sub btnAddSubDetails_Click(sender As Object, e As EventArgs) Handles btnAddSubDetails.Click
        Using F As New FrmLabMsgDetailChoice()
            F.ShowDialog()
        End Using
        BindDetailChoices()
        BuildMessagePreview()
    End Sub

    Private Sub btnShowHistory_Click(sender As Object, e As EventArgs) Handles btnShowHistory.Click
        Try
            Using f As New FrmLabWhatsHistory()
                f.InitialLabId = GetSelectedLabId()
                f.ShowDialog(Me)
            End Using
        Catch ex As Exception
            ApptErrorHelper.Report(ex,
                                   "FrmLabSendWhats.btnShowHistory_Click",
                                   showUser:=True,
                                   owner:=Me,
                                   englishMessage:="Could not open the lab WhatsApp history.",
                                   arabicMessage:="تعذر فتح سجل واتساب المختبر.")
        End Try
    End Sub
End Class