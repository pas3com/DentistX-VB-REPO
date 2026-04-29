Imports System.Globalization
Imports System.Windows.Forms
Imports DevExpress.Utils
Imports DevExpress.XtraEditors
Imports DevExpress.XtraEditors.Mask

''' <summary>
''' Money/numeric TextEdit UX: masks, parse helpers, and on focus empty when zero else select-all (timer-deferred).
''' </summary>
Public Module IntegerMoneyEditorFocus

    Public Sub ConfigureIntegerMoneyTextEdit(te As TextEdit)
        If te Is Nothing Then Return
        te.Properties.Mask.MaskType = MaskType.RegEx
        te.Properties.Mask.EditMask = "\d*"
        te.Properties.Mask.UseMaskAsDisplayFormat = False
        te.Properties.Mask.ShowPlaceHolders = False
        te.Properties.DisplayFormat.FormatType = FormatType.Numeric
        te.Properties.DisplayFormat.FormatString = "f0"
        te.Properties.EditFormat.FormatType = FormatType.Numeric
        te.Properties.EditFormat.FormatString = "f0"
    End Sub

    ''' <summary>Up to 2 decimal places; invariant dot in RegEx (display uses culture via f2).</summary>
    Public Sub ConfigureDecimal2MoneyTextEdit(te As TextEdit)
        If te Is Nothing Then Return
        te.Properties.Mask.MaskType = MaskType.RegEx
        te.Properties.Mask.EditMask = "\d*(\.\d{0,2})?"
        te.Properties.Mask.UseMaskAsDisplayFormat = False
        te.Properties.Mask.ShowPlaceHolders = False
        te.Properties.DisplayFormat.FormatType = FormatType.Numeric
        te.Properties.DisplayFormat.FormatString = "f2"
        te.Properties.EditFormat.FormatType = FormatType.Numeric
        te.Properties.EditFormat.FormatString = "f2"
    End Sub

    Public Function DecimalFromIntegerMoneyEdit(te As TextEdit) As Decimal
        If te Is Nothing Then Return 0D
        Dim ev = te.EditValue
        If ev IsNot Nothing AndAlso Not IsDBNull(ev) Then
            Dim t = Convert.ToString(ev).Trim()
            If t.Length > 0 Then
                Dim d As Decimal
                If Decimal.TryParse(t, NumberStyles.Integer, CultureInfo.CurrentCulture, d) Then Return d
                If Decimal.TryParse(t, NumberStyles.Integer, CultureInfo.InvariantCulture, d) Then Return d
            End If
        End If
        ' EditValue can be Nothing while the visible Text still holds the amount (mask/focus edge cases).
        If Not String.IsNullOrWhiteSpace(te.Text) Then
            Dim d As Decimal
            If Decimal.TryParse(te.Text, NumberStyles.Number, CultureInfo.CurrentCulture, d) Then Return d
            If Decimal.TryParse(te.Text, NumberStyles.Number, CultureInfo.InvariantCulture, d) Then Return d
        End If
        Return 0D
    End Function

    Public Function DecimalFromDecimal2MoneyEdit(te As TextEdit) As Decimal
        If te Is Nothing Then Return 0D
        Dim ev = te.EditValue
        If ev IsNot Nothing AndAlso Not IsDBNull(ev) Then
            Dim t = Convert.ToString(ev).Trim()
            If t.Length > 0 Then
                Dim d As Decimal
                If Decimal.TryParse(t, NumberStyles.Number, CultureInfo.CurrentCulture, d) Then Return d
                If Decimal.TryParse(t, NumberStyles.Number, CultureInfo.InvariantCulture, d) Then Return d
            End If
        End If
        If Not String.IsNullOrWhiteSpace(te.Text) Then
            Dim d As Decimal
            If Decimal.TryParse(te.Text, NumberStyles.Number, CultureInfo.CurrentCulture, d) Then Return d
            If Decimal.TryParse(te.Text, NumberStyles.Number, CultureInfo.InvariantCulture, d) Then Return d
        End If
        Return 0D
    End Function

    Public Sub AttachTextEditZeroEmptyElseSelectAll(ParamArray edits As TextEdit())
        If edits Is Nothing Then Return
        For Each ed In edits
            If ed Is Nothing Then Continue For
            RemoveHandler ed.GotFocus, AddressOf TextEdit_NumericMoney_GotFocus
            AddHandler ed.GotFocus, AddressOf TextEdit_NumericMoney_GotFocus
            RemoveHandler ed.LostFocus, AddressOf TextEdit_NumericMoney_LostFocus
            AddHandler ed.LostFocus, AddressOf TextEdit_NumericMoney_LostFocus
        Next
    End Sub

    Private Sub TextEdit_NumericMoney_GotFocus(sender As Object, e As EventArgs)
        Dim ed = TryCast(sender, TextEdit)
        If ed Is Nothing Then Return
        Dim d = ParseNumericTextEditValue(ed)
        RunDeferredStandaloneTextEditNumericFocus(ed, d)
    End Sub

    Private Sub TextEdit_NumericMoney_LostFocus(sender As Object, e As EventArgs)
        Dim ed = TryCast(sender, TextEdit)
        If ed Is Nothing Then Return
        If String.IsNullOrWhiteSpace(ed.Text) Then
            ed.EditValue = 0D
        End If
    End Sub

    Private Function ParseNumericTextEditValue(te As TextEdit) As Decimal
        If te Is Nothing Then Return 0D
        Try
            If te.EditValue IsNot Nothing AndAlso Not IsDBNull(te.EditValue) Then
                Return Convert.ToDecimal(te.EditValue, CultureInfo.CurrentCulture)
            End If
        Catch
        End Try
        If Not String.IsNullOrWhiteSpace(te.Text) Then
            Dim d As Decimal
            If Decimal.TryParse(te.Text, NumberStyles.Number, CultureInfo.CurrentCulture, d) Then Return d
            If Decimal.TryParse(te.Text, NumberStyles.Number, CultureInfo.InvariantCulture, d) Then Return d
        End If
        Return 0D
    End Function

    Private Sub RunDeferredStandaloneTextEditNumericFocus(te As TextEdit, value As Decimal)
        If te Is Nothing Then Return
        Dim target = te
        Dim t As New Timer With {.Interval = 35}
        Dim pass As Integer = 0
        Dim h As EventHandler = Nothing
        h = Sub()
                pass += 1
                If target.IsDisposed Then
                    RemoveHandler t.Tick, h
                    t.Stop()
                    t.Dispose()
                    Return
                End If
                If value = 0D Then
                    target.Text = ""
                    target.SelectionStart = 0
                    target.SelectionLength = 0
                    RemoveHandler t.Tick, h
                    t.Stop()
                    t.Dispose()
                    Return
                End If
                target.SelectAll()
                If pass >= 7 Then
                    RemoveHandler t.Tick, h
                    t.Stop()
                    t.Dispose()
                End If
            End Sub
        AddHandler t.Tick, h
        t.Start()
    End Sub

End Module
