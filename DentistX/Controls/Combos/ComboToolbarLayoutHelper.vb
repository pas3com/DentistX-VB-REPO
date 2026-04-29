Imports System.Drawing
Imports System.Windows.Forms
Imports DevExpress.XtraEditors

''' <summary>Shared toolbar layout for combo user controls (search + add in PanelControl1).</summary>
Public NotInheritable Class ComboToolbarLayoutHelper
    Public Const ToolbarButtonWidth As Integer = 25
    ''' <summary>Panel slot width for compact add-only combos (matches typical 22px designer panel).</summary>
    Public Const CompactToolbarSlotWidth As Integer = 22

    Private Sub New()
    End Sub

    ''' <summary>Places search (left) and add (right), sizes PanelControl1, and adjusts host width when Dock is None.</summary>
    ''' <param name="toolbarSlotWidth">Use 0 for default (25px); use <see cref="CompactToolbarSlotWidth"/> for compact combos.</param>
    Public Shared Sub ApplyPanelToolbar(panel1 As PanelControl, btnSearch As SimpleButton, btnAdd As SimpleButton,
                                        searchVisible As Boolean, addVisible As Boolean, host As Control,
                                        Optional toolbarSlotWidth As Integer = 0)
        If panel1 Is Nothing OrElse host Is Nothing Then Return

        Dim slotW As Integer = If(toolbarSlotWidth <= 0, ToolbarButtonWidth, toolbarSlotWidth)

        Dim oldReserved As Integer = If(panel1.Visible AndAlso panel1.Width > 0, panel1.Width, 0)

        If btnSearch IsNot Nothing Then btnSearch.Visible = searchVisible
        If btnAdd IsNot Nothing Then btnAdd.Visible = addVisible

        Dim searchSlot As Integer = If(btnSearch IsNot Nothing AndAlso searchVisible, 1, 0)
        Dim addSlot As Integer = If(btnAdd IsNot Nothing AndAlso addVisible, 1, 0)

        Dim btnH As Integer = host.ClientSize.Height
        If btnH <= 0 Then btnH = slotW

        If searchSlot > 0 Then
            btnSearch.Location = New Point(0, 0)
            btnSearch.Size = New Size(slotW, btnH)
        End If
        If addSlot > 0 Then
            Dim x As Integer = searchSlot * slotW
            btnAdd.Location = New Point(x, 0)
            btnAdd.Size = New Size(slotW, btnH)
        End If

        Dim newReserved As Integer = (searchSlot + addSlot) * slotW
        panel1.Visible = newReserved > 0
        panel1.Width = newReserved
        If newReserved > 0 AndAlso host.ClientSize.Height > 0 Then
            panel1.Height = host.ClientSize.Height
        End If

        Dim delta As Integer = newReserved - oldReserved
        If delta <> 0 AndAlso host.Dock = DockStyle.None Then
            host.Width = Math.Max(1, host.Width + delta)
        End If

        host.PerformLayout()
        If host.Parent IsNot Nothing Then host.Parent.PerformLayout()
    End Sub
End Class
