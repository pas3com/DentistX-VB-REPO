Imports System.Collections.Concurrent
Imports System.ComponentModel
Imports System.Data.SqlClient
Imports System.Diagnostics
Imports System.IO
Imports System.Linq
Imports TwainLib
Imports Dapper
Imports DevExpress.XtraEditors
Imports DevExpress.XtraGrid.Views.Base
Imports Infragistics.Win.UltraWinGrid
Imports Infragistics.Win.UltraWinEditors

Public Class Accounting
    Implements IPatientAwareUserControl

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        StoreOriginalBounds(Me)
        ' Enable double buffering to reduce flickering
        Me.DoubleBuffered = True
        SetStyle(ControlStyles.AllPaintingInWmPaint Or
                 ControlStyles.UserPaint Or
                 ControlStyles.DoubleBuffer, True)
        UpdateStyles()
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
    End Sub

    Private ReadOnly controlBoundsCache As New ConcurrentDictionary(Of Control, Rectangle)
    Private Const OriginalPanelWidth As Integer = 1214
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
    Private Sub ResizeControlsProportionally()
        If controlBoundsCache.IsEmpty Then Return
        Dim sw As New Stopwatch
        sw.Start()
        Dim widthRatio = CSng(Me.Width) / OriginalPanelWidth
        Dim heightRatio = CSng(Me.Height) / OriginalPanelHeight

        For Each kvp In controlBoundsCache
            kvp.Key.SetBounds(
            CInt(kvp.Value.X * widthRatio),
            CInt(kvp.Value.Y * heightRatio),
            CInt(kvp.Value.Width * widthRatio),
            CInt(kvp.Value.Height * heightRatio))
        Next
        sw.Stop()
    End Sub
    Private Sub Accounting_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        If controlBoundsCache.IsEmpty Then Return
        ResizeControlsProportionally()
    End Sub

    Private Sub Accounting_Load(sender As Object, e As EventArgs) Handles Me.Load
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
        IntegerMoneyGridColumns.ApplyIntegerPayTrtEditors(Patient_TrtsGridControl)
        IntegerMoneyGridColumns.ApplyIntegerPayTrtEditors(Patient_PaysGridControl)
        IntegerMoneyGridColumns.ApplyIntegerPayTrtEditors(ChqsPayGrid)

        PayDate.EditValue = Date.Now
        If btnTrtDel IsNot Nothing AndAlso btnTrtDel.Image Is Nothing Then
            btnTrtDel.Image = Global.DentistX.My.Resources.Resources.tbtnDelete
        End If
        If btnPayDel IsNot Nothing AndAlso btnPayDel.Image Is Nothing Then
            btnPayDel.Image = Global.DentistX.My.Resources.Resources.tbtnDelete
        End If

        RefreshLblWhats()
        AttachInfragisticsEditorEnterHandlers(Me)
    End Sub

    ''' <summary>Stops Infragistics editors from keeping "select all on focus" so the user can position the caret with the mouse (same as XtraForm3 / DevExpress behavior).</summary>
    Private Sub AttachInfragisticsEditorEnterHandlers(root As Control)
        If root Is Nothing Then Return
        For Each ctrl As Control In root.Controls
            Dim numEd As UltraNumericEditor = TryCast(ctrl, UltraNumericEditor)
            If numEd IsNot Nothing Then
                AddHandler numEd.Enter, AddressOf InfragisticsEditor_Enter
            Else
                Dim txtEd As UltraTextEditor = TryCast(ctrl, UltraTextEditor)
                If txtEd IsNot Nothing Then AddHandler txtEd.Enter, AddressOf InfragisticsEditor_Enter
            End If
            If ctrl.HasChildren Then AttachInfragisticsEditorEnterHandlers(ctrl)
        Next
    End Sub

    Private Sub InfragisticsEditor_Enter(sender As Object, e As EventArgs)
        If Control.MouseButtons <> MouseButtons.None Then
            Dim ctrl As Control = TryCast(sender, Control)
            If ctrl IsNot Nothing Then ctrl.BeginInvoke(New Action(Sub() ClearSelection(sender)))
        Else
            CollapseSelectionToEnd(sender)
        End If
    End Sub

    Private Sub ClearSelection(sender As Object)
        Dim numEd As UltraNumericEditor = TryCast(sender, UltraNumericEditor)
        If numEd IsNot Nothing Then
            numEd.SelectionLength = 0
            Return
        End If
        Dim txtEd As UltraTextEditor = TryCast(sender, UltraTextEditor)
        If txtEd IsNot Nothing Then txtEd.SelectionLength = 0
    End Sub

    Private Sub CollapseSelectionToEnd(sender As Object)
        Dim numEd As UltraNumericEditor = TryCast(sender, UltraNumericEditor)
        If numEd IsNot Nothing Then
            numEd.SelectionStart = numEd.SelectionLength
            Return
        End If
        Dim txtEd As UltraTextEditor = TryCast(sender, UltraTextEditor)
        If txtEd IsNot Nothing Then txtEd.SelectionStart = txtEd.SelectionLength
    End Sub

    Private Sub Accounting_Validated(sender As Object, e As EventArgs) Handles Me.Validated
        TrtDate.EditValue = Date.Now
    End Sub



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
    Private _attachedGridColumnsSetup As Boolean = False
    Private _repoButtonOpen As DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit

#End Region


#Region "Grid select"
    Private Sub btnEditGrid_CheckedChanged(sender As Object, e As EventArgs) Handles btnEditGrid.CheckedChanged
        If btnEditGrid.Checked Then
            'Patient_TrtsGridControl.Enabled = True
            'Patient_PaysGridControl.Enabled = True
            SetTrtRowValuesToControls()
        Else
            'Patient_TrtsGridControl.Enabled = False
            'Patient_PaysGridControl.Enabled = False
            DetailText.ResetText()
            TrtDate.ResetText()
            TrtValue.Value = 0D
        End If
    End Sub
    Private Sub GridViewTrts_FocusedRowChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles GridViewTrts.FocusedRowChanged
        If btnEditGrid.Checked Then
            SetTrtRowValuesToControls()
        End If
        Dim detail As Object = GridViewTrts.GetFocusedRowCellValue("Detail") 'colRowNum
        Dim RowNum As Object = GridViewTrts.GetFocusedRowCellValue("colRowNum") 'colRowNum
        lblTreat.Text = $"{If(RowNum IsNot Nothing, RowNum.ToString(), "")} : {If(detail IsNot Nothing, detail.ToString(), "")}"
    End Sub
    Private Sub SetTrtRowValuesToControls()
        Try
            ' Ensure a valid row is focused
            If GridViewTrts.FocusedRowHandle < 0 Then Exit Sub

            ' Get field values
            Dim trtID As Object = GridViewTrts.GetFocusedRowCellValue("TrtID")
            Dim detail As Object = GridViewTrts.GetFocusedRowCellValue("Detail")
            Dim trt_Date As Object = GridViewTrts.GetFocusedRowCellValue("TrtDate")
            Dim trt_Value As Object = GridViewTrts.GetFocusedRowCellValue("TrtValue")
            Dim trt_Disc As Object = GridViewTrts.GetFocusedRowCellValue("Discount")
            ' Assign to your controls (replace with your actual control names)
            'txtTrtID.Text = If(trtID IsNot Nothing, trtID.ToString(), "")
            DetailText.Text = If(detail IsNot Nothing, detail.ToString(), "")
            TrtDate.EditValue = If(trt_Date IsNot Nothing, trt_Date, Nothing)
            TrtValue.Text = If(trt_Value IsNot Nothing, Convert.ToDecimal(trt_Value).ToString("N0"), Nothing)
            'AndAlso IsNumeric(TrtValue)
            TrtDiscount.Text = If(trt_Disc IsNot Nothing, Convert.ToDecimal(trt_Disc).ToString("N0"), Nothing)
        Catch ex As Exception
            MessageBox.Show("Error setting values from grid: " & ex.Message)
        End Try
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

        ' Direct assignment if properties are non-nullable Double
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
            grpTrtDet.Text = If(Eng, $"Treatments Detaials For Patient {_patient.PatientName}", $"تفاصيل العلاجات للمريض {_patient.PatientName}")
            grpPayDet.Text = If(Eng, $"Payments Detaials For Patient {_patient.PatientName}", $"تفاصيل الدفعات للمريض {_patient.PatientName}")

            Using conn As SqlConnection = DentistXDATA.GetConnection
                conn.Open()

                ' --- Load parent (treatments) ---
                Dim allPatientTrts = conn.Query(Of Patient_Trts)(
                "SELECT TrtID, PatientID, ToothTrtID, OrthoID, OtherTrtID, Detail, TrtDate, TrtValue, IsMultiTooth, Discount, Discount2, DiscountType FROM dbo.Patient_Trts WHERE PatientID = @PatientID",
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
                 WHERE PatientID = @PatientID   ORDER BY [PayDate] DESC",
                New With {.PatientId = _patient.PatientID}).ToList()

                ' --- Link them manually ---
                For Each trt In patientTrts
                    trt.Patient_PaysIEnumerable = patientPays.Where(Function(p) p.TrtID = trt.TrtID).ToList()
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
                ApplyChqsPayGridFilter()
                LoadPatientScannedFiles()
                LoadPatientAttachments()

                ' Prefix + local Whats (same as NewAccounting / visits)
                If txtWhats IsNot Nothing AndAlso cboPrefix IsNot Nothing Then
                    Try
                        FillCboPrefixOnce()
                        WhatsHelper.BindPatientWhatsPrefixAndLocal(cboPrefix, txtWhats, _patient)
                        RefreshLblWhats()
                    Catch
                    End Try
                End If

                ' Totals or other logic
                GetTotals(_patient)

            End Using
            RaiseEvent BalChanged(Me, New BalChangedEventArgs(patientID, PatientName, Finished))
            FormManager.Instance.CurrentForm.UpdatePatientBalance(CurrentPatient)
            Finished = True
        Catch ex As SqlException
            MsgBox(ex.Message)
        Finally
            DetailText.ResetText()
            TrtDate.ResetText()
            TrtValue.Value = 0D
        End Try
    End Sub

    Public Sub LoadDataDapper(ByVal patientID As Integer)
        Try
            Dim _patient = CurrentPatient
            If _patient Is Nothing Then Exit Sub
            Finished = False
            grpTrtDet.Text = If(Eng, $"Treatments Detaials For Patient {_patient.PatientName}", $"تفاصيل العلاجات للمريض {_patient.PatientName}")
            grpPayDet.Text = If(Eng, $"Payments Detaials For Patient {_patient.PatientName}", $"تفاصيل الدفعات للمريض {_patient.PatientName}")
            Using conn As SqlConnection = DentistXDATA.GetConnection
                conn.Open() 'TrtValue
                ' Load Patient UnPaid Treatments
                Dim unpaidTrts = conn.Query(Of Patient_Trts)("SELECT ToothTrtID, PatientID, TreatDate, Treat,  IsExternal, IsPaid FROM dbo.Patient_ToothTrt
                                                                                    WHERE PatientID = @PatientID", New With {.PatientId = _patient.PatientID})
                ' Load Patient Treatments
                Dim patientTrts = conn.Query(Of Patient_Trts)("SELECT TrtID ,PatientID ,ToothTrtID ,OrthoID ,OtherTrtID ,Detail ,TrtDate ,TrtValue ,IsMultiTooth ,Discount ,DiscountType
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
                        showAsChildChck.Text = "Show Related Payments"

                        Dim patientPays = conn.Query(Of Patient_Pays)(
                "SELECT [PayID],[TrtID],[PatientID],[PayValue],[PayDate],[Notes],[PayType],[ChqOwner],[AccountNumber],[ChqNumber],[ChqDueDate],[ChqBank],[IsCashed],[InsuranceCompany],[InsuranceNotes],[IsForward],[ForwardFromTo],[ReceivedBy],[IsReturned] FROM dbo.Patient_Pays WHERE PatientID = @PatientID  ORDER BY [PayDate] DESC",
                New With {.PatientId = CurrentPatient.PatientID}
            )

                        Patient_PaysBindingSource.DataSource = patientPays.ToList()

                    Else
                        ' ❌ Show only related payments (activate parent/child relation)
                        showAsChildChck.Text = "Show All Payments"

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
                ApplyChqsPayGridFilter()
                LoadPatientScannedFiles()
                LoadPatientAttachments()

                ' ... rest of your code ...
                GetTotals(_patient)
            End Using

            Finished = True
        Catch ex As SqlException
            MsgBox(ex.Message)
        Finally
            DetailText.ResetText()
            TrtDate.ResetText()
            TrtValue.Value = 0D
        End Try
    End Sub
    Public Sub LoadSubDataDapper(ByVal patientID As Integer)
        Dim filterIndex = If(RadioFilter IsNot Nothing, RadioFilter.SelectedIndex, 0)
        LoadDataRelation(patientID, filterIndex)

        Dim currentTreatment As Patient_Trts = GetSelectedTreatment()
        TrtID = If(currentTreatment IsNot Nothing, currentTreatment.TrtID, 0)

        If CurrentPatient IsNot Nothing AndAlso CurrentPatient.PatientID = patientID Then
            GetTotals(CurrentPatient)
        End If
    End Sub


    Private Sub btnModifyValue_Click(sender As Object, e As EventArgs) Handles btnModifyValue.Click

        Me.Validate()
        Me.Patient_TrtsBindingSource.EndEdit()
        Dim trtsData As New Patient_TrtsDATA
        Dim trt As Patient_Trts = CType(Me.Patient_TrtsBindingSource.Current, Patient_Trts)
        If trt IsNot Nothing Then
            If trt.ToothTrtID.HasValue AndAlso trt.ToothTrtID.Value > 0 Then
                ' Handle ToothTrtID
                Try
                    Using conn As SqlConnection = DentistXDATA.GetConnection
                        conn.Execute("UPDATE Patient_Trts SET PatientID = @PatientID, ToothTrtID = @ToothTrtID,
                                         Detail = @Detail, TrtDate = @TrtDate, TrtValue = @TrtValue,
                                         Discount = @Discount, Discount2 = @Discount2 WHERE TrtID = @TrtID",
                                                                New With {
                                                                    .PatientID = trt.PatientID,
                                                                    .ToothTrtID = trt.ToothTrtID,
                                                                    .Detail = trt.Detail,
                                                                    .TrtDate = trt.TrtDate,
                                                                    .TrtValue = Val(TrtValue.Text),
                                                                    .Discount = Val(TrtDiscount.Text),
                                                                    .Discount2 = If(trt.Discount2.HasValue, trt.Discount2.Value, DBNull.Value),
                                                                    .TrtID = trt.TrtID
                                                                })
                        conn.Execute("UPDATE Patient_ToothTrt SET IsPaid = 1 WHERE ToothTrtID = @ToothTrtID",
                                                             New With {
                                                                 .ToothTrtID = trt.ToothTrtID
                                                             })
                    End Using

                    LoadSubDataDapper(PatientID)
                    RaiseEvent BalChanged(sender, New BalChangedEventArgs(PatientID, PatientName, Finished))
                    FormManager.Instance.CurrentForm.UpdatePatientBalance(CurrentPatient)
                    Me.TrtDate.ResetText()
                    TrtValue.Value = 0D
                    TrtDiscount.Value = 0D
                    DetailText.ResetText()
                Catch ex As SqlException
                    MsgBox(ex.Message)
                End Try
            ElseIf trt.OtherTrtID.HasValue AndAlso trt.OtherTrtID.Value > 0 Then
                ' Handle OtherTrtID
                Try

                    Using conn As SqlConnection = DentistXDATA.GetConnection
                        conn.Execute("UPDATE Patient_Trts SET PatientID = @PatientID, OtherTrtID = @OtherTrtID,
                                         Detail = @Detail, TrtDate = @TrtDate, TrtValue = @TrtValue,
                                         Discount = @Discount, Discount2 = @Discount2 WHERE TrtID = @TrtID",
                                                    New With {
                                                        .PatientID = trt.PatientID,
                                                        .OtherTrtID = trt.OtherTrtID,
                                                        .Detail = trt.Detail,
                                                        .TrtDate = trt.TrtDate,
                                                        .TrtValue = Val(TrtValue.Text),
                                                        .Discount = Val(TrtDiscount.Text),
                                                        .Discount2 = If(trt.Discount2.HasValue, trt.Discount2.Value, DBNull.Value),
                                                        .TrtID = trt.TrtID
                                                    })
                        conn.Execute("UPDATE Patient_ToothTrt SET IsPaid = 1 WHERE OtherTrtID = @OtherTrtID",
                                                 New With {
                                                     .OtherTrtID = trt.OtherTrtID
                                                 })
                    End Using

                    LoadSubDataDapper(PatientID)
                    RaiseEvent BalChanged(sender, New BalChangedEventArgs(PatientID, PatientName, Finished))
                    FormManager.Instance.CurrentForm.UpdatePatientBalance(CurrentPatient)
                    Me.TrtDate.ResetText()
                    TrtValue.Value = 0D
                    TrtDiscount.Value = 0D
                    DetailText.ResetText()
                Catch ex As SqlException
                    MsgBox(ex.Message)
                End Try
            ElseIf trt.OrthoID.HasValue AndAlso trt.OrthoID.Value > 0 Then
                ' Handle OrthoID
                Try

                    Using conn As SqlConnection = DentistXDATA.GetConnection
                        conn.Execute("UPDATE Patient_Trts SET PatientID = @PatientID, OrthoID = @OrthoID,
                                         Detail = @Detail, TrtDate = @TrtDate, TrtValue = @TrtValue,
                                         Discount = @Discount, Discount2 = @Discount2 WHERE TrtID = @TrtID",
                                                        New With {
                                                            .PatientID = trt.PatientID,
                                                            .OrthoID = trt.OrthoID,
                                                            .Detail = trt.Detail,
                                                            .TrtDate = trt.TrtDate,
                                                            .TrtValue = Val(TrtValue.Text),
                                                            .Discount = Val(TrtDiscount.Text),
                                                            .Discount2 = If(trt.Discount2.HasValue, trt.Discount2.Value, DBNull.Value),
                                                            .TrtID = trt.TrtID
                                                        })
                        'conn.Execute("UPDATE Patient_ToothTrt SET IsPaid = 1 WHERE OrthoID = @OrthoID",
                        '                             New With {
                        '                                 .OrthoID = trt.OrthoID
                        '                             })
                    End Using

                    LoadSubDataDapper(PatientID)
                    RaiseEvent BalChanged(sender, New BalChangedEventArgs(PatientID, PatientName, Finished))
                    FormManager.Instance.CurrentForm.UpdatePatientBalance(CurrentPatient)
                    DetailText.ResetText()
                    Me.TrtDate.ResetText()
                    TrtValue.Value = 0D
                    TrtDiscount.Value = 0D

                Catch ex As SqlException
                    MsgBox(ex.Message)
                End Try

            End If
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
                showAsChildChck.Text = "Show Related Payments"

                Using conn As SqlConnection = DentistXDATA.GetConnection
                    conn.Open()
                    Dim patientPays = conn.Query(Of Patient_Pays)(
                    "SELECT [PayID],[TrtID],[PatientID],[PayValue],[PayDate],[Notes],[PayType],[ChqOwner],[AccountNumber],[ChqNumber],[ChqDueDate],[ChqBank],[IsCashed],[InsuranceCompany],[InsuranceNotes],[IsForward],[ForwardFromTo],[ReceivedBy],[IsReturned] FROM dbo.Patient_Pays WHERE PatientID = @PatientID  ORDER BY [PayDate] DESC",
                    New With {.PatientId = CurrentPatient.PatientID}
                )

                    Patient_PaysBindingSource.DataSource = patientPays.ToList()
                End Using
            Else
                ' ❌ Show only related payments (activate parent/child relation)
                showAsChildChck.Text = "Show All Payments"

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
            ApplyChqsPayGridFilter()
        Catch ex As Exception
            MsgBox("Error switching payment mode: " & ex.Message)
        End Try
    End Sub

    ''' <summary>Apply filter so ChqsPayGrid displays only rows where PayType='Cheque'.</summary>
    Private Sub ApplyChqsPayGridFilter()
        If ChqsGridView Is Nothing Then Return
        ChqsGridView.ActiveFilterString = "PayType = 'Cheque'"
    End Sub

    Private Sub Patient_TrtsBindingSource_CurrentChanged(sender As Object, e As EventArgs) Handles Patient_TrtsBindingSource.CurrentChanged
        If showAsChildChck.Checked AndAlso CurrentPatient IsNot Nothing Then
            If Patient_TrtsBindingSource.Current IsNot Nothing Then
                Dim currentTreatment As Patient_Trts = CType(Patient_TrtsBindingSource.Current, Patient_Trts)

                Using conn As SqlConnection = DentistXDATA.GetConnection
                    conn.Open()
                    Dim payments = conn.Query(Of Patient_Pays)(
                                                "SELECT PayID, TrtID, PatientID, PayValue, PayDate, Notes " &
                                                "FROM dbo.Patient_Pays " &
                                                "WHERE PatientID = @PatientID AND TrtID = @TrtID  ORDER BY [PayDate] DESC",
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
    End Sub


#End Region


#Region "Events"

    Private Sub EnsureScanPreview()
        If _scanPreview IsNot Nothing AndAlso Not _scanPreview.IsDisposed Then
            UpdateScanPreviewLayout()
            Return
        End If

        _scanPreview = New PictureEdit() With {
            .Name = "picScanPreview",
            .Size = New Size(200, 140),
            .BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        }
        _scanPreview.Properties.ReadOnly = True
        _scanPreview.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Never
        _scanPreview.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom
        _scanPreview.Properties.AllowFocused = False

        PayMainDetPanel.Controls.Add(_scanPreview)
        UpdateScanPreviewLayout()

        If Not _scanPreviewResizeHooked Then
            AddHandler PayMainDetPanel.Resize, AddressOf PayMainDetPanel_Resize
            _scanPreviewResizeHooked = True
        End If
    End Sub

    Private Sub PayMainDetPanel_Resize(sender As Object, e As EventArgs)
        UpdateScanPreviewLayout()
    End Sub

    Private Sub UpdateScanPreviewLayout()
        If _scanPreview Is Nothing OrElse _scanPreview.IsDisposed Then Return

        Dim margin As Integer = 8
        Dim left As Integer = PayMainDetPanel.ClientSize.Width - _scanPreview.Width - margin
        Dim top As Integer = margin

        If btnScan IsNot Nothing Then
            top = btnScan.Bottom + margin
            If top + _scanPreview.Height > PayMainDetPanel.ClientSize.Height Then
                top = Math.Max(margin, PayMainDetPanel.ClientSize.Height - _scanPreview.Height - margin)
            End If
        End If

        If left < margin Then left = margin
        If top < margin Then top = margin

        _scanPreview.Location = New Point(left, top)
        _scanPreview.BringToFront()
    End Sub

    Private Sub SetScanPreviewImage(imagePath As String)
        If String.IsNullOrWhiteSpace(imagePath) OrElse Not File.Exists(imagePath) Then Return
        EnsureScanPreview()

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

    Private Async Sub btnWhatsSend_Click(sender As Object, e As EventArgs) Handles btnWhatsSend.Click
        If CurrentPatient Is Nothing Then
            MsgBox(If(Eng, "Please select a patient.", "يرجى اختيار مريض."), MsgBoxStyle.Exclamation)
            Return
        End If
        Dim number As String = If(txtWhats IsNot Nothing, txtWhats.Text, "").Trim()

        ' Normalize using selected country prefix:
        ' - Extract digits from cboPrefix (ignore + and text)
        ' - If the local number has length 10, strip leading 0
        ' - Prepend prefix digits to get a 12-digit international number
        If cboPrefix IsNot Nothing AndAlso cboPrefix.EditValue IsNot Nothing Then
            Dim rawPrefix As String = cboPrefix.EditValue.ToString()
            Dim prefixDigits As String = New String(rawPrefix.Where(Function(ch) Char.IsDigit(ch)).ToArray())

            If Not String.IsNullOrWhiteSpace(prefixDigits) Then
                Dim localDigits As String = New String(number.Where(Function(ch) Char.IsDigit(ch)).ToArray())
                If localDigits.Length = 10 AndAlso localDigits.StartsWith("0"c) Then
                    localDigits = localDigits.Substring(1)
                End If
                If localDigits.Length > 0 Then
                    number = prefixDigits & localDigits
                End If
            End If
        End If
        If String.IsNullOrWhiteSpace(number) Then
            MsgBox(If(Eng, "Enter patient WhatsApp/phone number.", "أدخل رقم واتساب/الجوال للمريض."), MsgBoxStyle.Exclamation)
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
            Dim question = If(Eng,
                "Some treatments have value 0. Do you want to send all treatments (including value 0), or only treatments with value > 0?",
                "بعض العلاجات قيمتها 0. هل تريد إرسال كل العلاجات (بما فيها قيمة 0)، أم فقط العلاجات ذات قيمة أكبر من 0؟")
            Dim cap = If(Eng, "Send treatments", "إرسال العلاجات")
            Dim result = MessageBox.Show(question, cap, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question)
            If result = DialogResult.Cancel Then Return
            excludeZero = (result = DialogResult.No)
        End If

        Dim msg = BuildAccountingWhatsAppMessage(excludeZero)
        If String.IsNullOrWhiteSpace(msg) Then
            MsgBox(If(Eng, "No treatments or payments to send.", "لا توجد علاجات أو دفعات لإرسالها."), MsgBoxStyle.Information)
            Return
        End If

        If Not Await WhatsAppService.EnsureWhatsAppConnectedOrNotifyAsync(Me) Then Return
        Dim clinicId As String = WhatsAppService.GetCurrentClinicId()

        Try
            btnWhatsSend.Enabled = False
            Dim waService As New WhatsAppService()
            Dim ctx As New WhatsAppSendContext With {
                .Category = WhatsAppMessageCategories.Accounting,
                .PatientId = CurrentPatient.PatientID,
                .DisplayName = CurrentPatient.PatientName,
                .SourceHint = NameOf(Accounting),
                .RevealMessageCenter = True
            }
            Await waService.SendMessageAsync(clinicId, number, msg, "", ctx)
            'MsgBox(If(Eng, "Message queued for sending.", "تم وضع الرسالة في الطابور للإرسال."), MsgBoxStyle.Information)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical)
        Finally
            btnWhatsSend.Enabled = True
        End Try
    End Sub

    ''' <summary>Delegates to WhatsHelper.BuildAccountingWhatsAppMessage (same format as NewAccounting).</summary>
    Private Function BuildAccountingWhatsAppMessage(Optional excludeZeroValueTreatments As Boolean = False) As String
        If CurrentPatient Is Nothing OrElse Patient_TrtsBindingSource Is Nothing Then Return ""
        Dim trts = Patient_TrtsBindingSource.Cast(Of Object)().OfType(Of Patient_Trts)().ToList()
        If excludeZeroValueTreatments Then trts = trts.Where(Function(t) t.TrtValue > 0).ToList()
        Dim pays = WhatsHelper.CollectDedupedOrderedPaysFromTreatments(trts)
        Return WhatsHelper.BuildAccountingWhatsAppMessage(
            CurrentPatient.PatientName,
            trts,
            pays,
            excludeZeroValueTreatments:=False,
            useArabic:=Not Eng,
            fullBody:=True,
            patientSex:=CurrentPatient.Sex)
    End Function

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

            Dim imagesRoot = Path.Combine(Application.StartupPath, "Images")
            Directory.CreateDirectory(imagesRoot)

            Dim savedCount As Integer = 0
            Dim lastPatientFolder As String = ""
            For Each srcPath In ofd.FileNames
                Dim destPath = BuildPatientAttachmentPath(imagesRoot, PatientID, srcPath)
                Dim patientFolder = Path.GetDirectoryName(destPath)
                If Not String.IsNullOrEmpty(patientFolder) AndAlso Not Directory.Exists(patientFolder) Then
                    Directory.CreateDirectory(patientFolder)
                End If
                File.Copy(srcPath, destPath, True)
                savedCount += 1
                lastPatientFolder = patientFolder
            Next

            Dim targetInfoPath As String = If(String.IsNullOrEmpty(lastPatientFolder),
                                              imagesRoot,
                                              lastPatientFolder)
            Dim msgEn = $"Saved {savedCount} file(s) to:{vbCrLf}{targetInfoPath}"
            Dim msgAr = $"تم حفظ {savedCount} ملف/ملفات في:{vbCrLf}{targetInfoPath}"
            MessageBox.Show(If(Eng, msgEn, msgAr), "Attachments", MessageBoxButtons.OK, MessageBoxIcon.Information)
            LoadPatientAttachments()
        End Using
    End Sub

    Private Function BuildPatientAttachmentPath(rootFolder As String, patientId As Integer, sourcePath As String) As String
        Dim ext = Path.GetExtension(sourcePath)
        Dim baseName = Path.GetFileNameWithoutExtension(sourcePath)
        Dim safeBase = SanitizeFileName(baseName)

        ' Root is the Images folder; inside it, create Patient{PatientID}\Attachs
        Dim patientFolder = Path.Combine(rootFolder, "Patient" & patientId.ToString())
        Dim attachFolder = Path.Combine(patientFolder, "Attachs")

        Dim baseFileName = $"Atch{patientId}"
        ' Keep a hint of original name to make files easier to recognize, but follow the Atch{PatientID} pattern.
        If Not String.IsNullOrWhiteSpace(safeBase) Then
            baseFileName &= "_" & safeBase
        End If

        Dim fileName = baseFileName & ext
        Dim fullPath = Path.Combine(attachFolder, fileName)

        If Not File.Exists(fullPath) Then Return fullPath

        Dim counter As Integer = 1
        Do
            fileName = $"{baseFileName}_{counter}{ext}"
            fullPath = Path.Combine(attachFolder, fileName)
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

    ''' <summary>Attachments folder: AppFolder\Images\Patient{PatientID}\Attachs. Files are named Atch{PatientID}_*.</summary>
    Private Function GetPatientAttachmentsFolder() As String
        If CurrentPatient Is Nothing OrElse CurrentPatient.PatientID <= 0 Then Return ""
        Dim appDir = Application.StartupPath
        If String.IsNullOrEmpty(appDir) Then Return ""
        Return Path.Combine(appDir,
                            "Images",
                            "Patient" & CurrentPatient.PatientID.ToString(),
                            "Attachs")
    End Function

    ''' <summary>Loads the current patient's attached files into GridAttached (AttachedPage).</summary>
    Private Sub LoadPatientAttachments()
        If GridAttached Is Nothing OrElse ViewAttached Is Nothing Then Return
        SetupAttachedGridColumnsOnce()
        Dim list As New List(Of AttachmentFileRow)
        If CurrentPatient Is Nothing OrElse CurrentPatient.PatientID <= 0 Then
            GridAttached.DataSource = list
            Return
        End If
        Dim folder = GetPatientAttachmentsFolder()
        If String.IsNullOrEmpty(folder) OrElse Not Directory.Exists(folder) Then
            GridAttached.DataSource = list
            Return
        End If
        Dim prefix = "Atch" & CurrentPatient.PatientID.ToString()
        For Each fullPath In Directory.GetFiles(folder).
            Where(Function(f) Path.GetFileName(f).StartsWith(prefix, StringComparison.OrdinalIgnoreCase)).
            OrderBy(Function(f) Path.GetFileName(f))
            Try
                Dim fi As New FileInfo(fullPath)
                list.Add(New AttachmentFileRow With {
                    .FullPath = fullPath,
                    .Size = fi.Length,
                    .Type = fi.Extension
                })
            Catch
            End Try
        Next
        GridAttached.DataSource = list
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
        Dim row = TryCast(ViewAttached.GetFocusedRow(), AttachmentFileRow)
        If row Is Nothing OrElse String.IsNullOrEmpty(row.FullPath) OrElse Not File.Exists(row.FullPath) Then Return
        Try
            Process.Start(New ProcessStartInfo("explorer.exe", "/select,""" & row.FullPath & """") With {.UseShellExecute = True})
        Catch
        End Try
    End Sub

    ''' <summary>Formats byte count for display (e.g. "1.5 MB").</summary>
    Public Shared Function FormatFileSize(bytes As Long) As String
        If bytes < 1024 Then Return bytes.ToString() & " B"
        If bytes < 1024 * 1024 Then Return (bytes / 1024.0).ToString("N1") & " KB"
        If bytes < 1024 * 1024 * 1024 Then Return (bytes / (1024.0 * 1024)).ToString("N1") & " MB"
        Return (bytes / (1024.0 * 1024 * 1024)).ToString("N1") & " GB"
    End Function

    ''' <summary>Gets the folder path for the current patient's scanned cheque images: AppFolder\Images\Patient{PatientID}\Cheques.</summary>
    Private Function GetPatientScannedFolder() As String
        If CurrentPatient Is Nothing OrElse CurrentPatient.PatientID <= 0 Then Return ""
        Dim appDir = Application.StartupPath
        If String.IsNullOrEmpty(appDir) Then Return ""
        Return IO.Path.Combine(appDir,
                               "Images",
                               "Patient" & CurrentPatient.PatientID.ToString(),
                               "Cheques")
    End Function

    ''' <summary>Loads the current patient's scanned image files into scannedFilesList (ScannedPage).</summary>
    Private Sub LoadPatientScannedFiles()
        _scannedFilePaths.Clear()
        If scannedFilesList Is Nothing Then Return
        scannedFilesList.Items.Clear()
        Dim folder = GetPatientScannedFolder()
        If String.IsNullOrEmpty(folder) OrElse Not Directory.Exists(folder) Then Return
        Dim imageExtensions = {".jpg", ".jpeg", ".png", ".bmp", ".gif", ".tif", ".tiff"}
        Dim files = Directory.GetFiles(folder, "*.*").
            Where(Function(f) imageExtensions.Contains(IO.Path.GetExtension(f).ToLowerInvariant())).
            OrderBy(Function(f) IO.Path.GetFileName(f)).
            ToList()
        For Each fullPath In files
            _scannedFilePaths.Add(fullPath)
            scannedFilesList.Items.Add(IO.Path.GetFileName(fullPath))
        Next
        ' Clear preview when list is repopulated; user can select an item to preview
        If scanPreview IsNot Nothing AndAlso Not scanPreview.IsDisposed Then
            scanPreview.Image = Nothing
            If files.Count > 0 Then
                ShowScannedFilePreview(files(0))
            End If

        End If
    End Sub

    Private Sub btnScan_Click(sender As Object, e As EventArgs) Handles btnScan.Click
        If CurrentPatient Is Nothing OrElse CurrentPatient.PatientID <= 0 Then
            Dim msgEn = "Please select a patient first."
            Dim msgAr = "يرجى اختيار المريض أولا."
            MessageBox.Show(If(Eng, msgEn, msgAr), "Scan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        ' Require cheque number before scanning so files can be named correctly
        Dim chqNumber As String = ""
        If txtChqNumber IsNot Nothing Then
            chqNumber = txtChqNumber.Text.Trim()
        End If
        If String.IsNullOrWhiteSpace(chqNumber) Then
            MsgBox("Fill all Fields for Cheque Pay")
            Return
        End If

        ' Warn if there is no payment yet for this cheque number
        If Not IsChequeNumberInPays(CurrentPatient.PatientID, chqNumber) Then
            MsgBox(If(Eng,
                      "There is no cheque payment recorded yet for this cheque number.",
                      "لا يوجد دفع شيك مسجل لهذا الرقم حتى الآن."))
        End If

        Try
            Dim folder = GetPatientScannedFolder()
            If String.IsNullOrEmpty(folder) Then Return
            ' Ensure Images\Patient{PatientID}\Cheques exists
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

            ' Rename scanned files to follow pattern: Chq{ChqNumber}.jpg (with numeric suffix if multiple/pages)
            Dim renamedFiles As New List(Of String)()
            For Each originalPath In files
                If Not File.Exists(originalPath) Then Continue For
                Dim ext = Path.GetExtension(originalPath)
                If String.IsNullOrEmpty(ext) Then ext = ".jpg"

                Dim baseName = $"Chq{chqNumber}"
                Dim targetPath = Path.Combine(folder, baseName & ext)

                If File.Exists(targetPath) Then
                    Dim counter As Integer = 1
                    Do
                        targetPath = Path.Combine(folder, $"{baseName}_{counter}{ext}")
                        counter += 1
                    Loop While File.Exists(targetPath)
                End If

                If String.Compare(originalPath, targetPath, StringComparison.OrdinalIgnoreCase) <> 0 Then
                    File.Move(originalPath, targetPath)
                End If
                renamedFiles.Add(targetPath)
            Next

            LoadPatientScannedFiles()
            ' Select first new file and show in ScannedPage preview
            If _scannedFilePaths.Count > 0 Then
                scannedFilesList.SelectedIndex = 0
                ShowScannedFilePreview(_scannedFilePaths(0))
            End If
            Dim firstPreviewPath As String = If(renamedFiles.Count > 0, renamedFiles(0), files(0))
            SetScanPreviewImage(firstPreviewPath)
            Dim msgEn = $"Scanned {files.Count} page(s) to patient folder."
            Dim msgAr = "تم مسح " & files.Count & " صفحة/صفحات إلى مجلد المريض."
            MessageBox.Show(If(Eng, msgEn, msgAr), "Scan", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            Dim msgEn = "Scan failed: " & ex.Message
            Dim msgAr = "فشل المسح: " & ex.Message
            MessageBox.Show(If(Eng, msgEn, msgAr), "Scan", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
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
            If oldImg IsNot Nothing Then oldImg.Dispose()
        Catch
            scanPreview.Image = Nothing
        End Try
    End Sub

    Private Sub scannedFilesList_SelectedIndexChanged(sender As Object, e As EventArgs) Handles scannedFilesList.SelectedIndexChanged
        If scannedFilesList Is Nothing OrElse _scannedFilePaths Is Nothing Then Return
        Dim idx = scannedFilesList.SelectedIndex
        If idx < 0 OrElse idx >= _scannedFilePaths.Count Then Return
        ShowScannedFilePreview(_scannedFilePaths(idx))
    End Sub

    Private Sub scannedFilesList_MouseMove(sender As Object, e As MouseEventArgs) Handles scannedFilesList.MouseMove
        If scannedFilesList Is Nothing OrElse _scannedFilePaths Is Nothing OrElse _scannedFilePaths.Count = 0 Then Return
        Dim idx = scannedFilesList.IndexFromPoint(scannedFilesList.PointToClient(Cursor.Position))
        If idx >= 0 AndAlso idx < _scannedFilePaths.Count Then
            Dim path = _scannedFilePaths(idx)
            Dim sizeStr = ""
            Try
                Dim fi As New FileInfo(path)
                sizeStr = FormatFileSize(fi.Length)
            Catch
                sizeStr = "?"
            End Try
            _scannedListToolTip.SetToolTip(scannedFilesList, path & vbCrLf & "Size: " & sizeStr)
        Else
            _scannedListToolTip.SetToolTip(scannedFilesList, "")
        End If
    End Sub

    Private Sub chkAddTrt_CheckedChanged(sender As Object, e As EventArgs) Handles chkAddTrt.CheckedChanged
        If chkAddTrt.Checked = True Then
            btnAddTrt.Visible = True
            lblTrtDet.Visible = True
            txtTrtDetails.Visible = True
        Else
            btnAddTrt.Visible = False
            lblTrtDet.Visible = False
            txtTrtDetails.Visible = False
        End If
    End Sub

    Private Sub btnAddTrt_Click(sender As Object, e As EventArgs) Handles btnAddTrt.Click

        Try
            If String.IsNullOrWhiteSpace(Me.TrtDate.Text) Then
                MsgBox(If(Eng, "Treat Date is Empty", "التاريخ فارغ"))
                Exit Sub
            End If
            If String.IsNullOrWhiteSpace(Me.txtTrtDetails.Text) Then
                MsgBox(If(Eng, "Detail is Empty", "التفاصيل فارغ"))
                Exit Sub
            End If
            If String.IsNullOrWhiteSpace(Me.TrtValue.Text) OrElse Val(Me.TrtValue.Text) = 0 Then
                MsgBox(If(Eng, "Enter Price", "السعر فارغ"))
                Exit Sub
            End If

            Dim TRTPrice As Decimal = Convert.ToDecimal(Val(Me.TrtValue.Text.Trim()))
            Dim TrtDat As Date = Me.TrtDate.DateTime

            Using conn As SqlConnection = DentistXDATA.GetConnection
                conn.Open()
                Dim sql = "INSERT INTO Patient_Trts (PatientID, Detail, TrtDate, TrtValue, Discount2) VALUES (@PatientID, @Detail, @TrtDate, @TrtValue, 0)"
                conn.Execute(sql, New With {PatientID, .Detail = Me.txtTrtDetails.Text.Trim(), .TrtDate = TrtDat, .TrtValue = TRTPrice})
            End Using

            LoadSubDataDapper(PatientID)
            RaiseEvent BalChanged(sender, New BalChangedEventArgs(PatientID, PatientName, Finished))
            FormManager.Instance.CurrentForm.UpdatePatientBalance(CurrentPatient)
            Me.TrtValue.Value = 0D
            Me.TrtDate.ResetText()
            Me.txtTrtDetails.ResetText()
        Catch ex As SqlException
            MsgBox(ex.Message)
        End Try
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
            If String.IsNullOrWhiteSpace(Me.PayDate.Text) Then
                MsgBox(If(Eng, "Payment Date is Empty", "التاريخ فارغ"))
                Exit Sub
            End If

            Dim Notes As String = Me.NotesText.Text
            Dim PayDat As Date = Me.PayDate.EditValue
            Dim PayValue As Double = Convert.ToDouble(Val(Me.PayValue.Text.Trim()))


            Using conn As SqlConnection = DentistXDATA.GetConnection
                conn.Open()
                ' Start the transaction
                Using trans As SqlTransaction = conn.BeginTransaction()
                    Try
                        ' Check for existing treatments (within transaction)
                        Dim treatments = conn.Query(Of Patient_Trts)("SELECT * FROM Patient_Trts WHERE PatientID = @PatientID",
                                                                        New With {.PatientId = PatientID}, trans).ToList() ' Pass transaction here

                        Dim trtID, toothTrtID As Integer

                        If treatments.Count = 0 Then
                            Dim detail = If(Eng, "Payment In Advance " & Me.DetailText.Text, "دفعة مقدما " & Me.DetailText.Text)
                            Dim Treat As String = detail
                            Dim TreatDate As Date = PayDat

                            ' First insert (within transaction)
                            conn.Execute("INSERT INTO Patient_ToothTrt (PatientID, Treat, TreatDate) VALUES (@PatientID, @Treat, @TreatDate)",
                                                                                    New With {.PatientId = PatientID, .Treat = Treat, .TreatDate = TreatDate},
                                                                                    trans) ' Pass transaction here
                            ' Get last inserted treatment ID (within transaction)
                            toothTrtID = conn.ExecuteScalar(Of Integer)("SELECT TOP 1 ToothTrtID FROM Patient_ToothTrt WHERE PatientID = @PatientID ORDER BY ToothTrtID DESC",
                                                                                        New With {.PatientId = PatientID}, trans) ' Pass transaction here

                            ' Second insert (within transaction)
                            conn.Execute("INSERT INTO Patient_Trts (PatientID, ToothTrtID, Detail, TrtDate, TrtValue, Discount2) VALUES (@PatientID, @ToothTrtID, @Detail, @TrtDate, 0, 0)",
                                                                                    New With {.PatientId = PatientID, .ToothTrtID = toothTrtID, .Detail = detail, .TrtDate = PayDat},
                                                                                    trans) ' Pass transaction here

                            ' Get last inserted treatment ID (within transaction)
                            trtID = conn.ExecuteScalar(Of Integer)("SELECT TOP 1 TrtID FROM Patient_Trts WHERE PatientID = @PatientID ORDER BY TrtID DESC",
                                                                                    New With {.PatientId = PatientID},
                                                                                    trans) ' Pass transaction here
                        Else
                            Dim currentTreatment As Patient_Trts = GetSelectedTreatment()
                            If currentTreatment Is Nothing Then
                                MsgBox(If(Eng, "Please select a treatment first.", "يرجى اختيار العلاج أولا."))
                                trans.Rollback()
                                Return
                            End If
                            trtID = currentTreatment.TrtID
                        End If


                        Select Case cboPayType.SelectedIndex
                            Case 0 'Cash
                                ' Insert payment (within transaction)
                                conn.Execute("INSERT INTO Patient_Pays (TrtID, PatientID, PayValue, PayDate, Notes, PayType, ReceivedBy, IsReturned) VALUES 
                                                                                        (@TrtID, @PatientID, @PayValue, @PayDate, @Notes, @PayType, @ReceivedBy, @IsReturned)",
                                      New With {.TrtID = trtID, .PatientID = PatientID, .PayValue = PayValue,
                                                .PayDate = PayDat, .Notes = Notes, .PayType = cboPayType.Text, .ReceivedBy = CType(Nothing, String), .IsReturned = False}, trans) ' Pass transaction here
                            Case 1 'Cheque
                                If CheckTxts() Then
                                    Dim chqNumberVal As String = txtChqNumber.Text.Trim()

                                    ' Warn if there is no scanned cheque yet
                                    If Not HasScannedCheque(PatientID, chqNumberVal) Then
                                        MsgBox(If(Eng,
                                                  "No scanned cheque image found yet for this cheque number.",
                                                  "لا توجد صورة شيك لهذا الرقم حتى الآن."))
                                    End If

                                    ' Block duplicate cheque numbers in payments table only
                                    If IsChequeNumberInPays(PatientID, chqNumberVal) Then
                                        MsgBox(If(Eng,
                                                  "This cheque number is already used for this patient.",
                                                  "رقم الشيك مستخدم مسبقا لهذا المريض."))
                                        trans.Rollback()
                                        Return
                                    End If

                                    ' Insert payment (within transaction)
                                    txtChqOwner.Text = PatientName
                                    conn.Execute("INSERT INTO Patient_Pays (TrtID, PatientID, PayValue, PayDate, Notes, PayType, ChqOwner, AccountNumber,
                                                    ChqNumber, ChqDueDate, ChqBank, IsCashed, ReceivedBy, IsReturned )
                                                  VALUES (@TrtID, @PatientID, @PayValue, @PayDate, @Notes, @PayType, @ChqOwner, @AccountNumber,
                                                          @ChqNumber, @ChqDueDate, @ChqBank, @IsCashed, @ReceivedBy, @IsReturned )",
                                    New With {
                                        .TrtID = trtID,
                                        .PatientID = PatientID,
                                        .PayValue = PayValue,
                                        .PayDate = PayDat,
                                        .Notes = Notes,
                                        .PayType = cboPayType.Text,
                                        .ChqOwner = txtChqOwner.Text,
                                        .AccountNumber = txtAccountNumber.Text,
                                        .ChqNumber = txtChqNumber.Text,
                                        .ChqDueDate = dtChqDueDate.DateTime,
                                        .ChqBank = txtChqBank.Text,
                                        .IsCashed = chkIsCashed.Checked,
                                        .ReceivedBy = CType(Nothing, String),
                                        .IsReturned = False
                                    },
                                    trans)
                                Else
                                    MsgBox("Fill all Fields for Cheque Pay")
                                End If

                            Case 2 'Insurance
                                If CheckTxtsInsur() Then
                                    ' Insert payment (within transaction)
                                    conn.Execute("INSERT INTO Patient_Pays (TrtID, PatientID, PayValue, PayDate, Notes, PayType, InsuranceCompany, InsuranceNotes, ReceivedBy, IsReturned)
                                                   VALUES (@TrtID, @PatientID, @PayValue, @PayDate, @Notes, @PayType, @InsuranceCompany, @InsuranceNotes, @ReceivedBy, @IsReturned)",
                                     New With {.TrtID = trtID, .PatientID = PatientID, .PayValue = PayValue, .PayDate = PayDat, .Notes = Notes,
                                                .PayType = cboPayType.Text, .InsuranceCompany = txtInureComp.Text,
                                                .InsuranceNotes = txtInsurNotes.Text, .ReceivedBy = CType(Nothing, String), .IsReturned = False}, trans)
                                Else
                                    MsgBox("Fill all Fields for Insurance Pay")
                                End If
                            Case Else
                                conn.Execute("INSERT INTO Patient_Pays (TrtID, PatientID, PayValue, PayDate, Notes, PayType, ReceivedBy, IsReturned) VALUES 
                                                                       (@TrtID, @PatientID, @PayValue, @PayDate, @Notes, @PayType, @ReceivedBy, @IsReturned)",
                                      New With {.TrtID = trtID, .PatientID = PatientID, .PayValue = PayValue,
                                                .PayDate = PayDat, .Notes = Notes, .PayType = cboPayType.Text, .ReceivedBy = CType(Nothing, String), .IsReturned = False}, trans) ' Pass transaction here

                        End Select
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



            LoadSubDataDapper(PatientID)
            RaiseEvent BalChanged(sender, New BalChangedEventArgs(PatientID, PatientName, Finished))
            FormManager.Instance.CurrentForm.UpdatePatientBalance(CurrentPatient)
        Catch ex As SqlException
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub btnScanAndPay_Click(sender As Object, e As EventArgs) Handles btnScanAndPay.Click
        ' Ensure cheque pay type is selected
        If cboPayType.SelectedIndex <> 1 Then
            MsgBox(If(Eng,
                      "Please select Cheque as Pay Type before Scan & Pay.",
                      "يرجى اختيار نوع الدفع شيك قبل المسح والدفع."))
            Return
        End If

        ' Ensure patient is selected
        If CurrentPatient Is Nothing OrElse CurrentPatient.PatientID <= 0 Then
            Dim msgEn = "Please select a patient first."
            Dim msgAr = "يرجى اختيار المريض أولا."
            MessageBox.Show(If(Eng, msgEn, msgAr), "Scan & Pay", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        ' Ensure cheque fields are filled (including cheque number)
        If Not CheckTxts() Then
            MsgBox("Fill all Fields for Cheque Pay")
            Return
        End If

        ' Add the payment using existing logic
        btAddPay_Click(sender, e)

        ' Then scan the cheque using the same cheque data
        btnScan_Click(sender, e)
    End Sub

    Private Sub cboPayType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboPayType.SelectedIndexChanged
        'chqInsurTab
        Select Case cboPayType.SelectedIndex
            Case 0
                GridTabControl.SelectedTabPage = CashGridPage
                chqInsurTab.Visible = False
            Case 1
                GridTabControl.SelectedTabPage = ChqGridPage
                chqInsurTab.SelectedTabPage = ChqPage
                txtChqOwner.Text = CurrentPatient.PatientName
                chqInsurTab.Visible = True
            Case 2
                chqInsurTab.SelectedTabPage = InsurePage
                chqInsurTab.Visible = True
            Case Else
                chqInsurTab.Visible = False
        End Select
    End Sub


    Private Function CheckTxts() As Boolean
        'ChqOwner, AccountNumber, ChqNumber, ChqDueDate, ' ChqBank, IsCashed, InsuranceCompany, InsuranceNotes.IsForward) 
        If txtChqOwner.Text.Length = 0 Then Return False
        If txtAccountNumber.Text.Length = 0 Then Return False
        If txtChqNumber.Text.Length = 0 Then Return False
        If dtChqDueDate.Text.Length = 0 Then Return False
        If txtChqBank.Text.Length = 0 Then Return False
        'If txtChqOwner.Text.Length = 0 Then Return False
        Return True
    End Function
    Private Function CheckTxtsInsur() As Boolean
        'ChqOwner, AccountNumber, ChqNumber, ChqDueDate, ' ChqBank, IsCashed, InsuranceCompany, InsuranceNotes.IsForward) 
        If txtInureComp.Text.Length = 0 Then Return False
        If txtInsurNotes.Text.Length = 0 Then Return False


        Return True
    End Function

    Private Function IsChequeNumberInPays(patientId As Integer, chqNumber As String) As Boolean
        If patientId <= 0 Then Return False
        If String.IsNullOrWhiteSpace(chqNumber) Then Return False

        chqNumber = chqNumber.Trim()

        Try
            Using conn As SqlConnection = DentistXDATA.GetConnection()
                conn.Open()
                Dim count = conn.ExecuteScalar(Of Integer)(
                    "SELECT COUNT(*) FROM Patient_Pays WHERE PatientID = @PatientID AND ChqNumber = @ChqNumber",
                    New With {.PatientID = patientId, .ChqNumber = chqNumber})
                Return count > 0
            End Using
        Catch
            ' If DB check fails, do not block everything
            Return False
        End Try
    End Function

    Private Function HasScannedCheque(patientId As Integer, chqNumber As String) As Boolean
        If patientId <= 0 Then Return False
        If String.IsNullOrWhiteSpace(chqNumber) Then Return False

        chqNumber = chqNumber.Trim()

        Dim folder = GetPatientScannedFolder()
        If String.IsNullOrEmpty(folder) OrElse Not Directory.Exists(folder) Then Return False

        Dim pattern = "Chq" & chqNumber & "*"
        Dim files = Directory.GetFiles(folder, pattern)
        Return files IsNot Nothing AndAlso files.Length > 0
    End Function

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

    Private Sub GridViewPays_CellValueChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles GridViewPays.CellValueChanged, ChqsGridView.CellValueChanged
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
                    .PayType = pay.PayType,
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

    Private Sub btnResetChqs_Click(sender As Object, e As EventArgs) Handles btnResetChqs.Click
        cboPayType.SelectedIndex = -1
        txtAccountNumber.ResetText()
        txtChqBank.ResetText()
        txtChqNumber.ResetText()
        txtChqOwner.ResetText()
        ChqValue.Value = 0D
        dtChqDueDate.ResetText()
        NotesText.ResetText()
        PayDate.ResetText()
        PayValue.Value = 0D
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


#End Region


    '#Region "Key Input"

    '    Private Sub txtChqValue_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtChqValue.KeyPress
    '        ' Allow control keys (Backspace, Delete, etc.)
    '        If Char.IsControl(e.KeyChar) Then
    '            Return
    '        End If

    '        ' Allow digits (0-9)
    '        If Char.IsDigit(e.KeyChar) Then
    '            Return
    '        End If

    '        ' Allow one decimal point
    '        If e.KeyChar = "."c AndAlso Not txtChqValue.Text.Contains("."c) Then
    '            Return
    '        End If

    '        ' Allow one leading minus sign
    '        If e.KeyChar = "-"c AndAlso Not txtChqValue.Text.Contains("-"c) AndAlso txtChqValue.SelectionStart = 0 Then
    '            Return
    '        End If

    '        ' Block any other character
    '        e.Handled = True
    '    End Sub


    '    Private Sub TrtValue_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TrtValue.KeyPress
    '        ' Allow control keys (Backspace, Delete, etc.)
    '        If Char.IsControl(e.KeyChar) Then
    '            Return
    '        End If

    '        ' Allow digits (0-9)
    '        If Char.IsDigit(e.KeyChar) Then
    '            Return
    '        End If

    '        ' Allow only ONE decimal point (for prices)
    '        If e.KeyChar = "."c AndAlso Not TrtValue.Text.Contains(".") Then
    '            Return
    '        End If

    '        ' Block any other character
    '        e.Handled = True
    '    End Sub
    '    Private Sub TrtValue_PreviewKeyDown(sender As Object, e As PreviewKeyDownEventArgs) Handles TrtValue.PreviewKeyDown
    '        ' Allow Ctrl+V (paste) - We'll handle validation separately
    '        If e.Control AndAlso e.KeyCode = Keys.V Then
    '            Return
    '        End If

    '        ' Allow Backspace, Delete, Arrows, Tab
    '        If e.KeyCode = Keys.Back OrElse e.KeyCode = Keys.Delete OrElse
    '                                       e.KeyCode = Keys.Left OrElse e.KeyCode = Keys.Right OrElse
    '                                       e.KeyCode = Keys.Tab Then
    '            Return
    '        End If

    '        ' Allow numbers (0-9)
    '        If e.KeyCode >= Keys.D0 AndAlso e.KeyCode <= Keys.D9 Then
    '            Return
    '        End If

    '        ' Allow numpad numbers (0-9)
    '        If e.KeyCode >= Keys.NumPad0 AndAlso e.KeyCode <= Keys.NumPad9 Then
    '            Return
    '        End If

    '        ' Allow ONE decimal point (if needed)
    '        If e.KeyCode = Keys.Decimal AndAlso Not TrtValue.Text.Contains(".") Then
    '            Return
    '        End If

    '        ' Block all other keys
    '        e.IsInputKey = False
    '    End Sub
    '    Private Sub TrtValue_EditValueChanged(sender As Object, e As EventArgs) Handles TrtValue.EditValueChanged
    '        ' Skip if empty
    '        If String.IsNullOrEmpty(TrtValue.Text) Then Return

    '        ' Store cursor position
    '        Dim cursorPos = TrtValue.SelectionStart

    '        ' Remove all non-numeric characters (except one decimal point)
    '        Dim cleanedText As New System.Text.StringBuilder()
    '        Dim hasDecimal As Boolean = False

    '        For Each c As Char In TrtValue.Text
    '            If Char.IsDigit(c) Then
    '                cleanedText.Append(c)
    '            ElseIf c = "."c AndAlso Not hasDecimal Then
    '                cleanedText.Append(c)
    '                hasDecimal = True
    '            End If
    '        Next

    '        ' If text was modified (due to invalid pasted chars), update it
    '        If cleanedText.ToString() <> TrtValue.Text Then
    '            TrtValue.Text = cleanedText.ToString()
    '            ' Restore cursor position (adjusting for removed characters)
    '            TrtValue.SelectionStart = Math.Min(cursorPos, TrtValue.Text.Length)
    '        End If
    '    End Sub



    '    Private Sub PayValue_KeyPress(sender As Object, e As KeyPressEventArgs) Handles PayValue.KeyPress, TextDiscount.KeyPress
    '        ' Allow control keys (Backspace, Delete, etc.)
    '        If Char.IsControl(e.KeyChar) Then
    '            Return
    '        End If

    '        ' Allow digits (0-9)
    '        If Char.IsDigit(e.KeyChar) Then
    '            Return
    '        End If

    '        ' Allow only ONE decimal point (for prices)
    '        If e.KeyChar = "."c AndAlso Not PayValue.Text.Contains(".") Then
    '            Return
    '        End If

    '        ' Block any other character
    '        e.Handled = True
    '    End Sub
    '    Private Sub PayValue_PreviewKeyDown(sender As Object, e As PreviewKeyDownEventArgs) Handles PayValue.PreviewKeyDown, TextDiscount.PreviewKeyDown
    '        ' Allow Ctrl+V (paste) - We'll handle validation separately
    '        If e.Control AndAlso e.KeyCode = Keys.V Then
    '            Return
    '        End If

    '        ' Allow Backspace, Delete, Arrows, Tab
    '        If e.KeyCode = Keys.Back OrElse e.KeyCode = Keys.Delete OrElse
    '                                       e.KeyCode = Keys.Left OrElse e.KeyCode = Keys.Right OrElse
    '                                       e.KeyCode = Keys.Tab Then
    '            Return
    '        End If

    '        ' Allow numbers (0-9)
    '        If e.KeyCode >= Keys.D0 AndAlso e.KeyCode <= Keys.D9 Then
    '            Return
    '        End If

    '        ' Allow numpad numbers (0-9)
    '        If e.KeyCode >= Keys.NumPad0 AndAlso e.KeyCode <= Keys.NumPad9 Then
    '            Return
    '        End If

    '        ' Allow ONE decimal point (if needed)
    '        If e.KeyCode = Keys.Decimal AndAlso Not PayValue.Text.Contains(".") Then
    '            Return
    '        End If

    '        ' Block all other keys
    '        e.IsInputKey = False
    '    End Sub
    '    Private Sub PayValue_EditValueChanged(sender As Object, e As EventArgs) Handles PayValue.EditValueChanged, TextDiscount.EditValueChanged
    '        ' Skip if empty
    '        If String.IsNullOrEmpty(PayValue.Text) Then Return

    '        ' Store cursor position
    '        Dim cursorPos = PayValue.SelectionStart

    '        ' Remove all non-numeric characters (except one decimal point)
    '        Dim cleanedText As New System.Text.StringBuilder()
    '        Dim hasDecimal As Boolean = False

    '        For Each c As Char In PayValue.Text
    '            If Char.IsDigit(c) Then
    '                cleanedText.Append(c)
    '            ElseIf c = "."c AndAlso Not hasDecimal Then
    '                cleanedText.Append(c)
    '                hasDecimal = True
    '            End If
    '        Next

    '        ' If text was modified (due to invalid pasted chars), update it
    '        If cleanedText.ToString() <> PayValue.Text Then
    '            PayValue.Text = cleanedText.ToString()
    '            ' Restore cursor position (adjusting for removed characters)
    '            PayValue.SelectionStart = Math.Min(cursorPos, PayValue.Text.Length)
    '        End If
    '    End Sub

    '#End Region
    Public Function GetFullChequeNumber(userInput As Integer) As Integer
        Const BaseChequeNumber As Integer = 30000000
        Const MaxChequeNumber As Integer = 39999999
        Dim fullNumber As Integer
        If userInput < 0 Then
            Throw New ArgumentException("Cheque number cannot be negative.")
        End If
        If BaseChequeNumber + userInput < MaxChequeNumber Then
            fullNumber = BaseChequeNumber + userInput
        End If


        If fullNumber > MaxChequeNumber Then
            MsgBox("Cheque number exceeds the valid range (39999999).")
            fullNumber = userInput
        End If

        Return fullNumber
    End Function


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
                totalDiscount += treatment.Discount
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
                            totalDiscount += treatment.Discount
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
                                cmd.Parameters.AddWithValue("@Discount", treatment.Discount)
                                cmd.Parameters.AddWithValue("@TaxRate", taxRate)
                                cmd.Parameters.AddWithValue("@LineTotal", treatment.TrtValue - treatment.Discount)
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

    Private Function GetFullWhatsNumber() As String
        Return WhatsHelper.BuildInternationalWhatsDigitsFromControls(cboPrefix, txtWhats)
    End Function

    Private Sub RefreshLblWhats()
        If lblWhats IsNot Nothing Then
            lblWhats.Text = GetFullWhatsNumber()
        End If
    End Sub

    Private Sub FillCboPrefixOnce()
        WhatsHelper.FillCboPrefixOnce(cboPrefix)
    End Sub

    Private Sub TryPersistAccountingLegacyWhatsIfChanged()
        If CurrentPatient Is Nothing OrElse CurrentPatient.PatientID <= 0 Then Return
        If Not WhatsHelper.HasPatientWhatsChangedVsDatabase(CurrentPatient.PatientID, cboPrefix, txtWhats) Then Return
        If WhatsHelper.PersistPatientWhatsNormalized(CurrentPatient.PatientID, cboPrefix, txtWhats) Then
            WhatsHelper.ApplyPersistedWhatsToPatientInstance(CurrentPatient, cboPrefix, txtWhats)
        End If
    End Sub

    Private Sub cboPrefix_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboPrefix.SelectedIndexChanged
        RefreshLblWhats()
        TryPersistAccountingLegacyWhatsIfChanged()
    End Sub

    Private Sub txtWhats_ValueChanged(sender As Object, e As EventArgs) Handles txtWhats.EditValueChanged
        RefreshLblWhats()
    End Sub

    Private Sub txtWhats_Leave(sender As Object, e As EventArgs) Handles txtWhats.Leave
        TryPersistAccountingLegacyWhatsIfChanged()
    End Sub

    Private Sub txtWhats_Validated(sender As Object, e As EventArgs) Handles txtWhats.Validated
        TryPersistAccountingLegacyWhatsIfChanged()
    End Sub







#End Region

End Class

''' <summary>Row for the Attached files grid (FullPath, Size, Type, Open).</summary>
Public Class AttachmentFileRow
    Public Property FullPath As String
    Public Property Size As Long
    Public Property Type As String
    ''' <summary>Formatted size for display (e.g. "1.5 MB").</summary>
    Public ReadOnly Property FormattedSize As String
        Get
            Return Accounting.FormatFileSize(Size)
        End Get
    End Property
End Class
