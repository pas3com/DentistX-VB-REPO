Imports System.Drawing
Imports System.Windows.Forms
Imports DevExpress.Utils
Imports DevExpress.XtraEditors

''' <summary>Shared flyout positioning for combo user controls (see PatientCombo).</summary>
Public NotInheritable Class ComboFlyoutSearchHelper
    Private Sub New()
    End Sub

    Public Shared Sub ApplyManualPositionBelowPanel(flyout As FlyoutPanel, panel As PanelControl, comboAnchor As Control)
        If flyout Is Nothing OrElse panel Is Nothing Then Return
        panel.PerformLayout()
        Dim y As Integer = panel.ClientSize.Height
        If comboAnchor IsNot Nothing Then
            y = Math.Max(y, comboAnchor.Bottom)
        End If
        If y <= 0 AndAlso panel.Height > 0 Then y = panel.Height
        Dim w As Integer = panel.ClientSize.Width
        If w <= 0 AndAlso panel.Width > 0 Then w = panel.Width
        flyout.OwnerControl = panel
        flyout.Options.AnchorType = DevExpress.Utils.Win.PopupToolWindowAnchor.Manual
        flyout.Options.Location = New Point(0, y)
        flyout.AutoSize = False
        If w > 0 Then flyout.Width = w
    End Sub

    Public Shared Sub ShowFlyoutSearchDeferred(flyout As FlyoutPanel, panel As PanelControl, comboAnchor As Control, txtSearch As TextEdit, syncCtrl As Control)
        syncCtrl.BeginInvoke(New MethodInvoker(Sub()
                                                   ApplyManualPositionBelowPanel(flyout, panel, comboAnchor)
                                                   flyout.ShowPopup()
                                                   syncCtrl.BeginInvoke(New MethodInvoker(Sub()
                                                                                              If txtSearch IsNot Nothing Then txtSearch.Focus()
                                                                                          End Sub))
                                               End Sub))
    End Sub
End Class
