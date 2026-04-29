Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections
Imports System.Configuration
Imports Dapper

Public Class RxFlyDATA

    Private ConnectionString As String = DentistXDATA.GetConnection.ConnectionString



    Public Function SelectAll() As IEnumerable(Of RxFly)
        Using conn As New SqlConnection(ConnectionString)
            conn.Open()
            Return conn.Query(Of RxFly)("SELECT * FROM RxFly")
        End Using
    End Function
    Public Function Select_RxFly(ByVal RxID As Integer) As IEnumerable(Of RxFly)
        Using conn As New SqlConnection(ConnectionString)
            Dim sql As String = "Select * FROM RxFly WHERE RxID = @RxID"
            Return conn.QuerySingleOrDefault(Of RxFly)(sql, New With {.RxID = RxID})
        End Using
    End Function

    Public Function Select_Record(ByVal clsRxFly As RxFly) As RxFly
        Using conn As New SqlConnection(ConnectionString)
            Dim sql As String = "Select * FROM RxFly WHERE RxID = @RxID"
            Return conn.QuerySingleOrDefault(Of RxFly)(sql, New With {.RxID = clsRxFly.RxID})
        End Using
    End Function
    Public Function GetLastRX() As Integer
        Dim query = "SELECT  MAX([RxID])  FROM [RxFly] "
        Using connection As New SqlConnection(ConnectionString)
            Return If(connection.QuerySingleOrDefault(Of Integer?)(query), -1)
        End Using
    End Function

    Public Function Add(ByVal clsRxFly As RxFly) As Boolean
        Dim RowsAffected As Integer = 0
        Using conn As New SqlConnection(ConnectionString)
            Dim sql As String = "INSERT INTO RxFly (PatientName, PatientAge, PatientSex, RxDate, RX, DrName)
									VALUES (@PatientName, @PatientAge, @PatientSex, @RxDate, @RX, @DrName)"
            RowsAffected = conn.Execute(sql, New With {.PatientName = clsRxFly.PatientName,
                                        .PatientAge = clsRxFly.PatientAge,
                                        .PatientSex = clsRxFly.PatientSex,
                                        .RxDate = clsRxFly.RxDate,
                                        .RX = clsRxFly.RX,
                                        .DrName = clsRxFly.DrName})
            Return RowsAffected > 0
        End Using
    End Function

    Public Function Update(oldRxFly As RxFly, newRxFly As RxFly) As Boolean
        Using conn As New SqlConnection(ConnectionString)
            Dim parameters = New With {
                    .NewPatientName = newRxFly.PatientName,
                    .OldPatientName = oldRxFly.PatientName,
                    .NewPatientAge = newRxFly.PatientAge,
                    .OldPatientAge = oldRxFly.PatientAge,
                    .NewPatientSex = newRxFly.PatientSex,
                    .OldPatientSex = oldRxFly.PatientSex,
                    .NewRxDate = newRxFly.RxDate,
                    .OldRxDate = oldRxFly.RxDate,
                    .NewRX = newRxFly.RX,
                    .OldRX = oldRxFly.RX,
                    .NewDrName = newRxFly.DrName,
                    .OldDrName = oldRxFly.DrName
                                          }
            Dim affectedRows As Integer = conn.Execute("UPDATE [RxFly] SET
														[PatientName] = @NewPatientName,
														[PatientAge] = @NewPatientAge,
														[PatientSex] = @NewPatientSex,
														[RxDate] = @NewRxDate,
														[RX] = @NewRX,
														[DrName] = @NewDrName
														WHERE 
														[PatientName] = @OldPatientName AND
														[PatientAge] = @OldPatientAge AND 
														[PatientSex] = @OldPatientSex AND
														[RxDate] = @OldRxDate AND
														[RX] = @OldRX AND 
														[DrName] = @OldDrName", parameters)
            Return affectedRows > 0
        End Using
    End Function

    Public Function Delete(ByVal clsRxFly As RxFly) As Boolean
        Dim deleteStatement As String =
            "DELETE FROM [RxFly] 
			WHERE RxID = @RxID"
        Using connection As SqlConnection = DentistXDATA.GetConnection()
            connection.Open()
            Dim affectedRows As Integer = connection.Execute(deleteStatement, New With {.RxID = clsRxFly.RxID})
            Return affectedRows > 0
        End Using
    End Function


    'Methods to get parents and childs
End Class
