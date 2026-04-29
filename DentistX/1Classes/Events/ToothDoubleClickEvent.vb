Imports DevExpress.XtraEditors
Public Class ToothDoubleClickEvent
    Inherits EventArgs

    Public Property PatientID() As Integer
    Public Property PatientName() As String
    Public Property SVG() As SvgImageBox
    Public Property Source() As String
    Public Sub New(ByVal newPatientID As Integer, ByVal newPatientName As String, ByVal newSvgIMG As SvgImageBox, ByVal newSource As String)
        PatientID = newPatientID
        PatientName = newPatientName
        SVG = newSvgIMG
        Source = newSource
    End Sub
End Class


