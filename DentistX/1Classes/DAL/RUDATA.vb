Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections
Imports System.Configuration
Imports Dapper

	Public Class RUDATA

		Private ConnectionString As String=DentistXDATA.GetConnection.connectionString



		Public Function SelectAll() As IEnumerable(Of RU)
			Using conn As New SqlConnection(ConnectionString)
			    conn.Open()
			    Return conn.Query(Of RU)("SELECT * FROM RU")
			End Using
		End Function
		

		Public Function Select_Record(ByVal clsRU As RU) As RU
			Using conn As New SqlConnection(ConnectionString)
			    Dim sql As String = "Select * FROM RU WHERE RUID = @RUID And PatientID = @PatientID"
			    Return conn.QuerySingleOrDefault(Of RU)(sql, New With { .RUID = clsRU.RUID, .PatientID = clsRU.PatientID })
			End Using
		End Function

		Public Function Add(ByVal clsRU As RU) As Boolean
			Dim RowsAffected As Integer=0
			Using conn As New SqlConnection(ConnectionString)
			    Dim sql As String = "INSERT INTO RU (PatientID, RU1, RU2, RU3, RU4, RU5, RU6, RU7, RU8) VALUES (@PatientID, @RU1, @RU2, @RU3, @RU4, @RU5, @RU6, @RU7, @RU8)" 
			    RowsAffected = conn.Execute(sql, New With { .PatientID =  clsRU.PatientID, .RU1 =  clsRU.RU1, .RU2 =  clsRU.RU2, .RU3 =  clsRU.RU3, .RU4 =  clsRU.RU4, .RU5 =  clsRU.RU5, .RU6 =  clsRU.RU6, .RU7 =  clsRU.RU7, .RU8 =  clsRU.RU8 })
			    Return RowsAffected > 0
			End Using
		End Function

		Public Function Update(oldRU As RU, newRU As RU) As Boolean
			Using conn As New SqlConnection(ConnectionString)
            Dim parameters = New With {
                    .OldRUID = oldRU.RUID, .OldPatientID = oldRU.PatientID, .NewRU1 = newRU.RU1, .NewRU2 = newRU.RU2, .NewRU3 = newRU.RU3, .NewRU4 = newRU.RU4,
                    .NewRU5 = newRU.RU5, .NewRU6 = newRU.RU6, .NewRU7 = newRU.RU7, .NewRU8 = newRU.RU8
                                          }
            Dim affectedRows As Integer = conn.Execute("UPDATE [RU] SET  [RU1] = @NewRU1, [RU2] = @NewRU2, [RU3] = @NewRU3, [RU4] = @NewRU4, [RU5] = @NewRU5, 
                                                        [RU6] = @NewRU6, [RU7] = @NewRU7, [RU8] = @NewRU8 WHERE 
                                                        [RUID] = @OldRUID AND [PatientID] = @OldPatientID", parameters)
            Return affectedRows > 0
        End Using
		End Function

		Public Function Delete(ByVal clsRU As RU) As Boolean
			Dim deleteStatement As String =
			"DELETE FROM [RU] 
			WHERE RUID = @RUID AND PatientID = @PatientID"
			Using connection As SqlConnection = DentistXData.GetConnection()
			    connection.Open()
			    Dim affectedRows As Integer = connection.Execute(deleteStatement, New With { .RUID = clsRU.RUID, .PatientID = clsRU.PatientID })
			    Return affectedRows > 0
			End Using
		End Function


'Methods to get parents and childs
		Public Function GetPatient(ByVal PatientID As Integer) As Patient
		Dim parent As Patient = Nothing
		Using conn As New SqlConnection(ConnectionString)
			Dim query As String = "SELECT * FROM [Patient] WHERE [PatientID] = @PatientID"
			Try
				conn.Open()
				parent = conn.QuerySingleOrDefault(Of Patient)(query, New With {.PatientId = PatientID})
			Catch ex As Exception
				' Handle exceptions
			Finally
				If conn.State = ConnectionState.Open Then conn.Close()
			End Try
		End Using
		Return parent
		End Function

	End Class
