Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections
Imports System.Configuration
Imports Dapper

	Public Class Patient_TrtScope


    Property ScopeID As Integer
    Property MobID As Integer
    Property TrtID As Integer
    Property LVL As Nullable(Of Byte)
    Property ToothCode As String
    Property ToothNum As Nullable(Of Byte)
    Property PropertyName As String
    Property Treat As String
    Property FillColor As String
    Property BorderColor As String
    Property ExternalClinicName As String
    Property Notes As String
    Property IsExternal As Nullable(Of Boolean)



End Class
