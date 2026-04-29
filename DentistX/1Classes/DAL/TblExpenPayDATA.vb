Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections
Imports System.Configuration
Imports Dapper

	Public Class TblExpenPayDATA

		Private ConnectionString As String=DentistXDATA.GetConnection.connectionString



		Public Function SelectAll() As IEnumerable(Of TblExpenPay)
			Using conn As New SqlConnection(ConnectionString)
			    conn.Open()
			    Return conn.Query(Of TblExpenPay)("SELECT * FROM TblExpenPay")
			End Using
		End Function
		

		Public Function Select_Record(ByVal clsTblExpenPay As TblExpenPay) As TblExpenPay
			Using conn As New SqlConnection(ConnectionString)
			    Dim sql As String = "Select * FROM TblExpenPay WHERE ExpPayID = @ExpPayID"
			    Return conn.QuerySingleOrDefault(Of TblExpenPay)(sql, New With { .ExpPayID = clsTblExpenPay.ExpPayID })
			End Using
		End Function

		Public Function Add(ByVal clsTblExpenPay As TblExpenPay) As Boolean
			Dim RowsAffected As Integer=0
			Using conn As New SqlConnection(ConnectionString)
			    Dim sql As String = "INSERT INTO TblExpenPay (MasrofID, PayValue, PayDate, Notes) VALUES (@MasrofID, @PayValue, @PayDate, @Notes)" 
			    RowsAffected = conn.Execute(sql, New With { .MasrofID =  clsTblExpenPay.MasrofID, .PayValue =  clsTblExpenPay.PayValue, .PayDate =  clsTblExpenPay.PayDate, .Notes =  clsTblExpenPay.Notes })
			    Return RowsAffected > 0
			End Using
		End Function

		Public Function Update(oldTblExpenPay As TblExpenPay, newTblExpenPay As TblExpenPay) As Boolean
			Using conn As New SqlConnection(ConnectionString)
			    Dim parameters = New With { 
					.NewMasrofID = newTblExpenPay.MasrofID, .OldMasrofID = oldTblExpenPay.MasrofID, .NewPayValue = newTblExpenPay.PayValue, .OldPayValue = oldTblExpenPay.PayValue, .NewPayDate = newTblExpenPay.PayDate, .OldPayDate = oldTblExpenPay.PayDate, .NewNotes = newTblExpenPay.Notes, .OldNotes = oldTblExpenPay.Notes
										  }
			    Dim affectedRows As Integer = conn.Execute("UPDATE [TblExpenPay] SET [MasrofID] = @NewMasrofID, [PayValue] = @NewPayValue, [PayDate] = @NewPayDate, [Notes] = @NewNotes WHERE [MasrofID] = @OldMasrofID AND [PayValue] = @OldPayValue AND [PayDate] = @OldPayDate AND [Notes] = @OldNotes", parameters)
			    Return affectedRows > 0
			End Using
		End Function

		Public Function Delete(ByVal clsTblExpenPay As TblExpenPay) As Boolean
			Dim deleteStatement As String =
			"DELETE FROM [TblExpenPay] 
			WHERE ExpPayID = @ExpPayID"
			Using connection As SqlConnection = DentistXData.GetConnection()
			    connection.Open()
			    Dim affectedRows As Integer = connection.Execute(deleteStatement, New With { .ExpPayID = clsTblExpenPay.ExpPayID })
			    Return affectedRows > 0
			End Using
		End Function


'Methods to get parents and childs
	End Class
