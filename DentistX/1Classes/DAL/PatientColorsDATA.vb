Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections
Imports System.Configuration
Imports Dapper

	Public Class PatientColorsDATA

		Private ConnectionString As String=DentistXDATA.GetConnection.connectionString



		Public Function SelectAll() As IEnumerable(Of PatientColors)
			Using conn As New SqlConnection(ConnectionString)
			    conn.Open()
			    Return conn.Query(Of PatientColors)("SELECT * FROM PatientColors")
			End Using
		End Function
		

		Public Function Select_Record(ByVal clsPatientColors As PatientColors) As PatientColors
			Using conn As New SqlConnection(ConnectionString)
			    Dim sql As String = "Select * FROM PatientColors WHERE ColorID = @ColorID"
			    Return conn.QuerySingleOrDefault(Of PatientColors)(sql, New With { .ColorID = clsPatientColors.ColorID })
			End Using
		End Function

		Public Function Add(ByVal clsPatientColors As PatientColors) As Boolean
			Dim RowsAffected As Integer=0
			Using conn As New SqlConnection(ConnectionString)
			    Dim sql As String = "INSERT INTO PatientColors (Color1, Color2, GradientIndex, AlphaValue, PatientID) VALUES (@Color1, @Color2, @GradientIndex, @AlphaValue, @PatientID)" 
			    RowsAffected = conn.Execute(sql, New With { .Color1 =  clsPatientColors.Color1, .Color2 =  clsPatientColors.Color2, .GradientIndex =  clsPatientColors.GradientIndex, .AlphaValue =  clsPatientColors.AlphaValue, .PatientID =  clsPatientColors.PatientID })
			    Return RowsAffected > 0
			End Using
		End Function

		Public Function Update(oldPatientColors As PatientColors, newPatientColors As PatientColors) As Boolean
			Using conn As New SqlConnection(ConnectionString)
			    Dim parameters = New With { 
					.NewColor1 = newPatientColors.Color1, .OldColor1 = oldPatientColors.Color1, .NewColor2 = newPatientColors.Color2, .OldColor2 = oldPatientColors.Color2, .NewGradientIndex = newPatientColors.GradientIndex, .OldGradientIndex = oldPatientColors.GradientIndex, .NewAlphaValue = newPatientColors.AlphaValue, .OldAlphaValue = oldPatientColors.AlphaValue, .NewPatientID = newPatientColors.PatientID, .OldPatientID = oldPatientColors.PatientID
										  }
			    Dim affectedRows As Integer = conn.Execute("UPDATE [PatientColors] SET [Color1] = @NewColor1, [Color2] = @NewColor2, [GradientIndex] = @NewGradientIndex, [AlphaValue] = @NewAlphaValue, [PatientID] = @NewPatientID WHERE [Color1] = @OldColor1 AND [Color2] = @OldColor2 AND [GradientIndex] = @OldGradientIndex AND [AlphaValue] = @OldAlphaValue AND [PatientID] = @OldPatientID", parameters)
			    Return affectedRows > 0
			End Using
		End Function

		Public Function Delete(ByVal clsPatientColors As PatientColors) As Boolean
			Dim deleteStatement As String =
			"DELETE FROM [PatientColors] 
			WHERE ColorID = @ColorID"
			Using connection As SqlConnection = DentistXData.GetConnection()
			    connection.Open()
			    Dim affectedRows As Integer = connection.Execute(deleteStatement, New With { .ColorID = clsPatientColors.ColorID })
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
