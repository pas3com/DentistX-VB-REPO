<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Public Class RxFlyPrint
    Inherits DevExpress.XtraReports.UI.XtraReport

    'XtraReport overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Designer
    'It can be modified using the Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim XrWatermark1 As DevExpress.XtraReports.UI.XRWatermark = New DevExpress.XtraReports.UI.XRWatermark()
        Me.TopMargin = New DevExpress.XtraReports.UI.TopMarginBand()
        Me.BottomMargin = New DevExpress.XtraReports.UI.BottomMarginBand()
        Me.Detail = New DevExpress.XtraReports.UI.DetailBand()
        Me.lblSex = New DevExpress.XtraReports.UI.XRLabel()
        Me.lblDrName = New DevExpress.XtraReports.UI.XRLabel()
        Me.lblDate = New DevExpress.XtraReports.UI.XRLabel()
        Me.lblRX = New DevExpress.XtraReports.UI.XRLabel()
        Me.lblAge = New DevExpress.XtraReports.UI.XRLabel()
        Me.lblPatientName = New DevExpress.XtraReports.UI.XRLabel()
        Me.DsRx1 = New DentistX.DsRx()
        Me.RxFlyTableAdapter = New DentistX.DsRxTableAdapters.RxFlyTableAdapter()
        Me.PageHeader = New DevExpress.XtraReports.UI.PageHeaderBand()
        CType(Me.DsRx1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        '
        'TopMargin
        '
        Me.TopMargin.Dpi = 25.4!
        Me.TopMargin.HeightF = 25.4!
        Me.TopMargin.Name = "TopMargin"
        '
        'BottomMargin
        '
        Me.BottomMargin.Dpi = 25.4!
        Me.BottomMargin.HeightF = 20.0!
        Me.BottomMargin.Name = "BottomMargin"
        '
        'Detail
        '
        Me.Detail.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.lblSex, Me.lblDrName, Me.lblDate, Me.lblRX, Me.lblAge, Me.lblPatientName})
        Me.Detail.Dpi = 25.4!
        Me.Detail.HeightF = 156.5813!
        Me.Detail.HierarchyPrintOptions.Indent = 5.08!
        Me.Detail.Name = "Detail"
        '
        'lblSex
        '
        Me.lblSex.Dpi = 25.4!
        Me.lblSex.ExpressionBindings.AddRange(New DevExpress.XtraReports.UI.ExpressionBinding() {New DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[PatientSex]")})
        Me.lblSex.LocationFloat = New DevExpress.Utils.PointFloat(77.7875!, 22.13546!)
        Me.lblSex.Multiline = True
        Me.lblSex.Name = "lblSex"
        Me.lblSex.Padding = New DevExpress.XtraPrinting.PaddingInfo(0.5!, 0.5!, 0!, 0!, 25.4!)
        Me.lblSex.SizeF = New System.Drawing.SizeF(25.4!, 5.841999!)
        Me.lblSex.StylePriority.UseTextAlignment = False
        Me.lblSex.Text = "XrLabel2"
        Me.lblSex.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        '
        'lblDrName
        '
        Me.lblDrName.Dpi = 25.4!
        Me.lblDrName.ExpressionBindings.AddRange(New DevExpress.XtraReports.UI.ExpressionBinding() {New DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[DrName]")})
        Me.lblDrName.Font = New DevExpress.Drawing.DXFont("Mistral", 12.0!, CType((DevExpress.Drawing.DXFontStyle.Bold Or DevExpress.Drawing.DXFontStyle.Italic), DevExpress.Drawing.DXFontStyle))
        Me.lblDrName.LocationFloat = New DevExpress.Utils.PointFloat(4.23336!, 129.2768!)
        Me.lblDrName.Multiline = True
        Me.lblDrName.Name = "lblDrName"
        Me.lblDrName.Padding = New DevExpress.XtraPrinting.PaddingInfo(0.5!, 0.5!, 0!, 0!, 25.4!)
        Me.lblDrName.SizeF = New System.Drawing.SizeF(38.62917!, 5.841995!)
        Me.lblDrName.StylePriority.UseFont = False
        Me.lblDrName.StylePriority.UseTextAlignment = False
        Me.lblDrName.Text = "Dr Imad Abatly"
        Me.lblDrName.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        '
        'lblDate
        '
        Me.lblDate.Dpi = 25.4!
        Me.lblDate.ExpressionBindings.AddRange(New DevExpress.XtraReports.UI.ExpressionBinding() {New DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[RXDate]")})
        Me.lblDate.LocationFloat = New DevExpress.Utils.PointFloat(95.25002!, 129.2768!)
        Me.lblDate.Multiline = True
        Me.lblDate.Name = "lblDate"
        Me.lblDate.Padding = New DevExpress.XtraPrinting.PaddingInfo(0.5!, 0.5!, 0!, 0!, 25.4!)
        Me.lblDate.SizeF = New System.Drawing.SizeF(25.40001!, 5.841995!)
        Me.lblDate.Text = "lblDate"
        Me.lblDate.TextFormatString = "{0:d}"
        '
        'lblRX
        '
        Me.lblRX.Dpi = 25.4!
        Me.lblRX.ExpressionBindings.AddRange(New DevExpress.XtraReports.UI.ExpressionBinding() {New DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[RX]")})
        Me.lblRX.LocationFloat = New DevExpress.Utils.PointFloat(20.20833!, 33.4101!)
        Me.lblRX.Multiline = True
        Me.lblRX.Name = "lblRX"
        Me.lblRX.Padding = New DevExpress.XtraPrinting.PaddingInfo(0.5!, 0.5!, 0!, 0!, 25.4!)
        Me.lblRX.SizeF = New System.Drawing.SizeF(100.4417!, 95.8667!)
        Me.lblRX.Text = "lblRX"
        '
        'lblAge
        '
        Me.lblAge.Dpi = 25.4!
        Me.lblAge.ExpressionBindings.AddRange(New DevExpress.XtraReports.UI.ExpressionBinding() {New DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[PatientAge]")})
        Me.lblAge.LocationFloat = New DevExpress.Utils.PointFloat(4.233333!, 15.89345!)
        Me.lblAge.Multiline = True
        Me.lblAge.Name = "lblAge"
        Me.lblAge.Padding = New DevExpress.XtraPrinting.PaddingInfo(0.5!, 0.5!, 0!, 0!, 25.4!)
        Me.lblAge.SizeF = New System.Drawing.SizeF(25.4!, 5.842!)
        Me.lblAge.Text = "lblAge"
        Me.lblAge.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        '
        'lblPatientName
        '
        Me.lblPatientName.Dpi = 25.4!
        Me.lblPatientName.ExpressionBindings.AddRange(New DevExpress.XtraReports.UI.ExpressionBinding() {New DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[PatientName]")})
        Me.lblPatientName.LocationFloat = New DevExpress.Utils.PointFloat(77.7875!, 15.89345!)
        Me.lblPatientName.Multiline = True
        Me.lblPatientName.Name = "lblPatientName"
        Me.lblPatientName.Padding = New DevExpress.XtraPrinting.PaddingInfo(0.5!, 0.5!, 0!, 0!, 25.4!)
        Me.lblPatientName.SizeF = New System.Drawing.SizeF(25.4!, 5.842!)
        Me.lblPatientName.StylePriority.UseTextAlignment = False
        Me.lblPatientName.Text = "lblPatientName"
        Me.lblPatientName.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        '
        'DsRx1
        '
        Me.DsRx1.DataSetName = "DsRx"
        Me.DsRx1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'RxFlyTableAdapter
        '
        Me.RxFlyTableAdapter.ClearBeforeFill = True
        '
        'PageHeader
        '
        Me.PageHeader.Dpi = 25.4!
        Me.PageHeader.HeightF = 34.0!
        Me.PageHeader.Name = "PageHeader"
        '
        'RxFlyPrint
        '
        Me.Bands.AddRange(New DevExpress.XtraReports.UI.Band() {Me.TopMargin, Me.BottomMargin, Me.Detail, Me.PageHeader})
        Me.ComponentStorage.AddRange(New System.ComponentModel.IComponent() {Me.DsRx1})
        Me.DataAdapter = Me.RxFlyTableAdapter
        Me.DataMember = "RxFly"
        Me.DataSource = Me.DsRx1
        Me.Dpi = 25.4!
        Me.Font = New DevExpress.Drawing.DXFont("Arial", 9.75!)
        Me.Margins = New DevExpress.Drawing.DXMargins(0.5!, 0.5!, 25.4!, 20.0!)
        Me.PageHeightF = 200.0!
        Me.PageWidthF = 140.0!
        Me.PaperKind = DevExpress.Drawing.Printing.DXPaperKind.Custom
        Me.ReportUnit = DevExpress.XtraReports.UI.ReportUnit.Millimeters
        Me.ScriptLanguage = DevExpress.XtraReports.ScriptLanguage.VisualBasic
        Me.SnapGridSize = 2.5!
        Me.Version = "25.1"
        XrWatermark1.Id = "Watermark1"
        Me.Watermarks.AddRange(New DevExpress.XtraPrinting.Drawing.Watermark() {XrWatermark1})
        CType(Me.DsRx1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()

    End Sub

    Friend WithEvents TopMargin As DevExpress.XtraReports.UI.TopMarginBand
    Friend WithEvents BottomMargin As DevExpress.XtraReports.UI.BottomMarginBand
    Friend WithEvents Detail As DevExpress.XtraReports.UI.DetailBand
    Friend WithEvents lblDate As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents lblRX As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents lblAge As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents lblPatientName As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents DsRx1 As DsRx
    Friend WithEvents RxFlyTableAdapter As DsRxTableAdapters.RxFlyTableAdapter
    Friend WithEvents PageHeader As DevExpress.XtraReports.UI.PageHeaderBand
    Friend WithEvents lblDrName As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents lblSex As DevExpress.XtraReports.UI.XRLabel
End Class
