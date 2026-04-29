Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections
Imports System.Configuration
Imports Dapper

	Public Class CtlRepDATA

		Private ConnectionString As String=DentistXDATA.GetConnection.connectionString



		Public Function SelectAll() As IEnumerable(Of CtlRep)
			Using conn As New SqlConnection(ConnectionString)
			    conn.Open()
			    Return conn.Query(Of CtlRep)("SELECT * FROM CtlRep")
			End Using
		End Function
		

		Public Function Select_Record(ByVal clsCtlRep As CtlRep) As CtlRep
			Using conn As New SqlConnection(ConnectionString)
			    Dim sql As String = "Select * FROM CtlRep WHERE CtlID = @CtlID"
			    Return conn.QuerySingleOrDefault(Of CtlRep)(sql, New With { .CtlID = clsCtlRep.CtlID })
			End Using
		End Function

		Public Function Add(ByVal clsCtlRep As CtlRep) As Boolean
			Dim RowsAffected As Integer=0
			Using conn As New SqlConnection(ConnectionString)
			    Dim sql As String = "INSERT INTO CtlRep (Ctl, X, Y) VALUES (@Ctl, @X, @Y)" 
			    RowsAffected = conn.Execute(sql, New With { .Ctl =  clsCtlRep.Ctl, .X =  clsCtlRep.X, .Y =  clsCtlRep.Y })
			    Return RowsAffected > 0
			End Using
		End Function

		Public Function Update(oldCtlRep As CtlRep, newCtlRep As CtlRep) As Boolean
			Using conn As New SqlConnection(ConnectionString)
			    Dim parameters = New With { 
					.NewCtl = newCtlRep.Ctl, .OldCtl = oldCtlRep.Ctl, .NewX = newCtlRep.X, .OldX = oldCtlRep.X, .NewY = newCtlRep.Y, .OldY = oldCtlRep.Y
										  }
			    Dim affectedRows As Integer = conn.Execute("UPDATE [CtlRep] SET [Ctl] = @NewCtl, [X] = @NewX, [Y] = @NewY WHERE [Ctl] = @OldCtl AND [X] = @OldX AND [Y] = @OldY", parameters)
			    Return affectedRows > 0
			End Using
		End Function

		Public Function Delete(ByVal clsCtlRep As CtlRep) As Boolean
			Dim deleteStatement As String =
			"DELETE FROM [CtlRep] 
			WHERE CtlID = @CtlID"
			Using connection As SqlConnection = DentistXData.GetConnection()
			    connection.Open()
			    Dim affectedRows As Integer = connection.Execute(deleteStatement, New With { .CtlID = clsCtlRep.CtlID })
			    Return affectedRows > 0
			End Using
		End Function


'Methods to get parents and childs
	End Class
