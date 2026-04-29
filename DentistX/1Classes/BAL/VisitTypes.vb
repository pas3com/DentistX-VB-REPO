Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections
Imports System.Configuration
Imports Dapper

	Public Class VisitTypes


		Private m_VtID As Integer
		Property VtID As Integer
			Get
				Return m_VtID
			End Get
			Set(ByVal value As Integer)
				m_VtID = value
			End Set
		End Property

		Private m_VisitType As String
		Property VisitType As String
			Get
				Return m_VisitType
			End Get
			Set(ByVal value As String)
				m_VisitType = value
			End Set
		End Property

		Private m_VisitTypeAr As String
		Property VisitTypeAr As String
			Get
				Return m_VisitTypeAr
			End Get
			Set(ByVal value As String)
				m_VisitTypeAr = value
			End Set
		End Property

		

	End Class
