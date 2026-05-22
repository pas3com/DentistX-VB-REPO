Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Linq
Imports System.Threading.Tasks
Imports DevExpress.XtraGrid.Views.Base

Public Class FrmApptsWhats
    Private _repo As New AppointmentCRepository()
    Private _doctorsData As New DoctorsDATA()
    Private _rows As New BindingList(Of AppointmentReminderRow)
    Private _repoSend As DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit
    Private ReadOnly _patientData As New PatientDATA()
    Private _whatsBinder As PatientWhatsControlsBinder
    Private useEng As Boolean

    Private Sub FrmApptsWhats_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        _whatsBinder = New PatientWhatsControlsBinder(cboPrefix, txtWhats, lblWhats, Me)
        WhatsHelper.FillCboPrefixOnce(cboPrefix)
        ApplyLanguageRadioFromGlobal()
        LoadDoctors()
        SetDefaultDates()
        ConfigureGridColumns()
        AddHandler BtnRefresh.Click, AddressOf BtnRefresh_Click
        AddHandler BtnSendAll.Click, AddressOf BtnSendAll_Click
        LoadAppointments()
    End Sub

    Private Sub ApplyLanguageRadioFromGlobal()
        If RadioLang Is Nothing Then Return
        RadioLang.SelectedIndex = 0
        useEng = False
    End Sub

    Private Sub RadioLang_SelectedIndexChanged(sender As Object, e As EventArgs) Handles RadioLang.SelectedIndexChanged
        If RadioLang Is Nothing Then Return
        If RadioLang.SelectedIndex = 0 Then
            useEng = False
        ElseIf RadioLang.SelectedIndex = 1 Then
            useEng = True
        End If
    End Sub

    Private Sub ViewAppts_FocusedRowChanged(sender As Object, e As FocusedRowChangedEventArgs) Handles ViewAppts.FocusedRowChanged
        BindWhatsForFocusedGridRow()
    End Sub

    Private Sub BindWhatsForFocusedGridRow()
        Dim rowHandle = ViewAppts.FocusedRowHandle
        If rowHandle < 0 Then
            _whatsBinder.Unbind()
            Return
        End If
        Dim row = TryCast(ViewAppts.GetRow(rowHandle), AppointmentReminderRow)
        If row Is Nothing OrElse row.Dto Is Nothing OrElse row.Dto.PatientID <= 0 Then
            _whatsBinder.Unbind()
            Return
        End If
        Try
            Dim p = _patientData.Select_RecordByID(row.Dto.PatientID)
            If p Is Nothing Then
                _whatsBinder.Unbind()
                Return
            End If
            _whatsBinder.BindToPatient(p, False)
        Catch
            _whatsBinder.Unbind()
        End Try
    End Sub

    Private Sub RefreshLblWhats()
        _whatsBinder.RefreshLabel()
    End Sub

    Private Sub cboPrefix_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboPrefix.SelectedIndexChanged
        _whatsBinder.OnCboPrefixSelectedIndexChanged()
    End Sub

    Private Sub cboPrefix_EditValueChanged(sender As Object, e As EventArgs) Handles cboPrefix.EditValueChanged
        _whatsBinder.OnCboPrefixEditValueChanged()
    End Sub

    Private Sub txtWhats_EditValueChanged(sender As Object, e As EventArgs) Handles txtWhats.EditValueChanged
        _whatsBinder.OnTxtWhatsEditValueChanged()
    End Sub

    Private Sub txtWhats_Leave(sender As Object, e As EventArgs) Handles txtWhats.Leave
        _whatsBinder.OnTxtWhatsLeave()
    End Sub

    Private Sub txtWhats_Validated(sender As Object, e As EventArgs) Handles txtWhats.Validated
        _whatsBinder.OnTxtWhatsValidated()
    End Sub

    Private Sub LoadDoctors()
        CboDoctor.Properties.Items.Clear()
        CboDoctor.Properties.Items.Add("الكل")
        For Each dr In _doctorsData.SelectAll()
            CboDoctor.Properties.Items.Add(dr.DrName)
        Next
        CboDoctor.SelectedIndex = 0
    End Sub

    Private Sub SetDefaultDates()
        Dim today = DateTime.Today
        DateFrom.EditValue = today
        DateTo.EditValue = today.AddDays(30)
    End Sub

    Private Sub ConfigureGridColumns()
        ViewAppts.Columns.Clear()

        If _repoSend Is Nothing Then
            _repoSend = New DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit()
            _repoSend.Buttons(0).Caption = "إرسال"
            _repoSend.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor
            GridAppts.RepositoryItems.Add(_repoSend)
            AddHandler _repoSend.ButtonClick, AddressOf ViewAppts_SendButtonClick
        End If

        ViewAppts.Columns.Add(New DevExpress.XtraGrid.Columns.GridColumn() With {
            .FieldName = "PatientName",
            .Caption = "المريض",
            .Visible = True
        })
        ViewAppts.Columns.Add(New DevExpress.XtraGrid.Columns.GridColumn() With {
            .FieldName = "DrName",
            .Caption = "الطبيب",
            .Visible = True
        })
        Dim colStart As New DevExpress.XtraGrid.Columns.GridColumn() With {.FieldName = "StartDateTime", .Caption = "من", .Visible = True}
        colStart.DisplayFormat.FormatString = "dd/MM/yyyy HH:mm"
        colStart.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        ViewAppts.Columns.Add(colStart)
        Dim colEnd As New DevExpress.XtraGrid.Columns.GridColumn() With {.FieldName = "EndDateTime", .Caption = "إلى", .Visible = True}
        colEnd.DisplayFormat.FormatString = "HH:mm"
        colEnd.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        ViewAppts.Columns.Add(colEnd)
        ViewAppts.Columns.Add(New DevExpress.XtraGrid.Columns.GridColumn() With {
            .FieldName = "Reason",
            .Caption = "السبب",
            .Visible = True
        })
        ViewAppts.Columns.Add(New DevExpress.XtraGrid.Columns.GridColumn() With {
            .FieldName = "PatientPhone",
            .Caption = "الجوال",
            .Visible = True
        })
        ViewAppts.Columns.Add(New DevExpress.XtraGrid.Columns.GridColumn() With {
            .FieldName = "ReminderStatus",
            .Caption = "حالة التذكير",
            .Visible = True
        })
        Dim colSend As New DevExpress.XtraGrid.Columns.GridColumn() With {
            .Caption = "إرسال",
            .ColumnEdit = _repoSend,
            .Visible = True,
            .UnboundType = DevExpress.Data.UnboundColumnType.String
        }
        ViewAppts.Columns.Add(colSend)

        For Each col In ViewAppts.Columns
            If col IsNot colSend Then
                col.OptionsColumn.AllowEdit = False
            End If
        Next
        colSend.OptionsColumn.AllowEdit = True
    End Sub

    Private Sub BtnOpenAutoReminderQueue_Click(sender As Object, e As EventArgs) Handles BtnOpenAutoReminderQueue.Click
        FrmApptsReminder.ShowQueue(Me)
    End Sub

    Private Sub BtnOpenWhatsAppLog_Click(sender As Object, e As EventArgs) Handles BtnOpenWhatsAppLog.Click
        FrmWhatsAppActivityLog.ShowArchive(Me)
    End Sub

    Private Sub BtnRefresh_Click(sender As Object, e As EventArgs)
        LoadAppointments()
    End Sub

    Private Sub LoadAppointments()
        Dim fromDt = If(DateFrom.EditValue, DateTime.Today)
        Dim toDt = If(DateTo.EditValue, DateTime.Today.AddDays(30))
        Dim fromDate = CType(fromDt, DateTime).Date
        Dim toDate = CType(toDt, DateTime).Date
        If toDate < fromDate Then
            MessageBox.Show("تاريخ النهاية يجب أن يكون بعد تاريخ البداية.", "فلترة", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim doctorId As Integer? = Nothing
        Dim selDr = If(CboDoctor.Text, "").Trim()
        If Not String.IsNullOrEmpty(selDr) AndAlso selDr <> "الكل" Then
            doctorId = _doctorsData.GetDoctorIdByName(selDr)
            If doctorId = 0 Then doctorId = Nothing
        End If

        Dim appts = _repo.GetFutureAppointments(fromDate, toDate, doctorId)
        _rows.Clear()
        For Each a In appts
            _rows.Add(New AppointmentReminderRow() With {
                .AppointmentID = a.AppointmentID,
                .PatientName = a.PatientName,
                .DrName = a.DrName,
                .StartDateTime = a.StartDateTime,
                .EndDateTime = a.EndDateTime,
                .Reason = If(a.Reason, ""),
                .PatientPhone = If(a.PatientPhone, ""),
                .ReminderSent = AppointmentReminderService.IsReminderSent(a.AppointmentID),
                .Dto = a
            })
        Next
        GridAppts.DataSource = _rows
        If _rows.Count > 0 Then
            ViewAppts.FocusedRowHandle = 0
            BindWhatsForFocusedGridRow()
        Else
            _whatsBinder.Unbind()
        End If
    End Sub

    Private Async Sub ViewAppts_SendButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs)
        Dim rowHandle = ViewAppts.FocusedRowHandle
        If rowHandle < 0 Then Return
        Dim row = TryCast(ViewAppts.GetRow(rowHandle), AppointmentReminderRow)
        If row Is Nothing OrElse row.Dto Is Nothing Then Return

        If row.ReminderSent Then
            MessageBox.Show("تم إرسال التذكير مسبقاً لهذا الموعد.", "تذكير", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        BindWhatsForFocusedGridRow()
        _whatsBinder.SaveIfDirty()
        RefreshLblWhats()
        Dim fullNum = If(lblWhats.Text, "").Trim()
        If String.IsNullOrWhiteSpace(fullNum) Then
            MessageBox.Show("لا يوجد رقم واتساب كامل للمريض.", "تذكير", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim validationMsg As String = WhatsHelper.ValidateWhatsAppNumberLocalized(fullNum)
        If validationMsg <> "" Then
            MessageBox.Show(validationMsg, "تصحيح الرقم", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        BtnRefresh.Enabled = False
        _whatsBinder.BeginSuppressAutoSaveWhileModal()
        Try
            If Not Await WhatsAppService.EnsureWhatsAppConnectedOrNotifyAsync(Me) Then Return
            Dim ok = Await AppointmentReminderService.SendReminderForAppointmentAsync(row.Dto, useEng, fullNum)
            If ok Then
                row.ReminderSent = True
                ViewAppts.RefreshRow(rowHandle)
                MessageBox.Show("تم وضع التذكير في الطابور.", "تذكير", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
                MessageBox.Show("فشل الإرسال. تحقق من اتصال واتساب.", "تذكير", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            _whatsBinder.EndSuppressAutoSaveAndSync()
            BtnRefresh.Enabled = True
        End Try
    End Sub

    Private Async Sub BtnSendAll_Click(sender As Object, e As EventArgs)
        Dim useEngAll = useEng
        Dim toSend = _rows.Where(Function(r) Not r.ReminderSent).ToList()
        If toSend.Count = 0 Then
            MessageBox.Show("لا توجد مواعيد جاهزة للإرسال.", "إرسال الكل", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        Dim result = MessageBox.Show($"سيتم إرسال التذكير لـ {toSend.Count} موعد. متابعة؟", "إرسال الكل", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If result <> DialogResult.Yes Then Return

        BtnRefresh.Enabled = False
        BtnSendAll.Enabled = False
        Dim sent As Integer = 0
        Dim failed As Integer = 0
        Dim invalidList As New List(Of String)
        Try
            If Not Await WhatsAppService.EnsureWhatsAppConnectedOrNotifyAsync(Me) Then Return
            For Each row In toSend
                If row.Dto Is Nothing Then Continue For
                Dim fullNum = WhatsHelper.BuildInternationalWhatsDigitsFromPatient(
                    If(row.Dto.PatientWhatsLocal, ""),
                    If(row.Dto.PatientPhoneFallback, ""),
                    If(row.Dto.PatientWhatsAppPrefix, ""))
                If String.IsNullOrWhiteSpace(fullNum) Then fullNum = If(row.Dto.PatientPhone, "").Trim()

                Dim validationMsg As String = WhatsHelper.ValidateWhatsAppNumberLocalized(fullNum)
                If validationMsg <> "" Then
                    invalidList.Add($"{row.PatientName} ({fullNum})")
                    ViewAppts.RefreshData()
                    Continue For
                End If
                Dim ok = Await AppointmentReminderService.SendReminderForAppointmentAsync(row.Dto, useEngAll, fullNum)
                If ok Then
                    row.ReminderSent = True
                    sent += 1
                Else
                    failed += 1
                End If
                ViewAppts.RefreshData()
            Next
            Dim summary = $"تم الإرسال: {sent} | فشل: {failed}"
            If invalidList.Count > 0 Then
                summary &= " | تخطي (رقم غير صحيح): " & invalidList.Count
                MessageBox.Show(summary & vbCrLf & vbCrLf & String.Join(vbCrLf, invalidList), "إرسال الكل", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
                MessageBox.Show(summary, "إرسال الكل", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            BtnRefresh.Enabled = True
            BtnSendAll.Enabled = True
        End Try
    End Sub

    Private Sub BtnSchedule_Click(sender As Object, e As EventArgs) Handles BtnSchedule.Click

        Using f As New FrmApptReminderSchedule()
            f.ShowDialog(Me)
        End Using
    End Sub
    Private Sub txtWhats_KeyDown(sender As Object, e As KeyEventArgs) Handles txtWhats.KeyDown
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
End Class
