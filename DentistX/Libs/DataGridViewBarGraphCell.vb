Imports System.Drawing
Imports System.Windows.Forms

Public Class DataGridViewBarGraphCell
    Inherits DataGridViewTextBoxCell


    Protected Overrides Sub Paint(ByVal graphics As Graphics, ByVal clipBounds As Rectangle, ByVal cellBounds As Rectangle, ByVal rowIndex As Integer,
                                    ByVal cellState As DataGridViewElementStates, ByVal value As Object, ByVal formattedValue As Object,
                                   ByVal errorText As String, ByVal cellStyle As DataGridViewCellStyle, ByVal advancedBorderStyle As DataGridViewAdvancedBorderStyle,
                                    ByVal paintParts As DataGridViewPaintParts)

        MyBase.Paint(graphics, clipBounds, cellBounds, rowIndex, cellState, value, "", errorText, cellStyle, advancedBorderStyle, paintParts)

        '=======================
        ' Get the value of the cell:
        Dim cellValue As Decimal
        If IsDBNull(value) Then
            cellValue = 0
        Else
            cellValue = CDec(value)
        End If

        ' If cell value is 0, you still
        ' want to show something, so set the value
        ' to 1.
        If cellValue = 0 Then
            cellValue = 1
        End If

        Const HORIZONTALOFFSET As Integer = 1
        Const SPACER As Integer = 4

        ' Get the parent column and the maximum value:
        Dim parent As DataGridViewBarGraphColumn = CType(Me.OwningColumn, DataGridViewBarGraphColumn)
        parent.CalcMaxValue()
        Dim maxValue As Long = parent.MaxValue

        Dim fnt As Font = parent.InheritedStyle.Font

        Dim maxValueSize As SizeF = graphics.MeasureString(maxValue.ToString, fnt)
        Dim availableWidth As Single = cellBounds.Width - maxValueSize.Width - SPACER - (HORIZONTALOFFSET * 2)
        cellValue = CDec((cellValue / maxValue) * availableWidth)

        ' Draw the bar, truncating to fit in the space 
        ' you've got in the cell:
        Const VERTOFFSET As Integer = 4
        Dim newRect As New RectangleF(cellBounds.X + HORIZONTALOFFSET, cellBounds.Y + VERTOFFSET, cellValue, cellBounds.Height - (VERTOFFSET * 2))
        graphics.FillRectangle(Brushes.Red, newRect)

        ' Get the text to draw and calculate its width:
        Dim cellText As String = formattedValue.ToString()
        Dim textSize As SizeF = graphics.MeasureString(cellText, fnt)

        ' Calculate where text would start:
        Dim textStart As PointF = New PointF(HORIZONTALOFFSET + cellValue + SPACER, (cellBounds.Height - textSize.Height) / 2)


        Dim textColor As Color = parent.InheritedStyle.ForeColor
        If (cellState And DataGridViewElementStates.Selected) = DataGridViewElementStates.Selected Then
            textColor = parent.InheritedStyle.SelectionForeColor
        End If

        Using brush As New SolidBrush(textColor)
            graphics.DrawString(cellText, fnt, brush,
             cellBounds.X + textStart.X,
              cellBounds.Y + textStart.Y)
        End Using

    End Sub
End Class
