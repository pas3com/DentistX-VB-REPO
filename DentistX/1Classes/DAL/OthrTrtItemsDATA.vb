Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections
Imports System.Configuration
Imports Dapper

	Public Class OthrTrtItemsDATA

		Private ConnectionString As String=DentistXDATA.GetConnection.connectionString



		Public Function SelectAll() As IEnumerable(Of OthrTrtItems)
			Using conn As New SqlConnection(ConnectionString)
			    conn.Open()
			    Return conn.Query(Of OthrTrtItems)("SELECT * FROM OthrTrtItems")
			End Using
		End Function
		

		Public Function Select_Record(ByVal clsOthrTrtItems As OthrTrtItems) As OthrTrtItems
			Using conn As New SqlConnection(ConnectionString)
            Dim sql As String = "Select * FROM OthrTrtItems WHERE OTrtID=@OTrtID "
            Return conn.QuerySingleOrDefault(Of OthrTrtItems)(sql, New With {.OTrtID = clsOthrTrtItems.OTrtID})
        End Using
		End Function

		Public Function Add(ByVal clsOthrTrtItems As OthrTrtItems) As Boolean
			Dim RowsAffected As Integer=0
			Using conn As New SqlConnection(ConnectionString)
			    Dim sql As String = "INSERT INTO OthrTrtItems (TrtEng, TrtEngDetails, TrtAr, TrtArDetails) VALUES (@TrtEng, @TrtEngDetails, @TrtAr, @TrtArDetails)" 
			    RowsAffected = conn.Execute(sql, New With { .TrtEng =  clsOthrTrtItems.TrtEng, .TrtEngDetails =  clsOthrTrtItems.TrtEngDetails, .TrtAr =  clsOthrTrtItems.TrtAr, .TrtArDetails =  clsOthrTrtItems.TrtArDetails })
			    Return RowsAffected > 0
			End Using
		End Function

		Public Function Update(oldOthrTrtItems As OthrTrtItems, newOthrTrtItems As OthrTrtItems) As Boolean
			Using conn As New SqlConnection(ConnectionString)
            Dim parameters = New With {
                .oldOTrtID = oldOthrTrtItems.OTrtID, .NewTrtEng = newOthrTrtItems.TrtEng, .OldTrtEng = oldOthrTrtItems.TrtEng, .NewTrtEngDetails = newOthrTrtItems.TrtEngDetails, .OldTrtEngDetails = oldOthrTrtItems.TrtEngDetails, .NewTrtAr = newOthrTrtItems.TrtAr, .OldTrtAr = oldOthrTrtItems.TrtAr, .NewTrtArDetails = newOthrTrtItems.TrtArDetails, .OldTrtArDetails = oldOthrTrtItems.TrtArDetails
                                          }
            Dim affectedRows As Integer = conn.Execute("UPDATE [OthrTrtItems] SET [TrtEng] = @NewTrtEng, [TrtEngDetails] = @NewTrtEngDetails, [TrtAr] = @NewTrtAr, [TrtArDetails] = @NewTrtArDetails WHERE [OTrtID]=@OTrtID", parameters)
            Return affectedRows > 0
			End Using
		End Function

		Public Function Delete(ByVal clsOthrTrtItems As OthrTrtItems) As Boolean
			Dim deleteStatement As String =
			"DELETE FROM [OthrTrtItems] 
			WHERE OTrtID = @OTrtID"
			Using connection As SqlConnection = DentistXData.GetConnection()
			    connection.Open()
            Dim affectedRows As Integer = connection.Execute(deleteStatement, New With {.OTrtID = clsOthrTrtItems.OTrtID})
            Return affectedRows > 0
			End Using
		End Function


'Methods to get parents and childs
	End Class
