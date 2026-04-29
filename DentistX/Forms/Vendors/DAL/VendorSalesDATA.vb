Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections
Imports System.Configuration
Imports Dapper

Public Class VendorSalesDATA

    Private ConnectionString As String = DentistXDATA.GetConnection.connectionString



    Public Function SelectAll() As IEnumerable(Of VendorSales)
        Using conn As New SqlConnection(ConnectionString)
            conn.Open()
            Return conn.Query(Of VendorSales)("SELECT * FROM VendorSales")
        End Using
    End Function


    Public Function Select_Record(ByVal clsVendorSales As VendorSales) As VendorSales
        Using conn As New SqlConnection(ConnectionString)
            Dim sql As String = "Select * FROM VendorSales WHERE SalesID = @SalesID"
            Return conn.QuerySingleOrDefault(Of VendorSales)(sql, New With {.SalesID = clsVendorSales.SalesID})
        End Using
    End Function

    Public Function Add(ByVal clsVendorSales As VendorSales) As Boolean
        Dim RowsAffected As Integer = 0
        Using conn As New SqlConnection(ConnectionString)
            Dim sql As String = "INSERT INTO VendorSales (VendID, Detail, SalesDate, SalesValue) VALUES (@VendID, @Detail, @SalesDate, @SalesValue)"
            RowsAffected = conn.Execute(sql, New With {.VendID = clsVendorSales.VendID, .Detail = clsVendorSales.Detail, .SalesDate = clsVendorSales.SalesDate, .SalesValue = clsVendorSales.SalesValue})
            Return RowsAffected > 0
        End Using
    End Function

    Public Function Update(oldVendorSales As VendorSales, newVendorSales As VendorSales) As Boolean
        Using conn As New SqlConnection(ConnectionString)
            Dim parameters = New With {
                    .NewVendID = newVendorSales.VendID, .OldVendID = oldVendorSales.VendID, .NewDetail = newVendorSales.Detail, .OldDetail = oldVendorSales.Detail, .NewSalesDate = newVendorSales.SalesDate, .OldSalesDate = oldVendorSales.SalesDate, .NewSalesValue = newVendorSales.SalesValue, .OldSalesValue = oldVendorSales.SalesValue
                                          }
            Dim affectedRows As Integer = conn.Execute("UPDATE [VendorSales] SET [VendID] = @NewVendID, [Detail] = @NewDetail, [SalesDate] = @NewSalesDate, [SalesValue] = @NewSalesValue WHERE [VendID] = @OldVendID AND [Detail] = @OldDetail AND [SalesDate] = @OldSalesDate AND [SalesValue] = @OldSalesValue", parameters)
            Return affectedRows > 0
        End Using
    End Function

    Public Function Delete(ByVal clsVendorSales As VendorSales) As Boolean
        Dim deleteStatement As String =
            "DELETE FROM [VendorSales] 
			WHERE SalesID = @SalesID"
        Using connection As SqlConnection = DentistXData.GetConnection()
            connection.Open()
            Dim affectedRows As Integer = connection.Execute(deleteStatement, New With {.SalesID = clsVendorSales.SalesID})
            Return affectedRows > 0
        End Using
    End Function

    Public Shared Function GetTotalBuys(vendID As Integer) As Decimal
        Using conn As SqlConnection = DentistXDATA.GetConnection()
            Return conn.ExecuteScalar(Of Decimal)(
                "SELECT ISNULL(SUM(SalesValue), 0) FROM VendorSales WHERE VendID = @VendID",
                New With {.VendID = vendID})
        End Using
    End Function

    Public Shared Function GetTotalPays(vendID As Integer) As Decimal
        Using conn As SqlConnection = DentistXDATA.GetConnection()
            Return conn.ExecuteScalar(Of Decimal)(
                "SELECT ISNULL(SUM(PayValue), 0) FROM VendorPays WHERE VendID = @VendID",
                New With {.VendID = vendID})
        End Using
    End Function

    Public Shared Function GetVendorBalance(vendID As Integer) As Decimal
        Dim totalBuys As Decimal = GetTotalBuys(vendID)
        Dim totalPays As Decimal = GetTotalPays(vendID)
        Return totalPays - totalBuys
    End Function

    Public Shared Function GetUnpaidBuyings(vendID As Integer) As List(Of VendorSales)
        Dim sql As String = "
            SELECT s.SalesID, s.VendID, s.Detail, s.SalesDate, s.SalesValue
            FROM VendorSales s
            LEFT JOIN (
                SELECT SalesID, SUM(PayValue) AS TotalPaid
                FROM VendorPays
                WHERE VendID = @VendID
                GROUP BY SalesID
            ) p ON s.SalesID = p.SalesID
            WHERE s.VendID = @VendID AND ISNULL(p.TotalPaid, 0) < s.SalesValue
        "

        Using conn As SqlConnection = DentistXDATA.GetConnection()
            Return conn.Query(Of VendorSales)(sql, New With {.VendID = vendID}).ToList()
        End Using
    End Function

    Public Shared Function GetUnpaidBuys(vendID As Integer) As List(Of UnpaidVendorSale)
        Dim sql As String = "
        SELECT 
            s.SalesID, 
            s.VendID, 
            s.Detail, 
            s.SalesDate, 
            s.SalesValue,
            ISNULL(p.TotalPaid, 0) AS TotalPaid
        FROM VendorSales s
        LEFT JOIN (
            SELECT SalesID, SUM(PayValue) AS TotalPaid
            FROM VendorPays
            WHERE VendID = @VendID
            GROUP BY SalesID
        ) p ON s.SalesID = p.SalesID
        WHERE s.VendID = @VendID AND ISNULL(p.TotalPaid, 0) < s.SalesValue
        ORDER BY s.SalesDate
    "

        Using conn As SqlConnection = DentistXDATA.GetConnection()
            Return conn.Query(Of UnpaidVendorSale)(sql, New With {.VendID = vendID}).ToList()
        End Using
    End Function

    Public Shared Function GetUnpaidBuys() As List(Of UnpaidVendorSale)
        '    Dim sql As String = "
        '    SELECT 
        '        s.SalesID, 
        '        s.VendID, 
        '        s.Detail, 
        '        s.SalesDate, 
        '        s.SalesValue,
        '        ISNULL(p.TotalPaid, 0) AS TotalPaid
        '    FROM VendorSales s
        '    LEFT JOIN (
        '        SELECT SalesID, SUM(PayValue) AS TotalPaid
        '        FROM VendorPays
        '        GROUP BY SalesID
        '    ) p ON s.SalesID = p.SalesID
        '    WHERE ISNULL(p.TotalPaid, 0) < s.SalesValue
        '    ORDER BY s.VendID, s.SalesDate
        '"
        Dim sql As String = "
        SELECT 
            s.SalesID, 
            s.VendID, 
            v.VendName,
            s.Detail, 
            s.SalesDate, 
            s.SalesValue,
            ISNULL(p.TotalPaid, 0) AS TotalPaid
        FROM VendorSales s
        INNER JOIN Vendors v ON s.VendID = v.VendID
        LEFT JOIN (
            SELECT SalesID, SUM(PayValue) AS TotalPaid
            FROM VendorPays
            GROUP BY SalesID
        ) p ON s.SalesID = p.SalesID
        WHERE ISNULL(p.TotalPaid, 0) < s.SalesValue
        ORDER BY s.VendID, s.SalesDate
    "
        Using conn As SqlConnection = DentistXDATA.GetConnection()
            Return conn.Query(Of UnpaidVendorSale)(sql).ToList()
        End Using
    End Function


    'Methods to get parents and childs
    Public Function GetVendors(ByVal VendID As Integer) As Vendors
        Dim parent As Vendors = Nothing
        Using conn As New SqlConnection(ConnectionString)
            Dim query As String = "SELECT * FROM [Vendors] WHERE [VendID] = @VendID"
            Try
                conn.Open()
                parent = conn.QuerySingleOrDefault(Of Vendors)(query, New With {.VendID = VendID})
            Catch ex As Exception
                ' Handle exceptions
            Finally
                If conn.State = ConnectionState.Open Then conn.Close()
            End Try
        End Using
        Return parent
    End Function

    Public Function GetVendorPays(ByVal clsVendorSales As VendorSales) As IEnumerable(Of VendorPays)
        Using conn As New SqlConnection(ConnectionString)
            conn.Open()
            Dim query As String = "SELECT * FROM [VendorPays] WHERE [SalesID] = @SalesID"
            Return conn.Query(Of VendorPays)(query, New With {.SalesID = clsVendorSales.SalesID})
        End Using
    End Function

End Class

Public Class UnpaidVendorSale
    Public Property SalesID As Integer
    Public Property VendID As Integer
    Public Property VendName As String
    Public Property Detail As String
    Public Property SalesDate As Date
    Public Property SalesValue As Decimal
    Public Property TotalPaid As Decimal
    Public ReadOnly Property Remaining As Decimal
        Get
            Return SalesValue - TotalPaid
        End Get
    End Property
End Class


