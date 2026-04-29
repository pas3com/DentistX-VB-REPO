Imports System.Data.SqlClient
Imports System.Drawing
Imports System.Windows.Forms
Imports Dapper
Imports DevExpress.XtraEditors.Controls
Imports DevExpress.XtraGrid.Views.Grid

Public Class AddNewDiagForm
    Private selectedTeethList As New List(Of Byte)
    Private selectedToothTrtList As New List(Of Patient_Diagnosis)
    Dim _shapeId As Integer = 0
    Dim _propName As String = ""
    Dim toothTrtData As New Patient_DiagnosisDATA
    Dim _toothTrt As New Patient_Diagnosis
    Dim external As Boolean = False
    Dim paid As Boolean = False
    Dim loaded As Boolean = False
    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()
        isPaidChck.Checked = False
        IsExternalchk.Checked = False
        loaded = False
        Me.Icon = AppIcon
    End Sub
    Public Sub New(ByVal clsToothTrt As List(Of Patient_Diagnosis), ByVal clsPatient As Patient)
        ' This call is required by the designer.
        InitializeComponent()
        Me.Icon = AppIcon
        loaded = False
        isPaidChck.Checked = False
        IsExternalchk.Checked = False
        selectedToothTrtList = clsToothTrt
        _toothTrt = clsToothTrt(0)
        txtPatientID.Text = clsToothTrt(0).PatientID
        txtPatientName.Text = clsPatient.PatientName
        txtToothNum.Text = clsToothTrt(0).ToothNum
        txtToothName.Text = GetToothFullName(clsToothTrt(0).ToothName)
        clrBorderColor.Color = ColorTranslator.FromHtml(clsToothTrt(0).BorderColor)
        tbThick.Value = 0
        txtTreat.Text = clsToothTrt(0).Treat
        clrFillColor.Color = GetCustomTrtColor(txtTreat.Text) 'ColorTranslator.FromHtml(clsToothTrt(0).FillColor)
        _propName = clsToothTrt(0).PropertyName
        _shapeId = clsToothTrt(0).ShapeID
        'ImpPopup.Visible = _propName.Contains("IMPLANT")
        'ImplantSpecsLbl.Visible = _propName.Contains("IMPLANT")
        ImpPopup.Visible = impCheck.Checked ' _propName.Contains("IMPLANT")
        ImplantSpecsLbl.Visible = impCheck.Checked ' _propName.Contains("IMPLANT")
        If clsToothTrt(0).IsExternal.HasValue Then
            external = clsToothTrt(0).IsExternal
        Else
            external = False
        End If
        If external = True Then
            GrpPays.Enabled = False
            GrpExtern.Enabled = True
        Else
            GrpPays.Enabled = True
            GrpExtern.Enabled = False
        End If
        If clsToothTrt(0).IsPaid.HasValue Then
            paid = clsToothTrt(0).IsPaid
        Else
            paid = False
        End If
        isPaidChck.Checked = paid
        IsExternalchk.Checked = external
        txtExtClinic.Text = clsToothTrt(0).ExternalClinicName
        loaded = True
        TrtBS.DataSource = clsToothTrt.ToList
    End Sub
    Public Sub New(ByVal clsToothTrt As Patient_Diagnosis, ByVal clsPatient As Patient)
        InitializeComponent()
        Me.Icon = AppIcon
        loaded = False
        isPaidChck.Checked = False
        IsExternalchk.Checked = False
        _toothTrt = clsToothTrt
        txtPatientID.Text = clsToothTrt.PatientID
        txtPatientName.Text = clsPatient.PatientName
        txtToothNum.Text = clsToothTrt.ToothNum
        txtToothName.Text = GetToothFullName(clsToothTrt.ToothName)
        clrFillColor.Color = GetCustomTrtColor(clsToothTrt.Treat) ' ColorTranslator.FromHtml(clsToothTrt.FillColor)
        clrBorderColor.Color = ColorTranslator.FromHtml(clsToothTrt.BorderColor)
        tbThick.Value = 0
        txtTreat.Text = clsToothTrt.Treat
        _propName = clsToothTrt.PropertyName
        _shapeId = clsToothTrt.ShapeID
        'ImpPopup.Visible = _propName.Contains("IMPLANT")
        'ImplantSpecsLbl.Visible = _propName.Contains("IMPLANT")
        ImpPopup.Visible = impCheck.Checked ' _propName.Contains("IMPLANT")
        ImplantSpecsLbl.Visible = impCheck.Checked ' _propName.Contains("IMPLANT")
        If clsToothTrt.IsExternal.HasValue Then
            external = clsToothTrt.IsExternal
        Else
            external = False
        End If
        If external = True Then
            GrpPays.Enabled = False
            GrpExtern.Enabled = True
        Else
            GrpPays.Enabled = True
            GrpExtern.Enabled = False
        End If
        If clsToothTrt.IsPaid.HasValue Then
            paid = clsToothTrt.IsPaid
        Else
            paid = False
        End If
        external = clsToothTrt.IsExternal
        paid = clsToothTrt.IsPaid
        isPaidChck.Checked = paid
        IsExternalchk.Checked = external
        loaded = True
        If clsToothTrt.BorderColor.Length = 8 Then
            SetBrdrClrFromHex(clsToothTrt.FillColor)
        End If
        txtExtClinic.Text = clsToothTrt.ExternalClinicName
        TrtBS.DataSource = clsToothTrt
    End Sub
    Private Function GetToothFullName(ByVal toothname As String) As String
        If String.IsNullOrEmpty(toothname) OrElse toothname.Length < 3 Then Return ""
        Dim direction As String = toothname.Substring(0, 1)
        Dim position As String = toothname.Substring(1, 1)
        Dim number As String = toothname.Substring(2)
        Dim directionFull As String = If(direction = "R", "RIGHT", If(direction = "L", "LEFT", ""))
        Dim positionFull As String = If(position = "U", "UPPER", If(position = "D", "LOWER", ""))
        If String.IsNullOrEmpty(directionFull) OrElse String.IsNullOrEmpty(positionFull) Then Return toothname
        Return $"{directionFull} {positionFull} {number}"
    End Function
    Private Sub NewTrtAddCTL_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        dtTrtDate.DateTime = Date.Now
        GrpPays.Visible = False
    End Sub

    ''' <summary>Diagnosis rows do not store price/pay; external vs in-house drives clinic name and IsPaid (external = paid).</summary>
    Private Sub ApplyExternalClinicAndPaid(ByVal row As Patient_Diagnosis)
        row.IsExternal = IsExternalchk.Checked
        If row.IsExternal Then
            row.IsPaid = True
            row.ExternalClinicName = If(String.IsNullOrWhiteSpace(txtExtClinic.Text), "Somewhere Else", txtExtClinic.Text.Trim())
        Else
            row.IsPaid = False
            row.ExternalClinicName = "In House"
        End If
    End Sub
    Private Sub btTrtClrDef_Click(sender As Object, e As EventArgs) Handles btTrtClrDef.Click
        If txtTreat.Text.Length = 0 Then Exit Sub
        clrFillColor.Color = GetDefaultTrtColor(txtTreat.Text)
    End Sub
    Private Sub txtTreat_EditValueChanged(sender As Object, e As EventArgs) Handles txtTreat.EditValueChanged
        If txtTreat.Text.Length = 0 Then Exit Sub
        clrFillColor.Color = GetCustomTrtColor(txtTreat.Text)
    End Sub
    Private Sub btnExit_Click(sender As Object, e As EventArgs)
        Me.Dispose()
    End Sub
    Public Sub SetBrdrClrFromHex(hexColor As String)
        Dim alpha, red, green, blue As Byte
        ' Remove the # if present
        If hexColor.StartsWith("#") Then
            hexColor = hexColor.Substring(1)
        End If
        ' Check if we have a valid ARGB format (8 characters)
        If hexColor.Length <> 8 Then
            Throw New ArgumentException("Hex color must be in #AARRGGBB format")
        End If
        ' Parse each component
        alpha = Convert.ToByte(hexColor.Substring(0, 2), 16)
        red = Convert.ToByte(hexColor.Substring(2, 2), 16)
        green = Convert.ToByte(hexColor.Substring(4, 2), 16)
        blue = Convert.ToByte(hexColor.Substring(6, 2), 16)
        clrBorderColor.Color = Color.FromArgb(alpha, red, green, blue)
    End Sub
    Private Function SetToothTrt() As Boolean
        Dim titleEn As String = "Field Required"
        Dim titleAr As String = "حقل مطلوب"
        Dim title As String = If(Eng, titleEn, titleAr)

        If cboTrtType.SelectedIndex < 0 Then
            Dim msgEn As String = "Select Treatment Type."
            Dim msgAr As String = "اختر نوع العلاج."
            Dim msg As String = If(Eng, msgEn, msgAr)
            MessageBox.Show(msg, title, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End If
        If dtTrtDate.Text.Length = 0 Then
            Dim msgEn As String = "Enter Treatment Date."
            Dim msgAr As String = "أدخل تاريخ العلاج."
            Dim msg As String = If(Eng, msgEn, msgAr)
            MessageBox.Show(msg, title, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End If
        If ceFinish.Checked Then
            If dtTrtEnd.Text.Length = 0 Then
                Dim msgEn As String = "Enter Treatment End Date."
                Dim msgAr As String = "أدخل تاريخ انتهاء العلاج."
                Dim msg As String = If(Eng, msgEn, msgAr)
                MessageBox.Show(msg, title, MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return False
            End If
        End If
        If txtTreat.Text.Length = 0 Then
            Dim msgEn As String = "Enter Treatment."
            Dim msgAr As String = "أدخل العلاج."
            Dim msg As String = If(Eng, msgEn, msgAr)
            MessageBox.Show(msg, title, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End If
        If txtToothName.Text.Length = 0 Then
            Dim msgEn As String = "Tooth Name is missing."
            Dim msgAr As String = "اسم السن مفقود."
            Dim msg As String = If(Eng, msgEn, msgAr)
            MessageBox.Show(msg, title, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End If
        If txtToothNum.Text.Length = 0 Then
            Dim msgEn As String = "Tooth Number is missing."
            Dim msgAr As String = "رقم السن مفقود."
            Dim msg As String = If(Eng, msgEn, msgAr)
            MessageBox.Show(msg, title, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End If
        If txtPatientID.Text.Length = 0 Then
            Dim msgEn As String = "Patient Number is missing."
            Dim msgAr As String = "رقم المريض مفقود."
            Dim msg As String = If(Eng, msgEn, msgAr)
            MessageBox.Show(msg, title, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End If
        If txtPatientName.Text.Length = 0 Then
            Dim msgEn As String = "Patient Name is missing."
            Dim msgAr As String = "اسم المريض مفقود."
            Dim msg As String = If(Eng, msgEn, msgAr)
            MessageBox.Show(msg, title, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End If
        Dim endDate As DateTime? = Nothing
        If ceFinish.Checked Then
            If dtTrtEnd.Text.Length > 0 Then
                endDate = dtTrtEnd.DateTime
            End If
        End If
        If txtTreat.Text.Contains("IMPLANT") Then
            If GetShapeIDByTrt("IMPLANT") < 1 Then
                Dim msgEn As String = "The selected Treat Has No Shape."
                Dim msgAr As String = "العلاج المحدد ليس له شكل."
                Dim msg As String = If(Eng, msgEn, msgAr)
                MessageBox.Show(msg, title, MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return False
            End If
        Else
            If GetShapeIDByTrt(txtTreat.Text) < 1 Then
                Dim msgEn As String = "The selected Treat Has No Shape."
                Dim msgAr As String = "العلاج المحدد ليس له شكل."
                Dim msg As String = If(Eng, msgEn, msgAr)
                MessageBox.Show(msg, title, MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return False
            End If
        End If
        'If _propName.Contains("IMPLANT") Then
        '    If _formattedResult.Length < 10 Then
        '        Dim msgEn As String = "IMPLANT Specs is missing."
        '        Dim msgAr As String = "مواصفات الزرعة مفقودة."
        '        Dim msg As String = If(Eng, msgEn, msgAr)
        '        MessageBox.Show(msg, title, MessageBoxButtons.OK, MessageBoxIcon.Error)
        '        Return False
        '    End If
        'End If
        If impCheck.Checked Then ' _propName.Contains("IMPLANT") Then
            If _formattedResult.Length < 10 Then
                Dim msgEn As String = "IMPLANT Specs is missing."
                Dim msgAr As String = "مواصفات الزرع مفقودة."
                Dim msg As String = If(Eng, msgEn, msgAr)
                MessageBox.Show(msg, title, MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return False
            End If
        End If
        Dim hexFillWithAlpha As String = $"#{fillClrWithOpacity.A:X2}{fillClrWithOpacity.R:X2}{fillClrWithOpacity.G:X2}{fillClrWithOpacity.B:X2}"
        Dim hexBrdrWithAlpha As String = $"#{brdrClrWithOpacity.A:X2}{brdrClrWithOpacity.R:X2}{brdrClrWithOpacity.G:X2}{brdrClrWithOpacity.B:X2}"
        _toothTrt.BorderColor = hexBrdrWithAlpha ' ColorTranslator.ToHtml(clrBorderColor.Color) #7F9B9A9A
        _toothTrt.BorderThickness = CByte(tbThick.Value)
        _toothTrt.FillColor = hexFillWithAlpha ' ColorTranslator.ToHtml(clrFillColor.Color) ' hexWithAlpha ' 
        _toothTrt.PatientID = PatientID
        _toothTrt.ShapeID = _shapeId
        _toothTrt.PropertyName = _propName
        _toothTrt.ToothName = txtToothName.Text 'GetToothFullName(clsToothTrt.ToothName) ' 
        _toothTrt.ToothNum = txtToothNum.Text
        _toothTrt.TreatDate = dtTrtDate.DateTime
        _toothTrt.TreatPlan = txtTrtPlan.Text
        _toothTrt.Treat = txtTreat.Text
        If _propName.Contains("IMPLANT") Then
            _toothTrt.LVL = GetLVL("IMPLANT")
        Else
            _toothTrt.LVL = GetLVL(txtTreat.Text)
        End If
        _toothTrt.TreatDetails = txtTrtDetails.Text
        _toothTrt.TreatmentType = cboTrtType.Text
        _toothTrt.TreatNotes = txtTrtNotes.Text
        _toothTrt.Finished = If(ceFinish.Checked, 1, 0)
        If ceFinish.Checked Then
            _toothTrt.TreatEndDate = dtTrtEnd.DateTime
        Else
            _toothTrt.TreatEndDate = Nothing
        End If
        ApplyExternalClinicAndPaid(_toothTrt)
        Return True
    End Function
    Private Function SetToothTrt(ByVal trtClas As Patient_Diagnosis) As Boolean

        ' Force data binding to update the data source
        TrtBS.EndEdit()

        ' The trtCls should already be updated via data binding
        ' But if not, explicitly copy values:
        ' trtCls.TrtValue = Val(txtTrtPrice.Text)
        ' trtCls.PayValue = Val(txtPayValue.Text)
        ' trtCls.TreatNotes = txtTreatNotes.Text
        txtToothNum.Text = trtClas.ToothNum
        txtToothName.Text = GetToothFullName(trtClas.ToothName)
        Dim titleEn As String = "Field Required"
        Dim titleAr As String = "حقل مطلوب"
        Dim title As String = If(Eng, titleEn, titleAr)

        If cboTrtType.SelectedIndex < 0 Then
            Dim msgEn As String = "Select Treatment Type."
            Dim msgAr As String = "اختر نوع العلاج."
            Dim msg As String = If(Eng, msgEn, msgAr)
            MessageBox.Show(msg, title, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End If
        If dtTrtDate.Text.Length = 0 Then
            Dim msgEn As String = "Enter Treatment Date."
            Dim msgAr As String = "أدخل تاريخ العلاج."
            Dim msg As String = If(Eng, msgEn, msgAr)
            MessageBox.Show(msg, title, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End If
        If ceFinish.Checked Then
            If dtTrtEnd.Text.Length = 0 Then
                Dim msgEn As String = "Enter Treatment End Date."
                Dim msgAr As String = "أدخل تاريخ انتهاء العلاج."
                Dim msg As String = If(Eng, msgEn, msgAr)
                MessageBox.Show(msg, title, MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return False
            End If
        End If
        If txtTreat.Text.Length = 0 Then
            Dim msgEn As String = "Enter Treatment."
            Dim msgAr As String = "أدخل العلاج."
            Dim msg As String = If(Eng, msgEn, msgAr)
            MessageBox.Show(msg, title, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End If
        If txtToothName.Text.Length = 0 Then
            Dim msgEn As String = "Tooth Name is missing."
            Dim msgAr As String = "اسم السن مفقود."
            Dim msg As String = If(Eng, msgEn, msgAr)
            MessageBox.Show(msg, title, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End If
        If txtToothNum.Text.Length = 0 Then
            Dim msgEn As String = "Tooth Number is missing."
            Dim msgAr As String = "رقم السن مفقود."
            Dim msg As String = If(Eng, msgEn, msgAr)
            MessageBox.Show(msg, title, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End If
        If txtPatientID.Text.Length = 0 Then
            Dim msgEn As String = "Patient Number is missing."
            Dim msgAr As String = "رقم المريض مفقود."
            Dim msg As String = If(Eng, msgEn, msgAr)
            MessageBox.Show(msg, title, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End If
        If txtPatientName.Text.Length = 0 Then
            Dim msgEn As String = "Patient Name is missing."
            Dim msgAr As String = "اسم المريض مفقود."
            Dim msg As String = If(Eng, msgEn, msgAr)
            MessageBox.Show(msg, title, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End If
        Dim endDate As DateTime? = Nothing
        If ceFinish.Checked Then
            If dtTrtEnd.Text.Length > 0 Then
                endDate = dtTrtEnd.DateTime
            End If
        End If
        If txtTreat.Text.Contains("IMPLANT") Then
            If GetShapeIDByTrt("IMPLANT") < 1 Then
                Dim msgEn As String = "The selected Treat Has No Shape."
                Dim msgAr As String = "العلاج المحدد ليس له شكل."
                Dim msg As String = If(Eng, msgEn, msgAr)
                MessageBox.Show(msg, title, MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return False
            End If
        Else
            If GetShapeIDByTrt(txtTreat.Text) < 1 Then
                Dim msgEn As String = "The selected Treat Has No Shape."
                Dim msgAr As String = "العلاج المحدد ليس له شكل."
                Dim msg As String = If(Eng, msgEn, msgAr)
                MessageBox.Show(msg, title, MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return False
            End If
        End If

        'If _propName.Contains("IMPLANT") Then
        '    If _formattedResult.Length < 10 Then
        '        Dim msgEn As String = "IMPLANT Specs is missing."
        '        Dim msgAr As String = "مواصفات الزرع مفقودة."
        '        Dim msg As String = If(Eng, msgEn, msgAr)
        '        MessageBox.Show(msg, title, MessageBoxButtons.OK, MessageBoxIcon.Error)
        '        Return False
        '    End If
        'End If
        If impCheck.Checked Then ' _propName.Contains("IMPLANT") Then
            If _formattedResult.Length < 10 Then
                Dim msgEn As String = "IMPLANT Specs is missing."
                Dim msgAr As String = "مواصفات الزرع مفقودة."
                Dim msg As String = If(Eng, msgEn, msgAr)
                MessageBox.Show(msg, title, MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return False
            End If
        End If
        'Dim clrWithOpacity As Color = Color.FromArgb(128, clrFillColor.Color.R, clrFillColor.Color.G, clrFillColor.Color.B)
        ' 128 is 50% opacity (range is 0-255)
        'Dim rgbaString As String = $"rgba({clrWithOpacity.R}, {clrWithOpacity.G}, {clrWithOpacity.B}, {clrWithOpacity.A / 255.0})"brdrClrWithOpacity
        'Console.WriteLine(rgbaString) ' Example: "rgba(255, 0, 0, 0.5)"
        Dim hexFillWithAlpha As String = $"#{fillClrWithOpacity.A:X2}{fillClrWithOpacity.R:X2}{fillClrWithOpacity.G:X2}{fillClrWithOpacity.B:X2}"
        Dim hexBrdrWithAlpha As String = $"#{brdrClrWithOpacity.A:X2}{brdrClrWithOpacity.R:X2}{brdrClrWithOpacity.G:X2}{brdrClrWithOpacity.B:X2}"

        trtClas.BorderColor = hexBrdrWithAlpha ' ColorTranslator.ToHtml(clrBorderColor.Color)
        trtClas.BorderThickness = CByte(tbThick.Value)
        trtClas.FillColor = hexFillWithAlpha ' ColorTranslator.ToHtml(clrFillColor.Color) '
        trtClas.PatientID = PatientID
        trtClas.ShapeID = _shapeId
        trtClas.PropertyName = _propName
        trtClas.ToothName = txtToothName.Text 'GetToothFullName(clsToothTrt.ToothName) ' 
        trtClas.ToothNum = txtToothNum.Text
        trtClas.TreatDate = dtTrtDate.DateTime
        trtClas.TreatPlan = txtTrtPlan.Text
        trtClas.Treat = txtTreat.Text
        If _propName.Contains("IMPLANT") Then
            trtClas.LVL = GetLVL("IMPLANT")
        Else
            trtClas.LVL = GetLVL(txtTreat.Text)
        End If

        trtClas.TreatDetails = txtTrtDetails.Text
        trtClas.TreatmentType = cboTrtType.Text
        trtClas.TreatNotes = txtTrtNotes.Text
        trtClas.Finished = If(ceFinish.Checked, 1, 0)
        If ceFinish.Checked Then
            trtClas.TreatEndDate = dtTrtEnd.DateTime
        Else
            trtClas.TreatEndDate = Nothing
        End If
        ApplyExternalClinicAndPaid(trtClas)
        Return True


    End Function

    Private Function SetBsTrt(ByVal trtClas As Patient_Diagnosis) As Boolean

        ' Force data binding to update the data source
        TrtBS.EndEdit()

        ' The trtCls should already be updated via data binding
        ' But if not, explicitly copy values:
        ' trtCls.TrtValue = Val(txtTrtPrice.Text)
        ' trtCls.PayValue = Val(txtPayValue.Text)
        ' trtCls.TreatNotes = txtTreatNotes.Text
        txtToothNum.Text = trtClas.ToothNum
        txtToothName.Text = GetToothFullName(trtClas.ToothName)
        Dim titleEn As String = "Field Required"
        Dim titleAr As String = "حقل مطلوب"
        Dim title As String = If(Eng, titleEn, titleAr)

        If cboTrtType.SelectedIndex < 0 Then
            Dim msgEn As String = "Select Treatment Type."
            Dim msgAr As String = "اختر نوع العلاج."
            Dim msg As String = If(Eng, msgEn, msgAr)
            MessageBox.Show(msg, title, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End If
        If dtTrtDate.Text.Length = 0 Then
            Dim msgEn As String = "Enter Treatment Date."
            Dim msgAr As String = "أدخل تاريخ العلاج."
            Dim msg As String = If(Eng, msgEn, msgAr)
            MessageBox.Show(msg, title, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End If
        If ceFinish.Checked Then
            If dtTrtEnd.Text.Length = 0 Then
                Dim msgEn As String = "Enter Treatment End Date."
                Dim msgAr As String = "أدخل تاريخ انتهاء العلاج."
                Dim msg As String = If(Eng, msgEn, msgAr)
                MessageBox.Show(msg, title, MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return False
            End If
        End If
        If txtTreat.Text.Length = 0 Then
            Dim msgEn As String = "Enter Treatment."
            Dim msgAr As String = "أدخل العلاج."
            Dim msg As String = If(Eng, msgEn, msgAr)
            MessageBox.Show(msg, title, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End If
        If txtToothName.Text.Length = 0 Then
            Dim msgEn As String = "Tooth Name is missing."
            Dim msgAr As String = "اسم السن مفقود."
            Dim msg As String = If(Eng, msgEn, msgAr)
            MessageBox.Show(msg, title, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End If
        If txtToothNum.Text.Length = 0 Then
            Dim msgEn As String = "Tooth Number is missing."
            Dim msgAr As String = "رقم السن مفقود."
            Dim msg As String = If(Eng, msgEn, msgAr)
            MessageBox.Show(msg, title, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End If
        If txtPatientID.Text.Length = 0 Then
            Dim msgEn As String = "Patient Number is missing."
            Dim msgAr As String = "رقم المريض مفقود."
            Dim msg As String = If(Eng, msgEn, msgAr)
            MessageBox.Show(msg, title, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End If
        If txtPatientName.Text.Length = 0 Then
            Dim msgEn As String = "Patient Name is missing."
            Dim msgAr As String = "اسم المريض مفقود."
            Dim msg As String = If(Eng, msgEn, msgAr)
            MessageBox.Show(msg, title, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End If
        Dim endDate As DateTime? = Nothing
        If ceFinish.Checked Then
            If dtTrtEnd.Text.Length > 0 Then
                endDate = dtTrtEnd.DateTime
            End If
        End If
        If txtTreat.Text.Contains("IMPLANT") Then
            If GetShapeIDByTrt("IMPLANT") < 1 Then
                Dim msgEn As String = "The selected Treat Has No Shape."
                Dim msgAr As String = "العلاج المحدد ليس له شكل."
                Dim msg As String = If(Eng, msgEn, msgAr)
                MessageBox.Show(msg, title, MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return False
            End If
        Else
            If GetShapeIDByTrt(txtTreat.Text) < 1 Then
                Dim msgEn As String = "The selected Treat Has No Shape."
                Dim msgAr As String = "العلاج المحدد ليس له شكل."
                Dim msg As String = If(Eng, msgEn, msgAr)
                MessageBox.Show(msg, title, MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return False
            End If
        End If

        If _propName.Contains("IMPLANT") Then
            If _formattedResult.Length < 10 Then
                Dim msgEn As String = "IMPLANT Specs is missing."
                Dim msgAr As String = "مواصفات الزرع مفقودة."
                Dim msg As String = If(Eng, msgEn, msgAr)
                MessageBox.Show(msg, title, MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return False
            End If
        End If
        Dim hexFillWithAlpha As String = $"#{fillClrWithOpacity.A:X2}{fillClrWithOpacity.R:X2}{fillClrWithOpacity.G:X2}{fillClrWithOpacity.B:X2}"
        Dim hexBrdrWithAlpha As String = $"#{brdrClrWithOpacity.A:X2}{brdrClrWithOpacity.R:X2}{brdrClrWithOpacity.G:X2}{brdrClrWithOpacity.B:X2}"

        trtClas.BorderColor = hexBrdrWithAlpha ' ColorTranslator.ToHtml(clrBorderColor.Color)
        trtClas.BorderThickness = CByte(tbThick.Value)
        trtClas.FillColor = hexFillWithAlpha ' ColorTranslator.ToHtml(clrFillColor.Color) '
        trtClas.PatientID = PatientID
        trtClas.ShapeID = _shapeId
        trtClas.PropertyName = _propName
        trtClas.ToothName = txtToothName.Text 'GetToothFullName(clsToothTrt.ToothName) ' 
        trtClas.ToothNum = txtToothNum.Text
        trtClas.TreatDate = dtTrtDate.DateTime
        trtClas.TreatPlan = txtTrtPlan.Text
        trtClas.Treat = txtTreat.Text
        If _propName.Contains("IMPLANT") Then
            trtClas.LVL = GetLVL("IMPLANT")
        Else
            trtClas.LVL = GetLVL(txtTreat.Text)
        End If

        trtClas.TreatDetails = txtTrtDetails.Text
        trtClas.TreatmentType = cboTrtType.Text
        trtClas.TreatNotes = txtTrtNotes.Text
        trtClas.Finished = If(ceFinish.Checked, 1, 0)
        If ceFinish.Checked Then
            trtClas.TreatEndDate = dtTrtEnd.DateTime
        Else
            trtClas.TreatEndDate = Nothing
        End If
        ApplyExternalClinicAndPaid(trtClas)
        Return True


    End Function





#Region "New Save Code"
    Private toothNumsInt As New List(Of Integer)
    Private toothNums As New List(Of String)
    Dim OnePayAdded As Boolean = False
    Private Sub btnSaveTrt_Click(sender As Object, e As EventArgs) Handles btnSaveTrt.Click
        Dim treatmentText, toothName As String
        Dim clsPatientTrtsData As New Patient_TrtsDATA
        ' Determine if this is a multi-tooth treatment
        Dim isMultiTrt As Boolean = IsMultiToothTreatment(_toothTrt.Treat)
        Dim treatmentGroupID As Guid = Guid.Empty
        ' Only create group ID once if this is a multi-tooth treatment
        If isMultiTrt AndAlso selectedToothTrtList.Count > 0 Then
            treatmentGroupID = Guid.NewGuid()
        End If

        If selectedToothTrtList.Count = 0 Then
            ' Single tooth treatment
            If SetToothTrt() Then
                treatmentText = _toothTrt.Treat
                toothName = _toothTrt.ToothName
                Select Case _toothTrt.Treat
                    Case "ABUTMENT", "HEALING CAP"
                        Saved = SaveTreatmentWithAbutment(_toothTrt)
                    Case Else
                        Saved = SaveTreatmentWithTransaction(PatientID, _toothTrt, treatmentText, toothName, isMultiTrt, treatmentGroupID)
                End Select
            End If
        Else
            ' Multi-tooth treatment
            Dim firstToothInGroup As Boolean = True
            toothNums.Clear()
            toothNumsInt.Clear()
            For Each trtCls In selectedToothTrtList
                toothNums.Add(GetShortToothNameWithDash(trtCls.ToothNum))
                toothNumsInt.Add(CInt(trtCls.ToothNum))
            Next

            '' Safe approach that handles various scenarios
            'Dim bsList As New List(Of Patient_Diagnosis)

            'If TrtBS.DataSource IsNot Nothing Then
            '    bsList = DirectCast(TrtBS.DataSource, List(Of Patient_Diagnosis))
            'End If
            '' Now you have your new list
            '' bsList contains all Patient_Diagnosis items from TrtBS.DataSource
            'For Each trtCls In bsList
            '    If SetToothTrt(trtCls) Then
            '        Dim trtPrice As Double = Val(txtTrtPrice.Text)
            '        Dim payValue As Double = Val(txtPayValue.Text)
            '        ' Set properties BEFORE assigning to _toothTrt
            '        trtCls.IsMultiTrt = isMultiTrt
            '        trtCls.TrtGroupID = treatmentGroupID
            '        _toothTrt = trtCls
            '        treatmentText = trtCls.Treat
            '        trtCls.ToothName = txtToothName.Text 'GetToothFullName(clsToothTrt.ToothName) ' 
            '        trtCls.ToothNum = txtToothNum.Text
            '        trtCls.PayValue = payValue
            '        trtCls.TrtValue = trtPrice
            '        toothName = trtCls.ToothName
            '        Dim oldTrt As String = If(_propName.StartsWith("IMPLANT"), _formattedResult, GetOldTrt(trtCls.Treat))
            '        Select Case trtCls.Treat
            '            Case "ABUTMENT", "HEALING CAP"
            '                Saved = SaveTreatmentWithAbutment(trtCls)
            '            Case Else
            '                Saved = SaveTreatmentWithTransaction(PatientID, trtCls, treatmentText, toothName, isMultiTrt, treatmentGroupID, firstToothInGroup)
            '        End Select
            '        'Saved = SaveTreatmentWithTransaction(trtCls.PatientID, _toothTrt, treatmentText, toothName,
            '        firstToothInGroup = False ' Only first tooth gets accounting record
            '    End If
            'Next
            ' ORIGINAL CODE BELOW
            For Each trtCls In selectedToothTrtList
                If SetToothTrt(trtCls) Then
                    ' Set properties BEFORE assigning to _toothTrt
                    trtCls.IsMultiTrt = isMultiTrt
                    trtCls.TrtGroupID = treatmentGroupID
                    _toothTrt = trtCls
                    treatmentText = trtCls.Treat
                    toothName = trtCls.ToothName
                    Dim oldTrt As String = If(_propName.StartsWith("IMPLANT"), _formattedResult, GetOldTrt(trtCls.Treat))
                    Select Case _toothTrt.Treat
                        Case "ABUTMENT", "HEALING CAP"
                            Saved = SaveTreatmentWithAbutment(_toothTrt)
                        Case Else
                            Saved = SaveTreatmentWithTransaction(PatientID, _toothTrt, treatmentText, toothName, isMultiTrt, treatmentGroupID, firstToothInGroup)
                    End Select
                    'Saved = SaveTreatmentWithTransaction(trtCls.PatientID, _toothTrt, treatmentText, toothName,
                    firstToothInGroup = False ' Only first tooth gets accounting record
                End If
            Next
        End If
        OnePayAdded = False
        If Saved Then
            Me.DialogResult = DialogResult.OK
            Me.Close()
        Else
            Me.DialogResult = DialogResult.Cancel
        End If
    End Sub
    Private Function GetLastToothTrtID() As Integer
        Dim dx As New DentistXDATA
        Using conn As New SqlConnection(dx.GetConnectionString)
            conn.Open()
            Return conn.ExecuteScalar(Of Integer)("SELECT TOP 1 DiagID FROM Patient_Diagnosis ORDER BY DiagID DESC")
        End Using
    End Function

    Public Function SaveTreatmentWithAbutment(_toothTrt As Patient_Diagnosis) As Boolean
        Dim saved As Boolean = False
        Dim canceled As Boolean = False
        Dim clsTrtsData As New Patient_TrtsDATA
        Dim toothTrtData As New Patient_DiagnosisDATA
        Dim Dx = New DentistXDATA
        Dim ConnectionString = Dx.GetConnectionString
        Return toothTrtData.AddNormal(_toothTrt)
    End Function
    Public Function SaveTreatmentWithTransaction(PatientID As Integer, _toothTrt As Patient_Diagnosis,
                                      treatmentText As String, toothName As String,
                                      Optional isMultiTooth As Boolean = False,
                                      Optional treatmentGroupID As Guid = Nothing,
                                      Optional isFirstInGroup As Boolean = True) As Boolean
        Dim saved As Boolean = False
        Dim canceled As Boolean = False
        Dim clsTrtsData As New Patient_TrtsDATA
        Dim toothTrtData As New Patient_DiagnosisDATA
        Dim Dx = New DentistXDATA
        Dim ConnectionString = Dx.GetConnectionString
        Dim oldTrt As String = If(_propName.StartsWith("IMPLANT"), _formattedResult, GetOldTrt(_toothTrt.Treat))
        Dim userID As Integer = CurrentUser.UsID
        _toothTrt.UserID = userID
        Using conn As New SqlConnection(ConnectionString)
            conn.Open()
            Using trans As SqlTransaction = conn.BeginTransaction()
                Try
                    AddTreatFrmChartToGrids_Transactional(conn, trans, PatientID, _toothTrt.ToothNum, _toothTrt.ToothName, oldTrt)
                    _toothTrt.QrtrTable = ""
                    _toothTrt.QrtrID = Nothing
                    _toothTrt.QrtrAddress = Nothing
                    _toothTrt.QrtrColumnName = ""
                    _toothTrt.QrtrColumnValue = ""
                    If isMultiTooth Then
                        _toothTrt.ParentDiagID = -1
                        _toothTrt.IsMultiTrt = False
                        _toothTrt.TrtGroupID = treatmentGroupID
                    End If
                    ' Diagnosis: no treatment price or payment fields; external vs in-house only
                    ApplyExternalClinicAndPaid(_toothTrt)
                    'A new Try
                    '======================
                    'its here where i'm stuck
                    Dim MaxLvl As Integer = toothTrtData.GetTreatLVL(_toothTrt.PatientID, _toothTrt.ToothNum)
                    Dim currentLvl As Integer = _toothTrt.LVL
                    'check if treat is a normal one after high level one
                    If (MaxLvl > 4 AndAlso currentLvl < 4) Then
                        Dim msgEng As String = "You Cant Add a Normal Treat On High Level Treat...."
                        Dim msgAr As String = "لا يمكنك إضافة علاج عادي على علاج عالي المستوى...."
                        Dim msg As String = If(Eng, msgEng, msgAr)
                        MsgBox(msg)
                        trans.Rollback()
                        Return False
                    End If
                    ' Special case: implant (5) → extraction (4) or extraction (4) → implant (5)
                    If (MaxLvl > 4 AndAlso currentLvl = 4) Then ' OrElse (MaxLvl = 4 AndAlso currentLvl = 5) Then
                        ' 1. Finish old treatment
                        conn.Execute("
                                            UPDATE Patient_Diagnosis
                                            SET LVL = 4
                                            WHERE PatientID = @PatientID
                                              AND ToothNum = @ToothNum
                                              AND LVL > 4
                                        ", New With {
                                            .PatientID = _toothTrt.PatientID,
                                            .ToothNum = _toothTrt.ToothNum,
                                            .OldLvl = MaxLvl
                                        }, trans)
                    End If
                    '============================
                    'normal
                    If Not toothTrtData.AddTransactional(conn, trans, _toothTrt) Then
                        Dim msgEng As String = $"Failed to save Treatment in Chart '{treatmentText}' for '{toothName}'."
                        Dim msgAr As String = $"فشل في حفظ العلاج في السجل  '{treatmentText}' ل '{toothName}'."
                        Dim msg As String = If(Eng, msgEng, msgAr)
                        MessageBox.Show(msg)
                        trans.Rollback()
                        Return False
                    End If
                    ' Get the last inserted DiagID
                    Dim lastToothTrtID As Integer = conn.ExecuteScalar(Of Integer)(
                            "SELECT TOP 1 DiagID FROM Patient_Diagnosis ORDER BY DiagID DESC",
                            transaction:=trans)
                    'If IsMultiTrt then update ParentDiagID and insert into Patient_DiagInfo
                    If isMultiTooth Then
                        'Update ParentDiagID
                        Dim masterToothID As Integer = conn.ExecuteScalar(Of Integer)(
                                                "SELECT MIN(DiagID) FROM Patient_Diagnosis 
                                                 WHERE TrtGroupID = @TrtGroupID",
                                                New With {.TrtGroupID = treatmentGroupID},
                                                transaction:=trans)
                        ' Update all other teeth in group to point to first tooth
                        Dim rowsUpdated As Integer = conn.ExecuteScalar(
                                                "UPDATE Patient_Diagnosis 
                                                 SET ParentDiagID = @ParentDiagID 
                                                 WHERE TrtGroupID = @TrtGroupID 
                                                ",'  AND DiagID <> @ParentDiagID
                                                New With {
                                                    .ParentDiagID = masterToothID,
                                                    .TrtGroupID = treatmentGroupID
                                                },
                                                transaction:=trans)
                        ' Insert the record into Patient_DiagInfo
                        Dim Trt As Patient_Diagnosis = toothTrtData.Select_Record(New Patient_Diagnosis With {
                                                                                                    .DiagID = lastToothTrtID,
                                                                                                    .PatientID = PatientID,
                                                                                                    .ToothName = toothName
                                                                                                }, trans) ' Pass the transaction here)

                        Trt.QrtrAddress = _toothTrt.QrtrAddress
                        Trt.QrtrColumnValue = _toothTrt.QrtrColumnValue
                        Trt.QrtrID = _toothTrt.QrtrID
                        Trt.QrtrTable = _toothTrt.QrtrTable
                        Trt.QrtrColumnName = _toothTrt.QrtrColumnName
                        ' Validate the record was found
                        If Trt Is Nothing Then
                            Dim msgEng As String = "Failed to retrieve tooth treatment record."
                            Dim msgAr As String = "فشل في استرداد سجل علاج السن."
                            Dim msg As String = If(Eng, msgEng, msgAr)
                            MessageBox.Show(msg)
                            trans.Rollback()
                            Return False
                        End If
                        ' Insert with all required parameters
                        Dim rowsInserted As Integer = conn.ExecuteScalar(
                                                                            "INSERT INTO [dbo].[Patient_DiagInfo] (
                                                                            [PatientID], [ParentDiagID], [TrtGroupID], 
                                                                            [ToothNum], [ToothName], [TreatDate], [Treat],
                                                                            [TreatNotes], [IsExternal], [ExternalClinicName], 
                                                                            [ExternalTreatmentDate]
                                                                        ) VALUES (
                                                                            @PatientID, @ParentDiagID, @TrtGroupID, 
                                                                            @ToothNum, @ToothName, @TreatDate, @Treat, 
                                                                            @TreatNotes, @IsExternal, @ExternalClinicName, 
                                                                            @ExternalTreatmentDate
                                                                        )",
                                                                        New With {
                                                                            .PatientID = PatientID,
                                                                            .ParentDiagID = If(masterToothID > 0, masterToothID, DBNull.Value),
                                                                            .TrtGroupID = If(treatmentGroupID <> Guid.Empty, treatmentGroupID, DBNull.Value),
                                                                            .ToothNum = Trt.ToothNum,
                                                                            .ToothName = Trt.ToothName, ' Added missing parameter
                                                                            .TreatDate = Trt.TreatDate,
                                                                            .Treat = If(Trt.Treat, DBNull.Value),
                                                                            .TreatNotes = If(Trt.TreatNotes, DBNull.Value),
                                                                            .IsExternal = Trt.IsExternal,
                                                                            .ExternalClinicName = If(Trt.ExternalClinicName, DBNull.Value),
                                                                            .ExternalTreatmentDate = If(Trt.ExternalTreatmentDate.HasValue,
                                                                                                      Trt.ExternalTreatmentDate.Value,
                                                                                                      DBNull.Value)
                                                                        },
                                                                        transaction:=trans)
                    End If
                    ' Create accounting record for:
                    ' 1. Normal treatments, or
                    'Make sure its IN HOUSE Treat
                    If _toothTrt.IsExternal = False Then
                        ' 2. The FIRST tooth in a multi-tooth treatment group
                        If Not isMultiTooth OrElse isFirstInGroup Then
                            Dim detailText As String
                            Dim teeth As String = String.Join(",", toothNums)
                            If isMultiTooth AndAlso Not String.IsNullOrEmpty(teeth) Then
                                ' Use concatenated teeth string for multi-tooth treatments
                                detailText = _toothTrt.Treat & " ==>> " & teeth
                            Else
                                ' Use individual tooth name for single treatments
                                detailText = _toothTrt.Treat & " ==>> " & GetShortToothNameWithDash(_toothTrt.ToothNum)
                            End If

                        End If


                    End If
                    trans.Commit()
                    saved = True
                    canceled = False
                    FormManager.Instance.CurrentForm.UpdatePatientBalance(CurrentPatient)
                    Me.DialogResult = DialogResult.OK
                Catch ex As Exception
                    trans.Rollback()
                    Dim msgEng As String = "Transaction Error: " & ex.Message
                    Dim msgAr As String = "خطأ في الإضافة: " & ex.Message
                    Dim msg As String = If(Eng, msgEng, msgAr)
                    MessageBox.Show(ex.Message, msg, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    saved = False
                    canceled = True
                End Try
            End Using
        End Using
        Return saved
    End Function
    Private Function IsFirstInTreatmentGroup(conn As SqlConnection, trans As SqlTransaction, groupID As Guid) As Boolean
        If groupID = Guid.Empty Then Return False
        Dim count = conn.ExecuteScalar(Of Integer)(
        "SELECT COUNT(*) FROM Patient_Diagnosis WHERE TrtGroupID = @GroupID",
        New With {.GroupID = groupID},
        transaction:=trans)
        Return count = 1
    End Function
    Private Function IsMultiToothTreatment(treatmentType As String) As Boolean
        Dim multiToothTreatments As String() = {"METAL BRIDGE", "ZERCONIA BRIDGE", "PFM BRIDGE", "EMAX BRIDGE", "TEMP BRIDGE", "STAINLESS STEEL BRIDGE",
                                                "REMOVABLE PARTIAL DENTURE", "COMPLETE DENTURE", "EXTRACTION + IMPLANT"}
        Return multiToothTreatments.Contains(treatmentType.ToUpper())
    End Function
#End Region
    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.DialogResult = DialogResult.Cancel
        Saved = False
        Canceled = True
    End Sub
    ' Read-only property for Saved
    Private _saved As Boolean = False
    Public Property Saved As Boolean
        Get
            Return _saved
        End Get
        Set(value As Boolean)
            _saved = value
        End Set
    End Property
    ' Read-only property for _Canceled
    Private _canceled As Boolean = False
    Public Property Canceled As Boolean
        Get
            Return _canceled
        End Get
        Set(value As Boolean)
            _canceled = value
        End Set
    End Property

    Dim _alpha As Byte = 128
    Dim fillClrWithOpacity As Color '= Color.FromArgb(128, clrFillColor.Color.R, clrFillColor.Color.G, clrFillColor.Color.B)
    Dim brdrClrWithOpacity As Color '= Color.FromArgb(128, clrBorderColor.Color.R, clrBorderColor.Color.G, clrBorderColor.Color.B)
    Private Sub tbOpacity_EditValueChanged(sender As Object, e As EventArgs) Handles tbOpacity.EditValueChanged
        _alpha = CByte(tbOpacity.Value)
        clrFillColor.Color = Color.FromArgb(_alpha, clrFillColor.Color.R, clrFillColor.Color.G, clrFillColor.Color.B)
        clrBorderColor.Color = Color.FromArgb(_alpha, clrBorderColor.Color.R, clrBorderColor.Color.G, clrBorderColor.Color.B)
    End Sub
    Private Sub clrFillColor_EditValueChanged(sender As Object, e As EventArgs) Handles clrFillColor.EditValueChanged
        fillClrWithOpacity = Color.FromArgb(_alpha, clrFillColor.Color.R, clrFillColor.Color.G, clrFillColor.Color.B)
    End Sub
    Private Sub clrBorderColor_EditValueChanged(sender As Object, e As EventArgs) Handles clrBorderColor.EditValueChanged
        brdrClrWithOpacity = Color.FromArgb(_alpha, clrBorderColor.Color.R, clrBorderColor.Color.G, clrBorderColor.Color.B)
    End Sub

#Region "Implants"
    Dim _formattedResult As String = ""
    Dim _normalResult As String = ""
    Dim ImpBrand, ImpType, ImpDmm, ImpLmm, Slim As String

    Private Function ValidateStringFormat(inputString As String, ParamArray placeholders() As String) As Boolean

        For Each placeholder In placeholders
            If placeholder Is Nothing Then
                Return False
            End If
            If placeholder.Length < 2 Then
                Return False
            End If
        Next
        Return True
    End Function

    Private Sub SetResult()
        _formattedResult =
                            $"IMPLANT " &
                            $"{ImpBrand}-{ImpType}{vbCrLf}" &
                            $"{Slim} - {ImpDmm}x{ImpLmm}"
        ResultLbl.Text = _formattedResult
        _normalResult = $"IMPLANT {ImpBrand}-{ImpType}-{Slim} {ImpDmm}x{ImpLmm}"
    End Sub

    Private Sub ImpPopup_Closed(sender As Object, e As ClosedEventArgs) Handles ImpPopup.Closed
        If Not ValidateStringFormat(_normalResult, ImpBrand, ImpType, Slim, ImpDmm, ImpLmm) Then
            MsgBox(If(Eng, "Incomplete Implant Specifications.", "المواصفات غير مكتملة للزرعة."))
        End If
    End Sub


    Private Sub cmbBrand_ImplantBrandValueChanged(sender As Object, e As ImplantBrandCombo.ImplantBrandIndexChangedEvent) Handles cmbBrand.ImplantBrandValueChanged
        ImpBrand = e.BrandName
        SetResult()
    End Sub
    Private Sub cmbType_ImplantTypeValueChanged(sender As Object, e As ImplantTypeCombo.ImplantTypeIndexChangedEvent) Handles cmbType.ImplantTypeValueChanged
        ImpType = e.TypeName
        SetResult()
    End Sub
    Private Sub cmbDiameter_ImplantDiameterValueChanged(sender As Object, e As ImplantDiameterCombo.ImplantDiameterIndexChangedEvent) Handles cmbDiameter.ImplantDiameterValueChanged
        ImpDmm = e.DiameterMM
        SetResult()
    End Sub
    Private Sub cmbLength_ImplantLengthValueChanged(sender As Object, e As ImplantLengthCombo.ImplantLengthIndexChangedEvent) Handles cmbLength.ImplantLengthValueChanged
        ImpLmm = e.LengthMM
        SetResult()
    End Sub
    Private Sub cmbDesign_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbDesign.SelectedIndexChanged
        Slim = cmbDesign.Text
        SetResult()
    End Sub
    Private Sub btnSelect_Click(sender As Object, e As EventArgs) Handles btnSelect.Click

        ImpPopup.Text = _normalResult
        'txtTreat.Text = _normalResult
        txtTrtDetails.Text = _formattedResult
        ImpPopup.ClosePopup()
    End Sub
    Private Sub btnCancelImp_Click(sender As Object, e As EventArgs) Handles btnCancelImp.Click
        'If _toothTrt IsNot Nothing Then
        ImpPopup.Text = ""
        '    'txtTreat.Text = _toothTrt.Treat
        ImpPopup.ClosePopup()
        'End If

    End Sub
#End Region

    Function IsValidHexColor(colorStr As String) As Boolean
        ' Regex pattern for # followed by 8 hex digits (AARRGGBB)
        Dim pattern As String = "^#[0-9A-Fa-f]{8}$"
        Return System.Text.RegularExpressions.Regex.IsMatch(colorStr, pattern)
    End Function
    Function ColorToHex(ByVal clr As Color, ByVal alphaValue As Integer) As String
        ' Ensure alpha is within byte range (0-255)
        Dim alpha As Byte = CByte(Math.Max(0, Math.Min(255, alphaValue)))
        Dim red As Byte = clr.R
        Dim green As Byte = clr.G
        Dim blue As Byte = clr.B
        ' Format as #AARRGGBB (ensures 2-digit hex for each component)
        Return String.Format("#{0:X2}{1:X2}{2:X2}{3:X2}", alpha, red, green, blue)
    End Function
    Private ConnectionString As String = DentistXDATA.GetConnection.ConnectionString
    Private Sub btnSetCustmColor_Click(sender As Object, e As EventArgs) Handles btnSetCustmColor.Click
        Dim clsTblTrtDATA As New TblTRTSDATA
        Dim CheckTrtID As String = "SELECT  [TrtID]  FROM [dbo].[TblTRTS] WHERE Trt = @Trt"
        Dim Treat As String = txtTreat.Text
        Dim TrtID As Integer
        Using conn As New SqlConnection(ConnectionString)
            conn.Open()
            ' Check if any record exists with this treatment
            TrtID = conn.ExecuteScalar(Of Integer?)(CheckTrtID, New With {.Trt = Treat}) '.HasValue
        End Using
        If IsValidHexColor(ColorToHex(clrFillColor.Color, tbOpacity.Value)) AndAlso IsValidHexColor(ColorToHex(clrBorderColor.Color, tbOpacity.Value)) Then
            If clsTblTrtDATA.UpdateTrtClr(TrtID, ColorToHex(clrFillColor.Color, tbOpacity.Value), ColorToHex(clrBorderColor.Color, tbOpacity.Value), tbThick.Value) Then
                Dim x = clsTblTrtDATA.UpdateTreatFillColor(ColorToHex(clrFillColor.Color, tbOpacity.Value), Treat)
                If x > 0 Then
                    Dim msgEng As String = $"{x} Colors Updated In Treats Table"
                    Dim msgAr As String = $"{x} تم تحديث الألوان في جدول العلاجات"
                    Dim msg As String = If(Eng, msgEng, msgAr)
                    MsgBox(x & msg)
                End If
                Dim msgEng1 As String = $" Colors Updated In Treats Table"
                Dim msgAr1 As String = $" تم تحديث الألوان في جدول العلاجات"
                Dim msg1 As String = If(Eng, msgEng1, msgAr1)
                MsgBox(msg1)
            End If
        End If
    End Sub
    Private Sub txtPrice_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtTrtPrice.KeyPress, txtPayValue.KeyPress
        ' Allow control keys (Backspace, Delete, etc.)
        If Char.IsControl(e.KeyChar) Then
            Return
        End If
        ' Allow digits (0-9)
        If Char.IsDigit(e.KeyChar) Then
            Return
        End If
        ' Allow only ONE decimal point (for prices)
        If e.KeyChar = "."c AndAlso Not txtTrtPrice.Text.Contains(".") Then
            Return
        End If
        ' Block any other character
        e.Handled = True
    End Sub
    Private Sub txtPrice_PreviewKeyDown(sender As Object, e As PreviewKeyDownEventArgs) Handles txtTrtPrice.PreviewKeyDown, txtPayValue.PreviewKeyDown
        ' Allow Ctrl+V (paste) - We'll handle validation separately
        If e.Control AndAlso e.KeyCode = Keys.V Then
            Return
        End If
        ' Allow Backspace, Delete, Arrows, Tab
        If e.KeyCode = Keys.Back OrElse e.KeyCode = Keys.Delete OrElse
                       e.KeyCode = Keys.Left OrElse e.KeyCode = Keys.Right OrElse
                       e.KeyCode = Keys.Tab Then
            Return
        End If
        ' Allow numbers (0-9)
        If e.KeyCode >= Keys.D0 AndAlso e.KeyCode <= Keys.D9 Then
            Return
        End If
        ' Allow numpad numbers (0-9)
        If e.KeyCode >= Keys.NumPad0 AndAlso e.KeyCode <= Keys.NumPad9 Then
            Return
        End If
        ' Allow ONE decimal point (if needed)
        If e.KeyCode = Keys.Decimal AndAlso Not txtTrtPrice.Text.Contains(".") Then
            Return
        End If
        ' Block all other keys
        e.IsInputKey = False
    End Sub
    Private Sub txtPrice_EditValueChanged(sender As Object, e As EventArgs) Handles txtTrtPrice.EditValueChanged, txtPayValue.EditValueChanged
        ' Skip if empty
        If String.IsNullOrEmpty(txtTrtPrice.Text) Then Return
        ' Store cursor position
        Dim cursorPos = txtTrtPrice.SelectionStart
        ' Remove all non-numeric characters (except one decimal point)
        Dim cleanedText As New System.Text.StringBuilder()
        Dim hasDecimal As Boolean = False
        For Each c As Char In txtTrtPrice.Text
            If Char.IsDigit(c) Then
                cleanedText.Append(c)
            ElseIf c = "."c AndAlso Not hasDecimal Then
                cleanedText.Append(c)
                hasDecimal = True
            End If
        Next
        ' If text was modified (due to invalid pasted chars), update it
        If cleanedText.ToString() <> txtTrtPrice.Text Then
            txtTrtPrice.Text = cleanedText.ToString()
            ' Restore cursor position (adjusting for removed characters)
            txtTrtPrice.SelectionStart = Math.Min(cursorPos, txtTrtPrice.Text.Length)
        End If
    End Sub
    Private Sub isPaidChck_CheckedChanged(sender As Object, e As EventArgs) Handles isPaidChck.CheckedChanged
        If loaded = True Then
            If isPaidChck.Checked = True Then
                paid = True
            Else
                paid = False
            End If
        End If
    End Sub
    Private Sub IsExternalchk_CheckedChanged(sender As Object, e As EventArgs) Handles IsExternalchk.CheckedChanged
        If loaded = True Then
            If IsExternalchk.Checked = True Then
                external = True
            Else
                external = False
            End If
        End If
    End Sub
    Private currentTooth As Patient_Diagnosis = Nothing
    Private Sub TrtBS_PositionChanged(sender As Object, e As EventArgs) Handles TrtBS.PositionChanged
        If TrtBS.Count = 0 Then Exit Sub
        currentTooth = CType(TrtBS.Current, Patient_Diagnosis)
        clrFillColor.Color = ColorTranslator.FromHtml(currentTooth.FillColor)
        clrBorderColor.Color = ColorTranslator.FromHtml(currentTooth.BorderColor)
    End Sub

    Private Sub impCheck_CheckedChanged(sender As Object, e As EventArgs) Handles impCheck.CheckedChanged
        ImpPopup.Visible = impCheck.Checked ' _propName.Contains("IMPLANT")
        ImplantSpecsLbl.Visible = impCheck.Checked ' _propName.Contains("IMPLANT")
    End Sub


    Private Sub txtTrtPrice_GotFocus(sender As Object, e As EventArgs) Handles txtTrtPrice.GotFocus
        txtTrtPrice.SelectAll()
    End Sub
    Private Sub txtPayValue_GotFocus(sender As Object, e As EventArgs) Handles txtPayValue.GotFocus
        txtPayValue.SelectAll()
    End Sub

    Private Sub txtPayValue_Click(sender As Object, e As EventArgs) Handles txtPayValue.Click
        txtPayValue.SelectAll()
    End Sub

    Private Sub txtTrtPrice_Click(sender As Object, e As EventArgs) Handles txtTrtPrice.Click
        txtTrtPrice.SelectAll()
    End Sub
End Class
