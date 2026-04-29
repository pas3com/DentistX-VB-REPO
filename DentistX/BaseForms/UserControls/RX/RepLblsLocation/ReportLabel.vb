Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections
Imports System.Configuration
Imports Dapper

	Public Class ReportLabel


		Private m_LblID As Integer
		Property LblID As Integer
			Get
				Return m_LblID
			End Get
			Set(ByVal value As Integer)
				m_LblID = value
			End Set
		End Property

		Private m_LblName As String
		Property LblName As String
			Get
				Return m_LblName
			End Get
			Set(ByVal value As String)
				m_LblName = value
			End Set
		End Property

		Private m_OffsetXmm As Decimal
		Property OffsetXmm As Decimal
			Get
				Return m_OffsetXmm
			End Get
			Set(ByVal value As Decimal)
				m_OffsetXmm = value
			End Set
		End Property

		Private m_OffsetYmm As Decimal
		Property OffsetYmm As Decimal
			Get
				Return m_OffsetYmm
			End Get
			Set(ByVal value As Decimal)
				m_OffsetYmm = value
			End Set
		End Property

		

	End Class
