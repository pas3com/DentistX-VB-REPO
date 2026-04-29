Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections
Imports System.Configuration
Imports Dapper

	Public Class Patient_OtherTRT
    Public Function Clone() As Patient_OtherTRT
		Return New Patient_OtherTRT With {
		.OtherTrtID = Me.OtherTrtID,
		.PatientID = Me.PatientID,
		.Trt = Me.Trt,
		.TreatDate = Me.TreatDate,
		.TrtDetails = Me.TrtDetails,
		.IsPaid = Me.IsPaid
	}
	End Function



    Private m_OtherTrtID As Integer
		Property OtherTrtID As Integer
			Get
				Return m_OtherTrtID
			End Get
			Set(ByVal value As Integer)
				m_OtherTrtID = value
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

		Private m_Trt As String
		Property Trt As String
			Get
				Return m_Trt
			End Get
			Set(ByVal value As String)
				m_Trt = value
			End Set
		End Property

		Private m_TrtDate As DateTime
	Property TreatDate As DateTime
		Get
			Return m_TrtDate
		End Get
		Set(ByVal value As DateTime)
			m_TrtDate = value
		End Set
	End Property

	Private m_TrtDetails As String
		Property TrtDetails As String
			Get
				Return m_TrtDetails
			End Get
			Set(ByVal value As String)
				m_TrtDetails = value
			End Set
		End Property

		Private m_IsPaid As Boolean
		Property IsPaid As Boolean
			Get
				Return m_IsPaid
			End Get
			Set(ByVal value As Boolean)
				m_IsPaid = value
			End Set
		End Property

		

	End Class
