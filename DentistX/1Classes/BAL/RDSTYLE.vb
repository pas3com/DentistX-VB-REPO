Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections
Imports System.Configuration
Imports Dapper

	Public Class RDSTYLE


		Private m_RDcellID As Integer
		Property RDcellID As Integer
			Get
				Return m_RDcellID
			End Get
			Set(ByVal value As Integer)
				m_RDcellID = value
			End Set
		End Property

		Private m_PatientID As Integer
		Property PatientID As Integer
			Get
				Return m_PatientID
			End Get
			Set(ByVal value As Integer)
				m_PatientID = value
			End Set
		End Property

		Private m_CellAddres As Integer
		Property CellAddres As Integer
			Get
				Return m_CellAddres
			End Get
			Set(ByVal value As Integer)
				m_CellAddres = value
			End Set
		End Property

		Private m_BakImg As Byte()
		Property BakImg As Byte()
			Get
				Return m_BakImg
			End Get
			Set(ByVal value As Byte())
				m_BakImg = value
			End Set
		End Property


    Public Property ImgName() As String
    Property ColName As String
    Property RowIndex As Integer


End Class
