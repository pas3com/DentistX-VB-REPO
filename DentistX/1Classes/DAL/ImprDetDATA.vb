Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections
Imports System.Configuration
Imports Dapper

	Public Class ImprDetDATA

		Private ConnectionString As String=DentistXDATA.GetConnection.connectionString



		Public Function SelectAll() As IEnumerable(Of ImprDet)
			Using conn As New SqlConnection(ConnectionString)
			    conn.Open()
			Return conn.Query(Of ImprDet)("SELECT [ImpDetID]
											,[dbo].[Impression].ImprType
											,[dbo].[ImprDet].[imprID]
											,[ImprDetail]
										FROM [dbo].[ImprDet] inner join [dbo].[Impression] on [dbo].[ImprDet].[imprID] = [dbo].[Impression].[imprID]
									")
		End Using
		End Function
		

		Public Function Select_Record(ByVal clsImprDet As ImprDet) As ImprDet
			Using conn As New SqlConnection(ConnectionString)
			Dim sql As String = "SELECT [ImpDetID]
											,[dbo].[Impression].ImprType
											,[dbo].[ImprDet].[imprID]
											,[ImprDetail]
										FROM [dbo].[ImprDet] inner join [dbo].[Impression] on [dbo].[ImprDet].[imprID] = [dbo].[Impression].[imprID]
								 WHERE ImpDetID = @ImpDetID"
			Return conn.QuerySingleOrDefault(Of ImprDet)(sql, New With { .ImpDetID = clsImprDet.ImpDetID })
			End Using
		End Function

		Public Function Add(ByVal clsImprDet As ImprDet) As Boolean
			Dim RowsAffected As Integer=0
			Using conn As New SqlConnection(ConnectionString)
			    Dim sql As String = "INSERT INTO ImprDet (imprID, ImprDetail) VALUES (@imprID, @ImprDetail)" 
			    RowsAffected = conn.Execute(sql, New With { .imprID =  clsImprDet.imprID, .ImprDetail =  clsImprDet.ImprDetail })
			    Return RowsAffected > 0
			End Using
		End Function

		Public Function Update(oldImprDet As ImprDet, newImprDet As ImprDet) As Boolean
			Using conn As New SqlConnection(ConnectionString)
			    Dim parameters = New With { 
					.NewimprID = newImprDet.imprID, .OldimprID = oldImprDet.imprID, .NewImprDetail = newImprDet.ImprDetail, .OldImprDetail = oldImprDet.ImprDetail
										  }
			    Dim affectedRows As Integer = conn.Execute("UPDATE [ImprDet] SET [imprID] = @NewimprID, [ImprDetail] = @NewImprDetail WHERE [imprID] = @OldimprID AND [ImprDetail] = @OldImprDetail", parameters)
			    Return affectedRows > 0
			End Using
		End Function

		Public Function Delete(ByVal clsImprDet As ImprDet) As Boolean
			Dim deleteStatement As String =
			"DELETE FROM [ImprDet] 
			WHERE ImpDetID = @ImpDetID"
			Using connection As SqlConnection = DentistXData.GetConnection()
			    connection.Open()
			    Dim affectedRows As Integer = connection.Execute(deleteStatement, New With { .ImpDetID = clsImprDet.ImpDetID })
			    Return affectedRows > 0
			End Using
		End Function


'Methods to get parents and childs
		Public Function GetImpression(ByVal ImprID As Integer) As Impression
		Dim parent As Impression = Nothing
		Using conn As New SqlConnection(ConnectionString)
			Dim query As String = "SELECT * FROM [Impression] WHERE [ImprID] = @ImprID"
			Try
				conn.Open()
				parent = conn.QuerySingleOrDefault(Of Impression)(query, New With {.ImprID = ImprID})
			Catch ex As Exception
				' Handle exceptions
			Finally
				If conn.State = ConnectionState.Open Then conn.Close()
			End Try
		End Using
		Return parent
		End Function

	End Class
