Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections
Imports System.Configuration
Imports Dapper

	Public Class Y_TRT


		Private m_YName As Integer
		Property YName As Integer
			Get
				Return m_YName
			End Get
			Set(ByVal value As Integer)
				m_YName = value
			End Set
		End Property

		Private m_YYName As String
		Property YYName As String
			Get
				Return m_YYName
			End Get
			Set(ByVal value As String)
				m_YYName = value
			End Set
		End Property

		

	End Class
