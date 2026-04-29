Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports Dapper
Imports System.Data.SqlClient
Imports System.Collections
Imports System.Configuration
Imports System.Windows.Forms
Imports Microsoft.Office.Interop

Public Class DentistXDATA

    Private ConnectionString As String

    Public Function GetOpenConnection() As SqlConnection
        Dim connectionString As String = DentistX.My.Settings.DentistXConnectionString ' "Data Source=.;Initial Catalog=DentistX;Integrated Security=true;"
        Dim connection As New SqlConnection(connectionString)
        connection.Open()
        Return connection
    End Function

    ''' <summary>Connection string from config after RefreshSection (startup may rewrite connectionStrings; My.Settings alone can stay stale).</summary>
    Public Shared Function GetEffectiveConnectionString() As String
        Try
            ConfigurationManager.RefreshSection("connectionStrings")
            Dim cs = ConfigurationManager.ConnectionStrings("DentistX.My.MySettings.DentistXConnectionString")
            If cs IsNot Nothing AndAlso Not String.IsNullOrWhiteSpace(cs.ConnectionString) Then
                Return cs.ConnectionString
            End If
        Catch
        End Try
        Return DentistX.My.Settings.DentistXConnectionString
    End Function

    Public Shared Function GetConnection() As SqlConnection
        Return New SqlConnection(GetEffectiveConnectionString())
    End Function
    Public Function GetConnectionString() As String
        Dim connectionString As String = DentistX.My.Settings.DentistXConnectionString ' "Data Source=.;Initial Catalog=DentistX;Integrated Security=true;"
        Return connectionString
    End Function

    Public Function GetIdent_CurrentSql(ByVal Table As String) As Integer
        Dim query As String
        Dim connection As SqlConnection = GetConnection()
        Dim command As SqlCommand = New SqlCommand()
        Dim reader As SqlDataReader
        Dim returnValue As Integer = 0
        query = "SELECT IDENT_CURRENT('" & Table & "')"
        command = New SqlCommand(query, connection)
        command.CommandType = CommandType.Text
        Try
            connection.Open()
            reader = command.ExecuteReader()
            If reader.HasRows Then
                While reader.Read()
                    returnValue = System.Convert.ToInt32(reader.GetValue(0))
                End While
            End If
            reader.Close()
            connection.Close()
        Catch ex As SqlException
            MessageBox.Show(ex.Message, ex.GetType.ToString)
        Finally
            connection.Close()
        End Try
        Return returnValue
    End Function
    Public Function GetIdent_IncrSql(ByVal Table As String) As Integer
        Dim query As String
        Dim connection As SqlConnection = GetConnection()
        Dim command As SqlCommand = New SqlCommand()
        Dim reader As SqlDataReader
        Dim returnValue As Integer = 0
        query = "SELECT IDENT_INCR('" & Table & "')"
        command = New SqlCommand(query, connection)
        command.CommandType = CommandType.Text
        Try
            connection.Open()
            reader = command.ExecuteReader()
            If reader.HasRows Then
                While reader.Read()
                    returnValue = System.Convert.ToInt32(reader.GetValue(0))
                End While
            End If
            reader.Close()
            connection.Close()
        Catch ex As SqlException
            MessageBox.Show(ex.Message, ex.GetType.ToString)
        Finally
            connection.Close()
        End Try
        Return returnValue
    End Function
    Public Function getAutoIDSql(ByVal Mode As String, ByVal Table As String) As Integer
        Dim Ident_Current As Integer = GetIdent_Current(Table)
        If Mode = "Last" Then
            Return Ident_Current
        ElseIf Mode = "New" Then
            Return Ident_Current + GetIdent_Incr(Table)
        End If
        Return Nothing
    End Function


    '---------------------------
    ' 1.  IDENT_CURRENT
    '---------------------------
    Public Shared Function GetIdent_Current(ByVal Table As String) As Integer
        'Const template As String = "SELECT CAST(IDENT_CURRENT({0}) AS int);"

        ''QUOTENAME()‑style escaping to avoid SQL‑injection on the table name
        'Dim sql As String = String.Format(template, "[" & Table.Replace("]", "]]") & "]")
        Dim sql As String = $"SELECT CAST(IDENT_CURRENT('{Table.Replace("'", "''")}') AS int);"

        Using cn As SqlConnection = GetConnection()
            Return cn.ExecuteScalar(Of Integer)(sql)
        End Using
    End Function

    '---------------------------
    ' 2.  IDENT_INCR
    '---------------------------
    Public Shared Function GetIdent_Incr(ByVal Table As String) As Integer
        'Const template As String = "SELECT CAST(IDENT_INCR({0}) AS int);"
        'Dim sql As String = String.Format(template, "[" & Table.Replace("]", "]]") & "]")
        Dim sql As String = $"SELECT CAST(IDENT_INCR('{Table.Replace("'", "''")}') AS int);"

        Using cn As SqlConnection = GetConnection()
            Return cn.ExecuteScalar(Of Integer)(sql)
        End Using
    End Function

    '---------------------------
    ' 3.  Convenience wrapper
    '---------------------------
    Public Shared Function getAutoID(ByVal Mode As String, ByVal Table As String) As Integer
        Dim identCurrent As Integer = GetIdent_Current(Table)

        Select Case Mode
            Case "Last" : Return identCurrent
            Case "New" : Return identCurrent + GetIdent_Incr(Table)
            Case Else : Return Nothing
        End Select
    End Function



    Public Sub ExportToExcel(ByVal Type As String, ByVal Head As String, ByVal Grid As DataGridView)
        Try
            If (Grid.ColumnCount > 0) Then
                Dim oExcel As Excel.Application
                Dim oBook As Excel.Workbook
                Dim oSheet As Excel.Worksheet
                oExcel = CType(CreateObject("Excel.Application"), Excel.Application)
                oExcel.Visible = False
                oBook = oExcel.Workbooks.Add
                oSheet = CType(oBook.Worksheets(1), Excel.Worksheet)
                oSheet.Range("B2").Value = Head
                oSheet.Range("B2").Font.Bold = True

                If Type = "One" Then
                    For s As Integer = 0 To Grid.ColumnCount - 1
                        oSheet.Range("B" & 4 + s).Value = Grid.Columns(s).HeaderText
                        oSheet.Range("C" & 4 + s).Value = Grid.SelectedRows.Item(0).Cells.Item(s).Value
                    Next
                    oSheet.Columns.AutoFit()
                ElseIf Type = "Many" Then
                    Dim DataArrayHead(0, 0 To Grid.ColumnCount - 1) As Object
                    For s As Integer = 0 To Grid.ColumnCount - 1
                        DataArrayHead(0, s) = Grid.Columns(s).HeaderText
                    Next
                    oSheet.Range("B4").Resize(1, Grid.ColumnCount).Value = DataArrayHead
                    oSheet.Range("B4").Resize(1, Grid.ColumnCount).Font.Bold = True

                    Dim DataArray(0 To Grid.RowCount - 1, 0 To Grid.ColumnCount - 1) As Object
                    For r As Integer = 0 To Grid.RowCount - 1
                        For s As Integer = 0 To Grid.ColumnCount - 1
                            DataArray(r, s) = Grid.Rows.Item(r).Cells.Item(s).Value
                        Next
                    Next
                    oSheet.Range("B5").Resize(Grid.RowCount, Grid.ColumnCount).Value = DataArray
                    oSheet.Columns.AutoFit()
                End If
            End If
        Catch
            ' Handle exception
        End Try
    End Sub
End Class
