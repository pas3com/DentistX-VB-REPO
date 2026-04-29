Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections
Imports System.Configuration
Imports Dapper

	Public Class ImpressionDATA

		Private ConnectionString As String=DentistXDATA.GetConnection.connectionString



		Public Function SelectAll() As IEnumerable(Of Impression)
			Using conn As New SqlConnection(ConnectionString)
			    conn.Open()
			    Return conn.Query(Of Impression)("SELECT * FROM Impression")
			End Using
		End Function
		

		Public Function Select_Record(ByVal clsImpression As Impression) As Impression
			Using conn As New SqlConnection(ConnectionString)
			    Dim sql As String = "Select * FROM Impression WHERE ImprID = @ImprID"
			    Return conn.QuerySingleOrDefault(Of Impression)(sql, New With { .ImprID = clsImpression.ImprID })
			End Using
		End Function

		Public Function Add(ByVal clsImpression As Impression) As Boolean
			Dim RowsAffected As Integer=0
			Using conn As New SqlConnection(ConnectionString)
			    Dim sql As String = "INSERT INTO Impression (ImprType) VALUES (@ImprType)" 
			    RowsAffected = conn.Execute(sql, New With { .ImprType =  clsImpression.ImprType })
			    Return RowsAffected > 0
			End Using
		End Function

		Public Function Update(oldImpression As Impression, newImpression As Impression) As Boolean
			Using conn As New SqlConnection(ConnectionString)
			    Dim parameters = New With { 
					.NewImprType = newImpression.ImprType, .OldImprType = oldImpression.ImprType
										  }
			    Dim affectedRows As Integer = conn.Execute("UPDATE [Impression] SET [ImprType] = @NewImprType WHERE [ImprType] = @OldImprType", parameters)
			    Return affectedRows > 0
			End Using
		End Function

		Public Function Delete(ByVal clsImpression As Impression) As Boolean
			Dim deleteStatement As String =
			"DELETE FROM [Impression] 
			WHERE ImprID = @ImprID"
			Using connection As SqlConnection = DentistXData.GetConnection()
			    connection.Open()
			    Dim affectedRows As Integer = connection.Execute(deleteStatement, New With { .ImprID = clsImpression.ImprID })
			    Return affectedRows > 0
			End Using
		End Function


'Methods to get parents and childs
		Public Function GetImprDet(ByVal clsImpression As Impression ) As IEnumerable(Of ImprDet)
			Using conn As New SqlConnection(ConnectionString)
				conn.Open()
				Dim query As String = "SELECT * FROM [ImprDet] WHERE [imprID] = @imprID"
				Return conn.Query(Of ImprDet)(query, New With { .imprID= clsImpression.ImprID })
			End Using
		End Function

	End Class
