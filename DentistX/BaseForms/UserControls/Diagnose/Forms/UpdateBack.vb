Imports System.Drawing.Drawing2D
Imports DevExpress.Office.Utils
Imports DevExpress.XtraEditors

Public Class UpdateBack

    Private _startColor As Color
    Private _endColor As Color

    Property startColor As Color
        Get
            Return _startColor
        End Get
        Set(value As Color)
            _startColor = value
        End Set
    End Property

    Property endColor As Color
        Get
            Return _endColor
        End Get
        Set(value As Color)
            _endColor = value
        End Set
    End Property

    Property gradientMode As Drawing2D.LinearGradientMode
        Get
            Return CType([Enum].Parse(GetType(Drawing2D.LinearGradientMode), cboGradient.SelectedItem.ToString()), Drawing2D.LinearGradientMode)
        End Get
        Set(value As Drawing2D.LinearGradientMode)
            cboGradient.SelectedItem = value.ToString()
        End Set
    End Property

    Property Glass As GlassStyle
        Get
            Return CType([Enum].Parse(GetType(GlassStyle), cboGlassStyle.SelectedItem.ToString()), GlassStyle)
        End Get
        Set(value As GlassStyle)
            cboGlassStyle.SelectedItem = value.ToString()
        End Set
    End Property
    Public Enum GlassStyle
        Simple
        Aero
        Frosted
        Reflective
        Metallic
        Aqua
        None
    End Enum


    Private Sub UpdateBack_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        FillCboGradient()
        FillCboGlassStyle()
    End Sub

    'fill cboGlassStyle with enum values
    Private Sub FillCboGlassStyle()
        ' Generic method to fill combo with any enum
        FillComboWithEnum(cboGlassStyle, GetType(GlassStyle))
    End Sub

    Private Sub FillCboGradient()
        ' Generic method to fill combo with any enum
        FillComboWithEnum(cboGradient, GetType(Drawing2D.LinearGradientMode))
    End Sub

    Public Sub FillComboWithEnum(combo As DevExpress.XtraEditors.ComboBoxEdit, enumType As Type)
        combo.Properties.Items.Clear()

        If Not enumType.IsEnum Then
            Throw New ArgumentException("Type must be an enumeration", NameOf(enumType))
        End If

        For Each name As String In [Enum].GetNames(enumType)
            combo.Properties.Items.Add(name)
        Next

        If combo.Properties.Items.Count > 0 Then
            combo.SelectedIndex = 0
        End If
    End Sub

    ' Usage for other enums:
    Private Sub FillCboWrapMode()
        FillComboWithEnum(cboGradient, GetType(Drawing2D.WrapMode))
    End Sub

    Private Sub FillCboDashStyle()
        FillComboWithEnum(cboGradient, GetType(Drawing2D.DashStyle))
    End Sub

    Private Sub firstColor_EditValueChanged(sender As Object, e As EventArgs) Handles firstColor.EditValueChanged
        startColor = firstColor.Color
    End Sub

    Private Sub secondColor_EditValueChanged(sender As Object, e As EventArgs) Handles secondColor.EditValueChanged
        endColor = secondColor.Color
    End Sub

    Private Sub btnApply_Click(sender As Object, e As EventArgs) Handles btnApply.Click
        Me.DialogResult = DialogResult.OK
        Me.Close()
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.DialogResult = DialogResult.Cancel
        Me.Close()
    End Sub
End Class