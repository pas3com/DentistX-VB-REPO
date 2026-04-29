Imports System.Data.SqlClient
Imports System.Drawing
Imports System.Windows.Forms
Imports Dapper
Imports DevExpress.Utils

Public Class EditDiagFrom

    ''' <summary>Stored in DB as English; UI may show Arabic when Eng is False.</summary>
    Private Const TrtTypeOneStageEn As String = "One Stage"
    Private Const TrtTypeMultipleStageEn As String = "Multiple Stages"
    Private Const TrtTypeOneStageAr As String = "مرحلة واحدة"
    Private Const TrtTypeMultipleStageAr As String = "عدة مراحل"

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()
        Me.Icon = AppIcon

        ' Add any initialization after the InitializeComponent() call.
        ' One Stage Size 385, 327
        'Multi Stage Size 911, 327
    End Sub
    'One Stage     مرحلة واحدة    'Multiple Stage    عدة مراحل

    Public Sub New(ByVal clsToothTrt As Patient_Diagnosis, ByVal clsPatient As Patient)

        ' This call is required by the designer.
        InitializeComponent()
        Me.Icon = AppIcon
        ' Add any initialization after the InitializeComponent() call.

        'txtTrtID.Text = clsToothTrt.DiagID
        txtPatientID.Text = clsToothTrt.PatientID
        txtPatientName.Text = clsPatient.PatientName
        'txtToothNum.Text = clsToothTrt.ToothNum
        'txtToothName.Enabled = True
        'txtToothName.Text = clsToothTrt.ToothName
        ''txtToothName.Enabled = False
        'txtTreat.Text = clsToothTrt.Treat
        'ztBorderThickness.Value = clsToothTrt.BorderThickness
        clrFillColor.Color = ColorTranslator.FromHtml(clsToothTrt.FillColor)
        'clrBorderColor.Color = ColorTranslator.FromHtml(clsToothTrt.BorderColor)
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
        oldToothDiag = clsToothTrt
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

        TrtBS.DataSource = clsToothTrt
    End Sub

    Private Sub EditDiagFrom_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If TrtBS.DataSource IsNot Nothing AndAlso TrtBS.Count > 0 Then
            TrtBS.Position = 0
        End If
        ConfigureTreatmentTypeCombo()
        Dim row As Patient_Diagnosis = TryCast(TrtBS.Current, Patient_Diagnosis)
        If row Is Nothing AndAlso TrtBS.Count > 0 Then
            row = TryCast(TrtBS.Item(0), Patient_Diagnosis)
        End If
        ' Do not call TrtBS.ResetBindings here: it refreshes every DevExpress bound editor and often
        ' drops Appearance (e.g. Bold on MemoEdits/Combo) and can reset the stage combo selection.
        ApplyTreatmentTypeComboFromRow(row)
    End Sub

    Private Sub EditDiagFrom_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        Dim row As Patient_Diagnosis = TryCast(TrtBS.Current, Patient_Diagnosis)
        If row Is Nothing AndAlso TrtBS.Count > 0 Then row = TryCast(TrtBS.Item(0), Patient_Diagnosis)
        ApplyTreatmentTypeComboFromRow(row)
        BeginInvoke(New MethodInvoker(AddressOf ReapplyTreatmentTypeComboDeferred))
    End Sub

    Private Sub ReapplyTreatmentTypeComboDeferred()
        Dim row As Patient_Diagnosis = TryCast(TrtBS.Current, Patient_Diagnosis)
        If row Is Nothing AndAlso TrtBS.Count > 0 Then row = TryCast(TrtBS.Item(0), Patient_Diagnosis)
        ApplyTreatmentTypeComboFromRow(row)
    End Sub

    ''' <summary>While setting SelectedIndex in code, skip syncing back to the row (avoids re-entrancy).</summary>
    Private _applyingTreatmentTypeIndex As Boolean

    Private Sub ConfigureTreatmentTypeCombo()
        cboTrtType.Properties.Items.Clear()
        If Eng Then
            cboTrtType.Properties.Items.AddRange({TrtTypeOneStageEn, TrtTypeMultipleStageEn})
        Else
            cboTrtType.Properties.Items.AddRange({TrtTypeOneStageAr, TrtTypeMultipleStageAr})
        End If

        '' WinForms Text/EditValue binding + DevExpress item matching often falls back to SelectedIndex 0; sync by index instead.
        'cboTrtType.DataBindings.Clear()
    End Sub

    Private Sub ApplyTreatmentTypeComboFromRow(row As Patient_Diagnosis)
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
        End Try
    End Sub

    Private Sub cboTrtType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboTrtType.SelectedIndexChanged
        If _applyingTreatmentTypeIndex Then Return
        FlushTreatmentTypeEnglishFromComboToRow()
    End Sub

    Private Sub FlushTreatmentTypeEnglishFromComboToRow()
        Dim row As Patient_Diagnosis = TryCast(TrtBS.Current, Patient_Diagnosis)
        If row Is Nothing AndAlso TrtBS.Count > 0 Then row = TryCast(TrtBS.Item(0), Patient_Diagnosis)
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
        If tl.Contains("Multiple") Then Return TrtTypeMultipleStageEn
        If tl.Contains("Multiple Stages") OrElse tl.StartsWith("Multiple") Then Return TrtTypeMultipleStageEn
        If tl.Contains("One Stage") OrElse (tl.Contains("One") AndAlso tl.Contains("Stage")) Then Return TrtTypeOneStageEn
        If tl.Contains("مرحلة") AndAlso (tl.Contains("واحدة")) Then Return TrtTypeOneStageEn
        If tl.Contains("عدة") OrElse tl.Contains("مراحل") Then Return TrtTypeMultipleStageEn

        Return t
    End Function

    Dim ShapeID As Integer = 0
    Dim propName As String = ""
    Dim oldTrt As String = ""
    Dim trtID As Integer = 0
    Dim treatmentText As String = ""
    Private TrtClr As String = ""
    Private ShapeName As String = ""

    Dim toothDiagData As New Patient_DiagnosisDATA
    Dim newToothDiag As New Patient_Diagnosis
    Dim oldToothDiag As New Patient_Diagnosis
    'Dim toothHistory As New Patient_ToothTrtHistory
    'Dim toothHisData As New Patient_ToothTrtHistoryDATA



    Private Sub txtTreat_EditValueChanged(sender As Object, e As EventArgs) Handles txtTreat.EditValueChanged
        ' Define the connection string
        Dim connectionString As String = DentistXDATA.GetConnection.ConnectionString

        ' Query to get the TrtColor for the selected treatment
        Dim query As String = "SELECT TrtColor FROM TblTRTS WHERE Trt = @Trt"

        ' Use a database connection to execute the query
        Using connection As New SqlClient.SqlConnection(connectionString)
            connection.Open()
            Using command As New SqlClient.SqlCommand(query, connection)
                ' Add parameter to avoid SQL injection
                command.Parameters.AddWithValue("@Trt", txtTreat.Text)

                ' Execute the query and read the result
                Dim result = command.ExecuteScalar()
                If result IsNot Nothing AndAlso Not String.IsNullOrEmpty(result.ToString()) Then
                    Dim colorString As String = result.ToString().Trim()

                    ' Ensure the color string starts with '#' if it doesn't already
                    If Not colorString.StartsWith("#") Then
                        colorString = "#" & colorString
                    End If

                    ' Convert the color from the database to a Color object
                    clrFillColor.Color = ColorTranslator.FromHtml(colorString)
                Else
                    ' Handle case where no color is found
                    clrFillColor.Color = Color.Empty ' Default to no color
                End If
            End Using
        End Using
    End Sub

    Private Sub btnExit_Click(sender As Object, e As EventArgs)
        Me.Dispose()
    End Sub

    Private Function SetToothTrt(TRT As Patient_Diagnosis) As Boolean
        FlushTreatmentTypeEnglishFromComboToRow()
        TrtBS.EndEdit()
        Dim cur As Patient_Diagnosis = TryCast(TrtBS.Current, Patient_Diagnosis)
        Dim treatmentTypeEnglish = NormalizeTreatmentTypeToEnglish(
            If(cur IsNot Nothing AndAlso Not String.IsNullOrWhiteSpace(cur.TreatmentType), cur.TreatmentType, CurrentTreatmentTypeEnglishFromCombo()))

        Dim titleEn As String = "Field Required"
        Dim titleAr As String = "حقل مطلوب"
        Dim title As String = If(Eng, titleEn, titleAr)

        Dim TreatString As String = txtTreat.Text
        If TreatString.StartsWith("IMPLANT") Then
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
        If chkIsExternal.Checked = True Then
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

        TRT = New Patient_Diagnosis With {.BorderColor = ColorTranslator.ToHtml(clrBorderColor.Color),
                                         .BorderThickness = CByte(ztBorderThickness.Value),
                                         .FillColor = hexWithAlpha,' ColorTranslator.ToHtml(clrFillColor.Color),
                                         .DiagID = CInt(txtTrtID.Text),
                                         .PatientID = PatientID,
                                         .ShapeID = GetShapeIDByTrt(TreatString),
                                         .LVL = GetLVL(TreatString),
                                         .PropertyName = GetTrtShape(TreatString), ' txtTreat.Text.Replace(" ", "_"),
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
                                         .IsExternal = chkIsExternal.Checked,
                                         .ExternalClinicName = clinicName,
                                         .QrtrAddress = oldToothDiag.QrtrAddress,
                                         .QrtrID = oldToothDiag.QrtrID,
                                         .TrtLoc = oldToothDiag.TrtLoc,
                                         .ExternalTreatmentDate = oldToothDiag.ExternalTreatmentDate,
                                         .IsMultiTrt = oldToothDiag.IsMultiTrt,
                                         .isOnImplant = oldToothDiag.isOnImplant,
                                         .IsPaid = oldToothDiag.IsPaid,
                                         .ParentDiagID = oldToothDiag.ParentDiagID,
                                         .QrtrColumnName = oldToothDiag.QrtrColumnName,
                                         .QrtrColumnValue = oldToothDiag.QrtrColumnValue,
                                         .QrtrTable = oldToothDiag.QrtrTable,
                                         .TrtGroupID = oldToothDiag.TrtGroupID,
                                         .UserID = CurrentUser.UsID}
        newToothDiag = TRT
        Return True
    End Function


    Private Sub btnSaveTrt_Click(sender As Object, e As EventArgs) Handles btnSaveTrt.Click
        'Dim newTrtID As Integer
        Dim treatmentText, toothName As String
        If SetToothTrt(newToothDiag) Then
            treatmentText = newToothDiag.Treat
            toothName = newToothDiag.ToothName
            'If UpdateTreatINQrtrTable(newToothDiag.PatientID, newToothDiag.ToothNum, newToothDiag.QrtrTable, newToothDiag.QrtrID, newToothDiag.QrtrAddress, newToothDiag.Treat) Then
            If toothDiagData.Update(oldToothDiag, newToothDiag) Then
                Dim msgEn As String = $"Treatment '{treatmentText}' saved for {toothName}"
                Dim msgAr As String = $"تم حفظ العلاج '{treatmentText}' للسن {toothName}"
                Dim msg As String = If(Eng, msgEn, msgAr)
                Dim titleEn As String = "Success"
                Dim titleAr As String = "نجاح"
                Dim title As String = If(Eng, titleEn, titleAr)
                MessageBox.Show(msg, title, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Saved = True
                Deleted = False
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
        End If



    End Sub

    Private Sub btnDelTrt_Click(sender As Object, e As EventArgs) Handles btnDelTrt.Click
        Dim toothDiagData = New Patient_DiagnosisDATA()
        Dim msgEn As String = "Are you sure you want to delete this treatment?"
        Dim msgAr As String = "هل أنت متأكد أنك تريد حذف هذا العلاج؟"
        Dim msg As String = If(Eng, msgEn, msgAr)
        Dim titleEn As String = "Delete"
        Dim titleAr As String = "حذف"
        Dim title As String = If(Eng, titleEn, titleAr)
        If MessageBox.Show(msg, title, MessageBoxButtons.YesNo) = DialogResult.Yes Then

            If txtPatientID.Text.Length = 0 Then
                    msgEn = "Patient Number is missing."
                    msgAr = "رقم المريض مفقود."
                    msg = If(Eng, msgEn, msgAr)
                    MessageBox.Show(msg, title, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Return
                End If
                If txtTrtID.Text.Length = 0 Then
                    msgEn = "Treatment ID is missing."
                    msgAr = "معرف العلاج مفقود."
                    msg = If(Eng, msgEn, msgAr)
                    MessageBox.Show(msg, title, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Return
                End If
                ' Update the corresponding table
                'If Not String.IsNullOrEmpty(newToothDiag.QrtrTable) AndAlso newToothDiag.QrtrID > 0 AndAlso newToothDiag.QrtrAddress > 0 Then
                '    If DeleteTreatAndStylesINQrtrTable(newToothDiag.PatientID, newToothDiag.ToothNum, newToothDiag.QrtrTable, newToothDiag.QrtrID, newToothDiag.QrtrAddress, newToothDiag.Treat) Then
                If toothDiagData.Delete(oldToothDiag) Then
                    Dim msgSuccessEn As String = "Treatment deleted and saved successfully."
                    Dim msgSuccessAr As String = "تم حذف العلاج وحفظه بنجاح."
                    Dim msgSuccess As String = If(Eng, msgSuccessEn, msgSuccessAr)
                    Dim titleSuccessEn As String = "Success"
                    Dim titleSuccessAr As String = "نجاح"
                    Dim titleSuccess As String = If(Eng, titleSuccessEn, titleSuccessAr)
                    MessageBox.Show(msgSuccess, titleSuccess)
                    Deleted = True
                    Saved = True
                    FormManager.Instance.CurrentForm.UpdatePatientBalance(CurrentPatient)
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


    Dim alpha As Byte = 0
    Dim clrWithOpacity As Color ' = Color.FromArgb(128, clrFillColor.Color.R, clrFillColor.Color.G, clrFillColor.Color.B)
    ' 128 is 50% opacity (range is 0-255)
    'Dim rgbaString As String = $"rgba({clrWithOpacity.R}, {clrWithOpacity.G}, {clrWithOpacity.B}, {clrWithOpacity.A / 255.0})"
    'Console.WriteLine(rgbaString) ' Example: "rgba(255, 0, 0, 0.5)"
    'Dim hexWithAlpha As String = $"#{clrWithOpacity.A:X2}{clrWithOpacity.R:X2}{clrWithOpacity.G:X2}{clrWithOpacity.B:X2}"


    Private Sub tbOpacity_EditValueChanged(sender As Object, e As EventArgs) Handles tbOpacity.EditValueChanged
        alpha = CByte(tbOpacity.Value)
        clrFillColor.Color = Color.FromArgb(alpha, clrFillColor.Color.R, clrFillColor.Color.G, clrFillColor.Color.B)
    End Sub

    Private Sub clrFillColor_EditValueChanged(sender As Object, e As EventArgs) Handles clrFillColor.EditValueChanged
        clrWithOpacity = Color.FromArgb(alpha, clrFillColor.Color.R, clrFillColor.Color.G, clrFillColor.Color.B)
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



    '============================================================================================================



    '========

#Region "Implants"





    Dim _formattedResult As String = ""
    Dim _normalResult As String = ""


    Dim ImpBrand, ImpType, ImpDmm, ImpLmm, Slim As String


    Private Sub SetResult()

        _formattedResult =
                            $"IMPLANT " &
                            $"{ImpBrand}-{ImpType}{vbCrLf}" &
                            $"{Slim} - {ImpDmm}x{ImpLmm}"
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

    'Private Sub ImpPopup_MouseClick(sender As Object, e As MouseEventArgs) Handles ImpPopup.MouseClick
    '    ImpPopup.ShowPopup()
    'End Sub

#End Region


End Class