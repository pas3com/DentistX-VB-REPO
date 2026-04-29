Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections
Imports System.Configuration
Imports Dapper

	Public Class TblUnitsDATA

		Private ConnectionString As String=DentistXDATA.GetConnection.connectionString



		Public Function SelectAll() As IEnumerable(Of TblUnits)
			Using conn As New SqlConnection(ConnectionString)
			    conn.Open()
			    Return conn.Query(Of TblUnits)("SELECT * FROM TblUnits")
			End Using
		End Function
		

		Public Function Select_Record(ByVal clsTblUnits As TblUnits) As TblUnits
			Using conn As New SqlConnection(ConnectionString)
			    Dim sql As String = "Select * FROM TblUnits WHERE UnitID = @UnitID"
			    Return conn.QuerySingleOrDefault(Of TblUnits)(sql, New With { .UnitID = clsTblUnits.UnitID })
			End Using
		End Function

		Public Function Add(ByVal clsTblUnits As TblUnits) As Boolean
			Dim RowsAffected As Integer=0
			Using conn As New SqlConnection(ConnectionString)
			    Dim sql As String = "INSERT INTO TblUnits (UnitName) VALUES (@UnitName)" 
			    RowsAffected = conn.Execute(sql, New With { .UnitName =  clsTblUnits.UnitName })
			    Return RowsAffected > 0
			End Using
		End Function

		Public Function Update(oldTblUnits As TblUnits, newTblUnits As TblUnits) As Boolean
			Using conn As New SqlConnection(ConnectionString)
			    Dim parameters = New With { 
					.NewUnitName = newTblUnits.UnitName, .OldUnitName = oldTblUnits.UnitName
										  }
			    Dim affectedRows As Integer = conn.Execute("UPDATE [TblUnits] SET [UnitName] = @NewUnitName WHERE [UnitName] = @OldUnitName", parameters)
			    Return affectedRows > 0
			End Using
		End Function

		Public Function Delete(ByVal clsTblUnits As TblUnits) As Boolean
			Dim deleteStatement As String =
			"DELETE FROM [TblUnits] 
			WHERE UnitID = @UnitID"
			Using connection As SqlConnection = DentistXData.GetConnection()
			    connection.Open()
			    Dim affectedRows As Integer = connection.Execute(deleteStatement, New With { .UnitID = clsTblUnits.UnitID })
			    Return affectedRows > 0
			End Using
		End Function


'Methods to get parents and childs
		Public Function GetTblItems(ByVal clsTblUnits As TblUnits ) As IEnumerable(Of TblItems)
			Using conn As New SqlConnection(ConnectionString)
				conn.Open()
				Dim query As String = "SELECT * FROM [TblItems] WHERE [UnitID] = @UnitID"
				Return conn.Query(Of TblItems)(query, New With { .UnitID= clsTblUnits.UnitID })
			End Using
		End Function

	End Class
