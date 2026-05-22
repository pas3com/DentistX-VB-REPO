Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Globalization
Imports System.Windows.Forms

''' <summary>
''' WinForms loads localized strings from .resx during <c>InitializeComponent</c> using the
''' culture <i>at that moment</i>. Changing <see cref="Globalization.CultureInfo.CurrentUICulture"/> later does not
''' re-bind those properties, and code that uses <c>If(Eng, …)</c> only updates when that code runs again.
''' This module re-applies resources to open forms and delegates to feature-specific refresh methods.
''' </summary>
Friend Module RuntimeUiLanguage

    Public Sub RefreshAllOpenFormsForCulture(culture As CultureInfo)
        Dim forms As New List(Of Form)
        For Each f As Form In Application.OpenForms
            If f IsNot Nothing AndAlso Not f.IsDisposed Then forms.Add(f)
        Next
        For Each f In forms
            Try
                If TypeOf f Is MainView3 Then
                    DirectCast(f, MainView3).ApplyRuntimeUiLanguageAfterCultureChange(culture)
                ElseIf TypeOf f Is MainView1 Then
                    DirectCast(f, MainView1).ApplyRuntimeUiLanguageAfterCultureChange(culture)
                Else
                    ApplyFormControlTreeResources(f, culture)
                End If
            Catch
            End Try
        Next
    End Sub

    Public Sub ApplyFormControlTreeResources(root As Form, culture As CultureInfo)
        If root Is Nothing OrElse root.IsDisposed Then Return
        Dim rm As New ComponentResourceManager(root.GetType())
        rm.ApplyResources(root, "$this", culture)
        ApplyChildControlResources(rm, root.Controls, culture)
        ' Arabic .resx sets $this.RightToLeft / RightToLeftLayout; English satellites often omit them, so switching ar→en leaves RTL. Sync from <see cref="Module1.Eng"/>.
        ApplyShellLayoutDirectionFromEng(root)
    End Sub

    ''' <summary>Forces LTR when <see cref="Module1.Eng"/> is true and RTL when false, so culture switches fix layout even when the target .resx has no $this.RightToLeft entry.</summary>
    Public Sub ApplyShellLayoutDirectionFromEng(shell As Control)
        If shell Is Nothing OrElse shell.IsDisposed Then Return
        Dim rtl = Not Eng
        shell.RightToLeft = If(rtl, RightToLeft.Yes, RightToLeft.No)
        'shell.RightToLeftLayout = rtl
    End Sub

    Private Sub ApplyChildControlResources(parentRm As ComponentResourceManager, controls As Control.ControlCollection, culture As CultureInfo)
        For Each c As Control In controls
            If c Is Nothing OrElse c.IsDisposed Then Continue For
            If TypeOf c Is UserControl Then
                Try
                    parentRm.ApplyResources(c, c.Name, culture)
                Catch
                End Try
                Dim ucRm As New ComponentResourceManager(c.GetType())
                Try
                    ucRm.ApplyResources(c, c.Name, culture)
                Catch
                End Try
                ApplyChildControlResources(ucRm, c.Controls, culture)
            Else
                Try
                    parentRm.ApplyResources(c, c.Name, culture)
                Catch
                End Try
                If c.Controls.Count > 0 Then ApplyChildControlResources(parentRm, c.Controls, culture)
            End If
        Next
    End Sub

    Public Sub RefreshSchedulerUserControlsUnder(root As Control)
        If root Is Nothing OrElse root.IsDisposed Then Return
        Dim sn = TryCast(root, SchedulerNew)
        If sn IsNot Nothing Then sn.ApplyLanguageRuntimeRefresh()
        For Each ch As Control In root.Controls
            RefreshSchedulerUserControlsUnder(ch)
        Next
    End Sub

End Module
