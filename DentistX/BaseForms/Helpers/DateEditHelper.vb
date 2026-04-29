Imports System.Collections.Generic
Imports System.Drawing
Imports System.Globalization
Imports System.Windows.Forms
Imports DevExpress.XtraEditors
Imports DevExpress.XtraEditors.Controls
Imports DevExpress.XtraEditors.Repository

' How to use it
' DateEditHelper.ApplyFullDayNameCalendar(myDateEdit)
' Dim popup = New DateEditFullDayNameBehavior(myDateEdit)
' ' On form/usercontrol dispose: popup.Dispose()

''' <summary>
''' Display-only DateEdit tweak: the dropdown calendar shows full weekday names
''' ("Sunday", "Monday", …) in the header row instead of abbreviations.
''' Does not change EditValue, parsing, or database values.
''' </summary>
Public NotInheritable Class DateEditHelper

    Private Sub New()
    End Sub

    ''' <summary>
    ''' Configure <paramref name="edit"/> to prefer full weekday names.
    ''' The popup behavior still applies the full names again on Popup for skins that cache abbreviations.
    ''' </summary>
    Public Shared Sub ApplyFullDayNameCalendar(edit As DateEdit)
        If edit Is Nothing Then Return
        edit.Properties.CalendarView = CalendarView.Fluent
        Dim calFmt = CType(CultureInfo.CurrentCulture.DateTimeFormat.Clone(), DateTimeFormatInfo)
        calFmt.Calendar = New GregorianCalendar()
        Dim full = calFmt.DayNames
        calFmt.AbbreviatedDayNames = full
        calFmt.ShortestDayNames = full
        edit.Properties.CalendarDateTimeFormatInfo = calFmt
    End Sub

    ''' <summary>
    ''' Measures an adequate popup width so all seven full weekday names are visible.
    ''' </summary>
    Public Shared Function ComputeFullDayNamePopupWidth(font As Font) As Integer
        Dim fmt = CultureInfo.CurrentCulture.DateTimeFormat
        Dim widestText = 0
        For Each dayName In fmt.DayNames
            If Not String.IsNullOrWhiteSpace(dayName) Then
                widestText = Math.Max(widestText, TextRenderer.MeasureText(dayName, font).Width)
            End If
        Next
        If widestText <= 0 Then widestText = TextRenderer.MeasureText("Wednesday", font).Width
        Dim cellWidth = Math.Max(58, widestText + 10)
        ' Keep width reasonable; too-wide popups can create visible empty space in some skins.
        Return Math.Max(440, Math.Min(760, cellWidth * 7 + 18))
    End Function

End Class

''' <summary>
''' Forces full weekday names on popup calendars and sizes popup width to fit them.
''' </summary>
Public Class DateEditFullDayNameBehavior
    Implements IDisposable

    Private ReadOnly _edit As DateEdit
    Private _hookedCalendar As CalendarControlBase
    Private _disposed As Boolean

    Public Sub New(edit As DateEdit)
        If edit Is Nothing Then Throw New ArgumentNullException(NameOf(edit))
        _edit = edit
        Dim popupWidth = DateEditHelper.ComputeFullDayNamePopupWidth(If(_edit.Font, SystemFonts.DefaultFont))
        Try
            _edit.Properties.PopupFormSize = New Size(popupWidth, Math.Max(280, _edit.Properties.PopupFormSize.Height))
        Catch
        End Try
        AddHandler _edit.Popup, AddressOf OnEditPopup
        AddHandler _edit.CloseUp, AddressOf OnEditCloseUp
    End Sub

    Private Sub OnEditPopup(sender As Object, e As EventArgs)
        Dim popup = _edit.GetPopupEditForm()
        If popup Is Nothing Then Return
        Dim popupWidth = DateEditHelper.ComputeFullDayNamePopupWidth(If(_edit.Font, SystemFonts.DefaultFont))
        If popup.Width < popupWidth Then
            popup.Width = popupWidth
        End If
        Dim cal = FindNestedCalendarControl(popup)
        If cal Is Nothing Then Return
        DetachCalendar()
        _hookedCalendar = cal
        cal.WeekDayAbbreviationLength = 99
        AddHandler cal.CustomWeekDayAbbreviation, AddressOf OnCustomWeekDayAbbreviation
        ' Force the inner calendar to consume the popup body; this removes right-side empty gutter.
        cal.Dock = DockStyle.Fill
        Try
            cal.PerformLayout()
            cal.Invalidate(True)
        Catch
        End Try
    End Sub

    Private Sub OnEditCloseUp(sender As Object, e As EventArgs)
        DetachCalendar()
    End Sub

    Private Sub DetachCalendar()
        If _hookedCalendar Is Nothing Then Return
        Try
            RemoveHandler _hookedCalendar.CustomWeekDayAbbreviation, AddressOf OnCustomWeekDayAbbreviation
        Catch
        End Try
        _hookedCalendar = Nothing
    End Sub

    Private Shared Function FindNestedCalendarControl(root As Control) As CalendarControlBase
        If root Is Nothing Then Return Nothing
        Dim q As New Queue(Of Control)
        q.Enqueue(root)
        While q.Count > 0
            Dim c = q.Dequeue()
            Dim cal = TryCast(c, CalendarControlBase)
            If cal IsNot Nothing Then Return cal
            For Each ch As Control In c.Controls
                q.Enqueue(ch)
            Next
        End While
        Return Nothing
    End Function

    Private Shared Sub OnCustomWeekDayAbbreviation(sender As Object, e As CustomWeekDayAbbreviationEventArgs)
        Dim cal = TryCast(sender, CalendarControlBase)
        If cal Is Nothing Then Return
        Dim df = cal.DateFormat
        Dim key = If(e.Day, "")
        For i = 0 To 6
            If String.Equals(key, df.DayNames(i), StringComparison.CurrentCultureIgnoreCase) OrElse
               String.Equals(key, df.AbbreviatedDayNames(i), StringComparison.CurrentCultureIgnoreCase) OrElse
               String.Equals(key, df.ShortestDayNames(i), StringComparison.CurrentCultureIgnoreCase) Then
                e.Value = df.DayNames(i)
                Return
            End If
        Next
    End Sub

    Public Sub Dispose() Implements IDisposable.Dispose
        If _disposed Then Return
        _disposed = True
        RemoveHandler _edit.Popup, AddressOf OnEditPopup
        RemoveHandler _edit.CloseUp, AddressOf OnEditCloseUp
        DetachCalendar()
    End Sub
End Class
