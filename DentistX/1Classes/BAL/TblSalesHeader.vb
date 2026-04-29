Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections
Imports System.Configuration
Imports Dapper

	Public Class TblSalesHeader


		Private m_SaleID As Integer
		Property SaleID As Integer
			Get
				Return m_SaleID
			End Get
			Set(ByVal value As Integer)
				m_SaleID = value
			End Set
		End Property

		Private m_SaleType As Nullable(Of Byte)
		Property SaleType As Nullable(Of Byte)
			Get
				Return m_SaleType
			End Get
			Set(ByVal value As Nullable(Of Byte))
				m_SaleType = value
			End Set
		End Property

		Private m_SaleDate As Nullable(Of DateTime)
		Property SaleDate As Nullable(Of DateTime)
			Get
				Return m_SaleDate
			End Get
			Set(ByVal value As Nullable(Of DateTime))
				m_SaleDate = value
			End Set
		End Property

		Private m_CusID As Nullable(Of Integer)
		Property CusID As Nullable(Of Integer)
			Get
				Return m_CusID
			End Get
			Set(ByVal value As Nullable(Of Integer))
				m_CusID = value
			End Set
		End Property

		Private m_DocNo As Nullable(Of Integer)
		Property DocNo As Nullable(Of Integer)
			Get
				Return m_DocNo
			End Get
			Set(ByVal value As Nullable(Of Integer))
				m_DocNo = value
			End Set
		End Property

		Private m_SaleEx As String
		Property SaleEx As String
			Get
				Return m_SaleEx
			End Get
			Set(ByVal value As String)
				m_SaleEx = value
			End Set
		End Property

		Private m_Hasm As Nullable(Of Decimal)
		Property Hasm As Nullable(Of Decimal)
			Get
				Return m_Hasm
			End Get
			Set(ByVal value As Nullable(Of Decimal))
				m_Hasm = value
			End Set
		End Property

				Private m_TblSalesBodyIEnumerable As IEnumerable(Of TblSalesBody)
		Public Property TblSalesBodyIEnumerable As IEnumerable(Of TblSalesBody)
			Get
				Return m_TblSalesBodyIEnumerable
			End Get
			Set(ByVal value As IEnumerable(Of TblSalesBody))
				m_TblSalesBodyIEnumerable = value
			End Set
		End Property



	End Class
