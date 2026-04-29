Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections
Imports System.Configuration
Imports Dapper

	Public Class TblCities


		Private m_CityID As Integer
		Property CityID As Integer
			Get
				Return m_CityID
			End Get
			Set(ByVal value As Integer)
				m_CityID = value
			End Set
		End Property

		Private m_CityName As String
		Property CityName As String
			Get
				Return m_CityName
			End Get
			Set(ByVal value As String)
				m_CityName = value
			End Set
		End Property

				Private m_TblCustomersIEnumerable As IEnumerable(Of TblCustomers)
		Public Property TblCustomersIEnumerable As IEnumerable(Of TblCustomers)
			Get
				Return m_TblCustomersIEnumerable
			End Get
			Set(ByVal value As IEnumerable(Of TblCustomers))
				m_TblCustomersIEnumerable = value
			End Set
		End Property

		Private m_TblResourcesIEnumerable As IEnumerable(Of TblResources)
		Public Property TblResourcesIEnumerable As IEnumerable(Of TblResources)
			Get
				Return m_TblResourcesIEnumerable
			End Get
			Set(ByVal value As IEnumerable(Of TblResources))
				m_TblResourcesIEnumerable = value
			End Set
		End Property



	End Class
