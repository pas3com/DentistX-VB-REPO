Imports System.ComponentModel
Imports System.Drawing
Imports System.Linq
Imports System.Threading.Tasks
Imports System.Data.SqlClient
Imports System.Windows.Forms
Imports DevExpress.XtraEditors
Imports DevExpress.XtraEditors.Controls
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Views.Grid

Public Class StockSuppliersForm

    Private ReadOnly _repo As SupplierRepository
    Private _binding As BindingList(Of Supplier)
    Private ReadOnly _defaultFont As Font = New Font("Calibri", 10, FontStyle.Bold)

    Public Sub New()
        InitializeComponent()

        _repo = New SupplierRepository(DentistXDATA.GetConnection.ConnectionString)

        Text = If(Eng, "Suppliers", "الموردين")

        StartPosition = FormStartPosition.CenterScreen
        Font = _defaultFont
        If Not Eng Then
            RightToLeft = RightToLeft.Yes
            RightToLeftLayout = True
        End If

        AddHandler Load, AddressOf OnFormLoad
        AddHandler _btnRefresh.Click, Async Sub() Await LoadDataAsync()
        AddHandler _btnAdd.Click, AddressOf OnAdd
        AddHandler _btnEdit.Click, AddressOf OnEdit
        AddHandler _btnDelete.Click, AddressOf OnDelete
    End Sub

    Private Async Sub OnFormLoad(sender As Object, e As EventArgs)
        Await LoadDataAsync()
    End Sub

    Private Async Function LoadDataAsync() As Task
        Dim data = _repo.GetAll().ToList()
        _binding = New BindingList(Of Supplier)(data)
        _grid.DataSource = _binding
        ApplyGridCaptions()
        _view.BestFitColumns()
        Await Task.CompletedTask
    End Function

    Private Async Sub OnAdd(sender As Object, e As EventArgs)
        Dim s As New Supplier()
        Using dlg As New SupplierEditForm(s)
            If dlg.ShowDialog(Me) = DialogResult.OK Then
                Await _repo.InsertAsync(s)
                Await LoadDataAsync()
            End If
        End Using
    End Sub

    Private Async Sub OnEdit(sender As Object, e As EventArgs)
        Dim s = TryCast(_view.GetFocusedRow(), Supplier)
        If s Is Nothing Then Return
        Dim edit = New Supplier With {
            .SupplierID = s.SupplierID,
            .SupplierName = s.SupplierName,
            .ContactPerson = s.ContactPerson,
            .PhoneNumber = s.PhoneNumber,
            .EmailAddress = s.EmailAddress,
            .PhysicalAddress = s.PhysicalAddress,
            .PaymentTerms = s.PaymentTerms,
            .WhatsAppPrefix = s.WhatsAppPrefix,
            .WhatsApp = s.WhatsApp
        }
        Using dlg As New SupplierEditForm(edit)
            If dlg.ShowDialog(Me) = DialogResult.OK Then
                Await _repo.UpdateAsync(edit)
                Await LoadDataAsync()
            End If
        End Using
    End Sub

    Private Async Sub OnDelete(sender As Object, e As EventArgs)
        Dim s = TryCast(_view.GetFocusedRow(), Supplier)
        If s Is Nothing Then Return
        If XtraMessageBox.Show(Me,
                               If(Eng, $"Delete supplier '{s.SupplierName}'?", $"حذف المورد '{s.SupplierName}'؟"),
                               If(Eng, "Confirm", "تأكيد"),
                               MessageBoxButtons.YesNo) = DialogResult.Yes Then
            Try
                Await _repo.DeleteAsync(s.SupplierID)
                Await LoadDataAsync()
            Catch ex As SqlException
                If ex.Number = 547 Then
                    XtraMessageBox.Show(Me,
                                        If(Eng,
                                           "You cannot delete this supplier because it is used in other records (invoices, expenses or payments).",
                                           "لا يمكن حذف هذا المورد لأنه مستخدم في سجلات أخرى (فواتير، مصروفات أو مدفوعات)."),
                                        If(Eng, "Delete not allowed", "لا يمكن الحذف"),
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Warning)
                Else
                    XtraMessageBox.Show(Me,
                                        If(Eng, $"Delete failed: {ex.Message}", $"فشل الحذف: {ex.Message}"),
                                        If(Eng, "Error", "خطأ"),
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Error)
                End If
            End Try
        End If
    End Sub

    Private Sub ApplyGridCaptions()
        Dim col = _view.Columns("SupplierID")
        If col IsNot Nothing Then col.Caption = If(Eng, "Supplier ID", "رقم المورد")
        col = _view.Columns("SupplierName")
        If col IsNot Nothing Then col.Caption = If(Eng, "Name", "الاسم")
        col = _view.Columns("ContactPerson")
        If col IsNot Nothing Then col.Caption = If(Eng, "Contact", "جهة الاتصال")
        col = _view.Columns("PhoneNumber")
        If col IsNot Nothing Then col.Caption = If(Eng, "Phone", "الهاتف")
        col = _view.Columns("EmailAddress")
        If col IsNot Nothing Then col.Caption = If(Eng, "Email", "البريد الإلكتروني")
        col = _view.Columns("PhysicalAddress")
        If col IsNot Nothing Then col.Caption = If(Eng, "Address", "العنوان")
        col = _view.Columns("PaymentTerms")
        If col IsNot Nothing Then col.Caption = If(Eng, "Payment Terms", "شروط الدفع")
        col = _view.Columns("WhatsAppPrefix")
        If col IsNot Nothing Then col.Caption = If(Eng, "WhatsApp prefix", "بادئة واتساب")
        col = _view.Columns("WhatsApp")
        If col IsNot Nothing Then col.Caption = If(Eng, "WhatsApp", "واتساب")
    End Sub
End Class

Friend Class SupplierEditForm
        Inherits XtraForm

        Private ReadOnly _supplier As Supplier
        Private ReadOnly _txtName As TextEdit
        Private ReadOnly _txtContact As TextEdit
        Private ReadOnly _txtPhone As TextEdit
        Private ReadOnly _txtEmail As TextEdit
        Private ReadOnly _txtAddress As MemoEdit
        Private ReadOnly _txtTerms As MemoEdit
        Private ReadOnly _cboWhatsPrefix As ComboBoxEdit
        Private ReadOnly _txtWhats As TextEdit
        Private ReadOnly _lblFullWhats As LabelControl
        Private ReadOnly _defaultFont As Font = New Font("Calibri", 10, FontStyle.Bold)

        Public Sub New(supplier As Supplier)
            _supplier = supplier
            Text = If(Eng, "Supplier", "مورد")
            Width = 560
            Height = 500
            StartPosition = FormStartPosition.CenterParent
            Font = _defaultFont
            If Not Eng Then
                RightToLeft = RightToLeft.Yes
                RightToLeftLayout = True
            End If

            Dim layout As New TableLayoutPanel With {.Dock = DockStyle.Fill, .ColumnCount = 2, .RowCount = 8}
            layout.ColumnStyles.Add(New ColumnStyle(SizeType.Absolute, 140))
            layout.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 100))

            _txtName = New TextEdit() With {.Dock = DockStyle.Fill}
            _txtContact = New TextEdit() With {.Dock = DockStyle.Fill}
            _txtPhone = New TextEdit() With {.Dock = DockStyle.Fill}
            _txtEmail = New TextEdit() With {.Dock = DockStyle.Fill}
            _txtAddress = New MemoEdit() With {.Height = 60, .Dock = DockStyle.Fill}
            _txtTerms = New MemoEdit() With {.Height = 60, .Dock = DockStyle.Fill}
            _cboWhatsPrefix = New ComboBoxEdit() With {.Dock = DockStyle.Fill}
            _cboWhatsPrefix.Properties.Buttons.AddRange(New EditorButton() {New EditorButton(ButtonPredefines.Combo)})
            _cboWhatsPrefix.Properties.Appearance.Font = _defaultFont
            _cboWhatsPrefix.Properties.AppearanceDropDown.Font = _defaultFont
            _txtWhats = New TextEdit() With {.Width = 120}
            _txtWhats.Properties.MaxLength = 10
            _txtWhats.Properties.Appearance.Font = _defaultFont
            _lblFullWhats = New LabelControl With {
                .AutoSizeMode = LabelAutoSizeMode.None,
                .Width = 220,
                .Height = 22
            }
            WhatsHelper.ApplyFullWhatsLabelAppearance(_lblFullWhats, _defaultFont)
            Dim flpWhats As New FlowLayoutPanel With {
                .Dock = DockStyle.Fill,
                .FlowDirection = FlowDirection.LeftToRight,
                .WrapContents = False,
                .Padding = New Padding(0),
                .Margin = New Padding(0)
            }
            flpWhats.Controls.Add(_txtWhats)
            flpWhats.Controls.Add(_lblFullWhats)

            _txtName.Font = _defaultFont
            _txtContact.Font = _defaultFont
            _txtPhone.Font = _defaultFont
            _txtEmail.Font = _defaultFont
            _txtAddress.Font = _defaultFont
            _txtTerms.Font = _defaultFont

            layout.Controls.Add(New LabelControl With {.Text = If(Eng, "Name", "الاسم"), .Font = _defaultFont}, 0, 0)
            layout.Controls.Add(_txtName, 1, 0)
            layout.Controls.Add(New LabelControl With {.Text = If(Eng, "Contact", "جهة الاتصال"), .Font = _defaultFont}, 0, 1)
            layout.Controls.Add(_txtContact, 1, 1)
            layout.Controls.Add(New LabelControl With {.Text = If(Eng, "Phone", "الهاتف"), .Font = _defaultFont}, 0, 2)
            layout.Controls.Add(_txtPhone, 1, 2)
            layout.Controls.Add(New LabelControl With {.Text = If(Eng, "Email", "البريد الإلكتروني"), .Font = _defaultFont}, 0, 3)
            layout.Controls.Add(_txtEmail, 1, 3)
            layout.Controls.Add(New LabelControl With {.Text = If(Eng, "Address", "العنوان"), .Font = _defaultFont}, 0, 4)
            layout.Controls.Add(_txtAddress, 1, 4)
            layout.Controls.Add(New LabelControl With {.Text = If(Eng, "Terms", "شروط الدفع"), .Font = _defaultFont}, 0, 5)
            layout.Controls.Add(_txtTerms, 1, 5)
            layout.Controls.Add(New LabelControl With {.Text = If(Eng, "WhatsApp prefix", "بادئة واتساب"), .Font = _defaultFont}, 0, 6)
            layout.Controls.Add(_cboWhatsPrefix, 1, 6)
            layout.Controls.Add(New LabelControl With {.Text = If(Eng, "WhatsApp #", "رقم واتساب (محلي)"), .Font = _defaultFont}, 0, 7)
            layout.Controls.Add(flpWhats, 1, 7)

            Dim panelButtons As New PanelControl With {.Dock = DockStyle.Bottom, .Height = 48}
            Dim btnOk As New SimpleButton With {.Text = If(Eng, "OK", "موافق"), .DialogResult = DialogResult.OK, .Left = 300, .Top = 10}
            Dim btnCancel As New SimpleButton With {.Text = If(Eng, "Cancel", "إلغاء"), .DialogResult = DialogResult.Cancel, .Left = 390, .Top = 10}
            btnOk.Font = _defaultFont
            btnCancel.Font = _defaultFont
            panelButtons.Controls.AddRange(New Control() {btnOk, btnCancel})

            Controls.Add(layout)
            Controls.Add(panelButtons)

            _txtName.Text = _supplier.SupplierName
            _txtContact.Text = _supplier.ContactPerson
            _txtPhone.Text = _supplier.PhoneNumber
            _txtEmail.Text = _supplier.EmailAddress
            _txtAddress.Text = _supplier.PhysicalAddress
            _txtTerms.Text = _supplier.PaymentTerms

            WhatsHelper.BindGenericWhatsPrefixAndLocal(_cboWhatsPrefix, _txtWhats,
                If(_supplier.WhatsAppPrefix, ""), If(_supplier.WhatsApp, ""))
            WhatsHelper.RefreshFullWhatsDigitsLabel(_cboWhatsPrefix, _txtWhats, _lblFullWhats)
            WhatsHelper.AttachWhatsLocalDigitsOnlyKeyDown(_txtWhats)

            Dim refreshFull = Sub()
                                  WhatsHelper.RefreshFullWhatsDigitsLabel(_cboWhatsPrefix, _txtWhats, _lblFullWhats)
                              End Sub
            AddHandler _cboWhatsPrefix.SelectedIndexChanged, Sub(s, e) refreshFull()
            AddHandler _cboWhatsPrefix.EditValueChanged, Sub(s, e) refreshFull()
            AddHandler _txtWhats.EditValueChanged, Sub(s, e) refreshFull()

            AddHandler btnOk.Click, AddressOf OnSave
        End Sub

        Private Sub OnSave(sender As Object, e As EventArgs)
            _supplier.SupplierName = _txtName.Text.Trim()
            _supplier.ContactPerson = _txtContact.Text.Trim()
            _supplier.PhoneNumber = _txtPhone.Text.Trim()
            _supplier.EmailAddress = _txtEmail.Text.Trim()
            _supplier.PhysicalAddress = _txtAddress.Text.Trim()
            _supplier.PaymentTerms = _txtTerms.Text.Trim()
            Dim waP As String = Nothing
            Dim waL As String = Nothing
            WhatsHelper.ReadGenericWhatsFromControls(_cboWhatsPrefix, _txtWhats, waP, waL)
            _supplier.WhatsAppPrefix = waP
            _supplier.WhatsApp = waL
        End Sub
    End Class
