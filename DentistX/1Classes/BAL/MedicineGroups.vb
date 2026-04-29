Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections
Imports System.Configuration
Imports Dapper

	Public Class MedicineGroups


		Private m_MedicineID As Integer
		Property MedicineID As Integer
			Get
				Return m_MedicineID
			End Get
			Set(ByVal value As Integer)
				m_MedicineID = value
			End Set
		End Property

		Private m_MedicineFamily As String
		Property MedicineFamily As String
			Get
				Return m_MedicineFamily
			End Get
			Set(ByVal value As String)
				m_MedicineFamily = value
			End Set
		End Property

				Private m_MedicineFamilyIEnumerable As IEnumerable(Of MedicineFamily)
		Public Property MedicineFamilyIEnumerable As IEnumerable(Of MedicineFamily)
			Get
				Return m_MedicineFamilyIEnumerable
			End Get
			Set(ByVal value As IEnumerable(Of MedicineFamily))
				m_MedicineFamilyIEnumerable = value
			End Set
		End Property



	End Class
