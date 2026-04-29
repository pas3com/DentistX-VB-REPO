Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections
Imports System.Configuration
Imports Dapper

Public Class Lab


    Private m_LabID As Integer
    Property LabID As Integer
        Get
            Return m_LabID
        End Get
        Set(ByVal value As Integer)
            m_LabID = value
        End Set
    End Property

    Private m_LabName As String
    Property LabName As String
        Get
            Return m_LabName
        End Get
        Set(ByVal value As String)
            m_LabName = value
        End Set
    End Property

    Private m_Adres As String
    Property Adres As String
        Get
            Return m_Adres
        End Get
        Set(ByVal value As String)
            m_Adres = value
        End Set
    End Property

    Private m_Phone As String
    Property Phone As String
        Get
            Return m_Phone
        End Get
        Set(ByVal value As String)
            m_Phone = value
        End Set
    End Property

    Private m_Mobile As String
    Property Mobile As String
        Get
            Return m_Mobile
        End Get
        Set(ByVal value As String)
            m_Mobile = value
        End Set
    End Property
    Property WhatsAppPrefix As String
    Property WhatsApp As String

    Private m_LabOrderIEnumerable As IEnumerable(Of LabOrder)
    Public Property LabOrderIEnumerable As IEnumerable(Of LabOrder)
        Get
            Return m_LabOrderIEnumerable
        End Get
        Set(ByVal value As IEnumerable(Of LabOrder))
            m_LabOrderIEnumerable = value
        End Set
    End Property

    Private m_LabPayIEnumerable As IEnumerable(Of LabPay)
    Public Property LabPayIEnumerable As IEnumerable(Of LabPay)
        Get
            Return m_LabPayIEnumerable
        End Get
        Set(ByVal value As IEnumerable(Of LabPay))
            m_LabPayIEnumerable = value
        End Set
    End Property



End Class
