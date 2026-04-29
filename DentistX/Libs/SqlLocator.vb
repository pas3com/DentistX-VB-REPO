Imports System.Text
Imports System.Windows.Forms
Imports System.Runtime.InteropServices



Public Class SqlLocator
        <DllImport("odbc32.dll")>
        Private Shared Function SQLAllocHandle(ByVal hType As Short, ByVal inputHandle As IntPtr, ByRef outputHandle As IntPtr) As Short
        End Function
        <DllImport("odbc32.dll")>
        Private Shared Function SQLSetEnvAttr(ByVal henv As IntPtr, ByVal attribute As Integer, ByVal valuePtr As IntPtr, ByVal strLength As Integer) As Short
        End Function
        <DllImport("odbc32.dll")>
        Private Shared Function SQLFreeHandle(ByVal hType As Short, ByVal handle As IntPtr) As Short
        End Function
        <DllImport("odbc32.dll", CharSet:=CharSet.Ansi)>
        Private Shared Function SQLBrowseConnect(ByVal hconn As IntPtr, ByVal inString As StringBuilder, ByVal inStringLength As Short, ByVal outString As StringBuilder, ByVal outStringLength As Short, ByRef outLengthNeeded As Short) As Short
        End Function

        Private Const SQL_HANDLE_ENV As Short = 1
        Private Const SQL_HANDLE_DBC As Short = 2
        Private Const SQL_ATTR_ODBC_VERSION As Integer = 200
        Private Const SQL_OV_ODBC3 As Integer = 3
        Private Const SQL_SUCCESS As Short = 0

        Private Const SQL_NEED_DATA As Short = 99
        Private Const DEFAULT_RESULT_SIZE As Short = 1024
        Private Const SQL_DRIVER_STR As String = "DRIVER=SQL SERVER"

        Private Sub New()
        End Sub


    Public Shared Function GetServers() As String()
        Dim retval() As String = Nothing
        Dim txt As String = String.Empty
        Dim henv As IntPtr = IntPtr.Zero
        Dim hconn As IntPtr = IntPtr.Zero
        Dim inString As New StringBuilder(SQL_DRIVER_STR)
        Dim outString As New StringBuilder(DEFAULT_RESULT_SIZE)
        Dim inStringLength As Short = CShort(inString.Length)
        Dim lenNeeded As Short = 0

        Try
            If SQL_SUCCESS = SQLAllocHandle(SQL_HANDLE_ENV, henv, henv) Then
                If SQL_SUCCESS = SQLSetEnvAttr(henv, SQL_ATTR_ODBC_VERSION, New IntPtr(SQL_OV_ODBC3), 0) Then
                    If SQL_SUCCESS = SQLAllocHandle(SQL_HANDLE_DBC, henv, hconn) Then
                        If SQL_NEED_DATA = SQLBrowseConnect(hconn, inString, inStringLength, outString, DEFAULT_RESULT_SIZE, lenNeeded) Then
                            If DEFAULT_RESULT_SIZE < lenNeeded Then
                                outString.Capacity = lenNeeded
                                If SQL_NEED_DATA <> SQLBrowseConnect(hconn, inString, inStringLength, outString, lenNeeded, lenNeeded) Then
                                    Throw New ApplicationException("Unabled to aquire SQL Servers from ODBC driver.")
                                End If
                            End If
                            txt = outString.ToString()
                            Dim start As Integer = txt.IndexOf("{") + 1
                            Dim len As Integer = txt.IndexOf("}") - start
                            If (start > 0) AndAlso (len > 0) Then
                                txt = txt.Substring(start, len)
                            Else
                                txt = String.Empty
                            End If
                        End If
                    End If
                End If
            End If
        Catch ex As Exception
            'Throw away any error if we are not in debug mode
#If (DEBUG) Then
            MessageBox.Show(ex.Message, "Acquire SQL Servier List Error")
#End If
            txt = String.Empty
        Finally
            If hconn <> IntPtr.Zero Then
                SQLFreeHandle(SQL_HANDLE_DBC, hconn)
            End If
            If henv <> IntPtr.Zero Then
                SQLFreeHandle(SQL_HANDLE_ENV, hconn)
            End If
        End Try

        If txt.Length > 0 Then
            retval = txt.Split(",".ToCharArray())
        End If

        Return retval
    End Function


    Friend Shared Function IsLocalDBInstalled() As Boolean
        ' Start the child process.
        Dim p As New Process()
        ' Redirect the output stream of the child process.
        p.StartInfo.UseShellExecute = False
        p.StartInfo.RedirectStandardOutput = True
        p.StartInfo.FileName = "cmd.exe"
        p.StartInfo.Arguments = "/C sqllocaldb info"
        p.StartInfo.CreateNoWindow = True
        p.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden
        p.Start()
        ' Do not wait for the child process to exit before
        ' reading to the end of its redirected stream.
        ' p.WaitForExit();
        ' Read the output stream first and then wait.
        Dim sOutput As String = p.StandardOutput.ReadToEnd()
        p.WaitForExit()

        'If LocalDb is not installed then it will return that 'sqllocaldb' is not recognized as an internal or external command operable program or batch file.
        If sOutput Is Nothing OrElse sOutput.Trim().Length = 0 OrElse sOutput.Contains("not recognized") Then
            Return False
        End If
        If sOutput.ToLower().Contains("mssqllocaldb") Then 'This is a defualt instance in local DB
            Return True
        End If
        Return False
    End Function

    Friend Shared Function LocalDB_Name() As String

        ' Start the child process.
        Dim p As New Process()
        ' Redirect the output stream of the child process.
        p.StartInfo.UseShellExecute = False
        p.StartInfo.RedirectStandardOutput = True
        p.StartInfo.FileName = "cmd.exe"
        p.StartInfo.Arguments = "/C sqllocaldb info"
        p.StartInfo.CreateNoWindow = True
        p.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden
        p.Start()
        ' Do not wait for the child process to exit before
        ' reading to the end of its redirected stream.
        ' p.WaitForExit();
        ' Read the output stream first and then wait.
        Dim sOutput As String = p.StandardOutput.ReadToEnd()
        p.WaitForExit()

        'If LocalDb is not installed then it will return that 'sqllocaldb' is not recognized as an internal or external command operable program or batch file.
        If sOutput Is Nothing OrElse sOutput.Trim().Length = 0 OrElse sOutput.Contains("not recognized") Then
            Return sOutput
        End If
        If sOutput.ToLower().Contains("mssqllocaldb") Then 'This is a defualt instance in local DB
            Return sOutput
        End If
        Return sOutput
    End Function


End Class

