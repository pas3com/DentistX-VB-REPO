Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections
Imports System.Configuration
Imports Dapper

	Public Class VendorPaysDATA

		Private ConnectionString As String=DentistXDATA.GetConnection.connectionString



		Public Function SelectAll() As IEnumerable(Of VendorPays)
			Using conn As New SqlConnection(ConnectionString)
			    conn.Open()
			    Return conn.Query(Of VendorPays)("SELECT * FROM VendorPays")
			End Using
		End Function
		

		Public Function Select_Record(ByVal clsVendorPays As VendorPays) As VendorPays
			Using conn As New SqlConnection(ConnectionString)
			    Dim sql As String = "Select * FROM VendorPays WHERE PayID = @PayID"
			    Return conn.QuerySingleOrDefault(Of VendorPays)(sql, New With { .PayID = clsVendorPays.PayID })
			End Using
		End Function

		Public Function Add(ByVal clsVendorPays As VendorPays) As Boolean
			Dim RowsAffected As Integer=0
			Using conn As New SqlConnection(ConnectionString)
			    Dim sql As String = "INSERT INTO VendorPays (SalesID, VendID, PayValue, PayDate, Notes, PayType, ChqOwner, AccountNumber, ChqNumber, ChqDueDate, ChqBank, IsCashed, IsForward) VALUES (@SalesID, @VendID, @PayValue, @PayDate, @Notes, @PayType, @ChqOwner, @AccountNumber, @ChqNumber, @ChqDueDate, @ChqBank, @IsCashed, @IsForward)" 
			    RowsAffected = conn.Execute(sql, New With { .SalesID =  clsVendorPays.SalesID, .VendID =  clsVendorPays.VendID, .PayValue =  clsVendorPays.PayValue, .PayDate =  clsVendorPays.PayDate, .Notes =  clsVendorPays.Notes, .PayType =  clsVendorPays.PayType, .ChqOwner =  clsVendorPays.ChqOwner, .AccountNumber =  clsVendorPays.AccountNumber, .ChqNumber =  clsVendorPays.ChqNumber, .ChqDueDate =  clsVendorPays.ChqDueDate, .ChqBank =  clsVendorPays.ChqBank, .IsCashed =  clsVendorPays.IsCashed, .IsForward =  clsVendorPays.ForwardFromTo })
			    Return RowsAffected > 0
			End Using
		End Function

    Public Function Add(pay As VendorPays, conn As SqlConnection, trans As SqlTransaction) As Boolean
        Dim sql As String = "INSERT INTO VendorPays (SalesID, VendID, PayValue, PayDate, Notes, PayType, ChqOwner, AccountNumber, ChqNumber, ChqDueDate, ChqBank, IsCashed, IsForward) VALUES (@SalesID, @VendID, @PayValue, @PayDate, @Notes, @PayType, @ChqOwner, @AccountNumber, @ChqNumber, @ChqDueDate, @ChqBank, @IsCashed, @IsForward)"
        Return conn.Execute(sql, pay, transaction:=trans) > 0
    End Function
    Public Function Update(oldVendorPays As VendorPays, newVendorPays As VendorPays) As Boolean
			Using conn As New SqlConnection(ConnectionString)
			    Dim parameters = New With { 
					.NewSalesID = newVendorPays.SalesID, .OldSalesID = oldVendorPays.SalesID, .NewVendID = newVendorPays.VendID, .OldVendID = oldVendorPays.VendID, .NewPayValue = newVendorPays.PayValue, .OldPayValue = oldVendorPays.PayValue, .NewPayDate = newVendorPays.PayDate, .OldPayDate = oldVendorPays.PayDate, .NewNotes = newVendorPays.Notes, .OldNotes = oldVendorPays.Notes, .NewPayType = newVendorPays.PayType, .OldPayType = oldVendorPays.PayType, .NewChqOwner = newVendorPays.ChqOwner, .OldChqOwner = oldVendorPays.ChqOwner, .NewAccountNumber = newVendorPays.AccountNumber, .OldAccountNumber = oldVendorPays.AccountNumber, .NewChqNumber = newVendorPays.ChqNumber, .OldChqNumber = oldVendorPays.ChqNumber, .NewChqDueDate = newVendorPays.ChqDueDate, .OldChqDueDate = oldVendorPays.ChqDueDate, .NewChqBank = newVendorPays.ChqBank, .OldChqBank = oldVendorPays.ChqBank, .NewIsCashed = newVendorPays.IsCashed, .OldIsCashed = oldVendorPays.IsCashed, .NewIsForward = newVendorPays.ForwardFromTo, .OldIsForward = oldVendorPays.ForwardFromTo
										  }
			    Dim affectedRows As Integer = conn.Execute("UPDATE [VendorPays] SET [SalesID] = @NewSalesID, [VendID] = @NewVendID, [PayValue] = @NewPayValue, [PayDate] = @NewPayDate, [Notes] = @NewNotes, [PayType] = @NewPayType, [ChqOwner] = @NewChqOwner, [AccountNumber] = @NewAccountNumber, [ChqNumber] = @NewChqNumber, [ChqDueDate] = @NewChqDueDate, [ChqBank] = @NewChqBank, [IsCashed] = @NewIsCashed, [IsForward] = @NewIsForward WHERE [SalesID] = @OldSalesID AND [VendID] = @OldVendID AND [PayValue] = @OldPayValue AND [PayDate] = @OldPayDate AND [Notes] = @OldNotes AND [PayType] = @OldPayType AND [ChqOwner] = @OldChqOwner AND [AccountNumber] = @OldAccountNumber AND [ChqNumber] = @OldChqNumber AND [ChqDueDate] = @OldChqDueDate AND [ChqBank] = @OldChqBank AND [IsCashed] = @OldIsCashed AND [IsForward] = @OldIsForward", parameters)
			    Return affectedRows > 0
			End Using
		End Function

		Public Function Delete(ByVal clsVendorPays As VendorPays) As Boolean
			Dim deleteStatement As String =
			"DELETE FROM [VendorPays] 
			WHERE PayID = @PayID"
			Using connection As SqlConnection = DentistXData.GetConnection()
			    connection.Open()
			    Dim affectedRows As Integer = connection.Execute(deleteStatement, New With { .PayID = clsVendorPays.PayID })
			    Return affectedRows > 0
			End Using
		End Function


'Methods to get parents and childs
		Public Function GetVendorSales(ByVal SalesID As Integer) As VendorSales
		Dim parent As VendorSales = Nothing
		Using conn As New SqlConnection(ConnectionString)
			Dim query As String = "SELECT * FROM [VendorSales] WHERE [SalesID] = @SalesID"
			Try
				conn.Open()
				parent = conn.QuerySingleOrDefault(Of VendorSales)(query, New With {.SalesID = SalesID})
			Catch ex As Exception
				' Handle exceptions
			Finally
				If conn.State = ConnectionState.Open Then conn.Close()
			End Try
		End Using
		Return parent
		End Function

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

	End Class
