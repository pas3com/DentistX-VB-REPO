Imports System.Collections.Concurrent
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data.SqlClient
Imports System.Diagnostics
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.IO
Imports System.Linq
Imports System.Windows.Forms
Imports TwainLib
Imports Dapper
Imports DevExpress.XtraEditors
Imports DevExpress.XtraGrid.Views.Base
Imports DevExpress.XtraTab
Imports Infragistics.Win.UltraWinGrid

Public Class NewAccounting
    Implements IPatientAwareUserControl

    Private ReadOnly _whatsBinder As PatientWhatsControlsBinder
    ''' <summary>Normalized English PayType values to show in GridViewPays (Cash tab = always Cash).</summary>
    Private _mainPaysPayTypeFilter As HashSet(Of String)
    ''' <summary>Normalized English PayType values for AllPaysView (driven by tab + pay-type combo).</summary>
    Private _allPaysViewPayTypeFilter As HashSet(Of String)
    ''' <summary>Normalized English PayType values for ChqsGridView; Nothing = show all.</summary>
    Private _chqsPaysPayTypeFilter As HashSet(Of String)

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        StoreOriginalBounds(Me)
        ' Double-buffer only — NEVER use UserPaint on a UserControl: it breaks child DevExpress
        ' editors (caret / click-to-position). Other forms that "work" don't use UserPaint here.
        Me.DoubleBuffered = True
        _whatsBinder = New PatientWhatsControlsBinder(cboPrefix, txtWhats, lblWhats, Me)
    End Sub
    Private Sub IPatientAwareUserControl_LoadPatientData(patientId As Integer) Implements IPatientAwareUserControl.LoadPatientData
        SyncCurrentPatientFromForm(patientId)
        If CurrentPatient Is Nothing Then Exit Sub
        Me.SuspendLayout()
        LoadPatientData(patientId)
        Me.ResumeLayout()
    End Sub
    ''' <summary>Sync CurrentPatient from base form or FormManager so control sees current patient.</summary>
    Private Sub SyncCurrentPatientFromForm(patientId As Integer)
        Dim parentWs = PatientAwareHelper.FindPatientWorkspace(Me)
        If parentWs IsNot Nothing AndAlso parentWs.Current_Patient IsNot Nothing AndAlso parentWs.Current_Patient.PatientID = patientId Then
            CurrentPatient = parentWs.Current_Patient
        ElseIf FormManager.Instance.IsBasePatientFormOpen AndAlso FormManager.Instance.GetCurrentPatient() IsNot Nothing AndAlso FormManager.Instance.GetCurrentPatient().PatientID = patientId Then
            CurrentPatient = FormManager.Instance.GetCurrentPatient()
        End If
    End Sub

    Public Sub LoadPatientData(patientId As Integer)
        LoadDataRelation(patientId)
        ' Load saved WhatsApp prefix + local/international number (same as appointment editor)
        Try
            Dim pData As New PatientDATA()
            Dim p = pData.Select_RecordByID(patientId)
            If p IsNot Nothing Then
                FillCboPrefixOnce()
                If CurrentPatient IsNot Nothing AndAlso CurrentPatient.PatientID = patientId Then
                    CurrentPatient.WhatsApp = p.WhatsApp
                    CurrentPatient.WhatsAppPrefix = p.WhatsAppPrefix
                    CurrentPatient.Phone = p.Phone
                    _whatsBinder.BindToPatient(CurrentPatient, False)
                Else
                    _whatsBinder.BindToPatient(p, False)
                End If
            End If
        Catch
            ' Ignore WhatsApp load errors; accounting UI should still work
        End Try
    End Sub

    ''' <summary>Refreshes txtWhats / cboPrefix / lblWhats from the patient record (e.g. after FrmAddTrtAccnt may have saved a new WhatsApp).</summary>
    Private Sub RefreshPatientWhatsAppDisplay(patientId As Integer)
        If patientId <= 0 Then Return
        Try
            Dim pData As New PatientDATA()
            Dim p = pData.Select_RecordByID(patientId)
            If p Is Nothing OrElse txtWhats Is Nothing Then Return
            FillCboPrefixOnce()
            If CurrentPatient IsNot Nothing AndAlso CurrentPatient.PatientID = patientId Then
                CurrentPatient.WhatsApp = p.WhatsApp
                CurrentPatient.WhatsAppPrefix = p.WhatsAppPrefix
                CurrentPatient.Phone = p.Phone
                _whatsBinder.BindToPatient(CurrentPatient, True)
            Else
                _whatsBinder.BindToPatient(p, True)
            End If
        Catch
        End Try
    End Sub

    Private ReadOnly controlBoundsCache As New ConcurrentDictionary(Of Control, Rectangle)
    Private Const OriginalPanelWidth As Integer = 1200
    Private Const OriginalPanelHeight As Integer = 648
    Private ReadOnly originalPanelSize As New Size(OriginalPanelWidth, OriginalPanelHeight)

    Private Sub StoreOriginalBounds(container As Control)
        Dim sw As New Stopwatch
        sw.Start()

        For Each ctrl As Control In container.Controls
            controlBoundsCache.TryAdd(ctrl, ctrl.Bounds)
            If ctrl.HasChildren Then StoreOriginalBounds(ctrl)
        Next
        sw.Stop()

    End Sub
    Private _lastProportionalSize As Size

    Private Sub ResizeControlsProportionally()
        If controlBoundsCache.IsEmpty Then Return
        Dim widthRatio = CSng(Me.ClientSize.Width) / OriginalPanelWidth
        Dim heightRatio = CSng(Me.ClientSize.Height) / OriginalPanelHeight
        Me.SuspendLayout()
        Try
            For Each kvp In controlBoundsCache
                kvp.Key.SetBounds(
                    CInt(kvp.Value.X * widthRatio),
                    CInt(kvp.Value.Y * heightRatio),
                    CInt(kvp.Value.Width * widthRatio),
                    CInt(kvp.Value.Height * heightRatio))
            Next
        Finally
            Me.ResumeLayout(True)
        End Try
    End Sub

    Private Sub NewAccounting_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        If controlBoundsCache.IsEmpty Then Return
        If Me.ClientSize.Width < 2 OrElse Me.ClientSize.Height < 2 Then Return
        ' Re-applying SetBounds on every repeated Resize with the same size resets focused editors
        ' and fights the caret. Only run when the client size actually changed.
        If Me.ClientSize = _lastProportionalSize Then Return
        _lastProportionalSize = Me.ClientSize
        ResizeControlsProportionally()
    End Sub

    Private Sub NewAccounting_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not DesignMode Then
            FillCboPrefixOnce()
        End If
        If CurrentPatient IsNot Nothing AndAlso CurrentPatient.PatientID > 0 Then
            LoadDataRelation(CurrentPatient.PatientID)
        Else
            LoadPatientScannedFiles()
            LoadPatientAttachments()
        End If
        SetupAttachedGridColumnsOnce()
        SetupImageFileContextMenus()
        ConfigureAccountingListTooltips()
        IntegerMoneyGridColumns.ApplyIntegerPayTrtEditors(Patient_TrtsGridControl)
        IntegerMoneyGridColumns.ApplyIntegerPayTrtEditors(AllPaysGrid)
        IntegerMoneyGridColumns.ApplyIntegerPayTrtEditors(Patient_PaysGridControl)
        IntegerMoneyGridColumns.ApplyIntegerPayTrtEditors(ChqsPayGrid)

        If btnTrtDel IsNot Nothing AndAlso btnTrtDel.Image Is Nothing Then
            btnTrtDel.Image = Global.DentistX.My.Resources.Resources.tbtnDelete
        End If
        If btnPayDel IsNot Nothing AndAlso btnPayDel.Image Is Nothing Then
            btnPayDel.Image = Global.DentistX.My.Resources.Resources.tbtnDelete
        End If
        RefreshLblWhats()
        ' Do not apply PayType grid criteria here — Patient_PaysBindingSource may still be designer
        ' nested mode with no current row; DevExpress then throws "Can't find property 'PayType'".
        ' Filters are re-applied from LoadDataRelation / LoadAllPays after data is bound.
    End Sub

#Region "Whats"
    Private Function GetFullWhatsNumber() As String
        If _whatsBinder.BoundPatient IsNot Nothing Then
            Return If(WhatsHelper.GetFullWhatsDigitsFromPatient(_whatsBinder.BoundPatient), "")
        End If
        Return WhatsHelper.BuildInternationalWhatsDigitsFromControls(cboPrefix, txtWhats)
    End Function

    ''' <summary>Updates lblWhats with the full WhatsApp number (prefix + txtWhats). Call after load or when prefix/number changes.</summary>
    Private Sub RefreshLblWhats()
        _whatsBinder.RefreshLabel()
    End Sub

    ''' <summary>Fills cboPrefix with country name and calling code. Palestine (970) and Israel (972) are first.</summary>
    Private Sub FillCboPrefixOnce()
        WhatsHelper.FillCboPrefixOnce(cboPrefix)
    End Sub

    Private Sub cboPrefix_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboPrefix.SelectedIndexChanged
        _whatsBinder.OnCboPrefixSelectedIndexChanged()
    End Sub

    Private Sub cboPrefix_EditValueChanged(sender As Object, e As EventArgs) Handles cboPrefix.EditValueChanged
        _whatsBinder.OnCboPrefixEditValueChanged()
    End Sub

    Private Sub txtWhats_ValueChanged(sender As Object, e As EventArgs) Handles txtWhats.EditValueChanged
        _whatsBinder.OnTxtWhatsEditValueChanged()
    End Sub

    Private Sub txtWhats_Leave(sender As Object, e As EventArgs) Handles txtWhats.Leave
        _whatsBinder.OnTxtWhatsLeave()
    End Sub

    Private Sub txtWhats_Validated(sender As Object, e As EventArgs) Handles txtWhats.Validated
        _whatsBinder.OnTxtWhatsValidated()
    End Sub

    Private Sub txtWhats_KeyDown(sender As Object, e As KeyEventArgs) Handles txtWhats.KeyDown
        ' Allow control keys: backspace, delete, arrows, tab, home/end, etc.
        If e.KeyCode = Keys.Back OrElse
           e.KeyCode = Keys.Delete OrElse
           e.KeyCode = Keys.Left OrElse
           e.KeyCode = Keys.Right OrElse
           e.KeyCode = Keys.Up OrElse
           e.KeyCode = Keys.Down OrElse
           e.KeyCode = Keys.Tab OrElse
           e.KeyCode = Keys.Home OrElse
           e.KeyCode = Keys.End Then
            Return
        End If

        ' Allow digits only (top row or numpad)
        Dim isTopRowDigit As Boolean = (e.KeyCode >= Keys.D0 AndAlso e.KeyCode <= Keys.D9)
        Dim isNumPadDigit As Boolean = (e.KeyCode >= Keys.NumPad0 AndAlso e.KeyCode <= Keys.NumPad9)

        ' Block Shift-modified digits (to avoid !, @, etc.)
        If (isTopRowDigit OrElse isNumPadDigit) AndAlso (Not e.Shift) Then
            Return
        End If

        ' Otherwise block the key
        e.SuppressKeyPress = True
        e.Handled = True
    End Sub

    Private Function ValidateWhatsAppNumber(fullNumberDigits As String, prefixDigits As String) As String
        If String.IsNullOrWhiteSpace(fullNumberDigits) Then
            Return If(Eng, "Enter WhatsApp/phone number (digits only).", "أدخل رقم واتساب/الجوال (أرقام فقط).")
        End If
        If fullNumberDigits.Any(Function(c) Not Char.IsDigit(c)) Then
            Return If(Eng, "Number must contain only digits (no spaces, dashes or plus sign).", "يجب أن يحتوي الرقم على أرقام فقط (بدون مسافات أو شرطات أو +).")
        End If

        If String.IsNullOrWhiteSpace(prefixDigits) Then
            If fullNumberDigits.Length < 10 OrElse fullNumberDigits.Length > 15 Then
                Return If(Eng, "Number must be 10–15 digits (e.g. 970599123456 for Palestine).", "يجب أن يكون الرقم 10–15 رقمًا (مثلاً 970599123456 لفلسطين).")
            End If
            Return ""
        End If

        Dim prefixLen As Integer = prefixDigits.Length
        Dim expectedLen As Integer = prefixLen + 9
        If fullNumberDigits.Length <> expectedLen Then
            Dim msgEn As String = $"Invalid length. For prefix +{prefixDigits} use {prefixLen} + 9 digits (after removing leading 0) = {expectedLen} total. Current: {fullNumberDigits.Length}."
            Dim msgAr As String = $"طول غير صحيح. لرمز +{prefixDigits} استخدم {prefixLen} + 9 أرقام بعد حذف الصفر الأول = {expectedLen}. الحالي: {fullNumberDigits.Length}."
            Return If(Eng, msgEn, msgAr)
        End If
        Return ""
    End Function

#End Region


#Region "Varaibles"
    Dim Finished As Boolean = False
    Dim CellStr As String = ""
    Public Event BalChanged(ByVal sender As Object, ByVal e As BalChangedEventArgs)
    Public Event PatientChanged(ByVal sender As Object, ByVal e As PatientChangedEventArgs)

    Private _currentPatient As Patient
    ''' <summary>Current patient from form/FormManager; synced in LoadPatientData(patientId).</summary>
    Public Property CurrentPatient As Patient
        Get
            Return _currentPatient
        End Get
        Set(value As Patient)
            _currentPatient = value
        End Set
    End Property
    Private clsPatient_Trts As New Patient_Trts
    Private clsPatient_TrtsData As New Patient_TrtsDATA
    Private clsPatient_Pays As New Patient_Pays
    Private clsPatient_PaysData As New Patient_PaysDATA
    Private clspatientBalance As PatientBalance

    Private Bal As Double = 0

    Private selectedTreatments As New List(Of TreatmentItem)

    Private _scanPreview1 As PictureEdit
    Private _scanPreviewResizeHooked As Boolean = False
    ''' <summary>Full paths of scanned files for current patient (sync with scannedFilesList by index).</summary>
    Private _scannedFilePaths As New List(Of String)
    Private _scannedListToolTip As New ToolTip()
    Private _lastScannedListTooltipText As String = ""
    ''' <summary>While True, <see cref="scannedFilesList_SelectedIndexChanged"/> does not update the preview (avoids placeholder flash while repopulating the list).</summary>
    Private _suppressScannedListPreviewEvents As Boolean
    Private _attachedGridColumnsSetup As Boolean = False
    Private _repoButtonOpen As DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit
    Private _ctxScannedFiles As ContextMenuStrip
    Private _mnuDeleteScannedFile As ToolStripMenuItem
    Private _scannedListContextIndex As Integer = -1
    Private _ctxAttachedFiles As ContextMenuStrip
    Private _mnuDeleteAttachedFile As ToolStripMenuItem
    ''' <summary>Full paths of rows in <see cref="GridAttached"/> after <see cref="LoadPatientAttachments"/> (sync with attachedFilesList by index).</summary>
    Private _attachedFilePaths As New List(Of String)
    Private _attachedListToolTip As New ToolTip()
    Private _lastAttachedListTooltipText As String = ""
    ''' <summary>While True, <see cref="attachedFilesList_SelectedIndexChanged"/> does not update the preview while the list is repopulated.</summary>
    Private _suppressAttachedListPreviewEvents As Boolean
    Private _attachedListContextIndex As Integer = -1

    Private Shared ReadOnly _acctTooltipFont As New Font("Calibri", 8.0F, FontStyle.Bold)
    Private Shared ReadOnly _acctContextMenuFont As New Font("Calibri", 10.0F, FontStyle.Bold)
    Private _accountingListTooltipsConfigured As Boolean

#End Region


#Region "Grid select"

    Private Sub GridViewTrts_FocusedRowChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles GridViewTrts.FocusedRowChanged

        Dim detail As Object = GridViewTrts.GetFocusedRowCellValue("Detail") 'colRowNum
        Dim RowNum As Object = GridViewTrts.GetFocusedRowCellValue("colRowNum") 'colRowNum
        'lblTreat.Text = $"{If(RowNum IsNot Nothing, RowNum.ToString(), "")} : {If(detail IsNot Nothing, detail.ToString(), "")}"
    End Sub


#End Region



#Region "Classes"





    Private Sub FormatLbls()
        Dim TrtsTot, PaysTot, Bal As Double
        ' Check if there's data
        If PatientBalanceBindingSource.Current Is Nothing Then
            TrtsTot = 0
            PaysTot = 0
            Bal = 0
            lblTotalTrts.Text = "0.00"
            lblTotalPays.Text = PaysTot.ToString("N2")
            lblBal.Text = Bal.ToString("N2")
            lblTotalDisc.Text = "0.00"
            lblTotalTrts.ForeColor = Color.Black
            lblTotalPays.ForeColor = Color.Black
            lblBal.ForeColor = Color.Black
            Exit Sub
        End If

        Dim clsPatientBalance As PatientBalance = CType(PatientBalanceBindingSource.Current, PatientBalance)

        ' Balance / pays = full account (GetPatientBalance: net treatment total vs payments). lblTotalTrts = gross TrtValue sum on the grid; lblTotalDisc = discount sum on the grid.
        TrtsTot = clsPatientBalance.TotalTreatments
        PaysTot = clsPatientBalance.TotalPayments
        Bal = clsPatientBalance.Balance

        ' Format numbers with 2 decimal places and thousands separator
        lblTotalTrts.Text = GetGridTotalTreatmentsBeforeDiscount().ToString("N2")
        lblTotalPays.Text = PaysTot.ToString("N2")
        lblBal.Text = Bal.ToString("N2")
        lblTotalDisc.Text = GetGridTotalDiscount().ToString("N2")

        ' Set colors (same as above)
        lblTotalTrts.ForeColor = Color.Blue
        lblTotalPays.ForeColor = Color.Green

        If Bal = 0 Then
            lblBal.ForeColor = Color.Black
        ElseIf Bal > 0 Then
            lblBal.ForeColor = Color.Blue
        Else
            lblBal.ForeColor = Color.Red
        End If
    End Sub

    Private Function GetGridTotalTreatmentsBeforeDiscount() As Decimal
        Dim trtList As IEnumerable(Of Patient_Trts) = TryCast(Patient_TrtsBindingSource?.List, IEnumerable(Of Patient_Trts))
        If trtList Is Nothing Then Return 0D
        Return trtList.Sum(Function(t) t.TrtValue)
    End Function

    Private Function GetGridTotalDiscount() As Decimal
        Dim trtList As IEnumerable(Of Patient_Trts) = TryCast(Patient_TrtsBindingSource?.List, IEnumerable(Of Patient_Trts))
        If trtList Is Nothing Then Return 0D
        Return trtList.Sum(Function(t) t.Discount.GetValueOrDefault() + t.Discount2.GetValueOrDefault())
    End Function

    Public Sub GetTotals(ByVal patient As Patient)

        ' Load Patient Balance
        clspatientBalance = clsPatient_TrtsData.GetPatientBalance(patient.PatientID)
        If PatientBalanceBindingSource Is Nothing Then
            PatientBalanceBindingSource = New BindingSource()
        End If
        PatientBalanceBindingSource.DataSource = New List(Of PatientBalance) From {clspatientBalance}

        FormatLbls()
    End Sub

    Private Sub RadioFilter_SelectedIndexChanged(sender As Object, e As EventArgs) Handles RadioFilter.SelectedIndexChanged
        Select Case RadioFilter.SelectedIndex
            Case 0
                LoadDataRelation(CurrentPatient.PatientID, 0)
            Case 1
                LoadDataRelation(CurrentPatient.PatientID, 1)
            Case 2
                LoadDataRelation(CurrentPatient.PatientID, 2)
            Case 3
                LoadDataRelation(CurrentPatient.PatientID, 3)
        End Select
    End Sub

    Public Sub LoadDataRelation(ByVal patientID As Integer, Optional index As Integer = 0)
        Try
            Dim _patient = CurrentPatient
            If _patient Is Nothing Then Exit Sub

            Finished = False
            lblTreatsDetails.Text = If(Eng, $"Treatments Detaials For Patient {_patient.PatientName}", $"تفاصيل العلاجات للمريض {_patient.PatientName}")
            lblPaysDetails.Text = If(Eng, $"Payments Detaials For Patient {_patient.PatientName}", $"تفاصيل الدفعات للمريض {_patient.PatientName}")

            Using conn As SqlConnection = DentistXDATA.GetConnection
                conn.Open()

                ' --- Load parent (treatments) ---
                Dim allPatientTrts = conn.Query(Of Patient_Trts)(
                "SELECT TrtID, PatientID, ToothTrtID, OrthoID, OtherTrtID, Detail, TrtDate, TrtValue, IsMultiTooth, Discount, Discount2,
                DiscountType FROM dbo.Patient_Trts WHERE PatientID = @PatientID order by TrtDate DESC",
                New With {.PatientId = _patient.PatientID}
            ).ToList()

                ' Apply filter based on index
                Dim patientTrts As List(Of Patient_Trts)
                Select Case index
                    Case 1 ' Only when ToothTrtID is not null
                        patientTrts = allPatientTrts.Where(Function(t) t.ToothTrtID IsNot Nothing AndAlso t.ToothTrtID > 0).ToList()
                    Case 2 ' Only when OtherTrtID is not null
                        patientTrts = allPatientTrts.Where(Function(t) t.OtherTrtID IsNot Nothing AndAlso t.OtherTrtID > 0).ToList()
                    Case 3 ' Only when OrthoID is not null
                        patientTrts = allPatientTrts.Where(Function(t) t.OrthoID IsNot Nothing AndAlso t.OrthoID > 0).ToList()
                    Case Else ' index = 0 or any other value - no filter
                        patientTrts = allPatientTrts
                End Select

                ' --- Load child (pays) ---
                Dim patientPays = conn.Query(Of Patient_Pays)(
                "SELECT [PayID],[TrtID],[PatientID],[PayValue],[PayDate],[Notes],[PayType],[ChqOwner],[AccountNumber],[ChqNumber]
                ,[ChqDueDate] ,[ChqBank],[IsCashed],[InsuranceCompany],[InsuranceNotes],[IsForward],[ForwardFromTo],[ReceivedBy],[IsReturned]
                 FROM [dbo].[Patient_Pays] 
                 WHERE PatientID = @PatientID ORDER BY PayDate DESC, PayID DESC",
                New With {.PatientId = _patient.PatientID}).ToList()

                ' --- Link them manually (same order as global list: newest pay date first) ---
                For Each trt In patientTrts
                    trt.Patient_PaysIEnumerable = patientPays.Where(Function(p) p.TrtID = trt.TrtID).
                        OrderByDescending(Function(p) p.PayDate).ThenByDescending(Function(p) p.PayID).ToList()
                Next

                ' --- Bind parent and child sources ---
                If Patient_TrtsBindingSource Is Nothing Then
                    Patient_TrtsBindingSource = New BindingSource()
                End If
                If Patient_PaysBindingSource Is Nothing Then
                    Patient_PaysBindingSource = New BindingSource()
                End If

                ' Parent
                Patient_TrtsBindingSource.DataSource = New BindingList(Of Patient_Trts)(patientTrts)

                ' Child (relation-style)
                Patient_PaysBindingSource.DataSource = Patient_TrtsBindingSource
                Patient_PaysBindingSource.DataMember = "Patient_PaysIEnumerable"

                ' Assign to your grids
                Patient_TrtsGridControl.DataSource = Patient_TrtsBindingSource
                Patient_PaysGridControl.DataSource = Patient_PaysBindingSource

                LoadAllPays(showAsChildChck.Checked)
                LoadPatientScannedFiles()
                LoadPatientAttachments()

                ' Totals or other logic
                GetTotals(_patient)
                ApplyPaymentTabAndFilters()

            End Using
            RaiseEvent BalChanged(Me, New BalChangedEventArgs(patientID, PatientName, Finished))
            FormManager.Instance.CurrentForm.UpdatePatientBalance(CurrentPatient)
            Finished = True
        Catch ex As SqlException
            MsgBox(ex.Message)
        Finally

        End Try
    End Sub

    Public Sub LoadDataDapper(ByVal patientID As Integer)
        Try
            Dim _patient = CurrentPatient
            If _patient Is Nothing Then Exit Sub
            Finished = False
            lblTreatsDetails.Text = If(Eng, $"Treatments Detaials For Patient {_patient.PatientName}", $"تفاصيل العلاجات للمريض {_patient.PatientName}")
            lblPaysDetails.Text = If(Eng, $"Payments Detaials For Patient {_patient.PatientName}", $"تفاصيل الدفعات للمريض {_patient.PatientName}")
            Using conn As SqlConnection = DentistXDATA.GetConnection
                conn.Open() 'TrtValue
                ' Load Patient UnPaid Treatments
                Dim unpaidTrts = conn.Query(Of Patient_Trts)("SELECT ToothTrtID, PatientID, TreatDate, Treat,  IsExternal, IsPaid FROM dbo.Patient_ToothTrt
                                                                                    WHERE PatientID = @PatientID", New With {.PatientId = _patient.PatientID})
                ' Load Patient Treatments
                Dim patientTrts = conn.Query(Of Patient_Trts)("SELECT TrtID ,PatientID ,ToothTrtID ,OrthoID ,OtherTrtID ,Detail ,TrtDate ,TrtValue ,IsMultiTooth ,Discount ,Discount2 ,DiscountType
                                                            FROM dbo.Patient_Trts WHERE PatientID = @PatientID", New With {.PatientId = _patient.PatientID})
                If Patient_TrtsBindingSource Is Nothing Then
                    Patient_TrtsBindingSource = New BindingSource()
                End If
                'Patient_TrtsBindingSource.DataSource = patientTrts.ToList()
                Patient_TrtsBindingSource.DataSource = New BindingList(Of Patient_Trts)(patientTrts.ToList())
                ' Initialize Patient_PaysBindingSource if needed
                If Patient_PaysBindingSource Is Nothing Then
                    Patient_PaysBindingSource = New BindingSource()
                End If

                '' Set up parent-child relationship
                Try
                    If showAsChildChck.Checked Then
                        ' ✅ Show ALL payments (independent)
                        showAsChildChck.Text = If(Eng, "Show Related Payments", "عرض الدفعات المرتبطة")

                        Dim patientPays = conn.Query(Of Patient_Pays)(
                "SELECT [PayID],[TrtID],[PatientID],[PayValue],[PayDate],[Notes],[PayType],[ChqOwner],[AccountNumber],[ChqNumber],[ChqDueDate],[ChqBank],[IsCashed],[InsuranceCompany],[InsuranceNotes],[IsForward],[ForwardFromTo],[ReceivedBy],[IsReturned] FROM dbo.Patient_Pays WHERE PatientID = @PatientID ORDER BY PayDate DESC, PayID DESC",
                New With {.PatientId = CurrentPatient.PatientID}
            )

                        Patient_PaysBindingSource.DataSource = patientPays.ToList()

                    Else
                        ' ❌ Show only related payments (activate parent/child relation)
                        showAsChildChck.Text = If(Eng, "Show All Payments", "عرض جميع الدفعات")

                        ' Rebind relation mode (child of Patient_Trts)
                        Patient_PaysBindingSource.DataSource = Patient_TrtsBindingSource
                        Patient_PaysBindingSource.DataMember = "Patient_PaysIEnumerable"

                        ' Refresh the view for the current treatment
                        If Patient_TrtsBindingSource.Current IsNot Nothing Then
                            Dim currentTreatment As Patient_Trts = CType(Patient_TrtsBindingSource.Current, Patient_Trts)
                            ' This ensures the grid refreshes immediately
                            Patient_PaysBindingSource.ResetBindings(False)
                        End If
                    End If

                Catch ex As Exception
                    MsgBox("Error switching payment mode: " & ex.Message)
                End Try
                ApplyPaymentTabAndFilters()
                LoadPatientScannedFiles()
                LoadPatientAttachments()

                ' ... rest of your code ...
                GetTotals(_patient)
            End Using

            Finished = True
        Catch ex As SqlException
            MsgBox(ex.Message)

        End Try
    End Sub
    Public Sub LoadSubDataDapper(ByVal patientID As Integer)
        Dim filterIndex = If(RadioFilter IsNot Nothing, RadioFilter.SelectedIndex, 0)
        LoadDataRelation(patientID, filterIndex)

        Dim currentTreatment As Patient_Trts = GetSelectedTreatment()
        TrtID = If(currentTreatment IsNot Nothing, currentTreatment.TrtID, 0)

        ' Totals (PatientBalance SP / labels) must refresh after pay grid data reload — e.g. edit cheque IsReturned.
        If CurrentPatient IsNot Nothing AndAlso CurrentPatient.PatientID = patientID Then
            GetTotals(CurrentPatient)
        End If
    End Sub




    Private Sub showAsChildChck_CheckedChanged(sender As Object, e As EventArgs) Handles showAsChildChck.CheckedChanged
        LoadAllPays(showAsChildChck.Checked)
    End Sub

    Private Sub LoadAllPays(all As Boolean)
        If CurrentPatient Is Nothing Then Exit Sub

        Try
            If all Then
                ' ✅ Show ALL payments (independent)
                showAsChildChck.Text = If(Eng, "Show Related Payments", "عرض الدفعات المرتبطة")

                Using conn As SqlConnection = DentistXDATA.GetConnection
                    conn.Open()
                    Dim patientPays = conn.Query(Of Patient_Pays)(
                    "SELECT [PayID],[TrtID],[PatientID],[PayValue],[PayDate],[Notes],[PayType],[ChqOwner],[AccountNumber],[ChqNumber],[ChqDueDate],[ChqBank],[IsCashed],[InsuranceCompany],[InsuranceNotes],[IsForward],[ForwardFromTo],[ReceivedBy],[IsReturned] FROM dbo.Patient_Pays WHERE PatientID = @PatientID ORDER BY PayDate DESC, PayID DESC",
                    New With {.PatientId = CurrentPatient.PatientID}
                )

                    Patient_PaysBindingSource.DataSource = patientPays.ToList()
                End Using
            Else
                ' ❌ Show only related payments (activate parent/child relation)
                showAsChildChck.Text = If(Eng, "Show All Payments", "عرض جميع الدفعات")

                ' Rebind relation mode (child of Patient_Trts)
                Patient_PaysBindingSource.DataSource = Patient_TrtsBindingSource
                Patient_PaysBindingSource.DataMember = "Patient_PaysIEnumerable"

                ' Refresh the view for the current treatment
                If Patient_TrtsBindingSource.Current IsNot Nothing Then
                    Dim currentTreatment As Patient_Trts = CType(Patient_TrtsBindingSource.Current, Patient_Trts)
                    ' This ensures the grid refreshes immediately
                    Patient_PaysBindingSource.ResetBindings(False)
                End If
            End If
            ApplyPaymentTabAndFilters()
        Catch ex As Exception
            MsgBox("Error switching payment mode: " & ex.Message)
        End Try
    End Sub

    Private Sub Patient_TrtsBindingSource_CurrentChanged(sender As Object, e As EventArgs) Handles Patient_TrtsBindingSource.CurrentChanged
        If showAsChildChck.Checked AndAlso CurrentPatient IsNot Nothing Then
            If Patient_TrtsBindingSource.Current IsNot Nothing Then
                Dim currentTreatment As Patient_Trts = CType(Patient_TrtsBindingSource.Current, Patient_Trts)

                Using conn As SqlConnection = DentistXDATA.GetConnection
                    conn.Open()
                    Dim payments = conn.Query(Of Patient_Pays)(
                                                "SELECT PayID, TrtID, PatientID, PayValue, PayDate, Notes, PayType, ChqOwner, AccountNumber, ChqNumber, ChqDueDate, ChqBank, IsCashed, InsuranceCompany, InsuranceNotes, IsForward, ForwardFromTo, ReceivedBy, IsReturned " &
                                                "FROM dbo.Patient_Pays " &
                                                "WHERE PatientID = @PatientID AND TrtID = @TrtID",
                                                New With {
                                                    .PatientID = currentTreatment.PatientID,
                                                    .TrtID = currentTreatment.TrtID
                                                })

                    'Patient_PaysBindingSource.DataSource = payments.ToList()
                End Using
            Else
                'Patient_PaysBindingSource.DataSource = Nothing
            End If
            GetTotals(CurrentPatient)
        End If
        ApplyPaymentTabAndFilters()
    End Sub


#End Region


#Region "Events"

    'Private Sub EnsureScanPreview()
    '    If _scanPreview IsNot Nothing AndAlso Not _scanPreview.IsDisposed Then
    '        UpdateScanPreviewLayout()
    '        Return
    '    End If

    '    _scanPreview = New PictureEdit() With {
    '        .Name = "picScanPreview",
    '        .Size = New Size(200, 140),
    '        .BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
    '    }
    '    _scanPreview.Properties.ReadOnly = True
    '    _scanPreview.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Never
    '    _scanPreview.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom
    '    _scanPreview.Properties.AllowFocused = False

    '    PayMainDetPanel.Controls.Add(_scanPreview)
    '    UpdateScanPreviewLayout()

    '    If Not _scanPreviewResizeHooked Then
    '        AddHandler PayMainDetPanel.Resize, AddressOf PayMainDetPanel_Resize
    '        _scanPreviewResizeHooked = True
    '    End If
    'End Sub

    Private Sub PayMainDetPanel_Resize(sender As Object, e As EventArgs)
        UpdateScanPreviewLayout()
    End Sub

    Private Sub UpdateScanPreviewLayout()
        If _scanPreview Is Nothing OrElse _scanPreview.IsDisposed Then Return

        'Dim margin As Integer = 8
        'Dim left As Integer = PayMainDetPanel.ClientSize.Width - _scanPreview.Width - margin
        'Dim top As Integer = margin

        'If btnScan IsNot Nothing Then
        '    top = btnScan.Bottom + margin
        '    If top + _scanPreview.Height > PayMainDetPanel.ClientSize.Height Then
        '        top = Math.Max(margin, PayMainDetPanel.ClientSize.Height - _scanPreview.Height - margin)
        '    End If
        'End If

        'If left < margin Then left = margin
        'If top < margin Then top = margin

        '_scanPreview.Location = New Point(left, top)
        _scanPreview.BringToFront()
    End Sub

    Private Sub SetScanPreviewImage(imagePath As String)
        If String.IsNullOrWhiteSpace(imagePath) OrElse Not File.Exists(imagePath) Then Return
        'EnsureScanPreview()

        Dim oldImage = TryCast(_scanPreview.Image, Image)
        Using img = Image.FromFile(imagePath)
            _scanPreview.Image = New Bitmap(img)
        End Using
        If oldImage IsNot Nothing Then oldImage.Dispose()
    End Sub

    Private Sub OpenScanFolder(folderPath As String)
        If String.IsNullOrWhiteSpace(folderPath) OrElse Not Directory.Exists(folderPath) Then Return
        Try
            Process.Start(New ProcessStartInfo("explorer.exe", folderPath) With {.UseShellExecute = True})
        Catch
        End Try
    End Sub

    Private Sub btnBrowse_Click(sender As Object, e As EventArgs) Handles btnBrowse.Click
        If CurrentPatient Is Nothing OrElse PatientID <= 0 Then
            Dim msgEn = "Please select a patient first."
            Dim msgAr = "يرجى اختيار المريض أولا."
            MessageBox.Show(If(Eng, msgEn, msgAr), "Attachments", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim filters = String.Join("|", {
            "All documents (*.pdf;*.doc;*.docx;*.xls;*.xlsx;*.txt;*.rtf)|*.pdf;*.doc;*.docx;*.xls;*.xlsx;*.txt;*.rtf",
            "All images (*.jpg;*.jpeg;*.png;*.bmp;*.gif;*.tif;*.tiff)|*.jpg;*.jpeg;*.png;*.bmp;*.gif;*.tif;*.tiff",
            "All files (*.*)|*.*"
        })

        Using ofd As New OpenFileDialog() With {
            .Title = "Select files to attach",
            .Filter = filters,
            .FilterIndex = 1,
            .Multiselect = True
        }
            If ofd.ShowDialog() <> DialogResult.OK Then Return

            Dim attachmentsRoot = Path.Combine(Application.StartupPath, "Attachments")
            Directory.CreateDirectory(attachmentsRoot)

            Dim savedCount As Integer = 0
            For Each srcPath In ofd.FileNames
                Dim destPath = BuildPatientAttachmentPath(attachmentsRoot, PatientID, srcPath)
                File.Copy(srcPath, destPath, True)
                savedCount += 1
            Next

            Dim msgEn = $"Saved {savedCount} file(s) to:{vbCrLf}{attachmentsRoot}"
            Dim msgAr = $"تم حفظ {savedCount} ملف/ملفات في:{vbCrLf}{attachmentsRoot}"
            MessageBox.Show(If(Eng, msgEn, msgAr), "Attachments", MessageBoxButtons.OK, MessageBoxIcon.Information)
            LoadPatientAttachments()
        End Using
    End Sub

    Private Function BuildPatientAttachmentPath(rootFolder As String, patientId As Integer, sourcePath As String) As String
        Dim ext = Path.GetExtension(sourcePath)
        Dim baseName = Path.GetFileNameWithoutExtension(sourcePath)
        Dim safeBase = SanitizeFileName(baseName)

        Dim fileName = $"Patient{patientId}_{safeBase}{ext}"
        Dim fullPath = Path.Combine(rootFolder, fileName)

        If Not File.Exists(fullPath) Then Return fullPath

        Dim counter As Integer = 1
        Do
            fileName = $"Patient{patientId}_{safeBase}_{counter}{ext}"
            fullPath = Path.Combine(rootFolder, fileName)
            counter += 1
        Loop While File.Exists(fullPath)

        Return fullPath
    End Function

    Private Function SanitizeFileName(name As String) As String
        If String.IsNullOrWhiteSpace(name) Then Return "Attachment"
        Dim invalid = Path.GetInvalidFileNameChars()
        Dim sb As New System.Text.StringBuilder(name.Length)
        For Each ch In name
            If Array.IndexOf(invalid, ch) >= 0 Then
                sb.Append("_"c)
            Else
                sb.Append(ch)
            End If
        Next
        Dim cleaned = sb.ToString().Trim()
        Return If(cleaned.Length = 0, "Attachment", cleaned)
    End Function

    ''' <summary>Attachments root folder: AppFolder\Attachments. Patient files are named Patient{id}_*.</summary>
    Private Function GetPatientAttachmentsFolder() As String
        Return Path.Combine(Application.StartupPath, "Attachments")
    End Function

    ''' <summary>Loads files linked to the focused ChqsPayGrid cheque row for GridAttached / attachedFilesList. Empty when no row or no linked files.</summary>
    Private Sub LoadPatientAttachments()
        If GridAttached Is Nothing OrElse ViewAttached Is Nothing Then Return
        SetupAttachedGridColumnsOnce()
        Dim list As New List(Of NewAttachmentFileRow)
        If CurrentPatient Is Nothing OrElse CurrentPatient.PatientID <= 0 Then
            GridAttached.DataSource = list
            RefreshAttachedFilesListFromGrid()
            If GridTabControl IsNot Nothing AndAlso GridTabControl.SelectedTabPage Is AttachedPage Then
                SyncAttachedPageToFocusedChequeRow()
            End If
            Return
        End If

        If ChqsGridView Is Nothing Then
            GridAttached.DataSource = list
            RefreshAttachedFilesListFromGrid()
            Return
        End If

        Dim pay = TryCast(ChqsGridView.GetFocusedRow(), Patient_Pays)
        If pay Is Nothing OrElse pay.PayID <= 0 Then
            GridAttached.DataSource = list
            RefreshAttachedFilesListFromGrid()
            If GridTabControl IsNot Nothing AndAlso GridTabControl.SelectedTabPage Is AttachedPage Then
                SyncAttachedPageToFocusedChequeRow()
            End If
            Return
        End If

        For Each fullPath In CollectLinkedAttachmentPathsForPay(pay)
            Try
                Dim fi As New FileInfo(fullPath)
                list.Add(New NewAttachmentFileRow With {
                    .FullPath = fullPath,
                    .Size = fi.Length,
                    .Type = fi.Extension
                })
            Catch
            End Try
        Next

        GridAttached.DataSource = list
        RefreshAttachedFilesListFromGrid()
        If GridTabControl IsNot Nothing AndAlso GridTabControl.SelectedTabPage Is AttachedPage Then
            SyncAttachedPageToFocusedChequeRow()
        End If
    End Sub

    ''' <summary>Fills attachedFilesList from the current <see cref="GridAttached"/> data (same order as the grid).</summary>
    Private Sub RefreshAttachedFilesListFromGrid()
        _attachedFilePaths.Clear()
        _lastAttachedListTooltipText = ""
        If attachedFilesList IsNot Nothing Then _attachedListToolTip.SetToolTip(attachedFilesList, "")
        If attachedFilesList Is Nothing Then Return
        attachedFilesList.Items.Clear()
        Dim list = TryCast(GridAttached?.DataSource, List(Of NewAttachmentFileRow))
        If list Is Nothing OrElse list.Count = 0 Then
            If attachPreview IsNot Nothing AndAlso Not attachPreview.IsDisposed Then attachPreview.Image = Nothing
            Return
        End If
        For Each row In list
            If String.IsNullOrWhiteSpace(row.FullPath) Then Continue For
            _attachedFilePaths.Add(row.FullPath)
            attachedFilesList.Items.Add(IO.Path.GetFileName(row.FullPath))
        Next
        If attachPreview IsNot Nothing AndAlso Not attachPreview.IsDisposed Then attachPreview.Image = Nothing
        If attachedFilesList IsNot Nothing Then attachedFilesList.SelectedIndex = -1
    End Sub

    Private Sub SetupAttachedGridColumnsOnce()
        If _attachedGridColumnsSetup OrElse ViewAttached Is Nothing Then Return
        _attachedGridColumnsSetup = True
        ViewAttached.Columns.Clear()
        Dim colFullPath As New DevExpress.XtraGrid.Columns.GridColumn() With {.Caption = "Full Path", .FieldName = "FullPath", .Visible = True}
        Dim colSize As New DevExpress.XtraGrid.Columns.GridColumn() With {.Caption = "Size", .FieldName = "FormattedSize", .Visible = True}
        Dim colType As New DevExpress.XtraGrid.Columns.GridColumn() With {.Caption = "Type", .FieldName = "Type", .Visible = True}
        _repoButtonOpen = New DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit()
        _repoButtonOpen.Buttons(0).Caption = "Open"
        _repoButtonOpen.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor
        AddHandler _repoButtonOpen.ButtonClick, AddressOf ViewAttached_OpenButtonClick
        Dim colOpen As New DevExpress.XtraGrid.Columns.GridColumn() With {
            .Caption = "Open",
            .ColumnEdit = _repoButtonOpen,
            .Visible = True,
            .ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowAlways
        }
        ViewAttached.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {colFullPath, colSize, colType, colOpen})
    End Sub

    Private Sub ViewAttached_OpenButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs)
        Dim row = TryCast(ViewAttached.GetFocusedRow(), NewAttachmentFileRow)
        If row Is Nothing OrElse String.IsNullOrEmpty(row.FullPath) OrElse Not File.Exists(row.FullPath) Then Return
        Try
            Process.Start(New ProcessStartInfo("explorer.exe", "/select,""" & row.FullPath & """") With {.UseShellExecute = True})
        Catch
        End Try
    End Sub

    Private Sub SetupImageFileContextMenus()
        If DesignMode Then Return
        If _ctxScannedFiles Is Nothing Then
            _ctxScannedFiles = New ContextMenuStrip()
            _mnuDeleteScannedFile = New ToolStripMenuItem()
            AddHandler _mnuDeleteScannedFile.Click, AddressOf MnuDeleteScannedFile_Click
            AddHandler _ctxScannedFiles.Opening, AddressOf CtxScannedFiles_Opening
            _ctxScannedFiles.Items.Add(_mnuDeleteScannedFile)
        End If
        ApplyContextMenuStripFont(_ctxScannedFiles)
        If scannedFilesList IsNot Nothing Then scannedFilesList.ContextMenuStrip = _ctxScannedFiles

        If _ctxAttachedFiles Is Nothing Then
            _ctxAttachedFiles = New ContextMenuStrip()
            _mnuDeleteAttachedFile = New ToolStripMenuItem()
            AddHandler _mnuDeleteAttachedFile.Click, AddressOf MnuDeleteAttachedFile_Click
            AddHandler _ctxAttachedFiles.Opening, AddressOf CtxAttachedFiles_Opening
            _ctxAttachedFiles.Items.Add(_mnuDeleteAttachedFile)
        End If
        ApplyContextMenuStripFont(_ctxAttachedFiles)
        If GridAttached IsNot Nothing Then GridAttached.ContextMenuStrip = _ctxAttachedFiles
        If attachedFilesList IsNot Nothing Then attachedFilesList.ContextMenuStrip = _ctxAttachedFiles
    End Sub

    Private Sub ApplyContextMenuStripFont(cms As ContextMenuStrip)
        If cms Is Nothing Then Return
        cms.Font = _acctContextMenuFont
        For Each item In cms.Items
            ApplyToolStripItemFontRecursive(item)
        Next
    End Sub

    Private Sub ApplyToolStripItemFontRecursive(item As ToolStripItem)
        If item Is Nothing Then Return
        item.Font = _acctContextMenuFont
        Dim m = TryCast(item, ToolStripMenuItem)
        If m IsNot Nothing AndAlso m.HasDropDownItems Then
            For Each subItem In m.DropDownItems
                ApplyToolStripItemFontRecursive(subItem)
            Next
        End If
    End Sub

    Private Sub ConfigureAccountingListTooltips()
        If DesignMode OrElse _accountingListTooltipsConfigured Then Return
        _accountingListTooltipsConfigured = True
        _scannedListToolTip.OwnerDraw = True
        _attachedListToolTip.OwnerDraw = True
        AddHandler _scannedListToolTip.Draw, AddressOf AccountingListToolTip_Draw
        AddHandler _scannedListToolTip.Popup, AddressOf AccountingListToolTip_Popup
        AddHandler _attachedListToolTip.Draw, AddressOf AccountingListToolTip_Draw
        AddHandler _attachedListToolTip.Popup, AddressOf AccountingListToolTip_Popup
        Const delayMs As Integer = 400
        _scannedListToolTip.AutomaticDelay = delayMs
        _scannedListToolTip.ReshowDelay = 120
        _scannedListToolTip.AutoPopDelay = 32000
        _attachedListToolTip.AutomaticDelay = delayMs
        _attachedListToolTip.ReshowDelay = 120
        _attachedListToolTip.AutoPopDelay = 32000
    End Sub

    Private Sub AccountingListToolTip_Popup(sender As Object, e As PopupEventArgs)
        Dim tt = TryCast(sender, ToolTip)
        If tt Is Nothing Then Return
        Dim text = tt.GetToolTip(e.AssociatedControl)
        If String.IsNullOrEmpty(text) Then Return
        Dim textSize = TextRenderer.MeasureText(text, _acctTooltipFont, New Size(440, 3000), TextFormatFlags.WordBreak)
        e.ToolTipSize = New Size(Math.Min(440, Math.Max(64, textSize.Width + 10)), Math.Max(18, textSize.Height + 8))
    End Sub

    Private Sub AccountingListToolTip_Draw(sender As Object, e As DrawToolTipEventArgs)
        e.DrawBackground()
        e.DrawBorder()
        Dim inset = New Rectangle(e.Bounds.X + 6, e.Bounds.Y + 4, Math.Max(1, e.Bounds.Width - 12), Math.Max(1, e.Bounds.Height - 8))
        TextRenderer.DrawText(e.Graphics, e.ToolTipText, _acctTooltipFont, inset,
            SystemColors.ControlText, TextFormatFlags.WordBreak Or TextFormatFlags.Left Or TextFormatFlags.Top)
    End Sub

    Private Sub RefreshImageDeleteMenuCaptions()
        If _mnuDeleteScannedFile IsNot Nothing Then
            _mnuDeleteScannedFile.Text = If(Eng, "Delete file…", "حذف الملف…")
            _mnuDeleteScannedFile.Font = _acctContextMenuFont
        End If
        If _mnuDeleteAttachedFile IsNot Nothing Then
            _mnuDeleteAttachedFile.Text = If(Eng, "Delete file…", "حذف الملف…")
            _mnuDeleteAttachedFile.Font = _acctContextMenuFont
        End If
    End Sub

    Private Sub CtxScannedFiles_Opening(sender As Object, e As CancelEventArgs)
        RefreshImageDeleteMenuCaptions()
        _scannedListContextIndex = -1
        If scannedFilesList Is Nothing OrElse _scannedFilePaths Is Nothing OrElse _scannedFilePaths.Count = 0 Then
            e.Cancel = True
            Return
        End If
        Dim pt = scannedFilesList.PointToClient(Control.MousePosition)
        _scannedListContextIndex = scannedFilesList.IndexFromPoint(pt)
        If _scannedListContextIndex < 0 OrElse _scannedListContextIndex >= _scannedFilePaths.Count Then e.Cancel = True
    End Sub

    Private Sub MnuDeleteScannedFile_Click(sender As Object, e As EventArgs)
        If _scannedListContextIndex < 0 OrElse _scannedListContextIndex >= _scannedFilePaths.Count Then Return
        Dim path = _scannedFilePaths(_scannedListContextIndex)
        If Not IsDeletablePatientImagePath(path) Then
            MessageBox.Show(If(Eng, "This file cannot be deleted from here.", "لا يمكن حذف هذا الملف من هنا."), If(Eng, "Delete", "حذف"), MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If
        Dim msgEn = "Delete this file permanently?" & vbCrLf & path
        Dim msgAr = "حذف هذا الملف نهائيا؟" & vbCrLf & path
        Dim chqDir = GetPatientScannedFolder()
        If Not String.IsNullOrWhiteSpace(chqDir) AndAlso IsPathUnderDirectorySafe(IO.Path.GetFullPath(path), IO.Path.GetFullPath(chqDir)) Then
            msgEn &= vbCrLf & vbCrLf & "This is in the cheque scans folder. The payment record is not deleted—only the image file."
            msgAr &= vbCrLf & vbCrLf & "الملف في مجلد مسح الشيكات؛ سجل الدفعة لا يُحذف—ملف الصورة فقط."
        End If
        If MessageBox.Show(If(Eng, msgEn, msgAr), If(Eng, "Confirm delete", "تأكيد الحذف"), MessageBoxButtons.YesNo, MessageBoxIcon.Warning) <> DialogResult.Yes Then Return
        Try
            If File.Exists(path) Then File.Delete(path)
        Catch ex As Exception
            MessageBox.Show(If(Eng, "Could not delete: ", "تعذر الحذف: ") & ex.Message, If(Eng, "Delete", "حذف"), MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End Try
        Dim onScannedTab = GridTabControl IsNot Nothing AndAlso GridTabControl.SelectedTabPage Is ScannedPage
        LoadPatientScannedFiles(applyDefaultSelection:=Not onScannedTab)
        LoadPatientAttachments()
        If onScannedTab Then SyncScannedPageToFocusedChequeRow()
        If GridTabControl IsNot Nothing AndAlso GridTabControl.SelectedTabPage Is AttachedPage Then SyncAttachedPageToFocusedChequeRow()
    End Sub

    Private Sub CtxAttachedFiles_Opening(sender As Object, e As CancelEventArgs)
        RefreshImageDeleteMenuCaptions()
        _attachedListContextIndex = -1
        Dim cms = TryCast(sender, ContextMenuStrip)
        If cms IsNot Nothing AndAlso attachedFilesList IsNot Nothing AndAlso cms.SourceControl Is attachedFilesList Then
            If _attachedFilePaths Is Nothing OrElse _attachedFilePaths.Count = 0 Then
                e.Cancel = True
                Return
            End If
            Dim pt = attachedFilesList.PointToClient(Control.MousePosition)
            _attachedListContextIndex = attachedFilesList.IndexFromPoint(pt)
            If _attachedListContextIndex < 0 OrElse _attachedListContextIndex >= _attachedFilePaths.Count Then e.Cancel = True
            Return
        End If
        If ViewAttached Is Nothing OrElse GridAttached Is Nothing Then
            e.Cancel = True
            Return
        End If
        Dim ptGrid = GridAttached.PointToClient(Control.MousePosition)
        Dim hi = ViewAttached.CalcHitInfo(ptGrid)
        If Not hi.InRow OrElse hi.RowHandle < 0 Then
            e.Cancel = True
            Return
        End If
        ViewAttached.FocusedRowHandle = hi.RowHandle
    End Sub

    Private Sub MnuDeleteAttachedFile_Click(sender As Object, e As EventArgs)
        Dim path As String = Nothing
        If _ctxAttachedFiles IsNot Nothing AndAlso attachedFilesList IsNot Nothing AndAlso _ctxAttachedFiles.SourceControl Is attachedFilesList Then
            If _attachedListContextIndex < 0 OrElse _attachedListContextIndex >= _attachedFilePaths.Count Then Return
            path = _attachedFilePaths(_attachedListContextIndex)
        Else
            If ViewAttached Is Nothing Then Return
            Dim row = TryCast(ViewAttached.GetFocusedRow(), NewAttachmentFileRow)
            If row Is Nothing OrElse String.IsNullOrWhiteSpace(row.FullPath) Then Return
            path = row.FullPath
        End If
        If Not IsDeletablePatientImagePath(path) Then
            MessageBox.Show(If(Eng, "This file cannot be deleted from here.", "لا يمكن حذف هذا الملف من هنا."), If(Eng, "Delete", "حذف"), MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If
        Dim msgEn = "Delete this file permanently?" & vbCrLf & path
        Dim msgAr = "حذف هذا الملف نهائيا؟" & vbCrLf & path
        If MessageBox.Show(If(Eng, msgEn, msgAr), If(Eng, "Confirm delete", "تأكيد الحذف"), MessageBoxButtons.YesNo, MessageBoxIcon.Warning) <> DialogResult.Yes Then Return
        Try
            If File.Exists(path) Then File.Delete(path)
        Catch ex As Exception
            MessageBox.Show(If(Eng, "Could not delete: ", "تعذر الحذف: ") & ex.Message, If(Eng, "Delete", "حذف"), MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End Try
        LoadPatientAttachments()
        If GridTabControl IsNot Nothing AndAlso GridTabControl.SelectedTabPage Is ScannedPage Then
            LoadPatientScannedFiles(applyDefaultSelection:=False)
            SyncScannedPageToFocusedChequeRow()
        ElseIf GridTabControl IsNot Nothing AndAlso GridTabControl.SelectedTabPage Is AttachedPage Then
            SyncAttachedPageToFocusedChequeRow()
        End If
    End Sub

    Private Shared Function IsPathUnderDirectorySafe(filePath As String, directoryPath As String) As Boolean
        Try
            Dim f = IO.Path.GetFullPath(filePath).TrimEnd(IO.Path.DirectorySeparatorChar, IO.Path.AltDirectorySeparatorChar)
            Dim d = IO.Path.GetFullPath(directoryPath).TrimEnd(IO.Path.DirectorySeparatorChar, IO.Path.AltDirectorySeparatorChar)
            If String.Equals(f, d, StringComparison.OrdinalIgnoreCase) Then Return True
            Return f.StartsWith(d & IO.Path.DirectorySeparatorChar, StringComparison.OrdinalIgnoreCase)
        Catch
            Return False
        End Try
    End Function

    ''' <summary>True if the file lives in this patient&apos;s Cheques folder or is a Patient{id}_* file under Attachments.</summary>
    Private Function IsDeletablePatientImagePath(candidatePath As String) As Boolean
        If String.IsNullOrWhiteSpace(candidatePath) OrElse CurrentPatient Is Nothing OrElse CurrentPatient.PatientID <= 0 Then Return False
        Try
            Dim full = IO.Path.GetFullPath(candidatePath)
            Dim chqDir = GetPatientScannedFolder()
            If Not String.IsNullOrWhiteSpace(chqDir) AndAlso IsPathUnderDirectorySafe(full, IO.Path.GetFullPath(chqDir)) Then Return True
            Dim attRoot = GetPatientAttachmentsFolder()
            If String.IsNullOrWhiteSpace(attRoot) Then Return False
            If Not IsPathUnderDirectorySafe(full, IO.Path.GetFullPath(attRoot)) Then Return False
            Return IO.Path.GetFileName(full).StartsWith("Patient" & CurrentPatient.PatientID.ToString() & "_", StringComparison.OrdinalIgnoreCase)
        Catch
            Return False
        End Try
    End Function

    ''' <summary>Formats byte count for display (e.g. "1.5 MB").</summary>
    Public Shared Function FormatFileSize(bytes As Long) As String
        If bytes < 1024 Then Return bytes.ToString() & " B"
        If bytes < 1024 * 1024 Then Return (bytes / 1024.0).ToString("N1") & " KB"
        If bytes < 1024 * 1024 * 1024 Then Return (bytes / (1024.0 * 1024)).ToString("N1") & " MB"
        Return (bytes / (1024.0 * 1024 * 1024)).ToString("N1") & " GB"
    End Function

    ''' <summary>Gets the folder path for patient cheque images: AppFolder\Images\Patient{PatientID}\Cheques (same as FrmChqPayAccnt).</summary>
    Private Function GetPatientScannedFolder() As String
        If CurrentPatient Is Nothing OrElse CurrentPatient.PatientID <= 0 Then Return ""
        Dim appDir = Application.StartupPath
        If String.IsNullOrEmpty(appDir) Then Return ""
        Return IO.Path.Combine(appDir, "Images", "Patient" & CurrentPatient.PatientID.ToString(), "Cheques")
    End Function

    Private Shared ReadOnly _chequeImageExtensions As String() = {".jpg", ".jpeg", ".png", ".bmp", ".gif", ".tif", ".tiff"}

    Private Shared Function IsChequeImageFile(path As String) As Boolean
        Dim ext = IO.Path.GetExtension(path)
        If String.IsNullOrEmpty(ext) Then Return False
        Return _chequeImageExtensions.Contains(ext.ToLowerInvariant())
    End Function

    ''' <summary>Primary linked scan (front first, then back, then legacy); see <see cref="ChequeImageLinkHelper.FindLinkedChequeImagePaths"/>.</summary>
    Private Function FindLinkedChequeImagePath(pay As Patient_Pays) As String
        If pay Is Nothing OrElse pay.PayID <= 0 Then Return Nothing
        If Not String.Equals(If(pay.PayType, ""), "Cheque", StringComparison.OrdinalIgnoreCase) Then Return Nothing
        Dim pid = If(pay.PatientID.HasValue, pay.PatientID.Value, 0)
        If pid <= 0 AndAlso CurrentPatient IsNot Nothing Then pid = CurrentPatient.PatientID
        Return ChequeImageLinkHelper.FindLinkedChequeImagePath(pay.PayID, pid, pay.ChqNumber)
    End Function

    ''' <summary>Linked cheque image paths for the payment row (existing files only).</summary>
    Private Function FindLinkedChequeImagePathsForPay(pay As Patient_Pays) As List(Of String)
        If pay Is Nothing OrElse pay.PayID <= 0 Then Return New List(Of String)
        If Not String.Equals(If(pay.PayType, ""), "Cheque", StringComparison.OrdinalIgnoreCase) Then Return New List(Of String)
        Dim pid = If(pay.PatientID.HasValue, pay.PatientID.Value, 0)
        If pid <= 0 AndAlso CurrentPatient IsNot Nothing Then pid = CurrentPatient.PatientID
        Return ChequeImageLinkHelper.FindLinkedChequeImagePaths(pay.PayID, pid, pay.ChqNumber).Where(Function(p) File.Exists(p)).ToList()
    End Function

    ''' <summary>Paths shown on Attached / File info tabs: linked cheque scans plus Attachments\Patient{id}_* files tagged with Pay{PayID}_Chq.</summary>
    Private Function CollectLinkedAttachmentPathsForPay(pay As Patient_Pays) As List(Of String)
        Dim result As New List(Of String)
        If pay Is Nothing OrElse pay.PayID <= 0 Then Return result
        Dim pid = If(pay.PatientID.HasValue, pay.PatientID.Value, 0)
        If pid <= 0 AndAlso CurrentPatient IsNot Nothing Then pid = CurrentPatient.PatientID
        If pid <= 0 Then Return result
        Dim seen As New HashSet(Of String)(StringComparer.OrdinalIgnoreCase)
        For Each p In FindLinkedChequeImagePathsForPay(pay)
            Dim key = IO.Path.GetFullPath(p)
            If seen.Add(key) Then result.Add(p)
        Next
        Dim payTag = "Pay" & pay.PayID.ToString() & "_Chq"
        Dim root = GetPatientAttachmentsFolder()
        If String.IsNullOrEmpty(root) OrElse Not Directory.Exists(root) Then
            Return result.OrderBy(Function(p) IO.Path.GetFileName(p), StringComparer.OrdinalIgnoreCase).ToList()
        End If
        Dim prefix = "Patient" & pid.ToString() & "_"
        For Each fullPath In Directory.GetFiles(root).Where(Function(f) IO.Path.GetFileName(f).StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
            If IO.Path.GetFileName(fullPath).IndexOf(payTag, StringComparison.OrdinalIgnoreCase) < 0 Then Continue For
            If Not File.Exists(fullPath) Then Continue For
            Dim key = IO.Path.GetFullPath(fullPath)
            If seen.Add(key) Then result.Add(fullPath)
        Next
        Return result.OrderBy(Function(p) IO.Path.GetFileName(p), StringComparer.OrdinalIgnoreCase).ToList()
    End Function

    Private Shared ReadOnly _chequeScanPlaceholderLock As New Object()
    Private Shared _chequeScanPlaceholderEn As Image
    Private Shared _chequeScanPlaceholderAr As Image

    Private Shared Function IsChequeScanPlaceholderImage(img As Image) As Boolean
        Return img IsNot Nothing AndAlso (Object.ReferenceEquals(img, _chequeScanPlaceholderEn) OrElse Object.ReferenceEquals(img, _chequeScanPlaceholderAr))
    End Function

    Private Shared Function GetChequeScanPlaceholderImage(english As Boolean) As Image
        SyncLock _chequeScanPlaceholderLock
            If english Then
                If _chequeScanPlaceholderEn Is Nothing Then
                    _chequeScanPlaceholderEn = CreateChequeScanPlaceholderBitmap("No image")
                End If
                Return _chequeScanPlaceholderEn
            Else
                If _chequeScanPlaceholderAr Is Nothing Then
                    _chequeScanPlaceholderAr = CreateChequeScanPlaceholderBitmap("لا توجد صورة")
                End If
                Return _chequeScanPlaceholderAr
            End If
        End SyncLock
    End Function

    Private Shared Function CreateChequeScanPlaceholderBitmap(caption As String) As Image
        Const w = 320, h = 200
        Dim bmp As New Bitmap(w, h, System.Drawing.Imaging.PixelFormat.Format24bppRgb)
        Using g = Graphics.FromImage(bmp)
            g.SmoothingMode = SmoothingMode.AntiAlias
            g.Clear(Color.FromArgb(244, 244, 244))
            Using bFill As New SolidBrush(Color.FromArgb(228, 228, 228))
                g.FillRectangle(bFill, 36, 36, w - 72, h - 80)
            End Using
            Using pBorder As New Pen(Color.FromArgb(180, 180, 180), 2)
                g.DrawRectangle(pBorder, 36, 36, w - 72 - 1, h - 80 - 1)
            End Using
            Using f As New Font(SystemFonts.MessageBoxFont.FontFamily, 12.0F, FontStyle.Regular, GraphicsUnit.Pixel)
                Using br As New SolidBrush(Color.FromArgb(120, 120, 120))
                    Dim sz = g.MeasureString(caption, f)
                    g.DrawString(caption, f, br, (w - sz.Width) / 2.0F, h - 48.0F)
                End Using
            End Using
        End Using
        Return bmp
    End Function

    ''' <summary>Shows a neutral placeholder when the focused cheque has no linked scan (or nothing is selected in the file list).</summary>
    Private Sub ShowNoLinkedChequeScanPreview()
        If scanPreview Is Nothing OrElse scanPreview.IsDisposed Then Return
        Dim ph = GetChequeScanPlaceholderImage(Eng)
        Dim oldImg = TryCast(scanPreview.Image, Image)
        scanPreview.Image = ph
        If oldImg IsNot Nothing AndAlso Not IsChequeScanPlaceholderImage(oldImg) Then oldImg.Dispose()
    End Sub

    ''' <summary>After load, select linked image for focused cheque row in ChqsPayGrid (list + preview).</summary>
    Private Sub SyncScannedListToFocusedChequeRow()
        If scannedFilesList Is Nothing OrElse ChqsGridView Is Nothing Then Return
        Dim pay = TryCast(ChqsGridView.GetFocusedRow(), Patient_Pays)
        If pay Is Nothing Then Return
        Dim path = FindLinkedChequeImagePath(pay)
        If String.IsNullOrWhiteSpace(path) Then Return
        Dim idx = _scannedFilePaths.FindIndex(Function(p) String.Equals(p, path, StringComparison.OrdinalIgnoreCase))
        If idx < 0 Then
            Dim fn = IO.Path.GetFileName(path)
            idx = _scannedFilePaths.FindIndex(Function(p) String.Equals(IO.Path.GetFileName(p), fn, StringComparison.OrdinalIgnoreCase))
        End If
        If idx < 0 OrElse idx >= scannedFilesList.Items.Count Then
            _suppressScannedListPreviewEvents = True
            Try
                scannedFilesList.SelectedIndex = -1
            Finally
                _suppressScannedListPreviewEvents = False
            End Try
            ShowScannedFilePreview(path)
            Return
        End If
        scannedFilesList.SelectedIndex = idx
    End Sub

    ''' <summary>Aligns Scanned file list + preview with ChqsPayGrid focus: only linked images; empty list + placeholder when no row or no links.</summary>
    Private Sub SyncScannedPageToFocusedChequeRow()
        If scannedFilesList Is Nothing OrElse ChqsGridView Is Nothing Then Return
        If GridTabControl Is Nothing OrElse GridTabControl.SelectedTabPage IsNot ScannedPage Then Return

        Dim pay = TryCast(ChqsGridView.GetFocusedRow(), Patient_Pays)
        If pay Is Nothing OrElse pay.PayID <= 0 Then
            _suppressScannedListPreviewEvents = True
            Try
                scannedFilesList.SelectedIndex = -1
            Finally
                _suppressScannedListPreviewEvents = False
            End Try
            ShowNoLinkedChequeScanPreview()
            Return
        End If

        If _scannedFilePaths.Count = 0 Then
            _suppressScannedListPreviewEvents = True
            Try
                scannedFilesList.SelectedIndex = -1
            Finally
                _suppressScannedListPreviewEvents = False
            End Try
            ShowNoLinkedChequeScanPreview()
            Return
        End If

        SyncScannedListToFocusedChequeRow()
    End Sub

    ''' <summary>Attached tab: list/preview + grid row for the focused cheque&apos;s linked files only; placeholder when empty.</summary>
    Private Sub SyncAttachedPageToFocusedChequeRow()
        If attachedFilesList Is Nothing OrElse ChqsGridView Is Nothing Then Return
        If GridTabControl Is Nothing OrElse GridTabControl.SelectedTabPage IsNot AttachedPage Then Return

        SyncAttachedGridToFocusedChequeRow()

        Dim pay = TryCast(ChqsGridView.GetFocusedRow(), Patient_Pays)
        If pay Is Nothing OrElse pay.PayID <= 0 Then
            _suppressAttachedListPreviewEvents = True
            Try
                attachedFilesList.SelectedIndex = -1
            Finally
                _suppressAttachedListPreviewEvents = False
            End Try
            ShowNoAttachFilePreview()
            Return
        End If

        If _attachedFilePaths.Count = 0 Then
            _suppressAttachedListPreviewEvents = True
            Try
                attachedFilesList.SelectedIndex = -1
            Finally
                _suppressAttachedListPreviewEvents = False
            End Try
            ShowNoAttachFilePreview()
            Return
        End If

        SyncAttachedListToFocusedChequeRow()
    End Sub

    Private Sub SyncAttachedGridToFocusedChequeRow()
        If ViewAttached Is Nothing OrElse ChqsGridView Is Nothing OrElse GridAttached Is Nothing Then Return
        Dim pay = TryCast(ChqsGridView.GetFocusedRow(), Patient_Pays)
        If pay Is Nothing OrElse pay.PayID <= 0 Then Return
        Dim linked = FindLinkedChequeImagePath(pay)
        Dim targetName = If(linked, "").Trim()
        If targetName.Length > 0 Then targetName = IO.Path.GetFileName(targetName)
        Dim payTag = "Pay" & pay.PayID.ToString() & "_Chq"
        Dim list = TryCast(GridAttached.DataSource, List(Of NewAttachmentFileRow))
        If list Is Nothing Then Return
        For i = 0 To list.Count - 1
            Dim fp = list(i).FullPath
            If String.IsNullOrEmpty(fp) Then Continue For
            Dim fn = IO.Path.GetFileName(fp)
            If targetName.Length > 0 AndAlso String.Equals(fn, targetName, StringComparison.OrdinalIgnoreCase) Then
                FocusAttachedRowHandle(i)
                Return
            End If
        Next
        For i = 0 To list.Count - 1
            Dim fp = list(i).FullPath
            If String.IsNullOrEmpty(fp) Then Continue For
            If IO.Path.GetFileName(fp).IndexOf(payTag, StringComparison.OrdinalIgnoreCase) >= 0 Then
                FocusAttachedRowHandle(i)
                Return
            End If
        Next
        If list.Count = 1 Then FocusAttachedRowHandle(0)
    End Sub

    Private Sub FocusAttachedRowHandle(listSourceIndex As Integer)
        Dim h = ViewAttached.GetRowHandle(listSourceIndex)
        If h < 0 Then Return
        ViewAttached.FocusedRowHandle = h
        ViewAttached.MakeRowVisible(h)
    End Sub

    Private Sub SyncAttachedListToFocusedChequeRow()
        If attachedFilesList Is Nothing OrElse ChqsGridView Is Nothing Then Return
        Dim pay = TryCast(ChqsGridView.GetFocusedRow(), Patient_Pays)
        If pay Is Nothing Then Return
        Dim path = FindLinkedChequeImagePath(pay)
        If String.IsNullOrWhiteSpace(path) AndAlso _attachedFilePaths IsNot Nothing AndAlso _attachedFilePaths.Count > 0 Then
            path = _attachedFilePaths(0)
        End If
        If String.IsNullOrWhiteSpace(path) Then Return
        Dim idx = _attachedFilePaths.FindIndex(Function(p) String.Equals(p, path, StringComparison.OrdinalIgnoreCase))
        If idx < 0 Then
            Dim fn = IO.Path.GetFileName(path)
            idx = _attachedFilePaths.FindIndex(Function(p) String.Equals(IO.Path.GetFileName(p), fn, StringComparison.OrdinalIgnoreCase))
        End If
        If idx < 0 OrElse idx >= attachedFilesList.Items.Count Then
            _suppressAttachedListPreviewEvents = True
            Try
                attachedFilesList.SelectedIndex = -1
            Finally
                _suppressAttachedListPreviewEvents = False
            End Try
            ShowAttachFilePreview(path)
            Return
        End If
        attachedFilesList.SelectedIndex = idx
    End Sub

    Private Sub GridTabControl_SelectedPageChanged(sender As Object, e As TabPageChangedEventArgs) Handles GridTabControl.SelectedPageChanged
        If e.Page Is Nothing Then Return
        If e.Page Is AllPaysPage OrElse e.Page Is CashGridPage OrElse e.Page Is ChqGridPage Then
            ApplyPaymentTabAndFilters()
        End If
        If e.Page Is ScannedPage Then
            _suppressScannedListPreviewEvents = True
            Try
                LoadPatientScannedFiles(applyDefaultSelection:=False)
            Finally
                _suppressScannedListPreviewEvents = False
            End Try
            SyncScannedPageToFocusedChequeRow()
        ElseIf e.Page Is AttachedPage Then
            LoadPatientAttachments()
            SyncAttachedPageToFocusedChequeRow()
        ElseIf e.Page Is FileInfoPage Then
            LoadPatientAttachments()
        End If
    End Sub

    Private Sub ChqsGridView_FocusedRowChanged(sender As Object, e As FocusedRowChangedEventArgs) Handles ChqsGridView.FocusedRowChanged
        If GridTabControl Is Nothing Then Return
        If GridTabControl.SelectedTabPage Is ScannedPage Then
            LoadPatientScannedFiles(applyDefaultSelection:=False)
            SyncScannedPageToFocusedChequeRow()
        ElseIf GridTabControl.SelectedTabPage Is AttachedPage Then
            LoadPatientAttachments()
            SyncAttachedPageToFocusedChequeRow()
        ElseIf GridTabControl.SelectedTabPage Is FileInfoPage Then
            LoadPatientAttachments()
        End If
    End Sub

    ''' <summary>Loads linked cheque scan images for the focused ChqsPayGrid row into scannedFilesList (ScannedPage). Empty when no row or no linked files.</summary>
    Private Sub LoadPatientScannedFiles(Optional applyDefaultSelection As Boolean = True)
        _scannedFilePaths.Clear()
        _lastScannedListTooltipText = ""
        If scannedFilesList IsNot Nothing Then _scannedListToolTip.SetToolTip(scannedFilesList, "")
        If scannedFilesList Is Nothing Then Return
        scannedFilesList.Items.Clear()
        If scanPreview IsNot Nothing AndAlso Not scanPreview.IsDisposed Then scanPreview.Image = Nothing
        If ChqsGridView Is Nothing Then Return
        Dim pay = TryCast(ChqsGridView.GetFocusedRow(), Patient_Pays)
        If pay Is Nothing OrElse pay.PayID <= 0 Then
            If scannedFilesList IsNot Nothing Then scannedFilesList.SelectedIndex = -1
            Return
        End If
        For Each fullPath In FindLinkedChequeImagePathsForPay(pay)
            _scannedFilePaths.Add(fullPath)
            scannedFilesList.Items.Add(IO.Path.GetFileName(fullPath))
        Next
        If Not applyDefaultSelection Then
            If scannedFilesList IsNot Nothing Then scannedFilesList.SelectedIndex = -1
            Return
        End If
        If _scannedFilePaths.Count > 0 Then
            scannedFilesList.SelectedIndex = 0
        Else
            scannedFilesList.SelectedIndex = -1
        End If
    End Sub

    Private Sub btnScan_Click(sender As Object, e As EventArgs) Handles btnScan.Click
        If CurrentPatient Is Nothing OrElse CurrentPatient.PatientID <= 0 Then
            Dim msgEn = "Please select a patient first."
            Dim msgAr = "يرجى اختيار المريض أولا."
            MessageBox.Show(If(Eng, msgEn, msgAr), "Scan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If
        Try
            Dim folder = GetPatientScannedFolder()
            If String.IsNullOrEmpty(folder) Then Return
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
                Dim msgNoScan = If(Eng, "No pages were scanned.", "لم يتم مسح أي صفحات.")
                MessageBox.Show(msgNoScan, "Scan", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return
            End If

            LoadPatientScannedFiles(applyDefaultSelection:=False)
            SyncScannedPageToFocusedChequeRow()
            Dim msgEn = $"Scanned {files.Count} page(s) to patient folder."
            Dim msgAr = "تم مسح " & files.Count & " صفحة/صفحات إلى مجلد المريض."
            MessageBox.Show(If(Eng, msgEn, msgAr), "Scan", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            Dim msgEn = "Scan failed: " & ex.Message
            Dim msgAr = "فشل المسح: " & ex.Message
            MessageBox.Show(If(Eng, msgEn, msgAr), "Scan", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnScanScan2Pages_Click(sender As Object, e As EventArgs) Handles btnScanScan2Pages.Click

    End Sub

    ''' <summary>Shows the image at the given path in the ScannedPage scanPreview control.</summary>
    Private Sub ShowScannedFilePreview(fullPath As String)
        If scanPreview Is Nothing OrElse scanPreview.IsDisposed OrElse String.IsNullOrEmpty(fullPath) Then Return
        If Not File.Exists(fullPath) Then Return
        Try
            Dim oldImg = TryCast(scanPreview.Image, Image)
            Using img As Image = Image.FromFile(fullPath)
                scanPreview.Image = New Bitmap(img)
            End Using
            If oldImg IsNot Nothing AndAlso Not IsChequeScanPlaceholderImage(oldImg) Then oldImg.Dispose()
        Catch
            scanPreview.Image = Nothing
        End Try
    End Sub

    Private Sub scannedFilesList_SelectedIndexChanged(sender As Object, e As EventArgs) Handles scannedFilesList.SelectedIndexChanged
        If _suppressScannedListPreviewEvents Then Return
        If scannedFilesList Is Nothing OrElse _scannedFilePaths Is Nothing Then Return
        Dim idx = scannedFilesList.SelectedIndex
        If idx < 0 OrElse idx >= _scannedFilePaths.Count Then
            ShowNoLinkedChequeScanPreview()
            Return
        End If
        ShowScannedFilePreview(_scannedFilePaths(idx))
    End Sub

    Private Sub scannedFilesList_MouseMove(sender As Object, e As MouseEventArgs) Handles scannedFilesList.MouseMove
        If scannedFilesList Is Nothing OrElse _scannedFilePaths Is Nothing OrElse _scannedFilePaths.Count = 0 Then
            If Not String.IsNullOrEmpty(_lastScannedListTooltipText) Then
                _lastScannedListTooltipText = ""
                _scannedListToolTip.SetToolTip(scannedFilesList, "")
            End If
            Return
        End If
        Dim idx = scannedFilesList.IndexFromPoint(e.Location)
        Dim newText As String = ""
        If idx >= 0 AndAlso idx < _scannedFilePaths.Count Then
            Dim path = _scannedFilePaths(idx)
            Dim sizeStr = ""
            Try
                Dim fi As New FileInfo(path)
                sizeStr = FormatFileSize(fi.Length)
            Catch
                sizeStr = "?"
            End Try
            newText = path & vbCrLf & If(Eng, "Size: ", "الحجم: ") & sizeStr
        End If
        If String.Equals(newText, _lastScannedListTooltipText, StringComparison.Ordinal) Then Return
        _lastScannedListTooltipText = newText
        _scannedListToolTip.SetToolTip(scannedFilesList, newText)
    End Sub

    Private Sub ShowNoAttachFilePreview()
        If attachPreview Is Nothing OrElse attachPreview.IsDisposed Then Return
        Dim ph = GetChequeScanPlaceholderImage(Eng)
        Dim oldImg = TryCast(attachPreview.Image, Image)
        attachPreview.Image = ph
        If oldImg IsNot Nothing AndAlso Not IsChequeScanPlaceholderImage(oldImg) Then oldImg.Dispose()
    End Sub

    ''' <summary>Shows the file in attachPreview when it is an image; otherwise a neutral placeholder.</summary>
    Private Sub ShowAttachFilePreview(fullPath As String)
        If attachPreview Is Nothing OrElse attachPreview.IsDisposed OrElse String.IsNullOrEmpty(fullPath) Then Return
        If Not File.Exists(fullPath) Then Return
        If Not IsChequeImageFile(fullPath) Then
            ShowNoAttachFilePreview()
            Return
        End If
        Try
            Dim oldImg = TryCast(attachPreview.Image, Image)
            Using img As Image = Image.FromFile(fullPath)
                attachPreview.Image = New Bitmap(img)
            End Using
            If oldImg IsNot Nothing AndAlso Not IsChequeScanPlaceholderImage(oldImg) Then oldImg.Dispose()
        Catch
            attachPreview.Image = Nothing
        End Try
    End Sub

    Private Sub attachedFilesList_SelectedIndexChanged(sender As Object, e As EventArgs) Handles attachedFilesList.SelectedIndexChanged
        If _suppressAttachedListPreviewEvents Then Return
        If attachedFilesList Is Nothing OrElse _attachedFilePaths Is Nothing Then Return
        Dim idx = attachedFilesList.SelectedIndex
        If idx < 0 OrElse idx >= _attachedFilePaths.Count Then
            ShowNoAttachFilePreview()
            Return
        End If
        ShowAttachFilePreview(_attachedFilePaths(idx))
    End Sub

    Private Sub attachedFilesList_MouseMove(sender As Object, e As MouseEventArgs) Handles attachedFilesList.MouseMove
        If attachedFilesList Is Nothing OrElse _attachedFilePaths Is Nothing OrElse _attachedFilePaths.Count = 0 Then
            If Not String.IsNullOrEmpty(_lastAttachedListTooltipText) Then
                _lastAttachedListTooltipText = ""
                _attachedListToolTip.SetToolTip(attachedFilesList, "")
            End If
            Return
        End If
        Dim idx = attachedFilesList.IndexFromPoint(e.Location)
        Dim newText As String = ""
        If idx >= 0 AndAlso idx < _attachedFilePaths.Count Then
            Dim path = _attachedFilePaths(idx)
            Dim sizeStr = ""
            Try
                Dim fi As New FileInfo(path)
                sizeStr = FormatFileSize(fi.Length)
            Catch
                sizeStr = "?"
            End Try
            newText = path & vbCrLf & If(Eng, "Size: ", "الحجم: ") & sizeStr
        End If
        If String.Equals(newText, _lastAttachedListTooltipText, StringComparison.Ordinal) Then Return
        _lastAttachedListTooltipText = newText
        _attachedListToolTip.SetToolTip(attachedFilesList, newText)
    End Sub




    Private Function GetSelectedTreatment() As Patient_Trts
        Dim currentTreatment As Patient_Trts = TryCast(Patient_TrtsBindingSource.Current, Patient_Trts)
        If currentTreatment IsNot Nothing Then Return currentTreatment
        If GridViewTrts IsNot Nothing AndAlso GridViewTrts.FocusedRowHandle >= 0 Then
            Return TryCast(GridViewTrts.GetRow(GridViewTrts.FocusedRowHandle), Patient_Trts)
        End If
        Return Nothing
    End Function

    Private Sub btAddPay_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAddPay.Click
        Try
            ' When Cash is selected, open FrmAddPayAccnt; when Cheque/Insurance/Other, open FrmChqPayAccnt
            Dim currentTreatment As Patient_Trts = GetSelectedTreatment()
            Dim addTrtId As Integer = If(currentTreatment IsNot Nothing, currentTreatment.TrtID, 0)
            Dim treatDetailForPay As String = If(currentTreatment IsNot Nothing, If(currentTreatment.Detail, ""), "")
            ' Index 0 (All Payments) or 1 (Cash): add as cash by default.
            If cboPayType.SelectedIndex <= 1 Then
                Dim F As New FrmAddPayAccnt With {.Treats = currentTreatment
                }
                F.LoadForAdd(PatientID, addTrtId, treatDetailForPay)

                If F.ShowDialog(Me) = DialogResult.OK Then
                    LoadSubDataDapper(PatientID)
                    RaiseEvent BalChanged(sender, New BalChangedEventArgs(PatientID, PatientName, Finished))
                    FormManager.Instance.CurrentForm.UpdatePatientBalance(CurrentPatient)
                End If
                Return
            End If
            If cboPayType.SelectedIndex >= 2 Then
                ' Cheque (2), Insurance (3), Credit Card (4), Transfer (5)
                Dim FChq As New FrmChqPayAccnt With {.Treats = currentTreatment
                }
                FChq.LoadForAdd(PatientID, addTrtId, treatDetailForPay, PayTypeLabels.EnglishForComboIndex(cboPayType.SelectedIndex))
                FChq.btnScan.Visible = False
                FChq.btnScanAndPay.Visible = True
                If FChq.ShowDialog(Me) = DialogResult.OK Then
                    LoadSubDataDapper(PatientID)
                    RaiseEvent BalChanged(sender, New BalChangedEventArgs(PatientID, PatientName, Finished))
                    FormManager.Instance.CurrentForm.UpdatePatientBalance(CurrentPatient)
                End If
                Return
            End If





            LoadSubDataDapper(PatientID)
            RaiseEvent BalChanged(sender, New BalChangedEventArgs(PatientID, PatientName, Finished))
            FormManager.Instance.CurrentForm.UpdatePatientBalance(CurrentPatient)
        Catch ex As SqlException
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub btnEditPay_Click(sender As Object, e As EventArgs) Handles btnEditPay.Click
        Try
            Me.Validate()
            Me.Patient_PaysBindingSource.EndEdit()

            Dim pay As Patient_Pays = Nothing
            If GridViewPays IsNot Nothing AndAlso GridViewPays.FocusedRowHandle >= 0 Then
                pay = TryCast(GridViewPays.GetRow(GridViewPays.FocusedRowHandle), Patient_Pays)
            End If
            If pay Is Nothing Then
                pay = TryCast(Patient_PaysBindingSource.Current, Patient_Pays)
            End If
            If pay Is Nothing Then
                MsgBox(If(Eng, "Please select a payment first.", "يرجى اختيار دفعة أولاً."))
                Return
            End If

            Dim treatDetail As String = ""
            Dim curTrt As Patient_Trts = GetSelectedTreatment()
            If curTrt IsNot Nothing AndAlso curTrt.TrtID = pay.TrtID Then
                treatDetail = If(curTrt.Detail, "")
            Else
                Using conn As New SqlConnection(DentistXDATA.GetConnection.ConnectionString)
                    conn.Open()
                    Dim d = conn.ExecuteScalar(Of String)("SELECT Detail FROM Patient_Trts WHERE TrtID = @TrtID", New With {.TrtID = pay.TrtID})
                    treatDetail = If(d, "")
                End Using
            End If

            If String.Equals(PayTypeLabels.NormalizeToEnglish(If(pay.PayType, "")), PayTypeLabels.CashEn, StringComparison.OrdinalIgnoreCase) Then
                Dim F As New FrmAddPayAccnt()
                F.LoadForEdit(pay, treatDetail)
                If F.ShowDialog(Me) = DialogResult.OK Then
                    LoadSubDataDapper(PatientID)
                    RaiseEvent BalChanged(sender, New BalChangedEventArgs(PatientID, PatientName, Finished))
                    FormManager.Instance.CurrentForm.UpdatePatientBalance(CurrentPatient)
                End If
            Else
                Dim FChq As New FrmChqPayAccnt()
                FChq.LoadForEdit(pay, treatDetail)
                FChq.btnScan.Visible = True
                FChq.btnScanAndPay.Visible = False
                If FChq.ShowDialog(Me) = DialogResult.OK Then
                    LoadSubDataDapper(PatientID)
                    RaiseEvent BalChanged(sender, New BalChangedEventArgs(PatientID, PatientName, Finished))
                    FormManager.Instance.CurrentForm.UpdatePatientBalance(CurrentPatient)
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    ''' <summary>Re-apply pay tab + combo row filters after binding.</summary>
    Private Sub ApplyPayTypeComboFilterIfReady()
        If cboPayType Is Nothing OrElse cboPayType.SelectedIndex < 0 Then Return
        ApplyPaymentTabAndFilters()
    End Sub

    ''' <summary>Cheque grid = cheques only; All tab = filtered by combo; Cash tab = cash only (ignores combo).</summary>
    Private Sub ApplyPaymentTabAndFilters()
        If GridTabControl Is Nothing OrElse cboPayType Is Nothing Then Return
        If GridViewPays Is Nothing OrElse AllPaysView Is Nothing OrElse ChqsGridView Is Nothing Then Return

        SetColumnViewPayTypeFilter(ChqsGridView, PayTypeLabels.ChequeEn, "شيك")

        Dim page = GridTabControl.SelectedTabPage
        If page Is AllPaysPage Then
            If cboPayType.SelectedIndex <= 0 Then
                SetColumnViewPayTypeFilter(AllPaysView)
            Else
                Select Case cboPayType.SelectedIndex
                    Case 1
                        SetColumnViewPayTypeFilter(AllPaysView, PayTypeLabels.CashEn, "نقدا")
                    Case 2
                        SetColumnViewPayTypeFilter(AllPaysView, PayTypeLabels.ChequeEn, "شيك")
                    Case 3
                        SetColumnViewPayTypeFilter(AllPaysView, PayTypeLabels.InsuranceEn, "تأمين")
                    Case 4
                        SetColumnViewPayTypeFilter(AllPaysView, PayTypeLabels.CreditCardEn, "بطاقة اعتماد")
                    Case 5
                        SetColumnViewPayTypeFilter(AllPaysView, PayTypeLabels.TransferEn, "تحويل")
                    Case Else
                        SetColumnViewPayTypeFilter(AllPaysView)
                End Select
            End If
            SetColumnViewPayTypeFilter(GridViewPays, PayTypeLabels.CashEn, "نقدا")
        ElseIf page Is CashGridPage Then
            SetColumnViewPayTypeFilter(GridViewPays, PayTypeLabels.CashEn, "نقدا")
        ElseIf page Is ChqGridPage Then
            SetColumnViewPayTypeFilter(GridViewPays, PayTypeLabels.CashEn, "نقدا")
        End If
    End Sub

    ''' <summary>Filters by PayType without ActiveFilterCriteria (DevExpress often cannot compile [PayType] for this BindingSource, especially when pays exist).</summary>
    Private Sub SetColumnViewPayTypeFilter(view As ColumnView, ParamArray matchValues As String())
        If view Is Nothing Then Return
        view.ActiveFilterCriteria = Nothing

        Dim target As HashSet(Of String) = Nothing
        If matchValues IsNot Nothing AndAlso matchValues.Length > 0 Then
            Dim hs As New HashSet(Of String)(StringComparer.OrdinalIgnoreCase)
            For Each v In matchValues
                If String.IsNullOrWhiteSpace(v) Then Continue For
                Dim n = PayTypeLabels.NormalizeToEnglish(v.Trim())
                If Not String.IsNullOrEmpty(n) Then hs.Add(n)
            Next
            If hs.Count > 0 Then target = hs
        End If

        If ReferenceEquals(view, ChqsGridView) Then
            _chqsPaysPayTypeFilter = target
        ElseIf ReferenceEquals(view, AllPaysView) Then
            _allPaysViewPayTypeFilter = target
        ElseIf ReferenceEquals(view, GridViewPays) Then
            _mainPaysPayTypeFilter = target
        Else
            Return
        End If

        view.RefreshData()
    End Sub

    Private Sub AllPaysView_CustomRowFilter(sender As Object, e As RowFilterEventArgs) Handles AllPaysView.CustomRowFilter
        ApplyPayTypeRowFilter(AllPaysView, e, _allPaysViewPayTypeFilter)
    End Sub

    Private Sub GridViewPays_CustomRowFilter(sender As Object, e As RowFilterEventArgs) Handles GridViewPays.CustomRowFilter
        ApplyPayTypeRowFilter(GridViewPays, e, _mainPaysPayTypeFilter)
    End Sub

    Private Sub ChqsGridView_CustomRowFilter(sender As Object, e As RowFilterEventArgs) Handles ChqsGridView.CustomRowFilter
        ApplyPayTypeRowFilter(ChqsGridView, e, _chqsPaysPayTypeFilter)
    End Sub

    Private Shared Sub ApplyPayTypeRowFilter(view As ColumnView, e As RowFilterEventArgs, allowedNormalized As HashSet(Of String))
        If allowedNormalized Is Nothing OrElse allowedNormalized.Count = 0 Then
            e.Visible = True
            e.Handled = True
            Return
        End If
        If e.ListSourceRow < 0 Then
            e.Visible = True
            e.Handled = True
            Return
        End If
        Dim row = TryCast(view.GetRow(e.ListSourceRow), Patient_Pays)
        If row Is Nothing Then
            e.Visible = False
            e.Handled = True
            Return
        End If
        Dim n = PayTypeLabels.NormalizeToEnglish(If(row.PayType, ""))
        e.Visible = allowedNormalized.Contains(n)
        e.Handled = True
    End Sub

    Private Sub cboPayType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboPayType.SelectedIndexChanged
        If GridTabControl Is Nothing OrElse cboPayType Is Nothing Then Return
        Select Case cboPayType.SelectedIndex
            Case 0
                GridTabControl.SelectedTabPage = AllPaysPage
            Case 1
                GridTabControl.SelectedTabPage = CashGridPage
            Case 2
                GridTabControl.SelectedTabPage = ChqGridPage
            Case 3, 4, 5
                GridTabControl.SelectedTabPage = AllPaysPage
            Case Else
                GridTabControl.SelectedTabPage = AllPaysPage
        End Select
        ApplyPaymentTabAndFilters()
    End Sub





    Private Sub GridViewTrts_CellValueChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles GridViewTrts.CellValueChanged
        Dim view = DirectCast(sender, DevExpress.XtraGrid.Views.Grid.GridView)
        Dim row = TryCast(view.GetRow(e.RowHandle), Patient_Trts)
        If row IsNot Nothing Then
            row.IsModified = True
        End If
        lblTotalTrts.Text = GetGridTotalTreatmentsBeforeDiscount().ToString("N2")
        lblTotalDisc.Text = GetGridTotalDiscount().ToString("N2")
    End Sub

    Private Sub TrtSavNav_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TrtSavNav.Click
        Try
            Me.Validate()
            Me.Patient_TrtsBindingSource.EndEdit()

            Dim trtList As IEnumerable(Of Patient_Trts) = TryCast(Me.Patient_TrtsBindingSource.List, IEnumerable(Of Patient_Trts))
            If trtList Is Nothing OrElse Not trtList.Any() Then
                MessageBox.Show("No treatments to save.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return
            End If

            Dim modifiedTrts = trtList.Where(Function(t) t.IsModified).ToList()
            If modifiedTrts.Count = 0 Then
                MessageBox.Show("No modified records to save.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return
            ElseIf modifiedTrts.Count > 0 Then
                Dim confirmMsgEn As String = $"You are about to save {modifiedTrts.Count} modified record(s). Do you want to proceed?"
                Dim confirmMsgAr As String = $"أنت على وشك حفظ {modifiedTrts.Count} سجل/سجلات معدلة. هل تريد المتابعة؟"
                Dim confirmMsg As String = If(Eng, confirmMsgEn, confirmMsgAr)
                Dim confirmTitleEn As String = "Confirm Save"
                Dim confirmTitleAr As String = "تأكيد الحفظ"
                Dim confirmTitle As String = If(Eng, confirmTitleEn, confirmTitleAr)
                If MessageBox.Show(confirmMsg, confirmTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question) <> DialogResult.Yes Then
                    Return
                End If
            End If

            Using conn As SqlConnection = DentistXDATA.GetConnection()
                conn.Open()

                Dim updateQuery As String =
                "UPDATE Patient_Trts SET 
                    PatientID = @PatientID, 
                    ToothTrtID = @ToothTrtID, 
                    OrthoID = @OrthoID, 
                    OtherTrtID = @OtherTrtID, 
                    Detail = @Detail, 
                    TrtDate = @TrtDate, 
                    TrtValue = @TrtValue, 
                    IsMultiTooth = @IsMultiTooth, 
                    Discount = @Discount, 
                    Discount2 = @Discount2, 
                    DiscountType = @DiscountType 
                 WHERE TrtID = @TrtID"

                For Each trt As Patient_Trts In modifiedTrts
                    conn.Execute(updateQuery, New With {
                    .PatientID = trt.PatientID,
                    .ToothTrtID = trt.ToothTrtID,
                    .OrthoID = trt.OrthoID,
                    .OtherTrtID = trt.OtherTrtID,
                    .Detail = trt.Detail,
                    .TrtDate = trt.TrtDate,
                    .TrtValue = trt.TrtValue,
                    .IsMultiTooth = trt.IsMultiTooth,
                    .Discount = trt.Discount,
                    .Discount2 = trt.Discount2,
                    .DiscountType = trt.DiscountType,
                    .TrtID = trt.TrtID
                })
                    trt.IsModified = False ' Reset flag after successful save
                Next
            End Using

            LoadSubDataDapper(PatientID)
            RaiseEvent BalChanged(sender, New BalChangedEventArgs(PatientID, PatientName, Finished))
            FormManager.Instance.CurrentForm.UpdatePatientBalance(CurrentPatient)

            MessageBox.Show($"{modifiedTrts.Count} record(s) saved successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

        Catch ex As SqlException
            MessageBox.Show("SQL Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As Exception
            MessageBox.Show("Unexpected Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub



    Private Sub btnResetPatientAccount_Click(sender As Object, e As EventArgs) Handles btnResetPatientAccount.Click
        Try
            Me.Validate()
            Me.Patient_TrtsBindingSource.EndEdit()

            Dim trt As Patient_Trts = CType(Me.Patient_TrtsBindingSource.Current, Patient_Trts)
            Dim trtData As New Patient_TrtsDATA
            If trt Is Nothing Then
                Dim noSelectionMsgEn As String = "No treatment selected to delete."
                Dim noSelectionMsgAr As String = "لم يتم تحديد علاج للحذف."
                Dim noSelectionMsg As String = If(Eng, noSelectionMsgEn, noSelectionMsgAr)
                Dim noSelectionTitleEn As String = "Error"
                Dim noSelectionTitleAr As String = "خطأ"
                Dim noSelectionTitle As String = If(Eng, noSelectionTitleEn, noSelectionTitleAr)
                MessageBox.Show(noSelectionMsg, noSelectionTitle, MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return
            End If

            ' First confirmation (standard MessageBox)
            Dim msgEn As String = "Are you sure you want to Reset This Patient Treatment Account?"
            Dim msgAr As String = "هل أنت متأكد أنك تريد تصفير حساب هذا " & CurrentPatient.PatientName & " المريض؟"
            Dim msg As String = If(Eng, msgEn, msgAr)
            Dim titleEn As String = "First Warning"
            Dim titleAr As String = "التحذير الأول"
            Dim title As String = If(Eng, titleEn, titleAr)
            If MessageBox.Show(msg, title, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) <> DialogResult.Yes Then
                Return
            End If

            ' Second confirmation (custom dialog)
            Dim confirmMsgEn As String = "FINAL WARNING: This will permanently delete All " & CurrentPatient.PatientName & " Treatments Account. Check the box to confirm."
            Dim confirmMsgAr As String = "تحذير نهائي: سيؤدي هذا إلى تصفير حساب " & CurrentPatient.PatientName & " المريض بشكل دائم. تحقق من المربع للتأكيد."
            Dim confirmMsg As String = If(Eng, confirmMsgEn, confirmMsgAr)
            Dim confirmTitleEn As String = "Patient Treatments Account Reset Form"
            Dim confirmTitleAr As String = "نموذج تصفير حساب علاجات المريض"
            Dim confirmTitle As String = If(Eng, confirmTitleEn, confirmTitleAr)

            Using confirmDialog As New DoubleConfirmDialog() With {.Text = confirmTitle}
                confirmDialog.Message = confirmMsg
                If confirmDialog.ShowDialog(Me) <> DialogResult.OK OrElse Not confirmDialog.IsConfirmed Then
                    Return ' Exit if user cancels or doesn't check the box
                End If
            End Using
            ' Proceed with deletion
            If trtData.DeleteAll(trt) Then
                Dim successMsgEn As String = "Treatment Account Reset successfully."
                Dim successMsgAr As String = "تم تصفير حساب المريض بنجاح."
                Dim successMsg As String = If(Eng, successMsgEn, successMsgAr)
                Dim successTitleEn As String = "Success"
                Dim successTitleAr As String = "نجاح"
                Dim successTitle As String = If(Eng, successTitleEn, successTitleAr)
                MessageBox.Show(successMsg, successTitle, MessageBoxButtons.OK, MessageBoxIcon.Information)
                LoadSubDataDapper(PatientID)
                RaiseEvent BalChanged(sender, New BalChangedEventArgs(PatientID, PatientName, Finished))
                FormManager.Instance.CurrentForm.UpdatePatientBalance(CurrentPatient)
            Else
                Dim errorMsgEn As String = "Failed to Reset treatment Account."
                Dim errorMsgAr As String = "فشل في تصفير حساب المريض."
                Dim errorMsg As String = If(Eng, errorMsgEn, errorMsgAr)
                Dim errorTitleEn As String = "Error"
                Dim errorTitleAr As String = "خطأ"
                Dim errorTitle As String = If(Eng, errorTitleEn, errorTitleAr)
                MessageBox.Show(errorMsg, errorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error)

            End If

        Catch ex As SqlException
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub btnTrtDel_Click(sender As Object, e As EventArgs) Handles btnTrtDel.Click
        Try
            Me.Validate()
            Me.Patient_TrtsBindingSource.EndEdit()

            Dim trt As Patient_Trts = CType(Me.Patient_TrtsBindingSource.Current, Patient_Trts)
            Dim trtData As New Patient_TrtsDATA
            If trt Is Nothing Then
                Dim noSelectionMsgEn As String = "No treatment selected to delete."
                Dim noSelectionMsgAr As String = "لم يتم تحديد علاج للحذف."
                Dim noSelectionMsg As String = If(Eng, noSelectionMsgEn, noSelectionMsgAr)
                Dim noSelectionTitleEn As String = "Error"
                Dim noSelectionTitleAr As String = "خطأ"
                Dim noSelectionTitle As String = If(Eng, noSelectionTitleEn, noSelectionTitleAr)
                MessageBox.Show(noSelectionMsg, noSelectionTitle, MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return
            End If

            ' Check if treatment is linked to an invoice (cannot delete)
            Dim hasInvoiceItems As Boolean = False
            Try
                Using conn As New SqlConnection(DentistXDATA.GetConnection.ConnectionString)
                    conn.Open()
                    Dim count = conn.ExecuteScalar(Of Integer)("SELECT COUNT(1) FROM Invoice_Items WHERE TrtID = @TrtID", New With {.TrtID = trt.TrtID})
                    hasInvoiceItems = (count > 0)
                End Using
            Catch
                ' If check fails, allow delete attempt; constraint error will be caught below
            End Try
            If hasInvoiceItems Then
                Dim msgEnI As String = "This treatment cannot be deleted because it is included in an invoice. Remove it from the invoice first, or delete the invoice."
                Dim msgArI As String = "لا يمكن حذف هذا العلاج لأنه مدرج في فاتورة. أزلّه من الفاتورة أولاً أو احذف الفاتورة."
                Dim titleEnI As String = "Cannot delete"
                Dim titleArI As String = "لا يمكن الحذف"
                MessageBox.Show(If(Eng, msgEnI, msgArI), If(Eng, titleEnI, titleArI), MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            ' First confirmation (standard MessageBox)
            Dim msgEn As String = "Are you sure you want to delete Treatment Account?"
            Dim msgAr As String = "هل أنت متأكد أنك تريد حذف هذا " & trt.Detail & " العلاج من الحساب؟"
            Dim msg As String = If(Eng, msgEn, msgAr)
            Dim titleEn As String = "First Warning"
            Dim titleAr As String = "التحذير الأول"
            Dim title As String = If(Eng, titleEn, titleAr)
            If MessageBox.Show(msg, title, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) <> DialogResult.Yes Then
                Return
            End If

            ' Second confirmation (custom dialog)
            Dim confirmMsgEn As String = "FINAL WARNING: This will permanently delete " & trt.Detail & " Account. Check the box to confirm."
            Dim confirmMsgAr As String = "تحذير نهائي: سيؤدي هذا إلى حذف حساب " & trt.Detail & " العلاج بشكل دائم. تحقق من المربع للتأكيد."
            Dim confirmMsg As String = If(Eng, confirmMsgEn, confirmMsgAr)
            Dim confirmTitleEn As String = "Patient Treatments Account Deletion Form"
            Dim confirmTitleAr As String = "نموذج حذف حساب علاجات المرضى"
            Dim confirmTitle As String = If(Eng, confirmTitleEn, confirmTitleAr)

            Using confirmDialog As New DoubleConfirmDialog() With {.Text = confirmTitle}
                confirmDialog.Message = confirmMsg
                If confirmDialog.ShowDialog(Me) <> DialogResult.OK OrElse Not confirmDialog.IsConfirmed Then
                    Return ' Exit if user cancels or doesn't check the box
                End If
            End Using
            ' Proceed with deletion
            Try
                If trtData.Delete(trt) Then
                    Dim successMsgEn As String = "Treatment Account deleted successfully."
                    Dim successMsgAr As String = "تم حذف حساب العلاج بنجاح."
                    Dim successMsg As String = If(Eng, successMsgEn, successMsgAr)
                    Dim successTitleEn As String = "Success"
                    Dim successTitleAr As String = "نجاح"
                    Dim successTitle As String = If(Eng, successTitleEn, successTitleAr)
                    MessageBox.Show(successMsg, successTitle, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    LoadSubDataDapper(PatientID)
                    RaiseEvent BalChanged(sender, New BalChangedEventArgs(PatientID, PatientName, Finished))
                    FormManager.Instance.CurrentForm.UpdatePatientBalance(CurrentPatient)
                Else
                    Dim errorMsgEn As String = "Failed to delete treatment Account."
                    Dim errorMsgAr As String = "فشل في حذف حساب العلاج."
                    Dim errorMsg As String = If(Eng, errorMsgEn, errorMsgAr)
                    Dim errorTitleEn As String = "Error"
                    Dim errorTitleAr As String = "خطأ"
                    Dim errorTitle As String = If(Eng, errorTitleEn, errorTitleAr)
                    MessageBox.Show(errorMsg, errorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If
            Catch ex As SqlException
                Dim friendlyEn As String = "This treatment cannot be deleted because it is included in an invoice. Remove it from the invoice first, or delete the invoice."
                Dim friendlyAr As String = "لا يمكن حذف هذا العلاج لأنه مدرج في فاتورة. أزلّه من الفاتورة أولاً أو احذف الفاتورة."
                If ex.Message.IndexOf("REFERENCE", StringComparison.OrdinalIgnoreCase) >= 0 AndAlso ex.Message.IndexOf("Invoice", StringComparison.OrdinalIgnoreCase) >= 0 Then
                    MessageBox.Show(If(Eng, friendlyEn, friendlyAr), If(Eng, "Cannot delete", "لا يمكن الحذف"), MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Else
                    MsgBox(ex.Message)
                End If
            End Try
        Catch ex As SqlException
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub GridViewPays_CellValueChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles GridViewPays.CellValueChanged, AllPaysView.CellValueChanged, ChqsGridView.CellValueChanged
        Dim view = DirectCast(sender, DevExpress.XtraGrid.Views.Grid.GridView)
        Dim row = TryCast(view.GetRow(e.RowHandle), Patient_Pays)
        If row IsNot Nothing Then
            row.IsModified = True
        End If
    End Sub
    Private Sub PaySavNav_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PaySavNav.Click
        Try
            Me.Validate()
            Me.Patient_PaysBindingSource.EndEdit()

            Dim payList As IEnumerable(Of Patient_Pays) = TryCast(Me.Patient_PaysBindingSource.List, IEnumerable(Of Patient_Pays))
            If payList Is Nothing OrElse Not payList.Any() Then
                MessageBox.Show("No payments to save.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return
            End If
            Dim modifiedPays = payList.Where(Function(p) p.IsModified).ToList()
            If modifiedPays.Count = 0 Then
                MessageBox.Show("No modified payment records to save.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return
            ElseIf modifiedPays.Count > 0 Then
                Dim confirmMsgEn As String = $"You are about to save {modifiedPays.Count} modified payment record(s). Do you want to proceed?"
                Dim confirmMsgAr As String = $"أنت على وشك حفظ {modifiedPays.Count} سجل/سجلات دفعات معدلة. هل تريد المتابعة؟"
                Dim confirmMsg As String = If(Eng, confirmMsgEn, confirmMsgAr)
                Dim confirmTitleEn As String = "Confirm Save"
                Dim confirmTitleAr As String = "تأكيد الحفظ"
                Dim confirmTitle As String = If(Eng, confirmTitleEn, confirmTitleAr)
                If MessageBox.Show(confirmMsg, confirmTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question) <> DialogResult.Yes Then
                    Return
                End If
            End If
            Using conn As SqlConnection = DentistXDATA.GetConnection()
                conn.Open()

                Dim updatePayQuery As String =
                "UPDATE Patient_Pays SET 
                    TrtID = @TrtID, 
                    PatientID = @PatientID, 
                    PayValue = @PayValue, 
                    PayDate = @PayDate, 
                    Notes = @Notes ,
                    PayType=@PayType,
                    ChqOwner=@ChqOwner,
                    AccountNumber=@AccountNumber,
                    ChqNumber=@ChqNumber,
                    ChqDueDate=@ChqDueDate,
                    ChqBank=@ChqBank,
                    IsCashed=@IsCashed,
                    InsuranceCompany=@InsuranceCompany,
                    InsuranceNotes=@InsuranceNotes,
                    IsForward=@IsForward,
                    ForwardFromTo=@ForwardFromTo,
                    ReceivedBy=@ReceivedBy,
                    IsReturned=@IsReturned
                 WHERE PayID = @PayID"
                For Each pay As Patient_Pays In modifiedPays
                    conn.Execute(updatePayQuery, New With {
                    .TrtID = pay.TrtID,
                    .PatientID = pay.PatientID,
                    .PayValue = pay.PayValue,
                    .PayDate = pay.PayDate,
                    .Notes = pay.Notes,
                    .PayType = PayTypeLabels.NormalizeToEnglish(pay.PayType),
                    .ChqOwner = pay.ChqOwner,
                    .AccountNumber = pay.AccountNumber,
                    .ChqNumber = pay.ChqNumber,
                    .ChqDueDate = pay.ChqDueDate,
                    .ChqBank = pay.ChqBank,
                    .IsCashed = pay.IsCashed,
                    .InsuranceCompany = pay.InsuranceCompany,
                    .InsuranceNotes = pay.InsuranceNotes,
                    .IsForward = pay.IsForward,
                    .ForwardFromTo = pay.ForwardFromTo,
                    .ReceivedBy = pay.ReceivedBy,
                    .IsReturned = pay.IsReturned,
                    .PayID = pay.PayID
                })
                    pay.IsModified = False ' Reset flag after successful save
                Next
            End Using

            LoadSubDataDapper(PatientID)
            GetTotals(CurrentPatient)
            RaiseEvent BalChanged(sender, New BalChangedEventArgs(PatientID, PatientName, Finished))
            FormManager.Instance.CurrentForm.UpdatePatientBalance(CurrentPatient)

            MessageBox.Show($"{modifiedPays.Count} payment record(s) saved successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

        Catch ex As SqlException
            MessageBox.Show("SQL Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As Exception
            MessageBox.Show("Unexpected Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnPayDel_Click(sender As Object, e As EventArgs) Handles btnPayDel.Click
        Try
            Me.Validate()
            Me.Patient_PaysBindingSource.EndEdit()

            Dim pay As Patient_Pays = CType(Me.Patient_PaysBindingSource.Current, Patient_Pays)
            If pay Is Nothing Then
                MsgBox("No payment selected to delete.")
                Exit Sub
            End If
            Dim payData As New Patient_PaysDATA

            If MsgBox("Are you sure you want to delete this Payment?", MsgBoxStyle.YesNo + MsgBoxStyle.Question) = MsgBoxResult.Yes Then
                If payData.Delete(pay) Then
                    MsgBox("Payment deleted successfully.")
                    LoadSubDataDapper(PatientID)
                    RaiseEvent BalChanged(sender, New BalChangedEventArgs(PatientID, PatientName, Finished))
                    FormManager.Instance.CurrentForm.UpdatePatientBalance(CurrentPatient)
                End If
            End If
        Catch ex As SqlException
            MsgBox(ex.Message)
        End Try
    End Sub


#End Region


#Region "Grids Number"
    Private Sub GridViewTrts_CustomUnboundColumnData(sender As Object, e As CustomColumnDataEventArgs) Handles GridViewTrts.CustomUnboundColumnData
        If e.Column.Name = "colRowNum" Then
            e.Value = e.ListSourceRowIndex + 1
        End If
    End Sub
    Private Sub GridViewPays_CustomUnboundColumnData(sender As Object, e As CustomColumnDataEventArgs) Handles GridViewPays.CustomUnboundColumnData
        If e.Column.Name = "colRowNum1" Then
            e.Value = e.ListSourceRowIndex + 1
        End If
    End Sub

    Private Sub GridViewPays_CustomColumnDisplayText(sender As Object, e As CustomColumnDisplayTextEventArgs) Handles GridViewPays.CustomColumnDisplayText
        If Eng OrElse e.Column Is Nothing OrElse e.Column.FieldName <> NameOf(Patient_Pays.PayType) Then Return
        Dim raw = If(e.Value, "").ToString()
        e.DisplayText = PayTypeLabels.ToArabic(PayTypeLabels.NormalizeToEnglish(raw))
    End Sub

    Private Sub ChqsGridView_CustomColumnDisplayText(sender As Object, e As CustomColumnDisplayTextEventArgs) Handles ChqsGridView.CustomColumnDisplayText
        If Eng OrElse e.Column Is Nothing OrElse e.Column.FieldName <> NameOf(Patient_Pays.PayType) Then Return
        Dim raw = If(e.Value, "").ToString()
        e.DisplayText = PayTypeLabels.ToArabic(PayTypeLabels.NormalizeToEnglish(raw))
    End Sub

    Private Sub ChqsGridView_CustomUnboundColumnData(sender As Object, e As CustomColumnDataEventArgs) Handles ChqsGridView.CustomUnboundColumnData
        If e.Column.Name = "colRowNum2" Then
            e.Value = e.ListSourceRowIndex + 1
        End If
    End Sub
    Private Sub AllPaysView_CustomUnboundColumnData(sender As Object, e As CustomColumnDataEventArgs) Handles AllPaysView.CustomUnboundColumnData
        If e.Column.Name = "colRowNum3" Then
            e.Value = e.ListSourceRowIndex + 1
        End If
    End Sub
#End Region



#Region "Invoicing"

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click

        Dim report As New FinancialReport()
        report.Parameters("parPatientID").Value = PatientID
        report.Parameters("parPatientID").Visible = False
        Dim printTool As New DevExpress.XtraReports.UI.ReportPrintTool(report)
        ' Get the Preview Form
        Dim previewForm As Form = printTool.PreviewForm
        ' Customize form settings
        previewForm.Size = New Size(1366, 768)
        previewForm.StartPosition = FormStartPosition.CenterScreen
        previewForm.WindowState = FormWindowState.Normal ' Or Normal / Minimized
        ' Show it
        printTool.ShowPreviewDialog()
    End Sub
    Private Sub btnInvoiceGen_Click(sender As Object, e As EventArgs) Handles btnInvoiceGen.Click
        Dim GenerateInvoice As New FormGenerateInvoice(CurrentPatient.PatientID)
        GenerateInvoice.Icon = GetIcon()
        GenerateInvoice.ShowDialog(Me)

        'LoadPatientTreatments()
        'CalculateTotals()
        'SaveInvoice()

    End Sub

    ' Load Treatments for Selected Patient - DevExpress GridControl version
    Private Sub LoadPatientTreatments()
        Try
            Using conn As New SqlConnection(DentistXDATA.GetConnection.ConnectionString)
                conn.Open()
                Dim query As String = "SELECT 
                                        pt.TrtID,
                                        pt.Detail,
                                        pt.TrtDate,
                                        CASE 
                                            WHEN EXISTS(SELECT 1 FROM Patient_Trts pt2 
                                                       WHERE pt2.TrtValue > 0 AND pt2.PatientID = pt.PatientID) 
                                            THEN pt.TrtValue
                                        END AS TrtValue,
                                        ISNULL(pt.Discount, 0) AS Discount,
                                        ISNULL(pt.Discount2, 0) AS Discount2,
                                        CASE 
                                            WHEN ii.TrtID IS NOT NULL THEN 1 
                                            ELSE 0 
                                        END AS IsInvoiced
                                        FROM Patient_Trts pt
                                        LEFT JOIN Invoice_Items ii ON ii.TrtID = pt.TrtID
                                        WHERE pt.PatientID = @PatientID 
                                            AND pt.TrtValue > 0
                                            AND ii.TrtID IS NULL  -- Only non-invoiced treatments
                                        ORDER BY pt.TrtDate DESC "

                Using cmd As New SqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@PatientID", PatientID)
                    Using reader As SqlDataReader = cmd.ExecuteReader()
                        selectedTreatments.Clear()

                        While reader.Read()
                            Dim isInvoiced As Boolean = reader("IsInvoiced")

                            Dim treatment As New TreatmentItem With {
                            .TrtID = reader("TrtID"),
                            .Detail = reader("Detail").ToString(),
                            .TrtDate = reader("TrtDate"),
                            .TrtValue = CDec(reader("TrtValue")),
                            .Discount = CDec(reader("Discount")),
                            .Discount2 = CDec(reader("Discount2")),
                            .IsInvoiced = isInvoiced,
                            .IsSelected = True ' Default not selected
                        }
                            selectedTreatments.Add(treatment)
                        End While
                    End Using
                End Using
            End Using

            ' Bind to GridControl
            CalculateTotals()

        Catch ex As Exception
            MessageBox.Show("Error loading treatments: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub


    ' Calculate Invoice Totals
    Private Sub CalculateTotals()
        Dim subTotal As Decimal = 0
        Dim totalDiscount As Decimal = 0
        Dim taxRate As Decimal = 0.16D ' 15% tax rate - adjust as needed
        Dim selectedCount As Integer = 0

        For Each treatment In selectedTreatments
            If treatment.IsSelected Then
                subTotal += treatment.TrtValue
                totalDiscount += treatment.Discount + treatment.Discount2
                selectedCount += 1
            End If
        Next

        Dim taxAmount As Decimal = (subTotal - totalDiscount) * taxRate
        Dim totalAmount As Decimal = 0
        'If chkTax.Checked Then
        '    totalAmount = (subTotal - totalDiscount) + taxAmount
        'Else
        '    totalAmount = (subTotal - totalDiscount)
        'End If


        '' Update display
        'lblSubTotal.Text = FormatCurrency(subTotal)
        'lblDiscount.Text = FormatCurrency(totalDiscount)
        'lblTaxAmount.Text = FormatCurrency(taxAmount)
        'lblTotalAmount.Text = FormatCurrency(totalAmount)
        'lblSelectedCount.Text = selectedCount.ToString() & " treatment(s) selected"
    End Sub

    ' Generate Invoice Number
    Private Function GenerateInvoiceNumber() As String
        Dim datePart As String = Date.Today.ToString("yyyyMMdd")
        Dim sequence As Integer = 1

        Try
            Using conn As New SqlConnection(DentistXDATA.GetConnection.ConnectionString)
                conn.Open()
                Dim query As String = "
                    SELECT COUNT(*) + 1 AS NextNumber 
                    FROM Invoices 
                    WHERE InvoiceNumber LIKE 'INV-" & datePart & "-%'"

                Using cmd As New SqlCommand(query, conn)
                    Dim result = cmd.ExecuteScalar()
                    If result IsNot Nothing AndAlso Not IsDBNull(result) Then
                        sequence = CInt(result)
                    End If
                End Using
            End Using
        Catch ex As Exception
            ' Table might not exist yet
            sequence = 1
        End Try

        Return $"INV-{datePart}-{sequence:000}"
    End Function

    Private Sub SaveInvoice()
        If selectedTreatments.Count = 0 Then
            MessageBox.Show("Either The treats have been invoiced or Treat Value is 0 ", "Error",
                                  MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If
        Dim invoiceID As Integer = 0
        Try
            Using conn As New SqlConnection(DentistXDATA.GetConnection.ConnectionString)
                conn.Open()
                Dim transaction As SqlTransaction = conn.BeginTransaction()

                Try
                    ' 1. Generate invoice number
                    Dim invoiceNumber As String = GenerateInvoiceNumber()

                    ' 2. Calculate totals
                    Dim subTotal As Decimal = 0
                    Dim totalDiscount As Decimal = 0
                    Dim taxRate As Decimal = 0.15D

                    For Each treatment In selectedTreatments
                        If treatment.IsSelected Then
                            subTotal += treatment.TrtValue
                            totalDiscount += treatment.Discount + treatment.Discount2
                        End If
                    Next

                    Dim taxAmount As Decimal = (subTotal - totalDiscount) * taxRate
                    Dim totalAmount As Decimal = (subTotal - totalDiscount) + taxAmount

                    ' 3. Insert invoice header
                    Dim insertInvoiceQuery As String = "
                        INSERT INTO Invoices 
                        (InvoiceNumber, PatientID, InvoiceDate, DueDate, InvoiceStatus, 
                         SubTotal, TaxAmount, DiscountAmount, TotalAmount, AmountPaid, BalanceDue, Notes, CreatedDate)
                        VALUES 
                        (@InvoiceNumber, @PatientID, @InvoiceDate, @DueDate, 1, 
                         @SubTotal, @TaxAmount, @DiscountAmount, @TotalAmount, 0, @TotalAmount, @Notes, GETDATE())
                        SELECT SCOPE_IDENTITY()"


                    Using cmd As New SqlCommand(insertInvoiceQuery, conn, transaction)
                        cmd.Parameters.AddWithValue("@InvoiceNumber", invoiceNumber)
                        cmd.Parameters.AddWithValue("@PatientID", PatientID)
                        cmd.Parameters.AddWithValue("@InvoiceDate", Now)
                        cmd.Parameters.AddWithValue("@DueDate", Now.AddDays(7))
                        cmd.Parameters.AddWithValue("@SubTotal", subTotal)
                        cmd.Parameters.AddWithValue("@TaxAmount", taxAmount)
                        cmd.Parameters.AddWithValue("@DiscountAmount", totalDiscount)
                        cmd.Parameters.AddWithValue("@TotalAmount", totalAmount)
                        cmd.Parameters.AddWithValue("@Notes", If(String.IsNullOrEmpty(""), DBNull.Value, ""))

                        invoiceID = CInt(cmd.ExecuteScalar())
                    End Using

                    ' 4. Insert invoice items
                    Dim insertItemQuery As String = "
                        INSERT INTO Invoice_Items 
                        (InvoiceID, TrtID, ItemDescription, Quantity, UnitPrice, Discount, TaxRate, LineTotal)
                        VALUES 
                        (@InvoiceID, @TrtID, @ItemDescription, 1, @UnitPrice, @Discount, @TaxRate, @LineTotal)"

                    For Each treatment In selectedTreatments
                        If treatment.IsSelected Then
                            Using cmd As New SqlCommand(insertItemQuery, conn, transaction)
                                cmd.Parameters.AddWithValue("@InvoiceID", invoiceID)
                                cmd.Parameters.AddWithValue("@TrtID", treatment.TrtID)
                                cmd.Parameters.AddWithValue("@ItemDescription", treatment.Detail)
                                cmd.Parameters.AddWithValue("@UnitPrice", treatment.TrtValue)
                                cmd.Parameters.AddWithValue("@Discount", treatment.Discount + treatment.Discount2)
                                cmd.Parameters.AddWithValue("@TaxRate", taxRate)
                                cmd.Parameters.AddWithValue("@LineTotal", treatment.TrtValue - treatment.Discount - treatment.Discount2)
                                cmd.ExecuteNonQuery()
                            End Using
                        End If
                    Next

                    ' 5. Add to invoice history
                    Dim insertHistoryQuery As String = "
                        INSERT INTO Invoice_History 
                        (InvoiceID, OldStatus, NewStatus, ChangeDate, ChangeReason)
                        VALUES 
                        (@InvoiceID, NULL, 1, GETDATE(), 'Invoice Created')"

                    Using cmd As New SqlCommand(insertHistoryQuery, conn, transaction)
                        cmd.Parameters.AddWithValue("@InvoiceID", invoiceID)
                        cmd.ExecuteNonQuery()
                    End Using

                    transaction.Commit()

                    MessageBox.Show($"Invoice #{invoiceNumber} created successfully!" & vbCrLf &
                                    $"Total Amount: {FormatCurrency(totalAmount)}",
                                    "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)



                Catch ex As Exception
                    transaction.Rollback()
                    Throw
                End Try
                Try
                    'Dim invoiceID As Integer = invoiceID 'GetCurrentInvoiceID() ' Get current invoice ID

                    ' Create and show report
                    Dim lang As String = If(Eng, "en", "ar")

                    Dim report As New rptPatientInvoice(invoiceID) ', lang)

                    ' Option 1: Show in Preview Form
                    Dim previewForm As New frmReportViewer(report)
                    previewForm.ShowDialog()

                    ' Option 2: Direct Print
                    ' report.Print()

                    ' Option 3: Export to PDF
                    ' report.ExportToPdf($"Invoice_{invoiceID}.pdf")

                Catch ex As Exception
                    MessageBox.Show("Error generating report: " & ex.Message, "Error",
                                   MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
            End Using
        Catch ex As Exception
            MessageBox.Show("Error saving invoice: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub btnEditTrt_Click(sender As Object, e As EventArgs) Handles btnEditTrt.Click
        Dim trt As Patient_Trts = GetSelectedTreatment()
        If trt Is Nothing Then
            MsgBox(If(Eng, "Please select a treatment row first.", "الرجاء اختيار صف المعالجة أولاً."))
            Return
        End If
        Dim F As New FrmAddTrtAccnt()
        F.SetWhatsFromHost(cboPrefix, txtWhats)
        F.LoadTreatment(trt)
        If F.ShowDialog(Me) <> DialogResult.OK Then Return
        Try
            Using conn As New SqlConnection(DentistXDATA.GetConnection.ConnectionString)
                conn.Open()
                Using trans As SqlTransaction = conn.BeginTransaction()
                    If clsPatient_TrtsData.UpdateTransactional(conn, trans, trt, trt) Then
                        trans.Commit()
                        Patient_TrtsBindingSource.ResetCurrentItem()
                        If CurrentPatient IsNot Nothing Then LoadDataRelation(CurrentPatient.PatientID)
                    End If
                End Using
            End Using
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        ' Refresh WhatsApp display in case user saved a new value from the dialog
        If CurrentPatient IsNot Nothing Then RefreshPatientWhatsAppDisplay(CurrentPatient.PatientID)
    End Sub

    Private Sub btnAddTreat_Click(sender As Object, e As EventArgs) Handles btnAddTreat.Click
        If CurrentPatient Is Nothing OrElse CurrentPatient.PatientID <= 0 Then
            MsgBox(If(Eng, "Please select a patient first.", "الرجاء اختيار مريض أولاً."))
            Return
        End If
        Dim F As New FrmAddTrtAccnt()
        F.SetWhatsFromHost(cboPrefix, txtWhats)
        F.LoadForAdd(CurrentPatient.PatientID)
        If F.ShowDialog(Me) <> DialogResult.OK Then Return
        If Not F.IsNewAdd OrElse F.NewTreatment Is Nothing Then Return
        Try
            Using conn As New SqlConnection(DentistXDATA.GetConnection.ConnectionString)
                conn.Open()
                Using trans As SqlTransaction = conn.BeginTransaction()
                    If clsPatient_TrtsData.AddTransactional(conn, trans, F.NewTreatment) Then
                        trans.Commit()
                        If CurrentPatient IsNot Nothing Then LoadDataRelation(CurrentPatient.PatientID)
                    End If
                End Using
            End Using
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        ' Refresh WhatsApp display in case user saved a new value from the dialog
        If CurrentPatient IsNot Nothing Then RefreshPatientWhatsAppDisplay(CurrentPatient.PatientID)
    End Sub

    Private Async Sub btnWhatsSend_Click(sender As Object, e As EventArgs) Handles btnWhatsSend.Click
        If CurrentPatient Is Nothing Then
            MsgBox(If(useEng, "Please select a patient.", "يرجى اختيار مريض."), MsgBoxStyle.Exclamation)
            Return
        End If
        _whatsBinder.SaveIfDirty()
        RefreshLblWhats()
        ' Use exactly the same number that is displayed in lblWhats
        ' (this already includes prefix + local digits, built by GetFullWhatsNumber/RefreshLblWhats).
        Dim number As String = ""
        If lblWhats IsNot Nothing AndAlso lblWhats.Text IsNot Nothing Then
            number = lblWhats.Text.ToString()
        Else
            number = GetFullWhatsNumber()
        End If

        Dim fullDigits As String = WhatsHelper.NormalizeWhatsDigits(number)
        Dim prefixDigitsUsed As String = WhatsHelper.GetPrefixDigitsFromCombo(cboPrefix)

        If String.IsNullOrWhiteSpace(fullDigits) Then
            MsgBox(If(useEng, "Enter patient WhatsApp/phone number (9 digits after country prefix / leading 0).", "أدخل رقم واتساب/الجوال (9 أرقام بعد الرمز أو الصفر)."), MsgBoxStyle.Exclamation)
            If txtWhats IsNot Nothing Then txtWhats.Focus()
            Return
        End If

        Dim validationMsg As String = ValidateWhatsAppNumber(fullDigits, prefixDigitsUsed)
        If validationMsg <> "" Then
            MsgBox(validationMsg, MsgBoxStyle.Exclamation)
            If txtWhats IsNot Nothing Then txtWhats.Focus()
            Return
        End If
        Dim trts As List(Of Patient_Trts)
        If Patient_TrtsBindingSource Is Nothing Then
            trts = New List(Of Patient_Trts)()
        Else
            trts = Patient_TrtsBindingSource.Cast(Of Object)().OfType(Of Patient_Trts)().ToList()
        End If
        Dim hasZeroValue = trts.Any(Function(t) t.TrtValue = 0)
        Dim excludeZero As Boolean = False
        If hasZeroValue Then
            Dim question = If(useEng,
                "Some treatments have value 0. Do you want to send all treatments (including value 0), or only treatments with value > 0?",
                "بعض العلاجات قيمتها 0. هل تريد إرسال كل العلاجات (بما فيها قيمة 0)، أم فقط العلاجات ذات قيمة أكبر من 0؟")
            Dim cap = If(useEng, "Send treatments", "إرسال العلاجات")
            Dim result = MessageBox.Show(question, cap, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question)
            If result = DialogResult.Cancel Then Return
            excludeZero = (result = DialogResult.No)
        End If

        Dim msg = BuildAccountingWhatsAppMessage(excludeZero, chkFullDetail.Checked)
        If String.IsNullOrWhiteSpace(msg) Then
            MsgBox(If(useEng, "No treatments or payments to send.", "لا توجد علاجات أو دفعات لإرسالها."), MsgBoxStyle.Information)
            Return
        End If

        If Not Await WhatsAppService.EnsureWhatsAppConnectedOrNotifyAsync(Me) Then Return
        Dim clinicId As String = WhatsAppService.GetCurrentClinicId()

        btnWhatsSend.Enabled = False
        _whatsBinder.BeginSuppressAutoSaveWhileModal()
        Try
            Dim waService As New WhatsAppService()
            Dim ctx As New WhatsAppSendContext With {
                .Category = WhatsAppMessageCategories.Accounting,
                .PatientId = CurrentPatient.PatientID,
                .DisplayName = CurrentPatient.PatientName,
                .SourceHint = NameOf(NewAccounting),
                .RevealMessageCenter = True
            }
            Await waService.SendMessageAsync(clinicId, number, msg, "", ctx)
            MsgBox(If(useEng, "Message queued for sending.", "تم وضع الرسالة في الطابور للإرسال."), MsgBoxStyle.Information)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical)
        Finally
            _whatsBinder.EndSuppressAutoSaveAndSync()
            btnWhatsSend.Enabled = True
        End Try
    End Sub

    ''' <summary>Delegates to WhatsHelper.BuildAccountingWhatsAppMessage (shared with Navigator Whats and background services).</summary>
    Private Function BuildAccountingWhatsAppMessage(Optional excludeZeroValueTreatments As Boolean = False, Optional fullBody As Boolean = False) As String
        If CurrentPatient Is Nothing OrElse Patient_TrtsBindingSource Is Nothing Then Return ""
        Dim trts = Patient_TrtsBindingSource.Cast(Of Object)().OfType(Of Patient_Trts)().ToList()
        If excludeZeroValueTreatments Then trts = trts.Where(Function(t) t.TrtValue > 0).ToList()
        Dim pays = WhatsHelper.CollectDedupedOrderedPaysFromTreatments(trts)
        Return WhatsHelper.BuildAccountingWhatsAppMessage(
            CurrentPatient.PatientName,
            trts,
            pays,
            excludeZeroValueTreatments:=False,
            useArabic:=Not useEng,
            fullBody,
            CurrentPatient.Sex)
    End Function

    Dim useEng As Boolean = False
    Private Sub RadioLang_SelectedIndexChanged(sender As Object, e As EventArgs) Handles RadioLang.SelectedIndexChanged
        If RadioLang.SelectedIndex = 0 Then
            useEng = False
        ElseIf RadioLang.SelectedIndex = 1 Then
            useEng = True
        End If
        RefreshImageDeleteMenuCaptions()
    End Sub








#End Region

End Class

''' <summary>Row for the Attached files grid (FullPath, Size, Type, Open).</summary>
Public Class NewAttachmentFileRow
    Public Property FullPath As String
    Public Property Size As Long
    Public Property Type As String
    ''' <summary>Formatted size for display (e.g. "1.5 MB").</summary>
    Public ReadOnly Property FormattedSize As String
        Get
            Return NewAccounting.FormatFileSize(Size)
        End Get
    End Property
End Class
