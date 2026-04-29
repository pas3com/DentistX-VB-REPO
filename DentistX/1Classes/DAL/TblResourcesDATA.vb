Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections
Imports System.Configuration
Imports Dapper

	Public Class TblResourcesDATA

		Private ConnectionString As String=DentistXDATA.GetConnection.connectionString



		Public Function SelectAll() As IEnumerable(Of TblResources)
			Using conn As New SqlConnection(ConnectionString)
			    conn.Open()
			    Return conn.Query(Of TblResources)("SELECT * FROM TblResources")
			End Using
		End Function
		

		Public Function Select_Record(ByVal clsTblResources As TblResources) As TblResources
			Using conn As New SqlConnection(ConnectionString)
			    Dim sql As String = "Select * FROM TblResources WHERE ResID = @ResID"
			    Return conn.QuerySingleOrDefault(Of TblResources)(sql, New With { .ResID = clsTblResources.ResID })
			End Using
		End Function

		Public Function Add(ByVal clsTblResources As TblResources) As Boolean
			Dim RowsAffected As Integer=0
			Using conn As New SqlConnection(ConnectionString)
			    Dim sql As String = "INSERT INTO TblResources (ResName, CityID, Address, Contacts, ResInvsNet, ResTotalPays, ResBal) VALUES (@ResName, @CityID, @Address, @Contacts, @ResInvsNet, @ResTotalPays, @ResBal)" 
			    RowsAffected = conn.Execute(sql, New With { .ResName =  clsTblResources.ResName, .CityID =  clsTblResources.CityID, .Address =  clsTblResources.Address, .Contacts =  clsTblResources.Contacts, .ResInvsNet =  clsTblResources.ResInvsNet, .ResTotalPays =  clsTblResources.ResTotalPays, .ResBal =  clsTblResources.ResBal })
			    Return RowsAffected > 0
			End Using
		End Function

		Public Function Update(oldTblResources As TblResources, newTblResources As TblResources) As Boolean
			Using conn As New SqlConnection(ConnectionString)
			    Dim parameters = New With { 
					.NewResName = newTblResources.ResName, .OldResName = oldTblResources.ResName, .NewCityID = newTblResources.CityID, .OldCityID = oldTblResources.CityID, .NewAddress = newTblResources.Address, .OldAddress = oldTblResources.Address, .NewContacts = newTblResources.Contacts, .OldContacts = oldTblResources.Contacts, .NewResInvsNet = newTblResources.ResInvsNet, .OldResInvsNet = oldTblResources.ResInvsNet, .NewResTotalPays = newTblResources.ResTotalPays, .OldResTotalPays = oldTblResources.ResTotalPays, .NewResBal = newTblResources.ResBal, .OldResBal = oldTblResources.ResBal
										  }
			    Dim affectedRows As Integer = conn.Execute("UPDATE [TblResources] SET [ResName] = @NewResName, [CityID] = @NewCityID, [Address] = @NewAddress, [Contacts] = @NewContacts, [ResInvsNet] = @NewResInvsNet, [ResTotalPays] = @NewResTotalPays, [ResBal] = @NewResBal WHERE [ResName] = @OldResName AND [CityID] = @OldCityID AND [Address] = @OldAddress AND [Contacts] = @OldContacts AND [ResInvsNet] = @OldResInvsNet AND [ResTotalPays] = @OldResTotalPays AND [ResBal] = @OldResBal", parameters)
			    Return affectedRows > 0
			End Using
		End Function

		Public Function Delete(ByVal clsTblResources As TblResources) As Boolean
			Dim deleteStatement As String =
			"DELETE FROM [TblResources] 
			WHERE ResID = @ResID"
			Using connection As SqlConnection = DentistXData.GetConnection()
			    connection.Open()
			    Dim affectedRows As Integer = connection.Execute(deleteStatement, New With { .ResID = clsTblResources.ResID })
			    Return affectedRows > 0
			End Using
		End Function


'Methods to get parents and childs
		Public Function GetTblCities(ByVal CityID As Integer) As TblCities
		Dim parent As TblCities = Nothing
		Using conn As New SqlConnection(ConnectionString)
			Dim query As String = "SELECT * FROM [TblCities] WHERE [CityID] = @CityID"
			Try
				conn.Open()
				parent = conn.QuerySingleOrDefault(Of TblCities)(query, New With {.CityID = CityID})
			Catch ex As Exception
				' Handle exceptions
			Finally
				If conn.State = ConnectionState.Open Then conn.Close()
			End Try
		End Using
		Return parent
		End Function

		Public Function GetTblInvoicesHeader(ByVal clsTblResources As TblResources ) As IEnumerable(Of TblInvoicesHeader)
			Using conn As New SqlConnection(ConnectionString)
				conn.Open()
				Dim query As String = "SELECT * FROM [TblInvoicesHeader] WHERE [ResID] = @ResID"
				Return conn.Query(Of TblInvoicesHeader)(query, New With { .ResID= clsTblResources.ResID })
			End Using
		End Function

		Public Function GetTblInvPay(ByVal clsTblResources As TblResources ) As IEnumerable(Of TblInvPay)
			Using conn As New SqlConnection(ConnectionString)
				conn.Open()
				Dim query As String = "SELECT * FROM [TblInvPay] WHERE [ResID] = @ResID"
				Return conn.Query(Of TblInvPay)(query, New With { .ResID= clsTblResources.ResID })
			End Using
		End Function

	End Class
