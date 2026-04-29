' In SvgResources project - SvgResourceProvider.vb
Imports System.IO

Public Class SvgResourceProvider
    Public Shared AdultSvgCache As New Dictionary(Of String, MemoryStream)
    Public Shared KidsSvgCache As New Dictionary(Of String, MemoryStream)

    Public Shared Function GetAdultSvgResourceCached(key As String, type As String) As Stream
        Dim cacheKey = $"{key}_{type}"
        If AdultSvgCache.ContainsKey(cacheKey) Then
            Dim cached = AdultSvgCache(cacheKey)
            cached.Position = 0
            Return cached
        End If

        ' Use the protected provider instead of direct access
        Dim stream = ProtectedResourceProvider.GetProtectedAdultSvg(key, type)
        If stream IsNot Nothing Then
            Dim copy = New MemoryStream()
            stream.CopyTo(copy)
            copy.Position = 0
            AdultSvgCache(cacheKey) = copy
            Return copy
        End If

        Debug.WriteLine($"MISSING adult resource: {key}_{type}")
        Return Nothing
    End Function

    Public Shared Function GetKidsSvgResourceCached(key As String, type As String) As Stream
        Dim cacheKey = $"{key}_{type}"
        If KidsSvgCache.ContainsKey(cacheKey) Then
            Dim cached = KidsSvgCache(cacheKey)
            cached.Position = 0
            Return cached
        End If

        ' Use the protected provider instead of direct access
        Dim stream = ProtectedResourceProvider.GetProtectedKidSvg(key, type)
        If stream IsNot Nothing Then
            Dim copy = New MemoryStream()
            stream.CopyTo(copy)
            copy.Position = 0
            KidsSvgCache(cacheKey) = copy
            Return copy
        End If

        Debug.WriteLine($"MISSING kid resource: {key}_{type}")
        Return Nothing
    End Function

    Public Shared Sub ListAllResources()
        Dim assembly = Reflection.Assembly.GetExecutingAssembly()
        Dim resourceNames = assembly.GetManifestResourceNames()

        Debug.WriteLine("=== ALL EMBEDDED RESOURCES ===")
        For Each name In resourceNames
            Debug.WriteLine(name)
        Next
        Debug.WriteLine("=== END RESOURCES ===")
    End Sub
End Class