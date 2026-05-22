Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections
Imports System.Configuration
Imports Dapper

	Public Class ImplantDiameterDATA

		Private ConnectionString As String=DentistXDATA.GetConnection.connectionString



		Public Function SelectAll() As IEnumerable(Of ImplantDiameter)
			Using conn As New SqlConnection(ConnectionString)
			    conn.Open()
			    Return conn.Query(Of ImplantDiameter)("SELECT * FROM ImplantDiameter")
			End Using
		End Function
		

		Public Function Select_Record(ByVal clsImplantDiameter As ImplantDiameter) As ImplantDiameter
			Using conn As New SqlConnection(ConnectionString)
			    Dim sql As String = "Select * FROM ImplantDiameter WHERE DiameterID = @DiameterID"
			    Return conn.QuerySingleOrDefault(Of ImplantDiameter)(sql, New With { .DiameterID = clsImplantDiameter.DiameterID })
			End Using
		End Function

		Public Function CountByDiameterMM(diameterMm As Decimal, Optional excludeDiameterId As Integer? = Nothing) As Integer
			Using conn As New SqlConnection(ConnectionString)
				conn.Open()
				Const sql As String =
					"SELECT COUNT(*) FROM ImplantDiameter WHERE DiameterMM = @DiameterMM AND (@ExcludeDiameterId IS NULL OR DiameterID <> @ExcludeDiameterId)"
				Return CInt(conn.ExecuteScalar(Of Integer)(sql, New With {.DiameterMM = diameterMm, .ExcludeDiameterId = excludeDiameterId}))
			End Using
		End Function

		Public Function Add(ByVal clsImplantDiameter As ImplantDiameter) As Boolean
			Dim RowsAffected As Integer=0
			Using conn As New SqlConnection(ConnectionString)
			    Dim sql As String = "INSERT INTO ImplantDiameter (DiameterMM) VALUES (@DiameterMM)" 
			    RowsAffected = conn.Execute(sql, New With { .DiameterMM =  clsImplantDiameter.DiameterMM })
			    Return RowsAffected > 0
			End Using
		End Function

		Public Function Update(oldImplantDiameter As ImplantDiameter, newImplantDiameter As ImplantDiameter) As Boolean
			Using conn As New SqlConnection(ConnectionString)
			    Dim affectedRows As Integer = conn.Execute(
					"UPDATE [ImplantDiameter] SET [DiameterMM] = @NewDiameterMM WHERE [DiameterID] = @DiameterID",
					New With {.NewDiameterMM = newImplantDiameter.DiameterMM, .DiameterID = oldImplantDiameter.DiameterID})
			    Return affectedRows > 0
			End Using
		End Function

		Public Function Delete(ByVal clsImplantDiameter As ImplantDiameter) As Boolean
			Dim deleteStatement As String =
			"DELETE FROM [ImplantDiameter] 
			WHERE DiameterID = @DiameterID"
			Using connection As SqlConnection = DentistXData.GetConnection()
			    connection.Open()
			    Dim affectedRows As Integer = connection.Execute(deleteStatement, New With { .DiameterID = clsImplantDiameter.DiameterID })
			    Return affectedRows > 0
			End Using
		End Function


'Methods to get parents and childs
	End Class
