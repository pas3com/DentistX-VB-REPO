Imports System.ComponentModel
Imports System.Linq
Imports System.Threading.Tasks
Imports System.Data.SqlClient
Imports DevExpress.XtraEditors
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Views.Grid

Public Class StockProductsForm
        Inherits XtraForm

        Private ReadOnly _productRepo As ProductRepository
        Private ReadOnly _categoryRepo As CategoryRepository
        Private ReadOnly _unitRepo As UnitRepository
        Private _binding As BindingList(Of Product)
        Private ReadOnly _defaultFont As Font = New Font("Calibri", 10, FontStyle.Bold)

        Public Sub New()
            InitializeComponent()

            Dim conn = DentistXDATA.GetConnection.ConnectionString
            _productRepo = New ProductRepository(conn)
            _categoryRepo = New CategoryRepository(conn)
            _unitRepo = New UnitRepository(conn)

            Text = If(Eng, "Products", "المنتجات")

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
            AddHandler _btnCategories.Click, AddressOf OnCategories
            AddHandler _btnUnits.Click, AddressOf OnUnits
        End Sub

        Private Async Sub OnFormLoad(sender As Object, e As EventArgs)
            Await LoadDataAsync()
        End Sub

        Private Async Function LoadDataAsync() As Task
            Dim data = _productRepo.GetAll().ToList()
            _binding = New BindingList(Of Product)(data)
            _grid.DataSource = _binding
            ApplyGridCaptions()
            IntegerMoneyGridColumns.ApplyIntegerMoneyGridEditors(_view, "ReorderLevel")
            _view.BestFitColumns()
            Await Task.CompletedTask
        End Function

        Private Async Sub OnAdd(sender As Object, e As EventArgs)
            Dim p As New Product()
            Using dlg As New ProductEditForm(p, _categoryRepo.GetAll().ToList(), _unitRepo.GetAll().ToList())
                If dlg.ShowDialog(Me) = DialogResult.OK Then
                    Await _productRepo.InsertAsync(p)
                    Await LoadDataAsync()
                End If
            End Using
        End Sub

        Private Async Sub OnEdit(sender As Object, e As EventArgs)
            Dim p = TryCast(_view.GetFocusedRow(), Product)
            If p Is Nothing Then Return
            Dim edit = New Product With {
                .ProductID = p.ProductID,
                .ProductName = p.ProductName,
                .ProductDescription = p.ProductDescription,
                .UnitID = p.UnitID,
                .CategoryID = p.CategoryID,
                .ReorderLevel = p.ReorderLevel
            }
            Using dlg As New ProductEditForm(edit, _categoryRepo.GetAll().ToList(), _unitRepo.GetAll().ToList())
                If dlg.ShowDialog(Me) = DialogResult.OK Then
                    Await _productRepo.UpdateAsync(edit)
                    Await LoadDataAsync()
                End If
            End Using
        End Sub

        Private Async Sub OnDelete(sender As Object, e As EventArgs)
            Dim p = TryCast(_view.GetFocusedRow(), Product)
            If p Is Nothing Then Return
            If XtraMessageBox.Show(Me,
                                   If(Eng, $"Delete product '{p.ProductName}'?", $"حذف المنتج '{p.ProductName}'؟"),
                                   If(Eng, "Confirm", "تأكيد"),
                                   MessageBoxButtons.YesNo) = DialogResult.Yes Then
                Try
                    Await _productRepo.DeleteAsync(p.ProductID)
                    Await LoadDataAsync()
                Catch ex As SqlException
                    If ex.Number = 547 Then
                        XtraMessageBox.Show(Me,
                                            If(Eng,
                                               "You cannot delete this product because it is used in invoices or other stock records.",
                                               "لا يمكن حذف هذا المنتج لأنه مستخدم في الفواتير أو سجلات المخزون."),
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
            Using dlg As New CategoriesForm(_categoryRepo)
                dlg.ShowDialog(Me)
            End Using
        End Sub

        Private Sub OnUnits(sender As Object, e As EventArgs)
            Using dlg As New UnitsForm(_unitRepo)
                dlg.ShowDialog(Me)
            End Using
        End Sub

        Private Sub ApplyGridCaptions()
            Dim col = _view.Columns("ProductID")
            If col IsNot Nothing Then
                col.Caption = If(Eng, "Product ID", "رقم المنتج")
                col.Visible = False
            End If
            col = _view.Columns("ProductName")
            If col IsNot Nothing Then col.Caption = If(Eng, "Name", "الاسم")
            col = _view.Columns("ProductDescription")
            If col IsNot Nothing Then col.Caption = If(Eng, "Description", "الوصف")
            col = _view.Columns("UnitID")
            If col IsNot Nothing Then
                col.Caption = If(Eng, "Unit ID", "رقم الوحدة")
                col.Visible = False
            End If
            col = _view.Columns("CategoryID")
            If col IsNot Nothing Then
                col.Caption = If(Eng, "Category ID", "رقم التصنيف")
                col.Visible = False
            End If
            col = _view.Columns("CategoryName")
            If col IsNot Nothing Then col.Caption = If(Eng, "Category", "التصنيف")
            col = _view.Columns("UnitName")
            If col IsNot Nothing Then col.Caption = If(Eng, "Unit", "الوحدة")
            col = _view.Columns("ReorderLevel")
            If col IsNot Nothing Then col.Caption = If(Eng, "Reorder Level", "حد إعادة الطلب")
        End Sub
    End Class

Module StockUI

    Friend Class ProductEditForm
        Inherits XtraForm

        Private ReadOnly _product As Product
        Private ReadOnly _txtName As TextEdit
        Private ReadOnly _txtDesc As MemoEdit
        Private ReadOnly _cmbCategory As ComboBoxEdit
        Private ReadOnly _cmbUnit As ComboBoxEdit
        Private ReadOnly _spinReorder As TextEdit
        Private ReadOnly _categories As List(Of Category)
        Private ReadOnly _units As List(Of Unit)
        Private ReadOnly _defaultFont As Font = New Font("Calibri", 10, FontStyle.Bold)

        Public Sub New(product As Product, categories As List(Of Category), units As List(Of Unit))
            _product = product
            _categories = categories
            _units = units

            Text = If(Eng, "Product", "منتج")
            Width = 560
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

            _txtName = New TextEdit() With {.Dock = DockStyle.Fill}
            _txtDesc = New MemoEdit() With {.Height = 80, .Dock = DockStyle.Fill}
            _cmbCategory = New ComboBoxEdit() With {.Dock = DockStyle.Fill}
            _cmbUnit = New ComboBoxEdit() With {.Dock = DockStyle.Fill}
            _spinReorder = New TextEdit()
            _spinReorder.Dock = DockStyle.Fill
            IntegerMoneyEditorFocus.ConfigureIntegerMoneyTextEdit(_spinReorder)
            IntegerMoneyEditorFocus.AttachTextEditZeroEmptyElseSelectAll(_spinReorder)
            _txtName.Font = _defaultFont
            _txtDesc.Font = _defaultFont
            _cmbCategory.Font = _defaultFont
            _cmbUnit.Font = _defaultFont
            _spinReorder.Font = _defaultFont

            _cmbCategory.Properties.Items.AddRange(_categories.Select(Function(c) c.CategoryName).ToArray())
            _cmbUnit.Properties.Items.AddRange(_units.Select(Function(u) u.UnitName).ToArray())

            layout.Controls.Add(New LabelControl With {.Text = If(Eng, "Name", "الاسم"), .Font = _defaultFont}, 0, 0)
            layout.Controls.Add(_txtName, 1, 0)
            layout.Controls.Add(New LabelControl With {.Text = If(Eng, "Description", "الوصف"), .Font = _defaultFont}, 0, 1)
            layout.Controls.Add(_txtDesc, 1, 1)
            layout.Controls.Add(New LabelControl With {.Text = If(Eng, "Category", "التصنيف"), .Font = _defaultFont}, 0, 2)
            layout.Controls.Add(_cmbCategory, 1, 2)
            layout.Controls.Add(New LabelControl With {.Text = If(Eng, "Unit", "الوحدة"), .Font = _defaultFont}, 0, 3)
            layout.Controls.Add(_cmbUnit, 1, 3)
            layout.Controls.Add(New LabelControl With {.Text = If(Eng, "Reorder Level", "حد إعادة الطلب"), .Font = _defaultFont}, 0, 4)
            layout.Controls.Add(_spinReorder, 1, 4)

            Dim panelButtons As New PanelControl With {.Dock = DockStyle.Bottom, .Height = 48}
            Dim btnOk As New SimpleButton With {.Text = If(Eng, "OK", "موافق"), .DialogResult = DialogResult.OK, .Left = 320, .Top = 10}
            Dim btnCancel As New SimpleButton With {.Text = If(Eng, "Cancel", "إلغاء"), .DialogResult = DialogResult.Cancel, .Left = 410, .Top = 10}
            btnOk.Font = _defaultFont
            btnCancel.Font = _defaultFont
            panelButtons.Controls.AddRange(New Control() {btnOk, btnCancel})

            Controls.Add(layout)
            Controls.Add(panelButtons)

            _txtName.Text = _product.ProductName
            _txtDesc.Text = _product.ProductDescription
            _spinReorder.EditValue = _product.ReorderLevel
            _cmbCategory.SelectedIndex = Math.Max(0, _categories.FindIndex(Function(c) c.CategoryID = _product.CategoryID))
            _cmbUnit.SelectedIndex = Math.Max(0, _units.FindIndex(Function(u) u.UnitID = _product.UnitID))

            AddHandler btnOk.Click, AddressOf OnSave
        End Sub

        Private Sub OnSave(sender As Object, e As EventArgs)
            _product.ProductName = _txtName.Text.Trim()
            _product.ProductDescription = _txtDesc.Text.Trim()
            If _cmbCategory.SelectedIndex >= 0 Then
                _product.CategoryID = _categories(_cmbCategory.SelectedIndex).CategoryID
            End If
            If _cmbUnit.SelectedIndex >= 0 Then
                _product.UnitID = _units(_cmbUnit.SelectedIndex).UnitID
            End If
            _product.ReorderLevel = CInt(IntegerMoneyEditorFocus.DecimalFromIntegerMoneyEdit(_spinReorder))
        End Sub
    End Class

    Friend Class CategoriesForm
        Inherits XtraForm

        Private ReadOnly _repo As CategoryRepository
        Private ReadOnly _grid As GridControl
        Private ReadOnly _view As GridView
        Private _binding As BindingList(Of Category)
        Private ReadOnly _defaultFont As Font = New Font("Calibri", 10, FontStyle.Bold)

        Public Sub New(repo As CategoryRepository)
            _repo = repo
            Text = If(Eng, "Categories", "التصنيفات")
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
            _binding = New BindingList(Of Category)(_repo.GetAll().ToList())
            _grid.DataSource = _binding
            ApplyGridCaptions()
            _view.BestFitColumns()
            Await Task.CompletedTask
        End Function

        Private Async Function AddCategoryAsync() As Task
            Dim name = XtraInputBox.Show(If(Eng, "Category name:", "اسم التصنيف:"), If(Eng, "Add Category", "إضافة تصنيف"), "")
            If String.IsNullOrWhiteSpace(name) Then Return
            Await _repo.InsertAsync(New Category With {.CategoryName = name.Trim()})
            Await LoadDataAsync()
        End Function

        Private Async Function EditCategoryAsync() As Task
            Dim c = TryCast(_view.GetFocusedRow(), Category)
            If c Is Nothing Then Return
            Dim name = XtraInputBox.Show(If(Eng, "Category name:", "اسم التصنيف:"), If(Eng, "Edit Category", "تعديل تصنيف"), c.CategoryName)
            If String.IsNullOrWhiteSpace(name) Then Return
            c.CategoryName = name.Trim()
            Await _repo.UpdateAsync(c)
            Await LoadDataAsync()
        End Function

        Private Async Function DeleteCategoryAsync() As Task
            Dim c = TryCast(_view.GetFocusedRow(), Category)
            If c Is Nothing Then Return
            If XtraMessageBox.Show(Me,
                                   If(Eng, $"Delete category '{c.CategoryName}'?", $"حذف التصنيف '{c.CategoryName}'؟"),
                                   If(Eng, "Confirm", "تأكيد"),
                                   MessageBoxButtons.YesNo) = DialogResult.Yes Then
                Try
                    Await _repo.DeleteAsync(c.CategoryID)
                    Await LoadDataAsync()
                Catch ex As SqlException
                    If ex.Number = 547 Then
                        XtraMessageBox.Show(Me,
                                            If(Eng,
                                               "You cannot delete this category because it is used by existing products.",
                                               "لا يمكن حذف هذا التصنيف لأنه مستخدم في منتجات موجودة."),
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
            Dim col = _view.Columns("CategoryID")
            If col IsNot Nothing Then col.Caption = If(Eng, "Category ID", "رقم التصنيف")
            col = _view.Columns("CategoryName")
            If col IsNot Nothing Then col.Caption = If(Eng, "Category Name", "اسم التصنيف")
        End Sub
    End Class

    Friend Class UnitsForm
        Inherits XtraForm

        Private ReadOnly _repo As UnitRepository
        Private ReadOnly _grid As GridControl
        Private ReadOnly _view As GridView
        Private _binding As BindingList(Of Unit)
        Private ReadOnly _defaultFont As Font = New Font("Calibri", 10, FontStyle.Bold)

        Public Sub New(repo As UnitRepository)
            _repo = repo
            Text = If(Eng, "Units", "الوحدات")
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
            AddHandler btnAdd.Click, Async Sub() Await AddUnitAsync()
            AddHandler btnEdit.Click, Async Sub() Await EditUnitAsync()
            AddHandler btnDelete.Click, Async Sub() Await DeleteUnitAsync()
        End Sub

        Private Async Sub OnFormLoad(sender As Object, e As EventArgs)
            Await LoadDataAsync()
        End Sub

        Private Async Function LoadDataAsync() As Task
            _binding = New BindingList(Of Unit)(_repo.GetAll().ToList())
            _grid.DataSource = _binding
            ApplyGridCaptions()
            _view.BestFitColumns()
            Await Task.CompletedTask
        End Function

        Private Async Function AddUnitAsync() As Task
            Dim name = XtraInputBox.Show(If(Eng, "Unit name:", "اسم الوحدة:"), If(Eng, "Add Unit", "إضافة وحدة"), "")
            If String.IsNullOrWhiteSpace(name) Then Return
            Await _repo.InsertAsync(New Unit With {.UnitName = name.Trim()})
            Await LoadDataAsync()
        End Function

        Private Async Function EditUnitAsync() As Task
            Dim u = TryCast(_view.GetFocusedRow(), Unit)
            If u Is Nothing Then Return
            Dim name = XtraInputBox.Show(If(Eng, "Unit name:", "اسم الوحدة:"), If(Eng, "Edit Unit", "تعديل وحدة"), u.UnitName)
            If String.IsNullOrWhiteSpace(name) Then Return
            u.UnitName = name.Trim()
            Await _repo.UpdateAsync(u)
            Await LoadDataAsync()
        End Function

        Private Async Function DeleteUnitAsync() As Task
            Dim u = TryCast(_view.GetFocusedRow(), Unit)
            If u Is Nothing Then Return
            If XtraMessageBox.Show(Me,
                                   If(Eng, $"Delete unit '{u.UnitName}'?", $"حذف الوحدة '{u.UnitName}'؟"),
                                   If(Eng, "Confirm", "تأكيد"),
                                   MessageBoxButtons.YesNo) = DialogResult.Yes Then
                Try
                    Await _repo.DeleteAsync(u.UnitID)
                    Await LoadDataAsync()
                Catch ex As SqlException
                    If ex.Number = 547 Then
                        XtraMessageBox.Show(Me,
                                            If(Eng,
                                               "You cannot delete this unit because it is used by existing products.",
                                               "لا يمكن حذف هذه الوحدة لأنها مستخدمة في منتجات موجودة."),
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
            Dim col = _view.Columns("UnitID")
            If col IsNot Nothing Then col.Caption = If(Eng, "Unit ID", "رقم الوحدة")
            col = _view.Columns("UnitName")
            If col IsNot Nothing Then col.Caption = If(Eng, "Unit Name", "اسم الوحدة")
        End Sub
    End Class
End Module
