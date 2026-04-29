Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections
Imports System.Configuration
Imports Dapper

	Public Class OrthoTrtDetDATA

		Private ConnectionString As String=DentistXDATA.GetConnection.ConnectionString




	Public Function SelectAll() As IEnumerable(Of OrthoTrtDet)
			Using conn As New SqlConnection(ConnectionString)
			    conn.Open()
			Return conn.Query(Of OrthoTrtDet)("SELECT * FROM OrthoTrtDet  ORDER BY [WorkDate] DESC")
		End Using
		End Function
		

		Public Function Select_Record(ByVal clsOrthoTrtDet As OrthoTrtDet) As OrthoTrtDet
			Using conn As New SqlConnection(ConnectionString)
			Dim sql As String = "Select * FROM OrthoTrtDet WHERE DetID = @DetID And PatientID = @PatientID And OrthoID = @OrthoID  ORDER BY [WorkDate] DESC"
			Return conn.QuerySingleOrDefault(Of OrthoTrtDet)(sql, New With {.DetID = clsOrthoTrtDet.DetID, .PatientID = clsOrthoTrtDet.PatientID, .OrthoID = clsOrthoTrtDet.OrthoID})
		End Using
		End Function

		Public Function Add(ByVal clsOrthoTrtDet As OrthoTrtDet) As Boolean
			Dim RowsAffected As Integer=0
			Using conn As New SqlConnection(ConnectionString)
			Dim sql As String = "INSERT INTO OrthoTrtDet (PatientID, OrthoID, DiagID, WorkDate, WireMeasure, WireType, WireImg, WireNotes) 
								VALUES (@PatientID, @OrthoID, @DiagID, @WorkDate, @WireMeasure, @WireType, @WireImg, @WireNotes)"
			RowsAffected = conn.Execute(sql, New With {.PatientID = clsOrthoTrtDet.PatientID, .OrthoID = clsOrthoTrtDet.OrthoID, .DiagID = clsOrthoTrtDet.DiagID, .WorkDate = clsOrthoTrtDet.WorkDate,
										.WireMeasure = clsOrthoTrtDet.WireMeasure, .WireType = clsOrthoTrtDet.WireType,
										.WireImg = clsOrthoTrtDet.WireImg, .WireNotes = clsOrthoTrtDet.WireNotes})
			Return RowsAffected > 0
			End Using
		End Function

		Public Function Update(oldOrthoTrtDet As OrthoTrtDet, newOrthoTrtDet As OrthoTrtDet) As Boolean
			Using conn As New SqlConnection(ConnectionString)
			Dim parameters = New With {
					.NewPatientID = newOrthoTrtDet.PatientID, .OldPatientID = oldOrthoTrtDet.PatientID,
					.NewOrthoID = newOrthoTrtDet.OrthoID, .OldOrthoID = oldOrthoTrtDet.OrthoID,
					.NewDiagID = newOrthoTrtDet.DiagID, .OldDiagID = oldOrthoTrtDet.DiagID, .OldDetID = oldOrthoTrtDet.DetID,
					.NewWorkDate = newOrthoTrtDet.WorkDate, .OldWorkDate = oldOrthoTrtDet.WorkDate,
					.NewWireMeasure = newOrthoTrtDet.WireMeasure, .OldWireMeasure = oldOrthoTrtDet.WireMeasure,
					.NewWireType = newOrthoTrtDet.WireType, .OldWireType = oldOrthoTrtDet.WireType,
					.NewWireImg = newOrthoTrtDet.WireImg, .OldWireImg = oldOrthoTrtDet.WireImg,
					.newWireNotes = newOrthoTrtDet.WireNotes, .oldWireNotes = oldOrthoTrtDet.WireNotes
										  }
			Dim affectedRows As Integer = conn.Execute("UPDATE [OrthoTrtDet] SET [PatientID] = @NewPatientID, [OrthoID] = @NewOrthoID, [DiagID] = @NewDiagID, [WorkDate] = @NewWorkDate,
													[WireMeasure] = @NewWireMeasure, [WireType] = @NewWireType, [WireImg] = @NewWireImg,
													[WireNotes] = @NewWireNotes 
													WHERE [DetID] = @OldDetID AND [OrthoID] = @OldOrthoID AND [PatientID] = @OldPatientID", parameters)
			Return affectedRows > 0
			End Using
		End Function

		Public Function Delete(ByVal clsOrthoTrtDet As OrthoTrtDet) As Boolean
			Dim deleteStatement As String =
			"DELETE FROM [OrthoTrtDet] 
			WHERE DetID = @DetID AND OrthoID = @OrthoID AND PatientID = @PatientID"
			Using connection As SqlConnection = DentistXDATA.GetConnection()
				connection.Open()
				Dim affectedRows As Integer = connection.Execute(deleteStatement, New With {.DetID = clsOrthoTrtDet.DetID, .OrthoID = clsOrthoTrtDet.OrthoID, .PatientID = clsOrthoTrtDet.PatientID})
				Return affectedRows > 0
			End Using
		End Function

		''' <summary>
		''' Bulk delete of all OrthoTrtDet rows for a given PatientID + OrthoID.
		''' Used by OrthoTreatingCTL when deleting a whole treatment.
		''' </summary>
		Public Function DeleteByPatientAndOrtho(patientID As Integer, orthoID As Integer) As Integer
			Const deleteStatement As String =
				"DELETE FROM [OrthoTrtDet] WHERE PatientID = @PatientID AND OrthoID = @OrthoID"
			Using connection As SqlConnection = DentistXDATA.GetConnection()
				connection.Open()
				Return connection.Execute(deleteStatement, New With {.PatientID = patientID, .OrthoID = orthoID})
			End Using
		End Function


'Methods to get parents and childs
		Public Function SelectByDiagID(patientID As Integer, diagID As Integer) As IEnumerable(Of OrthoTrtDet)
			Using conn As New SqlConnection(ConnectionString)
				conn.Open()
				Dim sql As String = "SELECT * FROM OrthoTrtDet WHERE PatientID = @PatientID AND DiagID = @DiagID ORDER BY WorkDate DESC"
				Return conn.Query(Of OrthoTrtDet)(sql, New With {.PatientID = patientID, .DiagID = diagID})
			End Using
		End Function

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
