Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections
Imports System.Configuration
Imports Dapper

	Public Class DrWorkDATA

		Private ConnectionString As String=DentistXDATA.GetConnection.connectionString



		Public Function SelectAll() As IEnumerable(Of DrWork)
			Using conn As New SqlConnection(ConnectionString)
			    conn.Open()
			Return conn.Query(Of DrWork)("SELECT [WorkID]
										  ,[dbo].[DrWork].[DrID]
										  ,[Doctors].[DrID]
										  ,[dbo].[DrWork].[PatientID]
										  ,[Patient].[PatientName]
										  ,[WrkDate]
										  ,[WrkDetail]
										  ,[WrkVal]
										  ,[PayVal]
										  ,[Imp]
										  ,[Orth]
										  ,[Surg]
										  ,[dbo].[DrWork].[Notes]
									  FROM [dbo].[DrWork] inner join [dbo].[Doctors] on [dbo].[DrWork].[DrID]=[Doctors].[DrID]
												inner join [Patient] on [dbo].[DrWork].[PatientID]=[Patient].[PatientID]
									")
		End Using
		End Function
		

		Public Function Select_Record(ByVal clsDrWork As DrWork) As DrWork
			Using conn As New SqlConnection(ConnectionString)
			Dim sql As String = "SELECT [WorkID]
									  ,[dbo].[DrWork].[DrID]
									  ,[Doctors].[DrID]
									  ,[dbo].[DrWork].[PatientID]
									  ,[Patient].[PatientName]
									  ,[WrkDate]
									  ,[WrkDetail]
									  ,[WrkVal]
									  ,[PayVal]
									  ,[Imp]
									  ,[Orth]
									  ,[Surg]
									  ,[dbo].[DrWork].[Notes]
								  FROM [dbo].[DrWork] inner join [dbo].[Doctors] on [dbo].[DrWork].[DrID]=[Doctors].[DrID]
											inner join [Patient] on [dbo].[DrWork].[PatientID]=[Patient].[PatientID]
								WHERE WorkID = @WorkID"
			Return conn.QuerySingleOrDefault(Of DrWork)(sql, New With { .WorkID = clsDrWork.WorkID })
			End Using
		End Function

		Public Function Add(ByVal clsDrWork As DrWork) As Boolean
			Dim RowsAffected As Integer=0
			Using conn As New SqlConnection(ConnectionString)
			    Dim sql As String = "INSERT INTO DrWork (DrID, PatientID, WrkDate, WrkDetail, WrkVal, PayVal, Imp, Orth, Surg, Notes) VALUES (@DrID, @PatientID, @WrkDate, @WrkDetail, @WrkVal, @PayVal, @Imp, @Orth, @Surg, @Notes)" 
			    RowsAffected = conn.Execute(sql, New With { .DrID =  clsDrWork.DrID, .PatientID =  clsDrWork.PatientID, .WrkDate =  clsDrWork.WrkDate, .WrkDetail =  clsDrWork.WrkDetail, .WrkVal =  clsDrWork.WrkVal, .PayVal =  clsDrWork.PayVal, .Imp =  clsDrWork.Imp, .Orth =  clsDrWork.Orth, .Surg =  clsDrWork.Surg, .Notes =  clsDrWork.Notes })
			    Return RowsAffected > 0
			End Using
		End Function

		Public Function Update(oldDrWork As DrWork, newDrWork As DrWork) As Boolean
			Using conn As New SqlConnection(ConnectionString)
			    Dim parameters = New With { 
					.NewDrID = newDrWork.DrID, .OldDrID = oldDrWork.DrID, .NewPatientID = newDrWork.PatientID, .OldPatientID = oldDrWork.PatientID, .NewWrkDate = newDrWork.WrkDate, .OldWrkDate = oldDrWork.WrkDate, .NewWrkDetail = newDrWork.WrkDetail, .OldWrkDetail = oldDrWork.WrkDetail, .NewWrkVal = newDrWork.WrkVal, .OldWrkVal = oldDrWork.WrkVal, .NewPayVal = newDrWork.PayVal, .OldPayVal = oldDrWork.PayVal, .NewImp = newDrWork.Imp, .OldImp = oldDrWork.Imp, .NewOrth = newDrWork.Orth, .OldOrth = oldDrWork.Orth, .NewSurg = newDrWork.Surg, .OldSurg = oldDrWork.Surg, .NewNotes = newDrWork.Notes, .OldNotes = oldDrWork.Notes
										  }
			    Dim affectedRows As Integer = conn.Execute("UPDATE [DrWork] SET [DrID] = @NewDrID, [PatientID] = @NewPatientID, [WrkDate] = @NewWrkDate, [WrkDetail] = @NewWrkDetail, [WrkVal] = @NewWrkVal, [PayVal] = @NewPayVal, [Imp] = @NewImp, [Orth] = @NewOrth, [Surg] = @NewSurg, [Notes] = @NewNotes WHERE [DrID] = @OldDrID AND [PatientID] = @OldPatientID AND [WrkDate] = @OldWrkDate AND [WrkDetail] = @OldWrkDetail AND [WrkVal] = @OldWrkVal AND [PayVal] = @OldPayVal AND [Imp] = @OldImp AND [Orth] = @OldOrth AND [Surg] = @OldSurg AND [Notes] = @OldNotes", parameters)
			    Return affectedRows > 0
			End Using
		End Function

		Public Function Delete(ByVal clsDrWork As DrWork) As Boolean
			Dim deleteStatement As String =
			"DELETE FROM [DrWork] 
			WHERE WorkID = @WorkID"
			Using connection As SqlConnection = DentistXData.GetConnection()
			    connection.Open()
			    Dim affectedRows As Integer = connection.Execute(deleteStatement, New With { .WorkID = clsDrWork.WorkID })
			    Return affectedRows > 0
			End Using
		End Function


'Methods to get parents and childs
	End Class
