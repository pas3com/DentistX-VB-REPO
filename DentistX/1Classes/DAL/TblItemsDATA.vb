Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections
Imports System.Configuration
Imports Dapper

	Public Class TblItemsDATA

		Private ConnectionString As String=DentistXDATA.GetConnection.connectionString



		Public Function SelectAll() As IEnumerable(Of TblItems)
			Using conn As New SqlConnection(ConnectionString)
			    conn.Open()
			    Return conn.Query(Of TblItems)("SELECT * FROM TblItems")
			End Using
		End Function
		

		Public Function Select_Record(ByVal clsTblItems As TblItems) As TblItems
			Using conn As New SqlConnection(ConnectionString)
			    Dim sql As String = "Select * FROM TblItems WHERE ItemID = @ItemID"
			    Return conn.QuerySingleOrDefault(Of TblItems)(sql, New With { .ItemID = clsTblItems.ItemID })
			End Using
		End Function

		Public Function Add(ByVal clsTblItems As TblItems) As Boolean
			Dim RowsAffected As Integer=0
			Using conn As New SqlConnection(ConnectionString)
			    Dim sql As String = "INSERT INTO TblItems (ItemName, ItemEx, CatID, UnitID, LastPrice, QuantityNow) VALUES (@ItemName, @ItemEx, @CatID, @UnitID, @LastPrice, @QuantityNow)" 
			    RowsAffected = conn.Execute(sql, New With { .ItemName =  clsTblItems.ItemName, .ItemEx =  clsTblItems.ItemEx, .CatID =  clsTblItems.CatID, .UnitID =  clsTblItems.UnitID, .LastPrice =  clsTblItems.LastPrice, .QuantityNow =  clsTblItems.QuantityNow })
			    Return RowsAffected > 0
			End Using
		End Function

		Public Function Update(oldTblItems As TblItems, newTblItems As TblItems) As Boolean
			Using conn As New SqlConnection(ConnectionString)
			    Dim parameters = New With { 
					.NewItemName = newTblItems.ItemName, .OldItemName = oldTblItems.ItemName, .NewItemEx = newTblItems.ItemEx, .OldItemEx = oldTblItems.ItemEx, .NewCatID = newTblItems.CatID, .OldCatID = oldTblItems.CatID, .NewUnitID = newTblItems.UnitID, .OldUnitID = oldTblItems.UnitID, .NewLastPrice = newTblItems.LastPrice, .OldLastPrice = oldTblItems.LastPrice, .NewQuantityNow = newTblItems.QuantityNow, .OldQuantityNow = oldTblItems.QuantityNow
										  }
			    Dim affectedRows As Integer = conn.Execute("UPDATE [TblItems] SET [ItemName] = @NewItemName, [ItemEx] = @NewItemEx, [CatID] = @NewCatID, [UnitID] = @NewUnitID, [LastPrice] = @NewLastPrice, [QuantityNow] = @NewQuantityNow WHERE [ItemName] = @OldItemName AND [ItemEx] = @OldItemEx AND [CatID] = @OldCatID AND [UnitID] = @OldUnitID AND [LastPrice] = @OldLastPrice AND [QuantityNow] = @OldQuantityNow", parameters)
			    Return affectedRows > 0
			End Using
		End Function

		Public Function Delete(ByVal clsTblItems As TblItems) As Boolean
			Dim deleteStatement As String =
			"DELETE FROM [TblItems] 
			WHERE ItemID = @ItemID"
			Using connection As SqlConnection = DentistXData.GetConnection()
			    connection.Open()
			    Dim affectedRows As Integer = connection.Execute(deleteStatement, New With { .ItemID = clsTblItems.ItemID })
			    Return affectedRows > 0
			End Using
		End Function


'Methods to get parents and childs
		Public Function GetTblCategories(ByVal CategoryID As Integer) As TblCategories
		Dim parent As TblCategories = Nothing
		Using conn As New SqlConnection(ConnectionString)
			Dim query As String = "SELECT * FROM [TblCategories] WHERE [CategoryID] = @CategoryID"
			Try
				conn.Open()
				parent = conn.QuerySingleOrDefault(Of TblCategories)(query, New With {.CategoryID = CategoryID})
			Catch ex As Exception
				' Handle exceptions
			Finally
				If conn.State = ConnectionState.Open Then conn.Close()
			End Try
		End Using
		Return parent
		End Function

		Public Function GetTblUnits(ByVal UnitID As Integer) As TblUnits
		Dim parent As TblUnits = Nothing
		Using conn As New SqlConnection(ConnectionString)
			Dim query As String = "SELECT * FROM [TblUnits] WHERE [UnitID] = @UnitID"
			Try
				conn.Open()
				parent = conn.QuerySingleOrDefault(Of TblUnits)(query, New With {.UnitID = UnitID})
			Catch ex As Exception
				' Handle exceptions
			Finally
				If conn.State = ConnectionState.Open Then conn.Close()
			End Try
		End Using
		Return parent
		End Function

		Public Function GetTblSalesBody(ByVal clsTblItems As TblItems ) As IEnumerable(Of TblSalesBody)
			Using conn As New SqlConnection(ConnectionString)
				conn.Open()
				Dim query As String = "SELECT * FROM [TblSalesBody] WHERE [ItemID] = @ItemID"
				Return conn.Query(Of TblSalesBody)(query, New With { .ItemID= clsTblItems.ItemID })
			End Using
		End Function

		Public Function GetTblInvoiceBody(ByVal clsTblItems As TblItems ) As IEnumerable(Of TblInvoiceBody)
			Using conn As New SqlConnection(ConnectionString)
				conn.Open()
				Dim query As String = "SELECT * FROM [TblInvoiceBody] WHERE [ItemID] = @ItemID"
				Return conn.Query(Of TblInvoiceBody)(query, New With { .ItemID= clsTblItems.ItemID })
			End Using
		End Function

	End Class
