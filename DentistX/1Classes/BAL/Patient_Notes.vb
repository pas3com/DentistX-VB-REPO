Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections
Imports System.Configuration
Imports Dapper

	Public Class Patient_Notes


		Private m_NoteID As Integer
		Property NoteID As Integer
			Get
				Return m_NoteID
			End Get
			Set(ByVal value As Integer)
				m_NoteID = value
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

		Private m_NoteDate As Nullable(Of DateTime)
		Property NoteDate As Nullable(Of DateTime)
			Get
				Return m_NoteDate
			End Get
			Set(ByVal value As Nullable(Of DateTime))
				m_NoteDate = value
			End Set
		End Property

		Private m_Note As String
		Property Note As String
			Get
				Return m_Note
			End Get
			Set(ByVal value As String)
				m_Note = value
			End Set
		End Property

		

	End Class
