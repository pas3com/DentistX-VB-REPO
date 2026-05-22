Imports System.ComponentModel
Imports DevExpress.XtraBars
Imports System.ComponentModel.DataAnnotations
Imports DevExpress.XtraGrid.Views.Base
Imports System.Windows.Forms

Partial Public Class FrmVendors


    Public Sub New()
        InitializeComponent()
        bsiRecordsCount.Caption = "RECORDS : " & dgView.RowCount
    End Sub

    Private Sub dgView_CustomUnboundColumnData(sender As Object, e As CustomColumnDataEventArgs) Handles dgView.CustomUnboundColumnData
        If e.Column.Name = "colRowNum" Then
            e.Value = e.ListSourceRowIndex + 1
        End If
    End Sub

    Private Sub bbiPrintPreview_ItemClick(ByVal sender As Object, ByVal e As ItemClickEventArgs) Handles bbiPrintPreview.ItemClick
        DGV.ShowRibbonPrintPreview()
    End Sub
    Private clsVendorsData As New VendorsDATA
    Private clsDentistXData As New DentistXDATA


    Private Sub FrmVendors_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'LoadDGV()
        Me.Cursor = System.Windows.Forms.Cursors.WaitCursor
        'LoadVendors_TblCitiesComboBox()
        LoadDGV()
        ShowToolStripItems("Cancel")
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub


    Private bAddMode As Boolean = False
    Private bEditMode As Boolean = False
    Private bDeleteMode As Boolean = False
    Private iRow As Int32 = 0

    Public Property AddMode() As Boolean
        Get
            Return bAddMode
        End Get
        Set(ByVal value As Boolean)
            bAddMode = value
        End Set
    End Property

    Public Property EditMode() As Boolean
        Get
            Return bEditMode
        End Get
        Set(ByVal value As Boolean)
            bEditMode = value
        End Set
    End Property

    Public Property DeleteMode() As Boolean
        Get
            Return bDeleteMode
        End Get
        Set(ByVal value As Boolean)
            bDeleteMode = value
        End Set
    End Property

    Public Property Row() As Int32
        Get
            Return iRow
        End Get
        Set(ByVal value As Int32)
            iRow = value
        End Set
    End Property

    Private Sub LoadDGV()
        Me.Cursor = System.Windows.Forms.Cursors.WaitCursor
        Try
            With DGV
                .DataSource = clsVendorsData.SelectAll
                If dgView.RowCount = 0 Then
                    ShowToolStripItems("No Record")
                Else
                    ClearRecord()
                End If
            End With
        Catch
        End Try
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub DGV_Click(sender As Object, e As System.EventArgs) Handles DGV.Click

        Try
            Row = dgView.FocusedRowHandle
            Display()
        Catch
        End Try
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub DGV_SelectionChanged(sender As Object, e As System.EventArgs) Handles dgView.SelectionChanged

        Try
            Row = dgView.FocusedRowHandle
            Display()
        Catch
        End Try
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub Display()
        Me.Cursor = System.Windows.Forms.Cursors.WaitCursor
        ClearRecord()

        Dim clsVendors As New Vendors
        clsVendors.VendID = System.Convert.ToInt32(dgView.GetRowCellDisplayText(Row, colVendID))
        clsVendors = clsVendorsData.Select_Record(clsVendors)

        If Not clsVendors Is Nothing Then
            Try
                VendIDSpinEdit.Value = System.Convert.ToInt32(clsVendors.VendID)
                VendNameTextEdit.Text = If(clsVendors.VendName Is Nothing, Nothing, Convert.ToString(clsVendors.VendName))
                CityCombo1.CityID = If(clsVendors.CityID Is Nothing, Nothing, System.Convert.ToInt32(clsVendors.CityID))
                VendAddressTextEdit.Text = If(clsVendors.VendAddress Is Nothing, Nothing, Convert.ToString(clsVendors.VendAddress))
                ContactsTextEdit.Text = If(clsVendors.Contacts Is Nothing, Nothing, Convert.ToString(clsVendors.Contacts))
            Catch
            End Try
        End If
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub Add()
        Me.Cursor = System.Windows.Forms.Cursors.WaitCursor
        Me.AddMode = True
        Me.EditMode = False
        Me.DeleteMode = False

        ClearRecord()
        ShowToolStripItems("Add")

        VendNameTextEdit.Select()
        VendIDSpinEdit.Value = DentistXDATA.getAutoID("New", "Vendors")

        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub Edit()
        Me.Cursor = System.Windows.Forms.Cursors.WaitCursor
        Me.AddMode = False
        Me.EditMode = True
        Me.DeleteMode = False

        ShowToolStripItems("Edit")

        VendNameTextEdit.Select()
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub Delete()
        Me.Cursor = System.Windows.Forms.Cursors.WaitCursor
        Me.AddMode = False
        Me.EditMode = False
        Me.DeleteMode = True

        ShowToolStripItems("Delete")
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub EnableRecord(ByVal YesNo As Boolean)
        Me.Cursor = System.Windows.Forms.Cursors.WaitCursor
        Me.VendNameTextEdit.Enabled = YesNo
        Me.CityCombo1.Enabled = YesNo
        Me.VendAddressTextEdit.Enabled = YesNo
        Me.ContactsTextEdit.Enabled = YesNo
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub ClearRecord()
        Me.Cursor = System.Windows.Forms.Cursors.WaitCursor
        Me.VendIDSpinEdit.Value = 0
        Me.VendNameTextEdit.Text = Nothing
        Me.CityCombo1.CboCity.SelectedIndex = -1
        Me.VendAddressTextEdit.Text = Nothing
        Me.ContactsTextEdit.Text = Nothing
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub GoBack_To_Grid()
        Me.Cursor = System.Windows.Forms.Cursors.WaitCursor
        RemoveHandler dgView.SelectionChanged, AddressOf DGV_SelectionChanged
        Dim gridOK As Boolean = False
        Try
            LoadDGV()
            ShowToolStripItems("Cancel")
            dgView.SelectRow(Row)

            dgView.FocusedRowHandle = Row
            dgView.Focus()
            gridOK = True
        Catch
            'Hide error message.
        Finally
            If gridOK = False Then
                ''''
                ShowToolStripItems("Cancel")
                ''''
            End If
        End Try
        AddHandler dgView.SelectionChanged, AddressOf DGV_SelectionChanged
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub butCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butCancel.Click
        Me.Cursor = System.Windows.Forms.Cursors.WaitCursor
        ShowToolStripItems("Cancel")
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub butClose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butClose.Click
        Me.Cursor = System.Windows.Forms.Cursors.WaitCursor
        Me.Close()
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub butApply_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butApply.Click
        Me.Cursor = System.Windows.Forms.Cursors.WaitCursor
        If Me.AddMode = True Then
            Me.InsertRecord()
        ElseIf Me.EditMode = True Then
            Me.UpdateRecord()
        ElseIf Me.DeleteMode = True Then
            Me.DeleteRecord()
        End If
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub SetData(ByVal clsVendors As Vendors)
        With clsVendors
            .VendName = If(String.IsNullOrEmpty(VendNameTextEdit.Text), Nothing, VendNameTextEdit.Text)
            .CityID = If(String.IsNullOrEmpty(CityCombo1.CityID.ToString), Nothing, CType(CityCombo1.CityID, Int32?))
            .VendAddress = If(String.IsNullOrEmpty(VendAddressTextEdit.Text), Nothing, VendAddressTextEdit.Text)
            .Contacts = If(String.IsNullOrEmpty(ContactsTextEdit.Text), Nothing, ContactsTextEdit.Text)
        End With
    End Sub

    Private Sub InsertRecord()
        Me.Cursor = System.Windows.Forms.Cursors.WaitCursor
        Dim clsVendors As New Vendors
        If VerifyData() = True Then
            SetData(clsVendors)
            Dim bSucess As Boolean
            bSucess = clsVendorsData.Add(clsVendors)
            If bSucess = True Then
                GoBack_To_Grid()
            Else
                MsgBox("Insert failed.", MsgBoxStyle.OkOnly, "Error")
            End If
        End If
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub UpdateRecord()
        Me.Cursor = System.Windows.Forms.Cursors.WaitCursor
        Dim oclsVendors As New Vendors
        Dim clsVendors As New Vendors

        oclsVendors.VendID = System.Convert.ToInt32(dgView.GetRowCellDisplayText(Row, colVendID))
        oclsVendors = clsVendorsData.Select_Record(oclsVendors)

        If VerifyData() = True Then
            SetData(clsVendors)
            Dim bSucess As Boolean
            bSucess = clsVendorsData.Update(oclsVendors, clsVendors)
            If bSucess = True Then
                GoBack_To_Grid()
            Else
                MsgBox("Update failed.", MsgBoxStyle.OkOnly, "Error")
            End If
        End If
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub DeleteRecord()
        Me.Cursor = System.Windows.Forms.Cursors.WaitCursor
        Dim clsVendors As New Vendors
        clsVendors.VendID = System.Convert.ToInt32(dgView.GetRowCellDisplayText(Row, colVendID))
        Dim result As DialogResult = MessageBox.Show("Are you sure? Delete this record?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If result = Windows.Forms.DialogResult.Yes Then
            SetData(clsVendors)
            Dim bSucess As Boolean
            bSucess = clsVendorsData.Delete(clsVendors)
            If bSucess = True Then
                GoBack_To_Grid()
            Else
                MsgBox("Delete failed.", MsgBoxStyle.OkOnly, "Error")
            End If
        End If
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub

    Private Function VerifyData() As Boolean
        Return True
    End Function

    Private Sub ShowToolStripItems(ByVal Item As String)
        Me.Cursor = System.Windows.Forms.Cursors.WaitCursor
        bAdd.Enabled = False
        bEdit.Enabled = False
        bDelete.Enabled = False
        bCancel.Enabled = False
        bRefresh.Enabled = False
        butApply.Enabled = True
        butCancel.Enabled = True
        Select Case Item
            Case "Add"
                EnableRecord(True)
                bCancel.Enabled = True
            Case "Edit"
                EnableRecord(True)
                bCancel.Enabled = True
            Case "Delete"
                EnableRecord(False)
                bCancel.Enabled = True
            Case "Cancel"
                If dgView.RowCount = 0 Then
                    ShowToolStripItems("No Record")
                Else
                    EnableRecord(False)
                    bAdd.Enabled = True
                    bEdit.Enabled = True
                    bDelete.Enabled = True
                    bRefresh.Enabled = True
                    butApply.Enabled = False
                    butCancel.Enabled = False
                    dgView.Focus()
                End If
            Case "Refresh"
                bAdd.Enabled = True
                bEdit.Enabled = True
                bDelete.Enabled = True
                bRefresh.Enabled = True
                butApply.Enabled = False
                butCancel.Enabled = False
                LoadDGV()
            Case "No Record"
                bAdd.Enabled = True
                bRefresh.Enabled = False
                butApply.Enabled = False
                butCancel.Enabled = False
        End Select
        Me.Text = "[Dbo. Vendors] - " & Item.Trim
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub bAdd_ItemClick(sender As Object, e As ItemClickEventArgs) Handles bAdd.ItemClick
        Add()
    End Sub

    Private Sub bEdit_ItemClick(sender As Object, e As ItemClickEventArgs) Handles bEdit.ItemClick
        Display()
        Edit()
    End Sub

    Private Sub bDelete_ItemClick(sender As Object, e As ItemClickEventArgs) Handles bDelete.ItemClick
        Display()
        Delete()
    End Sub

    Private Sub bRefresh_ItemClick(sender As Object, e As ItemClickEventArgs) Handles bRefresh.ItemClick
        LoadDGV()
    End Sub

    Private Sub bCancel_ItemClick(sender As Object, e As ItemClickEventArgs) Handles bCancel.ItemClick
        ShowToolStripItems("Cancel")
    End Sub




End Class


