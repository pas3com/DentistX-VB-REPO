Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections
Imports System.Configuration
Imports Dapper

	Public Class TblWireType


		Private m_TypeID As Integer
		Property TypeID As Integer
			Get
				Return m_TypeID
			End Get
			Set(ByVal value As Integer)
				m_TypeID = value
			End Set
		End Property

		Private m_WireType As String
		Property WireType As String
			Get
				Return m_WireType
			End Get
			Set(ByVal value As String)
				m_WireType = value
			End Set
		End Property

		

	End Class
