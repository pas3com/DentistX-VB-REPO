Imports System
Imports System.Collections.Generic
Imports System.Drawing
Imports System.IO
Imports System.Linq
Imports System.Globalization
Imports System.Net.Http
Imports System.Text
Imports System.Text.Json
Imports System.Threading.Tasks
Imports System.Windows.Forms

Public Class WhatsAppService
    ''' <summary>Reduces outbound GET /status traffic (can stress the gateway and correlate with flaky sessions).</summary>
    Private Shared ReadOnly GatewayStatusGate As New Object()
    Private Shared _gatewayStatusClinicNorm As String
    Private Shared _gatewayStatusUtc As DateTime = DateTime.MinValue
    Private Shared _gatewayStatusConnected As Boolean

    ' Base URL for WhatsApp API (without trailing path)
    Private ReadOnly _baseUrl As String = "https://whatsapp-sender-api.dentistx.net"
    'Private ReadOnly _apiUrl As String = "https://whatsapp-sender-api.dentistx.net/api/gateway/whatsapp/send"
    Private ReadOnly _httpClient As HttpClient

    ''' <summary>
    ''' Returns the current clinic ID (Guid string) from the Clinic table for use with WhatsApp API.
    ''' Use this anywhere in the app when sending WhatsApp messages. Returns Nothing if no clinic.
    ''' </summary>
    Public Shared Function GetCurrentClinicId() As String
        Try
            Dim clinicData As New ClinicDATA()
            Dim first As Clinic = clinicData.SelectAll().FirstOrDefault()
            If first Is Nothing Then Return Nothing
            Return first.ClinicID.ToString()
        Catch
            Return Nothing
        End Try
    End Function

    ''' <summary>Opens <see cref="WhatsAppForm"/> modally so the user can connect (QR). Caller should re-check status after it closes.</summary>
    Public Shared Sub ShowWhatsAppConnectionForm(Optional owner As IWin32Window = Nothing)
        Using f As New WhatsAppForm()
            Dim ctl = TryCast(owner, Control)
            If ctl IsNot Nothing Then
                Dim host = ctl.FindForm()
                If host IsNot Nothing AndAlso host.Icon IsNot Nothing Then f.Icon = host.Icon
            End If
            f.StartPosition = If(owner IsNot Nothing, FormStartPosition.CenterParent, FormStartPosition.CenterScreen)
            If owner IsNot Nothing Then
                f.ShowDialog(owner)
            Else
                f.ShowDialog()
            End If
        End Using
    End Sub

    ''' <summary>
    ''' Call before manual sends: verifies clinic exists and gateway reports WhatsApp connected.
    ''' When disconnected, briefly shows a wait dialog while silently retrying (same API sequence as
    ''' <see cref="WhatsAppForm"/> refresh: status, optional <see cref="ConnectAsync"/>, QR endpoint ping, repeat).
    ''' If still disconnected, opens <see cref="WhatsAppForm"/> for QR/connect, then re-checks.
    ''' Returns False if still not connected.
    ''' </summary>
    Public Shared Async Function EnsureWhatsAppConnectedOrNotifyAsync(Optional owner As IWin32Window = Nothing) As Task(Of Boolean)
        Dim clinicId = GetCurrentClinicId()
        If String.IsNullOrWhiteSpace(clinicId) Then
            MessageBox.Show(owner, If(Eng, "No clinic configured for WhatsApp.", "لا توجد عيادة مكونة لواتساب."),
                            If(Eng, "WhatsApp", "واتساب"), MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return False
        End If
        Try
            Dim wa As New WhatsAppService()
            If Await wa.GetConnectionStatusAsync(clinicId, bypassStatusThrottle:=True).ConfigureAwait(True) Then Return True
        Catch
        End Try

        If Await TryEnsureWhatsConnectedSilentAsync(owner, clinicId).ConfigureAwait(True) Then Return True

        ShowWhatsAppConnectionForm(owner)
        Try
            Dim wa2 As New WhatsAppService()
            If Await wa2.GetConnectionStatusAsync(clinicId, bypassStatusThrottle:=True).ConfigureAwait(True) Then Return True
        Catch
        End Try
        MessageBox.Show(owner,
                        If(Eng, "WhatsApp is still not connected. Use the Connection tab to scan the QR code, then try sending again.",
                           "واتساب لا يزال غير متصل. استخدم تبويب الاتصال لمسح رمز الاستجابة ثم أعد محاولة الإرسال."),
                        If(Eng, "WhatsApp", "واتساب"), MessageBoxButtons.OK, MessageBoxIcon.Warning)
        Return False
    End Function

    ''' <summary>
    ''' Shows the short wait dialog and runs the same silent reconnect attempts as <see cref="EnsureWhatsAppConnectedOrNotifyAsync"/>,
    ''' without opening another <see cref="WhatsAppForm"/> (avoids nested modals when sending from inside that form).
    ''' </summary>
    Public Shared Async Function TryEnsureWhatsConnectedSilentAsync(owner As IWin32Window, clinicId As String) As Task(Of Boolean)
        If String.IsNullOrWhiteSpace(clinicId) Then Return False
        Dim waitShell As Form = Nothing
        Try
            waitShell = CreateWhatsReconnectWaitShell(owner)
            waitShell.Show(owner)
            Dim waSilent As New WhatsAppService()
            Return Await TrySilentWhatsReconnectAsync(waSilent, clinicId, attemptCount:=3, delayBetweenMs:=1200, captureUiContext:=True).ConfigureAwait(True)
        Finally
            If waitShell IsNot Nothing AndAlso Not waitShell.IsDisposed Then
                waitShell.Close()
                waitShell.Dispose()
            End If
        End Try
    End Function

    ''' <summary>
    ''' No UI: same reconnect attempts as interactive flows (for timers, queues, schedules). Does not open <see cref="WhatsAppForm"/>.
    ''' </summary>
    Public Shared Async Function TrySilentWhatsReconnectBackgroundAsync(clinicId As String) As Task(Of Boolean)
        If String.IsNullOrWhiteSpace(clinicId) Then Return False
        Dim wa As New WhatsAppService()
        Try
            If Await wa.GetConnectionStatusAsync(clinicId, bypassStatusThrottle:=False).ConfigureAwait(False) Then Return True
        Catch
        End Try
        Return Await TrySilentWhatsReconnectAsync(wa, clinicId, attemptCount:=3, delayBetweenMs:=1200, captureUiContext:=False).ConfigureAwait(False)
    End Function

    ''' <summary>
    ''' Mirrors <see cref="WhatsAppForm.BtnRefresh_Click"/> when disconnected: poll status, ping QR endpoint (<see cref="GetQrCodeAsync"/>),
    ''' and on the first attempt calls <see cref="ConnectAsync"/> like <see cref="WhatsAppForm.BtnStartConnection_Click"/>.
    ''' <paramref name="captureUiContext"/> True when invoked from UI async handlers (wait shell already visible).
    ''' </summary>
    Private Shared Async Function TrySilentWhatsReconnectAsync(wa As WhatsAppService, clinicId As String, attemptCount As Integer, delayBetweenMs As Integer, Optional captureUiContext As Boolean = True) As Task(Of Boolean)
        Dim syn As Boolean = captureUiContext
        For attempt As Integer = 1 To attemptCount
            Try
                If Await wa.GetConnectionStatusAsync(clinicId, bypassStatusThrottle:=True).ConfigureAwait(syn) Then Return True
            Catch
            End Try

            If attempt = 1 Then
                Try
                    Await wa.ConnectAsync(clinicId).ConfigureAwait(syn)
                Catch
                End Try
            End If

            Try
                Await wa.GetQrCodeAsync(clinicId).ConfigureAwait(syn)
            Catch
            End Try

            Try
                If Await wa.GetConnectionStatusAsync(clinicId, bypassStatusThrottle:=True).ConfigureAwait(syn) Then Return True
            Catch
            End Try

            If attempt < attemptCount Then
                Await Task.Delay(delayBetweenMs).ConfigureAwait(syn)
            End If
        Next

        Try
            Return Await wa.GetConnectionStatusAsync(clinicId, bypassStatusThrottle:=True).ConfigureAwait(syn)
        Catch
            Return False
        End Try
    End Function

    Private Shared Function CreateWhatsReconnectWaitShell(owner As IWin32Window) As Form
        Dim f As New Form()
        f.SuspendLayout()
        f.FormBorderStyle = FormBorderStyle.FixedDialog
        f.ControlBox = False
        f.ShowInTaskbar = False
        f.StartPosition = If(owner IsNot Nothing, FormStartPosition.CenterParent, FormStartPosition.CenterScreen)
        f.ClientSize = New Size(440, 110)
        f.Text = If(Eng, "WhatsApp", "واتساب")
        Dim lbl As New Label With {
            .Dock = DockStyle.Fill,
            .TextAlign = ContentAlignment.MiddleCenter,
            .Font = New Font("Calibri", 10.0F, FontStyle.Bold),
            .Text = If(Eng, "Checking WhatsApp connection, please wait...", "جاري التحقق من اتصال واتساب، يرجى الانتظار...")
        }
        f.Controls.Add(lbl)
        f.ResumeLayout(False)
        Dim ctlOwner = TryCast(owner, Control)
        Dim host = If(ctlOwner IsNot Nothing, ctlOwner.FindForm(), Nothing)
        If host IsNot Nothing AndAlso host.Icon IsNot Nothing Then f.Icon = host.Icon
        Return f
    End Function

    Public Sub New()
        _httpClient = New HttpClient()
        ' زيادة وقت الانتظار لتجنب انقطاع الاتصال مع الملفات الكبيرة
        _httpClient.Timeout = TimeSpan.FromMinutes(5)
    End Sub

    Private Shared Function TruncatePreview(message As String) As String
        If String.IsNullOrEmpty(message) Then Return ""
        If message.Length <= 500 Then Return message
        Return message.Substring(0, 500)
    End Function

    Private Shared Sub NotifySessionAfterLog(logId As Long, success As Boolean, category As String, sourceHint As String, numberSafe As String, patientId As Integer?, patientDisplay As String, message As String, hasFile As Boolean, clinicSafe As String, responseOrError As String, context As WhatsAppSendContext)
        Dim revealDock = context IsNot Nothing AndAlso context.RevealMessageCenter
        Dim fromLocalQueue = context IsNot Nothing AndAlso context.SentAfterLocalQueue
        Dim row As WhatsAppActivityLogRow = Nothing
        If logId > 0 Then row = WhatsAppActivityLogRepository.GetById(logId)
        If row Is Nothing Then
            row = New WhatsAppActivityLogRow With {
                .LogId = logId,
                .SentAtUtc = DateTime.UtcNow,
                .Success = success,
                .Category = category,
                .SourceHint = sourceHint,
                .TargetNumber = numberSafe,
                .PatientId = patientId,
                .PatientDisplayName = patientDisplay,
                .MessagePreview = TruncatePreview(message),
                .FullMessage = If(message, ""),
                .HasAttachment = hasFile,
                .ClinicId = clinicSafe,
                .ClinicDisplayName = "",
                .ResponseOrError = responseOrError,
                .SentAfterLocalQueue = fromLocalQueue
            }
        Else
            row.SentAfterLocalQueue = fromLocalQueue
            row.FullMessage = If(message, "")
        End If
        WhatsAppActivityLogRow.TryApplyQueueMetadataFromResponse(row)
        WhatsAppSessionMessageCenter.RaiseMessageProcessed(row, revealDock)
    End Sub

    ''' <summary>
    ''' Get QR code string for pairing (GET /api/whatsapp/qr/:clinicId).
    ''' Returns base64 or data URL of QR image if success; Nothing on error or no QR yet.
    ''' </summary>
    Public Async Function GetQrCodeAsync(clinicId As String) As Task(Of String)
        If String.IsNullOrWhiteSpace(clinicId) Then Return Nothing
        Try
            Dim url = $"{_baseUrl}/api/gateway/whatsapp/qr/{Uri.EscapeDataString(clinicId)}"
            Dim response = Await _httpClient.GetAsync(url)
            Dim json = Await response.Content.ReadAsStringAsync()
            If Not response.IsSuccessStatusCode Then Return Nothing
            Using doc = JsonDocument.Parse(json)
                Dim root = doc.RootElement
                Dim success As JsonElement
                If root.TryGetProperty("success", success) AndAlso success.GetBoolean() Then
                    Dim qr As JsonElement
                    If root.TryGetProperty("qrCode", qr) Then Return qr.GetString()
                End If
            End Using
        Catch
            ' ignore
        End Try
        Return Nothing
    End Function

    ''' <summary>
    ''' Initiate WhatsApp connection (GET /connect/:clinicId).
    ''' </summary>
    Public Async Function ConnectAsync(clinicId As String) As Task(Of Boolean)
        If String.IsNullOrWhiteSpace(clinicId) Then Return False
        InvalidateGatewayConnectionStatus()
        Try
            Dim url = $"{_baseUrl}/connect/{Uri.EscapeDataString(clinicId)}"
            Dim response = Await _httpClient.GetAsync(url)
            Dim json = Await response.Content.ReadAsStringAsync()
            If Not response.IsSuccessStatusCode Then Return False
            Using doc = JsonDocument.Parse(json)
                Dim root = doc.RootElement
                Dim success As JsonElement
                If root.TryGetProperty("success", success) Then Return success.GetBoolean()
            End Using
        Catch
            ' ignore
        End Try
        Return False
    End Function

    ''' <summary>
    ''' Disconnect WhatsApp session (DELETE /api/whatsapp/session/:clinicId).
    ''' </summary>
    Public Async Function DisconnectAsync(clinicId As String) As Task(Of Boolean)
        If String.IsNullOrWhiteSpace(clinicId) Then Return False
        Try
            Dim url = $"{_baseUrl}/api/gateway/whatsapp/session/{Uri.EscapeDataString(clinicId)}"
            Dim response = Await _httpClient.DeleteAsync(url)
            Dim json = Await response.Content.ReadAsStringAsync()
            If Not response.IsSuccessStatusCode Then Return False
            Using doc = JsonDocument.Parse(json)
                Dim root = doc.RootElement
                Dim success As JsonElement
                Dim ok = root.TryGetProperty("success", success) AndAlso success.GetBoolean()
                If ok Then InvalidateGatewayConnectionStatus()
                Return ok
            End Using
        Catch
            Return False
        End Try
    End Function

    Public Shared Sub InvalidateGatewayConnectionStatus()
        SyncLock GatewayStatusGate
            _gatewayStatusClinicNorm = Nothing
            _gatewayStatusUtc = DateTime.MinValue
        End SyncLock
    End Sub

    ''' <summary>
    ''' Get connection status (GET /api/whatsapp/status/:clinicId). Returns True if connected.
    ''' When <paramref name="bypassStatusThrottle"/> is False, repeats within a short TTL return a cached result to reduce gateway churn.
    ''' </summary>
    Public Async Function GetConnectionStatusAsync(clinicId As String, Optional bypassStatusThrottle As Boolean = False) As Task(Of Boolean)
        If String.IsNullOrWhiteSpace(clinicId) Then Return False
        Dim key = clinicId.Trim()
        Dim nowUtc = DateTime.UtcNow
        Dim cacheTtl As Double
        If Not bypassStatusThrottle Then
            SyncLock GatewayStatusGate
                If _gatewayStatusClinicNorm IsNot Nothing AndAlso String.Equals(key, _gatewayStatusClinicNorm, StringComparison.Ordinal) AndAlso
                    _gatewayStatusUtc <> DateTime.MinValue Then
                    cacheTtl = If(_gatewayStatusConnected, 20.0R, 5.0R)
                    Dim age = (nowUtc - _gatewayStatusUtc).TotalSeconds
                    If age >= 0.0R AndAlso age < cacheTtl Then
                        Return _gatewayStatusConnected
                    End If
                End If
            End SyncLock
        End If

        Dim result As Boolean = False
        Try
            Dim url = $"{_baseUrl}/api/gateway/whatsapp/status/{Uri.EscapeDataString(key)}"
            Dim response = Await _httpClient.GetAsync(url)
            Dim json = Await response.Content.ReadAsStringAsync()
            If Not response.IsSuccessStatusCode Then
                result = False
            Else
                Using doc = JsonDocument.Parse(json)
                    Dim root = doc.RootElement
                    Dim connected As JsonElement
                    If root.TryGetProperty("connected", connected) Then result = connected.GetBoolean()
                End Using
            End If
        Catch
            result = False
        End Try

        SyncLock GatewayStatusGate
            _gatewayStatusClinicNorm = key
            _gatewayStatusUtc = DateTime.UtcNow
            _gatewayStatusConnected = result
        End SyncLock
        Return result
    End Function

    ''' <summary>
    ''' دالة إرسال رسالة واتساب (نصية أو مع مرفق)
    ''' </summary>
    Public Async Function SendMessageAsync(clinicId As String, number As String, message As String, Optional filePath As String = "", Optional context As WhatsAppSendContext = Nothing) As Task(Of String)
        Dim msg As String = If(message, "").Trim()
        Dim hasFile As Boolean = Not String.IsNullOrWhiteSpace(filePath) AndAlso File.Exists(filePath)
        Dim cat = If(context IsNot Nothing AndAlso Not String.IsNullOrWhiteSpace(context.Category), context.Category, WhatsAppMessageCategories.General)
        Dim src = If(context IsNot Nothing, context.SourceHint, Nothing)
        Dim patId = If(context IsNot Nothing, context.PatientId, Nothing)
        Dim patDisplay = If(context IsNot Nothing AndAlso Not String.IsNullOrWhiteSpace(context.DisplayName), context.DisplayName.Trim(), Nothing)
        Dim suppressUi As Boolean = context IsNot Nothing AndAlso context.SuppressUiFeedback
        Dim clinicSafe = If(clinicId, "")
        Dim numberSafe = If(number, "").Trim()
        If String.IsNullOrWhiteSpace(msg) Then
            If hasFile Then
                msg = "{" & Path.GetFileName(filePath) & "}"
            Else
                Throw New Exception("لا يمكن إرسال رسالة فارغة عبر واتساب.")
            End If
        End If

        If WhatsAppOutboundRepository.IsOutboundInfrastructureReady() AndAlso
            Not (context IsNot Nothing AndAlso context.BypassOutboundQueue) Then
            Dim enq = WhatsAppOutboundRepository.TryEnqueueUnifiedFromSendIntent(
                clinicSafe, numberSafe, msg, If(hasFile, filePath, ""), context, cat)
            If enq.BlockDirectGatewaySend Then
                If enq.ShouldRequestImmediateDispatch Then WhatsAppOutboundDispatchService.RequestImmediateDispatch()
                Return BuildLocalOutboundEnqueueResponseJson(enq)
            End If
        End If

        ' 1. بناء هيكل البيانات الأساسي (الذي طلبه خادم النود جي إس)
        Dim requestPayload = New Dictionary(Of String, String) From {
            {"clinicId", clinicId},
            {"number", number},
            {"message", msg}
        }

        ' 2. معالجة الملف في حال تم تمرير مسار صحيح
        If hasFile Then
            ' قراءة الملف وتحويله إلى Base64
            Dim fileBytes As Byte() = File.ReadAllBytes(filePath)
            Dim base64String As String = Convert.ToBase64String(fileBytes)

            ' استخراج اسم الملف والامتداد
            Dim fileName As String = Path.GetFileName(filePath)
            Dim extension As String = Path.GetExtension(filePath).ToLower()

            ' إضافة بيانات الملف إلى الطلب
            requestPayload.Add("base64Data", base64String)
            requestPayload.Add("mimeType", GetMimeType(extension))
            requestPayload.Add("fileName", fileName)
        End If

        ' 3. تحويل البيانات إلى JSON
        Dim jsonContent = JsonSerializer.Serialize(requestPayload)
        Dim content = New StringContent(jsonContent, Encoding.UTF8, "application/json")

        ' 4. إرسال الطلب إلى السيرفر
        Dim httpFailureLogged As Boolean = False
        Try
            Dim response = Await _httpClient.PostAsync($"{_baseUrl}/api/gateway/whatsapp/send", content)
            Dim responseString = Await response.Content.ReadAsStringAsync()

            If Not response.IsSuccessStatusCode Then
                Dim errDetail = $"HTTP {response.StatusCode}: {responseString}"
                Dim lid = WhatsAppActivityLogRepository.Insert(False, cat, src, numberSafe, patId, patDisplay, msg, hasFile, clinicSafe, errDetail)
                If Not suppressUi Then WhatsAppToastHost.NotifySendResult(lid, False, numberSafe, errDetail)
                If Not suppressUi Then NotifySessionAfterLog(lid, False, cat, src, numberSafe, patId, patDisplay, msg, hasFile, clinicSafe, errDetail, context)
                httpFailureLogged = True
                Throw New Exception($"فشل الإرسال. كود الخطأ: {response.StatusCode}. التفاصيل: {responseString}")
            End If

            Dim lidOk = WhatsAppActivityLogRepository.Insert(True, cat, src, numberSafe, patId, patDisplay, msg, hasFile, clinicSafe, responseString)
            If Not suppressUi Then WhatsAppToastHost.NotifySendResult(lidOk, True, numberSafe, msg)
            If Not suppressUi Then NotifySessionAfterLog(lidOk, True, cat, src, numberSafe, patId, patDisplay, msg, hasFile, clinicSafe, responseString, context)
            Return responseString

        Catch ex As Exception
            If Not httpFailureLogged Then
                Dim lidF = WhatsAppActivityLogRepository.Insert(False, cat, src, numberSafe, patId, patDisplay, msg, hasFile, clinicSafe, ex.Message)
                If Not suppressUi Then WhatsAppToastHost.NotifySendResult(lidF, False, numberSafe, ex.Message)
                If Not suppressUi Then NotifySessionAfterLog(lidF, False, cat, src, numberSafe, patId, patDisplay, msg, hasFile, clinicSafe, ex.Message, context)
                Throw New Exception($"حدث خطأ أثناء الاتصال بخادم الواتساب: {ex.Message}")
            End If
            Throw ex
        End Try
    End Function

    Private Shared Function JsonOptionalString(el As JsonElement) As String
        Select Case el.ValueKind
            Case JsonValueKind.Null, JsonValueKind.Undefined
                Return Nothing
            Case JsonValueKind.String
                Return el.GetString()
            Case JsonValueKind.Number
                Return el.GetRawText().Trim()
            Case Else
                Return Nothing
        End Select
    End Function

    Private Shared Sub ApplyDelaySecondsFromJson(item As JsonElement, msg As PendingMessageItem)
        Dim v As JsonElement
        If Not item.TryGetProperty("delaySeconds", v) Then
            msg.DelaySeconds = Nothing
            Return
        End If
        Select Case v.ValueKind
            Case JsonValueKind.Null, JsonValueKind.Undefined
                msg.DelaySeconds = Nothing
            Case JsonValueKind.Number
                msg.DelaySeconds = CInt(Math.Truncate(Math.Max(0R, v.GetDouble())))
            Case JsonValueKind.String
                Dim s = If(v.GetString(), "").Trim()
                Dim d As Double
                If Double.TryParse(s, NumberStyles.Any, CultureInfo.InvariantCulture, d) Then
                    msg.DelaySeconds = CInt(Math.Truncate(Math.Max(0R, d)))
                Else
                    msg.DelaySeconds = Nothing
                End If
            Case Else
                msg.DelaySeconds = Nothing
        End Select
    End Sub

    ''' <summary>
    ''' List pending messages in queue (GET /api/whatsapp/queue/:clinicId). Returns HTTP success separately so callers can avoid pruning on network errors.
    ''' </summary>
    Public Async Function TryGetQueueAsync(clinicId As String) As Task(Of (HttpOk As Boolean, Items As List(Of PendingMessageItem)))
        Dim result As New List(Of PendingMessageItem)
        If String.IsNullOrWhiteSpace(clinicId) Then Return (False, result)
        Try
            Dim url = $"{_baseUrl}/api/gateway/whatsapp/queue/{Uri.EscapeDataString(clinicId)}"
            Dim response = Await _httpClient.GetAsync(url)
            Dim json = Await response.Content.ReadAsStringAsync()
            If Not response.IsSuccessStatusCode Then Return (False, result)
            Using doc = JsonDocument.Parse(json)
                Dim root = doc.RootElement
                Dim itemsToIterate As JsonElement
                If root.ValueKind = JsonValueKind.Array Then
                    itemsToIterate = root
                ElseIf root.TryGetProperty("pendingMessages", itemsToIterate) Then
                    ' fallback for nested format
                Else
                    Return (True, result)
                End If
                Dim v As JsonElement
                For Each item In itemsToIterate.EnumerateArray()
                    If item.ValueKind <> JsonValueKind.Object Then Continue For
                    Dim msg As New PendingMessageItem
                    If item.TryGetProperty("jobId", v) Then msg.JobId = JsonOptionalString(v)
                    If String.IsNullOrWhiteSpace(msg.JobId) AndAlso item.TryGetProperty("messageId", v) Then msg.JobId = JsonOptionalString(v)
                    If String.IsNullOrWhiteSpace(msg.JobId) AndAlso item.TryGetProperty("id", v) Then msg.JobId = JsonOptionalString(v)
                    If item.TryGetProperty("targetNumber", v) Then msg.TargetNumber = JsonOptionalString(v)
                    If String.IsNullOrWhiteSpace(msg.TargetNumber) AndAlso item.TryGetProperty("number", v) Then msg.TargetNumber = JsonOptionalString(v)
                    If item.TryGetProperty("messageSnippet", v) Then msg.MessageSnippet = JsonOptionalString(v)
                    If String.IsNullOrWhiteSpace(msg.MessageSnippet) AndAlso item.TryGetProperty("message", v) Then msg.MessageSnippet = JsonOptionalString(v)
                    If item.TryGetProperty("scheduledAt", v) Then msg.ScheduledAt = JsonOptionalString(v)
                    If item.TryGetProperty("jobState", v) Then msg.JobState = JsonOptionalString(v)
                    ApplyDelaySecondsFromJson(item, msg)
                    result.Add(msg)
                Next
            End Using
            Return (True, result)
        Catch
            Return (False, result)
        End Try
    End Function

    ''' <summary>List pending messages in queue (GET /api/whatsapp/queue/:clinicId).</summary>
    Public Async Function GetQueueAsync(clinicId As String) As Task(Of List(Of PendingMessageItem))
        Dim t = Await TryGetQueueAsync(clinicId).ConfigureAwait(False)
        Return t.Items
    End Function

    ''' <summary>
    ''' Delete message from queue (DELETE /api/gateway/whatsapp/queue/{customerId}/{jobId}).
    ''' Success returns 204 No Content (empty body). Errors return 404 or 403 with JSON body.
    ''' </summary>
    Public Async Function DeleteFromQueueAsync(clinicId As String, jobId As String) As Task(Of Boolean)
        If String.IsNullOrWhiteSpace(clinicId) OrElse String.IsNullOrWhiteSpace(jobId) Then Return False
        Try
            Dim url = $"{_baseUrl}/api/gateway/whatsapp/queue/{Uri.EscapeDataString(clinicId)}/{Uri.EscapeDataString(jobId)}"
            Dim response = Await _httpClient.DeleteAsync(url)
            If response.IsSuccessStatusCode Then
                Return True
            End If
            Return False
        Catch
            Return False
        End Try
    End Function

    Private Shared Function JsonOptionalDateTimeUtc(el As JsonElement) As DateTime?
        Select Case el.ValueKind
            Case JsonValueKind.Null, JsonValueKind.Undefined
                Return Nothing
            Case JsonValueKind.String
                Dim s = el.GetString()
                If String.IsNullOrWhiteSpace(s) Then Return Nothing
                Dim dt As DateTime
                If DateTime.TryParse(s, CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind, dt) Then
                    Return dt
                End If
                Return Nothing
            Case Else
                Return Nothing
        End Select
    End Function

    Private Shared Function JsonGetBoolLoose(el As JsonElement, Optional defaultValue As Boolean = False) As Boolean
        Select Case el.ValueKind
            Case JsonValueKind.True
                Return True
            Case JsonValueKind.False
                Return False
            Case JsonValueKind.String
                Dim s = If(el.GetString(), "").Trim()
                If s.Length = 0 Then Return defaultValue
                Dim b As Boolean
                If Boolean.TryParse(s, b) Then Return b
                Dim n As Integer
                If Integer.TryParse(s, n) Then Return n <> 0
                Return defaultValue
            Case JsonValueKind.Number
                Return el.GetInt64() <> 0L
            Case Else
                Return defaultValue
        End Select
    End Function

    Private Shared Sub TryFillArchiveItem(item As JsonElement, row As WhatsAppArchiveItem)
        If item.ValueKind <> JsonValueKind.Object OrElse row Is Nothing Then Return
        Dim v As JsonElement
        If item.TryGetProperty("id", v) Then row.Id = JsonOptionalString(v)
        If String.IsNullOrWhiteSpace(row.Id) AndAlso item.TryGetProperty("messageId", v) Then row.Id = JsonOptionalString(v)
        If item.TryGetProperty("targetNumber", v) Then row.TargetNumber = JsonOptionalString(v)
        If String.IsNullOrWhiteSpace(row.TargetNumber) AndAlso item.TryGetProperty("number", v) Then row.TargetNumber = JsonOptionalString(v)
        If item.TryGetProperty("messageText", v) Then row.MessageText = JsonOptionalString(v)
        If String.IsNullOrWhiteSpace(row.MessageText) AndAlso item.TryGetProperty("message", v) Then row.MessageText = JsonOptionalString(v)
        If item.TryGetProperty("hasAttachment", v) Then row.HasAttachment = JsonGetBoolLoose(v, False)
        If item.TryGetProperty("status", v) Then row.Status = JsonOptionalString(v)
        If item.TryGetProperty("errorMessage", v) Then row.ErrorMessage = JsonOptionalString(v)
        If String.IsNullOrWhiteSpace(row.ErrorMessage) AndAlso item.TryGetProperty("error", v) Then row.ErrorMessage = JsonOptionalString(v)
        If item.TryGetProperty("createdAt", v) Then row.CreatedAtUtc = JsonOptionalDateTimeUtc(v)
        If item.TryGetProperty("lastUpdatedAt", v) Then row.LastUpdatedAtUtc = JsonOptionalDateTimeUtc(v)
    End Sub

    ''' <summary>
    ''' Gateway delivery archive (GET /api/whatsapp-archive/:clinicId, optional ?number= international digits).
    ''' Returns HTTP success flag, parsed rows, and error body or exception text when HTTP fails.
    ''' </summary>
    Public Async Function TryGetWhatsappArchiveAsync(clinicId As String, Optional filterNumber As String = Nothing) As Task(Of (HttpOk As Boolean, Items As List(Of WhatsAppArchiveItem), ErrorDetail As String))
        Dim result As New List(Of WhatsAppArchiveItem)
        If String.IsNullOrWhiteSpace(clinicId) Then Return (False, result, Nothing)
        Try
            Dim url = $"{_baseUrl}/api/whatsapp-archive/{Uri.EscapeDataString(clinicId.Trim())}"
            Dim num = If(filterNumber, "").Trim()
            If num.Length > 0 Then url &= "?number=" & Uri.EscapeDataString(num)
            Dim response = Await _httpClient.GetAsync(url)
            Dim json = Await response.Content.ReadAsStringAsync()
            If Not response.IsSuccessStatusCode Then
                Return (False, result, $"HTTP {CInt(response.StatusCode)}: {json}")
            End If
            Using doc = JsonDocument.Parse(json)
                Dim root = doc.RootElement
                Dim arr As JsonElement
                If root.ValueKind = JsonValueKind.Array Then
                    arr = root
                ElseIf root.TryGetProperty("items", arr) AndAlso arr.ValueKind = JsonValueKind.Array Then
                ElseIf root.TryGetProperty("messages", arr) AndAlso arr.ValueKind = JsonValueKind.Array Then
                ElseIf root.TryGetProperty("data", arr) AndAlso arr.ValueKind = JsonValueKind.Array Then
                Else
                    Return (True, result, Nothing)
                End If
                For Each el In arr.EnumerateArray()
                    Dim row As New WhatsAppArchiveItem()
                    TryFillArchiveItem(el, row)
                    result.Add(row)
                Next
            End Using
            Return (True, result, Nothing)
        Catch ex As Exception
            Return (False, result, ex.Message)
        End Try
    End Function

    ''' <summary>
    ''' Get failed messages (GET /api/whatsapp/failed-messages/:clinicId).
    ''' </summary>
    Public Async Function GetFailedMessagesAsync(clinicId As String) As Task(Of List(Of FailedMessageItem))
        Dim result As New List(Of FailedMessageItem)
        If String.IsNullOrWhiteSpace(clinicId) Then Return result
        Try
            Dim url = $"{_baseUrl}/api/gateway/whatsapp/failed-messages/{Uri.EscapeDataString(clinicId)}"
            Dim response = Await _httpClient.GetAsync(url)
            Dim json = Await response.Content.ReadAsStringAsync()
            If Not response.IsSuccessStatusCode Then Return result
            Using doc = JsonDocument.Parse(json)
                Dim root = doc.RootElement
                Dim failed As JsonElement
                If Not root.TryGetProperty("failedMessages", failed) Then Return result
                Dim v As JsonElement
                For Each item In failed.EnumerateArray()
                    Dim msg As New FailedMessageItem
                    If item.TryGetProperty("number", v) Then msg.Number = v.GetString()
                    If item.TryGetProperty("message", v) Then msg.Message = v.GetString()
                    If item.TryGetProperty("error", v) Then msg.[Error] = v.GetString()
                    If item.TryGetProperty("failedAt", v) Then msg.FailedAt = v.GetString()
                    result.Add(msg)
                Next
            End Using
        Catch
            ' return empty
        End Try
        Return result
    End Function

    ''' <summary>
    ''' Retry all failed messages (POST /api/whatsapp/retry-failed/:clinicId). Returns count retried or -1 on error.
    ''' </summary>
    Public Async Function RetryFailedAsync(clinicId As String) As Task(Of Integer)
        If String.IsNullOrWhiteSpace(clinicId) Then Return -1
        Try
            Dim url = $"{_baseUrl}/api/gateway/whatsapp/retry-failed/{Uri.EscapeDataString(clinicId)}"
            Dim response = Await _httpClient.PostAsync(url, New StringContent("", Encoding.UTF8, "application/json"))
            Dim json = Await response.Content.ReadAsStringAsync()
            If Not response.IsSuccessStatusCode Then Return -1
            Using doc = JsonDocument.Parse(json)
                Dim root = doc.RootElement
                Dim count As JsonElement
                If root.TryGetProperty("count", count) Then Return count.GetInt32()
            End Using
        Catch
            ' ignore
        End Try
        Return -1
    End Function

    ''' <summary>
    ''' Create customer/clinic on the WhatsApp API (POST /api/customers).
    ''' Call this when a new clinic is added locally to sync with the remote API.
    ''' Returns (Success, ErrorMessage) - ErrorMessage is Nothing on success.
    ''' </summary>
    Public Async Function CreateCustomerAsync(clsClinic As Clinic) As Task(Of (Success As Boolean, ErrorMessage As String))
        If clsClinic Is Nothing Then Return (False, "Clinic is null.")
        Try
            Dim clinicLogoValue As Object = Nothing
            If clsClinic.ClinicLogo IsNot Nothing AndAlso clsClinic.ClinicLogo.Length > 0 Then
                Dim base64 As String = Convert.ToBase64String(clsClinic.ClinicLogo)
                Dim dataUrl As String = "data:image/jpeg;base64," & base64
                If dataUrl.Length <= 2000 Then
                    clinicLogoValue = dataUrl
                End If
            End If

            Dim payload = New Dictionary(Of String, Object) From {
                {"clinicId", clsClinic.ClinicID.ToString()},
                {"clinicNameEn", If(clsClinic.ClinicNameEn, "")},
                {"clinicNameAr", If(clsClinic.ClinicNameAr, "")},
                {"drNameEn", If(clsClinic.DrNameEn, "")},
                {"drNameAr", If(clsClinic.DrNameAr, "")},
                {"specialistEn", If(clsClinic.SpecialistEn, "")},
                {"specialistAr", If(clsClinic.SpecialistAr, "")},
                {"addressEn", If(clsClinic.AddressEn, "")},
                {"addressAr", If(clsClinic.AddressAr, "")},
                {"phone", If(clsClinic.Phone, "")},
                {"mobile", If(clsClinic.Mobile, "")},
                {"email", If(clsClinic.Email, "")},
                {"clinicLogo", clinicLogoValue}
            }

            Dim jsonContent = JsonSerializer.Serialize(payload)
            Dim content = New StringContent(jsonContent, Encoding.UTF8, "application/json")
            Dim response = Await _httpClient.PostAsync($"{_baseUrl}/api/customers", content)

            If response.IsSuccessStatusCode Then
                Return (True, Nothing)
            End If

            Dim responseBody = Await response.Content.ReadAsStringAsync()
            Dim errorMsg = ParseValidationErrors(responseBody)
            Return (False, errorMsg)
        Catch ex As Exception
            Return (False, ex.Message)
        End Try
    End Function

    ''' <summary>
    ''' Update customer/clinic on the WhatsApp API (PUT /api/customers/{id}).
    ''' Call this when a clinic is updated locally to sync with the remote API.
    ''' Returns (Success, ErrorMessage) - ErrorMessage is Nothing on success.
    ''' </summary>
    Public Async Function UpdateCustomerAsync(clsClinic As Clinic) As Task(Of (Success As Boolean, ErrorMessage As String))
        If clsClinic Is Nothing Then Return (False, "Clinic is null.")
        Try
            Dim clinicLogoValue As Object = Nothing
            If clsClinic.ClinicLogo IsNot Nothing AndAlso clsClinic.ClinicLogo.Length > 0 Then
                Dim base64 As String = Convert.ToBase64String(clsClinic.ClinicLogo)
                Dim dataUrl As String = "data:image/jpeg;base64," & base64
                If dataUrl.Length <= 2000 Then
                    clinicLogoValue = dataUrl
                End If
            End If

            Dim payload = New Dictionary(Of String, Object) From {
                {"clinicNameEn", If(clsClinic.ClinicNameEn, "")},
                {"clinicNameAr", If(clsClinic.ClinicNameAr, "")},
                {"drNameEn", If(clsClinic.DrNameEn, "")},
                {"drNameAr", If(clsClinic.DrNameAr, "")},
                {"specialistEn", If(clsClinic.SpecialistEn, "")},
                {"specialistAr", If(clsClinic.SpecialistAr, "")},
                {"addressEn", If(clsClinic.AddressEn, "")},
                {"addressAr", If(clsClinic.AddressAr, "")},
                {"phone", If(clsClinic.Phone, "")},
                {"mobile", If(clsClinic.Mobile, "")},
                {"email", If(clsClinic.Email, "")},
                {"clinicLogo", clinicLogoValue}
            }

            Dim jsonContent = JsonSerializer.Serialize(payload)
            Dim content = New StringContent(jsonContent, Encoding.UTF8, "application/json")
            Dim url = $"{_baseUrl}/api/customers/{Uri.EscapeDataString(clsClinic.ClinicID.ToString())}"
            Dim response = Await _httpClient.PutAsync(url, content)

            If response.IsSuccessStatusCode Then
                Return (True, Nothing)
            End If

            Dim responseBody = Await response.Content.ReadAsStringAsync()
            Dim errorMsg = ParseValidationErrors(responseBody)
            Return (False, errorMsg)
        Catch ex As Exception
            Return (False, ex.Message)
        End Try
    End Function

    ''' <summary>
    ''' Parses RFC 9110 validation error response into a user-friendly message.
    ''' </summary>
    Private Function ParseValidationErrors(jsonBody As String) As String
        If String.IsNullOrWhiteSpace(jsonBody) Then Return "Unknown API error."
        Try
            Using doc = JsonDocument.Parse(jsonBody)
                Dim root = doc.RootElement
                Dim sb As New StringBuilder()

                Dim title As JsonElement
                If root.TryGetProperty("title", title) Then
                    sb.Append(title.GetString()).AppendLine()
                End If

                Dim errors As JsonElement
                If root.TryGetProperty("errors", errors) AndAlso errors.ValueKind = JsonValueKind.Object Then
                    For Each prop In errors.EnumerateObject()
                        sb.Append(prop.Name).Append(": ")
                        If prop.Value.ValueKind = JsonValueKind.Array Then
                            Dim first As Boolean = True
                            For Each item In prop.Value.EnumerateArray()
                                If Not first Then sb.Append("; ")
                                sb.Append(item.GetString())
                                first = False
                            Next
                        Else
                            sb.Append(prop.Value.ToString())
                        End If
                        sb.AppendLine()
                    Next
                Else
                    sb.Append(jsonBody)
                End If

                Dim result = sb.ToString().Trim()
                Return If(String.IsNullOrEmpty(result), "Unknown API error.", result)
            End Using
        Catch
            Return If(jsonBody.Length > 200, jsonBody.Substring(0, 200) & "...", jsonBody)
        End Try
    End Function

    ''' <summary>
    ''' دالة مساعدة لتحديد نوع الملف (MimeType) بناءً على امتداده
    ''' </summary>
    Private Function GetMimeType(extension As String) As String
        Select Case extension
            Case ".pdf"
                Return "application/pdf"
            Case ".png"
                Return "image/png"
            Case ".jpg", ".jpeg"
                Return "image/jpeg"
            Case ".txt"
                Return "text/plain"
            Case ".html", ".htm"
                Return "text/html"
            Case Else
                Return "application/octet-stream" ' نوع افتراضي للملفات المجهولة
        End Select
    End Function

    ''' <summary>
    ''' Clear in-memory queue fields when GET /queue no longer lists that job (empty list prunes every referenced job id).
    ''' </summary>
    Public Shared Sub PruneSessionRowsNotInGatewayQueue(rows As IList(Of WhatsAppActivityLogRow), pending As IList(Of PendingMessageItem))
        If rows Is Nothing Then Return
        Dim jobIds As New HashSet(Of String)(StringComparer.OrdinalIgnoreCase)
        If pending IsNot Nothing Then
            For Each p In pending
                If p Is Nothing OrElse String.IsNullOrWhiteSpace(p.JobId) Then Continue For
                jobIds.Add(p.JobId.Trim())
            Next
        End If
        For Each row In rows
            If row Is Nothing Then Continue For
            Dim jid = If(row.QueueJobId, "").Trim()
            If jid.Length > 0 AndAlso Not jobIds.Contains(jid) Then
                ' Keep QueueJobId for reference; only clear display fields
                row.ScheduledSendUtc = Nothing
                row.QueueDelaySeconds = Nothing
                row.QueueDelayBaselineUtc = Nothing
                row.ListedInLiveGatewayQueue = False
            End If
        Next
    End Sub

    ''' <summary>
    ''' Match session rows to GET /queue items. Each row gets its OWN baseline time (set only on FIRST match)
    ''' so multiple messages count down independently. Subsequent polls only verify the message is still in queue.
    ''' </summary>
    Public Shared Function TryEnrichRowsFromPendingQueue(rows As IList(Of WhatsAppActivityLogRow), pending As IList(Of PendingMessageItem)) As Boolean
        If rows Is Nothing Then Return False
        If pending Is Nothing Then pending = New List(Of PendingMessageItem)()
        Dim changed As Boolean = False
        Dim nowUtc = DateTime.UtcNow
        For Each row In rows
            If row Is Nothing Then Continue For
            ' Clear Listed flag; will be set True if still in queue
            Dim wasListed = row.ListedInLiveGatewayQueue
            row.ListedInLiveGatewayQueue = False
            If pending.Count = 0 Then
                ' Queue is empty - clear countdown fields if was previously listed
                If wasListed OrElse row.QueueDelayBaselineUtc.HasValue Then
                    row.QueueDelaySeconds = Nothing
                    row.QueueDelayBaselineUtc = Nothing
                    changed = True
                End If
                Continue For
            End If
            Dim pm = TryFindPendingForRow(row, pending)
            If pm Is Nothing Then
                ' Not in queue anymore - clear countdown fields
                If wasListed OrElse row.QueueDelayBaselineUtc.HasValue Then
                    row.QueueDelaySeconds = Nothing
                    row.QueueDelayBaselineUtc = Nothing
                    changed = True
                End If
                Continue For
            End If
            ' Found in queue
            If String.IsNullOrWhiteSpace(row.QueueJobId) AndAlso Not String.IsNullOrWhiteSpace(pm.JobId) Then
                row.QueueJobId = pm.JobId.Trim()
                changed = True
            End If
            If pm.DelaySeconds.HasValue Then
                row.ListedInLiveGatewayQueue = True
                ' ONLY set baseline on FIRST match - this gives each message its OWN countdown start time
                If Not row.QueueDelayBaselineUtc.HasValue Then
                    row.QueueDelaySeconds = pm.DelaySeconds.Value
                    row.QueueDelayBaselineUtc = nowUtc
                    changed = True
                End If
                ' If already has baseline, keep counting from original baseline (don't reset)
            End If
        Next
        Return changed
    End Function

    ''' <summary>Match ONLY by exact QueueJobId. No loose matching - if row has no job id or it's not in pending, return Nothing.</summary>
    Private Shared Function TryFindPendingForRow(row As WhatsAppActivityLogRow, pending As IList(Of PendingMessageItem)) As PendingMessageItem
        If row Is Nothing OrElse pending Is Nothing OrElse pending.Count = 0 Then Return Nothing
        If String.IsNullOrWhiteSpace(row.QueueJobId) Then Return Nothing
        Dim jid = row.QueueJobId.Trim()
        Return pending.FirstOrDefault(Function(p) p IsNot Nothing AndAlso Not String.IsNullOrWhiteSpace(p.JobId) AndAlso String.Equals(p.JobId.Trim(), jid, StringComparison.OrdinalIgnoreCase))
    End Function

    Private Shared Function BuildLocalOutboundEnqueueResponseJson(enq As WhatsAppOutboundUnifiedEnqueueResult) As String
        If Not String.IsNullOrWhiteSpace(enq.TerminalPriorStatus) Then
            Return JsonSerializer.Serialize(New Dictionary(Of String, Object) From {
                {"dentistXOutboundTerminalDedup", True},
                {"priorOutboundStatus", enq.TerminalPriorStatus}
            })
        End If
        Dim d As New Dictionary(Of String, Object) From {
            {"dentistXOutboundQueued", True},
            {"dentistXOutboundMessageId", enq.InsertedMessageId},
            {"dentistXOutboundLiveDuplicate", enq.IsLiveDuplicate}
        }
        If enq.CorrelationId.HasValue Then d("correlationId") = enq.CorrelationId.Value.ToString("D")
        Return JsonSerializer.Serialize(d)
    End Function

    ''' <summary>Parses synthetic JSON returned when a send was absorbed by dbo.WhatsAppOutboundMessage (see <see cref="SendMessageAsync"/>).</summary>
    Public Shared Function TryInterpretOutboundSendResponse(responseJson As String, ByRef into As WhatsAppOutboundSendInterpretation) As Boolean
        into = New WhatsAppOutboundSendInterpretation()
        If String.IsNullOrWhiteSpace(responseJson) Then Return False
        Dim raw = responseJson.Trim()
        If raw.Length > 0 AndAlso raw(0) = ChrW(&HFEFF) Then raw = raw.Substring(1).Trim()
        Dim start = raw.IndexOf("{"c)
        If start < 0 Then Return False
        raw = raw.Substring(start)
        Try
            Using doc = JsonDocument.Parse(raw)
                Dim root = doc.RootElement
                Dim el As JsonElement
                If root.TryGetProperty("dentistXOutboundTerminalDedup", el) AndAlso el.ValueKind = JsonValueKind.True Then
                    into.HadLocalOutboxSemantics = True
                    Dim ps As JsonElement
                    If root.TryGetProperty("priorOutboundStatus", ps) AndAlso ps.ValueKind = JsonValueKind.String Then
                        into.TerminalPriorStatus = If(ps.GetString(), "").Trim()
                    End If
                    Return True
                End If
                If root.TryGetProperty("dentistXOutboundQueued", el) AndAlso el.ValueKind = JsonValueKind.True Then
                    into.HadLocalOutboxSemantics = True
                    into.WasQueuedOrLiveDuplicate = True
                    Dim mid As JsonElement
                    If root.TryGetProperty("dentistXOutboundMessageId", mid) AndAlso mid.ValueKind = JsonValueKind.Number Then
                        into.OutboundMessageId = mid.GetInt64()
                    End If
                    Dim ld As JsonElement
                    If root.TryGetProperty("dentistXOutboundLiveDuplicate", ld) AndAlso ld.ValueKind = JsonValueKind.True Then
                        into.LiveDuplicate = True
                    End If
                    Return True
                End If
            End Using
        Catch
        End Try
        Return False
    End Function

End Class

''' <summary>Populated when <see cref="WhatsAppService.TryInterpretOutboundSendResponse"/> recognises local-outbox handshake JSON.</summary>
Public Structure WhatsAppOutboundSendInterpretation
    Public HadLocalOutboxSemantics As Boolean
    Public WasQueuedOrLiveDuplicate As Boolean
    Public TerminalPriorStatus As String
    Public OutboundMessageId As Long
    Public LiveDuplicate As Boolean
End Structure

''' <summary>Item from GET /api/whatsapp/queue/:clinicId</summary>
Public Class PendingMessageItem
    Public Property JobId As String
    Public Property TargetNumber As String
    Public Property MessageSnippet As String
    Public Property ScheduledAt As String
    Public Property JobState As String
    ''' <summary>Seconds until send from last GET /queue snapshot; drives message-center countdown when present.</summary>
    Public Property DelaySeconds As Integer?
End Class

''' <summary>Item from GET /api/whatsapp/failed-messages/:clinicId</summary>
Public Class FailedMessageItem
    Public Property Number As String
    Public Property Message As String
    Public Property [Error] As String
    Public Property FailedAt As String
End Class
