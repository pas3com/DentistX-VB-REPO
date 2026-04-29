Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections
Imports System.Configuration
Imports Dapper

	Public Class Groups


		Private m_GroupID As Integer
		Property GroupID As Integer
			Get
				Return m_GroupID
			End Get
			Set(ByVal value As Integer)
				m_GroupID = value
			End Set
		End Property

		Private m_GroupName As String
		Property GroupName As String
			Get
				Return m_GroupName
			End Get
			Set(ByVal value As String)
				m_GroupName = value
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



	End Class
