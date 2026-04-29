Imports System
Imports System.Collections.Generic
Imports System.Drawing
Imports System.Diagnostics
Imports System.Globalization
Imports System.IO
Imports System.Linq
Imports System.Net
Imports System.Text

''' <summary>Data for a schedule week HTML export (independent of the bitmap snapshot pipeline).</summary>
Public NotInheritable Class WeekSnapshotHtmlContext
    Public Sub New()
        Columns = New List(Of WeekSnapshotHtmlColumn)()
    End Sub

    Public Property Caption As String
    Public Property GeneratedUtc As DateTime
    ''' <summary>True: render sections as a vertical stack (e.g. month). False: horizontal columns (week / day doctors).</summary>
    Public Property UseVerticalLayout As Boolean
    Public ReadOnly Property Columns As List(Of WeekSnapshotHtmlColumn)
    ''' <summary>When set, renders time gutter + per-doctor time-scaled columns; Columns is unused.</summary>
    Public Property DayTimeline As WeekSnapshotHtmlDayTimeline
    ''' <summary>When set, <see cref="ApptViewMode.DaysTimeline"/>: Sat–Fri rows, time axis horizontal (same model as <see cref="ApptDayLine"/>); Columns and <see cref="DayTimeline"/> are unused.</summary>
    Public Property WeekHorizontal As WeekSnapshotHtmlWeekHorizontal
End Class

''' <summary>Day-view single-day timeline (30-min rows, time column; matches live Day view control).</summary>
Public NotInheritable Class WeekSnapshotHtmlDayTimeline
    Public Sub New()
        DoctorColumns = New List(Of WeekSnapshotHtmlDayColumn)()
    End Sub

    Public Property BannerTitle As String
    Public Property Use24HourFormat As Boolean
    Public Property GridStart As DateTime
    ''' <summary>End of the last 30-min slot (exclusive for iteration).</summary>
    Public Property GridEnd As DateTime
    Public ReadOnly Property DoctorColumns As List(Of WeekSnapshotHtmlDayColumn)
    Public Property TimeSlotCount As Integer
    Public Property SlotHeightCssPx As Integer
    Public Property TimeGutterCssPx As Integer
End Class

Public NotInheritable Class WeekSnapshotHtmlDayColumn
    Public Sub New()
        Placed = New List(Of WeekSnapshotHtmlDayPlacedAppt)()
    End Sub

    Public Property DoctorName As String
    Public ReadOnly Property Placed As List(Of WeekSnapshotHtmlDayPlacedAppt)
End Class

Public NotInheritable Class WeekSnapshotHtmlDayPlacedAppt
    Public Property Html As WeekSnapshotHtmlAppt
    ''' <summary>0–100 of the column body (below doctor header).</summary>
    Public Property TopPercent As Double
    Public Property HeightPercent As Double
    Public Property LaneNudgePx As Integer
    Public Property StackingZ As Integer
End Class

''' <summary>Horizontal time axis, seven day rows (matches <see cref="ApptDayLine"/> / <see cref="SchedulerNew.RenderDaysTimelineView"/>).</summary>
Public NotInheritable Class WeekSnapshotHtmlWeekHorizontal
    Public Sub New()
        HourTickLabels = New List(Of WeekSnapshotHtmlWeekHourTick)()
        DayRows = New List(Of WeekSnapshotHtmlWeekHDayRow)()
    End Sub

    Public Property BannerTitle As String
    Public Property Use24HourFormat As Boolean
    ''' <summary>Width of the day-name column in px (CSS).</summary>
    Public Property DayLabelWidthCssPx As Integer
    ''' <summary>Visible grid spans [StartHour, EndHour) in wall-clock (same as live control).</summary>
    Public Property StartHour As Integer
    Public Property EndHour As Integer
    Public Property TotalTimeMinutes As Integer
    ''' <summary>Width of the scrollable time area (px) so 30-min positions match labels.</summary>
    Public Property TimelineBodyMinWidthCssPx As Integer
    ''' <summary>100 × (30 / <see cref="TotalTimeMinutes"/>) for CSS repeating vertical grid.</summary>
    Public Property TimeGridStripePeriodPercent As Double
    Public ReadOnly Property HourTickLabels As List(Of WeekSnapshotHtmlWeekHourTick)
    Public ReadOnly Property DayRows As List(Of WeekSnapshotHtmlWeekHDayRow)
End Class

Public NotInheritable Class WeekSnapshotHtmlWeekHourTick
    ''' <summary>0–100 along the time strip (left edge of label).</summary>
    Public Property LeftPercent As Double
    Public Property Text As String
    Public Property IsMajor As Boolean
End Class

Public NotInheritable Class WeekSnapshotHtmlWeekHDayRow
    Public Sub New()
        Placed = New List(Of WeekSnapshotHtmlWeekHPlacedAppt)()
    End Sub

    Public Property DayLabel As String
    Public Property IsToday As Boolean
    Public Property RowBodyHeightCssPx As Integer
    Public ReadOnly Property Placed As List(Of WeekSnapshotHtmlWeekHPlacedAppt)
End Class

Public NotInheritable Class WeekSnapshotHtmlWeekHPlacedAppt
    Public Property Html As WeekSnapshotHtmlAppt
    ''' <summary>0–100 of the timeline body width (time axis).</summary>
    Public Property LeftPercent As Double
    Public Property WidthPercent As Double
    Public Property TopPx As Integer
    Public Property StackingZ As Integer
    Public Property MinHeightCssPx As Integer
End Class

Public NotInheritable Class WeekSnapshotHtmlColumn
    Public Sub New()
        Appointments = New List(Of WeekSnapshotHtmlAppt)()
    End Sub

    Public Property DayTitle As String
    Public Property DateLine As String
    Public Property HeaderBackColor As Color
    Public Property HeaderTitleForeColor As Color
    Public Property DateLineForeColor As Color
    Public Property IsToday As Boolean
    Public ReadOnly Property Appointments As List(Of WeekSnapshotHtmlAppt)
End Class

''' <summary>One appointment row in the week HTML export, with the same color cues as the live cards.</summary>
Public NotInheritable Class WeekSnapshotHtmlAppt
    Public Property TimeRange As String
    Public Property Patient As String
    Public Property Doctor As String
    Public Property Reason As String
    Public Property Notes As String
    Public Property StatusText As String

    Public Property CardBackColor As Color
    Public Property CardBorderColor As Color
    Public Property AccentColor As Color

    Public Property TimeStartFore As Color
    Public Property TimeEndFore As Color
    Public Property PatientFore As Color
    Public Property DoctorFore As Color
    Public Property ReasonFore As Color
    Public Property NotesFore As Color
    Public Property StatusBack As Color
    Public Property StatusFore As Color
End Class

Public NotInheritable Class WeekSnapshotHtmlWriter
    Public Shared Function BuildDocument(ctx As WeekSnapshotHtmlContext) As String
        If ctx Is Nothing Then
            Return "<!DOCTYPE html><html><head><meta charset=""utf-8""/><title></title></head><body></body></html>"
        End If
        Dim title = If(String.IsNullOrWhiteSpace(ctx.Caption), If(Eng, "Schedule", "الجدول"), WebUtility.HtmlEncode(ctx.Caption))
        Dim isRtl = Not Eng
        Dim dir = If(isRtl, "rtl", "ltr")
        Dim lang = If(Eng, "en", "ar")
        Dim sb As New StringBuilder()
        sb.AppendLine("<!DOCTYPE html>")
        sb.AppendLine($"<html lang=""{lang}"" dir=""{dir}"">")
        sb.AppendLine("<head>")
        sb.AppendLine("<meta charset=""utf-8""/>")
        sb.AppendLine("<meta name=""viewport"" content=""width=device-width, initial-scale=1""/>")
        sb.AppendLine($"<title>{title}</title>")
        sb.AppendLine("<style type=""text/css"">")
        sb.AppendLine("body{font-family:Calibri,Segoe UI,sans-serif;margin:0;padding:12px;background:#e8e8ea;color:#1a1a1a;}")
        sb.AppendLine("h1{font-size:1.15rem;font-weight:600;margin:0 0 12px 0;color:#111;}")
        sb.AppendLine(".sub{font-size:0.85rem;color:#555;margin-bottom:10px;}")
        sb.AppendLine(".grid{display:flex;flex-direction:row;flex-wrap:nowrap;align-items:stretch;gap:8px;overflow-x:auto;padding-bottom:8px;}")
        sb.AppendLine(".grid.grid-v{flex-direction:column;align-items:stretch;overflow-x:visible;}")
        sb.AppendLine(".day{flex:1 1 0;min-width:150px;max-width:320px;border:1px solid #c8ccd0;border-radius:8px;background:#fff;box-shadow:0 1px 2px rgba(0,0,0,.06);display:flex;flex-direction:column;overflow:hidden;}")
        sb.AppendLine(".grid.grid-v .day{min-width:0;max-width:100%;}")
        sb.AppendLine(".day-hd{padding:8px 10px;text-align:center;border-bottom:1px solid #ddd;}")
        sb.AppendLine(".day-hd .t{font-size:0.95rem;}")
        sb.AppendLine(".day-hd .d{font-size:0.8rem;margin-top:2px;}")
        sb.AppendLine(".day-body{flex:1 1 auto;padding:6px 0 10px 0;background:#fff;min-height:24px;}")
        sb.AppendLine(".appt{margin:6px 8px;padding:8px 10px;border-radius:4px;box-sizing:border-box;border:1px solid #ccd0d5;word-wrap:break-word;}")
        sb.AppendLine(".appt .time{line-height:1.2;margin-bottom:4px;}")
        sb.AppendLine(".appt .line{margin-top:2px;font-size:0.92rem;line-height:1.25;}")
        sb.AppendLine(".appt .st{display:inline-block;margin-top:4px;padding:2px 6px;border-radius:3px;font-size:0.8rem;font-weight:600;}")
        sb.AppendLine(".empty{margin:8px 10px;font-size:0.9rem;color:#888;font-style:italic;}")
        sb.AppendLine(".dv-outer{margin-top:4px;border:1px solid #c8ccd0;border-radius:8px;background:#fff;overflow:hidden;box-shadow:0 1px 2px rgba(0,0,0,.06);}")
        sb.AppendLine(".dv-banner{padding:8px 10px;font-size:0.95rem;font-weight:600;background:#f8f9fa;border-bottom:1px solid #e2e4e6;color:#1a1a1a;}")
        sb.AppendLine(".dv-colshead{display:flex;flex-direction:row;align-items:stretch;}")
        sb.AppendLine(".dv-corner{flex:0 0 64px;min-width:64px;max-width:64px;border-bottom:1px solid #e2e4e6;background:#fafafa;}")
        sb.AppendLine(".dv-dochead{flex:1 1 0;min-width:100px;border-left:1px solid #e8e8e8;border-bottom:1px solid #e2e4e6;padding:5px 6px;text-align:center;font-size:0.8rem;font-weight:600;background:#f0f3f7;color:#2c3544;}")
        sb.AppendLine(".dv-row2{display:flex;flex-direction:row;align-items:stretch;}")
        sb.AppendLine(".dv-time{flex:0 0 64px;min-width:64px;max-width:64px;border-right:1px solid #e0e0e0;background:#fafafa;}")
        sb.AppendLine(".dv-time-slot{border-bottom:1px solid #e8e8e8;font-size:0.75rem;color:#5a5a5a;padding:2px 4px;box-sizing:border-box;text-align:center;}")
        sb.AppendLine(".dv-docs{flex:1 1 auto;display:flex;flex-direction:row;min-width:0;}")
        sb.AppendLine(".dv-doc{flex:1 1 0;min-width:100px;border-left:1px solid #e8e8e8;position:relative;}")
        sb.AppendLine(".dv-dbody{position:relative;background:repeating-linear-gradient(to bottom,transparent 0,transparent 39px,#eee 39px,#eee 40px);}")
        sb.AppendLine(".appt-tl{position:absolute;left:0;right:0;min-height:32px;overflow:hidden;z-index:1;box-sizing:border-box;padding:4px 5px;}")
        sb.AppendLine(".wh-page{margin-top:4px;overflow-x:auto;overflow-y:visible;-webkit-overflow-scrolling:touch;width:100%;box-sizing:border-box;padding-bottom:8px;}")
        sb.AppendLine(".wh-outer{--wh-dayw:128px;--wh-timeline:var(--wh-timew,2000px);min-width:var(--wh-min,2160px);width:var(--wh-min,2160px);border:1px solid #b8c0cc;border-radius:8px;background:#fff;box-shadow:0 1px 2px rgba(0,0,0,.08);display:block;box-sizing:border-box;}")
        sb.AppendLine(".wh-banner{padding:8px 10px;font-size:0.95rem;font-weight:600;background:#eef1f5;border-bottom:1px solid #c5ccd6;color:#1a1a1a;}")
        sb.AppendLine(".wh-hrow{display:flex;flex-direction:row;align-items:stretch;border-bottom:2px solid #7d8898;background:#f0f2f5;}")
        sb.AppendLine(".wh-corner{flex:0 0 var(--wh-dayw);min-width:var(--wh-dayw);box-sizing:border-box;border-right:1px solid #7d8898;background:#e4e7ec;}")
        sb.AppendLine(".wh-timehead{flex:0 0 var(--wh-timeline);min-width:var(--wh-timeline);width:var(--wh-timeline);position:relative;padding:6px 0 4px 0;box-sizing:border-box;background:linear-gradient(to bottom,#fff 0 60%,#e8ebf0 100%);border-bottom:1px solid #7d8898;}")
        sb.AppendLine(".wh-tick{position:absolute;top:2px;transform:translateX(-50%);font-size:0.8rem;white-space:nowrap;color:#1a1a1a;font-weight:600;}")
        sb.AppendLine(".wh-tick.minor{color:#4a5560;font-size:0.72rem;font-weight:400;}")
        sb.AppendLine(".wh-dayrow{display:flex;flex-direction:row;align-items:stretch;min-height:40px;box-sizing:border-box;border-top:1px solid #5c6570;}")
        sb.AppendLine(".wh-day-odd .wh-dlbl{background:#f0f2f5;}.wh-day-even .wh-dlbl{background:#e4e7ec;}")
        sb.AppendLine(".wh-dlbl{display:flex;align-items:center;justify-content:center;text-align:center;padding:6px 8px;border-right:1px solid #7d8898;box-sizing:border-box;font-size:0.8rem;font-weight:600;flex:0 0 var(--wh-dayw);min-width:var(--wh-dayw);max-width:var(--wh-dayw);color:#1e2933;}")
        sb.AppendLine(".wh-dlbl.today{background:#a8c8f0;color:#02162e;border-right:1px solid #3d5a80;}")
        sb.AppendLine(".wh-strip{position:relative;flex:0 0 var(--wh-timeline);min-width:var(--wh-timeline);width:var(--wh-timeline);box-sizing:border-box;overflow:visible;}")
        sb.AppendLine(".wh-strip-inner{position:relative;box-sizing:border-box;border-left:1px solid #5c6570;border-right:1px solid #5c6570;min-height:24px;}")
        sb.AppendLine(".wh-ap{position:absolute;z-index:2;box-sizing:border-box;overflow:hidden;border:1px solid #5c6470;border-left:4px solid #2c5282;border-radius:2px;padding:3px 5px;font-size:0.78rem;line-height:1.15;min-height:var(--wh-chip-min,70px);max-height:124px;}")
        sb.AppendLine(".wh-ap .time{line-height:1.1;margin-bottom:2px;}.wh-ap .line{margin-top:1px;font-size:0.76rem;line-height:1.2;}.wh-ap .st{font-size:0.7rem;}")
        sb.AppendLine("</style>")
        sb.AppendLine("</head>")
        sb.AppendLine("<body>")
        sb.AppendLine($"<h1>{title}</h1>")
        If ctx.GeneratedUtc > DateTime.MinValue Then
            Dim genLine = If(Eng, "Generated: ", "تاريخ الإنشاء: ") &
                ctx.GeneratedUtc.ToLocalTime().ToString("g", DateTimeFormatInfo.CurrentInfo)
            sb.AppendLine($"<div class=""sub"">{WebUtility.HtmlEncode(genLine)}</div>")
        End If
        If ctx.WeekHorizontal IsNot Nothing Then
            AppendWeekHorizontal(sb, ctx.WeekHorizontal)
        ElseIf ctx.DayTimeline IsNot Nothing Then
            AppendDayTimeline(sb, ctx.DayTimeline)
        Else
            Dim gridClass = If(ctx.UseVerticalLayout, "grid grid-v", "grid")
            sb.AppendLine($"<div class=""{gridClass}"">")
            If ctx.Columns Is Nothing OrElse ctx.Columns.Count = 0 Then
                Dim noCols = If(Eng, "No columns to export.", "لا أعمدة للتصدير.")
                sb.AppendLine($"<p>{WebUtility.HtmlEncode(noCols)}</p>")
            Else
                For Each col In ctx.Columns
                    AppendColumn(sb, col)
                Next
            End If
            sb.AppendLine("</div>")
        End If
        sb.AppendLine("</body></html>")
        Return sb.ToString()
    End Function

    Private Shared Sub AppendColumn(sb As StringBuilder, col As WeekSnapshotHtmlColumn)
        If col Is Nothing Then Return
        Dim hdBg = Rgba(col.HeaderBackColor)
        Dim tFg = Rgba(col.HeaderTitleForeColor)
        Dim dFg = Rgba(col.DateLineForeColor)
        sb.AppendLine("<div class=""day"">")
        sb.AppendLine($"<div class=""day-hd"" style=""background-color:{hdBg};"">")
        Dim dayTitleEnc = WebUtility.HtmlEncode(If(col.DayTitle, String.Empty))
        Dim dateLineEnc = WebUtility.HtmlEncode(If(col.DateLine, String.Empty))
        sb.AppendLine($"<div class=""t"" style=""color:{tFg};"">{dayTitleEnc}</div>")
        sb.AppendLine($"<div class=""d"" style=""color:{dFg};"">{dateLineEnc}</div>")
        sb.AppendLine("</div>")
        sb.AppendLine("<div class=""day-body"">")
        If col.Appointments Is Nothing OrElse col.Appointments.Count = 0 Then
            Dim emptyMsg = If(Eng, "No appointments", "لا مواعيد")
            sb.AppendLine($"<div class=""empty"">{WebUtility.HtmlEncode(emptyMsg)}</div>")
        Else
            For Each ap In col.Appointments
                AppendAppt(sb, ap)
            Next
        End If
        sb.AppendLine("</div></div>")
    End Sub

    Private Shared Sub AppendAppt(sb As StringBuilder, ap As WeekSnapshotHtmlAppt)
        If ap Is Nothing Then Return
        Dim back = Rgba(ap.CardBackColor)
        Dim border = Rgba(ap.CardBorderColor)
        Dim accent = Rgba(ap.AccentColor)
        sb.AppendLine($"<div class=""appt"" style=""background-color:{back};border:1px solid {border};border-left:5px solid {accent};"">")
        AppendApptCardInner(sb, ap)
        sb.AppendLine("</div>")
    End Sub

    ''' <summary>Inner fields only (for column cards and for day-timeline absolute cards).</summary>
    Private Shared Sub AppendApptCardInner(sb As StringBuilder, ap As WeekSnapshotHtmlAppt)
        If ap Is Nothing Then Return
        If Not String.IsNullOrWhiteSpace(ap.TimeRange) Then
            sb.Append($"<div class=""time"">")
            If HasTwoTimeParts(ap.TimeRange) Then
                sb.Append($"<span style=""color:{Rgba(ap.TimeStartFore)};"">" &
                    $"{WebUtility.HtmlEncode(SplitTimePart(ap.TimeRange, True))}</span>" &
                    "<span> – </span>" &
                    $"<span style=""color:{Rgba(ap.TimeEndFore)};"">" &
                    $"{WebUtility.HtmlEncode(SplitTimePart(ap.TimeRange, False))}</span>")
            Else
                sb.Append($"<span style=""color:{Rgba(ap.TimeStartFore)};"">" &
                    $"{WebUtility.HtmlEncode(ap.TimeRange.Trim())}</span>")
            End If
            sb.AppendLine("</div>")
        End If
        If Not String.IsNullOrWhiteSpace(ap.Patient) Then
            Dim pe = WebUtility.HtmlEncode(ap.Patient)
            sb.AppendLine($"<div class=""line"" style=""color:{Rgba(ap.PatientFore)};"">{pe}</div>")
        End If
        If Not String.IsNullOrWhiteSpace(ap.Doctor) Then
            Dim de = WebUtility.HtmlEncode(ap.Doctor)
            sb.AppendLine($"<div class=""line"" style=""color:{Rgba(ap.DoctorFore)};"">{de}</div>")
        End If
        If Not String.IsNullOrWhiteSpace(ap.Reason) Then
            Dim re = WebUtility.HtmlEncode(ap.Reason)
            sb.AppendLine($"<div class=""line"" style=""color:{Rgba(ap.ReasonFore)};"">{re}</div>")
        End If
        If Not String.IsNullOrWhiteSpace(ap.Notes) Then
            Dim ne = WebUtility.HtmlEncode(ap.Notes)
            sb.AppendLine($"<div class=""line"" style=""color:{Rgba(ap.NotesFore)};"">{ne}</div>")
        End If
        If Not String.IsNullOrWhiteSpace(ap.StatusText) Then
            sb.AppendLine($"<span class=""st"" style=""color:{Rgba(ap.StatusFore)};background-color:{Rgba(ap.StatusBack)};"">" &
                        $"{WebUtility.HtmlEncode(ap.StatusText)}</span>")
        End If
    End Sub

    Private Shared Sub AppendWeekHorizontal(sb As StringBuilder, wh As WeekSnapshotHtmlWeekHorizontal)
        If wh Is Nothing Then Return
        Dim dlab = Math.Max(100, wh.DayLabelWidthCssPx)
        Dim timew = If(wh.TimelineBodyMinWidthCssPx > 0, wh.TimelineBodyMinWidthCssPx, 2000)
        Dim wmin = dlab + timew + 48
        Dim inv = CultureInfo.InvariantCulture
        Dim totalM = Math.Max(1, wh.TotalTimeMinutes)
        Dim p30 = wh.TimeGridStripePeriodPercent
        If p30 <= 0R Then p30 = 30.0R * 100.0R / CDbl(totalM)
        Dim p30Str = p30.ToString("0.####", inv)
        Dim p60 = 60.0R * 100.0R / CDbl(totalM)
        Dim p60Str = p60.ToString("0.####", inv)
        ' 30-min vertical grid (lighter) + 60-min emphasis (darker) — same width as time header for alignment.
        Dim gridBg = "background-color:#e4e6ea;" &
            "background-image:" &
            $"repeating-linear-gradient(90deg, #e9ebef 0, #e9ebef calc({p30Str}% - 1.1px), #98a0ab calc({p30Str}% - 1.1px), #98a0ab {p30Str}%)," &
            $"repeating-linear-gradient(90deg, rgba(0,0,0,0) 0, rgba(0,0,0,0) calc({p60Str}% - 1.5px), #5a616d calc({p60Str}% - 1.5px), #5a616d {p60Str}%);"
        sb.AppendLine("<div class=""wh-page"">")
        sb.AppendLine(
            $"<div class=""wh-outer"" style=""--wh-dayw:{dlab}px;--wh-timew:{timew}px;--wh-min:{wmin}px;--wh-chip-min:72px;"">")
        sb.AppendLine($"<div class=""wh-banner"">{WebUtility.HtmlEncode(If(wh.BannerTitle, String.Empty))}</div>")

        sb.AppendLine("<div class=""wh-hrow"">")
        sb.AppendLine("<div class=""wh-corner""></div>")
        sb.AppendLine($"<div class=""wh-timehead"" style=""min-height:40px;position:relative;{gridBg}"">")
        If wh.HourTickLabels IsNot Nothing Then
            For Each tk In wh.HourTickLabels
                If tk Is Nothing OrElse String.IsNullOrEmpty(tk.Text) Then Continue For
                Dim lp = Math.Max(0R, Math.Min(100R, tk.LeftPercent))
                Dim cls = If(tk.IsMajor, "wh-tick", "wh-tick minor")
                sb.AppendLine($"<div class=""{cls}"" style=""left:{lp.ToString("0.##", inv)}%;"">{WebUtility.HtmlEncode(tk.Text)}</div>")
            Next
        End If
        sb.AppendLine("</div></div>")

        If wh.DayRows IsNot Nothing Then
            Dim dayIx As Integer = 0
            For Each dr In wh.DayRows
                If dr Is Nothing Then Continue For
                dayIx += 1
                Dim rowAlt = If(dayIx Mod 2 = 0, " wh-day-even", " wh-day-odd")
                sb.AppendLine($"<div class=""wh-dayrow{rowAlt}"">")
                Dim dcls = "wh-dlbl" & If(dr.IsToday, " today", "")
                sb.AppendLine($"<div class=""{dcls}"">{WebUtility.HtmlEncode(If(dr.DayLabel, String.Empty))}</div>")
                sb.AppendLine("<div class=""wh-strip"">")
                sb.AppendLine(
                    $"<div class=""wh-strip-inner"" style=""{gridBg}min-height:{CStr(Math.Max(32, dr.RowBodyHeightCssPx))}px;position:relative;"">")
                If dr.Placed IsNot Nothing Then
                    For Each pa In dr.Placed
                        If pa Is Nothing OrElse pa.Html Is Nothing Then Continue For
                        Dim h = pa.Html
                        Dim back = Rgba(h.CardBackColor)
                        Dim border = Rgba(h.CardBorderColor)
                        Dim accent = Rgba(h.AccentColor)
                        Dim lp2 = Math.Max(0R, Math.Min(100R, pa.LeftPercent))
                        Dim wp2 = Math.Max(0.2R, Math.Min(100R - lp2, pa.WidthPercent))
                        Dim mnh = Math.Max(56, pa.MinHeightCssPx)
                        Dim tp = Math.Max(0, pa.TopPx)
                        Dim zi = Math.Max(1, pa.StackingZ)
                        sb.AppendLine(
                            $"<div class=""wh-ap"" style=""left:{lp2.ToString("0.##", inv)}%;width:{wp2.ToString("0.##", inv)}%;top:{CStr(tp)}px;min-height:{CStr(mnh)}px;z-index:{CStr(zi)};background-color:{back};border:1px solid {border};border-left:4px solid {accent};"">")
                        AppendApptCardInner(sb, h)
                        sb.AppendLine("</div>")
                    Next
                End If
                sb.AppendLine("</div></div></div>")
            Next
        End If
        sb.AppendLine("</div></div>")
    End Sub

    Private Shared Sub AppendDayTimeline(sb As StringBuilder, tl As WeekSnapshotHtmlDayTimeline)
        If tl Is Nothing Then Return
        sb.AppendLine("<div class=""dv-outer"">")
        sb.AppendLine($"<div class=""dv-banner"">{WebUtility.HtmlEncode(If(tl.BannerTitle, String.Empty))}</div>")
        sb.AppendLine("<div class=""dv-colshead"">")
        sb.AppendLine("<div class=""dv-corner""></div>")
        If tl.DoctorColumns Is Nothing OrElse tl.DoctorColumns.Count = 0 Then
            sb.AppendLine("<div class=""dv-dochead"">—</div>")
        Else
            For Each dc In tl.DoctorColumns
                Dim nm = If(dc Is Nothing, "", If(dc.DoctorName, ""))
                sb.AppendLine($"<div class=""dv-dochead"">{WebUtility.HtmlEncode(nm)}</div>")
            Next
        End If
        sb.AppendLine("</div>")

        Dim sh = Math.Max(24, tl.SlotHeightCssPx)
        Dim nSlots = Math.Max(1, tl.TimeSlotCount)
        Dim hPx = nSlots * sh
        Dim h1 = Math.Max(0, sh - 1)
        Dim grad = $"repeating-linear-gradient(to bottom, transparent 0, transparent {h1}px, #e0e0e0 {h1}px, #e0e0e0 {sh}px)"

        sb.AppendLine("<div class=""dv-row2"">")
        sb.AppendLine("<div class=""dv-time"">")
        For i = 0 To nSlots - 1
            Dim t = tl.GridStart.AddMinutes(30 * i)
            Dim lab = If(tl.Use24HourFormat, t.ToString("HH:mm"), t.ToString("hh:mm tt", DateTimeFormatInfo.CurrentInfo))
            sb.AppendLine($"<div class=""dv-time-slot"" style=""height:{sh}px;"">{WebUtility.HtmlEncode(lab)}</div>")
        Next
        sb.AppendLine("</div>")

        sb.AppendLine("<div class=""dv-docs"">")
        If tl.DoctorColumns Is Nothing OrElse tl.DoctorColumns.Count = 0 Then
            sb.AppendLine($"<div class=""dv-doc""><div class=""dv-dbody"" style=""height:{hPx}px;position:relative;background:{grad};"">")
            Dim em = If(Eng, "No appointments", "لا مواعيد")
            sb.AppendLine($"<div style=""position:absolute;left:0;right:0;top:8px;padding:0 6px;font-size:0.85rem;color:#888;"">{WebUtility.HtmlEncode(em)}</div>")
            sb.AppendLine("</div></div>")
        Else
            For Each dc In tl.DoctorColumns
                sb.AppendLine("<div class=""dv-doc"">")
                sb.AppendLine($"<div class=""dv-dbody"" style=""height:{hPx}px;position:relative;background:{grad};"">")
                If dc.Placed IsNot Nothing Then
                    For Each pa In dc.Placed
                        If pa Is Nothing OrElse pa.Html Is Nothing Then Continue For
                        Dim ap = pa.Html
                        Dim back = Rgba(ap.CardBackColor)
                        Dim border = Rgba(ap.CardBorderColor)
                        Dim accent = Rgba(ap.AccentColor)
                        Dim top = Math.Max(0R, Math.Min(100R, pa.TopPercent))
                        Dim hgt = Math.Max(0.15R, Math.Min(100R, pa.HeightPercent))
                        Dim mg = If(pa.LaneNudgePx <> 0, $"margin-left:{pa.LaneNudgePx}px;", "")
                        Dim z = Math.Max(1, pa.StackingZ)
                        sb.AppendLine($"<div class=""appt appt-tl"" style=""{mg}position:absolute;left:2px;right:2px;background-color:{back};border:1px solid {border};border-left:4px solid {accent};top:{top.ToString("0.##", CultureInfo.InvariantCulture)}%;height:{hgt.ToString("0.##", CultureInfo.InvariantCulture)}%;z-index:{z};"">")
                        AppendApptCardInner(sb, ap)
                        sb.AppendLine("</div>")
                    Next
                End If
                sb.AppendLine("</div></div>")
            Next
        End If
        sb.AppendLine("</div>")
        sb.AppendLine("</div></div>")
    End Sub

    Private Shared Function HasTwoTimeParts(timeRange As String) As Boolean
        If String.IsNullOrEmpty(timeRange) Then Return False
        Dim t = timeRange.Trim()
        Dim i = t.IndexOf(" – ", StringComparison.Ordinal)
        Dim sepLen = 3
        If i < 0 Then
            i = t.IndexOf(" - ", StringComparison.Ordinal)
            If i < 0 Then Return False
            sepLen = 3
        End If
        If i <= 0 OrElse i + sepLen >= t.Length Then Return False
        Return Not String.IsNullOrWhiteSpace(t.Substring(0, i)) AndAlso Not String.IsNullOrWhiteSpace(t.Substring(i + sepLen))
    End Function

    Private Shared Function SplitTimePart(timeRange As String, first As Boolean) As String
        If String.IsNullOrEmpty(timeRange) Then Return ""
        Dim sep = " – "
        Dim i = timeRange.IndexOf(sep, StringComparison.Ordinal)
        If i < 0 Then
            sep = " - "
            i = timeRange.IndexOf(sep, StringComparison.Ordinal)
        End If
        If i < 0 Then
            i = timeRange.IndexOf("-"c)
            sep = "-"
        End If
        If i < 0 Then Return timeRange
        If first Then Return timeRange.Substring(0, i).Trim()
        Return timeRange.Substring(i + sep.Length).Trim()
    End Function

    Public Shared Function Rgba(c As Color) As String
        If c.A = 0 OrElse c = Color.Transparent Then Return "transparent"
        If c.A = 255 Then Return String.Format(CultureInfo.InvariantCulture, "rgb({0},{1},{2})", c.R, c.G, c.B)
        Dim a = (CInt(c.A) / 255.0R).ToString("0.###", CultureInfo.InvariantCulture)
        Return String.Format(CultureInfo.InvariantCulture, "rgba({0},{1},{2},{3})", c.R, c.G, c.B, a)
    End Function
End Class

#If DEBUG Then
''' <summary>Validates HTML escaping and color rendering; only referenced from the snapshot preview in DEBUG with a debugger attached.</summary>
Friend NotInheritable Class WeekSnapshotHtmlExportSelfTest
    Public Shared Function RunSanityCheck() As Boolean
        Try
            Dim ap As New WeekSnapshotHtmlAppt With {
                .TimeRange = "10:00 – 11:00",
                .Patient = "<b>Mixed</b> & name",
                .Doctor = "Dr. A",
                .Reason = "Check",
                .Notes = "",
                .StatusText = "Planned",
                .CardBackColor = Color.FromArgb(240, 248, 255),
                .CardBorderColor = Color.FromArgb(200, 90, 90),
                .AccentColor = Color.SteelBlue,
                .TimeStartFore = Color.Red,
                .TimeEndFore = Color.DarkBlue,
                .PatientFore = Color.FromArgb(33, 37, 41),
                .DoctorFore = Color.FromArgb(40, 48, 58),
                .ReasonFore = Color.Teal,
                .NotesFore = Color.Purple,
                .StatusBack = Color.FromArgb(103, 114, 229),
                .StatusFore = Color.White
            }
            Dim col As New WeekSnapshotHtmlColumn()
            col.DayTitle = " Saturday "
            col.DateLine = "01 Jan 2026.(1 Appt)"
            col.HeaderBackColor = Color.FromArgb(255, 230, 230)
            col.HeaderTitleForeColor = Color.FromArgb(36, 64, 120)
            col.DateLineForeColor = Color.FromArgb(75, 84, 99)
            col.Appointments.Add(ap)
            Dim ctx As New WeekSnapshotHtmlContext() With {
                .Caption = "Test week",
                .GeneratedUtc = DateTime.UtcNow
            }
            ctx.Columns.Add(col)
            Dim html = WeekSnapshotHtmlWriter.BuildDocument(ctx)
            If String.IsNullOrEmpty(html) OrElse html.IndexOf("<!DOCTYPE", StringComparison.OrdinalIgnoreCase) < 0 Then
                Return False
            End If
            ' Escaped, not raw HTML
            If html.Contains("<b>Mixed</b>") OrElse Not html.Contains("&lt;b&gt;") Then
                Return False
            End If
            ' Color output
            If Not (html.Contains("rgb(") OrElse html.Contains("rgba(")) Then
                Return False
            End If
            ' Sample file for manual browser check (self-test data only)
            Try
                Dim p = Path.Combine(Path.GetTempPath(), "DentistX_WeekSnapshotHtmlSelfTest.html")
                File.WriteAllText(p, html, Encoding.UTF8)
                Debug.WriteLine("WeekSnapshotHtmlExportSelfTest wrote: " & p)
            Catch
            End Try
            Return True
        Catch
            Return False
        End Try
    End Function
End Class
#End If
