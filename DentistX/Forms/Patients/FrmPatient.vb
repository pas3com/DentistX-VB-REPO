Imports System.ComponentModel
Imports DevExpress.XtraBars
Imports System.ComponentModel.DataAnnotations
Imports DevExpress.XtraGrid.Views.Base
Imports System.Windows.Forms

Partial Public Class FrmPatient


    Public Sub New()
        InitializeComponent()
        bsiRecordsCount.Caption = "RECORDS : " & dgView.RowCount
    End Sub

    Private Sub dgView_CustomUnboundColumnData(sender As Object, e As CustomColumnDataEventArgs) Handles dgView.CustomUnboundColumnData
        If e.Column.Name = "colRowNum" Then
            e.Value = e.ListSourceRowIndex + 1
        End If
    End Sub

    Private Sub bbiPrintPreview_ItemClick(ByVal sender As Object, ByVal e As ItemClickEventArgs) Handles bbiPrintPreview.ItemClick
        DGV.ShowRibbonPrintPreview()
    End Sub
    Private clsPatientData As New PatientDATA
    Private clsDentistXData As New DentistXDATA


    Private Sub FrmPatient_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        FillCboPrefixOnce()
        LoadDGV()
        ShowToolStripItems("Cancel")

        ' After first load, if there are patients, show the first one immediately
        If dgView IsNot Nothing AndAlso dgView.RowCount > 0 Then
            Try
                Row = 0
                dgView.FocusedRowHandle = 0
                Display()
            Catch
            End Try
        End If

        ' Enable DevExpress built?in search panel to search patients by name (and other columns)
        dgView.OptionsFind.AlwaysVisible = True
        dgView.OptionsFind.FindNullPrompt = "Search patient by name..."

        If Eng Then
            CboSex.Properties.Items.AddRange({"Male", "Female"})
        Else
            CboSex.Properties.Items.AddRange({"ذكر", "أنثى"})
        End If
        CboSex.SelectedIndex = 0
    End Sub
    Private bAddMode As Boolean = False
    Private bEditMode As Boolean = False
    Private bDeleteMode As Boolean = False
    Private iRow As Int32 = 0

    Public Property AddMode() As Boolean
        Get
            Return bAddMode
        End Get
        Set(ByVal value As Boolean)
            bAddMode = value
        End Set
    End Property

    Public Property EditMode() As Boolean
        Get
            Return bEditMode
        End Get
        Set(ByVal value As Boolean)
            bEditMode = value
        End Set
    End Property

    Public Property DeleteMode() As Boolean
        Get
            Return bDeleteMode
        End Get
        Set(ByVal value As Boolean)
            bDeleteMode = value
        End Set
    End Property

    Public Property Row() As Int32
        Get
            Return iRow
        End Get
        Set(ByVal value As Int32)
            iRow = value
        End Set
    End Property

    Private Sub LoadDGV()
        Try
            With DGV
                .DataSource = clsPatientData.SelectAll
                bsiRecordsCount.Caption = "RECORDS : " & .DataSource.Count
                If dgView.RowCount = 0 Then
                    ShowToolStripItems("No Record")
                Else
                    ClearRecord()
                End If
            End With
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub DGV_Click(sender As Object, e As System.EventArgs) Handles DGV.Click
        Try
            If dgView Is Nothing OrElse Not dgView.IsDataRow(dgView.FocusedRowHandle) Then Return
            Row = dgView.FocusedRowHandle
            Display()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub DGV_SelectionChanged(sender As Object, e As System.EventArgs) Handles dgView.SelectionChanged
        Try
            If dgView Is Nothing OrElse Not dgView.IsDataRow(dgView.FocusedRowHandle) Then Return
            Row = dgView.FocusedRowHandle
            Display()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    ''' <summary>Reads PatientID from the grid row using the bound cell value (not display text), so Find/search and formatting cannot break parsing.</summary>
    Private Function TryGetPatientIdFromRowHandle(rowHandle As Integer, ByRef patientId As Integer) As Boolean
        patientId = 0
        If dgView Is Nothing OrElse Not dgView.IsDataRow(rowHandle) Then Return False
        Dim v = dgView.GetRowCellValue(rowHandle, colPatientID)
        If v Is Nothing OrElse IsDBNull(v) Then Return False
        If TypeOf v Is Integer Then
            patientId = DirectCast(v, Integer)
            Return True
        End If
        If TypeOf v Is Long Then
            patientId = CInt(CLng(v))
            Return True
        End If
        Return Integer.TryParse(Convert.ToString(v), patientId)
    End Function

    Private Sub EnableRecord(ByVal YesNo As Boolean)
        'PatientIDSpinEdit.Enabled = YesNo
        PatientNameTextEdit.Enabled = YesNo
        SexTextEdit.Enabled = YesNo
        CboSex.Enabled = YesNo
        AgeSpinEdit.Enabled = YesNo
        PhoneTextEdit.Enabled = YesNo
        AddressTextEdit.Enabled = YesNo
        CboAddress.Enabled = YesNo
        HealthTextEdit.Enabled = YesNo
        CboHlthStat.Enabled = YesNo
        TreatCheckEdit.Enabled = YesNo
        DiagCheckEdit.Enabled = YesNo
        OrthoCheckEdit.Enabled = YesNo
        NotesTextEdit.Enabled = YesNo
        txtWhats.Enabled = YesNo
        cboPrefix.Enabled = YesNo
    End Sub

    Private Sub butCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butCancel.Click
        ShowToolStripItems("Cancel")
    End Sub

    Private Sub butClose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butClose.Click
        Me.Close()
    End Sub

    Private Sub butApply_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butApply.Click
        If Me.AddMode = True Then
            Me.InsertRecord()
        ElseIf Me.EditMode = True Then
            Me.UpdateRecord()
        ElseIf Me.DeleteMode = True Then
            Me.DeleteRecord()
        End If
    End Sub
    Private Sub Display()
        ClearRecord()
        Dim pid As Integer
        If Not TryGetPatientIdFromRowHandle(Row, pid) Then Return
        Dim clsPatient As New Patient
        clsPatient.PatientID = pid
        clsPatient = clsPatientData.Select_Record(clsPatient)
        If Not clsPatient Is Nothing Then
            Try
                PatientIDSpinEdit.Value = System.Convert.ToInt32(clsPatient.PatientID)
                PatientNameTextEdit.Text = Convert.ToString(clsPatient.PatientName)
                SexTextEdit.Text = Convert.ToString(clsPatient.Sex)
                CboSex.Text = Convert.ToString(clsPatient.Sex)
                AgeSpinEdit.Value = System.Convert.ToInt32(clsPatient.Age)

                ' WhatsApp prefix + local number (centralized bind)
                Try
                    FillCboPrefixOnce()
                    WhatsHelper.BindPatientWhatsPrefixAndLocal(cboPrefix, txtWhats, clsPatient)
                    RefreshLblWhats()
                Catch
                End Try

                PhoneTextEdit.Text = Convert.ToString(clsPatient.Phone)
                AddressTextEdit.Text = Convert.ToString(clsPatient.Address)
                CboAddress.CityName = Convert.ToString(clsPatient.Address)
                HealthTextEdit.Text = Convert.ToString(clsPatient.Health)
                CboHlthStat.HealthStat = Convert.ToString(clsPatient.Health)
                TreatCheckEdit.Checked = System.Convert.ToBoolean(clsPatient.Treat)
                DiagCheckEdit.Checked = System.Convert.ToBoolean(clsPatient.Diag)
                OrthoCheckEdit.Checked = System.Convert.ToBoolean(clsPatient.Ortho)
                NotesTextEdit.Text = Convert.ToString(clsPatient.Notes)
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        End If
    End Sub

    Private Sub Add()
        Me.AddMode = True
        Me.EditMode = False
        Me.DeleteMode = False
        ClearRecord()
        ShowToolStripItems("Add")
        PatientIDSpinEdit.Text = DentistXDATA.getAutoID("New", "Patient")
        PatientIDSpinEdit.Enabled = False
        PatientNameTextEdit.Select()

    End Sub

    Private Sub Edit()
        Me.AddMode = False
        Me.EditMode = True
        Me.DeleteMode = False
        ShowToolStripItems("Edit")
    End Sub

    Private Sub Delete()
        Me.AddMode = False
        Me.EditMode = False
        Me.DeleteMode = True
        ShowToolStripItems("Delete")
    End Sub

    Private Sub ClearRecord()
        PatientIDSpinEdit.Value = 0
        PatientNameTextEdit.Text = Nothing
        SexTextEdit.Text = Nothing
        CboSex.SelectedIndex = -1
        AgeSpinEdit.Value = 0
        txtWhats.Text = Nothing
        PhoneTextEdit.Text = Nothing
        AddressTextEdit.Text = Nothing
        CboAddress.ResetText()
        HealthTextEdit.Text = Nothing
        CboHlthStat.ResetText()
        TreatCheckEdit.Checked = False
        DiagCheckEdit.Checked = False
        OrthoCheckEdit.Checked = False
        NotesTextEdit.Text = Nothing
        If cboPrefix IsNot Nothing AndAlso cboPrefix.Properties.Items.Count > 0 Then
            cboPrefix.SelectedIndex = 0
        End If
        If lblWhats IsNot Nothing Then
            lblWhats.Text = ""
        End If
    End Sub

    Private Sub GoBack_To_Grid()
        RemoveHandler dgView.SelectionChanged, AddressOf DGV_SelectionChanged
        Dim gridOK As Boolean = False
        Try
            LoadDGV()
            ShowToolStripItems("Cancel")
            dgView.SelectRow(Row)
            dgView.FocusedRowHandle = Row
            dgView.Focus()
            gridOK = True
        Catch ex As Exception
            MsgBox(ex.Message)
            'Hide error message.
        Finally
            If gridOK = False Then
                ''''
                ShowToolStripItems("Cancel")
                ''''
            End If
        End Try
        AddHandler dgView.SelectionChanged, AddressOf DGV_SelectionChanged
    End Sub
    Private Sub SetData(ByVal clsPatient As Patient)
        ' If WhatsApp is empty and phone starts with 05, default WhatsApp to phone
        If txtWhats IsNot Nothing AndAlso String.IsNullOrWhiteSpace(txtWhats.Text) AndAlso
           PhoneTextEdit IsNot Nothing AndAlso Not String.IsNullOrWhiteSpace(PhoneTextEdit.Text) AndAlso
           PhoneTextEdit.Text.Trim().StartsWith("05") Then
            txtWhats.Text = PhoneTextEdit.Text.Trim()
        End If

        Dim localDigits As String = WhatsHelper.NormalizeLocalWhatsTenDigitsForStorage(If(txtWhats?.Text, "").ToString())
        Dim prefixStored As String = WhatsHelper.GetPrefixTextForStorage(cboPrefix)

        With clsPatient
            .PatientID = System.Convert.ToInt32(PatientIDSpinEdit.Value)
            .PatientName = System.Convert.ToString(PatientNameTextEdit.Text)
            .Sex = System.Convert.ToString(CboSex.Text)
            .Age = System.Convert.ToInt32(AgeSpinEdit.Value)
            .WhatsApp = localDigits
            .WhatsAppPrefix = prefixStored
            .Phone = System.Convert.ToString(PhoneTextEdit.Text)
            .Address = System.Convert.ToString(CboAddress.CityName) 'System.Convert.ToString(AddressTextEdit.Text)
            .Health = System.Convert.ToString(CboHlthStat.HealthStat) 'System.Convert.ToString(HealthTextEdit.Text)
            .Treat = System.Convert.ToBoolean(TreatCheckEdit.Checked)
            .Implant = False
            .Diag = System.Convert.ToBoolean(DiagCheckEdit.Checked)
            .Ortho = System.Convert.ToBoolean(OrthoCheckEdit.Checked)
            .Struc = False
            .Notes = System.Convert.ToString(NotesTextEdit.Text)
            .BirthY = Now.Year - System.Convert.ToInt32(AgeSpinEdit.Value)
            .CreatedBy = 1
            .CreateDate = Now
        End With
    End Sub

    Private Sub InsertRecord()
        Dim clsPatient As New Patient
        If VerifyData() = True Then
            SetData(clsPatient)
            Dim result As Integer = clsPatientData.InsertPatient(clsPatient)
            Select Case result
                Case 18
                    'If MainView3.baseForm IsNot Nothing Then
                    '    MainView3.baseForm.ThinHDRChrtNew1.PatientBS.Add(clsPatient)
                    '    MsgBox("Patient inserted successfully.")
                    '    GoBack_To_Grid()
                    'Else
                    '    GoBack_To_Grid()
                    'End If
                Case -2
                    MsgBox("Patient already exists.")
                Case -3
                    MsgBox("Failed to generate patient number. Please try again.")
                Case -999
                    MsgBox("An error occurred during insertion.")
                Case Else
                    MsgBox("Unexpected return value: " & result)
            End Select
        End If
    End Sub


    Private Sub UpdateRecord()
        Dim oclsPatient As New Patient
        Dim clsPatient As New Patient
        Dim pid As Integer
        If Not TryGetPatientIdFromRowHandle(Row, pid) Then
            MsgBox("Could not read the selected patient ID from the grid.", MsgBoxStyle.OkOnly, "Patient")
            Return
        End If
        oclsPatient.PatientID = pid
        oclsPatient = clsPatientData.Select_Record(oclsPatient)

        If VerifyData() = True Then
            SetData(clsPatient)
            Dim bSuccess As Boolean = clsPatientData.Update(oclsPatient, clsPatient)

            If bSuccess = True Then
                'If MainView3.baseForm IsNot Nothing Then
                '    Dim bs = MainView3.baseForm.ThinHDRChrtNew1.PatientBS
                '    ' Find index of matching PatientID
                '    Dim index As Integer = -1
                '    For i As Integer = 0 To bs.Count - 1
                '        Dim p As Patient = TryCast(bs(i), Patient)
                '        If p IsNot Nothing AndAlso p.PatientID = oclsPatient.PatientID Then
                '            index = i
                '            Exit For
                '        End If
                '    Next

                '    If index >= 0 Then
                '        bs(index) = clsPatient
                '        bs.ResetItem(index)
                '    End If
                'End If

                GoBack_To_Grid()
            Else
                MsgBox("Update failed.", MsgBoxStyle.OkOnly, "Error")
            End If
        End If
    End Sub




    Private Sub DeleteRecord()
        Dim clsPatient As New Patient
        Dim pid As Integer
        If Not TryGetPatientIdFromRowHandle(Row, pid) Then
            MsgBox("Could not read the selected patient ID from the grid.", MsgBoxStyle.OkOnly, "Patient")
            Return
        End If
        clsPatient.PatientID = pid
        clsPatient = clsPatientData.Select_RecordByID(clsPatient.PatientID)
        '========================
        ' First confirmation (standard MessageBox)
        If MessageBox.Show(
        "Are you sure you want to delete this " & clsPatient.PatientName & " Patient?",
        "First Warning",
        MessageBoxButtons.YesNo,
        MessageBoxIcon.Warning
    ) <> DialogResult.Yes Then
            Return
        End If

        ' Second confirmation (custom dialog)
        Using confirmDialog As New DoubleConfirmDialog() With {.Text = "Patient Deletion From"}
            confirmDialog.Message = "FINAL WARNING: This will permanently delete" & vbCrLf & "{ " & clsPatient.PatientName & " } - Patient." & vbCrLf & "  Check the box to confirm."
            If confirmDialog.ShowDialog(Me) <> DialogResult.OK OrElse Not confirmDialog.IsConfirmed Then
                Return ' Exit if user cancels or doesn't check the box
            End If
        End Using
        '=================================


        'Dim result As DialogResult = MessageBox.Show("Are you sure? Delete this record?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        'If result = DialogResult.Yes Then
        Dim Index As Integer = -1
            Dim BS As BindingSource = Nothing
            Dim bSuccess As Boolean

        'If MainView3.baseForm IsNot Nothing Then
        '    BS = MainView3.baseForm.ThinHDRChrtNew1.PatientBS

        '    ' Find index by PatientID
        '    For i As Integer = 0 To BS.Count - 1
        '        Dim p As Patient = TryCast(BS(i), Patient)
        '        If p IsNot Nothing AndAlso p.PatientID = clsPatient.PatientID Then
        '            Index = i
        '            Exit For
        '        End If
        '    Next
        'End If

        bSuccess = clsPatientData.Delete(clsPatient)

            If bSuccess = True Then
                If BS IsNot Nothing AndAlso Index >= 0 Then
                    BS.RemoveAt(Index)
                    ' Optional: BS.ResetItem(Index) is not needed after RemoveAt
                    GoBack_To_Grid()
                Else
                    GoBack_To_Grid()
                End If
            Else
                MsgBox("Delete failed.", MsgBoxStyle.OkOnly, "Error")
            End If
        'End If
    End Sub




    Private Function VerifyData() As Boolean
        ' Check required fields
        If PatientIDSpinEdit.Value = Nothing OrElse PatientIDSpinEdit.Value.ToString() = "" Then
            MsgBox("PatientID is required.", MsgBoxStyle.OkOnly, "Entry Error")
            PatientIDSpinEdit.Select()
            Return False
        End If
        If PatientNameTextEdit.Text = "" Then
            MsgBox("PatientName is required.", MsgBoxStyle.OkOnly, "Entry Error")
            PatientNameTextEdit.Select()
            Return False
        End If
        Return True
    End Function

    Private Sub ShowToolStripItems(ByVal Item As String)
        bAdd.Enabled = False
        bEdit.Enabled = False
        bDelete.Enabled = False
        bCancel.Enabled = False
        bRefresh.Enabled = False
        butApply.Enabled = True
        butCancel.Enabled = True
        Select Case Item
            Case "Add"
                EnableRecord(True)
            Case "Edit"
                EnableRecord(True)
            Case "Delete"
                EnableRecord(False)
            Case "Cancel"
                If dgView.RowCount = 0 Then
                    ShowToolStripItems("No Record")
                Else
                    EnableRecord(False)
                    bAdd.Enabled = True
                    bEdit.Enabled = True
                    bDelete.Enabled = True
                    bRefresh.Enabled = True
                    butApply.Enabled = False
                    butCancel.Enabled = False
                    DGV.Focus()
                End If
            Case "Refresh"
                bAdd.Enabled = True
                bEdit.Enabled = True
                bDelete.Enabled = True
                bRefresh.Enabled = True
                butApply.Enabled = False
                butCancel.Enabled = False
                LoadDGV()
            Case "No Record"
                bAdd.Enabled = True
                bRefresh.Enabled = False
                butApply.Enabled = False
                butCancel.Enabled = False
        End Select
    End Sub
    Private Sub bAdd_ItemClick(sender As Object, e As ItemClickEventArgs) Handles bAdd.ItemClick
        Add()
    End Sub
    Private Sub bEdit_ItemClick(sender As Object, e As ItemClickEventArgs) Handles bEdit.ItemClick
        Display()
        Edit()
    End Sub
    Private Sub bDelete_ItemClick(sender As Object, e As ItemClickEventArgs) Handles bDelete.ItemClick
        Display()
        Delete()
    End Sub
    Private Sub bRefresh_ItemClick(sender As Object, e As ItemClickEventArgs) Handles bRefresh.ItemClick
        LoadDGV()
    End Sub
    Private Sub bCancel_ItemClick(sender As Object, e As ItemClickEventArgs) Handles bCancel.ItemClick
        ShowToolStripItems("Cancel")
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
    ''' Validates full WhatsApp number: digits only, prefix (no signs) + local number without leading 0.
    ''' For 970/972 expects 12 digits total (3 + 9). For other prefixes expects prefixLength + 9 digits.
    ''' If prefixDigits is empty (international input), requires 10–15 digits.
    ''' Returns empty string if valid; otherwise returns a message (in current language) to fix the number.
    ''' </summary>
    Private Function ValidateWhatsAppNumber(fullNumberDigits As String, prefixDigits As String) As String
        If String.IsNullOrWhiteSpace(fullNumberDigits) Then
            Return If(Eng, "Enter WhatsApp/phone number (digits only).", "أدخل رقم واتساب/الجوال (أرقام فقط).")
        End If
        If fullNumberDigits.Any(Function(c) Not Char.IsDigit(c)) Then
            Return If(Eng, "Number must contain only digits (no spaces, dashes or plus sign).", "يجب أن يحتوي الرقم على أرقام فقط (بدون مسافات أو شرطات أو +).")
        End If

        If String.IsNullOrWhiteSpace(prefixDigits) Then
            ' International-style number without combo prefix: require reasonable length
            If fullNumberDigits.Length < 10 OrElse fullNumberDigits.Length > 15 Then
                Return If(Eng, "Number must be 10–15 digits (e.g. 970599123456 for Palestine).", "يجب أن يكون الرقم 10–15 رقمًا (مثلاً 970599123456 لفلسطين).")
            End If
            Return ""
        End If

        Dim prefixLen As Integer = prefixDigits.Length
        ' Local mobile without leading 0 is typically 9 digits (e.g. Palestine/Israel). Total = prefix + 9.
        Dim expectedLen As Integer = prefixLen + 9
        If fullNumberDigits.Length <> expectedLen Then
            Dim msgEn As String = $"Invalid length. For prefix +{prefixDigits} use {prefixLen} (prefix) + 9 digits (number without leading 0) = {expectedLen} digits total. Current: {fullNumberDigits.Length}."
            Dim msgAr As String = $"طول غير صحيح. لرمز +{prefixDigits} استخدم {prefixLen} (الرمز) + 9 أرقام (الرقم بدون صفر في البداية) = {expectedLen} رقمًا. الحالي: {fullNumberDigits.Length}."
            Return If(Eng, msgEn, msgAr)
        End If
        Return ""
    End Function

#End Region



End Class
