Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections
Imports System.Configuration
Imports Dapper

	Public Class VendorSales


		Private m_SalesID As Integer
		Property SalesID As Integer
			Get
				Return m_SalesID
			End Get
			Set(ByVal value As Integer)
				m_SalesID = value
			End Set
		End Property

		Private m_VendID As Integer
		Property VendID As Integer
			Get
				Return m_VendID
			End Get
			Set(ByVal value As Integer)
				m_VendID = value
			End Set
		End Property

		Private m_Detail As String
		Property Detail As String
			Get
				Return m_Detail
			End Get
			Set(ByVal value As String)
				m_Detail = value
			End Set
		End Property

		Private m_SalesDate As DateTime
		Property SalesDate As DateTime
			Get
				Return m_SalesDate
			End Get
			Set(ByVal value As DateTime)
				m_SalesDate = value
			End Set
		End Property

		Private m_SalesValue As Decimal
		Property SalesValue As Decimal
			Get
				Return m_SalesValue
			End Get
			Set(ByVal value As Decimal)
				m_SalesValue = value
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

    Public Function Clone() As VendorSales
        Return New VendorSales With {
        .SalesID = Me.SalesID,
        .VendID = Me.VendID,
        .Detail = Me.Detail,
        .SalesDate = Me.SalesDate,
        .SalesValue = Me.SalesValue
    }
    End Function


End Class
