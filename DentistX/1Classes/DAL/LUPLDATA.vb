Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections
Imports System.Configuration
Imports Dapper

	Public Class LUPLDATA

		Private ConnectionString As String=DentistXDATA.GetConnection.connectionString



		Public Function SelectAll() As IEnumerable(Of LUPL)
			Using conn As New SqlConnection(ConnectionString)
			    conn.Open()
			    Return conn.Query(Of LUPL)("SELECT * FROM LUPL")
			End Using
		End Function
		

		Public Function Select_Record(ByVal clsLUPL As LUPL) As LUPL
			Using conn As New SqlConnection(ConnectionString)
			    Dim sql As String = "Select * FROM LUPL WHERE LUcellID = @LUcellID And PatientID = @PatientID"
			    Return conn.QuerySingleOrDefault(Of LUPL)(sql, New With { .LUcellID = clsLUPL.LUcellID, .PatientID = clsLUPL.PatientID })
			End Using
		End Function

		Public Function Add(ByVal clsLUPL As LUPL) As Boolean
			Dim RowsAffected As Integer=0
			Using conn As New SqlConnection(ConnectionString)
			    Dim sql As String = "INSERT INTO LUPL (PatientID, CellAddres, ForeColor) VALUES (@PatientID, @CellAddres, @ForeColor)" 
			    RowsAffected = conn.Execute(sql, New With { .PatientID =  clsLUPL.PatientID, .CellAddres =  clsLUPL.CellAddres, .ForeColor =  clsLUPL.ForeColor })
			    Return RowsAffected > 0
			End Using
		End Function

		Public Function Update(oldLUPL As LUPL, newLUPL As LUPL) As Boolean
			Using conn As New SqlConnection(ConnectionString)
			    Dim parameters = New With { 
					.NewPatientID = newLUPL.PatientID, .OldPatientID = oldLUPL.PatientID, .NewCellAddres = newLUPL.CellAddres, .OldCellAddres = oldLUPL.CellAddres, .NewForeColor = newLUPL.ForeColor, .OldForeColor = oldLUPL.ForeColor
										  }
			    Dim affectedRows As Integer = conn.Execute("UPDATE [LUPL] SET [PatientID] = @NewPatientID, [CellAddres] = @NewCellAddres, [ForeColor] = @NewForeColor WHERE [PatientID] = @OldPatientID AND [CellAddres] = @OldCellAddres AND [ForeColor] = @OldForeColor", parameters)
			    Return affectedRows > 0
			End Using
		End Function

		Public Function Delete(ByVal clsLUPL As LUPL) As Boolean
			Dim deleteStatement As String =
			"DELETE FROM [LUPL] 
			WHERE LUcellID = @LUcellID AND PatientID = @PatientID"
			Using connection As SqlConnection = DentistXData.GetConnection()
			    connection.Open()
			    Dim affectedRows As Integer = connection.Execute(deleteStatement, New With { .LUcellID = clsLUPL.LUcellID, .PatientID = clsLUPL.PatientID })
			    Return affectedRows > 0
			End Using
		End Function

    Public Function IsLUPL(patientID As Integer, cellAddress As Integer) As Integer?
        Dim query = "SELECT LUcellID FROM LUPL WHERE (PatientID = @PatientID) AND (CellAddres = @CellAddres)"
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
