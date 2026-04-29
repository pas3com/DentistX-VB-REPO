Imports System
Imports System.Data.SqlClient
Imports System.Windows.Forms
Imports Dapper
Imports DevExpress.XtraGrid.Views.Grid

Public Class FrmLastPatients


    Public Function GetRecentActivePatients(Optional topN As Integer = 10) As List(Of PatientRecentActivity)
        Try

            Using conn As SqlConnection = DentistXDATA.GetConnection
                Dim sql As String = "
            WITH PatientActivity AS (
                SELECT
                    p.PatientID,
                    p.PatientName,
                    n.Note AS LastNote,
                    rx.RX AS LastRX,
                    ttrt.Treat AS LastToothTreatment,
                    ptrt.Detail AS LastTrtDetail,

                    act.LastActivityDate

                FROM Patient p
                LEFT JOIN Patient_Notes n ON p.PatientID = n.PatientID
                LEFT JOIN Patient_Pays pay ON p.PatientID = pay.PatientID
                LEFT JOIN Patient_RX rx ON p.PatientID = rx.PatientID
                LEFT JOIN Patient_ToothTrt ttrt ON p.PatientID = ttrt.PatientID
                LEFT JOIN Patient_Trts ptrt ON p.PatientID = ptrt.PatientID

                CROSS APPLY (
                                SELECT MAX(ActivityDate) AS LastActivityDate
                                FROM (
                                    SELECT CAST(n.NoteDate AS DATETIME) AS ActivityDate WHERE n.NoteDate IS NOT NULL
                                    UNION ALL
                                    SELECT CAST(pay.PayDate AS DATETIME) AS ActivityDate WHERE pay.PayDate IS NOT NULL
                                    UNION ALL
                                    SELECT CAST(rx.RXDate AS DATETIME) AS ActivityDate WHERE rx.RXDate IS NOT NULL
                                    UNION ALL
                                    SELECT CAST(ttrt.TreatDate AS DATETIME) AS ActivityDate WHERE ttrt.TreatDate IS NOT NULL
                                    UNION ALL
                                    SELECT CAST(ptrt.TrtDate AS DATETIME) AS ActivityDate WHERE ptrt.TrtDate IS NOT NULL
                                ) AS AllDates
                            ) AS act
            )
            SELECT TOP (@TopN) *
            FROM PatientActivity
            ORDER BY LastActivityDate DESC
        "

                Return conn.Query(Of PatientRecentActivity)(sql, New With {.TopN = topN}).ToList()
            End Using
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Function


    Public Function GetLast10ActivePatients() As List(Of PatientRecentActivity)
        Using conn As SqlConnection = DentistXDATA.GetConnection
            Dim sql As String = "SELECT TOP 10
                                    p.PatientID,
                                    p.PatientName,
                                    n.Note AS LastNote,
                                    rx.RX AS LastRX,
                                    ttrt.Treat AS LastToothTreatment,
                                    ptrt.Detail AS LastTrtDetail,

                                    act.LastActivityDate

                                FROM Patient p
                                LEFT JOIN Patient_Notes n ON p.PatientID = n.PatientID
                                LEFT JOIN Patient_Pays pay ON p.PatientID = pay.PatientID
                                LEFT JOIN Patient_RX rx ON p.PatientID = rx.PatientID
                                LEFT JOIN Patient_ToothTrt ttrt ON p.PatientID = ttrt.PatientID
                                LEFT JOIN Patient_Trts ptrt ON p.PatientID = ptrt.PatientID

                                CROSS APPLY (
                                    SELECT MAX(ActivityDate) AS LastActivityDate
                                    FROM (
                                        SELECT CAST(n.NoteDate AS DATETIME) AS ActivityDate WHERE n.NoteDate IS NOT NULL
                                        UNION ALL
                                        SELECT CAST(pay.PayDate AS DATETIME) WHERE pay.PayDate IS NOT NULL
                                        UNION ALL
                                        SELECT CAST(rx.RXDate AS DATETIME) WHERE rx.RXDate IS NOT NULL
                                        UNION ALL
                                        SELECT CAST(ttrt.TreatDate AS DATETIME) WHERE ttrt.TreatDate IS NOT NULL
                                        UNION ALL
                                        SELECT CAST(ptrt.TrtDate AS DATETIME) WHERE ptrt.TrtDate IS NOT NULL
                                    ) AS AllDates
                                ) AS act

                                ORDER BY act.LastActivityDate DESC
                                "
            Return conn.Query(Of PatientRecentActivity)(sql).ToList()
        End Using
    End Function

    Public Function GetRecentUniquePatients(Optional daysBack As Integer = 30, Optional topN As Integer = 10) As List(Of PatientRecentActivity2)
        Using conn As SqlConnection = DentistXDATA.GetConnection
            Dim sql As String =
"WITH AllPatientActivities AS (
    SELECT
        p.PatientID,
        p.PatientName,
        CAST(n.NoteDate AS DATETIME) AS ActivityDate,
        CAST('Note' AS VARCHAR(20)) AS ActivitySource,
        n.Note AS LastNote,
        CAST(NULL AS NVARCHAR(MAX)) AS LastRX,
        CAST(NULL AS NVARCHAR(MAX)) AS LastToothTreatment,
        CAST(NULL AS NVARCHAR(MAX)) AS LastTrtDetail,
        CAST(NULL AS NVARCHAR(MAX)) AS LastApptReason,
        CAST(NULL AS NVARCHAR(MAX)) AS LastOtherTrt,
        CAST(NULL AS NVARCHAR(MAX)) AS LastDiagTreat,
        CAST(NULL AS NVARCHAR(MAX)) AS LastOrthoWireNotes,
        CAST(NULL AS NVARCHAR(MAX)) AS LastOrthoKhota
    FROM Patient p
    INNER JOIN Patient_Notes n ON p.PatientID = n.PatientID
    WHERE n.NoteDate >= DATEADD(DAY, -@DaysBack, CAST(GETDATE() AS DATE))

    UNION ALL

    SELECT
        p.PatientID,
        p.PatientName,
        CAST(pay.PayDate AS DATETIME),
        CAST('Pay' AS VARCHAR(20)),
        NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL
    FROM Patient p
    INNER JOIN Patient_Pays pay ON p.PatientID = pay.PatientID
    WHERE pay.PayDate >= DATEADD(DAY, -@DaysBack, CAST(GETDATE() AS DATE))

    UNION ALL

    SELECT
        p.PatientID,
        p.PatientName,
        CAST(rx.RXDate AS DATETIME),
        CAST('Rx' AS VARCHAR(20)),
        NULL, rx.RX, NULL, NULL, NULL, NULL, NULL, NULL, NULL
    FROM Patient p
    INNER JOIN Patient_RX rx ON p.PatientID = rx.PatientID
    WHERE rx.RXDate >= DATEADD(DAY, -@DaysBack, CAST(GETDATE() AS DATE))

    UNION ALL

    SELECT
        p.PatientID,
        p.PatientName,
        CAST(ttrt.TreatDate AS DATETIME),
        CAST('ToothTrt' AS VARCHAR(20)),
        NULL, NULL, ttrt.Treat, NULL, NULL, NULL, NULL, NULL, NULL
    FROM Patient p
    INNER JOIN Patient_ToothTrt ttrt ON p.PatientID = ttrt.PatientID
    WHERE ttrt.TreatDate >= DATEADD(DAY, -@DaysBack, CAST(GETDATE() AS DATE))

    UNION ALL

    SELECT
        p.PatientID,
        p.PatientName,
        CAST(ptrt.TrtDate AS DATETIME),
        CAST('Trts' AS VARCHAR(20)),
        NULL, NULL, NULL, ptrt.Detail, NULL, NULL, NULL, NULL, NULL
    FROM Patient p
    INNER JOIN Patient_Trts ptrt ON p.PatientID = ptrt.PatientID
    WHERE ptrt.TrtDate >= DATEADD(DAY, -@DaysBack, CAST(GETDATE() AS DATE))

    UNION ALL

    SELECT
        p.PatientID,
        p.PatientName,
        CAST(a.StartDateTime AS DATETIME),
        CAST('Appt' AS VARCHAR(20)),
        NULL, NULL, NULL, NULL, a.Reason, NULL, NULL, NULL, NULL
    FROM Patient p
    INNER JOIN dbo.AppointmentC a ON p.PatientID = a.PatientID
    WHERE a.StartDateTime >= DATEADD(DAY, -@DaysBack, CAST(GETDATE() AS DATE))

    UNION ALL

    SELECT
        p.PatientID,
        p.PatientName,
        CAST(ot.TreatDate AS DATETIME),
        CAST('OtherTrt' AS VARCHAR(20)),
        NULL, NULL, NULL, NULL, NULL, ot.Trt, NULL, NULL, NULL
    FROM Patient p
    INNER JOIN dbo.Patient_OtherTRT ot ON p.PatientID = ot.PatientID
    WHERE ot.TreatDate >= DATEADD(DAY, -@DaysBack, CAST(GETDATE() AS DATE))

    UNION ALL

    SELECT
        p.PatientID,
        p.PatientName,
        CAST(d.TreatDate AS DATETIME),
        CAST('Diag' AS VARCHAR(20)),
        NULL, NULL, NULL, NULL, NULL, NULL, d.Treat, NULL, NULL
    FROM Patient p
    INNER JOIN dbo.Patient_Diagnosis d ON p.PatientID = d.PatientID
    WHERE d.TreatDate >= DATEADD(DAY, -@DaysBack, CAST(GETDATE() AS DATE))

    UNION ALL

    SELECT
        p.PatientID,
        p.PatientName,
        CAST(od.WorkDate AS DATETIME),
        CAST('OrthoDet' AS VARCHAR(20)),
        NULL, NULL, NULL, NULL, NULL, NULL, NULL, od.WireNotes, NULL
    FROM Patient p
    INNER JOIN dbo.OrthoTrtDet od ON p.PatientID = od.PatientID
    WHERE od.WorkDate >= DATEADD(DAY, -@DaysBack, CAST(GETDATE() AS DATE))

    UNION ALL

    SELECT
        p.PatientID,
        p.PatientName,
        CAST(oi.TreatDate AS DATETIME),
        CAST('OrthoInf' AS VARCHAR(20)),
        NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, oi.Khota
    FROM Patient p
    INNER JOIN dbo.OrthoInf oi ON p.PatientID = oi.PatientID
    WHERE oi.TreatDate >= DATEADD(DAY, -@DaysBack, CAST(GETDATE() AS DATE))
),
RankedActivities AS (
    SELECT
        a.*,
        ROW_NUMBER() OVER (
            PARTITION BY a.PatientID
            ORDER BY
                a.ActivityDate DESC,
                CASE a.ActivitySource
                    WHEN 'Diag' THEN 1
                    WHEN 'OrthoDet' THEN 2
                    WHEN 'OrthoInf' THEN 3
                    WHEN 'Note' THEN 4
                    WHEN 'Rx' THEN 5
                    WHEN 'ToothTrt' THEN 6
                    WHEN 'Trts' THEN 7
                    WHEN 'Pay' THEN 8
                    WHEN 'OtherTrt' THEN 9
                    WHEN 'Appt' THEN 10
                END
        ) AS rn
    FROM AllPatientActivities a
),
PerPatientLatest AS (
    SELECT PatientID, ActivityDate, ActivitySource
    FROM RankedActivities
    WHERE rn = 1
),
Grouped AS (
    SELECT
        PatientID,
        PatientName,
        MAX(ActivityDate) AS LastActivityDate,
        MAX(LastNote) AS LastNote,
        MAX(LastRX) AS LastRX,
        MAX(LastToothTreatment) AS LastToothTreatment,
        MAX(LastTrtDetail) AS LastTrtDetail,
        MAX(LastApptReason) AS LastApptReason,
        MAX(LastOtherTrt) AS LastOtherTrt,
        MAX(LastDiagTreat) AS LastDiagTreat,
        MAX(LastOrthoWireNotes) AS LastOrthoWireNotes,
        MAX(LastOrthoKhota) AS LastOrthoKhota
    FROM AllPatientActivities
    GROUP BY PatientID, PatientName
)
SELECT TOP (@TopN) WITH TIES
    g.PatientID,
    g.PatientName,
    l.ActivityDate AS LastActivityDate,
    l.ActivitySource AS LastActivitySource,
    g.LastNote,
    g.LastRX,
    g.LastToothTreatment,
    g.LastTrtDetail,
    g.LastApptReason,
    g.LastOtherTrt,
    g.LastDiagTreat,
    g.LastOrthoWireNotes,
    g.LastOrthoKhota
FROM Grouped g
INNER JOIN PerPatientLatest l ON l.PatientID = g.PatientID
ORDER BY l.ActivityDate DESC"
            Dim raw = conn.Query(Of PatientRecentActivity2)(sql, New With {.TopN = topN, .DaysBack = daysBack}).ToList()
            For Each r In raw
                ApplyActivityDisplayKeys(r)
            Next
            Return raw
        End Using
    End Function

    ''' <summary>Prefix each non-empty detail with the same key used for SQL ActivitySource / navigation.</summary>
    Private Shared Sub ApplyActivityDisplayKeys(r As PatientRecentActivity2)
        If r Is Nothing Then Return
        PrefixDetail(r.LastNote, "Note", Sub(v) r.LastNote = v)
        PrefixDetail(r.LastRX, "Rx", Sub(v) r.LastRX = v)
        PrefixDetail(r.LastToothTreatment, "Tooth treatment", Sub(v) r.LastToothTreatment = v)
        PrefixDetail(r.LastTrtDetail, "Treatment", Sub(v) r.LastTrtDetail = v)
        PrefixDetail(r.LastApptReason, "Appointment", Sub(v) r.LastApptReason = v)
        PrefixDetail(r.LastOtherTrt, "Other treat", Sub(v) r.LastOtherTrt = v)
        PrefixDetail(r.LastDiagTreat, "Diagnose", Sub(v) r.LastDiagTreat = v)
        PrefixDetail(r.LastOrthoWireNotes, "Ortho details", Sub(v) r.LastOrthoWireNotes = v)
        PrefixDetail(r.LastOrthoKhota, "Ortho start", Sub(v) r.LastOrthoKhota = v)
    End Sub

    Private Shared Sub PrefixDetail(raw As String, key As String, apply As Action(Of String))
        If String.IsNullOrWhiteSpace(raw) Then Return
        Dim t = raw.Trim()
        Dim prefix = key & " — "
        If t.StartsWith(prefix, StringComparison.OrdinalIgnoreCase) Then Return
        apply(prefix & t)
    End Sub

    Private Sub FrmLastPatients_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        GridView1.OptionsBehavior.Editable = False
        GridView1.OptionsBehavior.ReadOnly = True
        'EnsureTrackingGridColumns()
        'ApplyTrackingColumnCaptions()
        PatientBS.DataSource = GetRecentUniquePatients() ' GetLast10ActivePatients()
    End Sub

    Private Sub ApplyTrackingColumnCaptions()
        colLastNote.Caption = "Note"
        colLastRX.Caption = "Rx"
        colLastToothTreatment.Caption = "Tooth treatment"
        colLastTrtDetail.Caption = "Treatment"
    End Sub

    ''' <summary>Adds columns for extended activity fields (not bound in designer).</summary>
    Private Sub EnsureTrackingGridColumns()
        Dim specs As (Field As String, Caption As String)() = {
            ("LastApptReason", "Appointment"),
            ("LastOtherTrt", "Other treat"),
            ("LastDiagTreat", "Diagnose"),
            ("LastOrthoWireNotes", "Ortho details"),
            ("LastOrthoKhota", "Ortho start")
        }
        For Each sp In specs
            If GridView1.Columns.ColumnByFieldName(sp.Field) Is Nothing Then
                Dim c = GridView1.Columns.AddVisible(sp.Field, sp.Caption)
                c.OptionsColumn.AllowEdit = False
            End If
        Next
    End Sub

    ''' <summary>Double-click a cell that has text to open the module for that activity column (not the whole row).</summary>
    Private Sub lastPatientsGrid_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles lastPatientsGrid.MouseDoubleClick
        If e.Button <> MouseButtons.Left Then Return
        Dim hi = GridView1.CalcHitInfo(e.Location)
        If Not hi.InRowCell OrElse hi.Column Is Nothing OrElse hi.RowHandle < 0 Then Return

        Dim row = TryCast(GridView1.GetRow(hi.RowHandle), PatientRecentActivity2)
        If row Is Nothing OrElse row.PatientID <= 0 Then Return

        Dim fieldName = hi.Column.FieldName
        Dim activitySrc = ActivitySourceForColumnField(fieldName, row)
        If String.IsNullOrWhiteSpace(activitySrc) Then Return

        Dim cellText = GetRowCellDisplayText(row, fieldName)
        If String.IsNullOrWhiteSpace(cellText) Then Return

        Dim owner = Me.Owner
        Me.Close()
        BeginInvokeNavigation(owner, row, activitySrc.Trim())
    End Sub

    Private Shared Function ActivitySourceForColumnField(fieldName As String, row As PatientRecentActivity2) As String
        If String.IsNullOrWhiteSpace(fieldName) Then Return Nothing
        Select Case fieldName
            Case "LastNote" : Return "Note"
            Case "LastRX" : Return "Rx"
            Case "LastToothTreatment" : Return "ToothTrt"
            Case "LastTrtDetail" : Return "Trts"
            Case "LastApptReason" : Return "Appt"
            Case "LastOtherTrt" : Return "OtherTrt"
            Case "LastDiagTreat" : Return "Diag"
            Case "LastOrthoWireNotes" : Return "OrthoDet"
            Case "LastOrthoKhota" : Return "OrthoInf"
            Case "LastActivitySource"
                Return If(row?.LastActivitySource, "").Trim()
            Case Else
                Return Nothing
        End Select
    End Function

    Private Shared Function GetRowCellDisplayText(row As PatientRecentActivity2, fieldName As String) As String
        If row Is Nothing Then Return Nothing
        Select Case fieldName
            Case "LastNote" : Return row.LastNote
            Case "LastRX" : Return row.LastRX
            Case "LastToothTreatment" : Return row.LastToothTreatment
            Case "LastTrtDetail" : Return row.LastTrtDetail
            Case "LastApptReason" : Return row.LastApptReason
            Case "LastOtherTrt" : Return row.LastOtherTrt
            Case "LastDiagTreat" : Return row.LastDiagTreat
            Case "LastOrthoWireNotes" : Return row.LastOrthoWireNotes
            Case "LastOrthoKhota" : Return row.LastOrthoKhota
            Case "LastActivitySource" : Return row.LastActivitySource
            Case Else : Return Nothing
        End Select
    End Function

    Private Sub BeginInvokeNavigation(owner As Form, row As PatientRecentActivity2, activitySource As String)
        If owner IsNot Nothing Then
            owner.BeginInvoke(New Action(Of Form, PatientRecentActivity2, String)(AddressOf NavigateToPatientModule), owner, row, activitySource)
        Else
            NavigateToPatientModule(Nothing, row, activitySource)
        End If
    End Sub

    Private Shared Sub NavigateToPatientModule(owner As Form, row As PatientRecentActivity2, activitySource As String)
        Try
            Dim pd As New PatientDATA()
            Dim patient = pd.Select_RecordByID(row.PatientID)
            If patient Is Nothing Then Return

            Dim mv3 = TryCast(owner, MainView3)
            Dim mv1 = TryCast(owner, MainView1)
            If mv3 Is Nothing Then mv3 = TryCast(Application.OpenForms("MainView3"), MainView3)
            If mv1 Is Nothing Then mv1 = TryCast(Application.OpenForms("MainView1"), MainView1)
            Dim workspaceHost As Form = TryCast(mv3, Form)
            If workspaceHost Is Nothing Then workspaceHost = TryCast(mv1, Form)

            Dim src = If(activitySource, "").Trim()
            If String.IsNullOrWhiteSpace(src) Then Return

            Select Case src
                Case "Pay"
                    FormManager.Instance.SwitchUserControl(GetType(NewAccounting), "Accounts", patient, workspaceHost)
                Case "Rx"
                    FormManager.Instance.SwitchUserControl(GetType(RxCTLNEW), "Rx", patient, workspaceHost)
                Case "Note"
                    FormManager.Instance.SwitchUserControl(GetType(CtlNotes), "Notes", patient, workspaceHost)
                Case "ToothTrt"
                    FormManager.Instance.SwitchUserControl(GetType(TreatsUserControl), "Treat", patient, workspaceHost)
                Case "Trts"
                    FormManager.Instance.SwitchUserControl(GetType(NewAccounting), "Accounts", patient, workspaceHost)
                Case "Appt"
                    FormManager.Instance.SwitchUserControl(GetType(PatientVisitsCtl), "Visits", patient, workspaceHost)
                Case "Diag"
                    FormManager.Instance.SwitchUserControl(GetType(DiagUserControl), "Diag", patient, workspaceHost)
                Case "OrthoDet", "OrthoInf"
                    FormManager.Instance.SwitchUserControl(GetType(FullOrthoTreating), "Ortho", patient, workspaceHost)

                Case "OtherTrt"
                    Using f As New FrmOtherTRTs(patient.PatientID)
                        Dim wh = TryCast(workspaceHost, Form)
                        If wh IsNot Nothing Then f.Icon = wh.Icon
                        f.ShowDialog(workspaceHost)
                    End Using
                Case Else
                    Return
            End Select

            If workspaceHost IsNot Nothing Then workspaceHost.Activate()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Navigation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End Try
    End Sub

    Private Sub btnListPatients_Click(sender As Object, e As EventArgs) Handles btnListPatients.Click
        PatientBS.DataSource = GetRecentUniquePatients(CInt(txLastNDays.Text), CInt(txTopN.Text)) 'GetRecentActivePatients(CInt(txTopN.Text))
    End Sub

    Private Sub txLastN_KeyDown(sender As Object, e As KeyEventArgs) Handles txTopN.KeyDown, txLastNDays.KeyDown
        ' Allow control keys: backspace, delete, arrows, tab, home/end, etc.
        If e.KeyCode = Keys.Back OrElse
           e.KeyCode = Keys.Delete OrElse
           e.KeyCode = Keys.Left OrElse
           e.KeyCode = Keys.Right OrElse
           e.KeyCode = Keys.Up OrElse
           e.KeyCode = Keys.Down OrElse
           e.KeyCode = Keys.Tab OrElse
           e.KeyCode = Keys.Home OrElse
           e.KeyCode = Keys.End Then
            Return
        End If

        ' Allow digits only (top row or numpad)
        Dim isTopRowDigit As Boolean = (e.KeyCode >= Keys.D0 AndAlso e.KeyCode <= Keys.D9)
        Dim isNumPadDigit As Boolean = (e.KeyCode >= Keys.NumPad0 AndAlso e.KeyCode <= Keys.NumPad9)

        ' Block Shift-modified digits (to avoid !, @, etc.)
        If (isTopRowDigit OrElse isNumPadDigit) AndAlso (Not e.Shift) Then
            Return
        End If

        ' Otherwise block the key
        e.SuppressKeyPress = True
        e.Handled = True
    End Sub



    Public Class PatientRecentActivity2
        Public Property PatientID As Integer
        Public Property PatientName As String
        Public Property LastActivityDate As DateTime?
        ''' <summary>Which activity type had <see cref="LastActivityDate"/> (drives double-click navigation).</summary>
        Public Property LastActivitySource As String
        Public Property LastNote As String
        Public Property LastRX As String
        Public Property LastToothTreatment As String
        Public Property LastTrtDetail As String
        Public Property LastApptReason As String
        Public Property LastOtherTrt As String
        Public Property LastDiagTreat As String
        Public Property LastOrthoWireNotes As String
        Public Property LastOrthoKhota As String
    End Class

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
End Class