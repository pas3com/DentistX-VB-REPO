Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections
Imports System.Configuration
Imports Dapper

	Public Class TblCategories


		Private m_CategoryID As Integer
		Property CategoryID As Integer
			Get
				Return m_CategoryID
			End Get
			Set(ByVal value As Integer)
				m_CategoryID = value
			End Set
		End Property

		Private m_CategoryName As String
		Property CategoryName As String
			Get
				Return m_CategoryName
			End Get
			Set(ByVal value As String)
				m_CategoryName = value
			End Set
		End Property

		Private m_ParentCategory As Nullable(Of Integer)
		Property ParentCategory As Nullable(Of Integer)
			Get
				Return m_ParentCategory
			End Get
			Set(ByVal value As Nullable(Of Integer))
				m_ParentCategory = value
			End Set
		End Property

				Private m_TblItemsIEnumerable As IEnumerable(Of TblItems)
		Public Property TblItemsIEnumerable As IEnumerable(Of TblItems)
			Get
				Return m_TblItemsIEnumerable
			End Get
			Set(ByVal value As IEnumerable(Of TblItems))
				m_TblItemsIEnumerable = value
			End Set
		End Property



	End Class
