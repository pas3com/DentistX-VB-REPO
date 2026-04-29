Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections
Imports System.Configuration
Imports Dapper

	Public Class OthrTrtItems

    Private m_OTrtID As Integer
    Property OTrtID As Integer
        Get
            Return m_OTrtID
        End Get
        Set(ByVal value As Integer)
            m_OTrtID = value
        End Set
    End Property

    Private m_TrtItemID As Integer
		Property TrtItemID As Integer
			Get
				Return m_TrtItemID
			End Get
			Set(ByVal value As Integer)
				m_TrtItemID = value
			End Set
		End Property

		Private m_TrtEng As String
		Property TrtEng As String
			Get
				Return m_TrtEng
			End Get
			Set(ByVal value As String)
				m_TrtEng = value
			End Set
		End Property

		Private m_TrtEngDetails As String
		Property TrtEngDetails As String
			Get
				Return m_TrtEngDetails
			End Get
			Set(ByVal value As String)
				m_TrtEngDetails = value
			End Set
		End Property

		Private m_TrtAr As String
		Property TrtAr As String
			Get
				Return m_TrtAr
			End Get
			Set(ByVal value As String)
				m_TrtAr = value
			End Set
		End Property

		Private m_TrtArDetails As String
		Property TrtArDetails As String
			Get
				Return m_TrtArDetails
			End Get
			Set(ByVal value As String)
				m_TrtArDetails = value
			End Set
		End Property

		

	End Class
