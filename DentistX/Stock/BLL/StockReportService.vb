Imports System.Data
Imports System.Data.SqlClient
Imports System.Threading.Tasks
Imports Dapper

Module StockBLL
    Public Class StockReportService
        Private ReadOnly _db As DapperHelper
        Private ReadOnly _connectionString As String

        Public Sub New(connectionString As String)
            _connectionString = connectionString
            _db = New DapperHelper(connectionString)
        End Sub

        Public Async Function GetTotalStockValueAsync() As Task(Of Decimal)
            Const sql = "
                SELECT ISNULL(SUM(b.CurrentQuantity * ISNULL(li.UnitPrice, 0)), 0)
                FROM Batchess b
                OUTER APPLY (
                    SELECT TOP 1 UnitPrice
                    FROM BuyInvoiceLineItems li
                    WHERE li.ProductID = b.ProductID
                    ORDER BY li.LineItemID DESC
                ) li"
            Dim result = Await _db.QuerySingleAsync(Of Decimal)(sql)
            Return result
        End Function

        Public Async Function GetLowStockCountAsync() As Task(Of Integer)
            Const sql = "
                SELECT COUNT(*)
                FROM Products p
                JOIN (
                    SELECT ProductID, SUM(CurrentQuantity) AS TotalStock
                    FROM Batchess
                    GROUP BY ProductID
                ) b ON p.ProductID = b.ProductID
                WHERE b.TotalStock < p.ReorderLevel"
            Return Await _db.QuerySingleAsync(Of Integer)(sql)
        End Function

        Public Async Function GetExpiringItemsCountAsync(daysThreshold As Integer) As Task(Of Integer)
            Const sql = "
                SELECT ISNULL(COUNT(*), 0)
                FROM Batchess
                WHERE ExpirationDate BETWEEN CAST(GETDATE() AS date) AND DATEADD(day, @Days, CAST(GETDATE() AS date))
                  AND CurrentQuantity > 0"
            Return Await _db.QuerySingleAsync(Of Integer)(sql, New With {.Days = daysThreshold})
        End Function

        Public Async Function GetExpiringBatchesAsync(daysThreshold As Integer) As Task(Of List(Of ExpiringBatchDto))
            Const sql = "
                SELECT b.BatchID, b.ProductID, p.ProductName, b.BatchNumber, b.ExpirationDate,
                       b.CurrentQuantity,
                       DATEDIFF(day, CAST(GETDATE() AS date), b.ExpirationDate) AS DaysToExpire
                FROM Batchess b
                JOIN Products p ON b.ProductID = p.ProductID
                WHERE b.CurrentQuantity > 0
                  AND b.ExpirationDate <= DATEADD(day, @Days, CAST(GETDATE() AS date))
                ORDER BY b.ExpirationDate"
            Dim results = Await _db.QueryAsync(Of ExpiringBatchDto)(sql, New With {.Days = daysThreshold})
            Return results.ToList()
        End Function

        Public Async Function GetStockTrendsAsync() As Task(Of List(Of StockTrendDto))
            Const sql = "
                SELECT FORMAT(MovementDate, 'yyyy-MM') AS Month,
                       SUM(QuantityChange) AS StockLevel
                FROM StockMovements
                WHERE MovementDate > DATEADD(month, -6, GETDATE())
                GROUP BY FORMAT(MovementDate, 'yyyy-MM')
                ORDER BY FORMAT(MovementDate, 'yyyy-MM')"
            Dim results = Await _db.QueryAsync(Of StockTrendDto)(sql)
            Return results.ToList()
        End Function

        Public Function GenerateMovementReport(startDate As Date, endDate As Date) As DataTable
            Const sql = "
                SELECT m.MovementDate, p.ProductName,
                       b.BatchNumber, m.QuantityChange, m.MovementType
                FROM StockMovements m
                JOIN Products p ON m.ProductID = p.ProductID
                LEFT JOIN Batchess b ON m.BatchID = b.BatchID
                WHERE m.MovementDate BETWEEN @Start AND @End"
            Using conn As New SqlConnection(_connectionString)
                Dim reader = conn.ExecuteReader(sql, New With {.Start = startDate, .End = endDate})
                Dim dt As New DataTable()
                dt.Load(reader)
                Return dt
            End Using
        End Function
    End Class
End Module
