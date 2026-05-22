Imports System.Data.SqlClient
Imports System.Globalization
Imports System.Linq
Imports System.Windows.Forms
Imports Dapper
Imports DevExpress.XtraEditors

''' <summary>Bulk-applies shareable fields (date, type, finished, external, value/pay, plan, details, notes) to many <see cref="Patient_ToothTrt"/> rows in one SQL transaction. Only fields whose "apply" toggle is on get persisted; other columns stay as-is per row. Arabic uses resx-driven captions + form-level <c>RightToLeft=Yes</c> only; <c>RightToLeftLayout</c> stays off so the Dock.Left/Fill split doesn't mirror and collapse.</summary>
Public Class EditMultiTreatFrom

    Private Const TrtTypeOneStageEn As String = "One Stage"
    Private Const TrtTypeMultipleStageEn As String = "Multiple Stages"
    Private Const TrtTypeOneStageAr As String = "مرحلة واحدة"
    Private Const TrtTypeMultipleStageAr As String = "عدة مراحل"

    Private ReadOnly _treats As New List(Of Patient_ToothTrt)
    Private _patient As Patient
    Private _toothNumList As List(Of Byte)

    ''' <summary>True when one or more rows were successfully updated.</summary>
    Public Property Saved As Boolean
    Public Property Canceled As Boolean

    Public Sub New()
        InitializeComponent()
        Me.Icon = AppIcon
    End Sub

    ''' <summary>Use <paramref name="toothNumList"/> the same way <c>DelTreatFrom</c> does: single tooth, multi teeth, or <c>{0}</c> for whole-mouth records.</summary>
    Public Sub New(patient As Patient, toothNumList As List(Of Byte))
        InitializeComponent()
        Me.Icon = AppIcon
        _patient = patient
        _toothNumList = If(toothNumList, New List(Of Byte))
    End Sub

    Private Sub EditMultiTreatFrom_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ApplyLanguage()
        ConfigureMoneyFields()
        ConfigureTreatmentTypeCombo()
        LoadTreats()
        WireFieldEnableHandlers()
        UpdateFieldEnableStates()
        dtTreatDate.DateTime = Date.Today
        dtTreatEnd.DateTime = Date.Today
    End Sub

    ''' <summary>Captions/text come from <c>.resx</c> / <c>.ar.resx</c> via the designer's <c>ApplyResources</c> call; this method is a belt-and-suspenders override that re-asserts the same strings in code so a missing resx key never ships an English caption inside the Arabic dialog. Mirroring stays off (no <c>RightToLeftLayout = True</c>) because the Dock.Left/Fill split collapses when mirrored.</summary>
    Private Sub ApplyLanguage()
        Me.Text = If(Eng, "Edit Multiple Treatments", "تعديل عدة علاجات")
        Me.RightToLeftLayout = False

        lblPatientCaption.Text = If(Eng, "Patient:", "المريض:")
        grpTreats.Text = If(Eng, "Treatments to update", "العلاجات المراد تعديلها")
        grpChanges.Text = If(Eng, "Bulk changes", "التعديلات الجماعية")
        btnSelectAll.Text = If(Eng, "Select All", "تحديد الكل")
        btnSelectNone.Text = If(Eng, "Select None", "إلغاء التحديد")
        chkApplyTreatDate.Properties.Caption = If(Eng, "Treat date", "تاريخ العلاج")
        chkApplyTreatmentType.Properties.Caption = If(Eng, "Treatment type", "نوع العلاج")
        chkApplyFinished.Properties.Caption = If(Eng, "Finished + end date", "منتهي + تاريخ الانتهاء")
        chkApplyIsExternal.Properties.Caption = If(Eng, "External clinic", "علاج خارجي")
        chkApplyTrtValue.Properties.Caption = If(Eng, "Treat value", "قيمة العلاج")
        chkApplyPayValue.Properties.Caption = If(Eng, "First payment", "الدفعة الأولى")
        chkApplyTrtPlan.Properties.Caption = If(Eng, "Treat plan", "خطة العلاج")
        chkApplyTrtDetails.Properties.Caption = If(Eng, "Treat details", "تفاصيل العلاج")
        chkApplyTrtNotes.Properties.Caption = If(Eng, "Treat notes", "ملاحظات العلاج")
        ceFinished.Properties.Caption = If(Eng, "Finished", "منتهي")
        ceIsExternal.Properties.Caption = If(Eng, "External", "خارجي")
        btnSave.Text = If(Eng, "Save", "حفظ")
        btnCancel.Text = If(Eng, "Cancel", "إلغاء")

        ApplyArabicTextEntryToMemos()
        pnlChangesScroll.AutoScrollMinSize = New Size(490, 420)
    End Sub

    ''' <summary>RTL for memo typing so Arabic input flows right-to-left even though we keep the form's docked layout LTR.</summary>
    Private Sub ApplyArabicTextEntryToMemos()
        Dim memoRtl = If(Eng, RightToLeft.No, RightToLeft.Yes)
        txtTrtPlan.RightToLeft = memoRtl
        txtTrtDetails.RightToLeft = memoRtl
        txtTrtNotes.RightToLeft = memoRtl
        txtExtClinic.RightToLeft = memoRtl
    End Sub

    Private Sub ConfigureMoneyFields()
        IntegerMoneyEditorFocus.ConfigureIntegerMoneyTextEdit(txtTrtValue)
        IntegerMoneyEditorFocus.AttachTextEditZeroEmptyElseSelectAll(txtTrtValue)
        IntegerMoneyEditorFocus.ConfigureIntegerMoneyTextEdit(txtPayValue)
        IntegerMoneyEditorFocus.AttachTextEditZeroEmptyElseSelectAll(txtPayValue)
    End Sub

    Private Sub ConfigureTreatmentTypeCombo()
        cboTreatmentType.Properties.Items.Clear()
        If Eng Then
            cboTreatmentType.Properties.Items.AddRange({TrtTypeOneStageEn, TrtTypeMultipleStageEn})
        Else
            cboTreatmentType.Properties.Items.AddRange({TrtTypeOneStageAr, TrtTypeMultipleStageAr})
        End If
    End Sub

    Private Sub LoadTreats()
        clstTreats.Items.Clear()
        _treats.Clear()
        If _patient Is Nothing OrElse _toothNumList Is Nothing OrElse _toothNumList.Count = 0 Then Return

        txtPatientName.Text = _patient.PatientName

        Dim dao As New Patient_ToothTrtDATA
        Dim rows = dao.GetPatientTreats(_patient.PatientID, _toothNumList)
        If rows Is Nothing Then Return

        Dim ordered = rows.OrderBy(Function(r) r.ToothNum).ThenByDescending(Function(r) If(r.TreatDate.HasValue, r.TreatDate.Value, Date.MinValue))
        For Each r In ordered
            _treats.Add(r)
            clstTreats.Items.Add(BuildTreatLineForList(r), True)
        Next
    End Sub

    ''' <summary>"FDI - treat (dd/MM/yyyy) - FRIENDLY TOOTH NAME". Whole-mouth rows get the localized whole-mouth label on both ends and skip the friendly suffix. <see cref="GetShortToothNameWithDash"/> matches what <c>Patient_Trts.Detail</c> stores so the picker reads the same as the rest of the app.</summary>
    Private Function BuildTreatLineForList(r As Patient_ToothTrt) As String
        Dim isWholeMouth As Boolean = (r.ToothNum = 0)
        Dim toothPrefix As String = If(isWholeMouth,
                                       TrtSourceHelper.FormatWholeMouthLabel(Eng),
                                       r.ToothNum.ToString())
        Dim dateText As String = ""
        If r.TreatDate.HasValue Then dateText = r.TreatDate.Value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture)

        Dim core As String = $"{toothPrefix}  -  {r.Treat}  ({dateText})"
        If isWholeMouth Then Return core

        Dim friendly As String = GetShortToothNameWithDash(r.ToothNum)
        If String.IsNullOrWhiteSpace(friendly) OrElse friendly = "InvalidTooth" Then Return core
        Return $"{core}  -  {friendly}"
    End Function

    Private Sub WireFieldEnableHandlers()
        AddHandler chkApplyTreatDate.CheckedChanged, AddressOf OnApplyFieldChanged
        AddHandler chkApplyTreatmentType.CheckedChanged, AddressOf OnApplyFieldChanged
        AddHandler chkApplyFinished.CheckedChanged, AddressOf OnApplyFieldChanged
        AddHandler chkApplyIsExternal.CheckedChanged, AddressOf OnApplyFieldChanged
        AddHandler chkApplyTrtValue.CheckedChanged, AddressOf OnApplyFieldChanged
        AddHandler chkApplyPayValue.CheckedChanged, AddressOf OnApplyFieldChanged
        AddHandler chkApplyTrtPlan.CheckedChanged, AddressOf OnApplyFieldChanged
        AddHandler chkApplyTrtDetails.CheckedChanged, AddressOf OnApplyFieldChanged
        AddHandler chkApplyTrtNotes.CheckedChanged, AddressOf OnApplyFieldChanged
        AddHandler ceFinished.CheckedChanged, AddressOf OnApplyFieldChanged
        AddHandler ceIsExternal.CheckedChanged, AddressOf OnApplyFieldChanged
    End Sub

    Private Sub OnApplyFieldChanged(sender As Object, e As EventArgs)
        UpdateFieldEnableStates()
    End Sub

    Private Sub UpdateFieldEnableStates()
        dtTreatDate.Enabled = chkApplyTreatDate.Checked
        cboTreatmentType.Enabled = chkApplyTreatmentType.Checked
        ceFinished.Enabled = chkApplyFinished.Checked
        dtTreatEnd.Enabled = chkApplyFinished.Checked AndAlso ceFinished.Checked
        ceIsExternal.Enabled = chkApplyIsExternal.Checked
        txtExtClinic.Enabled = chkApplyIsExternal.Checked AndAlso ceIsExternal.Checked
        txtTrtValue.Enabled = chkApplyTrtValue.Checked
        txtPayValue.Enabled = chkApplyPayValue.Checked
        txtTrtPlan.Enabled = chkApplyTrtPlan.Checked
        txtTrtDetails.Enabled = chkApplyTrtDetails.Checked
        txtTrtNotes.Enabled = chkApplyTrtNotes.Checked
    End Sub

    Private Sub btnSelectAll_Click(sender As Object, e As EventArgs) Handles btnSelectAll.Click
        For i = 0 To clstTreats.Items.Count - 1
            clstTreats.SetItemChecked(i, True)
        Next
    End Sub

    Private Sub btnSelectNone_Click(sender As Object, e As EventArgs) Handles btnSelectNone.Click
        For i = 0 To clstTreats.Items.Count - 1
            clstTreats.SetItemChecked(i, False)
        Next
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Saved = False
        Canceled = True
        Me.DialogResult = DialogResult.Cancel
    End Sub

    Private Function GetCheckedTreats() As List(Of Patient_ToothTrt)
        Dim result As New List(Of Patient_ToothTrt)
        For i = 0 To clstTreats.Items.Count - 1
            If clstTreats.GetItemChecked(i) AndAlso i < _treats.Count Then
                result.Add(_treats(i))
            End If
        Next
        Return result
    End Function

    Private Function AnyApplyChecked() As Boolean
        Return chkApplyTreatDate.Checked OrElse chkApplyTreatmentType.Checked OrElse
               chkApplyFinished.Checked OrElse chkApplyIsExternal.Checked OrElse
               chkApplyTrtValue.Checked OrElse chkApplyPayValue.Checked OrElse
               chkApplyTrtPlan.Checked OrElse chkApplyTrtDetails.Checked OrElse chkApplyTrtNotes.Checked
    End Function

    Private Function CurrentTreatmentTypeEnglish() As String
        Dim raw As String = ""
        If cboTreatmentType.EditValue IsNot Nothing Then raw = cboTreatmentType.EditValue.ToString()
        If String.IsNullOrWhiteSpace(raw) Then raw = cboTreatmentType.Text
        Return NormalizeTreatmentTypeToEnglish(raw)
    End Function

    Private Function NormalizeTreatmentTypeToEnglish(raw As String) As String
        If String.IsNullOrWhiteSpace(raw) Then Return ""
        Dim t = raw.Trim()
        If String.Equals(t, TrtTypeOneStageEn, StringComparison.OrdinalIgnoreCase) Then Return TrtTypeOneStageEn
        If String.Equals(t, TrtTypeMultipleStageEn, StringComparison.OrdinalIgnoreCase) Then Return TrtTypeMultipleStageEn
        If String.Equals(t, TrtTypeOneStageAr, StringComparison.Ordinal) Then Return TrtTypeOneStageEn
        If String.Equals(t, TrtTypeMultipleStageAr, StringComparison.Ordinal) Then Return TrtTypeMultipleStageEn
        Dim tl = t.ToLowerInvariant()
        If tl.Contains("multiple stages") OrElse (tl.Contains("multiple") AndAlso tl.Contains("stage")) Then Return TrtTypeMultipleStageEn
        If tl.Contains("one stage") OrElse (tl.Contains("one") AndAlso tl.Contains("stage")) Then Return TrtTypeOneStageEn
        If tl.Contains("مرحلة") AndAlso tl.Contains("واحدة") Then Return TrtTypeOneStageEn
        If tl.Contains("عدة") OrElse tl.Contains("مراحل") Then Return TrtTypeMultipleStageEn
        Return t
    End Function

    Private Function ParseBoundDecimal(raw As String) As Decimal
        If String.IsNullOrWhiteSpace(raw) Then Return 0D
        Dim t As Decimal
        If Decimal.TryParse(raw, NumberStyles.Currency Or NumberStyles.Number, CultureInfo.CurrentCulture, t) Then Return t
        If Decimal.TryParse(raw, NumberStyles.Currency Or NumberStyles.Number, CultureInfo.InvariantCulture, t) Then Return t
        Return 0D
    End Function

    Private Sub ShowWarn(msg As String)
        MessageBox.Show(msg, If(Eng, "Required", "مطلوب"), MessageBoxButtons.OK, MessageBoxIcon.Warning)
    End Sub

    Private Function ValidateInputs() As Boolean
        If chkApplyTreatDate.Checked AndAlso dtTreatDate.EditValue Is Nothing Then
            ShowWarn(If(Eng, "Pick a treatment date.", "اختر تاريخ العلاج."))
            Return False
        End If
        If chkApplyTreatmentType.Checked AndAlso String.IsNullOrWhiteSpace(CurrentTreatmentTypeEnglish()) Then
            ShowWarn(If(Eng, "Pick a treatment type.", "اختر نوع العلاج."))
            Return False
        End If
        If chkApplyFinished.Checked AndAlso ceFinished.Checked AndAlso dtTreatEnd.EditValue Is Nothing Then
            ShowWarn(If(Eng, "Pick an end date or uncheck Finished.", "اختر تاريخ الانتهاء أو ألغ تفعيل (منتهي)."))
            Return False
        End If
        Return True
    End Function

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If _patient Is Nothing Then Return

        Dim targets = GetCheckedTreats()
        If targets.Count = 0 Then
            ShowWarn(If(Eng, "Tick at least one treatment.", "اختر علاجاً واحداً على الأقل."))
            Return
        End If
        If Not AnyApplyChecked() Then
            ShowWarn(If(Eng, "Tick at least one field to apply.", "اختر حقلاً واحداً على الأقل للتطبيق."))
            Return
        End If
        If Not ValidateInputs() Then Return

        Dim confirmMsg = If(Eng,
                            $"Apply selected changes to {targets.Count} treatment(s)?",
                            $"تطبيق التعديلات على {targets.Count} علاج؟")
        Dim confirmTitle = If(Eng, "Confirm bulk edit", "تأكيد التعديل الجماعي")
        If MessageBox.Show(confirmMsg, confirmTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question) <> DialogResult.Yes Then
            Return
        End If

        Try
            Dim updated = ApplyBulkChanges(targets)
            If updated > 0 Then
                Saved = True
                Dim okMsg = If(Eng, $"{updated} treatment(s) updated.", $"تم تحديث {updated} علاج.")
                MessageBox.Show(okMsg, If(Eng, "Success", "نجاح"), MessageBoxButtons.OK, MessageBoxIcon.Information)
                NotifyBalanceUpdate()
                Me.DialogResult = DialogResult.OK
            Else
                Saved = False
                MessageBox.Show(If(Eng, "No rows were updated.", "لم يتم تحديث أي سجل."),
                                If(Eng, "No changes", "بدون تغيير"), MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If
        Catch ex As Exception
            Saved = False
            MessageBox.Show(ex.Message, If(Eng, "Save error", "خطأ في الحفظ"), MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub NotifyBalanceUpdate()
        Dim patientToUpdate As Patient = Nothing
        If FormManager.Instance IsNot Nothing Then patientToUpdate = FormManager.Instance.GetCurrentPatient()
        If patientToUpdate Is Nothing Then patientToUpdate = PasswordSecurity.CurrentPatient
        If FormManager.Instance IsNot Nothing AndAlso FormManager.Instance.CurrentForm IsNot Nothing AndAlso patientToUpdate IsNot Nothing Then
            FormManager.Instance.CurrentForm.UpdatePatientBalance(patientToUpdate)
        End If
    End Sub

    ''' <summary>Single SqlTransaction: per row apply ticked fields, call <see cref="Patient_ToothTrtDATA.UpdateTransactional"/>, then sync <c>Patient_Trts</c> + first <c>Patient_Pays</c> row when the price/date/pay fields are ticked.</summary>
    Private Function ApplyBulkChanges(targets As List(Of Patient_ToothTrt)) As Integer
        Dim dao As New Patient_ToothTrtDATA
        Dim cs = DentistXDATA.GetConnection.ConnectionString
        Dim updatedCount As Integer = 0

        Dim applyTreatDate As Boolean = chkApplyTreatDate.Checked
        Dim newTreatDate As Date = If(applyTreatDate, dtTreatDate.DateTime, Date.MinValue)

        Dim applyTreatmentType As Boolean = chkApplyTreatmentType.Checked
        Dim newTreatmentType As String = If(applyTreatmentType, CurrentTreatmentTypeEnglish(), "")

        Dim applyFinished As Boolean = chkApplyFinished.Checked
        Dim newFinished As Boolean = ceFinished.Checked
        Dim newTreatEnd As Date? = Nothing
        If applyFinished AndAlso newFinished Then newTreatEnd = dtTreatEnd.DateTime

        Dim applyIsExternal As Boolean = chkApplyIsExternal.Checked
        Dim newIsExternal As Boolean = ceIsExternal.Checked
        Dim newExtClinic As String = ""
        If applyIsExternal Then
            If newIsExternal Then
                newExtClinic = If(String.IsNullOrWhiteSpace(txtExtClinic.Text), "Somewhere Else", txtExtClinic.Text.Trim())
            Else
                newExtClinic = "In House"
            End If
        End If

        Dim applyTrtValue As Boolean = chkApplyTrtValue.Checked
        Dim newTrtValue As Decimal = ParseBoundDecimal(txtTrtValue.Text)

        Dim applyPayValue As Boolean = chkApplyPayValue.Checked
        Dim newPayValue As Decimal = ParseBoundDecimal(txtPayValue.Text)

        Dim applyTrtPlan As Boolean = chkApplyTrtPlan.Checked
        Dim newTrtPlan As String = If(applyTrtPlan, txtTrtPlan.Text, Nothing)

        Dim applyTrtDetails As Boolean = chkApplyTrtDetails.Checked
        Dim newTrtDetails As String = If(applyTrtDetails, txtTrtDetails.Text, Nothing)

        Dim applyTrtNotes As Boolean = chkApplyTrtNotes.Checked
        Dim newTrtNotes As String = If(applyTrtNotes, txtTrtNotes.Text, Nothing)

        Using conn As New SqlConnection(cs)
            conn.Open()
            Using trans As SqlTransaction = conn.BeginTransaction()
                Try
                    For Each oldRow In targets
                        Dim newRow As Patient_ToothTrt = oldRow.Clone()

                        If applyTreatDate Then newRow.TreatDate = newTreatDate
                        If applyTreatmentType Then newRow.TreatmentType = newTreatmentType
                        If applyFinished Then
                            newRow.Finished = If(newFinished, CByte(1), CByte(0))
                            newRow.TreatEndDate = If(newFinished, newTreatEnd, CType(Nothing, Date?))
                        End If
                        If applyIsExternal Then
                            newRow.IsExternal = newIsExternal
                            newRow.ExternalClinicName = newExtClinic
                        End If
                        If applyTrtValue Then newRow.TrtValue = newTrtValue
                        If applyPayValue Then newRow.PayValue = newPayValue
                        If applyTrtPlan Then newRow.TreatPlan = newTrtPlan
                        If applyTrtDetails Then newRow.TreatDetails = newTrtDetails
                        If applyTrtNotes Then newRow.TreatNotes = newTrtNotes

                        SyncIsPaidFlag(newRow)

                        If Not dao.UpdateTransactional(conn, trans, oldRow, newRow) Then
                            Throw New Exception($"Failed to update ToothTrtID {oldRow.ToothTrtID}.")
                        End If

                        SyncPatientTrtsAndPays(conn, trans, oldRow, newRow,
                                               applyTrtValue, applyTreatDate, applyPayValue)

                        updatedCount += 1
                    Next

                    trans.Commit()
                Catch
                    trans.Rollback()
                    Throw
                End Try
            End Using
        End Using

        Return updatedCount
    End Function

    ''' <summary>Mirrors <c>EditTreatFrom.ApplyPayAndPaidFlags</c>: external = paid; otherwise paid when first-payment is non-zero or price is non-zero.</summary>
    Private Sub SyncIsPaidFlag(newT As Patient_ToothTrt)
        If newT.IsExternal Then
            newT.IsPaid = True
            Return
        End If
        Dim trtVal As Decimal = newT.TrtValue
        Dim payVal As Decimal = newT.PayValue
        If payVal > 0 OrElse trtVal > 0 Then
            newT.IsPaid = (payVal > 0)
        ElseIf trtVal = 0 AndAlso payVal = 0 Then
            newT.IsPaid = False
        End If
    End Sub

    Private Sub SyncPatientTrtsAndPays(conn As SqlConnection, trans As SqlTransaction,
                                       oldRow As Patient_ToothTrt, newRow As Patient_ToothTrt,
                                       applyTrtValue As Boolean, applyTreatDate As Boolean, applyPayValue As Boolean)
        If newRow.IsExternal Then Return
        If Not (applyTrtValue OrElse applyTreatDate OrElse applyPayValue) Then Return

        Dim trtRow = conn.QuerySingleOrDefault(Of Patient_Trts)(
            "SELECT TOP 1 * FROM Patient_Trts WHERE PatientID = @PatientID AND ToothTrtID = @ToothTrtID",
            New With {.PatientID = oldRow.PatientID, .ToothTrtID = oldRow.ToothTrtID}, trans)
        If trtRow Is Nothing Then Return

        If applyTrtValue OrElse applyTreatDate Then
            Dim detailText As String = FormatPatientTrtsDetail(newRow.Treat, newRow.ToothNum)
            Dim trtDate As Date = If(newRow.TreatDate.HasValue, newRow.TreatDate.Value, Date.Today)
            conn.Execute(
                "UPDATE Patient_Trts SET Detail = @Detail, TrtDate = @TrtDate, TrtValue = @TrtValue WHERE TrtID = @TrtID",
                New With {
                    .Detail = detailText,
                    .TrtDate = trtDate,
                    .TrtValue = newRow.TrtValue,
                    .TrtID = trtRow.TrtID
                }, trans)
        End If

        If applyPayValue Then
            Dim pays = conn.Query(Of Patient_Pays)(
                "SELECT * FROM Patient_Pays WHERE TrtID = @TrtID ORDER BY PayID",
                New With {.TrtID = trtRow.TrtID}, trans).ToList()
            Dim payDate As Date = If(newRow.TreatDate.HasValue, newRow.TreatDate.Value, Date.Today)
            Dim payVal As Decimal = If(newRow.PayValue < 0, 0D, newRow.PayValue)

            If pays.Count = 0 Then
                If payVal > 0 Then
                    conn.Execute(
                        "INSERT INTO Patient_Pays (TrtID, PatientID, PayValue, PayDate, Notes, PayType, ReceivedBy, IsReturned) VALUES (@TrtID, @PatientID, @PayValue, @PayDate, @Notes, @PayType, @ReceivedBy, @IsReturned)",
                        New With {
                            .TrtID = trtRow.TrtID,
                            .PatientID = newRow.PatientID,
                            .PayValue = payVal,
                            .PayDate = payDate,
                            .Notes = "",
                            .PayType = "Cash",
                            .ReceivedBy = CType(Nothing, String),
                            .IsReturned = False
                        }, trans)
                End If
            Else
                Dim target = pays(0)
                conn.Execute(
                    "UPDATE Patient_Pays SET PayValue = @PayValue, PayDate = @PayDate WHERE PayID = @PayID",
                    New With {
                        .PayValue = payVal,
                        .PayDate = payDate,
                        .PayID = target.PayID
                    }, trans)
            End If
        End If
    End Sub

End Class
