Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections
Imports System.Configuration
Imports Dapper

	Public Class Emp


		Private m_EmpID As Integer
		Property EmpID As Integer
			Get
				Return m_EmpID
			End Get
			Set(ByVal value As Integer)
				m_EmpID = value
			End Set
		End Property

		Private m_EmpName As String
		Property EmpName As String
			Get
				Return m_EmpName
			End Get
			Set(ByVal value As String)
				m_EmpName = value
			End Set
		End Property

		Private m_EmpPhone As String
		Property EmpPhone As String
			Get
				Return m_EmpPhone
			End Get
			Set(ByVal value As String)
				m_EmpPhone = value
			End Set
		End Property

		Private m_EmpAddress As String
		Property EmpAddress As String
			Get
				Return m_EmpAddress
			End Get
			Set(ByVal value As String)
				m_EmpAddress = value
			End Set
		End Property

		Private m_EmpImg As Byte()
		Property EmpImg As Byte()
			Get
				Return m_EmpImg
			End Get
			Set(ByVal value As Byte())
				m_EmpImg = value
			End Set
		End Property

		Property WhatsAppPrefix As String
		Property WhatsApp As String

	End Class
