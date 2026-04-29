Imports System
Imports System.Collections.Generic
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Linq
Imports System.Runtime.CompilerServices
Imports System.Windows.Forms
Imports DevExpress.Utils
Imports DevExpress.XtraEditors
Imports DevExpress.XtraEditors.Controls

''' <summary>
''' Month views: magazine-style grid tiles, weekly rows with sparklines, slide-over day drawer (no popup forms).
''' </summary>
Public Class ApptMonthCtl
    Inherits XtraUserControl
    Implements IApptViewCtl

    Friend Const AppointmentTimeFormat As String = "hh:mm tt"
    Private Shared ReadOnly BrandAccent As Color = Color.FromArgb(0, 151, 167)
    ''' <summary>Month grid background (day cells).</summary>
    Private Shared ReadOnly MonthCalendarGridBack As Color = Color.FromArgb(238, 240, 245)
    ''' <summary>Month title strip — matches work area so it does not read as a separate gray band.</summary>
    Private Shared ReadOnly MonthCalendarTitleBack As Color = Color.FromArgb(250, 251, 253)

    Private _request As ApptViewRequest
    Private _selectedApptId As Integer = -1
    Private ReadOnly _monthWeekToolTip As New ToolTip With {.AutomaticDelay = 280, .AutoPopDelay = 8000}
    Private ReadOnly _mwDragTimer As New Timer()
    Private _mwDragSourceList As ListBox
    Private _mwDragStartPoint As Point
    Private _mwDragInitiated As Boolean
    Private Const DragMoveThreshold As Integer = 6

    Private _monthRoot As Panel
    Private _workArea As Panel
    Private _dimOverlay As MonthDrawerDimPanel
    Private _dayDrawerHost As MonthDayDrawerHost
    Private _drawerAnimTimer As Timer
    Private _drawerAnimTargetW As Integer
    Private _calendarTableHost As TableLayoutPanel
    Private ReadOnly _monthWeekHostResizeDebounce As Timer

    Friend Property DragSourceDay As Date = Date.MinValue
    Friend Property DragTargetDay As Date = Date.MinValue

    Public Sub New()
        Appearance.BackColor = Color.White
        Appearance.Options.UseBackColor = True
        DoubleBuffered = True
        _mwDragTimer.Enabled = False
        AddHandler _mwDragTimer.Tick, AddressOf MwDragTimer_Tick
        _drawerAnimTimer = New Timer With {.Interval = 15}
        AddHandler _drawerAnimTimer.Tick, AddressOf DrawerAnimTimer_Tick
        _monthWeekHostResizeDebounce = New Timer() With {.Enabled = False, .Interval = 32}
        AddHandler _monthWeekHostResizeDebounce.Tick, AddressOf MonthWeekHostResize_Tick
        AddHandler Me.Resize, AddressOf ApptMonthCtl_HostResize
    End Sub

    Public Property InteractionHub As ApptInteractionHub Implements IApptViewCtl.InteractionHub

    Friend Sub RefreshAllDayBorders()
        InvalidateBorders(Me)
    End Sub

    Private Sub InvalidateBorders(parent As Control)
        If parent Is Nothing Then Return
        For Each c As Control In parent.Controls
            If TypeOf c Is MonthTilePanel OrElse (TypeOf c Is Panel AndAlso c.Tag IsNot Nothing AndAlso TypeOf c.Tag Is Date) Then
                c.Invalidate()
                c.Update() ' Force immediate paint before blocking DoDragDrop
            End If
            InvalidateBorders(c)
        Next
    End Sub

    Friend Sub UpdateInternalSelection(apptId As Integer)
        If _selectedApptId = apptId Then Return
        _selectedApptId = apptId

        SyncSelectionAcrossControls(Me)
    End Sub

    Private Sub SyncSelectionAcrossControls(parent As Control)
        If parent Is Nothing Then Return

        For Each c As Control In parent.Controls
            If TypeOf c Is ListBox Then
                Dim lb = DirectCast(c, ListBox)
                Dim found = False
                If _selectedApptId > 0 Then
                    For i = 0 To lb.Items.Count - 1
                        Dim item = TryCast(lb.Items(i), MonthWeekApptItem)
                        If item IsNot Nothing AndAlso item.Ap IsNot Nothing AndAlso item.Ap.AppointmentID = _selectedApptId Then
                            lb.SelectedIndex = i
                            found = True
                            Exit For
                        End If
                    Next
                End If
                If Not found Then lb.SelectedIndex = -1
                lb.Invalidate()
            ElseIf TypeOf c Is MonthTilePanel Then
                c.Invalidate()
            Else
                SyncSelectionAcrossControls(c)
            End If
        Next
    End Sub
    Public Sub BindData(request As ApptViewRequest) Implements IApptViewCtl.BindData
        CloseDayDrawerImmediate()
        _request = request
        _selectedApptId = -1
        Controls.Clear()
        _monthRoot = Nothing
        _workArea = Nothing
        _dimOverlay = Nothing
        _dayDrawerHost = Nothing
        _calendarTableHost = Nothing

        If request Is Nothing OrElse request.State Is Nothing OrElse request.Data Is Nothing Then Return

        BuildMonthChrome()

        Select Case request.State.CurrentView
            Case ApptViewMode.MonthlyWeek
                RenderMonthWeeksView()
            Case ApptViewMode.MonthView
                RenderMonthCalendarView()
            Case Else
                RenderMonthCalendarView()
        End Select
    End Sub

    Private Sub BuildMonthChrome()
        _monthRoot = New Panel With {.Dock = DockStyle.Fill, .BackColor = Color.FromArgb(250, 251, 253)}
        Controls.Add(_monthRoot)

        _workArea = New Panel With {.Dock = DockStyle.Fill, .BackColor = Color.FromArgb(250, 251, 253)}
        _monthRoot.Controls.Add(_workArea)

        _dayDrawerHost = New MonthDayDrawerHost(Me) With {
            .Dock = If(Eng, DockStyle.Right, DockStyle.Left),
            .Width = 0,
            .Visible = False,
            .BackColor = DrawerDayPanelWash
        }
        _monthRoot.Controls.Add(_dayDrawerHost)

        _dimOverlay = New MonthDrawerDimPanel(Me) With {.Dock = DockStyle.Fill, .Visible = False}
        _workArea.Controls.Add(_dimOverlay)
    End Sub

    Friend Sub RequestCloseDayDrawer()
        CloseDayDrawerAnimated()
    End Sub

    Private Const DayDrawerWidthPx As Integer = 400

    Friend Sub ShowMonthDayDrawer(day As DateTime, appts As List(Of AppointmentC))
        If _request Is Nothing OrElse appts Is Nothing Then Return
        If _dayDrawerHost Is Nothing OrElse _dimOverlay Is Nothing OrElse _monthRoot Is Nothing Then Return
        _drawerAnimTimer.Stop()
        _dayDrawerHost.SuspendLayout()
        Try
            ' Width must be set before Populate so scroll ClientSize and rows lay out on first open.
            _dayDrawerHost.Visible = True
            _dayDrawerHost.Width = DayDrawerWidthPx
            _dimOverlay.Visible = True
            _dimOverlay.BringToFront()
            _monthRoot.PerformLayout()
            _workArea.PerformLayout()
            _dayDrawerHost.PerformLayout()
            _dayDrawerHost.BringToFront()
            _dayDrawerHost.Populate(day, ApptTheme.OrderAppointmentsForDisplay(appts, _request.Data, linkedDoctorAtEnd:=True), _request.Data, _request.State, GetStandardAppointmentStatusColors())
        Finally
            _dayDrawerHost.ResumeLayout(True)
        End Try
        _dayDrawerHost.Refresh()
        _dayDrawerHost.Focus()
        EnsureDayDrawerOnTop()
        BeginInvoke(New MethodInvoker(Sub()
                                          If _dayDrawerHost IsNot Nothing AndAlso _dayDrawerHost.Visible Then
                                              _dayDrawerHost.RelayoutAfterOpen()
                                              EnsureDayDrawerOnTop()
                                          End If
                                      End Sub))
    End Sub

    Private Sub DrawerAnimTimer_Tick(sender As Object, e As EventArgs)
        _drawerAnimTimer.Stop()
    End Sub

    Private Sub CloseDayDrawerAnimated()
        CloseDayDrawerImmediate()
    End Sub

    Private Sub CloseDayDrawerImmediate()
        _drawerAnimTimer.Stop()
        If _dayDrawerHost IsNot Nothing Then
            _dayDrawerHost.Visible = False
            _dayDrawerHost.Width = 0
        End If
        If _dimOverlay IsNot Nothing Then _dimOverlay.Visible = False
    End Sub

    Private Sub EnsureDayDrawerOnTop()
        If _dayDrawerHost IsNot Nothing AndAlso _dayDrawerHost.Parent IsNot Nothing Then
            _dayDrawerHost.BringToFront()
        End If
    End Sub

#Region "Monthly week (horizontal week rows)"
    Private Sub ClearWorkAreaForMonthWeekRebuild()
        If _workArea Is Nothing OrElse _dimOverlay Is Nothing Then Return
        For i = _workArea.Controls.Count - 1 To 0 Step -1
            Dim c = _workArea.Controls(i)
            If c Is _dimOverlay Then Continue For
            _workArea.Controls.Remove(c)
            c.Dispose()
        Next
    End Sub

    Private Sub ApptMonthCtl_HostResize(sender As Object, e As EventArgs)
        If _request Is Nothing OrElse _request.State Is Nothing Then Return
        If _request.State.CurrentView <> ApptViewMode.MonthlyWeek Then Return
        _monthWeekHostResizeDebounce.Stop()
        _monthWeekHostResizeDebounce.Interval = 32
        _monthWeekHostResizeDebounce.Start()
    End Sub

    Private Sub MonthWeekHostResize_Tick(sender As Object, e As EventArgs)
        _monthWeekHostResizeDebounce.Stop()
        If IsDisposed OrElse Not IsHandleCreated Then Return
        If _request Is Nothing OrElse _request.State Is Nothing OrElse _request.Data Is Nothing Then Return
        If _request.State.CurrentView <> ApptViewMode.MonthlyWeek Then Return
        If _workArea Is Nothing OrElse _dimOverlay Is Nothing Then Return
        RenderMonthWeeksView()
        If _selectedApptId > 0 Then
            SyncSelectionAcrossControls(Me)
        End If
    End Sub

    Private Sub RenderMonthWeeksView()
        ClearWorkAreaForMonthWeekRebuild()
        Dim data = _request.Data
        Dim state = _request.State
        Dim currentDate = state.CurrentDate

        Dim statusColors = GetStandardAppointmentStatusColors()

        Dim apptsAll = If(data.Appointments, New List(Of AppointmentC)())
        If apptsAll.Count = 0 Then
            Dim emptyLbl As New LabelControl With {
                .Dock = DockStyle.Fill,
                .AutoSizeMode = LabelAutoSizeMode.None,
                .Text = If(Eng, "No appointments to display.", "لا توجد مواعيد للعرض.")
            }
            emptyLbl.Appearance.Font = CreateCalibriFont(11.0F, FontStyle.Bold)
            emptyLbl.Appearance.Options.UseFont = True
            emptyLbl.Appearance.TextOptions.HAlignment = HorzAlignment.Center
            emptyLbl.Appearance.TextOptions.VAlignment = VertAlignment.Center
            emptyLbl.Appearance.Options.UseTextOptions = True
            _workArea.Controls.Add(emptyLbl)
            emptyLbl.BringToFront()
            Return
        End If

        Dim firstOfMonth = New DateTime(currentDate.Year, currentDate.Month, 1)
        Dim lastOfMonth = firstOfMonth.AddMonths(1).AddDays(-1)
        Dim startOfFirstWeek = firstOfMonth.AddDays(-CInt(firstOfMonth.DayOfWeek))
        Dim endOfMonthRange = lastOfMonth.AddDays(7 - CInt(lastOfMonth.DayOfWeek) - 1)
        Dim today = Date.Today

        Dim weekCount = 0
        Dim wIter = startOfFirstWeek
        Do While wIter <= endOfMonthRange
            weekCount += 1
            wIter = wIter.AddDays(7)
        Loop

        Dim mainFlow As New FlowLayoutPanel With {
            .Dock = DockStyle.Fill,
            .AutoScroll = True,
            .FlowDirection = FlowDirection.TopDown,
            .WrapContents = False,
            .Padding = New Padding(10, 8, 10, 10),
            .BackColor = Color.FromArgb(250, 251, 253)
        }
        Dim mainPadVert = mainFlow.Padding.Top + mainFlow.Padding.Bottom
        Dim bodyHAvail = Math.Max(weekCount * 92, ClientSize.Height - mainPadVert - 4)
        Const weekHeaderBarH = 34
        Const weekShellInnerPad = 6
        Dim dayBoxH = CInt(bodyHAvail / Math.Max(1, weekCount)) - weekHeaderBarH - weekShellInnerPad
        dayBoxH = Math.Max(68, Math.Min(280, dayBoxH))
        Dim weekShellH = dayBoxH + weekHeaderBarH + weekShellInnerPad
        Dim grpWeekContentW = Math.Max(320, ClientSize.Width - mainFlow.Padding.Horizontal - 8)

        Dim dayColors As Color() = {
            MonthVisual.Soften(Color.FromArgb(255, 230, 230)),
            MonthVisual.Soften(Color.FromArgb(255, 245, 220)),
            MonthVisual.Soften(Color.FromArgb(240, 255, 230)),
            MonthVisual.Soften(Color.FromArgb(230, 250, 255)),
            MonthVisual.Soften(Color.FromArgb(245, 230, 255)),
            MonthVisual.Soften(Color.FromArgb(255, 255, 230)),
            MonthVisual.Soften(Color.FromArgb(230, 240, 255))
        }

        Dim weekIdx = 0
        Dim weekStart = startOfFirstWeek
        Do While weekStart <= endOfMonthRange
            Dim weekEndEx = weekStart.AddDays(7)
            Dim weekLastDay = weekStart.AddDays(6)
            Dim weekAppts = apptsAll.Where(Function(a) a.StartDateTime.Date >= weekStart AndAlso a.StartDateTime.Date < weekEndEx).ToList()
            Dim isCurrentWeek = (today >= weekStart AndAlso today < weekEndEx)

            Dim wkTheme = GetMonthWeekColorTheme(weekIdx, isCurrentWeek)
            Dim bandBg = wkTheme.BodyBg
            Dim capHi = wkTheme.HeaderHi
            Dim capLo = wkTheme.HeaderLo

            Dim weekShell As New Panel With {
                .Width = grpWeekContentW,
                .Height = weekShellH,
                .BackColor = wkTheme.ShellBorder,
                .Padding = New Padding(1),
                .Margin = New Padding(0, 0, 0, 10)
            }
            Dim weekClient As New Panel With {.Dock = DockStyle.Fill, .BackColor = bandBg, .Padding = New Padding(0)}

            Dim dayCounts(6) As Integer
            For i = 0 To 6
                Dim d = weekStart.AddDays(i)
                dayCounts(i) = apptsAll.Where(Function(a) ApptTheme.GetAppointmentCalendarDay(a) = d.Date).Count()
            Next

            Dim headerRow As New Panel With {
                .Dock = DockStyle.Fill,
                .BackColor = MonthWeeksCaptionAverageColor(capHi, capLo)
            }
            Dim wkRange = FormatCaptionWeekRange(weekStart, weekLastDay)
            Dim lblWeekTitle As New Label With {
                .Dock = DockStyle.Fill,
                .Padding = New Padding(8, 4, 8, 4),
                .Font = CreateCalibriFont(9.0F, FontStyle.Bold),
                .ForeColor = Color.FromArgb(48, 55, 72),
                .BackColor = Color.Transparent,
                .TextAlign = ContentAlignment.MiddleLeft,
                .Text = If(Eng,
                    $"{wkRange}   ·   {weekAppts.Count} appt{If(weekAppts.Count <> 1, "s", "")}",
                    $"{wkRange}   ·   {weekAppts.Count} موعد")
            }
            If Not Eng Then lblWeekTitle.RightToLeft = RightToLeft.Yes
            Dim spark As New WeekSparklinePanel(dayCounts, wkTheme.SparkGradientHi, wkTheme.SparkGradientLo) With {
                .Dock = DockStyle.Fill,
                .Padding = New Padding(0, 5, 8, 5)
            }
            Dim headerGrid As New TableLayoutPanel With {
                .Dock = DockStyle.Fill,
                .BackColor = Color.Transparent,
                .ColumnCount = 2,
                .RowCount = 1,
                .Margin = New Padding(0)
            }
            headerGrid.RowStyles.Add(New RowStyle(SizeType.Percent, 100.0F))
            Const sparkColW = 118.0F
            headerGrid.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 100.0F))
            headerGrid.ColumnStyles.Add(New ColumnStyle(SizeType.Absolute, sparkColW))
            headerGrid.Controls.Add(lblWeekTitle, 0, 0)
            headerGrid.Controls.Add(spark, 1, 0)
            headerRow.Controls.Add(headerGrid)
            Dim linePen = wkTheme.HeaderRuleColor
            AddHandler headerRow.Paint,
                Sub(s, pe)
                    Using p As New Pen(linePen, 1)
                        pe.Graphics.DrawLine(p, 0, headerRow.Height - 1, headerRow.Width, headerRow.Height - 1)
                    End Using
                End Sub

            Dim apptListSurface = wkTheme.ListSurface

            Dim flow As New FlowLayoutPanel With {
                .Dock = DockStyle.Fill,
                .AutoScroll = False,
                .WrapContents = False,
                .Padding = New Padding(2, 0, 2, 2),
                .BackColor = bandBg,
                .Margin = New Padding(0)
            }
            Dim weekStack As New TableLayoutPanel With {
                .Dock = DockStyle.Fill,
                .ColumnCount = 1,
                .RowCount = 2,
                .BackColor = bandBg,
                .Margin = New Padding(0)
            }
            weekStack.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 100.0F))
            weekStack.RowStyles.Add(New RowStyle(SizeType.Absolute, CSng(weekHeaderBarH)))
            weekStack.RowStyles.Add(New RowStyle(SizeType.Percent, 100.0F))

            For i = 0 To 6
                Dim day = weekStart.AddDays(i)
                Dim dayAppts = ApptTheme.OrderAppointmentsForDisplay(apptsAll.Where(Function(a) ApptTheme.GetAppointmentCalendarDay(a) = day.Date), data, linkedDoctorAtEnd:=True)

                Dim dayBox As New Panel With {
                    .Width = CInt((grpWeekContentW - 72) / 7),
                    .Height = dayBoxH,
                    .BorderStyle = BorderStyle.None,
                    .BackColor = Color.Transparent,
                    .Tag = day,
                    .Margin = New Padding(1, 0, 1, 0)
                }
                MonthVisual.PaintWeekDayBoxBorder(Me, dayBox, day, dayAppts.Count, dayColors(i Mod dayColors.Length))

                Const dayPadH = 4
                Const dayPadTop = 0
                Const gapLabelToList = 2
                Dim dayLblW = Math.Max(1, dayBox.Width - dayPadH * 2)

                Dim preview = String.Join(vbCrLf, dayAppts.Take(4).Select(Function(a)
                                                                              Dim d = FormatMonthPreviewReasonNotes(a, state)
                                                                              Dim mid = If(String.IsNullOrWhiteSpace(d), "", $" · {d}")
                                                                              Return $"{a.StartDateTime.ToString(AppointmentTimeFormat)} {data.ResolvePatientName(a.PatientID)}{mid} · {GetAppointmentStatusDisplayText(a)}"
                                                                          End Function))
                If String.IsNullOrWhiteSpace(preview) Then preview = If(Eng, "No appointments", "لا توجد مواعيد")

                Dim dayDow As String
                If Eng Then
                    dayDow = day.ToString("ddd").ToUpperInvariant()
                Else
                    dayDow = If(day.Date = today, "اليوم", day.ToString("ddd"))
                End If
                Dim dayLine = $"{dayDow} · " &
                    If(Eng,
                       $"{dayAppts.Count} appt{If(dayAppts.Count <> 1, "s", "")}",
                       $"{dayAppts.Count} موعد")
                Dim dayHdrFont As Font = If(day.Date = today,
                    CreateCalibriFont(8.25F, FontStyle.Bold Or FontStyle.Italic),
                    CreateCalibriFont(8.25F, FontStyle.Bold))
                Dim dayLblSz = MeasureSingleLineLabelSize(dayLine, dayHdrFont, dayLblW, 0, 2)
                Dim lblDay As New Label With {
                    .Text = dayLine,
                    .Size = dayLblSz,
                    .Location = New Point(dayPadH, dayPadTop),
                    .TextAlign = ContentAlignment.MiddleCenter,
                    .BackColor = Color.Transparent,
                    .Font = dayHdrFont,
                    .ForeColor = If(day.Date = today, BrandAccent, Color.FromArgb(55, 65, 80))
                }
                If Not Eng Then lblDay.RightToLeft = RightToLeft.Yes
                lblDay.Cursor = Cursors.Hand
                AddHandler lblDay.MouseClick,
                    Sub(s, ev)
                        If ev.Button <> MouseButtons.Left Then Return
                        ShowMonthDayDrawer(day, dayAppts.ToList())
                    End Sub
                dayBox.Controls.Add(lblDay)
                _monthWeekToolTip.SetToolTip(lblDay, preview)
                Dim listTop = lblDay.Bottom + gapLabelToList

                Dim listH = Math.Max(40, dayBox.Height - listTop - dayPadH)
                Dim lst As New ListBox With {
                    .Left = dayPadH,
                    .Top = listTop,
                    .Width = dayLblW,
                    .Height = listH,
                    .BorderStyle = BorderStyle.None,
                    .BackColor = apptListSurface,
                    .Tag = day,
                    .AllowDrop = True,
                    .ScrollAlwaysVisible = True,
                    .DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed,
                    .IntegralHeight = True,
                    .Font = CreateCalibriFont(8.75F, FontStyle.Bold),
                    .Cursor = Cursors.Hand,
                    .ItemHeight = 30
                }

                For Each ap In dayAppts
                    Dim patientName = data.ResolvePatientName(ap.PatientID)
                    Dim display = $"{ap.StartDateTime.ToString(AppointmentTimeFormat)}  {patientName}"
                    lst.Items.Add(New MonthWeekApptItem(ap, display))
                Next

                WireMonthWeeksChipDrawItem(lst, apptListSurface, statusColors)

                AddHandler lst.MouseClick,
                    Sub(s, ev)
                        If ev.Button <> MouseButtons.Left Then Return
                        Dim lb = DirectCast(s, ListBox)
                        If lb.SelectedItem Is Nothing Then Return
                        Dim ai = TryCast(lb.SelectedItem, MonthWeekApptItem)
                        If ai IsNot Nothing Then
                            UpdateInternalSelection(ai.Ap.AppointmentID)
                            If InteractionHub IsNot Nothing Then InteractionHub.PublishAppointmentClicked(ai.Ap)
                        End If
                    End Sub

                AddHandler lst.DoubleClick,
                    Sub(s, ev)
                        Dim lb = DirectCast(s, ListBox)
                        If lb.SelectedItem Is Nothing Then Return
                        Dim ai = TryCast(lb.SelectedItem, MonthWeekApptItem)
                        If ai IsNot Nothing AndAlso InteractionHub IsNot Nothing Then
                            InteractionHub.PublishAppointmentDoubleClicked(ai.Ap)
                        End If
                    End Sub

                AddHandler lst.MouseDown,
                    Sub(s, e)
                        Dim lb = DirectCast(s, ListBox)
                        Dim idx = lb.IndexFromPoint(e.Location)
                        If idx >= 0 AndAlso idx < lb.Items.Count Then
                            lb.SelectedIndex = idx
                            Dim ai = TryCast(lb.Items(idx), MonthWeekApptItem)
                            If ai IsNot Nothing Then UpdateInternalSelection(ai.Ap.AppointmentID)
                        End If

                        If e.Button = MouseButtons.Right Then
                            If idx >= 0 AndAlso idx < lb.Items.Count Then
                                Dim aiR = TryCast(lb.Items(idx), MonthWeekApptItem)
                                If aiR IsNot Nothing Then ShowListStatusContextMenu(lb, aiR.Ap, statusColors, e)
                            End If
                            Return
                        End If

                        If e.Button = MouseButtons.Left AndAlso lb.SelectedItem IsNot Nothing Then
                            _mwDragSourceList = lb
                            _mwDragStartPoint = e.Location
                            _mwDragInitiated = False
                            _mwDragTimer.Stop()
                            _mwDragTimer.Interval = Math.Max(200, If(_request IsNot Nothing, _request.DragHoldTimeMs, 750))
                            _mwDragTimer.Start()
                        Else
                            _mwDragSourceList = Nothing
                            _mwDragTimer.Stop()
                        End If
                    End Sub

                AddHandler lst.MouseMove,
                    Sub(s, e)
                        If _mwDragSourceList Is Nothing Then Return
                        If _mwDragInitiated Then Return
                        If Math.Abs(e.Location.X - _mwDragStartPoint.X) > DragMoveThreshold OrElse Math.Abs(e.Location.Y - _mwDragStartPoint.Y) > DragMoveThreshold Then
                            _mwDragTimer.Stop()
                            _mwDragSourceList = Nothing
                        End If
                    End Sub

                AddHandler lst.MouseUp,
                    Sub(s, e)
                        If Not _mwDragInitiated Then
                            _mwDragTimer.Stop()
                            _mwDragSourceList = Nothing
                        End If
                    End Sub

                AddHandler lst.DragEnter,
                    Sub(s, e)
                        If e.Data.GetDataPresent(GetType(MonthWeekApptItem)) OrElse e.Data.GetDataPresent(GetType(AppointmentC)) OrElse e.Data.GetDataPresent("Appointment") Then
                            e.Effect = DragDropEffects.Move
                            DragTargetDay = day
                            RefreshAllDayBorders()
                        Else
                            e.Effect = DragDropEffects.None
                        End If
                    End Sub

                AddHandler lst.DragLeave,
                    Sub(s, e)
                        DragTargetDay = Date.MinValue
                        RefreshAllDayBorders()
                    End Sub

                AddHandler lst.DragDrop,
                    Sub(s, e)
                        DragTargetDay = Date.MinValue
                        RefreshAllDayBorders()
                        Try
                            Dim targetList = DirectCast(s, ListBox)
                            Dim targetDay = CDate(targetList.Tag)
                            Dim appt As AppointmentC = Nothing
                            If e.Data.GetDataPresent(GetType(MonthWeekApptItem)) Then
                                appt = DirectCast(e.Data.GetData(GetType(MonthWeekApptItem)), MonthWeekApptItem).Ap
                            ElseIf e.Data.GetDataPresent(GetType(AppointmentC)) Then
                                appt = DirectCast(e.Data.GetData(GetType(AppointmentC)), AppointmentC)
                            ElseIf e.Data.GetDataPresent("Appointment") Then
                                appt = DirectCast(e.Data.GetData("Appointment"), AppointmentC)
                            End If
                            If appt Is Nothing OrElse InteractionHub Is Nothing Then Return
                            InteractionHub.PublishWeekColumnAppointmentDrop(appt, appt.StartDateTime.Date, targetDay)
                        Catch
                        End Try
                    End Sub

                _monthWeekToolTip.SetToolTip(lst, preview)

                dayBox.Controls.Add(lst)
                flow.Controls.Add(dayBox)
            Next

            weekStack.Controls.Add(headerRow, 0, 0)
            weekStack.Controls.Add(flow, 0, 1)
            weekClient.Controls.Add(weekStack)
            weekShell.Controls.Add(weekClient)
            mainFlow.Controls.Add(weekShell)
            weekStart = weekStart.AddDays(7)
            weekIdx += 1
        Loop

        _workArea.Controls.Add(mainFlow)
        mainFlow.SendToBack()
        If _dimOverlay IsNot Nothing Then _dimOverlay.BringToFront()
        EnsureDayDrawerOnTop()
    End Sub

    Private Sub MwDragTimer_Tick(sender As Object, e As EventArgs)
        Try
            _mwDragTimer.Stop()
            If _mwDragSourceList Is Nothing Then Return
            _mwDragInitiated = True
            BeginDragFromMonthWeekList(_mwDragSourceList)
        Catch
        End Try
    End Sub

    Private Sub BeginDragFromMonthWeekList(lb As ListBox)
        If lb Is Nothing OrElse lb.SelectedItem Is Nothing Then Return
        Dim item = TryCast(lb.SelectedItem, MonthWeekApptItem)
        If item Is Nothing OrElse item.Ap Is Nothing Then Return
        
        Try
            DragSourceDay = item.Ap.StartDateTime.Date
            RefreshAllDayBorders()

            Dim dragData As New DataObject()
            dragData.SetData("Appointment", item.Ap)
            dragData.SetData("SourceDay", item.Ap.StartDateTime.Date)
            dragData.SetData("SourceDoctor", item.Ap.DrID)
            lb.DoDragDrop(dragData, DragDropEffects.Move)
        Finally
            DragSourceDay = Date.MinValue
            RefreshAllDayBorders()
            _mwDragSourceList = Nothing
            _mwDragInitiated = False
        End Try
    End Sub

    Private Sub WireMonthWeeksChipDrawItem(lst As ListBox, rowSurface As Color,
                                          statusColors As Dictionary(Of String, Color))
        AddHandler lst.DrawItem,
            Sub(sender, e)
                If e.Index < 0 Then Return
                Dim g = e.Graphics
                g.SmoothingMode = SmoothingMode.AntiAlias
                Dim item = TryCast(DirectCast(sender, ListBox).Items(e.Index), MonthWeekApptItem)
                Dim stKey = If(item?.Ap?.Status, "")
                ' Status keys on appointments are the canonical English keys in all UI languages.
                Dim statusColor As Color = If(item IsNot Nothing AndAlso statusColors.ContainsKey(stKey), statusColors(stKey), Color.Gainsboro)

                Dim b = e.Bounds
                b.Inflate(-2, -2)
                Dim path = MonthVisual.RoundedRect(b, 5)

                Dim isInternalSelected = (item IsNot Nothing AndAlso item.Ap IsNot Nothing AndAlso item.Ap.AppointmentID = _selectedApptId)
                If isInternalSelected OrElse (e.State And DrawItemState.Selected) = DrawItemState.Selected Then
                    Using br As New SolidBrush(Color.FromArgb(220, 236, 255))
                        g.FillPath(br, path)
                    End Using
                Else
                    Using br As New SolidBrush(Color.White)
                        g.FillPath(br, path)
                    End Using
                End If
                Using pen As New Pen(Color.FromArgb(220, 226, 235), 1)
                    g.DrawPath(pen, path)
                End Using

                Dim accentW = 5
                Dim glyph = MonthVisual.StatusGlyphLetter(item?.Ap)
                Using glyphFont = CreateCalibriFont(8.5F, FontStyle.Bold)
                    Dim accentRect = New Rectangle(b.Left, b.Top, accentW, b.Height)
                    Using br As New SolidBrush(statusColor)
                        g.FillRectangle(br, accentRect)
                    End Using
                    Dim glyphRect = New Rectangle(b.Left + accentW + 5, b.Top + (b.Height - 18) \ 2, 18, 18)
                    MonthVisual.PaintStatusGlyphCircle(g, glyphRect, statusColor, glyph, glyphFont)
                    Dim textLeft = glyphRect.Right + 6
                    Dim textRect = New Rectangle(textLeft, b.Top + 2, b.Right - textLeft - 6, b.Height - 4)
                    TextRenderer.DrawText(g, DirectCast(sender, ListBox).GetItemText(DirectCast(sender, ListBox).Items(e.Index)), lst.Font, textRect, Color.FromArgb(40, 45, 55),
                        TextFormatFlags.Left Or TextFormatFlags.VerticalCenter Or TextFormatFlags.EndEllipsis Or TextFormatFlags.SingleLine)
                End Using

                e.DrawFocusRectangle()
            End Sub
    End Sub

    Friend Sub ShowListStatusContextMenu(host As Control, appt As AppointmentC, statusColors As Dictionary(Of String, Color), e As MouseEventArgs)
        If e.Button <> MouseButtons.Right OrElse appt Is Nothing OrElse InteractionHub Is Nothing Then Return
        Dim menuFont = CreateCalibriFont(9.75F, FontStyle.Bold)
        Dim contextMenu As New ContextMenuStrip With {
            .RightToLeft = If(Eng, RightToLeft.No, RightToLeft.Yes),
            .Font = menuFont,
            .ShowImageMargin = False
        }
        Dim editItem = New ToolStripMenuItem(If(Eng, "Edit appointment…", "تعديل الموعد…")) With {.Font = menuFont}
        AddHandler editItem.Click, Sub() InteractionHub.PublishAppointmentDoubleClicked(appt)
        contextMenu.Items.Add(editItem)
        contextMenu.Items.Add(New ToolStripSeparator())
        For Each kvp In statusColors
            Dim statusKey = kvp.Key
            Dim c = kvp.Value
            Dim menuItem = New ToolStripMenuItem(TranslateAppointmentStatus(statusKey)) With {.Font = menuFont}
            AddHandler menuItem.Click, Sub() InteractionHub.PublishAppointmentStatusChange(appt, statusKey, c)
            contextMenu.Items.Add(menuItem)
        Next
        contextMenu.Show(host, e.Location)
    End Sub
#End Region

#Region "Month calendar grid (magazine tiles)"
    Private Sub RenderMonthCalendarView()
        Dim data = _request.Data
        Dim state = _request.State
        Dim currentDate = state.CurrentDate
        Dim apptsAll = If(data.Appointments, New List(Of AppointmentC)())
        Dim statusColors = GetStandardAppointmentStatusColors()

        Dim firstOfMonth = New DateTime(currentDate.Year, currentDate.Month, 1)
        Dim startDay = firstOfMonth.AddDays(-CInt(firstOfMonth.DayOfWeek))
        Const rows = 6
        Const cols = 7

        Dim titleFont = CreateCalibriFont(15.0F, FontStyle.Bold)
        Dim titleText = BuildRangeCaption(state, data)
        Dim titleAvailW = Math.Max(280, ClientSize.Width - 32)
        Dim titleTextH = TextRenderer.MeasureText(titleText, titleFont, New Size(titleAvailW, Integer.MaxValue),
            TextFormatFlags.SingleLine Or TextFormatFlags.WordEllipsis).Height
        Dim monthTitleBarH = titleTextH + 12

        Dim monthTitlePanel As New Panel With {
            .Dock = DockStyle.Fill,
            .BackColor = Color.Transparent,
            .Padding = New Padding(0, 0, 0, 1)
        }
        Dim monthTitleLbl As New Label With {
            .Dock = DockStyle.Fill,
            .Padding = New Padding(8, 6, 8, 6),
            .Font = titleFont,
            .ForeColor = Color.FromArgb(32, 42, 58),
            .BackColor = Color.Transparent,
            .TextAlign = ContentAlignment.MiddleCenter,
            .Text = titleText
        }
        If Eng Then monthTitlePanel.RightToLeft = RightToLeft.No Else monthTitlePanel.RightToLeft = RightToLeft.Yes
        monthTitlePanel.Controls.Add(monthTitleLbl)
        AddHandler monthTitlePanel.Paint,
            Sub(s, pe)
                Using p As New Pen(Color.FromArgb(225, 232, 240), 1)
                    pe.Graphics.DrawLine(p, 0, monthTitlePanel.Height - 1, monthTitlePanel.Width, monthTitlePanel.Height - 1)
                End Using
            End Sub

        Dim calendarHost As New TableLayoutPanel With {
            .Dock = DockStyle.Fill,
            .ColumnCount = 1,
            .RowCount = 2,
            .BackColor = MonthCalendarGridBack,
            .Margin = New Padding(0)
        }
        calendarHost.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 100.0F))
        calendarHost.RowStyles.Add(New RowStyle(SizeType.Absolute, CSng(monthTitleBarH)))
        calendarHost.RowStyles.Add(New RowStyle(SizeType.Percent, 100.0F))
        calendarHost.Controls.Add(monthTitlePanel, 0, 0)

        Dim table As New TableLayoutPanel With {
            .Dock = DockStyle.Fill,
            .ColumnCount = cols,
            .RowCount = rows,
            .BackColor = MonthCalendarGridBack,
            .Padding = New Padding(4, 4, 4, 8)
        }
        If Not Eng Then table.RightToLeft = RightToLeft.Yes
        For c = 1 To cols
            table.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 100.0F / cols))
        Next
        For r = 1 To rows
            table.RowStyles.Add(New RowStyle(SizeType.Percent, 100.0F / rows))
        Next

        Dim dayColors As Color() = {
            MonthVisual.Soften(Color.FromArgb(255, 230, 230)),
            MonthVisual.Soften(Color.FromArgb(255, 245, 220)),
            MonthVisual.Soften(Color.FromArgb(240, 255, 230)),
            MonthVisual.Soften(Color.FromArgb(230, 250, 255)),
            MonthVisual.Soften(Color.FromArgb(245, 230, 255)),
            MonthVisual.Soften(Color.FromArgb(255, 255, 230)),
            MonthVisual.Soften(Color.FromArgb(230, 255, 240))
        }

        Dim d = startDay
        For r = 0 To rows - 1
            For c = 0 To cols - 1
                Dim thisDay = d
                Dim appts = ApptTheme.OrderAppointmentsForDisplay(apptsAll.Where(Function(a) ApptTheme.GetAppointmentCalendarDay(a) = thisDay.Date), data, linkedDoctorAtEnd:=True)
                Dim inMonth = (thisDay.Month = currentDate.Month)
                Dim tile As New MonthTilePanel(Me, data, state, statusColors, thisDay, appts, inMonth, dayColors(CInt(thisDay.DayOfWeek)), _monthWeekToolTip)
                table.Controls.Add(tile, c, r)
                d = d.AddDays(1)
            Next
        Next

        _calendarTableHost = table
        calendarHost.Controls.Add(table, 0, 1)
        _workArea.Controls.Add(calendarHost)
        If _dimOverlay IsNot Nothing Then _dimOverlay.BringToFront()
        EnsureDayDrawerOnTop()
    End Sub

    Private Shared Function FormatMonthPreviewReasonNotes(ap As AppointmentC, state As ApptState) As String
        If ap Is Nothing OrElse state Is Nothing OrElse Not state.IncludeReason Then Return ""
        Dim r = If(ap.Reason, "").Trim()
        Dim n = If(ap.Notes, "").Trim()
        If String.IsNullOrWhiteSpace(r) AndAlso String.IsNullOrWhiteSpace(n) Then Return If(Eng, "(no reason / notes)", "(لا سبب / ملاحظات)")
        If String.IsNullOrWhiteSpace(n) Then Return r
        If String.IsNullOrWhiteSpace(r) Then Return n
        Return r & " · " & n
    End Function
#End Region

    Private Shared Function MeasureSingleLineLabelSize(text As String, font As Font, maxWidth As Integer, padH As Integer, padV As Integer) As Size
        Dim innerW = Math.Max(1, maxWidth - padH * 2)
        Dim sz = TextRenderer.MeasureText(text, font, New Size(innerW, Integer.MaxValue), TextFormatFlags.SingleLine Or TextFormatFlags.EndEllipsis Or TextFormatFlags.NoPadding)
        Return New Size(Math.Max(1, maxWidth), Math.Max(sz.Height + padV * 2, font.Height + padV * 2))
    End Function

    Private Shared Function MonthWeeksCaptionAverageColor(capHi As Color, capLo As Color) As Color
        Return Color.FromArgb(
            (CInt(capHi.R) + CInt(capLo.R)) \ 2,
            (CInt(capHi.G) + CInt(capLo.G)) \ 2,
            (CInt(capHi.B) + CInt(capLo.B)) \ 2)
    End Function

    Private Structure MonthWeekTheme
        Public HeaderHi As Color
        Public HeaderLo As Color
        Public BodyBg As Color
        Public ListSurface As Color
        Public ShellBorder As Color
        Public HeaderRuleColor As Color
        Public SparkGradientHi As Color
        Public SparkGradientLo As Color
    End Structure

    ''' <summary>Rotating pastel week bands (header gradient, body wash, list surface, border, sparkline).</summary>
    Private Shared Function GetMonthWeekColorTheme(weekIdx As Integer, isCurrentWeek As Boolean) As MonthWeekTheme
        Dim themes As MonthWeekTheme() = {
            New MonthWeekTheme With {
                .HeaderHi = Color.FromArgb(255, 225, 236), .HeaderLo = Color.FromArgb(252, 205, 224),
                .BodyBg = Color.FromArgb(255, 238, 244), .ListSurface = Color.FromArgb(255, 246, 250),
                .ShellBorder = Color.FromArgb(190, 85, 120), .HeaderRuleColor = Color.FromArgb(230, 175, 198),
                .SparkGradientHi = Color.FromArgb(200, 75, 115), .SparkGradientLo = Color.FromArgb(255, 195, 215)
            },
            New MonthWeekTheme With {
                .HeaderHi = Color.FromArgb(220, 236, 255), .HeaderLo = Color.FromArgb(200, 224, 252),
                .BodyBg = Color.FromArgb(235, 244, 255), .ListSurface = Color.FromArgb(242, 248, 255),
                .ShellBorder = Color.FromArgb(85, 125, 195), .HeaderRuleColor = Color.FromArgb(185, 210, 235),
                .SparkGradientHi = Color.FromArgb(70, 120, 200), .SparkGradientLo = Color.FromArgb(170, 210, 245)
            },
            New MonthWeekTheme With {
                .HeaderHi = Color.FromArgb(220, 246, 228), .HeaderLo = Color.FromArgb(200, 238, 212),
                .BodyBg = Color.FromArgb(234, 250, 238), .ListSurface = Color.FromArgb(241, 252, 244),
                .ShellBorder = Color.FromArgb(65, 145, 95), .HeaderRuleColor = Color.FromArgb(175, 220, 195),
                .SparkGradientHi = Color.FromArgb(55, 140, 90), .SparkGradientLo = Color.FromArgb(160, 225, 185)
            },
            New MonthWeekTheme With {
                .HeaderHi = Color.FromArgb(255, 240, 215), .HeaderLo = Color.FromArgb(252, 225, 190),
                .BodyBg = Color.FromArgb(255, 246, 232), .ListSurface = Color.FromArgb(255, 250, 240),
                .ShellBorder = Color.FromArgb(195, 125, 55), .HeaderRuleColor = Color.FromArgb(235, 200, 155),
                .SparkGradientHi = Color.FromArgb(210, 130, 60), .SparkGradientLo = Color.FromArgb(255, 215, 170)
            },
            New MonthWeekTheme With {
                .HeaderHi = Color.FromArgb(236, 228, 255), .HeaderLo = Color.FromArgb(222, 210, 248),
                .BodyBg = Color.FromArgb(244, 240, 252), .ListSurface = Color.FromArgb(248, 245, 255),
                .ShellBorder = Color.FromArgb(125, 95, 175), .HeaderRuleColor = Color.FromArgb(210, 195, 235),
                .SparkGradientHi = Color.FromArgb(115, 85, 165), .SparkGradientLo = Color.FromArgb(210, 190, 245)
            },
            New MonthWeekTheme With {
                .HeaderHi = Color.FromArgb(215, 246, 242), .HeaderLo = Color.FromArgb(195, 238, 232),
                .BodyBg = Color.FromArgb(232, 250, 247), .ListSurface = Color.FromArgb(238, 252, 249),
                .ShellBorder = Color.FromArgb(55, 150, 140), .HeaderRuleColor = Color.FromArgb(170, 220, 215),
                .SparkGradientHi = Color.FromArgb(45, 145, 135), .SparkGradientLo = Color.FromArgb(165, 230, 220)
            }
        }
        Dim t = themes(weekIdx Mod themes.Length)
        If isCurrentWeek Then
            t.ShellBorder = Color.FromArgb(0, 140, 155)
            t.HeaderRuleColor = Color.FromArgb(120, 195, 205)
            t.SparkGradientHi = Color.FromArgb(0, 130, 145)
            t.SparkGradientLo = Color.FromArgb(120, 210, 218)
        End If
        Return t
    End Function

    Protected Overrides Sub Dispose(disposing As Boolean)
        If disposing Then
            _mwDragTimer.Stop()
            RemoveHandler _mwDragTimer.Tick, AddressOf MwDragTimer_Tick
            _mwDragTimer.Dispose()
            If _drawerAnimTimer IsNot Nothing Then
                _drawerAnimTimer.Stop()
                RemoveHandler _drawerAnimTimer.Tick, AddressOf DrawerAnimTimer_Tick
                _drawerAnimTimer.Dispose()
            End If
            If _monthWeekHostResizeDebounce IsNot Nothing Then
                _monthWeekHostResizeDebounce.Stop()
                RemoveHandler _monthWeekHostResizeDebounce.Tick, AddressOf MonthWeekHostResize_Tick
                RemoveHandler Me.Resize, AddressOf ApptMonthCtl_HostResize
                _monthWeekHostResizeDebounce.Dispose()
            End If
            _monthWeekToolTip.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub
End Class

#Region "Visual helpers"
Friend NotInheritable Class MonthVisual
    Private Sub New()
    End Sub

    Public Shared Function Soften(c As Color) As Color
        Return Color.FromArgb(255,
            CInt(c.R * 0.85F + 255 * 0.15F),
            CInt(c.G * 0.85F + 255 * 0.15F),
            CInt(c.B * 0.85F + 255 * 0.15F))
    End Function

    Public Shared Function RoundedRect(bounds As Rectangle, radius As Integer) As GraphicsPath
        Dim path As New GraphicsPath()
        Dim d = radius * 2
        path.AddArc(bounds.Left, bounds.Top, d, d, 180, 90)
        path.AddArc(bounds.Right - d, bounds.Top, d, d, 270, 90)
        path.AddArc(bounds.Right - d, bounds.Bottom - d, d, d, 0, 90)
        path.AddArc(bounds.Left, bounds.Bottom - d, d, d, 90, 90)
        path.CloseFigure()
        Return path
    End Function

    Public Shared Function StatusGlyphLetter(ap As AppointmentC) As String
        If ap Is Nothing OrElse String.IsNullOrWhiteSpace(ap.Status) Then Return "·"
        Dim k = ap.Status.Trim()
        If k.Length > 0 Then Return k.Substring(0, 1).ToUpperInvariant()
        Return "·"
    End Function

    Public Shared Sub PaintStatusGlyphCircle(g As Graphics, bounds As Rectangle, statusColor As Color, glyphLetter As String, glyphFont As Font)
        g.SmoothingMode = SmoothingMode.AntiAlias
        Using br As New SolidBrush(statusColor)
            g.FillEllipse(br, bounds)
        End Using
        Using br As New SolidBrush(GetReadableForeColor(statusColor))
            Dim sf As New StringFormat With {.Alignment = StringAlignment.Center, .LineAlignment = StringAlignment.Center}
            g.DrawString(If(glyphLetter, "·"), glyphFont, br, bounds, sf)
        End Using
    End Sub

    Public Shared Sub PaintWeekDayBoxBorder(host As ApptMonthCtl, dayBox As Panel, day As Date, apptCount As Integer, wash As Color)
        AddHandler dayBox.Paint,
            Sub(s, pe)
                pe.Graphics.SmoothingMode = SmoothingMode.AntiAlias
                Dim r = dayBox.ClientRectangle
                r.Inflate(-1, -1)
                Using path = RoundedRect(r, 6)
                    Using br As New SolidBrush(wash)
                        pe.Graphics.FillPath(br, path)
                    End Using

                    Dim borderCol = Color.FromArgb(210, 216, 228)
                    Dim penW = 1.0F

                    Dim isSource = (host.DragSourceDay.Date = day.Date)
                    Dim isTarget = (host.DragTargetDay.Date = day.Date)

                    If isTarget Then
                        borderCol = Color.Red
                        penW = 2.5F
                    ElseIf isSource Then
                        borderCol = Color.BlueViolet
                        penW = 2.5F
                    ElseIf day.Date = Date.Today Then
                        borderCol = ApptMonthCtl.BrandAccentShared
                        penW = 2.0F
                    End If

                    Using p As New Pen(borderCol, penW)
                        pe.Graphics.DrawPath(p, path)
                    End Using
                End Using
            End Sub
    End Sub
End Class

Partial Public Class ApptMonthCtl
    Friend Shared ReadOnly Property BrandAccentShared As Color
        Get
            Return BrandAccent
        End Get
    End Property

    Friend Function GetSelectedApptId() As Integer
        Return _selectedApptId
    End Function
End Class
#End Region

#Region "Week sparkline"
Friend NotInheritable Class WeekSparklinePanel
    Inherits Panel

    Private ReadOnly _counts As Integer()
    Private ReadOnly _gradHi As Color
    Private ReadOnly _gradLo As Color

    Public Sub New(counts As Integer(), gradHi As Color, gradLo As Color)
        SetStyle(ControlStyles.AllPaintingInWmPaint Or ControlStyles.UserPaint Or ControlStyles.OptimizedDoubleBuffer Or ControlStyles.ResizeRedraw, True)
        _counts = If(counts, New Integer() {})
        _gradHi = gradHi
        _gradLo = gradLo
        BackColor = Color.Transparent
    End Sub

    Protected Overrides Sub OnPaint(e As PaintEventArgs)
        MyBase.OnPaint(e)
        Dim g = e.Graphics
        g.SmoothingMode = SmoothingMode.AntiAlias
        Dim n = Math.Min(7, _counts.Length)
        If n = 0 Then Return
        Dim mx = _counts.Take(n).Max()
        If mx < 1 Then mx = 1
        Dim gap = 3
        Dim barW = (Width - gap * (n - 1)) \ n
        Dim baseY = Height - 4
        For i = 0 To n - 1
            Dim h = CInt((_counts(i) / CSng(mx)) * (Height - 10))
            h = Math.Max(2, h)
            Dim x = i * (barW + gap)
            Dim rect = New Rectangle(x, baseY - h, barW, h)
            Using br As New LinearGradientBrush(rect, _gradHi, _gradLo, LinearGradientMode.Vertical)
                g.FillRoundedRectangle(br, rect, 2)
            End Using
        Next
    End Sub
End Class

Friend Module GraphicsExtensions
    <Extension>
    Public Sub FillRoundedRectangle(g As Graphics, brush As Brush, rect As Rectangle, radius As Integer)
        Using path = MonthVisual.RoundedRect(rect, radius)
            g.FillPath(brush, path)
        End Using
    End Sub
End Module
#End Region

#Region "Dim overlay"
Friend NotInheritable Class MonthDrawerDimPanel
    Inherits ApptDrawerDimScrimPanel

    Public Sub New(host As ApptMonthCtl)
        MyBase.New(Sub()
                       If host IsNot Nothing Then host.RequestCloseDayDrawer()
                   End Sub)
    End Sub
End Class
#End Region

#Region "Slide drawer host"
Friend Class MonthDayDrawerHost
    Inherits Panel

    Private ReadOnly _monthCtl As ApptMonthCtl
    Private ReadOnly _header As Panel
    Private ReadOnly _lblTitle As Label
    Private ReadOnly _btnClose As Label
    Private ReadOnly _scroll As Panel

    Public Sub New(monthCtl As ApptMonthCtl)
        _monthCtl = monthCtl
        SetStyle(ControlStyles.AllPaintingInWmPaint Or ControlStyles.OptimizedDoubleBuffer Or ControlStyles.ResizeRedraw, True)
        Width = 400
        BackColor = DrawerDayPanelWash

        _header = New Panel With {.Dock = DockStyle.Top, .Height = 52, .BackColor = DrawerDayHeaderSolid}
        _lblTitle = New Label With {
            .AutoSize = False,
            .Dock = DockStyle.Fill,
            .Padding = New Padding(14, 14, 44, 8),
            .Font = CreateCalibriFont(11.0F, FontStyle.Bold),
            .ForeColor = Color.White,
            .BackColor = Color.Transparent,
            .TextAlign = ContentAlignment.TopLeft
        }
        _btnClose = New Label With {
            .Text = "✕",
            .Size = New Size(36, 36),
            .Location = New Point(Width - 40, 8),
            .Anchor = If(Eng, AnchorStyles.Top Or AnchorStyles.Right, AnchorStyles.Top Or AnchorStyles.Left),
            .Font = CreateCalibriFont(12.0F, FontStyle.Bold),
            .ForeColor = DrawerHeaderCloseIdle,
            .BackColor = Color.Transparent,
            .Cursor = Cursors.Hand,
            .TextAlign = ContentAlignment.MiddleCenter
        }
        AddHandler _btnClose.Click, Sub() _monthCtl.RequestCloseDayDrawer()
        AddHandler _btnClose.MouseEnter, Sub() _btnClose.ForeColor = Color.FromArgb(255, 200, 200)
        AddHandler _btnClose.MouseLeave, Sub() _btnClose.ForeColor = DrawerHeaderCloseIdle

        _header.Controls.Add(_lblTitle)
        _header.Controls.Add(_btnClose)
        _btnClose.BringToFront()
        AddHandler _header.SizeChanged, Sub() PositionCloseButton()

        _scroll = New Panel With {
            .Dock = DockStyle.Fill,
            .AutoScroll = True,
            .BackColor = DrawerDayPanelWash,
            .Padding = New Padding(12, 10, 12, 16)
        }

        Controls.Add(_scroll)
        Controls.Add(_header)

        AddHandler Me.Paint,
            Sub(s, pe)
                Using p As New Pen(DrawerDayHeaderRule, 1)
                    pe.Graphics.DrawLine(p, 0, _header.Bottom, Width, _header.Bottom)
                End Using
            End Sub

        AddHandler _scroll.SizeChanged,
            Sub()
                RelayoutDrawerRows()
            End Sub
    End Sub

    Protected Overrides Sub OnPaintBackground(e As PaintEventArgs)
        Using br As New SolidBrush(BackColor)
            e.Graphics.FillRectangle(br, ClientRectangle)
        End Using
    End Sub

    Private Sub RelayoutDrawerRows()
        If _scroll Is Nothing Then Return
        Dim inner = Math.Max(80, _scroll.ClientSize.Width - _scroll.Padding.Horizontal)
        Dim w = Math.Max(200, inner - 8)
        For Each c As Control In _scroll.Controls
            c.Width = w
        Next
    End Sub

    Friend Sub RelayoutAfterOpen()
        PerformLayout()
        RelayoutDrawerRows()
        PositionCloseButton()
        Invalidate(True)
    End Sub

    Private Sub PositionCloseButton()
        If _header Is Nothing OrElse _btnClose Is Nothing Then Return
        If Eng Then
            _btnClose.Left = _header.ClientSize.Width - _btnClose.Width - 6
        Else
            _btnClose.Left = 6
        End If
    End Sub

    Public Sub Populate(day As DateTime, appts As List(Of AppointmentC), data As ApptDataBundle, state As ApptState, statusColors As Dictionary(Of String, Color))
        _scroll.Controls.Clear()
        If Eng Then
            RightToLeft = RightToLeft.No
            _lblTitle.RightToLeft = RightToLeft.No
            _scroll.RightToLeft = RightToLeft.No
            _header.RightToLeft = RightToLeft.No
            _lblTitle.Padding = New Padding(14, 14, 44, 8)
            _btnClose.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        Else
            RightToLeft = RightToLeft.Yes
            _lblTitle.RightToLeft = RightToLeft.Yes
            _scroll.RightToLeft = RightToLeft.Yes
            _header.RightToLeft = RightToLeft.Yes
            _lblTitle.Padding = New Padding(44, 14, 14, 8)
            _btnClose.Anchor = AnchorStyles.Top Or AnchorStyles.Left
        End If
        PositionCloseButton()
        _lblTitle.TextAlign = If(Eng, ContentAlignment.TopLeft, ContentAlignment.TopRight)
        _lblTitle.Text = FormatCaptionDayFull(day)

        Dim y = 0
        Dim innerW = Math.Max(80, _scroll.ClientSize.Width - _scroll.Padding.Horizontal)
        Dim rowW = Math.Max(200, Math.Min(innerW - 8, Width - 32))
        For Each ap In appts
            Dim row As New MonthDrawerApptRow(_monthCtl, ap, data, state, statusColors) With {
                .Location = New Point(4, y),
                .Width = rowW,
                .Anchor = AnchorStyles.Left Or AnchorStyles.Right Or AnchorStyles.Top
            }
            _scroll.Controls.Add(row)
            y += row.Height + 10
        Next
        If appts.Count = 0 Then
            Dim emptyLbl As New Label With {
                .AutoSize = False,
                .Location = New Point(4, 4),
                .Width = rowW,
                .Height = 56,
                .Text = If(Eng, "No appointments scheduled for this day.", "لا توجد مواعيد في هذا اليوم."),
                .Font = CreateCalibriFont(10.5F, FontStyle.Italic),
                .ForeColor = Color.FromArgb(118, 128, 145),
                .BackColor = DrawerDayPanelWash,
                .TextAlign = ContentAlignment.TopCenter
            }
            _scroll.Controls.Add(emptyLbl)
        End If
        _scroll.PerformLayout()
        RelayoutDrawerRows()
    End Sub

    Protected Overrides Sub OnResize(e As EventArgs)
        MyBase.OnResize(e)
        PositionCloseButton()
    End Sub
End Class

Friend Class MonthDrawerApptRow
    Inherits Panel

    Private ReadOnly _monthCtl As ApptMonthCtl
    Private ReadOnly _appt As AppointmentC
    Private ReadOnly _data As ApptDataBundle
    Private ReadOnly _state As ApptState
    Private ReadOnly _statusColors As Dictionary(Of String, Color)

    Public Sub New(monthCtl As ApptMonthCtl, appt As AppointmentC, data As ApptDataBundle, state As ApptState, statusColors As Dictionary(Of String, Color))
        SetStyle(ControlStyles.AllPaintingInWmPaint Or ControlStyles.UserPaint Or ControlStyles.OptimizedDoubleBuffer Or ControlStyles.ResizeRedraw, True)
        _monthCtl = monthCtl
        _appt = appt
        _data = data
        _state = state
        _statusColors = statusColors
        Height = 76
        Cursor = Cursors.Hand
        BackColor = DrawerDayPanelWash

        AddHandler Me.MouseClick,
            Sub(s, e)
                If e.Button = MouseButtons.Left AndAlso _monthCtl.InteractionHub IsNot Nothing Then
                    _monthCtl.InteractionHub.PublishAppointmentClicked(_appt)
                End If
            End Sub
        AddHandler Me.DoubleClick,
            Sub()
                If _monthCtl.InteractionHub IsNot Nothing Then _monthCtl.InteractionHub.PublishAppointmentDoubleClicked(_appt)
            End Sub
        AddHandler Me.MouseUp,
            Sub(s, e)
                If e.Button = MouseButtons.Right Then
                    _monthCtl.ShowDrawerRowStatusMenu(Me, _appt, _statusColors, e)
                End If
            End Sub
    End Sub

    Protected Overrides Sub OnPaint(e As PaintEventArgs)
        MyBase.OnPaint(e)
        Dim g = e.Graphics
        g.SmoothingMode = SmoothingMode.AntiAlias
        Dim r = ClientRectangle
        r.Inflate(-2, -2)
        Dim path = MonthVisual.RoundedRect(r, 8)
        Using br As New SolidBrush(DrawerApptCardSurface)
            g.FillPath(br, path)
        End Using
        Using p As New Pen(Color.FromArgb(210, 218, 228), 1)
            g.DrawPath(p, path)
        End Using

        Dim st = If(_appt?.Status, "")
        Dim sc = If(_statusColors IsNot Nothing AndAlso _statusColors.ContainsKey(st), _statusColors(st), Color.Silver)

        Dim patient = _data.ResolvePatientName(_appt.PatientID)
        Dim dr = _data.ResolveDoctor(_appt.DrID).DrName
        Dim timeStr = _appt.StartDateTime.ToString(ApptMonthCtl.AppointmentTimeFormat)
        Dim statusTxt = GetAppointmentStatusDisplayText(_appt)
        Dim subLine = $"{dr} · {statusTxt}"
        If _state IsNot Nothing AndAlso _state.IncludeReason Then
            Dim note = FormatDrawerReasonLine(_appt)
            If Not String.IsNullOrWhiteSpace(note) Then subLine = note & vbCrLf & subLine
        End If

        Dim glyph = MonthVisual.StatusGlyphLetter(_appt)
        Using glyphFont = CreateCalibriFont(9.0F, FontStyle.Bold)
            If Eng Then
                Dim accent = New Rectangle(r.Left, r.Top, 6, r.Height)
                Using br As New SolidBrush(sc)
                    g.FillRectangle(br, accent)
                End Using
                Dim x0 = r.Left + 18
                Using f1 = CreateCalibriFont(10.0F, FontStyle.Bold)
                    g.DrawString(timeStr & "   " & patient, f1, Brushes.Black, New PointF(x0, r.Top + 10))
                End Using
                Using f2 = CreateCalibriFont(8.75F, FontStyle.Bold)
                    g.DrawString(subLine, f2, New SolidBrush(Color.FromArgb(95, 105, 120)), New RectangleF(x0, r.Top + 34, r.Width - 24, 40))
                End Using
                Dim gr = New Rectangle(r.Right - 32, r.Top + (r.Height - 22) \ 2, 22, 22)
                MonthVisual.PaintStatusGlyphCircle(g, gr, sc, glyph, glyphFont)
            Else
                Dim accent = New Rectangle(r.Right - 6, r.Top, 6, r.Height)
                Using br As New SolidBrush(sc)
                    g.FillRectangle(br, accent)
                End Using
                Dim grL = New Rectangle(r.Left + 10, r.Top + (r.Height - 22) \ 2, 22, 22)
                MonthVisual.PaintStatusGlyphCircle(g, grL, sc, glyph, glyphFont)
                Const textPad = 36
                Dim textL = r.Left + textPad
                Dim textW = r.Width - textPad - 8
                Using f1 = CreateCalibriFont(10.0F, FontStyle.Bold)
                    TextRenderer.DrawText(g, timeStr & "   " & patient, f1, New Rectangle(textL, r.Top + 10, textW, 24), Color.Black,
                        TextFormatFlags.RightToLeft Or TextFormatFlags.Right Or TextFormatFlags.VerticalCenter Or TextFormatFlags.SingleLine Or TextFormatFlags.EndEllipsis)
                End Using
                Using f2 = CreateCalibriFont(8.75F, FontStyle.Bold)
                    TextRenderer.DrawText(g, subLine, f2, New Rectangle(textL, r.Top + 34, textW, 40), Color.FromArgb(95, 105, 120),
                        TextFormatFlags.RightToLeft Or TextFormatFlags.Right Or TextFormatFlags.WordBreak Or TextFormatFlags.Top)
                End Using
            End If
        End Using
    End Sub

    Private Shared Function FormatDrawerReasonLine(ap As AppointmentC) As String
        If ap Is Nothing Then Return ""
        Dim r = If(ap.Reason, "").Trim()
        Dim n = If(ap.Notes, "").Trim()
        If String.IsNullOrWhiteSpace(r) AndAlso String.IsNullOrWhiteSpace(n) Then Return ""
        If String.IsNullOrWhiteSpace(n) Then Return r
        If String.IsNullOrWhiteSpace(r) Then Return n
        Return r & " · " & n
    End Function
End Class

Partial Public Class ApptMonthCtl
    Friend Sub ShowDrawerRowStatusMenu(host As Control, appt As AppointmentC, statusColors As Dictionary(Of String, Color), e As MouseEventArgs)
        ShowListStatusContextMenu(host, appt, statusColors, e)
    End Sub
End Class
#End Region

#Region "Month tile (calendar cell)"
Friend Class MonthTilePanel
    Inherits Panel

    Private ReadOnly _month As ApptMonthCtl
    Private ReadOnly _data As ApptDataBundle
    Private ReadOnly _state As ApptState
    Private ReadOnly _statusColors As Dictionary(Of String, Color)
    Private ReadOnly _day As Date
    Private ReadOnly _appts As List(Of AppointmentC)
    Private ReadOnly _inMonth As Boolean
    Private ReadOnly _wash As Color
    Private ReadOnly _toolTip As ToolTip
    Private _lastTipText As String = ""

    Private _hover As Boolean
    Private _dropHover As Boolean

    Private _chipRects As New List(Of Rectangle)
    Private _chipAppts As New List(Of AppointmentC)
    Private _moreRect As Rectangle
    Private _hasMore As Boolean
    ''' <summary>Day-of-week + day number band; click opens <see cref="ApptMonthCtl.ShowMonthDayDrawer"/>.</summary>
    Private _headerHitRect As Rectangle

    Private ReadOnly _dragTimer As New Timer()
    Private _dragStartPoint As Point
    Private _dragInitiated As Boolean
    Private _dragAppt As AppointmentC

    Public Sub New(monthCtl As ApptMonthCtl, data As ApptDataBundle, state As ApptState, statusColors As Dictionary(Of String, Color),
                   day As Date, appts As List(Of AppointmentC), inMonth As Boolean, wash As Color,
                   Optional toolTip As ToolTip = Nothing)
        SetStyle(ControlStyles.AllPaintingInWmPaint Or ControlStyles.UserPaint Or ControlStyles.OptimizedDoubleBuffer Or ControlStyles.ResizeRedraw, True)
        _month = monthCtl
        _data = data
        _state = state
        _statusColors = statusColors
        _day = day
        _appts = appts
        _inMonth = inMonth
        _wash = wash
        _toolTip = toolTip
        Dock = DockStyle.Fill
        Margin = New Padding(2)
        Cursor = Cursors.Hand
        AllowDrop = True

        AddHandler _dragTimer.Tick, AddressOf DragTimer_Tick

        AddHandler Me.MouseMove, AddressOf Tile_MouseMove
        AddHandler Me.MouseLeave,
            Sub()
                _hover = False
                _lastTipText = ""
                If _toolTip IsNot Nothing Then _toolTip.SetToolTip(Me, "")
                Invalidate()
            End Sub
        AddHandler Me.MouseDown, AddressOf Tile_MouseDown
        AddHandler Me.MouseUp, AddressOf Tile_MouseUp
        AddHandler Me.DoubleClick, AddressOf Tile_DoubleClick

        AddHandler Me.DragEnter,
            Sub(s, e)
                If e.Data.GetDataPresent(GetType(MonthWeekApptItem)) OrElse e.Data.GetDataPresent(GetType(AppointmentC)) OrElse e.Data.GetDataPresent("Appointment") Then
                    e.Effect = DragDropEffects.Move
                    _month.DragTargetDay = _day.Date
                    _month.RefreshAllDayBorders()
                Else
                    e.Effect = DragDropEffects.None
                End If
            End Sub
        AddHandler Me.DragLeave,
            Sub()
                _month.DragTargetDay = Date.MinValue
                _month.RefreshAllDayBorders()
            End Sub
        AddHandler Me.DragDrop,
            Sub(s, e)
                _month.DragTargetDay = Date.MinValue
                _month.RefreshAllDayBorders()
                Try
                    Dim appt As AppointmentC = Nothing
                    If e.Data.GetDataPresent(GetType(MonthWeekApptItem)) Then
                        appt = DirectCast(e.Data.GetData(GetType(MonthWeekApptItem)), MonthWeekApptItem).Ap
                    ElseIf e.Data.GetDataPresent(GetType(AppointmentC)) Then
                        appt = DirectCast(e.Data.GetData(GetType(AppointmentC)), AppointmentC)
                    ElseIf e.Data.GetDataPresent("Appointment") Then
                        appt = DirectCast(e.Data.GetData("Appointment"), AppointmentC)
                    End If
                    If appt Is Nothing OrElse _month.InteractionHub Is Nothing Then Return
                    _month.InteractionHub.PublishWeekColumnAppointmentDrop(appt, appt.StartDateTime.Date, _day.Date)
                Catch
                End Try
            End Sub
    End Sub

    Private Sub Tile_MouseMove(sender As Object, e As MouseEventArgs)
        If _dragAppt IsNot Nothing AndAlso Not _dragInitiated Then
            If Math.Abs(e.Location.X - _dragStartPoint.X) > 6 OrElse Math.Abs(e.Location.Y - _dragStartPoint.Y) > 6 Then
                _dragTimer.Stop()
                _dragAppt = Nothing
            End If
        End If

        Dim was = _hover
        _hover = True
        If Not was Then Invalidate()
        If _toolTip Is Nothing Then Return
        Dim tip = ""
        If _appts.Count > 0 Then
            tip = String.Join(vbCrLf, _appts.Take(5).Select(Function(a)
                                                                Dim d = ApptMonthCtl.FormatMonthPreviewReasonNotesShared(a, _state)
                                                                Dim mid = If(String.IsNullOrWhiteSpace(d), "", $" · {d}")
                                                                Return $"{a.StartDateTime.ToString(ApptMonthCtl.AppointmentTimeFormat)} {_data.ResolvePatientName(a.PatientID)}{mid} · {GetAppointmentStatusDisplayText(a)}"
                                                            End Function))
        End If
        If tip <> _lastTipText Then
            _lastTipText = tip
            _toolTip.SetToolTip(Me, tip)
        End If
    End Sub

    Private Sub Tile_MouseDown(sender As Object, e As MouseEventArgs)
        If e.Button = MouseButtons.Right AndAlso _appts.Count > 0 Then
            _month.ShowListStatusContextMenu(Me, _appts(0), _statusColors, e)
            Return
        End If
        If e.Button <> MouseButtons.Left Then Return

        ' Check chips
        For i = 0 To _chipAppts.Count - 1
            If i < _chipRects.Count AndAlso _chipRects(i).Contains(e.Location) Then
                Dim ap = _chipAppts(i)
                _month.UpdateInternalSelection(ap.AppointmentID)
                If _month.InteractionHub IsNot Nothing Then _month.InteractionHub.PublishAppointmentClicked(ap)

                _dragAppt = ap
                _dragStartPoint = e.Location
                _dragInitiated = False
                _dragTimer.Interval = 750
                _dragTimer.Start()
                Return
            End If
        Next

        If Not _headerHitRect.IsEmpty AndAlso _headerHitRect.Contains(e.Location) Then
            _month.ShowMonthDayDrawer(_day, _appts.ToList())
            Return
        End If
        If _hasMore AndAlso _moreRect.Contains(e.Location) Then
            _month.ShowMonthDayDrawer(_day, _appts)
            Return
        End If
        If _appts.Count > 0 Then _month.ShowMonthDayDrawer(_day, _appts)
    End Sub

    Private Sub Tile_MouseUp(sender As Object, e As MouseEventArgs)
        _dragTimer.Stop()
        _dragAppt = Nothing
    End Sub

    Private Sub DragTimer_Tick(sender As Object, e As EventArgs)
        _dragTimer.Stop()
        If _dragAppt Is Nothing Then Return
        _dragInitiated = True
        Try
            _month.DragSourceDay = _day.Date
            _month.RefreshAllDayBorders()

            Dim dragData As New DataObject()
            dragData.SetData("Appointment", _dragAppt)
            dragData.SetData("SourceDay", _dragAppt.StartDateTime.Date)
            dragData.SetData("SourceDoctor", _dragAppt.DrID)
            DoDragDrop(dragData, DragDropEffects.Move)
        Finally
            _month.DragSourceDay = Date.MinValue
            _month.RefreshAllDayBorders()
            _dragInitiated = False
            _dragAppt = Nothing
        End Try
    End Sub

    Private Sub Tile_DoubleClick(sender As Object, e As EventArgs)
        If _appts.Count = 0 Then
            If _month.InteractionHub IsNot Nothing Then _month.InteractionHub.PublishEmptyDateInvoked(_day.Date)
        Else
            If _month.InteractionHub IsNot Nothing Then _month.InteractionHub.PublishAppointmentDoubleClicked(_appts(0))
        End If
    End Sub

    Protected Overrides Sub OnPaint(e As PaintEventArgs)
        MyBase.OnPaint(e)
        Dim g = e.Graphics
        g.SmoothingMode = SmoothingMode.AntiAlias
        _chipRects.Clear()
        _chipAppts.Clear()

        Dim pad = 7
        Dim r = ClientRectangle
        r.Inflate(-1, -1)
        Dim path = MonthVisual.RoundedRect(r, 10)

        Dim fill = If(_inMonth, _wash, Color.FromArgb(248, 248, 250))
        If Not _inMonth Then fill = Color.FromArgb(242, 243, 246)
        Using br As New SolidBrush(fill)
            g.FillPath(br, path)
        End Using

        Dim borderCol = Color.FromArgb(215, 222, 234)
        Dim penW = 1.0F
        
        Dim isSource = (_month.DragSourceDay.Date = _day.Date)
        Dim isTarget = (_month.DragTargetDay.Date = _day.Date)

        If isTarget Then
            borderCol = Color.Red
            penW = 2.5F
        ElseIf isSource Then
            borderCol = Color.BlueViolet
            penW = 2.5F
        ElseIf _day.Date = Date.Today Then
            borderCol = ApptMonthCtl.BrandAccentShared
            penW = 2.2F
        ElseIf _hover Then
            borderCol = Color.FromArgb(170, 190, 210)
        End If
        Using p As New Pen(borderCol, penW)
            g.DrawPath(p, path)
        End Using

        Dim isToday = (_day.Date = Date.Today)
        Dim dayNumFont As Font = If(_inMonth,
            CreateCalibriFont(If(isToday, 18.0F, 15.5F), FontStyle.Bold),
            CreateCalibriFont(13.0F, FontStyle.Bold))
        Dim dayNumColor = If(_inMonth, If(isToday, ApptMonthCtl.BrandAccentShared, Color.FromArgb(42, 48, 62)), Color.FromArgb(160, 168, 180))
        Dim weekFont = CreateCalibriFont(7.25F, FontStyle.Bold)
        Dim weekTxt As String
        If isToday Then
            weekTxt = If(Eng, "TODAY", "اليوم")
        ElseIf Eng Then
            weekTxt = _day.ToString("ddd").ToUpperInvariant()
        Else
            weekTxt = _day.ToString("ddd")
        End If

        Dim headerLeft = r.Left + pad
        Dim headerTop = r.Top + 5
        If Eng Then
            Using br As New SolidBrush(Color.FromArgb(120, 128, 142))
                g.DrawString(weekTxt, weekFont, br, New PointF(headerLeft, headerTop))
            End Using
            Using br As New SolidBrush(dayNumColor)
                g.DrawString(_day.Day.ToString(), dayNumFont, br, New PointF(headerLeft, headerTop + 10))
            End Using
        Else
            Dim headerRight = r.Right - pad
            TextRenderer.DrawText(g, weekTxt, weekFont, New Point(headerRight - TextRenderer.MeasureText(weekTxt, weekFont).Width, headerTop), Color.FromArgb(120, 128, 142))
            Dim dayStr = _day.Day.ToString()
            TextRenderer.DrawText(g, dayStr, dayNumFont, New Point(headerRight - TextRenderer.MeasureText(dayStr, dayNumFont).Width, headerTop + 10), dayNumColor)
        End If

        If Not _inMonth Then
            _headerHitRect = New Rectangle(r.Left + 2, r.Top + 2, r.Width - 4, 38)
            dayNumFont.Dispose()
            weekFont.Dispose()
            PaintDensity(g, r, _appts.Count)
            Return
        End If

        Dim chipsTop = headerTop + 34
        _headerHitRect = New Rectangle(r.Left + 2, r.Top + 2, r.Width - 4, Math.Max(20, chipsTop - r.Top - 4))
        Dim chipH = 30
        Dim chipGap = 6
        Dim maxChips = 2
        Dim shown = Math.Min(maxChips, _appts.Count)
        Dim chipW = r.Width - pad * 2
        Dim chipLeft = If(Eng, headerLeft, r.Right - pad - chipW)

        For i = 0 To shown - 1
            Dim ap = _appts(i)
            Dim cr = New Rectangle(chipLeft, chipsTop + i * (chipH + chipGap), chipW, chipH)
            _chipRects.Add(cr)
            _chipAppts.Add(ap)
            PaintChip(g, cr, ap)
        Next

        _hasMore = _appts.Count > maxChips
        If _hasMore Then
            Dim moreTop = chipsTop + shown * (chipH + chipGap)
            _moreRect = New Rectangle(chipLeft, moreTop, chipW, 26)
            Using pathM = MonthVisual.RoundedRect(_moreRect, 5)
                Using br As New SolidBrush(Color.FromArgb(248, 250, 255))
                    g.FillPath(br, pathM)
                End Using
                Using p As New Pen(Color.FromArgb(175, 188, 210), 1)
                    g.DrawPath(p, pathM)
                End Using
            End Using
            Dim moreTxt = If(Eng, $"+{_appts.Count - maxChips} more", $"+{_appts.Count - maxChips}")
            Using f = CreateCalibriFont(8.25F, FontStyle.Bold)
                Using br As New SolidBrush(Color.FromArgb(90, 108, 140))
                    Dim sf As New StringFormat With {.Alignment = StringAlignment.Center, .LineAlignment = StringAlignment.Center}
                    g.DrawString(moreTxt, f, br, _moreRect, sf)
                End Using
            End Using
        Else
            _moreRect = Rectangle.Empty
        End If

        PaintDensity(g, r, _appts.Count)

        dayNumFont.Dispose()
        weekFont.Dispose()
    End Sub

    Private Sub PaintChip(g As Graphics, cr As Rectangle, ap As AppointmentC)
        Dim rr = cr
        rr.Inflate(0, 0)
        Using path = MonthVisual.RoundedRect(rr, 5)
            Dim isSelected = (ap IsNot Nothing AndAlso ap.AppointmentID = _month.GetSelectedApptId())
            Dim chipFill = If(isSelected, Color.FromArgb(220, 236, 255), Color.White)
            Using br As New SolidBrush(chipFill)
                g.FillPath(br, path)
            End Using
            Using p As New Pen(Color.FromArgb(228, 234, 244), 1)
                g.DrawPath(p, path)
            End Using
        End Using

        Dim st = If(ap?.Status, "")
        Dim sc = If(_statusColors IsNot Nothing AndAlso _statusColors.ContainsKey(st), _statusColors(st), Color.Silver)
        Dim drName = _data.ResolveDoctor(ap.DrID).DrName
        Dim docColor = MonthTileDoctorColor(drName)
        Dim t = ap.StartDateTime.ToString(ApptMonthCtl.AppointmentTimeFormat)
        Dim name = Ellipsis(_data.ResolvePatientName(ap.PatientID), 18)
        Dim line = $"{t}  {name}"
        Dim glyph = MonthVisual.StatusGlyphLetter(ap)
        Using glyphF = CreateCalibriFont(8.0F, FontStyle.Bold), fLine = CreateCalibriFont(8.75F, FontStyle.Bold)
            If Eng Then
                Dim acc = New Rectangle(cr.Left, cr.Top, 4, cr.Height)
                Using br As New SolidBrush(sc)
                    g.FillRectangle(br, acc)
                End Using
                TextRenderer.DrawText(g, line, fLine, New Rectangle(cr.Left + 10, cr.Top + 5, cr.Width - 36, cr.Height - 6), docColor,
                    TextFormatFlags.Left Or TextFormatFlags.VerticalCenter Or TextFormatFlags.SingleLine Or TextFormatFlags.EndEllipsis)
                Dim gr = New Rectangle(cr.Right - 24, cr.Top + (cr.Height - 20) \ 2, 20, 20)
                MonthVisual.PaintStatusGlyphCircle(g, gr, sc, glyph, glyphF)
            Else
                Dim acc = New Rectangle(cr.Right - 4, cr.Top, 4, cr.Height)
                Using br As New SolidBrush(sc)
                    g.FillRectangle(br, acc)
                End Using
                Dim gr = New Rectangle(cr.Left + 4, cr.Top + (cr.Height - 20) \ 2, 20, 20)
                MonthVisual.PaintStatusGlyphCircle(g, gr, sc, glyph, glyphF)
                TextRenderer.DrawText(g, line, fLine, New Rectangle(cr.Left + 28, cr.Top + 5, cr.Width - 36, cr.Height - 6), docColor,
                    TextFormatFlags.Right Or TextFormatFlags.RightToLeft Or TextFormatFlags.VerticalCenter Or TextFormatFlags.SingleLine Or TextFormatFlags.EndEllipsis)
            End If
        End Using
    End Sub

    Private Shared Function MonthTileDoctorColor(doctorName As String) As Color
        Dim h = If(doctorName, "").GetHashCode()
        Return Color.FromArgb(255, Math.Max(40, Math.Abs(h) Mod 200), Math.Max(40, Math.Abs(h >> 8) Mod 200), Math.Max(40, Math.Abs(h >> 16) Mod 200))
    End Function

    Private Shared Function Ellipsis(s As String, maxLen As Integer) As String
        If String.IsNullOrEmpty(s) Then Return ""
        If s.Length <= maxLen Then Return s
        Return s.Substring(0, Math.Max(1, maxLen - 1)) & "…"
    End Function

    Private Sub PaintDensity(g As Graphics, cellRect As Rectangle, count As Integer)
        Dim seg = 5
        Dim barH = 4
        Dim bottom = cellRect.Bottom - 7
        Dim totalW = cellRect.Width - 20
        Dim gap = 4
        Dim segW = (totalW - gap * (seg - 1)) \ seg
        Dim left0 = cellRect.Left + 10
        Dim filled = Math.Min(seg, count)
        For i = 0 To seg - 1
            Dim rc = New Rectangle(left0 + i * (segW + gap), bottom - barH, segW, barH)
            Dim hi = (i < filled)
            Dim c = If(hi,
                Color.FromArgb(0, 140 + i * 8, 155 + i * 5),
                Color.FromArgb(230, 234, 240))
            Using br As New SolidBrush(c)
                Using path = MonthVisual.RoundedRect(rc, 2)
                    g.FillPath(br, path)
                End Using
            End Using
        Next
    End Sub

End Class

Partial Public Class ApptMonthCtl
    Friend Shared Function FormatMonthPreviewReasonNotesShared(ap As AppointmentC, state As ApptState) As String
        If ap Is Nothing OrElse state Is Nothing OrElse Not state.IncludeReason Then Return ""
        Dim r = If(ap.Reason, "").Trim()
        Dim n = If(ap.Notes, "").Trim()
        If String.IsNullOrWhiteSpace(r) AndAlso String.IsNullOrWhiteSpace(n) Then Return If(Eng, "(no reason / notes)", "(لا سبب / ملاحظات)")
        If String.IsNullOrWhiteSpace(n) Then Return r
        If String.IsNullOrWhiteSpace(r) Then Return n
        Return r & " · " & n
    End Function
End Class
#End Region

''' <summary>ListBox item for month-week rows (drag payload + display).</summary>
Public Class MonthWeekApptItem
    Public Property Ap As AppointmentC
    Public Property Display As String

    Public Sub New(appt As AppointmentC, displayText As String)
        Ap = appt
        Display = displayText
    End Sub

    Public Overrides Function ToString() As String
        Return Display
    End Function
End Class
