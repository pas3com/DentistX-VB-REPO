Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections
Imports System.Configuration
Imports Dapper

	Public Class TblBnodMsareef


		Private m_BandID As Integer
		Property BandID As Integer
			Get
				Return m_BandID
			End Get
			Set(ByVal value As Integer)
				m_BandID = value
			End Set
		End Property

		Private m_BandName As String
		Property BandName As String
			Get
				Return m_BandName
			End Get
			Set(ByVal value As String)
				m_BandName = value
			End Set
		End Property

				Private m_TblMasareefIEnumerable As IEnumerable(Of TblMasareef)
		Public Property TblMasareefIEnumerable As IEnumerable(Of TblMasareef)
			Get
				Return m_TblMasareefIEnumerable
			End Get
			Set(ByVal value As IEnumerable(Of TblMasareef))
				m_TblMasareefIEnumerable = value
			End Set
		End Property



	End Class
