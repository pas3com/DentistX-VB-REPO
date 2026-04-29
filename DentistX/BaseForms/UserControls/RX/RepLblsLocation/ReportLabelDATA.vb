Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections
Imports System.Configuration
Imports Dapper

	Public Class ReportLabelDATA

		Private ConnectionString As String=DentistXDATA.GetConnection.connectionString



		Public Function SelectAll() As IEnumerable(Of ReportLabel)
			Using conn As New SqlConnection(ConnectionString)
			    conn.Open()
			    Return conn.Query(Of ReportLabel)("SELECT * FROM ReportLabel")
			End Using
		End Function
		

		Public Function Select_Record(ByVal clsReportLabel As ReportLabel) As ReportLabel
			Using conn As New SqlConnection(ConnectionString)
			    Dim sql As String = "Select * FROM ReportLabel WHERE LblID = @LblID"
			    Return conn.QuerySingleOrDefault(Of ReportLabel)(sql, New With { .LblID = clsReportLabel.LblID })
			End Using
		End Function

		Public Function Add(ByVal clsReportLabel As ReportLabel) As Boolean
			Dim RowsAffected As Integer=0
			Using conn As New SqlConnection(ConnectionString)
			    Dim sql As String = "INSERT INTO ReportLabel (LblName, OffsetXmm, OffsetYmm) VALUES (@LblName, @OffsetXmm, @OffsetYmm)" 
			    RowsAffected = conn.Execute(sql, New With { .LblName =  clsReportLabel.LblName, .OffsetXmm =  clsReportLabel.OffsetXmm, .OffsetYmm =  clsReportLabel.OffsetYmm })
			    Return RowsAffected > 0
			End Using
		End Function

		Public Function Update(oldReportLabel As ReportLabel, newReportLabel As ReportLabel) As Boolean
			Using conn As New SqlConnection(ConnectionString)
			    Dim parameters = New With { 
					.NewLblName = newReportLabel.LblName, .OldLblName = oldReportLabel.LblName, .NewOffsetXmm = newReportLabel.OffsetXmm, .OldOffsetXmm = oldReportLabel.OffsetXmm, .NewOffsetYmm = newReportLabel.OffsetYmm, .OldOffsetYmm = oldReportLabel.OffsetYmm
										  }
			    Dim affectedRows As Integer = conn.Execute("UPDATE [ReportLabel] SET [LblName] = @NewLblName, [OffsetXmm] = @NewOffsetXmm, [OffsetYmm] = @NewOffsetYmm WHERE [LblName] = @OldLblName AND [OffsetXmm] = @OldOffsetXmm AND [OffsetYmm] = @OldOffsetYmm", parameters)
			    Return affectedRows > 0
			End Using
		End Function

		Public Function Delete(ByVal clsReportLabel As ReportLabel) As Boolean
			Dim deleteStatement As String =
			"DELETE FROM [ReportLabel] 
			WHERE LblID = @LblID"
			Using connection As SqlConnection = DentistXData.GetConnection()
			    connection.Open()
			    Dim affectedRows As Integer = connection.Execute(deleteStatement, New With { .LblID = clsReportLabel.LblID })
			    Return affectedRows > 0
			End Using
		End Function


'Methods to get parents and childs
	End Class
