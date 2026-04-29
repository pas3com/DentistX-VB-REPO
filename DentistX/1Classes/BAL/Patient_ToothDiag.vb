Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections
Imports System.Configuration
Imports Dapper

	Public Class Patient_ToothDiag


		Private m_DiagID As Integer
		Property DiagID As Integer
			Get
				Return m_DiagID
			End Get
			Set(ByVal value As Integer)
				m_DiagID = value
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

		Private m_ToothName As String
		Property ToothName As String
			Get
				Return m_ToothName
			End Get
			Set(ByVal value As String)
				m_ToothName = value
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

		Private m_Diagnose As String
		Property Diagnose As String
			Get
				Return m_Diagnose
			End Get
			Set(ByVal value As String)
				m_Diagnose = value
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

		

	End Class
