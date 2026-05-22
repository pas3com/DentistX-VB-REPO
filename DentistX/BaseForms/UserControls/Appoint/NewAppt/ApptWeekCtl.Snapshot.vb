Imports System
Imports System.Collections.Generic
Imports System.Drawing
Imports System.Globalization
Imports System.Linq
Imports System.Windows.Forms
Imports DevExpress.XtraEditors

' Week snapshot export for SchedulerNew / ApptHostCtl (bitmap stitch + HTML context).
' Routed from ApptSnapshotHtmlBuilder.BuildForApptModule and ApptHostCtl.TryCaptureWeekSnapshotBitmap.
Partial Public Class ApptWeekCtl

    Private Structure WeekSnapshotColState
        Public ShellH As Integer
        Public BarVisible As Boolean
        Public BarValue As Integer
        Public CardsH As Integer
        Public CardsTop As Integer
    End Structure

    ''' <summary>Card width for snapshot when the slim vertical bar is hidden (match <see cref="AdjustDayScrollExtent"/> full-width path).</summary>
    Private Function WeekSnapshotInnerFullWidthForCards(scrollHost As XtraScrollableControl) As Integer
        If scrollHost Is Nothing Then Return 1
        Dim cw = Math.Max(1, scrollHost.ClientSize.Width)
        If _compactSixDayWeek Then
            Return cw
        End If
        Return Math.Max(180, cw - 16)
    End Function

    Private Shared Function FindTopDockedHeaderControl(shell As Control, scroll As Control) As Control
        If shell Is Nothing OrElse scroll Is Nothing Then Return Nothing
        For Each c As Control In shell.Controls
            If c IsNot scroll AndAlso c.Dock = DockStyle.Top Then
                Return c
            End If
        Next
        Return Nothing
    End Function

    ''' <summary>Full unclipped week image for <see cref="SchedulerSnapshotShared"/>: each day column is expanded and composited to match classic scheduler snapshot behavior.</summary>
    ''' <remarks>
    ''' Do not use <see cref="Control.DrawToBitmap"/> on the day shell: <see cref="XtraScrollableControl"/> only paints the viewport, so a tall bitmap would be mostly empty below
    ''' the on-screen area. We composite the day header and cards layer directly, like the expanded in-flow week capture in the classic scheduler.
    ''' </remarks>
    Friend Function CaptureStitchedWeekSnapshotBitmap() As Bitmap
        If _dayColumns.Count = 0 Then Return Nothing
        For Each col In _dayColumns
            If col.Scroll Is Nothing OrElse col.Bar Is Nothing OrElse col.Cards Is Nothing Then Return Nothing
        Next

        Dim snaps As New List(Of WeekSnapshotColState)()
        Try
            For Each col In _dayColumns
                Dim s As WeekSnapshotColState
                s.ShellH = col.Shell.Height
                s.BarVisible = col.Bar.Visible
                s.BarValue = col.Bar.Value
                s.CardsH = col.Cards.Height
                s.CardsTop = col.Cards.Top
                snaps.Add(s)
                col.Bar.Visible = False
                col.Bar.Value = 0
            Next
            ' Apply Bar.Visible = False before we read scroll ClientSize and inner full width.
            If _layout IsNot Nothing Then _layout.PerformLayout()
            If IsHandleCreated Then Application.DoEvents()

            ' Expand cards to full (no slim bar) width, restack, then use laid-out height.
            For Each col In _dayColumns
                Dim innerFull = WeekSnapshotInnerFullWidthForCards(col.Scroll)
                col.Cards.SuspendLayout()
                col.Cards.Width = innerFull
                col.Cards.Top = 0
                col.Cards.ResumeLayout(True)
                LayoutDayCards(col.Cards)
            Next

            Dim finalMax = 0
            For Each col In _dayColumns
                Dim headerP = FindTopDockedHeaderControl(col.Shell, col.Scroll)
                Dim hdrH = If(headerP Is Nothing, 0, headerP.Height)
                finalMax = Math.Max(finalMax, hdrH + col.Cards.Height + 4)
            Next
            If finalMax < 20 Then Return Nothing

            For Each col In _dayColumns
                col.Shell.Height = finalMax
            Next

            _workArea.SuspendLayout()
            _weekRoot.SuspendLayout()
            _layout.SuspendLayout()
            _layout.ResumeLayout(True)
            _weekRoot.ResumeLayout(True)
            _workArea.ResumeLayout(True)
            _layout.PerformLayout()
            _weekRoot.PerformLayout()
            _workArea.PerformLayout()
            Application.DoEvents()

            Dim totalW = 1
            For Each col In _dayColumns
                totalW = Math.Max(totalW, col.Shell.Right + 8)
            Next
            Dim bg = _layout.BackColor
            Dim out As New Bitmap(Math.Max(1, totalW), finalMax)
            Using g As Graphics = Graphics.FromImage(out)
                g.Clear(bg)
                For Each col In _dayColumns.OrderBy(Function(x) x.Shell.Left)
                    If col.Shell.Width < 1 OrElse finalMax < 1 Then Continue For
                    If col.Cards Is Nothing OrElse col.Scroll Is Nothing Then Continue For

                    Dim headerP = FindTopDockedHeaderControl(col.Shell, col.Scroll)
                    Dim hdrH = If(headerP Is Nothing, 0, headerP.Height)
                    Dim colW = col.Shell.Width
                    Using colBmp As New Bitmap(colW, finalMax)
                        Using gCol As Graphics = Graphics.FromImage(colBmp)
                            gCol.Clear(bg)
                            If headerP IsNot Nothing AndAlso headerP.Width > 0 AndAlso headerP.Height > 0 Then
                                Using hb As New Bitmap(Math.Max(1, headerP.Width), Math.Max(1, headerP.Height))
                                    headerP.DrawToBitmap(hb, New Rectangle(0, 0, hb.Width, hb.Height))
                                    gCol.DrawImageUnscaled(hb, 0, 0)
                                End Using
                            End If
                            Dim cw = Math.Max(1, col.Cards.Width)
                            Dim ch = Math.Max(1, col.Cards.Height)
                            Using cb As New Bitmap(cw, ch)
                                col.Cards.DrawToBitmap(cb, New Rectangle(0, 0, cw, ch))
                                Dim drawY = hdrH + col.Cards.Top
                                gCol.DrawImageUnscaled(cb, col.Cards.Left, drawY)
                            End Using
                        End Using
                        g.DrawImageUnscaled(colBmp, col.Shell.Left, 0)
                    End Using
                Next
            End Using
            Return out
        Finally
            For i = 0 To _dayColumns.Count - 1
                If i >= snaps.Count Then Exit For
                Dim col = _dayColumns(i)
                Dim s = snaps(i)
                col.Shell.Height = s.ShellH
                col.Bar.Visible = s.BarVisible
                col.Bar.Value = s.BarValue
                col.Cards.Height = s.CardsH
                col.Cards.Top = s.CardsTop
            Next
            LayoutWeekColumns()
        End Try
    End Function

    ''' <summary>Builds structured data for a readable week HTML page (see <see cref="WeekSnapshotHtmlWriter"/>), matching day headers and per-card colors.</summary>
    Friend Function BuildHtmlExportContext(weekCaption As String) As WeekSnapshotHtmlContext
        If _request Is Nothing OrElse _dayColumns.Count = 0 Then Return Nothing
        Dim weekStart = GetWeekStartSaturday(_request.State.CurrentDate)
        Dim ctx As New WeekSnapshotHtmlContext() With {
            .Caption = If(weekCaption, ""),
            .GeneratedUtc = DateTime.UtcNow
        }
        For i = 0 To _dayColumns.Count - 1
            Dim col = _dayColumns(i)
            If col.Cards Is Nothing Then Continue For
            Dim dayDate As Date
            If col.Shell IsNot Nothing AndAlso col.Shell.Tag IsNot Nothing Then
                If TypeOf col.Shell.Tag Is DateTime Then
                    dayDate = CDate(CType(col.Shell.Tag, DateTime))
                ElseIf TypeOf col.Shell.Tag Is Date Then
                    dayDate = CDate(CType(col.Shell.Tag, Date))
                Else
                    dayDate = weekStart.AddDays(i).Date
                End If
            Else
                dayDate = weekStart.AddDays(i).Date
            End If

            Dim appts As New List(Of WeekSnapshotHtmlAppt)()
            For Each c In col.Cards.Controls.OfType(Of ApptCard)().Where(Function(card) card.Visible).OrderBy(Function(x) x.Top)
                Dim row = c.TryGetWeekHtmlExportRow()
                If row IsNot Nothing Then appts.Add(row)
            Next
            Dim apptCount = appts.Count
            Dim dayHeaderBg = If(dayDate = Date.Today, WeekViewTodayHeaderColor, WeekViewDayHeaderColors(i Mod WeekViewDayHeaderColors.Length))
            Dim htmlCol As New WeekSnapshotHtmlColumn With {
                .DayTitle = ApptTheme.FormatSchedulerStyleDayColumnTitle(dayDate),
                .DateLine = "· " & ApptTheme.FormatSchedulerStyleDayColumnApptCountParens(apptCount),
                .HeaderBackColor = dayHeaderBg,
                .HeaderTitleForeColor = Color.FromArgb(36, 64, 120),
                .DateLineForeColor = Color.FromArgb(75, 84, 99),
                .IsToday = (dayDate = Date.Today)
            }
            htmlCol.Appointments.AddRange(appts)
            ctx.Columns.Add(htmlCol)
        Next
        Return ctx
    End Function

End Class
