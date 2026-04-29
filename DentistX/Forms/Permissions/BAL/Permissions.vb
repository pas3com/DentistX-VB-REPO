Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections
Imports System.Configuration
Imports Dapper

Public Class Permissions



	Property PermID As Integer

	Property PermKey As String

	Property PermName As String

	Property PermDescription As String

	Property Category As String

	Property IsActive As Nullable(Of Boolean)

	Property CreatedAt As Nullable(Of DateTime)

	Public Property GroupPermissionsIEnumerable As IEnumerable(Of GroupPermissions)

	Public Property UserPermissionsIEnumerable As IEnumerable(Of UserPermissions)




End Class
