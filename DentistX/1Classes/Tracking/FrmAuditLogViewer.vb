Imports System.Data.SqlClient
Imports System.Windows.Forms
Imports Dapper
Imports DevExpress.XtraGrid.Views.Grid

Public Class FrmAuditLogViewer

    Private Sub FrmAuditLogViewer_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadAuditLogs()
    End Sub

    Private Sub LoadAuditLogs()
        Using conn As New SqlConnection(DentistXDATA.GetConnection.ConnectionString)
            Dim sql = "SELECT AuditID, ActionType, TableName, RecordID, OldValue, NewValue, ChangedBy, ChangeDate FROM AuditLog ORDER BY ChangeDate DESC"
            Dim logs = conn.Query(Of AuditLogEntry)(sql).ToList()

            GridControl1.DataSource = logs
        End Using
    End Sub

    ' Optional: Double-click row to see full details
    Private Sub GridView1_DoubleClick(sender As Object, e As EventArgs) Handles GridView1.DoubleClick
        Dim view As GridView = GridView1
        If view.FocusedRowHandle >= 0 Then
            Dim entry = CType(view.GetRow(view.FocusedRowHandle), AuditLogEntry)

            Dim details As String =
                If(eng, "Action: ", "الإجراء: ") & entry.ActionType & vbCrLf &
                If(eng, "Table: ", "الجدول: ") & entry.TableName & vbCrLf &
                If(eng, "RecordID: ", "معرف السجل: ") & entry.RecordID & vbCrLf &
                If(eng, "Changed By: ", "تم التغيير بواسطة: ") & entry.ChangedBy & vbCrLf &
                If(eng, "Date: ", "التاريخ: ") & entry.ChangeDate & vbCrLf &
                If(eng, "Old Value: ", "القيمة القديمة: ") & entry.OldValue & vbCrLf &
                If(eng, "New Value: ", "القيمة الجديدة: ") & entry.NewValue

            MessageBox.Show(details,
                            If(eng, "Audit Details", "تفاصيل سجل التغييرات"),
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information)
        End If
    End Sub
End Class
