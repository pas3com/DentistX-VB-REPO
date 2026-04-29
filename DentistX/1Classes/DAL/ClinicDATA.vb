Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections
Imports System.Configuration
Imports Dapper

Public Class ClinicDATA

	Private ConnectionString As String = DentistXDATA.GetConnection.ConnectionString



	Public Function SelectAll() As IEnumerable(Of Clinic)
		Using conn As New SqlConnection(ConnectionString)
			conn.Open()
			Return conn.Query(Of Clinic)("SELECT [ClinicID], [ClinicNameEn], [ClinicNameAr], [DrNameEn], [DrNameAr], [SpecialistEn], [SpecialistAr], [AddressEn], [AddressAr], [Phone], [Mobile], [Email], [ClinicLogo] FROM Clinic")
		End Using
	End Function


	Public Function Select_Record(ByVal ClinicID As Guid) As Clinic
		Using conn As New SqlConnection(ConnectionString)
			Dim sql As String = "SELECT * FROM Clinic WHERE ClinicID = @ClinicID"
			Return conn.QuerySingleOrDefault(Of Clinic)(sql, New With {.ClinicID = ClinicID})
		End Using
	End Function

	Public Function Add(ByVal clsClinic As Clinic) As Boolean
		Dim RowsAffected As Integer = 0
		Using conn As New SqlConnection(ConnectionString)
			Dim sql As String = "INSERT INTO Clinic (ClinicID, ClinicNameEn, ClinicNameAr, DrNameEn, DrNameAr, SpecialistEn, SpecialistAr, AddressEn, AddressAr, Phone, Mobile, Email, ClinicLogo) VALUES (@ClinicID, @ClinicNameEn, @ClinicNameAr, @DrNameEn, @DrNameAr, @SpecialistEn, @SpecialistAr, @AddressEn, @AddressAr, @Phone, @Mobile, @Email, @ClinicLogo)"
			RowsAffected = conn.Execute(sql, New With {.ClinicID = clsClinic.ClinicID, .ClinicNameEn = clsClinic.ClinicNameEn, .ClinicNameAr = clsClinic.ClinicNameAr, .DrNameEn = clsClinic.DrNameEn, .DrNameAr = clsClinic.DrNameAr, .SpecialistEn = clsClinic.SpecialistEn, .SpecialistAr = clsClinic.SpecialistAr, .AddressEn = clsClinic.AddressEn, .AddressAr = clsClinic.AddressAr, .Phone = clsClinic.Phone, .Mobile = clsClinic.Mobile, .Email = clsClinic.Email, .ClinicLogo = clsClinic.ClinicLogo})
			Return RowsAffected > 0
		End Using
	End Function

	Public Function Update(oldClinic As Clinic, newClinic As Clinic) As Boolean
		Using conn As New SqlConnection(ConnectionString)
			Dim parameters = New With {
					.NewClinicNameEn = newClinic.ClinicNameEn, .NewClinicNameAr = newClinic.ClinicNameAr, .NewDrNameEn = newClinic.DrNameEn, .NewDrNameAr = newClinic.DrNameAr, .NewSpecialistEn = newClinic.SpecialistEn, .NewSpecialistAr = newClinic.SpecialistAr, .NewAddressEn = newClinic.AddressEn, .NewAddressAr = newClinic.AddressAr, .NewPhone = newClinic.Phone, .NewMobile = newClinic.Mobile, .NewEmail = newClinic.Email, .NewClinicLogo = newClinic.ClinicLogo, .KeyClinicID = oldClinic.ClinicID
									  }
			Dim affectedRows As Integer = conn.Execute("UPDATE [Clinic] SET [ClinicNameEn] = @NewClinicNameEn, [ClinicNameAr] = @NewClinicNameAr, [DrNameEn] = @NewDrNameEn, [DrNameAr] = @NewDrNameAr, [SpecialistEn] = @NewSpecialistEn, [SpecialistAr] = @NewSpecialistAr, [AddressEn] = @NewAddressEn, [AddressAr] = @NewAddressAr, [Phone] = @NewPhone, [Mobile] = @NewMobile, [Email] = @NewEmail, [ClinicLogo] = @NewClinicLogo WHERE [ClinicID] = @KeyClinicID", parameters)
			Return affectedRows > 0
		End Using
	End Function

	Public Function Delete(ByVal clsClinic As Clinic) As Boolean
		Dim deleteStatement As String =
		"DELETE FROM [Clinic] 
		WHERE ClinicID = @ClinicID"
		Using connection As SqlConnection = DentistXDATA.GetConnection()
			connection.Open()
			Dim affectedRows As Integer = connection.Execute(deleteStatement, New With {.ClinicID = clsClinic.ClinicID})
			Return affectedRows > 0
		End Using
	End Function


	'Methods to get parents and childs
	' VB.NET Code Generation for Parent using Dapper
	' VB.NET Code Generation for Child
End Class
