Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections
Imports System.Configuration
Imports Dapper

	Public Class ImplantLengthDATA

		Private ConnectionString As String=DentistXDATA.GetConnection.connectionString



		Public Function SelectAll() As IEnumerable(Of ImplantLength)
			Using conn As New SqlConnection(ConnectionString)
			    conn.Open()
			    Return conn.Query(Of ImplantLength)("SELECT * FROM ImplantLength")
			End Using
		End Function
		

		Public Function Select_Record(ByVal clsImplantLength As ImplantLength) As ImplantLength
			Using conn As New SqlConnection(ConnectionString)
			    Dim sql As String = "Select * FROM ImplantLength WHERE LengthID = @LengthID"
			    Return conn.QuerySingleOrDefault(Of ImplantLength)(sql, New With { .LengthID = clsImplantLength.LengthID })
			End Using
		End Function

		Public Function Add(ByVal clsImplantLength As ImplantLength) As Boolean
			Dim RowsAffected As Integer=0
			Using conn As New SqlConnection(ConnectionString)
			    Dim sql As String = "INSERT INTO ImplantLength (LengthMM) VALUES (@LengthMM)" 
			    RowsAffected = conn.Execute(sql, New With { .LengthMM =  clsImplantLength.LengthMM })
			    Return RowsAffected > 0
			End Using
		End Function

		Public Function Update(oldImplantLength As ImplantLength, newImplantLength As ImplantLength) As Boolean
			Using conn As New SqlConnection(ConnectionString)
			    Dim parameters = New With { 
					.NewLengthMM = newImplantLength.LengthMM, .OldLengthMM = oldImplantLength.LengthMM
										  }
			    Dim affectedRows As Integer = conn.Execute("UPDATE [ImplantLength] SET [LengthMM] = @NewLengthMM WHERE [LengthMM] = @OldLengthMM", parameters)
			    Return affectedRows > 0
			End Using
		End Function

		Public Function Delete(ByVal clsImplantLength As ImplantLength) As Boolean
			Dim deleteStatement As String =
			"DELETE FROM [ImplantLength] 
			WHERE LengthID = @LengthID"
			Using connection As SqlConnection = DentistXData.GetConnection()
			    connection.Open()
			    Dim affectedRows As Integer = connection.Execute(deleteStatement, New With { .LengthID = clsImplantLength.LengthID })
			    Return affectedRows > 0
			End Using
		End Function


'Methods to get parents and childs
	End Class
