Imports System.Drawing
Imports System.Windows.Forms

Public Class DataGridViewBarGraphColumn
    Inherits DataGridViewColumn

    Public Sub New()
        Me.CellTemplate = New DataGridViewBarGraphCell()
        Me.ReadOnly = True
    End Sub

    Public MaxValue As Long
    Private needsRecalc As Boolean = True

    Public Sub CalcMaxValue()
        If needsRecalc Then
            Dim colIndex As Integer = Me.DisplayIndex
            For rowIndex As Integer = 0 To Me.DataGridView.Rows.Count - 1

                Dim row As DataGridViewRow = Me.DataGridView.Rows(rowIndex)
                MaxValue = Math.Max(MaxValue, CLng(row.Cells(colIndex).Value))
            Next
            needsRecalc = False
        End If
    End Sub
End Class

