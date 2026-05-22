Imports System.Collections.Concurrent
Imports System.ComponentModel
Imports System.Data
Imports System.Data.SqlClient
Imports System.Diagnostics
Imports System.Drawing
Imports System.Linq
Imports System.Threading
Imports System.Threading.Tasks
Imports System.Windows.Forms
Imports Dapper
Imports DentistX
Imports DevExpress.Utils
Imports DevExpress.XtraBars.Docking2010.Views.WindowsUI
Imports DevExpress.XtraEditors

Public Class Navigator3
    Implements IPatientHeaderControl

    Private Sub NotifyNonFatal(titleEn As String, titleAr As String, messageEn As String, messageAr As String, Optional ex As Exception = Nothing, Optional icon As MessageBoxIcon = MessageBoxIcon.Warning)
        Dim text = If(Eng, messageEn, messageAr)
        If ex IsNot Nothing AndAlso Not String.IsNullOrWhiteSpace(ex.Message) Then
            text &= vbCrLf & vbCrLf & ex.Message
        End If
        Dim caption = If(Eng, titleEn, titleAr)
        Dim iconLocal = icon
        Dim show As New Action(Sub()
                                   Try
                                       MessageBox.Show(text, caption, MessageBoxButtons.OK, iconLocal)
                                   Catch
                                   End Try
                               End Sub)
        If Me.IsDisposed Then
            show()
            Return
        End If
        Try
            If Me.IsHandleCreated AndAlso Me.InvokeRequired Then
                Me.BeginInvoke(show)
                Return
            End If
        Catch
        End Try
        Dim top = TryCast(Me.TopLevelControl, Control)
        Try
            If top IsNot Nothing AndAlso top.IsHandleCreated AndAlso top.InvokeRequired Then
                top.BeginInvoke(show)
                Return
            End If
        Catch
        End Try
        show()
    End Sub

#Region "PatientSearch"

    Public Shared ReadOnly PatientFilterAll As String = NavigatorPatientFilters.PatientFilterAll
    Public Shared ReadOnly PatientFilterTreat As String = NavigatorPatientFilters.PatientFilterTreat
    Public Shared ReadOnly PatientFilterOrtho As String = NavigatorPatientFilters.PatientFilterOrtho
    Public Shared ReadOnly PatientFilterDiag As String = NavigatorPatientFilters.PatientFilterDiag
    Public Shared ReadOnly PatientFilterMobile As String = NavigatorPatientFilters.PatientFilterMobile

    Private ReadOnly _listState As NavigatorPatientListState
    Private ReadOnly _searchCoordinator As NavigatorSearchUiCoordinator

    ''' <summary>
    ''' Full patient list from the last in-memory reload (<c>SELECT * FROM Patient</c> path). Not narrowed by Treat/Ortho/Diag/Mobile.
    ''' Same list instance the navigator mutates on insert/update/delete — treat as read-only from other types unless you own sync.
    ''' </summary>
    Friend ReadOnly Property AllPatientsSnapshot As List(Of Patient)
        Get
            Return _listState.AllPatients
        End Get
    End Property

    Private _CurrentPatientID As Integer
    Public Property PatientID As Integer
        Get
            Return _CurrentPatientID
        End Get
        Set(value As Integer)
            If _CurrentPatientID = value Then Return
            _CurrentPatientID = value
        End Set
    End Property

    Private _CurrentPatientName As String
    Public Property PatientName As String
        Get
            Return _CurrentPatientName
        End Get
        Set(value As String)
            If _CurrentPatientName = value Then Return
            _CurrentPatientName = value
        End Set
    End Property

    ''' <summary>Single search box: avoid reacting when we set text programmatically after picking a suggestion.</summary>
    Private _suppressSearchText As Boolean
    ''' <summary>Suggestion list parented directly to the top-level Form (sibling of the workspace body).
    ''' No separate window is created, so no activation / focus-steal is possible — typing in the search
    ''' box is never interrupted. Showing == Visible=True+BringToFront; hiding == Visible=False.</summary>
    Private _suggestionList As ListBox
    Private _suggestionHostForm As Form
    Private _suggestionUiReady As Boolean
    ''' <summary>Max height of the suggestion popup; list scrolls so every match is still listed.</summary>
    Private Const SuggestionPopupMaxHeight As Integer = 480
    Private _searchLeaveHideTimer As System.Windows.Forms.Timer
    ''' <summary>Skip one SelectAll after Escape from list or after committing a suggestion (Focus/Enter would otherwise select all).</summary>
    Private _ignoreSearchSelectAllOnce As Boolean
    ''' <summary>After focus enters the search box, the first left <see cref="Control.MouseClick"/> selects all text; later clicks allow normal caret/selection. Double-click always selects all.</summary>
    Private _patientSearchSelectAllOnFirstLeftClick As Boolean = True
    ''' <summary>Set before PatientBS.Position changes inside RefreshBindingListFromFiltered so PositionChanged can skip global PatientChanged (avoids LoadPatientData stealing focus while typing).</summary>
    Private _pendingSuppressPatientBroadcast As Boolean
    ''' <summary>After a non-major module switch, first user keystroke promotes search scope to ALL.</summary>
    Private _promoteSearchScopeToAllOnNextUserType As Boolean
    ''' <summary>Designer-hosted button at the right edge of txtPatientName to reopen suggestions.</summary>
    Private _showResultsButton As SimpleButton
    ''' <summary>Patients currently shown in the suggestion popup (may differ from <see cref="_bindingPatients"/> when the binding list is left empty after load).</summary>
    Private _lastSuggestionPatients As List(Of Patient)

    Private _resizeCoalesceTimer As System.Windows.Forms.Timer
    Private Const ResizeCoalesceMs As Integer = 48
    ''' <summary>Dismiss suggestion list on mouse down anywhere in navigator header outside search/dropdown/▼.</summary>
    Private _patientSuggestionHeaderClickFilter As PatientSuggestionHeaderClickFilter

    ''' <summary>Chart kid vs adult (automatic): age in (2,11) same as suggestion marker "K". Used everywhere instead of diverging rules.</summary>
    Private Shared Function NavigatorAutoKidFromPatient(p As Patient) As Boolean
        If p Is Nothing Then Return False
        Dim a = p.Age.GetValueOrDefault(0)
        Return a > 2 AndAlso a < 11
    End Function

    ''' <summary>Matches <see cref="BasePatientWorkspace.UseHdrTestModHeader"/>: False ("new" header / Navigator3 style) → park after load with empty binding;
    ''' True ("old" style) → fill binding and select first row.</summary>
    Private Function ShouldParkNoCurrentPatientAfterBackgroundLoad() As Boolean
        Return Not BasePatientWorkspace.UseHdrTestModHeader
    End Function

    ''' <summary>Workspace category filter (ALL / Treat / …). Backed by <see cref="NavigatorPatientListState"/>.</summary>
    Public Property _Target As String
        Get
            Return _listState.CategoryTarget
        End Get
        Set(value As String)
            _listState.CategoryTarget = value
        End Set
    End Property

    Private Sub ApplyFilter(Optional parkNoCurrentPatient As Boolean = False, Optional suppressPatientBroadcast As Boolean = False, Optional selectPatientIdAfterRefresh As Integer? = Nothing)
        _listState.RebuildFilteredFromAll()
        Dim raw = If(txtPatientName Is Nothing, "", txtPatientName.Text).Trim()
        Dim suppressBroadcast = suppressPatientBroadcast OrElse raw.Length > 0
        RefreshBindingListFromFiltered(If(txtPatientName Is Nothing, "", txtPatientName.Text), suppressPatientBroadcast:=suppressBroadcast, parkNoCurrentPatient:=parkNoCurrentPatient, selectPatientIdAfterRefresh:=selectPatientIdAfterRefresh)
        UpdateNavigationControls()
    End Sub

    Private Function BuildSearchFilteredPatientList(Optional searchText As String = Nothing) As List(Of Patient)
        Return _listState.BuildSearchFilteredPatientList(explicitSearchText:=searchText, fallbackBoxText:=If(txtPatientName Is Nothing, "", If(txtPatientName.Text, "")))
    End Function

    Private Sub RefreshBindingListFromFiltered(Optional searchText As String = Nothing, Optional suppressPatientBroadcast As Boolean = False, Optional parkNoCurrentPatient As Boolean = False, Optional selectPatientIdAfterRefresh As Integer? = Nothing)
        If _listState.FilteredPatients Is Nothing OrElse _bindingPatients Is Nothing Then Return
        Dim sourceList = BuildSearchFilteredPatientList(searchText)

        _pendingSuppressPatientBroadcast = suppressPatientBroadcast
        Try
            _bindingPatients.RaiseListChangedEvents = False
            _bindingPatients.Clear()
            If Not parkNoCurrentPatient Then
                For Each p In sourceList
                    _bindingPatients.Add(p)
                Next
            End If
            _bindingPatients.RaiseListChangedEvents = True
            _bindingPatients.ResetBindings()

            Dim oldPos = PatientBS.Position
            Dim newPos As Integer
            If _bindingPatients.Count = 0 Then
                newPos = -1
            ElseIf selectPatientIdAfterRefresh.HasValue Then
                newPos = -1
                For i = 0 To _bindingPatients.Count - 1
                    If _bindingPatients(i).PatientID = selectPatientIdAfterRefresh.Value Then newPos = i : Exit For
                Next
            Else
                newPos = 0
            End If
            PatientBS.Position = newPos
            If oldPos = newPos Then
                FirePatientChangedForCurrent(suppressPatientBroadcast)
            End If
        Finally
            _pendingSuppressPatientBroadcast = False
        End Try
        _listState.UpdateParkIntentAfterRefresh(parkNoCurrentPatient, _bindingPatients.Count)
        UpdateSearchResultsButtonVisibility()
        UpdateNavigationControls()
    End Sub

    Private Function HasActiveSearchQuery() As Boolean
        Return txtPatientName IsNot Nothing AndAlso Not String.IsNullOrWhiteSpace(If(txtPatientName.Text, ""))
    End Function

    Private Sub ClearCommittedSuggestionSearchContext()
        _searchCoordinator.ClearCommittedSuggestionSearchContext()
    End Sub

    Private Function ShouldUseCommittedSuggestionSearchContext(forceShow As Boolean) As Boolean
        Return _searchCoordinator.ShouldUseCommittedSuggestionSearchContext(forceShow, If(txtPatientName Is Nothing, "", If(txtPatientName.Text, "")))
    End Function

    Public Sub UpdateFilterTarget(filterTarget As String, Optional passedPatient As Patient = Nothing) Implements IPatientHeaderControl.UpdateFilterTarget
        Dim requestedTarget = If(String.IsNullOrEmpty(filterTarget), PatientFilterAll, filterTarget)
        If NavigatorPatientListState.IsMajorNavigatorFilterTarget(requestedTarget) Then
            Dim isSameMajorTarget As Boolean = String.Equals(_Target, requestedTarget, StringComparison.OrdinalIgnoreCase)
            Dim hasActiveQuery As Boolean = HasActiveSearchQuery()
            _promoteSearchScopeToAllOnNextUserType = False
            If Not (isSameMajorTarget AndAlso hasActiveQuery) Then
                Dim majorTargetChanged = Not String.Equals(_Target, requestedTarget, StringComparison.OrdinalIgnoreCase)
                _Target = requestedTarget
                Dim parkAfterSwitch = ShouldParkNoCurrentPatientAfterBackgroundLoad() AndAlso Not hasActiveQuery AndAlso majorTargetChanged
                ApplyFilter(parkNoCurrentPatient:=parkAfterSwitch)
            End If
        Else
            ' Neutral modules (Accounts / Visits / Images / Notes / …): do not keep Treat/Ortho/Diag/Mobile narrowing.
            ' Previously we only set _promoteSearchScopeToAllOnNextUserType, so Ortho→Accounts still searched 300 rows.
            _promoteSearchScopeToAllOnNextUserType = False
            Dim hasActiveQuery = HasActiveSearchQuery()
            Dim notAlreadyAll = Not String.Equals(_listState.CategoryTarget, NavigatorPatientFilters.PatientFilterAll, StringComparison.OrdinalIgnoreCase)
            If notAlreadyAll Then
                Dim cur = TryCast(PatientBS.Current, Patient)
                Dim hadSelection = cur IsNot Nothing AndAlso PatientBS.Position >= 0
                _Target = NavigatorPatientFilters.PatientFilterAll
                If hadSelection Then
                    _listState.ParkBindingEmptyUntilUserQuery = False
                    ApplyFilter(parkNoCurrentPatient:=False, selectPatientIdAfterRefresh:=cur.PatientID)
                Else
                    Dim parkAfter = ShouldParkNoCurrentPatientAfterBackgroundLoad() AndAlso Not hasActiveQuery
                    ApplyFilter(parkNoCurrentPatient:=parkAfter)
                End If
            End If
        End If
        Dim preserveSearchContext As Boolean = HasActiveSearchQuery()
        If passedPatient IsNot Nothing AndAlso Not preserveSearchContext Then
            _listState.ParkBindingEmptyUntilUserQuery = False
            If _bindingPatients.Count = 0 AndAlso _listState.FilteredPatients IsNot Nothing Then
                RefreshBindingListFromFiltered(Nothing, suppressPatientBroadcast:=False, parkNoCurrentPatient:=False, selectPatientIdAfterRefresh:=passedPatient.PatientID)
            Else
                For i As Integer = 0 To _bindingPatients.Count - 1
                    If _bindingPatients(i).PatientID = passedPatient.PatientID Then
                        PatientBS.Position = i
                        UpdateNavigationControls()
                        FirePatientChangedForCurrent()
                        Exit For
                    End If
                Next
            End If
        End If

        RefreshNavigatorShellChromeForCurrentTarget(passedPatient)
    End Sub

    Private Sub RefreshNavigatorShellChromeForCurrentTarget(Optional passedPatient As Patient = Nothing)
        Dim p = If(passedPatient IsNot Nothing, passedPatient, _currentPatient)
        If InvokeRequired Then
            BeginInvoke(New MethodInvoker(Sub() ApplyNavigatorShellStyleForTarget(p)))
        Else
            ApplyNavigatorShellStyleForTarget(p)
        End If
    End Sub

    ''' <summary>Side1 / search name field tint for Treat, Ortho, Mobile, Diag. Call when module target changes, not only on patient selection.</summary>
    Private Sub ApplyNavigatorShellStyleForTarget(Optional patientOverride As Patient = Nothing)
        If Side1 Is Nothing OrElse txtPatientName Is Nothing Then Return
        Dim p As Patient = If(patientOverride IsNot Nothing, patientOverride, _currentPatient)

        Select Case _Target
            Case "Treat"
                ResetControlBackground(Side1)
                Side1.BackColor = Color.Transparent
                ResetControlBackground(txtPatientName)
                txtPatientName.BackColor = SystemColors.Window
                If p IsNot Nothing AndAlso p.Diag.GetValueOrDefault(False) Then
                    ResetControlBackground(txtPatientName)
                    txtPatientName.BackColor = Color.FromArgb(220, 245, 255)
                End If
            Case "Ortho"
                ResetControlBackground(Side1)
                Side1.BackColor = BasePatientWorkspace.OrthoModuleShellBack
                ResetControlBackground(txtPatientName)
                txtPatientName.BackColor = SystemColors.Window
            Case "Mobile"
                ResetControlBackground(Side1)
                Side1.BackColor = Color.FromArgb(255, 232, 240)
                ResetControlBackground(txtPatientName)
                txtPatientName.BackColor = SystemColors.Window
            Case "Diag"
                ResetControlBackground(Side1)
                Side1.BackColor = BasePatientWorkspace.DiagModuleShellBack
                ResetControlBackground(txtPatientName)
                txtPatientName.BackColor = SystemColors.Window
            Case Else
                ResetControlBackground(Side1)
                Side1.BackColor = Color.Transparent
                ResetControlBackground(txtPatientName)
                txtPatientName.BackColor = SystemColors.Window
        End Select
    End Sub

    Private Sub txtPatientName_TextChanged(sender As Object, e As EventArgs) Handles txtPatientName.TextChanged
        If _suppressSearchText Then Return
        ClearCommittedSuggestionSearchContext()
        If _promoteSearchScopeToAllOnNextUserType Then
            _promoteSearchScopeToAllOnNextUserType = False
            _Target = PatientFilterAll
            ApplyFilter()
        End If
        _searchCoordinator.ScheduleDebouncedSearch()
    End Sub

    Private Sub OnSearchDebouncedTick(sender As Object, e As EventArgs)
        Dim raw = If(txtPatientName Is Nothing, "", txtPatientName.Text).Trim()
        If _listState.ParkBindingEmptyUntilUserQuery AndAlso raw.Length = 0 Then
            RefreshBindingListFromFiltered(If(txtPatientName Is Nothing, "", txtPatientName.Text), suppressPatientBroadcast:=True, parkNoCurrentPatient:=True)
            HidePatientSuggestions()
            UpdateSearchResultsButtonVisibility()
            UpdateNavigationControls()
            Return
        End If
        If raw.Length > 0 Then _listState.ParkBindingEmptyUntilUserQuery = False
        Dim suppressBroadcast = raw.Length > 0
        RefreshBindingListFromFiltered(If(txtPatientName Is Nothing, "", txtPatientName.Text), suppressPatientBroadcast:=suppressBroadcast)
        UpdateAndShowPatientSuggestions()
        UpdateSearchResultsButtonVisibility()
    End Sub

    Private Sub EnsureSearchResultsButton()
        If txtPatientName Is Nothing OrElse btnSearchResults Is Nothing Then Return
        If _showResultsButton Is btnSearchResults Then
            PositionSearchResultsButton()
            Return
        End If

        _showResultsButton = btnSearchResults
        _showResultsButton.Text = "▼"
        _showResultsButton.TabStop = False
        _showResultsButton.Visible = False
        _showResultsButton.ToolTip = If(Eng, "Show results", "إظهار النتائج")
        _showResultsButton.LookAndFeel.UseDefaultLookAndFeel = True

        AddHandler txtPatientName.SizeChanged, AddressOf txtPatientName_RepositionShowResultsButton
        AddHandler txtPatientName.LocationChanged, AddressOf txtPatientName_RepositionShowResultsButton
        PositionSearchResultsButton()
    End Sub

    Private Sub UpdateSearchResultsButtonVisibility()
        If txtPatientName Is Nothing OrElse _showResultsButton Is Nothing Then Return
        Dim hasResults As Boolean
        If ShouldUseCommittedSuggestionSearchContext(True) Then
            hasResults = _searchCoordinator.LastCommittedSearchPatients.Count > 0
        Else
            hasResults = BuildSearchFilteredPatientList(If(txtPatientName Is Nothing, "", txtPatientName.Text)).Count > 0
        End If
        _showResultsButton.Visible = hasResults
        If _showResultsButton.Visible Then
            PositionSearchResultsButton()
            _showResultsButton.BringToFront()
        End If
    End Sub

    Private Sub PositionSearchResultsButton()
        If txtPatientName Is Nothing OrElse _showResultsButton Is Nothing Then Return
        If txtPatientName.Parent Is Nothing Then Return

        Dim btnH As Integer = Math.Max(1, txtPatientName.Height)
        Dim btnW As Integer = Math.Max(14, btnH)
        Dim x As Integer = txtPatientName.Right
        Dim y As Integer = txtPatientName.Top
        _showResultsButton.SetBounds(x, y, btnW, btnH)
    End Sub

    Private Sub txtPatientName_RepositionShowResultsButton(sender As Object, e As EventArgs)
        PositionSearchResultsButton()
    End Sub

    Private Sub EnsurePatientSuggestionUi()
        If _suggestionUiReady Then Return
        Dim host = Me.FindForm()
        If host Is Nothing Then Return ' Not yet parented; try again on the next update.
        _suggestionHostForm = host
        _suggestionList = New ListBox With {
            .BorderStyle = BorderStyle.FixedSingle,
            .DrawMode = Windows.Forms.DrawMode.OwnerDrawFixed,
            .TabStop = False,
            .IntegralHeight = False,
            .Visible = False
        }
        If txtPatientName IsNot Nothing AndAlso txtPatientName.Font IsNot Nothing Then
            _suggestionList.Font = txtPatientName.Font
            _suggestionList.ItemHeight = Math.Max(17, CInt(txtPatientName.Font.Height) + 2)
        End If
        _suggestionList.RightToLeft = Me.RightToLeft
        ' Sibling of Navigator3 inside the top-level Form. Because it lives in the same window there is no
        ' activation/focus transfer when it becomes visible — typing in txtPatientName is never interrupted.
        host.Controls.Add(_suggestionList)
        AddHandler _suggestionList.DrawItem, AddressOf PatientSuggestion_DrawItem
        AddHandler _suggestionList.MouseDown, AddressOf PatientSuggestion_MouseDown
        AddHandler _suggestionList.KeyDown, AddressOf PatientSuggestion_KeyDown
        EnsurePatientSuggestionHeaderDismissFilter()
        _suggestionUiReady = True
    End Sub

    Private Sub EnsurePatientSuggestionHeaderDismissFilter()
        If _patientSuggestionHeaderClickFilter IsNot Nothing Then Return
        _patientSuggestionHeaderClickFilter = New PatientSuggestionHeaderClickFilter(Me)
        Application.AddMessageFilter(_patientSuggestionHeaderClickFilter)
    End Sub

    Private Sub RemovePatientSuggestionHeaderDismissFilter()
        If _patientSuggestionHeaderClickFilter Is Nothing Then Return
        Try
            Application.RemoveMessageFilter(_patientSuggestionHeaderClickFilter)
        Catch ex As Exception
            NotifyNonFatal("Navigator", "التنقل",
                "The application could not detach the patient search click handler. If clicks feel wrong after closing this window, restart the program.",
                "تعذر فصل معالج نقرات البحث عن المريض. إذا استمرت مشكلة في النقر بعد إغلاق هذه النافذة، أعد تشغيل البرنامج.",
                ex)
        End Try
        _patientSuggestionHeaderClickFilter = Nothing
    End Sub

    ''' <summary>When suggestions are visible, a mouse click anywhere outside search box / list / ▼ (and flyout panel) hides the dropdown.</summary>
    Friend Sub TryDismissPatientSuggestionsFromHeaderMouse(ByRef m As Message)
        Const WM_LBUTTONDOWN As Integer = &H201
        Const WM_RBUTTONDOWN As Integer = &H204
        If m.Msg <> WM_LBUTTONDOWN AndAlso m.Msg <> WM_RBUTTONDOWN Then Return
        If IsDisposed OrElse Not IsHandleCreated Then Return
        If _suggestionList Is Nothing OrElse Not _suggestionList.Visible Then Return
        Dim pt = Control.MousePosition
        If PatientSuggestionDismissMouseExempt(pt) Then Return
        HidePatientSuggestions()
    End Sub

    Private Function PatientSuggestionDismissMouseExempt(screenPt As Point) As Boolean
        If ScreenRectContainsControl(txtPatientName, screenPt) Then Return True
        If ScreenRectContainsControl(_suggestionList, screenPt) Then Return True
        If ScreenRectContainsControl(btnSearchResults, screenPt) Then Return True
        If FlyoutPatientInfoDismissExempt(screenPt) Then Return True
        Return False
    End Function

    Private Function FlyoutPatientInfoDismissExempt(screenPt As Point) As Boolean
        'If FlyoutPatientInfo Is Nothing OrElse FlyoutPatientInfo.IsDisposed OrElse Not FlyoutPatientInfo.Visible Then Return False
        'Try
        '    Return ScreenRectContainsControl(FlyoutPatientInfo, screenPt)
        'Catch ex As Exception
        '    ' No user dialog: runs on every application-wide mouse message; returning False is safe.
        '    Return False
        'End Try
    End Function

    Private Shared Function ScreenRectContainsControl(ctl As Control, screenPt As Point) As Boolean
        If ctl Is Nothing OrElse Not ctl.IsHandleCreated OrElse Not ctl.Visible Then Return False
        Try
            Return ctl.RectangleToScreen(New Rectangle(0, 0, ctl.Width, ctl.Height)).Contains(screenPt)
        Catch ex As Exception
            ' No user dialog: hot path for hit-testing during suggestion dismissal.
            Return False
        End Try
    End Function

    Private Shared Function IsKidSuggestionPatient(p As Patient) As Boolean
        Return NavigatorAutoKidFromPatient(p)
    End Function

    Private Shared Function GetPatientSuggestionAgeMarker(p As Patient) As String
        Return If(IsKidSuggestionPatient(p), "K", "A")
    End Function

    Private Shared Function GetPatientSuggestionColor(p As Patient) As Color
        Return If(IsKidSuggestionPatient(p), Color.Red, Color.Blue)
    End Function

    ''' <summary>Matches search suggestion list: kid when age is between 3 and 10 → red; otherwise blue; no patient → default text color.</summary>
    Private Sub ApplyPopupPatientAgeForeColor(Optional p As Patient = Nothing)
        If PopupPatient Is Nothing Then Return
        PopupPatient.Properties.Appearance.Options.UseForeColor = True
        If p Is Nothing Then
            PopupPatient.Properties.Appearance.ForeColor = SystemColors.ControlText
        Else
            PopupPatient.Properties.Appearance.ForeColor = GetPatientSuggestionColor(p)
        End If
    End Sub

    Private Shared Function FormatPatientSuggestionLine(p As Patient) As String
        If p Is Nothing Then Return ""
        Dim namePart = If(p.PatientName, "").Trim()
        Dim numPart = If(p.PatientNumber, "").Trim()
        Dim markerPart = "    " & GetPatientSuggestionAgeMarker(p)
        If numPart.Length > 0 Then Return namePart '& "    [#" & numPart & "]" & markerPart
        Return namePart '& markerPart
    End Function

    Private Sub PatientSuggestion_DrawItem(sender As Object, e As DrawItemEventArgs)
        If e.Index < 0 OrElse _suggestionList Is Nothing OrElse e.Index >= _suggestionList.Items.Count Then Return

        e.DrawBackground()

        Dim p As Patient = Nothing
        If _lastSuggestionPatients IsNot Nothing AndAlso e.Index < _lastSuggestionPatients.Count Then
            p = _lastSuggestionPatients(e.Index)
        End If

        Dim text = If(_suggestionList.Items(e.Index), "").ToString()
        Dim textColor = GetPatientSuggestionColor(p)
        Dim flags = TextFormatFlags.VerticalCenter Or TextFormatFlags.EndEllipsis
        If _suggestionList.RightToLeft = RightToLeft.Yes Then flags = flags Or TextFormatFlags.RightToLeft
        TextRenderer.DrawText(e.Graphics, text, e.Font, e.Bounds, textColor, flags)

        e.DrawFocusRectangle()
    End Sub

    Private Sub UpdateAndShowPatientSuggestions(Optional forceShow As Boolean = False)
        If txtPatientName Is Nothing Then Return
        Dim q = If(txtPatientName.Text, "").Trim()
        Dim displayList As List(Of Patient)
        If ShouldUseCommittedSuggestionSearchContext(forceShow) Then
            q = If(_searchCoordinator.LastCommittedSearchText, "").Trim()
            displayList = New List(Of Patient)(_searchCoordinator.LastCommittedSearchPatients)
        Else
            displayList = BuildSearchFilteredPatientList(If(txtPatientName Is Nothing, "", txtPatientName.Text))
        End If
        If (Not forceShow AndAlso q.Length = 0) OrElse displayList.Count = 0 Then
            HidePatientSuggestions()
            Return
        End If
        _lastSuggestionPatients = displayList
        EnsurePatientSuggestionUi()
        If _suggestionList Is Nothing OrElse _suggestionHostForm Is Nothing Then Return
        Dim cnt = displayList.Count
        _suggestionList.BeginUpdate()
        Try
            _suggestionList.Items.Clear()
            For i = 0 To cnt - 1
                _suggestionList.Items.Add(FormatPatientSuggestionLine(displayList(i)))
            Next
        Finally
            _suggestionList.EndUpdate()
        End Try
        Dim rowH = Math.Max(_suggestionList.ItemHeight, 17)
        Dim workingH = Screen.FromControl(txtPatientName).WorkingArea.Height
        Dim maxH = Math.Min(SuggestionPopupMaxHeight, Math.Max(160, workingH * 2 \ 5))
        Dim naturalH = cnt * rowH + 4
        Dim h = Math.Min(naturalH, maxH)
        If h < rowH + 4 Then h = rowH + 4
        ' Anchor exactly below txtPatientName using both edges (RTL-safe / mirrored-layout safe).
        Dim searchRectScreen As Rectangle = txtPatientName.RectangleToScreen(New Rectangle(0, 0, txtPatientName.Width, txtPatientName.Height))
        Dim hostLeftBottom As Point = _suggestionHostForm.PointToClient(New Point(searchRectScreen.Left, searchRectScreen.Bottom))
        Dim hostRightBottom As Point = _suggestionHostForm.PointToClient(New Point(searchRectScreen.Right, searchRectScreen.Bottom))
        Dim x As Integer = Math.Min(hostLeftBottom.X, hostRightBottom.X)
        Dim y As Integer = Math.Min(hostLeftBottom.Y, hostRightBottom.Y)
        Dim w As Integer = Math.Max(1, Math.Abs(hostRightBottom.X - hostLeftBottom.X))
        _suggestionList.Size = New Size(w, h)
        _suggestionList.Location = New Point(x, y)
        _suggestionList.Enabled = True
        _suggestionList.BringToFront()
        If Not _suggestionList.Visible Then
            _suggestionList.Visible = True
        End If
    End Sub

    Private Sub HardHideSuggestionOverlay()
        _lastSuggestionPatients = Nothing
        If _suggestionList Is Nothing Then Return
        Try
            _suggestionList.Visible = False
            _suggestionList.Enabled = False
            _suggestionList.Size = New Size(1, 1)
            If _suggestionHostForm IsNot Nothing AndAlso Not _suggestionHostForm.IsDisposed Then
                _suggestionList.SendToBack()
            End If
        Catch
        End Try
    End Sub

    Private Sub HidePatientSuggestions()
        HardHideSuggestionOverlay()
    End Sub

    Private Sub ShowResultsButton_Click(sender As Object, e As EventArgs) Handles btnSearchResults.Click
        If _showResultsButton Is Nothing Then Return
        UpdateAndShowPatientSuggestions(forceShow:=True)
        txtPatientName.Focus()
    End Sub

    Private Sub ClearPatientSearchBox()
        If txtPatientName Is Nothing Then Return
        _searchCoordinator.StopDebouncedSearch()
        HidePatientSuggestions()
        ClearCommittedSuggestionSearchContext()
        _suppressSearchText = True
        Try
            txtPatientName.Text = ""
        Finally
            _suppressSearchText = False
        End Try
        UpdateSearchResultsButtonVisibility()
    End Sub

    Private Sub PatientSuggestion_MouseDown(sender As Object, e As MouseEventArgs)
        If e.Button <> MouseButtons.Left OrElse _suggestionList Is Nothing Then Return
        Dim idx = _suggestionList.IndexFromPoint(e.Location)
        If idx < 0 Then Return
        If _lastSuggestionPatients Is Nothing OrElse idx >= _lastSuggestionPatients.Count Then Return
        CommitSuggestionAt(idx)
    End Sub

    Private Sub PatientSuggestion_KeyDown(sender As Object, e As KeyEventArgs)
        If e.KeyCode = Keys.Enter Then
            e.Handled = True
            e.SuppressKeyPress = True
            If _suggestionList Is Nothing OrElse _suggestionList.SelectedIndex < 0 Then Return
            CommitSuggestionAt(_suggestionList.SelectedIndex)
        ElseIf e.KeyCode = Keys.Escape Then
            e.Handled = True
            e.SuppressKeyPress = True
            HidePatientSuggestions()
            _ignoreSearchSelectAllOnce = True
            _patientSearchSelectAllOnFirstLeftClick = False
            txtPatientName.Focus()
            BeginInvoke(New Action(Sub()
                                       BeginInvoke(New Action(Sub() _ignoreSearchSelectAllOnce = False))
                                   End Sub))
        End If
    End Sub

    Private Function ResolveSuggestionPatientAt(index As Integer) As Patient
        If index < 0 Then Return Nothing
        If _lastSuggestionPatients IsNot Nothing AndAlso index < _lastSuggestionPatients.Count Then
            Return _lastSuggestionPatients(index)
        End If
        If _bindingPatients IsNot Nothing AndAlso index < _bindingPatients.Count Then
            Return _bindingPatients(index)
        End If
        Dim m = BuildSearchFilteredPatientList(If(txtPatientName Is Nothing, "", txtPatientName.Text))
        If index < m.Count Then Return m(index)
        Return Nothing
    End Function

    Private Sub CommitSuggestionAt(index As Integer)
        _listState.ParkBindingEmptyUntilUserQuery = False
        Dim p = ResolveSuggestionPatientAt(index)
        If p Is Nothing Then Return
        Dim useCommittedContext = ShouldUseCommittedSuggestionSearchContext(True) AndAlso _lastSuggestionPatients IsNot Nothing
        Dim searchText = If(useCommittedContext, If(_searchCoordinator.LastCommittedSearchText, ""), If(txtPatientName Is Nothing, "", txtPatientName.Text))
        Dim searchPatients = If(useCommittedContext, New List(Of Patient)(_lastSuggestionPatients), BuildSearchFilteredPatientList(searchText))
        RefreshBindingListFromFiltered(searchText, suppressPatientBroadcast:=False, parkNoCurrentPatient:=False, selectPatientIdAfterRefresh:=p.PatientID)
        HidePatientSuggestions()
        _searchCoordinator.LastCommittedSearchText = searchText
        _searchCoordinator.LastCommittedSearchPatients = searchPatients
        _searchCoordinator.LastCommittedSuggestionPatientName = If(p.PatientName, "")
        _suppressSearchText = True
        Try
            txtPatientName.Text = If(p.PatientName, "")
        Finally
            _suppressSearchText = False
        End Try
        _ignoreSearchSelectAllOnce = True
        _patientSearchSelectAllOnFirstLeftClick = False
        txtPatientName.Focus()
        BeginInvoke(New Action(Sub()
                                   BeginInvoke(New Action(Sub() _ignoreSearchSelectAllOnce = False))
                               End Sub))
    End Sub

    Private Sub txtPatientName_KeyDown(sender As Object, e As KeyEventArgs) Handles txtPatientName.KeyDown
        If e.KeyCode = Keys.Down Then
            ' Move highlight/focus into the live suggestion list so user can arrow-navigate.
            If _suggestionList IsNot Nothing AndAlso _suggestionList.Visible AndAlso _suggestionList.Items.Count > 0 Then
                _suggestionList.Focus()
                _suggestionList.SelectedIndex = 0
                e.Handled = True
                e.SuppressKeyPress = True
            End If
        ElseIf e.KeyCode = Keys.Escape Then
            HidePatientSuggestions()
        ElseIf e.KeyCode = Keys.Enter Then
            ' Commit the top/first match when the user has narrowed by typing or the nav list is already filled.
            ' With an empty search and a parked (empty) binding list, Enter must not pick the first DB patient.
            Dim m = BuildSearchFilteredPatientList(If(txtPatientName Is Nothing, "", txtPatientName.Text))
            Dim qEnter = If(txtPatientName Is Nothing, "", txtPatientName.Text).Trim()
            If m.Count > 0 AndAlso (qEnter.Length > 0 OrElse (_bindingPatients IsNot Nothing AndAlso _bindingPatients.Count > 0)) Then
                CommitSuggestionAt(0)
                e.Handled = True
                e.SuppressKeyPress = True
            End If
        End If
    End Sub

    Private Sub txtPatientName_Leave(sender As Object, e As EventArgs) Handles txtPatientName.Leave
        ' Small defer so a click on the suggestion list (which briefly moves focus off txtPatientName to the
        ' list inside the non-activating popup) does not count as "user really left the search box".
        If Me.IsDisposed OrElse Not Me.IsHandleCreated Then Return
        If _searchLeaveHideTimer Is Nothing Then
            _searchLeaveHideTimer = New System.Windows.Forms.Timer With {.Interval = 180}
            AddHandler _searchLeaveHideTimer.Tick, AddressOf OnSearchLeaveHideTimerTick
        End If
        _searchLeaveHideTimer.Stop()
        _searchLeaveHideTimer.Start()
    End Sub

    Private Sub OnSearchLeaveHideTimerTick(sender As Object, e As EventArgs)
        If _searchLeaveHideTimer IsNot Nothing Then _searchLeaveHideTimer.Stop()
        If Me.IsDisposed OrElse Not Me.IsHandleCreated Then Return
        If txtPatientName IsNot Nothing AndAlso txtPatientName.ContainsFocus Then Return
        If _suggestionList IsNot Nothing AndAlso _suggestionList.Focused Then Return
        'If FlyoutPatientInfo IsNot Nothing AndAlso FlyoutPatientInfo.Visible Then Return
        HidePatientSuggestions()
        ' User really left the search field; sync workspace to current patient (typing suppressed PatientChanged).
        FirePatientChangedForCurrent(False)
    End Sub

    ''' <summary>When nav selection changes: update state and labels (not txtPatientName so user can type to search), set Kid from new patient, optionally raise PatientChanged.</summary>
    ''' <param name="suppressBroadcast">Skip global PatientChanged (used while typing in search to avoid workspace reloading body and stealing focus).</param>
    Private Sub FirePatientChangedForCurrent(Optional suppressBroadcast As Boolean = False)
        Dim p As Patient = TryCast(PatientBS.Current, Patient)
        If p Is Nothing Then
            ' No row (e.g. search returned 0 matches): clear current patient so PopupPatient / flyout are not stale.
            _currentPatient = Nothing
            _CurrentPatientID = 0
            _CurrentPatientName = Nothing
            lblPatientNum.Text = ""
            LabelAddress.Text = ""
            LabelAge.Text = ""
            LabelBal.Text = "0.0"
            LabelBal.ForeColor = Color.Black
            If PopupPatient IsNot Nothing Then
                PopupPatient.Text = ""
                ApplyPopupPatientAgeForeColor(Nothing)
            End If
            If Not suppressBroadcast Then
                PatientEventManager.RaisePatientChanged(Nothing, False, False)
            End If
            Return
        End If
        _CurrentPatientID = p.PatientID
        _CurrentPatientName = p.PatientName
        _currentPatient = p
        ' Update labels/balance only – do NOT set txtPatientName so the search box stays editable
        UpdateBoundControlsExceptName(p)
        ' Update Kid/chart state for new patient and raise event with correct IsKid
        UpdateKidStateOnly(p)
        If Not suppressBroadcast Then
            PatientEventManager.RaisePatientChanged(p, Kid, False)
        End If
    End Sub

    Private Function LoadAllPatientsFromDb() As List(Of Patient)
        Using conn As New SqlConnection(connectionString)
            Return conn.Query(Of Patient)("SELECT * FROM Patient").ToList()
        End Using
    End Function

    Public Function GetPatientID(patientName As String) As Integer
        If String.IsNullOrEmpty(patientName) Then Return -1
        Using conn As New SqlConnection(connectionString)
            Dim id As Integer? = conn.QuerySingleOrDefault(Of Integer?)("SELECT PatientID FROM Patient WHERE PatientName = @n", New With {.n = patientName})
            Return If(id.HasValue, id.Value, -1)
        End Using
    End Function

    Public Function GetPatientName(patientID As Integer) As String
        Using conn As New SqlConnection(connectionString)
            Return conn.QuerySingleOrDefault(Of String)("SELECT PatientName FROM Patient WHERE PatientID = @id", New With {.id = patientID})
        End Using
    End Function

    Public Sub SetCurrentPatientName(patientID As Integer)
        Me.PatientID = patientID
        If _bindingPatients Is Nothing Then Return
        If _bindingPatients.Count = 0 AndAlso _listState.FilteredPatients IsNot Nothing Then
            RefreshBindingListFromFiltered(Nothing, suppressPatientBroadcast:=False, parkNoCurrentPatient:=False, selectPatientIdAfterRefresh:=patientID)
            Return
        End If
        For i As Integer = 0 To _bindingPatients.Count - 1
            If _bindingPatients(i).PatientID = patientID Then PatientBS.Position = i : Exit For
        Next
    End Sub

    Public Sub SetCurrentPatientID(patientName As String)
        PatientID = GetPatientID(patientName)
        If _bindingPatients Is Nothing Then Return
        If PatientID <= 0 Then Return
        If _bindingPatients.Count = 0 AndAlso _listState.FilteredPatients IsNot Nothing Then
            RefreshBindingListFromFiltered(Nothing, suppressPatientBroadcast:=False, parkNoCurrentPatient:=False, selectPatientIdAfterRefresh:=PatientID)
            Return
        End If
        For i As Integer = 0 To _bindingPatients.Count - 1
            If _bindingPatients(i).PatientID = PatientID Then PatientBS.Position = i : Exit For
        Next
    End Sub

#End Region

#Region "Header"
#Region "Initialize"

    Private connectionString As String = DentistXDATA.GetConnection.ConnectionString
    Private _patientData As New PatientDATA
    ''' <summary>True when popup was opened via btNewPatient (add mode); false when opened via PopupPatient (edit).</summary>
    Private _popupOpeningForAdd As Boolean = False
    ''' <summary>After FlyoutPanel paints, move focus to PatientNameTextEdit (ShowPopup steals focus from pre-flyout Focus calls).</summary>
    Private _focusPatientNameEditWhenFlyoutShown As Boolean = False
    Friend _bindingPatients As BindingList(Of Patient)
    Private _Navigator3PatientsLoadStarted As Boolean

    Dim Kid As Boolean = False
    Dim Adult As Boolean = True
    Dim Grid As Boolean = False


    'Private _btnBodyExpand As SimpleButton

    Public Sub New()
        InitializeComponent()
        _listState = New NavigatorPatientListState()
        _searchCoordinator = New NavigatorSearchUiCoordinator()
        AddHandler _searchCoordinator.DebouncedTick, AddressOf OnSearchDebouncedTick
        SetImgs()
        StoreOriginalBounds(Me)
        _bindingPatients = New BindingList(Of Patient)()
        PatientBS.DataSource = _bindingPatients
        BindPatientHeaderLabels()
        ApplyFilter()
        WireNavigatorLifetimeHooks()
    End Sub

    Public Sub New(ByVal filterTarget As String)
        InitializeComponent()
        _listState = New NavigatorPatientListState(If(String.IsNullOrEmpty(filterTarget), PatientFilterAll, filterTarget))
        _searchCoordinator = New NavigatorSearchUiCoordinator()
        AddHandler _searchCoordinator.DebouncedTick, AddressOf OnSearchDebouncedTick
        SetImgs()
        StoreOriginalBounds(Me)
        _bindingPatients = New BindingList(Of Patient)()
        PatientBS.DataSource = _bindingPatients
        BindPatientHeaderLabels()
        ApplyFilter()
        WireNavigatorLifetimeHooks()
    End Sub

    Private Sub WireNavigatorLifetimeHooks()
        AddHandler Me.Disposed, AddressOf OnNavigatorDisposed
    End Sub

    Private Sub OnNavigatorDisposed(sender As Object, e As EventArgs)
        RemoveHandler Me.Disposed, AddressOf OnNavigatorDisposed
        Try
            RemoveHandler ComboFlyoutSearchHelper.BeforeMaintenanceModalDialog, AddressOf OnBeforeMaintenanceModalDialogFromCombo
        Catch
        End Try
        TeardownAuxiliaryTimers()
    End Sub

    ''' <summary>Hide patient flyout synchronously before Frm_TblCities / Frm_Health (or similar) ShowDialog from nested combos.</summary>
    Private Sub OnBeforeMaintenanceModalDialogFromCombo(sender As Object, e As EventArgs)
        If Me.IsDisposed OrElse Not Me.IsHandleCreated Then Return
        HardHideSuggestionOverlay()
        HidePatientFlyoutSafe(suppressUserMessageOnError:=True)
    End Sub

    Private Sub TeardownAuxiliaryTimers()
        Try
            If _searchCoordinator IsNot Nothing Then
                RemoveHandler _searchCoordinator.DebouncedTick, AddressOf OnSearchDebouncedTick
                _searchCoordinator.Dispose()
            End If
            If _searchLeaveHideTimer IsNot Nothing Then
                _searchLeaveHideTimer.Stop()
                RemoveHandler _searchLeaveHideTimer.Tick, AddressOf OnSearchLeaveHideTimerTick
                _searchLeaveHideTimer.Dispose()
                _searchLeaveHideTimer = Nothing
            End If
            If _resizeCoalesceTimer IsNot Nothing Then
                _resizeCoalesceTimer.Stop()
                RemoveHandler _resizeCoalesceTimer.Tick, AddressOf OnResizeCoalesceTick
                _resizeCoalesceTimer.Dispose()
                _resizeCoalesceTimer = Nothing
            End If
            RemovePatientSuggestionHeaderDismissFilter()
        Catch ex As Exception
            NotifyNonFatal("Navigator", "التنقل",
                "Patient search timers could not shut down cleanly. You can keep working; if search or buttons misbehave, close the patient workspace or restart the program.",
                "تعذر إيقاف مؤقتات البحث عن المريض بشكل كامل. يمكنك المتابعة؛ إذا تعطّل البحث أو الأزرار، أغلق مساحة المريض أو أعد تشغيل البرنامج.",
                ex)
        End Try
    End Sub

    ''' <summary>Bind the header value labels (patient name, sum of treats, sum of pays) to PatientBS.
    ''' Same pattern as LabelBal (bound to Balance in the Designer); done here in code so the Designer
    ''' stays untouched. TotalTreatments / TotalPayments share one PatientBalance fetch with Balance, so
    ''' switching patients costs one DB round-trip regardless of how many of these labels refresh.</summary>
    Private Sub BindPatientHeaderLabels()
        If lblPatientName IsNot Nothing Then
            lblPatientName.DataBindings.Clear()
            lblPatientName.DataBindings.Add(New Binding("Text", Me.PatientBS, "PatientName", True))
        End If
        If lblTrts IsNot Nothing Then
            lblTrts.DataBindings.Clear()
            lblTrts.DataBindings.Add(New Binding("Text", Me.PatientBS, "TotalTreatments", True))
        End If
        If lblPays IsNot Nothing Then
            lblPays.DataBindings.Clear()
            lblPays.DataBindings.Add(New Binding("Text", Me.PatientBS, "TotalPayments", True))
        End If
    End Sub
    '_btnBodyExpand.Text = "▼"
    Private Sub StartNavigator3PatientsBackgroundLoad()
        If _Navigator3PatientsLoadStarted Then Return
        _Navigator3PatientsLoadStarted = True
        Me.UseWaitCursor = True
        Dim cs = connectionString
        Task.Run(Function()
                     Using conn As New SqlConnection(cs)
                         Return conn.Query(Of Patient)("SELECT * FROM Patient").ToList()
                     End Using
                 End Function).ContinueWith(
            Sub(t)
                If Me.IsDisposed Then Return
                BeginInvoke(Sub()
                                If Me.IsDisposed Then Return
                                Me.UseWaitCursor = False
                                If t.IsFaulted Then
                                    Dim ex = t.Exception?.GetBaseException()
                                    NotifyNonFatal("Patient list", "قائمة المرضى",
                                        "The patient list could not be loaded from the database. Search and navigation may be incomplete until you restart the program or try again.",
                                        "تعذر تحميل قائمة المرضى من قاعدة البيانات. قد يبقى البحث والتنقل ناقصاً حتى تعيد التشغيل أو تحاول مرة أخرى.",
                                        ex, MessageBoxIcon.Error)
                                    Return
                                End If
                                If t.Result Is Nothing Then
                                    NotifyNonFatal("Patient list", "قائمة المرضى",
                                        "No patient data was returned while loading the list. Check your database connection and try again.",
                                        "لم تُرجع أي بيانات مرضى أثناء التحميل. تحقق من الاتصال بقاعدة البيانات وحاول مرة أخرى.",
                                        Nothing, MessageBoxIcon.Warning)
                                    Return
                                End If
                                _listState.AllPatients.Clear()
                                _listState.AllPatients.AddRange(t.Result)
                                ApplyFilter(parkNoCurrentPatient:=ShouldParkNoCurrentPatientAfterBackgroundLoad())
                            End Sub)
            End Sub, TaskScheduler.Default)
    End Sub
    Private Sub Navigator3_Load(sender As Object, e As EventArgs) Handles Me.Load
        btnResetKid.Visible = False
        EnsurePatientSuggestionUi()
        EnsurePatientSuggestionHeaderDismissFilter()
        EnsureSearchResultsButton()
        suppressToggleEvent = True
        ' Store original size of MPanel
        Dim sw As New Stopwatch
        sw.Start()
        ResizeControlsProportionally()
        Select Case _Target
            Case "Treat"
                Trtready = True
                MobReady = False
                Orthready = False
            Case "Ortho"
                Trtready = False
                MobReady = False
                Orthready = True
            Case "Mobile"
                Trtready = False
                MobReady = True
                Orthready = False
            Case "Diag"
                Trtready = False
                MobReady = False
                Orthready = False
            Case Else
                Trtready = True
                MobReady = False
                Orthready = False
        End Select

        'LoadPatients(_Target)
        ' Show filtered patients in list box on load
        FillCboPrefixOnce()
        suppressToggleEvent = True
        btnAdultChart.Checked = False
        suppressToggleEvent = False
        'AddHandler PopupPatient.Popup, AddressOf PopupPatient_Popup
        txtPatientName.Focus()
        UpdateSearchResultsButtonVisibility()
        sw.Stop()
        LogToFile("Time spent: " & sw.Elapsed.TotalSeconds.ToString("F2") & " seconds", Me)
        StartNavigator3PatientsBackgroundLoad()
        AddHandler ComboFlyoutSearchHelper.BeforeMaintenanceModalDialog, AddressOf OnBeforeMaintenanceModalDialogFromCombo
        txtPatientName.Visible = True
    End Sub


#Region "Patient Context Preservation"
    Private _currentPatient As Patient
    Public ReadOnly Property Current_Patient As Patient Implements IPatientHeaderControl.Current_Patient
        Get
            Return _currentPatient
        End Get
    End Property
    Public ReadOnly Property IsKidLabelAsControl As System.Windows.Forms.Control Implements IPatientHeaderControl.IsKidLabel
        Get
            Return IsKidLabel
        End Get
    End Property




    Private Sub UpdateBoundControls(patient As Patient)
        If patient IsNot Nothing Then
            lblPatientNum.Text = patient.PatientNumber
            txtPatientName.Text = patient.PatientName
            LabelAddress.Text = patient.Address
            LabelAge.Text = patient.Age.ToString()
            LoadBal(patient.PatientID)
        End If
    End Sub
    ''' <summary>Update labels and balance when selection changes, without touching txtPatientName (so search box is not overwritten).</summary>
    Private Sub UpdateBoundControlsExceptName(patient As Patient)
        If patient IsNot Nothing Then
            lblPatientNum.Text = patient.PatientNumber
            LabelAddress.Text = patient.Address
            LabelAge.Text = patient.Age.ToString()
            ' LabelBal is bound to PatientBS "Balance"; avoid LoadBal/GetBalance here — second EXEC PatientBalance per nav.
            PopupPatient.Text = patient.PatientName
            ApplyPopupPatientAgeForeColor(patient)
        End If
    End Sub
    ''' <summary>Update Kid/chart state and UI only (IsKidLabel, btnAdultChart, btnResetKid). Does not touch txtPatientName or other bound controls.</summary>
    Private Sub UpdateKidStateOnly(patient As Patient)
        If patient Is Nothing Then Return
        If manualOverrideActive Then
            Kid = manualKidStatus
            Dim lblEn As String = If(Kid, "Kid (Manual)", "Adult (Manual)")
            Dim lblAr As String = If(Kid, "طفل (يدوي)", "بالغ (يدوي)")
            IsKidLabel.Text = If(Eng, lblEn, lblAr)
            btnResetKid.Visible = True
        Else
            Kid = NavigatorAutoKidFromPatient(patient)
            Dim lblEn As String = If(Kid, "Kid", "Adult")
            Dim lblAr As String = If(Kid, "طفل", "بالغ")
            IsKidLabel.Text = If(Eng, lblEn, lblAr)
            btnResetKid.Visible = False
            suppressToggleEvent = True
            btnAdultChart.Checked = Kid
            UpdateButtonAppearance()
            suppressToggleEvent = False
        End If
        Module1.isKid = Kid
        If IsKidLabel.Text.Contains("(Manual)") OrElse IsKidLabel.Text.Contains("(يدوي)") Then
            IsKidLabel.ForeColor = Color.Red
        Else
            IsKidLabel.ForeColor = If(Kid, Color.Green, Color.Blue)
        End If
        _currentPatient.IsKid = Kid
        _currentPatient.IsGrid = Grid
        _currentPatient.IsFull = Not btnAdultChart.Checked
    End Sub
    Private Sub ClearBoundControls()
        lblPatientNum.Text = ""
        txtPatientName.Text = ""
        LabelAddress.Text = ""
        LabelAge.Text = ""
        LabelBal.Text = "0.0"
    End Sub

    ' Inside Navigator3 user control
    Private Sub OnPatientSelected(ByVal newPatient As Patient, ByVal Optional showkid As Boolean = False, ByVal Optional force As Boolean = False)
        If newPatient Is Nothing Then
            ClearBoundControls()
            If PopupPatient IsNot Nothing Then
                PopupPatient.Text = ""
                ApplyPopupPatientAgeForeColor(Nothing)
            End If
            Exit Sub
        End If
        Dim sw As New Stopwatch
        sw.Start()
        _currentPatient = newPatient
        ' Update bound controls
        UpdateBoundControls(newPatient)
        'LabelAge.Text = newPatient.Age
        ' Determine kid status - manual override only if user clicked the button
        If manualOverrideActive Then
            ' When in manual mode, use the override value
            Kid = manualKidStatus
            Dim lblEn As String = If(Kid, "Kid (Manual)", "Adult (Manual)")
            Dim lblAr As String = If(Kid, "طفل (يدوي)", "بالغ (يدوي)")
            IsKidLabel.Text = If(Eng, lblEn, lblAr)
            'IsKidLabel.Text = If(Kid, "Kid (Manual)", "Adult (Manual)")
            btnResetKid.Visible = True
        Else
            ' Automatic age-based selection
            ' In auto mode, use age-based selection
            Kid = NavigatorAutoKidFromPatient(newPatient)
            Dim lblEn As String = If(Kid, "Kid", "Adult")
            Dim lblAr As String = If(Kid, "طفل", "بالغ")
            IsKidLabel.Text = If(Eng, lblEn, lblAr)
            'IsKidLabel.Text = If(Kid, "Kid", "Adult")
            btnResetKid.Visible = False
            ' Update button state without triggering events
            suppressToggleEvent = True
            btnAdultChart.Checked = Kid
            UpdateButtonAppearance()
            suppressToggleEvent = False
        End If
        Module1.isKid = Kid
        If IsKidLabel.Text.Contains("(Manual)") OrElse IsKidLabel.Text.Contains("(يدوي)") Then
            IsKidLabel.ForeColor = Color.Red
        Else
            IsKidLabel.ForeColor = If(Kid, Color.Green, Color.Blue)
        End If
        _currentPatient.IsKid = Kid
        _currentPatient.IsGrid = Grid
        _currentPatient.IsFull = Not btnAdultChart.Checked
        PopupPatient.Text = newPatient.PatientName
        ApplyPopupPatientAgeForeColor(newPatient)
        ApplyNavigatorShellStyleForTarget(newPatient)

        ' Raise the event so TreatsUserControl and others get patient + IsKid (adult/kid chart)
        PatientEventManager.RaisePatientChanged(newPatient, Kid, force)

        sw.Stop()
        LogToFile("OnPatientSelected spent: " & sw.Elapsed.TotalSeconds.ToString("F2") & " seconds", Me)
    End Sub

    Private Sub UpdateNavigationControls()
        If Me.InvokeRequired Then
            Me.Invoke(Sub() UpdateNavigationControls())
            Return
        End If
        If PatientBS Is Nothing Then Return
        Dim displayCount As Integer
        If PatientBS.Count > 0 Then
            displayCount = PatientBS.Count
        ElseIf _listState.FilteredPatients IsNot Nothing Then
            displayCount = _listState.FilteredPatients.Count
        Else
            displayCount = 0
        End If
        If txtCount IsNot Nothing Then txtCount.Text = displayCount.ToString()
        If posTxt IsNot Nothing Then posTxt.Text = If(PatientBS.Position >= 0, (PatientBS.Position + 1).ToString(), "0")
    End Sub

    Private Sub PatientBS_PositionChanged(sender As Object, e As EventArgs) Handles PatientBS.PositionChanged
        UpdateNavigationControls()
        FirePatientChangedForCurrent(_pendingSuppressPatientBroadcast)
    End Sub

    Private Sub PatientBS_CurrentChanged(sender As Object, e As EventArgs) Handles PatientBS.CurrentChanged
        ApplyPopupPatientAgeForeColor(TryCast(PatientBS.Current, Patient))
    End Sub

    Private Sub btnFirst_Click(sender As Object, e As EventArgs) Handles btnFirst.Click
        If PatientBS Is Nothing OrElse PatientBS.Count = 0 Then Return
        PatientBS.MoveFirst()
    End Sub

    Private Sub btnPrev_Click(sender As Object, e As EventArgs) Handles btnPrev.Click
        If PatientBS Is Nothing OrElse PatientBS.Count = 0 Then Return
        PatientBS.MovePrevious()
    End Sub

    Private Sub btnNext_Click(sender As Object, e As EventArgs) Handles btnNext.Click
        If PatientBS Is Nothing OrElse PatientBS.Count = 0 Then Return
        PatientBS.MoveNext()
    End Sub

    Private Sub btnLast_Click(sender As Object, e As EventArgs) Handles btnLast.Click
        If PatientBS Is Nothing OrElse PatientBS.Count = 0 Then Return
        PatientBS.MoveLast()
    End Sub

    Private Sub SetCurrentPatient(patient As Patient)
        _currentPatient = patient
        If patient IsNot Nothing Then
            Dim foundPosition As Integer = -1
            For i As Integer = 0 To PatientBS.Count - 1
                Dim current = CType(PatientBS(i), Patient)
                If current.PatientID = patient.PatientID Then
                    foundPosition = i
                    Exit For
                End If
            Next
            If foundPosition >= 0 Then
                PatientBS.Position = foundPosition
            Else
                NotifyNonFatal("Patient list", "قائمة المرضى",
                    $"The selected patient (ID {patient.PatientID}) is not in the current filtered list. Use search or change the filter, then try again.",
                    $"المريض المحدد (رقم {patient.PatientID}) ليس في القائمة المصفاة الحالية. استخدم البحث أو غيّر التصفية ثم أعد المحاولة.",
                    Nothing, MessageBoxIcon.Information)
            End If
        End If
    End Sub

    ''' <summary>Call after patient is updated (e.g. from PatientInfoForm) to refresh header display and keep list in sync.</summary>
    Public Sub UpdateCurrentPatient(patient As Patient)
        If patient Is Nothing Then Return
        _currentPatient = patient
        UpdateBoundControls(patient)
        LoadBal(patient.PatientID)
        ' Ensure this patient is selected in the list if present
        For i As Integer = 0 To PatientBS.Count - 1
            If CType(PatientBS(i), Patient).PatientID = patient.PatientID Then
                PatientBS.Position = i
                Exit For
            End If
        Next
    End Sub
#End Region

#End Region


    '------------------------------------
    ''' <summary>Forces PatientBS-bound financial labels to re-query Balance / TotalTreatments / TotalPayments.
    ''' <see cref="Patient"/> clears its cache in <see cref="Patient.RefreshComputedProperties"/> but does not
    ''' implement INotifyPropertyChanged, so bindings do not refresh by themselves.</summary>
    Private Sub RefreshFinancialHeaderBindingsReadValues()
        For Each lbl As Control In {LabelBal, lblTrts, lblPays}
            If lbl Is Nothing Then Continue For
            For Each b As Binding In lbl.DataBindings
                b.ReadValue()
            Next
        Next
    End Sub

    Public Sub LoadBal(ByVal PatientId As Integer) Implements IPatientHeaderControl.LoadBal

        Try
            If _currentPatient Is Nothing Then Return
            Dim samePatient = _currentPatient.PatientID = PatientId
            If samePatient Then
                _currentPatient.RefreshComputedProperties()
                RefreshFinancialHeaderBindingsReadValues()
            Else
                Dim d = _currentPatient.GetBalance(PatientId)
                Me.LabelBal.Text = d.ToString("###0.0")
                If d < 0 Then
                    Me.LabelBal.ForeColor = Color.Red
                ElseIf d > 0 Then
                    Me.LabelBal.ForeColor = Color.Blue
                Else
                    Me.LabelBal.ForeColor = Color.Black
                End If
            End If
        Catch ex As System.Data.SqlClient.SqlException
            NotifyNonFatal("Balance", "الرصيد",
                "The patient balance could not be read from the database.",
                "تعذر قراءة رصيد المريض من قاعدة البيانات.",
                ex, MessageBoxIcon.Warning)
        Catch ex As Exception
            NotifyNonFatal("Balance", "الرصيد",
                "The patient balance could not be updated.",
                "تعذر تحديث رصيد المريض.",
                ex, MessageBoxIcon.Warning)
        End Try
    End Sub


#Region "PatientHdrControls"

    Private Function GetTypedNameForNewPatient() As String
        If txtPatientName Is Nothing Then Return ""

        Dim typedName As String = If(txtPatientName.Text, "").Trim()
        If typedName.Length = 0 Then Return ""

        Dim normalizedTyped As String = NavigatorPatientListState.NormalizePatientSearchText(typedName)
        Dim exactMatchSavedName As Boolean =
            _listState.AllPatients IsNot Nothing AndAlso
            _listState.AllPatients.Any(Function(p) String.Equals(
                NavigatorPatientListState.NormalizePatientSearchText(If(p.PatientName, "")),
                normalizedTyped,
                StringComparison.OrdinalIgnoreCase))

        ' Never seed the flyout with a full name identical to one already on file.
        If exactMatchSavedName Then Return ""

        Dim currentSearchRows = BuildSearchFilteredPatientList(typedName)
        ' Prefill whenever the current typed text yields no selectable rows. Do not rely on PatientBS here:
        ' the debounce timer may not have rebuilt it yet when the user clicks Add New immediately after typing.
        If currentSearchRows.Count = 0 Then Return typedName

        Return ""
    End Function

    Private Sub FocusPatientNameTextEdit()
        If Me.IsDisposed OrElse Not Me.IsHandleCreated Then Return
        'If PatientNameTextEdit Is Nothing OrElse PatientNameTextEdit.IsDisposed Then Return

        Try
            'PatientNameTextEdit.Focus()
            'PatientNameTextEdit.Select()
            'PatientNameTextEdit.SelectionStart = If(PatientNameTextEdit.Text, "").Length
            'PatientNameTextEdit.SelectionLength = 0
        Catch ex As Exception
            Try
                'PatientNameTextEdit.Focus()
                'PatientNameTextEdit.Select()
            Catch ex2 As Exception
                NotifyNonFatal("Patient details", "بيانات المريض",
                    "The patient name field could not receive focus. Click inside the name field and try again.",
                    "تعذر نقل التركيز إلى حقل الاسم. انقر داخل حقل الاسم وحاول مرة أخرى.",
                    ex2, MessageBoxIcon.Information)
            End Try
        End Try
    End Sub

    Private Sub RestoreNewPatientFlyoutFromSearch()
        ClearFormForNewPatient()
        'PatientNameTextEdit.Text = GetTypedNameForNewPatient()
    End Sub

    Private Sub ShowFly(Optional focusPatientNameEditWhenShown As Boolean = False,
                        Optional ownerAnchor As Control = Nothing)
        ' Always close an existing flyout before ShowPopup; mixed HideBeakForm/HidePopup paths across the app
        ' otherwise leave the panel half-attached and subsequent opens may fail until the flyout is reset.
        HidePatientFlyoutSafe(suppressUserMessageOnError:=True)
        HardHideSuggestionOverlay()
        If Me.IsDisposed Then
            NotifyNonFatal("Patient details", "بيانات المريض",
                "The patient header is no longer available. Close the patient workspace and open it again.",
                "رأس المريض لم يعد متاحاً. أغلق مساحة المريض وأعد فتحها.",
                Nothing, MessageBoxIcon.Information)
            Return
        End If
        If Not Me.IsHandleCreated Then
            Try
                Me.CreateControl()
            Catch ex As Exception
                NotifyNonFatal("Patient details", "بيانات المريض",
                    "The patient header could not finish loading. Close the patient workspace and try again.",
                    "تعذر إكمال تحميل رأس المريض. أغلق مساحة المريض وحاول مرة أخرى.",
                    ex, MessageBoxIcon.Warning)
                Return
            End Try
        End If
        If Not Me.IsHandleCreated Then
            NotifyNonFatal("Patient details", "بيانات المريض",
                "The patient header is still initializing. Wait a moment, then click Add or the patient name again.",
                "رأس المريض ما زال يُهيأ. انتظر لحظة ثم انقر إضافة أو اسم المريض مرة أخرى.",
                Nothing, MessageBoxIcon.Information)
            Return
        End If
        'If FlyoutPatientInfo Is Nothing OrElse FlyoutPatientInfo.IsDisposed Then
        '    NotifyNonFatal("Patient details", "بيانات المريض",
        '        "The patient details panel is not available. Close the patient workspace and open it again, or restart the program.",
        '        "لوحة بيانات المريض غير متاحة. أغلق مساحة المريض وأعد فتحها، أو أعد تشغيل البرنامج.",
        '        Nothing, MessageBoxIcon.Warning)
        '    Return
        'End If

        Dim focusAfter As Boolean = focusPatientNameEditWhenShown
        ' Defer ShowPopup one message so nested FlyoutPanelToolForm / combo popups (e.g. HlthCombo) can finish teardown;
        ' immediate Show after HidePopup(True) has caused ObjectDisposedException on FlyoutPanel (crash_20260507_120635).
        Dim showWork As New Action(
            Sub()
                'If Me.IsDisposed OrElse FlyoutPatientInfo Is Nothing OrElse FlyoutPatientInfo.IsDisposed Then
                '    NotifyNonFatal("Patient details", "بيانات المريض",
                '        "The patient details panel became unavailable while opening. Close the workspace or restart the program, then try again.",
                '        "أصبحت لوحة بيانات المريض غير متاحة أثناء الفتح. أغلق المساحة أو أعد تشغيل البرنامج ثم حاول مرة أخرى.",
                '        Nothing, MessageBoxIcon.Warning)
                '    Return
                'End If
                'Try
                '    ' Only Fade and Slide exist in this DX enum; Slide avoids PopupToolWindowFadeAnimationProvider (see crash_20260507_120635).
                '    FlyoutPatientInfo.Options.AnimationType = DevExpress.Utils.Win.PopupToolWindowAnimation.Slide
                '    FlyoutPatientInfo.Options.AnchorType = DevExpress.Utils.Win.PopupToolWindowAnchor.Manual
                '    ' Anchor the popup to the top-level shell (not Navigator3). When OwnerControl is this UC, nested modal
                '    ' dialogs owned by MainView can tear down the flyout HWND and leave FlyoutPatientInfo disposed.
                '    Dim shell = TryCast(Me.TopLevelControl, Control)
                '    If shell IsNot Nothing AndAlso Not Object.ReferenceEquals(shell, Me) Then
                '        FlyoutPatientInfo.OwnerControl = shell
                '        Dim desiredNavPt As New System.Drawing.Point(299, 74)
                '        Dim screenPt = Me.PointToScreen(desiredNavPt)
                '        FlyoutPatientInfo.Options.Location = shell.PointToClient(screenPt)
                '    Else
                '        FlyoutPatientInfo.OwnerControl = Me
                '        FlyoutPatientInfo.Options.Location = New System.Drawing.Point(299, 74)
                '    End If
                '    _focusPatientNameEditWhenFlyoutShown = focusAfter
                '    FlyoutPatientInfo.ShowPopup()
                'Catch ex As ObjectDisposedException
                '    NotifyNonFatal("Patient details", "بيانات المريض",
                '        "The patient details window was closed while opening. Close any open patient dialogs and click Add or the patient name again.",
                '        "أُغلقت نافذة بيانات المريض أثناء الفتح. أغلق أي نوافذ مريض مفتوحة ثم انقر إضافة أو اسم المريض مرة أخرى.",
                '        ex, MessageBoxIcon.Warning)
                'Catch ex As Exception
                '    NotifyNonFatal("Patient details", "بيانات المريض",
                '        "The patient details window could not be shown.",
                '        "تعذر إظهار نافذة بيانات المريض.",
                '        ex, MessageBoxIcon.Warning)
                'End Try
            End Sub)
        If Me.IsHandleCreated AndAlso Me.InvokeRequired Then
            Me.BeginInvoke(showWork)
        Else
            showWork()
        End If
    End Sub

    ''' <summary>
    ''' Closes the patient flyout. Prefer <see cref="HidePopup(Boolean)"/> with <c>immediate:=False</c> so the panel host is not torn down
    ''' in a state where the next <c>ShowPopup</c> throws <see cref="ObjectDisposedException"/> after nested tool windows close.
    ''' Animated/duplicate-hide issues are swallowed by Try/Catch.
    ''' </summary>
    Private Sub HidePatientFlyoutSafe(Optional suppressUserMessageOnError As Boolean = False)
        'Try
        '    If FlyoutPatientInfo Is Nothing OrElse FlyoutPatientInfo.IsDisposed Then Return
        '    FlyoutPatientInfo.HidePopup(False)
        'Catch ex As Exception
        '    If suppressUserMessageOnError Then Return
        '    NotifyNonFatal("Patient details", "بيانات المريض",
        '        "The patient details window could not be closed cleanly. If it still appears or buttons stop responding, switch to another screen and back, or restart the program.",
        '        "تعذر إغلاق نافذة بيانات المريض بشكل صحيح. إذا بقيت النافذة أو توقفت الأزرار عن الاستجابة، انتقل إلى شاشة أخرى ثم عد، أو أعد تشغيل البرنامج.",
        '        ex, MessageBoxIcon.Information)
        'End Try
    End Sub

    ''' <summary>
    ''' Tear down patient flyout + search suggestions before swapping workspace body or re-showing the flyout.
    ''' Prevents DevExpress FlyoutPanel / peek window state where <see cref="ShowPopup"/> becomes a no-op and header clicks appear "dead" (no exception).
    ''' </summary>
    Friend Sub PrepareForEmbeddedBodySwitch()
        HardHideSuggestionOverlay()
        HidePatientFlyoutSafe(suppressUserMessageOnError:=True)
        _popupOpeningForAdd = False
    End Sub

    Private Sub FlyoutPatientInfo_Showing(sender As Object, e As FlyoutPanelEventArgs)
        If Not _focusPatientNameEditWhenFlyoutShown Then Return
        _focusPatientNameEditWhenFlyoutShown = False
        BeginInvoke(New Action(Sub() FocusPatientNameTextEdit()))
    End Sub

    Private Sub btNewPatient_Click(sender As Object, e As EventArgs) Handles btNewPatient.Click
        HardHideSuggestionOverlay()
        ComboFlyoutSearchHelper.RaiseBeforeMaintenanceModalDialog()
        _popupOpeningForAdd = False
        Dim filterToken As String = If(String.Equals(_Target, PatientFilterAll, StringComparison.OrdinalIgnoreCase), "", _Target)
        Dim owner As Form = ComboFlyoutSearchHelper.TryGetApplicationShellForm()
        If owner Is Nothing Then owner = TryCast(Me.TopLevelControl, Form)
        Try
            Using f As New PatientAddEditForm(filterToken)
                If f.ShowDialog(owner) = DialogResult.OK AndAlso f.LastInsertedPatient IsNot Nothing Then
                    MergeInsertedPatientIntoNavigator(f.LastInsertedPatient)
                End If
            End Using
        Finally
            ComboFlyoutSearchHelper.RaiseAfterMaintenanceModalDialog()
        End Try
    End Sub

    ''' <summary>Clear flyout patient fields (name, demographics, filters) without changing header title or Add/Update mode.</summary>
    Private Sub ClearPopupPatientDetailFields()
        'PatientIDEdit.EditValue = 0
        'PatientNameTextEdit.Text = ""
        'SexTextBox.Text = ""
        'AgeSpinEdit.EditValue = 25
        'PhoneTextEdit.Text = ""
        'AddressTextEdit.Text = ""
        'HealthTextBox.Text = ""
        'NotesTextEdit.Text = ""
        'BirthYSpinEdit.EditValue = Now.Year - 25
        'CboCity.CboCity.SelectedIndex = -1
        'CboHealth.CboHealth.SelectedIndex = -1
        'If lblPNum IsNot Nothing Then lblPNum.Text = ""
        'TreatCheckBox.Checked = (_Target = PatientFilterTreat)
        'ImplantCheckBox.Checked = (_Target = "Implant")
        'MobileCheckBox.Checked = (_Target = PatientFilterMobile)
        'OrthoCheckBox.Checked = (_Target = PatientFilterOrtho)
        'DiagCheck.Checked = (_Target = PatientFilterDiag)
        'StrucCheckBox.Checked = False
        'txtWhats.Text = ""
        'cboPrefix.SelectedIndex = -1
    End Sub
    ''' <summary>Clear popup form so Save/Update button will perform Insert (new patient).</summary>
    Private Sub ClearFormForNewPatient()
        'grpPatientInfo.Text = If(Eng, "Add New Patient", "إضافة مريض جديد")
        'btnAdd.Visible = True
        'btnUpdatePatient.Enabled = False
        'btnDeletePatient.Enabled = False
        'ClearPopupPatientDetailFields()
    End Sub

    Private Sub RefreshBoundControls()
        For Each ctrl As Control In Me.Controls
            If ctrl.DataBindings.Count > 0 Then
                ctrl.DataBindings(0).ReadValue()
            End If
        Next
    End Sub
    Private Sub OpenPopupPatientFlyout()
        HardHideSuggestionOverlay()
        If _popupOpeningForAdd Then _popupOpeningForAdd = False
        Dim cur As Patient = TryCast(PatientBS.Current, Patient)
        If cur Is Nothing Then cur = _currentPatient
        If cur Is Nothing OrElse cur.PatientID <= 0 Then
            MessageBox.Show(
                If(Eng,
                   "No patient is selected. Choose a patient from the list or adjust your search.",
                   "لا يوجد مريض محدد. اختر مريضاً من القائمة أو عدّل البحث."),
                If(Eng, "Patient", "المريض"),
                MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If
        _currentPatient = cur
        ComboFlyoutSearchHelper.RaiseBeforeMaintenanceModalDialog()
        Dim owner As Form = ComboFlyoutSearchHelper.TryGetApplicationShellForm()
        If owner Is Nothing Then owner = TryCast(Me.TopLevelControl, Form)
        Try
            Using infoForm As New PatientInfoForm(cur.PatientID)
                If infoForm.ShowDialog(owner) <> DialogResult.OK Then Return
                If infoForm.PatientDeleted Then
                    RemovePatientFromNavigatorAfterDelete(cur)
                    Return
                End If
                If infoForm.LastUpdatedPatient IsNot Nothing Then
                    MergeUpdatedPatientIntoNavigator(infoForm.LastUpdatedPatient)
                    HidePatientFlyoutSafe()
                End If
            End Using
        Finally
            ComboFlyoutSearchHelper.RaiseAfterMaintenanceModalDialog()
        End Try
    End Sub

    Private Sub PopupPatient_Click(sender As Object, e As EventArgs) Handles PopupPatient.Click
        OpenPopupPatientFlyout()
    End Sub

    Private Sub PopupPatient_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles PopupPatient.ButtonClick
        OpenPopupPatientFlyout()
    End Sub
    ''' <summary>Reloads <see cref="_currentPatient"/> and cached lists from DB so WhatsApp and other fields match saves from Appointments/Accounting/etc.</summary>
    Private Sub RebindCurrentPatientFromDatabaseBeforePopup()
        If _currentPatient Is Nothing OrElse _currentPatient.PatientID <= 0 Then Return
        Try
            Dim prev As Patient = _currentPatient
            Dim fresh As Patient = _patientData.Select_RecordByID(prev.PatientID)
            If fresh Is Nothing Then Return
            ' Navigator3-only state not loaded from Patient row
            fresh.IsKid = prev.IsKid
            fresh.IsGrid = prev.IsGrid
            fresh.IsFull = prev.IsFull
            _currentPatient = fresh
            If _listState.AllPatients IsNot Nothing Then
                Dim idxAll As Integer = _listState.AllPatients.FindIndex(Function(p) p.PatientID = fresh.PatientID)
                If idxAll >= 0 Then _listState.AllPatients(idxAll) = fresh
            End If
            If _listState.FilteredPatients IsNot Nothing Then
                Dim idxF As Integer = _listState.FilteredPatients.FindIndex(Function(p) p.PatientID = fresh.PatientID)
                If idxF >= 0 Then _listState.FilteredPatients(idxF) = fresh
            End If
            If _bindingPatients IsNot Nothing Then
                For bi As Integer = 0 To _bindingPatients.Count - 1
                    If _bindingPatients(bi).PatientID = fresh.PatientID Then
                        _bindingPatients(bi) = fresh
                        Exit For
                    End If
                Next
            End If
        Catch ex As Exception
            NotifyNonFatal("Patient details", "بيانات المريض",
                "The latest patient information could not be read from the database. The form may show older data.",
                "تعذر قراءة أحدث بيانات المريض من قاعدة البيانات. قد تظهر بيانات قديمة في النموذج.",
                ex, MessageBoxIcon.Warning)
        End Try
    End Sub

    ''' <summary>Fill popup form fields from _currentPatient (when user clicks PopupPatient to view/edit).</summary>
    Private Sub LoadCurrentPatientIntoPopup()
        'If _currentPatient Is Nothing Then Return
        'RebindCurrentPatientFromDatabaseBeforePopup()
        'PatientIDEdit.EditValue = _currentPatient.PatientID
        'PatientNameTextEdit.Text = _currentPatient.PatientName ' If(_currentPatient.PatientName, "")
        'SexTextBox.Text = If(_currentPatient.Sex, "")
        'AgeSpinEdit.EditValue = If(_currentPatient.Age.HasValue, CType(_currentPatient.Age.Value, Object), 0)
        'PhoneTextEdit.Text = If(_currentPatient.Phone, "")
        '' Saved WhatsApp prefix + local / phone fallback (same as appointment editor)
        'Try
        '    WhatsHelper.BindPatientWhatsPrefixAndLocal(cboPrefix, txtWhats, _currentPatient)
        '    RefreshLblWhats()
        'Catch ex As Exception
        '    NotifyNonFatal("WhatsApp", "واتساب",
        '        "WhatsApp fields for this patient could not be filled automatically. You can still edit the phone number manually.",
        '        "تعذر تعبئة حقول واتساب لهذا المريض تلقائياً. لا يزال بإمكانك تعديل رقم الهاتف يدوياً.",
        '        ex, MessageBoxIcon.Information)
        'End Try

        'AddressTextEdit.Text = If(_currentPatient.Address, "")
        'HealthTextBox.Text = If(_currentPatient.Health, "")
        'NotesTextEdit.Text = If(_currentPatient.Notes, "")
        'BirthYSpinEdit.EditValue = If(_currentPatient.BirthY.HasValue, CType(_currentPatient.BirthY.Value, Object), 0)
        'If lblPNum IsNot Nothing Then lblPNum.Text = If(_currentPatient.PatientNumber, "")
        'TreatCheckBox.Checked = _currentPatient.Treat.GetValueOrDefault(False)
        'ImplantCheckBox.Checked = _currentPatient.Implant.GetValueOrDefault(False)
        'MobileCheckBox.Checked = _currentPatient.Mobile.GetValueOrDefault(False)
        'OrthoCheckBox.Checked = _currentPatient.Ortho.GetValueOrDefault(False)
        'DiagCheck.Checked = _currentPatient.Diag.GetValueOrDefault(False)
        'StrucCheckBox.Checked = _currentPatient.Struc.GetValueOrDefault(False)
    End Sub
    Private Sub LabelAge_TextChanged(sender As Object, e As EventArgs) Handles LabelAge.TextChanged
        Dim Age As Integer
        If Integer.TryParse(Me.LabelAge.Text, Age) Then
            Me.LabelAge.Text = Age.ToString("###0")
            If Age > 11 And Age <= 150 Then
                Me.LabelAge.Text = If(Eng, Age.ToString & " Yrs", Age.ToString & "  سنة ")
            ElseIf Age <= 11 And Age >= 2 Then
                Me.LabelAge.Text = If(Eng, Age.ToString & " Yrs", Age.ToString & "  سنوات ")
            ElseIf Age < 2 And Age >= 1 Then
                Me.LabelAge.Text = If(Eng, "Age is not reasonable", "العمر غير منطقي")
            Else
                Me.LabelAge.Text = If(Eng, "Age Not Set", "العمر غير محدد")
                isKid = False
            End If
            If Age <= 11 And Age >= 2 Then
                Me.LabelAge.ForeColor = Color.Red
            ElseIf Age > 11 And Age <= 150 Then
                Me.LabelAge.ForeColor = Color.Blue
            Else
                Me.LabelAge.ForeColor = Color.Black
            End If
        End If
    End Sub
    ''' <summary>Formats the three numeric patient-financial labels (Balance, sum of treats, sum of pays)
    ''' into a clean "###0.0" form and colors them based on sign. Caption labels (lblTxtTrts / lblTxtPays /
    ''' lbltxtBal) and lblPatientNum fire the same handler but are intentionally left alone.</summary>
    Private Sub LabelBal_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) _
        Handles LabelBal.TextChanged, lblTrts.TextChanged, lblPays.TextChanged,
                lblPatientNum.TextChanged, lblTxtTrts.TextChanged, lblTxtPays.TextChanged, lbltxtBal.TextChanged, lblPatientName.TextChanged
        Dim lbl = TryCast(sender, DevExpress.XtraEditors.LabelControl)
        If lbl Is Nothing Then Return
        ' Only Balance / TotalTreatments / TotalPayments are numeric value labels. Everything else here is
        ' static caption text or patient number — don't reformat those (they'd get overwritten as "0.0").
        If lbl IsNot LabelBal AndAlso lbl IsNot lblTrts AndAlso lbl IsNot lblPays Then Return

        Dim d As Double
        If Double.TryParse(lbl.Text, d) Then
            lbl.Text = d.ToString("###0.0")
            If lbl Is LabelBal Then
                ' Balance: red when owed (<0), blue when credit (>0), black at zero.
                lbl.ForeColor = If(d < 0, Color.Red, If(d > 0, Color.Blue, Color.Black))
            ElseIf lbl Is lblTrts Then
                ' Treats total: neutral dark color so it doesn't compete with Balance.
                lbl.ForeColor = If(d > 0, Color.FromArgb(139, 69, 19), Color.Black) ' brownish for activity
            ElseIf lbl Is lblPays Then
                ' Pays total: green (money received). Keep consistent with the designer's green accents.
                lbl.ForeColor = If(d > 0, Color.FromArgb(0, 128, 0), Color.Black)
            End If
        Else
            lbl.ForeColor = Color.Black
            lbl.Text = "0.0"
        End If
    End Sub
#End Region
#Region "LoadTreats"
    Private Sub btnAdultChart_CheckedChanged(sender As Object, e As EventArgs) Handles btnAdultChart.CheckedChanged
        ' Skip if this is an automatic change during initialization
        If suppressToggleEvent Then Return
        If PatientBS.Count = 0 AndAlso _currentPatient Is Nothing Then Return
        Dim currentPatient As Patient = TryCast(PatientBS.Current, Patient)
        If currentPatient Is Nothing Then currentPatient = _currentPatient
        If currentPatient Is Nothing Then Return
        ' Only enter manual mode when user explicitly clicks the button
        manualOverrideActive = True
        manualKidStatus = btnAdultChart.Checked
        UpdatePatientDisplay(currentPatient)
    End Sub
    Private Sub btnResetKid_Click(sender As Object, e As EventArgs) Handles btnResetKid.Click
        ResetToAutoSelection()
    End Sub
    ' Add a method to reset to automatic age-based selection
    Public Sub ResetToAutoSelection()
        manualOverrideActive = False
        If PatientBS.Count > 0 Then 'AndAlso CurrentPatient IsNot Nothing
            UpdatePatientDisplay(CType(PatientBS.Current, Patient))
        End If
    End Sub
    ' In your main form
    Public Event KidStatusChanged(kidText As String, isKid As Boolean, isManual As Boolean)
    ' Helper method to update UI consistently
    Private Sub UpdatePatientDisplay(patient As Patient)
        If manualOverrideActive Then
            ' When in manual mode, use the override value
            Kid = manualKidStatus
            Dim lblEn As String = If(Kid, "Kid (Manual)", "Adult (Manual)")
            Dim lblAr As String = If(Kid, "طفل (يدوي)", "بالغ (يدوي)")
            IsKidLabel.Text = If(Eng, lblEn, lblAr)
            If IsKidLabel.Text.Contains("(Manual)") OrElse IsKidLabel.Text.Contains("(يدوي)") Then
                IsKidLabel.ForeColor = Color.Red
            Else
                IsKidLabel.ForeColor = If(Kid, Color.Green, Color.Blue)
            End If
            btnResetKid.Visible = True
        Else
            ' In auto mode, use age-based selection
            Kid = NavigatorAutoKidFromPatient(patient)
            Dim lblEn As String = If(Kid, "Kid", "Adult")
            Dim lblAr As String = If(Kid, "طفل", "بالغ")
            IsKidLabel.Text = If(Eng, lblEn, lblAr)
            If Not IsKidLabel.Text.Contains("(Manual)") OrElse Not IsKidLabel.Text.Contains("(يدوي)") Then
                IsKidLabel.ForeColor = If(Kid, Color.Green, Color.Blue)
            Else
                IsKidLabel.ForeColor = Color.Red
            End If
            'IsKidLabel.ForeColor = If(Kid, Color.Green, Color.Blue)
            btnResetKid.Visible = False
        End If
        Module1.isKid = Kid
        UpdateButtonAppearance()
        OnPatientSelected(patient, Kid, True)
    End Sub
    ' Helper method to update button appearance
    Private Sub UpdateButtonAppearance()
        If btnAdultChart.Checked = True Then
            Dim txtEn As String = "Adult Chart"
            Dim txtAr As String = "مخطط البالغين"
            Dim txt As String = If(Eng, txtEn, txtAr)
            btnAdultChart.Text = txt
        Else
            Dim txtEn As String = "Kid Chart"
            Dim txtAr As String = "مخطط الأطفال"
            Dim txt As String = If(Eng, txtEn, txtAr)
            btnAdultChart.Text = txt
        End If
    End Sub
#End Region
#End Region

#Region "PatientAddUpdateDeleteControls"

    Private Sub MergeInsertedPatientIntoNavigator(newPatient As Patient)
        _listState.AllPatients.Add(newPatient)
        _listState.RebuildFilteredFromAll()
        Dim matchesFilter = _listState.FilteredPatients.Exists(Function(p) p.PatientID = newPatient.PatientID)
        If matchesFilter Then
            _bindingPatients.Add(newPatient)
            Dim newIndex As Integer = _bindingPatients.Count - 1
            PatientBS.Position = newIndex
        End If
        If HasActiveSearchQuery() Then
            Dim selectPatientIdAfterClear As Integer? = Nothing
            If matchesFilter Then
                selectPatientIdAfterClear = newPatient.PatientID
            Else
                Dim curKeep As Patient = TryCast(PatientBS.Current, Patient)
                If curKeep IsNot Nothing AndAlso curKeep.PatientID > 0 Then selectPatientIdAfterClear = curKeep.PatientID
            End If
            ClearPatientSearchBox()
            ApplyFilter(suppressPatientBroadcast:=True, selectPatientIdAfterRefresh:=selectPatientIdAfterClear)
        End If
        If matchesFilter Then _listState.ParkBindingEmptyUntilUserQuery = False
        _popupOpeningForAdd = False
        HidePatientFlyoutSafe()
        UpdateNavigationControls()
        FirePatientChangedForCurrent()
    End Sub

    Private Sub MergeUpdatedPatientIntoNavigator(updated As Patient)
        Dim idxAll As Integer = _listState.AllPatients.FindIndex(Function(p) p.PatientID = updated.PatientID)
        If idxAll >= 0 Then _listState.AllPatients(idxAll) = updated
        _listState.RebuildFilteredFromAll()
        Dim rawSearch = If(txtPatientName Is Nothing, "", txtPatientName.Text)
        Dim suppressB = rawSearch.Trim().Length > 0
        RefreshBindingListFromFiltered(rawSearch, suppressPatientBroadcast:=suppressB, parkNoCurrentPatient:=False, selectPatientIdAfterRefresh:=updated.PatientID)
        _currentPatient = updated
        UpdateNavigationControls()
        UpdateBoundControlsExceptName(updated)
        UpdateKidStateOnly(updated)
        PatientEventManager.RaisePatientChanged(updated, Kid, True)
    End Sub

    Private Sub RemovePatientFromNavigatorAfterDelete(toDelete As Patient)
        Dim fallbackPatientId As Integer? = Nothing
        Dim idxInBinding As Integer = -1
        For i As Integer = 0 To _bindingPatients.Count - 1
            If CType(_bindingPatients(i), Patient).PatientID = toDelete.PatientID Then
                idxInBinding = i
                Exit For
            End If
        Next

        If idxInBinding > 0 Then
            fallbackPatientId = _bindingPatients(idxInBinding - 1).PatientID
        ElseIf idxInBinding >= 0 AndAlso idxInBinding < _bindingPatients.Count - 1 Then
            fallbackPatientId = _bindingPatients(idxInBinding + 1).PatientID
        End If

        If _listState.AllPatients IsNot Nothing Then
            Dim idxInAll As Integer = _listState.AllPatients.FindIndex(Function(p) p.PatientID = toDelete.PatientID)
            If idxInAll >= 0 Then _listState.AllPatients.RemoveAt(idxInAll)
        End If

        If _listState.FilteredPatients IsNot Nothing Then
            Dim idxInFiltered As Integer = _listState.FilteredPatients.FindIndex(Function(p) p.PatientID = toDelete.PatientID)
            If idxInFiltered >= 0 Then _listState.FilteredPatients.RemoveAt(idxInFiltered)
        End If

        ClearPatientSearchBox()
        RefreshBindingListFromFiltered(Nothing, suppressPatientBroadcast:=False, parkNoCurrentPatient:=False, selectPatientIdAfterRefresh:=fallbackPatientId)
        _currentPatient = If(PatientBS.Count > 0, TryCast(PatientBS.Current, Patient), Nothing)

        HidePatientFlyoutSafe()
        UpdateNavigationControls()
        FirePatientChangedForCurrent()
    End Sub

    Private Sub InsertPatient()

        Dim usr As Boolean = False
        If Not CheckTxt() Then Exit Sub
        If CurrentUser IsNot Nothing Then
            ' Use CurrentUser.UsID
            usr = True
        Else
            MsgBox("You are adding a patient without a user logged in, Default will be used.")
        End If
        Select Case _Target
            'Case "Treat"
            '    TreatCheckBox.Checked = True
            'Case "Implant"
            '    ImplantCheckBox.Checked = True
            'Case "Mobile"
            '    MobileCheckBox.Checked = True
            'Case "Ortho"
            '    OrthoCheckBox.Checked = True
            'Case "Diag"
            '    DiagCheck.Checked = True
        End Select
        '' If WhatsApp is empty and phone starts with 05, default WhatsApp to phone
        'If txtWhats IsNot Nothing AndAlso String.IsNullOrWhiteSpace(txtWhats.Text) AndAlso
        '   PhoneTextEdit IsNot Nothing AndAlso Not String.IsNullOrWhiteSpace(PhoneTextEdit.Text) AndAlso
        '   PhoneTextEdit.Text.Trim().StartsWith("05") Then
        '    txtWhats.Text = PhoneTextEdit.Text.Trim()
        'End If

        'Dim prefixStored As String = WhatsHelper.GetPrefixTextForStorage(cboPrefix)
        'Dim localW As String = WhatsHelper.NormalizeLocalWhatsTenDigitsForStorage(If(txtWhats?.Text, "").ToString())

        'Dim clsPatient As New Patient With {
        '    .PatientName = PatientNameTextEdit.Text,
        '    .Sex = SexTextBox.Text,
        '    .Age = If(IsNumeric(AgeSpinEdit.Value), CInt(AgeSpinEdit.Value), CType(Nothing, Integer?)),
        '    .IsKid = If(IsNumeric(AgeSpinEdit.Value), CInt(AgeSpinEdit.Value) < 10, False),
        '    .Phone = PhoneTextEdit.Text,
        '    .WhatsAppPrefix = prefixStored,
        '    .WhatsApp = localW,
        '    .Address = AddressTextEdit.Text,
        '    .Health = HealthTextBox.Text,
        '    .Treat = TreatCheckBox.Checked,
        '    .Implant = ImplantCheckBox.Checked,
        '    .Mobile = MobileCheckBox.Checked,
        '    .Ortho = OrthoCheckBox.Checked,
        '    .Diag = DiagCheck.Checked,
        '    .Struc = StrucCheckBox.Checked,
        '    .Notes = NotesTextEdit.Text,
        '    .BirthY = BirthYSpinEdit.Value,
        '    .CreatedBy = If(usr, CurrentUser.UsID, 1),
        '    .CreateDate = Now
        '}
        'If Not String.IsNullOrWhiteSpace(lblPNum.Text) Then clsPatient.PatientNumber = lblPNum.Text.Trim()
        Dim sw As New Stopwatch()
        'sw.Start()
        'Dim committed As Integer = _patientData.InsertPatient(clsPatient)
        'sw.Stop()
        LogToFile("📌 InsertPatient took: " & sw.Elapsed.TotalMilliseconds & " ms", Me)
        'If committed <= 0 Then
        '    Select Case committed
        '        Case -2
        '            MsgBox(If(Eng, "Patient with this name already exists.", "مريض بهذا الاسم موجود بالفعل."), MsgBoxStyle.Exclamation)
        '        Case -3
        '            MsgBox(If(Eng, "Failed to generate patient number. Please try again.", "فشل في إنشاء رقم المريض. يرجى المحاولة مرة أخرى."), MsgBoxStyle.Exclamation)
        '        Case Else
        '            MsgBox(If(Eng, "Failed to add patient.", "فشل في إضافة المريض."), MsgBoxStyle.Exclamation)
        '    End Select
        '    Return
        'End If
        ' InsertPatient returns commit status; get new ID from last inserted patient
        Dim newPatient As Patient = _patientData.Select_LastPatient()
        If newPatient Is Nothing Then
            MsgBox(If(Eng, "Patient added but could not load.", "تمت الإضافة لكن تعذر تحميل البيانات."), MsgBoxStyle.Exclamation)
            Return
        End If
        ' Ensure PatientNumber was persisted (defensive check)
        If String.IsNullOrWhiteSpace(newPatient.PatientNumber) Then
            MsgBox(If(Eng, "Patient was added but patient number is missing. Please contact support.", "تمت إضافة المريض لكن رقم الملف مفقود. يرجى التواصل مع الدعم."), MsgBoxStyle.Exclamation)
            Return
        End If
        MergeInsertedPatientIntoNavigator(newPatient)
    End Sub

    Private Sub HealthTextBox_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'If Eng Then
        '    If Me.HealthTextBox.Text = "Healthy" Or Me.HealthTextBox.Text = "سليم" Then
        '        Me.HealthTextBox.ForeColor = Color.Blue
        '    Else
        '        Me.HealthTextBox.ForeColor = Color.Red
        '    End If
        'Else
        '    If Me.HealthTextBox.Text = "Healthy" Or Me.HealthTextBox.Text = "سليم" Then
        '        Me.HealthTextBox.ForeColor = Color.Blue
        '    Else
        '        Me.HealthTextBox.ForeColor = Color.Red
        '    End If
        'End If
    End Sub
    Private Sub SexTextBox_TextChanged(sender As Object, e As EventArgs)
        'Try
        '    If Me.SexTextBox.Text = "ذكر" Or Me.SexTextBox.Text = "Male" Then
        '        Me.PicBox.Image = My.Resources.Male
        '    ElseIf Me.SexTextBox.Text = "أنثى" Or Me.SexTextBox.Text = "Female" Or Me.SexTextBox.Text = "انثى" Then
        '        Me.PicBox.Image = My.Resources.Female
        '    Else
        '        Me.PicBox.Image = PicBox.InitialImage
        '    End If
        'Catch ex As Exception
        '    NotifyNonFatal("Patient", "المريض",
        '        "Could not update the gender icon.",
        '        "تعذر تحديث أيقونة الجنس.",
        '        ex, MessageBoxIcon.Information)
        'End Try
    End Sub
    Private Sub AgeSpinEdit_EditValueChanged(sender As Object, e As EventArgs)
        'Dim x As Integer = CInt(AgeSpinEdit.EditValue)
        'If x >= 3 And x <= 10 Then
        '    Kid = True
        'ElseIf x > 10 Then
        '    Kid = False
        'End If
        'BirthYSpinEdit.EditValue = Now.Year - x
        'If _currentPatient IsNot Nothing Then _currentPatient.Age = x
        'If _currentPatient IsNot Nothing AndAlso _currentPatient.Age < 150 Then
        '    Me.LabelAge.Text = If(Eng, _currentPatient.Age.ToString & " Yrs", _currentPatient.Age.ToString & "  سنة ")
        'Else
        '    Me.LabelAge.Text = If(Eng, "Age Not Set", "العمر غير محدد")
        'End If
    End Sub
    Private Sub BirthYSpinEdit_EditValueChanged(sender As Object, e As EventArgs)
        'If BirthYSpinEdit.EditValue = 0 Then Exit Sub
        '' Temporarily disable the AgeSpinEdit event handler to avoid recursion
        'RemoveHandler AgeSpinEdit.EditValueChanged, AddressOf AgeSpinEdit_EditValueChanged
        'Dim x As Integer = CInt(BirthYSpinEdit.EditValue)
        'Dim y As Integer = Now.Year
        'x = y - x
        'If y - x >= 3 And y - x <= 10 Then
        '    Kid = True
        'ElseIf y - x > 10 Then
        '    Kid = False
        'End If
        'AgeSpinEdit.EditValue = x
        'If _currentPatient IsNot Nothing Then _currentPatient.Age = x
        'If _currentPatient IsNot Nothing AndAlso _currentPatient.Age < 150 Then
        '    Me.LabelAge.Text = If(Eng, _currentPatient.Age.ToString & " Yrs", _currentPatient.Age.ToString & "  سنة ")
        'Else
        '    Me.LabelAge.Text = If(Eng, "Age Not Set", "العمر غير محدد")
        'End If
        '' Re-enable the AgeSpinEdit event handler
        'AddHandler AgeSpinEdit.EditValueChanged, AddressOf AgeSpinEdit_EditValueChanged
    End Sub
    Private Sub PatientIDEdit_EditValueChanged(sender As Object, e As EventArgs)
        'PatientID = CInt(PatientIDEdit.EditValue)
    End Sub
    Private Function CheckTxt() As Boolean
        'Dim errorMessages As New List(Of String)()
        '' Check Patient Name
        'If String.IsNullOrWhiteSpace(PatientNameTextEdit.Text) Then
        '    errorMessages.Add(If(Eng, "Patient name is required", "اسم المريض مطلوب"))
        '    PatientNameTextEdit.Focus()
        '    ' Check Age
        'ElseIf AgeSpinEdit.Value <= 0 Then
        '    Dim message As String = If(AgeSpinEdit.Value = 0,
        '                        If(Eng, "Age cannot be zero", "العمر لا يمكن أن يكون صفر"),
        '                        If(Eng, "Age cannot be negative", "العمر لا يمكن أن يكون سالب"))
        '    errorMessages.Add(message)
        '    AgeSpinEdit.Focus()
        '    ' Check Health
        '    'ElseIf String.IsNullOrWhiteSpace(HealthTextBox.Text) Then
        '    '    errorMessages.Add(If(Eng, "Health information is required", "المعلومات الصحية مطلوبة"))
        '    '    HealthTextBox.Focus()
        '    '    ' Check Address
        '    'ElseIf String.IsNullOrWhiteSpace(AddressTextEdit.Text) Then
        '    '    errorMessages.Add(If(Eng, "Address is required", "العنوان مطلوب"))
        '    '    AddressTextEdit.Focus()
        'End If
        ' Check Age
        'If Me.LabelAge.Text = If(Eng, "Age Not Set", "العمر غير محدد") Then
        '    errorMessages.Add(If(Eng, "Age is required", "العمر مطلوب"))
        '    AddressTextEdit.Focus()
        'End If
        ' Check Number
        'If Me.lblPNum.Text.Length < 5 Then
        '    errorMessages.Add(If(Eng, "Patient Number is required", "رقم المريض مطلوب"))
        '    lblPNum.Focus()
        'End If
        '' Show error message if any validation failed
        'If errorMessages.Count > 0 Then
        '    Dim messageTitle As String = If(Eng, "Validation Error", "خطأ في التحقق")
        '    Dim fullMessage As String = String.Join(vbCrLf, errorMessages)
        '    MessageBox.Show(fullMessage, messageTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning)
        '    Return False
        'End If
        'Return True
    End Function
    Private Sub CboCity_CityValueChanged(sender As Object, e As CityCombo.CityIndexChangedEvent)
        'If CboCity.CityID = -1 Then
        '    AddressTextEdit.Text = ""
        'Else
        '    AddressTextEdit.Text = CboCity.CityName
        'End If
    End Sub
    Private Sub CboHealth_SelectedIndexChanged(sender As Object, e As HlthCombo.HealthIndexChangedEvent)
        'If CboHealth.HID = -1 Then
        '    HealthTextBox.Text = ""
        'Else
        '    HealthTextBox.Text = CboHealth.HealthStat
        'End If
    End Sub
    Property PatientUpdated As Boolean = False
    Private Sub btnAdd_Click(sender As Object, e As EventArgs)
        'If Not CheckTxt() Then Exit Sub
        '' Add new patient when no current patient or PatientID is empty/zero
        'If _currentPatient Is Nothing OrElse String.IsNullOrWhiteSpace(PatientIDEdit.Text) OrElse CInt(Val(PatientIDEdit.Text)) <= 0 Then
        '    InsertPatient()
        '    Return
        'End If
    End Sub

    Private Sub btnUpdatePatient_Click(sender As Object, e As EventArgs)
        'Try
        '    If Not CheckTxt() Then Exit Sub
        '' Add new patient when no current patient or PatientID is empty/zero
        'If _currentPatient Is Nothing OrElse String.IsNullOrWhiteSpace(PatientIDEdit.Text) OrElse CInt(Val(PatientIDEdit.Text)) <= 0 Then
        '    InsertPatient()
        '    Return
        'End If

        '    Dim prefixStored As String = WhatsHelper.GetPrefixTextForStorage(cboPrefix)
        '    Dim localW As String = WhatsHelper.NormalizeLocalWhatsTenDigitsForStorage(If(txtWhats?.Text, "").ToString())

        '    Dim oldPatient As Patient = _currentPatient
        '    Dim updated As New Patient With {
        '    .PatientID = oldPatient.PatientID,
        '    .Address = AddressTextEdit.Text,
        '    .PatientNumber = lblPNum.Text,
        '    .Age = AgeSpinEdit.Value,
        '    .BirthY = BirthYSpinEdit.Value,
        '    .Health = HealthTextBox.Text,
        '    .Implant = ImplantCheckBox.Checked,
        '    .Notes = NotesTextEdit.Text,
        '    .Mobile = MobileCheckBox.Checked,
        '    .Ortho = OrthoCheckBox.Checked,
        '    .Sex = SexTextBox.Text,
        '    .Diag = DiagCheck.Checked,
        '    .Struc = StrucCheckBox.Checked,
        '    .Treat = TreatCheckBox.Checked,
        '    .PatientName = PatientNameTextEdit.Text,
        '    .Phone = PhoneTextEdit.Text,
        '    .WhatsAppPrefix = prefixStored,
        '    .WhatsApp = localW,
        '    .CreatedBy = oldPatient.CreatedBy,
        '    .CreateDate = oldPatient.CreateDate
        '}

        '    Me.Cursor = Cursors.WaitCursor
        '    btnUpdatePatient.Enabled = False
        '    Try
        '        If _patientData.Update(oldPatient, updated) Then
        '            MergeUpdatedPatientIntoNavigator(updated)
        '            HidePatientFlyoutSafe()
        '        Else
        '            MsgBox(If(Eng, "Update operation failed. Please try again.", "فشل التحديث. يرجى المحاولة مرة أخرى."), MsgBoxStyle.Exclamation)
        '        End If
        '    Finally
        '        Me.Cursor = Cursors.Default
        '        btnUpdatePatient.Enabled = True
        '    End Try
        'Catch ex As Exception
        '    NotifyNonFatal("Patient", "المريض",
        '        "An error occurred while updating the patient.",
        '        "حدث خطأ أثناء تحديث بيانات المريض.",
        '        ex, MessageBoxIcon.Error)
        'End Try
    End Sub
    Property PatientDeleted As Boolean = False
    Private Sub btnDelete_Click(sender As Object, e As EventArgs)
        Try
            If _currentPatient Is Nothing Then Return
            Dim toDelete As Patient = _currentPatient
            Dim msg As String = If(Eng,
                $"Are you sure you want to delete this Patient?{vbCrLf}{toDelete.PatientName}{vbCrLf}File Number : {toDelete.PatientNumber}{vbCrLf} This Operation Is PERMANENT... It Can't Be UNDONE!!",
                $"هل انت متاكد من حذف هذا المريض؟{vbCrLf}{toDelete.PatientName}{vbCrLf} صاحب ملف رقم : {toDelete.PatientNumber}{vbCrLf}هذا الإجراء نهائي... لا يمكن التراجع!")
            Dim title As String = If(Eng, "Warning... Patient DELETE!!!", "تحذير... حذف مريض!")
            Dim response As MsgBoxResult = MsgBox(msg, MsgBoxStyle.DefaultButton2 Or MsgBoxStyle.Critical Or MsgBoxStyle.YesNo, title)
            If response <> DialogResult.Yes Then Return
            Using confirmDialog As New DoubleConfirmDialog() With {.Text = "Patient Record  Deletion From"}
                confirmDialog.Message = "FINAL WARNING: This will permanently delete { " & toDelete.PatientName & " } Record from your Archive?. Check the box to confirm."
                If confirmDialog.ShowDialog(Me) <> DialogResult.OK OrElse Not confirmDialog.IsConfirmed Then Return
            End Using
            If Not _patientData.Delete(toDelete) Then
                MsgBox(If(Eng, "Delete operation failed.", "فشل الحذف."), MsgBoxStyle.Exclamation)
                Return
            End If

            RemovePatientFromNavigatorAfterDelete(toDelete)
            MsgBox(If(Eng, $"Patient {toDelete.PatientName} has been deleted.", $"تم حذف المريض {toDelete.PatientName}."), MsgBoxStyle.Information)
        Catch ex As Exception
            MsgBox(If(Eng, $"Error deleting patient: {ex.Message}", $"خطأ في الحذف: {ex.Message}"))
        End Try
    End Sub
    Private Sub SexTextBox_EditValueChanged(sender As Object, e As EventArgs)
        'If String.IsNullOrEmpty(SexTextBox.Text) Then
        '    RadioMale.Checked = True
        'ElseIf SexTextBox.Text = "Male" OrElse SexTextBox.Text = "ذكر" Then
        '    RadioMale.Checked = True
        'ElseIf SexTextBox.Text = "Female" OrElse SexTextBox.Text = "أنثى" OrElse SexTextBox.Text = "انثى" Then
        '    RadioFemale.Checked = True
        'End If
    End Sub
    Private Sub RadioMale_CheckedChanged(sender As Object, e As EventArgs)
        'Dim maleEng, maleAr As String
        'maleAr = "ذكر"
        'maleEng = "Male"
        'If RadioMale.Checked = True Then
        '    If Eng Then
        '        SexTextBox.Text = maleEng
        '    Else
        '        SexTextBox.Text = maleAr
        '    End If
        'End If
    End Sub
    Private Sub RadioFemale_CheckedChanged(sender As Object, e As EventArgs)
        'Dim femEng, femAr As String
        'femAr = "أنثى"
        'femEng = "Female"
        'If RadioFemale.Checked = True Then
        '    If Eng Then
        '        SexTextBox.Text = femEng
        '    Else
        '        SexTextBox.Text = femAr
        '    End If
        'End If
    End Sub

#End Region

#Region "Whats"
    ''' <summary>
    ''' Extracts the 10 local digits starting with 0 from a full international number
    ''' for display in txtWhats (no prefix shown in the textbox).
    ''' Internally we work with 9 local digits without the leading 0; this function
    ''' converts that to 10 digits by prepending 0 when displaying.
    ''' </summary>
    Private Shared Function FullNumberToLocal9Digits(fullNumber As String) As String
        If String.IsNullOrWhiteSpace(fullNumber) Then Return ""

        ' Keep only digits from the stored number (could be with prefix like 970 or 972)
        Dim digits As String = New String(fullNumber.Where(Function(c) Char.IsDigit(c)).ToArray())
        If String.IsNullOrWhiteSpace(digits) Then Return ""

        ' First derive the 9 local digits (without leading 0)
        Dim local9 As String = ""

        If digits.Length = 9 Then
            ' Already 9-digit local number without 0
            local9 = digits
        ElseIf digits.Length = 10 AndAlso digits.StartsWith("0"c) Then
            ' 10-digit local starting with 0 (e.g. 0599xxxxxx) => strip 0 to get 9 digits
            local9 = digits.Substring(1, 9)
        ElseIf digits.Length >= 12 AndAlso (digits.StartsWith("970") OrElse digits.StartsWith("972")) Then
            ' Palestine/Israel stored as full international 970/972 + 9 local digits
            local9 = digits.Substring(3, 9)
        ElseIf digits.Length > 9 Then
            ' Fallback: take the last 9 digits as the local part
            local9 = digits.Substring(digits.Length - 9, 9)
        Else
            ' Shorter than 9 digits, use as-is
            local9 = digits
        End If

        If String.IsNullOrWhiteSpace(local9) Then Return ""

        ' For txtWhats we always show 10 digits starting with 0
        If local9.Length = 9 Then
            Return "0" & local9
        End If

        ' If for some reason we didn't get 9 digits, just return what we have
        Return local9
    End Function

    ''' <summary>
    ''' Builds full WhatsApp number:
    ''' - txtWhats shows 10 local digits starting with 0 (e.g. 0599xxxxxx)
    ''' - Here we strip the leading 0 so we work with 9 local digits (e.g. 599xxxxxx)
    ''' - Then we prepend the selected country prefix (e.g. 970599xxxxxx).
    ''' lblWhats shows this full number.
    ''' </summary>
    Private Function GetFullWhatsNumber() As String
        Dim number As String = ""
        'If txtWhats IsNot Nothing AndAlso txtWhats.Text IsNot Nothing Then
        '    number = txtWhats.Text.ToString().Trim()
        'End If
        'Dim localDigits As String = New String(number.Where(Function(ch) Char.IsDigit(ch)).ToArray())

        '' Remove any leading zeros (we expect 0 + 9 digits in txtWhats; we only want the 9 digits part).
        'While localDigits.StartsWith("0"c) AndAlso localDigits.Length > 0
        '    localDigits = localDigits.Substring(1)
        'End While

        '' Keep at most the last 9 digits as the local number (safety for overlong input).
        'If localDigits.Length > 9 Then
        '    localDigits = localDigits.Substring(localDigits.Length - 9, 9)
        'End If

        'If cboPrefix Is Nothing OrElse cboPrefix.EditValue Is Nothing Then
        '    Return localDigits
        'End If
        'Dim rawPrefix As String = cboPrefix.EditValue.ToString()
        'Dim prefixDigits As String = New String(rawPrefix.Where(Function(ch) Char.IsDigit(ch)).ToArray())
        'If String.IsNullOrWhiteSpace(prefixDigits) Then Return localDigits
        'If localDigits.Length = 0 Then Return ""
        'Return prefixDigits & localDigits
    End Function
    ''' <summary>Updates lblWhats with the full WhatsApp number (prefix + txtWhats). Call after load or when prefix/number changes.</summary>
    Private Sub RefreshLblWhats()
        'If lblWhats IsNot Nothing Then
        '    lblWhats.Text = GetFullWhatsNumber()
        'End If
    End Sub

    ''' <summary>Fills cboPrefix with country name and calling code. Palestine (970) and Israel (972) are first.</summary>
    Private Sub FillCboPrefixOnce()
        'WhatsHelper.FillCboPrefixOnce(cboPrefix)
    End Sub

    Private Sub cboPrefix_SelectedIndexChanged(sender As Object, e As EventArgs)
        RefreshLblWhats()
    End Sub

    Private Sub txtWhats_ValueChanged(sender As Object, e As EventArgs)
        RefreshLblWhats()
    End Sub

    Private Sub txtWhats_KeyDown(sender As Object, e As KeyEventArgs)
        ' Allow control keys: backspace, delete, arrows, tab, home/end, etc.
        If e.KeyCode = Keys.Back OrElse
           e.KeyCode = Keys.Delete OrElse
           e.KeyCode = Keys.Left OrElse
           e.KeyCode = Keys.Right OrElse
           e.KeyCode = Keys.Up OrElse
           e.KeyCode = Keys.Down OrElse
           e.KeyCode = Keys.Tab OrElse
           e.KeyCode = Keys.Home OrElse
           e.KeyCode = Keys.End Then
            Return
        End If

        ' Allow digits only (top row or numpad)
        Dim isTopRowDigit As Boolean = (e.KeyCode >= Keys.D0 AndAlso e.KeyCode <= Keys.D9)
        Dim isNumPadDigit As Boolean = (e.KeyCode >= Keys.NumPad0 AndAlso e.KeyCode <= Keys.NumPad9)

        ' Block Shift-modified digits (to avoid !, @, etc.)
        If (isTopRowDigit OrElse isNumPadDigit) AndAlso (Not e.Shift) Then
            Return
        End If

        ' Otherwise block the key
        e.SuppressKeyPress = True
        e.Handled = True
    End Sub

    ''' <summary>
    ''' Validates full WhatsApp number: digits only, prefix (no signs) + local number without leading 0.
    ''' For 970/972 expects 12 digits total (3 + 9). For other prefixes expects prefixLength + 9 digits.
    ''' If prefixDigits is empty (international input), requires 10–15 digits.
    ''' Returns empty string if valid; otherwise returns a message (in current language) to fix the number.
    ''' </summary>
    Private Function ValidateWhatsAppNumber(fullNumberDigits As String, prefixDigits As String) As String
        If String.IsNullOrWhiteSpace(fullNumberDigits) Then
            Return If(Eng, "Enter WhatsApp/phone number (digits only).", "أدخل رقم واتساب/الجوال (أرقام فقط).")
        End If
        If fullNumberDigits.Any(Function(c) Not Char.IsDigit(c)) Then
            Return If(Eng, "Number must contain only digits (no spaces, dashes or plus sign).", "يجب أن يحتوي الرقم على أرقام فقط (بدون مسافات أو شرطات أو +).")
        End If

        If String.IsNullOrWhiteSpace(prefixDigits) Then
            ' International-style number without combo prefix: require reasonable length
            If fullNumberDigits.Length < 10 OrElse fullNumberDigits.Length > 15 Then
                Return If(Eng, "Number must be 10–15 digits (e.g. 970599123456 for Palestine).", "يجب أن يكون الرقم 10–15 رقمًا (مثلاً 970599123456 لفلسطين).")
            End If
            Return ""
        End If

        Dim prefixLen As Integer = prefixDigits.Length
        ' Local mobile without leading 0 is typically 9 digits (e.g. Palestine/Israel). Total = prefix + 9.
        Dim expectedLen As Integer = prefixLen + 9
        If fullNumberDigits.Length <> expectedLen Then
            Dim msgEn As String = $"Invalid length. For prefix +{prefixDigits} use {prefixLen} (prefix) + 9 digits (number without leading 0) = {expectedLen} digits total. Current: {fullNumberDigits.Length}."
            Dim msgAr As String = $"طول غير صحيح. لرمز +{prefixDigits} استخدم {prefixLen} (الرمز) + 9 أرقام (الرقم بدون صفر في البداية) = {expectedLen} رقمًا. الحالي: {fullNumberDigits.Length}."
            Return If(Eng, msgEn, msgAr)
        End If
        Return ""
    End Function

#End Region





#Region "Resize"
    Private ReadOnly controlBoundsCache As New ConcurrentDictionary(Of Control, Rectangle)
    Private Const OriginalPanelWidth As Integer = 1214
    Private Const OriginalPanelHeight As Integer = 68
    Private ReadOnly originalPanelSize As New Size(OriginalPanelWidth, OriginalPanelHeight)
    Private originaMelSize As Size
    Private Sub StoreOriginalBounds(container As Control)
        For Each ctrl As Control In container.Controls
            controlBoundsCache.TryAdd(ctrl, ctrl.Bounds)
            If ctrl.HasChildren Then StoreOriginalBounds(ctrl)
        Next
    End Sub
    Private Sub ResizeControlsProportionally()
        If controlBoundsCache.IsEmpty Then Return
        Dim widthRatio = CSng(Me.Width) / OriginalPanelWidth
        Dim heightRatio = CSng(Me.Height) / OriginalPanelHeight
        For Each kvp In controlBoundsCache
            kvp.Key.SetBounds(
                CInt(kvp.Value.X * widthRatio),
                CInt(kvp.Value.Y * heightRatio),
                CInt(kvp.Value.Width * widthRatio),
                CInt(kvp.Value.Height * heightRatio))
        Next
    End Sub
    Private Sub Navigator3_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        If controlBoundsCache.IsEmpty Then Return
        If _resizeCoalesceTimer Is Nothing Then
            _resizeCoalesceTimer = New System.Windows.Forms.Timer With {.Interval = ResizeCoalesceMs}
            AddHandler _resizeCoalesceTimer.Tick, AddressOf OnResizeCoalesceTick
        End If
        _resizeCoalesceTimer.Stop()
        _resizeCoalesceTimer.Start()
    End Sub

    Private Sub OnResizeCoalesceTick(sender As Object, e As EventArgs)
        If _resizeCoalesceTimer IsNot Nothing Then _resizeCoalesceTimer.Stop()
        If Me.IsDisposed OrElse Not Me.IsHandleCreated Then Return
        Try
            ResizeControlsProportionally()
        Catch ex As Exception
            NotifyNonFatal("Navigator layout", "تخطيط المتصفح",
                "The patient header could not resize correctly. If controls overlap, resize the main window slightly or restart the program.",
                "تعذر تغيير حجم رأس المريض بشكل صحيح. إذا تداخلت العناصر، غيّر حجم النافذة قليلاً أو أعد تشغيل البرنامج.",
                ex, MessageBoxIcon.Warning)
        End Try
    End Sub
#End Region

#Region "GetImgs"
    Private Sub SetImgs()
        Me.btNewPatient.ImageOptions.Image = GetAdd32()
        Me.btnFirst.ImageOptions.Image = GetFirst()
        If Eng Then
            Me.btnFirst.ImageOptions.Image.RotateFlip(RotateFlipType.RotateNoneFlipNone)
        Else
            Me.btnFirst.ImageOptions.Image.RotateFlip(RotateFlipType.RotateNoneFlipX)
        End If
        Me.btnNext.ImageOptions.Image = GetNext()
        If Eng Then
            Me.btnNext.ImageOptions.Image.RotateFlip(RotateFlipType.RotateNoneFlipNone)
        Else
            Me.btnNext.ImageOptions.Image.RotateFlip(RotateFlipType.RotateNoneFlipX)
        End If
        Me.btnPrev.ImageOptions.Image = GetPrev()
        If Eng Then
            Me.btnPrev.ImageOptions.Image.RotateFlip(RotateFlipType.RotateNoneFlipNone)
        Else
            Me.btnPrev.ImageOptions.Image.RotateFlip(RotateFlipType.RotateNoneFlipX)
        End If
        Me.btnLast.ImageOptions.Image = GetLast()
        If Eng Then
            Me.btnLast.ImageOptions.Image.RotateFlip(RotateFlipType.RotateNoneFlipNone)
        Else
            Me.btnLast.ImageOptions.Image.RotateFlip(RotateFlipType.RotateNoneFlipX)
        End If
    End Sub



    ''' <summary>User came back to the search box by Tab or programmatic focus. Arm one SelectAll on first left click; move caret to end so typing does not eat the first character.</summary>
    Private Sub txtPatientName_FocusSelectAll(sender As Object, e As EventArgs) Handles txtPatientName.Enter
        If _ignoreSearchSelectAllOnce Then Return
        _patientSearchSelectAllOnFirstLeftClick = True
        MovePatientSearchCaretToEnd()
    End Sub

    Private Sub txtPatientName_MouseClickFirstSelectAll(sender As Object, e As MouseEventArgs) Handles txtPatientName.MouseClick
        If e.Button <> MouseButtons.Left Then Return
        If _ignoreSearchSelectAllOnce Then Return
        If txtPatientName Is Nothing OrElse txtPatientName.IsDisposed Then Return
        If Not _patientSearchSelectAllOnFirstLeftClick Then Return
        txtPatientName.SelectAll()
        _patientSearchSelectAllOnFirstLeftClick = False
    End Sub

    Private Sub txtPatientName_DoubleClickSelectAll(sender As Object, e As EventArgs) Handles txtPatientName.DoubleClick
        If _ignoreSearchSelectAllOnce Then Return
        If txtPatientName Is Nothing OrElse txtPatientName.IsDisposed Then Return
        txtPatientName.SelectAll()
        _patientSearchSelectAllOnFirstLeftClick = False
    End Sub

    Private Sub MovePatientSearchCaretToEnd()
        If Me.IsDisposed OrElse Not Me.IsHandleCreated Then Return
        If txtPatientName Is Nothing OrElse txtPatientName.IsDisposed Then Return
        If Not txtPatientName.ContainsFocus Then Return
        If _ignoreSearchSelectAllOnce Then Return
        Try
            Dim t = If(txtPatientName.Text, "")
            txtPatientName.SelectionStart = t.Length
            txtPatientName.SelectionLength = 0
        Catch ex As Exception
            NotifyNonFatal("Search", "بحث",
                "The search box caret could not be positioned.",
                "تعذر وضع مؤشر البحث.",
                ex, MessageBoxIcon.Information)
        End Try
    End Sub

    ' OnSearchSelectAllTimerTick removed: any delayed SelectAll fights live typing in DevExpress TextEdit.

    Private Async Sub btnWhatsSend_Click(sender As Object, e As EventArgs) Handles btnWhatsSend.Click
        Try
            Await SendWhatsForCurrentPatientAsync().ConfigureAwait(True)
        Catch ex As Exception
            MessageBox.Show(ex.Message, If(Eng, "Error", "خطأ"), MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Async Function SendWhatsForCurrentPatientAsync() As Task
        If Not Await WhatsAppService.EnsureWhatsAppConnectedOrNotifyAsync(Me).ConfigureAwait(True) Then Return
        Using frm As New WhatsAppSender(_currentPatient)
            frm.Icon = GetIcon()
            frm.ShowDialog(Me)
        End Using
    End Function

    Private Sub FlyoutPatientInfo_ButtonClick(sender As Object, e As DevExpress.Utils.FlyoutPanelButtonClickEventArgs)
        If e.Button.Caption = "Close" OrElse e.Button.Caption = "إغلاق" Then
            _popupOpeningForAdd = False
            HidePatientFlyoutSafe()
        End If
    End Sub










#End Region

    ''' <summary>Application-wide pre-filter: dismiss suggestion popup when the user clicks outside it (navigator, workspace body, chrome).</summary>
    Private NotInheritable Class PatientSuggestionHeaderClickFilter
        Implements IMessageFilter
        Private ReadOnly _nav As Navigator3

        Public Sub New(nav As Navigator3)
            _nav = nav
        End Sub

        Public Function PreFilterMessage(ByRef m As Message) As Boolean Implements IMessageFilter.PreFilterMessage
            _nav.TryDismissPatientSuggestionsFromHeaderMouse(m)
            Return False
        End Function
    End Class

End Class

