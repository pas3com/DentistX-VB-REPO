Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections
Imports System.Configuration
Imports Dapper

	Public Class TblResources


		Private m_ResID As Integer
		Property ResID As Integer
			Get
				Return m_ResID
			End Get
			Set(ByVal value As Integer)
				m_ResID = value
			End Set
		End Property

		Private m_ResName As String
		Property ResName As String
			Get
				Return m_ResName
			End Get
			Set(ByVal value As String)
				m_ResName = value
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

		Private ReadOnly m_ResInvsNet As Nullable(Of Decimal)
		ReadOnly Property ResInvsNet As Nullable(Of Decimal)
			Get
				Return m_ResInvsNet
			End Get
		End Property

		Private ReadOnly m_ResTotalPays As Nullable(Of Decimal)
		ReadOnly Property ResTotalPays As Nullable(Of Decimal)
			Get
				Return m_ResTotalPays
			End Get
		End Property

		Private ReadOnly m_ResBal As Nullable(Of Decimal)
		ReadOnly Property ResBal As Nullable(Of Decimal)
			Get
				Return m_ResBal
			End Get
		End Property

				Private m_TblInvoicesHeaderIEnumerable As IEnumerable(Of TblInvoicesHeader)
		Public Property TblInvoicesHeaderIEnumerable As IEnumerable(Of TblInvoicesHeader)
			Get
				Return m_TblInvoicesHeaderIEnumerable
			End Get
			Set(ByVal value As IEnumerable(Of TblInvoicesHeader))
				m_TblInvoicesHeaderIEnumerable = value
			End Set
		End Property

		Private m_TblInvPayIEnumerable As IEnumerable(Of TblInvPay)
		Public Property TblInvPayIEnumerable As IEnumerable(Of TblInvPay)
			Get
				Return m_TblInvPayIEnumerable
			End Get
			Set(ByVal value As IEnumerable(Of TblInvPay))
				m_TblInvPayIEnumerable = value
			End Set
		End Property



	End Class
