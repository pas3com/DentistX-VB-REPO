Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections
Imports System.Configuration
Imports Dapper

	Public Class ShapesDATA

		Private ConnectionString As String=DentistXDATA.GetConnection.ConnectionString



	Public Shared Function SelectAll() As IEnumerable(Of Shapes)
		Using conn As New SqlConnection(DentistXDATA.GetConnection.ConnectionString)
			conn.Open()
			Return conn.Query(Of Shapes)("SELECT * FROM Shapes")
		End Using
	End Function


	Public Function Select_Record(ByVal clsShapes As Shapes) As Shapes
			Using conn As New SqlConnection(ConnectionString)
			    Dim sql As String = "Select * FROM Shapes WHERE ShapeID = @ShapeID"
			    Return conn.QuerySingleOrDefault(Of Shapes)(sql, New With { .ShapeID = clsShapes.ShapeID })
			End Using
		End Function

		Public Function Add(ByVal clsShapes As Shapes) As Boolean
			Dim RowsAffected As Integer=0
			Using conn As New SqlConnection(ConnectionString)
			    Dim sql As String = "INSERT INTO Shapes (ShapeID, ShapeName, ShapeDetail, OutID, TopID, INID, ShapeColor) VALUES (@ShapeID, @ShapeName, @ShapeDetail, @OutID, @TopID, @INID, @ShapeColor)" 
			    RowsAffected = conn.Execute(sql, New With { .ShapeID =  clsShapes.ShapeID, .ShapeName =  clsShapes.ShapeName, .ShapeDetail =  clsShapes.ShapeDetail, .OutID =  clsShapes.OutID, .TopID =  clsShapes.TopID, .INID =  clsShapes.INID, .ShapeColor =  clsShapes.ShapeColor })
			    Return RowsAffected > 0
			End Using
		End Function

		Public Function Update(oldShapes As Shapes, newShapes As Shapes) As Boolean
			Using conn As New SqlConnection(ConnectionString)
			    Dim parameters = New With { 
					.NewShapeID = newShapes.ShapeID, .OldShapeID = oldShapes.ShapeID, .NewShapeName = newShapes.ShapeName, .OldShapeName = oldShapes.ShapeName, .NewShapeDetail = newShapes.ShapeDetail, .OldShapeDetail = oldShapes.ShapeDetail, .NewOutID = newShapes.OutID, .OldOutID = oldShapes.OutID, .NewTopID = newShapes.TopID, .OldTopID = oldShapes.TopID, .NewINID = newShapes.INID, .OldINID = oldShapes.INID, .NewShapeColor = newShapes.ShapeColor, .OldShapeColor = oldShapes.ShapeColor
										  }
			    Dim affectedRows As Integer = conn.Execute("UPDATE [Shapes] SET [ShapeID] = @NewShapeID, [ShapeName] = @NewShapeName, [ShapeDetail] = @NewShapeDetail, [OutID] = @NewOutID, [TopID] = @NewTopID, [INID] = @NewINID, [ShapeColor] = @NewShapeColor WHERE [ShapeID] = @OldShapeID AND [ShapeName] = @OldShapeName AND [ShapeDetail] = @OldShapeDetail AND [OutID] = @OldOutID AND [TopID] = @OldTopID AND [INID] = @OldINID AND [ShapeColor] = @OldShapeColor", parameters)
			    Return affectedRows > 0
			End Using
		End Function

		Public Function Delete(ByVal clsShapes As Shapes) As Boolean
			Dim deleteStatement As String =
			"DELETE FROM [Shapes] 
			WHERE ShapeID = @ShapeID"
			Using connection As SqlConnection = DentistXData.GetConnection()
			    connection.Open()
			    Dim affectedRows As Integer = connection.Execute(deleteStatement, New With { .ShapeID = clsShapes.ShapeID })
			    Return affectedRows > 0
			End Using
		End Function


'Methods to get parents and childs
	End Class
