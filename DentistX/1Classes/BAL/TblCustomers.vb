Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections
Imports System.Configuration
Imports Dapper

	Public Class TblCustomers


		Private m_CusID As Integer
		Property CusID As Integer
			Get
				Return m_CusID
			End Get
			Set(ByVal value As Integer)
				m_CusID = value
			End Set
		End Property

		Private m_CusName As String
		Property CusName As String
			Get
				Return m_CusName
			End Get
			Set(ByVal value As String)
				m_CusName = value
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

		Private m_Address As String
		Property Address As String
			Get
				Return m_Address
			End Get
			Set(ByVal value As String)
				m_Address = value
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

				Private m_TblSalesHeaderIEnumerable As IEnumerable(Of TblSalesHeader)
		Public Property TblSalesHeaderIEnumerable As IEnumerable(Of TblSalesHeader)
			Get
				Return m_TblSalesHeaderIEnumerable
			End Get
			Set(ByVal value As IEnumerable(Of TblSalesHeader))
				m_TblSalesHeaderIEnumerable = value
			End Set
		End Property



	End Class
