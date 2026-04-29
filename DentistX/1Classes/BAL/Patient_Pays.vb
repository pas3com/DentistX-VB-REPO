Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections
Imports System.Configuration
Imports Dapper

Public Class Patient_Pays
    Property PayID As Integer
    Property TrtID As Integer
    Property PatientID As Nullable(Of Integer)
    Property PatientName As String
    Property PayValue As Decimal
    Property PayDate As DateTime
    Property Notes As String
    Property PayType As String
    Property ChqOwner As String
    Property AccountNumber As String
    Property ChqNumber As String
    Property ChqDueDate As Nullable(Of DateTime)
    Property ChqBank As String
    Property IsCashed As Nullable(Of Boolean) 'IsForward
    Property InsuranceCompany As String
    Property InsuranceNotes As String
    Property IsForward As Nullable(Of Boolean)
    Property ForwardFromTo As String
    Property ReceivedBy As String
    Property IsReturned As Nullable(Of Boolean)
    ' Not mapped to DB
    <ComponentModel.Browsable(False)>
    Public Property IsModified As Boolean = False

End Class
