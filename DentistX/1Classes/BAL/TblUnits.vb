Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections
Imports System.Configuration
Imports Dapper

	Public Class TblUnits


		Private m_UnitID As Integer
		Property UnitID As Integer
			Get
				Return m_UnitID
			End Get
			Set(ByVal value As Integer)
				m_UnitID = value
			End Set
		End Property

		Private m_UnitName As String
		Property UnitName As String
			Get
				Return m_UnitName
			End Get
			Set(ByVal value As String)
				m_UnitName = value
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
