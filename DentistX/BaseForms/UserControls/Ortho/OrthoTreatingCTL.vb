Imports System.Collections.Concurrent
Imports DevExpress.XtraGrid.Views.Base
Imports Infragistics.Win.UltraWinGrid

Public Class OrthoTreatingCTL



#Region "Resize"
    Private ReadOnly controlBoundsCache As New ConcurrentDictionary(Of Control, Rectangle)
    Private Const OriginalPanelWidth As Integer = 1200 ', 600
    Private Const OriginalPanelHeight As Integer = 600
    Private ReadOnly originalPanelSize As New Size(OriginalPanelWidth, OriginalPanelHeight)
    Private originaMelSize As Size
    Private Sub StoreOriginalBounds(container As Control)
        For Each ctrl As Control In container.Controls
            controlBoundsCache.TryAdd(ctrl, ctrl.Bounds)
            If ctrl.HasChildren Then StoreOriginalBounds(ctrl)
        Next
    End Sub
    Private Sub ResizeControlsProportionally()
        If controlBoundsCache.IsEmpty Then Return
        Dim widthRatio = CSng(Me.Width) / OriginalPanelWidth
        Dim heightRatio = CSng(Me.Height) / OriginalPanelHeight
        For Each kvp In controlBoundsCache
            kvp.Key.SetBounds(
                CInt(kvp.Value.X * widthRatio),
                CInt(kvp.Value.Y * heightRatio),
                CInt(kvp.Value.Width * widthRatio),
                CInt(kvp.Value.Height * heightRatio))
        Next
    End Sub
    Private Sub OrthoTreatingCTL_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        If controlBoundsCache.IsEmpty Then Return
        ResizeControlsProportionally()
    End Sub
#End Region

    Dim stBite, stclass As String
    Dim CellStr As String = ""
    Private PatientID As Integer
    'Dim row As DsOrth.PatientRow

    'to show number in Print
    Private Sub GridView1_CustomUnboundColumnData(sender As Object, e As CustomColumnDataEventArgs) Handles GridView1.CustomUnboundColumnData
        If e.Column.Name = "ColRowNum" Then
            e.Value = e.ListSourceRowIndex + 1

        End If
    End Sub

    Private Sub OrthTreat_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        StoreOriginalBounds(Me)
        'PatientID = Qrs.FrstOrthID
        LoadData(PatientID)
        WorkDate.DateTime = Now
        SetupExtraToolTips()
        UpdateAddTreatButtonState()
    End Sub

    Private Sub SetupExtraToolTips()
        For Each ctrl As Control In {ExtraURTextBox, ExtraULTextBox, ExtraLLTextBox, ExtraLRTextBox}
            UpdateExtraToolTip(ctrl)
            AddHandler ctrl.TextChanged, AddressOf ExtraTextBox_TextChanged
        Next
    End Sub

    Private Sub ExtraTextBox_TextChanged(sender As Object, e As EventArgs)
        UpdateExtraToolTip(CType(sender, Control))
    End Sub

    Private Sub UpdateExtraToolTip(ctrl As Control)
        _extraToolTip.SetToolTip(ctrl, If(String.IsNullOrEmpty(ctrl.Text), "", ctrl.Text))
    End Sub
    Public WriteOnly Property ValueFromNewOrth() As Integer
        Set(ByVal Value As Integer)
            PatientID = Value
            ShowHide(Value)
        End Set
    End Property

    Public WriteOnly Property ValueFromParent() As Integer
        Set(ByVal Value As Integer)
            PatientID = Value
            LoadData(Value)
            ShowHide(Value)
        End Set
    End Property
    ' Suppose A needs to directly call a method in B
    Private Sub RefreshParentTreatments(ByVal PatientID As Integer)
        Dim parentAB = TryCast(Me.Parent, FullOrthoTreating)
        If parentAB IsNot Nothing Then
            parentAB.LoadData(PatientID)
        End If
        ShowHide(PatientID)
    End Sub

    Private clsOrthoInf As New OrthoInf
    Private clsOrthoInfDATA As New OrthoInfDATA
    Private clsOrthoDiag As New OrthoDiag
    Private clsOrthoDiagDATA As New OrthoDiagDATA
    '======
    Private clsOrthoTreat As New OrthoTreat
    Private clsOrthoTreatDATA As New OrthoTreatDATA
    Private clsOrthoTretDet As New OrthoTrtDet
    Private clsOrthoTrtDetDATA As New OrthoTrtDetDATA
    '=======
    Private clsTblMeasure As New TblMeasure
    Private clsTblMeasureDATA As New TblMeasureDATA
    Private clsTblWireType As New TblWireType
    Private clsTblWireTypeDATA As New TblWireTypeDATA

    Private _extraToolTip As New ToolTip()
    Private isAdded As Boolean = False
    Private Sub DataBind()
        If Not isAdded Then
            Me.WireImgTextBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.OrthoTrtDetBindingSource, "WireImg", True))
            Me.ExtraLLTextBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.OrthoTreatBindingSource, "ExtraLL", True))
            Me.ExtraLRTextBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.OrthoTreatBindingSource, "ExtraLR", True))
            Me.ExtraURTextBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.OrthoTreatBindingSource, "ExtraUR", True))
            Me.FixerTypeTextBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.OrthoTreatBindingSource, "FixerType", True))
            Me.txtFixerDate.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.OrthoTreatBindingSource, "FixerDate", True))
            Me.ExtraULTextBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.OrthoTreatBindingSource, "ExtraUL", True))
            Me.BraketTypeTextBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.OrthoTreatBindingSource, "BraketType", True))
            Me.txtFinishDate.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.OrthoTreatBindingSource, "FinishDate", True))
            Me.txtOrthoType.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.OrthoTreatBindingSource, "OrthoType", True))
            Me.txtBeginDate.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.OrthoTreatBindingSource, "BeginDate", True))
            Me.txtKhota.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.OrthoInfBindingSource, "Khota", True))
            Me.OverLoadTeethTextBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.OrthoInfBindingSource, "OverLoadTeeth", True))
            Me.BurriedTeethTextBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.OrthoInfBindingSource, "BurriedTeeth", True))
            Me.txtPrevIll.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.OrthoInfBindingSource, "PrevIll", True))
            Me.TeethLossTextBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.OrthoInfBindingSource, "TeethLoss", True))
            Me.IllnesPeriodTextBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.OrthoInfBindingSource, "IllnesPeriod", True))
            Me.MilkTeethChngTextBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.OrthoInfBindingSource, "MilkTeethChng", True))
            Me.MalfunctionTextBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.OrthoInfBindingSource, "Malfunction", True))
            Me.MilkTeethAppearTextBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.OrthoInfBindingSource, "MilkTeethAppear", True))
            Me.BadHabitsTextBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.OrthoInfBindingSource, "BadHabits", True))
            Me.FeedTextBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.OrthoInfBindingSource, "Feed", True))
            Me.txtPervOrth.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.OrthoInfBindingSource, "PrevOrth", True))
            Me.BirthTextBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.OrthoInfBindingSource, "Birth", True))
            Me.CousinsHFactorTextBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.OrthoInfBindingSource, "CousinsHFactor", True))
            Me.CompliantsTextBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.OrthoInfBindingSource, "Compliants", True))
            Me.ThroatCutTextBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.OrthoInfBindingSource, "ThroatCut", True)) 'TextOrthoInfID
            Me.TextOrthoInfID.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.OrthoInfBindingSource, "OrthoID", True)) 'TextPatientID
            Me.TextPatientID.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.OrthoInfBindingSource, "PatientID", True)) 'TextPatientID
            Me.LipsCutTextBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.OrthoInfBindingSource, "LipsCut", True))
            Me.BiteTextBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.OrthoDiagBindingSource, "Bite", True))
            Me.ClassTextBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.OrthoDiagBindingSource, "ClassI", True))
            Me.DiagnoseTextBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.OrthoDiagBindingSource, "CloseType", True))
            Me.txtOrthoDate.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.OrthoInfBindingSource, "TreatDate", True))
            isAdded = True
        End If
    End Sub
    Public Sub LoadData(ByVal PatientID As Integer)
        Try
            ' Load data using DATA classes
            Dim orthoInfs = clsOrthoInfDATA.SelectAll().Where(Function(x) x.PatientID = PatientID).ToList()
            Dim orthoDiags = clsOrthoDiagDATA.SelectAll().Where(Function(x) x.PatientID = PatientID).ToList()
            Dim orthoTreats = clsOrthoTreatDATA.SelectAll().Where(Function(x) x.PatientID = PatientID).ToList()
            Dim orthoSteps = clsOrthoTrtDetDATA.SelectAll().Where(Function(x) x.PatientID = PatientID).ToList()
            Dim measures = clsTblMeasureDATA.SelectAll().ToList()
            Dim wireTypes = clsTblWireTypeDATA.SelectAll().ToList()

            ' Assign to binding sources if needed
            OrthoTreatBindingSource.DataSource = orthoTreats
            OrthoTrtDetBindingSource.DataSource = orthoSteps
            OrthoTrtDetGrid.DataSource = OrthoTrtDetBindingSource
            OrthoInfBindingSource.DataSource = orthoInfs
            OrthoDiagBindingSource.DataSource = orthoDiags
            DataBind()
            ShowHide(PatientID)
            RefreshFinishUIState()

        Catch ex As SqlClient.SqlException
            MsgBox(ex.Message)
        End Try
    End Sub


#Region "OrthoTreat_Det"

    ''' <summary>
    ''' "Mark as Finished" / "Edit Finish Date" button: toggles the finish date picker.
    ''' When finishDate is hidden: shows it so user can set/edit the finish date, then save via OrthoTrtSave.
    ''' When finishDate is visible: cancels and hides it.
    ''' </summary>
    Private Sub btEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFinish.Click
        Try
            If finishDate.Visible Then
                ' Cancel: hide date picker, return to display mode
                finishDate.Visible = False
                RefreshFinishUIState()
            Else
                ' Enter edit mode: show date picker for user to set finish date
                finishDate.Visible = True
                If Not String.IsNullOrWhiteSpace(txtFinishDate.Text) Then
                    Try
                        finishDate.EditValue = DateTime.Parse(txtFinishDate.Text)
                    Catch
                        finishDate.EditValue = DateTime.Today
                    End Try
                Else
                    finishDate.EditValue = DateTime.Today
                End If
                btnFinish.Text = If(Eng, "Cancel", "إلغاء")
            End If
        Catch ex As System.Data.SqlClient.SqlException
            MsgBox(ex.Message)
        End Try
    End Sub

    ''' <summary>
    ''' Updates finish panel and date picker visibility based on whether treatment has a finish date.
    ''' Call on load, when binding changes, and after successful save.
    ''' </summary>
    Private Sub RefreshFinishUIState()
        Dim hasFinishDate As Boolean = Not String.IsNullOrWhiteSpace(txtFinishDate.Text)
        finishPnl.Visible = True
        finishDate.Visible = False
        txtFinishDate.ReadOnly = True
        txtFixerDate.ReadOnly = True
        FixerTypeTextBox.ReadOnly = True
        If hasFinishDate Then
            btnFinish.Text = If(Eng, "Edit Finish Date", "تعديل تاريخ الانهاء")
        Else
            btnFinish.Text = If(Eng, "Mark as Finished", "تعيين كمنتهي")
        End If
    End Sub



    Private Sub btAddTreat_Click(sender As Object, e As EventArgs) Handles btAddTreat.Click
        Try
            If beginDate.EditValue Is Nothing OrElse beginDate.EditValue Is DBNull.Value OrElse String.IsNullOrWhiteSpace(beginDate.Text) OrElse beginDate.Text.Length < 8 Then
                MsgBox(If(Eng, "Please select a valid Begin Date.", "الرجاء تحديد تاريخ بداية صالح."))
                Return
            End If
            Dim Bdate As Date = Me.beginDate.EditValue

            Dim orthoId As Integer = 0
            Integer.TryParse(TextOrthoInfID.Text, orthoId)
            Dim countForThisOrtho As Integer = clsOrthoTreatDATA.TrtCountByOrthoID(PatientID, orthoId)

            ' If a treatment already exists for this ortho: behave as "Save" and reuse OrthoTrtSave logic
            If countForThisOrtho > 0 Then
                OrthoTrtSave_Click(sender, e)
                Return
            End If

            ' No treatment exists yet for this ortho: behave as "Add" and insert a new row
            Dim newTreat As New OrthoTreat With {
                .PatientID = PatientID,
                .OrthoID = orthoId,
                .BeginDate = Bdate,
                .OrthoType = orthoTypeCombo.Text,
                .ExtraUL = ulCombo.Text,
                .ExtraLL = llCombo.Text,
                .ExtraUR = urCombo.Text,
                .ExtraLR = lrCombo.Text,
                .FixerType = fixerCombo.Text,
                .BraketType = bracetCombo.BracetName
            }
            ' Use specialized method that ignores DiagID FK and works only with PatientID + OrthoID
            If clsOrthoTreatDATA.AddForOrthoTreating(newTreat) Then
                LoadData(PatientID)
                GrpTrtMainINf.Visible = True
                GrpTrtSteps.Visible = True
                OrthoTrtDetGrid.Visible = True
                UpdateAddTreatButtonState()
            Else
                MsgBox("Failed to add treatment")
            End If

        Catch ex As SqlClient.SqlException
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub btAddtrtStep_Click(sender As Object, e As EventArgs) Handles btAddtrtStep.Click
        Try
            If WorkDate.EditValue Is Nothing OrElse WorkDate.EditValue Is DBNull.Value OrElse String.IsNullOrWhiteSpace(WorkDate.Text) OrElse WorkDate.Text.Length < 8 Then
                MsgBox(If(Eng, "Please select a valid Work Date.", "الرجاء تحديد تاريخ العمل صالح."))
                Return
            End If
            Dim Wdate As Date = Me.WorkDate.EditValue

            Dim orthoId As Integer = 0
            Integer.TryParse(TextOrthoInfID.Text, orthoId)
            Dim newStep As New OrthoTrtDet With {
            .PatientID = PatientID,
            .OrthoID = orthoId,
            .WorkDate = Wdate,
            .WireMeasure = wireMeasCombo.Measure,
            .WireType = wireTypeCombo.WireType,
            .WireImg = WireImgTextBox.Text,
            .WireNotes = txtWireNotes.Text
        }

            If clsOrthoTrtDetDATA.Add(newStep) Then
                LoadData(PatientID)
                ClearCtls()
            Else
                MsgBox("Failed to add step")
            End If

        Catch ex As SqlClient.SqlException
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub ClearCtls()
        Me.WorkDate.EditValue = Now
        Me.wireMeasCombo.CboWireMeasure.SelectedIndex = -1
        Me.wireTypeCombo.CboWireType.SelectedIndex = -1
        Me.WireImgTextBox.Text = ""
        Me.txtWireNotes.Text = ""
    End Sub



    Private Sub OrthoTrtSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OrthoTrtSave.Click
        Try
            ' Push any pending edits from bound controls
            Me.Validate()
            Me.OrthoTreatBindingSource.EndEdit()

            Dim updated As OrthoTreat = TryCast(Me.OrthoTreatBindingSource.Current, OrthoTreat)
            If updated Is Nothing Then Exit Sub

            ' Copy values from NON‑bound UI controls into the current entity
            ' (otherwise changes done in these controls are never persisted).
            Dim orthoId As Integer
            If Integer.TryParse(TextOrthoInfID.Text, orthoId) AndAlso orthoId > 0 Then
                updated.OrthoID = orthoId
            End If

            ' BeginDate comes from beginDate control (not directly bound)
            If beginDate.EditValue IsNot Nothing AndAlso beginDate.EditValue IsNot DBNull.Value Then
                Try
                    Dim bDate As Date = CType(beginDate.EditValue, Date)
                    updated.BeginDate = bDate
                Catch
                End Try
            End If

            ' Ortho type / extras / fixer / braket are edited via combos, not via bound textboxes
            updated.OrthoType = orthoTypeCombo.Text
            updated.ExtraUL = ulCombo.Text
            updated.ExtraLL = llCombo.Text
            updated.ExtraUR = urCombo.Text
            updated.ExtraLR = lrCombo.Text
            updated.FixerType = If(Not String.IsNullOrWhiteSpace(FixerTypeTextBox.Text), FixerTypeTextBox.Text, fixerCombo.Text)
            Try
                updated.BraketType = bracetCombo.BracetName
            Catch
                updated.BraketType = bracetCombo.Text
            End Try

            ' Sync fixer / finish controls into their bound textboxes before reading entity values
            SyncFixerControlsToTextboxes()
            If finishDate.EditValue IsNot Nothing AndAlso finishDate.EditValue IsNot DBNull.Value AndAlso finishDate.Text.Length > 0 Then
                Try
                    updated.FinishDate = CType(finishDate.EditValue, Date)
                Catch
                End Try
            End If

            ' Fetch original record from DB
            Dim original As OrthoTreat = clsOrthoTreatDATA.Select_Record(New OrthoTreat With {.TreatID = updated.TreatID, .OrthoID = updated.OrthoID, .PatientID = updated.PatientID})
            If original Is Nothing Then
                MsgBox("Original record not found.")
                Exit Sub
            End If

            If clsOrthoTreatDATA.Update(original, updated) Then
                MsgBox(If(Eng, "Treatment updated successfully.", "تم تحديث العلاج بنجاح."))
                RefreshFinishUIState()
                ' Reload latest data and return user to main treatment tab
                LoadData(PatientID)
                TreatTabCtl.SelectedTabPage = TreatmentStepsTab
                UpdateAddTreatButtonState()
            Else
                MsgBox(If(Eng, "Update failed.", "فشل التحديث."))
            End If

        Catch ex As SqlClient.SqlException
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub OrthoTrtDetSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OrthoTrtDetSave.Click
        Try
            Me.Validate()
            Me.OrthoTrtDetBindingSource.EndEdit()

            ' Build list of steps currently in the grid (after user may have deleted rows)
            Dim currentSteps As New List(Of OrthoTrtDet)
            For i As Integer = 0 To OrthoTrtDetBindingSource.Count - 1
                Dim s As OrthoTrtDet = TryCast(OrthoTrtDetBindingSource.Item(i), OrthoTrtDet)
                If s IsNot Nothing Then currentSteps.Add(s)
            Next
            Dim currentDetIds As New HashSet(Of Integer)(currentSteps.Select(Function(x) x.DetID))

            ' Delete from DB any step that was removed from the grid (navigator Delete),
            ' but only for the current OrthoID.
            Dim orthoId As Integer = 0
            Integer.TryParse(TextOrthoInfID.Text, orthoId)
            Dim dbSteps = clsOrthoTrtDetDATA.SelectAll().Where(Function(x) x.PatientID = PatientID AndAlso x.OrthoID = orthoId).ToList()
            For Each steps In dbSteps
                If Not currentDetIds.Contains(steps.DetID) Then
                    clsOrthoTrtDetDATA.Delete(New OrthoTrtDet With {.DetID = steps.DetID, .OrthoID = steps.OrthoID, .PatientID = steps.PatientID})
                End If
            Next

            ' Update in DB each step that is still in the grid (handles null current and all rows)
            For Each steps In currentSteps
                If steps.DetID <> 0 Then
                    Dim original As OrthoTrtDet = clsOrthoTrtDetDATA.Select_Record(New OrthoTrtDet With {.DetID = steps.DetID, .OrthoID = steps.OrthoID, .PatientID = steps.PatientID})
                    If original IsNot Nothing Then
                        clsOrthoTrtDetDATA.Update(original, steps)
                    End If
                End If
            Next

            LoadData(PatientID)
            MsgBox(If(Eng, "Steps saved successfully.", "تم حفظ الخطوات بنجاح."))
        Catch ex As SqlClient.SqlException
            MsgBox(ex.Message)
        End Try
    End Sub



    Private Sub wireMeasCombo_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles wireMeasCombo.WireMeasureValueChanged
        If Me.wireMeasCombo.MeasureID = 0 Or Me.wireMeasCombo.MeasureID = 1 Or Me.wireMeasCombo.MeasureID = 2 Or Me.wireMeasCombo.MeasureID = 3 Then
            Me.WireImgTextBox.Text = "m"
        Else
            Me.WireImgTextBox.Text = "p"
        End If
    End Sub
    Private Sub OrthoTreatBindingSource_CurrentChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        If clsOrthoTreatDATA.TrtCount(PatientID) = 0 Then
            TreatTabCtl.SelectedTabPage = BeginNewTab
        Else
            TreatTabCtl.SelectedTabPage = TreatmentStepsTab
            RefreshFinishUIState()
        End If
    End Sub

    Public Sub ShowHide(ByVal PatientID As Integer)
        Try
            If clsOrthoTreatDATA.TrtCount(PatientID) = 0 Then
                TreatTabCtl.SelectedTabPage = BeginNewTab
            Else
                TreatTabCtl.SelectedTabPage = TreatmentStepsTab
            End If
            UpdateAddTreatButtonState()
            'Me.GrpOrthCrd.Text = "البيانات الاولية للتقويم-معلومات عامة" & "  -->  " & PatientName
        Catch ex As Exception

        End Try
    End Sub

    ''' <summary>
    ''' Updates btAddTreat caption and role based on whether a treatment already exists for the current OrthoID.
    ''' If at least one OrthoTreat exists for (PatientID, OrthoID) => button text is "Save" and will update.
    ''' Otherwise button text is "Add" and will insert a new record.
    ''' </summary>
    Private Sub UpdateAddTreatButtonState()
        Dim orthoId As Integer = 0
        Integer.TryParse(TextOrthoInfID.Text, orthoId)
        If PatientID <= 0 OrElse orthoId <= 0 Then
            btAddTreat.Text = If(Eng, "Add", "إضافة")
            Return
        End If

        Dim countForThisOrtho As Integer = clsOrthoTreatDATA.TrtCountByOrthoID(PatientID, orthoId)
        If countForThisOrtho > 0 Then
            btAddTreat.Text = If(Eng, "Save", "حفظ")
        Else
            btAddTreat.Text = If(Eng, "Add", "إضافة")
        End If
    End Sub

    ''' <summary>
    ''' When user switches tabs, keep Add/Save caption in sync and, when entering BeginNewTab,
    ''' pre-fill the "new treatment" fields from the current OrthoTreat row (if one exists).
    ''' </summary>
    Private Sub TreatTabCtl_SelectedPageChanged(sender As Object, e As DevExpress.XtraTab.TabPageChangedEventArgs) Handles TreatTabCtl.SelectedPageChanged
        UpdateAddTreatButtonState()

        If e.Page Is BeginNewTab Then
            FillBeginNewTreatFromCurrent()
        End If
    End Sub

    ''' <summary>
    ''' Fills beginDate / orthoType / extra and fixer fields with the current OrthoTreat record
    ''' for the active OrthoID, but only if such a record exists.
    ''' </summary>
    Private Sub FillBeginNewTreatFromCurrent()
        Dim orthoId As Integer = 0
        Integer.TryParse(TextOrthoInfID.Text, orthoId)
        If PatientID <= 0 OrElse orthoId <= 0 Then Return

        ' Ensure there is at least one treatment record for this ortho
        Dim countForThisOrtho As Integer = clsOrthoTreatDATA.TrtCountByOrthoID(PatientID, orthoId)
        If countForThisOrtho <= 0 Then Return

        ' Position binding source on the matching OrthoTreat row
        Dim currentTreat As OrthoTreat = TryCast(OrthoTreatBindingSource.Current, OrthoTreat)
        If currentTreat Is Nothing OrElse currentTreat.OrthoID <> orthoId Then
            For i As Integer = 0 To OrthoTreatBindingSource.Count - 1
                Dim row As OrthoTreat = TryCast(OrthoTreatBindingSource.Item(i), OrthoTreat)
                If row IsNot Nothing AndAlso row.OrthoID = orthoId Then
                    OrthoTreatBindingSource.Position = i
                    currentTreat = row
                    Exit For
                End If
            Next
        End If

        If currentTreat Is Nothing Then Return

        ' Copy entity values into the "begin new" controls
        If currentTreat.BeginDate <> Date.MinValue Then
            beginDate.EditValue = currentTreat.BeginDate
        End If

        orthoTypeCombo.Text = currentTreat.OrthoType
        ulCombo.Text = currentTreat.ExtraUL
        llCombo.Text = currentTreat.ExtraLL
        urCombo.Text = currentTreat.ExtraUR
        lrCombo.Text = currentTreat.ExtraLR
        fixerCombo.Text = currentTreat.FixerType

        ' Bracket control exposes BracetName when building the OrthoTreat object
        Try
            bracetCombo.BracetName = currentTreat.BraketType
            bracetCombo.SetBracetIDByBracetName(currentTreat.BraketType)
        Catch
            ' If BracetName is not available, fall back to Text
            bracetCombo.Text = currentTreat.BraketType
        End Try
    End Sub

    Private Sub finishDate_ValueChanged(sender As Object, e As EventArgs) Handles finishDate.EditValueChanged
        If finishDate.Text.Length > 9 Then
            Me.txtFinishDate.Text = finishDate.EditValue.ToShortDateString
        End If

    End Sub

    Private Sub fixerDate_EditValueChanged(sender As Object, e As EventArgs) Handles fixerDate.EditValueChanged, fixerDate2.EditValueChanged
        SyncFixerControlsToTextboxes()
    End Sub

    Private Sub fixerCombo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles fixerCombo.EditValueChanged, fixerCombo2.EditValueChanged
        SyncFixerControlsToTextboxes()
    End Sub

    ''' <summary>
    ''' Syncs fixerDate, fixerDate2, fixerCombo, fixerCombo2 to txtFixerDate and FixerTypeTextBox.
    ''' Only updates when at least one of the four controls has a value in its text portion.
    ''' </summary>
    Private Sub SyncFixerControlsToTextboxes()
        Dim hasFixerDate As Boolean = Not String.IsNullOrWhiteSpace(fixerDate.Text)
        Dim hasFixerDate2 As Boolean = Not String.IsNullOrWhiteSpace(fixerDate2.Text)
        Dim hasFixerCombo As Boolean = Not String.IsNullOrWhiteSpace(fixerCombo.Text)
        Dim hasFixerCombo2 As Boolean = Not String.IsNullOrWhiteSpace(fixerCombo2.Text)

        If Not (hasFixerDate OrElse hasFixerDate2 OrElse hasFixerCombo OrElse hasFixerCombo2) Then
            Return
        End If

        ' Fill txtFixerDate from date controls (only if at least one date has value)
        If hasFixerDate OrElse hasFixerDate2 Then
            If hasFixerDate AndAlso fixerDate.EditValue IsNot Nothing AndAlso fixerDate.EditValue IsNot DBNull.Value Then
                Try
                    Me.txtFixerDate.Text = CType(fixerDate.EditValue, Date).ToShortDateString()
                Catch
                End Try
            ElseIf hasFixerDate2 AndAlso fixerDate2.EditValue IsNot Nothing AndAlso fixerDate2.EditValue IsNot DBNull.Value Then
                Try
                    Me.txtFixerDate.Text = CType(fixerDate2.EditValue, Date).ToShortDateString()
                Catch
                End Try
            End If
        End If

        ' Fill FixerTypeTextBox from combo controls (only if at least one combo has value)
        If hasFixerCombo OrElse hasFixerCombo2 Then
            If hasFixerCombo Then
                Me.FixerTypeTextBox.Text = fixerCombo.Text.Trim()
            Else
                Me.FixerTypeTextBox.Text = fixerCombo2.Text.Trim()
            End If
        End If
    End Sub



    Private Sub btnEditOrtho_Click(sender As Object, e As EventArgs) Handles btnEditOrtho.Click
        Try
            Dim orthoId As Integer
            Dim patientId As Integer

            If Not Integer.TryParse(TextOrthoInfID.Text, orthoId) OrElse orthoId <= 0 Then
                MsgBox(If(Eng, "No orthodontic record selected to edit.", "لا يوجد تقويم محدد للتعديل."))
                Return
            End If
            If Not Integer.TryParse(TextPatientID.Text, patientId) OrElse patientId <= 0 Then
                MsgBox(If(Eng, "No patient selected.", "لم يتم اختيار مريض."))
                Return
            End If

            Dim currentInf As OrthoInf = TryCast(OrthoInfBindingSource.Current, OrthoInf)
            Dim currentDiag As OrthoDiag = TryCast(OrthoDiagBindingSource.Current, OrthoDiag)

            If currentInf Is Nothing OrElse currentInf.OrthoID <> orthoId Then
                ' Try to locate the matching OrthoInf row by OrthoID if bindingsource is on a different record
                For i As Integer = 0 To OrthoInfBindingSource.Count - 1
                    Dim row As OrthoInf = TryCast(OrthoInfBindingSource.Item(i), OrthoInf)
                    If row IsNot Nothing AndAlso row.OrthoID = orthoId Then
                        OrthoInfBindingSource.Position = i
                        currentInf = row
                        Exit For
                    End If
                Next
            End If

            If currentDiag Is Nothing OrElse currentDiag.OrthoID <> orthoId Then
                For i As Integer = 0 To OrthoDiagBindingSource.Count - 1
                    Dim row As OrthoDiag = TryCast(OrthoDiagBindingSource.Item(i), OrthoDiag)
                    If row IsNot Nothing AndAlso row.OrthoID = orthoId Then
                        OrthoDiagBindingSource.Position = i
                        currentDiag = row
                        Exit For
                    End If
                Next
            End If

            If currentInf Is Nothing OrElse currentDiag Is Nothing Then
                MsgBox(If(Eng, "Unable to find orthodontic data for editing.", "يتعذر العثور على بيانات التقويم للتعديل."))
                Return
            End If

            Using frm As New FrmEditOrthInfDiag()
                frm.InitForEdit(currentInf, currentDiag)
                frm.StartPosition = FormStartPosition.CenterParent
                frm.ShowDialog(Me.FindForm())
            End Using

            ' Reload to reflect any changes
            LoadData(patientId)
            ShowHide(patientId)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub btnDelOrthTreat_Click(sender As Object, e As EventArgs) Handles btnDelOrthTreat.Click
        Try
            Dim orthoId As Integer = 0
            Integer.TryParse(TextOrthoInfID.Text, orthoId)

            If PatientID <= 0 OrElse orthoId <= 0 Then
                Dim noTrtMsgEn As String = "No orthodontic treatment selected."
                Dim noTrtMsgAr As String = "لم يتم اختيار علاج تقويمي."
                MessageBox.Show(If(Eng, noTrtMsgEn, noTrtMsgAr),
                                If(Eng, "Error", "خطأ"),
                                MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return
            End If

            ' Ensure there is a treatment for this OrthoID
            Dim countForThisOrtho As Integer = clsOrthoTreatDATA.TrtCountByOrthoID(PatientID, orthoId)
            If countForThisOrtho <= 0 Then
                Dim notFoundMsgEn As String = "No orthodontic treatment found for this record."
                Dim notFoundMsgAr As String = "لا يوجد علاج تقويمي مسجل لهذا السجل."
                MessageBox.Show(If(Eng, notFoundMsgEn, notFoundMsgAr),
                                If(Eng, "Error", "خطأ"),
                                MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return
            End If

            ' Confirm deletion of treatment and its steps (does NOT delete the whole Ortho episode)
            Dim msgEn As String = "Are you sure you want to delete this orthodontic treatment and all its steps?"
            Dim msgAr As String = "هل أنت متأكد أنك تريد حذف هذا العلاج التقويمي وكل خطواته؟"
            Dim titleEn As String = "Delete Orthodontic Treatment"
            Dim titleAr As String = "حذف علاج التقويم"
            If MessageBox.Show(If(Eng, msgEn, msgAr),
                               If(Eng, titleEn, titleAr),
                               MessageBoxButtons.YesNo,
                               MessageBoxIcon.Warning) <> DialogResult.Yes Then
                Return
            End If

            ' Delete steps then treatment rows for this OrthoID
            clsOrthoTrtDetDATA.DeleteByPatientAndOrtho(PatientID, orthoId)
            clsOrthoTreatDATA.DeleteByPatientAndOrtho(PatientID, orthoId)

            ' Reload UI to reflect removal
            LoadData(PatientID)
            ShowHide(PatientID)
            UpdateAddTreatButtonState()

            Dim okMsgEn As String = "Orthodontic treatment deleted successfully."
            Dim okMsgAr As String = "تم حذف علاج التقويم بنجاح."
            MessageBox.Show(If(Eng, okMsgEn, okMsgAr),
                            If(Eng, "Success", "نجاح"),
                            MessageBoxButtons.OK, MessageBoxIcon.Information)

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub btnDelTrtStep_Click(sender As Object, e As EventArgs) Handles btnDelTrtStep.Click
        Try
            Dim currentStep As OrthoTrtDet = TryCast(OrthoTrtDetBindingSource.Current, OrthoTrtDet)
            If currentStep Is Nothing OrElse currentStep.DetID = 0 Then
                Dim msgEn As String = "No treatment step selected."
                Dim msgAr As String = "لم يتم اختيار خطوة علاج."
                MessageBox.Show(If(Eng, msgEn, msgAr),
                                If(Eng, "Error", "خطأ"),
                                MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return
            End If

            Dim confirmEn As String = "Are you sure you want to delete this treatment step?"
            Dim confirmAr As String = "هل أنت متأكد أنك تريد حذف خطوة العلاج هذه؟"
            Dim titleEn As String = "Delete Step"
            Dim titleAr As String = "حذف خطوة"
            If MessageBox.Show(If(Eng, confirmEn, confirmAr),
                               If(Eng, titleEn, titleAr),
                               MessageBoxButtons.YesNo,
                               MessageBoxIcon.Warning) <> DialogResult.Yes Then
                Return
            End If

            ' Delete ONLY this step from the database using its primary key (DetID + PatientID + OrthoID)
            clsOrthoTrtDetDATA.Delete(New OrthoTrtDet With {
                .DetID = currentStep.DetID,
                .OrthoID = currentStep.OrthoID,
                .PatientID = currentStep.PatientID
            })

            ' Reload from DB so the grid reflects the exact persisted state
            LoadData(PatientID)

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub btnShowImages_Click(sender As Object, e As EventArgs) Handles btnShowImages.Click
        FrmOrthoImages.ShowForPatient(PatientID)
    End Sub






#End Region




    Private Sub btnDelOrtho_Click(sender As Object, e As EventArgs) Handles btnDelOrtho.Click
        Try
            If PatientID < 1 Then
                Dim noPatientMsgEn As String = "No patient selected."
                Dim noPatientMsgAr As String = "لم يتم اختيار مريض."
                Dim noPatientMsg As String = If(Eng, noPatientMsgEn, noPatientMsgAr)
                Dim noPatientTitleEn As String = "Error"
                Dim noPatientTitleAr As String = "خطأ"
                Dim noPatientTitle As String = If(Eng, noPatientTitleEn, noPatientTitleAr)
                MessageBox.Show(noPatientMsg, noPatientTitle, MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return
            End If

            Dim orthoId As Integer = 0
            Integer.TryParse(TextOrthoInfID.Text, orthoId)
            Dim infToDel As OrthoInf = clsOrthoInfDATA.Select_Record(New OrthoInf With {.OrthoID = orthoId, .PatientID = CInt(TextPatientID.Text)})
            If infToDel Is Nothing Then
                Dim notFoundMsgEn As String = "Orthodontics treat not found."
                Dim notFoundMsgAr As String = "لم يتم العثور على علاج تقويم الأسنان."
                Dim notFoundMsg As String = If(Eng, notFoundMsgEn, notFoundMsgAr)
                Dim notFoundTitleEn As String = "Error"
                Dim notFoundTitleAr As String = "خطأ"
                Dim notFoundTitle As String = If(Eng, notFoundTitleEn, notFoundTitleAr)
                MessageBox.Show(notFoundMsg, notFoundTitle, MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return
            End If
            ' First confirmation (standard MessageBox)
            Dim msgEn As String = "Are you sure you want to delete this  Orthodontics treat?"
            Dim msgAr As String = "هل أنت متأكد أنك تريد حذف هذا  التقويم؟"
            Dim msg As String = If(Eng, msgEn, msgAr)
            Dim titleEn As String = "First Warning"
            Dim titleAr As String = "التحذير الأول"
            Dim title As String = If(Eng, titleEn, titleAr)
            If MessageBox.Show(msg, title, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) <> DialogResult.Yes Then
                Return
            End If
            ' Second confirmation (custom dialog)
            Dim confirmMsgEn As String = "FINAL WARNING: This will permanently delete Orthodontics treat. Check the box to confirm."
            Dim confirmMsgAr As String = "تحذير نهائي: سيؤدي هذا إلى حذف  التقويم بشكل دائم. تحقق من المربع للتأكيد."
            Dim confirmMsg As String = If(Eng, confirmMsgEn, confirmMsgAr)
            Dim confirmTitleEn As String = "Patient Orthodontics Treat Deletion Form"
            Dim confirmTitleAr As String = "نموذج حذف تقويم المرضى"
            Dim confirmTitle As String = If(Eng, confirmTitleEn, confirmTitleAr)

            Using confirmDialog As New DoubleConfirmDialog() With {.Text = confirmTitle}
                confirmDialog.Message = confirmMsg
                If confirmDialog.ShowDialog(Me) <> DialogResult.OK OrElse Not confirmDialog.IsConfirmed Then
                    Return ' Exit if user cancels or doesn't check the box
                End If
            End Using
            ' Proceed with deletion of OrthoTreat steps, OrthoTreat row, then OrthoInf/Diag
            Dim deletedSteps As Integer = clsOrthoTrtDetDATA.DeleteByPatientAndOrtho(PatientID, orthoId)
            Dim deletedTreats As Integer = clsOrthoTreatDATA.DeleteByPatientAndOrtho(PatientID, orthoId)

            If clsOrthoInfDATA.Delete(infToDel) Then
                Dim successMsgEn As String = "Treatments deleted successfully."
                Dim successMsgAr As String = "تم حذف التقويم بنجاح."
                Dim successMsg As String = If(Eng, successMsgEn, successMsgAr)
                Dim successTitleEn As String = "Success"
                Dim successTitleAr As String = "نجاح"
                Dim successTitle As String = If(Eng, successTitleEn, successTitleAr)
                MessageBox.Show(successMsg, successTitle, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Dim parentFull = TryCast(Me.Parent, FullOrthoTreating)
                If parentFull IsNot Nothing AndAlso parentFull.CurrentPatient IsNot Nothing Then
                    FormManager.Instance.CurrentForm.UpdatePatientBalance(parentFull.CurrentPatient)
                End If
                RefreshParentTreatments(PatientID)
                ShowHide(PatientID)
            Else
                Dim errorMsgEn As String = "Failed to delete treatments."
                Dim errorMsgAr As String = "فشل في حذف التقويم."
                Dim errorMsg As String = If(Eng, errorMsgEn, errorMsgAr)
                Dim errorTitleEn As String = "Error"
                Dim errorTitleAr As String = "خطأ"
                Dim errorTitle As String = If(Eng, errorTitleEn, errorTitleAr)
                MessageBox.Show(errorMsg, errorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub













End Class
