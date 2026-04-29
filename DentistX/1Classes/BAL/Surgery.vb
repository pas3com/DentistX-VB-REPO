Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections
Imports System.Configuration
Imports Dapper

	Public Class Surgery


		Private m_SurgID As Integer
		Property SurgID As Integer
			Get
				Return m_SurgID
			End Get
			Set(ByVal value As Integer)
				m_SurgID = value
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

		Private m_SurgeryDet As String
		Property SurgeryDet As String
			Get
				Return m_SurgeryDet
			End Get
			Set(ByVal value As String)
				m_SurgeryDet = value
			End Set
		End Property

		Private m_SurDate As Nullable(Of DateTime)
		Property SurDate As Nullable(Of DateTime)
			Get
				Return m_SurDate
			End Get
			Set(ByVal value As Nullable(Of DateTime))
				m_SurDate = value
			End Set
		End Property

		

	End Class
