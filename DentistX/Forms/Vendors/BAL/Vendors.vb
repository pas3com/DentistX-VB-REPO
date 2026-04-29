Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections
Imports System.Configuration
Imports Dapper

	Public Class Vendors


		Private m_VendID As Integer
		Property VendID As Integer
			Get
				Return m_VendID
			End Get
			Set(ByVal value As Integer)
				m_VendID = value
			End Set
		End Property

		Private m_VendName As String
		Property VendName As String
			Get
				Return m_VendName
			End Get
			Set(ByVal value As String)
				m_VendName = value
			End Set
		End Property

		Private m_CityID As Nullable(Of Integer)
		Property CityID As Nullable(Of Integer)
			Get
				Return m_CityID
			End Get
			Set(ByVal value As Nullable(Of Integer))
				m_CityID = value
			End Set
		End Property

		Private m_VendAddress As String
		Property VendAddress As String
			Get
				Return m_VendAddress
			End Get
			Set(ByVal value As String)
				m_VendAddress = value
			End Set
		End Property

		Private m_Contacts As String
		Property Contacts As String
			Get
				Return m_Contacts
			End Get
			Set(ByVal value As String)
				m_Contacts = value
			End Set
		End Property

				Private m_VendorPaysIEnumerable As IEnumerable(Of VendorPays)
		Public Property VendorPaysIEnumerable As IEnumerable(Of VendorPays)
			Get
				Return m_VendorPaysIEnumerable
			End Get
			Set(ByVal value As IEnumerable(Of VendorPays))
				m_VendorPaysIEnumerable = value
			End Set
		End Property

		Private m_VendorSalesIEnumerable As IEnumerable(Of VendorSales)
		Public Property VendorSalesIEnumerable As IEnumerable(Of VendorSales)
			Get
				Return m_VendorSalesIEnumerable
			End Get
			Set(ByVal value As IEnumerable(Of VendorSales))
				m_VendorSalesIEnumerable = value
			End Set
		End Property



	End Class
