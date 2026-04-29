Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections
Imports System.Configuration
Imports Dapper

	Public Class Resources


		Private m_UniqueID As Integer
		Property UniqueID As Integer
			Get
				Return m_UniqueID
			End Get
			Set(ByVal value As Integer)
				m_UniqueID = value
			End Set
		End Property

		Private m_ResourceID As Integer
		Property ResourceID As Integer
			Get
				Return m_ResourceID
			End Get
			Set(ByVal value As Integer)
				m_ResourceID = value
			End Set
		End Property

		Private m_ResourceName As String
		Property ResourceName As String
			Get
				Return m_ResourceName
			End Get
			Set(ByVal value As String)
				m_ResourceName = value
			End Set
		End Property

		Private m_Color As Nullable(Of Integer)
		Property Color As Nullable(Of Integer)
			Get
				Return m_Color
			End Get
			Set(ByVal value As Nullable(Of Integer))
				m_Color = value
			End Set
		End Property

		Private m_Image As Byte()
		Property Image As Byte()
			Get
				Return m_Image
			End Get
			Set(ByVal value As Byte())
				m_Image = value
			End Set
		End Property

		Private m_CustomField1 As String
		Property CustomField1 As String
			Get
				Return m_CustomField1
			End Get
			Set(ByVal value As String)
				m_CustomField1 = value
			End Set
		End Property

		

	End Class
