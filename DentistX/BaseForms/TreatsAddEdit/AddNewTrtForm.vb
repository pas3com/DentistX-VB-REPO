Imports System.ComponentModel
Imports System.Data.SqlClient
Imports System.Drawing
Imports System.Globalization
Imports System.Windows.Forms
Imports Dapper
Imports DevExpress.CodeParser
Imports DevExpress.XtraEditors.Controls
Imports DevExpress.XtraGrid.Views.Grid

Public Class AddNewTrtForm
    ''' <summary>Stored in DB as English; UI may show Arabic when Eng is False.</summary>
    Private Const TrtTypeOneStageEn As String = "One Stage"
    Private Const TrtTypeMultipleStageEn As String = "Multiple Stages"
    Private Const TrtTypeOneStageAr As String = "مرحلة واحدة"
    Private Const TrtTypeMultipleStageAr As String = "عدة مراحل"

    Private _applyingTreatmentTypeIndex As Boolean

    Private selectedToothTrtList As New List(Of Patient_ToothTrt)
    ''' <summary>Patient passed from parent (e.g. AdultJaw); used when updating balance after save.</summary>
    Private _currentPatient As Patient
    Private selectedTeethList As New List(Of Byte)
    Dim _shapeId As Integer = 0
    Dim _propName As String = ""
    Dim toothTrtData As New Patient_ToothTrtDATA
    Dim _toothTrt As New Patient_ToothTrt
    Dim external As Boolean = False
    Dim paid As Boolean = False
    Dim loaded As Boolean = False

    Dim specialTrts As String() = {"INDIRECT PULP CAPPING", "DIRECT PULP CAPPING", "PULPOTOMY"}

    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()
        isPaidChck.Checked = False
        IsExternalchk.Checked = False
        loaded = False
        Me.Icon = AppIcon
        Me.txtTrtPrice.Focus()
        Me.txtTrtPrice.SelectAll()
    End Sub
    Public Sub New(ByVal clsToothTrt As List(Of Patient_ToothTrt), ByVal clsPatient As Patient)
        ' This call is required by the designer.
        InitializeComponent()
        Me.Icon = AppIcon
        If clsToothTrt Is Nothing OrElse clsToothTrt.Count = 0 OrElse clsPatient Is Nothing Then
            loaded = False
            Return
        End If
        loaded = False
        isPaidChck.Checked = False
        IsExternalchk.Checked = False
        _currentPatient = clsPatient
        selectedToothTrtList = clsToothTrt
        _toothTrt = clsToothTrt(0)
        txtPatientID.Text = clsToothTrt(0).PatientID
        txtPatientName.Text = clsPatient.PatientName
        txtToothNum.Text = clsToothTrt(0).ToothNum
        txtToothName.Text = GetToothFullName(clsToothTrt(0).ToothName)
        clrBorderColor.Color = ColorTranslator.FromHtml(clsToothTrt(0).BorderColor)
        CapFillPick.Color = ColorTranslator.FromHtml(clsToothTrt(0).CapFill)
        RootFillPick.Color = ColorTranslator.FromHtml(clsToothTrt(0).RootFill)
        tbThick.Value = 0
        txtTreat.Text = clsToothTrt(0).Treat
        clrFillColor.Color = GetCustomTrtColor(txtTreat.Text) 'ColorTranslator.FromHtml(clsToothTrt(0).FillColor)
        _propName = clsToothTrt(0).PropertyName
        _shapeId = clsToothTrt(0).ShapeID
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
        impCheck.Visible = clsToothTrt(0).PropertyName.Contains("IMPLANT")
        loaded = True
        grpSpecialClrs.Visible = specialTrts.Contains(txtTreat.Text.ToUpper())
        TrtBS.DataSource = clsToothTrt.ToList
        Me.txtTrtPrice.Focus()
        Me.txtTrtPrice.SelectAll()
    End Sub
    Public Sub New(ByVal clsToothTrt As Patient_ToothTrt, ByVal clsPatient As Patient)
        InitializeComponent()
        Me.Icon = AppIcon
        If clsToothTrt Is Nothing OrElse clsPatient Is Nothing Then
            loaded = False
            Return
        End If
        loaded = False
        isPaidChck.Checked = False
        IsExternalchk.Checked = False
        _currentPatient = clsPatient
        _toothTrt = clsToothTrt
        txtPatientID.Text = clsToothTrt.PatientID
        txtPatientName.Text = clsPatient.PatientName
        txtToothNum.Text = clsToothTrt.ToothNum
        txtToothName.Text = GetToothFullName(clsToothTrt.ToothName)
        clrFillColor.Color = GetCustomTrtColor(clsToothTrt.Treat) ' ColorTranslator.FromHtml(clsToothTrt.FillColor)
        clrBorderColor.Color = ColorTranslator.FromHtml(clsToothTrt.BorderColor)
        CapFillPick.Color = ColorTranslator.FromHtml(clsToothTrt.CapFill)
        RootFillPick.Color = ColorTranslator.FromHtml(clsToothTrt.RootFill)
        tbThick.Value = 0
        txtTreat.Text = clsToothTrt.Treat
        _propName = clsToothTrt.PropertyName
        _shapeId = clsToothTrt.ShapeID
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
        If clsToothTrt.BorderColor IsNot Nothing AndAlso clsToothTrt.BorderColor.Length = 8 Then
            SetBrdrClrFromHex(clsToothTrt.FillColor)
        End If
        txtExtClinic.Text = clsToothTrt.ExternalClinicName
        impCheck.Visible = clsToothTrt.PropertyName.Contains("IMPLANT")
        grpSpecialClrs.Visible = specialTrts.Contains(txtTreat.Text.ToUpper())

        TrtBS.DataSource = clsToothTrt
        Me.txtTrtPrice.Focus()
        Me.txtTrtPrice.SelectAll()
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
        WireTreatEndAndFinishedBindings()
        If TrtBS.DataSource IsNot Nothing AndAlso TrtBS.Count > 0 Then
            TrtBS.Position = 0
        End If
        ' Single-tooth add: default today. Multi-tooth: each row keeps its own TreatDate via binding.
        If selectedToothTrtList Is Nothing OrElse selectedToothTrtList.Count = 0 Then
            dtTrtDate.DateTime = Date.Now
        End If
        ConfigureTreatmentTypeCombo()
        Dim row As Patient_ToothTrt = TryCast(TrtBS.Current, Patient_ToothTrt)
        If row Is Nothing AndAlso TrtBS.Count > 0 Then row = TryCast(TrtBS.Item(0), Patient_ToothTrt)
        ApplyTreatmentTypeComboFromRow(row)

        IntegerMoneyEditorFocus.ConfigureIntegerMoneyTextEdit(txtPayValue)
        IntegerMoneyEditorFocus.AttachTextEditZeroEmptyElseSelectAll(txtPayValue)
        IntegerMoneyEditorFocus.ConfigureIntegerMoneyTextEdit(txtTrtPrice)
        IntegerMoneyEditorFocus.AttachTextEditZeroEmptyElseSelectAll(txtTrtPrice)
        IntegerMoneyEditorFocus.WireIntegerMoneyFieldBinding(txtTrtPrice, TrtBS, "TrtValue")
        IntegerMoneyEditorFocus.WireIntegerMoneyFieldBinding(txtPayValue, TrtBS, "PayValue")
        Me.txtTrtPrice.Focus()
        Me.txtTrtPrice.SelectAll()
    End Sub

    Private Sub NewTrtAddCTL_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        Dim row As Patient_ToothTrt = TryCast(TrtBS.Current, Patient_ToothTrt)
        If row Is Nothing AndAlso TrtBS.Count > 0 Then row = TryCast(TrtBS.Item(0), Patient_ToothTrt)
        ApplyTreatmentTypeComboFromRow(row)
        BeginInvoke(New MethodInvoker(AddressOf ReapplyTreatmentTypeComboDeferred))
    End Sub

    Private Sub ReapplyTreatmentTypeComboDeferred()
        Dim row As Patient_ToothTrt = TryCast(TrtBS.Current, Patient_ToothTrt)
        If row Is Nothing AndAlso TrtBS.Count > 0 Then row = TryCast(TrtBS.Item(0), Patient_ToothTrt)
        ApplyTreatmentTypeComboFromRow(row)
    End Sub

    Private Sub WireTreatEndAndFinishedBindings()
        If dtTrtEnd.DataBindings.Count = 0 Then
            dtTrtEnd.DataBindings.Add(New Binding("EditValue", TrtBS, "TreatEndDate", True, DataSourceUpdateMode.OnPropertyChanged))
        End If
        If ceFinish.DataBindings.Count = 0 Then
            Dim b As New Binding("EditValue", TrtBS, "Finished", True, DataSourceUpdateMode.OnPropertyChanged)
            AddHandler b.Format, AddressOf FinishedBinding_Format
            AddHandler b.Parse, AddressOf FinishedBinding_Parse
            ceFinish.DataBindings.Add(b)
        End If
    End Sub

    Private Sub FinishedBinding_Format(sender As Object, e As ConvertEventArgs)
        If e.Value Is Nothing OrElse e.Value Is DBNull.Value Then
            e.Value = False
            Return
        End If
        Try
            e.Value = (Convert.ToByte(e.Value) = 1)
        Catch
            e.Value = False
        End Try
    End Sub

    Private Sub FinishedBinding_Parse(sender As Object, e As ConvertEventArgs)
        Dim checked As Boolean = False
        If e.Value IsNot Nothing AndAlso TypeOf e.Value Is Boolean Then checked = DirectCast(e.Value, Boolean)
        e.Value = If(checked, CByte(1), CByte(0))
    End Sub

    Private Sub ConfigureTreatmentTypeCombo()
        cboTrtType.Properties.Items.Clear()
        If Eng Then
            cboTrtType.Properties.Items.AddRange({TrtTypeOneStageEn, TrtTypeMultipleStageEn})
        Else
            cboTrtType.Properties.Items.AddRange({TrtTypeOneStageAr, TrtTypeMultipleStageAr})
        End If
    End Sub

    Private Sub ApplyTreatmentTypeComboFromRow(row As Patient_ToothTrt)
        If row Is Nothing Then
            If cboTrtType.Properties.Items.Count >= 1 Then
                _applyingTreatmentTypeIndex = True
                Try
                    cboTrtType.SelectedIndex = 0
                    cboTrtType.EditValue = cboTrtType.Properties.Items(0)
                Finally
                    _applyingTreatmentTypeIndex = False
                End Try
            End If
            Return
        End If
        If String.IsNullOrWhiteSpace(row.TreatmentType) Then
            _applyingTreatmentTypeIndex = True
            Try
                cboTrtType.EditValue = Nothing
                cboTrtType.SelectedIndex = -1
            Finally
                _applyingTreatmentTypeIndex = False
            End Try
            Return
        End If

        Dim en = NormalizeTreatmentTypeToEnglish(row.TreatmentType)
        row.TreatmentType = en
        If cboTrtType.Properties.Items.Count < 2 Then Return

        _applyingTreatmentTypeIndex = True
        Try
            cboTrtType.Properties.BeginUpdate()
            Try
                If String.Equals(en, TrtTypeOneStageEn, StringComparison.OrdinalIgnoreCase) Then
                    cboTrtType.SelectedIndex = 0
                    cboTrtType.EditValue = cboTrtType.Properties.Items(0)
                ElseIf String.Equals(en, TrtTypeMultipleStageEn, StringComparison.OrdinalIgnoreCase) Then
                    cboTrtType.SelectedIndex = 1
                    cboTrtType.EditValue = cboTrtType.Properties.Items(1)
                Else
                    cboTrtType.EditValue = Nothing
                    cboTrtType.SelectedIndex = -1
                End If
            Finally
                cboTrtType.Properties.EndUpdate()
            End Try
        Finally
            _applyingTreatmentTypeIndex = False
            Me.txtTrtPrice.Focus()
            Me.txtTrtPrice.SelectAll()
        End Try
    End Sub

    Private Sub cboTrtType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboTrtType.SelectedIndexChanged
        If _applyingTreatmentTypeIndex Then Return
        FlushTreatmentTypeEnglishFromComboToRow()
    End Sub

    Private Sub FlushTreatmentTypeEnglishFromComboToRow()
        Dim row As Patient_ToothTrt = TryCast(TrtBS.Current, Patient_ToothTrt)
        If row Is Nothing AndAlso TrtBS.Count > 0 Then row = TryCast(TrtBS.Item(0), Patient_ToothTrt)
        If row Is Nothing Then Return
        row.TreatmentType = NormalizeTreatmentTypeToEnglish(ComboStageDisplayString())
    End Sub

    Private Function ComboStageDisplayString() As String
        If cboTrtType.EditValue IsNot Nothing Then
            Dim s = cboTrtType.EditValue.ToString()
            If Not String.IsNullOrWhiteSpace(s) Then Return s.Trim()
        End If
        Return If(cboTrtType.Text, "").Trim()
    End Function

    Private Function CurrentTreatmentTypeEnglishFromCombo() As String
        Return NormalizeTreatmentTypeToEnglish(ComboStageDisplayString())
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

    Private Function TreatmentTypeEnglishForSave() As String
        FlushTreatmentTypeEnglishFromComboToRow()
        Dim cur As Patient_ToothTrt = TryCast(TrtBS.Current, Patient_ToothTrt)
        Return NormalizeTreatmentTypeToEnglish(
            If(cur IsNot Nothing AndAlso Not String.IsNullOrWhiteSpace(cur.TreatmentType), cur.TreatmentType, CurrentTreatmentTypeEnglishFromCombo()))
    End Function

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
        Dim treatmentTypeEnglish = TreatmentTypeEnglishForSave()

        Dim titleEn As String = "Field Required"
        Dim titleAr As String = "حقل مطلوب"
        Dim title As String = If(Eng, titleEn, titleAr)

        If String.IsNullOrWhiteSpace(treatmentTypeEnglish) OrElse
           (Not String.Equals(treatmentTypeEnglish, TrtTypeOneStageEn, StringComparison.OrdinalIgnoreCase) AndAlso
            Not String.Equals(treatmentTypeEnglish, TrtTypeMultipleStageEn, StringComparison.OrdinalIgnoreCase)) Then
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
        Dim clinicName As String = "In House"
        If IsExternalchk.Checked = True Then
            clinicName = txtExtClinic.Text
        End If
        If txtExtClinic.Text.Length < 1 Then
            clinicName = "Somewhere Else"
        End If
        If impCheck.Checked Then '_propName.Contains("IMPLANT") Then
            If Not ImplantTreatBracketHelper.ImplantSpecsPresentInTreat(txtTreat.Text) Then
                Dim msgEn As String = "IMPLANT Specs is missing."
                Dim msgAr As String = "مواصفات الزرعة مفقودة."
                Dim msg As String = If(Eng, msgEn, msgAr)
                MessageBox.Show(msg, title, MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return False
            End If
        End If
        Dim hexFillWithAlpha As String = $"#{fillClrWithOpacity.A:X2}{fillClrWithOpacity.R:X2}{fillClrWithOpacity.G:X2}{fillClrWithOpacity.B:X2}"
        Dim hexBrdrWithAlpha As String = $"#{brdrClrWithOpacity.A:X2}{brdrClrWithOpacity.R:X2}{brdrClrWithOpacity.G:X2}{brdrClrWithOpacity.B:X2}"
        Dim hexCapFillWithAlpha As String = $"#{capFill.A:X2}{capFill.R:X2}{capFill.G:X2}{capFill.B:X2}"
        Dim hexRootWithAlpha As String = $"#{rootFill.A:X2}{rootFill.R:X2}{rootFill.G:X2}{rootFill.B:X2}"

        _toothTrt.BorderColor = hexBrdrWithAlpha ' ColorTranslator.ToHtml(clrBorderColor.Color) #7F9B9A9A
        _toothTrt.BorderThickness = CByte(tbThick.Value)
        _toothTrt.FillColor = hexFillWithAlpha ' ColorTranslator.ToHtml(clrFillColor.Color) ' hexWithAlpha ' 
        _toothTrt.CapFill = hexCapFillWithAlpha
        _toothTrt.RootFill = hexRootWithAlpha
        _toothTrt.IsPaid = paid
        _toothTrt.IsExternal = external
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
        _toothTrt.TreatmentType = treatmentTypeEnglish
        _toothTrt.TreatNotes = txtTrtNotes.Text
        _toothTrt.Finished = If(ceFinish.Checked, 1, 0)
        If ceFinish.Checked Then
            _toothTrt.TreatEndDate = dtTrtEnd.DateTime
        Else
            _toothTrt.TreatEndDate = Nothing
        End If
        Return True
    End Function
    Private Function SetToothTrt(ByVal trtClas As Patient_ToothTrt) As Boolean

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

        Dim treatmentTypeEnglish = NormalizeTreatmentTypeToEnglish(ComboStageDisplayString())

        If String.IsNullOrWhiteSpace(treatmentTypeEnglish) OrElse
           (Not String.Equals(treatmentTypeEnglish, TrtTypeOneStageEn, StringComparison.OrdinalIgnoreCase) AndAlso
            Not String.Equals(treatmentTypeEnglish, TrtTypeMultipleStageEn, StringComparison.OrdinalIgnoreCase)) Then
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

        Dim clinicName As String = "In House"
        If IsExternalchk.Checked = True Then
            clinicName = txtExtClinic.Text
        End If
        If txtExtClinic.Text.Length < 1 Then
            clinicName = "Somewhere Else"
        End If
        If impCheck.Checked Then ' _propName.Contains("IMPLANT") Then
            If Not ImplantTreatBracketHelper.ImplantSpecsPresentInTreat(txtTreat.Text) Then
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
        Dim hexCapFillWithAlpha As String = $"#{capFill.A:X2}{capFill.R:X2}{capFill.G:X2}{capFill.B:X2}"
        Dim hexRootWithAlpha As String = $"#{rootFill.A:X2}{rootFill.R:X2}{rootFill.G:X2}{rootFill.B:X2}"

        trtClas.BorderColor = hexBrdrWithAlpha ' ColorTranslator.ToHtml(clrBorderColor.Color)
        trtClas.BorderThickness = CByte(tbThick.Value)
        trtClas.FillColor = hexFillWithAlpha ' ColorTranslator.ToHtml(clrFillColor.Color) '
        _toothTrt.IsPaid = paid
        _toothTrt.IsExternal = external
        trtClas.PatientID = PatientID
        trtClas.ShapeID = _shapeId
        trtClas.PropertyName = _propName
        trtClas.ToothName = txtToothName.Text 'GetToothFullName(clsToothTrt.ToothName) ' 
        trtClas.ToothNum = txtToothNum.Text
        trtClas.TreatDate = dtTrtDate.DateTime
        'trtClas.TreatPlan = txtTrtPlan.Text
        trtClas.Treat = txtTreat.Text
        If _propName.Contains("IMPLANT") Then
            trtClas.LVL = GetLVL("IMPLANT")
        Else
            trtClas.LVL = GetLVL(txtTreat.Text)
        End If

        trtClas.TreatDetails = txtTrtDetails.Text
        trtClas.TreatmentType = treatmentTypeEnglish
        trtClas.TreatNotes = txtTrtNotes.Text
        trtClas.Finished = If(ceFinish.Checked, 1, 0)
        If ceFinish.Checked Then
            trtClas.TreatEndDate = dtTrtEnd.DateTime
        Else
            trtClas.TreatEndDate = Nothing
        End If
        Return True


    End Function

    Private Function SetBsTrt(ByVal trtClas As Patient_ToothTrt) As Boolean

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

        Dim treatmentTypeEnglishBs = NormalizeTreatmentTypeToEnglish(ComboStageDisplayString())

        If String.IsNullOrWhiteSpace(treatmentTypeEnglishBs) OrElse
           (Not String.Equals(treatmentTypeEnglishBs, TrtTypeOneStageEn, StringComparison.OrdinalIgnoreCase) AndAlso
            Not String.Equals(treatmentTypeEnglishBs, TrtTypeMultipleStageEn, StringComparison.OrdinalIgnoreCase)) Then
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

        Dim clinicName As String = "In House"
        If IsExternalchk.Checked = True Then
            clinicName = txtExtClinic.Text
        End If
        If txtExtClinic.Text.Length < 1 Then
            clinicName = "Somewhere Else"
        End If
        If impCheck.Checked Then
            If Not ImplantTreatBracketHelper.ImplantSpecsPresentInTreat(txtTreat.Text) Then
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
        _toothTrt.IsPaid = paid
        _toothTrt.IsExternal = external
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
        trtClas.TreatmentType = treatmentTypeEnglishBs
        trtClas.TreatNotes = txtTrtNotes.Text
        trtClas.Finished = If(ceFinish.Checked, 1, 0)
        If ceFinish.Checked Then
            trtClas.TreatEndDate = dtTrtEnd.DateTime
        Else
            trtClas.TreatEndDate = Nothing
        End If
        Return True


    End Function





#Region "New Save Code"
    Private toothNumsInt As New List(Of Integer)
    Private toothNums As New List(Of String)
    Dim OnePayAdded As Boolean = False

    Private Shared Function IsCompleteDentureTreatmentType(treat As String) As Boolean
        If String.IsNullOrWhiteSpace(treat) Then Return False
        Select Case treat.Trim().ToUpperInvariant()
            Case "COMPLETE DENTURE", "CD"
                Return True
            Case Else
                Return False
        End Select
    End Function

    Private Function FormatCompleteDentureArchSummary() As String
        Return TrtSourceHelper.FormatAccountingArchSummaryFromFdi(toothNumsInt, Eng)
    End Function

    Private Sub btnSaveTrt_Click(sender As Object, e As EventArgs) Handles btnSaveTrt.Click
        ' Ensure current row edits are pushed into the bound list
        TrtBS.EndEdit()

        Dim treatmentText, toothName As String
        Dim clsPatientTrtsData As New Patient_TrtsDATA
        ' Determine if this is a multi-tooth treatment (bridges/dentures or empty-jaw "other" arch treats on 2+ teeth)
        Dim isMultiTrt As Boolean = IsMultiToothTreatment(_toothTrt.Treat) OrElse
            (selectedToothTrtList.Count > 1 AndAlso TrtSourceHelper.IsArchGroupedOtherTreat(_toothTrt.Treat))
        If selectedToothTrtList.Count <= 1 Then
            isMultiTrt = False
        End If
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
                Dim trtPrice As Double = Val(txtTrtPrice.Text)
                Dim payValue As Double = Val(txtPayValue.Text)
                Dim oldTrt As String = If(_propName.Contains("IMPLANT"), "IMPLANT", GetOldTrt(_toothTrt.Treat))
                _toothTrt.PayValue = payValue
                _toothTrt.TrtValue = trtPrice

                Select Case _toothTrt.Treat
                    Case "ABUTMENT", "HEALING CAP"
                        Saved = SaveTreatmentWithAbutment(_toothTrt)
                    Case Else
                        Saved = SaveTreatmentWithTransaction(PatientID, _toothTrt, treatmentText, toothName, isMultiTrt, treatmentGroupID)
                End Select
            End If
        Else
            ' Multi-tooth treatment

            ' Multi-tooth treatment

            ' 1) Validate form inputs ONCE (copy the checks from SetToothTrt, but do not assign to _toothTrt)
            Dim titleEn As String = "Field Required"
            Dim titleAr As String = "حقل مطلوب"
            Dim title As String = If(Eng, titleEn, titleAr)

            Dim sharedTreatmentTypeEn = NormalizeTreatmentTypeToEnglish(ComboStageDisplayString())
            If String.IsNullOrWhiteSpace(sharedTreatmentTypeEn) OrElse
               (Not String.Equals(sharedTreatmentTypeEn, TrtTypeOneStageEn, StringComparison.OrdinalIgnoreCase) AndAlso
                Not String.Equals(sharedTreatmentTypeEn, TrtTypeMultipleStageEn, StringComparison.OrdinalIgnoreCase)) Then
                MessageBox.Show(If(Eng, "Select Treatment Type.", "اختر نوع العلاج."), title, MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If
            If Not Module1.isKid AndAlso IsCompleteDentureTreatmentType(txtTreat.Text.Trim()) Then
                Dim fdiNums = selectedToothTrtList.Select(Function(t) CInt(t.ToothNum)).ToList()
                If Not JawTreatmentTreeHelper.IsAdultFullArchCompleteDentureSelection(fdiNums) Then
                    MessageBox.Show(If(Eng, "Complete denture requires exactly 16 teeth on one arch (upper FDI 11–28 or lower 31–48).",
                                         "الطقم الكامل يتطلب 16 سناً بالضبط في فك واحد (علوي 11-28 أو سفلي 31-48)."),
                                   title, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End If
            End If
            For Each trtValidate As Patient_ToothTrt In selectedToothTrtList
                If Not trtValidate.TreatDate.HasValue Then
                    MessageBox.Show(If(Eng, "Enter Treatment Date for every tooth (use the navigator).", "أدخل تاريخ العلاج لكل سن (استخدم أزرار التنقل)."), title, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End If
                If trtValidate.Finished.GetValueOrDefault() = 1 AndAlso Not trtValidate.TreatEndDate.HasValue Then
                    MessageBox.Show(If(Eng, "Enter Treatment End Date for every finished tooth.", "أدخل تاريخ انتهاء العلاج لكل سن مكتمل."), title, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End If
            Next
            If txtTreat.Text.Length = 0 Then
                MessageBox.Show(If(Eng, "Enter Treatment.", "أدخل العلاج."), title, MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If
            If txtPatientID.Text.Length = 0 OrElse txtPatientName.Text.Length = 0 Then
                MessageBox.Show(If(Eng, "Patient data is missing.", "بيانات المريض مفقودة."), title, MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If
            If txtTreat.Text.Contains("IMPLANT") Then
                If GetShapeIDByTrt("IMPLANT") < 1 Then
                    MessageBox.Show(If(Eng, "The selected Treat Has No Shape.", "العلاج المحدد ليس له شكل."), title, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End If
            Else
                If GetShapeIDByTrt(txtTreat.Text) < 1 Then
                    MessageBox.Show(If(Eng, "The selected Treat Has No Shape.", "العلاج المحدد ليس له شكل."), title, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End If
            End If
            If impCheck.Checked Then
                For Each trtImpl As Patient_ToothTrt In selectedToothTrtList
                    If trtImpl.Treat IsNot Nothing AndAlso trtImpl.Treat.ToUpperInvariant().Contains("IMPLANT") AndAlso Not ImplantTreatBracketHelper.ImplantSpecsPresentInTreat(trtImpl.Treat) Then
                        MessageBox.Show(If(Eng, "IMPLANT Specs is missing for one or more teeth. Use the navigator to set specs on each tooth.", "مواصفات الزرعة مفقودة لسن واحد أو أكثر. استخدم التنقل لضبط المواصفات لكل سن."), title, MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Exit Sub
                    End If
                Next
            End If

            ' 2) Build list of teeth for accounting text
            Dim firstToothInGroup As Boolean = True
            toothNums.Clear()
            toothNumsInt.Clear()
            For Each trtCls In selectedToothTrtList
                toothNums.Add(GetShortToothNameWithDash(trtCls.ToothNum))
                toothNumsInt.Add(CInt(trtCls.ToothNum))
            Next

            ' 3) Save each tooth, using its own properties from the binding source (incl. per-tooth TreatDate)
            For Each trtCls In selectedToothTrtList
                ' Only mark grouping; do not overwrite per-tooth properties like colors, LVL, etc.
                trtCls.IsMultiTrt = isMultiTrt
                trtCls.TrtGroupID = treatmentGroupID
                trtCls.TreatmentType = sharedTreatmentTypeEn

                ' Ensure LVL is set in case it was left at 0 (similar to SaveTreatHelper)
                If _propName.Contains("IMPLANT") Then
                    trtCls.LVL = GetLVL("IMPLANT")
                ElseIf trtCls.LVL = 0 Then
                    trtCls.LVL = GetLVL(trtCls.Treat)
                End If

                _toothTrt = trtCls
                treatmentText = trtCls.Treat
                toothName = trtCls.ToothName

                Dim oldTrt As String = If(_propName.Contains("IMPLANT"), "IMPLANT", GetOldTrt(trtCls.Treat))

                Select Case _toothTrt.Treat
                    Case "ABUTMENT", "HEALING CAP"
                        Saved = SaveTreatmentWithAbutment(_toothTrt)
                    Case Else
                        Saved = SaveTreatmentWithTransaction(PatientID, _toothTrt, treatmentText, toothName,
                                                             isMultiTrt, treatmentGroupID, firstToothInGroup)
                End Select

                firstToothInGroup = False   ' Only first tooth gets accounting record
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
            Return conn.ExecuteScalar(Of Integer)("SELECT TOP 1 ToothTrtID FROM Patient_ToothTrt ORDER BY ToothTrtID DESC")
        End Using
    End Function

    Public Function SaveTreatmentWithAbutment(_toothTrt As Patient_ToothTrt) As Boolean
        Dim saved As Boolean = False
        Dim canceled As Boolean = False
        Dim clsTrtsData As New Patient_TrtsDATA
        Dim toothTrtData As New Patient_ToothTrtDATA
        Dim Dx = New DentistXDATA
        Dim ConnectionString = Dx.GetConnectionString
        Return toothTrtData.AddNormal(_toothTrt)
    End Function
    Public Function SaveTreatmentWithTransaction(PatientID As Integer, _toothTrt As Patient_ToothTrt,
                                      treatmentText As String, toothName As String,
                                      Optional isMultiTooth As Boolean = False,
                                      Optional treatmentGroupID As Guid = Nothing,
                                      Optional isFirstInGroup As Boolean = True) As Boolean
        Dim saved As Boolean = False
        Dim canceled As Boolean = False
        Dim clsTrtsData As New Patient_TrtsDATA
        Dim toothTrtData As New Patient_ToothTrtDATA
        Dim Dx = New DentistXDATA
        Dim ConnectionString = Dx.GetConnectionString
        Dim oldTrt As String = If(_propName.Contains("IMPLANT"), "IMPLANT", GetOldTrt(_toothTrt.Treat))
        Dim userID As Integer = CurrentUser.UsID
        _toothTrt.UserID = userID
        Using conn As New SqlConnection(ConnectionString)
            conn.Open()
            Using trans As SqlTransaction = conn.BeginTransaction()
                Dim committed As Boolean = False
                Try
                    '' Set common properties AddTreatFrmChartToGrids_Transactional
                    If Not TrtSourceHelper.IsWholeMouthChartlessSave(_toothTrt.Treat, _toothTrt.ToothNum) Then
                        AddTreatFrmChartToGrids_Transactional(conn, trans, PatientID, _toothTrt.ToothNum, _toothTrt.ToothName, oldTrt)
                    End If
                    _toothTrt.QrtrTable = ""
                    _toothTrt.QrtrID = 0
                    _toothTrt.QrtrAddress = 0
                    _toothTrt.QrtrColumnName = ""
                    _toothTrt.QrtrColumnValue = ""
                    If isMultiTooth Then
                        _toothTrt.ParentToothTrtID = -1
                        _toothTrt.IsMultiTrt = False
                        _toothTrt.TrtGroupID = treatmentGroupID
                    End If
                    ' Treatment value and payment
                    Dim trtValue As Double = _toothTrt.TrtValue
                    Dim payValue As Double = _toothTrt.PayValue
                    ' Payment logic (per-tooth, using stored values)
                    Dim payNote As String = ""
                    If payValue > 0 OrElse trtValue > 0 Then
                        If payValue = trtValue Then
                            payNote = "Payed In Full"
                            isPaidChck.Checked = True
                        ElseIf payValue < trtValue AndAlso payValue > 0 Then
                            payNote = "Payed Partially"
                            isPaidChck.CheckState = True
                        ElseIf payValue > trtValue Then
                            payNote = "Payed With Advance Payment"
                            isPaidChck.Checked = True
                        End If
                    ElseIf trtValue = 0 AndAlso payValue = 0 Then
                        isPaidChck.Checked = False
                    End If
                    ' Save Patient_ToothTrt
                    _toothTrt.IsPaid = isPaidChck.Checked
                    If _toothTrt.IsExternal = True Then _toothTrt.IsPaid = True
                    'A new Try
                    '======================
                    'its here where i'm stuck
                    If Not TrtSourceHelper.IsWholeMouthChartlessSave(_toothTrt.Treat, _toothTrt.ToothNum) Then
                        Dim MaxLvl As Integer = toothTrtData.GetTreatLVL(_toothTrt.PatientID, _toothTrt.ToothNum)
                        Dim currentLvl As Integer = _toothTrt.LVL
                        'check if treat is a normal one after high level one
                        If (MaxLvl > 4 AndAlso currentLvl < 4) AndAlso Not TrtSourceHelper.AllowLowLevelTreatOnChartDespiteHighMaxLevel(_toothTrt.PatientID, _toothTrt.ToothNum, _toothTrt.Treat, False) Then
                            Dim msgEng As String = "You Cant Add a Normal Treat On High Level Treat...."
                            Dim msgAr As String = "لا يمكنك إضافة علاج عادي على علاج عالي المستوى...."
                            Dim msg As String = If(Eng, msgEng, msgAr)
                            MsgBox(msg)
                            Return False
                        End If
                        ' Special case: implant (5) → extraction (4) or extraction (4) → implant (5)
                        If (MaxLvl > 4 AndAlso currentLvl = 4) Then ' OrElse (MaxLvl = 4 AndAlso currentLvl = 5) Then
                            ' 1. Finish old treatment
                            conn.Execute("
                                            UPDATE Patient_ToothTrt
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
                    End If
                    '============================
                    'normal
                    If Not toothTrtData.AddTransactional(conn, trans, _toothTrt) Then
                        Dim msgEng As String = $"Failed to save Treatment in Chart '{treatmentText}' for '{toothName}'."
                        Dim msgAr As String = $"فشل في حفظ العلاج في السجل  '{treatmentText}' ل '{toothName}'."
                        Dim msg As String = If(Eng, msgEng, msgAr)
                        MessageBox.Show(msg)
                    End If
                    ' Get the last inserted ToothTrtID
                    Dim lastToothTrtID As Integer = conn.ExecuteScalar(Of Integer)(
                            "SELECT TOP 1 ToothTrtID FROM Patient_ToothTrt ORDER BY ToothTrtID DESC",
                            transaction:=trans)
                    'If IsMultiTrt then update ParentToothTrtID and insert into Patient_TrtInfo
                    If isMultiTooth Then
                        'Update ParentToothTrtID
                        Dim masterToothID As Integer = conn.ExecuteScalar(Of Integer)(
                                                "SELECT MIN(ToothTrtID) FROM Patient_ToothTrt 
                                                 WHERE TrtGroupID = @TrtGroupID",
                                                New With {.TrtGroupID = treatmentGroupID},
                                                transaction:=trans)
                        ' Update all other teeth in group to point to first tooth
                        Dim rowsUpdated As Integer = conn.ExecuteScalar(
                                                "UPDATE Patient_ToothTrt 
                                                 SET ParentToothTrtID = @ParentToothTrtID 
                                                 WHERE TrtGroupID = @TrtGroupID 
                                                ",'  AND ToothTrtID <> @ParentToothTrtID
                                                New With {
                                                    .ParentToothTrtID = masterToothID,
                                                    .TrtGroupID = treatmentGroupID
                                                },
                                                transaction:=trans)
                        ' Insert the record into Patient_TrtInfo
                        Dim Trt As Patient_ToothTrt = toothTrtData.Select_Record(New Patient_ToothTrt With {
                                                                                                    .ToothTrtID = lastToothTrtID,
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
                        End If
                        ' Insert with all required parameters
                        Dim rowsInserted As Integer = conn.ExecuteScalar(
                                                                            "INSERT INTO [dbo].[Patient_TrtInfo] (
                                                                            [PatientID], [ParentToothTrtID], [TrtGroupID], 
                                                                            [ToothNum], [ToothName], [TreatDate], [Treat],
                                                                            [TreatNotes], [IsExternal], [ExternalClinicName], 
                                                                            [ExternalTreatmentDate]
                                                                        ) VALUES (
                                                                            @PatientID, @ParentToothTrtID, @TrtGroupID, 
                                                                            @ToothNum, @ToothName, @TreatDate, @Treat, 
                                                                            @TreatNotes, @IsExternal, @ExternalClinicName, 
                                                                            @ExternalTreatmentDate
                                                                        )",
                                                                        New With {
                                                                            .PatientID = PatientID,
                                                                            .ParentToothTrtID = If(masterToothID > 0, masterToothID, DBNull.Value),
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
                                If IsCompleteDentureTreatmentType(_toothTrt.Treat) Then
                                    detailText = _toothTrt.Treat.Trim() & " ==>> " & FormatCompleteDentureArchSummary()
                                ElseIf TrtSourceHelper.IsArchGroupedOtherTreat(_toothTrt.Treat) Then
                                    detailText = _toothTrt.Treat.Trim() & " ==>> " & TrtSourceHelper.FormatAccountingArchSummaryFromFdi(toothNumsInt, Eng)
                                Else
                                    detailText = _toothTrt.Treat & " ==>> " & teeth
                                End If
                            ElseIf TrtSourceHelper.IsWholeMouthChartlessSave(_toothTrt.Treat, _toothTrt.ToothNum) Then
                                detailText = _toothTrt.Treat.Trim() & " ==>> " & _toothTrt.ToothName.Trim()
                            Else
                                detailText = FormatPatientTrtsDetail(_toothTrt.Treat, _toothTrt.ToothNum)
                            End If
                            Dim clsPatientTrts As New Patient_Trts With {
                            .ToothTrtID = lastToothTrtID,
                            .OrthoID = 0,  ' This is now set correctly
                            .OtherTrtID = 0,
                            .Detail = detailText,
                            .PatientID = _toothTrt.PatientID,
                            .TrtDate = _toothTrt.TreatDate,
                            .TrtValue = trtValue,
                            .Discount = 0,
                            .DiscountType = 1,
                            .IsMultiTooth = isMultiTooth
                        }
                            If Not clsTrtsData.AddTransactional(conn, trans, clsPatientTrts) Then
                                Dim msgEng As String = $"Failed to save Patient_Treatment '{treatmentText}' for '{toothName}'."
                                Dim msgAr As String = $"فشل في حفظ علاج المريض '{treatmentText}' ل '{toothName}'."
                                Dim msg As String = If(Eng, msgEng, msgAr)
                                MessageBox.Show(msg)
                            End If
                        End If
                        ' Payment saving logic (per-tooth)
                        If payValue > 0 Then
                            Dim lastTrtID As Integer = conn.ExecuteScalar(Of Integer)(
                            "SELECT TOP 1 TrtID FROM Patient_Trts ORDER BY TrtID DESC",
                            transaction:=trans)
                            Dim clsPatientPays As New Patient_Pays With {
                            .Notes = payNote,
                            .PayType = "Cash",
                            .PatientID = _toothTrt.PatientID,
                            .PayDate = _toothTrt.TreatDate,
                            .PayValue = payValue,
                            .TrtID = lastTrtID
                        }
                            Dim clsPatientPaysData As New Patient_PaysDATA
                            ' Always add a payment record for this treatment (single or multi-tooth),
                            ' using this tooth's own payValue and payNote.
                            If Not clsPatientPaysData.AddTransactional(conn, trans, clsPatientPays) Then
                                Dim msgEng As String = $"Failed to save Patient_Payment '{treatmentText}' for '{toothName}'."
                                Dim msgAr As String = $"فشل في حفظ دفعة المريض '{treatmentText}' ل '{toothName}'."
                                Dim msg As String = If(Eng, msgEng, msgAr)
                                MessageBox.Show(msg)
                            End If
                        End If
                    End If
                    trans.Commit()
                    committed = True
                    saved = True
                    canceled = False
                    ' Use global CurrentPatient for balance update (used by all patient-related controls project-wide)
                    Dim patientToUpdate As Patient = If(FormManager.Instance IsNot Nothing, FormManager.Instance.CurrentPatient, Nothing)
                    If patientToUpdate Is Nothing AndAlso _currentPatient IsNot Nothing Then patientToUpdate = _currentPatient
                    If patientToUpdate IsNot Nothing AndAlso FormManager.Instance IsNot Nothing AndAlso FormManager.Instance.CurrentForm IsNot Nothing Then
                        FormManager.Instance.CurrentForm.UpdatePatientBalance(patientToUpdate)
                    End If
                    Me.DialogResult = DialogResult.OK
                Catch ex As Exception
                    If Not committed Then trans.Rollback()
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
        "SELECT COUNT(*) FROM Patient_ToothTrt WHERE TrtGroupID = @GroupID",
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
    Dim _alphaCap As Byte = 128
    Dim fillClrWithOpacity As Color '= Color.FromArgb(128, clrFillColor.Color.R, clrFillColor.Color.G, clrFillColor.Color.B)
    Dim brdrClrWithOpacity As Color '= Color.FromArgb(128, clrBorderColor.Color.R, clrBorderColor.Color.G, clrBorderColor.Color.B)
    Dim capFill As Color
    Dim rootFill As Color



    Private Sub capOpacity_EditValueChanged(sender As Object, e As EventArgs) Handles capOpacity.EditValueChanged
        _alphaCap = CByte(capOpacity.Value)
        CapFillPick.Color = Color.FromArgb(_alphaCap, CapFillPick.Color.R, CapFillPick.Color.G, CapFillPick.Color.B)
        RootFillPick.Color = Color.FromArgb(_alphaCap, RootFillPick.Color.R, RootFillPick.Color.G, RootFillPick.Color.B)

    End Sub
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
    Private Sub CapFillPick_EditValueChanged(sender As Object, e As EventArgs) Handles CapFillPick.EditValueChanged
        capFill = Color.FromArgb(_alphaCap, CapFillPick.Color.R, CapFillPick.Color.G, CapFillPick.Color.B)
    End Sub

    Private Sub RootFillPick_EditValueChanged(sender As Object, e As EventArgs) Handles RootFillPick.EditValueChanged
        rootFill = Color.FromArgb(_alphaCap, RootFillPick.Color.R, RootFillPick.Color.G, RootFillPick.Color.B)
    End Sub
#Region "Implants"
    Dim _formattedResult As String = ""
    Dim _normalResult As String = ""
    Dim ImpBrand, ImpType, ImpDmm, ImpLmm, Slim As String

    Private Function BaseTreatForImplantPreview() As String
        Return ImplantTreatBracketHelper.TreatWithoutImplantBracketSuffix(txtTreat.Text)
    End Function

    ''' <summary>
    ''' Keeps ImpPopup / ResultLbl / _formattedResult aligned with the current row's Treat when using the navigator.
    ''' </summary>
    Private Sub SyncImplantPopupFromTreat(treat As String)
        If Not impCheck.Visible Then Return
        If Not impCheck.Checked Then
            ImpPopup.Text = ""
            _formattedResult = ""
            ResultLbl.Text = ""
            Return
        End If
        If ImplantTreatBracketHelper.ImplantSpecsPresentInTreat(treat) Then
            Dim i = treat.IndexOf("["c)
            Dim suffix As String = If(i > 0 AndAlso treat(i - 1) = " "c, treat.Substring(i - 1), " " & treat.Substring(i))
            ImpPopup.Text = suffix
            _formattedResult = treat
            ResultLbl.Text = treat
        Else
            ImpPopup.Text = ""
            _formattedResult = ""
            ResultLbl.Text = ""
        End If
    End Sub

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
        Dim baseTreat = BaseTreatForImplantPreview()
        _formattedResult =
                            $"{baseTreat} " &
                            $"{ImpBrand}-{ImpType}{vbCrLf}" &
                            $"{Slim} - {ImpDmm}x{ImpLmm}"
        ResultLbl.Text = _formattedResult
        _normalResult = $" [{ImpBrand}-{ImpType}-{Slim} {ImpDmm}x{ImpLmm}]"

    End Sub

    ''' <summary>
    ''' Sets ImpBrand, ImpType, Slim, ImpDmm, and ImpLmm from a full Treat string.
    ''' Uses the same bracket format as SetResult / btnSelect: … [Brand-Type-Slim DmmxLmm].
    ''' On parse failure, all five fields are cleared.
    ''' </summary>
    Private Sub SetImplantFieldsFromTreat(treat As String)
        Dim b As String = "", tn As String = "", sl As String = "", d As String = "", l As String = ""
        If ImplantTreatBracketHelper.TryParseImplantBracketPayload(treat, b, tn, sl, d, l) Then
            ImpBrand = b
            ImpType = tn
            Slim = sl
            ImpDmm = d
            ImpLmm = l
        Else
            ImpBrand = ""
            ImpType = ""
            Slim = ""
            ImpDmm = ""
            ImpLmm = ""
        End If
    End Sub

    ''' <summary>
    ''' Same field updates as the combo Implant*ValueChanged handlers, then SetResult — used when the popup is shown
    ''' so ResultLbl matches what the user would see after interacting with the combos.
    ''' </summary>
    Private Sub ApplyImplantSetResultFromCombosLikeSelection()
        If cmbBrand Is Nothing OrElse cmbType Is Nothing OrElse cmbDiameter Is Nothing OrElse cmbLength Is Nothing OrElse cmbDesign Is Nothing Then Return
        ImpBrand = cmbBrand.BrandName
        If String.IsNullOrWhiteSpace(ImpBrand) Then ImpBrand = If(cmbBrand.Text, "").Trim()
        ImpType = cmbType.TypeName
        If String.IsNullOrWhiteSpace(ImpType) Then ImpType = If(cmbType.Text, "").Trim()
        ImpDmm = cmbDiameter.DiameterMM
        ImpLmm = cmbLength.LengthMM
        Slim = If(cmbDesign.Text, "").Trim()
        SetResult()
    End Sub

    ''' <summary>
    ''' When editing a treat like "EXTRACTION + IMPLANT [Brand-Type-Slim 2.90x8.00]", populate combos from the bracket, then refresh ResultLbl.
    ''' Otherwise use default combo values like a fresh open.
    ''' </summary>
    Private Sub ApplyImplantWhenPopupOpens()
        Dim t = txtTreat.Text
        If impCheck.Checked AndAlso ImplantTreatBracketHelper.ImplantSpecsPresentInTreat(t) Then
            If TryApplyImplantCombosFromTreat(t) Then
                ApplyImplantSetResultFromCombosLikeSelection()
                Return
            End If
        End If
        ApplyImplantSetResultFromCombosLikeSelection()
    End Sub

    Private Function TryApplyImplantCombosFromTreat(treat As String) As Boolean
        SetImplantFieldsFromTreat(treat)
        If String.IsNullOrWhiteSpace(ImpBrand) Then Return False
        If cmbBrand Is Nothing OrElse cmbType Is Nothing OrElse cmbDiameter Is Nothing OrElse cmbLength Is Nothing OrElse cmbDesign Is Nothing Then Return False
        Try
            cmbBrand.SetSelectedBrandID(ImpBrand)
            cmbType.SetSelectedTypeID(ImpType)
            cmbDesign.Text = Slim
            Dim idx As Integer
            For idx = 0 To cmbDesign.Properties.Items.Count - 1
                If String.Equals(cmbDesign.Properties.Items(idx).ToString(), Slim, StringComparison.OrdinalIgnoreCase) Then
                    cmbDesign.SelectedIndex = idx
                    Exit For
                End If
            Next
            cmbDiameter.SetSelectedDiameterID(ImpDmm)
            cmbLength.SetSelectedLengthID(ImpLmm)
            Return True
        Catch
            Return False
        End Try
    End Function

    'Private Sub ImpPopup_QueryPopUp(sender As Object, e As CancelEventArgs) Handles ImpPopup.QueryPopUp
    '    BeginInvoke(New MethodInvoker(AddressOf ApplyImplantWhenPopupOpens))
    'End Sub

    'Private Sub ImpContainer_VisibleChanged(sender As Object, e As EventArgs) Handles ImpContainer.VisibleChanged
    '    If Not loaded OrElse Not ImpContainer.Visible OrElse Not impCheck.Checked Then Return
    '    BeginInvoke(New MethodInvoker(AddressOf ApplyImplantWhenPopupOpens))
    'End Sub
    Private Sub ImpPopup_Click(sender As Object, e As EventArgs) Handles ImpPopup.Click
        FillResult()
    End Sub
    Private Sub ImpPopup_Closed(sender As Object, e As ClosedEventArgs) Handles ImpPopup.Closed
        If Not ValidateStringFormat(_normalResult, ImpBrand, ImpType, Slim, ImpDmm, ImpLmm) Then
            MsgBox(If(Eng, "Incomplete Implant Specifications.", "المواصفات غير مكتملة للزرعة."))
        End If
    End Sub

    Private Sub FillResult()
        ImpBrand = cmbBrand.CboImplantBrand.Text
        ImpType = cmbType.CboImplantType.Text
        ImpDmm = cmbDiameter.CboImplantDiameter.Text
        ImpLmm = cmbLength.CboImplantLength.Text
        Slim = cmbDesign.Text
        SetResult()
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
        txtTreat.Text &= _normalResult
        Dim treatBinding = txtTreat.DataBindings("Text")
        If treatBinding IsNot Nothing Then
            treatBinding.WriteValue()
        End If
        TrtBS.EndEdit()
        SyncImplantPopupFromTreat(txtTreat.Text)
        'txtTrtDetails.Text = _formattedResult
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
                    MsgBox(msg)
                Else
                    Dim msgEng1 As String = $" Colors Not Updated In Treats Table"
                    Dim msgAr1 As String = $"لم يتم تحديث الألوان في جدول العلاجات"
                    Dim msg1 As String = If(Eng, msgEng1, msgAr1)
                    MsgBox(msg1)
                End If
                Dim msgEng2 As String = $" Colors Updated In Treats Table"
                Dim msgAr2 As String = $" تم تحديث الألوان في جدول العلاجات"
                Dim msg2 As String = If(Eng, msgEng2, msgAr2)
                MsgBox(msg2)
            Else
                Dim msgEng1 As String = $" Colors Not Updated In Treats Table"
                Dim msgAr1 As String = $"لم يتم تحديث الألوان في جدول العلاجات"
                Dim msg1 As String = If(Eng, msgEng1, msgAr1)
                MsgBox(msg1)
            End If
        End If
    End Sub
    Private Sub btTrtClrDef_Click(sender As Object, e As EventArgs) Handles btTrtClrDef.Click
        If txtTreat.Text.Length = 0 Then Exit Sub
        clrFillColor.Color = GetDefaultTrtColor(txtTreat.Text)
    End Sub
    Private Sub btCapRootClrDef_Click(sender As Object, e As EventArgs) Handles btCapRootClrDef.Click
        If txtTreat.Text.Length = 0 Then Exit Sub
        If Not specialTrts.Contains(txtTreat.Text.ToUpper()) Then Exit Sub
        CapFillPick.Color = GetDefaultCapColor(txtTreat.Text)
        RootFillPick.Color = GetDefaultRootColor(txtTreat.Text)
    End Sub

    Private Sub btnSetCapRootCustmClr_Click(sender As Object, e As EventArgs) Handles btnSetCapRootCustmClr.Click
        Dim Trt As String = txtTreat.Text.ToUpper()
        Select Case Trt
            Case "INDIRECT PULP CAPPING"
                Trt = "INDIRECTCAP"
            Case "DIRECT PULP CAPPING"
                ' In TblTRTS the code is stored as DIRECCAP (see Untitled data dump)
                Trt = "DIRECCAP"
            Case "PULPOTOMY"
                Trt = "PULPCAP"
        End Select
        Dim clsTblTrtDATA As New TblTRTSDATA
        Dim CheckTrtID As String = "SELECT  [TrtID]  FROM [dbo].[TblTRTS] WHERE Trt = @Trt"
        Dim Treat As String = txtTreat.Text
        Dim TrtID As Integer
        Using conn As New SqlConnection(ConnectionString)
            conn.Open()
            ' Check if any record exists with this treatment
            TrtID = conn.ExecuteScalar(Of Integer?)(CheckTrtID, New With {.Trt = Trt}) '.HasValue
        End Using
        If IsValidHexColor(ColorToHex(CapFillPick.Color, capOpacity.Value)) AndAlso IsValidHexColor(ColorToHex(RootFillPick.Color, capOpacity.Value)) Then
            If clsTblTrtDATA.UpdateTrtClr(TrtID, ColorToHex(CapFillPick.Color, capOpacity.Value), ColorToHex(RootFillPick.Color, capOpacity.Value), capRootThick.Value) Then
                ' Also update the current in-memory treatment(s) so drawing reflects immediately
                Dim newCapHex = ColorToHex(CapFillPick.Color, capOpacity.Value)
                Dim newRootHex = ColorToHex(RootFillPick.Color, capOpacity.Value)
                ' Single-tooth context
                If selectedToothTrtList Is Nothing OrElse selectedToothTrtList.Count = 0 Then
                    If _toothTrt IsNot Nothing Then
                        _toothTrt.CapFill = newCapHex
                        _toothTrt.RootFill = newRootHex
                    End If
                Else
                    ' Multi-tooth context: update all selected treatments with the same Treat text
                    For Each trt1 As Patient_ToothTrt In selectedToothTrtList
                        If String.Equals(trt1.Treat, txtTreat.Text, StringComparison.OrdinalIgnoreCase) Then
                            trt1.CapFill = newCapHex
                            trt1.RootFill = newRootHex
                        End If
                    Next
                End If
                Dim msgEng As String = $" Colors Updated In Treats Table"
                Dim msgAr As String = $" تم تحديث الألوان في جدول العلاجات"
                Dim msg As String = If(Eng, msgEng, msgAr)
                MsgBox(msg)
            Else
                Dim msgEng1 As String = $" Colors Not Updated In Treats Table"
                Dim msgAr1 As String = $"لم يتم تحديث الألوان في جدول العلاجات"
                Dim msg1 As String = If(Eng, msgEng1, msgAr1)
                MsgBox(msg1)
            End If
        End If
    End Sub
    Private Sub txtPrice_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtTrtPrice.KeyPress, txtPayValue.KeyPress
        If Char.IsControl(e.KeyChar) Then Return
        If Char.IsDigit(e.KeyChar) Then Return
        e.Handled = True
    End Sub
    Private Sub txtPrice_PreviewKeyDown(sender As Object, e As PreviewKeyDownEventArgs) Handles txtTrtPrice.PreviewKeyDown, txtPayValue.PreviewKeyDown
        If e.Control AndAlso e.KeyCode = Keys.V Then Return
        If e.KeyCode = Keys.Back OrElse e.KeyCode = Keys.Delete OrElse
                       e.KeyCode = Keys.Left OrElse e.KeyCode = Keys.Right OrElse
                       e.KeyCode = Keys.Tab Then Return
        If e.KeyCode >= Keys.D0 AndAlso e.KeyCode <= Keys.D9 Then Return
        If e.KeyCode >= Keys.NumPad0 AndAlso e.KeyCode <= Keys.NumPad9 Then Return
        e.IsInputKey = False
    End Sub
    Private Sub txtPrice_EditValueChanged(sender As Object, e As EventArgs) Handles txtTrtPrice.EditValueChanged, txtPayValue.EditValueChanged
        Dim ed = TryCast(sender, DevExpress.XtraEditors.TextEdit)
        If ed Is Nothing Then Return
        If String.IsNullOrEmpty(ed.Text) Then Return
        Dim cursorPos = ed.SelectionStart
        Dim cleaned As New System.Text.StringBuilder(ed.Text.Length)
        For Each c As Char In ed.Text
            If Char.IsDigit(c) Then cleaned.Append(c)
        Next
        Dim cleanedStr = cleaned.ToString()
        If cleanedStr <> ed.Text Then
            ed.Text = cleanedStr
            ed.SelectionStart = Math.Min(cursorPos, ed.Text.Length)
        End If
    End Sub

    Private Sub impCheck_CheckedChanged(sender As Object, e As EventArgs) Handles impCheck.CheckedChanged
        ImpPopup.Visible = impCheck.Checked ' _propName.Contains("IMPLANT")
        ImplantSpecsLbl.Visible = impCheck.Checked ' _propName.Contains("IMPLANT")
        If Not loaded Then Return
        If impCheck.Checked Then
            SyncImplantPopupFromTreat(If(currentTooth IsNot Nothing AndAlso currentTooth.Treat IsNot Nothing, currentTooth.Treat, txtTreat.Text))
        Else
            Dim curRow As Patient_ToothTrt = TryCast(TrtBS.Current, Patient_ToothTrt)
            Dim raw As String = If(curRow IsNot Nothing, curRow.Treat, txtTreat.Text)
            If raw Is Nothing Then raw = txtTreat.Text
            Dim stripped = ImplantTreatBracketHelper.TreatWithoutImplantBracketSuffix(raw)
            Dim row = TryCast(TrtBS.Current, Patient_ToothTrt)
            If row IsNot Nothing Then
                row.Treat = stripped
            End If
            Dim treatBinding = txtTreat.DataBindings("Text")
            If treatBinding IsNot Nothing Then
                treatBinding.ReadValue()
            Else
                txtTreat.Text = stripped
            End If
            TrtBS.EndEdit()
            ImpPopup.Text = ""
            _formattedResult = ""
            ResultLbl.Text = ""
            ImpPopup.ClosePopup()
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
    Private currentTooth As Patient_ToothTrt = Nothing
    Private Sub TrtBS_PositionChanged(sender As Object, e As EventArgs) Handles TrtBS.PositionChanged
        If TrtBS.Count = 0 Then Exit Sub
        currentTooth = CType(TrtBS.Current, Patient_ToothTrt)
        clrFillColor.Color = ColorTranslator.FromHtml(currentTooth.FillColor)
        clrBorderColor.Color = ColorTranslator.FromHtml(currentTooth.BorderColor)
        SyncImplantPopupFromTreat(If(currentTooth.Treat, ""))
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
