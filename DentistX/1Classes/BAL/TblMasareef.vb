Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections
Imports System.Configuration
Imports Dapper

	Public Class TblMasareef


		Private m_MasrofID As Integer
		Property MasrofID As Integer
			Get
				Return m_MasrofID
			End Get
			Set(ByVal value As Integer)
				m_MasrofID = value
			End Set
		End Property

		Private m_MasrofDate As Nullable(Of DateTime)
		Property MasrofDate As Nullable(Of DateTime)
			Get
				Return m_MasrofDate
			End Get
			Set(ByVal value As Nullable(Of DateTime))
				m_MasrofDate = value
			End Set
		End Property

		Private m_BandID As Nullable(Of Integer)
		Property BandID As Nullable(Of Integer)
			Get
				Return m_BandID
			End Get
			Set(ByVal value As Nullable(Of Integer))
				m_BandID = value
			End Set
		End Property

		Private m_MasrofAmount As Nullable(Of Decimal)
		Property MasrofAmount As Nullable(Of Decimal)
			Get
				Return m_MasrofAmount
			End Get
			Set(ByVal value As Nullable(Of Decimal))
				m_MasrofAmount = value
			End Set
		End Property

		Private m_MasrofEx As String
		Property MasrofEx As String
			Get
				Return m_MasrofEx
			End Get
			Set(ByVal value As String)
				m_MasrofEx = value
			End Set
		End Property

		

	End Class
