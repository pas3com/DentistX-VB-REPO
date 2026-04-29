Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections
Imports System.Configuration
Imports Dapper

	Public Class RDDATA

		Private ConnectionString As String=DentistXDATA.GetConnection.connectionString



		Public Function SelectAll() As IEnumerable(Of RD)
			Using conn As New SqlConnection(ConnectionString)
			    conn.Open()
			    Return conn.Query(Of RD)("SELECT * FROM RD")
			End Using
		End Function
		

		Public Function Select_Record(ByVal clsRD As RD) As RD
			Using conn As New SqlConnection(ConnectionString)
			    Dim sql As String = "Select * FROM RD WHERE RDID = @RDID And PatientID = @PatientID"
			    Return conn.QuerySingleOrDefault(Of RD)(sql, New With { .RDID = clsRD.RDID, .PatientID = clsRD.PatientID })
			End Using
		End Function

		Public Function Add(ByVal clsRD As RD) As Boolean
			Dim RowsAffected As Integer=0
			Using conn As New SqlConnection(ConnectionString)
			    Dim sql As String = "INSERT INTO RD (PatientID, RD1, RD2, RD3, RD4, RD5, RD6, RD7, RD8) VALUES (@PatientID, @RD1, @RD2, @RD3, @RD4, @RD5, @RD6, @RD7, @RD8)" 
			    RowsAffected = conn.Execute(sql, New With { .PatientID =  clsRD.PatientID, .RD1 =  clsRD.RD1, .RD2 =  clsRD.RD2, .RD3 =  clsRD.RD3, .RD4 =  clsRD.RD4, .RD5 =  clsRD.RD5, .RD6 =  clsRD.RD6, .RD7 =  clsRD.RD7, .RD8 =  clsRD.RD8 })
			    Return RowsAffected > 0
			End Using
		End Function

		Public Function Update(oldRD As RD, newRD As RD) As Boolean
			Using conn As New SqlConnection(ConnectionString)
            Dim parameters = New With {
                    .OldRDID = oldRD.RDID, .OldPatientID = oldRD.PatientID, .NewRD1 = newRD.RD1, .NewRD2 = newRD.RD2, .NewRD3 = newRD.RD3, .NewRD4 = newRD.RD4,
                    .NewRD5 = newRD.RD5, .NewRD6 = newRD.RD6, .NewRD7 = newRD.RD7, .NewRD8 = newRD.RD8
                                          }
            Dim affectedRows As Integer = conn.Execute("UPDATE [RD] SET  [RD1] = @NewRD1, [RD2] = @NewRD2, [RD3] = @NewRD3, [RD4] = @NewRD4, [RD5] = @NewRD5, 
                                                        [RD6] = @NewRD6, [RD7] = @NewRD7, [RD8] = @NewRD8 WHERE 
                                                        [RDID] = @OldRDID AND [PatientID] = @OldPatientID", parameters)
            Return affectedRows > 0
        End Using
		End Function

		Public Function Delete(ByVal clsRD As RD) As Boolean
			Dim deleteStatement As String =
			"DELETE FROM [RD] 
			WHERE RDID = @RDID AND PatientID = @PatientID"
			Using connection As SqlConnection = DentistXData.GetConnection()
			    connection.Open()
			    Dim affectedRows As Integer = connection.Execute(deleteStatement, New With { .RDID = clsRD.RDID, .PatientID = clsRD.PatientID })
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
