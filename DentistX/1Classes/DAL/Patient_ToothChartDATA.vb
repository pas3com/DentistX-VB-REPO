Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections
Imports System.Configuration
Imports Dapper

	Public Class Patient_ToothChartDATA

		Private ConnectionString As String=DentistXDATA.GetConnection.connectionString



		Public Function SelectAll() As IEnumerable(Of Patient_ToothChart)
			Using conn As New SqlConnection(ConnectionString)
			    conn.Open()
			    Return conn.Query(Of Patient_ToothChart)("SELECT * FROM Patient_ToothChart")
			End Using
		End Function
		

		Public Function Select_Record(ByVal clsPatient_ToothChart As Patient_ToothChart) As Patient_ToothChart
			Using conn As New SqlConnection(ConnectionString)
			    Dim sql As String = "Select * FROM Patient_ToothChart WHERE ChartID = @ChartID And PatientID = @PatientID"
			    Return conn.QuerySingleOrDefault(Of Patient_ToothChart)(sql, New With { .ChartID = clsPatient_ToothChart.ChartID, .PatientID = clsPatient_ToothChart.PatientID })
			End Using
		End Function

		Public Function Add(ByVal clsPatient_ToothChart As Patient_ToothChart) As Boolean
			Dim RowsAffected As Integer=0
			Using conn As New SqlConnection(ConnectionString)
			    Dim sql As String = "INSERT INTO Patient_ToothChart (PatientID, ToothNum, SVG, Treat, StageColor) VALUES (@PatientID, @ToothNum, @SVG, @Treat, @StageColor)" 
			    RowsAffected = conn.Execute(sql, New With { .PatientID =  clsPatient_ToothChart.PatientID, .ToothNum =  clsPatient_ToothChart.ToothNum, .SVG =  clsPatient_ToothChart.SVG, .Treat =  clsPatient_ToothChart.Treat, .StageColor =  clsPatient_ToothChart.StageColor })
			    Return RowsAffected > 0
			End Using
		End Function

		Public Function Update(oldPatient_ToothChart As Patient_ToothChart, newPatient_ToothChart As Patient_ToothChart) As Boolean
			Using conn As New SqlConnection(ConnectionString)
			    Dim parameters = New With { 
					.NewPatientID = newPatient_ToothChart.PatientID, .OldPatientID = oldPatient_ToothChart.PatientID, .NewToothNum = newPatient_ToothChart.ToothNum, .OldToothNum = oldPatient_ToothChart.ToothNum, .NewSVG = newPatient_ToothChart.SVG, .OldSVG = oldPatient_ToothChart.SVG, .NewTreat = newPatient_ToothChart.Treat, .OldTreat = oldPatient_ToothChart.Treat, .NewStageColor = newPatient_ToothChart.StageColor, .OldStageColor = oldPatient_ToothChart.StageColor
										  }
			    Dim affectedRows As Integer = conn.Execute("UPDATE [Patient_ToothChart] SET [PatientID] = @NewPatientID, [ToothNum] = @NewToothNum, [SVG] = @NewSVG, [Treat] = @NewTreat, [StageColor] = @NewStageColor WHERE [PatientID] = @OldPatientID AND [ToothNum] = @OldToothNum AND [SVG] = @OldSVG AND [Treat] = @OldTreat AND [StageColor] = @OldStageColor", parameters)
			    Return affectedRows > 0
			End Using
		End Function

		Public Function Delete(ByVal clsPatient_ToothChart As Patient_ToothChart) As Boolean
			Dim deleteStatement As String =
			"DELETE FROM [Patient_ToothChart] 
			WHERE ChartID = @ChartID AND PatientID = @PatientID"
			Using connection As SqlConnection = DentistXData.GetConnection()
			    connection.Open()
			    Dim affectedRows As Integer = connection.Execute(deleteStatement, New With { .ChartID = clsPatient_ToothChart.ChartID, .PatientID = clsPatient_ToothChart.PatientID })
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
