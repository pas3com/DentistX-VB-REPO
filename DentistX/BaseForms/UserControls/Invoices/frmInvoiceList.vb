Imports System.Data.SqlClient
Imports DevExpress.XtraEditors
Imports DevExpress.XtraGrid

Public Class frmInvoiceList
    Private patientID As Integer

    Public Sub New(ByVal patID As Integer)
        InitializeComponent()
        patientID = patID
        LoadInvoices()
    End Sub

    Private Sub LoadInvoices()
        Try
            Using conn As New SqlConnection(DentistXDATA.GetConnection.ConnectionString)
                conn.Open()
                Dim statusDraft = If(Eng, "Draft", "مسودة")
                Dim statusIssued = If(Eng, "Issued", "صادرة")
                Dim statusPaid = If(Eng, "Paid", "مدفوعة")
                Dim statusPartial = If(Eng, "Partially Paid", "مدفوعة جزئيا")
                Dim statusCancelled = If(Eng, "Cancelled", "ملغاة")
                Dim query As String = "
                    SELECT 
                        i.InvoiceID,
                        i.InvoiceNumber,
                        i.InvoiceDate,
                        i.TotalAmount,
                        i.AmountPaid,
                        i.BalanceDue,
                        CASE i.InvoiceStatus
                            WHEN 0 THEN '" & statusDraft & "'
                            WHEN 1 THEN '" & statusIssued & "'
                            WHEN 2 THEN '" & statusPaid & "'
                            WHEN 3 THEN '" & statusPartial & "'
                            WHEN 4 THEN '" & statusCancelled & "'
                        END AS Status,
                        i.Notes
                    FROM Invoices i
                    WHERE i.PatientID = @PatientID
                    ORDER BY i.InvoiceDate DESC"

                Using cmd As New SqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@PatientID", patientID)
                    Using adapter As New SqlDataAdapter(cmd)
                        Dim dt As New DataTable()
                        adapter.Fill(dt)
                        dgvInvoices.DataSource = dt

                        ApplyInvoiceGridCaptions()

                        GridView1.FormatConditions.Clear()

                        Dim AddRule =
    Sub(statusText As String, backColor As Color)
        Dim rule As New GridFormatRule()
        rule.Column = GridView1.Columns("Status")

        Dim cond As New FormatConditionRuleValue()
        cond.Condition = FormatCondition.Equal
        cond.Value1 = statusText
        cond.Appearance.BackColor = backColor
        cond.Appearance.Options.UseBackColor = True

        rule.Rule = cond
        'rule.ApplyToRow = True   ' optional but recommended

        GridView1.FormatRules.Add(rule)

    End Sub

                        AddRule(statusIssued, Color.LightCoral)
                        AddRule(statusPaid, Color.LightGreen)
                        AddRule(statusPartial, Color.Khaki)
                        AddRule(statusCancelled, Color.LightGray)


                        'GridView1.FormatConditions.Clear()

                        '' Issued = 1 (Red)
                        'Dim fcIssued As New StyleFormatCondition()
                        'fcIssued.Column = GridView1.Columns("Status")
                        'fcIssued.Condition = FormatCondition.Equal
                        'fcIssued.Value1 = 1

                        '' Create appearance and set properties
                        'fcIssued.Appearance.BackColor = Color.LightCoral

                        'GridView1.FormatConditions.Add(fcIssued)

                        '' Paid = 2 (Green)
                        'Dim fcPaid As New StyleFormatCondition()
                        'fcPaid.Column = GridView1.Columns("Status")
                        'fcPaid.Condition = FormatCondition.Equal

                        'fcPaid.Value1 = 2

                        'fcPaid.Appearance.BackColor = Color.LightGreen

                        'GridView1.FormatConditions.Add(fcPaid)

                        '' Partially Paid = 3 (Yellow)
                        'Dim fcPartial As New StyleFormatCondition()
                        'fcPartial.Column = GridView1.Columns("Status")
                        'fcPartial.Condition = FormatCondition.Equal
                        'fcPartial.Value1 = 3

                        'fcPartial.Appearance.BackColor = Color.Khaki

                        'GridView1.FormatConditions.Add(fcPartial)
                    End Using
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error loading invoices: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' Also update the LoadInvoices method to work with DevExpress
    Private Sub LoadInvoicesDev()
        Try
            Using conn As New SqlConnection(DentistXDATA.GetConnection.ConnectionString)
                conn.Open()
                Dim statusDraft = If(Eng, "Draft", "مسودة")
                Dim statusIssued = If(Eng, "Issued", "صادرة")
                Dim statusPaid = If(Eng, "Paid", "مدفوعة")
                Dim statusPartial = If(Eng, "Partially Paid", "مدفوعة جزئيا")
                Dim statusCancelled = If(Eng, "Cancelled", "ملغاة")
                Dim query As String = "
                SELECT 
                    i.InvoiceID,
                    i.InvoiceNumber,
                    i.InvoiceDate,
                    i.TotalAmount,
                    i.AmountPaid,
                    i.BalanceDue,
                    CASE i.InvoiceStatus
                        WHEN 0 THEN '" & statusDraft & "'
                        WHEN 1 THEN '" & statusIssued & "'
                        WHEN 2 THEN '" & statusPaid & "'
                        WHEN 3 THEN '" & statusPartial & "'
                        WHEN 4 THEN '" & statusCancelled & "'
                    END AS Status,
                    i.Notes
                FROM Invoices i
                WHERE i.PatientID = @PatientID
                ORDER BY i.InvoiceDate DESC"

                Using cmd As New SqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@PatientID", patientID)
                    Using adapter As New SqlDataAdapter(cmd)
                        Dim dt As New DataTable()
                        adapter.Fill(dt)

                        ' Bind to DevExpress GridControl
                        dgvInvoices.DataSource = dt
                        GridView1.BestFitColumns()
                        ApplyInvoiceGridCaptions()

                        ' Format columns
                        GridView1.Columns("InvoiceDate").DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
                        GridView1.Columns("InvoiceDate").DisplayFormat.FormatString = "dd/MM/yyyy"

                        GridView1.Columns("TotalAmount").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                        GridView1.Columns("TotalAmount").DisplayFormat.FormatString = "c2"

                        GridView1.Columns("AmountPaid").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                        GridView1.Columns("AmountPaid").DisplayFormat.FormatString = "c2"

                        GridView1.Columns("BalanceDue").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                        GridView1.Columns("BalanceDue").DisplayFormat.FormatString = "c2"

                        ' Color code by status
                        GridView1.FormatConditions.Clear()

                        '' Issued = 1 (Red)
                        'Dim fcIssued As New StyleFormatCondition()
                        'fcIssued.Column = GridView1.Columns("InvoiceStatus")
                        'fcIssued.Condition = FormatCondition.Equal
                        'fcIssued.Value1 = 1
                        'fcIssued.Appearance.BackColor = Color.LightCoral
                        'GridView1.FormatConditions.Add(fcIssued)

                        '' Paid = 2 (Green)
                        'Dim fcPaid As New StyleFormatCondition()
                        'fcPaid.Column = GridView1.Columns("InvoiceStatus")
                        'fcPaid.Condition = FormatCondition.Equal
                        'fcPaid.Value1 = 2
                        'fcPaid.Appearance.BackColor = Color.LightGreen
                        'GridView1.FormatConditions.Add(fcPaid)

                        '' Partially Paid = 3 (Yellow)
                        'Dim fcPartial As New StyleFormatCondition()
                        'fcPartial.Column = GridView1.Columns("InvoiceStatus")
                        'fcPartial.Condition = FormatCondition.Equal
                        'fcPartial.Value1 = 3
                        'fcPartial.Appearance.BackColor = Color.Khaki
                        'GridView1.FormatConditions.Add(fcPartial)
                        GridView1.FormatConditions.Clear()

                        ' Create a single method to handle all conditions
                        Dim AddCondition = Sub(status As Integer, color As Color)
                                               Dim condition As New StyleFormatCondition()
                                               condition.Column = GridView1.Columns("InvoiceStatus")
                                               condition.Condition = FormatCondition.Equal
                                               condition.Value1 = status
                                               condition.Appearance.BackColor = color
                                               ' Uncomment to highlight entire row
                                               ' condition.ApplyToRow = True
                                               GridView1.FormatConditions.Add(condition)
                                           End Sub

                        ' Add all conditions
                        AddCondition(1, Color.LightCoral)
                        AddCondition(2, Color.LightGreen)
                        AddCondition(3, Color.Khaki)

                    End Using
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error loading invoices: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub ApplyInvoiceGridCaptions()
        If GridView1 Is Nothing OrElse GridView1.Columns Is Nothing Then Return

        If GridView1.Columns("InvoiceID") IsNot Nothing Then
            GridView1.Columns("InvoiceID").Caption = If(Eng, "Invoice ID", "معرف الفاتورة")
        End If
        If GridView1.Columns("InvoiceNumber") IsNot Nothing Then
            GridView1.Columns("InvoiceNumber").Caption = If(Eng, "Invoice Number", "رقم الفاتورة")
        End If
        If GridView1.Columns("InvoiceDate") IsNot Nothing Then
            GridView1.Columns("InvoiceDate").Caption = If(Eng, "Invoice Date", "تاريخ الفاتورة")
        End If
        If GridView1.Columns("TotalAmount") IsNot Nothing Then
            GridView1.Columns("TotalAmount").Caption = If(Eng, "Total Amount", "الإجمالي")
        End If
        If GridView1.Columns("AmountPaid") IsNot Nothing Then
            GridView1.Columns("AmountPaid").Caption = If(Eng, "Amount Paid", "المدفوع")
        End If
        If GridView1.Columns("BalanceDue") IsNot Nothing Then
            GridView1.Columns("BalanceDue").Caption = If(Eng, "Balance Due", "المتبقي")
        End If
        If GridView1.Columns("Status") IsNot Nothing Then
            GridView1.Columns("Status").Caption = If(Eng, "Status", "الحالة")
        End If
        If GridView1.Columns("Notes") IsNot Nothing Then
            GridView1.Columns("Notes").Caption = If(Eng, "Notes", "ملاحظات")
        End If
    End Sub

    Private Sub btnViewDetails_Click(sender As Object, e As EventArgs) Handles btnViewDetails.Click
        'If dgvInvoices.CurrentRow IsNot Nothing Then
        '    Dim invoiceID As Integer = CInt(dgvInvoices.CurrentRow.Cells("InvoiceID").Value)
        '    ' Show invoice details form
        '    Dim detailsForm As New frmInvoiceDetails(invoiceID)
        '    detailsForm.ShowDialog()
        'End If
        ' DevExpress GridControl version
        If GridView1.FocusedRowHandle >= 0 Then
            ' Get the focused row
            Dim row As DataRow = GridView1.GetDataRow(GridView1.FocusedRowHandle)
            If row IsNot Nothing Then
                Dim invoiceID As Integer = CInt(row("InvoiceID"))
                Dim detailsForm As New frmInvoiceDetails(invoiceID)
                detailsForm.ShowDialog()
            End If
        Else
            MessageBox.Show("Please select an invoice first.", "Information",
                           MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub

    ' Alternative: Double-click to view details
    Private Sub GridView1_DoubleClick(sender As Object, e As EventArgs) Handles GridView1.DoubleClick
        btnViewDetails_Click(sender, e)
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
End Class