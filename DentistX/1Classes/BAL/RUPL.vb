Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections
Imports System.Configuration
Imports Dapper

	Public Class RUPL


		Private m_RUcellID As Integer
		Property RUcellID As Integer
			Get
				Return m_RUcellID
			End Get
			Set(ByVal value As Integer)
				m_RUcellID = value
			End Set
		End Property

		Private m_PatientID As Integer
		Property PatientID As Integer
			Get
				Return m_PatientID
			End Get
			Set(ByVal value As Integer)
				m_PatientID = value
			End Set
		End Property

		Private m_CellAddres As Integer
		Property CellAddres As Integer
			Get
				Return m_CellAddres
			End Get
			Set(ByVal value As Integer)
				m_CellAddres = value
			End Set
		End Property

		Private m_ForeColor As String
		Property ForeColor As String
			Get
				Return m_ForeColor
			End Get
			Set(ByVal value As String)
				m_ForeColor = value
			End Set
		End Property

		

	End Class
