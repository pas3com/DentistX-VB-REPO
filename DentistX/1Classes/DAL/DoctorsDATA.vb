Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections
Imports System.Configuration
Imports Dapper

Public Class DoctorsDATA

    Private ConnectionString As String = DentistXDATA.GetConnection.ConnectionString

    Public Function SelectAll() As IEnumerable(Of Doctors)
        Using conn As New SqlConnection(ConnectionString)
            conn.Open()
            Return conn.Query(Of Doctors)("SELECT * FROM Doctors  ORDER BY DrName")
        End Using
    End Function


    Public Function Select_Record(ByVal clsDoctors As Doctors) As Doctors 'GetDoctorById
        Using conn As New SqlConnection(ConnectionString)
            Dim sql As String = "Select * FROM Doctors WHERE DrID = @DrID"
            Return conn.QuerySingleOrDefault(Of Doctors)(sql, New With {.DrID = clsDoctors.DrID})
        End Using
    End Function

    Public Function GetDoctorById(ByVal DrID As Integer) As Doctors 'GetDoctorById
        Using conn As New SqlConnection(ConnectionString)
            Dim sql As String = "Select * FROM Doctors WHERE DrID = @DrID"
            Return conn.QuerySingleOrDefault(Of Doctors)(sql, New With {.DrID = DrID})
        End Using
    End Function
    Public Function GetDoctorColor(drId As Integer) As String
        Using conn As New SqlConnection(ConnectionString)
            Return conn.ExecuteScalar(Of String)("SELECT DrColor FROM Doctors WHERE DrID=@ID", New With {.ID = drId})
        End Using
    End Function
    Public Function GetDoctorNameById(ByVal DrID As Integer) As String 'GetDoctorById
        Using conn As New SqlConnection(ConnectionString)
            Dim sql As String = "Select DrName FROM Doctors WHERE DrID = @DrID"
            Return conn.QuerySingleOrDefault(Of String)(sql, New With {.DrID = DrID})
        End Using
    End Function

    Public Function GetDoctorIdByName(ByVal DrName As String) As Integer 'GetDoctorById
        Using conn As New SqlConnection(ConnectionString)
            Dim sql As String = "Select DrID  FROM Doctors WHERE DrName = @DrName"
            Return conn.QuerySingleOrDefault(Of Integer)(sql, New With {.DrName = DrName})
        End Using
    End Function
    Public Function Add(ByVal clsDoctors As Doctors) As Boolean
        Dim RowsAffected As Integer = 0
        Using conn As New SqlConnection(ConnectionString)
            Dim sql As String = "INSERT INTO Doctors (DrName, DrAdres, Drphone, DrMobile, DrColor, WhatsAppPrefix, WhatsApp) VALUES (@DrName, @DrAdres, @Drphone, @DrMobile, @DrColor, @WhatsAppPrefix, @WhatsApp)"
            RowsAffected = conn.Execute(sql, New With {
                .DrName = clsDoctors.DrName, .DrAdres = clsDoctors.DrAdres, .Drphone = clsDoctors.DrPhone, .DrMobile = clsDoctors.DrMobile, .DrColor = clsDoctors.DrColor,
                .WhatsAppPrefix = clsDoctors.WhatsAppPrefix, .WhatsApp = clsDoctors.WhatsApp
            })
            Return RowsAffected > 0
        End Using
    End Function

    Public Function Update(oldDoctors As Doctors, newDoctors As Doctors) As Boolean
        Using conn As New SqlConnection(ConnectionString)
            Dim sql = "UPDATE [Doctors] SET [DrName] = @DrName, [DrAdres] = @DrAdres, [Drphone] = @Drphone, [DrMobile] = @DrMobile, [DrColor] = @DrColor, [WhatsAppPrefix] = @WhatsAppPrefix, [WhatsApp] = @WhatsApp WHERE [DrID] = @DrID"
            Dim affectedRows As Integer = conn.Execute(sql, New With {
                .DrName = newDoctors.DrName, .DrAdres = newDoctors.DrAdres, .Drphone = newDoctors.DrPhone, .DrMobile = newDoctors.DrMobile, .DrColor = newDoctors.DrColor,
                .WhatsAppPrefix = newDoctors.WhatsAppPrefix, .WhatsApp = newDoctors.WhatsApp,
                .DrID = oldDoctors.DrID
            })
            Return affectedRows > 0
        End Using
    End Function

    Public Function CountAppointmentCForDoctor(drId As Integer) As Integer
        Using conn As New SqlConnection(ConnectionString)
            Return conn.ExecuteScalar(Of Integer)("SELECT COUNT(*) FROM AppointmentC WHERE DrID = @DrID", New With {.DrID = drId})
        End Using
    End Function

    Public Function CountUsersLinkedToDoctor(drId As Integer) As Integer
        Using conn As New SqlConnection(ConnectionString)
            Return conn.ExecuteScalar(Of Integer)("SELECT COUNT(*) FROM USERS WHERE DrID = @DrID", New With {.DrID = drId})
        End Using
    End Function

    Public Function Delete(ByVal clsDoctors As Doctors) As Boolean
        Dim deleteStatement As String =
        "DELETE FROM [Doctors] 
			WHERE DrID = @DrID"
        Using connection As SqlConnection = DentistXDATA.GetConnection()
            connection.Open()
            Dim affectedRows As Integer = connection.Execute(deleteStatement, New With {.DrID = clsDoctors.DrID})
            Return affectedRows > 0
        End Using
    End Function


    'Methods to get parents and childs
End Class
