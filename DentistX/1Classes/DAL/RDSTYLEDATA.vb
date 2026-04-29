Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections
Imports System.Configuration
Imports Dapper

	Public Class RDSTYLEDATA

		Private ConnectionString As String=DentistXDATA.GetConnection.connectionString



		Public Function SelectAll() As IEnumerable(Of RDSTYLE)
			Using conn As New SqlConnection(ConnectionString)
			    conn.Open()
			    Return conn.Query(Of RDSTYLE)("SELECT * FROM RDSTYLE")
			End Using
		End Function
		

		Public Function Select_Record(ByVal clsRDSTYLE As RDSTYLE) As RDSTYLE
			Using conn As New SqlConnection(ConnectionString)
			    Dim sql As String = "Select * FROM RDSTYLE WHERE RDcellID = @RDcellID"
			    Return conn.QuerySingleOrDefault(Of RDSTYLE)(sql, New With { .RDcellID = clsRDSTYLE.RDcellID })
			End Using
		End Function

		Public Function Add(ByVal clsRDSTYLE As RDSTYLE) As Boolean
			Dim RowsAffected As Integer=0
			Using conn As New SqlConnection(ConnectionString)
			    Dim sql As String = "INSERT INTO RDSTYLE (PatientID, CellAddres, BakImg) VALUES (@PatientID, @CellAddres, @BakImg)" 
			    RowsAffected = conn.Execute(sql, New With { .PatientID =  clsRDSTYLE.PatientID, .CellAddres =  clsRDSTYLE.CellAddres, .BakImg =  clsRDSTYLE.BakImg })
			    Return RowsAffected > 0
			End Using
		End Function

		Public Function Update(oldRDSTYLE As RDSTYLE, newRDSTYLE As RDSTYLE) As Boolean
			Using conn As New SqlConnection(ConnectionString)
			    Dim parameters = New With { 
					.NewPatientID = newRDSTYLE.PatientID, .OldPatientID = oldRDSTYLE.PatientID, .NewCellAddres = newRDSTYLE.CellAddres, .OldCellAddres = oldRDSTYLE.CellAddres, .NewBakImg = newRDSTYLE.BakImg, .OldBakImg = oldRDSTYLE.BakImg
										  }
			    Dim affectedRows As Integer = conn.Execute("UPDATE [RDSTYLE] SET [PatientID] = @NewPatientID, [CellAddres] = @NewCellAddres, [BakImg] = @NewBakImg WHERE [PatientID] = @OldPatientID AND [CellAddres] = @OldCellAddres AND [BakImg] = @OldBakImg", parameters)
			    Return affectedRows > 0
			End Using
		End Function

		Public Function Delete(ByVal clsRDSTYLE As RDSTYLE) As Boolean
			Dim deleteStatement As String =
			"DELETE FROM [RDSTYLE] 
			WHERE RDcellID = @RDcellID"
			Using connection As SqlConnection = DentistXData.GetConnection()
			    connection.Open()
			    Dim affectedRows As Integer = connection.Execute(deleteStatement, New With { .RDcellID = clsRDSTYLE.RDcellID })
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
