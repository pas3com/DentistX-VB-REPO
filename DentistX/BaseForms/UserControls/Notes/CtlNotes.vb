'Imports System.ComponentModel

Imports System
Imports System.Windows.Forms
Imports DevExpress.XtraEditors
Imports DevExpress.XtraGrid.Views.Base

Public Class CtlNotes
    Implements IPatientAwareUserControl


    Private Sub IPatientAwareUserControl_LoadPatientData(patientId As Integer) Implements IPatientAwareUserControl.LoadPatientData
        SyncCurrentPatientFromForm(patientId)
        Me.Hide()
        LoadPatientData(patientId)
        Me.Show()
    End Sub
    Private Sub SyncCurrentPatientFromForm(patientId As Integer)
        Dim ws = PatientAwareHelper.TryGetPatientWorkspace(Me)
        If ws IsNot Nothing AndAlso ws.Current_Patient IsNot Nothing AndAlso ws.Current_Patient.PatientID = patientId Then
            CurrentPatient = ws.Current_Patient
        ElseIf FormManager.Instance.IsBasePatientFormOpen AndAlso FormManager.Instance.GetCurrentPatient() IsNot Nothing AndAlso FormManager.Instance.GetCurrentPatient().PatientID = patientId Then
            CurrentPatient = FormManager.Instance.GetCurrentPatient()
        End If
    End Sub

    Public Sub LoadPatientData(patientId As Integer)
        LoadData(patientId)
    End Sub


    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        ' Enable double buffering to reduce flickering
        Me.DoubleBuffered = True
        SetStyle(ControlStyles.AllPaintingInWmPaint Or
                 ControlStyles.UserPaint Or
                 ControlStyles.DoubleBuffer, True)
        UpdateStyles()
    End Sub

    Private _inputLangMessageFilter As InputLangMessageFilter
    Dim memo As New DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit()

    Protected Overrides Sub OnHandleCreated(e As EventArgs)
        'MyBase.OnHandleCreated(e)
        'If _inputLangMessageFilter Is Nothing Then
        '    _inputLangMessageFilter = New InputLangMessageFilter(Sub() OnInputLangChangeFromMessageFilter())
        '    Application.AddMessageFilter(_inputLangMessageFilter)
        'End If
        '' Never touch child RightToLeft / grid appearance synchronously here — CreateHandle can re-enter
        '' (e.g. WM_INPUTLANGCHANGE with Arabic) and WinForms throws Win32Exception creating handles.
        'BeginInvoke(New MethodInvoker(AddressOf ApplyTxtNotesRightToLeftFromInputLanguage))
    End Sub

    Protected Overrides Sub OnHandleDestroyed(e As EventArgs)
        'If _inputLangMessageFilter IsNot Nothing Then
        '    Application.RemoveMessageFilter(_inputLangMessageFilter)
        '    _inputLangMessageFilter = Nothing
        'End If
        'MyBase.OnHandleDestroyed(e)
    End Sub

    Private Sub OnInputLangChangeFromMessageFilter()
        'If IsDisposed OrElse Not IsHandleCreated Then Return
        '' Always marshal: filter runs inside the message pump and may overlap CreateControl/Show.
        'BeginInvoke(New MethodInvoker(AddressOf ApplyTxtNotesRightToLeftFromInputLanguage))
    End Sub

    ''' <summary>English Windows input language → LTR; any other → RTL (matches typical Arabic/Hebrew typing).</summary>
    Private Sub ApplyTxtNotesRightToLeftFromInputLanguage()
        If IsDisposed OrElse Not IsHandleCreated OrElse RecreatingHandle Then Return
        Dim isRtl As Boolean
        Try
            Dim lang = InputLanguage.CurrentInputLanguage.Culture.TwoLetterISOLanguageName
            Dim isEnglish = String.Equals(lang, "en", StringComparison.OrdinalIgnoreCase)
            isRtl = Not isEnglish
        Catch
            isRtl = Not Eng
        End Try
        Me.RightToLeft = If(isRtl, RightToLeft.Yes, RightToLeft.No)
        ' Do not set Me.RightToLeft: mirroring the whole user control during Show/CreateControl breaks handle creation.
        If txtNotes IsNot Nothing AndAlso Not txtNotes.IsDisposed Then
            txtNotes.RightToLeft = If(isRtl, RightToLeft.Yes, RightToLeft.No)
        End If

        ' Repository appearance affects the in-place editor; non-edit cells use the column's AppearanceCell.
        If memo IsNot Nothing Then
            memo.Appearance.Options.UseTextOptions = True
            memo.Appearance.TextOptions.RightToLeft = isRtl
        End If
        If ColNote IsNot Nothing Then
            ColNote.AppearanceCell.Options.UseTextOptions = True
            ColNote.AppearanceCell.TextOptions.RightToLeft = isRtl
        End If
        If GridView1 IsNot Nothing AndAlso GridView1.GridControl IsNot Nothing AndAlso Not GridView1.GridControl.IsDisposed Then
            GridView1.Invalidate()
        End If

        ApplyNotesGridActiveMemoEditorRightToLeft(isRtl)
    End Sub

    Private Sub ApplyNotesGridActiveMemoEditorRightToLeft(isRtl As Boolean)
        If GridView1 Is Nothing Then Return
        Dim editor = TryCast(GridView1.ActiveEditor, MemoEdit)
        If editor Is Nothing OrElse editor.IsDisposed Then Return
        editor.RightToLeft = If(isRtl, RightToLeft.Yes, RightToLeft.No)
    End Sub

    Private Sub GridView1_ShownEditor(sender As Object, e As EventArgs) Handles GridView1.ShownEditor
        'ApplyTxtNotesRightToLeftFromInputLanguage()
    End Sub

    Private Sub txtNotes_Enter(sender As Object, e As EventArgs) Handles txtNotes.Enter
        'ApplyTxtNotesRightToLeftFromInputLanguage()
    End Sub

    Public Sub New(ByVal patientID As Integer)

        ' This call is required by the designer.
        InitializeComponent()
        ' Enable double buffering to reduce flickering
        Me.DoubleBuffered = True
        SetStyle(ControlStyles.AllPaintingInWmPaint Or
                 ControlStyles.UserPaint Or
                 ControlStyles.DoubleBuffer, True)
        UpdateStyles()
        '' Add any initialization after the InitializeComponent() call.
        'If LicenseManager.UsageMode = LicenseUsageMode.Runtime Then
        '    'LoadData(patientID)
        'End If
    End Sub

    Friend WithEvents Patient_NotesBindingSource As BindingSource
    Private isEditingNotes As Boolean = False
    Private clsPatientData As New PatientDATA
    Private clsPatient As Patient
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
    Private clsPatientNotes As IEnumerable(Of Patient_Notes)
    Private clsPatientNoteData As New Patient_NotesDATA

    Private Sub CtNotes_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            '' Prevent runtime logic from running at design time
            'If LicenseManager.UsageMode = LicenseUsageMode.Runtime Then
            '    LoadData(PatientID)
            'End If
            NotesDate.DateTime = Now
            'Create multiline editor

            NotesGrid.RepositoryItems.Add(memo)

            GridView1.Columns("Note").ColumnEdit = memo

            'Allow row height to expand
            GridView1.OptionsView.RowAutoHeight = True
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub GridViewTrts_CustomUnboundColumnData(sender As Object, e As CustomColumnDataEventArgs) Handles GridView1.CustomUnboundColumnData
        If e.Column.Name = "colRowNum" Then
            e.Value = e.ListSourceRowIndex + 1
        End If
    End Sub


    Public WriteOnly Property ValueFromParent() As Integer
        Set(ByVal Value As Integer)
            LoadData(Value)

        End Set
    End Property

    Public Sub LoadData(ByVal patientID As Integer)
        If CurrentPatient Is Nothing Then Exit Sub
        Try
            Me.SuspendLayout()

            If Patient_NotesBindingSource Is Nothing Then
                Patient_NotesBindingSource = New BindingSource
            End If
            clsPatient = clsPatientData.Select_RecordByID(patientID) 'New Patient With {.PatientID = patientID} '
            clsPatientNotes = clsPatientData.GetPatient_Notes(clsPatient)
            Patient_NotesBindingSource.DataSource = clsPatientNotes.ToList
            NotesGroup.Text = "جدول ملاحظات المريض " & clsPatient.PatientName
            NotesGrid.DataSource = Patient_NotesBindingSource
        Catch ex As System.Data.SqlClient.SqlException
            MsgBox(ex.Message)
        Finally
            Me.ResumeLayout()
        End Try
    End Sub

#Region "Notes"

    Private Sub btAddNotes_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btAddNotes.Click
        Try

            Dim NoteDate As Date

            NoteDate = Me.NotesDate.DateTime

            If CurrentPatient Is Nothing Then
                If Eng Then
                    MsgBox("Must Choose A Patient Name")
                    Exit Sub
                Else
                    MessageBox.Show("يجب إختيار مريض")
                    Exit Sub
                End If
            End If

            Dim notesData As New Patient_NotesDATA()
            If clsPatientNoteData.Add(New Patient_Notes With {.PatientID = PatientID, .NoteDate = NoteDate, .Note = txtNotes.Text}) Then
                LoadData(PatientID)
                txtNotes.ResetText()
            End If
        Catch ex As System.Data.SqlClient.SqlException
            MsgBox(ex.Message)
        End Try
    End Sub

    'Private Sub btEditNotes_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btEditNotes.Click
    '    isEditingNotes = Not isEditingNotes

    '    If isEditingNotes Then
    '        btEditNotes.Text = If(Eng, "End Edit", "إنهاء التعديل")
    '        NotesNavigator.Visible = True
    '        Patient_NotesDataGridView.EditMode = DataGridViewEditMode.EditOnKeystrokeOrF2
    '    Else
    '        btEditNotes.Text = If(Eng, "Edit", "تعديل")
    '        NotesNavigator.Visible = False
    '        Patient_NotesDataGridView.EditMode = DataGridViewEditMode.EditProgrammatically
    '    End If
    'End Sub

    'Private isEditingNotes As Boolean = False

    Private Sub btEditNotes_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btEditNotes.Click
        isEditingNotes = Not isEditingNotes

        If isEditingNotes Then
            btEditNotes.Text = If(Eng, "End Edit", "إنهاء التعديل")
            NotesNavigator.Visible = True
            'Patient_NotesDataGridView.EditMode = DataGridViewEditMode.EditOnKeystrokeOrF2
        Else
            btEditNotes.Text = If(Eng, "Edit", "تعديل")
            NotesNavigator.Visible = False
            'Patient_NotesDataGridView.EditMode = DataGridViewEditMode.EditProgrammatically


        End If
    End Sub


    Private Sub NotesSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NotesSave.Click
        ' Save changes
        Try
            Me.Validate()
            Me.Patient_NotesBindingSource.EndEdit()

            ' Save to DB here
            Dim updatedList = CType(Me.Patient_NotesBindingSource.DataSource, List(Of Patient_Notes))
            Dim notesData As New Patient_NotesDATA()
            For Each note In updatedList
                notesData.Update_Record(note) ' Or SaveRecord, depending on your method name
            Next

            MsgBox(If(Eng, "Changes saved successfully.", "تم حفظ التعديلات بنجاح."), MsgBoxStyle.Information)
            LoadData(PatientID)
        Catch ex As Exception
            MsgBox("Error saving notes: " & ex.Message, MsgBoxStyle.Critical)
        End Try
    End Sub

    Private Sub btnDel_Click(sender As Object, e As EventArgs) Handles btnDel.Click
        Try
            Me.Validate()
            Me.Patient_NotesBindingSource.EndEdit()

            ' Delete from DB here
            Dim selectedNote As Patient_Notes = CType(Me.Patient_NotesBindingSource.Current, Patient_Notes)
            Dim notesData As New Patient_NotesDATA()
            If selectedNote IsNot Nothing Then
                notesData.Delete(selectedNote)
                Me.Patient_NotesBindingSource.RemoveCurrent()
            End If
            LoadData(PatientID)
            MsgBox(If(Eng, "Note Deleted successfully.", "تم حذف التعديل بنجاح."), MsgBoxStyle.Information)
        Catch ex As Exception
            MsgBox("Error saving notes: " & ex.Message, MsgBoxStyle.Critical)
        End Try
    End Sub


#End Region

    ''' <summary>DevExpress editors / XtraUserControl do not surface WinForms InputLanguageChanged; WM_INPUTLANGCHANGE still fires app-wide.</summary>
    Private NotInheritable Class InputLangMessageFilter
        Implements IMessageFilter

        Private Const WM_INPUTLANGCHANGE As Integer = &H51
        Private ReadOnly _onInputLangChange As Action

        Public Sub New(onInputLangChange As Action)
            _onInputLangChange = onInputLangChange
        End Sub

        Public Function PreFilterMessage(ByRef m As Message) As Boolean Implements IMessageFilter.PreFilterMessage
            If m.Msg = WM_INPUTLANGCHANGE Then
                _onInputLangChange()
            End If
            Return False
        End Function
    End Class

End Class