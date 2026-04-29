Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections
Imports System.Configuration
Imports Dapper

	Public Class AppointmentC


		Private m_AppointmentID As Integer
		Property AppointmentID As Integer
			Get
				Return m_AppointmentID
			End Get
			Set(ByVal value As Integer)
				m_AppointmentID = value
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

		Private m_DrID As Integer
		Property DrID As Integer
			Get
				Return m_DrID
			End Get
			Set(ByVal value As Integer)
				m_DrID = value
			End Set
		End Property

		Private m_AppDate As DateTime
		Property AppDate As DateTime
			Get
				Return m_AppDate
			End Get
			Set(ByVal value As DateTime)
				m_AppDate = value
			End Set
		End Property

		Private m_StartDateTime As DateTime
		Property StartDateTime As DateTime
			Get
				Return m_StartDateTime
			End Get
			Set(ByVal value As DateTime)
				m_StartDateTime = value
			End Set
		End Property

		Private m_EndDateTime As DateTime
		Property EndDateTime As DateTime
			Get
				Return m_EndDateTime
			End Get
			Set(ByVal value As DateTime)
				m_EndDateTime = value
			End Set
		End Property

		Private m_Reason As String
		Property Reason As String
			Get
				Return m_Reason
			End Get
			Set(ByVal value As String)
				m_Reason = value
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

		Private m_CreatedBy As String
		Property CreatedBy As String
			Get
				Return m_CreatedBy
			End Get
			Set(ByVal value As String)
				m_CreatedBy = value
			End Set
		End Property

		Private m_CreatedAt As DateTime
		Property CreatedAt As DateTime
			Get
				Return m_CreatedAt
			End Get
			Set(ByVal value As DateTime)
				m_CreatedAt = value
			End Set
		End Property

		

	End Class
