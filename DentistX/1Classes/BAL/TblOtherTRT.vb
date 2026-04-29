Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections
Imports System.Configuration
Imports Dapper

	Public Class TblOtherTRT


		Private m_TblOtherTrtID As Integer
		Property TblOtherTrtID As Integer
			Get
				Return m_TblOtherTrtID
			End Get
			Set(ByVal value As Integer)
				m_TblOtherTrtID = value
			End Set
		End Property

		Private m_Trt As String
		Property Trt As String
			Get
				Return m_Trt
			End Get
			Set(ByVal value As String)
				m_Trt = value
			End Set
		End Property

		

	End Class
