Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections
Imports System.Configuration
Imports Dapper

	Public Class ImpClrs


		Private m_ImpClrID As Integer
		Property ImpClrID As Integer
			Get
				Return m_ImpClrID
			End Get
			Set(ByVal value As Integer)
				m_ImpClrID = value
			End Set
		End Property

		Private m_ImpClr As String
		Property ImpClr As String
			Get
				Return m_ImpClr
			End Get
			Set(ByVal value As String)
				m_ImpClr = value
			End Set
		End Property

		

	End Class
