Imports System
Imports System.Collections.Generic
Imports System.Drawing
Imports System.Linq
Imports System.Windows.Forms
Imports DevExpress.XtraEditors

''' <summary>
''' <see cref="ApptViewMode.DayView"/>: 30-minute slot rows with borders, time gutter (~70px), optional doctor caption strip,
''' transparent doctor columns for empty double-clicks, and time-scaled <see cref="ApptCardCtl"/> blocks
''' (same layout model as <see cref="SchedulerNew.RenderDayView"/>). To use <see cref="HtmlCard"/> instead, replace
''' <c>ApptCardCtl</c> with <c>HtmlCard</c> here and in <see cref="ApptDayDoctors"/>; week view uses <see cref="ApptWeekCtl"/>.
''' </summary>
Public Class ApptDayCtl
    Inherits XtraUserControl
    Implements IApptViewCtl

    ''' <summary>Same default as <see cref="SchedulerNew._slotHeight"/> — pixels per 30-minute slot.</summary>
    Private Const SlotHeightPx As Integer = 60
    Private Const TimeGutterPx As Integer = 70
    Private Const MinDoctorColPx As Integer = 140
    Private Const GridOriginY As Integer = 44
    Private Const DoctorStripHeightPx As Integer = 28
    Private Const CardInsetPx As Integer = 6
    Private Const OverlapLanePx As Integer = 6

    Private _request As ApptViewRequest
    Private _scrollHost As Panel
    Private _content As Panel
    Private _banner As Panel
    Private _lblBanner As Label

    ' --- Drag/Resize State ---
    Private _dragSourceCard As ApptCardCtl
    Private _dragSourceAppt As AppointmentC
    Private _dragSourceDay As Date?
    Private ReadOnly _holdTimer As New Timer With {.Enabled = False}
    
    Private _resizingCard As ApptCardCtl
    Private _resizeEdge As ApptCardCtl.CardEdge
    Private _resizeStartTime As DateTime
    Private _resizeEndTime As DateTime
    Private _resizeStartPt As Point
    
    Private _dragTargetTime As DateTime = DateTime.MinValue
    Private _dragTargetDrId As Integer = -1

    Private _ghostRect As Rectangle = Rectangle.Empty
    Private _draggingDuration As TimeSpan = TimeSpan.Zero

    Private _pxPerMin As Single

    Private ReadOnly _scrollResizeDebounce As Timer

    Public Sub New()
        Appearance.BackColor = Color.FromArgb(245, 247, 250)
        Appearance.Options.UseBackColor = True
        DoubleBuffered = True

        _scrollHost = New Panel With {
            .Dock = DockStyle.Fill,
            .AutoScroll = True,
            .BackColor = Color.FromArgb(240, 242, 246),
            .Padding = New Padding(8, 8, 8, 10),
            .RightToLeft = RightToLeft.No
        }

        _content = New Panel With {
            .BackColor = Color.White,
            .BorderStyle = BorderStyle.FixedSingle,
            .RightToLeft = RightToLeft.No,
            .AllowDrop = True
        }
        AddHandler _content.Paint, AddressOf Content_Paint
        _scrollHost.Controls.Add(_content)

        _banner = New Panel With {
            .Height = GridOriginY,
            .BackColor = Color.FromArgb(248, 250, 252),
            .RightToLeft = RightToLeft.No
        }
        _lblBanner = New Label With {
            .AutoSize = False,
            .Dock = DockStyle.Fill,
            .TextAlign = ContentAlignment.MiddleCenter,
            .Font = CreateCalibriFont(11.0F, FontStyle.Bold),
            .ForeColor = Color.FromArgb(38, 48, 64),
            .BackColor = Color.Transparent
        }
        _banner.Controls.Add(_lblBanner)

        _scrollResizeDebounce = New Timer() With {.Enabled = False, .Interval = 28}
        AddHandler _scrollResizeDebounce.Tick, AddressOf ScrollResizeDebounce_Tick
        AddHandler _holdTimer.Tick, AddressOf HoldTimer_Tick
        AddHandler _content.DragEnter, AddressOf Content_DragEnter
        AddHandler _content.DragOver, AddressOf Content_DragOver
        AddHandler _content.DragLeave, AddressOf Content_DragLeave
        AddHandler _content.DragDrop, AddressOf Content_DragDrop

        RightToLeft = RightToLeft.No
        Controls.Add(_scrollHost)
        AddHandler _scrollHost.Resize, AddressOf ScrollHost_Resize
    End Sub

    Public Property InteractionHub As ApptInteractionHub Implements IApptViewCtl.InteractionHub

    Public Sub BindData(request As ApptViewRequest) Implements IApptViewCtl.BindData
        _request = request
        RenderTimeline()
        If request IsNot Nothing Then TryScrollToAppointment(request.PendingScrollAppointment)
    End Sub

    ''' <summary>HTML export with the same day-timeline model as this control (time column + per-doctor columns, time-scaled cards).</summary>
    Friend Function BuildHtmlExportContext(caption As String) As WeekSnapshotHtmlContext
        If _request Is Nothing OrElse _request.State Is Nothing OrElse _request.Data Is Nothing Then Return Nothing
        Return ApptSnapshotHtmlBuilder.BuildFromViewRequestData(_request, caption, linkedDoctorAtEnd:=True)
    End Function

    Private Sub ScrollHost_Resize(sender As Object, e As EventArgs)
        ' FitContentWidth() alone is not enough: slot rows, doctor columns, and card bounds are
        ' set only in RenderTimeline. Debounce a full remeasure so maximize/drag is responsive.
        _scrollResizeDebounce.Stop()
        _scrollResizeDebounce.Interval = 28
        _scrollResizeDebounce.Start()
    End Sub

    Private Sub ScrollResizeDebounce_Tick(sender As Object, e As EventArgs)
        _scrollResizeDebounce.Stop()
        If IsDisposed OrElse _scrollHost Is Nothing OrElse _content Is Nothing Then Return
        Dim sp = _scrollHost.AutoScrollPosition
        RenderTimeline()
        ' Restore scroll: getter returns negative offsets; setter expects positive.
        _scrollHost.AutoScrollPosition = New Point(-sp.X, -sp.Y)
    End Sub

    Private Sub FitContentWidth()
        If _scrollHost Is Nothing OrElse _content Is Nothing Then Return
        Dim w = Math.Max(520, _scrollHost.ClientSize.Width - _scrollHost.Padding.Horizontal)
        If _content.Width <> w Then _content.Width = w
    End Sub

    Private Sub RenderTimeline()
        If _content Is Nothing Then Return
        _content.SuspendLayout()
        Try
            _content.Controls.Clear()
            if _request Is Nothing OrElse _request.State Is Nothing OrElse _request.Data Is Nothing Then Return

            Dim state = _request.State
            Dim data = _request.Data
            Dim day = state.CurrentDate.Date

            FitContentWidth()
            Dim dayW = _content.Width

            Dim workStart = day.Add(state.WorkStartTime)
            Dim workEnd = day.Add(state.WorkEndTime)
            If workEnd <= workStart Then workEnd = workStart.AddHours(1)

            Dim startMinTotal = CInt(workStart.TimeOfDay.TotalMinutes)
            Dim snapStart = (startMinTotal \ 30) * 30
            Dim gridStart = day.Date.AddMinutes(snapStart)
            Dim totalMin = Math.Max(30, CInt(Math.Ceiling((workEnd - gridStart).TotalMinutes)))
            Dim totalSlots = Math.Max(1, CInt(Math.Ceiling(totalMin / 30.0R)))

            _pxPerMin = CSng(SlotHeightPx / 30.0F)
            Dim cardGridTop = GridOriginY + DoctorStripHeightPx
            Dim gridPixelHeight = totalSlots * SlotHeightPx

            _lblBanner.Text = FormatCaptionDayFull(day)
            _banner.SetBounds(0, 0, dayW, GridOriginY)
            _content.Controls.Add(_banner)

            Dim dayApptsAll = If(data.Appointments, New List(Of AppointmentC)()).Where(
                Function(a) ApptTheme.AppointmentCalendarDayInInclusiveRange(a, day, day) AndAlso
                ApptTheme.AppointmentMatchesApptStateFilters(a, state)).ToList()
            Dim byDr0 = dayApptsAll.GroupBy(Function(a) a.DrID).ToDictionary(Function(g) g.Key, Function(g) g)
            Dim gKeys0 = ApptTheme.OrderDoctorColumnIdsForDisplay(byDr0.Keys, data, linkedDoctorAtEnd:=True)
            Dim groups = gKeys0.Select(Function(k) byDr0(k)).ToList()
            If state.DoctorFilterId.HasValue AndAlso state.DoctorFilterId.Value > 0 Then
                Dim fid = state.DoctorFilterId.Value
                Dim byDrF = dayApptsAll.Where(Function(a) a.DrID = fid).GroupBy(Function(a) a.DrID).ToDictionary(Function(g) g.Key, Function(g) g)
                gKeys0 = ApptTheme.OrderDoctorColumnIdsForDisplay(byDrF.Keys, data, linkedDoctorAtEnd:=True)
                groups = gKeys0.Select(Function(k) byDrF(k)).ToList()
            End If

            Const overlapRowSpacing As Integer = 6
            Dim maxStackH As Integer = 0
            For Each grp In groups
                If grp IsNot Nothing AndAlso grp.Count() > 1 Then
                    maxStackH = Math.Max(maxStackH, (grp.Count() - 1) * overlapRowSpacing)
                End If
            Next
            Dim contentH = cardGridTop + gridPixelHeight + 8 + maxStackH

            Dim docCount = Math.Max(1, groups.Count)
            Dim scheduleW = Math.Max(MinDoctorColPx * docCount, dayW - TimeGutterPx - 8)
            Dim doctorW = Math.Max(MinDoctorColPx, scheduleW \ docCount)
            Dim baseLeft = TimeGutterPx

            ' --- 30-minute slot rows
            For i = 0 To totalSlots - 1
                Dim slotStart = gridStart.AddMinutes(30 * i)
                Dim slotPanel As New Panel With {
                    .Height = SlotHeightPx,
                    .Width = dayW,
                    .Left = 0,
                    .Top = GridOriginY + i * SlotHeightPx,
                    .BorderStyle = BorderStyle.FixedSingle,
                    .Tag = slotStart,
                    .BackColor = Color.Transparent
                }
                Dim lbl As New Label With {
                    .AutoSize = False,
                    .Width = TimeGutterPx - 6,
                    .Left = 2,
                    .Height = slotPanel.Height,
                    .Top = 0,
                    .TextAlign = ContentAlignment.MiddleCenter,
                    .ForeColor = Color.Gray,
                    .BackColor = Color.Transparent,
                    .Font = CreateCalibriFont(8.75F, FontStyle.Bold),
                    .Text = If(state.Use24HourFormat, slotStart.ToString("HH:mm"), slotStart.ToString("hh:mm tt"))
                }
                slotPanel.Controls.Add(lbl)
                AddHandler slotPanel.DoubleClick, AddressOf SlotPanel_DoubleClick
                AddHandler lbl.DoubleClick, AddressOf SlotPanel_DoubleClick
                AddHandler slotPanel.Paint, AddressOf SlotPanel_Paint
                _content.Controls.Add(slotPanel)
            Next

            ' --- Doctor captions + column hit targets
            For gi = 0 To groups.Count - 1
                Dim grp = groups(gi)
                Dim di = data.ResolveDoctor(grp.Key)
                Dim colLeft = baseLeft + gi * doctorW
                Dim cap As New Label With {
                    .AutoSize = False,
                    .Text = If(di Is Nothing, $"Dr {grp.Key}", di.DrName),
                    .Font = CreateCalibriFont(8.75F, FontStyle.Bold),
                    .ForeColor = Color.FromArgb(52, 62, 82),
                    .BackColor = Color.FromArgb(236, 240, 246),
                    .TextAlign = ContentAlignment.MiddleCenter,
                    .BorderStyle = BorderStyle.FixedSingle,
                    .Bounds = New Rectangle(colLeft, GridOriginY, doctorW - 8, DoctorStripHeightPx)
                }
                _content.Controls.Add(cap)

                Dim colPanel As New Panel With {
                    .Width = doctorW - 8,
                    .Height = gridPixelHeight + maxStackH,
                    .Left = colLeft,
                    .Top = cardGridTop,
                    .BackColor = Color.Transparent,
                    .Tag = New DoctorColumnTag With {.DrId = grp.Key, .GridStart = gridStart, .TotalSlots = totalSlots}
                }
                AddHandler colPanel.DoubleClick, AddressOf DoctorColumn_DoubleClick
                _content.Controls.Add(colPanel)
            Next

            If groups.Count = 0 Then
                Dim fid = If(state.DoctorFilterId.HasValue, state.DoctorFilterId.Value, 0)
                Dim colPanel As New Panel With {
                    .Width = doctorW - 8,
                    .Height = gridPixelHeight + maxStackH,
                    .Left = baseLeft,
                    .Top = cardGridTop,
                    .BackColor = Color.Transparent,
                    .Tag = New DoctorColumnTag With {.DrId = fid, .GridStart = gridStart, .TotalSlots = totalSlots}
                }
                AddHandler colPanel.DoubleClick, AddressOf DoctorColumn_DoubleClick
                _content.Controls.Add(colPanel)
            End If

            ' --- Appointment cards
            Dim workStartDay = day.Add(state.WorkStartTime)
            Dim workEndDay = day.Add(state.WorkEndTime)
            If workEndDay <= workStartDay Then workEndDay = workStartDay.AddHours(1)

            For gi = 0 To groups.Count - 1
                Dim grp = groups(gi)
                Dim colLeft = baseLeft + gi * doctorW
                Dim appts = grp.OrderBy(Function(a) a.StartDateTime).ToList()
                Dim laneEnds As New List(Of DateTime)()

                For Each ap In appts
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

                    Dim startOffsetMins = CSng((startT - gridStart).TotalMinutes)
                    Dim durMin = CSng(Math.Max(1, (endT - startT).TotalMinutes))
                    Dim topPx = CInt(startOffsetMins * _pxPerMin) + cardGridTop + rowIndex * OverlapLanePx
                    Dim hPx = Math.Max(48, CInt(durMin * _pxPerMin) - CardInsetPx * 2)
                    Dim cardW = doctorW - 8 - CardInsetPx * 2
                    Dim cardLeft = colLeft + (doctorW - 8 - cardW) \ 2

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

                    Dim card As New ApptCardCtl With {
                        .Left = cardLeft,
                        .Top = topPx,
                        .Width = Math.Max(80, cardW),
                        .Height = hPx
                    }
                    card.Bind(model, state.Use24HourFormat)
                    WireCard(card, day)
                    _content.Controls.Add(card)
                    card.BringToFront()
                Next
            Next

            _content.Height = contentH
        Finally
            _content.ResumeLayout(True)
        End Try
    End Sub

    Private Sub WireCard(card As ApptCardCtl, day As Date)
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
    End Sub

    Private Sub OnCardDragMouseDown(card As ApptCardCtl, day As Date, e As MouseEventArgs)
        If e.Button <> MouseButtons.Left Then Return
        If card.BoundAppointment Is Nothing OrElse card.BoundAppointment.AppointmentID <= 0 Then Return

        Dim edge = card.GetEdgeAt(e.Location)
        If edge <> ApptCardCtl.CardEdge.None Then
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

    Private Sub OnCardDragMouseMove(card As ApptCardCtl, e As MouseEventArgs)
        If _resizingCard Is card Then
            PerformResize(card, e)
            card.Cursor = Cursors.SizeNS ' Force resize cursor during operation
            Return
        End If

        ' Update cursor for resize hit-test
        Dim edge = card.GetEdgeAt(e.Location)
        card.Cursor = If(edge <> ApptCardCtl.CardEdge.None, Cursors.SizeNS, Cursors.Hand)
    End Sub

    Private Sub PerformResize(card As ApptCardCtl, e As MouseEventArgs)
        Dim currentPt = card.PointToScreen(e.Location)
        Dim diffY = currentPt.Y - _resizeStartPt.Y
        
        ' Snap logical time to 5 mins
        Dim diffMinsSnapped = (CInt(diffY / _pxPerMin) \ 5) * 5

        Dim newStart = _resizeStartTime
        Dim newEnd = _resizeEndTime

        If _resizeEdge = ApptCardCtl.CardEdge.Top Then
            newStart = _resizeStartTime.AddMinutes(diffMinsSnapped)
            If newStart >= newEnd.AddMinutes(-5) Then newStart = newEnd.AddMinutes(-5)
        Else
            newEnd = _resizeEndTime.AddMinutes(diffMinsSnapped)
            If newEnd <= newStart.AddMinutes(5) Then newEnd = newStart.AddMinutes(5)
        End If

        ' Update Ghost Rect for visual feedback (Smooth motion for visual, snapped for calculation)
        Dim day = _request.State.CurrentDate.Date
        Dim startMinTotal = CInt(_request.State.WorkStartTime.TotalMinutes)
        Dim snapStart = (startMinTotal \ 30) * 30
        Dim gridStart = day.AddMinutes(snapStart)
        Dim cardGridTop = GridOriginY + DoctorStripHeightPx

        ' Calculate exact visual Y/H for "smooth motion" feel
        Dim visualDiffY = CSng(diffY)
        Dim yGhost As Single
        Dim hGhost As Single

        If _resizeEdge = ApptCardCtl.CardEdge.Top Then
            yGhost = CSng((_resizeStartTime - gridStart).TotalMinutes) * _pxPerMin + cardGridTop + visualDiffY
            hGhost = CSng((_resizeEndTime - _resizeStartTime).TotalMinutes) * _pxPerMin - visualDiffY
        Else
            yGhost = CSng((_resizeStartTime - gridStart).TotalMinutes) * _pxPerMin + cardGridTop
            hGhost = CSng((_resizeEndTime - _resizeStartTime).TotalMinutes) * _pxPerMin + visualDiffY
        End If
        
        ' Clamp height to minimum
        If hGhost < 10 Then hGhost = 10 

        _ghostRect = New Rectangle(card.Left, CInt(yGhost), card.Width, CInt(hGhost))
        _content.Invalidate()
    End Sub

    Private Sub OnCardDragMouseUp(card As ApptCardCtl, e As MouseEventArgs)
        _holdTimer.Stop()

        If _resizingCard Is card Then
            CommitResize(card, e)
            _resizingCard = Nothing
            _ghostRect = Rectangle.Empty
            card.IndicatorColor = Color.Transparent
            _content.Invalidate()
            Return
        End If

        _dragSourceCard = Nothing
        _dragSourceAppt = Nothing
        _dragSourceDay = Nothing
        _ghostRect = Rectangle.Empty
        card.IndicatorColor = Color.Transparent
        _content.Invalidate()
    End Sub

    Private Sub CommitResize(card As ApptCardCtl, e As MouseEventArgs)
        Dim currentPt = card.PointToScreen(e.Location)
        Dim diffY = currentPt.Y - _resizeStartPt.Y
        Dim diffMins = CInt(diffY / _pxPerMin)
        diffMins = (diffMins \ 5) * 5

        Dim newStart = _resizeStartTime
        Dim newEnd = _resizeEndTime

        If _resizeEdge = ApptCardCtl.CardEdge.Top Then
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
            _ghostRect = Rectangle.Empty
            _content.Invalidate()
        End Try
    End Sub

    Private Sub Content_DragEnter(sender As Object, e As DragEventArgs)
        If e.Data.GetDataPresent("Appointment") Then
            e.Effect = DragDropEffects.Move
        Else
            e.Effect = DragDropEffects.None
        End If
    End Sub

    Private Sub Content_DragOver(sender As Object, e As DragEventArgs)
        If Not e.Data.GetDataPresent("Appointment") Then Return
        
        Dim pt = _content.PointToClient(New Point(e.X, e.Y))
        Dim cardGridTop = GridOriginY + DoctorStripHeightPx
        
        If pt.Y < cardGridTop OrElse pt.X < TimeGutterPx Then
            _dragTargetTime = DateTime.MinValue
            _dragTargetDrId = -1
            _ghostRect = Rectangle.Empty
        Else
            ' Time detection
            Dim relY = pt.Y - cardGridTop
            Dim totalMins = CInt(relY / _pxPerMin)
            ' Snap to 5 mins for smoother dragging
            totalMins = (totalMins \ 5) * 5
            
            Dim day = If(_request?.State?.CurrentDate.Date, Date.Today)
            Dim startMinTotal = CInt(_request.State.WorkStartTime.TotalMinutes)
            Dim snapStart = (startMinTotal \ 30) * 30
            Dim gridStart = day.AddMinutes(snapStart)
            _dragTargetTime = gridStart.AddMinutes(totalMins)

            ' Doctor detection
            Dim relX = pt.X - TimeGutterPx
            Dim dayW = _content.Width
            Dim colPanels = _content.Controls.OfType(Of Panel).Where(Function(p) p.Tag IsNot Nothing AndAlso TypeOf p.Tag Is DoctorColumnTag).ToList()
            Dim docCount = Math.Max(1, colPanels.Count)
            Dim scheduleW = Math.Max(MinDoctorColPx * docCount, dayW - TimeGutterPx - 8)
            Dim doctorW = Math.Max(MinDoctorColPx, scheduleW \ docCount)
            
            Dim docIdx = relX \ doctorW
            docIdx = Math.Max(0, Math.Min(docIdx, docCount - 1))
            
            If docIdx < colPanels.Count Then
                Dim tag = DirectCast(colPanels(docIdx).Tag, DoctorColumnTag)
                _dragTargetDrId = tag.DrId
                
                ' Calculate Ghost Rect for visual feedback (same size as purple card)
                Dim x = TimeGutterPx + docIdx * doctorW + CardInsetPx
                Dim w = doctorW - 8 - CardInsetPx * 2
                Dim yGhost = CSng((_dragTargetTime - gridStart).TotalMinutes) * _pxPerMin + cardGridTop
                Dim hGhost = CSng(_draggingDuration.TotalMinutes) * _pxPerMin
                _ghostRect = New Rectangle(x, CInt(yGhost), w, CInt(hGhost))
            Else
                _dragTargetDrId = -1
                _ghostRect = Rectangle.Empty
            End If
        End If

        _content.Invalidate()
    End Sub

    Private Sub Content_DragLeave(sender As Object, e As EventArgs)
        _dragTargetTime = DateTime.MinValue
        _dragTargetDrId = -1
        _ghostRect = Rectangle.Empty
        _content.Invalidate()
    End Sub

    Private Sub Content_DragDrop(sender As Object, e As DragEventArgs)
        Dim appt = TryCast(e.Data.GetData("Appointment"), AppointmentC)
        If appt Is Nothing OrElse _dragTargetTime = DateTime.MinValue Then Return

        Dim duration = appt.EndDateTime - appt.StartDateTime
        Dim newStart = _dragTargetTime
        Dim newEnd = newStart.Add(duration)

        ' Update DrID if changed
        If _dragTargetDrId <> -1 AndAlso _dragTargetDrId <> appt.DrID Then
            appt.DrID = _dragTargetDrId
        End If

        If InteractionHub IsNot Nothing Then
            InteractionHub.PublishAppointmentTimeChange(appt, newStart, newEnd)
        End If

        _dragTargetTime = DateTime.MinValue
        _dragTargetDrId = -1
        _ghostRect = Rectangle.Empty
        _content.Invalidate()
    End Sub

    Private Sub Content_Paint(sender As Object, e As PaintEventArgs)
        If _ghostRect.IsEmpty Then Return
        Using p As New Pen(Color.Red, 2.5F)
            e.Graphics.DrawRectangle(p, _ghostRect)
        End Using
    End Sub

    Private Sub SlotPanel_Paint(sender As Object, e As PaintEventArgs)
        ' Redundant now that Content_Paint handles ghost rect
    End Sub

    Private Sub RelayWeekDragDown(card As ApptCardCtl, day As Date, e As MouseEventArgs)
        ' Redirected to OnCardDragMouseDown
    End Sub

    Private Sub RelayWeekDragUp(card As ApptCardCtl, e As MouseEventArgs)
        ' Redirected to OnCardDragMouseUp
    End Sub

    Private Sub LineDragTimer_Tick(sender As Object, e As EventArgs)
        ' Redirected to HoldTimer_Tick
    End Sub

    Private Sub SlotPanel_DoubleClick(sender As Object, e As EventArgs)
        Dim p = TryCast(sender, Panel)
        If p Is Nothing Then
            Dim lb = TryCast(sender, Label)
            If lb?.Parent IsNot Nothing Then p = TryCast(lb.Parent, Panel)
        End If
        If p?.Tag Is Nothing OrElse InteractionHub Is Nothing Then Return
        Dim t = CType(p.Tag, DateTime)
        InteractionHub.PublishEmptyDateInvoked(t)
    End Sub

    Private NotInheritable Class DoctorColumnTag
        Public DrId As Integer
        Public GridStart As DateTime
        Public TotalSlots As Integer
    End Class

    Private Sub DoctorColumn_DoubleClick(sender As Object, e As EventArgs)
        If InteractionHub Is Nothing Then Return
        Dim col = TryCast(sender, Panel)
        If col?.Tag Is Nothing Then Return
        Dim tag = DirectCast(col.Tag, DoctorColumnTag)
        Dim pt = col.PointToClient(Cursor.Position)
        Dim slotIndex = pt.Y \ SlotHeightPx
        If slotIndex < 0 OrElse slotIndex >= tag.TotalSlots Then Return
        Dim slotTime = tag.GridStart.AddMinutes(30 * slotIndex)
        InteractionHub.PublishEmptyDateInvoked(slotTime)
    End Sub

    Private Sub TryScrollToAppointment(ap As AppointmentC)
        If ap Is Nothing OrElse _scrollHost Is Nothing OrElse _content Is Nothing Then Return
        For Each c As Control In _content.Controls
            Dim card = TryCast(c, ApptCardCtl)
            If card IsNot Nothing AndAlso card.BoundAppointment Is ap Then
                _scrollHost.ScrollControlIntoView(card)
                Return
            End If
        Next
    End Sub

    Protected Overrides Sub Dispose(disposing As Boolean)
        If disposing AndAlso _scrollResizeDebounce IsNot Nothing Then
            RemoveHandler _scrollResizeDebounce.Tick, AddressOf ScrollResizeDebounce_Tick
            _scrollResizeDebounce.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub
End Class
