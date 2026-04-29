''' <summary>
''' Display-only mapping for SVG tooth control names: legacy quarter codes RU/LU/RD/LD → UR/UL/LR/LL when
''' <see cref="TreatsUserControl.AlternateQuadrantLabelsEnabled"/> is True. Does not change <see cref="Control.Name"/>.
''' </summary>
Public Module ToothSvgQuadrantNaming


    Public Function FormatSvgControlNameForDisplay(canonicalSvgControlName As String) As String
        If Not TreatsUserControl.AlternateQuadrantLabelsEnabled Then Return canonicalSvgControlName
        If String.IsNullOrEmpty(canonicalSvgControlName) OrElse canonicalSvgControlName.Length < 2 Then Return canonicalSvgControlName

        Dim offset As Integer = 0
        Dim c0 = canonicalSvgControlName(0)
        If c0 = "z"c OrElse c0 = "Z"c Then
            offset = 1
            If canonicalSvgControlName.Length - offset < 2 Then Return canonicalSvgControlName
        End If

        Dim a = canonicalSvgControlName(offset)
        Dim b = canonicalSvgControlName(offset + 1)
        Dim mapped = MapLegacyQuarterPairChars(a, b)
        If mapped Is Nothing Then Return canonicalSvgControlName

        Return canonicalSvgControlName.Substring(0, offset) & mapped & canonicalSvgControlName.Substring(offset + 2)
    End Function

    Private Function MapLegacyQuarterPairChars(a As Char, b As Char) As String
        Dim q = Char.ToUpperInvariant(a).ToString() & Char.ToUpperInvariant(b).ToString()
        Dim letters As String
        Select Case q
            Case "RU" : letters = "UR"
            Case "LU" : letters = "UL"
            Case "RD" : letters = "LR"
            Case "LD" : letters = "LL"
            Case Else : Return Nothing
        End Select

        If Char.IsUpper(a) AndAlso Char.IsUpper(b) Then Return letters
        If Char.IsUpper(a) AndAlso Char.IsLower(b) Then
            Return Char.ToUpperInvariant(letters(0)) & Char.ToLowerInvariant(letters(1))
        End If
        Return letters
    End Function

    ''' <summary>Kid tooth SVGs use OUTK / TOPK / …INK… in the control name; adult uses Out / Top / IN without those K-suffixes.</summary>
    Public Function IsKidJawSvgControlName(controlName As String) As Boolean
        If String.IsNullOrEmpty(controlName) Then Return False
        If controlName.IndexOf("OUTK", StringComparison.OrdinalIgnoreCase) >= 0 Then Return True
        If controlName.IndexOf("TOPK", StringComparison.OrdinalIgnoreCase) >= 0 Then Return True
        If controlName.IndexOf("INK", StringComparison.OrdinalIgnoreCase) >= 0 Then Return True
        Return False
    End Function

    Public Function TryParseFdiToothTag(tagObj As Object) As Integer
        If tagObj Is Nothing OrElse tagObj Is DBNull.Value Then Return 0
        If TypeOf tagObj Is Integer Then Return DirectCast(tagObj, Integer)
        If TypeOf tagObj Is Byte Then Return CInt(DirectCast(tagObj, Byte))
        If TypeOf tagObj Is Short Then Return CInt(DirectCast(tagObj, Short))
        If TypeOf tagObj Is Long Then
            Dim L = DirectCast(tagObj, Long)
            If L < Integer.MinValue OrElse L > Integer.MaxValue Then Return 0
            Return CInt(L)
        End If
        Dim s = Convert.ToString(tagObj, Globalization.CultureInfo.InvariantCulture).Trim()
        Dim n As Integer
        If Integer.TryParse(s, Globalization.NumberStyles.Integer, Globalization.CultureInfo.InvariantCulture, n) Then Return n
        Return 0
    End Function

    Private Function AdultAbbrevFromFdiByte(tag As Byte) As String
        Select Case tag
            Case 11 To 18
                Return "Ru" & (tag - 10)
            Case 21 To 28
                Return "Lu" & (tag - 20)
            Case 31 To 38
                Return "Ld" & (tag - 30)
            Case 41 To 48
                Return "Rd" & (tag - 40)
            Case Else
                Return "Invalid"
        End Select
    End Function

    Private Function KidAbbrevFromFdiByte(tag As Byte) As String
        Select Case tag
            Case 51 To 55
                Return "RU" & (tag - 50)
            Case 61 To 65
                Return "LU" & (tag - 60)
            Case 71 To 75
                Return "LD" & (tag - 70)
            Case 81 To 85
                Return "RD" & (tag - 80)
            Case Else
                Return "Invalid"
        End Select
    End Function

    Private Function GetAdultToothNameByFdi(fdi As Integer) As String
        If fdi < Byte.MinValue OrElse fdi > Byte.MaxValue Then Return ""
        Dim abbrev = AdultAbbrevFromFdiByte(CByte(fdi))
        If abbrev = "Invalid" Then Return ""
        Return GridHelper.GetToothFullName(abbrev.ToUpperInvariant(), TreatsUserControl.AlternateQuadrantLabelsEnabled)
    End Function

    ''' <summary>
    ''' <c>Patient_ToothTrt.ToothName</c> for SQL lookups on adult teeth (FDI 11–48). Uses R/L-first abbrev from FDI so SVG control
    ''' naming (e.g. UR vs RU in <see cref="Control.Name"/>) cannot produce an empty <see cref="GridHelper.GetToothFullName"/> result.
    ''' </summary>
    Public Function GetAdultToothTrtLookupNameByFdi(fdiToothTag As Integer) As String
        Return GetAdultToothNameByFdi(fdiToothTag)
    End Function

    ''' <summary>Same full quadrant + tooth-index label as chart/DB tooth name, for primary teeth (FDI 51–85).</summary>
    Public Function GetKidToothTrtLookupNameByFdi(fdiToothTag As Integer) As String
        Return GetKidToothNameByFdi(fdiToothTag)
    End Function

    Private Function GetKidToothNameByFdi(fdi As Integer) As String
        If fdi < Byte.MinValue OrElse fdi > Byte.MaxValue Then Return ""
        Dim abbrev = KidAbbrevFromFdiByte(CByte(fdi))
        If abbrev = "Invalid" Then Return ""
        Return GridHelper.GetToothFullName(abbrev, TreatsUserControl.AlternateQuadrantLabelsEnabled)
    End Function

    ''' <summary>Human-readable tooth line: quadrant full name + FDI (ISO 3950), same rules as selected-tooth header label.</summary>
    Public Function GetToothHeadingLabelForFdi(isKidJaw As Boolean, fdiToothTag As Integer) As String
        If fdiToothTag <= 0 Then Return String.Empty
        Dim quadrantName = If(isKidJaw, GetKidToothNameByFdi(fdiToothTag), GetAdultToothNameByFdi(fdiToothTag))
        If String.IsNullOrWhiteSpace(quadrantName) Then
            quadrantName = If(isKidJaw, GetAdultToothNameByFdi(fdiToothTag), GetKidToothNameByFdi(fdiToothTag))
        End If
        If String.IsNullOrWhiteSpace(quadrantName) Then Return $" {fdiToothTag}"
        Return $"{quadrantName}  •   {fdiToothTag}"
    End Function

    ''' <summary>SuperTip / tooltip title from SVG control name + tag; falls back to legacy display name if FDI cannot be resolved.</summary>
    Public Function GetToothTooltipHeading(svgControlName As String, tag As Object) As String
        Dim fdi = TryParseFdiToothTag(tag)
        Dim heading = GetToothHeadingLabelForFdi(IsKidJawSvgControlName(svgControlName), fdi)
        If Not String.IsNullOrWhiteSpace(heading) Then Return heading
        Dim legacy = FormatSvgControlNameForDisplay(If(svgControlName, String.Empty))
        Dim tagStr As String
        If tag Is Nothing OrElse tag Is DBNull.Value Then
            tagStr = String.Empty
        Else
            tagStr = Convert.ToString(tag, Globalization.CultureInfo.InvariantCulture)
        End If
        Return $"{legacy} -->> {tagStr}"
    End Function

End Module
