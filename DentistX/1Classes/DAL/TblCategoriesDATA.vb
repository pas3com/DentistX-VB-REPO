Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections
Imports System.Configuration
Imports Dapper

	Public Class TblCategoriesDATA

		Private ConnectionString As String=DentistXDATA.GetConnection.connectionString



		Public Function SelectAll() As IEnumerable(Of TblCategories)
			Using conn As New SqlConnection(ConnectionString)
			    conn.Open()
			    Return conn.Query(Of TblCategories)("SELECT * FROM TblCategories")
			End Using
		End Function
		

		Public Function Select_Record(ByVal clsTblCategories As TblCategories) As TblCategories
			Using conn As New SqlConnection(ConnectionString)
			    Dim sql As String = "Select * FROM TblCategories WHERE CategoryID = @CategoryID"
			    Return conn.QuerySingleOrDefault(Of TblCategories)(sql, New With { .CategoryID = clsTblCategories.CategoryID })
			End Using
		End Function

		Public Function Add(ByVal clsTblCategories As TblCategories) As Boolean
			Dim RowsAffected As Integer=0
			Using conn As New SqlConnection(ConnectionString)
			    Dim sql As String = "INSERT INTO TblCategories (CategoryName, ParentCategory) VALUES (@CategoryName, @ParentCategory)" 
			    RowsAffected = conn.Execute(sql, New With { .CategoryName =  clsTblCategories.CategoryName, .ParentCategory =  clsTblCategories.ParentCategory })
			    Return RowsAffected > 0
			End Using
		End Function

		Public Function Update(oldTblCategories As TblCategories, newTblCategories As TblCategories) As Boolean
			Using conn As New SqlConnection(ConnectionString)
			    Dim parameters = New With { 
					.NewCategoryName = newTblCategories.CategoryName, .OldCategoryName = oldTblCategories.CategoryName, .NewParentCategory = newTblCategories.ParentCategory, .OldParentCategory = oldTblCategories.ParentCategory
										  }
			    Dim affectedRows As Integer = conn.Execute("UPDATE [TblCategories] SET [CategoryName] = @NewCategoryName, [ParentCategory] = @NewParentCategory WHERE [CategoryName] = @OldCategoryName AND [ParentCategory] = @OldParentCategory", parameters)
			    Return affectedRows > 0
			End Using
		End Function

		Public Function Delete(ByVal clsTblCategories As TblCategories) As Boolean
			Dim deleteStatement As String =
			"DELETE FROM [TblCategories] 
			WHERE CategoryID = @CategoryID"
			Using connection As SqlConnection = DentistXData.GetConnection()
			    connection.Open()
			    Dim affectedRows As Integer = connection.Execute(deleteStatement, New With { .CategoryID = clsTblCategories.CategoryID })
			    Return affectedRows > 0
			End Using
		End Function


'Methods to get parents and childs
		Public Function GetTblItems(ByVal clsTblCategories As TblCategories ) As IEnumerable(Of TblItems)
			Using conn As New SqlConnection(ConnectionString)
				conn.Open()
				Dim query As String = "SELECT * FROM [TblItems] WHERE [CatID] = @CatID"
				Return conn.Query(Of TblItems)(query, New With { .CatID= clsTblCategories.CategoryID })
			End Using
		End Function

	End Class
