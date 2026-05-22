Imports System.Globalization
Imports System.ComponentModel
Imports System.Windows.Forms
Imports DevExpress.Utils
Imports DevExpress.XtraEditors
Imports DevExpress.XtraEditors.Mask

''' <summary>
''' Integer-only money TextEdits: digits-only entry (<see cref="ConfigureIntegerMoneyTextEdit"/>).
''' - Display never shows fractional zeros — values are truncated to whole units.
''' - <see cref="AttachTextEditZeroEmptyElseSelectAll"/>: on GotFocus clears the box when value is 0, otherwise SelectAll;
'''   on LostFocus normalizes to empty text when 0, otherwise plain integer text (no decimals).
''' </summary>
Public Module IntegerMoneyEditorFocus

    ''' <summary>
    ''' Configures the editor for whole numbers only: RegEx digits, no mask-as-display corruption,
    ''' numeric format fixed 0 decimals (still prefer binding via <see cref="WireIntegerMoneyFieldBinding"/> for data-bound fields).
    ''' </summary>
    Public Sub ConfigureIntegerMoneyTextEdit(te As TextEdit)
        If te Is Nothing Then Return
        te.Properties.Mask.MaskType = MaskType.RegEx
        te.Properties.Mask.EditMask = "\d*"
        ' Must clear BOTH: designer/resx often sets Properties.UseMaskAsDisplayFormat=True.
        ' If True, RegEx "\d*" strips the decimal/group chars from formatted decimals (e.g. "130,00" -> "13000").
        te.Properties.UseMaskAsDisplayFormat = False
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
        te.Properties.UseMaskAsDisplayFormat = False
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
                If Decimal.TryParse(t, NumberStyles.Integer, CultureInfo.CurrentCulture, d) Then Return Decimal.Truncate(d)
                If Decimal.TryParse(t, NumberStyles.Integer, CultureInfo.InvariantCulture, d) Then Return Decimal.Truncate(d)
            End If
        End If
        ' EditValue can be Nothing while the visible Text still holds the amount (mask/focus edge cases).
        If Not String.IsNullOrWhiteSpace(te.Text) Then
            Dim d As Decimal
            If Decimal.TryParse(te.Text, NumberStyles.Number, CultureInfo.CurrentCulture, d) Then Return Decimal.Truncate(d)
            If Decimal.TryParse(te.Text, NumberStyles.Number, CultureInfo.InvariantCulture, d) Then Return Decimal.Truncate(d)
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

    ''' <summary>Binds Text to a numeric member with Format/Parse so culture decimals (e.g. "130,00") never corrupt digit-only masks.</summary>
    Public Sub WireIntegerMoneyFieldBinding(te As TextEdit, dataSource As Object, dataMember As String)
        If te Is Nothing OrElse dataSource Is Nothing Then Return
        te.DataBindings.Clear()
        Dim b As New Binding("Text", dataSource, dataMember, True, DataSourceUpdateMode.OnPropertyChanged)
        AddHandler b.Format, AddressOf Binding_FormatTruncatedDecimalAsIntegerText
        AddHandler b.Parse, AddressOf Binding_ParseIntegerMoneyText
        te.DataBindings.Add(b)
    End Sub

    Public Sub Binding_FormatTruncatedDecimalAsIntegerText(sender As Object, e As ConvertEventArgs)
        If e.Value Is Nothing OrElse IsDBNull(e.Value) Then
            e.Value = ""
            Return
        End If
        Try
            Dim d = Convert.ToDecimal(e.Value, CultureInfo.InvariantCulture)
            d = Decimal.Truncate(d)
            If d = 0D Then
                e.Value = ""
            Else
                e.Value = d.ToString("0", CultureInfo.CurrentCulture)
            End If
        Catch
            e.Value = ""
        End Try
    End Sub

    Public Sub Binding_ParseIntegerMoneyText(sender As Object, e As ConvertEventArgs)
        If e.Value Is Nothing Then
            e.Value = 0D
            Return
        End If
        Dim s = Convert.ToString(e.Value).Trim()
        If String.IsNullOrEmpty(s) Then
            e.Value = 0D
            Return
        End If
        Dim d As Decimal
        If Decimal.TryParse(s, NumberStyles.Integer, CultureInfo.CurrentCulture, d) Then
            e.Value = d
            Return
        End If
        If Decimal.TryParse(s, NumberStyles.Integer, CultureInfo.InvariantCulture, d) Then
            e.Value = d
            Return
        End If
        If Decimal.TryParse(s, NumberStyles.Currency Or NumberStyles.Number, CultureInfo.CurrentCulture, d) Then
            e.Value = Decimal.Truncate(d)
            Return
        End If
        If Decimal.TryParse(s, NumberStyles.Currency Or NumberStyles.Number, CultureInfo.InvariantCulture, d) Then
            e.Value = Decimal.Truncate(d)
            Return
        End If
        e.Value = 0D
    End Sub

    ''' <summary>GotFocus: empty text when value is 0, otherwise select all. LostFocus: normalize to empty (0) or integer text only.</summary>
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
        NormalizeIntegerMoneyTextEditDisplay(ed)
    End Sub

    ''' <summary>After editing: show nothing for 0; otherwise integer text only (no decimal zeros).</summary>
    Public Sub NormalizeIntegerMoneyTextEditDisplay(ed As TextEdit)
        If ed Is Nothing Then Return
        Dim d = ParseNumericTextEditValue(ed)
        If d = 0D Then
            ed.EditValue = 0D
            ed.Text = ""
        Else
            Dim shown = d.ToString("0", CultureInfo.CurrentCulture)
            ed.EditValue = d
            If Not String.Equals(ed.Text, shown, StringComparison.Ordinal) Then ed.Text = shown
        End If
    End Sub

    Private Function ParseNumericTextEditValue(te As TextEdit) As Decimal
        If te Is Nothing Then Return 0D
        Try
            If te.EditValue IsNot Nothing AndAlso Not IsDBNull(te.EditValue) Then
                Return Decimal.Truncate(Convert.ToDecimal(te.EditValue, CultureInfo.CurrentCulture))
            End If
        Catch
        End Try
        If Not String.IsNullOrWhiteSpace(te.Text) Then
            Dim d As Decimal
            If Decimal.TryParse(te.Text, NumberStyles.Number, CultureInfo.CurrentCulture, d) Then Return Decimal.Truncate(d)
            If Decimal.TryParse(te.Text, NumberStyles.Number, CultureInfo.InvariantCulture, d) Then Return Decimal.Truncate(d)
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
