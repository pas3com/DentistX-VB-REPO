Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections
Imports System.Configuration
Imports Dapper

	Public Class Health


		Private m_HID As Integer
		Property HID As Integer
			Get
				Return m_HID
			End Get
			Set(ByVal value As Integer)
				m_HID = value
			End Set
		End Property

		Private m_HealthStat As String
		Property HealthStat As String
			Get
				Return m_HealthStat
			End Get
			Set(ByVal value As String)
				m_HealthStat = value
			End Set
		End Property

		

	End Class
