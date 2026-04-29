Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections
Imports System.Configuration
Imports Dapper

	Public Class MedicineDozeDATA

		Private ConnectionString As String=DentistXDATA.GetConnection.connectionString



		Public Function SelectAll() As IEnumerable(Of MedicineDoze)
			Using conn As New SqlConnection(ConnectionString)
			    conn.Open()
			    Return conn.Query(Of MedicineDoze)("SELECT * FROM MedicineDoze")
			End Using
		End Function
		

		Public Function Select_Record(ByVal clsMedicineDoze As MedicineDoze) As MedicineDoze
			Using conn As New SqlConnection(ConnectionString)
			    Dim sql As String = "Select * FROM MedicineDoze WHERE DozeID = @DozeID"
			    Return conn.QuerySingleOrDefault(Of MedicineDoze)(sql, New With { .DozeID = clsMedicineDoze.DozeID })
			End Using
		End Function
	Public Function SelectByDozeID(DozeID As Integer) As IEnumerable(Of MedicineDoze)
		Using conn As New SqlConnection(ConnectionString)
			Return conn.Query(Of MedicineDoze)("SELECT * FROM MedicineDoze WHERE DozeID = @DozeID", New With {.DozeID = DozeID})
		End Using
	End Function
	Public Function Add(ByVal clsMedicineDoze As MedicineDoze) As Boolean
			Dim RowsAffected As Integer=0
			Using conn As New SqlConnection(ConnectionString)
			    Dim sql As String = "INSERT INTO MedicineDoze (ShapeID, Doze) VALUES (@ShapeID, @Doze)" 
			    RowsAffected = conn.Execute(sql, New With { .ShapeID =  clsMedicineDoze.ShapeID, .Doze =  clsMedicineDoze.Doze })
			    Return RowsAffected > 0
			End Using
		End Function

		Public Function Update(oldMedicineDoze As MedicineDoze, newMedicineDoze As MedicineDoze) As Boolean
			Using conn As New SqlConnection(ConnectionString)
			    Dim parameters = New With { 
					.NewShapeID = newMedicineDoze.ShapeID, .OldShapeID = oldMedicineDoze.ShapeID, .NewDoze = newMedicineDoze.Doze, .OldDoze = oldMedicineDoze.Doze
										  }
			    Dim affectedRows As Integer = conn.Execute("UPDATE [MedicineDoze] SET [ShapeID] = @NewShapeID, [Doze] = @NewDoze WHERE [ShapeID] = @OldShapeID AND [Doze] = @OldDoze", parameters)
			    Return affectedRows > 0
			End Using
		End Function

		Public Function Delete(ByVal clsMedicineDoze As MedicineDoze) As Boolean
			Dim deleteStatement As String =
			"DELETE FROM [MedicineDoze] 
			WHERE DozeID = @DozeID"
			Using connection As SqlConnection = DentistXData.GetConnection()
			    connection.Open()
			    Dim affectedRows As Integer = connection.Execute(deleteStatement, New With { .DozeID = clsMedicineDoze.DozeID })
			    Return affectedRows > 0
			End Using
		End Function


'Methods to get parents and childs
		Public Function GetMedicineShape(ByVal ShapeID As Integer) As MedicineShape
		Dim parent As MedicineShape = Nothing
		Using conn As New SqlConnection(ConnectionString)
			Dim query As String = "SELECT * FROM [MedicineShape] WHERE [ShapeID] = @ShapeID"
			Try
				conn.Open()
				parent = conn.QuerySingleOrDefault(Of MedicineShape)(query, New With {.ShapeID = ShapeID})
			Catch ex As Exception
				' Handle exceptions
			Finally
				If conn.State = ConnectionState.Open Then conn.Close()
			End Try
		End Using
		Return parent
		End Function

	End Class
