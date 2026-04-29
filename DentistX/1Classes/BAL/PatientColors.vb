Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections
Imports System.Configuration
Imports Dapper

	Public Class PatientColors


		Private m_ColorID As Integer
		Property ColorID As Integer
			Get
				Return m_ColorID
			End Get
			Set(ByVal value As Integer)
				m_ColorID = value
			End Set
		End Property

		Private m_Color1 As String
		Property Color1 As String
			Get
				Return m_Color1
			End Get
			Set(ByVal value As String)
				m_Color1 = value
			End Set
		End Property

		Private m_Color2 As String
		Property Color2 As String
			Get
				Return m_Color2
			End Get
			Set(ByVal value As String)
				m_Color2 = value
			End Set
		End Property

		Private m_GradientIndex As Nullable(Of Byte)
		Property GradientIndex As Nullable(Of Byte)
			Get
				Return m_GradientIndex
			End Get
			Set(ByVal value As Nullable(Of Byte))
				m_GradientIndex = value
			End Set
		End Property

		Private m_AlphaValue As Nullable(Of Integer)
		Property AlphaValue As Nullable(Of Integer)
			Get
				Return m_AlphaValue
			End Get
			Set(ByVal value As Nullable(Of Integer))
				m_AlphaValue = value
			End Set
		End Property

		Private m_PatientID As Nullable(Of Integer)
		Property PatientID As Nullable(Of Integer)
			Get
				Return m_PatientID
			End Get
			Set(ByVal value As Nullable(Of Integer))
				m_PatientID = value
			End Set
		End Property

		

	End Class
