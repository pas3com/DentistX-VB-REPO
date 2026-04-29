Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections
Imports System.Configuration
Imports Dapper

	Public Class M_TRT


		Private m_MNo As Integer
		Property MNo As Integer
			Get
				Return m_MNo
			End Get
			Set(ByVal value As Integer)
				m_MNo = value
			End Set
		End Property

		Private m_MName As String
		Property MName As String
			Get
				Return m_MName
			End Get
			Set(ByVal value As String)
				m_MName = value
			End Set
		End Property

		Private m_MTrt As Nullable(Of Decimal)
		Property MTrt As Nullable(Of Decimal)
			Get
				Return m_MTrt
			End Get
			Set(ByVal value As Nullable(Of Decimal))
				m_MTrt = value
			End Set
		End Property

		Private m_MPay As Nullable(Of Decimal)
		Property MPay As Nullable(Of Decimal)
			Get
				Return m_MPay
			End Get
			Set(ByVal value As Nullable(Of Decimal))
				m_MPay = value
			End Set
		End Property

		Private m_MRemain As Nullable(Of Decimal)
		Property MRemain As Nullable(Of Decimal)
			Get
				Return m_MRemain
			End Get
			Set(ByVal value As Nullable(Of Decimal))
				m_MRemain = value
			End Set
		End Property

		

	End Class
