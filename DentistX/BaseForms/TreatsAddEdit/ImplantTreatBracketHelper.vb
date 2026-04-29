Imports System.Text.RegularExpressions

''' <summary>
''' Parse and strip the implant bracket appended to Treat text, e.g.
''' "EXTRACTION + IMPLANT [ALFABIO-COMP-Slim 2.90x8.00]" → Brand, Type, Slim, Dmm, Lmm.
''' Format matches AddNewTrtForm SetResult / btnSelect: [Brand-Type-Slim DmmxLmm].
''' </summary>
Public Module ImplantTreatBracketHelper

    Public Function ImplantSpecsPresentInTreat(treat As String) As Boolean
        If String.IsNullOrWhiteSpace(treat) Then Return False
        If Not treat.ToUpperInvariant().Contains("IMPLANT") Then Return False
        Dim i = treat.LastIndexOf("["c)
        Dim j = treat.LastIndexOf("]"c)
        If i < 0 OrElse j <= i Then Return False
        Dim inner = treat.Substring(i + 1, j - i - 1)
        Return inner.Length >= 4 AndAlso inner.Contains("x"c)
    End Function

    Public Function TreatWithoutImplantBracketSuffix(treat As String) As String
        If String.IsNullOrWhiteSpace(treat) Then Return treat
        If Not ImplantSpecsPresentInTreat(treat) Then Return treat.TrimEnd()
        Dim idx = treat.LastIndexOf(" [", StringComparison.Ordinal)
        If idx >= 0 Then Return treat.Substring(0, idx).TrimEnd()
        idx = treat.LastIndexOf("["c)
        If idx > 0 Then Return treat.Substring(0, idx).TrimEnd()
        Return treat.TrimEnd()
    End Function

    ''' <summary>
    ''' Parses inner bracket content as Brand-Type-Slim DmmxLmm (hyphens separate the three name parts; Slim may contain hyphens).
    ''' </summary>
    Public Function TryParseImplantBracketPayload(treat As String,
        ByRef brand As String, ByRef typeName As String, ByRef slim As String,
        ByRef diameterText As String, ByRef lengthText As String) As Boolean
        brand = ""
        typeName = ""
        slim = ""
        diameterText = ""
        lengthText = ""
        If Not ImplantSpecsPresentInTreat(treat) Then Return False
        Dim i = treat.LastIndexOf("["c)
        Dim j = treat.LastIndexOf("]"c)
        If i < 0 OrElse j <= i Then Return False
        Dim inner = treat.Substring(i + 1, j - i - 1).Trim()
        Dim rx As New Regex("^(?<head>.+)\s+(?<d>[\d.,]+)\s*x\s*(?<l>[\d.,]+)\s*$", RegexOptions.CultureInvariant Or RegexOptions.IgnoreCase)
        Dim m = rx.Match(inner)
        If Not m.Success Then Return False
        Dim head = m.Groups("head").Value.Trim()
        diameterText = m.Groups("d").Value.Trim()
        lengthText = m.Groups("l").Value.Trim()
        If head.Length = 0 OrElse diameterText.Length = 0 OrElse lengthText.Length = 0 Then Return False

        Dim parts = head.Split("-"c)
        If parts.Length < 3 Then Return False
        brand = parts(0).Trim()
        typeName = parts(1).Trim()
        Dim sb As New System.Text.StringBuilder
        sb.Append(parts(2).Trim())
        For k = 3 To parts.Length - 1
            sb.Append("-"c)
            sb.Append(parts(k).Trim())
        Next
        slim = sb.ToString()
        Return brand.Length > 0 AndAlso typeName.Length > 0 AndAlso slim.Length > 0
    End Function

End Module
