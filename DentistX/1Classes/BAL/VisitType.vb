Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections
Imports System.Configuration
Imports Dapper

	Public Class VisitType


		Private m_VtID As Integer
		Property VtID As Integer
			Get
				Return m_VtID
			End Get
			Set(ByVal value As Integer)
				m_VtID = value
			End Set
		End Property

		Private m_VisType As String
    Property VisitType As String
        Get
            Return m_VisType
        End Get
        Set(ByVal value As String)
            m_VisType = value
        End Set
    End Property



End Class
