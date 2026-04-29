Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections
Imports System.Configuration
Imports Dapper

	Public Class ImplantBrand


		Private m_BrandID As Integer
		Property BrandID As Integer
			Get
				Return m_BrandID
			End Get
			Set(ByVal value As Integer)
				m_BrandID = value
			End Set
		End Property

		Private m_BrandName As String
		Property BrandName As String
			Get
				Return m_BrandName
			End Get
			Set(ByVal value As String)
				m_BrandName = value
			End Set
		End Property

		

	End Class
