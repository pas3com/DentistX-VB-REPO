Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections
Imports System.Configuration
Imports Dapper

	Public Class OtherTrts


		Private m_OTrtID As Integer
		Property OTrtID As Integer
			Get
				Return m_OTrtID
			End Get
			Set(ByVal value As Integer)
				m_OTrtID = value
			End Set
		End Property

		Private m_PatientID As Integer
		Property PatientID As Integer
			Get
				Return m_PatientID
			End Get
			Set(ByVal value As Integer)
				m_PatientID = value
			End Set
		End Property

		Private m_TrtItemID As Nullable(Of Integer)
		Property TrtItemID As Nullable(Of Integer)
			Get
				Return m_TrtItemID
			End Get
			Set(ByVal value As Nullable(Of Integer))
				m_TrtItemID = value
			End Set
		End Property

		Private m_OtherTrtsDet As String
		Property OtherTrtsDet As String
			Get
				Return m_OtherTrtsDet
			End Get
			Set(ByVal value As String)
				m_OtherTrtsDet = value
			End Set
		End Property

		Private m_TrtDate As Nullable(Of DateTime)
		Property TrtDate As Nullable(Of DateTime)
			Get
				Return m_TrtDate
			End Get
			Set(ByVal value As Nullable(Of DateTime))
				m_TrtDate = value
			End Set
		End Property

		

	End Class
