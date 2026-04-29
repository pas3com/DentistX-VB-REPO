Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections
Imports System.Configuration
Imports Dapper

	Public Class Patient_ExternalToothTrt


		Private m_ExternalTrtID As Integer
		Property ExternalTrtID As Integer
			Get
				Return m_ExternalTrtID
			End Get
			Set(ByVal value As Integer)
				m_ExternalTrtID = value
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

		Private m_ToothNum As Byte
		Property ToothNum As Byte
			Get
				Return m_ToothNum
			End Get
			Set(ByVal value As Byte)
				m_ToothNum = value
			End Set
		End Property

		Private m_TreatmentType As String
		Property TreatmentType As String
			Get
				Return m_TreatmentType
			End Get
			Set(ByVal value As String)
				m_TreatmentType = value
			End Set
		End Property

		Private m_ClinicName As String
		Property ClinicName As String
			Get
				Return m_ClinicName
			End Get
			Set(ByVal value As String)
				m_ClinicName = value
			End Set
		End Property

		Private m_TreatmentDate As Nullable(Of DateTime)
		Property TreatmentDate As Nullable(Of DateTime)
			Get
				Return m_TreatmentDate
			End Get
			Set(ByVal value As Nullable(Of DateTime))
				m_TreatmentDate = value
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

		

	End Class
