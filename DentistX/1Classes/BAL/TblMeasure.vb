Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections
Imports System.Configuration
Imports Dapper

	Public Class TblMeasure


		Private m_MeasureID As Integer
		Property MeasureID As Integer
			Get
				Return m_MeasureID
			End Get
			Set(ByVal value As Integer)
				m_MeasureID = value
			End Set
		End Property

		Private m_Measure As String
		Property Measure As String
			Get
				Return m_Measure
			End Get
			Set(ByVal value As String)
				m_Measure = value
			End Set
		End Property

		

	End Class
