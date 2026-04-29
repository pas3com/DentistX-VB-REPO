Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections
Imports System.Configuration
Imports Dapper

	Public Class TblInvPayDATA

		Private ConnectionString As String=DentistXDATA.GetConnection.connectionString



		Public Function SelectAll() As IEnumerable(Of TblInvPay)
			Using conn As New SqlConnection(ConnectionString)
			    conn.Open()
			    Return conn.Query(Of TblInvPay)("SELECT * FROM TblInvPay")
			End Using
		End Function
		

		Public Function Select_Record(ByVal clsTblInvPay As TblInvPay) As TblInvPay
			Using conn As New SqlConnection(ConnectionString)
			    Dim sql As String = "Select * FROM TblInvPay WHERE PayID = @PayID"
			    Return conn.QuerySingleOrDefault(Of TblInvPay)(sql, New With { .PayID = clsTblInvPay.PayID })
			End Using
		End Function

		Public Function Add(ByVal clsTblInvPay As TblInvPay) As Boolean
			Dim RowsAffected As Integer=0
			Using conn As New SqlConnection(ConnectionString)
			    Dim sql As String = "INSERT INTO TblInvPay (InvoiceID, ResID, PayDate, Amount, Notes, InvRemain) VALUES (@InvoiceID, @ResID, @PayDate, @Amount, @Notes, @InvRemain)" 
			    RowsAffected = conn.Execute(sql, New With { .InvoiceID =  clsTblInvPay.InvoiceID, .ResID =  clsTblInvPay.ResID, .PayDate =  clsTblInvPay.PayDate, .Amount =  clsTblInvPay.Amount, .Notes =  clsTblInvPay.Notes, .InvRemain =  clsTblInvPay.InvRemain })
			    Return RowsAffected > 0
			End Using
		End Function

		Public Function Update(oldTblInvPay As TblInvPay, newTblInvPay As TblInvPay) As Boolean
			Using conn As New SqlConnection(ConnectionString)
			    Dim parameters = New With { 
					.NewInvoiceID = newTblInvPay.InvoiceID, .OldInvoiceID = oldTblInvPay.InvoiceID, .NewResID = newTblInvPay.ResID, .OldResID = oldTblInvPay.ResID, .NewPayDate = newTblInvPay.PayDate, .OldPayDate = oldTblInvPay.PayDate, .NewAmount = newTblInvPay.Amount, .OldAmount = oldTblInvPay.Amount, .NewNotes = newTblInvPay.Notes, .OldNotes = oldTblInvPay.Notes, .NewInvRemain = newTblInvPay.InvRemain, .OldInvRemain = oldTblInvPay.InvRemain
										  }
			    Dim affectedRows As Integer = conn.Execute("UPDATE [TblInvPay] SET [InvoiceID] = @NewInvoiceID, [ResID] = @NewResID, [PayDate] = @NewPayDate, [Amount] = @NewAmount, [Notes] = @NewNotes, [InvRemain] = @NewInvRemain WHERE [InvoiceID] = @OldInvoiceID AND [ResID] = @OldResID AND [PayDate] = @OldPayDate AND [Amount] = @OldAmount AND [Notes] = @OldNotes AND [InvRemain] = @OldInvRemain", parameters)
			    Return affectedRows > 0
			End Using
		End Function

		Public Function Delete(ByVal clsTblInvPay As TblInvPay) As Boolean
			Dim deleteStatement As String =
			"DELETE FROM [TblInvPay] 
			WHERE PayID = @PayID"
			Using connection As SqlConnection = DentistXData.GetConnection()
			    connection.Open()
			    Dim affectedRows As Integer = connection.Execute(deleteStatement, New With { .PayID = clsTblInvPay.PayID })
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

		Public Function GetTblResources(ByVal ResID As Integer) As TblResources
		Dim parent As TblResources = Nothing
		Using conn As New SqlConnection(ConnectionString)
			Dim query As String = "SELECT * FROM [TblResources] WHERE [ResID] = @ResID"
			Try
				conn.Open()
				parent = conn.QuerySingleOrDefault(Of TblResources)(query, New With {.ResID = ResID})
			Catch ex As Exception
				' Handle exceptions
			Finally
				If conn.State = ConnectionState.Open Then conn.Close()
			End Try
		End Using
		Return parent
		End Function

	End Class
