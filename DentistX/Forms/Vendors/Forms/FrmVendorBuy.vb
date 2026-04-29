Imports System.Data.SqlClient
Imports Dapper

Public Class FrmVendorBuy


    Private clsVendors As New Vendors
    Private clsVendorsData As New VendorsDATA
    Private clsVendorBuys As New VendorSales
    Private clsVendorBuysData As New VendorSalesDATA
    Private clsVendorPays As New VendorPays
    Private clsVendorPaysData As New VendorPaysDATA

    Private Sub FrmVendorBuy_Load(sender As Object, e As EventArgs) Handles Me.Load
        SetDataSources()
        IntegerMoneyGridColumns.ApplyIntegerPayTrtEditors(VendorBuysGrid)
        IntegerMoneyGridColumns.ApplyIntegerPayTrtEditors(VendorPaysGrid)
        IntegerMoneyGridColumns.ApplyIntegerPayTrtEditors(ChqsPayGrid)
    End Sub

    Private Sub SetDataSources()
        VendorsBS.DataSource = clsVendorsData.SelectAll.ToList
        VendorBuysBS.DataSource = clsVendorBuysData.SelectAll.ToList
        If showAsChildChck.Checked Then
            ' Set up parent-child relationship
            If VendorBuysBS.Current IsNot Nothing Then
                Dim currentBuy As VendorSales = CType(VendorBuysBS.Current, VendorSales)
                Dim vendorPays = Conn.Query(Of VendorPays)("Select [PayID],[SalesID],[VendID],[PayValue],[PayDate],[Notes],[PayType],[ChqOwner]
                                                              ,[AccountNumber],[ChqNumber],[ChqDueDate],[ChqBank],[IsCashed],[IsForward]
                                                            FROM [dbo].[VendorPays] 
                                                            WHERE VendID = @VendID AND SalesID = @SalesID",
                                                              New With {.VendID = currentBuy.VendID, .SalesID = currentBuy.SalesID})
                VendorPaysBS.DataSource = vendorPays.ToList()
            End If
        Else
            'VendorBuysBS.DataSource = clsVendorBuysData.SelectAll.ToList
            ' Load all payments
            VendorPaysBS.DataSource = clsVendorPaysData.SelectAll.ToList
        End If

        LoadFutureUncashedPayments(cboUncashedChqs)
        FillCbos()
        If VendorsBS.Count > 0 Then
            CboVendor.SelectedIndex = 0
        End If
    End Sub

    Dim currentVendor As Vendors
    Private Sub VendorsBS_CurrentChanged(sender As Object, e As EventArgs) Handles VendorsBS.CurrentChanged
        If VendorsBS.Count = 0 Then Exit Sub
        currentVendor = CType(VendorsBS.Current, Vendors)
        FillVendorLBLs(currentVendor)
    End Sub

    Dim currentBuy As VendorSales
    Private Sub VendorBuysBS_CurrentChanged(sender As Object, e As EventArgs) Handles VendorBuysBS.CurrentChanged
        If VendorBuysBS.Count = 0 Then Exit Sub
        Try
            If VendorBuysBS.Current IsNot Nothing Then
                currentBuy = CType(VendorBuysBS.Current, VendorSales)
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Dim currentPay As VendorPays
    Private Sub VendorPaysBS_CurrentChanged(sender As Object, e As EventArgs) Handles VendorPaysBS.CurrentChanged
        If VendorPaysBS.Count = 0 Then Exit Sub
        Try
            If VendorPaysBS.Current IsNot Nothing Then
                currentPay = CType(VendorPaysBS.Current, VendorPays)
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub FillCbos()
        Dim vendorsList As List(Of Vendors) = clsVendorsData.SelectAll.ToList
        CboVendor.Properties.Items.Clear()
        For Each vend In vendorsList
            Dim displayText As String = $"{vend.VendName}" '   {vend.VendID}"
            'CboVendor.Properties.Items.Add(displayText)
            CboVendor.Properties.Items.Add(New DisplayPayItem With {
                                                                 .TableID = vend.VendID,
                                                                 .DisplayText = displayText
                                                                 })
        Next

        Dim unpaidList As List(Of UnpaidVendorSale) = VendorSalesDATA.GetUnpaidBuys
        cboUnPaid.Properties.Items.Clear()
        For Each unpaid In unpaidList
            Dim displayText As String = $"[{unpaid.SalesID}]_[{unpaid.VendName}]_[{unpaid.SalesDate}]_[{unpaid.SalesValue}]_[{unpaid.TotalPaid}]"
            'CboVendor.Properties.Items.Add(displayText)
            cboUnPaid.Properties.Items.Add(New DisplayPayItem With {
                                                                .TableID = unpaid.VendID,
                                                                .DisplayText = displayText
                                                                })
        Next
    End Sub

    Private Sub FillVendorLBLs(ByVal clsVend As Vendors)
        VendIdLbl.Text = clsVend.VendID
        VenNameLbl.Text = clsVend.VendName
        VendContactLbl.Text = clsVend.Contacts
        VenAdresLbl.Text = clsVend.VendAddress
        VendBalance.Text = $"{VendorSalesDATA.GetVendorBalance(clsVend.VendID):0}"
        VenTotalPays.Text = $"{VendorSalesDATA.GetTotalPays(clsVend.VendID):0}"
        VendTotalBuys.Text = $"{VendorSalesDATA.GetTotalBuys(clsVend.VendID):0}"
        If Val(VendBalance.Text) < 0 Then
            VendBalance.ForeColor = Color.Red
        Else
            VendBalance.ForeColor = Color.Green
        End If
    End Sub

    Private Sub FillChqInfo(ByVal clsPay As Patient_Pays)
        txtChqBank.Text = clsPay.ChqBank
        txtAccountNumber.Text = clsPay.AccountNumber
        txtChqNumber.Text = clsPay.ChqNumber
        dtChqDueDate.Text = clsPay.ChqDueDate
        txtChqOwner.Text = clsPay.ChqOwner
        txtChqValue.Text = clsPay.PayValue

    End Sub
    Public Sub LoadFutureUncashedPayments(cbo As DevExpress.XtraEditors.ComboBoxEdit)
        Try

            Dim sql As String = "
                        SELECT 
                            p.[PayID], p.[TrtID], p.[PatientID], p.[PayValue], p.[PayDate], p.[Notes], p.[PayType], 
                            p.[ChqOwner], p.[AccountNumber], p.[ChqNumber], p.[ChqDueDate], p.[ChqBank], 
                            p.[IsCashed], p.[InsuranceCompany], p.[InsuranceNotes], p.[IsForward],
                            pt.[PatientName]
                        FROM 
                            [dbo].[Patient_Pays] p
                        JOIN 
                            [dbo].[Patient] pt ON p.PatientID = pt.PatientID
                        WHERE 
                            p.IsCashed = 0 AND p.IsForward=0  AND (p.ForwardFromTo IS NULL OR p.ForwardFromTo <> 'PERSONAL') AND p.ChqDueDate > @Today  "

            Using conn As SqlConnection = DentistXDATA.GetConnection
                Dim list As List(Of Patient_Pays) = conn.Query(Of Patient_Pays)(sql, New With {.Today = Date.Now}).ToList()
                'VendorBuysBS.DataSource = list
                cbo.Properties.Items.Clear()

                For Each pay In list

                    Dim dateOnly As String = pay.PayDate.ToString("yyyy-MM-dd")
                    Dim text As String = $"[{pay.PayID}] [{pay.PatientName}] [({dateOnly})] [{pay.ChqNumber}] [{pay.PayValue:0}]"

                    cbo.Properties.Items.Add(New DisplayPayItem With {
                                                                                      .TableID = pay.PayID,
                                                                                      .DisplayText = text
                                                                                      })
                Next

            End Using
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Public Class DisplayPayItem
        Public Property TableID As Integer
        Public Property DisplayText As String
        Public Overrides Function ToString() As String
            Return DisplayText
        End Function
    End Class

    Private VendID As Integer

    Private Sub CboVendor_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CboVendor.SelectedIndexChanged
        Dim selected = TryCast(CboVendor.SelectedItem, DisplayPayItem)
        If selected IsNot Nothing Then
            VendID = selected.TableID

            ' Set VendorsBS.Current to the selected vendor by VendID
            For i As Integer = 0 To VendorsBS.Count - 1
                Dim vend = TryCast(VendorsBS(i), Vendors)
                If vend IsNot Nothing AndAlso vend.VendID = VendID Then
                    VendorsBS.Position = i
                    Exit For
                End If
            Next
        End If
    End Sub


    Private Sub btnAddvendor_Click(sender As Object, e As EventArgs) Handles btnAddvendor.Click
        Dim FR As New FrmVendors
        If FR.ShowDialog(Me) = DialogResult.OK Then
            VendorsBS.DataSource = clsVendorsData.SelectAll.ToList
        End If
    End Sub

    Private SalesID As Integer

    Private Sub cboUnPaid_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboUnPaid.SelectedIndexChanged
        If cboUnPaid.SelectedIndex < 0 Then
            btnAllBuys.Enabled = False
        Else
            btnAllBuys.Enabled = True
        End If
        Dim selected = TryCast(cboUnPaid.SelectedItem, DisplayPayItem)
        If selected IsNot Nothing Then
            SalesID = selected.TableID
        End If
        lblSalesID.Text = SalesID
    End Sub

    Private Sub btnAddBuy_Click(sender As Object, e As EventArgs) Handles btnAddBuy.Click
        Try
            If txtBuyDetail.Text.Length = 0 Then
                MsgBox("Enter Buy Details")
                Exit Sub
            End If
            If BuyDate.Text.Length < 10 Then
                MsgBox("Enter Buy Date")
                Exit Sub
            End If
            If Val(BuyValue.Text) <= 0 Then
                MsgBox("Enter Buy Correct value")
                Exit Sub
            End If
            clsVendorBuys = New VendorSales With {.Detail = txtBuyDetail.Text,
                                                    .SalesDate = BuyDate.DateTime,
                                                    .SalesValue = Val(BuyValue.Text),
                                                    .VendID = VendID}

            If clsVendorBuysData.Add(clsVendorBuys) Then
                MsgBox("Buy Added Successfuly")
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub showAsChildChck_CheckedChanged(sender As Object, e As EventArgs) Handles showAsChildChck.CheckedChanged
        If VendorBuysBS.Count = 0 Then Exit Sub
        If showAsChildChck.Checked Then
            ' Set up parent-child relationship
            If VendorBuysBS.Current IsNot Nothing Then
                Dim currentBuy As VendorSales = CType(VendorBuysBS.Current, VendorSales)
                Dim vendorPays = Conn.Query(Of VendorPays)("Select [PayID],[SalesID],[VendID],[PayValue],[PayDate],[Notes],[PayType],[ChqOwner]
                                                              ,[AccountNumber],[ChqNumber],[ChqDueDate],[ChqBank],[IsCashed],[IsForward]
                                                            FROM [dbo].[VendorPays] 
                                                            WHERE VendID = @VendID AND SalesID = @SalesID",
                                                              New With {.VendID = currentBuy.VendID, .SalesID = currentBuy.SalesID})
                VendorPaysBS.DataSource = vendorPays.ToList()
            End If
        Else
            ' Load all payments
            VendorPaysBS.DataSource = clsVendorPaysData.SelectAll.ToList
        End If
    End Sub

    Private Sub btnAllBuys_Click(sender As Object, e As EventArgs) Handles btnAllBuys.Click
        VendorBuysBS.DataSource = clsVendorBuysData.SelectAll.ToList
    End Sub

    Private Sub btnBuyDel_Click(sender As Object, e As EventArgs) Handles btnBuyDel.Click
        If VendorBuysBS.Count = 0 Then Exit Sub
        Dim currentBuy As VendorSales = CType(VendorBuysBS.Current, VendorSales)
        If clsVendorBuysData.Delete(currentBuy) Then
            MsgBox("Buy Was Deleted Successfuly")
        End If
    End Sub

    Private originalBuy As VendorSales

    Private Sub GridViewBuys_FocusedRowChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles GridViewBuys.FocusedRowChanged
        If TypeOf VendorBuysBS.Current Is VendorSales Then
            originalBuy = DirectCast(VendorBuysBS.Current, VendorSales).Clone()
        Else
            originalBuy = Nothing
        End If
    End Sub


    Private Sub btnBuySavNav_Click(sender As Object, e As EventArgs) Handles btnBuySavNav.Click
        If VendorBuysBS.Count = 0 Then Exit Sub

        Me.Validate()
        VendorBuysBS.EndEdit()

        Dim updatedBuy As VendorSales = TryCast(VendorBuysBS.Current, VendorSales)
        If updatedBuy Is Nothing OrElse originalBuy Is Nothing Then Exit Sub

        If clsVendorBuysData.Update(originalBuy, updatedBuy) Then
            MsgBox("Buy was updated successfully.")
            VendorBuysBS.DataSource = clsVendorBuysData.SelectAll
        End If
    End Sub

    Private Sub cboPayType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboPayType.SelectedIndexChanged
        If cboPayType.SelectedIndex = 0 Then
            chqCashTab.SelectedTabPage = CashPage
            GridTabControl.SelectedTabPage = CashGridPage
        ElseIf cboPayType.SelectedIndex = 1 Then
            chqCashTab.SelectedTabPage = ChqPage
            GridTabControl.SelectedTabPage = ChqGridPage
        End If
    End Sub

    Private PayID As Integer
    Private Sub cboUncashedChqs_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboUncashedChqs.SelectedIndexChanged
        Dim selected = TryCast(cboUncashedChqs.SelectedItem, DisplayPayItem)
        If selected IsNot Nothing Then
            PayID = selected.TableID
        End If
        lblPayID.Text = PayID
        Dim clsPatientPayData As New Patient_PaysDATA
        Dim clsPpays As Patient_Pays = clsPatientPayData.Select_Record(New Patient_Pays With {.PayID = PayID})
        FillChqInfo(clsPpays)
    End Sub

    Private Sub btnResetChqs_Click(sender As Object, e As EventArgs) Handles btnResetChqs.Click
        cboUncashedChqs.SelectedIndex = -1
        txtAccountNumber.ResetText()
        txtChqBank.ResetText()
        txtChqNumber.ResetText()
        txtChqOwner.ResetText()
        txtChqValue.ResetText()
        dtChqDueDate.ResetText()
        txtChqNotes.ResetText()
        PayDateChq.ResetText()
        PayValue.ResetText()
    End Sub


    Private Sub btnAddPay_Click(sender As Object, e As EventArgs) Handles btnAddPay.Click
        If Val(PayValue.Text) < 0 Then
            MsgBox("Enter Correct Pay Value")
            Exit Sub
        End If
        If PayDate.Text.Length < 10 Then
            MsgBox("Enter Correct Pay Date")
            Exit Sub
        End If


        Using conn As SqlConnection = DentistXDATA.GetConnection
            conn.Open()
            Using trans As SqlTransaction = conn.BeginTransaction()
                Try
                    VendorBuysBS.EndEdit()
                    VendorPaysBS.EndEdit()

                    clsVendorBuys = CType(VendorBuysBS.Current, VendorSales)
                    ' Cash
                    If cboPayType.SelectedIndex = 0 Then
                        clsVendorPays = New VendorPays With {
                                                            .Notes = txtPayNotes.Text,
                                                            .PayDate = PayDate.DateTime,
                                                            .PayType = cboPayType.Text,
                                                            .PayValue = Val(PayValue.Text),
                                                            .SalesID = clsVendorBuys.SalesID,
                                                            .VendID = clsVendorBuys.VendID
                                                        }

                        If clsVendorPaysData.Add(clsVendorPays, conn, trans) Then
                            trans.Commit()
                            MsgBox("Payment Added Successfully")
                            VendorPaysBS.DataSource = clsVendorPaysData.SelectAll()
                        Else
                            Throw New Exception("Insert failed.")
                        End If
                        ' Personal Cheque
                    ElseIf cboPayType.SelectedIndex = 1 Then
                        If cboUncashedChqs.SelectedIndex = -1 Then
                            clsVendorPays = New VendorPays With {
                                                            .AccountNumber = txtAccountNumber.Text,
                                                            .ChqBank = txtChqBank.Text,
                                                            .ChqDueDate = dtChqDueDate.DateTime,
                                                            .ChqNumber = txtChqNumber.Text,
                                                            .ChqOwner = txtChqOwner.Text,
                                                            .Notes = txtPayNotes.Text,
                                                            .IsCashed = chkIsCashed.Checked,
                                                            .PayDate = PayDate.DateTime,
                                                            .PayType = cboPayType.Text,
                                                            .PayValue = Val(PayValue.Text),
                                                            .SalesID = clsVendorBuys.SalesID,
                                                            .VendID = clsVendorBuys.VendID,
                                                            .IsForward = False,
                                                            .ForwardFromTo = "PERSONAL"
                                                        }
                            If clsVendorPaysData.Add(clsVendorPays, conn, trans) Then
                                trans.Commit()
                                MsgBox("Payment Added Successfully")
                                VendorPaysBS.DataSource = clsVendorPaysData.SelectAll()
                            Else
                                Throw New Exception("Insert failed.")
                            End If
                        Else
                            ' Forwarded (Patient Cheque)
                            Dim clsPatientPayData As New Patient_PaysDATA
                            Dim clsPpays As Patient_Pays = clsPatientPayData.Select_Record(New Patient_Pays With {.PayID = PayID}) ', conn, trans)
                            'clsVendors = clsVendorsData.Select_Record(New Vendors With {.VendID = VendID}) ', conn, trans)
                            Dim clsPatientData As New PatientDATA
                            Dim clsPatient As Patient = clsPatientData.Select_Record(New Patient With {.PatientID = clsPpays.PatientID}) ', conn, trans)
                            'clsVendors = clsVendorsData.Select_Record(New Vendors With {.VendID = VendID}) ', conn, trans)
                            Dim Forward As String = $"FROM ({clsPatient.PatientName}) TO ({currentVendor.VendName})"

                            clsVendorPays = New VendorPays With {
                                                            .AccountNumber = txtAccountNumber.Text,
                                                            .ChqBank = txtChqBank.Text,
                                                            .ChqDueDate = dtChqDueDate.DateTime,
                                                            .ChqNumber = txtChqNumber.Text,
                                                            .ChqOwner = txtChqOwner.Text,
                                                            .Notes = txtPayNotes.Text,
                                                            .IsCashed = chkIsCashed.Checked,
                                                            .PayDate = PayDate.DateTime,
                                                            .PayType = cboPayType.Text,
                                                            .PayValue = Val(PayValue.Text),
                                                            .SalesID = clsVendorBuys.SalesID,
                                                            .VendID = clsVendorBuys.VendID,
                                                            .IsForward = True,
                                                            .ForwardFromTo = Forward
                                                        }

                            If clsVendorPaysData.Add(clsVendorPays, conn, trans) Then
                                conn.Execute("UPDATE dbo.Patient_Pays SET IsForward = 1, [ForwardFromTo] = @ForwardFromTo WHERE PayID = @PayID",
                                                                         New With {.ForwardFromTo = Forward, .PayID = PayID}, transaction:=trans)
                                'Get the PayID for the added clsVendorPays
                                PayID = conn.ExecuteScalar("SELECT MAX(PayID) FROM dbo.VendorPays", transaction:=trans)
                                clsVendorPays.PayID = PayID
                                conn.Execute("UPDATE dbo.VendorPays SET IsForward = 1, [ForwardFromTo] = @ForwardFromTo WHERE PayID = @PayID",
                                                                         New With {.ForwardFromTo = Forward, .PayID = clsVendorPays.PayID}, transaction:=trans)

                                trans.Commit()
                                MsgBox("Forwarded Payment Added Successfully")
                                VendorPaysBS.DataSource = clsVendorPaysData.SelectAll()
                            Else
                                Throw New Exception("Insert failed.")
                            End If
                        End If
                    End If
                    LoadFutureUncashedPayments(cboUncashedChqs)
                    FillVendorLBLs(currentVendor)
                Catch ex As Exception
                    trans.Rollback()
                    MsgBox("Error: " & ex.Message)
                End Try
            End Using
        End Using

    End Sub

    Private Sub btnPayDel_Click(sender As Object, e As EventArgs) Handles btnPayDel.Click
        Try
            If VendorPaysBS.Count = 0 Then Exit Sub
            clsVendorPays = CType(VendorPaysBS.Current, VendorPays)
            If clsVendorPaysData.Delete(clsVendorPays) Then
                MsgBox("Payment Deleted Successfuly")
                VendorPaysBS.DataSource = clsVendorPaysData.SelectAll
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private originalPay As VendorPays

    Private Sub GridViewPays_FocusedRowChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles GridViewPays.FocusedRowChanged
        If VendorPaysBS.Current IsNot Nothing Then
            originalPay = DirectCast(VendorPaysBS.Current, VendorPays).Clone()
        End If
    End Sub
    Private Sub PaySavNav_Click(sender As Object, e As EventArgs) Handles PaySavNav.Click

        If VendorPaysBS.Count = 0 Then Exit Sub
        Try
            Me.Validate()
            VendorPaysBS.EndEdit()
            Dim updatedPay As VendorPays = TryCast(VendorBuysBS.Current, VendorPays)
            If updatedPay Is Nothing OrElse originalPay Is Nothing Then Exit Sub
            If clsVendorPaysData.Update(originalPay, updatedPay) Then
                MsgBox("Payment was updated successfully.")
                VendorPaysBS.DataSource = clsVendorPaysData.SelectAll
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub


#Region "Key Input"

    Private Sub txtChqValue_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtChqValue.KeyPress
        ' Allow control keys (Backspace, Delete, etc.)
        If Char.IsControl(e.KeyChar) Then
            Return
        End If

        ' Allow digits (0-9)
        If Char.IsDigit(e.KeyChar) Then
            Return
        End If

        ' Allow only ONE decimal point (for prices)
        If e.KeyChar = "."c AndAlso Not txtChqValue.Text.Contains(".") Then
            Return
        End If

        ' Block any other character
        e.Handled = True
    End Sub
    Private Sub txtChqValue_PreviewKeyDown(sender As Object, e As PreviewKeyDownEventArgs) Handles txtChqValue.PreviewKeyDown
        ' Allow Ctrl+V (paste) - We'll handle validation separately
        If e.Control AndAlso e.KeyCode = Keys.V Then
            Return
        End If

        ' Allow Backspace, Delete, Arrows, Tab
        If e.KeyCode = Keys.Back OrElse e.KeyCode = Keys.Delete OrElse
                                       e.KeyCode = Keys.Left OrElse e.KeyCode = Keys.Right OrElse
                                       e.KeyCode = Keys.Tab Then
            Return
        End If

        ' Allow numbers (0-9)
        If e.KeyCode >= Keys.D0 AndAlso e.KeyCode <= Keys.D9 Then
            Return
        End If

        ' Allow numpad numbers (0-9)
        If e.KeyCode >= Keys.NumPad0 AndAlso e.KeyCode <= Keys.NumPad9 Then
            Return
        End If

        ' Allow ONE decimal point (if needed)
        If e.KeyCode = Keys.Decimal AndAlso Not txtChqValue.Text.Contains(".") Then
            Return
        End If

        ' Block all other keys
        e.IsInputKey = False
    End Sub
    Private Sub txtChqValue_EditValueChanged(sender As Object, e As EventArgs) Handles txtChqValue.EditValueChanged
        ' Skip if empty
        If String.IsNullOrEmpty(txtChqValue.Text) Then Return

        ' Store cursor position
        Dim cursorPos = txtChqValue.SelectionStart

        ' Remove all non-numeric characters (except one decimal point)
        Dim cleanedText As New System.Text.StringBuilder()
        Dim hasDecimal As Boolean = False

        For Each c As Char In txtChqValue.Text
            If Char.IsDigit(c) Then
                cleanedText.Append(c)
            ElseIf c = "."c AndAlso Not hasDecimal Then
                cleanedText.Append(c)
                hasDecimal = True
            End If
        Next

        ' If text was modified (due to invalid pasted chars), update it
        If cleanedText.ToString() <> txtChqValue.Text Then
            txtChqValue.Text = cleanedText.ToString()
            ' Restore cursor position (adjusting for removed characters)
            txtChqValue.SelectionStart = Math.Min(cursorPos, txtChqValue.Text.Length)
        End If
        PayValue.Text = txtChqValue.Text
    End Sub


    Private Sub BuyValue_KeyPress(sender As Object, e As KeyPressEventArgs) Handles BuyValue.KeyPress
        ' Allow control keys (Backspace, Delete, etc.)
        If Char.IsControl(e.KeyChar) Then
            Return
        End If

        ' Allow digits (0-9)
        If Char.IsDigit(e.KeyChar) Then
            Return
        End If

        ' Allow only ONE decimal point (for prices)
        If e.KeyChar = "."c AndAlso Not BuyValue.Text.Contains(".") Then
            Return
        End If

        ' Block any other character
        e.Handled = True
    End Sub
    Private Sub BuyValue_PreviewKeyDown(sender As Object, e As PreviewKeyDownEventArgs) Handles BuyValue.PreviewKeyDown
        ' Allow Ctrl+V (paste) - We'll handle validation separately
        If e.Control AndAlso e.KeyCode = Keys.V Then
            Return
        End If

        ' Allow Backspace, Delete, Arrows, Tab
        If e.KeyCode = Keys.Back OrElse e.KeyCode = Keys.Delete OrElse
                                       e.KeyCode = Keys.Left OrElse e.KeyCode = Keys.Right OrElse
                                       e.KeyCode = Keys.Tab Then
            Return
        End If

        ' Allow numbers (0-9)
        If e.KeyCode >= Keys.D0 AndAlso e.KeyCode <= Keys.D9 Then
            Return
        End If

        ' Allow numpad numbers (0-9)
        If e.KeyCode >= Keys.NumPad0 AndAlso e.KeyCode <= Keys.NumPad9 Then
            Return
        End If

        ' Allow ONE decimal point (if needed)
        If e.KeyCode = Keys.Decimal AndAlso Not BuyValue.Text.Contains(".") Then
            Return
        End If

        ' Block all other keys
        e.IsInputKey = False
    End Sub
    Private Sub BuyValue_EditValueChanged(sender As Object, e As EventArgs) Handles BuyValue.EditValueChanged
        ' Skip if empty
        If String.IsNullOrEmpty(BuyValue.Text) Then Return

        ' Store cursor position
        Dim cursorPos = BuyValue.SelectionStart

        ' Remove all non-numeric characters (except one decimal point)
        Dim cleanedText As New System.Text.StringBuilder()
        Dim hasDecimal As Boolean = False

        For Each c As Char In BuyValue.Text
            If Char.IsDigit(c) Then
                cleanedText.Append(c)
            ElseIf c = "."c AndAlso Not hasDecimal Then
                cleanedText.Append(c)
                hasDecimal = True
            End If
        Next

        ' If text was modified (due to invalid pasted chars), update it
        If cleanedText.ToString() <> BuyValue.Text Then
            BuyValue.Text = cleanedText.ToString()
            ' Restore cursor position (adjusting for removed characters)
            BuyValue.SelectionStart = Math.Min(cursorPos, BuyValue.Text.Length)
        End If
    End Sub


    Private Sub PayValue_KeyPress(sender As Object, e As KeyPressEventArgs) Handles PayValue.KeyPress
        ' Allow control keys (Backspace, Delete, etc.)
        If Char.IsControl(e.KeyChar) Then
            Return
        End If

        ' Allow digits (0-9)
        If Char.IsDigit(e.KeyChar) Then
            Return
        End If

        ' Allow only ONE decimal point (for prices)
        If e.KeyChar = "."c AndAlso Not PayValue.Text.Contains(".") Then
            Return
        End If

        ' Block any other character
        e.Handled = True
    End Sub
    Private Sub PayValue_PreviewKeyDown(sender As Object, e As PreviewKeyDownEventArgs) Handles PayValue.PreviewKeyDown
        ' Allow Ctrl+V (paste) - We'll handle validation separately
        If e.Control AndAlso e.KeyCode = Keys.V Then
            Return
        End If

        ' Allow Backspace, Delete, Arrows, Tab
        If e.KeyCode = Keys.Back OrElse e.KeyCode = Keys.Delete OrElse
                                       e.KeyCode = Keys.Left OrElse e.KeyCode = Keys.Right OrElse
                                       e.KeyCode = Keys.Tab Then
            Return
        End If

        ' Allow numbers (0-9)
        If e.KeyCode >= Keys.D0 AndAlso e.KeyCode <= Keys.D9 Then
            Return
        End If

        ' Allow numpad numbers (0-9)
        If e.KeyCode >= Keys.NumPad0 AndAlso e.KeyCode <= Keys.NumPad9 Then
            Return
        End If

        ' Allow ONE decimal point (if needed)
        If e.KeyCode = Keys.Decimal AndAlso Not PayValue.Text.Contains(".") Then
            Return
        End If

        ' Block all other keys
        e.IsInputKey = False
    End Sub
    Private Sub PayValue_EditValueChanged(sender As Object, e As EventArgs) Handles PayValue.EditValueChanged
        ' Skip if empty
        If String.IsNullOrEmpty(PayValue.Text) Then Return

        ' Store cursor position
        Dim cursorPos = PayValue.SelectionStart

        ' Remove all non-numeric characters (except one decimal point)
        Dim cleanedText As New System.Text.StringBuilder()
        Dim hasDecimal As Boolean = False

        For Each c As Char In PayValue.Text
            If Char.IsDigit(c) Then
                cleanedText.Append(c)
            ElseIf c = "."c AndAlso Not hasDecimal Then
                cleanedText.Append(c)
                hasDecimal = True
            End If
        Next

        ' If text was modified (due to invalid pasted chars), update it
        If cleanedText.ToString() <> PayValue.Text Then
            PayValue.Text = cleanedText.ToString()
            ' Restore cursor position (adjusting for removed characters)
            PayValue.SelectionStart = Math.Min(cursorPos, PayValue.Text.Length)
        End If
    End Sub

    Private Sub PayDateChq_EditValueChanged(sender As Object, e As EventArgs) Handles PayDateChq.EditValueChanged
        PayDate.EditValue = PayDateChq.EditValue
    End Sub

    Private Sub txtChqNotes_EditValueChanged(sender As Object, e As EventArgs) Handles txtChqNotes.EditValueChanged
        txtPayNotes.Text = txtChqNotes.Text
    End Sub












    'Private Sub showAsChildChck_CheckedChanged(sender As Object, e As EventArgs) Handles showAsChildChck.CheckedChanged
    '    If CurrentPatient Is Nothing Then Exit Sub
    '    LoadDataDapper(CurrentPatient)
    'End Sub

    'Private Sub Patient_TrtsBindingSource_CurrentChanged(sender As Object, e As EventArgs) Handles Patient_TrtsBindingSource.CurrentChanged
    '    ' Load payments based on view mode
    '    If showAsChildChck.Checked Then
    '        ' Null check
    '        If Patient_TrtsBindingSource.Current Is Nothing Then
    '            Patient_PaysBindingSource.Clear()
    '            Return
    '        End If

    '        Dim currentTreatment As Patient_Trts = CType(Patient_TrtsBindingSource.Current, Patient_Trts)

    '        ' Initialize BindingSource if needed
    '        If Patient_PaysBindingSource Is Nothing Then
    '            Patient_PaysBindingSource = New BindingSource()
    '        End If
    '        Patient_PaysBindingSource.Clear()
    '        PayNavigator.Refresh()
    '        ' Load payments for specific treatment
    '        Dim payments = Conn.Query(Of Patient_Pays)(
    '            "SELECT PayID, TrtID, PatientID, PayValue, PayDate, Notes " &
    '            "FROM dbo.Patient_Pays " &
    '            "WHERE PatientID = @PatientID AND TrtID = @TrtID",
    '            New With {
    '                .PatientID = currentTreatment.PatientID,
    '                .TrtID = currentTreatment.TrtID
    '            })
    '        Patient_PaysBindingSource.DataSource = payments.ToList()
    '        Patient_PaysGridControl.DataSource = Patient_PaysBindingSource
    '    Else
    '        ' Null check
    '        If Patient_TrtsBindingSource.Current Is Nothing Then
    '            'Patient_PaysBindingSource.Clear()
    '            Return
    '        End If
    '        ' Load all payments for patient
    '        Dim payments = Conn.Query(Of Patient_Pays)(
    '            "SELECT PayID, TrtID, PatientID, PayValue, PayDate, Notes " &
    '            "FROM dbo.Patient_Pays " &
    '            "WHERE PatientID = @PatientID",
    '            New With {
    '                .PatientID = CurrentPatient.PatientID
    '            })
    '        Patient_PaysBindingSource.DataSource = payments.ToList()
    '        Patient_PaysGridControl.DataSource = Patient_PaysBindingSource
    '    End If
    'End Sub





#End Region

End Class