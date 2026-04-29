Imports System.Data.SqlClient
Imports Dapper

Public Module PatientFilterModule

    Private ReadOnly ConnectionString As String = DentistXDATA.GetConnection.ConnectionString

    ' Retrieves all table names that have a foreign key referencing the Patient table
    Public Function GetRelatedTables() As List(Of String)
        Dim query As String = "
            SELECT DISTINCT
                OBJECT_NAME(fk.parent_object_id) AS ReferencingTable
            FROM
                sys.foreign_keys AS fk
            INNER JOIN
                sys.foreign_key_columns AS fkc ON fk.object_id = fkc.constraint_object_id
            INNER JOIN
                sys.tables AS t ON t.object_id = fk.parent_object_id
            WHERE
                fk.referenced_object_id = OBJECT_ID('Patient');
        "

        Using connection As New SqlConnection(ConnectionString)
            Return connection.Query(Of String)(query).ToList()
        End Using
    End Function

    ' Retrieves patients who have related records in the specified table
    Public Function GetPatientsWithRelation(tableName As String) As IEnumerable(Of Patient)
        ' Ensure the table name is valid to prevent SQL injection
        Dim allowedTables = GetRelatedTables()
        If Not allowedTables.Contains(tableName) Then
            Throw New ArgumentException($"Table '{tableName}' is not a valid related table.")
        End If

        Dim query As String = $"
            SELECT DISTINCT p.*
            FROM Patient p
            INNER JOIN [{tableName}] t ON p.PatientID = t.PatientID
        "

        Using connection As New SqlConnection(ConnectionString)
            Return connection.Query(Of Patient)(query).ToList()
        End Using
    End Function

End Module
