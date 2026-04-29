Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections
Imports System.Configuration
Imports Dapper

	Public Class GroupPermissions


		Private m_GroupID As Integer
		Property GroupID As Integer
			Get
				Return m_GroupID
			End Get
			Set(ByVal value As Integer)
				m_GroupID = value
			End Set
		End Property

		Private m_PermID As Integer
		Property PermID As Integer
			Get
				Return m_PermID
			End Get
			Set(ByVal value As Integer)
				m_PermID = value
			End Set
		End Property

		Private m_IsAllowed As Boolean
		Property IsAllowed As Boolean
			Get
				Return m_IsAllowed
			End Get
			Set(ByVal value As Boolean)
				m_IsAllowed = value
			End Set
		End Property

		

	End Class
