Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections
Imports System.Configuration
Imports Dapper

	Public Class LabPayDATA

		Private ConnectionString As String=DentistXDATA.GetConnection.connectionString



		Public Function SelectAll() As IEnumerable(Of LabPay)
			Using conn As New SqlConnection(ConnectionString)
			conn.Open()
			Return conn.Query(Of LabPay)("SELECT [dbo].[LabPay].[LabPayID]
										  ,[dbo].[Lab].[LabName]
										  ,[dbo].[LabPay].[LabID]
										  ,[dbo].[LabPay].[LabOrderID]
										  ,[dbo].[LabOrder].[OrderDetails]
										  ,[dbo].[LabPay].[PayValue]
										  ,[dbo].[LabPay].[PayDate]
										  ,[dbo].[LabPay].[PayDetail]
										  ,[dbo].[LabPay].[Notes]
									  FROM [dbo].[LabPay]
									  INNER JOIN [dbo].[Lab] ON [dbo].[LabPay].[LabID] = [dbo].[Lab].[LabID]
									  INNER JOIN [dbo].[LabOrder] ON [dbo].[LabPay].[LabOrderID] = [dbo].[LabOrder].[LabOrderID]
									")
		End Using
	End Function

		Public Function SelectByPayDateRange(dFrom As Date, dTo As Date) As IEnumerable(Of LabPay)
			Using conn As New SqlConnection(ConnectionString)
				conn.Open()
				Dim sql As String = "SELECT [dbo].[LabPay].[LabPayID]
										  ,[dbo].[Lab].[LabName]
										  ,[dbo].[LabPay].[LabID]
										  ,[dbo].[LabPay].[LabOrderID]
										  ,[dbo].[LabOrder].[OrderDetails]
										  ,[dbo].[LabPay].[PayValue]
										  ,[dbo].[LabPay].[PayDate]
										  ,[dbo].[LabPay].[PayDetail]
										  ,[dbo].[LabPay].[Notes]
									  FROM [dbo].[LabPay]
									  INNER JOIN [dbo].[Lab] ON [dbo].[LabPay].[LabID] = [dbo].[Lab].[LabID]
									  INNER JOIN [dbo].[LabOrder] ON [dbo].[LabPay].[LabOrderID] = [dbo].[LabOrder].[LabOrderID]
									  WHERE CAST([dbo].[LabPay].[PayDate] AS DATE) >= @dFrom AND CAST([dbo].[LabPay].[PayDate] AS DATE) <= @dTo
									  ORDER BY [dbo].[LabPay].[PayDate] DESC"
				Return conn.Query(Of LabPay)(sql, New With {.dFrom = dFrom.Date, .dTo = dTo.Date})
			End Using
		End Function
		

		Public Function Select_Record(ByVal clsLabPay As LabPay) As LabPay
			Using conn As New SqlConnection(ConnectionString)
			    Dim sql As String = "Select * FROM LabPay WHERE LabPayID = @LabPayID"
			    Return conn.QuerySingleOrDefault(Of LabPay)(sql, New With { .LabPayID = clsLabPay.LabPayID })
			End Using
		End Function

		Public Function Add(ByVal clsLabPay As LabPay) As Boolean
			Dim RowsAffected As Integer=0
			Using conn As New SqlConnection(ConnectionString)
			    Dim sql As String = "INSERT INTO LabPay (LabID, LabOrderID, PayValue, PayDate, PayDetail, Notes) VALUES (@LabID, @LabOrderID, @PayValue, @PayDate, @PayDetail, @Notes)" 
			    RowsAffected = conn.Execute(sql, New With { .LabID =  clsLabPay.LabID, .LabOrderID =  clsLabPay.LabOrderID, .PayValue =  clsLabPay.PayValue, .PayDate =  clsLabPay.PayDate, .PayDetail =  clsLabPay.PayDetail, .Notes =  clsLabPay.Notes })
			    Return RowsAffected > 0
			End Using
		End Function

		Public Function Update(oldLabPay As LabPay, newLabPay As LabPay) As Boolean
			Using conn As New SqlConnection(ConnectionString)
				Dim sql As String = "UPDATE [dbo].[LabPay] SET [LabID] = @LabID, [LabOrderID] = @LabOrderID, [PayValue] = @PayValue, [PayDate] = @PayDate, [PayDetail] = @PayDetail, [Notes] = @Notes WHERE [LabPayID] = @LabPayID"
				Dim affectedRows As Integer = conn.Execute(sql, New With {
					.LabPayID = newLabPay.LabPayID,
					.LabID = newLabPay.LabID,
					.LabOrderID = newLabPay.LabOrderID,
					.PayValue = newLabPay.PayValue,
					.PayDate = newLabPay.PayDate,
					.PayDetail = newLabPay.PayDetail,
					.Notes = newLabPay.Notes
				})
				Return affectedRows > 0
			End Using
		End Function

		Public Function Delete(ByVal clsLabPay As LabPay) As Boolean
			Dim deleteStatement As String =
			"DELETE FROM [LabPay] 
			WHERE LabPayID = @LabPayID"
			Using connection As SqlConnection = DentistXData.GetConnection()
			    connection.Open()
			    Dim affectedRows As Integer = connection.Execute(deleteStatement, New With { .LabPayID = clsLabPay.LabPayID })
			    Return affectedRows > 0
			End Using
		End Function


'Methods to get parents and childs
		Public Function GetLab(ByVal LabID As Integer) As Lab
		Dim parent As Lab = Nothing
		Using conn As New SqlConnection(ConnectionString)
			Dim query As String = "SELECT * FROM [Lab] WHERE [LabID] = @LabID"
			Try
				conn.Open()
				parent = conn.QuerySingleOrDefault(Of Lab)(query, New With {.LabID = LabID})
			Catch ex As Exception
				' Handle exceptions
			Finally
				If conn.State = ConnectionState.Open Then conn.Close()
			End Try
		End Using
		Return parent
		End Function

	End Class
