Imports System
Imports System.Collections.Generic
Imports System.Data.SqlClient
Imports System.Threading.Tasks
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraPrinting
Imports DevExpress.XtraScheduler

Module DashClassesModel








    '=====================================================
    Public Class ExportHelper
        Public Shared Sub ExportToExcel(gridView As GridView, fileName As String)
            Try
                Using saveDialog As New SaveFileDialog()
                    saveDialog.Filter = "Excel Files|*.xlsx"
                    saveDialog.FileName = fileName

                    If saveDialog.ShowDialog() = DialogResult.OK Then
                        gridView.ExportToXlsx(saveDialog.FileName)
                    End If
                End Using
            Catch ex As Exception
                Throw New Exception($"Export failed: {ex.Message}", ex)
            End Try
        End Sub

        Public Shared Sub ExportToPDF(gridView As GridView, fileName As String)
            Try
                Using saveDialog As New SaveFileDialog()
                    saveDialog.Filter = "PDF Files|*.pdf"
                    saveDialog.FileName = fileName

                    If saveDialog.ShowDialog() = DialogResult.OK Then
                        gridView.ExportToPdf(saveDialog.FileName)
                    End If
                End Using
            Catch ex As Exception
                Throw New Exception($"Export failed: {ex.Message}", ex)
            End Try
        End Sub

        Public Shared Sub PrintPreview(gridControl As GridControl)
            Dim link As New PrintableComponentLink(New PrintingSystem())
            link.Component = gridControl
            link.CreateDocument()
            link.ShowPreview()
        End Sub
    End Class
    '=====================================================
    ' Patient Model
    Public Class Patient4
        Public Property PatientID As Integer
        Public Property PatientName As String
        Public Property PatientNumber As String
        Public Property Sex As String
        Public Property Age As Integer?
        Public Property StillKid As Boolean?
        Public Property Phone As String
        Public Property Address As String
        Public Property Health As String
        Public Property Treat As Boolean?
        Public Property Implant As Boolean?
        Public Property Mobile As Boolean?
        Public Property Ortho As Boolean?
        Public Property Diag As Boolean?
        Public Property Struc As Boolean?
        Public Property Notes As String
        Public Property BirthY As Integer?
        Public Property CreatedBy As Integer?
        Public Property CreateDate As Date?
    End Class

    ' Appointment Model


    ' Treatment Model

    ' Payment Model
    Public Class PatientPayment1
        Public Property PayID As Integer
        Public Property TrtID As Integer
        Public Property PatientID As Integer?
        Public Property PayValue As Decimal
        Public Property PayDate As Date
        Public Property Notes As String
        Public Property PayType As String
        Public Property ChqOwner As String
        Public Property AccountNumber As String
        Public Property ChqNumber As String
        Public Property ChqDueDate As Date?
        Public Property ChqBank As String
        Public Property IsCashed As Boolean?
        Public Property InsuranceCompany As String
        Public Property InsuranceNotes As String
        Public Property IsForward As Boolean?
        Public Property ForwardFromTo As String
        Public Property PatientName As String ' For display
        Public Property TreatmentDetail As String ' For display
    End Class

    ' Dashboard KPI Model


    ' Chart Data Models






    ' Filter Model


End Module
