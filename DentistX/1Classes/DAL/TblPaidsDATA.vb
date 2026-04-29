Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections
Imports System.Configuration
Imports Dapper

	Public Class TblPaidsDATA

		Private ConnectionString As String=DentistXDATA.GetConnection.connectionString



		Public Function SelectAll() As IEnumerable(Of TblPaids)
			Using conn As New SqlConnection(ConnectionString)
			    conn.Open()
			    Return conn.Query(Of TblPaids)("SELECT * FROM TblPaids")
			End Using
		End Function
		

		Public Function Select_Record(ByVal clsTblPaids As TblPaids) As TblPaids
			Using conn As New SqlConnection(ConnectionString)
			    Dim sql As String = "Select * FROM TblPaids WHERE PayID = @PayID"
			    Return conn.QuerySingleOrDefault(Of TblPaids)(sql, New With { .PayID = clsTblPaids.PayID })
			End Using
		End Function

		Public Function Add(ByVal clsTblPaids As TblPaids) As Boolean
			Dim RowsAffected As Integer=0
			Using conn As New SqlConnection(ConnectionString)
			    Dim sql As String = "INSERT INTO TblPaids (PayType, PayDate, ResCusId, PayAmount, PayEx) VALUES (@PayType, @PayDate, @ResCusId, @PayAmount, @PayEx)" 
			    RowsAffected = conn.Execute(sql, New With { .PayType =  clsTblPaids.PayType, .PayDate =  clsTblPaids.PayDate, .ResCusId =  clsTblPaids.ResCusId, .PayAmount =  clsTblPaids.PayAmount, .PayEx =  clsTblPaids.PayEx })
			    Return RowsAffected > 0
			End Using
		End Function

		Public Function Update(oldTblPaids As TblPaids, newTblPaids As TblPaids) As Boolean
			Using conn As New SqlConnection(ConnectionString)
			    Dim parameters = New With { 
					.NewPayType = newTblPaids.PayType, .OldPayType = oldTblPaids.PayType, .NewPayDate = newTblPaids.PayDate, .OldPayDate = oldTblPaids.PayDate, .NewResCusId = newTblPaids.ResCusId, .OldResCusId = oldTblPaids.ResCusId, .NewPayAmount = newTblPaids.PayAmount, .OldPayAmount = oldTblPaids.PayAmount, .NewPayEx = newTblPaids.PayEx, .OldPayEx = oldTblPaids.PayEx
										  }
			    Dim affectedRows As Integer = conn.Execute("UPDATE [TblPaids] SET [PayType] = @NewPayType, [PayDate] = @NewPayDate, [ResCusId] = @NewResCusId, [PayAmount] = @NewPayAmount, [PayEx] = @NewPayEx WHERE [PayType] = @OldPayType AND [PayDate] = @OldPayDate AND [ResCusId] = @OldResCusId AND [PayAmount] = @OldPayAmount AND [PayEx] = @OldPayEx", parameters)
			    Return affectedRows > 0
			End Using
		End Function

		Public Function Delete(ByVal clsTblPaids As TblPaids) As Boolean
			Dim deleteStatement As String =
			"DELETE FROM [TblPaids] 
			WHERE PayID = @PayID"
			Using connection As SqlConnection = DentistXData.GetConnection()
			    connection.Open()
			    Dim affectedRows As Integer = connection.Execute(deleteStatement, New With { .PayID = clsTblPaids.PayID })
			    Return affectedRows > 0
			End Using
		End Function


'Methods to get parents and childs
	End Class
