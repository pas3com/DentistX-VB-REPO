Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections
Imports System.Configuration
Imports Dapper

	Public Class DrWorkPayDATA

		Private ConnectionString As String=DentistXDATA.GetConnection.connectionString



		Public Function SelectAll() As IEnumerable(Of DrWorkPay)
			Using conn As New SqlConnection(ConnectionString)
			    conn.Open()
			Return conn.Query(Of DrWorkPay)("SELECT [WorkPayID]
												  ,[dbo].[DrWorkPay].[WorkID]
												  ,[dbo].[DrWork].[WrkDetail]
												  ,[dbo].[DrWorkPay].[DrID]
												  ,[dbo].[Doctors].[DrID]
												  ,[PayValue]
												  ,[PayDate]
												  ,[dbo].[DrWorkPay].[Notes]
											  FROM [dbo].[DrWorkPay] inner join [dbo].[DrWork] on [dbo].[DrWorkPay].[WorkID] = [dbo].[DrWork].[WorkID]
													inner join [dbo].[Doctors] on [dbo].[DrWorkPay].[WorkID]=[dbo].[Doctors].[DrID]
											")
		End Using
		End Function
		

		Public Function Select_Record(ByVal clsDrWorkPay As DrWorkPay) As DrWorkPay
			Using conn As New SqlConnection(ConnectionString)
			Dim sql As String = "SELECT [WorkPayID]
								  ,[dbo].[DrWorkPay].[WorkID]
								  ,[dbo].[DrWork].[WrkDetail]
								  ,[dbo].[DrWorkPay].[DrID]
								  ,[dbo].[Doctors].[DrID]
								  ,[PayValue]
								  ,[PayDate]
								  ,[dbo].[DrWorkPay].[Notes]
							  FROM [dbo].[DrWorkPay] inner join [dbo].[DrWork] on [dbo].[DrWorkPay].[WorkID] = [dbo].[DrWork].[WorkID]
									inner join [dbo].[Doctors] on [dbo].[DrWorkPay].[WorkID]=[dbo].[Doctors].[DrID]
							WHERE WorkPayID = @WorkPayID"
			Return conn.QuerySingleOrDefault(Of DrWorkPay)(sql, New With { .WorkPayID = clsDrWorkPay.WorkPayID })
			End Using
		End Function

		Public Function Add(ByVal clsDrWorkPay As DrWorkPay) As Boolean
			Dim RowsAffected As Integer=0
			Using conn As New SqlConnection(ConnectionString)
			    Dim sql As String = "INSERT INTO DrWorkPay (WorkID, DrID, PayValue, PayDate, Notes) VALUES (@WorkID, @DrID, @PayValue, @PayDate, @Notes)" 
			    RowsAffected = conn.Execute(sql, New With { .WorkID =  clsDrWorkPay.WorkID, .DrID =  clsDrWorkPay.DrID, .PayValue =  clsDrWorkPay.PayValue, .PayDate =  clsDrWorkPay.PayDate, .Notes =  clsDrWorkPay.Notes })
			    Return RowsAffected > 0
			End Using
		End Function

		Public Function Update(oldDrWorkPay As DrWorkPay, newDrWorkPay As DrWorkPay) As Boolean
			Using conn As New SqlConnection(ConnectionString)
			    Dim parameters = New With { 
					.NewWorkID = newDrWorkPay.WorkID, .OldWorkID = oldDrWorkPay.WorkID, .NewDrID = newDrWorkPay.DrID, .OldDrID = oldDrWorkPay.DrID, .NewPayValue = newDrWorkPay.PayValue, .OldPayValue = oldDrWorkPay.PayValue, .NewPayDate = newDrWorkPay.PayDate, .OldPayDate = oldDrWorkPay.PayDate, .NewNotes = newDrWorkPay.Notes, .OldNotes = oldDrWorkPay.Notes
										  }
			    Dim affectedRows As Integer = conn.Execute("UPDATE [DrWorkPay] SET [WorkID] = @NewWorkID, [DrID] = @NewDrID, [PayValue] = @NewPayValue, [PayDate] = @NewPayDate, [Notes] = @NewNotes WHERE [WorkID] = @OldWorkID AND [DrID] = @OldDrID AND [PayValue] = @OldPayValue AND [PayDate] = @OldPayDate AND [Notes] = @OldNotes", parameters)
			    Return affectedRows > 0
			End Using
		End Function

		Public Function Delete(ByVal clsDrWorkPay As DrWorkPay) As Boolean
			Dim deleteStatement As String =
			"DELETE FROM [DrWorkPay] 
			WHERE WorkPayID = @WorkPayID"
			Using connection As SqlConnection = DentistXData.GetConnection()
			    connection.Open()
			    Dim affectedRows As Integer = connection.Execute(deleteStatement, New With { .WorkPayID = clsDrWorkPay.WorkPayID })
			    Return affectedRows > 0
			End Using
		End Function


'Methods to get parents and childs
	End Class
