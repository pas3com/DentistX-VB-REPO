Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections
Imports System.Configuration
Imports Dapper

	Public Class ImplantType


		Private m_TypeID As Integer
		Property TypeID As Integer
			Get
				Return m_TypeID
			End Get
			Set(ByVal value As Integer)
				m_TypeID = value
			End Set
		End Property

		Private m_TypeName As String
		Property TypeName As String
			Get
				Return m_TypeName
			End Get
			Set(ByVal value As String)
				m_TypeName = value
			End Set
		End Property

		Private m_IsSlim As Boolean
		Property IsSlim As Boolean
			Get
				Return m_IsSlim
			End Get
			Set(ByVal value As Boolean)
				m_IsSlim = value
			End Set
		End Property

		

	End Class
