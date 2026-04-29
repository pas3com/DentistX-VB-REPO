Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections
Imports System.Configuration
Imports Dapper

	Public Class MedicineItems


		Private m_MedicineItemID As Integer
		Property MedicineItemID As Integer
			Get
				Return m_MedicineItemID
			End Get
			Set(ByVal value As Integer)
				m_MedicineItemID = value
			End Set
		End Property

		Private m_ScincID As Integer
		Property ScincID As Integer
			Get
				Return m_ScincID
			End Get
			Set(ByVal value As Integer)
				m_ScincID = value
			End Set
		End Property

		Private m_CommName As String
		Property CommName As String
			Get
				Return m_CommName
			End Get
			Set(ByVal value As String)
				m_CommName = value
			End Set
		End Property

		Private m_Company As String
		Property Company As String
			Get
				Return m_Company
			End Get
			Set(ByVal value As String)
				m_Company = value
			End Set
		End Property

		Private m_Notes As String
		Property Notes As String
			Get
				Return m_Notes
			End Get
			Set(ByVal value As String)
				m_Notes = value
			End Set
		End Property

				Private m_MedicineShapeIEnumerable As IEnumerable(Of MedicineShape)
		Public Property MedicineShapeIEnumerable As IEnumerable(Of MedicineShape)
			Get
				Return m_MedicineShapeIEnumerable
			End Get
			Set(ByVal value As IEnumerable(Of MedicineShape))
				m_MedicineShapeIEnumerable = value
			End Set
		End Property



	End Class
