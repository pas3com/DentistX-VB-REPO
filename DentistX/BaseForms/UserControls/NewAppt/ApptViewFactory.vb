Imports System
Imports System.Drawing
Imports System.Windows.Forms
Imports DevExpress.Utils
Imports DevExpress.XtraEditors

Public Class ApptViewFactory
    Public Function Create(viewMode As ApptViewMode) As XtraUserControl
        Select Case viewMode
            Case ApptViewMode.ThisWeek, ApptViewMode.ThisWeekFull
                Return New ApptWeekCtl()
            Case ApptViewMode.DayView
                Return New ApptDayCtl()
            Case ApptViewMode.MonthView, ApptViewMode.MonthlyWeek
                Return New ApptMonthCtl()
            Case ApptViewMode.DaysTimeline
                Return New ApptDayLine()
            Case ApptViewMode.DoctorsDay
                Return New ApptDayDoctors()
            Case Else
                Throw New ArgumentOutOfRangeException(NameOf(viewMode), viewMode, Nothing)
        End Select
    End Function
End Class

Public MustInherit Class ApptPlaceholderCtlBase
    Inherits XtraUserControl
    Implements IApptViewCtl

    Private ReadOnly _messageLabel As LabelControl

    Protected Sub New(title As String)
        Appearance.BackColor = Color.White
        Appearance.Options.UseBackColor = True

        _messageLabel = New LabelControl() With {
            .Dock = DockStyle.Fill,
            .AutoSizeMode = LabelAutoSizeMode.None,
            .Text = $"{title} modular control is prepared. The host can already swap to this control independently."
        }
        _messageLabel.Appearance.Font = CreateCalibriFont(12.0F, FontStyle.Bold)
        _messageLabel.Appearance.Options.UseFont = True
        _messageLabel.Appearance.ForeColor = Color.FromArgb(77, 85, 102)
        _messageLabel.Appearance.Options.UseForeColor = True
        _messageLabel.Appearance.TextOptions.WordWrap = WordWrap.Wrap
        _messageLabel.Appearance.TextOptions.HAlignment = HorzAlignment.Center
        _messageLabel.Appearance.TextOptions.VAlignment = VertAlignment.Center
        _messageLabel.Appearance.Options.UseTextOptions = True
        Controls.Add(_messageLabel)
    End Sub

    Public Property InteractionHub As ApptInteractionHub Implements IApptViewCtl.InteractionHub

    Public Overridable Sub BindData(request As ApptViewRequest) Implements IApptViewCtl.BindData
    End Sub
End Class
