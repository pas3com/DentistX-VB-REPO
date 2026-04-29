Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections
Imports System.Configuration
Imports Dapper

Public Class Patient_Trts
    Property TrtID As Integer
    Property PatientID As Integer
    Property ToothTrtID As Nullable(Of Integer)
    Property OrthoID As Nullable(Of Integer)
    Property OtherTrtID As Nullable(Of Integer)
    Property Detail As String
    Property TrtDate As DateTime
    Property TrtValue As Decimal
    Property IsMultiTooth As Nullable(Of Boolean)
    Property Discount As Nullable(Of Decimal)
    Property Discount2 As Nullable(Of Decimal)
    Property DiscountType As Nullable(Of Byte)
    Public Property Patient_PaysIEnumerable As IEnumerable(Of Patient_Pays)
    Public Property Patient_TrtScopeIEnumerable As IEnumerable(Of Patient_TrtScope)
    ' Not mapped to DB
    <ComponentModel.Browsable(False)>
    Public Property IsModified As Boolean = False
End Class
