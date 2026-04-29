Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections
Imports System.Configuration
Imports Dapper

Public Class Patient_Mobile


    Property MobID As Integer

    Property PatientID As Nullable(Of Integer)

    Property ShapeID As Nullable(Of Integer)
    Property ToothNums As String

    Property ToothNum As Nullable(Of Byte)
    Property ToothName As String

    Property LVL As Nullable(Of Byte)

    Property PropertyName As String

    Property FillColor As String

    Property BorderThickness As Nullable(Of Byte)

    Property BorderColor As String

    Property TreatmentType As String

    Property TreatDate As Nullable(Of DateTime)

    Property Treat As String

    Property TreatPlan As String

    Property TreatDetails As String

    Property TreatNotes As String

    Property Finished As Nullable(Of Byte)

    Property TreatEndDate As Nullable(Of DateTime)

    Property QrtrTable As String

    Property QrtrID As Nullable(Of Integer)

    Property QrtrAddress As Nullable(Of Integer)

    Property IsExternal As Nullable(Of Boolean)

    Property ExternalClinicName As String

    Property ExternalTreatmentDate As Nullable(Of DateTime)

    Property IsPaid As Nullable(Of Boolean)
    Public Property IsMultiTrt As Boolean
    Public Property ParentToothTrtID As Nullable(Of Integer)
    Public Property TrtGroupID As Guid ' Not nullable Guid
    Property Patient_TrtScope As IEnumerable(Of Patient_TrtScope)
    Property Patient_Trts As IEnumerable(Of Patient_Trts)

End Class
