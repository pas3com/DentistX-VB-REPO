Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections
Imports System.Configuration
Imports Dapper

	Public Class TblCitiesDATA

		Private ConnectionString As String=DentistXDATA.GetConnection.connectionString



		Public Function SelectAll() As IEnumerable(Of TblCities)
			Using conn As New SqlConnection(ConnectionString)
			    conn.Open()
			    Return conn.Query(Of TblCities)("SELECT * FROM TblCities")
			End Using
		End Function
		

		Public Function Select_Record(ByVal clsTblCities As TblCities) As TblCities
			Using conn As New SqlConnection(ConnectionString)
			    Dim sql As String = "Select * FROM TblCities WHERE CityID = @CityID"
			    Return conn.QuerySingleOrDefault(Of TblCities)(sql, New With { .CityID = clsTblCities.CityID })
			End Using
		End Function

		Public Function Add(ByVal clsTblCities As TblCities) As Boolean
			Dim RowsAffected As Integer=0
			Using conn As New SqlConnection(ConnectionString)
			    Dim sql As String = "INSERT INTO TblCities (CityName) VALUES (@CityName)" 
			    RowsAffected = conn.Execute(sql, New With { .CityName =  clsTblCities.CityName })
			    Return RowsAffected > 0
			End Using
		End Function

		Public Function Update(oldTblCities As TblCities, newTblCities As TblCities) As Boolean
			Using conn As New SqlConnection(ConnectionString)
			    Dim parameters = New With { 
					.NewCityName = newTblCities.CityName, .OldCityName = oldTblCities.CityName
										  }
			    Dim affectedRows As Integer = conn.Execute("UPDATE [TblCities] SET [CityName] = @NewCityName WHERE [CityName] = @OldCityName", parameters)
			    Return affectedRows > 0
			End Using
		End Function

		Public Function Delete(ByVal clsTblCities As TblCities) As Boolean
			Dim deleteStatement As String =
			"DELETE FROM [TblCities] 
			WHERE CityID = @CityID"
			Using connection As SqlConnection = DentistXData.GetConnection()
			    connection.Open()
			    Dim affectedRows As Integer = connection.Execute(deleteStatement, New With { .CityID = clsTblCities.CityID })
			    Return affectedRows > 0
			End Using
		End Function


'Methods to get parents and childs
		Public Function GetTblCustomers(ByVal clsTblCities As TblCities ) As IEnumerable(Of TblCustomers)
			Using conn As New SqlConnection(ConnectionString)
				conn.Open()
				Dim query As String = "SELECT * FROM [TblCustomers] WHERE [CityID] = @CityID"
				Return conn.Query(Of TblCustomers)(query, New With { .CityID= clsTblCities.CityID })
			End Using
		End Function

		Public Function GetTblResources(ByVal clsTblCities As TblCities ) As IEnumerable(Of TblResources)
			Using conn As New SqlConnection(ConnectionString)
				conn.Open()
				Dim query As String = "SELECT * FROM [TblResources] WHERE [CityID] = @CityID"
				Return conn.Query(Of TblResources)(query, New With { .CityID= clsTblCities.CityID })
			End Using
		End Function

	End Class
