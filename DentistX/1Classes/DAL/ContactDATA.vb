Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections
Imports System.Configuration
Imports Dapper

Public Class ContactDATA

    Private ConnectionString As String = DentistXDATA.GetConnection.ConnectionString



    Public Function SelectAll() As IEnumerable(Of Contact)
        Using conn As New SqlConnection(ConnectionString)
            conn.Open()
            Return conn.Query(Of Contact)("SELECT * FROM Contacts")
        End Using
    End Function


    Public Function Select_Record(ByVal clsContact As Contact) As Contact
        Using conn As New SqlConnection(ConnectionString)
            Dim sql As String = "Select * FROM Contacts WHERE ContactID = @ContactID"
            Return conn.QuerySingleOrDefault(Of Contact)(sql, New With {.ContactID = clsContact.ContactID})
        End Using
    End Function

    Public Function Add(ByVal clsContact As Contact) As Boolean
        Dim RowsAffected As Integer = 0
        Using conn As New SqlConnection(ConnectionString)
            Dim sql As String = "INSERT INTO Contacts (CName, Phone, Email, Notes, CreatedAt, WhatsAppPrefix, WhatsApp) VALUES (@CName, @Phone, @Email, @Notes, @CreatedAt, @WhatsAppPrefix, @WhatsApp)"
            RowsAffected = conn.Execute(sql, New With {
                .CName = clsContact.CName, .Phone = clsContact.Phone, .Email = clsContact.Email, .Notes = clsContact.Notes, .CreatedAt = clsContact.CreatedAt,
                .WhatsAppPrefix = clsContact.WhatsAppPrefix, .WhatsApp = clsContact.WhatsApp
            })
            Return RowsAffected > 0
        End Using
    End Function

    Public Function Update(oldContact As Contact, newContact As Contact) As Boolean
        Using conn As New SqlConnection(ConnectionString)
            Dim sql = "UPDATE [Contacts] SET [CName] = @CName, [Phone] = @Phone, [Email] = @Email, [Notes] = @Notes, [CreatedAt] = @CreatedAt, [WhatsAppPrefix] = @WhatsAppPrefix, [WhatsApp] = @WhatsApp WHERE [ContactID] = @ContactID"
            Dim affectedRows As Integer = conn.Execute(sql, New With {
                .CName = newContact.CName, .Phone = newContact.Phone, .Email = newContact.Email, .Notes = newContact.Notes, .CreatedAt = newContact.CreatedAt,
                .WhatsAppPrefix = newContact.WhatsAppPrefix, .WhatsApp = newContact.WhatsApp,
                .ContactID = oldContact.ContactID
            })
            Return affectedRows > 0
        End Using
    End Function

    Public Function Delete(ByVal clsContact As Contact) As Boolean
        Dim deleteStatement As String =
                            "DELETE FROM [Contacts] 
			                WHERE ContactID = @ContactID"
        Using connection As SqlConnection = DentistXDATA.GetConnection()
            connection.Open()
            Dim affectedRows As Integer = connection.Execute(deleteStatement, New With {.ContactID = clsContact.ContactID})
            Return affectedRows > 0
        End Using
    End Function


    'Methods to get parents and childs


End Class
