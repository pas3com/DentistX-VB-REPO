Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections
Imports System.Configuration
Imports Dapper

Public Class Patient_MobInfo



    Property MobInfoID As Integer

    Property PatientID As Nullable(Of Integer)

    Property ParentToothTrtID As Nullable(Of Integer)

    Property TrtGroupID As Nullable(Of Guid)

    Property ToothNum As Nullable(Of Byte)

    Property ToothName As String

    Property TreatDate As Nullable(Of DateTime)

    Property Treat As String

    Property TreatNotes As String

    Property IsExternal As Nullable(Of Boolean)

    Property ExternalClinicName As String

    Property ExternalTreatmentDate As Nullable(Of DateTime)




End Class
