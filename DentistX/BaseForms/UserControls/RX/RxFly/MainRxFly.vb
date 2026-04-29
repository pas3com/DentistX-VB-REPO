Imports System.Data.SqlClient
Imports Dapper
Imports System.Drawing
Imports DevExpress.XtraReports.UI
Imports System.Globalization
Imports System.Text.RegularExpressions

Public Class MainRxFly

    Private clsRxBodyView As IEnumerable(Of RxBodyView)
    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        DsMainRX1.EnforceConstraints = False
        RxFly_BdyVWTableAdapter.FillBody(DsMainRX1.RxFly_BdyVW)
    End Sub
    Public Sub New(ByVal rID As Integer)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        DsMainRX1.EnforceConstraints = False
        RxFly_BdyVWTableAdapter.FillBody(DsMainRX1.RxFly_BdyVW)
        RxFly_BdyVWTableAdapter.FillBy(DsMainRX1.RxFly_BdyVW, rID)
    End Sub




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