Imports System.Data.SqlClient
Imports Dapper

''' <summary>Repository for accounting summaries with filters.</summary>
Public Class AccountingRepository
    Private ReadOnly _connStr As String = DentistXDATA.GetConnection.ConnectionString

    ''' <summary>Get patient accounting summaries with optional filters. Period applies to TrtDate and PayDate.</summary>
    Public Function GetPatientAccountingSummaries(
        fromDate As DateTime,
        toDate As DateTime,
        Optional patientNameLike As String = Nothing,
        Optional payType As String = Nothing,
        Optional minTrtAmount As Decimal? = Nothing,
        Optional maxTrtAmount As Decimal? = Nothing,
        Optional minPayAmount As Decimal? = Nothing,
        Optional maxPayAmount As Decimal? = Nothing,
        Optional balanceOnlyNonZero As Boolean = False) As List(Of AccountingSummaryDto)

        Using conn As New SqlConnection(_connStr)
            conn.Open()

            Dim toDateEnd = toDate.Date.AddDays(1)
            Dim sql = "
;WITH TrtSums AS (
    SELECT PatientID,
           SUM(TrtValue - ISNULL(Discount, 0)) AS TotalTrt
    FROM dbo.Patient_Trts
    WHERE TrtDate >= @FromDate AND TrtDate < @ToDateEnd
    GROUP BY PatientID
),
PaySums AS (
    SELECT PatientID,
           SUM(PayValue) AS TotalPay
    FROM dbo.Patient_Pays
    WHERE PayDate >= @FromDate AND PayDate < @ToDateEnd
      AND (@PayType IS NULL OR @PayType = '' OR PayType = @PayType)
    GROUP BY PatientID
)
SELECT p.PatientID,
       p.PatientName,
       ISNULL(NULLIF(RTRIM(p.WhatsApp), ''), RTRIM(p.Phone)) AS PatientPhone,
       ISNULL(t.TotalTrt, 0) AS TotalTreatments,
       ISNULL(pp.TotalPay, 0) AS TotalPayments,
       ISNULL(t.TotalTrt, 0) - ISNULL(pp.TotalPay, 0) AS Balance
FROM dbo.Patient p
LEFT JOIN TrtSums t ON p.PatientID = t.PatientID
LEFT JOIN PaySums pp ON p.PatientID = pp.PatientID
WHERE (t.PatientID IS NOT NULL OR pp.PatientID IS NOT NULL)
  AND (@PatientName IS NULL OR @PatientName = '' OR p.PatientName LIKE @PatientNameLike)
  AND (@MinTrt IS NULL OR ISNULL(t.TotalTrt, 0) >= @MinTrt)
  AND (@MaxTrt IS NULL OR ISNULL(t.TotalTrt, 0) <= @MaxTrt)
  AND (@MinPay IS NULL OR ISNULL(pp.TotalPay, 0) >= @MinPay)
  AND (@MaxPay IS NULL OR ISNULL(pp.TotalPay, 0) <= @MaxPay)
  AND (@BalanceOnly = 0 OR (ISNULL(t.TotalTrt, 0) - ISNULL(pp.TotalPay, 0)) <> 0)
ORDER BY p.PatientName"

            Dim param = New With {
                .FromDate = fromDate.Date,
                .ToDateEnd = toDateEnd,
                .PayType = If(String.IsNullOrWhiteSpace(payType), CType(Nothing, String), payType.Trim()),
                .PatientName = If(String.IsNullOrWhiteSpace(patientNameLike), CType(Nothing, String), patientNameLike.Trim()),
                .PatientNameLike = If(String.IsNullOrWhiteSpace(patientNameLike), CType(Nothing, String), "%" & patientNameLike.Trim() & "%"),
                .MinTrt = minTrtAmount,
                .MaxTrt = maxTrtAmount,
                .MinPay = minPayAmount,
                .MaxPay = maxPayAmount,
                .BalanceOnly = If(balanceOnlyNonZero, 1, 0)
            }

            Return conn.Query(Of AccountingSummaryDto)(sql, param).ToList()
        End Using
    End Function

    ''' <summary>Get distinct PayType values from Patient_Pays.</summary>
    Public Function GetDistinctPayTypes() As List(Of String)
        Using conn As New SqlConnection(_connStr)
            Return conn.Query(Of String)(
                "SELECT DISTINCT PayType FROM dbo.Patient_Pays WHERE PayType IS NOT NULL AND RTRIM(PayType) <> '' ORDER BY PayType").ToList()
        End Using
    End Function
End Class
