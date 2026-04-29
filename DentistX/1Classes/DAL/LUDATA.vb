Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections
Imports System.Configuration
Imports Dapper

	Public Class LUDATA

		Private ConnectionString As String=DentistXDATA.GetConnection.connectionString



		Public Function SelectAll() As IEnumerable(Of LU)
			Using conn As New SqlConnection(ConnectionString)
			    conn.Open()
			    Return conn.Query(Of LU)("SELECT * FROM LU")
			End Using
		End Function
		

		Public Function Select_Record(ByVal clsLU As LU) As LU
			Using conn As New SqlConnection(ConnectionString)
			    Dim sql As String = "Select * FROM LU WHERE LUID = @LUID And PatientID = @PatientID"
			    Return conn.QuerySingleOrDefault(Of LU)(sql, New With { .LUID = clsLU.LUID, .PatientID = clsLU.PatientID })
			End Using
		End Function

		Public Function Add(ByVal clsLU As LU) As Boolean
			Dim RowsAffected As Integer=0
			Using conn As New SqlConnection(ConnectionString)
			    Dim sql As String = "INSERT INTO LU (PatientID, LU1, LU2, LU3, LU4, LU5, LU6, LU7, LU8) VALUES (@PatientID, @LU1, @LU2, @LU3, @LU4, @LU5, @LU6, @LU7, @LU8)" 
			    RowsAffected = conn.Execute(sql, New With { .PatientID =  clsLU.PatientID, .LU1 =  clsLU.LU1, .LU2 =  clsLU.LU2, .LU3 =  clsLU.LU3, .LU4 =  clsLU.LU4, .LU5 =  clsLU.LU5, .LU6 =  clsLU.LU6, .LU7 =  clsLU.LU7, .LU8 =  clsLU.LU8 })
			    Return RowsAffected > 0
			End Using
		End Function

		Public Function Update(oldLU As LU, newLU As LU) As Boolean
			Using conn As New SqlConnection(ConnectionString)
            Dim parameters = New With {
                    .OldLUID = oldLU.LUID, .OldPatientID = oldLU.PatientID, .NewLU1 = newLU.LU1, .NewLU2 = newLU.LU2, .NewLU3 = newLU.LU3, .NewLU4 = newLU.LU4,
                    .NewLU5 = newLU.LU5, .NewLU6 = newLU.LU6, .NewLU7 = newLU.LU7, .NewLU8 = newLU.LU8
                                          }
            Dim affectedRows As Integer = conn.Execute("UPDATE [LU] SET  [LU1] = @NewLU1, [LU2] = @NewLU2, [LU3] = @NewLU3, [LU4] = @NewLU4, [LU5] = @NewLU5, 
                                                        [LU6] = @NewLU6, [LU7] = @NewLU7, [LU8] = @NewLU8 WHERE 
                                                        [LUID] = @OldLUID AND [PatientID] = @OldPatientID", parameters)
            Return affectedRows > 0
			End Using
		End Function

		Public Function Delete(ByVal clsLU As LU) As Boolean
			Dim deleteStatement As String =
			"DELETE FROM [LU] 
			WHERE LUID = @LUID AND PatientID = @PatientID"
			Using connection As SqlConnection = DentistXData.GetConnection()
			    connection.Open()
			    Dim affectedRows As Integer = connection.Execute(deleteStatement, New With { .LUID = clsLU.LUID, .PatientID = clsLU.PatientID })
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
