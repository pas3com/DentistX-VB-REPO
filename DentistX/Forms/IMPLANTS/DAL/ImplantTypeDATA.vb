Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections
Imports System.Configuration
Imports Dapper

	Public Class ImplantTypeDATA

		Private ConnectionString As String=DentistXDATA.GetConnection.connectionString



		Public Function SelectAll() As IEnumerable(Of ImplantType)
			Using conn As New SqlConnection(ConnectionString)
			    conn.Open()
			    Return conn.Query(Of ImplantType)("SELECT * FROM ImplantType")
			End Using
		End Function
		

		Public Function Select_Record(ByVal clsImplantType As ImplantType) As ImplantType
			Using conn As New SqlConnection(ConnectionString)
			    Dim sql As String = "Select * FROM ImplantType WHERE TypeID = @TypeID"
			    Return conn.QuerySingleOrDefault(Of ImplantType)(sql, New With { .TypeID = clsImplantType.TypeID })
			End Using
		End Function

		''' <summary>Count rows with the same logical name (trimmed). Optionally exclude one TypeID when editing.</summary>
		Public Function CountByTypeName(typeName As String, Optional excludeTypeId As Integer? = Nothing) As Integer
			If String.IsNullOrWhiteSpace(typeName) Then Return 0
			Dim trimmed = typeName.Trim()
			Using conn As New SqlConnection(ConnectionString)
				conn.Open()
				Const sql As String =
					"SELECT COUNT(*) FROM ImplantType WHERE LTRIM(RTRIM(TypeName)) = @TypeName AND (@ExcludeTypeId IS NULL OR TypeID <> @ExcludeTypeId)"
				Return CInt(conn.ExecuteScalar(Of Integer)(sql, New With {.TypeName = trimmed, .ExcludeTypeId = excludeTypeId}))
			End Using
		End Function

		Public Function Add(ByVal clsImplantType As ImplantType) As Boolean
			Dim RowsAffected As Integer=0
			Using conn As New SqlConnection(ConnectionString)
			    Dim sql As String = "INSERT INTO ImplantType (TypeName, IsSlim) VALUES (@TypeName, @IsSlim)" 
			    RowsAffected = conn.Execute(sql, New With { .TypeName =  clsImplantType.TypeName, .IsSlim =  clsImplantType.IsSlim })
			    Return RowsAffected > 0
			End Using
		End Function

		Public Function Update(oldImplantType As ImplantType, newImplantType As ImplantType) As Boolean
			Using conn As New SqlConnection(ConnectionString)
			    Dim affectedRows As Integer = conn.Execute(
					"UPDATE [ImplantType] SET [TypeName] = @NewTypeName, [IsSlim] = @NewIsSlim WHERE [TypeID] = @TypeID",
					New With {.NewTypeName = newImplantType.TypeName, .NewIsSlim = newImplantType.IsSlim, .TypeID = oldImplantType.TypeID})
			    Return affectedRows > 0
			End Using
		End Function

		Public Function Delete(ByVal clsImplantType As ImplantType) As Boolean
			Dim deleteStatement As String =
			"DELETE FROM [ImplantType] 
			WHERE TypeID = @TypeID"
			Using connection As SqlConnection = DentistXData.GetConnection()
			    connection.Open()
			    Dim affectedRows As Integer = connection.Execute(deleteStatement, New With { .TypeID = clsImplantType.TypeID })
			    Return affectedRows > 0
			End Using
		End Function


'Methods to get parents and childs
	End Class
