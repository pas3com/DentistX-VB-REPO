Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections
Imports System.Configuration
Imports Dapper

	Public Class VendorsDATA

		Private ConnectionString As String=DentistXDATA.GetConnection.connectionString



		Public Function SelectAll() As IEnumerable(Of Vendors)
			Using conn As New SqlConnection(ConnectionString)
			    conn.Open()
			    Return conn.Query(Of Vendors)("SELECT * FROM Vendors")
			End Using
		End Function
		

		Public Function Select_Record(ByVal clsVendors As Vendors) As Vendors
			Using conn As New SqlConnection(ConnectionString)
			    Dim sql As String = "Select * FROM Vendors WHERE VendID = @VendID"
			    Return conn.QuerySingleOrDefault(Of Vendors)(sql, New With { .VendID = clsVendors.VendID })
			End Using
		End Function

		Public Function Add(ByVal clsVendors As Vendors) As Boolean
			Dim RowsAffected As Integer=0
			Using conn As New SqlConnection(ConnectionString)
			    Dim sql As String = "INSERT INTO Vendors (VendName, CityID, VendAddress, Contacts) VALUES (@VendName, @CityID, @VendAddress, @Contacts)" 
			    RowsAffected = conn.Execute(sql, New With { .VendName =  clsVendors.VendName, .CityID =  clsVendors.CityID, .VendAddress =  clsVendors.VendAddress, .Contacts =  clsVendors.Contacts })
			    Return RowsAffected > 0
			End Using
		End Function

		Public Function Update(oldVendors As Vendors, newVendors As Vendors) As Boolean
			Using conn As New SqlConnection(ConnectionString)
			    Dim parameters = New With { 
					.NewVendName = newVendors.VendName, .OldVendName = oldVendors.VendName, .NewCityID = newVendors.CityID, .OldCityID = oldVendors.CityID, .NewVendAddress = newVendors.VendAddress, .OldVendAddress = oldVendors.VendAddress, .NewContacts = newVendors.Contacts, .OldContacts = oldVendors.Contacts
										  }
			    Dim affectedRows As Integer = conn.Execute("UPDATE [Vendors] SET [VendName] = @NewVendName, [CityID] = @NewCityID, [VendAddress] = @NewVendAddress, [Contacts] = @NewContacts WHERE [VendName] = @OldVendName AND [CityID] = @OldCityID AND [VendAddress] = @OldVendAddress AND [Contacts] = @OldContacts", parameters)
			    Return affectedRows > 0
			End Using
		End Function

		Public Function Delete(ByVal clsVendors As Vendors) As Boolean
			Dim deleteStatement As String =
			"DELETE FROM [Vendors] 
			WHERE VendID = @VendID"
			Using connection As SqlConnection = DentistXData.GetConnection()
			    connection.Open()
			    Dim affectedRows As Integer = connection.Execute(deleteStatement, New With { .VendID = clsVendors.VendID })
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

		Public Function GetVendorPays(ByVal clsVendors As Vendors ) As IEnumerable(Of VendorPays)
			Using conn As New SqlConnection(ConnectionString)
				conn.Open()
				Dim query As String = "SELECT * FROM [VendorPays] WHERE [VendID] = @VendID"
				Return conn.Query(Of VendorPays)(query, New With { .VendID= clsVendors.VendID })
			End Using
		End Function

		Public Function GetVendorSales(ByVal clsVendors As Vendors ) As IEnumerable(Of VendorSales)
			Using conn As New SqlConnection(ConnectionString)
				conn.Open()
				Dim query As String = "SELECT * FROM [VendorSales] WHERE [VendID] = @VendID"
				Return conn.Query(Of VendorSales)(query, New With { .VendID= clsVendors.VendID })
			End Using
		End Function

	End Class
