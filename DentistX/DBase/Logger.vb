Imports System.IO
Imports DevExpress.XtraEditors

Public Class Logger
    Private _memoEdit As MemoEdit
    Private _logToFile As Boolean = True
    Private _lineCounter As Integer = 0
    Private _syncObject As New Object()

    Public Sub New(Optional memoEdit As MemoEdit = Nothing, Optional logToFile As Boolean = True)
        _memoEdit = memoEdit
        _logToFile = logToFile
    End Sub

    Public Sub Log(message As String)
        If _logToFile Then
            LogToFile(message)
        End If

        If _memoEdit IsNot Nothing Then
            LogToMemoEdit(message)
        End If
    End Sub

    Public Sub ResetLineCounter()
        SyncLock _syncObject
            _lineCounter = 0
        End SyncLock
    End Sub

    Private Sub LogToFile(message As String)
        Dim logDirectory As String = Path.Combine(Application.StartupPath, "DentistXLogs")
        Dim datePart As String = DateTime.Now.ToString("dd-MM-yyyy")
        Dim logFilePath As String = Path.Combine(logDirectory, $"DbaseMig_{datePart}.txt")

        Try
            Directory.CreateDirectory(logDirectory)
            Using writer As New StreamWriter(logFilePath, append:=True)
                SyncLock _syncObject
                    _lineCounter += 1
                    writer.WriteLine($"{_lineCounter:D4} - {DateTime.Now:dd-MM-yyyy HH:mm:ss}: {message}")
                End SyncLock
            End Using
        Catch ex As Exception
            ' Handle error silently
        End Try
    End Sub

    Private Sub LogToMemoEdit(message As String)
        If _memoEdit.InvokeRequired Then
            _memoEdit.Invoke(Sub() LogToMemoEdit(message))
        Else
            SyncLock _syncObject
                _lineCounter += 1
                Dim logEntry As String = $"{_lineCounter:D4} - {DateTime.Now:dd-MM-yyyy HH:mm:ss}: {message}" & Environment.NewLine
                _memoEdit.Text += logEntry
                _memoEdit.SelectionStart = _memoEdit.Text.Length
                _memoEdit.ScrollToCaret()
            End SyncLock

            ' Optional: Limit the number of lines to prevent memory issues
            LimitMemoEditLines()
        End If
    End Sub

    Private Sub LimitMemoEditLines(Optional maxLines As Integer = 10000)
        Dim lines As String() = _memoEdit.Text.Split(New String() {Environment.NewLine}, StringSplitOptions.None)
        If lines.Length > maxLines Then
            ' Keep the line counter consistent
            Dim remainingLines = lines.Skip(lines.Length - maxLines).ToArray()
            SyncLock _syncObject
                _lineCounter = remainingLines.Length
                _memoEdit.Text = String.Join(Environment.NewLine, remainingLines) & Environment.NewLine
            End SyncLock
        End If
    End Sub

    ' Property to get current line count
    Public ReadOnly Property CurrentLineCount As Integer
        Get
            SyncLock _syncObject
                Return _lineCounter
            End SyncLock
        End Get
    End Property
End Class



'Public Class Logger
'    Private _memoEdit As MemoEdit
'    Private _logToFile As Boolean = True

'    Public Sub New(Optional memoEdit As MemoEdit = Nothing, Optional logToFile As Boolean = True)
'        _memoEdit = memoEdit
'        _logToFile = logToFile
'    End Sub

'    Public Sub Log(message As String)
'        If _logToFile Then
'            LogToFile(message)
'        End If

'        If _memoEdit IsNot Nothing Then
'            LogToMemoEdit(message)
'        End If
'    End Sub

'    Private Sub LogToFile(message As String)
'        ' Your existing file logging code
'        Dim logDirectory As String = Path.Combine(Application.StartupPath, "DentistXLogs")
'        Dim datePart As String = DateTime.Now.ToString("dd-MM-yyyy")
'        Dim logFilePath As String = Path.Combine(logDirectory, $"DbaseMig_{datePart}.txt")

'        Try
'            Directory.CreateDirectory(logDirectory)
'            Using writer As New StreamWriter(logFilePath, append:=True)
'                writer.WriteLine($"{DateTime.Now:dd-MM-yyyy HH:mm:ss}: {message}")
'            End Using
'        Catch ex As Exception
'            ' Handle error silently or show message
'        End Try
'    End Sub

'    Private Sub LogToMemoEdit(message As String)
'        If _memoEdit.InvokeRequired Then
'            _memoEdit.Invoke(Sub() LogToMemoEdit(message))
'        Else
'            Dim logEntry As String = $"{DateTime.Now:dd-MM-yyyy HH:mm:ss}: {message}" & Environment.NewLine
'            _memoEdit.Text += logEntry
'            _memoEdit.SelectionStart = _memoEdit.Text.Length
'            _memoEdit.ScrollToCaret()

'            ' Optional: Limit the number of lines to prevent memory issues
'            LimitMemoEditLines()
'        End If
'    End Sub

'    Private Sub LimitMemoEditLines(Optional maxLines As Integer = 1000)
'        Dim lines As String() = _memoEdit.Text.Split(Environment.NewLine)
'        If lines.Length > maxLines Then
'            _memoEdit.Text = String.Join(Environment.NewLine, lines.Skip(lines.Length - maxLines))
'        End If
'    End Sub
'End Class
