Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections
Imports System.Configuration
Imports Dapper

	Public Class OrthoTreat



	Property TreatID As Integer
	Property OrthoID As Integer
	Property DiagID As Integer

	Property PatientID As Integer

	Property BeginDate As Nullable(Of DateTime)

	Property OrthoType As String

	Property ExtraUL As String

	Property ExtraLL As String

	Property ExtraUR As String

	Property ExtraLR As String

	Property FixerDate As Nullable(Of DateTime)

	Property FixerType As String

	Property BraketType As String

	Property FinishDate As Nullable(Of DateTime)




End Class
