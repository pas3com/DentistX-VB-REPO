Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections
Imports System.Configuration
Imports Dapper

	Public Class USERS



	Property UsID As Integer

	Property UsName As String

	Property UsPassHash As Byte()

	Property UsSalt As Byte()

	Property GroupID As Integer

	Property UsLvl As Integer
	Property UsGrp As String
	Property DrID As Nullable(Of Integer)
	Property SecID As Nullable(Of Integer)

	Property EmpID As Nullable(Of Integer)

End Class
