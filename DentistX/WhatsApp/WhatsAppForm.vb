Imports System.Drawing
Imports System.IO
Imports System.Linq
Imports System.Threading
Imports System.Threading.Tasks
Imports System.ComponentModel
Imports DevExpress.XtraEditors

Public Class WhatsAppForm
    Private _service As WhatsAppService
    Private _statusTimer As System.Windows.Forms.Timer
    Private _queueRefreshTimer As System.Windows.Forms.Timer
    Private _queueRefreshInFlight As Boolean
    Private _clinicIdValue As String = ""
    Private _clinicDisplayName As String = ""
    Private _queueBinding As BindingList(Of PendingMessageItem)
    Private _failedBinding As BindingList(Of FailedMessageItem)
    Private _repoQueueDelete As DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit

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
        _statusTimer.Interval = 3000
        AddHandler _statusTimer.Tick, AddressOf StatusTimer_Tick
        _queueRefreshTimer = New System.Windows.Forms.Timer With {.Interval = 1000}
        AddHandler _queueRefreshTimer.Tick, AddressOf QueueRefreshTimer_Tick
        _queueBinding = New BindingList(Of PendingMessageItem)
        _failedBinding = New BindingList(Of FailedMessageItem)
        GridQueue.DataSource = _queueBinding
        GridFailed.DataSource = _failedBinding
        ConfigureQueueColumns()
        ConfigureFailedColumns()
        AddHandler TabControl.SelectedPageChanged, AddressOf TabControl_SelectedPageChanged
        AddHandler BtnRefreshQueue.Click, AddressOf BtnRefreshQueue_Click
        AddHandler BtnDeleteFromQueue.Click, AddressOf BtnDeleteFromQueue_Click
        AddHandler BtnRefreshFailed.Click, AddressOf BtnRefreshFailed_Click
        AddHandler BtnRetryFailed.Click, AddressOf BtnRetryFailed_Click
        AddHandler BtnSendMessage.Click, AddressOf BtnSendMessage_Click
        AddHandler TxtSendFile.Properties.ButtonClick, AddressOf TxtSendFile_ButtonClick
        AddHandler BtnDisconnect.Click, AddressOf BtnDisconnect_Click
        AddHandler btnRefresh.Click, AddressOf BtnRefresh_Click


    End Sub

    Private Async Sub BtnRefresh_Click(sender As Object, e As EventArgs)
        If String.IsNullOrWhiteSpace(ClinicId) Then Return
        btnRefresh.Enabled = False
        Try
            Dim connected = Await _service.GetConnectionStatusAsync(ClinicId)
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
            Dim connected = Await _service.GetConnectionStatusAsync(ClinicId)
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
            Await _service.SendMessageAsync(ClinicId, number, message, filePath, ctx)
            MessageBox.Show("تم وضع الرسالة في الطابور.", "إرسال", MessageBoxButtons.OK, MessageBoxIcon.Information)
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
            Dim connected = Await _service.GetConnectionStatusAsync(ClinicId)
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
