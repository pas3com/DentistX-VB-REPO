Public Class MonthCalendar


    Private WithEvents flowPanel As New FlowLayoutPanel
        Private containerPanel As New Panel
        Private dayLabels As New List(Of Label)
        Private _month As Integer = DateTime.Now.Month
        Private _year As Integer = DateTime.Now.Year

        Public Sub New()
            InitializeComponent()
            SetupControl()
        End Sub

        Private Sub SetupControl()
            ' Configure container panel
            containerPanel.Dock = DockStyle.Fill
            containerPanel.AutoScroll = True
            Me.Controls.Add(containerPanel)

            ' Configure flow layout panel
            flowPanel.AutoSize = True
            flowPanel.AutoSizeMode = AutoSizeMode.GrowAndShrink
            flowPanel.WrapContents = True
            flowPanel.Padding = New Padding(5)
            containerPanel.Controls.Add(flowPanel)
        End Sub

        Public Property Month As Integer
            Get
                Return _month
            End Get
            Set(value As Integer)
                _month = value
                GenerateCalendar()
            End Set
        End Property

        Public Property Year As Integer
            Get
                Return _year
            End Get
            Set(value As Integer)
                _year = value
                GenerateCalendar()
            End Set
        End Property

        Private Sub GenerateCalendar()
            flowPanel.Controls.Clear()
            dayLabels.Clear()

            ' Calculate days in month and first day
            Dim daysInMonth = DateTime.DaysInMonth(_year, _month)
            Dim firstDay = New DateTime(_year, _month, 1)
            Dim startDay = CInt(firstDay.DayOfWeek) ' Sunday = 0

            ' Add empty labels for days before the first day
            For i = 0 To startDay - 1
                AddLabel("")
            Next

            ' Add day labels
            For day = 1 To daysInMonth
                AddLabel(day.ToString())
            Next

            ' Adjust label sizes
            AdjustLabelSizes()
        End Sub

        Private Sub AddLabel(text As String)
            Dim lbl As New Label
            lbl.Text = text
            lbl.Margin = New Padding(2)
            lbl.TextAlign = ContentAlignment.MiddleCenter
            lbl.MinimumSize = New Size(30, 30)
            lbl.AutoSize = True
            lbl.BorderStyle = BorderStyle.FixedSingle
            dayLabels.Add(lbl)
            flowPanel.Controls.Add(lbl)
        End Sub

        Private Sub AdjustLabelSizes()
            If dayLabels.Count = 0 Then Return

            ' Calculate max width and height from visible labels
            Dim maxWidth = dayLabels.Where(Function(l) l.Text <> "").
                                Max(Function(l) l.PreferredWidth)
            Dim maxHeight = dayLabels.Where(Function(l) l.Text <> "").
                                 Max(Function(l) l.PreferredHeight)

            ' Set consistent size for all labels
            Dim labelSize = New Size(
                Math.Max(maxWidth + 10, 30),
                Math.Max(maxHeight + 10, 30))

            For Each lbl In dayLabels
                lbl.Size = labelSize
                lbl.Margin = New Padding(2)
            Next
        End Sub

        Protected Overrides Sub OnSizeChanged(e As EventArgs)
            MyBase.OnSizeChanged(e)
            If flowPanel IsNot Nothing Then
                flowPanel.MaximumSize = New Size(Me.ClientSize.Width - 10, 0)
            End If
        End Sub
    End Class
