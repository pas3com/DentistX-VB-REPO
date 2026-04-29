Public Class TransformationController
    Private SelectedShape As DrawableN = Nothing
    Private StartPoint As PointF
    Private OriginalState As DrawableN

    Public Property IsTransforming As Boolean = False

    Public Sub StartTransform(shape As DrawableN, anchor As AnchorEnumN, startPoint As PointF)
        IsTransforming = True
        SelectedShape = shape
        Me.StartPoint = startPoint
        OriginalState = CloneShape(shape)
        SelectedShape.StartTransform(anchor, startPoint)
    End Sub

    Public Sub UpdateTransform(currentPoint As PointF)
        If IsTransforming AndAlso SelectedShape IsNot Nothing Then
            SelectedShape.UpdateTransform(currentPoint)
        End If
    End Sub

    Public Sub EndTransform()
        IsTransforming = False
        If SelectedShape IsNot Nothing Then
            SelectedShape.IsTransforming = False
            SelectedShape.CurrentAnchor = AnchorEnumN.None
        End If
        SelectedShape = Nothing
    End Sub

    Private Function CloneShape(shape As DrawableN) As DrawableN
        Return shape.Clone()
    End Function
End Class