Imports System.Configuration
Imports System.IO
Imports System.Text
Imports System.Xml

Public Module KeyManager
    ' Cache the loaded key
    Private _cachedPublicKey As String = Nothing

    Public ReadOnly Property DevPublicKeyXml As String
        Get
            If _cachedPublicKey Is Nothing Then
                _cachedPublicKey = LoadPublicKey()
            End If
            Return _cachedPublicKey
        End Get
    End Property

    Private Function LoadPublicKey() As String
        ' Order of precedence:
        ' 1. External file (most secure)
        ' 2. App.config (convenient)
        ' 3. Embedded SAMPLE_KEYS (fallback)

        ' Try external file first
        Dim externalKey = LoadFromExternalFile()
        If Not String.IsNullOrEmpty(externalKey) Then
            Return externalKey
        End If

        ' Try app.config
        Dim configKey = LoadFromAppConfig()
        If Not String.IsNullOrEmpty(configKey) Then
            Return configKey
        End If

        ' Fallback to SAMPLE_KEYS
        Return SAMPLE_KEYS.DevPublicKeyXml
    End Function

    Private Function LoadFromExternalFile() As String
        Try
            Dim keyPaths = New String() {
                Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Keys", "PublicKey.xml"),
                Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "PublicKey.xml"),
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "DentistX", "PublicKey.xml")
            }

            For Each keyPath In keyPaths
                If File.Exists(keyPath) Then
                    Return File.ReadAllText(keyPath)
                End If
            Next

            Return Nothing
        Catch
            Return Nothing
        End Try
    End Function

    Private Function LoadFromAppConfig() As String
        Try
            Dim keyFromConfig = ConfigurationManager.AppSettings("DevPublicKeyXml")

            If String.IsNullOrEmpty(keyFromConfig) Then
                Return Nothing
            End If

            ' Unescape XML if needed
            Return UnescapeXml(keyFromConfig)

        Catch
            Return Nothing
        End Try
    End Function

    Private Function UnescapeXml(escapedXml As String) As String
        ' Replace common HTML/XML entities
        Dim unescaped = escapedXml

        ' Replace common XML entities
        unescaped = unescaped.Replace("&lt;", "<")
        unescaped = unescaped.Replace("&gt;", ">")
        unescaped = unescaped.Replace("&amp;", "&")
        unescaped = unescaped.Replace("&quot;", """")
        unescaped = unescaped.Replace("&apos;", "'")

        ' Also handle numeric entities if present
        unescaped = System.Text.RegularExpressions.Regex.Replace(unescaped, "&#(\d+);",
            Function(m) ChrW(Integer.Parse(m.Groups(1).Value)).ToString())

        unescaped = System.Text.RegularExpressions.Regex.Replace(unescaped, "&#x([0-9A-F]+);",
            Function(m) ChrW(Convert.ToInt32(m.Groups(1).Value, 16)).ToString(),
            System.Text.RegularExpressions.RegexOptions.IgnoreCase)

        Return unescaped
    End Function

    Public Function IsKeyValid() As Boolean
        Try
            If String.IsNullOrEmpty(DevPublicKeyXml) Then
                Return False
            End If

            ' Basic XML validation
            If Not DevPublicKeyXml.Contains("<RSAKeyValue>") OrElse
               Not DevPublicKeyXml.Contains("<Modulus>") OrElse
               Not DevPublicKeyXml.Contains("</Modulus>") Then
                Return False
            End If

            ' Try to load into RSA object
            Using rsa = New System.Security.Cryptography.RSACryptoServiceProvider()
                rsa.FromXmlString(DevPublicKeyXml)
                Return True
            End Using

        Catch
            Return False
        End Try
    End Function

    Public Function GetKeySource() As String
        ' Determine where the key was loaded from
        If _cachedPublicKey Is Nothing Then
            LoadPublicKey()
        End If

        ' Check external file
        Dim externalPaths = New String() {
            Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Keys", "PublicKey.xml"),
            Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "PublicKey.xml")
        }

        For Each path In externalPaths
            If File.Exists(path) AndAlso
               File.ReadAllText(path) = _cachedPublicKey Then
                Return $"External file: {path}"
            End If
        Next

        ' Check app.config
        Try
            Dim configKey = ConfigurationManager.AppSettings("DevPublicKeyXml")
            If Not String.IsNullOrEmpty(configKey) Then
                Dim decodedKey = UnescapeXml(configKey)
                If decodedKey = _cachedPublicKey Then
                    Return "App.config"
                End If
            End If
        Catch
        End Try

        Return "SAMPLE_KEYS (fallback)"
    End Function

    ' Helper to escape XML for storing in app.config
    Public Function EscapeXml(xmlString As String) As String
        If String.IsNullOrEmpty(xmlString) Then Return ""

        Dim escaped = xmlString
        escaped = escaped.Replace("&", "&amp;")
        escaped = escaped.Replace("<", "&lt;")
        escaped = escaped.Replace(">", "&gt;")
        escaped = escaped.Replace("""", "&quot;")
        escaped = escaped.Replace("'", "&apos;")

        Return escaped
    End Function
End Module