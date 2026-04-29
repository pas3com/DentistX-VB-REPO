Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections
Imports System.Configuration
Imports Dapper

	Public Class Patient_MobStruc


		Private m_StrucID As Integer
		Property StrucID As Integer
			Get
				Return m_StrucID
			End Get
			Set(ByVal value As Integer)
				m_StrucID = value
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

		Private m_StrucName As String
		Property StrucName As String
			Get
				Return m_StrucName
			End Get
			Set(ByVal value As String)
				m_StrucName = value
			End Set
		End Property

		Private m_StrucType As String
		Property StrucType As String
			Get
				Return m_StrucType
			End Get
			Set(ByVal value As String)
				m_StrucType = value
			End Set
		End Property

		Private m_TeethType As String
		Property TeethType As String
			Get
				Return m_TeethType
			End Get
			Set(ByVal value As String)
				m_TeethType = value
			End Set
		End Property

		Private m_StrucDate As DateTime
		Property StrucDate As DateTime
			Get
				Return m_StrucDate
			End Get
			Set(ByVal value As DateTime)
				m_StrucDate = value
			End Set
		End Property

				Private m_Patient_MobStrucAddIEnumerable As IEnumerable(Of Patient_MobStrucAdd)
		Public Property Patient_MobStrucAddIEnumerable As IEnumerable(Of Patient_MobStrucAdd)
			Get
				Return m_Patient_MobStrucAddIEnumerable
			End Get
			Set(ByVal value As IEnumerable(Of Patient_MobStrucAdd))
				m_Patient_MobStrucAddIEnumerable = value
			End Set
		End Property



	End Class
