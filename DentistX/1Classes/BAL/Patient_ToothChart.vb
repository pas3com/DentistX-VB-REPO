Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections
Imports System.Configuration
Imports Dapper

	Public Class Patient_ToothChart


		Private m_ChartID As Integer
		Property ChartID As Integer
			Get
				Return m_ChartID
			End Get
			Set(ByVal value As Integer)
				m_ChartID = value
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

		Private m_ToothNum As Nullable(Of Byte)
		Property ToothNum As Nullable(Of Byte)
			Get
				Return m_ToothNum
			End Get
			Set(ByVal value As Nullable(Of Byte))
				m_ToothNum = value
			End Set
		End Property

		Private m_SVG As String
		Property SVG As String
			Get
				Return m_SVG
			End Get
			Set(ByVal value As String)
				m_SVG = value
			End Set
		End Property

		Private m_Treat As String
		Property Treat As String
			Get
				Return m_Treat
			End Get
			Set(ByVal value As String)
				m_Treat = value
			End Set
		End Property

		Private m_StageColor As String
		Property StageColor As String
			Get
				Return m_StageColor
			End Get
			Set(ByVal value As String)
				m_StageColor = value
			End Set
		End Property

		

	End Class
