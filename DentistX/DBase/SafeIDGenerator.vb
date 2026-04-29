Imports System.Data.SqlClient
Imports Dapper

Public Class SafeIDGenerator
    Public Shared Function GetNextManualID(tableName As String, Optional connectionString As String = Nothing,
                                          Optional useTransaction As Boolean = True) As Integer
        If String.IsNullOrEmpty(connectionString) Then
            Dim Dx As New DentistXDATA()
            connectionString = Dx.GetConnectionString()
        End If

        tableName = CleanTableName(tableName)

        Using conn As New SqlConnection(connectionString)
            conn.Open()

            If useTransaction Then
                Using trans As SqlTransaction = conn.BeginTransaction()
                    Try
                        Dim nextID = GetNextIDInTransaction(conn, trans, tableName)
                        trans.Commit()
                        Return nextID
                    Catch
                        trans.Rollback()
                        Throw
                    End Try
                End Using
            Else
                Return GetNextIDInTransaction(conn, Nothing, tableName)
            End If
        End Using
    End Function

    Private Shared Function GetNextIDInTransaction(conn As SqlConnection, trans As SqlTransaction, tableName As String) As Integer
        Dim pkColumn = GetPrimaryKeyColumn(conn, tableName)
        If String.IsNullOrEmpty(pkColumn) Then
            Throw New Exception($"No primary key found for table '{tableName}'")
        End If

        ' Use serializable isolation level implicitly with UPDLOCK
        Dim sql = $"
            SELECT ISNULL(MAX([{pkColumn}]), 0) + 1 
            FROM [{tableName}] 
            WITH (UPDLOCK, HOLDLOCK)"

        If trans IsNot Nothing Then
            Return conn.ExecuteScalar(Of Integer)(sql, trans, commandTimeout:=30)
        Else
            Return conn.ExecuteScalar(Of Integer)(sql, commandTimeout:=30)
        End If
    End Function

    ' Include the same helper methods from Option 2 here...
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

        Dim integerTypes() As String = {"int", "bigint", "smallint", "tinyint"}
        Return integerTypes.Contains(dataType.ToLower())
    End Function

    Private Shared Function CleanTableName(tableName As String) As String
        ' Remove any potentially dangerous characters
        Return System.Text.RegularExpressions.Regex.Replace(tableName, "[^a-zA-Z0-9_]", "")
    End Function
End Class
