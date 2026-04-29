Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections
Imports System.Configuration
Imports Dapper

	Public Class LDDATA

		Private ConnectionString As String=DentistXDATA.GetConnection.connectionString



		Public Function SelectAll() As IEnumerable(Of LD)
			Using conn As New SqlConnection(ConnectionString)
			    conn.Open()
			    Return conn.Query(Of LD)("SELECT * FROM LD")
			End Using
		End Function
		

		Public Function Select_Record(ByVal clsLD As LD) As LD
			Using conn As New SqlConnection(ConnectionString)
			    Dim sql As String = "Select * FROM LD WHERE LDID = @LDID And PatientID = @PatientID"
			    Return conn.QuerySingleOrDefault(Of LD)(sql, New With { .LDID = clsLD.LDID, .PatientID = clsLD.PatientID })
			End Using
		End Function

		Public Function Add(ByVal clsLD As LD) As Boolean
			Dim RowsAffected As Integer=0
			Using conn As New SqlConnection(ConnectionString)
			    Dim sql As String = "INSERT INTO LD (PatientID, LD1, LD2, LD3, LD4, LD5, LD6, LD7, LD8) VALUES (@PatientID, @LD1, @LD2, @LD3, @LD4, @LD5, @LD6, @LD7, @LD8)" 
			    RowsAffected = conn.Execute(sql, New With { .PatientID =  clsLD.PatientID, .LD1 =  clsLD.LD1, .LD2 =  clsLD.LD2, .LD3 =  clsLD.LD3, .LD4 =  clsLD.LD4, .LD5 =  clsLD.LD5, .LD6 =  clsLD.LD6, .LD7 =  clsLD.LD7, .LD8 =  clsLD.LD8 })
			    Return RowsAffected > 0
			End Using
		End Function

		Public Function Update(oldLD As LD, newLD As LD) As Boolean
			Using conn As New SqlConnection(ConnectionString)
            Dim parameters = New With {
                    .OldLDID = oldLD.LDID, .OldPatientID = oldLD.PatientID, .NewLD1 = newLD.LD1, .NewLD2 = newLD.LD2, .NewLD3 = newLD.LD3, .NewLD4 = newLD.LD4,
                    .NewLD5 = newLD.LD5, .NewLD6 = newLD.LD6, .NewLD7 = newLD.LD7, .NewLD8 = newLD.LD8
                                          }
            Dim affectedRows As Integer = conn.Execute("UPDATE [LD] SET  [LD1] = @NewLD1, [LD2] = @NewLD2, [LD3] = @NewLD3, [LD4] = @NewLD4, [LD5] = @NewLD5, 
                                                        [LD6] = @NewLD6, [LD7] = @NewLD7, [LD8] = @NewLD8 WHERE 
                                                        [LDID] = @OldLDID AND [PatientID] = @OldPatientID", parameters)
            Return affectedRows > 0
        End Using
		End Function

		Public Function Delete(ByVal clsLD As LD) As Boolean
			Dim deleteStatement As String =
			"DELETE FROM [LD] 
			WHERE LDID = @LDID AND PatientID = @PatientID"
			Using connection As SqlConnection = DentistXData.GetConnection()
			    connection.Open()
			    Dim affectedRows As Integer = connection.Execute(deleteStatement, New With { .LDID = clsLD.LDID, .PatientID = clsLD.PatientID })
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
