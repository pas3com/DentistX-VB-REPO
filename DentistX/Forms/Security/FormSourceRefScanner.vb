Imports System.Collections.Generic
Imports System.IO
Imports System.Linq
Imports System.Text.RegularExpressions

''' <summary>
''' Estimates how often each type name appears in VB source (whole-wordRegex).
''' Heuristic only: includes class/Designer declarations; misses reflection/string-based opens. No extra NuGet packages.
''' </summary>
Public Module FormSourceRefScanner

    ''' <summary>Folder that contains DentistX.vbproj, or Nothing if not found (e.g. deployed exe only).</summary>
    Public Function TryFindDentistXProjectSourceRoot() As String
        Dim dir As String = AppDomain.CurrentDomain.BaseDirectory
        For i = 0 To 12
            Dim vbproj = Path.Combine(dir, "DentistX.vbproj")
            If File.Exists(vbproj) Then Return Path.GetFullPath(dir)
            Dim parent = Directory.GetParent(dir)
            If parent Is Nothing Then Exit For
            dir = parent.FullName
        Next
        Return Nothing
    End Function

    ''' <summary>Counts whole-word matches of each type name across all .vb files under <paramref name="projectRoot"/>.</summary>
    Public Function CountNameHitsInProjectSource(projectRoot As String, typeNames As IEnumerable(Of String)) As Dictionary(Of String, Integer)
        Dim result As New Dictionary(Of String, Integer)(StringComparer.OrdinalIgnoreCase)
        If String.IsNullOrWhiteSpace(projectRoot) OrElse Not Directory.Exists(projectRoot) Then Return result

        Dim nameList = typeNames.
            Where(Function(n) Not String.IsNullOrWhiteSpace(n)).
            Select(Function(n) n.Trim()).
            Distinct(StringComparer.OrdinalIgnoreCase).
            ToList()
        If nameList.Count = 0 Then Return result

        Dim texts As New List(Of String)
        For Each fp In Directory.EnumerateFiles(projectRoot, "*.vb", SearchOption.AllDirectories)
            Dim rel = fp.Substring(projectRoot.Length).TrimStart("\"c, "/"c)
            If rel.IndexOf("\bin\", StringComparison.OrdinalIgnoreCase) >= 0 Then Continue For
            If rel.IndexOf("/bin/", StringComparison.OrdinalIgnoreCase) >= 0 Then Continue For
            If rel.IndexOf("\obj\", StringComparison.OrdinalIgnoreCase) >= 0 Then Continue For
            If rel.IndexOf("/obj/", StringComparison.OrdinalIgnoreCase) >= 0 Then Continue For
            If rel.IndexOf("\packages\", StringComparison.OrdinalIgnoreCase) >= 0 Then Continue For
            Try
                texts.Add(File.ReadAllText(fp))
            Catch
            End Try
        Next

        Dim combined = String.Join(vbLf, texts)
        For Each name In nameList
            Dim pattern = "\b" & Regex.Escape(name) & "\b"
            result(name) = Regex.Matches(combined, pattern, RegexOptions.IgnoreCase).Count
        Next
        Return result
    End Function

End Module
