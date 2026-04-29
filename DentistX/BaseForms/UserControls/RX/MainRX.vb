Imports System.Data.SqlClient
Imports Dapper
Imports System.Drawing
Imports DevExpress.XtraReports.UI
Imports System.Globalization
Imports System.Text.RegularExpressions

Public Class MainRX

    Private clsRxBodyView As IEnumerable(Of RxBodyView)
    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        DsMainRx1.EnforceConstraints = False
        Rx_BdyVWTableAdapter.FillBody(DsMainRx1.Rx_BdyVW)
    End Sub
    Public Sub New(ByVal pID As Integer, ByVal rID As Integer)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        Rx_BdyVWTableAdapter.FillBy(DsMainRx1.Rx_BdyVW, pID, rID)
    End Sub

    Private Function GetRxBody(ByVal patientID As Integer, ByVal rxID As Integer) As List(Of RxBodyView)
        Dim sql As String = "SELECT dbo.Patient.PatientID, dbo.Patient.PatientName, dbo.Patient.Sex, YEAR(GETDATE()) - dbo.Patient.BirthY AS Age,
                                dbo.Patient_RX.RxID, dbo.Patient_RX.RXDate, dbo.Patient_RX.RX, dbo.RxBody.EnHdrName, dbo.RxBody.EnHdrAdres,
                                dbo.RxBody.Logo, dbo.RxBody.Detail, dbo.RxBody.ArHdrName, dbo.RxBody.ArHdrAdres, dbo.RxBody.EnFtr, dbo.RxBody.ArFtr,
                                dbo.RxBody.DrName
                             FROM  dbo.Patient_RX INNER JOIN
                            dbo.Patient ON dbo.Patient_RX.PatientID = dbo.Patient.PatientID CROSS JOIN
                            dbo.RxBody  WHERE dbo.Patient.PatientID = @PatientID AND dbo.Patient_RX.RxID = @RxID "
        Using conn As New SqlConnection(DentistXDATA.GetConnection.ConnectionString)
            Return conn.Query(Of RxBodyView)(sql, New With {.PatientID = patientID, .RxID = rxID}).ToList()
        End Using
    End Function

    Private Function EstimateCharCountFromLabel1(xrLabel As XRLabel) As Integer
        Dim labelFont As Font = xrLabel.Font
        Dim labelWidth As Integer = xrLabel.WidthF - xrLabel.Padding.Left - xrLabel.Padding.Right

        Using g As Graphics = Graphics.FromImage(New Bitmap(1, 1))
            Dim avgCharSize As SizeF = g.MeasureString("م", labelFont) ' Arabic "mim" gives decent average
            If avgCharSize.Width = 0 Then Return 0
            Dim estimatedCount As Integer = CInt(labelWidth / avgCharSize.Width)
            Return estimatedCount
        End Using
    End Function

    Public Function EstimateCharCountFromLabel(myXrLabel As XRLabel) As Integer
        'Dim labelFont As New Font(myXrLabel.FontFamily, myXrLabel.Font.Size, myXrLabel.Font.Style)
        Dim labelFont As Font = myXrLabel.Font

        ' Determine test string based on text direction or script type
        Dim sampleText As String
        If ContainsArabic(myXrLabel.Text) Then
            ' Use a representative Arabic sentence (connected letters)
            sampleText = "اللغة العربية جميلة"
        Else
            ' Latin fallback
            sampleText = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz"
        End If

        ' Convert label width and padding to pixels
        Dim dpi As Single = 96.0F
        Dim totalWidthPx As Single = myXrLabel.WidthF / 100.0F * dpi
        Dim paddingLeftPx As Single = myXrLabel.Padding.Left / 100.0F * dpi
        Dim paddingRightPx As Single = myXrLabel.Padding.Right / 100.0F * dpi
        Dim usableWidthPx As Single = totalWidthPx - paddingLeftPx - paddingRightPx

        Using bmp As New Bitmap(1, 1)
            Using g As Graphics = Graphics.FromImage(bmp)
                g.TextRenderingHint = Drawing.Text.TextRenderingHint.AntiAlias
                Dim textSize As SizeF = g.MeasureString(sampleText, labelFont)
                Dim avgCharWidth As Single = textSize.Width / sampleText.Length

                If myXrLabel.WordWrap Then
                    Dim heightPx As Single = myXrLabel.HeightF / 100.0F * dpi
                    Dim lineHeight As Single = labelFont.GetHeight(dpi)
                    Dim lineCount As Integer = Math.Floor(heightPx / lineHeight)
                    Return CInt((usableWidthPx / avgCharWidth) * lineCount)
                Else
                    Return CInt(usableWidthPx / avgCharWidth)
                End If
            End Using
        End Using
    End Function

    Private Shared Function ContainsArabic(text As String) As Boolean
        Return System.Text.RegularExpressions.Regex.IsMatch(text, "[\u0600-\u06FF]")
    End Function

    Private Function ContainsArabic1(text As String) As Boolean
        ' Unicode Arabic range: 0600–06FF
        Return Regex.IsMatch(text, "[\u0600-\u06FF]")
    End Function

    Public Class RxBodyView
        Property RxID As Integer
        Property PatientID As Integer
        Property PatientName As String
        Property Sex As String
        Property Age As Integer
        Property RXDate As DateTime
        Property RX As String
        '===
        Property EnHdrName As String
        Property EnHdrAdres As String
        Property Logo As Byte()
        Property Detail As String
        Property ArHdrName As String
        Property ArHdrAdres As String
        Property EnFtr As String
        Property ArFtr As String
        Property DrName As String
    End Class

End Class