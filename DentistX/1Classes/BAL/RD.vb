Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections
Imports System.Configuration
Imports Dapper

	Public Class RD


		Private m_RDID As Integer
		Property RDID As Integer
			Get
				Return m_RDID
			End Get
			Set(ByVal value As Integer)
				m_RDID = value
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

		Private m_RD1 As String
		Property RD1 As String
			Get
				Return m_RD1
			End Get
			Set(ByVal value As String)
				m_RD1 = value
			End Set
		End Property

		Private m_RD2 As String
		Property RD2 As String
			Get
				Return m_RD2
			End Get
			Set(ByVal value As String)
				m_RD2 = value
			End Set
		End Property

		Private m_RD3 As String
		Property RD3 As String
			Get
				Return m_RD3
			End Get
			Set(ByVal value As String)
				m_RD3 = value
			End Set
		End Property

		Private m_RD4 As String
		Property RD4 As String
			Get
				Return m_RD4
			End Get
			Set(ByVal value As String)
				m_RD4 = value
			End Set
		End Property

		Private m_RD5 As String
		Property RD5 As String
			Get
				Return m_RD5
			End Get
			Set(ByVal value As String)
				m_RD5 = value
			End Set
		End Property

		Private m_RD6 As String
		Property RD6 As String
			Get
				Return m_RD6
			End Get
			Set(ByVal value As String)
				m_RD6 = value
			End Set
		End Property

		Private m_RD7 As String
		Property RD7 As String
			Get
				Return m_RD7
			End Get
			Set(ByVal value As String)
				m_RD7 = value
			End Set
		End Property

		Private m_RD8 As String
		Property RD8 As String
			Get
				Return m_RD8
			End Get
			Set(ByVal value As String)
				m_RD8 = value
			End Set
		End Property

		

	End Class
