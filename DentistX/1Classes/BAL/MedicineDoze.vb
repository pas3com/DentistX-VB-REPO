Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections
Imports System.Configuration
Imports Dapper

	Public Class MedicineDoze


		Private m_DozeID As Integer
		Property DozeID As Integer
			Get
				Return m_DozeID
			End Get
			Set(ByVal value As Integer)
				m_DozeID = value
			End Set
		End Property

		Private m_ShapeID As Integer
		Property ShapeID As Integer
			Get
				Return m_ShapeID
			End Get
			Set(ByVal value As Integer)
				m_ShapeID = value
			End Set
		End Property

		Private m_Doze As String
		Property Doze As String
			Get
				Return m_Doze
			End Get
			Set(ByVal value As String)
				m_Doze = value
			End Set
		End Property

		

	End Class
