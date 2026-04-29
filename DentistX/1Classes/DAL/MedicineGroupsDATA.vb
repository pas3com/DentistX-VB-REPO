Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections
Imports System.Configuration
Imports Dapper

	Public Class MedicineGroupsDATA

		Private ConnectionString As String=DentistXDATA.GetConnection.connectionString



		Public Function SelectAll() As IEnumerable(Of MedicineGroups)
			Using conn As New SqlConnection(ConnectionString)
			    conn.Open()
			    Return conn.Query(Of MedicineGroups)("SELECT * FROM MedicineGroups")
			End Using
		End Function
		

		Public Function Select_Record(ByVal clsMedicineGroups As MedicineGroups) As MedicineGroups
			Using conn As New SqlConnection(ConnectionString)
			    Dim sql As String = "Select * FROM MedicineGroups WHERE MedicineSubCat = @MedicineSubCat"
			    Return conn.QuerySingleOrDefault(Of MedicineGroups)(sql, New With { .MedicineID = clsMedicineGroups.MedicineID })
			End Using
		End Function
	Public Function SelectByGroup(MedicineID As Integer) As IEnumerable(Of MedicineGroups)
		Using conn As New SqlConnection(ConnectionString)
			Return conn.Query(Of MedicineGroups)("Select * FROM MedicineGroups WHERE MedicineSubCat = @MedicineSubCat", New With {.MedicineID = MedicineID})
		End Using
	End Function

	Public Function Add(ByVal clsMedicineGroups As MedicineGroups) As Boolean
			Dim RowsAffected As Integer=0
			Using conn As New SqlConnection(ConnectionString)
			    Dim sql As String = "INSERT INTO MedicineGroups (MedicineFamily) VALUES (@MedicineFamily)" 
			    RowsAffected = conn.Execute(sql, New With { .MedicineFamily =  clsMedicineGroups.MedicineFamily })
			    Return RowsAffected > 0
			End Using
		End Function

		Public Function Update(oldMedicineGroups As MedicineGroups, newMedicineGroups As MedicineGroups) As Boolean
			Using conn As New SqlConnection(ConnectionString)
			    Dim parameters = New With { 
					.NewMedicineFamily = newMedicineGroups.MedicineFamily, .OldMedicineFamily = oldMedicineGroups.MedicineFamily
										  }
			    Dim affectedRows As Integer = conn.Execute("UPDATE [MedicineGroups] SET [MedicineFamily] = @NewMedicineFamily WHERE [MedicineFamily] = @OldMedicineFamily", parameters)
			    Return affectedRows > 0
			End Using
		End Function

		Public Function Delete(ByVal clsMedicineGroups As MedicineGroups) As Boolean
			Dim deleteStatement As String =
			"DELETE FROM [MedicineGroups] 
			WHERE MedicineSubCat = @MedicineSubCat"
			Using connection As SqlConnection = DentistXData.GetConnection()
			    connection.Open()
			    Dim affectedRows As Integer = connection.Execute(deleteStatement, New With { .MedicineID = clsMedicineGroups.MedicineID })
			    Return affectedRows > 0
			End Using
		End Function


'Methods to get parents and childs
		Public Function GetMedicineFamily(ByVal clsMedicineGroups As MedicineGroups ) As IEnumerable(Of MedicineFamily)
			Using conn As New SqlConnection(ConnectionString)
				conn.Open()
				Dim query As String = "SELECT * FROM [MedicineFamily] WHERE [MedicineSubCat] = @MedicineSubCat"
				Return conn.Query(Of MedicineFamily)(query, New With { .MedicineID= clsMedicineGroups.MedicineID })
			End Using
		End Function

	End Class
