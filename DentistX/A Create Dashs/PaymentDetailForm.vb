Imports System.Data.SqlClient
Imports DevExpress.Utils.Extensions
Imports DevExpress.XtraEditors
Imports DevExpress.XtraLayout
Public Class PaymentDetailForm

    Private _payment As PatientPayment
    Private ReadOnly _connectionString As String = DashDataModel.DatabaseHelper._connectionString

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Public Sub New(payment As PatientPayment)
        InitializeComponent()
        InitializeComponent1()
        _payment = payment
        LoadPaymentDetails()
    End Sub

    Private Sub InitializeComponent2()
        Me.Text = If(eng, "Payment Details", "تفاصيل الدفع")
        Me.StartPosition = FormStartPosition.CenterParent
        Me.Size = New Size(500, 400)
        Me.FormBorderStyle = FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False

        ' Create layout
        Dim layoutControl As New DevExpress.XtraLayout.LayoutControl()
        layoutControl.Dock = DockStyle.Fill
        Me.Controls.Add(layoutControl)

        ' Add controls for payment details
        Dim txtPatient As New DevExpress.XtraEditors.TextEdit() With {
            .ReadOnly = True
        }

        Dim txtAmount As New DevExpress.XtraEditors.TextEdit() With {
             .ReadOnly = True
        }

        Dim txtDate As New DevExpress.XtraEditors.DateEdit() With {
             .ReadOnly = True
        }

        Dim txtType As New DevExpress.XtraEditors.TextEdit() With {
             .ReadOnly = True
        }

        Dim txtNotes As New DevExpress.XtraEditors.MemoEdit() With {
             .ReadOnly = True,
            .Height = 100
        }

        ' Add to layout
        layoutControl.AddItem(If(eng, "Patient:", "المريض:"), txtPatient)
        layoutControl.AddItem(If(eng, "Amount:", "المبلغ:"), txtAmount)
        layoutControl.AddItem(If(eng, "Date:", "التاريخ:"), txtDate)
        layoutControl.AddItem(If(eng, "Payment Type:", "نوع الدفع:"), txtType)
        layoutControl.AddItem(If(eng, "Notes:", "ملاحظات:"), txtNotes)

        ' Close button
        Dim btnClose As New DevExpress.XtraEditors.SimpleButton() With {
            .Text = If(eng, "Close", "إغلاق"),
            .DialogResult = DialogResult.OK
        }
        layoutControl.AddControl(btnClose)
    End Sub

    Private Sub LoadPaymentDetails()
        If _payment Is Nothing Then Return

        Try
            ' Load payment details into form controls
            txtPatient.Text = _payment.PatientName
            txtAmount.Text = _payment.PayValue.ToString("C2")
            txtDate.DateTime = _payment.PayDate
            txtType.Text = _payment.PayType

            ' Handle optional fields
            If Not String.IsNullOrEmpty(_payment.Notes) Then
                txtNotes.Text = _payment.Notes
            Else
                txtNotes.Text = If(eng, "No notes available.", "لا توجد ملاحظات.")
            End If

            ' Load additional payment details if available
            LoadExtendedPaymentDetails()

            ' Set form title
            Me.Text = If(eng,
                         $"Payment Details - {_payment.PatientName} - {_payment.PayDate:dd/MM/yyyy}",
                         $"تفاصيل الدفع - {_payment.PatientName} - {_payment.PayDate:dd/MM/yyyy}")

        Catch ex As Exception
            XtraMessageBox.Show(If(eng,
                                   $"Error loading payment details: {ex.Message}",
                                   $"خطأ أثناء تحميل تفاصيل الدفع: {ex.Message}"),
                                If(eng, "Error", "خطأ"),
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub LoadExtendedPaymentDetails()
        ' Load additional payment information from database
        Using conn As New SqlConnection(_connectionString)
            conn.Open()

            Dim sql As String = "
            SELECT 
                pp.*,
                pt.Detail as TreatmentDetail,
                pt.TrtValue as TreatmentValue,
                (SELECT ISNULL(SUM(PayValue), 0) 
                 FROM Patient_Pays WHERE TrtID = pp.TrtID) as TotalPaid,
                pt.TrtValue - (SELECT ISNULL(SUM(PayValue), 0) 
                               FROM Patient_Pays WHERE TrtID = pp.TrtID) as Balance,
                p.Phone,
                p.Address
            FROM Patient_Pays pp
            LEFT JOIN Patient_Trts pt ON pp.TrtID = pt.TrtID
            LEFT JOIN Patient p ON pp.PatientID = p.PatientID
            WHERE pp.PayID = @PayID"

            Using cmd As New SqlCommand(sql, conn)
                cmd.Parameters.AddWithValue("@PayID", _payment.PayID)

                Using reader As SqlDataReader = cmd.ExecuteReader()
                    If reader.Read() Then
                        ' Load extended details
                        If reader("TreatmentDetail") IsNot DBNull.Value Then
                            lblTreatment.Text = $"Treatment: {reader("TreatmentDetail")}"
                        End If

                        If reader("TreatmentValue") IsNot DBNull.Value Then
                            lblTreatmentValue.Text = $"Treatment Value: {Convert.ToDecimal(reader("TreatmentValue")):C2}"
                        End If

                        If reader("TotalPaid") IsNot DBNull.Value Then
                            lblTotalPaid.Text = $"Total Paid: {Convert.ToDecimal(reader("TotalPaid")):C2}"
                        End If

                        If reader("Balance") IsNot DBNull.Value Then
                            Dim balance As Decimal = Convert.ToDecimal(reader("Balance"))
                            lblBalance.Text = $"Balance: {balance:C2}"

                            ' Color code based on balance
                            If balance > 0 Then
                                lblBalance.Appearance.ForeColor = Color.Red
                            Else
                                lblBalance.Appearance.ForeColor = Color.Green
                            End If
                        End If

                        ' Load check details if payment type is check
                        If _payment.PayType.ToUpper() = "CHECK" Then
                            LoadCheckDetails(reader)
                        End If

                        ' Load insurance details if applicable
                        If Not String.IsNullOrEmpty(_payment.InsuranceCompany) Then
                            LoadInsuranceDetails(reader)
                        End If
                    End If
                End Using
            End Using
        End Using
    End Sub

    Private Sub LoadCheckDetails(reader As SqlDataReader)
        ' Create check details group
        Dim groupCheck As New DevExpress.XtraEditors.GroupControl()
        groupCheck.Text = If(eng, "Check Details", "تفاصيل الشيك")
        groupCheck.Dock = DockStyle.Top
        groupCheck.Height = 120

        ' Create layout for check details
        Dim layoutCheck As New LayoutControl()
        layoutCheck.Dock = DockStyle.Fill

        Dim txtChqNumber As New TextEdit() With {.ReadOnly = True}
        Dim txtChqBank As New TextEdit() With {.ReadOnly = True}
        Dim txtChqDueDate As New DateEdit() With {.ReadOnly = True}
        Dim txtChqOwner As New TextEdit() With {.ReadOnly = True}

        ' Populate check details
        If reader("ChqNumber") IsNot DBNull.Value Then
            txtChqNumber.Text = reader("ChqNumber").ToString()
        End If

        If reader("ChqBank") IsNot DBNull.Value Then
            txtChqBank.Text = reader("ChqBank").ToString()
        End If

        If reader("ChqDueDate") IsNot DBNull.Value Then
            txtChqDueDate.DateTime = Convert.ToDateTime(reader("ChqDueDate"))
        End If

        If reader("ChqOwner") IsNot DBNull.Value Then
            txtChqOwner.Text = reader("ChqOwner").ToString()
        End If

        ' Add to layout
        layoutCheck.AddItem(If(eng, "Check Number:", "رقم الشيك:"), txtChqNumber)
        layoutCheck.AddItem(If(eng, "Bank:", "البنك:"), txtChqBank)
        layoutCheck.AddItem(If(eng, "Due Date:", "تاريخ الاستحقاق:"), txtChqDueDate)
        layoutCheck.AddItem(If(eng, "Check Owner:", "صاحب الشيك:"), txtChqOwner)

        groupCheck.Controls.Add(layoutCheck)

        ' Add to main panel
        panelMain.Controls.Add(groupCheck)
        groupCheck.BringToFront()
    End Sub

    Private Sub LoadInsuranceDetails(reader As SqlDataReader)
        ' Create insurance details group
        Dim groupInsurance As New DevExpress.XtraEditors.GroupControl()
        groupInsurance.Text = If(eng, "Insurance Details", "تفاصيل التأمين")
        groupInsurance.Dock = DockStyle.Top
        groupInsurance.Height = 100

        Dim layoutInsurance As New LayoutControl()
        layoutInsurance.Dock = DockStyle.Fill

        Dim txtInsuranceCo As New TextEdit() With {.ReadOnly = True}
        Dim txtInsuranceNotes As New MemoEdit() With {.ReadOnly = True}
        txtInsuranceNotes.Height = 60

        ' Populate insurance details
        If reader("InsuranceCompany") IsNot DBNull.Value Then
            txtInsuranceCo.Text = reader("InsuranceCompany").ToString()
        End If

        If reader("InsuranceNotes") IsNot DBNull.Value Then
            txtInsuranceNotes.Text = reader("InsuranceNotes").ToString()
        End If

        ' Add to layout
        layoutInsurance.AddItem(If(eng, "Insurance Company:", "شركة التأمين:"), txtInsuranceCo)
        layoutInsurance.AddItem(If(eng, "Notes:", "ملاحظات:"), txtInsuranceNotes)

        groupInsurance.Controls.Add(layoutInsurance)

        ' Add to main panel
        panelMain.Controls.Add(groupInsurance)
        groupInsurance.BringToFront()
    End Sub

    ' Also need to add these controls to the form
    Friend WithEvents panelMain As PanelControl
    Friend WithEvents lblTreatment As LabelControl
    Friend WithEvents lblTreatmentValue As LabelControl
    Friend WithEvents lblTotalPaid As LabelControl
    Friend WithEvents lblBalance As LabelControl
    Friend WithEvents txtPatient As New TextEdit 'txtType
    Friend WithEvents txtType As New TextEdit 'txtType
    Friend WithEvents txtAmount As New TextEdit
    Friend WithEvents txtDate As New DateEdit
    Friend WithEvents txtNotes As New MemoEdit
    ' Update InitializeComponent to include these controls
    Private Sub InitializeComponent1()
        Me.Text = If(eng, "Payment Details", "تفاصيل الدفع")
        Me.StartPosition = FormStartPosition.CenterParent
        Me.Size = New Size(600, 500)
        Me.FormBorderStyle = FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False

        ' Create main panel
        panelMain = New PanelControl()
        panelMain.Dock = DockStyle.Fill
        panelMain.Padding = New Padding(10)
        Me.Controls.Add(panelMain)

        ' Create scrollable container
        Dim scrollable As New ScrollableControl()
        scrollable.Dock = DockStyle.Fill
        scrollable.AutoScroll = True
        panelMain.Controls.Add(scrollable)

        ' Create main layout
        Dim layoutMain As New LayoutControl()
        layoutMain.Dock = DockStyle.Fill
        scrollable.Controls.Add(layoutMain)

        ' Create basic payment fields
        txtPatient = New TextEdit() With {.ReadOnly = True}
        txtAmount = New TextEdit() With {.ReadOnly = True}
        txtDate = New DateEdit() With {.ReadOnly = True}
        txtType = New TextEdit() With {.ReadOnly = True}
        txtNotes = New MemoEdit() With {
            .ReadOnly = True,
            .Height = 80
        }

        ' Create labels for extended info
        lblTreatment = New LabelControl() With {.Text = If(eng, "Treatment:", "العلاج:")}
        lblTreatmentValue = New LabelControl() With {.Text = If(eng, "Treatment Value:", "قيمة العلاج:")}
        lblTotalPaid = New LabelControl() With {.Text = If(eng, "Total Paid:", "إجمالي المدفوع:")}
        lblBalance = New LabelControl() With {.Text = If(eng, "Balance:", "الرصيد:")}

        ' Add to layout
        layoutMain.AddItem(If(eng, "Patient:", "المريض:"), txtPatient)
        layoutMain.AddItem(If(eng, "Amount:", "المبلغ:"), txtAmount)
        layoutMain.AddItem(If(eng, "Date:", "التاريخ:"), txtDate)
        layoutMain.AddItem(If(eng, "Payment Type:", "نوع الدفع:"), txtType)
        layoutMain.AddItem(If(eng, "Notes:", "ملاحظات:"), txtNotes)
        layoutMain.AddControl(lblTreatment)
        layoutMain.AddControl(lblTreatmentValue)
        layoutMain.AddControl(lblTotalPaid)
        layoutMain.AddControl(lblBalance)

        ' Close button
        Dim btnClose As New SimpleButton() With {
            .Text = If(eng, "Close", "إغلاق"),
            .DialogResult = DialogResult.OK,
            .Location = New Point(250, 400)
        }
        AddHandler btnClose.Click, AddressOf BtnClose_Click
        layoutMain.AddControl(btnClose)
    End Sub

    Private Sub BtnClose_Click(sender As Object, e As EventArgs)
        Me.Close()
    End Sub



End Class
