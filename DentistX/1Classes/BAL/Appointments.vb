Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections
Imports System.Configuration
Imports Dapper

	Public Class Appointments


		Private m_UniqueID As Integer
		Property UniqueID As Integer
			Get
				Return m_UniqueID
			End Get
			Set(ByVal value As Integer)
				m_UniqueID = value
			End Set
		End Property

		Private m_Type As Nullable(Of Integer)
		Property Type As Nullable(Of Integer)
			Get
				Return m_Type
			End Get
			Set(ByVal value As Nullable(Of Integer))
				m_Type = value
			End Set
		End Property

		Private m_StartDate As Nullable(Of DateTime)
		Property StartDate As Nullable(Of DateTime)
			Get
				Return m_StartDate
			End Get
			Set(ByVal value As Nullable(Of DateTime))
				m_StartDate = value
			End Set
		End Property

		Private m_EndDate As Nullable(Of DateTime)
		Property EndDate As Nullable(Of DateTime)
			Get
				Return m_EndDate
			End Get
			Set(ByVal value As Nullable(Of DateTime))
				m_EndDate = value
			End Set
		End Property

		Private m_QueryStartDate As Nullable(Of DateTime)
		Property QueryStartDate As Nullable(Of DateTime)
			Get
				Return m_QueryStartDate
			End Get
			Set(ByVal value As Nullable(Of DateTime))
				m_QueryStartDate = value
			End Set
		End Property

		Private m_QueryEndDate As Nullable(Of DateTime)
		Property QueryEndDate As Nullable(Of DateTime)
			Get
				Return m_QueryEndDate
			End Get
			Set(ByVal value As Nullable(Of DateTime))
				m_QueryEndDate = value
			End Set
		End Property

		Private m_AllDay As Nullable(Of Boolean)
		Property AllDay As Nullable(Of Boolean)
			Get
				Return m_AllDay
			End Get
			Set(ByVal value As Nullable(Of Boolean))
				m_AllDay = value
			End Set
		End Property

		Private m_Subject As String
		Property Subject As String
			Get
				Return m_Subject
			End Get
			Set(ByVal value As String)
				m_Subject = value
			End Set
		End Property

		Private m_Location As String
		Property Location As String
			Get
				Return m_Location
			End Get
			Set(ByVal value As String)
				m_Location = value
			End Set
		End Property

		Private m_Description As String
		Property Description As String
			Get
				Return m_Description
			End Get
			Set(ByVal value As String)
				m_Description = value
			End Set
		End Property

		Private m_Status As Nullable(Of Integer)
		Property Status As Nullable(Of Integer)
			Get
				Return m_Status
			End Get
			Set(ByVal value As Nullable(Of Integer))
				m_Status = value
			End Set
		End Property

		Private m_Label As Nullable(Of Integer)
		Property Label As Nullable(Of Integer)
			Get
				Return m_Label
			End Get
			Set(ByVal value As Nullable(Of Integer))
				m_Label = value
			End Set
		End Property

		Private m_ResourceID As Nullable(Of Integer)
		Property ResourceID As Nullable(Of Integer)
			Get
				Return m_ResourceID
			End Get
			Set(ByVal value As Nullable(Of Integer))
				m_ResourceID = value
			End Set
		End Property

		Private m_ResourceIDs As String
		Property ResourceIDs As String
			Get
				Return m_ResourceIDs
			End Get
			Set(ByVal value As String)
				m_ResourceIDs = value
			End Set
		End Property

		Private m_ReminderInfo As String
		Property ReminderInfo As String
			Get
				Return m_ReminderInfo
			End Get
			Set(ByVal value As String)
				m_ReminderInfo = value
			End Set
		End Property

		Private m_RecurrenceInfo As String
		Property RecurrenceInfo As String
			Get
				Return m_RecurrenceInfo
			End Get
			Set(ByVal value As String)
				m_RecurrenceInfo = value
			End Set
		End Property

		Private m_TimeZoneId As String
		Property TimeZoneId As String
			Get
				Return m_TimeZoneId
			End Get
			Set(ByVal value As String)
				m_TimeZoneId = value
			End Set
		End Property

		Private m_CustomField1 As String
		Property CustomField1 As String
			Get
				Return m_CustomField1
			End Get
			Set(ByVal value As String)
				m_CustomField1 = value
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

		

	End Class
