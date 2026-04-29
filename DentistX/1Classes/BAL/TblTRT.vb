Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections
Imports System.Configuration
Imports Dapper

	Public Class TblTRTS
    Property TrtID As Integer
    Property Trt As String
    Property TrtValue As Nullable(Of Decimal)
    Property ShapeID As Nullable(Of Integer)
    Property TrtDetails As String
    Property TrtLVL As String
    Property TrtLoc As String
    Property ToothID As String
    Property ToothIDkID As String
    Property OldTrt As String
    Property TrtGroup As String
    Property ParentGroup As String
    Property TrtColor As String
    Property TrtBrdrClr As String
    Property TrtBrdrThick As Nullable(Of Byte)
    Property KidTrt As Byte
    Property TrtClrID As Nullable(Of Integer)
    Property DefFillColor As String
    Property DefBrdrColor As String
    Property DefBrdrThick As Nullable(Of Byte)

End Class
