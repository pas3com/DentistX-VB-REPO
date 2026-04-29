Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections
Imports System.Configuration
Imports Dapper

	Public Class VendorPays


		Private m_PayID As Integer
		Property PayID As Integer
			Get
				Return m_PayID
			End Get
			Set(ByVal value As Integer)
				m_PayID = value
			End Set
		End Property

		Private m_SalesID As Integer
		Property SalesID As Integer
			Get
				Return m_SalesID
			End Get
			Set(ByVal value As Integer)
				m_SalesID = value
			End Set
		End Property

		Private m_VendID As Nullable(Of Integer)
		Property VendID As Nullable(Of Integer)
			Get
				Return m_VendID
			End Get
			Set(ByVal value As Nullable(Of Integer))
				m_VendID = value
			End Set
		End Property

		Private m_PayValue As Decimal
		Property PayValue As Decimal
			Get
				Return m_PayValue
			End Get
			Set(ByVal value As Decimal)
				m_PayValue = value
			End Set
		End Property

		Private m_PayDate As DateTime
		Property PayDate As DateTime
			Get
				Return m_PayDate
			End Get
			Set(ByVal value As DateTime)
				m_PayDate = value
			End Set
		End Property

		Private m_Notes As String
		Property Notes As String
			Get
				Return m_Notes
			End Get
			Set(ByVal value As String)
				m_Notes = value
			End Set
		End Property

		Private m_PayType As String
		Property PayType As String
			Get
				Return m_PayType
			End Get
			Set(ByVal value As String)
				m_PayType = value
			End Set
		End Property

		Private m_ChqOwner As String
		Property ChqOwner As String
			Get
				Return m_ChqOwner
			End Get
			Set(ByVal value As String)
				m_ChqOwner = value
			End Set
		End Property

		Private m_AccountNumber As String
		Property AccountNumber As String
			Get
				Return m_AccountNumber
			End Get
			Set(ByVal value As String)
				m_AccountNumber = value
			End Set
		End Property

		Private m_ChqNumber As String
		Property ChqNumber As String
			Get
				Return m_ChqNumber
			End Get
			Set(ByVal value As String)
				m_ChqNumber = value
			End Set
		End Property

		Private m_ChqDueDate As Nullable(Of DateTime)
		Property ChqDueDate As Nullable(Of DateTime)
			Get
				Return m_ChqDueDate
			End Get
			Set(ByVal value As Nullable(Of DateTime))
				m_ChqDueDate = value
			End Set
		End Property

		Private m_ChqBank As String
		Property ChqBank As String
			Get
				Return m_ChqBank
			End Get
			Set(ByVal value As String)
				m_ChqBank = value
			End Set
		End Property

		Private m_IsCashed As Nullable(Of Boolean)
		Property IsCashed As Nullable(Of Boolean)
			Get
				Return m_IsCashed
			End Get
			Set(ByVal value As Nullable(Of Boolean))
				m_IsCashed = value
			End Set
		End Property

    Private m_IsForward As Nullable(Of Boolean)
    Property IsForward As Nullable(Of Boolean)
        Get
            Return m_IsForward
        End Get
        Set(ByVal value As Nullable(Of Boolean))
            m_IsForward = value
        End Set
    End Property

    Private m_ForwardFromTo As String
    Property ForwardFromTo As String
        Get
            Return m_ForwardFromTo
        End Get
        Set(ByVal value As String)
            m_ForwardFromTo = value
        End Set
    End Property

    Public Function Clone() As VendorPays
        Return New VendorPays With {
        .SalesID = Me.SalesID,
        .VendID = Me.VendID,
        .AccountNumber = Me.AccountNumber,
        .ChqBank = Me.ChqBank,
        .ChqDueDate = Me.ChqDueDate,
        .ChqNumber = Me.ChqNumber,
        .ChqOwner = Me.ChqOwner,
        .ForwardFromTo = Me.ForwardFromTo,
        .IsCashed = Me.IsCashed,
        .IsForward = Me.IsForward,
        .Notes = Me.Notes,
        .PayDate = Me.PayDate,
        .PayValue = Me.PayValue,
        .PayID = Me.PayID,
        .PayType = Me.PayType
    }
    End Function

End Class
