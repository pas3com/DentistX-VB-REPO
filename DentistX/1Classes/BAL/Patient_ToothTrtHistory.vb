Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections
Imports System.Configuration
Imports Dapper

	Public Class Patient_ToothTrtHistory


Public Function Clone() As Patient_ToothTrtHistory
Return DirectCast(Me.MemberwiseClone(), Patient_ToothTrtHistory)
End Function

		Private m_TrtHistID As Integer
		Property TrtHistID As Integer
			Get
				Return m_TrtHistID
			End Get
			Set(ByVal value As Integer)
				m_TrtHistID = value
			End Set
		End Property

		Private m_TrtID As Integer
		Property TrtID As Integer
			Get
				Return m_TrtID
			End Get
			Set(ByVal value As Integer)
				m_TrtID = value
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

		Private m_PropertyName As String
		Property PropertyName As String
			Get
				Return m_PropertyName
			End Get
			Set(ByVal value As String)
				m_PropertyName = value
			End Set
		End Property

		Private m_FillColor As String
		Property FillColor As String
			Get
				Return m_FillColor
			End Get
			Set(ByVal value As String)
				m_FillColor = value
			End Set
		End Property

		Private m_BorderThickness As Nullable(Of Byte)
		Property BorderThickness As Nullable(Of Byte)
			Get
				Return m_BorderThickness
			End Get
			Set(ByVal value As Nullable(Of Byte))
				m_BorderThickness = value
			End Set
		End Property

		Private m_BorderColor As String
		Property BorderColor As String
			Get
				Return m_BorderColor
			End Get
			Set(ByVal value As String)
				m_BorderColor = value
			End Set
		End Property

		Private m_Treatment As String
		Property Treatment As String
			Get
				Return m_Treatment
			End Get
			Set(ByVal value As String)
				m_Treatment = value
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

		Private m_TreatDetails As String
		Property TreatDetails As String
			Get
				Return m_TreatDetails
			End Get
			Set(ByVal value As String)
				m_TreatDetails = value
			End Set
		End Property

		Private m_TreatStatus As Nullable(Of Byte)
		Property TreatStatus As Nullable(Of Byte)
			Get
				Return m_TreatStatus
			End Get
			Set(ByVal value As Nullable(Of Byte))
				m_TreatStatus = value
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

		Private m_Notes As String
		Property Notes As String
			Get
				Return m_Notes
			End Get
			Set(ByVal value As String)
				m_Notes = value
			End Set
		End Property

		Private m_Finished As Nullable(Of Byte)
		Property Finished As Nullable(Of Byte)
			Get
				Return m_Finished
			End Get
			Set(ByVal value As Nullable(Of Byte))
				m_Finished = value
			End Set
		End Property

		

	End Class
