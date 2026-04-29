Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections
Imports System.Configuration
Imports Dapper

	Public Class SecretariesDATA

		Private ConnectionString As String=DentistXDATA.GetConnection.connectionString



		Public Function SelectAll() As IEnumerable(Of Secretaries)
			Using conn As New SqlConnection(ConnectionString)
			    conn.Open()
			    Return conn.Query(Of Secretaries)("SELECT * FROM Secretaries")
			End Using
		End Function


	Public Function Select_Record(ByVal clsSecretaries As Secretaries) As Secretaries
			Using conn As New SqlConnection(ConnectionString)
			    Dim sql As String = "Select * FROM Secretaries WHERE SecID = @SecID"
			    Return conn.QuerySingleOrDefault(Of Secretaries)(sql, New With { .SecID = clsSecretaries.SecID })
			End Using
		End Function
	Public Function GetSecretaryById(ByVal SecID As Integer) As Secretaries
		Using conn As New SqlConnection(ConnectionString)
			Dim sql As String = "Select * FROM Secretaries WHERE SecID = @SecID"
			Return conn.QuerySingleOrDefault(Of Secretaries)(sql, New With {.SecID = SecID})
		End Using
	End Function
	Public Function GetSecretaryByName(ByVal SecName As String) As Integer
		Using conn As New SqlConnection(ConnectionString)
			Dim sql As String = "Select SecID FROM Secretaries WHERE SecName = @SecName"
			Return conn.QuerySingleOrDefault(Of Integer)(sql, New With {.SecName = SecName})
		End Using
	End Function
	Public Function Add(ByVal clsSecretaries As Secretaries) As Boolean
			Dim RowsAffected As Integer=0
			Using conn As New SqlConnection(ConnectionString)
			    Dim sql As String = "INSERT INTO Secretaries (SecName, SecAdres, SecPhone, SecMobile, SecColor, WhatsAppPrefix, WhatsApp) VALUES (@SecName, @SecAdres, @SecPhone, @SecMobile, @SecColor, @WhatsAppPrefix, @WhatsApp)"
			    RowsAffected = conn.Execute(sql, New With {
 .SecName = clsSecretaries.SecName, .SecAdres = clsSecretaries.SecAdres, .SecPhone = clsSecretaries.SecPhone, .SecMobile = clsSecretaries.SecMobile, .SecColor = clsSecretaries.SecColor,
			        .WhatsAppPrefix = clsSecretaries.WhatsAppPrefix, .WhatsApp = clsSecretaries.WhatsApp
			    })
			    Return RowsAffected > 0
			End Using
		End Function

		Public Function Update(oldSecretaries As Secretaries, newSecretaries As Secretaries) As Boolean
			Using conn As New SqlConnection(ConnectionString)
			    Dim sql = "UPDATE [Secretaries] SET [SecName]=@SecName, [SecAdres]=@SecAdres, [SecPhone]=@SecPhone, [SecMobile]=@SecMobile, [SecColor]=@SecColor, [WhatsAppPrefix]=@WhatsAppPrefix, [WhatsApp]=@WhatsApp WHERE [SecID]=@SecID"
			    Dim affectedRows As Integer = conn.Execute(sql, New With {
			        .SecName = newSecretaries.SecName, .SecAdres = newSecretaries.SecAdres, .SecPhone = newSecretaries.SecPhone, .SecMobile = newSecretaries.SecMobile, .SecColor = newSecretaries.SecColor,
			        .WhatsAppPrefix = newSecretaries.WhatsAppPrefix, .WhatsApp = newSecretaries.WhatsApp,
			        .SecID = oldSecretaries.SecID
			    })
			    Return affectedRows > 0
			End Using
		End Function

		Public Function Delete(ByVal clsSecretaries As Secretaries) As Boolean
			Dim deleteStatement As String =
			"DELETE FROM [Secretaries] 
			WHERE SecID = @SecID"
			Using connection As SqlConnection = DentistXData.GetConnection()
			    connection.Open()
			    Dim affectedRows As Integer = connection.Execute(deleteStatement, New With { .SecID = clsSecretaries.SecID })
			    Return affectedRows > 0
			End Using
		End Function


'Methods to get parents and childs
		Public Function GetUSERS(ByVal clsSecretaries As Secretaries ) As IEnumerable(Of USERS)
			Using conn As New SqlConnection(ConnectionString)
				conn.Open()
				Dim query As String = "SELECT * FROM [USERS] WHERE [SecID] = @SecID"
				Return conn.Query(Of USERS)(query, New With { .SecID= clsSecretaries.SecID })
			End Using
		End Function

	End Class
