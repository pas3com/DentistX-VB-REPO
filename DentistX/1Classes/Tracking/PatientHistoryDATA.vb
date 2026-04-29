Imports System.Data.SqlClient
Imports Dapper

Public Class PatientHistoryDATA


    Private ConnectionString As String = DentistXDATA.GetConnection.ConnectionString

    Public Function SelectAll() As IEnumerable(Of PatientHistory)
        Using conn As New SqlConnection(ConnectionString)
            conn.Open()
            Return conn.Query(Of PatientHistory)("SELECT * FROM PatientHistoryLog")
        End Using
    End Function

    Public Function Select_Record(ByVal clsPatientHistoryLog As PatientHistory) As PatientHistory
        Using conn As New SqlConnection(ConnectionString)
            Dim sql As String = "Select * FROM PatientHistoryLog WHERE HistoryID = @HistoryID"
            Return conn.QuerySingleOrDefault(Of PatientHistory)(sql, New With {.HistoryID = clsPatientHistoryLog.HistoryID})
        End Using
    End Function

    Public Function GetHistoryGroupedByDay() As Dictionary(Of Date, List(Of PatientHistory))
        Using con As New SqlConnection(ConnectionString)
            Dim sql = "
            SELECT * FROM PatientHistory
            ORDER BY CAST(ActionTime AS DATE) DESC, ActionTime DESC"
            Dim result = con.Query(Of PatientHistory)(sql).ToList()
            Return result.GroupBy(Function(h) h.ActionTime.Date).ToDictionary(Function(g) g.Key, Function(g) g.ToList())
        End Using
    End Function


    Public Sub LogHistory(loggedBy As Integer?, patientName As String, tableName As String, action As String, Optional info As String = Nothing)
        Using con As New SqlConnection(ConnectionString)
            Dim sql = "
            INSERT INTO PatientHistory (LoggedBy, PatientName, TableName, Action, AdditionalInfo)
            VALUES (@LoggedBy, @PatientName, @TableName, @Action, @AdditionalInfo)"
            con.Execute(sql, New With {
            .LoggedBy = loggedBy,
            .PatientName = patientName,
            .TableName = tableName,
            .Action = action,
            .AdditionalInfo = info
        })
        End Using
    End Sub


    Public Function Delete(ByVal clsPatientHistoryLog As PatientHistory) As Boolean
        Dim deleteStatement As String =
        "DELETE FROM [PatientHistoryLog] 
			WHERE HistoryID = @HistoryID"
        Using connection As SqlConnection = DentistXDATA.GetConnection()
            connection.Open()
            Dim affectedRows As Integer = connection.Execute(deleteStatement, New With {.HistoryID = clsPatientHistoryLog.HistoryID})
            Return affectedRows > 0
        End Using
    End Function

    Public Function DeleteAll() As Boolean
        Dim deleteStatement As String =
        "DELETE FROM [PatientHistoryLog] "
        Using connection As SqlConnection = DentistXDATA.GetConnection()
            connection.Open()
            Dim affectedRows As Integer = connection.Execute(deleteStatement)
            Return affectedRows > 0
        End Using
    End Function
End Class
