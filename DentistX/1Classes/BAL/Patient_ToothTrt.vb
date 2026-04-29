Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data.SqlClient
Imports System.Collections
Imports System.Configuration
Imports Dapper


Public Class Patient_ToothTrt
    Public Overrides Function Equals(obj As Object) As Boolean
        If obj Is Nothing OrElse Me.GetType() IsNot obj.GetType() Then
            Return False
        End If

        Dim other As Patient_ToothTrt = CType(obj, Patient_ToothTrt)

        Return ToothTrtID = other.ToothTrtID AndAlso
           PatientID = other.PatientID AndAlso
           ToothNum = other.ToothNum AndAlso
           String.Equals(ToothName, other.ToothName) AndAlso
           String.Equals(Treat, other.Treat) AndAlso
           TreatDate.GetValueOrDefault() = other.TreatDate.GetValueOrDefault() AndAlso
           String.Equals(BorderColor, other.BorderColor) AndAlso
           BorderThickness = other.BorderThickness AndAlso
           String.Equals(FillColor, other.FillColor) AndAlso
           Finished = other.Finished AndAlso
           String.Equals(PropertyName, other.PropertyName) AndAlso
           String.Equals(TreatDetails, other.TreatDetails) AndAlso
           TreatEndDate.GetValueOrDefault() = other.TreatEndDate.GetValueOrDefault() AndAlso
           String.Equals(TreatmentType, other.TreatmentType) AndAlso
           String.Equals(TreatNotes, other.TreatNotes) AndAlso
           String.Equals(TreatPlan, other.TreatPlan)
    End Function
    Public Function Clone() As Patient_ToothTrt
        Return DirectCast(Me.MemberwiseClone(), Patient_ToothTrt)
    End Function
    Property ToothTrtID As Integer
    Property PatientID As Integer
    Property ShapeID As Integer
    Property ToothNum As Byte
    Property ToothName As String
    Property LVL As Byte
    Property PropertyName As String
    Property FillColor As String
    Property BorderThickness As Nullable(Of Byte)
    Property BorderColor As String
    Property TreatmentType As String
    Property TreatDate As Nullable(Of DateTime)
    Property Treat As String
    Property TrtValue As Decimal
    Property PayValue As Decimal
    Property TreatPlan As String
    Property TreatDetails As String
    Property TreatNotes As String
    Property Finished As Nullable(Of Byte)
    Property TreatEndDate As Nullable(Of DateTime)
    Property TrtLoc As String
    Property QrtrID As Integer
    Property QrtrTable As String
    Property QrtrColumnName As String
    Property QrtrColumnValue As String
    Property QrtrAddress As Integer
    Property IsExternal As Nullable(Of Boolean)
    Property ExternalClinicName As String
    Property ExternalTreatmentDate As Nullable(Of DateTime)
    Property IsPaid As Nullable(Of Boolean)
    Public Property IsMultiTrt As Boolean
    Public Property ParentToothTrtID As Nullable(Of Integer)
    Public Property TrtGroupID As Guid ' Not nullable Guid
    Public Property isOnImplant As Nullable(Of Boolean)
    Public Property UserID As Nullable(Of Integer)
    Property CapFill As String

    Property RootFill As String

End Class
