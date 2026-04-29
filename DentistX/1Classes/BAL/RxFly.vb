Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections
Imports System.Configuration
Imports Dapper

Public Class RxFly



	Property RxID As Integer
	Property PatientName As String
	Property PatientAge As Nullable(Of Integer)
	Property PatientSex As String
	Property RxDate As Nullable(Of DateTime)
	Property RX As String
	Property DrName As String
End Class
