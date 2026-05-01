Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data.SqlClient
Imports System.Diagnostics
Imports System.Drawing
Imports System.Globalization
Imports System.IO
Imports System.Linq
Imports System.Reflection
Imports System.Runtime.InteropServices
Imports System.Security
Imports System.Text
Imports System.Text.RegularExpressions
Imports System.Threading.Tasks
Imports System.Windows.Forms
Imports AppResources
Imports Dapper
Imports DentistXLicense
Imports DevExpress.LookAndFeel
Imports DevExpress.Utils
Imports DevExpress.Utils.Extensions
Imports DevExpress.XtraBars
Imports DevExpress.XtraBars.Docking
Imports DevExpress.XtraBars.Helpers
Imports DevExpress.XtraBars.Ribbon
Imports DevExpress.XtraBars.Ribbon.Gallery
Imports DevExpress.XtraBars.ToastNotifications
Imports DevExpress.XtraEditors
Imports DevExpress.XtraRichEdit.Model
Imports DevExpress.XtraTab
Imports Infralution.Localization

Public Class MainView3

    Private _suppressToggleSwchProgrammatic As Boolean
    Private _suppressUseHdrCheckItemPersistence As Boolean
    Private _suppressLangComboEvents As Boolean
    Private _modalSchedulerSessionDepth As Integer

    Public Sub New()
        _suppressToggleSwchProgrammatic = True
        _suppressUseHdrCheckItemPersistence = True
        Try
            InitializeComponent()
            Me.DoubleBuffered = True
            AutoAssignAccordionResources(Me.MainAccordion)
            AutoAssignImagesToAllBarItems(FluentFormDefaultManager1)
            ' DevExpress skin popup layout can differ by version/skin; FirstOrDefault() = Nothing → NRE on .Gallery.
            Dim skinPopup = TryCast(SkinDropDownButtonItem1.DropDownControl, SkinPopupControlContainer)
            Dim galleryCtl = If(skinPopup IsNot Nothing, skinPopup.Controls.OfType(Of GalleryControl)().FirstOrDefault(), Nothing)
            If galleryCtl IsNot Nothing Then
                gallery = galleryCtl.Gallery
            End If
            AppIcon = Me.Icon
            UpdateLanguageMenus()
            ToggleSwch.Checked = Not Eng
        Finally
            _suppressToggleSwchProgrammatic = False
            _suppressUseHdrCheckItemPersistence = False
        End Try
    End Sub
#Region "map controls to resources"

    ' Automatically map controls to resources
    Private Sub AutoAssignAccordionResources(accControl As DevExpress.XtraBars.Navigation.AccordionControl)
        For Each element As DevExpress.XtraBars.Navigation.AccordionControlElement In accControl.Elements
            'If element.Name = "LaboratoriesButton" Then Continue For 'LaboratoriesButton 'LabsButton LabOrdersButton LabReceiveOrderButton LabPaymentsButton
            'If element.Name = "LabOrdersButton" Then Continue For 'LaboratoriesButton 'LabsButton LabOrdersButton LabReceiveOrderButton LabPaymentsButton
            'If element.Name = "LabReceiveOrderButton" Then Continue For 'LaboratoriesButton 'LabsButton LabOrdersButton LabReceiveOrderButton LabPaymentsButton
            'If element.Name = "LabPaymentsButton" Then Continue For 'LaboratoriesButton 'LabsButton LabOrdersButton LabReceiveOrderButton LabPaymentsButton
            'If element.Name.StartsWith("Lab") Then Continue For 'PaymentsButton" Then Continue For 
            AssignImageToAccordionElement(element)
        Next
    End Sub

    Private Sub AssignImageToAccordionElement(element As DevExpress.XtraBars.Navigation.AccordionControlElement)
        ' Assign image based on element.Name or element.Text
        'Dim imgKey As String = If(Not String.IsNullOrEmpty(element.Name), element.Name, element.Text)

        'element.ImageOptions.Image = ResourceLoader.GetPngResource($"{imgKey}.png")

        ' Recursively assign images to child elements
        If element.Elements.Count > 0 Then
            For Each child As DevExpress.XtraBars.Navigation.AccordionControlElement In element.Elements
                If child.Name.Contains("Separator") Then Continue For
                If child.Name = "LendBorrowButton" Then Continue For 'bntLogInOUT 'UseGridCheckItem
                If child.Name = "LaboratoriesButton" Then Continue For
                If child.Name = "PatientsButton" Then Continue For
                If child.Name = "VendorsButton" Then Continue For
                If child.Name = "DoctorsButton" Then Continue For

                Dim imgKey As String = If(Not String.IsNullOrEmpty(child.Name), child.Name, child.Text)
                Dim img As Image = Nothing
                If ResourceLoader.GetPngResource($"{imgKey}.png") IsNot Nothing Then
                    child.ImageOptions.Image = ResourceLoader.GetPngResource($"{imgKey}.png")
                End If

                'AssignImageToAccordionElement(child)
            Next
        End If
    End Sub

    Private Sub AutoAssignImagesToBarSubItems(container As Control)
        ' Scan all fields declared on the form
        For Each field In Me.GetType().GetFields(Reflection.BindingFlags.NonPublic Or Reflection.BindingFlags.Instance)
            If TypeOf field.GetValue(Me) Is DevExpress.XtraBars.BarSubItem Then
                Dim subItem As DevExpress.XtraBars.BarSubItem = CType(field.GetValue(Me), DevExpress.XtraBars.BarSubItem)
                AssignImagesToSubItemRecursive(subItem)
            End If
        Next
    End Sub

    Private Sub AssignImagesToSubItemRecursive(subItem As DevExpress.XtraBars.BarSubItem)
        ' Assign image to the current subitem
        Dim imgKey As String = If(Not String.IsNullOrEmpty(subItem.Name), subItem.Name, subItem.Caption)
        subItem.ImageOptions.Image = ResourceLoader.GetPngResource($"{imgKey}.png")

        ' Handle nested items inside the subitem
        For Each link As DevExpress.XtraBars.BarItemLink In subItem.ItemLinks
            If TypeOf link.Item Is DevExpress.XtraBars.BarButtonItem Then
                Dim btn = CType(link.Item, DevExpress.XtraBars.BarButtonItem)
                Dim key = If(Not String.IsNullOrEmpty(btn.Name), btn.Name, btn.Caption)
                btn.ImageOptions.Image = ResourceLoader.GetPngResource($"{key}.png")

            ElseIf TypeOf link.Item Is DevExpress.XtraBars.BarSubItem Then
                ' Recursively process nested subitems
                AssignImagesToSubItemRecursive(CType(link.Item, DevExpress.XtraBars.BarSubItem))
            End If
        Next
    End Sub

    Private Sub AutoAssignImagesToAllBarItems(barManager As DevExpress.XtraBars.BarManager)
        For Each item As DevExpress.XtraBars.BarItem In barManager.Items
            If item.Name = "SkinBarSubItem1" Then Continue For 'SkinDropDownButtonItem1
            If item.Name = "BarHeaderItem1" Then Continue For
            If item.Name = "SkinDropDownButtonItem1" Then Continue For 'SkinDropDownButtonItem1
            If item.Name = "ListUserMngmntMnu" Then Continue For 'ListUserMngmntMnu
            If item.Name = "ListUsersMnu" Then Continue For 'ListUsersMnu
            If item.Name = "ListAddNewUserMnu" Then Continue For 'ListAddNewUserMnu
            If item.Name = "ListEditUserMnu" Then Continue For 'ListEditUserMnu
            If item.Name = "ListChangePassMnu" Then Continue For 'ListChangePassMnu
            If item.Name = "LstRestPassMnu" Then Continue For 'LstRestPassMnu 'bntLogInOUT
            If item.Name = "bntLogInOUT" Then Continue For 'bntLogInOUT 'UseGridCheckItem
            If item.Name = "UseGridCheckItem" Then Continue For 'bntLogInOUT 'UseGridCheckItem
            If item.Name = "LendBorrowButton" Then Continue For 'bntLogInOUT 'UseGridCheckItem
            If item.Name = "BtnDashCreate" Then Continue For 'bntLogInOUT 'UseGridCheckItem
            If item.Name = "BtnFinancePass" Then Continue For
            'If item.Name = "LaboratoriesButton" Then Continue For 'LaboratoriesButton 'LabsButton LabOrdersButton LabReceiveOrderButton LabPaymentsButton
            'If item.Name = "LabOrdersButton" Then Continue For 'LaboratoriesButton 'LabsButton LabOrdersButton LabReceiveOrderButton LabPaymentsButton
            'If item.Name = "LabReceiveOrderButton" Then Continue For 'LaboratoriesButton 'LabsButton LabOrdersButton LabReceiveOrderButton LabPaymentsButton
            'If item.Name = "LabPaymentsButton" Then Continue For 'LaboratoriesButton 'LabsButton LabOrdersButton LabReceiveOrderButton LabPaymentsButton

            AssignImageRecursive(item)
        Next
    End Sub

    Private Sub AssignImageRecursive(item As DevExpress.XtraBars.BarItem)
        If String.IsNullOrEmpty(item.Name) Then Return

        ' Assign image based on Name
        item.ImageOptions.Image = ResourceLoader.GetPngResource($"{item.Name}.png")

        ' If it's a sub-item, go deeper
        If TypeOf item Is DevExpress.XtraBars.BarSubItem Then
            Dim subItem = CType(item, DevExpress.XtraBars.BarSubItem)
            For Each link As DevExpress.XtraBars.BarItemLink In subItem.ItemLinks
                If link.Item.Name = "SkinBarSubItem1" Then Continue For
                AssignImageRecursive(link.Item)
            Next
        End If
    End Sub


#End Region

#Region "ChangeInputKeyBoard"
    Private Sub UpdateLanguageMenus()
        If Eng Then
            RadioEn.Checked = True
            RadioAr.Checked = False
            ChangeDate("en")
        Else
            RadioAr.Checked = True
            RadioEn.Checked = False
            ChangeDate("en")
        End If
    End Sub
    '====for lang input
    ' Declare the necessary Windows API functions
    <DllImport("user32.dll", SetLastError:=True)>
    Private Shared Function LoadKeyboardLayout(pwszKLID As String, flags As UInteger) As IntPtr
    End Function

    <DllImport("user32.dll")>
    Private Shared Function ActivateKeyboardLayout(hkl As IntPtr, flags As UInteger) As IntPtr
    End Function

    ' Constants for the API functions
    Private Const KLF_ACTIVATE As UInteger = &H1
    ''' <summary>Allows Windows to substitute a user-preferred layout from the registry when the requested KLID is not installed.</summary>
    Private Const KLF_SUBSTITUTE_OK As UInteger = &H2

    '==========================================

    Private Sub ChangeDate(ByVal DateLangCulture As String)

        Dim DTFormat As DateTimeFormatInfo
        DateLangCulture = DateLangCulture.ToLower()

        DTFormat = New System.Globalization.CultureInfo("ar-JO", False).DateTimeFormat

        DTFormat.Calendar = New System.Globalization.GregorianCalendar()
        '' We format the date structure to whatever we want - LAITH - 11/13/2005 1:05:39 PM -
        DTFormat.ShortDatePattern = "yyyy/MM/dd"

        'Thread.CurrentThread.CurrentUICulture = New CultureInfo("en-US")
        'Thread.CurrentThread.CurrentCulture = New CultureInfo("en-US")


    End Sub

    Private Sub ToggleSwch_CheckedChanged(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles ToggleSwch.CheckedChanged
        If _suppressToggleSwchProgrammatic Then Return

        If ToggleSwch.Checked Then
            ToggleSwch.Caption = "English"
            PrevLang = "ar"
            Eng = False
            SettingsRuntimeApply.PersistLanguageToAppConfig("ar")
            SettingsRuntimeApply.ApplyLanguageFromSettingsCode("ar")
        Else
            ToggleSwch.Caption = "عربي"
            PrevLang = "en"
            Eng = True
            SettingsRuntimeApply.PersistLanguageToAppConfig("en")
            SettingsRuntimeApply.ApplyLanguageFromSettingsCode("en")
        End If
    End Sub

    Private Sub InitializeLangCombo()
        Try
            _suppressLangComboEvents = True
            langComboItems.Items.Clear()
            Dim currentName = InputLanguage.CurrentInputLanguage?.Culture?.Name
            Dim matchedDisplay As String = Nothing
            For Each il As InputLanguage In InputLanguage.InstalledInputLanguages
                Dim display = il.Culture.DisplayName & " (" & il.Culture.Name & ")"
                langComboItems.Items.Add(display)
                If matchedDisplay Is Nothing AndAlso String.Equals(il.Culture.Name, currentName, StringComparison.OrdinalIgnoreCase) Then
                    matchedDisplay = display
                End If
            Next
            If matchedDisplay IsNot Nothing Then
                langCombo.EditValue = matchedDisplay
            ElseIf langComboItems.Items.Count > 0 Then
                langCombo.EditValue = langComboItems.Items(0).ToString()
            End If
        Finally
            _suppressLangComboEvents = False
        End Try
    End Sub
    Private Function ExtractCultureName(input As String) As String
        ' Define the regular expression pattern to match the culture name within parentheses
        Dim pattern As String = "\(([^)]+)\)$"
        Dim match As Match = Regex.Match(input, pattern)

        ' Check if the match is successful and return the captured group
        If match.Success Then
            Return match.Groups(1).Value
        Else
            Return String.Empty
        End If
    End Function
    Private Sub langCombo_EditValueChanged(sender As Object, e As EventArgs) Handles langCombo.EditValueChanged
        If _suppressLangComboEvents OrElse langCombo.EditValue Is Nothing Then Return
        Dim selectedLang = langCombo.EditValue.ToString()
        Dim cultureName = ExtractCultureName(selectedLang)
        If String.IsNullOrWhiteSpace(cultureName) Then Return

        ChangeInputLanguage(cultureName)
    End Sub
    ''' <summary>
    ''' Switches the thread input language. Prefer <see cref="InputLanguage"/> entries that are already installed on this PC;
    ''' <see cref="CultureInfo.KeyboardLayoutId"/> + <c>LoadKeyboardLayout</c> alone is fragile (e.g. Arabic 101 vs 102, regional SKUs, user substitutions).
    ''' </summary>
    Private Sub ChangeInputLanguage(cultureName As String)
        Try
            Dim culture As CultureInfo = New CultureInfo(cultureName)

            ' 1) WinForms: map culture to an installed input language (works on any machine that has that layout in Settings).
            Dim ilFromCulture = InputLanguage.FromCulture(culture)
            If ilFromCulture IsNot Nothing Then
                InputLanguage.CurrentInputLanguage = ilFromCulture
                Return
            End If

            ' 2) Exact match on culture name (e.g. user picked "Arabic (Jordan) (ar-JO)" but FromCulture failed on some builds).
            For Each il As InputLanguage In InputLanguage.InstalledInputLanguages
                If String.Equals(il.Culture.Name, culture.Name, StringComparison.OrdinalIgnoreCase) Then
                    InputLanguage.CurrentInputLanguage = il
                    Return
                End If
            Next

            ' 3) Same language: first installed layout (e.g. only ar-SA is installed but UI asked for ar-JO).
            Dim two = culture.TwoLetterISOLanguageName
            For Each il As InputLanguage In InputLanguage.InstalledInputLanguages
                If String.Equals(il.Culture.TwoLetterISOLanguageName, two, StringComparison.OrdinalIgnoreCase) Then
                    InputLanguage.CurrentInputLanguage = il
                    Return
                End If
            Next

            ' 4) Fallback: load default KLID for culture (may still fail if Windows cannot load that DLL on this edition).
            Dim inputLanguageId As String = culture.KeyboardLayoutId.ToString("X8")
            Dim hkl As IntPtr = LoadKeyboardLayout(inputLanguageId, KLF_ACTIVATE Or KLF_SUBSTITUTE_OK)
            If hkl = IntPtr.Zero Then
                Throw New Win32Exception(Marshal.GetLastWin32Error())
            End If
            ActivateKeyboardLayout(hkl, KLF_ACTIVATE)
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        End Try
    End Sub


#End Region


    Private gallery As GalleryControlGallery '= ((TryCast(SkinDropDownButtonItem1.DropDownControl, SkinPopupControlContainer)).Controls.OfType(Of GalleryControl)().FirstOrDefault()).Gallery



    'Private WithEvents ThinHDR As ThinHDRChrtNew
    'Friend WithEvents baseForm As BaseFormNew

    Private idleManager As IdleManager

    Private idleTimer As Timer
    Private lastActivityTime As DateTime
    ''' <summary>Timer to check once per hour whether a weekly backup is due.</summary>
    Private _weeklyBackupTimer As System.Windows.Forms.Timer
    ''' <summary>Timer to send WhatsApp reminders for appointments in ~24 hours.</summary>
    Private _appointmentReminderTimer As System.Windows.Forms.Timer
    ''' <summary>Timer (every 1 min) to process the short-lead appointment WhatsApp queue; lead hours from My.Settings.ShortReminder (ApptTwoHourReminderQueueRepository).</summary>
    Private _twoHourApptReminderTimer As System.Windows.Forms.Timer
    ''' <summary>Lines for the last batch of WhatsApp reminders (status label hint + click).</summary>
    Private _lastReminderDetails As String = ""
    Private _toastNotifications As ToastNotificationsManager
    Private _financeDashboardHotkeyRegistered As Boolean
    Private _treatHotkeyRegistered As Boolean
    Private _orthoHotkeyRegistered As Boolean
    Private _diagHotkeyRegistered As Boolean

    Private ReadOnly _whatsSessionStore As New List(Of WhatsAppActivityLogRow)
    Private _whatsExpandedCard As WhatsSessionNotifCard
    Private _whatsCopySource As WhatsAppActivityLogRow
    Private _btnMsgCenterReloadTodayFromSql As SimpleButton
    Private _btnMsgCenterReloadRecentFromSql As SimpleButton
    Private _whatsMsgCenterCountdownTimer As System.Windows.Forms.Timer
    Private _lastWhatsQueueEnrichUtc As DateTime
    Private _whatsQueueEnrichInFlight As Boolean
    Private _whatsMessageCenterDockShown As Boolean
    ''' <summary>Collapses expanded auto-hide dock panels when the user clicks outside them (see <see cref="ProcessDockAutoHideOutsideClickFilter"/>).</summary>
    Private _dockAutoHideOutsideClickFilter As IMessageFilter

    Private ReadOnly _currentPatientDockApptRepo As New AppointmentCRepository()
    Private ReadOnly _currentPatientSnapshotRepo As New CurrentPatientSnapshotRepository()


#Region "User32"
    ' --- Global hotkeys (RegisterHotKey; works with DevExpress Fluent where IMessageFilter does not get WM_KEY*).
    ' To add one: pick an unused HotkeyId in &H7D00–&H7DFF, set fsModifiers (0 = key alone, or HOTKEYF_ALT/CONTROL/SHIFT/WIN),
    ' add a VK_* from https://learn.microsoft.com/windows/win32/inputdev/virtual-key-codes,
    ' register in TryRegisterGlobalHotkeys, unregister in TryUnregisterGlobalHotkeys, branch in WndProc (e.g. BeginInvoke + call the same *Click handler).
    ' Note: each registration is system-wide while this app runs; plain F-keys may override the same key in other apps.

    Private Const WM_HOTKEY As Integer = &H312
    Private Const FinanceDashboardHotkeyId As Integer = &H7D00
    Private Const HotkeyIdTreat As Integer = &H7D01
    Private Const HotkeyIdOrtho As Integer = &H7D02
    Private Const HotkeyIdDiag As Integer = &H7D03
    Private Const HOTKEYF_CONTROL As UInteger = &H2UI
    Private Const HOTKEYF_SHIFT As UInteger = &H4UI
    Private Const VK_F10 As UInteger = &H79UI
    Private Const VK_F5 As UInteger = &H74UI
    Private Const VK_F6 As UInteger = &H75UI
    Private Const VK_F7 As UInteger = &H76UI

    <DllImport("user32.dll", SetLastError:=True)>
    Private Shared Function RegisterHotKey(hWnd As IntPtr, id As Integer, fsModifiers As UInteger, vk As UInteger) As Boolean
    End Function

    <DllImport("user32.dll", SetLastError:=True)>
    Private Shared Function UnregisterHotKey(hWnd As IntPtr, id As Integer) As Boolean
    End Function

    <DllImport("user32.dll")>
    Private Shared Function GetForegroundWindow() As IntPtr
    End Function

    <DllImport("user32.dll")>
    Private Shared Function GetWindowThreadProcessId(hWnd As IntPtr, ByRef lpdwProcessId As Integer) As UInteger
    End Function

    <DllImport("user32.dll")>
    Private Shared Function WindowFromPoint(ByVal pt As Point) As IntPtr
    End Function

    <DllImport("user32.dll")>
    Private Shared Function GetParent(hWnd As IntPtr) As IntPtr
    End Function

    ''' <summary>True if the foreground window belongs to this process (hotkey fires globally; we only act in-app).</summary>
    Private Shared Function IsForegroundOwnedByThisProcess() As Boolean
        Dim fg = GetForegroundWindow()
        If fg = IntPtr.Zero Then Return False
        Dim pid As Integer
        GetWindowThreadProcessId(fg, pid)
        Return pid = Process.GetCurrentProcess().Id
    End Function
#End Region

#Region "Dock auto-hide: collapse on outside click"

    Private NotInheritable Class DockAutoHideOutsideClickFilter
        Implements IMessageFilter
        Private ReadOnly _owner As MainView3
        Public Sub New(owner As MainView3)
            _owner = owner
        End Sub
        Public Function PreFilterMessage(ByRef m As Message) As Boolean Implements IMessageFilter.PreFilterMessage
            Return _owner.ProcessDockAutoHideOutsideClickFilter(m)
        End Function
    End Class

    ''' <summary>Returns false so the mouse message is never consumed. Uses <see cref="CollapseAutoHideActivePanelFromOutsideClick"/> after the default click handling would run.</summary>
    Private Function ProcessDockAutoHideOutsideClickFilter(m As Message) As Boolean
        Try
            Const WM_LBUTTONDOWN As Integer = &H201
            Const WM_RBUTTONDOWN As Integer = &H204
            Const WM_MBUTTONDOWN As Integer = &H207
            Const WM_NCLBUTTONDOWN As Integer = &HA1
            Const WM_NCRBUTTONDOWN As Integer = &HA4
            Const WM_NCMBUTTONDOWN As Integer = &HA7
            Dim msg = m.Msg
            If msg <> WM_LBUTTONDOWN AndAlso msg <> WM_RBUTTONDOWN AndAlso msg <> WM_MBUTTONDOWN AndAlso
                msg <> WM_NCLBUTTONDOWN AndAlso msg <> WM_NCRBUTTONDOWN AndAlso msg <> WM_NCMBUTTONDOWN Then
                Return False
            End If
            If Not IsHandleCreated OrElse IsDisposed OrElse DockManager1 Is Nothing Then Return False
            Dim ap = DockManager1.ActivePanel
            If ap Is Nothing OrElse ap.Visibility <> DockVisibility.AutoHide Then Return False
            Dim pt = Cursor.Position
            Dim ctrl = GetDotNetControlAtScreenPoint(pt)
            If IsUnderAutoHidePanel(ctrl, ap) Then Return False
            BeginInvoke(New Action(AddressOf CollapseAutoHideActivePanelFromOutsideClick))
        Catch
        End Try
        Return False
    End Function

    Private Shared Function GetDotNetControlAtScreenPoint(screenPt As Point) As Control
        Dim hWnd = WindowFromPoint(screenPt)
        While hWnd <> IntPtr.Zero
            Dim c = Control.FromHandle(hWnd)
            If c IsNot Nothing Then Return c
            hWnd = GetParent(hWnd)
        End While
        Return Nothing
    End Function

    Private Shared Function IsUnderAutoHidePanel(ctrl As Control, panel As DockPanel) As Boolean
        If ctrl Is Nothing OrElse panel Is Nothing Then Return False
        Dim walk As Control = ctrl
        While walk IsNot Nothing
            If Object.ReferenceEquals(walk, panel) Then Return True
            If Object.ReferenceEquals(walk, panel.ControlContainer) Then Return True
            walk = walk.Parent
        End While
        Return False
    End Function

    Private Function IsDescendantOfForm(control As Control) As Boolean
        If control Is Nothing Then Return False
        Dim walk As Control = control
        While walk IsNot Nothing
            If Object.ReferenceEquals(walk, Me) Then Return True
            walk = walk.Parent
        End While
        Return False
    End Function

    ''' <summary>Steals focus from an expanded auto-hide panel so DevExpress collapses it, without calling <see cref="DockPanel.Hide"/> (which would move panels to <see cref="DockManager.HiddenPanels"/>).</summary>
    Private Sub CollapseAutoHideActivePanelFromOutsideClick()
        Try
            If DockManager1 Is Nothing Then Return
            Dim ap = DockManager1.ActivePanel
            If ap Is Nothing OrElse ap.Visibility <> DockVisibility.AutoHide Then Return
            Dim pt = Cursor.Position
            Dim ctrl = GetDotNetControlAtScreenPoint(pt)
            If IsUnderAutoHidePanel(ctrl, ap) Then Return
            If ctrl Is Nothing OrElse Not IsDescendantOfForm(ctrl) Then
                DockManager1.ActivePanel = Nothing
                Return
            End If
            Dim focusWalk As Control = ctrl
            While focusWalk IsNot Nothing
                If focusWalk.CanFocus Then
                    focusWalk.Focus()
                    Return
                End If
                focusWalk = focusWalk.Parent
            End While
            Focus()
        Catch
        End Try
    End Sub

#End Region

#Region "Form Loading And DBase Connection"

    ''' <summary>
    ''' Do not set <see cref="DockPanel.Visibility"/> to Hidden for panels in AutoHideContainers — DevExpress v25 throws inside
    ''' <c>DockLayout.UpdateOnChangeAutoHide</c>. Hiding the WinForms <see cref="AutoHideContainer"/> controls avoids that and keeps layout valid.
    ''' <see cref="hideContainerRight"/> is left visible so <see cref="DockMessageCenter"/> is available immediately (not only after WhatsApp activity).
    ''' </summary>
    Private Sub ApplyShellDockPanelsStartHidden()
        Try
            If hideContainerLeft IsNot Nothing Then hideContainerLeft.Visible = False
            If hideContainerTop IsNot Nothing Then hideContainerTop.Visible = False
            If hideContainerBottom IsNot Nothing Then hideContainerBottom.Visible = False
        Catch
        End Try
    End Sub

    Private Sub MainView3_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ApplyShellDockPanelsStartHidden()
        AddHandler PatientEventManager.PatientChanged, AddressOf HandlePatientChanged
        If gallery IsNot Nothing Then
            AddHandler gallery.ItemClick, AddressOf Gallery_ItemClick
        End If

        'idleManager = New IdleManager(10) ' 10 minutes idle time
        'AddHandler idleManager.IdleDetected, AddressOf OnIdleDetected
        'idleManager.Start()

        'btnSettings.Enabled = Perms.CanDo("Admins.ALl")
        NewSplash.ShowDialog(Me)
        'Splash1.ShowDialog(Me)
        ' Ensure database connection before any database operations
        EnsureDatabaseConnection()


        ' Licensing and trial validation handled by CursorLicManager

        '=================================

        'Dim reader As New LicenseReader(SAMPLE_KEYS.DevPublicKeyXml)
        'Dim license = reader.Read("DentistX.lic")

        'If DateTime.UtcNow > license.ExpiresOn Then
        '    Throw New SecurityException("License expired.")
        'End If

        'Dim comparer As New FingerprintComparer()
        'Dim result = comparer.Compare(license.Fingerprint, GetCurrentFingerprint())

        'If Not result.IsMatch Then
        '    Throw New SecurityException("License mismatch: " &
        'String.Join(", ", result.Mismatches))
        'End If




        BackClr = Me.ContainerA.BackColor

        ' Sync header choice: Checked = "Use New Header Style" (Navigator), Unchecked = "Use Old Header Style" (Navigator2 on workspace; HdrTestMod retired)
        BasePatientWorkspace.UseHdrTestModHeader = My.Settings.UseHdrTestModHeader
        _suppressUseHdrCheckItemPersistence = True
        Try
            UseHdrCheckItem.Checked = Not BasePatientWorkspace.UseHdrTestModHeader
        Finally
            _suppressUseHdrCheckItemPersistence = False
        End Try
        UpdateHeaderStyleCaption()

        ApplyShortReminderSpinFromSettings()

        ' Start weekly backup timer: check periodically (every hour) if a weekly backup is due
        _weeklyBackupTimer = New System.Windows.Forms.Timer With {.Interval = 60 * 60 * 1000}
        AddHandler _weeklyBackupTimer.Tick, AddressOf WeeklyBackupTimer_Tick
        TryStartMainTimer(_weeklyBackupTimer, NameOf(_weeklyBackupTimer))
        ' Also perform an immediate check at startup so backups are not delayed until the first timer tick
        WeeklyBackupTimer_Tick(Nothing, EventArgs.Empty)

        ' Start appointment reminder timer (check every hour, send reminders for appts in ~24h)
        _appointmentReminderTimer = New System.Windows.Forms.Timer With {.Interval = 60 * 60 * 1000}
        AddHandler _appointmentReminderTimer.Tick, AddressOf AppointmentReminderTimer_Tick
        TryStartMainTimer(_appointmentReminderTimer, NameOf(_appointmentReminderTimer))
        ' Run first check after 1 minute (so first run doesn't wait a full hour)
        Task.Run(Async Function()
                     Await Task.Delay(60 * 1000)
                     If _appointmentReminderTimer IsNot Nothing AndAlso IsHandleCreated Then
                         BeginInvoke(New Action(Sub() AppointmentReminderTimer_Tick(Nothing, EventArgs.Empty)))
                     End If
                 End Function)

        ApptWhatsAppReminderBackgroundPoller.EnsureStarted()
        SchedulerSnapshotAutoSendService.EnsureStarted()

        ' Short reminder send time = appointment start minus My.Settings.ShortReminder hours (sync on save + this poll).
        _twoHourApptReminderTimer = New System.Windows.Forms.Timer With {.Interval = 60 * 1000}
        AddHandler _twoHourApptReminderTimer.Tick, AddressOf TwoHourApptReminderTimer_Tick
        TryStartMainTimer(_twoHourApptReminderTimer, NameOf(_twoHourApptReminderTimer))
        Task.Run(Async Function()
                     Await Task.Delay(15 * 1000)
                     If _twoHourApptReminderTimer IsNot Nothing AndAlso IsHandleCreated Then
                         BeginInvoke(New Action(Sub() TwoHourApptReminderTimer_Tick(Nothing, EventArgs.Empty)))
                     End If
                 End Function)

        _toastNotifications = New ToastNotificationsManager()
        WhatsAppToastHost.Register(Me, _toastNotifications)
        InitializeWhatsAppMessageCenter()
        AddHandler WhatsAppSessionMessageCenter.MessageProcessed, AddressOf OnWhatsAppSessionMessageProcessed
        InitializeCurrentPatientDock()
        _dockAutoHideOutsideClickFilter = New DockAutoHideOutsideClickFilter(Me)
        Application.AddMessageFilter(_dockAutoHideOutsideClickFilter)
        ' chkBackup = session override only: checked → interactive Backup() on exit; unchecked → respect My.Settings.BackupOnExit for silent backup.
    End Sub

    Private _didInitializeLangCombo As Boolean

    Private Sub MainView3_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        If _didInitializeLangCombo Then Return
        _didInitializeLangCombo = True
        InitializeLangCombo()
    End Sub

    ''' <summary>Re-applies .resx strings and code-driven captions after <see cref="Module1.Eng"/> / UI culture changes (settings, no restart).</summary>
    Friend Sub ApplyRuntimeUiLanguageAfterCultureChange(culture As CultureInfo)
        'RuntimeUiLanguage.ApplyFormControlTreeResources(Me, culture)
        'If ContainerA IsNot Nothing Then RuntimeUiLanguage.ApplyShellLayoutDirectionFromEng(ContainerA)
        'If MainAccordion IsNot Nothing Then RuntimeUiLanguage.ApplyShellLayoutDirectionFromEng(MainAccordion)
        'If FluentDesignFormControl1 IsNot Nothing Then RuntimeUiLanguage.ApplyShellLayoutDirectionFromEng(FluentDesignFormControl1)
        'UpdateHeaderStyleCaption()
        'ApplyCurrentPatientDockLocalizedTexts()
        'RefreshCurrentPatientDockPanel()
        'RuntimeUiLanguage.RefreshSchedulerUserControlsUnder(Me)
    End Sub

    Protected Overrides Sub OnHandleCreated(e As EventArgs)
        MyBase.OnHandleCreated(e)
        TryRegisterGlobalHotkeys()
    End Sub

    Protected Overrides Sub OnHandleDestroyed(e As EventArgs)
        TryUnregisterGlobalHotkeys()
        MyBase.OnHandleDestroyed(e)
    End Sub

    Private Sub TryRegisterGlobalHotkeys()
        If Not IsHandleCreated OrElse Handle = IntPtr.Zero Then Return
        If Not _financeDashboardHotkeyRegistered Then
            If RegisterHotKey(Handle, FinanceDashboardHotkeyId, HOTKEYF_CONTROL Or HOTKEYF_SHIFT, VK_F10) Then
                _financeDashboardHotkeyRegistered = True
            End If
        End If
        If Not _treatHotkeyRegistered Then
            If RegisterHotKey(Handle, HotkeyIdTreat, 0UI, VK_F5) Then _treatHotkeyRegistered = True
        End If
        If Not _orthoHotkeyRegistered Then
            If RegisterHotKey(Handle, HotkeyIdOrtho, 0UI, VK_F6) Then _orthoHotkeyRegistered = True
        End If
        If Not _diagHotkeyRegistered Then
            If RegisterHotKey(Handle, HotkeyIdDiag, 0UI, VK_F7) Then _diagHotkeyRegistered = True
        End If
    End Sub

    Private Sub TryUnregisterGlobalHotkeys()
        If IsHandleCreated AndAlso Handle <> IntPtr.Zero Then
            If _financeDashboardHotkeyRegistered Then UnregisterHotKey(Handle, FinanceDashboardHotkeyId)
            If _treatHotkeyRegistered Then UnregisterHotKey(Handle, HotkeyIdTreat)
            If _orthoHotkeyRegistered Then UnregisterHotKey(Handle, HotkeyIdOrtho)
            If _diagHotkeyRegistered Then UnregisterHotKey(Handle, HotkeyIdDiag)
        End If
        _financeDashboardHotkeyRegistered = False
        _treatHotkeyRegistered = False
        _orthoHotkeyRegistered = False
        _diagHotkeyRegistered = False
    End Sub

    Protected Overrides Sub WndProc(ByRef m As Message)
        If m.Msg = WM_HOTKEY Then
            Dim hid = m.WParam.ToInt32()
            If hid = FinanceDashboardHotkeyId OrElse hid = HotkeyIdTreat OrElse hid = HotkeyIdOrtho OrElse hid = HotkeyIdDiag Then
                If IsForegroundOwnedByThisProcess() AndAlso Not IsDisposed AndAlso IsHandleCreated Then
                    Select Case hid
                        Case FinanceDashboardHotkeyId
                            BeginInvoke(New Action(AddressOf OpenFinanceDashboardForm))
                        Case HotkeyIdTreat
                            BeginInvoke(Sub() TreatsButton_Click(TreatsButton, EventArgs.Empty))
                        Case HotkeyIdOrtho
                            BeginInvoke(Sub() OrthoButton_Click(OrthoButton, EventArgs.Empty))
                        Case HotkeyIdDiag
                            BeginInvoke(Sub() MobileButton_Click(DiagButton, EventArgs.Empty))
                    End Select
                End If
                Return
            End If
        End If
        MyBase.WndProc(m)
    End Sub

    'Private Sub MainView3_Shown(sender As Object, e As EventArgs) Handles Me.Shown
    '    TodayApptEditorForm.ShowDialog(Me)

    'End Sub


    Private Async Sub TwoHourApptReminderTimer_Tick(sender As Object, e As EventArgs)
        Try
            If _twoHourApptReminderTimer IsNot Nothing Then _twoHourApptReminderTimer.Stop()
            Await AppointmentTwoHourReminderService.RunAsync()
        Catch
        Finally
            If _twoHourApptReminderTimer IsNot Nothing AndAlso Not _twoHourApptReminderTimer.Enabled Then
                TryStartMainTimer(_twoHourApptReminderTimer, NameOf(_twoHourApptReminderTimer))
            End If
        End Try
    End Sub
    Private Async Sub AppointmentReminderTimer_Tick(sender As Object, e As EventArgs)
        Try
            If _appointmentReminderTimer IsNot Nothing Then _appointmentReminderTimer.Stop()
            Dim apptRes = Await AppointmentReminderService.RunAsync()
            Dim acctRes = Await AccountReminderScheduleService.RunAsync()
            Dim sentAppt = apptRes.SentCount
            Dim sentAcct = acctRes.SentCount
            If (sentAppt + sentAcct) > 0 AndAlso stLoadingLbl IsNot Nothing AndAlso IsHandleCreated Then
                Dim parts As New List(Of String)
                If sentAppt > 0 Then parts.Add($"{sentAppt} appointment reminder(s)")
                If sentAcct > 0 Then parts.Add($"{sentAcct} account reminder(s)")
                Dim lines As New List(Of String)
                lines.AddRange(apptRes.Lines)
                lines.AddRange(acctRes.Lines)
                _lastReminderDetails = String.Join(Environment.NewLine, lines)
                Dim caption = "Sent via WhatsApp: " & String.Join(", ", parts)
                Dim hintIntro = If(Eng, "Click for details:", "انقر للتفاصيل:")
                Dim hintText = If(String.IsNullOrEmpty(_lastReminderDetails), caption, hintIntro & Environment.NewLine & _lastReminderDetails)
                BeginInvoke(New Action(Sub()
                                           stLoadingLbl.Caption = caption
                                           stLoadingLbl.Hint = hintText
                                       End Sub))
            End If
        Catch
            ' Silently ignore; will retry next hour
        Finally
            If _appointmentReminderTimer IsNot Nothing AndAlso Not _appointmentReminderTimer.Enabled Then
                TryStartMainTimer(_appointmentReminderTimer, NameOf(_appointmentReminderTimer))
            End If
        End Try
    End Sub

    Private Sub BarManager1_ItemClick(sender As Object, e As ItemClickEventArgs) Handles BarManager1.ItemClick
        If e.Item IsNot stLoadingLbl Then Return
        TryShowReminderDetailsFromStatusLabel()
    End Sub

    Private Sub TryShowReminderDetailsFromStatusLabel()
        If String.IsNullOrWhiteSpace(_lastReminderDetails) Then Return
        If stLoadingLbl Is Nothing OrElse stLoadingLbl.Caption Is Nothing OrElse Not stLoadingLbl.Caption.StartsWith("Sent via WhatsApp", StringComparison.OrdinalIgnoreCase) Then Return
        Dim title = If(Eng, "WhatsApp reminders sent", "تذكيرات واتساب مرسلة")
        XtraMessageBox.Show(_lastReminderDetails, title, MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub
    Private Sub WeeklyBackupTimer_Tick(sender As Object, e As EventArgs)
        Try
            DriveScheduledBackup.TryRunIfDue(Me)
            Dim lastStr As String = If(My.Settings.LastWeeklyBackupDate, "").Trim()
            Dim lastDate As DateTime
            Dim shouldRun As Boolean = False
            If String.IsNullOrEmpty(lastStr) Then
                shouldRun = True
            ElseIf DateTime.TryParse(lastStr, lastDate) Then
                If (DateTime.Today - lastDate.Date).TotalDays >= 7 Then
                    shouldRun = True
                End If
            Else
                shouldRun = True
            End If
            If shouldRun Then
                _weeklyBackupTimer.Stop()
                TrySilentBackupToDefaultFolder(New CancelEventArgs, Me)
                My.Settings.LastWeeklyBackupDate = DateTime.Today.ToString("yyyy-MM-dd")
                My.Settings.Save()
                TryStartMainTimer(_weeklyBackupTimer, NameOf(_weeklyBackupTimer))
            End If
        Catch ex As Exception
            ' Avoid breaking the app; timer will retry next hour
            If _weeklyBackupTimer IsNot Nothing AndAlso _weeklyBackupTimer.Enabled = False Then
                TryStartMainTimer(_weeklyBackupTimer, NameOf(_weeklyBackupTimer))
            End If
        End Try
    End Sub

    Private Sub TryStartMainTimer(timer As System.Windows.Forms.Timer, timerName As String)
        If timer Is Nothing Then Return
        Try
            timer.Start()
        Catch ex As System.ComponentModel.Win32Exception
            ApptErrorHelper.Report(ex, $"MainView3.TryStartMainTimer.{timerName}", showUser:=False)
        Catch ex As Exception
            ApptErrorHelper.Report(ex, $"MainView3.TryStartMainTimer.{timerName}", showUser:=False)
        End Try
    End Sub
    Private Sub EnsureDatabaseConnection()
        While True
            Try
                If TestDatabaseConnection() Then
                    If Not LicenseStartupGate.RunOnceAfterDatabaseConnected() Then
                        Me.Close()
                        Exit While
                    End If
                    If CursorLicManager.IsLimitedMode Then
                        ApplyLimitedModeUi()
                    End If
                    ShowLoginForm()
                    Exit While
                Else
                    Throw New Exception("Database connection test failed")
                End If

            Catch ex As Exception
                Dim result = MessageBox.Show($"Cannot connect to database: {ex.Message}" &
                                       Environment.NewLine &
                                       "Would you like to configure database connection?",
                                       "Database Connection Error",
                                       MessageBoxButtons.YesNo, MessageBoxIcon.Error)

                If result = DialogResult.Yes Then
                    ShowConnectionConfigurationForm()
                Else
                    Me.Close()
                    Exit While
                End If
            End Try
        End While
    End Sub
    Private Function TestDatabaseConnection() As Boolean
        Try
            Using connection As New SqlConnection(DentistXDATA.GetConnection.ConnectionString) '(GetConnectionString())
                connection.Open()
                Return connection.State = ConnectionState.Open
            End Using
        Catch
            Return False
        End Try
    End Function
    Private Sub ShowConnectionConfigurationForm()
        Using configForm As New FrmChooseConn()
            configForm.Icon = AppIcon
            If configForm.ShowDialog(Me) <> DialogResult.OK Then
                MsgBox("User canceled connection configuration")
                Me.Close()
            End If
        End Using
    End Sub
    Private Sub ShowLoginForm()
        FrmLogin.Icon = AppIcon
        Dim result = FrmLogin.ShowDialog(Me)

        If result = DialogResult.OK Then
            ApplyFormAccessShell()
            'If CurrentDoctor IsNot Nothing Then
            '    stUserNameTxt.Caption = CurrentUser.UsName & " == " & CurrentDoctor.DrName
            'Else
            '    stUserNameTxt.Caption = CurrentUser.UsName & " == No Doctor"
            'End If

        Else
            Me.Close()
        End If
    End Sub

    ''' <summary>
    ''' Runs before shell teardown: closes any other top-level forms (e.g. modeless windows), then disposes
    ''' <see cref="FormManager"/> hosted <see cref="BasePatientWorkspace"/> and embedded user controls so they are not left running after the main form closes.
    ''' </summary>
    Private Sub CloseAuxiliaryOpenFormsAndHostedWorkspace()
        Try
            Dim others As New List(Of Form)
            For Each f As Form In Application.OpenForms
                If f IsNot Me AndAlso Not f.IsDisposed Then others.Add(f)
            Next
            For Each f In others
                Try
                    f.Close()
                Catch
                End Try
            Next
        Catch
        End Try
        Try
            If FormManager.Instance.IsBasePatientFormOpen Then
                FormManager.Instance.CloseBaseForm()
            End If
            If ContainerA IsNot Nothing Then ContainerA.Controls.Clear()
        Catch
        End Try
    End Sub

    Private Sub MainView3_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        CloseAuxiliaryOpenFormsAndHostedWorkspace()
        ' BackupOnExit (settings) → silent backup when closing. chkBackup checked → override: always interactive Backup(), ignores BackupOnExit for mode.
        If chkBackup IsNot Nothing AndAlso chkBackup.Checked Then
            Backup(New CancelEventArgs, Me)
        ElseIf My.Settings.BackupOnExit Then
            TrySilentBackupToDefaultFolder(New CancelEventArgs, Me)
        End If

        RemoveHandler WhatsAppSessionMessageCenter.MessageProcessed, AddressOf OnWhatsAppSessionMessageProcessed
        If _dockAutoHideOutsideClickFilter IsNot Nothing Then
            Application.RemoveMessageFilter(_dockAutoHideOutsideClickFilter)
            _dockAutoHideOutsideClickFilter = Nothing
        End If
        TryUnregisterGlobalHotkeys()
        WhatsAppToastHost.Unregister(Me)
        If _toastNotifications IsNot Nothing Then
            _toastNotifications.Dispose()
            _toastNotifications = Nothing
        End If
        RemoveHandler PatientEventManager.PatientChanged, AddressOf HandlePatientChanged
        If DockManager1 IsNot Nothing Then
            RemoveHandler DockManager1.ActivePanelChanged, AddressOf DockManager1_ActivePanelChanged
        End If
        If DockCurrentPatient IsNot Nothing Then
            RemoveHandler DockCurrentPatient.VisibilityChanged, AddressOf DockCurrentPatient_VisibilityChanged
        End If
        If _weeklyBackupTimer IsNot Nothing Then
            _weeklyBackupTimer.Stop()
            RemoveHandler _weeklyBackupTimer.Tick, AddressOf WeeklyBackupTimer_Tick
            _weeklyBackupTimer.Dispose()
            _weeklyBackupTimer = Nothing
        End If
        If _appointmentReminderTimer IsNot Nothing Then
            _appointmentReminderTimer.Stop()
            RemoveHandler _appointmentReminderTimer.Tick, AddressOf AppointmentReminderTimer_Tick
            _appointmentReminderTimer.Dispose()
            _appointmentReminderTimer = Nothing
        End If
        If _twoHourApptReminderTimer IsNot Nothing Then
            _twoHourApptReminderTimer.Stop()
            RemoveHandler _twoHourApptReminderTimer.Tick, AddressOf TwoHourApptReminderTimer_Tick
            _twoHourApptReminderTimer.Dispose()
            _twoHourApptReminderTimer = Nothing
        End If
        If _whatsMsgCenterCountdownTimer IsNot Nothing Then
            _whatsMsgCenterCountdownTimer.Stop()
            RemoveHandler _whatsMsgCenterCountdownTimer.Tick, AddressOf WhatsMsgCenterCountdownTimer_Tick
            _whatsMsgCenterCountdownTimer.Dispose()
            _whatsMsgCenterCountdownTimer = Nothing
        End If

    End Sub

    Private Sub UserActivity(sender As Object, e As EventArgs)
        lastActivityTime = DateTime.Now
    End Sub

    Private Sub IdleTimer_Tick(sender As Object, e As EventArgs)
        If CurrentUser IsNot Nothing Then
            If (DateTime.Now - lastActivityTime).TotalMinutes >= 10 Then
                idleTimer.Stop()

                Dim lockScreen As New FrmLockScreen With {
                .CurrentUser = CurrentUser
            }

                If lockScreen.ShowDialog() = DialogResult.OK Then
                    lastActivityTime = DateTime.Now
                    idleTimer.Start()
                Else
                    Me.Close()
                End If
            End If
        End If

    End Sub
#End Region




#Region "Current patient dock (top auto-hide)"

    Private Sub InitializeCurrentPatientDock()
        If panelCurrentPatientHost Is Nothing Then Return
        AddHandler panelCurrentPatientHost.SizeChanged, AddressOf PanelCurrentPatientHost_SizeChanged
        AddHandler btnCpCopyPhone.Click, AddressOf BtnCpCopyPhone_Click
        AddHandler btnCpFocusWorkspace.Click, AddressOf BtnCpFocusWorkspace_Click
        AddHandler btnCpRefresh.Click, AddressOf BtnCpRefresh_Click
        AddHandler hlCpAppts.HyperlinkClick, AddressOf CpStatHyperlink_Click
        AddHandler hlCpTrtCount.HyperlinkClick, AddressOf CpStatHyperlink_Click
        AddHandler hlCpTrtSum.HyperlinkClick, AddressOf CpStatHyperlink_Click
        AddHandler hlCpPaySum.HyperlinkClick, AddressOf CpStatHyperlink_Click
        AddHandler hlCpOrthoFlag.HyperlinkClick, AddressOf CpStatHyperlink_Click
        AddHandler hlCpDiagFlag.HyperlinkClick, AddressOf CpStatHyperlink_Click
        AddHandler hlCpImgBefore.HyperlinkClick, AddressOf CpStatHyperlink_Click
        AddHandler hlCpImgDuring.HyperlinkClick, AddressOf CpStatHyperlink_Click
        AddHandler hlCpImgAfter.HyperlinkClick, AddressOf CpStatHyperlink_Click
        AddHandler hlCpLabs.HyperlinkClick, AddressOf CpStatHyperlink_Click
        If DockManager1 IsNot Nothing Then
            AddHandler DockManager1.ActivePanelChanged, AddressOf DockManager1_ActivePanelChanged
        End If
        If DockCurrentPatient IsNot Nothing Then
            AddHandler DockCurrentPatient.VisibilityChanged, AddressOf DockCurrentPatient_VisibilityChanged
        End If
        ApplyCurrentPatientDockLocalizedTexts()
        RefreshCurrentPatientDockPanel()
    End Sub

    Private Sub DockManager1_ActivePanelChanged(sender As Object, e As ActivePanelChangedEventArgs)
        Try
            If DockCurrentPatient Is Nothing OrElse e.Panel IsNot DockCurrentPatient Then Return
            RefreshCurrentPatientDockPanel()
        Catch
        End Try
    End Sub

    Private Sub DockCurrentPatient_VisibilityChanged(sender As Object, e As DevExpress.XtraBars.Docking.DockPanelEventArgs)
        Try
            If DockCurrentPatient Is Nothing Then Return
            If DockCurrentPatient.Visibility = DockVisibility.Visible Then
                RefreshCurrentPatientDockPanel()
            End If
        Catch
        End Try
    End Sub

    Private Sub PanelCurrentPatientHost_SizeChanged(sender As Object, e As EventArgs)
        Dim w = panelCurrentPatientHost.ClientSize.Width - panelCurrentPatientHost.Padding.Horizontal
        If w < 40 Then Return
        Dim patientInnerW = If(flowCpGrpPatientInfo IsNot Nothing AndAlso flowCpGrpPatientInfo.Visible, flowCpGrpPatientInfo.ClientSize.Width - 8, 200)
        patientInnerW = Math.Max(120, patientInnerW)
        SetCpLabelWrapWidth(lblCpName, patientInnerW)
        SetCpLabelWrapWidth(lblCpMeta, patientInnerW)
        SetCpLabelWrapWidth(lblCpPhone, patientInnerW)
        SetCpLabelWrapWidth(lblCpWhats, patientInnerW)
        SetCpLabelWrapWidth(lblCpWhatsNumber, patientInnerW)
        SetCpLabelWrapWidth(lblCpNextAppt, patientInnerW)
        SetCpLabelWrapWidth(lblCpHealth, patientInnerW)
        Dim apW = If(flowCpGrpAppts IsNot Nothing AndAlso flowCpGrpAppts.Visible, flowCpGrpAppts.ClientSize.Width - 8, patientInnerW)
        SetCpLabelWrapWidth(lblCpApptTotal, apW)
        SetCpLabelWrapWidth(lblCpApptFirst, apW)
        SetCpLabelWrapWidth(lblCpApptLast, apW)
        Dim orthoW = If(flowCpGrpOrtho IsNot Nothing AndAlso flowCpGrpOrtho.Visible, flowCpGrpOrtho.ClientSize.Width - 8, patientInnerW)
        SetCpLabelWrapWidth(lblCpOrthoStart, orthoW)
        SetCpLabelWrapWidth(lblCpOrthoLast, orthoW)
        Dim trtW = If(flowCpGrpTreats IsNot Nothing AndAlso flowCpGrpTreats.Visible, flowCpGrpTreats.ClientSize.Width - 8, patientInnerW)
        SetCpLabelWrapWidth(lblCpTrtFirst, trtW)
        SetCpLabelWrapWidth(lblCpTrtLast, trtW)
        SetCpLabelWrapWidth(lblCpBalance, trtW)
        Dim dgW = If(flowCpGrpDiag IsNot Nothing AndAlso flowCpGrpDiag.Visible, flowCpGrpDiag.ClientSize.Width - 8, patientInnerW)
        SetCpLabelWrapWidth(lblCpDiagCount, dgW)
        SetCpLabelWrapWidth(lblCpDiagFirst, dgW)
        SetCpLabelWrapWidth(lblCpDiagLast, dgW)
        SetCpLabelWrapWidth(lblCpDiagAgreements, dgW)
        SetCpLabelWrapWidth(lblCpDiagDetFirst, dgW)
        SetCpLabelWrapWidth(lblCpDiagDetLast, dgW)
        If lblCurrentPatientEmpty IsNot Nothing Then
            lblCurrentPatientEmpty.MaximumSize = New Size(w, 0)
        End If
    End Sub

    Private Shared Sub SetCpLabelWrapWidth(lbl As LabelControl, width As Integer)
        If lbl Is Nothing Then Return
        lbl.MaximumSize = New Size(width, 0)
    End Sub

    Private Shared Function FormatCpDockDate(dt As DateTime?) As String
        If Not dt.HasValue Then Return "—"
        Return dt.Value.ToString("d", CultureInfo.CurrentCulture)
    End Function

    Private Shared Function TruncateCpLabelText(s As String, maxLen As Integer) As String
        If String.IsNullOrWhiteSpace(s) Then Return ""
        s = s.Trim()
        If s.Length <= maxLen Then Return s
        Return s.Substring(0, Math.Max(0, maxLen - 1)) & "…"
    End Function

    ''' <summary>Country/isd prefix plus local WhatsApp digits, with leading zeros removed from the local part.</summary>
    Private Shared Function FormatCpWhatsDialNumber(prefix As String, whatsLocal As String) As String
        Dim p = If(prefix, "").Trim()
        Dim w = If(whatsLocal, "").Trim().TrimStart("0"c)
        If String.IsNullOrEmpty(p) AndAlso String.IsNullOrEmpty(w) Then Return ""
        If String.IsNullOrEmpty(p) Then Return w
        If String.IsNullOrEmpty(w) Then Return p
        Return p & w
    End Function

    Private Sub ApplyCurrentPatientDockLocalizedTexts()
        If DockCurrentPatient Is Nothing Then Return
        Dim dockCaption = If(Eng, "Current patient", "المريض الحالي")
        DockCurrentPatient.Text = dockCaption
        If hideContainerTop IsNot Nothing Then hideContainerTop.Text = dockCaption

        If lblCurrentPatientEmpty IsNot Nothing Then
            lblCurrentPatientEmpty.Text = If(Eng,
                "No patient workspace is open. Open a patient from the navigator to see a snapshot here.",
                "لا توجد مساحة مريض مفتوحة. افتح مريضاً من مستكشف المرضى لعرض ملخص هنا.")
            lblCurrentPatientEmpty.RightToLeft = If(Eng, RightToLeft.No, RightToLeft.Yes)
        End If

        If btnCpCopyPhone IsNot Nothing Then
            btnCpCopyPhone.Text = If(Eng, "Copy WhatsApp #", "نسخ رقم واتساب")
            btnCpCopyPhone.ToolTip = If(Eng, "Copy the combined WhatsApp number (country prefix + number, no leading zero on local part) to the clipboard.", "نسخ رقم الواتساب الكامل (مفتاح الدولة + الرقم بدون الصفر الأول) إلى الحافظة.")
        End If
        If btnCpFocusWorkspace IsNot Nothing Then
            btnCpFocusWorkspace.Text = If(Eng, "Show workspace", "عرض مساحة المريض")
            btnCpFocusWorkspace.ToolTip = If(Eng, "Bring the patient workspace to the front or open it.", "إظهار مساحة المريض أو فتحها.")
        End If
        If btnCpRefresh IsNot Nothing Then
            btnCpRefresh.Text = If(Eng, "Refresh", "تحديث")
            btnCpRefresh.ToolTip = If(Eng, "Reload patient data from the database and refresh the balance and this panel.", "إعادة تحميل بيانات المريض من قاعدة البيانات وتحديث الرصيد وهذه اللوحة.")
        End If

        If flowCpActions IsNot Nothing Then
            flowCpActions.RightToLeft = If(Eng, RightToLeft.No, RightToLeft.Yes)
            flowCpActions.FlowDirection = If(Eng, FlowDirection.LeftToRight, FlowDirection.RightToLeft)
        End If
        Dim rtlStat = If(Eng, RightToLeft.No, RightToLeft.Yes)
        Dim cpGrpFlows = {flowCpGrpLabs, flowCpGrpImages, flowCpGrpDiag, flowCpGrpOrtho, flowCpGrpTreats, flowCpGrpAppts, flowCpGrpPatientInfo}
        For Each f In cpGrpFlows
            If f Is Nothing Then Continue For
            f.RightToLeft = rtlStat
            f.FlowDirection = FlowDirection.TopDown
        Next
        If hlCpTrtSum IsNot Nothing Then
            hlCpTrtSum.ToolTip = If(Eng, "Sum of treatment values", "مجموع قيم العلاجات")
        End If
        If hlCpAppts IsNot Nothing Then
            hlCpAppts.Text = If(Eng, "Open visits →", "فتح الزيارات ←")
            hlCpAppts.ToolTip = If(Eng, "Open visits / schedule", "فتح الزيارات / الجدول")
        End If
        If hlCpTrtCount IsNot Nothing Then hlCpTrtCount.ToolTip = If(Eng, "Open treatments", "فتح العلاجات")
        If hlCpPaySum IsNot Nothing Then hlCpPaySum.ToolTip = If(Eng, "Open accounting", "فتح المحاسبة")
        If hlCpOrthoFlag IsNot Nothing Then hlCpOrthoFlag.ToolTip = If(Eng, "Open orthodontics", "فتح التقويم")
        If hlCpDiagFlag IsNot Nothing Then hlCpDiagFlag.ToolTip = If(Eng, "Open diagnostics", "فتح التشخيص")
        If hlCpImgBefore IsNot Nothing Then hlCpImgBefore.ToolTip = If(Eng, "Open images", "فتح الصور")
        If hlCpImgDuring IsNot Nothing Then hlCpImgDuring.ToolTip = If(Eng, "Open images", "فتح الصور")
        If hlCpImgAfter IsNot Nothing Then hlCpImgAfter.ToolTip = If(Eng, "Open images", "فتح الصور")
        If hlCpLabs IsNot Nothing Then hlCpLabs.ToolTip = If(Eng, "Open lab orders", "فتح طلبات المخبر")
        If panelCurrentPatientBody IsNot Nothing Then
            panelCurrentPatientBody.RightToLeft = If(Eng, RightToLeft.No, RightToLeft.Yes)
        End If
        If pnlCpPatientTable IsNot Nothing Then
            pnlCpPatientTable.RightToLeft = If(Eng, RightToLeft.No, RightToLeft.Yes)
        End If
    End Sub

    Private Sub RefreshCurrentPatientDockPanel(Optional reloadFromDb As Boolean = False, Optional patientOverride As Patient = Nothing)
        If panelCurrentPatientHost Is Nothing Then Return
        Dim apply =
            Sub()
                Try
                    Dim p = patientOverride
                    If p Is Nothing Then p = FormManager.Instance.GetCurrentPatient()
                    If reloadFromDb AndAlso p IsNot Nothing AndAlso p.PatientID > 0 Then
                        Using data As New PatientDATA()
                            Dim fresh = data.Select_RecordByID(p.PatientID)
                            If fresh IsNot Nothing Then
                                fresh.RefreshComputedProperties()
                                Dim ws = FormManager.Instance.CurrentForm
                                If ws IsNot Nothing AndAlso Not ws.IsDisposed Then
                                    ws.Current_Patient = fresh
                                End If
                            End If
                        End Using
                        p = FormManager.Instance.GetCurrentPatient()
                    End If

                    If p Is Nothing OrElse p.PatientID <= 0 Then
                        HideCurrentPatientDockShell()
                        lblCurrentPatientEmpty.Visible = True
                        panelCurrentPatientBody.Visible = False
                        btnCpCopyPhone.Enabled = False
                        btnCpRefresh.Enabled = False
                        Return
                    End If

                    EnsureCurrentPatientDockVisible()
                    lblCurrentPatientEmpty.Visible = False
                    panelCurrentPatientBody.Visible = True
                    'Dim waForCopy = FormatCpWhatsDialNumber(p.WhatsAppPrefix, If(p.WhatsApp, "").Trim())
                    Dim waForCopy = BuildInternationalWhatsDigits(If(p.WhatsApp, "").Trim(), p.WhatsAppPrefix)

                    btnCpCopyPhone.Enabled = Not String.IsNullOrWhiteSpace(waForCopy)
                    btnCpRefresh.Enabled = True

                    lblCpName.Text = If(String.IsNullOrWhiteSpace(p.PatientName), "—", p.PatientName.Trim())

                    Dim sex = If(String.IsNullOrWhiteSpace(p.Sex), "—", p.Sex.Trim())
                    Dim agePart = If(p.Age.GetValueOrDefault() > 0,
                        If(Eng, $"Age {p.Age}", $"العمر {p.Age}"),
                        If(Eng, "Age —", "العمر —"))
                    Dim num = If(String.IsNullOrWhiteSpace(p.PatientNumber), "—", p.PatientNumber.Trim())
                    lblCpMeta.Text = If(Eng,
                        $"#{num} · {sex} · {agePart}",
                        $"{agePart} · {sex} · #{num}")

                    Dim stats = _currentPatientSnapshotRepo.GetStats(p.PatientID, p)
                    Dim dock = _currentPatientSnapshotRepo.GetDockDetails(p.PatientID)
                    Dim curTyp = If(Eng, CurrencyType.ILS_En, CurrencyType.ILS_Ar)
                    Dim trtSumStr = stats.TreatmentSum.ToCurrencyString(curTyp)
                    Dim paySumStr = stats.PaymentsSum.ToCurrencyString(curTyp)
                    lblCpApptTotal.Text = If(Eng, $"Total appointments: {stats.ApptCount}", $"إجمالي المواعيد: {stats.ApptCount}")
                    lblCpApptFirst.Text = If(Eng, $"First: {FormatCpDockDate(dock.FirstApptStart)}", $"الأول: {FormatCpDockDate(dock.FirstApptStart)}")
                    lblCpApptLast.Text = If(Eng, $"Last: {FormatCpDockDate(dock.LastApptStart)}", $"الأخير: {FormatCpDockDate(dock.LastApptStart)}")
                    If hlCpAppts IsNot Nothing Then hlCpAppts.Text = If(Eng, "Open visits →", "فتح الزيارات ←")
                    hlCpTrtCount.Text = If(Eng, $"Treatments: {stats.TreatmentCount}", $"علاجات: {stats.TreatmentCount}")
                    lblCpTrtFirst.Text = If(Eng, $"First treatment: {FormatCpDockDate(dock.FirstTrtDate)}", $"أول علاج: {FormatCpDockDate(dock.FirstTrtDate)}")
                    lblCpTrtLast.Text = If(Eng, $"Last treatment: {FormatCpDockDate(dock.LastTrtDate)}", $"آخر علاج: {FormatCpDockDate(dock.LastTrtDate)}")
                    hlCpTrtSum.Text = If(Eng, $"Treatments Σ: {trtSumStr}", $"مجموع علاجات: {trtSumStr}")
                    hlCpPaySum.Text = If(Eng, $"Payments Σ: {paySumStr}", $"مجموع مدفوعات: {paySumStr}")
                    lblCpOrthoStart.Text = If(Eng, $"Ortho start: {FormatCpDockDate(dock.OrthoStartDate)}", $"بداية التقويم: {FormatCpDockDate(dock.OrthoStartDate)}")
                    lblCpOrthoLast.Text = If(Eng, $"Last ortho work: {FormatCpDockDate(dock.OrthoLastWorkDate)}", $"آخر عمل تقويم: {FormatCpDockDate(dock.OrthoLastWorkDate)}")
                    lblCpDiagCount.Text = If(Eng, $"Tooth diagnoses: {dock.DiagToothCount}", $"تشخيص أسنان: {dock.DiagToothCount}")
                    Dim d1 = FormatCpDockDate(dock.DiagToothFirstDate)
                    Dim d2 = FormatCpDockDate(dock.DiagToothLastDate)
                    Dim t1 = TruncateCpLabelText(dock.DiagToothFirstTreat, 48)
                    Dim t2 = TruncateCpLabelText(dock.DiagToothLastTreat, 48)
                    lblCpDiagFirst.Text = If(Eng, $"First: {d1}" & If(String.IsNullOrEmpty(t1), "", $" · {t1}"), $"الأول: {d1}" & If(String.IsNullOrEmpty(t1), "", $" · {t1}"))
                    lblCpDiagLast.Text = If(Eng, $"Last: {d2}" & If(String.IsNullOrEmpty(t2), "", $" · {t2}"), $"الأخير: {d2}" & If(String.IsNullOrEmpty(t2), "", $" · {t2}"))
                    lblCpDiagAgreements.Text = If(Eng, $"Diag. agreements: {dock.DiagDetAgreementCount}", $"اتفاقيات تفصيلية: {dock.DiagDetAgreementCount}")
                    lblCpDiagDetFirst.Text = If(Eng, $"First detail date: {FormatCpDockDate(dock.DiagDetFirstDate)}", $"أول تفصيل: {FormatCpDockDate(dock.DiagDetFirstDate)}")
                    lblCpDiagDetLast.Text = If(Eng, $"Last detail date: {FormatCpDockDate(dock.DiagDetLastDate)}", $"آخر تفصيل: {FormatCpDockDate(dock.DiagDetLastDate)}")
                    hlCpOrthoFlag.Text = If(stats.OrthoFlag,
                        If(Eng, "Ortho: Yes", "تقويم: نعم"),
                        If(Eng, "Ortho: No", "تقويم: لا"))
                    hlCpDiagFlag.Text = If(stats.DiagFlag,
                        If(Eng, "Diag: Yes", "تشخيص: نعم"),
                        If(Eng, "Diag: No", "تشخيص: لا"))
                    hlCpImgBefore.Text = If(Eng, $"Before: {stats.ImgBeforeCount}", $"قبل: {stats.ImgBeforeCount}")
                    hlCpImgDuring.Text = If(Eng, $"During: {stats.ImgDuringCount}", $"أثناء: {stats.ImgDuringCount}")
                    hlCpImgAfter.Text = If(Eng, $"After: {stats.ImgAfterCount}", $"بعد: {stats.ImgAfterCount}")
                    hlCpLabs.Text = If(Eng, $"Lab orders: {stats.LabOrderCount}", $"مختبر: {stats.LabOrderCount}")

                    Dim phoneDisp = If(String.IsNullOrWhiteSpace(p.Phone), If(Eng, "Phone: —", "الهاتف: —"), If(Eng, $"Phone: {p.Phone.Trim()}", $"الهاتف: {p.Phone.Trim()}"))
                    lblCpPhone.Text = phoneDisp

                    Dim waLocal = If(String.IsNullOrWhiteSpace(p.WhatsApp), "", p.WhatsApp.Trim())
                    lblCpWhats.Text = If(String.IsNullOrWhiteSpace(waLocal),
                        If(Eng, "WhatsApp: —", "واتساب: —"),
                        If(Eng, $"WhatsApp: {waLocal}", $"واتساب: {waLocal}"))
                    Dim waDial = FormatCpWhatsDialNumber(p.WhatsAppPrefix, waLocal)
                    If lblCpWhatsNumber IsNot Nothing Then
                        lblCpWhatsNumber.Text = If(String.IsNullOrWhiteSpace(waDial),
                            If(Eng, "WhatsApp #: —", "واتساب للاتصال: —"),
                            If(Eng, $"WhatsApp #: {waDial}", $"واتساب للاتصال: {waDial}"))
                    End If

                    Dim balStr = CDec(p.Balance).ToCurrencyString(If(Eng, CurrencyType.ILS_En, CurrencyType.ILS_Ar))
                    lblCpBalance.Text = If(Eng, $"Balance: {balStr}", $"الرصيد: {balStr}")

                    Dim nextAppt = _currentPatientDockApptRepo.GetNextFutureAppointment(p.PatientID, DateTime.Now)
                    If nextAppt Is Nothing Then
                        lblCpNextAppt.Text = If(Eng, "Next appointment: —", "الموعد القادم: —")
                    Else
                        Dim drName = ""
                        Try
                            If nextAppt.DrID > 0 Then drName = _currentPatientDockApptRepo.GetDoctorName(nextAppt.DrID)
                        Catch
                        End Try
                        Dim whenTxt = nextAppt.StartDateTime.ToString("g", CultureInfo.CurrentCulture)
                        If String.IsNullOrWhiteSpace(drName) Then
                            lblCpNextAppt.Text = If(Eng, $"Next appointment: {whenTxt}", $"الموعد القادم: {whenTxt}")
                        Else
                            lblCpNextAppt.Text = If(Eng, $"Next appointment: {whenTxt} ({drName})", $"الموعد القادم: {whenTxt} ({drName})")
                        End If
                    End If

                    Dim healthRaw = If(String.IsNullOrWhiteSpace(p.Health), "", p.Health.Trim())
                    If String.IsNullOrWhiteSpace(healthRaw) Then
                        lblCpHealth.Text = If(Eng, "Health / alerts: —", "الصحة / تنبيهات: —")
                        lblCpHealth.Appearance.ForeColor = Color.FromArgb(64, 64, 64)
                    Else
                        lblCpHealth.Text = If(Eng, $"Health / alerts: {healthRaw}", $"الصحة / تنبيهات: {healthRaw}")
                        lblCpHealth.Appearance.ForeColor = Color.FromArgb(180, 0, 0)
                    End If
                    lblCpHealth.Appearance.Options.UseForeColor = True

                    PanelCurrentPatientHost_SizeChanged(panelCurrentPatientHost, EventArgs.Empty)
                Catch
                End Try
            End Sub
        If InvokeRequired Then
            BeginInvoke(apply)
        Else
            apply()
        End If
    End Sub

    Private Sub BtnCpCopyPhone_Click(sender As Object, e As EventArgs)
        Dim p = FormManager.Instance.GetCurrentPatient()
        If p Is Nothing Then Return
        'Dim wa = FormatCpWhatsDialNumber(p.WhatsAppPrefix, If(p.WhatsApp, "").Trim())
        Dim waForCopy = BuildInternationalWhatsDigits(If(p.WhatsApp, "").Trim(), p.WhatsAppPrefix)

        If String.IsNullOrWhiteSpace(waForCopy) Then Return
        Try
            Clipboard.SetText(waForCopy)
        Catch
        End Try
    End Sub

    Private Sub BtnCpFocusWorkspace_Click(sender As Object, e As EventArgs)
        Dim p = FormManager.Instance.GetCurrentPatient()
        If p Is Nothing OrElse p.PatientID <= 0 Then Return
        Try
            If Not FormAccessGate.TryEnterForm(Me, "TreatsUserControl") Then Return
            FormManager.Instance.SwitchUserControl(GetType(TreatsUserControl), "Treat", p, Me)
            Dim ws = FormManager.Instance.CurrentForm
            If ws IsNot Nothing AndAlso Not ws.IsDisposed Then ws.BringToFront()
            Activate()
        Catch ex As Exception
            MsgBox("Treatments form could not load: " & vbCrLf & vbCrLf & GetInnermostExceptionMessage(ex))
        End Try
    End Sub

    Private Sub CpStatHyperlink_Click(sender As Object, e As HyperlinkClickEventArgs)
        Dim p = FormManager.Instance.GetCurrentPatient()
        If p Is Nothing OrElse p.PatientID <= 0 Then Return
        Dim hl = TryCast(sender, HyperlinkLabelControl)
        If hl Is Nothing Then Return
        Try
            If ReferenceEquals(hl, hlCpAppts) Then
                If Not FormAccessGate.TryEnterForm(Me, "PatientVisitsCtl") Then Return
                stFormNameTxt.Caption = If(Eng, "Visits", "الزيارات")
                FormManager.Instance.SwitchUserControl(GetType(PatientVisitsCtl), "Visits", p, Me)
            ElseIf ReferenceEquals(hl, hlCpTrtCount) Then
                If Not FormAccessGate.TryEnterForm(Me, "TreatsUserControl") Then Return
                _filter = "Treat"
                stFormNameTxt.Caption = "Treatments Form"
                FormManager.Instance.SwitchUserControl(GetType(TreatsUserControl), "Treat", p, Me)
            ElseIf ReferenceEquals(hl, hlCpPaySum) OrElse ReferenceEquals(hl, hlCpTrtSum) Then
                If Not FormAccessGate.TryEnterForm(Me, "NewAccounting") Then Return
                FormManager.Instance.SwitchUserControl(GetType(NewAccounting), "Accounts", p, Me)
            ElseIf ReferenceEquals(hl, hlCpOrthoFlag) Then
                If Not FormAccessGate.TryEnterForm(Me, "FullOrthoTreating") Then Return
                _filter = "Ortho"
                stFormNameTxt.Caption = "Orthodontic Form"
                FormManager.Instance.SwitchUserControl(GetType(FullOrthoTreating), "Ortho", p, Me)
            ElseIf ReferenceEquals(hl, hlCpDiagFlag) Then
                If Not FormAccessGate.TryEnterForm(Me, "DiagUserControl") Then Return
                _filter = "Diag"
                stFormNameTxt.Caption = "Diagnostics Form"
                FormManager.Instance.SwitchUserControl(GetType(DiagUserControl), "Diag", p, Me)
            ElseIf ReferenceEquals(hl, hlCpImgBefore) OrElse ReferenceEquals(hl, hlCpImgDuring) OrElse ReferenceEquals(hl, hlCpImgAfter) Then
                If Not FormAccessGate.TryEnterForm(Me, "ThumbNailViewer") Then Return
                FormManager.Instance.SwitchUserControl(GetType(ThumbNailViewer), "Images", p, Me)
            ElseIf ReferenceEquals(hl, hlCpLabs) Then
                OpenLabOrderDialogForPatient(p)
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub OpenLabOrderDialogForPatient(p As Patient)
        If Not FormAccessGate.TryEnterForm(Me, "FrmLabOrder2") Then Return
        FrmLabOrder2.Icon = AppIcon
        Dim shownHandler As EventHandler = Nothing
        shownHandler = Sub(s, args)
                           RemoveHandler FrmLabOrder2.Shown, shownHandler
                           FrmLabOrder2.colPatientCombo.SetCurrentPatientName(p.PatientID)
                       End Sub
        AddHandler FrmLabOrder2.Shown, shownHandler
        FrmLabOrder2.ShowDialog(Me)
    End Sub

    Private Sub BtnCpRefresh_Click(sender As Object, e As EventArgs)
        RefreshCurrentPatientDockPanel(reloadFromDb:=True)
    End Sub

    ''' <summary>Hides the top auto-hide strip entirely when no patient (same idea as Message Center starting hidden).</summary>
    Private Sub HideCurrentPatientDockShell()
        Try
            If hideContainerTop IsNot Nothing Then hideContainerTop.Visible = False
        Catch
        End Try
    End Sub

    ''' <summary>Shows the top auto-hide container so the Current patient tab can appear (does not force the panel open).</summary>
    Private Sub EnsureCurrentPatientDockVisible()
        Try
            If hideContainerTop IsNot Nothing Then hideContainerTop.Visible = True
            If DockCurrentPatient IsNot Nothing AndAlso DockCurrentPatient.Visibility <> DevExpress.XtraBars.Docking.DockVisibility.AutoHide Then
                DockCurrentPatient.Visibility = DevExpress.XtraBars.Docking.DockVisibility.AutoHide
            End If
        Catch
        End Try
    End Sub

#End Region

#Region "MainButtons"
    Property IsBaseClosed As Boolean = False
    Dim isGrid As Boolean = False
    Dim isKid As Boolean = False
    Dim isFull As Boolean = True
    Dim currentPatient As New Patient
    Private Sub HandlePatientChanged(sender As Object, e As PatientChangedEventArgs)
        If e Is Nothing Then Return
        If e.NewPatient Is Nothing Then
            currentPatient = Nothing
            PatientID = 0
            stPatientNameTxt.Caption = ""
            RefreshCurrentPatientDockPanel(patientOverride:=Nothing)
            Return
        End If
        PatientID = e.NewPatient.PatientID
        currentPatient = e.NewPatient
        stPatientNameTxt.Caption = $"{e.NewPatient.PatientName} ** {e.NewPatient.PatientNumber}"
        isGrid = currentPatient.IsGrid
        isFull = currentPatient.IsFull
        isKid = currentPatient.IsKid
        ' Workspace assigns Current_Patient after this handler (subscription order) — pass the new patient so the dock updates immediately.
        RefreshCurrentPatientDockPanel(patientOverride:=e.NewPatient)
    End Sub

    Public WriteOnly Property ValueFromChild() As Integer
        Set(ByVal Value As Integer)
            'LoadBal(Value)

        End Set
    End Property
    Public Sub EnableDoubleBuffering(control As Control)
        Dim doubleBufferProp = control.[GetType]().GetProperty("DoubleBuffered", BindingFlags.Instance Or BindingFlags.NonPublic)
        If doubleBufferProp IsNot Nothing Then
            doubleBufferProp.SetValue(control, True, Nothing)
        End If
    End Sub
    Public Sub SetStatusText(text As String)
        If stLoadingLbl Is Nothing Then Return
        Dim apply =
            Sub()
                stLoadingLbl.Caption = text
                stLoadingLbl.Hint = Nothing
                _lastReminderDetails = ""
            End Sub
        If Me.InvokeRequired Then
            Me.Invoke(apply)
        Else
            apply()
        End If
    End Sub

    Private Function GetPatientCount(colName As String) As Boolean
        Try
            Dim connectionString As String = DentistXDATA.GetConnection.ConnectionString
            Dim query As String = $"SELECT COUNT(*) FROM [Patient] WHERE {colName}=1"

            Using connection As New SqlConnection(connectionString)
                connection.Open()
                Dim count As Integer = connection.QuerySingle(Of Integer)(query)
                Return (count > 0)
            End Using
        Catch ex As Exception
            ' Log error if needed (e.g., Debug.WriteLine(ex.Message))
            Return False ' Assume no patients if an error occurs
        End Try
    End Function

    Private Function GetFirstPatient(colName As String) As Patient
        Try
            Dim connectionString As String = DentistXDATA.GetConnection.ConnectionString
            Dim query As String = $"SELECT TOP(1) FROM [Patient] WHERE {colName}=1"

            Using connection As New SqlConnection(connectionString)
                connection.Open()
                Return Conn.QuerySingleOrDefault(Of Patient)(query)
            End Using
        Catch ex As Exception
            ' Log error if needed (e.g., Debug.WriteLine(ex.Message))
            Return Nothing ' Assume no patients if an error occurs
        End Try
    End Function

    Private Sub UseHdrCheckItem_CheckedChanged(sender As Object, e As ItemClickEventArgs) Handles UseHdrCheckItem.CheckedChanged
        If _suppressUseHdrCheckItemPersistence Then
            UpdateHeaderStyleCaption()
            Return
        End If

        ' Checked = "Use New Header Style" (Navigator), Unchecked = legacy slot (Navigator2 in BasePatientWorkspace; HdrTestMod retired)
        BasePatientWorkspace.UseHdrTestModHeader = Not UseHdrCheckItem.Checked
        My.Settings.UseHdrTestModHeader = BasePatientWorkspace.UseHdrTestModHeader
        My.Settings.Save()
        UpdateHeaderStyleCaption()
        If FormManager.Instance.IsBasePatientFormOpen Then
            XtraMessageBox.Show(
                If(Eng, "Close and reopen the patient form (Treat/Diag/Ortho) for the Patient Navigator change to take effect.", "أغلق وأعد فتح نموذج المريض (علاج/تشخيص/تقويم) لتطبيق تغيير نوع مستكشف المرضى."),
                If(Eng, "Patient Navigator type changed", "تم تغيير نوع مستكشف المرضى"),
                MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub

    Private Sub UpdateHeaderStyleCaption()
        UseHdrCheckItem.Caption = If(UseHdrCheckItem.Checked, If(Eng, "Use New Header Style", "استخدم نمط عرض المرضى الجديد"), If(Eng, "Use Old Header Style", "استخدم نمط عرض المرضى القديم"))
        If UseHdrCheckItem.Hint IsNot Nothing Then
            UseHdrCheckItem.Hint = UseHdrCheckItem.Caption
        End If
    End Sub

    ' Method to get current patient from any context
    ' Method to get current context
    Public Function GetCurrentContext() As String
        If FormManager.Instance.IsBasePatientFormOpen Then
            Return $"Form: {FormManager.Instance.CurrentForm.GetType().Name}, Patient: {FormManager.Instance.GetCurrentPatient()?.PatientID}"
        Else
            Return "No form open"
        End If
    End Function

    ' Method to close current form
    Public Sub CloseCurrentForm()
        If FormManager.Instance.IsBasePatientFormOpen Then
            FormManager.Instance.CloseBaseForm()
            Me.ContainerA.Controls.Clear()
        End If
    End Sub
    Public Function GetCurrentContextPatient() As Patient
        Return FormManager.Instance.GetCurrentPatient()
    End Function

    ''' <summary>Screen bounds of the Fluent client area (same region as hosted patient forms) — for overlay dialogs such as the WhatsApp log.</summary>
    Public Function GetContainerAScreenBounds() As Rectangle
        Return ContainerA.RectangleToScreen(New Rectangle(0, 0, ContainerA.ClientSize.Width, ContainerA.ClientSize.Height))
    End Function

    Private _filter As String = "Treat"

    ''' <summary>Unwrap nested "target of invocation" to get the real error (e.g. missing DLL, file not found).</summary>
    Private Shared Function GetInnermostExceptionMessage(ex As Exception) As String
        Dim inner = ex
        While inner.InnerException IsNot Nothing
            inner = inner.InnerException
        End While
        Return $"{inner.GetType().Name}: {inner.Message}"
    End Function

    ' Method to sync patient changes from HdrTest to MainView3
    Public Sub SyncPatientFromHdrTest(patient As Patient)
        currentPatient = patient
        Select Case _filter
            Case "Treat"
                Try
                    If Not FormAccessGate.TryEnterForm(Me, "TreatsUserControl") Then Return
                    If SplashScreenManager1.IsSplashFormVisible Then SplashScreenManager1.CloseWaitForm()
                    SplashScreenManager1.ShowWaitForm()
                    stFormNameTxt.Caption = "Treatments Form"
                    ''FormManager.Instance.SwitchUserControl(GetType(AdultJaw), "Treat")
                    FormManager.Instance.SwitchUserControl(GetType(TreatsUserControl), "Treat")
                Catch ex As Exception
                    MsgBox("Treatments form could not load: " & vbCrLf & vbCrLf & GetInnermostExceptionMessage(ex))
                Finally
                    SplashScreenManager1.CloseWaitForm()
                    Me.Cursor = Cursors.Default
                End Try
            Case "Ortho"
                Try
                    If Not FormAccessGate.TryEnterForm(Me, "FullOrthoTreating") Then Return
                    If SplashScreenManager1.IsSplashFormVisible Then SplashScreenManager1.CloseWaitForm()
                    SplashScreenManager1.ShowWaitForm()
                    stFormNameTxt.Caption = "Orthodontic Form"
                    If FormManager.Instance.CurrentForm IsNot Nothing Then
                        Dim currPatient = FormManager.Instance.GetCurrentPatient()
                    End If
                    FormManager.Instance.SwitchUserControl(GetType(FullOrthoTreating), "Ortho")
                Catch ex As Exception
                    MsgBox(ex.Message)
                Finally
                    SplashScreenManager1.CloseWaitForm()
                    Me.Cursor = Cursors.Default
                End Try
            Case "Diag"
                Try
                    If Not FormAccessGate.TryEnterForm(Me, "DiagUserControl") Then Return
                    If SplashScreenManager1.IsSplashFormVisible Then SplashScreenManager1.CloseWaitForm()
                    SplashScreenManager1.ShowWaitForm()
                    stFormNameTxt.Caption = "Diagnostics Form"
                    'ShowModule(GetType(OrthoForm), GetType(FullOrthoTreating), "Ortho")
                    If FormManager.Instance.CurrentForm IsNot Nothing Then
                        Dim currPatient = FormManager.Instance.GetCurrentPatient()
                    End If
                    FormManager.Instance.SwitchUserControl(GetType(DiagUserControl), "Diag")
                Catch ex As Exception
                    MsgBox(ex.Message)
                Finally
                    SplashScreenManager1.CloseWaitForm()
                    Me.Cursor = Cursors.Default
                End Try
        End Select
        RefreshCurrentPatientDockPanel(patientOverride:=FormManager.Instance.GetCurrentPatient())
    End Sub

    Private Sub TreatsButton_Click(sender As Object, e As EventArgs) Handles TreatsButton.Click
        Try
            If Not FormAccessGate.TryEnterForm(Me, "TreatsUserControl") Then Return
            _filter = "Treat"
            If SplashScreenManager1.IsSplashFormVisible Then SplashScreenManager1.CloseWaitForm()
            SplashScreenManager1.ShowWaitForm()
            stFormNameTxt.Caption = "Treatments Form"
            If FormManager.Instance.CurrentForm IsNot Nothing Then
                Dim currPatient = FormManager.Instance.GetCurrentPatient()
            End If
            FormManager.Instance.SwitchUserControl(GetType(TreatsUserControl), "Treat")
        Catch ex As Exception
            MsgBox("Treatments form could not load: " & vbCrLf & vbCrLf & GetInnermostExceptionMessage(ex))
        Finally
            SplashScreenManager1.CloseWaitForm()
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    Private Sub OrthoButton_Click(sender As Object, e As EventArgs) Handles OrthoButton.Click
        Try
            If Not FormAccessGate.TryEnterForm(Me, "FullOrthoTreating") Then Return
            _filter = "Ortho"
            If SplashScreenManager1.IsSplashFormVisible Then SplashScreenManager1.CloseWaitForm()
            SplashScreenManager1.ShowWaitForm()
            stFormNameTxt.Caption = "Orthodontic Form"
            If FormManager.Instance.CurrentForm IsNot Nothing Then
                Dim currPatient = FormManager.Instance.GetCurrentPatient()
            End If
            FormManager.Instance.SwitchUserControl(GetType(FullOrthoTreating), "Ortho")
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            SplashScreenManager1.CloseWaitForm()
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub MobileButton_Click(sender As Object, e As EventArgs) Handles DiagButton.Click
        Try
            If Not FormAccessGate.TryEnterForm(Me, "DiagUserControl") Then Return
            _filter = "Diag"
            If SplashScreenManager1.IsSplashFormVisible Then SplashScreenManager1.CloseWaitForm()
            SplashScreenManager1.ShowWaitForm()
            stFormNameTxt.Caption = "Diagnostics Form"
            'ShowModule(GetType(OrthoForm), GetType(FullOrthoTreating), "Ortho")
            If FormManager.Instance.CurrentForm IsNot Nothing Then
                Dim currPatient = FormManager.Instance.GetCurrentPatient()
            End If
            FormManager.Instance.SwitchUserControl(GetType(DiagUserControl), "Diag")
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            SplashScreenManager1.CloseWaitForm()
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub ScheduleAdmin_Click(sender As Object, e As EventArgs) Handles ScheduleAdmin.Click

        Try
            If Not FormAccessGate.TryEnterForm(Me, "SchedulerFull") Then Return
            FormManager.Instance.SwitchUserControl(GetType(SchedulerNew), "Appointments Scheduler")
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub btnOLdScheduler_Click(sender As Object, e As EventArgs) Handles btnOLdScheduler.Click

        Try
            If Not FormAccessGate.TryEnterForm(Me, "SchedulerFull") Then Return
            ShowModernSchedulerForm()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
#End Region
#Region "Auxes"

    Private Sub AccountsButton_Click(sender As Object, e As EventArgs) Handles AccountsButton.Click
        Try
            If Not FormAccessGate.TryEnterForm(Me, "NewAccounting") Then Return

            ''ShowModule(GetType(AccountsForm), GetType(AccntCtlNew), "Accounts")
            FormManager.Instance.SwitchUserControl(GetType(NewAccounting), "Accounts")
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub



    Private Sub VisitsButton_Click(sender As Object, e As EventArgs) Handles VisitsButton.Click
        Try
            If Not FormAccessGate.TryEnterForm(Me, "PatientVisitsCtl") Then Return
            'FormManager.Instance.SwitchUserControl(GetType(ScheduleThisPatient), "Visits")Frm5DueChqs
            FormManager.Instance.SwitchUserControl(GetType(PatientVisitsCtl), "Visits")
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub
    Private Sub NotesButton_Click(sender As Object, e As EventArgs) Handles NotesButton.Click
        Try
            If Not FormAccessGate.TryEnterForm(Me, "CtlNotes") Then Return
            'ShowModule(GetType(NotesForm), GetType(CtlNotes), "Notes")
            FormManager.Instance.SwitchUserControl(GetType(CtlNotes), "Notes")
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub RxButton_Click(sender As Object, e As EventArgs) Handles RxButton.Click
        Try
            If Not FormAccessGate.TryEnterForm(Me, "RxCTLNEW") Then Return
            'ShowModule(GetType(RxForm), GetType(RxCTL), "Rx")
            FormManager.Instance.SwitchUserControl(GetType(RxCTLNEW), "Rx")
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub ImagesButton_Click(sender As Object, e As EventArgs) Handles ImagesButton.Click
        Try
            If Not FormAccessGate.TryEnterForm(Me, "ThumbNailViewer") Then Return
            'ShowModule(GetType(ImagesForm), GetType(ThumbNailViewer), "Images")
            FormManager.Instance.SwitchUserControl(GetType(ThumbNailViewer), "Images")
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub TodayButton_Click(sender As Object, e As EventArgs)
        Dim fr As New TodayApptEditorForm 'FrmAdminAppt()
        fr.Icon = AppIcon
        fr.ShowDialog(Me)
        Try
            'FormManager.Instance.SwitchUserControl(GetType(ScheduleThisPatient), "Visits")
            'FormManager.Instance.SwitchUserControl(GetType(PatientVisitsCtl), "Visits")
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub ShowAllAppointments()
        Dim _repo As New AppointmentCRepository(DentistXDATA.GetConnection.ConnectionString)
        Dim allAppts As List(Of AppointmentC) = _repo.GetAllAppointments().ToList()

        If allAppts.Count = 0 Then
            MessageBox.Show("No appointments found.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        ' === Main Form ===
        Dim frm As New Form With {
        .Text = "All Appointments",
        .Size = New Size(850, 520),
        .StartPosition = FormStartPosition.CenterScreen,
        .BackColor = Color.White
    }

        ' === Split Container ===
        Dim split As New SplitContainer With {
        .Dock = DockStyle.Fill,
        .Orientation = Orientation.Horizontal,
        .SplitterDistance = 50,   ' Top panel height
        .IsSplitterFixed = False,
        .BackColor = Color.White
    }
        frm.Controls.Add(split)

        ' === GroupBox for Search ===
        Dim grpSearch As New GroupBox With {
        .Text = "Search patient or doctor...",
        .Dock = DockStyle.Fill,
        .ForeColor = Color.Navy,
        .Font = New Font("Segoe UI", 9, FontStyle.Bold)
    }

        Dim lblPatient As New Label With {.Text = "Search Patient:", .Location = New Point(12, 20), .AutoSize = True}
        Dim txtPatient As New TextBox With {.Location = New Point(110, 18), .Width = 160}

        Dim lblDoctor As New Label With {.Text = "Search Doctor:", .Location = New Point(290, 20), .AutoSize = True}
        Dim txtDoctor As New TextBox With {.Location = New Point(380, 18), .Width = 160}

        Dim chkToday As New CheckBox With {.Text = "Today only", .Location = New Point(560, 20), .AutoSize = True}

        grpSearch.Controls.AddRange({lblPatient, txtPatient, lblDoctor, txtDoctor, chkToday})
        split.Panel1.Controls.Add(grpSearch)

        ' === ListView ===
        Dim list As New ListView With {
        .Dock = DockStyle.Fill,
        .View = View.Details,
        .FullRowSelect = True,
        .GridLines = True,
        .Font = New Font("Segoe UI", 9, FontStyle.Regular),
        .OwnerDraw = True,
        .HeaderStyle = ColumnHeaderStyle.Nonclickable
    }

        list.Columns.Add("Date", 100, HorizontalAlignment.Center)
        list.Columns.Add("Time (From–To)", 130, HorizontalAlignment.Center)
        list.Columns.Add("Patient", 160, HorizontalAlignment.Left)
        list.Columns.Add("Doctor", 160, HorizontalAlignment.Left)
        list.Columns.Add("Details", 260, HorizontalAlignment.Left)

        split.Panel2.Controls.Add(list)

        ' === Event Handlers ===
        AddHandler frm.Load, Sub(sender, e)
                                 RefreshAppointmentList(list, _repo, allAppts, chkToday, txtPatient, txtDoctor)
                             End Sub

        AddHandler chkToday.CheckedChanged, Sub(sender, e)
                                                RefreshAppointmentList(list, _repo, allAppts, chkToday, txtPatient, txtDoctor)
                                            End Sub
        AddHandler txtPatient.TextChanged, Sub(sender, e)
                                               RefreshAppointmentList(list, _repo, allAppts, chkToday, txtPatient, txtDoctor)
                                           End Sub
        AddHandler txtDoctor.TextChanged, Sub(sender, e)
                                              RefreshAppointmentList(list, _repo, allAppts, chkToday, txtPatient, txtDoctor)
                                          End Sub

        AddHandler list.DoubleClick, Sub(sender, e)
                                         If list.SelectedItems.Count = 0 Then Return
                                         Dim selectedAppt = CType(list.SelectedItems(0).Tag, AppointmentC)
                                         Dim editor As New AppointCEditorForm(selectedAppt, False)
                                         editor.ShowDialog(frm)
                                         RefreshAppointmentList(list, _repo, allAppts, chkToday, txtPatient, txtDoctor)
                                     End Sub

        ' === Draw only headers with colors 🟢 🔵 🟣 🟠 ===
        Dim headerColors() As Color = {
        Color.FromArgb(60, 179, 113), ' 🟢 Green
        Color.FromArgb(30, 144, 255), ' 🔵 Blue
        Color.FromArgb(138, 43, 226), ' 🟣 Purple
        Color.FromArgb(255, 140, 0),  ' 🟠 Orange
        Color.FromArgb(30, 144, 255)  ' Repeat blue
    }

        AddHandler list.DrawColumnHeader, Sub(sender, e)
                                              Dim colIndex As Integer = e.ColumnIndex Mod headerColors.Length
                                              Using br As New SolidBrush(headerColors(colIndex))
                                                  e.Graphics.FillRectangle(br, e.Bounds)
                                              End Using
                                              Dim flags As TextFormatFlags = TextFormatFlags.VerticalCenter Or TextFormatFlags.HorizontalCenter Or TextFormatFlags.SingleLine
                                              TextRenderer.DrawText(e.Graphics, e.Header.Text, New Font(list.Font, FontStyle.Bold), e.Bounds, Color.White, flags)
                                          End Sub

        ' No owner-draw for rows: let WinForms handle them, we will just set alternating colors in RefreshAppointmentList
        frm.ShowDialog()
    End Sub


    ' === Separate Sub to Refresh the List ===
    Private Sub RefreshAppointmentList(list As ListView, _repo As AppointmentCRepository, allAppts As List(Of AppointmentC),
                                   chkToday As CheckBox, txtPatient As TextBox, txtDoctor As TextBox)

        list.BeginUpdate()
        list.Items.Clear()

        Dim filtered = allAppts.AsEnumerable()

        ' --- Filter for Today ---
        If chkToday.Checked Then
            filtered = filtered.Where(Function(a) a.AppDate.Date = Date.Today)
        End If

        ' --- Filter by patient ---
        Dim pKey As String = txtPatient.Text.Trim().ToLower()
        If pKey <> "" Then
            filtered = filtered.Where(Function(a)
                                          Dim pName = _repo.GetPatientName(a.PatientID).ToLower()
                                          Return pName.Contains(pKey)
                                      End Function)
        End If

        ' --- Filter by doctor ---
        Dim dKey As String = txtDoctor.Text.Trim().ToLower()
        If dKey <> "" Then
            filtered = filtered.Where(Function(a)
                                          Dim dName = _repo.GetDoctorName(a.DrID).ToLower()
                                          Return dName.Contains(dKey)
                                      End Function)
        End If

        ' --- Populate ---
        For Each appt In filtered.OrderBy(Function(a) a.AppDate).ThenBy(Function(a) a.StartDateTime)
            Dim pName = _repo.GetPatientName(appt.PatientID)
            Dim dName = _repo.GetDoctorName(appt.DrID)

            Dim item As New ListViewItem(appt.AppDate.ToString("dd/MM/yyyy"))
            item.SubItems.Add($"{appt.StartDateTime:HH:mm} - {appt.EndDateTime:HH:mm}")
            item.SubItems.Add(pName)
            item.SubItems.Add(dName)
            item.SubItems.Add(appt.Reason)

            ' Alternate row colors
            If list.Items.Count Mod 2 = 0 Then
                item.BackColor = Color.FromArgb(230, 245, 255)
            Else
                item.BackColor = Color.White
            End If

            item.ForeColor = GetDoctorColor(dName)
            item.Tag = appt
            list.Items.Add(item)
        Next

        list.EndUpdate()
    End Sub




    Private Function GetDoctorColor(doctorName As String) As Color
        Dim hash = doctorName.GetHashCode()
        Dim r = (hash And &HFF)
        Dim g = (hash >> 8) And &HFF
        Dim b = (hash >> 16) And &HFF
        Return Color.FromArgb(255, r Mod 200, g Mod 200, b Mod 200)
    End Function

    'Private Sub LendBorrowButton_Click(sender As Object, e As EventArgs) Handles LendBorrowButton.Click
    '    Try
    '        Dim F As New FrmVendorBuy 'FrmVendors ' HistoryViewerForm 'FrmLastPatients 'FrmTodayVisits 'Frm5DueChqs 'FrmExchMny 'FrmDefColors '
    '        F.ShowDialog(Me)
    '    Catch ex As Exception
    '        MsgBox(ex.Message)
    '    End Try
    'End Sub

#End Region

#Region "Skins"

    Protected Overrides Sub OnShown(ByVal e As EventArgs)
        MyBase.OnShown(e)
        RestorePalette()
        ShowSettings()
    End Sub

    Private Sub SavePalette()
        Dim settings = My.Settings
        settings.SkinName = UserLookAndFeel.Default.SkinName
        settings.Palette = UserLookAndFeel.Default.ActiveSvgPaletteName
        settings.CompactMode = UserLookAndFeel.Default.CompactUIModeForced
        'settings.Lang = _lang
        settings.Save()
    End Sub

    Private Sub RestorePalette()
        Dim settings = My.Settings
        If Not String.IsNullOrEmpty(settings.SkinName) Then
            If settings.CompactMode Then UserLookAndFeel.ForceCompactUIMode(True, False)
            If Not String.IsNullOrEmpty(settings.Palette) Then
                UserLookAndFeel.Default.SetSkinStyle(settings.SkinName, settings.Palette)
            Else
                UserLookAndFeel.Default.SetSkinStyle(settings.SkinName)
            End If
        End If
        'If Not String.IsNullOrEmpty(settings.Lang) Then
        '    _lang = settings.Lang
        '    If _lang = "en" Then
        '        Eng = True
        '    Else
        '        Eng = False
        '    End If
        'End If
    End Sub

    Private Sub ShowSettings()

        'labelControl1.Text = MySettings.Default.SkinName
        'labelControl4.Text = If(Not Equals(MySettings.Default.Palette, String.Empty), MySettings.Default.Palette, "n/a (default palette or raster skin)")
        'labelControl6.Text = If(MySettings.Default.CompactMode, "Yes", "No")
    End Sub

#End Region

#Region "WhatsApp short lead (settings)"

    Private _suppressShortReminderSpinPersist As Boolean

    Private Sub ApplyShortReminderSpinFromSettings()
        If spinShortReminder Is Nothing OrElse RepositoryItemSpinEdit1 Is Nothing Then Return
        _suppressShortReminderSpinPersist = True
        Try
            Dim v = CInt(My.Settings.ShortReminder)
            Dim min = CInt(RepositoryItemSpinEdit1.MinValue)
            Dim max = CInt(RepositoryItemSpinEdit1.MaxValue)
            If v < min Then v = min
            If v > max Then v = max
            spinShortReminder.EditValue = v
        Finally
            _suppressShortReminderSpinPersist = False
        End Try
    End Sub

    Private Sub spinShortReminder_EditValueChanged(sender As Object, e As EventArgs) Handles spinShortReminder.EditValueChanged
        If _suppressShortReminderSpinPersist Then Return
        If spinShortReminder Is Nothing OrElse RepositoryItemSpinEdit1 Is Nothing Then Return
        Dim raw = spinShortReminder.EditValue
        If raw Is Nothing OrElse TypeOf raw Is DBNull Then Return
        Dim v As Integer
        If Not Integer.TryParse(raw.ToString().Trim(), NumberStyles.Integer, CultureInfo.CurrentCulture, v) Then
            If Not Integer.TryParse(raw.ToString().Trim(), NumberStyles.Integer, CultureInfo.InvariantCulture, v) Then Return
        End If
        Dim min = CInt(RepositoryItemSpinEdit1.MinValue)
        Dim max = CInt(RepositoryItemSpinEdit1.MaxValue)
        If v < min Then v = min
        If v > max Then v = max
        Try
            My.Settings.ShortReminder = CShort(v)
            My.Settings.Save()
        Catch
        End Try
    End Sub

#End Region

#Region "MainMenu"

    Private Sub Gallery_ItemClick(ByVal sender As Object, ByVal e As GalleryItemClickEventArgs) '(sender As Object, e As ItemClickEventArgs) 'Handles SkinDropDownButtonItem1.ItemPress
        If e Is Nothing OrElse e.Item Is Nothing Then Return
        Dim selectedSkinName As String = e.Item.Caption
        ' The e.Item.Tag contains the internal skin name, which Is more reliable
        ' For older skin types, the tag may Not be set consistently. For modern skins, it's the skin name.
        Dim skinNameFromTag As String = e.Item.Tag?.ToString()
        ' The e.Item represents the clicked skin item
        Dim clickedItemName As String = e.Item.ToString
        BackClr = ContainerA.BackColor
    End Sub

    Private Sub ListUsersMnu_ItemClick(sender As Object, e As ItemClickEventArgs) Handles ListUsersMnu.ItemClick
        FrmListUsers.Icon = AppIcon
        FrmListUsers.ShowDialog(Me)
    End Sub
    Private Sub ListSettingsMnu_ItemClick(sender As Object, e As ItemClickEventArgs) Handles ListSettingsMnu.ItemClick
        FrmSettings.Icon = AppIcon
        FrmSettings.ShowDialog(Me)

    End Sub

    Private Sub ListAddNewUsrGrpMnu_ItemClick(sender As Object, e As ItemClickEventArgs) Handles ListAddNewUsrGrpMnu.ItemClick
        FrmGroupManager.Icon = AppIcon
        FrmGroupManager.ShowDialog(Me)
    End Sub

    Private Sub bntLogInOUT_ItemClick(sender As Object, e As ItemClickEventArgs) Handles bntLogInOUT.ItemClick
        Select Case bntLogInOUT.Caption
            Case "Log IN"
                FrmLogin.Icon = AppIcon
                FrmLogin.ShowDialog(Me)

                If CurrentUser IsNot Nothing Then
                    If CurrentDoctor IsNot Nothing Then
                        stUserNameTxt.Caption = $"User {CurrentUser.UsName} is associated with Doctor. {CurrentDoctor.DrName}"
                    End If
                    'stUserNameTxt.Caption = CurrentUser.UsName & " == " & CurrentDoctor.DrName CurrentEmp
                    If CurrentSecretary IsNot Nothing Then
                        stUserNameTxt.Caption = $"User {CurrentUser.UsName} is associated with Secretary. {CurrentSecretary.SecName}"
                    End If
                    If CurrentEmp IsNot Nothing Then
                        stUserNameTxt.Caption = $"User {CurrentUser.UsName} is associated with Employee. {CurrentEmp.EmpName}"
                    End If
                    If CurrentDoctor Is Nothing AndAlso CurrentSecretary Is Nothing AndAlso CurrentEmp Is Nothing Then
                        stUserNameTxt.Caption = CurrentUser.UsName & " == No Link"
                    End If
                    If CurrentDoctor Is Nothing AndAlso CurrentSecretary Is Nothing AndAlso CurrentEmp Is Nothing Then
                        stUserNameTxt.Caption = CurrentUser.UsName & " == No Link"
                    End If
                End If
                If currentPatient IsNot Nothing Then
                    stPatientNameTxt.Caption = currentPatient.PatientName
                Else
                    stPatientNameTxt.Caption = ""
                End If

                bntLogInOUT.Caption = "Log Out"
            Case "Log Out"
                If ContainerA.Controls.Count > 0 Then
                    For Each ct As Control In ContainerA.Controls
                        ct.Dispose()
                    Next
                    ContainerA.Controls.Clear()
                Else

                End If

                bntLogInOUT.Caption = "Log IN"
                CurrentUser = Nothing
                CurrentDoctor = Nothing
                currentPatient = Nothing
                CurrentSecretary = Nothing
                CurrentEmp = Nothing
                stUserNameTxt.Caption = "Logged Out" 'CurrentUser.UsName
                stPatientNameTxt.Caption = ""
                stFormNameTxt.Caption = ""
                PatientID = 0
                RefreshCurrentPatientDockPanel()
        End Select
        If CurrentUser IsNot Nothing Then
            MainAccordion.Enabled = True
        Else
            MainAccordion.Enabled = False
        End If
        FormAccessGate.Clear()
        If CurrentUser IsNot Nothing Then
            FormAccessGate.RefreshAfterLogin()
        End If
        ApplyFormAccessShell()
        If CursorLicManager.IsLimitedMode Then
            ApplyLimitedModeUi()
        End If
    End Sub

    Private Sub ApplyLimitedModeUi()
        MainAccordion.Enabled = False
        BasicDataMenu.Enabled = False
        MainMenu.Enabled = True
        ListUserMngmntMnu.Enabled = False
        SettingsMnu.Enabled = False
    End Sub

    Private Sub ListChckConnMnu_ItemClick(sender As Object, e As ItemClickEventArgs) Handles ListChckConnMnu.ItemClick
        Try
            ' Test current database connection (server or LocalDB) using the app's main connection string.
            Using connection As New SqlConnection(MainCon.ConString)
                connection.Open()
            End Using
            MsgBox("Database connection succeeded.", MsgBoxStyle.Information, "Connection Test")
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub ListBackupMnu_ItemClick(sender As Object, e As ItemClickEventArgs) Handles ListBackupMnu.ItemClick
        FormBackup.Icon = GetIcon()
        FormBackup.ShowDialog(Me)
    End Sub

    Private Sub ListRestoreMnu_ItemClick(sender As Object, e As ItemClickEventArgs) Handles ListRestoreMnu.ItemClick
        FormRestoreAdv.Icon = AppIcon
        FormRestoreAdv.ShowDialog(Me)
    End Sub

    Private Sub btnMnuExit_ItemClick(sender As Object, e As ItemClickEventArgs) Handles btnMnuExit.ItemClick
        Me.Close()
    End Sub

    Private Sub ListPermissionMnu_ItemClick(sender As Object, e As ItemClickEventArgs) Handles ListPermissionMnu.ItemClick
        If Not FormAccessGate.IsCurrentUserFormAccessAdmin() Then
            XtraMessageBox.Show(Me, If(Eng, "Only ADMINS group users can manage form access.", "فقط مجموعة ADMINS تدير الصلاحيات."),
                If(Eng, "Form access", "صلاحيات"), MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If
        Using fa As New FrmUserFormAccess()
            fa.Icon = AppIcon
            fa.ShowDialog(Me)
        End Using
    End Sub
    Private Sub ListChangePassMnu_ItemClick(sender As Object, e As ItemClickEventArgs) Handles ListChangePassMnu.ItemClick
        FrmChangePassword.CurrentUser = CurrentUser
        FrmChangePassword.Icon = AppIcon
        FrmChangePassword.ShowDialog(Me)
    End Sub

    Private Sub LstRestPassMnu_ItemClick(sender As Object, e As ItemClickEventArgs) Handles LstRestPassMnu.ItemClick
        Dim frm As New FrmResetPassword(CurrentUser)
        frm.Icon = AppIcon
        frm.ShowDialog(Me)
    End Sub



#End Region

#Region "Basic Data"

    Private Sub ListCitiesMnu_ItemClick(sender As Object, e As ItemClickEventArgs) Handles ListCitiesMnu.ItemClick
        Dim FR As New Frm_TblCities
        FR.Icon = AppIcon
        FR.ShowDialog(Me)
    End Sub

    Private Sub ListHealthMnu_ItemClick(sender As Object, e As ItemClickEventArgs) Handles ListHealthMnu.ItemClick
        Dim FR As New Frm_Health
        FR.Icon = AppIcon
        FR.ShowDialog(Me)
    End Sub

    Private Sub ListTreatsMnu_ItemClick(sender As Object, e As ItemClickEventArgs) Handles ListTreatsMnu.ItemClick
        Dim FR As New FrmTblTRTS
        FR.Icon = AppIcon
        FR.ShowDialog(Me)
    End Sub

    Private Sub ListWireTypeMnu_ItemClick(sender As Object, e As ItemClickEventArgs) Handles ListWireTypeMnu.ItemClick
        Dim FR As New FrmTblWireType
        FR.Icon = AppIcon
        FR.ShowDialog(Me)
    End Sub

    Private Sub ListWireMeasureMnu_ItemClick(sender As Object, e As ItemClickEventArgs) Handles ListWireMeasureMnu.ItemClick
        Dim FR As New FrmTblMeasure
        FR.Icon = AppIcon
        FR.ShowDialog(Me)
    End Sub

    Private Sub ListVisitTypeMnu_ItemClick(sender As Object, e As ItemClickEventArgs) Handles ListVisitTypeMnu.ItemClick
        Dim FR As New FrmVisitTypes
        FR.Icon = AppIcon
        FR.ShowDialog(Me)
    End Sub

    Private Sub ListRxDetailsMnu_ItemClick(sender As Object, e As ItemClickEventArgs) Handles ListRxDetailsMnu.ItemClick
        Dim FR As New FrmRxDetails
        FR.Icon = AppIcon
        FR.ShowDialog(Me)
    End Sub
    Private Sub BtnMedic_ItemClick(sender As Object, e As ItemClickEventArgs) Handles BtnMedic.ItemClick
        Try
            Dim F As New MedicFormDS 'MedicForm 'FrmDefColors 'FrmCreateDash 'FrmDiagram 'FormSettings '
            F.Icon = AppIcon
            F.ShowDialog(Me)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub BtnDashCreate_ItemClick(sender As Object, e As ItemClickEventArgs) Handles BtnDashCreate.ItemClick
        'OpenFinanceDashboardForm()
        Try
            Dim F As New FrmExchMny
            F.Icon = AppIcon
            F.ShowDialog(Me)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub BtnFinancePass_ItemClick(sender As Object, e As ItemClickEventArgs) Handles BtnFinancePass.ItemClick
        Try
            Using dlg As New FrmChngFinancePass()
                dlg.Icon = AppIcon
                dlg.ShowDialog(Me)
            End Using
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    ''' <summary>Shows FrmFinance (same as Basic Data → dashboard item). Used by menu and Ctrl+Shift+F10 (RegisterHotKey).</summary>
    Public Sub OpenFinanceDashboardForm()
        Try
            Dim owner = Form.ActiveForm
            If owner Is Nothing OrElse Not owner.IsHandleCreated Then owner = Me
            If Not FormAccessGate.TryEnterForm(owner, "FrmFinance") Then Return
            If Not FinanceDashboardPasswordHelper.TryUnlockFinanceDashboard(owner) Then Return
            Dim F As New FrmFinance
            F.Icon = AppIcon
            F.ShowDialog(owner)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub btnRxFly_ItemClick(sender As Object, e As ItemClickEventArgs) Handles btnRxFly.ItemClick
        Try
            Dim F As New PatientRXFlyFrm 'FrmDiagram 'FormSettings ''MedicForm 'FrmDefColors 'FrmTrtDash
            F.Icon = AppIcon
            F.ShowDialog(Me)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub btnListLabs_Click(sender As Object, e As EventArgs) Handles btnListLabs.Click
        FrmLab.Icon = AppIcon
        FrmLab.ShowDialog(Me)
    End Sub

    Private Sub btnLabOrder_Click(sender As Object, e As EventArgs) Handles btnLabOrder.Click
        FrmLabOrder2.Icon = AppIcon
        FrmLabOrder2.ShowDialog(Me)
    End Sub

    Private Sub btnRecieveOrder_Click(sender As Object, e As EventArgs) Handles btnRecieveOrder.Click
        FrmRecieveLabOrder.Icon = AppIcon
        FrmRecieveLabOrder.ShowDialog(Me)
    End Sub

    Private Sub btnLabPay_Click(sender As Object, e As EventArgs) Handles btnLabPay.Click
        FrmLabPay.Icon = AppIcon
        FrmLabPay.ShowDialog(Me)

    End Sub

    Private Sub btnListPatients_Click(sender As Object, e As EventArgs) Handles btnListPatients.Click
        FrmPatient.Icon = AppIcon
        FrmPatient.ShowDialog(Me)
    End Sub

    Private Sub btnPatientsDebts_Click(sender As Object, e As EventArgs) Handles btnPatientsDebts.Click
        FrmPatientDebts.Icon = AppIcon
        FrmPatientDebts.ShowDialog(Me)
    End Sub

    Private Sub RibMenuDocs_Click(sender As Object, e As EventArgs) Handles RibMenuDocs.Click
        FrmDoctors.Icon = AppIcon
        FrmDoctors.ShowDialog(Me)
    End Sub

    Private Sub btnSecretaries_Click(sender As Object, e As EventArgs) Handles btnSecretaries.Click
        FrmSecretaries.Icon = AppIcon
        FrmSecretaries.ShowDialog(Me)
    End Sub

    Private Sub btnEmployees_Click(sender As Object, e As EventArgs) Handles btnEmployees.Click
        FrmEmp.Icon = AppIcon
        FrmEmp.ShowDialog(Me)
    End Sub

    Private Sub btnAbout_ItemClick(sender As Object, e As ItemClickEventArgs) Handles btnAbout.ItemClick
        FormAbout.Icon = GetIcon()
        FormAbout.ShowDialog(Me)
    End Sub

    Private Sub btnClinicInfo_ItemClick(sender As Object, e As ItemClickEventArgs) Handles btnClinicInfo.ItemClick
        FrmClinic.Icon = GetIcon()
        FrmClinic.ShowDialog(Me)
    End Sub

    Private Sub BtnListVendors_Click(sender As Object, e As EventArgs) Handles BtnListVendors.ItemClick
        StockHubForm.Icon = GetIcon()
        StockHubForm.ShowDialog(Me)
    End Sub

    Private Sub btnCheuqes_ItemClick(sender As Object, e As ItemClickEventArgs) Handles btnCheuqes.ItemClick
        Try
            Frm5DueChqs.Icon = GetIcon()
            Frm5DueChqs.ShowDialog(Me)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

#Region "Whats"


    Private Sub btnWhatsApp_ItemClick(sender As Object, e As ItemClickEventArgs) Handles btnWhatsApp.ItemClick
        Try
            Using hub As New FrmWhatsHub()
                hub.ShellIcon = GetIcon()
                hub.ShowDialog(Me)
            End Using
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub btnApptSend_ItemClick(sender As Object, e As ItemClickEventArgs) Handles btnApptSend.ItemClick
        Try
            'Dim fr As New FrmSchedulerFull 'FrmAdminAppt() '
            FrmApptsWhats.Icon = AppIcon
            FrmApptsWhats.ShowDialog(Me)

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub btnWhatsAppActivityLog_ItemClick(sender As Object, e As ItemClickEventArgs) Handles btnWhatsAppActivityLog.ItemClick
        FrmWhatsAppActivityLog.ShowArchive(Me)
    End Sub
    Private Sub btnApptsReminder_ItemClick(sender As Object, e As ItemClickEventArgs) Handles btnApptsReminder.ItemClick
        Try
            FrmApptsReminder.ShowQueue(Me)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub btnAccountWhats_ItemClick(sender As Object, e As ItemClickEventArgs) Handles btnAccountWhats.ItemClick
        Try
            Dim F As New FrmAccountWhats
            F.Icon = AppIcon
            F.ShowDialog(Me)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub btnAccountReminder_ItemClick(sender As Object, e As ItemClickEventArgs) Handles btnAccountReminder.ItemClick
        Try
            Dim F As New FrmAccountReminderSchedule
            F.Icon = AppIcon
            F.ShowDialog(Me)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

#End Region

    Private Sub btnSettings_ItemClick(sender As Object, e As ItemClickEventArgs) Handles btnSettings.ItemClick
        Try
            Dim F As New FrmNewSettings
            F.Icon = AppIcon
            F.ShowDialog(Me)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub btnStaffMange_ItemClick(sender As Object, e As ItemClickEventArgs) Handles btnStaffMange.ItemClick
        Try
            Dim F As New StaffHubForm
            F.Icon = AppIcon
            F.ShowDialog(Me)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub btnLast10Patients_ItemClick(sender As Object, e As ItemClickEventArgs) Handles btnLast10Patients.ItemClick
        Try
            Dim F As New FrmLastPatients
            F.Icon = AppIcon
            F.ShowDialog(Me)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub TodayButton_ItemClick(sender As Object, e As ItemClickEventArgs) Handles TodayButton.ItemClick
        Try
            Dim F As New TodayApptEditorForm
            F.Icon = AppIcon
            F.ShowDialog(Me)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub btnScheduler_ItemClick(sender As Object, e As ItemClickEventArgs) Handles btnScheduler.ItemClick
        ShowModernSchedulerForm()
    End Sub

    Private Sub ShowModernSchedulerForm()
        Try
            EnterModalSchedulerSession()
            Dim F As New FrmSchedulerFull
            F.Icon = AppIcon
            F.ShowDialog(Me)
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            ExitModalSchedulerSession()
        End Try
    End Sub

    Friend Function ShouldDeferSchedulerBackgroundWork() As Boolean
        Return _modalSchedulerSessionDepth > 0 OrElse IsEmbeddedSchedulerHostActive()
    End Function

    Private Function IsEmbeddedSchedulerHostActive() As Boolean
        Try
            Dim ws = FormManager.Instance.CurrentForm
            If ws Is Nothing OrElse ws.IsDisposed OrElse ws.BodyPanel Is Nothing OrElse ws.BodyPanel.Controls.Count = 0 Then Return False
            Dim activeBody = ws.BodyPanel.Controls(0)
            Return TypeOf activeBody Is ApptHostCtl OrElse TypeOf activeBody Is SchedulerNew
        Catch
            Return False
        End Try
    End Function

    Private Sub EnterModalSchedulerSession()
        _modalSchedulerSessionDepth += 1
        SchedulerSnapshotAutoSendService.PushUiSuppression()
    End Sub

    Private Sub ExitModalSchedulerSession()
        If _modalSchedulerSessionDepth > 0 Then
            _modalSchedulerSessionDepth -= 1
        End If
        SchedulerSnapshotAutoSendService.PopUiSuppression(shouldRequestImmediateRun:=_modalSchedulerSessionDepth = 0)
    End Sub

    Private Sub btnSnapshotSender_ItemClick(sender As Object, e As ItemClickEventArgs) Handles btnSnapshotSender.ItemClick
        Try
            Using f As New SnapShotSender()
                f.Icon = AppIcon
                f.ShowDialog(Me)
            End Using
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub















#End Region

#Region "Form access shell"

    Public Sub ApplyFormAccessShell()
        If FluentFormDefaultManager1 Is Nothing Then Return
        For Each bi In FluentFormDefaultManager1.Items
            Dim btn = TryCast(bi, BarButtonItem)
            If btn Is Nothing Then Continue For
            If String.Equals(btn.Name, "ListPermissionMnu", StringComparison.OrdinalIgnoreCase) Then
                btn.Enabled = (CurrentUser IsNot Nothing AndAlso FormAccessGate.IsCurrentUserFormAccessAdmin())
                Continue For
            End If
            If String.Equals(btn.Name, "ListChangePassMnu", StringComparison.OrdinalIgnoreCase) Then
                btn.Enabled = (CurrentUser IsNot Nothing)
                Continue For
            End If
            If String.Equals(btn.Name, "LstRestPassMnu", StringComparison.OrdinalIgnoreCase) Then
                btn.Enabled = (CurrentUser IsNot Nothing AndAlso FormAccessGate.IsCurrentUserFormAccessAdmin())
                Continue For
            End If
            If String.Equals(btn.Name, "ListUsersMnu", StringComparison.OrdinalIgnoreCase) OrElse
                String.Equals(btn.Name, "ListAddNewUsrGrpMnu", StringComparison.OrdinalIgnoreCase) Then
                btn.Enabled = (CurrentUser IsNot Nothing AndAlso FormAccessGate.IsCurrentUserFormAccessAdmin())
                Continue For
            End If
            Dim fn = MainViewFormAccessMap.TryGetFormForBarItem(btn.Name)
            If fn IsNot Nothing Then
                btn.Enabled = (CurrentUser Is Nothing OrElse FormAccessGate.CanOpenForm(fn))
            End If
        Next
        If MainAccordion IsNot Nothing Then
            For Each root As DevExpress.XtraBars.Navigation.AccordionControlElement In MainAccordion.Elements
                ApplyFormAccessAccordionRecursive(root)
            Next
        End If
    End Sub

    Private Sub ApplyFormAccessAccordionRecursive(el As DevExpress.XtraBars.Navigation.AccordionControlElement)
        For Each c As DevExpress.XtraBars.Navigation.AccordionControlElement In el.Elements
            Dim fn = MainViewFormAccessMap.TryGetFormForAccordion(c.Name)
            If fn IsNot Nothing Then
                c.Enabled = (CurrentUser Is Nothing OrElse FormAccessGate.CanOpenForm(fn))
            End If
            ApplyFormAccessAccordionRecursive(c)
        Next
    End Sub

#End Region

#Region "WhatsApp session message center"

    Private Const WhatsSessionMessageMax As Integer = 500

    Private Sub InitializeWhatsAppMessageCenter()
        EnsureMsgCenterReloadButtonCreated()
        ApplyMessageCenterLocalizedTextsAndFonts()
        lblMsgCenterTitle.AutoSizeMode = LabelAutoSizeMode.None
        lblMsgCenterTitle.Appearance.TextOptions.WordWrap = True
        lblMsgCenterTitle.Appearance.TextOptions.VAlignment = VertAlignment.Top
        lblMsgCenterTitle.Appearance.TextOptions.HAlignment = If(Eng, HorzAlignment.Near, HorzAlignment.Far)
        AddHandler MsgCenterToolbarPanel.SizeChanged, AddressOf MsgCenterToolbarPanel_SizeChanged
        AddHandler txtMsgCenterSearch.TextChanged, AddressOf WhatsMsgCenterFilterChanged
        AddHandler txtMsgCenterSearch.KeyDown, AddressOf WhatsMsgCenterSearch_KeyDown
        AddHandler chkMsgCenterFailuresOnly.CheckedChanged, AddressOf WhatsMsgCenterFilterChanged
        AddHandler btnMsgCenterClear.Click, AddressOf BtnMsgCenterClear_Click
        AddHandler btnMsgCenterOpenLog.Click, AddressOf BtnMsgCenterOpenLog_Click
        AddHandler btnMsgCenterCopyRow.Click, AddressOf BtnMsgCenterCopyRow_Click
        AddHandler flowWhatsSessionFeed.SizeChanged, AddressOf FlowWhatsSessionFeed_SizeChanged
        RelayoutMessageCenterToolbar()
        If _whatsMsgCenterCountdownTimer Is Nothing Then
            _whatsMsgCenterCountdownTimer = New System.Windows.Forms.Timer With {.Interval = 1000}
            AddHandler _whatsMsgCenterCountdownTimer.Tick, AddressOf WhatsMsgCenterCountdownTimer_Tick
        End If
        _lastWhatsQueueEnrichUtc = DateTime.MinValue
        _whatsMsgCenterCountdownTimer.Start()
        RebuildWhatsSessionFeed()
    End Sub

    Private Async Sub WhatsMsgCenterCountdownTimer_Tick(sender As Object, e As EventArgs)
        If _whatsSessionStore.Count > 0 AndAlso Not _whatsQueueEnrichInFlight Then
            If (DateTime.UtcNow - _lastWhatsQueueEnrichUtc).TotalSeconds >= 1.0R Then
                _whatsQueueEnrichInFlight = True
                _lastWhatsQueueEnrichUtc = DateTime.UtcNow
                Try
                    Await EnrichWhatsSessionRowsFromGatewayQueueAsync()
                Finally
                    _whatsQueueEnrichInFlight = False
                End Try
            End If
        End If
        If flowWhatsSessionFeed Is Nothing OrElse Not flowWhatsSessionFeed.IsHandleCreated Then Return
        For Each c As Control In flowWhatsSessionFeed.Controls
            Dim card = TryCast(c, WhatsSessionNotifCard)
            If card IsNot Nothing Then card.RefreshQueueUi()
        Next
    End Sub

    Private Async Function EnrichWhatsSessionRowsFromGatewayQueueAsync() As Task
        If _whatsSessionStore.Count = 0 Then Return
        Dim clinicId = WhatsAppService.GetCurrentClinicId()
        If String.IsNullOrWhiteSpace(clinicId) Then Return
        Dim svc As New WhatsAppService()
        Dim qr = Await svc.TryGetQueueAsync(clinicId).ConfigureAwait(True)
        Dim pending = qr.Items
        If pending Is Nothing Then pending = New List(Of PendingMessageItem)()
        If qr.HttpOk Then
            WhatsAppService.PruneSessionRowsNotInGatewayQueue(_whatsSessionStore, pending)
            WhatsAppService.TryEnrichRowsFromPendingQueue(_whatsSessionStore, pending)
        Else
            For Each r In _whatsSessionStore
                If r Is Nothing Then Continue For
                r.ListedInLiveGatewayQueue = False
                r.QueueDelaySeconds = Nothing
                r.QueueDelayBaselineUtc = Nothing
            Next
        End If
    End Function

    Private Sub RemoveWhatsSessionRowAndRebuild(row As WhatsAppActivityLogRow)
        If row Is Nothing OrElse _whatsSessionStore.Count = 0 Then Return
        Dim removed = _whatsSessionStore.RemoveAll(Function(x)
                                                       If x Is Nothing Then Return False
                                                       If row.LogId > 0 AndAlso x.LogId = row.LogId Then Return True
                                                       If Not String.IsNullOrWhiteSpace(row.QueueJobId) AndAlso
                                                         Not String.IsNullOrWhiteSpace(x.QueueJobId) AndAlso
                                                         String.Equals(x.QueueJobId, row.QueueJobId, StringComparison.OrdinalIgnoreCase) Then Return True
                                                       Return False
                                                   End Function)
        If removed <= 0 Then Return
        If _whatsCopySource IsNot Nothing AndAlso RowMatchesForRemoval(_whatsCopySource, row) Then _whatsCopySource = Nothing
        RebuildWhatsSessionFeed()
    End Sub

    Private Shared Function RowMatchesForRemoval(a As WhatsAppActivityLogRow, b As WhatsAppActivityLogRow) As Boolean
        If a Is Nothing OrElse b Is Nothing Then Return False
        If b.LogId > 0 AndAlso a.LogId = b.LogId Then Return True
        If Not String.IsNullOrWhiteSpace(b.QueueJobId) AndAlso Not String.IsNullOrWhiteSpace(a.QueueJobId) AndAlso
            String.Equals(a.QueueJobId, b.QueueJobId, StringComparison.OrdinalIgnoreCase) Then Return True
        Return False
    End Function

    Private Sub ApplyMessageCenterLocalizedTextsAndFonts()
        Dim dockCaption = If(Eng, "Message Center", "مركز الرسائل")
        If DockMessageCenter IsNot Nothing Then DockMessageCenter.Text = dockCaption
        If hideContainerRight IsNot Nothing Then hideContainerRight.Text = dockCaption

        ApplyCurrentPatientDockLocalizedTexts()

        lblMsgCenterTitle.Text = If(Eng, "WhatsApp — this session (newest first)", "واتساب — هذه الجلسة (الأحدث أولاً)")
        lblMsgCenterTitle.Appearance.Font = New Font("Calibri", 10.0F, FontStyle.Bold)
        lblMsgCenterTitle.RightToLeft = If(Eng, RightToLeft.No, RightToLeft.Yes)

        chkMsgCenterFailuresOnly.Properties.Caption = If(Eng, "Failures only", "فشل فقط")
        chkMsgCenterFailuresOnly.Properties.Appearance.Font = New Font("Calibri", 9.0F, FontStyle.Bold)
        chkMsgCenterFailuresOnly.Properties.Appearance.Options.UseFont = True

        btnMsgCenterClear.Text = If(Eng, "Clear list", "مسح القائمة")
        btnMsgCenterOpenLog.Text = If(Eng, "Full SQL log…", "سجل SQL كامل…")
        btnMsgCenterCopyRow.Text = If(Eng, "Copy message", "نسخ الرسالة")
        btnMsgCenterCopyRow.ToolTip = If(Eng, "Copies the WhatsApp text of the notification you last clicked (expand a card first, or click its header).", "ينسخ نص واتساب للتنبيه الذي نقرّته مؤخراً (وسِّع البطاقة أو انقر عنوانها).")

        Dim btnFont = New Font("Calibri", 9.0F, FontStyle.Bold)
        btnMsgCenterClear.Appearance.Font = btnFont
        btnMsgCenterClear.Appearance.Options.UseFont = True
        btnMsgCenterOpenLog.Appearance.Font = btnFont
        btnMsgCenterOpenLog.Appearance.Options.UseFont = True
        btnMsgCenterCopyRow.Appearance.Font = btnFont
        btnMsgCenterCopyRow.Appearance.Options.UseFont = True

        If _btnMsgCenterReloadTodayFromSql IsNot Nothing Then
            _btnMsgCenterReloadTodayFromSql.Text = If(Eng, "Today (SQL)", "اليوم (SQL)")
            _btnMsgCenterReloadTodayFromSql.ToolTip = If(Eng,
                "Reload rows logged today (local date). Same cap as this panel.",
                "إعادة تحميل صفوف اليوم حسب التاريخ المحلي. نفس الحد الأقصى للوحة.")
            _btnMsgCenterReloadTodayFromSql.Appearance.Font = btnFont
            _btnMsgCenterReloadTodayFromSql.Appearance.Options.UseFont = True
        End If
        If _btnMsgCenterReloadRecentFromSql IsNot Nothing Then
            _btnMsgCenterReloadRecentFromSql.Text = If(Eng, "All recent (SQL)", "كل الأحدث (SQL)")
            _btnMsgCenterReloadRecentFromSql.ToolTip = If(Eng,
                "Reload the latest rows from the log regardless of date (up to this panel's limit).",
                "إعادة تحميل أحدث الصفوف من السجل بغض النظر عن التاريخ (حتى حد اللوحة).")
            _btnMsgCenterReloadRecentFromSql.Appearance.Font = btnFont
            _btnMsgCenterReloadRecentFromSql.Appearance.Options.UseFont = True
        End If

        txtMsgCenterSearch.Font = New Font("Calibri", 9.0F, FontStyle.Bold)

        If flowWhatsSessionFeed IsNot Nothing Then
            flowWhatsSessionFeed.Font = New Font("Calibri", 9.0F, FontStyle.Bold)
        End If
    End Sub

    Private Sub EnsureMsgCenterReloadButtonCreated()
        If MsgCenterToolbarPanel Is Nothing Then Return
        If _btnMsgCenterReloadTodayFromSql Is Nothing Then
            _btnMsgCenterReloadTodayFromSql = New SimpleButton With {.Name = "btnMsgCenterReloadTodayFromSql"}
            MsgCenterToolbarPanel.Controls.Add(_btnMsgCenterReloadTodayFromSql)
            AddHandler _btnMsgCenterReloadTodayFromSql.Click, AddressOf BtnMsgCenterReloadTodayFromSql_Click
        End If
        If _btnMsgCenterReloadRecentFromSql Is Nothing Then
            _btnMsgCenterReloadRecentFromSql = New SimpleButton With {.Name = "btnMsgCenterReloadRecentFromSql"}
            MsgCenterToolbarPanel.Controls.Add(_btnMsgCenterReloadRecentFromSql)
            AddHandler _btnMsgCenterReloadRecentFromSql.Click, AddressOf BtnMsgCenterReloadRecentFromSql_Click
        End If
    End Sub

    Private Sub MsgCenterToolbarPanel_SizeChanged(sender As Object, e As EventArgs)
        RelayoutMessageCenterToolbar()
    End Sub

    Private Sub RelayoutMessageCenterToolbar()
        If MsgCenterToolbarPanel Is Nothing OrElse lblMsgCenterTitle Is Nothing Then Return
        Dim m = MsgCenterToolbarPanel
        Dim pad = 6
        Dim innerW = Math.Max(80, m.ClientSize.Width - pad * 2)

        lblMsgCenterTitle.Location = New Point(pad, pad)
        lblMsgCenterTitle.Width = innerW
        Dim tf = TextFormatFlags.WordBreak Or TextFormatFlags.TextBoxControl
        If Not Eng Then tf = tf Or TextFormatFlags.RightToLeft
        Dim titleH = TextRenderer.MeasureText(If(lblMsgCenterTitle.Text, ""), lblMsgCenterTitle.Appearance.Font, New Size(innerW, Integer.MaxValue), tf).Height + 10
        titleH = Math.Max(28, Math.Min(titleH, 72))
        lblMsgCenterTitle.Height = titleH

        Dim y = lblMsgCenterTitle.Bottom + 6
        txtMsgCenterSearch.SetBounds(pad, y, innerW, 24)

        y = txtMsgCenterSearch.Bottom + 6
        chkMsgCenterFailuresOnly.Location = New Point(pad + 2, y + 2)

        y = chkMsgCenterFailuresOnly.Bottom + 10
        Dim gap = 8
        Dim colW = Math.Max(70, (innerW - gap) \ 2)
        btnMsgCenterOpenLog.SetBounds(pad, y, colW, 26)
        btnMsgCenterClear.SetBounds(pad + colW + gap, y, innerW - colW - gap - pad, 26)

        y += 32
        If _btnMsgCenterReloadTodayFromSql IsNot Nothing AndAlso _btnMsgCenterReloadRecentFromSql IsNot Nothing Then
            _btnMsgCenterReloadTodayFromSql.SetBounds(pad, y, colW, 26)
            _btnMsgCenterReloadRecentFromSql.SetBounds(pad + colW + gap, y, innerW - colW - gap - pad, 26)
            y += 32
        End If
        btnMsgCenterCopyRow.SetBounds(pad, y, innerW, 26)

        Dim needH = btnMsgCenterCopyRow.Bottom + pad
        If m.Dock = DockStyle.Top AndAlso Math.Abs(m.Height - needH) > 2 Then
            m.Height = needH
        End If
    End Sub

    Private Sub FlowWhatsSessionFeed_SizeChanged(sender As Object, e As EventArgs)
        LayoutWhatsSessionCardWidths()
    End Sub

    Private Sub OnWhatsAppSessionMessageProcessed(sender As Object, e As WhatsAppSessionMessageEventArgs)
        If IsDisposed Then Return
        If InvokeRequired Then
            BeginInvoke(New Action(Sub() HandleWhatsSessionMessage(e)))
        Else
            HandleWhatsSessionMessage(e)
        End If
    End Sub

    Private Sub HandleWhatsSessionMessage(e As WhatsAppSessionMessageEventArgs)
        If e Is Nothing OrElse e.Row Is Nothing Then Return
        If e.RevealMessageCenterDock Then EnsureMessageCenterDockVisible()
        _whatsSessionStore.Insert(0, e.Row)
        While _whatsSessionStore.Count > WhatsSessionMessageMax
            _whatsSessionStore.RemoveAt(_whatsSessionStore.Count - 1)
        End While
        _lastWhatsQueueEnrichUtc = DateTime.MinValue
        RebuildWhatsSessionFeed()
    End Sub

    Private Sub EnsureMessageCenterDockVisible()
        If _whatsMessageCenterDockShown Then Return
        _whatsMessageCenterDockShown = True
        Try
            If hideContainerRight IsNot Nothing Then hideContainerRight.Visible = True
            If DockMessageCenter IsNot Nothing AndAlso DockMessageCenter.Visibility <> DevExpress.XtraBars.Docking.DockVisibility.AutoHide Then
                DockMessageCenter.Visibility = DevExpress.XtraBars.Docking.DockVisibility.AutoHide
            End If
        Catch
        End Try
    End Sub

    Private Function GetMsgCenterSearchText() As String
        If txtMsgCenterSearch Is Nothing Then Return ""
        Return If(txtMsgCenterSearch.Text, "").Trim()
    End Function

    Private Iterator Function EnumerateFilteredWhatsRows() As IEnumerable(Of WhatsAppActivityLogRow)
        Dim rows = _whatsSessionStore.AsEnumerable()
        If chkMsgCenterFailuresOnly IsNot Nothing AndAlso chkMsgCenterFailuresOnly.Checked Then
            rows = rows.Where(Function(r) Not r.Success)
        End If
        Dim s = GetMsgCenterSearchText()
        If s.Length > 0 Then
            Dim cmp = StringComparison.CurrentCultureIgnoreCase
            Dim cmpFast = StringComparison.OrdinalIgnoreCase
            rows = rows.Where(Function(r)
                                  Dim phone = If(r.TargetNumber, "")
                                  Dim matchesPhone = phone.IndexOf(s, cmpFast) >= 0 OrElse phone.IndexOf(s, cmp) >= 0
                                  Return matchesPhone OrElse
                                      If(r.MessagePreview, "").IndexOf(s, cmp) >= 0 OrElse
                                      If(r.PatientDisplayName, "").IndexOf(s, cmp) >= 0 OrElse
                                      If(r.Category, "").IndexOf(s, cmp) >= 0 OrElse
                                      If(r.SourceHint, "").IndexOf(s, cmp) >= 0 OrElse
                                      If(r.ClinicDisplayName, "").IndexOf(s, cmp) >= 0 OrElse
                                      If(r.ResponseOrError, "").IndexOf(s, cmp) >= 0 OrElse
                                      (r.LogId > 0 AndAlso r.LogId.ToString().IndexOf(s, cmpFast) >= 0)
                              End Function)
        End If
        For Each r In rows
            Yield r
        Next
    End Function

    Private Sub RebuildWhatsSessionFeed()
        If flowWhatsSessionFeed Is Nothing Then Return
        _whatsExpandedCard = Nothing
        flowWhatsSessionFeed.SuspendLayout()
        Try
            flowWhatsSessionFeed.Controls.Clear()
            For Each r In EnumerateFilteredWhatsRows()
                Dim card As New WhatsSessionNotifCard(
                    Me, r, Eng,
                    Sub(x) _whatsCopySource = x,
                    AddressOf OnWhatsSessionCardExpanded,
                    AddressOf RemoveWhatsSessionRowAndRebuild)
                flowWhatsSessionFeed.Controls.Add(card)
            Next
        Finally
            flowWhatsSessionFeed.ResumeLayout(True)
        End Try
        LayoutWhatsSessionCardWidths()
        flowWhatsSessionFeed.AutoScrollPosition = New Point(0, 0)
    End Sub

    Private Sub LayoutWhatsSessionCardWidths()
        If flowWhatsSessionFeed Is Nothing Then Return
        Dim w = flowWhatsSessionFeed.ClientSize.Width - flowWhatsSessionFeed.Padding.Horizontal
        If w < 80 Then w = 80
        For Each c As Control In flowWhatsSessionFeed.Controls
            Dim card = TryCast(c, WhatsSessionNotifCard)
            If card IsNot Nothing Then card.SetPreferredWidth(w)
        Next
    End Sub

    Private Sub OnWhatsSessionCardExpanded(card As WhatsSessionNotifCard)
        If card Is Nothing Then Return
        If card.IsExpanded Then
            If _whatsExpandedCard IsNot Nothing AndAlso Not ReferenceEquals(_whatsExpandedCard, card) Then
                _whatsExpandedCard.CollapseQuiet()
            End If
            _whatsExpandedCard = card
        ElseIf ReferenceEquals(_whatsExpandedCard, card) Then
            _whatsExpandedCard = Nothing
        End If
    End Sub

    Private Sub WhatsMsgCenterSearch_KeyDown(sender As Object, e As KeyEventArgs)
        If e.KeyCode = Keys.Escape AndAlso txtMsgCenterSearch IsNot Nothing Then
            txtMsgCenterSearch.Text = ""
            RebuildWhatsSessionFeed()
            e.Handled = True
        End If
    End Sub

    Private Sub OpenWhatsSessionRowDetail(row As WhatsAppActivityLogRow)
        If row.LogId > 0 Then
            FrmWhatsAppActivityLog.ShowForLogEntry(Me, row.LogId)
            Return
        End If
        Dim sb As New StringBuilder()
        sb.AppendLine(If(Eng, "Time (UTC): ", "الوقت UTC: ") & row.SentAtUtc.ToString("yyyy-MM-dd HH:mm:ss"))
        sb.AppendLine(If(Eng, "Success: ", "نجح: ") & row.Success.ToString())
        sb.AppendLine(If(Eng, "Category: ", "التصنيف: ") & If(row.Category, ""))
        sb.AppendLine(If(Eng, "Source: ", "المصدر: ") & If(row.SourceHint, ""))
        sb.AppendLine(If(Eng, "Number: ", "الرقم: ") & If(row.TargetNumber, ""))
        sb.AppendLine(If(Eng, "Message: ", "الرسالة: ") & If(row.MessagePreview, ""))
        sb.AppendLine()
        sb.AppendLine(If(Eng, "Response / error:", "الاستجابة أو الخطأ:"))
        sb.AppendLine(If(row.ResponseOrError, ""))
        XtraMessageBox.Show(sb.ToString(), If(Eng, "WhatsApp (session)", "واتساب (الجلسة)"), MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Private Sub WhatsMsgCenterFilterChanged(sender As Object, e As EventArgs)
        RebuildWhatsSessionFeed()
    End Sub

    Private Sub BtnMsgCenterClear_Click(sender As Object, e As EventArgs)
        Dim ok = XtraMessageBox.Show(Me,
            If(Eng,
                "Clear the in-memory list only (nothing is deleted from the SQL log). Refill with ""Today (SQL)"" or ""All recent (SQL)"".",
                "مسح القائمة من الذاكرة فقط (دون حذف من سجل SQL). أعد الملء بـ «اليوم (SQL)» أو «كل الأحدث (SQL)»."),
            If(Eng, "Clear session list", "مسح قائمة الجلسة"),
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Warning)
        If ok <> DialogResult.Yes Then Return
        _whatsSessionStore.Clear()
        _whatsCopySource = Nothing
        RebuildWhatsSessionFeed()
    End Sub

    Private Sub BtnMsgCenterReloadTodayFromSql_Click(sender As Object, e As EventArgs)
        ApplyWhatsSessionReloadFromRows(WhatsAppActivityLogRepository.GetRecentForLocalToday(WhatsSessionMessageMax))
    End Sub

    Private Sub BtnMsgCenterReloadRecentFromSql_Click(sender As Object, e As EventArgs)
        ApplyWhatsSessionReloadFromRows(WhatsAppActivityLogRepository.GetRecent(WhatsSessionMessageMax))
    End Sub

    Private Sub ApplyWhatsSessionReloadFromRows(rows As IList(Of WhatsAppActivityLogRow))
        Try
            _whatsSessionStore.Clear()
            If rows IsNot Nothing Then
                For Each r In rows
                    If r IsNot Nothing Then
                        WhatsAppActivityLogRow.TryApplyQueueMetadataFromResponse(r)
                        _whatsSessionStore.Add(r)
                    End If
                Next
            End If
            _whatsCopySource = Nothing
            _whatsExpandedCard = Nothing
            RebuildWhatsSessionFeed()
        Catch ex As Exception
            XtraMessageBox.Show(Me, ex.Message, If(Eng, "WhatsApp log", "سجل واتساب"), MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End Try
    End Sub

    Private Sub BtnMsgCenterOpenLog_Click(sender As Object, e As EventArgs)
        FrmWhatsAppActivityLog.ShowArchive(Me)
    End Sub

    Private Sub BtnMsgCenterCopyRow_Click(sender As Object, e As EventArgs)
        Dim row = _whatsCopySource
        If row Is Nothing Then Return
        Dim t = If(row.MessagePreview, "").Trim()
        If t.Length = 0 Then Return
        Try
            Clipboard.SetText(t)
        Catch
        End Try
    End Sub

    Private Shared ReadOnly WhatsSourceAccentPalette As Color() = {
        Color.FromArgb(52, 120, 210),
        Color.FromArgb(0, 137, 123),
        Color.FromArgb(142, 68, 173),
        Color.FromArgb(230, 126, 34),
        Color.FromArgb(92, 107, 192),
        Color.FromArgb(0, 151, 167),
        Color.FromArgb(183, 28, 28),
        Color.FromArgb(67, 160, 71)
    }

    Private Shared Function WhatsMsgCenterAccentForSource(sourceHint As String) As Color
        Dim hc = StringComparer.OrdinalIgnoreCase.GetHashCode(If(sourceHint, ""))
        If hc = Integer.MinValue Then hc = 0
        Dim idx = Math.Abs(hc) Mod WhatsSourceAccentPalette.Length
        Return WhatsSourceAccentPalette(idx)
    End Function

    Private Shared Function WhatsMsgCenterBlend(c1 As Color, c2 As Color, t As Single) As Color
        Dim u = 1.0F - t
        Return Color.FromArgb(255,
            CInt(Math.Min(255, Math.Max(0, c1.R * u + c2.R * t))),
            CInt(Math.Min(255, Math.Max(0, c1.G * u + c2.G * t))),
            CInt(Math.Min(255, Math.Max(0, c1.B * u + c2.B * t))))
    End Function

    Private Shared Function WhatsMsgCenterHeaderBackForSource(success As Boolean, sourceHint As String) As Color
        Dim accent = WhatsMsgCenterAccentForSource(sourceHint)
        ' Stronger green / red base so Sent vs Failed is obvious; source tint stays on the left strip.
        If success Then
            Return WhatsMsgCenterBlend(Color.FromArgb(210, 245, 210), accent, 0.2F)
        End If
        Return WhatsMsgCenterBlend(Color.FromArgb(255, 220, 220), accent, 0.16F)
    End Function

#End Region



    Private NotInheritable Class WhatsSessionNotifCard
        Inherits Panel

        Private Const QueueStripHeight As Integer = 30

        Private ReadOnly _host As MainView3
        Private ReadOnly _row As WhatsAppActivityLogRow
        Private ReadOnly _englishUi As Boolean
        Private ReadOnly _onSelectCopy As Action(Of WhatsAppActivityLogRow)
        Private ReadOnly _onExpandedChanged As Action(Of WhatsSessionNotifCard)
        Private ReadOnly _onRemovedFromStore As Action(Of WhatsAppActivityLogRow)

        Private ReadOnly _header As Panel
        Private ReadOnly _lblHeader As Label
        Private ReadOnly _queueStrip As Panel
        Private ReadOnly _lblCountdown As Label
        Private ReadOnly _btnDelete As Button
        Private ReadOnly _body As Panel
        Private ReadOnly _txt As TextBox
        Private ReadOnly _lblMeta As Label

        Private _expanded As Boolean

        Public ReadOnly Property IsExpanded As Boolean
            Get
                Return _expanded
            End Get
        End Property

        Public Sub New(host As MainView3, row As WhatsAppActivityLogRow, englishUi As Boolean, onSelectCopy As Action(Of WhatsAppActivityLogRow), onExpandedChanged As Action(Of WhatsSessionNotifCard), onRemovedFromStore As Action(Of WhatsAppActivityLogRow))
            _host = host
            _row = row
            _englishUi = englishUi
            _onSelectCopy = onSelectCopy
            _onExpandedChanged = onExpandedChanged
            _onRemovedFromStore = onRemovedFromStore

            BackColor = Color.White
            BorderStyle = BorderStyle.None
            Margin = New Padding(0, 0, 0, 8)
            Padding = New Padding(1)

            RightToLeft = If(englishUi, RightToLeft.No, RightToLeft.Yes)

            _header = New Panel With {.Height = 44, .Cursor = Cursors.Hand}
            _header.BackColor = WhatsMsgCenterHeaderBackForSource(row.Success, row.SourceHint)

            _lblHeader = New Label With {
                .Dock = DockStyle.Fill,
                .TextAlign = If(englishUi, ContentAlignment.MiddleLeft, ContentAlignment.MiddleRight),
                .Padding = If(englishUi, New Padding(14, 6, 10, 6), New Padding(10, 6, 14, 6)),
                .ForeColor = Color.FromArgb(32, 32, 32),
                .AutoEllipsis = True,
                .UseMnemonic = False,
                .Font = New Font("Calibri", 9.0F, FontStyle.Bold)
            }
            _lblHeader.Text = BuildHeaderText()
            AddHandler _header.Click, AddressOf Header_Click
            AddHandler _lblHeader.Click, AddressOf Header_Click
            _header.Controls.Add(_lblHeader)

            _queueStrip = New Panel With {
                .Height = QueueStripHeight,
                .BackColor = Color.FromArgb(255, 248, 225),
                .Visible = False
            }
            _lblCountdown = New Label With {
                .AutoSize = True,
                .ForeColor = Color.FromArgb(20, 60, 120),
                .Font = New Font("Calibri", 9.0F, FontStyle.Bold),
                .Visible = False,
                .BackColor = Color.FromArgb(255, 248, 225),
                .Padding = New Padding(8, 6, 4, 4)
            }
            _btnDelete = New Button With {
                .Text = If(englishUi, "Delete", "حذف"),
                .Visible = False,
                .Cursor = Cursors.Hand,
                .Width = 72,
                .Height = 24,
                .Font = New Font("Calibri", 8.5F, FontStyle.Bold),
                .FlatStyle = FlatStyle.System,
                .TabStop = True,
                .UseVisualStyleBackColor = True
            }
            AddHandler _btnDelete.Click, AddressOf BtnDelete_Click
            If englishUi Then
                _lblCountdown.Location = New Point(8, 6)
                _btnDelete.Anchor = AnchorStyles.Top Or AnchorStyles.Right
            Else
                _lblCountdown.Location = New Point(80, 6)
                _btnDelete.Anchor = AnchorStyles.Top Or AnchorStyles.Left
            End If
            _queueStrip.Controls.Add(_lblCountdown)
            _queueStrip.Controls.Add(_btnDelete)

            _body = New Panel With {
                .Visible = False,
                .BackColor = Color.FromArgb(252, 252, 252),
                .Padding = New Padding(10, 6, 10, 8)
            }

            _txt = New TextBox With {
                .BorderStyle = BorderStyle.None,
                .Multiline = True,
                .ReadOnly = True,
                .ScrollBars = ScrollBars.Vertical,
                .BackColor = _body.BackColor,
                .TabStop = False,
                .Font = New Font("Calibri", 9.0F, FontStyle.Bold),
                .WordWrap = True
            }
            _txt.Text = GetFullOrPreviewMessage().Trim()
            ApplyMessageTextDirection()

            _lblMeta = New Label With {
                .AutoSize = False,
                .Font = New Font("Calibri", 8.5F, FontStyle.Italic),
                .ForeColor = Color.FromArgb(96, 96, 96),
                .BackColor = _body.BackColor,
                .TextAlign = If(englishUi, ContentAlignment.TopLeft, ContentAlignment.TopRight),
                .UseMnemonic = False
            }
            _lblMeta.Text = BuildMetaLine()
            _lblMeta.RightToLeft = If(englishUi, RightToLeft.No, RightToLeft.Yes)

            _body.Controls.Add(_txt)
            _body.Controls.Add(_lblMeta)

            Controls.Add(_body)
            Controls.Add(_queueStrip)
            Controls.Add(_header)

            AddHandler Me.Paint, AddressOf Card_Paint
            AddHandler _queueStrip.SizeChanged, AddressOf QueueStrip_SizeChanged

            _expanded = False
            RefreshQueueUi()
            LayoutContents()
        End Sub

        Private Sub QueueStrip_SizeChanged(sender As Object, e As EventArgs)
            LayoutQueueStripInner()
        End Sub

        Private Sub LayoutQueueStripInner()
            If _queueStrip Is Nothing OrElse Not _queueStrip.Visible Then Return
            Dim pad As Integer = 8
            If _englishUi Then
                _btnDelete.Location = New Point(_queueStrip.ClientSize.Width - _btnDelete.Width - pad, 4)
            Else
                _btnDelete.Location = New Point(pad, 4)
                _lblCountdown.Location = New Point(_btnDelete.Right + 6, 6)
            End If
        End Sub

        Private Function ShouldShowLiveGatewayQueueStrip() As Boolean
            If Not _row.Success Then Return False
            If Not _row.ListedInLiveGatewayQueue Then Return False
            If String.IsNullOrWhiteSpace(_row.QueueJobId) Then Return False
            Return _row.QueueDelayBaselineUtc.HasValue AndAlso _row.QueueDelaySeconds.HasValue
        End Function

        Public Sub RefreshQueueUi()
            Dim nowUtc = DateTime.UtcNow
            Dim inQueueWindow = ShouldShowLiveGatewayQueueStrip()
            _lblCountdown.Visible = inQueueWindow
            _btnDelete.Visible = inQueueWindow
            _queueStrip.Visible = inQueueWindow
            If inQueueWindow Then
                Dim sec = WhatsAppActivityLogRow.GetQueueCountdownRemainingSeconds(_row, nowUtc)
                Dim remi = TimeSpan.FromSeconds(sec)
                _lblCountdown.Text = If(_englishUi, "Sends in ", "الإرسال خلال ") & FormatCountdown(remi)
            End If
            _lblHeader.Text = BuildHeaderText()
            LayoutQueueStripInner()
            LayoutContents()
        End Sub

        Private Function GetFullOrPreviewMessage() As String
            If Not String.IsNullOrEmpty(_row.FullMessage) Then Return _row.FullMessage
            Return If(_row.MessagePreview, "")
        End Function

        Private Sub ApplyMessageTextDirection()
            Dim rtl = WhatsHelper.MessageBodyShouldUseRtl(GetFullOrPreviewMessage())
            _txt.RightToLeft = If(rtl, RightToLeft.Yes, RightToLeft.No)
            '_txt.RightToLeftLayout = rtl
        End Sub

        Private Function FormatCountdown(REMi As TimeSpan) As String
            If REMi.TotalSeconds < 0 Then REMi = TimeSpan.Zero
            If REMi.TotalHours >= 1.0 Then
                Return $"{CInt(Math.Floor(REMi.TotalHours))}:{REMi.Minutes:D2}:{REMi.Seconds:D2}"
            End If
            Return $"{REMi.Minutes}:{REMi.Seconds:D2}"
        End Function

        Private Async Sub BtnDelete_Click(sender As Object, e As EventArgs)
            _btnDelete.Enabled = False
            Dim host = _host
            Dim rowRef = _row
            Dim english = _englishUi
            Dim ok As Boolean = False
            Dim skipFailureToast As Boolean = False
            Try
                Dim clinicId = WhatsAppService.GetCurrentClinicId()
                If String.IsNullOrWhiteSpace(clinicId) Then
                    skipFailureToast = True
                    Return
                End If
                Dim job = If(rowRef.QueueJobId, "").Trim()
                If job.Length = 0 Then
                    Dim svcQ As New WhatsAppService()
                    Dim pending = Await svcQ.GetQueueAsync(clinicId).ConfigureAwait(True)
                    WhatsAppService.TryEnrichRowsFromPendingQueue(New List(Of WhatsAppActivityLogRow) From {rowRef}, pending)
                    job = If(rowRef.QueueJobId, "").Trim()
                End If
                If job.Length = 0 Then
                    skipFailureToast = True
                    If host IsNot Nothing AndAlso host.IsHandleCreated Then
                        XtraMessageBox.Show(host,
                            If(english, "Queue id not available yet. Wait a second and try again, or open the WhatsApp form Queue tab.", "المعرّف غير جاهز بعد. انتظر ثانية وحاول، أو افتح تبويب الطابور."),
                            If(english, "WhatsApp", "واتساب"),
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information)
                    End If
                    Return
                End If
                Dim svc As New WhatsAppService()
                ok = Await svc.DeleteFromQueueAsync(clinicId, job).ConfigureAwait(True)
            Catch
                ok = False
            Finally
                If host IsNot Nothing AndAlso host.IsHandleCreated Then
                    host.BeginInvoke(Sub()
                                         _btnDelete.Enabled = True
                                         If ok Then
                                             _onRemovedFromStore(rowRef)
                                         ElseIf Not skipFailureToast Then
                                             XtraMessageBox.Show(host,
                                                 If(english, "Could not remove this message from the queue.", "تعذر حذف الرسالة من الطابور."),
                                                 If(english, "WhatsApp queue", "طابور واتساب"),
                                                 MessageBoxButtons.OK,
                                                 MessageBoxIcon.Warning)
                                         End If
                                     End Sub)
                Else
                    _btnDelete.Enabled = True
                End If
            End Try
        End Sub

        Private Sub Card_Paint(sender As Object, e As PaintEventArgs)
            Dim accent = WhatsMsgCenterAccentForSource(_row.SourceHint)
            Using b As New SolidBrush(accent)
                e.Graphics.FillRectangle(b, 0, 0, 4, Height)
            End Using
            Using p As New Pen(Color.FromArgb(210, 210, 210))
                e.Graphics.DrawRectangle(p, 0, 0, Width - 1, Height - 1)
            End Using
        End Sub

        Private Function BuildHeaderText() As String
            Dim queuedUi = ShouldShowLiveGatewayQueueStrip()
            Dim stSym As String
            Dim stWord As String
            If queuedUi Then
                stSym = "⏱"
                stWord = If(_englishUi, "Queued", "مجدولة")
            ElseIf _row.Success Then
                stSym = "✓"
                stWord = If(_englishUi, "Sent", "مُرسل")
            Else
                stSym = "✗"
                stWord = If(_englishUi, "Failed", "فشل")
            End If
            Dim t = _row.SentAtUtc.ToLocalTime().ToString("MM-dd HH:mm")
            Dim phone = If(_row.TargetNumber, "").Trim()
            Dim who = If(_row.PatientDisplayName, "").Trim()
            If who.Length > 38 Then who = who.Substring(0, 35) & "…"
            Dim snippet = If(_row.MessagePreview, "").Replace(vbCr, " ").Replace(vbLf, " ").Trim()
            If snippet.Length > 72 Then snippet = snippet.Substring(0, 69) & "…"
            Return $"{stSym} {stWord}  ·  {t}  ·  {phone}  ·  {who}  —  {snippet}"
        End Function

        Private Function BuildMetaLine() As String
            If _row.SentAfterLocalQueue Then
                Return If(_englishUi,
                    "Sent after this item left the scheduled reminder queue.",
                    "أُرسلت بعد خروجها من قائمة انتظار التذكير المجدول.")
            End If
            Return If(_englishUi,
                "Sent directly (not from the scheduled reminder queue).",
                "إرسال مباشر (ليست عبر قائمة التذكير المجدولة).")
        End Function

        Private Sub Header_Click(sender As Object, e As EventArgs)
            _onSelectCopy(_row)
            ToggleExpanded()
        End Sub

        Public Sub ToggleExpanded()
            _expanded = Not _expanded
            LayoutContents()
            _onExpandedChanged(Me)
        End Sub

        Public Sub CollapseQuiet()
            If Not _expanded Then Return
            _expanded = False
            _body.Visible = False
            LayoutContents()
            _onExpandedChanged(Me)
        End Sub

        Public Sub SetPreferredWidth(totalWidth As Integer)
            Width = totalWidth
            LayoutContents()
        End Sub

        Private Sub LayoutContents()
            _header.SetBounds(0, 0, Width, 44)
            Dim queueH = If(_queueStrip.Visible, QueueStripHeight, 0)
            If _queueStrip.Visible Then
                _queueStrip.SetBounds(0, _header.Bottom, Width, QueueStripHeight)
                LayoutQueueStripInner()
            Else
                _queueStrip.SetBounds(0, _header.Bottom, Width, 0)
            End If
            Dim bodyTop = _header.Height + queueH
            If Not _expanded Then
                _body.Visible = False
                Height = bodyTop + Padding.Vertical + 2
                Return
            End If
            _body.Visible = True
            Dim pl = _body.Padding.Left
            Dim pr = _body.Padding.Right
            Dim pt = _body.Padding.Top
            Dim pb = _body.Padding.Bottom
            Dim innerW = Math.Max(Width - pl - pr, 40)
            Dim measureW = Math.Max(innerW - 22, 32)
            Dim msgRtl = WhatsHelper.MessageBodyShouldUseRtl(GetFullOrPreviewMessage())
            Dim msgFmt = TextFormatFlags.WordBreak Or TextFormatFlags.TextBoxControl Or TextFormatFlags.NoPadding
            If msgRtl Then msgFmt = msgFmt Or TextFormatFlags.RightToLeft
            Dim textH = TextRenderer.MeasureText(If(_txt.Text, ""), _txt.Font, New Size(measureW, Integer.MaxValue), msgFmt).Height
            Const maxTextBoxDisplayH = 400
            textH = Math.Min(Math.Max(textH + 10, 96), maxTextBoxDisplayH)
            _txt.SetBounds(pl, pt, innerW, textH)
            Dim metaTop = pt + textH + 8
            Dim metaFmt = TextFormatFlags.WordBreak Or TextFormatFlags.TextBoxControl Or TextFormatFlags.NoPadding
            If Not _englishUi Then metaFmt = metaFmt Or TextFormatFlags.RightToLeft
            Dim metaH = TextRenderer.MeasureText(If(_lblMeta.Text, ""), _lblMeta.Font, New Size(measureW, Integer.MaxValue), metaFmt).Height + 6
            metaH = Math.Max(metaH, 28)
            _lblMeta.SetBounds(pl, metaTop, innerW, metaH)
            Dim bodyH = pt + textH + 8 + metaH + pb
            _body.SetBounds(0, bodyTop, Width, bodyH)
            Height = bodyTop + bodyH + Padding.Vertical + 2
        End Sub
    End Class

    Private Sub btnCsiImage_Click(sender As Object, e As EventArgs) Handles btnCsiImage.Click
        ' Common install locations: 32-bit under Program Files (x86), 64-bit under Program Files.
        Dim exes = {
            "C:\Program Files (x86)\Carestream\Patient Browser\Patient.exe",
            "C:\Program Files\Carestream\Patient Browser\Patient.exe"
        }
        Dim workDirs = {
            "C:\Program Files (x86)\Carestream\Patient Browser",
            "C:\Program Files\Carestream\Patient Browser"
        }
        For i = 0 To exes.Length - 1
            If Not File.Exists(exes(i)) Then Continue For
            Try
                Process.Start(New ProcessStartInfo With {
                    .FileName = exes(i),
                    .WorkingDirectory = workDirs(i),
                    .UseShellExecute = True
                })
                Return
            Catch ex As Exception
                XtraMessageBox.Show(Me, ex.Message,
                    If(Eng, "Carestream Patient Browser", "متصفح مريض كاريستريم"),
                    MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End Try
        Next
        XtraMessageBox.Show(Me,
            If(Eng,
                "Carestream Patient Browser was not found. Install it or check that Patient.exe exists under Program Files\Carestream\Patient Browser (or Program Files (x86)\…).",
                "لم يُعثر على Carestream Patient Browser. ثبّت البرنامج أو تحقق من وجود Patient.exe في Program Files\Carestream\Patient Browser (أو Program Files (x86)…)."),
            If(Eng, "Carestream Patient Browser", "متصفح مريض كاريستريم"),
            MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub


End Class

