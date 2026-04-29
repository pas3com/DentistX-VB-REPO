Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections
Imports System.Configuration
Imports Dapper

	Public Class RDPLDATA

		Private ConnectionString As String=DentistXDATA.GetConnection.connectionString



		Public Function SelectAll() As IEnumerable(Of RDPL)
			Using conn As New SqlConnection(ConnectionString)
			    conn.Open()
			    Return conn.Query(Of RDPL)("SELECT * FROM RDPL")
			End Using
		End Function
		

		Public Function Select_Record(ByVal clsRDPL As RDPL) As RDPL
			Using conn As New SqlConnection(ConnectionString)
			    Dim sql As String = "Select * FROM RDPL WHERE RDcellID = @RDcellID And PatientID = @PatientID"
			    Return conn.QuerySingleOrDefault(Of RDPL)(sql, New With { .RDcellID = clsRDPL.RDcellID, .PatientID = clsRDPL.PatientID })
			End Using
		End Function

		Public Function Add(ByVal clsRDPL As RDPL) As Boolean
			Dim RowsAffected As Integer=0
			Using conn As New SqlConnection(ConnectionString)
			    Dim sql As String = "INSERT INTO RDPL (PatientID, CellAddres, ForeColor) VALUES (@PatientID, @CellAddres, @ForeColor)" 
			    RowsAffected = conn.Execute(sql, New With { .PatientID =  clsRDPL.PatientID, .CellAddres =  clsRDPL.CellAddres, .ForeColor =  clsRDPL.ForeColor })
			    Return RowsAffected > 0
			End Using
		End Function

		Public Function Update(oldRDPL As RDPL, newRDPL As RDPL) As Boolean
			Using conn As New SqlConnection(ConnectionString)
			    Dim parameters = New With { 
					.NewPatientID = newRDPL.PatientID, .OldPatientID = oldRDPL.PatientID, .NewCellAddres = newRDPL.CellAddres, .OldCellAddres = oldRDPL.CellAddres, .NewForeColor = newRDPL.ForeColor, .OldForeColor = oldRDPL.ForeColor
										  }
			    Dim affectedRows As Integer = conn.Execute("UPDATE [RDPL] SET [PatientID] = @NewPatientID, [CellAddres] = @NewCellAddres, [ForeColor] = @NewForeColor WHERE [PatientID] = @OldPatientID AND [CellAddres] = @OldCellAddres AND [ForeColor] = @OldForeColor", parameters)
			    Return affectedRows > 0
			End Using
		End Function

		Public Function Delete(ByVal clsRDPL As RDPL) As Boolean
			Dim deleteStatement As String =
			"DELETE FROM [RDPL] 
			WHERE RDcellID = @RDcellID AND PatientID = @PatientID"
			Using connection As SqlConnection = DentistXData.GetConnection()
			    connection.Open()
			    Dim affectedRows As Integer = connection.Execute(deleteStatement, New With { .RDcellID = clsRDPL.RDcellID, .PatientID = clsRDPL.PatientID })
			    Return affectedRows > 0
			End Using
		End Function

    Public Function IsRDPL(patientID As Integer, cellAddress As Integer) As Integer?
        Dim query = "SELECT RDcellID FROM LDPL WHERE (PatientID = @PatientID) AND (CellAddres = @CellAddres)"
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
