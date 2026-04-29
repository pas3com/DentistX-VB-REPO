Imports System
Imports System.Collections.Generic
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Linq
Imports System.Windows.Forms
Imports DevExpress.XtraEditors

''' <summary>
''' <see cref="ApptViewMode.DaysTimeline"/>: horizontal week timeline (Sat–Fri) with days on the Y-axis and time on the X-axis,
''' matching <see cref="SchedulerNew.RenderDaysTimelineView"/>.
''' </summary>
Public Class ApptDayLine
    Inherits XtraUserControl
    Implements IApptViewCtl

    Private Const EnhancedReadability As Boolean = True
    Private Const ChipTailNarrow As Integer = 3
    Private Const DayLabelWidth As Integer = 120
    Private Const EmptyDayLaneHeight As Integer = 40
    Private Const DayLanePadWithAppts As Integer = 0
    Private Const TlBodyInset As Integer = 2
    Private Const ChipVerticalSpacing As Integer = 0

    Private _request As ApptViewRequest
    Private _scrollHost As Panel
    Private _content As Panel
    Private _toolTip As ToolTip
    Private _lastScrollInnerW As Integer = -1

    ' --- Drag/Resize State ---
    Private _dragSourceChip As Panel
    Private _dragSourceAppt As AppointmentC
    Private ReadOnly _holdTimer As New Timer With {.Enabled = False}
    
    Private _resizingChip As Panel
    Private _resizeIsEnd As Boolean ' True if resizing End time (Right edge), False for Start (Left edge)
    Private _resizeStartTime As DateTime
    Private _resizeEndTime As DateTime
    Private _resizeStartPt As Point
    
    Private _dragTargetTime As DateTime = DateTime.MinValue
    Private _dragTargetDay As DateTime = DateTime.MinValue

    Private _draggingDuration As TimeSpan = TimeSpan.Zero
    ''' <summary>BlueViolet border only in <see cref="HoldTimer_Tick"/> before <c>DoDragDrop</c> (same as <see cref="ApptDayDoctors"/>).</summary>
    Private _holdStripBlueChip As Panel
    ''' <summary>Proposed chip bounds in chip client coords while resizing (same role as <see cref="ApptDayDoctors"/> canvas ghost during resize).</summary>
    Private _resizeProposedInChip As Rectangle = Rectangle.Empty

    Private _pixelsPerMinute As Double

    Public Sub New()
        Appearance.BackColor = Color.FromArgb(245, 247, 250)
        Appearance.Options.UseBackColor = True
        DoubleBuffered = True
        RightToLeft = RightToLeft.No

        _scrollHost = New Panel With {
            .Dock = DockStyle.Fill,
            .AutoScroll = True,
            .BackColor = Color.White,
            .Padding = New Padding(4, 4, 4, 6),
            .RightToLeft = RightToLeft.No
        }
        Controls.Add(_scrollHost)
        AddHandler _scrollHost.Resize, AddressOf ScrollHost_Resize
        AddHandler _holdTimer.Tick, AddressOf HoldTimer_Tick
    End Sub

    Private Sub ScrollHost_Resize(sender As Object, e As EventArgs)
        If _request Is Nothing Then Return
        Dim w = _scrollHost.ClientSize.Width - _scrollHost.Padding.Horizontal
        If w = _lastScrollInnerW Then Return
        RenderTimeline()
    End Sub

    Public Property InteractionHub As ApptInteractionHub Implements IApptViewCtl.InteractionHub

    Public Sub BindData(request As ApptViewRequest) Implements IApptViewCtl.BindData
        _request = request
        If _toolTip IsNot Nothing Then
            _toolTip.Dispose()
            _toolTip = Nothing
        End If
        _toolTip = New ToolTip With {.AutoPopDelay = 8000, .InitialDelay = 300}
        _lastScrollInnerW = -1
        RenderTimeline()
        If request IsNot Nothing Then TryScrollToAppointment(request.PendingScrollAppointment)
    End Sub

    ''' <summary>HTML export: Sat–Fri day rows, time on the X-axis (not week columns or day-doctors layout).</summary>
    Friend Function BuildHtmlExportContext(caption As String) As WeekSnapshotHtmlContext
        Return ApptSnapshotHtmlBuilder.BuildDaysTimelineHtmlContext(_request, caption, linkedDoctorAtEnd:=True)
    End Function

    Protected Overrides Sub Dispose(disposing As Boolean)
        If disposing AndAlso _toolTip IsNot Nothing Then
            _toolTip.Dispose()
            _toolTip = Nothing
        End If
        MyBase.Dispose(disposing)
    End Sub

    Private Sub OnChipDragMouseDown(chip As Panel, day As Date, e As MouseEventArgs)
        If e.Button <> MouseButtons.Left Then Return
        Dim ap = TryCast(chip.Tag, AppointmentC)
        If ap Is Nothing OrElse ap.AppointmentID <= 0 Then Return

        Dim edge = GetChipEdgeAt(chip, e.Location)
        If edge <> ApptCardCtl.CardEdge.None Then
            _resizingChip = chip
            _resizeIsEnd = (edge = ApptCardCtl.CardEdge.Bottom) ' Using Bottom as Right for horizontal
            _resizeStartPt = chip.PointToScreen(e.Location)
            _resizeStartTime = ap.StartDateTime
            _resizeEndTime = ap.EndDateTime
            _resizeProposedInChip = New Rectangle(0, 0, Math.Max(1, chip.ClientSize.Width - 1), Math.Max(1, chip.ClientSize.Height - 1))
            _resizingChip.Invalidate()
            _resizingChip.Update()
            Return
        End If

        _holdTimer.Stop()
        _dragSourceChip = chip
        _dragSourceAppt = ap
        _draggingDuration = ap.EndDateTime - ap.StartDateTime
        _holdTimer.Interval = Math.Max(200, If(_request IsNot Nothing, _request.DragHoldTimeMs, 750))
        _holdTimer.Start()
    End Sub

    Private Function GetChipEdgeAt(chip As Panel, pt As Point) As ApptCardCtl.CardEdge
        Const edgeW = 8
        If pt.X < edgeW Then Return ApptCardCtl.CardEdge.Top ' Using Top as Left
        If pt.X > chip.Width - edgeW Then Return ApptCardCtl.CardEdge.Bottom ' Using Bottom as Right
        Return ApptCardCtl.CardEdge.None
    End Function

    Private Sub OnChipDragMouseMove(chip As Panel, e As MouseEventArgs)
        If _resizingChip Is chip Then
            PerformResize(chip, e)
            chip.Cursor = Cursors.SizeWE
            Return
        End If

        Dim edge = GetChipEdgeAt(chip, e.Location)
        chip.Cursor = If(edge <> ApptCardCtl.CardEdge.None, Cursors.SizeWE, Cursors.Hand)
    End Sub

    Private Sub PerformResize(chip As Panel, e As MouseEventArgs)
        Dim currentPt = chip.PointToScreen(e.Location)
        Dim diffX = currentPt.X - _resizeStartPt.X
        
        ' Snapped diff for logic
        Dim diffMinsSnapped = (CInt(diffX / _pixelsPerMinute) \ 5) * 5

        Dim newStart = _resizeStartTime
        Dim newEnd = _resizeEndTime

        If Not _resizeIsEnd Then ' Left edge
            newStart = _resizeStartTime.AddMinutes(diffMinsSnapped)
            If newStart >= newEnd.AddMinutes(-5) Then newStart = newEnd.AddMinutes(-5)
        Else ' Right edge
            newEnd = _resizeEndTime.AddMinutes(diffMinsSnapped)
            If newEnd <= newStart.AddMinutes(5) Then newEnd = newStart.AddMinutes(5)
        End If

        ' Ghost visual - smooth motion
        Dim visualDiffX = CSng(diffX)
        Dim xGhost As Single
        Dim wGhost As Single

        If Not _resizeIsEnd Then
            xGhost = CSng(chip.Left) + visualDiffX
            wGhost = CSng(chip.Width) - visualDiffX
        Else
            xGhost = CSng(chip.Left)
            wGhost = CSng(chip.Width) + visualDiffX
        End If

        If wGhost < 10 Then wGhost = 10

        ' Proposed border in chip client coords (same as ApptDayDoctors' ghost in canvas: preview of new time span).
        _resizeProposedInChip = New Rectangle(CInt(xGhost) - chip.Left, 0, CInt(wGhost), chip.Height)
        chip.Invalidate()
    End Sub

    Private Sub OnChipDragMouseUp(chip As Panel, e As MouseEventArgs)
        _holdTimer.Stop()
        If _resizingChip Is chip Then
            CommitResize(chip, e)
            _resizingChip = Nothing
            _resizeProposedInChip = Rectangle.Empty
            chip.Invalidate()
            Return
        End If
        _dragSourceChip = Nothing
        _dragSourceAppt = Nothing
        _resizeProposedInChip = Rectangle.Empty
        _content.Invalidate()
    End Sub

    Private Sub CommitResize(chip As Panel, e As MouseEventArgs)
        Dim currentPt = chip.PointToScreen(e.Location)
        Dim diffX = currentPt.X - _resizeStartPt.X
        Dim diffMins = (CInt(diffX / _pixelsPerMinute) \ 5) * 5

        Dim newStart = _resizeStartTime
        Dim newEnd = _resizeEndTime

        If Not _resizeIsEnd Then
            newStart = _resizeStartTime.AddMinutes(diffMins)
            If newStart >= newEnd.AddMinutes(-5) Then newStart = newEnd.AddMinutes(-5)
        Else
            newEnd = _resizeEndTime.AddMinutes(diffMins)
            If newEnd <= newStart.AddMinutes(5) Then newEnd = newStart.AddMinutes(5)
        End If

        If newStart <> _resizeStartTime OrElse newEnd <> _resizeEndTime Then
            Dim ap = TryCast(chip.Tag, AppointmentC)
            If ap IsNot Nothing AndAlso InteractionHub IsNot Nothing Then
                InteractionHub.PublishAppointmentTimeChange(ap, newStart, newEnd)
            End If
        End If
    End Sub

    Private Sub HoldTimer_Tick(sender As Object, e As EventArgs)
        _holdTimer.Stop()
        If _dragSourceChip Is Nothing OrElse _dragSourceAppt Is Nothing Then Return
        Try
            _holdStripBlueChip = _dragSourceChip
            _holdStripBlueChip.Invalidate()
            _holdStripBlueChip.Update()

            Dim dObj As New DataObject()
            dObj.SetData("Appointment", _dragSourceAppt)
            dObj.SetData("SourceDay", _dragSourceAppt.StartDateTime.Date)
            dObj.SetData("SourceDoctor", _dragSourceAppt.DrID)
            _dragSourceChip.DoDragDrop(dObj, DragDropEffects.Move)
        Finally
            _holdStripBlueChip = Nothing
            _dragSourceChip = Nothing
            _dragSourceAppt = Nothing
            _dragTargetTime = DateTime.MinValue
            _dragTargetDay = DateTime.MinValue
            _content.Invalidate()
            InvalidateAllRows()
        End Try
    End Sub

    Private Sub Row_DragEnter(e As DragEventArgs)
        If e.Data.GetDataPresent("Appointment") Then
            e.Effect = DragDropEffects.Move
        Else
            e.Effect = DragDropEffects.None
        End If
    End Sub

    Private Sub Row_DragOver(rowPanel As Panel, e As DragEventArgs)
        If Not e.Data.GetDataPresent("Appointment") Then Return
        e.Effect = DragDropEffects.Move

        Dim pt = rowPanel.PointToClient(New Point(e.X, e.Y))
        
        Dim day = DirectCast(rowPanel.Tag, DateTime)
        Dim state = _request.State
        Dim startHour = Math.Max(0, Math.Min(23, CInt(Math.Floor(state.WorkStartTime.TotalHours))))

        ' Smooth visual target calculation
        Dim clickMinutes = CSng(pt.X / _pixelsPerMinute)
        ' Logic snaps to 5 mins
        Dim snapMin = (CInt(clickMinutes) \ 5) * 5
        
        _dragTargetTime = day.Date.AddHours(startHour).AddMinutes(snapMin)
        _dragTargetDay = day.Date
    End Sub

    Private Sub Row_DragLeave()
        _dragTargetTime = DateTime.MinValue
        _dragTargetDay = DateTime.MinValue
        _content.Invalidate()
    End Sub

    Private Sub Row_DragDrop(e As DragEventArgs)
        Dim appt = TryCast(e.Data.GetData("Appointment"), AppointmentC)
        If appt Is Nothing OrElse _dragTargetTime = DateTime.MinValue Then Return

        Dim duration = appt.EndDateTime - appt.StartDateTime
        Dim newStart = _dragTargetTime
        Dim newEnd = newStart.Add(duration)

        If InteractionHub IsNot Nothing Then
            InteractionHub.PublishAppointmentTimeChange(appt, newStart, newEnd)
        End If

        _dragTargetTime = DateTime.MinValue
        _dragTargetDay = DateTime.MinValue
        _content.Invalidate()
    End Sub

    Private Sub InvalidateAllRows()
        If _content Is Nothing Then Return
        For Each c As Control In _content.Controls
            If TypeOf c Is Panel AndAlso c.Tag IsNot Nothing AndAlso TypeOf c.Tag Is DateTime Then
                c.Invalidate()
            End If
        Next
    End Sub

    Private Sub RenderTimeline()
        _scrollHost.SuspendLayout()
        Dim headerFont As Font = Nothing
        Dim apptFont As Font = Nothing
        Dim lblDrBoldFont As Font = Nothing
        Try
            _scrollHost.Controls.Clear()
            If _request Is Nothing OrElse _request.State Is Nothing OrElse _request.Data Is Nothing Then Return

            Dim state = _request.State
            Dim data = _request.Data
            Dim startHour = Math.Max(0, Math.Min(23, CInt(Math.Floor(state.WorkStartTime.TotalHours))))
            Dim endHour = Math.Min(24, Math.Max(startHour + 1, CInt(Math.Ceiling(state.WorkEndTime.TotalHours))))
            Dim totalTimeMinutes = (endHour - startHour) * 60
            If totalTimeMinutes <= 0 Then Return

            Dim weekStart = GetWeekStartSaturday(state.CurrentDate)
            Dim innerW = Math.Max(200, _scrollHost.ClientSize.Width - _scrollHost.Padding.Horizontal)
            _lastScrollInnerW = innerW
            Dim timelineWidth = Math.Max(120, innerW - DayLabelWidth - 2)
            _pixelsPerMinute = timelineWidth / CDbl(totalTimeMinutes)

            headerFont = New Font("Calibri", 12.0F, FontStyle.Bold)
            Dim timeHeaderHeight As Integer
            
            _content = New Panel With {
                .Location = Point.Empty,
                .AutoSize = False,
                .BackColor = Color.White,
                .RightToLeft = RightToLeft.No
            }

            If EnhancedReadability Then
                PopulateTimeHeaderEnhanced(_content, DayLabelWidth, timelineWidth, totalTimeMinutes, _pixelsPerMinute, headerFont, timeHeaderHeight, startHour, state.Use24HourFormat)
            Else
                timeHeaderHeight = 36
                For hour1 = startHour To endHour - 1
                    Dim hourX = DayLabelWidth + CInt((hour1 - startHour) * 60 * _pixelsPerMinute)
                    Dim lblHour As New Label With {
                        .Text = If(state.Use24HourFormat,
                                   hour1.ToString("00") & ":00",
                                   DateTime.Today.AddHours(hour1).ToString("hh:mm tt")),
                        .Left = hourX,
                        .Top = 0,
                        .Width = CInt(60 * _pixelsPerMinute),
                        .Height = timeHeaderHeight,
                        .TextAlign = ContentAlignment.BottomCenter,
                        .ForeColor = Color.FromArgb(80, 80, 80),
                        .Font = headerFont,
                        .BackColor = Color.Transparent
                    }
                    _content.Controls.Add(lblHour)
                Next
            End If

            Dim tlStatusColors As New Dictionary(Of String, Color) From {
                {"Pending", Color.LightGoldenrodYellow},
                {"Running", Color.LightSkyBlue},
                {"Completed", Color.LightGreen},
                {"Canceled", Color.LightCoral},
                {"Postponed", Color.LightGray}
            }

            apptFont = GetTimelineAppointmentFont(state)
            lblDrBoldFont = New Font(apptFont, FontStyle.Bold)
            Dim timeFormat = If(state.Use24HourFormat, "HH:mm", "hh:mm tt")
            Dim currentTop = timeHeaderHeight

            For dayIndex = 0 To 6
                Dim day = weekStart.AddDays(dayIndex)
                Dim dayAppts = ApptTheme.OrderAppointmentsForDisplay(
                    If(data.Appointments, New List(Of AppointmentC)()).Where(Function(a) MatchesScheduleDay(a, day)),
                    data, linkedDoctorAtEnd:=True)

                Dim stackRows As New List(Of List(Of AppointmentC))()
                For Each ap In dayAppts
                    Dim placed = False
                    For Each row In stackRows
                        If row.All(Function(existing) existing.EndDateTime <= ap.StartDateTime OrElse ap.EndDateTime <= existing.StartDateTime) Then
                            row.Add(ap)
                            placed = True
                            Exit For
                        End If
                    Next
                    If Not placed Then stackRows.Add(New List(Of AppointmentC) From {ap})
                Next

                Dim rowHeights As New List(Of Integer)()
                Dim rowAdvanceHeights As New List(Of Integer)()
                For rowIdxH = 0 To stackRows.Count - 1
                    Dim maxChipH = 0
                    Dim seqAp = 0
                    Dim paintPairs As New List(Of (dHdr As Integer, lh As Integer))()
                    For Each apH In stackRows(rowIdxH)
                        Dim startMinH = (apH.StartDateTime.TimeOfDay - TimeSpan.FromHours(startHour)).TotalMinutes
                        Dim endMinH = (apH.EndDateTime.TimeOfDay - TimeSpan.FromHours(startHour)).TotalMinutes
                        If startMinH < 0 Then startMinH = 0
                        If endMinH > totalTimeMinutes Then endMinH = totalTimeMinutes
                        If endMinH <= startMinH Then endMinH = startMinH + 15
                        Dim chipWidthH = Math.Max(50, CInt((endMinH - startMinH) * _pixelsPerMinute))
                        Dim patientNH = data.ResolvePatientName(apH.PatientID)
                        Dim idH = If(apH.DrID > 0, apH.DrID, 0)
                        Dim doctorNH = If(data.ResolveDoctor(idH) Is Nothing, "", data.ResolveDoctor(idH).DrName)
                        Dim doctorClrH = If(data.ResolveDoctor(idH) Is Nothing, Color.LightSteelBlue, data.ResolveDoctor(idH).DrColor)
                        Dim dHdrTmp As Integer
                        Dim txtTmp As String = Nothing
                        Dim lblBkTmp As Color = Color.White
                        Dim lw = 0, lh = 0, ch = 0
                        CalcChipLayout(apH, chipWidthH, patientNH, doctorNH, doctorClrH, apptFont, timeFormat, seqAp, state, dHdrTmp, txtTmp, lblBkTmp, lw, lh, ch)
                        maxChipH = Math.Max(maxChipH, ch)
                        paintPairs.Add((dHdrTmp, lh))
                        seqAp += 1
                    Next
                    rowHeights.Add(maxChipH)
                    Dim maxPaintedH = 0
                    For Each pair In paintPairs
                        maxPaintedH = Math.Max(maxPaintedH, ChipLayoutMetrics(pair.dHdr, pair.lh, maxChipH, apptFont).chipVisualH)
                    Next
                    rowAdvanceHeights.Add(maxPaintedH)
                Next

                Dim stackBlockH = 0
                For iRh = 0 To rowAdvanceHeights.Count - 1
                    stackBlockH += rowAdvanceHeights(iRh)
                    If iRh < rowAdvanceHeights.Count - 1 Then stackBlockH += ChipVerticalSpacing
                Next
                Dim dayRowHeight = If(stackRows.Count = 0, EmptyDayLaneHeight, stackBlockH + DayLanePadWithAppts * 2)

                Dim isToday = (day.Date = DateTime.Today)
                Dim lblDay As New Label With {
                    .Text = day.ToString("ddd") & "  " & day.ToString("dd MMM"),
                    .Left = 0,
                    .Top = currentTop,
                    .Width = DayLabelWidth,
                    .Height = dayRowHeight,
                    .TextAlign = ContentAlignment.MiddleCenter,
                    .BackColor = If(isToday,
                                    Color.FromArgb(210, 230, 255),
                                    If(dayIndex Mod 2 = 0, Color.FromArgb(245, 245, 248), Color.FromArgb(252, 252, 255))),
                    .ForeColor = If(isToday, Color.FromArgb(20, 70, 160), Color.FromArgb(60, 60, 60)),
                    .Font = headerFont,
                    .BorderStyle = BorderStyle.FixedSingle
                }
                _content.Controls.Add(lblDay)

                Dim dayRow As New Panel With {
                    .Left = DayLabelWidth,
                    .Top = currentTop,
                    .Width = timelineWidth,
                    .Height = dayRowHeight,
                    .BackColor = If(dayIndex Mod 2 = 0, Color.White, Color.FromArgb(250, 250, 252)),
                    .BorderStyle = BorderStyle.FixedSingle,
                    .Tag = day,
                    .AllowDrop = True
                }
                AddHandler dayRow.DragEnter, Sub(s, e) Row_DragEnter(e)
                AddHandler dayRow.DragOver, Sub(s, e) Row_DragOver(DirectCast(s, Panel), e)
                AddHandler dayRow.DragLeave, Sub(s, e) Row_DragLeave()
                AddHandler dayRow.DragDrop, Sub(s, e) Row_DragDrop(e)

                Dim pm = _pixelsPerMinute
                Dim sh = startHour
                AddHandler dayRow.DoubleClick,
                    Sub(sender, ev)
                        If InteractionHub Is Nothing Then Return
                        Dim rowPanel = DirectCast(sender, Panel)
                        Dim clickDay = DirectCast(rowPanel.Tag, DateTime)
                        Dim clickPos = rowPanel.PointToClient(Cursor.Position)
                        Dim clickMinutes = CInt(clickPos.X / pm) + (sh * 60)
                        Dim snapMin = CInt(Math.Round(clickMinutes / 15.0) * 15)
                        Dim clickTime = clickDay.Date.AddMinutes(snapMin)
                        InteractionHub.PublishEmptyDateInvoked(clickTime)
                    End Sub

                For hour1 = startHour To endHour
                    Dim lineX = CInt((hour1 - startHour) * 60 * _pixelsPerMinute)
                    Dim gridLine As New Panel With {
                        .Left = lineX,
                        .Top = 0,
                        .Width = 1,
                        .Height = dayRowHeight,
                        .BackColor = If(hour1 = startHour OrElse hour1 = endHour,
                                        Color.FromArgb(200, 200, 200),
                                        Color.FromArgb(230, 230, 230))
                    }
                    dayRow.Controls.Add(gridLine)
                    gridLine.SendToBack()
                Next

                For hour1 = startHour To endHour - 1
                    Dim halfLineX = CInt(((hour1 - startHour) * 60 + 30) * _pixelsPerMinute)
                    Dim halfLine As New Panel With {
                        .Left = halfLineX,
                        .Top = 0,
                        .Width = 1,
                        .Height = dayRowHeight,
                        .BackColor = Color.FromArgb(242, 242, 242)
                    }
                    dayRow.Controls.Add(halfLine)
                    halfLine.SendToBack()
                Next

                Dim yRowCursor = DayLanePadWithAppts
                For rowIdx = 0 To stackRows.Count - 1
                    Dim rowRunMaxH = 0
                    Dim seqInRow = 0
                    Dim rowPaintAps As New List(Of AppointmentC)()
                    Dim rowPaintChipLefts As New List(Of Integer)()
                    Dim rowPaintChipWidths As New List(Of Integer)()
                    Dim rowPaintDoctorColors As New List(Of Color)()
                    Dim rowPaintPatientNames As New List(Of String)()
                    Dim rowPaintDoctorNames As New List(Of String)()
                    Dim rowPaintApptTexts As New List(Of String)()
                    Dim rowPaintLabelBacks As New List(Of Color)()
                    Dim rowPaintDoctorHdrHs As New List(Of Integer)()
                    Dim rowPaintAppTops As New List(Of Integer)()
                    Dim rowPaintAppTextHs As New List(Of Integer)()
                    Dim rowPaintStatusTops As New List(Of Integer)()
                    Dim rowPaintStatusStripHs As New List(Of Integer)()
                    Dim rowPaintChipVisualHs As New List(Of Integer)()

                    For Each ap In stackRows(rowIdx)
                        Dim startMin = (ap.StartDateTime.TimeOfDay - TimeSpan.FromHours(startHour)).TotalMinutes
                        Dim endMin = (ap.EndDateTime.TimeOfDay - TimeSpan.FromHours(startHour)).TotalMinutes
                        If startMin < 0 Then startMin = 0
                        If endMin > totalTimeMinutes Then endMin = totalTimeMinutes
                        If endMin <= startMin Then endMin = startMin + 15
                        Dim durationMin = endMin - startMin
                        Dim chipLeft = CInt(startMin * _pixelsPerMinute)
                        Dim chipWidth = Math.Max(50, CInt(durationMin * _pixelsPerMinute))
                        Dim clinId = If(ap.DrID > 0, ap.DrID, 0)
                        Dim dInfo = data.ResolveDoctor(clinId)
                        Dim doctorColor = If(dInfo Is Nothing, Color.LightSteelBlue, dInfo.DrColor)
                        Dim patientName = data.ResolvePatientName(ap.PatientID)
                        Dim doctorName = If(dInfo Is Nothing, "", dInfo.DrName)
                        Dim apptText As String = Nothing
                        Dim labelBack As Color
                        Dim labelW As Integer
                        Dim labelH As Integer
                        Dim chipH As Integer
                        Dim doctorHdrH As Integer
                        CalcChipLayout(ap, chipWidth, patientName, doctorName, doctorColor, apptFont, timeFormat, seqInRow, state, doctorHdrH, apptText, labelBack, labelW, labelH, chipH)
                        Dim rowBandH = rowHeights(rowIdx)
                        Dim lay = ChipLayoutMetrics(doctorHdrH, labelH, rowBandH, apptFont)
                        rowRunMaxH = Math.Max(rowRunMaxH, lay.chipVisualH)
                        rowPaintAps.Add(ap)
                        rowPaintChipLefts.Add(chipLeft)
                        rowPaintChipWidths.Add(chipWidth)
                        rowPaintDoctorColors.Add(doctorColor)
                        rowPaintPatientNames.Add(patientName)
                        rowPaintDoctorNames.Add(doctorName)
                        rowPaintApptTexts.Add(apptText)
                        rowPaintLabelBacks.Add(labelBack)
                        rowPaintDoctorHdrHs.Add(doctorHdrH)
                        rowPaintAppTops.Add(lay.appTop)
                        rowPaintAppTextHs.Add(lay.appTextH)
                        rowPaintStatusTops.Add(lay.statusTop)
                        rowPaintStatusStripHs.Add(lay.statusStripH)
                        rowPaintChipVisualHs.Add(lay.chipVisualH)
                        seqInRow += 1
                    Next

                    For rpIdx = 0 To rowPaintAps.Count - 1
                        Dim ap = rowPaintAps(rpIdx)
                        Dim chipLeft = rowPaintChipLefts(rpIdx)
                        Dim chipWidth = rowPaintChipWidths(rpIdx)
                        Dim doctorColor = rowPaintDoctorColors(rpIdx)
                        Dim patientName = rowPaintPatientNames(rpIdx)
                        Dim doctorName = rowPaintDoctorNames(rpIdx)
                        Dim apptText = rowPaintApptTexts(rpIdx)
                        Dim labelBack = rowPaintLabelBacks(rpIdx)
                        Dim doctorHdrH = rowPaintDoctorHdrHs(rpIdx)
                        Dim appTop = rowPaintAppTops(rpIdx)
                        Dim appTextH = rowPaintAppTextHs(rpIdx)
                        Dim statusTop = rowPaintStatusTops(rpIdx)
                        Dim statusStripH = rowPaintStatusStripHs(rpIdx)
                        Dim chipVisualH = rowPaintChipVisualHs(rpIdx)
                        Dim innerChipW = chipWidth - TlBodyInset * 2
                        Dim chipTop = yRowCursor
                        Dim extraH = rowRunMaxH - chipVisualH
                        If extraH > 0 Then
                            appTextH += extraH
                            statusTop += extraH
                        End If

                        Dim chip As New Panel With {
                            .Left = chipLeft,
                            .Top = chipTop,
                            .Width = chipWidth,
                            .Height = rowRunMaxH,
                            .BackColor = labelBack,
                            .BorderStyle = BorderStyle.FixedSingle,
                            .Tag = ap,
                            .Cursor = Cursors.Hand
                        }
                        AddHandler chip.Paint,
                            Sub(s, pe)
                                ' Same feedback as ApptDayDoctors on ApptCardCtl: 3px Red (edge resize), BlueViolet (hold then DoDragDrop), proposed rect during drag-width.
                                Dim pnl = DirectCast(s, Panel)
                                Dim g = pe.Graphics
                                If _resizingChip Is pnl AndAlso Not _resizeProposedInChip.IsEmpty Then
                                    Dim sClip = g.Save()
                                    Try
                                        g.SetClip(New Rectangle(-4000, -4000, 10000, 10000), CombineMode.Replace)
                                        Using p As New Pen(Color.Red, 3.0F)
                                            g.DrawRectangle(p, _resizeProposedInChip)
                                        End Using
                                    Finally
                                        g.Restore(sClip)
                                    End Try
                                End If
                                If _resizingChip Is pnl Then
                                    Using p As New Pen(Color.Red, 3.0F)
                                        Dim r = pnl.ClientRectangle
                                        r.Inflate(-1, -1)
                                        g.DrawRectangle(p, r)
                                    End Using
                                ElseIf _holdStripBlueChip Is pnl Then
                                    Using p As New Pen(Color.BlueViolet, 3.0F)
                                        Dim r = pnl.ClientRectangle
                                        r.Inflate(-1, -1)
                                        g.DrawRectangle(p, r)
                                    End Using
                                End If
                            End Sub

                        Dim lblDr As New Label With {
                            .Text = If(String.IsNullOrWhiteSpace(doctorName), If(Eng, "(No doctor)", "لا طبيب"), doctorName),
                            .Location = New Point(0, 0),
                            .Size = New Size(chipWidth, doctorHdrH),
                            .Font = lblDrBoldFont,
                            .BackColor = doctorColor,
                            .ForeColor = GetContrastColor(doctorColor),
                            .TextAlign = ContentAlignment.MiddleLeft,
                            .Padding = New Padding(4, 2, 2, 2),
                            .Tag = ap,
                            .Cursor = Cursors.Hand
                        }

                        Dim lblApp As New Label With {
                            .Text = apptText,
                            .Location = New Point(TlBodyInset, appTop),
                            .Size = New Size(innerChipW, appTextH),
                            .Font = apptFont,
                            .TextAlign = ContentAlignment.TopLeft,
                            .BorderStyle = BorderStyle.None,
                            .BackColor = Color.Transparent,
                            .ForeColor = GetContrastColor(labelBack),
                            .Tag = ap,
                            .Cursor = Cursors.Hand
                        }
                        Dim lblStatus As New Label With {
                            .AutoSize = False,
                            .Width = innerChipW,
                            .Height = statusStripH,
                            .TextAlign = ContentAlignment.MiddleCenter,
                            .Font = apptFont,
                            .Tag = ap,
                            .Text = GetAppointmentStatusDisplayText(ap),
                            .ForeColor = Color.Black,
                            .BackColor = If(tlStatusColors.ContainsKey(If(ap.Status, "")), tlStatusColors(If(ap.Status, "")), Color.LightGray),
                            .Location = New Point(TlBodyInset, statusTop),
                            .Cursor = Cursors.Hand
                        }

                        Dim tipTls = If(Eng,
                               $"Patient: {patientName}{vbCrLf}Doctor: {doctorName}{vbCrLf}Time: {ap.StartDateTime.ToString(timeFormat)} - {ap.EndDateTime.ToString(timeFormat)}{vbCrLf}Reason: {If(ap.Reason, "")}{vbCrLf}Notes: {If(ap.Notes, "")}{vbCrLf}Status: {If(ap.Status, "")}",
                               $"المريض: {patientName}{vbCrLf}الطبيب: {doctorName}{vbCrLf}الوقت: {ap.StartDateTime.ToString(timeFormat)} - {ap.EndDateTime.ToString(timeFormat)}{vbCrLf}السبب: {If(ap.Reason, "")}{vbCrLf}ملاحظات: {If(ap.Notes, "")}{vbCrLf}الحالة: {GetAppointmentStatusDisplayText(ap)}")
                        _toolTip.SetToolTip(chip, tipTls)
                        _toolTip.SetToolTip(lblDr, tipTls)
                        _toolTip.SetToolTip(lblApp, tipTls)
                        _toolTip.SetToolTip(lblStatus, tipTls)

                        WireChipClicks(chip, ap)
                        WireChipClicks(lblDr, ap)
                        WireChipClicks(lblApp, ap)
                        WireChipClicks(lblStatus, ap)

                        ' Wire Drag/Resize
                        Dim dayVal = day
                        AddHandler chip.MouseDown, Sub(s, ev) OnChipDragMouseDown(DirectCast(s, Panel), dayVal, ev)
                        AddHandler chip.MouseMove, Sub(s, ev) OnChipDragMouseMove(DirectCast(s, Panel), ev)
                        AddHandler chip.MouseUp, Sub(s, ev) OnChipDragMouseUp(DirectCast(s, Panel), ev)

                        AddHandler lblDr.MouseDown, Sub(s, ev) OnChipDragMouseDown(chip, dayVal, ev)
                        AddHandler lblDr.MouseMove, Sub(s, ev) OnChipDragMouseMove(chip, ev)
                        AddHandler lblDr.MouseUp, Sub(s, ev) OnChipDragMouseUp(chip, ev)

                        AddHandler lblApp.MouseDown, Sub(s, ev) OnChipDragMouseDown(chip, dayVal, ev)
                        AddHandler lblApp.MouseMove, Sub(s, ev) OnChipDragMouseMove(chip, ev)
                        AddHandler lblApp.MouseUp, Sub(s, ev) OnChipDragMouseUp(chip, ev)

                        AddHandler lblStatus.MouseDown, Sub(s, ev) OnChipDragMouseDown(chip, dayVal, ev)
                        AddHandler lblStatus.MouseMove, Sub(s, ev) OnChipDragMouseMove(chip, ev)
                        AddHandler lblStatus.MouseUp, Sub(s, ev) OnChipDragMouseUp(chip, ev)

                        Dim apLocal = ap
                        AddHandler lblStatus.MouseUp,
                            Sub(s, ev)
                                If ev.Button <> MouseButtons.Right Then Return
                                ShowTimelineStatusContextMenu(DirectCast(s, Label), apLocal, ev)
                            End Sub

                        chip.Controls.Add(lblDr)
                        chip.Controls.Add(lblApp)
                        chip.Controls.Add(lblStatus)
                        lblStatus.BringToFront()
                        dayRow.Controls.Add(chip)
                        chip.BringToFront()
                    Next
                    yRowCursor += rowRunMaxH + If(rowIdx < stackRows.Count - 1, ChipVerticalSpacing, 0)
                Next

                _content.Controls.Add(dayRow)
                currentTop += dayRowHeight
            Next

            Dim tlMaxR = 0
            Dim tlMaxB = 0
            AccumulateExtents(_content, tlMaxR, tlMaxB)
            Const slack = 12
            Dim minW = Math.Max(_scrollHost.ClientSize.Width, DayLabelWidth + timelineWidth + slack)
            _content.Width = Math.Max(minW, tlMaxR + slack)
            _content.Height = Math.Max(currentTop, tlMaxB + slack)
            _scrollHost.Controls.Add(_content)
        Finally
            If lblDrBoldFont IsNot Nothing Then lblDrBoldFont.Dispose()
            If apptFont IsNot Nothing Then apptFont.Dispose()
            If headerFont IsNot Nothing Then headerFont.Dispose()
            _scrollHost.ResumeLayout(True)
        End Try
    End Sub

    Private Sub WireChipClicks(host As Control, ap As AppointmentC)
        AddHandler host.Click,
            Sub(s, ev)
                If InteractionHub Is Nothing Then Return
                InteractionHub.PublishAppointmentClicked(ap)
            End Sub
        AddHandler host.DoubleClick,
            Sub(s, ev)
                If InteractionHub Is Nothing Then Return
                InteractionHub.PublishAppointmentDoubleClicked(ap)
            End Sub
    End Sub

    Private Sub ShowTimelineStatusContextMenu(host As Label, ap As AppointmentC, e As MouseEventArgs)
        If InteractionHub Is Nothing Then Return
        Dim statusColors = GetStandardAppointmentStatusColors()
        Dim menuFont As New Font("Calibri", 10.0F, FontStyle.Bold)
        Dim contextMenu As New ContextMenuStrip With {
            .RightToLeft = If(Eng, RightToLeft.No, RightToLeft.Yes),
            .Font = menuFont,
            .ShowImageMargin = False
        }
        Dim editItem = New ToolStripMenuItem(If(Eng, "Edit appointment...", "تعديل الموعد...")) With {.Font = menuFont}
        AddHandler editItem.Click, Sub(s, ea) InteractionHub.PublishAppointmentDoubleClicked(ap)
        contextMenu.Items.Add(editItem)
        contextMenu.Items.Add(New ToolStripSeparator())
        For Each kvp In statusColors
            Dim statusKey = kvp.Key
            Dim c = kvp.Value
            Dim menuItem = New ToolStripMenuItem(TranslateAppointmentStatus(statusKey)) With {.Font = menuFont}
            AddHandler menuItem.Click, Sub(s, ea) InteractionHub.PublishAppointmentStatusChange(ap, statusKey, c)
            contextMenu.Items.Add(menuItem)
        Next
        contextMenu.Show(host, e.Location)
    End Sub

    Private Shared Function MatchesScheduleDay(a As AppointmentC, day As DateTime) As Boolean
        If a Is Nothing Then Return False
        If a.AppDate <> Date.MinValue AndAlso a.AppDate.Date = day.Date Then Return True
        Return ApptTheme.GetAppointmentCalendarDay(a) = day.Date
    End Function

    Private Shared Function GetTimelineAppointmentFont(state As ApptState) As Font
        Dim fontStyle As FontStyle = If(state IsNot Nothing AndAlso state.UseBoldAppointments, FontStyle.Bold, FontStyle.Regular)
        Dim fontSize As Single = If(state IsNot Nothing AndAlso state.UseLargeAppointments, 10.0F, 8.0F)
        Return New Font("Calibri", fontSize, fontStyle)
    End Function

    Private Shared Sub PopulateTimeHeaderEnhanced(
        _content As Panel,
        dayLabelWidth As Integer,
        timelineWidth As Integer,
        totalTimeMinutes As Integer,
        _pixelsPerMinute As Double,
        headerFontBold As Font,
        ByRef timeHeaderHeightOut As Integer,
        startHour As Integer,
        use24 As Boolean)

        Const labelAreaHeight = 30
        Const rulerHeight = 6
        timeHeaderHeightOut = labelAreaHeight + rulerHeight
        Dim timelineRight = dayLabelWidth + timelineWidth
        Using halfHourFont As New Font(headerFontBold.FontFamily, 10.0F, FontStyle.Regular)
            For offsetMin = 0 To totalTimeMinutes - 1 Step 30
                Dim tickTime = DateTime.Today.AddHours(startHour).AddMinutes(offsetMin)
                Dim tickX = dayLabelWidth + CInt(offsetMin * _pixelsPerMinute)
                Dim isHourMark = (offsetMin Mod 60 = 0)
                Dim tickFont = If(isHourMark, headerFontBold, halfHourFont)
                Dim text = If(use24, tickTime.ToString("H:mm"), tickTime.ToString("h:mm"))
                Dim textSize = TextRenderer.MeasureText(text, tickFont, New Size(Integer.MaxValue, labelAreaHeight), TextFormatFlags.SingleLine Or TextFormatFlags.NoPadding)
                Dim lblW = Math.Max(1, textSize.Width)
                Dim lblLeft = tickX - lblW \ 2
                If lblLeft < 0 Then lblLeft = 0
                If lblLeft + lblW > timelineRight Then lblLeft = Math.Max(0, timelineRight - lblW)
                Dim lblTick As New Label With {
                    .Text = text,
                    .Left = lblLeft,
                    .Top = 0,
                    .Width = lblW,
                    .Height = labelAreaHeight,
                    .TextAlign = ContentAlignment.BottomCenter,
                    .ForeColor = If(isHourMark, Color.FromArgb(50, 50, 50), Color.FromArgb(115, 115, 115)),
                    .Font = tickFont,
                    .BackColor = Color.Transparent
                }
                _content.Controls.Add(lblTick)
            Next
        End Using

        Dim ruler As New Panel With {
            .Left = dayLabelWidth,
            .Top = labelAreaHeight,
            .Width = timelineWidth,
            .Height = rulerHeight,
            .BackColor = Color.Transparent
        }
        AddHandler ruler.Paint,
            Sub(sender, ev)
                Dim g = ev.Graphics
                g.SmoothingMode = SmoothingMode.AntiAlias
                Using penMajor As New Pen(Color.FromArgb(150, 150, 150)),
                      penMinor As New Pen(Color.FromArgb(205, 205, 205))
                    For m = 0 To totalTimeMinutes Step 15
                        Dim x = CSng(CInt(m * _pixelsPerMinute))
                        Dim isHour = (m Mod 60 = 0)
                        Dim isHalf = (m Mod 60 = 30)
                        Dim tickDrawHeight = If(isHour, rulerHeight, If(isHalf, rulerHeight * 0.7F, rulerHeight * 0.38F))
                        Dim p = If(isHour, penMajor, penMinor)
                        g.DrawLine(p, x, rulerHeight, x, rulerHeight - tickDrawHeight)
                    Next
                End Using
            End Sub
        _content.Controls.Add(ruler)
    End Sub

    Private Shared Sub CalcChipLayout(
        ap As AppointmentC,
        chipWidth As Integer,
        patientName As String,
        doctorName As String,
        doctorColor As Color,
        appointmentFont As Font,
        timeFormat As String,
        alternateIdx As Integer,
        state As ApptState,
        ByRef doctorHdrH As Integer,
        ByRef appointmentText As String,
        ByRef labelBack As Color,
        ByRef labelW As Integer,
        ByRef labelH As Integer,
        ByRef chipHeight As Integer)

        Using hdrFont As New Font(appointmentFont, FontStyle.Bold)
            Dim hdrProbe = If(String.IsNullOrWhiteSpace(doctorName), If(Eng, "(No doctor)", "(—)"), doctorName)
            doctorHdrH = TextRenderer.MeasureText(hdrProbe, hdrFont, New Size(Math.Max(1, chipWidth), Integer.MaxValue), TextFormatFlags.SingleLine Or TextFormatFlags.EndEllipsis).Height + 2
        End Using
        doctorHdrH = Math.Max(18, doctorHdrH)
        appointmentText = ap.StartDateTime.ToString(timeFormat) & "-" & ap.EndDateTime.ToString(timeFormat) & vbCrLf & patientName
        AppendReasonNotesIfEnabled(appointmentText, ap, state)
        Dim lighter = ControlPaint.Light(doctorColor, 0.7F)
        Dim darker = ControlPaint.Dark(doctorColor, 0.3F)
        labelBack = If(alternateIdx Mod 2 = 0, lighter, darker)
        labelW = Math.Max(40, chipWidth - 4)
        labelH = MeasureAppTextHeight(appointmentText, appointmentFont, labelW)
        Dim maxTextH = appointmentFont.Height * 3 + 4
        If labelH > maxTextH Then labelH = maxTextH
        Dim statusStripHCalc = Math.Max(20, appointmentFont.Height + 4)
        Dim bodyH = labelH + statusStripHCalc + ChipTailNarrow
        chipHeight = doctorHdrH + bodyH
    End Sub

    Private Shared Function ChipLayoutMetrics(doctorHdrH As Integer, labelH As Integer, rowBandH As Integer, appointmentFontTl As Font) As (appTop As Integer, appTextH As Integer, statusTop As Integer, statusStripH As Integer, chipVisualH As Integer)
        Dim tail = ChipTailNarrow
        Dim statusStripH = Math.Max(20, appointmentFontTl.Height + 4)
        Dim appTop = doctorHdrH + TlBodyInset
        Dim appTextH = labelH
        Dim statusTop = appTop + appTextH + tail
        Dim contentBottom = statusTop + statusStripH + TlBodyInset
        Dim paintLimitH = rowBandH + TlBodyInset * 2
        If contentBottom > paintLimitH Then
            statusTop = paintLimitH - statusStripH - TlBodyInset
            appTextH = Math.Max(1, statusTop - tail - appTop)
        End If
        Dim chipVisualH = Math.Max(1, statusTop + statusStripH + TlBodyInset)
        Return (appTop, appTextH, statusTop, statusStripH, chipVisualH)
    End Function

    Private Shared Sub AppendReasonNotesIfEnabled(ByRef appointmentText As String, ap As AppointmentC, state As ApptState)
        If ap Is Nothing Then Return
        If state Is Nothing OrElse Not state.IncludeReason Then Return
        Dim r = If(ap.Reason, "").Trim()
        Dim n = If(ap.Notes, "").Trim()
        If r.Length > 0 Then appointmentText &= vbCrLf & r
        If n.Length > 0 Then appointmentText &= vbCrLf & n
    End Sub

    Private Shared Function MeasureAppTextHeight(text As String, font As Font, width As Integer) As Integer
        Dim maxWidth = Math.Max(1, width)
        Dim measured = TextRenderer.MeasureText(text, font, New Size(maxWidth, Integer.MaxValue), TextFormatFlags.WordBreak Or TextFormatFlags.NoPadding)
        Return Math.Max(font.Height, measured.Height + 2)
    End Function

    Private Shared Sub AccumulateExtents(root As Control, ByRef maxRight As Integer, ByRef maxBottom As Integer)
        If root Is Nothing Then Return
        For Each c As Control In root.Controls
            maxRight = Math.Max(maxRight, c.Right)
            maxBottom = Math.Max(maxBottom, c.Bottom)
            If c.HasChildren Then AccumulateExtents(c, maxRight, maxBottom)
        Next
    End Sub

    Private Shared Function GetContrastColor(backColor As Color) As Color
        Dim luminance = (0.299 * backColor.R + 0.587 * backColor.G + 0.114 * backColor.B) / 255.0
        Return If(luminance > 0.5, Color.Black, Color.White)
    End Function

    Private Sub TryScrollToAppointment(ap As AppointmentC)
        If ap Is Nothing OrElse _scrollHost Is Nothing Then Return
        For Each c0 As Control In _scrollHost.Controls
            Dim found = FindChipForAppointment(c0, ap)
            If found IsNot Nothing Then
                _scrollHost.ScrollControlIntoView(found)
                Return
            End If
        Next
    End Sub

    Private Shared Function FindChipForAppointment(root As Control, ap As AppointmentC) As Control
        If root Is Nothing Then Return Nothing
        Dim chip = TryCast(root, Panel)
        If chip IsNot Nothing AndAlso chip.Tag Is ap Then Return chip
        For Each c As Control In root.Controls
            Dim inner = FindChipForAppointment(c, ap)
            If inner IsNot Nothing Then Return inner
        Next
        Return Nothing
    End Function
End Class
