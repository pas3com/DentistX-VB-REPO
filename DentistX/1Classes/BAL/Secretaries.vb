Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections
Imports System.Configuration
Imports Dapper

	Public Class Secretaries


		Private m_SecID As Integer
		Property SecID As Integer
			Get
				Return m_SecID
			End Get
			Set(ByVal value As Integer)
				m_SecID = value
			End Set
		End Property

		Private m_SecName As String
		Property SecName As String
			Get
				Return m_SecName
			End Get
			Set(ByVal value As String)
				m_SecName = value
			End Set
		End Property

		Private m_SecAdres As String
		Property SecAdres As String
			Get
				Return m_SecAdres
			End Get
			Set(ByVal value As String)
				m_SecAdres = value
			End Set
		End Property

		Private m_SecPhone As String
		Property SecPhone As String
			Get
				Return m_SecPhone
			End Get
			Set(ByVal value As String)
				m_SecPhone = value
			End Set
		End Property

		Private m_SecMobile As String
		Property SecMobile As String
			Get
				Return m_SecMobile
			End Get
			Set(ByVal value As String)
				m_SecMobile = value
			End Set
		End Property

		Private m_SecColor As String
		Property SecColor As String
			Get
				Return m_SecColor
			End Get
			Set(ByVal value As String)
				m_SecColor = value
			End Set
		End Property

				Private m_USERSIEnumerable As IEnumerable(Of USERS)
		Public Property USERSIEnumerable As IEnumerable(Of USERS)
			Get
				Return m_USERSIEnumerable
			End Get
			Set(ByVal value As IEnumerable(Of USERS))
				m_USERSIEnumerable = value
			End Set
		End Property

		Property WhatsAppPrefix As String
		Property WhatsApp As String

	End Class
