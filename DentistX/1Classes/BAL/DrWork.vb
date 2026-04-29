Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections
Imports System.Configuration
Imports Dapper

	Public Class DrWork



	Property WorkID As Integer
	Property DrName As String
	Property PatientName As String

	Property DrID As Integer

	Property PatientID As Nullable(Of Integer)

	Property WrkDate As Nullable(Of DateTime)

	Property WrkDetail As String

	Property WrkVal As Nullable(Of Decimal)

	Property PayVal As Nullable(Of Decimal)

	Property Imp As Nullable(Of Boolean)

	Property Orth As Nullable(Of Boolean)

	Property Surg As Nullable(Of Boolean)

	Property Notes As String




End Class
