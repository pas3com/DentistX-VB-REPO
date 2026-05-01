Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections
Imports System.Configuration
Imports Dapper

Public Class LabMsgSubject

    Public Property LabMsgSubjectID As Integer
    Public Property SubjectName As String
    Public Property SortOrder As Integer
    Public Property IsActive As Boolean

    Private m_LabMsgDetailChoiceIEnumerable As IEnumerable(Of LabMsgDetailChoice)
    Public Property LabMsgDetailChoiceIEnumerable As IEnumerable(Of LabMsgDetailChoice)
        Get
            Return m_LabMsgDetailChoiceIEnumerable
        End Get
        Set(ByVal value As IEnumerable(Of LabMsgDetailChoice))
            m_LabMsgDetailChoiceIEnumerable = value
        End Set
    End Property

End Class
