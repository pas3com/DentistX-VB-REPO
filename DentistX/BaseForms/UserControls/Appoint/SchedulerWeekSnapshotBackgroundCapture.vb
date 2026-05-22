Imports System
Imports System.Drawing
Imports System.Drawing.Imaging
Imports System.IO
Imports System.Windows.Forms

''' <summary>
''' Renders the same 7-day (Sat-Fri) week snapshot as the live appointments week view without the user opening the scheduler,
''' by hosting <see cref="ApptHostCtl"/> on a hidden off-screen form on the UI thread.
''' </summary>
Public NotInheritable Class SchedulerWeekSnapshotBackgroundCapture
    Private Sub New()
    End Sub

    ''' <summary>Same rule as <see cref="SchedulerNew"/> week view: week starts Saturday.</summary>
    Private Shared Function GetWeekStartSaturday(dateValue As Date) As Date
        Dim currentDayOfWeek As Integer = CInt(dateValue.DayOfWeek)
        Dim daysToSaturday As Integer = (currentDayOfWeek - 6 + 7) Mod 7
        Return dateValue.Date.AddDays(-daysToSaturday)
    End Function

    ''' <summary>File name: Snapshot-{firstDay_yyyyMMdd}-{lastDay_yyyyMMdd}.png (Sat–Fri inclusive).</summary>
    Private Shared Function BuildSnapshotFileNameForSatFriWeek(weekStartSaturday As Date) As String
        Dim weekEndFriday = weekStartSaturday.Date.AddDays(6)
        Return $"Snapshot-{weekStartSaturday:yyyyMMdd}-{weekEndFriday:yyyyMMdd}.png"
    End Function

    ''' <summary>
    ''' Saves two PNGs under Attachments\ClinicUse. Names reflect the real Sat–Fri period each (e.g. 11–17 Apr 2026 → Snapshot-20260411-20260417.png).
    ''' Uses the same full-height week capture as interactive <see cref="SchedulerNew"/> (no scrolling).
    ''' Must be called on the UI thread (STA).
    ''' </summary>
    Public Shared Function TrySaveCurrentAndNextWeekPngs(anchorDate As Date,
                                                       ByRef captionCurrent As String,
                                                       ByRef captionNext As String,
                                                       ByRef pathCurrent As String,
                                                       ByRef pathNext As String) As Boolean
        Dim h1 As String = Nothing
        Dim h2 As String = Nothing
        Return TrySaveCurrentAndNextWeekMedia(anchorDate, SchedulerSnapshotAutoSendRepository.SendContentMode.PngOnly,
            captionCurrent, captionNext, pathCurrent, pathNext, h1, h2)
    End Function

    ''' <summary>
    ''' Produces up to two week PNGs and/or two HTML files under Attachments\ClinicUse, per <paramref name="mode"/>.
    ''' When <paramref name="useScheduledWeekRule"/> is True (always used by <see cref="SchedulerSnapshotAutoSendService.RunJobAsync"/>):
    ''' if <paramref name="anchorDate"/> is Thursday, capture shifts forward (next Sat–Fri week plus the one after), matching live scheduler behaviour.
    ''' When False, anchor Thursday uses the current Sat–Fri week plus the next only — pass False only for callers that need “calendar” weeks instead.
    ''' HTML is written while each week is still the active view (required for a correct export).
    ''' </summary>
    Public Shared Function TrySaveCurrentAndNextWeekMedia(anchorDate As Date,
                                                        mode As SchedulerSnapshotAutoSendRepository.SendContentMode,
                                                        ByRef captionCurrent As String,
                                                        ByRef captionNext As String,
                                                        ByRef pathPngCurrent As String,
                                                        ByRef pathPngNext As String,
                                                        ByRef pathHtmlCurrent As String,
                                                        ByRef pathHtmlNext As String,
                                                        Optional useScheduledWeekRule As Boolean = True) As Boolean
        captionCurrent = Nothing
        captionNext = Nothing
        pathPngCurrent = Nothing
        pathPngNext = Nothing
        pathHtmlCurrent = Nothing
        pathHtmlNext = Nothing

        Dim wantPng = (mode = SchedulerSnapshotAutoSendRepository.SendContentMode.PngOnly OrElse
            mode = SchedulerSnapshotAutoSendRepository.SendContentMode.PngAndHtml)
        Dim wantHtml = (mode = SchedulerSnapshotAutoSendRepository.SendContentMode.HtmlOnly OrElse
            mode = SchedulerSnapshotAutoSendRepository.SendContentMode.PngAndHtml)

        Dim exportsDir = Path.Combine(Application.StartupPath, "Attachments", "ClinicUse")
        Try
            Directory.CreateDirectory(exportsDir)
        Catch
            Return False
        End Try

        Dim baseWeekSat = GetWeekStartSaturday(anchorDate.Date)
        Dim firstWeekOffset = If(useScheduledWeekRule AndAlso anchorDate.Date.DayOfWeek = DayOfWeek.Thursday, 1, 0)
        Dim secondWeekOffset = firstWeekOffset + 1
        Dim firstWeekSat = baseWeekSat.AddDays(firstWeekOffset * 7)
        Dim secondWeekSat = baseWeekSat.AddDays(secondWeekOffset * 7)
        pathPngCurrent = Path.Combine(exportsDir, BuildSnapshotFileNameForSatFriWeek(firstWeekSat))
        pathPngNext = Path.Combine(exportsDir, BuildSnapshotFileNameForSatFriWeek(secondWeekSat))

        Dim wa = Screen.PrimaryScreen.WorkingArea
        Using host As New Form With {
            .ShowInTaskbar = False,
            .FormBorderStyle = FormBorderStyle.None,
            .StartPosition = FormStartPosition.Manual,
            .Bounds = New Rectangle(-20000, -20000, wa.Width, wa.Height),
            .Opacity = 0R
        }
            Dim sched As New ApptHostCtl()
            sched.Dock = DockStyle.Fill
            host.Controls.Add(sched)
            host.Show()
            Application.DoEvents()
            host.PerformLayout()
            sched.PerformLayout()
            Application.DoEvents()

            Dim cap1 As String = Nothing
            Dim cap2 As String = Nothing
            Dim h1 As String = Nothing
            Dim h2 As String = Nothing
            Dim bmp1 As Bitmap = Nothing
            Dim bmp2 As Bitmap = Nothing
            Try
                bmp1 = sched.TryCaptureWeekSnapshotBitmap(anchorDate, firstWeekOffset, clearFiltersForBroadcast:=True, weekCaptionOut:=cap1,
                    alsoWritePng:=wantPng, alsoWriteHtml:=wantHtml, htmlExportDir:=exportsDir, weekHtmlFilePathOut:=h1)
                bmp2 = sched.TryCaptureWeekSnapshotBitmap(anchorDate, secondWeekOffset, clearFiltersForBroadcast:=True, weekCaptionOut:=cap2,
                    alsoWritePng:=wantPng, alsoWriteHtml:=wantHtml, htmlExportDir:=exportsDir, weekHtmlFilePathOut:=h2)
            Finally
                Try
                    host.Hide()
                Catch
                End Try
            End Try

            captionCurrent = cap1
            captionNext = cap2
            If wantHtml Then
                pathHtmlCurrent = h1
                pathHtmlNext = h2
            End If

            Dim pngOk1 = True
            Dim pngOk2 = True
            If wantPng Then
                pngOk1 = bmp1 IsNot Nothing AndAlso SaveBmp(pathPngCurrent, bmp1)
                pngOk2 = bmp2 IsNot Nothing AndAlso SaveBmp(pathPngNext, bmp2)
            End If
            If bmp1 IsNot Nothing Then bmp1.Dispose()
            If bmp2 IsNot Nothing Then bmp2.Dispose()

            Dim htmlOk1 = True
            Dim htmlOk2 = True
            If wantHtml Then
                htmlOk1 = Not String.IsNullOrWhiteSpace(h1) AndAlso File.Exists(h1)
                htmlOk2 = Not String.IsNullOrWhiteSpace(h2) AndAlso File.Exists(h2)
            End If

            Select Case mode
                Case SchedulerSnapshotAutoSendRepository.SendContentMode.PngOnly
                    Return pngOk1 AndAlso pngOk2
                Case SchedulerSnapshotAutoSendRepository.SendContentMode.HtmlOnly
                    Return htmlOk1 AndAlso htmlOk2
                Case Else
                    Return pngOk1 AndAlso pngOk2 AndAlso htmlOk1 AndAlso htmlOk2
            End Select
        End Using
    End Function

    Private Shared Function SaveBmp(path As String, bmp As Bitmap) As Boolean
        Try
            bmp.Save(path, ImageFormat.Png)
            Return True
        Catch
            Try
                If File.Exists(path) Then File.Delete(path)
            Catch
            End Try
            Return False
        End Try
    End Function
End Class
