Imports System.Data.SqlClient
Imports Dapper
Imports DentistX
Imports DevExpress.CodeParser
Imports DevExpress.XtraGrid.Views.Base

Public Class FrmOtherTRTs

    Private _patientID As Integer

    ''' <summary>Patient context for saves and grid binding (required for Add / balance updates).</summary>
    Public ReadOnly Property PatientID As Integer
        Get
            Return _patientID
        End Get
    End Property

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Public Sub New(patientID As Integer)

        ' This call is required by the designer.
        InitializeComponent()

        _patientID = patientID
        LoadOtherTrtsData(patientID)
    End Sub

    ''' <summary>Prefill combo and free-text when opened from external callers that pass a treatment name.</summary>
    Public Sub ApplyTrtFromChart(trt As String)
        If String.IsNullOrWhiteSpace(trt) Then Return
        Dim t = trt.Trim()
        cboTrtType.SetSelectedTblOtherTrtID(t)
        txtTRT.Text = t
    End Sub

    Private clsOtherTrt As New Patient_OtherTRT
    Private clsOtherTrtData As New Patient_OtherTRTDATA

#Region "EditAddOtherTrt"


    Private Sub GridView1_CustomUnboundColumnData(sender As Object, e As CustomColumnDataEventArgs) Handles GridView1.CustomUnboundColumnData
        If e.Column.Name = "colRowNum" Then
            e.Value = e.ListSourceRowIndex + 1
        End If
    End Sub

    ' Load Surgery Data
    Public Sub LoadOtherTrtsData(ByVal PatientID As Integer)
        Try
            Dim otherTrts = clsOtherTrtData.SelectAll(PatientID).ToList

            ' Bind to SurgeryBindingSource
            If otherTrts IsNot Nothing Then
                OtherTrtBindingSource.DataSource = otherTrts
            Else
                OtherTrtBindingSource.DataSource = Nothing ' Clear if no data found
            End If
        Catch ex As Exception
            MsgBox("Error loading Other Treats: " & ex.Message)
        End Try
    End Sub



    Private Sub btAddOtherTrt_Click(sender As Object, e As EventArgs) Handles btAddOtherTrt.Click
        Try
            If SaveOtherTrtWithTransaction(sender, PatientID) Then
                LoadOtherTrtsData(PatientID)
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Public Function SaveOtherTrtWithTransaction(sender As Object, PatientID As Integer) As Boolean
        Dim saved As Boolean = False
        Dim clsTrtsData As New Patient_TrtsDATA
        Dim Dx = New DentistXDATA
        Dim ConnectionString = Dx.GetConnectionString

        Using conn As New SqlConnection(ConnectionString)
            conn.Open()
            Using trans As SqlTransaction = conn.BeginTransaction("OtherSave")
                Try
                    ' Prepare OrthoInf instance
                    clsOtherTrt = New Patient_OtherTRT With {.IsPaid = False,
                .PatientID = PatientID,
                .Trt = txtTRT.Text,
                .TreatDate = CDate(TrtDate.EditValue),
                .TrtDetails = TrtDetails.Text}

                    ' Insert into database and get OtherID
                    Dim lastOtherTrtID As Integer = clsOtherTrtData.AddTransAndGetID(conn, trans, clsOtherTrt)
                    If lastOtherTrtID <= 0 Then
                        MsgBox("Failed to insert Other Treat record - no ID returned")
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
                    Dim detailText = "Other Treat Details ==>> " & txtTRT.Text
                    Dim clsPatientTrts As New Patient_Trts With {
                    .OtherTrtID = lastOtherTrtID,  ' This is now set correctly
                    .OrthoID = 0,  ' This is now set correctly
                    .ToothTrtID = 0,
                    .Detail = detailText,
                    .PatientID = PatientID,
                    .TrtDate = CDate(TrtDate.EditValue),
                    .TrtValue = trtValue,
                    .DiscountType = 1,
                    .Discount = 0,
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
                        .PayDate = CDate(TrtDate.EditValue),
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
                    saved = True
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

    Private originalTrt As Patient_OtherTRT

    Private Sub GridViewBuys_FocusedRowChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles GridView1.FocusedRowChanged
        If TypeOf OtherTrtBindingSource.Current Is Patient_OtherTRT Then
            originalTrt = DirectCast(OtherTrtBindingSource.Current, Patient_OtherTRT).Clone()
        Else
            originalTrt = Nothing
        End If
    End Sub
    Private Sub btEditOtherTrt_Click(sender As Object, e As EventArgs) Handles btEditOtherTrt.Click
        If originalTrt Is Nothing Then Exit Sub
        Try
            Select Case btEditOtherTrt.Text
                Case "Edit"
                    btEditOtherTrt.Text = "Save"
                    GridView1.OptionsBehavior.Editable = True
                Case "Save"
                    OtherTrtBindingSource.EndEdit()

                    Dim newTrt = CType(OtherTrtBindingSource.Current, Patient_OtherTRT)
                    If clsOtherTrtData.Update(originalTrt, newTrt) Then
                        btEditOtherTrt.Text = "Edit"
                        GridView1.OptionsBehavior.Editable = False
                        MsgBox("Changes Saved")
                    End If

            End Select

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub



    Private Sub cboTrtType_TblOtherTRTValueChanged(sender As Object, e As TblOtherTRTCombo.TblOtherTRTIndexChangedEvent) Handles cboTrtType.TblOtherTRTValueChanged
        txtTRT.Text = e.Trt
    End Sub

    Private Sub btnDelTrt_Click(sender As Object, e As EventArgs) Handles btnDelTrt.Click
        If originalTrt Is Nothing Then Exit Sub
        Try
            OtherTrtBindingSource.EndEdit()
            Dim newTrt = CType(OtherTrtBindingSource.Current, Patient_OtherTRT)
            If MsgBox("Are you sure you want to delete this Other Treat?", MsgBoxStyle.YesNo + MsgBoxStyle.Question) = MsgBoxResult.Yes Then
                If clsOtherTrtData.Delete(newTrt) Then
                    MsgBox("Other Treat Deleted")
                    LoadOtherTrtsData(PatientID)
                    FormManager.Instance.CurrentForm.UpdatePatientBalance(CurrentPatient)
                End If
            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub









#End Region


End Class