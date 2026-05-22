Imports System.ComponentModel
Imports System.IO
Imports System.Linq
Imports System.Threading.Tasks
Imports DentistX
Imports DevExpress.XtraEditors

Public Class WhatsNormal

    Private _service As WhatsAppService
    Private _clinicIdValue As String = ""
    Private _clinicDisplayName As String = ""
    Private _queueBinding As BindingList(Of PendingMessageItem)
    Private _failedBinding As BindingList(Of FailedMessageItem)
    Private _repoQueueDelete As DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit
    Private ReadOnly _patient As Patient


    Public Property ClinicId As String
        Get
            Return _clinicIdValue
        End Get
        Set(value As String)
            _clinicIdValue = If(value, "").Trim()
        End Set
    End Property

    Public Sub New()
        Me.New(Nothing)
    End Sub

    Public Sub New(patient As Patient)
        InitializeComponent()
        _patient = patient
        _service = New WhatsAppService()
        _queueBinding = New BindingList(Of PendingMessageItem)
        _failedBinding = New BindingList(Of FailedMessageItem)
        ' BtnSendMessage_Click is already wired via Handles; no need for AddHandler here.
        AddHandler TxtSendFile.Properties.ButtonClick, AddressOf TxtSendFile_ButtonClick

        ' Ensure ClinicId is set so connection status works
        ClinicId = WhatsAppService.GetCurrentClinicId()
        FillCboPrefixOnce(cboPrefix)
        ' If we have a patient from Navigator, prebuild his WhatsApp prefix, local number and full number.
        If _patient IsNot Nothing Then
            ApplyPatientWhatsData(_patient)

            ' Enhance header text to include patient name
            Dim baseTextEn As String = "Send Normal WhatsApp Message With or Without Attachments"
            Dim baseTextAr As String = "إرسال رسالة واتساب عادية مع أو بدون مرفقات"
            Dim namePart As String = If(String.IsNullOrWhiteSpace(_patient.PatientName), "", $" - {_patient.PatientName}")
            Dim headerText As String = If(Eng, baseTextEn, baseTextAr) & namePart
            lblSendTo.Text = headerText
        End If

        AttachWhatsOutboundPendingButton()
    End Sub

    Private Sub AttachWhatsOutboundPendingButton()
        Dim btn As New SimpleButton With {.Text = If(Eng, "Pending (local)…", "معلّقة محليًا…")}
        btn.Size = BtnSendMessage.Size
        btn.Font = BtnSendMessage.Font
        Dim p = BtnSendMessage.Location
        btn.Location = New System.Drawing.Point(Math.Max(8, p.X - btn.Width - 10), p.Y)
        AddHandler btn.Click, Sub(s, ea)
                                   Dim cid = WhatsAppService.GetCurrentClinicId()
                                   If String.IsNullOrWhiteSpace(cid) Then cid = ClinicId
                                   Using f As New FrmWhatsOutboundPending(cid)
                                       f.ShowDialog(Me)
                                   End Using
                               End Sub
        Controls.Add(btn)
    End Sub

    ''' <summary>
    ''' Fills cboPrefix, txtWhats and lblWhats from Patient table values.
    ''' - Prefix is taken from Patient.WhatsAppPrefix and matched to the combo items by digits.
    ''' - txtWhats is filled from Patient.WhatsApp (or Phone fallback).
    ''' - lblWhats always shows the full international number (prefix + local, without leading 0).
    ''' </summary>
    Private Sub ApplyPatientWhatsData(p As Patient)
        If p Is Nothing Then Return
        WhatsHelper.BindPatientWhatsPrefixAndLocal(cboPrefix, txtWhats, p)
        RefreshLblWhats()
    End Sub

    Private Sub RefreshLblWhats()
        If lblWhats IsNot Nothing Then
            lblWhats.Text = WhatsHelper.BuildInternationalWhatsDigitsFromControls(cboPrefix, txtWhats)
        End If
    End Sub


    Private Sub TxtSendFile_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs)
        Using ofd As New OpenFileDialog()
            ' Make "All Files" the first option and allow multiple selection
            ofd.Filter = If(Eng,
                "All Files|*.*|PDF|*.pdf|Images|*.png;*.jpg;*.jpeg",
                "جميع الملفات|*.*|PDF|*.pdf|صور|*.png;*.jpg;*.jpeg")
            ofd.Multiselect = True
            If ofd.ShowDialog(Me) = DialogResult.OK Then
                ' Join multiple selected files with semicolon
                TxtSendFile.Text = String.Join(";", ofd.FileNames)
            End If
        End Using
    End Sub

    Private Async Sub BtnSendMessage_Click(sender As Object, e As EventArgs) Handles BtnSendMessage.Click
        'If String.IsNullOrWhiteSpace(ClinicId) Then
        '    MessageBox.Show("لا توجد عيادة محددة.", "إرسال", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        '    Return
        'End If
        Dim number As String = If(lblWhats.Text, "").Trim()
        If String.IsNullOrWhiteSpace(number) Then
            MessageBox.Show(
                If(Eng, "Enter the mobile number.", "أدخل رقم الجوال."),
                If(Eng, "Send", "إرسال"),
                MessageBoxButtons.OK, MessageBoxIcon.Warning)
            lblWhats.Focus()
            Return
        End If
        Dim message As String = If(TxtSendMessage.Text, "").Trim()
        Dim rawFiles As String = If(TxtSendFile.Text, "").Trim()

        ' Split multiple files (joined by ;) and validate each. If any file does not exist, show a warning and stop.
        Dim files As String() = {}
        If Not String.IsNullOrWhiteSpace(rawFiles) Then
            files = rawFiles.Split(";"c).
                Select(Function(p) p.Trim()).
                Where(Function(p) Not String.IsNullOrWhiteSpace(p)).
                ToArray()
            For Each f In files
                If Not IO.File.Exists(f) Then
                    MessageBox.Show(
                        If(Eng, "The selected file does not exist: ", "الملف المحدد غير موجود: ") & f,
                        If(Eng, "Send", "إرسال"),
                        MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Return
                End If
            Next
        End If
        BtnSendMessage.Enabled = False
        Try
            Dim clinicIdUse = WhatsAppService.GetCurrentClinicId()
            If String.IsNullOrWhiteSpace(clinicIdUse) Then clinicIdUse = ClinicId

            If Not Await WhatsAppService.EnsureWhatsAppConnectedOrNotifyAsync(Me).ConfigureAwait(True) Then
                BtnSendMessage.Enabled = True
                Return
            End If

            Dim ctx As New WhatsAppSendContext With {
                .Category = WhatsAppMessageCategories.NavigatorWhatsApp,
                .PatientId = If(_patient IsNot Nothing, CType(_patient.PatientID, Integer?), Nothing),
                .DisplayName = If(_patient IsNot Nothing, _patient.PatientName, Nothing),
                .SourceHint = NameOf(WhatsNormal),
                .RevealMessageCenter = True
            }

            If files.Length = 0 Then
                Dim resp = Await _service.SendMessageAsync(clinicIdUse, number, message, "", ctx).ConfigureAwait(True)
                Dim intr As WhatsAppOutboundSendInterpretation = Nothing
                If WhatsAppService.TryInterpretOutboundSendResponse(resp, intr) AndAlso intr.HadLocalOutboxSemantics Then
                    If Not String.IsNullOrWhiteSpace(intr.TerminalPriorStatus) Then
                        MessageBox.Show(
                            If(Eng,
                               "This outbound send was skipped because an identical logical send already completed in the database.",
                               "تم تخطي الإرسال لأنه سبق وأن اكتمل نفس المعرف المنطقي في الطابُر المحلي."),
                            If(Eng, "Send", "إرسال"), MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Else
                        MessageBox.Show(
                            If(Eng,
                               "Queued locally. It sends in the background when WhatsApp connects. Use Pending (local) to cancel before send.",
                               "تم الطابَر محليًا. سيُرسَل في الخلفية عند الاتصال. استخدم زر المعقّبة المحلي لإلغاء الإرسال قبل التنفيذ."),
                            If(Eng, "Send", "إرسال"), MessageBoxButtons.OK, MessageBoxIcon.Information)
                    End If
                Else
                    MessageBox.Show(
                        If(Eng, "The message has been queued.", "تم وضع الرسالة في الطابور."),
                        If(Eng, "Send", "إرسال"),
                        MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If
            Else
                Dim sawLocal As Boolean = False
                Dim sawDedup As Boolean = False
                For Each f In files
                    Dim resp = Await _service.SendMessageAsync(clinicIdUse, number, message, f, ctx).ConfigureAwait(True)
                    Dim intr As WhatsAppOutboundSendInterpretation = Nothing
                    If WhatsAppService.TryInterpretOutboundSendResponse(resp, intr) AndAlso intr.HadLocalOutboxSemantics Then
                        sawLocal = True
                        If Not String.IsNullOrWhiteSpace(intr.TerminalPriorStatus) Then sawDedup = True
                    End If
                Next

                If sawDedup Then
                    MessageBox.Show(
                        If(Eng,
                           "One or more attachments were skipped: an identical logical send already completed in the database.",
                           "تم تخطّي مرفق أو أكثر لأن نفس المعرف المنطقي اكتمل مسبقًا في الطابُر المحلي."),
                        If(Eng, "Send", "إرسال"), MessageBoxButtons.OK, MessageBoxIcon.Information)
                ElseIf sawLocal Then
                    Dim count As Integer = files.Length
                    Dim lines As New System.Text.StringBuilder()
                    lines.AppendLine(String.Format(
                        If(Eng, "{0} local queue item(s):", "تم الطابَر {0} عنصرًا محليًا:"),
                        count))
                    For Each f In files
                        Dim nameOnly As String = IO.Path.GetFileName(f)
                        Dim extOnly As String = IO.Path.GetExtension(f)
                        lines.AppendLine(String.Format("- {0} ({1})", nameOnly, If(String.IsNullOrWhiteSpace(extOnly), If(Eng, "No extension", "بدون امتداد"), extOnly)))
                    Next
                    MessageBox.Show(lines.ToString(), If(Eng, "Send", "إرسال"), MessageBoxButtons.OK, MessageBoxIcon.Information)
                Else
                    Dim count As Integer = files.Length
                    Dim lines As New System.Text.StringBuilder()
                    lines.AppendLine(String.Format(
                        If(Eng, "The message has been queued with {0} attachment(s):", "تم وضع الرسالة في الطابور مع {0} مرفق(ات):"),
                        count))
                    For Each f In files
                        Dim name As String = IO.Path.GetFileName(f)
                        Dim ext As String = IO.Path.GetExtension(f)
                        lines.AppendLine(String.Format("- {0} ({1})", name, If(String.IsNullOrWhiteSpace(ext), If(Eng, "No extension", "بدون امتداد"), ext)))
                    Next
                    MessageBox.Show(lines.ToString(), If(Eng, "Send", "إرسال"), MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If
            End If
            TxtSendMessage.Text = ""
            TxtSendFile.Text = ""
        Catch ex As Exception
            MessageBox.Show(ex.Message, If(Eng, "Error", "خطأ"), MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            BtnSendMessage.Enabled = True
        End Try
    End Sub

    Private Sub cboPrefix_EditValueChanged(sender As Object, e As EventArgs) Handles cboPrefix.EditValueChanged
        RefreshLblWhats()
    End Sub

    Private Sub txtWhats_EditValueChanged(sender As Object, e As EventArgs) Handles txtWhats.EditValueChanged
        RefreshLblWhats()
    End Sub





End Class