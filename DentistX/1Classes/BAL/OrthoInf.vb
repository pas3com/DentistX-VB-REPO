Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections
Imports System.Configuration
Imports Dapper

	Public Class OrthoInf


		Private m_OrthoID As Integer
		Property OrthoID As Integer
			Get
				Return m_OrthoID
			End Get
			Set(ByVal value As Integer)
				m_OrthoID = value
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

		Private m_Compliants As String
		Property Compliants As String
			Get
				Return m_Compliants
			End Get
			Set(ByVal value As String)
				m_Compliants = value
			End Set
		End Property

		Private m_Birth As String
		Property Birth As String
			Get
				Return m_Birth
			End Get
			Set(ByVal value As String)
				m_Birth = value
			End Set
		End Property

		Private m_Feed As String
		Property Feed As String
			Get
				Return m_Feed
			End Get
			Set(ByVal value As String)
				m_Feed = value
			End Set
		End Property

		Private m_MilkTeethChng As String
		Property MilkTeethChng As String
			Get
				Return m_MilkTeethChng
			End Get
			Set(ByVal value As String)
				m_MilkTeethChng = value
			End Set
		End Property

		Private m_MilkTeethAppear As String
		Property MilkTeethAppear As String
			Get
				Return m_MilkTeethAppear
			End Get
			Set(ByVal value As String)
				m_MilkTeethAppear = value
			End Set
		End Property

		Private m_TeethLoss As String
		Property TeethLoss As String
			Get
				Return m_TeethLoss
			End Get
			Set(ByVal value As String)
				m_TeethLoss = value
			End Set
		End Property

		Private m_BurriedTeeth As String
		Property BurriedTeeth As String
			Get
				Return m_BurriedTeeth
			End Get
			Set(ByVal value As String)
				m_BurriedTeeth = value
			End Set
		End Property

		Private m_OverLoadTeeth As String
		Property OverLoadTeeth As String
			Get
				Return m_OverLoadTeeth
			End Get
			Set(ByVal value As String)
				m_OverLoadTeeth = value
			End Set
		End Property

		Private m_LipsCut As String
		Property LipsCut As String
			Get
				Return m_LipsCut
			End Get
			Set(ByVal value As String)
				m_LipsCut = value
			End Set
		End Property

		Private m_ThroatCut As String
		Property ThroatCut As String
			Get
				Return m_ThroatCut
			End Get
			Set(ByVal value As String)
				m_ThroatCut = value
			End Set
		End Property

		Private m_IllnesPeriod As String
		Property IllnesPeriod As String
			Get
				Return m_IllnesPeriod
			End Get
			Set(ByVal value As String)
				m_IllnesPeriod = value
			End Set
		End Property

		Private m_CousinsHFactor As String
		Property CousinsHFactor As String
			Get
				Return m_CousinsHFactor
			End Get
			Set(ByVal value As String)
				m_CousinsHFactor = value
			End Set
		End Property

		Private m_BadHabits As String
		Property BadHabits As String
			Get
				Return m_BadHabits
			End Get
			Set(ByVal value As String)
				m_BadHabits = value
			End Set
		End Property

		Private m_Malfunction As String
		Property Malfunction As String
			Get
				Return m_Malfunction
			End Get
			Set(ByVal value As String)
				m_Malfunction = value
			End Set
		End Property

		Private m_Khota As String
		Property Khota As String
			Get
				Return m_Khota
			End Get
			Set(ByVal value As String)
				m_Khota = value
			End Set
		End Property

		Private m_PrevOrth As String
		Property PrevOrth As String
			Get
				Return m_PrevOrth
			End Get
			Set(ByVal value As String)
				m_PrevOrth = value
			End Set
		End Property

		Private m_PrevIll As String
    Property PrevIll As String
        Get
            Return m_PrevIll
        End Get
        Set(ByVal value As String)
            m_PrevIll = value
        End Set
    End Property

    Property TreatDate As Nullable(Of DateTime)

End Class
