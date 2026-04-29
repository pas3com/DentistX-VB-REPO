Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections
Imports System.Configuration
Imports Dapper

	Public Class TblCustomersDATA

		Private ConnectionString As String=DentistXDATA.GetConnection.connectionString



		Public Function SelectAll() As IEnumerable(Of TblCustomers)
			Using conn As New SqlConnection(ConnectionString)
			    conn.Open()
			    Return conn.Query(Of TblCustomers)("SELECT * FROM TblCustomers")
			End Using
		End Function
		

		Public Function Select_Record(ByVal clsTblCustomers As TblCustomers) As TblCustomers
			Using conn As New SqlConnection(ConnectionString)
			    Dim sql As String = "Select * FROM TblCustomers WHERE CusID = @CusID"
			    Return conn.QuerySingleOrDefault(Of TblCustomers)(sql, New With { .CusID = clsTblCustomers.CusID })
			End Using
		End Function

		Public Function Add(ByVal clsTblCustomers As TblCustomers) As Boolean
			Dim RowsAffected As Integer=0
			Using conn As New SqlConnection(ConnectionString)
			    Dim sql As String = "INSERT INTO TblCustomers (CusName, CityID, Address, Contacts) VALUES (@CusName, @CityID, @Address, @Contacts)" 
			    RowsAffected = conn.Execute(sql, New With { .CusName =  clsTblCustomers.CusName, .CityID =  clsTblCustomers.CityID, .Address =  clsTblCustomers.Address, .Contacts =  clsTblCustomers.Contacts })
			    Return RowsAffected > 0
			End Using
		End Function

		Public Function Update(oldTblCustomers As TblCustomers, newTblCustomers As TblCustomers) As Boolean
			Using conn As New SqlConnection(ConnectionString)
			    Dim parameters = New With { 
					.NewCusName = newTblCustomers.CusName, .OldCusName = oldTblCustomers.CusName, .NewCityID = newTblCustomers.CityID, .OldCityID = oldTblCustomers.CityID, .NewAddress = newTblCustomers.Address, .OldAddress = oldTblCustomers.Address, .NewContacts = newTblCustomers.Contacts, .OldContacts = oldTblCustomers.Contacts
										  }
			    Dim affectedRows As Integer = conn.Execute("UPDATE [TblCustomers] SET [CusName] = @NewCusName, [CityID] = @NewCityID, [Address] = @NewAddress, [Contacts] = @NewContacts WHERE [CusName] = @OldCusName AND [CityID] = @OldCityID AND [Address] = @OldAddress AND [Contacts] = @OldContacts", parameters)
			    Return affectedRows > 0
			End Using
		End Function

		Public Function Delete(ByVal clsTblCustomers As TblCustomers) As Boolean
			Dim deleteStatement As String =
			"DELETE FROM [TblCustomers] 
			WHERE CusID = @CusID"
			Using connection As SqlConnection = DentistXData.GetConnection()
			    connection.Open()
			    Dim affectedRows As Integer = connection.Execute(deleteStatement, New With { .CusID = clsTblCustomers.CusID })
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

		Public Function GetTblSalesHeader(ByVal clsTblCustomers As TblCustomers ) As IEnumerable(Of TblSalesHeader)
			Using conn As New SqlConnection(ConnectionString)
				conn.Open()
				Dim query As String = "SELECT * FROM [TblSalesHeader] WHERE [CusID] = @CusID"
				Return conn.Query(Of TblSalesHeader)(query, New With { .CusID= clsTblCustomers.CusID })
			End Using
		End Function

	End Class
