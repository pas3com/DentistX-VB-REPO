Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections
Imports System.Configuration
Imports Dapper

	Public Class TblBnodMsareefDATA

		Private ConnectionString As String=DentistXDATA.GetConnection.connectionString



		Public Function SelectAll() As IEnumerable(Of TblBnodMsareef)
			Using conn As New SqlConnection(ConnectionString)
			    conn.Open()
			    Return conn.Query(Of TblBnodMsareef)("SELECT * FROM TblBnodMsareef")
			End Using
		End Function
		

		Public Function Select_Record(ByVal clsTblBnodMsareef As TblBnodMsareef) As TblBnodMsareef
			Using conn As New SqlConnection(ConnectionString)
			    Dim sql As String = "Select * FROM TblBnodMsareef WHERE BandID = @BandID"
			    Return conn.QuerySingleOrDefault(Of TblBnodMsareef)(sql, New With { .BandID = clsTblBnodMsareef.BandID })
			End Using
		End Function

		Public Function Add(ByVal clsTblBnodMsareef As TblBnodMsareef) As Boolean
			Dim RowsAffected As Integer=0
			Using conn As New SqlConnection(ConnectionString)
			    Dim sql As String = "INSERT INTO TblBnodMsareef (BandName) VALUES (@BandName)" 
			    RowsAffected = conn.Execute(sql, New With { .BandName =  clsTblBnodMsareef.BandName })
			    Return RowsAffected > 0
			End Using
		End Function

		Public Function Update(oldTblBnodMsareef As TblBnodMsareef, newTblBnodMsareef As TblBnodMsareef) As Boolean
			Using conn As New SqlConnection(ConnectionString)
			    Dim parameters = New With { 
					.NewBandName = newTblBnodMsareef.BandName, .OldBandName = oldTblBnodMsareef.BandName
										  }
			    Dim affectedRows As Integer = conn.Execute("UPDATE [TblBnodMsareef] SET [BandName] = @NewBandName WHERE [BandName] = @OldBandName", parameters)
			    Return affectedRows > 0
			End Using
		End Function

		Public Function Delete(ByVal clsTblBnodMsareef As TblBnodMsareef) As Boolean
			Dim deleteStatement As String =
			"DELETE FROM [TblBnodMsareef] 
			WHERE BandID = @BandID"
			Using connection As SqlConnection = DentistXData.GetConnection()
			    connection.Open()
			    Dim affectedRows As Integer = connection.Execute(deleteStatement, New With { .BandID = clsTblBnodMsareef.BandID })
			    Return affectedRows > 0
			End Using
		End Function


'Methods to get parents and childs
		Public Function GetTblMasareef(ByVal clsTblBnodMsareef As TblBnodMsareef ) As IEnumerable(Of TblMasareef)
			Using conn As New SqlConnection(ConnectionString)
				conn.Open()
				Dim query As String = "SELECT * FROM [TblMasareef] WHERE [BandID] = @BandID"
				Return conn.Query(Of TblMasareef)(query, New With { .BandID= clsTblBnodMsareef.BandID })
			End Using
		End Function

	End Class
