Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections
Imports System.Configuration
Imports Dapper

	Public Class MedicineShapeDATA

		Private ConnectionString As String=DentistXDATA.GetConnection.connectionString



		Public Function SelectAll() As IEnumerable(Of MedicineShape)
			Using conn As New SqlConnection(ConnectionString)
			    conn.Open()
			    Return conn.Query(Of MedicineShape)("SELECT * FROM MedicineShape")
			End Using
		End Function
		

		Public Function Select_Record(ByVal clsMedicineShape As MedicineShape) As MedicineShape
			Using conn As New SqlConnection(ConnectionString)
			    Dim sql As String = "Select * FROM MedicineShape WHERE ShapeID = @ShapeID"
			    Return conn.QuerySingleOrDefault(Of MedicineShape)(sql, New With { .ShapeID = clsMedicineShape.ShapeID })
			End Using
		End Function
	Public Function SelectByShapeID(ShapeID As Integer) As IEnumerable(Of MedicineShape)
		Using conn As New SqlConnection(ConnectionString)
			Return conn.Query(Of MedicineShape)("SELECT * FROM MedicineShape WHERE ShapeID = @ShapeID", New With {.ShapeID = ShapeID})
		End Using
	End Function
	Public Function Add(ByVal clsMedicineShape As MedicineShape) As Boolean
			Dim RowsAffected As Integer=0
			Using conn As New SqlConnection(ConnectionString)
			    Dim sql As String = "INSERT INTO MedicineShape (MedicineShape, MedicineItemID, ShapeInfo) VALUES (@MedicineShape, @MedicineItemID, @ShapeInfo)" 
			    RowsAffected = conn.Execute(sql, New With { .MedicineShape =  clsMedicineShape.MedicineShape, .MedicineItemID =  clsMedicineShape.MedicineItemID, .ShapeInfo =  clsMedicineShape.ShapeInfo })
			    Return RowsAffected > 0
			End Using
		End Function

		Public Function Update(oldMedicineShape As MedicineShape, newMedicineShape As MedicineShape) As Boolean
			Using conn As New SqlConnection(ConnectionString)
			    Dim parameters = New With { 
					.NewMedicineShape = newMedicineShape.MedicineShape, .OldMedicineShape = oldMedicineShape.MedicineShape, .NewMedicineItemID = newMedicineShape.MedicineItemID, .OldMedicineItemID = oldMedicineShape.MedicineItemID, .NewShapeInfo = newMedicineShape.ShapeInfo, .OldShapeInfo = oldMedicineShape.ShapeInfo
										  }
			    Dim affectedRows As Integer = conn.Execute("UPDATE [MedicineShape] SET [MedicineShape] = @NewMedicineShape, [MedicineItemID] = @NewMedicineItemID, [ShapeInfo] = @NewShapeInfo WHERE [MedicineShape] = @OldMedicineShape AND [MedicineItemID] = @OldMedicineItemID AND [ShapeInfo] = @OldShapeInfo", parameters)
			    Return affectedRows > 0
			End Using
		End Function

		Public Function Delete(ByVal clsMedicineShape As MedicineShape) As Boolean
			Dim deleteStatement As String =
			"DELETE FROM [MedicineShape] 
			WHERE ShapeID = @ShapeID"
			Using connection As SqlConnection = DentistXData.GetConnection()
			    connection.Open()
			    Dim affectedRows As Integer = connection.Execute(deleteStatement, New With { .ShapeID = clsMedicineShape.ShapeID })
			    Return affectedRows > 0
			End Using
		End Function


'Methods to get parents and childs
		Public Function GetMedicineItems(ByVal MedicineItemID As Integer) As MedicineItems
		Dim parent As MedicineItems = Nothing
		Using conn As New SqlConnection(ConnectionString)
			Dim query As String = "SELECT * FROM [MedicineItems] WHERE [MedicineItemID] = @MedicineItemID"
			Try
				conn.Open()
				parent = conn.QuerySingleOrDefault(Of MedicineItems)(query, New With {.MedicineItemID = MedicineItemID})
			Catch ex As Exception
				' Handle exceptions
			Finally
				If conn.State = ConnectionState.Open Then conn.Close()
			End Try
		End Using
		Return parent
		End Function

		Public Function GetMedicineDoze(ByVal clsMedicineShape As MedicineShape ) As IEnumerable(Of MedicineDoze)
			Using conn As New SqlConnection(ConnectionString)
				conn.Open()
				Dim query As String = "SELECT * FROM [MedicineDoze] WHERE [ShapeID] = @ShapeID"
				Return conn.Query(Of MedicineDoze)(query, New With { .ShapeID= clsMedicineShape.ShapeID })
			End Using
		End Function

	End Class
