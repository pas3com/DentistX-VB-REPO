Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections
Imports System.Configuration
Imports Dapper

	Public Class Patient_Imgs


		Private m_PicID As Integer
		Property PicID As Integer
			Get
				Return m_PicID
			End Get
			Set(ByVal value As Integer)
				m_PicID = value
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

		Private m_PicName As String
		Property PicName As String
			Get
				Return m_PicName
			End Get
			Set(ByVal value As String)
				m_PicName = value
			End Set
		End Property

		Private m_PicPath As String
		Property PicPath As String
			Get
				Return m_PicPath
			End Get
			Set(ByVal value As String)
				m_PicPath = value
			End Set
		End Property

		Private m_FullName As String
		Property FullName As String
			Get
				Return m_FullName
			End Get
			Set(ByVal value As String)
				m_FullName = value
			End Set
		End Property

		

	End Class
