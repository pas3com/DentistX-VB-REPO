Imports System.Data.SqlClient
Imports System.Linq
Imports DevExpress.CodeParser

Public Class FrmEditOrthInfDiag

    Private _patientId As Integer
    Private _orthoId As Integer
    Private _trtId As Integer
    Private _payId As Integer
    Private _bindingsAdded As Boolean = False

    Public Sub InitForEdit(inf As OrthoInf, diag As OrthoDiag)
        If inf Is Nothing Then Throw New ArgumentNullException(NameOf(inf))
        If diag Is Nothing Then Throw New ArgumentNullException(NameOf(diag))

        _patientId = inf.PatientID
        _orthoId = inf.OrthoID

        OrthoInfBindingSource.DataSource = New List(Of OrthoInf) From {inf}
        OrthoDiagBindingSource.DataSource = New List(Of OrthoDiag) From {diag}

        AddDataBindingsIfNeeded()

        If inf.TreatDate <> Date.MinValue Then
            orthoDate.EditValue = inf.TreatDate
        End If

        LoadFinancialData()
    End Sub

    Private Sub AddDataBindingsIfNeeded()
        If _bindingsAdded Then Return

        ' OrthoInf bindings – keep in sync with OrthoTreatingCTL.DataBind
        CompliantsTextBox.DataBindings.Add(New Binding("Text", OrthoInfBindingSource, "Compliants", True))
        BirthTextBox.DataBindings.Add(New Binding("Text", OrthoInfBindingSource, "Birth", True))
        FeedTextBox.DataBindings.Add(New Binding("Text", OrthoInfBindingSource, "Feed", True))
        MilkTeethChngTextBox.DataBindings.Add(New Binding("Text", OrthoInfBindingSource, "MilkTeethChng", True))
        MilkTeethAppearTextBox.DataBindings.Add(New Binding("Text", OrthoInfBindingSource, "MilkTeethAppear", True))
        TeethLossTextBox.DataBindings.Add(New Binding("Text", OrthoInfBindingSource, "TeethLoss", True))
        BurriedTeethTextBox.DataBindings.Add(New Binding("Text", OrthoInfBindingSource, "BurriedTeeth", True))
        OverLoadTeethTextBox.DataBindings.Add(New Binding("Text", OrthoInfBindingSource, "OverLoadTeeth", True))
        LipsCutTextBox.DataBindings.Add(New Binding("Text", OrthoInfBindingSource, "LipsCut", True))
        ThroatCutTextBox.DataBindings.Add(New Binding("Text", OrthoInfBindingSource, "ThroatCut", True))
        IllnesPeriodTextBox.DataBindings.Add(New Binding("Text", OrthoInfBindingSource, "IllnesPeriod", True))
        CousinsHFactorTextBox.DataBindings.Add(New Binding("Text", OrthoInfBindingSource, "CousinsHFactor", True))
        BadHabitsTextBox.DataBindings.Add(New Binding("Text", OrthoInfBindingSource, "BadHabits", True))
        MalfunctionTextBox.DataBindings.Add(New Binding("Text", OrthoInfBindingSource, "Malfunction", True))
        txtKhota.DataBindings.Add(New Binding("Text", OrthoInfBindingSource, "Khota", True))
        txtPervOrth.DataBindings.Add(New Binding("Text", OrthoInfBindingSource, "PrevOrth", True))
        txtPrevIll.DataBindings.Add(New Binding("Text", OrthoInfBindingSource, "PrevIll", True))

        ' Some OrthoDiag bindings already exist in designer (txtClas1, txtClosing, txtBite)
        ' We only ensure they are hooked to the binding source
        If txtClas1.DataBindings.Count = 0 Then
            txtClas1.DataBindings.Add(New Binding("Text", OrthoDiagBindingSource, "ClassI", True))
        End If
        If txtClosing.DataBindings.Count = 0 Then
            txtClosing.DataBindings.Add(New Binding("Text", OrthoDiagBindingSource, "CloseType", True))
        End If
        If txtBite.DataBindings.Count = 0 Then
            txtBite.DataBindings.Add(New Binding("Text", OrthoDiagBindingSource, "Bite", True))
        End If

        _bindingsAdded = True
    End Sub

    Private Sub LoadFinancialData()
        Dim dx As New DentistXDATA()
        Dim cnStr = dx.GetConnectionString()

        Using cn As New SqlConnection(cnStr)
            cn.Open()

            ' Load main ortho treatment row related to this OrthoID
            Using cmd As New SqlCommand("SELECT TOP 1 TrtID, TrtDate, TrtValue, Detail FROM Patient_Trts WHERE PatientID=@PatientID AND OrthoID=@OrthoID ORDER BY TrtDate", cn)
                cmd.Parameters.AddWithValue("@PatientID", _patientId)
                cmd.Parameters.AddWithValue("@OrthoID", _orthoId)

                Using rd = cmd.ExecuteReader()
                    If rd.Read() Then
                        _trtId = Convert.ToInt32(rd("TrtID"))

                        If Not Convert.IsDBNull(rd("TrtValue")) Then
                            txtTrtPrice.Text = Convert.ToDecimal(rd("TrtValue")).ToString()
                        End If
                        If Not Convert.IsDBNull(rd("Detail")) Then
                            txtOrthoPriceDet.Text = Convert.ToString(rd("Detail"))
                        End If
                        If orthoDate.EditValue Is Nothing OrElse String.IsNullOrWhiteSpace(orthoDate.Text) Then
                            If Not Convert.IsDBNull(rd("TrtDate")) Then
                                orthoDate.EditValue = CDate(rd("TrtDate"))
                            End If
                        End If
                    End If
                End Using
            End Using

            ' Load single payment row (initial ortho payment) if it exists
            If _trtId > 0 Then
                Using cmdPay As New SqlCommand("SELECT TOP 1 PayID, PayValue, Notes, PayDate FROM Patient_Pays WHERE TrtID=@TrtID ORDER BY PayDate", cn)
                    cmdPay.Parameters.AddWithValue("@TrtID", _trtId)

                    Using rdPay = cmdPay.ExecuteReader()
                        If rdPay.Read() Then
                            _payId = Convert.ToInt32(rdPay("PayID"))

                            If Not Convert.IsDBNull(rdPay("PayValue")) Then
                                txtPayValue.Text = Convert.ToDecimal(rdPay("PayValue")).ToString()
                            End If
                            If Not Convert.IsDBNull(rdPay("Notes")) Then
                                txtPayNotes.Text = Convert.ToString(rdPay("Notes"))
                            End If
                        End If
                    End Using
                End Using
            End If
        End Using
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            Me.Validate()
            FlushOrthoDiagCombosToBoundTextEdits()
            OrthoInfBindingSource.EndEdit()
            OrthoDiagBindingSource.EndEdit()

            Dim updatedInf = TryCast(OrthoInfBindingSource.Current, OrthoInf)
            Dim updatedDiag = TryCast(OrthoDiagBindingSource.Current, OrthoDiag)
            If updatedInf Is Nothing OrElse updatedDiag Is Nothing Then
                MsgBox("No orthodontic record selected.")
                Return
            End If

            Dim dx As New DentistXDATA()
            Dim cnStr = dx.GetConnectionString()

            Using cn As New SqlConnection(cnStr)
                cn.Open()
                Using tr = cn.BeginTransaction()
                    Try
                        ' Update OrthoInf
                        Dim infCmd As New SqlCommand("UPDATE OrthoInf SET Compliants=@Compliants, Birth=@Birth, Feed=@Feed, MilkTeethChng=@MilkTeethChng, MilkTeethAppear=@MilkTeethAppear, TeethLoss=@TeethLoss, BurriedTeeth=@BurriedTeeth, OverLoadTeeth=@OverLoadTeeth, LipsCut=@LipsCut, ThroatCut=@ThroatCut, IllnesPeriod=@IllnesPeriod, CousinsHFactor=@CousinsHFactor, BadHabits=@BadHabits, Malfunction=@Malfunction, Khota=@Khota, PrevOrth=@PrevOrth, PrevIll=@PrevIll, TreatDate=@TreatDate WHERE OrthoID=@OrthoID AND PatientID=@PatientID", cn, tr)
                        infCmd.Parameters.AddWithValue("@Compliants", updatedInf.Compliants)
                        infCmd.Parameters.AddWithValue("@Birth", updatedInf.Birth)
                        infCmd.Parameters.AddWithValue("@Feed", updatedInf.Feed)
                        infCmd.Parameters.AddWithValue("@MilkTeethChng", updatedInf.MilkTeethChng)
                        infCmd.Parameters.AddWithValue("@MilkTeethAppear", updatedInf.MilkTeethAppear)
                        infCmd.Parameters.AddWithValue("@TeethLoss", updatedInf.TeethLoss)
                        infCmd.Parameters.AddWithValue("@BurriedTeeth", updatedInf.BurriedTeeth)
                        infCmd.Parameters.AddWithValue("@OverLoadTeeth", updatedInf.OverLoadTeeth)
                        infCmd.Parameters.AddWithValue("@LipsCut", updatedInf.LipsCut)
                        infCmd.Parameters.AddWithValue("@ThroatCut", updatedInf.ThroatCut)
                        infCmd.Parameters.AddWithValue("@IllnesPeriod", updatedInf.IllnesPeriod)
                        infCmd.Parameters.AddWithValue("@CousinsHFactor", updatedInf.CousinsHFactor)
                        infCmd.Parameters.AddWithValue("@BadHabits", updatedInf.BadHabits)
                        infCmd.Parameters.AddWithValue("@Malfunction", updatedInf.Malfunction)
                        infCmd.Parameters.AddWithValue("@Khota", updatedInf.Khota)
                        infCmd.Parameters.AddWithValue("@PrevOrth", updatedInf.PrevOrth)
                        infCmd.Parameters.AddWithValue("@PrevIll", updatedInf.PrevIll)
                        infCmd.Parameters.AddWithValue("@TreatDate", If(orthoDate.EditValue IsNot Nothing AndAlso Not String.IsNullOrWhiteSpace(orthoDate.Text), CDate(orthoDate.EditValue), CType(DBNull.Value, Object)))
                        infCmd.Parameters.AddWithValue("@OrthoID", updatedInf.OrthoID)
                        infCmd.Parameters.AddWithValue("@PatientID", updatedInf.PatientID)
                        infCmd.ExecuteNonQuery()

                        ' Update OrthoDiag — use @OrthoDiagClass1 (digit 1) for the value; @ClassI vs @Classl is ambiguous to some parsers/clients.
                        Dim diagCmd As New SqlCommand("UPDATE OrthoDiag SET CloseType=@CloseType, [ClassI]=@OrthoDiagClass1, Bite=@Bite WHERE OrthoID=@OrthoID AND PatientID=@PatientID", cn, tr)
                        diagCmd.Parameters.AddWithValue("@CloseType", If(updatedDiag.CloseType, CType(DBNull.Value, Object)))
                        diagCmd.Parameters.AddWithValue("@OrthoDiagClass1", If(updatedDiag.ClassI, CType(DBNull.Value, Object)))
                        diagCmd.Parameters.AddWithValue("@Bite", If(updatedDiag.Bite, CType(DBNull.Value, Object)))
                        diagCmd.Parameters.AddWithValue("@OrthoID", updatedDiag.OrthoID)
                        diagCmd.Parameters.AddWithValue("@PatientID", updatedDiag.PatientID)
                        diagCmd.ExecuteNonQuery()

                        ' Update related treatment price and detail if we know the TrtID
                        If _trtId > 0 Then
                            Dim trtValue As Decimal = 0D
                            Decimal.TryParse(txtTrtPrice.Text, trtValue)

                            Dim trtCmd As New SqlCommand("UPDATE Patient_Trts SET TrtValue=@TrtValue, Detail=@Detail, TrtDate=@TrtDate WHERE TrtID=@TrtID AND PatientID=@PatientID", cn, tr)
                            trtCmd.Parameters.AddWithValue("@TrtValue", trtValue)
                            trtCmd.Parameters.AddWithValue("@Detail", txtOrthoPriceDet.Text)
                            trtCmd.Parameters.AddWithValue("@TrtDate", If(orthoDate.EditValue IsNot Nothing AndAlso Not String.IsNullOrWhiteSpace(orthoDate.Text), CDate(orthoDate.EditValue), CType(DBNull.Value, Object)))
                            trtCmd.Parameters.AddWithValue("@TrtID", _trtId)
                            trtCmd.Parameters.AddWithValue("@PatientID", _patientId)
                            trtCmd.ExecuteNonQuery()

                            ' Update or insert payment row
                            Dim payValue As Decimal = 0D
                            Decimal.TryParse(txtPayValue.Text, payValue)

                            If _payId > 0 Then
                                Dim payCmd As New SqlCommand("UPDATE Patient_Pays SET PayValue=@PayValue, Notes=@Notes WHERE PayID=@PayID AND TrtID=@TrtID", cn, tr)
                                payCmd.Parameters.AddWithValue("@PayValue", payValue)
                                payCmd.Parameters.AddWithValue("@Notes", txtPayNotes.Text)
                                payCmd.Parameters.AddWithValue("@PayID", _payId)
                                payCmd.Parameters.AddWithValue("@TrtID", _trtId)
                                payCmd.ExecuteNonQuery()
                            ElseIf payValue > 0D Then
                                Dim payCmdIns As New SqlCommand("INSERT INTO Patient_Pays (PatientID, TrtID, PayDate, PayValue, Notes, ReceivedBy, IsReturned) VALUES (@PatientID, @TrtID, @PayDate, @PayValue, @Notes, @ReceivedBy, @IsReturned)", cn, tr)
                                payCmdIns.Parameters.AddWithValue("@PatientID", _patientId)
                                payCmdIns.Parameters.AddWithValue("@TrtID", _trtId)
                                payCmdIns.Parameters.AddWithValue("@PayDate", If(orthoDate.EditValue IsNot Nothing AndAlso Not String.IsNullOrWhiteSpace(orthoDate.Text), CDate(orthoDate.EditValue), Date.Now))
                                payCmdIns.Parameters.AddWithValue("@PayValue", payValue)
                                payCmdIns.Parameters.AddWithValue("@Notes", txtPayNotes.Text)
                                payCmdIns.Parameters.AddWithValue("@ReceivedBy", CType(DBNull.Value, Object))
                                payCmdIns.Parameters.AddWithValue("@IsReturned", False)
                                payCmdIns.ExecuteNonQuery()
                            End If
                        End If

                        tr.Commit()
                        MsgBox("Orthodontic information updated successfully.")
                        Me.DialogResult = DialogResult.OK
                        Me.Close()
                    Catch ex As Exception
                        Try
                            tr.Rollback()
                        Catch
                        End Try
                        MsgBox("Update failed: " & ex.Message)
                    End Try
                End Using
            End Using
        Catch ex As Exception
            MsgBox("Update failed: " & ex.Message)
        End Try
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.DialogResult = DialogResult.Cancel
        Me.Close()
    End Sub

    Dim stBite, stclass As String

#Region "DiagGrp"

    ''' <summary>Copies diagnosis combo selections into the three OrthoDiag-bound text edits (CloseType, ClassI, Bite).</summary>
    Private Sub FlushOrthoDiagCombosToBoundTextEdits()
        If ClosingCombo.SelectedIndex >= 0 Then
            txtClosing.Text = ClosingCombo.Text
        End If

        If Clas3Combo.SelectedIndex >= 0 Then
            Dim s As String = Label4.Text & "-" & Clas3Combo.Text
            txtClas1.Text = s
            txtClas3.Text = s
            stclass = s
        ElseIf Clas2Combo.SelectedIndex >= 0 Then
            Dim s As String = Label3.Text & "-" & Clas2Combo.Text
            If Clas2DetCombo.SelectedIndex >= 0 AndAlso Not String.IsNullOrWhiteSpace(Clas2DetCombo.Text) Then
                s &= "-" & Clas2DetCombo.Text
            End If
            txtClas1.Text = s
            txtClas2.Text = s
            stclass = s
        ElseIf Clas1Combo.SelectedIndex >= 0 Then
            Dim s As String = Label2.Text & "-" & Clas1Combo.Text
            txtClas1.Text = s
            stclass = s
        End If

        RefreshBiteTextFromCombos()
    End Sub

    Private Sub RefreshBiteTextFromCombos()
        If BiteCombo.SelectedIndex < 0 Then Return
        Dim parts As New List(Of String) From {BiteCombo.Text}
        Select Case BiteCombo.SelectedIndex
            Case 0
                If OpenBiteCombo.SelectedIndex >= 0 Then parts.Add(OpenBiteCombo.Text)
                If OpenBiteDetCombo.Visible AndAlso OpenBiteDetCombo.SelectedIndex >= 0 AndAlso Not String.IsNullOrWhiteSpace(OpenBiteDetCombo.Text) Then
                    parts.Add(OpenBiteDetCombo.Text)
                End If
            Case 1
                If DeepBiteCombo.SelectedIndex >= 0 Then parts.Add(DeepBiteCombo.Text)
            Case 2
                If ReverseBiteCombo.SelectedIndex >= 0 Then parts.Add(ReverseBiteCombo.Text)
        End Select
        txtBite.Text = String.Join("-", parts.Where(Function(p) Not String.IsNullOrWhiteSpace(p)))
    End Sub

    Private Sub ClosingCombo_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ClosingCombo.SelectedIndexChanged

        If Me.ClosingCombo.SelectedIndex = -1 Then
            DiagCntlsDes()
        ElseIf Me.ClosingCombo.SelectedIndex = 0 Then
            DiagCntlsEn()
            Me.txtClosing.Text = Me.ClosingCombo.Text
        ElseIf Me.ClosingCombo.SelectedIndex = 1 Then
            DiagCntlsDes()
            Me.txtClosing.Text = Me.ClosingCombo.Text
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
            Dim baseCls As String = Me.Label3.Text & "-" & Me.Clas2Combo.Text
            Me.txtClas1.Text = baseCls
            Me.txtClas2.Enabled = True
            Me.txtClas2.Text = baseCls
            stclass = baseCls
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
            Dim baseCls As String = Me.Label3.Text & "-" & Me.Clas2Combo.Text
            Me.txtClas1.Text = baseCls
            Me.txtClas2.Enabled = True
            Me.txtClas2.Text = baseCls
            stclass = baseCls
            Me.Clas3Combo.Enabled = False
            Clas3Combo.SelectedIndex = -1
            Me.txtClas3.Enabled = False
            Me.txtClas3.Text = ""
        End If
    End Sub

    Private Sub Clas2DetCombo_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Clas2DetCombo.SelectedIndexChanged
        Me.txtClas2.Text = Me.Label3.Text & "-" & Me.Clas2Combo.Text & "-" & Me.Clas2DetCombo.Text
        Me.txtClas1.Text = Me.txtClas2.Text
        stclass = Me.txtClas2.Text
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
            Me.txtClas3.Enabled = True

            Me.Clas2Combo.Enabled = False
            Clas2Combo.SelectedIndex = -1
            Me.txtClas2.Enabled = False
            Me.txtClas2.Text = ""
            Dim cls3 As String = Me.Label4.Text & "-" & Me.Clas3Combo.Text
            Me.txtClas3.Text = cls3
            Me.txtClas1.Text = cls3
            stclass = cls3
        ElseIf Me.Clas3Combo.SelectedIndex = 1 Then

            Me.Clas2DetCombo.Enabled = False
            Me.Clas2DetCombo.SelectedIndex = -1
            Me.Clas1Combo.Enabled = False
            Me.Clas1Combo.SelectedIndex = -1
            Me.txtClas1.Enabled = False
            Me.txtClas3.Enabled = True

            Me.Clas2Combo.Enabled = False
            Clas2Combo.SelectedIndex = -1
            Me.txtClas2.Enabled = False
            Me.txtClas2.Text = ""
            Dim cls3 As String = Me.Label4.Text & "-" & Me.Clas3Combo.Text
            Me.txtClas3.Text = cls3
            Me.txtClas1.Text = cls3
            stclass = cls3
        End If
    End Sub

    Private Sub BiteCombo_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BiteCombo.SelectedIndexChanged
        If Me.BiteCombo.SelectedIndex = 0 Then
            Me.OpenBiteCombo.BringToFront()
            Me.OpenBiteCombo.Visible = True
            RefreshBiteTextFromCombos()
        ElseIf Me.BiteCombo.SelectedIndex = 1 Then
            Me.DeepBiteCombo.BringToFront()
            Me.DeepBiteCombo.Visible = True
            Me.OpenBiteCombo.Visible = False
            Me.OpenBiteDetCombo.Visible = False
            RefreshBiteTextFromCombos()
        ElseIf Me.BiteCombo.SelectedIndex = 2 Then
            Me.ReverseBiteCombo.BringToFront()
            Me.ReverseBiteCombo.Visible = True
            Me.OpenBiteCombo.Visible = False
            Me.OpenBiteDetCombo.Visible = False
            RefreshBiteTextFromCombos()
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
        ElseIf Me.OpenBiteCombo.SelectedIndex = 1 Then
            Me.DeepBiteCombo.Visible = False
            Me.ReverseBiteCombo.Visible = False
            Me.OpenBiteDetCombo.Visible = False
        ElseIf Me.OpenBiteCombo.SelectedIndex = 2 Then
            Me.DeepBiteCombo.Visible = False
            Me.ReverseBiteCombo.Visible = False
            Me.OpenBiteDetCombo.Visible = True
        ElseIf Me.OpenBiteCombo.SelectedIndex = 3 Then
            Me.DeepBiteCombo.Visible = False
            Me.ReverseBiteCombo.Visible = False
            Me.OpenBiteDetCombo.Visible = True
        End If
        RefreshBiteTextFromCombos()
    End Sub

    Private Sub OpenBiteDetCombo_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OpenBiteDetCombo.SelectedIndexChanged
        RefreshBiteTextFromCombos()
    End Sub
    Private Sub DeepBiteCombo_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DeepBiteCombo.SelectedIndexChanged
        RefreshBiteTextFromCombos()
    End Sub
    Private Sub ReverseBiteCombo_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ReverseBiteCombo.SelectedIndexChanged
        RefreshBiteTextFromCombos()
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

    Private Sub FrmEditOrthInfDiag_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Icon = GetIcon()

        IntegerMoneyEditorFocus.ConfigureIntegerMoneyTextEdit(txtPayValue)
        IntegerMoneyEditorFocus.AttachTextEditZeroEmptyElseSelectAll(txtPayValue)
        IntegerMoneyEditorFocus.ConfigureIntegerMoneyTextEdit(txtTrtPrice)
        IntegerMoneyEditorFocus.AttachTextEditZeroEmptyElseSelectAll(txtTrtPrice)
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