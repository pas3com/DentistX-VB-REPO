Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections
Imports System.Configuration
Imports Dapper

	Public Class RUPLDATA

		Private ConnectionString As String=DentistXDATA.GetConnection.connectionString



		Public Function SelectAll() As IEnumerable(Of RUPL)
			Using conn As New SqlConnection(ConnectionString)
			    conn.Open()
			    Return conn.Query(Of RUPL)("SELECT * FROM RUPL")
			End Using
		End Function
		

		Public Function Select_Record(ByVal clsRUPL As RUPL) As RUPL
			Using conn As New SqlConnection(ConnectionString)
			    Dim sql As String = "Select * FROM RUPL WHERE RUcellID = @RUcellID And PatientID = @PatientID"
			    Return conn.QuerySingleOrDefault(Of RUPL)(sql, New With { .RUcellID = clsRUPL.RUcellID, .PatientID = clsRUPL.PatientID })
			End Using
		End Function

		Public Function Add(ByVal clsRUPL As RUPL) As Boolean
			Dim RowsAffected As Integer=0
			Using conn As New SqlConnection(ConnectionString)
			    Dim sql As String = "INSERT INTO RUPL (PatientID, CellAddres, ForeColor) VALUES (@PatientID, @CellAddres, @ForeColor)" 
			    RowsAffected = conn.Execute(sql, New With { .PatientID =  clsRUPL.PatientID, .CellAddres =  clsRUPL.CellAddres, .ForeColor =  clsRUPL.ForeColor })
			    Return RowsAffected > 0
			End Using
		End Function

		Public Function Update(oldRUPL As RUPL, newRUPL As RUPL) As Boolean
			Using conn As New SqlConnection(ConnectionString)
			    Dim parameters = New With { 
					.NewPatientID = newRUPL.PatientID, .OldPatientID = oldRUPL.PatientID, .NewCellAddres = newRUPL.CellAddres, .OldCellAddres = oldRUPL.CellAddres, .NewForeColor = newRUPL.ForeColor, .OldForeColor = oldRUPL.ForeColor
										  }
			    Dim affectedRows As Integer = conn.Execute("UPDATE [RUPL] SET [PatientID] = @NewPatientID, [CellAddres] = @NewCellAddres, [ForeColor] = @NewForeColor WHERE [PatientID] = @OldPatientID AND [CellAddres] = @OldCellAddres AND [ForeColor] = @OldForeColor", parameters)
			    Return affectedRows > 0
			End Using
		End Function

		Public Function Delete(ByVal clsRUPL As RUPL) As Boolean
			Dim deleteStatement As String =
			"DELETE FROM [RUPL] 
			WHERE RUcellID = @RUcellID AND PatientID = @PatientID"
			Using connection As SqlConnection = DentistXData.GetConnection()
			    connection.Open()
			    Dim affectedRows As Integer = connection.Execute(deleteStatement, New With { .RUcellID = clsRUPL.RUcellID, .PatientID = clsRUPL.PatientID })
			    Return affectedRows > 0
			End Using
		End Function


    Public Function IsRUPL(patientID As Integer, cellAddress As Integer) As Integer?
        Dim query = "SELECT RUcellID FROM LDPL WHERE (PatientID = @PatientID) AND (CellAddres = @CellAddres)"
        Using connection As New SqlConnection(ConnectionString)
            Return If(connection.QuerySingleOrDefault(Of Integer?)(query, New With {.PatientID = patientID, .CellAddres = cellAddress}), 0)
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
