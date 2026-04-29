Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Drawing
Imports System.Linq
Imports System.Windows.Forms
Imports DevExpress.XtraEditors
Imports DevExpress.XtraGrid.Views.Base

Partial Public Class FrmUserFormAccess

    Private ReadOnly _usersData As New UsersDATA()
    Private ReadOnly _formsData As New FormAccessDATA()
    Private ReadOnly _ufa As New UserFormAccessDATA()
    Private ReadOnly _rows As New BindingList(Of UserFormAccessRowVm)()
    Private _allForms As List(Of FormAccess)
    Private _srcRefHitsByFormName As Dictionary(Of String, Integer)

    Public Sub New()
        MyBase.New()
        InitializeComponent()
        Text = If(Eng, "Form access by user", "صلاحية الشاشات للمستخدم")
        lblHeading.Text = If(Eng,
            "Choose a user, tick Allow for each screen they may open, then Save. ADMINS always have full access.",
            "اختر مستخدماً وحدد السماح لكل شاشة، ثم احفظ. مجموعة ADMINS ترى كل شيء.")
        lblUser.Text = If(Eng, "User:", "المستخدم:")
        lblSearch.Text = If(Eng, "Filter:", "تصفية:")
        lblGridCaption.Text = If(Eng,
            "Step 2: The list below is every registered form—not a separate picker. Choose the user above, tick Allow for each screen they may open, then Save.",
            "الخطوة 2: القائمة أدناه هي كل النماذج المسجّلة. اختر المستخدم أعلاه، حدد السماح لكل شاشة، ثم احفظ.")
        lblHint.Text = If(Eng,
            "Tip: Sync first. Title is stored in the database—sync can fill it from a class <DisplayName(""…"")> attribute (no form load). For user controls, use that attribute or type a Title and Save; Tag cannot be read without creating the control.",
            "نصيحة: زامن أولاً. العنوان يُحفظ في القاعدة—ويمكن للمزامنة ملؤه من خاصية DisplayName على الصنف. للمستخدم الفرعي استخدمها أو اكتب العنوان واحفظ؛ لا يُقرأ Tag دون إنشاء عنصر التحكم.")
        colFormName.Caption = If(Eng, "Form name", "اسم النموذج")
        colSrcRefs.Caption = If(Eng, "Src hits", "عدد الظهور")
        colSrcRefs.ToolTip = If(Eng,
            "Whole-word matches of the class name in local .vb sources (estimate). Click Scan source hits. Low counts may mean unused or opened via strings/reflection.",
            "تقدير لظهور اسم الصنف كلمات كاملة في ملفات VB. اضغط ""فحص المصدر"". العدد المنخفض قد يعني غير مستخدم أو استدعاء عبر نصوص.")
        colTitle.Caption = If(Eng, "Title (English)", "العنوان (إنجليزي)")
        colTitleAr.Caption = If(Eng, "Title (Arabic)", "العنوان (عربي)")
        colDescription.Caption = If(Eng, "Description", "الوصف")
        colAllowed.Caption = If(Eng, "Allow (this user)", "مسموح")
        btnPruneObsolete.Text = If(Eng, "Remove obsolete forms", "حذف النماذج المحذوفة من التطبيق")
        btnScanSourceRefs.Text = If(Eng, "Scan source hits", "فحص المصدر")
    End Sub

    Private Sub FrmUserFormAccess_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            _allForms = _formsData.SelectAll()
        Catch ex As Exception
            XtraMessageBox.Show(Me,
                If(Eng, $"Could not load Forms table: {ex.Message}", $"تعذر تحميل جدول النماذج: {ex.Message}"))
            Close()
            Return
        End Try

        Dim userList = _usersData.SelectAll().OrderBy(Function(u) u.UsName).ToList()
        luUser.Properties.DataSource = userList
        luUser.Properties.DisplayMember = "UsName"
        luUser.Properties.ValueMember = "UsID"
        luUser.Properties.Columns.Clear()
        luUser.Properties.Columns.Add(New DevExpress.XtraEditors.Controls.LookUpColumnInfo("UsName", If(Eng, "User", "المستخدم")) With {.Width = 320})
        luUser.Properties.PopupWidth = 420

        GridFormAccess.DataSource = _rows
        txtFilter.Text = ""

        UpdateEmptyStateHint()

        If userList.Count > 0 Then
            luUser.EditValue = userList(0).UsID
            ReloadRowsForUser(CInt(userList(0).UsID))
        End If
    End Sub

    Private Sub luUser_EditValueChanged(sender As Object, e As EventArgs) Handles luUser.EditValueChanged
        If luUser.EditValue Is Nothing OrElse luUser.EditValue Is DBNull.Value Then Return
        ReloadRowsForUser(CInt(luUser.EditValue))
    End Sub

    Private Sub UpdateEmptyStateHint()
        If _allForms Is Nothing OrElse _allForms.Count = 0 Then
            lblGridCaption.Appearance.ForeColor = Color.DarkRed
            lblGridCaption.Text = If(Eng,
                "No forms in the database yet. Click Sync forms from app, then assign Allow per user.",
                "لا توجد نماذج في القاعدة. استخدم ""مزامنة النماذج"" ثم حدد السماح لكل مستخدم.")
        Else
            lblGridCaption.Appearance.ForeColor = Color.FromArgb(64, 64, 64)
            lblGridCaption.Text = If(Eng,
                "Step 2: The list below is every registered form—not a separate picker. Choose the user above, tick Allow for each screen they may open, then Save.",
                "الخطوة 2: القائمة أدناه هي كل النماذج المسجّلة. اختر المستخدم أعلاه، حدد السماح لكل شاشة، ثم احفظ.")
        End If
    End Sub

    Private Sub ReloadRowsForUser(usId As Integer)
        Dim allowed = _ufa.GetAllowedFormIdsForUser(usId)
        _rows.RaiseListChangedEvents = False
        _rows.Clear()
        Dim ftxt = If(txtFilter.Text, "").Trim()
        For Each f In _allForms.OrderBy(Function(x) x.FormName)
            Dim srcDisp = ""
            If _srcRefHitsByFormName IsNot Nothing Then
                Dim h As Integer
                If _srcRefHitsByFormName.TryGetValue(If(f.FormName, ""), h) Then srcDisp = h.ToString()
            End If
            If ftxt.Length > 0 Then
                Dim matchName = f.FormName IsNot Nothing AndAlso f.FormName.IndexOf(ftxt, StringComparison.OrdinalIgnoreCase) >= 0
                Dim matchTitle = f.DisplayTitle IsNot Nothing AndAlso f.DisplayTitle.IndexOf(ftxt, StringComparison.OrdinalIgnoreCase) >= 0
                Dim matchTitleAr = f.DisplayTitleAr IsNot Nothing AndAlso f.DisplayTitleAr.IndexOf(ftxt, StringComparison.OrdinalIgnoreCase) >= 0
                Dim matchDesc = f.Description IsNot Nothing AndAlso f.Description.IndexOf(ftxt, StringComparison.OrdinalIgnoreCase) >= 0
                Dim matchSrc = srcDisp.Length > 0 AndAlso srcDisp.IndexOf(ftxt, StringComparison.OrdinalIgnoreCase) >= 0
                If Not matchName AndAlso Not matchTitle AndAlso Not matchTitleAr AndAlso Not matchDesc AndAlso Not matchSrc Then Continue For
            End If
            _rows.Add(New UserFormAccessRowVm With {
                .FormID = f.FormID,
                .FormName = f.FormName,
                .SrcRefHits = srcDisp,
                .Title = If(f.DisplayTitle, ""),
                .TitleAr = If(f.DisplayTitleAr, ""),
                .Description = If(f.Description, ""),
                .IsAllowed = allowed.Contains(f.FormID)
            })
        Next
        _rows.RaiseListChangedEvents = True
        _rows.ResetBindings()
        GridViewFormAccess.BeginUpdate()
        colFormName.BestFit()
        colFormName.OptionsColumn.FixedWidth = True
        colSrcRefs.BestFit()
        colSrcRefs.OptionsColumn.FixedWidth = True
        GridViewFormAccess.EndUpdate()
    End Sub

    Private Sub txtFilter_EditValueChanged(sender As Object, e As EventArgs) Handles txtFilter.EditValueChanged
        If luUser.EditValue Is Nothing OrElse luUser.EditValue Is DBNull.Value Then Return
        ReloadRowsForUser(CInt(luUser.EditValue))
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        GridViewFormAccess.PostEditor()
        GridViewFormAccess.UpdateCurrentRow()
        If luUser.EditValue Is Nothing OrElse luUser.EditValue Is DBNull.Value Then Return
        Dim usId = CInt(luUser.EditValue)
        Try
            Dim allowedIds = _rows.Where(Function(r) r.IsAllowed).Select(Function(r) r.FormID).ToList()
            _ufa.ReplaceAllForUser(usId, allowedIds)
            _formsData.UpdateDisplayTitles(_rows)
            _allForms = _formsData.SelectAll()
            XtraMessageBox.Show(Me, If(Eng, "Saved.", "تم الحفظ."), If(Eng, "Form access", "صلاحيات"), MessageBoxButtons.OK, MessageBoxIcon.Information)
            If PasswordSecurity.CurrentUser IsNot Nothing AndAlso PasswordSecurity.CurrentUser.UsID = usId Then
                FormAccessGate.RefreshAfterLogin()
                Dim mv = TryCast(Application.OpenForms.OfType(Of MainView3)().FirstOrDefault(), MainView3)
                mv?.ApplyFormAccessShell()
            End If
        Catch ex As Exception
            XtraMessageBox.Show(Me,
                If(Eng, $"Save failed. Run UserFormAccess.sql if the table is missing.{vbCrLf}{ex.Message}",
                    $"فشل الحفظ. نفّذ سكربت إنشاء الجدول.{vbCrLf}{ex.Message}"),
                If(Eng, "Error", "خطأ"), MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        _allForms = _formsData.SelectAll()
        UpdateEmptyStateHint()
        If luUser.EditValue IsNot Nothing AndAlso luUser.EditValue IsNot DBNull.Value Then
            ReloadRowsForUser(CInt(luUser.EditValue))
        End If
    End Sub

    Private Sub btnSyncForms_Click(sender As Object, e As EventArgs) Handles btnSyncForms.Click
        Try
            Dim n = FormRegistrySync.SyncWinFormsFromExecutingAssembly()
            _allForms = _formsData.SelectAll()
            UpdateEmptyStateHint()
            If luUser.EditValue IsNot Nothing AndAlso luUser.EditValue IsNot DBNull.Value Then
                ReloadRowsForUser(CInt(luUser.EditValue))
            End If
            XtraMessageBox.Show(Me,
                If(Eng, $"Sync complete. {n} new form(s) added to registry.", $"اكتملت المزامنة. {n} نموذج جديد."),
                If(Eng, "Forms", "نماذج"), MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            XtraMessageBox.Show(Me, ex.Message, If(Eng, "Sync failed", "فشل"), MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnScanSourceRefs_Click(sender As Object, e As EventArgs) Handles btnScanSourceRefs.Click
        Dim root = FormSourceRefScanner.TryFindDentistXProjectSourceRoot()
        If root Is Nothing Then
            XtraMessageBox.Show(Me,
                If(Eng,
                    "Could not find DentistX.vbproj near this executable. Scan needs the project source folder (typical when running from Visual Studio output).",
                    "لم يُعثر على DentistX.vbproj بجانب التطبيق. الفحص يحتاج مجلد المشروع."),
                If(Eng, "Source scan", "فحص المصدر"), MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If
        Cursor = Cursors.WaitCursor
        Try
            Dim names = _allForms.Where(Function(x) Not String.IsNullOrWhiteSpace(x.FormName)).Select(Function(x) x.FormName).Distinct(StringComparer.OrdinalIgnoreCase).ToList()
            _srcRefHitsByFormName = FormSourceRefScanner.CountNameHitsInProjectSource(root, names)
            If luUser.EditValue IsNot Nothing AndAlso luUser.EditValue IsNot DBNull.Value Then
                ReloadRowsForUser(CInt(luUser.EditValue))
            End If
            XtraMessageBox.Show(Me,
                If(Eng, $"Scan complete. Project: {root}", $"اكتمل الفحص: {root}"),
                If(Eng, "Source hits", "ظهور في المصدر"), MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            XtraMessageBox.Show(Me, ex.Message, If(Eng, "Scan failed", "فشل الفحص"), MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub btnPruneObsolete_Click(sender As Object, e As EventArgs) Handles btnPruneObsolete.Click
        Dim q = If(Eng,
            "Delete Forms registry rows that no longer exist in this application (or in the main menu map)?" & vbCrLf &
            "Matching per-user permissions (UserFormAccess) will be removed too. This cannot be undone.",
            "حذف سجلات النماذج التي لم تعد موجودة في التطبيق (أو في قائمة الشاشات)؟" & vbCrLf &
            "سُتحذف صلاحيات المستخدمين المرتبطة أيضاً. لا يمكن التراجع.")
        If XtraMessageBox.Show(Me, q, If(Eng, "Remove obsolete", "تأكيد الحذف"), MessageBoxButtons.YesNo, MessageBoxIcon.Warning) <> DialogResult.Yes Then
            Return
        End If
        Try
            Dim r = FormRegistrySync.PruneObsoleteFormsFromRegistry()
            _allForms = _formsData.SelectAll()
            UpdateEmptyStateHint()
            If luUser.EditValue IsNot Nothing AndAlso luUser.EditValue IsNot DBNull.Value Then
                ReloadRowsForUser(CInt(luUser.EditValue))
            End If
            Dim msg = If(Eng,
                $"Removed {r.RemovedForms} form(s) from registry and {r.RemovedUserFormAccessRows} user permission row(s).",
                $"تم حذف {r.RemovedForms} نموذجاً و{r.RemovedUserFormAccessRows} صف صلاحيات مستخدم.")
            XtraMessageBox.Show(Me, msg, If(Eng, "Prune complete", "اكتمال"), MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            XtraMessageBox.Show(Me, ex.Message, If(Eng, "Prune failed", "فشل الحذف"), MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnGrantAll_Click(sender As Object, e As EventArgs) Handles btnGrantAll.Click
        For Each r In _rows
            r.IsAllowed = True
        Next
        _rows.ResetBindings()
    End Sub

    Private Sub btnDenyAll_Click(sender As Object, e As EventArgs) Handles btnDenyAll.Click
        For Each r In _rows
            r.IsAllowed = False
        Next
        _rows.ResetBindings()
    End Sub

    Private Sub btnLegacyKeys_Click(sender As Object, e As EventArgs) Handles btnLegacyKeys.Click
        FrmPermissionsMngr.Icon = TryCast(Owner, Form)?.Icon
        FrmPermissionsMngr.ShowDialog(Me)
    End Sub

End Class
