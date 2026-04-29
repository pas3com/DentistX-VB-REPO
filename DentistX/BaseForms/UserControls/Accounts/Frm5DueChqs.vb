Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data.SqlClient
Imports System.Drawing
Imports System.IO
Imports System.Linq
Imports System.Windows.Forms
Imports Dapper
Imports DevExpress.Data
Imports DevExpress.XtraEditors.Controls
Imports DevExpress.XtraGrid.Columns
Imports DevExpress.XtraGrid.Views.Base
Imports DevExpress.XtraGrid.Views.Grid
Imports TwainLib

Public Class Frm5DueChqs

    Private Enum DueCheqListFilterMode
        ReminderAdjustedDateEqualsToday = 0
        ChequeDueWithinNextNCalendarDays = 1
    End Enum

    Private Const AllChqsEditColumnField As String = "AllChqsEditAction"

    Private clsPaysData As New Patient_PaysDATA
    Private ReadOnly _allChqsBS As New BindingSource()
    Private _allChqsColumnsSetup As Boolean

    Private Sub Frm5DueChqs_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If btnEditDueCheque IsNot Nothing Then
            btnEditDueCheque.Text = If(Eng, "Edit payment…", "تعديل الدفعة…")
        End If
        If btnEditReturnedCheque IsNot Nothing Then
            btnEditReturnedCheque.Text = If(Eng, "Edit payment…", "تعديل الدفعة…")
        End If
        If btnEditAllChqs IsNot Nothing Then
            btnEditAllChqs.Text = If(Eng, "Edit payment…", "تعديل الدفعة…")
        End If
        EnsureDueCheqListModeRadioItems()
        UpdateDueCheqFilterUi()
        ReloadDueChequesGrid()
        EnsureAllChequesTabReady()
        ConfigureAllChqsChequeSideRadio()
        IntegerMoneyGridColumns.ApplyIntegerPayTrtEditors(chqsGrid)
        IntegerMoneyGridColumns.ApplyIntegerPayTrtEditors(gridReturned)
        IntegerMoneyGridColumns.ApplyIntegerPayTrtEditors(AllChqsGrid)
        ConfigurePayValueFooterSum(DueChqView, colPayValue, "n0")
        ConfigurePayValueFooterSum(ReturnedGridView, colReturnedPayValue, "n0")
    End Sub

    ''' <summary>Footer total for cheque / payment amount column (English or Arabic label).</summary>
    Private Sub ConfigurePayValueFooterSum(view As GridView, amountColumn As GridColumn, numericFooterFormat As String)
        If view Is Nothing OrElse amountColumn Is Nothing Then Return
        view.OptionsView.ShowFooter = True
        amountColumn.SummaryItem.SummaryType = SummaryItemType.Sum
        amountColumn.SummaryItem.FieldName = amountColumn.FieldName
        amountColumn.SummaryItem.DisplayFormat = If(Eng, "Sum = {0:" & numericFooterFormat & "}", "المجموع = {0:" & numericFooterFormat & "}")
    End Sub

    Private Sub ConfigureAllChqsChequeSideRadio()
        If lblAllChqsChequeScanSide Is Nothing OrElse radioAllChqsChequeSide Is Nothing Then Return
        lblAllChqsChequeScanSide.Text = If(Eng, "Cheque scan side:", "جانب مسح الشيك:")
        radioAllChqsChequeSide.Properties.Items.Clear()
        radioAllChqsChequeSide.Properties.Items.Add(New RadioGroupItem(ChequeImageFace.Front, If(Eng, "Front", "أمام")))
        radioAllChqsChequeSide.Properties.Items.Add(New RadioGroupItem(ChequeImageFace.Back, If(Eng, "Back", "خلف")))
        radioAllChqsChequeSide.EditValue = ChequeImageFace.Front
    End Sub

    Private Function GetSelectedAllChqsChequeImageFace() As ChequeImageFace
        If radioAllChqsChequeSide Is Nothing Then Return ChequeImageFace.Front
        Dim v = radioAllChqsChequeSide.EditValue
        If TypeOf v Is ChequeImageFace Then Return DirectCast(v, ChequeImageFace)
        Return ChequeImageFace.Front
    End Function

    Private Sub EnsureDueCheqListModeRadioItems()
        If rgDueCheqListMode Is Nothing OrElse rgDueCheqListMode.Properties.Items.Count > 0 Then Return
        rgDueCheqListMode.Properties.Items.Add(New RadioGroupItem(CInt(DueCheqListFilterMode.ReminderAdjustedDateEqualsToday), If(Eng,
            "Adjusted reminder = today (cheque due + N working days, Sun–Thu)",
            "تاريخ التذكير المعدّل = اليوم (الاستحقاق + N يوم عمل، أحد–خميس)")))
        rgDueCheqListMode.Properties.Items.Add(New RadioGroupItem(CInt(DueCheqListFilterMode.ChequeDueWithinNextNCalendarDays), If(Eng,
            "Cheque due within next N calendar days (incl. today)",
            "استحقاق الشيك خلال N يوماً تقويمياً (بما فيها اليوم)")))
        If rgDueCheqListMode.EditValue Is Nothing Then rgDueCheqListMode.EditValue = CInt(DueCheqListFilterMode.ReminderAdjustedDateEqualsToday)
    End Sub

    Private Function GetDueCheqFilterMode() As DueCheqListFilterMode
        Try
            Dim v = rgDueCheqListMode?.EditValue
            If v Is Nothing OrElse IsDBNull(v) Then Return DueCheqListFilterMode.ReminderAdjustedDateEqualsToday
            Return CType(CInt(v), DueCheqListFilterMode)
        Catch
            Return DueCheqListFilterMode.ReminderAdjustedDateEqualsToday
        End Try
    End Function

    Private Function ParseDueCheqDaysValue() As Integer
        Dim t = If(txtWorkDays?.Text, "").Trim()
        Dim n As Integer
        If Integer.TryParse(t, n) Then Return Math.Max(0, n)
        Return 0
    End Function

    Private Sub UpdateDueCheqFilterUi()
        If DueChqView Is Nothing Then Return
        Dim rm As New ComponentResourceManager(GetType(Frm5DueChqs))
        Dim windowMode = GetDueCheqFilterMode() = DueCheqListFilterMode.ChequeDueWithinNextNCalendarDays
        colAdjustedDueDate.Visible = Not windowMode
        If windowMode Then
            LabelControl2.Text = If(Eng, "Calendar days N (incl. today)", "أيام تقويمية N (بما فيها اليوم)")
            lblDueChqListHint.Text = If(Eng,
                "Lists cheques whose cheque due date is from today through today + N calendar days (inclusive). All calendar days count; weekends are not excluded. Change N, then click List Cheques.",
                "تُعرض الشيكات التي يقع تاريخ استحقاق الشيك من اليوم حتى اليوم+N (بالمعتمد الشامل). تُحسب كل أيام التقويم بما فيها عطلة نهاية الأسبوع. غيّر N ثم اضغط «إعرض الشيكات».")
        Else
            lblDueChqListHint.Text = rm.GetString("lblDueChqListHint.Text")
            LabelControl2.Text = rm.GetString("LabelControl2.Text")
        End If
    End Sub

    Private Sub ReloadDueChequesGrid()
        Dim n = ParseDueCheqDaysValue()
        Dim list As List(Of ChequeReminder)
        Select Case GetDueCheqFilterMode()
            Case DueCheqListFilterMode.ChequeDueWithinNextNCalendarDays
                list = clsPaysData.GetDueCheqsChequeDueWithinNextCalendarDays(Now, n)
            Case Else
                list = clsPaysData.GetDueCheqsAfterWorkingDays(Now, n)
        End Select
        ChqsBS.DataSource = list
        chqsGrid.DataSource = ChqsBS
    End Sub

    Private Sub rgDueCheqListMode_EditValueChanged(sender As Object, e As EventArgs) Handles rgDueCheqListMode.EditValueChanged
        UpdateDueCheqFilterUi()
        ReloadDueChequesGrid()
    End Sub

    Private Sub Frm5DueChqs_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        DisposeChqPreviewImage()
    End Sub

    Private Sub CheuqesTab_SelectedPageChanged(sender As Object, e As DevExpress.XtraTab.TabPageChangedEventArgs) Handles CheuqesTab.SelectedPageChanged
        If CheuqesTab.SelectedTabPage Is AllChequesTab Then
            EnsureAllChequesTabReady()
            ConfigureAllChqsChequeSideRadio()
        ElseIf CheuqesTab.SelectedTabPage Is ChequeReturnedTab Then
            LoadReturnedChequesListAll(showEmptyInformationMsg:=False)
        End If
    End Sub

    Private Sub EnsureAllChequesTabReady()
        SetupAllChequesGridColumnsOnce()
        If _allChqsBS.DataSource Is Nothing Then
            LoadAllChequesInternal(useFilters:=False)
        End If
    End Sub

    Private Sub SetupAllChequesGridColumnsOnce()
        If _allChqsColumnsSetup Then Return
        _allChqsColumnsSetup = True
        AddHandler AllChqsView.FocusedRowChanged, AddressOf AllChqsView_FocusedRowChanged
        AddHandler AllChqsView.CustomColumnDisplayText, AddressOf AllChqsView_CustomColumnDisplayText
        AllChqsView.Columns.Clear()
        AllChqsView.OptionsBehavior.Editable = False
        AllChqsView.OptionsView.ShowGroupPanel = False
        AllChqsGrid.DataSource = _allChqsBS
        ChqImage.Properties.ReadOnly = True
        ChqImage.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Never

        Dim ix = 0
        Dim c = AllChqsView.Columns.AddField("PatientName")
        c.Caption = If(Eng, "Patient name", "اسم المريض")
        c.VisibleIndex = ix
        ix += 1
        c = AllChqsView.Columns.AddField("ChqNumber")
        c.Caption = If(Eng, "Cheque #", "رقم الشيك")
        c.VisibleIndex = ix
        ix += 1
        c = AllChqsView.Columns.AddField("PayValue")
        c.Caption = If(Eng, "Amount", "المبلغ")
        c.VisibleIndex = ix
        ix += 1
        c.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        c.DisplayFormat.FormatString = "n2"
        c = AllChqsView.Columns.AddField("PayDate")
        c.Caption = If(Eng, "Pay date", "تاريخ الدفع")
        c.VisibleIndex = ix
        ix += 1
        c.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        c.DisplayFormat.FormatString = "yyyy-MM-dd"
        c = AllChqsView.Columns.AddField("ChqDueDate")
        c.Caption = If(Eng, "Cheque due", "استحقاق الشيك")
        c.VisibleIndex = ix
        ix += 1
        c.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        c.DisplayFormat.FormatString = "yyyy-MM-dd"
        c = AllChqsView.Columns.AddField("ChqBank")
        c.Caption = If(Eng, "Bank", "البنك")
        c.VisibleIndex = ix
        ix += 1
        c = AllChqsView.Columns.AddField("LinkedImagePath")
        c.Caption = If(Eng, "Linked image", "صورة مربوطة")
        c.VisibleIndex = ix
        ix += 1

        c = AllChqsView.Columns.AddField(AllChqsEditColumnField)
        c.Caption = If(Eng, "Scanner", "مسح ضوئي")
        c.UnboundType = UnboundColumnType.String
        c.VisibleIndex = ix
        ix += 1

        c = AllChqsView.Columns.AddField("PayID")
        c.Visible = False
        c = AllChqsView.Columns.AddField("PatientID")
        c.Visible = False

        Dim payValCol = AllChqsView.Columns("PayValue")
        If payValCol IsNot Nothing Then ConfigurePayValueFooterSum(AllChqsView, payValCol, "n2")
    End Sub

    Private Sub AllChqsView_CustomUnboundColumnData(sender As Object, e As CustomColumnDataEventArgs) Handles AllChqsView.CustomUnboundColumnData
        If e.Column Is Nothing OrElse e.Column.FieldName <> AllChqsEditColumnField Then Return
        If Not e.IsGetData Then Return
        If e.ListSourceRowIndex < 0 OrElse e.ListSourceRowIndex >= _allChqsBS.Count Then
            e.Value = ""
            Return
        End If
        Dim row = TryCast(_allChqsBS.Item(e.ListSourceRowIndex), ChequeReminder)
        If row Is Nothing Then
            e.Value = ""
            Return
        End If
        Dim paths = ChequeImageLinkHelper.FindLinkedChequeImagePaths(row.PayID, row.PatientID, row.ChqNumber)
        Dim hasScan = paths.Any(Function(p) File.Exists(p))
        e.Value = If(hasScan, If(Eng, "Rescan", "إعادة مسح"), If(Eng, "Link scan", "ربط مسح"))
    End Sub

    Private Sub AllChqsView_CustomColumnDisplayText(sender As Object, e As CustomColumnDisplayTextEventArgs)
        If e.Column Is Nothing OrElse e.Column.FieldName <> "LinkedImagePath" Then Return
        If e.ListSourceRowIndex < 0 OrElse e.ListSourceRowIndex >= _allChqsBS.Count Then
            e.DisplayText = "—"
            Return
        End If
        Dim row = TryCast(_allChqsBS.Item(e.ListSourceRowIndex), ChequeReminder)
        If row Is Nothing Then
            e.DisplayText = "—"
            Return
        End If
        Dim paths = ChequeImageLinkHelper.FindLinkedChequeImagePaths(row.PayID, row.PatientID, row.ChqNumber).Where(Function(p) File.Exists(p)).ToList()
        If paths.Count = 0 Then
            e.DisplayText = "—"
        ElseIf paths.Count = 1 Then
            e.DisplayText = If(Eng, "Yes", "نعم")
        Else
            e.DisplayText = If(Eng, "Yes (" & paths.Count & ")", "نعم (" & paths.Count & ")")
        End If
    End Sub

    Private Sub AllChqsView_RowCellClick(sender As Object, e As RowCellClickEventArgs) Handles AllChqsView.RowCellClick
        If e.Column Is Nothing OrElse e.Column.FieldName <> AllChqsEditColumnField Then Return
        Dim row = TryCast(AllChqsView.GetRow(e.RowHandle), ChequeReminder)
        If row Is Nothing Then Return
        RunTwainScanAndLinkChequeRow(row, e.RowHandle)
    End Sub

    Private Sub btnEditAllChqs_Click(sender As Object, e As EventArgs) Handles btnEditAllChqs.Click
        Try
            Dim row = TryCast(AllChqsView.GetFocusedRow(), ChequeReminder)
            If row Is Nothing AndAlso _allChqsBS IsNot Nothing AndAlso _allChqsBS.Current IsNot Nothing Then
                row = TryCast(_allChqsBS.Current, ChequeReminder)
            End If
            If row Is Nothing Then
                MessageBox.Show(If(Eng, "Select a row in the grid first.", "اختر صفاً في الجدول أولاً."), Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return
            End If
            If TryOpenPaymentEditorForChequeRow(row) Then RefreshGridsAfterPaymentEdit()
        Catch ex As Exception
            MessageBox.Show(ex.Message, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>Twain scan and link: two pages → _Front / _Back; one page → chosen side (same as <see cref="FrmChqPayAccnt.RunScanPatientChequeLinked"/>).</summary>
    Private Sub RunTwainScanAndLinkChequeRow(row As ChequeReminder, rowHandle As Integer)
        If row Is Nothing OrElse row.PayID <= 0 OrElse row.PatientID <= 0 Then
            MessageBox.Show(If(Eng, "Invalid row.", "صف غير صالح."), "Scan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If
        Dim chqRaw = If(row.ChqNumber, "").Trim()
        If String.IsNullOrWhiteSpace(chqRaw) Then
            MessageBox.Show(If(Eng, "Cheque number is required to name the linked image.", "رقم الشيك مطلوب لتسمية الملف المربوط."),
                            "Scan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If
        Try
            Dim folder = ChequeImageLinkHelper.GetPatientChequesFolder(row.PatientID)
            If String.IsNullOrWhiteSpace(folder) Then Return
            If Not Directory.Exists(folder) Then Directory.CreateDirectory(folder)

            Dim parentHandle As IntPtr = IntPtr.Zero
            Dim topForm = Me.FindForm()
            If topForm IsNot Nothing Then parentHandle = topForm.Handle

            Dim files = TwainOperations.ScanImagesToFolder(ImageType:=".jpg",
                                                          CloseScannerUIAfterImageTransfer:=True,
                                                          ScannerInfo:="",
                                                          DestinationFolder:=folder,
                                                          ParentWindowHandle:=parentHandle)
            If files Is Nothing OrElse files.Count = 0 Then
                MessageBox.Show(If(Eng, "No pages were scanned.", "لم يتم مسح أي صفحات."), "Scan", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return
            End If

            Dim keepPaths As New HashSet(Of String)(StringComparer.OrdinalIgnoreCase)
            If files.Count >= 2 Then
                Dim destF = ChequeImageLinkHelper.CopyScannedToLinkedCheque(row.PatientID, row.PayID, row.ChqNumber, files(0), ChequeImageFace.Front)
                Dim destB = ChequeImageLinkHelper.CopyScannedToLinkedCheque(row.PatientID, row.PayID, row.ChqNumber, files(1), ChequeImageFace.Back)
                keepPaths.Add(Path.GetFullPath(destF))
                keepPaths.Add(Path.GetFullPath(destB))
            Else
                Dim dest = ChequeImageLinkHelper.CopyScannedToLinkedCheque(row.PatientID, row.PayID, row.ChqNumber, files(0), GetSelectedAllChqsChequeImageFace())
                keepPaths.Add(Path.GetFullPath(dest))
            End If
            For Each f In files
                If String.IsNullOrWhiteSpace(f) Then Continue For
                Try
                    If Not keepPaths.Contains(Path.GetFullPath(f)) Then File.Delete(f)
                Catch
                End Try
            Next

            row.LinkedImagePath = ChequeImageLinkHelper.FindLinkedChequeImagePath(row.PayID, row.PatientID, row.ChqNumber)

            AllChqsView.RefreshData()
            If rowHandle >= 0 Then UpdateAllChqsPreviewImage(rowHandle)

            Dim msg = If(Eng, "Cheque scanned and linked to payment.", "تم مسح الشيك وربطه بالدفعة.")
            If files.Count >= 2 Then
                msg &= If(Eng, " Front and back images saved.", " تم حفظ صورة الأمام والخلف.")
            End If
            MessageBox.Show(msg, "Scan", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            MessageBox.Show(If(Eng, "Scan failed: ", "فشل المسح: ") & ex.Message, "Scan", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub AllChqsView_FocusedRowChanged(sender As Object, e As FocusedRowChangedEventArgs)
        UpdateAllChqsPreviewImage(e.FocusedRowHandle)
    End Sub

    Private Sub UpdateAllChqsPreviewImage(rowHandle As Integer)
        DisposeChqPreviewImage()
        If rowHandle < 0 Then Return
        Dim row = TryCast(AllChqsView.GetRow(rowHandle), ChequeReminder)
        If row Is Nothing OrElse String.IsNullOrEmpty(row.LinkedImagePath) OrElse Not File.Exists(row.LinkedImagePath) Then Return
        Try
            Using img = Image.FromFile(row.LinkedImagePath)
                ChqImage.Image = New Bitmap(img)
            End Using
        Catch
        End Try
    End Sub

    Private Sub DisposeChqPreviewImage()
        Dim oldImg = TryCast(ChqImage.Image, Image)
        ChqImage.Image = Nothing
        If oldImg IsNot Nothing Then oldImg.Dispose()
    End Sub

    Private Shared Function GetDateOrNull(de As DevExpress.XtraEditors.DateEdit) As Date?
        Dim v = de.EditValue
        If v Is Nothing OrElse IsDBNull(v) Then Return Nothing
        Try
            Return CDate(v).Date
        Catch
            Return Nothing
        End Try
    End Function

    Private Sub AttachLinkedChequeImages(rows As List(Of ChequeReminder))
        If rows Is Nothing Then Return
        For Each r In rows
            r.LinkedImagePath = ChequeImageLinkHelper.FindLinkedChequeImagePath(r.PayID, r.PatientID, r.ChqNumber)
        Next
    End Sub

    Private Sub LoadAllChequesInternal(useFilters As Boolean)
        Dim list As List(Of ChequeReminder)
        If useFilters Then
            list = clsPaysData.GetAllChequesFiltered(
                txtPatientSrch.Text,
                txtChqNumSrch.Text,
                GetDateOrNull(dtpFromPayDate),
                GetDateOrNull(dtpToPayDate))
        Else
            list = clsPaysData.GetAllCheques()
        End If
        AttachLinkedChequeImages(list)
        _allChqsBS.DataSource = Nothing
        _allChqsBS.DataSource = list
        _allChqsBS.ResetBindings(False)
        If AllChqsView.RowCount > 0 Then
            AllChqsView.FocusedRowHandle = 0
            UpdateAllChqsPreviewImage(0)
        Else
            UpdateAllChqsPreviewImage(-1)
        End If
    End Sub

    Private Sub btnListAllChqs_Click(sender As Object, e As EventArgs) Handles btnListAllChqs.Click
        txtPatientSrch.Text = ""
        txtChqNumSrch.Text = ""
        dtpFromPayDate.EditValue = Nothing
        dtpToPayDate.EditValue = Nothing
        SetupAllChequesGridColumnsOnce()
        LoadAllChequesInternal(useFilters:=False)
    End Sub

    Private Sub btnSrchAllChqs_Click(sender As Object, e As EventArgs) Handles btnSrchAllChqs.Click
        SetupAllChequesGridColumnsOnce()
        LoadAllChequesInternal(useFilters:=True)
    End Sub

    Private Sub btnListChqs_Click(sender As Object, e As EventArgs) Handles btnListChqs.Click
        ReloadDueChequesGrid()
    End Sub
    Private Function GetTreatmentDetailForTrtId(trtId As Integer) As String
        Using conn As New SqlConnection(DentistXDATA.GetConnection.ConnectionString)
            conn.Open()
            Dim d = conn.ExecuteScalar(Of String)("SELECT Detail FROM Patient_Trts WHERE TrtID = @TrtID", New With {.TrtID = trtId})
            Return If(d, "")
        End Using
    End Function

    Private Sub UpdatePatientBalanceIfMatchesPayment(patientId As Integer)
        If patientId <= 0 Then Return
        Try
            If FormManager.Instance Is Nothing OrElse FormManager.Instance.CurrentForm Is Nothing Then Return
            Dim p = FormManager.Instance.GetCurrentPatient()
            If p IsNot Nothing AndAlso p.PatientID = patientId Then
                FormManager.Instance.CurrentForm.UpdatePatientBalance(p)
                Dim bp = TryCast(FormManager.Instance.CurrentForm, BasePatientWorkspace)
                If bp IsNot Nothing AndAlso bp.BodyPanel.Controls.Count > 0 Then
                    Dim body = bp.BodyPanel.Controls(0)
                    Dim na = TryCast(body, NewAccounting)
                    If na IsNot Nothing Then na.GetTotals(p)
                    Dim ac = TryCast(body, Accounting)
                    If ac IsNot Nothing Then ac.GetTotals(p)
                End If
            End If
        Catch
        End Try
    End Sub

    ''' <summary>Same editors as <see cref="NewAccounting.btnEditPay_Click"/> (cash vs cheque/insurance/other).</summary>
    Private Function TryOpenPaymentEditorForChequeRow(row As ChequeReminder) As Boolean
        If row Is Nothing OrElse row.PayID <= 0 Then
            MessageBox.Show(If(Eng, "Invalid payment row.", "صف دفعة غير صالح."), Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return False
        End If
        Dim pay = clsPaysData.Select_Record(New Patient_Pays With {.PayID = row.PayID})
        If pay Is Nothing Then
            MessageBox.Show(If(Eng, "Payment not found.", "لم يُعثر على الدفعة."), Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return False
        End If
        Dim treatDetail = GetTreatmentDetailForTrtId(pay.TrtID)
        Dim isCash = String.Equals(PayTypeLabels.NormalizeToEnglish(If(pay.PayType, "")), PayTypeLabels.CashEn, StringComparison.OrdinalIgnoreCase)
        Dim patientId = If(pay.PatientID.HasValue, pay.PatientID.Value, 0)
        Dim ok As Boolean
        If isCash Then
            Dim F As New FrmAddPayAccnt()
            F.LoadForEdit(pay, treatDetail)
            ok = F.ShowDialog(Me) = DialogResult.OK
        Else
            Dim FChq As New FrmChqPayAccnt()
            FChq.LoadForEdit(pay, treatDetail)
            FChq.btnScan.Visible = True
            FChq.btnScanAndPay.Visible = False
            ok = FChq.ShowDialog(Me) = DialogResult.OK
        End If
        If ok Then UpdatePatientBalanceIfMatchesPayment(patientId)
        Return ok
    End Function

    Private Sub RefreshGridsAfterPaymentEdit()
        ReloadDueChequesGrid()
        If _allChqsBS.DataSource IsNot Nothing Then
            Dim hadFilters = Not String.IsNullOrWhiteSpace(txtPatientSrch.Text) OrElse Not String.IsNullOrWhiteSpace(txtChqNumSrch.Text) OrElse GetDateOrNull(dtpFromPayDate).HasValue OrElse GetDateOrNull(dtpToPayDate).HasValue
            LoadAllChequesInternal(useFilters:=hadFilters)
        End If
        If ReturnedChqsBS.DataSource IsNot Nothing Then
            Dim chqNo = If(txtSearchChqNumber?.Text?.Trim(), "")
            Dim accNo = If(txtSearchAccountNumber?.Text?.Trim(), "")
            If Not String.IsNullOrWhiteSpace(chqNo) OrElse Not String.IsNullOrWhiteSpace(accNo) Then
        Try
            Dim list = clsPaysData.SearchChequesByChqOrAccount(chqNo, accNo)
            AttachLinkedChequeImages(list)
            ReturnedChqsBS.DataSource = list
                    ReturnedGridView.RefreshData()
                    If list IsNot Nothing AndAlso list.Count > 0 Then
                        ReturnedGridView.FocusedRowHandle = 0
                        UpdateReturnedDetailsFromRow(0)
                    Else
                        ClearReturnedDetails()
                    End If
                Catch
                End Try
            Else
                Try
                    LoadReturnedChequesListAll(showEmptyInformationMsg:=False)
                Catch
                End Try
            End If
        End If
    End Sub

    Private Sub btnEditDueCheque_Click(sender As Object, e As EventArgs) Handles btnEditDueCheque.Click
        Try
            Dim row = TryCast(DueChqView.GetFocusedRow(), ChequeReminder)
            If row Is Nothing AndAlso ChqsBS IsNot Nothing AndAlso ChqsBS.Current IsNot Nothing Then
                row = TryCast(ChqsBS.Current, ChequeReminder)
            End If
            If row Is Nothing Then
                MessageBox.Show(If(Eng, "Select a cheque row first.", "اختر صف شيك أولاً."), Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return
            End If
            If TryOpenPaymentEditorForChequeRow(row) Then RefreshGridsAfterPaymentEdit()
        Catch ex As Exception
            MessageBox.Show(ex.Message, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnEditReturnedCheque_Click(sender As Object, e As EventArgs) Handles btnEditReturnedCheque.Click
        Try
            Dim row = TryCast(ReturnedGridView.GetFocusedRow(), ChequeReminder)
            If row Is Nothing AndAlso ReturnedChqsBS IsNot Nothing AndAlso ReturnedChqsBS.Current IsNot Nothing Then
                row = TryCast(ReturnedChqsBS.Current, ChequeReminder)
            End If
            If row Is Nothing Then
                MessageBox.Show(If(Eng, "Select a row first.", "اختر صفاً أولاً."), Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return
            End If
            If TryOpenPaymentEditorForChequeRow(row) Then RefreshGridsAfterPaymentEdit()
        Catch ex As Exception
            MessageBox.Show(ex.Message, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        Try
            If ChqsBS Is Nothing OrElse ChqsBS.Current Is Nothing Then
                MsgBox(If(Eng, "Select a cheque row in the Due cheques tab first.", "اختر صف شيك في تعليمة التبويب الشيكات المستحقة أولاً."), MsgBoxStyle.Information)
                Return
            End If
            Dim chqDue = TryCast(ChqsBS.Current, ChequeReminder)
            If chqDue Is Nothing Then
                MsgBox(If(Eng, "Could not read the selected row.", "تعذر قراءة الصف المحدد."), MsgBoxStyle.Exclamation)
                Return
            End If
            Me.Validate()
            ChqsBS.EndEdit()
            Dim isCashed = chqDue.IsCashed
            Dim isReturned As Boolean = If(chqDue.IsReturned.HasValue, chqDue.IsReturned.Value, False)
            Using conn As SqlConnection = DentistXDATA.GetConnection
                conn.Open()
                conn.Execute(
                    "UPDATE Patient_Pays SET IsCashed = @IsCashed, IsReturned = @IsReturned WHERE PayID = @PayID AND PatientID = @PatientID",
                    New With {
                        .PayID = chqDue.PayID,
                        .PatientID = chqDue.PatientID,
                        .IsCashed = isCashed,
                        .IsReturned = isReturned
                    })
            End Using
            UpdatePatientBalanceIfMatchesPayment(chqDue.PatientID)
        Catch ex As SqlException
            MsgBox(ex.Message)
        Catch ex As Exception
            MsgBox(If(Eng, "Update failed: ", "فشل التحديث: ") & ex.Message, MsgBoxStyle.Exclamation)
        End Try
    End Sub

    Private Sub btnUpdateAll_Click(sender As Object, e As EventArgs) Handles btnUpdateAll.Click
        Try
            Me.Validate()
            DueChqView.CloseEditor()
            DueChqView.UpdateCurrentRow()

            Using conn As SqlConnection = DentistXDATA.GetConnection
                conn.Open()
                Using trans = conn.BeginTransaction()
                    Try
                        For i As Integer = 0 To DueChqView.RowCount - 1
                            Dim payID As Integer = Convert.ToInt32(DueChqView.GetRowCellValue(i, "PayID"))
                            Dim patientID As Integer = Convert.ToInt32(DueChqView.GetRowCellValue(i, "PatientID"))
                            Dim isCashedObj As Object = DueChqView.GetRowCellValue(i, "IsCashed")
                            Dim isReturnedObj As Object = DueChqView.GetRowCellValue(i, "IsReturned")

                            Dim isCashed As Boolean? = Nothing
                            If isCashedObj IsNot Nothing AndAlso Not IsDBNull(isCashedObj) Then
                                isCashed = Convert.ToBoolean(isCashedObj)
                            End If
                            Dim isReturned As Boolean = False
                            If isReturnedObj IsNot Nothing AndAlso Not IsDBNull(isReturnedObj) Then
                                isReturned = Convert.ToBoolean(isReturnedObj)
                            End If

                            conn.Execute("
                            UPDATE Patient_Pays 
                            SET IsCashed = @IsCashed, IsReturned = @IsReturned 
                            WHERE PayID = @PayID AND PatientID = @PatientID",
                            New With {
                                .PayID = payID,
                                .PatientID = patientID,
                                .IsCashed = isCashed,
                                .IsReturned = isReturned
                            }, trans)
                        Next

                        trans.Commit()
                        Dim refreshed As New HashSet(Of Integer)()
                        For j As Integer = 0 To DueChqView.RowCount - 1
                            Dim pid = Convert.ToInt32(DueChqView.GetRowCellValue(j, "PatientID"))
                            If refreshed.Add(pid) Then
                                UpdatePatientBalanceIfMatchesPayment(pid)
                            End If
                        Next
                        MsgBox(If(Eng, "All changes saved successfully.", "تم حفظ جميع التغييرات بنجاح."), MsgBoxStyle.Information)
                    Catch ex As Exception
                        trans.Rollback()
                        MsgBox(If(Eng, "Error during update: ", "خطأ أثناء التحديث: ") & ex.Message)
                    End Try
                End Using
            End Using
        Catch ex As Exception
            MsgBox(If(Eng, "Unexpected error: ", "خطأ غير متوقع: ") & ex.Message)
        End Try
    End Sub

#Region "CheckReturnedTab – search by cheque/account"
    ''' <summary>Binds the returned tab to every cheque row with <see cref="Patient_Pays.IsReturned"/> set (newest first).</summary>
    Private Sub LoadReturnedChequesListAll(Optional showEmptyInformationMsg As Boolean = True)
        Try
            Dim list = clsPaysData.GetAllReturnedCheques()
            AttachLinkedChequeImages(list)
            ReturnedChqsBS.DataSource = list
            ReturnedGridView.RefreshData()
            If list Is Nothing OrElse list.Count = 0 Then
                ClearReturnedDetails()
                If showEmptyInformationMsg Then
                    MsgBox(If(Eng, "No returned cheques found.", "لم يُعثر على شيكات مرتجعة."), MsgBoxStyle.Information)
                End If
            Else
                ReturnedGridView.FocusedRowHandle = 0
                UpdateReturnedDetailsFromRow(0)
            End If
        Catch ex As Exception
            MsgBox(If(Eng, "Failed to load returned cheques: ", "فشل تحميل الشيكات المرتجعة: ") & ex.Message, MsgBoxStyle.Critical)
        End Try
    End Sub

    Private Sub btnListAllReturned_Click(sender As Object, e As EventArgs) Handles btnListAllReturned.Click
        LoadReturnedChequesListAll(showEmptyInformationMsg:=True)
    End Sub

    Private Sub btnSearchReturned_Click(sender As Object, e As EventArgs) Handles btnSearchReturned.Click
        Dim chqNo = If(txtSearchChqNumber?.Text?.Trim(), "")
        Dim accNo = If(txtSearchAccountNumber?.Text?.Trim(), "")
        If String.IsNullOrWhiteSpace(chqNo) AndAlso String.IsNullOrWhiteSpace(accNo) Then
            MsgBox(If(Eng, "Enter at least Cheque Number or Account Number to search.", "أدخل على الأقل رقم الشيك أو رقم الحساب للبحث."), MsgBoxStyle.Exclamation)
            Return
        End If
        Try
            Dim list = clsPaysData.SearchChequesByChqOrAccount(chqNo, accNo)
            ReturnedChqsBS.DataSource = list
            ReturnedGridView.RefreshData()
            If list Is Nothing OrElse list.Count = 0 Then
                ClearReturnedDetails()
                MsgBox(If(Eng, "No cheques found.", "لم يتم العثور على شيكات."), MsgBoxStyle.Information)
            Else
                ' Select first row so details update
                ReturnedGridView.FocusedRowHandle = 0
                UpdateReturnedDetailsFromRow(0)
            End If
        Catch ex As Exception
            MsgBox(If(Eng, "Search failed: ", "فشل البحث: ") & ex.Message, MsgBoxStyle.Critical)
        End Try
    End Sub

    Private Sub ReturnedGridView_FocusedRowChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles ReturnedGridView.FocusedRowChanged
        UpdateReturnedDetailsFromRow(ReturnedGridView.FocusedRowHandle)
    End Sub

    Private Sub UpdateReturnedDetailsFromRow(rowHandle As Integer)
        If rowHandle < 0 OrElse ReturnedChqsBS Is Nothing OrElse ReturnedChqsBS.Count = 0 Then
            ClearReturnedDetails()
            Return
        End If
        Dim row = CType(ReturnedGridView.GetRow(rowHandle), ChequeReminder)
        If row Is Nothing Then
            ClearReturnedDetails()
            Return
        End If
        Dim dueDateStr = If(row.ChqDueDate.HasValue, row.ChqDueDate.Value.ToString("yyyy-MM-dd"), "")
        Dim cashedStr = If(row.IsCashed.HasValue AndAlso row.IsCashed.Value,
            If(Eng, "Yes", "نعم"),
            If(Eng, "No", "لا"))
        Dim returnedStr = If(row.IsReturned.HasValue AndAlso row.IsReturned.Value,
            If(Eng, "Yes", "نعم"),
            If(Eng, "No", "لا"))
        Dim fmtEng = "PayID: {0}  |  Chq No: {1}  |  Account No: {2}  |  Bank: {3}  |  Owner: {4}" & vbCrLf &
            "Due Date: {5}  |  Value: {6:N2}  |  Pay Date: {7:yyyy-MM-dd}" & vbCrLf &
            "Notes: {8}  |  Cashed: {9}  |  Returned: {10}"
        Dim fmtAr = "معرّف الدفع: {0}  |  رقم الشيك: {1}  |  رقم الحساب: {2}  |  البنك: {3}  |  المالك: {4}" & vbCrLf &
            "تاريخ الاستحقاق: {5}  |  المبلغ: {6:N2}  |  تاريخ الدفع: {7:yyyy-MM-dd}" & vbCrLf &
            "ملاحظات: {8}  |  تم الصرف: {9}  |  مرتجع: {10}"
        Dim fmt = If(Eng, fmtEng, fmtAr)
        lblChqDetailsVal.Text = String.Format(fmt,
            row.PayID,
            If(row.ChqNumber, ""),
            If(row.AccountNumber, ""),
            If(row.ChqBank, ""),
            If(row.ChqOwner, ""),
            dueDateStr,
            row.PayValue,
            row.PayDate,
            If(row.Notes, ""),
            cashedStr,
            returnedStr)
        lblPatientIDVal.Text = row.PatientID.ToString()
        lblPatientNameVal.Text = If(row.PatientName, "")
    End Sub

    Private Sub ClearReturnedDetails()
        lblChqDetailsVal.Text = If(Eng, "Select a row above to see details.", "اختر صفاً أعلاه لعرض التفاصيل.")
        lblPatientIDVal.Text = "--"
        lblPatientNameVal.Text = "--"
    End Sub
#End Region

#Region "KeyPress"
    Private Sub txtWorkDays_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtWorkDays.KeyPress
        ' Allow control keys (Backspace, Delete, etc.)
        If Char.IsControl(e.KeyChar) Then
            Return
        End If

        ' Allow digits (0-9)
        If Char.IsDigit(e.KeyChar) Then
            Return
        End If

        ' Allow only ONE decimal point (for prices)
        If e.KeyChar = "."c AndAlso Not txtWorkDays.Text.Contains(".") Then
            Return
        End If

        ' Block any other character
        e.Handled = True
    End Sub
    Private Sub txtWorkDays_PreviewKeyDown(sender As Object, e As PreviewKeyDownEventArgs) Handles txtWorkDays.PreviewKeyDown
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
        If e.KeyCode = Keys.Decimal AndAlso txtWorkDays.Text.Contains(".") Then
            Return
        End If

        ' Block all other keys
        e.IsInputKey = False
    End Sub
    Private Sub txtWorkDays_EditValueChanged(sender As Object, e As EventArgs) Handles txtWorkDays.EditValueChanged
        ' Skip if empty
        If String.IsNullOrEmpty(txtWorkDays.Text) Then Return

        ' Store cursor position
        Dim cursorPos = txtWorkDays.SelectionStart

        ' Remove all non-numeric characters (except one decimal point)
        Dim cleanedText As New System.Text.StringBuilder()
        Dim hasDecimal As Boolean = False

        For Each c As Char In txtWorkDays.Text
            If Char.IsDigit(c) Then
                cleanedText.Append(c)
            ElseIf c = "."c AndAlso Not hasDecimal Then
                cleanedText.Append(c)
                hasDecimal = True
            End If
        Next

        ' If text was modified (due to invalid pasted chars), update it
        If cleanedText.ToString() <> txtWorkDays.Text Then
            txtWorkDays.Text = cleanedText.ToString()
            ' Restore cursor position (adjusting for removed characters)
            txtWorkDays.SelectionStart = Math.Min(cursorPos, txtWorkDays.Text.Length)
        End If
    End Sub




#End Region
End Class