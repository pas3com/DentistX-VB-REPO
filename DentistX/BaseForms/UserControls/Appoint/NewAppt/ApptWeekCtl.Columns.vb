Imports System
Imports System.Collections.Generic
Imports System.Drawing
Imports System.Globalization
Imports System.Linq
Imports System.Runtime.InteropServices
Imports System.Windows.Forms
Imports DevExpress.Utils
Imports DevExpress.Utils.Layout
Imports DevExpress.XtraEditors

Partial Public Class ApptWeekCtl
    Private Sub LayoutDayPanelHeader(headerPanel As PanelControl, titleLabel As LabelControl, dateLabel As LabelControl, maxLabel As Label)
        If headerPanel Is Nothing OrElse titleLabel Is Nothing OrElse dateLabel Is Nothing OrElse maxLabel Is Nothing Then Return

        Const badgeWidth As Integer = 42
        Const badgeHeight As Integer = 24
        Const sidePad As Integer = 6

        Dim badgeLeft As Integer = If(Eng, headerPanel.ClientSize.Width - badgeWidth - sidePad, sidePad)
        maxLabel.SetBounds(Math.Max(0, badgeLeft), 8, badgeWidth, badgeHeight)

        Dim textLeft As Integer = If(Eng, sidePad, badgeWidth + sidePad * 2)
        Dim textRight As Integer = If(Eng, badgeWidth + sidePad * 2, sidePad)
        Dim textWidth As Integer = Math.Max(40, headerPanel.ClientSize.Width - textLeft - textRight)
        titleLabel.SetBounds(textLeft, 1, textWidth, 18)
        dateLabel.SetBounds(textLeft, 18, textWidth, Math.Max(18, headerPanel.ClientSize.Height - 20))
    End Sub

    Private Function CurrentCardAppearanceSelector() As Func(Of ApptCardVm, ApptCardAppearance)
        Return If(_request Is Nothing, Nothing, _request.AppointmentAppearanceSelector)
    End Function

    Friend Sub PrepareReusableWeekCard(card As ApptCard, dayDate As Date)
        If card Is Nothing Then Return
        Dim ctx = TryCast(card.Tag, WeekReusableCardContext)
        If ctx Is Nothing Then
            ctx = New WeekReusableCardContext()
            card.Tag = ctx
        End If
        ctx.DayDate = dayDate.Date
        If ctx.IsWired Then Return

        AddHandler card.AppointmentClicked, AddressOf ReusableWeekCard_AppointmentClicked
        AddHandler card.AppointmentDoubleClicked, AddressOf ReusableWeekCard_AppointmentDoubleClicked
        AddHandler card.StatusContextEditRequested, AddressOf ReusableWeekCard_StatusContextEditRequested
        AddHandler card.StatusChangeRequested, AddressOf ReusableWeekCard_StatusChangeRequested
        AddHandler card.CardDragMouseDown, AddressOf ReusableWeekCard_CardDragMouseDown
        AddHandler card.CardDragMouseUp, AddressOf ReusableWeekCard_CardDragMouseUp
        AddHandler card.DragEnter, AddressOf ReusableWeekCard_DragEnter
        AddHandler card.DragDrop, AddressOf ReusableWeekCard_DragDrop
        card.AllowDrop = True
        ctx.IsWired = True
    End Sub

    Private Shared Function GetReusableWeekCardDay(card As ApptCard) As Date
        Dim ctx = TryCast(If(card Is Nothing, Nothing, card.Tag), WeekReusableCardContext)
        If ctx Is Nothing Then Return Date.MinValue
        Return ctx.DayDate
    End Function

    Private Sub ReusableWeekCard_AppointmentClicked(ap As AppointmentC)
        If InteractionHub IsNot Nothing Then InteractionHub.PublishAppointmentClicked(ap)
    End Sub

    Private Sub ReusableWeekCard_AppointmentDoubleClicked(ap As AppointmentC)
        If InteractionHub IsNot Nothing Then InteractionHub.PublishAppointmentDoubleClicked(ap)
    End Sub

    Private Sub ReusableWeekCard_StatusContextEditRequested(ap As AppointmentC)
        If InteractionHub IsNot Nothing Then InteractionHub.PublishAppointmentDoubleClicked(ap)
    End Sub

    Private Sub ReusableWeekCard_StatusChangeRequested(ap As AppointmentC, statusKey As String, col As Color)
        If InteractionHub IsNot Nothing Then InteractionHub.PublishAppointmentStatusChange(ap, statusKey, col)
    End Sub

    Private Sub ReusableWeekCard_CardDragMouseDown(card As ApptCard, e As MouseEventArgs)
        OnWeekCardDragMouseDown(card, GetReusableWeekCardDay(card), e)
    End Sub

    Private Sub ReusableWeekCard_CardDragMouseUp(card As ApptCard, e As MouseEventArgs)
        OnWeekCardDragMouseUp(card, e)
    End Sub

    Private Sub ReusableWeekCard_DragEnter(sender As Object, e As DragEventArgs)
        WeekColumn_DragEnter(e)
    End Sub

    Private Sub ReusableWeekCard_DragDrop(sender As Object, e As DragEventArgs)
        Dim card = TryCast(sender, ApptCard)
        If card Is Nothing Then Return
        Dim targetDay = GetReusableWeekCardDay(card)
        If targetDay = Date.MinValue Then Return
        WeekColumn_DragDrop(targetDay, e)
    End Sub

    Private Function EnsureWeekDayCardPool(cardsLayer As PanelControl, requiredCount As Integer, dayDate As Date) As List(Of ApptCard)
        Return AppointmentCardPool.Ensure(cardsLayer, requiredCount, dayDate,
            Sub(card, d)
                card.Dock = DockStyle.None
                PrepareReusableWeekCard(card, d)
            End Sub)
    End Function

    Private Function GetOrCreateWeekCompactStrip(host As Control) As WeekDayCompactApptStrip
        If host Is Nothing Then Return Nothing
        Dim existing = host.Controls.OfType(Of WeekDayCompactApptStrip)().FirstOrDefault()
        If existing IsNot Nothing Then Return existing
        Dim strip As New WeekDayCompactApptStrip()
        host.Controls.Add(strip)
        host.Controls.SetChildIndex(strip, 0)
        Return strip
    End Function

    Private Function GetOrCreateWeekEmptyLabel(host As Control, controlName As String, emptyText As String) As LabelControl
        If host Is Nothing Then Return Nothing
        Dim existing = host.Controls.OfType(Of LabelControl)().FirstOrDefault(Function(lbl) String.Equals(lbl.Name, controlName, StringComparison.Ordinal))
        If existing IsNot Nothing Then
            existing.Text = emptyText
            existing.Visible = True
            Return existing
        End If

        Dim emptyLabel = New LabelControl() With {
            .Name = controlName,
            .Dock = DockStyle.None,
            .AutoSizeMode = LabelAutoSizeMode.None,
            .Height = 48,
            .Text = emptyText
        }
        emptyLabel.Appearance.Font = CreateCalibriFont(10.0F, FontStyle.Italic)
        emptyLabel.Appearance.Options.UseFont = True
        emptyLabel.Appearance.ForeColor = Color.Gray
        emptyLabel.Appearance.Options.UseForeColor = True
        emptyLabel.Appearance.TextOptions.HAlignment = HorzAlignment.Center
        emptyLabel.Appearance.TextOptions.VAlignment = VertAlignment.Center
        emptyLabel.Appearance.Options.UseTextOptions = True
        AddHandler emptyLabel.DoubleClick, AddressOf WeekEmptyLabel_DoubleClick
        host.Controls.Add(emptyLabel)
        Return emptyLabel
    End Function

    Private Function GetOrCreateWeekMoreLabel(host As Control) As LabelControl
        If host Is Nothing Then Return Nothing
        Dim existing = host.Controls.OfType(Of LabelControl)().FirstOrDefault(Function(lbl) String.Equals(lbl.Name, "weekMoreLabel", StringComparison.Ordinal))
        If existing IsNot Nothing Then Return existing

        Dim moreLabel = New LabelControl() With {
            .Name = "weekMoreLabel",
            .Dock = DockStyle.None,
            .AutoSizeMode = LabelAutoSizeMode.None,
            .Height = 34,
            .Cursor = Cursors.Hand,
            .Visible = False
        }
        moreLabel.Appearance.Font = CreateCalibriFont(9.0F, FontStyle.Italic)
        moreLabel.Appearance.Options.UseFont = True
        moreLabel.Appearance.ForeColor = Color.FromArgb(37, 99, 235)
        moreLabel.Appearance.Options.UseForeColor = True
        moreLabel.Appearance.TextOptions.HAlignment = HorzAlignment.Center
        moreLabel.Appearance.TextOptions.VAlignment = VertAlignment.Center
        moreLabel.Appearance.Options.UseTextOptions = True
        AddHandler moreLabel.MouseClick, AddressOf WeekMoreLabel_MouseClick
        host.Controls.Add(moreLabel)
        host.Controls.SetChildIndex(moreLabel, 0)
        Return moreLabel
    End Function

    Private Shared Function ResolveTaggedDate(ctrl As Control) As Date
        If ctrl Is Nothing OrElse ctrl.Tag Is Nothing Then Return Date.MinValue
        If TypeOf ctrl.Tag Is DateTime Then Return CType(ctrl.Tag, DateTime).Date
        If TypeOf ctrl.Tag Is Date Then Return CDate(ctrl.Tag).Date
        Dim ctx = TryCast(ctrl.Tag, WeekReusableCardContext)
        If ctx IsNot Nothing Then Return ctx.DayDate
        Return Date.MinValue
    End Function

    Private Sub WeekMoreLabel_MouseClick(sender As Object, e As MouseEventArgs)
        If e.Button <> MouseButtons.Left Then Return
        If _request Is Nothing OrElse _request.State Is Nothing OrElse _request.Data Is Nothing Then Return
        Dim targetDay = ResolveTaggedDate(TryCast(sender, Control))
        If targetDay = Date.MinValue Then Return
        ShowWeekDayDrawer(targetDay, _request.State, _request.Data)
    End Sub

    Private Sub WeekEmptyLabel_DoubleClick(sender As Object, e As EventArgs)
        Dim targetDay = ResolveTaggedDate(TryCast(sender, Control))
        If targetDay = Date.MinValue Then Return
        RaiseEmptyDate(targetDay)
    End Sub

    Private Sub PopulateDayCards(cardsLayer As PanelControl, dayDate As DateTime, state As ApptState, data As ApptDataBundle)
        cardsLayer.SuspendLayout()
        Try
            cardsLayer.BackColor = Color.Transparent
            Dim fullDayAppointments = BuildWeekDayAppointments(dayDate, data, state)
            Dim dayAppointments = fullDayAppointments
            Dim emptyText = If(Eng, "No appointments", "لا توجد مواعيد")
            Dim emptyLabel = GetOrCreateWeekEmptyLabel(cardsLayer, "weekEmptyLabel", emptyText)
            Dim moreLabel = GetOrCreateWeekMoreLabel(cardsLayer)
            Dim compactStrip As WeekDayCompactApptStrip = Nothing
            If WeekViewColumnRenderMode = WeekColumnRenderMode.ApptCardWithCompactPaintOverflow Then
                compactStrip = GetOrCreateWeekCompactStrip(cardsLayer)
            End If
            If emptyLabel IsNot Nothing Then
                emptyLabel.Tag = dayDate.Date
                emptyLabel.Visible = False
            End If
            If moreLabel IsNot Nothing Then
                moreLabel.Tag = dayDate.Date
                moreLabel.Visible = False
            End If

            Dim totalDay = dayAppointments.Count
            Dim hiddenAfterCap = 0
            If WeekViewMaxApptCardsPerDay > 0 AndAlso totalDay > WeekViewMaxApptCardsPerDay Then
                hiddenAfterCap = totalDay - WeekViewMaxApptCardsPerDay
                dayAppointments = dayAppointments.Take(WeekViewMaxApptCardsPerDay).ToList()
            End If

            If dayAppointments.Count = 0 Then
                If emptyLabel IsNot Nothing Then
                    emptyLabel.Visible = True
                    emptyLabel.BringToFront()
                End If
                If compactStrip IsNot Nothing Then compactStrip.BindOverflow(Nothing, data, state.Use24HourFormat, 0)
                For Each pooledCard In cardsLayer.Controls.OfType(Of ApptCard)()
                    pooledCard.Visible = False
                Next
                LayoutDayCards(cardsLayer)
                Return
            End If

            Dim pooledCards = EnsureWeekDayCardPool(cardsLayer, dayAppointments.Count, dayDate.Date)
            For i = 0 To dayAppointments.Count - 1
                Dim appointment = dayAppointments(i)
                Dim card = pooledCards(i)
                card.Width = If(_compactSixDayWeek,
                    Math.Max(1, cardsLayer.Width - 2 * DayColumnCardHorizontalInsetSixDay),
                    Math.Max(190, cardsLayer.Width - 4))
                BindAppointmentCard(card, appointment, data, state, CurrentCardAppearanceSelector(), state.Use24HourFormat)
                card.Visible = True
            Next

            If hiddenAfterCap > 0 Then
                If moreLabel IsNot Nothing Then
                    moreLabel.Text = If(Eng,
                        $"+{hiddenAfterCap} more — click to show all",
                        $"و{hiddenAfterCap} إضافية — انقر لعرض الكل")
                    moreLabel.Visible = True
                    moreLabel.BringToFront()
                End If
            End If

            If compactStrip IsNot Nothing Then
                If WeekViewColumnRenderMode = WeekColumnRenderMode.ApptCardWithCompactPaintOverflow AndAlso hiddenAfterCap > 0 Then
                    Dim overflow = fullDayAppointments.Skip(WeekViewMaxApptCardsPerDay)
                    compactStrip.BindOverflow(overflow, data, state.Use24HourFormat, WeekViewCompactPaintOverflowMaxPreviewRows)
                Else
                    compactStrip.BindOverflow(Nothing, data, state.Use24HourFormat, 0)
                End If
            End If

            LayoutDayCards(cardsLayer)
        Finally
            cardsLayer.ResumeLayout(True)
        End Try
    End Sub

    ''' <param name="remeasureUnchangedWidth">When false, <see cref="ApptCard.ApplyContentHeightToFitForWeekView"/> runs only if the card width actually changed (resize / scrollbar toggle). Use true after data bind or snapshot.</param>
    Private Sub LayoutDayCards(cardsLayer As PanelControl, Optional remeasureUnchangedWidth As Boolean = True)
        Dim top = 8
        Dim hInset As Integer
        Dim width As Integer
        If _compactSixDayWeek Then
            hInset = DayColumnCardHorizontalInsetSixDay
            width = Math.Max(1, cardsLayer.Width - 2 * hInset)
        Else
            hInset = 2
            width = Math.Max(180, cardsLayer.Width - 4)
        End If

        Dim visibleCards = cardsLayer.Controls.OfType(Of ApptCard)().
            Where(Function(ctrl) ctrl.Visible).
            OrderBy(Function(ctrl) cardsLayer.Controls.GetChildIndex(ctrl)).
            Cast(Of Control)().
            ToList()
        Dim visibleOtherControls = cardsLayer.Controls.OfType(Of Control)().
            Where(Function(ctrl) ctrl.Visible AndAlso Not TypeOf ctrl Is ApptCard).
            OrderBy(Function(ctrl) cardsLayer.Controls.GetChildIndex(ctrl)).
            ToList()

        For Each ctrl As Control In visibleCards.Concat(visibleOtherControls)
            Dim wChanged = (ctrl.Width <> width)
            ctrl.Width = width
            Dim weekCard = TryCast(ctrl, ApptCard)
            If weekCard IsNot Nothing AndAlso (wChanged OrElse remeasureUnchangedWidth) Then
                weekCard.ApplyContentHeightToFitForWeekView()
            End If
            ctrl.Left = hInset
            ctrl.Top = top
            top += ctrl.Height + DayColumnCardVerticalGap
        Next

        cardsLayer.Height = Math.Max(40, top)
    End Sub

    ''' <summary>
    ''' Largest <see cref="VScrollBar.Value"/> that keeps the bottom of <paramref name="cardsLayer"/> at the viewport bottom (no empty tail).
    ''' Matches <c>Maximum - LargeChange + 1</c> when column scroll extent sets <c>Maximum = maxOffset + LargeChange - 1</c>.
    ''' </summary>
    Private Shared Function DayColumnMaxScrollValue(cardsLayer As PanelControl, bar As DevExpress.XtraEditors.VScrollBar) As Integer
        If cardsLayer Is Nothing OrElse bar Is Nothing Then Return 0
        Dim host = TryCast(cardsLayer.Parent, XtraScrollableControl)
        If host Is Nothing Then Return 0
        Dim ch = Math.Max(1, host.ClientSize.Height)
        Return Math.Max(0, cardsLayer.Height - ch)
    End Function

    Private Shared Sub DayColumnClampBarAndSyncTop(bar As DevExpress.XtraEditors.VScrollBar, cardsLayer As PanelControl)
        If bar Is Nothing OrElse cardsLayer Is Nothing Then Return
        Dim maxV = DayColumnMaxScrollValue(cardsLayer, bar)
        If bar.Value > maxV Then bar.Value = maxV
        If bar.Value < bar.Minimum Then bar.Value = bar.Minimum
        cardsLayer.Top = -bar.Value
    End Sub

    ''' <summary>Custom slim vertical scrollbar for day columns (same idea as SchedulerNew doctor strip horizontal bar).</summary>
    Private Sub DayColumn_DoMouseWheel(bar As DevExpress.XtraEditors.VScrollBar, cardsLayer As PanelControl, e As MouseEventArgs)
        If e Is Nothing OrElse e.Delta = 0 OrElse bar Is Nothing OrElse cardsLayer Is Nothing Then Return
        If Not bar.Visible Then Return
        Dim lines = Math.Max(1, SystemInformation.MouseWheelScrollLines)
        Dim stepPx = Math.Max(bar.SmallChange, bar.SmallChange * lines \ 2)
        Dim delta = If(e.Delta > 0, -stepPx, stepPx)
        Dim nv = bar.Value + delta
        Dim maxV = DayColumnMaxScrollValue(cardsLayer, bar)
        nv = Math.Max(bar.Minimum, Math.Min(maxV, nv))
        bar.Value = nv
        cardsLayer.Top = -bar.Value
    End Sub

    Private Sub AdjustDayScrollExtent(scrollHost As XtraScrollableControl, cardsLayer As PanelControl, bar As DevExpress.XtraEditors.VScrollBar)
        For Each dayColumn In _dayColumns
            If dayColumn.Scroll Is scrollHost AndAlso dayColumn.Cards Is cardsLayer AndAlso dayColumn.Bar Is bar Then
                dayColumn.AdjustScrollExtent()
                Return
            End If
        Next
    End Sub

    Private Sub RefreshAllDayColumnScrollExtents()
        If _workArea Is Nothing OrElse _dayColumns.Count = 0 Then Return
        For Each dayColumn In _dayColumns
            dayColumn.AdjustScrollExtent()
        Next
    End Sub

    Private Sub TryScrollToAppointment(ap As AppointmentC)
        If ap Is Nothing OrElse _request Is Nothing OrElse _dayColumns.Count = 0 Then Return
        Dim weekStart = GetWeekStartSaturday(_request.State.CurrentDate)
        Dim visibleDays = If(_compactSixDayWeek, 6, 7)
        Dim ix = CInt((ap.StartDateTime.Date - weekStart).TotalDays)
        If ix < 0 OrElse ix >= visibleDays OrElse ix >= _dayColumns.Count Then Return
        _dayColumns(ix).TryScrollToAppointment(ap)
    End Sub

    Private Sub LayoutWeekColumns(Optional useWorkAreaRedraw As Boolean = True)
        If _dayColumns.Count = 0 Then Return

        Dim hWnd = _workArea.Handle
        Dim batchPaint = useWorkAreaRedraw AndAlso _workArea.IsHandleCreated AndAlso hWnd <> IntPtr.Zero
        If batchPaint Then NativeWm.SendMessage(hWnd, NativeWm.WM_SETREDRAW, IntPtr.Zero, IntPtr.Zero)
        Try
            _workArea.SuspendLayout()
            _weekRoot.SuspendLayout()
            For Each dayColumn In _dayColumns
                dayColumn.Shell.SuspendLayout()
            Next
            Try
                Dim spacing = 2
                Dim width = Math.Max(0, _layout.ClientSize.Width)
                Dim height = Math.Max(0, _layout.ClientSize.Height)
                Dim columnWidth = Math.Max(140, (width - (spacing * (_dayColumns.Count - 1))) \ _dayColumns.Count)
                Dim x = 0

                For Each dayColumn In _dayColumns
                    dayColumn.Shell.SetBounds(x, 0, columnWidth, height)
                    x += columnWidth + spacing
                Next
            Finally
                For Each dayColumn In _dayColumns
                    dayColumn.Shell.ResumeLayout(True)
                Next
                _weekRoot.ResumeLayout(True)
                _workArea.ResumeLayout(True)
            End Try

            If WeekViewUseCoalescedScrollRefresh Then
                RefreshAllDayColumnScrollExtents()
            End If
        Finally
            If batchPaint Then
                NativeWm.SendMessage(hWnd, NativeWm.WM_SETREDRAW, New IntPtr(1), IntPtr.Zero)
                _workArea.Invalidate(True)
            End If
        End Try
    End Sub
End Class
