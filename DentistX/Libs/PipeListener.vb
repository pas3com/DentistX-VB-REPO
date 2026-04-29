Imports System.IO.Pipes
Imports System.IO
Imports System.Threading.Tasks
Imports Microsoft.Office.Interop

Module PipeListener
    Public Sub StartPipeListener()
        Task.Run(Sub()
                     While True
                         Try
                             Using client As New NamedPipeClientStream(".", "DentistX_pipe", PipeDirection.In)
                                 client.Connect()
                                 Using reader As New StreamReader(client)
                                     Dim msg = reader.ReadLine()
                                     If msg = "LOAD" Then
                                         MsgBox("PowerPoint requested to LOAD images.")

                                         ' Ask user to select folder
                                         Using fbd As New FolderBrowserDialog()
                                             fbd.Description = "Select a folder to load into PowerPoint"
                                             If fbd.ShowDialog() = DialogResult.OK Then
                                                 Dim selectedPath = fbd.SelectedPath

                                                 ' Write path to shared instruction file
                                                 Dim sharedFile As String = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "DentistX\ppt_instruction.txt")
                                                 Directory.CreateDirectory(Path.GetDirectoryName(sharedFile))
                                                 File.WriteAllText(sharedFile, selectedPath)

                                                 ' Launch PowerPoint
                                                 Dim pptApp As New PowerPoint.Application
                                                 pptApp.Visible = Microsoft.Office.Core.MsoTriState.msoTrue
                                                 pptApp.Presentations.Add()
                                             End If
                                         End Using

                                     ElseIf msg.StartsWith("SAVE|") Then
                                         Dim folder = msg.Split("|"c)(1)
                                         MsgBox("PowerPoint wants to save images to: " & folder)

                                         '' Call your logic to export images
                                         'SaveImagesFromDentistX(folder)
                                     End If
                                 End Using
                             End Using
                         Catch ex As Exception
                             ' Optional: log or retry delay
                             Threading.Thread.Sleep(500)
                         End Try
                     End While
                 End Sub)
    End Sub

End Module
