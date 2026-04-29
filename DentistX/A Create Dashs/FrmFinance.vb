Imports System.Data
Imports System.Drawing
Imports System.Windows.Forms
Imports DevExpress.Data
Imports DevExpress.LookAndFeel
Imports DevExpress.XtraBars.Navigation
Imports DevExpress.XtraEditors
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Views.Grid

Partial Public Class FrmFinance

    Private Shared ReadOnly UiFont As New Font("Calibri", 10.0F, FontStyle.Bold, GraphicsUnit.Point)

    Private lblKpiTreatTotal As LabelControl
    Private lblKpiPatientPaysTotal As LabelControl
    Private lblKpiExpTotal As LabelControl
    Private lblKpiPurchTotal As LabelControl
    Private lblKpiSupplierPaysTotal As LabelControl
    Private lblKpiLabTotal As LabelControl
    Private lblKpiStaffTotal As LabelControl
    Private lblFinanceWarnings As LabelControl

    Private ReadOnly _financeLoader As New FinancePeriodLoader()

    Public Sub New()
        InitializeComponent()
    End Sub

    Protected Overrides Sub OnShown(e As EventArgs)
        MyBase.OnShown(e)
        Dim mv3 = TryCast(Owner, MainView3)
        If mv3 IsNot Nothing Then
            Bounds = mv3.GetContainerAScreenBounds()
        Else
            CenterToScreen()
        End If
    End Sub

    Private Sub FrmFinance_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        dateFinanceFrom.EditValue = Date.Today.AddMonths(-1)
        dateFinanceTo.EditValue = Date.Today

        MakeKpiTotalLabels()
        ApplyFinanceToolbarAndKpiTexts()
        LayoutKpiCards()
        ApplyCalibriBold10Everywhere(Me)
        WireKpiNavigation(pnlKpiTreat, pageFinTreatments)
        WireKpiNavigation(pnlKpiPatientPays, pageFinPatientPays)
        WireKpiNavigation(pnlKpiExp, pageFinExpenses)
        WireKpiNavigation(pnlKpiPurch, pageFinPurchases)
        WireKpiNavigation(pnlKpiSupplierPays, pageFinSupplierPays)
        WireKpiNavigation(pnlKpiLab, pageFinLab)
        WireKpiNavigation(pnlKpiStaff, pageFinStaff)

        lblFinanceWarnings = New LabelControl With {
            .AutoSizeMode = LabelAutoSizeMode.None,
            .Size = New Size(900, 18),
            .Location = New Point(16, 66),
            .Visible = False
        }
        lblFinanceWarnings.Appearance.Font = New Font("Calibri", 8.0F, FontStyle.Italic, GraphicsUnit.Point)
        lblFinanceWarnings.Appearance.ForeColor = Color.FromArgb(140, 90, 30)
        lblFinanceWarnings.Appearance.Options.UseFont = True
        lblFinanceWarnings.Appearance.Options.UseForeColor = True
        pnlFinanceToolbar.Controls.Add(lblFinanceWarnings)

        RefreshAllFinanceData()
    End Sub

    Private Sub ApplyFinanceToolbarAndKpiTexts()
        lblIncomeCaption.Text = If(Eng,
            "Treatment / service income (accrued TrtValue; Patient payments card = cash in)",
            "دخل العلاجات والخدمات (مستحق؛ بطاقة «مدفوعات المرضى» = النقد المحصل)")
        lblOutflowsCaption.Text = If(Eng,
            "Total outflows (expenses + invoices + lab + staff). Supplier payments card = cash to suppliers.",
            "إجمالي الصرف (مصاريف + فواتير + مختبر + رواتب). بطاقة «مدفوعات الموردين» = النقد للموردين.")
        lblNetCaption.Text = If(Eng,
            "Net (accrued income − outflows above; green if positive)",
            "الصافي (دخل مستحق − الصرف؛ أخضر إذا موجب)")

        lblOverviewIntro.Text = If(Eng,
            "Overview — accrued vs cash is labeled on each card. Click a card for lines. Refresh reloads all figures.",
            "نظرة عامة — يُوضَّح على كل بطاقة المستحق مقابل النقد. انقر للتفاصيل. «تحديث» يعيد تحميل الأرقام.")

        lblKpiTreatTitle.Text = If(Eng, "Treatment income", "دخل العلاجات")
        lblKpiTreatHint.Text = If(Eng, "Accrued (TrtValue) in period", "مستحق (قيمة العلاج) خلال الفترة")

        lblKpiExpTitle.Text = If(Eng, "Expense payments", "مدفوعات المصاريف")
        lblKpiExpHint.Text = If(Eng, "Operating expenses paid", "مصاريف التشغيل المدفوعة")

        lblKpiPurchTitle.Text = If(Eng, "Purchase invoices", "فواتير المشتريات")
        lblKpiPurchHint.Text = If(Eng, "Supplier invoice totals (accrual)", "إجمالي فواتير الموردين (مستحق)")

        lblKpiLabTitle.Text = If(Eng, "Lab payments", "مدفوعات المختبر")
        lblKpiLabHint.Text = If(Eng, "Laboratory pays in period", "مدفوعات المختبر في الفترة")

        lblKpiStaffTitle.Text = If(Eng, "Staff payments", "مدفوعات الموظفين")
        lblKpiStaffHint.Text = If(Eng, "Payroll & personnel pays", "الرواتب ومدفوعات العاملين")

        Dim goTxt = If(Eng, "Open detail →", "فتح التفاصيل ←")
        lblKpiTreatGo.Text = goTxt
        lblKpiExpGo.Text = goTxt
        lblKpiPurchGo.Text = goTxt
        lblKpiLabGo.Text = goTxt
        lblKpiStaffGo.Text = goTxt

        lblKpiPatientPaysTitle.Text = If(Eng, "Patient payments", "مدفوعات المرضى")
        lblKpiPatientPaysHint.Text = If(Eng, "Cash in (Patient_Pays table)", "النقد الوارد (جدول Patient_Pays)")
        lblKpiPatientPaysGo.Text = goTxt
        lblKpiSupplierPaysTitle.Text = If(Eng, "Supplier payments", "مدفوعات الموردين")
        lblKpiSupplierPaysHint.Text = If(Eng, "Cash out (Payments vs BuyInvoices / Suppliers)", "النقد الصادر (Payments مع BuyInvoices والموردين)")
        lblKpiSupplierPaysGo.Text = goTxt
        pageFinPatientPays.Caption = If(Eng, "Patient pays", "مدفوعات المرضى")
        pageFinSupplierPays.Caption = If(Eng, "Supplier pays", "مدفوعات الموردين")
        btnBackPatPay.Text = If(Eng, "Back to overview", "العودة إلى النظرة العامة")
        btnBackSupPay.Text = If(Eng, "Back to overview", "العودة إلى النظرة العامة")
        lblTitlePatPay.Text = If(Eng, "Patient payments — detail", "مدفوعات المرضى — التفاصيل")
        lblTitleSupPay.Text = If(Eng, "Supplier payments — detail", "مدفوعات الموردين — التفاصيل")
    End Sub


    Private Sub MakeTransparentKpiText(ctrl As Control)
        Dim lc = TryCast(ctrl, LabelControl)
        If lc IsNot Nothing Then
            lc.Appearance.BackColor = Color.Transparent
            lc.Appearance.Options.UseBackColor = True
        End If
        For Each ch As Control In ctrl.Controls
            MakeTransparentKpiText(ch)
        Next
    End Sub

    Private Sub MakeKpiTotalLabels()
        lblKpiTreatTotal = CreateKpiTotalLabel(pnlKpiTreat, Color.FromArgb(22, 66, 55))
        lblKpiPatientPaysTotal = CreateKpiTotalLabel(pnlKpiPatientPays, Color.FromArgb(50, 120, 180))
        lblKpiExpTotal = CreateKpiTotalLabel(pnlKpiExp, Color.FromArgb(120, 55, 40))
        lblKpiPurchTotal = CreateKpiTotalLabel(pnlKpiPurch, Color.FromArgb(140, 90, 30))
        lblKpiSupplierPaysTotal = CreateKpiTotalLabel(pnlKpiSupplierPays, Color.FromArgb(160, 110, 50))
        lblKpiLabTotal = CreateKpiTotalLabel(pnlKpiLab, Color.FromArgb(45, 65, 120))
        lblKpiStaffTotal = CreateKpiTotalLabel(pnlKpiStaff, Color.FromArgb(75, 55, 110))
        MakeTransparentKpiText(pnlKpiTreat)
        MakeTransparentKpiText(pnlKpiPatientPays)
        MakeTransparentKpiText(pnlKpiExp)
        MakeTransparentKpiText(pnlKpiPurch)
        MakeTransparentKpiText(pnlKpiSupplierPays)
        MakeTransparentKpiText(pnlKpiLab)
        MakeTransparentKpiText(pnlKpiStaff)
    End Sub

    Private Function CreateKpiTotalLabel(parent As PanelControl, accent As Color) As LabelControl
        Dim l As New LabelControl With {
            .Text = If(Eng, "Total: —", "الإجمالي: —"),
            .AutoSizeMode = LabelAutoSizeMode.None,
            .Size = New Size(236, 20),
            .Location = New Point(16, 40)
        }
        l.Appearance.Font = UiFont
        l.Appearance.ForeColor = accent
        l.Appearance.Options.UseFont = True
        l.Appearance.Options.UseForeColor = True
        parent.Controls.Add(l)
        parent.Controls.SetChildIndex(l, 1)
        Return l
    End Function

    Private Sub LayoutKpiCards()
        Const cardH As Integer = 132
        Dim cards = {pnlKpiTreat, pnlKpiPatientPays, pnlKpiExp, pnlKpiPurch, pnlKpiSupplierPays, pnlKpiLab, pnlKpiStaff}
        For Each c In cards
            c.Height = cardH
        Next
        lblKpiTreatTitle.Location = New Point(16, 10)
        lblKpiPatientPaysTitle.Location = New Point(16, 10)
        lblKpiExpTitle.Location = New Point(16, 10)
        lblKpiPurchTitle.Location = New Point(16, 10)
        lblKpiSupplierPaysTitle.Location = New Point(16, 10)
        lblKpiLabTitle.Location = New Point(16, 10)
        lblKpiStaffTitle.Location = New Point(16, 10)
        lblKpiTreatHint.Location = New Point(16, 64)
        lblKpiPatientPaysHint.Location = New Point(16, 64)
        lblKpiExpHint.Location = New Point(16, 64)
        lblKpiPurchHint.Location = New Point(16, 64)
        lblKpiSupplierPaysHint.Location = New Point(16, 64)
        lblKpiLabHint.Location = New Point(16, 64)
        lblKpiStaffHint.Location = New Point(16, 64)
        lblKpiTreatGo.Location = New Point(16, 104)
        lblKpiPatientPaysGo.Location = New Point(16, 104)
        lblKpiExpGo.Location = New Point(16, 104)
        lblKpiPurchGo.Location = New Point(16, 104)
        lblKpiSupplierPaysGo.Location = New Point(16, 104)
        lblKpiLabGo.Location = New Point(16, 104)
        lblKpiStaffGo.Location = New Point(16, 104)
    End Sub

    Private Sub ApplyCalibriBold10Everywhere(root As Control)
        root.Font = UiFont
        Dim lc = TryCast(root, LabelControl)
        If lc IsNot Nothing Then
            lc.Appearance.Font = UiFont
            lc.Appearance.Options.UseFont = True
        End If
        Dim de = TryCast(root, DateEdit)
        If de IsNot Nothing Then
            de.Properties.Appearance.Font = UiFont
            de.Properties.Appearance.Options.UseFont = True
        End If
        Dim sb = TryCast(root, SimpleButton)
        If sb IsNot Nothing Then
            sb.Appearance.Font = UiFont
            sb.Appearance.Options.UseFont = True
        End If
        For Each ch As Control In root.Controls
            ApplyCalibriBold10Everywhere(ch)
        Next
        Dim gv = TryCast(root, GridControl)
        If gv IsNot Nothing Then
            Dim v = TryCast(gv.MainView, GridView)
            If v IsNot Nothing Then
                v.Appearance.Row.Font = UiFont
                v.Appearance.HeaderPanel.Font = UiFont
                v.Appearance.FooterPanel.Font = UiFont
                v.Appearance.Row.Options.UseFont = True
                v.Appearance.HeaderPanel.Options.UseFont = True
                v.Appearance.FooterPanel.Options.UseFont = True
            End If
        End If
    End Sub

    Private Sub WireKpiNavigation(rootCtrl As Control, target As NavigationPage)
        Dim go = Sub(s As Object, ev As EventArgs) navFinance.SelectedPage = target
        AddHandler rootCtrl.Click, go
        For Each ch As Control In rootCtrl.Controls
            WireKpiNavigation(ch, target)
        Next
    End Sub

    Private Sub btnFinanceRefresh_Click(sender As Object, e As EventArgs) Handles btnFinanceRefresh.Click
        RefreshAllFinanceData()
    End Sub

    Private Sub btnBackTreat_Click(sender As Object, e As EventArgs) Handles btnBackTreat.Click
        navFinance.SelectedPage = pageFinOverview
    End Sub

    Private Sub btnBackExp_Click(sender As Object, e As EventArgs) Handles btnBackExp.Click
        navFinance.SelectedPage = pageFinOverview
    End Sub

    Private Sub btnBackPurch_Click(sender As Object, e As EventArgs) Handles btnBackPurch.Click
        navFinance.SelectedPage = pageFinOverview
    End Sub

    Private Sub btnBackLab_Click(sender As Object, e As EventArgs) Handles btnBackLab.Click
        navFinance.SelectedPage = pageFinOverview
    End Sub

    Private Sub btnBackStaff_Click(sender As Object, e As EventArgs) Handles btnBackStaff.Click
        navFinance.SelectedPage = pageFinOverview
    End Sub

    Private Sub btnBackPatPay_Click(sender As Object, e As EventArgs) Handles btnBackPatPay.Click
        navFinance.SelectedPage = pageFinOverview
    End Sub

    Private Sub btnBackSupPay_Click(sender As Object, e As EventArgs) Handles btnBackSupPay.Click
        navFinance.SelectedPage = pageFinOverview
    End Sub

    Private Function GetDateFromSafe() As Date
        Dim v = dateFinanceFrom.EditValue
        If v Is Nothing OrElse Not TypeOf v Is Date Then Return Date.Today.AddMonths(-1)
        Return CDate(v).Date
    End Function

    Private Function GetDateToSafe() As Date
        Dim v = dateFinanceTo.EditValue
        If v Is Nothing OrElse Not TypeOf v Is Date Then Return Date.Today
        Return CDate(v).Date
    End Function

    ''' <summary>
    ''' Reloads header metrics, KPI card totals, and all detail grids for the selected period.
    ''' </summary>
    Public Sub RefreshAllFinanceData()
        ApplyFinanceToolbarAndKpiTexts()

        Dim snap = _financeLoader.Load(GetDateFromSafe(), GetDateToSafe())
        Dim income = snap.TreatmentIncome
        Dim patientPays = snap.PatientPaysTotal
        Dim expenses = snap.OperatingExpenses
        Dim purchases = snap.PurchasesInvoices
        Dim supplierPays = snap.SupplierPaymentsTotal
        Dim lab = snap.LabPayments
        Dim staff = snap.StaffPayments
        Dim outflows As Decimal = snap.TotalOutflows
        Dim net As Decimal = snap.NetPosition

        If lblFinanceWarnings IsNot Nothing Then
            If snap.Warnings IsNot Nothing AndAlso snap.Warnings.Count > 0 Then
                lblFinanceWarnings.Text = String.Join("  |  ", snap.Warnings)
                lblFinanceWarnings.Visible = True
            Else
                lblFinanceWarnings.Text = ""
                lblFinanceWarnings.Visible = False
            End If
        End If

        lblHeaderIncome.Text = income.ToString("N2")
        lblHeaderOutflows.Text = outflows.ToString("N2")
        lblHeaderNet.Text = net.ToString("N2")

        lblHeaderIncome.Appearance.ForeColor = Color.FromArgb(22, 66, 55)
        lblHeaderIncome.Appearance.Options.UseForeColor = True
        lblHeaderOutflows.Appearance.ForeColor = Color.FromArgb(120, 55, 40)
        lblHeaderOutflows.Appearance.Options.UseForeColor = True
        Dim netColor = If(net >= 0D, Color.FromArgb(30, 100, 70), Color.FromArgb(160, 50, 50))
        lblHeaderNet.Appearance.ForeColor = netColor
        lblHeaderNet.Appearance.Options.UseForeColor = True

        If lblKpiTreatTotal IsNot Nothing Then
            Dim tPrefix = If(Eng, "Total: ", "الإجمالي: ")
            lblKpiTreatTotal.Text = tPrefix & income.ToString("N2")
            lblKpiPatientPaysTotal.Text = tPrefix & patientPays.ToString("N2")
            lblKpiExpTotal.Text = tPrefix & expenses.ToString("N2")
            lblKpiPurchTotal.Text = tPrefix & purchases.ToString("N2")
            lblKpiSupplierPaysTotal.Text = tPrefix & supplierPays.ToString("N2")
            lblKpiLabTotal.Text = tPrefix & lab.ToString("N2")
            lblKpiStaffTotal.Text = tPrefix & staff.ToString("N2")
        End If

        gridFinTreatments.DataSource = snap.DtTreatments
        gridFinPatientPays.DataSource = snap.DtPatientPays
        gridFinExpenses.DataSource = snap.DtExpenses
        gridFinPurchases.DataSource = snap.DtPurchases
        gridFinSupplierPays.DataSource = snap.DtSupplierPays
        gridFinLab.DataSource = snap.DtLab
        gridFinStaff.DataSource = snap.DtStaff

        gridFinTreatments.RefreshDataSource()
        gridFinPatientPays.RefreshDataSource()
        gridFinExpenses.RefreshDataSource()
        gridFinPurchases.RefreshDataSource()
        gridFinSupplierPays.RefreshDataSource()
        gridFinLab.RefreshDataSource()
        gridFinStaff.RefreshDataSource()
        gridViewFinTreatments.LayoutChanged()
        gridViewFinPatientPays.LayoutChanged()
        gridViewFinExpenses.LayoutChanged()
        gridViewFinPurchases.LayoutChanged()
        gridViewFinSupplierPays.LayoutChanged()
        gridViewFinLab.LayoutChanged()
        gridViewFinStaff.LayoutChanged()

        gridViewFinTreatments.BestFitColumns()
        gridViewFinPatientPays.BestFitColumns()
        gridViewFinExpenses.BestFitColumns()
        gridViewFinPurchases.BestFitColumns()
        gridViewFinSupplierPays.BestFitColumns()
        gridViewFinLab.BestFitColumns()
        gridViewFinStaff.BestFitColumns()

        ApplyFinanceGridCaptions()
        ApplyFinanceGridFooterSummaries()

        pnlFinanceToolbar.Invalidate(True)
        navFinance.Invalidate(True)
    End Sub

    ''' <summary>
    ''' Backwards-compatible name — same as <see cref="RefreshAllFinanceData"/>.
    ''' </summary>
    Public Sub RefreshHeaderTotals()
        RefreshAllFinanceData()
    End Sub

    ''' <summary>
    ''' Column headers follow <see cref="Eng"/> (Module1): True = English, False = Arabic.
    ''' </summary>
    Private Sub ApplyFinanceGridCaptions()
        SetGridColCaption(gridViewFinTreatments, "Treatment", If(Eng, "Treatment", "العلاج"))
        SetGridColCaption(gridViewFinTreatments, "Category", If(Eng, "Category", "التصنيف"))
        SetGridColCaption(gridViewFinTreatments, "ServiceDate", If(Eng, "Service date", "تاريخ الخدمة"))
        SetGridColCaption(gridViewFinTreatments, "Amount", If(Eng, "Amount", "المبلغ"))
        SetGridColCaption(gridViewFinTreatments, "Patient", If(Eng, "Patient", "المريض"))

        SetGridColCaption(gridViewFinExpenses, "Date", If(Eng, "Date", "التاريخ"))
        SetGridColCaption(gridViewFinExpenses, "Category", If(Eng, "Category", "التصنيف"))
        SetGridColCaption(gridViewFinExpenses, "Description", If(Eng, "Description", "الوصف"))
        SetGridColCaption(gridViewFinExpenses, "Amount", If(Eng, "Amount", "المبلغ"))

        SetGridColCaption(gridViewFinPurchases, "InvoiceNo", If(Eng, "Invoice no.", "رقم الفاتورة"))
        SetGridColCaption(gridViewFinPurchases, "Date", If(Eng, "Date", "التاريخ"))
        SetGridColCaption(gridViewFinPurchases, "Supplier", If(Eng, "Supplier", "المورد"))
        SetGridColCaption(gridViewFinPurchases, "Amount", If(Eng, "Amount", "المبلغ"))
        SetGridColCaption(gridViewFinPurchases, "Status", If(Eng, "Status", "الحالة"))

        SetGridColCaption(gridViewFinLab, "Date", If(Eng, "Date", "التاريخ"))
        SetGridColCaption(gridViewFinLab, "Laboratory", If(Eng, "Laboratory", "المختبر"))
        SetGridColCaption(gridViewFinLab, "CaseRef", If(Eng, "Case / ref.", "مرجع الحالة"))
        SetGridColCaption(gridViewFinLab, "Amount", If(Eng, "Amount", "المبلغ"))
        SetGridColCaption(gridViewFinLab, "Paid", If(Eng, "Paid", "مدفوع"))

        SetGridColCaption(gridViewFinStaff, "PayPeriod", If(Eng, "Pay period", "فترة الدفع"))
        SetGridColCaption(gridViewFinStaff, "Employee", If(Eng, "Employee", "الموظف"))
        SetGridColCaption(gridViewFinStaff, "Role", If(Eng, "Role", "الدور"))
        SetGridColCaption(gridViewFinStaff, "Gross", If(Eng, "Gross", "الإجمالي"))
        SetGridColCaption(gridViewFinStaff, "NetPay", If(Eng, "Net pay", "صافي الدفع"))

        SetGridColCaption(gridViewFinPatientPays, "PayDate", If(Eng, "Pay date", "تاريخ الدفع"))
        SetGridColCaption(gridViewFinPatientPays, "Amount", If(Eng, "Amount", "المبلغ"))
        SetGridColCaption(gridViewFinPatientPays, "PayType", If(Eng, "Pay type", "نوع الدفع"))
        SetGridColCaption(gridViewFinPatientPays, "Patient", If(Eng, "Patient", "المريض"))
        SetGridColCaption(gridViewFinPatientPays, "Treatment", If(Eng, "Treatment", "العلاج"))
        SetGridColCaption(gridViewFinPatientPays, "Notes", If(Eng, "Notes", "ملاحظات"))

        SetGridColCaption(gridViewFinSupplierPays, "PaymentDate", If(Eng, "Pay date", "تاريخ الدفع"))
        SetGridColCaption(gridViewFinSupplierPays, "Supplier", If(Eng, "Supplier", "المورد"))
        SetGridColCaption(gridViewFinSupplierPays, "SupplierId", If(Eng, "Supplier ID", "رقم المورد"))
        Dim colSupId = gridViewFinSupplierPays.Columns.ColumnByFieldName("SupplierId")
        If colSupId IsNot Nothing Then colSupId.Visible = False
        SetGridColCaption(gridViewFinSupplierPays, "Amount", If(Eng, "Amount", "المبلغ"))
        SetGridColCaption(gridViewFinSupplierPays, "PaymentMethod", If(Eng, "Method", "طريقة الدفع"))
        SetGridColCaption(gridViewFinSupplierPays, "ChqNumber", If(Eng, "Cheque #", "رقم الشيك"))
        SetGridColCaption(gridViewFinSupplierPays, "ChqBank", If(Eng, "Cheque bank", "بنك الشيك"))
        SetGridColCaption(gridViewFinSupplierPays, "ChqDueDate", If(Eng, "Cheque due", "استحقاق الشيك"))
        SetGridColCaption(gridViewFinSupplierPays, "ChqOwner", If(Eng, "Cheque owner", "صاحب الشيك"))
        SetGridColCaption(gridViewFinSupplierPays, "AccountNumber", If(Eng, "Account #", "رقم الحساب"))
        SetGridColCaption(gridViewFinSupplierPays, "InvoiceId", If(Eng, "Invoice ID", "رقم الفاتورة"))
    End Sub

    Private Shared Sub SetGridColCaption(view As GridView, fieldName As String, caption As String)
        Dim col = view.Columns.ColumnByFieldName(fieldName)
        If col IsNot Nothing Then col.Caption = caption
    End Sub

    ''' <summary>
    ''' Footer sums for currency columns (columns are created when DataTable binds; summaries are reapplied after each refresh).
    ''' </summary>
    Private Sub ApplyFinanceGridFooterSummaries()
        ApplySumFooters(gridViewFinTreatments, "Amount")
        ApplySumFooters(gridViewFinPatientPays, "Amount")
        ApplySumFooters(gridViewFinExpenses, "Amount")
        ApplySumFooters(gridViewFinPurchases, "Amount")
        ApplySumFooters(gridViewFinSupplierPays, "Amount")
        ApplySumFooters(gridViewFinLab, "Amount")
        ApplySumFooters(gridViewFinStaff, "NetPay")
    End Sub

    Private Shared Sub ApplySumFooters(view As GridView, ParamArray numericFieldNames As String())
        If view Is Nothing OrElse numericFieldNames Is Nothing Then Return
        For Each fieldName In numericFieldNames
            If String.IsNullOrEmpty(fieldName) Then Continue For
            Dim col = view.Columns.ColumnByFieldName(fieldName)
            If col Is Nothing Then Continue For
            col.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
            col.DisplayFormat.FormatString = "n2"
            col.Summary.Clear()
            col.Summary.Add(New DevExpress.XtraGrid.GridColumnSummaryItem(SummaryItemType.Sum, fieldName, "{0:N2}"))
        Next
    End Sub

End Class
