Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections
Imports System.Configuration
Imports Dapper

	Public Class LUSTYLEDATA

		Private ConnectionString As String=DentistXDATA.GetConnection.connectionString



		Public Function SelectAll() As IEnumerable(Of LUSTYLE)
			Using conn As New SqlConnection(ConnectionString)
			    conn.Open()
			    Return conn.Query(Of LUSTYLE)("SELECT * FROM LUSTYLE")
			End Using
		End Function
		

		Public Function Select_Record(ByVal clsLUSTYLE As LUSTYLE) As LUSTYLE
			Using conn As New SqlConnection(ConnectionString)
			    Dim sql As String = "Select * FROM LUSTYLE WHERE LUcellID = @LUcellID"
			    Return conn.QuerySingleOrDefault(Of LUSTYLE)(sql, New With { .LUcellID = clsLUSTYLE.LUcellID })
			End Using
		End Function

		Public Function Add(ByVal clsLUSTYLE As LUSTYLE) As Boolean
			Dim RowsAffected As Integer=0
			Using conn As New SqlConnection(ConnectionString)
			    Dim sql As String = "INSERT INTO LUSTYLE (PatientID, CellAddres, BakImg) VALUES (@PatientID, @CellAddres, @BakImg)" 
			    RowsAffected = conn.Execute(sql, New With { .PatientID =  clsLUSTYLE.PatientID, .CellAddres =  clsLUSTYLE.CellAddres, .BakImg =  clsLUSTYLE.BakImg })
			    Return RowsAffected > 0
			End Using
		End Function

		Public Function Update(oldLUSTYLE As LUSTYLE, newLUSTYLE As LUSTYLE) As Boolean
			Using conn As New SqlConnection(ConnectionString)
			    Dim parameters = New With { 
					.NewPatientID = newLUSTYLE.PatientID, .OldPatientID = oldLUSTYLE.PatientID, .NewCellAddres = newLUSTYLE.CellAddres, .OldCellAddres = oldLUSTYLE.CellAddres, .NewBakImg = newLUSTYLE.BakImg, .OldBakImg = oldLUSTYLE.BakImg
										  }
			    Dim affectedRows As Integer = conn.Execute("UPDATE [LUSTYLE] SET [PatientID] = @NewPatientID, [CellAddres] = @NewCellAddres, [BakImg] = @NewBakImg WHERE [PatientID] = @OldPatientID AND [CellAddres] = @OldCellAddres AND [BakImg] = @OldBakImg", parameters)
			    Return affectedRows > 0
			End Using
		End Function

		Public Function Delete(ByVal clsLUSTYLE As LUSTYLE) As Boolean
			Dim deleteStatement As String =
			"DELETE FROM [LUSTYLE] 
			WHERE LUcellID = @LUcellID"
			Using connection As SqlConnection = DentistXData.GetConnection()
			    connection.Open()
			    Dim affectedRows As Integer = connection.Execute(deleteStatement, New With { .LUcellID = clsLUSTYLE.LUcellID })
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
