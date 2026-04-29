Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections
Imports System.Configuration
Imports Dapper

	Public Class OrthoTrtDet



	Property DetID As Integer

	Property PatientID As Integer
	Property OrthoID As Integer
	Property DiagID As Integer

	Property WorkDate As Nullable(Of DateTime)

	Property WireMeasure As String

	Property WireType As String

	Property WireImg As String


	Property WireNotes As String

End Class
