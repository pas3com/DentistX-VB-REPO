Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections
Imports System.Configuration
Imports Dapper

	Public Class M_TRTDATA

		Private ConnectionString As String=DentistXDATA.GetConnection.connectionString



		Public Function SelectAll() As IEnumerable(Of M_TRT)
			Using conn As New SqlConnection(ConnectionString)
			    conn.Open()
			    Return conn.Query(Of M_TRT)("SELECT * FROM M_TRT")
			End Using
		End Function
		

		Public Function Select_Record(ByVal clsM_TRT As M_TRT) As M_TRT
			Using conn As New SqlConnection(ConnectionString)
			    Dim sql As String = "Select * FROM M_TRT WHERE MNo = @MNo"
			    Return conn.QuerySingleOrDefault(Of M_TRT)(sql, New With { .MNo = clsM_TRT.MNo })
			End Using
		End Function

		Public Function Add(ByVal clsM_TRT As M_TRT) As Boolean
			Dim RowsAffected As Integer=0
			Using conn As New SqlConnection(ConnectionString)
			    Dim sql As String = "INSERT INTO M_TRT (MNo, MName, MTrt, MPay, MRemain) VALUES (@MNo, @MName, @MTrt, @MPay, @MRemain)" 
			    RowsAffected = conn.Execute(sql, New With { .MNo =  clsM_TRT.MNo, .MName =  clsM_TRT.MName, .MTrt =  clsM_TRT.MTrt, .MPay =  clsM_TRT.MPay, .MRemain =  clsM_TRT.MRemain })
			    Return RowsAffected > 0
			End Using
		End Function

		Public Function Update(oldM_TRT As M_TRT, newM_TRT As M_TRT) As Boolean
			Using conn As New SqlConnection(ConnectionString)
			    Dim parameters = New With { 
					.NewMNo = newM_TRT.MNo, .OldMNo = oldM_TRT.MNo, .NewMName = newM_TRT.MName, .OldMName = oldM_TRT.MName, .NewMTrt = newM_TRT.MTrt, .OldMTrt = oldM_TRT.MTrt, .NewMPay = newM_TRT.MPay, .OldMPay = oldM_TRT.MPay, .NewMRemain = newM_TRT.MRemain, .OldMRemain = oldM_TRT.MRemain
										  }
			    Dim affectedRows As Integer = conn.Execute("UPDATE [M_TRT] SET [MNo] = @NewMNo, [MName] = @NewMName, [MTrt] = @NewMTrt, [MPay] = @NewMPay, [MRemain] = @NewMRemain WHERE [MNo] = @OldMNo AND [MName] = @OldMName AND [MTrt] = @OldMTrt AND [MPay] = @OldMPay AND [MRemain] = @OldMRemain", parameters)
			    Return affectedRows > 0
			End Using
		End Function

		Public Function Delete(ByVal clsM_TRT As M_TRT) As Boolean
			Dim deleteStatement As String =
			"DELETE FROM [M_TRT] 
			WHERE MNo = @MNo"
			Using connection As SqlConnection = DentistXData.GetConnection()
			    connection.Open()
			    Dim affectedRows As Integer = connection.Execute(deleteStatement, New With { .MNo = clsM_TRT.MNo })
			    Return affectedRows > 0
			End Using
		End Function


'Methods to get parents and childs
	End Class
