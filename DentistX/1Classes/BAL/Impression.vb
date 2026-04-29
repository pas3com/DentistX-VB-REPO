Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections
Imports System.Configuration
Imports Dapper

Public Class Impression
    Property ImprID As Integer
    Property ImprType As String
    Public Property ImprDetIEnumerable As IEnumerable(Of ImprDet)
End Class
