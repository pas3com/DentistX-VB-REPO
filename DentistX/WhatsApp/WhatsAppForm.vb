Imports System.Drawing
Imports System.IO
Imports System.Linq
Imports System.Text
Imports System.Threading
Imports System.Threading.Tasks
Imports System.ComponentModel
Imports System.Windows.Forms
Imports DevExpress.XtraEditors
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Columns
Imports DevExpress.XtraGrid.Views.Base
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraTab

Public Class WhatsAppForm
    Private _service As WhatsAppService
    Private _statusTimer As System.Windows.Forms.Timer
    Private _queueRefreshTimer As System.Windows.Forms.Timer
    Private _queueRefreshInFlight As Boolean
    Private _clinicIdValue As String = ""
    Private _clinicDisplayName As String = ""
    Private _queueBinding As BindingList(Of PendingMessageItem)
    Private _failedBinding As BindingList(Of FailedMessageItem)
    Private _archiveBinding As BindingList(Of WhatsAppArchiveItem)
    Private _tabArchive As XtraTabPage
    Private _gridArchive As GridControl
    Private _viewArchive As GridView
    Private _lblArchiveFilter As LabelControl
    Private _txtArchiveNumberFilter As TextEdit
    ''' <summary>Digit-only substring for client-side archive grid filter (matches any row whose number contains these digits).</summary>
    Private _archiveNumberFilterDigits As String = ""
    Private _btnArchiveRefresh As SimpleButton
    Private _btnArchiveRetryFailed As SimpleButton
    Private _repoQueueDelete As DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit
    Private _btnOutboundPending As SimpleButton

    ''' <summary>
    ''' Clinic ID (Guid from Clinic table) used for QR and status API calls.
    ''' Loaded from Clinic table on form load if not set.
    ''' </summary>
    Public Property ClinicId As String
        Get
            Return _clinicIdValue
        End Get
        Set(value As String)
            _clinicIdValue = If(value, "").Trim()
        End Set
    End Property

    Public Sub New()
        InitializeComponent()
        _service = New WhatsAppService()
        _statusTimer = New System.Windows.Forms.Timer()
        _statusTimer.Interval = 5000
        AddHandler _statusTimer.Tick, AddressOf StatusTimer_Tick
        _queueRefreshTimer = New System.Windows.Forms.Timer With {.Interval = 5000}
        AddHandler _queueRefreshTimer.Tick, AddressOf QueueRefreshTimer_Tick
        _queueBinding = New BindingList(Of PendingMessageItem)
        _failedBinding = New BindingList(Of FailedMessageItem)
        _archiveBinding = New BindingList(Of WhatsAppArchiveItem)
        GridQueue.DataSource = _queueBinding
        GridFailed.DataSource = _failedBinding
        ConfigureQueueColumns()
        ConfigureFailedColumns()
        BuildArchiveTab()
        AddHandler TabControl.SelectedPageChanged, AddressOf TabControl_SelectedPageChanged
        AddHandler BtnRefreshQueue.Click, AddressOf BtnRefreshQueue_Click
        AddHandler BtnDeleteFromQueue.Click, AddressOf BtnDeleteFromQueue_Click
        AddHandler BtnRefreshFailed.Click, AddressOf BtnRefreshFailed_Click
        AddHandler BtnRetryFailed.Click, AddressOf BtnRetryFailed_Click
        AddHandler BtnSendMessage.Click, AddressOf BtnSendMessage_Click
        AddHandler TxtSendFile.Properties.ButtonClick, AddressOf TxtSendFile_ButtonClick
        AddHandler BtnDisconnect.Click, AddressOf BtnDisconnect_Click
        AddHandler btnRefresh.Click, AddressOf BtnRefresh_Click
        AttachOutboundPendingButton()
    End Sub

    Private Sub BuildArchiveTab()
        _tabArchive = New XtraTabPage() With {
            .Name = "TabArchive",
            .Text = If(Eng, "Server archive", "أرشيف الخادم"),
                        .Padding = New Padding(0)}

        Dim bottom As New DevExpress.XtraEditors.PanelControl With {
            .Dock = DockStyle.Bottom,
            .Height = 44,
            .BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder}

        _lblArchiveFilter = New LabelControl With {
            .Location = New Point(8, 12),
            .AutoSizeMode = LabelAutoSizeMode.Horizontal,
            .Font = New Font("Calibri", 10, FontStyle.Bold),
            .Text = If(Eng, "Number filter (optional):", "تصفية الرقم (اختياري):")}
        _txtArchiveNumberFilter = New TextEdit With {.Location = New Point(168, 8), .Size = New Size(220, 24)}
        _btnArchiveRefresh = New SimpleButton With {
            .Text = If(Eng, "Refresh", "تحديث"),
            .Location = New Point(396, 6),
             .Font = New Font("Calibri", 10, FontStyle.Bold),
            .Size = New Size(96, 28)}
        _btnArchiveRetryFailed = New SimpleButton With {
            .Text = If(Eng, "Retry all failed", "إعادة كل الفاشلة"),
            .Location = New Point(498, 6),
             .Font = New Font("Calibri", 10, FontStyle.Bold),
            .Size = New Size(180, 28)}
        bottom.Controls.AddRange({_lblArchiveFilter, _txtArchiveNumberFilter, _btnArchiveRefresh, _btnArchiveRetryFailed})

        _gridArchive = New GridControl With {.Dock = DockStyle.Fill}
        _viewArchive = New GridView(_gridArchive)
        _gridArchive.MainView = _viewArchive
        _gridArchive.ViewCollection.Add(_viewArchive)
        _viewArchive.OptionsBehavior.Editable = False
        _viewArchive.OptionsView.ShowGroupPanel = False
        _viewArchive.Appearance.HeaderPanel.Font = New Font("Calibri", 10, FontStyle.Bold)
        _viewArchive.Appearance.Row.Font = New Font("Calibri", 10, FontStyle.Bold)
        _gridArchive.DataSource = _archiveBinding
        ConfigureArchiveColumns()


        _tabArchive.Controls.Add(_gridArchive)
        _tabArchive.Controls.Add(bottom)
        TabControl.TabPages.Insert(3, _tabArchive)
        AddHandler _btnArchiveRefresh.Click, AddressOf BtnArchiveRefresh_Click
        AddHandler _btnArchiveRetryFailed.Click, AddressOf BtnArchiveRetryFailed_Click
        AddHandler _viewArchive.DoubleClick, AddressOf ViewArchive_DoubleClick
        AddHandler _viewArchive.CustomRowFilter, AddressOf ViewArchive_CustomRowFilter
        AddHandler _txtArchiveNumberFilter.TextChanged, AddressOf TxtArchiveNumberFilter_TextChanged
        AddHandler _txtArchiveNumberFilter.KeyDown, Async Sub(s As Object, e As KeyEventArgs)
                                                        If e.KeyCode = Keys.Enter Then
                                                            e.Handled = True
                                                            Await LoadArchiveAsync()
                                                        End If
                                                    End Sub
    End Sub

    Private Shared Function ArchiveNumberDigitsOnly(s As String) As String
        If String.IsNullOrEmpty(s) Then Return ""
        Dim sb As New StringBuilder(s.Length)
        For Each ch In s
            If Char.IsDigit(ch) Then sb.Append(ch)
        Next
        Return sb.ToString()
    End Function

    Private Sub SyncArchiveNumberFilterFromText()
        _archiveNumberFilterDigits = ArchiveNumberDigitsOnly(If(_txtArchiveNumberFilter IsNot Nothing, _txtArchiveNumberFilter.Text, ""))
    End Sub

    Private Sub TxtArchiveNumberFilter_TextChanged(sender As Object, e As EventArgs)
        SyncArchiveNumberFilterFromText()
        If _viewArchive IsNot Nothing Then _viewArchive.RefreshData()
    End Sub

    Private Sub ViewArchive_CustomRowFilter(sender As Object, e As RowFilterEventArgs)
        If String.IsNullOrEmpty(_archiveNumberFilterDigits) Then
            e.Handled = False
            Return
        End If
        If e.ListSourceRow < 0 Then
            e.Visible = True
            e.Handled = True
            Return
        End If
        Dim view = TryCast(sender, GridView)
        If view Is Nothing Then
            e.Handled = False
            Return
        End If
        Dim row = TryCast(view.GetRow(e.ListSourceRow), WhatsAppArchiveItem)
        If row Is Nothing Then
            e.Visible = False
            e.Handled = True
            Return
        End If
        Dim hay = ArchiveNumberDigitsOnly(If(row.TargetNumber, ""))
        e.Visible = hay.IndexOf(_archiveNumberFilterDigits, StringComparison.Ordinal) >= 0
        e.Handled = True
    End Sub

    Private Sub ConfigureArchiveColumns()
        _viewArchive.Columns.Clear()
        Dim mk = Sub(field As String, caption As String)
                     _viewArchive.Columns.Add(New GridColumn With {.FieldName = field, .Caption = caption, .Visible = True})
                 End Sub
        If Eng Then
            mk(NameOf(WhatsAppArchiveItem.CreatedAtUtc), "Created (UTC)")
            mk(NameOf(WhatsAppArchiveItem.LastUpdatedAtUtc), "Updated (UTC)")
            mk(NameOf(WhatsAppArchiveItem.Status), "Status")
            mk(NameOf(WhatsAppArchiveItem.TargetNumber), "Number")
            mk(NameOf(WhatsAppArchiveItem.HasAttachment), "Attachment")
            mk(NameOf(WhatsAppArchiveItem.Id), "Id")
            mk(NameOf(WhatsAppArchiveItem.MessageText), "Message")
            mk(NameOf(WhatsAppArchiveItem.ErrorMessage), "Error")
        Else
            mk(NameOf(WhatsAppArchiveItem.CreatedAtUtc), "وقت الإنشاء UTC")
            mk(NameOf(WhatsAppArchiveItem.LastUpdatedAtUtc), "آخر تحديث UTC")
            mk(NameOf(WhatsAppArchiveItem.Status), "الحالة")
            mk(NameOf(WhatsAppArchiveItem.TargetNumber), "الرقم")
            mk(NameOf(WhatsAppArchiveItem.HasAttachment), "مرفق")
            mk(NameOf(WhatsAppArchiveItem.Id), "المعرف")
            mk(NameOf(WhatsAppArchiveItem.MessageText), "الرسالة")
            mk(NameOf(WhatsAppArchiveItem.ErrorMessage), "الخطأ")
        End If
        _viewArchive.BestFitColumns()
    End Sub

    Private Sub AttachOutboundPendingButton()
        _btnOutboundPending = New SimpleButton With {
            .Text = If(Eng, "Pending (local DB)…", "معلّقة (محلي)…")}
        _btnOutboundPending.Size = BtnSendMessage.Size
        Dim p = BtnSendMessage.Location
        _btnOutboundPending.Location = New Point(Math.Max(8, p.X - _btnOutboundPending.Width - 10), p.Y)
        AddHandler _btnOutboundPending.Click, AddressOf BtnOutboundPendingLocal_Click
        TabSend.Controls.Add(_btnOutboundPending)
    End Sub

    Private Sub BtnOutboundPendingLocal_Click(sender As Object, e As EventArgs)
        Dim c = ClinicId.Trim()
        If String.IsNullOrWhiteSpace(c) Then c = WhatsAppService.GetCurrentClinicId()
        Using frm As New FrmWhatsOutboundPending(c)
            frm.ShowDialog(Me)
        End Using
    End Sub

    Private Async Sub BtnRefresh_Click(sender As Object, e As EventArgs)
        If String.IsNullOrWhiteSpace(ClinicId) Then Return
        btnRefresh.Enabled = False
        Try
            Dim connected = Await _service.GetConnectionStatusAsync(ClinicId, bypassStatusThrottle:=True)
            If connected Then
                _statusTimer.Stop()
                PanelQr.Visible = False
                PanelSuccess.Visible = True
                UpdateStatusLabel(FormatClinicWithStatus("متصل ✓"))
            Else
                PanelSuccess.Visible = False
                PanelQr.Visible = True
                UpdateStatusLabel(FormatClinicWithStatus("غير متصل. امسح رمز QR للاتصال."))
                Await RefreshQrAndStatusAsync()
            End If
        Catch ex As Exception
            UpdateStatusLabel("خطأ في التحقق: " & ex.Message)
        Finally
            btnRefresh.Enabled = True
        End Try
    End Sub

    Private Async Sub BtnDisconnect_Click(sender As Object, e As EventArgs)
        If String.IsNullOrWhiteSpace(ClinicId) Then Return
        _statusTimer.Stop()
        BtnDisconnect.Enabled = False
        Try
            Dim ok = Await _service.DisconnectAsync(ClinicId)
            If ok Then
                PanelSuccess.Visible = False
                PanelQr.Visible = True
                UpdateStatusLabel(FormatClinicWithStatus("تم قطع الاتصال. امسح QR للاتصال من جديد."))
                PictureQr.Image = Nothing
            Else
                UpdateStatusLabel(FormatClinicWithStatus("فشل قطع الاتصال."))
            End If
        Catch ex As Exception
            UpdateStatusLabel("خطأ: " & ex.Message)
        Finally
            BtnDisconnect.Enabled = True
        End Try
    End Sub

    Private Sub TxtSendFile_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs)
        Using ofd As New OpenFileDialog()
            ofd.Filter = "PDF|*.pdf|Images|*.png;*.jpg;*.jpeg|All|*.*"
            If ofd.ShowDialog(Me) = DialogResult.OK Then TxtSendFile.Text = ofd.FileName
        End Using
    End Sub

    Private Async Sub BtnSendMessage_Click(sender As Object, e As EventArgs)
        If String.IsNullOrWhiteSpace(ClinicId) Then
            MessageBox.Show("لا توجد عيادة محددة.", "إرسال", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If
        Dim number As String = If(TxtSendNumber.Text, "").Trim()
        If String.IsNullOrWhiteSpace(number) Then
            MessageBox.Show("أدخل رقم الجوال.", "إرسال", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            TxtSendNumber.Focus()
            Return
        End If
        Dim message As String = If(TxtSendMessage.Text, "").Trim()
        Dim filePath As String = If(TxtSendFile.Text, "").Trim()
        If Not String.IsNullOrWhiteSpace(filePath) AndAlso Not IO.File.Exists(filePath) Then
            MessageBox.Show("الملف المحدد غير موجود.", "إرسال", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If
        BtnSendMessage.Enabled = False
        Try
            Dim connected = Await _service.GetConnectionStatusAsync(ClinicId, bypassStatusThrottle:=True)
            If Not connected Then
                connected = Await WhatsAppService.TryEnsureWhatsConnectedSilentAsync(Me, ClinicId).ConfigureAwait(True)
            End If
            If Not connected Then
                TabControl.SelectedTabPage = TabConnection
                PanelSuccess.Visible = False
                PanelQr.Visible = True
                UpdateStatusLabel(FormatClinicWithStatus("غير متصل. امسح رمز QR للاتصال."))
                Await RefreshQrAndStatusAsync()
                _statusTimer.Start()
                MessageBox.Show(
                    If(Eng, "WhatsApp is not connected. Scan the QR code on the Connection tab, then try sending again.",
                       "واتساب غير متصل. امسح رمز الاستجابة في تبويب الاتصال ثم أعد الإرسال."),
                    If(Eng, "WhatsApp", "واتساب"), MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return
            End If

            Dim ctx As New WhatsAppSendContext With {
                .Category = WhatsAppMessageCategories.ManualSend,
                .SourceHint = NameOf(WhatsAppForm),
                .RevealMessageCenter = True
            }
            Dim resp = Await _service.SendMessageAsync(ClinicId, number, message, filePath, ctx)

            Dim intr As WhatsAppOutboundSendInterpretation = Nothing
            If WhatsAppService.TryInterpretOutboundSendResponse(resp, intr) AndAlso intr.HadLocalOutboxSemantics Then
                If Not String.IsNullOrWhiteSpace(intr.TerminalPriorStatus) Then
                    MessageBox.Show(
                        If(Eng,
                           "This outbound send was skipped because an identical logical send already completed in the database.",
                           "تم تخطي الإرسال لأنه سبق وأن اكتمل نفس المعرف المنطقي في الطابُر المحلي."),
                        If(Eng, "WhatsApp", "واتساب"), MessageBoxButtons.OK, MessageBoxIcon.Information)
                Else
                    MessageBox.Show(
                        If(Eng,
                           "Queued locally. It sends in the background when WhatsApp connects. Use Pending (local DB) to cancel before send.",
                           "تم الطابَر محليًا. سيُرسَل في الخلفية عند اتصال واتساب. استخدم زر المعقّبة المحلي لإلغاء الإرسال قبل التنفيذ."),
                        If(Eng, "WhatsApp", "واتساب"), MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If
            Else
                MessageBox.Show("تم وضع الرسالة في الطابور.", "إرسال", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
            TxtSendMessage.Text = ""
            TxtSendFile.Text = ""
        Catch ex As Exception
            MessageBox.Show(ex.Message, "خطأ في الإرسال", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            BtnSendMessage.Enabled = True
        End Try
    End Sub

    Private Sub ConfigureQueueColumns()
        ViewQueue.Columns.Clear()
        ' Delete button column (unbound)
        If _repoQueueDelete Is Nothing Then
            _repoQueueDelete = New DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit()
            _repoQueueDelete.Buttons(0).Caption = "حذف"
            _repoQueueDelete.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor
            GridQueue.RepositoryItems.Add(_repoQueueDelete)
            AddHandler _repoQueueDelete.ButtonClick, AddressOf ViewQueue_DeleteButtonClick
        End If
        Dim colDelete As New DevExpress.XtraGrid.Columns.GridColumn() With {
            .Caption = "حذف",
            .ColumnEdit = _repoQueueDelete,
            .Visible = True,
            .UnboundType = DevExpress.Data.UnboundColumnType.String
        }
        Dim colId As New DevExpress.XtraGrid.Columns.GridColumn() With {.FieldName = "JobId", .Caption = "المعرف", .Visible = True}
        Dim colNum As New DevExpress.XtraGrid.Columns.GridColumn() With {.FieldName = "TargetNumber", .Caption = "الرقم", .Visible = True}
        Dim colMsg As New DevExpress.XtraGrid.Columns.GridColumn() With {.FieldName = "MessageSnippet", .Caption = "الرسالة", .Visible = True}
        Dim colScheduled As New DevExpress.XtraGrid.Columns.GridColumn() With {.FieldName = "ScheduledAt", .Caption = "الوقت المجدول", .Visible = True}
        Dim colDelay As New DevExpress.XtraGrid.Columns.GridColumn() With {.FieldName = "DelaySeconds", .Caption = "تأخير (ث)", .Visible = True}
        Dim colState As New DevExpress.XtraGrid.Columns.GridColumn() With {.FieldName = "JobState", .Caption = "الحالة", .Visible = True}
        ViewQueue.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {colId, colNum, colMsg, colScheduled, colDelay, colState, colDelete})
        ViewQueue.OptionsBehavior.Editable = True
        colId.OptionsColumn.AllowEdit = False
        colNum.OptionsColumn.AllowEdit = False
        colMsg.OptionsColumn.AllowEdit = False
        colScheduled.OptionsColumn.AllowEdit = False
        colDelay.OptionsColumn.AllowEdit = False
        colState.OptionsColumn.AllowEdit = False
        colDelete.OptionsColumn.AllowEdit = True
    End Sub

    Private Async Sub ViewQueue_DeleteButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs)
        If String.IsNullOrWhiteSpace(ClinicId) Then Return
        Dim rowHandle = ViewQueue.FocusedRowHandle
        If rowHandle < 0 Then Return
        Dim item = TryCast(ViewQueue.GetRow(rowHandle), PendingMessageItem)
        If item Is Nothing OrElse String.IsNullOrWhiteSpace(item.JobId) Then Return
        Try
            Dim ok = Await _service.DeleteFromQueueAsync(ClinicId, item.JobId)
            If ok Then Await LoadQueueAsync()
            MessageBox.Show(If(ok, "تم الحذف.", "فشل الحذف."), "طابور الرسائل", MessageBoxButtons.OK, If(ok, MessageBoxIcon.Information, MessageBoxIcon.Warning))
        Catch ex As Exception
            MessageBox.Show(ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub ConfigureFailedColumns()
        ViewFailed.Columns.Clear()
        Dim colNum As New DevExpress.XtraGrid.Columns.GridColumn() With {.FieldName = "Number", .Caption = "الرقم", .Visible = True}
        Dim colMsg As New DevExpress.XtraGrid.Columns.GridColumn() With {.FieldName = "Message", .Caption = "الرسالة", .Visible = True}
        Dim colErr As New DevExpress.XtraGrid.Columns.GridColumn() With {.FieldName = "Error", .Caption = "الخطأ", .Visible = True}
        Dim colAt As New DevExpress.XtraGrid.Columns.GridColumn() With {.FieldName = "FailedAt", .Caption = "وقت الفشل", .Visible = True}
        ViewFailed.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {colNum, colMsg, colErr, colAt})
    End Sub

    Private Async Sub TabControl_SelectedPageChanged(sender As Object, e As DevExpress.XtraTab.TabPageChangedEventArgs)
        If TabControl.SelectedTabPage Is TabQueue Then
            If Not _queueRefreshTimer.Enabled Then _queueRefreshTimer.Start()
            Await LoadQueueAsync()
        Else
            _queueRefreshTimer.Stop()
        End If
        If e.Page Is TabFailed Then Await LoadFailedAsync()
        If e.Page Is _tabArchive Then Await LoadArchiveAsync()
    End Sub

    Private Async Sub BtnRefreshQueue_Click(sender As Object, e As EventArgs)
        Await LoadQueueAsync()
    End Sub

    Private Async Sub BtnRefreshFailed_Click(sender As Object, e As EventArgs)
        Await LoadFailedAsync()
    End Sub

    Private Async Function LoadQueueAsync(Optional suppressErrors As Boolean = False) As Task
        If String.IsNullOrWhiteSpace(ClinicId) Then Return
        Try
            Dim list = Await _service.GetQueueAsync(ClinicId)
            _queueBinding.Clear()
            For Each item In list
                _queueBinding.Add(item)
            Next
        Catch ex As Exception
            If Not suppressErrors Then MessageBox.Show(ex.Message, "طابور الرسائل", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End Try
    End Function

    Private Async Sub QueueRefreshTimer_Tick(sender As Object, e As EventArgs)
        If TabControl.SelectedTabPage IsNot TabQueue OrElse String.IsNullOrWhiteSpace(ClinicId) Then Return
        If _queueRefreshInFlight Then Return
        _queueRefreshInFlight = True
        Try
            Await LoadQueueAsync(suppressErrors:=True)
        Finally
            _queueRefreshInFlight = False
        End Try
    End Sub

    Private Async Function LoadFailedAsync() As Task
        If String.IsNullOrWhiteSpace(ClinicId) Then Return
        Try
            Dim list = Await _service.GetFailedMessagesAsync(ClinicId)
            _failedBinding.Clear()
            For Each item In list
                _failedBinding.Add(item)
            Next
        Catch ex As Exception
            MessageBox.Show(ex.Message, "الرسائل الفاشلة", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End Try
    End Function

    Private Async Function LoadArchiveAsync(Optional suppressErrors As Boolean = False) As Task
        If String.IsNullOrWhiteSpace(ClinicId) Then Return
        Try
            ' Full list from gateway; number box filters client-side (partial digit match while typing).
            Dim res = Await _service.TryGetWhatsappArchiveAsync(ClinicId, filterNumber:=Nothing)
            _archiveBinding.Clear()
            If res.HttpOk Then
                For Each row In res.Items
                    _archiveBinding.Add(row)
                Next
            ElseIf Not suppressErrors Then
                Dim msg = If(String.IsNullOrWhiteSpace(res.ErrorDetail), If(Eng, "Archive request failed.", "فشل طلب الأرشيف."), res.ErrorDetail)
                MessageBox.Show(msg, If(Eng, "WhatsApp archive", "أرشيف واتساب"), MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If
        Catch ex As Exception
            If Not suppressErrors Then MessageBox.Show(ex.Message, If(Eng, "WhatsApp archive", "أرشيف واتساب"), MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End Try
        SyncArchiveNumberFilterFromText()
        If _viewArchive IsNot Nothing Then _viewArchive.RefreshData()
    End Function

    Private Async Sub BtnArchiveRefresh_Click(sender As Object, e As EventArgs)
        _btnArchiveRefresh.Enabled = False
        Try
            Await LoadArchiveAsync()
        Finally
            _btnArchiveRefresh.Enabled = True
        End Try
    End Sub

    Private Async Sub BtnArchiveRetryFailed_Click(sender As Object, e As EventArgs)
        If String.IsNullOrWhiteSpace(ClinicId) Then Return
        _btnArchiveRetryFailed.Enabled = False
        Try
            Dim count = Await _service.RetryFailedAsync(ClinicId)
            If count >= 0 Then
                MessageBox.Show(If(Eng, $"Retried {count} message(s).", $"تمت إعادة المحاولة لـ {count} رسالة."),
                                If(Eng, "Retry", "إعادة المحاولة"), MessageBoxButtons.OK, MessageBoxIcon.Information)
                Await LoadArchiveAsync()
            Else
                MessageBox.Show(If(Eng, "Retry request failed.", "فشل طلب إعادة الإرسال."),
                                If(Eng, "WhatsApp archive", "أرشيف واتساب"), MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, If(Eng, "WhatsApp", "واتساب"), MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            _btnArchiveRetryFailed.Enabled = True
        End Try
    End Sub

    Private Sub ViewArchive_DoubleClick(sender As Object, e As EventArgs)
        Dim h = _viewArchive.FocusedRowHandle
        If h < 0 Then Return
        Dim row = TryCast(_viewArchive.GetRow(h), WhatsAppArchiveItem)
        If row Is Nothing Then Return
        Dim sb As New StringBuilder()
        sb.AppendLine(If(Eng, "Gateway archive message", "رسالة من أرشيف الخادم"))
        sb.AppendLine("Id: " & If(row.Id, ""))
        sb.AppendLine(If(Eng, "Status: ", "الحالة: ") & If(row.Status, ""))
        sb.AppendLine(If(Eng, "Number: ", "الرقم: ") & If(row.TargetNumber, ""))
        sb.AppendLine(If(Eng, "Attachment: ", "مرفق: ") & row.HasAttachment.ToString())
        If row.CreatedAtUtc.HasValue Then sb.AppendLine(If(Eng, "Created (UTC): ", "الإنشاء UTC: ") & row.CreatedAtUtc.Value.ToString("yyyy-MM-dd HH:mm:ss"))
        If row.LastUpdatedAtUtc.HasValue Then sb.AppendLine(If(Eng, "Updated (UTC): ", "التحديث UTC: ") & row.LastUpdatedAtUtc.Value.ToString("yyyy-MM-dd HH:mm:ss"))
        sb.AppendLine()
        sb.AppendLine(If(Eng, "Message:", "الرسالة:"))
        sb.AppendLine(If(row.MessageText, ""))
        sb.AppendLine()
        sb.AppendLine(If(Eng, "Error:", "الخطأ:"))
        sb.AppendLine(If(row.ErrorMessage, ""))
        MessageBox.Show(sb.ToString(), If(Eng, "WhatsApp archive", "أرشيف واتساب"), MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Private Async Sub BtnDeleteFromQueue_Click(sender As Object, e As EventArgs)
        If String.IsNullOrWhiteSpace(ClinicId) Then Return
        Dim rowHandle = ViewQueue.FocusedRowHandle
        If rowHandle < 0 Then
            MessageBox.Show("اختر رسالة من الطابور.", "حذف", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If
        Dim item = TryCast(ViewQueue.GetRow(rowHandle), PendingMessageItem)
        If item Is Nothing OrElse String.IsNullOrWhiteSpace(item.JobId) Then Return
        Try
            Dim ok = Await _service.DeleteFromQueueAsync(ClinicId, item.JobId)
            If ok Then Await LoadQueueAsync()
            MessageBox.Show(If(ok, "تم الحذف.", "فشل الحذف."), "طابور الرسائل", MessageBoxButtons.OK, If(ok, MessageBoxIcon.Information, MessageBoxIcon.Warning))
        Catch ex As Exception
            MessageBox.Show(ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Async Sub BtnRetryFailed_Click(sender As Object, e As EventArgs)
        If String.IsNullOrWhiteSpace(ClinicId) Then Return
        Try
            Dim count = Await _service.RetryFailedAsync(ClinicId)
            If count >= 0 Then
                MessageBox.Show($"تمت إعادة إرسال {count} رسالة.", "إعادة الإرسال", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Await LoadFailedAsync()
            Else
                MessageBox.Show("فشل طلب إعادة الإرسال.", "الرسائل الفاشلة", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub WhatsAppForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Load clinic ID from Clinic table (Guid) for WhatsApp API
        If String.IsNullOrWhiteSpace(ClinicId) Then
            Try
                Dim clinicData As New ClinicDATA()
                Dim clinics = clinicData.SelectAll().ToList()
                Dim firstClinic As Clinic = clinics.FirstOrDefault()
                If firstClinic IsNot Nothing Then
                    ClinicId = firstClinic.ClinicID.ToString()
                    _clinicDisplayName = If(String.IsNullOrWhiteSpace(firstClinic.ClinicNameAr), firstClinic.ClinicNameEn, firstClinic.ClinicNameAr)
                    UpdateStatusLabel("العيادة: " & _clinicDisplayName)
                Else
                    UpdateStatusLabel("لا توجد عيادة في قاعدة البيانات")
                End If
            Catch ex As Exception
                UpdateStatusLabel("خطأ في تحميل العيادة: " & ex.Message)
            End Try
        End If
        If TabControl.SelectedTabPage Is TabQueue AndAlso Not String.IsNullOrWhiteSpace(ClinicId) Then _queueRefreshTimer.Start()
    End Sub

    Private Sub UpdateStatusLabel(text As String)
        LblStatus.Text = text
    End Sub

    Private Function FormatClinicWithStatus(status As String) As String
        If String.IsNullOrEmpty(_clinicDisplayName) Then Return status
        Return "العيادة:   " & _clinicDisplayName & "  -  " & status
    End Function

    Private Async Sub BtnStartConnection_Click(sender As Object, e As EventArgs) Handles BtnStartConnection.Click
        If String.IsNullOrWhiteSpace(ClinicId) Then
            UpdateStatusLabel("يرجى تعيين ClinicId")
            Return
        End If

        BtnStartConnection.Enabled = False
        UpdateStatusLabel(FormatClinicWithStatus("جاري الاتصال..."))

        Try
            Await _service.ConnectAsync(ClinicId)
            Await RefreshQrAndStatusAsync()
        Catch ex As Exception
            UpdateStatusLabel("خطأ: " & ex.Message)
            BtnStartConnection.Enabled = True
            Return
        End Try

        UpdateStatusLabel(FormatClinicWithStatus("امسح رمز QR"))
        BtnStartConnection.Enabled = True
        PanelSuccess.Visible = False
        PanelQr.Visible = True
        _statusTimer.Start()
    End Sub

    Private Async Sub StatusTimer_Tick(sender As Object, e As EventArgs)
        If String.IsNullOrWhiteSpace(ClinicId) Then Return
        Try
            Dim connected = Await _service.GetConnectionStatusAsync(ClinicId, bypassStatusThrottle:=False)
            If connected AndAlso IsHandleCreated Then
                BeginInvoke(New Action(Sub()
                                           _statusTimer.Stop()
                                           PanelQr.Visible = False
                                           PanelSuccess.Visible = True
                                           UpdateStatusLabel(FormatClinicWithStatus("متصل ✓"))
                                       End Sub))
            End If
        Catch
            ' keep polling
        End Try
    End Sub

    Private Async Function RefreshQrAndStatusAsync() As Task
        Dim qr = Await _service.GetQrCodeAsync(ClinicId)
        If Not String.IsNullOrWhiteSpace(qr) Then
            Dim img = QrStringToImage(qr)
            If img IsNot Nothing Then
                PictureQr.Image = img
            End If
        End If
    End Function

    ''' <summary>
    ''' Converts API qrCode value (base64 or data URL) to System.Drawing.Image.
    ''' </summary>
    Private Shared Function QrStringToImage(qrCode As String) As Image
        If String.IsNullOrWhiteSpace(qrCode) Then Return Nothing
        Dim base64 = qrCode
        If qrCode.StartsWith("data:", StringComparison.OrdinalIgnoreCase) Then
            Dim idx = qrCode.IndexOf(",", StringComparison.Ordinal)
            If idx >= 0 Then base64 = qrCode.Substring(idx + 1)
        End If
        Try
            Dim bytes = Convert.FromBase64String(base64.Trim())
            Using ms As New MemoryStream(bytes)
                Return Image.FromStream(ms)
            End Using
        Catch
            Return Nothing
        End Try
    End Function

    Protected Overrides Sub OnFormClosing(e As System.Windows.Forms.FormClosingEventArgs)
        _statusTimer.Stop()
        _statusTimer.Dispose()
        _queueRefreshTimer.Stop()
        _queueRefreshTimer.Dispose()
        MyBase.OnFormClosing(e)
    End Sub


End Class
