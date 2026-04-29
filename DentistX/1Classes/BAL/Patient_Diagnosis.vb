Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections
Imports System.Configuration
Imports Dapper

	Public Class Patient_Diagnosis



	Property DiagID As Integer

	Property PatientID As Nullable(Of Integer)

	Property ShapeID As Nullable(Of Integer)

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

	Property TrtLoc As String

	Property QrtrID As Nullable(Of Integer)

	Property QrtrAddress As Nullable(Of Integer)

	Property QrtrColumnName As String

	Property QrtrColumnValue As String

	Property IsExternal As Nullable(Of Boolean)

	Property ExternalClinicName As String

	Property ExternalTreatmentDate As Nullable(Of DateTime)

	Property IsPaid As Nullable(Of Boolean)

	Property IsMultiTrt As Nullable(Of Boolean)

	Property ParentDiagID As Nullable(Of Integer)

	Property TrtGroupID As Nullable(Of Guid)

	Property isOnImplant As Nullable(Of Boolean)

	Property UserID As Nullable(Of Integer)



End Class
