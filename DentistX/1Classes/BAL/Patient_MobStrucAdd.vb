Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections
Imports System.Configuration
Imports Dapper

	Public Class Patient_MobStrucAdd


		Private m_AddTothID As Integer
		Property AddTothID As Integer
			Get
				Return m_AddTothID
			End Get
			Set(ByVal value As Integer)
				m_AddTothID = value
			End Set
		End Property

		Private m_StrucID As Integer
		Property StrucID As Integer
			Get
				Return m_StrucID
			End Get
			Set(ByVal value As Integer)
				m_StrucID = value
			End Set
		End Property

		Private m_StrucName As String
		Property StrucName As String
			Get
				Return m_StrucName
			End Get
			Set(ByVal value As String)
				m_StrucName = value
			End Set
		End Property

		Private m_ToothLoc As String
		Property ToothLoc As String
			Get
				Return m_ToothLoc
			End Get
			Set(ByVal value As String)
				m_ToothLoc = value
			End Set
		End Property

		Private m_ToothNum As String
		Property ToothNum As String
			Get
				Return m_ToothNum
			End Get
			Set(ByVal value As String)
				m_ToothNum = value
			End Set
		End Property

		Private m_AddTothDate As DateTime
		Property AddTothDate As DateTime
			Get
				Return m_AddTothDate
			End Get
			Set(ByVal value As DateTime)
				m_AddTothDate = value
			End Set
		End Property

		

	End Class
