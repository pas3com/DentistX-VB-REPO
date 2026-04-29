Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections
Imports System.Configuration
Imports Dapper

Public Class LabOrder



    Property LabOrderID As Integer

    Property LabID As Integer

    Property LabName As String
    Property PatientName As String
    Private m_PatientID As Integer
    Property PatientID As Integer

    Property ImprType As String

    Property ImprDet As String

    Property ImprClr As String

    Property ImprCount As Integer

    Property DeliveryDate As Nullable(Of DateTime)

    Property Price As Integer
    Property OrderDetails As String
    Property RecieveDate As Nullable(Of DateTime)

    Property Notes As String



End Class
