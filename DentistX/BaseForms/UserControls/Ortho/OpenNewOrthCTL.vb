Imports System.Data.SqlClient
Imports Dapper




Public Class OpenNewOrthCTL

    ' Layout / resize: only FullOrthoTreating.ResizeControlsProportionally should scale this tree.
    ' Scaling here as well doubled every child SetBounds and broke alignment when maximized.

    Dim stBite, stclass As String
    Dim CellStr As String = ""
    Private PatientID As Integer
    'Dim row As DsOrth.PatientRow
    Public Event BeginOrthTrt(ByVal sender As Object, ByVal e As BeginOrthTrtEventArgs)


    Private Sub NewOrth_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Try
            If PatientID > 0 Then
                LoadData(PatientID)
            End If

            IntegerMoneyEditorFocus.ConfigureIntegerMoneyTextEdit(txtPayValue)
            IntegerMoneyEditorFocus.AttachTextEditZeroEmptyElseSelectAll(txtPayValue)
            IntegerMoneyEditorFocus.ConfigureIntegerMoneyTextEdit(txtTrtPrice)
            IntegerMoneyEditorFocus.AttachTextEditZeroEmptyElseSelectAll(txtTrtPrice)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        OrthoEmbeddedChrome.ApplyToRoot(Me)
    End Sub


    Public WriteOnly Property ValueFromParent() As Integer
        Set(ByVal Value As Integer)
            PatientID = Value
            LoadData(Value)
        End Set
    End Property

    Private ReadOnly Property PatientName As String
        Get
            Dim parentFull = TryCast(Me.Parent, FullOrthoTreating)
            If parentFull IsNot Nothing AndAlso parentFull.CurrentPatient IsNot Nothing Then
                Return parentFull.CurrentPatient.PatientName
            End If
            Dim p = FormManager.Instance.GetCurrentPatient()
            Return If(p IsNot Nothing, p.PatientName, "")
        End Get
    End Property

    Private ReadOnly Property CurrentPatient As Patient
        Get
            Dim parentFull = TryCast(Me.Parent, FullOrthoTreating)
            If parentFull IsNot Nothing Then Return parentFull.CurrentPatient
            Return FormManager.Instance.GetCurrentPatient()
        End Get
    End Property

    Private clsOrthoInf As New OrthoInf
    Private clsOrthoInfDATA As New OrthoInfDATA
    Private clsOrthoDiag As New OrthoDiag
    Private clsOrthoDiagDATA As New OrthoDiagDATA
    Public Sub LoadData(ByVal PatientID As Integer)
        Try
            Dim orthoInfs = clsOrthoInfDATA.SelectAll().Where(Function(x) x.PatientID = PatientID).ToList()
            Dim orthoDiags = clsOrthoDiagDATA.SelectAll().Where(Function(x) x.PatientID = PatientID).ToList()

            ' You can bind them to your controls or DataSources as needed
            ' Example:
            OrthoInfBindingSource.DataSource = orthoInfs
            OrthoDiagBindingSource.DataSource = orthoDiags

        Catch ex As System.Data.SqlClient.SqlException
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub btAddOrtho_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btAddOrtho.Click
        If orthoDate.EditValue Is Nothing OrElse orthoDate.Text.Length < 8 Then
            If Eng Then
                MsgBox("Please select Orthodont Date")
            Else
                MsgBox("الرجاء تحديد تاريخ التقويم")
            End If
            Exit Sub
        End If
        SaveOrthoWithTransaction(sender, PatientID)

    End Sub

    Public Function SaveOrthoWithTransaction(sender As Object, PatientID As Integer) As Boolean
        Dim saved As Boolean = False
        Dim clsTrtsData As New Patient_TrtsDATA
        Dim Dx = New DentistXDATA
        Dim ConnectionString = Dx.GetConnectionString

        Using conn As New SqlConnection(ConnectionString)
            conn.Open()
            Using trans As SqlTransaction = conn.BeginTransaction("OrthoSave")
                Try
                    ' Prepare OrthoInf instance
                    Dim newOrthoInf As New OrthoInf With {
                    .PatientID = PatientID,
                    .Compliants = CompliantsTextBox.Text,
                    .Birth = BirthTextBox.Text,
                    .Feed = FeedTextBox.Text,
                    .MilkTeethChng = MilkTeethChngTextBox.Text,
                    .MilkTeethAppear = MilkTeethAppearTextBox.Text,
                    .TeethLoss = TeethLossTextBox.Text,
                    .BurriedTeeth = BurriedTeethTextBox.Text,
                    .OverLoadTeeth = OverLoadTeethTextBox.Text,
                    .LipsCut = LipsCutTextBox.Text,
                    .ThroatCut = ThroatCutTextBox.Text,
                    .IllnesPeriod = IllnesPeriodTextBox.Text,
                    .CousinsHFactor = CousinsHFactorTextBox.Text,
                    .BadHabits = BadHabitsTextBox.Text,
                    .Malfunction = MalfunctionTextBox.Text,
                    .Khota = txtKhota.Text,
                    .PrevOrth = txtPervOrth.Text,
                    .PrevIll = txtPrevIll.Text,
                    .TreatDate = orthoDate.DateTime
                }

                    ' Prepare OrthoDiag instance (OrthoID set after OrthoInf insert)
                    Dim newOrthoDiag As New OrthoDiag With {
                    .PatientID = PatientID,
                    .CloseType = txtClosing.Text,
                    .ClassI = stclass,
                    .Bite = txtBite.Text
                }

                    ' Insert into database and get OrthoID
                    Dim lastOrthoID As Integer = clsOrthoInfDATA.AddTransAndGetID(conn, trans, newOrthoInf)
                    If lastOrthoID <= 0 Then
                        MsgBox("Failed to insert OrthoInf record - no ID returned")
                        trans.Rollback()
                        Return False
                    End If

                    newOrthoDiag.OrthoID = lastOrthoID
                    Dim insertedDiag = clsOrthoDiagDATA.AddTrans(conn, trans, newOrthoDiag)
                    If Not insertedDiag Then
                        MsgBox("Failed to insert OrthoDiag record")
                        trans.Rollback()
                        Return False
                    End If

                    ' Treatment value and payment
                    Dim trtValue As Double = Val(txtTrtPrice.Text)
                    Dim payValue As Double = Val(txtPayValue.Text)

                    ' Payment logic
                    Dim payNote As String = ""
                    If payValue > 0 OrElse trtValue > 0 Then
                        If payValue = trtValue Then
                            payNote = "Payed In Full"
                        ElseIf payValue < trtValue AndAlso payValue > 0 Then
                            payNote = "Payed Partially"
                        ElseIf payValue > trtValue Then
                            payNote = "Payed With Advance Payment"
                        End If
                    End If

                    ' Create treatment record
                    Dim detailText = "Ortho Detail ==>> " & newOrthoDiag.ClassI 'txtOrthoTreatDet.Text
                    Dim clsPatientTrts As New Patient_Trts With {
                    .OrthoID = lastOrthoID,  ' This is now set correctly
                    .OtherTrtID = 0,
                    .ToothTrtID = 0,
                    .Detail = detailText,
                    .PatientID = PatientID,
                    .TrtDate = CDate(orthoDate.EditValue),
                    .TrtValue = trtValue,
                    .IsMultiTooth = True
                }

                    ' Use the new method that returns the TrtID
                    Dim lastTrtID As Integer = clsTrtsData.AddTransactionalAndGetID(conn, trans, clsPatientTrts)
                    If lastTrtID <= 0 Then
                        MsgBox($"Failed to save Patient_Trts - no ID returned")
                        trans.Rollback()
                        Return False
                    End If

                    ' Payment saving logic
                    If payValue > 0 Then
                        Dim clsPatientPays As New Patient_Pays With {
                        .Notes = payNote,
                        .PatientID = PatientID,
                        .PayDate = CDate(orthoDate.EditValue),
                        .PayValue = payValue,
                        .TrtID = lastTrtID  ' This is now set correctly
                    }

                        Dim clsPatientPaysData As New Patient_PaysDATA
                        If Not clsPatientPaysData.AddTransactional(conn, trans, clsPatientPays) Then
                            MsgBox($"Failed to save Patient_Payment")
                            trans.Rollback()
                            Return False
                        End If
                    End If

                    ' Update and commit
                    trans.Commit()

                    ' These should happen after successful commit
                    clsOrthoDiagDATA.UpdateOrtho(True, PatientID)
                    LoadData(PatientID)
                    saved = True
                    RaiseEvent BeginOrthTrt(sender, New BeginOrthTrtEventArgs(PatientID, PatientName))
                    FormManager.Instance.CurrentForm.UpdatePatientBalance(CurrentPatient)
                Catch ex As Exception
                    ' Rollback transaction first
                    Try
                        trans.Rollback()
                    Catch rollbackEx As Exception
                        MsgBox($"Rollback failed: {rollbackEx.Message}")
                    End Try

                    Dim msg As String = If(Eng,
                    $"Save failed: {ex.Message}",
                    $"فشل الحفظ: {ex.Message}")
                    MessageBox.Show(msg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    saved = False
                End Try
            End Using
        End Using

        Return saved
    End Function







#Region "DiagGrp"

    Private Sub ClosingCombo_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ClosingCombo.SelectedIndexChanged

        If Me.ClosingCombo.SelectedIndex = -1 Then
            DiagCntlsDes()
        ElseIf Me.ClosingCombo.SelectedIndex = 0 Then
            DiagCntlsEn()
            Me.txtClosing.Text = Me.ClosingCombo.Text
        ElseIf Me.ClosingCombo.SelectedIndex = 1 Then
            DiagCntlsDes()
        End If
    End Sub

    Private Sub Clas1Combo_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Clas1Combo.SelectedIndexChanged
        If Me.Clas1Combo.SelectedIndex = -1 Then
            DiagCntlsEn()
            Me.txtClas1.Text = ""
        Else
            Me.Clas2Combo.Enabled = False
            Me.Clas2Combo.SelectedIndex = -1
            Me.Clas2DetCombo.Enabled = False
            Me.Clas2DetCombo.SelectedIndex = -1
            Me.txtClas2.Enabled = False
            Me.txtClas2.Text = ""
            Me.Clas3Combo.Enabled = False
            Clas3Combo.SelectedIndex = -1
            Me.txtClas3.Enabled = False
            Me.txtClas3.Text = ""
            Me.txtClas1.Text = Me.Label2.Text & "-" & Me.Clas1Combo.Text
            stclass = Me.txtClas1.Text
        End If

    End Sub

    Private Sub Clas2Combo_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Clas2Combo.SelectedIndexChanged
        If Me.Clas2Combo.SelectedIndex = -1 Then
            DiagCntlsEn()
        ElseIf Me.Clas2Combo.SelectedIndex = 0 Then
            Me.Clas2DetCombo.Properties.Items.Clear()
            Me.Clas2DetCombo.Text = ""
            If Eng Then
                Me.Clas2DetCombo.Properties.Items.Add("Proclination")
            Else
                Me.Clas2DetCombo.Properties.Items.Add("إندفاع")
            End If

            Me.Clas2DetCombo.SelectedIndex = -1
            Me.Clas1Combo.Enabled = False
            Me.Clas1Combo.SelectedIndex = -1
            Me.txtClas1.Enabled = False
            Me.txtClas1.Text = ""
            Me.txtClas2.Enabled = True
            ' Me.txtClas2.Text = Me.Clas2Combo.Text
            Me.Clas3Combo.Enabled = False
            Clas3Combo.SelectedIndex = -1
            Me.txtClas3.Enabled = False
            Me.txtClas3.Text = ""
        ElseIf Me.Clas2Combo.SelectedIndex = 1 Then
            Me.Clas2DetCombo.Properties.Items.Clear()
            Me.Clas2DetCombo.Text = ""
            If Eng Then
                Me.Clas2DetCombo.Properties.Items.Add("Retroclination")
            Else
                Me.Clas2DetCombo.Properties.Items.Add("إنقباض")
            End If

            Me.Clas2DetCombo.SelectedIndex = -1
            Me.Clas1Combo.Enabled = False
            Me.Clas1Combo.SelectedIndex = -1
            Me.txtClas1.Enabled = False
            Me.txtClas1.Text = ""
            Me.txtClas2.Enabled = True
            ' Me.txtClas2.Text = Me.Clas2Combo.Text
            Me.Clas3Combo.Enabled = False
            Clas3Combo.SelectedIndex = -1
            Me.txtClas3.Enabled = False
            Me.txtClas3.Text = ""
        End If
    End Sub

    Private Sub Clas2DetCombo_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Clas2DetCombo.SelectedIndexChanged
        Me.txtClas2.Text = Me.Label3.Text & "-" & Me.Clas2Combo.Text & "-" & Me.Clas2DetCombo.Text
    End Sub

    Private Sub Clas3Combo_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Clas3Combo.SelectedIndexChanged
        If Me.Clas3Combo.SelectedIndex = -1 Then
            DiagCntlsEn()
        ElseIf Me.Clas3Combo.SelectedIndex = 0 Then

            Me.Clas2DetCombo.Enabled = False
            Me.Clas2DetCombo.SelectedIndex = -1
            Me.Clas1Combo.Enabled = False
            Me.Clas1Combo.SelectedIndex = -1
            Me.txtClas1.Enabled = False
            Me.txtClas1.Text = ""
            Me.txtClas3.Enabled = True

            Me.Clas2Combo.Enabled = False
            Clas2Combo.SelectedIndex = -1
            Me.txtClas2.Enabled = False
            Me.txtClas2.Text = ""
            Me.txtClas3.Text = Me.Label4.Text & "-" & Me.Clas3Combo.Text
        ElseIf Me.Clas3Combo.SelectedIndex = 1 Then

            Me.Clas2DetCombo.Enabled = False
            Me.Clas2DetCombo.SelectedIndex = -1
            Me.Clas1Combo.Enabled = False
            Me.Clas1Combo.SelectedIndex = -1
            Me.txtClas1.Enabled = False
            Me.txtClas1.Text = ""
            Me.txtClas3.Enabled = True

            Me.Clas2Combo.Enabled = False
            Clas2Combo.SelectedIndex = -1
            Me.txtClas2.Enabled = False
            Me.txtClas2.Text = ""
            Me.txtClas3.Text = Me.Label4.Text & "-" & Me.Clas3Combo.Text
        End If
    End Sub

    Private Sub BiteCombo_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BiteCombo.SelectedIndexChanged
        If Me.BiteCombo.SelectedIndex = 0 Then
            Me.OpenBiteCombo.BringToFront()
            Me.OpenBiteCombo.Visible = True
        ElseIf Me.BiteCombo.SelectedIndex = 1 Then
            Me.DeepBiteCombo.BringToFront()
            Me.DeepBiteCombo.Visible = True
            Me.OpenBiteCombo.Visible = False
            Me.OpenBiteDetCombo.Visible = False

        ElseIf Me.BiteCombo.SelectedIndex = 2 Then
            Me.ReverseBiteCombo.BringToFront()
            Me.ReverseBiteCombo.Visible = True
            Me.OpenBiteCombo.Visible = False
            Me.OpenBiteDetCombo.Visible = False
            Me.txtBite.Text = Me.BiteCombo.Text & "-" & Me.OpenBiteCombo.Text
        Else
            Me.OpenBiteCombo.Visible = False
            Me.DeepBiteCombo.Visible = False
            Me.ReverseBiteCombo.Visible = False
        End If
    End Sub

    Private Sub OpenBiteCombo_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OpenBiteCombo.SelectedIndexChanged
        If Me.OpenBiteCombo.SelectedIndex = 0 Then
            Me.DeepBiteCombo.Visible = False
            Me.ReverseBiteCombo.Visible = False
            Me.OpenBiteDetCombo.Visible = False
            Me.txtBite.Text = Me.BiteCombo.Text & "-" & Me.OpenBiteCombo.Text
        ElseIf Me.OpenBiteCombo.SelectedIndex = 1 Then
            Me.DeepBiteCombo.Visible = False
            Me.ReverseBiteCombo.Visible = False
            Me.OpenBiteDetCombo.Visible = False
            Me.txtBite.Text = Me.BiteCombo.Text & "-" & Me.OpenBiteCombo.Text
        ElseIf Me.OpenBiteCombo.SelectedIndex = 2 Then
            Me.DeepBiteCombo.Visible = False
            Me.ReverseBiteCombo.Visible = False
            Me.OpenBiteDetCombo.Visible = True
            Me.txtBite.Text = Me.BiteCombo.Text & "-" & Me.OpenBiteCombo.Text & "-" & Me.OpenBiteDetCombo.Text
        ElseIf Me.OpenBiteCombo.SelectedIndex = 3 Then
            Me.DeepBiteCombo.Visible = False
            Me.ReverseBiteCombo.Visible = False
            Me.OpenBiteDetCombo.Visible = True
            Me.txtBite.Text = Me.BiteCombo.Text & "-" & Me.OpenBiteCombo.Text & "-" & Me.OpenBiteDetCombo.Text
        End If
    End Sub

    Private Sub OpenBiteDetCombo_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OpenBiteDetCombo.SelectedIndexChanged
        Me.txtBite.Text = Me.BiteCombo.Text & "-" & Me.OpenBiteCombo.Text & "-" & Me.OpenBiteDetCombo.Text

    End Sub
    Private Sub DeepBiteCombo_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DeepBiteCombo.SelectedIndexChanged
        Me.txtBite.Text = Me.BiteCombo.Text & "-" & Me.DeepBiteCombo.Text
    End Sub
    Private Sub ReverseBiteCombo_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ReverseBiteCombo.SelectedIndexChanged
        Me.txtBite.Text = Me.BiteCombo.Text & "-" & Me.ReverseBiteCombo.Text
    End Sub

    Public Sub DiagCntlsEn()
        Me.txtClosing.Enabled = True
        Me.txtClas1.Enabled = True
        Me.txtClas2.Enabled = True
        Me.txtClas3.Enabled = True
        Me.txtBite.Enabled = True
        Me.txtClosing.Text = ""
        Me.txtClas1.Text = ""
        Me.txtClas2.Text = ""
        Me.txtClas3.Text = ""
        Me.txtBite.Text = ""
        Me.Clas1Combo.Enabled = True
        Me.Clas1Combo.SelectedIndex = -1
        Me.Clas2Combo.Enabled = True
        Me.Clas2Combo.SelectedIndex = -1
        Me.Clas2DetCombo.Enabled = True
        Me.Clas2DetCombo.SelectedIndex = -1
        Me.Clas3Combo.Enabled = True
        Me.Clas3Combo.SelectedIndex = -1
        Me.OpenBiteCombo.Enabled = True
        Me.OpenBiteCombo.SelectedIndex = -1
        Me.OpenBiteDetCombo.Enabled = True
        Me.OpenBiteDetCombo.SelectedIndex = -1
        Me.DeepBiteCombo.Enabled = True
        Me.DeepBiteCombo.SelectedIndex = -1
        Me.ReverseBiteCombo.Enabled = True
        Me.ReverseBiteCombo.SelectedIndex = -1
        Me.BiteCombo.Enabled = True
        Me.BiteCombo.SelectedIndex = -1

    End Sub
    Public Sub DiagCntlsDes()
        Me.BiteCombo.SelectedIndex = -1
        Me.ReverseBiteCombo.SelectedIndex = -1
        Me.DeepBiteCombo.SelectedIndex = -1
        Me.OpenBiteDetCombo.SelectedIndex = -1
        Me.OpenBiteCombo.SelectedIndex = -1
        Me.Clas1Combo.SelectedIndex = -1
        Me.Clas2Combo.SelectedIndex = -1
        Me.Clas3Combo.SelectedIndex = -1
        Me.Clas2DetCombo.SelectedIndex = -1
        Me.Clas1Combo.Enabled = False
        Me.Clas2Combo.Enabled = False
        Me.Clas2DetCombo.Enabled = False
        Me.Clas3Combo.Enabled = False
        Me.OpenBiteCombo.Enabled = False
        Me.DeepBiteCombo.Enabled = False
        Me.ReverseBiteCombo.Enabled = False
        Me.BiteCombo.Enabled = False
        Me.OpenBiteDetCombo.Visible = False
        Me.txtClosing.Text = ""
        Me.txtClas1.Text = ""
        Me.txtClas2.Text = ""
        Me.txtClas3.Text = ""
        Me.txtBite.Text = ""
        Me.txtClas1.Enabled = False
        Me.txtClas2.Enabled = False
        Me.txtClas3.Enabled = False
        Me.txtBite.Enabled = False
    End Sub
    Private Sub btResetDiag_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btResetDiag.Click
        Me.ClosingCombo.SelectedIndex = -1
    End Sub



    Private Sub txtClas1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtClas1.TextChanged
        If Me.txtClas1.Text.Length > 0 Then
            stclass = Me.txtClas1.Text
        Else
            Exit Sub
        End If
    End Sub

    Private Sub txtClas2_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtClas2.TextChanged
        If Me.txtClas2.Text.Length > 0 Then
            stclass = Me.txtClas2.Text
        Else
            Exit Sub
        End If
    End Sub

    Private Sub txtClas3_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtClas3.TextChanged
        If Me.txtClas3.Text.Length > 0 Then
            stclass = Me.txtClas3.Text
        Else
            Exit Sub
        End If
    End Sub







#End Region

#Region "OrthInfoGrp"
    Private Sub CompliantsCombo_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CompliantsCombo.SelectedIndexChanged
        Me.CompliantsTextBox.Text = Me.CompliantsCombo.Text
    End Sub
    Private Sub BirthCombo_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BirthCombo.SelectedIndexChanged
        Me.BirthTextBox.Text = Me.BirthCombo.Text
    End Sub
    Private Sub BreastCombo_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BreastCombo.SelectedIndexChanged
        Me.FeedTextBox.Text = Me.BreastCombo.Text
    End Sub
    Private Sub TeethChCombo_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TeethChCombo.SelectedIndexChanged
        Me.MilkTeethChngTextBox.Text = Me.TeethChCombo.Text
    End Sub
    Private Sub TeethApprCombo_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TeethApprCombo.SelectedIndexChanged
        Me.MilkTeethAppearTextBox.Text = Me.TeethApprCombo.Text
    End Sub
    Private Sub TeethLosCombo_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TeethLosCombo.SelectedIndexChanged
        Me.TeethLossTextBox.Text &= Me.TeethLosCombo.Text & " "
    End Sub
    Private Sub TeethBurdCombo_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TeethBurdCombo.SelectedIndexChanged
        Me.BurriedTeethTextBox.Text &= Me.TeethBurdCombo.Text & " "
    End Sub
    Private Sub TeethOvrCombo_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TeethOvrCombo.SelectedIndexChanged
        Me.OverLoadTeethTextBox.Text &= Me.TeethOvrCombo.Text & " "
    End Sub

    Private Sub IllnesCombo_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles IllnesCombo.SelectedIndexChanged
        Me.IllnesPeriodTextBox.Text = Me.IllnesCombo.Text
    End Sub


    Private Sub RadioLips_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioLips.SelectedIndexChanged
        If Me.RadioLips.SelectedIndex = 1 Then
            If Eng Then
                Me.LipsCutTextBox.Text = "NO"
            Else
                Me.LipsCutTextBox.Text = "لا"
            End If
        Else
            If Eng Then
                Me.LipsCutTextBox.Text = "YES"
            Else
                Me.LipsCutTextBox.Text = "نعم"
            End If
        End If
    End Sub


    Private Sub RadioThroat_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioThroat.SelectedIndexChanged
        If Me.RadioThroat.SelectedIndex = 1 Then
            If Eng Then
                Me.ThroatCutTextBox.Text = "NO"
            Else
                Me.ThroatCutTextBox.Text = "لا"
            End If
        Else
            If Eng Then
                Me.ThroatCutTextBox.Text = "YES"
            Else
                Me.ThroatCutTextBox.Text = "نعم"
            End If
        End If
    End Sub

    Private Sub RadioCusin_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioCusin.SelectedIndexChanged
        If Me.RadioCusin.SelectedIndex = 1 Then
            If Eng Then
                Me.CousinsHFactorTextBox.Text = "NO"
            Else
                Me.CousinsHFactorTextBox.Text = "لا"
            End If
        Else
            If Eng Then
                Me.CousinsHFactorTextBox.Text = "YES"
            Else
                Me.CousinsHFactorTextBox.Text = "نعم"
            End If
        End If
    End Sub

    Private Sub RadioPrevOrth_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioPrevOrth.SelectedIndexChanged
        If Me.RadioPrevOrth.SelectedIndex = 1 Then
            If Eng Then
                Me.txtPervOrth.Text = "NO"
            Else
                Me.txtPervOrth.Text = "لا"
            End If
        Else
            If Eng Then
                Me.txtPervOrth.Text = "YES"
            Else
                Me.txtPervOrth.Text = "نعم"
            End If
        End If
    End Sub




    Private Sub BadHabCombo_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BadHabCombo.SelectedIndexChanged
        Me.BadHabitsTextBox.Text = Me.BadHabCombo.Text
    End Sub


    Private Sub MalfuncCombo_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MalfuncCombo.SelectedIndexChanged
        Me.MalfunctionTextBox.Text = Me.MalfuncCombo.Text
    End Sub


    'Private Sub lnkUseNewOrtho_Click(sender As Object, e As DevExpress.Utils.HyperlinkClickEventArgs) Handles lnkUseNewOrtho.Click
    '    Try
    '        FormManager.Instance.SwitchUserControl(GetType(FullOrtho), "Ortho")
    '    Catch ex As Exception
    '        MsgBox(ex.Message)
    '    End Try
    'End Sub

    Private Sub btRestINF_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btRestINF.Click
        CompliantsTextBox.Text = ""
        BirthTextBox.Text = ""
        FeedTextBox.Text = ""
        MilkTeethChngTextBox.Text = ""
        MilkTeethAppearTextBox.Text = ""
        TeethLossTextBox.Text = ""
        BurriedTeethTextBox.Text = ""
        OverLoadTeethTextBox.Text = ""
        LipsCutTextBox.Text = ""
        ThroatCutTextBox.Text = ""
        IllnesPeriodTextBox.Text = ""
        CousinsHFactorTextBox.Text = ""
        BadHabitsTextBox.Text = ""
        MalfunctionTextBox.Text = ""
        Me.txtKhota.Text = ""
        CompliantsCombo.SelectedIndex = -1
        BirthCombo.SelectedIndex = -1
        BreastCombo.SelectedIndex = -1
        TeethBurdCombo.SelectedIndex = -1
        TeethChCombo.SelectedIndex = -1
        TeethLosCombo.SelectedIndex = -1
        TeethOvrCombo.SelectedIndex = -1
        TeethApprCombo.SelectedIndex = -1
        RadioLips.SelectedIndex = 1
        RadioThroat.SelectedIndex = 1
        IllnesCombo.SelectedIndex = -1
        RadioCusin.SelectedIndex = 1
        BadHabCombo.SelectedIndex = -1
        MalfuncCombo.SelectedIndex = -1
    End Sub






#End Region




End Class
