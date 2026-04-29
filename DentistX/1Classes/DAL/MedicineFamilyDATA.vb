Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections
Imports System.Configuration
Imports Dapper

	Public Class MedicineFamilyDATA

		Private ConnectionString As String=DentistXDATA.GetConnection.connectionString



		Public Function SelectAll() As IEnumerable(Of MedicineFamily)
			Using conn As New SqlConnection(ConnectionString)
			    conn.Open()
			    Return conn.Query(Of MedicineFamily)("SELECT * FROM MedicineFamily")
			End Using
		End Function
		

		Public Function Select_Record(ByVal clsMedicineFamily As MedicineFamily) As MedicineFamily
			Using conn As New SqlConnection(ConnectionString)
			    Dim sql As String = "Select * FROM MedicineFamily WHERE SubCatID = @SubCatID"
			    Return conn.QuerySingleOrDefault(Of MedicineFamily)(sql, New With { .SubCatID = clsMedicineFamily.SubCatID })
			End Using
		End Function
	Public Function SelectBySubCatID(SubCatID As Integer) As IEnumerable(Of MedicineFamily)
		Using conn As New SqlConnection(ConnectionString)
			Return conn.Query(Of MedicineFamily)("SELECT * FROM MedicineFamily WHERE SubCatID = @SubCatID", New With {.SubCatID = SubCatID})
		End Using
	End Function

	Public Function Add(ByVal clsMedicineFamily As MedicineFamily) As Boolean
			Dim RowsAffected As Integer=0
			Using conn As New SqlConnection(ConnectionString)
			    Dim sql As String = "INSERT INTO MedicineFamily (MedicineSubCat, MedicineSubCat) VALUES (@MedicineSubCat, @MedicineSubCat)" 
			    RowsAffected = conn.Execute(sql, New With { .MedicineID =  clsMedicineFamily.MedicineID, .MedicineSubCat =  clsMedicineFamily.MedicineSubCat })
			    Return RowsAffected > 0
			End Using
		End Function

		Public Function Update(oldMedicineFamily As MedicineFamily, newMedicineFamily As MedicineFamily) As Boolean
			Using conn As New SqlConnection(ConnectionString)
			    Dim parameters = New With { 
					.NewMedicineID = newMedicineFamily.MedicineID, .OldMedicineID = oldMedicineFamily.MedicineID, .NewMedicineSubCat = newMedicineFamily.MedicineSubCat, .OldMedicineSubCat = oldMedicineFamily.MedicineSubCat
										  }
			    Dim affectedRows As Integer = conn.Execute("UPDATE [MedicineFamily] SET [MedicineSubCat] = @NewMedicineID, [MedicineSubCat] = @NewMedicineSubCat WHERE [MedicineSubCat] = @OldMedicineID AND [MedicineSubCat] = @OldMedicineSubCat", parameters)
			    Return affectedRows > 0
			End Using
		End Function

		Public Function Delete(ByVal clsMedicineFamily As MedicineFamily) As Boolean
			Dim deleteStatement As String =
			"DELETE FROM [MedicineFamily] 
			WHERE SubCatID = @SubCatID"
			Using connection As SqlConnection = DentistXData.GetConnection()
			    connection.Open()
			    Dim affectedRows As Integer = connection.Execute(deleteStatement, New With { .SubCatID = clsMedicineFamily.SubCatID })
			    Return affectedRows > 0
			End Using
		End Function


'Methods to get parents and childs
		Public Function GetMedicineGroups(ByVal MedicineID As Integer) As MedicineGroups
		Dim parent As MedicineGroups = Nothing
		Using conn As New SqlConnection(ConnectionString)
			Dim query As String = "SELECT * FROM [MedicineGroups] WHERE [MedicineSubCat] = @MedicineSubCat"
			Try
				conn.Open()
				parent = conn.QuerySingleOrDefault(Of MedicineGroups)(query, New With {.MedicineID = MedicineID})
			Catch ex As Exception
				' Handle exceptions
			Finally
				If conn.State = ConnectionState.Open Then conn.Close()
			End Try
		End Using
		Return parent
		End Function

		Public Function GetMedScienceFamily(ByVal clsMedicineFamily As MedicineFamily ) As IEnumerable(Of MedScienceFamily)
			Using conn As New SqlConnection(ConnectionString)
				conn.Open()
				Dim query As String = "SELECT * FROM [MedScienceFamily] WHERE [SubCatID] = @SubCatID"
				Return conn.Query(Of MedScienceFamily)(query, New With { .SubCatID= clsMedicineFamily.SubCatID })
			End Using
		End Function

	End Class
