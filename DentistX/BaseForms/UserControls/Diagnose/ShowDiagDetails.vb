Imports DevExpress.CodeParser
Imports DevExpress.XtraGrid.Views.Base

Public Class ShowDiagDetails
    Implements IPatientAwareUserControl
    Private clsDiags As IEnumerable(Of Patient_Diagnosis)
    Private clsDiagData As New Patient_DiagnosisDATA
    Private clsDiagDetails As IEnumerable(Of Patient_DiagDet)
    Private clsDiagDet As Patient_DiagDet
    Private clsDiagDetData As New Patient_DiagDetDATA
    Private clsDiagnos As Patient_Diagnosis
    Private WithEvents DiagBS As New BindingSource
    Private _currentPatient As Patient
    Friend Property CurrentPatient As Patient
        Get
            Return _currentPatient
        End Get
        Set(value As Patient)
            _currentPatient = value
        End Set
    End Property
    Private ReadOnly Property PatientID As Integer
        Get
            Return If(CurrentPatient Is Nothing, 0, CurrentPatient.PatientID)
        End Get
    End Property

    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        ApplyCtlGradientBackground(Me,
                             Color.AliceBlue,
                              Color.BlueViolet,
                              Drawing2D.LinearGradientMode.ForwardDiagonal, 128)
        LoadData(PatientID)
    End Sub
    Public Sub New(patientid As Integer)
        ' This call is required by the designer.
        InitializeComponent()
        ApplyCtlGradientBackground(Me,
                             Color.AliceBlue,
                              Color.BlueViolet,
                              Drawing2D.LinearGradientMode.ForwardDiagonal, 128)
        ' Add any initialization after the InitializeComponent() call.
        LoadData(patientid)
    End Sub
    Private Sub DiagDetails_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
    Private Sub GridViewTrts_CustomUnboundColumnData(sender As Object, e As CustomColumnDataEventArgs) Handles GridViewTrts.CustomUnboundColumnData
        If e.Column.Name = "colRowNum" Then
            e.Value = e.ListSourceRowIndex + 1
        End If
    End Sub
    Public Sub LoadPatientData(patientId As Integer) Implements IPatientAwareUserControl.LoadPatientData
        SyncCurrentPatientFromForm(patientId)
        If CurrentPatient IsNot Nothing AndAlso CurrentPatient.PatientID = patientId Then
            LoadShowDiagData(patientId)
        ElseIf patientId > 0 Then
            LoadShowDiagData(patientId)
        End If
    End Sub
    Private Sub SyncCurrentPatientFromForm(patientId As Integer)
        Dim ws = PatientAwareHelper.TryGetPatientWorkspace(Me)
        If ws IsNot Nothing AndAlso ws.Current_Patient IsNot Nothing AndAlso ws.Current_Patient.PatientID = patientId Then
            CurrentPatient = ws.Current_Patient
        ElseIf FormManager.Instance.IsBasePatientFormOpen AndAlso FormManager.Instance.GetCurrentPatient() IsNot Nothing AndAlso FormManager.Instance.GetCurrentPatient().PatientID = patientId Then
            CurrentPatient = FormManager.Instance.GetCurrentPatient()
        End If
    End Sub

    'load data from Patient_Diagnosis
    Public Sub LoadShowDiagData(patientid As Integer)
        LoadData(patientid)
    End Sub

    Property PassedPatientID As Integer
    Property PassedPatientName As String
    'load data from Patient_Diagnosis
    Private Sub LoadData()
        clsDiagDetails = clsDiagDetData.SelectAll
        'clsDiags = clsDiagData.SelectAll
        DiagDetBS.DataSource = clsDiagDetails
        DiagBS.DataSource = clsDiags
        Patient_DiagsGrid.DataSource = clsDiagDet
    End Sub
    Private Sub LoadData(patientid As Integer)
        clsDiagDetails = clsDiagDetData.SelectAllByPatient(patientid)
        DiagDetBS.DataSource = clsDiagDetails

        'Get current Patient_DiagDet
        clsDiagDet = TryCast(DiagDetBS.Current, Patient_DiagDet)

        'FIX: Ensure clsDiagDet is not null before accessing its properties
        If clsDiagDet IsNot Nothing AndAlso Not String.IsNullOrEmpty(clsDiagDet.DiagIDs) Then
            'FIX: Changed from clsDiagDetData to clsDiagData
            clsDiags = clsDiagData.SelectAllByIDs(clsDiagDet.DiagIDs)
            DiagBS.DataSource = clsDiags
            Patient_DiagsGrid.DataSource = DiagBS
        Else
            'Clear or handle empty case
            DiagBS.DataSource = New List(Of Patient_Diagnosis)()
            Patient_DiagsGrid.DataSource = DiagBS
        End If
        BindControls()
    End Sub

    Private Sub DiagDetBS_CurrentChanged(sender As Object, e As EventArgs) Handles DiagDetBS.CurrentChanged
        If DiagBS.Current Is Nothing Then
            Return
        End If
        clsDiagDet = TryCast(DiagDetBS.Current, Patient_DiagDet)
        If clsDiagDet Is Nothing OrElse String.IsNullOrEmpty(clsDiagDet.DiagIDs) Then
            'Clear or handle empty case
            DiagBS.DataSource = New List(Of Patient_Diagnosis)()
            Return
        Else
            clsDiags = clsDiagData.SelectAllByIDs(clsDiagDet.DiagIDs)
            DiagBS.DataSource = clsDiags
        End If

    End Sub

    'bind controls to DiagDetBS
    Private Sub BindControls()
        Try
            txtDiagAgree.DataBindings.Clear()
            txtDiagAgree.DataBindings.Add("Text", DiagDetBS, "DiagAgreament", True, DataSourceUpdateMode.OnPropertyChanged)
            txtDiagDetails.DataBindings.Clear()
            txtDiagDetails.DataBindings.Add("Text", DiagDetBS, "DiagDetails", True, DataSourceUpdateMode.OnPropertyChanged)
            dtDiagDate.DataBindings.Clear()
            dtDiagDate.DataBindings.Add("EditValue", DiagDetBS, "DiagDate", True, DataSourceUpdateMode.OnPropertyChanged)
            dtTrtToStart.DataBindings.Clear()
            dtTrtToStart.DataBindings.Add("EditValue", DiagDetBS, "DateToStart", True, DataSourceUpdateMode.OnPropertyChanged)
            txtPayValue.DataBindings.Clear()
            txtPayValue.DataBindings.Add("EditValue", DiagDetBS, "AdvancePay", True, DataSourceUpdateMode.OnPropertyChanged)
            'formatting txtPayValue as currency
            txtPayValue.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
            txtPayValue.Properties.DisplayFormat.FormatString = "N0"
            txtTrtPlan.DataBindings.Clear()
            txtTrtPlan.DataBindings.Add("Text", DiagDetBS, "TreatPlan", True, DataSourceUpdateMode.OnPropertyChanged)
            txtTrtValue.DataBindings.Clear()
            txtTrtValue.DataBindings.Add("EditValue", DiagDetBS, "TotalValue", True, DataSourceUpdateMode.OnPropertyChanged)
            txtTrtValue.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
            txtTrtValue.Properties.DisplayFormat.FormatString = "N0"
            txtDiagNotes.DataBindings.Clear()
            txtDiagNotes.DataBindings.Add("Text", DiagDetBS, "DiagNotes", True, DataSourceUpdateMode.OnPropertyChanged)
        Catch ex As Exception

        End Try
    End Sub

    Public Event ReturnToJaw As EventHandler

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        RaiseEvent ReturnToJaw(Me, EventArgs.Empty)
    End Sub

    Private Sub btnSaveTrt_Click(sender As Object, e As EventArgs) Handles btnSaveTrt.Click
        Try


            'Update the current clsDiagDet from bound controls
            'the update method takes old object and new object
            Dim oldDiagDet As Patient_DiagDet = TryCast(DiagDetBS.Current, Patient_DiagDet)
            'Create a new object to hold updated values
            Dim clsDiagDet As Patient_DiagDet
            'iterate through all items in DiagBS to get selected DiagIDs
            Dim selectedDiagIDs As New List(Of Integer)
            'For Each diag As Patient_DiagDet In DiagDetBS.List
            oldDiagDet = TryCast(DiagDetBS.Current, Patient_DiagDet)
            clsDiagDet = New Patient_DiagDet With {
           .DiagDetID = oldDiagDet.DiagDetID,
           .PatientID = oldDiagDet.PatientID,
           .AdvancePay = CInt(txtPayValue.EditValue),
           .DateToStart = CDate(dtTrtToStart.EditValue),
           .DiagAgreament = txtDiagAgree.Text,
           .DiagDate = CDate(dtDiagDate.EditValue),
           .DiagDetails = txtDiagDetails.Text,
           .DiagIDs = oldDiagDet.DiagIDs, 'keep existing DiagIDs
           .DiagNotes = txtDiagNotes.Text,
           .TotalValue = CInt(txtTrtValue.EditValue),
           .TreatPlan = txtTrtPlan.Text,
           .UserID = CurrentUser.UsID
       }

            If clsDiagDetData.Update(oldDiagDet, clsDiagDet) Then
                MsgBox(If(Eng, "Diagnosis details updated successfully.", "تم تحديث تفاصيل التشخيص بنجاح."), MsgBoxStyle.Information)
            Else
                MsgBox(If(Eng, "Error updating diagnosis details.", "حدث خطأ أثناء تحديث تفاصيل التشخيص."), MsgBoxStyle.Critical)
            End If

            'Next

        Catch ex As Exception
            MsgBox(If(Eng, "Error saving diagnosis details: " & ex.Message, "حدث خطأ أثناء حفظ تفاصيل التشخيص: " & ex.Message), MsgBoxStyle.Critical)
        End Try




    End Sub

#Region "Numeric TextBox Handlers"

    Private Sub txtPrice_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtTrtValue.KeyPress, txtPayValue.KeyPress
        ' Allow control keys (Backspace, Delete, etc.)
        If Char.IsControl(e.KeyChar) Then
            Return
        End If
        ' Allow digits (0-9)
        If Char.IsDigit(e.KeyChar) Then
            Return
        End If
        ' Allow only ONE decimal point (for prices)
        If e.KeyChar = "."c AndAlso Not txtTrtValue.Text.Contains(".") Then
            Return
        End If
        ' Block any other character
        e.Handled = True
    End Sub
    Private Sub txtPrice_PreviewKeyDown(sender As Object, e As PreviewKeyDownEventArgs) Handles txtTrtValue.PreviewKeyDown, txtPayValue.PreviewKeyDown
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
        If e.KeyCode = Keys.Decimal AndAlso Not txtTrtValue.Text.Contains(".") Then
            Return
        End If
        ' Block all other keys
        e.IsInputKey = False
    End Sub
    Private Sub txtPrice_EditValueChanged(sender As Object, e As EventArgs) Handles txtTrtValue.EditValueChanged, txtPayValue.EditValueChanged
        ' Skip if empty
        If String.IsNullOrEmpty(txtTrtValue.Text) Then Return
        ' Store cursor position
        Dim cursorPos = txtTrtValue.SelectionStart
        ' Remove all non-numeric characters (except one decimal point)
        Dim cleanedText As New System.Text.StringBuilder()
        Dim hasDecimal As Boolean = False
        For Each c As Char In txtTrtValue.Text
            If Char.IsDigit(c) Then
                cleanedText.Append(c)
            ElseIf c = "."c AndAlso Not hasDecimal Then
                cleanedText.Append(c)
                hasDecimal = True
            End If
        Next
        ' If text was modified (due to invalid pasted chars), update it
        If cleanedText.ToString() <> txtTrtValue.Text Then
            txtTrtValue.Text = cleanedText.ToString()
            ' Restore cursor position (adjusting for removed characters)
            txtTrtValue.SelectionStart = Math.Min(cursorPos, txtTrtValue.Text.Length)
        End If
    End Sub
    Private Sub txtTrtPrice_GotFocus(sender As Object, e As EventArgs) Handles txtTrtValue.GotFocus
        txtTrtValue.SelectAll()
    End Sub
    Private Sub txtPayValue_GotFocus(sender As Object, e As EventArgs) Handles txtPayValue.GotFocus
        txtPayValue.SelectAll()
    End Sub

    Private Sub txtPayValue_Click(sender As Object, e As EventArgs) Handles txtPayValue.Click
        txtPayValue.SelectAll()
    End Sub

    Private Sub txtTrtPrice_Click(sender As Object, e As EventArgs) Handles txtTrtValue.Click
        txtTrtValue.SelectAll()
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        Try
            'delete current clsDiagDet with confirmation
            Dim clsDiagDet As Patient_DiagDet = TryCast(DiagDetBS.Current, Patient_DiagDet)
            If MsgBox(If(Eng, "Are you sure you want to delete this diagnosis detail?", "هل أنت متأكد أنك تريد حذف تفاصيل التشخيص هذه؟"), MsgBoxStyle.YesNo + MsgBoxStyle.Question) = MsgBoxResult.Yes Then
                If clsDiagDetData.Delete(clsDiagDet) Then
                    MsgBox(If(Eng, "Diagnosis detail deleted successfully.", "تم حذف تفاصيل التشخيص بنجاح."), MsgBoxStyle.Information)
                    'Reload data
                    LoadData(PassedPatientID)
                Else
                    MsgBox(If(Eng, "Error deleting diagnosis detail.", "حدث خطأ أثناء حذف تفاصيل التشخيص."), MsgBoxStyle.Critical)
                End If
            End If
        Catch ex As Exception
            MsgBox(If(Eng, "Error deleting diagnosis detail: " & ex.Message, "حدث خطأ أثناء حذف تفاصيل التشخيص: " & ex.Message), MsgBoxStyle.Critical)
        End Try
    End Sub

#End Region

End Class
