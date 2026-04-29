Imports System.Data.SqlClient
Imports Dapper

Public Class Patient

    ' All three financial numbers come from a single GetPatientBalance() call and are cached together.
    ' Touching Balance / TotalTreatments / TotalPayments triggers the fetch once, then serves all three.
    Private _patientBalance As PatientBalance
    Private _age As Integer?
    Private _isKid As Boolean?

    Public Property IsFull() As Boolean
    Public Property IsGrid() As Boolean

    ' Computed property for IsKid with proper implementation
    Public Property IsKid() As Boolean
        Get
            If Not _isKid.HasValue Then
                _isKid = (Me.Age.GetValueOrDefault() <= 11)
            End If
            Return _isKid.Value
        End Get
        Set(ByVal value As Boolean)
            _isKid = value
        End Set
    End Property

    Property StillKid As Nullable(Of Boolean)

    ''' <summary>Lazily fetches (once) and caches the full PatientBalance for this patient so Balance,
    ''' TotalTreatments, and TotalPayments all come from a single DB round-trip.</summary>
    Private Function EnsurePatientBalance() As PatientBalance
        If _patientBalance Is Nothing Then
            Dim dal As New Patient_TrtsDATA()
            _patientBalance = dal.GetPatientBalance(Me.PatientID)
        End If
        Return _patientBalance
    End Function

    ' Computed property for Balance with caching (shares one PatientBalance fetch with TotalTreatments / TotalPayments).
    Public ReadOnly Property Balance As Double
        Get
            Dim pb = EnsurePatientBalance()
            Return CDbl(pb.Balance)
        End Get
    End Property

    ''' <summary>Sum of all treatment values for this patient (same computation as Accounting header totals).</summary>
    Public ReadOnly Property TotalTreatments As Decimal
        Get
            Dim pb = EnsurePatientBalance()
            Return pb.TotalTreatments
        End Get
    End Property

    ''' <summary>Sum of all payment values for this patient, excluding returned cheques (same rule as GetPatientBalance SQL).</summary>
    Public ReadOnly Property TotalPayments As Decimal
        Get
            Dim pb = EnsurePatientBalance()
            Return pb.TotalPayments
        End Get
    End Property

    ' Computed property for Age with caching
    Public Property Age As Nullable(Of Integer)
        Get
            If Not _age.HasValue Then
                _age = CalculateAge(Me.PatientID)
            End If
            Return _age

        End Get
        Set(ByVal value As Nullable(Of Integer))
            _age = value
        End Set
    End Property

    ' Function to get balance (same rules as Patient_TrtsDATA.GetPatientBalance — returned cheques excluded from payment total).
    ' Kept for callers that pass an arbitrary ID. For the current patient, prefer the cached Balance property.
    Public Function GetBalance(ByVal PatientID As Integer) As Double
        Dim dal As New Patient_TrtsDATA()
        Dim pb = dal.GetPatientBalance(PatientID)
        Return CDbl(pb.Balance)
    End Function


    ' (Removed unused nested Patient.PatientBalance DTO; the top-level PatientBalance class at
    ' 1Classes\BAL\PatientBalance.vb is the one GetPatientBalance actually returns.)


    ' Function to calculate age based on birth year
    Private Function CalculateAge(ByVal PatientID As Integer) As Integer
        Dim query As String = "SELECT BirthY FROM Patient WHERE PatientID = @PatientID"

        Try
            Using conn As New SqlConnection(DentistXDATA.GetConnection.ConnectionString)
                Dim birthYear As Integer? = conn.QuerySingleOrDefault(Of Integer?)(query, New With {.PatientID = PatientID})

                If birthYear.HasValue AndAlso birthYear.Value > 0 Then
                    Return DateTime.Now.Year - birthYear.Value
                Else
                    Return 0 ' Default value if no birth year or invalid
                End If
            End Using
        Catch ex As Exception
            ' Consider logging instead of showing message boxes in properties
            Debug.WriteLine("Error calculating age: " & ex.Message)
            Return 0 ' Default value in case of an error
        End Try
    End Function

    ' Method to refresh computed properties (call this when data might have changed)
    Public Sub RefreshComputedProperties()
        _patientBalance = Nothing
        _age = Nothing
        _isKid = Nothing
    End Sub

    Public Function HasAnyCategory() As Boolean
        Return Treat.GetValueOrDefault(False) OrElse Ortho.GetValueOrDefault(False) OrElse Mobile.GetValueOrDefault(False) OrElse
            Struc.GetValueOrDefault(False) OrElse Implant.GetValueOrDefault(False)
    End Function
    Property PatientID As Integer
    Property PatientName As String
    Property Sex As String
    Property PatientNumber As String
    Property Phone As String
    Property WhatsAppPrefix As String
    Property WhatsApp As String
    Property Address As String
    Property Health As String
    Property Treat As Nullable(Of Boolean)
    Property Implant As Nullable(Of Boolean)
    Property Mobile As Nullable(Of Boolean)
    Property Ortho As Nullable(Of Boolean)
    Property Diag As Nullable(Of Boolean)
    Property Struc As Nullable(Of Boolean)
    Property Notes As String
    Property BirthY As Nullable(Of Integer)
    Property CreatedBy As Nullable(Of Integer)
    Property CreateDate As Nullable(Of DateTime)
    Public Property LabOrderIEnumerable As IEnumerable(Of LabOrder)
    Public Property LDIEnumerable As IEnumerable(Of LD)
    Public Property LDPLIEnumerable As IEnumerable(Of LDPL)
    Public Property LDSTYLEIEnumerable As IEnumerable(Of LDSTYLE)
    Public Property LUIEnumerable As IEnumerable(Of LU)
    Public Property LUPLIEnumerable As IEnumerable(Of LUPL)
    Public Property LUSTYLEIEnumerable As IEnumerable(Of LUSTYLE)
    Public Property OrthoDiagIEnumerable As IEnumerable(Of OrthoDiag)
    Public Property OrthoInfIEnumerable As IEnumerable(Of OrthoInf)
    Public Property OrthoTreatIEnumerable As IEnumerable(Of OrthoTreat)
    Public Property OrthoTrtDetIEnumerable As IEnumerable(Of OrthoTrtDet)
    Public Property OtherTrtsIEnumerable As IEnumerable(Of OtherTrts)
    Public Property Patient_ImgsIEnumerable As IEnumerable(Of Patient_Imgs)
    Public Property Patient_MobStrucIEnumerable As IEnumerable(Of Patient_MobStruc)
    Public Property Patient_NotesIEnumerable As IEnumerable(Of Patient_Notes)
    Public Property Patient_RXIEnumerable As IEnumerable(Of Patient_RX)
    Public Property Patient_ToothChartIEnumerable As IEnumerable(Of Patient_ToothChart)
    Public Property Patient_ToothCheckIEnumerable As IEnumerable(Of Patient_ToothCheck)
    Public Property Patient_ToothDiagIEnumerable As IEnumerable(Of Patient_ToothDiag)
    Public Property Patient_ToothTrtIEnumerable As IEnumerable(Of Patient_ToothTrt)
    Public Property Patient_TrtsIEnumerable As IEnumerable(Of Patient_Trts)
    Public Property PatientColorsIEnumerable As IEnumerable(Of PatientColors)
    Public Property RDIEnumerable As IEnumerable(Of RD)
    Public Property RDPLIEnumerable As IEnumerable(Of RDPL)
    Public Property RDSTYLEIEnumerable As IEnumerable(Of RDSTYLE)
    Public Property RUIEnumerable As IEnumerable(Of RU)
    Public Property RUPLIEnumerable As IEnumerable(Of RUPL)
    Public Property RUSTYLEIEnumerable As IEnumerable(Of RUSTYLE)
    Public Property SurgeryIEnumerable As IEnumerable(Of Surgery)
    Public Property VisitsIEnumerable As IEnumerable(Of Visits)

End Class
