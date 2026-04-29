Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections
Imports System.Configuration
Imports Dapper

	Public Class EmpAtend


		Private m_AtndID As Integer
		Property AtndID As Integer
			Get
				Return m_AtndID
			End Get
			Set(ByVal value As Integer)
				m_AtndID = value
			End Set
		End Property

		Private m_EmpID As Integer
		Property EmpID As Integer
			Get
				Return m_EmpID
			End Get
			Set(ByVal value As Integer)
				m_EmpID = value
			End Set
		End Property

		Private m_AtnDay As DateTime
		Property AtnDay As DateTime
			Get
				Return m_AtnDay
			End Get
			Set(ByVal value As DateTime)
				m_AtnDay = value
			End Set
		End Property

		Private m_AtnNote As String
		Property AtnNote As String
			Get
				Return m_AtnNote
			End Get
			Set(ByVal value As String)
				m_AtnNote = value
			End Set
		End Property

		Private m_AbsPrsnt As Boolean
		Property AbsPrsnt As Boolean
			Get
				Return m_AbsPrsnt
			End Get
			Set(ByVal value As Boolean)
				m_AbsPrsnt = value
			End Set
		End Property

		

	End Class
