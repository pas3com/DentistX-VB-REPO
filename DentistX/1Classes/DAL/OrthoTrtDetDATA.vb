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
			Dim sql As String = "SELECT * FROM OrthoTrtDet WHERE DetID = @DetID AND PatientID = @PatientID ORDER BY [WorkDate] DESC"
			Return conn.QuerySingleOrDefault(Of OrthoTrtDet)(sql, New With {.DetID = clsOrthoTrtDet.DetID, .PatientID = clsOrthoTrtDet.PatientID})
		End Using
		End Function

		Private Shared Function EffectiveOrthoIdForDet(patientId As Integer, orthoId As Integer) As Integer
			If orthoId > 0 Then Return orthoId
			Return OrthoInfDATA.ResolveDefaultOrthoIdForPatient(patientId)
		End Function

		Private Shared Function EffectiveOrthoIdForDetUpdate(oldD As OrthoTrtDet, newD As OrthoTrtDet) As Integer
			If newD.OrthoID > 0 Then Return newD.OrthoID
			If oldD IsNot Nothing AndAlso oldD.OrthoID > 0 Then Return oldD.OrthoID
			Return OrthoInfDATA.ResolveDefaultOrthoIdForPatient(newD.PatientID)
		End Function

		Public Function Add(ByVal clsOrthoTrtDet As OrthoTrtDet) As Boolean
			Dim RowsAffected As Integer=0
			Dim orthoId As Integer = EffectiveOrthoIdForDet(clsOrthoTrtDet.PatientID, clsOrthoTrtDet.OrthoID)
			Using conn As New SqlConnection(ConnectionString)
			Dim sql As String = "INSERT INTO OrthoTrtDet (PatientID, OrthoID, DiagID, WorkDate, WireMeasure, WireType, WireImg, WireNotes) 
								VALUES (@PatientID, @OrthoID, @DiagID, @WorkDate, @WireMeasure, @WireType, @WireImg, @WireNotes)"
			RowsAffected = conn.Execute(sql, New With {.PatientID = clsOrthoTrtDet.PatientID, .OrthoID = orthoId, .DiagID = clsOrthoTrtDet.DiagID, .WorkDate = clsOrthoTrtDet.WorkDate,
										.WireMeasure = clsOrthoTrtDet.WireMeasure, .WireType = clsOrthoTrtDet.WireType,
										.WireImg = clsOrthoTrtDet.WireImg, .WireNotes = clsOrthoTrtDet.WireNotes})
			Return RowsAffected > 0
			End Using
		End Function

		Public Function Update(oldOrthoTrtDet As OrthoTrtDet, newOrthoTrtDet As OrthoTrtDet) As Boolean
			Using conn As New SqlConnection(ConnectionString)
			Dim newOrthoId As Integer = EffectiveOrthoIdForDetUpdate(oldOrthoTrtDet, newOrthoTrtDet)
			Dim parameters = New With {
					.NewPatientID = newOrthoTrtDet.PatientID, .OldPatientID = oldOrthoTrtDet.PatientID,
					.NewOrthoID = newOrthoId, .OldOrthoID = oldOrthoTrtDet.OrthoID,
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
													WHERE [DetID] = @OldDetID AND [PatientID] = @OldPatientID AND ISNULL([OrthoID], 0) = ISNULL(@OldOrthoID, 0)", parameters)
			Return affectedRows > 0
			End Using
		End Function

		Public Function Delete(ByVal clsOrthoTrtDet As OrthoTrtDet) As Boolean
			Dim deleteStatement As String =
			"DELETE FROM [OrthoTrtDet] 
			WHERE DetID = @DetID AND PatientID = @PatientID AND ISNULL(OrthoID, 0) = ISNULL(@OrthoID, 0)"
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
