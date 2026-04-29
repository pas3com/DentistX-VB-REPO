Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections
Imports System.Configuration
Imports Dapper

	Public Class TblInvoicesHeader


		Private m_InvoiceID As Integer
		Property InvoiceID As Integer
			Get
				Return m_InvoiceID
			End Get
			Set(ByVal value As Integer)
				m_InvoiceID = value
			End Set
		End Property

		Private m_InvoiceType As Nullable(Of Byte)
		Property InvoiceType As Nullable(Of Byte)
			Get
				Return m_InvoiceType
			End Get
			Set(ByVal value As Nullable(Of Byte))
				m_InvoiceType = value
			End Set
		End Property

		Private m_InvoiceDate As Nullable(Of DateTime)
		Property InvoiceDate As Nullable(Of DateTime)
			Get
				Return m_InvoiceDate
			End Get
			Set(ByVal value As Nullable(Of DateTime))
				m_InvoiceDate = value
			End Set
		End Property

		Private m_ResID As Nullable(Of Integer)
		Property ResID As Nullable(Of Integer)
			Get
				Return m_ResID
			End Get
			Set(ByVal value As Nullable(Of Integer))
				m_ResID = value
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

		Private m_InvoiceEx As String
		Property InvoiceEx As String
			Get
				Return m_InvoiceEx
			End Get
			Set(ByVal value As String)
				m_InvoiceEx = value
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

		Private ReadOnly m_InvTotlQuantItms As Nullable(Of Double)
		ReadOnly Property InvTotlQuantItms As Nullable(Of Double)
			Get
				Return m_InvTotlQuantItms
			End Get
		End Property

		Private ReadOnly m_InvTotlPriceItms As Nullable(Of Double)
		ReadOnly Property InvTotlPriceItms As Nullable(Of Double)
			Get
				Return m_InvTotlPriceItms
			End Get
		End Property

		Private ReadOnly m_InvTotlDiscItms As Nullable(Of Decimal)
		ReadOnly Property InvTotlDiscItms As Nullable(Of Decimal)
			Get
				Return m_InvTotlDiscItms
			End Get
		End Property

		Private ReadOnly m_InvTotlDisc As Nullable(Of Decimal)
		ReadOnly Property InvTotlDisc As Nullable(Of Decimal)
			Get
				Return m_InvTotlDisc
			End Get
		End Property

		Private ReadOnly m_InvoiceNet As Nullable(Of Decimal)
		ReadOnly Property InvoiceNet As Nullable(Of Decimal)
			Get
				Return m_InvoiceNet
			End Get
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
