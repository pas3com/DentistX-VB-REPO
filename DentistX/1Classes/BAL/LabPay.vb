Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections
Imports System.Configuration
Imports Dapper

	Public Class LabPay


		Private m_LabPayID As Integer
		Property LabPayID As Integer
			Get
				Return m_LabPayID
			End Get
			Set(ByVal value As Integer)
				m_LabPayID = value
			End Set
		End Property

		Private m_LabID As Integer
		Property LabID As Integer
			Get
				Return m_LabID
			End Get
			Set(ByVal value As Integer)
				m_LabID = value
			End Set
		End Property

		Private m_LabOrderID As Integer
		Property LabOrderID As Integer
			Get
				Return m_LabOrderID
			End Get
			Set(ByVal value As Integer)
				m_LabOrderID = value
			End Set
		End Property

		Private m_LabName As String
		Property LabName As String
			Get
				Return m_LabName
			End Get
			Set(ByVal value As String)
				m_LabName = value
			End Set
		End Property

		Private m_OrderDetails As String
		Property OrderDetails As String
			Get
				Return m_OrderDetails
			End Get
			Set(ByVal value As String)
				m_OrderDetails = value
			End Set
		End Property

		Private m_PayValue As Integer
		Property PayValue As Integer
			Get
				Return m_PayValue
			End Get
			Set(ByVal value As Integer)
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

		Private m_PayDetail As String
		Property PayDetail As String
			Get
				Return m_PayDetail
			End Get
			Set(ByVal value As String)
				m_PayDetail = value
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
