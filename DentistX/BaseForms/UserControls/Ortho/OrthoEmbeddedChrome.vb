Imports System.Drawing
Imports System.Windows.Forms
Imports DevExpress.LookAndFeel
Imports DevExpress.XtraEditors

''' <summary>
''' Applies <see cref="BasePatientWorkspace.OrthoModuleShellBack"/> to Ortho-hosted panels (nested DevExpress Flat skin quirks).
''' </summary>
Friend Module OrthoEmbeddedChrome

    Friend Sub ApplyToRoot(root As Control)
        If root Is Nothing OrElse root.IsDisposed Then Return
        Dim c = BasePatientWorkspace.OrthoModuleShellBack
        Helpers.ResetControlBackground(root)
        root.BackColor = c
        ApplyRecursive(root, c)
    End Sub

    Private Sub ApplyRecursive(root As Control, c As Color)
        If root Is Nothing Then Return

        Dim grp = TryCast(root, GroupControl)
        If grp IsNot Nothing Then
            grp.LookAndFeel.UseDefaultLookAndFeel = False
            grp.LookAndFeel.Style = LookAndFeelStyle.Flat
            grp.Appearance.BackColor = c
            grp.Appearance.Options.UseBackColor = True
            grp.AppearanceCaption.BackColor = c
            grp.AppearanceCaption.Options.UseBackColor = True
        End If

        Dim rg = TryCast(root, RadioGroup)
        If rg IsNot Nothing Then
            rg.LookAndFeel.UseDefaultLookAndFeel = False
            rg.LookAndFeel.Style = LookAndFeelStyle.Flat
            rg.Properties.Appearance.BackColor = c
            rg.Properties.Appearance.Options.UseBackColor = True
        End If

        Dim lbl = TryCast(root, LabelControl)
        If lbl IsNot Nothing Then
            lbl.LookAndFeel.UseDefaultLookAndFeel = False
            lbl.LookAndFeel.Style = LookAndFeelStyle.Flat
            lbl.Appearance.BackColor = c
            lbl.Appearance.Options.UseBackColor = True
        End If

        Dim chk = TryCast(root, CheckEdit)
        If chk IsNot Nothing Then
            chk.LookAndFeel.UseDefaultLookAndFeel = False
            chk.LookAndFeel.Style = LookAndFeelStyle.Flat
            chk.Properties.Appearance.BackColor = c
            chk.Properties.Appearance.Options.UseBackColor = True
        End If

        Dim tlp = TryCast(root, TableLayoutPanel)
        If tlp IsNot Nothing Then tlp.BackColor = c

        Dim svg = TryCast(root, SvgImageBox)
        If svg IsNot Nothing Then svg.BackColor = c

        If TypeOf root Is FullOrthoTreating OrElse TypeOf root Is OpenNewOrthCTL OrElse TypeOf root Is OrthoTreatingCTL Then
            Helpers.ResetControlBackground(root)
            root.BackColor = c
        End If

        For Each child As Control In root.Controls
            ApplyRecursive(child, c)
        Next
    End Sub

End Module
