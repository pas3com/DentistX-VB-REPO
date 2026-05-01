Imports System.Collections.Generic
Imports System.Drawing
Imports System.Linq
Imports System.Windows.Forms
Imports DevExpress.XtraEditors

''' <summary>Single snapshot implementation shared by <see cref="SchedulerNew"/> and <see cref="ApptHostCtl"/> (same as <c>CaptureSchedulerSnapshotBitmap</c> in the scheduler, plus <see cref="ApptWeekCtl"/> when the week layout is the new table).</summary>
Friend NotInheritable Class SchedulerSnapshotShared
    Private Sub New()
    End Sub

    ''' <param name="arrowHost">Where <see cref="ArrowLable"/> hints live: <c>pnlBody</c> in <see cref="SchedulerNew"/>, the appointment host for <see cref="ApptHostCtl"/>.</param>
    ''' <param name="gdiContext">For Day/Doctors GDI+ export (only available on a live <see cref="SchedulerNew"/>). Pass <see langword="Nothing"/> for the new Appt host (autoscroll full capture).</param>
    Friend Shared Function CaptureSnapshot(arrowHost As Control, pnlBody As Control, view As SchedulerNew.ViewMode, gdiContext As SchedulerNew) As Bitmap
        If pnlBody Is Nothing Then Return Nothing
        Dim hintBk As List(Of KeyValuePair(Of Control, Boolean)) = Nothing
        SnapshotHideArrowHints(arrowHost, hintBk)
        Try
            Select Case view
                Case SchedulerNew.ViewMode.DayView
                    If gdiContext IsNot Nothing Then
                        Dim dayBmp = gdiContext.SnapshotGdi_DayViewBitmap()
                        If dayBmp IsNot Nothing Then Return dayBmp
                    End If
                    Dim p = FindAutoscrollPanelDeep(pnlBody)
                    If p IsNot Nothing Then
                        Dim b = CaptureBodyAutoScrollFullBitmap(p)
                        If b IsNot Nothing Then Return b
                    End If
                Case SchedulerNew.ViewMode.ThisWeekFull, SchedulerNew.ViewMode.ThisWeek
                    Dim wk = FindDescendantApptWeekCtl(pnlBody)
                    If wk IsNot Nothing Then
                        Dim apptW = wk.CaptureStitchedWeekSnapshotBitmap()
                        If apptW IsNot Nothing Then Return apptW
                    End If
                    Dim wf = TryFindWeekDayColumnsMainFlow(pnlBody)
                    If wf IsNot Nothing Then
                        Dim wBmp = CaptureWeekColumnsFullHeightBitmap(wf, pnlBody.ClientSize.Width)
                        If wBmp IsNot Nothing Then Return wBmp
                    End If
                Case SchedulerNew.ViewMode.MonthlyWeek
                    Dim mwf = TryFindMonthWeeksMainFlow(pnlBody)
                    If mwf IsNot Nothing Then
                        Dim mBmp = CaptureMonthWeeksFullHeightBitmap(mwf, pnlBody.ClientSize.Width)
                        If mBmp IsNot Nothing Then Return mBmp
                    End If
                Case SchedulerNew.ViewMode.MonthView
                    Dim mwf = TryFindMonthWeeksMainFlow(pnlBody)
                    If mwf IsNot Nothing Then
                        Dim mBmp = CaptureMonthWeeksFullHeightBitmap(mwf, pnlBody.ClientSize.Width)
                        If mBmp IsNot Nothing Then Return mBmp
                    End If
                Case SchedulerNew.ViewMode.DaysTimeline
                    Dim adlForSnap As ApptDayLine = Nothing
                    If pnlBody IsNot Nothing AndAlso pnlBody.Controls.Count > 0 Then
                        adlForSnap = TryCast(pnlBody.Controls(0), ApptDayLine)
                        If adlForSnap IsNot Nothing Then adlForSnap.BeginSnapshotBitmapLayout()
                    End If
                    Try
                        If pnlBody IsNot Nothing AndAlso pnlBody.Controls.Count > 0 Then
                            Dim adl = TryCast(pnlBody.Controls(0), ApptDayLine)
                            If adl IsNot Nothing Then
                                Dim p0 = FindAutoscrollPanelDeep(adl)
                                If p0 IsNot Nothing Then
                                    Dim b0 = CaptureBodyAutoScrollFullBitmap(p0)
                                    If b0 IsNot Nothing Then Return b0
                                End If
                            End If
                        End If
                        Dim tl = FindTimelineOrScrollBodyPanel(pnlBody)
                        If tl Is Nothing Then tl = FindAutoscrollPanelDeep(pnlBody)
                        If tl IsNot Nothing Then
                            Dim tBmp = CaptureBodyAutoScrollFullBitmap(tl)
                            If tBmp IsNot Nothing Then Return tBmp
                        End If
                    Finally
                        If adlForSnap IsNot Nothing Then adlForSnap.EndSnapshotBitmapLayout()
                    End Try
                Case SchedulerNew.ViewMode.DoctorsDay
                    If gdiContext IsNot Nothing Then
                        Dim ddBmp = gdiContext.SnapshotGdi_DoctorsDayBitmap()
                        If ddBmp IsNot Nothing Then Return ddBmp
                    End If
                    Dim p2 = FindAutoscrollPanelDeep(pnlBody)
                    If p2 IsNot Nothing Then
                        Dim b2 = CaptureBodyAutoScrollFullBitmap(p2)
                        If b2 IsNot Nothing Then Return b2
                    End If
            End Select
            Return CapturePnlBodyClientBitmap(pnlBody)
        Finally
            SnapshotRestoreArrowHints(hintBk)
        End Try
    End Function

#Region "Arrow hints (SchedulerNew.SnapshotHideArrowHints on arrowHost)"
    Private Shared Sub SnapshotHideArrowHints(arrowHost As Control, ByRef backup As List(Of KeyValuePair(Of Control, Boolean)))
        backup = New List(Of KeyValuePair(Of Control, Boolean))()
        If arrowHost Is Nothing Then Return
        For Each c As Control In arrowHost.Controls
            If TryCast(c, ArrowLable) Is Nothing Then Continue For
            backup.Add(New KeyValuePair(Of Control, Boolean)(c, c.Visible))
            c.Visible = False
        Next
    End Sub

    Private Shared Sub SnapshotRestoreArrowHints(backup As List(Of KeyValuePair(Of Control, Boolean)))
        If backup Is Nothing Then Return
        For Each kvp In backup
            Try
                If kvp.Key IsNot Nothing AndAlso Not kvp.Key.IsDisposed Then kvp.Key.Visible = kvp.Value
            Catch
            End Try
        Next
    End Sub
#End Region

#Region "Layout finders (identical to SchedulerNew — direct pnlBody children, except autoscroll deep for Appt host)"
    ''' <summary>Week 6/7 layout: main vertical flow whose second child is the horizontal day-column band (ignores PREV/NEXT <see cref="ArrowLable"/> siblings on pnlBody).</summary>
    Private Shared Function TryFindWeekDayColumnsMainFlow(pnlBody As Control) As FlowLayoutPanel
        If pnlBody Is Nothing Then Return Nothing
        For Each c As Control In pnlBody.Controls
            If TryCast(c, ArrowLable) IsNot Nothing Then Continue For
            Dim mf = TryCast(c, FlowLayoutPanel)
            If mf IsNot Nothing AndAlso mf.FlowDirection = FlowDirection.TopDown Then
                If mf.Controls.Count >= 2 Then
                    Dim band = TryCast(mf.Controls(1), FlowLayoutPanel)
                    If band IsNot Nothing AndAlso band.FlowDirection = FlowDirection.LeftToRight AndAlso band.Controls.Count >= 3 Then
                        Dim columnsOk = False
                        For Each bc As Control In band.Controls
                            Dim pcol = TryCast(bc, Panel)
                            If pcol Is Nothing Then Continue For
                            If pcol.Controls.OfType(Of FlowLayoutPanel)().Any() Then
                                columnsOk = True
                                Exit For
                            End If
                        Next
                        If columnsOk Then Return mf
                    End If
                End If
            End If
            Dim nested = TryFindWeekDayColumnsMainFlow(c)
            If nested IsNot Nothing Then Return nested
        Next
        Return Nothing
    End Function

    Private Shared Function FindDescendantApptWeekCtl(root As Control) As ApptWeekCtl
        If root Is Nothing Then Return Nothing
        For Each c As Control In root.Controls
            If TryCast(c, ArrowLable) IsNot Nothing Then Continue For
            Dim wk = TryCast(c, ApptWeekCtl)
            If wk IsNot Nothing Then Return wk
            Dim nested = FindDescendantApptWeekCtl(c)
            If nested IsNot Nothing Then Return nested
        Next
        Return Nothing
    End Function

    Private Shared Function TryFindMonthWeeksMainFlow(pnlBody As Control) As FlowLayoutPanel
        If pnlBody Is Nothing Then Return Nothing
        For Each c As Control In pnlBody.Controls
            If TryCast(c, ArrowLable) IsNot Nothing Then Continue For
            Dim mf = TryCast(c, FlowLayoutPanel)
            If mf IsNot Nothing AndAlso mf.FlowDirection = FlowDirection.TopDown AndAlso mf.Controls.Count > 0 AndAlso TryCast(mf.Controls(0), GroupControl) IsNot Nothing Then
                Return mf
            End If
            Dim nested = TryFindMonthWeeksMainFlow(c)
            If nested IsNot Nothing Then Return nested
        Next
        Return Nothing
    End Function

    Private Shared Function TryGetWeekDaysBand(mainFlow As FlowLayoutPanel) As FlowLayoutPanel
        Dim df = TryCast(mainFlow.Controls(1), FlowLayoutPanel)
        If df Is Nothing OrElse df.FlowDirection <> FlowDirection.LeftToRight Then Return Nothing
        Return df
    End Function

    ''' <summary>Month-week layout: vertical flow of <see cref="GroupControl"/> week groups.</summary>
    Private Shared Function FindTimelineOrScrollBodyPanel(pnlBody As Control) As Panel
        If pnlBody Is Nothing Then Return Nothing
        For Each c As Control In pnlBody.Controls
            If TryCast(c, ArrowLable) IsNot Nothing Then Continue For
            Dim p = TryCast(c, Panel)
            If p Is Nothing OrElse Not p.AutoScroll Then Continue For
            Return p
        Next
        Return Nothing
    End Function

    ''' <summary>First <see cref="Panel"/> with <see cref="Panel.AutoScroll"/> in the subtree (new appt day/timeline is nested in one child, unlike classic scheduler).</summary>
    Private Shared Function FindAutoscrollPanelDeep(root As Control) As Panel
        If root Is Nothing Then Return Nothing
        If TryCast(root, ArrowLable) Is Nothing Then
            Dim p = TryCast(root, Panel)
            If p IsNot Nothing AndAlso p.AutoScroll Then Return p
        End If
        For Each c As Control In root.Controls
            If TryCast(c, ArrowLable) IsNot Nothing Then Continue For
            Dim d = FindAutoscrollPanelDeep(c)
            If d IsNot Nothing Then Return d
        Next
        Return Nothing
    End Function
#End Region

#Region "GDI+ bitmap helpers (same as SchedulerNew)"
    Private Shared Function SnapBmpW(control As Control) As Integer
        Return Math.Max(1, control.Width)
    End Function

    Private Shared Function SnapBmpH(control As Control) As Integer
        Return Math.Max(1, control.Height)
    End Function

    Private Shared Sub AccumulateControlSubtreeExtents(root As Control, ByRef maxRight As Integer, ByRef maxBottom As Integer)
        If root Is Nothing Then Return
        For Each c As Control In root.Controls
            maxRight = Math.Max(maxRight, c.Right)
            maxBottom = Math.Max(maxBottom, c.Bottom)
            If c.HasChildren Then AccumulateControlSubtreeExtents(c, maxRight, maxBottom)
        Next
    End Sub

    Private Shared Function CaptureWeekColumnsFullHeightBitmap(mainFlow As FlowLayoutPanel, pnlBodyClientW As Integer) As Bitmap
        If mainFlow Is Nothing Then Return Nothing
        Dim daysFlow = TryGetWeekDaysBand(mainFlow)
        If daysFlow Is Nothing OrElse daysFlow.Controls.Count = 0 Then Return Nothing

        Dim savedMainAutoScroll = mainFlow.AutoScroll
        Dim savedDock = mainFlow.Dock
        Dim colHeights As New List(Of Integer)

        For Each c As Control In daysFlow.Controls
            Dim col = TryCast(c, Panel)
            If col Is Nothing Then Continue For
            Dim doctorsFlow = col.Controls.OfType(Of FlowLayoutPanel)().FirstOrDefault()
            If doctorsFlow Is Nothing Then Continue For
            col.AutoScroll = False
            Dim flowW = Math.Max(40, col.Width - 10)
            doctorsFlow.Width = flowW
            doctorsFlow.PerformLayout()
            Dim prefH = doctorsFlow.GetPreferredSize(New Size(flowW, 0)).Height
            colHeights.Add(doctorsFlow.Top + prefH + 5)
        Next

        If colHeights.Count = 0 Then Return Nothing

        Dim maxColH = colHeights.Max()
        For Each c As Control In daysFlow.Controls
            Dim col = TryCast(c, Panel)
            If col Is Nothing Then Continue For
            Dim doctorsFlow = col.Controls.OfType(Of FlowLayoutPanel)().FirstOrDefault()
            If doctorsFlow Is Nothing Then Continue For
            col.SuspendLayout()
            col.AutoScroll = False
            col.Height = maxColH
            doctorsFlow.Height = Math.Max(1, maxColH - doctorsFlow.Top - 5)
            col.ResumeLayout(True)
        Next

        daysFlow.SuspendLayout()
        daysFlow.Height = maxColH + daysFlow.Padding.Top + daysFlow.Padding.Bottom
        daysFlow.ResumeLayout(True)

        mainFlow.SuspendLayout()
        mainFlow.AutoScroll = False
        mainFlow.Dock = DockStyle.None
        Dim bodyW = Math.Max(1, pnlBodyClientW)
        mainFlow.Width = bodyW

        Dim maxBottom = 0
        For Each c As Control In mainFlow.Controls
            maxBottom = Math.Max(maxBottom, c.Bottom)
        Next
        Dim totalH = Math.Max(1, maxBottom + mainFlow.Padding.Bottom)
        mainFlow.Height = totalH
        mainFlow.ResumeLayout(True)
        mainFlow.PerformLayout()
        mainFlow.Refresh()

        Dim bmpW = mainFlow.Width
        Dim bmpH = mainFlow.Height
        If bmpW <= 0 OrElse bmpH <= 0 Then
            mainFlow.Dock = savedDock
            mainFlow.AutoScroll = savedMainAutoScroll
            Return Nothing
        End If

        Dim bmp As New Bitmap(bmpW, bmpH)
        Try
            mainFlow.DrawToBitmap(bmp, New Rectangle(0, 0, bmpW, bmpH))
        Catch
            bmp.Dispose()
            Return Nothing
        End Try

        mainFlow.Dock = savedDock
        mainFlow.AutoScroll = savedMainAutoScroll
        Return bmp
    End Function

    Private Shared Function CaptureBodyAutoScrollFullBitmap(content As Panel) As Bitmap
        If content Is Nothing Then Return Nothing
        Dim minSz = content.AutoScrollMinSize
        Dim maxR = 0
        Dim maxB = 0
        AccumulateControlSubtreeExtents(content, maxR, maxB)
        Const slack As Integer = 12
        Dim w = Math.Max(Math.Max(content.ClientSize.Width, minSz.Width), maxR + content.Padding.Right + slack)
        Dim h = Math.Max(Math.Max(content.ClientSize.Height, minSz.Height), maxB + content.Padding.Bottom + slack)

        Dim savedAuto = content.AutoScroll
        Dim savedDock = content.Dock
        content.SuspendLayout()
        content.AutoScroll = False
        content.Dock = DockStyle.None
        content.Size = New Size(Math.Max(1, w), Math.Max(1, h))
        content.ResumeLayout(True)
        content.PerformLayout()
        content.Refresh()

        Dim bmpW = SnapBmpW(content)
        Dim bmpH = SnapBmpH(content)
        Dim bmp As Bitmap = Nothing
        Try
            bmp = New Bitmap(bmpW, bmpH)
            content.DrawToBitmap(bmp, New Rectangle(0, 0, bmpW, bmpH))
        Catch
            If bmp IsNot Nothing Then bmp.Dispose()
            bmp = Nothing
        Finally
            content.Dock = savedDock
            content.AutoScroll = savedAuto
        End Try
        Return bmp
    End Function

    Private Shared Function CaptureMonthWeeksFullHeightBitmap(mainFlow As FlowLayoutPanel, pnlBodyClientW As Integer) As Bitmap
        If mainFlow Is Nothing Then Return Nothing
        Const groupChromeH As Integer = 36
        mainFlow.SuspendLayout()
        Dim savedDock = mainFlow.Dock
        Dim savedAuto = mainFlow.AutoScroll
        mainFlow.AutoScroll = False

        For Each grpC As Control In mainFlow.Controls
            Dim grp = TryCast(grpC, GroupControl)
            If grp Is Nothing Then Continue For
            Dim flow = grp.Controls.OfType(Of FlowLayoutPanel)().FirstOrDefault()
            If flow Is Nothing Then Continue For
            flow.SuspendLayout()
            flow.Dock = DockStyle.None
            flow.Width = Math.Max(1, grp.ClientSize.Width - grp.Padding.Horizontal)

            Dim maxDayBottom = 0
            For Each dayBox As Control In flow.Controls
                Dim p = TryCast(dayBox, Panel)
                If p Is Nothing Then Continue For
                Dim lst = p.Controls.OfType(Of ListBox)().FirstOrDefault()
                If lst Is Nothing Then Continue For
                lst.ScrollAlwaysVisible = False
                lst.IntegralHeight = False
                Dim ih = Math.Max(15, lst.ItemHeight)
                lst.Height = If(lst.Items.Count > 0, ih * lst.Items.Count + 6, ih)
                p.Height = lst.Bottom + 8
                maxDayBottom = Math.Max(maxDayBottom, p.Bottom)
            Next

            flow.Height = maxDayBottom + flow.Padding.Vertical
            flow.ResumeLayout(True)
            flow.PerformLayout()
            Dim innerRight = 0
            For Each box As Control In flow.Controls
                innerRight = Math.Max(innerRight, box.Right)
            Next
            Dim needFlowW = innerRight + flow.Padding.Right
            flow.Width = Math.Max(flow.Width, needFlowW)
            grp.Width = Math.Max(grp.Width, flow.Width + grp.Padding.Horizontal + 8)
            grp.Height = flow.Height + grp.Padding.Vertical + groupChromeH
        Next

        mainFlow.Dock = DockStyle.None
        Dim maxGrpRight = 0
        For Each c As Control In mainFlow.Controls
            maxGrpRight = Math.Max(maxGrpRight, c.Right)
        Next
        mainFlow.Width = Math.Max(Math.Max(1, pnlBodyClientW), maxGrpRight + mainFlow.Padding.Horizontal + 8)
        Dim maxBottom = 0
        For Each c As Control In mainFlow.Controls
            maxBottom = Math.Max(maxBottom, c.Bottom)
        Next
        mainFlow.Height = Math.Max(1, maxBottom + mainFlow.Padding.Bottom)
        mainFlow.ResumeLayout(True)
        mainFlow.PerformLayout()
        mainFlow.Refresh()

        Dim bw = SnapBmpW(mainFlow)
        Dim bh = SnapBmpH(mainFlow)
        Dim bmp As Bitmap = Nothing
        Try
            bmp = New Bitmap(bw, bh)
            mainFlow.DrawToBitmap(bmp, New Rectangle(0, 0, bw, bh))
        Catch
            If bmp IsNot Nothing Then bmp.Dispose()
            bmp = Nothing
        Finally
            mainFlow.Dock = savedDock
            mainFlow.AutoScroll = savedAuto
        End Try
        Return bmp
    End Function

    Private Shared Function CapturePnlBodyClientBitmap(pnlBody As Control) As Bitmap
        If pnlBody Is Nothing OrElse pnlBody.Width <= 0 OrElse pnlBody.Height <= 0 Then Return Nothing
        Dim bmp As New Bitmap(pnlBody.Width, pnlBody.Height)
        Try
            pnlBody.DrawToBitmap(bmp, New Rectangle(0, 0, pnlBody.Width, pnlBody.Height))
            Return bmp
        Catch
            bmp.Dispose()
            Return Nothing
        End Try
    End Function
#End Region
End Class
