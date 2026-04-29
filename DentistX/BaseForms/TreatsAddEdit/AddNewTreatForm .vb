Imports System.ComponentModel
Imports System.Data.SqlClient
Imports System.Drawing
Imports System.Windows.Forms
Imports Dapper
Imports DentistX.TreatHelper
Imports DevExpress.DataProcessing.ExtractStorage
Imports DevExpress.Utils.Svg
Imports DevExpress.XtraEditors
Imports DevExpress.XtraGrid.Views.Grid
Public Class AddNewTreatForm
    Private selectedTeethList As New List(Of Byte)
    Private selectedToothTrtList As New List(Of Patient_ToothTrt)
    Dim _shapeId As Integer = 0
    Dim _propName As String = ""
    Dim toothTrtData As New Patient_ToothTrtDATA
    Dim _toothTrt As New Patient_ToothTrt
    Dim external As Boolean = False
    Dim paid As Boolean = False
    Dim loaded As Boolean = False
    Dim _alpha As Byte = 128
    Dim fillClrWithOpacity As Color
    Dim brdrClrWithOpacity As Color
    Dim _formattedResult As String = ""
    Dim _normalResult As String = ""
    Dim ImpBrand, ImpType, ImpDmm, ImpLmm, Slim As String
    Private toothNumsInt As New List(Of Integer)
    Private toothNums As New List(Of String)
    Dim OnePayAdded As Boolean = False
    Private currentTooth As Patient_ToothTrt = Nothing

    Private ReadOnly ConnectionString As String = DentistXDATA.GetConnection.ConnectionString

    ' Common initialization method
    Private Sub CommonInitialize()
        Me.Icon = AppIcon
        loaded = False
        isPaidChck.Checked = False
        IsExternalchk.Checked = False
        loaded = True
        cboTrtType.SelectedIndex = 0
        dtTrtDate.DateTime = Date.Now
        dtTrtEnd.DateTime = Date.Now
    End Sub

    Public Sub New()
        InitializeComponent()
        CommonInitialize()
    End Sub

    'Public Sub New(ByVal clsToothTrt As List(Of Patient_ToothTrt), ByVal clsPatient As Patient)
    '    InitializeComponent()
    '    CommonInitialize()
    '    selectedToothTrtList = clsToothTrt
    '    _toothTrt = clsToothTrt(0)
    '    InitializeFormData(clsToothTrt(0), clsPatient)
    '    TrtBS.DataSource = clsToothTrt.ToList
    '    SetSvgImages()
    'End Sub

    Public Sub New(ByVal clsToothTrt As Patient_ToothTrt, ByVal clsPatient As Patient)
        InitializeComponent()
        _toothTrt = clsToothTrt
        InitializeFormData(clsToothTrt, clsPatient)
        If clsToothTrt.BorderColor.Length = 8 Then
            SetBrdrClrFromHex(clsToothTrt.FillColor)
        End If
        TrtBS.DataSource = selectedToothTrtList
        SetupDataBindings()
        SetSvgImages()
        CommonInitialize()
    End Sub

    ' Update your constructors to ensure proper list handling
    Public Sub New(ByVal clsToothTrt As List(Of Patient_ToothTrt), ByVal clsPatient As Patient)
        InitializeComponent()

        selectedToothTrtList = clsToothTrt
        _toothTrt = clsToothTrt(0) ' Keep reference to first item for compatibility

        ' Initialize each treatment with individual values
        For Each treatment In selectedToothTrtList
            InitializeIndividualTreatment(treatment, clsPatient)
        Next

        TrtBS.DataSource = selectedToothTrtList
        SetupDataBindings()
        SetSvgImages()

        CommonInitialize()
    End Sub

    Private Sub InitializeIndividualTreatment(ByVal treatment As Patient_ToothTrt, ByVal patient As Patient)
        ' Set default values for each treatment if they're not already set
        If String.IsNullOrEmpty(treatment.BorderColor) Then
            treatment.BorderColor = "#FF000000"
        End If
        If String.IsNullOrEmpty(treatment.FillColor) Then
            'treatment.FillColor = GetCustomTrtColor(treatment.Treat)
        End If
        If Not treatment.TreatDate.HasValue Then
            treatment.TreatDate = Date.Now
        End If
        txtPatientName.Text = patient.PatientName
        _propName = treatment.PropertyName
        ImpPopup.Visible = _propName = "IMPLANT"
        ImplantSpecsLbl.Visible = _propName.StartsWith("IMPLANT")
    End Sub

    ' Consolidated form data initialization
    Private Sub InitializeFormData(ByVal toothTrt As Patient_ToothTrt, ByVal patient As Patient)
        'txtPatientID.Text = toothTrt.PatientID
        txtPatientName.Text = patient.PatientName
        'txtToothNum.Text = toothTrt.ToothNum
        'txtToothName.Text = GetToothFullName(toothTrt.ToothName)
        'clrBorderColor.Color = ColorTranslator.FromHtml(toothTrt.BorderColor)
        'tbThick.Value = 0
        'txtTreat.Text = toothTrt.Treat
        'clrFillColor.Color = GetCustomTrtColor(txtTreat.Text)
        _propName = toothTrt.PropertyName
        '_shapeId = toothTrt.ShapeID

        ImpPopup.Visible = _propName = "IMPLANT"
        ImplantSpecsLbl.Visible = _propName.StartsWith("IMPLANT")

        '' Consolidated status handling
        'external = If(toothTrt.IsExternal.HasValue, toothTrt.IsExternal, False)
        'paid = If(toothTrt.IsPaid.HasValue, toothTrt.IsPaid, False)
        ' Set up binding source
        SetupDataBindings()
        UpdateGroupBoxStatus()

        'isPaidChck.Checked = paid
        'IsExternalchk.Checked = external
        'txtExtClinic.Text = toothTrt.ExternalClinicName
    End Sub

    ' Add this new method to set up data bindings

    Private Sub SetupDataBindings()
        ' Clear existing bindings first
        txtPatientID.DataBindings.Clear()
        txtToothNum.DataBindings.Clear()
        txtToothName.DataBindings.Clear()
        clrBorderColor.DataBindings.Clear()
        clrFillColor.DataBindings.Clear()
        txtTreat.DataBindings.Clear()
        txtTrtPlan.DataBindings.Clear()
        txtTrtDetails.DataBindings.Clear()
        txtTrtNotes.DataBindings.Clear()
        dtTrtDate.DataBindings.Clear()
        dtTrtEnd.DataBindings.Clear()
        isPaidChck.DataBindings.Clear()
        IsExternalchk.DataBindings.Clear()
        txtExtClinic.DataBindings.Clear()
        txtTrtPrice.DataBindings.Clear()
        txtPayValue.DataBindings.Clear()
        cboTrtType.DataBindings.Clear()
        ceFinish.DataBindings.Clear()

        ' Set up new bindings with custom formatting for colors
        txtToothNum.DataBindings.Add("Text", TrtBS, "ToothNum", False, DataSourceUpdateMode.OnPropertyChanged)
        txtToothName.DataBindings.Add("Text", TrtBS, "ToothName", False, DataSourceUpdateMode.OnPropertyChanged)

        ' Custom binding for colors with conversion
        Dim borderColorBinding As New Binding("Color", TrtBS, "BorderColor", True, DataSourceUpdateMode.OnPropertyChanged)
        AddHandler borderColorBinding.Format, AddressOf HexToColor_Format
        AddHandler borderColorBinding.Parse, AddressOf ColorToHex_Parse
        clrBorderColor.DataBindings.Add(borderColorBinding)

        Dim fillColorBinding As New Binding("Color", TrtBS, "FillColor", True, DataSourceUpdateMode.OnPropertyChanged)
        AddHandler fillColorBinding.Format, AddressOf HexToColor_Format
        AddHandler fillColorBinding.Parse, AddressOf ColorToHex_Parse
        clrFillColor.DataBindings.Add(fillColorBinding)

        ' Other bindings
        txtPatientID.DataBindings.Add("Text", TrtBS, "PatientID", True, DataSourceUpdateMode.OnPropertyChanged)
        txtTreat.DataBindings.Add("Text", TrtBS, "Treat", True, DataSourceUpdateMode.OnPropertyChanged)
        txtTrtPlan.DataBindings.Add("Text", TrtBS, "TreatPlan", True, DataSourceUpdateMode.OnPropertyChanged)
        txtTrtDetails.DataBindings.Add("Text", TrtBS, "TreatDetails", True, DataSourceUpdateMode.OnPropertyChanged)
        txtTrtNotes.DataBindings.Add("Text", TrtBS, "TreatNotes", True, DataSourceUpdateMode.OnPropertyChanged)
        dtTrtDate.DataBindings.Add("DateTime", TrtBS, "TreatDate", True, DataSourceUpdateMode.OnPropertyChanged)
        dtTrtEnd.DataBindings.Add("DateTime", TrtBS, "TreatEndDate", True, DataSourceUpdateMode.OnPropertyChanged)
        isPaidChck.DataBindings.Add("Checked", TrtBS, "IsPaid", True, DataSourceUpdateMode.OnPropertyChanged)
        IsExternalchk.DataBindings.Add("Checked", TrtBS, "IsExternal", True, DataSourceUpdateMode.OnPropertyChanged)
        txtExtClinic.DataBindings.Add("Text", TrtBS, "ExternalClinicName", True, DataSourceUpdateMode.OnPropertyChanged)
        txtTrtPrice.DataBindings.Add("Text", TrtBS, "TrtValue", True, DataSourceUpdateMode.OnPropertyChanged, "0.00")
        txtPayValue.DataBindings.Add("Text", TrtBS, "PayValue", True, DataSourceUpdateMode.OnPropertyChanged, "0.00")
        cboTrtType.DataBindings.Add("Text", TrtBS, "TreatmentType", True, DataSourceUpdateMode.OnPropertyChanged)
        ceFinish.DataBindings.Add("Checked", TrtBS, "Finished", True, DataSourceUpdateMode.OnPropertyChanged)
    End Sub

    ' Color conversion handlers
    Private Sub HexToColor_Format(sender As Object, e As ConvertEventArgs)
        If TypeOf e.Value Is String Then
            Try
                e.Value = ColorTranslator.FromHtml(e.Value.ToString())
            Catch
                e.Value = Color.Black
            End Try
        End If
    End Sub

    Private Sub ColorToHex_Parse(sender As Object, e As ConvertEventArgs)
        If TypeOf e.Value Is Color Then
            e.Value = ColorTranslator.ToHtml(DirectCast(e.Value, Color))
        End If
    End Sub
    ' Consolidated status update method
    Private Sub UpdateGroupBoxStatus()
        If external Then
            GrpPays.Enabled = False
            GrpExtern.Enabled = True
        Else
            GrpPays.Enabled = True
            GrpExtern.Enabled = False
        End If
    End Sub

    ' Add these methods to handle color conversion for binding
    Private Function ColorToString(ByVal color As Color) As String
        Return ColorTranslator.ToHtml(color)
    End Function

    Private Function StringToColor(ByVal colorString As String) As Color
        If String.IsNullOrEmpty(colorString) Then Return Color.Black
        Try
            Return ColorTranslator.FromHtml(colorString)
        Catch
            Return Color.Black
        End Try
    End Function

    Private Function GetCurrentToothName() As String
        If TrtBS.Current IsNot Nothing Then
            Dim currentTrt As Patient_ToothTrt = DirectCast(TrtBS.Current, Patient_ToothTrt)
            Return GetToothFullName(currentTrt.ToothName)
        End If
        Return ""
    End Function

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
        If hexColor.StartsWith("#") Then
            hexColor = hexColor.Substring(1)
        End If
        If hexColor.Length <> 8 Then
            Throw New ArgumentException("Hex color must be in #AARRGGBB format")
        End If
        alpha = Convert.ToByte(hexColor.Substring(0, 2), 16)
        red = Convert.ToByte(hexColor.Substring(2, 2), 16)
        green = Convert.ToByte(hexColor.Substring(4, 2), 16)
        blue = Convert.ToByte(hexColor.Substring(6, 2), 16)
        clrBorderColor.Color = Color.FromArgb(alpha, red, green, blue)
    End Sub

    ' Consolidated validation method
    Private Function ValidateFormData() As Boolean
        Dim titleEn As String = "Field Required"
        Dim titleAr As String = "حقل مطلوب"
        Dim title As String = If(Eng, titleEn, titleAr)

        Dim validations As New List(Of (Condition As Boolean, MessageEn As String, MessageAr As String)) From {
            (cboTrtType.SelectedIndex < 0, "Select Treatment Type.", "اختر نوع العلاج."),
            (dtTrtDate.Text.Length = 0, "Enter Treatment Date.", "أدخل تاريخ العلاج."),
            (ceFinish.Checked AndAlso dtTrtEnd.Text.Length = 0, "Enter Treatment End Date.", "أدخل تاريخ انتهاء العلاج."),
            (txtTreat.Text.Length = 0, "Enter Treatment.", "أدخل العلاج."),
            (txtToothName.Text.Length = 0, "Tooth Name is missing.", "اسم السن مفقود."),
            (txtToothNum.Text.Length = 0, "Tooth Number is missing.", "رقم السن مفقود."),
            (txtPatientID.Text.Length = 0, "Patient Number is missing.", "رقم المريض مفقود."),
            (txtPatientName.Text.Length = 0, "Patient Name is missing.", "اسم المريض مفقود.")
        }

        For Each validation In validations
            If validation.Condition Then
                Dim msg As String = If(Eng, validation.MessageEn, validation.MessageAr)
                MessageBox.Show(msg, title, MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return False
            End If
        Next

        '' Shape validation
        'Dim treatmentToCheck As String = If(txtTreat.Text.Contains("IMPLANT"), "IMPLANT", txtTreat.Text)
        'If GetShapeIDByTrt(treatmentToCheck) < 1 Then
        '    Dim msgEn As String = "The selected Treat Has No Shape."
        '    Dim msgAr As String = "العلاج المحدد ليس له شكل."
        '    Dim msg As String = If(Eng, msgEn, msgAr)
        '    MessageBox.Show(msg, title, MessageBoxButtons.OK, MessageBoxIcon.Error)
        '    Return False
        'End If
        ' Shape validation
        Dim treatmentToCheck As String = If(txtTreat.Text.Contains("IMPLANT"), "IMPLANT", GetFirstTreatmentPart(txtTreat.Text))
        If GetShapeIDByTrt(treatmentToCheck) < 1 Then
            Dim msgEn As String = "The selected Treat Has No Shape."
            Dim msgAr As String = "العلاج المحدد ليس له شكل."
            Dim msg As String = If(Eng, msgEn, msgAr)
            MessageBox.Show(msg, title, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End If
        ' Implant validation
        If _propName = "IMPLANT" AndAlso _formattedResult.Length < 10 Then
            Dim msgEn As String = "IMPLANT Specs is missing."
            Dim msgAr As String = "مواصفات الزرعة مفقودة."
            Dim msg As String = If(Eng, msgEn, msgAr)
            MessageBox.Show(msg, title, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End If

        Return True
    End Function

    ' Consolidated tooth treatment setup
    Private Function SetupToothTrt1(Optional ByRef trtClas As Patient_ToothTrt = Nothing) As Boolean
        If Not ValidateFormData() Then Return False
        If cboTrtType.SelectedIndex = 0 Then
            ceFinish.Checked = True
        End If
        Dim targetTrt As Patient_ToothTrt = If(trtClas Is Nothing, _toothTrt, trtClas)
        Dim endDate As DateTime? = If(ceFinish.Checked AndAlso dtTrtEnd.Text.Length > 0, dtTrtEnd.DateTime, Nothing)
        Dim clinicName As String = GetClinicName()

        Dim hexFillWithAlpha As String = GetColorHexWithAlpha(fillClrWithOpacity)
        Dim hexBrdrWithAlpha As String = GetColorHexWithAlpha(brdrClrWithOpacity)

        With targetTrt
            .BorderColor = hexBrdrWithAlpha
            .BorderThickness = CByte(tbThick.Value)
            .FillColor = hexFillWithAlpha
            .IsPaid = paid
            .IsExternal = external
            .PatientID = PatientID
            .ShapeID = _shapeId
            .PropertyName = _propName
            ' IMPORTANT: Don't overwrite the tooth-specific data!
            ' .ToothName and .ToothNum should remain as they were in the original object
            .TreatDate = dtTrtDate.DateTime
            .TreatPlan = txtTrtPlan.Text
            .Treat = txtTreat.Text
            .LVL = If(_propName = "IMPLANT", GetLVL("IMPLANT"), GetLVL(txtTreat.Text))
            .TreatDetails = txtTrtDetails.Text
            .TreatmentType = cboTrtType.Text
            .TreatNotes = txtTrtNotes.Text & " " & If(.IsExternal, txtExtClinic.Text, "")
            .Finished = If(ceFinish.Checked, 1, 0)
            .TreatEndDate = endDate
            .ExternalClinicName = txtExtClinic.Text
        End With

        Return True
    End Function
    ' In your btnSaveTrt_Click method, the current logic should work fine since 
    ' it already iterates through each treatment and calls SetupToothTrt for each one
    ' Just ensure that SetupToothTrt uses the bound values from TrtBS.Current

    Private Function SetupToothTrt(Optional ByRef trtClas As Patient_ToothTrt = Nothing) As Boolean
        If Not ValidateFormData() Then Return False

        ' FIX: Properly handle both single and multi-tooth scenarios
        Dim targetTrt As Patient_ToothTrt = Nothing

        If trtClas IsNot Nothing Then
            ' Multi-tooth scenario: use the passed treatment
            targetTrt = trtClas
        ElseIf TrtBS.Current IsNot Nothing AndAlso TypeOf TrtBS.Current Is Patient_ToothTrt Then
            ' Single tooth scenario: use current from binding source
            targetTrt = DirectCast(TrtBS.Current, Patient_ToothTrt)
        ElseIf _toothTrt IsNot Nothing Then
            ' Fallback: use the original _toothTrt
            targetTrt = _toothTrt
        Else
            ' Last resort: try to get from selectedToothTrtList
            If selectedToothTrtList.Count > 0 Then
                targetTrt = selectedToothTrtList(0)
            Else
                ' If all else fails, show error
                MessageBox.Show("No treatment data available to save.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return False
            End If
        End If

        ' Additional null check
        If targetTrt Is Nothing Then
            MessageBox.Show("Cannot determine which treatment to save.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End If

        If cboTrtType.SelectedIndex = 0 Then
            ceFinish.Checked = True
        End If

        Dim endDate As DateTime? = If(ceFinish.Checked AndAlso dtTrtEnd.Text.Length > 0, dtTrtEnd.DateTime, Nothing)
        Dim clinicName As String = GetClinicName()

        Dim hexFillWithAlpha As String = GetColorHexWithAlpha(fillClrWithOpacity)
        Dim hexBrdrWithAlpha As String = GetColorHexWithAlpha(brdrClrWithOpacity)

        With targetTrt
            .BorderColor = hexBrdrWithAlpha
            .BorderThickness = CByte(tbThick.Value)
            .FillColor = hexFillWithAlpha
            .IsPaid = paid
            .IsExternal = external
            .PatientID = PatientID
            .ShapeID = _shapeId
            .PropertyName = _propName
            ' IMPORTANT: Don't overwrite the tooth-specific data!
            ' .ToothName and .ToothNum should remain as they were in the original object
            .TreatDate = dtTrtDate.DateTime
            .TreatPlan = txtTrtPlan.Text
            .Treat = txtTreat.Text
            .LVL = If(_propName = "IMPLANT", GetLVL("IMPLANT"), GetLVL(txtTreat.Text))
            .TreatDetails = txtTrtDetails.Text
            .TreatmentType = cboTrtType.Text
            .TreatNotes = txtTrtNotes.Text & " " & If(.IsExternal, txtExtClinic.Text, "")
            .Finished = If(ceFinish.Checked, 1, 0)
            .TreatEndDate = endDate
            .ExternalClinicName = txtExtClinic.Text
        End With

        Return True
    End Function


    Private Function SetToothTrt() As Boolean
        Return SetupToothTrt()
    End Function

    Private Function SetToothTrt(ByVal trtClas As Patient_ToothTrt) As Boolean
        Return SetupToothTrt(trtClas)
    End Function

    ' Helper methods
    Private Function GetClinicName() As String
        If IsExternalchk.Checked Then
            Return If(txtExtClinic.Text.Length < 1, "Somewhere Else", txtExtClinic.Text)
        End If
        Return "In House"
    End Function

    Private Function GetColorHexWithAlpha(color As Color) As String
        Return $"#{color.A:X2}{color.R:X2}{color.G:X2}{color.B:X2}"
    End Function

#Region "New Save Code"
    'Private Sub btnSaveTrt_Click(sender As Object, e As EventArgs) Handles btnSaveTrt.Click
    '    Dim treatmentText, toothName As String
    '    Dim clsPatientTrtsData As New Patient_TrtsDATA

    '    Dim isMultiTrt As Boolean = IsMultiToothTreatment(_toothTrt.Treat)
    '    Dim treatmentGroupID As Guid = Guid.Empty

    '    If isMultiTrt AndAlso selectedToothTrtList.Count > 0 Then
    '        treatmentGroupID = Guid.NewGuid()
    '    End If

    '    Dim trtPrice As Double = Val(txtTrtPrice.Text)
    '    Dim payValue As Double = Val(txtPayValue.Text)

    '    selectedToothTrtList.Clear()
    '    selectedToothTrtList = GetTreatmentsList()

    '    If selectedToothTrtList.Count = 0 Then
    '        ' Single tooth treatment
    '        If SetupToothTrt() Then
    '            treatmentText = _toothTrt.Treat
    '            toothName = _toothTrt.ToothName
    '            Saved = ExecuteTreatmentSave(_toothTrt, treatmentText, toothName, isMultiTrt, treatmentGroupID)
    '        End If
    '    Else
    '        ' Multi-tooth treatment - FIXED: Create a copy for each tooth
    '        toothNums.Clear()
    '        toothNumsInt.Clear()

    '        For Each trtCls In selectedToothTrtList
    '            toothNums.Add(GetShortToothNameWithDash(trtCls.ToothNum))
    '            toothNumsInt.Add(CInt(trtCls.ToothNum))
    '        Next

    '        Dim firstToothInGroup As Boolean = True
    '        For Each trtCls In selectedToothTrtList
    '            ' Create a NEW instance for each tooth to avoid reference issues
    '            Dim toothTrtCopy As Patient_ToothTrt = CreateToothTrtCopy(trtCls)

    '            If SetupToothTrt(toothTrtCopy) Then
    '                toothTrtCopy.IsMultiTrt = isMultiTrt
    '                toothTrtCopy.TrtGroupID = treatmentGroupID
    '                treatmentText = toothTrtCopy.Treat
    '                toothName = toothTrtCopy.ToothName

    '                Saved = ExecuteTreatmentSave(toothTrtCopy, treatmentText, toothName, isMultiTrt, treatmentGroupID, firstToothInGroup)
    '                firstToothInGroup = False
    '            End If
    '        Next
    '    End If

    '    OnePayAdded = False
    '    If Saved Then
    '        Me.DialogResult = DialogResult.OK
    '        Me.Close()
    '    Else
    '        Me.DialogResult = DialogResult.Cancel
    '    End If
    'End Sub

    Private Sub btnSaveTrt_Click(sender As Object, e As EventArgs) Handles btnSaveTrt.Click
        Dim treatmentText, toothName As String
        Dim clsPatientTrtsData As New Patient_TrtsDATA

        Dim isMultiTrt As Boolean = IsMultiToothTreatment(_toothTrt.Treat)
        Dim treatmentGroupID As Guid = Guid.Empty

        If isMultiTrt AndAlso selectedToothTrtList.Count > 0 Then
            treatmentGroupID = Guid.NewGuid()
        End If

        Dim trtPrice As Double = Val(txtTrtPrice.Text)
        Dim payValue As Double = Val(txtPayValue.Text)

        selectedToothTrtList.Clear()
        selectedToothTrtList = GetTreatmentsList()

        ' COUNT how many treatments we're trying to save
        Dim totalTreatmentsToSave As Integer = selectedToothTrtList.Count
        Dim successfullySavedCount As Integer = 0
        Dim failedTreatments As New List(Of String)

        If selectedToothTrtList.Count = 0 Then
            ' Single tooth treatment
            If SetupToothTrt() Then
                treatmentText = _toothTrt.Treat
                toothName = _toothTrt.ToothName
                If ExecuteTreatmentSave(_toothTrt, treatmentText, toothName, isMultiTrt, treatmentGroupID) Then
                    successfullySavedCount += 1
                Else
                    failedTreatments.Add($"Tooth {_toothTrt.ToothNum} - {_toothTrt.Treat}")
                End If
            End If
        Else
            ' Multi-tooth treatment
            toothNums.Clear()
            toothNumsInt.Clear()

            For Each trtCls In selectedToothTrtList
                toothNums.Add(GetShortToothNameWithDash(trtCls.ToothNum))
                toothNumsInt.Add(CInt(trtCls.ToothNum))
            Next

            Dim firstToothInGroup As Boolean = True
            For Each trtCls In selectedToothTrtList
                ' Create a NEW instance for each tooth to avoid reference issues
                Dim toothTrtCopy As Patient_ToothTrt = CreateToothTrtCopy(trtCls)

                If SetupToothTrt(toothTrtCopy) Then
                    toothTrtCopy.IsMultiTrt = isMultiTrt
                    toothTrtCopy.TrtGroupID = treatmentGroupID
                    treatmentText = toothTrtCopy.Treat
                    toothName = toothTrtCopy.ToothName

                    If ExecuteTreatmentSave(toothTrtCopy, treatmentText, toothName, isMultiTrt, treatmentGroupID, firstToothInGroup) Then
                        successfullySavedCount += 1
                    Else
                        failedTreatments.Add($"Tooth {toothTrtCopy.ToothNum} - {toothTrtCopy.Treat}")
                    End If
                    firstToothInGroup = False
                Else
                    failedTreatments.Add($"Tooth {trtCls.ToothNum} - {trtCls.Treat} (Setup failed)")
                End If
            Next
        End If

        OnePayAdded = False

        ' SHOW SUMMARY MESSAGE
        ShowSaveSummary(totalTreatmentsToSave, successfullySavedCount, failedTreatments)

        If successfullySavedCount > 0 Then
            Me.DialogResult = DialogResult.OK
            Me.Close()
        Else
            Me.DialogResult = DialogResult.Cancel
        End If
    End Sub

    Private Sub ShowSaveSummary(totalToSave As Integer, successfullySaved As Integer, failedTreatments As List(Of String))
        Dim title As String = If(Eng, "Save Summary", "ملخص الحفظ")
        Dim message As String = ""

        If Eng Then
            ' English message
            message = $"Treatment Save Summary:" & vbCrLf &
                  $"─────────────────────────" & vbCrLf &
                  $"Total treatments to save: {totalToSave}" & vbCrLf &
                  $"Successfully saved: {successfullySaved}" & vbCrLf &
                  $"Failed: {totalToSave - successfullySaved}"

            If failedTreatments.Count > 0 Then
                message &= vbCrLf & vbCrLf & "Failed treatments:" & vbCrLf
                For Each failed In failedTreatments
                    message &= $"• {failed}" & vbCrLf
                Next
            End If

            If successfullySaved = totalToSave Then
                message &= vbCrLf & "✅ All treatments saved successfully!"
            ElseIf successfullySaved = 0 Then
                message &= vbCrLf & "❌ No treatments were saved!"
            ElseIf successfullySaved < totalToSave Then
                message &= vbCrLf & "⚠️ Some treatments failed to save!"
            End If
        Else
            ' Arabic message
            message = $"ملخص حفظ العلاجات:" & vbCrLf &
                  $"──────────────────" & vbCrLf &
                  $"إجمالي العلاجات المطلوب حفظها: {totalToSave}" & vbCrLf &
                  $"تم الحفظ بنجاح: {successfullySaved}" & vbCrLf &
                  $"فشل في الحفظ: {totalToSave - successfullySaved}"

            If failedTreatments.Count > 0 Then
                message &= vbCrLf & vbCrLf & "العلاجات التي فشل حفظها:" & vbCrLf
                For Each failed In failedTreatments
                    message &= $"• {failed}" & vbCrLf
                Next
            End If

            If successfullySaved = totalToSave Then
                message &= vbCrLf & "✅ تم حفظ جميع العلاجات بنجاح!"
            ElseIf successfullySaved = 0 Then
                message &= vbCrLf & "❌ لم يتم حفظ أي علاج!"
            ElseIf successfullySaved < totalToSave Then
                message &= vbCrLf & "⚠️ فشل حفظ بعض العلاجات!"
            End If
        End If

        MessageBox.Show(message, title, MessageBoxButtons.OK,
                   If(successfullySaved = totalToSave, MessageBoxIcon.Information,
                   If(successfullySaved = 0, MessageBoxIcon.Error, MessageBoxIcon.Warning)))
    End Sub

    'Private Function ExecuteTreatmentSave(toothTrt As Patient_ToothTrt, treatmentText As String, toothName As String,
    '                                   isMultiTooth As Boolean, treatmentGroupID As Guid,
    '                                   Optional isFirstInGroup As Boolean = True) As Boolean
    '    Select Case toothTrt.Treat
    '        Case "ABUTMENT", "HEALING CAP"
    '            Return SaveTreatmentWithAbutment(toothTrt)
    '        Case Else
    '            Return SaveTreatmentWithTransaction(PatientID, toothTrt, treatmentText, toothName, isMultiTooth, treatmentGroupID, isFirstInGroup)
    '    End Select
    'End Function

    Private Function ExecuteTreatmentSave(toothTrt As Patient_ToothTrt, treatmentText As String, toothName As String,
                                   isMultiTooth As Boolean, treatmentGroupID As Guid,
                                   Optional isFirstInGroup As Boolean = True) As Boolean
        Try
            Select Case toothTrt.Treat
                Case "ABUTMENT", "HEALING CAP"
                    Return SaveTreatmentWithAbutment(toothTrt)
                Case Else
                    Return SaveTreatmentWithTransaction(PatientID, toothTrt, treatmentText, toothName, isMultiTooth, treatmentGroupID, isFirstInGroup)
            End Select
        Catch ex As Exception
            ' Log the error for debugging
            Debug.WriteLine($"Error saving treatment for tooth {toothTrt.ToothNum}: {ex.Message}")
            Return False
        End Try
    End Function
    Private Function GetLastToothTrtID() As Integer
        Dim dx As New DentistXDATA
        Using conn As New SqlConnection(dx.GetConnectionString)
            conn.Open()
            Return conn.ExecuteScalar(Of Integer)("SELECT TOP 1 ToothTrtID FROM Patient_ToothTrt ORDER BY ToothTrtID DESC")
        End Using
    End Function

    Public Function SaveTreatmentWithAbutment(_toothTrt As Patient_ToothTrt) As Boolean
        Dim toothTrtData As New Patient_ToothTrtDATA
        Return toothTrtData.AddNormal(_toothTrt)
    End Function

    Public Function SaveTreatmentWithTransaction(PatientID As Integer, _toothTrt As Patient_ToothTrt,
                                      treatmentText As String, toothName As String,
                                      Optional isMultiTooth As Boolean = False,
                                      Optional treatmentGroupID As Guid = Nothing,
                                      Optional isFirstInGroup As Boolean = True) As Boolean
        Dim saved As Boolean = False
        Dim clsTrtsData As New Patient_TrtsDATA
        Dim toothTrtData As New Patient_ToothTrtDATA
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
                    Dim trtValue As Double = Val(txtTrtPrice.Text)
                    Dim payValue As Double = Val(txtPayValue.Text)
                    Dim payNote As String = CalculatePaymentNote(trtValue, payValue)

                    ' Save Patient_ToothTrt
                    _toothTrt.IsPaid = isPaidChck.Checked
                    If _toothTrt.IsExternal = True Then _toothTrt.IsPaid = True

                    ' Level validation
                    If Not ValidateTreatmentLevel(conn, trans, _toothTrt) Then
                        Return False
                    End If

                    If Not toothTrtData.AddTransactional(conn, trans, _toothTrt) Then
                        ShowErrorMessage($"Failed to save Treatment in Chart '{treatmentText}' for '{toothName}'.")
                    End If

                    Dim lastToothTrtID As Integer = conn.ExecuteScalar(Of Integer)(
                            "SELECT TOP 1 ToothTrtID FROM Patient_ToothTrt ORDER BY ToothTrtID DESC",
                            transaction:=trans)

                    ' Handle multi-tooth treatment grouping
                    If isMultiTooth Then
                        HandleMultiToothTreatment(conn, trans, treatmentGroupID, lastToothTrtID, PatientID, toothName)
                    End If

                    ' Create accounting record for IN HOUSE treatments
                    If _toothTrt.IsExternal = False AndAlso (Not isMultiTooth OrElse isFirstInGroup) Then
                        CreateAccountingRecord(conn, trans, _toothTrt, lastToothTrtID, trtValue, isMultiTooth)
                    End If

                    ' Payment saving logic
                    If payValue > 0 AndAlso _toothTrt.IsExternal = False Then
                        ProcessPayment(conn, trans, _toothTrt, payValue, payNote)
                    End If

                    trans.Commit()
                    saved = True
                    FormManager.Instance.CurrentForm.UpdatePatientBalance(CurrentPatient)
                    Me.DialogResult = DialogResult.OK
                Catch ex As Exception
                    trans.Rollback()
                    ShowErrorMessage("Transaction Error: " & ex.Message, True)
                    saved = False
                End Try
            End Using
        End Using
        Return saved
    End Function

    ' Consolidated helper methods for transaction
    Private Function CalculatePaymentNote(trtValue As Double, payValue As Double) As String
        If payValue = trtValue Then
            isPaidChck.Checked = True
            Return "Payed In Full"
        ElseIf payValue < trtValue AndAlso payValue > 0 Then
            isPaidChck.CheckState = True
            Return "Payed Partially"
        ElseIf payValue > trtValue Then
            isPaidChck.Checked = True
            Return "Payed With Advance Payment"
        ElseIf trtValue = 0 AndAlso payValue = 0 Then
            isPaidChck.Checked = False
        End If
        Return ""
    End Function

    Private Function ValidateTreatmentLevel(conn As SqlConnection, trans As SqlTransaction, toothTrt As Patient_ToothTrt) As Boolean
        Dim MaxLvl As Integer = toothTrtData.GetTreatLVL(toothTrt.PatientID, toothTrt.ToothNum)
        Dim currentLvl As Integer = toothTrt.LVL

        ' Check if treat is a normal one after high level one
        If (MaxLvl > 4 AndAlso currentLvl < 4) Then
            ShowErrorMessage("You Cant Add a Normal Treat On High Level Treat....")
            Return False
        End If

        ' Special case: implant (5) → extraction (4)
        If (MaxLvl > 4 AndAlso currentLvl = 4) Then
            conn.Execute("UPDATE Patient_ToothTrt SET LVL = 4 WHERE PatientID = @PatientID AND ToothNum = @ToothNum AND LVL > 4",
                        New With {.PatientID = toothTrt.PatientID, .ToothNum = toothTrt.ToothNum}, trans)
        End If

        Return True
    End Function

    Private Sub HandleMultiToothTreatment(conn As SqlConnection, trans As SqlTransaction, treatmentGroupID As Guid,
                                        lastToothTrtID As Integer, patientID As Integer, toothName As String)
        Dim masterToothID As Integer = conn.ExecuteScalar(Of Integer)(
            "SELECT MIN(ToothTrtID) FROM Patient_ToothTrt WHERE TrtGroupID = @TrtGroupID",
            New With {.TrtGroupID = treatmentGroupID}, transaction:=trans)

        ' Update all other teeth in group to point to first tooth
        conn.Execute("UPDATE Patient_ToothTrt SET ParentToothTrtID = @ParentToothTrtID WHERE TrtGroupID = @TrtGroupID",
                    New With {.ParentToothTrtID = masterToothID, .TrtGroupID = treatmentGroupID}, transaction:=trans)

        ' Insert the record into Patient_TrtInfo
        Dim Trt As Patient_ToothTrt = toothTrtData.Select_Record(
            New Patient_ToothTrt With {.ToothTrtID = lastToothTrtID, .PatientID = patientID, .ToothName = toothName}, trans)

        If Trt Is Nothing Then
            ShowErrorMessage("Failed to retrieve tooth treatment record.")
            Return
        End If

        ' Copy quarter properties
        Trt.QrtrAddress = _toothTrt.QrtrAddress
        Trt.QrtrColumnValue = _toothTrt.QrtrColumnValue
        Trt.QrtrID = _toothTrt.QrtrID
        Trt.QrtrTable = _toothTrt.QrtrTable
        Trt.QrtrColumnName = _toothTrt.QrtrColumnName

        conn.Execute(
            "INSERT INTO [dbo].[Patient_TrtInfo] ([PatientID], [ParentToothTrtID], [TrtGroupID], [ToothNum], [ToothName], [TreatDate], [Treat], [TreatNotes], [IsExternal], [ExternalClinicName], [ExternalTreatmentDate]) VALUES (@PatientID, @ParentToothTrtID, @TrtGroupID, @ToothNum, @ToothName, @TreatDate, @Treat, @TreatNotes, @IsExternal, @ExternalClinicName, @ExternalTreatmentDate)",
            New With {
                .PatientID = patientID,
                .ParentToothTrtID = If(masterToothID > 0, masterToothID, DBNull.Value),
                .TrtGroupID = If(treatmentGroupID <> Guid.Empty, treatmentGroupID, DBNull.Value),
                .ToothNum = Trt.ToothNum,
                .ToothName = Trt.ToothName,
                .TreatDate = Trt.TreatDate,
                .Treat = If(Trt.Treat, DBNull.Value),
                .TreatNotes = If(Trt.TreatNotes, DBNull.Value),
                .IsExternal = Trt.IsExternal,
                .ExternalClinicName = If(Trt.ExternalClinicName, DBNull.Value),
                .ExternalTreatmentDate = If(Trt.ExternalTreatmentDate.HasValue, Trt.ExternalTreatmentDate.Value, DBNull.Value)
            }, transaction:=trans)
    End Sub

    ' Helper method to create a deep copy of Patient_ToothTrt
    Private Function CreateToothTrtCopy(original As Patient_ToothTrt) As Patient_ToothTrt
        Return New Patient_ToothTrt With {
        .ToothNum = original.ToothNum,
        .ToothName = original.ToothName,
        .PatientID = original.PatientID,
        .PropertyName = original.PropertyName,
        .ShapeID = original.ShapeID, ' Copy other properties as needed
        .BorderColor = original.BorderColor,
        .FillColor = original.FillColor,
        .BorderThickness = original.BorderThickness,
        .Treat = original.Treat,
        .TreatDate = original.TreatDate,
        .TreatPlan = original.TreatPlan,
        .TreatDetails = original.TreatDetails,
        .TreatmentType = original.TreatmentType,
        .TreatNotes = original.TreatNotes,
        .Finished = original.Finished,
        .TreatEndDate = original.TreatEndDate,
        .IsPaid = original.IsPaid,
        .IsExternal = original.IsExternal,
        .ExternalClinicName = original.ExternalClinicName,
        .LVL = original.LVL,
        .UserID = original.UserID,
        .QrtrAddress = original.QrtrAddress,
        .QrtrColumnValue = original.QrtrColumnValue,
        .QrtrID = original.QrtrID,
        .QrtrTable = original.QrtrTable,
        .QrtrColumnName = original.QrtrColumnName,
        .ExternalTreatmentDate = original.ExternalTreatmentDate,
        .IsMultiTrt = original.IsMultiTrt,
        .isOnImplant = original.isOnImplant,
        .ParentToothTrtID = original.ParentToothTrtID,
        .TrtGroupID = original.TrtGroupID,
        .TrtLoc = original.TrtLoc,
        .TrtValue = original.TrtValue
    }
    End Function

    Private Shared Function IsCompleteDentureTreatmentType(treat As String) As Boolean
        If String.IsNullOrWhiteSpace(treat) Then Return False
        Select Case treat.Trim().ToUpperInvariant()
            Case "COMPLETE DENTURE", "CD"
                Return True
            Case Else
                Return False
        End Select
    End Function

    ''' <summary>Short arch label for complete-denture billing row (avoids listing every tooth).</summary>
    Private Function FormatCompleteDentureArchSummary() As String
        If toothNumsInt Is Nothing OrElse toothNumsInt.Count = 0 Then
            Return If(Eng, "arch (unspecified)", "الفك (غير محدد)")
        End If
        Dim nums = toothNumsInt.Distinct().ToList()
        Dim allAdultUpper = nums.All(Function(t) t >= 11 AndAlso t <= 28)
        Dim allAdultLower = nums.All(Function(t) t >= 31 AndAlso t <= 48)
        If allAdultUpper Then Return If(Eng, "Upper arch", "الفك العلوي")
        If allAdultLower Then Return If(Eng, "Lower arch", "الفك السفلي")
        Return String.Join(", ", toothNums)
    End Function

    Private Sub CreateAccountingRecord(conn As SqlConnection, trans As SqlTransaction, toothTrt As Patient_ToothTrt,
                                    lastToothTrtID As Integer, trtValue As Double, isMultiTooth As Boolean)
        Dim clsTrtsData As New Patient_TrtsDATA
        Dim detailText As String

        If isMultiTooth AndAlso toothNums.Count > 0 Then
            If IsCompleteDentureTreatmentType(toothTrt.Treat) Then
                detailText = toothTrt.Treat.Trim() & " ==>> " & FormatCompleteDentureArchSummary()
            Else
                detailText = toothTrt.Treat & " ==>> " & String.Join(",", toothNums)
            End If
        Else
            detailText = toothTrt.Treat & " ==>> " & GetShortToothNameWithDash(toothTrt.ToothNum)
        End If

        Dim clsPatientTrts As New Patient_Trts With {
            .ToothTrtID = lastToothTrtID,
            .OrthoID = 0,
            .OtherTrtID = 0,
            .Detail = detailText,
            .PatientID = toothTrt.PatientID,
            .TrtDate = toothTrt.TreatDate,
            .TrtValue = trtValue,
            .Discount = 0,
            .DiscountType = 1,
            .IsMultiTooth = isMultiTooth
        }

        If Not clsTrtsData.AddTransactional(conn, trans, clsPatientTrts) Then
            ShowErrorMessage($"Failed to save Patient_Treatment '{toothTrt.Treat}' for '{toothTrt.ToothName}'.")
        End If
    End Sub

    Private Sub ProcessPayment(conn As SqlConnection, trans As SqlTransaction, toothTrt As Patient_ToothTrt,
                             payValue As Double, payNote As String)
        Dim clsPatientPaysData As New Patient_PaysDATA
        Dim lastTrtID As Integer = conn.ExecuteScalar(Of Integer)(
            "SELECT TOP 1 TrtID FROM Patient_Trts ORDER BY TrtID DESC", transaction:=trans)

        Dim clsPatientPays As New Patient_Pays With {
            .Notes = payNote,
            .PayType = "Cash",
            .PatientID = toothTrt.PatientID,
            .PayDate = toothTrt.TreatDate,
            .PayValue = payValue,
            .TrtID = lastTrtID
        }

        If selectedToothTrtList.Count > 0 Then
            If Not OnePayAdded Then
                If clsPatientPaysData.AddTransactional(conn, trans, clsPatientPays) Then
                    OnePayAdded = True
                Else
                    ShowErrorMessage($"Failed to save Patient_Payment '{toothTrt.Treat}' for '{toothTrt.ToothName}'.")
                End If
            End If
        Else
            If Not clsPatientPaysData.AddTransactional(conn, trans, clsPatientPays) Then
                ShowErrorMessage($"Failed to save Patient_Payment '{toothTrt.Treat}' for '{toothTrt.ToothName}'.")
            End If
        End If
    End Sub
    Private Sub ShowErrorMessage(message As String, Optional isException As Boolean = False)
        Dim msgEng As String = message
        Dim msgAr As String = If(isException, "خطأ في الإضافة: " & message, message)
        Dim msg As String = If(Eng, msgEng, msgAr)
        MessageBox.Show(If(isException, message, msg), msg, MessageBoxButtons.OK, MessageBoxIcon.Error)
    End Sub

    Private Function IsFirstInTreatmentGroup(conn As SqlConnection, trans As SqlTransaction, groupID As Guid) As Boolean
        If groupID = Guid.Empty Then Return False
        Dim count = conn.ExecuteScalar(Of Integer)(
            "SELECT COUNT(*) FROM Patient_ToothTrt WHERE TrtGroupID = @GroupID",
            New With {.GroupID = groupID}, transaction:=trans)
        Return count = 1
    End Function

    Private Function IsMultiToothTreatment(treatmentType As String) As Boolean
        Dim multiToothTreatments As String() = {"METAL BRIDGE", "ZERCONIA BRIDGE", "PFM BRIDGE", "EMAX BRIDGE", "TEMP BRIDGE", "STAINLESS STEEL BRIDGE",
                                                "REMOVABLE PARTIAL DENTURE", "COMPLETE DENTURE"}
        Return multiToothTreatments.Contains(treatmentType.ToUpper())
    End Function
#End Region

#Region "Set Svgs"
    Private Sub SetSvgImages()
        toothNumsInt.Clear()
        If selectedToothTrtList.Count <> 0 Then

            For Each trtCls In selectedToothTrtList
                toothNumsInt.Add(CInt(trtCls.ToothNum))
            Next

            CreateSvgs(selectedToothTrtList)
        Else
            toothNumsInt.Add(CInt(_toothTrt.ToothNum))
            For Each num As Integer In toothNumsInt

            Next
        End If

    End Sub
    Private Function SetSvgImg(num As Integer) As SvgImage
        Dim resourceMap = Helpers.AdultResourceMapping
        Dim baseKey As String = GetSvgKey(num)
        Dim svg As New SvgImage
        If resourceMap.ContainsKey(baseKey) Then
            Dim resources = resourceMap(baseKey)
            svg = resources.SvgOutResource
        End If
        Return svg
    End Function

    Private Sub SetKidSvgImages()

        Dim resourceMap = Helpers.KidResourceMapping

        For Each ct As Control In Me.svgsPanel.Controls
            If TypeOf ct Is SvgImageBox Then
                Dim sv As SvgImageBox = CType(ct, SvgImageBox)
                If sv.Name.Contains("K") Then
                    Dim baseKey As String = sv.Name.Replace("OUTK", "").Replace("INK", "").Replace("TOPK", "")
                    If resourceMap.ContainsKey(baseKey) Then
                        Dim resources = resourceMap(baseKey)
                        If sv.Name.Contains("OUT") Then
                            sv.SvgImage = resources.SvgOutResource
                        ElseIf sv.Name.Contains("IN") Then
                            sv.SvgImage = resources.SvgInResource
                        ElseIf sv.Name.Contains("TOP") Then
                            sv.SvgImage = resources.SvgTopResource
                        Else
                            sv.SvgImage = Nothing
                        End If
                    End If
                End If
            End If
        Next

    End Sub


    Private svgExternalList As New List(Of SvgImageBox)
    Private svgMetadata As New Dictionary(Of SvgImageBox, SvgTag)

    '=====================
    Private Sub CreateSvgs1(trts As List(Of Patient_ToothTrt))
        ' Clear existing controls first
        svgsPanel.Controls.Clear()
        svgsPanel.Size = New Size(790, 395)
        ' Configure panel for proper layout and scrolling
        svgsPanel.AutoScroll = True
        svgsPanel.AutoScrollMargin = New Size(10, 10) ' Add margin around content
        svgsPanel.HorizontalScroll.Visible = True
        svgsPanel.VerticalScroll.Visible = False ' Only horizontal scrolling
        svgsPanel.AutoScrollMinSize = New Size(0, 0) ' Reset min size

        ' Calculate layout parameters
        Dim svgWidth As Integer = 100
        Dim svgHeight As Integer = 170
        Dim horizontalPadding As Integer = 10 ' Space between SVG controls
        Dim panelPadding As Integer = 5 ' Space from panel edges

        ' Calculate total width needed for all SVGs
        Dim totalSvgWidth As Integer = (trts.Count * svgWidth) + ((trts.Count - 1) * horizontalPadding)

        ' Determine starting position based on panel width and content width
        Dim startX As Integer

        If totalSvgWidth < svgsPanel.ClientSize.Width Then
            ' Center the SVGs if they fit within panel width
            startX = (svgsPanel.ClientSize.Width - totalSvgWidth) \ 2
        Else
            ' Left align with padding if they exceed panel width
            startX = panelPadding
            ' Set minimum scrollable width
            svgsPanel.AutoScrollMinSize = New Size(totalSvgWidth + (2 * panelPadding), svgHeight)
        End If

        ' Vertical centering
        Dim startY As Integer = (svgsPanel.ClientSize.Height - svgHeight) \ 2

        ' Create and position SVG controls
        For i As Integer = 0 To trts.Count - 1
            Dim trt = trts(i)
            Dim svg As New SvgImageBox With {
            .Size = New Size(svgWidth, svgHeight),
            .SvgImage = SetSvgImg(trt.ToothNum),
            .Tag = trt.ToothNum ' Store tooth number for reference
        }
            'Treats handling
            TreatHelper.ResetSvgItemsVisibility(svg.RootItems)
            TreatHelper.ShowBaseTooth(svg.RootItems)
            Dim col As SvgImageItemCollection = svg.RootItems
            TreatHelper.ProcessToothTreatments(svg, svgExternalList, trts)

            ' Calculate position with proper spacing
            Dim xPosition As Integer = startX + (i * (svgWidth + horizontalPadding))
            svg.Location = New Point(xPosition, startY)

            ' Add some visual padding/margin around each SVG
            svg.Padding = New Padding(5) ' Internal padding
            svg.Margin = New Padding(2) ' External margin

            ' Optional: Add border for better visual separation
            svg.LookAndFeel.UseDefaultLookAndFeel = True
            svgsPanel.Controls.Add(svg)
        Next

        ' Refresh panel to ensure proper rendering
        svgsPanel.Invalidate()
        svgsPanel.Refresh()
    End Sub


    Private Sub CreateSvgs2(trts As List(Of Patient_ToothTrt))
        ' Clear existing controls first
        svgsPanel.Controls.Clear()
        svgsPanel.Size = New Size(790, 395)
        ' Configure panel for proper layout and scrolling
        svgsPanel.AutoScroll = True
        svgsPanel.AutoScrollMargin = New Size(10, 10) ' Add margin around content
        svgsPanel.HorizontalScroll.Visible = True
        svgsPanel.VerticalScroll.Visible = False ' Only horizontal scrolling
        svgsPanel.AutoScrollMinSize = New Size(0, 0) ' Reset min size

        ' Calculate layout parameters
        Dim svgWidth As Integer = 100
        Dim svgHeight As Integer = 170
        Dim horizontalPadding As Integer = 10 ' Space between SVG controls
        Dim panelPadding As Integer = 5 ' Space from panel edges

        ' Calculate total width needed for all SVGs
        Dim totalSvgWidth As Integer = (trts.Count * svgWidth) + ((trts.Count - 1) * horizontalPadding)

        ' Determine starting position based on panel width and content width
        Dim startX As Integer

        If totalSvgWidth < svgsPanel.ClientSize.Width Then
            ' Center the SVGs if they fit within panel width
            startX = (svgsPanel.ClientSize.Width - totalSvgWidth) \ 2
        Else
            ' Left align with padding if they exceed panel width
            startX = panelPadding
            ' Set minimum scrollable width
            svgsPanel.AutoScrollMinSize = New Size(totalSvgWidth + (2 * panelPadding), svgHeight)
        End If

        ' Vertical centering
        Dim startY As Integer = (svgsPanel.ClientSize.Height - svgHeight) \ 2

        ' Create and position SVG controls
        For i As Integer = 0 To trts.Count - 1
            Dim trt = trts(i)
            Dim svg As New SvgImageBox With {
            .Size = New Size(svgWidth, svgHeight),
            .SvgImage = SetSvgImg(trt.ToothNum),
            .Tag = trt.ToothNum ' Keep original byte value for compatibility
        }

            ' Store metadata in dictionary
            svgMetadata(svg) = New SvgTag With {
            .ToothNumber = trt.ToothNum,
            .TreatmentIndex = i,
            .Treatment = trt
        }
            ' Add mouse click handler
            AddHandler svg.Click, AddressOf Svg_Click
            AddHandler svg.DoubleClick, AddressOf Svg_DoubleClick

            'Treats handling
            TreatHelper.ResetSvgItemsVisibility(svg.RootItems)
            TreatHelper.ShowBaseTooth(svg.RootItems)
            Dim col As SvgImageItemCollection = svg.RootItems
            TreatHelper.ProcessToothTreatments(svg, svgExternalList, trts)

            ' Calculate position with proper spacing
            Dim xPosition As Integer = startX + (i * (svgWidth + horizontalPadding))
            svg.Location = New Point(xPosition, startY)

            ' Add some visual padding/margin around each SVG
            svg.Padding = New Padding(5) ' Internal padding
            svg.Margin = New Padding(2) ' External margin

            ' Set visual indicator for current item
            If TrtBS.Current IsNot Nothing AndAlso TypeOf TrtBS.Current Is Patient_ToothTrt Then
                Dim currentTrt As Patient_ToothTrt = DirectCast(TrtBS.Current, Patient_ToothTrt)
                If currentTrt.ToothTrtID = trt.ToothTrtID Then
                    SetSvgAsCurrent(svg)
                End If
            End If

            ' Optional: Add border for better visual separation
            svg.LookAndFeel.UseDefaultLookAndFeel = True
            svgsPanel.Controls.Add(svg)
        Next

        ' Refresh panel to ensure proper rendering
        svgsPanel.Invalidate()
        svgsPanel.Refresh()
    End Sub


    Private Sub CreateSvgs(trts As List(Of Patient_ToothTrt))
        ' Clear existing controls and metadata first
        svgsPanel.Controls.Clear()
        svgMetadata.Clear()

        svgsPanel.Size = New Size(790, 395)
        ' Configure panel for vertical scrolling only
        svgsPanel.AutoScroll = True
        svgsPanel.AutoScrollMargin = New Size(10, 10)
        svgsPanel.HorizontalScroll.Visible = False
        svgsPanel.HorizontalScroll.Enabled = False
        svgsPanel.VerticalScroll.Visible = True
        svgsPanel.VerticalScroll.Enabled = True
        svgsPanel.AutoScrollMinSize = New Size(0, 0)

        ' Calculate layout parameters
        Dim svgWidth As Integer = 100
        Dim svgHeight As Integer = 170
        Dim horizontalPadding As Integer = 10 ' Space between SVG controls horizontally
        Dim verticalPadding As Integer = 15 ' Space between rows
        Dim panelPadding As Integer = 10 ' Space from panel edges

        ' Calculate how many SVGs can fit in one row
        Dim availableWidth As Integer = svgsPanel.ClientSize.Width - (2 * panelPadding)
        Dim svgsPerRow As Integer = availableWidth \ (svgWidth + horizontalPadding)

        ' Ensure at least 1 SVG per row
        If svgsPerRow < 1 Then svgsPerRow = 1

        ' Always use flow layout with rows (no horizontal scrolling)
        Dim currentX As Integer = panelPadding
        Dim currentY As Integer = panelPadding
        Dim currentRowCount As Integer = 0

        For i As Integer = 0 To trts.Count - 1
            ' If current row is full, move to next row
            If currentRowCount >= svgsPerRow Then
                currentX = panelPadding
                currentY += svgHeight + verticalPadding
                currentRowCount = 0
            End If

            CreateSingleSvg(trts(i), i, currentX, currentY, svgWidth, svgHeight)

            currentX += svgWidth + horizontalPadding
            currentRowCount += 1
        Next

        ' Set minimum scrollable height if content exceeds panel height
        Dim totalHeight As Integer = currentY + svgHeight + panelPadding
        If totalHeight > svgsPanel.ClientSize.Height Then
            svgsPanel.AutoScrollMinSize = New Size(0, totalHeight)
        End If

        ' Refresh panel to ensure proper rendering
        svgsPanel.Invalidate()
        svgsPanel.Refresh()
    End Sub

    ' Helper method to create individual SVG controls
    Private Sub CreateSingleSvg(trt As Patient_ToothTrt, index As Integer, x As Integer, y As Integer, svgWidth As Integer, svgHeight As Integer)
        ' Create SVG with proper size including padding
        Dim svg As New SvgImageBox With {
        .Size = New Size(svgWidth, svgHeight),
        .SvgImage = SetSvgImg(trt.ToothNum),
        .Tag = trt.ToothNum,
        .Location = New Point(x, y),
        .Padding = New Padding(0), ' No internal padding
        .Margin = New Padding(0)   ' No external margin
    }

        ' Store metadata in dictionary
        svgMetadata(svg) = New SvgTag With {
        .ToothNumber = trt.ToothNum,
        .TreatmentIndex = index,
        .Treatment = trt
    }

        ' Add mouse click handler
        AddHandler svg.Click, AddressOf Svg_Click
        AddHandler svg.DoubleClick, AddressOf Svg_DoubleClick

        'Treats handling
        TreatHelper.ResetSvgItemsVisibility(svg.RootItems)
        TreatHelper.ShowBaseTooth(svg.RootItems)
        TreatHelper.ProcessToothTreatments(svg, svgExternalList, New List(Of Patient_ToothTrt) From {trt})

        ' Remove any border or special styling that might cause clipping
        'svg.BorderStyle = BorderStyle.None
        svg.BackColor = Color.Transparent

        ' Set visual indicator for current item
        If TrtBS.Current IsNot Nothing AndAlso TypeOf TrtBS.Current Is Patient_ToothTrt Then
            Dim currentTrt As Patient_ToothTrt = DirectCast(TrtBS.Current, Patient_ToothTrt)
            If currentTrt.ToothTrtID = trt.ToothTrtID Then
                SetSvgAsCurrent(svg)
            End If
        End If

        svgsPanel.Controls.Add(svg)
    End Sub

    ' Click event handler for SVG controls
    Private Sub Svg_Click(sender As Object, e As EventArgs)
        Dim clickedSvg As SvgImageBox = TryCast(sender, SvgImageBox)
        If clickedSvg IsNot Nothing AndAlso svgMetadata.ContainsKey(clickedSvg) Then
            Dim tag As SvgTag = svgMetadata(clickedSvg)

            ' Set the BindingSource position to the clicked SVG's index
            If TrtBS.DataSource IsNot Nothing AndAlso TrtBS.Count > tag.TreatmentIndex Then
                TrtBS.Position = tag.TreatmentIndex
                SetSvgAsCurrent(clickedSvg)
            End If
        End If
    End Sub

    ' Double-click event handler (optional - for additional functionality)
    Private Sub Svg_DoubleClick(sender As Object, e As EventArgs)
        ' You can add additional functionality here if needed
        ' For example, zooming or showing details
        Dim clickedSvg As SvgImageBox = TryCast(sender, SvgImageBox)
        If clickedSvg IsNot Nothing Then
            ' Add your double-click logic here
        End If
    End Sub

    ' Helper method to set a SVG as current and update visual appearance
    Private Sub SetSvgAsCurrent(svg As SvgImageBox)
        ' Reset all SVGs to normal appearance
        svgsPanel.SuspendLayout()
        For Each control As Control In svgsPanel.Controls
            If TypeOf control Is SvgImageBox Then
                Dim otherSvg As SvgImageBox = DirectCast(control, SvgImageBox)
                'otherSvg.BorderStyle = BorderStyle.None
                otherSvg.BackColor = Color.Transparent
            End If
        Next
        svg.BackColor = Color.AliceBlue ' Or any color you prefer for selection
        svgsPanel.ResumeLayout()
    End Sub
    ' Helper class to store SVG metadata
    Private Class SvgTag
        Public Property ToothNumber As Byte
        Public Property TreatmentIndex As Integer
        Public Property Treatment As Patient_ToothTrt
    End Class

    ' Update the BindingSource position changed event to sync with SVG selection
    'Private Sub TrtBS_PositionChanged(sender As Object, e As EventArgs) Handles TrtBS.PositionChanged
    '    If TrtBS.Count = 0 Then Exit Sub
    '    SyncSvgSelection()
    '    currentTooth = CType(TrtBS.Current, Patient_ToothTrt)
    '    clrFillColor.Color = ColorTranslator.FromHtml(currentTooth.FillColor)
    '    clrBorderColor.Color = ColorTranslator.FromHtml(currentTooth.BorderColor)
    'End Sub

    Private Sub TrtBS_PositionChanged(sender As Object, e As EventArgs) Handles TrtBS.PositionChanged
        If TrtBS.Count = 0 Then Exit Sub

        SyncSvgSelection()
        currentTooth = CType(TrtBS.Current, Patient_ToothTrt)

        ' Update the opacity and color displays for the CURRENT tooth
        UpdateDisplayForCurrentTooth()

    End Sub

    Private Sub UpdateDisplayForCurrentTooth()
        If currentTooth IsNot Nothing Then
            ' These will be handled by data binding, but we need to update the opacity slider
            ' since opacity is not directly bound to the treatment object
            Dim fillColor As Color = ColorTranslator.FromHtml(currentTooth.FillColor)
            Dim borderColor As Color = ColorTranslator.FromHtml(currentTooth.BorderColor)

            ' Update opacity based on current tooth's alpha values
            _alpha = fillColor.A
            tbOpacity.Value = _alpha

            ' Update colors with current opacity
            fillClrWithOpacity = Color.FromArgb(_alpha, fillColor.R, fillColor.G, fillColor.B)
            brdrClrWithOpacity = Color.FromArgb(_alpha, borderColor.R, borderColor.G, borderColor.B)
        End If
    End Sub

    ' Method to sync SVG selection with BindingSource position
    Private Sub SyncSvgSelection()
        If TrtBS.Current IsNot Nothing AndAlso TypeOf TrtBS.Current Is Patient_ToothTrt Then
            Dim currentTrt As Patient_ToothTrt = DirectCast(TrtBS.Current, Patient_ToothTrt)

            ' Find the SVG that matches the current treatment
            For Each kvp As KeyValuePair(Of SvgImageBox, SvgTag) In svgMetadata
                If kvp.Value.Treatment.ToothTrtID = currentTrt.ToothTrtID Then
                    SetSvgAsCurrent(kvp.Key)
                    Exit For
                End If
            Next
        End If
    End Sub

    Public Function GetTreatmentsList1() As List(Of Patient_ToothTrt)
        If TrtBS.DataSource Is Nothing Then
            Return New List(Of Patient_ToothTrt)()
        End If

        ' Using OfType to safely filter only Patient_ToothTrt objects
        If TypeOf TrtBS.DataSource Is IEnumerable Then
            Return DirectCast(TrtBS.DataSource, IEnumerable) _
               .OfType(Of Patient_ToothTrt)() _
               .ToList()
        Else
            Return New List(Of Patient_ToothTrt)()
        End If
    End Function

    Public Function GetTreatmentsList() As List(Of Patient_ToothTrt)
        If TrtBS.DataSource Is Nothing Then
            Return New List(Of Patient_ToothTrt)()
        End If

        ' Debug output to see what we're working with
        Debug.WriteLine($"GetTreatmentsList - DataSource type: {TrtBS.DataSource.GetType().Name}")

        If TypeOf TrtBS.DataSource Is IEnumerable Then
            Dim treatments = DirectCast(TrtBS.DataSource, IEnumerable) _
           .OfType(Of Patient_ToothTrt)() _
           .ToList()

            Debug.WriteLine($"GetTreatmentsList - Found {treatments.Count} treatments")
            For Each treatment In treatments
                Debug.WriteLine($"  - Tooth {treatment.ToothNum}: {treatment.Treat}")
            Next

            Return treatments
        ElseIf TypeOf TrtBS.DataSource Is Patient_ToothTrt Then
            ' Single treatment scenario
            Dim singleTreatment = DirectCast(TrtBS.DataSource, Patient_ToothTrt)
            Debug.WriteLine($"GetTreatmentsList - Single treatment: Tooth {singleTreatment.ToothNum}: {singleTreatment.Treat}")
            Return New List(Of Patient_ToothTrt) From {singleTreatment}
        Else
            Debug.WriteLine($"GetTreatmentsList - Unknown DataSource type: {TrtBS.DataSource.GetType().Name}")
            Return New List(Of Patient_ToothTrt)()
        End If
    End Function


    Private Sub svgsPanel_Resize(sender As Object, e As EventArgs) Handles svgsPanel.Resize
        ' Re-layout SVGs when panel is resized
        If svgsPanel.Controls.Count > 0 Then
            LayoutSvgs()
        End If
    End Sub

    Private Sub LayoutSvgs()
        If svgsPanel.Controls.Count = 0 Then Return
        Dim svgWidth As Integer = 100
        Dim svgHeight As Integer = 180
        Dim horizontalPadding As Integer = 10
        Dim panelPadding As Integer = 20
        ' Calculate total width needed
        Dim totalSvgWidth As Integer = (svgsPanel.Controls.Count * svgWidth) + ((svgsPanel.Controls.Count - 1) * horizontalPadding)
        ' Determine starting position
        Dim startX As Integer
        If totalSvgWidth < svgsPanel.ClientSize.Width Then
            startX = (svgsPanel.ClientSize.Width - totalSvgWidth) \ 2
        Else
            startX = panelPadding
            svgsPanel.AutoScrollMinSize = New Size(totalSvgWidth + (2 * panelPadding), svgHeight)
        End If
        ' Vertical centering
        Dim startY As Integer = (svgsPanel.ClientSize.Height - svgHeight) \ 2

        ' Reposition all SVG controls
        For i As Integer = 0 To svgsPanel.Controls.Count - 1
            Dim svg As SvgImageBox = DirectCast(svgsPanel.Controls(i), SvgImageBox)
            Dim xPosition As Integer = startX + (i * (svgWidth + horizontalPadding))
            svg.Location = New Point(xPosition, startY)
        Next
        svgsPanel.Invalidate()
    End Sub
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

    Private Sub tbOpacity_EditValueChanged(sender As Object, e As EventArgs) Handles tbOpacity.EditValueChanged
        _alpha = CByte(tbOpacity.Value)
        UpdateColorsWithOpacity()
    End Sub

    Private Sub clrFillColor_EditValueChanged(sender As Object, e As EventArgs) Handles clrFillColor.EditValueChanged
        fillClrWithOpacity = Color.FromArgb(_alpha, clrFillColor.Color.R, clrFillColor.Color.G, clrFillColor.Color.B)
    End Sub

    Private Sub clrBorderColor_EditValueChanged(sender As Object, e As EventArgs) Handles clrBorderColor.EditValueChanged
        brdrClrWithOpacity = Color.FromArgb(_alpha, clrBorderColor.Color.R, clrBorderColor.Color.G, clrBorderColor.Color.B)
    End Sub

    Private Sub UpdateColorsWithOpacity()
        clrFillColor.Color = Color.FromArgb(_alpha, clrFillColor.Color.R, clrFillColor.Color.G, clrFillColor.Color.B)
        clrBorderColor.Color = Color.FromArgb(_alpha, clrBorderColor.Color.R, clrBorderColor.Color.G, clrBorderColor.Color.B)
    End Sub

#Region "Implants"
    Private Sub SetResult()
        _formattedResult = $"IMPLANT {ImpBrand}-{ImpType}{vbCrLf}{Slim} - {ImpDmm}x{ImpLmm}"
        ResultLbl.Text = _formattedResult
        _normalResult = $"IMPLANT {ImpBrand}-{ImpType}-{Slim} {ImpDmm}x{ImpLmm}"
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
        txtTreat.Text = _normalResult
        txtTrtDetails.Text = _formattedResult
        ImpPopup.ClosePopup()
    End Sub

    Private Sub btnCancelImp_Click(sender As Object, e As EventArgs) Handles btnCancelImp.Click
        ImpPopup.Text = ""
        txtTreat.Text = "IMPLANT"
        ImpPopup.ClosePopup()
    End Sub
#End Region

    Function IsValidHexColor(colorStr As String) As Boolean
        Dim pattern As String = "^#[0-9A-Fa-f]{8}$"
        Return System.Text.RegularExpressions.Regex.IsMatch(colorStr, pattern)
    End Function

    Function ColorToHex(ByVal clr As Color, ByVal alphaValue As Integer) As String
        Dim alpha As Byte = CByte(Math.Max(0, Math.Min(255, alphaValue)))
        Dim red As Byte = clr.R
        Dim green As Byte = clr.G
        Dim blue As Byte = clr.B
        Return String.Format("#{0:X2}{1:X2}{2:X2}{3:X2}", alpha, red, green, blue)
    End Function

    Private Sub btnSetCustmColor_Click(sender As Object, e As EventArgs) Handles btnSetCustmColor.Click
        Dim clsTblTrtDATA As New TblTRTSDATA
        Dim CheckTrtID As String = "SELECT [TrtID] FROM [dbo].[TblTRTS] WHERE Trt = @Trt"
        Dim Treat As String = txtTreat.Text
        Dim TrtID As Integer

        Using conn As New SqlConnection(ConnectionString)
            conn.Open()
            TrtID = conn.ExecuteScalar(Of Integer?)(CheckTrtID, New With {.Trt = Treat})
        End Using

        If IsValidHexColor(ColorToHex(clrFillColor.Color, tbOpacity.Value)) AndAlso IsValidHexColor(ColorToHex(clrBorderColor.Color, tbOpacity.Value)) Then
            If clsTblTrtDATA.UpdateTrtClr(TrtID, ColorToHex(clrFillColor.Color, tbOpacity.Value), ColorToHex(clrBorderColor.Color, tbOpacity.Value), tbThick.Value) Then
                Dim x = clsTblTrtDATA.UpdateTreatFillColor(ColorToHex(clrFillColor.Color, tbOpacity.Value), Treat)
                If x > 0 Then
                    Dim msg As String = If(Eng, $"{x} Colors Updated In Treats Table", $"{x} تم تحديث الألوان في جدول العلاجات")
                    MsgBox(msg)
                End If
            End If
        End If
    End Sub

    ' Consolidated input validation
    Private Sub txtPrice_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtTrtPrice.KeyPress, txtPayValue.KeyPress
        If Char.IsControl(e.KeyChar) Then Return
        If Char.IsDigit(e.KeyChar) Then Return
        If e.KeyChar = "."c AndAlso Not DirectCast(sender, Control).Text.Contains(".") Then Return
        e.Handled = True
    End Sub

    Private Sub txtPrice_PreviewKeyDown(sender As Object, e As PreviewKeyDownEventArgs) Handles txtTrtPrice.PreviewKeyDown, txtPayValue.PreviewKeyDown
        If e.Control AndAlso e.KeyCode = Keys.V Then Return
        If e.KeyCode = Keys.Back OrElse e.KeyCode = Keys.Delete OrElse
           e.KeyCode = Keys.Left OrElse e.KeyCode = Keys.Right OrElse
           e.KeyCode = Keys.Tab Then Return
        If (e.KeyCode >= Keys.D0 AndAlso e.KeyCode <= Keys.D9) OrElse
           (e.KeyCode >= Keys.NumPad0 AndAlso e.KeyCode <= Keys.NumPad9) Then Return
        If e.KeyCode = Keys.Decimal AndAlso Not DirectCast(sender, Control).Text.Contains(".") Then Return
        e.IsInputKey = False
    End Sub

    Private Sub txtPrice_EditValueChanged(sender As Object, e As EventArgs) Handles txtTrtPrice.EditValueChanged, txtPayValue.EditValueChanged
        Dim textBox = DirectCast(sender, TextEdit)
        If String.IsNullOrEmpty(textBox.Text) Then Return

        Dim cursorPos = textBox.Text.Length
        Dim cleanedText As New System.Text.StringBuilder()
        Dim hasDecimal As Boolean = False

        For Each c As Char In textBox.Text
            If Char.IsDigit(c) Then
                cleanedText.Append(c)
            ElseIf c = "."c AndAlso Not hasDecimal Then
                cleanedText.Append(c)
                hasDecimal = True
            End If
        Next

        If cleanedText.ToString() <> textBox.Text Then
            textBox.Text = cleanedText.ToString()
            textBox.SelectionStart = Math.Min(cursorPos, textBox.Text.Length)
        End If
    End Sub

    Private Sub isPaidChck_CheckedChanged(sender As Object, e As EventArgs) Handles isPaidChck.CheckedChanged
        If loaded Then paid = isPaidChck.Checked
    End Sub

    Private Sub IsExternalchk_CheckedChanged(sender As Object, e As EventArgs) Handles IsExternalchk.CheckedChanged
        If loaded Then
            external = IsExternalchk.Checked
            UpdateGroupBoxStatus()
        End If
    End Sub

End Class