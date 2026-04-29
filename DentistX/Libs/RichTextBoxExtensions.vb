Imports System.Drawing
Imports System.Windows.Forms
Imports System.Runtime.CompilerServices
Public Module RichTextBoxExtensions
    <System.Runtime.CompilerServices.Extension>
    Public Sub AppendText(ByVal box As RichTextBox, ByVal text As String, ByVal color As Color)
        box.SelectionStart = box.TextLength
        box.SelectionLength = 0

        box.SelectionColor = color
        box.AppendText(text)
        box.SelectionColor = box.ForeColor
    End Sub
End Module

Public Module ArrayExtension
    <Extension()>
    Public Sub RemoveAll(Of T)(ByRef arr As T(), matching As Predicate(Of T))
        If Not IsNothing(arr) Then
            If arr.Count > 0 Then
                Dim ls As List(Of T) = arr.ToList
                ls.RemoveAll(matching)
                arr = ls.ToArray
            End If
        End If
    End Sub

    ' Remove element at index "index". Result is one element shorter.
    ' Similar to List.RemoveAt, but for arrays.
    <System.Runtime.CompilerServices.Extension()>
    Public Sub RemoveAt(Of T)(ByRef a() As T, ByVal index As Integer)
        ' Move elements after "index" down 1 position.
        Array.Copy(a, index + 1, a, index, UBound(a) - index)
        ' Shorten by 1 element.
        ReDim Preserve a(UBound(a) - 1)
    End Sub


    <System.Runtime.CompilerServices.Extension()>
    Public Sub DropFirstElement(Of T)(ByRef a() As T)
        a.RemoveAt(0)
    End Sub

    <System.Runtime.CompilerServices.Extension()>
    Public Sub DropLastElement(Of T)(ByRef a() As T)
        a.RemoveAt(UBound(a))
    End Sub
End Module