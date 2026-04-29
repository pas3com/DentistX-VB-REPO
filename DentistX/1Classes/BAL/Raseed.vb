Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections
Imports System.Configuration
Imports Dapper

	Public Class Raseed


		Private m_PatientID As Integer
		Property PatientID As Integer
			Get
				Return m_PatientID
			End Get
			Set(ByVal value As Integer)
				m_PatientID = value
			End Set
		End Property

		Private m_LastBal As Nullable(Of Decimal)
		Property LastBal As Nullable(Of Decimal)
			Get
				Return m_LastBal
			End Get
			Set(ByVal value As Nullable(Of Decimal))
				m_LastBal = value
			End Set
		End Property

		Private ReadOnly m_Bal As Decimal
		ReadOnly Property Bal As Decimal
			Get
				Return m_Bal
			End Get
		End Property

		

	End Class
