Imports System.ComponentModel
Imports System.Data.SqlClient
Imports DentistX

Public Class FrmPatientAppts
    Private _service As WhatsAppService
    Private _clinicIdValue As String = ""
    Private _clinicDisplayName As String = ""
    Private _queueBinding As BindingList(Of PendingMessageItem)
    Private _failedBinding As BindingList(Of FailedMessageItem)
    Private _repoQueueDelete As DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit
    Private ReadOnly _patient As Patient
    Private ReadOnly _repo As New AppointmentCRepository()
    Private useEng As Boolean = False


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

        ' Ensure ClinicId is set so connection status works
        ClinicId = WhatsAppService.GetCurrentClinicId()
        FillCboPrefixOnce(cboPrefix)
        ' If we have a patient from Navigator, prebuild his Whats prefix/local/full and header.
        If _patient IsNot Nothing Then
            ApplyPatientWhatsData(_patient)

            Dim baseTextEn As String = "Send Appointment / Appointments WhatsApp Message "
            Dim baseTextAr As String = "إرسال رسالة واتساب "
            Dim namePart As String = If(String.IsNullOrWhiteSpace(_patient.PatientName), "", $" - {_patient.PatientName}")
            Dim headerText As String = If(Eng, baseTextEn, baseTextAr) & namePart
            lblSendTo.Text = headerText
        End If
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

    Private _apptRows As New BindingList(Of AppointmentRow)

    Private Sub FrmPatientAppts_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Default period: today to +30 days
        dtpFrom.EditValue = Date.Today
        dtpTo.EditValue = Date.Today.AddDays(30)

        gcAppts.DataSource = _apptRows

        LoadDoctors()
        LoadStatuses()
        LoadAppointments()

        ' Ensure initial full Whats in lblWhats
        RefreshLblWhats()
    End Sub

    Private Sub RadioLang_SelectedIndexChanged(sender As Object, e As EventArgs) Handles RadioLang.SelectedIndexChanged
        ' 0 = Arabic, 1 = English
        If RadioLang.SelectedIndex = 0 Then
            useEng = False
        ElseIf RadioLang.SelectedIndex = 1 Then
            useEng = True
        End If
        LoadAppointments()
    End Sub

    Private Sub cboPrefix_EditValueChanged(sender As Object, e As EventArgs) Handles cboPrefix.EditValueChanged
        RefreshLblWhats()
    End Sub

    Private Sub txtWhats_EditValueChanged(sender As Object, e As EventArgs) Handles txtWhats.EditValueChanged
        RefreshLblWhats()
    End Sub

    Private Sub LoadDoctors()
        cboDoctor.Properties.Items.Clear()
        cboDoctor.Properties.Items.Add("(All)")

        Dim cs = DentistXDATA.GetConnection.ConnectionString
        Using conn As New SqlConnection(cs)
            conn.Open()
            Using cmd As New SqlCommand("SELECT DrID, DrName FROM Doctors ORDER BY DrName", conn)
                Using rdr = cmd.ExecuteReader()
                    While rdr.Read()
                        Dim id = rdr.GetInt32(0)
                        Dim name = rdr.GetString(1)
                        cboDoctor.Properties.Items.Add(New DoctorItem(id, name))
                    End While
                End Using
            End Using
        End Using

        cboDoctor.SelectedIndex = 0
    End Sub

    Private Sub LoadStatuses()
        cboStatus.Properties.Items.Clear()
        cboStatus.Properties.Items.Add("(All)")
        ' Common statuses used in AppointmentC
        cboStatus.Properties.Items.AddRange(New Object() {"Pending", "Confirmed", "Done", "Cancelled"})
        cboStatus.SelectedIndex = 0
    End Sub

    Private Sub btnApplyFilter_Click(sender As Object, e As EventArgs) Handles btnApplyFilter.Click
        LoadAppointments()
    End Sub

    Private Sub LoadAppointments()
        _apptRows.Clear()
        If _patient Is Nothing OrElse _patient.PatientID <= 0 Then Return

        Dim startDate As DateTime? = CType(dtpFrom.EditValue, DateTime?).GetValueOrDefault().Date
        Dim endDate As DateTime? = CType(dtpTo.EditValue, DateTime?).GetValueOrDefault().Date.AddDays(1).AddTicks(-1)

        Dim doctorId As Integer? = Nothing
        If cboDoctor.SelectedIndex > 0 Then
            Dim item = TryCast(cboDoctor.SelectedItem, DoctorItem)
            If item IsNot Nothing Then doctorId = item.DrID
        End If

        Dim status As String = Nothing
        If cboStatus.SelectedIndex > 0 Then
            status = cboStatus.SelectedItem.ToString()
        End If

        Dim list = _repo.GetFiltered(startDate, endDate, _patient.PatientID, doctorId, Nothing, status)

        ' Fill binding list for DevExpress grid
        For Each ap In list
            Dim row As New AppointmentRow With {
                .Send = False,
                .DateText = WhatsHelper.FormatWhatsAppDateLong(ap.StartDateTime.Date, useEng),
                .TimeText = ap.StartDateTime.ToString("HH:mm") & " - " & ap.EndDateTime.ToString("HH:mm"),
                .DoctorName = _repo.GetDoctorName(ap.DrID),
                .Status = ap.Status,
                .Reason = ap.Reason,
                .Appointment = ap
            }
            _apptRows.Add(row)
        Next
    End Sub

    Private Async Sub BtnSendMessage_Click(sender As Object, e As EventArgs) Handles BtnSendMessage.Click
        Dim number As String = If(lblWhats.Text, "").Trim()
        If String.IsNullOrWhiteSpace(number) Then
            MessageBox.Show("أدخل رقم الجوال.", "إرسال", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            lblWhats.Focus()
            Return
        End If

        ' Collect selected appointments from binding list
        Dim selectedAppts As New List(Of AppointmentC)(
            _apptRows.Where(Function(r) r.Send).Select(Function(r) r.Appointment))

        If selectedAppts.Count = 0 Then
            MessageBox.Show("اختر مواعيد لإرسالها.", "إرسال", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        BtnSendMessage.Enabled = False
        Try
            If Not Await WhatsAppService.EnsureWhatsAppConnectedOrNotifyAsync(Me) Then Return

            ' Build and send one WhatsApp message per selected appointment using shared template.
            For Each ap In selectedAppts
                Dim msg As String = BuildAppointmentWhatsAppMessage(ap)
                Dim ctx As New WhatsAppSendContext With {
                    .Category = WhatsAppMessageCategories.NavigatorAppointment,
                    .PatientId = If(_patient IsNot Nothing, CType(_patient.PatientID, Integer?), Nothing),
                    .DisplayName = If(_patient IsNot Nothing, _patient.PatientName, Nothing),
                    .SourceHint = NameOf(FrmPatientAppts) & " · Appt#" & ap.AppointmentID.ToString(),
                    .RevealMessageCenter = True
                }
                Await _service.SendMessageAsync(ClinicId, lblWhats.Text.Trim(), msg, "", ctx)
            Next

            MessageBox.Show(String.Format("تم وضع {0} رسالة (مواعيد) في الطابور.", selectedAppts.Count),
                            "إرسال", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "خطأ في الإرسال", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            BtnSendMessage.Enabled = True
        End Try
    End Sub

    Private Function BuildAppointmentWhatsAppMessage(ap As AppointmentC) As String
        Dim patientName As String = If(_patient IsNot Nothing, _patient.PatientName, "")
        Dim drName As String = _repo.GetDoctorName(ap.DrID)
        Dim appDt As DateTime = ap.StartDateTime.Date
        Dim startDt As DateTime? = ap.StartDateTime
        Dim endDt As DateTime? = ap.EndDateTime
        Dim reason As String = If(ap.Reason, "")
        Dim notes As String = If(ap.Notes, "")
        Dim status As String = If(ap.Status, "")

        Return WhatsHelper.BuildAppointmentWhatsAppMessage(
            patientName,
            drName,
            appDt,
            startDt,
            endDt,
            reason,
            notes,
            status,
            useEng:=useEng,
            patientSex:=If(_patient IsNot Nothing, _patient.Sex, Nothing),
            includeReason:=ap.WhatsIncludeReason,
            includeNotes:=ap.WhatsIncludeNotes)
    End Function

    Private Class AppointmentRow
        Public Property Send As Boolean
        Public Property DateText As String
        Public Property TimeText As String
        Public Property DoctorName As String
        Public Property Status As String
        Public Property Reason As String
        Public Property Appointment As AppointmentC
    End Class

    Private Class DoctorItem
        Public Property DrID As Integer
        Public Property DrName As String

        Public Sub New(id As Integer, name As String)
            DrID = id
            DrName = name
        End Sub

        Public Overrides Function ToString() As String
            Return DrName
        End Function
    End Class

End Class