Imports Dapper
Imports DevExpress.XtraEditors
Imports DevExpress.XtraGrid.Views.Base
Imports DevExpress.XtraGrid.Views.Grid
Imports System.Data.SqlClient
Imports System.IO
'Imports System.Windows.Controls

Public Class HistoryViewerForm

    Private ConnectionString As String = DentistXDATA.GetConnection.ConnectionString
    Private ReadOnly LayoutFilePath As String = Path.Combine(Application.StartupPath, "DentistXLogs", "HistoryGridLayout.xml")

    Private Sub HistoryViewerForm_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        Try
            If Not Directory.Exists(Path.GetDirectoryName(LayoutFilePath)) Then
                Directory.CreateDirectory(Path.GetDirectoryName(LayoutFilePath))
            End If

            GridView1.SaveLayoutToXml(LayoutFilePath)
        Catch ex As Exception
            ' Optional: log or ignore
        End Try
    End Sub


    Private Sub HistoryViewerForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If File.Exists(LayoutFilePath) Then
            Try
                GridView1.RestoreLayoutFromXml(LayoutFilePath)
            Catch ex As Exception
                ' Optional: delete corrupted layout file
                File.Delete(LayoutFilePath)
            End Try
        End If
        LoadHistory()
    End Sub

    Private Sub BtnResetLayout_Click(sender As Object, e As EventArgs) Handles BtnResetLayout.Click
        If File.Exists(LayoutFilePath) Then File.Delete(LayoutFilePath)
        Application.Restart() ' or Me.Close() + reopen
    End Sub


    Private Sub BtnRefresh_Click(sender As Object, e As EventArgs) Handles BtnRefresh.Click
        LoadHistory()
    End Sub

    Private Sub LoadHistory()
        Dim fromDate As Date? = If(DateFrom.EditValue IsNot Nothing, CType(DateFrom.EditValue, Date?), Nothing)
        Dim toDate As Date? = If(DateTo.EditValue IsNot Nothing, CType(DateTo.EditValue, Date?), Nothing)
        Dim actionFilter As String = CStr(CboAction.EditValue)

        Using con As New SqlConnection(ConnectionString)
            Dim sql As String = "
                SELECT * FROM PatientHistory
                WHERE (@FromDate IS NULL OR CAST(ActionTime AS DATE) >= @FromDate)
                  AND (@ToDate IS NULL OR CAST(ActionTime AS DATE) <= @ToDate)
                  AND (@ActionFilter = 'All' OR Action = @ActionFilter)
                ORDER BY ActionTime DESC"
            Dim list = con.Query(Of PatientHistory)(sql, New With {
                .FromDate = fromDate,
                .ToDate = toDate,
                .ActionFilter = actionFilter
            }).ToList()

            GridControl1.DataSource = list
            GridView1.Columns("ActionTime").Group()
            GridView1.ExpandAllGroups()
        End Using
    End Sub

    Private Sub GridView1_RowStyle(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs) Handles GridView1.RowStyle
        If e.RowHandle >= 0 Then
            Dim view As GridView = CType(sender, GridView)
            Dim action As String = view.GetRowCellValue(e.RowHandle, "Action").ToString()

            Select Case action.ToLower()
                Case "insert"
                    e.Appearance.BackColor = Color.LightGreen
                Case "update"
                    e.Appearance.BackColor = Color.LightSkyBlue
                Case "delete"
                    e.Appearance.BackColor = Color.MistyRose
                Case "view"
                    e.Appearance.BackColor = Color.Gainsboro
            End Select
        End If
    End Sub


    Private Sub GridView1_DoubleClick(sender As Object, e As EventArgs) Handles GridView1.DoubleClick
        Dim rowHandle = GridView1.FocusedRowHandle
        If rowHandle >= 0 Then
            Dim historyItem As PatientHistory = CType(GridView1.GetRow(rowHandle), PatientHistory)
            If historyItem IsNot Nothing Then
                Dim title As String = If(Eng,
                                         $"Details - {historyItem.PatientName}",
                                         $"تفاصيل - {historyItem.PatientName}")
                XtraMessageBox.Show(historyItem.AdditionalInfo, title, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        End If
    End Sub

    Private Sub GridView1_CustomUnboundColumnData(sender As Object, e As CustomColumnDataEventArgs) Handles GridView1.CustomUnboundColumnData
        If e.Column.FieldName = "ActionIcon" AndAlso e.IsGetData Then
            Dim row As PatientHistory = CType(GridView1.GetRow(e.ListSourceRowIndex), PatientHistory)
            Select Case row.Action.ToLower()
                Case "insert"
                    e.Value = ImgActions.Images("Insert")
                Case "update"
                    e.Value = ImgActions.Images("Update")
                Case "delete"
                    e.Value = ImgActions.Images("Delete")
                Case "view"
                    e.Value = ImgActions.Images("View")
            End Select
        End If
    End Sub


    Private Sub TxtSearchPatient_EditValueChanged(sender As Object, e As EventArgs) Handles TxtSearchPatient.EditValueChanged
        GridView1.ActiveFilterString = $"[PatientName] LIKE '%{TxtSearchPatient.Text.Replace("'", "''")}%'"
    End Sub


End Class
