Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections
Imports System.Configuration
Imports Dapper

	Public Class Patient_DiagInfo


		Private m_TrtInfoID As Integer
		Property TrtInfoID As Integer
			Get
				Return m_TrtInfoID
			End Get
			Set(ByVal value As Integer)
				m_TrtInfoID = value
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

		Private m_ParentDiagID As Nullable(Of Integer)
		Property ParentDiagID As Nullable(Of Integer)
			Get
				Return m_ParentDiagID
			End Get
			Set(ByVal value As Nullable(Of Integer))
				m_ParentDiagID = value
			End Set
		End Property

		Private m_TrtGroupID As Nullable(Of Guid)
		Property TrtGroupID As Nullable(Of Guid)
			Get
				Return m_TrtGroupID
			End Get
			Set(ByVal value As Nullable(Of Guid))
				m_TrtGroupID = value
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

		Private m_TreatDate As Nullable(Of DateTime)
		Property TreatDate As Nullable(Of DateTime)
			Get
				Return m_TreatDate
			End Get
			Set(ByVal value As Nullable(Of DateTime))
				m_TreatDate = value
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

		Private m_TreatNotes As String
		Property TreatNotes As String
			Get
				Return m_TreatNotes
			End Get
			Set(ByVal value As String)
				m_TreatNotes = value
			End Set
		End Property

		Private m_IsExternal As Nullable(Of Boolean)
		Property IsExternal As Nullable(Of Boolean)
			Get
				Return m_IsExternal
			End Get
			Set(ByVal value As Nullable(Of Boolean))
				m_IsExternal = value
			End Set
		End Property

		Private m_ExternalClinicName As String
		Property ExternalClinicName As String
			Get
				Return m_ExternalClinicName
			End Get
			Set(ByVal value As String)
				m_ExternalClinicName = value
			End Set
		End Property

		Private m_ExternalTreatmentDate As Nullable(Of DateTime)
		Property ExternalTreatmentDate As Nullable(Of DateTime)
			Get
				Return m_ExternalTreatmentDate
			End Get
			Set(ByVal value As Nullable(Of DateTime))
				m_ExternalTreatmentDate = value
			End Set
		End Property

		

	End Class
