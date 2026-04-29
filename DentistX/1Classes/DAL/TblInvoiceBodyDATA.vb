Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections
Imports System.Configuration
Imports Dapper

	Public Class TblInvoiceBodyDATA

		Private ConnectionString As String=DentistXDATA.GetConnection.connectionString



		Public Function SelectAll() As IEnumerable(Of TblInvoiceBody)
			Using conn As New SqlConnection(ConnectionString)
			    conn.Open()
			    Return conn.Query(Of TblInvoiceBody)("SELECT * FROM TblInvoiceBody")
			End Using
		End Function
		

		Public Function Select_Record(ByVal clsTblInvoiceBody As TblInvoiceBody) As TblInvoiceBody
			Using conn As New SqlConnection(ConnectionString)
			    Dim sql As String = "Select * FROM TblInvoiceBody WHERE InvID = @InvID And ItemID = @ItemID"
			    Return conn.QuerySingleOrDefault(Of TblInvoiceBody)(sql, New With { .InvID = clsTblInvoiceBody.InvID, .ItemID = clsTblInvoiceBody.ItemID })
			End Using
		End Function

		Public Function Add(ByVal clsTblInvoiceBody As TblInvoiceBody) As Boolean
			Dim RowsAffected As Integer=0
			Using conn As New SqlConnection(ConnectionString)
			    Dim sql As String = "INSERT INTO TblInvoiceBody (InvID, ItemID, Quantity, Price, ItemHasm, Note, ItemNet, BdyNet) VALUES (@InvID, @ItemID, @Quantity, @Price, @ItemHasm, @Note, @ItemNet, @BdyNet)" 
			    RowsAffected = conn.Execute(sql, New With { .InvID =  clsTblInvoiceBody.InvID, .ItemID =  clsTblInvoiceBody.ItemID, .Quantity =  clsTblInvoiceBody.Quantity, .Price =  clsTblInvoiceBody.Price, .ItemHasm =  clsTblInvoiceBody.ItemHasm, .Note =  clsTblInvoiceBody.Note, .ItemNet =  clsTblInvoiceBody.ItemNet, .BdyNet =  clsTblInvoiceBody.BdyNet })
			    Return RowsAffected > 0
			End Using
		End Function

		Public Function Update(oldTblInvoiceBody As TblInvoiceBody, newTblInvoiceBody As TblInvoiceBody) As Boolean
			Using conn As New SqlConnection(ConnectionString)
			    Dim parameters = New With { 
					.NewInvID = newTblInvoiceBody.InvID, .OldInvID = oldTblInvoiceBody.InvID, .NewItemID = newTblInvoiceBody.ItemID, .OldItemID = oldTblInvoiceBody.ItemID, .NewQuantity = newTblInvoiceBody.Quantity, .OldQuantity = oldTblInvoiceBody.Quantity, .NewPrice = newTblInvoiceBody.Price, .OldPrice = oldTblInvoiceBody.Price, .NewItemHasm = newTblInvoiceBody.ItemHasm, .OldItemHasm = oldTblInvoiceBody.ItemHasm, .NewNote = newTblInvoiceBody.Note, .OldNote = oldTblInvoiceBody.Note, .NewItemNet = newTblInvoiceBody.ItemNet, .OldItemNet = oldTblInvoiceBody.ItemNet, .NewBdyNet = newTblInvoiceBody.BdyNet, .OldBdyNet = oldTblInvoiceBody.BdyNet
										  }
			    Dim affectedRows As Integer = conn.Execute("UPDATE [TblInvoiceBody] SET [InvID] = @NewInvID, [ItemID] = @NewItemID, [Quantity] = @NewQuantity, [Price] = @NewPrice, [ItemHasm] = @NewItemHasm, [Note] = @NewNote, [ItemNet] = @NewItemNet, [BdyNet] = @NewBdyNet WHERE [InvID] = @OldInvID AND [ItemID] = @OldItemID AND [Quantity] = @OldQuantity AND [Price] = @OldPrice AND [ItemHasm] = @OldItemHasm AND [Note] = @OldNote AND [ItemNet] = @OldItemNet AND [BdyNet] = @OldBdyNet", parameters)
			    Return affectedRows > 0
			End Using
		End Function

		Public Function Delete(ByVal clsTblInvoiceBody As TblInvoiceBody) As Boolean
			Dim deleteStatement As String =
			"DELETE FROM [TblInvoiceBody] 
			WHERE InvID = @InvID AND ItemID = @ItemID"
			Using connection As SqlConnection = DentistXData.GetConnection()
			    connection.Open()
			    Dim affectedRows As Integer = connection.Execute(deleteStatement, New With { .InvID = clsTblInvoiceBody.InvID, .ItemID = clsTblInvoiceBody.ItemID })
			    Return affectedRows > 0
			End Using
		End Function


'Methods to get parents and childs
		Public Function GetTblInvoicesHeader(ByVal InvoiceID As Integer) As TblInvoicesHeader
		Dim parent As TblInvoicesHeader = Nothing
		Using conn As New SqlConnection(ConnectionString)
			Dim query As String = "SELECT * FROM [TblInvoicesHeader] WHERE [InvoiceID] = @InvoiceID"
			Try
				conn.Open()
				parent = conn.QuerySingleOrDefault(Of TblInvoicesHeader)(query, New With {.InvoiceID = InvoiceID})
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
