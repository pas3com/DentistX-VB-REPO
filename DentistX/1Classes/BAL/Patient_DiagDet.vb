Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections
Imports System.Configuration
Imports Dapper

	Public Class Patient_DiagDet


		Private m_DiagDetID As Integer
		Property DiagDetID As Integer
			Get
				Return m_DiagDetID
			End Get
			Set(ByVal value As Integer)
				m_DiagDetID = value
			End Set
		End Property

		Private m_DiagIDs As String
		Property DiagIDs As String
			Get
				Return m_DiagIDs
			End Get
			Set(ByVal value As String)
				m_DiagIDs = value
			End Set
		End Property

		Private m_PatientID As Nullable(Of Integer)
		Property PatientID As Nullable(Of Integer)
			Get
				Return m_PatientID
			End Get
			Set(ByVal value As Nullable(Of Integer))
				m_PatientID = value
			End Set
		End Property

		Private m_DiagDate As Nullable(Of DateTime)
		Property DiagDate As Nullable(Of DateTime)
			Get
				Return m_DiagDate
			End Get
			Set(ByVal value As Nullable(Of DateTime))
				m_DiagDate = value
			End Set
		End Property

		Private m_TreatPlan As String
		Property TreatPlan As String
			Get
				Return m_TreatPlan
			End Get
			Set(ByVal value As String)
				m_TreatPlan = value
			End Set
		End Property

		Private m_DiagAgreament As String
		Property DiagAgreament As String
			Get
				Return m_DiagAgreament
			End Get
			Set(ByVal value As String)
				m_DiagAgreament = value
			End Set
		End Property

		Private m_DiagDetails As String
		Property DiagDetails As String
			Get
				Return m_DiagDetails
			End Get
			Set(ByVal value As String)
				m_DiagDetails = value
			End Set
		End Property

		Private m_DiagNotes As String
		Property DiagNotes As String
			Get
				Return m_DiagNotes
			End Get
			Set(ByVal value As String)
				m_DiagNotes = value
			End Set
		End Property

		Private m_DateToStart As Nullable(Of DateTime)
		Property DateToStart As Nullable(Of DateTime)
			Get
				Return m_DateToStart
			End Get
			Set(ByVal value As Nullable(Of DateTime))
				m_DateToStart = value
			End Set
		End Property

		Private m_TotalValue As Nullable(Of Decimal)
		Property TotalValue As Nullable(Of Decimal)
			Get
				Return m_TotalValue
			End Get
			Set(ByVal value As Nullable(Of Decimal))
				m_TotalValue = value
			End Set
		End Property

		Private m_AdvancePay As Nullable(Of Decimal)
		Property AdvancePay As Nullable(Of Decimal)
			Get
				Return m_AdvancePay
			End Get
			Set(ByVal value As Nullable(Of Decimal))
				m_AdvancePay = value
			End Set
		End Property

		Private m_UserID As Nullable(Of Integer)
		Property UserID As Nullable(Of Integer)
			Get
				Return m_UserID
			End Get
			Set(ByVal value As Nullable(Of Integer))
				m_UserID = value
			End Set
		End Property

		

	End Class
