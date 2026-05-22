Imports System.ComponentModel
Imports System.Linq
Imports System.Threading.Tasks
Imports System.Data.SqlClient
Imports DevExpress.XtraEditors
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Views.Grid

Public Class StockExpensesForm
    Inherits XtraForm

    Private ReadOnly _expenseRepo As ExpenseRepository
    Private ReadOnly _categoryRepo As ExpenseCategoryRepository

    Private ReadOnly _defaultFont As Font = New Font("Calibri", 10, FontStyle.Bold)

    Private _binding As BindingList(Of Expense)

    Public Sub New()
        InitializeComponent()

        Dim conn = DentistXDATA.GetConnection.ConnectionString
        _expenseRepo = New ExpenseRepository(conn)
        _categoryRepo = New ExpenseCategoryRepository(conn)

        Text = If(Eng, "Expenses", "المصروفات")

        StartPosition = FormStartPosition.CenterScreen
        Font = _defaultFont
        If Not Eng Then
            RightToLeft = RightToLeft.Yes
            RightToLeftLayout = True
        End If

        _dateFrom.DateTime = Date.Today.AddMonths(-1)
        _dateTo.DateTime = Date.Today.AddDays(1)

        AddHandler Load, AddressOf OnFormLoad
        AddHandler _btnRefresh.Click, AddressOf OnRefresh
        AddHandler _btnAdd.Click, AddressOf OnAdd
        AddHandler _btnEdit.Click, AddressOf OnEdit
        AddHandler _btnDelete.Click, AddressOf OnDelete
        AddHandler _btnCategories.Click, AddressOf OnCategories
        AddHandler _cmbCategory.SelectedIndexChanged, AddressOf OnRefresh
        AddHandler _spinAmount.EditValueChanged, AddressOf OnRefresh
    End Sub

    Private Sub OnFormLoad(sender As Object, e As EventArgs)
        IntegerMoneyEditorFocus.ConfigureDecimal2MoneyTextEdit(_spinAmount)
        IntegerMoneyEditorFocus.AttachTextEditZeroEmptyElseSelectAll(_spinAmount)
        LoadFilterLists()
        LoadData()
    End Sub

    Private Sub LoadFilterLists()
        _cmbCategory.Properties.Items.Clear()
        _cmbCategory.Properties.Items.Add(If(Eng, "All categories", "كل التصنيفات"))
        _cmbCategory.Properties.Items.AddRange(_categoryRepo.GetAll().Select(Function(c) c.CategoryName).ToArray())
        _cmbCategory.SelectedIndex = 0

        _spinAmount.EditValue = 0D
    End Sub

    Private Sub OnRefresh(sender As Object, e As EventArgs)
        LoadData()
    End Sub

    Private Sub LoadData()
        Dim list = _expenseRepo.GetByRange(_dateFrom.DateTime.Date, _dateTo.DateTime.Date).ToList()

        ' Filter by category (index 0 = All)
        If _cmbCategory.SelectedIndex > 0 Then
            Dim categories = _categoryRepo.GetAll().ToList()
            If _cmbCategory.SelectedIndex <= categories.Count Then
                Dim catId = categories(_cmbCategory.SelectedIndex - 1).ExpenseCategoryID
                list = list.Where(Function(x) x.ExpenseCategoryID = catId).ToList()
            End If
        End If

        ' Filter by min amount
        Dim minAmt = IntegerMoneyEditorFocus.DecimalFromDecimal2MoneyEdit(_spinAmount)
        If minAmt > 0 Then
            list = list.Where(Function(x) x.Amount >= minAmt).ToList()
        End If

        _binding = New BindingList(Of Expense)(list)
        _grid.DataSource = _binding
        ApplyGridCaptions()
        IntegerMoneyGridColumns.ApplyDecimal2MoneyGridEditors(_view, "Amount")
        _view.BestFitColumns()
    End Sub

    Private Async Sub OnAdd(sender As Object, e As EventArgs)
        Dim exp As New Expense With {.ExpenseDate = Date.Today}
        Using dlg As New ExpenseEditForm(exp, _categoryRepo.GetAll().ToList())
            If dlg.ShowDialog(Me) = DialogResult.OK Then
                Await _expenseRepo.InsertAsync(exp)
                LoadData()
            End If
        End Using
    End Sub

    Private Async Sub OnEdit(sender As Object, e As EventArgs)
        Dim exp = TryCast(_view.GetFocusedRow(), Expense)
        If exp Is Nothing Then Return
        Dim edit = New Expense With {
                .ExpenseID = exp.ExpenseID,
                .ExpenseCategoryID = exp.ExpenseCategoryID,
                .ExpenseDate = exp.ExpenseDate,
                .Amount = exp.Amount,
                .PaymentMethod = exp.PaymentMethod,
                .ReferenceNumber = exp.ReferenceNumber,
                .Notes = exp.Notes
            }
        Using dlg As New ExpenseEditForm(edit, _categoryRepo.GetAll().ToList())
            If dlg.ShowDialog(Me) = DialogResult.OK Then
                Await _expenseRepo.UpdateAsync(edit)
                LoadData()
            End If
        End Using
    End Sub

    Private Async Sub OnDelete(sender As Object, e As EventArgs)
        Dim exp = TryCast(_view.GetFocusedRow(), Expense)
        If exp Is Nothing Then Return
        If XtraMessageBox.Show(Me, If(Eng, "Delete selected expense?", "حذف المصروف المحدد؟"), If(Eng, "Confirm", "تأكيد"), MessageBoxButtons.YesNo) = DialogResult.Yes Then
            Try
                Await _expenseRepo.DeleteAsync(exp.ExpenseID)
                LoadData()
            Catch ex As SqlException
                If ex.Number = 547 Then
                    XtraMessageBox.Show(Me,
                                            If(Eng,
                                               "You cannot delete this expense because it is linked to other accounting or stock records.",
                                               "لا يمكن حذف هذا المصروف لأنه مرتبط بسجلات محاسبية أو مخزون أخرى."),
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

    Private Sub OnCategories(sender As Object, e As EventArgs)
        Using dlg As New ExpenseCategoriesForm(_categoryRepo)
            dlg.ShowDialog(Me)
        End Using
    End Sub

    Private Sub ApplyGridCaptions()
        Dim col = _view.Columns("ExpenseID")
        If col IsNot Nothing Then
            col.Caption = If(Eng, "Expense ID", "رقم المصروف")
            col.Visible = False
        End If
        col = _view.Columns("ExpenseCategoryID")
        If col IsNot Nothing Then
            col.Caption = If(Eng, "Category ID", "رقم التصنيف")
            col.Visible = False
        End If
        col = _view.Columns("SupplierID")
        If col IsNot Nothing Then
            col.Caption = If(Eng, "Supplier ID", "رقم المورد")
            col.Visible = False
        End If
        col = _view.Columns("CategoryName")
        If col IsNot Nothing Then col.Caption = If(Eng, "Category", "التصنيف")
        col = _view.Columns("SupplierName")
        If col IsNot Nothing Then
            col.Caption = If(Eng, "Supplier", "المورد")
            col.Visible = False
        End If
        col = _view.Columns("ExpenseDate")
        If col IsNot Nothing Then col.Caption = If(Eng, "Date", "التاريخ")
        col = _view.Columns("Amount")
        If col IsNot Nothing Then col.Caption = If(Eng, "Amount", "المبلغ")
        col = _view.Columns("PaymentMethod")
        If col IsNot Nothing Then col.Caption = If(Eng, "Method", "طريقة الدفع")
        col = _view.Columns("ReferenceNumber")
        If col IsNot Nothing Then col.Caption = If(Eng, "Reference", "المرجع")
        col = _view.Columns("Notes")
        If col IsNot Nothing Then col.Caption = If(Eng, "Notes", "ملاحظات")
        col = _view.Columns("CreatedDate")
        If col IsNot Nothing Then col.Caption = If(Eng, "Created", "تاريخ الإنشاء")
    End Sub

    Private Async Sub btnSend_Click(sender As Object, e As EventArgs) Handles btnSend.Click
        Try
            If Not Await WhatsAppService.EnsureWhatsAppConnectedOrNotifyAsync(Me) Then Return
            Dim clinicId As String = WhatsAppService.GetCurrentClinicId()
            Dim waService As New WhatsAppService()
            ' Phone number: placeholder until wired to a recipient flow
            Dim number As String = "970512345678"
            Dim pdfPath As String = "C:\Invoices\inv-1001.pdf"
            Dim ctx As New WhatsAppSendContext With {
                .Category = WhatsAppMessageCategories.StockExpense,
                .SourceHint = NameOf(StockExpensesForm),
                .RevealMessageCenter = True
            }
            Dim result = Await waService.SendMessageAsync(clinicId, number, If(Eng, "Attached expense/review invoice.", "مرفق فاتورة المراجعة"), pdfPath, ctx)

            'MessageBox.Show(If(Eng, "Message queued for sending.", "تم وضع الرسالة في طابور الإرسال بنجاح!"), If(Eng, "Success", "نجاح"), MessageBoxButtons.OK, MessageBoxIcon.Information)

        Catch ex As Exception
            MessageBox.Show(ex.Message, If(Eng, "Send Error", "خطأ في الإرسال"), MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub panelTop_Paint(sender As Object, e As PaintEventArgs) Handles panelTop.Paint

    End Sub
End Class

Friend Class ExpenseCategoriesForm
    Inherits XtraForm

    Private ReadOnly _repo As ExpenseCategoryRepository
    Private ReadOnly _grid As GridControl
    Private ReadOnly _view As GridView
    Private _binding As BindingList(Of ExpenseCategory)
    Private ReadOnly _defaultFont As Font = New Font("Calibri", 10, FontStyle.Bold)

    Public Sub New(repo As ExpenseCategoryRepository)
        _repo = repo
        Text = If(Eng, "Expense Categories", "تصنيفات المصروفات")
        Width = 600
        Height = 420
        StartPosition = FormStartPosition.CenterParent
        Font = _defaultFont
        If Not Eng Then
            RightToLeft = RightToLeft.Yes
            RightToLeftLayout = True
        End If

        Dim panelTop As New PanelControl With {.Dock = DockStyle.Top, .Height = 48}
        Dim btnAdd As New SimpleButton With {.Text = If(Eng, "Add", "إضافة"), .Width = 90, .Left = 10, .Top = 10}
        Dim btnEdit As New SimpleButton With {.Text = If(Eng, "Edit", "تعديل"), .Width = 90, .Left = 110, .Top = 10}
        Dim btnDelete As New SimpleButton With {.Text = If(Eng, "Delete", "حذف"), .Width = 90, .Left = 210, .Top = 10}
        Dim btnRefresh As New SimpleButton With {.Text = If(Eng, "Refresh", "تحديث"), .Width = 90, .Left = 310, .Top = 10}
        btnAdd.Font = _defaultFont
        btnEdit.Font = _defaultFont
        btnDelete.Font = _defaultFont
        btnRefresh.Font = _defaultFont
        panelTop.Controls.AddRange(New Control() {btnAdd, btnEdit, btnDelete, btnRefresh})

        _grid = New GridControl With {.Dock = DockStyle.Fill}
        _view = New GridView(_grid)
        _grid.MainView = _view
        _grid.ViewCollection.Add(_view)
        _view.Appearance.Row.Font = _defaultFont
        _view.Appearance.HeaderPanel.Font = _defaultFont

        Controls.Add(_grid)
        Controls.Add(panelTop)

        AddHandler Load, AddressOf OnFormLoad
        AddHandler btnRefresh.Click, Async Sub() Await LoadDataAsync()
        AddHandler btnAdd.Click, Async Sub() Await AddCategoryAsync()
        AddHandler btnEdit.Click, Async Sub() Await EditCategoryAsync()
        AddHandler btnDelete.Click, Async Sub() Await DeleteCategoryAsync()
    End Sub

    Private Async Sub OnFormLoad(sender As Object, e As EventArgs)
        Await LoadDataAsync()
    End Sub

    Private Async Function LoadDataAsync() As Task
        _binding = New BindingList(Of ExpenseCategory)(_repo.GetAll().ToList())
        _grid.DataSource = _binding
        ApplyGridCaptions()
        _view.BestFitColumns()
        Await Task.CompletedTask
    End Function

    Private Async Function AddCategoryAsync() As Task
        Dim name = XtraInputBox.Show(If(Eng, "Category name:", "اسم التصنيف:"), If(Eng, "Add Category", "إضافة تصنيف"), "")
        If String.IsNullOrWhiteSpace(name) Then Return
        Await _repo.InsertAsync(New ExpenseCategory With {.CategoryName = name.Trim()})
        Await LoadDataAsync()
    End Function

    Private Async Function EditCategoryAsync() As Task
        Dim c = TryCast(_view.GetFocusedRow(), ExpenseCategory)
        If c Is Nothing Then Return
        Dim name = XtraInputBox.Show(If(Eng, "Category name:", "اسم التصنيف:"), If(Eng, "Edit Category", "تعديل تصنيف"), c.CategoryName)
        If String.IsNullOrWhiteSpace(name) Then Return
        c.CategoryName = name.Trim()
        Await _repo.UpdateAsync(c)
        Await LoadDataAsync()
    End Function

    Private Async Function DeleteCategoryAsync() As Task
        Dim c = TryCast(_view.GetFocusedRow(), ExpenseCategory)
        If c Is Nothing Then Return
        If XtraMessageBox.Show(Me,
                                   If(Eng, $"Delete category '{c.CategoryName}'?", $"حذف التصنيف '{c.CategoryName}'؟"),
                                   If(Eng, "Confirm", "تأكيد"),
                                   MessageBoxButtons.YesNo) = DialogResult.Yes Then
            Try
                Await _repo.DeleteAsync(c.ExpenseCategoryID)
                Await LoadDataAsync()
            Catch ex As SqlException
                If ex.Number = 547 Then
                    XtraMessageBox.Show(Me,
                                            If(Eng,
                                               "You cannot delete this expense category because it is used by existing expenses.",
                                               "لا يمكن حذف تصنيف المصروف لأنه مستخدم في مصروفات موجودة."),
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
    End Function

        Private Sub ApplyGridCaptions()
            Dim col = _view.Columns("ExpenseCategoryID")
            If col IsNot Nothing Then col.Caption = If(Eng, "Category ID", "رقم التصنيف")
            col = _view.Columns("CategoryName")
            If col IsNot Nothing Then col.Caption = If(Eng, "Category Name", "اسم التصنيف")
        End Sub
    End Class

    Friend Class ExpenseEditForm
        Inherits XtraForm

        Private ReadOnly _expense As Expense
        Private ReadOnly _categories As List(Of ExpenseCategory)
        Private ReadOnly _cmbCategory As ComboBoxEdit
        Private ReadOnly _dateExp As DateEdit
        Private ReadOnly _spinAmount As TextEdit
        Private ReadOnly _cmbMethod As ComboBoxEdit
        Private ReadOnly _txtRef As TextEdit
        Private ReadOnly _txtNotes As MemoEdit
        Private ReadOnly _defaultFont As Font = New Font("Calibri", 10, FontStyle.Bold)

        Public Sub New(expense As Expense, categories As List(Of ExpenseCategory))
            _expense = expense
            _categories = categories

            Text = If(Eng, "Expense", "مصروف")
            Width = 540
            Height = 420
            StartPosition = FormStartPosition.CenterParent
            Font = _defaultFont
            If Not Eng Then
                RightToLeft = RightToLeft.Yes
                RightToLeftLayout = True
            End If

            Dim layout As New TableLayoutPanel With {.Dock = DockStyle.Fill, .ColumnCount = 2, .RowCount = 5}
            layout.ColumnStyles.Add(New ColumnStyle(SizeType.Absolute, 140))
            layout.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 100))

            _cmbCategory = New ComboBoxEdit() With {.Dock = DockStyle.Fill}
            _cmbCategory.Properties.Items.AddRange(_categories.Select(Function(c) c.CategoryName).ToArray())
            _dateExp = New DateEdit() With {.Dock = DockStyle.Fill}
            _spinAmount = New TextEdit() With {.Dock = DockStyle.Fill}
            IntegerMoneyEditorFocus.ConfigureDecimal2MoneyTextEdit(_spinAmount)
            IntegerMoneyEditorFocus.AttachTextEditZeroEmptyElseSelectAll(_spinAmount)
            _cmbMethod = New ComboBoxEdit() With {.Dock = DockStyle.Fill}
            _cmbMethod.Properties.Items.AddRange(New String() {
                If(Eng, "Cash", "نقداً"),
                If(Eng, "BankTransfer", "تحويل بنكي"),
                If(Eng, "CreditCard", "بطاقة")
            })
            _txtRef = New TextEdit() With {.Dock = DockStyle.Fill}
            _txtNotes = New MemoEdit() With {.Height = 80, .Dock = DockStyle.Fill}
            _cmbCategory.Font = _defaultFont
            _dateExp.Font = _defaultFont
            _spinAmount.Font = _defaultFont
            _cmbMethod.Font = _defaultFont
            _txtRef.Font = _defaultFont
            _txtNotes.Font = _defaultFont

            layout.Controls.Add(New LabelControl With {.Text = If(Eng, "Category", "التصنيف"), .Font = _defaultFont}, 0, 0)
            layout.Controls.Add(_cmbCategory, 1, 0)
            layout.Controls.Add(New LabelControl With {.Text = If(Eng, "Date", "التاريخ"), .Font = _defaultFont}, 0, 1)
            layout.Controls.Add(_dateExp, 1, 1)
            layout.Controls.Add(New LabelControl With {.Text = If(Eng, "Amount", "المبلغ"), .Font = _defaultFont}, 0, 2)
            layout.Controls.Add(_spinAmount, 1, 2)
            layout.Controls.Add(New LabelControl With {.Text = If(Eng, "Method", "طريقة الدفع"), .Font = _defaultFont}, 0, 3)
            layout.Controls.Add(_cmbMethod, 1, 3)
            layout.Controls.Add(New LabelControl With {.Text = If(Eng, "Notes", "ملاحظات"), .Font = _defaultFont}, 0, 4)
            layout.Controls.Add(_txtNotes, 1, 4)

            Dim panelButtons As New PanelControl With {.Dock = DockStyle.Bottom, .Height = 48}
            Dim btnOk As New SimpleButton With {.Text = If(Eng, "OK", "موافق"), .DialogResult = DialogResult.OK, .Left = 300, .Top = 10}
            Dim btnCancel As New SimpleButton With {.Text = If(Eng, "Cancel", "إلغاء"), .DialogResult = DialogResult.Cancel, .Left = 390, .Top = 10}
            btnOk.Font = _defaultFont
            btnCancel.Font = _defaultFont
            panelButtons.Controls.AddRange(New Control() {btnOk, btnCancel})

            Controls.Add(layout)
            Controls.Add(panelButtons)

            _cmbCategory.SelectedIndex = Math.Max(0, _categories.FindIndex(Function(c) c.ExpenseCategoryID = _expense.ExpenseCategoryID))
            _dateExp.DateTime = If(_expense.ExpenseDate = Date.MinValue, Date.Today, _expense.ExpenseDate)
            _spinAmount.EditValue = _expense.Amount
            _cmbMethod.Text = If(String.IsNullOrWhiteSpace(_expense.PaymentMethod),
                                 If(Eng, "Cash", "نقداً"),
                                 _expense.PaymentMethod)
            _txtRef.Text = _expense.ReferenceNumber
            _txtNotes.Text = _expense.Notes

            AddHandler btnOk.Click, AddressOf OnSave
        End Sub

        Private Sub OnSave(sender As Object, e As EventArgs)
            If _cmbCategory.SelectedIndex < 0 Then
                XtraMessageBox.Show(Me, If(Eng, "Select a category.", "يرجى اختيار تصنيف."), If(Eng, "Validation", "تنبيه"), MessageBoxButtons.OK)
                DialogResult = DialogResult.None
                Return
            End If
            _expense.ExpenseCategoryID = _categories(_cmbCategory.SelectedIndex).ExpenseCategoryID
            _expense.SupplierID = Nothing
            _expense.ExpenseDate = _dateExp.DateTime.Date
            _expense.Amount = IntegerMoneyEditorFocus.DecimalFromDecimal2MoneyEdit(_spinAmount)
            _expense.PaymentMethod = _cmbMethod.Text
            _expense.ReferenceNumber = _txtRef.Text.Trim()
            _expense.Notes = _txtNotes.Text.Trim()
        End Sub
    End Class
