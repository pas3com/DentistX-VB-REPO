Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections
Imports System.Configuration
Imports Dapper

	Public Class TblExpenPay


		Private m_ExpPayID As Integer
		Property ExpPayID As Integer
			Get
				Return m_ExpPayID
			End Get
			Set(ByVal value As Integer)
				m_ExpPayID = value
			End Set
		End Property

		Private m_MasrofID As Integer
		Property MasrofID As Integer
			Get
				Return m_MasrofID
			End Get
			Set(ByVal value As Integer)
				m_MasrofID = value
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
