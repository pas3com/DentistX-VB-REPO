Imports System.Drawing
Imports System.Text.RegularExpressions
Imports System.Windows.Forms
Imports DevExpress.XtraEditors

''' <summary>
''' Stateless lifecycle helpers shared by every jaw control that uses the embedded <c>FlyMenu</c> <see cref="PanelControl"/>
''' (AdultJaw is the source-of-truth implementation; AdultUpperJaw, AdultLowerJaw, KidJaw, KidUpperJaw, KidLowerJaw
''' and the 6 diagnose jaws all delegate to this module). Keeps the per-jaw glue tiny (just an <see cref="IMessageFilter"/>
''' wrapper + the four button click handlers).
''' </summary>
Public Module JawFlyMenuRuntime

    ''' <summary>Right-click on an SVG: place FlyMenu next to the tooth using the Ld/Rd/Lu/Ru quadrant prefix (Adult uses <see cref="StringComparison.Ordinal"/>; Kid jaws sometimes use mixed case so pass <see cref="StringComparison.OrdinalIgnoreCase"/>). When <paramref name="anchorToTopRow"/> is True the clicked row is normalized to the reference row that matches full FlyMenu placement on that half-jaw: Lu/Ru use the middle <c>Top</c> row; Ld/Rd use the bottom <c>Out</c> row (lower jaws stack IN → Top → Out top-to-bottom, opposite of upper).</summary>
    Public Sub PositionFlyMenuForTooth(flyMenu As PanelControl, svg As Object, prefixComparison As StringComparison, Optional anchorToTopRow As Boolean = False)
        If flyMenu Is Nothing OrElse flyMenu.IsDisposed OrElse svg Is Nothing Then Return
        Dim svgCtl As Control = TryCast(svg, Control)
        If svgCtl Is Nothing Then Return
        If anchorToTopRow Then
            Dim refSibling As Control = TryGetReferenceRowSibling(svgCtl)
            If refSibling IsNot Nothing Then svgCtl = refSibling
        End If
        Dim loc As Point = svgCtl.Location
        Dim svgWidth As Integer = svgCtl.Width
        Dim svgHeight As Integer = svgCtl.Height
        Dim n As String = svgCtl.Name
        If n.StartsWith("Ld", prefixComparison) Then
            flyMenu.Location = New Point(loc.X - flyMenu.Width, loc.Y + svgHeight - flyMenu.Height)
        ElseIf n.StartsWith("Rd", prefixComparison) Then
            flyMenu.Location = New Point(loc.X + svgWidth, loc.Y + svgHeight - flyMenu.Height)
        ElseIf n.StartsWith("Lu", prefixComparison) Then
            flyMenu.Location = New Point(loc.X - flyMenu.Width, loc.Y)
        ElseIf n.StartsWith("Ru", prefixComparison) Then
            flyMenu.Location = New Point(loc.X + svgWidth, loc.Y)
        Else
            flyMenu.Location = New Point(loc.X + svgWidth, loc.Y)
        End If
        flyMenu.BringToFront()
    End Sub

    Private ReadOnly s_upperRowToRefRegex As New Regex("(Out|IN)", RegexOptions.IgnoreCase Or RegexOptions.Compiled)
    Private ReadOnly s_lowerRowToRefRegex As New Regex("(IN|Top)", RegexOptions.IgnoreCase Or RegexOptions.Compiled)
    Private ReadOnly s_hasOutRowRegex As New Regex("Out", RegexOptions.IgnoreCase Or RegexOptions.Compiled)
    Private ReadOnly s_hasTopRowRegex As New Regex("Top", RegexOptions.IgnoreCase Or RegexOptions.Compiled)

    ''' <summary>Lu/Ru (and kid LU/RU): anchor to Top row. Ld/Rd (and kid LD/RD): anchor to Out row (last/bottom row — lower half-jaw row order is reversed vs upper).</summary>
    Private Function TryGetReferenceRowSibling(svgCtl As Control) As Control
        If svgCtl Is Nothing Then Return Nothing
        Dim parent As Control = svgCtl.Parent
        If parent Is Nothing Then Return Nothing
        Dim name As String = svgCtl.Name
        If String.IsNullOrEmpty(name) Then Return Nothing

        Dim siblingName As String
        If IsLowerQuadrantTooth(name) Then
            If s_hasOutRowRegex.IsMatch(name) Then Return Nothing
            If Not s_lowerRowToRefRegex.IsMatch(name) Then Return Nothing
            siblingName = s_lowerRowToRefRegex.Replace(name, "Out", 1)
        Else
            If s_hasTopRowRegex.IsMatch(name) Then Return Nothing
            If Not s_upperRowToRefRegex.IsMatch(name) Then Return Nothing
            siblingName = s_upperRowToRefRegex.Replace(name, "Top", 1)
        End If

        For Each c As Control In parent.Controls
            If c IsNot Nothing AndAlso String.Equals(c.Name, siblingName, StringComparison.OrdinalIgnoreCase) Then Return c
        Next
        Return Nothing
    End Function

    ''' <summary>Lower-left / lower-right tooth SVGs (Ld/Rd, LDOUTK/RDOUTK, …).</summary>
    Private Function IsLowerQuadrantTooth(name As String) As Boolean
        Return name.StartsWith("Ld", StringComparison.OrdinalIgnoreCase) OrElse
               name.StartsWith("Rd", StringComparison.OrdinalIgnoreCase)
    End Function

    ''' <summary>Right-click on blank JawPanel area: pin FlyMenu near the click while keeping it fully inside <paramref name="jawPanel"/> (replaces the old <c>ComboFlyoutSearchHelper.ConfigureManualAnchoredFlyoutOnOwner</c> path).</summary>
    Public Sub PositionFlyMenuAtPoint(flyMenu As PanelControl, jawPanel As Control, anchorInJawPanelClient As Point)
        If flyMenu Is Nothing OrElse flyMenu.IsDisposed OrElse jawPanel Is Nothing Then Return
        Dim x As Integer = jawPanel.Left + anchorInJawPanelClient.X
        Dim y As Integer = jawPanel.Top + anchorInJawPanelClient.Y
        Dim maxX As Integer = (jawPanel.Left + jawPanel.Width) - flyMenu.Width
        Dim maxY As Integer = (jawPanel.Top + jawPanel.Height) - flyMenu.Height
        If x > maxX Then x = maxX
        If y > maxY Then y = maxY
        If x < jawPanel.Left Then x = jawPanel.Left
        If y < jawPanel.Top Then y = jawPanel.Top
        flyMenu.Location = New Point(x, y)
        flyMenu.BringToFront()
    End Sub

    ''' <summary>Common pre-show bits (apply Eng/Ar captions, color delete red, set edit visibility, dock tree). <paramref name="hideEditMulti"/> hides the bulk-edit button on diagnose jaws so users don't bulk-edit through the treats form.</summary>
    Public Sub PrepareFlyMenuPresentation(flyMenu As PanelControl,
                                          btnDelTrts As SimpleButton,
                                          btnEditTrts As SimpleButton,
                                          btnEditMultiTrts As SimpleButton,
                                          hasMultiSelectedTeeth As Boolean,
                                          showEditTrtBtn As Boolean,
                                          trtsTreeView As Control,
                                          hideEditMulti As Boolean)
        If flyMenu Is Nothing OrElse flyMenu.IsDisposed Then Return
        JawFlyoutLocalization.ApplyJawTreatmentsFlyMenuLanguage(flyMenu)
        If btnDelTrts IsNot Nothing Then
            btnDelTrts.Appearance.ForeColor = Color.Red
            JawFlyoutLocalization.SetJawFlyMenuDeleteButtonCaption(btnDelTrts, hasMultiSelectedTeeth)
        End If
        If btnEditTrts IsNot Nothing Then btnEditTrts.Visible = showEditTrtBtn
        If btnEditMultiTrts IsNot Nothing Then btnEditMultiTrts.Visible = Not hideEditMulti
        If trtsTreeView IsNot Nothing Then
            trtsTreeView.Dock = DockStyle.Fill
            trtsTreeView.Visible = True
            trtsTreeView.BringToFront()
        End If
    End Sub

    ''' <summary>Refresh delete caption + edit-button visibility while FlyMenu is open (called from per-jaw <c>UpdateEditButtonVisibility</c>).</summary>
    Public Sub RefreshFlyMenuEditVisibility(flyMenu As PanelControl,
                                            btnDelTrts As SimpleButton,
                                            btnEditTrts As SimpleButton,
                                            hasMultiSelectedTeeth As Boolean,
                                            showEditTrtBtn As Boolean)
        If flyMenu Is Nothing OrElse Not flyMenu.Visible Then Return
        If btnEditTrts IsNot Nothing Then btnEditTrts.Visible = showEditTrtBtn
        If btnDelTrts IsNot Nothing Then
            JawFlyoutLocalization.SetJawFlyMenuDeleteButtonCaption(btnDelTrts, hasMultiSelectedTeeth)
        End If
    End Sub

    ''' <summary>Hide FlyMenu when an app-wide left click lands outside its on-screen rectangle; also clears the search box so re-opening starts fresh (mirrors AdultJaw).</summary>
    Public Sub TryHideOnOutsideClick(flyMenu As PanelControl, txtSrchTrt As TextEdit, clickScreenPoint As Point)
        If flyMenu Is Nothing OrElse flyMenu.IsDisposed Then Return
        If Not flyMenu.Visible Then Return
        Try
            Dim flyoutScreenRect As New Rectangle(flyMenu.PointToScreen(Point.Empty), flyMenu.Size)
            If Not flyoutScreenRect.Contains(clickScreenPoint) Then
                flyMenu.Visible = False
                If txtSrchTrt IsNot Nothing Then txtSrchTrt.ResetText()
            End If
        Catch
        End Try
    End Sub

End Module
