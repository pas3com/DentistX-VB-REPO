Imports System.ComponentModel
Imports System.Data.SqlClient
Imports System.Drawing
Imports System.Globalization
Imports System.Linq
Imports System.Windows.Forms
Imports Dapper
Imports DevExpress.Utils

Public Class EditTreatFrom
    ''' <summary>DB keys as they were when the form opened — used for UPDATE ... WHERE after the bound row is edited.</summary>
    Private _rowAtOpen As Patient_ToothTrt

    ''' <summary>Stored in DB as English; UI may show Arabic when Eng is False.</summary>
    Private Const TrtTypeOneStageEn As String = "One Stage"
    Private Const TrtTypeMultipleStageEn As String = "Multiple Stages"
    Private Const TrtTypeOneStageAr As String = "مرحلة واحدة"
    Private Const TrtTypeMultipleStageAr As String = "عدة مراحل"

    Private _applyingTreatmentTypeIndex As Boolean

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()
        Me.Icon = AppIcon

        ' Add any initialization after the InitializeComponent() call.
        ' One Stage Size 385, 327
        'Multi Stage Size 911, 327
    End Sub


    Public Sub New(ByVal clsToothTrt As Patient_ToothTrt, ByVal clsPatient As Patient)

        ' This call is required by the designer.
        InitializeComponent()
        Me.Icon = AppIcon
        ' Add any initialization after the InitializeComponent() call.
        _rowAtOpen = clsToothTrt.Clone()

        'txtTrtID.Text = clsToothTrt.ToothTrtID
        txtPatientID.Text = clsToothTrt.PatientID
        txtPatientName.Text = clsPatient.PatientName
        'txtToothNum.Text = clsToothTrt.ToothNum
        'txtToothName.Enabled = True
        'txtToothName.Text = clsToothTrt.ToothName
        ''txtToothName.Enabled = False
        'txtTreat.Text = clsToothTrt.Treat
        tbThick.Value = clsToothTrt.BorderThickness
        clrFillColor.Color = ColorTranslator.FromHtml(clsToothTrt.FillColor)
        clrBorderColor.Color = ColorTranslator.FromHtml(clsToothTrt.BorderColor)
        CapFillPick.Color = ColorTranslator.FromHtml(clsToothTrt.CapFill)
        RootFillPick.Color = ColorTranslator.FromHtml(clsToothTrt.RootFill)
        'dtTrtDate.DateTime = clsToothTrt.TreatDate
        'txtTrtPlan.Text = clsToothTrt.TreatPlan
        'txtTrtDetails.Text = clsToothTrt.TreatDetails
        'clsToothTrt.TreatmentType = cboTrtType.Text
        'txtTrtNotes.Text = clsToothTrt.TreatNotes
        'ceFinish.Checked = (clsToothTrt.Finished = True)
        'If clsToothTrt.IsExternal Is Nothing Then
        '    chkIsExternal.Checked = False
        'Else
        '    chkIsExternal.Checked = (clsToothTrt.IsExternal = True)
        'End If
        'txtExtClinic.Text = clsToothTrt.ExternalClinicName
        '' Check and fix TreatEndDate if it is Nothing
        'If clsToothTrt.TreatEndDate Is Nothing OrElse clsToothTrt.TreatEndDate < New DateTime(2000, 1, 1) Then
        '    dtTrtEnd.Text = String.Empty ' Set to empty if invalid
        'Else
        '    dtTrtEnd.DateTime = clsToothTrt.TreatEndDate
        'End If
        'txtTreat.Text = clsToothTrt.Treat
        oldToothTRT = clsToothTrt
        propName = clsToothTrt.PropertyName
        ShapeID = clsToothTrt.ShapeID
        'If clsToothTrt.IsExternal.HasValue Then
        '    If (clsToothTrt.IsExternal = True) Then
        '        lblExternal.Visible = True
        '        chkIsExternal.Visible = True
        '        txtExtClinic.Visible = True
        '    Else
        '        lblExternal.Visible = False
        '        chkIsExternal.Visible = False
        '        txtExtClinic.Visible = False
        '    End If
        'Else
        '    lblExternal.Visible = False
        '    chkIsExternal.Visible = False
        '    txtExtClinic.Visible = False
        'End If
        grpSpecialClrs.Visible = Not String.IsNullOrEmpty(clsToothTrt.Treat) AndAlso specialTrts.Contains(clsToothTrt.Treat.ToUpper())

        impCheck.Visible = clsToothTrt.PropertyName IsNot Nothing AndAlso clsToothTrt.PropertyName.Contains("IMPLANT")
        EnrichTrtValueAndFirstPay(clsToothTrt)
        TrtBS.DataSource = clsToothTrt
        WireTreatEndAndFinishedBindings()

    End Sub

    ''' <summary>Sets price from Patient_Trts.TrtValue and pay from the first Patient_Pays row (by PayID) for this tooth treatment.</summary>
    Private Sub EnrichTrtValueAndFirstPay(row As Patient_ToothTrt)
        If row Is Nothing OrElse row.PatientID <= 0 OrElse row.ToothTrtID <= 0 Then Return
        Try
            Dim cs = DentistXDATA.GetConnection.ConnectionString
            Using conn As New SqlConnection(cs)
                conn.Open()
                Dim trtRow = conn.QuerySingleOrDefault(Of Patient_Trts)(
                    "SELECT TOP 1 * FROM Patient_Trts WHERE PatientID = @PatientID AND ToothTrtID = @ToothTrtID ORDER BY TrtID",
                    New With {.PatientID = row.PatientID, .ToothTrtID = row.ToothTrtID})
                If trtRow Is Nothing Then Return
                row.TrtValue = trtRow.TrtValue
                Dim firstPay = conn.ExecuteScalar(Of Decimal?)(
                    "SELECT TOP 1 PayValue FROM Patient_Pays WHERE TrtID = @TrtID ORDER BY PayID",
                    New With {.TrtID = trtRow.TrtID})
                If firstPay.HasValue Then row.PayValue = firstPay.Value
            End Using
        Catch
        End Try
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

    Private Function ParseBoundDecimal(raw As String, fallback As Decimal) As Decimal
        If String.IsNullOrWhiteSpace(raw) Then Return fallback
        Dim t As Decimal
        If Decimal.TryParse(raw, NumberStyles.Currency Or NumberStyles.Number, CultureInfo.CurrentCulture, t) Then Return t
        If Decimal.TryParse(raw, NumberStyles.Currency Or NumberStyles.Number, CultureInfo.InvariantCulture, t) Then Return t
        Return fallback
    End Function
    Private Sub EditTreatFrom_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SyncOpacityFromEditors()
        If TrtBS.DataSource IsNot Nothing AndAlso TrtBS.Count > 0 Then
            TrtBS.Position = 0
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
    End Sub

    Private Sub EditTreatFrom_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
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

    Private Sub ConfigureTreatmentTypeCombo()
        cboTrtType.DataBindings.Clear()
        cboTrtType.Properties.Items.Clear()
        If Eng Then
            cboTrtType.Properties.Items.AddRange({TrtTypeOneStageEn, TrtTypeMultipleStageEn})
        Else
            cboTrtType.Properties.Items.AddRange({TrtTypeOneStageAr, TrtTypeMultipleStageAr})
        End If
    End Sub

    Private Sub ApplyTreatmentTypeComboFromRow(row As Patient_ToothTrt)
        If row Is Nothing Then Return
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

    Private Sub SyncOpacityFromEditors()
        alpha = CByte(tbOpacity.Value)
        _alphaCap = CByte(capOpacity.Value)
        clrWithOpacity = Color.FromArgb(alpha, clrFillColor.Color.R, clrFillColor.Color.G, clrFillColor.Color.B)
        brdrClrWithOpacity = Color.FromArgb(alpha, clrBorderColor.Color.R, clrBorderColor.Color.G, clrBorderColor.Color.B)
        capFill = Color.FromArgb(_alphaCap, CapFillPick.Color.R, CapFillPick.Color.G, CapFillPick.Color.B)
        rootFill = Color.FromArgb(_alphaCap, RootFillPick.Color.R, RootFillPick.Color.G, RootFillPick.Color.B)
    End Sub

    Dim ShapeID As Integer = 0
    Dim propName As String = ""
    Dim oldTrt As String = ""
    Dim trtID As Integer = 0
    Dim treatmentText As String = ""
    Private TrtClr As String = ""
    Private ShapeName As String = ""

    Dim toothTrtData = New Patient_ToothTrtDATA
    Dim newToothTRT As New Patient_ToothTrt
    Dim oldToothTRT As New Patient_ToothTrt
    Dim toothHistory As New Patient_ToothTrtHistory
    Dim toothHisData As New Patient_ToothTrtHistoryDATA



    Private Sub txtTreat_EditValueChanged(sender As Object, e As EventArgs) Handles txtTreat.EditValueChanged
        ' Define the connection string
        Dim connectionString As String = DentistXDATA.GetConnection.ConnectionString

        ' Query to get the TrtColor for the selected treatment
        Dim query As String = "SELECT TrtColor FROM TblTRTS WHERE Trt = @Trt"

        '' Use a database connection to execute the query
        'Using connection As New SqlClient.SqlConnection(connectionString)
        '    connection.Open()
        '    Using command As New SqlClient.SqlCommand(query, connection)
        '        ' Add parameter to avoid SQL injection
        '        command.Parameters.AddWithValue("@Trt", txtTreat.Text)

        '        ' Execute the query and read the result
        '        Dim result = command.ExecuteScalar()
        '        If result IsNot Nothing AndAlso Not String.IsNullOrEmpty(result.ToString()) Then
        '            Dim colorString As String = result.ToString().Trim()

        '            ' Ensure the color string starts with '#' if it doesn't already
        '            If Not colorString.StartsWith("#") Then
        '                colorString = "#" & colorString
        '            End If

        '            ' Convert the color from the database to a Color object
        '            clrFillColor.Color = ColorTranslator.FromHtml(colorString)
        '        Else
        '            ' Handle case where no color is found
        '            clrFillColor.Color = Color.Empty ' Default to no color
        '        End If
        '    End Using
        'End Using


    End Sub

    Private Sub btnExit_Click(sender As Object, e As EventArgs)
        Me.Dispose()
    End Sub

    Private Function SetToothTrt(TRT As Patient_ToothTrt) As Boolean
        FlushTreatmentTypeEnglishFromComboToRow()
        TrtBS.EndEdit()
        Dim cur As Patient_ToothTrt = TryCast(TrtBS.Current, Patient_ToothTrt)
        Dim treatmentTypeEnglish = NormalizeTreatmentTypeToEnglish(
            If(cur IsNot Nothing AndAlso Not String.IsNullOrWhiteSpace(cur.TreatmentType), cur.TreatmentType, CurrentTreatmentTypeEnglishFromCombo()))

        Dim titleEn As String = "Field Required"
        Dim titleAr As String = "حقل مطلوب"
        Dim title As String = If(Eng, titleEn, titleAr)

        ' TblTRTS / Shapes lookups use exact Trt keys (e.g. "IMPLANT"). Full stored text like
        ' "EXTRACTION + IMPLANT [Brand-Type-Slim 2.9x8]" does not match any row — GetTrtShape returns Nothing and PropertyName becomes NULL.
        Dim TreatString As String = txtTreat.Text
        If TreatString IsNot Nothing AndAlso TreatString.Contains("IMPLANT") Then
            TreatString = "IMPLANT"
        End If
        If txtTrtID.Text.Length = 0 Then
            Dim msgEn As String = "Treat ID is missing."
            Dim msgAr As String = "معرف العلاج مفقود."
            Dim msg As String = If(Eng, msgEn, msgAr)

            MessageBox.Show(msg, title, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End If
        If String.IsNullOrWhiteSpace(treatmentTypeEnglish) OrElse
           (Not String.Equals(treatmentTypeEnglish, TrtTypeOneStageEn, StringComparison.OrdinalIgnoreCase) AndAlso
            Not String.Equals(treatmentTypeEnglish, TrtTypeMultipleStageEn, StringComparison.OrdinalIgnoreCase)) Then
            Dim msgEn As String = "Select Treatment Type."
            Dim msgAr As String = "اختر نوع العلاج."
            MessageBox.Show(If(Eng, msgEn, msgAr), title, MessageBoxButtons.OK, MessageBoxIcon.Error)
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
        'Dim clrWithOpacity As Color = Color.FromArgb(128, clrFillColor.Color.R, clrFillColor.Color.G, clrFillColor.Color.B)
        ' 128 is 50% opacity (range is 0-255)
        'Dim rgbaString As String = $"rgba({clrWithOpacity.R}, {clrWithOpacity.G}, {clrWithOpacity.B}, {clrWithOpacity.A / 255.0})"
        'Console.WriteLine(rgbaString) ' Example: "rgba(255, 0, 0, 0.5)"
        Dim hexWithAlpha As String = $"#{clrWithOpacity.A:X2}{clrWithOpacity.R:X2}{clrWithOpacity.G:X2}{clrWithOpacity.B:X2}"
        'Console.WriteLine(hexWithAlpha) ' Example: "#80FF0000" (50% opacity red)
        Dim hexBrdrWithAlpha As String = $"#{brdrClrWithOpacity.A:X2}{brdrClrWithOpacity.R:X2}{brdrClrWithOpacity.G:X2}{brdrClrWithOpacity.B:X2}"
        Dim hexCapFillWithAlpha As String = $"#{capFill.A:X2}{capFill.R:X2}{capFill.G:X2}{capFill.B:X2}"
        Dim hexRootWithAlpha As String = $"#{rootFill.A:X2}{rootFill.R:X2}{rootFill.G:X2}{rootFill.B:X2}"

        Dim trtVal As Decimal = If(cur IsNot Nothing, cur.TrtValue, 0D)
        trtVal = ParseBoundDecimal(txtTrtPrice.Text, trtVal)
        Dim payVal As Decimal = If(cur IsNot Nothing, cur.PayValue, 0D)
        payVal = ParseBoundDecimal(txtPayValue.Text, payVal)

        Dim resolvedShapeName As String = GetTrtShape(TreatString)
        If String.IsNullOrEmpty(resolvedShapeName) Then
            If Not String.IsNullOrEmpty(propName) Then
                resolvedShapeName = propName
            ElseIf oldToothTRT IsNot Nothing AndAlso Not String.IsNullOrEmpty(oldToothTRT.PropertyName) Then
                resolvedShapeName = oldToothTRT.PropertyName
            End If
        End If

        Dim resolvedShapeId As Integer = GetShapeIDByTrt(TreatString)
        If resolvedShapeId < 1 AndAlso oldToothTRT IsNot Nothing AndAlso oldToothTRT.ShapeID > 0 Then
            resolvedShapeId = oldToothTRT.ShapeID
        End If

        TRT = New Patient_ToothTrt With {.BorderColor = hexBrdrWithAlpha,
                                         .BorderThickness = CByte(tbThick.Value),
                                         .FillColor = hexWithAlpha,' ColorTranslator.ToHtml(clrFillColor.Color),
                                         .CapFill = hexCapFillWithAlpha,
                                         .RootFill = hexRootWithAlpha,
                                         .ToothTrtID = CInt(txtTrtID.Text),
                                         .PatientID = PatientID,
                                         .ShapeID = resolvedShapeId,
                                         .LVL = GetLVL(TreatString),
                                         .PropertyName = resolvedShapeName,
                                         .ToothName = txtToothName.Text,
                                         .ToothNum = txtToothNum.Text,
                                         .TreatDate = dtTrtDate.DateTime,
                                         .TreatPlan = txtTrtPlan.Text,
                                         .TreatDetails = txtTrtDetails.Text,
                                         .TreatmentType = treatmentTypeEnglish,
                                         .TreatNotes = txtTrtNotes.Text,
                                         .Finished = If(ceFinish.Checked, 1, 0),
                                         .TreatEndDate = endDate, 'If(ceFinish.Checked, dtTrtEnd.DateTime, Nothing),
                                         .Treat = txtTreat.Text,
                                         .IsExternal = IsExternalchk.Checked,
                                         .ExternalClinicName = clinicName,
                                         .QrtrAddress = oldToothTRT.QrtrAddress,
                                         .QrtrID = oldToothTRT.QrtrID,
                                         .TrtLoc = oldToothTRT.TrtLoc,
                                         .ExternalTreatmentDate = oldToothTRT.ExternalTreatmentDate,
                                         .IsMultiTrt = oldToothTRT.IsMultiTrt,
                                         .isOnImplant = oldToothTRT.isOnImplant,
                                         .ParentToothTrtID = oldToothTRT.ParentToothTrtID,
                                         .QrtrColumnName = oldToothTRT.QrtrColumnName,
                                         .QrtrColumnValue = oldToothTRT.QrtrColumnValue,
                                         .QrtrTable = oldToothTRT.QrtrTable,
                                         .TrtGroupID = oldToothTRT.TrtGroupID,
                                         .TrtValue = trtVal,
                                         .PayValue = payVal,
                                         .UserID = CurrentUser.UsID}
        If TrtSourceHelper.IsStoredWholeMouthPatientRecord(_rowAtOpen) Then
            TRT.ToothNum = 0
            TRT.ToothName = TrtSourceHelper.FormatWholeMouthLabel(Eng)
        End If
        newToothTRT = TRT
        Return True
    End Function

    ''' <summary>Align IsPaid with price/pay like AddNewTrtForm.SaveTreatmentWithTransaction.</summary>
    ''' <param name="effectivePayTotal">When the pay box shows only the first installment, pass projected total of all payment rows after save; otherwise Nothing uses newT.PayValue.</param>
    Private Sub ApplyPayAndPaidFlags(newT As Patient_ToothTrt, ByRef payNote As String, Optional effectivePayTotal As Decimal? = Nothing)
        payNote = ""
        If newT.IsExternal Then
            newT.IsPaid = True
            Return
        End If
        Dim trtVal As Decimal = newT.TrtValue
        Dim payVal As Decimal = If(effectivePayTotal.HasValue, effectivePayTotal.Value, newT.PayValue)
        If payVal > 0 OrElse trtVal > 0 Then
            If payVal = trtVal Then
                payNote = "Payed In Full"
                newT.IsPaid = True
            ElseIf payVal < trtVal AndAlso payVal > 0 Then
                payNote = "Payed Partially"
                newT.IsPaid = True
            ElseIf payVal > trtVal Then
                payNote = "Payed With Advance Payment"
                newT.IsPaid = True
            End If
        ElseIf trtVal = 0 AndAlso payVal = 0 Then
            newT.IsPaid = False
        End If
    End Sub

    Private Function SaveEditWithRelatedTables(newT As Patient_ToothTrt, payNote As String) As Boolean
        If _rowAtOpen Is Nothing Then Return False
        Dim toothData As New Patient_ToothTrtDATA
        Dim cs = DentistXDATA.GetConnection.ConnectionString
        Using conn As New SqlConnection(cs)
            conn.Open()
            Using trans As SqlTransaction = conn.BeginTransaction()
                Try
                    If Not toothData.UpdateTransactional(conn, trans, _rowAtOpen, newT) Then
                        trans.Rollback()
                        Return False
                    End If

                    If newT.IsExternal Then
                        trans.Commit()
                        Return True
                    End If

                    Dim trtRow = conn.QuerySingleOrDefault(Of Patient_Trts)(
                        "SELECT TOP 1 * FROM Patient_Trts WHERE PatientID = @PatientID AND ToothTrtID = @ToothTrtID",
                        New With {.PatientID = _rowAtOpen.PatientID, .ToothTrtID = _rowAtOpen.ToothTrtID}, trans)

                    If trtRow Is Nothing Then
                        trans.Commit()
                        Return True
                    End If

                    Dim detailText = FormatPatientTrtsDetail(newT.Treat, newT.ToothNum)
                    conn.Execute(
                        "UPDATE Patient_Trts SET Detail = @Detail, TrtDate = @TrtDate, TrtValue = @TrtValue WHERE TrtID = @TrtID",
                        New With {
                            .Detail = detailText,
                            .TrtDate = If(newT.TreatDate.HasValue, newT.TreatDate.Value, Date.Today),
                            .TrtValue = newT.TrtValue,
                            .TrtID = trtRow.TrtID
                        }, trans)

                    Dim pays = conn.Query(Of Patient_Pays)(
                        "SELECT * FROM Patient_Pays WHERE TrtID = @TrtID ORDER BY PayID",
                        New With {.TrtID = trtRow.TrtID}, trans).ToList()

                    Dim payVal As Decimal = newT.PayValue
                    Dim payDate As Date = If(newT.TreatDate.HasValue, newT.TreatDate.Value, Date.Today)

                    If pays.Count = 0 Then
                        If payVal > 0 Then
                            conn.Execute(
                                "INSERT INTO Patient_Pays (TrtID, PatientID, PayValue, PayDate, Notes, PayType, ReceivedBy, IsReturned) VALUES (@TrtID, @PatientID, @PayValue, @PayDate, @Notes, @PayType, @ReceivedBy, @IsReturned)",
                                New With {
                                    .TrtID = trtRow.TrtID,
                                    .PatientID = newT.PatientID,
                                    .PayValue = payVal,
                                    .PayDate = payDate,
                                    .Notes = payNote,
                                    .PayType = "Cash",
                                    .ReceivedBy = CType(Nothing, String),
                                    .IsReturned = False
                                }, trans)
                        End If
                    Else
                        ' Pay box is bound to the first payment row (ORDER BY PayID); persist edits to that row.
                        Dim target = pays(0)
                        Dim newFirstVal = payVal
                        If newFirstVal < 0 Then newFirstVal = 0D
                        Dim noteOut As String = If(String.IsNullOrWhiteSpace(payNote), If(target.Notes, ""), payNote)
                        conn.Execute(
                            "UPDATE Patient_Pays SET PayValue = @PayValue, PayDate = @PayDate, Notes = @Notes WHERE PayID = @PayID",
                            New With {
                                .PayValue = newFirstVal,
                                .PayDate = payDate,
                                .Notes = noteOut,
                                .PayID = target.PayID
                            }, trans)
                    End If

                    trans.Commit()
                    Return True
                Catch
                    trans.Rollback()
                    Throw
                End Try
            End Using
        End Using
    End Function

    Private Sub btnSaveTrt_Click(sender As Object, e As EventArgs) Handles btnSaveTrt.Click
        Dim treatmentText, toothName As String
        If _rowAtOpen Is Nothing Then
            MessageBox.Show(If(Eng, "Treatment row is not loaded.", "لم يتم تحميل سجل العلاج."), "", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If
        Try
            If Not SetToothTrt(newToothTRT) Then Return
            treatmentText = newToothTRT.Treat
            toothName = newToothTRT.ToothName
            Dim payNote As String = ""
            Dim payTotalForFlags As Decimal? = Nothing
            Try
                Dim csFlags = DentistXDATA.GetConnection.ConnectionString
                Using connFlags As New SqlConnection(csFlags)
                    connFlags.Open()
                    Dim trtRowFlags = connFlags.QuerySingleOrDefault(Of Patient_Trts)(
                        "SELECT TOP 1 * FROM Patient_Trts WHERE PatientID = @PatientID AND ToothTrtID = @ToothTrtID ORDER BY TrtID",
                        New With {.PatientID = _rowAtOpen.PatientID, .ToothTrtID = _rowAtOpen.ToothTrtID})
                    If trtRowFlags IsNot Nothing Then
                        Dim paysFlags = connFlags.Query(Of Patient_Pays)(
                            "SELECT * FROM Patient_Pays WHERE TrtID = @TrtID ORDER BY PayID",
                            New With {.TrtID = trtRowFlags.TrtID}).ToList()
                        If paysFlags.Count > 1 Then
                            Dim oldSum = paysFlags.Sum(Function(p) p.PayValue)
                            payTotalForFlags = oldSum - paysFlags(0).PayValue + newToothTRT.PayValue
                        End If
                    End If
                End Using
            Catch
            End Try
            ApplyPayAndPaidFlags(newToothTRT, payNote, payTotalForFlags)

            If SaveEditWithRelatedTables(newToothTRT, payNote) Then
                Dim msgEn As String = $"Treatment '{treatmentText}' saved for {toothName}"
                Dim msgAr As String = $"تم حفظ العلاج '{treatmentText}' للسن {toothName}"
                Dim msg As String = If(Eng, msgEn, msgAr)
                Dim titleEn As String = "Success"
                Dim titleAr As String = "نجاح"
                Dim title As String = If(Eng, titleEn, titleAr)
                MessageBox.Show(msg, title, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Saved = True
                Deleted = False
                Dim patientToUpdate As Patient = If(FormManager.Instance IsNot Nothing, FormManager.Instance.GetCurrentPatient(), Nothing)
                If patientToUpdate Is Nothing Then patientToUpdate = PasswordSecurity.CurrentPatient
                If FormManager.Instance IsNot Nothing AndAlso FormManager.Instance.CurrentForm IsNot Nothing AndAlso patientToUpdate IsNot Nothing Then
                    FormManager.Instance.CurrentForm.UpdatePatientBalance(patientToUpdate)
                End If
                Me.DialogResult = DialogResult.OK
            Else
                Dim msgEn As String = $"Failed to save Treatment '{treatmentText}' for '{toothName}'."
                Dim msgAr As String = $"فشل في حفظ العلاج '{treatmentText}' للسن '{toothName}'"
                Dim msg As String = If(Eng, msgEn, msgAr)
                Dim titleEn As String = "Error"
                Dim titleAr As String = "خطأ"
                Dim title As String = If(Eng, titleEn, titleAr)
                MessageBox.Show(msg, title, MessageBoxButtons.OK, MessageBoxIcon.Error)
                Saved = False
                Deleted = False
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, If(Eng, "Save error", "خطأ في الحفظ"), MessageBoxButtons.OK, MessageBoxIcon.Error)
            Saved = False
        End Try
        'Else
        '    MessageBox.Show($"Failed to save Treatment '{treatmentText}' for '{toothName}'.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '    Saved = False
        '    Deleted = False
        'End If


    End Sub

    Private Sub btnDelTrt_Click(sender As Object, e As EventArgs) Handles btnDelTrt.Click
        Dim toothTrtData = New Patient_ToothTrtDATA()
        Dim msgEn As String = "Are you sure you want to delete this treatment?"
        Dim msgAr As String = "هل أنت متأكد أنك تريد حذف هذا العلاج؟"
        Dim msg As String = If(Eng, msgEn, msgAr)
        Dim titleEn As String = "Delete"
        Dim titleAr As String = "حذف"
        Dim title As String = If(Eng, titleEn, titleAr)
        If MessageBox.Show(msg, title, MessageBoxButtons.YesNo) = DialogResult.Yes Then
            Dim rowToDelete As Patient_ToothTrt = If(_rowAtOpen IsNot Nothing, _rowAtOpen, oldToothTRT)
            If rowToDelete Is Nothing Then Return
            If toothTrtData.Delete(rowToDelete) Then
                Dim msgSuccessEn As String = "Treatment deleted and saved successfully."
                Dim msgSuccessAr As String = "تم حذف العلاج وحفظه بنجاح."
                Dim msgSuccess As String = If(Eng, msgSuccessEn, msgSuccessAr)
                Dim titleSuccessEn As String = "Success"
                Dim titleSuccessAr As String = "نجاح"
                Dim titleSuccess As String = If(Eng, titleSuccessEn, titleSuccessAr)
                MessageBox.Show(msgSuccess, titleSuccess)
                Deleted = True
                Saved = True
                Dim patientToUpdate As Patient = If(FormManager.Instance IsNot Nothing, FormManager.Instance.GetCurrentPatient(), Nothing)
                If patientToUpdate Is Nothing Then patientToUpdate = PasswordSecurity.CurrentPatient
                If FormManager.Instance IsNot Nothing AndAlso FormManager.Instance.CurrentForm IsNot Nothing AndAlso patientToUpdate IsNot Nothing Then
                    FormManager.Instance.CurrentForm.UpdatePatientBalance(patientToUpdate)
                End If
                Me.DialogResult = DialogResult.OK
            Else
                Dim msgFailEn As String = "Failed to delete treatment."
                Dim msgFailAr As String = "فشل في حذف العلاج."
                Dim msgFail As String = If(Eng, msgFailEn, msgFailAr)
                Dim titleFailEn As String = "Error"
                Dim titleFailAr As String = "خطأ"
                Dim titleFail As String = If(Eng, titleFailEn, titleFailAr)
                MessageBox.Show(msgFail, titleFail)
                Deleted = False
                Saved = False
            End If
        End If
        'Else
        '    If toothTrtData.DeleteTrans(newToothTRT) Then
        '        MessageBox.Show("Treatment deleted and saved successfully.", "Success")
        '        MessageBox.Show($"Check the GRID View for Tooth Number {newToothTRT.ToothNum}.", "Error Deleting From Grid")
        '        Deleted = True
        '        Saved = True
        '        Me.DialogResult = DialogResult.OK
        '    Else

        '        MessageBox.Show("Failed to delete treatment.", "Error")
        '        Deleted = False
        '        Saved = False
        '    End If
        'End If

        'Else
        '    Dim result As String = newToothTRT.Treat & vbCrLf & Format(newToothTRT.TreatDate, "dd-MM-yyyy")
        '    If DelTreatFromQuadrantTables(newToothTRT.PatientID, newToothTRT.ToothNum, result) Then
        '        If toothTrtData.DeleteTrans(newToothTRT) Then
        '            MessageBox.Show("Treatment deleted and saved successfully.", "Success")
        '            Deleted = True
        '            Saved = True
        '            Me.DialogResult = DialogResult.OK
        '        Else

        '            MessageBox.Show("Failed to delete treatment.", "Error")
        '            Deleted = False
        '            Saved = False
        '        End If
        '    Else
        '        If toothTrtData.DeleteTrans(newToothTRT) Then
        '            MessageBox.Show("Treatment deleted and saved successfully.", "Success")
        '            MessageBox.Show($"Check the GRID View for Tooth Number {newToothTRT.ToothNum}.", "Error Deleting From Grid")
        '            Deleted = True
        '            Saved = True
        '            Me.DialogResult = DialogResult.OK
        '        Else

        '            MessageBox.Show("Failed to delete treatment.", "Error")
        '            Deleted = False
        '            Saved = False
        '        End If
        '    End If
        'End If
        '    End If
        'End If
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Dim cancelEn As String = "Cancel The Operation."
        Dim cancelAr As String = "إلغاء العملية."
        Dim cancelMsg As String = If(Eng, cancelEn, cancelAr)
        Dim titleEn As String = "Cancel"
        Dim titleAr As String = "إلغاء"
        Dim title As String = If(Eng, titleEn, titleAr)
        MessageBox.Show(cancelMsg, title, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Me.DialogResult = DialogResult.Cancel
        Saved = False
        Deleted = False
        Canceled = True
    End Sub

    ' Read-only property for Saved
    Public Property Saved As Boolean


    ' Read-only property for Deleted
    Public Property Deleted As Boolean


    ' Read-only property for _Canceled
    Public Property Canceled As Boolean
    Dim specialTrts As String() = {"INDIRECT PULP CAPPING", "DIRECT PULP CAPPING", "PULPOTOMY"}


    Dim alpha As Byte = 0
    Dim clrWithOpacity As Color ' = Color.FromArgb(128, clrFillColor.Color.R, clrFillColor.Color.G, clrFillColor.Color.B)
    Dim brdrClrWithOpacity As Color
    Dim capFill As Color
    Dim rootFill As Color
    Dim _alphaCap As Byte = 128


    ' 128 is 50% opacity (range is 0-255)
    'Dim rgbaString As String = $"rgba({clrWithOpacity.R}, {clrWithOpacity.G}, {clrWithOpacity.B}, {clrWithOpacity.A / 255.0})"
    'Console.WriteLine(rgbaString) ' Example: "rgba(255, 0, 0, 0.5)"
    'Dim hexWithAlpha As String = $"#{clrWithOpacity.A:X2}{clrWithOpacity.R:X2}{clrWithOpacity.G:X2}{clrWithOpacity.B:X2}"
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

    Private Sub btnApply_Click(sender As Object, e As EventArgs) Handles btnApply.Click
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
        Using conn As New SqlConnection(DentistXDATA.GetConnection.ConnectionString)
            conn.Open()
            ' Check if any record exists with this treatment
            TrtID = conn.ExecuteScalar(Of Integer?)(CheckTrtID, New With {.Trt = Trt}) '.HasValue
        End Using
        If IsValidHexColor(ColorToHex(CapFillPick.Color, capOpacity.Value)) AndAlso IsValidHexColor(ColorToHex(RootFillPick.Color, capOpacity.Value)) Then
            If clsTblTrtDATA.UpdateTrtClr(TrtID, ColorToHex(CapFillPick.Color, capOpacity.Value), ColorToHex(RootFillPick.Color, capOpacity.Value), capRootThick.Value) Then
                ' Also update the current in-memory treatment so drawing reflects immediately
                Dim newCapHex = ColorToHex(CapFillPick.Color, capOpacity.Value)
                Dim newRootHex = ColorToHex(RootFillPick.Color, capOpacity.Value)
                If oldToothTRT IsNot Nothing Then
                    oldToothTRT.CapFill = newCapHex
                    oldToothTRT.RootFill = newRootHex
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
    Private Sub CapFillPick_EditValueChanged(sender As Object, e As EventArgs) Handles CapFillPick.EditValueChanged
        capFill = Color.FromArgb(_alphaCap, CapFillPick.Color.R, CapFillPick.Color.G, CapFillPick.Color.B)
    End Sub

    Private Sub RootFillPick_EditValueChanged(sender As Object, e As EventArgs) Handles RootFillPick.EditValueChanged
        rootFill = Color.FromArgb(_alphaCap, RootFillPick.Color.R, RootFillPick.Color.G, RootFillPick.Color.B)
    End Sub
    Private Sub capOpacity_EditValueChanged(sender As Object, e As EventArgs) Handles capOpacity.EditValueChanged
        _alphaCap = CByte(capOpacity.Value)
        CapFillPick.Color = Color.FromArgb(_alphaCap, CapFillPick.Color.R, CapFillPick.Color.G, CapFillPick.Color.B)
        RootFillPick.Color = Color.FromArgb(_alphaCap, RootFillPick.Color.R, RootFillPick.Color.G, RootFillPick.Color.B)
    End Sub
    Private Sub tbOpacity_EditValueChanged(sender As Object, e As EventArgs) Handles tbOpacity.EditValueChanged
        'alpha = CByte(tbOpacity.Value)
        'clrFillColor.Color = Color.FromArgb(alpha, clrFillColor.Color.R, clrFillColor.Color.G, clrFillColor.Color.B)
        alpha = CByte(tbOpacity.Value)
        clrFillColor.Color = Color.FromArgb(alpha, clrFillColor.Color.R, clrFillColor.Color.G, clrFillColor.Color.B)
        clrBorderColor.Color = Color.FromArgb(alpha, clrBorderColor.Color.R, clrBorderColor.Color.G, clrBorderColor.Color.B)
    End Sub

    Private Sub clrFillColor_EditValueChanged(sender As Object, e As EventArgs) Handles clrFillColor.EditValueChanged
        clrWithOpacity = Color.FromArgb(alpha, clrFillColor.Color.R, clrFillColor.Color.G, clrFillColor.Color.B)
    End Sub
    Private Sub clrBorderColor_EditValueChanged(sender As Object, e As EventArgs) Handles clrBorderColor.EditValueChanged
        brdrClrWithOpacity = Color.FromArgb(alpha, clrBorderColor.Color.R, clrBorderColor.Color.G, clrBorderColor.Color.B)
    End Sub

    Private Sub btTrtClrDef_Click(sender As Object, e As EventArgs) Handles btTrtClrDef.Click
        If txtTreat.Text.Length = 0 Then Exit Sub

        Dim TrtClr As String = ""
        Dim Trt As String = Trim(txtTreat.Text.Trim)
        ' Define the connection string
        Dim connectionString As String = DentistXDATA.GetConnection.ConnectionString

        ' Define the SQL query
        Dim query As String = "SELECT [TrtID],[Trt][TrtDetails],[ToothID],[OldTrt],[TrtGroup],[DefFillColor]
                            FROM [TblTRTS] 
                            WHERE Trt = @Trt"

        ' Create and open a connection
        Using connection As New SqlConnection(connectionString)
            connection.Open()

            ' Create a command with the parameterized query
            Dim command As New SqlCommand(query, connection)
            command.Parameters.AddWithValue("@Trt", Trt)

            ' Create a DataAdapter to fill a DataTable
            Dim adapter As New SqlDataAdapter(command)
            Dim dataTable As New DataTable()
            adapter.Fill(dataTable)

            ' If there are rows, set TrtClr to the first treatment's color
            If dataTable.Rows.Count > 0 Then
                TrtClr = dataTable.Rows(0)("DefFillColor").ToString()
            End If
            clrFillColor.Color = ColorTranslator.FromHtml(TrtClr)
        End Using
    End Sub

    Private Sub btCapRootClrDef_Click(sender As Object, e As EventArgs) Handles btCapRootClrDef.Click
        If txtTreat.Text.Length = 0 Then Exit Sub
        If Not specialTrts.Contains(txtTreat.Text.ToUpper()) Then Exit Sub
        CapFillPick.Color = GetDefaultCapColor(txtTreat.Text)
        RootFillPick.Color = GetDefaultRootColor(txtTreat.Text)
    End Sub

    '============================================================================================================



    '========

#Region "Implants"





    Dim _formattedResult As String = ""
    Dim _normalResult As String = ""


    Dim ImpBrand, ImpType, ImpDmm, ImpLmm, Slim As String


    Private Sub SetResult()
        Dim baseTreat = ImplantTreatBracketHelper.TreatWithoutImplantBracketSuffix(txtTreat.Text)
        _formattedResult =
                            $"{baseTreat} " &
                            $"{ImpBrand}-{ImpType}{vbCrLf}" &
                            $"{Slim} - {ImpDmm}x{ImpLmm}"
        ResultLbl.Text = _formattedResult
        _normalResult = $" [{ImpBrand}-{ImpType}-{Slim} {ImpDmm}x{ImpLmm}]"

    End Sub

    ''' <summary>
    ''' Sets ImpBrand, ImpType, Slim, ImpDmm, and ImpLmm from a full Treat string (same bracket format as SetResult / btnSelect).
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
            FillCombos()
        Else
            ImpBrand = ""
            ImpType = ""
            Slim = ""
            ImpDmm = ""
            ImpLmm = ""
        End If
    End Sub
    Private Sub FillCombos()
        Try
            cmbBrand.SetSelectedBrandID(ImpBrand)
            cmbType.SetSelectedTypeID(ImpType)
            cmbDiameter.SetSelectedDiameterID(ImpDmm)
            cmbLength.SetSelectedLengthID(ImpLmm)
            cmbDesign.SelectedItem = Slim
            SetResult()
        Catch
        End Try
    End Sub
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

    Private Sub ApplyImplantWhenPopupOpens()
        Dim t = txtTreat.Text
        If ImplantTreatBracketHelper.ImplantSpecsPresentInTreat(t) Then
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

    Private Sub ImpPopup_QueryPopUp(sender As Object, e As CancelEventArgs) Handles ImpPopup.QueryPopUp
        BeginInvoke(New MethodInvoker(AddressOf ApplyImplantWhenPopupOpens))
    End Sub

    Private Sub ImpContainer_VisibleChanged(sender As Object, e As EventArgs) Handles ImpContainer.VisibleChanged
        If Not ImpContainer.Visible Then Return
        BeginInvoke(New MethodInvoker(AddressOf ApplyImplantWhenPopupOpens))
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











    'Private Sub impCheck_CheckedChanged(sender As Object, e As EventArgs) Handles impCheck.CheckedChanged
    '    ImpPopup.Visible = impCheck.Checked ' _propName.Contains("IMPLANT")
    '    ImplantSpecsLbl.Visible = impCheck.Checked ' _propName.Contains("IMPLANT")
    'End Sub

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
        'txtTrtDetails.Text = _formattedResult
        ImpPopup.ClosePopup()
    End Sub

    Private Sub btnCancelImp_Click(sender As Object, e As EventArgs) Handles btnCancelImp.Click
        ImpPopup.Text = ""
        txtTreat.Text = "IMPLANT"
        ImpPopup.ClosePopup()
    End Sub

    'Private Sub ImpPopup_MouseClick(sender As Object, e As MouseEventArgs) Handles ImpPopup.MouseClick
    '    ImpPopup.ShowPopup()
    'End Sub

#End Region


End Class