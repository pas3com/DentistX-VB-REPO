Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections
Imports System.Configuration
Imports Dapper

	Public Class EmpDATA

		Private ConnectionString As String=DentistXDATA.GetConnection.connectionString



		Public Function SelectAll() As IEnumerable(Of Emp)
			Using conn As New SqlConnection(ConnectionString)
			    conn.Open()
			    Return conn.Query(Of Emp)("SELECT * FROM Emp")
			End Using
		End Function
		

		Public Function Select_Record(ByVal clsEmp As Emp) As Emp
			Using conn As New SqlConnection(ConnectionString)
			    Dim sql As String = "Select * FROM Emp WHERE EmpID = @EmpID"
			    Return conn.QuerySingleOrDefault(Of Emp)(sql, New With { .EmpID = clsEmp.EmpID })
			End Using
		End Function
	Public Function GetEmpById(ByVal EmpID As Integer) As Emp
		Using conn As New SqlConnection(ConnectionString)
			Dim sql As String = "Select * FROM Emp WHERE EmpID = @EmpID"
			Return conn.QuerySingleOrDefault(Of Emp)(sql, New With {.EmpID = EmpID})
		End Using
	End Function
	Public Function GetEmpIdByName(ByVal EmpName As String) As Integer
		Using conn As New SqlConnection(ConnectionString)
			Dim sql As String = "Select EmpID FROM Emp WHERE EmpName = @EmpName"
			Return conn.QuerySingleOrDefault(Of Integer)(sql, New With {.EmpName = EmpName})
		End Using
	End Function
	Public Function Add(ByVal clsEmp As Emp) As Boolean
			Dim RowsAffected As Integer=0
			Using conn As New SqlConnection(ConnectionString)
			    Dim sql As String = "INSERT INTO Emp (EmpName, EmpPhone, EmpAddress, EmpImg, WhatsAppPrefix, WhatsApp) VALUES (@EmpName, @EmpPhone, @EmpAddress, @EmpImg, @WhatsAppPrefix, @WhatsApp)"
			    RowsAffected = conn.Execute(sql, New With {
			        .EmpName = clsEmp.EmpName, .EmpPhone = clsEmp.EmpPhone, .EmpAddress = clsEmp.EmpAddress, .EmpImg = clsEmp.EmpImg,
			        .WhatsAppPrefix = clsEmp.WhatsAppPrefix, .WhatsApp = clsEmp.WhatsApp
			    })
			    Return RowsAffected > 0
			End Using
		End Function

		Public Function Update(oldEmp As Emp, newEmp As Emp) As Boolean
			Using conn As New SqlConnection(ConnectionString)
			    Dim sql = "UPDATE [Emp] SET [EmpName]=@EmpName, [EmpPhone]=@EmpPhone, [EmpAddress]=@EmpAddress, [EmpImg]=@EmpImg, [WhatsAppPrefix]=@WhatsAppPrefix, [WhatsApp]=@WhatsApp WHERE [EmpID]=@EmpID"
			    Dim affectedRows As Integer = conn.Execute(sql, New With {
			        .EmpName = newEmp.EmpName, .EmpPhone = newEmp.EmpPhone, .EmpAddress = newEmp.EmpAddress, .EmpImg = newEmp.EmpImg,
			        .WhatsAppPrefix = newEmp.WhatsAppPrefix, .WhatsApp = newEmp.WhatsApp,
			        .EmpID = oldEmp.EmpID
			    })
			    Return affectedRows > 0
			End Using
		End Function

		Public Function Delete(ByVal clsEmp As Emp) As Boolean
			Dim deleteStatement As String =
			"DELETE FROM [Emp] 
			WHERE EmpID = @EmpID"
			Using connection As SqlConnection = DentistXData.GetConnection()
			    connection.Open()
			    Dim affectedRows As Integer = connection.Execute(deleteStatement, New With { .EmpID = clsEmp.EmpID })
			    Return affectedRows > 0
			End Using
		End Function


'Methods to get parents and childs
	End Class
