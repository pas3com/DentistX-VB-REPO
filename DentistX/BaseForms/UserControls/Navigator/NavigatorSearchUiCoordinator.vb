Imports System.Windows.Forms

''' <summary>
''' Debounced search tick and “committed suggestion” snapshot state for <see cref="Navigator3"/>.
''' Keeps timers and search-session fields out of the user control.
''' </summary>
Friend Class NavigatorSearchUiCoordinator
    Private Const IntervalMs As Integer = 120
    Private _timer As Timer

    Public Event DebouncedTick As EventHandler

    Friend LastCommittedSearchText As String
    Friend LastCommittedSearchPatients As List(Of Patient)
    Friend LastCommittedSuggestionPatientName As String

    Friend Sub ScheduleDebouncedSearch()
        EnsureTimer()
        _timer.Stop()
        _timer.Start()
    End Sub

    Friend Sub StopDebouncedSearch()
        If _timer IsNot Nothing Then _timer.Stop()
    End Sub

    Private Sub EnsureTimer()
        If _timer Is Nothing Then
            _timer = New Timer() With {.Interval = IntervalMs}
            AddHandler _timer.Tick, AddressOf OnTimerTick
        End If
    End Sub

    Private Sub OnTimerTick(sender As Object, e As EventArgs)
        _timer.Stop()
        RaiseEvent DebouncedTick(Me, EventArgs.Empty)
    End Sub

    Friend Sub Dispose()
        If _timer IsNot Nothing Then
            RemoveHandler _timer.Tick, AddressOf OnTimerTick
            _timer.Dispose()
            _timer = Nothing
        End If
    End Sub

    Friend Sub ClearCommittedSuggestionSearchContext()
        LastCommittedSearchText = Nothing
        LastCommittedSearchPatients = Nothing
        LastCommittedSuggestionPatientName = Nothing
    End Sub

    Friend Function ShouldUseCommittedSuggestionSearchContext(forceShow As Boolean, currentTextBoxText As String) As Boolean
        If Not forceShow Then Return False
        If LastCommittedSearchPatients Is Nothing OrElse LastCommittedSearchPatients.Count = 0 Then Return False

        Dim currentText = NavigatorPatientListState.NormalizePatientSearchText(If(currentTextBoxText, "").Trim())
        Dim committedPatientName = NavigatorPatientListState.NormalizePatientSearchText(If(LastCommittedSuggestionPatientName, "").Trim())
        Return currentText.Length > 0 AndAlso
               String.Equals(currentText, committedPatientName, StringComparison.OrdinalIgnoreCase)
    End Function
End Class
