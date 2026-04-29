Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections
Imports System.Configuration
Imports Dapper

	Public Class DrWorkPay


		Private m_WorkPayID As Integer
		Property WorkPayID As Integer
			Get
				Return m_WorkPayID
			End Get
			Set(ByVal value As Integer)
				m_WorkPayID = value
			End Set
		End Property

		Private m_WorkID As Integer
		Property WorkID As Integer
			Get
				Return m_WorkID
			End Get
			Set(ByVal value As Integer)
				m_WorkID = value
			End Set
		End Property

		Private m_DrID As Integer
		Property DrID As Integer
			Get
				Return m_DrID
			End Get
			Set(ByVal value As Integer)
				m_DrID = value
			End Set
		End Property

		Private m_PayValue As Decimal
		Property PayValue As Decimal
			Get
				Return m_PayValue
			End Get
			Set(ByVal value As Decimal)
				m_PayValue = value
			End Set
		End Property

		Private m_PayDate As DateTime
		Property PayDate As DateTime
			Get
				Return m_PayDate
			End Get
			Set(ByVal value As DateTime)
				m_PayDate = value
			End Set
		End Property

		Private m_Notes As String
		Property Notes As String
			Get
				Return m_Notes
			End Get
			Set(ByVal value As String)
				m_Notes = value
			End Set
		End Property

		

	End Class
