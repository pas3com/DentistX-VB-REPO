Imports AppResources
Imports Dapper
Imports DevExpress.Utils.Svg
Imports System.Data.SqlClient
Imports System.Drawing
Imports System.Globalization
Imports System.IO
Imports System.Runtime.Serialization.Formatters.Binary
Imports System.Threading
Imports System.Windows.Forms

Module Module1
    'Public sharedFilePath As String = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "DentistX\ppt_instruction.txt")




    Public CellStr As String = ""
    Public senderObj As String = ""
    Public Init_Dir As String = ""
    Public IsPatient As Boolean
    Public FromMenu As Boolean = False
    Public OnePatient As Boolean = False
    Public SqlConnection1 As SqlClient.SqlConnection = New SqlClient.SqlConnection
    ''Dim BalQrs As DentistX.BalanceDSTableAdapters.BalanceQueries
    Public CtAcntReady, CtRxReady, CtVisitReady, CtNoteReady, CtImgReady As Boolean
    Public Auxready As Boolean = False
    Public Orthready As Boolean = False
    Public Trtready As Boolean = False
    Public MobReady As Boolean = False
    Public Surgready As Boolean = False
    Public Eng As Boolean = False 'True '
    Public PrevLang As String = "en"
    Public DbT As String = "Srvr"
    Public HostName As String = "."
    Public HostPass As String = "1234"
    Public SizeMode As String = "Normal"
    Public Start As String = "MainView3"
    Public TempImg As Image
    Public CurrentDate As Date = Now
    Public LastCheckDate As Date = Now
    Public isKid As Boolean = False
    Public manualOverrideActive As Boolean = False
    Public manualKidStatus As Boolean = False
    Public suppressToggleEvent As Boolean = True

    Public BackClr As Color = Color.FromArgb(227, 239, 255)

    Public AppIcon As Icon
    'Dim Ds As New PatientDS
    Public WithEvents BS As New BindingSource

    Public Const Filters As String = "All Supported Images|*.bmp;*.gif;*.jpeg;*.jpg;*.png;*.tiff;*.ico;*.wmf;*.emf|" &
                                 "Bitmap Files (*.bmp)|*.bmp|" &
                                 "JPEG Files (*.jpg, *.jpeg)|*.jpg;*.jpeg|" &
                                 "PNG Files (*.png)|*.png|" &
                                 "GIF Files (*.gif)|*.gif|" &
                                 "TIFF Files (*.tif, *.tiff)|*.tif;*.tiff|" &
                                 "Icon Files (*.ico)|*.ico|" &
                                 "All Files (*.*)|*.*"


#Region "DentSubsAndFuncs"
    Public Structure ColumnRow
        Public Column As Integer
        Public Row As Integer

        Public Sub New(col As Integer, row As Integer)
            Me.Column = col
            Me.Row = row
        End Sub

        Public Overrides Function ToString() As String
            Return $"{Column}-{Row}"
        End Function
    End Structure

    ''' <summary>
    '''Converts grid cell address to ColumnRow structure
    ''' Valid range: 20-93 (columns 2-9, rows 0-3)
    ''' </summary>
    Public Function AddressToColumnRow(cellAddress As Integer) As ColumnRow
        ' Validate input range
        If cellAddress < 20 OrElse cellAddress > 93 Then
            Throw New ArgumentOutOfRangeException(NameOf(cellAddress), "Valid addresses are 20-93")
        End If

        Dim column = cellAddress \ 10
        Dim row = cellAddress Mod 10

        ' Validate row range
        If row > 3 Then
            Throw New ArgumentException("Row value must be 0-3", NameOf(cellAddress))
        End If

        Return New ColumnRow(column, row)
    End Function

    ''' <summary>
    ''' Converts ColumnRow structure to grid cell address
    ''' Valid columns: 2-9, Valid rows: 0-3
    ''' </summary>
    Public Function ColumnRowToAddress(colRow As ColumnRow) As Integer
        ' Validate column range
        If colRow.Column < 2 OrElse colRow.Column > 9 Then
            Throw New ArgumentOutOfRangeException(NameOf(colRow.Column), "Valid columns are 2-9")
        End If

        ' Validate row range
        If colRow.Row < 0 OrElse colRow.Row > 3 Then
            Throw New ArgumentOutOfRangeException(NameOf(colRow.Row), "Valid rows are 0-3")
        End If

        Return (colRow.Column * 10) + colRow.Row
    End Function

    ''' <summary>
    ''' Alternative conversion using separate column/row parameters
    ''' </summary>
    Public Function ColumnRowToAddress(column As Integer, row As Integer) As Integer
        Return ColumnRowToAddress(New ColumnRow(column, row))
    End Function

    ''' <summary>
    ''' Safety wrapper for string-based cell addresses
    ''' </summary>
    Public Function TryParseCellAddress(address As String, ByRef result As ColumnRow) As Boolean
        Dim numericAddress As Integer
        If Integer.TryParse(address, numericAddress) Then
            If numericAddress >= 20 AndAlso numericAddress <= 93 Then
                result = AddressToColumnRow(numericAddress)
                Return True
            End If
        End If
        result = New ColumnRow(0, 0)
        Return False
    End Function
#End Region






    Public Function SetPatientsNumbers() As Integer
        Dim totalUpdated As Integer = 0

        Try
            Using connection As New SqlConnection(DentistXDATA.GetConnection.ConnectionString)
                connection.Open()

                ' Since your proc returns a single value, you can use ExecuteScalar
                Dim result = connection.ExecuteScalar(Of Integer)(
                "SetPatientsNumbers",
                commandType:=CommandType.StoredProcedure
            )
            End Using

        Catch ex As Exception
            ' Log the error or handle it as needed
            MsgBox($"Error setting patient numbers: {ex.Message}")
        End Try

        Return totalUpdated
    End Function

    Public Function GetIcon() As Icon
        Dim ic As Icon = My.Resources.DentWhite 'Icon.ExtractAssociatedIcon(AppDomain.CurrentDomain.FriendlyName)
        Return ic
    End Function


    ''' <summary>
    ''' Changes fonts of controls contained in font collection recursively. <br/>
    ''' <b>Usage:</b> <c><br/>
    ''' SetAllControlsFont(this.Controls, 20); // This makes fonts 20% bigger. <br/>
    ''' SetAllControlsFont(this.Controls, -4, false); // This makes fonts smaller by 4.</c>
    ''' </summary>
    ''' <param name="ctrls">Control collection containing controls</param>
    ''' <param name="amount">Amount to change: posive value makes it bigger, 
    ''' negative value smaller</param>
    ''' <param name="amountInPercent">True - grow / shrink in percent, 
    ''' False - grow / shrink absolute</param>
    ''' 

    Public Sub SetAllControlsFontSize(ByVal ctrls As System.Windows.Forms.Control.ControlCollection, Optional ByVal amount As Integer = 0, Optional ByVal amountInPercent As Boolean = True)
        If amount = 0 Then
            Return
        End If
        For Each ctrl As Control In ctrls
            ' recursive
            If ctrl.Controls IsNot Nothing Then
                SetAllControlsFontSize(ctrl.Controls, amount, amountInPercent)
            End If
            If ctrl IsNot Nothing Then
                Dim oldSize = ctrl.Font.Size
                Dim newSize As Single = If(amountInPercent, oldSize + amount * (amount \ 100), oldSize + amount)
                If newSize < 4 Then
                    newSize = 4 ' don't allow less than 4
                End If
                Dim fontFamilyName = ctrl.Font.FontFamily.Name

                ctrl.Font = New Font(fontFamilyName, newSize) ', FontStyle.Bold)
            End If
        Next ctrl
    End Sub

    '#Region "DentSubsAndFuncs"
    '    Public Structure ColumnRow
    '        Dim col As Integer
    '        Dim row As Integer
    '    End Structure

    '    Public Function CellSplit(ByVal Adres As Integer) As ColumnRow
    '        Dim ClRo As New ColumnRow
    '        Dim s As String
    '        s = Adres
    '        ClRo.col = s.Substring(0)
    '        ClRo.row = s.Substring(1)
    '        Return ClRo
    '    End Function

    '    Public Function AddressToColumnRow(ByVal CelAddres As Integer) As ColumnRow
    '        Dim clros As New ColumnRow
    '        Select Case CelAddres
    '            Case 20
    '                clros.col = 2
    '                clros.row = 0
    '                Return clros
    '            Case 21
    '                clros.col = 2
    '                clros.row = 1
    '                Return clros
    '            Case 22
    '                clros.col = 2
    '                clros.row = 2
    '                Return clros
    '            Case 23
    '                clros.col = 2
    '                clros.row = 3
    '                Return clros
    '            Case 30
    '                clros.col = 3
    '                clros.row = 0
    '                Return clros
    '            Case 31
    '                clros.col = 3
    '                clros.row = 1
    '                Return clros
    '            Case 32
    '                clros.col = 3
    '                clros.row = 2
    '                Return clros
    '            Case 33
    '                clros.col = 3
    '                clros.row = 3
    '                Return clros
    '            Case 40
    '                clros.col = 4
    '                clros.row = 0
    '                Return clros
    '            Case 41
    '                clros.col = 4
    '                clros.row = 1
    '                Return clros
    '            Case 42
    '                clros.col = 4
    '                clros.row = 2
    '                Return clros
    '            Case 43
    '                clros.col = 4
    '                clros.row = 3
    '                Return clros
    '            Case 50
    '                clros.col = 5
    '                clros.row = 0
    '                Return clros
    '            Case 51
    '                clros.col = 5
    '                clros.row = 1
    '                Return clros
    '            Case 52
    '                clros.col = 5
    '                clros.row = 2
    '                Return clros
    '            Case 53
    '                clros.col = 5
    '                clros.row = 3
    '                Return clros
    '            Case 60
    '                clros.col = 6
    '                clros.row = 0
    '                Return clros
    '            Case 61
    '                clros.col = 6
    '                clros.row = 1
    '                Return clros
    '            Case 62
    '                clros.col = 6
    '                clros.row = 2
    '                Return clros
    '            Case 63
    '                clros.col = 6
    '                clros.row = 3
    '                Return clros
    '            Case 70
    '                clros.col = 7
    '                clros.row = 0
    '                Return clros
    '            Case 71
    '                clros.col = 7
    '                clros.row = 1
    '                Return clros
    '            Case 72
    '                clros.col = 7
    '                clros.row = 2
    '                Return clros
    '            Case 73
    '                clros.col = 7
    '                clros.row = 3
    '                Return clros
    '            Case 80
    '                clros.col = 8
    '                clros.row = 0
    '                Return clros
    '            Case 81
    '                clros.col = 8
    '                clros.row = 1
    '                Return clros
    '            Case 82
    '                clros.col = 8
    '                clros.row = 2
    '                Return clros
    '            Case 83
    '                clros.col = 8
    '                clros.row = 3
    '                Return clros
    '            Case 90
    '                clros.col = 9
    '                clros.row = 0
    '                Return clros
    '            Case 91
    '                clros.col = 9
    '                clros.row = 1
    '                Return clros
    '            Case 92
    '                clros.col = 9
    '                clros.row = 2
    '                Return clros
    '            Case 93
    '                clros.col = 9
    '                clros.row = 3
    '                Return clros
    '        End Select
    '    End Function
    '    Public Function ColumnRowToAddress(ByVal cellcol As Integer, ByVal cellrow As Integer) As Integer
    '        Dim x As Integer = 0
    '        Select Case cellcol
    '            Case 2
    '                Select Case cellrow
    '                    Case 0
    '                        x = 20
    '                    Case 1
    '                        x = 21
    '                    Case 2
    '                        x = 22
    '                    Case 3
    '                        x = 23
    '                End Select
    '            Case 3
    '                Select Case cellrow
    '                    Case 0
    '                        x = 30
    '                    Case 1
    '                        x = 31
    '                    Case 2
    '                        x = 32
    '                    Case 3
    '                        x = 33
    '                End Select
    '            Case 4
    '                Select Case cellrow
    '                    Case 0
    '                        x = 40
    '                    Case 1
    '                        x = 41
    '                    Case 2
    '                        x = 42
    '                    Case 3
    '                        x = 43
    '                End Select
    '            Case 5
    '                Select Case cellrow
    '                    Case 0
    '                        x = 50
    '                    Case 1
    '                        x = 51
    '                    Case 2
    '                        x = 52
    '                    Case 3
    '                        x = 53
    '                End Select
    '            Case 6
    '                Select Case cellrow
    '                    Case 0
    '                        x = 60
    '                    Case 1
    '                        x = 61
    '                    Case 2
    '                        x = 62
    '                    Case 3
    '                        x = 63
    '                End Select
    '            Case 7
    '                Select Case cellrow
    '                    Case 0
    '                        x = 70
    '                    Case 1
    '                        x = 71
    '                    Case 2
    '                        x = 72
    '                    Case 3
    '                        x = 73
    '                End Select
    '            Case 8
    '                Select Case cellrow
    '                    Case 0
    '                        x = 80
    '                    Case 1
    '                        x = 81
    '                    Case 2
    '                        x = 82
    '                    Case 3
    '                        x = 83
    '                End Select
    '            Case 9
    '                Select Case cellrow
    '                    Case 0
    '                        x = 90
    '                    Case 1
    '                        x = 91
    '                    Case 2
    '                        x = 92
    '                    Case 3
    '                        x = 93
    '                End Select
    '        End Select
    '        Return x
    '    End Function


    '#End Region


    Public Function IsFormOpen(ByVal sender As String) As Boolean
        Return ((From f In Application.OpenForms.Cast(Of Form)() Where f.Name.Equals(sender) Select f.Name).ToList.Count > 0)
    End Function


#Region "GetImage from resources"

    Public Function GetAdd16() As Image
        Return ResourceLoader.GetPngResource($"add_16x16.png")
    End Function

    Public Function GetAdd32() As Image
        Return ResourceLoader.GetPngResource($"add_32x32.png")
    End Function

    Public Function GetNext() As Image
        Return ResourceLoader.GetPngResource($"next_32x32.png")
    End Function

    Public Function GetPrev() As Image
        Return ResourceLoader.GetPngResource($"prev_32x32.png")
    End Function

    Public Function GetLast() As Image
        Return ResourceLoader.GetPngResource($"last_32x32.png")
    End Function

    Public Function GetFirst() As Image
        Return ResourceLoader.GetPngResource($"first_32x32.png")
    End Function

    Public Function GetUndoSvg() As SvgImage
        Return ResourceLoader.GetSvgResource($"undo.svg")
    End Function

    Public Function GetResetSvg() As SvgImage
        Return ResourceLoader.GetSvgResource($"resetlayoutoptions.svg")
    End Function

    Public Function GetIMP(ByVal grid As String) As Image
        Dim imageMap As New Dictionary(Of String, Image) From {
            {"LU", ResourceLoader.GetPngResource($"Teeth/imp_U.gif")},
            {"LD", ResourceLoader.GetPngResource($"Teeth/imp_L.gif")},
            {"RU", ResourceLoader.GetPngResource($"Teeth/imp_U.gif")},
            {"RD", ResourceLoader.GetPngResource($"Teeth/imp_L.gif")}
        }
        Return If(imageMap.ContainsKey(grid), imageMap(grid), Nothing)
    End Function

    Public Function GetImpName(ByVal grid As String) As String

        Dim nameMap As New Dictionary(Of String, String) From {
        {"LU", "imp_U"},
        {"LD", "imp_L"},
        {"RU", "imp_U"},
        {"RD", "imp_L"}
        }
        Return If(nameMap.ContainsKey(grid), nameMap(grid), Nothing)
    End Function

    Public Function GetIMG(ByVal CelAdres As Integer, ByVal grid As String) As Image
        Dim Column As Integer = AddressToColumnRow(CelAdres).Column
        Dim imgDict As New Dictionary(Of String, Image()) From {
        {"LU", {Nothing, Nothing, ResourceLoader.GetPngResource($"Teeth/LU1.gif"), ResourceLoader.GetPngResource($"Teeth/LU2.gif"),
        ResourceLoader.GetPngResource($"Teeth/LU3.gif"), ResourceLoader.GetPngResource($"Teeth/LU4.gif"), ResourceLoader.GetPngResource($"Teeth/LU5.gif"),
        ResourceLoader.GetPngResource($"Teeth/LU6.gif"), ResourceLoader.GetPngResource($"Teeth/LU7.gif"), ResourceLoader.GetPngResource($"Teeth/LU8.gif")}},
        {"LD", {Nothing, Nothing, ResourceLoader.GetPngResource($"Teeth/LD1.gif"), ResourceLoader.GetPngResource($"Teeth/LD2.gif"),
        ResourceLoader.GetPngResource($"Teeth/LD3.gif"), ResourceLoader.GetPngResource($"Teeth/LD4.gif"), ResourceLoader.GetPngResource($"Teeth/LD5.gif"),
        ResourceLoader.GetPngResource($"Teeth/LD6.gif"), ResourceLoader.GetPngResource($"Teeth/LD7.gif"), ResourceLoader.GetPngResource($"Teeth/LD8.gif")}},
        {"RU", {Nothing, Nothing, ResourceLoader.GetPngResource($"Teeth/RU1.gif"), ResourceLoader.GetPngResource($"Teeth/RU2.gif"),
        ResourceLoader.GetPngResource($"Teeth/RU3.gif"), ResourceLoader.GetPngResource($"Teeth/RU4.gif"), ResourceLoader.GetPngResource($"Teeth/RU5.gif"),
        ResourceLoader.GetPngResource($"Teeth/RU6.gif"), ResourceLoader.GetPngResource($"Teeth/RU7.gif"), ResourceLoader.GetPngResource($"Teeth/RU8.gif")}},
        {"RD", {Nothing, Nothing, ResourceLoader.GetPngResource($"Teeth/RD1.gif"), ResourceLoader.GetPngResource($"Teeth/RD2.gif"),
        ResourceLoader.GetPngResource($"Teeth/RD3.gif"), ResourceLoader.GetPngResource($"Teeth/RD4.gif"), ResourceLoader.GetPngResource($"Teeth/RD5.gif"),
        ResourceLoader.GetPngResource($"Teeth/RD6.gif"), ResourceLoader.GetPngResource($"Teeth/RD7.gif"), ResourceLoader.GetPngResource($"Teeth/RD8.gif")}}
        }
        Return If(imgDict.ContainsKey(grid) AndAlso Column >= 2 AndAlso Column <= 9, imgDict(grid)(Column), Nothing)
    End Function

    Public Function GetImgName(ByVal CelAdres As Integer, ByVal grid As String) As String
        Dim Column As Integer = AddressToColumnRow(CelAdres).Column
        Dim nameDict As New Dictionary(Of String, String()) From {
        {"LU", {Nothing, "LU1", "LU2", "LU3", "LU4", "LU5", "LU6", "LU7", "LU8"}},
        {"LD", {Nothing, "LD1", "LD2", "LD3", "LD4", "LD5", "LD6", "LD7", "LD8"}},
        {"RU", {Nothing, "RU1", "RU2", "RU3", "RU4", "RU5", "RU6", "RU7", "RU8"}},
        {"RD", {Nothing, "RD1", "RD2", "RD3", "RD4", "RD5", "RD6", "RD7", "RD8"}}
    }
        Return If(nameDict.ContainsKey(grid) AndAlso Column >= 1 AndAlso Column <= 8, nameDict(grid)(Column), Nothing)
    End Function


#End Region

#Region "SqlFuncs"
    Public Function GetConnection() As SqlConnection
        Dim connectionString As String = DentistX.MainCon.ConString ' My.Settings.DentistXConnectionString
        '= "Data Source=.;Initial Catalog=DentistX;Integrated Security=SSPI;"
        Return New SqlConnection(connectionString)
    End Function

    Public Function IsDbExist(ByVal db As String) As Boolean
        Dim b As Boolean = False
        Dim cn As SqlConnection = GetConnection()

        Dim sql As String = " select count(*) from sys.databases where [name] = '" & db & "' "
        Dim cmd As SqlCommand = New SqlCommand(sql, cn)

        cmd.Connection.Open()
        Dim blah As Integer = cmd.ExecuteScalar
        cmd.Connection.Close()
        If blah = 0 Then
            b = False
        ElseIf blah > 0 Then
            b = True
        End If
        Return b
    End Function
    Public Function IsTblExist(ByVal tbl As String) As Boolean
        Dim b As Boolean = False
        Dim cn As SqlConnection = GetConnection()

        Dim sql As String = "select * from sys.objects where [name] = '" & tbl & "' and type_desc = 'USER_TABLE'"

        Dim cmd As SqlCommand = New SqlCommand(sql, cn)

        cmd.Connection.Open()
        Dim blah As Integer = cmd.ExecuteNonQuery()
        cmd.Connection.Close()
        If blah = 0 Then
            b = False
        ElseIf b > 0 Then
            b = True
        End If
        Return b
    End Function

    Public Function DentTables() As List(Of String)
        Dim returnValue As New List(Of String)
        Dim sql As String = "SELECT TABLE_NAME FROM DentistX.INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'BASE TABLE'"

        Dim connection As SqlConnection = GetConnection()
        Dim command As New SqlCommand
        Dim reader As SqlDataReader
        command = New SqlCommand(sql, connection)
        command.CommandType = CommandType.Text
        Try
            connection.Open()
            reader = command.ExecuteReader()
            If reader.HasRows = True Then
                While reader.Read()
                    returnValue.Add(System.Convert.ToString(reader.GetValue(0)))
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
    Public Function GetCurrentIdentity(ByVal Table As String) As Integer
        Dim query As String
        Dim connection As New SqlConnection
        Dim command As New SqlCommand
        Dim reader As SqlDataReader
        Dim returnValue As Integer = 0

        query = "SELECT IDENT_CURRENT('" & Table & "')"
        connection = GetConnection()
        command = New SqlCommand(query, connection)
        command.CommandType = CommandType.Text
        Try
            connection.Open()
            reader = command.ExecuteReader()
            If reader.HasRows = True Then
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

    Public Function GetIdentity_Incr(ByVal Table As String) As Integer
        Dim query As String
        Dim connection As New SqlConnection
        Dim command As New SqlCommand
        Dim reader As SqlDataReader
        Dim returnValue As Integer = 0

        query = "SELECT IDENT_INCR('" & Table & "')"
        connection = GetConnection()
        command = New SqlCommand(query, connection)
        command.CommandType = CommandType.Text
        Try
            connection.Open()
            reader = command.ExecuteReader()
            If reader.HasRows = True Then
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

    Public Function GetAutoID(ByVal Mode As String, ByVal Table As String) As Integer
        Dim Ident_Current As Integer = GetCurrentIdentity(Table)
        If Mode = "Last" Then
            Return Ident_Current
        ElseIf Mode = "New" Then
            Return Ident_Current + GetIdentity_Incr(Table)
        End If
        Return Nothing
    End Function


    Public Function StyleSTRINGSUpdt(ByVal Table As String, ByVal field As String, ByVal NewValue As String,
                              ByVal Patient As Integer, ByVal StyleID As Integer) As Integer
        Dim SQL As String = ""
        Dim st As String = ""
        Select Case Table
            Case "LUSTYLE"
                st = "LUID"
            Case "LDSTYLE"
                st = "LDID"
            Case "RUSTYLE"
                st = "RUID"
            Case "RDSTYLE"
                st = "RDID"
        End Select
        Dim x As Integer
        SqlConnection1.ConnectionString = DentistX.MainCon.ConString ' DentistX.My.Settings.DentistXConnectionString
        SQL = "update " & Table & " set " & field & " = " & NewValue & " where PatientID =" & Patient & " and " & st & " = " & StyleID & " "
        Dim CMDsl As SqlClient.SqlCommand = New SqlClient.SqlCommand
        Try
            CMDsl.CommandType = CommandType.Text
            CMDsl.Connection = SqlConnection1
            If SqlConnection1.State = ConnectionState.Open Then SqlConnection1.Close()
            SqlConnection1.Open()
            CMDsl.CommandText = SQL
            x = CMDsl.ExecuteScalar
        Catch ex As System.Data.SqlClient.SqlException
            MsgBox(ex.Message)
        Finally
            SqlConnection1.Close()
        End Try
        Return x
    End Function

    Public Function SelectId(ByVal table As String, ByVal Field As String, ByVal i As Integer) As Integer
        Dim SQL As String = ""
        Dim x As Integer
        SqlConnection1.ConnectionString = DentistX.MainCon.ConString ' DentistX.My.Settings.DentistXConnectionString
        SQL = "select " & Field & " from " & table & " where " & Field & " =" & i & " "
        Dim CMDsl As SqlClient.SqlCommand = New SqlClient.SqlCommand
        Try
            CMDsl.CommandType = CommandType.Text
            CMDsl.Connection = SqlConnection1
            If SqlConnection1.State = ConnectionState.Open Then SqlConnection1.Close()
            SqlConnection1.Open()
            CMDsl.CommandText = SQL
            x = CMDsl.ExecuteScalar

        Finally
            SqlConnection1.Close()
        End Try
        Return x
    End Function
    Public Function CountId(ByVal table As String, ByVal Field As String) As Integer
        Dim SQL As String = ""
        Dim x As Integer
        SqlConnection1.ConnectionString = DentistX.MainCon.ConString ' DentistX.My.Settings.DentistXConnectionString
        SQL = "select count(" & Field & ") from " & table & " "
        Dim CMDsl As SqlClient.SqlCommand = New SqlClient.SqlCommand
        Try
            CMDsl.CommandType = CommandType.Text
            CMDsl.Connection = SqlConnection1
            If SqlConnection1.State = ConnectionState.Open Then SqlConnection1.Close()
            SqlConnection1.Open()
            CMDsl.CommandText = SQL
            x = CMDsl.ExecuteScalar
        Catch ex As System.Data.SqlClient.SqlException
            MsgBox(ex.Message)
        Finally
            SqlConnection1.Close()
        End Try

        Return x
    End Function



#End Region



#Region "MyDateAndTimePicker"

    'To get the current windows date format pattern 
    'VisitNotes.Text = System.Globalization.CultureInfo.CurrentUICulture.DateTimeFormat.ShortDatePattern()
    'To change the pattern
    'Microsoft.Win32.Registry.SetValue("HKEY_CURRENT_USER\Control Panel\International", "sShortDate", "dd/MM/yyyy")

    Public ResDateString As String = ""
    Public ResDateTag As String = ""

    Public Function GetDateStr(ByVal MYMDate As String, Optional ByVal YearISBefore As Boolean = True) As String
        On Error Resume Next
        If MYMDate = "" Or MYMDate = "0" Then Return ""
        Dim str As String = ""
        Dim TY As String = Mid(MYMDate, 1, 4)
        Dim TM As String = Mid(MYMDate, 5, 2)
        Dim TD As String = Mid(MYMDate, 7, 2)
        Dim THY As String = ""
        Dim THM As String = ""
        Dim THD As String = ""
        Dim Dt As New DateTime
        Dt = GregorianToHijri(TY, TM, TD)
        str += Dt.ToString("dddd") & "،   "
        THY = Dt.ToString("yyyy")
        THM = Dt.ToString("MM").PadLeft(2, "0")
        THD = Dt.ToString("dd").PadLeft(2, "0")
        If YearISBefore = True Then
            str += THY & "/" & THM & "/" & THD & " هـ  -  " & TY & "/" & TM & "/" & TD & " م"
        Else
            str += THD & "/" & THM & "/" & THY & " هـ  -  " & TD & "/" & TM & "/" & TY & " م"
        End If
        Return str
    End Function

#Region "DateConverter"

    Public Function ConvertDateCalendar(ByVal DateConv As DateTime,
ByVal Calendar As String, ByVal DateLangCulture As String) As String

        Dim DTFormat As DateTimeFormatInfo
        DateLangCulture = DateLangCulture.ToLower()
        '' We can't have the hijri date writen in English. We will get a runtime error - LAITH - 11/13/2005 1:01:45 PM -

        If Calendar = "Hijri" AndAlso DateLangCulture.StartsWith("en-") Then
            DateLangCulture = "ar-sa"
        End If

        '' Set the date time format to the given culture - LAITH - 11/13/2005 1:04:22 PM -
        DTFormat = New System.Globalization.CultureInfo(DateLangCulture, False).DateTimeFormat

        '' Set the calendar property of the date time format to the given calendar - LAITH - 11/13/2005 1:04:52 PM -
        Select Case Calendar
            Case "Hijri"
                DTFormat.Calendar = New System.Globalization.HijriCalendar()
                Exit Select

            Case "Gregorian"
                DTFormat.Calendar = New System.Globalization.GregorianCalendar()
                Exit Select
            Case Else

                Return ""
        End Select

        '' We format the date structure to whatever we want - LAITH - 11/13/2005 1:05:39 PM -
        DTFormat.ShortDatePattern = "dd/MM/yyyy"
        Return (DateConv.[Date].ToString("f", DTFormat))
    End Function
    Public Function GregorianToHijri(ByVal Year As Integer, ByVal Month As Integer, ByVal Day As Integer) As DateTime
        Dim saved = Thread.CurrentThread.CurrentCulture
        Try
            Dim ar As Globalization.CultureInfo = New Globalization.CultureInfo("en")
            Thread.CurrentThread.CurrentCulture = ar
            ar.DateTimeFormat.Calendar = New Globalization.GregorianCalendar
            Return New DateTime(Year, Month, Day, ar.DateTimeFormat.Calendar)
        Catch
            Return DateTime.MinValue
        Finally
            Thread.CurrentThread.CurrentCulture = saved
        End Try
    End Function

    Public Function HijriToGregorian(ByVal Year As Integer, ByVal Month As Integer, ByVal Day As Integer) As DateTime
        Dim saved = Thread.CurrentThread.CurrentCulture
        Try
            Dim ar As Globalization.CultureInfo = New Globalization.CultureInfo("ar")
            Thread.CurrentThread.CurrentCulture = ar
            ar.DateTimeFormat.Calendar = New Globalization.GregorianCalendar
            Return New DateTime(Year, Month, Day, ar.DateTimeFormat.Calendar)
        Catch
            Return DateTime.MinValue
        Finally
            Thread.CurrentThread.CurrentCulture = saved
        End Try
    End Function
#End Region

#End Region

#Region "Strings"

    Public Function RichTextBoxChangeWordColor(ByRef rtb As RichTextBox, ByVal startWord As String, ByVal endWord As String, ByVal color As Color) As RichTextBox
        rtb.SuspendLayout()
        Dim scroll As Point = rtb.AutoScrollOffset
        Dim slct As Integer = rtb.SelectionIndent
        Dim ss As Integer = rtb.SelectionStart
        Dim ls As List(Of Point) = GetAllWordsIndecesBetween(rtb.Text, startWord, endWord, True)
        For Each item In ls
            rtb.SelectionStart = item.X
            rtb.SelectionLength = item.Y - item.X
            rtb.SelectionColor = color
        Next item
        rtb.SelectionStart = ss
        rtb.SelectionIndent = slct
        rtb.AutoScrollOffset = scroll
        rtb.ResumeLayout(True)
        Return rtb
    End Function

    Public Function GetAllWordsIndecesBetween(ByVal intoText As String, ByVal fromThis As String, ByVal toThis As String, Optional ByVal withSigns As Boolean = True) As List(Of Point)
        Dim result As New List(Of Point)()
        Dim stack As New Stack(Of Integer)()
        Dim start As Boolean = False
        For i As Integer = 0 To intoText.Length - 1
            Dim ssubstr As String = intoText.Substring(i)
            If ssubstr.StartsWith(fromThis) AndAlso ((fromThis = toThis AndAlso Not start) OrElse Not ssubstr.StartsWith(toThis)) Then
                If Not withSigns Then
                    i += fromThis.Length
                End If
                start = True
                stack.Push(i)
            ElseIf ssubstr.StartsWith(toThis) Then
                If withSigns Then
                    i += toThis.Length
                End If
                start = False
                If stack.Count > 0 Then
                    Dim startindex As Integer = stack.Pop()
                    result.Add(New Point(startindex, i))
                End If
            End If
        Next i
        Return result
    End Function

    Public Function DigitsOnly(ByVal origText As String) As String
        ' ----- Return only the digits found in a string.
        Dim destText As String
        Dim counter As Integer
        ' ----- Examine each character.
        destText = ""
        For counter = 1 To Len(origText)
            If (IsNumeric(Mid(origText, counter, 1))) Then _
            destText &= Mid(origText, counter, 1)
        Next counter
        Return destText
    End Function
    '====================
    '=bigString = "abc,def,ghi,jkl,mno"
    '=MsgBox(GetSubStr(bigString, ",", 3)) ' Displays: ghi
    '=====================
    Public Function GetSubStr(ByVal origString As String,
        ByVal delim As String, ByVal whichField As Integer) As String
        ' ----- Extracts a delimited string from another
        ' larger string.
        Dim stringParts() As String
        ' ----- Handle some errors.

        If (whichField < 0) Then Return ""
        If (Len(origString) < 1) Then Return ""
        If (Len(delim) = 0) Then Return ""
        ' ----- Break the string up into delimited parts.
        stringParts = Split(origString, delim)

        ' ----- See whether the part we want exists and return it.
        If (whichField > UBound(stringParts) + 1) Then Return "" _
        Else Return stringParts(whichField - 1)
    End Function

    Function RegKeyExists(ByVal regKey As String) As Boolean
        Dim exists As Boolean = False
        Try
            If My.Computer.Registry.CurrentUser.OpenSubKey(regKey) IsNot Nothing Then
                exists = True
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            My.Computer.Registry.CurrentUser.Close()
        End Try
        Return exists
    End Function
#End Region

    'KeyDown
    '' F10 displays the number of contacts
    '        If e.KeyCode = Keys.F10 Then
    '            MsgBox("There are " & MyContacts.Count.ToString & _
    '                      " contacts in the database")
    '            e.Handled = True
    '        End If
    '' ALT+ takes you to next contact, ALT- takes you to previous contact
    '        If e.KeyCode = Keys.Subtract And e.Modifiers = Keys.Alt Then
    '            bttnPrevious_Click(sender, e)
    '        End If
    '        If e.KeyCode = Keys.Add And e.Modifiers = Keys.Alt Then
    '            bttnNext_Click(sender, e)
    '        End If
    '' If Enter was pressed and the active control is a textbox, move the focus as with the tab key
    '        If e.KeyCode = Keys.Enter Then
    '            If Me.ActiveControl.GetType Is GetType(TextBox) Then
    '                e.SuppressKeyPress = True
    '                If e.Shift Then
    '                    Me.ProcessTabKey(False)
    '                Else
    '                    Me.ProcessTabKey(True)
    '                End If
    '            End If
    '        End If


End Module
'===========================
'Private Sub btAddPic_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btAddPic.Click

'    Try
'        Dim files() As String
'        Dim file As String
'        'Dim filesEnum As IEnumerator

'        'While filesEnum.MoveNext
'        '    'ListBox1.Items.Add(filesEnum.Current)
'        'End While
'        Dim PicID As Integer
'        Dim row As PatientDS.PatientRow
'        If Me.PatientBindingSource.Count = 0 Then
'            Exit Sub
'        Else
'            row = CType(CType(Me.PatientBindingSource.Current, DataRowView).Row, PatientDS.PatientRow)
'            PatientID = row.PatientID
'        End If
'        Dim rowimg As PatientDS.Patient_ImgsRow
'        If Me.Patient_ImgsBindingSource.Count <> 0 Then
'            rowimg = CType(CType(Me.Patient_ImgsBindingSource.Current, DataRowView).Row, PatientDS.Patient_ImgsRow)
'            PatientID = rowimg.PatientID
'            PicID = rowimg.PicID
'        End If
'        Static a As Integer = 1

'        Dim PicName, PicPath, PicDes As String
'        Dim OpFDlg As New OpenFileDialog
'        With OpFDlg
'            .Multiselect = True
'            .FilterIndex = 2

'            .CheckFileExists = True
'            .ShowReadOnly = True
'            '"Text files (*.txt)|*.txt|All files (*.*)|*.*"
'            .Filter = "All files (*.*)|*.*|Bitmap Files(*.bmp)|*.bmp|JPeg Files(*.jpg)|*.jpg|Gif Files(*.gif)|*.gif"
'            .FilterIndex = 3
'            If .ShowDialog = DialogResult.OK Then
'                ' Load the specified file into a PictureBox control.


'                files = .FileNames
'                With My.Computer.FileSystem
'                    If .DirectoryExists("..\..\Images\PatientID_" & PatientID) = False Then
'                        'filesEnum = .GetFiles("..\..\Images\PatientID_" & PatientID)
'                        'If filesEnum IsNot Nothing Then
'                        '    ' Dim i As Integer
'                        '    While filesEnum.MoveNext
'                        '        'Pic(i).Image = filesEnum.Current
'                        '    End While
'                        'End If
'                        .CreateDirectory("..\..\Images\PatientID_" & PatientID)

'                        If files IsNot Nothing Then
'                            For Each s As String In files
'                                file = CType(s.Reverse, String)
'                                file = GetSubStr(file, "\", 1)
'                                If .FileExists("..\..\Images\PatientID_" & PatientID & "\PatientID_" & PatientID & "\" & s) Then '"_" & a & ".jpg") Then
'                                    If MsgBox("This File Already Exist...Do You Want To Replace It ??", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
'                                        .CopyFile(s, "..\..\Images\PatientID_" & PatientID & "\" & s, True) '"\PatientID_" & PatientID & "_" & a & ".jpg", True)
'                                    Else
'                                        Exit Sub
'                                    End If
'                                Else
'                                    .CopyFile(s, "..\..\Images\PatientID_" & PatientID & "\" & s) '"\PatientID_" & PatientID & "_" & a & ".jpg")
'                                End If
'                            Next
'                        Else
'                            If .FileExists("..\..\Images\PatientID_" & PatientID & "\PatientID_" & PatientID & "\" & OpFDlg.FileName) Then '"_" & a & ".jpg") Then
'                                If MsgBox("This File Already Exist...Do You Want To Replace It ??", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
'                                    .CopyFile(OpFDlg.FileName, "..\..\Images\PatientID_" & PatientID & "\" & OpFDlg.FileName, True) '"\PatientID_" & PatientID & "_" & a & ".jpg", True)
'                                Else
'                                    Exit Sub
'                                End If
'                            Else
'                                .CopyFile(OpFDlg.FileName, "..\..\Images\PatientID_" & PatientID & "\" & OpFDlg.FileName) '"\PatientID_" & PatientID & "_" & a & ".jpg")
'                            End If
'                        End If
'                    Else
'                        If files IsNot Nothing Then
'                            For Each s As String In files
'                                s = CType(s.Reverse, String)
'                                file = GetSubStr(file, "\", 1)
'                                If .FileExists("..\..\Images\PatientID_" & PatientID & "\PatientID_" & PatientID & "\" & s) Then '"_" & a & ".jpg") Then
'                                    If MsgBox("This File Already Exist...Do You Want To Replace It ??", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
'                                        .CopyFile(s, "..\..\Images\PatientID_" & PatientID & "\PatientID_" & PatientID & "\" & s, True) '"_" & a & ".jpg", True)
'                                    Else
'                                        Exit Sub
'                                    End If
'                                Else
'                                    .CopyFile(s, "..\..\Images\PatientID_" & PatientID & "\PatientID_" & PatientID & "\" & s) '"_" & a & ".jpg")
'                                End If
'                            Next
'                        Else
'                            If .FileExists("..\..\Images\PatientID_" & PatientID & "\PatientID_" & PatientID & "\" & OpFDlg.FileName) Then '"_" & a & ".jpg") Then
'                                If MsgBox("This File Already Exist...Do You Want To Replace It ??", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
'                                    .CopyFile(OpFDlg.FileName, "..\..\Images\PatientID_" & PatientID & "\PatientID_" & PatientID & "\" & OpFDlg.FileName, True) '"_" & a & ".jpg", True)
'                                Else
'                                    Exit Sub
'                                End If
'                            Else
'                                .CopyFile(OpFDlg.FileName, "..\..\Images\PatientID_" & PatientID & "\PatientID_" & PatientID & "\" & OpFDlg.FileName) '"_" & a & ".jpg")
'                            End If
'                        End If
'                    End If
'                End With
'            End If
'            PicName = .SafeFileName
'            PicPath = .FileName
'            Dim nam() As Char
'            nam = PicName
'            PicPath = PicPath.TrimEnd(nam)
'        End With

'        ' PicDes = DescTxt.Value
'    Catch ex As System.IO.IOException
'        MsgBox(ex.Message)
'    End Try
'End Sub
'----
'================================

'Private Sub btAddPay_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
'    Try
'        Dim remain As Decimal
'        Dim trtID As Integer
'        Dim TrtValue As Decimal
'        Dim clsd As Boolean
'        If Me.Patient_TrtsBindingSource.Count <> 0 Then
'            Dim trtrow As PatientDS.Patient_TrtsRow
'            trtrow = CType(CType(Me.Patient_TrtsBindingSource.Current, DataRowView).Row, PatientDS.Patient_TrtsRow)
'            PatientID = trtrow.PatientID
'            trtID = trtrow.TrtID
'            TrtValue = trtrow.trtValue
'            clsd = trtrow.Clsd
'        End If
'        Me.PatientDS.EnforceConstraints = False
'        If Me.Pay.Text = "" Or Me.Pay.Text = 0 Then
'            MsgBox("Enter A reasonable Value")
'            Exit Sub
'        Else
'            Dim PayValue As Decimal
'            PayValue = Convert.ToDecimal(Val(Trim(Me.Pay.Text)))
'            Dim PayDat As Date
'            Dim oldbal, newbal, oldpays, trtval As Decimal
'            PayDat = Me.PayDate.Value.ToShortDateString
'            Select Case PayValue
'                Case Is < TrtValue
'                    If Me.Patient_PaysBindingSource.Count <> 0 Then
'                        Dim payid, trtid_pay As Integer
'                        Dim paysrow As PatientDS.Patient_PaysRow
'                        paysrow = CType(CType(Me.Patient_PaysBindingSource.Current, DataRowView).Row, PatientDS.Patient_PaysRow)
'                        payid = paysrow.PayID
'                        trtid_pay = paysrow.TrtID
'                        If Me.Qrs.OldPays(payid, trtid_pay) Is Nothing Then
'                            oldpays = 0
'                        Else
'                            oldpays = CDec(Me.Qrs.OldPays(payid, trtid_pay))
'                        End If

'                        trtval = CDec(Me.Qrs.TrtValByTrtID(trtid_pay, PatientID))
'                        oldbal = oldpays - trtval
'                        newbal = oldpays + PayValue
'                        Me.Qrs.insert_Patient_Pays(PatientID, 1, trtid_pay, oldbal, PayValue, PayDat, newbal)
'                        Me.Qrs.UpdatePatientBal(newbal, PatientID)
'                    Else
'                        oldbal = CDec(Me.Qrs.TrtValByTrtID(trtID, PatientID)) * -1
'                        newbal = oldpays + PayValue
'                        Me.Qrs.insert_Patient_Pays(PatientID, 1, trtID, oldbal, PayValue, PayDat, newbal)
'                        Me.Qrs.UpdatePatientBal(newbal, PatientID)
'                    End If
'                    Me.Patient_PaysTableAdapter.Fill(Me.PatientDS.Patient_Pays)
'                    Me.PatientDS.EnforceConstraints = True
'                Case Is = TrtValue
'                    If Me.Patient_PaysBindingSource.Count <> 0 Then
'                        Dim payid, trtid_pay As Integer
'                        Dim paysrow As PatientDS.Patient_PaysRow
'                        paysrow = CType(CType(Me.Patient_PaysBindingSource.Current, DataRowView).Row, PatientDS.Patient_PaysRow)
'                        payid = paysrow.PayID
'                        trtid_pay = paysrow.TrtID
'                        If Me.Qrs.OldPays(payid, trtid_pay) Is Nothing Then
'                            oldpays = 0
'                        Else
'                            oldpays = CDec(Me.Qrs.OldPays(payid, trtid_pay))
'                        End If
'                        trtval = CDec(Me.Qrs.TrtValByTrtID(trtid_pay, PatientID))
'                        oldbal = oldpays - trtval
'                        newbal = oldpays + PayValue
'                        Me.Qrs.insert_Patient_Pays(PatientID, 1, trtid_pay, oldbal, PayValue, PayDat, newbal)
'                        Me.Qrs.UpdateTrtClsd(trtID, PatientID)
'                        Me.Qrs.UpdatePatientBal(newbal, PatientID)
'                    Else
'                        oldbal = CDec(Me.Qrs.TrtValByTrtID(trtID, PatientID)) * -1
'                        newbal = oldbal + PayValue
'                        Me.Qrs.insert_Patient_Pays(PatientID, 1, trtID, oldbal, PayValue, PayDat, newbal)
'                        Me.Qrs.UpdateTrtClsd(trtID, PatientID)
'                        Me.Qrs.UpdatePatientBal(newbal, PatientID)
'                    End If
'                    Me.Patient_PaysTableAdapter.Fill(Me.PatientDS.Patient_Pays)
'                    Me.PatientDS.EnforceConstraints = True
'                Case Is > TrtValue
'                    If TreatID.Text = "" Then
'                        If MsgBox("هذه الدفعة غير تابعة لاي علاج,هل هي دفعة مقدما؟؟", MsgBoxStyle.YesNo, "استفسار عن دفعة غير تابعة لاي علاج!!!") = MsgBoxResult.Yes Then
'                            Dim sT As String
'                            sT = InputBox("قم بادخال بيان الدفعة متبوعة بشرطة ثم مبلغ العلاج" & vbCrLf & "مثال:- البيان-12345678", "دفعة مقدما؟؟")
'                            sT = sT.Trim
'                            If sT.Count = 0 Then
'                                Exit Sub
'                            Else
'                                Dim bn, trval As String
'                                bn = GetSubStr(sT, "-", 1)
'                                trval = GetSubStr(sT, "-", 2)
'                                TrtValue = CDec(trval)
'                                Select Case trtval
'                                    Case Is > PayValue
'                                        Me.Qrs.insert_Patient_Trts(1, PatientID, "دفعة مقدما " & bn, Date.Now.ToShortDateString, TrtValue, False)
'                                        trtID = CInt(Me.Qrs.MAXTRTID(PatientID))
'                                        oldbal = TrtValue * -1
'                                        newbal = oldbal + PayValue
'                                        Me.Qrs.insert_Patient_Pays(PatientID, 1, trtID, oldbal, PayValue, PayDat, newbal)
'                                        Me.Patient_TrtsTableAdapter.Fill(Me.PatientDS.Patient_Trts)
'                                        Me.Patient_PaysTableAdapter.Fill(Me.PatientDS.Patient_Pays)
'                                        Me.Qrs.UpdatePatientBal(newbal, PatientID)
'                                        Me.PatientDS.EnforceConstraints = True
'                                        sT = ""
'                                        Exit Sub
'                                    Case Is = PayValue
'                                        Me.Qrs.insert_Patient_Trts(1, PatientID, "دفعة مقدما " & bn, Date.Now.ToShortDateString, TrtValue, False)
'                                        trtID = CInt(Me.Qrs.MAXTRTID(PatientID))
'                                        oldbal = TrtValue * -1
'                                        newbal = oldbal + PayValue
'                                        Me.Qrs.insert_Patient_Pays(PatientID, 1, trtID, oldbal, PayValue, PayDat, newbal)
'                                        Me.Qrs.UpdateTrtClsd(trtID, PatientID)
'                                        Me.Qrs.UpdatePatientBal(newbal, PatientID)
'                                        Me.Patient_TrtsTableAdapter.Fill(Me.PatientDS.Patient_Trts)
'                                        Me.Patient_PaysTableAdapter.Fill(Me.PatientDS.Patient_Pays)
'                                        Me.PatientDS.EnforceConstraints = True
'                                        sT = ""
'                                        Exit Sub
'                                    Case Is < PayValue
'                                        MsgBox()
'                                End Select

'                            End If
'                        Else
'                            Exit Sub
'                        End If
'                    Else

'                        'Me.Qrs.insert_Patient_Pays(PatientID, 1, trtID, PayValue, PayDat)
'                        Me.Patient_TrtsTableAdapter.Fill(Me.PatientDS.Patient_Trts)
'                        Me.Patient_PaysTableAdapter.Fill(Me.PatientDS.Patient_Pays)

'                        Me.PatientDS.EnforceConstraints = True
'                    End If
'            End Select
'            Me.Patient_PaysTableAdapter.Fill(Me.PatientDS.Patient_Pays)

'            TextBox2.Text = remain
'        End If

'    Catch ex As System.Data.SqlClient.SqlException
'        MsgBox(ex.Message)
'    Finally
'        Me.PatientDS.EnforceConstraints = True

'    End Try

'End Sub