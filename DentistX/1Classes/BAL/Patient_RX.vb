Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections
Imports System.Configuration
Imports Dapper

	Public Class Patient_RX


		Private m_RxID As Integer
		Property RxID As Integer
			Get
				Return m_RxID
			End Get
			Set(ByVal value As Integer)
				m_RxID = value
			End Set
		End Property

		Private m_PatientID As Integer
		Property PatientID As Integer
			Get
				Return m_PatientID
			End Get
			Set(ByVal value As Integer)
				m_PatientID = value
			End Set
		End Property

		Private m_RXDate As Nullable(Of DateTime)
		Property RXDate As Nullable(Of DateTime)
			Get
				Return m_RXDate
			End Get
			Set(ByVal value As Nullable(Of DateTime))
				m_RXDate = value
			End Set
		End Property

		Private m_RX As String
		Property RX As String
			Get
				Return m_RX
			End Get
			Set(ByVal value As String)
				m_RX = value
			End Set
		End Property

		

	End Class
