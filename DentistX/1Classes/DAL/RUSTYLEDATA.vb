Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections
Imports System.Configuration
Imports Dapper

	Public Class RUSTYLEDATA

		Private ConnectionString As String=DentistXDATA.GetConnection.connectionString



		Public Function SelectAll() As IEnumerable(Of RUSTYLE)
			Using conn As New SqlConnection(ConnectionString)
			    conn.Open()
			    Return conn.Query(Of RUSTYLE)("SELECT * FROM RUSTYLE")
			End Using
		End Function
		

		Public Function Select_Record(ByVal clsRUSTYLE As RUSTYLE) As RUSTYLE
			Using conn As New SqlConnection(ConnectionString)
			    Dim sql As String = "Select * FROM RUSTYLE WHERE RUcellID = @RUcellID"
			    Return conn.QuerySingleOrDefault(Of RUSTYLE)(sql, New With { .RUcellID = clsRUSTYLE.RUcellID })
			End Using
		End Function

		Public Function Add(ByVal clsRUSTYLE As RUSTYLE) As Boolean
			Dim RowsAffected As Integer=0
			Using conn As New SqlConnection(ConnectionString)
			    Dim sql As String = "INSERT INTO RUSTYLE (PatientID, CellAddres, BakImg) VALUES (@PatientID, @CellAddres, @BakImg)" 
			    RowsAffected = conn.Execute(sql, New With { .PatientID =  clsRUSTYLE.PatientID, .CellAddres =  clsRUSTYLE.CellAddres, .BakImg =  clsRUSTYLE.BakImg })
			    Return RowsAffected > 0
			End Using
		End Function

		Public Function Update(oldRUSTYLE As RUSTYLE, newRUSTYLE As RUSTYLE) As Boolean
			Using conn As New SqlConnection(ConnectionString)
			    Dim parameters = New With { 
					.NewPatientID = newRUSTYLE.PatientID, .OldPatientID = oldRUSTYLE.PatientID, .NewCellAddres = newRUSTYLE.CellAddres, .OldCellAddres = oldRUSTYLE.CellAddres, .NewBakImg = newRUSTYLE.BakImg, .OldBakImg = oldRUSTYLE.BakImg
										  }
			    Dim affectedRows As Integer = conn.Execute("UPDATE [RUSTYLE] SET [PatientID] = @NewPatientID, [CellAddres] = @NewCellAddres, [BakImg] = @NewBakImg WHERE [PatientID] = @OldPatientID AND [CellAddres] = @OldCellAddres AND [BakImg] = @OldBakImg", parameters)
			    Return affectedRows > 0
			End Using
		End Function

		Public Function Delete(ByVal clsRUSTYLE As RUSTYLE) As Boolean
			Dim deleteStatement As String =
			"DELETE FROM [RUSTYLE] 
			WHERE RUcellID = @RUcellID"
			Using connection As SqlConnection = DentistXData.GetConnection()
			    connection.Open()
			    Dim affectedRows As Integer = connection.Execute(deleteStatement, New With { .RUcellID = clsRUSTYLE.RUcellID })
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
