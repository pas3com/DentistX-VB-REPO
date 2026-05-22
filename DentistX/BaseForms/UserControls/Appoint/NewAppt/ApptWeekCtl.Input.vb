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
    Protected Overrides Sub OnMouseWheel(e As MouseEventArgs)
        If HandleWeekViewMouseWheel(e) Then Return
        MyBase.OnMouseWheel(e)
    End Sub

    Private Function HandleWeekViewMouseWheel(e As MouseEventArgs) As Boolean
        If e Is Nothing OrElse e.Delta = 0 OrElse _dayColumns Is Nothing Then Return False
        If _dayColumns.Count = 0 Then Return False
        Dim p = Me.PointToClient(Control.MousePosition)
        For Each col In _dayColumns
            Dim d = col.Shell
            If d Is Nothing OrElse Not d.Visible Then Continue For
            Dim rScreen = d.RectangleToScreen(d.ClientRectangle)
            Dim r = Me.RectangleToClient(rScreen)
            If r.Contains(p) AndAlso col.Bar IsNot Nothing AndAlso col.Bar.Visible Then
                DayColumn_DoMouseWheel(col.Bar, col.Cards, e)
                Return True
            End If
        Next
        Return False
    End Function
End Class
