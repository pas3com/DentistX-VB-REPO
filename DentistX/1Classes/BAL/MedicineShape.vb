Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections
Imports System.Configuration
Imports Dapper

	Public Class MedicineShape


		Private m_ShapeID As Integer
		Property ShapeID As Integer
			Get
				Return m_ShapeID
			End Get
			Set(ByVal value As Integer)
				m_ShapeID = value
			End Set
		End Property

		Private m_MedicineShape As String
		Property MedicineShape As String
			Get
				Return m_MedicineShape
			End Get
			Set(ByVal value As String)
				m_MedicineShape = value
			End Set
		End Property

		Private m_MedicineItemID As Integer
		Property MedicineItemID As Integer
			Get
				Return m_MedicineItemID
			End Get
			Set(ByVal value As Integer)
				m_MedicineItemID = value
			End Set
		End Property

		Private m_ShapeInfo As String
		Property ShapeInfo As String
			Get
				Return m_ShapeInfo
			End Get
			Set(ByVal value As String)
				m_ShapeInfo = value
			End Set
		End Property

				Private m_MedicineDozeIEnumerable As IEnumerable(Of MedicineDoze)
		Public Property MedicineDozeIEnumerable As IEnumerable(Of MedicineDoze)
			Get
				Return m_MedicineDozeIEnumerable
			End Get
			Set(ByVal value As IEnumerable(Of MedicineDoze))
				m_MedicineDozeIEnumerable = value
			End Set
		End Property



	End Class
