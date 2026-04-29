Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections
Imports System.Configuration
Imports Dapper

	Public Class LDSTYLEDATA

		Private ConnectionString As String=DentistXDATA.GetConnection.connectionString



		Public Function SelectAll() As IEnumerable(Of LDSTYLE)
			Using conn As New SqlConnection(ConnectionString)
			    conn.Open()
			    Return conn.Query(Of LDSTYLE)("SELECT * FROM LDSTYLE")
			End Using
		End Function
		

		Public Function Select_Record(ByVal clsLDSTYLE As LDSTYLE) As LDSTYLE
			Using conn As New SqlConnection(ConnectionString)
			    Dim sql As String = "Select * FROM LDSTYLE WHERE LDcellID = @LDcellID"
			    Return conn.QuerySingleOrDefault(Of LDSTYLE)(sql, New With { .LDcellID = clsLDSTYLE.LDcellID })
			End Using
		End Function

		Public Function Add(ByVal clsLDSTYLE As LDSTYLE) As Boolean
			Dim RowsAffected As Integer=0
			Using conn As New SqlConnection(ConnectionString)
			    Dim sql As String = "INSERT INTO LDSTYLE (PatientID, CellAddres, BakImg) VALUES (@PatientID, @CellAddres, @BakImg)" 
			    RowsAffected = conn.Execute(sql, New With { .PatientID =  clsLDSTYLE.PatientID, .CellAddres =  clsLDSTYLE.CellAddres, .BakImg =  clsLDSTYLE.BakImg })
			    Return RowsAffected > 0
			End Using
		End Function

		Public Function Update(oldLDSTYLE As LDSTYLE, newLDSTYLE As LDSTYLE) As Boolean
			Using conn As New SqlConnection(ConnectionString)
			    Dim parameters = New With { 
					.NewPatientID = newLDSTYLE.PatientID, .OldPatientID = oldLDSTYLE.PatientID, .NewCellAddres = newLDSTYLE.CellAddres, .OldCellAddres = oldLDSTYLE.CellAddres, .NewBakImg = newLDSTYLE.BakImg, .OldBakImg = oldLDSTYLE.BakImg
										  }
			    Dim affectedRows As Integer = conn.Execute("UPDATE [LDSTYLE] SET [PatientID] = @NewPatientID, [CellAddres] = @NewCellAddres, [BakImg] = @NewBakImg WHERE [PatientID] = @OldPatientID AND [CellAddres] = @OldCellAddres AND [BakImg] = @OldBakImg", parameters)
			    Return affectedRows > 0
			End Using
		End Function

		Public Function Delete(ByVal clsLDSTYLE As LDSTYLE) As Boolean
			Dim deleteStatement As String =
			"DELETE FROM [LDSTYLE] 
			WHERE LDcellID = @LDcellID"
			Using connection As SqlConnection = DentistXData.GetConnection()
			    connection.Open()
			    Dim affectedRows As Integer = connection.Execute(deleteStatement, New With { .LDcellID = clsLDSTYLE.LDcellID })
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
