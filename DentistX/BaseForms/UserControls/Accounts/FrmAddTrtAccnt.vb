Public Class FrmAddTrtAccnt
    Private _treatment As Patient_Trts
    Private _originalDetail As String
    Private _patientIdForAdd As Integer
    Private _hostCboPrefix As DevExpress.XtraEditors.ComboBoxEdit
    Private _hostTxtWhats As DevExpress.XtraEditors.TextEdit

    ''' <summary>Call before ShowDialog so Load can copy country prefix + local Whats from the accounting panel (same as parent display).</summary>
    Public Sub SetWhatsFromHost(hostCboPrefix As DevExpress.XtraEditors.ComboBoxEdit, hostTxtWhats As DevExpress.XtraEditors.TextEdit)
        _hostCboPrefix = hostCboPrefix
        _hostTxtWhats = hostTxtWhats
    End Sub

    Private Sub FrmAddTrtAccnt_Load(sender As Object, e As EventArgs) Handles Me.Load
        FillCboPrefixOnce()
        ApplyWhatsFromHostIfSet()
        IntegerMoneyEditorFocus.ConfigureIntegerMoneyTextEdit(TrtValue)
        IntegerMoneyEditorFocus.ConfigureIntegerMoneyTextEdit(TrtDiscount)
        IntegerMoneyEditorFocus.ConfigureIntegerMoneyTextEdit(TrtSecDiscount)
        IntegerMoneyEditorFocus.AttachTextEditZeroEmptyElseSelectAll(TrtValue, TrtDiscount, TrtSecDiscount)
    End Sub

    Private Sub ApplyWhatsFromHostIfSet()
        Try
            If _hostCboPrefix IsNot Nothing AndAlso cboPrefix IsNot Nothing AndAlso _hostCboPrefix.EditValue IsNot Nothing Then
                Dim hostItem As String = _hostCboPrefix.EditValue.ToString()
                cboPrefix.EditValue = hostItem
                If cboPrefix.SelectedIndex < 0 AndAlso cboPrefix.Properties.Items.Count > 0 Then
                    Dim hostDigits As String = WhatsHelper.GetPrefixDigitsFromCombo(_hostCboPrefix)
                    For i As Integer = 0 To cboPrefix.Properties.Items.Count - 1
                        Dim it As String = If(cboPrefix.Properties.Items(i)?.ToString(), "")
                        If WhatsHelper.NormalizeWhatsDigits(it) = hostDigits Then
                            cboPrefix.SelectedIndex = i
                            Exit For
                        End If
                    Next
                End If
            End If
            If _hostTxtWhats IsNot Nothing AndAlso txtWhats IsNot Nothing Then
                txtWhats.Text = If(_hostTxtWhats.Text, "").ToString()
            End If
        Finally
            _hostCboPrefix = Nothing
            _hostTxtWhats = Nothing
        End Try
        RefreshLblWhats()
    End Sub



    ''' <summary>True when form was opened for add and user clicked Add; caller should add NewTreatment.</summary>
    Public ReadOnly Property IsNewAdd As Boolean
        Get
            Return _isNewAdd
        End Get
    End Property
    Private _isNewAdd As Boolean

    ''' <summary>When form was opened for add and user clicked Add, this is the new treatment to insert.</summary>
    Public ReadOnly Property NewTreatment As Patient_Trts
        Get
            Return _newTreatment
        End Get
    End Property
    Private _newTreatment As Patient_Trts

    ''' <summary>Load form for adding a new treatment. Call before ShowDialog. Show Add button, hide Save.</summary>
    Public Sub LoadForAdd(patientId As Integer)
        _treatment = Nothing
        _patientIdForAdd = patientId
        _isNewAdd = False
        _newTreatment = Nothing
        DetailText.Text = ""
        TrtDate.EditValue = Date.Today
        TrtValue.EditValue = 0D
        TrtValue.Enabled = True
        TrtDiscount.EditValue = 0D
        TrtDiscount.Enabled = True
        chkSecDiscount.Visible = False
        lblSecDisc.Visible = False
        TrtSecDiscount.Visible = False
        btnAddTrt.Visible = True
        btnSave.Visible = False
        btnAddTrt.Text = If(Eng, "Add", "إضافة")
        btnCancel.Text = If(Eng, "Cancel", "إلغاء")
        Me.Text = If(Eng, "Add New Treat", "إضافة معالجة جديدة")
    End Sub

    ''' <summary>Load the treatment record into the form for editing. Call before ShowDialog. Show Save button, hide Add.</summary>
    Public Sub LoadTreatment(trt As Patient_Trts)
        If trt Is Nothing Then Return
        _treatment = trt
        _isNewAdd = False
        btnAddTrt.Visible = False
        btnSave.Visible = True
        btnSave.Text = If(Eng, "Save", "حفظ")
        btnCancel.Text = If(Eng, "Cancel", "إلغاء")
        Me.Text = If(Eng, "Edit Treat", "تعديل المعالجة")
        _originalDetail = If(trt.Detail, "")
        DetailText.Text = _originalDetail
        TrtDate.EditValue = trt.TrtDate
        TrtValue.EditValue = trt.TrtValue
        TrtValue.Enabled = True
        TrtDiscount.EditValue = If(trt.Discount.HasValue, trt.Discount.Value, 0D)
        TrtDiscount.Enabled = True
        chkSecDiscount.Checked = False
        _newTreatment = Nothing
        ' If passed treat has a discount, show second-discount controls and load Discount2; else keep invisible
        Dim hasDiscount As Boolean = trt.Discount.HasValue AndAlso trt.Discount.Value <> 0D
        lblSecDisc.Visible = hasDiscount
        TrtSecDiscount.Visible = hasDiscount
        chkSecDiscount.Visible = hasDiscount
        If hasDiscount Then
            TrtSecDiscount.EditValue = If(trt.Discount2.HasValue, trt.Discount2.Value, 0D)
            lblSecDisc.Text = If(Eng, "Second Discount:", "الخصم الثانوي:")
            chkSecDiscount.Properties.Caption = If(Eng, "Add Second Discount", "إضافة خصم ثانٍ")
        End If
    End Sub

    ''' <summary>After ShowDialog = Ok, the same Patient_Trts instance has been updated with form values (including Discount2 when second discount was set).</summary>
    Public ReadOnly Property EditedTreatment As Patient_Trts
        Get
            Return _treatment
        End Get
    End Property

    ''' <summary>If user changed country prefix or Whats number vs. database, update Patient.WhatsApp and Patient.WhatsAppPrefix.</summary>
    Private Sub PersistPatientWhatsIfChanged()
        Dim patientId As Integer = 0
        If _treatment IsNot Nothing AndAlso _treatment.PatientID > 0 Then
            patientId = _treatment.PatientID
        ElseIf _patientIdForAdd > 0 Then
            patientId = _patientIdForAdd
        End If
        If patientId <= 0 Then Return
        If Not WhatsHelper.HasPatientWhatsChangedVsDatabase(patientId, cboPrefix, txtWhats) Then Return
        WhatsHelper.PersistPatientWhatsNormalized(patientId, cboPrefix, txtWhats)
    End Sub

    Private Sub chkSecDiscount_CheckedChanged(sender As Object, e As EventArgs) Handles chkSecDiscount.CheckedChanged
        If _treatment Is Nothing Then Return
        If chkSecDiscount.Checked Then
            lblSecDisc.Visible = True
            TrtSecDiscount.Visible = True
            DetailText.Text = If(Eng, "Second Discount For ", "خصم ثانٍ لـ ") & _originalDetail
            TrtDate.EditValue = Date.Today
            TrtValue.EditValue = 0D
            TrtValue.Enabled = False
            TrtDiscount.EditValue = 0D
            TrtDiscount.Enabled = False
        Else
            DetailText.Text = _originalDetail
            TrtDate.EditValue = _treatment.TrtDate
            TrtValue.EditValue = _treatment.TrtValue
            TrtValue.Enabled = True
            TrtDiscount.EditValue = If(_treatment.Discount.HasValue, _treatment.Discount.Value, 0D)
            TrtDiscount.Enabled = True
        End If
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If _treatment Is Nothing Then
            Me.DialogResult = DialogResult.None
            Return
        End If
        PersistPatientWhatsIfChanged()
        If chkSecDiscount.Checked Then
            ' Only update Discount2 on the same record (no new row)
            _treatment.Discount2 = IntegerMoneyEditorFocus.DecimalFromIntegerMoneyEdit(TrtSecDiscount)
            Me.DialogResult = DialogResult.OK
            Me.Close()
            Return
        End If
        _treatment.Detail = DetailText.Text.Trim()
        _treatment.TrtDate = CType(TrtDate.EditValue, DateTime)
        _treatment.TrtValue = IntegerMoneyEditorFocus.DecimalFromIntegerMoneyEdit(TrtValue)
        _treatment.Discount = IntegerMoneyEditorFocus.DecimalFromIntegerMoneyEdit(TrtDiscount)
        Me.DialogResult = DialogResult.OK
        Me.Close()
    End Sub

    Private Sub btnAddTrt_Click(sender As Object, e As EventArgs) Handles btnAddTrt.Click
        If String.IsNullOrWhiteSpace(DetailText.Text) Then
            MsgBox(If(Eng, "Detail is required.", "التفاصيل مطلوبة."))
            Return
        End If
        PersistPatientWhatsIfChanged()
        _newTreatment = New Patient_Trts With {
            .PatientID = _patientIdForAdd,
            .Detail = DetailText.Text.Trim(),
            .TrtDate = CType(TrtDate.EditValue, DateTime),
            .TrtValue = IntegerMoneyEditorFocus.DecimalFromIntegerMoneyEdit(TrtValue),
            .Discount = IntegerMoneyEditorFocus.DecimalFromIntegerMoneyEdit(TrtDiscount),
            .Discount2 = Nothing
        }
        _isNewAdd = True
        Me.DialogResult = DialogResult.OK
        Me.Close()
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.DialogResult = DialogResult.Cancel
        Me.Close()
    End Sub


#Region "Whats"
    Private Sub RefreshLblWhats()
        If lblWhats IsNot Nothing Then
            lblWhats.Text = WhatsHelper.BuildInternationalWhatsDigitsFromControls(cboPrefix, txtWhats)
        End If
    End Sub

    Private Sub FillCboPrefixOnce()
        WhatsHelper.FillCboPrefixOnce(cboPrefix)
    End Sub

    Private Sub cboPrefix_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboPrefix.SelectedIndexChanged
        RefreshLblWhats()
    End Sub

    Private Sub txtWhats_ValueChanged(sender As Object, e As EventArgs) Handles txtWhats.EditValueChanged
        RefreshLblWhats()
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

    ''' <summary>
    ''' Validates full number: with prefix, expects prefix digits + 9 local (after removing leading 0 from Whats field). If no prefix, 10–15 digit international.
    ''' </summary>
    Private Function ValidateWhatsAppNumber(fullNumberDigits As String, prefixDigits As String) As String
        If String.IsNullOrWhiteSpace(fullNumberDigits) Then
            Return If(Eng, "Enter WhatsApp/phone number (digits only).", "أدخل رقم واتساب/الجوال (أرقام فقط).")
        End If
        If fullNumberDigits.Any(Function(c) Not Char.IsDigit(c)) Then
            Return If(Eng, "Number must contain only digits (no spaces, dashes or plus sign).", "يجب أن يحتوي الرقم على أرقام فقط (بدون مسافات أو شرطات أو +).")
        End If

        If String.IsNullOrWhiteSpace(prefixDigits) Then
            If fullNumberDigits.Length < 10 OrElse fullNumberDigits.Length > 15 Then
                Return If(Eng, "Number must be 10–15 digits (e.g. 970599123456 for Palestine).", "يجب أن يكون الرقم 10–15 رقمًا (مثلاً 970599123456 لفلسطين).")
            End If
            Return ""
        End If

        Dim prefixLen As Integer = prefixDigits.Length
        Dim expectedLen As Integer = prefixLen + 9
        If fullNumberDigits.Length <> expectedLen Then
            Dim msgEn As String = $"Invalid length. For prefix +{prefixDigits} use {prefixLen} + 9 digits (drop leading 0 from Whats) = {expectedLen}. Current: {fullNumberDigits.Length}."
            Dim msgAr As String = $"طول غير صحيح. لرمز +{prefixDigits} استخدم {prefixLen} + 9 أرقام (بدون صفر أولي في حقل الرقم) = {expectedLen}. الحالي: {fullNumberDigits.Length}."
            Return If(Eng, msgEn, msgAr)
        End If
        Return ""
    End Function

#End Region





    '#Region "Whats"
    '    ''' <summary>Extracts the 9 local digits from a full international number for display in txtWhats (no prefix).</summary>
    '    Private Shared Function FullNumberToLocal9Digits(fullNumber As String) As String
    '        If String.IsNullOrWhiteSpace(fullNumber) Then Return ""
    '        Dim digits As String = New String(fullNumber.Where(Function(c) Char.IsDigit(c)).ToArray())
    '        If String.IsNullOrWhiteSpace(digits) Then Return ""
    '        If digits.Length = 9 Then Return digits
    '        If digits.Length = 10 AndAlso digits.StartsWith("0"c) Then Return digits.Substring(1, 9)
    '        If digits.Length >= 12 AndAlso (digits.StartsWith("970") OrElse digits.StartsWith("972")) Then Return digits.Substring(3, 9)
    '        If digits.Length > 9 Then Return digits.Substring(digits.Length - 9, 9)
    '        Return digits
    '    End Function

    '    ''' <summary>Builds full WhatsApp number: prefix from cboPrefix + 9 local digits from txtWhats. lblWhats shows this.</summary>
    '    Private Function GetFullWhatsNumber() As String
    '        Dim number As String = ""
    '        If txtWhats IsNot Nothing AndAlso txtWhats.Text IsNot Nothing Then
    '            number = txtWhats.Text.ToString().Trim()
    '        End If
    '        Dim localDigits As String = New String(number.Where(Function(ch) Char.IsDigit(ch)).ToArray())
    '        If localDigits.Length = 10 AndAlso localDigits.StartsWith("0"c) Then
    '            localDigits = localDigits.Substring(1)
    '        End If
    '        If localDigits.Length > 9 Then localDigits = localDigits.Substring(0, 9)
    '        If cboPrefix Is Nothing OrElse cboPrefix.EditValue Is Nothing Then
    '            Return localDigits
    '        End If
    '        Dim rawPrefix As String = cboPrefix.EditValue.ToString()
    '        Dim prefixDigits As String = New String(rawPrefix.Where(Function(ch) Char.IsDigit(ch)).ToArray())
    '        If String.IsNullOrWhiteSpace(prefixDigits) Then Return localDigits
    '        If localDigits.Length = 0 Then Return ""
    '        Return prefixDigits & localDigits
    '    End Function

    '    ''' <summary>Updates lblWhats with the full WhatsApp number (prefix + txtWhats). Call after load or when prefix/number changes.</summary>
    '    Private Sub RefreshLblWhats()
    '        If lblWhats IsNot Nothing Then
    '            lblWhats.Text = GetFullWhatsNumber()
    '        End If
    '    End Sub

    '    ''' <summary>Fills cboPrefix with country name and calling code. Palestine (970) and Israel (972) are first.</summary>
    '    Private Sub FillCboPrefixOnce()
    '        If cboPrefix Is Nothing OrElse cboPrefix.Properties.Items.Count > 0 Then Return
    '        Dim prefixesEn As String() = {
    '            "Palestine (+970)", "Israel (+972)",
    '            "Algeria (+213)", "Egypt (+20)", "Saudi Arabia (+966)", "UAE (+971)", "Jordan (+962)", "Lebanon (+961)", "Syria (+963)", "Iraq (+964)", "Kuwait (+965)", "Bahrain (+973)", "Qatar (+974)", "Oman (+968)", "Yemen (+967)",
    '            "Morocco (+212)", "Tunisia (+216)", "Libya (+218)", "Gambia (+220)", "Senegal (+221)", "Mauritania (+222)", "Mali (+223)", "Guinea (+224)", "Côte d'Ivoire (+225)", "Burkina Faso (+226)", "Niger (+227)", "Togo (+228)", "Benin (+229)", "Mauritius (+230)", "Liberia (+231)", "Sierra Leone (+232)", "Ghana (+233)", "Nigeria (+234)", "Chad (+235)", "Central African Republic (+236)", "Cameroon (+237)", "Cape Verde (+238)", "São Tomé and Príncipe (+239)", "Equatorial Guinea (+240)", "Gabon (+241)", "Congo (+242)", "DR Congo (+243)", "Angola (+244)", "Guinea-Bissau (+245)", "British Indian Ocean (+246)", "Seychelles (+248)", "Sudan (+249)", "Ethiopia (+251)", "Somalia (+252)", "Djibouti (+253)", "Kenya (+254)", "Tanzania (+255)", "Uganda (+256)", "Burundi (+257)", "Mozambique (+258)", "Zambia (+260)", "Madagascar (+261)", "Réunion (+262)", "Zimbabwe (+263)", "Namibia (+264)", "Malawi (+265)", "Lesotho (+266)", "Botswana (+267)", "Swaziland (+268)", "Comoros (+269)", "South Africa (+27)", "Saint Helena (+290)", "Eritrea (+291)", "Aruba (+297)", "Faroe Islands (+298)", "Greenland (+299)",
    '            "Greece (+30)", "Netherlands (+31)", "Belgium (+32)", "France (+33)", "Spain (+34)", "Gibraltar (+350)", "Portugal (+351)", "Luxembourg (+352)", "Ireland (+353)", "Iceland (+354)", "Albania (+355)", "Malta (+356)", "Cyprus (+357)", "Finland (+358)", "Bulgaria (+359)", "Hungary (+36)", "Lithuania (+370)", "Latvia (+371)", "Estonia (+372)", "Moldova (+373)", "Armenia (+374)", "Belarus (+375)", "Andorra (+376)", "Monaco (+377)", "San Marino (+378)", "Vatican (+379)", "Ukraine (+380)", "Serbia (+381)", "Montenegro (+382)", "Kosovo (+383)", "Croatia (+385)", "Slovenia (+386)", "Bosnia and Herzegovina (+387)", "North Macedonia (+389)", "Italy (+39)", "Romania (+40)", "Switzerland (+41)", "Czech Republic (+420)", "Slovakia (+421)", "Liechtenstein (+423)", "Austria (+43)", "United Kingdom (+44)", "Denmark (+45)", "Sweden (+46)", "Norway (+47)", "Poland (+48)", "Germany (+49)",
    '            "Falkland Islands (+500)", "Belize (+501)", "Guatemala (+502)", "El Salvador (+503)", "Honduras (+504)", "Nicaragua (+505)", "Costa Rica (+506)", "Panama (+507)", "Saint-Pierre and Miquelon (+508)", "Haiti (+509)", "Peru (+51)", "Mexico (+52)", "Cuba (+53)", "Argentina (+54)", "Brazil (+55)", "Chile (+56)", "Colombia (+57)", "Venezuela (+58)", "Guadeloupe (+590)", "Bolivia (+591)", "Guyana (+592)", "Ecuador (+593)", "French Guiana (+594)", "Paraguay (+595)", "Martinique (+596)", "Suriname (+597)", "Uruguay (+598)", "Netherlands Antilles (+599)",
    '            "Malaysia (+60)", "Australia (+61)", "Indonesia (+62)", "Philippines (+63)", "New Zealand (+64)", "Singapore (+65)", "Thailand (+66)", "Christmas Island (+670)", "Norfolk Island (+672)", "Brunei (+673)", "Nauru (+674)", "Papua New Guinea (+675)", "Tonga (+676)", "Solomon Islands (+677)", "Vanuatu (+678)", "Fiji (+679)", "Palau (+680)", "Wallis and Futuna (+681)", "Cook Islands (+682)", "Niue (+683)", "Samoa (+685)", "Kiribati (+686)", "New Caledonia (+687)", "Tuvalu (+688)", "French Polynesia (+689)", "Tokelau (+690)", "Micronesia (+691)", "Marshall Islands (+692)",
    '            "Russia (+7)", "Japan (+81)", "South Korea (+82)", "Vietnam (+84)", "China (+86)", "Bangladesh (+880)", "Turkey (+90)", "India (+91)", "Pakistan (+92)", "Afghanistan (+93)", "Sri Lanka (+94)", "Myanmar (+95)", "Maldives (+960)", "Mongolia (+976)", "Iran (+98)", "Tajikistan (+992)", "Turkmenistan (+993)", "Azerbaijan (+994)", "Georgia (+995)", "Kyrgyzstan (+996)", "Uzbekistan (+998)",
    '            "USA/Canada (+1)"}

    '        Dim prefixesAr As String() = {
    '            "فلسطين (+970)", "إسرائيل (+972)",
    '            "الجزائر (+213)", "مصر (+20)", "السعودية (+966)", "الإمارات (+971)", "الأردن (+962)", "لبنان (+961)", "سوريا (+963)", "العراق (+964)", "الكويت (+965)", "البحرين (+973)", "قطر (+974)", "عُمان (+968)", "اليمن (+967)",
    '            "المغرب (+212)", "تونس (+216)", "ليبيا (+218)", "غامبيا (+220)", "السنغال (+221)", "موريتانيا (+222)", "مالي (+223)", "غينيا (+224)", "ساحل العاج (+225)", "بوركينا فاسو (+226)", "النيجر (+227)", "توغو (+228)", "بنين (+229)", "موريشيوس (+230)", "ليبيريا (+231)", "سيراليون (+232)", "غانا (+233)", "نيجيريا (+234)", "تشاد (+235)", "جمهورية أفريقيا الوسطى (+236)", "الكاميرون (+237)", "الرأس الأخضر (+238)", "ساو تومي وبرينسيبي (+239)", "غينيا الاستوائية (+240)", "الغابون (+241)", "الكونغو (+242)", "الكونغو الديموقراطية (+243)", "أنغولا (+244)", "غينيا بيساو (+245)", "المحيط الهندي البريطاني (+246)", "سيشل (+248)", "السودان (+249)", "إثيوبيا (+251)", "الصومال (+252)", "جيبوتي (+253)", "كينيا (+254)", "تنزانيا (+255)", "أوغندا (+256)", "بوروندي (+257)", "موزمبيق (+258)", "زامبيا (+260)", "مدغشقر (+261)", "ريونيون (+262)", "زيمبابوي (+263)", "ناميبيا (+264)", "مالاوي (+265)", "ليسوتو (+266)", "بوتسوانا (+267)", "سوازيلاند (+268)", "جزر القمر (+269)", "جنوب أفريقيا (+27)", "سانت هيلينا (+290)", "إريتريا (+291)", "أروبا (+297)", "جزر الفارو (+298)", "غرينلاند (+299)",
    '            "اليونان (+30)", "هولندا (+31)", "بلجيكا (+32)", "فرنسا (+33)", "إسبانيا (+34)", "جبل طارق (+350)", "البرتغال (+351)", "لوكسمبورغ (+352)", "إيرلندا (+353)", "آيسلندا (+354)", "ألبانيا (+355)", "مالطا (+356)", "قبرص (+357)", "فنلندا (+358)", "بلغاريا (+359)", "المجر (+36)", "ليتوانيا (+370)", "لاتفيا (+371)", "إستونيا (+372)", "مولدوفا (+373)", "أرمينيا (+374)", "بيلاروسيا (+375)", "أندورا (+376)", "موناكو (+377)", "سان مارينو (+378)", "الفاتيكان (+379)", "أوكرانيا (+380)", "صربيا (+381)", "مونتينيغرو (+382)", "كوسوفو (+383)", "كرواتيا (+385)", "سلوفينيا (+386)", "البوسنة والهرسك (+387)", "مقدونيا الشمالية (+389)", "إيطاليا (+39)", "رومانيا (+40)", "سويسرا (+41)", "جمهورية التشيك (+420)", "سلوفاكيا (+421)", "ليختنشتاين (+423)", "النمسا (+43)", "المملكة المتحدة (+44)", "الدانمارك (+45)", "السويد (+46)", "النرويج (+47)", "بولندا (+48)", "ألمانيا (+49)",
    '            "جزر فوكلاند (+500)", "بليز (+501)", "غواتيمالا (+502)", "السلفادور (+503)", "هندوراس (+504)", "نيكاراغوا (+505)", "كوستاريكا (+506)", "بنما (+507)", "سان بيير وميكلون (+508)", "هايتي (+509)", "بيرو (+51)", "المكسيك (+52)", "كوبا (+53)", "الأرجنتين (+54)", "البرازيل (+55)", "تشيلي (+56)", "كولومبيا (+57)", "فنزويلا (+58)", "غوادلوب (+590)", "بوليفيا (+591)", "غيانا (+592)", "الإكوادور (+593)", "غويانا الفرنسية (+594)", "باراغواي (+595)", "مارتينيك (+596)", "سورينام (+597)", "الأوروغواي (+598)", "جزر الأنتيل الهولندية (+599)",
    '            "ماليزيا (+60)", "أستراليا (+61)", "إندونيسيا (+62)", "الفلبين (+63)", "نيوزيلندا (+64)", "سنغافورة (+65)", "تايلاند (+66)", "جزر كريسماس (+670)", "جزيرة نورفولك (+672)", "بروناي (+673)", "ناورو (+674)", "بابوا غينيا الجديدة (+675)", "تونغا (+676)", "جزر سليمان (+677)", "فانواتو (+678)", "فيجي (+679)", "بالاو (+680)", "واليس وفوتونا (+681)", "جزر كوك (+682)", "نييوي (+683)", "ساموا (+685)", "كيريباتي (+686)", "كاليدونيا الجديدة (+687)", "توفالو (+688)", "بولينيزيا الفرنسية (+689)", "توكيلاو (+690)", "ميكرونيزيا (+691)", "جزر مارشال (+692)",
    '            "روسيا (+7)", "اليابان (+81)", "كوريا الجنوبية (+82)", "فيتنام (+84)", "الصين (+86)", "بنغلاديش (+880)", "تركيا (+90)", "الهند (+91)", "باكستان (+92)", "أفغانستان (+93)", "سريلانكا (+94)", "ميانمار (+95)", "المالديف (+960)", "منغوليا (+976)", "إيران (+98)", "طاجيكستان (+992)", "تركمانستان (+993)", "أذربيجان (+994)", "جورجيا (+995)", "قيرغيزستان (+996)", "أوزبكستان (+998)",
    '            "الولايات المتحدة/كندا (+1)"}

    '        Dim prefixes As String() = If(Eng, prefixesEn, prefixesAr)
    '        cboPrefix.Properties.Items.AddRange(prefixes)
    '        cboPrefix.SelectedIndex = 0
    '    End Sub

    '    Private Sub cboPrefix_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboPrefix.SelectedIndexChanged
    '        RefreshLblWhats()
    '    End Sub

    '    Private Sub txtWhats_ValueChanged(sender As Object, e As EventArgs) Handles txtWhats.EditValueChanged
    '        RefreshLblWhats()
    '    End Sub

    '    Private Sub txtWhats_KeyDown(sender As Object, e As KeyEventArgs) Handles txtWhats.KeyDown
    '        ' Allow control keys: backspace, delete, arrows, tab, home/end, etc.
    '        If e.KeyCode = Keys.Back OrElse
    '           e.KeyCode = Keys.Delete OrElse
    '           e.KeyCode = Keys.Left OrElse
    '           e.KeyCode = Keys.Right OrElse
    '           e.KeyCode = Keys.Up OrElse
    '           e.KeyCode = Keys.Down OrElse
    '           e.KeyCode = Keys.Tab OrElse
    '           e.KeyCode = Keys.Home OrElse
    '           e.KeyCode = Keys.End Then
    '            Return
    '        End If

    '        ' Allow digits only (top row or numpad)
    '        Dim isTopRowDigit As Boolean = (e.KeyCode >= Keys.D0 AndAlso e.KeyCode <= Keys.D9)
    '        Dim isNumPadDigit As Boolean = (e.KeyCode >= Keys.NumPad0 AndAlso e.KeyCode <= Keys.NumPad9)

    '        ' Block Shift-modified digits (to avoid !, @, etc.)
    '        If (isTopRowDigit OrElse isNumPadDigit) AndAlso (Not e.Shift) Then
    '            Return
    '        End If

    '        ' Otherwise block the key
    '        e.SuppressKeyPress = True
    '        e.Handled = True
    '    End Sub



    '#End Region


End Class