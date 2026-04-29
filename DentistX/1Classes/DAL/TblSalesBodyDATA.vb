Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections
Imports System.Configuration
Imports Dapper

	Public Class TblSalesBodyDATA

		Private ConnectionString As String=DentistXDATA.GetConnection.connectionString



		Public Function SelectAll() As IEnumerable(Of TblSalesBody)
			Using conn As New SqlConnection(ConnectionString)
			    conn.Open()
			    Return conn.Query(Of TblSalesBody)("SELECT * FROM TblSalesBody")
			End Using
		End Function
		

		Public Function Select_Record(ByVal clsTblSalesBody As TblSalesBody) As TblSalesBody
			Using conn As New SqlConnection(ConnectionString)
			    Dim sql As String = "Select * FROM TblSalesBody WHERE SaleID = @SaleID And ItemID = @ItemID"
			    Return conn.QuerySingleOrDefault(Of TblSalesBody)(sql, New With { .SaleID = clsTblSalesBody.SaleID, .ItemID = clsTblSalesBody.ItemID })
			End Using
		End Function

		Public Function Add(ByVal clsTblSalesBody As TblSalesBody) As Boolean
			Dim RowsAffected As Integer=0
			Using conn As New SqlConnection(ConnectionString)
			    Dim sql As String = "INSERT INTO TblSalesBody (SaleID, ItemID, Quantity, Price, ItemHasm, Note) VALUES (@SaleID, @ItemID, @Quantity, @Price, @ItemHasm, @Note)" 
			    RowsAffected = conn.Execute(sql, New With { .SaleID =  clsTblSalesBody.SaleID, .ItemID =  clsTblSalesBody.ItemID, .Quantity =  clsTblSalesBody.Quantity, .Price =  clsTblSalesBody.Price, .ItemHasm =  clsTblSalesBody.ItemHasm, .Note =  clsTblSalesBody.Note })
			    Return RowsAffected > 0
			End Using
		End Function

		Public Function Update(oldTblSalesBody As TblSalesBody, newTblSalesBody As TblSalesBody) As Boolean
			Using conn As New SqlConnection(ConnectionString)
			    Dim parameters = New With { 
					.NewSaleID = newTblSalesBody.SaleID, .OldSaleID = oldTblSalesBody.SaleID, .NewItemID = newTblSalesBody.ItemID, .OldItemID = oldTblSalesBody.ItemID, .NewQuantity = newTblSalesBody.Quantity, .OldQuantity = oldTblSalesBody.Quantity, .NewPrice = newTblSalesBody.Price, .OldPrice = oldTblSalesBody.Price, .NewItemHasm = newTblSalesBody.ItemHasm, .OldItemHasm = oldTblSalesBody.ItemHasm, .NewNote = newTblSalesBody.Note, .OldNote = oldTblSalesBody.Note
										  }
			    Dim affectedRows As Integer = conn.Execute("UPDATE [TblSalesBody] SET [SaleID] = @NewSaleID, [ItemID] = @NewItemID, [Quantity] = @NewQuantity, [Price] = @NewPrice, [ItemHasm] = @NewItemHasm, [Note] = @NewNote WHERE [SaleID] = @OldSaleID AND [ItemID] = @OldItemID AND [Quantity] = @OldQuantity AND [Price] = @OldPrice AND [ItemHasm] = @OldItemHasm AND [Note] = @OldNote", parameters)
			    Return affectedRows > 0
			End Using
		End Function

		Public Function Delete(ByVal clsTblSalesBody As TblSalesBody) As Boolean
			Dim deleteStatement As String =
			"DELETE FROM [TblSalesBody] 
			WHERE SaleID = @SaleID AND ItemID = @ItemID"
			Using connection As SqlConnection = DentistXData.GetConnection()
			    connection.Open()
			    Dim affectedRows As Integer = connection.Execute(deleteStatement, New With { .SaleID = clsTblSalesBody.SaleID, .ItemID = clsTblSalesBody.ItemID })
			    Return affectedRows > 0
			End Using
		End Function


'Methods to get parents and childs
		Public Function GetTblSalesHeader(ByVal SaleID As Integer) As TblSalesHeader
		Dim parent As TblSalesHeader = Nothing
		Using conn As New SqlConnection(ConnectionString)
			Dim query As String = "SELECT * FROM [TblSalesHeader] WHERE [SaleID] = @SaleID"
			Try
				conn.Open()
				parent = conn.QuerySingleOrDefault(Of TblSalesHeader)(query, New With {.SaleID = SaleID})
			Catch ex As Exception
				' Handle exceptions
			Finally
				If conn.State = ConnectionState.Open Then conn.Close()
			End Try
		End Using
		Return parent
		End Function

		Public Function GetTblItems(ByVal ItemID As Integer) As TblItems
		Dim parent As TblItems = Nothing
		Using conn As New SqlConnection(ConnectionString)
			Dim query As String = "SELECT * FROM [TblItems] WHERE [ItemID] = @ItemID"
			Try
				conn.Open()
				parent = conn.QuerySingleOrDefault(Of TblItems)(query, New With {.ItemID = ItemID})
			Catch ex As Exception
				' Handle exceptions
			Finally
				If conn.State = ConnectionState.Open Then conn.Close()
			End Try
		End Using
		Return parent
		End Function

	End Class
