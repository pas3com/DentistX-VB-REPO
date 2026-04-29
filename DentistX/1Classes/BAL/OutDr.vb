Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections
Imports System.Configuration
Imports Dapper

	Public Class OutDr


		Private m_DrID As Integer
		Property DrID As Integer
			Get
				Return m_DrID
			End Get
			Set(ByVal value As Integer)
				m_DrID = value
			End Set
		End Property

		Private m_DrName As String
		Property DrName As String
			Get
				Return m_DrName
			End Get
			Set(ByVal value As String)
				m_DrName = value
			End Set
		End Property

		Private m_DrAdres As String
		Property DrAdres As String
			Get
				Return m_DrAdres
			End Get
			Set(ByVal value As String)
				m_DrAdres = value
			End Set
		End Property

		Private m_Drphone As String
		Property Drphone As String
			Get
				Return m_Drphone
			End Get
			Set(ByVal value As String)
				m_Drphone = value
			End Set
		End Property

		Private m_DrMobile As String
		Property DrMobile As String
			Get
				Return m_DrMobile
			End Get
			Set(ByVal value As String)
				m_DrMobile = value
			End Set
		End Property

		

	End Class
