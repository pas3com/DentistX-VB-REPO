Imports System.ComponentModel
Imports System.Linq
Imports DevExpress.XtraReports.UI
Imports Stock.Models

Public Class rptSupplierInvoice
    Public Sub New()
        InitializeComponent()
        lays()
    End Sub

    ''' <summary>
    ''' Convenient constructor to feed the report with all header values and the line items list.
    ''' </summary>
    Friend Sub New(
        clinicName As String,
        clinicAddress As String,
        clinicPhone As String,
        supplierName As String,
        invoiceNumber As Integer,
        invoiceDate As Date,
        dueDate As Date,
        invoiceStatus As String,
        totalAmount As Decimal,
        lines As IEnumerable(Of BuyInvoiceLineItem),
        Optional showPayments As Boolean = False,
        Optional paymentsText As String = ""
    )
        InitializeComponent()
        lays()
        ' Header values
        Me.parClinicName.Value = clinicName
        Me.parClinicAddress.Value = clinicAddress
        Me.parClinicPhone.Value = clinicPhone

        Me.parSupplierName.Value = supplierName
        Me.parInvoiceNumber.Value = invoiceNumber
        Me.parInvoiceDate.Value = invoiceDate
        Me.parDueDate.Value = dueDate
        Me.parInvoiceStatus.Value = invoiceStatus
        Me.parTotalAmount.Value = totalAmount

        Me.parShowPayments.Value = showPayments
        Me.parPaymentsText.Value = paymentsText

        ' Assign DataSource for line items
        Dim listLines = If(lines?.ToList(), New List(Of BuyInvoiceLineItem)())
        Me.DataSource = listLines

        ' Fill visible header labels (designer uses "-" defaults)
        xrLabelClinicName.Text = If(clinicName, "-")
        xrLabelClinicAddress.Text = If(clinicAddress, "-")
        xrLabelClinicPhone.Text = If(clinicPhone, "-")

        xrLabelSupplierValue.Text = If(supplierName, "-")
        xrLabelInvoiceValue.Text = invoiceNumber.ToString()
        xrLabelInvoiceDateValue.Text = invoiceDate.ToString("dd/MM/yyyy")
        xrLabelDueDateValue.Text = dueDate.ToString("dd/MM/yyyy")
        xrLabelStatusValue.Text = If(invoiceStatus, "-")
        xrLabelTotalValue.Text = totalAmount.ToString("N2")
        xrLabelGrandTotalValue.Text = totalAmount.ToString("N2")

        ' Optional payments block
        xrLabelPaymentsCaption.Visible = showPayments
        xrPanelPayments.Visible = showPayments
        xrLabelPaymentsText.Text = If(String.IsNullOrWhiteSpace(paymentsText), "-", paymentsText)

    End Sub

    Private Sub lays()
        Dim leftX As Single = 20.0!
        Dim labelW As Single = 110.0!
        Dim valueX As Single = 140.0!
        Dim y1 As Single = 8.0!
        Dim rowH As Single = 16.0!

        'Supplier
        Me.xrLabelSupplierCaption.LocationFloat = New DevExpress.Utils.PointFloat(leftX, y1)
        Me.xrLabelSupplierCaption.SizeF = New System.Drawing.SizeF(labelW, rowH)
        Me.xrLabelSupplierCaption.Text = "Supplier:"

        Me.xrLabelSupplierValue.LocationFloat = New DevExpress.Utils.PointFloat(valueX, y1)
        Me.xrLabelSupplierValue.SizeF = New System.Drawing.SizeF(670.0!, rowH)
        Me.xrLabelSupplierValue.Text = "-"

        'Invoice no
        Me.xrLabelInvoiceCaption.LocationFloat = New DevExpress.Utils.PointFloat(leftX, y1 + rowH)
        Me.xrLabelInvoiceCaption.SizeF = New System.Drawing.SizeF(labelW, rowH)
        Me.xrLabelInvoiceCaption.Text = "Invoice No:"

        Me.xrLabelInvoiceValue.LocationFloat = New DevExpress.Utils.PointFloat(valueX, y1 + rowH)
        Me.xrLabelInvoiceValue.SizeF = New System.Drawing.SizeF(260.0!, rowH)
        Me.xrLabelInvoiceValue.Text = "-"

        'Invoice date
        Me.xrLabelInvoiceDateCaption.LocationFloat = New DevExpress.Utils.PointFloat(420.0!, y1 + rowH)
        Me.xrLabelInvoiceDateCaption.SizeF = New System.Drawing.SizeF(120.0!, rowH)
        Me.xrLabelInvoiceDateCaption.Text = "Date:"

        Me.xrLabelInvoiceDateValue.LocationFloat = New DevExpress.Utils.PointFloat(545.0!, y1 + rowH)
        Me.xrLabelInvoiceDateValue.SizeF = New System.Drawing.SizeF(240.0!, rowH)
        Me.xrLabelInvoiceDateValue.Text = "-"

        'Due date
        Me.xrLabelDueDateCaption.LocationFloat = New DevExpress.Utils.PointFloat(leftX, y1 + 2 * rowH)
        Me.xrLabelDueDateCaption.SizeF = New System.Drawing.SizeF(labelW, rowH)
        Me.xrLabelDueDateCaption.Text = "Due Date:"

        Me.xrLabelDueDateValue.LocationFloat = New DevExpress.Utils.PointFloat(valueX, y1 + 2 * rowH)
        Me.xrLabelDueDateValue.SizeF = New System.Drawing.SizeF(260.0!, rowH)
        Me.xrLabelDueDateValue.Text = "-"

        'Status
        Me.xrLabelStatusCaption.LocationFloat = New DevExpress.Utils.PointFloat(420.0!, y1 + 2 * rowH)
        Me.xrLabelStatusCaption.SizeF = New System.Drawing.SizeF(120.0!, rowH)
        Me.xrLabelStatusCaption.Text = "Status:"

        Me.xrLabelStatusValue.LocationFloat = New DevExpress.Utils.PointFloat(545.0!, y1 + 2 * rowH)
        Me.xrLabelStatusValue.SizeF = New System.Drawing.SizeF(240.0!, rowH)
        Me.xrLabelStatusValue.Text = "-"

        'Total
        Me.xrLabelTotalCaption.LocationFloat = New DevExpress.Utils.PointFloat(leftX, y1 + 3 * rowH)
        Me.xrLabelTotalCaption.SizeF = New System.Drawing.SizeF(labelW, rowH)
        Me.xrLabelTotalCaption.Text = "Total:"

        Me.xrLabelTotalValue.LocationFloat = New DevExpress.Utils.PointFloat(valueX, y1 + 3 * rowH)
        Me.xrLabelTotalValue.SizeF = New System.Drawing.SizeF(260.0!, rowH)
        Me.xrLabelTotalValue.Text = "-"
    End Sub
    Private Sub rptSupplierInvoice_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles Me.BeforePrint
        ' Keep header labels consistent if a caller only set parameters and DataSource.
        Try
            If parClinicName.Value IsNot Nothing Then xrLabelClinicName.Text = CStr(parClinicName.Value)
            If parClinicAddress.Value IsNot Nothing Then xrLabelClinicAddress.Text = CStr(parClinicAddress.Value)
            If parClinicPhone.Value IsNot Nothing Then xrLabelClinicPhone.Text = CStr(parClinicPhone.Value)

            If parSupplierName.Value IsNot Nothing Then xrLabelSupplierValue.Text = CStr(parSupplierName.Value)
            If parInvoiceNumber.Value IsNot Nothing Then xrLabelInvoiceValue.Text = CInt(parInvoiceNumber.Value).ToString()
            If parInvoiceDate.Value IsNot Nothing Then xrLabelInvoiceDateValue.Text = CType(parInvoiceDate.Value, Date).ToString("dd/MM/yyyy")
            If parDueDate.Value IsNot Nothing Then xrLabelDueDateValue.Text = CType(parDueDate.Value, Date).ToString("dd/MM/yyyy")
            If parInvoiceStatus.Value IsNot Nothing Then xrLabelStatusValue.Text = CStr(parInvoiceStatus.Value)

            If parTotalAmount.Value IsNot Nothing Then xrLabelTotalValue.Text = CType(parTotalAmount.Value, Decimal).ToString("N2")
            If parTotalAmount.Value IsNot Nothing Then xrLabelGrandTotalValue.Text = CType(parTotalAmount.Value, Decimal).ToString("N2")

            Dim showPay As Boolean = False
            If parShowPayments.Value IsNot Nothing Then showPay = CType(parShowPayments.Value, Boolean)

            xrLabelPaymentsCaption.Visible = showPay
            xrPanelPayments.Visible = showPay
            xrLabelPaymentsText.Text = If(String.IsNullOrWhiteSpace(CStr(parPaymentsText.Value)), "-", CStr(parPaymentsText.Value))
        Catch
            ' Do nothing; the designer defaults are safe.
        End Try
    End Sub
End Class

