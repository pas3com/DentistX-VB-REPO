Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections
Imports System.Configuration
Imports Dapper

	Public Class LD


		Private m_LDID As Integer
		Property LDID As Integer
			Get
				Return m_LDID
			End Get
			Set(ByVal value As Integer)
				m_LDID = value
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

		Private m_LD1 As String
		Property LD1 As String
			Get
				Return m_LD1
			End Get
			Set(ByVal value As String)
				m_LD1 = value
			End Set
		End Property

		Private m_LD2 As String
		Property LD2 As String
			Get
				Return m_LD2
			End Get
			Set(ByVal value As String)
				m_LD2 = value
			End Set
		End Property

		Private m_LD3 As String
		Property LD3 As String
			Get
				Return m_LD3
			End Get
			Set(ByVal value As String)
				m_LD3 = value
			End Set
		End Property

		Private m_LD4 As String
		Property LD4 As String
			Get
				Return m_LD4
			End Get
			Set(ByVal value As String)
				m_LD4 = value
			End Set
		End Property

		Private m_LD5 As String
		Property LD5 As String
			Get
				Return m_LD5
			End Get
			Set(ByVal value As String)
				m_LD5 = value
			End Set
		End Property

		Private m_LD6 As String
		Property LD6 As String
			Get
				Return m_LD6
			End Get
			Set(ByVal value As String)
				m_LD6 = value
			End Set
		End Property

		Private m_LD7 As String
		Property LD7 As String
			Get
				Return m_LD7
			End Get
			Set(ByVal value As String)
				m_LD7 = value
			End Set
		End Property

		Private m_LD8 As String
		Property LD8 As String
			Get
				Return m_LD8
			End Get
			Set(ByVal value As String)
				m_LD8 = value
			End Set
		End Property

		

	End Class
