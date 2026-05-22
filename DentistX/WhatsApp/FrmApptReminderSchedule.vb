Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Linq

''' <summary>Manage recurring appointment reminders: select patients and set reminder interval (e.g. every 3 days).</summary>
Public Class FrmApptReminderSchedule
    Inherits DevExpress.XtraEditors.XtraForm

    Private _rows As New BindingList(Of ScheduleGridRow)
    Private ReadOnly _patientData As New PatientDATA()
    Private _pendingAdd As PatientItem = Nothing
    Private _pendingDays As Integer = 3
    Private _whatsBinder As PatientWhatsControlsBinder
    Private useEng As Boolean

    Private Sub FrmApptReminderSchedule_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        _whatsBinder = New PatientWhatsControlsBinder(cboPrefix, txtWhats, lblWhats, Me)
        WhatsHelper.FillCboPrefixOnce(cboPrefix)
        PanelWhatsApp.Visible = True
        SetFixRowVisible(False)
        ApplyLanguageRadioFromGlobal()
        LoadSchedule()
        ConfigureGridColumns()
        AddHandler BtnRefresh.Click, AddressOf BtnRefresh_Click
        AddHandler BtnAdd.Click, AddressOf BtnAdd_Click
        AddHandler BtnRemove.Click, AddressOf BtnRemove_Click
        AddHandler BtnSaveWhatsApp.Click, AddressOf BtnSaveWhatsApp_Click
        AddHandler BtnCancelWhatsApp.Click, AddressOf BtnCancelWhatsApp_Click
        AddHandler CboPatient.PatientValueChanged, AddressOf CboPatient_PatientValueChanged
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

    Private Sub CboPatient_PatientValueChanged(sender As Object, e As PatientCombo.PatientIndexChangedEvent)
        If e.PatientID <= 0 Then
            _whatsBinder.Unbind()
            Return
        End If
        BindWhatsForPatientId(e.PatientID)
    End Sub

    Private Sub BindWhatsForPatientId(patientId As Integer)
        If patientId <= 0 Then
            _whatsBinder.Unbind()
            Return
        End If
        Try
            Dim p = _patientData.Select_RecordByID(patientId)
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

    Private Sub SetFixRowVisible(visible As Boolean)
        LblWhatsAppPrompt.Visible = visible
        TxtWhatsApp.Visible = visible
        BtnSaveWhatsApp.Visible = visible
        BtnCancelWhatsApp.Visible = visible
    End Sub

    Private Sub ShowWhatsAppPanel(patientName As String, currentNumber As String)
        LblWhatsAppPrompt.Text = If(Eng,
            $"Invalid or empty WhatsApp number for patient ""{patientName}"". Enter the number (for prefix 970/972: 12 digits; otherwise 10–15 digits, digits only):",
            $"رقم واتساب غير صالح أو فارغ للمريض ""{patientName}"". أدخل الرقم (للرمز 970/972: 12 رقمًا، وإلا 10–15 رقمًا، أرقام فقط):")
        TxtWhatsApp.Text = WhatsHelper.NormalizeWhatsDigits(currentNumber)
        SetFixRowVisible(True)
        TxtWhatsApp.Focus()
    End Sub

    Private Sub HideWhatsAppPanel()
        SetFixRowVisible(False)
        TxtWhatsApp.Text = ""
        _pendingAdd = Nothing
    End Sub

    Private Sub BtnCancelWhatsApp_Click(sender As Object, e As EventArgs)
        HideWhatsAppPanel()
    End Sub

    Private Sub LoadSchedule()
        Dim schedule = ApptReminderScheduleService.GetAllScheduled()
        _rows.Clear()
        For Each entry In schedule
            Dim name = _patientData.Select_RecordByID(entry.PatientID)?.PatientName
            Dim nextSend = If(entry.LastSentAt.HasValue,
                entry.LastSentAt.Value.AddDays(entry.IntervalDays),
                DateTime.Now)
            _rows.Add(New ScheduleGridRow() With {
                .PatientID = entry.PatientID,
                .PatientName = If(name, If(Eng, "Unknown", "غير معروف")),
                .IntervalDays = entry.IntervalDays,
                .LastSentAt = entry.LastSentAt,
                .NextSendAt = nextSend,
                .MessageEnglish = entry.MessageEnglish
            })
        Next
        GridSchedule.DataSource = _rows
    End Sub

    Private Sub ConfigureGridColumns()
        ViewSchedule.Columns.Clear()
        ViewSchedule.Columns.Add(New DevExpress.XtraGrid.Columns.GridColumn() With {.FieldName = "PatientName", .Caption = If(Eng, "Patient", "المريض"), .Visible = True})
        ViewSchedule.Columns.Add(New DevExpress.XtraGrid.Columns.GridColumn() With {.FieldName = "IntervalDays", .Caption = If(Eng, "Every (days)", "كل (يوم)"), .Visible = True})
        Dim colLast As New DevExpress.XtraGrid.Columns.GridColumn() With {.FieldName = "LastSentAt", .Caption = If(Eng, "Last sent", "آخر إرسال"), .Visible = True}
        colLast.DisplayFormat.FormatString = "dd/MM/yyyy HH:mm"
        colLast.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        ViewSchedule.Columns.Add(colLast)
        Dim colNext As New DevExpress.XtraGrid.Columns.GridColumn() With {.FieldName = "NextSendAt", .Caption = If(Eng, "Next send", "الإرسال القادم"), .Visible = True}
        colNext.DisplayFormat.FormatString = "dd/MM/yyyy HH:mm"
        colNext.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        ViewSchedule.Columns.Add(colNext)
        For Each col In ViewSchedule.Columns
            col.OptionsColumn.AllowEdit = (col.FieldName = "IntervalDays")
        Next
    End Sub

    Private Sub BtnRefresh_Click(sender As Object, e As EventArgs)
        LoadSchedule()
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
    Private Function ResolvePatientForAdd() As PatientItem
        Dim pid = CboPatient.PatientID
        Dim pname = If(CboPatient.PatientName, "")
        If pid <= 0 Then
            Dim txt = If(CboPatient.CboPatient.Text, "").Trim()
            If String.IsNullOrEmpty(txt) Then
                MessageBox.Show(If(Eng, "Select a patient first.", "اختر مريضاً أولاً."), If(Eng, "Add", "إضافة"), MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return Nothing
            End If
            Dim allPatients = _patientData.SelectAll()
            Dim match = allPatients.FirstOrDefault(Function(p) String.Equals(p.PatientName, txt, StringComparison.OrdinalIgnoreCase))
            If match Is Nothing Then match = allPatients.FirstOrDefault(Function(p) (If(p.PatientName, "")).Contains(txt))
            If match Is Nothing Then
                MessageBox.Show(If(Eng, "Patient not found.", "لم يتم العثور على المريض."), If(Eng, "Add", "إضافة"), MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return Nothing
            End If
            pid = match.PatientID
            pname = match.PatientName
        End If
        Return New PatientItem() With {.PatientID = pid, .PatientName = pname}
    End Function

    Private Sub BtnAdd_Click(sender As Object, e As EventArgs)
        Dim sel = ResolvePatientForAdd()
        If sel Is Nothing Then Return

        Dim days As Integer = 3
        Integer.TryParse(If(SpnInterval.EditValue?.ToString(), "3"), days)
        If days < 1 Then days = 1

        BindWhatsForPatientId(sel.PatientID)
        _whatsBinder.SaveIfDirty()
        RefreshLblWhats()
        Dim fullNum = If(lblWhats.Text, "").Trim()
        Dim validationMsg As String = WhatsHelper.ValidateWhatsAppNumberLocalized(fullNum)
        If validationMsg <> "" Then
            _pendingAdd = sel
            _pendingDays = days
            ShowWhatsAppPanel(sel.PatientName, fullNum)
            MessageBox.Show(validationMsg, If(Eng, "Fix number", "تصحيح الرقم"), MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        DoAddToSchedule(sel, days)
    End Sub

    Private Sub BtnSaveWhatsApp_Click(sender As Object, e As EventArgs)
        Dim num = WhatsHelper.NormalizeWhatsDigits(If(TxtWhatsApp.Text, ""))
        Dim validationMsg As String = WhatsHelper.ValidateWhatsAppNumberLocalized(num)
        If validationMsg <> "" Then
            MessageBox.Show(validationMsg, If(Eng, "WhatsApp number", "رقم واتساب"), MessageBoxButtons.OK, MessageBoxIcon.Warning)
            TxtWhatsApp.Focus()
            Return
        End If

        If _pendingAdd Is Nothing Then
            HideWhatsAppPanel()
            Return
        End If

        Try
            Dim p = _patientData.Select_RecordByID(_pendingAdd.PatientID)
            If p Is Nothing Then
                MessageBox.Show(If(Eng, "Patient not found.", "لم يُعثر على المريض."), If(Eng, "Error", "خطأ"), MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return
            End If
            _whatsBinder.Unbind()
            Dim splitPatient As New Patient With {.WhatsApp = num, .WhatsAppPrefix = ""}
            WhatsHelper.BindPatientWhatsPrefixAndLocal(cboPrefix, txtWhats, splitPatient)
            Dim pOld = p
            p.WhatsApp = WhatsHelper.NormalizeLocalWhatsTenDigitsForStorage(If(txtWhats.Text, "").ToString())
            p.WhatsAppPrefix = WhatsHelper.GetPrefixTextForStorage(cboPrefix)
            If Not _patientData.Update(pOld, p) Then
                MessageBox.Show(If(Eng, "Failed to save number.", "فشل حفظ الرقم."), If(Eng, "Error", "خطأ"), MessageBoxButtons.OK, MessageBoxIcon.Error)
                BindWhatsForPatientId(_pendingAdd.PatientID)
                Return
            End If
            BindWhatsForPatientId(_pendingAdd.PatientID)
            RefreshLblWhats()
            DoAddToSchedule(_pendingAdd, _pendingDays)
            HideWhatsAppPanel()
        Catch
            MessageBox.Show(If(Eng, "Failed to save number.", "فشل حفظ الرقم."), If(Eng, "Error", "خطأ"), MessageBoxButtons.OK, MessageBoxIcon.Error)
            Try
                If _pendingAdd IsNot Nothing Then BindWhatsForPatientId(_pendingAdd.PatientID)
            Catch
            End Try
        End Try
    End Sub

    Private Sub DoAddToSchedule(sel As PatientItem, days As Integer)
        If _rows.Any(Function(r) r.PatientID = sel.PatientID) Then
            MessageBox.Show(If(Eng, "Patient is already on the list. Change the interval in the grid to adjust how often reminders are sent.", "المريض مضاف مسبقاً. استخدم تعديل المدة لتغيير الفترة."), If(Eng, "Add", "إضافة"), MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
        ApptReminderScheduleService.AddOrUpdate(sel.PatientID, days, useEng)
        LoadSchedule()
        CboPatient.PatientID = 0
        CboPatient.ClearSearchBox()
    End Sub

    Private Sub BtnRemove_Click(sender As Object, e As EventArgs)
        Dim rowHandle = ViewSchedule.FocusedRowHandle
        If rowHandle < 0 Then Return
        Dim row = TryCast(ViewSchedule.GetRow(rowHandle), ScheduleGridRow)
        If row Is Nothing Then Return
        ApptReminderScheduleService.Remove(row.PatientID)
        LoadSchedule()
    End Sub

    Private Sub ViewSchedule_CellValueChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles ViewSchedule.CellValueChanged
        If e.Column?.FieldName <> "IntervalDays" Then Return
        Dim row = TryCast(ViewSchedule.GetRow(e.RowHandle), ScheduleGridRow)
        If row Is Nothing Then Return
        Dim days As Integer = 0
        If Integer.TryParse(If(e.Value?.ToString(), "0"), days) AndAlso days > 0 Then
            ApptReminderScheduleService.AddOrUpdate(row.PatientID, days, row.MessageEnglish)
            row.IntervalDays = days
            row.NextSendAt = If(row.LastSentAt.HasValue, row.LastSentAt.Value.AddDays(days), DateTime.Now)
        End If
    End Sub

    Private Class PatientItem
        Public Property PatientID As Integer
        Public Property PatientName As String
        Public Overrides Function ToString() As String
            Return PatientName
        End Function
    End Class

    Private Class ScheduleGridRow
        Public Property PatientID As Integer
        Public Property PatientName As String
        Public Property IntervalDays As Integer
        Public Property LastSentAt As DateTime?
        Public Property NextSendAt As DateTime
        Public Property MessageEnglish As Boolean?
    End Class
End Class
