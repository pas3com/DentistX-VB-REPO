Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections
Imports System.Configuration
Imports Dapper

	Public Class ImplantLength


		Private m_LengthID As Integer
		Property LengthID As Integer
			Get
				Return m_LengthID
			End Get
			Set(ByVal value As Integer)
				m_LengthID = value
			End Set
		End Property

		Private m_LengthMM As Decimal
		Property LengthMM As Decimal
			Get
				Return m_LengthMM
			End Get
			Set(ByVal value As Decimal)
				m_LengthMM = value
			End Set
		End Property

		

	End Class
