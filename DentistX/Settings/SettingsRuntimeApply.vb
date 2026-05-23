Imports System.Configuration
Imports System.Globalization
Imports System.Threading
Imports Infralution.Localization

''' <summary>
''' Applies settings that normally run in <c>MyApplication_Startup</c> without restarting the app.
''' </summary>
Friend Module SettingsRuntimeApply

    ''' <summary>Persists <c>appSettings/Language</c> (same pattern as <see cref="FrmNewSettings.ChangLang"/>).</summary>
    Public Sub PersistLanguageToAppConfig(lang As String)
        Dim code = NormalizeUiLanguageCode(lang)
        Try
            Dim config As Configuration = ConfigurationManager.OpenExeConfiguration(System.Reflection.Assembly.GetExecutingAssembly().Location)
            config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None)
            Dim protectedSection As AppSettingsSection = CType(config.GetSection("appSettings"), AppSettingsSection)
            If protectedSection Is Nothing Then Return
            protectedSection.SectionInformation.UnprotectSection()
            config.Save()
            protectedSection.Settings.Item("Language").Value = code
            config.Save()
            ConfigurationManager.RefreshSection("appSettings")
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        Try
            My.Settings.UiLanguage = code
            My.Settings.Save()
        Catch
        End Try
    End Sub

    ''' <summary>Returns <c>en</c> or <c>ar</c>.</summary>
    Public Function NormalizeUiLanguageCode(lang As String) As String
        If String.IsNullOrWhiteSpace(lang) Then Return "en"
        Dim s = lang.Trim()
        If s.StartsWith("en", StringComparison.OrdinalIgnoreCase) Then Return "en"
        If s.StartsWith("ar", StringComparison.OrdinalIgnoreCase) Then Return "ar"
        Return "en"
    End Function

    ''' <summary>
    ''' Regional culture for Arabic UI: Jordan formatting with Gregorian calendar only.
    ''' <see cref="CultureInfo.CreateSpecificCulture"/>(<c>ar-JO</c>) can follow Windows calendar overrides (Hijri);
    ''' neutral <c>ar</c> as <see cref="CultureInfo.CurrentCulture"/> uses Um Al Qura — avoid both for consistent Gregorian dates.
    ''' </summary>
    Friend Function CreateEnglishRegionalCultureGregorian() As CultureInfo
        Dim c = New CultureInfo("en-US", useUserOverride:=False)
        AppointDateFormat.ApplyToCulture(c)
        Return c
    End Function

    Friend Function CreateArabicRegionalCultureGregorian() As CultureInfo
        Dim c = New CultureInfo("ar-JO", useUserOverride:=False)
        AppointDateFormat.ApplyToCulture(c)
        Return c
    End Function

    ''' <summary>
    ''' English: neutral <c>en</c> UI + <c>en-US</c> regional. Arabic: Gregorian <c>ar-JO</c> for both UI and regional
    ''' so <see cref="CultureInfo.CurrentUICulture"/> date formatting is never Um Al Qura; .resx fallbacks still resolve (<c>ar-JO</c> → <c>ar</c>).
    ''' </summary>
    Public Sub ApplyLanguageFromSettingsCode(lang As String)
        lang = NormalizeUiLanguageCode(lang)
        Dim uiCulture As CultureInfo
        Dim regionalCulture As CultureInfo
        If String.Equals(lang, "en", StringComparison.OrdinalIgnoreCase) Then
            Eng = True
            uiCulture = New CultureInfo("en", useUserOverride:=False)
            regionalCulture = CreateEnglishRegionalCultureGregorian()
        Else
            Eng = False
            regionalCulture = CreateArabicRegionalCultureGregorian()
            uiCulture = CType(regionalCulture.Clone(), CultureInfo)
        End If
        ' Infralution: raises change on every <see cref="CultureManager"/> with <see cref="CultureManager.ManagedControl"/> set.
        CultureManager.ApplicationUICulture = uiCulture
        Thread.CurrentThread.CurrentUICulture = uiCulture
        Thread.CurrentThread.CurrentCulture = regionalCulture
        CultureInfo.DefaultThreadCurrentCulture = regionalCulture
        CultureInfo.DefaultThreadCurrentUICulture = uiCulture
        RuntimeUiLanguage.RefreshAllOpenFormsForCulture(uiCulture)
    End Sub

End Module
