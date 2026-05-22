Imports System.Linq
Imports System.Windows.Forms
Imports DevExpress.XtraEditors.Controls
Imports DevExpress.XtraGrid.Views.Base

Public Class FrmLabWhatsHistory

    Public Property InitialLabId As Integer

    Private _rows As New List(Of HistoryGridRow)()

    Public Sub New()
        InitializeComponent()
        ConfigureGrid()
    End Sub

    Private Sub FrmLabWhatsHistory_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            BindLabLookup()
            BindSubjectLookup()
            dtFrom.EditValue = Date.Today.AddDays(-30)
            dtTo.EditValue = Date.Today
            If InitialLabId > 0 Then lookUpLab.EditValue = InitialLabId
            LoadHistory()
        Catch ex As Exception
            ApptErrorHelper.Report(ex,
                                   "FrmLabWhatsHistory.Load",
                                   showUser:=True,
                                   owner:=Me,
                                   englishMessage:="Could not load the lab WhatsApp history.",
                                   arabicMessage:="تعذر تحميل سجل واتساب المختبر.")
        End Try
    End Sub

    Private Sub ConfigureGrid()
        viewHistory.OptionsBehavior.Editable = False
        viewHistory.OptionsSelection.EnableAppearanceFocusedCell = False
        viewHistory.OptionsView.ShowGroupPanel = False
        viewHistory.OptionsFind.AlwaysVisible = True
        AddHandler viewHistory.FocusedRowChanged, AddressOf viewHistory_FocusedRowChanged
    End Sub

    Private Function GetSelectedLabId() As Integer
        Dim ev = lookUpLab.EditValue
        If ev Is Nothing OrElse IsDBNull(ev) Then Return 0
        Try
            Return Convert.ToInt32(ev)
        Catch
            Return 0
        End Try
    End Function

    Private Function GetSelectedSubjectId() As Integer
        Dim ev = lookUpSubject.EditValue
        If ev Is Nothing OrElse IsDBNull(ev) Then Return 0
        Try
            Return Convert.ToInt32(ev)
        Catch
            Return 0
        End Try
    End Function

    Private Function GetDateValue(editor As DevExpress.XtraEditors.DateEdit) As Nullable(Of Date)
        If editor Is Nothing OrElse editor.EditValue Is Nothing OrElse IsDBNull(editor.EditValue) Then Return Nothing
        Try
            Return Convert.ToDateTime(editor.EditValue)
        Catch
            Return Nothing
        End Try
    End Function

    Private Sub BindLabLookup()
        Dim list = New LabDATA().SelectAll().
            Select(Function(l) New LookupRow With {.Id = l.LabID, .Name = If(l.LabName, "")}).
            OrderBy(Function(x) x.Name).
            ToList()

        lookUpLab.Properties.DataSource = list
        lookUpLab.Properties.DisplayMember = NameOf(LookupRow.Name)
        lookUpLab.Properties.ValueMember = NameOf(LookupRow.Id)
        lookUpLab.Properties.Columns.Clear()
        lookUpLab.Properties.Columns.Add(New LookUpColumnInfo(NameOf(LookupRow.Name), If(Eng, "Lab", "مختبر")) With {.Width = 240})
        lookUpLab.Properties.NullText = ""
    End Sub

    Private Sub BindSubjectLookup()
        Dim list = New LabMsgSubjectDATA().SelectActive().
            Select(Function(s) New LookupRow With {.Id = s.LabMsgSubjectID, .Name = If(s.SubjectName, "")}).
            OrderBy(Function(x) x.Name).
            ToList()

        lookUpSubject.Properties.DataSource = list
        lookUpSubject.Properties.DisplayMember = NameOf(LookupRow.Name)
        lookUpSubject.Properties.ValueMember = NameOf(LookupRow.Id)
        lookUpSubject.Properties.Columns.Clear()
        lookUpSubject.Properties.Columns.Add(New LookUpColumnInfo(NameOf(LookupRow.Name), If(Eng, "Subject", "الموضوع")) With {.Width = 220})
        lookUpSubject.Properties.NullText = ""
    End Sub

    Private Sub LoadHistory()
        Dim labId = GetSelectedLabId()
        Dim subjectId = GetSelectedSubjectId()
        Dim fromDate = GetDateValue(dtFrom)
        Dim toDate = GetDateValue(dtTo)

        Dim rows = New LabMsgsDATA().SelectAll().
            Where(Function(x) x.IsSent).
            AsEnumerable()

        If labId > 0 Then
            rows = rows.Where(Function(x) x.LabID.HasValue AndAlso x.LabID.Value = labId)
        End If

        If subjectId > 0 Then
            rows = rows.Where(Function(x) x.LabMsgSubjectID = subjectId)
        End If

        If fromDate.HasValue Then
            rows = rows.Where(Function(x) x.MsgDate.Date >= fromDate.Value.Date)
        End If

        If toDate.HasValue Then
            rows = rows.Where(Function(x) x.MsgDate.Date <= toDate.Value.Date)
        End If

        _rows = rows.
            OrderByDescending(Function(x) x.MsgDate).
            ThenByDescending(Function(x) x.LabMsgID).
            Select(Function(x) New HistoryGridRow With {
                .LabMsgID = x.LabMsgID,
                .MsgDate = x.MsgDate,
                .LabName = If(x.LabName, ""),
                .PatientName = If(x.PatientName, ""),
                .SubjectText = If(x.SubjectText, ""),
                .ReceiveDate = x.ReceiveDate,
                .ClinicName = If(x.ClinicName, ""),
                .Note = If(x.Note, ""),
                .MessageBody = If(x.MessageBody, ""),
                .SentDate = x.SentDate
            }).
            ToList()

        gridHistory.DataSource = Nothing
        gridHistory.DataSource = _rows
        ConfigureGridColumns()

        If _rows.Count > 0 Then
            viewHistory.FocusedRowHandle = 0
            ShowSelectedHistory()
        Else
            ClearDetails()
        End If
    End Sub

    Private Sub ConfigureGridColumns()
        If viewHistory.Columns.Count = 0 Then Return

        For Each col In viewHistory.Columns
            col.Visible = False
        Next

        ShowColumn(NameOf(HistoryGridRow.MsgDate), If(Eng, "Created", "تاريخ الإنشاء"), 0, "dd/MM/yyyy HH:mm")
        ShowColumn(NameOf(HistoryGridRow.LabName), If(Eng, "Lab", "المختبر"), 1)
        ShowColumn(NameOf(HistoryGridRow.PatientName), If(Eng, "Patient", "المريض"), 2)
        ShowColumn(NameOf(HistoryGridRow.SubjectText), If(Eng, "Subject", "الموضوع"), 3)
        ShowColumn(NameOf(HistoryGridRow.ReceiveDate), If(Eng, "Date", "التاريخ"), 4, "dd/MM/yyyy")
    End Sub

    Private Sub ShowColumn(fieldName As String, caption As String, visibleIndex As Integer, Optional formatString As String = Nothing)
        Dim col = viewHistory.Columns.ColumnByFieldName(fieldName)
        If col Is Nothing Then Return
        col.Visible = True
        col.Caption = caption
        col.VisibleIndex = visibleIndex
        If Not String.IsNullOrWhiteSpace(formatString) Then
            col.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
            col.DisplayFormat.FormatString = formatString
        End If
    End Sub

    Private Sub viewHistory_FocusedRowChanged(sender As Object, e As FocusedRowChangedEventArgs)
        ShowSelectedHistory()
    End Sub

    Private Sub ShowSelectedHistory()
        Dim row = TryCast(viewHistory.GetFocusedRow(), HistoryGridRow)
        If row Is Nothing Then
            ClearDetails()
            Return
        End If

        txtPatient.Text = row.PatientName
        txtSubject.Text = row.SubjectText
        txtMsgDate.Text = row.MsgDate.ToString("dd/MM/yyyy HH:mm")
        txtReceiveDate.Text = If(row.ReceiveDate.HasValue, row.ReceiveDate.Value.ToString("dd/MM/yyyy"), "")
        memoNote.Text = row.Note
        memoMessageBody.Text = row.MessageBody
        txtClinicLab.Visible = True
        txtClinicLab.Text = If(Eng, $"Clinic: {row.ClinicName}    Lab: {row.LabName}", $"العيادة: {row.ClinicName}    المختبر: {row.LabName}")

        Dim details = New LabMsgDetailDATA().SelectByLabMsgID(row.LabMsgID).
            OrderBy(Function(x) x.SortOrder).
            ThenBy(Function(x) x.LabMsgDetailID).
            Select(Function(x) x.DetailText).
            ToList()

        listDetails.DataSource = Nothing
        listDetails.DataSource = details
        ApplyMessageDirection(row.MessageBody)
    End Sub

    Private Sub ApplyMessageDirection(message As String)
        Dim isEnglish = LooksEnglish(message)
        memoMessageBody.RightToLeft = If(isEnglish, RightToLeft.No, RightToLeft.Yes)
        memoMessageBody.Properties.Appearance.Options.UseTextOptions = True
        memoMessageBody.Properties.Appearance.TextOptions.HAlignment = If(isEnglish, DevExpress.Utils.HorzAlignment.Near, DevExpress.Utils.HorzAlignment.Far)
    End Sub

    Private Shared Function LooksEnglish(text As String) As Boolean
        If String.IsNullOrWhiteSpace(text) Then Return False
        Return text.Any(Function(ch) (ch >= "A"c AndAlso ch <= "Z"c) OrElse (ch >= "a"c AndAlso ch <= "z"c))
    End Function

    Private Sub ClearDetails()
        txtClinicLab.Text = ""
        txtClinicLab.Visible = False
        txtPatient.Text = ""
        txtSubject.Text = ""
        txtMsgDate.Text = ""
        txtReceiveDate.Text = ""
        memoNote.Text = ""
        memoMessageBody.Text = ""
        listDetails.DataSource = Nothing
    End Sub

    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        Try
            LoadHistory()
        Catch ex As Exception
            ApptErrorHelper.Report(ex, "FrmLabWhatsHistory.btnSearch_Click", showUser:=True, owner:=Me)
        End Try
    End Sub

    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        Try
            lookUpLab.EditValue = Nothing
            lookUpSubject.EditValue = Nothing
            dtFrom.EditValue = Date.Today.AddDays(-30)
            dtTo.EditValue = Date.Today
            LoadHistory()
        Catch ex As Exception
            ApptErrorHelper.Report(ex, "FrmLabWhatsHistory.btnClear_Click", showUser:=True, owner:=Me)
        End Try
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Close()
    End Sub

    Private NotInheritable Class LookupRow
        Public Property Id As Integer
        Public Property Name As String
    End Class

    Private NotInheritable Class HistoryGridRow
        Public Property LabMsgID As Integer
        Public Property MsgDate As DateTime
        Public Property LabName As String
        Public Property PatientName As String
        Public Property SubjectText As String
        Public Property ReceiveDate As Nullable(Of Date)
        Public Property ClinicName As String
        Public Property Note As String
        Public Property MessageBody As String
        Public Property SentDate As Nullable(Of Date)
    End Class

End Class