Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections
Imports System.Configuration
Imports Dapper

	Public Class MedScienceFamily


		Private m_ScincID As Integer
		Property ScincID As Integer
			Get
				Return m_ScincID
			End Get
			Set(ByVal value As Integer)
				m_ScincID = value
			End Set
		End Property

		Private m_SubCatID As Integer
		Property SubCatID As Integer
			Get
				Return m_SubCatID
			End Get
			Set(ByVal value As Integer)
				m_SubCatID = value
			End Set
		End Property

		Private m_ScienceName As String
		Property ScienceName As String
			Get
				Return m_ScienceName
			End Get
			Set(ByVal value As String)
				m_ScienceName = value
			End Set
		End Property

				Private m_MedicineItemsIEnumerable As IEnumerable(Of MedicineItems)
		Public Property MedicineItemsIEnumerable As IEnumerable(Of MedicineItems)
			Get
				Return m_MedicineItemsIEnumerable
			End Get
			Set(ByVal value As IEnumerable(Of MedicineItems))
				m_MedicineItemsIEnumerable = value
			End Set
		End Property



	End Class
