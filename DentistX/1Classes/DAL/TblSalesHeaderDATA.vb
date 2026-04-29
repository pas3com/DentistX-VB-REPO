Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections
Imports System.Configuration
Imports Dapper

	Public Class TblSalesHeaderDATA

		Private ConnectionString As String=DentistXDATA.GetConnection.connectionString



		Public Function SelectAll() As IEnumerable(Of TblSalesHeader)
			Using conn As New SqlConnection(ConnectionString)
			    conn.Open()
			    Return conn.Query(Of TblSalesHeader)("SELECT * FROM TblSalesHeader")
			End Using
		End Function
		

		Public Function Select_Record(ByVal clsTblSalesHeader As TblSalesHeader) As TblSalesHeader
			Using conn As New SqlConnection(ConnectionString)
			    Dim sql As String = "Select * FROM TblSalesHeader WHERE SaleID = @SaleID"
			    Return conn.QuerySingleOrDefault(Of TblSalesHeader)(sql, New With { .SaleID = clsTblSalesHeader.SaleID })
			End Using
		End Function

		Public Function Add(ByVal clsTblSalesHeader As TblSalesHeader) As Boolean
			Dim RowsAffected As Integer=0
			Using conn As New SqlConnection(ConnectionString)
			    Dim sql As String = "INSERT INTO TblSalesHeader (SaleType, SaleDate, CusID, DocNo, SaleEx, Hasm) VALUES (@SaleType, @SaleDate, @CusID, @DocNo, @SaleEx, @Hasm)" 
			    RowsAffected = conn.Execute(sql, New With { .SaleType =  clsTblSalesHeader.SaleType, .SaleDate =  clsTblSalesHeader.SaleDate, .CusID =  clsTblSalesHeader.CusID, .DocNo =  clsTblSalesHeader.DocNo, .SaleEx =  clsTblSalesHeader.SaleEx, .Hasm =  clsTblSalesHeader.Hasm })
			    Return RowsAffected > 0
			End Using
		End Function

		Public Function Update(oldTblSalesHeader As TblSalesHeader, newTblSalesHeader As TblSalesHeader) As Boolean
			Using conn As New SqlConnection(ConnectionString)
			    Dim parameters = New With { 
					.NewSaleType = newTblSalesHeader.SaleType, .OldSaleType = oldTblSalesHeader.SaleType, .NewSaleDate = newTblSalesHeader.SaleDate, .OldSaleDate = oldTblSalesHeader.SaleDate, .NewCusID = newTblSalesHeader.CusID, .OldCusID = oldTblSalesHeader.CusID, .NewDocNo = newTblSalesHeader.DocNo, .OldDocNo = oldTblSalesHeader.DocNo, .NewSaleEx = newTblSalesHeader.SaleEx, .OldSaleEx = oldTblSalesHeader.SaleEx, .NewHasm = newTblSalesHeader.Hasm, .OldHasm = oldTblSalesHeader.Hasm
										  }
			    Dim affectedRows As Integer = conn.Execute("UPDATE [TblSalesHeader] SET [SaleType] = @NewSaleType, [SaleDate] = @NewSaleDate, [CusID] = @NewCusID, [DocNo] = @NewDocNo, [SaleEx] = @NewSaleEx, [Hasm] = @NewHasm WHERE [SaleType] = @OldSaleType AND [SaleDate] = @OldSaleDate AND [CusID] = @OldCusID AND [DocNo] = @OldDocNo AND [SaleEx] = @OldSaleEx AND [Hasm] = @OldHasm", parameters)
			    Return affectedRows > 0
			End Using
		End Function

		Public Function Delete(ByVal clsTblSalesHeader As TblSalesHeader) As Boolean
			Dim deleteStatement As String =
			"DELETE FROM [TblSalesHeader] 
			WHERE SaleID = @SaleID"
			Using connection As SqlConnection = DentistXData.GetConnection()
			    connection.Open()
			    Dim affectedRows As Integer = connection.Execute(deleteStatement, New With { .SaleID = clsTblSalesHeader.SaleID })
			    Return affectedRows > 0
			End Using
		End Function


'Methods to get parents and childs
		Public Function GetTblCustomers(ByVal CusID As Integer) As TblCustomers
		Dim parent As TblCustomers = Nothing
		Using conn As New SqlConnection(ConnectionString)
			Dim query As String = "SELECT * FROM [TblCustomers] WHERE [CusID] = @CusID"
			Try
				conn.Open()
				parent = conn.QuerySingleOrDefault(Of TblCustomers)(query, New With {.CusID = CusID})
			Catch ex As Exception
				' Handle exceptions
			Finally
				If conn.State = ConnectionState.Open Then conn.Close()
			End Try
		End Using
		Return parent
		End Function

		Public Function GetTblSalesBody(ByVal clsTblSalesHeader As TblSalesHeader ) As IEnumerable(Of TblSalesBody)
			Using conn As New SqlConnection(ConnectionString)
				conn.Open()
				Dim query As String = "SELECT * FROM [TblSalesBody] WHERE [SaleID] = @SaleID"
				Return conn.Query(Of TblSalesBody)(query, New With { .SaleID= clsTblSalesHeader.SaleID })
			End Using
		End Function

	End Class
