Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections
Imports System.Configuration
Imports Dapper

	Public Class TblInvoicesHeaderDATA

		Private ConnectionString As String=DentistXDATA.GetConnection.connectionString



		Public Function SelectAll() As IEnumerable(Of TblInvoicesHeader)
			Using conn As New SqlConnection(ConnectionString)
			    conn.Open()
			    Return conn.Query(Of TblInvoicesHeader)("SELECT * FROM TblInvoicesHeader")
			End Using
		End Function
		

		Public Function Select_Record(ByVal clsTblInvoicesHeader As TblInvoicesHeader) As TblInvoicesHeader
			Using conn As New SqlConnection(ConnectionString)
			    Dim sql As String = "Select * FROM TblInvoicesHeader WHERE InvoiceID = @InvoiceID"
			    Return conn.QuerySingleOrDefault(Of TblInvoicesHeader)(sql, New With { .InvoiceID = clsTblInvoicesHeader.InvoiceID })
			End Using
		End Function

		Public Function Add(ByVal clsTblInvoicesHeader As TblInvoicesHeader) As Boolean
			Dim RowsAffected As Integer=0
			Using conn As New SqlConnection(ConnectionString)
			    Dim sql As String = "INSERT INTO TblInvoicesHeader (InvoiceType, InvoiceDate, ResID, DocNo, InvoiceEx, Hasm, InvTotlQuantItms, InvTotlPriceItms, InvTotlDiscItms, InvTotlDisc, InvoiceNet) VALUES (@InvoiceType, @InvoiceDate, @ResID, @DocNo, @InvoiceEx, @Hasm, @InvTotlQuantItms, @InvTotlPriceItms, @InvTotlDiscItms, @InvTotlDisc, @InvoiceNet)" 
			    RowsAffected = conn.Execute(sql, New With { .InvoiceType =  clsTblInvoicesHeader.InvoiceType, .InvoiceDate =  clsTblInvoicesHeader.InvoiceDate, .ResID =  clsTblInvoicesHeader.ResID, .DocNo =  clsTblInvoicesHeader.DocNo, .InvoiceEx =  clsTblInvoicesHeader.InvoiceEx, .Hasm =  clsTblInvoicesHeader.Hasm, .InvTotlQuantItms =  clsTblInvoicesHeader.InvTotlQuantItms, .InvTotlPriceItms =  clsTblInvoicesHeader.InvTotlPriceItms, .InvTotlDiscItms =  clsTblInvoicesHeader.InvTotlDiscItms, .InvTotlDisc =  clsTblInvoicesHeader.InvTotlDisc, .InvoiceNet =  clsTblInvoicesHeader.InvoiceNet })
			    Return RowsAffected > 0
			End Using
		End Function

		Public Function Update(oldTblInvoicesHeader As TblInvoicesHeader, newTblInvoicesHeader As TblInvoicesHeader) As Boolean
			Using conn As New SqlConnection(ConnectionString)
			    Dim parameters = New With { 
					.NewInvoiceType = newTblInvoicesHeader.InvoiceType, .OldInvoiceType = oldTblInvoicesHeader.InvoiceType, .NewInvoiceDate = newTblInvoicesHeader.InvoiceDate, .OldInvoiceDate = oldTblInvoicesHeader.InvoiceDate, .NewResID = newTblInvoicesHeader.ResID, .OldResID = oldTblInvoicesHeader.ResID, .NewDocNo = newTblInvoicesHeader.DocNo, .OldDocNo = oldTblInvoicesHeader.DocNo, .NewInvoiceEx = newTblInvoicesHeader.InvoiceEx, .OldInvoiceEx = oldTblInvoicesHeader.InvoiceEx, .NewHasm = newTblInvoicesHeader.Hasm, .OldHasm = oldTblInvoicesHeader.Hasm, .NewInvTotlQuantItms = newTblInvoicesHeader.InvTotlQuantItms, .OldInvTotlQuantItms = oldTblInvoicesHeader.InvTotlQuantItms, .NewInvTotlPriceItms = newTblInvoicesHeader.InvTotlPriceItms, .OldInvTotlPriceItms = oldTblInvoicesHeader.InvTotlPriceItms, .NewInvTotlDiscItms = newTblInvoicesHeader.InvTotlDiscItms, .OldInvTotlDiscItms = oldTblInvoicesHeader.InvTotlDiscItms, .NewInvTotlDisc = newTblInvoicesHeader.InvTotlDisc, .OldInvTotlDisc = oldTblInvoicesHeader.InvTotlDisc, .NewInvoiceNet = newTblInvoicesHeader.InvoiceNet, .OldInvoiceNet = oldTblInvoicesHeader.InvoiceNet
										  }
			    Dim affectedRows As Integer = conn.Execute("UPDATE [TblInvoicesHeader] SET [InvoiceType] = @NewInvoiceType, [InvoiceDate] = @NewInvoiceDate, [ResID] = @NewResID, [DocNo] = @NewDocNo, [InvoiceEx] = @NewInvoiceEx, [Hasm] = @NewHasm, [InvTotlQuantItms] = @NewInvTotlQuantItms, [InvTotlPriceItms] = @NewInvTotlPriceItms, [InvTotlDiscItms] = @NewInvTotlDiscItms, [InvTotlDisc] = @NewInvTotlDisc, [InvoiceNet] = @NewInvoiceNet WHERE [InvoiceType] = @OldInvoiceType AND [InvoiceDate] = @OldInvoiceDate AND [ResID] = @OldResID AND [DocNo] = @OldDocNo AND [InvoiceEx] = @OldInvoiceEx AND [Hasm] = @OldHasm AND [InvTotlQuantItms] = @OldInvTotlQuantItms AND [InvTotlPriceItms] = @OldInvTotlPriceItms AND [InvTotlDiscItms] = @OldInvTotlDiscItms AND [InvTotlDisc] = @OldInvTotlDisc AND [InvoiceNet] = @OldInvoiceNet", parameters)
			    Return affectedRows > 0
			End Using
		End Function

		Public Function Delete(ByVal clsTblInvoicesHeader As TblInvoicesHeader) As Boolean
			Dim deleteStatement As String =
			"DELETE FROM [TblInvoicesHeader] 
			WHERE InvoiceID = @InvoiceID"
			Using connection As SqlConnection = DentistXData.GetConnection()
			    connection.Open()
			    Dim affectedRows As Integer = connection.Execute(deleteStatement, New With { .InvoiceID = clsTblInvoicesHeader.InvoiceID })
			    Return affectedRows > 0
			End Using
		End Function


'Methods to get parents and childs
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

		Public Function GetTblInvoiceBody(ByVal clsTblInvoicesHeader As TblInvoicesHeader ) As IEnumerable(Of TblInvoiceBody)
			Using conn As New SqlConnection(ConnectionString)
				conn.Open()
				Dim query As String = "SELECT * FROM [TblInvoiceBody] WHERE [InvID] = @InvID"
				Return conn.Query(Of TblInvoiceBody)(query, New With { .InvID= clsTblInvoicesHeader.InvoiceID })
			End Using
		End Function

		Public Function GetTblInvPay(ByVal clsTblInvoicesHeader As TblInvoicesHeader ) As IEnumerable(Of TblInvPay)
			Using conn As New SqlConnection(ConnectionString)
				conn.Open()
				Dim query As String = "SELECT * FROM [TblInvPay] WHERE [InvoiceID] = @InvoiceID"
				Return conn.Query(Of TblInvPay)(query, New With { .InvoiceID= clsTblInvoicesHeader.InvoiceID })
			End Using
		End Function

	End Class
