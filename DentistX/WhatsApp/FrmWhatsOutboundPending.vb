Imports System.Drawing
Imports System.ComponentModel
Imports System.Globalization
Imports System.Threading.Tasks
Imports DevExpress.Utils
Imports DevExpress.XtraEditors
Imports DevExpress.XtraEditors.Repository
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraGrid.Columns

''' <summary>Manage local dbo.WhatsAppOutboundMessage sends (manual / navigator) until dispatched: refresh + cancel.</summary>
Public Class FrmWhatsOutboundPending
    Inherits XtraForm

    Private ReadOnly _clinicId As String
    Private ReadOnly _grid As GridControl
    Private ReadOnly _view As GridView
    Private ReadOnly _binding As BindingList(Of WhatsAppOutboundPendingCasualRow)
    Private ReadOnly _btnRefresh As SimpleButton
    Private ReadOnly _btnClose As SimpleButton
    Private ReadOnly _repoCancel As RepositoryItemButtonEdit
    Private _refreshBusy As Boolean

    Public Sub New(clinicId As String)
        MyBase.New()
        Font = New Font("Calibri", 10.0F, FontStyle.Bold)
        _clinicId = If(clinicId, "").Trim()
        Text = If(Eng, "WhatsApp — pending local sends", "واتساب — إرسالات محلية قيد الانتظار")
        MinimumSize = New Size(640, 360)
        ClientSize = New Size(720, 440)
        StartPosition = FormStartPosition.CenterParent

        Dim bottomPanel As New DevExpress.XtraEditors.PanelControl With {.Dock = DockStyle.Bottom, .Height = 48}
        Controls.Add(bottomPanel)

        _btnRefresh = New SimpleButton With {.Text = If(Eng, "Refresh", "تحديث")}
        Dim btnSz = New Size(120, 32)
        _btnRefresh.Location = New Point(12, 8)
        _btnRefresh.Size = btnSz
        bottomPanel.Controls.Add(_btnRefresh)
        AddHandler _btnRefresh.Click, AddressOf BtnRefresh_Click

        _btnClose = New SimpleButton With {.Text = If(Eng, "Close", "إغلاق"), .Anchor = AnchorStyles.Top Or AnchorStyles.Right}
        _btnClose.Size = btnSz
        _btnClose.Location = New Point(bottomPanel.Width - _btnClose.Width - 12, 8)
        AddHandler bottomPanel.SizeChanged,
            Sub(s, ea)
                _btnClose.Left = bottomPanel.Width - _btnClose.Width - 12
            End Sub
        bottomPanel.Controls.Add(_btnClose)
        AddHandler _btnClose.Click, Sub(sender, args) Close()

        _binding = New BindingList(Of WhatsAppOutboundPendingCasualRow)
        _grid = New GridControl With {.Dock = DockStyle.Fill}
        Controls.Add(_grid)
        Dim gv As New GridView(_grid)
        _grid.MainView = gv
        _grid.ViewCollection.Add(gv)
        _view = gv
        _grid.DataSource = _binding

        _view.Appearance.Row.Font = Font
        _view.Appearance.HeaderPanel.Font = Font

        _repoCancel = New RepositoryItemButtonEdit With {.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor}
        _repoCancel.Buttons(0).Caption = If(Eng, "Cancel", "إلغاء")
        _repoCancel.Appearance.Font = Font
        _grid.RepositoryItems.Add(_repoCancel)
        AddHandler _repoCancel.ButtonClick, AddressOf ViewCancel_ButtonClick

        ConfigureColumns()
    End Sub

    Private Sub ConfigureColumns()
        _view.Columns.Clear()
        _view.OptionsBehavior.AutoPopulateColumns = False
        Dim colDel As New GridColumn With {.Caption = If(Eng, "Cancel", "إلغاء"), .Visible = True, .ColumnEdit = _repoCancel, .UnboundType = DevExpress.Data.UnboundColumnType.String}
        Dim colCat As New GridColumn With {.FieldName = NameOf(WhatsAppOutboundPendingCasualRow.MessageCategory), .Caption = If(Eng, "Type", "النوع"), .Visible = True}
        Dim colNum As New GridColumn With {.FieldName = NameOf(WhatsAppOutboundPendingCasualRow.TargetDigits), .Caption = If(Eng, "Number", "الرقم"), .Visible = True}
        Dim colMsg As New GridColumn With {.FieldName = NameOf(WhatsAppOutboundPendingCasualRow.MessagePreview), .Caption = If(Eng, "Message", "الرسالة"), .Visible = True}
        Dim colAtt As New GridColumn With {.FieldName = NameOf(WhatsAppOutboundPendingCasualRow.AttachmentDisplay), .Caption = If(Eng, "Attachment", "مرفق"), .Visible = True}
        Dim colAt As New GridColumn With {.FieldName = NameOf(WhatsAppOutboundPendingCasualRow.CreatedAtUtc), .Caption = If(Eng, "Queued (UTC)", "وقت الطابور (UTC)"), .Visible = True}

        _view.Columns.AddRange({colCat, colNum, colMsg, colAtt, colAt, colDel})
        For Each c As GridColumn In _view.Columns
            If ReferenceEquals(c, colDel) Then
                c.OptionsColumn.AllowEdit = True
                c.Fixed = FixedStyle.Right
                c.Width = 80
                c.OptionsColumn.FixedWidth = True
            Else
                c.OptionsColumn.AllowEdit = False
            End If
        Next
        _view.BestFitColumns()
    End Sub

    Protected Overrides Sub OnShown(e As EventArgs)
        MyBase.OnShown(e)
        BeginInvoke(New Action(Sub() FireLoadAsyncSafe()))
    End Sub

    Private Async Sub FireLoadAsyncSafe()
        Try
            Await ReloadAsyncInternal().ConfigureAwait(True)
        Catch
        End Try
    End Sub

    Private Async Sub BtnRefresh_Click(sender As Object, e As EventArgs)
        Await ReloadAsyncInternal().ConfigureAwait(True)
    End Sub

    Private Async Function ReloadAsyncInternal() As Task
        If String.IsNullOrWhiteSpace(_clinicId) OrElse _refreshBusy Then Return
        _refreshBusy = True
        Try
            Dim rows = Await Task.Run(Function() WhatsAppOutboundRepository.ListPendingCasualForClinic(_clinicId, take:=120)).ConfigureAwait(False)
            If IsDisposed OrElse Not IsHandleCreated Then Return
            Dim act As Action = Sub()
                                    _binding.Clear()
                                    If rows IsNot Nothing Then
                                        For Each r In rows
                                            _binding.Add(r)
                                        Next
                                    End If
                                End Sub
            If InvokeRequired Then BeginInvoke(act) Else act()
        Finally
            _refreshBusy = False
        End Try
    End Function

    Private Sub ViewCancel_ButtonClick(sender As Object, e As Controls.ButtonPressedEventArgs)
        Dim rowObj = TryCast(_view.GetRow(_view.FocusedRowHandle), WhatsAppOutboundPendingCasualRow)
        If rowObj Is Nothing Then Return
        WhatsAppOutboundRepository.TryCancelPendingCasualOutbound(rowObj.MessageId, _clinicId)
        FireLoadAsyncSafe()
    End Sub

    Private Sub InitializeComponent()
        Me.SuspendLayout()
        '
        'FrmWhatsOutboundPending
        '
        Me.ClientSize = New System.Drawing.Size(294, 268)
        Me.Name = "FrmWhatsOutboundPending"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.ResumeLayout(False)

    End Sub
End Class
