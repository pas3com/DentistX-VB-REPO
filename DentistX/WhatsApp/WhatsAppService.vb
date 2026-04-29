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
    ''' Call before manual sends (e.g. btnWhatsSend): verifies a clinic exists and the gateway reports WhatsApp as connected.
    ''' If disconnected, opens <see cref="WhatsAppForm"/> for QR/connect, then re-checks. Returns False if still not connected.
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
            If Await wa.GetConnectionStatusAsync(clinicId).ConfigureAwait(True) Then Return True
        Catch
        End Try
        ShowWhatsAppConnectionForm(owner)
        Try
            Dim wa2 As New WhatsAppService()
            If Await wa2.GetConnectionStatusAsync(clinicId).ConfigureAwait(True) Then Return True
        Catch
        End Try
        MessageBox.Show(owner,
                        If(Eng, "WhatsApp is still not connected. Use the Connection tab to scan the QR code, then try sending again.",
                           "واتساب لا يزال غير متصل. استخدم تبويب الاتصال لمسح رمز الاستجابة ثم أعد محاولة الإرسال."),
                        If(Eng, "WhatsApp", "واتساب"), MessageBoxButtons.OK, MessageBoxIcon.Warning)
        Return False
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
                Return root.TryGetProperty("success", success) AndAlso success.GetBoolean()
            End Using
        Catch
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Get connection status (GET /api/whatsapp/status/:clinicId). Returns True if connected.
    ''' </summary>
    Public Async Function GetConnectionStatusAsync(clinicId As String) As Task(Of Boolean)
        If String.IsNullOrWhiteSpace(clinicId) Then Return False
        Try
            Dim url = $"{_baseUrl}/api/gateway/whatsapp/status/{Uri.EscapeDataString(clinicId)}"
            Dim response = Await _httpClient.GetAsync(url)
            Dim json = Await response.Content.ReadAsStringAsync()
            If Not response.IsSuccessStatusCode Then Return False
            Using doc = JsonDocument.Parse(json)
                Dim root = doc.RootElement
                Dim connected As JsonElement
                If root.TryGetProperty("connected", connected) Then Return connected.GetBoolean()
            End Using
        Catch
            ' ignore
        End Try
        Return False
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

End Class

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
