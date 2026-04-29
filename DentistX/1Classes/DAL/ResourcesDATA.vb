Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections
Imports System.Configuration
Imports Dapper

	Public Class ResourcesDATA

		Private ConnectionString As String=DentistXDATA.GetConnection.connectionString



		Public Function SelectAll() As IEnumerable(Of Resources)
			Using conn As New SqlConnection(ConnectionString)
			    conn.Open()
			    Return conn.Query(Of Resources)("SELECT * FROM Resources")
			End Using
		End Function
		

		Public Function Select_Record(ByVal clsResources As Resources) As Resources
			Using conn As New SqlConnection(ConnectionString)
			    Dim sql As String = "Select * FROM Resources WHERE UniqueID = @UniqueID"
			    Return conn.QuerySingleOrDefault(Of Resources)(sql, New With { .UniqueID = clsResources.UniqueID })
			End Using
		End Function

		Public Function Add(ByVal clsResources As Resources) As Boolean
			Dim RowsAffected As Integer=0
			Using conn As New SqlConnection(ConnectionString)
			    Dim sql As String = "INSERT INTO Resources (ResourceID, ResourceName, Color, Image, CustomField1) VALUES (@ResourceID, @ResourceName, @Color, @Image, @CustomField1)" 
			    RowsAffected = conn.Execute(sql, New With { .ResourceID =  clsResources.ResourceID, .ResourceName =  clsResources.ResourceName, .Color =  clsResources.Color, .Image =  clsResources.Image, .CustomField1 =  clsResources.CustomField1 })
			    Return RowsAffected > 0
			End Using
		End Function

		Public Function Update(oldResources As Resources, newResources As Resources) As Boolean
			Using conn As New SqlConnection(ConnectionString)
			    Dim parameters = New With { 
					.NewResourceID = newResources.ResourceID, .OldResourceID = oldResources.ResourceID, .NewResourceName = newResources.ResourceName, .OldResourceName = oldResources.ResourceName, .NewColor = newResources.Color, .OldColor = oldResources.Color, .NewImage = newResources.Image, .OldImage = oldResources.Image, .NewCustomField1 = newResources.CustomField1, .OldCustomField1 = oldResources.CustomField1
										  }
			    Dim affectedRows As Integer = conn.Execute("UPDATE [Resources] SET [ResourceID] = @NewResourceID, [ResourceName] = @NewResourceName, [Color] = @NewColor, [Image] = @NewImage, [CustomField1] = @NewCustomField1 WHERE [ResourceID] = @OldResourceID AND [ResourceName] = @OldResourceName AND [Color] = @OldColor AND [Image] = @OldImage AND [CustomField1] = @OldCustomField1", parameters)
			    Return affectedRows > 0
			End Using
		End Function

		Public Function Delete(ByVal clsResources As Resources) As Boolean
			Dim deleteStatement As String =
			"DELETE FROM [Resources] 
			WHERE UniqueID = @UniqueID"
			Using connection As SqlConnection = DentistXData.GetConnection()
			    connection.Open()
			    Dim affectedRows As Integer = connection.Execute(deleteStatement, New With { .UniqueID = clsResources.UniqueID })
			    Return affectedRows > 0
			End Using
		End Function


'Methods to get parents and childs
	End Class
