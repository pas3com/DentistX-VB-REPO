Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections
Imports System.Configuration
Imports Dapper

	Public Class TblTrtClr


		Private m_TrtClrID As Integer
		Property TrtClrID As Integer
			Get
				Return m_TrtClrID
			End Get
			Set(ByVal value As Integer)
				m_TrtClrID = value
			End Set
		End Property

		Private m_TrtID As Integer
		Property TrtID As Integer
			Get
				Return m_TrtID
			End Get
			Set(ByVal value As Integer)
				m_TrtID = value
			End Set
		End Property

		Private m_FillColor As String
		Property FillColor As String
			Get
				Return m_FillColor
			End Get
			Set(ByVal value As String)
				m_FillColor = value
			End Set
		End Property

		Private m_BorderColor As String
		Property BorderColor As String
			Get
				Return m_BorderColor
			End Get
			Set(ByVal value As String)
				m_BorderColor = value
			End Set
		End Property

		Private m_BorderThick As Nullable(Of Byte)
		Property BorderThick As Nullable(Of Byte)
			Get
				Return m_BorderThick
			End Get
			Set(ByVal value As Nullable(Of Byte))
				m_BorderThick = value
			End Set
		End Property

		Private m_FillColorDef As String
		Property FillColorDef As String
			Get
				Return m_FillColorDef
			End Get
			Set(ByVal value As String)
				m_FillColorDef = value
			End Set
		End Property

		

	End Class
