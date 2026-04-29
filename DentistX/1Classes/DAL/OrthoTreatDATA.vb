Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections
Imports System.Configuration
Imports Dapper

Public Class OrthoTreatDATA

	Private ConnectionString As String = DentistXDATA.GetConnection.ConnectionString



	Public Function SelectAll() As IEnumerable(Of OrthoTreat)
		Using conn As New SqlConnection(ConnectionString)
			conn.Open()
			Return conn.Query(Of OrthoTreat)("SELECT * FROM OrthoTreat")
		End Using
	End Function


	Public Function Select_Record(ByVal clsOrthoTreat As OrthoTreat) As OrthoTreat
		Using conn As New SqlConnection(ConnectionString)
			Dim sql As String = "Select * FROM OrthoTreat WHERE TreatID = @TreatID AND  PatientID = @PatientID And OrthoID = @OrthoID"
			Return conn.QuerySingleOrDefault(Of OrthoTreat)(sql, New With {.TreatID = clsOrthoTreat.TreatID, .PatientID = clsOrthoTreat.PatientID, .OrthoID = clsOrthoTreat.OrthoID})
		End Using
	End Function

	Public Function Add(ByVal clsOrthoTreat As OrthoTreat) As Boolean
		Dim RowsAffected As Integer = 0
		Using conn As New SqlConnection(ConnectionString)
			Dim sql As String = "INSERT INTO OrthoTreat (PatientID, OrthoID, DiagID, BeginDate, OrthoType, ExtraUL, ExtraLL, ExtraUR, ExtraLR, FixerType, BraketType) VALUES (@PatientID, @OrthoID, @DiagID, @BeginDate, @OrthoType, @ExtraUL, @ExtraLL, @ExtraUR, @ExtraLR, @FixerType, @BraketType)"
			RowsAffected = conn.Execute(sql, New With {.PatientID = clsOrthoTreat.PatientID, .OrthoID = clsOrthoTreat.OrthoID, .DiagID = clsOrthoTreat.DiagID, .BeginDate = clsOrthoTreat.BeginDate, .OrthoType = clsOrthoTreat.OrthoType, .ExtraUL = clsOrthoTreat.ExtraUL, .ExtraLL = clsOrthoTreat.ExtraLL, .ExtraUR = clsOrthoTreat.ExtraUR, .ExtraLR = clsOrthoTreat.ExtraLR, .FixerType = clsOrthoTreat.FixerType, .BraketType = clsOrthoTreat.BraketType})
			Return RowsAffected > 0
		End Using
	End Function

	Public Function AddFull(ByVal clsOrthoTreat As OrthoTreat) As Boolean
		Dim RowsAffected As Integer = 0
		Using conn As New SqlConnection(ConnectionString)
			Dim sql As String = "INSERT INTO OrthoTreat (PatientID, OrthoID, BeginDate, OrthoType, ExtraUL, ExtraLL, ExtraUR, ExtraLR, FixerDate, FixerType, BraketType, FinishDate) VALUES (@PatientID, @OrthoID, @BeginDate, @OrthoType, @ExtraUL, @ExtraLL, @ExtraUR, @ExtraLR, @FixerDate, @FixerType, @BraketType, @FinishDate)"
			RowsAffected = conn.Execute(sql, New With {.PatientID = clsOrthoTreat.PatientID, .OrthoID = clsOrthoTreat.OrthoID, .BeginDate = clsOrthoTreat.BeginDate, .OrthoType = clsOrthoTreat.OrthoType, .ExtraUL = clsOrthoTreat.ExtraUL, .ExtraLL = clsOrthoTreat.ExtraLL, .ExtraUR = clsOrthoTreat.ExtraUR, .ExtraLR = clsOrthoTreat.ExtraLR, .FixerDate = clsOrthoTreat.FixerDate, .FixerType = clsOrthoTreat.FixerType, .BraketType = clsOrthoTreat.BraketType, .FinishDate = clsOrthoTreat.FinishDate})
			Return RowsAffected > 0
		End Using
	End Function

#Region "For OrthoTreatingCTL"

	''' <summary>
	''' Insert OrthoTreat row using only PatientID and OrthoID (no DiagID FK),
	''' matching the simplified flow used in OrthoTreatingCTL.
	''' </summary>
	Public Function AddForOrthoTreating(ByVal clsOrthoTreat As OrthoTreat) As Boolean
		Dim rowsAffected As Integer = 0
		Using conn As New SqlConnection(ConnectionString)
			Dim sql As String = "INSERT INTO OrthoTreat (PatientID, OrthoID, BeginDate, OrthoType, ExtraUL, ExtraLL, ExtraUR, ExtraLR, FixerType, BraketType) " &
								"VALUES (@PatientID, @OrthoID, @BeginDate, @OrthoType, @ExtraUL, @ExtraLL, @ExtraUR, @ExtraLR, @FixerType, @BraketType)"
			rowsAffected = conn.Execute(sql, New With {
				.PatientID = clsOrthoTreat.PatientID,
				.OrthoID = clsOrthoTreat.OrthoID,
				.BeginDate = clsOrthoTreat.BeginDate,
				.OrthoType = clsOrthoTreat.OrthoType,
				.ExtraUL = clsOrthoTreat.ExtraUL,
				.ExtraLL = clsOrthoTreat.ExtraLL,
				.ExtraUR = clsOrthoTreat.ExtraUR,
				.ExtraLR = clsOrthoTreat.ExtraLR,
				.FixerType = clsOrthoTreat.FixerType,
				.BraketType = clsOrthoTreat.BraketType
			})
			Return rowsAffected > 0
		End Using
	End Function

#End Region

	Public Function Update(oldOrthoTreat As OrthoTreat, newOrthoTreat As OrthoTreat) As Boolean
		Using conn As New SqlConnection(ConnectionString)
			Dim parameters = New With {
					.NewPatientID = newOrthoTreat.PatientID, .OldPatientID = oldOrthoTreat.PatientID, .NewOrthoID = newOrthoTreat.OrthoID, .OldOrthoID = oldOrthoTreat.OrthoID, .OldTreatID = oldOrthoTreat.TreatID, .NewBeginDate = newOrthoTreat.BeginDate, .OldBeginDate = oldOrthoTreat.BeginDate, .NewOrthoType = newOrthoTreat.OrthoType, .OldOrthoType = oldOrthoTreat.OrthoType, .NewExtraUL = newOrthoTreat.ExtraUL, .OldExtraUL = oldOrthoTreat.ExtraUL, .NewExtraLL = newOrthoTreat.ExtraLL, .OldExtraLL = oldOrthoTreat.ExtraLL, .NewExtraUR = newOrthoTreat.ExtraUR, .OldExtraUR = oldOrthoTreat.ExtraUR, .NewExtraLR = newOrthoTreat.ExtraLR, .OldExtraLR = oldOrthoTreat.ExtraLR, .NewFixerDate = newOrthoTreat.FixerDate, .OldFixerDate = oldOrthoTreat.FixerDate, .NewFixerType = newOrthoTreat.FixerType, .OldFixerType = oldOrthoTreat.FixerType, .NewBraketType = newOrthoTreat.BraketType, .OldBraketType = oldOrthoTreat.BraketType, .NewFinishDate = newOrthoTreat.FinishDate, .OldFinishDate = oldOrthoTreat.FinishDate
										  }
			Dim affectedRows As Integer = conn.Execute("UPDATE [OrthoTreat] SET [PatientID] = @NewPatientID, [OrthoID] = @NewOrthoID, [BeginDate] = @NewBeginDate, [OrthoType] = @NewOrthoType, [ExtraUL] = @NewExtraUL, [ExtraLL] = @NewExtraLL, [ExtraUR] = @NewExtraUR, [ExtraLR] = @NewExtraLR, [FixerDate] = @NewFixerDate, [FixerType] = @NewFixerType, [BraketType] = @NewBraketType, [FinishDate] = @NewFinishDate WHERE [TreatID] = @OldTreatID AND [OrthoID] = @OldOrthoID AND [PatientID] = @OldPatientID", parameters)
			Return affectedRows > 0
		End Using
	End Function

	Public Function Delete(ByVal clsOrthoTreat As OrthoTreat) As Boolean
		Dim deleteStatement As String =
			"DELETE FROM [OrthoTreat] 
			WHERE TreatID = @TreatID AND OrthoID = @OrthoID AND PatientID = @PatientID"
		Using connection As SqlConnection = DentistXDATA.GetConnection()
			connection.Open()
			Dim affectedRows As Integer = connection.Execute(deleteStatement, New With {.TreatID = clsOrthoTreat.TreatID, .OrthoID = clsOrthoTreat.OrthoID, .PatientID = clsOrthoTreat.PatientID})
			Return affectedRows > 0
		End Using
	End Function



	Public Function TrtCount(patientID As Integer) As Integer
		Dim query As String = "
        SELECT COUNT(TreatID) 
        FROM OrthoTreat 
        WHERE PatientID = @PatientID"

		Using connection As SqlConnection = DentistXDATA.GetConnection()
			connection.Open()
			Dim count As Integer? = connection.ExecuteScalar(Of Integer?)(query, New With {.PatientID = patientID})
			Return If(count.HasValue, count.Value, 0)
		End Using
	End Function
	Public Function TrtCountByOrthoID(patientID As Integer, OrthoID As Integer) As Integer
		Dim query As String = "
        SELECT COUNT(TreatID) 
        FROM OrthoTreat 
        WHERE PatientID = @PatientID AND  OrthoID = @OrthoID"

		Using connection As SqlConnection = DentistXDATA.GetConnection()
			connection.Open()
			Dim count As Integer? = connection.ExecuteScalar(Of Integer?)(query, New With {.PatientID = patientID, .OrthoID = OrthoID})
			Return If(count.HasValue, count.Value, 0)
		End Using
	End Function

	Public Function TrtCountByDiagID(patientID As Integer, diagID As Integer) As Integer
		Dim query As String = "SELECT COUNT(TreatID) FROM OrthoTreat WHERE PatientID = @PatientID AND DiagID = @DiagID"
		Using connection As SqlConnection = DentistXDATA.GetConnection()
			connection.Open()
			Dim count As Integer? = connection.ExecuteScalar(Of Integer?)(query, New With {.PatientID = patientID, .DiagID = diagID})
			Return If(count.HasValue, count.Value, 0)
		End Using
	End Function

	Public Function SelectByDiagID(patientID As Integer, diagID As Integer) As OrthoTreat
		Using conn As New SqlConnection(ConnectionString)
			conn.Open()
			Dim sql As String = "SELECT TOP 1 * FROM OrthoTreat WHERE PatientID = @PatientID AND DiagID = @DiagID"
			Return conn.QuerySingleOrDefault(Of OrthoTreat)(sql, New With {.PatientID = patientID, .DiagID = diagID})
		End Using
	End Function

	''' <summary>
	''' Bulk delete of OrthoTreat rows for a given PatientID + OrthoID.
	''' Primarily used by OrthoTreatingCTL to remove a whole treatment record.
	''' </summary>
	Public Function DeleteByPatientAndOrtho(patientID As Integer, orthoID As Integer) As Integer
		Const deleteStatement As String =
			"DELETE FROM [OrthoTreat] WHERE PatientID = @PatientID AND OrthoID = @OrthoID"
		Using connection As SqlConnection = DentistXDATA.GetConnection()
			connection.Open()
			Return connection.Execute(deleteStatement, New With {.PatientID = patientID, .OrthoID = orthoID})
		End Using
	End Function


	Public Function InfCount(patientID) As Integer
		Dim query As String =
			"SELECT COUNT(PatientID) FROM OrthoInf
             WHERE PatientID = @Patientid"
		Using connection As SqlConnection = DentistXDATA.GetConnection()
			connection.Open()
			Dim count As Integer? = connection.ExecuteScalar(Of Integer?)(query, New With {.PatientID = patientID})
			Return If(count.HasValue, count.Value, 0)
		End Using
	End Function

	Public Function DiagCount(patientID) As Integer
		Dim query As String =
			"SELECT COUNT(PatientID) FROM OrthoDiag
             WHERE PatientID = @Patientid"
		Using connection As SqlConnection = DentistXDATA.GetConnection()
			connection.Open()
			Dim count As Integer? = connection.ExecuteScalar(Of Integer?)(query, New With {.PatientID = patientID})
			Return If(count.HasValue, count.Value, 0)
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
