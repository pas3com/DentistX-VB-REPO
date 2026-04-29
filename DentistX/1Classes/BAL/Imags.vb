Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections
Imports System.Configuration
Imports Dapper

	Public Class Imags


		Private m_ImageID As Integer
		Property ImageID As Integer
			Get
				Return m_ImageID
			End Get
			Set(ByVal value As Integer)
				m_ImageID = value
			End Set
		End Property

		Private m_IMG As Byte()
		Property IMG As Byte()
			Get
				Return m_IMG
			End Get
			Set(ByVal value As Byte())
				m_IMG = value
			End Set
		End Property

		Private m_Height As Nullable(Of Double)
		Property Height As Nullable(Of Double)
			Get
				Return m_Height
			End Get
			Set(ByVal value As Nullable(Of Double))
				m_Height = value
			End Set
		End Property

		Private m_Width As Nullable(Of Double)
		Property Width As Nullable(Of Double)
			Get
				Return m_Width
			End Get
			Set(ByVal value As Nullable(Of Double))
				m_Width = value
			End Set
		End Property

		Private m_Sze As Nullable(Of Double)
		Property Sze As Nullable(Of Double)
			Get
				Return m_Sze
			End Get
			Set(ByVal value As Nullable(Of Double))
				m_Sze = value
			End Set
		End Property

		Private m_DatePictureTaken As Nullable(Of DateTime)
		Property DatePictureTaken As Nullable(Of DateTime)
			Get
				Return m_DatePictureTaken
			End Get
			Set(ByVal value As Nullable(Of DateTime))
				m_DatePictureTaken = value
			End Set
		End Property

		Private m_EquipmentMaker As String
		Property EquipmentMaker As String
			Get
				Return m_EquipmentMaker
			End Get
			Set(ByVal value As String)
				m_EquipmentMaker = value
			End Set
		End Property

		Private m_EquipmentModel As String
		Property EquipmentModel As String
			Get
				Return m_EquipmentModel
			End Get
			Set(ByVal value As String)
				m_EquipmentModel = value
			End Set
		End Property

		Private m_Thumbnail As Byte()
		Property Thumbnail As Byte()
			Get
				Return m_Thumbnail
			End Get
			Set(ByVal value As Byte())
				m_Thumbnail = value
			End Set
		End Property

		Private m_DateCreated As Nullable(Of DateTime)
		Property DateCreated As Nullable(Of DateTime)
			Get
				Return m_DateCreated
			End Get
			Set(ByVal value As Nullable(Of DateTime))
				m_DateCreated = value
			End Set
		End Property

		Private m_DateModified As Nullable(Of DateTime)
		Property DateModified As Nullable(Of DateTime)
			Get
				Return m_DateModified
			End Get
			Set(ByVal value As Nullable(Of DateTime))
				m_DateModified = value
			End Set
		End Property

		

	End Class
