Imports System.Data.SqlClient
Imports Dapper
Imports DevExpress.XtraEditors
Imports DevExpress.XtraReports.UI

Public Class RxFlyPrint
    Public Sub New(ByVal RxID As Integer)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        RxFlyTableAdapter.FillByRxID(DsRx1.RxFly, RxID)
        LoadLabelOffsets()
    End Sub

    Private Const PixelsPerMm As Single = 4.0F   ' 1 mm = 4 px
    Private _loadedOffsets As List(Of ReportLabel)
    Private Sub LoadLabelOffsets()
        Try
            Dim sql As String =
        "SELECT LblName, OffsetXmm, OffsetYmm
         FROM ReportLabel"
            Dim offsets As List(Of ReportLabel)
            Using cn As New SqlConnection(DentistXDATA.GetConnection.ConnectionString)
                offsets = cn.Query(Of ReportLabel)(sql).ToList()
            End Using

            _loadedOffsets = offsets

            For Each item In offsets

                Dim lbl As XRLabel =
                Detail.Controls.OfType(Of XRLabel)().
                    FirstOrDefault(Function(l) l.Name = item.LblName)
                If lbl Is Nothing Then Continue For

                lbl.Left = CInt(lbl.Left + item.OffsetXmm * PixelsPerMm)
                lbl.Top = CInt(lbl.Top + item.OffsetYmm * PixelsPerMm)
            Next
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

End Class