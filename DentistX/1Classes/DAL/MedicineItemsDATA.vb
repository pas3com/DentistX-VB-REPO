Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections
Imports System.Configuration
Imports Dapper

	Public Class MedicineItemsDATA

		Private ConnectionString As String=DentistXDATA.GetConnection.connectionString



		Public Function SelectAll() As IEnumerable(Of MedicineItems)
			Using conn As New SqlConnection(ConnectionString)
			    conn.Open()
			    Return conn.Query(Of MedicineItems)("SELECT * FROM MedicineItems")
			End Using
		End Function
		

		Public Function Select_Record(ByVal clsMedicineItems As MedicineItems) As MedicineItems
			Using conn As New SqlConnection(ConnectionString)
			    Dim sql As String = "Select * FROM MedicineItems WHERE MedicineItemID = @MedicineItemID"
			    Return conn.QuerySingleOrDefault(Of MedicineItems)(sql, New With { .MedicineItemID = clsMedicineItems.MedicineItemID })
			End Using
		End Function
	Public Function SelectByMedicineItemID(MedicineItemID As Integer) As IEnumerable(Of MedicineItems)
		Using conn As New SqlConnection(ConnectionString)
			Return conn.Query(Of MedicineItems)("SELECT * FROM MedicineItems WHERE MedicineItemID = @MedicineItemID", New With {.MedicineItemID = MedicineItemID})
		End Using
	End Function
	Public Function Add(ByVal clsMedicineItems As MedicineItems) As Boolean
			Dim RowsAffected As Integer=0
			Using conn As New SqlConnection(ConnectionString)
			    Dim sql As String = "INSERT INTO MedicineItems (ScincID, CommName, Company, Notes) VALUES (@ScincID, @CommName, @Company, @Notes)" 
			    RowsAffected = conn.Execute(sql, New With { .ScincID =  clsMedicineItems.ScincID, .CommName =  clsMedicineItems.CommName, .Company =  clsMedicineItems.Company, .Notes =  clsMedicineItems.Notes })
			    Return RowsAffected > 0
			End Using
		End Function

		Public Function Update(oldMedicineItems As MedicineItems, newMedicineItems As MedicineItems) As Boolean
			Using conn As New SqlConnection(ConnectionString)
			    Dim parameters = New With { 
					.NewScincID = newMedicineItems.ScincID, .OldScincID = oldMedicineItems.ScincID, .NewCommName = newMedicineItems.CommName, .OldCommName = oldMedicineItems.CommName, .NewCompany = newMedicineItems.Company, .OldCompany = oldMedicineItems.Company, .NewNotes = newMedicineItems.Notes, .OldNotes = oldMedicineItems.Notes
										  }
			    Dim affectedRows As Integer = conn.Execute("UPDATE [MedicineItems] SET [ScincID] = @NewScincID, [CommName] = @NewCommName, [Company] = @NewCompany, [Notes] = @NewNotes WHERE [ScincID] = @OldScincID AND [CommName] = @OldCommName AND [Company] = @OldCompany AND [Notes] = @OldNotes", parameters)
			    Return affectedRows > 0
			End Using
		End Function

		Public Function Delete(ByVal clsMedicineItems As MedicineItems) As Boolean
			Dim deleteStatement As String =
			"DELETE FROM [MedicineItems] 
			WHERE MedicineItemID = @MedicineItemID"
			Using connection As SqlConnection = DentistXData.GetConnection()
			    connection.Open()
			    Dim affectedRows As Integer = connection.Execute(deleteStatement, New With { .MedicineItemID = clsMedicineItems.MedicineItemID })
			    Return affectedRows > 0
			End Using
		End Function


'Methods to get parents and childs
		Public Function GetMedScienceFamily(ByVal ScincID As Integer) As MedScienceFamily
		Dim parent As MedScienceFamily = Nothing
		Using conn As New SqlConnection(ConnectionString)
			Dim query As String = "SELECT * FROM [MedScienceFamily] WHERE [ScincID] = @ScincID"
			Try
				conn.Open()
				parent = conn.QuerySingleOrDefault(Of MedScienceFamily)(query, New With {.ScincID = ScincID})
			Catch ex As Exception
				' Handle exceptions
			Finally
				If conn.State = ConnectionState.Open Then conn.Close()
			End Try
		End Using
		Return parent
		End Function

		Public Function GetMedicineShape(ByVal clsMedicineItems As MedicineItems ) As IEnumerable(Of MedicineShape)
			Using conn As New SqlConnection(ConnectionString)
				conn.Open()
				Dim query As String = "SELECT * FROM [MedicineShape] WHERE [MedicineItemID] = @MedicineItemID"
				Return conn.Query(Of MedicineShape)(query, New With { .MedicineItemID= clsMedicineItems.MedicineItemID })
			End Using
		End Function

	End Class
