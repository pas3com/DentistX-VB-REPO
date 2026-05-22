Imports System.Collections.Generic
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Linq
Imports System.Windows.Forms

''' <summary>Owner-draw preview rows for appointments that are not materialized as <see cref="ApptCard"/> controls.</summary>
Friend NotInheritable Class WeekDayCompactApptStrip
    Inherits Control

    Private Const RowHeight As Integer = 18
    Private Const RowGap As Integer = 2
    Private ReadOnly _appointments As New List(Of AppointmentC)()
    Private _data As ApptDataBundle
    Private _use24Hour As Boolean

    Public Sub New()
        SetStyle(ControlStyles.AllPaintingInWmPaint Or ControlStyles.OptimizedDoubleBuffer Or ControlStyles.UserPaint Or ControlStyles.ResizeRedraw, True)
        DoubleBuffered = True
        TabStop = False
        Cursor = Cursors.Hand
        BackColor = Color.White
        Font = CreateCalibriFont(8.5F, FontStyle.Regular)
        Visible = False
        Name = "weekCompactApptStrip"
    End Sub

    Public Sub BindOverflow(appointments As IEnumerable(Of AppointmentC), data As ApptDataBundle, use24Hour As Boolean, maxPreviewRows As Integer)
        _data = data
        _use24Hour = use24Hour
        _appointments.Clear()
        If appointments Is Nothing Then
            Visible = False
            Height = 0
            Invalidate()
            Return
        End If
        Dim take = Math.Max(0, maxPreviewRows)
        For Each ap In appointments
            If ap Is Nothing Then Continue For
            _appointments.Add(ap)
            If take > 0 AndAlso _appointments.Count >= take Then Exit For
        Next
        Visible = _appointments.Count > 0
        Height = If(Visible, 6 + _appointments.Count * RowHeight + Math.Max(0, _appointments.Count - 1) * RowGap, 0)
        Invalidate()
    End Sub

    Protected Overrides Sub OnPaintBackground(e As PaintEventArgs)
        Using br As New SolidBrush(BackColor)
            e.Graphics.FillRectangle(br, ClientRectangle)
        End Using
    End Sub

    Protected Overrides Sub OnPaint(e As PaintEventArgs)
        If _appointments.Count = 0 Then Return
        Dim g = e.Graphics
        g.SmoothingMode = SmoothingMode.AntiAlias
        Dim y = 2
        For Each ap In _appointments
            Dim accent = ResolveAccentColor(ap)
            Using fill As New SolidBrush(Color.FromArgb(28, accent))
                g.FillRectangle(fill, 0, y, Width, RowHeight)
            End Using
            Using edge As New Pen(Color.FromArgb(120, accent), 1.0F)
                g.DrawRectangle(edge, 0, y, Math.Max(0, Width - 1), RowHeight)
            End Using
            Dim patient = If(_data Is Nothing, $"Patient {ap.PatientID}", _data.ResolvePatientName(ap.PatientID))
            Dim timeText = ApptTheme.FormatAppointmentTime(ap.StartDateTime, _use24Hour)
            Dim text = $"{timeText}  {patient}"
            TextRenderer.DrawText(g, text, Font, New Rectangle(6, y, Math.Max(0, Width - 8), RowHeight), Color.FromArgb(55, 65, 81),
                                  TextFormatFlags.Left Or TextFormatFlags.VerticalCenter Or TextFormatFlags.EndEllipsis Or TextFormatFlags.NoPrefix)
            y += RowHeight + RowGap
        Next
    End Sub

    Private Shared Function ResolveAccentColor(ap As AppointmentC) As Color
        If ap Is Nothing Then Return Color.FromArgb(70, 160, 255)
        Dim colors = ApptTheme.GetStandardAppointmentStatusColors()
        If Not String.IsNullOrWhiteSpace(ap.Status) AndAlso colors.ContainsKey(ap.Status) Then
            Return colors(ap.Status)
        End If
        Return Color.FromArgb(70, 160, 255)
    End Function
End Class
