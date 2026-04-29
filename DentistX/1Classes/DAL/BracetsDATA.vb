Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections
Imports System.Configuration
Imports Dapper

Public Class BracetsDATA

	Private ConnectionString As String=DentistXDATA.GetConnection.connectionString



	Public Function SelectAll() As IEnumerable(Of Bracets)
		Using conn As New SqlConnection(ConnectionString)
		    conn.Open()
		    Return conn.Query(Of Bracets)("SELECT [BracetID], [BracetName] FROM Bracets")
		End Using
	End Function
	

	Public Function Select_Record(ByVal BracetID As Integer) As Bracets
		Using conn As New SqlConnection(ConnectionString)
		    Dim sql As String = "SELECT * FROM Bracets WHERE BracetID = @BracetID"
		    Return conn.QuerySingleOrDefault(Of Bracets)(sql, New With { .BracetID = BracetID })
		End Using
	End Function

	Public Function Add(ByVal clsBracets As Bracets) As Boolean
		Dim RowsAffected As Integer=0
		Using conn As New SqlConnection(ConnectionString)
		    Dim sql As String = "INSERT INTO Bracets (BracetName) VALUES (@BracetName)" 
		    RowsAffected = conn.Execute(sql, New With { .BracetName =  clsBracets.BracetName })
		    Return RowsAffected > 0
		End Using
	End Function

	Public Function Update(oldBracets As Bracets, newBracets As Bracets) As Boolean
		Using conn As New SqlConnection(ConnectionString)
		    Dim parameters = New With { 
					.NewBracetName = newBracets.BracetName, .KeyBracetID = oldBracets.BracetID
									  }
		    Dim affectedRows As Integer = conn.Execute("UPDATE [Bracets] SET [BracetName] = @NewBracetName WHERE [BracetID] = @KeyBracetID", parameters)
		    Return affectedRows > 0
		End Using
	End Function

	Public Function Delete(ByVal clsBracets As Bracets) As Boolean
		Dim deleteStatement As String =
		"DELETE FROM [Bracets] 
		WHERE BracetID = @BracetID"
		Using connection As SqlConnection = DentistXData.GetConnection()
		    connection.Open()
		    Dim affectedRows As Integer = connection.Execute(deleteStatement, New With { .BracetID = clsBracets.BracetID })
		    Return affectedRows > 0
		End Using
	End Function


'Methods to get parents and childs
  ' VB.NET Code Generation for Parent using Dapper
  ' VB.NET Code Generation for Child
End Class
