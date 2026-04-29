Imports System.Drawing
Imports System.Linq
Imports System.Windows.Forms
Imports DevExpress.Utils
Imports DevExpress.XtraEditors
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Columns
Imports DevExpress.XtraGrid.Views.Base
Imports DevExpress.XtraGrid.Views.Grid

Public Class FrmWhatsAppActivityLog
    Inherits XtraForm

    Private Shared ReadOnly LogUiFont As New Font("Calibri", 10.0F, FontStyle.Bold)

    Private ReadOnly _grid As New GridControl()
    Private ReadOnly _view As New GridView()
    Private ReadOnly _highlightLogId As Long?
    Private _txtSearch As TextEdit
    Private _chkFail As CheckEdit
    Private _lblPage As LabelControl
    Private _lblStorage As LabelControl
    Private _cboPageSize As ComboBoxEdit
    Private _topPanel As Panel
    Private _pageIndex As Integer
    Private _pageSize As Integer = 250
    Private _totalCount As Integer
    Private _showingPinnedEntry As Boolean

    ''' <summary>When set (e.g. MainView1 / MainView3), the form sizes and positions over that shell’s workspace (ContainerA or ribbon/status area).</summary>
    Public Property LayoutHost As IWin32Window

    Public Sub New()
        Me.New(Nothing)
    End Sub

    Public Sub New(highlightLogId As Long?)
        _highlightLogId = highlightLogId
        BuildUi()
    End Sub

    Private Sub BuildUi()
        Font = LogUiFont
        Text = If(Eng, "WhatsApp message log", "سجل رسائل واتساب")
        StartPosition = FormStartPosition.Manual
        MinimumSize = New Size(480, 320)
        Icon = AppIcon

        _topPanel = New Panel With {.Dock = DockStyle.Top, .Height = 100, .Font = LogUiFont}

        Dim lblSearch As New LabelControl With {
            .Text = If(Eng, "Search (phone, patient, clinic, text):", "بحث (هاتف، مريض، عيادة، نص):"),
            .Location = New Point(8, 6),
            .AutoSizeMode = LabelAutoSizeMode.Horizontal
        }
        ApplyDeLabelFont(lblSearch)

        _txtSearch = New TextEdit With {.Location = New Point(8, 24), .Size = New Size(320, 24)}
        ApplyTextEditFont(_txtSearch)

        Dim btnSearch As New SimpleButton With {
            .Text = If(Eng, "Search", "بحث"),
            .Location = New Point(336, 22),
            .Size = New Size(88, 26)
        }
        ApplySimpleButtonFont(btnSearch)

        Dim btnRefresh As New SimpleButton With {
            .Text = If(Eng, "Refresh", "تحديث"),
            .Location = New Point(432, 22),
            .Size = New Size(88, 26)
        }
        ApplySimpleButtonFont(btnRefresh)

        _chkFail = New CheckEdit With {
            .Text = If(Eng, "Failures only", "فشل فقط"),
            .Location = New Point(528, 24),
            .Width = 150
        }
        ApplyCheckEditFont(_chkFail)

        Dim lblRows As New LabelControl With {
            .Text = If(Eng, "Page size:", "حجم الصفحة:"),
            .Location = New Point(690, 6),
            .AutoSizeMode = LabelAutoSizeMode.Horizontal
        }
        ApplyDeLabelFont(lblRows)

        _cboPageSize = New ComboBoxEdit With {.Location = New Point(690, 24), .Size = New Size(72, 24)}
        _cboPageSize.Properties.Items.AddRange(New Object() {100, 250, 500, 1000})
        _cboPageSize.EditValue = _pageSize
        ApplyComboFont(_cboPageSize)

        Dim btnPrev As New SimpleButton With {
            .Text = "« " & If(Eng, "Prev", "السابق"),
            .Location = New Point(770, 22),
            .Size = New Size(88, 26)
        }
        ApplySimpleButtonFont(btnPrev)

        Dim btnNext As New SimpleButton With {
            .Text = If(Eng, "Next", "التالي") & " »",
            .Location = New Point(864, 22),
            .Size = New Size(88, 26)
        }
        ApplySimpleButtonFont(btnNext)

        _lblPage = New LabelControl With {
            .Text = "",
            .Location = New Point(8, 54),
            .Width = 1050,
            .AutoSizeMode = LabelAutoSizeMode.Vertical
        }
        _lblPage.Appearance.TextOptions.WordWrap = WordWrap.Wrap
        ApplyDeLabelFont(_lblPage)

        _lblStorage = New LabelControl With {
            .Text = If(Eng,
                "Stored in: SQL Server table dbo.WhatsAppMessageLog (your DentistX database — same connection as the app).",
                "التخزين: جدول dbo.WhatsAppMessageLog في قاعدة بيانات SQL Server (نفس اتصال برنامج DentistX)."),
            .Location = New Point(8, 76),
            .Width = 1050,
            .AutoSizeMode = LabelAutoSizeMode.Vertical
        }
        _lblStorage.Appearance.TextOptions.WordWrap = WordWrap.Wrap
        _lblStorage.Appearance.ForeColor = Color.DimGray
        ApplyDeLabelFont(_lblStorage)

        AddHandler btnSearch.Click, Sub(s, e) RunSearchFromStart()
        AddHandler btnRefresh.Click, Sub(s, e) RefreshFullGrid()
        AddHandler _chkFail.CheckedChanged, Sub(s, e) If Not _showingPinnedEntry Then RunSearchFromStart()
        AddHandler btnPrev.Click, Sub(s, e) GoPrevPage()
        AddHandler btnNext.Click, Sub(s, e) GoNextPage()
        AddHandler _cboPageSize.EditValueChanged, Sub(s, e) OnPageSizeChanged()
        AddHandler _txtSearch.KeyDown, Sub(s As Object, e As KeyEventArgs)
                                           If e.KeyCode = Keys.Enter Then
                                               e.Handled = True
                                               RunSearchFromStart()
                                           End If
                                       End Sub

        _topPanel.Controls.AddRange({lblSearch, _txtSearch, btnSearch, btnRefresh, _chkFail, lblRows, _cboPageSize, btnPrev, btnNext, _lblPage, _lblStorage})

        _grid.Dock = DockStyle.Fill
        _grid.Font = LogUiFont
        _grid.MainView = _view
        _grid.ViewCollection.AddRange(New BaseView() {_view})
        _view.OptionsBehavior.Editable = False
        _view.OptionsView.ShowGroupPanel = False
        AddHandler _view.DoubleClick, AddressOf View_DoubleClick

        ApplyGridFonts(_view)

        Controls.Add(_grid)
        Controls.Add(_topPanel)
    End Sub

    Private Shared Sub ApplyDeLabelFont(lbl As LabelControl)
        lbl.Appearance.Font = LogUiFont
        lbl.Appearance.Options.UseFont = True
    End Sub

    Private Shared Sub ApplySimpleButtonFont(btn As SimpleButton)
        btn.Appearance.Font = LogUiFont
        btn.Appearance.Options.UseFont = True
    End Sub

    Private Shared Sub ApplyTextEditFont(te As TextEdit)
        te.Properties.Appearance.Font = LogUiFont
        te.Properties.Appearance.Options.UseFont = True
    End Sub

    Private Shared Sub ApplyComboFont(cb As ComboBoxEdit)
        cb.Properties.Appearance.Font = LogUiFont
        cb.Properties.Appearance.Options.UseFont = True
    End Sub

    Private Shared Sub ApplyCheckEditFont(chk As CheckEdit)
        chk.Properties.Appearance.Font = LogUiFont
        chk.Properties.Appearance.Options.UseFont = True
    End Sub

    Private Shared Sub ApplyGridFonts(v As GridView)
        v.Appearance.Row.Font = LogUiFont
        v.Appearance.Row.Options.UseFont = True
        v.Appearance.HeaderPanel.Font = LogUiFont
        v.Appearance.HeaderPanel.Options.UseFont = True
        v.Appearance.FooterPanel.Font = LogUiFont
        v.Appearance.FooterPanel.Options.UseFont = True
        v.Appearance.GroupRow.Font = LogUiFont
        v.Appearance.GroupRow.Options.UseFont = True
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
            If Not _showingPinnedEntry Then RunSearchFromStart()
        End If
    End Sub

    Private Sub RunSearchFromStart()
        _showingPinnedEntry = False
        _pageIndex = 0
        LoadCurrentPage()
    End Sub

    Private Sub RefreshFullGrid()
        _showingPinnedEntry = False
        _pageIndex = 0
        LoadCurrentPage()
    End Sub

    Private Sub GoPrevPage()
        If _showingPinnedEntry Then Return
        If _pageIndex <= 0 Then Return
        _pageIndex -= 1
        LoadCurrentPage()
    End Sub

    Private Sub GoNextPage()
        If _showingPinnedEntry Then Return
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

        If _highlightLogId.HasValue Then
            Dim one = WhatsAppActivityLogRepository.GetById(_highlightLogId.Value)
            If one IsNot Nothing Then
                _showingPinnedEntry = True
                _grid.DataSource = New List(Of WhatsAppActivityLogRow) From {one}
                _view.PopulateColumns()
                ApplyLogGridLayout()
                _lblPage.Text = If(Eng,
                    "Showing the notification entry. Click Refresh to browse and search the full log.",
                    "عرض السجل المرتبط بالإشعار. اضغط تحديث لتصفح البحث في السجل الكامل.")
                _view.FocusedRowHandle = 0
                Return
            End If
        End If
        LoadCurrentPage()
    End Sub

    Private Sub LoadCurrentPage()
        Dim search = If(_txtSearch IsNot Nothing, _txtSearch.Text, "")
        Dim skip = _pageIndex * _pageSize
        Dim res = WhatsAppActivityLogRepository.QueryPage(skip, _pageSize, _chkFail.Checked, search)
        _totalCount = res.TotalCount
        _grid.DataSource = res.Rows
        _view.PopulateColumns()
        ApplyLogGridLayout()

        Dim pageCount = Math.Max(1, CInt(Math.Ceiling(_totalCount / CDbl(Math.Max(1, _pageSize)))))
        Dim fromN = If(_totalCount = 0, 0, skip + 1)
        Dim toN = Math.Min(skip + _pageSize, _totalCount)
        _lblPage.Text = If(Eng,
            $"Rows {fromN}–{toN} of {_totalCount} · Page {_pageIndex + 1} / {pageCount}",
            $"الصفوف {fromN}–{toN} من {_totalCount} · صفحة {_pageIndex + 1} / {pageCount}")

        If _highlightLogId.HasValue AndAlso Not _showingPinnedEntry Then
            For i As Integer = 0 To _view.RowCount - 1
                Dim h = _view.GetRow(i)
                Dim row = TryCast(h, WhatsAppActivityLogRow)
                If row IsNot Nothing AndAlso row.LogId = _highlightLogId.Value Then
                    _view.FocusedRowHandle = i
                    _view.MakeRowVisible(i)
                    Exit For
                End If
            Next
        End If
    End Sub

    Private Sub ApplyLogGridLayout()
        ApplyGridFonts(_view)
        For Each col As GridColumn In _view.Columns
            col.AppearanceCell.Font = LogUiFont
            col.AppearanceCell.Options.UseFont = True
            col.AppearanceHeader.Font = LogUiFont
            col.AppearanceHeader.Options.UseFont = True

            Select Case col.FieldName
                Case "PatientId", "ClinicId"
                    col.Visible = False
                Case "PatientDisplayName"
                    col.Caption = If(Eng, "Patient", "المريض")
                Case "ClinicDisplayName"
                    col.Caption = If(Eng, "Clinic", "العيادة")
                Case "LogId"
                    col.Caption = If(Eng, "ID", "الرقم")
                Case "SentAtUtc"
                    col.Caption = If(Eng, "Time (UTC)", "الوقت UTC")
                Case "Success"
                    col.Caption = If(Eng, "OK", "نجاح")
                Case "Category"
                    col.Caption = If(Eng, "Category", "التصنيف")
                Case "SourceHint"
                    col.Caption = If(Eng, "Source", "المصدر")
                Case "TargetNumber"
                    col.Caption = If(Eng, "Phone", "الهاتف")
                Case "MessagePreview"
                    col.Caption = If(Eng, "Message", "الرسالة")
                Case "HasAttachment"
                    col.Caption = If(Eng, "Attach", "مرفق")
                Case "ResponseOrError"
                    col.Caption = If(Eng, "Response / error", "الاستجابة")
            End Select
        Next
        _view.BestFitColumns()
    End Sub

    Private Sub View_DoubleClick(sender As Object, e As EventArgs)
        Dim row = TryCast(_view.GetRow(_view.FocusedRowHandle), WhatsAppActivityLogRow)
        If row Is Nothing Then Return
        ShowRowDetail(row)
    End Sub

    Private Shared Sub ShowRowDetail(row As WhatsAppActivityLogRow)
        Dim sb As New System.Text.StringBuilder()
        sb.AppendLine(If(Eng, "Time (UTC): ", "الوقت UTC: ") & row.SentAtUtc.ToString("yyyy-MM-dd HH:mm:ss"))
        sb.AppendLine(If(Eng, "Success: ", "نجح: ") & row.Success.ToString())
        sb.AppendLine(If(Eng, "Category: ", "التصنيف: ") & If(row.Category, ""))
        sb.AppendLine(If(Eng, "Source: ", "المصدر: ") & If(row.SourceHint, ""))
        sb.AppendLine(If(Eng, "Number: ", "الرقم: ") & If(row.TargetNumber, ""))
        sb.AppendLine(If(Eng, "Patient: ", "المريض: ") & If(row.PatientDisplayName, ""))
        If row.PatientId.HasValue Then sb.AppendLine(If(Eng, "Patient ID: ", "رقم المريض: ") & row.PatientId.Value.ToString())
        sb.AppendLine(If(Eng, "Clinic: ", "العيادة: ") & If(row.ClinicDisplayName, ""))
        If Not String.IsNullOrWhiteSpace(row.ClinicId) Then sb.AppendLine(If(Eng, "Clinic ID: ", "معرّف العيادة: ") & row.ClinicId)
        sb.AppendLine(If(Eng, "Log storage: ", "مكان التخزين: ") & If(Eng, "SQL dbo.WhatsAppMessageLog", "جدول SQL dbo.WhatsAppMessageLog"))
        sb.AppendLine(If(Eng, "Attachment: ", "مرفق: ") & row.HasAttachment.ToString())
        sb.AppendLine()
        sb.AppendLine(If(Eng, "Message preview:", "معاينة الرسالة:"))
        sb.AppendLine(If(row.MessagePreview, ""))
        sb.AppendLine()
        sb.AppendLine(If(Eng, "Response / error:", "الاستجابة أو الخطأ:"))
        sb.AppendLine(If(row.ResponseOrError, ""))
        XtraMessageBox.Show(sb.ToString(), If(Eng, "WhatsApp log entry", "سجل واتساب"), MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Public Shared Sub ShowArchive(owner As IWin32Window)
        Dim f As New FrmWhatsAppActivityLog(Nothing)
        f.LayoutHost = owner
        If owner IsNot Nothing Then f.Show(owner) Else f.Show()
    End Sub

    Public Shared Sub ShowForLogEntry(owner As IWin32Window, logId As Long)
        Dim f As New FrmWhatsAppActivityLog(logId)
        f.LayoutHost = owner
        If owner IsNot Nothing Then f.Show(owner) Else f.Show()
        f.Activate()
    End Sub
End Class
