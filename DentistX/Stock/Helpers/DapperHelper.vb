Imports Dapper
Imports System.Configuration
Imports System.Data.SqlClient
Imports System.Threading.Tasks

Public Class DapperHelper
    Private ReadOnly _connectionString As String

    Public Sub New(connectionString As String)
        _connectionString = connectionString
    End Sub

    Public Function Query(Of T)(sql As String, Optional parameters As Object = Nothing) As IEnumerable(Of T)
        Using conn As New SqlConnection(_connectionString)
            Return conn.Query(Of T)(sql, parameters)
        End Using
    End Function

    Public Async Function QueryAsync(Of T)(sql As String, Optional parameters As Object = Nothing) As Task(Of IEnumerable(Of T))
        Using conn As New SqlConnection(_connectionString)
            Return Await conn.QueryAsync(Of T)(sql, parameters)
        End Using
    End Function

    Public Async Function ExecuteAsync(sql As String, Optional parameters As Object = Nothing) As Task(Of Integer)
        Using conn As New SqlConnection(_connectionString)
            Return Await conn.ExecuteAsync(sql, parameters)
        End Using
    End Function

    Public Async Function QuerySingleAsync(Of T)(sql As String, Optional parameters As Object = Nothing, Optional commandTimeoutSeconds As Integer? = Nothing) As Task(Of T)
        Using conn As New SqlConnection(_connectionString)
            Return Await conn.QuerySingleOrDefaultAsync(Of T)(sql, parameters, transaction:=Nothing, commandTimeout:=commandTimeoutSeconds)
        End Using
    End Function

    Public Function QuerySingle(Of T)(sql As String, Optional parameters As Object = Nothing) As T
        Using conn As New SqlConnection(_connectionString)
            Return conn.QuerySingleOrDefault(Of T)(sql, parameters)
        End Using
    End Function

    Public Async Function ExecuteInTransactionAsync(operations As Func(Of SqlTransaction, Task)) As Task
        Using conn As New SqlConnection(_connectionString)
            Await conn.OpenAsync()
            Using trans = conn.BeginTransaction()
                Try
                    Await operations.Invoke(trans)
                    trans.Commit()
                Catch
                    trans.Rollback()
                    Throw
                End Try
            End Using
        End Using
    End Function
End Class

Public Class InventoryException
    Inherits Exception
    Public Sub New(message As String, Optional innerException As Exception = Nothing)
        MyBase.New(message, innerException)
    End Sub
End Class

Public Class InsufficientStockException
    Inherits InventoryException
    Public Sub New(message As String)
        MyBase.New(message)
    End Sub
End Class
