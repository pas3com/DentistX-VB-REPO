Imports System
Imports System.Collections.Generic
Imports System.Drawing
Imports System.Globalization
Imports System.Linq
Imports System.Windows.Forms
Imports DevExpress.XtraEditors

''' <summary>Builds <see cref="WeekSnapshotHtmlContext"/> for any Appt/Scheduler view from live data and the same card appearance rules as the UI.</summary>
Friend NotInheritable Class ApptSnapshotHtmlBuilder
    Private Sub New()
    End Sub

    ' Match <see cref="ApptWeekCtl"/> day header tints
    Private Shared ReadOnly DayHeaderTints As Color() = {
        Color.FromArgb(255, 230, 230),
        Color.FromArgb(255, 245, 220),
        Color.FromArgb(240, 255, 230),
        Color.FromArgb(230, 250, 255),
        Color.FromArgb(245, 230, 255),
        Color.FromArgb(255, 255, 230),
        Color.FromArgb(230, 255, 240)
    }
    Private Shared ReadOnly TodayHeaderTint As Color = Color.FromArgb(180, 220, 255)
    Private Shared ReadOnly HeaderTitleFore As Color = Color.FromArgb(36, 64, 120)
    Private Shared ReadOnly HeaderDateLineFore As Color = Color.FromArgb(75, 84, 99)

    ''' <summary>Appt host: use live week control when present; otherwise build from <see cref="ApptViewRequest.Data"/> for the current view.</summary>
    Public Shared Function BuildForApptModule(request As ApptViewRequest, currentView As XtraUserControl, caption As String) As WeekSnapshotHtmlContext
        If request Is Nothing OrElse request.State Is Nothing OrElse request.Data Is Nothing Then Return Nothing
        Dim wk = TryCast(currentView, ApptWeekCtl)
        If wk IsNot Nothing Then
            Return wk.BuildHtmlExportContext(caption)
        End If
        Dim dayCtl = TryCast(currentView, ApptDayCtl)
        If dayCtl IsNot Nothing Then
            Return dayCtl.BuildHtmlExportContext(caption)
        End If
        Dim dayLine = TryCast(currentView, ApptDayLine)
        If dayLine IsNot Nothing Then
            Return dayLine.BuildHtmlExportContext(caption)
        End If
        Return BuildFromViewRequestData(request, caption, linkedDoctorAtEnd:=True)
    End Function

    ''' <summary>Scheduler and any caller with a filled <see cref="ApptViewRequest"/> (no <see cref="ApptWeekCtl"/> required).</summary>
    ''' <param name="linkedDoctorAtEnd">When true (new Appt module HTML), the admin-linked doctor is last; when false (classic scheduler export), that doctor is first to match on-screen order.</param>
    Public Shared Function BuildFromViewRequestData(request As ApptViewRequest, caption As String, Optional linkedDoctorAtEnd As Boolean = False) As WeekSnapshotHtmlContext
        If request Is Nothing OrElse request.State Is Nothing OrElse request.Data Is Nothing Then Return Nothing
        Dim st = request.State
        Dim data = request.Data
        If data.Appointments Is Nothing OrElse data.Appointments.Count = 0 Then
            Return EmptyContext(caption, If(Eng, "No appointments in range.", "لا مواعيد في النطاق."))
        End If
        Dim apps = FilterForView(st, data)
        If apps.Count = 0 Then
            Return EmptyContext(caption, If(Eng, "No appointments in range.", "لا مواعيد في النطاق."))
        End If

        Dim ctx As New WeekSnapshotHtmlContext() With {
            .Caption = If(caption, ""),
            .GeneratedUtc = DateTime.UtcNow
        }
        Select Case st.CurrentView
            Case ApptViewMode.ThisWeek, ApptViewMode.ThisWeekFull
                AddWeekDayColumns(ctx, st, request, apps, st.GetWeekVariant() = ApptWeekVariant.CompactSixDay, linkedDoctorAtEnd)
            Case ApptViewMode.DayView
                AddDayViewTimeline(ctx, st, request, apps, linkedDoctorAtEnd)
            Case ApptViewMode.DoctorsDay
                AddDoctorDayColumns(ctx, st, request, apps, linkedDoctorAtEnd)
            Case ApptViewMode.DaysTimeline
                AddDaysHorizontalTimeline(ctx, st, request, apps, linkedDoctorAtEnd)
            Case ApptViewMode.MonthlyWeek
                Dim firstD = st.CurrentDate.Date.AddDays(-CInt(st.CurrentDate.DayOfWeek))
                AddCalendarDayColumns(ctx, st, request, apps, firstD, 7, linkedDoctorAtEnd)
            Case ApptViewMode.MonthView
                AddMonthStacked(ctx, st, request, apps, linkedDoctorAtEnd)
            Case Else
                Return Nothing
        End Select
        If ctx.WeekHorizontal IsNot Nothing Then Return ctx
        If ctx.DayTimeline IsNot Nothing Then Return ctx
        Return If(ctx.Columns.Count = 0, Nothing, ctx)
    End Function

    ''' <summary>HTML/PNG data model for <see cref="ApptViewMode.DaysTimeline"/> (horizontal time axis, day rows) — used by <see cref="ApptDayLine"/> and <see cref="BuildFromViewRequestData"/>.</summary>
    Public Shared Function BuildDaysTimelineHtmlContext(request As ApptViewRequest, caption As String, Optional linkedDoctorAtEnd As Boolean = False) As WeekSnapshotHtmlContext
        If request Is Nothing OrElse request.State Is Nothing OrElse request.Data Is Nothing Then Return Nothing
        If request.Data.Appointments Is Nothing OrElse request.Data.Appointments.Count = 0 Then
            Return EmptyContext(If(caption, ""), If(Eng, "No appointments in range.", "لا مواعيد في النطاق."))
        End If
        Dim apps = FilterForView(request.State, request.Data)
        If apps.Count = 0 Then
            Return EmptyContext(If(caption, ""), If(Eng, "No appointments in range.", "لا مواعيد في النطاق."))
        End If
        Dim ctx As New WeekSnapshotHtmlContext() With {
            .Caption = If(caption, ""),
            .GeneratedUtc = DateTime.UtcNow
        }
        AddDaysHorizontalTimeline(ctx, request.State, request, apps, linkedDoctorAtEnd)
        Return ctx
    End Function

    Private Shared Function EmptyContext(caption As String, emptyMsg As String) As WeekSnapshotHtmlContext
        Dim ctx As New WeekSnapshotHtmlContext() With {
            .Caption = If(caption, ""),
            .GeneratedUtc = DateTime.UtcNow
        }
        Dim col As New WeekSnapshotHtmlColumn With {
            .DayTitle = If(Eng, "Schedule", "الجدول"),
            .DateLine = emptyMsg,
            .HeaderBackColor = DayHeaderTints(0),
            .HeaderTitleForeColor = HeaderTitleFore,
            .DateLineForeColor = HeaderDateLineFore,
            .IsToday = False
        }
        ctx.Columns.Add(col)
        Return ctx
    End Function

    Private Shared Function FilterForView(st As ApptState, data As ApptDataBundle) As List(Of AppointmentC)
        Dim startD As Date
        Dim endD As Date
        GetViewDateWindow(st, startD, endD)
        Return data.Appointments.
            Where(Function(a) a IsNot Nothing AndAlso
                ApptTheme.AppointmentMatchesApptStateFilters(a, st) AndAlso
                ApptTheme.AppointmentCalendarDayInInclusiveRange(a, startD, endD)).
            ToList()
    End Function

    ''' <summary>Maps <see cref="ApptState.OrderByDoctorId"/> to the ordering helper (<c>Nothing</c> when unset).</summary>
    Private Shared Function PreferOrderDoctorId(st As ApptState) As Integer?
        If st Is Nothing Then Return Nothing
        If Not st.OrderByDoctorId.HasValue OrElse st.OrderByDoctorId.Value <= 0 Then Return Nothing
        Return st.OrderByDoctorId.Value
    End Function

    Private Shared Sub GetViewDateWindow(st As ApptState, ByRef startD As Date, ByRef endD As Date)
        Select Case st.CurrentView
            Case ApptViewMode.DayView, ApptViewMode.DoctorsDay
                startD = st.CurrentDate.Date
                endD = startD
            Case ApptViewMode.ThisWeek
                startD = ApptTheme.GetWeekStartSaturday(st.CurrentDate)
                endD = startD.AddDays(5)
            Case ApptViewMode.ThisWeekFull, ApptViewMode.DaysTimeline
                startD = ApptTheme.GetWeekStartSaturday(st.CurrentDate)
                endD = startD.AddDays(6)
            Case ApptViewMode.MonthlyWeek
                startD = st.CurrentDate.Date.AddDays(-CInt(st.CurrentDate.DayOfWeek))
                endD = startD.AddDays(6)
            Case ApptViewMode.MonthView
                Dim ms = New DateTime(st.CurrentDate.Year, st.CurrentDate.Month, 1)
                startD = ms
                endD = ms.AddMonths(1).AddDays(-1)
            Case Else
                startD = st.CurrentDate.Date
                endD = startD
        End Select
    End Sub

    Private Shared Sub AddDaysHorizontalTimeline(ctx As WeekSnapshotHtmlContext, st As ApptState, request As ApptViewRequest, apps As List(Of AppointmentC), linkedDoctorAtEnd As Boolean)
        Dim weekStart = ApptTheme.GetWeekStartSaturday(st.CurrentDate)
        Dim startHour = Math.Max(0, Math.Min(23, CInt(Math.Floor(st.WorkStartTime.TotalHours))))
        Dim endHour = Math.Min(24, Math.Max(startHour + 1, CInt(Math.Ceiling(st.WorkEndTime.TotalHours))))
        Dim totalTimeMinutes = Math.Max(30, (endHour - startHour) * 60)
        Dim dlW = ApptDayLine.DayLabelWidth
        ' Time axis width in HTML: same floor as live ApptDayLine minimum pixels-per-hour across visible hours; horizontal scroll when needed.
        Dim timelineRailMinPx = CInt(Math.Ceiling((CDbl(totalTimeMinutes) / 60.0R) * ApptDayLine.MinTimelinePixelsPerHour))
        timelineRailMinPx = Math.Max(120, timelineRailMinPx)
        Const laneRowStepPx As Integer = 96
        Const minPlacedH As Integer = 72
        Dim stripe30Pct = 30.0R * 100.0R / CDbl(Math.Max(1, totalTimeMinutes))

        Dim wh As New WeekSnapshotHtmlWeekHorizontal With {
            .BannerTitle = ApptTheme.FormatCaptionWeekRange(weekStart, weekStart.AddDays(6)),
            .Use24HourFormat = st.Use24HourFormat,
            .DayLabelWidthCssPx = dlW,
            .TimelineBodyMinWidthCssPx = timelineRailMinPx,
            .TimeGridStripePeriodPercent = stripe30Pct,
            .StartHour = startHour,
            .EndHour = endHour,
            .TotalTimeMinutes = totalTimeMinutes
        }

        For offsetMin = 0 To totalTimeMinutes - 1 Step 30
            Dim leftP = 100.0R * (offsetMin / CDbl(Math.Max(1, totalTimeMinutes)))
            Dim tickTime = DateTime.Today.AddHours(startHour).AddMinutes(offsetMin)
            Dim isMajor = (offsetMin Mod 60 = 0)
            Dim txt = If(st.Use24HourFormat, tickTime.ToString("H:mm", CultureInfo.CurrentCulture), tickTime.ToString("h:mm tt", CultureInfo.CurrentCulture))
            wh.HourTickLabels.Add(New WeekSnapshotHtmlWeekHourTick With {
                .LeftPercent = Math.Max(0R, Math.Min(100R, leftP)),
                .Text = txt,
                .IsMajor = isMajor
            })
        Next

        Dim z As Integer = 1
        For dayIndex = 0 To 6
            Dim d = weekStart.AddDays(dayIndex)
            Dim dayRaw = apps.Where(Function(a) ApptTheme.GetAppointmentCalendarDay(a) = d.Date)
            Dim dayAppts = ApptTheme.OrderAppointmentsForWeekDayGroupsAndSolos(dayRaw, request.Data, PreferOrderDoctorId(st))

            Dim stackRows As New List(Of List(Of AppointmentC))()
            For Each ap In dayAppts
                Dim put = False
                For Each r In stackRows
                    If r.All(Function(ex) ex.EndDateTime <= ap.StartDateTime OrElse ap.EndDateTime <= ex.StartDateTime) Then
                        r.Add(ap)
                        put = True
                        Exit For
                    End If
                Next
                If Not put Then stackRows.Add(New List(Of AppointmentC) From {ap})
            Next

            Dim drow As New WeekSnapshotHtmlWeekHDayRow With {
                .DayLabel = AppointDateFormat.FormatDayShortDate(d),
                .IsToday = (d.Date = DateTime.Today)
            }

            If stackRows.Count = 0 Then
                drow.RowBodyHeightCssPx = 48
            Else
                drow.RowBodyHeightCssPx = Math.Max(48, stackRows.Count * laneRowStepPx + 20)
            End If

            For rowIdx = 0 To stackRows.Count - 1
                For Each ap In stackRows(rowIdx)
                    Dim startMin = (ap.StartDateTime.TimeOfDay - TimeSpan.FromHours(startHour)).TotalMinutes
                    Dim endMin = (ap.EndDateTime.TimeOfDay - TimeSpan.FromHours(startHour)).TotalMinutes
                    If startMin < 0 Then startMin = 0
                    If endMin > totalTimeMinutes Then endMin = totalTimeMinutes
                    If endMin <= startMin Then endMin = startMin + 15
                    Dim leftP2 = 100.0R * (startMin / totalTimeMinutes)
                    Dim widthP2 = 100.0R * ((endMin - startMin) / totalTimeMinutes)
                    leftP2 = Math.Max(0R, Math.Min(100R, leftP2))
                    widthP2 = Math.Max(0.3R, Math.Min(100R - leftP2, widthP2))

                    Dim topPx = 6 + rowIdx * laneRowStepPx
                    Dim htmlA = ToHtmlApptRow(CreateCardVm(ap, request), st.Use24HourFormat)
                    drow.Placed.Add(New WeekSnapshotHtmlWeekHPlacedAppt With {
                        .Html = htmlA,
                        .LeftPercent = leftP2,
                        .WidthPercent = widthP2,
                        .TopPx = topPx,
                        .StackingZ = z,
                        .MinHeightCssPx = minPlacedH
                    })
                    z += 1
                Next
            Next
            wh.DayRows.Add(drow)
        Next

        ctx.WeekHorizontal = wh
    End Sub

    Private Shared Sub AddWeekDayColumns(ctx As WeekSnapshotHtmlContext, st As ApptState, request As ApptViewRequest, apps As List(Of AppointmentC), sixDay As Boolean, linkedDoctorAtEnd As Boolean)
        Dim weekStart = ApptTheme.GetWeekStartSaturday(st.CurrentDate)
        Dim n = If(sixDay, 6, 7)
        For i = 0 To n - 1
            Dim d = weekStart.AddDays(i)
            Dim dayRaw = apps.Where(Function(a) ApptTheme.GetAppointmentCalendarDay(a) = d)
            Dim dayList = ApptTheme.OrderAppointmentsForWeekDayGroupsAndSolos(dayRaw, request.Data, PreferOrderDoctorId(st))
            Dim cnt = dayList.Count
            Dim apSfx = If(Eng, If(cnt = 1, "Appt", "Appts"), If(cnt = 1, "موعد", "مواعيد"))
            Dim c As New WeekSnapshotHtmlColumn With {
                .DayTitle = AppointDateFormat.FormatWeekdayOnly(d),
                .DateLine = $"{AppointDateFormat.FormatDate(d)}.({cnt} {apSfx})",
                .HeaderBackColor = If(d.Date = Date.Today, TodayHeaderTint, DayHeaderTints(i Mod DayHeaderTints.Length)),
                .HeaderTitleForeColor = HeaderTitleFore,
                .DateLineForeColor = HeaderDateLineFore,
                .IsToday = (d.Date = Date.Today)
            }
            For Each ap In dayList
                c.Appointments.Add(ToHtmlApptRow(CreateCardVm(ap, request), st.Use24HourFormat))
            Next
            ctx.Columns.Add(c)
        Next
    End Sub

    ' Match <see cref="ApptDayCtl"/> / HTML output density (30-min row height in CSS).
    Private Const DayHtmlSlotHeightPx As Integer = 40
    Private Const DayHtmlOverlapLanePx As Integer = 4

    ''' <summary>Day view: time gutter + 30-min grid + one column per doctor, time-proportional cards (same geometry as <see cref="ApptDayCtl"/>).</summary>
    Private Shared Sub AddDayViewTimeline(ctx As WeekSnapshotHtmlContext, st As ApptState, request As ApptViewRequest, apps As List(Of AppointmentC), linkedDoctorAtEnd As Boolean)
        Dim day = st.CurrentDate.Date
        Dim workStart = day.Add(st.WorkStartTime)
        Dim workEnd = day.Add(st.WorkEndTime)
        If workEnd <= workStart Then workEnd = workStart.AddHours(1)

        Dim startMinTotal = CInt(workStart.TimeOfDay.TotalMinutes)
        Dim snapStart = (startMinTotal \ 30) * 30
        Dim gridStart = day.Date.AddMinutes(snapStart)
        Dim totalMin = Math.Max(30, CInt(Math.Ceiling((workEnd - gridStart).TotalMinutes)))
        Dim totalSlots = Math.Max(1, CInt(Math.Ceiling(totalMin / 30.0R)))
        Dim totalSpanMin = totalSlots * 30

        Dim workStartDay = workStart
        Dim workEndDay = workEnd

        Dim byDr = apps.GroupBy(Function(a) a.DrID).ToDictionary(Function(g) g.Key, Function(g) g)
        Dim drIds = If(st.DoctorFilterId.HasValue AndAlso st.DoctorFilterId.Value > 0,
            New List(Of Integer) From {st.DoctorFilterId.Value},
            ApptTheme.OrderDoctorColumnIdsForDisplay(apps.Select(Function(a) a.DrID), request.Data, linkedDoctorAtEnd, PreferOrderDoctorId(st)))
        drIds = drIds.Where(Function(id) byDr.ContainsKey(id)).ToList()

        Dim tl As New WeekSnapshotHtmlDayTimeline With {
            .BannerTitle = ApptTheme.FormatCaptionDayFull(day),
            .Use24HourFormat = st.Use24HourFormat,
            .GridStart = gridStart,
            .GridEnd = gridStart.AddMinutes(totalSpanMin),
            .TimeSlotCount = totalSlots,
            .SlotHeightCssPx = DayHtmlSlotHeightPx,
            .TimeGutterCssPx = 64
        }

        If drIds.Count = 0 Then
            Dim c0 As New WeekSnapshotHtmlDayColumn With {
                .DoctorName = If(Eng, "—", "—")
            }
            tl.DoctorColumns.Add(c0)
            ctx.DayTimeline = tl
            Return
        End If

        For Each drId In drIds
            Dim grp = byDr(drId)
            Dim dName = If(request.Data.ResolveDoctor(drId).DrName, $"Doctor {drId}")
            Dim col As New WeekSnapshotHtmlDayColumn With {.DoctorName = dName}
            Dim laneEnds As New List(Of DateTime)()
            Dim z As Integer = 1
            For Each ap In ApptTheme.OrderAppointmentsForDisplay(grp, request.Data, linkedDoctorAtEnd, PreferOrderDoctorId(st))
                Dim startT = If(ap.StartDateTime < workStartDay, workStartDay, ap.StartDateTime)
                Dim endT = If(ap.EndDateTime > workEndDay, workEndDay, ap.EndDateTime)
                If endT <= startT Then endT = startT.AddMinutes(1)

                Dim rowIndex = -1
                For j = 0 To laneEnds.Count - 1
                    If laneEnds(j) <= startT Then
                        rowIndex = j
                        Exit For
                    End If
                Next
                If rowIndex = -1 Then
                    rowIndex = laneEnds.Count
                    laneEnds.Add(endT)
                Else
                    laneEnds(rowIndex) = endT
                End If
                Dim nudge = rowIndex * DayHtmlOverlapLanePx

                Dim topP = 100.0R * CSng((startT - gridStart).TotalMinutes) / totalSpanMin
                Dim hP = 100.0R * CSng(Math.Max(1, (endT - startT).TotalMinutes)) / totalSpanMin
                If hP < 2.2F Then hP = 2.2F
                If topP < 0F Then topP = 0F
                If topP >= 100F Then Continue For
                If topP + hP > 100F Then hP = Math.Max(0.5F, 100F - topP)

                Dim htmlA = ToHtmlApptRow(CreateCardVm(ap, request), st.Use24HourFormat)
                col.Placed.Add(New WeekSnapshotHtmlDayPlacedAppt With {
                    .Html = htmlA,
                    .TopPercent = CDbl(topP),
                    .HeightPercent = CDbl(hP),
                    .LaneNudgePx = nudge,
                    .StackingZ = z
                })
                z += 1
            Next
            tl.DoctorColumns.Add(col)
        Next
        ctx.DayTimeline = tl
    End Sub

    Private Shared Sub AddDoctorDayColumns(ctx As WeekSnapshotHtmlContext, st As ApptState, request As ApptViewRequest, apps As List(Of AppointmentC), linkedDoctorAtEnd As Boolean)
        Dim dayD = st.CurrentDate.Date
        Dim byDr = apps.GroupBy(Function(a) a.DrID).ToDictionary(Function(g) g.Key, Function(g) g)
        Dim drIds = If(st.DoctorFilterId.HasValue AndAlso st.DoctorFilterId.Value > 0,
            New List(Of Integer) From {st.DoctorFilterId.Value},
            ApptTheme.OrderDoctorColumnIdsForDisplay(apps.Select(Function(a) a.DrID), request.Data, linkedDoctorAtEnd, PreferOrderDoctorId(st)))
        drIds = drIds.Where(Function(id) byDr.ContainsKey(id)).ToList()
        If drIds.Count = 0 Then Return
        For Each drId In drIds
            Dim grp = byDr(drId)
            Dim dName = If(request.Data.ResolveDoctor(drId).DrName, $"Doctor {drId}")
            Dim c As New WeekSnapshotHtmlColumn With {
                .DayTitle = dName,
                .DateLine = ApptTheme.FormatCaptionDayFull(dayD),
                .HeaderBackColor = DayHeaderTints(0),
                .HeaderTitleForeColor = HeaderTitleFore,
                .DateLineForeColor = HeaderDateLineFore,
                .IsToday = (dayD = Date.Today)
            }
            For Each ap In ApptTheme.OrderAppointmentsForDisplay(grp, request.Data, linkedDoctorAtEnd, PreferOrderDoctorId(st))
                c.Appointments.Add(ToHtmlApptRow(CreateCardVm(ap, request), st.Use24HourFormat))
            Next
            ctx.Columns.Add(c)
        Next
    End Sub

    Private Shared Sub AddCalendarDayColumns(ctx As WeekSnapshotHtmlContext, st As ApptState, request As ApptViewRequest, apps As List(Of AppointmentC), firstDay As Date, nDays As Integer, linkedDoctorAtEnd As Boolean)
        For i = 0 To nDays - 1
            Dim d = firstDay.AddDays(i)
            Dim dayRaw = apps.Where(Function(a) ApptTheme.GetAppointmentCalendarDay(a) = d)
            Dim dayList = ApptTheme.OrderAppointmentsForWeekDayGroupsAndSolos(dayRaw, request.Data, PreferOrderDoctorId(st))
            Dim cnt = dayList.Count
            Dim apSfx = If(Eng, If(cnt = 1, "Appt", "Appts"), If(cnt = 1, "موعد", "مواعيد"))
            Dim c As New WeekSnapshotHtmlColumn With {
                .DayTitle = AppointDateFormat.FormatDayShortDate(d),
                .DateLine = $"({cnt} {apSfx})",
                .HeaderBackColor = If(d.Date = Date.Today, TodayHeaderTint, DayHeaderTints(i Mod DayHeaderTints.Length)),
                .HeaderTitleForeColor = HeaderTitleFore,
                .DateLineForeColor = HeaderDateLineFore,
                .IsToday = (d.Date = Date.Today)
            }
            For Each ap In dayList
                c.Appointments.Add(ToHtmlApptRow(CreateCardVm(ap, request), st.Use24HourFormat))
            Next
            ctx.Columns.Add(c)
        Next
    End Sub

    Private Shared Sub AddMonthStacked(ctx As WeekSnapshotHtmlContext, st As ApptState, request As ApptViewRequest, apps As List(Of AppointmentC), linkedDoctorAtEnd As Boolean)
        ctx.UseVerticalLayout = True
        Dim byDay = apps.GroupBy(Function(a) ApptTheme.GetAppointmentCalendarDay(a)).OrderBy(Function(g) g.Key).ToList()
        For Each g In byDay
            Dim d = g.Key
            Dim list = ApptTheme.OrderAppointmentsForDisplay(g, request.Data, linkedDoctorAtEnd, PreferOrderDoctorId(st))
            Dim cnt = list.Count
            Dim apSfx = If(Eng, If(cnt = 1, "Appt", "Appts"), If(cnt = 1, "موعد", "مواعيد"))
            Dim c As New WeekSnapshotHtmlColumn With {
                .DayTitle = AppointDateFormat.FormatDayDate(d),
                .DateLine = $"({cnt} {apSfx})",
                .HeaderBackColor = If(d = Date.Today, TodayHeaderTint, DayHeaderTints(CInt(d.DayOfWeek) Mod DayHeaderTints.Length)),
                .HeaderTitleForeColor = HeaderTitleFore,
                .DateLineForeColor = HeaderDateLineFore,
                .IsToday = (d = Date.Today)
            }
            For Each ap In list
                c.Appointments.Add(ToHtmlApptRow(CreateCardVm(ap, request), st.Use24HourFormat))
            Next
            ctx.Columns.Add(c)
        Next
    End Sub

    Private Shared Function CreateCardVm(ap As AppointmentC, request As ApptViewRequest) As ApptCardVm
        Dim m As New ApptCardVm With {
            .Appointment = ap,
            .PatientName = request.Data.ResolvePatientName(ap.PatientID),
            .DoctorInfo = request.Data.ResolveDoctor(ap.DrID)
        }
        m.Appearance = ApptTheme.BuildDefaultCardAppearance(m, request.State)
        If request.AppointmentAppearanceSelector IsNot Nothing Then
            Dim custom = request.AppointmentAppearanceSelector(m)
            If custom IsNot Nothing Then
                m.Appearance = custom
            End If
        End If
        Return m
    End Function

    Private Shared Function ToHtmlApptRow(m As ApptCardVm, use24h As Boolean) As WeekSnapshotHtmlAppt
        If m Is Nothing OrElse m.Appointment Is Nothing Then Return Nothing
        Dim a = m.Appointment
        Dim ap = If(m.Appearance, New ApptCardAppearance())
        Dim t1 = ApptTheme.FormatAppointmentTime(a.StartDateTime, use24h)
        Dim t2 = ApptTheme.FormatAppointmentTime(a.EndDateTime, use24h)
        Dim tr = t1 & " – " & t2
        Dim stBg = ap.StatusStyle.BackColor
        If stBg.A = 0 OrElse stBg = Color.Transparent Then
            stBg = Color.FromArgb(103, 114, 229)
        End If
        Dim bdr = ap.BorderColor
        If bdr.A = 0 OrElse bdr = Color.Transparent Then
            bdr = Color.FromArgb(205, 210, 220)
        End If
        Dim reasVis = ap.ReasonStyle.Visible AndAlso Not String.IsNullOrWhiteSpace(If(a.Reason, ""))
        Dim noteVis = ap.NotesStyle.Visible AndAlso Not String.IsNullOrWhiteSpace(If(a.Notes, ""))
        Return New WeekSnapshotHtmlAppt With {
            .TimeRange = tr,
            .Patient = If(m.PatientName, ""),
            .Doctor = If(m.DoctorName, ""),
            .Reason = If(reasVis, If(a.Reason, ""), ""),
            .Notes = If(noteVis, If(a.Notes, ""), ""),
            .StatusText = If(Not String.IsNullOrWhiteSpace(If(a.Status, "")), ApptTheme.GetAppointmentStatusDisplayText(a), ""),
            .CardBackColor = ap.CardBackColor,
            .CardBorderColor = bdr,
            .AccentColor = ap.AccentColor,
            .TimeStartFore = ap.StartTimeStyle.ForeColor,
            .TimeEndFore = ap.EndTimeStyle.ForeColor,
            .PatientFore = ap.PatientStyle.ForeColor,
            .DoctorFore = ap.DoctorStyle.ForeColor,
            .ReasonFore = ap.ReasonStyle.ForeColor,
            .NotesFore = ap.NotesStyle.ForeColor,
            .StatusBack = stBg,
            .StatusFore = ap.StatusStyle.ForeColor
        }
    End Function
End Class
