Imports DevExpress.XtraReports.UI

Public Class frmReportViewer
    Public Sub New(report As XtraReport)
        InitializeComponent()
        DocumentViewer1.DocumentSource = report
        report.CreateDocument()
    End Sub
End Class