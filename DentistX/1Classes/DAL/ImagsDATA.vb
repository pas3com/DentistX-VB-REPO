Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections
Imports System.Configuration
Imports Dapper

	Public Class ImagsDATA

		Private ConnectionString As String=DentistXDATA.GetConnection.connectionString



		Public Function SelectAll() As IEnumerable(Of Imags)
			Using conn As New SqlConnection(ConnectionString)
			    conn.Open()
			    Return conn.Query(Of Imags)("SELECT * FROM Imags")
			End Using
		End Function
		

		Public Function Select_Record(ByVal clsImags As Imags) As Imags
			Using conn As New SqlConnection(ConnectionString)
			    Dim sql As String = "Select * FROM Imags WHERE ImageID = @ImageID"
			    Return conn.QuerySingleOrDefault(Of Imags)(sql, New With { .ImageID = clsImags.ImageID })
			End Using
		End Function

		Public Function Add(ByVal clsImags As Imags) As Boolean
			Dim RowsAffected As Integer=0
			Using conn As New SqlConnection(ConnectionString)
			    Dim sql As String = "INSERT INTO Imags (IMG, Height, Width, Sze, DatePictureTaken, EquipmentMaker, EquipmentModel, Thumbnail, DateCreated, DateModified) VALUES (@IMG, @Height, @Width, @Sze, @DatePictureTaken, @EquipmentMaker, @EquipmentModel, @Thumbnail, @DateCreated, @DateModified)" 
			    RowsAffected = conn.Execute(sql, New With { .IMG =  clsImags.IMG, .Height =  clsImags.Height, .Width =  clsImags.Width, .Sze =  clsImags.Sze, .DatePictureTaken =  clsImags.DatePictureTaken, .EquipmentMaker =  clsImags.EquipmentMaker, .EquipmentModel =  clsImags.EquipmentModel, .Thumbnail =  clsImags.Thumbnail, .DateCreated =  clsImags.DateCreated, .DateModified =  clsImags.DateModified })
			    Return RowsAffected > 0
			End Using
		End Function

		Public Function Update(oldImags As Imags, newImags As Imags) As Boolean
			Using conn As New SqlConnection(ConnectionString)
			    Dim parameters = New With { 
					.NewIMG = newImags.IMG, .OldIMG = oldImags.IMG, .NewHeight = newImags.Height, .OldHeight = oldImags.Height, .NewWidth = newImags.Width, .OldWidth = oldImags.Width, .NewSze = newImags.Sze, .OldSze = oldImags.Sze, .NewDatePictureTaken = newImags.DatePictureTaken, .OldDatePictureTaken = oldImags.DatePictureTaken, .NewEquipmentMaker = newImags.EquipmentMaker, .OldEquipmentMaker = oldImags.EquipmentMaker, .NewEquipmentModel = newImags.EquipmentModel, .OldEquipmentModel = oldImags.EquipmentModel, .NewThumbnail = newImags.Thumbnail, .OldThumbnail = oldImags.Thumbnail, .NewDateCreated = newImags.DateCreated, .OldDateCreated = oldImags.DateCreated, .NewDateModified = newImags.DateModified, .OldDateModified = oldImags.DateModified
										  }
			    Dim affectedRows As Integer = conn.Execute("UPDATE [Imags] SET [IMG] = @NewIMG, [Height] = @NewHeight, [Width] = @NewWidth, [Sze] = @NewSze, [DatePictureTaken] = @NewDatePictureTaken, [EquipmentMaker] = @NewEquipmentMaker, [EquipmentModel] = @NewEquipmentModel, [Thumbnail] = @NewThumbnail, [DateCreated] = @NewDateCreated, [DateModified] = @NewDateModified WHERE [IMG] = @OldIMG AND [Height] = @OldHeight AND [Width] = @OldWidth AND [Sze] = @OldSze AND [DatePictureTaken] = @OldDatePictureTaken AND [EquipmentMaker] = @OldEquipmentMaker AND [EquipmentModel] = @OldEquipmentModel AND [Thumbnail] = @OldThumbnail AND [DateCreated] = @OldDateCreated AND [DateModified] = @OldDateModified", parameters)
			    Return affectedRows > 0
			End Using
		End Function

		Public Function Delete(ByVal clsImags As Imags) As Boolean
			Dim deleteStatement As String =
			"DELETE FROM [Imags] 
			WHERE ImageID = @ImageID"
			Using connection As SqlConnection = DentistXData.GetConnection()
			    connection.Open()
			    Dim affectedRows As Integer = connection.Execute(deleteStatement, New With { .ImageID = clsImags.ImageID })
			    Return affectedRows > 0
			End Using
		End Function


'Methods to get parents and childs
	End Class
