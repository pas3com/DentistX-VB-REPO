Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections
Imports System.Configuration
Imports Dapper

Public Class LabMsgs

    Public Property LabMsgID As Integer
    Public Property ClinicID As Nullable(Of Guid)
    Public Property ClinicName As String
    Public Property LabID As Nullable(Of Integer)
    Public Property LabName As String
    Public Property PatientID As Nullable(Of Integer)
    Public Property PatientName As String
    Public Property LabMsgSubjectID As Integer
    Public Property SubjectText As String
    Public Property ReceiveDate As Nullable(Of Date)
    Public Property Note As String
    Public Property MessageBody As String
    Public Property MsgDate As DateTime
    Public Property SentDate As Nullable(Of Date)
    Public Property IsSent As Boolean

    Private m_LabMsgDetailIEnumerable As IEnumerable(Of LabMsgDetail)
    Public Property LabMsgDetailIEnumerable As IEnumerable(Of LabMsgDetail)
        Get
            Return m_LabMsgDetailIEnumerable
        End Get
        Set(ByVal value As IEnumerable(Of LabMsgDetail))
            m_LabMsgDetailIEnumerable = value
        End Set
    End Property

    Public Property LabMsgSubject As LabMsgSubject

End Class
