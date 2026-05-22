Imports System.Windows.Forms
Imports DevExpress.Utils
Imports DevExpress.XtraEditors

''' <summary>
''' Runtime strings for jaw treatment popups: DevExpress <see cref="FlyoutPanel"/> (Flyout1) and embedded
''' <c>FlyMenu</c> panel controls. Jaw SVG surfaces stay English-only; uses <see cref="Eng"/>.
''' </summary>
Public Module JawFlyoutLocalization

    Private Const EnSearchPrompt As String = "Search Treatments..."
    Private Const ArSearchPrompt As String = "البحث في العلاجات..."

    Private Const EnToggleNull As String = "Search Mode"
    Private Const EnToggleOff As String = "Normal Add"
    Private Const EnToggleOn As String = "Quick Add"
    Private Const ArToggleNull As String = "وضع البحث"
    Private Const ArToggleOff As String = "إضافة عادية"
    Private Const ArToggleOn As String = "إضافة سريعة"

    Private Const EnMultiTeethGroup As String = "Multiple Teeth Selection"
    Private Const ArMultiTeethGroup As String = "تحديد عدة أسنان"

    Private Const EnExternalGroup As String = "IS External Treat?"
    Private Const ArExternalGroup As String = "علاج خارجي؟"

    Private Const EnSetTeethLabel As String = "Set Selected Teeth As :"
    Private Const ArSetTeethLabel As String = "تعيين الأسنان المحددة كـ:"

    Private Const EnInHouse As String = "Is In House Treat"
    Private Const EnExternal As String = "Is External Treat"
    Private Const ArInHouse As String = "علاج داخل العيادة"
    Private Const ArExternal As String = "علاج خارجي"

    Private Const EnSomewhereElse As String = "Somewhere Else"
    Private Const ArSomewhereElse As String = "مكان آخر"

    ''' <summary>Button captions plus inner RTL. The flyout shell stays LTR so the DevExpress top button row does not clip or reorder when the middle button is hidden (multi-tooth).</summary>
    Public Sub ApplyJawTreatmentsFlyoutLanguage(flyout As FlyoutPanel)
        If flyout Is Nothing Then Return

        ' Never mirror the FlyoutPanel itself: OptionsButtonPanel reverses in RTL and with Btn(1) hidden only Tree+Delete show badly.
        flyout.RightToLeft = RightToLeft.No
        ApplyRightToLeftToFlyoutClientArea(flyout, rtlContent:=Not Eng)

        Dim bp = flyout.OptionsButtonPanel
        If bp IsNot Nothing AndAlso bp.Buttons IsNot Nothing AndAlso bp.Buttons.Count >= 3 Then
            If Eng Then
                ' Compact captions so Tree + Edit + Delete fit on one row (English delete text is set again in Showing).
                bp.Buttons.Item(0).Properties.Caption = "Tree view"
                bp.Buttons.Item(1).Properties.Caption = "Edit"
                bp.Buttons.Item(2).Properties.Caption = "Delete"
            Else
                bp.Buttons.Item(0).Properties.Caption = "عرض العلاجات"
                bp.Buttons.Item(1).Properties.Caption = "تعديل العلاجات"
                bp.Buttons.Item(2).Properties.Caption = "حذف"
            End If
        End If

        LocalizeFlyoutContents(flyout)
        KeepTreatmentsTreeLtr(flyout)
    End Sub

    ''' <summary>Same strings/RTL as <see cref="ApplyJawTreatmentsFlyoutLanguage"/> for AdultJaw embedded <c>FlyMenu</c>.</summary>
    Public Sub ApplyJawTreatmentsFlyMenuLanguage(flyMenu As Control)
        If flyMenu Is Nothing Then Return

        flyMenu.RightToLeft = RightToLeft.No
        ApplyRightToLeftToFlyMenuClientArea(flyMenu, rtlContent:=Not Eng)

        Dim btnTree = TryCast(FindControlRecursive(flyMenu, "btnTrtView"), SimpleButton)
        Dim btnEdit = TryCast(FindControlRecursive(flyMenu, "btnEditTrts"), SimpleButton)
        Dim btnEditMulti = TryCast(FindControlRecursive(flyMenu, "btnEditMultiTrts"), SimpleButton)
        If btnTree IsNot Nothing Then
            btnTree.Text = If(Eng, "Tree view", "عرض العلاجات")
        End If
        If btnEdit IsNot Nothing Then
            btnEdit.Text = If(Eng, "Edit", "تعديل العلاجات")
        End If
        If btnEditMulti IsNot Nothing Then
            btnEditMulti.Text = If(Eng, "Bulk edit", "تعديل جماعي")
        End If

        LocalizeFlyoutContents(flyMenu)
        KeepTreatmentsTreeLtr(flyMenu)
    End Sub

    ''' <summary>Delete caption on <c>btnDelTrts</c> when FlyMenu opens (multi vs single tooth).</summary>
    Public Sub SetJawFlyMenuDeleteButtonCaption(btnDelTrts As SimpleButton, multiSelectedTeeth As Boolean)
        If btnDelTrts Is Nothing Then Return
        If Eng Then
            btnDelTrts.Text = If(multiSelectedTeeth,
                "Delete all (teeth)",
                "Delete From (tooth)")
        Else
            btnDelTrts.Text = If(multiSelectedTeeth,
                "حذف من الأسنان المحددة",
                "حذف من السن المحدد")
        End If
    End Sub

    Private Sub KeepTreatmentsTreeLtr(root As Control)
        ' RTL on the panel breaks WinForms TreeView expansion/display; keep the tree LTR so first level under TREATS always shows.
        Dim tv = TryCast(FindControlRecursive(root, "TrtsTreeView"), TreeView)
        If tv IsNot Nothing Then
            tv.RightToLeft = RightToLeft.No
            tv.RightToLeftLayout = False
        End If
    End Sub

    ''' <summary>Long delete caption when the flyout opens (multi vs single tooth), matching existing logic.</summary>
    Public Sub SetJawFlyoutDeleteButtonCaption(flyout As FlyoutPanel, multiSelectedTeeth As Boolean)
        If flyout Is Nothing Then Return
        Dim buttons = flyout.OptionsButtonPanel?.Buttons
        If buttons Is Nothing OrElse buttons.Count < 3 Then Return

        If Eng Then
            ' Long captions push the third button off the flyout when Edit is visible (single-tooth); keep short labels.
            buttons.Item(2).Properties.Caption = If(multiSelectedTeeth,
                "Delete all (teeth)",
                "Delete From (tooth)")
        Else
            ' Shorter than English (fits beside Edit+Tree when visible; safe with two-button row when Edit hidden).
            buttons.Item(2).Properties.Caption = If(multiSelectedTeeth,
                "حذف من الأسنان المحددة",
                "حذف من السن المحدد")
        End If
    End Sub

    ''' <summary>RTL for search + treatments area only; keeps DevExpress flyout button panel layout stable.</summary>
    Private Sub ApplyRightToLeftToFlyoutClientArea(flyout As FlyoutPanel, rtlContent As Boolean)
        Dim rl = If(rtlContent, RightToLeft.Yes, RightToLeft.No)
        Dim srch = FindControlRecursive(flyout, "SrchPanel")
        If srch IsNot Nothing Then srch.RightToLeft = rl
        Dim trts = FindControlRecursive(flyout, "TrtsPanel")
        If trts IsNot Nothing Then trts.RightToLeft = rl
    End Sub

    ''' <summary>RTL for FlyMenu search + multi-tooth strip; button row stays LTR (same idea as flyout button panel).</summary>
    Private Sub ApplyRightToLeftToFlyMenuClientArea(flyMenu As Control, rtlContent As Boolean)
        Dim rl = If(rtlContent, RightToLeft.Yes, RightToLeft.No)
        Dim srch = FindControlRecursive(flyMenu, "SrchPanel")
        If srch IsNot Nothing Then srch.RightToLeft = rl
        Dim grpMulti = FindControlRecursive(flyMenu, "grpSlctdTeeth")
        If grpMulti IsNot Nothing Then grpMulti.RightToLeft = rl
        Dim buttons = FindControlRecursive(flyMenu, "ButtonsPane")
        If buttons IsNot Nothing Then buttons.RightToLeft = RightToLeft.No
    End Sub

    Private Sub LocalizeFlyoutContents(flyout As Control)
        Dim txtSrch = TryCast(FindControlRecursive(flyout, "txtSrchTrt"), TextEdit)
        If txtSrch IsNot Nothing Then
            txtSrch.Properties.NullValuePrompt = If(Eng, EnSearchPrompt, ArSearchPrompt)
        End If

        Dim quick = TryCast(FindControlRecursive(flyout, "btnQuickSrch"), ToggleSwitch)
        If quick IsNot Nothing Then
            If Eng Then
                quick.Properties.NullText = EnToggleNull
                quick.Properties.OffText = EnToggleOff
                quick.Properties.OnText = EnToggleOn
                quick.Properties.GlyphAlignment = HorzAlignment.Far
            Else
                quick.Properties.NullText = ArToggleNull
                quick.Properties.OffText = ArToggleOff
                quick.Properties.OnText = ArToggleOn
                quick.Properties.GlyphAlignment = HorzAlignment.Near
            End If
        End If

        Dim grpMulti = TryCast(FindControlRecursive(flyout, "grpSlctdTeeth"), GroupControl)
        If grpMulti IsNot Nothing Then
            grpMulti.Text = If(Eng, EnMultiTeethGroup, ArMultiTeethGroup)
        End If

        Dim grpExt = TryCast(FindExternalTreatGroup(flyout), GroupControl)
        If grpExt IsNot Nothing Then
            grpExt.Text = If(Eng, EnExternalGroup, ArExternalGroup)
            Dim setLbl = FindSetTeethAsLabel(grpExt)
            If setLbl IsNot Nothing Then setLbl.Text = If(Eng, EnSetTeethLabel, ArSetTeethLabel)
        End If

        Dim rg = TryCast(FindControlRecursive(flyout, "grpRadioSetAs"), RadioGroup)
        If rg IsNot Nothing AndAlso rg.Properties.Items.Count >= 2 Then
            If Eng Then
                rg.Properties.Items(0).Description = EnInHouse
                rg.Properties.Items(1).Description = EnExternal
            Else
                rg.Properties.Items(0).Description = ArInHouse
                rg.Properties.Items(1).Description = ArExternal
            End If
        End If

        Dim txtExt = TryCast(FindControlRecursive(flyout, "txtExtClinic"), TextEdit)
        If txtExt IsNot Nothing Then
            SyncExtClinicPlaceholder(txtExt)
        End If
    End Sub

    Private Sub SyncExtClinicPlaceholder(txtExt As TextEdit)
        Dim t = If(txtExt.Text, String.Empty).Trim()
        If Eng Then
            If t = ArSomewhereElse Then txtExt.EditValue = EnSomewhereElse
        Else
            If t = EnSomewhereElse OrElse String.IsNullOrEmpty(t) Then txtExt.EditValue = ArSomewhereElse
        End If
    End Sub

    ''' <summary>AdultJaw FlyMenu uses <c>IsExternalGroup</c>; other jaws use <c>GroupControl1</c>.</summary>
    Private Function FindExternalTreatGroup(root As Control) As Control
        Dim grp = FindControlRecursive(root, "IsExternalGroup")
        If grp IsNot Nothing Then Return grp
        Return FindControlRecursive(root, "GroupControl1")
    End Function

    Private Function FindControlRecursive(parent As Control, name As String) As Control
        For Each child As Control In parent.Controls
            If String.Equals(child.Name, name, StringComparison.Ordinal) Then Return child
            Dim found = FindControlRecursive(child, name)
            If found IsNot Nothing Then Return found
        Next
        Return Nothing
    End Function

    ''' <summary>Adult / KidUpper use LabelControl3; KidJaw uses LabelControl31; KidLowerJaw uses LabelControl8.</summary>
    Private Function FindSetTeethAsLabel(grpExternal As GroupControl) As LabelControl
        Dim names = {"LabelControl3", "LabelControl31", "LabelControl8"}
        For Each n In names
            For Each c As Control In grpExternal.Controls
                If String.Equals(c.Name, n, StringComparison.Ordinal) Then Return TryCast(c, LabelControl)
            Next
        Next
        Return Nothing
    End Function

End Module
