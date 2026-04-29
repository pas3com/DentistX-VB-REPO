Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections
Imports System.Configuration
Imports Dapper

	Public Class LDPLDATA

		Private ConnectionString As String=DentistXDATA.GetConnection.connectionString



		Public Function SelectAll() As IEnumerable(Of LDPL)
			Using conn As New SqlConnection(ConnectionString)
			    conn.Open()
			    Return conn.Query(Of LDPL)("SELECT * FROM LDPL")
			End Using
		End Function
		

		Public Function Select_Record(ByVal clsLDPL As LDPL) As LDPL
			Using conn As New SqlConnection(ConnectionString)
			    Dim sql As String = "Select * FROM LDPL WHERE LDcellID = @LDcellID And PatientID = @PatientID"
			    Return conn.QuerySingleOrDefault(Of LDPL)(sql, New With { .LDcellID = clsLDPL.LDcellID, .PatientID = clsLDPL.PatientID })
			End Using
		End Function

		Public Function Add(ByVal clsLDPL As LDPL) As Boolean
			Dim RowsAffected As Integer=0
			Using conn As New SqlConnection(ConnectionString)
			    Dim sql As String = "INSERT INTO LDPL (PatientID, CellAddres, ForeColor) VALUES (@PatientID, @CellAddres, @ForeColor)" 
			    RowsAffected = conn.Execute(sql, New With { .PatientID =  clsLDPL.PatientID, .CellAddres =  clsLDPL.CellAddres, .ForeColor =  clsLDPL.ForeColor })
			    Return RowsAffected > 0
			End Using
		End Function

		Public Function Update(oldLDPL As LDPL, newLDPL As LDPL) As Boolean
			Using conn As New SqlConnection(ConnectionString)
			    Dim parameters = New With { 
					.NewPatientID = newLDPL.PatientID, .OldPatientID = oldLDPL.PatientID, .NewCellAddres = newLDPL.CellAddres, .OldCellAddres = oldLDPL.CellAddres, .NewForeColor = newLDPL.ForeColor, .OldForeColor = oldLDPL.ForeColor
										  }
			    Dim affectedRows As Integer = conn.Execute("UPDATE [LDPL] SET [PatientID] = @NewPatientID, [CellAddres] = @NewCellAddres, [ForeColor] = @NewForeColor WHERE [PatientID] = @OldPatientID AND [CellAddres] = @OldCellAddres AND [ForeColor] = @OldForeColor", parameters)
			    Return affectedRows > 0
			End Using
		End Function

		Public Function Delete(ByVal clsLDPL As LDPL) As Boolean
			Dim deleteStatement As String =
			"DELETE FROM [LDPL] 
			WHERE LDcellID = @LDcellID AND PatientID = @PatientID"
			Using connection As SqlConnection = DentistXData.GetConnection()
			    connection.Open()
			    Dim affectedRows As Integer = connection.Execute(deleteStatement, New With { .LDcellID = clsLDPL.LDcellID, .PatientID = clsLDPL.PatientID })
			    Return affectedRows > 0
			End Using
		End Function

    Public Function IsLDPL(patientID As Integer, cellAddress As Integer) As Integer?
        Dim query = "SELECT LDcellID FROM LDPL WHERE (PatientID = @PatientID) AND (CellAddres = @CellAddres)"
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
