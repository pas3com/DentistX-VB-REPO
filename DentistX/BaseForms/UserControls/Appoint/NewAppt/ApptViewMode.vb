Imports System
Imports System.Collections.Generic
Imports System.Drawing
Imports System.Linq
Imports System.Reflection
Imports System.Windows.Forms
Imports DevExpress.Utils
Imports DevExpress.XtraEditors

Public Enum ApptViewMode
    DayView
    ThisWeek
    ThisWeekFull
    MonthlyWeek
    MonthView
    DaysTimeline
    DoctorsDay
End Enum

Public Enum ApptWeekVariant
    CompactSixDay
    FullSevenDay
End Enum

Public Class ApptDateRange
    Public Property StartDate As DateTime
    Public Property EndDate As DateTime
End Class

Public Class ApptDoctorInfo
    Public Property DrID As Integer
    Public Property DrName As String
    Public Property DrColor As Color = Color.LightSteelBlue
End Class

Public Class ApptTextAppearance
    Public Sub New()
        FontName = "Calibri"
        FontSize = 10.0F
        FontStyle = FontStyle.Regular
        ForeColor = Color.Black
        BackColor = Color.Transparent
        Visible = True
        MaxLines = 1
    End Sub

    Public Property Visible As Boolean
    Public Property FontName As String
    Public Property FontSize As Single
    Public Property FontStyle As FontStyle
    Public Property ForeColor As Color
    Public Property BackColor As Color
    Public Property MaxLines As Integer

    Public Function Clone() As ApptTextAppearance
        Return CType(MemberwiseClone(), ApptTextAppearance)
    End Function

    Public Function CreateFont() As Font
        Return New Font(If(String.IsNullOrWhiteSpace(FontName), "Calibri", FontName), Math.Max(6.0F, FontSize), FontStyle)
    End Function
End Class

Public Class ApptCardAppearance
    Public Sub New()
        CardBackColor = BlendCardBackFromAccent(Color.SteelBlue)
        BorderColor = Color.FromArgb(205, 210, 220)
        AccentColor = Color.SteelBlue
        CardPadding = New Padding(0)
        CardMargin = New Padding(0)
        FieldSpacing = 1
        MaxBodyLines = 4
        ShowBorder = True
        ShowShadowHint = False
        StartTimeStyle = New ApptTextAppearance With {.FontSize = 9.5F, .FontStyle = FontStyle.Bold, .ForeColor = Color.Red, .MaxLines = 1}
        EndTimeStyle = New ApptTextAppearance With {.FontSize = 9.5F, .FontStyle = FontStyle.Bold, .ForeColor = Color.FromArgb(30, 70, 140), .BackColor = Color.FromArgb(28, 236, 244, 255), .MaxLines = 1}
        PatientStyle = New ApptTextAppearance With {.FontSize = 10.5F, .FontStyle = FontStyle.Bold, .ForeColor = Color.FromArgb(33, 37, 41), .MaxLines = 2}
        DoctorStyle = New ApptTextAppearance With {.FontSize = 9.25F, .FontStyle = FontStyle.Bold, .ForeColor = Color.FromArgb(40, 48, 58), .MaxLines = 1}
        ReasonStyle = New ApptTextAppearance With {.FontSize = 9.0F, .FontStyle = FontStyle.Bold, .ForeColor = Color.FromArgb(0, 118, 128), .MaxLines = 2}
        StatusStyle = New ApptTextAppearance With {.FontSize = 8.75F, .FontStyle = FontStyle.Bold, .ForeColor = Color.White, .BackColor = Color.FromArgb(103, 114, 229), .MaxLines = 1}
        NotesStyle = New ApptTextAppearance With {.FontSize = 8.75F, .FontStyle = FontStyle.Bold, .ForeColor = Color.FromArgb(93, 64, 140), .MaxLines = 2}
    End Sub

    Public Property CardBackColor As Color
    Public Property BorderColor As Color
    Public Property AccentColor As Color
    Public Property CardPadding As Padding
    Public Property CardMargin As Padding
    Public Property FieldSpacing As Integer
    Public Property MaxBodyLines As Integer
    Public Property ShowBorder As Boolean
    Public Property ShowShadowHint As Boolean
    Public Property StartTimeStyle As ApptTextAppearance
    Public Property EndTimeStyle As ApptTextAppearance
    Public Property PatientStyle As ApptTextAppearance
    Public Property DoctorStyle As ApptTextAppearance
    Public Property ReasonStyle As ApptTextAppearance
    Public Property StatusStyle As ApptTextAppearance
    Public Property NotesStyle As ApptTextAppearance

    Public Function Clone() As ApptCardAppearance
        Dim clon = CType(MemberwiseClone(), ApptCardAppearance)
        clon.StartTimeStyle = StartTimeStyle.Clone()
        clon.EndTimeStyle = EndTimeStyle.Clone()
        clon.PatientStyle = PatientStyle.Clone()
        clon.DoctorStyle = DoctorStyle.Clone()
        clon.ReasonStyle = ReasonStyle.Clone()
        clon.StatusStyle = StatusStyle.Clone()
        clon.NotesStyle = NotesStyle.Clone()
        Return clon
    End Function

    ''' <summary>Sets the same <see cref="ApptTextAppearance.ForeColor"/> on every text field (times, patient, doctor, reason, notes, status).</summary>
    Public Sub UniformTextForeColor(foreColor As Color)
        StartTimeStyle.ForeColor = foreColor
        EndTimeStyle.ForeColor = foreColor
        PatientStyle.ForeColor = foreColor
        DoctorStyle.ForeColor = foreColor
        ReasonStyle.ForeColor = foreColor
        NotesStyle.ForeColor = foreColor
        StatusStyle.ForeColor = foreColor
    End Sub

    ''' <summary>
    ''' Sets fore (and optionally back) on all text fields. Status chip uses <paramref name="backColor"/> when provided
    ''' (same as other fields). Omit <paramref name="backColor"/> to leave each field’s BackColor unchanged.
    ''' </summary>
    Public Sub UniformTextColors(foreColor As Color, Optional backColor As Color? = Nothing)
        UniformTextForeColor(foreColor)
        If Not backColor.HasValue Then Return
        Dim bg = backColor.Value
        StartTimeStyle.BackColor = bg
        EndTimeStyle.BackColor = bg
        PatientStyle.BackColor = bg
        DoctorStyle.BackColor = bg
        ReasonStyle.BackColor = bg
        NotesStyle.BackColor = bg
        StatusStyle.BackColor = bg
    End Sub
End Class

Public Class ApptCardVm
    Public Property Appointment As AppointmentC
    Public Property PatientName As String
    Public Property DoctorInfo As ApptDoctorInfo
    Public Property Appearance As ApptCardAppearance

    Public ReadOnly Property DoctorName As String
        Get
            Return If(DoctorInfo Is Nothing, "", DoctorInfo.DrName)
        End Get
    End Property
End Class

Public Class ApptDataBundle
    Public Sub New()
        Appointments = New List(Of AppointmentC)()
        DoctorInfos = New Dictionary(Of Integer, ApptDoctorInfo)()
        PatientNames = New Dictionary(Of Integer, String)()
    End Sub

    Public Property DateRange As ApptDateRange
    Public Property Appointments As List(Of AppointmentC)
    Public Property DoctorInfos As Dictionary(Of Integer, ApptDoctorInfo)
    Public Property PatientNames As Dictionary(Of Integer, String)

    Public Function ResolveDoctor(drId As Integer) As ApptDoctorInfo
        If DoctorInfos.ContainsKey(drId) Then Return DoctorInfos(drId)
        Return New ApptDoctorInfo With {.DrID = drId, .DrName = $"Doctor {drId}"}
    End Function

    Public Function ResolvePatientName(patientId As Integer) As String
        If PatientNames.ContainsKey(patientId) Then Return PatientNames(patientId)
        Return $"Patient {patientId}"
    End Function
End Class

Public Class ApptState
    Public Sub New()
        CurrentDate = Date.Today
        CurrentView = ApptViewMode.ThisWeekFull
        WorkStartTime = TimeSpan.FromHours(8)
        WorkEndTime = TimeSpan.FromHours(16)
        Use24HourFormat = False
        ShowPatientLabels = True
        IncludeReason = True
        UseBoldAppointments = True
        UseLargeAppointments = True
        VisibleStatus = Nothing
        VisibleReason = Nothing
        DoctorFilterId = Nothing
        PatientFilterId = Nothing
        PatientOnlyMode = False
        FilterStartDate = Date.Today.AddMonths(-1)
        FilterEndDate = Date.Today.AddMonths(1)
        ApptCardStartTimeColor = Color.Red
        ApptCardEndTimeColor = Color.SteelBlue
        ApptCardPatientNameColor = Color.FromArgb(33, 37, 41)
        ApptCardReasonColor = Color.FromArgb(0, 118, 128)
        ApptCardNotesColor = Color.FromArgb(93, 64, 140)
    End Sub

    Public Property CurrentDate As DateTime
    Public Property CurrentView As ApptViewMode
    Public Property WorkStartTime As TimeSpan
    Public Property WorkEndTime As TimeSpan
    Public Property Use24HourFormat As Boolean
    Public Property ShowPatientLabels As Boolean
    Public Property IncludeReason As Boolean
    Public Property UseBoldAppointments As Boolean
    Public Property UseLargeAppointments As Boolean
    Public Property DoctorFilterId As Integer?
    Public Property PatientFilterId As Integer?
    Public Property VisibleReason As String
    Public Property VisibleStatus As String
    Public Property FilterStartDate As DateTime
    Public Property FilterEndDate As DateTime
    Public Property PatientOnlyMode As Boolean
    ''' <summary>Appointment card start-time label foreground; persisted via <c>My.Settings</c> from <see cref="ApptHeaderCtl"/>.</summary>
    Public Property ApptCardStartTimeColor As Color
    ''' <summary>Appointment card end-time label foreground; persisted via <c>My.Settings</c> from <see cref="ApptHeaderCtl"/>.</summary>
    Public Property ApptCardEndTimeColor As Color
    ''' <summary>Patient name on appointment card; persisted from <see cref="ApptHeaderCtl"/> label-colors dialog.</summary>
    Public Property ApptCardPatientNameColor As Color
    ''' <summary>Reason line on appointment card; persisted from <see cref="ApptHeaderCtl"/> label-colors dialog.</summary>
    Public Property ApptCardReasonColor As Color
    ''' <summary>Notes line on appointment card; persisted from <see cref="ApptHeaderCtl"/> label-colors dialog.</summary>
    Public Property ApptCardNotesColor As Color

    Public Function Clone() As ApptState
        Return CType(MemberwiseClone(), ApptState)
    End Function

    Public Function GetWeekVariant() As ApptWeekVariant
        If CurrentView = ApptViewMode.ThisWeek Then
            Return ApptWeekVariant.CompactSixDay
        End If
        Return ApptWeekVariant.FullSevenDay
    End Function
End Class

Public Class ApptViewRequest
    Public Property State As ApptState
    Public Property Data As ApptDataBundle
    Public Property AppointmentAppearanceSelector As Func(Of ApptCardVm, ApptCardAppearance)
    ''' <summary>Optional: after navigation, scroll this appointment into view (e.g. week column).</summary>
    Public Property PendingScrollAppointment As AppointmentC
    ''' <summary>Long-press before <c>DoDragDrop</c> on week cards (<see cref="SchedulerNew.DragHoldTimeMs"/>).</summary>
    Public Property DragHoldTimeMs As Integer = 750
End Class

Public Interface IApptViewCtl
    Property InteractionHub As ApptInteractionHub
    Sub BindData(request As ApptViewRequest)
End Interface

Public Class ApptInteractionHub
    Public Event AppointmentClicked As Action(Of AppointmentC)
    Public Event AppointmentDoubleClicked As Action(Of AppointmentC)
    Public Event EmptyDateInvoked As Action(Of DateTime)
    ''' <summary>English <paramref name="statusKey"/> (Pending, Running, …) and chip color from <see cref="GetStandardAppointmentStatusColors"/>.</summary>
    Public Event AppointmentStatusChangeRequested As Action(Of AppointmentC, String, Color)
    Public Event WeekColumnAppointmentDrop As Action(Of AppointmentC, DateTime, DateTime)
    ''' <summary>New start/end times (usually same day, different slots or resized).</summary>
    Public Event AppointmentTimeChangeRequested As Action(Of AppointmentC, DateTime, DateTime)

    Public Sub PublishAppointmentClicked(appointment As AppointmentC)
        RaiseEvent AppointmentClicked(appointment)
    End Sub

    Public Sub PublishAppointmentDoubleClicked(appointment As AppointmentC)
        RaiseEvent AppointmentDoubleClicked(appointment)
    End Sub

    Public Sub PublishEmptyDateInvoked(clickedDate As DateTime)
        RaiseEvent EmptyDateInvoked(clickedDate)
    End Sub

    Public Sub PublishAppointmentStatusChange(appointment As AppointmentC, statusKey As String, statusColor As Color)
        RaiseEvent AppointmentStatusChangeRequested(appointment, statusKey, statusColor)
    End Sub

    Public Sub PublishWeekColumnAppointmentDrop(appointment As AppointmentC, sourceDay As DateTime, targetDay As DateTime)
        RaiseEvent WeekColumnAppointmentDrop(appointment, sourceDay, targetDay)
    End Sub

    Public Sub PublishAppointmentTimeChange(appointment As AppointmentC, newStart As DateTime, newEnd As DateTime)
        RaiseEvent AppointmentTimeChangeRequested(appointment, newStart, newEnd)
    End Sub
End Class

Public Module ApptTheme
    ''' <summary>Warm luminous wash for month/week day slide drawer (cheerful, not stark white).</summary>
    Public ReadOnly DrawerDayPanelWash As Color = Color.FromArgb(255, 249, 240)
    ''' <summary>Solid header band for the day drawer — deep cheerful teal.</summary>
    Public ReadOnly DrawerDayHeaderSolid As Color = Color.FromArgb(0, 124, 140)
    ''' <summary>Separator under drawer header.</summary>
    Public ReadOnly DrawerDayHeaderRule As Color = Color.FromArgb(0, 104, 118)
    ''' <summary>Light “card” fill for appointment rows inside the warm drawer.</summary>
    Public ReadOnly DrawerApptCardSurface As Color = Color.FromArgb(255, 254, 250)
    ''' <summary>Close button on dark drawer header (idle).</summary>
    Public ReadOnly DrawerHeaderCloseIdle As Color = Color.FromArgb(200, 235, 240)

    ''' <summary>Enables double-buffering on <see cref="Control"/>s that do not expose it (e.g. <see cref="Panel"/>, some DevExpress hosts). Same idea as the scheduler forms.</summary>
    Public Sub SetControlDoubleBuffered(c As Control)
        If c Is Nothing Then Return
        Try
            Dim p = GetType(Control).GetProperty("DoubleBuffered", BindingFlags.Instance Or BindingFlags.NonPublic)
            If p IsNot Nothing Then p.SetValue(c, True, Nothing)
        Catch
        End Try
    End Sub

    Public Function CreateCalibriFont(size As Single, Optional style As FontStyle = FontStyle.Regular) As Font
        Return New Font("Calibri", Math.Max(6.0F, size), style)
    End Function

    Public Function ParseDoctorColor(colorText As String) As Color
        Try
            If Not String.IsNullOrWhiteSpace(colorText) Then
                Dim hex = colorText.Trim()
                If Not hex.StartsWith("#", StringComparison.Ordinal) Then hex = "#" & hex
                Return ColorTranslator.FromHtml(hex)
            End If
        Catch
        End Try

        Return Color.LightSteelBlue
    End Function

    ''' <summary>Localized status caption; English keys stay in <c>AppointmentC.Status</c> (<see cref="SchedulerNew.TranslateAppointmentStatus"/>).</summary>
    Public Function TranslateAppointmentStatus(statusKey As String) As String
        If Eng OrElse String.IsNullOrEmpty(statusKey) Then Return statusKey
        Select Case statusKey
            Case "Pending"
                Return "قيد الانتظار"
            Case "Running"
                Return "قيد التنفيذ"
            Case "Completed"
                Return "منجز"
            Case "Canceled"
                Return "ملغى"
            Case "Postponed"
                Return "مؤجل"
            Case Else
                Return statusKey
        End Select
    End Function

    Public Function GetAppointmentStatusDisplayText(appt As AppointmentC) As String
        If appt Is Nothing Then Return ""
        Return TranslateAppointmentStatus(If(appt.Status, ""))
    End Function

    ''' <summary>Same keys/colors as <see cref="SchedulerNew"/> status context menu.</summary>
    Public Function GetStandardAppointmentStatusColors() As Dictionary(Of String, Color)
        Return New Dictionary(Of String, Color) From {
            {"Pending", Color.LightGoldenrodYellow},
            {"Running", Color.LightSkyBlue},
            {"Completed", Color.LightGreen},
            {"Canceled", Color.LightCoral},
            {"Postponed", Color.LightGray}
        }
    End Function

    Public Function GetStatusBackColor(statusText As String) As Color
        Dim key = If(statusText, "").Trim().ToLowerInvariant()
        Select Case key
            Case "pending"
                Return Color.LightGoldenrodYellow
            Case "running"
                Return Color.LightSkyBlue
            Case "completed"
                Return Color.LightGreen
            Case "canceled", "cancelled"
                Return Color.LightCoral
            Case "postponed"
                Return Color.LightGray
            Case Else
                Return Color.Gainsboro
        End Select
    End Function

    Public Function GetReadableForeColor(backColor As Color) As Color
        Dim luminance = (0.299R * backColor.R + 0.587R * backColor.G + 0.114R * backColor.B) / 255.0R
        Return If(luminance < 0.5R, Color.White, Color.Black)
    End Function

    ''' <summary>Card surface: blends <see cref="ControlPaint.Light"/>, <see cref="ControlPaint.LightLight"/>, and white so the hue follows the accent.</summary>
    Public Function BlendCardBackFromAccent(accent As Color) As Color
        Dim a = If(accent.A < 255, Color.FromArgb(255, accent.R, accent.G, accent.B), accent)
        Dim lightA = ControlPaint.Light(a)
        Dim lightB = ControlPaint.LightLight(a)
        Const wa = 0.34F
        Const wb = 0.34F
        Const ww = 0.32F
        Dim r = CInt(lightA.R * wa + lightB.R * wb + 255.0F * ww)
        Dim g = CInt(lightA.G * wa + lightB.G * wb + 255.0F * ww)
        Dim b = CInt(lightA.B * wa + lightB.B * wb + 255.0F * ww)
        Return Color.FromArgb(255, Math.Min(255, Math.Max(0, r)), Math.Min(255, Math.Max(0, g)), Math.Min(255, Math.Max(0, b)))
    End Function

    Public Function BuildDefaultCardAppearance(model As ApptCardVm, state As ApptState) As ApptCardAppearance
        Dim appearance = New ApptCardAppearance()
        Dim doctorColor = If(model IsNot Nothing AndAlso model.DoctorInfo IsNot Nothing, model.DoctorInfo.DrColor, Color.SteelBlue)
        Dim statusColor = GetStatusBackColor(If(model Is Nothing OrElse model.Appointment Is Nothing, Nothing, model.Appointment.Status))
        appearance.AccentColor = doctorColor
        appearance.BorderColor = Color.FromArgb(180, doctorColor)
        appearance.CardBackColor = BlendCardBackFromAccent(doctorColor)
        If state IsNot Nothing Then
            appearance.StartTimeStyle.ForeColor = state.ApptCardStartTimeColor
            appearance.EndTimeStyle.ForeColor = state.ApptCardEndTimeColor
        Else
            appearance.StartTimeStyle.ForeColor = Color.Red
            appearance.EndTimeStyle.ForeColor = ControlPaint.Dark(doctorColor, 0.08F)
        End If
        Dim endTint = Color.FromArgb(28, Math.Min(255, doctorColor.R + 55), Math.Min(255, doctorColor.G + 55), Math.Min(255, doctorColor.B + 40))
        appearance.EndTimeStyle.BackColor = endTint
        appearance.StatusStyle.BackColor = statusColor
        appearance.StatusStyle.ForeColor = GetReadableForeColor(statusColor)
        appearance.PatientStyle.FontStyle = If(state IsNot Nothing AndAlso state.UseBoldAppointments, FontStyle.Bold, FontStyle.Regular)
        appearance.PatientStyle.FontSize = If(state IsNot Nothing AndAlso state.UseLargeAppointments, 10.5F, 9.0F)
        Dim timeBold = If(state IsNot Nothing AndAlso state.UseBoldAppointments, FontStyle.Bold, FontStyle.Regular)
        Dim timeSize = If(state IsNot Nothing AndAlso state.UseLargeAppointments, 9.5F, 8.5F)
        appearance.StartTimeStyle.FontStyle = timeBold
        appearance.StartTimeStyle.FontSize = timeSize
        appearance.EndTimeStyle.FontStyle = timeBold
        appearance.EndTimeStyle.FontSize = timeSize

        Dim reasonNotesBold = If(state IsNot Nothing AndAlso state.UseBoldAppointments, FontStyle.Bold, FontStyle.Regular)
        Dim reasonSize = If(state IsNot Nothing AndAlso state.UseLargeAppointments, 10.5F, 9.0F)
        Dim notesSize = If(state IsNot Nothing AndAlso state.UseLargeAppointments, 10.0F, 8.75F)
        appearance.ReasonStyle.FontStyle = reasonNotesBold
        appearance.ReasonStyle.FontSize = reasonSize
        appearance.NotesStyle.FontStyle = reasonNotesBold
        appearance.NotesStyle.FontSize = notesSize
        If state IsNot Nothing Then
            appearance.PatientStyle.ForeColor = state.ApptCardPatientNameColor
            appearance.ReasonStyle.ForeColor = state.ApptCardReasonColor
            appearance.NotesStyle.ForeColor = state.ApptCardNotesColor
        Else
            appearance.PatientStyle.ForeColor = Color.FromArgb(33, 37, 41)
            appearance.ReasonStyle.ForeColor = Color.FromArgb(0, 118, 128)
            appearance.NotesStyle.ForeColor = Color.FromArgb(93, 64, 140)
        End If

        Dim docBg = ControlPaint.Light(doctorColor)
        appearance.DoctorStyle.BackColor = docBg
        appearance.DoctorStyle.ForeColor = GetReadableForeColor(docBg)
        appearance.DoctorStyle.FontStyle = If(state IsNot Nothing AndAlso state.UseBoldAppointments, FontStyle.Bold, FontStyle.Regular)
        appearance.DoctorStyle.FontSize = If(state IsNot Nothing AndAlso state.UseLargeAppointments, 9.75F, 9.0F)

        appearance.ReasonStyle.Visible = state IsNot Nothing AndAlso state.IncludeReason
        appearance.NotesStyle.Visible = state IsNot Nothing AndAlso state.IncludeReason
        Return appearance
    End Function

    Public Function MeasureTextHeight(text As String, font As Font, width As Integer, maxLines As Integer) As Integer
        Dim safeWidth = Math.Max(40, width)
        Dim lines = Math.Max(1, maxLines)
        Dim size = TextRenderer.MeasureText(If(text, ""), font, New Size(safeWidth, Integer.MaxValue), TextFormatFlags.WordBreak Or TextFormatFlags.Left)
        Dim lineHeight = Math.Max(CInt(Math.Ceiling(font.GetHeight())) + 2, 18)
        Return Math.Min(size.Height + 4, lineHeight * lines + 4)
    End Function

    Public Function FormatTimeRange(startValue As DateTime, endValue As DateTime, use24Hour As Boolean) As String
        Dim formatText = If(use24Hour, "HH:mm", "hh:mm tt")
        Return $"{startValue.ToString(formatText)} - {endValue.ToString(formatText)}"
    End Function

    Public Function FormatAppointmentTime(value As DateTime, use24Hour As Boolean) As String
        Dim formatText = If(use24Hour, "HH:mm", "hh:mm tt")
        Return value.ToString(formatText)
    End Function

    Public Function GetWeekStartSaturday(value As DateTime) As DateTime
        Dim currentDow = CInt(value.DayOfWeek)
        Dim daysToSaturday = (currentDow - 6 + 7) Mod 7
        Return value.Date.AddDays(-daysToSaturday)
    End Function

    ''' <summary>Sunday-based week start (matches <see cref="SchedulerNew"/> month / monthly-week hints).</summary>
    Public Function GetWeekStartSunday(value As DateTime) As DateTime
        Dim currentDow = CInt(value.DayOfWeek)
        Return value.Date.AddDays(-currentDow)
    End Function

    Public Function GetViewRange(state As ApptState) As ApptDateRange
        Dim rangeStart As DateTime
        Dim rangeEnd As DateTime

        Select Case state.CurrentView
            Case ApptViewMode.DayView, ApptViewMode.DoctorsDay
                rangeStart = state.CurrentDate.Date
                rangeEnd = rangeStart.AddDays(1)
            Case ApptViewMode.ThisWeek
                rangeStart = GetWeekStartSaturday(state.CurrentDate)
                rangeEnd = rangeStart.AddDays(6)
            Case ApptViewMode.ThisWeekFull, ApptViewMode.DaysTimeline
                rangeStart = GetWeekStartSaturday(state.CurrentDate)
                rangeEnd = rangeStart.AddDays(7)
            Case ApptViewMode.MonthlyWeek
                rangeStart = state.CurrentDate.Date.AddDays(-CInt(state.CurrentDate.DayOfWeek))
                rangeEnd = rangeStart.AddDays(7)
            Case Else
                rangeStart = New DateTime(state.CurrentDate.Year, state.CurrentDate.Month, 1)
                rangeEnd = rangeStart.AddMonths(1)
        End Select

        If state.FilterStartDate <> Date.MinValue Then
            rangeStart = If(rangeStart < state.FilterStartDate, rangeStart, state.FilterStartDate)
        End If

        If state.FilterEndDate <> Date.MinValue Then
            rangeEnd = If(rangeEnd > state.FilterEndDate, rangeEnd, state.FilterEndDate)
        End If

        Return New ApptDateRange With {.StartDate = rangeStart, .EndDate = rangeEnd}
    End Function

    ''' <summary>Week range string; same pattern as the header range label (<see cref="ApptHeaderCtl"/>).</summary>
    Public Function FormatCaptionWeekRange(startDate As Date, endDate As Date) As String
        Return $"{startDate:dd MMM} - {endDate:dd MMM yyyy}"
    End Function

    ''' <summary>Month line for range caption (<see cref="lblRange"/> month view).</summary>
    Public Function FormatCaptionMonthTitle(monthStart As Date) As String
        Return monthStart.ToString("MMMM yyyy")
    End Function

    ''' <summary>Full day line for range caption (day view).</summary>
    Public Function FormatCaptionDayFull(d As Date) As String
        Return d.ToString("dddd, dd MMM yyyy")
    End Function

    ''' <summary>Same filter dimensions as <see cref="ApptDataProvider.Load"/> / <c>AppointmentCRepository.GetFiltered</c> (patient, doctor, reason contains, status equals).</summary>
    Public Function AppointmentMatchesApptStateFilters(appointment As AppointmentC, state As ApptState) As Boolean
        If appointment Is Nothing OrElse state Is Nothing Then Return False
        If state.PatientFilterId.HasValue AndAlso appointment.PatientID <> state.PatientFilterId.Value Then Return False
        If state.DoctorFilterId.HasValue AndAlso appointment.DrID <> state.DoctorFilterId.Value Then Return False
        Dim reasonFilter = If(String.IsNullOrWhiteSpace(state.VisibleReason), Nothing, state.VisibleReason.Trim())
        If reasonFilter IsNot Nothing Then
            If String.IsNullOrEmpty(appointment.Reason) Then Return False
            If appointment.Reason.IndexOf(reasonFilter, StringComparison.OrdinalIgnoreCase) < 0 Then Return False
        End If
        Dim statusFilter = If(String.IsNullOrWhiteSpace(state.VisibleStatus), Nothing, state.VisibleStatus.Trim())
        If statusFilter IsNot Nothing Then
            Dim s = If(appointment.Status, "")
            If Not s.Equals(statusFilter, StringComparison.OrdinalIgnoreCase) Then Return False
        End If
        Return True
    End Function

    ''' <summary>Calendar day for list/export: <see cref="AppointmentC.AppDate"/> when set, otherwise <see cref="AppointmentC.StartDateTime"/>. (Aligns with <see cref="ApptDataProvider.NormalizeAppointmentDate"/> and <see cref="AppointmentCRepository.GetFiltered"/> OR AppDate.)</summary>
    Public Function GetAppointmentCalendarDay(a As AppointmentC) As Date
        If a Is Nothing Then Return DateTime.MinValue.Date
        If a.AppDate <> DateTime.MinValue Then Return a.AppDate.Date
        Return a.StartDateTime.Date
    End Function

    ''' <summary>True if <see cref="GetAppointmentCalendarDay"/> is within <paramref name="startD"/>..<paramref name="endD"/> (inclusive calendar dates), matching snapshot HTML and day UI.</summary>
    Public Function AppointmentCalendarDayInInclusiveRange(a As AppointmentC, startD As Date, endD As Date) As Boolean
        If a Is Nothing Then Return False
        Dim d = GetAppointmentCalendarDay(a)
        Return d >= startD.Date AndAlso d <= endD.Date
    End Function

    ''' <summary>Count of appointments that pass <see cref="AppointmentMatchesApptStateFilters"/> and whose calendar day falls in the same window as <see cref="BuildRangeCaption"/>.</summary>
    Public Function CountAppointmentsForCurrentView(state As ApptState, data As ApptDataBundle) As Integer
        If state Is Nothing OrElse data Is Nothing OrElse data.Appointments Is Nothing Then Return 0
        Dim startD As Date
        Dim endD As Date
        Select Case state.CurrentView
            Case ApptViewMode.DayView, ApptViewMode.DoctorsDay
                startD = state.CurrentDate.Date
                endD = startD
            Case ApptViewMode.ThisWeek
                startD = GetWeekStartSaturday(state.CurrentDate)
                endD = startD.AddDays(5)
            Case ApptViewMode.ThisWeekFull, ApptViewMode.DaysTimeline
                startD = GetWeekStartSaturday(state.CurrentDate)
                endD = startD.AddDays(6)
            Case ApptViewMode.MonthlyWeek
                startD = state.CurrentDate.Date.AddDays(-CInt(state.CurrentDate.DayOfWeek))
                endD = startD.AddDays(6)
            Case ApptViewMode.MonthView
                startD = New DateTime(state.CurrentDate.Year, state.CurrentDate.Month, 1)
                endD = startD.AddMonths(1).AddDays(-1)
        End Select
        Return data.Appointments.Where(Function(a) a IsNot Nothing AndAlso AppointmentMatchesApptStateFilters(a, state) AndAlso AppointmentCalendarDayInInclusiveRange(a, startD, endD)).Count()
    End Function

    Public Function BuildRangeCaption(state As ApptState, data As ApptDataBundle) As String
        If state Is Nothing Then Return ""

        Select Case state.CurrentView
            Case ApptViewMode.ThisWeek, ApptViewMode.ThisWeekFull, ApptViewMode.DaysTimeline
                Dim startDate = GetWeekStartSaturday(state.CurrentDate)
                Dim endDate = startDate.AddDays(If(state.CurrentView = ApptViewMode.ThisWeek, 5, 6))
                Return FormatCaptionWeekRange(startDate, endDate)
            Case ApptViewMode.DayView, ApptViewMode.DoctorsDay
                Return FormatCaptionDayFull(state.CurrentDate)
            Case ApptViewMode.MonthlyWeek
                Dim firstDay = state.CurrentDate.Date.AddDays(-CInt(state.CurrentDate.DayOfWeek))
                Return FormatCaptionWeekRange(firstDay, firstDay.AddDays(6))
            Case Else
                Dim monthStart = New DateTime(state.CurrentDate.Year, state.CurrentDate.Month, 1)
                Return FormatCaptionMonthTitle(monthStart)
        End Select
    End Function

#Region "Appointment display order (linked admin doctor, other doctors by name, then by time)"
    ''' <summary>Same rules as <see cref="ResolveCurrentLinkedDoctorId"/> in <see cref="SchedulerNew"/>: <see cref="PasswordSecurity.CurrentDoctor"/>, <see cref="PasswordSecurity.CurrentUser"/>.<see langword="DrID"/>, <see cref="PasswordSecurity.LoggedInDoctorID"/>.</summary>
    Public Function ResolveDisplayLinkedDrId() As Integer
        Try
            If PasswordSecurity.CurrentDoctor IsNot Nothing AndAlso PasswordSecurity.CurrentDoctor.DrID > 0 Then
                Return PasswordSecurity.CurrentDoctor.DrID
            End If
        Catch
        End Try
        Try
            If PasswordSecurity.CurrentUser IsNot Nothing AndAlso PasswordSecurity.CurrentUser.DrID.HasValue AndAlso PasswordSecurity.CurrentUser.DrID.Value > 0 Then
                Return PasswordSecurity.CurrentUser.DrID.Value
            End If
        Catch
        End Try
        If PasswordSecurity.LoggedInDoctorID > 0 Then Return PasswordSecurity.LoggedInDoctorID
        Return 0
    End Function

    Private Function SafeDisplayDoctorName(getDoctorName As Func(Of Integer, String), drId As Integer) As String
        Try
            Dim n = getDoctorName(drId)
            If String.IsNullOrWhiteSpace(n) Then Return "Doctor " & drId
            Return n.Trim()
        Catch
            Return "Doctor " & drId
        End Try
    End Function

    Private Function LinkedVsOthersSortKey(isLinked As Boolean, linkedDoctorAtEnd As Boolean) As Integer
        If Not isLinked Then Return If(linkedDoctorAtEnd, 0, 1)
        Return If(linkedDoctorAtEnd, 1, 0)
    End Function

    ''' <summary>Top-to-bottom: by default the linked (admin) doctor first (scheduler); set <paramref name="linkedDoctorAtEnd"/> to put that doctor last (new Appt module week/day views).</summary>
    Public Function OrderAppointmentsForDisplay(source As IEnumerable(Of AppointmentC), getDoctorName As Func(Of Integer, String), Optional linkedDoctorAtEnd As Boolean = False) As List(Of AppointmentC)
        If source Is Nothing Then Return New List(Of AppointmentC)()
        Dim linked = ResolveDisplayLinkedDrId()
        Return source.
            OrderBy(Function(a) LinkedVsOthersSortKey(linked > 0 AndAlso a.DrID = linked, linkedDoctorAtEnd)).
            ThenBy(Function(a) SafeDisplayDoctorName(getDoctorName, a.DrID)).
            ThenBy(Function(a) a.StartDateTime).
            ThenBy(Function(a) a.EndDateTime).
            ToList()
    End Function

    ''' <summary>Top-to-bottom: by default the linked (admin) doctor first; set <paramref name="linkedDoctorAtEnd"/> to put that doctor last (new Appt module).</summary>
    Public Function OrderAppointmentsForDisplay(source As IEnumerable(Of AppointmentC), data As ApptDataBundle, Optional linkedDoctorAtEnd As Boolean = False) As List(Of AppointmentC)
        If data Is Nothing Then
            If source Is Nothing Then Return New List(Of AppointmentC)()
            Return source.OrderBy(Function(a) a.StartDateTime).ToList()
        End If
        Return OrderAppointmentsForDisplay(source, Function(id) data.ResolveDoctor(id).DrName, linkedDoctorAtEnd)
    End Function

    ''' <summary>Doctor columns: by default linked doctor first; set <paramref name="linkedDoctorAtEnd"/> to put that column last (new Appt module).</summary>
    Public Function OrderDoctorColumnIdsForDisplay(drIds As IEnumerable(Of Integer), getDoctorName As Func(Of Integer, String), Optional linkedDoctorAtEnd As Boolean = False) As List(Of Integer)
        If drIds Is Nothing Then Return New List(Of Integer)()
        Dim linked = ResolveDisplayLinkedDrId()
        Return drIds.Distinct().
            OrderBy(Function(id) LinkedVsOthersSortKey(linked > 0 AndAlso id = linked, linkedDoctorAtEnd)).
            ThenBy(Function(id) SafeDisplayDoctorName(getDoctorName, id)).
            ToList()
    End Function

    ''' <summary>Doctor columns: by default linked doctor first; set <paramref name="linkedDoctorAtEnd"/> to put that column last (new Appt module).</summary>
    Public Function OrderDoctorColumnIdsForDisplay(drIds As IEnumerable(Of Integer), data As ApptDataBundle, Optional linkedDoctorAtEnd As Boolean = False) As List(Of Integer)
        If data Is Nothing Then
            If drIds Is Nothing Then Return New List(Of Integer)()
            Return drIds.Distinct().OrderBy(Function(x) x).ToList()
        End If
        Return OrderDoctorColumnIdsForDisplay(drIds, Function(id) data.ResolveDoctor(id).DrName, linkedDoctorAtEnd)
    End Function
#End Region
End Module
