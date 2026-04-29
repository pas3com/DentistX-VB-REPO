Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections
Imports System.Configuration
Imports Dapper

	Public Class RxBody


		Private m_RxBdyID As Integer
		Property RxBdyID As Integer
			Get
				Return m_RxBdyID
			End Get
			Set(ByVal value As Integer)
				m_RxBdyID = value
			End Set
		End Property

		Private m_ArHdrName As String
		Property ArHdrName As String
			Get
				Return m_ArHdrName
			End Get
			Set(ByVal value As String)
				m_ArHdrName = value
			End Set
		End Property

		Private m_ArHdrAdres As String
		Property ArHdrAdres As String
			Get
				Return m_ArHdrAdres
			End Get
			Set(ByVal value As String)
				m_ArHdrAdres = value
			End Set
		End Property

		Private m_EnHdrName As String
		Property EnHdrName As String
			Get
				Return m_EnHdrName
			End Get
			Set(ByVal value As String)
				m_EnHdrName = value
			End Set
		End Property

		Private m_EnHdrAdres As String
		Property EnHdrAdres As String
			Get
				Return m_EnHdrAdres
			End Get
			Set(ByVal value As String)
				m_EnHdrAdres = value
			End Set
		End Property

		Private m_Logo As Byte()
		Property Logo As Byte()
			Get
				Return m_Logo
			End Get
			Set(ByVal value As Byte())
				m_Logo = value
			End Set
		End Property

		Private m_Detail As String
		Property Detail As String
			Get
				Return m_Detail
			End Get
			Set(ByVal value As String)
				m_Detail = value
			End Set
		End Property

		Private m_ArFtr As String
		Property ArFtr As String
			Get
				Return m_ArFtr
			End Get
			Set(ByVal value As String)
				m_ArFtr = value
			End Set
		End Property

		Private m_EnFtr As String
		Property EnFtr As String
			Get
				Return m_EnFtr
			End Get
			Set(ByVal value As String)
				m_EnFtr = value
			End Set
		End Property

		Private m_WtrImg As Byte()
		Property WtrImg As Byte()
			Get
				Return m_WtrImg
			End Get
			Set(ByVal value As Byte())
				m_WtrImg = value
			End Set
		End Property

		Private m_WtrText As String
		Property WtrText As String
			Get
				Return m_WtrText
			End Get
			Set(ByVal value As String)
				m_WtrText = value
			End Set
		End Property

		Private m_UseWtrImg As Nullable(Of Boolean)
		Property UseWtrImg As Nullable(Of Boolean)
			Get
				Return m_UseWtrImg
			End Get
			Set(ByVal value As Nullable(Of Boolean))
				m_UseWtrImg = value
			End Set
		End Property

		Private m_UseWtrText As Nullable(Of Boolean)
		Property UseWtrText As Nullable(Of Boolean)
			Get
				Return m_UseWtrText
			End Get
			Set(ByVal value As Nullable(Of Boolean))
				m_UseWtrText = value
			End Set
		End Property

		Private m_DrName As String
		Property DrName As String
			Get
				Return m_DrName
			End Get
			Set(ByVal value As String)
				m_DrName = value
			End Set
		End Property

		

	End Class
