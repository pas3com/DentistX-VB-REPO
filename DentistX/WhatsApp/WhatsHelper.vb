Imports System
Imports System.Drawing
Imports System.Globalization
Imports System.Linq
Imports System.Text
Imports System.Windows.Forms
Imports DevExpress.XtraEditors

Module WhatsHelper

    Private ReadOnly CultureWhatsAppEnglish As CultureInfo = CultureInfo.GetCultureInfo("en-GB")
    Private ReadOnly CultureWhatsAppArabic As CultureInfo = SettingsRuntimeApply.CreateArabicRegionalCultureGregorian()

    ' --- Centralized country prefix + local Whats (10 digits starting with 0, store 0 + 9 national) ---

    ''' <summary>WhatsApp patient-facing date: weekday + dd/MM/yyyy. Arabic uses U+060C and LTR-isolated digits so RTL layout stays readable.</summary>
    Public Function FormatWhatsAppDateLong(d As DateTime, useEng As Boolean) As String
        Dim day = d.Date
        If useEng Then
            Return day.ToString("dddd, dd/MM/yyyy", CultureWhatsAppEnglish)
        End If
        Dim dayName = CultureWhatsAppArabic.DateTimeFormat.GetDayName(day.DayOfWeek)
        Dim nums = day.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture)
        Const lri As Char = ChrW(&H2066)
        Const pdi As Char = ChrW(&H2069)
        Return dayName & ChrW(&H60C) & " " & lri & nums & pdi
    End Function

    ''' <summary>Same as <see cref="FormatWhatsAppDateLong"/> or empty when the date is unset.</summary>
    Public Function FormatWhatsAppDateLongOrEmpty(d As DateTime, useEng As Boolean) As String
        If d = DateTime.MinValue OrElse d.Year < 1900 Then Return ""
        Return FormatWhatsAppDateLong(d, useEng)
    End Function

    ''' <summary>Weekday only for appointment WhatsApp lines (e.g. English "Monday," or Arabic "الإثنين،").</summary>
    Public Function FormatWhatsAppAppointmentWeekdayOnly(d As DateTime, useEng As Boolean) As String
        Dim day = d.Date
        If useEng Then
            Return day.ToString("dddd", CultureWhatsAppEnglish) & ","
        End If
        Return CultureWhatsAppArabic.DateTimeFormat.GetDayName(day.DayOfWeek) & ChrW(&H60C)
    End Function

    ''' <summary>dd/MM/yyyy only; Arabic wraps digits in LRI/PDI for RTL display (same as <see cref="FormatWhatsAppDateLong"/> numeric part).</summary>
    Public Function FormatWhatsAppAppointmentDateNumbersOnly(d As DateTime, useEng As Boolean) As String
        Dim day = d.Date
        Dim nums = day.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture)
        If useEng Then Return nums
        Const lri As Char = ChrW(&H2066)
        Const pdi As Char = ChrW(&H2069)
        Return lri & nums & pdi
    End Function

    ''' <summary>12-hour clock (h:mm) plus Arabic period phrase for appointment lines: صباحا / ظهرا / بعد الظهر / بعد العصر / مساءا.</summary>
    Private Function FormatArabicAppointmentClockWithPeriod(t As DateTime) As String
        Dim clock = t.ToString("h:mm", CultureInfo.InvariantCulture)
        Dim tod = t.TimeOfDay
        If tod < TimeSpan.FromHours(12) Then
            Return clock & " صباحا"
        End If
        If tod < TimeSpan.FromHours(14) Then
            Return clock & " ظهرا"
        End If
        If tod < TimeSpan.FromHours(16) Then
            Return clock & " بعد الظهر"
        End If
        Dim t1630 = New TimeSpan(17, 30, 0)
        If tod < t1630 Then
            Return clock & " بعد العصر"
        End If
        If tod < TimeSpan.FromHours(23) Then
            Return clock & " مساءا"
        End If
        Return clock
    End Function

    ''' <summary>Full combo display value for <see cref="Patient.WhatsAppPrefix"/> (e.g. "Palestine (+970)").</summary>
    Public Function GetPrefixTextForStorage(cboPrefix As ComboBoxEdit) As String
        If cboPrefix Is Nothing Then Return ""
        If cboPrefix.EditValue IsNot Nothing AndAlso Not String.IsNullOrWhiteSpace(cboPrefix.EditValue.ToString()) Then
            Return cboPrefix.EditValue.ToString().Trim()
        End If
        Return If(cboPrefix.Text, "").Trim()
    End Function

    ''' <summary>Digit-only country code from the selected combo item (e.g. 970).</summary>
    Public Function GetPrefixDigitsFromCombo(cboPrefix As ComboBoxEdit) As String
        Return NormalizeWhatsDigits(GetPrefixTextForStorage(cboPrefix))
    End Function

    ''' <summary>
    ''' Normalizes the local part for <see cref="Patient.WhatsApp"/>: digits only, 10 characters when complete,
    ''' first digit 0 and following 9 national digits (e.g. 0599123456). Empty if no digits.
    ''' </summary>
    Public Function NormalizeLocalWhatsTenDigitsForStorage(localRaw As String) As String
        Dim d = NormalizeWhatsDigits(If(localRaw, ""))
        If d.Length = 0 Then Return ""
        If d.Length = 10 AndAlso d.StartsWith("0"c) Then Return d
        If d.Length = 9 Then Return "0"c & d
        If d.Length < 9 Then Return d
        If d.Length = 10 AndAlso Not d.StartsWith("0"c) Then Return "0"c & d.Substring(0, 9)
        If d.Length > 10 AndAlso d.StartsWith("0"c) Then Return d.Substring(0, 10)
        Return d
    End Function

    ''' <summary>Full international digits for lblWhats / API: prefix digits + 9 digits after removing a single leading 0 from 10-digit local input.</summary>
    Public Function BuildInternationalWhatsDigitsFromControls(cboPrefix As ComboBoxEdit, txtWhats As TextEdit) As String
        Dim localRaw = ""
        If txtWhats IsNot Nothing AndAlso txtWhats.Text IsNot Nothing Then localRaw = txtWhats.Text.ToString().Trim()
        Return BuildInternationalWhatsDigits(localRaw, GetPrefixTextForStorage(cboPrefix))
    End Function

    Private Function FindLongestMatchingPrefixIndex(allDigits As String, cboPrefix As ComboBoxEdit) As Integer
        If cboPrefix Is Nothing OrElse cboPrefix.Properties.Items.Count = 0 Then Return -1
        Dim bestI As Integer = -1
        Dim bestLen As Integer = 0
        For i As Integer = 0 To cboPrefix.Properties.Items.Count - 1
            Dim id = NormalizeWhatsDigits(If(cboPrefix.Properties.Items(i)?.ToString(), ""))
            If id.Length = 0 Then Continue For
            If allDigits.StartsWith(id) AndAlso allDigits.Length > id.Length Then
                If id.Length > bestLen Then
                    bestLen = id.Length
                    bestI = i
                End If
            End If
        Next
        Return bestI
    End Function

    Private Sub SelectComboByPrefixDigits(cboPrefix As ComboBoxEdit, prefixDigits As String)
        If cboPrefix Is Nothing OrElse String.IsNullOrWhiteSpace(prefixDigits) Then Return
        For i As Integer = 0 To cboPrefix.Properties.Items.Count - 1
            If NormalizeWhatsDigits(If(cboPrefix.Properties.Items(i)?.ToString(), "")) = prefixDigits Then
                cboPrefix.SelectedIndex = i
                Exit For
            End If
        Next
    End Sub

    ''' <summary>Resolves stored <see cref="Patient.WhatsAppPrefix"/> to a combo item display line for data-binding <c>EditValue</c>.</summary>
    Public Function GetResolvedWhatsAppPrefixForBinding(cboPrefix As ComboBoxEdit, whatsAppPrefixColumn As String) As String
        If cboPrefix Is Nothing OrElse cboPrefix.Properties.Items.Count = 0 Then Return If(whatsAppPrefixColumn, "").Trim()
        Dim trimmed = If(whatsAppPrefixColumn, "").Trim()
        If trimmed.Length > 0 Then
            For i As Integer = 0 To cboPrefix.Properties.Items.Count - 1
                Dim it = If(cboPrefix.Properties.Items(i)?.ToString(), "").Trim()
                If String.Equals(it, trimmed, StringComparison.Ordinal) Then Return it
            Next
        End If
        Dim dig = NormalizeWhatsDigits(trimmed)
        If dig.Length > 0 Then
            For i As Integer = 0 To cboPrefix.Properties.Items.Count - 1
                Dim it = If(cboPrefix.Properties.Items(i)?.ToString(), "").Trim()
                If NormalizeWhatsDigits(it) = dig Then Return it
            Next
        End If
        Return trimmed
    End Function

    ''' <summary>Local Whats line shown in <c>txtWhats</c> (10-digit with leading 0 when applicable), from patient fields and optional prefix combo.</summary>
    Public Function GetLocalWhatsDisplayForPatient(p As Patient, cboPrefix As ComboBoxEdit) As String
        If p Is Nothing Then Return ""
        Dim source As String = If(p.WhatsApp, "").Trim()
        If String.IsNullOrWhiteSpace(source) Then source = If(p.Phone, "").Trim()
        Dim prefixStoredDigits As String = NormalizeWhatsDigits(If(p.WhatsAppPrefix, ""))
        Dim allDigits As String = NormalizeWhatsDigits(source)
        If cboPrefix Is Nothing OrElse cboPrefix.Properties.Items.Count = 0 Then Return source
        If String.IsNullOrWhiteSpace(allDigits) Then Return ""
        If prefixStoredDigits.Length > 0 AndAlso allDigits.StartsWith(prefixStoredDigits) AndAlso allDigits.Length > prefixStoredDigits.Length Then
            Return NormalizeLocalWhatsTenDigitsForStorage(allDigits.Substring(prefixStoredDigits.Length))
        End If
        If allDigits.Length = 10 AndAlso allDigits.StartsWith("0"c) Then Return allDigits
        If allDigits.Length = 9 AndAlso Not source.StartsWith("+"c) Then Return "0"c & allDigits
        Dim idx As Integer = FindLongestMatchingPrefixIndex(allDigits, cboPrefix)
        If idx >= 0 Then
            Dim pfxItem As String = NormalizeWhatsDigits(If(cboPrefix.Properties.Items(idx)?.ToString(), ""))
            Return NormalizeLocalWhatsTenDigitsForStorage(allDigits.Substring(pfxItem.Length))
        End If
        Return source
    End Function

    ''' <summary>Selects combo by exact <see cref="Patient.WhatsAppPrefix"/> text, else by embedded calling code digits (legacy rows).</summary>
    Public Sub SelectComboFromStoredPrefixColumn(cboPrefix As ComboBoxEdit, whatsAppPrefixColumn As String)
        If cboPrefix Is Nothing OrElse cboPrefix.Properties.Items.Count = 0 Then Return
        Dim trimmed = If(whatsAppPrefixColumn, "").Trim()
        If trimmed.Length > 0 Then
            For i As Integer = 0 To cboPrefix.Properties.Items.Count - 1
                Dim it = If(cboPrefix.Properties.Items(i)?.ToString(), "").Trim()
                If String.Equals(it, trimmed, StringComparison.Ordinal) Then
                    cboPrefix.SelectedIndex = i
                    Return
                End If
            Next
        End If
        SelectComboByPrefixDigits(cboPrefix, NormalizeWhatsDigits(trimmed))
    End Sub

    ''' <summary>After DB update, keep an in-memory <see cref="Patient"/> in sync (workspace / navigator).</summary>
    Public Sub ApplyPersistedWhatsToPatientInstance(p As Patient, cboPrefix As ComboBoxEdit, txtWhats As TextEdit)
        If p Is Nothing Then Return
        p.WhatsApp = NormalizeLocalWhatsTenDigitsForStorage(If(txtWhats?.Text, "").ToString())
        p.WhatsAppPrefix = GetPrefixTextForStorage(cboPrefix)
    End Sub

    ''' <summary>Persists 10-digit local and full combo prefix line (same as <see cref="Patient"/> table).</summary>
    Public Function PersistPatientWhatsNormalized(patientId As Integer, cboPrefix As ComboBoxEdit, txtWhats As TextEdit) As Boolean
        If patientId <= 0 Then Return False
        Try
            Dim pd As New PatientDATA()
            Return pd.UpdateWhatsAppAndPrefix(
                patientId,
                NormalizeLocalWhatsTenDigitsForStorage(If(txtWhats?.Text, "").ToString()),
                GetPrefixTextForStorage(cboPrefix))
        Catch
            Return False
        End Try
    End Function

    ''' <summary>True when DB prefix text (or legacy digits-only) matches the current combo selection.</summary>
    Public Function StoredPrefixMatchesUi(prefixFromDb As String, cboPrefix As ComboBoxEdit) As Boolean
        Dim db = If(prefixFromDb, "").Trim()
        Dim ui = GetPrefixTextForStorage(cboPrefix)
        If String.Equals(db, ui, StringComparison.Ordinal) Then Return True
        Dim dbDigits = NormalizeWhatsDigits(db)
        Dim uiDigits = GetPrefixDigitsFromCombo(cboPrefix)
        If dbDigits.Length > 0 AndAlso String.Equals(dbDigits, uiDigits, StringComparison.Ordinal) Then Return True
        Return String.IsNullOrWhiteSpace(db) AndAlso String.IsNullOrWhiteSpace(ui)
    End Function

    ''' <summary>Full international digits from stored patient fields (same rules as <see cref="BuildInternationalWhatsDigitsFromPatient"/>).</summary>
    Public Function GetFullWhatsDigitsFromPatient(p As Patient) As String
        If p Is Nothing Then Return ""
        Return NormalizeWhatsDigits(BuildInternationalWhatsDigitsFromPatient(If(p.WhatsApp, ""), If(p.Phone, ""), If(p.WhatsAppPrefix, "")))
    End Function

    Public Function GetFullWhatsDigitsFromControls(cboPrefix As ComboBoxEdit, txtWhats As TextEdit) As String
        Return NormalizeWhatsDigits(BuildInternationalWhatsDigitsFromControls(cboPrefix, txtWhats))
    End Function

    ''' <summary>True when prefix column + normalized local match the controls (DB is source of truth for display strings).</summary>
    Public Function PatientWhatsUiMatchesSavedPatient(p As Patient, cboPrefix As ComboBoxEdit, txtWhats As TextEdit) As Boolean
        If p Is Nothing Then Return True
        Dim localDb = NormalizeLocalWhatsTenDigitsForStorage(If(p.WhatsApp, ""))
        Dim localUi = NormalizeLocalWhatsTenDigitsForStorage(If(txtWhats?.Text, "").ToString())
        Return StoredPrefixMatchesUi(If(p.WhatsAppPrefix, ""), cboPrefix) AndAlso
            String.Equals(localDb, localUi, StringComparison.Ordinal)
    End Function

    ''' <summary>Reloads the patient from the database and compares full international digits to the controls.</summary>
    Public Function PatientWhatsUiMatchesDatabase(patientId As Integer, cboPrefix As ComboBoxEdit, txtWhats As TextEdit) As Boolean
        If patientId <= 0 Then Return True
        Try
            Dim pd As New PatientDATA()
            Dim p = pd.Select_RecordByID(patientId)
            If p Is Nothing Then Return True
            Return PatientWhatsUiMatchesSavedPatient(p, cboPrefix, txtWhats)
        Catch
            Return True
        End Try
    End Function

    ''' <summary>True when the UI full number differs from the value stored in the database.</summary>
    Public Function HasPatientWhatsChangedVsDatabase(patientId As Integer, cboPrefix As ComboBoxEdit, txtWhats As TextEdit) As Boolean
        If patientId <= 0 Then Return False
        Return Not PatientWhatsUiMatchesDatabase(patientId, cboPrefix, txtWhats)
    End Function

    ''' <summary>
    ''' Fills prefix list once, loads the patient row from the database, binds <c>cboPrefix</c> / <c>txtWhats</c>,
    ''' then optionally applies full digits to a label (pass a lambda that sets <c>lblWhats.Text</c>).
    ''' Use when a panel/form with Whats controls is shown so values always match <c>Patient</c>.
    ''' </summary>
    Public Sub ReloadWhatsControlsFromDatabase(patientId As Integer, cboPrefix As ComboBoxEdit, txtWhats As TextEdit, Optional applyFullDigitsToLabel As Action(Of String) = Nothing)
        If patientId <= 0 Then Return
        Try
            FillCboPrefixOnce(cboPrefix)
            Dim pd As New PatientDATA()
            Dim p = pd.Select_RecordByID(patientId)
            If p Is Nothing Then Return
            BindPatientWhatsPrefixAndLocal(cboPrefix, txtWhats, p)
            If applyFullDigitsToLabel IsNot Nothing Then
                applyFullDigitsToLabel(BuildInternationalWhatsDigitsFromControls(cboPrefix, txtWhats))
            End If
        Catch
        End Try
    End Sub

    ''' <summary>
    ''' Normalizes a WhatsApp/phone string to digits only.
    ''' Returns empty string for null/whitespace.
    ''' </summary>
    Public Function NormalizeWhatsDigits(number As String) As String
        If String.IsNullOrWhiteSpace(number) Then Return ""
        Return New String(number.Where(Function(c) Char.IsDigit(c)).ToArray())
    End Function

    ''' <summary>
    ''' Shared validation for WhatsApp/phone numbers.
    ''' For 970/972 expects 12 digits (prefix + 9 without leading 0); otherwise 10–15 digits.
    ''' Returns empty string if valid; otherwise an Arabic error message.
    ''' </summary>
    Public Function ValidateWhatsAppNumberArabic(phone As String) As String
        Dim s = If(phone, "").Trim()
        If String.IsNullOrWhiteSpace(s) Then
            Return "أدخل رقم واتساب/الجوال (أرقام فقط)."
        End If
        Dim digits As String = NormalizeWhatsDigits(s)
        If String.IsNullOrWhiteSpace(digits) Then
            Return "أدخل رقم واتساب/الجوال (أرقام فقط)."
        End If
        If s.Any(Function(c) Not Char.IsDigit(c) AndAlso c <> " "c AndAlso c <> "-"c AndAlso c <> "+"c) Then
            Return "يجب أن يحتوي الرقم على أرقام فقط (بدون مسافات أو شرطات أو +)."
        End If
        If digits.StartsWith("970") OrElse digits.StartsWith("972") Then
            If digits.Length <> 12 Then
                Return "للرمز 970 أو 972 يجب أن يكون الرقم 12 رقمًا (رمز 3 + 9 أرقام بدون صفر في البداية). الحالي: " & digits.Length & "."
            End If
            Return ""
        End If
        If digits.Length < 10 OrElse digits.Length > 15 Then
            Return "يجب أن يكون الرقم بين 10 و 15 رقمًا (مثلاً 970599123456). الحالي: " & digits.Length & "."
        End If
        Return ""
    End Function

    ''' <summary>
    ''' Shared validation for WhatsApp/phone numbers with English messages
    ''' (same rules as ValidateWhatsAppNumberArabic).
    ''' </summary>
    Public Function ValidateWhatsAppNumberEnglish(phone As String) As String
        Dim s = If(phone, "").Trim()
        If String.IsNullOrWhiteSpace(s) Then
            Return "Enter WhatsApp/phone number (digits only)."
        End If
        Dim digits As String = NormalizeWhatsDigits(s)
        If String.IsNullOrWhiteSpace(digits) Then
            Return "Enter WhatsApp/phone number (digits only)."
        End If
        If s.Any(Function(c) Not Char.IsDigit(c) AndAlso c <> " "c AndAlso c <> "-"c AndAlso c <> "+"c) Then
            Return "Number must contain only digits (no spaces, dashes or plus sign)."
        End If
        If digits.StartsWith("970") OrElse digits.StartsWith("972") Then
            If digits.Length <> 12 Then
                Return "For prefix 970 or 972 the number must be 12 digits (3 prefix + 9 digits without leading 0). Current: " & digits.Length & "."
            End If
            Return ""
        End If
        If digits.Length < 10 OrElse digits.Length > 15 Then
            Return "Number must be 10–15 digits (e.g. 970599123456). Current: " & digits.Length & "."
        End If
        Return ""
    End Function

    ''' <summary>
    ''' Localized validation for WhatsApp numbers based on global Eng flag:
    ''' English when Eng=True, Arabic when Eng=False.
    ''' </summary>
    Public Function ValidateWhatsAppNumberLocalized(phone As String) As String
        If Eng Then
            Return ValidateWhatsAppNumberEnglish(phone)
        Else
            Return ValidateWhatsAppNumberArabic(phone)
        End If
    End Function

    ''' <summary>Shared top block: clinic line (if available), single title line, then salutation + patient name + blank line.</summary>
    Private Sub AppendWhatsStandardHeader(sb As StringBuilder, titleLine As String, patientName As String, patientSex As String, useEng As Boolean, Optional clinic As Clinic = Nothing)
        If clinic IsNot Nothing Then
            sb.AppendLine(If(useEng, $"{clinic.ClinicNameEn}", $"{clinic.ClinicNameAr}"))
        End If
        sb.AppendLine(titleLine)
        Dim sex = FormatPatientSexSalutation(If(patientSex, ""), useEng)
        sb.AppendLine(If(useEng, $"{sex}: ", $"{sex}: ") & If(patientName, ""))
        sb.AppendLine()
    End Sub

    Private Function ApplyArabicRtlWrapIfNeeded(trimmedBody As String, useEng As Boolean) As String
        If useEng Then Return trimmedBody
        Const rle As Char = ChrW(&H202B)
        Const pdf As Char = ChrW(&H202C)
        Return rle & trimmedBody & vbLf & pdf
    End Function

    ''' <summary>
    ''' True when message text should use RTL layout in WinForms (matches Arabic templates: RLE U+202B from
    ''' <see cref="ApplyArabicRtlWrapIfNeeded"/>, or dominant Arabic script vs Latin letters).
    ''' </summary>
    Public Function MessageBodyShouldUseRtl(message As String) As Boolean
        If String.IsNullOrWhiteSpace(message) Then Return False
        If message.IndexOf(ChrW(&H202B)) >= 0 Then Return True
        Dim arabic = 0
        Dim latin = 0
        For Each ch In message
            Dim c = AscW(ch)
            If (c >= &H600 AndAlso c <= &H6FF) OrElse (c >= &H750 AndAlso c <= &H77F) OrElse
                (c >= &HFB50 AndAlso c <= &HFDFF) OrElse (c >= &HFE70 AndAlso c <= &HFEFF) Then
                arabic += 1
            ElseIf (c >= AscW("A"c) AndAlso c <= AscW("Z"c)) OrElse (c >= AscW("a"c) AndAlso c <= AscW("z"c)) Then
                latin += 1
            End If
        Next
        If arabic = 0 Then Return False
        Return arabic >= latin
    End Function

    ''' <summary>
    ''' Standard WhatsApp prefix for patient-facing texts: clinic (if DB has one), one title line, salutation + name, blank line, then <paramref name="appendRest"/>.
    ''' Arabic bodies get the same RLE/PDF wrapper as appointments and accounting.
    ''' </summary>
    Public Function BuildWhatsMessageWithStandardHeader(titleLine As String,
                                                        patientName As String,
                                                        patientSex As String,
                                                        useEng As Boolean,
                                                        appendRest As Action(Of StringBuilder)) As String
        Dim sb As New StringBuilder()
        AppendWhatsStandardHeader(sb, titleLine, patientName, patientSex, useEng, GetFirstClinicOrNothing())
        If appendRest IsNot Nothing Then appendRest(sb)
        Return ApplyArabicRtlWrapIfNeeded(sb.ToString().Trim(), useEng)
    End Function

    Private Function FormatEnglishAppointmentClock12h(t As DateTime) As String
        Return t.ToString("h:mm tt", CultureInfo.InvariantCulture)
    End Function

    ''' <summary>
    ''' Builds an appointment WhatsApp message (English or Arabic) given raw fields.
    ''' Header matches accounting: clinic, title line (overridable for 24h/2h reminder wording), salutation + patient name.
    ''' </summary>
    Public Function BuildAppointmentWhatsAppMessage(patientName As String,
                                                    drName As String,
                                                    appDate As DateTime,
                                                    startTime As DateTime?,
                                                    endTime As DateTime?,
                                                    reason As String,
                                                    notes As String,
                                                    status As String,
                                                    useEng As Boolean,
                                                    Optional patientSex As String = Nothing,
                                                    Optional titleEnglish As String = Nothing,
                                                    Optional titleArabic As String = Nothing,
                                                    Optional includeReason As Boolean = True,
                                                    Optional includeNotes As Boolean = True) As String
        Dim titleLine = If(useEng,
            If(String.IsNullOrWhiteSpace(titleEnglish), "Appointment reminder", titleEnglish),
            If(String.IsNullOrWhiteSpace(titleArabic), "تذكير بموعد", titleArabic))
        Return BuildWhatsMessageWithStandardHeader(titleLine, patientName, patientSex, useEng,
            Sub(sb)
                If Not useEng Then
                    sb.AppendLine("الطبيب: " & If(drName, ""))
                    sb.AppendLine("اليوم: " & FormatWhatsAppAppointmentWeekdayOnly(appDate, False))
                    'sb.AppendLine()
                    sb.AppendLine("التاريخ: " & FormatWhatsAppAppointmentDateNumbersOnly(appDate, False))
                    If startTime.HasValue Then sb.AppendLine("من الساعة: " & FormatArabicAppointmentClockWithPeriod(startTime.Value))
                    If endTime.HasValue Then sb.AppendLine("إلى الساعة: " & FormatArabicAppointmentClockWithPeriod(endTime.Value))
                    If includeReason AndAlso Not String.IsNullOrWhiteSpace(reason) Then sb.AppendLine("سبب الزيارة: " & reason)
                    If includeNotes AndAlso Not String.IsNullOrWhiteSpace(notes) Then sb.AppendLine("ملاحظات: " & notes)
                    If Not String.IsNullOrWhiteSpace(status) Then sb.AppendLine("حالة الموعد: " & status)
                    sb.AppendLine()
                    sb.AppendLine("نرجو تأكيد حضورك أو التواصل مع العيادة في حال الرغبة بتغيير الموعد.")
                Else
                    sb.AppendLine("Doctor: " & If(drName, ""))
                    sb.AppendLine("Day: " & FormatWhatsAppAppointmentWeekdayOnly(appDate, True))
                    'sb.AppendLine()  "C:\Program Files (x86)\Carestream\Patient Browser\Patient.exe" "C:\Program Files (x86)\Carestream\Patient Browser"
                    sb.AppendLine("Date: " & FormatWhatsAppAppointmentDateNumbersOnly(appDate, True))
                    If startTime.HasValue Then sb.AppendLine("From: " & FormatEnglishAppointmentClock12h(startTime.Value))
                    If endTime.HasValue Then sb.AppendLine("To: " & FormatEnglishAppointmentClock12h(endTime.Value))
                    If includeReason AndAlso Not String.IsNullOrWhiteSpace(reason) Then sb.AppendLine("Reason: " & reason)
                    If includeNotes AndAlso Not String.IsNullOrWhiteSpace(notes) Then sb.AppendLine("Notes: " & notes)
                    If Not String.IsNullOrWhiteSpace(status) Then sb.AppendLine("Status: " & status)
                    sb.AppendLine()
                    sb.AppendLine("Please confirm your attendance or let us know if you need to reschedule.")
                End If
            End Sub)
    End Function

    ''' <summary>
    ''' Builds an accounting WhatsApp message (English or Arabic) from treatments and payments.
    ''' Same format as NewAccounting BuildAccountingWhatsAppMessage: clinic header, salutation, numbered treatments (optional full list),
    ''' Discount/Discount2, localized pay types in Arabic, cheque details, totals and balance (payments − net treatments).
    ''' </summary>
    Public Function BuildAccountingWhatsAppMessage(patientName As String,
                                                   treatments As IEnumerable(Of Patient_Trts),
                                                   payments As IEnumerable(Of Patient_Pays),
                                                   excludeZeroValueTreatments As Boolean,
                                                   useArabic As Boolean,
                                                   Optional fullBody As Boolean = True,
                                                   Optional patientSex As String = Nothing) As String
        Dim firstClinic As Clinic = GetFirstClinicOrNothing()
        If firstClinic Is Nothing Then Return Nothing

        Dim trts = If(treatments, Enumerable.Empty(Of Patient_Trts)()).ToList()
        If excludeZeroValueTreatments Then
            trts = trts.Where(Function(t) t.TrtValue > 0D).ToList()
        End If

        Dim pays = If(payments, Enumerable.Empty(Of Patient_Pays)()).OrderBy(Function(p) p.PayDate).ToList()
        If Not trts.Any() AndAlso Not pays.Any() Then Return ""

        Dim useEng As Boolean = Not useArabic
        Dim titleLine = If(useEng, "Account Summary", "ملخص حساب ")

        Dim sb As New StringBuilder()
        Dim sep = " — "
        AppendWhatsStandardHeader(sb, titleLine, patientName, patientSex, useEng, firstClinic)

        If fullBody Then
            If trts.Any() Then
                sb.AppendLine(If(useEng, "Treatments:", "العلاجات:"))
                Dim trtIdx = 1
                For Each t In trts
                    Dim detail = If(t.Detail, "")
                    Dim dt = FormatWhatsAppDateLongOrEmpty(t.TrtDate, useEng)
                    Dim val = t.TrtValue.ToString("N2")
                    Dim discVal = If(t.Discount.HasValue, t.Discount.Value, 0D)
                    Dim disc2Val = If(t.Discount2.HasValue, t.Discount2.Value, 0D)
                    Dim disc = If(discVal <> 0D, " " & If(useEng, "Disc:", "خصم:") & " " & discVal.ToString("N2"), "")
                    Dim disc2 = If(disc2Val <> 0D, " " & If(useEng, "Disc2:", "خصم 2:") & " " & disc2Val.ToString("N2"), "")
                    If discVal = 0D AndAlso disc2Val = 0D Then
                        sb.AppendLine($"  {trtIdx}. {detail}{sep}{dt}{sep}{If(useEng, "Value:", "المبلغ:")} {val}")
                    ElseIf discVal <> 0D AndAlso disc2Val = 0D Then
                        sb.AppendLine($"  {trtIdx}. {detail}{sep}{dt}{sep}{If(useEng, "Value:", "المبلغ:")} {val}{disc}")
                    ElseIf discVal = 0D AndAlso disc2Val <> 0D Then
                        sb.AppendLine($"  {trtIdx}. {detail}{sep}{dt}{sep}{If(useEng, "Value:", "المبلغ:")} {val}{disc2}")
                    Else
                        sb.AppendLine($"  {trtIdx}. {detail}{sep}{dt}{sep}{If(useEng, "Value:", "المبلغ:")} {val}{disc}{disc2}")
                    End If
                    trtIdx += 1
                Next
                sb.AppendLine()
            End If
        Else
            If trts.Any() Then
                sb.AppendLine()
            End If
        End If

        If pays.Any() Then
            sb.AppendLine(If(useEng, "Payments:", "الدفعات:"))
            Dim payIdx = 1
            For Each P In pays
                Dim pType = LocalizePayTypeForAccountingWhatsApp(If(P.PayType, ""), useArabic)
                Dim dt = FormatWhatsAppDateLongOrEmpty(P.PayDate, useEng)
                Dim val = P.PayValue.ToString("N2")
                sb.Append($"  {payIdx}. {pType}{sep}{dt}{sep}{If(useEng, "Value:", "المبلغ:")} {val}")
                If String.Equals(P.PayType, "Cheque", StringComparison.OrdinalIgnoreCase) Then
                    If Not String.IsNullOrWhiteSpace(P.ChqNumber) Then sb.Append($" {If(useEng, "Chq#:", "شيك رقم:")} {P.ChqNumber}")
                    If P.ChqDueDate.HasValue Then sb.Append($" {If(useEng, "Due:", "استحقاق:")} {FormatWhatsAppDateLong(P.ChqDueDate.Value, useEng)}")
                    If Not String.IsNullOrWhiteSpace(P.ChqBank) Then sb.Append($" {If(useEng, "Bank:", "البنك:")} {P.ChqBank}")
                    If Not String.IsNullOrWhiteSpace(P.ChqOwner) Then sb.Append($" {If(useEng, "Owner:", "صاحب الشيك:")} {P.ChqOwner}")
                End If
                sb.AppendLine()
                payIdx += 1
            Next
        End If

        sb.AppendLine()
        Dim totalDisc As Decimal = trts.Sum(Function(t) If(t.Discount.HasValue, t.Discount.Value, 0D))
        Dim totalDisc2 As Decimal = trts.Sum(Function(t) If(t.Discount2.HasValue, t.Discount2.Value, 0D))
        Dim totalTreats As Decimal = trts.Sum(Function(t) t.TrtValue - If(t.Discount.HasValue, t.Discount.Value, 0D) - If(t.Discount2.HasValue, t.Discount2.Value, 0D))
        Dim totalPays As Decimal = pays.Sum(Function(p) p.PayValue)
        Dim balance As Decimal = totalPays - totalTreats
        If useEng Then
            sb.AppendLine("Total treatments: " & totalTreats.ToString("N2"))
            sb.AppendLine("Total payments: " & totalPays.ToString("N2"))
            If totalDisc <> 0D Then
                sb.AppendLine("Total discount: " & totalDisc.ToString("N2"))
            End If
            If totalDisc2 <> 0D Then
                If totalDisc = 0D Then
                    sb.AppendLine("Total discount: " & totalDisc2.ToString("N2"))
                Else
                    sb.AppendLine("Total discount 2: " & totalDisc2.ToString("N2"))
                End If
            End If
            sb.AppendLine("Balance: " & balance.ToString("N2"))
        Else
            Dim rlm As Char = ChrW(&H200F)
            sb.AppendLine("إجمالي العلاجات: " & rlm & totalTreats.ToString("N2"))
            sb.AppendLine("إجمالي الدفعات: " & rlm & totalPays.ToString("N2"))
            If totalDisc <> 0D Then
                sb.AppendLine("إجمالي الخصم: " & totalDisc.ToString("N2"))
            End If
            If totalDisc2 <> 0D Then
                If totalDisc = 0D Then
                    sb.AppendLine("إجمالي الخصم: " & totalDisc2.ToString("N2"))
                Else
                    sb.AppendLine("إجمالي خصم 2: " & totalDisc2.ToString("N2"))
                End If
            End If
            sb.AppendLine("الرصيد: " & rlm & balance.ToString("N2"))
        End If

        Return ApplyArabicRtlWrapIfNeeded(sb.ToString().Trim(), useEng)
    End Function

    ''' <summary>Gathers payments from treatment rows, deduplicated by PayID, ordered by PayDate (same rules as NewAccounting).</summary>
    Public Function CollectDedupedOrderedPaysFromTreatments(trts As IEnumerable(Of Patient_Trts)) As List(Of Patient_Pays)
        Dim list = If(trts, Enumerable.Empty(Of Patient_Trts)()).ToList()
        Dim payIds As New HashSet(Of Integer)
        Dim pays As New List(Of Patient_Pays)
        For Each t In list
            If t.Patient_PaysIEnumerable IsNot Nothing Then
                For Each P In t.Patient_PaysIEnumerable
                    If P IsNot Nothing AndAlso Not payIds.Contains(P.PayID) Then
                        payIds.Add(P.PayID)
                        pays.Add(P)
                    End If
                Next
            End If
        Next
        Return pays.OrderBy(Function(p) p.PayDate).ToList()
    End Function

    Private Function GetFirstClinicOrNothing() As Clinic
        Try
            Dim clinicData As New ClinicDATA()
            Return clinicData.SelectAll().FirstOrDefault()
        Catch
            Return Nothing
        End Try
    End Function

    Private Function FormatPatientSexSalutation(sexRaw As String, useEng As Boolean) As String
        If String.IsNullOrWhiteSpace(sexRaw) Then
            Return If(useEng, "Mr\Mrs", "السيد/السيدة")
        End If
        If sexRaw = "Male" OrElse sexRaw = "ذكر" Then
            Return If(useEng, "Mr.", "السيد")
        End If
        If sexRaw = "Female" OrElse sexRaw = "انثى" OrElse sexRaw = "أنثى" Then
            Return If(useEng, "Ms.", "السيدة")
        End If
        Return ""
    End Function

    Private Function LocalizePayTypeForAccountingWhatsApp(payType As String, useArabic As Boolean) As String
        If Not useArabic OrElse String.IsNullOrWhiteSpace(payType) Then Return If(payType, "")
        Dim p = payType.Trim()
        If String.Equals(p, "Cash", StringComparison.OrdinalIgnoreCase) Then Return "نقدي"
        If String.Equals(p, "Cheque", StringComparison.OrdinalIgnoreCase) OrElse String.Equals(p, "Check", StringComparison.OrdinalIgnoreCase) Then Return "شيك"
        If String.Equals(p, "Insurance", StringComparison.OrdinalIgnoreCase) Then Return "تأمين"
        If String.Equals(p, "Other", StringComparison.OrdinalIgnoreCase) Then Return "أخرى"
        Return p
    End Function
    ''' <summary>Fills cboPrefix with country name and calling code. Palestine (970) and Israel (972) are first.</summary>
    Public Sub FillCboPrefixOnce(cboPrefix As ComboBoxEdit)
        If cboPrefix Is Nothing OrElse cboPrefix.Properties.Items.Count > 0 Then Return
        Dim prefixesEn As String() = {
            "Palestine (+970)", "Israel (+972)",
            "Algeria (+213)", "Egypt (+20)", "Saudi Arabia (+966)", "UAE (+971)", "Jordan (+962)", "Lebanon (+961)", "Syria (+963)", "Iraq (+964)", "Kuwait (+965)", "Bahrain (+973)", "Qatar (+974)", "Oman (+968)", "Yemen (+967)",
            "Morocco (+212)", "Tunisia (+216)", "Libya (+218)", "Gambia (+220)", "Senegal (+221)", "Mauritania (+222)", "Mali (+223)", "Guinea (+224)", "Côte d'Ivoire (+225)", "Burkina Faso (+226)", "Niger (+227)", "Togo (+228)", "Benin (+229)", "Mauritius (+230)", "Liberia (+231)", "Sierra Leone (+232)", "Ghana (+233)", "Nigeria (+234)", "Chad (+235)", "Central African Republic (+236)", "Cameroon (+237)", "Cape Verde (+238)", "São Tomé and Príncipe (+239)", "Equatorial Guinea (+240)", "Gabon (+241)", "Congo (+242)", "DR Congo (+243)", "Angola (+244)", "Guinea-Bissau (+245)", "British Indian Ocean (+246)", "Seychelles (+248)", "Sudan (+249)", "Ethiopia (+251)", "Somalia (+252)", "Djibouti (+253)", "Kenya (+254)", "Tanzania (+255)", "Uganda (+256)", "Burundi (+257)", "Mozambique (+258)", "Zambia (+260)", "Madagascar (+261)", "Réunion (+262)", "Zimbabwe (+263)", "Namibia (+264)", "Malawi (+265)", "Lesotho (+266)", "Botswana (+267)", "Swaziland (+268)", "Comoros (+269)", "South Africa (+27)", "Saint Helena (+290)", "Eritrea (+291)", "Aruba (+297)", "Faroe Islands (+298)", "Greenland (+299)",
            "Greece (+30)", "Netherlands (+31)", "Belgium (+32)", "France (+33)", "Spain (+34)", "Gibraltar (+350)", "Portugal (+351)", "Luxembourg (+352)", "Ireland (+353)", "Iceland (+354)", "Albania (+355)", "Malta (+356)", "Cyprus (+357)", "Finland (+358)", "Bulgaria (+359)", "Hungary (+36)", "Lithuania (+370)", "Latvia (+371)", "Estonia (+372)", "Moldova (+373)", "Armenia (+374)", "Belarus (+375)", "Andorra (+376)", "Monaco (+377)", "San Marino (+378)", "Vatican (+379)", "Ukraine (+380)", "Serbia (+381)", "Montenegro (+382)", "Kosovo (+383)", "Croatia (+385)", "Slovenia (+386)", "Bosnia and Herzegovina (+387)", "North Macedonia (+389)", "Italy (+39)", "Romania (+40)", "Switzerland (+41)", "Czech Republic (+420)", "Slovakia (+421)", "Liechtenstein (+423)", "Austria (+43)", "United Kingdom (+44)", "Denmark (+45)", "Sweden (+46)", "Norway (+47)", "Poland (+48)", "Germany (+49)",
            "Falkland Islands (+500)", "Belize (+501)", "Guatemala (+502)", "El Salvador (+503)", "Honduras (+504)", "Nicaragua (+505)", "Costa Rica (+506)", "Panama (+507)", "Saint-Pierre and Miquelon (+508)", "Haiti (+509)", "Peru (+51)", "Mexico (+52)", "Cuba (+53)", "Argentina (+54)", "Brazil (+55)", "Chile (+56)", "Colombia (+57)", "Venezuela (+58)", "Guadeloupe (+590)", "Bolivia (+591)", "Guyana (+592)", "Ecuador (+593)", "French Guiana (+594)", "Paraguay (+595)", "Martinique (+596)", "Suriname (+597)", "Uruguay (+598)", "Netherlands Antilles (+599)",
            "Malaysia (+60)", "Australia (+61)", "Indonesia (+62)", "Philippines (+63)", "New Zealand (+64)", "Singapore (+65)", "Thailand (+66)", "Christmas Island (+670)", "Norfolk Island (+672)", "Brunei (+673)", "Nauru (+674)", "Papua New Guinea (+675)", "Tonga (+676)", "Solomon Islands (+677)", "Vanuatu (+678)", "Fiji (+679)", "Palau (+680)", "Wallis and Futuna (+681)", "Cook Islands (+682)", "Niue (+683)", "Samoa (+685)", "Kiribati (+686)", "New Caledonia (+687)", "Tuvalu (+688)", "French Polynesia (+689)", "Tokelau (+690)", "Micronesia (+691)", "Marshall Islands (+692)",
            "Russia (+7)", "Japan (+81)", "South Korea (+82)", "Vietnam (+84)", "China (+86)", "Bangladesh (+880)", "Turkey (+90)", "India (+91)", "Pakistan (+92)", "Afghanistan (+93)", "Sri Lanka (+94)", "Myanmar (+95)", "Maldives (+960)", "Mongolia (+976)", "Iran (+98)", "Tajikistan (+992)", "Turkmenistan (+993)", "Azerbaijan (+994)", "Georgia (+995)", "Kyrgyzstan (+996)", "Uzbekistan (+998)",
            "USA/Canada (+1)"}

        Dim prefixesAr As String() = {
            "فلسطين (+970)", "إسرائيل (+972)",
            "الجزائر (+213)", "مصر (+20)", "السعودية (+966)", "الإمارات (+971)", "الأردن (+962)", "لبنان (+961)", "سوريا (+963)", "العراق (+964)", "الكويت (+965)", "البحرين (+973)", "قطر (+974)", "عُمان (+968)", "اليمن (+967)",
            "المغرب (+212)", "تونس (+216)", "ليبيا (+218)", "غامبيا (+220)", "السنغال (+221)", "موريتانيا (+222)", "مالي (+223)", "غينيا (+224)", "ساحل العاج (+225)", "بوركينا فاسو (+226)", "النيجر (+227)", "توغو (+228)", "بنين (+229)", "موريشيوس (+230)", "ليبيريا (+231)", "سيراليون (+232)", "غانا (+233)", "نيجيريا (+234)", "تشاد (+235)", "جمهورية أفريقيا الوسطى (+236)", "الكاميرون (+237)", "الرأس الأخضر (+238)", "ساو تومي وبرينسيبي (+239)", "غينيا الاستوائية (+240)", "الغابون (+241)", "الكونغو (+242)", "الكونغو الديموقراطية (+243)", "أنغولا (+244)", "غينيا بيساو (+245)", "المحيط الهندي البريطاني (+246)", "سيشل (+248)", "السودان (+249)", "إثيوبيا (+251)", "الصومال (+252)", "جيبوتي (+253)", "كينيا (+254)", "تنزانيا (+255)", "أوغندا (+256)", "بوروندي (+257)", "موزمبيق (+258)", "زامبيا (+260)", "مدغشقر (+261)", "ريونيون (+262)", "زيمبابوي (+263)", "ناميبيا (+264)", "مالاوي (+265)", "ليسوتو (+266)", "بوتسوانا (+267)", "سوازيلاند (+268)", "جزر القمر (+269)", "جنوب أفريقيا (+27)", "سانت هيلينا (+290)", "إريتريا (+291)", "أروبا (+297)", "جزر الفارو (+298)", "غرينلاند (+299)",
            "اليونان (+30)", "هولندا (+31)", "بلجيكا (+32)", "فرنسا (+33)", "إسبانيا (+34)", "جبل طارق (+350)", "البرتغال (+351)", "لوكسمبورغ (+352)", "إيرلندا (+353)", "آيسلندا (+354)", "ألبانيا (+355)", "مالطا (+356)", "قبرص (+357)", "فنلندا (+358)", "بلغاريا (+359)", "المجر (+36)", "ليتوانيا (+370)", "لاتفيا (+371)", "إستونيا (+372)", "مولدوفا (+373)", "أرمينيا (+374)", "بيلاروسيا (+375)", "أندورا (+376)", "موناكو (+377)", "سان مارينو (+378)", "الفاتيكان (+379)", "أوكرانيا (+380)", "صربيا (+381)", "مونتينيغرو (+382)", "كوسوفو (+383)", "كرواتيا (+385)", "سلوفينيا (+386)", "البوسنة والهرسك (+387)", "مقدونيا الشمالية (+389)", "إيطاليا (+39)", "رومانيا (+40)", "سويسرا (+41)", "جمهورية التشيك (+420)", "سلوفاكيا (+421)", "ليختنشتاين (+423)", "النمسا (+43)", "المملكة المتحدة (+44)", "الدانمارك (+45)", "السويد (+46)", "النرويج (+47)", "بولندا (+48)", "ألمانيا (+49)",
            "جزر فوكلاند (+500)", "بليز (+501)", "غواتيمالا (+502)", "السلفادور (+503)", "هندوراس (+504)", "نيكاراغوا (+505)", "كوستاريكا (+506)", "بنما (+507)", "سان بيير وميكلون (+508)", "هايتي (+509)", "بيرو (+51)", "المكسيك (+52)", "كوبا (+53)", "الأرجنتين (+54)", "البرازيل (+55)", "تشيلي (+56)", "كولومبيا (+57)", "فنزويلا (+58)", "غوادلوب (+590)", "بوليفيا (+591)", "غيانا (+592)", "الإكوادور (+593)", "غويانا الفرنسية (+594)", "باراغواي (+595)", "مارتينيك (+596)", "سورينام (+597)", "الأوروغواي (+598)", "جزر الأنتيل الهولندية (+599)",
            "ماليزيا (+60)", "أستراليا (+61)", "إندونيسيا (+62)", "الفلبين (+63)", "نيوزيلندا (+64)", "سنغافورة (+65)", "تايلاند (+66)", "جزر كريسماس (+670)", "جزيرة نورفولك (+672)", "بروناي (+673)", "ناورو (+674)", "بابوا غينيا الجديدة (+675)", "تونغا (+676)", "جزر سليمان (+677)", "فانواتو (+678)", "فيجي (+679)", "بالاو (+680)", "واليس وفوتونا (+681)", "جزر كوك (+682)", "نييوي (+683)", "ساموا (+685)", "كيريباتي (+686)", "كاليدونيا الجديدة (+687)", "توفالو (+688)", "بولينيزيا الفرنسية (+689)", "توكيلاو (+690)", "ميكرونيزيا (+691)", "جزر مارشال (+692)",
            "روسيا (+7)", "اليابان (+81)", "كوريا الجنوبية (+82)", "فيتنام (+84)", "الصين (+86)", "بنغلاديش (+880)", "تركيا (+90)", "الهند (+91)", "باكستان (+92)", "أفغانستان (+93)", "سريلانكا (+94)", "ميانمار (+95)", "المالديف (+960)", "منغوليا (+976)", "إيران (+98)", "طاجيكستان (+992)", "تركمانستان (+993)", "أذربيجان (+994)", "جورجيا (+995)", "قيرغيزستان (+996)", "أوزبكستان (+998)",
            "الولايات المتحدة/كندا (+1)"}

        Dim prefixes As String() = If(Eng, prefixesEn, prefixesAr)
        cboPrefix.Properties.Items.AddRange(prefixes)
        cboPrefix.SelectedIndex = 0
    End Sub

    ''' <summary>
    ''' Same rules as AppointCEditorForm.GetFullWhatsAppNumber: full international digits for API/send,
    ''' combining saved country prefix with local WhatsApp (or plain local digits).
    ''' </summary>
    Public Function BuildInternationalWhatsDigits(localRaw As String, prefixRaw As String) As String
        Dim number = If(localRaw, "").Trim()
        If String.IsNullOrWhiteSpace(number) Then Return ""
        Dim allDigits As String = NormalizeWhatsDigits(number)
        If number.StartsWith("+"c) OrElse (allDigits.Length > 10 AndAlso Not allDigits.StartsWith("0"c)) Then
            Return allDigits
        End If
        Dim prefixDigits As String = NormalizeWhatsDigits(If(prefixRaw, ""))
        If String.IsNullOrWhiteSpace(prefixDigits) Then Return allDigits
        Dim localDigits As String = allDigits
        If localDigits.Length = 10 AndAlso localDigits.StartsWith("0"c) Then
            localDigits = localDigits.Substring(1)
        End If
        If localDigits.Length = 0 Then Return ""
        Return prefixDigits & localDigits
    End Function

    ''' <summary>Uses Patient.WhatsApp, then Phone, with Patient.WhatsAppPrefix (digits or text).</summary>
    Public Function BuildInternationalWhatsDigitsFromPatient(whatsLocal As String, phoneFallback As String, whatsAppPrefix As String) As String
        Dim localRaw = If(whatsLocal, "").Trim()
        If String.IsNullOrWhiteSpace(localRaw) Then localRaw = If(phoneFallback, "").Trim()
        Return BuildInternationalWhatsDigits(localRaw, whatsAppPrefix)
    End Function

    ''' <summary>
    ''' Loads <see cref="Patient.WhatsApp"/> (fallback <see cref="Patient.Phone"/>) into txtWhats as 10 digits starting with 0,
    ''' and restores <see cref="Patient.WhatsAppPrefix"/> in the combo (exact saved text, else digits match for legacy).
    ''' If the DB holds a full international number, splits using stored prefix digits or longest matching combo prefix.
    ''' Call <see cref="FillCboPrefixOnce"/> first so items exist.
    ''' </summary>
    Public Sub BindPatientWhatsPrefixAndLocal(cboPrefix As ComboBoxEdit, txtWhats As TextEdit, p As Patient)
        If p Is Nothing Then Return
        If txtWhats IsNot Nothing Then txtWhats.Text = GetLocalWhatsDisplayForPatient(p, cboPrefix)
        If cboPrefix IsNot Nothing AndAlso cboPrefix.Properties.Items.Count > 0 Then
            SelectComboFromStoredPrefixColumn(cboPrefix, If(p.WhatsAppPrefix, ""))
        End If
    End Sub

    ' --- Generic Whats (Doctors, Contacts, Secretaries, Emp, Suppliers, etc.): same storage as Patient prefix + local ---

    ''' <summary>Fills prefix list if needed, then binds stored prefix line and normalized local digits.</summary>
    Public Sub BindGenericWhatsPrefixAndLocal(cboPrefix As ComboBoxEdit, txtWhats As TextEdit, whatsAppPrefixStored As String, whatsLocalStored As String)
        FillCboPrefixOnce(cboPrefix)
        If txtWhats IsNot Nothing Then
            txtWhats.Text = NormalizeLocalWhatsTenDigitsForStorage(If(whatsLocalStored, ""))
        End If
        SelectComboFromStoredPrefixColumn(cboPrefix, If(whatsAppPrefixStored, ""))
    End Sub

    ''' <summary>Full international digits for API/send from prefix combo + local Whats textbox.</summary>
    Public Sub RefreshFullWhatsDigitsLabel(cboPrefix As ComboBoxEdit, txtWhats As TextEdit, lblFull As LabelControl)
        If lblFull Is Nothing Then Return
        lblFull.Text = BuildInternationalWhatsDigitsFromControls(cboPrefix, txtWhats)
    End Sub

    ''' <summary>Blue full-number preview label (optional font).</summary>
    Public Sub ApplyFullWhatsLabelAppearance(lblFull As LabelControl, Optional uiFont As Font = Nothing)
        If lblFull Is Nothing Then Return
        If uiFont IsNot Nothing Then
            lblFull.Appearance.Font = uiFont
            lblFull.Appearance.Options.UseFont = True
        End If
        lblFull.Appearance.ForeColor = Color.Blue
        lblFull.Appearance.Options.UseForeColor = True
    End Sub

    ''' <summary>Reads persisted prefix line + normalized local from UI (for SetData on generic entities).</summary>
    Public Sub ReadGenericWhatsFromControls(cboPrefix As ComboBoxEdit, txtWhats As TextEdit, ByRef whatsAppPrefixOut As String, ByRef whatsLocalOut As String)
        whatsAppPrefixOut = GetPrefixTextForStorage(cboPrefix)
        whatsLocalOut = NormalizeLocalWhatsTenDigitsForStorage(If(txtWhats?.Text, "").ToString())
    End Sub

    Private Sub WhatsLocalTextEdit_KeyDownDigitsOnly(sender As Object, e As KeyEventArgs)
        If e.KeyCode = Keys.Back OrElse e.KeyCode = Keys.Delete OrElse
           e.KeyCode = Keys.Left OrElse e.KeyCode = Keys.Right OrElse
           e.KeyCode = Keys.Up OrElse e.KeyCode = Keys.Down OrElse
           e.KeyCode = Keys.Tab OrElse e.KeyCode = Keys.Home OrElse e.KeyCode = Keys.End Then
            Return
        End If
        Dim topRow = (e.KeyCode >= Keys.D0 AndAlso e.KeyCode <= Keys.D9)
        Dim numPad = (e.KeyCode >= Keys.NumPad0 AndAlso e.KeyCode <= Keys.NumPad9)
        If (topRow OrElse numPad) AndAlso Not e.Shift Then Return
        e.SuppressKeyPress = True
        e.Handled = True
    End Sub

    ''' <summary>Restricts local Whats textbox to digits and navigation keys (idempotent per control).</summary>
    Public Sub AttachWhatsLocalDigitsOnlyKeyDown(txtWhats As TextEdit)
        If txtWhats Is Nothing Then Return
        RemoveHandler txtWhats.KeyDown, AddressOf WhatsLocalTextEdit_KeyDownDigitsOnly
        AddHandler txtWhats.KeyDown, AddressOf WhatsLocalTextEdit_KeyDownDigitsOnly
    End Sub

End Module

