Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections
Imports System.Configuration
Imports Dapper

	Public Class TblMasareefDATA

		Private ConnectionString As String=DentistXDATA.GetConnection.connectionString



		Public Function SelectAll() As IEnumerable(Of TblMasareef)
			Using conn As New SqlConnection(ConnectionString)
			    conn.Open()
			    Return conn.Query(Of TblMasareef)("SELECT * FROM TblMasareef")
			End Using
		End Function
		

		Public Function Select_Record(ByVal clsTblMasareef As TblMasareef) As TblMasareef
			Using conn As New SqlConnection(ConnectionString)
			    Dim sql As String = "Select * FROM TblMasareef WHERE MasrofID = @MasrofID"
			    Return conn.QuerySingleOrDefault(Of TblMasareef)(sql, New With { .MasrofID = clsTblMasareef.MasrofID })
			End Using
		End Function

		Public Function Add(ByVal clsTblMasareef As TblMasareef) As Boolean
			Dim RowsAffected As Integer=0
			Using conn As New SqlConnection(ConnectionString)
			    Dim sql As String = "INSERT INTO TblMasareef (MasrofDate, BandID, MasrofAmount, MasrofEx) VALUES (@MasrofDate, @BandID, @MasrofAmount, @MasrofEx)" 
			    RowsAffected = conn.Execute(sql, New With { .MasrofDate =  clsTblMasareef.MasrofDate, .BandID =  clsTblMasareef.BandID, .MasrofAmount =  clsTblMasareef.MasrofAmount, .MasrofEx =  clsTblMasareef.MasrofEx })
			    Return RowsAffected > 0
			End Using
		End Function

		Public Function Update(oldTblMasareef As TblMasareef, newTblMasareef As TblMasareef) As Boolean
			Using conn As New SqlConnection(ConnectionString)
			    Dim parameters = New With { 
					.NewMasrofDate = newTblMasareef.MasrofDate, .OldMasrofDate = oldTblMasareef.MasrofDate, .NewBandID = newTblMasareef.BandID, .OldBandID = oldTblMasareef.BandID, .NewMasrofAmount = newTblMasareef.MasrofAmount, .OldMasrofAmount = oldTblMasareef.MasrofAmount, .NewMasrofEx = newTblMasareef.MasrofEx, .OldMasrofEx = oldTblMasareef.MasrofEx
										  }
			    Dim affectedRows As Integer = conn.Execute("UPDATE [TblMasareef] SET [MasrofDate] = @NewMasrofDate, [BandID] = @NewBandID, [MasrofAmount] = @NewMasrofAmount, [MasrofEx] = @NewMasrofEx WHERE [MasrofDate] = @OldMasrofDate AND [BandID] = @OldBandID AND [MasrofAmount] = @OldMasrofAmount AND [MasrofEx] = @OldMasrofEx", parameters)
			    Return affectedRows > 0
			End Using
		End Function

		Public Function Delete(ByVal clsTblMasareef As TblMasareef) As Boolean
			Dim deleteStatement As String =
			"DELETE FROM [TblMasareef] 
			WHERE MasrofID = @MasrofID"
			Using connection As SqlConnection = DentistXData.GetConnection()
			    connection.Open()
			    Dim affectedRows As Integer = connection.Execute(deleteStatement, New With { .MasrofID = clsTblMasareef.MasrofID })
			    Return affectedRows > 0
			End Using
		End Function


'Methods to get parents and childs
		Public Function GetTblBnodMsareef(ByVal BandID As Integer) As TblBnodMsareef
		Dim parent As TblBnodMsareef = Nothing
		Using conn As New SqlConnection(ConnectionString)
			Dim query As String = "SELECT * FROM [TblBnodMsareef] WHERE [BandID] = @BandID"
			Try
				conn.Open()
				parent = conn.QuerySingleOrDefault(Of TblBnodMsareef)(query, New With {.BandID = BandID})
			Catch ex As Exception
				' Handle exceptions
			Finally
				If conn.State = ConnectionState.Open Then conn.Close()
			End Try
		End Using
		Return parent
		End Function

	End Class
