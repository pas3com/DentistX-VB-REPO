Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections
Imports System.Configuration
Imports Dapper

	Public Class CtlRep


		Private m_CtlID As Integer
		Property CtlID As Integer
			Get
				Return m_CtlID
			End Get
			Set(ByVal value As Integer)
				m_CtlID = value
			End Set
		End Property

		Private m_Ctl As String
		Property Ctl As String
			Get
				Return m_Ctl
			End Get
			Set(ByVal value As String)
				m_Ctl = value
			End Set
		End Property

		Private m_X As Decimal
		Property X As Decimal
			Get
				Return m_X
			End Get
			Set(ByVal value As Decimal)
				m_X = value
			End Set
		End Property

		Private m_Y As Decimal
		Property Y As Decimal
			Get
				Return m_Y
			End Get
			Set(ByVal value As Decimal)
				m_Y = value
			End Set
		End Property

		

	End Class
