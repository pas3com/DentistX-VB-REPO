Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms
Imports DevExpress.XtraEditors
Imports DevExpress.XtraEditors.Controls

''' <summary>Compact picker for patient name, reason, and notes label colors on appointment cards.</summary>
Public Class ApptCardLabelColorsDialog
    Inherits XtraForm

    Private Shared ReadOnly DialogUiFont As New Font("Calibri", 10.0F, FontStyle.Bold)

    Private _patientNameColor As Color
    Private _reasonColor As Color
    Private _notesColor As Color

    Private ReadOnly _colorPatient As New ColorEdit()
    Private ReadOnly _colorReason As New ColorEdit()
    Private ReadOnly _colorNotes As New ColorEdit()

    Private ReadOnly _lblPatient As New LabelControl()
    Private ReadOnly _lblReason As New LabelControl()
    Private ReadOnly _lblNotes As New LabelControl()

    Private ReadOnly _btnOk As New SimpleButton()
    Private ReadOnly _btnCancel As New SimpleButton()

    Public Sub New(patientNameColor As Color, reasonColor As Color, notesColor As Color)
        _patientNameColor = patientNameColor
        _reasonColor = reasonColor
        _notesColor = notesColor

        Font = DialogUiFont
        Appearance.Options.UseFont = True

        Text = If(Eng, "Appointment label colors", "ألوان تسميات المواعيد")
        FormBorderStyle = FormBorderStyle.FixedDialog
        MinimizeBox = False
        MaximizeBox = False
        ShowInTaskbar = False
        StartPosition = FormStartPosition.CenterParent
        ClientSize = New Size(400, 176)

        Dim y As Integer = 12
        Const rowH As Integer = 32
        Const gap As Integer = 8
        Const labelW As Integer = 140
        Const editW As Integer = 200

        _lblPatient.Text = If(Eng, "Patient name", "اسم المريض")
        _lblReason.Text = If(Eng, "Reason", "السبب")
        _lblNotes.Text = If(Eng, "Notes", "الملاحظات")

        ApplyLabelFont(_lblPatient)
        ApplyLabelFont(_lblReason)
        ApplyLabelFont(_lblNotes)

        LayoutRowColorEdit(_lblPatient, _colorPatient, y, labelW, editW, rowH)
        y += rowH + gap
        LayoutRowColorEdit(_lblReason, _colorReason, y, labelW, editW, rowH)
        y += rowH + gap
        LayoutRowColorEdit(_lblNotes, _colorNotes, y, labelW, editW, rowH)

        ConfigureColorEdit(_colorPatient)
        ConfigureColorEdit(_colorReason)
        ConfigureColorEdit(_colorNotes)

        _colorPatient.EditValue = _patientNameColor
        _colorReason.EditValue = _reasonColor
        _colorNotes.EditValue = _notesColor

        AddHandler _colorPatient.EditValueChanged, AddressOf ColorPatient_EditValueChanged
        AddHandler _colorReason.EditValueChanged, AddressOf ColorReason_EditValueChanged
        AddHandler _colorNotes.EditValueChanged, AddressOf ColorNotes_EditValueChanged

        y += rowH + 14
        _btnOk.Text = If(Eng, "OK", "موافق")
        _btnCancel.Text = If(Eng, "Cancel", "إلغاء")
        _btnOk.SetBounds(204, y, 88, 28)
        _btnCancel.SetBounds(300, y, 88, 28)
        _btnOk.DialogResult = DialogResult.OK
        _btnCancel.DialogResult = DialogResult.Cancel
        ApplyButtonFont(_btnOk)
        ApplyButtonFont(_btnCancel)

        Controls.AddRange(New Control() {
            _lblPatient, _colorPatient,
            _lblReason, _colorReason,
            _lblNotes, _colorNotes,
            _btnOk, _btnCancel
        })
    End Sub

    Public ReadOnly Property ResultPatientNameColor As Color
        Get
            Return _patientNameColor
        End Get
    End Property

    Public ReadOnly Property ResultReasonColor As Color
        Get
            Return _reasonColor
        End Get
    End Property

    Public ReadOnly Property ResultNotesColor As Color
        Get
            Return _notesColor
        End Get
    End Property

    Private Shared Sub ApplyLabelFont(lbl As LabelControl)
        lbl.Appearance.Font = DialogUiFont
        lbl.Appearance.Options.UseFont = True
        lbl.Appearance.Options.UseTextOptions = True
        lbl.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
    End Sub

    Private Shared Sub ApplyButtonFont(btn As SimpleButton)
        btn.Appearance.Font = DialogUiFont
        btn.Appearance.Options.UseFont = True
    End Sub

    Private Sub ConfigureColorEdit(edit As ColorEdit)
        CType(edit.Properties, ISupportInitialize).BeginInit()
        Try
            edit.Properties.Appearance.Font = DialogUiFont
            edit.Properties.Appearance.Options.UseFont = True
            edit.Properties.ColorDialogType = DevExpress.XtraEditors.Popup.ColorDialogType.Advanced
            edit.Properties.Buttons.Clear()
            edit.Properties.Buttons.Add(New EditorButton(ButtonPredefines.Combo))
        Finally
            CType(edit.Properties, ISupportInitialize).EndInit()
        End Try
    End Sub

    Private Sub LayoutRowColorEdit(lbl As LabelControl, edit As ColorEdit, top As Integer, labelW As Integer, editW As Integer, rowH As Integer)
        lbl.SetBounds(12, top, labelW, rowH)
        edit.SetBounds(12 + labelW + 6, top + (rowH - 24) \ 2, editW, 24)
    End Sub

    Private Sub ColorPatient_EditValueChanged(sender As Object, e As EventArgs)
        _patientNameColor = ColorFromEditValue(_colorPatient, _patientNameColor)
    End Sub

    Private Sub ColorReason_EditValueChanged(sender As Object, e As EventArgs)
        _reasonColor = ColorFromEditValue(_colorReason, _reasonColor)
    End Sub

    Private Sub ColorNotes_EditValueChanged(sender As Object, e As EventArgs)
        _notesColor = ColorFromEditValue(_colorNotes, _notesColor)
    End Sub

    Private Function ColorFromEditValue(edit As ColorEdit, fallback As Color) As Color
        If edit Is Nothing Then Return fallback
        Return NormalizeColorValue(edit.EditValue, fallback)
    End Function

    Private Shared Function NormalizeColorValue(editValue As Object, fallback As Color) As Color
        If editValue Is Nothing OrElse editValue Is DBNull.Value Then Return fallback
        If TypeOf editValue Is Color Then
            Dim c = DirectCast(editValue, Color)
            If c.IsEmpty Then Return fallback
            Return c
        End If
        Try
            Return Color.FromArgb(Convert.ToInt32(editValue))
        Catch
            Return fallback
        End Try
    End Function
End Class
