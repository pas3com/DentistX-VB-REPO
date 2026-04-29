Imports System.Data.SqlClient
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Security.Cryptography
Imports System.Text
Imports System.Windows.Forms
Imports Dapper
Imports Microsoft.Office.Interop


Module MyMod



#Region "MyExternalCode"

    Public Function GetUniqueKey(ByVal maxSize As Integer) As String
        Dim chars As Char() = New Char(61) {}
        chars = "123456789".ToCharArray()
        Dim data As Byte() = New Byte(0) {}
        Dim crypto As New RNGCryptoServiceProvider()
        crypto.GetNonZeroBytes(data)
        data = New Byte(maxSize - 1) {}
        crypto.GetNonZeroBytes(data)
        Dim result As New StringBuilder(maxSize)
        For Each b As Byte In data
            result.Append(chars(b Mod (chars.Length)))
        Next
        Return result.ToString()
    End Function

    Public Sub Calc()
        System.Diagnostics.Process.Start("Calc.exe")
    End Sub
    ''Code for Phone numbers Key Press
    'Private Sub txtCartons_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtCartons.KeyPress
    '    If (e.KeyChar < Chr(48) Or e.KeyChar > Chr(57)) And e.KeyChar <> Chr(8) Then

    '        e.Handled = True

    '    End If
    'End Sub

    Public Sub ExportToExcel(ByVal Dgv As DataGridView)
        If Dgv.RowCount = Nothing Then
            MessageBox.Show("Sorry nothing to export into excel sheet.." & vbCrLf & "Please retrieve data in datagridview", "", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If
        Dim rowsTotal, colsTotal As Short
        Dim I, j, iC As Short
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Dim xlApp As New Excel.Application

        Try
            Dim excelBook As Excel.Workbook = xlApp.Workbooks.Add
            Dim excelWorksheet As Excel.Worksheet = CType(excelBook.Worksheets(1), Excel.Worksheet)
            xlApp.Visible = True

            rowsTotal = Dgv.RowCount - 1
            colsTotal = Dgv.Columns.Count - 1
            With excelWorksheet
                .Cells.Select()
                .Cells.Delete()
                For iC = 0 To colsTotal
                    .Cells(1, iC + 1).Value = Dgv.Columns(iC).HeaderText
                Next
                For I = 0 To rowsTotal - 1
                    For j = 0 To colsTotal
                        .Cells(I + 2, j + 1).value = Dgv.Rows(I).Cells(j).Value
                    Next j
                Next I
                .Rows("1:1").Font.FontStyle = "Bold"
                .Rows("1:1").Font.Size = 12

                .Cells.Columns.AutoFit()
                .Cells.Select()
                .Cells.EntireColumn.AutoFit()
                .Cells(1, 1).Select()
            End With
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            'RELEASE ALLOACTED RESOURCES
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            xlApp = Nothing
        End Try
    End Sub

#End Region


#Region "Project General Vars"

    Public Conn As New SqlConnection(DentistXDATA.GetConnection.ConnectionString) '(DentistX.MainCon.ConString)

    'متغيرات خاصة بخواص الرسائل
    Public Me_MsgInfoEng As String = MsgBoxStyle.Information + MsgBoxStyle.OkOnly ' + vbMsgBoxRight + vbMsgBoxRtlReading
    Public Me_MsgQuestionEng As String = MessageBoxButtons.YesNo + MessageBoxIcon.Question + vbDefaultButton1 '+ vbMsgBoxRight + vbMsgBoxRtlReading
    Public Me_MsgCaptionEng As String = ""
    Public Me_MsgOkAddEng As String = "Added Successfully" & vbNewLine & vbNewLine & "Another Add Operation ??"
    Public me_MsgOkEditEng As String = "Saved Successfully"
    '-------------------------
    'متغيرات خاصة بخواص الرسائل
    Public Me_MsgInfoAr As String = MsgBoxStyle.Information + MsgBoxStyle.OkOnly + vbMsgBoxRight + vbMsgBoxRtlReading
    Public Me_MsgQuestionAr As String = MessageBoxButtons.YesNo + MessageBoxIcon.Question + vbDefaultButton1 + vbMsgBoxRight + vbMsgBoxRtlReading
    Public Me_MsgCaptionAr As String = ""
    Public Me_MsgOkAddAr As String = "تمت الإضافة بنجاح" & vbNewLine & vbNewLine & "هل تريد عملبة إضافة اخرى ؟؟؟؟"
    Public me_MsgOkEditAr As String = "تم الحفظ بنجاح"

#End Region

#Region "DataSet Filling Procs"

    'إجرائية تعبئة الداتا سيت بالجدول المطلوب
    Public Sub Me_DSFillStrdPro(ByVal DS As DataSet, ByVal SQL_STR As String, ByVal TBL As String)
        Dim Da As SqlDataAdapter
        Dim Cmd As SqlCommand

        Da = New SqlDataAdapter(SQL_STR, Conn)
        Cmd = New SqlCommand(SQL_STR, Conn)

        Cmd.CommandType = CommandType.StoredProcedure
        'إذا كان الاتصال مع قاعدة البيانات مفتوح أغلقه
        If Conn.State = ConnectionState.Open Then Conn.Close()

        Conn.Open() 'فتح الاتصال
        Cmd.ExecuteNonQuery()
        DS.Clear()
        Da.Fill(DS, TBL) 'تعبئة الداتا سيت باستخدام الداتا أدابتر
        Cmd.Dispose()
        Da.Dispose()
        Conn.Close() 'إغلاق الاتصال
    End Sub


    ' Using Dapper
    Public Sub Me_DSFillTextDapper(ByVal Ds As DataSet, ByVal SqlStr As String, ByVal TableName As String)

        ' Clear the DataSet
        Ds.Clear()

        ' Open connection and execute query with Dapper
        Using Conn
            Conn.Open()

            ' Execute the query with a specific type
            Dim result As List(Of PatientResult) = Conn.Query(Of PatientResult)(SqlStr).ToList()

            ' Convert the result to a DataTable and add it to the DataSet
            If result.Any() Then
                Dim dt As DataTable = result.ToDataTable()
                dt.TableName = TableName
                Ds.Tables.Add(dt)
            End If
        End Using
    End Sub

    Public Class PatientResult
        Public Property PatientID As Integer
        Public Property PatientName As String
    End Class
    '========================
    ' Using Dapper
    '========================


    ' إجرائية تعبئة الداتا سيت بالجدول المطلوب أمر نصي
    Public Sub Me_DSFillText(ByVal Ds As DataSet, ByVal SqlStr As String, ByVal TableName As String)
        Dim Da As SqlDataAdapter
        Dim Cmd As SqlCommand

        Da = New SqlDataAdapter(SqlStr, Conn)
        Cmd = New SqlCommand(SqlStr, Conn)

        Cmd.CommandType = CommandType.Text

        'إذا كان الاتصال مع قاعدة البيانات مفتوح أغلقه
        If Conn.State = ConnectionState.Open Then Conn.Close()

        Conn.Open() 'فتح الاتصال
        Cmd.ExecuteNonQuery()
        Ds.Clear()
        Da.Fill(Ds, TableName) 'تعبئة الداتا سيت باستخدام الداتا أدابتر
        Cmd.Dispose()
        Da.Dispose()
        Conn.Close() 'إغلاق الاتصال
    End Sub

    ' إجرائية تعبئة الداتا سيت بالجدول المطلوب بناءً على بارمترات مقارنة تاريخين الخاصة ببحث الفواتير
    Public Sub Me_DsFillPar(ByVal DS As DataSet, ByVal SQL_STR As String, ByVal TBL As String, ByVal D1 As Date, ByVal D2 As Date)
        Dim cmd As New SqlCommand(SQL_STR, Conn)
        Dim DA As New SqlDataAdapter(cmd)

        cmd.CommandType = CommandType.Text
        cmd.Parameters.Add("@D1", SqlDbType.SmallDateTime).Value = D1
        cmd.Parameters.Add("@D2", SqlDbType.SmallDateTime).Value = D2
        'إذا كان الاتصال مع قاعدة البيانات مفتوح أغلقه
        If Conn.State = ConnectionState.Open Then Conn.Close()

        Conn.Open() 'فتح الاتصال
        cmd.ExecuteNonQuery()
        DS.Clear()
        DA.Fill(DS, TBL) 'تعبئة الداتا سيت باستخدام الداتا أدابتر
        Conn.Close() 'إغلاق الاتصال
    End Sub

    'إجرائية تعبئة الداتا سيت بأكثر من جدول
    Public Sub Me_DsMultiFill(ByVal DS As DataSet, ByVal SqlStr As ArrayList, ByVal TableName As ArrayList, ByVal Num As Integer)
        Dim Da As SqlDataAdapter
        Dim Cmd As SqlCommand
        For I As Integer = 0 To Num

            Da = New SqlDataAdapter(SqlStr(I), Conn)
            Cmd = New SqlCommand(SqlStr(I), Conn)

            Cmd.CommandType = CommandType.Text

            'إذا كان الاتصال مع قاعدة البيانات مفتوح أغلقه
            If Conn.State = ConnectionState.Open Then Conn.Close()

            Conn.Open() 'فتح الاتصال
            Cmd.ExecuteNonQuery()
            Da.Fill(DS, TableName(I)) 'تعبئة الداتا سيت باستخدام الداتا أدابتر
        Next
        Cmd = Nothing
        Da = Nothing
        Conn.Close() 'إغلاق الاتصال
    End Sub

#End Region



#Region "Filling Data In Forms"

    'إجرائية ملئ الكومبو بوكس ببيانات جدول
    Public Sub Me_CboFill_Param(ByVal Cbo As ComboBox, ByVal Sql As String, ByVal Dm As String, ByVal Vm As String, ByVal D1 As Integer)

        '====================
        Cbo.DataSource = Nothing
        Dim Da As New SqlDataAdapter(Sql, Conn)
        Dim Dt As New DataTable
        Da.Fill(Dt)

        If Dt.Rows.Count = 0 Then Exit Sub


        Cbo.DataSource = Dt
        Cbo.DisplayMember = Dm
        Cbo.ValueMember = Vm
    End Sub
    'إجرائية ملئ الكومبو بوكس ببيانات جدول
    Public Sub Me_CboFill(ByVal Cbo As ComboBox, ByVal Sql As String, ByVal Dm As String, ByVal Vm As String)
        Cbo.DataSource = Nothing
        Dim Da As New SqlDataAdapter(Sql, Conn)
        Dim Dt As New DataTable
        Da.Fill(Dt)

        If Dt.Rows.Count = 0 Then Exit Sub


        Cbo.DataSource = Dt
        Cbo.DisplayMember = Dm
        Cbo.ValueMember = Vm
    End Sub
    'إجرائية ملئ الكومبو بوكس ببيانات جدول المرضى

    Public Sub Me_CboFillPatient(ByVal Cbo As ComboBox, ByVal Sql As String, ByVal Dm As String, ByVal Vm As String)
        Cbo.DataSource = Nothing
        Dim Da As New SqlDataAdapter(Sql, Conn)
        Dim Dt As New DataTable
        Da.Fill(Dt)

        If Dt.Rows.Count = 0 Then Exit Sub

        BS.DataSource = Dt
        Cbo.DataSource = BS
        Cbo.DisplayMember = Dm
        Cbo.ValueMember = Vm
    End Sub
    'إجرائية ملئ الكومبو بوكس في التول سترايب ببيانات جدول
    Public Sub Me_TsCboFill(ByVal TsCbo As ToolStripComboBox, ByVal Sql As String, ByVal Dm As String, ByVal Vm As String)
        TsCbo.ComboBox.DataSource = Nothing
        Dim Da As New SqlDataAdapter(Sql, Conn)
        Dim Dt As New DataTable
        Da.Fill(Dt)

        If Dt.Rows.Count = 0 Then Exit Sub

        TsCbo.ComboBox.DataSource = Dt
        TsCbo.ComboBox.DisplayMember = Dm
        TsCbo.ComboBox.ValueMember = Vm
    End Sub


    'إجرائية تعبئة الجريد فيو 
    Public Sub Me_DgvFill(ByVal DS As DataSet, ByVal TableName As String, ByVal DGV As DataGridView)
        With DGV
            .AutoGenerateColumns = False
            .DataSource = DS
            .DataMember = TableName
        End With
    End Sub

    'إجرائية ملئ الليست بوكس ببيانات جدول
    Public Sub Me_ListBoxFill(ByVal LstBx As ListBox, ByVal Sql As String, ByVal Dm As String, ByVal Vm As String)
        LstBx.DataSource = Nothing
        Dim Da As New SqlDataAdapter(Sql, Conn)
        Dim Dt As New DataTable
        Da.Fill(Dt)

        If Dt.Rows.Count = 0 Then Exit Sub

        LstBx.DataSource = Dt
        LstBx.DisplayMember = Dm
        LstBx.ValueMember = Vm
    End Sub


#End Region

#Region "Data Input Check Procs"

    'إجرائية التحقق من عدم وجود سجل مشابه للسجل المدخل بالاعتماد على معايير
    Public Function Me_RecExCountOne(ByVal Ds As DataSet, ByVal Txt As TextBox, ByVal SQL As String, ByVal TableName As String, ByVal MSG As String) As Boolean
        Ds.Clear()
        MyMod.Me_DSFillText(Ds, SQL, TableName)
        Dim RecCount As Integer = Ds.Tables(0).Rows.Count
        If RecCount = 1 Then
            MsgBox(MSG, Me_MsgInfoEng, Me_MsgCaptionEng)
            Txt.Text = Nothing
            Txt.Focus()
            Return True
            Exit Function
        End If
        Return False
    End Function

    'إجرائية التحقق من عدم وجود سجل مشابه للسجل المدخلComboBox
    Public Function Me_RecEx_Cbo(ByVal Ds As DataSet, ByVal TableName As String, ByVal Cbo As ComboBox, ByVal SQL As String, ByVal CNum As Byte, ByVal MSG As String) As Boolean
        Ds.Clear()
        MyMod.Me_DSFillText(Ds, SQL, TableName)

        Dim Value As String

        For i As Integer = 0 To Ds.Tables(0).Rows.Count - 1
            Value = Ds.Tables(0).Rows(i).Item(CNum)
            If Cbo.Text = Value Then
                'MsgBox(MSG, Me_MsgInfo, Me_MsgCaption)
                'Cbo.Text = Nothing
                'Cbo.Focus()
                Return True
                Exit For
                Exit Function
            End If
        Next i
        Return False
    End Function
    'إجرائية التحقق من عدم وجود سجل مشابه للسجل المدخلTextBox
    Public Function Me_RecEx(ByVal Ds As DataSet, ByVal TableName As String, ByVal Txt As TextBox, ByVal SQL As String, ByVal CNum As Byte, ByVal MSG As String) As Boolean
        Ds.Clear()
        MyMod.Me_DSFillText(Ds, SQL, TableName)

        Dim Value As String

        For i As Integer = 0 To Ds.Tables(0).Rows.Count - 1
            Value = Ds.Tables(0).Rows(i).Item(CNum)
            If Txt.Text = Value Then
                MsgBox(MSG, Me_MsgInfoEng, Me_MsgCaptionEng)
                Txt.Text = Nothing
                Txt.Focus()
                Return True
                Exit For
                Exit Function
            End If
        Next i
        Return False
    End Function
    'إجرائية التحقق من عدم وجود سجل مشابه للسجل المدخل في حالة التعديل
    Public Function Me_RecExEdit(ByVal Ds As DataSet, ByVal TableName As String, ByVal Txt As TextBox, ByVal ID As Integer, ByVal SQL As String, ByVal CNum As Byte, ByVal MSG As String) As Boolean
        Ds.Clear()
        MyMod.Me_DSFillText(Ds, SQL, TableName)

        Dim IdValue As Integer
        Dim TextValue As String

        Dim i As Integer
        For i = 0 To Ds.Tables(0).Rows.Count - 1
            IdValue = Ds.Tables(0).Rows(i).Item(0)
            TextValue = Ds.Tables(0).Rows(i).Item(CNum)
            If ID = IdValue And Txt.Text = TextValue Then
                GoTo Line22
            ElseIf Txt.Text = TextValue Then
                MsgBox(MSG, Me_MsgInfoEng, Me_MsgCaptionEng)
                Txt.Text = Nothing
                Txt.Focus()
                Return True
                Exit For
                Exit Function
            End If
        Next i
Line22: Return False

    End Function

    'إجرائية التحقق من عدم وجود سجلات مرتبطة
    Public Function Me_RecRel(ByVal Sql As String, ByVal TableName As String, ByVal ItemNo As Byte, ByVal Id As Integer, ByVal MSG As String) As Boolean
        Dim DS_Rel As New DataSet
        Dim DA_Rel As New SqlDataAdapter(Sql, Conn)
        DA_Rel.Fill(DS_Rel, TableName)

        Dim RecCount As Integer = DS_Rel.Tables(TableName).Rows.Count
        Dim Value As String

        Dim i As Integer
        For i = 0 To RecCount - 1
            Value = CInt(DS_Rel.Tables(TableName).Rows(i).Item(ItemNo))
            If Id = Value Then
                MsgBox(MSG, Me_MsgInfoEng, Me_MsgCaptionEng)
                Return True
                Exit For
                Exit Function
            End If

        Next i
Line2:  Return False
    End Function

    'التحقق من وجود قيمة في مربع الكومبو بوكس
    Public Function Me_CboNotNull(ByVal Cbo As ComboBox, ByVal CboCaption As String) As Boolean
        If Cbo.Text = Nothing Then
            If Eng Then
                MsgBox("Please Select" & Space(1) & "'" & CboCaption & "'" & Space(1) & "From The List", MyMod.Me_MsgInfoEng, MyMod.Me_MsgCaptionEng)
                Cbo.Focus()
                Return True
            Else
                MsgBox("الرجاء اختيار" & Space(1) & "'" & CboCaption & "'" & Space(1) & "من القائمة", MyMod.Me_MsgInfoAr, MyMod.Me_MsgCaptionAr)
                Cbo.Focus()
                Return True
            End If

        Else
            Return False
        End If
    End Function

    'فانكشن للتحقق من عنصر أنه مطلوب ويجب ان يكون قيمة رقمية
    Public Function Me_NotNullNum(ByVal TXT As TextBox, ByVal Caption As String) As Boolean
        If Trim(TXT.Text) = Nothing Then
            If Eng Then
                MsgBox("Field" & Space(1) & "'" & Caption & "'" & Space(2) & "Required", Me_MsgInfoEng, Me_MsgCaptionEng)
                TXT.Focus()
                Return True
            Else
                MsgBox("الحقل" & Space(1) & "'" & Caption & "'" & Space(2) & "مطلوب", Me_MsgInfoAr, Me_MsgCaptionAr)
                TXT.Focus()
                Return True
            End If

        ElseIf Not IsNumeric(TXT.Text) Then
            If Eng Then
                MsgBox("Field" & Space(1) & "'" & Caption & "'" & Space(2) & "Must Be A Numeric Value", Me_MsgInfoEng, Me_MsgCaptionEng)
                TXT.Text = "0"
                TXT.Focus()
                Return True
            Else

                MsgBox("الحقل" & Space(1) & "'" & Caption & "'" & Space(2) & "يجب ان يكون قيمة رقمية", Me_MsgInfoAr, Me_MsgCaptionAr)
                TXT.Text = "0"
                TXT.Focus()
                Return True
            End If

        Else
            TXT.Text = Trim(TXT.Text)
            Return False
        End If
    End Function

    'فانكشن للتحقق من عنصر أنه ليس فارغاً 
    Public Function Me_TextNotNull(ByVal TXT As TextBox, ByVal Caption As String) As Boolean
        If Trim(TXT.Text) = Nothing Then
            If Eng Then
                MsgBox("Field  '" & Caption & "'  " & "Required", Me_MsgInfoEng, Me_MsgCaptionEng)
                TXT.Text = Nothing
                TXT.Focus()
                Return True
            Else
                MsgBox("الحقل  '" & Caption & "'  " & "مطلوب", Me_MsgInfoAr, Me_MsgCaptionAr)
                TXT.Text = Nothing
                TXT.Focus()
                Return True
            End If

        Else
            TXT.Text = Trim(TXT.Text)
            Return False
        End If
    End Function

    'فانكشن للتحقق من عنصر أنه رقمي وليس صفر 
    Public Function Me_IsNumAndNotZero(ByVal TXT As TextBox, ByVal Caption As String) As Boolean
        If Trim(TXT.Text) = 0 Then
            If Eng Then
                MsgBox("Field  '" & Caption & "'  " & "Required", Me_MsgInfoEng, Me_MsgCaptionEng)
                TXT.Text = 0
                TXT.Focus()
                Return True
            Else
                MsgBox("الحقل  '" & Caption & "'  " & "مطلوب", Me_MsgInfoAr, Me_MsgCaptionAr)
                TXT.Text = 0
                TXT.Focus()
                Return True
            End If

        ElseIf Not IsNumeric(TXT.Text) Then
            If Eng Then
                MsgBox("Field  '" & Caption & "'  " & "Must Be A Numeric Value", Me_MsgInfoEng, Me_MsgCaptionEng)
                TXT.Text = 0
                TXT.Focus()
                Return True
            Else
                MsgBox("الحقل  '" & Caption & "'  " & "يجب ان يكون قيمة رقمية", Me_MsgInfoAr, Me_MsgCaptionAr)
                TXT.Text = 0
                TXT.Focus()
                Return True
            End If

        Else
            TXT.Text = Trim(TXT.Text)
            Return False
        End If
    End Function

    'فانكشن للتحقق من عنصر أنه رقمي 
    Public Function Me_IsNum(ByVal TXT As TextBox, ByVal Caption As String) As Boolean
        If Not IsNumeric(TXT.Text) Then
            If Eng Then
                MsgBox("'" & Caption & "'" & Space(2) & "Must Be A Numeric Value", Me_MsgInfoEng, Me_MsgCaptionEng)
                TXT.Text = 0
                TXT.Focus()
                Return True
            Else
                MsgBox("'" & Caption & "'" & Space(2) & "يجب ان يكون قيمة رقمية", Me_MsgInfoAr, Me_MsgCaptionAr)
                TXT.Text = 0
                TXT.Focus()
                Return True
            End If

        Else
            TXT.Text = Trim(TXT.Text)
            Return False
        End If
    End Function

    'التحقق من وجود عناصر في مربع الكومبو بوكس
    Public Function Me_CboItemZero(ByVal Cbo As ComboBox, ByVal Caption As String) As Boolean
        If Cbo.Items.Count = 0 Then
            If Eng Then
                MsgBox("No Items " & Space(1) & "'" & Caption & "'" & Space(1) & "In List", MyMod.Me_MsgInfoEng, MyMod.Me_MsgCaptionEng)
                Cbo.Focus()
                Return True
            Else
                MsgBox("لا يوجد عناصر  " & Space(1) & "'" & Caption & "'" & Space(1) & "في القائمة", MyMod.Me_MsgInfoAr, MyMod.Me_MsgCaptionAr)
                Cbo.Focus()
                Return True
            End If

        Else
            Return False
        End If
    End Function

#End Region

#Region "DataBase Proedures"

    'مسار قاعدة البيانات
    Public Sub Get_CONN_STR()
        Try

            Conn.ConnectionString = DentistX.MainCon.ConString
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    'إجرائية معرفة عدد السجلات وموضع السجل الحالي
    Public Sub Me_RecPos(ByVal Ds As DataSet, ByVal TableName As String, ByVal F As Form, ByRef ShowPlace As String)
        Dim C, P As String
        C = Ds.Tables(0).Rows.Count
        P = F.BindingContext(Ds, TableName).Position + 1
        If Eng Then
            ShowPlace = P & Space(2) & "OF" & Space(2) & C
        Else
            ShowPlace = C & Space(2) & "من" & Space(2) & P
        End If

    End Sub

    'إجرائية الانتقال للسجل الأول
    Public Sub Me_FirstRow(ByVal F As Form, ByVal Ds As DataSet, ByVal TableName As String)
        F.BindingContext(Ds, TableName).Position = 0
    End Sub

    'إجرائية الانتقال للسجل التالي
    Public Sub Me_NextRow(ByVal F As Form, ByVal Ds As DataSet, ByVal TableName As String)
        F.BindingContext(Ds, TableName).Position += 1
    End Sub

    'إجرائية الانتقال للسجل السابق
    Public Sub Me_PrevRow(ByVal F As Form, ByVal Ds As DataSet, ByVal TableName As String)
        F.BindingContext(Ds, TableName).Position -= 1
    End Sub

    'إجرائية الانتقال للسجل الأخير
    Public Sub Me_LastRow(ByVal F As Form, ByVal Ds As DataSet, ByVal TableName As String)
        F.BindingContext(Ds, TableName).Position = F.BindingContext(Ds, TableName).Count - 1
    End Sub

    'اجرائية عامة لتنفيذ استعلام معين
    Public Function Me_SqlExecute(ByVal SqlStr As String, ByVal MsgState As Boolean, Optional ByVal Msg As String = Nothing) As Boolean
        If MsgState = True Then
            If MsgBox(Msg, MyMod.Me_MsgQuestionEng, MyMod.Me_MsgCaptionEng) = vbYes Then
                Dim Cmd As New SqlCommand(SqlStr, MyMod.Conn)
                Cmd.CommandType = CommandType.Text
                If MyMod.Conn.State = ConnectionState.Open Then MyMod.Conn.Close()
                Conn.Open()
                Cmd.ExecuteNonQuery()
                MyMod.Conn.Close()
                Cmd.Dispose()
            End If
            Return True
        Else
            Dim Cmd As New SqlCommand(SqlStr, MyMod.Conn)
            Cmd.CommandType = CommandType.Text
            If MyMod.Conn.State = ConnectionState.Open Then MyMod.Conn.Close()
            Conn.Open()
            Cmd.ExecuteNonQuery()
            MyMod.Conn.Close()
            Cmd.Dispose()
        End If
        Return True
    End Function

    Public Function Me_SqlScalar(ByVal SqlStr As String) As Object
        Dim ob As Object
        Dim Cmd As New SqlCommand(SqlStr, MyMod.Conn)
        Cmd.CommandType = CommandType.Text
        If MyMod.Conn.State = ConnectionState.Open Then MyMod.Conn.Close()
        Conn.Open()
        ob = Cmd.ExecuteScalar()
        MyMod.Conn.Close()
        Cmd.Dispose()

        Return ob
    End Function
#End Region

#Region "Program Procedures"

    'إجرائية مسح كافة عناصر نموذج
    Public Sub Me_ClearTextBox(ByVal F As Form)
        Dim TXT As Control
        For Each TXT In F.Controls
            If TypeOf TXT Is TextBox Then
                TXT.Text = Nothing
            End If
        Next
    End Sub

    'إجرائية مسح كافة عناصر نموذج ضمن البانل
    Public Sub Me_ClearDataPnl(ByVal Pnl As Panel)
        Dim TXT As Control
        For Each TXT In Pnl.Controls
            If TypeOf TXT Is TextBox Then
                TXT.Text = ""
            End If
        Next
    End Sub

    'منع الزر اليمين
    Public Function Me_RClick(ByVal e As System.Windows.Forms.MouseEventArgs, ByVal Combo As ComboBox) As Boolean
        If e.Button = MouseButtons.Right Then
            MsgBox("Please Choose From The List", Me_MsgInfoEng, Me_MsgCaptionEng)
            Combo.Focus()
            Return True
        Else
            Return False
        End If
    End Function

    'إجرائية مسح الداتا بينديج الخاص بكل تيكست بوكس موجود في نموذج
    Public Sub Me_ClearDataBinding(ByVal Pnl As Panel)
        Dim Txt As Control
        For Each Txt In Pnl.Controls
            If TypeOf Txt Is TextBox Then
                Txt.DataBindings.Clear()
            End If
        Next
    End Sub

#End Region

#Region "Current Rec Position"

    'إجرائية معرفة موضع السجل الحالي
    Sub Trash1(ByVal DS As DataSet, ByVal FRM As Form, ByVal TBL As String, ByRef Result As String)

        Dim position, Count As String
        position = FRM.BindingContext(DS, TBL).Position + 1
        Count = FRM.BindingContext(DS, TBL).Count
        Result = position & Space(2) & "From" & Space(2) & Count
    End Sub

#End Region


#Region "Date Time Funcs"

    'فانكشن التوقيت مع الفورمات الخاص به
    Public Function My_Time() As String
        My_Time = TimeOfDay.ToString("T")
    End Function

    'فانكشن التاريخ مع الفورمات الخاص به
    Public Function My_Date() As String
        My_Date = Now.Date.ToString(" ddd d / MMM / yyyy")
    End Function

#End Region

#Region "General Procedures"
    'إجرائية تلوين النماذج بلون متدرج
    Public Function GetGraphicsObject(ByVal W As Int16, ByVal H As Int16) As Image
        Dim bmp As Bitmap
        bmp = New Bitmap(W, H)
        Dim G As Graphics = Graphics.FromImage(bmp)
        Dim R As New RectangleF(0, 0, W, H)
        Dim startColor As Color = Color.DimGray 'يمكنك من هنا اختيار أي لون يبدأ التلوين به
        Dim EndColor As Color = Color.Black 'يمكنك من هنا اختيار أي لون ينتهي التلوين به
        Dim LGBrush As New System.Drawing.Drawing2D.LinearGradientBrush(R, startColor, EndColor, LinearGradientMode.Vertical)
        G.FillRectangle(LGBrush, New Rectangle(0, 0, W, H))
        Return bmp
    End Function

    'إجرائية ترقيم سجلات الجريد فيو
    Sub Me_DgvAutoNo(ByRef Dgv As DataGridView)
        Dim Dgvr As DataGridViewRow
        For Each Dgvr In Dgv.Rows
            Dgvr.HeaderCell.Value = (Dgvr.Index + 1).ToString
        Next

    End Sub

    Public Sub Me_TvPath(ByVal Tv As TreeView, ByRef Caption As String)
        Dim myNodeCount As Integer = Tv.SelectedNode.GetNodeCount(True)
        'Dim myChildPercentage As Decimal = CDec(myNodeCount) / CDec(Tv.GetNodeCount(True)) * 100
        Caption = Tv.SelectedNode.FullPath
    End Sub

#End Region

#Region "Nombers To Words"
    Public Function Amount_to_arabic(ByVal Amount As Decimal,
                                     Optional ByVal SingleCur As String = "شيقل",
                                     Optional ByVal PluralCur As String = "شواقل",
                                     Optional ByVal SingleNat As String = "اسرائيلي",
                                     Optional ByVal SingleCent As String = "اغورة",
                                     Optional ByVal PluralCent As String = "اغورات"
                                     ) As String


        Dim TextAmount As String = String.Empty                                                 'متغير حمل القيمة المعادة من الدالة 
        Dim numToString As String = Format(Amount, "000000000000.00")                           'متغير يحمل الرقم المرسل كقيمة نصية

        Dim currencyNumParts As Integer = Right(numToString, 2)                       'متغير حمل قيمة أجزاء العملة من القيمة الرقمية
        Dim firstNumPart As Integer = Mid(numToString, 10, 3)                     'متغير حمل قيمة الآحاد إلى المئات من القيمة الرقمية
        Dim secondNumPart As Integer = Mid(numToString, 7, 3)              'متغير حمل قيمة آحاد الآلاف إلى مئات الآلاف من القيمة الرقمية
        Dim thirdNumPart As Integer = Mid(numToString, 4, 3)        'متغير حمل قيمة آحاد الملايين إلى مئات الملايين  من القيمة الرقمية
        Dim forthNumPart As Integer = Mid(numToString, 1, 3)    'متغير حمل قيمة آحاد المليارات إلى مئات المليارات  من القيمة الرقمية

        Dim currencyTxtPart As String = String.Empty                                 'متغير حمل قيمة أجزاء العملة من القيمة النصية
        Dim firstTxtPart As String = String.Empty                                 'متغير حمل قيمة الآحاد إلى المئات من القيمة النصية
        Dim secondTxtPart As String = String.Empty                                            'متغير حمل قيمة الآلاف من القيمة النصية
        Dim thirdTxtPart As String = String.Empty                                          'متغير حمل قيمةالملايين  من القيمة النصية
        Dim forthTxtPart As String = String.Empty                                       'متغير حمل قيمة المليارات  من القيمة النصية

        Dim once = 0, tens = 0, hundreds As Integer = 0      'متغيرات حمل قيمة الآحاد والعشرات والمئات من القيمة الرقمية

        Dim CurGender As String = "female"
        Dim CentGender As String = "male"

        Dim DualCur As String = "شاقلان"
        Dim DualCent As String = "اغورتان"

        Dim DualNat As String = "اسرائيليان" 'متغير يحمل مثنى اسم الدولة مثل ليرتان سوريتان
        Dim PluralNat As String = String.Empty 'متغير يحمل جمع اسم الدولة إذا كان مثل دينار أردني أو دنانير أردنية حيث لايمكن أن تكون مثل ليرة سورية أو ليرات سورية وذلك لأن اسم العملة مذكر

        If SingleCur <> "شيقل" Then
            If Right(SingleCur, 1) = "ة" Then
                CurGender = "female"
                Dim sinCur As String = Left(SingleCur, Len(SingleCur) - 1)
                DualCur = sinCur & "ان"
            Else
                CurGender = "male"
                DualCur = SingleCur & "ان"
            End If
        End If

        If SingleCent <> "اغورة" Then
            If Right(SingleCent, 1) = "ة" Then
                CentGender = "female"
                Dim sinCen As String = Left(SingleCent, Len(SingleCent) - 1)
                DualCent = sinCen & "تان"
            Else
                CentGender = "male"
                DualCent = SingleCent & "ان"
            End If
        End If

        If SingleNat <> "اسرائيلي" Then
            If Right(SingleNat, 1) = "ة" Or Right(SingleNat, 1) = "ا" Then
                CentGender = "female"
                DualNat = (Left(SingleNat, (Len(SingleNat) - 1))) & "تان"
            Else
                DualNat = SingleNat & "ان"
            End If
        End If

        If CurGender = "male" Then PluralNat = SingleNat & "ة"
        '---------------------------------------------------------------------
        'البدء بتحليل أول ثلاث أرقام صحيحة
        Dim temp As String = Format(firstNumPart, "000")
        once = Val(Right(temp, 1))
        tens = Val(Mid(temp, 2, 1))
        hundreds = Val(Left(temp, 1))
        If firstNumPart <> 0 Then
            If CurGender = "male" Then
                firstTxtPart = maCurrency(firstNumPart, SingleCur, DualCur, SingleNat, DualNat)
            Else
                firstTxtPart = femaCurrency(firstNumPart, SingleCur, DualCur, SingleNat, DualNat)
            End If

            If once = 0 AndAlso tens = 1 Then
                If CurGender = "male" Then firstTxtPart = firstTxtPart & " " & PluralCur & " " & PluralNat
                If CurGender = "female" Then firstTxtPart = firstTxtPart & " " & PluralCur & " " & SingleNat
            ElseIf once = 2 AndAlso tens = 1 Then
                If CurGender = "female" Then firstTxtPart = firstTxtPart & " " & SingleCur & " " & SingleNat
                If CurGender = "male" Then firstTxtPart = firstTxtPart & " " & SingleCur & "اً" & " " & SingleNat & "اً"
            ElseIf once = 1 AndAlso tens > 0 Then
                If CurGender = "female" Then firstTxtPart = firstTxtPart & " " & SingleCur & " " & SingleNat
                If CurGender = "male" Then firstTxtPart = firstTxtPart & " " & SingleCur & "اً" & " " & SingleNat & "اً"
            Else
                If firstTxtPart <> "" AndAlso once <> 1 AndAlso tens <> 0 Then

                    If CurGender = "female" Then firstTxtPart = firstTxtPart & " " & SingleCur & " " & SingleNat
                    If CurGender = "male" Then firstTxtPart = firstTxtPart & " " & SingleCur & "اً" & " " & SingleNat & "اً"
                End If
            End If
            If CurGender = "male" Then
                If once > 2 AndAlso tens = 0 Then firstTxtPart = firstTxtPart & " " & PluralCur & " " & PluralNat
                If once = 0 AndAlso tens = 0 AndAlso hundreds <> 0 Then firstTxtPart = firstTxtPart & " " & SingleCur & " " & SingleNat
            End If

            If CurGender = "female" Then
                If once > 2 AndAlso tens = 0 Then firstTxtPart = firstTxtPart & " " & PluralCur & " " & SingleNat
                If once = 0 AndAlso tens = 0 AndAlso hundreds <> 0 Then firstTxtPart = firstTxtPart & " " & SingleCur & " " & SingleNat
            End If
        End If
        If firstTxtPart <> "" Then TextAmount = firstTxtPart

        'الانتقال لتحليل مراتب الآلاف
        temp = Format(secondNumPart, "000")
        once = Val(Right(temp, 1))
        tens = Val(Mid(temp, 2, 1))
        hundreds = Val(Left(temp, 1))
        If secondNumPart <> 0 Then
            Select Case secondNumPart
                Case 1
                    If firstTxtPart = "" Then secondTxtPart = "ألف" & " " & SingleCur & " " & SingleNat
                    If firstTxtPart <> "" Then secondTxtPart = "ألفاً"
                Case 2
                    If firstTxtPart = "" Then secondTxtPart = "ألفا" & " " & SingleCur & " " & SingleNat
                    If firstTxtPart <> "" Then secondTxtPart = "ألفان"
                Case 3 To 10 : secondTxtPart = Replace(secondNumPart) & " " & "آلاف"
                    If secondNumPart = 10 Then secondTxtPart = "عشرة آلاف"
                    If firstTxtPart = "" Then secondTxtPart = secondTxtPart & " " & SingleCur & " " & SingleNat
                Case Is > 10
                    Dim x As Integer = tens & once
                    Select Case x
                        Case 1
                            secondTxtPart = Replace(hundreds * 100) & " " & "وألف"
                            If firstTxtPart <> "" Then secondTxtPart = secondTxtPart & "اً"
                        Case 2
                            secondTxtPart = Replace(hundreds * 100) & " " & "وألفا"
                            If firstTxtPart <> "" Then secondTxtPart = secondTxtPart & "ن"
                        Case 3 To 10
                            secondTxtPart = Replace(secondNumPart)
                            If x = 10 Then secondTxtPart = secondTxtPart & "ة"
                            secondTxtPart = secondTxtPart & " " & "آلاف"
                        Case Else
                            secondTxtPart = Replace(secondNumPart)
                            secondTxtPart = secondTxtPart & " " & "ألف"
                            If firstTxtPart <> "" AndAlso tens > 0 Then secondTxtPart = secondTxtPart & "اً"
                    End Select
                    If firstTxtPart = "" Then secondTxtPart = secondTxtPart & " " & SingleCur & " " & SingleNat
            End Select
        End If
        If firstTxtPart <> "" AndAlso secondTxtPart <> "" Then secondTxtPart = secondTxtPart & " " & "و" & firstTxtPart
        If firstTxtPart <> "" AndAlso secondTxtPart = "" Then secondTxtPart = firstTxtPart
        TextAmount = secondTxtPart
        'الانتقال لتحليل مراتب الملايين
        temp = Format(thirdNumPart, "000")
        once = Val(Right(temp, 1))
        tens = Val(Mid(temp, 2, 1))
        hundreds = Val(Left(temp, 1))
        If thirdNumPart <> 0 Then
            Select Case thirdNumPart
                Case 1
                    If secondTxtPart = "" Then thirdTxtPart = "مليون" & " " & SingleCur & " " & SingleNat
                    If secondTxtPart <> "" Then thirdTxtPart = "مليوناً"
                Case 2 : thirdTxtPart = "مليونان"
                    If secondTxtPart = "" Then thirdTxtPart = "مليونا" & " " & SingleCur & " " & SingleNat
                    If secondTxtPart <> "" Then thirdTxtPart = "مليونان"
                Case 3 To 10 : thirdTxtPart = Replace(thirdNumPart) & " " & "ملايين"
                    If thirdNumPart = 10 Then thirdTxtPart = "عشرة ملايين"
                    If secondTxtPart = "" Then thirdTxtPart = thirdTxtPart & " " & SingleCur & " " & SingleNat
                Case Is > 10
                    Dim x As Integer = tens & once
                    Select Case x
                        Case 1
                            thirdTxtPart = Replace(hundreds * 100) & " " & "ومليون"
                            If firstTxtPart <> "" Or secondTxtPart <> "" Then thirdTxtPart = thirdTxtPart & "اً"
                        Case 2
                            thirdTxtPart = Replace(hundreds * 100) & " " & "ومليونا"
                            If firstTxtPart <> "" Or secondTxtPart <> "" Then thirdTxtPart = thirdTxtPart & "ن"
                        Case 3 To 10
                            thirdTxtPart = Replace(thirdNumPart)
                            If x = 10 Then thirdTxtPart = thirdTxtPart & "ة"
                            thirdTxtPart = thirdTxtPart & " " & "ملايين"
                        Case Else
                            thirdTxtPart = Replace(thirdNumPart)
                            thirdTxtPart = thirdTxtPart & " " & "مليون"
                            If firstTxtPart <> "" Or secondTxtPart <> "" Then thirdTxtPart = thirdTxtPart & "اً"
                    End Select

                    If firstTxtPart = "" AndAlso secondTxtPart = "" Then thirdTxtPart = thirdTxtPart & " " & SingleCur & " " & SingleNat
            End Select
        End If
        If secondTxtPart <> "" AndAlso thirdTxtPart <> "" Then thirdTxtPart = thirdTxtPart & " " & "و" & secondTxtPart
        If secondTxtPart <> "" AndAlso thirdTxtPart = "" Then thirdTxtPart = secondTxtPart

        TextAmount = thirdTxtPart

        'الانتقال لتحليل مراتب المليارات
        temp = Format(forthNumPart, "000")
        once = Val(Right(temp, 1))
        tens = Val(Mid(temp, 2, 1))
        hundreds = Val(Left(temp, 1))
        If forthNumPart <> 0 Then
            Select Case forthNumPart
                Case 1
                    If thirdTxtPart = "" Then forthTxtPart = "مليار" & " " & SingleCur & " " & SingleNat
                    ''TextAmount = thirdtxtpart
                    If thirdTxtPart <> "" Then forthTxtPart = "ملياراً"

                Case 2 : forthTxtPart = "ملياران"
                    If thirdTxtPart = "" Then forthTxtPart = "مليارا" & " " & SingleCur & " " & SingleNat
                    ''TextAmount = thirdtxtpart
                    If thirdTxtPart <> "" Then forthTxtPart = "ملياران"
                Case 3 To 10 : forthTxtPart = Replace(forthNumPart) & " " & "مليارات"
                    If forthNumPart = 10 Then forthTxtPart = "عشرة مليارات"
                    If thirdTxtPart = "" Then forthTxtPart = forthTxtPart & " " & SingleCur & " " & SingleNat
                Case Is > 10
                    Dim x As Integer = tens & once
                    Select Case x
                        Case 1
                            forthTxtPart = Replace(hundreds * 100) & " " & "ومليار"
                            If firstTxtPart <> "" Or secondTxtPart <> "" Or thirdTxtPart <> "" Then forthTxtPart = forthTxtPart & "اً"
                        Case 2
                            forthTxtPart = Replace(hundreds * 100) & " " & "ومليارا"
                            If firstTxtPart <> "" Or secondTxtPart <> "" Or thirdTxtPart <> "" Then forthTxtPart = forthTxtPart & "ن"
                        Case 3 To 10
                            forthTxtPart = Replace(forthNumPart)
                            If x = 10 Then forthTxtPart = forthTxtPart & "ة"
                            forthTxtPart = forthTxtPart & " " & "مليارات"
                        Case Else
                            forthTxtPart = Replace(forthNumPart)
                            forthTxtPart = forthTxtPart & " " & "مليار"
                            If firstTxtPart <> "" Or secondTxtPart <> "" Or thirdTxtPart <> "" Then forthTxtPart = forthTxtPart & "اً"
                    End Select

                    If firstTxtPart = "" AndAlso secondTxtPart = "" AndAlso thirdTxtPart = "" Then forthTxtPart = forthTxtPart & " " & SingleCur & " " & SingleNat
            End Select
        End If
        If thirdTxtPart <> "" AndAlso forthTxtPart <> "" Then forthTxtPart = forthTxtPart & " " & "و" & thirdTxtPart
        If thirdTxtPart <> "" AndAlso forthTxtPart = "" Then forthTxtPart = thirdTxtPart

        TextAmount = forthTxtPart

        'الانتقال لتحليل مراتب الأجزاء
        If currencyNumParts <> 0 Then
            Select Case currencyNumParts
                Case 1
                    If CentGender = "male" Then currencyTxtPart = SingleCent & "اً" & " " & "واحداً"
                    If CentGender = "female" Then currencyTxtPart = SingleCent & " " & "واحدة"
                Case 2
                    If CentGender = "male" Then currencyTxtPart = DualCent & " " & "إثنان"
                    If CentGender = "female" Then currencyTxtPart = DualCent & " " & "إثنتان"
                Case 3 To 10 : currencyTxtPart = Replace(currencyNumParts) & " " & PluralCent
                    If currencyNumParts = 10 Then currencyTxtPart = "عشرة" & " " & PluralCent
                    If CentGender = "male" Then
                        currencyTxtPart = maCurrency(currencyNumParts, SingleCent, DualCent, "", "") & " " & PluralCent
                    Else
                        currencyTxtPart = femaCurrency(currencyNumParts, SingleCent, DualCent, "", "") & " " & PluralCent
                    End If
                Case Is > 10
                    If CentGender = "male" Then
                        currencyTxtPart = maCurrency(currencyNumParts, SingleCent, DualCent, "", "") & " " & SingleCent & "اً"
                    Else
                        currencyTxtPart = femaCurrency(currencyNumParts, SingleCent, DualCent, "", "") & " " & SingleCent
                    End If

                    If currencyNumParts = 11 Then
                        If CentGender = "male" Then currencyTxtPart = "أحد عشر" & " " & SingleCent & "اً"
                        If CentGender = "female" Then currencyTxtPart = "إحدى عشرة" & " " & SingleCent
                    End If
                    If currencyNumParts = 12 Then
                        If CentGender = "male" Then currencyTxtPart = "إثنا عشر" & " " & SingleCent & "اً"
                        If CentGender = "female" Then currencyTxtPart = "إثنتا عشرة" & " " & SingleCent

                    End If
            End Select
        End If
        If currencyTxtPart <> "" Then
            currencyTxtPart = "( " & currencyTxtPart & " )"
            If TextAmount <> "" Then TextAmount = TextAmount & " " & " و" & currencyTxtPart
        End If
        If TextAmount = "" Then
            TextAmount = currencyTxtPart
        End If
        Return TextAmount
    End Function


    Private Function maCurrency(ByVal strNumber As String, ByVal singleUnit As String, ByVal dualUnit As String,
                                ByVal singleNat As String, ByVal dualNat As String) As String

        Dim OnceResult As String = String.Empty
        Dim TensResult As String = String.Empty
        Dim HundredResult As String = String.Empty
        Dim FinalResult As String = String.Empty
        Dim temp As Integer = Val(strNumber)

        Dim once = 0, tens = 0, hundreds As Integer = 0      'متغيرات حمل قيمة الآحاد والعشرات والمئات من القيمة الرقمية
        strNumber = Format(temp, "000")
        once = Val(Right(strNumber, 1))
        tens = Val(Mid(strNumber, 2, 1))
        hundreds = Val(Left(strNumber, 1))

        Select Case once
            Case 1 : OnceResult = "واحد"
            Case 2 : OnceResult = "إثنان"
            Case 3 : OnceResult = "ثلاثة"
            Case 4 : OnceResult = "أربعة"
            Case 5 : OnceResult = "خمسة"
            Case 6 : OnceResult = "ستة"
            Case 7 : OnceResult = "سبعة"
            Case 8 : OnceResult = "ثمانية"
            Case 9 : OnceResult = "تسعة"
        End Select

        Select Case tens
            Case 1 : TensResult = "عشر"
            Case 2 : TensResult = "عشرون"
            Case 3 : TensResult = "ثلاثون"
            Case 4 : TensResult = "أربعون"
            Case 5 : TensResult = "خمسون"
            Case 6 : TensResult = "ستون"
            Case 7 : TensResult = "سبعون"
            Case 8 : TensResult = "ثمانون"
            Case 9 : TensResult = "تسعون"
        End Select

        If OnceResult <> "" AndAlso Val(tens) > 1 Then TensResult = OnceResult & " و" & TensResult
        If TensResult = "" Then TensResult = OnceResult
        If once = 1 AndAlso tens = 0 Then TensResult = singleUnit & "اً" & " " & singleNat & "اً" & " " & "واحداً"
        If once = 2 AndAlso tens = 0 Then TensResult = dualUnit & " " & dualNat
        If once = 0 AndAlso tens = 1 Then TensResult = "عشرة"
        If once = 1 AndAlso tens = 1 Then TensResult = "أحد عشر"
        If once = 2 AndAlso tens = 1 Then TensResult = "إثنا عشر"
        If once > 2 AndAlso tens = 1 Then TensResult = OnceResult & " " & TensResult

        Dim strHundred As String = Format(hundreds, "000")

        Select Case hundreds
            Case Is = 1 : HundredResult = "مئة"
            Case Is = 2 : HundredResult = "مئتان"
            Case Is > 2 : HundredResult = maCurrency(strHundred, "", "", "", "")
                ''HundredResult = Left(HundredResult, Len(HundredResult) - 1) & "مئة"
                If hundreds = 8 Then
                    HundredResult = Left(HundredResult, Len(HundredResult) - 2) & "مئة"
                Else
                    HundredResult = Left(HundredResult, Len(HundredResult) - 1) & "مئة"
                End If
        End Select

        If once = 0 AndAlso tens = 0 AndAlso hundreds = 2 Then HundredResult = "مئتا"
        If TensResult <> "" AndAlso HundredResult <> "" Then HundredResult = HundredResult & " و" & TensResult
        If HundredResult = "" Then HundredResult = TensResult

        FinalResult = HundredResult

        Return FinalResult
    End Function

    Private Function femaCurrency(ByVal strNumber As String, ByVal singleUnit As String, ByVal dualUnit As String,
                                  ByVal singleNat As String, ByVal dualNat As String) As String

        Dim OnceResult As String = String.Empty
        Dim TensResult As String = String.Empty
        Dim HundredResult As String = String.Empty
        Dim FinalResult As String = String.Empty
        Dim temp As Integer = Val(strNumber)

        Dim once = 0, tens = 0, hundreds As Integer = 0      'متغيرات حمل قيمة الآحاد والعشرات والمئات من القيمة الرقمية
        strNumber = Format(temp, "000")
        once = Val(Right(strNumber, 1))
        tens = Val(Mid(strNumber, 2, 1))
        hundreds = Val(Left(strNumber, 1))

        Select Case once
            Case 1 : OnceResult = "إحدى"
            Case 2 : OnceResult = "إثنتان"
            Case 3 : OnceResult = "ثلاث"
            Case 4 : OnceResult = "أربع"
            Case 5 : OnceResult = "خمس"
            Case 6 : OnceResult = "ست"
            Case 7 : OnceResult = "سبع"
            Case 8 : If tens > 1 Then
                    OnceResult = "ثمان"
                Else
                    OnceResult = "ثماني"
                End If
            Case 9 : OnceResult = "تسع"
        End Select

        Select Case tens
            Case 1 : TensResult = "عشرة"
            Case 2 : TensResult = "عشرون"
            Case 3 : TensResult = "ثلاثون"
            Case 4 : TensResult = "أربعون"
            Case 5 : TensResult = "خمسون"
            Case 6 : TensResult = "ستون"
            Case 7 : TensResult = "سبعون"
            Case 8 : TensResult = "ثمانون"
            Case 9 : TensResult = "تسعون"
        End Select

        If OnceResult <> "" AndAlso Val(tens) > 1 Then TensResult = OnceResult & " و" & TensResult
        If TensResult = "" Then TensResult = OnceResult
        If once = 1 AndAlso tens = 0 Then TensResult = singleUnit & " " & singleNat & " " & "واحدة"
        If once = 2 AndAlso tens = 0 Then TensResult = dualUnit & " " & dualNat
        If once = 0 AndAlso tens = 1 Then TensResult = "عشر"
        If once = 1 AndAlso tens = 1 Then TensResult = "إحدى عشرة"
        If once = 2 AndAlso tens = 1 Then TensResult = "إثنتا عشرة"
        If once > 2 AndAlso tens = 1 Then TensResult = OnceResult & " " & TensResult

        Dim strHundred As String = Format(hundreds, "000")

        Select Case hundreds
            Case Is = 1 : HundredResult = "مئة"
            Case Is = 2 : HundredResult = "مئتان"
            Case Is > 2 : HundredResult = maCurrency(strHundred, "", "", "", "")
                If hundreds = 8 Then
                    HundredResult = Left(HundredResult, Len(HundredResult) - 2) & "مئة"
                Else
                    HundredResult = Left(HundredResult, Len(HundredResult) - 1) & "مئة"
                End If
        End Select
        If once = 0 AndAlso tens = 0 AndAlso hundreds = 2 Then HundredResult = "مئتا"
        If TensResult <> "" AndAlso HundredResult <> "" Then HundredResult = HundredResult & " و" & TensResult
        If HundredResult = "" Then HundredResult = TensResult
        FinalResult = HundredResult
        Return FinalResult
    End Function

    Private Function Replace(ByVal strNumber As String) As String

        Dim OnceResult As String = String.Empty
        Dim TensResult As String = String.Empty
        Dim HundredResult As String = String.Empty
        Dim FinalResult As String = String.Empty
        Dim temp As Integer = Val(strNumber)
        Dim once = 0, tens = 0, hundreds As Integer = 0      'متغيرات حمل قيمة الآحاد والعشرات والمئات من القيمة الرقمية
        strNumber = Format(temp, "000")
        once = Val(Right(strNumber, 1))
        tens = Val(Mid(strNumber, 2, 1))
        hundreds = Val(Left(strNumber, 1))

        Select Case once
            Case 1 : OnceResult = "واحد"
            Case 2 : OnceResult = "إثنان"
            Case 3 : OnceResult = "ثلاثة"
            Case 4 : OnceResult = "أربعة"
            Case 5 : OnceResult = "خمسة"
            Case 6 : OnceResult = "ستة"
            Case 7 : OnceResult = "سبعة"
            Case 8 : OnceResult = "ثمانية"
            Case 9 : OnceResult = "تسعة"
        End Select

        Select Case tens
            Case 1 : TensResult = "عشر"
            Case 2 : TensResult = "عشرون"
            Case 3 : TensResult = "ثلاثون"
            Case 4 : TensResult = "أربعون"
            Case 5 : TensResult = "خمسون"
            Case 6 : TensResult = "ستون"
            Case 7 : TensResult = "سبعون"
            Case 8 : TensResult = "ثمانون"
            Case 9 : TensResult = "تسعون"
        End Select

        If OnceResult <> "" AndAlso Val(tens) > 1 Then TensResult = OnceResult & " و" & TensResult
        If tens = 1 AndAlso once > 2 Then TensResult = OnceResult & " " & TensResult
        If TensResult = "" Then TensResult = OnceResult
        If once = 1 AndAlso tens = 1 Then TensResult = "أحد عشر"
        If once = 2 AndAlso tens = 1 Then TensResult = "إثنا عشر"
        Dim strHundred As String = Format(hundreds, "000")

        Select Case hundreds
            Case Is = 1 : HundredResult = "مئة"
            Case Is = 2 : HundredResult = "مئتان"
            Case Is > 2 : HundredResult = maCurrency(strHundred, "", "", "", "")
                If hundreds = 8 Then
                    HundredResult = Left(HundredResult, Len(HundredResult) - 2) & "مئة"
                Else
                    HundredResult = Left(HundredResult, Len(HundredResult) - 1) & "مئة"
                End If

        End Select

        If once = 0 AndAlso tens = 0 AndAlso hundreds = 2 Then HundredResult = "مئتا"
        If TensResult <> "" AndAlso HundredResult <> "" Then HundredResult = HundredResult & " و" & TensResult
        If HundredResult = "" Then HundredResult = TensResult

        FinalResult = HundredResult

        Return FinalResult
    End Function

#End Region

#Region "Eng Numbers To Words"


    Public Function AmountInWords(ByVal nAmount As String, Optional ByVal wAmount As String = vbNullString, Optional ByVal nSet As Object = Nothing) As String
        'Let's make sure entered value is numeric
        If Not IsNumeric(nAmount) Then Return "Please enter numeric values only."

        'I used this kind of approach using String instead of Double as datatype then I applied string manipulation
        'and remove the decimal value then store it in tempDecValue and re-attach it to nAmount upon entering the recursion
        'The reason is that I tried using the Double datatype but when it comes to quadrillion values and with decimal values
        'I noticed that the ones and tens values changed, or some errors encountered. I couldn't figure it out until I came up with this concept.

        'tempDecValue gets the decimal value from the original value nAmount 
        'tempDecValue is added to the nAmount every time it enters the recursion
        Dim tempDecValue As String = String.Empty : If InStr(nAmount, ".") Then tempDecValue = nAmount.Substring(nAmount.IndexOf("."))

        'Removing the decimal value from nAmount
        nAmount = Strings.Replace(nAmount, tempDecValue, String.Empty)

        Try
            'Assigning the nAmount to intAmount having the LONG datatype
            Dim intAmount As Long = nAmount

            'Let's trap the values entered into the recursion; if greater than 0 then let's evaluate the numbers in set, otherwise, 
            '(1) all numbers have already been evaluated and return the designated word values (for entered whole numbers); or
            '(2) evaluate last set, which is the decimal value
            'either which proceed to else statement and/or return the result
            If intAmount > 0 Then
                'Let's segregate the entered values into 3-digit sets of numbers and count it then store in the nSet
                'For example: 9223372036854775807 has 7 sets; start from right to left (807, 775, 854, 036...), until the last set which is 9 
                nSet = IIf((intAmount.ToString.Trim.Length / 3) > (CLng(intAmount.ToString.Trim.Length / 3)), CLng(intAmount.ToString.Trim.Length / 3) + 1, CLng(intAmount.ToString.Trim.Length / 3))

                'eAmount gets each 3-digit set entered into the recursion
                'Though I said sets start from right to left, we start evaluating each set from left to right of the entered values.
                'For example: 9223372036854775807 (actual: 9,223,372,036,854,775,807), 9 (1st set to be evaluated), 223 (next set), 372 (next set),... and so on
                'and so forth until all are evaluated, including the decimal values, in case there exists
                'NOTE: 9 is in Quintillion range, 223 is in Quadrillion, 372 in Trillion, and so on and so forth
                Dim eAmount As Long = Microsoft.VisualBasic.Left(intAmount.ToString.Trim, (intAmount.ToString.Trim.Length - ((nSet - 1) * 3)))

                'Multiplier gets the 10 to the power of the nSet
                'This is needed to remove the evaluated set from the original values
                'For example: 1st set is 9 (the 1st value of eAmount) and there are 7 sets so:
                '10 ^ (((7 - 1) * 3)) is 1000000000000000000
                '9 multiplied by 1000000000000000000 is 9000000000000000000
                'remove 9000000000000000000 from 9223372036854775807 which results in 223372036854775807, the next value of nAmount within which the decimal value .75 shall be attached
                'the flow goes on and on until nAmount reaches zero, or the decimal value
                'so multiplier has a vital function as we go on and we shall see later
                Dim multiplier As Long = 10 ^ (((nSet - 1) * 3))

                'These are the worded values
                Dim Ones() As String = {"", "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine"}
                Dim Teens() As String = {"", "Eleven", "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen", "Seventeen", "Eighteen", "Nineteen"}
                Dim Tens() As String = {"", "Ten", "Twenty", "Thirty", "Forty", "Fifty", "Sixty", "Seventy", "Eighty", "Ninety"}
                Dim HMBT() As String = {"", "", "Thousand", "Million", "Billion", "Trillion", "Quadrillion", "Quintillion"}

                'Let's reuse the intAmount object
                intAmount = eAmount

                'Remember, we have a 3-digit number in each set
                'from left to right is hundreds, tens, and ones
                'For example 9 is the ones in the first set for evaluation; the next set is 223 (1st 2 is hundreds, next 2 is tens, and 3 is ones)
                'These statements segregate the numbers in the set
                Dim nHundred As Integer = intAmount \ 100 : intAmount = intAmount Mod 100
                Dim nTen As Integer = intAmount \ 10 : intAmount = intAmount Mod 10
                Dim nOne As Integer = intAmount \ 1

                'After the segregation, we now have to evaluate the retrieved numbers and put the corresponding words for each number                
                If nHundred > 0 Then wAmount = wAmount & Ones(nHundred) & " Hundred " 'This is for hundreds                
                If nTen > 0 Then 'This is for tens and teens
                    'In case of teens, like 11 to 19, it will be trapped in here and proceed to the designated word                    
                    If nTen = 1 And nOne > 0 Then 'This is for teens (number ten plus ones succeeding it) Example:
                        wAmount = wAmount & Teens(nOne) & " " '11 is Eleven, 12 is Twelve, 15 is Fifteen, 17 is Seventeen, and so on
                    Else 'This is for tens, 10 to 90
                        wAmount = wAmount & Tens(nTen) & IIf(nOne > 0, "-", " ") 'If there is ones succeeding it, put a dash before the ones, example: Twenty-, Thirty-, Forty-, etc.
                        If nOne > 0 Then wAmount = wAmount & Ones(nOne) & " " 'This is for ones (1-9) with the tens (20-90) preceeding it. Example: 21 is Twenty-One, 34 is Thirty-Four, 48 is Forty-Eight
                    End If
                Else 'This is for ones, 1 to 9
                    If nOne > 0 Then wAmount = wAmount & Ones(nOne) & " "
                End If
                'Let's put in here the ranges (1st set(number 9) is Quintillion, next(number 223) is Quadrillion, next(372) is Trillion, ...so on and so forth)
                wAmount = wAmount & HMBT(nSet) & " "
                'Proceed to next set to evaluate by entering the same function we are in (recursion)
                wAmount = AmountInWords(CStr(CLng(nAmount) - (eAmount * multiplier)).Trim & tempDecValue, wAmount, nSet - 1)
            Else 'When nAmount reaches 0, we proceed to this statement
                'If there is no decimal value then proceed to display the result
                'But if there is a decimal value then let's evaluate each number of the decimal value
                'First we re-attach the decimal value to its original value, the nAmount
                If Val(nAmount) = 0 Then nAmount = nAmount & tempDecValue : tempDecValue = String.Empty
                'Now, if we have decimal value, let's convert it into whole numbers (not rounding it off, but actually converting to whole numbers), 
                'put it into the recursion for the last time, evaluate it, give the corresponding words and then finally, display the result
                If (Math.Round(Val(nAmount), 2) * 100) > 0 Then wAmount = Trim(AmountInWords(CStr(Math.Round(Val(nAmount), 2) * 100), wAmount.Trim & " Nis And ", 1)) & " Agoras"
            End If
        Catch ex As Exception
            'Should there be any error encountered, this will handle it.
            MessageBox.Show("Error Encountered: " & ex.Message, "Convert Numbers To Words", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return "!#ERROR_ENCOUNTERED"
        End Try

        'Trap null values
        If IsNothing(wAmount) = True Then wAmount = String.Empty Else wAmount = IIf(InStr(wAmount.Trim.ToLower, "nis"), wAmount.Trim, wAmount.Trim & " Nis")

        'Display the result
        Return wAmount
    End Function


#End Region
End Module


