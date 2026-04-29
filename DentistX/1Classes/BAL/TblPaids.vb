Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections
Imports System.Configuration
Imports Dapper

	Public Class TblPaids


		Private m_PayID As Integer
		Property PayID As Integer
			Get
				Return m_PayID
			End Get
			Set(ByVal value As Integer)
				m_PayID = value
			End Set
		End Property

		Private m_PayType As String
		Property PayType As String
			Get
				Return m_PayType
			End Get
			Set(ByVal value As String)
				m_PayType = value
			End Set
		End Property

		Private m_PayDate As Nullable(Of DateTime)
		Property PayDate As Nullable(Of DateTime)
			Get
				Return m_PayDate
			End Get
			Set(ByVal value As Nullable(Of DateTime))
				m_PayDate = value
			End Set
		End Property

		Private m_ResCusId As Nullable(Of Integer)
		Property ResCusId As Nullable(Of Integer)
			Get
				Return m_ResCusId
			End Get
			Set(ByVal value As Nullable(Of Integer))
				m_ResCusId = value
			End Set
		End Property

		Private m_PayAmount As Nullable(Of Decimal)
		Property PayAmount As Nullable(Of Decimal)
			Get
				Return m_PayAmount
			End Get
			Set(ByVal value As Nullable(Of Decimal))
				m_PayAmount = value
			End Set
		End Property

		Private m_PayEx As String
		Property PayEx As String
			Get
				Return m_PayEx
			End Get
			Set(ByVal value As String)
				m_PayEx = value
			End Set
		End Property

		

	End Class
