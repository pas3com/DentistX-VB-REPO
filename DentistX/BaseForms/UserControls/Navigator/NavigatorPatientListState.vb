Imports System.Globalization
Imports System.Linq
Imports System.Text
Imports System.Text.RegularExpressions

''' <summary>
''' In-memory patient lists and category/search narrowing for the navigator header.
''' No WinForms controls — binding and UI stay in <see cref="Navigator3"/>.
''' </summary>
Friend NotInheritable Class NavigatorPatientListState

    Friend ReadOnly AllPatients As New List(Of Patient)()
    Friend FilteredPatients As List(Of Patient)

    Private _categoryTarget As String
    ''' <summary>ALL / Treat / Ortho / Diag / Mobile / or other workspace-specific tag (e.g. Implant → treated as broad list).</summary>
    Friend Property CategoryTarget As String
        Get
            Return _categoryTarget
        End Get
        Set(value As String)
            _categoryTarget = If(String.IsNullOrEmpty(value), NavigatorPatientFilters.PatientFilterAll, value)
        End Set
    End Property

    ''' <summary>After a deliberate park (empty binding), search debounce must not refill until the user types or picks.</summary>
    Friend Property ParkBindingEmptyUntilUserQuery As Boolean

    Friend Sub New()
        FilteredPatients = New List(Of Patient)()
        _categoryTarget = NavigatorPatientFilters.PatientFilterAll
    End Sub

    Friend Sub New(initialCategoryTarget As String)
        FilteredPatients = New List(Of Patient)()
        CategoryTarget = If(String.IsNullOrEmpty(initialCategoryTarget), NavigatorPatientFilters.PatientFilterAll, initialCategoryTarget)
    End Sub

    Friend Sub RebuildFilteredFromAll()
        If AllPatients Is Nothing Then
            FilteredPatients = New List(Of Patient)()
            Return
        End If
        Select Case _categoryTarget
            Case NavigatorPatientFilters.PatientFilterTreat
                FilteredPatients = AllPatients.Where(Function(p) p.Treat.GetValueOrDefault(False)).ToList()
            Case NavigatorPatientFilters.PatientFilterOrtho
                FilteredPatients = AllPatients.Where(Function(p) p.Ortho.GetValueOrDefault(False)).ToList()
            Case NavigatorPatientFilters.PatientFilterDiag
                FilteredPatients = AllPatients.Where(Function(p) p.Diag.GetValueOrDefault(False)).ToList()
            Case NavigatorPatientFilters.PatientFilterMobile
                FilteredPatients = AllPatients.Where(Function(p) p.Mobile.GetValueOrDefault(False)).ToList()
            Case Else
                FilteredPatients = New List(Of Patient)(AllPatients)
        End Select
    End Sub

    Friend Sub UpdateParkIntentAfterRefresh(parkNoCurrentPatient As Boolean, bindingCount As Integer)
        If parkNoCurrentPatient AndAlso bindingCount = 0 Then
            ParkBindingEmptyUntilUserQuery = True
        ElseIf bindingCount > 0 Then
            ParkBindingEmptyUntilUserQuery = False
        End If
    End Sub

    ''' <summary>Patients matching category filter plus optional search text.</summary>
    ''' <param name="explicitSearchText">When not Nothing, use this string (same as passing searchText from the navigator). When Nothing, uses <paramref name="fallbackBoxText"/>.</param>
    Friend Function BuildSearchFilteredPatientList(Optional explicitSearchText As String = Nothing, Optional fallbackBoxText As String = Nothing) As List(Of Patient)
        Dim result As New List(Of Patient)()
        If FilteredPatients Is Nothing Then Return result
        Dim source As IEnumerable(Of Patient) = FilteredPatients

        Dim raw As String
        If explicitSearchText IsNot Nothing Then
            raw = explicitSearchText.Trim()
        Else
            raw = If(fallbackBoxText, "").Trim()
        End If

        Dim idDigits = Regex.Replace(raw, "\D", "")
        Dim nameQ = Regex.Replace(raw, "\d", "").Trim()

        If idDigits.Length > 0 Then
            source = source.Where(Function(p) Not String.IsNullOrEmpty(p.PatientNumber) AndAlso
                p.PatientNumber.EndsWith(idDigits, StringComparison.OrdinalIgnoreCase))
        End If

        If nameQ.Length > 0 Then
            source = source.Where(Function(p) NameMatchesSearch(If(p.PatientName, ""), nameQ, 1))
        End If

        result.AddRange(source)
        Return result
    End Function

    Friend Shared Function IsMajorNavigatorFilterTarget(filterTarget As String) As Boolean
        If String.IsNullOrWhiteSpace(filterTarget) Then Return True
        Return String.Equals(filterTarget, NavigatorPatientFilters.PatientFilterAll, StringComparison.OrdinalIgnoreCase) OrElse
               String.Equals(filterTarget, NavigatorPatientFilters.PatientFilterTreat, StringComparison.OrdinalIgnoreCase) OrElse
               String.Equals(filterTarget, NavigatorPatientFilters.PatientFilterOrtho, StringComparison.OrdinalIgnoreCase) OrElse
               String.Equals(filterTarget, NavigatorPatientFilters.PatientFilterDiag, StringComparison.OrdinalIgnoreCase) OrElse
               String.Equals(filterTarget, NavigatorPatientFilters.PatientFilterMobile, StringComparison.OrdinalIgnoreCase)
    End Function

    Friend Shared Function NormalizePatientSearchText(value As String) As String
        If String.IsNullOrWhiteSpace(value) Then Return ""

        Dim raw As String = value.Trim().Normalize(NormalizationForm.FormKC)
        Dim sb As New StringBuilder(raw.Length)
        Dim prevWasSpace As Boolean = False

        For Each ch As Char In raw
            Dim mapped As String = Nothing
            Select Case ch
                Case ChrW(&H622), ChrW(&H623), ChrW(&H625), ChrW(&H671) ' alef variants
                    mapped = ChrW(&H627)
                Case ChrW(&H624) ' waw with hamza
                    mapped = ChrW(&H648)
                Case ChrW(&H626), ChrW(&H649), ChrW(&H6CC) ' yaa variants
                    mapped = ChrW(&H64A)
                Case ChrW(&H6A9) ' Persian keheh -> Arabic kaf
                    mapped = ChrW(&H643)
                Case ChrW(&H640), ChrW(&H200C), ChrW(&H200D), ChrW(&H200E), ChrW(&H200F)
                    Continue For
            End Select

            Dim cat As UnicodeCategory = CharUnicodeInfo.GetUnicodeCategory(ch)
            If cat = UnicodeCategory.NonSpacingMark OrElse
               cat = UnicodeCategory.SpacingCombiningMark OrElse
               cat = UnicodeCategory.EnclosingMark Then
                Continue For
            End If

            Dim outText As String = If(mapped, ch.ToString())
            For Each outCh As Char In outText
                If Char.IsWhiteSpace(outCh) Then
                    If Not prevWasSpace Then
                        sb.Append(" "c)
                        prevWasSpace = True
                    End If
                Else
                    sb.Append(outCh)
                    prevWasSpace = False
                End If
            Next
        Next

        Return sb.ToString().Trim()
    End Function

    ''' <summary>Search mode: 0 first, 1 contains, 2 last.</summary>
    Friend Shared Function NameMatchesSearch(name As String, q As String, searchMethod As Integer) As Boolean
        Dim normalizedName As String = NormalizePatientSearchText(name)
        Dim normalizedQuery As String = NormalizePatientSearchText(q)
        If String.IsNullOrEmpty(normalizedName) OrElse String.IsNullOrEmpty(normalizedQuery) Then Return False

        Select Case searchMethod
            Case 0
                Return normalizedName.StartsWith(normalizedQuery, StringComparison.OrdinalIgnoreCase)
            Case 1
                Return normalizedName.IndexOf(normalizedQuery, StringComparison.OrdinalIgnoreCase) >= 0
            Case 2
                Return normalizedName.EndsWith(normalizedQuery, StringComparison.OrdinalIgnoreCase)
            Case Else
                Return normalizedName.IndexOf(normalizedQuery, StringComparison.OrdinalIgnoreCase) >= 0
        End Select
    End Function
End Class
