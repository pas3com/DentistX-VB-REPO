Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Linq
Imports System.Threading.Tasks
Imports DevExpress.XtraGrid.Views.Base

Public Class FrmAccountWhats
    Private _repo As New AccountingRepository()
    Private _rows As New BindingList(Of AccountingSummaryRow)
    Private _repoSend As DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit
    Private ReadOnly _patientData As New PatientDATA()
    Private _whatsBinder As PatientWhatsControlsBinder
    Private useEng As Boolean

    Private Sub FrmAccountWhats_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        _whatsBinder = New PatientWhatsControlsBinder(cboPrefix, txtWhats, lblWhats, Me)
        WhatsHelper.FillCboPrefixOnce(cboPrefix)
        ApplyLanguageRadioFromGlobal()
        LoadPayTypes()
        SetDefaultDates()
        ConfigureGridColumns()
        AddHandler BtnRefresh.Click, AddressOf BtnRefresh_Click
        AddHandler BtnSendAll.Click, AddressOf BtnSendAll_Click
        AddHandler BtnSchedule.Click, AddressOf BtnSchedule_Click
        LoadAccounts()
    End Sub

    Private Sub ApplyLanguageRadioFromGlobal()
        If RadioLang Is Nothing Then Return
        RadioLang.SelectedIndex = If(Eng, 1, 0)
        useEng = Eng
    End Sub

    Private Sub RadioLang_SelectedIndexChanged(sender As Object, e As EventArgs) Handles RadioLang.SelectedIndexChanged
        If RadioLang Is Nothing Then Return
        If RadioLang.SelectedIndex = 0 Then
            useEng = False
        ElseIf RadioLang.SelectedIndex = 1 Then
            useEng = True
        End If
    End Sub

    Private Sub ViewAccounts_FocusedRowChanged(sender As Object, e As FocusedRowChangedEventArgs) Handles ViewAccounts.FocusedRowChanged
        BindWhatsForFocusedGridRow()
    End Sub

    Private Sub BindWhatsForFocusedGridRow()
        Dim rowHandle = ViewAccounts.FocusedRowHandle
        If rowHandle < 0 Then
            _whatsBinder.Unbind()
            Return
        End If
        Dim row = TryCast(ViewAccounts.GetRow(rowHandle), AccountingSummaryRow)
        If row Is Nothing OrElse row.PatientID <= 0 Then
            _whatsBinder.Unbind()
            Return
        End If
        Try
            Dim p = _patientData.Select_RecordByID(row.PatientID)
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

    Private Sub LoadPayTypes()
        CboPayType.Properties.Items.Clear()
        CboPayType.Properties.Items.Add(If(Eng, "All", "الكل"))
        For Each pt In _repo.GetDistinctPayTypes()
            CboPayType.Properties.Items.Add(pt)
        Next
        CboPayType.SelectedIndex = 0
    End Sub

    Private Sub SetDefaultDates()
        Dim today = DateTime.Today
        DateFrom.EditValue = today.AddMonths(-12)
        DateTo.EditValue = today
    End Sub

    ''' <summary>Get patient search text; treat placeholder/empty as no filter.</summary>
    Private Function GetPatientSearchText() As String
        If TxtPatient Is Nothing Then Return ""
        Dim t = If(TxtPatient.Text, If(TxtPatient.EditValue?.ToString(), "")).Trim()
        If String.IsNullOrWhiteSpace(t) Then Return ""
        If t = If(Eng, "Search by name...", "بحث بالاسم...") Then Return ""
        Return t
    End Function

    Private Sub ConfigureGridColumns()
        ViewAccounts.Columns.Clear()

        If _repoSend Is Nothing Then
            _repoSend = New DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit()
            _repoSend.Buttons(0).Caption = If(Eng, "Send", "إرسال")
            _repoSend.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor
            GridAccounts.RepositoryItems.Add(_repoSend)
            AddHandler _repoSend.ButtonClick, AddressOf ViewAccounts_SendButtonClick
        End If

        ViewAccounts.Columns.Add(New DevExpress.XtraGrid.Columns.GridColumn() With {.FieldName = "PatientName", .Caption = If(Eng, "Patient", "المريض"), .Visible = True})
        Dim colTrt As New DevExpress.XtraGrid.Columns.GridColumn() With {.FieldName = "TotalTreatments", .Caption = If(Eng, "Total treatments", "إجمالي العلاجات"), .Visible = True}
        colTrt.DisplayFormat.FormatString = "N2"
        colTrt.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        ViewAccounts.Columns.Add(colTrt)
        Dim colPay As New DevExpress.XtraGrid.Columns.GridColumn() With {.FieldName = "TotalPayments", .Caption = If(Eng, "Total payments", "إجمالي الدفعات"), .Visible = True}
        colPay.DisplayFormat.FormatString = "N2"
        colPay.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        ViewAccounts.Columns.Add(colPay)
        Dim colBal As New DevExpress.XtraGrid.Columns.GridColumn() With {.FieldName = "Balance", .Caption = If(Eng, "Balance", "الرصيد"), .Visible = True}
        colBal.DisplayFormat.FormatString = "N2"
        colBal.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        ViewAccounts.Columns.Add(colBal)
        ViewAccounts.Columns.Add(New DevExpress.XtraGrid.Columns.GridColumn() With {.FieldName = "PatientPhone", .Caption = If(Eng, "Mobile", "الجوال"), .Visible = True})
        ViewAccounts.Columns.Add(New DevExpress.XtraGrid.Columns.GridColumn() With {.FieldName = "ReminderStatus", .Caption = If(Eng, "Send status", "حالة الإرسال"), .Visible = True})
        Dim colSend As New DevExpress.XtraGrid.Columns.GridColumn() With {
            .Caption = If(Eng, "Send", "إرسال"),
            .ColumnEdit = _repoSend,
            .Visible = True,
            .UnboundType = DevExpress.Data.UnboundColumnType.String
        }
        ViewAccounts.Columns.Add(colSend)

        For Each col In ViewAccounts.Columns
            col.OptionsColumn.AllowEdit = False
        Next
        colSend.OptionsColumn.AllowEdit = True

        ' Summary footer
        colTrt.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
        colTrt.SummaryItem.DisplayFormat = If(Eng, "Total: {0:N2}", "الإجمالي: {0:N2}")
        colPay.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
        colPay.SummaryItem.DisplayFormat = If(Eng, "Total: {0:N2}", "الإجمالي: {0:N2}")
        colBal.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
        colBal.SummaryItem.DisplayFormat = If(Eng, "Total: {0:N2}", "الإجمالي: {0:N2}")
    End Sub

    Private Sub BtnRefresh_Click(sender As Object, e As EventArgs)
        LoadAccounts()
    End Sub

    Private Sub LoadAccounts()
        Dim fromDt = If(DateFrom.EditValue, DateTime.Today.AddMonths(-1))
        Dim toDt = If(DateTo.EditValue, DateTime.Today)
        Dim fromDate = CType(fromDt, DateTime).Date
        Dim toDate = CType(toDt, DateTime).Date
        If toDate < fromDate Then
            MessageBox.Show(If(Eng, "End date must be on or after start date.", "تاريخ النهاية يجب أن يكون بعد تاريخ البداية."), If(Eng, "Filter", "فلترة"), MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        ' Patient search: treat placeholder/whitespace as empty (otherwise no data shows)
        Dim patientName = GetPatientSearchText()
        Dim payType As String = Nothing
        Dim selPay = If(CboPayType.Text, "").Trim()
        If Not String.IsNullOrEmpty(selPay) AndAlso selPay <> If(Eng, "All", "الكل") Then payType = selPay

        Dim minTrt As Decimal? = Nothing
        Dim maxTrt As Decimal? = Nothing
        Dim minPay As Decimal? = Nothing
        Dim maxPay As Decimal? = Nothing
        Dim v As Decimal
        If SpnTrtMin.EditValue IsNot Nothing AndAlso Decimal.TryParse(SpnTrtMin.EditValue.ToString(), v) AndAlso v > 0 Then minTrt = v
        If SpnTrtMax.EditValue IsNot Nothing AndAlso Decimal.TryParse(SpnTrtMax.EditValue.ToString(), v) AndAlso v > 0 Then maxTrt = v
        If SpnPayMin.EditValue IsNot Nothing AndAlso Decimal.TryParse(SpnPayMin.EditValue.ToString(), v) AndAlso v > 0 Then minPay = v
        If SpnPayMax.EditValue IsNot Nothing AndAlso Decimal.TryParse(SpnPayMax.EditValue.ToString(), v) AndAlso v > 0 Then maxPay = v

        Dim balanceOnly = ChkBalanceOnly.Checked

        Dim list = _repo.GetPatientAccountingSummaries(fromDate, toDate, patientName, payType, minTrt, maxTrt, minPay, maxPay, balanceOnly)
        _rows.Clear()
        For Each d In list
            _rows.Add(New AccountingSummaryRow() With {
                .PatientID = d.PatientID,
                .PatientName = d.PatientName,
                .PatientPhone = If(d.PatientPhone, ""),
                .TotalTreatments = d.TotalTreatments,
                .TotalPayments = d.TotalPayments,
                .Balance = d.Balance,
                .ReminderSent = AccountingWhatsAppService.IsAccountSent(d.PatientID)
            })
        Next
        GridAccounts.DataSource = _rows
        UpdateSummaryLabel()
        If _rows.Count > 0 Then
            ViewAccounts.FocusedRowHandle = 0
            BindWhatsForFocusedGridRow()
        Else
            _whatsBinder.Unbind()
        End If
    End Sub

    ''' <summary>
    ''' Validates WhatsApp/phone number using shared Arabic helper.
    ''' </summary>
    Private Function ValidateWhatsAppNumber(phone As String) As String
        Return WhatsHelper.ValidateWhatsAppNumberLocalized(phone)
    End Function

    Private Sub UpdateSummaryLabel()
        Dim cnt = _rows.Count
        Dim sumTrt = _rows.Sum(Function(r) r.TotalTreatments)
        Dim sumPay = _rows.Sum(Function(r) r.TotalPayments)
        Dim sumBal = _rows.Sum(Function(r) r.Balance)
        LblSummary.Text = If(Eng,
            $"Summary: {cnt} patients | Total treatments: {sumTrt:N2} | Total payments: {sumPay:N2} | Total balance: {sumBal:N2}",
            $"ملخص: {cnt} مريض | إجمالي علاجات: {sumTrt:N2} | إجمالي دفعات: {sumPay:N2} | إجمالي رصيد: {sumBal:N2}")
    End Sub

    Private Async Sub ViewAccounts_SendButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs)
        Dim rowHandle = ViewAccounts.FocusedRowHandle
        If rowHandle < 0 Then Return
        Dim row = TryCast(ViewAccounts.GetRow(rowHandle), AccountingSummaryRow)
        If row Is Nothing Then Return

        If row.ReminderSent Then
            MessageBox.Show(If(Eng, "Account summary was already sent to this patient.", "تم إرسال الملخص مسبقاً لهذا المريض."), If(Eng, "Account", "حساب"), MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        BindWhatsForFocusedGridRow()
        _whatsBinder.SaveIfDirty()
        RefreshLblWhats()
        Dim fullNum = If(lblWhats.Text, "").Trim()
        If String.IsNullOrWhiteSpace(fullNum) Then
            MessageBox.Show(If(Eng, "No complete WhatsApp number for this patient.", "لا يوجد رقم واتساب كامل للمريض."), If(Eng, "Account", "حساب"), MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim validationMsg As String = ValidateWhatsAppNumber(fullNum)
        If validationMsg <> "" Then
            MessageBox.Show(validationMsg, If(Eng, "Fix number", "تصحيح الرقم"), MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        BtnRefresh.Enabled = False
        BtnSendAll.Enabled = False
        _whatsBinder.BeginSuppressAutoSaveWhileModal()
        Try
            If Not Await WhatsAppService.EnsureWhatsAppConnectedOrNotifyAsync(Me) Then Return
            Dim ok = Await AccountingWhatsAppService.SendAccountForPatientAsync(row.PatientID, row.PatientName, fullNum, useEng)
            If ok Then
                row.ReminderSent = True
                ViewAccounts.RefreshRow(rowHandle)
                MessageBox.Show(If(Eng, "Account summary queued for sending.", "تم وضع الملخص في الطابور."), If(Eng, "Account", "حساب"), MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
                MessageBox.Show(If(Eng, "Send failed. Check WhatsApp connection.", "فشل الإرسال. تحقق من اتصال واتساب."), If(Eng, "Account", "حساب"), MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, If(Eng, "Error", "خطأ"), MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            _whatsBinder.EndSuppressAutoSaveAndSync()
            BtnRefresh.Enabled = True
            BtnSendAll.Enabled = True
        End Try
    End Sub

    Private Sub BtnSchedule_Click(sender As Object, e As EventArgs)
        Using f As New FrmAccountReminderSchedule()
            f.ShowDialog(Me)
        End Using
    End Sub

    Private Async Sub BtnSendAll_Click(sender As Object, e As EventArgs)
        Dim useEngAll = useEng
        Dim toSend = _rows.Where(Function(r) Not r.ReminderSent).ToList()
        If toSend.Count = 0 Then
            MessageBox.Show(If(Eng, "No patients ready to send (already sent or missing mobile number).", "لا يوجد مرضى جاهزين للإرسال (إما تم إرسالهم أو لا يوجد رقم جوال)."), If(Eng, "Send All", "إرسال الكل"), MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        Dim result = MessageBox.Show(If(Eng, $"Send account summary to {toSend.Count} patients. Continue?", $"سيتم إرسال ملخص الحساب لـ {toSend.Count} مريض. متابعة؟"), If(Eng, "Send All", "إرسال الكل"), MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If result <> DialogResult.Yes Then Return

        BtnRefresh.Enabled = False
        BtnSendAll.Enabled = False
        Dim sent As Integer = 0
        Dim failed As Integer = 0
        Dim invalidList As New List(Of String)
        Try
            If Not Await WhatsAppService.EnsureWhatsAppConnectedOrNotifyAsync(Me) Then Return
            For Each row In toSend
                Dim p = _patientData.Select_RecordByID(row.PatientID)
                Dim fullNum = ""
                If p IsNot Nothing Then
                    fullNum = WhatsHelper.BuildInternationalWhatsDigitsFromPatient(p.WhatsApp, p.Phone, p.WhatsAppPrefix)
                End If
                If String.IsNullOrWhiteSpace(fullNum) Then fullNum = If(row.PatientPhone, "").Trim()

                Dim validationMsg As String = ValidateWhatsAppNumber(fullNum)
                If validationMsg <> "" Then
                    invalidList.Add($"{row.PatientName} ({fullNum})")
                    ViewAccounts.RefreshData()
                    Continue For
                End If
                Dim ok = Await AccountingWhatsAppService.SendAccountForPatientAsync(row.PatientID, row.PatientName, fullNum, useEngAll)
                If ok Then
                    row.ReminderSent = True
                    sent += 1
                Else
                    failed += 1
                End If
                ViewAccounts.RefreshData()
            Next
            Dim summary = If(Eng, $"Sent: {sent} | Failed: {failed}", $"تم الإرسال: {sent} | فشل: {failed}")
            If invalidList.Count > 0 Then
                summary &= If(Eng, " | Not sent (invalid number): ", " | لم يُرسَل (رقم غير صحيح): ") & invalidList.Count
                MessageBox.Show(summary & vbCrLf & vbCrLf & If(Eng, "Invalid numbers:", "أرقام غير صحيحة:") & vbCrLf & String.Join(vbCrLf, invalidList), If(Eng, "Send All", "إرسال الكل"), MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
                MessageBox.Show(summary, If(Eng, "Send All", "إرسال الكل"), MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, If(Eng, "Error", "خطأ"), MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            BtnRefresh.Enabled = True
            BtnSendAll.Enabled = True
        End Try
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
