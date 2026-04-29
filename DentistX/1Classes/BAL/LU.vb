Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections
Imports System.Configuration
Imports Dapper

	Public Class LU


		Private m_LUID As Integer
		Property LUID As Integer
			Get
				Return m_LUID
			End Get
			Set(ByVal value As Integer)
				m_LUID = value
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

		Private m_LU1 As String
		Property LU1 As String
			Get
				Return m_LU1
			End Get
			Set(ByVal value As String)
				m_LU1 = value
			End Set
		End Property

		Private m_LU2 As String
		Property LU2 As String
			Get
				Return m_LU2
			End Get
			Set(ByVal value As String)
				m_LU2 = value
			End Set
		End Property

		Private m_LU3 As String
		Property LU3 As String
			Get
				Return m_LU3
			End Get
			Set(ByVal value As String)
				m_LU3 = value
			End Set
		End Property

		Private m_LU4 As String
		Property LU4 As String
			Get
				Return m_LU4
			End Get
			Set(ByVal value As String)
				m_LU4 = value
			End Set
		End Property

		Private m_LU5 As String
		Property LU5 As String
			Get
				Return m_LU5
			End Get
			Set(ByVal value As String)
				m_LU5 = value
			End Set
		End Property

		Private m_LU6 As String
		Property LU6 As String
			Get
				Return m_LU6
			End Get
			Set(ByVal value As String)
				m_LU6 = value
			End Set
		End Property

		Private m_LU7 As String
		Property LU7 As String
			Get
				Return m_LU7
			End Get
			Set(ByVal value As String)
				m_LU7 = value
			End Set
		End Property

		Private m_LU8 As String
		Property LU8 As String
			Get
				Return m_LU8
			End Get
			Set(ByVal value As String)
				m_LU8 = value
			End Set
		End Property

		

	End Class
