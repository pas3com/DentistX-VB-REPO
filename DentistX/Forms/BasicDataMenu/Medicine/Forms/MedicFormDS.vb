Imports System.Diagnostics
Imports DevExpress.XtraGrid.Views.Base

Public Class MedicFormDS



#Region "Number"


    Private Sub GridView1_CustomUnboundColumnData(sender As Object, e As CustomColumnDataEventArgs) Handles GridView1.CustomUnboundColumnData
        If e.Column.Name = "colRowNum1" Then
            e.Value = e.ListSourceRowIndex + 1
        End If
    End Sub
    Private Sub GridView2_CustomUnboundColumnData(sender As Object, e As CustomColumnDataEventArgs) Handles GridView2.CustomUnboundColumnData
        If e.Column.Name = "colRowNum2" Then
            e.Value = e.ListSourceRowIndex + 1
        End If
    End Sub
    Private Sub GridView3_CustomUnboundColumnData(sender As Object, e As CustomColumnDataEventArgs) Handles GridView3.CustomUnboundColumnData
        If e.Column.Name = "colRowNum3" Then
            e.Value = e.ListSourceRowIndex + 1
        End If
    End Sub
    Private Sub GridView4_CustomUnboundColumnData(sender As Object, e As CustomColumnDataEventArgs) Handles GridView4.CustomUnboundColumnData
        If e.Column.Name = "colRowNum4" Then
            e.Value = e.ListSourceRowIndex + 1
        End If
    End Sub
    Private Sub GridView5_CustomUnboundColumnData(sender As Object, e As CustomColumnDataEventArgs) Handles GridView5.CustomUnboundColumnData
        If e.Column.Name = "colRowNum5" Then
            e.Value = e.ListSourceRowIndex + 1
        End If
    End Sub
    Private Sub GridView6_CustomUnboundColumnData(sender As Object, e As CustomColumnDataEventArgs) Handles GridView6.CustomUnboundColumnData
        If e.Column.Name = "colRowNum6" Then
            e.Value = e.ListSourceRowIndex + 1
        End If
    End Sub
#End Region



    Private Sub MedicineCatBindingNavigatorSaveItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.Validate()
        Me.MedicineGroupsBindingSource.EndEdit()
        Me.TableAdapterManager.UpdateAll(Me.MedDs)

    End Sub



    Private Sub medicineFormNew_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.MedicineGroupsTableAdapter.Fill(Me.MedDs.MedicineGroups)
            Me.MedicineFamilyTableAdapter.Fill(Me.MedDs.MedicineFamily)
            Me.MedScienceFamilyTableAdapter.Fill(Me.MedDs.MedScienceFamily)
            Me.MedicineItemsTableAdapter.Fill(Me.MedDs.MedicineItems)
            Me.MedicineShapeTableAdapter.Fill(Me.MedDs.MedicineShape)
            Me.MedicineDozeTableAdapter.Fill(Me.MedDs.MedicineDoze)
        Catch ex As System.Data.SqlClient.SqlException
            MsgBox(ex.Message)
        End Try
    End Sub

#Region "Add"
    Private Sub btAddGrp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btAddGrp.Click
        If Me.MedicenGroup.Text = "" Then
            If Eng Then
                MsgBox("You Must Enter Group Name")
                Exit Sub
            Else
                MsgBox("يجب ادخال اسم المجموعة")
                Exit Sub
            End If

        Else
            Try
                ' Me.MedicineCatTableAdapter.Insert(Trim(Me.medicenCat.Text))
                Me.Qrs.MedicineGroupsInsert(Trim(Me.MedicenGroup.Text))
                Me.MedicineGroupsTableAdapter.Fill(Me.MedDs.MedicineGroups)
            Catch ex As System.Data.SqlClient.SqlException
                MsgBox(ex.Message)
            End Try
        End If
    End Sub

    Private Sub btAddFamily_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btAddFamily.Click
        If Me.MedicineFamily.Text = "" Or Me.GroupCombo.Text = "" Then
            If Eng Then
                MsgBox("You Must Enter Family Name")
                Exit Sub
            Else
                MsgBox("يجب ادخال اسم العائلة")
                Exit Sub
            End If
        Else
            Try
                Dim GrpID As Integer = Convert.ToInt32(Me.GroupCombo.SelectedValue)
                ' Me.MedicineCatTableAdapter.Insert(Trim(Me.medicenCat.Text))
                Me.Qrs.MedicineFamilyInsert(GrpID, Trim(Me.MedicineFamily.Text))
                Me.MedicineFamilyTableAdapter.Fill(Me.MedDs.MedicineFamily)
            Catch ex As System.Data.SqlClient.SqlException
                MsgBox(ex.Message)
            End Try
        End If
    End Sub

    Private Sub btAddScience_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btAddScience.Click
        If Me.FamilyCombo.Text = "" Or Me.Scintific.Text = "" Then
            If Eng Then
                MsgBox("You Must Enter Science Name")
                Exit Sub
            Else
                MsgBox("يجب ادخال الاسم العلمي")
                Exit Sub
            End If
        Else
            Try
                Dim FmlyID As Integer = Convert.ToInt32(Me.FamilyCombo.SelectedValue)
                'Me.MedicineItemsTableAdapter.Insert(medic, Trim(medicineType.Text), Trim(Me.Scintific.Text), _
                '                                    Trim(Me.Commercial.Text), Trim(Me.MedicNote.Text))
                Me.Qrs.MedScienceFamilyInsert(FmlyID, Trim(Me.Scintific.Text))
                Me.MedScienceFamilyTableAdapter.Fill(Me.MedDs.MedScienceFamily)
            Catch ex As System.Data.SqlClient.SqlException
                MsgBox(ex.Message)
            End Try
        End If

    End Sub

    Private Sub btAddMedicine_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btAddMedicine.Click
        If Me.ScientCombo.Text = "" Or Me.Commercial.Text = "" Or Me.CompanyTxt.Text = "" Then
            If Eng Then
                MsgBox("You Must Enter Medicine A Name")
                Exit Sub
            Else
                MsgBox("يجب ادخال اسم الدواء")
                Exit Sub
            End If
        Else
            Try
                Dim ScinID As Integer = Convert.ToInt32(Me.ScientCombo.SelectedValue)
                'Me.MedicineItemsTableAdapter.Insert(medic, Trim(medicineType.Text), Trim(Me.Scintific.Text), _
                '                                    Trim(Me.Commercial.Text), Trim(Me.MedicNote.Text))
                Me.Qrs.MedicineItemsInsert(ScinID, Trim(Me.Commercial.Text), Trim(Me.CompanyTxt.Text), Trim(Me.MedicNote.Text))
                Me.MedicineItemsTableAdapter.Fill(Me.MedDs.MedicineItems)
            Catch ex As System.Data.SqlClient.SqlException
                MsgBox(ex.Message)
            End Try
        End If
    End Sub

    Private Sub btAddShape_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btAddShape.Click
        If Me.Shape.Text = "" Or Me.itemCombo.Text = "" Then

            If Eng Then
                MsgBox("You Must Enter A Shape")
                Exit Sub
            Else
                MsgBox("يجب ادخال شكل الدواء")
                Exit Sub
            End If
        Else
            Try
                Dim meditem As Integer = Convert.ToInt32(Trim(Me.itemCombo.SelectedValue))
                'Me.MedicineShapeTableAdapter.Insert(meditem, Trim(Me.Shape.Text))
                Me.Qrs.MedicineShapeInsert(meditem, Trim(Me.Shape.Text), Trim(Me.ShapeInfotxt.Text))
                Me.MedicineShapeTableAdapter.Fill(Me.MedDs.MedicineShape)
            Catch ex As System.Data.SqlClient.SqlException
                MsgBox(ex.Message)
            End Try

        End If
    End Sub
    Private Sub btAddDoze_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btAddDoze.Click
        If Me.Doze.Text = "" Or Me.shapeCombo.Text = "" Then

            If Eng Then
                MsgBox("You Must Enter A Doze")
                Exit Sub
            Else
                MsgBox("يجب ادخال الجرعة")
                Exit Sub
            End If
        Else
            Try
                Dim medshape As Integer = Convert.ToInt32(Trim(Me.shapeCombo.SelectedValue))
                'Me.MedicineDozeTableAdapter.Insert(medshape, Trim(Me.Doze.Text))
                Me.Qrs.MedicineDozeInsert(medshape, Trim(Me.Doze.Text))
                Me.MedicineDozeTableAdapter.Fill(Me.MedDs.MedicineDoze)
            Catch ex As System.Data.SqlClient.SqlException
                MsgBox(ex.Message)
            End Try

        End If
    End Sub
#End Region

#Region "Edit"
    Private Sub btEditGrp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btEditGrp.Click
        Static x As Integer = 2
        Select Case x Mod 2
            Case 0
                If Eng Then
                    btEditGrp.Text = "End Edit"
                Else
                    btEditGrp.Text = "انهاء"
                End If
                '"انهاء"
                Me.btSaveGrp.Visible = True
                Me.GrpNavigator.Visible = True
                Me.GridView1.OptionsBehavior.ReadOnly = True
                Me.GridView1.OptionsBehavior.Editable = True
            Case Else
                If Eng Then
                    btEditGrp.Text = "Edit Group"
                Else
                    btEditGrp.Text = "تعديل"
                End If
                '"جديد"
                Me.btSaveGrp.Visible = False
                Me.GrpNavigator.Visible = False
                Me.GridView1.OptionsBehavior.ReadOnly = False
                Me.GridView1.OptionsBehavior.Editable = False
        End Select
        x += 1
    End Sub
    Private Sub btEditFamly_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btEditFamly.Click
        Static x As Integer = 2
        Select Case x Mod 2
            Case 0
                If Eng Then
                    btEditFamly.Text = "End Edit"
                Else
                    btEditFamly.Text = "انهاء"
                End If
                '"انهاء"
                Me.btSaveFamly.Visible = True
                Me.FamlyNavigator.Visible = True
                Me.GridView2.OptionsBehavior.ReadOnly = True
                Me.GridView2.OptionsBehavior.Editable = True
            Case Else
                If Eng Then
                    btEditFamly.Text = "Edit Family"
                Else
                    btEditFamly.Text = "تعديل"
                End If
                '"جديد"
                Me.btSaveFamly.Visible = False
                Me.FamlyNavigator.Visible = False
                Me.GridView2.OptionsBehavior.ReadOnly = False
                Me.GridView2.OptionsBehavior.Editable = False
        End Select
        x += 1
    End Sub
    Private Sub btEditScinc_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btEditScinc.Click
        Static x As Integer = 2
        Select Case x Mod 2
            Case 0
                If Eng Then
                    btEditScinc.Text = "End Edit"
                Else
                    btEditScinc.Text = "انهاء"
                End If
                '"انهاء"
                Me.btSaveScinc.Visible = True
                Me.SciencNavigator.Visible = True
                Me.GridView3.OptionsBehavior.ReadOnly = True
                Me.GridView3.OptionsBehavior.Editable = True
            Case Else
                If Eng Then
                    btEditScinc.Text = "Edit Science"
                Else
                    btEditScinc.Text = "تعديل"
                End If
                '"جديد"
                Me.btSaveScinc.Visible = False
                Me.SciencNavigator.Visible = False
                Me.GridView3.OptionsBehavior.ReadOnly = False
                Me.GridView3.OptionsBehavior.Editable = False
        End Select
        x += 1
    End Sub
    Private Sub btEditMedic_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btEditMedic.Click
        Static x As Integer = 2
        Select Case x Mod 2
            Case 0
                If Eng Then
                    btEditMedic.Text = "End Edit"
                Else
                    btEditMedic.Text = "انهاء"
                End If
                '"انهاء"
                Me.btSaveMedic.Visible = True
                Me.ItemsNavigator.Visible = True
                Me.GridView4.OptionsBehavior.ReadOnly = True
                Me.GridView4.OptionsBehavior.Editable = True
            Case Else
                If Eng Then
                    btEditMedic.Text = "Edit Items"
                Else
                    btEditMedic.Text = "تعديل"
                End If
                '"جديد"
                Me.btSaveMedic.Visible = False
                Me.ItemsNavigator.Visible = False
                Me.GridView4.OptionsBehavior.ReadOnly = False
                Me.GridView4.OptionsBehavior.Editable = False
        End Select
        x += 1
    End Sub
    Private Sub btEditShape_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btEditShape.Click
        Static x As Integer = 2
        Select Case x Mod 2
            Case 0
                If Eng Then
                    btEditShape.Text = "End Edit" '"انهاء"
                Else
                    btEditShape.Text = "انهاء" '"انهاء"
                End If

                Me.btSaveShape.Visible = True
                Me.ShapeNavigator.Visible = True
                Me.GridView5.OptionsBehavior.ReadOnly = True
                Me.GridView5.OptionsBehavior.Editable = True
            Case Else
                If Eng Then
                    btEditShape.Text = "Edit Shape" '"جديد"
                Else
                    btEditShape.Text = "تعديل"
                End If

                Me.btSaveShape.Visible = False
                Me.ShapeNavigator.Visible = False
                Me.GridView5.OptionsBehavior.ReadOnly = False
                Me.GridView5.OptionsBehavior.Editable = False
        End Select
        x += 1
    End Sub
    Private Sub btEditDoze_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btEditDoze.Click
        Static x As Integer = 2
        Select Case x Mod 2
            Case 0
                If Eng Then
                    btEditDoze.Text = "End Edit" '"انهاء"
                Else
                    btEditDoze.Text = "انهاء" '"انهاء"
                End If

                Me.btSaveDoze.Visible = True
                Me.DozeNavigator.Visible = True
                Me.GridView6.OptionsBehavior.ReadOnly = True
                Me.GridView6.OptionsBehavior.Editable = True
            Case Else
                If Eng Then
                    btEditDoze.Text = "Edit Doze" '"جديد"
                Else
                    btEditDoze.Text = "تعديل" '"جديد
                End If

                Me.btSaveDoze.Visible = False
                Me.DozeNavigator.Visible = False
                Me.GridView6.OptionsBehavior.ReadOnly = False
                Me.GridView6.OptionsBehavior.Editable = False
        End Select
        x += 1
    End Sub
#End Region

#Region "Save"
    Private Sub btSaveGrp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btSaveGrp.Click
        Try
            Me.Validate()
            Me.MedicineGroupsBindingSource.EndEdit()
            Me.MedicineGroupsTableAdapter.Update(Me.MedDs.MedicineGroups)
        Catch ex As System.Data.SqlClient.SqlException
            MsgBox(ex.Message)
        End Try

    End Sub
    Private Sub btSaveFamly_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btSaveFamly.Click
        Try
            Me.Validate()
            Me.MedicineFamilyBindingSource.EndEdit()
            Me.MedicineFamilyTableAdapter.Update(Me.MedDs.MedicineFamily)
        Catch ex As System.Data.SqlClient.SqlException
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub btSaveScinc_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btSaveScinc.Click
        Try
            Me.Validate()
            Me.ScienceFamilyBindingSource.EndEdit()
            Me.MedScienceFamilyTableAdapter.Update(Me.MedDs.MedScienceFamily)
        Catch ex As System.Data.SqlClient.SqlException
            MsgBox(ex.Message)
        End Try

    End Sub
    Private Sub btSaveMedic_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btSaveMedic.Click
        Try
            Me.Validate()
            Me.MedicineItemsBindingSource.EndEdit()
            Me.MedicineItemsTableAdapter.Update(Me.MedDs.MedicineItems)
        Catch ex As System.Data.SqlClient.SqlException
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub btSaveShape_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btSaveShape.Click
        Try
            Me.Validate()
            Me.MedicineShapeBindingSource.EndEdit()
            Me.MedicineShapeTableAdapter.Update(Me.MedDs.MedicineShape)
        Catch ex As System.Data.SqlClient.SqlException
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub btSaveDoze_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btSaveDoze.Click
        Try
            Me.Validate()
            Me.MedicineDozeBindingSource.EndEdit()
            Me.MedicineDozeTableAdapter.Update(Me.MedDs.MedicineDoze)
        Catch ex As System.Data.SqlClient.SqlException
            MsgBox(ex.Message)
        End Try
    End Sub
#End Region

#Region "Nav Save"
    Private Sub PatientBindingNavigatorSaveItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PatientBindingNavigatorSaveItem.Click
        Me.Validate()
        Me.MedicineGroupsBindingSource.EndEdit()
        Me.MedicineGroupsTableAdapter.Update(Me.MedDs.MedicineGroups)
    End Sub
    Private Sub ToolStripButton6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton6.Click
        Try
            Me.Validate()
            Me.ScienceFamilyBindingSource.EndEdit()
            Me.MedScienceFamilyTableAdapter.Update(Me.MedDs.MedScienceFamily)
        Catch ex As System.Data.SqlClient.SqlException
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub ToolStripButton12_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton12.Click
        Me.Validate()
        Me.MedicineShapeBindingSource.EndEdit()
        Me.MedicineShapeTableAdapter.Update(Me.MedDs.MedicineShape)
    End Sub
    Private Sub ToolStripButton18_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton18.Click
        Me.Validate()
        Me.MedicineDozeBindingSource.EndEdit()
        Me.MedicineDozeTableAdapter.Update(Me.MedDs.MedicineDoze)
    End Sub
    Private Sub ToolStripButton30_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton30.Click
        Try
            Me.Validate()
            Me.MedicineItemsBindingSource.EndEdit()
            Me.MedicineItemsTableAdapter.Update(Me.MedDs.MedicineItems)
        Catch ex As System.Data.SqlClient.SqlException
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub ToolStripButton24_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton24.Click
        Try
            Me.Validate()
            Me.MedicineFamilyBindingSource.EndEdit()
            Me.MedicineFamilyTableAdapter.Update(Me.MedDs.MedicineFamily)
        Catch ex As System.Data.SqlClient.SqlException
            MsgBox(ex.Message)
        End Try
    End Sub
#End Region

End Class
