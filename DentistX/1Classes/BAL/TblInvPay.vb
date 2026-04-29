Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections
Imports System.Configuration
Imports Dapper

	Public Class TblInvPay


		Private m_PayID As Integer
		Property PayID As Integer
			Get
				Return m_PayID
			End Get
			Set(ByVal value As Integer)
				m_PayID = value
			End Set
		End Property

		Private m_InvoiceID As Integer
		Property InvoiceID As Integer
			Get
				Return m_InvoiceID
			End Get
			Set(ByVal value As Integer)
				m_InvoiceID = value
			End Set
		End Property

		Private m_ResID As Integer
		Property ResID As Integer
			Get
				Return m_ResID
			End Get
			Set(ByVal value As Integer)
				m_ResID = value
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

		Private m_Amount As Decimal
		Property Amount As Decimal
			Get
				Return m_Amount
			End Get
			Set(ByVal value As Decimal)
				m_Amount = value
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

		Private ReadOnly m_InvRemain As Nullable(Of Decimal)
		ReadOnly Property InvRemain As Nullable(Of Decimal)
			Get
				Return m_InvRemain
			End Get
		End Property

		

	End Class
