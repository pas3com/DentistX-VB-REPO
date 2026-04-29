Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections
Imports System.Configuration
Imports Dapper

	Public Class Gender


		Private m_SID As Integer
		Property SID As Integer
			Get
				Return m_SID
			End Get
			Set(ByVal value As Integer)
				m_SID = value
			End Set
		End Property

		Private m_Sex As String
		Property Sex As String
			Get
				Return m_Sex
			End Get
			Set(ByVal value As String)
				m_Sex = value
			End Set
		End Property

		

	End Class
