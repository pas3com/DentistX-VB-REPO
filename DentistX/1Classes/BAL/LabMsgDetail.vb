Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections
Imports System.Configuration
Imports Dapper

Public Class LabMsgDetail

    Public Property LabMsgDetailID As Integer
    Public Property LabMsgID As Integer
    Public Property LabMsgDetailChoiceID As Nullable(Of Integer)
    Public Property DetailText As String
    Public Property SortOrder As Integer

    Public Property LabMsgs As LabMsgs
    Public Property LabMsgDetailChoice As LabMsgDetailChoice

End Class
