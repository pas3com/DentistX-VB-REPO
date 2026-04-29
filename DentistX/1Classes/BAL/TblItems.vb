Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections
Imports System.Configuration
Imports Dapper

	Public Class TblItems


		Private m_ItemID As Integer
		Property ItemID As Integer
			Get
				Return m_ItemID
			End Get
			Set(ByVal value As Integer)
				m_ItemID = value
			End Set
		End Property

		Private m_ItemName As String
		Property ItemName As String
			Get
				Return m_ItemName
			End Get
			Set(ByVal value As String)
				m_ItemName = value
			End Set
		End Property

		Private m_ItemEx As String
		Property ItemEx As String
			Get
				Return m_ItemEx
			End Get
			Set(ByVal value As String)
				m_ItemEx = value
			End Set
		End Property

		Private m_CatID As Nullable(Of Integer)
		Property CatID As Nullable(Of Integer)
			Get
				Return m_CatID
			End Get
			Set(ByVal value As Nullable(Of Integer))
				m_CatID = value
			End Set
		End Property

		Private m_UnitID As Nullable(Of Integer)
		Property UnitID As Nullable(Of Integer)
			Get
				Return m_UnitID
			End Get
			Set(ByVal value As Nullable(Of Integer))
				m_UnitID = value
			End Set
		End Property

		Private m_LastPrice As Nullable(Of Decimal)
		Property LastPrice As Nullable(Of Decimal)
			Get
				Return m_LastPrice
			End Get
			Set(ByVal value As Nullable(Of Decimal))
				m_LastPrice = value
			End Set
		End Property

		Private m_QuantityNow As Nullable(Of Double)
		Property QuantityNow As Nullable(Of Double)
			Get
				Return m_QuantityNow
			End Get
			Set(ByVal value As Nullable(Of Double))
				m_QuantityNow = value
			End Set
		End Property

				Private m_TblInvoiceBodyIEnumerable As IEnumerable(Of TblInvoiceBody)
		Public Property TblInvoiceBodyIEnumerable As IEnumerable(Of TblInvoiceBody)
			Get
				Return m_TblInvoiceBodyIEnumerable
			End Get
			Set(ByVal value As IEnumerable(Of TblInvoiceBody))
				m_TblInvoiceBodyIEnumerable = value
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
