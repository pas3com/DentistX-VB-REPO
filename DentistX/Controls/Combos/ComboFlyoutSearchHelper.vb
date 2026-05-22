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

    ''' <summary>Hide a combo's local search flyout before opening a modal (nested FlyoutPanels + ShowDialog() can corrupt the parent patient flyout).</summary>
    Public Shared Sub HideFlyoutIfOpen(flyout As FlyoutPanel)
        If flyout Is Nothing OrElse flyout.IsDisposed Then Return
        Try
            flyout.HidePopup(False)
        Catch
        End Try
    End Sub

    ''' <summary>
    ''' Prefer <see cref="MainView3"/> / <see cref="MainView1"/> as the modal owner so <c>ShowDialog</c> is not tied to a DevExpress FlyoutPanel HWND
    ''' (otherwise closing the dialog can dispose or break the patient details flyout).
    ''' </summary>
    Public Shared Function TryGetApplicationShellForm() As Form
        For Each f As Form In Application.OpenForms
            If TypeOf f Is MainView3 OrElse TypeOf f Is MainView1 Then Return f
        Next
        Return TryCast(Form.ActiveForm, Form)
    End Function

    ''' <summary>
    ''' Manual flyout: <paramref name="manualPointInOwnerClient"/> is in <paramref name="ownerControl"/>'s client space
    ''' (legacy jaws used the jaw <c>UserControl</c> as <c>Flyout1.OwnerControl</c>).
    ''' </summary>
    Public Shared Sub ConfigureManualAnchoredFlyoutOnOwner(flyout As FlyoutPanel, ownerControl As Control, manualPointInOwnerClient As Point,
                                                          Optional useSlideAnimation As Boolean = True)
        If flyout Is Nothing OrElse flyout.IsDisposed Then Return
        If ownerControl Is Nothing OrElse ownerControl.IsDisposed Then Return
        If useSlideAnimation Then flyout.Options.AnimationType = DevExpress.Utils.Win.PopupToolWindowAnimation.Slide
        flyout.Options.AnchorType = DevExpress.Utils.Win.PopupToolWindowAnchor.Manual
        flyout.OwnerControl = ownerControl
        flyout.Options.Location = manualPointInOwnerClient
    End Sub

    ''' <summary>
    ''' Classic jaw flyout placement (pre-Refactor AdultJaw): <c>OwnerControl = jawHostControl</c>,
    ''' <c>svg.Location</c> mapped through <paramref name="jawPanel"/> into host client space, then Ld/Rd/Lu/Ru corner rules.
    ''' </summary>
    Public Shared Sub ConfigureLegacyToothFlyout(flyout As FlyoutPanel, jawHostControl As Control, jawPanel As Control, svg As SvgImageBox,
                                                 namePrefixComparison As StringComparison, Optional useSlideAnimation As Boolean = True)
        If flyout Is Nothing OrElse flyout.IsDisposed OrElse svg Is Nothing OrElse svg.IsDisposed Then Return
        If jawHostControl Is Nothing OrElse jawHostControl.IsDisposed OrElse jawPanel Is Nothing OrElse jawPanel.IsDisposed Then Return
        If useSlideAnimation Then flyout.Options.AnimationType = DevExpress.Utils.Win.PopupToolWindowAnimation.Slide
        flyout.Options.AnchorType = DevExpress.Utils.Win.PopupToolWindowAnchor.Manual
        flyout.OwnerControl = jawHostControl
        Dim loc As New Point(jawPanel.Left + svg.Left, jawPanel.Top + svg.Top)
        Dim n As String = svg.Name
        If n.StartsWith("Ld", namePrefixComparison) Then
            flyout.Options.Location = New Point(loc.X - flyout.Width, loc.Y + svg.Height - flyout.Height)
        ElseIf n.StartsWith("Rd", namePrefixComparison) Then
            flyout.Options.Location = New Point(loc.X + svg.Width, loc.Y + svg.Height - flyout.Height)
        ElseIf n.StartsWith("Lu", namePrefixComparison) Then
            flyout.Options.Location = New Point(loc.X - flyout.Width, loc.Y)
        ElseIf n.StartsWith("Ru", namePrefixComparison) Then
            flyout.Options.Location = New Point(loc.X + svg.Width, loc.Y)
        Else
            flyout.Options.Location = New Point(loc.X + svg.Width, loc.Y)
        End If
    End Sub

    ''' <summary>
    ''' Raised immediately before <see cref="Frm_TblCities"/>, <see cref="Frm_Health"/>, or similar modal dialogs
    ''' opened from combos inside the patient flyout. Subscribers must hide the patient <see cref="FlyoutPanel"/>
    ''' synchronously so DevExpress does not dispose it when activation moves to the modal.
    ''' </summary>
    Public Shared Event BeforeMaintenanceModalDialog As EventHandler

    Public Shared Sub RaiseBeforeMaintenanceModalDialog()
        RaiseEvent BeforeMaintenanceModalDialog(Nothing, EventArgs.Empty)
    End Sub

    ''' <summary>
    ''' Raised after a shell-owned maintenance modal closes (Navigator patient add/edit dialogs). DevExpress popup state
    ''' can remain broken until the next message pump cycle; subscribers should hide hosted <see cref="FlyoutPanel"/>s and
    ''' defer any <see cref="FlyoutPanel.ShowPopup"/> retries (see Diagnostics jaw flyouts after <see cref="PatientInfoForm"/>).
    ''' </summary>
    Public Shared Event AfterMaintenanceModalDialog As EventHandler

    Public Shared Sub RaiseAfterMaintenanceModalDialog()
        RaiseEvent AfterMaintenanceModalDialog(Nothing, EventArgs.Empty)
    End Sub
End Class
