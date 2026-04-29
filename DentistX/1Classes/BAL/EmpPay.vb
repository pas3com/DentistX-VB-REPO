Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections
Imports System.Configuration
Imports Dapper

	Public Class EmpPay


		Private m_UsSalID As Integer
		Property UsSalID As Integer
			Get
				Return m_UsSalID
			End Get
			Set(ByVal value As Integer)
				m_UsSalID = value
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

		Private m_MonthPay As Integer
		Property MonthPay As Integer
			Get
				Return m_MonthPay
			End Get
			Set(ByVal value As Integer)
				m_MonthPay = value
			End Set
		End Property

		Private m_DayPay As Integer
		Property DayPay As Integer
			Get
				Return m_DayPay
			End Get
			Set(ByVal value As Integer)
				m_DayPay = value
			End Set
		End Property

		Private m_FromDT As DateTime
		Property FromDT As DateTime
			Get
				Return m_FromDT
			End Get
			Set(ByVal value As DateTime)
				m_FromDT = value
			End Set
		End Property

		Private m_ToDT As DateTime
		Property ToDT As DateTime
			Get
				Return m_ToDT
			End Get
			Set(ByVal value As DateTime)
				m_ToDT = value
			End Set
		End Property

		Private m_DaysCount As Integer
		Property DaysCount As Integer
			Get
				Return m_DaysCount
			End Get
			Set(ByVal value As Integer)
				m_DaysCount = value
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

		Private m_PayNote As String
		Property PayNote As String
			Get
				Return m_PayNote
			End Get
			Set(ByVal value As String)
				m_PayNote = value
			End Set
		End Property

		

	End Class
