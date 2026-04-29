Imports System
Imports System.Collections.Generic
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Linq
Imports System.Windows.Forms
Imports DevExpress.Utils
Imports DevExpress.XtraEditors

''' <summary>
''' Doctors day: single-day painted timeline with calm hour grid, doctor columns, and <see cref="HtmlCard"/> blocks
''' (<see cref="ApptViewMode.DoctorsDay"/>). Same layout model as the former <c>ApptDayCtl</c> implementation.
''' </summary>
Public Class ApptDayDoctors
    Inherits XtraUserControl
    Implements IApptViewCtl

    Private Shared ReadOnly WorkAreaWash As Color = Color.FromArgb(246, 248, 252)
    Private Shared ReadOnly HeaderWash As Color = Color.FromArgb(250, 251, 255)
    Private Shared ReadOnly CanvasPaper As Color = Color.FromArgb(252, 253, 255)
    Private Shared ReadOnly HourLine As Color = Color.FromArgb(218, 226, 238)
    Private Shared ReadOnly HalfHourLine As Color = Color.FromArgb(236, 240, 247)
    Private Shared ReadOnly HalfHourBand As Color = Color.FromArgb(244, 247, 252)
    Private Shared ReadOnly ColumnHairline As Color = Color.FromArgb(218, 224, 234)
    Private Shared ReadOnly TickHour As Color = Color.FromArgb(90, 102, 122)
    Private Shared ReadOnly TickHalf As Color = Color.FromArgb(160, 172, 192)
    Private Shared ReadOnly NowLine As Color = Color.FromArgb(0, 151, 167)

    Private Const TimeGutterPx As Integer = 56
    Private Const MinDoctorColPx As Integer = 188
    Private Const CardInsetPx As Integer = 5
    Private Const LaneStackPx As Integer = 3
    Private Const CanvasTopPadPx As Integer = 6
    Private Const DoctorHeaderPx As Integer = 30
    Private Const SnapMinutes As Integer = 15
    Private Const MainHeaderHeightPx As Integer = 46

    ''' <summary>Vertical scale: pixels per minute (~1.85 → ~55px per 30 minutes).</summary>
    Private ReadOnly _pxPerMinute As Single = 1.85F

    Private _request As ApptViewRequest
    Private _scrollHost As Panel
    Private _canvas As DayDoctorsTimelineCanvas
    Private _headerBand As Panel
    Private _nowTimer As Timer
    ''' <summary>Painted in <see cref="HeaderBand_Paint"/> — weekday + comma, localized.</summary>
    Private _headerWeekdayLead As String
    ''' <summary>Painted at 14pt bold — numeric date (dd MMM yyyy).</summary>
    Private _headerDateCore As String
    ''' <summary>Painted at 10pt bold — · count · working hours.</summary>
    Private _headerMetaTail As String
    Private _headerRtl As Boolean
    ''' <summary>Left-to-right doctor column order for the current day — used for drag/drop hit testing.</summary>
    Private _dayColumnDoctorIds As List(Of Integer)
    Private _chkShowAllDoctors As CheckEdit

    ' --- Drag/Resize State ---
    Private _dragSourceCard As HtmlCard
    Private _dragSourceAppt As AppointmentC
    Private _dragSourceDay As Date?
    Private ReadOnly _holdTimer As New Timer With {.Enabled = False}
    
    Private _resizingCard As HtmlCard
    Private _resizeEdge As HtmlCard.CardEdge
    Private _resizeStartTime As DateTime
    Private _resizeEndTime As DateTime
    Private _resizeStartPt As Point
    
    Private _dragTargetTime As DateTime = DateTime.MinValue
    Private _dragTargetDrId As Integer = -1

    Private _ghostRect As Rectangle = Rectangle.Empty
    Private _draggingDuration As TimeSpan = TimeSpan.Zero

    Public Sub New()
        Appearance.BackColor = WorkAreaWash
        Appearance.Options.UseBackColor = True
        DoubleBuffered = True

        _headerBand = New Panel With {
            .Dock = DockStyle.Top,
            .Height = MainHeaderHeightPx,
            .BackColor = HeaderWash
        }

        _scrollHost = New Panel With {
            .Dock = DockStyle.Fill,
            .AutoScroll = True,
            .BackColor = WorkAreaWash,
            .Padding = New Padding(14, 16, 14, 14)
        }

        _canvas = New DayDoctorsTimelineCanvas(Me) With {
            .Location = Point.Empty,
            .BackColor = CanvasPaper,
            .AllowDrop = True
        }
        _scrollHost.Controls.Add(_canvas)

        Controls.Add(_scrollHost)
        Controls.Add(_headerBand)

        AddHandler _headerBand.Paint, AddressOf HeaderBand_Paint
        AddHandler _headerBand.Resize, Sub(s, a) _headerBand.Invalidate()
        AddHandler _scrollHost.Resize, AddressOf ScrollHost_Resize
        
        AddHandler _holdTimer.Tick, AddressOf HoldTimer_Tick

        _chkShowAllDoctors = New CheckEdit With {
            .Anchor = AnchorStyles.Top Or AnchorStyles.Left,
            .Location = New Point(8, (MainHeaderHeightPx - 22) \ 2),
            .AutoSize = True
        }
        _chkShowAllDoctors.Properties.AutoWidth = True
        AddHandler _chkShowAllDoctors.CheckedChanged, Sub()
                                                        If _canvas IsNot Nothing Then RenderDay()
                                                    End Sub
        _headerBand.Controls.Add(_chkShowAllDoctors)
        _chkShowAllDoctors.BringToFront()

        AddHandler _canvas.DragEnter, AddressOf Canvas_DragEnter
        AddHandler _canvas.DragOver, AddressOf Canvas_DragOver
        AddHandler _canvas.DragLeave, AddressOf Canvas_DragLeave
        AddHandler _canvas.DragDrop, AddressOf Canvas_DragDrop

        _nowTimer = New Timer With {.Interval = 60000}
        AddHandler _nowTimer.Tick, Sub()
                                       If _canvas IsNot Nothing AndAlso _canvas.Visible Then _canvas.Invalidate()
                                   End Sub
        _nowTimer.Start()
    End Sub

    Private Sub OnCardDragMouseDown(card As HtmlCard, day As Date, e As MouseEventArgs)
        If e.Button <> MouseButtons.Left Then Return
        If card.BoundAppointment Is Nothing OrElse card.BoundAppointment.AppointmentID <= 0 Then Return

        Dim edge = card.GetEdgeAt(e.Location)
        If edge <> HtmlCard.CardEdge.None Then
            ' Start Resize
            _resizingCard = card
            _resizeEdge = edge
            _resizeStartPt = card.PointToScreen(e.Location)
            _resizeStartTime = card.BoundAppointment.StartDateTime
            _resizeEndTime = card.BoundAppointment.EndDateTime
            _draggingDuration = _resizeEndTime - _resizeStartTime
            card.IndicatorColor = Color.Red
            Return
        End If

        ' Start Hold Timer for Move
        _holdTimer.Stop()
        _dragSourceCard = card
        _dragSourceAppt = card.BoundAppointment
        _dragSourceDay = day
        _draggingDuration = _dragSourceAppt.EndDateTime - _dragSourceAppt.StartDateTime
        _holdTimer.Interval = Math.Max(200, If(_request IsNot Nothing, _request.DragHoldTimeMs, 750))
        _holdTimer.Start()
    End Sub

    Private Sub OnCardDragMouseMove(card As HtmlCard, e As MouseEventArgs)
        If _resizingCard Is card Then
            PerformResize(card, e)
            card.Cursor = Cursors.SizeNS
            Return
        End If

        ' Update cursor for resize hit-test
        Dim edge = card.GetEdgeAt(e.Location)
        card.Cursor = If(edge <> HtmlCard.CardEdge.None, Cursors.SizeNS, Cursors.Hand)
    End Sub

    Private Sub PerformResize(card As HtmlCard, e As MouseEventArgs)
        Dim currentPt = card.PointToScreen(e.Location)
        Dim diffY = currentPt.Y - _resizeStartPt.Y
        
        ' Snapped diff for logic
        Dim diffMinsSnapped = (CInt(diffY / _pxPerMinute) \ 5) * 5

        Dim newStart = _resizeStartTime
        Dim newEnd = _resizeEndTime

        If _resizeEdge = HtmlCard.CardEdge.Top Then
            newStart = _resizeStartTime.AddMinutes(diffMinsSnapped)
            If newStart >= newEnd.AddMinutes(-5) Then newStart = newEnd.AddMinutes(-5)
        Else
            newEnd = _resizeEndTime.AddMinutes(diffMinsSnapped)
            If newEnd <= newStart.AddMinutes(5) Then newEnd = newStart.AddMinutes(5)
        End If

        ' Ghost visual - smooth motion
        Dim gridTop = CanvasTopPadPx + DoctorHeaderPx
        Dim day = _request.State.CurrentDate.Date
        Dim workStart = day.Add(_request.State.WorkStartTime)

        Dim visualDiffY = CSng(diffY)
        Dim yGhost As Single
        Dim hGhost As Single

        If _resizeEdge = HtmlCard.CardEdge.Top Then
            yGhost = CSng((_resizeStartTime - workStart).TotalMinutes * _pxPerMinute) + gridTop + visualDiffY
            hGhost = CSng((_resizeEndTime - _resizeStartTime).TotalMinutes * _pxPerMinute) - visualDiffY
        Else
            yGhost = CSng((_resizeStartTime - workStart).TotalMinutes * _pxPerMinute) + gridTop
            hGhost = CSng((_resizeEndTime - _resizeStartTime).TotalMinutes * _pxPerMinute) + visualDiffY
        End If

        If hGhost < 10 Then hGhost = 10
        _ghostRect = New Rectangle(card.Left, CInt(yGhost), card.Width, CInt(hGhost))
        _canvas.Invalidate()
    End Sub

    Private Sub OnCardDragMouseUp(card As HtmlCard, e As MouseEventArgs)
        _holdTimer.Stop()

        If _resizingCard Is card Then
            CommitResize(card, e)
            _resizingCard = Nothing
            _ghostRect = Rectangle.Empty
            card.IndicatorColor = Color.Transparent
            _canvas.Invalidate()
            Return
        End If

        _dragSourceCard = Nothing
        _dragSourceAppt = Nothing
        _dragSourceDay = Nothing
        _ghostRect = Rectangle.Empty
        card.IndicatorColor = Color.Transparent
        _canvas.Invalidate()
    End Sub

    Private Sub CommitResize(card As HtmlCard, e As MouseEventArgs)
        Dim currentPt = card.PointToScreen(e.Location)
        Dim diffY = currentPt.Y - _resizeStartPt.Y
        Dim diffMins = CInt(diffY / _pxPerMinute)
        diffMins = (diffMins \ 5) * 5

        Dim newStart = _resizeStartTime
        Dim newEnd = _resizeEndTime

        If _resizeEdge = HtmlCard.CardEdge.Top Then
            newStart = _resizeStartTime.AddMinutes(diffMins)
            If newStart >= newEnd.AddMinutes(-5) Then newStart = newEnd.AddMinutes(-5)
        Else
            newEnd = _resizeEndTime.AddMinutes(diffMins)
            If newEnd <= newStart.AddMinutes(5) Then newEnd = newStart.AddMinutes(5)
        End If

        If newStart <> _resizeStartTime OrElse newEnd <> _resizeEndTime Then
            If InteractionHub IsNot Nothing Then
                InteractionHub.PublishAppointmentTimeChange(card.BoundAppointment, newStart, newEnd)
            End If
        End If
    End Sub

    Private Sub HoldTimer_Tick(sender As Object, e As EventArgs)
        _holdTimer.Stop()
        If _dragSourceCard Is Nothing OrElse _dragSourceAppt Is Nothing Then Return

        Try
            _dragSourceCard.IndicatorColor = Color.BlueViolet
            _dragSourceCard.Update()

            Dim dObj As New DataObject()
            dObj.SetData("Appointment", _dragSourceAppt)
            dObj.SetData("SourceDay", If(_dragSourceDay, Date.Today))
            dObj.SetData("SourceDoctor", _dragSourceAppt.DrID)
            _dragSourceCard.DoDragDrop(dObj, DragDropEffects.Move)
        Finally
            _dragSourceCard.IndicatorColor = Color.Transparent
            _dragSourceCard = Nothing
            _dragSourceAppt = Nothing
            _dragSourceDay = Nothing
            _dragTargetTime = DateTime.MinValue
            _dragTargetDrId = -1
            _canvas.Invalidate()
        End Try
    End Sub

    Private Sub Canvas_DragEnter(sender As Object, e As DragEventArgs)
        If e.Data.GetDataPresent("Appointment") Then
            e.Effect = DragDropEffects.Move
        Else
            e.Effect = DragDropEffects.None
        End If
    End Sub

    Private Sub Canvas_DragOver(sender As Object, e As DragEventArgs)
        If Not e.Data.GetDataPresent("Appointment") Then Return
        If _request Is Nothing OrElse _request.State Is Nothing Then Return

        Dim pt = _canvas.PointToClient(New Point(e.X, e.Y))
        Dim gridTop = CanvasTopPadPx + DoctorHeaderPx

        If pt.Y < gridTop OrElse pt.X < TimeGutterPx Then
            _dragTargetTime = DateTime.MinValue
            _dragTargetDrId = -1
        Else
            Dim relY = pt.Y - gridTop
            Dim totalMins = CInt(relY / _pxPerMinute)
            totalMins = (totalMins \ 15) * 15

            Dim day = If(_request?.State?.CurrentDate.Date, Date.Today)
            Dim workStart = day.Add(_request.State.WorkStartTime)
            _dragTargetTime = workStart.AddMinutes(totalMins)

            ' Doctor detection — use same column order as RenderDay
            Dim relX = pt.X - TimeGutterPx
            Dim innerW = _canvas.Width - TimeGutterPx - 4
            Dim docCount = If(_dayColumnDoctorIds IsNot Nothing AndAlso _dayColumnDoctorIds.Count > 0, _dayColumnDoctorIds.Count, 1)
            Dim colW = Math.Max(MinDoctorColPx, innerW \ docCount)
            Dim docIdx = relX \ colW
            docIdx = Math.Max(0, Math.Min(docIdx, docCount - 1))
            If _dayColumnDoctorIds IsNot Nothing AndAlso docIdx < _dayColumnDoctorIds.Count Then
                _dragTargetDrId = _dayColumnDoctorIds(docIdx)
            Else
                _dragTargetDrId = -1
            End If
        End If

        _canvas.Invalidate()
    End Sub

    Private Sub Canvas_DragLeave(sender As Object, e As EventArgs)
        _dragTargetTime = DateTime.MinValue
        _dragTargetDrId = -1
        _canvas.Invalidate()
    End Sub

    Private Sub Canvas_DragDrop(sender As Object, e As DragEventArgs)
        Dim appt = TryCast(e.Data.GetData("Appointment"), AppointmentC)
        If appt Is Nothing OrElse _dragTargetTime = DateTime.MinValue Then Return

        Dim duration = appt.EndDateTime - appt.StartDateTime
        Dim newStart = _dragTargetTime
        Dim newEnd = newStart.Add(duration)

        If _dragTargetDrId <> -1 AndAlso _dragTargetDrId <> appt.DrID Then
            appt.DrID = _dragTargetDrId
        End If

        If InteractionHub IsNot Nothing Then
            InteractionHub.PublishAppointmentTimeChange(appt, newStart, newEnd)
        End If

        _dragTargetTime = DateTime.MinValue
        _dragTargetDrId = -1
        _canvas.Invalidate()
    End Sub

    Private Sub HeaderBand_Paint(sender As Object, e As PaintEventArgs)
        Dim g = e.Graphics
        Using pSep As New Pen(Color.FromArgb(200, 210, 224), 2)
            g.DrawLine(pSep, 12, _headerBand.Height - 1, Math.Max(12, _headerBand.Width - 12), _headerBand.Height - 1)
        End Using

        Using f10 = CreateCalibriFont(10.0F, FontStyle.Bold)
        Using f14 = CreateCalibriFont(14.0F, FontStyle.Bold)
            Dim lead = If(_headerWeekdayLead, "")
            Dim core = If(_headerDateCore, "")
            Dim tail = If(_headerMetaTail, "")
            Dim colLead = Color.FromArgb(72, 82, 98)
            Dim colCore = Color.FromArgb(28, 38, 54)
            Dim colTail = Color.FromArgb(95, 105, 125)

            Dim pad = 4
            Dim szL = TextRenderer.MeasureText(lead, f10)
            Dim szC = TextRenderer.MeasureText(core, f14)
            Dim szT = TextRenderer.MeasureText(tail, f10)
            Dim total = szL.Width + If(String.IsNullOrEmpty(lead), 0, pad) + szC.Width + If(String.IsNullOrEmpty(tail), 0, pad) + szT.Width
            Dim checkReserve = 0
            If _chkShowAllDoctors IsNot Nothing AndAlso _chkShowAllDoctors.Visible Then
                checkReserve = _chkShowAllDoctors.Width + 24
            End If
            Dim xStart = checkReserve + Math.Max(0, (_headerBand.Width - checkReserve - 8 - total) \ 2)
            If xStart < checkReserve + 4 Then xStart = checkReserve + 4

            Dim rBandH = _headerBand.Height - 2
            Dim flags = TextFormatFlags.Left Or TextFormatFlags.VerticalCenter Or TextFormatFlags.SingleLine Or TextFormatFlags.NoPadding
            If _headerRtl Then flags = flags Or TextFormatFlags.RightToLeft

            If Not String.IsNullOrEmpty(lead) Then
                TextRenderer.DrawText(g, lead, f10, New Rectangle(xStart, 0, szL.Width + 1, rBandH), colLead, flags)
                xStart += szL.Width + pad
            End If
            If Not String.IsNullOrEmpty(core) Then
                TextRenderer.DrawText(g, core, f14, New Rectangle(xStart, 0, szC.Width + 1, rBandH), colCore, flags)
                xStart += szC.Width + pad
            End If
            If Not String.IsNullOrEmpty(tail) Then
                TextRenderer.DrawText(g, tail, f10, New Rectangle(xStart, 0, szT.Width + 2, rBandH), colTail, flags)
            End If
        End Using
        End Using
    End Sub

    Private Sub ScrollHost_Resize(sender As Object, e As EventArgs)
        If _canvas Is Nothing Then Return
        FitCanvasWidth()
    End Sub

    Public Property InteractionHub As ApptInteractionHub Implements IApptViewCtl.InteractionHub

    Public Sub BindData(request As ApptViewRequest) Implements IApptViewCtl.BindData
        _request = request
        RenderDay()
        If request IsNot Nothing Then TryScrollToAppointment(request.PendingScrollAppointment)
    End Sub

    Private Sub FitCanvasWidth()
        If _scrollHost Is Nothing OrElse _canvas Is Nothing Then Return
        Dim w = Math.Max(400, _scrollHost.ClientSize.Width - _scrollHost.Padding.Horizontal)
        If _canvas.Width <> w Then _canvas.Width = w
    End Sub

    Private Sub ResetTimelineCanvasModel()
        If _canvas Is Nothing Then Return
        Dim blankNames As New List(Of String) From {If(Eng, "Schedule", "الجدول")}
        Dim blankCols As New List(Of Color) From {Color.FromArgb(241, 244, 250)}
        _canvas.SetModel(Date.Today, Date.Today.AddHours(1), 60, _pxPerMinute, 1, MinDoctorColPx, blankNames, blankCols)
        _canvas.Height = 80
    End Sub

    ''' <summary>Doctors with appointments for this day first (by name), then the rest (by name) when "All doctors" is on; otherwise only doctors with appointments.</summary>
    Private Function BuildDoctorsDayColumnOrder(
        state As ApptState,
        data As ApptDataBundle,
        dayAppts As List(Of AppointmentC),
        groups As IEnumerable(Of IGrouping(Of Integer, AppointmentC))) As List(Of Integer)

        If state Is Nothing OrElse data Is Nothing Then Return New List(Of Integer)()
        If state.DoctorFilterId.HasValue AndAlso state.DoctorFilterId.Value > 0 Then
            Return New List(Of Integer) From {state.DoctorFilterId.Value}
        End If
        If _chkShowAllDoctors IsNot Nothing AndAlso _chkShowAllDoctors.Checked AndAlso data.DoctorInfos IsNot Nothing AndAlso data.DoctorInfos.Count > 0 Then
            Dim withAppt = ApptTheme.OrderDoctorColumnIdsForDisplay(dayAppts.Select(Function(a) a.DrID).Distinct(), data, linkedDoctorAtEnd:=True)
            Dim withSet As New HashSet(Of Integer)(withAppt)
            Dim without = ApptTheme.OrderDoctorColumnIdsForDisplay(data.DoctorInfos.Keys.Where(Function(id) Not withSet.Contains(id)), data, linkedDoctorAtEnd:=True)
            Return withAppt.Concat(without).ToList()
        End If
        Return ApptTheme.OrderDoctorColumnIdsForDisplay(groups.Select(Function(g) g.Key), data, linkedDoctorAtEnd:=True)
    End Function

    Private Sub RenderDay()
        If _canvas Is Nothing Then Return
        _canvas.SuspendLayout()
        Try
            _canvas.Controls.Clear()
            If _request Is Nothing OrElse _request.State Is Nothing OrElse _request.Data Is Nothing Then
                _headerWeekdayLead = ""
                _headerDateCore = ""
                _headerMetaTail = ""
                _dayColumnDoctorIds = Nothing
                ResetTimelineCanvasModel()
                _headerBand.Invalidate()
                _canvas.Invalidate()
                Return
            End If

            Dim state = _request.State
            Dim data = _request.Data
            Dim day = state.CurrentDate.Date

            _headerRtl = Not Eng
            _headerBand.RightToLeft = If(_headerRtl, RightToLeft.Yes, RightToLeft.No)
            _chkShowAllDoctors.Text = If(Eng, "All doctors", "جميع الأطباء")

            Dim dayApptsAll = If(data.Appointments, New List(Of AppointmentC)()).Where(
                Function(a) ApptTheme.AppointmentCalendarDayInInclusiveRange(a, day, day) AndAlso
                ApptTheme.AppointmentMatchesApptStateFilters(a, state)).ToList()
            Dim count = dayApptsAll.Count
            Dim meta = If(Eng,
                $"{count} appointment{If(count <> 1, "s", "")} · {FormatWorkHoursSubtitle(state)}",
                $"{count} موعد · {FormatWorkHoursSubtitle(state)}")
            _headerWeekdayLead = day.ToString("dddd") & ", "
            _headerDateCore = day.ToString("dd MMM yyyy")
            _headerMetaTail = " · " & meta
            _headerBand.Invalidate()

            Dim workStart = day.Add(state.WorkStartTime)
            Dim workEnd = day.Add(state.WorkEndTime)
            If workEnd <= workStart Then workEnd = workStart.AddHours(1)
            Dim totalMin = CInt(Math.Ceiling((workEnd - workStart).TotalMinutes))

            Dim filteredDoctor = state.DoctorFilterId.HasValue AndAlso state.DoctorFilterId.Value > 0
            _chkShowAllDoctors.Enabled = Not filteredDoctor

            Dim byDr = dayApptsAll.GroupBy(Function(a) a.DrID).ToDictionary(Function(g) g.Key, Function(g) g)
            Dim gKeys = ApptTheme.OrderDoctorColumnIdsForDisplay(byDr.Keys, data, linkedDoctorAtEnd:=True)
            Dim groups = gKeys.Select(Function(k) byDr(k)).ToList()
            If filteredDoctor Then
                Dim fid = state.DoctorFilterId.Value
                Dim byDrF = dayApptsAll.Where(Function(a) a.DrID = fid).GroupBy(Function(a) a.DrID).ToDictionary(Function(g) g.Key, Function(g) g)
                gKeys = ApptTheme.OrderDoctorColumnIdsForDisplay(byDrF.Keys, data, linkedDoctorAtEnd:=True)
                groups = gKeys.Select(Function(k) byDrF(k)).ToList()
            End If

            Dim columnDrIds = BuildDoctorsDayColumnOrder(state, data, dayApptsAll, groups)
            _dayColumnDoctorIds = columnDrIds

            FitCanvasWidth()
            Dim innerW = Math.Max(200, _canvas.Width)
            Dim docCount = Math.Max(1, columnDrIds.Count)
            Dim colW = Math.Max(MinDoctorColPx, (innerW - TimeGutterPx - 4) \ docCount)
            Dim needW = TimeGutterPx + 4 + colW * docCount
            If needW > _canvas.Width Then _canvas.Width = needW

            Dim gridTop = CanvasTopPadPx + DoctorHeaderPx
            Dim colNames As New List(Of String)()
            Dim colColors As New List(Of Color)()
            If columnDrIds.Count = 0 Then
                If filteredDoctor Then
                    Dim di0 = data.ResolveDoctor(state.DoctorFilterId.Value)
                    colNames.Add(If(di0 Is Nothing, "", di0.DrName))
                    Dim raw0 = If(di0 Is Nothing, Color.LightSteelBlue, di0.DrColor)
                    colColors.Add(BlendCardBackFromAccent(raw0))
                Else
                    colNames.Add(If(Eng, "Schedule", "الجدول"))
                    colColors.Add(Color.FromArgb(241, 244, 250))
                End If
            Else
                For Each drKey In columnDrIds
                    Dim di = data.ResolveDoctor(drKey)
                    colNames.Add(If(di Is Nothing, "", di.DrName))
                    Dim raw = If(di Is Nothing, Color.LightSteelBlue, di.DrColor)
                    colColors.Add(BlendCardBackFromAccent(raw))
                Next
            End If

            _canvas.SetModel(workStart, workEnd, totalMin, _pxPerMinute, docCount, colW, colNames, colColors)

            If count = 0 Then
                Dim empty As New LabelControl With {
                    .AutoSizeMode = LabelAutoSizeMode.None,
                    .Width = Math.Max(200, _canvas.Width - 40),
                    .Height = 48,
                    .Location = New Point(TimeGutterPx + 20, gridTop + 36),
                    .Text = If(Eng, "No appointments this day — double-click a time to add.", "لا مواعيد اليوم — انقر نقرًا مزدوجًا لإضافة موعد.")
                }
                empty.Appearance.Font = CreateCalibriFont(10.5F, FontStyle.Italic)
                empty.Appearance.ForeColor = Color.FromArgb(130, 138, 152)
                empty.Appearance.Options.UseFont = True
                empty.Appearance.Options.UseForeColor = True
                _canvas.Controls.Add(empty)
            End If

            For gi = 0 To columnDrIds.Count - 1
                Dim colDrId = columnDrIds(gi)
                Dim colLeft = TimeGutterPx + gi * colW
                Dim appts = dayApptsAll.Where(Function(a) a.DrID = colDrId).OrderBy(Function(a) a.StartDateTime).ToList()
                Dim laneEnds As New List(Of DateTime)()

                For Each ap In appts
                    Dim startT = If(ap.StartDateTime < workStart, workStart, ap.StartDateTime)
                    Dim endT = If(ap.EndDateTime > workEnd, workEnd, ap.EndDateTime)
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

                    Dim yStack = rowIndex * LaneStackPx
                    Dim topF = CSng((startT - workStart).TotalMinutes * _pxPerMinute) + gridTop + yStack
                    Dim durMin = Math.Max(1, (endT - startT).TotalMinutes)
                    Dim cardH = CInt(durMin * _pxPerMinute) ' No -2 here, match ApptDayCtl logic

                    Dim model As New ApptCardVm With {
                        .Appointment = ap,
                        .PatientName = data.ResolvePatientName(ap.PatientID),
                        .DoctorInfo = data.ResolveDoctor(ap.DrID)
                    }
                    model.Appearance = BuildDefaultCardAppearance(model, state)
                    If _request.AppointmentAppearanceSelector IsNot Nothing Then
                        Dim sel = _request.AppointmentAppearanceSelector(model)
                        If sel IsNot Nothing Then model.Appearance = sel
                    End If

                    Dim card As New HtmlCard With {
                        .Left = colLeft + CardInsetPx,
                        .Top = CInt(topF),
                        .Width = Math.Max(120, colW - CardInsetPx * 2),
                        .Height = cardH
                    }
                    card.Bind(model, state.Use24HourFormat)
                    
                    AddHandler card.AppointmentClicked, Sub(a) If InteractionHub IsNot Nothing Then InteractionHub.PublishAppointmentClicked(a)
                    AddHandler card.AppointmentDoubleClicked, Sub(a) If InteractionHub IsNot Nothing Then InteractionHub.PublishAppointmentDoubleClicked(a)
                    AddHandler card.StatusContextEditRequested, Sub(a) If InteractionHub IsNot Nothing Then InteractionHub.PublishAppointmentDoubleClicked(a)
                    AddHandler card.StatusChangeRequested,
                        Sub(a, key, col)
                            If InteractionHub IsNot Nothing Then InteractionHub.PublishAppointmentStatusChange(a, key, col)
                        End Sub
                    
                    AddHandler card.CardDragMouseDown, Sub(c, ev) OnCardDragMouseDown(c, day, ev)
                    AddHandler card.CardDragMouseUp, Sub(c, ev) OnCardDragMouseUp(c, ev)
                    AddHandler card.CardDragMouseMove, Sub(c, ev) OnCardDragMouseMove(c, ev)

                    _canvas.Controls.Add(card)
                    card.BringToFront()
                Next
            Next

            Dim maxBottom = gridTop + CInt(totalMin * _pxPerMinute) + 20
            For Each c As Control In _canvas.Controls
                maxBottom = Math.Max(maxBottom, c.Bottom + 8)
            Next
            _canvas.Height = maxBottom
            _canvas.Invalidate()
        Finally
            _canvas.ResumeLayout(True)
        End Try
    End Sub

    Private Shared Function FormatWorkHoursSubtitle(state As ApptState) As String
        If state Is Nothing Then Return ""
        Dim a = FormatAppointmentTime(Date.Today.Add(state.WorkStartTime), state.Use24HourFormat)
        Dim b = FormatAppointmentTime(Date.Today.Add(state.WorkEndTime), state.Use24HourFormat)
        Return $"{a} – {b}"
    End Function

    Private Sub TryScrollToAppointment(ap As AppointmentC)
        If ap Is Nothing OrElse _scrollHost Is Nothing OrElse _canvas Is Nothing Then Return
        For Each c As Control In _canvas.Controls
            Dim card = TryCast(c, HtmlCard)
            If card IsNot Nothing AndAlso card.BoundAppointment Is ap Then
                _scrollHost.ScrollControlIntoView(card)
                Return
            End If
        Next
    End Sub

    Friend Sub NotifyEmptySlotDoubleClick(minutesFromStart As Integer)
        If InteractionHub Is Nothing OrElse _request Is Nothing OrElse _request.State Is Nothing Then Return
        Dim day = _request.State.CurrentDate.Date
        Dim workStart = day.Add(_request.State.WorkStartTime)
        Dim t = workStart.AddMinutes(SnapToGrid(minutesFromStart, SnapMinutes))
        InteractionHub.PublishEmptyDateInvoked(t)
    End Sub

    Private Shared Function SnapToGrid(mins As Integer, grid As Integer) As Integer
        If grid <= 0 Then Return mins
        Return CInt(Math.Round(mins / CDbl(grid))) * grid
    End Function

    ''' <summary>Paints hour grid, half-hour wash, column rules, time labels, and optional “now” line.</summary>
    Private NotInheritable Class DayDoctorsTimelineCanvas
        Inherits Panel

        Private ReadOnly _host As ApptDayDoctors
        Private _workStart As DateTime
        Private _workEnd As DateTime
        Private _totalMin As Integer
        Private _pxPm As Single
        Private _docCount As Integer
        Private _colW As Integer
        Private _gridTop As Integer
        Private _columnNames As List(Of String)
        Private _columnBackColors As List(Of Color)

        Public Sub New(host As ApptDayDoctors)
            _host = host
            _columnNames = New List(Of String)()
            _columnBackColors = New List(Of Color)()
            SetStyle(ControlStyles.AllPaintingInWmPaint Or ControlStyles.UserPaint Or ControlStyles.OptimizedDoubleBuffer Or ControlStyles.ResizeRedraw, True)
            DoubleBuffered = True
        End Sub

        Public Sub SetModel(workStart As DateTime, workEnd As DateTime, totalMin As Integer, pxPm As Single, docCount As Integer, colW As Integer, columnNames As IReadOnlyList(Of String), columnBackColors As IReadOnlyList(Of Color))
            _workStart = workStart
            _workEnd = workEnd
            _totalMin = totalMin
            _pxPm = pxPm
            _docCount = Math.Max(1, docCount)
            _colW = colW
            _gridTop = CanvasTopPadPx + DoctorHeaderPx
            _columnNames = If(columnNames IsNot Nothing, New List(Of String)(columnNames), New List(Of String)())
            _columnBackColors = If(columnBackColors IsNot Nothing, New List(Of Color)(columnBackColors), New List(Of Color)())
        End Sub

        Protected Overrides Sub OnPaint(e As PaintEventArgs)
            MyBase.OnPaint(e)
            Dim g = e.Graphics
            g.SmoothingMode = SmoothingMode.AntiAlias
            Dim r = ClientRectangle
            Using br As New SolidBrush(BackColor)
                g.FillRectangle(br, r)
            End Using

            Using pTop As New Pen(Color.FromArgb(205, 214, 228), 1)
                g.DrawLine(pTop, 0, 0, Width, 0)
            End Using

            Dim hdrTop = CanvasTopPadPx
            Using brGutter As New SolidBrush(Color.FromArgb(243, 246, 251))
                g.FillRectangle(brGutter, 0, hdrTop, TimeGutterPx, DoctorHeaderPx)
            End Using
            For ci = 0 To _docCount - 1
                Dim xCol = TimeGutterPx + ci * _colW
                Dim back = If(ci < _columnBackColors.Count, _columnBackColors(ci), Color.FromArgb(241, 244, 250))
                Using brCol As New SolidBrush(back)
                    g.FillRectangle(brCol, xCol, hdrTop, _colW, DoctorHeaderPx)
                End Using
            Next
            Using pHead As New Pen(ColumnHairline, 1)
                g.DrawLine(pHead, 0, hdrTop + DoctorHeaderPx - 1, Width, hdrTop + DoctorHeaderPx - 1)
            End Using
            For csep = 1 To _docCount - 1
                Dim xs = TimeGutterPx + csep * _colW
                Using pv As New Pen(ColumnHairline, 1)
                    g.DrawLine(pv, xs, hdrTop, xs, hdrTop + DoctorHeaderPx)
                End Using
            Next

            Using fCorner = CreateCalibriFont(8.25F, FontStyle.Bold)
                Dim timeLab = If(Eng, "Time", "الوقت")
                TextRenderer.DrawText(g, timeLab, fCorner, New Rectangle(6, hdrTop + 4, TimeGutterPx - 12, DoctorHeaderPx - 6), Color.FromArgb(72, 82, 98),
                    TextFormatFlags.Left Or TextFormatFlags.VerticalCenter Or TextFormatFlags.EndEllipsis)
            End Using
            For ci = 0 To _docCount - 1
                Dim colTitle = If(ci < _columnNames.Count, _columnNames(ci), "")
                Dim xCol = TimeGutterPx + ci * _colW
                Dim back = If(ci < _columnBackColors.Count, _columnBackColors(ci), Color.FromArgb(241, 244, 250))
                Dim tcol = GetReadableForeColor(back)
                Using fc = CreateCalibriFont(8.75F, FontStyle.Bold)
                    TextRenderer.DrawText(g, colTitle, fc, New Rectangle(xCol + 6, hdrTop + 4, _colW - 12, DoctorHeaderPx - 6), tcol,
                        TextFormatFlags.HorizontalCenter Or TextFormatFlags.VerticalCenter Or TextFormatFlags.SingleLine Or TextFormatFlags.EndEllipsis)
                End Using
            Next

            Dim y0 = _gridTop

            ' Half-hour calming bands (whole-hour rows alternate)
            Dim halfCount = CInt(Math.Ceiling(_totalMin / 30.0R))
            For i = 0 To halfCount - 1
                If i Mod 2 = 1 Then
                    Dim yBand = y0 + CSng(i * 30) * _pxPm
                    Dim rh = CSng(30) * _pxPm
                    Using br As New SolidBrush(ApptDayDoctors.HalfHourBand)
                        g.FillRectangle(br, TimeGutterPx, CInt(yBand), Math.Max(1, Width - TimeGutterPx), CInt(Math.Max(1.0F, rh)))
                    End Using
                End If
            Next

            ' Half-hour ticks and faint guide lines (between hours)
            For hm = 30 To _totalMin - 1 Step 30
                If hm Mod 60 = 0 Then Continue For
                Dim yh = y0 + CSng(hm) * _pxPm
                Using pHalf As New Pen(ApptDayDoctors.HalfHourLine, 1.0F)
                    g.DrawLine(pHalf, TimeGutterPx + 2, yh, Width, yh)
                End Using
                Using pTk As New Pen(ApptDayDoctors.TickHalf, 1.0F)
                    g.DrawLine(pTk, TimeGutterPx - 6, yh, TimeGutterPx, yh)
                End Using
            Next

            ' Hour lines, gutter ticks, bold labels
            Dim t = _workStart
            While t < _workEnd.AddSeconds(1)
                Dim minsH = CInt((t - _workStart).TotalMinutes)
                Dim y = y0 + CSng(minsH) * _pxPm
                Using p As New Pen(ApptDayDoctors.HourLine, 1.35F)
                    g.DrawLine(p, TimeGutterPx, y, Width, y)
                End Using
                Using pTk As New Pen(ApptDayDoctors.TickHour, 1.35F)
                    g.DrawLine(pTk, TimeGutterPx - 11, y, TimeGutterPx, y)
                End Using
                Dim tf = If(_host._request IsNot Nothing AndAlso _host._request.State IsNot Nothing AndAlso _host._request.State.Use24HourFormat,
                    "HH:mm", "hh:mm tt")
                Dim lab = t.ToString(tf)
                Using f = CreateCalibriFont(9.5F, FontStyle.Bold)
                    TextRenderer.DrawText(g, lab, f, New Rectangle(2, CInt(y - 12), TimeGutterPx - 4, 24), Color.FromArgb(48, 58, 76),
                        TextFormatFlags.Right Or TextFormatFlags.VerticalCenter Or TextFormatFlags.SingleLine)
                End Using
                t = t.AddHours(1)
            End While

            For c = 1 To _docCount - 1
                Dim x = TimeGutterPx + c * _colW
                Using p As New Pen(ApptDayDoctors.ColumnHairline, 1)
                    g.DrawLine(p, x, hdrTop, x, Height)
                End Using
            Next

            If Date.Today = _workStart.Date AndAlso Date.Now >= _workStart AndAlso Date.Now <= _workEnd Then
                Dim nowM = CSng((Date.Now - _workStart).TotalMinutes)
                Dim yn = y0 + nowM * _pxPm
                Using p As New Pen(ApptDayDoctors.NowLine, 1.6F)
                    p.DashStyle = DashStyle.Dot
                    g.DrawLine(p, TimeGutterPx, yn, Width, yn)
                End Using
                Using br As New SolidBrush(ApptDayDoctors.NowLine)
                    g.FillEllipse(br, TimeGutterPx - 4, yn - 4, 8, 8)
                End Using
            End If

            ' --- DND/Resize Ghost Highlight ---
            If Not _host._ghostRect.IsEmpty Then
                Using pTarget As New Pen(Color.Red, 3.0F)
                    g.DrawRectangle(pTarget, _host._ghostRect)
                End Using
            End If
        End Sub

        Protected Overrides Sub OnMouseDoubleClick(e As MouseEventArgs)
            MyBase.OnMouseDoubleClick(e)
            If e.Button <> MouseButtons.Left Then Return
            If e.X <= TimeGutterPx Then Return
            For Each c As Control In Controls
                If TypeOf c Is HtmlCard AndAlso c.Bounds.Contains(e.Location) Then Return
            Next
            If e.Y < _gridTop Then Return
            Dim relY = e.Y - _gridTop
            Dim mins = CInt(relY / _pxPm)
            mins = SnapToGrid(mins, SnapMinutes)
            Dim maxM = Math.Max(0, _totalMin - SnapMinutes)
            mins = Math.Max(0, Math.Min(mins, maxM))
            _host.NotifyEmptySlotDoubleClick(mins)
        End Sub
    End Class
End Class
