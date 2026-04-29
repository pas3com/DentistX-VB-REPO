Imports System.Data.SqlClient
Imports Dapper

Public Class FrmAddPayAccnt
    Private _patientId As Integer
    Private _trtId As Integer
    Private _treatDetail As String
    Private _pay As Patient_Pays

    Public Property Treats As Patient_Trts

    Private Sub FrmAddPayAccnt_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        IntegerMoneyEditorFocus.ConfigureIntegerMoneyTextEdit(PayValue)
        IntegerMoneyEditorFocus.AttachTextEditZeroEmptyElseSelectAll(PayValue)
    End Sub
    ''' <summary>Load form for adding a new Cash payment for the given treatment. Call before ShowDialog.</summary>
    Public Sub LoadForAdd(patientId As Integer, trtId As Integer, treatDetail As String)
        _patientId = patientId
        _trtId = trtId
        _treatDetail = If(String.IsNullOrWhiteSpace(treatDetail), "-", treatDetail)
        _pay = Nothing
        lblTreat.Text = If(Eng, "For Treat: ", "للمعالجة: ") & _treatDetail
        PayDate.EditValue = Date.Today
        PayValue.EditValue = 0D
        NotesText.Text = ""
        If txtReceivedBy IsNot Nothing Then txtReceivedBy.Text = ""
        btnAddTrt.Visible = True
        btnSave.Visible = False
        btnAddTrt.Text = If(Eng, "Add", "إضافة")
        btnCancel.Text = If(Eng, "Cancel", "إلغاء")
        Me.Text = If(Eng, "Add Cash Payment", "إضافة دفعة نقدية")
    End Sub

    ''' <summary>Load the payment record for editing. Call before ShowDialog. treatDetail is shown in lblTreat.</summary>
    Public Sub LoadForEdit(pay As Patient_Pays, treatDetail As String)
        If pay Is Nothing Then Return
        _pay = pay
        _patientId = If(pay.PatientID.HasValue, pay.PatientID.Value, 0)
        _trtId = pay.TrtID
        _treatDetail = If(String.IsNullOrWhiteSpace(treatDetail), "-", treatDetail)
        lblTreat.Text = If(Eng, "For Treat: ", "للمعالجة: ") & _treatDetail
        PayDate.EditValue = pay.PayDate
        PayValue.EditValue = pay.PayValue
        NotesText.Text = If(pay.Notes, "")
        If txtReceivedBy IsNot Nothing Then txtReceivedBy.Text = If(pay.ReceivedBy, "")
        btnAddTrt.Visible = False
        btnSave.Visible = True
        btnSave.Text = If(Eng, "Save", "حفظ")
        btnCancel.Text = If(Eng, "Cancel", "إلغاء")
        Me.Text = If(Eng, "Edit Payment", "تعديل الدفعة")
    End Sub

    Private Sub btnAddTrt_Click(sender As Object, e As EventArgs) Handles btnAddTrt.Click
        If PayDate.EditValue Is Nothing OrElse String.IsNullOrWhiteSpace(PayDate.Text) Then
            MsgBox(If(Eng, "Payment Date is required.", "التاريخ مطلوب."))
            Return
        End If
        Dim payVal As Decimal = IntegerMoneyEditorFocus.DecimalFromIntegerMoneyEdit(PayValue)
        If payVal <= 0 Then
            MsgBox(If(Eng, "Pay Value must be greater than 0.", "قيمة الدفعة يجب أن تكون أكبر من صفر."))
            Return
        End If
        Dim Notes As String = Me.NotesText.Text
        Dim PayDat As Date = Me.PayDate.EditValue

        'Try
        '    Using conn As New SqlConnection(DentistXDATA.GetConnection.ConnectionString)
        '        conn.Open()
        '        conn.Execute(
        '            "INSERT INTO Patient_Pays (TrtID, PatientID, PayValue, PayDate, Notes, PayType) VALUES (@TrtID, @PatientID, @PayValue, @PayDate, @Notes, @PayType)",
        '            New With {
        '                .TrtID = _trtId,
        '                .PatientID = _patientId,
        '                .PayValue = payVal,
        '                .PayDate = payDat,
        '                .Notes = notes,
        '                .PayType = "Cash"
        '            })
        '    End Using
        '    Me.DialogResult = DialogResult.OK
        '    Me.Close()
        'Catch ex As Exception
        '    MsgBox(If(Eng, "Error adding payment: ", "خطأ عند إضافة الدفعة: ") & ex.Message)
        'End Try

        Try
            If String.IsNullOrWhiteSpace(Me.PayDate.Text) Then
                MsgBox(If(Eng, "Payment Date is Empty", "التاريخ فارغ"))
                Exit Sub
            End If



            Using conn As SqlConnection = DentistXDATA.GetConnection
                conn.Open()
                ' Start the transaction
                Using trans As SqlTransaction = conn.BeginTransaction()
                    Try
                        ' Check for existing treatments (within transaction)
                        Dim trtID As Integer

                        If Treats IsNot Nothing Then
                            trtID = Treats.TrtID
                        Else
                            ' Payment in advance: placeholder tooth/treatment rows (no treatment row selected)
                            Dim detail = If(Eng, "Payment In Advance " & Me.NotesText.Text, "دفعة مقدما " & Me.NotesText.Text)
                            Dim Treat As String = detail
                            Dim TreatDate As Date = PayDat
                            Dim toothTrtID As Integer

                            conn.Execute("INSERT INTO Patient_ToothTrt (PatientID, Treat, TreatDate) VALUES (@PatientID, @Treat, @TreatDate)",
                                                                                    New With {.PatientId = _patientId, .Treat = Treat, .TreatDate = TreatDate},
                                                                                    trans)
                            toothTrtID = conn.ExecuteScalar(Of Integer)("SELECT TOP 1 ToothTrtID FROM Patient_ToothTrt WHERE PatientID = @PatientID ORDER BY ToothTrtID DESC",
                                                                                        New With {.PatientId = _patientId}, trans)

                            conn.Execute("INSERT INTO Patient_Trts (PatientID, ToothTrtID, Detail, TrtDate, TrtValue, Discount2) VALUES (@PatientID, @ToothTrtID, @Detail, @TrtDate, 0, 0)",
                                                                                    New With {.PatientId = _patientId, .ToothTrtID = toothTrtID, .Detail = detail, .TrtDate = PayDat},
                                                                                    trans)

                            trtID = conn.ExecuteScalar(Of Integer)("SELECT TOP 1 TrtID FROM Patient_Trts WHERE PatientID = @PatientID ORDER BY TrtID DESC",
                                                                                    New With {.PatientId = _patientId},
                                                                                    trans)
                        End If



                        ' Insert payment (within transaction)
                        Dim recvBy As String = If(txtReceivedBy Is Nothing OrElse String.IsNullOrWhiteSpace(txtReceivedBy.Text), Nothing, txtReceivedBy.Text.Trim())
                        conn.Execute("INSERT INTO Patient_Pays (TrtID, PatientID, PayValue, PayDate, Notes, PayType, ReceivedBy, IsReturned) VALUES 
                                                                                        (@TrtID, @PatientID, @PayValue, @PayDate, @Notes, @PayType, @ReceivedBy, @IsReturned)",
                                      New With {.TrtID = trtID, .PatientID = _patientId, .PayValue = payVal,
                                                .PayDate = PayDat, .Notes = Notes, .PayType = "Cash", .ReceivedBy = recvBy, .IsReturned = False}, trans)

                        ' Commit if everything succeeds
                        trans.Commit()

                    Catch ex As Exception
                        ' Rollback if any operation fails
                        trans.Rollback()
                        ' Re-throw the exception to handle it at a higher level
                        Throw
                    End Try
                End Using ' Transaction will be disposed here
            End Using ' Connection will be disposed here


            FormManager.Instance.CurrentForm.UpdatePatientBalance(CurrentPatient)

            Me.DialogResult = DialogResult.OK
            Me.Close()
        Catch ex As SqlException
            MsgBox(ex.Message)
        End Try


    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If _pay Is Nothing Then Return
        If PayDate.EditValue Is Nothing OrElse String.IsNullOrWhiteSpace(PayDate.Text) Then
            MsgBox(If(Eng, "Payment Date is required.", "التاريخ مطلوب."))
            Return
        End If
        Dim payVal As Decimal = IntegerMoneyEditorFocus.DecimalFromIntegerMoneyEdit(PayValue)
        If payVal <= 0 Then
            MsgBox(If(Eng, "Pay Value must be greater than 0.", "قيمة الدفعة يجب أن تكون أكبر من صفر."))
            Return
        End If
        Dim payDat As Date = CType(PayDate.EditValue, DateTime)
        Dim notes As String = If(NotesText.Text, "").Trim()
        Dim recvBy As String = If(txtReceivedBy Is Nothing OrElse String.IsNullOrWhiteSpace(txtReceivedBy.Text), Nothing, txtReceivedBy.Text.Trim())

        Try
            Using conn As New SqlConnection(DentistXDATA.GetConnection.ConnectionString)
                conn.Open()
                conn.Execute(
                    "UPDATE Patient_Pays SET PayValue = @PayValue, PayDate = @PayDate, Notes = @Notes, ReceivedBy = @ReceivedBy, IsReturned = @IsReturned WHERE PayID = @PayID",
                    New With {.PayValue = payVal, .PayDate = payDat, .Notes = notes, .ReceivedBy = recvBy, .IsReturned = False, .PayID = _pay.PayID})
            End Using
            _pay.PayValue = payVal
            _pay.PayDate = payDat
            _pay.Notes = notes
            _pay.ReceivedBy = recvBy
            _pay.IsReturned = False
            Me.DialogResult = DialogResult.OK
            Me.Close()
        Catch ex As Exception
            MsgBox(If(Eng, "Error saving payment: ", "خطأ عند حفظ الدفعة: ") & ex.Message)
        End Try
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.DialogResult = DialogResult.Cancel
        Me.Close()
    End Sub


End Class
