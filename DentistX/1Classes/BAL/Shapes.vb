Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections
Imports System.Configuration
Imports Dapper

	Public Class Shapes


		Private m_ShapeID As Integer
		Property ShapeID As Integer
			Get
				Return m_ShapeID
			End Get
			Set(ByVal value As Integer)
				m_ShapeID = value
			End Set
		End Property

		Private m_ShapeName As String
		Property ShapeName As String
			Get
				Return m_ShapeName
			End Get
			Set(ByVal value As String)
				m_ShapeName = value
			End Set
		End Property

		Private m_ShapeDetail As String
		Property ShapeDetail As String
			Get
				Return m_ShapeDetail
			End Get
			Set(ByVal value As String)
				m_ShapeDetail = value
			End Set
		End Property

		Private m_OutID As String
		Property OutID As String
			Get
				Return m_OutID
			End Get
			Set(ByVal value As String)
				m_OutID = value
			End Set
		End Property

		Private m_TopID As String
		Property TopID As String
			Get
				Return m_TopID
			End Get
			Set(ByVal value As String)
				m_TopID = value
			End Set
		End Property

		Private m_INID As String
		Property INID As String
			Get
				Return m_INID
			End Get
			Set(ByVal value As String)
				m_INID = value
			End Set
		End Property

		Private m_ShapeColor As String
		Property ShapeColor As String
			Get
				Return m_ShapeColor
			End Get
			Set(ByVal value As String)
				m_ShapeColor = value
			End Set
		End Property

		

	End Class
