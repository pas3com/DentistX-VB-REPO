Imports System.Linq
Imports System.Threading.Tasks
Imports System.Windows.Forms
Imports DevExpress.XtraEditors.Controls

''' <summary>Send WhatsApp to a lab; lab lists only include rows with a usable WhatsApp number (same check as SnapShotSender recipients).</summary>
Partial Public Class FrmLabSendWhats

    Private ReadOnly _service As New WhatsAppService()
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
        InitializeComponent()
        ClinicId = WhatsAppService.GetCurrentClinicId()
        BindLabLookUpWhatsOnly()
    End Sub

    Private Function GetSelectedLabId() As Integer
        Dim ev = lookUpSource.EditValue
        If ev Is Nothing OrElse IsDBNull(ev) Then Return 0
        Try
            Return Convert.ToInt32(ev)
        Catch
            Return 0
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
        Catch
        End Try
    End Sub

    Private NotInheritable Class LabPick
        Public Property Id As Integer
        Public Property Name As String
    End Class

    Private Async Sub btnWhatsSend_Click(sender As Object, e As EventArgs) Handles btnWhatsSend.Click
        Try
            Await SendLabWhatsMessageAsync().ConfigureAwait(True)
        Catch ex As Exception
            MessageBox.Show(ex.Message, If(Eng, "Error", "خطأ"), MessageBoxButtons.OK, MessageBoxIcon.Error)
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

        Dim labRow As Lab = Nothing
        Try
            labRow = New LabDATA().Select_Record(New Lab With {.LabID = labId})
        Catch
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

        Dim message As String = If(TxtSendMessage.Text, "").Trim()
        If String.IsNullOrWhiteSpace(message) Then
            MessageBox.Show(If(Eng, "Enter a message.", "أدخل نص الرسالة."), If(Eng, "Send", "إرسال"), MessageBoxButtons.OK, MessageBoxIcon.Warning)
            TxtSendMessage.Focus()
            Return
        End If

        btnWhatsSend.Enabled = False
        Try
            If Not Await WhatsAppService.EnsureWhatsAppConnectedOrNotifyAsync(Me).ConfigureAwait(True) Then Return

            Dim ctx As New WhatsAppSendContext With {
                .Category = WhatsAppMessageCategories.ManualSend,
                .SourceHint = NameOf(FrmLabSendWhats) & " · Lab#" & labId.ToString(),
                .DisplayName = If(labRow.LabName, lookUpSource.Text),
                .RevealMessageCenter = True
            }
            Await _service.SendMessageAsync(ClinicId, number, message, "", ctx).ConfigureAwait(True)
            MessageBox.Show(If(Eng, "Message queued for sending.", "تم وضع الرسالة في الطابور."), If(Eng, "Send", "إرسال"), MessageBoxButtons.OK, MessageBoxIcon.Information)
            TxtSendMessage.Text = ""
        Catch ex As Exception
            MessageBox.Show(ex.Message, If(Eng, "Send error", "خطأ في الإرسال"), MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            btnWhatsSend.Enabled = True
        End Try
    End Function

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
End Class