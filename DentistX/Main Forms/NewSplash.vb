Imports System.Configuration
Imports System.Globalization
Imports System.Threading.Tasks
Imports DevExpress.XtraRichEdit.Model
Imports Infralution.Localization
Imports SvgResources

Public Class NewSplash
    Sub New()
        InitializeComponent()
        'UpdateLanguageMenus()
        Me.labelCopyright.Text = If(Eng, "Copyright © 2007-" & DateTime.Now.Year.ToString(), "حقوق الطبع © 2007-" & DateTime.Now.Year.ToString())
    End Sub


    Public Overrides Sub ProcessCommand(ByVal cmd As System.Enum, ByVal arg As Object)
        MyBase.ProcessCommand(cmd, arg)
    End Sub

    Public Enum SplashScreenCommand
        SomeCommandId
    End Enum
    Private Sub NewSplash_Load(sender As Object, e As EventArgs) Handles Me.Load
        'Helpers.LoadAdultSvgResources()
    End Sub

    Public Sub SetStatusText(text As String)
        If Me.InvokeRequired Then
            Me.Invoke(Sub() labelStatus.Text = text)
        Else
            labelStatus.Text = text
        End If
    End Sub

    Private Async Sub NewSplash_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        Dim sw = StartTimer()
        sw.Start()
        ' Debug: List all available resources
        'SvgResourceProvider.ListAllResources()

        ' Load both adult and kid resources in parallel
        Dim adultTask = Helpers.LoadAdultSvgResourcesAsync(AddressOf SetStatusText)
        Dim kidTask = Helpers.LoadKidSvgResourcesAsync(AddressOf SetStatusText)

        ' Wait for both to complete
        Await Task.WhenAll(adultTask, kidTask)
        sw.Stop()
        Console.WriteLine($"Loading took {sw.ElapsedMilliseconds} ms")
        ' Close the splash screen
        Me.Invoke(Sub() Me.Close())
    End Sub
    'Private Async Sub NewSplash_Shown(sender As Object, e As EventArgs) Handles Me.Shown
    '    Dim sw = StartTimer()
    '    sw.Start()
    '    Await Task.Run(Sub()
    '                       Helpers.LoadAdultSvgResourcesAsync(AddressOf SetStatusText)
    '                       Helpers.LoadKidSvgResourcesAsync(AddressOf SetStatusText)
    '                   End Sub)
    '    'Me.Close() ' or signal that splash is done
    '    sw.Stop()
    '    Console.WriteLine($"Loading took {sw.ElapsedMilliseconds} ms")
    '    ' Close the splash after loading is done
    '    Me.Invoke(Sub() Me.Close())

    'End Sub

    '===========My Code==================
    Dim config As Configuration = ConfigurationManager.OpenExeConfiguration(System.Reflection.Assembly.GetExecutingAssembly().Location)

    'Dim section As AppSettingsSection = config.AppSettings




    Private Sub UpdateLanguageMenus()

        'Dim x As Boolean = True
        'Dim vaultSection As NameValueCollection = CType(ConfigurationManager.GetSection("Vault"), NameValueCollection)
        Dim appSet As AppSettingsSection = CType(config.GetSection("appSettings"), AppSettingsSection)
        Dim lang As String = appSet.Settings.Item("Language").Value
        Dim db As String = appSet.Settings.Item("DbType").Value
        Dim sizeM As String = appSet.Settings.Item("Size").Value
        Dim HostNam As String = appSet.Settings.Item("Server").Value
        SizeMode = sizeM
        DbT = db
        HostName = HostNam
        Dim culture As CultureInfo = CultureManager.ApplicationUICulture
        If lang = "ar" Then
            RadioAr.Checked = True
            RadioAr.Checked = culture.TwoLetterISOLanguageName = "ar"
            Eng = False

        ElseIf lang = "en" Then
            RadioEn.Checked = True
            RadioEn.Checked = culture.TwoLetterISOLanguageName = "en"
            Me.RightToLeftLayout = False
            Me.RightToLeft = Windows.Forms.RightToLeft.No
            Eng = True
        End If


    End Sub
    Private Sub cultureManager_UICultureChanged(ByVal newCulture As CultureInfo) Handles CultureMngr.UICultureChanged
        'UpdateLanguageMenus()
    End Sub

    Private Sub RadioAr_CheckedChanged(sender As Object, e As EventArgs) Handles RadioAr.CheckedChanged
        ''Dim myDTFI As DateTimeFormatInfo = New CultureInfo("en", False).DateTimeFormat
        'CultureManager.ApplicationUICulture = New CultureInfo("ar") ' With {.DateTimeFormat = myDTFI} ' With {.Calendar = New GregorianCalendar}}
        ''CultureInfo.InvariantCulture.DateTimeFormat.Calendar = New GregorianCalendar
        ''CultureManager.ApplicationUICulture.DateTimeFormat = CultureInfo.InvariantCulture.DateTimeFormat
    End Sub

    Private Sub RadioEn_CheckedChanged(sender As Object, e As EventArgs) Handles RadioEn.CheckedChanged
        'CultureManager.ApplicationUICulture = New CultureInfo("en")
    End Sub
End Class

'Private Async Sub NewSplash_LoadAsync(sender As Object, e As EventArgs) Handles Me.Load
'    ' Async wrapper for preload
'    Await Task.Run(Sub()
'                       'Helpers.LoadAdultSvgResources()
'                       Helpers.LoadAdultSvgResources(AddressOf MainView3.SetStatusText)
'                   End Sub)

'    Await Task.Run(Sub()
'                       Helpers.LoadKidSvgResources(AddressOf MainView3.SetStatusText)
'                   End Sub)

'End Sub