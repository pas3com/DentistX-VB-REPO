Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections
Imports System.Configuration
Imports Dapper

	Public Class TblWireTypeDATA

		Private ConnectionString As String=DentistXDATA.GetConnection.connectionString



		Public Function SelectAll() As IEnumerable(Of TblWireType)
			Using conn As New SqlConnection(ConnectionString)
			    conn.Open()
			    Return conn.Query(Of TblWireType)("SELECT * FROM TblWireType")
			End Using
		End Function
		

		Public Function Select_Record(ByVal clsTblWireType As TblWireType) As TblWireType
			Using conn As New SqlConnection(ConnectionString)
			    Dim sql As String = "Select * FROM TblWireType WHERE TypeID = @TypeID"
			    Return conn.QuerySingleOrDefault(Of TblWireType)(sql, New With { .TypeID = clsTblWireType.TypeID })
			End Using
		End Function

		Public Function Add(ByVal clsTblWireType As TblWireType) As Boolean
			Dim RowsAffected As Integer=0
			Using conn As New SqlConnection(ConnectionString)
			    Dim sql As String = "INSERT INTO TblWireType (WireType) VALUES (@WireType)" 
			    RowsAffected = conn.Execute(sql, New With { .WireType =  clsTblWireType.WireType })
			    Return RowsAffected > 0
			End Using
		End Function

		Public Function Update(oldTblWireType As TblWireType, newTblWireType As TblWireType) As Boolean
			Using conn As New SqlConnection(ConnectionString)
			    Dim parameters = New With { 
					.NewWireType = newTblWireType.WireType, .OldWireType = oldTblWireType.WireType
										  }
			    Dim affectedRows As Integer = conn.Execute("UPDATE [TblWireType] SET [WireType] = @NewWireType WHERE [WireType] = @OldWireType", parameters)
			    Return affectedRows > 0
			End Using
		End Function

		Public Function Delete(ByVal clsTblWireType As TblWireType) As Boolean
			Dim deleteStatement As String =
			"DELETE FROM [TblWireType] 
			WHERE TypeID = @TypeID"
			Using connection As SqlConnection = DentistXData.GetConnection()
			    connection.Open()
			    Dim affectedRows As Integer = connection.Execute(deleteStatement, New With { .TypeID = clsTblWireType.TypeID })
			    Return affectedRows > 0
			End Using
		End Function


'Methods to get parents and childs
	End Class
