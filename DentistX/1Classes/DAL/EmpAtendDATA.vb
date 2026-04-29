Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections
Imports System.Configuration
Imports Dapper

	Public Class EmpAtendDATA

		Private ConnectionString As String=DentistXDATA.GetConnection.connectionString



		Public Function SelectAll() As IEnumerable(Of EmpAtend)
			Using conn As New SqlConnection(ConnectionString)
			    conn.Open()
			    Return conn.Query(Of EmpAtend)("SELECT * FROM EmpAtend")
			End Using
		End Function
		

		Public Function Select_Record(ByVal clsEmpAtend As EmpAtend) As EmpAtend
			Using conn As New SqlConnection(ConnectionString)
			    Dim sql As String = "Select * FROM EmpAtend WHERE AtndID = @AtndID"
			    Return conn.QuerySingleOrDefault(Of EmpAtend)(sql, New With { .AtndID = clsEmpAtend.AtndID })
			End Using
		End Function

		Public Function Add(ByVal clsEmpAtend As EmpAtend) As Boolean
			Dim RowsAffected As Integer=0
			Using conn As New SqlConnection(ConnectionString)
			    Dim sql As String = "INSERT INTO EmpAtend (EmpID, AtnDay, AtnNote, AbsPrsnt) VALUES (@EmpID, @AtnDay, @AtnNote, @AbsPrsnt)" 
			    RowsAffected = conn.Execute(sql, New With { .EmpID =  clsEmpAtend.EmpID, .AtnDay =  clsEmpAtend.AtnDay, .AtnNote =  clsEmpAtend.AtnNote, .AbsPrsnt =  clsEmpAtend.AbsPrsnt })
			    Return RowsAffected > 0
			End Using
		End Function

		Public Function Update(oldEmpAtend As EmpAtend, newEmpAtend As EmpAtend) As Boolean
			Using conn As New SqlConnection(ConnectionString)
			    Dim parameters = New With { 
					.NewEmpID = newEmpAtend.EmpID, .OldEmpID = oldEmpAtend.EmpID, .NewAtnDay = newEmpAtend.AtnDay, .OldAtnDay = oldEmpAtend.AtnDay, .NewAtnNote = newEmpAtend.AtnNote, .OldAtnNote = oldEmpAtend.AtnNote, .NewAbsPrsnt = newEmpAtend.AbsPrsnt, .OldAbsPrsnt = oldEmpAtend.AbsPrsnt
										  }
			    Dim affectedRows As Integer = conn.Execute("UPDATE [EmpAtend] SET [EmpID] = @NewEmpID, [AtnDay] = @NewAtnDay, [AtnNote] = @NewAtnNote, [AbsPrsnt] = @NewAbsPrsnt WHERE [EmpID] = @OldEmpID AND [AtnDay] = @OldAtnDay AND [AtnNote] = @OldAtnNote AND [AbsPrsnt] = @OldAbsPrsnt", parameters)
			    Return affectedRows > 0
			End Using
		End Function

		Public Function Delete(ByVal clsEmpAtend As EmpAtend) As Boolean
			Dim deleteStatement As String =
			"DELETE FROM [EmpAtend] 
			WHERE AtndID = @AtndID"
			Using connection As SqlConnection = DentistXData.GetConnection()
			    connection.Open()
			    Dim affectedRows As Integer = connection.Execute(deleteStatement, New With { .AtndID = clsEmpAtend.AtndID })
			    Return affectedRows > 0
			End Using
		End Function


'Methods to get parents and childs
	End Class
