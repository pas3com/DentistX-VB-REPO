Imports System.Text
Imports System.Windows.Forms

''' <summary>Shows a modal when a queued reminder is skipped because <see cref="AppointmentC"/> no longer has that appointment.</summary>
Public NotInheritable Class ApptReminderAppointmentMissingNotifier
    Private Sub New()
    End Sub

    ''' <summary>Builds a readable block from stored queue previews (24h and short-lead).</summary>
    Public Shared Function FormatQueueRowPreviewsForDialog(row As ApptTwoHourReminderQueueRow) As String
        If row Is Nothing Then Return ""
        Dim sb As New StringBuilder()
        Dim p24 = If(row.MessagePreview24h, "").Trim()
        Dim p2 = If(row.MessagePreview2h, "").Trim()
        If Eng Then
            If p24.Length > 0 Then
                sb.AppendLine("— ~24-hour reminder —")
                sb.AppendLine(p24)
                sb.AppendLine()
            End If
            If p2.Length > 0 Then
                sb.AppendLine("— Short-lead reminder —")
                sb.AppendLine(p2)
            End If
        Else
            If p24.Length > 0 Then
                sb.AppendLine("— تذكير قبل نحو 24 ساعة —")
                sb.AppendLine(p24)
                sb.AppendLine()
            End If
            If p2.Length > 0 Then
                sb.AppendLine("— تذكير قبل بدء الموعد —")
                sb.AppendLine(p2)
            End If
        End If
        If sb.Length = 0 Then
            Return If(Eng, "(No reminder text was stored on the queue row.)", "(لم يُحفظ نص التذكير في الطابور.)")
        End If
        Return sb.ToString().Trim()
    End Function

    ''' <param name="intendedMessageBody">Full text intended for WhatsApp (one leg or combined previews).</param>
    ''' <param name="legDescription">Optional e.g. 24-hour vs short-lead (English or Arabic).</param>
    Public Shared Sub Show(appointmentId As Integer, patientName As String, drName As String, snapshot As DateTime,
                           intendedMessageBody As String, Optional legDescription As String = Nothing)
        Dim snapTxt = snapshot.ToString("dd/MM/yyyy HH:mm", Globalization.CultureInfo.InvariantCulture)
        Dim title = If(Eng, "WhatsApp reminder not sent", "لم يُرسل تذكير واتساب")
        Dim intro = If(Eng,
            "This appointment is no longer in the database. It may have been deleted or merged. The reminder was not sent.",
            "هذا الموعد لم يعد موجودًا في قاعدة البيانات (قد يكون محذوفًا أو مدمجًا). لم يُرسل التذكير.")
        Dim sb As New StringBuilder()
        sb.AppendLine(intro)
        sb.AppendLine()
        If Not String.IsNullOrWhiteSpace(legDescription) Then
            sb.AppendLine(legDescription)
            sb.AppendLine()
        End If
        sb.AppendLine(If(Eng, "Appointment ID:", "رقم الموعد:") & " " & appointmentId.ToString())
        If Not String.IsNullOrWhiteSpace(patientName) Then sb.AppendLine(If(Eng, "Patient:", "المريض:") & " " & patientName.Trim())
        If Not String.IsNullOrWhiteSpace(drName) Then sb.AppendLine(If(Eng, "Doctor:", "الطبيب:") & " " & drName.Trim())
        sb.AppendLine(If(Eng, "Saved start (from reminder queue):", "وقت البدء المحفوظ (من طابور التذكير):") & " " & snapTxt)
        sb.AppendLine()
        sb.AppendLine(If(Eng, "Message that would have been sent:", "الرسالة التي كانت ستُرسل:"))
        sb.AppendLine()
        sb.AppendLine(If(String.IsNullOrWhiteSpace(intendedMessageBody),
            If(Eng, "(empty)", "(فارغ)"),
            intendedMessageBody.Trim()))
        Dim text = sb.ToString()
        RunOnWinFormsUiThread(Sub() MessageBox.Show(text, title, MessageBoxButtons.OK, MessageBoxIcon.Information))
    End Sub

    Private Shared Sub RunOnWinFormsUiThread(act As Action)
        If act Is Nothing Then Return
        For Each frm As Form In Application.OpenForms
            If frm Is Nothing OrElse frm.IsDisposed OrElse Not frm.IsHandleCreated Then Continue For
            Try
                If frm.InvokeRequired Then
                    frm.Invoke(act)
                Else
                    act()
                End If
                Return
            Catch
            End Try
        Next
        Try
            act()
        Catch
        End Try
    End Sub
End Class
