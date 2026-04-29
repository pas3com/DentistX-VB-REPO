Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections
Imports System.Configuration
Imports Dapper

	Public Class Visits


		Private m_VisitDetID As Integer
		Property VisitDetID As Integer
			Get
				Return m_VisitDetID
			End Get
			Set(ByVal value As Integer)
				m_VisitDetID = value
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

		Private m_VtID As Nullable(Of Integer)
		Property VtID As Nullable(Of Integer)
			Get
				Return m_VtID
			End Get
			Set(ByVal value As Nullable(Of Integer))
				m_VtID = value
			End Set
		End Property

    Property VisitType As String

    Private m_VisitDay As String
		Property VisitDay As String
			Get
				Return m_VisitDay
			End Get
			Set(ByVal value As String)
				m_VisitDay = value
			End Set
		End Property

		Private m_VisTime As String
		Property VisTime As String
			Get
				Return m_VisTime
			End Get
			Set(ByVal value As String)
				m_VisTime = value
			End Set
		End Property

		Private m_VisTimeEnd As String
		Property VisTimeEnd As String
			Get
				Return m_VisTimeEnd
			End Get
			Set(ByVal value As String)
				m_VisTimeEnd = value
			End Set
		End Property

		Private m_PatientName As String
		Property PatientName As String
			Get
				Return m_PatientName
			End Get
			Set(ByVal value As String)
				m_PatientName = value
			End Set
		End Property

		Private m_VisDetail As String
		Property VisDetail As String
			Get
				Return m_VisDetail
			End Get
			Set(ByVal value As String)
				m_VisDetail = value
			End Set
		End Property

		Private m_VisNotes As String
		Property VisNotes As String
			Get
				Return m_VisNotes
			End Get
			Set(ByVal value As String)
				m_VisNotes = value
			End Set
		End Property

		Private m_VisDateTime As Nullable(Of DateTime)
		Property VisDateTime As Nullable(Of DateTime)
			Get
				Return m_VisDateTime
			End Get
			Set(ByVal value As Nullable(Of DateTime))
				m_VisDateTime = value
			End Set
		End Property

		

	End Class
