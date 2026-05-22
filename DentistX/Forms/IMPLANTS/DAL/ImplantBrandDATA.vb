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

		Public Function CountByBrandName(brandName As String, Optional excludeBrandId As Integer? = Nothing) As Integer
			If String.IsNullOrWhiteSpace(brandName) Then Return 0
			Dim trimmed = brandName.Trim()
			Using conn As New SqlConnection(ConnectionString)
				conn.Open()
				Const sql As String =
					"SELECT COUNT(*) FROM ImplantBrand WHERE LTRIM(RTRIM(BrandName)) = @BrandName AND (@ExcludeBrandId IS NULL OR BrandID <> @ExcludeBrandId)"
				Return CInt(conn.ExecuteScalar(Of Integer)(sql, New With {.BrandName = trimmed, .ExcludeBrandId = excludeBrandId}))
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
			    Dim affectedRows As Integer = conn.Execute(
					"UPDATE [ImplantBrand] SET [BrandName] = @NewBrandName WHERE [BrandID] = @BrandID",
					New With {.NewBrandName = newImplantBrand.BrandName, .BrandID = oldImplantBrand.BrandID})
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
