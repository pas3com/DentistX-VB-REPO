Imports System.Drawing
Imports System.Text
Imports System.Windows.Forms
Imports DevExpress.Utils
Imports DevExpress.XtraEditors
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Columns
Imports DevExpress.XtraGrid.Views.Base
Imports DevExpress.XtraGrid.Views.Grid

''' <summary>Browse dbo.WhatsAppOutboundMessage — same UX pattern as <see cref="FrmWhatsAppActivityLog"/>.</summary>
Public Class FrmWhatsAppOutboundArchive
    Inherits XtraForm

    Private Shared ReadOnly UiFont As New Font("Calibri", 10.0F, FontStyle.Bold)

    Private ReadOnly _grid As New GridControl()
    Private ReadOnly _view As New GridView()
    Private _txtSearch As TextEdit
    Private _chkProblems As CheckEdit
    Private _lblPage As LabelControl
    Private _lblStorage As LabelControl
    Private _cboPageSize As ComboBoxEdit
    Private _topPanel As Panel
    Private _pageIndex As Integer
    Private _pageSize As Integer = 250
    Private _totalCount As Integer

    Public Property LayoutHost As IWin32Window

    Public Sub New()
        BuildUi()
    End Sub

    Private Sub BuildUi()
        Font = UiFont
        Text = If(Eng, "WhatsApp outbound queue (SQL)", "طابور إرسال واتساب (SQL)")
        StartPosition = FormStartPosition.Manual
        MinimumSize = New Size(480, 320)
        Icon = AppIcon

        _topPanel = New Panel With {.Dock = DockStyle.Top, .Height = 100, .Font = UiFont}

        Dim lblSearch As New LabelControl With {
            .Text = If(Eng, "Search (phone, category, id, clinic, text):", "بحث (هاتف، تصنيف، رقم، عيادة، نص):"),
            .Location = New Point(8, 6),
            .AutoSizeMode = LabelAutoSizeMode.Horizontal
        }
        ApplyLbl(lblSearch)

        _txtSearch = New TextEdit With {.Location = New Point(8, 24), .Size = New Size(320, 24)}
        ApplyTe(_txtSearch)

        Dim btnSearch As New SimpleButton With {.Text = If(Eng, "Search", "بحث"), .Location = New Point(336, 22), .Size = New Size(88, 26)}
        ApplyBtn(btnSearch)

        Dim btnRefresh As New SimpleButton With {.Text = If(Eng, "Refresh", "تحديث"), .Location = New Point(432, 22), .Size = New Size(88, 26)}
        ApplyBtn(btnRefresh)

        _chkProblems = New CheckEdit With {
            .Text = If(Eng, "Problems only (dead / cancelled)", "مشاكل فقط (ميت أو ملغى)"),
            .Location = New Point(528, 24),
            .Width = 260}
        ApplyChk(_chkProblems)

        Dim lblRows As New LabelControl With {
            .Text = If(Eng, "Page size:", "حجم الصفحة:"),
            .Location = New Point(800, 6),
            .AutoSizeMode = LabelAutoSizeMode.Horizontal}
        ApplyLbl(lblRows)

        _cboPageSize = New ComboBoxEdit With {.Location = New Point(800, 24), .Size = New Size(72, 24)}
        _cboPageSize.Properties.Items.AddRange(New Object() {100, 250, 500, 1000})
        _cboPageSize.EditValue = _pageSize
        ApplyCbo(_cboPageSize)

        Dim btnPrev As New SimpleButton With {.Text = "« " & If(Eng, "Prev", "السابق"), .Location = New Point(878, 22), .Size = New Size(88, 26)}
        ApplyBtn(btnPrev)

        Dim btnNext As New SimpleButton With {.Text = If(Eng, "Next", "التالي") & " »", .Location = New Point(972, 22), .Size = New Size(88, 26)}
        ApplyBtn(btnNext)

        _lblPage = New LabelControl With {.Text = "", .Location = New Point(8, 54), .Width = 1050, .AutoSizeMode = LabelAutoSizeMode.Vertical}
        _lblPage.Appearance.TextOptions.WordWrap = WordWrap.Wrap
        ApplyLbl(_lblPage)

        _lblStorage = New LabelControl With {
            .Text = If(Eng,
                "Stored in: SQL dbo.WhatsAppOutboundMessage (central outbox — reminders, snapshots, manual sends).",
                "التخزين: جدول SQL dbo.WhatsAppOutboundMessage (طابور موحّد: تذكيرات، صور مجدول، إرسال يدوي)."),
            .Location = New Point(8, 76),
            .Width = 1050,
            .AutoSizeMode = LabelAutoSizeMode.Vertical}
        _lblStorage.Appearance.TextOptions.WordWrap = WordWrap.Wrap
        _lblStorage.Appearance.ForeColor = Color.DimGray
        ApplyLbl(_lblStorage)

        AddHandler btnSearch.Click, Sub(s, e) RunSearchFromStart()
        AddHandler btnRefresh.Click, Sub(s, e) RunSearchFromStart()
        AddHandler _chkProblems.CheckedChanged, Sub(s, e) RunSearchFromStart()
        AddHandler btnPrev.Click, Sub(s, e) GoPrevPage()
        AddHandler btnNext.Click, Sub(s, e) GoNextPage()
        AddHandler _cboPageSize.EditValueChanged, Sub(s, e) OnPageSizeChanged()
        AddHandler _txtSearch.KeyDown, Sub(s As Object, e As KeyEventArgs)
                                           If e.KeyCode = Keys.Enter Then
                                               e.Handled = True
                                               RunSearchFromStart()
                                           End If
                                       End Sub

        _topPanel.Controls.AddRange({lblSearch, _txtSearch, btnSearch, btnRefresh, _chkProblems, lblRows, _cboPageSize, btnPrev, btnNext, _lblPage, _lblStorage})

        _grid.Dock = DockStyle.Fill
        _grid.Font = UiFont
        _grid.MainView = _view
        _grid.ViewCollection.AddRange(New BaseView() {_view})
        _view.OptionsBehavior.Editable = False
        _view.OptionsView.ShowGroupPanel = False
        AddHandler _view.DoubleClick, AddressOf View_DoubleClick
        ApplyGrd(_view)

        Controls.Add(_grid)
        Controls.Add(_topPanel)
    End Sub

    Private Shared Sub ApplyLbl(l As LabelControl)
        l.Appearance.Font = UiFont
        l.Appearance.Options.UseFont = True
    End Sub

    Private Shared Sub ApplyBtn(b As SimpleButton)
        b.Appearance.Font = UiFont
        b.Appearance.Options.UseFont = True
    End Sub

    Private Shared Sub ApplyTe(t As TextEdit)
        t.Properties.Appearance.Font = UiFont
        t.Properties.Appearance.Options.UseFont = True
    End Sub

    Private Shared Sub ApplyCbo(c As ComboBoxEdit)
        c.Properties.Appearance.Font = UiFont
        c.Properties.Appearance.Options.UseFont = True
    End Sub

    Private Shared Sub ApplyChk(ch As CheckEdit)
        ch.Properties.Appearance.Font = UiFont
        ch.Properties.Appearance.Options.UseFont = True
    End Sub

    Private Shared Sub ApplyGrd(v As GridView)
        v.Appearance.Row.Font = UiFont
        v.Appearance.Row.Options.UseFont = True
        v.Appearance.HeaderPanel.Font = UiFont
        v.Appearance.HeaderPanel.Options.UseFont = True
    End Sub

    Private Sub AlignToWorkspace()
        Dim hostForm = TryCast(LayoutHost, Form)
        If hostForm Is Nothing Then
            StartPosition = FormStartPosition.CenterScreen
            Size = New Size(1100, 700)
            Return
        End If

        StartPosition = FormStartPosition.Manual
        Dim r As Rectangle = Rectangle.Empty
        Dim mv3 = TryCast(hostForm, MainView3)
        If mv3 IsNot Nothing Then
            r = mv3.GetContainerAScreenBounds()
        Else
            Dim mv1 = TryCast(hostForm, MainView1)
            If mv1 IsNot Nothing Then
                r = mv1.GetPatientWorkspaceScreenBounds()
            End If
        End If

        If r.IsEmpty OrElse r.Width < 120 OrElse r.Height < 120 Then
            StartPosition = FormStartPosition.CenterScreen
            Dim wa = Screen.FromHandle(hostForm.Handle).WorkingArea
            Size = New Size(Math.Min(1100, wa.Width - 40), Math.Min(700, wa.Height - 40))
            Location = New Point(wa.Left + (wa.Width - Width) \ 2, wa.Top + (wa.Height - Height) \ 2)
            Return
        End If

        Bounds = r
    End Sub

    Private Sub OnPageSizeChanged()
        Dim v = _cboPageSize.EditValue
        Dim n As Integer
        If v IsNot Nothing AndAlso Integer.TryParse(v.ToString(), n) AndAlso n > 0 Then
            _pageSize = n
            RunSearchFromStart()
        End If
    End Sub

    Private Sub RunSearchFromStart()
        _pageIndex = 0
        LoadCurrentPage()
    End Sub

    Private Sub GoPrevPage()
        If _pageIndex <= 0 Then Return
        _pageIndex -= 1
        LoadCurrentPage()
    End Sub

    Private Sub GoNextPage()
        Dim maxPage = GetMaxPageIndex()
        If _pageIndex >= maxPage Then Return
        _pageIndex += 1
        LoadCurrentPage()
    End Sub

    Private Function GetMaxPageIndex() As Integer
        If _totalCount <= 0 Then Return 0
        Return Math.Max(0, CInt(Math.Ceiling(_totalCount / CDbl(_pageSize))) - 1)
    End Function

    Protected Overrides Sub OnLoad(e As EventArgs)
        MyBase.OnLoad(e)
        AlignToWorkspace()
        LoadCurrentPage()
    End Sub

    Private Sub LoadCurrentPage()
        Dim search = If(_txtSearch IsNot Nothing, _txtSearch.Text, "")
        Dim skip = _pageIndex * _pageSize
        Dim res = WhatsAppOutboundRepository.QueryOutboundArchivePage(skip, _pageSize, _chkProblems.Checked, search)
        _totalCount = res.TotalCount
        _grid.DataSource = res.Rows
        _view.PopulateColumns()
        ApplyOutboundGridLayout()

        Dim pageCount = Math.Max(1, CInt(Math.Ceiling(_totalCount / CDbl(Math.Max(1, _pageSize)))))
        Dim fromN = If(_totalCount = 0, 0, skip + 1)
        Dim toN = Math.Min(skip + _pageSize, _totalCount)
        If Not WhatsAppOutboundRepository.IsOutboundInfrastructureReady() Then
            _lblPage.Text = If(Eng, "Table dbo.WhatsAppOutboundMessage is not installed on this database.", "جدول dbo.WhatsAppOutboundMessage غير موجود في قاعدة البيانات.")
        Else
            _lblPage.Text = If(Eng,
                $"Rows {fromN}–{toN} of {_totalCount} · Page {_pageIndex + 1} / {pageCount}",
                $"الصفوف {fromN}–{toN} من {_totalCount} · صفحة {_pageIndex + 1} / {pageCount}")
        End If
    End Sub

    Private Sub ApplyOutboundGridLayout()
        ApplyGrd(_view)
        For Each col As GridColumn In _view.Columns
            col.AppearanceCell.Font = UiFont
            col.AppearanceCell.Options.UseFont = True
            col.AppearanceHeader.Font = UiFont
            col.AppearanceHeader.Options.UseFont = True

            Select Case col.FieldName
                Case NameOf(WhatsAppOutboundArchiveRow.ClinicId)
                    col.Caption = If(Eng, "ClinicId", "معرف العيادة")
                Case NameOf(WhatsAppOutboundArchiveRow.MessageId)
                    col.Caption = If(Eng, "ID", "الرقم")
                Case NameOf(WhatsAppOutboundArchiveRow.CreatedAtUtc)
                    col.Caption = If(Eng, "Created (UTC)", "إنشاء UTC")
                Case NameOf(WhatsAppOutboundArchiveRow.UpdatedAtUtc)
                    col.Caption = If(Eng, "Updated (UTC)", "تحديث UTC")
                Case NameOf(WhatsAppOutboundArchiveRow.Status)
                    col.Caption = If(Eng, "Status", "الحالة")
                Case NameOf(WhatsAppOutboundArchiveRow.MessageCategory)
                    col.Caption = If(Eng, "Category", "التصنيف")
                Case NameOf(WhatsAppOutboundArchiveRow.SourceHint)
                    col.Caption = If(Eng, "Source", "المصدر")
                Case NameOf(WhatsAppOutboundArchiveRow.TargetDigits)
                    col.Caption = If(Eng, "Phone", "الهاتف")
                Case NameOf(WhatsAppOutboundArchiveRow.BodyPreview)
                    col.Caption = If(Eng, "Message", "الرسالة")
                Case NameOf(WhatsAppOutboundArchiveRow.AttachmentPathPreview)
                    col.Caption = If(Eng, "Attach", "مرفق")
                Case NameOf(WhatsAppOutboundArchiveRow.PatientId)
                    col.Caption = If(Eng, "Pat#", "مريض")
                Case NameOf(WhatsAppOutboundArchiveRow.AppointmentId)
                    col.Caption = If(Eng, "Appt#", "موعد")
                Case NameOf(WhatsAppOutboundArchiveRow.Priority)
                    col.Caption = If(Eng, "Pri", "أولوية")
                Case NameOf(WhatsAppOutboundArchiveRow.LastErrorPreview)
                    col.Caption = If(Eng, "Last error", "آخر خطأ")
                Case NameOf(WhatsAppOutboundArchiveRow.CancelledBeforeSend)
                    col.Caption = If(Eng, "Cancl", "إلغاء")
                Case NameOf(WhatsAppOutboundArchiveRow.GatewayJobId)
                    col.Caption = If(Eng, "Gateway job", "مهمة بوابة")
                Case NameOf(WhatsAppOutboundArchiveRow.NextAttemptAtUtc)
                    col.Caption = If(Eng, "Next try (UTC)", "محاولة تالية UTC")
                Case NameOf(WhatsAppOutboundArchiveRow.AttemptCount)
                    col.Caption = If(Eng, "Tries", "محاولات")
                Case NameOf(WhatsAppOutboundArchiveRow.ExpiresAtUtc)
                    col.Caption = If(Eng, "Expires (UTC)", "انتهاء UTC")
            End Select
        Next
        _view.BestFitColumns()
    End Sub

    Private Sub View_DoubleClick(sender As Object, e As EventArgs)
        Dim row = TryCast(_view.GetRow(_view.FocusedRowHandle), WhatsAppOutboundArchiveRow)
        If row Is Nothing Then Return
        Dim sb As New StringBuilder()
        sb.AppendLine(If(Eng, "Outbound row (truncated preview in grid)", "صف طابور (معاينة مقطوعة في الجدول)"))
        sb.AppendLine(If(Eng, "MessageId: ", "MessageId: ") & row.MessageId.ToString())
        sb.AppendLine(If(Eng, "Status: ", "الحالة: ") & If(row.Status, ""))
        sb.AppendLine(If(Eng, "Category: ", "التصنيف: ") & If(row.MessageCategory, ""))
        sb.AppendLine(If(Eng, "Created (UTC): ", "إنشاء UTC: ") & row.CreatedAtUtc.ToString("yyyy-MM-dd HH:mm:ss"))
        sb.AppendLine(If(Eng, "Updated (UTC): ", "تحديث UTC: ") & row.UpdatedAtUtc.ToString("yyyy-MM-dd HH:mm:ss"))
        sb.AppendLine(If(Eng, "Next attempt (UTC): ", "المحاولة التالية UTC: ") & row.NextAttemptAtUtc.ToString("yyyy-MM-dd HH:mm:ss"))
        sb.AppendLine(If(Eng, "Attempts: ", "عدد المحاولات: ") & row.AttemptCount.ToString())
        If row.ExpiresAtUtc.HasValue Then sb.AppendLine(If(Eng, "Expires (UTC): ", "انتهاء UTC: ") & row.ExpiresAtUtc.Value.ToString("yyyy-MM-dd HH:mm:ss"))
        sb.AppendLine(If(Eng, "Cancelled before send: ", "ألغيت قبل الإرسال: ") & row.CancelledBeforeSend.ToString())
        sb.AppendLine(If(Eng, "ClinicId: ", "معرف العيادة: ") & If(row.ClinicId, ""))
        sb.AppendLine(If(Eng, "Phone: ", "الهاتف: ") & If(row.TargetDigits, ""))
        If row.PatientId.HasValue Then sb.AppendLine(If(Eng, "Patient ID: ", "المريض: ") & row.PatientId.Value.ToString())
        If row.AppointmentId.HasValue Then sb.AppendLine(If(Eng, "Appointment ID: ", "الموعد: ") & row.AppointmentId.Value.ToString())
        sb.AppendLine(If(Eng, "Source: ", "المصدر: ") & If(row.SourceHint, ""))
        sb.AppendLine(If(Eng, "Gateway job: ", "مهمة البوابة: ") & If(row.GatewayJobId, ""))
        sb.AppendLine()
        sb.AppendLine(If(Eng, "Message preview:", "معاينة الرسالة:"))
        sb.AppendLine(If(row.BodyPreview, ""))
        sb.AppendLine()
        sb.AppendLine(If(Eng, "Attachment preview:", "معاينة المرفق:"))
        sb.AppendLine(If(row.AttachmentPathPreview, ""))
        sb.AppendLine()
        sb.AppendLine(If(Eng, "Last error preview:", "معاينة آخر خطأ:"))
        sb.AppendLine(If(row.LastErrorPreview, ""))
        XtraMessageBox.Show(sb.ToString(), Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Public Shared Sub ShowArchive(owner As IWin32Window)
        Dim f As New FrmWhatsAppOutboundArchive()
        f.LayoutHost = owner
        If owner IsNot Nothing Then f.Show(owner) Else f.Show()
    End Sub
End Class
