Imports System.ComponentModel
Imports System.Data.SqlClient
Imports System.Windows.Forms
Imports Dapper
Imports DentistX

Public Class FrmPatientAccnt
    Private ReadOnly _patient As Patient
    Private ReadOnly _rows As New BindingList(Of AccountRow)
    Private useEng As Boolean = False
    Private _whatsBs As BindingSource
    Private _whatsBindLoading As Boolean
    Private _whatsFullDigitsSnapshot As String = ""
    Private _inWhatsSave As Boolean
    Private _inPrefixLocalSync As Boolean
    ''' <summary>While True, Leave/Validated must not persist — modal dialogs (e.g. after send) desync bindings and would overwrite UI / patient with stale control state.</summary>
    Private _suppressAutoWhatsSave As Boolean

    Public Sub New(patient As Patient)
        InitializeComponent()
        _patient = patient

        If _patient IsNot Nothing Then
            Dim baseTextEn As String = "Send Patient Account WhatsApp Message"
            Dim baseTextAr As String = "إرسال ملخص حساب المريض عبر واتساب"
            Dim namePart As String = If(String.IsNullOrWhiteSpace(_patient.PatientName), "", $" - {_patient.PatientName}")
            lblSendTo.Text = If(Eng, baseTextEn, baseTextAr) & namePart

            FillCboPrefixOnce(cboPrefix)
        End If
    End Sub

    ''' <summary>Earliest treatment date for this patient only (Patient_Trts.PatientID = patientId).</summary>
    Private Function GetFirstTreatDateForThisPatient(patientId As Integer) As Date?
        If patientId <= 0 Then Return Nothing
        Dim minSqlDate = New Date(1900, 1, 1)
        Using conn As New SqlConnection(DentistXDATA.GetConnection.ConnectionString)
            conn.Open()
            Dim o1 = conn.ExecuteScalar(
                "SELECT MIN(TrtDate) FROM Patient_Trts WHERE PatientID = @PatientID AND TrtDate > @MinDate",
                New With {.PatientID = patientId, .MinDate = minSqlDate})
            If o1 IsNot Nothing AndAlso Not IsDBNull(o1) Then
                Dim d = Convert.ToDateTime(o1).Date
                If d.Year >= 1900 Then Return d
            End If
            Dim o2 = conn.ExecuteScalar(
                "SELECT TOP 1 TrtDate FROM Patient_Trts WHERE PatientID = @PatientID ORDER BY TrtID ASC",
                New With {.PatientID = patientId})
            If o2 IsNot Nothing AndAlso Not IsDBNull(o2) Then
                Dim d2 = Convert.ToDateTime(o2).Date
                If d2 > minSqlDate AndAlso d2.Year >= 1900 Then Return d2
            End If
        End Using
        Return Nothing
    End Function

    Private Sub FrmPatientAccnt_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If _patient IsNot Nothing AndAlso _patient.PatientID > 0 Then
            ReloadPatientWhatsFromDatabase()
        End If
        SetupWhatsPatientBinding()
        If _patient IsNot Nothing Then
            _whatsFullDigitsSnapshot = If(WhatsHelper.GetFullWhatsDigitsFromPatient(_patient), "")
        End If

        Dim fromDefault = Date.Today.AddMonths(-3)
        If _patient IsNot Nothing AndAlso _patient.PatientID > 0 Then
            Dim firstTreat = GetFirstTreatDateForThisPatient(_patient.PatientID)
            If firstTreat.HasValue Then fromDefault = firstTreat.Value
        End If
        dtpFrom.EditValue = fromDefault
        dtpTo.EditValue = Date.Today
        gcAccnt.DataSource = _rows
        LoadStatusFilter()
        LoadAccountRows()
        RefreshLblWhatsFromPatient()
    End Sub

    Private Sub ReloadPatientWhatsFromDatabase()
        If _patient Is Nothing OrElse _patient.PatientID <= 0 Then Return
        Try
            Dim pd As New PatientDATA()
            Dim row = pd.Select_RecordByID(_patient.PatientID)
            If row Is Nothing Then Return
            _patient.WhatsApp = row.WhatsApp
            _patient.WhatsAppPrefix = row.WhatsAppPrefix
            _patient.Phone = row.Phone
        Catch
        End Try
    End Sub

    ''' <summary>Binds <c>cboPrefix</c> and <c>txtWhats</c> to the same <see cref="Patient"/> instance (WhatsApp / WhatsAppPrefix).</summary>
    Private Sub SetupWhatsPatientBinding()
        If _patient Is Nothing Then Return
        If _whatsBs IsNot Nothing Then Return

        _whatsBindLoading = True
        Try
            _whatsBs = New BindingSource(New List(Of Patient) From {_patient}, Nothing)

            Dim bLocal As New Binding("EditValue", _whatsBs, "WhatsApp", True, DataSourceUpdateMode.OnValidation)
            AddHandler bLocal.Format, Sub(s As Object, conv As ConvertEventArgs)
                                          Dim p = TryCast(_whatsBs.Current, Patient)
                                          conv.Value = If(WhatsHelper.GetLocalWhatsDisplayForPatient(p, cboPrefix), "")
                                      End Sub
            AddHandler bLocal.Parse, Sub(s As Object, conv As ConvertEventArgs)
                                         conv.Value = WhatsHelper.NormalizeLocalWhatsTenDigitsForStorage(Convert.ToString(conv.Value))
                                     End Sub
            txtWhats.DataBindings.Add(bLocal)

            Dim bPrefix As New Binding("EditValue", _whatsBs, "WhatsAppPrefix", True, DataSourceUpdateMode.OnPropertyChanged)
            AddHandler bPrefix.Format, Sub(s As Object, conv As ConvertEventArgs)
                                           Dim p = TryCast(_whatsBs.Current, Patient)
                                           Dim col = If(p Is Nothing, "", If(p.WhatsAppPrefix, ""))
                                           conv.Value = WhatsHelper.GetResolvedWhatsAppPrefixForBinding(cboPrefix, col)
                                       End Sub
            AddHandler bPrefix.Parse, Sub(s As Object, conv As ConvertEventArgs)
                                          conv.Value = WhatsHelper.GetPrefixTextForStorage(cboPrefix)
                                      End Sub
            cboPrefix.DataBindings.Add(bPrefix)

            _whatsBs.ResetBindings(False)
        Finally
            _whatsBindLoading = False
        End Try
    End Sub

    Private Sub SavePatientWhatsToDatabaseIfDirty()
        If _suppressAutoWhatsSave Then Return
        If _inWhatsSave Then Return
        If _whatsBindLoading OrElse Disposing OrElse IsDisposed Then Return
        If _patient Is Nothing OrElse _patient.PatientID <= 0 OrElse _whatsBs Is Nothing Then Return
        If cboPrefix.IsDisposed OrElse txtWhats.IsDisposed Then Return
        _inWhatsSave = True
        Try
            ' Do not call ValidateChildren here — it re-validates txtWhats and fires Validated again → infinite recursion.
            _whatsBs.EndEdit()
            Dim w = WhatsHelper.NormalizeLocalWhatsTenDigitsForStorage(If(_patient.WhatsApp, ""))
            Dim px = WhatsHelper.GetPrefixTextForStorage(cboPrefix)
            _patient.WhatsApp = w
            _patient.WhatsAppPrefix = px
            Dim nowFull = If(WhatsHelper.GetFullWhatsDigitsFromPatient(_patient), "")
            If String.Equals(nowFull, _whatsFullDigitsSnapshot, StringComparison.Ordinal) Then Return
            Try
                Dim pd As New PatientDATA()
                If pd.UpdateWhatsAppAndPrefix(_patient.PatientID, w, px) Then
                    _whatsFullDigitsSnapshot = nowFull
                    ReloadPatientWhatsFromDatabase()
                    SyncWhatsBindingsFromPatient()
                End If
            Catch
            End Try
        Finally
            _inWhatsSave = False
        End Try
    End Sub

    Private Sub SyncWhatsBindingsFromPatient()
        If _whatsBs Is Nothing OrElse _patient Is Nothing Then Return
        _whatsBindLoading = True
        Try
            _whatsBs.ResetBindings(False)
        Finally
            _whatsBindLoading = False
        End Try
        RefreshLblWhatsFromPatient()
    End Sub

    Private Sub RefreshLblWhatsFromPatient()
        If lblWhats IsNot Nothing AndAlso _patient IsNot Nothing Then
            lblWhats.Text = WhatsHelper.GetFullWhatsDigitsFromPatient(_patient)
        End If
    End Sub

    Private Sub LoadStatusFilter()
        cboStatus.Properties.Items.Clear()
        cboStatus.Properties.Items.Add("(All)")
        cboStatus.Properties.Items.Add("Treatments only")
        cboStatus.Properties.Items.Add("Payments only")
        cboStatus.SelectedIndex = 0
    End Sub

    Private Sub btnApplyFilter_Click(sender As Object, e As EventArgs) Handles btnApplyFilter.Click
        LoadAccountRows()
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

    Private Sub LoadAccountRows()
        _rows.Clear()
        If _patient Is Nothing OrElse _patient.PatientID <= 0 Then Return

        Dim fromDate = CType(dtpFrom.EditValue, DateTime?).GetValueOrDefault().Date
        Dim toDate = CType(dtpTo.EditValue, DateTime?).GetValueOrDefault().Date

        Dim trts As List(Of Patient_Trts)
        Dim pays As List(Of Patient_Pays)

        Using conn As New SqlConnection(DentistXDATA.GetConnection.ConnectionString)
            conn.Open()
            trts = conn.Query(Of Patient_Trts)(
                "SELECT * FROM Patient_Trts WHERE PatientID=@ID AND TrtDate >= @From AND TrtDate <= @To",
                New With {.ID = _patient.PatientID, .From = fromDate, .To = toDate}).ToList()
            pays = conn.Query(Of Patient_Pays)(
                "SELECT * FROM Patient_Pays WHERE PatientID=@ID AND PayDate >= @From AND PayDate <= @To",
                New With {.ID = _patient.PatientID, .From = fromDate, .To = toDate}).ToList()
        End Using

        Dim includeZeroTrts = chkIncludeZeroTrts.Checked
        If Not includeZeroTrts Then
            trts = trts.Where(Function(t) t.TrtValue > 0D).ToList()
        End If

        Dim showTrts As Boolean = True
        Dim showPays As Boolean = True
        If cboStatus.SelectedIndex = 1 Then
            showPays = False
        ElseIf cboStatus.SelectedIndex = 2 Then
            showTrts = False
        End If

        If showTrts Then
            For Each t In trts.OrderBy(Function(x) x.TrtDate)
                Dim dt = WhatsHelper.FormatWhatsAppDateLongOrEmpty(t.TrtDate, Eng)
                Dim detail = If(t.Detail, "")
                Dim val = t.TrtValue.ToString("N2")
                Dim extra As String = ""
                If t.Discount.HasValue AndAlso t.Discount.Value <> 0D Then
                    extra = If(Eng,
                               "Disc: " & t.Discount.Value.ToString("N2"),
                               "خصم: " & t.Discount.Value.ToString("N2"))
                End If
                _rows.Add(New AccountRow With {
                    .Send = False,
                    .DateText = dt,
                    .TypeText = If(Eng, "Treat", "علاج"),
                    .Detail = detail,
                    .ValueText = val,
                    .Extra = extra,
                    .Trt = t
                })
            Next
        End If

        If showPays Then
            For Each p In pays.OrderBy(Function(x) x.PayDate)
                Dim dt = WhatsHelper.FormatWhatsAppDateLongOrEmpty(p.PayDate, Eng)
                Dim pType = If(p.PayType, "")
                Dim val = p.PayValue.ToString("N2")
                Dim extra As String = ""
                If String.Equals(p.PayType, "Cheque", StringComparison.OrdinalIgnoreCase) Then
                    If Not String.IsNullOrWhiteSpace(p.ChqNumber) Then extra &= "Chq#: " & p.ChqNumber & " "
                    If p.ChqDueDate.HasValue Then extra &= If(Eng, "Due: ", "استحقاق: ") & WhatsHelper.FormatWhatsAppDateLong(p.ChqDueDate.Value, Eng) & " "
                    If Not String.IsNullOrWhiteSpace(p.ChqBank) Then extra &= "Bank: " & p.ChqBank & " "
                End If
                _rows.Add(New AccountRow With {
                    .Send = False,
                    .DateText = dt,
                    .TypeText = If(Eng, "Payment", "دفعة"),
                    .Detail = pType,
                    .ValueText = val,
                    .Extra = extra.Trim(),
                    .Pay = p
                })
            Next
        End If
    End Sub

    Private Async Sub BtnSendMessage_Click(sender As Object, e As EventArgs) Handles BtnSendMessage.Click
        Dim number As String = If(lblWhats.Text, "").Trim()
        If String.IsNullOrWhiteSpace(number) Then
            MessageBox.Show("أدخل رقم الجوال.", "إرسال", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtWhats.Focus()
            Return
        End If

        If _patient Is Nothing OrElse _patient.PatientID <= 0 Then
            MessageBox.Show("لا يوجد مريض محدد.", "إرسال", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim selectedTrts As New List(Of Patient_Trts)(
            _rows.Where(Function(r) r.Send AndAlso r.Trt IsNot Nothing).Select(Function(r) r.Trt))
        Dim selectedPays As New List(Of Patient_Pays)(
            _rows.Where(Function(r) r.Send AndAlso r.Pay IsNot Nothing).Select(Function(r) r.Pay))

        If selectedTrts.Count = 0 AndAlso selectedPays.Count = 0 Then
            MessageBox.Show("اختر عناصر من الحساب لإرسالها.", "إرسال", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        SavePatientWhatsToDatabaseIfDirty()

        BtnSendMessage.Enabled = False
        _suppressAutoWhatsSave = True
        Try
            If Not Await WhatsAppService.EnsureWhatsAppConnectedOrNotifyAsync(Me) Then Return
            Dim clinicId As String = WhatsAppService.GetCurrentClinicId()
            Dim waService As New WhatsAppService()

            Dim msg As String = WhatsHelper.BuildAccountingWhatsAppMessage(
                _patient.PatientName,
                selectedTrts,
                selectedPays,
                excludeZeroValueTreatments:=Not chkIncludeZeroTrts.Checked,
                useArabic:=Not useEng,
                chkFullDetail.Checked,
                patientSex:=_patient.Sex)

            If String.IsNullOrWhiteSpace(msg) Then
                MessageBox.Show("لا توجد بيانات كافية لإرسالها.", "إرسال", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return
            End If

            Dim ctx As New WhatsAppSendContext With {
                .Category = WhatsAppMessageCategories.PatientAccount,
                .PatientId = _patient.PatientID,
                .DisplayName = _patient.PatientName,
                .SourceHint = NameOf(FrmPatientAccnt),
                .RevealMessageCenter = True
            }
            Await waService.SendMessageAsync(clinicId, number, msg, "", ctx)

            MessageBox.Show("تم وضع ملخص الحساب في طابور الإرسال.", "إرسال", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "خطأ في الإرسال", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            _suppressAutoWhatsSave = False
            BtnSendMessage.Enabled = True
            SyncWhatsBindingsFromPatient()
        End Try
    End Sub

    Private Sub BtnSendAllAccnt_Click(sender As Object, e As EventArgs) Handles BtnSendAllAccnt.Click
        For Each r In _rows
            r.Send = True
        Next
        gvAccnt.RefreshData()
        BtnSendMessage_Click(sender, e)
    End Sub

    Private Sub cboPrefix_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboPrefix.SelectedIndexChanged
        If _whatsBindLoading OrElse _whatsBs Is Nothing Then Return
        If _inPrefixLocalSync Then Return
        _inPrefixLocalSync = True
        Try
            Try
                If txtWhats.DataBindings.Count > 0 Then txtWhats.DataBindings("EditValue").ReadValue()
            Catch
            End Try
            RefreshLblWhatsFromPatient()
            SavePatientWhatsToDatabaseIfDirty()
        Finally
            _inPrefixLocalSync = False
        End Try
    End Sub

    Private Sub cboPrefix_EditValueChanged(sender As Object, e As EventArgs) Handles cboPrefix.EditValueChanged
        RefreshLblWhatsFromPatient()
    End Sub

    Private Sub txtWhats_EditValueChanged(sender As Object, e As EventArgs) Handles txtWhats.EditValueChanged
        RefreshLblWhatsFromPatient()
    End Sub

    Private Sub txtWhats_Leave(sender As Object, e As EventArgs) Handles txtWhats.Leave
        SavePatientWhatsToDatabaseIfDirty()
    End Sub

    Private Sub txtWhats_Validated(sender As Object, e As EventArgs) Handles txtWhats.Validated
        SavePatientWhatsToDatabaseIfDirty()
    End Sub

    Private Sub RadioLang_SelectedIndexChanged(sender As Object, e As EventArgs) Handles RadioLang.SelectedIndexChanged
        If RadioLang.SelectedIndex = 0 Then
            useEng = False
        ElseIf RadioLang.SelectedIndex = 1 Then
            useEng = True
        End If
    End Sub


    Private Class AccountRow
        Public Property Send As Boolean
        Public Property DateText As String
        Public Property TypeText As String
        Public Property Detail As String
        Public Property ValueText As String
        Public Property Extra As String
        Public Property Trt As Patient_Trts
        Public Property Pay As Patient_Pays
    End Class


End Class
