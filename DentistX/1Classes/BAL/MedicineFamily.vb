Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections
Imports System.Configuration
Imports Dapper

	Public Class MedicineFamily


		Private m_SubCatID As Integer
		Property SubCatID As Integer
			Get
				Return m_SubCatID
			End Get
			Set(ByVal value As Integer)
				m_SubCatID = value
			End Set
		End Property

		Private m_MedicineID As Integer
		Property MedicineID As Integer
			Get
				Return m_MedicineID
			End Get
			Set(ByVal value As Integer)
				m_MedicineID = value
			End Set
		End Property

		Private m_MedicineSubCat As String
		Property MedicineSubCat As String
			Get
				Return m_MedicineSubCat
			End Get
			Set(ByVal value As String)
				m_MedicineSubCat = value
			End Set
		End Property

				Private m_MedScienceFamilyIEnumerable As IEnumerable(Of MedScienceFamily)
		Public Property MedScienceFamilyIEnumerable As IEnumerable(Of MedScienceFamily)
			Get
				Return m_MedScienceFamilyIEnumerable
			End Get
			Set(ByVal value As IEnumerable(Of MedScienceFamily))
				m_MedScienceFamilyIEnumerable = value
			End Set
		End Property



	End Class
