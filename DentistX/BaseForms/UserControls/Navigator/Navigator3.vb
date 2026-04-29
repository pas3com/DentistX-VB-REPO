Imports System.Collections.Concurrent
Imports System.ComponentModel
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports System.Globalization
Imports System.Linq
Imports System.Text
Imports System.Text.RegularExpressions
Imports System.Threading
Imports System.Threading.Tasks
Imports System.Windows.Forms
Imports Dapper
Imports DentistX
Imports DevExpress.XtraBars.Docking2010.Views.WindowsUI
Imports DevExpress.XtraEditors

Public Class Navigator3
    Implements IPatientHeaderControl

#Region "PatientSearch"

    Public Shared ReadOnly PatientFilterAll As String = "ALL"
    Public Shared ReadOnly PatientFilterTreat As String = "Treat"
    Public Shared ReadOnly PatientFilterOrtho As String = "Ortho"
    Public Shared ReadOnly PatientFilterDiag As String = "Diag"
    Public Shared ReadOnly PatientFilterMobile As String = "Mobile"

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

    ''' <summary>Full list for current filter (ALL/Treat/Ortho/Diag/Mobile). Search filters this into _bindingPatients.</summary>
    Private _filteredPatients As List(Of Patient)
    Private _searchTimer As System.Windows.Forms.Timer
    Private Const SearchDebounceMs As Integer = 120
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
    ''' <summary>Set before PatientBS.Position changes inside RefreshBindingListFromFiltered so PositionChanged can skip global PatientChanged (avoids LoadPatientData stealing focus while typing).</summary>
    Private _pendingSuppressPatientBroadcast As Boolean
    ''' <summary>After a non-major module switch, first user keystroke promotes search scope to ALL.</summary>
    Private _promoteSearchScopeToAllOnNextUserType As Boolean
    ''' <summary>Small button at the right edge of txtPatientName to reopen suggestions.</summary>
    Private _showResultsButton As SimpleButton
    ''' <summary>Patients currently shown in the suggestion popup (may differ from <see cref="_bindingPatients"/> when the binding list is left empty after load).</summary>
    Private _lastSuggestionPatients As List(Of Patient)

    ''' <summary>Matches MainView3 <c>UseHdrCheckItem</c>: checked (new header) → park after load; unchecked (old header) → bind list and select first patient.</summary>
    Private Function ShouldParkNoCurrentPatientAfterBackgroundLoad() As Boolean
        Return Not BasePatientWorkspace.UseHdrTestModHeader
    End Function

    ''' <summary>Apply filter to _allPatients and refresh PatientBS list.</summary>
    ''' <param name="parkNoCurrentPatient">When True, keep <see cref="_bindingPatients"/> empty so <see cref="PatientBS"/> has no current row (.NET <see cref="BindingSource"/> forces <c>Position = 0</c> whenever the list is non-empty, so “park at -1” is done by not binding rows yet).</param>
    Private Sub ApplyFilter(Optional parkNoCurrentPatient As Boolean = False)
        If _allPatients Is Nothing Then Return
        ' DB columns are nullable; treat NULL as False (do not use "= True" on Nullable — can throw).
        Select Case _Target
            Case PatientFilterTreat
                _filteredPatients = _allPatients.Where(Function(p) p.Treat.GetValueOrDefault(False)).ToList()
            Case PatientFilterOrtho
                _filteredPatients = _allPatients.Where(Function(p) p.Ortho.GetValueOrDefault(False)).ToList()
            Case PatientFilterDiag
                _filteredPatients = _allPatients.Where(Function(p) p.Diag.GetValueOrDefault(False)).ToList()
            Case PatientFilterMobile
                _filteredPatients = _allPatients.Where(Function(p) p.Mobile.GetValueOrDefault(False)).ToList()
            Case Else
                _filteredPatients = New List(Of Patient)(_allPatients)
        End Select
        ' Same rule as OnSearchTimerTick: non-empty search text must not broadcast PatientChanged (e.g. when
        ' background patient load finishes and ApplyFilter runs while the user is already typing — otherwise
        ' workspace LoadPatientData steals focus from the search box after the first character).
        Dim raw = If(txtPatientName Is Nothing, "", txtPatientName.Text).Trim()
        RefreshBindingListFromFiltered(If(txtPatientName Is Nothing, "", txtPatientName.Text), suppressPatientBroadcast:=raw.Length > 0, parkNoCurrentPatient:=parkNoCurrentPatient)
        UpdateNavigationControls()
    End Sub

    ''' <summary>Patients matching category filter plus optional search text (same rules as <see cref="RefreshBindingListFromFiltered"/>).</summary>
    Private Function BuildSearchFilteredPatientList(Optional searchText As String = Nothing) As List(Of Patient)
        Dim result As New List(Of Patient)()
        If _filteredPatients Is Nothing Then Return result
        Dim source As IEnumerable(Of Patient) = _filteredPatients

        Dim raw As String
        If searchText IsNot Nothing Then
            raw = searchText.Trim()
        ElseIf txtPatientName IsNot Nothing Then
            raw = If(txtPatientName.Text, "").Trim()
        Else
            raw = ""
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

    ''' <summary>Copy _filteredPatients into _bindingPatients using one query string: letters filter name (contains); digits filter file # (suffix). Updates pos/count.</summary>
    ''' <param name="suppressPatientBroadcast">When True, update header only — do not raise PatientChanged (prevents workspace LoadPatientData / body controls from stealing focus while user types in search).</param>
    ''' <param name="parkNoCurrentPatient">When True, clear <see cref="_bindingPatients"/> only (no rows) so there is no current patient until the user searches or picks from the list.</param>
    ''' <param name="selectPatientIdAfterRefresh">When set, after refill move to that patient in one step (avoids briefly selecting the first row when committing a suggestion).</param>
    Private Sub RefreshBindingListFromFiltered(Optional searchText As String = Nothing, Optional suppressPatientBroadcast As Boolean = False, Optional parkNoCurrentPatient As Boolean = False, Optional selectPatientIdAfterRefresh As Integer? = Nothing)
        If _filteredPatients Is Nothing OrElse _bindingPatients Is Nothing Then Return
        Dim sourceList = BuildSearchFilteredPatientList(searchText)

        ' CRITICAL: set the suppress flag BEFORE any mutation. ResetBindings below fires ListChanged(Reset),
        ' which PatientBS turns into PositionChanged -> FirePatientChangedForCurrent. If the flag is still
        ' False at that point, PatientChanged is broadcast -> workspace LoadPatientData -> focus is stolen
        ' from the search box after the very first letter.
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
                If newPos < 0 Then newPos = 0
            Else
                newPos = 0
            End If
            PatientBS.Position = newPos
            ' BindingSource may not raise PositionChanged when position stays the same; still refresh header.
            If oldPos = newPos Then
                FirePatientChangedForCurrent(suppressPatientBroadcast)
            End If
        Finally
            _pendingSuppressPatientBroadcast = False
        End Try
        UpdateSearchResultsButtonVisibility()
        UpdateNavigationControls()
    End Sub

    ''' <summary>Search mode: 0 = first (StartsWith), 1 = any (contains), 2 = last (EndsWith). Unified box uses contains (1) for name text.</summary>
    Private Shared Function NameMatchesSearch(name As String, q As String, searchMethod As Integer) As Boolean
        If String.IsNullOrEmpty(name) Then Return False
        Select Case searchMethod
            Case 0
                Return name.StartsWith(q, StringComparison.OrdinalIgnoreCase)
            Case 1
                Return name.IndexOf(q, StringComparison.OrdinalIgnoreCase) >= 0
            Case 2
                Return name.EndsWith(q, StringComparison.OrdinalIgnoreCase)
            Case Else
                Return name.IndexOf(q, StringComparison.OrdinalIgnoreCase) >= 0
        End Select
    End Function

    Private Shared Function IsMajorNavigatorFilterTarget(filterTarget As String) As Boolean
        If String.IsNullOrWhiteSpace(filterTarget) Then Return True
        Return String.Equals(filterTarget, PatientFilterAll, StringComparison.OrdinalIgnoreCase) OrElse
               String.Equals(filterTarget, PatientFilterTreat, StringComparison.OrdinalIgnoreCase) OrElse
               String.Equals(filterTarget, PatientFilterOrtho, StringComparison.OrdinalIgnoreCase) OrElse
               String.Equals(filterTarget, PatientFilterDiag, StringComparison.OrdinalIgnoreCase) OrElse
               String.Equals(filterTarget, PatientFilterMobile, StringComparison.OrdinalIgnoreCase)
    End Function

    Private Function HasActiveSearchQuery() As Boolean
        Return txtPatientName IsNot Nothing AndAlso Not String.IsNullOrWhiteSpace(If(txtPatientName.Text, ""))
    End Function

    Public Sub UpdateFilterTarget(filterTarget As String, Optional passedPatient As Patient = Nothing) Implements IPatientHeaderControl.UpdateFilterTarget
        Dim requestedTarget = If(String.IsNullOrEmpty(filterTarget), PatientFilterAll, filterTarget)
        If IsMajorNavigatorFilterTarget(requestedTarget) Then
            Dim isSameMajorTarget As Boolean = String.Equals(_Target, requestedTarget, StringComparison.OrdinalIgnoreCase)
            Dim hasActiveQuery As Boolean = HasActiveSearchQuery()
            _promoteSearchScopeToAllOnNextUserType = False
            If Not (isSameMajorTarget AndAlso hasActiveQuery) Then
                _Target = requestedTarget
                ApplyFilter()
            End If
        Else
            ' Keep current filtered list while switching body modules (Visits/Accounts/Images...).
            ' If user starts a new search after this, switch scope to ALL on first keystroke.
            _promoteSearchScopeToAllOnNextUserType = True
        End If
        ' When an active search is already narrowing the list, preserve the current search context
        ' and avoid re-focusing to passedPatient during module switches.
        Dim preserveSearchContext As Boolean = HasActiveSearchQuery()
        If passedPatient IsNot Nothing AndAlso Not preserveSearchContext Then
            If _bindingPatients.Count = 0 AndAlso _filteredPatients IsNot Nothing Then
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
    End Sub

    Private Sub txtPatientName_TextChanged(sender As Object, e As EventArgs) Handles txtPatientName.TextChanged
        If _suppressSearchText Then Return
        If _promoteSearchScopeToAllOnNextUserType Then
            _promoteSearchScopeToAllOnNextUserType = False
            _Target = PatientFilterAll
            ApplyFilter()
        End If
        If _searchTimer Is Nothing Then
            _searchTimer = New System.Windows.Forms.Timer With {.Interval = SearchDebounceMs}
            AddHandler _searchTimer.Tick, AddressOf OnSearchTimerTick
        End If
        _searchTimer.Stop()
        _searchTimer.Start()
    End Sub

    Private Sub OnSearchTimerTick(sender As Object, e As EventArgs)
        _searchTimer.Stop()
        Dim raw = If(txtPatientName Is Nothing, "", txtPatientName.Text).Trim()
        ' While query text is non-empty, do not broadcast PatientChanged — workspace LoadPatientData steals focus from the search box.
        Dim suppressBroadcast = raw.Length > 0
        RefreshBindingListFromFiltered(If(txtPatientName Is Nothing, "", txtPatientName.Text), suppressPatientBroadcast:=suppressBroadcast)
        ' Google-like incremental popup: show suggestions live as the user types. The suggestion form is
        ' non-activating (ShowWithoutActivation + WS_EX_NOACTIVATE), so focus stays in the search box.
        UpdateAndShowPatientSuggestions()
        UpdateSearchResultsButtonVisibility()
    End Sub

    Private Sub EnsureSearchResultsButton()
        If txtPatientName Is Nothing Then Return
        If _showResultsButton IsNot Nothing Then Return

        _showResultsButton = New SimpleButton With {
            .Text = "▼",
            .TabStop = False,
            .Visible = False
        }

        _showResultsButton.ToolTip = If(Eng, "Show results", "إظهار النتائج")
        _showResultsButton.LookAndFeel.UseDefaultLookAndFeel = True
        AddHandler _showResultsButton.Click, AddressOf ShowResultsButton_Click

        Dim parent = txtPatientName.Parent
        If parent IsNot Nothing Then
            parent.Controls.Add(_showResultsButton)
            _showResultsButton.BringToFront()
        End If

        AddHandler txtPatientName.SizeChanged, AddressOf txtPatientName_RepositionShowResultsButton
        AddHandler txtPatientName.LocationChanged, AddressOf txtPatientName_RepositionShowResultsButton
        PositionSearchResultsButton()
    End Sub

    Private Sub UpdateSearchResultsButtonVisibility()
        If txtPatientName Is Nothing OrElse _showResultsButton Is Nothing Then Return
        Dim hasResults As Boolean = BuildSearchFilteredPatientList(If(txtPatientName Is Nothing, "", txtPatientName.Text)).Count > 0
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
        Dim isRtl As Boolean = (Me.RightToLeft = RightToLeft.Yes) OrElse
                               (txtPatientName.RightToLeft = RightToLeft.Yes) OrElse
                               (Not Eng)
        Dim x As Integer = If(isRtl, txtPatientName.Left, txtPatientName.Left + txtPatientName.Width - btnW)
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
        AddHandler _suggestionList.MouseDown, AddressOf PatientSuggestion_MouseDown
        AddHandler _suggestionList.KeyDown, AddressOf PatientSuggestion_KeyDown
        _suggestionUiReady = True
    End Sub

    Private Shared Function FormatPatientSuggestionLine(p As Patient) As String
        If p Is Nothing Then Return ""
        Dim namePart = If(p.PatientName, "").Trim()
        Dim numPart = If(p.PatientNumber, "").Trim()
        If numPart.Length > 0 Then Return namePart & "    [#" & numPart & "]"
        Return namePart
    End Function

    Private Sub UpdateAndShowPatientSuggestions(Optional forceShow As Boolean = False)
        If txtPatientName Is Nothing Then Return
        Dim q = If(txtPatientName.Text, "").Trim()
        Dim displayList = BuildSearchFilteredPatientList(If(txtPatientName Is Nothing, "", txtPatientName.Text))
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
        _suggestionList.BringToFront()
        If Not _suggestionList.Visible Then
            _suggestionList.Visible = True
        End If
    End Sub

    Private Sub HidePatientSuggestions()
        _lastSuggestionPatients = Nothing
        If _suggestionList IsNot Nothing AndAlso _suggestionList.Visible Then
            _suggestionList.Visible = False
        End If
    End Sub

    Private Sub ShowResultsButton_Click(sender As Object, e As EventArgs)
        If _showResultsButton Is Nothing Then Return
        UpdateAndShowPatientSuggestions(forceShow:=True)
        txtPatientName.Focus()
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
        Dim p = ResolveSuggestionPatientAt(index)
        If p Is Nothing Then Return
        RefreshBindingListFromFiltered(If(txtPatientName Is Nothing, "", txtPatientName.Text), suppressPatientBroadcast:=False, parkNoCurrentPatient:=False, selectPatientIdAfterRefresh:=p.PatientID)
        HidePatientSuggestions()
        _suppressSearchText = True
        Try
            txtPatientName.Text = If(p.PatientName, "")
        Finally
            _suppressSearchText = False
        End Try
        _ignoreSearchSelectAllOnce = True
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
            If PopupPatient IsNot Nothing Then PopupPatient.Text = ""
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
        If _bindingPatients.Count = 0 AndAlso _filteredPatients IsNot Nothing Then
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
        If _bindingPatients.Count = 0 AndAlso _filteredPatients IsNot Nothing Then
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
    Public _Target As String = "Treat"
    Friend _allPatients As List(Of Patient)
    Friend _bindingPatients As BindingList(Of Patient)
    Private _Navigator3PatientsLoadStarted As Boolean

    Dim Kid As Boolean = False
    Dim Adult As Boolean = True
    Dim Grid As Boolean = False


    'Private _btnBodyExpand As SimpleButton

    Public Sub New()
        InitializeComponent()
        SetImgs()
        StoreOriginalBounds(Me)
        _allPatients = New List(Of Patient)()
        _Target = PatientFilterAll
        _bindingPatients = New BindingList(Of Patient)()
        PatientBS.DataSource = _bindingPatients
        BindPatientHeaderLabels()
        ApplyFilter()
    End Sub

    Public Sub New(ByVal filterTarget As String)
        InitializeComponent()
        SetImgs()
        StoreOriginalBounds(Me)
        _allPatients = New List(Of Patient)()
        _Target = If(String.IsNullOrEmpty(filterTarget), PatientFilterAll, filterTarget)
        _bindingPatients = New BindingList(Of Patient)()
        PatientBS.DataSource = _bindingPatients
        BindPatientHeaderLabels()
        ApplyFilter()
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
                                If t.IsFaulted OrElse t.Result Is Nothing Then Return
                                _allPatients.Clear()
                                _allPatients.AddRange(t.Result)
                                ApplyFilter(parkNoCurrentPatient:=ShouldParkNoCurrentPatientAfterBackgroundLoad())
                            End Sub)
            End Sub, TaskScheduler.Default)
    End Sub
    Private Sub Navigator3_Load(sender As Object, e As EventArgs) Handles Me.Load
        btnResetKid.Visible = False
        EnsurePatientSuggestionUi()
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
        txtPatientName.Select()
        UpdateSearchResultsButtonVisibility()
        sw.Stop()
        LogToFile("Time spent: " & sw.Elapsed.TotalSeconds.ToString("F2") & " seconds", Me)
        StartNavigator3PatientsBackgroundLoad()

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
            Kid = If(patient.Age() < 11 AndAlso patient.Age() > 2, True, False)
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
            Kid = If(newPatient.Age() < 11 AndAlso newPatient.Age() > 2, True, False)
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
        Select Case _Target
            Case "Treat"
                ResetControlBackground(Side1)
                Side1.BackColor = Color.Transparent
                ResetControlBackground(txtPatientName)
                txtPatientName.BackColor = Color.Transparent
                If _currentPatient.Diag.GetValueOrDefault(False) Then
                    ApplyCtlGradientBackground(txtPatientName, Color.LightCyan, Color.Cyan,
                                           gradientMode:=Drawing2D.LinearGradientMode.ForwardDiagonal, 200)

                    'Else
                    '    ResetControlBackground(txtPatientName)
                    '    txtPatientName.BackColor = Color.Transparent
                End If
            Case "Ortho"
                ResetControlBackground(Side1)
                Side1.BackColor = Color.Transparent
                ResetControlBackground(txtPatientName)
                txtPatientName.BackColor = Color.Transparent
            Case "Mobile"

                ApplyCtlGradientBackground(Side1, Color.LightPink, Color.Pink,
                                           gradientMode:=Drawing2D.LinearGradientMode.ForwardDiagonal, 200)
                ResetControlBackground(txtPatientName)
                txtPatientName.BackColor = Color.Transparent
            Case "Diag"

                ApplyCtlGradientBackground(Side1,
                                             Color.AliceBlue,
                                              Color.Blue,
                                              Drawing2D.LinearGradientMode.ForwardDiagonal, 128)
                ApplyCtlGradientBackground(txtPatientName, Color.LightCyan, Color.Cyan,
                                           gradientMode:=Drawing2D.LinearGradientMode.ForwardDiagonal, 200)
        End Select

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
        ElseIf _filteredPatients IsNot Nothing Then
            displayCount = _filteredPatients.Count
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
                Debug.WriteLine($"Warning: Patient {patient.PatientID} not found in PatientBS")
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
    Public Sub LoadBal(ByVal PatientId As Integer) Implements IPatientHeaderControl.LoadBal

        Try
            If _currentPatient Is Nothing Then Return
            Dim d As Double
            If _currentPatient.PatientID = PatientId Then
                _currentPatient.RefreshComputedProperties()
                d = _currentPatient.Balance
            Else
                d = _currentPatient.GetBalance(PatientId)
            End If
            Me.LabelBal.Text = d.ToString("###0.0")
            If d < 0 Then
                Me.LabelBal.ForeColor = Color.Red
            ElseIf d > 0 Then
                Me.LabelBal.ForeColor = Color.Blue
            Else
                Me.LabelBal.ForeColor = Color.Black
            End If
        Catch ex As System.Data.SqlClient.SqlException
            MsgBox(ex.Message)
        End Try
    End Sub


#Region "PatientHdrControls"

    Private Sub ShowFly()
        FlyoutPatientInfo.OwnerControl = Me
        FlyoutPatientInfo.Options.AnchorType = DevExpress.Utils.Win.PopupToolWindowAnchor.Manual
        FlyoutPatientInfo.Options.Location = New System.Drawing.Point(299, 74)
        FlyoutPatientInfo.ShowPopup()
    End Sub

    Private Sub btnAddPatient_Click(sender As Object, e As EventArgs) Handles btNewPatient.Click
        _popupOpeningForAdd = True
        ClearFormForNewPatient()
        ' Prefill name from search box only for explicit Add — do not sync on every keystroke (avoids ghost name when opening edit with 0 matches).
        If txtCount.Text = "0" AndAlso txtPatientName.Text.Trim().Length > 0 Then
            PatientNameTextEdit.Text = If(txtPatientName Is Nothing, "", txtPatientName.Text).Trim()
        Else
            PatientNameTextEdit.Text = ""
        End If

        'PopupPatient.ShowPopup()
        ShowFly()
    End Sub
    ''' <summary>Clear flyout patient fields (name, demographics, filters) without changing header title or Add/Update mode.</summary>
    Private Sub ClearPopupPatientDetailFields()
        PatientIDEdit.EditValue = 0
        PatientNameTextEdit.Text = ""
        SexTextBox.Text = ""
        AgeSpinEdit.EditValue = 25
        PhoneTextEdit.Text = ""
        AddressTextEdit.Text = ""
        HealthTextBox.Text = ""
        NotesTextEdit.Text = ""
        BirthYSpinEdit.EditValue = Now.Year - 25
        CboCity.CboCity.SelectedIndex = -1
        CboHealth.CboHealth.SelectedIndex = -1
        If lblPNum IsNot Nothing Then lblPNum.Text = ""
        TreatCheckBox.Checked = (_Target = PatientFilterTreat)
        ImplantCheckBox.Checked = (_Target = "Implant")
        MobileCheckBox.Checked = (_Target = PatientFilterMobile)
        OrthoCheckBox.Checked = (_Target = PatientFilterOrtho)
        DiagCheck.Checked = (_Target = PatientFilterDiag)
        StrucCheckBox.Checked = False
        txtWhats.Text = ""
        cboPrefix.SelectedIndex = -1
    End Sub
    ''' <summary>Clear popup form so Save/Update button will perform Insert (new patient).</summary>
    Private Sub ClearFormForNewPatient()
        grpPatientInfo.Text = If(Eng, "Add New Patient", "إضافة مريض جديد")
        btnAdd.Visible = True
        btnUpdatePatient.Enabled = False
        btnDeletePatient.Enabled = False
        ClearPopupPatientDetailFields()
    End Sub

    Private Sub RefreshBoundControls()
        For Each ctrl As Control In Me.Controls
            If ctrl.DataBindings.Count > 0 Then
                ctrl.DataBindings(0).ReadValue()
            End If
        Next
    End Sub
    Private Sub PopupPatient_Click(sender As Object, e As EventArgs) Handles PopupPatient.Click
        FlyoutPatientInfo.OwnerControl = PopupPatient
        If _popupOpeningForAdd Then
            ClearFormForNewPatient()
        Else
            Dim cur As Patient = TryCast(PatientBS.Current, Patient)
            If cur Is Nothing OrElse PatientBS.Position < 0 Then
                ' No row in the filtered list (e.g. search matched nothing): do not open the flyout — avoids empty Save/Delete/Update.
                MessageBox.Show(
                    If(Eng,
                       "No patient is selected. Choose a patient from the list or adjust your search.",
                       "لا يوجد مريض محدد. اختر مريضاً من القائمة أو عدّل البحث."),
                    If(Eng, "Patient", "المريض"),
                    MessageBoxButtons.OK, MessageBoxIcon.Information)
                _popupOpeningForAdd = False
                Return
            End If
            grpPatientInfo.Text = If(Eng, "Patient Info", "بيانات المريض")
            If btnAdd IsNot Nothing Then btnAdd.Visible = False
            btnUpdatePatient.Enabled = True
            btnDeletePatient.Enabled = True
            LoadCurrentPatientIntoPopup()
        End If
        _popupOpeningForAdd = False
        ShowFly()
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
            If _allPatients IsNot Nothing Then
                Dim idxAll As Integer = _allPatients.FindIndex(Function(p) p.PatientID = fresh.PatientID)
                If idxAll >= 0 Then _allPatients(idxAll) = fresh
            End If
            If _filteredPatients IsNot Nothing Then
                Dim idxF As Integer = _filteredPatients.FindIndex(Function(p) p.PatientID = fresh.PatientID)
                If idxF >= 0 Then _filteredPatients(idxF) = fresh
            End If
            If _bindingPatients IsNot Nothing Then
                For bi As Integer = 0 To _bindingPatients.Count - 1
                    If _bindingPatients(bi).PatientID = fresh.PatientID Then
                        _bindingPatients(bi) = fresh
                        Exit For
                    End If
                Next
            End If
        Catch
        End Try
    End Sub

    ''' <summary>Fill popup form fields from _currentPatient (when user clicks PopupPatient to view/edit).</summary>
    Private Sub LoadCurrentPatientIntoPopup()
        If _currentPatient Is Nothing Then Return
        RebindCurrentPatientFromDatabaseBeforePopup()
        PatientIDEdit.EditValue = _currentPatient.PatientID
        PatientNameTextEdit.Text = _currentPatient.PatientName ' If(_currentPatient.PatientName, "")
        SexTextBox.Text = If(_currentPatient.Sex, "")
        AgeSpinEdit.EditValue = If(_currentPatient.Age.HasValue, CType(_currentPatient.Age.Value, Object), 0)
        PhoneTextEdit.Text = If(_currentPatient.Phone, "")
        ' Saved WhatsApp prefix + local / phone fallback (same as appointment editor)
        Try
            WhatsHelper.BindPatientWhatsPrefixAndLocal(cboPrefix, txtWhats, _currentPatient)
            RefreshLblWhats()
        Catch
        End Try

        AddressTextEdit.Text = If(_currentPatient.Address, "")
        HealthTextBox.Text = If(_currentPatient.Health, "")
        NotesTextEdit.Text = If(_currentPatient.Notes, "")
        BirthYSpinEdit.EditValue = If(_currentPatient.BirthY.HasValue, CType(_currentPatient.BirthY.Value, Object), 0)
        If lblPNum IsNot Nothing Then lblPNum.Text = If(_currentPatient.PatientNumber, "")
        TreatCheckBox.Checked = _currentPatient.Treat.GetValueOrDefault(False)
        ImplantCheckBox.Checked = _currentPatient.Implant.GetValueOrDefault(False)
        MobileCheckBox.Checked = _currentPatient.Mobile.GetValueOrDefault(False)
        OrthoCheckBox.Checked = _currentPatient.Ortho.GetValueOrDefault(False)
        DiagCheck.Checked = _currentPatient.Diag.GetValueOrDefault(False)
        StrucCheckBox.Checked = _currentPatient.Struc.GetValueOrDefault(False)
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
        If suppressToggleEvent OrElse PatientBS.Count = 0 Then Return
        Dim currentPatient = CType(PatientBS.Current, Patient)
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
            Kid = If(patient.Age() <= 11, True, False)
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
            Case "Treat"
                TreatCheckBox.Checked = True
            Case "Implant"
                ImplantCheckBox.Checked = True
            Case "Mobile"
                MobileCheckBox.Checked = True
            Case "Ortho"
                OrthoCheckBox.Checked = True
            Case "Diag"
                DiagCheck.Checked = True
        End Select
        ' If WhatsApp is empty and phone starts with 05, default WhatsApp to phone
        If txtWhats IsNot Nothing AndAlso String.IsNullOrWhiteSpace(txtWhats.Text) AndAlso
           PhoneTextEdit IsNot Nothing AndAlso Not String.IsNullOrWhiteSpace(PhoneTextEdit.Text) AndAlso
           PhoneTextEdit.Text.Trim().StartsWith("05") Then
            txtWhats.Text = PhoneTextEdit.Text.Trim()
        End If

        Dim prefixStored As String = WhatsHelper.GetPrefixTextForStorage(cboPrefix)
        Dim localW As String = WhatsHelper.NormalizeLocalWhatsTenDigitsForStorage(If(txtWhats?.Text, "").ToString())

        Dim clsPatient As New Patient With {
            .PatientName = PatientNameTextEdit.Text,
            .Sex = SexTextBox.Text,
            .Age = If(IsNumeric(AgeSpinEdit.Value), CInt(AgeSpinEdit.Value), CType(Nothing, Integer?)),
            .IsKid = If(IsNumeric(AgeSpinEdit.Value), CInt(AgeSpinEdit.Value) < 10, False),
            .Phone = PhoneTextEdit.Text,
            .WhatsAppPrefix = prefixStored,
            .WhatsApp = localW,
            .Address = AddressTextEdit.Text,
            .Health = HealthTextBox.Text,
            .Treat = TreatCheckBox.Checked,
            .Implant = ImplantCheckBox.Checked,
            .Mobile = MobileCheckBox.Checked,
            .Ortho = OrthoCheckBox.Checked,
            .Diag = DiagCheck.Checked,
            .Struc = StrucCheckBox.Checked,
            .Notes = NotesTextEdit.Text,
            .BirthY = BirthYSpinEdit.Value,
            .CreatedBy = If(usr, CurrentUser.UsID, 1),
            .CreateDate = Now
        }
        If Not String.IsNullOrWhiteSpace(lblPNum.Text) Then clsPatient.PatientNumber = lblPNum.Text.Trim()
        Dim sw As New Stopwatch()
        sw.Start()
        Dim committed As Integer = _patientData.InsertPatient(clsPatient)
        sw.Stop()
        LogToFile("📌 InsertPatient took: " & sw.Elapsed.TotalMilliseconds & " ms", Me)
        If committed <= 0 Then
            Select Case committed
                Case -2
                    MsgBox(If(Eng, "Patient with this name already exists.", "مريض بهذا الاسم موجود بالفعل."), MsgBoxStyle.Exclamation)
                Case -3
                    MsgBox(If(Eng, "Failed to generate patient number. Please try again.", "فشل في إنشاء رقم المريض. يرجى المحاولة مرة أخرى."), MsgBoxStyle.Exclamation)
                Case Else
                    MsgBox(If(Eng, "Failed to add patient.", "فشل في إضافة المريض."), MsgBoxStyle.Exclamation)
            End Select
            Return
        End If
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
        _allPatients.Add(newPatient)
        Dim matchesFilter As Boolean = True
        Select Case _Target
            Case PatientFilterTreat : matchesFilter = newPatient.Treat.GetValueOrDefault(False)
            Case PatientFilterOrtho : matchesFilter = newPatient.Ortho.GetValueOrDefault(False)
            Case PatientFilterDiag : matchesFilter = newPatient.Diag.GetValueOrDefault(False)
            Case PatientFilterMobile : matchesFilter = newPatient.Mobile.GetValueOrDefault(False)
        End Select
        If matchesFilter Then
            _bindingPatients.Add(newPatient)
            Dim newIndex As Integer = _bindingPatients.Count - 1
            PatientBS.Position = newIndex
        End If
        FlyoutPatientInfo.HidePopup()
        UpdateNavigationControls()
        FirePatientChangedForCurrent()
    End Sub

    Private Sub HealthTextBox_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HealthTextBox.TextChanged
        If Eng Then
            If Me.HealthTextBox.Text = "Healthy" Or Me.HealthTextBox.Text = "سليم" Then
                Me.HealthTextBox.ForeColor = Color.Blue
            Else
                Me.HealthTextBox.ForeColor = Color.Red
            End If
        Else
            If Me.HealthTextBox.Text = "Healthy" Or Me.HealthTextBox.Text = "سليم" Then
                Me.HealthTextBox.ForeColor = Color.Blue
            Else
                Me.HealthTextBox.ForeColor = Color.Red
            End If
        End If
    End Sub
    Private Sub SexTextBox_TextChanged(sender As Object, e As EventArgs) Handles SexTextBox.TextChanged
        Try
            If Me.SexTextBox.Text = "ذكر" Or Me.SexTextBox.Text = "Male" Then
                Me.PicBox.Image = My.Resources.Male
            ElseIf Me.SexTextBox.Text = "أنثى" Or Me.SexTextBox.Text = "Female" Or Me.SexTextBox.Text = "انثى" Then
                Me.PicBox.Image = My.Resources.Female
            Else
                Me.PicBox.Image = PicBox.InitialImage
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub AgeSpinEdit_EditValueChanged(sender As Object, e As EventArgs) Handles AgeSpinEdit.EditValueChanged
        If AgeSpinEdit.EditValue = 0 Then Exit Sub
        Dim x As Integer = CInt(AgeSpinEdit.EditValue)
        If x >= 3 And x <= 10 Then
            Kid = True
        ElseIf x > 10 Then
            Kid = False
        End If
        BirthYSpinEdit.EditValue = Now.Year - x
        If _currentPatient IsNot Nothing Then _currentPatient.Age = x
        If _currentPatient IsNot Nothing AndAlso _currentPatient.Age < 150 Then
            Me.LabelAge.Text = If(Eng, _currentPatient.Age.ToString & " Yrs", _currentPatient.Age.ToString & "  سنة ")
        Else
            Me.LabelAge.Text = If(Eng, "Age Not Set", "العمر غير محدد")
        End If
    End Sub
    Private Sub BirthYSpinEdit_EditValueChanged(sender As Object, e As EventArgs) Handles BirthYSpinEdit.EditValueChanged
        If BirthYSpinEdit.EditValue = 0 Then Exit Sub
        ' Temporarily disable the AgeSpinEdit event handler to avoid recursion
        RemoveHandler AgeSpinEdit.EditValueChanged, AddressOf AgeSpinEdit_EditValueChanged
        Dim x As Integer = CInt(BirthYSpinEdit.EditValue)
        Dim y As Integer = Now.Year
        x = y - x
        If y - x >= 3 And y - x <= 10 Then
            Kid = True
        ElseIf y - x > 10 Then
            Kid = False
        End If
        AgeSpinEdit.EditValue = x
        If _currentPatient IsNot Nothing Then _currentPatient.Age = x
        If _currentPatient IsNot Nothing AndAlso _currentPatient.Age < 150 Then
            Me.LabelAge.Text = If(Eng, _currentPatient.Age.ToString & " Yrs", _currentPatient.Age.ToString & "  سنة ")
        Else
            Me.LabelAge.Text = If(Eng, "Age Not Set", "العمر غير محدد")
        End If
        ' Re-enable the AgeSpinEdit event handler
        AddHandler AgeSpinEdit.EditValueChanged, AddressOf AgeSpinEdit_EditValueChanged
    End Sub
    Private Sub PatientIDEdit_EditValueChanged(sender As Object, e As EventArgs) Handles PatientIDEdit.TextChanged
        PatientID = CInt(PatientIDEdit.EditValue)
    End Sub
    Private Function CheckTxt() As Boolean
        Dim errorMessages As New List(Of String)()
        ' Check Patient Name
        If String.IsNullOrWhiteSpace(PatientNameTextEdit.Text) Then
            errorMessages.Add(If(Eng, "Patient name is required", "اسم المريض مطلوب"))
            PatientNameTextEdit.Focus()
            ' Check Age
        ElseIf AgeSpinEdit.Value <= 0 Then
            Dim message As String = If(AgeSpinEdit.Value = 0,
                                If(Eng, "Age cannot be zero", "العمر لا يمكن أن يكون صفر"),
                                If(Eng, "Age cannot be negative", "العمر لا يمكن أن يكون سالب"))
            errorMessages.Add(message)
            AgeSpinEdit.Focus()
            ' Check Health
            'ElseIf String.IsNullOrWhiteSpace(HealthTextBox.Text) Then
            '    errorMessages.Add(If(Eng, "Health information is required", "المعلومات الصحية مطلوبة"))
            '    HealthTextBox.Focus()
            '    ' Check Address
            'ElseIf String.IsNullOrWhiteSpace(AddressTextEdit.Text) Then
            '    errorMessages.Add(If(Eng, "Address is required", "العنوان مطلوب"))
            '    AddressTextEdit.Focus()
        End If
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
        ' Show error message if any validation failed
        If errorMessages.Count > 0 Then
            Dim messageTitle As String = If(Eng, "Validation Error", "خطأ في التحقق")
            Dim fullMessage As String = String.Join(vbCrLf, errorMessages)
            MessageBox.Show(fullMessage, messageTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return False
        End If
        Return True
    End Function
    Private Sub CboCity_CityValueChanged(sender As Object, e As CityCombo.CityIndexChangedEvent) Handles CboCity.CityValueChanged
        If CboCity.CityID = -1 Then
            AddressTextEdit.Text = ""
        Else
            AddressTextEdit.Text = CboCity.CityName
        End If
    End Sub
    Private Sub CboHealth_SelectedIndexChanged(sender As Object, e As HlthCombo.HealthIndexChangedEvent) Handles CboHealth.HealthValueChanged
        If CboHealth.HID = -1 Then
            HealthTextBox.Text = ""
        Else
            HealthTextBox.Text = CboHealth.HealthStat
        End If
    End Sub
    Property PatientUpdated As Boolean = False
    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        If Not CheckTxt() Then Exit Sub
        ' Add new patient when no current patient or PatientID is empty/zero
        If _currentPatient Is Nothing OrElse String.IsNullOrWhiteSpace(PatientIDEdit.Text) OrElse CInt(Val(PatientIDEdit.Text)) <= 0 Then
            InsertPatient()
            FlyoutPatientInfo.HidePopup()
            Return
        End If
    End Sub

    Private Sub btnUpdatePatient_Click(sender As Object, e As EventArgs) Handles btnUpdatePatient.Click
        Try
            If Not CheckTxt() Then Exit Sub
            '' Add new patient when no current patient or PatientID is empty/zero
            'If _currentPatient Is Nothing OrElse String.IsNullOrWhiteSpace(PatientIDEdit.Text) OrElse CInt(Val(PatientIDEdit.Text)) <= 0 Then
            '    InsertPatient()
            '    Return
            'End If

            Dim prefixStored As String = WhatsHelper.GetPrefixTextForStorage(cboPrefix)
            Dim localW As String = WhatsHelper.NormalizeLocalWhatsTenDigitsForStorage(If(txtWhats?.Text, "").ToString())

            Dim oldPatient As Patient = _currentPatient
            Dim updated As New Patient With {
            .PatientID = oldPatient.PatientID,
            .Address = AddressTextEdit.Text,
            .PatientNumber = lblPNum.Text,
            .Age = AgeSpinEdit.Value,
            .BirthY = BirthYSpinEdit.Value,
            .Health = HealthTextBox.Text,
            .Implant = ImplantCheckBox.Checked,
            .Notes = NotesTextEdit.Text,
            .Mobile = MobileCheckBox.Checked,
            .Ortho = OrthoCheckBox.Checked,
            .Sex = SexTextBox.Text,
            .Diag = DiagCheck.Checked,
            .Struc = StrucCheckBox.Checked,
            .Treat = TreatCheckBox.Checked,
            .PatientName = PatientNameTextEdit.Text,
            .Phone = PhoneTextEdit.Text,
            .WhatsAppPrefix = prefixStored,
            .WhatsApp = localW,
            .CreatedBy = oldPatient.CreatedBy,
            .CreateDate = oldPatient.CreateDate
        }

            Me.Cursor = Cursors.WaitCursor
            btnUpdatePatient.Enabled = False
            Try
                If _patientData.Update(oldPatient, updated) Then
                    Dim idxAll As Integer = _allPatients.FindIndex(Function(p) p.PatientID = oldPatient.PatientID)
                    If idxAll >= 0 Then _allPatients(idxAll) = updated
                    Dim idxBind As Integer = -1
                    For i As Integer = 0 To _bindingPatients.Count - 1
                        If CType(_bindingPatients(i), Patient).PatientID = oldPatient.PatientID Then
                            idxBind = i
                            Exit For
                        End If
                    Next
                    If idxBind >= 0 Then
                        _bindingPatients(idxBind) = updated
                        ' Force BindingSource to use the new reference so reopening the same patient shows updated values
                        Dim savePos As Integer = PatientBS.Position
                        If savePos = idxBind Then
                            PatientBS.Position = -1
                            PatientBS.Position = idxBind
                        End If
                    End If
                    _currentPatient = updated
                    FlyoutPatientInfo.HidePopup()
                    UpdateNavigationControls()
                    ' Refresh Kid/Adult and chart without writing patient name into txtPatientName (no list filter)
                    UpdateBoundControlsExceptName(updated)
                    UpdateKidStateOnly(updated)
                    PatientEventManager.RaisePatientChanged(updated, Kid, True)
                Else
                    MsgBox(If(Eng, "Update operation failed. Please try again.", "فشل التحديث. يرجى المحاولة مرة أخرى."), MsgBoxStyle.Exclamation)
                End If
            Finally
                Me.Cursor = Cursors.Default
                btnUpdatePatient.Enabled = True
                FlyoutPatientInfo.HidePopup()
            End Try
        Catch ex As Exception
            MsgBox($"Error updating patient: {ex.Message}", MsgBoxStyle.Critical, "Error")
        End Try
    End Sub
    Property PatientDeleted As Boolean = False
    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDeletePatient.Click
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
            Dim currentPos As Integer = PatientBS.Position
            Dim idxInBinding As Integer = -1
            For i As Integer = 0 To _bindingPatients.Count - 1
                If CType(_bindingPatients(i), Patient).PatientID = toDelete.PatientID Then
                    idxInBinding = i
                    Exit For
                End If
            Next
            If idxInBinding >= 0 Then
                _bindingPatients.RemoveAt(idxInBinding)
                Dim idxInAll As Integer = _allPatients.FindIndex(Function(p) p.PatientID = toDelete.PatientID)
                If idxInAll >= 0 Then _allPatients.RemoveAt(idxInAll)
                ' Focus on the one before: move to previous index (or 0 if we removed first)
                If _bindingPatients.Count > 0 Then
                    Dim newPos As Integer = Math.Max(0, idxInBinding - 1)
                    PatientBS.Position = newPos
                End If
                _currentPatient = If(PatientBS.Count > 0, TryCast(PatientBS.Current, Patient), Nothing)
            End If
            FlyoutPatientInfo.HidePopup()
            UpdateNavigationControls()
            FirePatientChangedForCurrent()
            MsgBox(If(Eng, $"Patient {toDelete.PatientName} has been deleted.", $"تم حذف المريض {toDelete.PatientName}."), MsgBoxStyle.Information)
        Catch ex As Exception
            MsgBox(If(Eng, $"Error deleting patient: {ex.Message}", $"خطأ في الحذف: {ex.Message}"))
        End Try
    End Sub
    Private Sub SexTextBox_EditValueChanged(sender As Object, e As EventArgs) Handles SexTextBox.EditValueChanged
        If String.IsNullOrEmpty(SexTextBox.Text) Then
            RadioMale.Checked = True
        ElseIf SexTextBox.Text = "Male" OrElse SexTextBox.Text = "ذكر" Then
            RadioMale.Checked = True
        ElseIf SexTextBox.Text = "Female" OrElse SexTextBox.Text = "أنثى" OrElse SexTextBox.Text = "انثى" Then
            RadioFemale.Checked = True
        End If
    End Sub
    Private Sub RadioMale_CheckedChanged(sender As Object, e As EventArgs) Handles RadioMale.CheckedChanged
        Dim maleEng, maleAr As String
        maleAr = "ذكر"
        maleEng = "Male"
        If RadioMale.Checked = True Then
            If Eng Then
                SexTextBox.Text = maleEng
            Else
                SexTextBox.Text = maleAr
            End If
        End If
    End Sub
    Private Sub RadioFemale_CheckedChanged(sender As Object, e As EventArgs) Handles RadioFemale.CheckedChanged
        Dim femEng, femAr As String
        femAr = "أنثى"
        femEng = "Female"
        If RadioFemale.Checked = True Then
            If Eng Then
                SexTextBox.Text = femEng
            Else
                SexTextBox.Text = femAr
            End If
        End If
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
        If txtWhats IsNot Nothing AndAlso txtWhats.Text IsNot Nothing Then
            number = txtWhats.Text.ToString().Trim()
        End If
        Dim localDigits As String = New String(number.Where(Function(ch) Char.IsDigit(ch)).ToArray())

        ' Remove any leading zeros (we expect 0 + 9 digits in txtWhats; we only want the 9 digits part).
        While localDigits.StartsWith("0"c) AndAlso localDigits.Length > 0
            localDigits = localDigits.Substring(1)
        End While

        ' Keep at most the last 9 digits as the local number (safety for overlong input).
        If localDigits.Length > 9 Then
            localDigits = localDigits.Substring(localDigits.Length - 9, 9)
        End If

        If cboPrefix Is Nothing OrElse cboPrefix.EditValue Is Nothing Then
            Return localDigits
        End If
        Dim rawPrefix As String = cboPrefix.EditValue.ToString()
        Dim prefixDigits As String = New String(rawPrefix.Where(Function(ch) Char.IsDigit(ch)).ToArray())
        If String.IsNullOrWhiteSpace(prefixDigits) Then Return localDigits
        If localDigits.Length = 0 Then Return ""
        Return prefixDigits & localDigits
    End Function
    ''' <summary>Updates lblWhats with the full WhatsApp number (prefix + txtWhats). Call after load or when prefix/number changes.</summary>
    Private Sub RefreshLblWhats()
        If lblWhats IsNot Nothing Then
            lblWhats.Text = GetFullWhatsNumber()
        End If
    End Sub

    ''' <summary>Fills cboPrefix with country name and calling code. Palestine (970) and Israel (972) are first.</summary>
    Private Sub FillCboPrefixOnce()
        WhatsHelper.FillCboPrefixOnce(cboPrefix)
    End Sub

    Private Sub cboPrefix_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboPrefix.SelectedIndexChanged
        RefreshLblWhats()
    End Sub

    Private Sub txtWhats_ValueChanged(sender As Object, e As EventArgs) Handles txtWhats.EditValueChanged
        RefreshLblWhats()
    End Sub

    Private Sub txtWhats_KeyDown(sender As Object, e As KeyEventArgs) Handles txtWhats.KeyDown
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
        ResizeControlsProportionally()
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



    ''' <summary>User came back to the search box (Tab, click, or programmatic) — select all so they can start fresh. Single pass, no timer.</summary>
    Private Sub txtPatientName_FocusSelectAll(sender As Object, e As EventArgs) Handles txtPatientName.Enter
        If _ignoreSearchSelectAllOnce Then Return
        ScheduleSelectAllPatientSearch()
    End Sub

    ''' <summary>DevExpress TextEdit often clears selection right after focus; do a single deferred SelectAll via BeginInvoke. Avoid a repeating timer — it fights keystrokes (each tick re-selects the text, so next keypress replaces the just-typed character).</summary>
    Private Sub ScheduleSelectAllPatientSearch()
        If Me.IsDisposed OrElse Not Me.IsHandleCreated Then Return
        If txtPatientName Is Nothing OrElse txtPatientName.IsDisposed Then Return
        If Not txtPatientName.ContainsFocus Then Return
        If _ignoreSearchSelectAllOnce Then Return
        BeginInvoke(New Action(
            Sub()
                Try
                    If Me.IsDisposed OrElse Not Me.IsHandleCreated Then Return
                    If txtPatientName Is Nothing OrElse txtPatientName.IsDisposed Then Return
                    If Not txtPatientName.ContainsFocus Then Return
                    If _ignoreSearchSelectAllOnce Then Return
                    Dim t = If(txtPatientName.Text, "")
                    txtPatientName.SelectionStart = 0
                    txtPatientName.SelectionLength = t.Length
                Catch
                    Try
                        txtPatientName.SelectAll()
                    Catch
                    End Try
                End Try
            End Sub))
    End Sub

    ' OnSearchSelectAllTimerTick removed: a repeating SelectAll timer fights live typing (each tick re-selects text,
    ' so the next keypress replaces the just-typed character -> "one letter then no more"). SelectAll is now a
    ' single BeginInvoke pass on Enter, gated by _ignoreSearchSelectAllOnce for suggestion commit/refocus paths.

    Private Async Sub btnWhatsSend_Click(sender As Object, e As EventArgs) Handles btnWhatsSend.Click
        Try
            If Not Await WhatsAppService.EnsureWhatsAppConnectedOrNotifyAsync(Me) Then Return
            Using frm As New WhatsAppSender(_currentPatient)
                frm.Icon = GetIcon()
                frm.ShowDialog(Me)
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message, If(Eng, "Error", "خطأ"), MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub FlyoutPatientInfo_ButtonClick(sender As Object, e As DevExpress.Utils.FlyoutPanelButtonClickEventArgs) Handles FlyoutPatientInfo.ButtonClick
        If e.Button.Caption = "Close" OrElse e.Button.Caption = "إغلاق" Then
            FlyoutPatientInfo.HideBeakForm()
        End If
    End Sub










#End Region

End Class

