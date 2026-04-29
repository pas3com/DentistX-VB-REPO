Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections
Imports System.Configuration
Imports Dapper

	Public Class EmpPayDATA

		Private ConnectionString As String=DentistXDATA.GetConnection.connectionString



		Public Function SelectAll() As IEnumerable(Of EmpPay)
			Using conn As New SqlConnection(ConnectionString)
			    conn.Open()
			    Return conn.Query(Of EmpPay)("SELECT * FROM EmpPay")
			End Using
		End Function
		

		Public Function Select_Record(ByVal clsEmpPay As EmpPay) As EmpPay
			Using conn As New SqlConnection(ConnectionString)
			    Dim sql As String = "Select * FROM EmpPay WHERE UsSalID = @UsSalID"
			    Return conn.QuerySingleOrDefault(Of EmpPay)(sql, New With { .UsSalID = clsEmpPay.UsSalID })
			End Using
		End Function

		Public Function Add(ByVal clsEmpPay As EmpPay) As Boolean
			Dim RowsAffected As Integer=0
			Using conn As New SqlConnection(ConnectionString)
			    Dim sql As String = "INSERT INTO EmpPay (EmpID, MonthPay, DayPay, FromDT, ToDT, DaysCount, PayDate, PayNote) VALUES (@EmpID, @MonthPay, @DayPay, @FromDT, @ToDT, @DaysCount, @PayDate, @PayNote)" 
			    RowsAffected = conn.Execute(sql, New With { .EmpID =  clsEmpPay.EmpID, .MonthPay =  clsEmpPay.MonthPay, .DayPay =  clsEmpPay.DayPay, .FromDT =  clsEmpPay.FromDT, .ToDT =  clsEmpPay.ToDT, .DaysCount =  clsEmpPay.DaysCount, .PayDate =  clsEmpPay.PayDate, .PayNote =  clsEmpPay.PayNote })
			    Return RowsAffected > 0
			End Using
		End Function

		Public Function Update(oldEmpPay As EmpPay, newEmpPay As EmpPay) As Boolean
			Using conn As New SqlConnection(ConnectionString)
			    Dim parameters = New With { 
					.NewEmpID = newEmpPay.EmpID, .OldEmpID = oldEmpPay.EmpID, .NewMonthPay = newEmpPay.MonthPay, .OldMonthPay = oldEmpPay.MonthPay, .NewDayPay = newEmpPay.DayPay, .OldDayPay = oldEmpPay.DayPay, .NewFromDT = newEmpPay.FromDT, .OldFromDT = oldEmpPay.FromDT, .NewToDT = newEmpPay.ToDT, .OldToDT = oldEmpPay.ToDT, .NewDaysCount = newEmpPay.DaysCount, .OldDaysCount = oldEmpPay.DaysCount, .NewPayDate = newEmpPay.PayDate, .OldPayDate = oldEmpPay.PayDate, .NewPayNote = newEmpPay.PayNote, .OldPayNote = oldEmpPay.PayNote
										  }
			    Dim affectedRows As Integer = conn.Execute("UPDATE [EmpPay] SET [EmpID] = @NewEmpID, [MonthPay] = @NewMonthPay, [DayPay] = @NewDayPay, [FromDT] = @NewFromDT, [ToDT] = @NewToDT, [DaysCount] = @NewDaysCount, [PayDate] = @NewPayDate, [PayNote] = @NewPayNote WHERE [EmpID] = @OldEmpID AND [MonthPay] = @OldMonthPay AND [DayPay] = @OldDayPay AND [FromDT] = @OldFromDT AND [ToDT] = @OldToDT AND [DaysCount] = @OldDaysCount AND [PayDate] = @OldPayDate AND [PayNote] = @OldPayNote", parameters)
			    Return affectedRows > 0
			End Using
		End Function

		Public Function Delete(ByVal clsEmpPay As EmpPay) As Boolean
			Dim deleteStatement As String =
			"DELETE FROM [EmpPay] 
			WHERE UsSalID = @UsSalID"
			Using connection As SqlConnection = DentistXData.GetConnection()
			    connection.Open()
			    Dim affectedRows As Integer = connection.Execute(deleteStatement, New With { .UsSalID = clsEmpPay.UsSalID })
			    Return affectedRows > 0
			End Using
		End Function


'Methods to get parents and childs
	End Class
