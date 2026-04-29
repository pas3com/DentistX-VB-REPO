Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections
Imports System.Configuration
Imports Dapper

	Public Class TblSalesBody


		Private m_SaleID As Integer
		Property SaleID As Integer
			Get
				Return m_SaleID
			End Get
			Set(ByVal value As Integer)
				m_SaleID = value
			End Set
		End Property

		Private m_ItemID As Integer
		Property ItemID As Integer
			Get
				Return m_ItemID
			End Get
			Set(ByVal value As Integer)
				m_ItemID = value
			End Set
		End Property

		Private m_Quantity As Nullable(Of Double)
		Property Quantity As Nullable(Of Double)
			Get
				Return m_Quantity
			End Get
			Set(ByVal value As Nullable(Of Double))
				m_Quantity = value
			End Set
		End Property

		Private m_Price As Nullable(Of Decimal)
		Property Price As Nullable(Of Decimal)
			Get
				Return m_Price
			End Get
			Set(ByVal value As Nullable(Of Decimal))
				m_Price = value
			End Set
		End Property

		Private m_ItemHasm As Nullable(Of Decimal)
		Property ItemHasm As Nullable(Of Decimal)
			Get
				Return m_ItemHasm
			End Get
			Set(ByVal value As Nullable(Of Decimal))
				m_ItemHasm = value
			End Set
		End Property

		Private m_Note As String
		Property Note As String
			Get
				Return m_Note
			End Get
			Set(ByVal value As String)
				m_Note = value
			End Set
		End Property

		

	End Class
