Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections
Imports System.Configuration
Imports Dapper

	Public Class LabOrderDATA

		Private ConnectionString As String=DentistXDATA.GetConnection.connectionString



		Public Function SelectAll() As IEnumerable(Of LabOrder)
			Using conn As New SqlConnection(ConnectionString)
			    conn.Open()
			Return conn.Query(Of LabOrder)("SELECT [LabOrderID]
											  ,[dbo].[LabOrder].[LabID]
											  ,[dbo].[Lab].[LabName]
											  ,[dbo].[Patient].[PatientName]
											  ,[dbo].[LabOrder].[PatientID]
											  ,[ImprType]
											  ,[ImprDet]
											  ,[ImprClr]
											  ,[ImprCount]
											  ,[DeliveryDate]
											  ,[Price]
											  ,[OrderDetails]
											  ,[RecieveDate]
											  ,[dbo].[LabOrder].[Notes]
											FROM [dbo].[LabOrder] inner join  [dbo].[Lab] on [dbo].[LabOrder].LabID= [dbo].[Lab].LabID
											inner join [dbo].[Patient] on [dbo].[Patient].[PatientID] = [dbo].[LabOrder].[PatientID] ")
		End Using
		End Function
		

		Public Function Select_Record(ByVal clsLabOrder As LabOrder) As LabOrder
			Using conn As New SqlConnection(ConnectionString)
			Dim sql As String = "SELECT [LabOrderID]
											  ,[dbo].[LabOrder].[LabID]
											  ,[dbo].[Lab].[LabName]
											  ,[dbo].[Patient].[PatientName]
											  ,[dbo].[LabOrder].[PatientID]
											  ,[ImprType]
											  ,[ImprDet]
											  ,[ImprClr]
											  ,[ImprCount]
											  ,[DeliveryDate]
											  ,[Price]
											  ,[OrderDetails]
											  ,[RecieveDate]
											  ,[dbo].[LabOrder].[Notes]
											FROM [dbo].[LabOrder] inner join  [dbo].[Lab] on [dbo].[LabOrder].LabID= [dbo].[Lab].LabID
											inner join [dbo].[Patient] on [dbo].[Patient].[PatientID] = [dbo].[LabOrder].[PatientID]
										WHERE LabOrderID = @LabOrderID"
			Return conn.QuerySingleOrDefault(Of LabOrder)(sql, New With { .LabOrderID = clsLabOrder.LabOrderID })
			End Using
		End Function

		Public Function Add(ByVal clsLabOrder As LabOrder) As Boolean
			Dim RowsAffected As Integer=0
			Using conn As New SqlConnection(ConnectionString)
			Dim sql As String = "INSERT INTO LabOrder (LabID, PatientID, ImprType, ImprDet, ImprClr, ImprCount, DeliveryDate, Price,
								OrderDetails, RecieveDate, Notes)
								VALUES (@LabID, @PatientID, @ImprType, @ImprDet, @ImprClr, @ImprCount, @DeliveryDate, @Price ,
								@OrderDetails, @RecieveDate, @Notes)"
			RowsAffected = conn.Execute(sql, New With {.LabID = clsLabOrder.LabID, .PatientID = clsLabOrder.PatientID, .ImprType = clsLabOrder.ImprType, .ImprDet = clsLabOrder.ImprDet, .ImprClr = clsLabOrder.ImprClr, .ImprCount = clsLabOrder.ImprCount, .DeliveryDate = clsLabOrder.DeliveryDate, .Price = clsLabOrder.Price, .OrderDetails = clsLabOrder.OrderDetails, .RecieveDate = clsLabOrder.RecieveDate, .Notes = clsLabOrder.Notes})
			Return RowsAffected > 0
			End Using
		End Function

		Public Function Update(oldLabOrder As LabOrder, newLabOrder As LabOrder) As Boolean
			Using conn As New SqlConnection(ConnectionString)
			Dim sql As String = "UPDATE [dbo].[LabOrder] SET [LabID] = @LabID, [PatientID] = @PatientID,
					[ImprType] = @ImprType, [ImprDet] = @ImprDet, [ImprClr] = @ImprClr,
					[ImprCount] = @ImprCount, [DeliveryDate] = @DeliveryDate, [Price] = @Price,
					[OrderDetails] = @OrderDetails, [RecieveDate] = @RecieveDate, [Notes] = @Notes
					WHERE [LabOrderID] = @LabOrderID"
			Dim parameters = New With {
					.LabOrderID = newLabOrder.LabOrderID,
					.LabID = newLabOrder.LabID,
					.PatientID = newLabOrder.PatientID,
					.ImprType = newLabOrder.ImprType,
					.ImprDet = newLabOrder.ImprDet,
					.ImprClr = newLabOrder.ImprClr,
					.ImprCount = newLabOrder.ImprCount,
					.DeliveryDate = newLabOrder.DeliveryDate,
					.Price = newLabOrder.Price,
					.OrderDetails = newLabOrder.OrderDetails,
					.RecieveDate = newLabOrder.RecieveDate,
					.Notes = newLabOrder.Notes
				}
			Dim affectedRows As Integer = conn.Execute(sql, parameters)
			Return affectedRows > 0
		End Using
		End Function

		Public Function Delete(ByVal clsLabOrder As LabOrder) As Boolean
			Dim deleteStatement As String =
			"DELETE FROM [LabOrder] 
			WHERE LabOrderID = @LabOrderID"
			Using connection As SqlConnection = DentistXData.GetConnection()
			    connection.Open()
			    Dim affectedRows As Integer = connection.Execute(deleteStatement, New With { .LabOrderID = clsLabOrder.LabOrderID })
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

		Public Function GetPatient(ByVal PatientID As Integer) As Patient
		Dim parent As Patient = Nothing
		Using conn As New SqlConnection(ConnectionString)
			Dim query As String = "SELECT * FROM [Patient] WHERE [PatientID] = @PatientID"
			Try
				conn.Open()
				parent = conn.QuerySingleOrDefault(Of Patient)(query, New With {.PatientId = PatientID})
			Catch ex As Exception
				' Handle exceptions
			Finally
				If conn.State = ConnectionState.Open Then conn.Close()
			End Try
		End Using
		Return parent
		End Function

	End Class
