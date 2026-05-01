Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections
Imports System.Configuration
Imports Dapper

Public Class LabMsgDetailChoice

    Public Property LabMsgDetailChoiceID As Integer
    Public Property LabMsgSubjectID As Integer
    Public Property DetailText As String
    Public Property SortOrder As Integer
    Public Property IsActive As Boolean

    Public Property LabMsgSubject As LabMsgSubject

End Class
