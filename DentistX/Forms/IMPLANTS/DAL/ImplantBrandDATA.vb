Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections
Imports System.Configuration
Imports Dapper

	Public Class ImplantBrandDATA

		Private ConnectionString As String=DentistXDATA.GetConnection.connectionString



		Public Function SelectAll() As IEnumerable(Of ImplantBrand)
			Using conn As New SqlConnection(ConnectionString)
			    conn.Open()
			    Return conn.Query(Of ImplantBrand)("SELECT * FROM ImplantBrand")
			End Using
		End Function
		

		Public Function Select_Record(ByVal clsImplantBrand As ImplantBrand) As ImplantBrand
			Using conn As New SqlConnection(ConnectionString)
			    Dim sql As String = "Select * FROM ImplantBrand WHERE BrandID = @BrandID"
			    Return conn.QuerySingleOrDefault(Of ImplantBrand)(sql, New With { .BrandID = clsImplantBrand.BrandID })
			End Using
		End Function

		Public Function Add(ByVal clsImplantBrand As ImplantBrand) As Boolean
			Dim RowsAffected As Integer=0
			Using conn As New SqlConnection(ConnectionString)
			    Dim sql As String = "INSERT INTO ImplantBrand (BrandName) VALUES (@BrandName)" 
			    RowsAffected = conn.Execute(sql, New With { .BrandName =  clsImplantBrand.BrandName })
			    Return RowsAffected > 0
			End Using
		End Function

		Public Function Update(oldImplantBrand As ImplantBrand, newImplantBrand As ImplantBrand) As Boolean
			Using conn As New SqlConnection(ConnectionString)
			    Dim parameters = New With { 
					.NewBrandName = newImplantBrand.BrandName, .OldBrandName = oldImplantBrand.BrandName
										  }
			    Dim affectedRows As Integer = conn.Execute("UPDATE [ImplantBrand] SET [BrandName] = @NewBrandName WHERE [BrandName] = @OldBrandName", parameters)
			    Return affectedRows > 0
			End Using
		End Function

		Public Function Delete(ByVal clsImplantBrand As ImplantBrand) As Boolean
			Dim deleteStatement As String =
			"DELETE FROM [ImplantBrand] 
			WHERE BrandID = @BrandID"
			Using connection As SqlConnection = DentistXData.GetConnection()
			    connection.Open()
			    Dim affectedRows As Integer = connection.Execute(deleteStatement, New With { .BrandID = clsImplantBrand.BrandID })
			    Return affectedRows > 0
			End Using
		End Function


'Methods to get parents and childs
	End Class
