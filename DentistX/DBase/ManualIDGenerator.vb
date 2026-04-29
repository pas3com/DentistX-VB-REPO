Imports Dapper
Imports System.Data.SqlClient
Imports System.Linq

Public Class ManualIDGenerator
    Public Shared Function GetNextManualID(tableName As String, Optional connectionString As String = Nothing,
                                          Optional primaryKeyColumn As String = Nothing) As Integer
        ' Validate table name
        If String.IsNullOrWhiteSpace(tableName) Then
            Throw New ArgumentException("Table name cannot be empty", NameOf(tableName))
        End If

        ' Clean table name to prevent SQL injection
        tableName = CleanTableName(tableName)

        If String.IsNullOrEmpty(connectionString) Then
            Dim Dx As New DentistXDATA()
            connectionString = Dx.GetConnectionString()
        End If

        Using conn As New SqlConnection(connectionString)
            conn.Open()

            ' Get or validate primary key column
            Dim pkColumn As String = If(Not String.IsNullOrEmpty(primaryKeyColumn),
                                      ValidatePrimaryKeyColumn(conn, tableName, primaryKeyColumn),
                                      GetPrimaryKeyColumn(conn, tableName))

            If String.IsNullOrEmpty(pkColumn) Then
                Throw New Exception($"No suitable primary key found for table '{tableName}'")
            End If

            ' Verify the primary key is integer type
            If Not IsIntegerPrimaryKey(conn, tableName, pkColumn) Then
                Throw New Exception($"Primary key column '{pkColumn}' in table '{tableName}' is not of integer type")
            End If

            ' Get the next ID using MAX + 1
            Return GetNextIDFromTable(conn, tableName, pkColumn)
        End Using
    End Function

    Private Shared Function GetNextIDFromTable(conn As SqlConnection, tableName As String, pkColumn As String) As Integer
        Try
            Dim sql = $"
                SELECT ISNULL(MAX([{pkColumn}]), 0) + 1 
                FROM [{tableName}] 
                WITH (UPDLOCK, HOLDLOCK)"  ' Prevents race conditions

            Return conn.ExecuteScalar(Of Integer)(sql, commandTimeout:=30)
        Catch ex As Exception
            Throw New Exception($"Failed to get next ID for table '{tableName}': {ex.Message}", ex)
        End Try
    End Function

    Private Shared Function GetPrimaryKeyColumn(conn As SqlConnection, tableName As String) As String
        Dim sql = "
            SELECT c.COLUMN_NAME
            FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS tc
            INNER JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE c 
                ON tc.CONSTRAINT_NAME = c.CONSTRAINT_NAME 
                AND tc.TABLE_SCHEMA = c.TABLE_SCHEMA
            WHERE tc.CONSTRAINT_TYPE = 'PRIMARY KEY'
            AND tc.TABLE_NAME = @TableName"

        Return conn.ExecuteScalar(Of String)(sql, New With {.TableName = tableName})
    End Function

    Private Shared Function ValidatePrimaryKeyColumn(conn As SqlConnection, tableName As String, columnName As String) As String
        Dim sql = "
            SELECT COLUMN_NAME
            FROM INFORMATION_SCHEMA.COLUMNS
            WHERE TABLE_NAME = @TableName 
            AND COLUMN_NAME = @ColumnName
            AND DATA_TYPE IN ('int', 'bigint', 'smallint', 'tinyint')"

        Dim result = conn.ExecuteScalar(Of String)(sql, New With {
            .TableName = tableName,
            .ColumnName = columnName
        })

        Return If(String.IsNullOrEmpty(result), Nothing, columnName)
    End Function

    Private Shared Function IsIntegerPrimaryKey(conn As SqlConnection, tableName As String, pkColumn As String) As Boolean
        Dim sql = "
            SELECT DATA_TYPE
            FROM INFORMATION_SCHEMA.COLUMNS
            WHERE TABLE_NAME = @TableName 
            AND COLUMN_NAME = @ColumnName"

        Dim dataType = conn.ExecuteScalar(Of String)(sql, New With {
            .TableName = tableName,
            .ColumnName = pkColumn
        })

        If dataType Is Nothing Then Return False

        Dim integerTypes() As String = {"int", "bigint", "smallint", "tinyint"}
        Return integerTypes.Contains(dataType.ToLower())
    End Function

    Private Shared Function CleanTableName(tableName As String) As String
        ' Remove any potentially dangerous characters
        Return System.Text.RegularExpressions.Regex.Replace(tableName, "[^a-zA-Z0-9_]", "")
    End Function
End Class