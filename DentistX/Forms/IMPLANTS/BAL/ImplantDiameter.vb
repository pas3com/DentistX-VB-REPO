Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections
Imports System.Configuration
Imports Dapper

	Public Class ImplantDiameter


		Private m_DiameterID As Integer
		Property DiameterID As Integer
			Get
				Return m_DiameterID
			End Get
			Set(ByVal value As Integer)
				m_DiameterID = value
			End Set
		End Property

		Private m_DiameterMM As Decimal
		Property DiameterMM As Decimal
			Get
				Return m_DiameterMM
			End Get
			Set(ByVal value As Decimal)
				m_DiameterMM = value
			End Set
		End Property

		

	End Class
